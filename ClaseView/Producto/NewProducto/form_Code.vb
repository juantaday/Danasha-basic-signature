Imports System.Threading
Imports System.Windows.Forms
Imports CADsisVenta

Public Class form_Code

    Private ReadOnly _idPresentation As Integer
    Private ReadOnly _CodeProduct As String
    Private _currentCode As String

    Sub New(ByRef idPresent As Integer, codProdcut As String)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

        Me._idPresentation = idPresent
        Me._CodeProduct = codProdcut

    End Sub


    Private Sub form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtCodeProduct.Text = Me._CodeProduct.Trim()
    End Sub


    Public Function GetCurrentCode() As String
        Return _currentCode
    End Function
    Private Async Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            _currentCode = txtCodeProduct.Text.ToUpper().Trim()

            If (String.IsNullOrEmpty(_currentCode)) Then
                MsgBox("El código no puede ser nulo..")
                Return
            End If


            For Each s In _currentCode
                If (String.IsNullOrEmpty(s)) Then
                    MsgBox("El código no puede contener espacios vacios.")
                    Return
                End If
            Next

            OK_Button.Enabled = False
            Cursor = Cursors.WaitCursor

            If (Await (UpdateCode())) Then
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        Catch ex As Exception
            OK_Button.Enabled = True
            Cursor = Cursors.Default

            If (ex.Message.Contains("UQ__Producto__")) Then
                MsgBox("Ya esta asignado éste código a otro producto", MsgBoxStyle.Critical, "Error")
            Else
                MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")

            End If
        Finally
            OK_Button.Enabled = True
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Async Function UpdateCode() As Task(Of Boolean)

        Dim sql As String = "update pp set pp.codProducto = @codProducto
        from ProductoPresentacion as  pp 
        where pp.idPresentacion =@idPresentacion"

        Using cmd As New Funtions.SqlComandExec()

            cmd.ParameterCollection = New SqlClient.SqlParameter() {
                New SqlClient.SqlParameter With {
                    .ParameterName = "@codProducto",
                    .SqlDbType = SqlDbType.VarChar,
                    .Value = Me._currentCode
                },
                New SqlClient.SqlParameter With {
                    .ParameterName = "@idPresentacion",
                    .SqlDbType = SqlDbType.Int,
                    .Value = Me._idPresentation
                }
            }

            Return Await cmd.ExecuteComandAsync(sql)
        End Using

    End Function


End Class
