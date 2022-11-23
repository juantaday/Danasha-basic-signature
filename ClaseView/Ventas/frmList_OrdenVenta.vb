Imports System.Data.SqlClient
Imports CADsisVenta.Class
Imports CADsisVenta.Helpers.FInicio

Public Class frmList_OrdenVenta
    Dim i As Integer
    Dim cargando As Boolean
    Dim Total As Double
    Dim OtroValor As Double
    Protected Friend idcliente As Integer
    Protected Friend flag As String
    Private Sub ChekOrderList_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ChekOrderList.SelectedIndexChanged
        ChekOrderList_Changet()
    End Sub
    Private Sub ChekOrderList_Changet()
        Carg_SegunChecken()
        Activa_Controles()
    End Sub

    Private Sub Carg_SegunChecken()
        If ChekOrderList.SelectedIndex >= 0 Then
            Me.DataGridView1.DataSource = {}
            For itemAct = 0 To Me.ChekOrderList.Items.Count - 1
                If Me.ChekOrderList.Items(itemAct) = Me.ChekOrderList.Items(ChekOrderList.SelectedIndex) Then
                    ChekOrderList.SetItemChecked(itemAct, True)
                    If itemAct = 0 Then
                        Me.GroupBox1.Visible = False
                        Me.GroupBox2.Visible = False
                        Sql_Todo_Orden() 'mando a cargar sql todo orden
                        Carga_Orden()  ' mando a cargar el dadagridview con el sql
                    ElseIf itemAct = 1 Then
                        Me.GroupBox1.Visible = True
                        Me.GroupBox2.Visible = False
                    ElseIf itemAct = 2 Then
                        Me.GroupBox1.Visible = False
                        Me.GroupBox2.Visible = True
                    End If
                Else
                    ChekOrderList.SetItemChecked(itemAct, False)
                End If

            Next
        End If
    End Sub
    Private Sub Carga_Orden()
        Dim i As Integer

        conecta_sql()

        Try
            Dim cmd As New SqlCommand(sql, Cnn_sql)
            cmd.CommandType = CommandType.Text
            Dim dat As New SqlDataAdapter(cmd)
            Dim dt As New DataTable

            dat.Fill(dt)

            If dt.Rows.Count > 0 Then
                i = dt.Rows.Count

                Me.DataGridView1.DataSource = dt
                DataGridView1.AutoSizeColumnsMode =
                                     DataGridViewAutoSizeColumnsMode.AllCells
                DataGridView1.Columns(0).DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight 'Alineado a la derecha a la orden

                DataGridView1.Columns(4).DefaultCellStyle.Alignment =
                     DataGridViewContentAlignment.MiddleCenter 'Alineado al centro a la fecha

                DataGridView1.Columns(2).DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight 'Alineado a la derecha valor de venta

                DataGridView1.Columns(3).DefaultCellStyle.Alignment =
                  DataGridViewContentAlignment.MiddleRight 'Alineado a la derecha item pr
                DataGridView1.Columns(5).Visible = False  'otro valor
                DataGridView1.Columns(6).Visible = False  'nom apellido
                DataGridView1.Columns(7).Visible = False  'ruc
                DataGridView1.Columns(8).Visible = False  'idcliente
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error Al cargar Ordenes")
        End Try
        cargando = False
        Activa_Controles()
    End Sub

    Private Sub Sql_Todo_Orden()

        sql = "SELECT v.idVenta AS Proforma,p.Ruc_Ci+' '+p.Apellidos+' '+p.Nombre as Clientes, v.fechaVenta AS Fecha, "
        sql = sql & "COUNT(vd.idVentaDetail) AS [Num Articulos], "
        sql = sql & "(CAST(SUM(vd.Vent_Total) AS DECIMAL(30, 2))  + v.OtrosVlores)AS [Total proforma] , "
        sql = sql & "v.OtrosVlores, p.Apellidos + ' ' + p.Nombre as [NomApelli],p.Ruc_Ci, v.idCliente "

        sql = sql & "FROM dbo.Ventas AS v INNER JOIN dbo.Ventas_Detail AS vd ON v.idVenta = vd.idVenta  "
        sql = sql & "inner join Clientes as C on v.idcliente = c.idCliente "
        sql = sql & "inner join personas as p on p.idPersona = c.idPersona "

        sql = sql & "WHERE(v.tipoVenta = 0) "
        sql = sql & "GROUP BY v.idVenta, p.Ruc_Ci+' '+p.Apellidos+' '+p.Nombre, v.fechaVenta, p.Ruc_Ci,v.OtrosVlores,p.Apellidos + ' ' + p.Nombre, v.idCliente "
        sql = sql & "ORDER BY Fecha "
    End Sub
    Private Sub Sql_Fecha_Orden()

        Dim fecha1 As Date = FormatDateTime(Me.dtFecha1.Value, DateFormat.ShortDate)
        Dim fecha2 As Date = FormatDateTime(Me.dtFecha2.Value, DateFormat.ShortDate)

        sql = "SELECT v.idVenta AS Proforma,p.Ruc_Ci+' '+p.Apellidos+' '+p.Nombre as Clientes, v.fechaVenta AS Fecha, "
        sql = sql & "COUNT(vd.idVentaDetail) AS [Num Articulos], "
        sql = sql & "(CAST(SUM(vd.Vent_Total) AS DECIMAL(30, 2))  + v.OtrosVlores)AS [Total proforma] , "
        sql = sql & "v.OtrosVlores, p.Apellidos + ' ' + p.Nombre as [NomApelli],p.Ruc_Ci, v.idCliente "

        sql = sql & "FROM dbo.Ventas AS v INNER JOIN dbo.Ventas_Detail AS vd ON v.idVenta = vd.idVenta  "
        sql = sql & "inner join Clientes as C on v.idcliente = c.idCliente "
        sql = sql & "inner join personas as p on p.idPersona = c.idPersona "

        sql = sql & "WHERE (((v.tipoVenta)=0) AND ((v.fechaVenta) Between '" & fecha1 & "' And '" & fecha2 & "')) "
        sql = sql & "GROUP BY v.idVenta, p.Ruc_Ci+' '+p.Apellidos+' '+p.Nombre, v.fechaVenta, p.Ruc_Ci,v.OtrosVlores,p.Apellidos + ' ' + p.Nombre, v.idCliente "
        sql = sql & "ORDER BY Fecha "

    End Sub
    Private Sub Sql_Orden_Orden()
        Dim Orden1 As Integer = Me.txtNumber1.Value
        Dim Orden2 As Integer = Me.txtNumber2.Value

        sql = "SELECT v.idVenta AS Proforma,p.Ruc_Ci+' '+p.Apellidos+' '+p.Nombre as Clientes, v.fechaVenta AS Fecha, "
        sql = sql & "COUNT(vd.idVentaDetail) AS [Num Articulos], "
        sql = sql & "(CAST(SUM(vd.Vent_Total) AS DECIMAL(30, 2))  + v.OtrosVlores)AS [Total proforma] , "
        sql = sql & "v.OtrosVlores, p.Apellidos + ' ' + p.Nombre as [NomApelli],p.Ruc_Ci, v.idCliente "

        sql = sql & "FROM dbo.Ventas AS v INNER JOIN dbo.Ventas_Detail AS vd ON v.idVenta = vd.idVenta  "
        sql = sql & "inner join Clientes as C on v.idcliente = c.idCliente "
        sql = sql & "inner join personas as p on p.idPersona = c.idPersona "

        sql = sql & "WHERE (((v.tipoVenta)='0') AND ((v.idVenta) Between " & Orden1 & " And " & Orden2 & ")) "
        sql = sql & "GROUP BY v.idVenta, p.Ruc_Ci+' '+p.Apellidos+' '+p.Nombre, v.fechaVenta, p.Ruc_Ci,v.OtrosVlores,p.Apellidos + ' ' + p.Nombre, v.idCliente "
        sql = sql & "ORDER BY Proforma "

    End Sub
    Private Sub sql_UltDocument()
        sql = "Select  v.idVenta As Proforma,c.Ruc_Ci+' '+c.Nombres as Clientes, v.fechaVenta AS Fecha, "
        sql = sql & "COUNT(vd.idVentaDetail) As [Num Articulos], "
        sql = sql & "(CAST(SUM(vd.Vent_Total) As Decimal(30, 2))  + v.OtrosVlores)As [Total proforma] , "
        sql = sql & "v.OtrosVlores, c.Nombres [NomApelli], c.Ruc_Ci, v.idCliente "

        sql = sql & "From dbo.Ventas As v "
        sql = sql & "INNER Join dbo.Ventas_Detail AS vd ON v.idVenta = vd.idVenta  "
        sql = sql & "INNER Join [dbo].[ClienteName] As C On v.idcliente = c.idCliente "
        sql = sql & "INNER Join [tmp].[DocumentULT]  as U on u.idDocument = v.idVenta "

        sql = sql & " WHERE U.codUser = '" & UsuarioActivo.codUser & "' and U.codTerminal = '" & TerminalActivo.codTerminal & "' "
        sql = sql & " GROUP BY v.idVenta, c.Ruc_Ci +' '+c.Nombres, v.fechaVenta, c.Ruc_Ci,v.OtrosVlores,c.Nombres, v.idCliente "
    End Sub



    Private Sub btnBuscarOrden_Click(sender As System.Object, e As System.EventArgs) Handles btnBuscarOrden.Click
        cargando = True  'para que los controles no surtan efecto a rato de cargar
        DataGridView1.DataSource = Nothing
        Sql_Orden_Orden()
        Carga_Orden()
        cargando = False
    End Sub

    Private Sub frmList_OrdenNoFacturada_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        cargando = True
        Me.dtFecha1.Value = Now
        Me.dtFecha2.Value = Now
        If Not IsNothing(flag) Then
            If flag.ToString().Equals("Ventas") Then
                btnBuscarOrden.PerformClick()
            ElseIf flag.ToString().Equals("Imprimir") Then
                btnBuscarOrden.PerformClick()
                btnPrint.PerformClick()
                Close()
            ElseIf flag.ToString().Equals("ImprimirList") Then
                sql_UltDocument()
                Carga_Orden()
                If DataGridView1.Rows.Count > 0 Then
                    DataGridView1.Rows(0).Selected = True
                    btnPrint.PerformClick()
                    Close()
                End If
            End If
        End If
        WindowState = FormWindowState.Maximized
        cargando = False
    End Sub



    Private Sub btnBuscarFech_Click(sender As System.Object, e As System.EventArgs) Handles btnBuscarFech.Click
        cargando = True
        Me.DataGridView1.DataSource = Nothing
        Sql_Fecha_Orden()
        Carga_Orden()
        cargando = False
    End Sub

    Private Sub menuVerDetalle_Click(sender As Object, e As System.EventArgs) Handles menuVerDetalle.Click
        btnDetail.PerformClick()
    End Sub

    Private Sub btnPrint_Click(sender As System.Object, e As System.EventArgs) Handles btnDetail.Click
        Try
            If DataGridView1.SelectedRows.Count = 1 Then
                Using frmDetailOrderVenta
                    With frmDetailOrderVenta
                        .idOrden = DataGridView1.SelectedCells.Item(0).Value
                        .Ruc = DataGridView1.SelectedCells.Item(7).Value
                        .NameCliente = DataGridView1.SelectedCells.Item(6).Value
                        .OtroValor = DataGridView1.SelectedCells.Item(5).Value
                        .lblOrdenN.Text = "DETALLE -- proforma N°:" & .idOrden & "   (" & DataGridView1.SelectedCells.Item(1).Value & ")"
                        .ShowDialog()
                    End With
                End Using
            Else
                MsgBox("Seleccione uno del listado", MsgBoxStyle.Exclamation, "Aviso")
                Me.DataGridView1.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Function PreparaOrden(ByVal cn As SqlConnection, tr As SqlTransaction) As Boolean
        Try
            If DataGridView1.SelectedRows.Count = 0 Then
                MsgBox("Seleccione las 'ordenes a trasferir", MsgBoxStyle.Information, "Importante")
                Return False
            End If
            Cursor = Cursors.WaitCursor
            FacturVenta.total = 0
            sql = "delete tmp.VentasTmp where codTerminal = '" & TerminalActivo.codTerminal & "' and codUser='" & UsuarioActivo.codUser & "' "
            Dim cmd As New SqlCommand(sql, cn, tr)
            cmd.CommandType = CommandType.Text
            cmd.ExecuteNonQuery()
            Dim RowSelect As DataGridViewSelectedRowCollection = DataGridView1.SelectedRows
            Dim Rows As DataGridViewRow
            Dim i As Integer = 0  'para comparar que selecciona un solo cliente
            Dim ultIdCliente As Integer = 0

            For Each Rows In RowSelect
                Pagos.idCliente = Rows.Cells(8).Value
                If i > 0 Then
                    If Not (ultIdCliente = Pagos.idCliente) Then
                        MsgBox("Seleccione datos de un solo cliente", MsgBoxStyle.Exclamation, "Importante")
                        Return False
                    End If
                End If
                If i = 0 Then
                    FacturVenta.fechDesde = Rows.Cells(2).Value
                    FacturVenta.fechHasta = Rows.Cells(2).Value
                Else
                    'para fecha mínimo
                    If Rows.Cells(2).Value < FacturVenta.fechDesde Then
                        FacturVenta.fechDesde = Rows.Cells(2).Value
                    End If
                    'para fecha ma´ximo
                    If Rows.Cells(2).Value > FacturVenta.fechHasta Then
                        FacturVenta.fechHasta = Rows.Cells(2).Value
                    End If
                End If
                idOrderVenta = Rows.Cells(0).Value
                If Not Order_Proceso(idOrderVenta, cn, tr) Then
                    Return False
                End If
                FacturVenta.total += Rows.Cells(4).Value
                ultIdCliente = Rows.Cells(8).Value
                i += 1
            Next
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error en el PreparaOrden")
            tr.Rollback()
            cn.Close()
            Return False
        Finally
            Cursor = Cursors.Default
        End Try
    End Function
    Private Sub SumaTotal()
        Try
            FacturVenta.total = 0
            Dim datagrid As New DataGridViewRow
            For Each datagrid In Me.DataGridView1.SelectedRows
                FacturVenta.total += datagrid.Cells(3).Value
            Next
        Catch ex As Exception
            MsgBox(ex.Message + ex.GetBaseException.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnSelect_Click(sender As System.Object, e As System.EventArgs) Handles btnSelect.Click
        cmdSelectAll(DataGridView1)
        Activa_Controles()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            If MsgBox("Esta seguro de eliminar los items seleccionados.?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Responda") = MsgBoxResult.Yes Then
                If Eliminar_Items() Then
                    Carg_SegunChecken()
                End If
            End If
        End If
    End Sub
    Private Function Eliminar_Items() As Boolean
        Dim resul As Boolean = False
        Try
            Dim data As New DataGridViewRow
            Dim cmd As New ClassCargadorProducto()
            For Each data In DataGridView1.SelectedRows
                sql = "Delete v from Ventas as v "
                sql = sql & "Where v.idVenta = " & data.Cells(0).Value & " "
                resul = cmd.ExecuteComand(sql)
                If Not resul Then
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function

    Private Sub btnNoselect_Click(sender As Object, e As EventArgs) Handles btnNoselect.Click
        cmdNotSelect(DataGridView1)
        Activa_Controles()
    End Sub

    Private Sub Activa_Controles()
        If Not cargando Then
            If DataGridView1.SelectedRows.Count = 1 Then
                btnDelete.Enabled = True
                btnPrint.Enabled = True
                btnDetail.Enabled = True
                btnNotaVenta.Enabled = True
                btnFactura.Enabled = True
            ElseIf DataGridView1.SelectedRows.Count > 1 Then
                btnPrint.Enabled = False
                btnDetail.Enabled = False
                btnNotaVenta.Enabled = True
                btnFactura.Enabled = True
            Else
                btnDelete.Enabled = False
                btnPrint.Enabled = False
                btnDetail.Enabled = False
                btnNotaVenta.Enabled = False
                btnFactura.Enabled = False
            End If
        End If
    End Sub

    Private Function PruebaClintes() As Boolean
        Try
            Dim RowSelect As DataGridViewSelectedRowCollection = DataGridView1.SelectedRows
            Dim vuel As Integer = 0
            Total = 0

            Dim Rows As DataGridViewRow
            For Each Rows In RowSelect
                Total += Rows.Cells(4).Value
                If vuel > 0 Then
                    If Not idcliente = (Rows.Cells(8).Value) Then
                        Return False
                    End If
                End If
                OtroValor += (Rows.Cells(5).Value)
                idcliente = (Rows.Cells(8).Value)
                vuel += 1
            Next
            Return vuel
        Catch ex As Exception
            Return False
        End Try
    End Function


    Private Function TransfiereDatos(NameDocumento As String, cn As SqlConnection, tr As SqlTransaction) As Boolean
        Try
            Dim myidTerminal As ResponseTerminal = isTerminalHabil(UsuarioActivo.codUser, TerminalActivo.codTerminal)
            If myidTerminal.Success Then
                TerminalActivo.idCajaStado = myidTerminal.IDterminal
            Else
                Return False
            End If

            Using cmd As New SqlCommand("[dbo].[prcFacturaVenta]", cn, tr)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add("@idCajaStado", SqlDbType.Int)
                cmd.Parameters("@idCajaStado").Value = TerminalActivo.idCajaStado
                '..
                cmd.Parameters.Add("@NameDocumento", SqlDbType.VarChar, 255)
                cmd.Parameters("@NameDocumento").Value = NameDocumento
                '..
                cmd.Parameters.Add("@codTerminal", SqlDbType.Char, 8)
                cmd.Parameters("@codTerminal").Value = TerminalActivo.codTerminal
                '..
                cmd.Parameters.Add("@idCliente", SqlDbType.Int)
                cmd.Parameters("@idCliente").Value = Pagos.idCliente

                cmd.Parameters.Add("@idFormaPago", SqlDbType.Int)
                cmd.Parameters("@idFormaPago").Value = Pagos.idformaPago

                cmd.Parameters.Add("@idBanco", SqlDbType.Int)
                cmd.Parameters("@idBanco").Value = Pagos.idBancoCuenta

                cmd.Parameters.Add("@fech_desde", SqlDbType.DateTime)
                cmd.Parameters("@fech_desde").Value = FacturVenta.fechDesde

                cmd.Parameters.Add("@fech_hasta", SqlDbType.DateTime)
                cmd.Parameters("@fech_hasta").Value = FacturVenta.fechHasta

                cmd.Parameters.Add("@fech_Cobro", SqlDbType.Date)
                cmd.Parameters("@fech_Cobro").Value = Pagos.fech_Pago

                cmd.Parameters.Add("@num_Cobro", SqlDbType.VarChar, 12)
                cmd.Parameters("@num_Cobro").Value = Pagos.num_pago

                cmd.Parameters.Add("@num_cheq", SqlDbType.VarChar, 15)
                cmd.Parameters("@num_cheq").Value = Pagos.Num_Documento

                cmd.Parameters.Add("@codUser", SqlDbType.Char, 8)
                cmd.Parameters("@codUser").Value = UsuarioActivo.codUser

                cmd.Parameters.Add("@idBodega", SqlDbType.Int)
                cmd.Parameters("@idBodega").Value = TerminalActivo.idBodega

                cmd.Parameters.Add("@OtroValor", SqlDbType.Decimal, 18, 2)
                cmd.Parameters("@OtroValor").Value = FacturVenta.OtroValor

                If cmd.ExecuteNonQuery() >= 0 Then
                    Return True
                Else
                    Return False
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function

    Private Sub btnFactura_Click(sender As Object, e As EventArgs) Handles btnFactura.Click, btnNotaVenta.Click
        Try
            Dim cn As New SqlConnection(SimpleDataApp.Utility.GetConnectionString)
            AddHandler cn.InfoMessage, AddressOf cn_InfoMessage
            cn.Open()
            Dim transaction As SqlTransaction = cn.BeginTransaction()
            Try
                If Not PreparaOrden(cn, transaction) Then
                    Return
                End If
                Using fornew As New frmFormaPago()
                    With fornew
                        .flag = "Clientes"
                        .idcliente = Pagos.idCliente
                        .txtTotal.Text = FacturVenta.total
                        .txtPaga.Value = FacturVenta.total
                        .ValCaheqtxt.Text = FacturVenta.total
                        .txtCreditoActual.Text = FacturVenta.total
                        If .InitialityMenu() Then
                            .ShowDialog()
                            If Not (.DialogResult = DialogResult.OK) Then
                                transaction.Rollback()
                                cn.Close()
                                Return
                            End If
                        End If
                    End With
                End Using
                ' Aqui trasferimos los datos
                If Not TransfiereDatos(sender.Tag, cn, transaction) Then
                    transaction.Rollback()
                    cn.Close()
                    Return
                End If
                'si llegamos hasta aqui es por que fue todo bien
                transaction.Commit()
                cn.Close()
                sql = "Operación Exitosa" + vbNewLine
                sql = sql & "Desea imprimir el documento en TICKET...?"
                If (MsgBox(sql, MsgBoxStyle.Information + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Aviso")) = MsgBoxResult.Yes Then
                    Using frm As New frmUltDocument(frmUltDocument.stateLoad.PrintAll)
                    End Using
                End If
                ChekOrderList.SelectedIndex = 1
                ChekOrderList.SelectedIndex = 0
            Catch SqlEx As SqlException
                For Each myError In SqlEx.Errors
                    MsgBox(myError.Number & " - " & myError.Message, MsgBoxStyle.Exclamation, "Error")
                Next
                transaction.Rollback()
                cn.Close()
            End Try
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "error")
        End Try
    End Sub
    Public Sub cn_InfoMessage(ByVal sender As Object, ByVal e As System.Data.SqlClient.SqlInfoMessageEventArgs)
        Debug.WriteLine("info message event: " & e.Message)
    End Sub

    Private Sub frmList_OrdenVenta_Enter(sender As Object, e As EventArgs) Handles MyBase.Enter
        Activa_Controles()
    End Sub

    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        Activa_Controles()
    End Sub

    Private Sub DataGridView1_Click(sender As Object, e As EventArgs) Handles DataGridView1.Click
        Activa_Controles()
    End Sub

    Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click
        cmdCopy(DataGridView1)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub btnPrint_Click_1(sender As Object, e As EventArgs) Handles btnPrint.Click
        If DataGridView1.SelectedRows.Count = 1 Then
            Try
                Dim clien As String() = Split(DataGridView1.SelectedRows(0).Cells(1).Value)

                ClienteActivo.Ruc = clien(0)
                ClienteActivo.Nombres = clien(1) & " " & clien(2)
                PrintTicket_Proforma(DataGridView1.SelectedCells.Item(0).Value, "Proforma")

            Catch ex As Exception
                MsgBox(ex.Message & vbLet & ex.StackTrace, MsgBoxStyle.Critical, "Error")
            End Try
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MsgBox("!Ups, no disponible en esta versión")
    End Sub

End Class