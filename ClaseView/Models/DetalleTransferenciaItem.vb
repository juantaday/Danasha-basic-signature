Public Class DetalleTransferenciaItem
    ' — Identificación —
    Public Property idProducto As Integer
    Public Property codProducto As String
    Public Property NombreProducto As String
    Public Property NomComun As String

    ' — Clasificación (FK + nombre para fallback) —
    Public Property idUnidad As Integer
    Public Property nomUnidad As String
    Public Property idSubCategoria As Integer
    Public Property nomSubCategoria As String

    ' — Fiscal —
    Public Property ivaPorcentaje As Decimal
    Public Property Facturable As Boolean

    ' — Precios —
    Public Property PrecioCompra As Decimal
    Public Property PrecioVenta As Decimal
    Public Property PrecioTotal As Decimal

    ' — Presentación —
    Public Property Unidad As String        ' unidadPresent
    Public Property CantPresent As Decimal

    ' — Transferencia —
    Public Property CantidadEnviada As Decimal
    Public Property CantidadRecibida As Decimal?
End Class
