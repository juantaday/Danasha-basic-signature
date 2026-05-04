# Guía de Implementación: Módulo de Transferencia entre Locales/Bodegas
**Proyecto:** Danasha Basic Signature  
**Repositorio:** `https://github.com/juantaday/Danasha-basic-signature.git`  
**Tecnología:** VB.NET + C# · WinForms · SQL Server local · Supabase/PostgreSQL (nube)  
**Solución:** `Danasha Basic Signature.sln`

---

## Contexto del Negocio

| Ubicación | Tipo | Nombre lógico |
|-----------|------|---------------|
| Inés María | Local principal | `LOCAL_PRINCIPAL` |
| Inés María | Bodega adjunta | `BODEGA` |
| Matilde | Sucursal | `SUCURSAL_MATILDE` |

La red local (cable/WiFi) conecta Local Principal ↔ Bodega en Inés María sobre la misma base de datos SQL Server. La Sucursal Matilde tiene su propia instancia local de SQL Server y se sincroniza con **Supabase** como intermediario en la nube.

---

## Arquitectura de la Solución

```
[Inés María — SQL Server local]          [Supabase — Nube]        [Matilde — SQL Server local]
  Local Principal  ←──────────────────────────────────────────────→  Sucursal
  Bodega           ←─red local─→ Local Principal                      App recibe transferencia
                                    │
                                    └──── PUSH transferencia ──────→ Supabase
                                                                        │
                                                               PULL  ←──┘
```

**Regla clave:**
- Transferencia **Bodega → Sucursal Matilde** o **Local Principal → Sucursal Matilde**: usa Supabase como intermediario.
- Transferencia **Bodega → Local Principal** (misma red): operación **100% local** sin Supabase.

---

## Parte 1 — Base de Datos SQL Server Local

### 1.1 Nueva tabla `TransferenciaEncabezado`

```sql
CREATE TABLE [dbo].[TransferenciaEncabezado] (
    idTransferencia    INT           IDENTITY(1,1) PRIMARY KEY,
    NumTransferencia   VARCHAR(20)   NOT NULL,        -- ej: TRF-2025-0001
    idBodegaOrigen     INT           NOT NULL REFERENCES Bodegas(idBodega),
    idBodegaDestino    INT           NOT NULL REFERENCES Bodegas(idBodega),
    FechaEmision       DATETIME      NOT NULL DEFAULT GETDATE(),
    idUsuario          INT           NOT NULL,
    EstadoEnvio        VARCHAR(20)   NOT NULL DEFAULT 'PENDIENTE',
    -- PENDIENTE | ENVIADO | RECIBIDO | CON_NOVEDAD
    SupabaseId         VARCHAR(50)   NULL,            -- UUID devuelto por Supabase
    Novedad            NVARCHAR(500) NULL,
    FechaRecepcion     DATETIME      NULL
)
```

### 1.2 Nueva tabla `TransferenciaDetalle`

```sql
CREATE TABLE [dbo].[TransferenciaDetalle] (
    idDetalle          INT      IDENTITY(1,1) PRIMARY KEY,
    idTransferencia    INT      NOT NULL REFERENCES TransferenciaEncabezado(idTransferencia),
    idProducto         INT      NOT NULL,
    CantidadEnviada    DECIMAL(18,4) NOT NULL,
    CantidadRecibida   DECIMAL(18,4) NULL,   -- NULL hasta que la sucursal confirme
    Unidad             VARCHAR(30) NULL
)
```

### 1.3 Agregar columnas a tabla `Bodegas` (si no existen)

```sql
-- Para identificar si la bodega pertenece a otra sucursal (ciudad distinta)
ALTER TABLE Bodegas ADD EsSucursalRemota BIT NOT NULL DEFAULT 0;
ALTER TABLE Bodegas ADD CiudadSucursal   VARCHAR(100) NULL;
```

### 1.4 Procedure: descontar stock en origen

```sql
CREATE PROCEDURE sp_TransferenciaDescontarStock
    @idTransferencia INT
AS
BEGIN
    UPDATE s
    SET s.stock = s.stock - d.CantidadEnviada
    FROM Stock s
    INNER JOIN TransferenciaDetalle d ON s.idProducto = d.idProducto
    INNER JOIN TransferenciaEncabezado e ON d.idTransferencia = e.idTransferencia
    WHERE e.idTransferencia = @idTransferencia
      AND s.idBodega = e.idBodegaOrigen
END
```

### 1.5 Procedure: acreditar stock en destino (solo para transferencia local)

```sql
CREATE PROCEDURE sp_TransferenciaAcreditarStockLocal
    @idTransferencia INT
AS
BEGIN
    -- Inserta o actualiza stock en bodega destino
    MERGE Stock AS target
    USING (
        SELECT d.idProducto, d.CantidadRecibida, e.idBodegaDestino
        FROM TransferenciaDetalle d
        INNER JOIN TransferenciaEncabezado e ON d.idTransferencia = e.idTransferencia
        WHERE e.idTransferencia = @idTransferencia
    ) AS source ON target.idProducto = source.idProducto
               AND target.idBodega   = source.idBodegaDestino
    WHEN MATCHED THEN
        UPDATE SET target.stock = target.stock + source.CantidadRecibida
    WHEN NOT MATCHED THEN
        INSERT (idProducto, idBodega, stock)
        VALUES (source.idProducto, source.idBodegaDestino, source.CantidadRecibida);
END
```

### 1.6 Procedure: publicar producto nuevo en Supabase (ejecutar en Inés María al enviar)

Este procedure extrae los datos completos de un producto desde la BD local de Inés María
y los sube a la tabla `productos_sync` de Supabase, para que Matilde los pueda importar
automáticamente al aceptar la transferencia.

Se llama desde `frmTransferencia.vb` al confirmar el envío remoto (ver Parte 4).

```sql
-- No es un SP de SQL Server — es lógica de C# que llama a Supabase.
-- Ver sección 4.3 para la implementación en frmTransferencia.vb.
```

> **Nota:** La sincronización de productos nuevos se implementa en dos pasos:
> 1. **Al enviar** (Inés María): `frmTransferencia` sube a `productos_sync` en Supabase los
>    productos de la transferencia que podrían no existir en Matilde.
> 2. **Al recibir** (Matilde): `frmRecibirTransferencia` llama a `AsegurarProductoExiste()`
>    que consulta `productos_sync` e inserta el producto en la BD local si no existe.

---

> Solo se usa cuando el destino es **Sucursal Matilde** (sucursal remota).

### 2.1 Tabla en Supabase: `transferencias`

Ejecutar en el SQL Editor de Supabase:

```sql
CREATE TABLE transferencias (
    id                  UUID          DEFAULT gen_random_uuid() PRIMARY KEY,
    num_transferencia   TEXT          NOT NULL,
    bodega_origen_id    INT           NOT NULL,
    bodega_origen_nom   TEXT          NOT NULL,
    bodega_destino_id   INT           NOT NULL,
    bodega_destino_nom  TEXT          NOT NULL,
    fecha_emision       TIMESTAMPTZ   NOT NULL DEFAULT now(),
    estado              TEXT          NOT NULL DEFAULT 'PENDIENTE',
    -- PENDIENTE | RECIBIDO | CON_NOVEDAD
    novedad             TEXT,
    fecha_recepcion     TIMESTAMPTZ,
    detalle             JSONB         NOT NULL
    -- Array JSON: [{idProducto, nombre, cantidadEnviada, cantidadRecibida, unidad}]
);

-- Índice para que la sucursal consulte solo sus transferencias
CREATE INDEX idx_transferencias_destino ON transferencias(bodega_destino_id, estado);
```

### 2.2 Credenciales Supabase

Guardar en `App.config` del proyecto `ClaseView`:

```xml
<appSettings>
  <add key="SupabaseUrl"    value="https://XXXX.supabase.co" />
  <add key="SupabaseApiKey" value="eyJhbGci..." />
</appSettings>
```

### 2.3 Helper de Supabase en C# — `SupabaseHelper.cs`

Crear archivo nuevo en `CADsisVenta/Helpers/SupabaseHelper.cs`:

```csharp
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; // ya está en packages

public static class SupabaseHelper
{
    private static readonly string _url = 
        System.Configuration.ConfigurationManager.AppSettings["SupabaseUrl"];
    private static readonly string _key = 
        System.Configuration.ConfigurationManager.AppSettings["SupabaseApiKey"];

    private static HttpClient GetClient()
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("apikey", _key);
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _key);
        return client;
    }

    /// <summary>
    /// Sube una transferencia a Supabase. Devuelve el UUID generado o null si falla.
    /// </summary>
    public static async Task<string> SubirTransferenciaAsync(object payload)
    {
        using (var client = GetClient())
        {
            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // Prefer=return=representation para que Supabase devuelva el registro creado
            client.DefaultRequestHeaders.Add("Prefer", "return=representation");
            var resp = await client.PostAsync($"{_url}/rest/v1/transferencias", content);
            if (!resp.IsSuccessStatusCode) return null;
            var body = await resp.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject<dynamic>(body);
            return result[0]?.id?.ToString();
        }
    }

    /// <summary>
    /// Obtiene transferencias PENDIENTES para una bodega destino.
    /// </summary>
    public static async Task<string> ObtenerTransferenciasPendientesAsync(int idBodegaDestino)
    {
        using (var client = GetClient())
        {
            var url = $"{_url}/rest/v1/transferencias" +
                      $"?bodega_destino_id=eq.{idBodegaDestino}&estado=eq.PENDIENTE" +
                      "&order=fecha_emision.desc";
            var resp = await client.GetAsync(url);
            if (!resp.IsSuccessStatusCode) return null;
            return await resp.Content.ReadAsStringAsync();
        }
    }

    /// <summary>
    /// Actualiza el estado de una transferencia en Supabase (RECIBIDO o CON_NOVEDAD).
    /// </summary>
    public static async Task<bool> ActualizarEstadoAsync(string supabaseId, string estado, 
                                                          string novedad = null)
    {
        using (var client = GetClient())
        {
            var payload = new { estado, novedad, fecha_recepcion = DateTime.UtcNow };
            var json = JsonConvert.SerializeObject(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var resp = await client.PatchAsync(
                $"{_url}/rest/v1/transferencias?id=eq.{supabaseId}", content);
            return resp.IsSuccessStatusCode;
        }
    }
}
```

---

## Parte 2B — DLL de Conexión Directa a Supabase PostgreSQL (`SupabaseDataAccess`)

> **Por qué este DLL:** El `SupabaseHelper.cs` de la Parte 2 usa la REST API de Supabase (HTTP).
> Este proyecto separado se conecta **directamente al PostgreSQL de Supabase con Npgsql**, igual
> que Prisma usa `DATABASE_URL` y `DIRECT_URL`. Permite SQL completo, transacciones reales,
> y consultas más eficientes sin depender del API REST.
>
> **Ambos pueden coexistir.** El agente puede usar el DLL para las operaciones de transferencia
> y mantener `SupabaseHelper.cs` si ya hay otros usos del API REST en el proyecto.

### 2B.1 Crear el proyecto `SupabaseDataAccess`

En Visual Studio, dentro de la solución `Danasha Basic Signature.sln`:
- Click derecho en la solución → **Agregar → Nuevo proyecto**
- Tipo: **Biblioteca de clases (.NET Framework)** — C#
- Nombre: `SupabaseDataAccess`
- Framework: el mismo que usa `CADsisVenta` (verificar en propiedades del proyecto)

Instalar vía NuGet (con `SupabaseDataAccess` seleccionado como proyecto activo):

```
PM> Install-Package Npgsql -Version 8.0.3
PM> Install-Package Newtonsoft.Json
```

> Si el proyecto usa .NET Framework 4.6.x, usar `Npgsql 6.0.x` en lugar de 8.x.

### 2B.2 Cadenas de conexión — agregar en `App.config` del proyecto de inicio

```xml
<appSettings>
  <!-- Supabase REST API (ya existente) -->
  <add key="SupabaseUrl"    value="https://lweyoixmbsqepwewcnrb.supabase.co" />
  <add key="SupabaseApiKey" value="REEMPLAZAR_CON_ANON_KEY" />

  <!-- PostgreSQL vía PgBouncer (puerto 6543) — para CRUD normal -->
  <!-- Equivale al DATABASE_URL de Prisma con pgbouncer=true -->
  <add key="SupabasePgPoolUrl"
       value="Host=aws-0-us-west-2.pooler.supabase.com;Port=6543;Database=postgres;Username=postgres.lweyoixmbsqepwewcnrb;Password=REEMPLAZAR_CON_PASSWORD;Pooling=true;SSL Mode=Require;Trust Server Certificate=true;No Reset On Close=true;Max Auto Prepare=0" />

  <!-- PostgreSQL directo (puerto 5432) — para DDL / migraciones -->
  <!-- Equivale al DIRECT_URL de Prisma -->
  <add key="SupabasePgDirectUrl"
       value="Host=aws-0-us-west-2.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.lweyoixmbsqepwewcnrb;Password=REEMPLAZAR_CON_PASSWORD;SSL Mode=Require;Trust Server Certificate=true" />
</appSettings>
```

> ⚠️ No subir `App.config` con contraseñas al repositorio. Agregar al `.gitignore`.

### 2B.3 Archivo `SupabaseDataAccess/SupabasePgConnection.cs`

```csharp
using System.Configuration;
using System.Data;
using Npgsql;

namespace SupabaseDataAccess
{
    /// <summary>
    /// Gestiona conexiones directas a Supabase PostgreSQL via Npgsql.
    /// Usar OpenPoolConnection() para CRUD normal (puerto 6543, PgBouncer).
    /// Usar OpenDirectConnection() para DDL o migraciones (puerto 5432).
    /// Las cadenas se leen desde App.config del ejecutable principal.
    /// </summary>
    public static class SupabasePgConnection
    {
        private static string PoolUrl =>
            ConfigurationManager.AppSettings["SupabasePgPoolUrl"];

        private static string DirectUrl =>
            ConfigurationManager.AppSettings["SupabasePgDirectUrl"];

        public static NpgsqlConnection OpenPoolConnection()
        {
            var conn = new NpgsqlConnection(PoolUrl);
            conn.Open();
            return conn;
        }

        public static NpgsqlConnection OpenDirectConnection()
        {
            var conn = new NpgsqlConnection(DirectUrl);
            conn.Open();
            return conn;
        }

        /// <summary>
        /// Prueba la conectividad. Llamar antes de operaciones de red
        /// para mostrar mensaje claro al usuario si no hay internet.
        /// </summary>
        public static bool TestConnection()
        {
            try
            {
                using (var conn = OpenPoolConnection())
                    return conn.State == ConnectionState.Open;
            }
            catch { return false; }
        }
    }
}
```

### 2B.4 Archivo `SupabaseDataAccess/TransferenciaRepository.cs`

```csharp
using System;
using System.Data;
using Newtonsoft.Json;
using Npgsql;

namespace SupabaseDataAccess
{
    /// <summary>
    /// CRUD sobre la tabla 'transferencias' en Supabase PostgreSQL.
    /// Métodos síncronos para compatibilidad con WinForms .NET Framework.
    /// </summary>
    public static class TransferenciaRepository
    {
        /// <summary>
        /// Inserta una transferencia. Devuelve el UUID generado o null si falla.
        /// </summary>
        public static string SubirTransferencia(
            string numTransferencia,
            int bodegaOrigenId,    string bodegaOrigenNom,
            int bodegaDestinoId,   string bodegaDestinoNom,
            object detalle)
        {
            const string sql = @"
                INSERT INTO transferencias
                    (num_transferencia,
                     bodega_origen_id,  bodega_origen_nom,
                     bodega_destino_id, bodega_destino_nom,
                     detalle)
                VALUES (@num, @orig_id, @orig_nom, @dest_id, @dest_nom, @detalle::jsonb)
                RETURNING id;";

            using (var conn = SupabasePgConnection.OpenPoolConnection())
            using (var cmd  = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@num",      numTransferencia);
                cmd.Parameters.AddWithValue("@orig_id",  bodegaOrigenId);
                cmd.Parameters.AddWithValue("@orig_nom", bodegaOrigenNom);
                cmd.Parameters.AddWithValue("@dest_id",  bodegaDestinoId);
                cmd.Parameters.AddWithValue("@dest_nom", bodegaDestinoNom);
                cmd.Parameters.AddWithValue("@detalle",  JsonConvert.SerializeObject(detalle));
                return cmd.ExecuteScalar()?.ToString();
            }
        }

        /// <summary>
        /// Transferencias PENDIENTES para una bodega destino.
        /// Columnas: id, num_transferencia, bodega_origen_nom, fecha_emision, estado, detalle
        /// </summary>
        public static DataTable ObtenerPendientesPorDestino(int idBodegaDestino)
        {
            const string sql = @"
                SELECT id, num_transferencia, bodega_origen_nom,
                       fecha_emision, estado, detalle::text AS detalle
                FROM   transferencias
                WHERE  bodega_destino_id = @dest_id AND estado = 'PENDIENTE'
                ORDER  BY fecha_emision DESC;";

            var dt = new DataTable();
            using (var conn = SupabasePgConnection.OpenPoolConnection())
            using (var cmd  = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@dest_id", idBodegaDestino);
                using (var adapter = new NpgsqlDataAdapter(cmd))
                    adapter.Fill(dt);
            }
            return dt;
        }

        /// <summary>
        /// Actualiza estado a RECIBIDO o CON_NOVEDAD.
        /// Opcionalmente actualiza el JSONB de detalle con cantidades reales recibidas.
        /// </summary>
        public static bool ActualizarEstado(
            string supabaseId, string estado,
            string novedad = null, object detalleActualizado = null)
        {
            const string sql = @"
                UPDATE transferencias
                SET    estado          = @estado,
                       novedad         = @novedad,
                       fecha_recepcion = NOW(),
                       detalle         = COALESCE(@detalle::jsonb, detalle)
                WHERE  id = @id::uuid;";

            using (var conn = SupabasePgConnection.OpenPoolConnection())
            using (var cmd  = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id",     supabaseId);
                cmd.Parameters.AddWithValue("@estado", estado);
                cmd.Parameters.AddWithValue("@novedad",
                    (object)novedad ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@detalle",
                    detalleActualizado != null
                        ? (object)JsonConvert.SerializeObject(detalleActualizado)
                        : DBNull.Value);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>
        /// Historial de transferencias de los últimos N días (default 30).
        /// </summary>
        public static DataTable ObtenerHistorial(int ultimosDias = 30)
        {
            const string sql = @"
                SELECT id, num_transferencia, bodega_origen_nom, bodega_destino_nom,
                       fecha_emision, estado, novedad, fecha_recepcion
                FROM   transferencias
                WHERE  fecha_emision >= NOW() - (@dias || ' days')::interval
                ORDER  BY fecha_emision DESC;";

            var dt = new DataTable();
            using (var conn = SupabasePgConnection.OpenPoolConnection())
            using (var cmd  = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@dias", ultimosDias);
                using (var adapter = new NpgsqlDataAdapter(cmd))
                    adapter.Fill(dt);
            }
            return dt;
        }
    }
}
```

### 2B.5 Tabla en Supabase para sincronización de productos nuevos

Ejecutar también en el SQL Editor de Supabase (complementa la tabla `transferencias` del paso 2.1):

```sql
-- Tabla auxiliar para recibir productos nuevos que no existen en Matilde
CREATE TABLE IF NOT EXISTS productos_sync (
    id               UUID        DEFAULT gen_random_uuid() PRIMARY KEY,
    id_producto_orig INT         NOT NULL,   -- idProducto en BD de Inés María
    nom_comercial    TEXT        NOT NULL,
    nom_comun        TEXT,
    descripcion      TEXT,
    cant_minima      DECIMAL(18,4) DEFAULT 1,
    id_unidad        INT         DEFAULT 1,
    id_subcategoria  INT,
    iva_porcentaje   DECIMAL(5,2) DEFAULT 0,
    facturable       BOOLEAN     DEFAULT TRUE,
    -- Presentación por defecto
    cod_producto     TEXT,
    cant_present     DECIMAL(18,4) DEFAULT 1,
    precio_compra    DECIMAL(18,6) DEFAULT 0,
    precio_venta     DECIMAL(18,6) DEFAULT 0,
    unidad_present   TEXT,
    -- Control de sincronización
    estado_sync      TEXT        DEFAULT 'PENDIENTE',
    -- PENDIENTE | APLICADO
    fecha_creacion   TIMESTAMPTZ DEFAULT now()
);

CREATE INDEX IF NOT EXISTS idx_productos_sync_estado
    ON productos_sync(estado_sync);
```

### 2B.6 Agregar referencia del DLL en `CADsisVenta` y `ClaseView`

En Visual Studio: click derecho sobre cada proyecto → **Agregar referencia → Proyectos → SupabaseDataAccess**.

### 2B.7 Adaptar `frmTransferencia` para usar el DLL (opcional)

Si se quiere reemplazar el `SupabaseHelper.cs` (REST) por el DLL (SQL directo),
cambiar en `btnConfirmar_Click` dentro de `frmTransferencia.vb`:

```vb
' En lugar de:
Dim supabaseId As String = SupabaseHelper.SubirTransferenciaAsync(payload).Result

' Usar (síncrono, sin riesgo de deadlock en WinForms):
If Not SupabaseDataAccess.SupabasePgConnection.TestConnection() Then
    MsgBox("Sin conexión a Supabase. Verifique internet.", MsgBoxStyle.Critical, "Sin conexión")
    Exit Sub
End If
Dim supabaseId As String = SupabaseDataAccess.TransferenciaRepository.SubirTransferencia(
    numTransf, idOrigen, nomOrigen, idDestino, nomDestino, payloadDetalle)
```

Y en `frmRecibirTransferencia.vb`, cambiar `CargarTransferenciasPendientes`:

```vb
' En lugar de: SupabaseHelper.ObtenerTransferenciasPendientesAsync(...).Result
' Usar:
Dim dt As DataTable = SupabaseDataAccess.TransferenciaRepository
    .ObtenerPendientesPorDestino(TerminalActivo.idBodega)
```

---

### 3.1 Renombrar `pedidoButton` → `TransferenciaButton`

**Archivo:** `ClaseView/Ventas/frmVentas.Designer.vb`

Localizar el bloque `'pedidoButton` y reemplazar solo los valores de presentación:

```vb
' Antes:
Me.pedidoButton.Text = "Pedido"
Me.pedidoButton.Tag  = "Pedido"
Me.pedidoButton.Image = Global.DanashaBasicSignature.My.Resources.Resources.TextBox_32x32

' Después:
Me.pedidoButton.Text  = "Transferir"
Me.pedidoButton.Tag   = "Transferencia"
Me.pedidoButton.Image = Global.DanashaBasicSignature.My.Resources.Resources.transfer_32
' (agregar ícono transfer_32 en Resources o usar uno existente apropiado)
```

> **Nota:** No es necesario renombrar el control en el designer; solo cambia Text y Tag. Esto minimiza el riesgo de romper referencias existentes.

### 3.2 Evento Click del botón `pedidoButton`

**Archivo:** `ClaseView/Ventas/frmVentas.vb`

Buscar el handler del `pedidoButton_Click` (o donde se maneja el Tag "Pedido") y agregar la rama de Transferencia:

```vb
Private Sub pedidoButton_Click(sender As Object, e As EventArgs) _
        Handles pedidoButton.Click
    Try
        ' Si la lista de productos está vacía, no continuar
        If DataGridView1.Rows.Count = 0 Then
            MsgBox("Agregue productos antes de crear una transferencia.", _
                   MsgBoxStyle.Exclamation, "Sin productos")
            Exit Sub
        End If

        ' Abrir el formulario de transferencia pasando la lista actual
        Using frm As New frmTransferencia(BuildDetalleTransferencia())
            frm.ShowDialog(Me)
        End Using

    Catch ex As Exception
        MsgBox(ex.Message & " en pedidoButton_Click", MsgBoxStyle.Critical, "Error")
    End Try
End Sub

''' <summary>
''' Construye la lista de detalle desde el DataGridView de ventas.
''' Ajustar los índices de columna según el diseño real del DGV.
''' </summary>
Private Function BuildDetalleTransferencia() As List(Of DetalleTransferenciaItem)
    Dim lista As New List(Of DetalleTransferenciaItem)
    For Each row As DataGridViewRow In DataGridView1.Rows
        If row.IsNewRow Then Continue For
        lista.Add(New DetalleTransferenciaItem With {
            .idProducto      = CInt(row.Cells("ColIdProducto").Value),
            .NombreProducto  = row.Cells("ColNombreProducto").Value?.ToString(),
            .Cantidad        = CDec(row.Cells("ColCantidad").Value),
            .Unidad          = row.Cells("ColUnidad").Value?.ToString()
        })
    Next
    Return lista
End Function
```

### 3.3 Modelo compartido `DetalleTransferenciaItem`

Crear archivo `ClaseView/Models/DetalleTransferenciaItem.vb`:

```vb
Public Class DetalleTransferenciaItem
    Public Property idProducto     As Integer
    Public Property NombreProducto As String
    Public Property Cantidad       As Decimal
    Public Property Unidad         As String
    Public Property CantidadRecibida As Decimal?   ' usado en recepción
End Class
```

---

## Parte 4 — Formulario `frmTransferencia` (Origen)

**Ruta:** `ClaseView/Transferencia/frmTransferencia.vb` (carpeta nueva)

### 4.1 Diseño del formulario

```
┌─────────────────────────────────────────────────────┐
│  TRANSFERENCIA DE PRODUCTOS                         │
│                                                     │
│  DESDE: [ComboBox cboOrigen  ▼]                     │
│  HACIA: [ComboBox cboDestino ▼]                     │
│                                                     │
│  ┌───────────────────────────────────────────────┐  │
│  │ # │ Producto          │ Cantidad │ Unidad      │  │
│  │ 1 │ Urea 50kg         │  100.00  │ Sacos       │  │
│  │ 2 │ Melaza            │   10.00  │ Fundas      │  │
│  └───────────────────────────────────────────────┘  │
│                                                     │
│  [Cancelar]                  [✔ Confirmar Envío]    │
└─────────────────────────────────────────────────────┘
```

### 4.2 Código `frmTransferencia.vb`

```vb
Imports CADsisVenta.DataSetSystemTableAdapters
Imports CADsisVenta.Helpers.FInicio
Imports Newtonsoft.Json

Public Class frmTransferencia

    Private _detalle As List(Of DetalleTransferenciaItem)

    Public Sub New(detalle As List(Of DetalleTransferenciaItem))
        InitializeComponent()
        _detalle = detalle
    End Sub

    Private Sub frmTransferencia_Load(sender As Object, e As EventArgs) _
            Handles MyBase.Load
        CargarOrigen()
        CargarDestinos()
        MostrarDetalle()
    End Sub

    ''' <summary>
    ''' El ORIGEN solo muestra bodegas/locales del equipo actual (por hostname).
    ''' </summary>
    Private Sub CargarOrigen()
        Dim tap As New BodegasTableAdapter   ' adapter existente
        Dim dt As DataTable = tap.GetBodegasByHostName(Dominio._HotName)
        cboOrigen.DataSource    = dt
        cboOrigen.DisplayMember = "nom_bodega"
        cboOrigen.ValueMember   = "idBodega"

        ' Pre-seleccionar la bodega activa del terminal
        cboOrigen.SelectedValue = TerminalActivo.idBodega
    End Sub

    ''' <summary>
    ''' El DESTINO muestra todas las bodegas/locales registradas EXCEPTO el origen.
    ''' </summary>
    Private Sub CargarDestinos()
        Dim tap As New BodegasTableAdapter
        Dim dt As DataTable = tap.GetAllBodegas()   ' traer todas
        ' Filtrar el origen en memoria
        Dim dv As New DataView(dt)
        dv.RowFilter = "idBodega <> " & TerminalActivo.idBodega.ToString()
        cboDestino.DataSource    = dv.ToTable()
        cboDestino.DisplayMember = "nom_bodega"
        cboDestino.ValueMember   = "idBodega"
    End Sub

    Private Sub MostrarDetalle()
        DgvDetalle.AutoGenerateColumns = False
        DgvDetalle.DataSource = _detalle
        ' Columnas ya definidas en el designer del DGV
    End Sub

    Private Sub btnConfirmar_Click(sender As Object, e As EventArgs) _
            Handles btnConfirmar.Click

        If cboOrigen.SelectedIndex < 0 OrElse cboDestino.SelectedIndex < 0 Then
            MsgBox("Seleccione origen y destino.", MsgBoxStyle.Exclamation, "Requerido")
            Exit Sub
        End If

        Dim idOrigen  As Integer = CInt(cboOrigen.SelectedValue)
        Dim idDestino As Integer = CInt(cboDestino.SelectedValue)
        Dim nomOrigen As String  = cboOrigen.Text
        Dim nomDestino As String = cboDestino.Text

        Dim confirm As MsgBoxResult = MsgBox(
            "¿Confirma la transferencia de " & _detalle.Count & " producto(s)" & vbNewLine &
            "DESDE: " & nomOrigen & vbNewLine &
            "HACIA: " & nomDestino & "?",
            MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Confirmar transferencia")

        If confirm <> MsgBoxResult.Yes Then Exit Sub

        Try
            ' 1. Determinar si el destino es sucursal remota
            Dim tap As New BodegasTableAdapter
            Dim dtDest As DataTable = tap.GetDataByIdBodega(idDestino)
            Dim esRemota As Boolean = CBool(dtDest.Rows(0)("EsSucursalRemota"))

            ' 2. Generar número de transferencia
            Dim numTransf As String = GenerarNumTransferencia()

            ' 3. Insertar encabezado en SQL local
            Dim idTransf As Integer = InsertarEncabezado(numTransf, idOrigen, idDestino)
            InsertarDetalle(idTransf)

            ' 4. Descontar stock en bodega origen
            EjecutarSP("sp_TransferenciaDescontarStock", idTransf)

            If esRemota Then
                ' 5a. Subir a Supabase de forma asíncrona
                Dim payload = New With {
                    .num_transferencia = numTransf,
                    .bodega_origen_id  = idOrigen,
                    .bodega_origen_nom = nomOrigen,
                    .bodega_destino_id = idDestino,
                    .bodega_destino_nom = nomDestino,
                    .detalle = _detalle.Select(Function(d) New With {
                        .idProducto       = d.idProducto,
                        .nombre           = d.NombreProducto,
                        .cantidadEnviada  = d.Cantidad,
                        .cantidadRecibida = CType(Nothing, Object),
                        .unidad           = d.Unidad
                    }).ToList()
                }
                Dim supabaseId As String = SupabaseHelper.SubirTransferenciaAsync(payload).Result
                ' Guardar supabaseId en el encabezado local
                ActualizarSupabaseId(idTransf, supabaseId)
            Else
                ' 5b. Transferencia local: acreditar directamente
                EjecutarSP("sp_TransferenciaAcreditarStockLocal", idTransf)
                ActualizarEstado(idTransf, "RECIBIDO")
            End If

            ' 6. Imprimir guía de remisión en ticket
            ImprimirGuiaRemision(idTransf, numTransf, nomOrigen, nomDestino)

            MsgBox("Transferencia registrada correctamente." & vbNewLine &
                   "Número: " & numTransf, MsgBoxStyle.Information, "Éxito")
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()

        Catch ex As Exception
            MsgBox("Error al procesar transferencia: " & ex.Message, _
                   MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' ─── Helpers privados ─────────────────────────────────────────────────────

    Private Function GenerarNumTransferencia() As String
        ' Formato: TRF-YYYYMMDD-NNNN  (NNNN = correlativo del día)
        Dim fecha As String = DateTime.Now.ToString("yyyyMMdd")
        Dim sql As String = "SELECT ISNULL(MAX(CAST(RIGHT(NumTransferencia,4) AS INT)),0)+1 " &
                            "FROM TransferenciaEncabezado " &
                            "WHERE NumTransferencia LIKE 'TRF-" & fecha & "-%'"
        Dim n As Integer
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            n = CInt(cmd.RetornaEscalar(sql))
        End Using
        Return "TRF-" & fecha & "-" & n.ToString("D4")
    End Function

    Private Function InsertarEncabezado(num As String, origen As Integer, destino As Integer) As Integer
        Dim sql As String =
            "INSERT INTO TransferenciaEncabezado " &
            "(NumTransferencia, idBodegaOrigen, idBodegaDestino, idUsuario, EstadoEnvio) " &
            "VALUES (@num, @origen, @destino, @usr, 'PENDIENTE'); SELECT SCOPE_IDENTITY();"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            ' Usar el método parametrizado existente del proyecto
            Return CInt(cmd.RetornaEscalarConParams(sql,
                {"@num", "@origen", "@destino", "@usr"},
                {num, origen, destino, SettingObject.UsuarioActivo.IdUsuario}))
        End Using
    End Function

    Private Sub InsertarDetalle(idTransf As Integer)
        For Each item In _detalle
            Dim sql As String =
                "INSERT INTO TransferenciaDetalle " &
                "(idTransferencia, idProducto, CantidadEnviada, Unidad) " &
                "VALUES (@idT, @idP, @cant, @unidad)"
            Using cmd As New CADsisVenta.Funtions.SqlComandExec
                cmd.EjecutarConParams(sql,
                    {"@idT", "@idP", "@cant", "@unidad"},
                    {idTransf, item.idProducto, item.Cantidad, item.Unidad})
            End Using
        Next
    End Sub

    Private Sub EjecutarSP(spName As String, idTransf As Integer)
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarSP(spName, {"@idTransferencia"}, {idTransf})
        End Using
    End Sub

    Private Sub ActualizarSupabaseId(idTransf As Integer, supabaseId As String)
        Dim sql As String = "UPDATE TransferenciaEncabezado SET SupabaseId=@sid, " &
                            "EstadoEnvio='ENVIADO' WHERE idTransferencia=@id"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(sql, {"@sid", "@id"}, {supabaseId, idTransf})
        End Using
    End Sub

    Private Sub ActualizarEstado(idTransf As Integer, estado As String)
        Dim sql As String = "UPDATE TransferenciaEncabezado SET EstadoEnvio=@est WHERE idTransferencia=@id"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(sql, {"@est", "@id"}, {estado, idTransf})
        End Using
    End Sub

End Class
```

---

## Parte 5 — Impresión de Guía de Remisión (Ticket)

**Archivo:** `ClaseView/Modulos/PrintTickets.vb` — agregar nueva función al final del módulo existente.

```vb
#Region "PrintGuiaRemision"

Public Function ImprimirGuiaRemision(ByVal idTransf As Integer,
                                     ByVal numTransf As String,
                                     ByVal nomOrigen As String,
                                     ByVal nomDestino As String) As Boolean
    Try
        Dim Ticket1 As New CreaTicket(myOptnsPrint.NamePrint,
                                      PaperSizeWidth.GetCharLenght(Printer.myOptnsPrint.PaperSizeWidth))

        Ticket1.isAvanzaLinea = True
        Ticket1.FontZiseText(FontZise.l8cpp)
        Ticket1.TextoIzquierda(SettingObject.EcommerceActive.RazonSocial, False)

        Ticket1.FontZiseText(FontZise.l4cpp)
        Ticket1.TextoIzquierda("GUIA DE REMISION INTERNA", False)
        Ticket1.TextoIzquierda("Nro: " & numTransf, False)
        Ticket1.TextoIzquierda("Fecha: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm"), False)
        Ticket1.Separador("-")

        Ticket1.TextoIzquierda("ORIGEN : " & nomOrigen, False)
        Ticket1.TextoIzquierda("DESTINO: " & nomDestino, False)
        Ticket1.Separador("-")

        Ticket1.FontZiseText(FontZise.l2cpp)
        Ticket1.TextoIzquierda(TextoDiseñado("PRODUCTO", Alinea.Izquierda, 22) &
                                TextoDiseñado("CANT", Alinea.Derecha, 8) &
                                TextoDiseñado("UND", Alinea.Derecha, 6), False)
        Ticket1.Separador("-")

        ' Leer detalle desde SQL local
        Dim sql As String =
            "SELECT p.Nombre_Producto, d.CantidadEnviada, d.Unidad " &
            "FROM TransferenciaDetalle d " &
            "INNER JOIN Productos p ON d.idProducto = p.idProducto " &
            "WHERE d.idTransferencia = " & idTransf

        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            Using dt = cmd.RetornaTabla(sql)
                For Each row As DataRow In dt.Rows
                    Dim nombre As String = row("Nombre_Producto").ToString()
                    If nombre.Length > 22 Then nombre = nombre.Substring(0, 22)
                    Dim cant As String = CDec(row("CantidadEnviada")).ToString("N2")
                    Dim unidad As String = row("Unidad")?.ToString() ?? ""
                    Ticket1.TextoIzquierda(
                        TextoDiseñado(nombre, Alinea.Izquierda, 22) &
                        TextoDiseñado(cant, Alinea.Derecha, 8) &
                        TextoDiseñado(unidad, Alinea.Derecha, 6), False)
                Next
            End Using
        End Using

        Ticket1.Separador("=")
        Ticket1.TextoIzquierda("Emitido por: " & SettingObject.UsuarioActivo.Nombre, False)
        Ticket1.TextoIzquierda("Terminal: " & Dominio._HotName, False)
        Ticket1.Separador(" ")
        Ticket1.TextoIzquierda("RECIBIDO POR: ___________________", False)
        Ticket1.TextoIzquierda("FIRMA: ___________________", False)
        Ticket1.AvanzaLinea(4)
        Ticket1.Imprimir()
        Return True

    Catch ex As Exception
        MsgBox("Error al imprimir guía: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Return False
    End Try
End Function

#End Region
```

---

## Parte 6 — Formulario `frmRecibirTransferencia` (Sucursal Matilde)

**Ruta:** `ClaseView/Transferencia/frmRecibirTransferencia.vb` (misma carpeta nueva)

### 6.1 Diseño del formulario

```
┌──────────────────────────────────────────────────────────────────┐
│  RECEPCIÓN DE TRANSFERENCIAS                    [↻ Actualizar]  │
│                                                                  │
│  Transferencias pendientes de: SUCURSAL MATILDE                 │
│                                                                  │
│  ┌────────────────────────────────────────────────────────────┐  │
│  │ [○] TRF-20250610-0001 │ Origen: Bodega │ 15/06/2025 09:30 │  │
│  │ [○] TRF-20250611-0002 │ Origen: Local  │ 16/06/2025 14:00 │  │
│  └────────────────────────────────────────────────────────────┘  │
│                                                                  │
│  Detalle de la transferencia seleccionada:                      │
│  ┌────────────────────────────────────────────────────────────┐  │
│  │ ✔ │ Producto          │ Enviado │ Recibido │ Unidad        │  │
│  │ ☑ │ Urea 50kg         │  100.00 │ [  98  ] │ Sacos        │  │
│  │ ☑ │ Melaza            │   10.00 │ [   0  ] │ Fundas       │  │
│  └────────────────────────────────────────────────────────────┘  │
│                                                                  │
│  Novedad general:                                               │
│  ┌────────────────────────────────────────────────────────────┐  │
│  │ La melasa fue robada en el camino                          │  │
│  └────────────────────────────────────────────────────────────┘  │
│                                                                  │
│  [Cancelar]                     [✔ Aceptar productos llegados] │
└──────────────────────────────────────────────────────────────────┘
```

### 6.2 Código `frmRecibirTransferencia.vb`

```vb
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class frmRecibirTransferencia

    Private _transferenciasJson As JArray
    Private _transferenciaSeleccionada As JObject

    Private Sub frmRecibirTransferencia_Load(sender As Object, e As EventArgs) _
            Handles MyBase.Load
        lblBodega.Text = "Terminal: " & Dominio._HotName & " — Bodega: " & TerminalActivo.idBodega
        CargarTransferenciasPendientes()
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) _
            Handles btnActualizar.Click
        CargarTransferenciasPendientes()
    End Sub

    Private Sub CargarTransferenciasPendientes()
        Try
            Cursor = Cursors.WaitCursor
            lblEstado.Text = "Consultando Supabase..."
            Dim json As String = SupabaseHelper.ObtenerTransferenciasPendientesAsync(
                                     TerminalActivo.idBodega).Result
            If String.IsNullOrEmpty(json) Then
                lblEstado.Text = "Sin transferencias pendientes."
                ListBoxTransf.DataSource = Nothing
                Return
            End If

            _transferenciasJson = JArray.Parse(json)
            Dim lista As New List(Of String)
            For Each t As JObject In _transferenciasJson
                lista.Add(t("num_transferencia").ToString() & " | " &
                          "Desde: " & t("bodega_origen_nom").ToString() & " | " &
                          CDate(t("fecha_emision").ToString()).ToString("dd/MM/yyyy HH:mm"))
            Next
            ListBoxTransf.DataSource = lista
            lblEstado.Text = lista.Count & " transferencia(s) pendiente(s)."

        Catch ex As Exception
            lblEstado.Text = "Error al conectar con Supabase."
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error Supabase")
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ListBoxTransf_SelectedIndexChanged(sender As Object, e As EventArgs) _
            Handles ListBoxTransf.SelectedIndexChanged
        If ListBoxTransf.SelectedIndex < 0 Then Exit Sub
        _transferenciaSeleccionada = _transferenciasJson(ListBoxTransf.SelectedIndex)
        CargarDetalleEnGrid()
    End Sub

    Private Sub CargarDetalleEnGrid()
        DgvDetalle.Rows.Clear()
        Dim detalle As JArray = _transferenciaSeleccionada("detalle")
        For Each item As JObject In detalle
            Dim rowIdx As Integer = DgvDetalle.Rows.Add()
            With DgvDetalle.Rows(rowIdx)
                .Cells("ColCheck").Value        = True
                .Cells("ColProducto").Value     = item("nombre").ToString()
                .Cells("ColEnviado").Value      = item("cantidadEnviada").ToString()
                .Cells("ColRecibido").Value     = item("cantidadEnviada").ToString() ' pre-rellena
                .Cells("ColUnidad").Value       = item("unidad")?.ToString()
                .Tag = item("idProducto").ToString()   ' guardamos idProducto en Tag de la fila
            End With
        Next
    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) _
            Handles btnAceptar.Click

        If _transferenciaSeleccionada Is Nothing Then
            MsgBox("Seleccione una transferencia.", MsgBoxStyle.Exclamation, "Requerido")
            Exit Sub
        End If

        Dim novedad As String = txtNovedad.Text.Trim()
        Dim confirm As MsgBoxResult = MsgBox(
            "¿Confirma la recepción de los productos marcados?" & vbNewLine &
            "Los productos NO marcados NO se acreditarán al inventario.",
            MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Confirmar recepción")

        If confirm <> MsgBoxResult.Yes Then Exit Sub

        Try
            Dim supabaseId As String = _transferenciaSeleccionada("id").ToString()
            Dim hayNovedad As Boolean = False

            ' 1. Acreditar al inventario local y preparar lista de recibidos
            Dim detalleActualizado As New List(Of Object)
            For Each row As DataGridViewRow In DgvDetalle.Rows
                If row.IsNewRow Then Continue For
                Dim cheked As Boolean = CBool(row.Cells("ColCheck").Value)
                Dim idProd As Integer = CInt(row.Tag)
                Dim cantEnviada As Decimal = CDec(row.Cells("ColEnviado").Value)
                Dim cantRecibida As Decimal = If(cheked,
                    CDec(row.Cells("ColRecibido").Value), 0D)

                If cantRecibida <> cantEnviada Then hayNovedad = True

                ' Acreditar al stock de esta bodega/sucursal
                If cheked AndAlso cantRecibida > 0 Then
                    ' ── NUEVO: garantizar que el producto existe en la BD local ──
                    ' Si es un producto nuevo que Matilde no tiene, se inserta
                    ' automáticamente con los datos de Supabase antes de acreditar stock.
                    AsegurarProductoExiste(idProd,
                        row.Cells("ColProducto").Value?.ToString())
                    AcreditarStockLocal(idProd, cantRecibida)
                End If

                detalleActualizado.Add(New With {
                    .idProducto       = idProd,
                    .nombre           = row.Cells("ColProducto").Value?.ToString(),
                    .cantidadEnviada  = cantEnviada,
                    .cantidadRecibida = cantRecibida,
                    .unidad           = row.Cells("ColUnidad").Value?.ToString()
                })
            Next

            ' 2. Actualizar estado en Supabase
            Dim estado As String = If(hayNovedad OrElse Not String.IsNullOrEmpty(novedad),
                                      "CON_NOVEDAD", "RECIBIDO")
            SupabaseHelper.ActualizarEstadoAsync(supabaseId, estado, novedad).Wait()

            ' 3. Registrar recepción en SQL local (trazabilidad)
            RegistrarRecepcionLocal(supabaseId, estado, novedad, detalleActualizado)

            MsgBox("Recepción confirmada. Estado: " & estado, MsgBoxStyle.Information, "Listo")
            CargarTransferenciasPendientes()

        Catch ex As Exception
            MsgBox("Error al procesar recepción: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub AcreditarStockLocal(idProducto As Integer, cantidad As Decimal)
        Dim sql As String =
            "IF EXISTS (SELECT 1 FROM Stock WHERE idProducto=@p AND idBodega=@b) " &
            "    UPDATE Stock SET stock = stock + @c WHERE idProducto=@p AND idBodega=@b " &
            "ELSE " &
            "    INSERT INTO Stock (idProducto, idBodega, stock) VALUES (@p, @b, @c)"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(sql,
                {"@p", "@b", "@c"},
                {idProducto, TerminalActivo.idBodega, cantidad})
        End Using
    End Sub

    ' ─── NUEVO: Sincronizar producto si no existe en la BD local de Matilde ──
    ' Se llama ANTES de AcreditarStockLocal para garantizar que el idProducto existe.
    ' Usa la tabla 'productos_sync' de Supabase para obtener los datos del producto.
    '
    ' Esquema real confirmado de la BD local:
    '   Productos   : idProducto, Nom_Comercial, Nom_Comun, Descripcion, Cant_minima,
    '                 idUnidad, IdSubCategoria, ivaPorcentaje, Facturable, Activo, idData
    '   ProductoPresentacion : idPresentacion, codProducto, idProducto, idProUndMed,
    '                          idProUndReferen, Cant_Present, precioCompra, precioVenta,
    '                          Empaquetado, Presentacion, PresentacionPrint, isPresentFactory
    '   ProdcutStock (tabla real): idProdcutStock, idProducto, idBodega, stock, pvpUND, Und, idProUndMed

    Private Function AsegurarProductoExiste(idProducto As Integer,
                                             nombreProducto As String) As Boolean
        ' 1. Verificar si ya existe en la BD local de Matilde
        Dim sqlCheck As String = "SELECT COUNT(1) FROM Productos WHERE idProducto = @id"
        Dim existe As Integer
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            existe = CInt(cmd.RetornaEscalarConParams(sqlCheck, {"@id"}, {idProducto}))
        End Using
        If existe > 0 Then Return True  ' ya existe, nada que hacer

        ' 2. No existe → buscar datos en Supabase (tabla productos_sync)
        Dim dtSync As DataTable = Nothing
        Try
            Dim sqlSync As String =
                "SELECT * FROM productos_sync " &
                "WHERE id_producto_orig = @id AND estado_sync = 'PENDIENTE' " &
                "LIMIT 1"
            Using conn = SupabaseDataAccess.SupabasePgConnection.OpenPoolConnection()
            Using cmd  = New Npgsql.NpgsqlCommand(sqlSync, conn)
                cmd.Parameters.AddWithValue("@id", idProducto)
                Dim adapter As New Npgsql.NpgsqlDataAdapter(cmd)
                dtSync = New DataTable()
                adapter.Fill(dtSync)
            End Using
            End Using
        Catch ex As Exception
            ' Sin conexión o sin datos de sincronización → insertar mínimo con lo que tenemos
        End Try

        ' 3. Insertar en Productos local
        Dim nom_comercial As String = nombreProducto
        Dim nom_comun     As String = nombreProducto
        Dim cant_minima   As Decimal = 1
        Dim id_unidad     As Integer = 1
        Dim id_subcateg   As Integer = 1
        Dim iva_porc      As Decimal = 0
        Dim facturable    As Integer = 1
        Dim cod_producto  As String  = "SN" & idProducto.ToString("D5")
        Dim precio_compra As Decimal = 0
        Dim precio_venta  As Decimal = 0
        Dim unidad_pres   As String  = "UN"

        If dtSync IsNot Nothing AndAlso dtSync.Rows.Count > 0 Then
            Dim r As DataRow = dtSync.Rows(0)
            nom_comercial = If(r("nom_comercial")?.ToString(), nombreProducto)
            nom_comun     = If(r("nom_comun")?.ToString(),     nombreProducto)
            cant_minima   = If(IsDBNull(r("cant_minima")),   1D, CDec(r("cant_minima")))
            id_unidad     = If(IsDBNull(r("id_unidad")),     1,  CInt(r("id_unidad")))
            id_subcateg   = If(IsDBNull(r("id_subcategoria")),1, CInt(r("id_subcategoria")))
            iva_porc      = If(IsDBNull(r("iva_porcentaje")), 0D, CDec(r("iva_porcentaje")))
            facturable    = If(CBool(r("facturable")), 1, 0)
            cod_producto  = If(r("cod_producto")?.ToString(), cod_producto)
            precio_compra = If(IsDBNull(r("precio_compra")), 0D, CDec(r("precio_compra")))
            precio_venta  = If(IsDBNull(r("precio_venta")),  0D, CDec(r("precio_venta")))
            unidad_pres   = If(r("unidad_present")?.ToString(), "UN")
        End If

        ' INSERT en Productos (SET IDENTITY_INSERT si idProducto es IDENTITY)
        Dim sqlIns As String =
            "SET IDENTITY_INSERT Productos ON; " &
            "INSERT INTO Productos (idProducto, Nom_Comercial, Nom_Comun, Cant_minima, " &
            "  idUnidad, IdSubCategoria, ivaPorcentaje, Facturable, Activo) " &
            "VALUES (@id, @nom_c, @nom_u, @cant, @unidad, @subcat, @iva, @fact, 1); " &
            "SET IDENTITY_INSERT Productos OFF;"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(sqlIns,
                {"@id",     "@nom_c",    "@nom_u",   "@cant",
                 "@unidad", "@subcat",   "@iva",     "@fact"},
                {idProducto, nom_comercial, nom_comun, cant_minima,
                 id_unidad,  id_subcateg,  iva_porc,  facturable})
        End Using

        ' INSERT en ProductoPresentacion
        Dim sqlPres As String =
            "INSERT INTO ProductoPresentacion " &
            "  (codProducto, idProducto, idProUndMed, idProUndReferen, " &
            "   Cant_Present, precioCompra, precioVenta, Empaquetado, " &
            "   Presentacion, PresentacionPrint, isPresentFactory) " &
            "VALUES (@cod, @idP, @und, @und, 1, @pc, @pv, 1, @pres, @presp, 1)"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(sqlPres,
                {"@cod",  "@idP",      "@und",     "@pc",
                 "@pv",   "@pres",     "@presp"},
                {cod_producto, idProducto, id_unidad, precio_compra,
                 precio_venta, unidad_pres, "[" & unidad_pres & "]"})
        End Using

        ' INSERT en ProdcutStock (nombre real de la tabla según BD confirmada)
        Dim sqlStock As String =
            "INSERT INTO ProdcutStock (idProducto, idBodega, stock, pvpUND, Und, idProUndMed) " &
            "VALUES (@idP, @idB, 0, @pvp, @und, @undM)"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(sqlStock,
                {"@idP", "@idB",                   "@pvp",       "@und",      "@undM"},
                {idProducto, TerminalActivo.idBodega, precio_venta, unidad_pres, id_unidad})
        End Using

        ' Marcar como APLICADO en Supabase para no repetir la sincronización
        If dtSync IsNot Nothing AndAlso dtSync.Rows.Count > 0 Then
            Try
                Using conn = SupabaseDataAccess.SupabasePgConnection.OpenPoolConnection()
                Using cmd2 = New Npgsql.NpgsqlCommand(
                    "UPDATE productos_sync SET estado_sync='APLICADO' WHERE id_producto_orig=@id",
                    conn)
                    cmd2.Parameters.AddWithValue("@id", idProducto)
                    cmd2.ExecuteNonQuery()
                End Using
                End Using
            Catch : End Try
        End If

        Return True  ' producto insertado correctamente
    End Function

    Private Sub RegistrarRecepcionLocal(supabaseId As String, estado As String,
                                        novedad As String, detalle As List(Of Object))
        ' Guardar en tabla local para historial y para que Inés María consulte novedades
        Dim sql As String =
            "UPDATE TransferenciaEncabezado SET EstadoEnvio=@est, Novedad=@nov, " &
            "FechaRecepcion=GETDATE() WHERE SupabaseId=@sid"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(sql,
                {"@est", "@nov", "@sid"},
                {estado, novedad, supabaseId})
        End Using
        ' Nota: si la BD de Matilde es independiente, insertar como nuevo registro
        ' con una columna "EsRecepcion=1" para distinguirlo del encabezado de envío.
    End Sub

End Class
```

---

## Parte 7 — Acceso al Menú desde el Panel Principal

### 7.1 Agregar ítem en el menú de navegación

En el formulario principal (buscar donde están los ítems de menú de módulos, ej. `FrmInicio` o `frmMain`):

```vb
' Agregar junto a los otros módulos de ventas/operaciones
Private Sub mnuRecibirTransferencia_Click(sender As Object, e As EventArgs) _
        Handles mnuRecibirTransferencia.Click
    Dim frm As New frmRecibirTransferencia()
    frm.MdiParent = Me
    frm.Show()
End Sub
```

> El ítem del menú "Recibir Transferencia" solo debe aparecer activo en equipos registrados en la sucursal Matilde. Se puede controlar con `TerminalActivo.idBodega`.

---

## Parte 8 — Adapters SQL faltantes

Los siguientes métodos se deben agregar en los TableAdapters correspondientes del proyecto `CADsisVenta` (en los archivos `.xsd` o mediante queries directas):

| Adapter | Método a agregar | Query |
|---------|-----------------|-------|
| `BodegasTableAdapter` | `GetBodegasByHostName(hostname)` | `SELECT b.* FROM Bodegas b INNER JOIN Terminal t ON t.idBodega = b.idBodega WHERE t.Dominio = @hostname` |
| `BodegasTableAdapter` | `GetAllBodegas()` | `SELECT idBodega, nom_bodega, EsSucursalRemota, CiudadSucursal FROM Bodegas ORDER BY nom_bodega` |

---

## Parte 9 — Resumen de Flujos

### Flujo A: Bodega → Sucursal Matilde (remota)

```
Usuario llena lista en frmVentas → Click "Transferir"
→ frmTransferencia: selecciona Bodega (origen) y Matilde (destino)
→ Alerta de confirmación → Usuario acepta
→ INSERT SQL local (encabezado + detalle)
→ sp_TransferenciaDescontarStock (descuenta de Bodega)
→ POST Supabase → guarda UUID
→ Imprime guía de remisión en ticket
→ Físicamente: se carga el vehículo y se envía a Matilde

[En Matilde]
→ Usuario abre "Recibir Transferencia"
→ GET Supabase → lista transferencias PENDIENTES para idBodega=Matilde
→ Revisa lista, pone cantidades reales recibidas, marca checks
→ Click "Aceptar productos llegados"
→ Acredita stock en SQL local de Matilde
→ PATCH Supabase: estado → RECIBIDO o CON_NOVEDAD + novedad

[En Inés María - gestión posterior]
→ Ver estado en Supabase o tabla local (campo SupabaseId)
→ Si CON_NOVEDAD → gestionar faltantes
```

### Flujo B: Bodega → Local Principal (misma red, sin Supabase)

```
Usuario llena lista → Click "Transferir"
→ frmTransferencia: selecciona Bodega (origen) y Local Principal (destino)
→ Alerta → acepta
→ INSERT SQL local
→ sp_TransferenciaDescontarStock (Bodega)
→ sp_TransferenciaAcreditarStockLocal (Local Principal) — ¡inmediato!
→ estado → RECIBIDO automáticamente
→ Imprime guía de remisión para control interno
```

---

## Parte 10 — Checklist de Implementación

- [ ] Ejecutar scripts SQL (tablas, procedures) en la BD local de Inés María
- [ ] Ejecutar script SQL de tabla `transferencias` en Supabase
- [ ] Agregar columnas `EsSucursalRemota`, `CiudadSucursal` en tabla `Bodegas`
- [ ] Marcar la bodega de Matilde como `EsSucursalRemota = 1` en la BD de Inés María
- [ ] Agregar `SupabaseUrl` y `SupabaseApiKey` en `App.config`
- [ ] Instalar `Newtonsoft.Json` si no está (`packages.config` — ya suele estar en el proyecto)
- [ ] Crear carpeta `ClaseView/Transferencia/`
- [ ] Crear `DetalleTransferenciaItem.vb` en `ClaseView/Models/`
- [ ] Crear `SupabaseHelper.cs` en `CADsisVenta/Helpers/`
- [ ] Crear `frmTransferencia.vb` (+ Designer + Resources)
- [ ] Crear `frmRecibirTransferencia.vb` (+ Designer + Resources)
- [ ] Modificar `frmVentas.vb` — agregar handler y método `BuildDetalleTransferencia`
- [ ] Modificar `frmVentas.Designer.vb` — cambiar Text/Tag del `pedidoButton`
- [ ] Agregar función `ImprimirGuiaRemision` en `PrintTickets.vb`
- [ ] Agregar ítem "Recibir Transferencia" en el menú principal
- [ ] Agregar métodos faltantes en `BodegasTableAdapter`
- [ ] Replicar los scripts SQL de las tablas nuevas en la BD local de Matilde
- [ ] Probar Flujo A completo (Bodega → Matilde con Supabase)
- [ ] Probar Flujo B completo (Bodega → Local, solo local)
- [ ] Probar recepción parcial (cantidades distintas + novedad)

---

## Notas Técnicas Adicionales

**Sobre el uso de `async/await`:** WinForms en .NET Framework no maneja bien `await` en eventos de UI sin `Async` en la firma. Se usa `.Result` en este documento como simplificación. Para producción, marcar los eventos con `Async Sub` y usar `Await` para evitar deadlocks en interfaces complejas.

**Sobre Supabase y conectividad:** Si Matilde no tiene internet en el momento de la recepción, el panel debe mostrar un mensaje claro y guardar localmente la intención de actualizar Supabase, reintentando cuando haya conexión. Esto puede implementarse con una tabla `ColaPendienteSupabase` local.

**Sobre la tabla `Stock`:** Este documento asume que existe una tabla `Stock` con columnas `idProducto`, `idBodega`, `stock`. Verificar el nombre real en el dataset (`DataSetStock.xsd`) antes de escribir las queries.

**Sobre el nombre exacto de columnas del DGV:** En `BuildDetalleTransferencia()`, ajustar `ColIdProducto`, `ColNombreProducto`, `ColCantidad`, `ColUnidad` con los nombres reales de las columnas del `DataGridView` en `frmVentas`.
