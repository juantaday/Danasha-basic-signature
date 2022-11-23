Imports CADsisVenta
Imports CADsisVenta.DataSetVentasTableAdapters

Public Class frmList_DeudaClientes
    Private cargado As Boolean
    Private idcliente As Integer
    Private SelectIndexUbicated As Integer
    Private NameCliente As String
    Dim masterDetail As MasterControl
    Private Sub CedulaTextBox_TextChanged(sender As Object, e As EventArgs)
        AcceptButton = Nothing

    End Sub

    Private Sub NomApelliTextBox_TextChanged(sender As Object, e As EventArgs) Handles NomApelliTextBox.TextChanged
        AcceptButton = Nothing
        BuscNomApelliButton.Enabled = False
        If NomApelliTextBox.TextLength > 3 Then
            BuscNomApelliButton.Enabled = True
            AcceptButton = BuscNomApelliButton
        End If
    End Sub

    Private Sub todosClientButton_Click(sender As Object, e As EventArgs) Handles todosClientButton.Click
        Carga_Datosclientes()
    End Sub
    Sub clearFields()
        panelView.Controls.Clear()
        masterDetail = Nothing
        Refresh()
    End Sub
    Sub Carga_Datosclientes()
        Try
            clearFields()
            Dim totalDeuda As Double = 0
            Dim ds As New CADsisVenta.DataSetVentas
            Dim tapDC As New DeudaClientesTableAdapter
            Dim tapDD As New DeudaDetalleClientesTableAdapter
            Dim tapCN As New ClienteNameTableAdapter

            masterDetail = New MasterControl(ds)
            panelView.Controls.Add(masterDetail)

            tapDC.Fill(ds.DeudaClientes)
            tapDD.Fill(ds.DeudaDetalleClientes)
            tapCN.Fill(ds.ClienteName)

            masterDetail.setParentSource(ds.DeudaClientes.TableName, "idCliente")
            masterDetail.childView.Add(ds.DeudaDetalleClientes.TableName, "Detalle de dueda")
            masterDetail.childView.Add(ds.ClienteName.TableName, "Detalle cliente")
            AddHandler masterDetail.RowEnter, AddressOf ClienteRowEnter
            For i = 0 To masterDetail.RowCount - 1
                totalDeuda += masterDetail.Rows(i).Cells(masterDetail.Columns("Saldo").Index).Value
            Next
            totalLabel.Text = "Total por cobrar: " & FormatNumber(totalDeuda, 2)
        Catch ex As Exception
            MsgBox(ex.Message + " en el Carga_Datosclientes ", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub ClienteRowEnter(sender As Object, e As DataGridViewCellEventArgs)
        Try
            CobrarLinkLabel.Visible = False
            NameCliente = String.Empty
            Dim dt As DataGridView = sender
            If dt.SelectedRows.Count = 1 Then
                NameCliente = dt.SelectedCells.Item(1).Value
                CobrarLinkLabel.Text = "Cobrar la deuda del: " & NameCliente
                CobrarLinkLabel.Visible = True
                idcliente = dt.SelectedRows(0).Cells(dt.Columns("idCliente").Index).Value
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub BuscNomApelliButton_Click(sender As Object, e As EventArgs) Handles BuscNomApelliButton.Click
        Try
            If panelView.Controls.Count > 0 Then
                Dim dt As DataGridView = panelView.Controls(0)
                CType(dt.DataSource, DataView).RowFilter = String.Format("Nombres like '%" & NomApelliTextBox.Text & "%'")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub NomApelliTextBox_Leave(sender As Object, e As EventArgs) Handles NomApelliTextBox.Leave
        NomApelliTextBox.Text = Trim(NomApelliTextBox.Text)
    End Sub

    Private Sub CedulaBusButton_Click(sender As Object, e As EventArgs)
        Try
            If panelView.Controls.Count > 0 Then
                Dim dt As DataGridView = panelView.Controls(0)
                CType(dt.DataSource, DataView).RowFilter = String.Format("Cedula = '" & NomApelliTextBox.Text & "'")
            End If
        Catch ex As Exception
            MsgBox(ex.Message + " en el CedulaBusButton_Click", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub CobrarLinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles CobrarLinkLabel.LinkClicked
        If idcliente > 0 Then
            Using formnew As New frmCobro()
                With formnew
                    .Text = String.Format("ESTADO DE DEUDA DE: {0}", NameCliente)
                    .idCliente = idcliente
                    .StartPosition = FormStartPosition.CenterScreen
                    .ShowDialog()
                    If .Operation Then
                        todosClientButton.PerformClick()
                    End If
                End With
            End Using
        End If
    End Sub

    Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles _CancelButton.Click
        Close()
    End Sub
    Private Sub frmList_DeudaClientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        todosClientButton.PerformClick()
        SelectIndexUbicated = -1

    End Sub

End Class