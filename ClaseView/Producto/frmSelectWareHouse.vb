Imports System.Windows.Forms
Imports CADsisVenta.Funtions

Public Class frmSelectWareHouse


    Protected Friend IdBodega As Integer
    Protected Friend NomBodega As String

    Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            If Me.ComboBox1.SelectedIndex >= 0 Then
                IdBodega = CType(Me.ComboBox1.SelectedValue, Integer)
                NomBodega = ComboBox1.Text
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmSelectWareHouse_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        GetListWareHouse()
    End Sub

    Private Async Sub GetListWareHouse()
        Try

            Me.ComboBox1.DataSource = Await StoreProcedure.GetListWareHuose()
            Me.ComboBox1.DisplayMember = "Nom_Bodega"
            Me.ComboBox1.ValueMember = "idBodega"
            Me.ComboBox1.SelectedIndex = -1
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub
End Class
