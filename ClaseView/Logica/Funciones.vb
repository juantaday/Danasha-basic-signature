Imports System.Data.SqlClient
Imports CADsisVenta.Class
Imports CADsisVenta.DataSetSystemTableAdapters
Imports CADsisVenta.Funtions
Imports CADsisVenta.Helpers.FInicio

Module PFunciones
    Public Function DecimalFromString(numnerTostring As String) As Double
        Try
            Dim texNumber As String = String.Empty
            For Each Text_ In numnerTostring
                If InStr("0123456789.", Text_) > 0 Then
                    texNumber += Text_
                End If
            Next
            Return Decimal.Parse(texNumber)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return 0
        End Try
    End Function
    Public Function RedondearSi(valorARedondear As Double, PosicionRedondeo As Integer) As Double
        Dim retorno As Double = 0

        retorno = Math.Round(valorARedondear, PosicionRedondeo)
        Return retorno

    End Function
    Public Function isTerminalHabil(codUser As String, codTerminal As String) As ResponseTerminal
        Try
            Using atap As New CajaStadoTableAdapter
                Dim dt As New DataTable
                dt = atap.GetDataByHabilUserWithIdTerminal(codUser, TerminalActivo.idTerminal)
                If Not (dt.Rows.Count = 0) Then
                    Return New ResponseTerminal With {.Success = True, .DataDb = dt, .IDterminal = dt.Rows(0)("idCajaStado")}
                End If
                dt = atap.GetDataByHabilAllUserOnIdTerminal(TerminalActivo.idTerminal)
                If Not (dt.Rows.Count = 0) Then
                    Return New ResponseTerminal With {.Success = True, .DataDb = dt, .IDterminal = dt.Rows(0)("idCajaStado")}
                End If
                dt = Nothing
                sql = "Este terminal no tiene estado de operación activa." & vbNewLine
                sql = sql & "Solicítelo al administrador de terminales."
                MsgBox(sql, MsgBoxStyle.Exclamation, "Importante")
                Return New ResponseTerminal With {.Success = False, .DataDb = Nothing}
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return New ResponseTerminal With {.Success = False, .DataDb = Nothing}
        End Try
    End Function


    Public Function IsOpenTerminal(idStadoCaja As Integer, Optional satate As Integer = 1) As Boolean
        Try
            sql = "select  top (1) idCajaStado 
                from CajaStado" & vbLf &
                 $"where idCajaStado = {idStadoCaja} and  [Stado] = {satate};"

            Using cmd As New SqlComandExec()
                Using dt As DataTable = cmd.RetornaTabla(sql)
                    If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                        Return True
                    Else
                        If (satate = 1) Then
                            MsgBox($"Esta operación esta cerrada idStadoCaja: {idStadoCaja}", MsgBoxStyle.Exclamation, "Alert..")

                        End If
                        Return False

                    End If

                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function


    Public Function RedondearNo(valorARedondear As Double, PosicionRedondeo As Integer) As Double
        Dim retorno As Double = 0
        Dim strinNum As String = valorARedondear
        Dim datos() As String

        datos = Split(strinNum, ".")
        If datos.Count > 1 Then
            sql = Left(datos(1), PosicionRedondeo)
            strinNum = datos(0) & "." & sql
        End If

        retorno = strinNum

        Return retorno

    End Function
    Public Function Borra_SelectRowDataGrip(ByVal DataGrip As DataGridView) As Boolean
        Try
            For Each Grip In DataGrip.SelectedRows
                DataGrip.Rows(Grip.index).Selected = False
            Next
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Erro en ele Modulo: Funciones Funcion:Borra_SelectRowDataGrip")
            Return False
        End Try

    End Function

    Public Function ImpideOrdenamiento(ByVal dataGrid As DataGridView) As Boolean
        Try
            For i = 0 To dataGrid.Columns.Count - 1
                dataGrid.Columns.Item(i).SortMode = DataGridViewColumnSortMode.Programmatic
            Next i
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            Return False
        End Try
    End Function
    Public Function MySelectProduct(myFindText As String) As Boolean
        Try
            'si no ha dada
            myFindText = Trim(myFindText)
            If myFindText.Length = 0 Or String.IsNullOrWhiteSpace(myFindText) Then
                Return False
            End If
            'preparamos el texto
            Dim isSpace As Boolean = False
            sql = ""
            For Each stri In myFindText
                If Not isSpace Then
                    sql += stri
                    isSpace = False
                End If
                If String.IsNullOrWhiteSpace(stri) Then
                    isSpace = True
                Else
                    If isSpace Then
                        sql += stri
                    End If
                    isSpace = False
                End If
            Next
            myFindText = sql

            'rebisamos si no es codigo munerico entonces es barra de codigo
            Dim isnumric As Boolean = True
            For Each texto In myFindText
                If InStr("0123456789", texto) = False Then
                    isnumric = False
                    Exit For
                End If
            Next
            If isnumric Then
                Return preparatedStatement(myFindText, String.Empty, String.Empty, 0)
            End If

            'para codigo de producto
            Dim isText As Boolean = False
            isnumric = False
            For Each texto In myFindText
                If String.IsNullOrWhiteSpace(texto) Then
                    isText = False
                    isnumric = False
                    Exit For
                ElseIf InStr("0123456789", texto) = False Then
                    If Not isText Then
                        isText = True
                    End If
                Else
                    If Not isnumric Then
                        isnumric = True
                    End If
                End If
            Next
            If (isText = True) And (isnumric = True) Then
                Return preparatedStatement(myFindText, String.Empty, String.Empty, 1)
            End If


            'si es nombre de producto covierto en una matriz
            Dim split As String() = myFindText.Split(" ")
            Select Case split.Count
                Case 1
                    Return preparatedStatement(split(0), String.Empty, String.Empty, 2)
                Case 2
                    Return preparatedStatement(split(0), split(1), String.Empty, 2)
                Case 3
                    Return preparatedStatement(split(0), split(1), split(2), 2)
                Case > 3
                    Return preparatedStatement(split(0), split(1), split(2), 2)
                Case Else
                    Return False
            End Select
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function
    Private Function preparatedStatement(param1 As String, param2 As String,
                       param3 As String, isField As Int16) As Boolean
        Try
            Using cnn As New SqlConnection(SimpleDataApp.Utility.GetConnectionString())
                cnn.Open()
                Using cmd As New SqlCommand("[dbo].[prdSelectMyProduc]", cnn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("@parameter1", SqlDbType.VarChar)
                    cmd.Parameters.Add("@parameter2", SqlDbType.VarChar)
                    cmd.Parameters.Add("@parameter3", SqlDbType.VarChar)
                    cmd.Parameters.Add("@isField", SqlDbType.TinyInt)
                    cmd.Parameters.Add("@codUser", SqlDbType.Char, 8)
                    cmd.Parameters.Add("@codTerminal", SqlDbType.Char, 8)
                    'set values
                    cmd.Parameters("@parameter1").Value = param1
                    cmd.Parameters("@parameter2").Value = param2
                    cmd.Parameters("@parameter3").Value = param3
                    cmd.Parameters("@isField").Value = isField
                    cmd.Parameters("@codUser").Value = UsuarioActivo.codUser
                    cmd.Parameters("@codTerminal").Value = TerminalActivo.codTerminal
                    ' prepara solo prodcutos seleccionados
                    If cmd.ExecuteNonQuery() Then
                        Return True
                    Else
                        Return False
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function

    Public Async Sub SetCeroIdCajaStado()
        Await Task.Run(Sub()

                           For Each mdi As Form In Application.OpenForms
                               If (mdi.Name.Equals("frmVentas")) Then
                                   CType(mdi, frmVentas).idCajaStado = -1
                               End If

                           Next
                       End Sub)

    End Sub
End Module

