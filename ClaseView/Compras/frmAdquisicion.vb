Imports System.Data.SqlClient
Imports CADsisVenta.DataSetSystemTableAdapters
Imports CADsisVenta.DataSetCompras
Imports CADsisVenta.DataSetComprasTableAdapters
Imports CADsisVenta.ClsSystem
Imports CADsisVenta.Data.Emuns.EnumSatateModule
Imports CADsisVenta.Helpers.FInicio

Public Class frmAdquisicion
    Public Enum state
        gasto_Personal = 1
        Negocio = 2
        View = 3
    End Enum
    Dim nonNumberEntered As Boolean
    Dim validado, typoCosto As Boolean
    Dim idPresent, id_proveedor, tipoIva, id_Form_Pago, NewPedido, id_Bodega As Integer
    Dim Cant, PUnt, Ptotals, IvaPor, IvaReal, Descuen As Double
    Private Const ivaSi = "Sí"
    Private Const ivaNo = "Nó"
    Protected Friend iniciado As state

    Private Sub frmAdquisicion_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If IsNothing(iniciado) Then
            MsgBox("No se ha determinado forma de iniciado ", MsgBoxStyle.Information, "Importante")
            Me.Close()
            Return
        End If
        Inicia_Catalogo()
        Carga_Bodega()
    End Sub
    Public Function Register_inTerminal() As Boolean
inicia:
        Try
            If Not isRegisterInTerminal(Dominio._HotName) Then
                sql = "Equipo no registrado en una estación" & vbNewLine
                sql = sql & "Solicite a su administrador.."
                MsgBox(sql, MsgBoxStyle.Exclamation, "Aviso")
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function
    Private Function Carga_Bodega() As Boolean
        Try

            Dim tapt As New TerminalTableAdapter
            Dim dt As New DataTable
            dt = tapt.GetDataByHostNameAndIdBodega(Dominio._HotName, TerminalActivo.idBodega)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    id_Bodega = dt.Rows(0)("idBodega")
                    'cargamos terminal
                    With TerminalActivo
                        .idTerminal = dt.Rows(0)("idTerminal")
                        .idBodega = id_Bodega
                        .codTerminal = dt.Rows(0)("CodTerminal")
                        .Dominio = Dominio._HotName
                    End With
                    '//cargamos informcion de la bodega
                    Dim tap_bod As New BodegasTableAdapter
                    dt = tap_bod.GetDataByIdBodega(TerminalActivo.idBodega)
                    If dt.Rows.Count > 0 Then
                        If IsNumeric(dt.Rows(0)("Resp2_idEmpleado")) Then
                            txtidAutorzCheque.Text = dt.Rows(0)("Resp2_idEmpleado")
                        Else
                            txtidAutorzCheque.Text = 0
                        End If
                        lblbodega.Text = "Adquiriendo productos para: " & vbNewLine & dt.Rows(0)("Nom_Bodega")
                        sql = dt.Rows(0)("Des_Bodega") & vbNewLine
                        sql = sql & "Dirección: " & dt.Rows(0)("Direc_Bodega") & vbNewLine
                        sql = sql & "Teléfono: " & dt.Rows(0)("Telef1_Bodega")
                        lblBodega2.Text = sql
                        BodegaLinkLabel.Text = lblbodega.Text + sql
                    End If
                    Return True
                End If
            End If
            Return False
        Catch ex As Exception
            MsgBox(ex.Message + " en el Carga_Bodega del " + Name, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function
    Private Sub Inicia_Catalogo()

        If Me.txtFalg.Text = 1 Or iniciado = state.gasto_Personal Then ' Para realizar Pedidos
            tabcontrol.SelectedIndex = 0
            tabcontrol.TabPages(0).Enabled = True
            tabcontrol.TabPages(1).Enabled = False
            tabcontrol.TabPages(2).Enabled = False
        ElseIf Me.txtFalg.Text = 2 Then 'Para comprar pediso
            tabcontrol.TabPages(0).Enabled = False
            tabcontrol.TabPages(1).Enabled = False
            tabcontrol.TabPages(2).Enabled = True
            tabcontrol.SelectedIndex = 2

            Carga_Tipo_Doc()
            Me.cmbTipoDocumento.SelectedIndex = -1
            cmbTipoDocumento.Text = "Selecione...."
        ElseIf Me.txtFalg.Text = 3 Then 'Ver Lista de Pedido
        End If
    End Sub

    Private Sub Carga_Item_Productos(ByVal idProveedor As Integer)
        Try
            cmbItemProducto.DataSource = Nothing
            Dim tap As New ProductosByProviderTableAdapter
            Dim dt As New ProductosByProviderDataTable
            tap.Fill(dt, idProveedor)
            With cmbItemProducto
                .DataSource = dt
                .ValueMember = "idPresentacion"
                .DisplayMember = "NomComercial"
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al cargar Proveedor")
        End Try
    End Sub


    Private Sub btnVerProducto_Click(sender As System.Object, e As System.EventArgs) Handles btnVerProducto.Click
        validaItem_Producto()
        If validado Then
            Dim MDI_AddProduct As New MDI_AddProdcutos
            With MDI_AddProduct
                .flag = "Modificar"
                .id_Proveedor = id_proveedor
                .id_Producto = cmbItemProducto.Items(cmbItemProducto.SelectedIndex).item("idProducto").ToString
                .ShowDialog()
                If .DialogResult = DialogResult.OK Then
                    Carga_Item_Productos(id_proveedor)
                    Me.cmbItemProducto.SelectedValue = .id_Presentation
                End If
            End With
            MDI_AddProduct = Nothing
        End If
    End Sub
    Private Sub validaItem_Producto()
        If cmbItemProducto.SelectedIndex < 0 Then
            MsgBox("Debe seleccionar un producto", MsgBoxStyle.Exclamation, "Alerta")
            cmbItemProducto.Focus()
            validado = False
        ElseIf cmbItemProducto.SelectedIndex >= 0 Then
            validado = True
            idPresent = cmbItemProducto.Items(cmbItemProducto.SelectedIndex).item("idPresentacion").ToString
        Else
            MsgBox("Debe seleccionar un producto", MsgBoxStyle.Exclamation, "Alerta")
            cmbItemProducto.Focus()
            validado = False
        End If
    End Sub
    Private Sub btnNuevoProducto_Click(sender As System.Object, e As System.EventArgs)

        With MDI_AddProdcutos
            .id_Proveedor = id_proveedor
            .id_Producto = 0
            .flag = "Agregar"
            .ShowDialog()
            If .DialogResult = DialogResult.OK Then
                Carga_Item_Productos(id_proveedor)
                Me.cmbItemProducto.SelectedValue = Selecion_producto(.id_Producto)
            End If
        End With

        MDI_AddProdcutos = Nothing
    End Sub

    Private Sub btnListProducto_Click(sender As System.Object, e As System.EventArgs) Handles btnListProducto.Click
        Try
            Using frmnew As New frmList_ProductoComprable()
                With frmnew
                    .idProveedor = id_proveedor
                    .State = _state.Admin
                    .StartPosition = FormStartPosition.CenterScreen
                    .ShowDialog()
                    If .DialogResult = DialogResult.OK Then
                        Carga_Item_Productos(id_proveedor)
                        cmbItemProducto.SelectedValue = .idPresent_Master
                    End If
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Function Selecion_producto(ByVal idProducto As Integer) As Integer
        Try
            Dim dt As DataTable = cmbItemProducto.DataSource
            Dim id As Integer = 0
            For i = 0 To dt.Rows.Count
                If dt.Rows(i)("idProducto") = idProducto Then
                    id = dt.Rows(i)("idPresentacion")
                    Exit For
                End If
            Next
            Return id
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error en el Selecion_producto del frmAdquisicion")
            Return 0
        End Try
    End Function

    Public Sub Carga_Declaracion()
        sql = "select * from Declaracion Order By Nom_declaracion"

        Try
            Using cnn = New SqlConnection(DomainSQLite.Setting.Configuration.ConectionString)
                cnn.Open()
                Using cmd As New SqlCommand(sql, cnn)
                    cmd.CommandType = CommandType.Text

                    Dim da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable

                    da.Fill(dt)

                    cmbDeclaracion.DataSource = Nothing
                    If dt.Rows.Count > 0 Then
                        With cmbDeclaracion
                            .DataSource = dt
                            .DisplayMember = "Nom_declaracion"
                            .ValueMember = "iddeclaracion"
                        End With
                        dt = Nothing
                    End If
                End Using
            End Using

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error en el: Carga_Declaracion ")
        End Try
    End Sub


    Private Sub btnAddListaProduc_Click(sender As System.Object, e As System.EventArgs) Handles btnAddListaProduc.Click
        If IsNumeric(txtCantidad.Text) Then
            If Agrega_Items() Then
                Calcula_Total()
                cmbItemProducto.Focus()
                AcceptButton = Nothing
                ListView1.MultiSelect = False
                ListView1.Items(ListView1.Items.Count - 1).EnsureVisible()
                ListView1.Items(ListView1.Items.Count - 1).Selected = True
                ListView1.MultiSelect = False
            End If
        End If
    End Sub

    Private Sub menuEmilinar_Click(sender As Object, e As System.EventArgs) Handles menuEmilinar.Click
        If Me.ListView1.SelectedItems.Count = 1 Then
            If MessageBox.Show("Realmente desea eliminar este registro", "responda", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3) = DialogResult.Yes Then
                Me.ListView1.Items(ListView1.SelectedIndices(0)).Remove()
            End If
        End If

    End Sub

    Private Sub menuCantidad_Click(sender As Object, e As System.EventArgs) Handles menuCantidad.Click
        If ListView1.SelectedIndices.Count = 1 Then
            Using formnew As New frmImputData
                With formnew
                    .Text = "Ingrese la CANTIDAD"
                    .txtFlag.Text = 1
                    .txtNumber.Value = CDec(Me.ListView1.Items(ListView1.SelectedIndices(0)).SubItems(2).Text)
                    .txtNumber.Visible = True
                    .ShowDialog()
                    If .DialogResult = DialogResult.OK Then
                        ListView1.Items(ListView1.SelectedIndices(0)).SubItems(CantidadClm.Index).Text = .txtNumber.Value
                        Calcula_Linea(ListView1.SelectedIndices(0))  'con el codigo 1 hago que el total se modifique 
                        Calcula_Total()
                    End If
                End With
            End Using
        End If
    End Sub



    Private Sub menuDescuento_Click(sender As Object, e As System.EventArgs) Handles menuDescuento.Click
        If ListView1.SelectedIndices.Count = 1 Then
            Using formnew As New frmImputData
                With formnew
                    .Text = "Ingrese el DESCUENTO"
                    .txtFlag.Text = 1
                    .txtNumber.Value = CDec(Me.ListView1.Items(ListView1.SelectedIndices(0)).SubItems(4).Text)
                    .txtNumber.Visible = True
                    .ShowDialog()
                    If .DialogResult = DialogResult.OK Then
                        ListView1.Items(ListView1.SelectedIndices(0)).SubItems(DescuentoClm.Index).Text = .txtNumber.Value
                        Calcula_Linea(ListView1.SelectedIndices(0))  'con el codigo 1 hago que el total se modifique 
                        Calcula_Total()
                    End If
                End With

            End Using
        End If

    End Sub
    Private Sub Cambiatotal()
        If ListView1.SelectedIndices.Count = 1 Then
            Using Form As New frmImputData()
                With Form
                    .Text = "Ingrese el PRECIO TOTAL"
                    .txtFlag.Text = 1
                    .txtNumber.Value = Me.ListView1.Items(ListView1.SelectedIndices(0)).SubItems(6).Text
                    .txtNumber.Visible = True
                    .ShowDialog()
                    If .DialogResult = DialogResult.OK Then
                        ListView1.Items(ListView1.SelectedIndices(0)).SubItems(SubTotalClm.Index).Text = .txtNumber.Value
                        Calcula_Linea(ListView1.SelectedIndices(0))  'CON LA OPCION 0 HACEMOS QUE EL COSTO TOTAL SEA la no se ba ha mover
                        Calcula_Total()
                    End If
                End With
            End Using

        End If
    End Sub


    Private Sub menuPtotal_Click(sender As Object, e As System.EventArgs) Handles menuPtotal.Click
        Cambiatotal()
    End Sub
    Private Function Agrega_Items() As Boolean
        Dim idProProveedor As Integer = 0

        'validamos informacion para poder agaregar
        Try
            'si no está selecionado un item salgo de esta funcion
            If cmbItemProducto.SelectedIndex < 0 Then
                MsgBox("Seleccione del listado", MsgBoxStyle.Information, "Aviso..")
                cmbItemProducto.Focus()
                Return False
            End If

            'cojo el codigo para validar costos 
            'idProProveedor = cmbItemProducto.Items(cmbItemProducto.SelectedIndex).Item("idProProveedor").ToString
            'exijo que ingrese cantidad de compra
            If Double.Parse(txtCantidad.Text) <= 0 Then
                MsgBox("Cantidad de producto comprado no puede ser cero", MsgBoxStyle.Information, "Aviso..")
                txtCantidad.Focus()
                Return False
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error en el frmAdquisicion en el sub Calcula_Cost_LineaAdd")
            Return False
        End Try

        Try
            'AGREGO EL REGISTRA AL LISTADO DE COMPRAS -------------------------------------------------------------------------------------------------------     -
            'carga % de IVA de la base en la CostoTotal 
            IvaPor = Convert.ToDecimal(cmbItemProducto.Items(cmbItemProducto.SelectedIndex)("ivaPorcentaje").ToString())
            IvaReal = 0
            '
            'agregamos itemas
            'El items es el fila
            Dim Filas As Integer = Me.ListView1.Items.Count


            'codigo de producto [0]
            Me.ListView1.Items.Add(cmbItemProducto.Items(cmbItemProducto.SelectedIndex)("codProducto").ToString)

            'Nombre e producto [1]
            ListView1.Items.Item(Filas).SubItems.Add(cmbItemProducto.Items(cmbItemProducto.SelectedIndex)("NomComercial").ToString)
            'cantidad  [2]
            Me.ListView1.Items.Item(Filas).SubItems.Add(txtCantidad.Text)
            Cant = txtCantidad.Text

            'precio unitario  [3] CostoTotal
            PUnt = cmbItemProducto.Items(cmbItemProducto.SelectedIndex)("CostoTotal").ToString
            If Not IvaCheckBox.Checked Then
                PUnt = FormatNumber(PUnt / (1 + IvaPor), 5)
            End If
            Me.ListView1.Items.Item(Filas).SubItems.Add(PUnt)

            'Descuento  [4]
            ListView1.Items.Item(Filas).SubItems.Add(0)

            'EL COSTO YA INCLUYE IVA
            Ptotals = FormatNumber(Cant * PUnt, 5)
            If IvaCheckBox.Checked Then
                'valor de iva  [5]
                ListView1.Items.Item(Filas).SubItems.Add(FormatNumber(Ptotals - (Ptotals / (1 + IvaPor)), 2))
                'P/Total   [6]
                ListView1.Items.Item(Filas).SubItems.Add(Ptotals)
                ListView1.Items.Item(Filas).SubItems.Add(ivaSi)
            Else
                If IvaPor > 0 Then
                    'agrego el valor de iva  [5]
                    IvaReal = Ptotals * IvaPor
                    ListView1.Items.Item(Filas).SubItems.Add(IvaReal)
                    'agrego el valor TOTAL de fila sin incluir el precio de iva [6]
                    ListView1.Items.Item(Filas).SubItems.Add(Ptotals)
                    ListView1.Items.Item(Filas).SubItems.Add(ivaNo)
                Else
                    'valor de iva  [5]
                    ListView1.Items.Item(Filas).SubItems.Add(0)
                    'valor total incluido iva [6]
                    ListView1.Items.Item(Filas).SubItems.Add(Cant * PUnt)
                    ListView1.Items.Item(Filas).SubItems.Add(ivaNo)
                End If
            End If
            'agrego el idPresentacion y lo oculto [8]
            ListView1.Items.Item(Filas).SubItems.Add(cmbItemProducto.Items(cmbItemProducto.SelectedIndex).item("idPresentacion").ToString())
            ListView1.Columns(8).Width = 0
            'agrego idProducto y lo oculto [9]
            ListView1.Items.Item(Filas).SubItems.Add(cmbItemProducto.Items(cmbItemProducto.SelectedIndex).item("idProducto").ToString())
            ListView1.Columns(9).Width = 0
            'Porcentaje de iva [10]
            ListView1.Items.Item(Filas).SubItems.Add(IvaPor)
            ListView1.Columns(10).Width = 0
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function
    Private Function Cargar_dtCosto(ByVal idProProveedor As Integer) As DataTable

        sql = "select pv.idPresentacion, p.IvaPorcentaje , pv.CostoTotal, pp.codProducto "
        sql = sql & "from Productos as p "
        sql = sql & "inner join ProductoPresentacion as pp on p.idProducto  = pp.idProducto "
        sql = sql & "inner join ProductoProveedor as pv on pv.idPresentacion = pp.idPresentacion "
        sql = sql & "inner join Proveedores as pr on pr.idProveedor = pv.idProveedor "
        sql = sql & "where pv.idProProveedor = " & idProProveedor & " "
        Try
            Dim cmd As New ClassCargadorProducto()
            Dim dt As DataTable = cmd.RetornaTabla(sql)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    Return dt
                End If
            End If
            Return Nothing
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error en el frmAdquisicion en el Cargar_dtCosto")
            Return Nothing
        End Try
    End Function

    Private Sub Calcula_Linea(ByVal items As Integer)
        'EL COSTO TOTAL ES LA QUE NO SE BA HA CAMBIAR
        Try
            With ListView1
                IvaPor = .Items(items).SubItems(IvaPorcentClm.Index).Text
                Ptotals = .Items(items).SubItems(SubTotalClm.Index).Text
                If Not DescueCheckBox.Checked Then
                    Ptotals -= .Items(items).SubItems(DescuentoClm.Index).Text
                End If
                If IvaCheckBox.Checked Then
                    .Items(items).SubItems(IvaClm.Index).Text = FormatNumber(Ptotals - (Ptotals / (1 + IvaPor)), 5)
                Else
                    .Items(items).SubItems(IvaClm.Index).Text = FormatNumber(Ptotals * IvaPor, 5)
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message + "en el Calculando_Precios_Linea", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub txtCantidad_GotFocus(sender As Object, e As System.EventArgs) Handles txtCantidad.GotFocus
        txtCantidad.Select(0, Len(txtCantidad.Text))
    End Sub

    Private Sub txtCantidad_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtCantidad.KeyPress
        Try
            If InStr(".0123456789", e.KeyChar) = 0 Then
                If Asc(e.KeyChar) <> 8 Then
                    e.Handled = True
                End If
            Else
                If Asc(e.KeyChar) = 46 Then
                    If Not (txtCantidad.SelectedText.Length = txtCantidad.Text.Length) Then
                        If InStr(txtCantidad.Text, ".") > 0 Then
                            e.Handled = True
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub txtCantidad_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtCantidad.TextChanged
        AcceptButton = Nothing
        If IsNumeric(txtCantidad.Text) Then
            AcceptButton = btnAddListaProduc
        End If
    End Sub

    Private Sub Calcula_Total()
        Dim Bas0 As Double = 0
        Dim Bas12 As Double = 0
        Dim Desc0 As Double = 0
        Dim Desc12 As Double = 0
        Dim SubLine As Double = 0
        IvaReal = 0
        Try
            Dim dt As New DataTable()
            Dim Column1 As New DataColumn()
            Column1.ColumnName = "SubTotal"
            Column1.DataType = GetType(Double)

            Dim Column2 As New DataColumn()
            Column2.ColumnName = "iva"
            Column2.DataType = GetType(Double)

            Dim Column3 As New DataColumn()
            Column3.ColumnName = "Desc"
            Column3.DataType = GetType(Double)

            Dim Column4 As New DataColumn()
            Column4.ColumnName = "ivaPorcent"
            Column4.DataType = GetType(Double)

            dt.Columns.AddRange({Column1, Column2, Column3, Column4})
            'llenamos los datos
            With ListView1
                For i = 0 To .Items.Count - 1
                    dt.Rows.Add()
                    dt.Rows(i)("SubTotal") = .Items(i).SubItems(SubTotalClm.Index).Text
                    dt.Rows(i)("iva") = .Items(i).SubItems(IvaClm.Index).Text
                    dt.Rows(i)("Desc") = .Items(i).SubItems(DescuentoClm.Index).Text
                    dt.Rows(i)("ivaPorcent") = .Items(i).SubItems(IvaPorcentClm.Index).Text
                Next
            End With
            'Sumo los datos padasos en la table
            For i = 0 To ListView1.Items.Count - 1
                IvaReal += dt.Rows(i)("iva")
                SubLine = dt.Rows(i)("SubTotal")
                If Not DescueCheckBox.Checked Then
                    SubLine -= dt.Rows(i)("Desc")
                End If
                'si ya esta incluido iva
                If IvaCheckBox.Checked And (dt.Rows(i)("iva") > 0) Then
                    Bas12 += FormatNumber(SubLine - dt.Rows(i)("iva"), 5)
                    Desc12 += dt.Rows(i)("Desc")
                ElseIf Not (IvaCheckBox.Checked) And (dt.Rows(i)("iva") > 0) Then
                    Bas12 += FormatNumber(dt.Rows(i)("iva") / dt.Rows(i)("ivaPorcent"), 5)
                    Desc12 += dt.Rows(i)("Desc")
                Else
                    Bas0 += SubLine
                    Desc0 += dt.Rows(i)("Desc")
                End If
            Next

            Bas0 = FormatNumber(Bas0, txtLugarDecimal.Text)
            Bas12 = FormatNumber(Bas12, txtLugarDecimal.Text)
            IvaReal = FormatNumber(IvaReal, txtLugarDecimal.Text)

            Bas0text.Text = FormatNumber(Bas0, txtLugarDecimal.Text)
            Bas12text.Text = FormatNumber(Bas12, txtLugarDecimal.Text)
            TotalBasText.Text = FormatNumber(Bas0 + Bas12, txtLugarDecimal.Text)
            ' en los detalles
            DescBase0Text.Text = "[" & Bas0 + Desc0 & "] - [Desc:" & Desc0 & "]"
            DescBase12Text.Text = "[" & Bas12 + Desc12 & "] - [Desc:" & Desc12 & "]"
            TotalBase.Text = "[" & Bas0 + Bas12 + Desc0 + Desc12 & "] - [Desc:" & Desc0 + Desc12 & "]"
            '-----total
            IvaText.Text = FormatNumber(IvaReal, txtLugarDecimal.Text)
            TotalPediText.Text = FormatNumber(Bas0 + Bas12 + IvaReal, txtLugarDecimal.Text)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Function Guarda_Pedido() As Boolean
        Try
            If Not (Guarda_PedidoTmp()) Then 'si no puedo guardar detalle salgo
                Return False
            End If
            Using cnn = New SqlConnection(DomainSQLite.Setting.Configuration.ConectionString)
                cnn.Open()
                Using cmd = New SqlCommand("[dbo].[prcOrdenCompra]", cnn)
                    cmd.CommandType = CommandType.StoredProcedure
                    'bodega
                    cmd.Parameters.Add(New SqlParameter("@idBodega", SqlDbType.Int))
                    cmd.Parameters("@idBodega").Value = FacturCompra.idBodega  'esto de obtiene en el load
                    'fecha de pedido
                    cmd.Parameters.Add(New SqlParameter("@FechaPedido", SqlDbType.Date))
                    cmd.Parameters("@FechaPedido").Value = FechaPedidoDatatime.Value
                    'id proveedor
                    cmd.Parameters.Add(New SqlParameter("@IdProveedor", SqlDbType.Int))
                    cmd.Parameters("@IdProveedor").Value = FacturCompra.idProveedor
                    'codigo de uduario
                    cmd.Parameters.Add(New SqlParameter("@codUser", SqlDbType.Char, 8))
                    cmd.Parameters("@codUser").Value = UsuarioActivo.codUser
                    'codigo de terminal
                    cmd.Parameters.Add(New SqlParameter("@codTerminal", SqlDbType.Char, 8))
                    cmd.Parameters("@codTerminal").Value = TerminalActivo.codTerminal
                    'salida de [id] automumerico
                    cmd.Parameters.Add(New SqlParameter("@idPedido", SqlDbType.Int))
                    cmd.Parameters("@idPedido").Direction = ParameterDirection.Output
                    If cmd.ExecuteNonQuery Then
                        FacturCompra.idPedido = 0
                        txtOrden.Text = CInt(cmd.Parameters("@idPedido").Value)
                        FacturCompra.idPedido = txtOrden.Text
                        Return True
                    End If
                End Using
            End Using

            Return False
        Catch ex As Exception
            MsgBox("Guarda_Pedido: " & ex.Message, MsgBoxStyle.Critical, "Error: frmAdquisicion en el Guarda_Pedido")
            Return False
        End Try
    End Function
    Private Function Guarda_PedidoTmp() As Boolean
        Try
            Dim i As Integer = 0
            Dim cmd As New ClassCargadorProducto()
            '--Elimino los dato temporles
            sql = "Delete [tmp].[PedidosTmp] where codUser ='" & UsuarioActivo.codUser & "' and codTerminal = '" & TerminalActivo.codTerminal & "' "
            cmd.ExecuteComand(sql)

            For i = 0 To Me.ListView1.Items.Count - 1
                IvaReal = RedondearSi(Me.ListView1.Items(i).SubItems(5).Text, 5)
                Descuen = RedondearSi(Me.ListView1.Items(i).SubItems(4).Text, 5)
                Ptotals = RedondearSi(Me.ListView1.Items(i).SubItems(6).Text, 5)

                If IvaCheckBox.Checked Then
                    'SI EL COSTO DE CADA ITEM YA INCLUYE TODO
                    sql = "insert into [tmp].[PedidosTmp] (codUser,codTerminal, idPresent, cant, Descuento, iva, SubTotal, Total) "
                    sql = sql & "Values ('" & UsuarioActivo.codUser & "','" & TerminalActivo.codTerminal & "', " & ListView1.Items(i).SubItems(IdPresentClm.Index).Text & ", " & ListView1.Items(i).SubItems(CantidadClm.Index).Text & ", "
                    sql = sql & "" & Descuen & ", " & IvaReal & ", "
                    sql = sql & "" & (Ptotals + Descuen) - IvaReal & ", " & Ptotals & ") "
                Else
                    sql = "insert into [tmp].[PedidosTmp] (codUser,codTerminal, idPresent, cant, Descuento, iva, SubTotal, Total) "
                    sql = sql & "Values ('" & UsuarioActivo.codUser & "','" & TerminalActivo.codTerminal & "', " & ListView1.Items(i).SubItems(IdPresentClm.Index).Text & ", " & ListView1.Items(i).SubItems(CantidadClm.Index).Text & ", "
                    sql = sql & "" & Descuen & ", " & IvaReal & ", "
                    sql = sql & "" & ListView1.Items(i).SubItems(6).Text & ", " & (Ptotals + IvaReal) - Descuen & ") "
                End If
                Try
                    If Not cmd.ExecuteComand(sql) Then
                        Return False
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Error: frmAdquisicion en el Guarda_Pedido_Detalle")
                    Return False
                End Try
            Next
            If i > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox("Guarda_PedidoTmp: " & ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function


    Private Sub btnGuardaPedido_Click(sender As System.Object, e As System.EventArgs) Handles btnGuardaPedido.Click
        Try
            If ListView1.Items.Count > 0 Then
                Me.Cursor = Cursors.WaitCursor
                FacturCompra.idProveedor = id_proveedor
                FacturCompra.idBodega = id_Bodega
                If Guarda_Pedido() Then
                    tabcontrol.TabPages(0).Enabled = False
                    tabcontrol.TabPages(1).Enabled = False
                    tabcontrol.SelectedIndex = 2
                    tabcontrol.TabPages(2).Enabled = True
                    Carga_Tipo_Doc()
                    If Not (iniciado = state.gasto_Personal) Then
                        Carga_Declaracion()
                        cmbDeclaracion.SelectedValue = 1
                        Carga_Tipo_Consumo()
                        cmbItmTipconsumo.SelectedValue = 1
                    End If
                    dtFechaPedido.Value = Me.FechaPedidoDatatime.Value
                    Me.dtFechaCompra.Value = Me.dtFechaPedido.Value
                    Me.txtNumDoc.Text = ""
                    Me.txtNumDoc.Focus()
                End If
                Me.Cursor = Cursors.Default
            Else
                MsgBox("No existe items para guardar..", MsgBoxStyle.Information, "Aviso")
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Public Sub Carga_Tipo_Consumo()

        Try
            sql = "Select * from Consumo "

            Using cnn = New SqlConnection(DomainSQLite.Setting.Configuration.ConectionString)
                cnn.Open()
                Dim cmd As New SqlCommand(sql, cnn)
                cmd.CommandType = CommandType.Text

                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable

                da.Fill(dt)
                cmbItmTipconsumo.DataSource = Nothing
                If dt.Rows.Count > 0 Then
                    With cmbItmTipconsumo
                        .DataSource = dt
                        .ValueMember = "idconsumo"
                        .DisplayMember = "Nom_Consumo"
                    End With
                    dt = Nothing
                    cmbItmTipconsumo.SelectedValue = 1
                End If
            End Using


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error: frmAdquisicion en el  cargar Carga_Tipo_Consumo")
        End Try

    End Sub

    Private Sub cbxRedondSin_Click(sender As Object, e As System.EventArgs) Handles cbxRedondSin.Click
        cbxRedondSin.Checked = True
        cbxRedondCon.Checked = False
    End Sub

    Private Sub cbxRedondCon_Click(sender As Object, e As System.EventArgs) Handles cbxRedondCon.Click
        cbxRedondSin.Checked = False
        cbxRedondCon.Checked = True
    End Sub

    Private Sub btnCalTotal_Click(sender As System.Object, e As System.EventArgs) Handles CalculaTotalBtn.Click
        Calcula_Total()
    End Sub

    Private Sub txtLugarDecimal_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtLugarDecimal.TextChanged
        Dim i As Long

        ' elimino espacios en blanco al principio
        Me.txtLugarDecimal.Text = Trim(Me.txtLugarDecimal.Text)
        ' me aseguro de que la IP solo contiene números y puntos y en caso contrario salgo
        For i = 1 To Len(Me.txtLugarDecimal.Text)
            If Not IsNumeric(Mid(Me.txtLugarDecimal.Text, i, 1)) Then
                If Mid(Me.txtLugarDecimal.Text, i, 1) <> "." Then
                    MsgBox("Ingrese solo numeros enteros" & Chr(34) & Me.txtLugarDecimal.Text & Chr(34), vbOKOnly + vbCritical, "ATENCION")
                    Me.txtLugarDecimal.Text = 2
                    Me.txtLugarDecimal.Focus()
                    Exit Sub
                End If
            End If
        Next i
    End Sub

    Private Sub btnCancelCompra_Click(sender As System.Object, e As System.EventArgs) Handles btnCancelCompra.Click
        tabPago.Parent = Nothing
        tabDocumento.Parent = tabcontrol
        tabItem.Parent = Nothing
    End Sub

    Private Sub btnAcepProveedor_Click(sender As System.Object, e As System.EventArgs) Handles btnAcepProveedor.Click
        If id_proveedor > 0 Then
            nonNumberEntered = True  'para decir a los controles que recien estoy cargando datos
            sql = FechaPedidoDatatime.Value.ToString("D")
            If MsgBox("La fecha es correcta:  " & sql, MsgBoxStyle.Information + MsgBoxStyle.YesNo, "Responda") = MsgBoxResult.No Then Exit Sub

            tabcontrol.SelectedIndex = 1
            tabcontrol.TabPages(0).Enabled = False
            tabcontrol.TabPages(1).Enabled = True
            tabcontrol.TabPages(2).Enabled = False
            Carga_Item_Productos(id_proveedor)
            cmbItemProducto.Focus()
            nonNumberEntered = False
            sql = txtProveedorDetail.Text & vbNewLine
            sql = sql & "Fecha :" & FechaPedidoDatatime.Text
            lblDetalleItema.Text = sql
        Else
            MsgBox("Seleccione un proveedor [F4]", MsgBoxStyle.Information, "Aviso")
        End If

    End Sub



    Private Sub btnGuardarCompra_Click(sender As System.Object, e As System.EventArgs) Handles btnGuardarCompra.Click
        Try
            Dim idFactur As Integer = 0
            Cursor = Cursors.WaitCursor
            FacturCompra.idBodega = id_Bodega
            FacturCompra.idPedido = Integer.Parse(txtOrden.Text)
            FacturCompra.Tipo_Doc = cmbTipoDocumento.SelectedValue
            FacturCompra.Num_Doc = txtNumDoc.Text
            FacturCompra.idConsumo = cmbItmTipconsumo.SelectedValue
            FacturCompra.idDeclaracion = cmbDeclaracion.SelectedValue
            FacturCompra.FechaComra = dtFechaCompra.Value
            'comprobamos si podemos grabar compra
            idFactur = Guarda_compraActual(FacturCompra.idPedido)
            If idFactur > 0 Then
                MsgBox("Compra Guardada Exitosamente Número de transferencia: " & idFactur, MsgBoxStyle.Information, "Aviso")
                If Me.txtFalg.Text = "2" Then  'Cuando vengo desde listado de pedidos frmListPedido
                    Me.Close()
                    Exit Sub
                End If
                txtIdFormaPago.Text = 0
                txtDetailpago.Text = ""
                ListView1.Items.Clear()
                Inicia_Catalogo()
                Me.FechaPedidoDatatime.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Carga_Tipo_Doc()

        Try
            sql = "Select * from [STM].[TypoDocumento] "
            Using cnn = New SqlConnection(DomainSQLite.Setting.Configuration.ConectionString)
                cnn.Open()
                Dim cmd As New SqlCommand(sql, cnn)
                cmd.CommandType = CommandType.Text

                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable

                da.Fill(dt)
                If dt.Rows.Count > 0 Then
                    With cmbTipoDocumento
                        .DataSource = dt
                        .ValueMember = "idTypoDocu"
                        .DisplayMember = "Nom_Docu"
                    End With
                    dt = Nothing
                End If
            End Using


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error: frmAdquisicion en el  cargar Carga_Tipo_Doc")
        End Try

    End Sub


    Private Sub cmbTipoDocumento_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmbTipoDocumento.Validating

        If DirectCast(sender, ComboBox).SelectedIndex < 0 Then
            Me.ErrorIcono.SetError(sender, "Seleccione uno de la lista deplegable")
        Else
            Me.ErrorIcono.SetError(sender, "")
        End If
    End Sub

    Private Sub txtNumDoc_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtNumDoc.Validating
        If DirectCast(sender, TextBox).Text.Length = 0 Then
            Me.ErrorIcono.SetError(sender, "Ingrese el numero de Factura o puede buscar un numero con el comando alternativo para recibos y notas de venta")
        Else
            Me.ErrorIcono.SetError(sender, "")
        End If
    End Sub

    Private Sub dtFechaCompra_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles dtFechaCompra.Validating, dtFechaPedido.Validating

        If FormatDateTime(DirectCast(sender, DateTimePicker).Value, DateFormat.ShortDate) < FormatDateTime(Me.dtFechaPedido.Value, DateFormat.ShortDate) Then
            Me.ErrorIcono.SetError(sender, "La fecha de compra no puede ser menor que la de PEDIDO")
        Else
            Me.ErrorIcono.SetError(sender, "")
        End If
    End Sub
    Private Sub ListFormaPago_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
        If Me.txtIdFormaPago.Text = 0 Then
            Me.ErrorIcono.SetError(sender, "Click derecho y Seleccione una forma de pagar la compra")
        Else
            Me.ErrorIcono.SetError(sender, "")
        End If
    End Sub
    Private Sub txtIdPedido_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtOrden.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.ErrorIcono.SetError(sender, "")
        Else
            Me.ErrorIcono.SetError(sender, "No existe el numedo de pedido")
        End If

    End Sub
    Private Sub btnListProveedor_Click(sender As System.Object, e As System.EventArgs) Handles btnListProveedor.Click
        Try
            Using frmListProveedor As New frmList_Proveedores(stateLoad.Dialogo, stateClient.User)
                With frmListProveedor
                    .btnOk.Visible = True
                    .txtbuscar.Focus()
                    .ShowDialog()
                    If .DialogResult = DialogResult.OK Then
                        Me.Text = String.Format("Compra de productos: {0}", .dataListado.SelectedCells.Item(2).Value)
                        id_proveedor = .dataListado.SelectedCells.Item(0).Value
                        sql = "Proveedor :" & .dataListado.SelectedCells.Item(2).Value & vbNewLine
                        sql = sql & "Ruc :" & .dataListado.SelectedCells.Item(1).Value & vbNewLine
                        sql = sql & "Representante :" & .dataListado.SelectedCells.Item(3).Value & vbNewLine
                        sql = sql & "Teléfono:" & .dataListado.SelectedCells.Item(4).Value & vbNewLine
                        txtProveedorDetail.Text = sql
                        ListView1.Items.Clear()
                        IvaCheckBox.Checked = .dataListado.SelectedCells.Item(5).Value
                    End If
                    Me.FechaPedidoDatatime.Focus()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error")
        End Try

    End Sub

    Private Sub DeleteButton_Click(sender As Object, e As EventArgs) Handles DeleteButton.Click
        If ListView1.Items.Count = 0 Then
            MsgBox("Seleccione uno de la lista", MsgBoxStyle.Information, "Aviso")
            Return
        End If
        If (MsgBox("Está seguro de eliminar..", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Responda..")) = DialogResult.Yes Then
            Dim coleecionItem As ListView.SelectedListViewItemCollection =
                 ListView1.SelectedItems
            Dim item As ListViewItem
            For Each item In coleecionItem
                item.Remove()
            Next
            Calcula_Total()
        End If
    End Sub

    Private Sub SelectAllButton_Click(sender As Object, e As EventArgs) Handles SelectAllButton.Click
        cmdSelectAll(ListView1)
        ListView1.Focus()
    End Sub

    Private Sub CopyButton_Click(sender As Object, e As EventArgs) Handles CopyButton.Click
        cmdCopy(ListView1)
        ListView1.Focus()
    End Sub
    Private Sub CantiEddButton_Click(sender As Object, e As EventArgs) Handles CantiEddButton.Click
        menuCantidad.PerformClick()
        ListView1.Focus()
    End Sub

    Private Sub TotalEditButton_Click(sender As Object, e As EventArgs) Handles TotalEditButton.Click
        menuPtotal.PerformClick()
        ListView1.Focus()
    End Sub

    Private Sub MoveDowButton_Click(sender As Object, e As EventArgs) Handles MoveDowButton.Click
        If ListView1.Items.Count = 0 Then Return
        If ListView1.SelectedItems.Count = 0 Then Return

        Dim item As ListViewItem = ListView1.SelectedItems(0)
        If item.Index = (ListView1.Items.Count - 1) Then
            ListView1.Focus()
            Return
        End If
        Dim pos As Integer = item.Index + 1
        ListView1.Items.RemoveAt(item.Index)
        ListView1.Items.Insert(pos, item)
        ListView1.Focus()
        ListView1.Items(pos).Selected = True
    End Sub

    Private Sub IvaCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles IvaCheckBox.CheckedChanged
        '[7] columna
        If IvaCheckBox.Checked Then
            sql = ivaSi
        Else
            sql = ivaNo
        End If
        With ListView1
            For i = 0 To .Items.Count - 1
                .Items(i).SubItems(IvaIncluyeClm.Index).Text = sql
                Calcula_Linea(i)
            Next
            Calcula_Total()
        End With
    End Sub

    Private Sub PunitarioEddButton_Click(sender As Object, e As EventArgs) Handles PunitarioEddButton.Click
        Using fornew As New frmEditCompra()
            With fornew
                ._flag = frmEditCompra.Estado.Unitario
                .list = ListView1
                .ivaCheckBox.Checked = IvaCheckBox.Checked
                .descCheckBox.Checked = DescueCheckBox.Checked
                .ShowDialog()
                If .DialogResult = DialogResult.OK Then
                    Copio_DatosdelLidtado(.DataGridView1)
                End If
            End With
        End Using
    End Sub
    Private Sub cbxRedondCon_CheckedChanged(sender As Object, e As EventArgs) Handles cbxRedondCon.CheckedChanged
        Calcula_Total()
    End Sub

    Private Sub cbxRedondSin_CheckedChanged(sender As Object, e As EventArgs) Handles cbxRedondSin.CheckedChanged
        Calcula_Total()
    End Sub

    Private Sub DescueCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles DescueCheckBox.CheckedChanged
        With ListView1
            For i = 0 To .Items.Count - 1
                Calcula_Linea(i)
            Next
            Calcula_Total()
        End With
    End Sub

    Private Sub frmAdquisicion_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If ListView1.Items.Count > 0 Then
            If Not (MsgBox("Existe información sin guardara...!" + vbNewLine + "Desea salir de todas maneras..?",
                      MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = DialogResult.Yes) Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub TotalEdidListButton_Click(sender As Object, e As EventArgs) Handles TotalEdidListButton.Click
        Using fornew As New frmEditCompra()
            With fornew
                ._flag = frmEditCompra.Estado.Total
                .list = ListView1
                .ivaCheckBox.Checked = IvaCheckBox.Checked
                .descCheckBox.Checked = DescueCheckBox.Checked
                .ShowDialog()
                If .DialogResult = DialogResult.OK Then
                    Copio_DatosdelLidtado(fornew.DataGridView1)
                End If
            End With
        End Using
    End Sub
    Private Sub Copio_DatosdelLidtado(dt As DataGridView)
        Try
            For i = 0 To dt.RowCount - 1
                With ListView1
                    .Items(i).SubItems(PUnitarioClm.Index).Text = dt.Rows(i).Cells(1).Value
                    .Items(i).SubItems(SubTotalClm.Index).Text = dt.Rows(i).Cells(2).Value
                    Calcula_Linea(i)
                End With
            Next
            Calcula_Total()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub AtrasButton_Click(sender As Object, e As EventArgs) Handles AtrasButton.Click
        sql = "Se puede cambiar la fecha de compra." & vbNewLine & "Pero sí cambia el proveedor se perderá los artículos ingresados.." & vbNewLine
        sql = sql & vbNewLine & "¿De todas formas desea regresar..?"
        If MsgBox(sql, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Responda") Then
            tabcontrol.TabPages(0).Enabled = True
            tabcontrol.SelectedIndex = 0
            tabcontrol.TabPages(1).Enabled = False
            tabcontrol.TabPages(2).Enabled = False
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub ActualizarButton_Click(sender As Object, e As EventArgs) Handles ActualizarButton.Click

        Dim idProduct As String = String.Empty

        For Each item As ListViewItem In ListView1.Items
            idProduct = item.SubItems(9).Text

            For Each row As DataRowView In cmbItemProducto.Items
                If row.Item(5).ToString.Equals(idProduct) Then
                    item.SubItems(1).Text = row.Item(2).ToString
                    item.SubItems(10).Text = row.Item(4).ToString
                    Calcula_Linea(item.Index)
                    Exit For
                End If
            Next

            '0 codigo de presentacion
            '1 unit price
            '2 name product
            '3 cod product
            '4 ive percent
            '5 codigo de producto
        Next
        Calcula_Total()
    End Sub

    Private Sub AtrasButtonFactur_Click(sender As Object, e As EventArgs) Handles AtrasButtonFactur.Click
        tabcontrol.TabPages(0).Enabled = False
        tabcontrol.TabPages(1).Enabled = True
        tabcontrol.SelectedIndex = 1
        tabcontrol.TabPages(2).Enabled = False
    End Sub

    Private Sub BodegaLinkLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles BodegaLinkLabel.LinkClicked
        MsgBox("No disponible en esta versión")
        Return


        Using newfor As New frmList_Bodega()
            With newfor
                .ShowDialog()
                If .DialogResult = DialogResult.OK Then
                    id_Bodega = .DataGridView1.SelectedCells(0).Value
                    sql = "Adquiriendo productos para: " & vbNewLine
                    sql = sql & .DataGridView1.SelectedCells(1).Value & vbNewLine
                    sql = sql & .DataGridView1.SelectedCells(2).Value & vbNewLine
                    sql = sql & "Teléfono" & .DataGridView1.SelectedCells(4).Value & vbNewLine
                    BodegaLinkLabel.Text = sql
                End If
            End With
        End Using
    End Sub

    Private Sub DescuenEddButton_Click(sender As Object, e As EventArgs) Handles DescuenEddButton.Click
        menuDescuento.PerformClick()
        ListView1.Focus()
    End Sub

    Private Sub MoveUPButton_Click(sender As Object, e As EventArgs) Handles MoveUPButton.Click
        If ListView1.Items.Count = 0 Then Return
        If ListView1.SelectedItems.Count = 0 Then Return
        Dim item As ListViewItem = ListView1.SelectedItems(0)
        If item.Index = 0 Then
            ListView1.Focus()
            Return
        End If
        Dim pos As Integer = item.Index - 1
        ListView1.Items.RemoveAt(item.Index)
        ListView1.Items.Insert(pos, item)
        ListView1.Focus()
        ListView1.Items(pos).Selected = True
    End Sub


    Private Sub btnAlternativo_Click(sender As Object, e As EventArgs) Handles btnAlternativo.Click
        Me.txtNumDoc.Text = Busca_New_NumDoc()
        Me.cmbTipoDocumento.SelectedValue = 3
    End Sub
    Private Sub cmbTipoDocumento_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTipoDocumento.SelectedIndexChanged
        If cmbTipoDocumento.SelectedIndex >= 0 Then
            If cmbTipoDocumento.DisplayMember.Length > 0 Then
                NumDocumenLabel.Text = "Número de  " & cmbTipoDocumento.Text
            End If
        End If
    End Sub
    Private Sub ListView1_KeyDown(sender As Object, e As KeyEventArgs) Handles ListView1.KeyDown
    End Sub

    Private Sub TabControl1_KeyDown(sender As Object, e As KeyEventArgs) Handles tabcontrol.KeyDown
        If Me.tabcontrol.TabPages(tabcontrol.SelectedIndex).Name = "tabItem" Then
            If e.KeyCode = Keys.Up Or e.KeyCode = Keys.Down Then
                Me.ListView1.Focus()
            ElseIf e.KeyCode = Keys.F12 Then
                btnGuardaPedido.PerformClick()
            ElseIf e.KeyCode = Keys.F1 Then
                Cambiatotal()
            End If
        ElseIf Me.tabcontrol.TabPages(tabcontrol.SelectedIndex).Name = "tabPago" Then
            If e.KeyCode = Keys.F12 Then
                btnGuardarCompra.PerformClick()
            ElseIf e.KeyCode = Keys.F1 Then
                btnAlternativo.PerformClick()
            ElseIf e.KeyCode = Keys.F4 Then
                btnFormaPago.PerformClick()
            End If
        ElseIf Me.tabcontrol.TabPages(tabcontrol.SelectedIndex).Name = "tabDocumento" Then
            If e.KeyCode = Keys.F12 Then
                btnAcepProveedor.PerformClick()
            ElseIf e.KeyCode = Keys.F4 Then
                btnListProveedor.PerformClick()
            End If
        End If
    End Sub
    Private Sub btnFormaPago_Click(sender As Object, e As EventArgs) Handles btnFormaPago.Click
        Try
            Using newfor As New frmFormaPago()
                With newfor
                    .id_proveedor = id_proveedor
                    .flag = "Compras"
                    .txtTotal.Text = TotalPediText.Text
                    .txtPaga.Maximum = TotalPediText.Text
                    .txtPaga.Value = TotalPediText.Text
                    .idcliente = txtidAutorzCheque.Text
                    .ValCaheqtxt.Text = TotalPediText.Text
                    If .InitialityMenu() Then
                        .StartPosition = FormStartPosition.CenterParent
                        .ShowDialog()
                        If .DialogResult = DialogResult.OK Then
                            txtIdFormaPago.Text = .idFormaPago
                            txtDetailpago.Text = .MsgBoxRetur
                        End If
                    Else
                        MsgBox("Menú fuera de control no se pudo controlar", MsgBoxStyle.Information, "Alerta")
                    End If
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub txtIdFormaPago_TextChanged(sender As Object, e As EventArgs) Handles txtIdFormaPago.TextChanged, txtNumDoc.TextChanged
        If Integer.Parse(txtIdFormaPago.Text) > 0 And txtNumDoc.TextLength > 0 Then
            btnGuardarCompra.Enabled = True
        Else
            btnGuardarCompra.Enabled = False
        End If
    End Sub

    Private Sub cmbItemProducto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbItemProducto.SelectedIndexChanged
        If cmbItemProducto.DisplayMember.Length > 0 Then
            If cmbItemProducto.SelectedIndex >= 0 Then
                txtCantidad.Focus()
            End If
        End If
    End Sub
End Class