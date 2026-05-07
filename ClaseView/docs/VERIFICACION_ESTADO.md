# VERIFICACIÓN AUTOMÁTICA DE ESTADO

Este archivo contiene verificaciones que puedes ejecutar para saber exactamente qué falta.

## 1️⃣ VERIFICACIÓN SQL SERVER

Ejecuta este SQL en tu instancia local:

```sql
-- ============================================================================
-- VERIFICACIÓN DE BASE DE DATOS
-- ============================================================================

PRINT '============ VERIFICACIÓN DE ESTRUCTURAS SQL ============';
PRINT '';

-- 1. Verificar columnas en Bodegas
PRINT 'PASO 1: Columnas en tabla Bodegas';
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
           WHERE TABLE_NAME = 'Bodegas' AND COLUMN_NAME = 'EsSucursalRemota')
    PRINT '  ✓ Columna EsSucursalRemota existe'
ELSE
    PRINT '  ✗ FALTA: Columna EsSucursalRemota'

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
           WHERE TABLE_NAME = 'Bodegas' AND COLUMN_NAME = 'CiudadSucursal')
    PRINT '  ✓ Columna CiudadSucursal existe'
ELSE
    PRINT '  ✗ FALTA: Columna CiudadSucursal'

PRINT ''

-- 2. Verificar tabla TransferenciaEncabezado
PRINT 'PASO 2: Tabla TransferenciaEncabezado';
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TransferenciaEncabezado')
    PRINT '  ✓ Tabla existe'
ELSE
    PRINT '  ✗ FALTA: Tabla TransferenciaEncabezado'

PRINT ''

-- 3. Verificar tabla TransferenciaDetalle
PRINT 'PASO 3: Tabla TransferenciaDetalle';
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TransferenciaDetalle')
    PRINT '  ✓ Tabla existe'
ELSE
    PRINT '  ✗ FALTA: Tabla TransferenciaDetalle'

PRINT ''

-- 4. Verificar procedures
PRINT 'PASO 4: Stored Procedures';
IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'sp_TransferenciaDescontarStock')
    PRINT '  ✓ sp_TransferenciaDescontarStock existe'
ELSE
    PRINT '  ✗ FALTA: sp_TransferenciaDescontarStock'

IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'sp_TransferenciaAcreditarStockLocal')
    PRINT '  ✓ sp_TransferenciaAcreditarStockLocal existe'
ELSE
    PRINT '  ✗ FALTA: sp_TransferenciaAcreditarStockLocal'

PRINT ''
PRINT '============ FIN VERIFICACIÓN ============';
```

---

## 2️⃣ VERIFICACIÓN APP.CONFIG

Abre: `ClaseView\App.config`

Busca la sección `<appSettings>` y verifica que existan:

```xml
<appSettings>
    <!-- Existentes: -->
    <add key="..." value="..." />
    
    <!-- DEBERÍAN EXISTIR: -->
    <add key="SupabaseUrl"    value="https://..." />
    <add key="SupabaseApiKey" value="eyJ..." />
</appSettings>
```

✅ Si ves las dos líneas de Supabase: **OK**  
❌ Si NO estan: **FALTA - Agregar ahora**

---

## 3️⃣ VERIFICACIÓN ARCHIVOS DEL PROYECTO

Asegúrate que estos archivos existen en Visual Studio:

```
✓ ClaseView\Models\DetalleTransferenciaItem.vb
✓ ClaseView\Transferencia\frmTransferencia.vb
✓ ClaseView\Transferencia\frmRecibirTransferencia.vb
✓ CADsisVenta\Helpers\SupabaseHelper.cs
✓ CADsisVenta\Extensions\BodegasTableAdapterExtensions.cs
✓ SupabaseDataAccess\SupabasePgConnection.cs
✓ SupabaseDataAccess\TransferenciaRepository.cs

? ClaseView\Ventas\frmVentas.vb - Debe tener:
  - Evento pedidoButton_Click
  - Método BuildDetalleTransferencia()

? ClaseView\Ventas\frmVentas.Designer.vb - Debe tener:
  - pedidoButton.Text = "Transferir"
  - pedidoButton.Tag = "Transferencia"

? ClaseView\Modulos\PrintTickets.vb - Debe tener:
  - Función ImprimirGuiaRemision() [SI AÚN NO EXISTE, AGREGAR]

? ClaseView\Inicio\MDIPareInicio.vb - Debe tener:
  - Evento mnuRecibirTransferencia_Click [SI NO EXISTE, AGREGAR]
```

---

## 4️⃣ VERIFICACIÓN COMPILACIÓN

Abre Visual Studio y ejecuta:

1. **Build → Rebuild Solution**
2. Mira la ventana **Output** (Ctrl+Alt+O)
3. **Busca:**
   - ✓ "Build succeeded" = ✅ OK
   - ✗ "Errores encontrados" = ❌ REVISAR ERRORES

**Errores comunes:**
- "frmTransferencia no existe" → Falta crear en `ClaseView\Transferencia\`
- "SupabaseDataAccess no referenciado" → Agregar referencia al proyecto
- "DetalleTransferenciaItem no encontrado" → Verificar ubicación y namespace

---

## 5️⃣ VERIFICACIÓN SUPABASE

Ve a: https://app.supabase.com → Tu proyecto

En **SQL Editor**, ejecuta:

```sql
-- Verificar tabla transferencias
SELECT COUNT(*) as total FROM information_schema.tables 
WHERE table_name = 'transferencias';

-- Verificar tabla productos_sync
SELECT COUNT(*) as total FROM information_schema.tables 
WHERE table_name = 'productos_sync';

-- Si ambas devuelven 1 = ✓ OK
-- Si devuelven 0 = ✗ FALTA crear tablas
```

---

## 6️⃣ CHECKLIST FINAL

```
BASE DE DATOS SQL SERVER:
[ ] Columnas EsSucursalRemota y CiudadSucursal en Bodegas
[ ] Tabla TransferenciaEncabezado creada
[ ] Tabla TransferenciaDetalle creada
[ ] Procedure sp_TransferenciaDescontarStock creado
[ ] Procedure sp_TransferenciaAcreditarStockLocal creado

SUPABASE:
[ ] Tabla transferencias creada
[ ] Tabla productos_sync creada
[ ] Credenciales en App.config

PROYECTO VISUAL STUDIO:
[ ] Todos los archivos .vb y .cs existen
[ ] Proyecto compila sin errores
[ ] Referencias correctas a SupabaseDataAccess

INTEGRACIONES:
[ ] frmVentas.vb tiene pedidoButton_Click y BuildDetalleTransferencia()
[ ] frmVentas.Designer.vb tiene Text="Transferir", Tag="Transferencia"
[ ] PrintTickets.vb tiene ImprimirGuiaRemision()
[ ] MDIPareInicio.vb tiene mnuRecibirTransferencia_Click()
```

---

## 🎯 PRÓXIMO PASO

Cuando hayas completado todas las verificaciones:
1. Ejecuta el script SQL de verificación (paso 1)
2. Revisa los resultados
3. Completa lo que falte según los resultados
4. Vuelve a verificar

**¿Necesitas ayuda?** Abre un issue en GitHub con los resultados de estas verificaciones.

