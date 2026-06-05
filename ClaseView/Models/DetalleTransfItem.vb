' ── Modelo de fila para ObjectListView ──────────────────────────────────────────
Public Class DetalleTransfItem

    ' — UI / recepción —
    Public Property Seleccionado As Boolean = True
    Public Property EstadoItem As String        ' "✦ NUEVO" | "En stock"
    Public Property IdMotivo As Integer?        ' NULL = sin discrepancia
    Public Property DescMotivo As String        ' para mostrar en OLV

    ' — Identificación —
    Public Property IdProducto As Integer
    Public Property Producto As String          ' Nom_Comercial
    Public Property NomComun As String
    Public Property CodProducto As String

    ' — Clasificación —
    Public Property IdUnidad As Integer
    Public Property IdSubCategoria As Integer

    ' — Fiscal —
    Public Property IvaPorcentaje As Decimal
    Public Property Facturable As Boolean

    ' — Precios —
    Public Property PrecioCompra As Decimal
    Public Property PrecioVenta As Decimal
    Public Property PrecioTotal As Decimal

    ' — Presentación —
    Public Property Unidad As String            ' unidadPresent
    Public Property CantPresent As Decimal

    ' — Cantidades transferencia —
    Public Property CantEnviada As Decimal
    Public Property CantRecibida As Decimal
    Public Property EsNuevo As Boolean

    ' — Computed —
    Public ReadOnly Property TieneDiscrepancia As Boolean
        Get
            Return (Seleccionado AndAlso CantRecibida < CantEnviada) OrElse Not Seleccionado
        End Get
    End Property

    Public Property Deft_idPresenCompra As Integer = 1

    Public Property Deft_idPresenVenta As Integer = 1

End Class