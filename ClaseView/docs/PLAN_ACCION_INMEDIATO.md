# 🚀 PLAN DE ACCIÓN INMEDIATO - Módulo Transferencia Stock

**Objetivo:** Completar la implementación en orden secuencial sin bloqueos  
**Tiempo estimado:** 2-3 horas

---

## FASE 1️⃣: BASE DE DATOS (10 minutos)
**Estado:** ⚠️ BLOQUEANTE - Debe ejecutarse primero

### Paso 1.1: Ejecutar Script SQL
1. Abre **SQL Server Management Studio**
2. Conecta a tu instancia local
3. Abre: `D:\Proyects\Danasha Basic Signature\ClaseView\docs\01_SCRIPTS_BASE_DATOS.sql`
4. Presiona **F5** para ejecutar todo el script
5. **Verifica:**
   - ✅ 0 errores
   - ✅ Columnas creadas en `Bodegas`
   - ✅ 2 tablas nuevas: `TransferenciaEncabezado`, `TransferenciaDetalle`
   - ✅ 2 procedures: `sp_TransferenciaDescontarStock`, `sp_TransferenciaAcreditarStockLocal`

**Comando de verificación rápida:**
```sql
-- Ejecuta esto para confirmar
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_NAME IN ('TransferenciaEncabezado', 'TransferenciaDetalle');

SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Bodegas' AND COLUMN_NAME IN ('EsSucursalRemota', 'CiudadSucursal');
```

✅ **Próximo paso:** Cuando SQL esté OK, continúa a la FASE 2

---

## FASE 2️⃣: CONFIGURACIÓN SUPABASE (15 minutos)
**Requisitos:** FASE 1 completada

### Paso 2.1: Tablas en Supabase
1. Ve a https://app.supabase.com → Tu proyecto
2. Abre **SQL Editor**
3. Copia y pega el siguiente script:

```sql
-- Tabla de transferencias
CREATE TABLE IF NOT EXISTS transferencias (
    id                  UUID          DEFAULT gen_random_uuid() PRIMARY KEY,
    num_transferencia   TEXT          NOT NULL,
    bodega_origen_id    INT           NOT NULL,
    bodega_origen_nom   TEXT          NOT NULL,
    bodega_destino_id   INT           NOT NULL,
    bodega_destino_nom  TEXT          NOT NULL,
    fecha_emision       TIMESTAMPTZ   NOT NULL DEFAULT now(),
    estado              TEXT          NOT NULL DEFAULT 'PENDIENTE',
    novedad             TEXT,
    fecha_recepcion     TIMESTAMPTZ,
    detalle             JSONB         NOT NULL
);

CREATE INDEX idx_transferencias_destino 
    ON transferencias(bodega_destino_id, estado);

-- Tabla de sincronización de productos
CREATE TABLE IF NOT EXISTS productos_sync (
    id               UUID        DEFAULT gen_random_uuid() PRIMARY KEY,
    id_producto_orig INT         NOT NULL,
    nom_comercial    TEXT        NOT NULL,
    nom_comun        TEXT,
    descripcion      TEXT,
    cant_minima      DECIMAL     DEFAULT 1,
    id_unidad        INT         DEFAULT 1,
    id_subcategoria  INT,
    iva_porcentaje   DECIMAL     DEFAULT 0,
    facturable       BOOLEAN     DEFAULT TRUE,
    cod_producto     TEXT,
    cant_present     DECIMAL     DEFAULT 1,
    precio_compra    DECIMAL     DEFAULT 0,
    precio_venta     DECIMAL     DEFAULT 0,
    unidad_present   TEXT,
    estado_sync      TEXT        DEFAULT 'PENDIENTE',
    fecha_creacion   TIMESTAMPTZ DEFAULT now()
);

CREATE INDEX idx_productos_sync_estado ON productos_sync(estado_sync);
```

4. Presiona **Run**
5. **Verifica:** Sin errores, 2 tablas creadas

### Paso 2.2: Obtener Credenciales Supabase
1. Abre **Supabase Dashboard** → **Settings** → **API**
2. Copia:
   - `Project URL` → guardar como `SupabaseUrl`
   - `anon public` key → guardar como `SupabaseApiKey`

### Paso 2.3: Actualizar App.config
Edita: `ClaseView\App.config`

```xml
<appSettings>
  <!-- Agregar estas líneas DENTRO de <appSettings> -->
  <add key="SupabaseUrl"    value="https://YOUR-PROJECT.supabase.co" />
  <add key="SupabaseApiKey" value="eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..." />
</appSettings>
```

✅ **Próximo paso:** FASE 3

---

## FASE 3️⃣: ADAPTERS SQL (5 minutos)
**Requisitos:** FASE 1 completada

Agregar extensión para `BodegasTableAdapter`.

Edita: `CADsisVenta\Extensions\BodegasTableAdapterExtensions.cs`

Asegúrate que contenga estos **dos métodos**:

```csharp
public static System.Data.DataTable GetBodegasByHostName(
    this CADsisVenta.DataSetSystemTableAdapters.BodegasTableAdapter adapter,
    string hostName)
{
    var dt = new System.Data.DataTable();
    var cmd = new System.Data.SqlClient.SqlCommand(
        "SELECT b.* FROM Bodegas b " +
        "INNER JOIN Terminal t ON t.idBodega = b.idBodega " +
        "WHERE t.Dominio = @hostname",
        new System.Data.SqlClient.SqlConnection(
            System.Configuration.ConfigurationManager.ConnectionStrings[0].ConnectionString));
    cmd.Parameters.AddWithValue("@hostname", hostName);
    var adapter2 = new System.Data.SqlClient.SqlDataAdapter(cmd);
    adapter2.Fill(dt);
    return dt;
}

public static System.Data.DataTable GetAllBodegas(
    this CADsisVenta.DataSetSystemTableAdapters.BodegasTableAdapter adapter)
{
    var dt = new System.Data.DataTable();
    var cmd = new System.Data.SqlClient.SqlCommand(
        "SELECT idBodega, nom_bodega, EsSucursalRemota, CiudadSucursal FROM Bodegas ORDER BY nom_bodega",
        new System.Data.SqlClient.SqlConnection(
            System.Configuration.ConfigurationManager.ConnectionStrings[0].ConnectionString));
    var adapter2 = new System.Data.SqlClient.SqlDataAdapter(cmd);
    adapter2.Fill(dt);
    return dt;
}
```

✅ **Próximo paso:** FASE 4

---

## FASE 4️⃣: INTEGRACIÓN EN UI (20 minutos)
**Requisitos:** FASE 3 completada, Proyecto compila sin errores

### Paso 4.1: Marcar Bodega Matilde como Remota
En SQL Server, ejecuta:
```sql
-- Reemplaza 'SUCURSAL_MATILDE' con el nom_bodega real
UPDATE Bodegas 
SET EsSucursalRemota = 1, CiudadSucursal = 'Matilde' 
WHERE nom_bodega = 'SUCURSAL_MATILDE';  -- Ajusta según tu BD
```

### Paso 4.2: Modificar `frmVentas.vb`
Edita: `ClaseView\Ventas\frmVentas.vb`

Busca el evento `pedidoButton_Click` y asegúrate que el código sea:

```vb
Private Sub pedidoButton_Click(sender As Object, e As EventArgs) _
        Handles pedidoButton.Click
    Try
        If DataGridView1.Rows.Count = 0 Then
            MsgBox("Agregue productos antes de crear una transferencia.", _
                   MsgBoxStyle.Exclamation, "Sin productos")
            Exit Sub
        End If

        Using frm As New frmTransferencia(BuildDetalleTransferencia())
            frm.ShowDialog(Me)
        End Using

    Catch ex As Exception
        MsgBox(ex.Message & " en pedidoButton_Click", MsgBoxStyle.Critical, "Error")
    End Try
End Sub

Private Function BuildDetalleTransferencia() As List(Of DetalleTransferenciaItem)
    Dim lista As New List(Of DetalleTransferenciaItem)
    For Each row As DataGridViewRow In DataGridView1.Rows
        If row.IsNewRow Then Continue For
        ' Ajusta los nombres de columnas según tu DGV real
        Dim idProd = row.Cells("ColIdProducto")?.Value
        Dim nombre = row.Cells("ColNombreProducto")?.Value
        Dim cantidad = row.Cells("ColCantidad")?.Value
        Dim unidad = row.Cells("ColUnidad")?.Value
        
        If idProd IsNot Nothing Then
            lista.Add(New DetalleTransferenciaItem With {
                .idProducto      = CInt(idProd),
                .NombreProducto  = nombre?.ToString(),
                .Cantidad        = CDec(cantidad),
                .Unidad          = unidad?.ToString()
            })
        End If
    Next
    Return lista
End Function
```

### Paso 4.3: Modificar `frmVentas.Designer.vb`
Edita: `ClaseView\Ventas\frmVentas.Designer.vb`

Busca el bloque de inicialización de `pedidoButton` y cámbialo:

```vb
' Cambiar Text y Tag SOLAMENTE
Me.pedidoButton.Text  = "Transferir"
Me.pedidoButton.Tag   = "Transferencia"
' Mantener el resto igual
```

✅ **Próximo paso:** FASE 5

---

## FASE 5️⃣: COMPILACIÓN Y PRUEBA (15 minutos)
**Requisitos:** FASES 1-4 completadas

### Paso 5.1: Compilar Solución
1. Visual Studio → **Build** → **Rebuild Solution**
2. **Verifica:** 0 errores, solo warnings (si hay)
3. Si hay errores:
   - Revisa referencias faltantes
   - Asegúrate que `SupabaseDataAccess` está referenciado en `ClaseView` y `CADsisVenta`
   - Comprueba que `App.config` tiene las credenciales Supabase

### Paso 5.2: Prueba Rápida de Conexión
1. Abre el formulario principal
2. Navega a Ventas
3. Agrega algunos productos al listado
4. Haz clic en botón **"Transferir"**
5. **Verifica:**
   - ✅ Se abre `frmTransferencia`
   - ✅ Los combos se llenan (origen y destino)
   - ✅ La lista de productos aparece

### Paso 5.3: Prueba de Transferencia Local (Bodega → Local)
1. En `frmTransferencia`:
   - Origen: tu bodega actual
   - Destino: otra bodega en la misma red (NO remota)
2. Haz clic **"Confirmar Envío"**
3. **Verifica:**
   - ✅ Se genera número TRF-YYYYMMDD-NNNN
   - ✅ Stock se descuenta en origen
   - ✅ Stock se acredita en destino inmediatamente
   - ✅ Estado pasa a "RECIBIDO"
   - ✅ Se imprime guía de remisión (si tienes impresora configurada)

### Paso 5.4: Prueba de Transferencia Remota (→ Matilde)
1. En `frmTransferencia`:
   - Origen: tu bodega actual
   - Destino: SUCURSAL_MATILDE (remota)
2. Haz clic **"Confirmar Envío"**
3. **Verifica:**
   - ✅ Se sube a Supabase (revisa en Dashboard)
   - ✅ Estado pasa a "ENVIADO"
   - ✅ Se imprime guía de remisión

✅ **Próximo paso:** FASE 6

---

## FASE 6️⃣: RECEPCIÓN (Sucursal Matilde)
**Requisitos:** FASE 5 completada, Matilde tiene su propia BD local

### Paso 6.1: En Matilde - Agregar menú
En `ClaseView\Inicio\MDIPareInicio.vb` o donde esté el menú principal:

```vb
Private Sub mnuRecibirTransferencia_Click(sender As Object, e As EventArgs) _
        Handles mnuRecibirTransferencia.Click
    Dim frm As New frmRecibirTransferencia()
    ' Si es MDI:
    frm.MdiParent = Me
    frm.Show()
End Sub
```

### Paso 6.2: Prueba de Recepción
1. En Matilde, abre **"Recibir Transferencia"**
2. Haz clic **"Actualizar"**
3. **Verifica:**
   - ✅ Se listan las transferencias pendientes de Supabase
   - ✅ Aparece la transferencia que enviaste en la FASE 5.4
4. Selecciona la transferencia
5. **Verifica:**
   - ✅ Se carga el detalle
   - ✅ Puedes editar cantidades recibidas
6. Haz clic **"Aceptar productos llegados"**
7. **Verifica:**
   - ✅ Stock se acredita en Matilde
   - ✅ Estado cambia a "RECIBIDO" en Supabase
   - ✅ Productos nuevos (si hay) se insertan en BD local de Matilde

---

## 📋 CHECKLIST FINAL

```
FASE 1: BASE DE DATOS
 [ ] Script 01_SCRIPTS_BASE_DATOS.sql ejecutado sin errores
 [ ] Tablas TransferenciaEncabezado y TransferenciaDetalle creadas
 [ ] Columnas EsSucursalRemota y CiudadSucursal en Bodegas
 [ ] Procedures creados

FASE 2: SUPABASE
 [ ] Tablas transferencias y productos_sync creadas en Supabase
 [ ] Credenciales Supabase en App.config
 [ ] Conexión a Supabase probada

FASE 3: ADAPTERS
 [ ] BodegasTableAdapterExtensions contiene 2 métodos
 [ ] Compile sin errores

FASE 4: UI
 [ ] frmVentas tiene evento pedidoButton_Click
 [ ] BuildDetalleTransferencia implementado
 [ ] Bodega Matilde marcada como remota en BD
 [ ] frmTransferencia abre al hacer click en "Transferir"

FASE 5: PRUEBA LOCAL
 [ ] Transferencia local funciona (Bodega → Local)
 [ ] Transferencia remota sube a Supabase (Bodega → Matilde)
 [ ] Stock se actualiza correctamente

FASE 6: RECEPCIÓN
 [ ] Menú "Recibir Transferencia" en Matilde
 [ ] Recepción funciona (descarga de Supabase)
 [ ] Stock se acredita en Matilde
 [ ] Productos nuevos se sincronizan
```

---

## 🆘 SOLUCIÓN DE PROBLEMAS RÁPIDA

| Problema | Solución |
|----------|----------|
| "Tabla X no existe" | Ejecutar 01_SCRIPTS_BASE_DATOS.sql |
| "SupabaseUrl not found" | Verificar App.config con credenciales Supabase |
| "frmTransferencia no abre" | Asegurarse que `ClaseView` y `CADsisVenta` compilaron |
| "No se descuenta stock" | Verificar que tabla `Stock` existe y tiene las columnas requeridas |
| "Supabase timeout" | Revisar conexión a internet, credenciales |
| "Productos no se sincronizan" | Ejecutar tabla `productos_sync` en Supabase |

---

## 📞 Referencias Rápidas

- Script SQL: `ClaseView/docs/01_SCRIPTS_BASE_DATOS.sql`
- Estado: `ClaseView/docs/ESTADO_IMPLEMENTACION.md`
- Guía completa: `ClaseView/docs/GUIA_IMPLEMENTACION_TRANSFERENCIA_STOCK.md`

