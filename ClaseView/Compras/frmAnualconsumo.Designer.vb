<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAnualconsumo
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAnualconsumo))
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.codigo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.producto = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Cantidad = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PUnitario = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Descuento = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Iva = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PTotal = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TypeCostoColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.IdPresentColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ContextMenuListCompra = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.menuEmilinar = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.menuModificar = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuCantidad = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuPUnitario = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuDescuento = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuIva = New System.Windows.Forms.ToolStripMenuItem()
        Me.menuPtotal = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuListPago = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.menuSelecPago = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmbProveedor = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dtFechaCompra = New System.Windows.Forms.DateTimePicker()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cmbItmTipconsumo = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtruc = New System.Windows.Forms.TextBox()
        Me.btnNewProveedor = New System.Windows.Forms.Button()
        Me.btnListProveedor = New System.Windows.Forms.Button()
        Me.btnAddCabConsumo = New System.Windows.Forms.Button()
        Me.grupDetail = New System.Windows.Forms.GroupBox()
        Me.btnListPtduct = New System.Windows.Forms.Button()
        Me.cmbFormaPago = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtCantidad = New System.Windows.Forms.TextBox()
        Me.btnAddListaProduc = New System.Windows.Forms.Button()
        Me.cmbItemProducto = New System.Windows.Forms.ComboBox()
        Me.Groptotal = New System.Windows.Forms.GroupBox()
        Me.cbxRedondSin = New System.Windows.Forms.CheckBox()
        Me.cbxRedondCon = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtLugarDecimal = New System.Windows.Forms.TextBox()
        Me.btnCalTotal = New System.Windows.Forms.Button()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtIva = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txt12Iva = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtDescuento = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txt0Iva = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnActulProducro = New System.Windows.Forms.Button()
        Me.btnNuevoProducto = New System.Windows.Forms.Button()
        Me.btnVerProducto = New System.Windows.Forms.Button()
        Me.txtcodProdcuto = New System.Windows.Forms.TextBox()
        Me.txtValueResul = New System.Windows.Forms.TextBox()
        Me.txtOrden = New System.Windows.Forms.TextBox()
        Me.txtIdFormaPago = New System.Windows.Forms.TextBox()
        Me.dtFechaPedido = New System.Windows.Forms.DateTimePicker()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtNumDoc = New System.Windows.Forms.TextBox()
        Me.cmbTipoDocumento = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmbDeclaracion = New System.Windows.Forms.ComboBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ContextMenuListCompra.SuspendLayout()
        Me.ContextMenuListPago.SuspendLayout()
        Me.grupDetail.SuspendLayout()
        Me.Groptotal.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListView1
        '
        Me.ListView1.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.codigo, Me.producto, Me.Cantidad, Me.PUnitario, Me.Descuento, Me.Iva, Me.PTotal, Me.TypeCostoColumn, Me.IdPresentColumn})
        Me.ListView1.ContextMenuStrip = Me.ContextMenuListCompra
        Me.ListView1.FullRowSelect = True
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(10, 19)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(960, 159)
        Me.ListView1.TabIndex = 13
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'codigo
        '
        Me.codigo.Text = "Codigo"
        Me.codigo.Width = 50
        '
        'producto
        '
        Me.producto.Text = "Producto"
        Me.producto.Width = 300
        '
        'Cantidad
        '
        Me.Cantidad.Text = "Cantidad"
        Me.Cantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Cantidad.Width = 80
        '
        'PUnitario
        '
        Me.PUnitario.Text = "P/Unitario"
        Me.PUnitario.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.PUnitario.Width = 120
        '
        'Descuento
        '
        Me.Descuento.Text = "Descuento $"
        Me.Descuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Descuento.Width = 120
        '
        'Iva
        '
        Me.Iva.Text = "Iva $"
        Me.Iva.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Iva.Width = 80
        '
        'PTotal
        '
        Me.PTotal.Text = "Sub Total"
        Me.PTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.PTotal.Width = 150
        '
        'TypeCostoColumn
        '
        Me.TypeCostoColumn.Text = "Incluye Iva"
        Me.TypeCostoColumn.Width = 109
        '
        'IdPresentColumn
        '
        Me.IdPresentColumn.Text = "IdPresent"
        Me.IdPresentColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.IdPresentColumn.Width = 0
        '
        'ContextMenuListCompra
        '
        Me.ContextMenuListCompra.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuEmilinar, Me.ToolStripSeparator1, Me.menuModificar})
        Me.ContextMenuListCompra.Name = "ContextMenuListCompra"
        Me.ContextMenuListCompra.Size = New System.Drawing.Size(173, 54)
        '
        'menuEmilinar
        '
        Me.menuEmilinar.Name = "menuEmilinar"
        Me.menuEmilinar.Size = New System.Drawing.Size(172, 22)
        Me.menuEmilinar.Text = "Eliminar Fila.."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(169, 6)
        '
        'menuModificar
        '
        Me.menuModificar.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuCantidad, Me.menuPUnitario, Me.menuDescuento, Me.menuIva, Me.menuPtotal})
        Me.menuModificar.Name = "menuModificar"
        Me.menuModificar.Size = New System.Drawing.Size(172, 22)
        Me.menuModificar.Text = "Modificar Valores.."
        '
        'menuCantidad
        '
        Me.menuCantidad.Name = "menuCantidad"
        Me.menuCantidad.Size = New System.Drawing.Size(152, 22)
        Me.menuCantidad.Text = "Cantidad"
        '
        'menuPUnitario
        '
        Me.menuPUnitario.Name = "menuPUnitario"
        Me.menuPUnitario.Size = New System.Drawing.Size(152, 22)
        Me.menuPUnitario.Text = "Precio Unitario"
        '
        'menuDescuento
        '
        Me.menuDescuento.Name = "menuDescuento"
        Me.menuDescuento.Size = New System.Drawing.Size(152, 22)
        Me.menuDescuento.Text = "Descuento"
        '
        'menuIva
        '
        Me.menuIva.Name = "menuIva"
        Me.menuIva.Size = New System.Drawing.Size(152, 22)
        Me.menuIva.Text = "Iva..."
        '
        'menuPtotal
        '
        Me.menuPtotal.Name = "menuPtotal"
        Me.menuPtotal.Size = New System.Drawing.Size(152, 22)
        Me.menuPtotal.Text = "Precio Total"
        '
        'ContextMenuListPago
        '
        Me.ContextMenuListPago.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuSelecPago})
        Me.ContextMenuListPago.Name = "ContextMenuListPago"
        Me.ContextMenuListPago.Size = New System.Drawing.Size(135, 26)
        '
        'menuSelecPago
        '
        Me.menuSelecPago.Name = "menuSelecPago"
        Me.menuSelecPago.Size = New System.Drawing.Size(134, 22)
        Me.menuSelecPago.Text = "Seleccionar"
        '
        'cmbProveedor
        '
        Me.cmbProveedor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbProveedor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbProveedor.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProveedor.ForeColor = System.Drawing.Color.Red
        Me.cmbProveedor.Location = New System.Drawing.Point(221, 88)
        Me.cmbProveedor.Name = "cmbProveedor"
        Me.cmbProveedor.Size = New System.Drawing.Size(402, 27)
        Me.cmbProveedor.TabIndex = 14
        Me.cmbProveedor.Text = "Selecione..."
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Red
        Me.Label9.Location = New System.Drawing.Point(98, 90)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(117, 19)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "PROVEEDOR:"
        '
        'dtFechaCompra
        '
        Me.dtFechaCompra.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtFechaCompra.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtFechaCompra.Location = New System.Drawing.Point(221, 9)
        Me.dtFechaCompra.Name = "dtFechaCompra"
        Me.dtFechaCompra.Size = New System.Drawing.Size(184, 30)
        Me.dtFechaCompra.TabIndex = 0
        Me.dtFechaCompra.Value = New Date(2014, 1, 19, 16, 25, 39, 0)
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Red
        Me.Label14.Location = New System.Drawing.Point(12, 9)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(203, 23)
        Me.Label14.TabIndex = 16
        Me.Label14.Text = "FECHA DE COMPRA:"
        '
        'cmbItmTipconsumo
        '
        Me.cmbItmTipconsumo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbItmTipconsumo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbItmTipconsumo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbItmTipconsumo.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbItmTipconsumo.Location = New System.Drawing.Point(221, 40)
        Me.cmbItmTipconsumo.Name = "cmbItmTipconsumo"
        Me.cmbItmTipconsumo.Size = New System.Drawing.Size(183, 24)
        Me.cmbItmTipconsumo.TabIndex = 20
        Me.cmbItmTipconsumo.Text = "Selecione...."
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Green
        Me.Label2.Location = New System.Drawing.Point(83, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(127, 16)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "TIPO DE CONSUMO:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(354, 68)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(58, 13)
        Me.Label10.TabIndex = 22
        Me.Label10.Text = "(Ruc)(C.I.):"
        '
        'txtruc
        '
        Me.txtruc.Enabled = False
        Me.txtruc.Location = New System.Drawing.Point(418, 65)
        Me.txtruc.Name = "txtruc"
        Me.txtruc.Size = New System.Drawing.Size(232, 20)
        Me.txtruc.TabIndex = 21
        '
        'btnNewProveedor
        '
        Me.btnNewProveedor.BackgroundImage =  Global.DanashaBasic.My.Resources.new_32
        Me.btnNewProveedor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnNewProveedor.Location = New System.Drawing.Point(636, 86)
        Me.btnNewProveedor.Name = "btnNewProveedor"
        Me.btnNewProveedor.Size = New System.Drawing.Size(35, 32)
        Me.btnNewProveedor.TabIndex = 23
        Me.btnNewProveedor.UseVisualStyleBackColor = True
        '
        'btnListProveedor
        '
        Me.btnListProveedor.BackgroundImage =  Global.DanashaBasic.My.Resources.list_48
        Me.btnListProveedor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnListProveedor.Location = New System.Drawing.Point(676, 86)
        Me.btnListProveedor.Name = "btnListProveedor"
        Me.btnListProveedor.Size = New System.Drawing.Size(36, 32)
        Me.btnListProveedor.TabIndex = 24
        Me.btnListProveedor.UseVisualStyleBackColor = True
        '
        'btnAddCabConsumo
        '
        Me.btnAddCabConsumo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnAddCabConsumo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAddCabConsumo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddCabConsumo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnAddCabConsumo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAddCabConsumo.Location = New System.Drawing.Point(771, 114)
        Me.btnAddCabConsumo.Name = "btnAddCabConsumo"
        Me.btnAddCabConsumo.Size = New System.Drawing.Size(200, 27)
        Me.btnAddCabConsumo.TabIndex = 25
        Me.btnAddCabConsumo.Text = "Agregar Articulos"
        Me.btnAddCabConsumo.UseVisualStyleBackColor = True
        '
        'grupDetail
        '
        Me.grupDetail.Controls.Add(Me.btnListPtduct)
        Me.grupDetail.Controls.Add(Me.cmbFormaPago)
        Me.grupDetail.Controls.Add(Me.Label12)
        Me.grupDetail.Controls.Add(Me.btnGuardar)
        Me.grupDetail.Controls.Add(Me.Label11)
        Me.grupDetail.Controls.Add(Me.txtCantidad)
        Me.grupDetail.Controls.Add(Me.btnAddListaProduc)
        Me.grupDetail.Controls.Add(Me.cmbItemProducto)
        Me.grupDetail.Controls.Add(Me.Groptotal)
        Me.grupDetail.Controls.Add(Me.Label3)
        Me.grupDetail.Controls.Add(Me.ListView1)
        Me.grupDetail.Controls.Add(Me.btnActulProducro)
        Me.grupDetail.Controls.Add(Me.btnNuevoProducto)
        Me.grupDetail.Controls.Add(Me.btnVerProducto)
        Me.grupDetail.Enabled = False
        Me.grupDetail.Location = New System.Drawing.Point(2, 142)
        Me.grupDetail.Name = "grupDetail"
        Me.grupDetail.Size = New System.Drawing.Size(973, 350)
        Me.grupDetail.TabIndex = 26
        Me.grupDetail.TabStop = False
        '
        'btnListPtduct
        '
        Me.btnListPtduct.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnListPtduct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnListPtduct.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnListPtduct.Location = New System.Drawing.Point(704, 188)
        Me.btnListPtduct.Name = "btnListPtduct"
        Me.btnListPtduct.Size = New System.Drawing.Size(37, 32)
        Me.btnListPtduct.TabIndex = 37
        Me.ToolTip1.SetToolTip(Me.btnListPtduct, "Listado de productos...")
        Me.btnListPtduct.UseVisualStyleBackColor = True
        '
        'cmbFormaPago
        '
        Me.cmbFormaPago.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbFormaPago.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbFormaPago.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbFormaPago.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbFormaPago.Location = New System.Drawing.Point(512, 301)
        Me.cmbFormaPago.Name = "cmbFormaPago"
        Me.cmbFormaPago.Size = New System.Drawing.Size(287, 26)
        Me.cmbFormaPago.TabIndex = 31
        '
        'Label12
        '
        Me.Label12.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Green
        Me.Label12.Location = New System.Drawing.Point(363, 304)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(148, 19)
        Me.Label12.TabIndex = 30
        Me.Label12.Text = "FORMA DE RAGO"
        '
        'btnGuardar
        '
        Me.btnGuardar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGuardar.BackgroundImage =  Global.DanashaBasic.My.Resources.save_as_64
        Me.btnGuardar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnGuardar.Location = New System.Drawing.Point(897, 280)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(70, 64)
        Me.btnGuardar.TabIndex = 36
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(761, 197)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(49, 13)
        Me.Label11.TabIndex = 34
        Me.Label11.Text = "Cantidad"
        '
        'txtCantidad
        '
        Me.txtCantidad.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCantidad.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantidad.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCantidad.Location = New System.Drawing.Point(816, 196)
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.Size = New System.Drawing.Size(84, 22)
        Me.txtCantidad.TabIndex = 33
        Me.txtCantidad.Text = "0"
        Me.txtCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnAddListaProduc
        '
        Me.btnAddListaProduc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddListaProduc.BackColor = System.Drawing.Color.Transparent
        Me.btnAddListaProduc.BackgroundImage = CType(resources.GetObject("btnAddListaProduc.BackgroundImage"), System.Drawing.Image)
        Me.btnAddListaProduc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddListaProduc.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddListaProduc.Location = New System.Drawing.Point(906, 186)
        Me.btnAddListaProduc.Name = "btnAddListaProduc"
        Me.btnAddListaProduc.Size = New System.Drawing.Size(61, 53)
        Me.btnAddListaProduc.TabIndex = 32
        Me.btnAddListaProduc.Tag = ""
        Me.btnAddListaProduc.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.btnAddListaProduc.UseVisualStyleBackColor = False
        '
        'cmbItemProducto
        '
        Me.cmbItemProducto.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbItemProducto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbItemProducto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbItemProducto.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbItemProducto.Location = New System.Drawing.Point(109, 190)
        Me.cmbItemProducto.Name = "cmbItemProducto"
        Me.cmbItemProducto.Size = New System.Drawing.Size(477, 26)
        Me.cmbItemProducto.TabIndex = 28
        Me.cmbItemProducto.Text = "Selecione...."
        '
        'Groptotal
        '
        Me.Groptotal.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Groptotal.BackColor = System.Drawing.Color.LightBlue
        Me.Groptotal.Controls.Add(Me.cbxRedondSin)
        Me.Groptotal.Controls.Add(Me.cbxRedondCon)
        Me.Groptotal.Controls.Add(Me.Label1)
        Me.Groptotal.Controls.Add(Me.txtLugarDecimal)
        Me.Groptotal.Controls.Add(Me.btnCalTotal)
        Me.Groptotal.Controls.Add(Me.txtTotal)
        Me.Groptotal.Controls.Add(Me.Label8)
        Me.Groptotal.Controls.Add(Me.txtIva)
        Me.Groptotal.Controls.Add(Me.Label7)
        Me.Groptotal.Controls.Add(Me.txt12Iva)
        Me.Groptotal.Controls.Add(Me.Label6)
        Me.Groptotal.Controls.Add(Me.txtDescuento)
        Me.Groptotal.Controls.Add(Me.Label5)
        Me.Groptotal.Controls.Add(Me.txt0Iva)
        Me.Groptotal.Controls.Add(Me.Label4)
        Me.Groptotal.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Groptotal.Location = New System.Drawing.Point(6, 224)
        Me.Groptotal.Name = "Groptotal"
        Me.Groptotal.Size = New System.Drawing.Size(872, 69)
        Me.Groptotal.TabIndex = 14
        Me.Groptotal.TabStop = False
        '
        'cbxRedondSin
        '
        Me.cbxRedondSin.AutoSize = True
        Me.cbxRedondSin.Location = New System.Drawing.Point(183, 36)
        Me.cbxRedondSin.Name = "cbxRedondSin"
        Me.cbxRedondSin.Size = New System.Drawing.Size(102, 18)
        Me.cbxRedondSin.TabIndex = 5
        Me.cbxRedondSin.Text = "Sin Redondeo"
        Me.cbxRedondSin.UseVisualStyleBackColor = True
        '
        'cbxRedondCon
        '
        Me.cbxRedondCon.AutoSize = True
        Me.cbxRedondCon.Checked = True
        Me.cbxRedondCon.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbxRedondCon.Location = New System.Drawing.Point(183, 11)
        Me.cbxRedondCon.Name = "cbxRedondCon"
        Me.cbxRedondCon.Size = New System.Drawing.Size(104, 18)
        Me.cbxRedondCon.TabIndex = 5
        Me.cbxRedondCon.Text = "Con redondeo"
        Me.cbxRedondCon.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(86, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 14)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Lugar Decimal"
        '
        'txtLugarDecimal
        '
        Me.txtLugarDecimal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtLugarDecimal.Location = New System.Drawing.Point(86, 34)
        Me.txtLugarDecimal.MaxLength = 1
        Me.txtLugarDecimal.Name = "txtLugarDecimal"
        Me.txtLugarDecimal.Size = New System.Drawing.Size(83, 22)
        Me.txtLugarDecimal.TabIndex = 3
        Me.txtLugarDecimal.Text = "2"
        Me.txtLugarDecimal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnCalTotal
        '
        Me.btnCalTotal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnCalTotal.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCalTotal.Location = New System.Drawing.Point(4, 14)
        Me.btnCalTotal.Name = "btnCalTotal"
        Me.btnCalTotal.Size = New System.Drawing.Size(76, 48)
        Me.btnCalTotal.TabIndex = 2
        Me.btnCalTotal.Text = "Calcula Total.."
        Me.btnCalTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCalTotal.UseVisualStyleBackColor = True
        '
        'txtTotal
        '
        Me.txtTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.ForeColor = System.Drawing.Color.Red
        Me.txtTotal.Location = New System.Drawing.Point(718, 30)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Size = New System.Drawing.Size(129, 26)
        Me.txtTotal.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Red
        Me.Label8.Location = New System.Drawing.Point(733, 9)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(126, 20)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Total a Pagar  "
        '
        'txtIva
        '
        Me.txtIva.Location = New System.Drawing.Point(647, 33)
        Me.txtIva.Name = "txtIva"
        Me.txtIva.Size = New System.Drawing.Size(65, 22)
        Me.txtIva.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(650, 15)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(26, 14)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Iva"
        '
        'txt12Iva
        '
        Me.txt12Iva.Location = New System.Drawing.Point(430, 33)
        Me.txt12Iva.Name = "txt12Iva"
        Me.txt12Iva.Size = New System.Drawing.Size(105, 22)
        Me.txt12Iva.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(433, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(96, 14)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Base 12%  Iva"
        '
        'txtDescuento
        '
        Me.txtDescuento.Location = New System.Drawing.Point(541, 33)
        Me.txtDescuento.Name = "txtDescuento"
        Me.txtDescuento.Size = New System.Drawing.Size(100, 22)
        Me.txtDescuento.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(544, 15)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 14)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Descuento"
        '
        'txt0Iva
        '
        Me.txt0Iva.Location = New System.Drawing.Point(306, 33)
        Me.txt0Iva.Name = "txt0Iva"
        Me.txt0Iva.Size = New System.Drawing.Size(118, 22)
        Me.txt0Iva.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(309, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 14)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Base 0 % Iva"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(1, 193)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(102, 19)
        Me.Label3.TabIndex = 27
        Me.Label3.Text = "PRODUCTO"
        '
        'btnActulProducro
        '
        Me.btnActulProducro.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnActulProducro.BackgroundImage = CType(resources.GetObject("btnActulProducro.BackgroundImage"), System.Drawing.Image)
        Me.btnActulProducro.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnActulProducro.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnActulProducro.Location = New System.Drawing.Point(666, 188)
        Me.btnActulProducro.Name = "btnActulProducro"
        Me.btnActulProducro.Size = New System.Drawing.Size(37, 32)
        Me.btnActulProducro.TabIndex = 29
        Me.btnActulProducro.UseVisualStyleBackColor = True
        '
        'btnNuevoProducto
        '
        Me.btnNuevoProducto.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNuevoProducto.BackgroundImage = CType(resources.GetObject("btnNuevoProducto.BackgroundImage"), System.Drawing.Image)
        Me.btnNuevoProducto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnNuevoProducto.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnNuevoProducto.Location = New System.Drawing.Point(592, 188)
        Me.btnNuevoProducto.Name = "btnNuevoProducto"
        Me.btnNuevoProducto.Size = New System.Drawing.Size(37, 32)
        Me.btnNuevoProducto.TabIndex = 30
        Me.btnNuevoProducto.UseVisualStyleBackColor = True
        '
        'btnVerProducto
        '
        Me.btnVerProducto.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnVerProducto.BackgroundImage = CType(resources.GetObject("btnVerProducto.BackgroundImage"), System.Drawing.Image)
        Me.btnVerProducto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnVerProducto.Location = New System.Drawing.Point(629, 188)
        Me.btnVerProducto.Name = "btnVerProducto"
        Me.btnVerProducto.Size = New System.Drawing.Size(37, 32)
        Me.btnVerProducto.TabIndex = 31
        Me.btnVerProducto.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnVerProducto.UseVisualStyleBackColor = True
        '
        'txtcodProdcuto
        '
        Me.txtcodProdcuto.Enabled = False
        Me.txtcodProdcuto.Location = New System.Drawing.Point(762, 9)
        Me.txtcodProdcuto.Name = "txtcodProdcuto"
        Me.txtcodProdcuto.Size = New System.Drawing.Size(31, 20)
        Me.txtcodProdcuto.TabIndex = 35
        Me.txtcodProdcuto.Text = "0"
        Me.txtcodProdcuto.Visible = False
        '
        'txtValueResul
        '
        Me.txtValueResul.Location = New System.Drawing.Point(642, 9)
        Me.txtValueResul.Name = "txtValueResul"
        Me.txtValueResul.Size = New System.Drawing.Size(42, 20)
        Me.txtValueResul.TabIndex = 28
        Me.txtValueResul.Text = "0"
        Me.txtValueResul.Visible = False
        '
        'txtOrden
        '
        Me.txtOrden.Location = New System.Drawing.Point(690, 9)
        Me.txtOrden.Name = "txtOrden"
        Me.txtOrden.Size = New System.Drawing.Size(30, 20)
        Me.txtOrden.TabIndex = 29
        Me.txtOrden.Text = "0"
        Me.txtOrden.Visible = False
        '
        'txtIdFormaPago
        '
        Me.txtIdFormaPago.Location = New System.Drawing.Point(726, 9)
        Me.txtIdFormaPago.Name = "txtIdFormaPago"
        Me.txtIdFormaPago.Size = New System.Drawing.Size(30, 20)
        Me.txtIdFormaPago.TabIndex = 29
        Me.txtIdFormaPago.Text = "0"
        Me.txtIdFormaPago.Visible = False
        '
        'dtFechaPedido
        '
        Me.dtFechaPedido.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtFechaPedido.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtFechaPedido.Location = New System.Drawing.Point(411, 9)
        Me.dtFechaPedido.Name = "dtFechaPedido"
        Me.dtFechaPedido.Size = New System.Drawing.Size(67, 30)
        Me.dtFechaPedido.TabIndex = 17
        Me.dtFechaPedido.Value = New Date(2014, 1, 19, 16, 25, 39, 0)
        Me.dtFechaPedido.Visible = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(101, 124)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(114, 13)
        Me.Label13.TabIndex = 31
        Me.Label13.Text = "Numro de Documento:"
        '
        'txtNumDoc
        '
        Me.txtNumDoc.Location = New System.Drawing.Point(222, 121)
        Me.txtNumDoc.Name = "txtNumDoc"
        Me.txtNumDoc.Size = New System.Drawing.Size(232, 20)
        Me.txtNumDoc.TabIndex = 30
        '
        'cmbTipoDocumento
        '
        Me.cmbTipoDocumento.FormattingEnabled = True
        Me.cmbTipoDocumento.Location = New System.Drawing.Point(517, 120)
        Me.cmbTipoDocumento.Name = "cmbTipoDocumento"
        Me.cmbTipoDocumento.Size = New System.Drawing.Size(203, 21)
        Me.cmbTipoDocumento.TabIndex = 32
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(485, 124)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(31, 13)
        Me.Label15.TabIndex = 31
        Me.Label15.Text = "Tipo:"
        '
        'Label16
        '
        Me.Label16.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Green
        Me.Label16.Location = New System.Drawing.Point(768, 65)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(129, 16)
        Me.Label16.TabIndex = 19
        Me.Label16.Text = "DECLARACION SRI:"
        '
        'cmbDeclaracion
        '
        Me.cmbDeclaracion.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbDeclaracion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbDeclaracion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbDeclaracion.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDeclaracion.Location = New System.Drawing.Point(771, 84)
        Me.cmbDeclaracion.Name = "cmbDeclaracion"
        Me.cmbDeclaracion.Size = New System.Drawing.Size(183, 24)
        Me.cmbDeclaracion.TabIndex = 20
        Me.cmbDeclaracion.Text = "Selecione...."
        '
        'frmAnualconsumo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 500)
        Me.Controls.Add(Me.cmbTipoDocumento)
        Me.Controls.Add(Me.txtcodProdcuto)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtNumDoc)
        Me.Controls.Add(Me.txtIdFormaPago)
        Me.Controls.Add(Me.txtOrden)
        Me.Controls.Add(Me.txtValueResul)
        Me.Controls.Add(Me.grupDetail)
        Me.Controls.Add(Me.btnAddCabConsumo)
        Me.Controls.Add(Me.btnNewProveedor)
        Me.Controls.Add(Me.btnListProveedor)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtruc)
        Me.Controls.Add(Me.cmbDeclaracion)
        Me.Controls.Add(Me.cmbItmTipconsumo)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dtFechaPedido)
        Me.Controls.Add(Me.dtFechaCompra)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.cmbProveedor)
        Me.Controls.Add(Me.Label9)
        Me.Name = "frmAnualconsumo"
        Me.Text = "frmAnualconsumo"
        Me.ContextMenuListCompra.ResumeLayout(False)
        Me.ContextMenuListPago.ResumeLayout(False)
        Me.grupDetail.ResumeLayout(False)
        Me.grupDetail.PerformLayout()
        Me.Groptotal.ResumeLayout(False)
        Me.Groptotal.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents codigo As System.Windows.Forms.ColumnHeader
    Friend WithEvents producto As System.Windows.Forms.ColumnHeader
    Friend WithEvents Cantidad As System.Windows.Forms.ColumnHeader
    Friend WithEvents PUnitario As System.Windows.Forms.ColumnHeader
    Friend WithEvents Descuento As System.Windows.Forms.ColumnHeader
    Friend WithEvents Iva As System.Windows.Forms.ColumnHeader
    Friend WithEvents PTotal As System.Windows.Forms.ColumnHeader
    Friend WithEvents TypeCostoColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents IdPresentColumn As System.Windows.Forms.ColumnHeader
    Private WithEvents cmbProveedor As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dtFechaCompra As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cmbItmTipconsumo As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtruc As System.Windows.Forms.TextBox
    Friend WithEvents btnNewProveedor As System.Windows.Forms.Button
    Friend WithEvents btnListProveedor As System.Windows.Forms.Button
    Friend WithEvents btnAddCabConsumo As System.Windows.Forms.Button
    Friend WithEvents grupDetail As System.Windows.Forms.GroupBox
    Friend WithEvents Groptotal As System.Windows.Forms.GroupBox
    Friend WithEvents cbxRedondSin As System.Windows.Forms.CheckBox
    Friend WithEvents cbxRedondCon As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtLugarDecimal As System.Windows.Forms.TextBox
    Friend WithEvents btnCalTotal As System.Windows.Forms.Button
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtIva As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txt12Iva As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtDescuento As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txt0Iva As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbItemProducto As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnActulProducro As System.Windows.Forms.Button
    Friend WithEvents btnNuevoProducto As System.Windows.Forms.Button
    Friend WithEvents btnVerProducto As System.Windows.Forms.Button
    Friend WithEvents btnAddListaProduc As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtCantidad As System.Windows.Forms.TextBox
    Friend WithEvents txtcodProdcuto As System.Windows.Forms.TextBox
    Friend WithEvents ContextMenuListPago As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents menuSelecPago As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtValueResul As System.Windows.Forms.TextBox
    Friend WithEvents ContextMenuListCompra As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents menuEmilinar As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents menuModificar As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuCantidad As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuPUnitario As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuDescuento As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuIva As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents menuPtotal As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents txtOrden As System.Windows.Forms.TextBox
    Friend WithEvents cmbFormaPago As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtIdFormaPago As System.Windows.Forms.TextBox
    Friend WithEvents dtFechaPedido As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtNumDoc As System.Windows.Forms.TextBox
    Friend WithEvents cmbTipoDocumento As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cmbDeclaracion As System.Windows.Forms.ComboBox
    Friend WithEvents btnListPtduct As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class
