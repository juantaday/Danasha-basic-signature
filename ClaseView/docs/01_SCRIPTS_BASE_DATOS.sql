-- ============================================================================
-- GUÍA DE IMPLEMENTACIÓN: MÓDULO DE TRANSFERENCIA ENTRE LOCALES/BODEGAS
-- ============================================================================

-- ============================================================================
-- PASO 1.3: Agregar columnas a Bodegas SOLO si no existen
-- ============================================================================
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'Bodegas' AND COLUMN_NAME = 'EsSucursalRemota'
)
BEGIN
    ALTER TABLE Bodegas ADD EsSucursalRemota BIT NOT NULL DEFAULT 0;
    PRINT 'Columna EsSucursalRemota agregada.';
END
ELSE
    PRINT 'Columna EsSucursalRemota ya existe, se omite.';

IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'Bodegas' AND COLUMN_NAME = 'CiudadSucursal'
)
BEGIN
    ALTER TABLE Bodegas ADD CiudadSucursal VARCHAR(100) NULL;
    PRINT 'Columna CiudadSucursal agregada.';
END
ELSE
    PRINT 'Columna CiudadSucursal ya existe, se omite.';

-- ============================================================================
-- PASO 1.1: Nueva tabla TransferenciaEncabezado
-- ============================================================================
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TransferenciaEncabezado')
BEGIN
    CREATE TABLE [dbo].[TransferenciaEncabezado] (
        idTransferencia  INT           IDENTITY(1,1) PRIMARY KEY,
        NumTransferencia VARCHAR(20)   NOT NULL,        -- ej: TRF-20250610-0001
        idBodegaOrigen   INT           NOT NULL REFERENCES Bodegas(idBodega),
        idBodegaDestino  INT           NOT NULL REFERENCES Bodegas(idBodega),
        FechaEmision     DATETIME      NOT NULL DEFAULT GETDATE(),
        idUsuario        INT           NOT NULL,
        EstadoEnvio      VARCHAR(20)   NOT NULL DEFAULT 'PENDIENTE',
        -- PENDIENTE | ENVIADO | RECIBIDO | CON_NOVEDAD
        SupabaseId       VARCHAR(50)   NULL,            -- UUID devuelto por Supabase
        Novedad          NVARCHAR(500) NULL,
        FechaRecepcion   DATETIME      NULL
    );

    CREATE INDEX idx_TransferenciaEncabezado_Numero    ON TransferenciaEncabezado(NumTransferencia);
    CREATE INDEX idx_TransferenciaEncabezado_SupabaseId ON TransferenciaEncabezado(SupabaseId);
    CREATE INDEX idx_TransferenciaEncabezado_Estado    ON TransferenciaEncabezado(EstadoEnvio);

    PRINT 'Tabla TransferenciaEncabezado creada exitosamente.';
END
ELSE
    PRINT 'Tabla TransferenciaEncabezado ya existe, se omite.';

-- ============================================================================
-- PASO 1.2: Nueva tabla TransferenciaDetalle
-- ============================================================================
IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TransferenciaDetalle')
BEGIN
    CREATE TABLE [dbo].[TransferenciaDetalle] (
        idDetalle       INT           IDENTITY(1,1) PRIMARY KEY,
        idTransferencia INT           NOT NULL REFERENCES TransferenciaEncabezado(idTransferencia),
        idProducto      INT           NOT NULL,
        CantidadEnviada DECIMAL(18,4) NOT NULL,
        CantidadRecibida DECIMAL(18,4) NULL,  -- NULL hasta que la sucursal confirme
        Unidad          VARCHAR(30)   NULL
    );

    CREATE INDEX idx_TransferenciaDetalle_Transferencia ON TransferenciaDetalle(idTransferencia);
    CREATE INDEX idx_TransferenciaDetalle_Producto      ON TransferenciaDetalle(idProducto);

    PRINT 'Tabla TransferenciaDetalle creada exitosamente.';
END
ELSE
    PRINT 'Tabla TransferenciaDetalle ya existe, se omite.';

-- ============================================================================
-- PASO 1.4: Procedure sp_TransferenciaDescontarStock
-- ============================================================================
GO
CREATE OR ALTER PROCEDURE sp_TransferenciaDescontarStock
    @idTransferencia INT
AS
BEGIN
    BEGIN TRY
        UPDATE s
        SET    s.stock = s.stock - d.CantidadEnviada
        FROM   ProductosStock s
        INNER JOIN TransferenciaDetalle  d ON s.idProducto = d.idProducto
        INNER JOIN TransferenciaEncabezado e ON d.idTransferencia = e.idTransferencia
        WHERE  e.idTransferencia = @idTransferencia
          AND  s.idBodega        = e.idBodegaOrigen;

        PRINT 'Stock descontado exitosamente de bodega origen.';
    END TRY
    BEGIN CATCH
        PRINT 'Error al descontar stock: ' + ERROR_MESSAGE();
        THROW;
    END CATCH
END;
GO

-- ============================================================================
-- PASO 1.5: Procedure sp_TransferenciaAcreditarStockLocal
-- ============================================================================
CREATE OR ALTER PROCEDURE sp_TransferenciaAcreditarStockLocal
    @idTransferencia INT
AS
BEGIN
    BEGIN TRY
        MERGE ProductosStock AS target          -- ← nombre corregido (era ProductStock)
        USING (
            SELECT d.idProducto,
                   d.CantidadEnviada AS CantidadRecibida,
                   e.idBodegaDestino
            FROM   TransferenciaDetalle d
            INNER JOIN TransferenciaEncabezado e ON d.idTransferencia = e.idTransferencia
            WHERE  e.idTransferencia = @idTransferencia
        ) AS source
            ON  target.idProducto = source.idProducto
            AND target.idBodega   = source.idBodegaDestino
        WHEN MATCHED THEN
            UPDATE SET target.stock = target.stock + source.CantidadRecibida
        WHEN NOT MATCHED THEN
            INSERT (idProducto, idBodega, stock)
            VALUES (source.idProducto, source.idBodegaDestino, source.CantidadRecibida);

        PRINT 'Stock acreditado exitosamente en bodega destino.';
    END TRY
    BEGIN CATCH
        PRINT 'Error al acreditar stock: ' + ERROR_MESSAGE();
        THROW;
    END CATCH
END;
GO

-- ============================================================================
-- VERIFICACIÓN FINAL
-- ============================================================================
PRINT '';
PRINT '========== VERIFICACIÓN DE ESTRUCTURAS ==========';

SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_NAME IN ('TransferenciaEncabezado','TransferenciaDetalle')
ORDER BY TABLE_NAME;

SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Bodegas'
  AND COLUMN_NAME IN ('EsSucursalRemota','CiudadSucursal')
ORDER BY COLUMN_NAME;

SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES
WHERE ROUTINE_NAME LIKE 'sp_Transferencia%'
ORDER BY ROUTINE_NAME;

PRINT '========== ¡LISTO! ==========';