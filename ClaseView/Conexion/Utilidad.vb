Imports System.Configuration
Imports DomainSQLite.Setting
Imports CADsisVenta.GetConnectionStringCadSysytem
Imports DomainSQLite.Models

Namespace SimpleDataApp
    Public Class Utility
        Friend Shared Function GetConnectionString() As String

            Return DomainSQLite.Setting.Configuration.ConectionString

            ' Util-2 Assume failure.
            Dim returnValue As String = Nothing
            ' Util-3 Look for the name in the connectionStrings section.
            Dim settings As ConnectionStringSettings = ConfigurationManager.ConnectionStrings("JsofConneccionString")
            ' Dim settings As ConnectionStringSettings = DefaultStringSCadSysytem
            ' If found, return the connection string.
            If settings IsNot Nothing Then
                returnValue = settings.ConnectionString

            End If

        End Function
    End Class
End Namespace