' ── Modelo de fila para ObjectListView ──────────────────────────────────────────
Imports Newtonsoft.Json.Linq
Imports SupabaseDataAccess.Models

Public Class DetalleTransfItem

    Public Property Seleccionado As Boolean = True
    Public Property Producto As String
    Public Property CantEnviada As Decimal
    Public Property CantRecibida As Decimal
    Public Property Unidad As String
    Public Property EstadoItem As String    ' "✦ NUEVO" | "En stock"
    Public Property EsNuevo As Boolean
    Public Property IdProducto As Integer

    ' ── Nuevos ──────────────────────────────────────────
    Public Property IdMotivo As Integer?        ' NULL = sin discrepancia
    Public Property DescMotivo As String        ' para mostrar en OLV
    Public ReadOnly Property TieneDiscrepancia As Boolean
        Get
            Return Seleccionado AndAlso CantRecibida < CantEnviada OrElse
                   Not Seleccionado
        End Get
    End Property

End Class
' ── Modelo para transferencias pendientes en ListBox ────────────────────────────
Public Class TransferenciaItem

    Public Property Display As String

    Public Property Json As Transferencia

    Public Overrides Function ToString() As String
        Return Display
    End Function

End Class
