Imports System.Drawing.Printing
Imports System.Data.SqlClient
Imports LibPrintTicket.PrintTicket
Imports CADsisVenta.Funtions
Imports CADsisVenta.Class
Imports CADsisVenta.Data.Emuns.EnumSatateModule
Imports CADsisVenta.Helpers.FInicio
Imports CADsisVenta.Data.Emuns
Imports CADsisVenta.DataSetEmployeeTableAdapters
Imports CADsisVenta

Public Class frmList_Facturas
    Dim Num_Factura, Cliente, TipoVent, Cajero, Fechades, FechaHast, Direccion, Telefono, fechaReal, Ruc As String
    Dim Base0, Base12, ivaTotal, otroValor, Total As Double

    Private document As String
    Private prtSettings As PrinterSettings
    Private prtDoc As PrintDocument
    Private prtFont As System.Drawing.Font
    Private isLoad As Boolean
    Private documentName As String
    Protected Friend flag As String

    Private Sub frmListFactura_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Initializa_Load_Base()
    End Sub

    Private Sub Initializa_Load_Base()
        If IsNothing(flag) Then
            flag = "Visualizar"
        End If
        If Not IsNothing(flag) Then
            Select Case flag.ToString()
                Case "Generar"
                    If IniciaProseso() Then
                        flag = "Visualizar"
                        Call Initializa_Load_Base()
                    Else
                        Close()
                    End If
                Case "GenerarImprimir"
                Case "Imprimir"
                    sql = "WHERE  (fv.Impreso = 0) AND (fv.codUser = '" + UsuarioActivo.codUser + "') and (fv.Estado = 255) "
                    MostarFactura_Select(sql)
                    If PrinterTodo() Then
                        Close()
                    End If
                Case "ImprimirSelect"
                    sql = "WHERE  fv.idFactVenta  = " & id & ""
                    MostarFactura_Select(sql)
                    If PrinterSelect() Then
                        Close()
                    End If
                Case "ImprimirList"
                    sql = "INNER Join [tmp].[DocumentULT] AS t ON fv.idFactVenta = t.idDocument "
                    sql = sql & "where t.codTerminal  = '" & TerminalActivo.codTerminal & "' and t.codUser  = '" & UsuarioActivo.codUser & "' "
                    MostarFactura_Select(sql)
                    If PrinterSelect() Then
                        Close()
                    End If
                Case "Visualizar"
                    sql = "WHERE  (fv.Impreso = 0) AND (fv.codUser = '" + UsuarioActivo.codUser + "') "
                    MostarFactura_Select(sql)
            End Select
        End If
        SplitContainer1.Panel2Collapsed = True
        WindowState = FormWindowState.Maximized
    End Sub
    Private Function IniciaProseso() As Boolean
        Return True
    End Function

    Private Function BuscandoFecha() As Boolean
        sql = "SELECT Min(v.fechaVenta) AS fecha1, Max(v.fechaVenta) AS Fecha2 "
        sql = sql & "FROM Ventas as v "
        sql = sql & "WHERE (v.tipoVenta=" & codRecupa & ") and (v.codUser = '" & UsuarioActivo.codUser & "') "
        Try

            Using cnn = New SqlConnection(DomainSQLite.Setting.Configuration.ConectionString)
                cnn.Open()

                Using cmd As New SqlCommand(sql, cnn)
                    cmd.CommandType = CommandType.Text
                    Dim dat As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable

                    dat.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        FacturVenta.fechDesde = FormatDateTime(dt.Rows(0)("fecha1").ToString, DateFormat.ShortDate)
                        FacturVenta.fechHasta = FormatDateTime(dt.Rows(0)("Fecha2").ToString, DateFormat.ShortDate)
                        Return True
                    Else
                        Return False
                    End If
                    dt = Nothing
                End Using

            End Using


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error en el BuscandoFecha")
            Return False
        End Try
    End Function

    Private Sub btnSalir_Click(sender As System.Object, e As System.EventArgs)
        Me.Close()
    End Sub


    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        If flag = "Generar" Then
            Me.Cursor = Cursors.WaitCursor
            IniciaProseso()
            sql = "WHERE  (fv.Impreso = 0) AND (fv.codUser = '" + UsuarioActivo.codUser + "')"
            MostarFactura_Select(sql)
            Me.Cursor = Cursors.Default
        End If
    End Sub
    Private Sub btnPrint1_Click(sender As System.Object, e As System.EventArgs) Handles printTicket.Click
        If PrinterTodo() Then
            Initializa_Load_Base()
        End If
    End Sub
    Private Function PrinterSelect() As Boolean
        Try
            'si  no tengo determidado mi vandera salgo
            If IsNothing(flag) Then
                Return False
            End If
            If flag.Equals(String.Empty) Then
                Return False
            End If
            If flag.Equals("ImprimirSelect") Then
                ListViewCabecera.Items(0).Selected = True
                Return imprimir_Factura()
            ElseIf flag.Equals("ImprimirList") Then
                Dim i As Integer
                For i = 0 To ListViewCabecera.Items.Count - 1
                    ListViewCabecera.Items(i).Selected = True
                    imprimir_Factura()
                Next
                If i > 0 Then
                    Return True
                Else
                    Return False
                End If
            ElseIf ListViewCabecera.SelectedItems.Count = 1 Then
                If Not (MsgBox("Va imprimir esta factura en TICKET.?", MsgBoxStyle.Information + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Responda") = MsgBoxResult.Yes) Then
                    Return False
                Else
                    Return imprimir_Factura()
                End If
            End If
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function
    Private Function PrinterTodo() As Boolean
        Try
            Dim vueltas As Integer = 0
            Dim idFactura As Integer = 0
            If ListViewCabecera.Items.Count > 0 Then
                If MsgBox("Está seguro de imprimir los documentos seleccionados?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Responda") = MsgBoxResult.Yes Then
                    Me.ListViewCabecera.MultiSelect = False
                    For Each eachItem As ListViewItem In ListViewCabecera.CheckedItems
                        Integer.TryParse(eachItem.Text, idFactura)
                        Print_Ticket(idFactura, False)
                    Next
                    MarqImpreso()
                    Return True
                Else
                    Return False
                End If
            Else
                MsgBox("No hay información para imprimir", MsgBoxStyle.Information, "Aviso")
                printTicket.Enabled = False
            End If
            Return False
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function

    Private Function MarqImpreso() As Boolean
        Try
            SeleccionaImpreso()
            Dim breakfast As System.Windows.Forms.ListView.CheckedListViewItemCollection = ListViewCabecera.CheckedItems
            Dim item As System.Windows.Forms.ListViewItem
            Dim resul As Boolean = False

            For Each item In breakfast
                resul = True
                Dim idFactur As Integer = Integer.Parse(item.SubItems(0).Text)
                '  *************************************** pono como ya impreso
                sql = "update FacturaVenta set Impreso  = 1 "
                sql = sql & "Where idFactVenta  = " & idFactur & " "
                Try

                    Using cnn = New SqlConnection(DomainSQLite.Setting.Configuration.ConectionString)
                        cnn.Open()

                        Using cmd As New SqlCommand(sql, cnn)
                            cmd.CommandType = CommandType.Text
                            If cmd.ExecuteNonQuery Then
                            Else
                                resul = False
                                Exit For
                            End If
                        End Using

                    End Using



                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical, "Error en el MarqImpreso")
                    Return False
                End Try

                '    **************************** si esta en cola de impresion lo retira
                sql = "update FacturaVenta set Estado  = 1 "
                sql = sql & "Where (idFactVenta  = " & idFactur & ")  and (Estado = 255) "
                Try

                    Using cnn = New SqlConnection(DomainSQLite.Setting.Configuration.ConectionString)
                        cnn.Open()

                        Using cmd As New SqlCommand(sql, cnn)
                            cmd.CommandType = CommandType.Text
                            cmd.ExecuteNonQuery()
                        End Using

                    End Using


                Catch ex As Exception
                    MsgBox("No se retiro de la cola se impresión", MsgBoxStyle.Critical, "Error en el MarqImpreso")
                End Try
            Next
            Return resul
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function

    Private Function SeleccionaImpreso() As Boolean
        For i = 0 To Me.ListViewCabecera.Items.Count - 1

            If Me.ListViewCabecera.Items(i).Selected = True Then
                SelecionaImp(i)
                Return True
            End If
        Next
        Return False
    End Function
    Private Sub SelecionaImp(ByVal List As Integer)
        Me.ListViewCabecera.MultiSelect = True
        For i = 0 To List
            Me.ListViewCabecera.Items(i).Selected = True
        Next
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs)
        imprimir_Factura()
    End Sub

    Public Function Carga_Cabera() As Boolean
        Try
            If (ListViewCabecera.SelectedItems.Count = 1) Then
                Dim coleecionItem As System.Windows.Forms.ListView.SelectedListViewItemCollection =
                        ListViewCabecera.SelectedItems
                Dim item As New System.Windows.Forms.ListViewItem
                For Each item In coleecionItem
                    FacturVenta.NumFactur = item.SubItems(1).Text
                    Cliente = item.SubItems(2).Text
                    Fechades = item.SubItems(3).Text
                    FechaHast = item.SubItems(4).Text
                    Base0 = item.SubItems(5).Text
                    Base12 = item.SubItems(6).Text
                    ivaTotal = item.SubItems(7).Text
                    otroValor = item.SubItems(8).Text
                    Total = item.SubItems(9).Text
                    Direccion = item.SubItems(10).Text
                    TipoVent = item.SubItems(11).Text
                    Ruc = item.SubItems(12).Text
                    If Fechades = FechaHast Then  'berificamos las fecha decompra facturadas
                        fechaReal = Convert.ToString(FormatDateTime(Fechades, DateFormat.ShortDate))
                    Else
                        fechaReal = Convert.ToString(FormatDateTime(Fechades, DateFormat.ShortDate) _
                                    & "-" & FormatDateTime(FechaHast, DateFormat.ShortDate))
                    End If
                    Return True
                Next
            End If
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error:" & Me.Name & " en el CargaDatosCabera")
            Return False
        End Try
    End Function

    Private Sub detailButton_Click(sender As Object, e As EventArgs) Handles detailButton.Click
        Try
            If Not SplitContainer1.Panel2Collapsed Then
                SplitContainer1.Panel2Collapsed = True
                Return
            End If
            If ListViewCabecera.SelectedItems.Count = 1 Then
                lblTituloDetalle.Text = " Detalle del documento: " & ListViewCabecera.SelectedItems(0).SubItems(FacturColum.Index).Text
                If Carga_DetalleFactur(Me.ListViewCabecera.SelectedItems(IdFactureColum.Index).Text) Then
                    SplitContainer1.Panel2Collapsed = False
                End If
            Else
                MsgBox("Seleccione uno del listado..", MsgBoxStyle.Exclamation, "Importante")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ListViewCabecera_Click(sender As Object, e As EventArgs) Handles ListViewCabecera.Click
        SplitContainer1.Panel2Collapsed = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles setIsPrinterButton.Click
        Try
            If ListViewCabecera.CheckedItems.Count > 0 Then
                sql = "Esta opción desaparecerá la lista de documentos aún no impresos." & vbNewLine
                sql = sql & "Desea continuar...?"
                If MsgBox(sql, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Responda..") = MsgBoxResult.Yes Then
                    If MarqImpreso() Then
                        sql = "WHERE  (fv.Impreso = 0) AND (fv.codUser = '" + UsuarioActivo.codUser + "')"
                        MostarFactura_Select(sql)
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub viewReportButton_Click_1(sender As Object, e As EventArgs) Handles viewReportButton.Click
        Try
            If preparateViewData() Then
                Select Case Me.documentName
                    Case "Factura"
                        Using viewReport As New frmReportFactura(viewLoadReport.All)
                            viewReport.ShowDialog()
                        End Using
                    Case "Nota de venta"
                        Using viewReport As New frmReportNotaVenta(viewLoadReport.All)
                            viewReport.ShowDialog()
                        End Using
                    Case "Proforma"
                        Using viewReport As New frmReportProforma(viewLoadReport.All)
                            viewReport.ShowDialog()
                        End Using
                End Select
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Function preparateViewData() As Boolean
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim breakfast As System.Windows.Forms.ListView.CheckedListViewItemCollection = ListViewCabecera.CheckedItems
            Dim item As System.Windows.Forms.ListViewItem
            Me.documentName = String.Empty

            Dim laps As Integer = 0
            For Each item In breakfast
                If laps = 0 Then
                    Me.documentName = item.SubItems(Nom_Docu.Index).Text
                Else
                    If Not (documentName.ToString.Equals(item.SubItems(Nom_Docu.Index).Text)) Then
                        MsgBox("Seleccione solo un tipo de documento.", MsgBoxStyle.Exclamation, "Importante")
                        Return False
                    End If
                End If
                laps += 1
            Next
            Return printViewDocument()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try

    End Function


    Private Function printViewDocument() As Boolean
        Try
            Using cnn As New SqlConnection(SimpleDataApp.Utility.GetConnectionString)
                cnn.Open()
                Dim command As SqlCommand = cnn.CreateCommand()
                Dim transaction As SqlTransaction

                ' Start a local transaction
                transaction = cnn.BeginTransaction("SampleTransaction")

                ' Must assign both transaction object and connection
                ' to Command object for a pending local transaction.
                command.Connection = cnn
                command.Transaction = transaction
                command.CommandType = CommandType.Text
                'sql intruccion for delete
                sql = "delete [tmp].[DocumentSelectUser]  "
                sql = sql & "WHERE((codUser = @codUser)And (codTerminal =@codTerminal)) "
                command.CommandText = sql
                command.Parameters.Add("@codUser", SqlDbType.Char, 8)
                command.Parameters.Add("@codTerminal", SqlDbType.Char, 8)
                'set value
                command.Parameters("@codUser").Value = UsuarioActivo.codUser
                command.Parameters("@codTerminal").Value = TerminalActivo.codTerminal
                'delete temp datd
                command.ExecuteNonQuery()
                'Add data

                Dim breakfast As System.Windows.Forms.ListView.CheckedListViewItemCollection = ListViewCabecera.CheckedItems
                Dim item As System.Windows.Forms.ListViewItem
                Dim laps As Integer = 0
                'data sql string

                sql = "Insert into [tmp].[DocumentSelectUser] "
                sql = sql & "(codTerminal,codUser,idFactVenta) "
                sql = sql & "Values(@codTerminal,@codUser,@idFactVenta)"

                command.CommandText = sql
                For Each item In breakfast
                    If laps = 0 Then
                        command.Parameters.Add("@idFactVenta", SqlDbType.Int)
                    End If
                    command.Parameters("@codUser").Value = UsuarioActivo.codUser
                    command.Parameters("@codTerminal").Value = TerminalActivo.codTerminal
                    command.Parameters("@idFactVenta").Value = item.Text
                    If Not (command.ExecuteNonQuery() = 1) Then
                        Return False
                    End If
                    laps += 1
                Next
                transaction.Commit()
            End Using

            Return True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function

    Private Sub printMatricialButton_Click(sender As Object, e As EventArgs) Handles printMatricialButton.Click
        Try
            MsgBox("No disponible en esta versión..")
            Return

            If preparateViewData() Then
                Select Case Me.documentName
                    Case "Factura"
                        Using viewReport As New frmReportFactura(viewLoadReport.All)
                            viewReport.Show()
                            viewReport.printDefaultButton.PerformClick()
                        End Using
                    Case "Nota de venta"
                        Using viewReport As New frmReportNotaVenta(viewLoadReport.All)
                            viewReport.Show()
                            viewReport.printDefaultButton.PerformClick()
                        End Using
                    Case "Proforma"
                        Using viewReport As New frmReportProforma(viewLoadReport.All)
                            viewReport.Show()
                            viewReport.printDefaultButton.PerformClick()
                        End Using

                End Select
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub anulaButton_Click(sender As Object, e As EventArgs) Handles anulaButton.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Using newform As New LoginForm(stateReturn._response, "Ventas")
                With newform

                    .Text = "Validando para midificar"
                    .ShowDialog()
                    If .DialogResult = DialogResult.OK Then
                        Dim idFactura As Integer = 0
                        Integer.TryParse(ListViewCabecera.CheckedItems(0).Text, idFactura)
                        If (idFactura > 0) Then
                            sql = $"update FacturaCompra set state  = 0
                                    where idFacturaCompra = {idFactura}"
                            Using cmd As New SqlComandExec
                                If cmd.ExecuteComand(sql) Then
                                    Me.Cursor = Cursors.Default
                                    MsgBox("Documento anulado", MsgBoxStyle.Information, "Aviso.")
                                End If
                            End Using
                        End If

                    End If
                End With
            End Using

            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ChangCustomerButton_Click(sender As Object, e As EventArgs) Handles ChangCustomerButton.Click
        If ListViewCabecera.CheckedItems.Count = 0 Then
            Interaction.MsgBox("Seleccione un item para cambiar de cliente..")
            Return
        End If

        Dim id = ListViewCabecera.CheckedItems(0).Text

        Try
            Me.Cursor = Cursors.WaitCursor

            Dim codUserAutorize As String = ""

            Using newform As New LoginForm(stateReturn._response, "Ventas")
                With newform

                    .Text = "Validando para midificar"
                    .ShowDialog()
                    If .DialogResult = DialogResult.OK Then
                        codUserAutorize = .UsernameTextBox.Text
                    End If
                End With
            End Using

            If String.IsNullOrEmpty(codUserAutorize) Then
                Return
            End If


            Dim idFactura As Integer = 0
            Dim idPersonas As Integer = 0
            Dim idCliente As Integer = 0

            If Not Integer.TryParse(id, idFactura) Then
                Return
            End If


            Using listPerson As New frmList_Person(stateLoad.Dialogo)
                With listPerson
                    .StartPosition = FormStartPosition.CenterScreen
                    .ShowDialog()
                    If .DialogResult = DialogResult.OK Then
                        idPersonas = .idPersona
                        idCliente = ClsClientes.isClinteBypersonAdmin(idPersonas)
                    End If
                End With
            End Using

            If (idCliente = 0) Then
                Return
            End If



            sql = $"update FacturaVenta set idCliente ={idCliente}" + vbLf +
                       $"where idFactVenta ={idFactura};"

            Using cmd As New SqlComandExec
                If cmd.ExecuteComand(sql) Then
                    Me.Cursor = Cursors.Default
                    MsgBox("Documento actualizado (cliente)", MsgBoxStyle.Information, "Aviso.")
                End If
            End Using

            bntBuscar.PerformClick()

        Catch ex As Exception

            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Me.Cursor = Cursors.Default
        End Try


    End Sub

    Private Sub selectAllCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles selectAllCheckBox.CheckedChanged
        Try
            Me.isLoad = False
            For i = 0 To Me.ListViewCabecera.Items.Count - 1
                ListViewCabecera.Items(i).Checked = selectAllCheckBox.CheckState
            Next
            If selectAllCheckBox.CheckState = CheckState.Checked Then
                PanelControls.Enabled = True
            Else
                PanelControls.Enabled = False
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Me.isLoad = True
        End Try
    End Sub

    Private Sub ListViewCabecera_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles ListViewCabecera.ItemChecked
        If Me.isLoad Then
            If ListViewCabecera.CheckedItems.Count > 0 Then
                PanelControls.Enabled = True
                Me.anulaButton.Enabled = ListViewCabecera.CheckedItems.Count = 1
            Else
                PanelControls.Enabled = False
            End If
        End If
    End Sub

    Private Sub ViewReportButton_Click(sender As Object, e As EventArgs)
        Try
            Select Case Me.document
                Case "Factura"
                    Using reporteNotaVenta As New frmReportFactura(viewLoadReport.Latest)
                        reporteNotaVenta.WindowState = FormWindowState.Maximized
                        reporteNotaVenta.ShowDialog()
                    End Using
                Case "Nota de venta"
                    Using reporteNotaVenta As New frmReportNotaVenta(viewLoadReport.Latest)
                        reporteNotaVenta.WindowState = FormWindowState.Maximized
                        reporteNotaVenta.ShowDialog()
                    End Using
            End Select
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub txtbuscar_TextChanged(sender As Object, e As EventArgs) Handles txtbuscar.TextChanged
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.bntBuscar.Enabled = True
            Me.AcceptButton = Me.bntBuscar
        Else
            Me.bntBuscar.Enabled = False
        End If
    End Sub

    Private Sub bntBuscar_Click(sender As Object, e As EventArgs) Handles bntBuscar.Click
        sql = ""
        Select Case CmbOptionSelect.Text
            Case "Número de Factura"
                sql = "WHERE  (fv.Num_Factu Like '%" + Me.txtbuscar.Text + "%') "
            Case "Cliente"
                Dim mySpliter As ResponseSpliter = GenerateSpliter(txtbuscar.Text)
                If mySpliter.IsSucces Then
                    Select Case mySpliter.Spliter.Count
                        Case 1
                            sql = "WHERE (c.Nombres LIKE '%" + mySpliter.Spliter(0) + "%') "
                        Case 2
                            sql = "WHERE (c.Nombres LIKE '%" + mySpliter.Spliter(0) + "%') and (c.Nombres LIKE '%" + mySpliter.Spliter(1) + "%')  "
                        Case 3
                            sql = "WHERE (c.Nombres LIKE '%" + mySpliter.Spliter(0) + "%') and (c.Nombres LIKE '%" + mySpliter.Spliter(1) + "%') and (c.Nombres LIKE '%" + mySpliter.Spliter(2) + "%') "
                        Case Else
                            sql = "WHERE (c.Nombres LIKE '%" + mySpliter.Spliter(0) + "%') and (c.Nombres LIKE '%" + mySpliter.Spliter(1) + "%') and (c.Nombres LIKE '%" + mySpliter.Spliter(2) + "%') "
                    End Select
                Else
                    MsgBox("Debe determinar el cliente ", MsgBoxStyle.Exclamation, "Error")
                End If

            Case "Ruc (o) C.I"
                sql = "WHERE  (c.Ruc_Ci LIKE '%" + Me.txtbuscar.Text + "%') "
            Case "No Impresas"
                sql = "WHERE (fv.Impreso = 0) AND (fv.codUser = '" + UsuarioActivo.codUser + "')"
            Case "Fecha del documento"
                Dim dateFind As Date = DateTimePickerEnd.Value.Date
                Dim dateStar As Date = DateTimePickerStar.Value.Date
                sql = "WHERE (fv.fechaDesde >= '" & dateStar & "') AND (fv.fechaDesde < dateadd(day, 1, '" & dateFind & "')) "
            Case "ID"
                sql = "WHERE (fv.idFactVenta >= " & Me.txtbuscar.Text & ") AND (fv.idFactVenta <= " & Me.txtbuscar.Text & ") "
        End Select

        If sql.Length > 0 Then
            If MostarFactura_Select(sql) Then
                lbltotalFactur.Visible = True
            Else
                lblNoInforcion.Visible = True
            End If
        Else
            MsgBox("Seleccione una de las opciones de consulta...", MsgBoxStyle.Information, "Aviso")
        End If
    End Sub

    Private Function MostarFactura_Select(ByVal SrtWhere As String) As Boolean
        Me.isLoad = False

        sql = "Select Top(1000) fv.idFactVenta,fv.Num_Factu,c.Nombres As[Cliente], fv.fechaDesde, fv.fechaHasta,  "
        sql = sql & "FV.Base00Iva, FV.Base12Iva, FV.Iva, cast(FV.OtroValor + FV.Total As Decimal(18, 2))  as [Total],  "
        sql = sql & "c.Direccion, fp.formaPago, c.Ruc_Ci, FV.Impreso, FV.codUser, FV.estado, FV.OtroValor, td.Nom_Docu "

        sql = sql & "From [dbo].[FacturaVenta] AS fv "
        sql = sql & "INNER Join [dbo].[ClienteName] AS c ON fv.idCliente = c.idCliente "
        sql = sql & "INNER Join [stm].[FormaPago] AS fp ON fv.idFormaPago = fp.idformaPago "
        sql = sql & "INNER Join [stm].[TypoDocumento] AS td on td.idTypoDocu = fv.idTypoDocument "
        sql = sql & SrtWhere
        sql = sql & "ORDER BY fv.idFactVenta desc"
        SplitContainer1.Panel2Collapsed = True

        Try


            Using cnn = New SqlConnection(DomainSQLite.Setting.Configuration.ConectionString)
                cnn.Open()

                Using cmd As New SqlCommand(sql, cnn)
                    cmd.CommandType = CommandType.Text
                    cmd.ExecuteNonQuery()
                End Using

                Using cmd As New SqlCommand(sql, cnn)
                    cmd.CommandType = CommandType.Text
                    Dim data As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable
                    data.Fill(dt)
                    ListViewCabecera.Items.Clear()
                    If dt.Rows.Count > 0 Then
                        Dim subTotalFila As Double = 0
                        With Me.ListViewCabecera
                            lbltotalFactur.Text = "Total factura:" & 0
                            For i = 0 To dt.Rows.Count - 1
                                Dim Filas As Integer = .Items.Count
                                .Items.Add(dt(i)("idFactVenta").ToString)
                                .Items.Item(Filas).SubItems.Add(dt(i)("Num_Factu").ToString)
                                .Items.Item(Filas).SubItems.Add(dt(i)("Nom_Docu").ToString)
                                .Items.Item(Filas).SubItems.Add(dt(i)("Cliente").ToString)
                                .Items.Item(Filas).SubItems.Add(FormatDateTime(dt(i)("fechaDesde").ToString, DateFormat.ShortDate))
                                .Items.Item(Filas).SubItems.Add(FormatDateTime(dt(i)("fechaHasta").ToString, DateFormat.ShortDate))
                                .Items.Item(Filas).SubItems.Add(dt(i)("Base00Iva").ToString)  'Format((Precio), "###,##0.00")
                                .Items.Item(Filas).SubItems.Add(dt(i)("Base12Iva").ToString)
                                .Items.Item(Filas).SubItems.Add(dt(i)("Iva"))

                                subTotalFila = dt(i)("OtroValor")
                                .Items.Item(Filas).SubItems.Add(subTotalFila.ToString("C2"))

                                subTotalFila = dt(i)("Total")

                                .Items.Item(Filas).SubItems.Add(subTotalFila.ToString("C2"))
                                .Items.Item(Filas).SubItems.Add(dt(i)("Direccion").ToString)
                                .Items.Item(Filas).SubItems.Add(dt(i)("formaPago").ToString)
                                .Items.Item(Filas).SubItems.Add(dt(i)("Ruc_Ci").ToString)

                                'cambio el color del total
                                .Items(Filas).UseItemStyleForSubItems = False
                                .Items(Filas).SubItems(TotalColum.Index).BackColor = Color.Aqua
                                .Items(Filas).SubItems(TotalColum.Index).ForeColor = Color.Blue

                                lbltotalFactur.Text = "Total documentos: " & i + 1
                                lblNoInforcion.Visible = False
                            Next
                            ListViewCabecera.Focus()
                        End With
                        Return True
                    End If
                    Return False
                End Using

            End Using




        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error: frmlistFactura en el MostarFactura_Select")
            ListViewCabecera.Items.Clear()
            Return False
        Finally
            Me.isLoad = True
            Me.PanelControls.Enabled = False
            If ListViewCabecera.Items.Count > 0 Then
                Me.selectAllCheckBox.Enabled = True
            Else
                Me.selectAllCheckBox.Enabled = False
            End If
        End Try
    End Function

    Private Sub CmbOptionSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbOptionSelect.SelectedIndexChanged
        Try
            ListViewCabecera.Items.Clear()
            Me.PanelControls.Enabled = False
            lbltotalFactur.Visible = False
            lblNoInforcion.Visible = False
            Me.PanelImputDate.Visible = False
            txtbuscar.Visible = True
            If CmbOptionSelect.SelectedIndex > -1 Then
                If CmbOptionSelect.Text.Contains("No Impresas") Then 'No Impresas
                    txtbuscar.Enabled = False
                    bntBuscar.Enabled = False
                    sql = "WHERE  (fv.Impreso = 0) AND (fv.codUser = '" + UsuarioActivo.codUser + "') "
                    sql = sql & "ORDER BY fv.Num_Factu "
                    If MostarFactura_Select(sql) Then
                        lbltotalFactur.Visible = True
                    End If
                ElseIf CmbOptionSelect.Text.Contains("Fecha del documento") Then
                    txtbuscar.Visible = False
                    Me.PanelImputDate.Visible = True
                Else
                    txtbuscar.Enabled = True
                    bntBuscar.Enabled = True
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub
    Private Function Carga_DetalleFactur(ByVal IdFactur As Integer) As Boolean

        sql = "SELECT fd.Cantidad, "
        sql = sql & "Cast(fd.Prec_Venta / fd.Cantidad as decimal(12,2)) AS pvp, "
        sql = sql & "fd.Prec_Venta, fd.Iva,  fd.idPresent, "
        sql = sql & "CAST(fd.Prec_Venta AS Decimal(12,2)) as totalDecimal "

        sql = sql & "FROM dbo.FacturaVentaDetail AS fd "
        sql = sql & "WHERE(fd.idFacturaVenta = " & IdFactur & ") "
        sql = sql & "ORDER BY fd.idFacturVentaDetail "


        Dim Filas As Integer = 0
        Try

            Using cnn = New SqlConnection(DomainSQLite.Setting.Configuration.ConectionString)
                cnn.Open()

                Using cmd As New SqlCommand(sql, cnn)
                    cmd.CommandType = CommandType.Text
                    Dim data As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    Dim DR As New DataTable()

                    data.Fill(dt)

                    With Me.ListViewDetail
                        .Items.Clear()
                        For i = 0 To dt.Rows.Count - 1
                            Filas = .Items.Count
                            DR = Medida(dt.Rows(i)("idPresent"))
                            If IsNothing(DR) Then Exit For
                            .Items.Add(dt(i)("Cantidad").ToString)                       '[0]cantidad
                            .Items.Item(Filas).SubItems.Add(DR.Rows(0)("Empaque"))       '[1]'empaque
                            .Items.Item(Filas).SubItems.Add(DR.Rows(0)("NomComun"))      '[2] nombre
                            .Items.Item(Filas).SubItems.Add(dt(i)("pvp").ToString)       '[3] nombre    
                            .Items.Item(Filas).SubItems.Add(dt(i)("Prec_Venta").ToString) '[4] nombre 
                            .Items.Item(Filas).SubItems.Add(dt(i)("Iva").ToString)         '[5] nombre 
                            .Items.Item(Filas).SubItems.Add(dt(i)("totalDecimal").ToString) '[6] nombre 
                        Next
                        If Filas >= 0 Then
                            Me.lblCountItem.Text = "Total de Articulos: " & Filas + 1
                            Me.lblCountItem.Visible = True
                            .Columns(5).Width = 0
                            .Columns(6).Width = 0  'precioTotal a dos cecimales
                            Return True
                        Else
                            Me.lblCountItem.Visible = False
                            Return False
                        End If
                    End With

                End Using

            End Using


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error: " & Me.Name & "en el frmTicket_Load")
            Return False
        End Try
    End Function
    Private Function imprimir_Factura() As Boolean

        'impreson en tcket 
        Try
            Dim i, VueltaItem As Integer
            Dim iva, Cant As Double
            Dim cantStg, Descp, SimIva, pvp, Empaq, TotalStri As String
            Dim Hora As String
            Hora = DateTime.Now.ToString("h:mm:ss")

            'carga nombre de la impresora configurada si no esta  va ha panel de opciones
cargaNuevo:
            If IsNothing(myOptnsPrint.NamePrint) Then
                If Not LoadOptionsPrint(1) Then
                    MsgBox("No se encuentra la impresora configurada", MsgBoxStyle.Information, "Aviso")
                    Using form As New frmOptionPrint
                        With form
                            .ShowDialog()
                            If .DialogResult = DialogResult.OK Then
                                If i = 0 Then
                                    i = 1
                                    GoTo cargaNuevo
                                Else
                                    Return False
                                End If
                            End If
                        End With
                    End Using
                    Return False  'si ya da 2 vueltas y no pudo configurar sale
                End If
                If Not PrinterNametInstol(myOptnsPrint.NamePrint) Then
                    MsgBox("La impresora" + myOptnsPrint.NamePrint + " no está instalada", MsgBoxStyle.Exclamation, "Aviso")
                    Return False
                End If
            Else
                If Not PrinterNametInstol(myOptnsPrint.NamePrint) Then
                    MsgBox("La impresora" + myOptnsPrint.NamePrint + " no está instalada", MsgBoxStyle.Exclamation, "Aviso")
                    Return False
                End If
            End If

            Dim Ticket1 As New CreaTicket(myOptnsPrint.NamePrint, PaperSizeWidth.GetCharLenght(Printer.myOptnsPrint.PaperSizeWidth))
            'caega color de la impresora
            If myOptnsPrint.Color.Equals("Negro") Then
                Ticket1.ColorPrintCinta(ColorPrint.Negro)
            ElseIf myOptnsPrint.Color.Equals("Rojo") Then
                Ticket1.ColorPrintCinta(ColorPrint.Rojo)
            Else
                Ticket1.ColorPrintCinta(ColorPrint.Negro)
            End If

            Ticket1.FontZiseText(FontZise.Default)
            If Not Carga_Cabera() Then 'informacion para imprimir como cabecera
                MsgBox("No se pudo leer la cabecera del listview ", MsgBoxStyle.Information, "Aviso")
                Return False
            End If

            Ticket1.AbreCajon()
            Ticket1.AvanzaEncabezado()                      'AVANZA 28 LINEAS
            Ticket1.isAvanzaLinea = False                     'Impide avanzar a la sigiente lines
            Ticket1.TextoDerecha(FacturVenta.NumFactur, False)
            Ticket1.Avanza2Lines()
            Ticket1.TextoIzquierda("CLIENTE:" + Cliente, False)
            Ticket1.Avanza2Lines()
            Ticket1.TextoExtremos("CI/RUC:" + Ruc, "User:" + UsuarioActivo.codUser)
            Ticket1.Avanza2Lines()
            Ticket1.TextoIzquierda("DIRECCION:" + Direccion, False)
            Ticket1.Avanza2Lines()
            Ticket1.TextoExtremos("FECH:" + Trim(fechaReal) + "H:" + Hora, "Term:" + TerminalActivo.codTerminal)
            Ticket1.Avanza2Lines()
            Ticket1.isAvanzaLinea = True
            Ticket1.TextoIzquierda("FORMA DE PAGO: " + TipoVent, False)
            Ticket1.isAvanzaLinea = False
            Ticket1.LineasIgual()
            Ticket1.Avanza2Lines()
            Ticket1.EncabezadoVenta()
            Ticket1.Avanza2Lines()
            Ticket1.LineasIgual()
            Ticket1.isAvanzaLinea = True
            VueltaItem = 0
            For i = 0 To Me.ListViewDetail.Items.Count - 1
                Cant = Me.ListViewDetail.Items.Item(i).SubItems(0).Text
                Empaq = Me.ListViewDetail.Items.Item(i).SubItems(1).Text
                Descp = (Me.ListViewDetail.Items.Item(i).SubItems(2).Text)
                TotalStri = Me.ListViewDetail.Items.Item(i).SubItems(6).Text       'Precio total del item
                pvp = Me.ListViewDetail.Items.Item(i).SubItems(3).Text             'precio unidad

                iva = Double.Parse(Me.ListViewDetail.Items.Item(i).SubItems(5).Text) 'Rebiso si tengo iva
                If iva > 0 Then
                    SimIva = "*"
                Else
                    SimIva = ""
                End If
                cantStg = Cant
                cantStg += Empaq
                cantStg = TextoDiseñado(cantStg, Alinea.Derecha, 6)
                Ticket1.AgregaArticulo(SimIva, cantStg, Descp, pvp, TotalStri) 'imprime una linea de descripcion
                VueltaItem += 1
                If VueltaItem = myOptnsPrint.items Then Exit For
            Next

            For i = 1 To (myOptnsPrint.items - VueltaItem)
                Ticket1.AvanzaRollo(1)
            Next
            Ticket1.isAvanzaLinea = False
            Ticket1.LineasIgual()
            Ticket1.Avanza2Lines()
            Ticket1.AgregaTotales("BASE TARIFA IVA 0%:", FormatNumber(Base0, 2))
            Ticket1.Avanza2Lines()
            Ticket1.AgregaTotales("BASE TARIFA IVA 12%:", FormatNumber(Base12, 2))
            Ticket1.Avanza2Lines()
            Ticket1.AgregaTotales("SUB TOTAL:", FormatNumber(Base0 + Base12, 2))
            Ticket1.Avanza2Lines()
            Ticket1.AgregaTotales("IVA 12%", FormatNumber(ivaTotal, 2))
            Ticket1.isAvanzaLinea = False
            'Ticket1.LineasTotales()
            If otroValor > 0 Then
                Ticket1.AgregaTotales("*TOTAL A PAGAR", FormatNumber(Total, 2))
            Else
                Ticket1.AgregaTotales("TOTAL A PAGAR", FormatNumber(Total, 2))
            End If
            Ticket1.AvanzaPiePagina()
            Ticket1.AvanzaRollo(2)
            Ticket1.CortaTicket()
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function
    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs) Handles btnSelectAll.Click
        cmdSelectAll(ListViewCabecera)
        ListViewCabecera.Focus()
    End Sub

    Private Sub btnNoselect_Click(sender As Object, e As EventArgs) Handles btnNoselect.Click
        cmdNotSelect(ListViewCabecera)
    End Sub

    Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click
        cmdCopy(ListViewCabecera)
    End Sub
End Class