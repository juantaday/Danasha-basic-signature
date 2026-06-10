Imports System.Data.SqlClient
Imports System.Threading
Imports System.Windows
Imports CADsisVenta
Imports CADsisVenta.[Class]
Imports CADsisVenta.Data.Emuns.EnumSatateModule
Imports CADsisVenta.DataSetClientesTableAdapters
Imports CADsisVenta.DataSetComprasTableAdapters
Imports CADsisVenta.DataSetSystemTableAdapters
Imports CADsisVenta.Funtions
Imports CADsisVenta.Helpers.FInicio
Imports CADsisVenta.Statics
Imports Domain.Logica
Imports ec.gob.sri.comprobantes.Enum
Imports InterfaceSignatureAndSRI.Processes
Imports Domain.Helpers.EnumExtensions
Imports NpgsqlTypes

Public Class frmVentas
    'Para sumar totales
    Private vueltas As Integer
    Protected Friend idCajaStado As Integer
    Private descuento, IvaTotal, base0, base12, Iva As Double

    Dim Cant, PUnt, Descuen, totalFact As Double
    Dim nonNumberEntered As Boolean = False
    Dim Item_Select As Integer = -1

    'para determinar cual es el ultimo ingreso
    Dim counUltimo As Double
    Dim nameProductUltimo As String
    Dim itemUltimoIngreso As Integer
    Dim dtProducItem As DataTable
    'miembros peotegidos
    Protected Friend idPersona As Integer
    Protected Friend idcliente As Integer
    Protected Friend idBodega As Integer
    Protected Friend vuelto As Double
    Protected Friend otrosValores As Double
    Private _nameDocument As String

    Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        If txtExploret.Parent.Parent IsNot Nothing Then
            txtExploret.Parent.Parent.TabIndex = 0
        End If
        txtExploret.Parent.TabIndex = 0
        txtExploret.TabIndex = 0
        txtExploret.Focus()

    End Sub
    Private Sub frmDiario_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            'cargamos bodega en la que manejará el stock
            Carga_Bodega()
            'obtenemos el numero de factura agenerarse...
            btnUpda_NumFactur.PerformClick()
            'caraga cliente predeterminado
            CargaConsumidFinal()
            'oculta tarifario de otros valores

            Mays()
            txtExploret.Focus()
        Catch ex As Exception
            MsgBox(ex.Message + " en el frmDiario_Load del " + Name, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Async Sub pedidoButton_Click(sender As Object, e As EventArgs) Handles pedidoButton.Click
        Try
            pedidoButton.Enabled = False
            Me.Cursor = Cursors.WaitCursor

            If ListView1.Items.Count = 0 Then
                MsgBox("Agregue productos antes de crear una transferencia.", MsgBoxStyle.Exclamation, "Sin productos")
                Exit Sub
            End If

            Using frm As New frmTransferencia(Await BuildDetalleTransferencia())
                If (frm.ShowDialog(Me) = DialogResult.OK) Then
                    ListView1.Items.Clear()
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message & " en pedidoButton_Click", MsgBoxStyle.Critical, "Error")
        Finally
            pedidoButton.Enabled = True
            Me.Cursor = Cursors.Default
        End Try
    End Sub


    Private Async Function BuildDetalleTransferencia() As Task(Of List(Of DetalleTransferenciaItem))
        ' Paso 1: extraer solo id y cantidad del ListView
        Dim seleccion As New List(Of (idProducto As Integer, cantidad As Decimal, precioTotal As Decimal))

        For Each item As ListViewItem In ListView1.Items
            Dim idProducto As Integer
            Dim cantidad As Decimal
            Dim precioTotal As Decimal
            Integer.TryParse(item.SubItems(idProductoClm.Index).Text, idProducto)
            Decimal.TryParse(item.SubItems(CantidadClm.Index).Text, cantidad)
            Decimal.TryParse(item.SubItems(PTotalClm.Index).Text, precioTotal)
            seleccion.Add((idProducto, cantidad, precioTotal))
        Next

        ' Paso 2: consultar BD local con todos los IDs de una sola vez
        Dim ids As String = String.Join(",", seleccion.Select(Function(x) x.idProducto))
        Dim sql As String =
        "SELECT p.idProducto, p.Nom_Comercial, p.Nom_Comun, " &
        "       p.idUnidad, u.Nom_Unidad, " &
        "       p.IdSubCategoria, s.Nom_SubCategoria, " &
        "       p.ivaPorcentaje, p.Facturable, " &
        "       pp.codProducto, pp.precioCompra, pp.precioVenta, " &
        "       pp.Presentacion AS unidadPresent, " &
        "       p.Deft_idPresenVenta, p.Deft_idPresenCompra, pp.Cant_Present" &
        " FROM   Productos p " &
        " JOIN   ProductoPresentacion pp ON pp.idProducto = p.idProducto " &
        "                              AND pp.isPresentFactory = 1 " &
        " JOIN   ProductoUndMin       u  ON u.idUnidad = p.idUnidad " &
        " JOIN   ProductoSubCategoria s  ON s.idSubCategoria = p.IdSubCategoria " &
        "WHERE  p.idProducto IN (" & ids & ")"

        ' Paso 3: cargar en Dictionary para cruce eficiente
        Dim mapa As New Dictionary(Of Integer, DetalleTransferenciaItem)
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            Dim dt As DataTable = Await cmd.RetornaTablaAsync(sql)
            For Each row As DataRow In dt.Rows
                Dim idP As Integer = CInt(row("idProducto"))
                mapa(idP) = New DetalleTransferenciaItem With {
                .idProducto = idP,
                .NombreProducto = row("Nom_Comercial").ToString(),
                .NomComun = row("Nom_Comun").ToString(),
                .codProducto = row("codProducto").ToString(),
                .idUnidad = CInt(row("idUnidad")),
                .idSubCategoria = CInt(row("IdSubCategoria")),
                .Deft_idPresenCompra = CInt(row("Deft_idPresenCompra")),
                .Deft_idPresenVenta = CInt(row("Deft_idPresenVenta")),
                .ivaPorcentaje = CDec(row("ivaPorcentaje")),
                .Facturable = CBool(row("Facturable")),
                .PrecioCompra = CDec(row("precioCompra")),
                .PrecioVenta = CDec(row("precioVenta")),
                .Unidad = row("unidadPresent").ToString(),
                .CantPresent = CDec(row("Cant_Present"))
            }
            Next
        End Using


        ' Paso 4: cruzar con cantidad y precioTotal del ListView
        Dim lista As New List(Of DetalleTransferenciaItem)
        For Each sel In seleccion
            If mapa.ContainsKey(sel.idProducto) Then
                Dim d = mapa(sel.idProducto)
                d.CantidadEnviada = sel.cantidad
                d.PrecioTotal = sel.precioTotal
                lista.Add(d)
            End If
        Next

        Return lista
    End Function


    Private Sub Carga_Bodega()
        Try
            idBodega = 0
            Dim tapt As New TerminalTableAdapter
            Dim dt As New DataTable
            dt = tapt.GetDataByHostNameAndIdBodega(Dominio._HotName, TerminalActivo.idBodega)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    With TerminalActivo
                        .idTerminal = dt.Rows(0)("idTerminal")
                        .idBodega = dt.Rows(0)("idBodega")
                        .codTerminal = dt.Rows(0)("CodTerminal")
                        .Dominio = Dominio._HotName
                    End With
                    '//cargamos informcion de la bodega
                    Dim tap_bod As New BodegasTableAdapter
                    dt = tap_bod.GetDataByIdBodega(TerminalActivo.idBodega)
                    If dt.Rows.Count = 1 Then
                        lblBodega.Text = dt.Rows(0)("nom_bodega").ToString
                        idBodega = dt.Rows(0)("idBodega")
                    End If
                End If
            End If

            If idBodega = 0 Then
                MsgBox("Es posible que este equipo no esté registrado o no tenga asignado una Bodega o Local a la que Debe manejar el Stock.", MsgBoxStyle.Exclamation, "Importante")
                FacturaButton.Enabled = False
                NotaVentaButton.Enabled = False
                ProformaButton.Enabled = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message + " Al cargar bodega final", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub CargaConsumidFinal()
        Try
            sql = "select c.idCliente ,c.Nombres  as cliente "
            sql = sql & " from [dbo].[clienteName] As c "
            sql = sql & "where c.Ruc_Ci = '9999999999999' "
            Using cmd As New Funtions.SqlComandExec
                Using dt = cmd.RetornaTabla(sql)
                    If Not IsNothing(dt) Then
                        If dt.Rows.Count > 0 Then
                            idcliente = dt.Rows(0)("idCliente")
                            CedulaTextBox.Text = String.Empty
                            NomClienteText.Text = dt.Rows(0)("cliente")
                        End If
                    End If
                End Using
            End Using


        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub SumatoriaTotal()
        Dim ivaPorcent As Double = 0
        Dim simImpuesto As Decimal = 0
        Dim ivaLinea As Decimal = 0
        descuento = 0
        vueltas = 0
        base0 = 0
        Iva = 0
        IvaTotal = 0
        totalFact = 0
        base12 = 0
        Dim total As Double = 0
        Try
            With ListView1
                For i = 0 To .Items.Count - 1
                    ivaPorcent = Double.Parse(.Items(i).SubItems(IvaPorClm.Index).Text)
                    total = Double.Parse(.Items(i).SubItems(PTotalClm.Index).Text)


                    totalFact += total
                    descuento += Double.Parse(.Items(i).SubItems(DescuentoClm.Index).Text)
                    'si no tiene iva ponemos el total
                    If ivaPorcent = 0 Then
                        base0 += total
                    Else
                        simImpuesto = Math.Round(total / (1 + ivaPorcent), 2, MidpointRounding.AwayFromZero)
                        ivaLinea = total - simImpuesto

                        base12 += simImpuesto
                        Iva += ivaLinea
                    End If
                    'si el totas es cero pintamos
                    .Items(i).UseItemStyleForSubItems = False
                    If total = 0 Then
                        .Items(i).SubItems(PTotalClm.Index).BackColor = Color.Red
                        .Items(i).SubItems(PTotalClm.Index).ForeColor = Color.White
                    Else
                        .Items(i).SubItems(PTotalClm.Index).BackColor = Color.White
                        .Items(i).SubItems(PTotalClm.Index).ForeColor = Color.Black
                    End If
                    'hasta aqui pinto los valores ceros en total
                    'anoto vueltas
                    vueltas += 1
                Next
            End With
            Call MostrarTotal()
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Function Carga_Item_ProductoIdPresent(ByVal idPresent As Integer) As DataTable

        sql = "Select Top(1) pr.idPresentacion, p.idProducto, pr.codProducto, p.Nom_Comercial,pr.precioCompra,
        pr.PresentacionPrint As [Medida], pr.precioVenta, p.IvaPorcentaje,pr.Empaquetado,
        pr.idProUndMed, pr.idProUndReferen, pr.Cant_Present, pr.Empaquetado, Presentacion, 
        case when  s.stock is null  then 0 else  s.stock end as [Stock]
        From dbo.Productos As p 
        INNER Join  dbo.ProductoPresentacion AS pr ON p.idProducto = pr.idProducto 
        INNER Join  dbo.ProductoUndMedida As m On pr.idProUndMed = m.idProUndMed
        left join ProductosStock as s  on s.idProducto = p.idProducto and s.idBodega =@idBodega
        WHERE   pr.idPresentacion =@idPresent"

        Try
            Using cmd As New SqlComandExec

                cmd.ParameterCollection = New SqlParameter() {New SqlParameter With {
                    .ParameterName = "@idPresent",
                    .SqlDbType = SqlDbType.VarChar,
                    .Value = idPresent
                }, New SqlParameter With {
                    .ParameterName = "@idBodega",
                    .SqlDbType = SqlDbType.Int,
                    .Value = TerminalActivo.idBodega
                }}

                Return cmd.RetornaTabla(sql)
            End Using
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error al cargar Item Producto")
            Return Nothing
        End Try

    End Function


    Private Function Carga_Item_ProductoBarcode(ByVal BarCode As String) As DataTable

        sql = "Select Top(1) pr.idPresentacion, p.idProducto, pr.codProducto, p.Nom_Comercial,pr.precioCompra,
        pr.PresentacionPrint As [Medida], pr.precioVenta, p.IvaPorcentaje,pr.Empaquetado,
        pr.idProUndMed, pr.idProUndReferen, pr.Cant_Present, pr.Empaquetado, Presentacion, 
        case when  s.stock is null  then 0 else  s.stock end as [Stock]
        From dbo.Productos As p 
        INNER Join  dbo.ProductoPresentacion AS pr ON p.idProducto = pr.idProducto 
        INNER Join  dbo.ProductoUndMedida As m On pr.idProUndMed = m.idProUndMed
        left join ProductosStock as s  on s.idProducto = p.idProducto and s.idBodega =@idBodega
        WHERE  pr.Barcode =@Barcode"

        Try
            Using cmd As New SqlComandExec

                cmd.ParameterCollection = New SqlParameter() {New SqlParameter With {
                    .ParameterName = "@Barcode",
                    .SqlDbType = SqlDbType.VarChar,
                    .Value = BarCode
                }, New SqlParameter With {
                    .ParameterName = "@idBodega",
                    .SqlDbType = SqlDbType.Int,
                    .Value = TerminalActivo.idBodega
                }}

                Return cmd.RetornaTabla(sql)
            End Using
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error al cargar Item Producto")
            Return Nothing
        End Try

    End Function

    Private Function Agrega_Fila() As Boolean
        Dim totalCantidad As Double = 0
        Dim fila As Integer = 0
        Try
            Dim Cantidad As Double = Double.Parse(txtCantidad.Text)
            If Cantidad = 0 Then
                Cantidad = 1
            End If

            'rebisamos si no es codigo munerico entonces es barra de codigo
            Dim isnumric As Boolean = True
            For Each texto In txtExploret.Text
                If InStr("0123456789", texto) = False Then
                    isnumric = False
                    Exit For
                End If
            Next
            'si es codigo de barra busco************
            If isnumric Then
                dtProducItem = Carga_Item_ProductoBarcode(txtExploret.Text)
            End If
            'Buscamos si existe el prodducto enviado por el usuario   ********************************************************************
            Dim isLoadProdcuto As Boolean = True
            If IsNothing(dtProducItem) Then
                isLoadProdcuto = False
            ElseIf dtProducItem.Rows.Count = 0 Then
                isLoadProdcuto = False
            End If
            'si no econtrebusco en la lista de prodcuto
            If Not (isLoadProdcuto) Then
                Using fornew As New frmList_ProductPrecioVenta
                    With fornew
                        .flag = "Ventas"
                        .txtProduc_Select.Text = Me.txtExploret.Text
                        .ShowDialog()
                        If .DialogResult = DialogResult.OK Then
                            dtProducItem = Carga_Item_ProductoIdPresent(.idPresent)
                            If (dtProducItem Is Nothing) Then Return False

                            If dtProducItem.Rows.Count > 0 Then
                                GoTo Agrega_Item
                            Else
                                Return False
                            End If
                        Else
                            Return False
                        End If
                    End With
                End Using
            End If

Agrega_Item:
            'Buscamos si ya emos ingreasado el idPresentacion para modificar la cantidad *********************************************************************
            For i = 0 To Me.ListView1.Items.Count - 1
                If Integer.Parse(dtProducItem(0)("idPresentacion").ToString()) = Integer.Parse(ListView1.Items(i).Text) Then
                    If IsNumeric(ListView1.Items(i).SubItems(5).Text) Then
                        fila = i
                        totalCantidad = Cantidad + Double.Parse(ListView1.Items(i).SubItems(5).Text)
                        'editamos la fila
                        'modificamos la cantidad
                        ListView1.Items(i).SubItems(5).Text = totalCantidad
                        PUnt = ListView1.Items(i).SubItems(6).Text
                        'modificamos precio total
                        Dim total As Double = RedondearSi(PUnt * totalCantidad, 2)
                        ListView1.Items(i).SubItems(7).Text = total.ToString("N2")
                        Me.counUltimo = ListView1.Items(i).SubItems(5).Text
                        Me.nameProductUltimo = ListView1.Items(i).SubItems(3).Text
                        Me.itemUltimoIngreso = i
                        GoTo CalculaOfertas 'rebisamos las ofertas
                    End If
                End If
            Next

            'si llegamos hasta aqui es poque es nuevo idPresentacion ha ingresar******************************************************
            'determino en que linea debo ingresar el producto
            totalCantidad = Cantidad
            fila = ListView1.Items.Count

            If (fila = 0) Then
                Color_Control()
                txtFormaPago.Text = String.Empty
                lblVuelto.Text = String.Empty
            End If

            'idpresentacion que no se ve (codigo)  [0]
            ListView1.Items.Add(dtProducItem(0)("idPresentacion").ToString)

            'IdProducto de producto  [1]
            ListView1.Items.Item(fila).SubItems.Add((dtProducItem(0)("idProducto").ToString))

            'Codigo de prodcuto  [2]
            ListView1.Items.Item(fila).SubItems.Add((dtProducItem(0)("CodProducto").ToString))

            'Nombre de producto   [3]
            ListView1.Items.Item(fila).SubItems.Add((dtProducItem(0)("Nom_Comercial").ToString))

            'Empaque  [4]
            ListView1.Items.Item(fila).SubItems.Add((dtProducItem(0)("Medida").ToString))

            'CANTIDAD  [5]
            ListView1.Items.Item(fila).SubItems.Add(Cantidad)
            Cant = Cantidad  'cargo el valor en una variable para luego multiplicar

            'STOCK  [6]
            ListView1.Items.Item(fila).SubItems.Add(dtProducItem(0)("Stock").ToString())

            'Precio unitario [7]
            ListView1.Items.Item(fila).SubItems.Add(dtProducItem(0)("precioVenta").ToString())
            PUnt = Double.Parse(ListView1.Items.Item(fila).SubItems(PUnitarioClm.Index).Text)

            'Precio Total [8]
            totalFact = RedondearSi(PUnt * Cant, 2)
            ListView1.Items.Item(fila).SubItems.Add(totalFact.ToString("N2"))

            'porcentage iva [9]
            ListView1.Items.Item(fila).SubItems.Add(dtProducItem(0)("IvaPorcentaje").ToString)

            'Precio compra  [10]
            ListView1.Items.Item(fila).SubItems.Add(dtProducItem(0)("precioCompra").ToString)

            'descuento  [11]
            ListView1.Items.Item(fila).SubItems.Add(0)

            'tarifa  [12]
            ListView1.Items.Item(fila).SubItems.Add(0)

            'para avisar al cliente quien ingreso al ultimo
            Me.counUltimo = ListView1.Items(fila).SubItems(CantidadClm.Index).Text
            Me.nameProductUltimo = ListView1.Items(fila).SubItems(3).Text
            Me.itemUltimoIngreso = fila

CalculaOfertas:
            Ofertas(fila)

PintaRepedidas:
            'PintaRepetido(fila, Color.Bisque)
            Return True
        Catch ex As Exception
            MsgBox(ex.Message & vbLet & ex.StackTrace, MsgBoxStyle.Critical, "Error")
            Return False
        End Try

    End Function

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click

        txtExploret.Text = Trim(txtExploret.Text)
        'si  no tengo nada regreso
        If Not (txtExploret.Text.Length > 2) Then
            Return
        End If
        'preparamos el texto
        Dim isSpace As Boolean = False
        sql = ""
        For Each stri In txtExploret.Text
            If Not isSpace Then
                sql += stri
                isSpace = False
            End If
            If String.IsNullOrWhiteSpace(stri) Then
                isSpace = True
            Else
                If isSpace Then
                    sql += stri
                End If
                isSpace = False
            End If
        Next
        txtExploret.Text = sql
        dtProducItem = Nothing
        If Agrega_Fila() Then
            SumatoriaTotal()
            txtCantidad.Text = 0
            txtExploret.Text = ""
            ListView1.MultiSelect = False
            ListView1.Items(itemUltimoIngreso).EnsureVisible()
            ListView1.Items(itemUltimoIngreso).Selected = True
            ListView1.MultiSelect = True
            txtExploret.Focus()
            ProformaButton.Enabled = True
            PanefinalizFactur.Enabled = False
            Me.UltimoIngresoLabel.Text = String.Format("Ultimo ingreso: {0} | {1}", Me.counUltimo.ToString("N2"), Me.nameProductUltimo)
            '  Beep()   ' Sound a tone.
            If ListView1.Items.Count > 5 And otrosValores > 0 Then
                totalFact += otrosValores
                lblOtrosValoresView.Visible = True
                lblOtrosValoresView.Text = String.Format("Aplicando valor adicional: {0}", otrosValores.ToString("C2"))
                Call MostrarTotal()
            Else
                lblOtrosValoresView.Visible = False
            End If
        End If
    End Sub
    Private Sub MostrarTotal()

        lblItemsTotal.Text = "Total items:  " & vueltas.ToString("N0")
        lblIva.Text = "Iva $ : " & Iva.ToString("N2")
        lbldescuento.Text = "Total descuento $: " & descuento.ToString("N2")
        lblIva0.Text = "Base 0%  : " & base0.ToString("N2")
        lblIva12.Text = "Base 15%  : " & base12.ToString("N2")
        lbltotal.Text = "Total $: " & totalFact.ToString("N2")
    End Sub
    Private Sub PintaRepetido(Filas As Integer, myColor As Color)
        Try
            With ListView1
                .Items(Filas).UseItemStyleForSubItems = False
                For i = 0 To .Items(Filas).SubItems.Count - 1
                    .Items(Filas).SubItems(i).BackColor = myColor
                    .Items(Filas).SubItems(i).ForeColor = Color.Blue
                Next
                .Items(Filas).UseItemStyleForSubItems = True
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub Ofertas(ByVal Items As Integer)
        Try
            Dim i As Integer
            Dim idPresnt As Integer = Integer.Parse(ListView1.Items(Items).SubItems(0).Text)
            Dim Cantid As Double = Double.Parse(ListView1.Items(Items).SubItems(CantidadClm.Index).Text)
            'Aliminamos si antes ya se aplico 
            ListView1.Items(Items).SubItems(DescuentoClm.Index).Text = "0"

            Dim cmd As New ClassCargadorProducto

            sql = "Select * From Ofertas As o "
            sql = sql & "Where o.idPresent = " & idPresnt & " And o.CantOferta <= " & Cantid & " "
            sql = sql & "Order By o.CantOferta desc "
            Dim dt As DataTable = cmd.RetornaTabla(sql)

            If Not IsNothing(dt) Then
                For i = 0 To dt.Rows.Count - 1
                    'si el valor de oferta es mayo de 0 para que no produsca erro al dividir
                    If Double.Parse(dt(i)("valor_Oferta").ToString) > 0 Then
                        If Boolean.Parse(dt(i)("Caducidad").ToString) = True Then
                            Dim ahora As Date = FormatDateTime(Now(), DateFormat.ShortDate)
                            Dim evaluar As Date = Date.Parse(dt.Rows(i)("fech_Caduce").ToString)
                            If Date.Parse(evaluar) >= Date.Parse(ahora) Then
                                GoTo Aplicando
                            End If
                        Else
                            GoTo Aplicando
                        End If
                    End If
                Next
            End If
            'salimos hantes de realizar aplicación
            Exit Sub

Aplicando:
            'bamos aplicar el descuento ******************************************************************************************
            'ValTotal  = cantidad * Precio unitario
            Dim ValTotal As Double = Double.Parse(ListView1.Items(Items).SubItems(CantidadClm.Index).Text) *
                Double.Parse(ListView1.Items(Items).SubItems(PUnitarioClm.Index).Text)

            Dim Oferta As Double = Double.Parse(dt(i)("valor_Oferta").ToString)
            Dim Descuento = RedondearSi(ValTotal * Oferta, 2)
            'aplicamos los cambio a listview
            '---Total descuento del item
            ListView1.Items(Items).SubItems(DescuentoClm.Index).Text = Descuento
            '---total Precio del item
            ListView1.Items(Items).SubItems(PTotalClm.Index).Text = ValTotal - Descuento
            Beep()

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub txtCantidad_GotFocus(sender As Object, e As System.EventArgs) Handles txtCantidad.GotFocus
        sender.Select(0, Len(sender.Text))
    End Sub

    Private Sub txtCantidad_LostFocus(sender As Object, e As System.EventArgs) Handles txtCantidad.LostFocus


    End Sub 'textBox1_KeyPress


    Private Sub txtCantidad_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtCantidad.TextChanged

    End Sub
    Private Sub SetDefaulBnt(ByVal btn As Button)
        Me.AcceptButton = btn
    End Sub

    Private Sub LostDefaulBnt(ByVal myDefaultBtn As Button)
        Me.AcceptButton = Nothing
    End Sub
    Private Sub btnAddFactura_Click(sender As System.Object, e As System.EventArgs) Handles FacturaButton.Click, ProformaButton.Click, NotaVentaButton.Click
        Color_Control()

        Dim nameDocument As String = If(Not IsNothing(sender.tag), sender.tag, String.Empty)

        If nameDocument = "Proforma" Or nameDocument = "Pedido" Then
            If PreviewFactura() Then
                txtNumFactur.Text = vbCrLf & " EN PROCESO DE " & nameDocument.ToUpper()
                txtFormaPago.Text = txtNumFactur.Text
                lblVuelto.Text = String.Empty
                FacturVenta.nameDocunt = nameDocument
                PanefinalizFactur.Enabled = True
                sender.backColor = Color.Yellow
                btnPago.Focus()
                Return
            End If
        End If

        _nameDocument = nameDocument

        'cambio de nombre para enviar a facturacion electronica...
        nameDocument = If(_nameDocument.Equals("ElectronicBill"), "Factura", _nameDocument)

        If Procesa_Datos(nameDocument) Then
            PanefinalizFactur.Enabled = True
            sender.backColor = Color.Yellow
            btnPago.Focus()
        End If

    End Sub

    Private Async Sub SingninInvoiceAsync(idFactur As Integer)
        Await Task.Delay(5)


        Dim token As TokensValidos = Nothing
        Dim mss As String = ""

        Try
            If (SettingObject.SignatureOptios Is Nothing OrElse String.IsNullOrWhiteSpace(SettingObject.SignatureOptios.TOKEN)) Then
                mss = "Debe configurar elgùn token kalido para firma electronica.."
                GoTo viewMesagge
            End If

            token = TokensValidos.obtenerToken(SettingObject.SignatureOptios.TOKEN, SettingObject.SignatureOptios.THUMBPRINT)
        Catch ex As Exception
            mss = ex.Message & vbNewLine & ex.StackTrace
            GoTo viewMesagge
        End Try


        Dim claveAcceso = ""
        Dim sql = ""


        Dim progress = New Progress(Of String)(Sub(state)
                                                   If state.Contains("=>Clave") Then
                                                       claveAcceso = state
                                                   End If

                                                   labelViewProgresSing.Text = claveAcceso & vbLf & vbLf & state
                                               End Sub)

        If TypeOf token Is Object Then

            Try
                Using axion = New SignSendInvoice(New CancellationTokenSource())
                    axion.ActionToExecute = Sub(t) axion.ExecuteWidhtIdProcess(token,
                                    SettingObject.SignatureOptios.TIPO_AMBIENTE.ToString(),
                                    progress, idFactur, SettingObject.EcommerceActive.CommerceId,
                                    SaveFile:=False, 'SettingObject.WareHouseActive.SaveToFile,
                                    SaveInDataBase:=SettingObject.WareHouseActive.SaveToDataBase, files:=Nothing)

                    axion.star()
                End Using
            Catch ex As Exception
                mss = ex.Message & vbNewLine & ex.StackTrace
                GoTo viewMesagge
            End Try
        Else
            mss = "No se puede realizar la firma dijital." & vbCrLf
            mss = mss & "El token configurado no  esta registrado para este software" & vbCrLf & vbLf
            mss = mss & "Token configuraco:" & SettingObject.SignatureOptios.TOKEN

            GoTo viewMesagge
        End If

        Return


viewMesagge:
        Me.Invoke(New MethodInvoker(Sub()
                                        MsgBox(mss, MsgBoxStyle.Critical, "Error")
                                    End Sub))

    End Sub

    Private Sub Color_Control()
        Try
            SumatoriaTotal()
            For Each con As System.Windows.Forms.Button In TableFactur.Controls
                con.UseVisualStyleBackColor = True
            Next
        Catch Ex As Exception
            MsgBox(Ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Function Procesa_Datos(documento As String) As Boolean
        Try
            Dim tipoDocumento = FromDisplayName(Of TipoDocumento)(documento)

            If Not tipoDocumento.HasValue Then
                MessageBox.Show("Configuracion de documento no encontrado")
                Return False
            End If

            FacturVenta.nameDocunt = String.Empty
            If Determina_formaPago(documento) Then
                If PreviewFactura() Then
                    FacturVenta.nameDocunt = documento
                    If itemsXFactur(tipoDocumento.Value) Then
                        txtNumFactur.Text = sql
                    End If
                    Return True
                End If
            End If
            Return False
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
            Return False    
        End Try


    End Function

    Private Sub menuEliminar_Click(sender As Object, e As System.EventArgs) Handles menuEliminar.Click
        btnDeleteItems.PerformClick()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        If Me.ListView1.SelectedItems.Count = 1 Then
            btnEditCant.Enabled = True
            btnPacketProdcut.Enabled = True
            btnUp.Enabled = True
            btnDow.Enabled = True
        Else
            btnEditCant.Enabled = False
            btnPacketProdcut.Enabled = False
            btnUp.Enabled = False
            btnDow.Enabled = False
        End If
        'para boton actualizar
        If Me.ListView1.Items.Count >= 0 Then
            btnActualiza.Enabled = True
        Else
            btnActualiza.Enabled = False
        End If
        'para boton eliminar
        If ListView1.SelectedItems.Count > 0 Then
            btnDeleteItems.Enabled = True
        Else
            btnDeleteItems.Enabled = False
        End If

    End Sub

    Private Sub menuCantidad_Click(sender As Object, e As System.EventArgs) Handles menuCantidad.Click
        btnEditCant.PerformClick()
    End Sub



    Private Sub menuPTotal_Click(sender As Object, e As System.EventArgs) Handles menuPTotal.Click
        Try
            Dim isAhutorize = False
            Dim codUserAuthorizwe = String.Empty

            Using newform As New LoginForm(stateReturn._response, "Ventas")
                With newform
                    .Text = "Validando para midificar"
                    .ShowDialog()
                    If .DialogResult = DialogResult.OK Then
                        codUserAuthorizwe = .UsernameTextBox.Text
                        isAhutorize = True
                    End If
                End With
            End Using

            If isAhutorize Then
                Dim oldTotal As Decimal = 0
                Decimal.TryParse(Me.ListView1.SelectedItems(0).SubItems(PTotalClm.Index).Text, oldTotal)


                Using newfom2 As New frmImputData()
                    With newfom2
                        .txtNumber.Value = oldTotal
                        .ShowDialog()
                        If .DialogResult = DialogResult.OK Then

                            If (oldTotal > .txtNumber.Value) Then
                                Me.ListView1.SelectedItems(0).SubItems(DescuentoClm.Index).Text = (oldTotal - .txtNumber.Value).ToString()
                            ElseIf (oldTotal < .txtNumber.Value) Then
                                Me.ListView1.SelectedItems(0).SubItems(TarifaClm.Index).Text = (.txtNumber.Value - oldTotal).ToString()
                            End If

                            Me.ListView1.SelectedItems(0).SubItems(PTotalClm.Index).Text = .txtNumber.Value.ToString("N2")

                            Me.ListView1.SelectedItems(0).SubItems(PTotalClm.Index).Tag = codUserAuthorizwe
                            SumatoriaTotal()
                        End If
                    End With

                End Using
            End If
        Catch ex As Exception

        End Try



    End Sub
    Public Function Carga_idStadoCaja(dt As DataTable) As Boolean
        Try
            If Not (dt.Rows.Count = 1) Then
                sql = "Hay varias operaciones abierta" & vbNewLine
                sql = sql & "Habilite solo una..."
                MsgBox(sql, MsgBoxStyle.Exclamation, "Importante")
                Return False
            End If

            If Not IsDBNull(dt.Rows(0)("own_User")) Then
                If Not UsuarioActivo.codUser.Equals(dt.Rows(0)("own_User")) Then
                    sql = "La operacion abierta pertenece a: " & dt.Rows(0)("own_User") & vbNewLine
                    sql = sql & "Habilite una para este usuario..."
                    MsgBox(sql, MsgBoxStyle.Exclamation, "Importante")
                    Return False
                End If
            End If
            OperacionNumLabel.Text = "Operación:" & vbNewLine & dt.Rows(0)("idCajaStado")
            OperacionNumLabel.Tag = dt.Rows(0)("idCajaStado")
            Me.idCajaStado = dt.Rows(0)("idCajaStado")

            Dim _user As String = Convert.ToString(dt.Rows(0)("own_User"))
            If String.IsNullOrEmpty(_user) Then
                User_operaLabel.Text = "Para: Todos" & vbNewLine & "los usuarios"
            Else
                User_operaLabel.Text = "Para: " & vbNewLine & _user
            End If
            Return True
        Catch ex As Exception
            MsgBox(ex.Message & vbLet & ex.StackTrace, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function

    Private Sub btnListProducto_Click(sender As Object, e As EventArgs)
        Using fornew As New frmList_ProductPrecioVenta
            With fornew
                .flag = "Ventas"
                .txtProduc_Select.Text = Me.txtExploret.Text
                .ShowDialog()
                If .DialogResult = DialogResult.OK Then
                    txtExploret.Text = Busca_Codigo(.idPresent)
                    btnAgregar.PerformClick()
                End If
            End With
        End Using
    End Sub

    Private Function Busca_Codigo(ByVal idPresent As Integer) As String
        Try
            sql = "select pp.codProducto from ProductoPresentacion  as pp "
            sql = sql & "where pp.idPresentacion = " & idPresent & ""
            Dim cmd As New ClassCargadorProducto()
            Dim dt As DataTable = cmd.RetornaTabla(sql)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0)(0)
                End If
            End If
            Return ""
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return ""
        End Try
    End Function

    Private Sub btnClose_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        If Me.ListView1.Items.Count > 0 Then
            If MsgBox("Existe informacion sin guardar. Desea salir de todas maneras", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Alerta") = MsgBoxResult.Yes Then
                Me.Close()
            Else
                Exit Sub
            End If
        End If
        Me.Close()
    End Sub

    Private Sub frmVentaDiario_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.ListView1.Items.Count > 0 Then

            If MsgBox("Existe informacion sin guardar. Desea salir de todas maneras", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Alerta") = MsgBoxResult.Yes Then
            Else
                e.Cancel = True
            End If
        End If
    End Sub
    Private Sub btnPacketProdcut_Click(sender As Object, e As EventArgs) Handles btnPacketProdcut.Click
        Try
            If ListView1.SelectedItems.Count = 0 Then
                Return
            End If
            Using fornew As New frmProductoPresentacion()
                With fornew
                    .flag = "Operando"
                    .lblProducto.Text = ListView1.SelectedItems.Item(0).SubItems(3).Text
                    .idproducto = ListView1.SelectedItems.Item(0).SubItems(1).Text
                    .ShowDialog()
                    If .DialogResult = DialogResult.OK Then
                        ListView1.SelectedItems(0).SubItems(0).Text = .idpresentacion
                        ListView1.SelectedItems(0).SubItems(4).Text = .txtMedida.Text
                        ListView1.SelectedItems(0).SubItems(6).Text = .txtPrecioUnidad.Text
                        'modificamos los valores de linea  ! cantidad * valor unitario
                        Dim ValTotal As Double = Double.Parse(ListView1.SelectedItems(0).SubItems(5).Text) * Double.Parse(ListView1.SelectedItems(0).SubItems(6).Text)
                        ValTotal = RedondearSi(ValTotal, 2)
                        ListView1.SelectedItems(0).SubItems(7).Text = ValTotal.ToString("N2")  'valor total de lines
                        ListView1.SelectedItems(0).SubItems(10).Text = 0   'descuento 0 para luego modificar con la funcion sigiente
                        Ofertas(ListView1.SelectedItems(0).Index)
                        SumatoriaTotal()
                    End If
                End With
            End Using

        Catch ex As Exception
            MsgBox(ex.Message + " " + ex.Source.ToString, MsgBoxStyle.Critical, "Error")
        Finally
            ListView1.Focus()
        End Try

    End Sub



    Private Sub btnDeleteItems_Click(sender As Object, e As EventArgs) Handles btnDeleteItems.Click
        Try
            Dim SelectItems As ListView.SelectedListViewItemCollection =
                        ListView1.SelectedItems
            Dim item As ListViewItem
            If Not IsNothing(SelectItems) Then
                If SelectItems.Count > 0 Then
                    If MsgBox("Está seguro de eliminar Items seleccionados?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Responda") = MsgBoxResult.Yes Then
                        For Each item In SelectItems
                            item.Remove()
                        Next
                        SumatoriaTotal()
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub btnEditLine_Click(sender As Object, e As EventArgs) Handles btnEditCant.Click
        Try
            If ListView1.SelectedItems.Count = 1 Then
                Using newform As New frmImputData()
                    With newform
                        .txtNumber.Value = ListView1.SelectedItems(0).SubItems(CantidadClm.Index).Text
                        .ShowDialog()
                        If .DialogResult = DialogResult.OK Then
                            If .txtNumber.Value = 0 Then
                                MsgBox("No se puede modificar la canidad a cero, puede tomar la opción de Eliminar", MsgBoxStyle.Exclamation, "Aviso")
                            Else
                                Cant = .txtNumber.Value
                                PUnt = ListView1.SelectedItems(0).SubItems(PUnitarioClm.Index).Text  'cojemos precio unitario
                                ListView1.SelectedItems(0).SubItems(CantidadClm.Index).Text = Cant 'actualizamos cantidad
                                'precio total////
                                Dim total As Double = RedondearSi(Cant * PUnt, 2)
                                ListView1.SelectedItems(0).SubItems(PTotalClm.Index).Text = total.ToString("N2") 'catualizamosm precio total
                                Ofertas(ListView1.SelectedItems(0).Index)

                            End If
                        End If
                    End With
                End Using
                SumatoriaTotal()

            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            ListView1.Focus()
        End Try
    End Sub

    Private Sub frmVentas_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F1 Then
            ListPersonButton.PerformClick()
        ElseIf e.KeyCode = Keys.Escape Then
            txtExploret.Focus()
        ElseIf e.KeyCode = Keys.F1 Then
            ListPersonButton.PerformClick()
        ElseIf e.KeyCode = Keys.F2 Then
            Call CargaConsumidFinal()
        ElseIf e.KeyCode = Keys.F4 Then
            Agrega_Fila()
        ElseIf e.KeyCode = Keys.F6 Then
            btnDeleteItems.PerformClick()
        ElseIf e.KeyCode = Keys.F8 Then
            btnEditCant.PerformClick()
        ElseIf e.KeyCode = Keys.F9 Then
            btnPacketProdcut.PerformClick()
        ElseIf e.KeyCode = Keys.F10 Then
            ProformaButton.PerformClick()
        ElseIf e.KeyCode = Keys.F11 Then
            NotaVentaButton.PerformClick()
        ElseIf e.KeyCode = Keys.F12 Then
            FacturaButton.PerformClick()
        End If
    End Sub

    Private Sub txtExploret_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtExploret.KeyPress
        If Asc(e.KeyChar) = 42 Then 'solo cuando ingresa * para cambiar cantidad
            Try
                If IsNumeric(Me.txtExploret.Text) Then
                    Dim Cant As Double = Me.txtExploret.Text
                    Me.txtCantidad.Text = Cant
                    txtExploret.Text = ""
                    txtExploret.Focus()
                    e.Handled = True
                Else
                    MsgBox("Para considerar como cantidad deben ser valores numéricos", MsgBoxStyle.Exclamation, "Importante")
                    txtExploret.Focus()
                    e.Handled = True
                End If

            Catch ex As Exception
                MsgBox(ex.Message + " en el btnCantidad_Click del " + Me.Name, MsgBoxStyle.Critical, "Revise que todo sea número")
                txtExploret.Focus()
            End Try
        End If
    End Sub

    Private Sub txtExploret_TextChanged(sender As Object, e As EventArgs) Handles txtExploret.TextChanged
        If txtExploret.TextLength > 0 Then
            AcceptButton = btnAgregar
        Else
            AcceptButton = Nothing
        End If
    End Sub

    Private Sub btnUp_Click(sender As Object, e As EventArgs) Handles btnUp.Click
        Try
            If ListView1.SelectedItems.Count = 0 Then
                Return
            End If

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
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnDow_Click(sender As Object, e As EventArgs) Handles btnDow.Click
        Try
            If ListView1.SelectedItems.Count = 0 Then
                Return
            End If
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
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Async Sub btnActualiza_Click(sender As Object, e As EventArgs) Handles btnActualiza.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim total As Double = 0
            Dim list As ListView = ListView1()
            Dim dt As DataTable
            Dim idPresentation As Integer = 0

            'Actualizamos codigo Nom_Producto, Empq, Precio unitario y precio Total
            sql = $"Select pp.codProducto, p.Nom_Comercial, pp.PresentacionPrint as [Medida], pp.precioVenta 
                    From ProductoPresentacion As pp 
                    inner Join Productos As p On pp.idProducto = p.idProducto
                    inner Join ProductoUndMedida as m on pp.idProUndMed = m.idProUndMed
                    where pp.idPresentacion = @idPresentacion;"

            Using cmd As New Funtions.SqlComandExec
                For i = 0 To list.Items.Count - 1
                    Integer.TryParse(list.Items(i).SubItems(0).Text, idPresentation)
                    If i = 0 Then
                        cmd.ParameterCollection = New SqlParameter() {New SqlParameter With
                            {
                                .ParameterName = "@idPresentacion",
                                .SqlDbType = SqlDbType.Int
                         }}
                    End If
                    cmd.SetValueParamater("@idPresentacion", idPresentation)
                    dt = Await cmd.RetornaTablaAsync(sql)
                    If Not IsNothing(dt) Then
                        If dt.Rows.Count > 0 Then
                            list.Items(i).SubItems(2).Text = dt.Rows(0)("codProducto")  'codido de producto
                            list.Items(i).SubItems(3).Text = dt.Rows(0)("Nom_Comercial")  'codido de producto
                            list.Items(i).SubItems(4).Text = dt.Rows(0)("Medida")  'codido de producto
                            list.Items(i).SubItems(PUnitarioClm.Index).Text = dt.Rows(0)("precioVenta")  'codido de producto
                            'precio tatal  = cantidad * precio unitario
                            total = Double.Parse(list.Items(i).SubItems(CantidadClm.Index).Text) * Double.Parse(list.Items(i).SubItems(PUnitarioClm.Index).Text)
                            total = RedondearSi(total, 2)
                            list.Items(i).SubItems(PTotalClm.Index).Text = total.ToString("N2")
                        End If
                    End If

                Next
            End Using

            'Actualizamos ofertas
            For i = 0 To list.Items.Count - 1
                Ofertas(list.Items(i).Index)
            Next
            SumatoriaTotal()
            Cursor = Cursors.Default
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
            Cursor = Cursors.Default
        Finally
            ListView1.Focus()
        End Try
    End Sub



    Private Sub menuPTotal_Paint(sender As Object, e As PaintEventArgs) Handles menuPTotal.Paint
        If ListView1.SelectedItems.Count = 1 Then
            sender.enabled = True
        Else
            sender.enabled = False
        End If
    End Sub

    Private Sub menuCantidad_Paint(sender As Object, e As PaintEventArgs) Handles menuCantidad.Paint
        If ListView1.SelectedItems.Count = 1 Then
            sender.enabled = True
        Else
            sender.enabled = False
        End If
    End Sub



    Private Sub menuEliminar_Paint(sender As Object, e As PaintEventArgs) Handles menuEliminar.Paint
        If ListView1.SelectedItems.Count > 0 Then
            sender.enabled = True
        Else
            sender.enabled = False
        End If
    End Sub

    Private Sub btnEdd_NumFactur_Click(sender As Object, e As EventArgs) Handles btnEdd_NumFactur.Click
        Try
            Using fornew As New frmConfFactura()
                With fornew
                    .ShowDialog()
                    If .DialogResult = DialogResult.OK Then
                        btnUpda_NumFactur.PerformClick()
                    End If
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Function Determina_formaPago(documento As String) As Boolean
        Try
            totalFact = FormatCurrency(totalFact + otrosValores, 2)
            MostrarTotal()
            If otrosValores > 0 Then
                lblOtrosValoresView.Text = String.Format("Aplicando valor adicional: {0}", otrosValores.ToString("C2"))
                lblOtrosValoresView.Visible = True
            Else
                lblOtrosValoresView.Visible = False
            End If

            If Not Integer.Parse(idcliente) > 0 Then
                MsgBox("Seleccione el Cliente", MsgBoxStyle.Information, "Aviso")
                Return False
            End If
            'acualizo informcion para creditos
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try

        Try
            If Me.ListView1.Items.Count > 0 Then
                Using fornew As New frmFormaPago()
                    FacturVenta.OtroValor = 0
                    Pagos.idCliente = 0
                    Pagos.idformaPago = 0
                    With fornew
                        .flag = "Clientes"
                        .idcliente = idcliente
                        .txtNom_Persona.Text = NomClienteText.Text
                        .txtMontoMaximo.Text = txtMontoMaxito.Text
                        .txtTotal.Text = totalFact
                        .txtPaga.Value = totalFact
                        .ValCaheqtxt.Text = totalFact
                        .txtCreditoActual.Text = totalFact
                        If .InitialityMenu Then
                            .ShowDialog()
                            If .DialogResult = DialogResult.OK Then
                                FacturVenta.OtroValor = Me.otrosValores
                                Pagos.idformaPago = .idFormaPago
                                Pagos.idCliente = .idcliente
                                txtFormaPago.Text = .MsgBoxRetur.ToString()
                                lblVuelto.Text = "Cambio $: " & .Vuelto
                                Return True
                            End If
                        End If
                    End With
                End Using
            Else
                MsgBox("No existe items para determinar pago ", MsgBoxStyle.Information, "Aviso")
            End If
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function
    Private Sub BorraTotal()
        totalFact = 0
        base0 = 0
        base12 = 0
        IvaTotal = 0
        vueltas = 0
        descuento = 0
        otrosValores = 0
        MostrarTotal()
    End Sub
    Private Sub btnBorraCliente_Click(sender As Object, e As EventArgs)
        If MsgBox("Está seguro de borra el cliente", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
            Call CargaConsumidFinal()
        End If
    End Sub

    Private Sub btnbodega_Click(sender As Object, e As EventArgs) Handles btnbodega.Click

        MsgBox("No disponible en esta versión")
        Return

        Using newform As New LoginForm(stateReturn._response, "Ventas")
            With newform
                .Text = "Validando para midificar"
                .ShowDialog()
                If .DialogResult = DialogResult.OK Then
                    Using newfom2 As New frmBodegas()
                        With newfom2
                            .ShowDialog()
                            If .DialogResult = DialogResult.OK Then
                                Carga_Bodega()
                            End If
                        End With
                    End Using
                End If
            End With
        End Using
    End Sub
    Private Sub btnOtroValor_Click(sender As Object, e As EventArgs) Handles btnOtroValor.Click
        '  Me.ContextMenuOtroValor.Show()
    End Sub
    Private Sub OtroValorSelect(selectItem As String)
        Try
            otrosValores = 0
            Select Case selectItem.ToString
                Case "Ninguno"
                    otrosValores = 0
                    SumatoriaTotal()
                Case "Otro valor"
                    Using fornew As New frmImputData
                        With fornew
                            .txtNumber.Value = otrosValores
                            .ShowDialog()
                            If .DialogResult = DialogResult.OK Then
                                otrosValores = .txtNumber.Value
                            End If
                        End With
                    End Using
                Case Else
                    otrosValores = Strings.Right(selectItem.ToString, 1)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub UltDocumentButton_Click(sender As Object, e As EventArgs) Handles UltDocumentButton.Click
        Using frm As New frmUltDocument(frmUltDocument.stateLoad.PrintNot)
            frm.ShowDialog()
        End Using
    End Sub

    Private Sub btnCancelFactur_Click(sender As Object, e As EventArgs) Handles btnCancelFactur.Click
        TableFactur.Enabled = True
        txtFormaPago.Text = String.Empty
        lblVuelto.Text = String.Empty
        PanefinalizFactur.Enabled = False
        AcceptButton = Nothing
        CancelButton = Nothing
    End Sub
    Private Function PreviewFactura() As Boolean
        Try
            Dim idPresent As Integer = 0
            Dim i As Integer
            Dim cat, prec_Compra, prec_Venta, ivaPorcent, descuento, tarifa As Decimal
            Dim codUserAhotorize As String = String.Empty
            Dim del As New ClassCargadorProducto

            sql = "Delete [tmp].[VentasTmp] where codTerminal ='" & TerminalActivo.codTerminal & "' and codUser ='" & UsuarioActivo.codUser & "' "
            del.ExecuteComand(sql)


            Dim cn As New SqlConnection(SimpleDataApp.Utility.GetConnectionString)
            cn.Open()
            For i = 0 To ListView1.Items.Count - 1
                idPresent = Integer.Parse(ListView1.Items(i).SubItems(0).Text)
                cat = Decimal.Parse(ListView1.Items(i).SubItems(CantidadClm.Index).Text)
                prec_Compra = Decimal.Parse(ListView1.Items(i).SubItems(PrecCompraClm.Index).Text)
                prec_Venta = Decimal.Parse(ListView1.Items(i).SubItems(PTotalClm.Index).Text)
                ivaPorcent = Decimal.Parse(ListView1.Items(i).SubItems(IvaPorClm.Index).Text)
                descuento = Decimal.Parse(ListView1.Items(i).SubItems(DescuentoClm.Index).Text)
                tarifa = Decimal.Parse(ListView1.Items(i).SubItems(TarifaClm.Index).Text)
                codUserAhotorize = Convert.ToString(ListView1.Items(i).SubItems(PTotalClm.Index).Tag)

                'alim¿nimiento de sql
                sql = "insert into  [tmp].[VentasTmp] (codTerminal,codUser,idPresent,cant,prec_Compra,"
                sql = sql & "prec_Venta,ivaPorcentaje, descuento,tarifa,CodUserAuthorizeDiscount) "

                sql = sql & "Values (@codTerminal,@codUser,@idPresent,@Cant,@prec_Compra,@prec_Venta,"
                sql = sql & "@ivaPorcen,@descuento,@tarifa,@CodAhutorize) "
                'inicializa cmd tex
                Using cmd As New SqlCommand()
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = cn

                    cmd.CommandText = sql

                    'ID PRESENTATION
                    cmd.Parameters.Add("@idPresent", SqlDbType.Int)
                    cmd.Parameters("@idPresent").Value = idPresent

                    'CODIGO DE TERMIANAL
                    cmd.Parameters.Add("@CodTerminal", SqlDbType.VarChar)
                    cmd.Parameters("@CodTerminal").Value = TerminalActivo.codTerminal

                    'CODIGO usuario determina venta
                    cmd.Parameters.Add("@codUser", SqlDbType.VarChar)
                    cmd.Parameters("@codUser").Value = UsuarioActivo.codUser

                    'CATIDAD DE PRODCUCTOS
                    cmd.Parameters.Add("@Cant", SqlDbType.Decimal)
                    cmd.Parameters("@Cant").Value = cat

                    'PRECIO DE COMPRA
                    cmd.Parameters.Add("@prec_Compra", SqlDbType.Decimal)
                    cmd.Parameters("@prec_Compra").Value = prec_Compra

                    'PRECIO DE VENATA
                    cmd.Parameters.Add("@prec_Venta", SqlDbType.Decimal)
                    cmd.Parameters("@prec_Venta").Value = prec_Venta

                    'PRECIO DE IVA PORCENTAGE
                    cmd.Parameters.Add("@ivaPorcen", SqlDbType.Decimal)
                    cmd.Parameters("@ivaPorcen").Value = ivaPorcent

                    'PRECIO DE DESCUENTO
                    cmd.Parameters.Add("@descuento", SqlDbType.Decimal)
                    cmd.Parameters("@descuento").Value = descuento

                    'PRECIO DE TRIFA
                    cmd.Parameters.Add("@tarifa", SqlDbType.Decimal)
                    cmd.Parameters("@tarifa").Value = tarifa

                    'COD USER AUTHORIZE
                    cmd.Parameters.Add("@CodAhutorize", SqlDbType.VarChar)
                    cmd.Parameters("@CodAhutorize").Value = If(String.IsNullOrEmpty(codUserAhotorize), DBNull.Value, codUserAhotorize)

                    ' EJECUTA EL COMANDO....
                    cmd.ExecuteNonQuery()

                End Using
            Next

            If i > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function
    Private Sub PreviewFacturControl()
        TableFactur.Enabled = False
        PanefinalizFactur.Enabled = True
        AcceptButton = btnPago
        CancelButton = btnCancelFactur
    End Sub
    Private Sub btnPago_Click(sender As Object, e As EventArgs) Handles btnPago.Click

        If Genera_Venta() Then
            Me.UltimoIngresoLabel.Text = String.Empty
            PanefinalizFactur.Enabled = False
            txtExploret.Focus()
        End If

    End Sub
    Private Function Genera_Venta() As Boolean
        Try
            Dim identity As Integer = 0
            Cursor = Cursors.WaitCursor
            FacturVenta.fechDesde = Now
            FacturVenta.fechHasta = Now
            FacturVenta.idBodega = idBodega
            FacturVenta.idCliente = idcliente
            ' determino el tipo de proceso
            If FacturVenta.nameDocunt = "Pedido" Or FacturVenta.nameDocunt = "Proforma" Then
                identity = Procesa_Proforma()
                If identity > 0 Then
                    Me.Cursor = Cursors.Default

                    ListView1.Items.Clear()
                    CargaConsumidFinal()

                    sql = "!..Pedido generada correctamente..!" & vbNewLine
                    sql = sql & "Desea imprimir el documento en Impresora de Ticket..?"
                    Me.Cursor = Cursors.WaitCursor
                    If (MsgBox(sql, MsgBoxStyle.Information + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1) = vbYes) Then
                        PrintTicket_Proforma(identity, FacturVenta.nameDocunt)
                    End If
                    Return True
                End If
            Else  'si es venta o nota de venta
                'reviso si tiene numero de operación 
                Dim stado As Boolean = False
                If IsNumeric(OperacionNumLabel.Tag) Then
                    If Not (Convert.ToInt32(OperacionNumLabel.Tag) = 0) Then
                        stado = True
                    End If
                End If
                ' cargo el numero de operacion si no salgo....
                If stado Then
                    TerminalActivo.idCajaStado = OperacionNumLabel.Tag
                Else
                    MsgBox("No está habilitada este terminal para operar", MsgBoxStyle.Exclamation, "Importante")
                    Return False
                End If
                'chck terminal state
                If Not IsOpenTerminal(Me.idCajaStado, 1) Then
                    Return False
                End If


                FacturVenta.idFactur = -1
                If Not (Procesa_Venta()) Then
                    Return False
                End If


                ' DESAPARESCO LA LISTA POR QUE YA SE EFECTUO LA VENTA
                Me.Cursor = Cursors.Default
                ListView1.Items.Clear()

                If (_nameDocument.Equals("Factura")) Then
                    SingninInvoiceAsync(FacturVenta.idFactur)
                End If


                CargaConsumidFinal()


                sql = "!..Proceso ejecutado correctamente..!" & vbNewLine
                sql = sql & "Desea imprimir el documento en Impresora " & myOptnsPrint.typePrint & " " & myOptnsPrint.NamePrint
                If Not (MsgBox(sql, MsgBoxStyle.Information + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1) = vbYes) Then
                    Return True
                End If

                Me.Cursor = Cursors.WaitCursor
                Select Case myOptnsPrint.typePrint
                    Case "Ticket"
                        Print_Ticket(0, True, True, False, False)
                    Case "Matricial"
                        MsgBox("Opción de impresoras matriciales, no estan disponibles en esta versión..")
                        'printMatricial(FacturVenta.nameDocunt, idDocument:=0, isLatest:=True)
                    Case "Tinta"
                End Select
                Return True
            End If
            Return False
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
            Return False
        Finally
            Cursor = Cursors.Default
        End Try
    End Function

    Private Sub FindClienteButton_Click(sender As Object, e As EventArgs) Handles FindClienteButton.Click
        Try
            If (CedulaTextBox.Text.Trim().Length < 4) Then
                Return
            End If

            Dim response = GeneratedSplit.GenerateSpliter(CedulaTextBox.Text.Trim())
            If Not response.IsSucces Then
                MsgBox("No se pudo analizar los datos a consultar..")
                Return
            Else

                Dim dt As DataTable = ClsPerson.getDataLikePerson(response.Spliter(0), response.Spliter(1), response.Spliter(2))
                If Not IsNothing(dt) Then
                    If dt.Rows.Count = 1 Then
                        idPersona = dt.Rows(0)("idPersona")
                        idcliente = ClsClientes.isClinteBypersonAdmin(idPersona)
                        Carga_Cliente(idcliente)
                        txtExploret.Focus()
                        Return
                    End If
                    Using ListClient As New frmList_Person(stateLoad.Dialogo)
                        With ListClient
                            .dtPersonas = dt
                            .FindTextBox.Text = CedulaTextBox.Text
                            .StartPosition = FormStartPosition.CenterScreen
                            .ShowDialog()
                            If .DialogResult = DialogResult.OK Then
                                idPersona = .PersonClickNamaLabel.Tag
                                idcliente = ClsClientes.isClinteBypersonAdmin(idPersona)
                                CedulaTextBox.Text = .PersonVisibleNemuClicLabel.Tag
                                NomClienteText.Text = .PersonVisibleNemuClicLabel.Text
                                txtExploret.Focus()
                            End If
                            CedulaTextBox.Focus()
                        End With
                    End Using
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub CedulaTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles CedulaTextBox.KeyDown
        nonNumberEntered = True
        'SI VIENE NUMEROS
        If e.KeyCode >= Keys.D0 And e.KeyCode <= Keys.D9 Then
            nonNumberEntered = False
            Return
        End If
        'SI VIENE DEL TECLADO NUMERICO
        If e.KeyCode >= Keys.NumPad0 And e.KeyCode <= Keys.NumPad9 Then
            nonNumberEntered = False
            Return
        End If

        If e.KeyCode >= Keys.A And e.KeyCode <= Keys.Z Then
            nonNumberEntered = False
            Return
        End If
        'SI VIENE Ñ
        If e.KeyCode = 192 Then
            nonNumberEntered = False
            Return
        End If

        If e.KeyCode = 37 Then
            nonNumberEntered = False
            Return
        End If

        If e.KeyCode = Keys.Back Then
            nonNumberEntered = False
            Return
        End If

        If e.KeyCode = Keys.Space Then
            nonNumberEntered = False
            Return
        End If

    End Sub 'textBox1_KeyDown

    Private Sub CedulaTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CedulaTextBox.KeyPress
        If nonNumberEntered Then
            e.Handled = True
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles deleteClienteButton.Click
        If Not NomClienteText.Text.Contains("Consumidor Final") Then
            Call CargaConsumidFinal()
        End If
    End Sub

    Private Sub btnOtroValor_MouseDown(sender As Object, e As MouseEventArgs) Handles btnOtroValor.MouseDown
        If (e.Button = System.Windows.Forms.MouseButtons.Left) Then
            Dim Menu As ContextMenuStrip = Me.ContextMenuOtroValor()
            Menu.Show(Cursor.Position)
        End If
    End Sub

    Private Sub CedulaTextBox_Leave(sender As Object, e As EventArgs) Handles CedulaTextBox.Leave
        sender.Text = Trim(sender.Text)
    End Sub

    Private Sub CedulaTextBox_TextChanged(sender As Object, e As EventArgs) Handles CedulaTextBox.TextChanged
        If CedulaTextBox.TextLength > 2 Then
            AcceptButton = FindClienteButton
        End If
    End Sub
    Sub Mays()
        CedulaTextBox.CharacterCasing = CharacterCasing.Upper
    End Sub

    Private Sub DetailPersonButton_Click(sender As Object, e As EventArgs) Handles DetailPersonButton.Click
        Try
            If Not IsNothing(idPersona) Then
                If idPersona > 0 Then
                    Using addperso As New frmAdd_Personas(stateOperation.Update, idPersona)
                        With addperso
                            .StartPosition = FormStartPosition.CenterScreen
                            .ShowDialog()
                            If .DialogResult = System.Windows.Forms.DialogResult.OK Then
                                idcliente = ClsClientes.isClinteBypersonAdmin(.idPersona)
                                Carga_Cliente(idcliente)
                            End If
                        End With
                    End Using
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub ListPersonButton_Click(sender As Object, e As EventArgs) Handles ListPersonButton.Click
        Try
            Using ListClient As New frmList_Person(stateLoad.Dialogo)
                With ListClient
                    .StartPosition = FormStartPosition.CenterScreen
                    .ShowDialog()
                    If .DialogResult = DialogResult.OK Then
                        idPersona = .PersonClickNamaLabel.Tag
                        idcliente = ClsClientes.isClinteBypersonAdmin(.idPersona)
                        CedulaTextBox.Text = .PersonVisibleNemuClicLabel.Tag
                        NomClienteText.Text = .PersonVisibleNemuClicLabel.Text
                        txtExploret.Focus()
                    End If
                    CedulaTextBox.Focus()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub Carga_Cliente(idCliente)
        Try
            Dim atap As New ClienteNameTableAdapter
            Dim dt As DataTable = atap.GetDataByIdCliente(idCliente)
            If dt.Rows.Count > 0 Then
                CedulaTextBox.Text = dt.Rows(0)("Ruc_Ci")
                NomClienteText.Text = dt.Rows(0)("Nombres")
            End If
            atap = Nothing
            dt = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub findDocumentButton_Click(sender As Object, e As EventArgs) Handles findDocumentButton.Click
        Try
            Cursor = Cursors.WaitCursor
            Using Form As New frmList_Facturas()
                With Form
                    .StartPosition = FormStartPosition.CenterScreen
                    .ShowDialog()
                End With
            End Using
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error ")
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub creditoButton_Click(sender As Object, e As EventArgs) Handles creditoButton.Click
        Try
            Using forcobro As New frmCobro()
                With forcobro
                    .Text = "Estado de deuda del cliente: " & Me.NomClienteText.Text
                    .idCliente = Me.idcliente
                    .ShowDialog()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub txtCantidad_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCantidad.KeyPress
        If InStr(".0123456789", e.KeyChar) = False Then
            If Asc(e.KeyChar) <> 8 Then
                e.Handled = True
            End If
        End If
        'Ascii 
        '8  = Retroceso 
        '58 = dos Puntos Decimales 
        '46 = Punto Decimal 
    End Sub

    Private Sub NomClienteText_TextChanged(sender As Object, e As EventArgs) Handles NomClienteText.TextChanged
        Me.Text = String.Format("VENTAS: {0}", NomClienteText.Text)
    End Sub

    Private Sub frmVentas_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        txtExploret.Focus()
    End Sub


    Private Sub ToolStripMenuItemUnDolar_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemUnDolar.Click
        OtroValorSelect(DirectCast(sender, ToolStripMenuItem).Text)
    End Sub
    Private Sub ToolStripMenuItemDos_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemDosDolar.Click
        OtroValorSelect(DirectCast(sender, ToolStripMenuItem).Text)
    End Sub
    Private Sub ToolStripMenuItemTres_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemTresDolar.Click
        OtroValorSelect(DirectCast(sender, ToolStripMenuItem).Text)
    End Sub
    Private Sub ToolStripMenuItemCuatro_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemCuatroDolra.Click
        OtroValorSelect(DirectCast(sender, ToolStripMenuItem).Text)
    End Sub
    Private Sub ToolStripMenuItemCinco_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemCincoDolar.Click
        OtroValorSelect(DirectCast(sender, ToolStripMenuItem).Text)
    End Sub
    Private Sub ToolStripMenuItemNinguno_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemNinguno.Click
        OtroValorSelect(DirectCast(sender, ToolStripMenuItem).Text)
    End Sub
    Private Sub ToolStripMenuItemOtras_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemOtraValor.Click
        OtroValorSelect(DirectCast(sender, ToolStripMenuItem).Text)
    End Sub
End Class
