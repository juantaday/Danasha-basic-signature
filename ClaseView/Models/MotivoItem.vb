Public Class MotivoItem
    Public Property Id As Integer
    Public Property Descripcion As String
    Public Overrides Function ToString() As String
        Return Descripcion
    End Function
End Class
