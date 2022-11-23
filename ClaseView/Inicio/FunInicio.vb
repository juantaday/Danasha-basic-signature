Imports System.Data.SqlClient
Imports System.Net
Imports System.IO
Imports CADsisVenta.DataSetSystemTableAdapters
Imports CADsisVenta.ClsSystem
Imports CADsisVenta
Imports CADsisVenta.Class
Imports CADsisVenta.Helpers
Imports CADsisVenta.Helpers.FInicio
Imports DanashaBasic.ClassView.Conexion
Imports CADsisVenta.Statics

Public Class FunInicio
    Implements IDisposable
    Protected Friend connecction As New SqlConnection(SimpleDataApp.Utility.GetConnectionString())
    Sub New()
        Try
            connecction.Open()
            FInicio.UsuarioActivo.DataSource = connecction.DataSource.ToString()
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & " Intentando conectar al " & vbNewLine & connecction.DataSource.ToString(), MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Public Function IniciarUsuario(ByVal data As GetInicio) As Boolean
        Try
            Dim cmd = New SqlCommand("ValidaUsuario", connecction)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@Login", SqlDbType.Char, 8)
            cmd.Parameters.Add("@Password", SqlDbType.VarChar, 40)
            cmd.Parameters("@Login").Value = data.GUsuario
            cmd.Parameters("@Password").Value = data.gContrasena

            Dim dt As New DataTable
            If cmd.ExecuteNonQuery Then
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(dt)
                If dt.Rows.Count > 0 Then
                    If Not (dt.Rows(0)("Estado")) Then
                        sql = "Es usuario está inhabilitado, consulte a su administrador."
                        MsgBox(sql, MsgBoxStyle.Exclamation, "Al Iniciar Secion")
                        Return False
                    End If
                    With FInicio.UsuarioActivo
                        .Apellido = dt.Rows(0)("Apellidos")
                        .Nombre = dt.Rows(0)("Nombre")
                        .codUser = dt.Rows(0)("Login")
                        .IdUsuario = dt.Rows(0)("idPersona")
                    End With
                    Return True ' InitialityUser(data)
                Else
                    MsgBox("Usuario y/o contraseña incorrecto.", MsgBoxStyle.Exclamation, "Al Iniciar Secion")
                    Return False
                End If
            End If
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Al validar Usuario")
            Return False
        End Try
    End Function
    Public Function PruevaSecion(ByVal data As GetInicio) As Response
        Dim response As New Response()
        Try
            Dim cmd = New SqlCommand("ValidaUsuario")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Connection = Cnn_sql
            cmd.Parameters.AddWithValue("@Login", data.GUsuario)
            cmd.Parameters.AddWithValue("@Password", data.gContrasena)


            Dim dt As New DataTable
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                response.Success = True
                Return response
            Else
                response.Messague = "Contraseña actual Incorrecta"
                response.Success = False
                Return response
            End If

            response.Messague = "Contraseña actual Incorrecta"
            response.Success = False
            Return response
        Catch ex As Exception
            response.Messague = ex.Message
            response.Success = False
            Return response
        End Try
    End Function

    Private Sub InitialityUser(ByVal data As GetInicio)
        Try
            If Not (Me.connecction.State = ConnectionState.Open) Then
                Return
            End If
            sql = "Update Usuarios set [Iniciado] = 1 "
            sql = sql + "Where [Usuarios].[Login] = @CodUser and [Iniciado] = 0 "
            Using cmd = New SqlCommand(sql, Me.connecction)
                cmd.CommandType = CommandType.Text
                cmd.Parameters.AddWithValue("@CodUser", data.GUsuario)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            Return
        End Try
    End Sub
    Public Function CierraSecion(ByVal LoGinUser As String) As Boolean
        Try
            If Not (connecction.State = ConnectionState.Open) Then
                Return False
            End If
            sql = "Update Usuarios set [Iniciado] = 0 "
            sql = sql + "Where [Usuarios].[Login] = '" & LoGinUser & "' "
            Using cmd = New SqlCommand(sql, connecction)
                cmd.CommandType = CommandType.Text
                If cmd.ExecuteNonQuery() Then
                    FInicio.UsuarioActivo = Nothing
                    Return True
                End If
            End Using
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function
    Public Function UserAccesoResponse(ByVal data As GetInicio, setValueFilter As String) As Boolean
        Try
            If Not (connecction.State = ConnectionState.Open) Then
                Return False
            End If
            Using cmd = New SqlCommand("getUserSpecial", connecction)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@CodUser", data.GUsuario)
                cmd.Parameters.AddWithValue("@SetValue", String.Format("%{0}%", setValueFilter))

                Dim dt As New DataTable
                Dim da As New SqlDataAdapter(cmd)
                da.Fill(dt)
                If dt.Rows.Count Then
                    Return True
                Else
                    MsgBox("No tiene permiso para esta opción", MsgBoxStyle.Critical, "Al Valida Usuario")
                    Return False
                End If
            End Using
            Return False
        Catch ex As Exception
            MsgBox(ex.Message + " en le UserAccesoEdid de ", MsgBoxStyle.Critical, "Al validar Usuario")
            Return False
        End Try
    End Function
    Public Function WhatIsMyIP() As String

        Dim WhatIsMyIPUrl As String = "http://whatismyip.com/automation/n09230945.asp"
        Dim req As HttpWebRequest
        Dim res As HttpWebResponse
        Dim Stream As IO.Stream
        Dim PublicIP As String = String.Empty
        Dim sr As StreamReader

        Try
            req = WebRequest.Create(WhatIsMyIPUrl)
            res = req.GetResponse()
            Stream = res.GetResponseStream()
            sr = New StreamReader(Stream)
            PublicIP = sr.ReadToEnd()
            sr.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
        Return PublicIP
    End Function

    Public Function Inicia_Terminal() As Boolean
        Try
            TerminalActivo = Nothing
            Dim tap As New EquiposTableAdapter
            If isRegisterEquipo(Dominio._HotName) = False Then
                sql = "Equipo no registrado en el sistema" + vbNewLine
                sql = sql + "Regístrese por favor.."
                MsgBox(sql, MsgBoxStyle.Critical, "Importante")
                Using forRegister As New frmRegistroEquipo()
                    With forRegister
                        .StartPosition = FormStartPosition.CenterScreen
                        .ShowDialog()
                        If .DialogResult = Windows.Forms.DialogResult.OK Then
                            Return Inicia_Terminal()
                        Else
                            Return False
                        End If
                    End With
                End Using
            End If

            If Not IsNothing(Dominio._HotName) Then

                Dim tapt As New TerminalTableAdapter()
                Dim dt As New DataTable
                dt = tapt.GetDataByDominio(Dominio._HotName)
                If dt.Rows.Count > 0 Then
                    With TerminalActivo
                        .Dominio = Dominio._HotName
                        .codTerminal = dt(0)("codTerminal")
                        .idTerminal = dt(0)("idTerminal")
                        .idBodega = dt(0)("idBodega")
                        .CodPntoEmision = dt(0)("CodPntoEmision")
                    End With
                    Return True
                End If
            End If

            If IsNothing(TerminalActivo.codTerminal) Then
                sql = "Este Equipo navega como anónimo" + vbNewLine
                sql = sql + "No tendrá acceso a ciertas opciones.."
                MsgBox(sql, MsgBoxStyle.Exclamation, "Importante")
            End If

            Return True
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
            Return False
        End Try

    End Function


    Public Async Function GetInfoEcommerce(Optional ViewExeption As Boolean = False) As Task(Of myCommerce)
        Return Await Task.Factory.StartNew(Function()

                                               Try
                                                   Using db As New DataContext
                                                       Return db.myCommerce.FirstOrDefault()
                                                   End Using
                                               Catch ex As Exception
                                                   If ViewExeption Then
                                                       MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error ")
                                                   Else
                                                       System.Diagnostics.Debug.WriteLine(ex.Message & vbLf & ex.StackTrace)
                                                   End If
                                                   Return Nothing
                                               End Try
                                           End Function)

    End Function

    Public Async Function GetBodegas(Optional ViewExeption As Boolean = False) As Task(Of Bodegas)
        Return Await Task.Factory.StartNew(Function()

                                               Try
                                                   Using db As New DataContext
                                                       Return db.Bodegas.FirstOrDefault()
                                                   End Using
                                               Catch ex As Exception
                                                   If ViewExeption Then
                                                       MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error ")
                                                   Else
                                                       System.Diagnostics.Debug.WriteLine(ex.Message & vbLf & ex.StackTrace)
                                                   End If
                                                   Return Nothing
                                               End Try
                                           End Function)

    End Function

    Public Async Function GetOptionSignature(Optional ViewExeption As Boolean = False) As Task(Of SignatureOptions)
        Return Await Task.Factory.StartNew(Function()

                                               Try
                                                   Using db As New DataContext
                                                       Return db.SignatureOptions.FirstOrDefault()
                                                   End Using
                                               Catch ex As Exception
                                                   If ViewExeption Then
                                                       MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error ")
                                                   Else
                                                       System.Diagnostics.Debug.WriteLine(ex.Message & vbLf & ex.StackTrace)
                                                   End If
                                                   Return Nothing
                                               End Try
                                           End Function)

    End Function


#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
            End If
        End If
        disposedValue = True
    End Sub
    Public Sub Dispose() Implements IDisposable.Dispose
        If Me.connecction.State = ConnectionState.Open Then
            Me.connecction.Close()
        End If
    End Sub
#End Region
End Class
