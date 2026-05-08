' ── Modelo de fila para ObjectListView ──────────────────────────────────────────
Imports Newtonsoft.Json.Linq
Imports SupabaseDataAccess.Models

Public Class DetalleTransfItem

    Public Property Seleccionado As Boolean = True

    Public Property Producto As String

    Public Property CantEnviada As Decimal

    Public Property CantRecibida As Decimal

    Public Property Unidad As String

    ' "✦ NUEVO" | "En stock"
    Public Property EstadoItem As String

    Public Property EsNuevo As Boolean

    Public Property IdProducto As Integer

End Class


' ── Modelo para transferencias pendientes en ListBox ────────────────────────────
Public Class TransferenciaItem

    Public Property Display As String

    Public Property Json As Transferencia

    Public Overrides Function ToString() As String
        Return Display
    End Function

End Class
