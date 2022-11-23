Imports System.Data.SqlClient
Imports System.Windows.Forms

Public Class frmBarrCode

    Private ReadOnly _idPresent As Integer

    Sub New(ByVal idPresent As Integer)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

        Me._idPresent = idPresent
    End Sub


    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If DibujaBarra() Then
            If UpdateBarrCode() Then
                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If
        End If
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Function UpdateBarrCode() As Boolean
        Try
            txtBarcode.Text = Trim(txtBarcode.Text)
            'Revisamos si hay espacio vacios
            If txtBarcode.Text.Length > 0 Then
                For Each Texto In txtBarcode.Text
                    If String.IsNullOrWhiteSpace(Texto) Then
                        MsgBox("Código de barra no válida.", MsgBoxStyle.Exclamation, "Error")
                        Return False
                    End If
                Next
            End If

            Return True

        Catch ex As Exception
            If ex.Message.Contains("CK_ProductoPresentacion_Barcode") Then
                MsgBox("Hay otro producto con este código.", MsgBoxStyle.Exclamation, "Importante")
            Else
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            End If

            Return False
        End Try
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        DibujaBarra()
    End Sub
    Private Function DibujaBarra() As Boolean
        Try
            Dim alto As Single = 50
            Dim bm As Bitmap = Nothing
            txtBarcode.Text = Trim(txtBarcode.Text)
            If txtBarcode.Text.Length > 0 Then
                bm = BarCodeClass.codigo128("A" & txtBarcode.Text & "B", True, alto)
                If Not IsNothing(bm) Then
                    PictureBox1.Image = bm
                End If
            Else
                PictureBox1.Image = Nothing
            End If
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function

    Private Sub frmBarrCode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DibujaBarra()
    End Sub
    Private Sub barCodeTextBox_TextChanged(sender As Object, e As EventArgs) Handles txtBarcode.TextChanged
        txtBarcode.Text = Trim(txtBarcode.Text)
        If txtBarcode.Text.Length > 0 Then
            DibujaBarra()
        End If
    End Sub

End Class
