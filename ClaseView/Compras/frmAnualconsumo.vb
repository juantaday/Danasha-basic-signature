Imports System.Data.SqlClient
Imports CADsisVenta.Data.Emuns.EnumSatateModule
Imports CADsisVenta.Helpers.FInicio

Public Class frmAnualconsumo
    Dim nonNumberEntered As Boolean
    Dim validado, typoCosto As Boolean
    Dim Item_Select As Integer = -1
    Dim idPresent, id_proveedor, tipoIva, id_Form_Pago, NewPedido, OrderDetail As Integer
    Dim Cant, PUnt, Ptotals, IvaPor, IvaReal, Descuen As Double
    Dim dtCosto As New DataTable



    Private Sub frmAnualconsumo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.dtFechaCompra.Value = Now()
        Carga_Combo_Proveedor()
        Carga_Declaracion()
        Carga_Tipo_Doc()
        Carga_Tipo_Consumo()
        dtFechaCompra.Focus()
    End Sub

    Private Function Carga_Declaracion()
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
                    Return True
                End Using
            End Using




        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error en el: Carga_Declaracion ")
            Return False
        End Try
    End Function

    Private Sub Carga_Item_Productos(ByVal idProveedor As Integer)



        Try

            sql = "SELECT        TOP (100) PERCENT pp.idPresentacion, p.Nom_Comercial + ' (' + pum.Medida + ' de ' + CAST(pp.Cant_Present AS varchar(25)) + ' ' + pm.Medida + ')' AS Unidad, "
            sql = sql & "p.IvaPorcentaje, ppr.CostoTotal, ppr.idProProveedor, pp.codProducto, pp.Cant_Present "

            sql = sql & "FROM            dbo.ProductoPresentacion AS pp INNER JOIN "
            sql = sql & "dbo.Productos AS p ON pp.idProducto = p.idProducto AND pp.idProUndMed = p.Deft_idPresenCompra INNER JOIN "
            sql = sql & " dbo.ProductoProveedor AS ppr ON pp.idPresentacion = ppr.idPresentacion INNER JOIN "
            sql = sql & " dbo.ProductoUndMedida AS pm ON pp.idProUndReferen = pm.idProUndMed INNER JOIN "
            sql = sql & " dbo.ProductoUndMedida AS pum ON pp.idProUndMed = pum.idProUndMed "

            sql = sql & "WHERE(ppr.idProveedor =" & idProveedor & ") "
            sql = sql & "ORDER BY p.Nom_Comercial"

            Using cnn = New SqlConnection(DomainSQLite.Setting.Configuration.ConectionString)
                cnn.Open()

                Using cmd As New SqlCommand(sql, cnn)
                    cmd.CommandType = CommandType.Text

                    Dim da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable

                    da.Fill(dt)
                    cmbItemProducto.DataSource = Nothing
                    If dt.Rows.Count > 0 Then
                        With cmbItemProducto
                            .DataSource = dt
                            .DisplayMember = "Unidad"
                            .ValueMember = "idPresentacion"
                        End With
                        dt = Nothing
                    End If
                End Using

            End Using



        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al cargar Proveedor")
        End Try

    End Sub


    Private Sub Carga_Combo_Proveedor()
        sql = "Select * from Proveedores ORDER BY [Razon_social]"



        Try

            Using cnn = New SqlConnection(DomainSQLite.Setting.Configuration.ConectionString)
                cnn.Open()
                Using cmd As New SqlCommand(sql, cnn)
                    cmd.CommandType = CommandType.Text

                    Dim da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable

                    da.Fill(dt)
                    cmbProveedor.DataSource = Nothing
                    If dt.Rows.Count > 0 Then
                        With cmbProveedor
                            .DataSource = dt
                            .ValueMember = "idProveedor"
                            .DisplayMember = "Razon_social"
                        End With
                    End If
                End Using
            End Using





        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error en frmAdquisicion en el Carga_Combo_Proveedor")
        End Try
    End Sub

    Private Sub btnAddCabConsumo_Click(sender As Object, e As EventArgs) Handles btnAddCabConsumo.Click
        'comienzo ha balidar datos para dar paso
        Me.grupDetail.Enabled = False
        If CInt(cmbItmTipconsumo.SelectedIndex) < 0 Then
            MsgBox("Selecione uno de la lista despegable", MsgBoxStyle.Exclamation, "Aviso")
            cmbItmTipconsumo.Focus()
            Exit Sub
        End If

        If CInt(cmbProveedor.SelectedIndex) < 0 Then
            MsgBox("Selecione uno de la lista despegable", MsgBoxStyle.Exclamation, "Aviso")
            cmbProveedor.Focus()
            Exit Sub
        End If

        If Len(Me.txtNumDoc.Text) = 0 Then
            MsgBox("Ingrese el numero de factura", MsgBoxStyle.Exclamation, "Aviso")
            txtNumDoc.Focus()
            Exit Sub
        End If


        id_proveedor = Me.cmbProveedor.SelectedValue
        Me.grupDetail.Enabled = True
        Carga_Item_Productos(id_proveedor)
        carga_Forma_Pago()



    End Sub

    Private Sub btnAddListaProduc_Click(sender As Object, e As EventArgs) Handles btnAddListaProduc.Click
        dtFechaPedido.Value = Me.dtFechaCompra.Value
        If Calcula_Cost_LineaAdd() Then
            Calcula_Total()
            Me.ListView1.Items(ListView1.Items.Count - 1).EnsureVisible()
            cmbItemProducto.Focus()
        End If
    End Sub

    Private Function Cargar_dtCosto(ByVal idProProveedor As Integer) As DataTable

        sql = "SELECT TOP (100) PERCENT pp.idPresentacion, p.IvaPorcentaje, ppr.CostoTotal, ppr.idProProveedor, pv.ivaSubtotal "
        sql = sql & "FROM   dbo.ProductoPresentacion AS pp INNER JOIN "
        sql = sql & "dbo.Productos AS p ON pp.idProducto = p.idProducto INNER JOIN "
        sql = sql & "dbo.ProductoProveedor AS ppr ON pp.idPresentacion = ppr.idPresentacion INNER JOIN "
        sql = sql & "dbo.Proveedores AS pv ON ppr.idProveedor = pv.idProveedor "
        sql = sql & "WHERE(ppr.idProProveedor = " & idProProveedor & ") "


        Try

            Using cnn = New SqlConnection(DomainSQLite.Setting.Configuration.ConectionString)
                cnn.Open()

                Using cmd As New SqlCommand(sql, cnn)
                    cmd.CommandType = CommandType.Text


                    Dim da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable

                    da.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        Return dt
                    Else
                        Return Nothing
                    End If
                End Using

            End Using





        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error en el frmAdquisicion en el Cargar_dtCosto")
            Return Nothing
        End Try
    End Function


    Private Sub Calcula_Total()
        Dim i As Integer
        Dim iva12, iva00, costFila As Double

        Ptotals = 0
        Descuen = 0
        IvaReal = 0
        costFila = 0

        For i = 0 To Me.ListView1.Items.Count - 1

            IvaReal = IvaReal + ListView1.Items(i).SubItems(5).Text
            Descuen = Descuen + ListView1.Items(i).SubItems(4).Text
            costFila = ListView1.Items(i).SubItems(6).Text

            'valida si el costo incluye iva
            If ListView1.Items(i).SubItems(7).Text = "Si" Then
                typoCosto = True
            Else
                typoCosto = False
            End If



            If typoCosto Then
                'si el costo incluye iva
                Ptotals = Ptotals + ListView1.Items(i).SubItems(6).Text
                'Si tiene porcentaje
                If Double.Parse(ListView1.Items(i).SubItems(5).Text) > 0 Then
                    iva12 = (iva12 + ListView1.Items(i).SubItems(6).Text) - ListView1.Items(i).SubItems(5).Text
                Else
                    iva00 = iva00 + ListView1.Items(i).SubItems(6).Text
                End If
            Else
                'cuando el costo no incluye iva
                Ptotals = Ptotals + ((costFila + ListView1.Items(i).SubItems(5).Text) - ListView1.Items(i).SubItems(4).Text)
                'Si tiene porcentaje
                If ListView1.Items(i).SubItems(5).Text > 0 Then
                    iva12 = iva12 + ListView1.Items(i).SubItems(6).Text
                Else
                    iva00 = iva00 + ListView1.Items(i).SubItems(6).Text + ListView1.Items(i).SubItems(5).Text
                End If
            End If

        Next

        If Me.cbxRedondSin.Checked = True Then

            Me.txt0Iva.Text = RedondearNo(iva00, Me.txtLugarDecimal.Text)
            Me.txt12Iva.Text = RedondearNo(iva12, Me.txtLugarDecimal.Text)
            Me.txtDescuento.Text = RedondearNo(Descuen, Me.txtLugarDecimal.Text)
            Me.txtIva.Text = RedondearNo(IvaReal, Me.txtLugarDecimal.Text)
            Me.txtTotal.Text = RedondearNo((Ptotals), Me.txtLugarDecimal.Text)

        ElseIf Me.cbxRedondCon.Checked = True Then
            Me.txt0Iva.Text = RedondearSi(iva00, Me.txtLugarDecimal.Text)
            Me.txt12Iva.Text = RedondearSi(iva12, Me.txtLugarDecimal.Text)
            Me.txtDescuento.Text = RedondearSi(Descuen, Me.txtLugarDecimal.Text)
            Me.txtIva.Text = RedondearSi(IvaReal, Me.txtLugarDecimal.Text)
            Me.txtTotal.Text = RedondearSi((Ptotals), Me.txtLugarDecimal.Text)
        End If

    End Sub

    Private Function Calcula_Cost_LineaAdd() As Boolean
        Dim idProProveedor As Integer = 0

        'validamos informacion para poder agaregar
        Try
            'si no está selecionado un item salgo de esta funcion
            If cmbItemProducto.SelectedIndex < 0 Then
                cmbItemProducto.Focus()
                Return False
            End If

            'cojo el cidigo para validar costos 
            idProProveedor = Me.cmbItemProducto.Items(cmbItemProducto.SelectedIndex).Item("idProProveedor").ToString
            'exijo que ingrese cantidadd e compra
            If Double.Parse(txtCantidad.Text) <= 0 Then
                MsgBox("Cantidad de producto comprado no puede ser cero", MsgBoxStyle.Information, "Aviso..")
                txtCantidad.Focus()
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error en el frmAdquisicion en el sub Calcula_Cost_LineaAdd")
            Return False
        End Try

        'gargo y valido la tabla para determinar costos
        Dim dt As New DataTable

        'cargo la tabla
        dt = Cargar_dtCosto(idProProveedor)

        'si no pude  gargar salgo de l afuncion
        If IsNothing(dt) Then Return False
        'cargo la tabla pero no obtengo registro 
        If dt.Rows.Count = 0 Then
            MsgBox("Resulto 0 registro para poder validar costos se recomienta Actualizar del menu actualizar costo")
            Me.btnActulProducro.Focus()
            Return False
        End If



        'AGREGO EL REGISTRA AL LISTADO DE COMPRAS -------------------------------------------------------------------------------------------------------     -

        'Me.txtcodProducto.Text = dt.Rows(0).Item("CostTotal").ToString
        Dim Filas As Integer = Me.ListView1.Items.Count

        'id de producto 
        Me.ListView1.Items.Add(txtcodProdcuto.Text)

        'Nombre de producto es solo para ver y no guarda nindun lago solo en idproducto
        Me.ListView1.Items.Item(Filas).SubItems.Add(cmbItemProducto.Items(cmbItemProducto.SelectedIndex)("Unidad").ToString)

        'cantidad
        Me.ListView1.Items.Item(Filas).SubItems.Add(txtCantidad.Text)
        Cant = txtCantidad.Text

        'precio unitario
        Me.ListView1.Items.Item(Filas).SubItems.Add(dt.Rows(0).Item("CostoTotal").ToString)
        PUnt = CDbl(Me.ListView1.Items.Item(Filas).SubItems(3).Text)

        'Descuento
        Me.ListView1.Items.Item(Filas).SubItems.Add(0)
        Descuen = CDbl(Me.ListView1.Items.Item(Filas).SubItems(4).Text)

        'carga % de IVA de la base en la variable 
        IvaPor = dt.Rows(0).Item("IvaPorcentaje").ToString

        'iva cuando el costo total  ya incluye 
        typoCosto = dt.Rows(0).Item("ivaSubtotal").ToString

        'EL COSTO YA INCLUYE IVA
        If typoCosto Then
            'valor de iva
            Me.ListView1.Items.Item(Filas).SubItems.Add(((IvaPor * 100) * (Cant * PUnt)) / (100 + ((IvaPor * 100))))
            'P/Total
            Me.ListView1.Items.Item(Filas).SubItems.Add(Cant * PUnt)
        Else

            'Precio Unitario prebio sin iva
            Me.ListView1.Items.Item(Filas).SubItems(3).Text = RedondearSi((PUnt * 100 * IvaPor) / (100 + IvaPor * 100), 5)


            'agrego el valor de iva
            Me.ListView1.Items.Item(Filas).SubItems.Add(Me.ListView1.Items.Item(Filas).SubItems(3).Text * Cant)

            'Modifico El valor de pUnitario sin iva
            Me.ListView1.Items.Item(Filas).SubItems(3).Text = PUnt - Me.ListView1.Items.Item(Filas).SubItems(3).Text


            'agrego el valor total de fila sin incluir el precio de ñiva
            Me.ListView1.Items.Item(Filas).SubItems.Add(Cant * Me.ListView1.Items.Item(Filas).SubItems(3).Text)
        End If

        'TIPO DE COSTO SI ya incluye iva
        If typoCosto Then
            Me.ListView1.Items.Item(Filas).SubItems.Add("Si")
        Else
            Me.ListView1.Items.Item(Filas).SubItems.Add("No")
        End If
        'agrego el codigo de presentacion y la oculto
        Me.ListView1.Items.Item(Filas).SubItems.Add(idPresent)
        Me.ListView1.Columns(8).Width = 0


        Return True


    End Function

    Private Sub btnNuevoProducto_Click(sender As Object, e As EventArgs) Handles btnNuevoProducto.Click

        With MDI_AddProdcutos
            .id_Proveedor = id_proveedor
            .flag = 6
            .ShowDialog()
            If .DialogResult = DialogResult.OK Then
                Carga_Item_Productos(id_proveedor)
                Me.cmbItemProducto.SelectedValue = Me.txtValueResul.Text
                Me.txtValueResul.Text = 0
            End If
        End With
        MDI_AddProdcutos = Nothing
    End Sub

    Private Sub btnVerProducto_Click(sender As Object, e As EventArgs) Handles btnVerProducto.Click
        validaItem_Producto()
        If validado Then

            With MDI_AddProdcutos
                .id_Proveedor = id_proveedor
                .flag = 4
                .ShowDialog()
            End With
            MDI_AddProdcutos = Nothing
        End If

    End Sub
    Private Sub validaItem_Producto()


        If cmbItemProducto.SelectedIndex < 0 Then
            MsgBox("Debe seleccionar un producto", MsgBoxStyle.Exclamation, "Alerta")
            cmbItemProducto.Focus()
            validado = False

        ElseIf cmbItemProducto.SelectedValue > 0 Then
            validado = True
            idPresent = cmbItemProducto.Items(cmbItemProducto.SelectedIndex).item("idPresentacion").ToString
        Else
            MsgBox("Debe seleccionar un producto", MsgBoxStyle.Exclamation, "Alerta")
            cmbItemProducto.Focus()
            validado = False
        End If
    End Sub

    Private Sub btnActulProducro_Click(sender As Object, e As EventArgs) Handles btnActulProducro.Click
        Carga_Item_Productos(id_proveedor)
    End Sub

    Private Sub btnNewProveedor_Click(sender As Object, e As EventArgs) Handles btnNewProveedor.Click
        Using frmAdd_Proveedor
            With frmAdd_Proveedor
                .flag = "Agregar"
                .ShowDialog()
                If .DialogResult = DialogResult.OK Then
                    Carga_Combo_Proveedor()
                    Me.cmbProveedor.Text = Me.txtValueResul.Text
                    Me.txtValueResul.Text = 0
                End If
            End With
        End Using
    End Sub

    Private Sub btnListProveedor_Click(sender As Object, e As EventArgs) Handles btnListProveedor.Click
        Try
            Using listProveedor As New frmList_Proveedores(stateLoad.Dialogo, stateClient.User)
                With listProveedor
                    .ShowDialog()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, "Error")
        End Try
    End Sub
    Private Sub carga_Forma_Pago()

        Try
            sql = "Select * from [stm].[FormaPago] order by formaPago "


            Using cnn = New SqlConnection(DomainSQLite.Setting.Configuration.ConectionString)
                cnn.Open()

                Dim cmd As New SqlCommand(sql, cnn)
                cmd.CommandType = CommandType.Text

                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable

                da.Fill(dt)
                cmbFormaPago.DataSource = Nothing
                If dt.Rows.Count > 0 Then
                    With cmbFormaPago
                        .DataSource = dt
                        .ValueMember = "idformaPago"
                        .DisplayMember = "formaPago"
                        .SelectedValue = 1
                    End With
                    dt = Nothing
                End If
            End Using



        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al cargar Proveedor")
        End Try
    End Sub

    Private Sub menuCantidad_Click(sender As Object, e As System.EventArgs) Handles menuCantidad.Click

        If Item_Select < 0 Then
            MsgBox("Seleccione un registro", MsgBoxStyle.Exclamation, "Aviso")
            Exit Sub
        End If

        Using formnew As New frmImputData
            With formnew
                .Text = "Ingrese la CANTIDAD"
                .txtFlag.Text = 3
                .txtNumber.Value = CDec(Me.ListView1.Items(Item_Select).SubItems(2).Text)
                .txtNumber.Visible = True
                .ShowDialog()
                If .DialogResult = DialogResult.OK Then
                    Me.ListView1.Items(Item_Select).SubItems(2).Text = CDec(sql)
                    LeeFila()
                    Calculando_Precios_Linea(1)  'con el codigo 1 hago que el total se modifique 
                End If
            End With
        End Using
    End Sub

    Private Sub Calculando_Precios_Linea(ByVal Otp As Integer)

        'EL COSTO TOTAL ES LA QUE NO SE BA HA CAMBIAR
        If Otp = 0 Then
            'SI EN EL TOTAL INCLUYE IVA
            If typoCosto Then

                'IVA REAL aplicando el escuento
                IvaReal = ((Ptotals - Descuen) * IvaPor * 100) / (IvaPor * 100 + 100)
                IvaReal = RedondearSi(IvaReal, 5)

                'IVA REAL APLICANDO DESCUENTO
                Me.ListView1.Items.Item(Item_Select).SubItems(5).Text = IvaReal

                'P/Unitario
                PUnt = RedondearSi(Ptotals / Cant, 5)
                Me.ListView1.Items.Item(Item_Select).SubItems(3).Text = PUnt
            Else
                sql = ((Ptotals - Descuen) * IvaPor)
                IvaReal = ((Ptotals - Descuen) * IvaPor)
                IvaReal = RedondearSi(IvaReal, 5)
                'valor de iva
                Me.ListView1.Items.Item(Item_Select).SubItems(5).Text = IvaReal
                'P/Unitario
                Me.ListView1.Items.Item(Item_Select).SubItems(3).Text = (Ptotals + Descuen) / Cant

            End If


            'CAMBIA EL COSTO TOTAL (se considera el precio unitario)
        ElseIf Otp = 1 Then   'El costo no incluye iva
            'si el costo total incluye iva
            If typoCosto Then
                Ptotals = Ptotals - Descuen
                IvaReal = (Ptotals * IvaPor * 100) / (IvaPor * 100 + 100)
                Ptotals = Ptotals - IvaReal
                'precio unitario
                Me.ListView1.Items.Item(Item_Select).SubItems(3).Text = (Ptotals + Descuen) / Cant
                'valor de iva
                Me.ListView1.Items.Item(Item_Select).SubItems(5).Text = IvaReal
                'P/Total

            Else   'cuado el sub total no incluye iva
                'P/Total
                Ptotals = Cant * PUnt
                Ptotals = RedondearSi(Ptotals, 5)
                Me.ListView1.Items.Item(Item_Select).SubItems(6).Text = Ptotals

                ' iva real menos descuento 
                IvaReal = (Ptotals - Descuen) * IvaPor
                IvaReal = RedondearSi(IvaReal, 5)
                Me.ListView1.Items.Item(Item_Select).SubItems(5).Text = IvaReal
                'P/Total
            End If
        End If
        Calcula_Total()
    End Sub


    Private Sub LeeFila()

        Try
            Cant = ListView1.Items.Item(Item_Select).SubItems(2).Text
            PUnt = ListView1.Items.Item(Item_Select).SubItems(3).Text
            Descuen = ListView1.Items.Item(Item_Select).SubItems(4).Text
            IvaReal = ListView1.Items.Item(Item_Select).SubItems(5).Text
            Ptotals = ListView1.Items.Item(Item_Select).SubItems(6).Text

            If ListView1.Items.Item(Item_Select).SubItems(7).Text = "Si" Then
                typoCosto = True
            Else
                typoCosto = False
            End If

            Dim ivaEsp As Double = IvaLook(ListView1.Items.Item(Item_Select).SubItems(0).Text)

            If ivaEsp >= 0 Then
                IvaPor = ivaEsp
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error en el frmAdquisicion en el sub LeeFila")

        End Try


    End Sub
    Private Function IvaLook(ByVal CodProduct As String) As Double
        sql = "SELECT IvaPorcetaje  from Productos where idproducto  = (SELECT TOP 1 idproducto  from ProductoPresentacion where codProducto  = '" & CodProduct & "')"


        Try

            Using cnn = New SqlConnection(DomainSQLite.Setting.Configuration.ConectionString)
                cnn.Open()
                Using cmd As New SqlCommand(sql, cnn)
                    cmd.CommandType = CommandType.Text
                    Dim dat As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable

                    dat.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        Return dt(0)(0)
                    Else
                        Return -1
                    End If

                End Using
            End Using





        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error: frmAdquisicion en el IvaLook")
            Return -1
        Finally

        End Try
    End Function


    Private Sub menuPUnitario_Click(sender As Object, e As System.EventArgs) Handles menuPUnitario.Click

        If Item_Select < 0 Then
            MsgBox("Seleccione un registro", MsgBoxStyle.Exclamation, "Aviso")
            Exit Sub
        End If



        If MsgBox("Quiere modificar PRECIO UNITARIO", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "Responda") = MsgBoxResult.Yes Then

            If Me.ListView1.Items(Item_Select).SubItems(3).Text > 0 Then
                sql = Me.ListView1.Items(Item_Select).SubItems(3).Text
            Else
                sql = 0
            End If
            With frmImputData
                .Text = "Ingrese el PRECIO"
                .txtFlag.Text = 3
                .txtNumber.Value = sql
                .txtNumber.Visible = True
                .ShowDialog()
            End With

            If frmImputData.DialogResult = DialogResult.OK Then
                Me.ListView1.Items(Item_Select).SubItems(3).Text = CDec(Me.txtValueResul.Text)
                Me.txtValueResul.Text = 0
                LeeFila()
                Calculando_Precios_Linea(1) 'codigo 1 para alterar en prescio tatal
            End If
            frmImputData = Nothing

        End If
    End Sub

    Private Sub menuDescuento_Click(sender As Object, e As System.EventArgs) Handles menuDescuento.Click
        If Item_Select < 0 Then
            MsgBox("Seleccione un registro", MsgBoxStyle.Exclamation, "Aviso")
            Exit Sub
        End If


        Dim resul As DialogResult
        resul = MessageBox.Show("Quiere modificar DESCUENTO", "responda", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3)
        If resul = DialogResult.Yes Then
            With frmImputData
                .Text = "Ingrese el DESCUENTO"
                .txtFlag.Text = 3
                .txtNumber.Value = CDec(Me.ListView1.Items(Item_Select).SubItems(4).Text)
                .txtNumber.Visible = True
                .ShowDialog()
            End With
            If frmImputData.DialogResult = DialogResult.OK Then
                Me.ListView1.Items(Item_Select).SubItems(4).Text = CDec(Me.txtValueResul.Text)

                Me.txtValueResul.Text = 0
                LeeFila()
                Calculando_Precios_Linea(1)  'con el codigo 1 hago que el total se modifique 

            End If
            frmImputData = Nothing
        End If
    End Sub


    Private Sub menuIva_Click(sender As Object, e As System.EventArgs) Handles menuIva.Click
        If Item_Select < 0 Then
            MsgBox("Seleccione un registro", MsgBoxStyle.Exclamation, "Aviso")
            Exit Sub
        End If


        Dim resul As DialogResult
        resul = MessageBox.Show("Quiere modificar IVA", "responda", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3)
        If resul = DialogResult.Yes Then
            With frmImputData
                .Text = "Ingrese el VALOR DE IVA"
                .txtFlag.Text = 3
                .txtNumber.Value = CDec(Me.ListView1.Items(Item_Select).SubItems(5).Text)
                .txtNumber.Visible = True
                .ShowDialog()
            End With
            If frmImputData.DialogResult = DialogResult.OK Then
                Me.ListView1.Items(Item_Select).SubItems(5).Text = CDec(Me.txtValueResul.Text)
                Me.txtValueResul.Text = 0
                LeeFila()
                Calculando_Precios_Linea(1)
            End If

            frmImputData = Nothing
        End If

    End Sub

    Private Sub menuPtotal_Click(sender As Object, e As System.EventArgs) Handles menuPtotal.Click
        If Item_Select < 0 Then
            MsgBox("Seleccione un registro", MsgBoxStyle.Exclamation, "Aviso")
            Exit Sub
        End If


        If MsgBox("Esta seguro de moficicar el PRECIO TOTAL ", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then

            If Me.ListView1.Items(Item_Select).SubItems(6).Text > 0 Then
                sql = Me.ListView1.Items(Item_Select).SubItems(6).Text
            Else
                sql = 0
            End If
            With frmImputData
                .Text = "Ingrese el PRECIO TOTAL"
                .txtFlag.Text = 3
                .txtNumber.Value = sql
                .txtNumber.Visible = True
                .ShowDialog()
            End With
            If frmImputData.DialogResult = DialogResult.OK Then
                Me.ListView1.Items(Item_Select).SubItems(6).Text = CDec(Me.txtValueResul.Text)
                Me.txtValueResul.Text = 0
                LeeFila()

                Calculando_Precios_Linea(0)  'CON LA OPCION 0 HACEMOS QUE EL COSTO TOTAL SEA la no se ba ha mover
            End If

            frmImputData = Nothing
        End If

    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        Dim breakfast As ListView.SelectedListViewItemCollection = Me.ListView1.SelectedItems

        Dim item As ListViewItem

        Item_Select = -1
        For Each item In breakfast
            Item_Select = item.Index
        Next
    End Sub

    Private Sub menuEmilinar_Click(sender As Object, e As EventArgs) Handles menuEmilinar.Click
        If Item_Select < 0 Then
            MsgBox("Debe seleccionar un registro", MsgBoxStyle.Exclamation, "Aviso")
            Exit Sub
        End If

        Dim resul As DialogResult
        resul = MessageBox.Show("Realmente desea eliminar este registro", "responda", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3)
        If resul = DialogResult.Yes Then
            Me.ListView1.Items(Item_Select).Remove()
        End If
    End Sub

    Private Sub menuModificar_Click(sender As Object, e As EventArgs) Handles menuModificar.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Me.Cursor = Cursors.WaitCursor

        Me.txtIdFormaPago.Text = Me.cmbFormaPago.SelectedValue
        If Guarda_Pedido() Then
            If Guarda_compra() Then
                Me.ListView1.Items.Clear()
                Me.txtNumDoc.Text = ""
                Me.dtFechaCompra.Focus()
                Me.grupDetail.Enabled = False
                Me.Cursor = Cursors.Default
                Exit Sub
            End If
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Function Guarda_Pedido() As Boolean
        OrderDetail = 1
        Me.txtOrden.Text = OrderDetail
        Dim MyFecha As Date = FormatDateTime(Me.dtFechaPedido.Value, DateFormat.ShortDate)

        If OrderDetail > 0 Then
            sql = "insert into Pedidos (FechaPedido, IdProveedor, base00Iva, base12Iva, Descuento, Iva, TotalPedido, codUser) "
            sql = sql & "Values ('" & MyFecha & "', " & id_proveedor & ", " & txt0Iva.Text & ", " & txt12Iva.Text & ", " & txtDescuento.Text & ", " & txtIva.Text & ", " & txtTotal.Text & ", '" & UsuarioActivo.codUser & "') "


            Try

                Using cnn = New SqlConnection(DomainSQLite.Setting.Configuration.ConectionString)
                    cnn.Open()
                    Using cmd = New SqlCommand(sql, cnn)
                        cmd.CommandType = CommandType.Text
                        If cmd.ExecuteNonQuery Then
                            Return True
                        Else
                            Return False
                        End If
                    End Using
                End Using




            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error: frmAdquisicion en el Guarda_Pedido")
                Return False
            End Try

        Else
            MsgBox("No se pudo Obtener el número de orden de compra", MsgBoxStyle.Exclamation, "Aviso")
            Return False
        End If

    End Function


    Private Function Guarda_compra() As Boolean
        Return True
    End Function

    Private Sub cmbFormaPago_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbFormaPago.SelectedValueChanged
        If Me.cmbFormaPago.SelectedIndex > 0 Then
            txtIdFormaPago.Text = Me.cmbFormaPago.SelectedValue
        End If
    End Sub

    Private Sub Carga_Tipo_Doc()


        Try
            sql = "Select * from [stm].[TypoDocumento] "

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

    Private Sub Carga_Tipo_Consumo()


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
                End If
            End Using



        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error: frmAdquisicion en el  cargar Carga_Tipo_Consumo")
        End Try

    End Sub



    Private Sub cmbItemProducto_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbItemProducto.SelectedValueChanged
        If cmbItemProducto.SelectedIndex < 0 Then Exit Sub
        idPresent = cmbItemProducto.Items(cmbItemProducto.SelectedIndex).item("idPresentacion").ToString
        txtcodProdcuto.Text = cmbItemProducto.Items(cmbItemProducto.SelectedIndex).item("CodProducto").ToString
        Me.txtCantidad.Focus()
    End Sub



    Private Sub btnAddListaProduc_GotFocus(sender As Object, e As EventArgs) Handles btnAddListaProduc.GotFocus
        txtCantidad.Select(0, Len(txtCantidad.Text))
    End Sub

    Private Sub btnAddListaProduc_KeyDown(sender As Object, e As KeyEventArgs) Handles btnAddListaProduc.KeyDown
        ' Initialize the flag to false.
        nonNumberEntered = False

        ' Determine whether the keystroke is a number from the top of the keyboard.
        If e.KeyCode < Keys.D0 OrElse e.KeyCode > Keys.D9 Then
            ' Determine whether the keystroke is a number from the keypad.
            If e.KeyCode < Keys.NumPad0 OrElse e.KeyCode > Keys.NumPad9 Then
                ' Determine whether the keystroke is a backspace.
                If e.KeyCode <> Keys.Back And e.KeyCode <> 190 Then
                    ' A non-numerical keystroke was pressed. 
                    ' Set the flag to true and evaluate in KeyPress event.
                    nonNumberEntered = True
                End If

                If e.KeyCode = 110 Then
                    nonNumberEntered = False
                End If
            End If
        End If
        'If shift key was pressed, it's not a number.
        If Control.ModifierKeys = Keys.Shift Then
            nonNumberEntered = True
        End If
    End Sub

    Private Sub btnAddListaProduc_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnAddListaProduc.KeyPress
        ' Check for the flag being set in the KeyDown event.
        If nonNumberEntered = True Then
            ' Stop the character from being entered into the control since it is non-numerical.
            e.Handled = True
            Beep()
        End If
    End Sub

    Private Sub btnAddListaProduc_TextChanged(sender As Object, e As EventArgs) Handles btnAddListaProduc.TextChanged
        SetDefaulBnt(Me.btnAddListaProduc)
    End Sub

    Private Sub SetDefaulBnt(ByVal myDefaultBtn As Button)
        Me.AcceptButton = myDefaultBtn
        myDefaultBtn.BackColor = Color.LightGreen
    End Sub
    Private Sub LostDefaulBnt(ByVal myDefaultBtn As Button)
        Me.AcceptButton = Nothing
        myDefaultBtn.BackColor = Color.Transparent
    End Sub
    Private Sub cmbItemProducto_GotFocus(sender As Object, e As System.EventArgs) Handles cmbItemProducto.GotFocus
        LostDefaulBnt(Me.btnAddListaProduc)
    End Sub

    Private Sub cmbItemProducto_Enter(sender As Object, e As EventArgs) Handles cmbItemProducto.Enter
        LostDefaulBnt(Me.btnAddListaProduc)
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles btnListPtduct.Click
        With frmLista_Producto
            'COMO ADMINISTRADOR
            .flag = "ADMINISTRADOR"
            .ShowDialog()
        End With
        frmLista_Producto = Nothing
    End Sub

    Private Sub txtNumDoc_Leave(sender As Object, e As EventArgs) Handles txtNumDoc.Leave
        Me.txtNumDoc.Text = Trim(Me.txtNumDoc.Text)
    End Sub
End Class