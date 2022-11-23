Imports CADsisVenta
Imports CADsisVenta.Data.Emuns.EnumSatateModule
Imports CADsisVenta.DataSetComprasTableAdapters

Public Class FrmFacturCompProvider
    Dim masterDetail As MasterControl
    Private isTime As Boolean
    Private dateIni As DateTime
    Private dateFin As DateTime
    Private idProveedor As Integer
    Private Sub FrmVentaXcliente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        idProveedor = 0
        isTime = False
        dateIni = Date.Today.AddYears(-1)
        dateFin = Date.Today
        dateFin = dateFin.AddHours(23.9999)
        ViewDate(isTime)
        Carga_Combox()
        Carga_LocalBodega()
    End Sub
    Private Sub Carga_LocalBodega()
        Try
            Dim tadap As New BodegasListTableAdapter
            Dim dt As DataTable = tadap.GetData()
            If dt.Rows.Count > 0 Then
                LocalBodegaComboBox.DataSource = dt
                LocalBodegaComboBox.ValueMember = "idBodega"
                LocalBodegaComboBox.DisplayMember = "Nom_Bodega"
                LocalBodegaComboBox.SelectedIndex = 0
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub Carga_TipoDocument()
        Try
            Dim tadap As New TypoDocumentoTableAdapter
            Dim dt As DataTable = tadap.GetData()
            If dt.Rows.Count > 0 Then
                LocalBodegaComboBox.DataSource = dt
                LocalBodegaComboBox.ValueMember = "idTypoDocu"
                LocalBodegaComboBox.DisplayMember = "Nom_Docu"
                LocalBodegaComboBox.SelectedIndex = 0
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub Carga_Combox()
        ListProviderComboBox.Items.Clear()
        ListProviderComboBox.Items.Add("Todos los proveedores")
        ListProviderComboBox.SelectedIndex = 0
    End Sub
    Sub ViewDate(isTime As Boolean)
        If isTime Then
            sql = "Desde: " & FormatDateTime(dateIni, DateFormat.ShortDate)
            sql = sql & " " & Format(dateIni, "hh:mm tt") & vbNewLine + vbNewLine
            sql = sql & "Hasta: " & FormatDateTime(dateFin, DateFormat.ShortDate)
            sql = sql & " " & Format(dateFin, "hh:mm tt")
            LinkLabel1.Text = sql
        Else
            sql = "Desde: " & FormatDateTime(dateIni, DateFormat.ShortDate) & vbNewLine + vbNewLine
            sql = sql & "Hasta: " & FormatDateTime(dateFin, DateFormat.ShortDate)
            LinkLabel1.Text = sql
        End If
    End Sub
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Using newfecha As New FrmImputDate
            With newfecha
                If isTime Then
                    .DateIni = dateIni
                    .DateFin = dateFin
                Else
                    .DateIni = CDate(FormatDateTime(dateIni, DateFormat.ShortDate))
                    .DateFin = CDate(FormatDateTime(dateFin, DateFormat.ShortDate))
                End If
                .IsHourCheckBox.Checked = isTime
                .ShowDialog()
                If .DialogResult = System.Windows.Forms.DialogResult.OK Then
                    dateIni = .DateIni
                    dateFin = .DateFin
                    isTime = .IsHourCheckBox.Checked
                    ViewDate(isTime)
                    clearFields()
                End If
            End With
        End Using
    End Sub
    Sub clearFields()
        panelView.Controls.Clear()
        masterDetail = Nothing
        Refresh()
    End Sub

    Private Sub FindButton_Click(sender As Object, e As EventArgs) Handles FindButton.Click
        If ListProviderComboBox.SelectedIndex < 0 Then
            MsgBox("SEleccione una opción", MsgBoxStyle.Exclamation, "Importante")
            ListProviderComboBox.Focus()
            Return
        End If

        If isTime Then
            If dateIni.Year = dateFin.Year And
                      dateIni.Month = dateFin.Month And
                      dateIni.Day = dateFin.Day Then
            Else
                MsgBox("Es necesario que consulte solo de una fecha", MsgBoxStyle.Exclamation, "Importante")
                Return
            End If
        End If

        Select Case ListProviderComboBox.SelectedIndex
            Case 0
                Carga_datosAll()
            Case 1
                If Not IsNothing(idProveedor) Then
                    If idProveedor > 0 Then
                        Carga_datosProviderId(idProveedor)
                        Return
                    End If
                End If
        End Select
    End Sub

    Private Sub Carga_datosAll()
        Try
            clearFields()
            Dim totalDeuda As Double = 0
            Dim ds As New DataSetCompras
            Dim tapFCA As New FacturaCompraAllTableAdapter
            Dim tapFCB As New FacturaCampraBodegaTableAdapter
            Dim tapFCD As New FacturaCompraDocumentoTableAdapter

            masterDetail = New MasterControl(ds)
            panelView.Controls.Add(masterDetail)

            tapFCA.Fill(ds.FacturaCompraAll, dateIni, dateFin)
            tapFCB.Fill(ds.FacturaCampraBodega, dateIni, dateFin)
            tapFCD.Fill(ds.FacturaCompraDocumento, dateIni, dateFin)


            masterDetail.setParentSource(ds.FacturaCompraAll.TableName, "idProveedor")
            masterDetail.childView.Add(ds.FacturaCampraBodega.TableName, "Detalle por bodega o local")
            masterDetail.childView.Add(ds.FacturaCompraDocumento.TableName, "Detalle por documento")

            'AddHandler masterDetail.RowEnter, AddressOf ClienteRowEnter
            masterDetail.Columns(0).Visible = False
            masterDetail.Columns(masterDetail.Columns("Total").Index).HeaderText = "Total $"
            For i = 0 To masterDetail.RowCount - 1
                totalDeuda += masterDetail.Rows(i).Cells(masterDetail.Columns("Total").Index).Value
            Next
            TotalLabel.Text = "Total => " & FormatNumber(totalDeuda, 2)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub Carga_datosProductoAll()
        Try
            'clearFields()
            'Dim totalDeuda As Double = 0
            'Dim ds As New DataSetVentas
            'Dim tapFVC As New FacturaVentaGroupByProductTableAdapter
            'Dim tapFVF As New FacturaVentaGroupByClientTableAdapter

            'masterDetail = New MasterControl(ds)
            'panelView.Controls.Add(masterDetail)

            'tapFVC.Fill(ds.FacturaVentaGroupByProduct, dateIni, dateFin)
            'tapFVF.Fill(ds.FacturaVentaGroupByClient, dateIni, dateFin)


            'masterDetail.setParentSource(ds.FacturaVentaGroupByProduct.TableName, "idProducto")
            'masterDetail.childView.Add(ds.FacturaVentaGroupByClient.TableName, "Detalle por cliente")
            ' ''AddHandler masterDetail.RowEnter, AddressOf ClienteRowEnter
            'masterDetail.Columns(masterDetail.Columns("idProducto").Index).Visible = False
            'For i = 0 To masterDetail.RowCount - 1
            '    totalDeuda += masterDetail.Rows(i).Cells(masterDetail.Columns("Total").Index).Value
            'Next
            'TotalLabel.Text = "Total => " & FormatNumber(totalDeuda, 2)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub Carga_datosProviderId(idprovider As Integer)
        Try
            clearFields()
            Dim totalDeuda As Double = 0
            Dim ds As New DataSetCompras
            Dim tapFCD As New FacturaCompraByProviderTableAdapter
            Dim tapFCI As New FacturaCompraItemsTableAdapter

            masterDetail = New MasterControl(ds)
            panelView.Controls.Add(masterDetail)

            tapFCD.Fill(ds.FacturaCompraByProvider, dateIni, dateFin, idprovider)
            tapFCI.Fill(ds.FacturaCompraItems, dateIni, dateFin, idprovider)

            masterDetail.setParentSource(ds.FacturaCompraByProvider.TableName, "idFacturaCompra")
            masterDetail.childView.Add(ds.FacturaCompraItems.TableName, "Detalle por articulo")
            'AddHandler masterDetail.RowEnter, AddressOf ClienteRowEnter
            masterDetail.Columns(0).Visible = False
            masterDetail.Columns(masterDetail.Columns("Total").Index).HeaderText = "Total $"

            For i = 0 To masterDetail.RowCount - 1
                totalDeuda += masterDetail.Rows(i).Cells(masterDetail.Columns("Total").Index).Value
            Next
            TotalLabel.Text = "Total => " & FormatNumber(totalDeuda, 2)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub FindClienteButton_Click(sender As Object, e As EventArgs) Handles FindProviderButton.Click
        Try
            Using listProvider As New frmList_Proveedores(stateLoad.Dialogo, stateClient.User)
                With listProvider
                    .ShowDialog()
                    If .DialogResult = System.Windows.Forms.DialogResult.OK Then
                        idProveedor = .dataListado.SelectedCells.Item(.dataListado.Columns("idProveedor").Index).Value
                        Carga_Combox()
                        sql = Convert.ToString(.dataListado.SelectedCells(.dataListado.Columns("Ruc_Ci").Index).Value) & " "
                        sql = sql & Convert.ToString(.dataListado.SelectedCells(.dataListado.Columns("Razon_social").Index).Value) & " "
                        ListProviderComboBox.Items.Add(sql) 'Nombre del proveedor
                        ListProviderComboBox.SelectedIndex = 1
                        panelView.Controls.Clear()
                    End If
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub
    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
        Me.Close()
    End Sub
End Class