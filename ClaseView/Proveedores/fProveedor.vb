Imports System.Data.SqlClient

Public Class fProveedor

    Public Function mostrar() As DataTable

        Try


            sql = "SELECT idProveedor, Ruc_Ci, Razon_social, Represent, Telefono , ivaSubTotal FROM Proveedores"

            Using cnn = New SqlConnection(DomainSQLite.Setting.Configuration.ConectionString)
                cnn.Open()
                Dim cmd As New SqlCommand(sql)
                cmd.CommandType = CommandType.Text
                cmd.Connection = cnn


                Dim dt As New DataTable
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(dt)

                If dt.Rows.Count <> 0 Then
                    Return dt
                Else
                    Return Nothing

                End If
            End Using




        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        Finally

        End Try

    End Function

    Public Function Tipo_Proveedor() As DataTable


        Try


            sql = "SELECT * FROM ProveedorTypo"

            Using cnn = New SqlConnection(DomainSQLite.Setting.Configuration.ConectionString)
                cnn.Open()

                Dim cmd As New SqlCommand(sql)
                cmd.CommandType = CommandType.Text
                cmd.Connection = cnn


                Dim dt As New DataTable
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(dt)

                If dt.Rows.Count <> 0 Then
                    Return dt
                Else
                    Return Nothing

                End If
            End Using



        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        Finally

        End Try

    End Function

End Class

