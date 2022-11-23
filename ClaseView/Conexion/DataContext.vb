Imports CADsisVenta
Namespace ClassView.Conexion
    Public Class DataContext
        Inherits DataClassesDBDataContext
        Sub New()
            MyBase.Connection.ConnectionString = SimpleDataApp.Utility.GetConnectionString()
        End Sub
    End Class
End Namespace

