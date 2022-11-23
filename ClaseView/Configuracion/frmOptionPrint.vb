'Imports LibPrintTicketMatriz.PrintTick
Imports CADsisVenta.DataSetComprasTableAdapters
Imports CADsisVenta.DataSetTicketTableAdapters
Imports CADsisVenta.Helpers.FInicio

Public Class frmOptionPrint
    Inherits System.Windows.Forms.Form
    Public Título As String
    Private prtFont As System.Drawing.Font
    Private lineaActual As Integer
    Private Idfucntio As Integer = 0
    Private isLoadCheck As Boolean
    Private Sub OptionPrint_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshListDocumentButton.PerformClick()
    End Sub

    Private Sub CargaDatos(idDocument As Integer)
        Me.txtNameTicket.Text = ""
        Me.chekColor.SetItemChecked(0, False)
        Me.chekColor.SetItemChecked(1, False)

        If LoadOptionsPrint(DocumentListBox.SelectedValue, False) Then
            Dim selectItem As Integer = 0
            If (myOptnsPrint.isDefaultConfig) Then
                isDEfaultCheckedListBox.SetItemChecked(0, True)
            Else
                isDEfaultCheckedListBox.SetItemChecked(1, True)
            End If

            Me.txtNameTicket.Text = myOptnsPrint.NamePrint
            If myOptnsPrint.Color = "Rojo" Then
                Me.chekColor.SetItemChecked(1, True)
            ElseIf myOptnsPrint.Color = "Negro" Then
                Me.chekColor.SetItemChecked(0, True)
            End If
            txtItems.Value = myOptnsPrint.items
            Me.typePrintComboBox.Text = myOptnsPrint.typePrint
            Me.PrintLogoCheck.Checked = myOptnsPrint.PrintLogo
        Else
            isDEfaultCheckedListBox.SetItemChecked(0, True)
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnSeleciona.Click
        If SeleccionarImpresora() Then
            Me.txtNameTicket.Text = prtSettings.PrinterName
        End If
    End Sub
    Private Sub btnGuardaTicket_Click(sender As Object, e As EventArgs) Handles okBooton.Click
        Try
            If ValidaPageOne() Then
                If ActulizaDato() Then
                    MsgBox("Informacióm actualizada correctamente", MsgBoxStyle.Information, "Aviso")
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Function ActulizaDato()
        Try
            Dim data As New TerminalConfiTableAdapter
            Dim myforma As String = String.Empty
            For Each item In isDEfaultCheckedListBox.CheckedItems
                myforma = item
            Next
            'leo si ya esta tregistrado este tipode Documento

            Dim dt As New DataTable
            dt = data.GetDataByRegisterWithIdTerminalAndIdDocument(TerminalActivo.idTerminal, DocumentListBox.SelectedValue)
            'si no esta registrado lo insertamos
            If Not dt.Rows.Count > 0 Then
                If Not (data.InsertPageDefault(TerminalActivo.idTerminal, DocumentListBox.SelectedValue) = 1) Then
                    Return False
                End If
            End If


            Select Case myforma
                Case "Configurar"
                    If data.UpdateTicketPageOne(
                       txtItems.Value,
                       chekColor.Text,
                       txtNameTicket.Text,
                       Me.typePrintComboBox.Text,
                        False,'uno es codigo de configrar independiente
                       PrintLogoCheck.Checked,
                       TerminalActivo.idTerminal,
                       DocumentListBox.SelectedValue) = 1 Then

                        Return True
                    End If

                Case "Predeterminado en el sistema"
                    If data.UpdateIsDefaultConfig(True, TerminalActivo.idTerminal, DocumentListBox.SelectedValue) = 1 Then
                        Return True
                    End If
            End Select
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub chekColor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chekColor.SelectedIndexChanged
        If chekColor.SelectedIndex >= 0 Then
            For itemAct = 0 To Me.chekColor.Items.Count - 1
                If Me.chekColor.Items(itemAct) = Me.chekColor.Items(chekColor.SelectedIndex) Then
                    chekColor.SetItemChecked(itemAct, True)
                    If itemAct = 0 Then
                    ElseIf itemAct = 1 Then

                    ElseIf itemAct = 2 Then
                    End If
                Else
                    chekColor.SetItemChecked(itemAct, False)
                End If
            Next
        End If
    End Sub
    Private Sub DocumentListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DocumentListBox.SelectedIndexChanged
        If DocumentListBox.ValueMember.ToString.Length > 0 Then
            If DocumentListBox.SelectedIndex >= 0 Then
                CargaDatos(DocumentListBox.SelectedValue)
                Me.PaneTicketDetail.Visible = True
                Me.PaneTicketDetail.Text = "Configurando para " & DocumentListBox.Text
            End If
        End If
    End Sub
    Private Sub RefreshListDocumentButton_Click(sender As Object, e As EventArgs) Handles RefreshListDocumentButton.Click
        Try
            DocumentListBox.DataSource = Nothing
            Dim adpt As New TypoDocumentoTableAdapter
            Dim dt As DataTable
            dt = adpt.GetData
            If IsNothing(dt) Then
                Return
            End If
            If dt.Rows.Count > 0 Then
                DocumentListBox.DataSource = dt
                DocumentListBox.DisplayMember = "Nom_Docu"
                DocumentListBox.ValueMember = "idTypoDocu"
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            DocumentListBox.DataSource = Nothing
        End Try
    End Sub
    Private Function ValidaPageOne() As Boolean
        'si no ha seleccionado predeterminado retornamos
        If isDEfaultCheckedListBox.GetItemChecked(0) Then
            Return True
        End If
        If typePrintComboBox.SelectedIndex = -1 Then
            MsgBox("Determine el tipo de impresora", MsgBoxStyle.Exclamation, "Aviso")
            typePrintComboBox.Focus()
            Return False
        End If
        'si esta selecionado totas
        If IsNothing(typePrintComboBox.SelectedValue = 0) Then
            MsgBox("Seleccione una del listado", MsgBoxStyle.Exclamation, "Aviso")
            typePrintComboBox.Focus()
            Return False
        End If

        Dim itemcolor As Boolean = False

        For Each itemChecked In chekColor.CheckedItems
            itemcolor = True
        Next

        If Not itemcolor Then
            MsgBox("Seleccione un tipo de color de tinta", MsgBoxStyle.Exclamation, "Aviso")
            chekColor.Focus()
            Return False
        End If

        Dim isDefaul = False
        For Each itemChecked In isDEfaultCheckedListBox.CheckedItems
            isDefaul = True
        Next
        If Not isDefaul Then
            MsgBox("Seleccione una de las opciones..", MsgBoxStyle.Exclamation, "Aviso")
            isDEfaultCheckedListBox.Focus()
            Return False
        End If

        If txtItems.Value = 0 Or txtItems.Value > 120 Then
            MsgBox("La cantidad de item puede tener maximo de 120 y minimo de 1", MsgBoxStyle.Exclamation, "Aviso")
            txtItems.Focus()
            Return False
        End If
        If String.IsNullOrEmpty(txtNameTicket.Text) Then
            MsgBox("Determine el nombre de la impresora", MsgBoxStyle.Exclamation, "Aviso")
            txtItems.Focus()
            Return False
        End If

        If Not PrinterNametInstol(txtNameTicket.Text) Then
            MsgBox("Esta impresona no esta instalada", MsgBoxStyle.Exclamation, "Aviso")
            btnSeleciona.PerformClick()
            Return False
        End If
        Return True
    End Function
    Private Sub isDEfaultCheckedListBox_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles isDEfaultCheckedListBox.ItemCheck
        Try
            If Not isLoadCheck Then
                isLoadCheck = True
                If e.Index = 0 Then
                    If e.NewValue = CheckState.Checked Then
                        PaneTicketDetail.Enabled = False
                        isDEfaultCheckedListBox.SetItemChecked(1, False)
                    Else
                        isDEfaultCheckedListBox.SetItemChecked(1, True)
                        PaneTicketDetail.Enabled = True
                    End If
                ElseIf e.Index = 1 Then
                    If e.NewValue = CheckState.Checked Then
                        isDEfaultCheckedListBox.SetItemChecked(0, False)
                        PaneTicketDetail.Enabled = True
                    Else
                        isDEfaultCheckedListBox.SetItemChecked(0, True)
                        PaneTicketDetail.Enabled = False
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            isLoadCheck = False
        End Try

    End Sub
    Private Sub TableLayoutPanel2_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel2.Paint

    End Sub
End Class