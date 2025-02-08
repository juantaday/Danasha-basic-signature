<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmList_Facturas
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
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.printTicket = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSelectAll = New System.Windows.Forms.Button()
        Me.detailButton = New System.Windows.Forms.Button()
        Me.selectAllCheckBox = New System.Windows.Forms.CheckBox()
        Me.anulaButton = New System.Windows.Forms.Button()
        Me.setIsPrinterButton = New System.Windows.Forms.Button()
        Me.ListViewCabecera = New System.Windows.Forms.ListView()
        Me.IdFactureColum = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FacturColum = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Nom_Docu = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ClienteColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.fecDesColum = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.FecHastColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Base0Colum = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Base12Column = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.IvaColum = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clmOtroValor = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TotalColum = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.DireccColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TipVentcolumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.RucColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ListViewDetail = New System.Windows.Forms.ListView()
        Me.CantidadColum = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clmEmpaque = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ArticuloColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pvpColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TotalColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ivaColumn = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.clmTotalDecimal = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblCountItem = New System.Windows.Forms.Label()
        Me.bntBuscar = New System.Windows.Forms.Button()
        Me.txtbuscar = New System.Windows.Forms.TextBox()
        Me.CmbOptionSelect = New System.Windows.Forms.ComboBox()
        Me.paneTitulo = New System.Windows.Forms.Panel()
        Me.PanePie = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lbltotalFactur = New System.Windows.Forms.Label()
        Me.lblNoInforcion = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.btnNoselect = New System.Windows.Forms.Button()
        Me.btnCopy = New System.Windows.Forms.Button()
        Me.FlowLayoutPanel3 = New System.Windows.Forms.FlowLayoutPanel()
        Me.FlowLayoutPanel4 = New System.Windows.Forms.FlowLayoutPanel()
        Me.PanelImputDate = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DateTimePickerStar = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerEnd = New System.Windows.Forms.DateTimePicker()
        Me.PaneCentral = New System.Windows.Forms.Panel()
        Me.PanelView = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelControls = New System.Windows.Forms.Panel()
        Me.printMatricialButton = New System.Windows.Forms.Button()
        Me.viewReportButton = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblTituloDetalle = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.PaneDetalle = New System.Windows.Forms.Panel()
        Me.ChangCustomerButton = New System.Windows.Forms.Button()
        Me.paneTitulo.SuspendLayout()
        Me.PanePie.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.FlowLayoutPanel3.SuspendLayout()
        Me.FlowLayoutPanel4.SuspendLayout()
        Me.PanelImputDate.SuspendLayout()
        Me.PaneCentral.SuspendLayout()
        Me.PanelView.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.PanelControls.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.PaneDetalle.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblTitle.Font = New System.Drawing.Font("Tahoma", 22.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.Blue
        Me.lblTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblTitle.Margin = New System.Windows.Forms.Padding(3)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(574, 36)
        Me.lblTitle.TabIndex = 20
        Me.lblTitle.Text = "Facturas, notas de venta y proformas"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Timer1
        '
        Me.Timer1.Interval = 500
        '
        'printTicket
        '
        Me.printTicket.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.printTicket.Dock = System.Windows.Forms.DockStyle.Left
        Me.printTicket.Image = Global.DanashaBasicSignature.My.Resources.Resources.Action_Printing_Print_32x32
        Me.printTicket.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.printTicket.Location = New System.Drawing.Point(116, 0)
        Me.printTicket.Name = "printTicket"
        Me.printTicket.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.printTicket.Size = New System.Drawing.Size(47, 42)
        Me.printTicket.TabIndex = 23
        Me.printTicket.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.printTicket, "Imprime un maximo de 12 documentos consecutivos" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Iniciando desde la primera fila " &
        "del listado")
        Me.printTicket.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(74, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(114, 16)
        Me.Label1.TabIndex = 75
        Me.Label1.Text = "Saltar espacios ...."
        Me.ToolTip1.SetToolTip(Me.Label1, "Espacios en blanco a saltar antes de imprimir la primera linea")
        '
        'btnSelectAll
        '
        Me.btnSelectAll.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.btnSelectAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnSelectAll.Image = Global.DanashaBasicSignature.My.Resources.Resources.SelectAll_16x16
        Me.btnSelectAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSelectAll.Location = New System.Drawing.Point(3, 3)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(114, 25)
        Me.btnSelectAll.TabIndex = 24
        Me.btnSelectAll.Text = "Seleccionar todo"
        Me.btnSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnSelectAll, "Selecionar todo el listado")
        Me.btnSelectAll.UseVisualStyleBackColor = True
        '
        'detailButton
        '
        Me.detailButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.detailButton.Location = New System.Drawing.Point(822, 22)
        Me.detailButton.Name = "detailButton"
        Me.detailButton.Size = New System.Drawing.Size(66, 19)
        Me.detailButton.TabIndex = 27
        Me.detailButton.Text = "........"
        Me.ToolTip1.SetToolTip(Me.detailButton, "Ver detalle de Documento")
        Me.detailButton.UseVisualStyleBackColor = True
        '
        'selectAllCheckBox
        '
        Me.selectAllCheckBox.Dock = System.Windows.Forms.DockStyle.Left
        Me.selectAllCheckBox.Location = New System.Drawing.Point(8, 0)
        Me.selectAllCheckBox.Name = "selectAllCheckBox"
        Me.selectAllCheckBox.Size = New System.Drawing.Size(25, 42)
        Me.selectAllCheckBox.TabIndex = 29
        Me.ToolTip1.SetToolTip(Me.selectAllCheckBox, "Seleccionar todo..")
        Me.selectAllCheckBox.UseVisualStyleBackColor = True
        '
        'anulaButton
        '
        Me.anulaButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.anulaButton.Image = Global.DanashaBasicSignature.My.Resources.Resources.NullDocument_32
        Me.anulaButton.Location = New System.Drawing.Point(163, 0)
        Me.anulaButton.Name = "anulaButton"
        Me.anulaButton.Size = New System.Drawing.Size(60, 42)
        Me.anulaButton.TabIndex = 26
        Me.ToolTip1.SetToolTip(Me.anulaButton, "Anular documento")
        Me.anulaButton.UseVisualStyleBackColor = True
        '
        'setIsPrinterButton
        '
        Me.setIsPrinterButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.setIsPrinterButton.Image = Global.DanashaBasicSignature.My.Resources.Resources.printMatrix_ok_32
        Me.setIsPrinterButton.Location = New System.Drawing.Point(223, 0)
        Me.setIsPrinterButton.Name = "setIsPrinterButton"
        Me.setIsPrinterButton.Size = New System.Drawing.Size(54, 42)
        Me.setIsPrinterButton.TabIndex = 27
        Me.ToolTip1.SetToolTip(Me.setIsPrinterButton, "Establecer como documentos ya impresos.")
        Me.setIsPrinterButton.UseVisualStyleBackColor = True
        '
        'ListViewCabecera
        '
        Me.ListViewCabecera.CheckBoxes = True
        Me.ListViewCabecera.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.IdFactureColum, Me.FacturColum, Me.Nom_Docu, Me.ClienteColumn, Me.fecDesColum, Me.FecHastColumn, Me.Base0Colum, Me.Base12Column, Me.IvaColum, Me.clmOtroValor, Me.TotalColum, Me.DireccColumn, Me.TipVentcolumn, Me.RucColumn})
        Me.ListViewCabecera.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewCabecera.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListViewCabecera.FullRowSelect = True
        Me.ListViewCabecera.GridLines = True
        Me.ListViewCabecera.HideSelection = False
        Me.ListViewCabecera.Location = New System.Drawing.Point(0, 0)
        Me.ListViewCabecera.Name = "ListViewCabecera"
        Me.ListViewCabecera.Size = New System.Drawing.Size(890, 416)
        Me.ListViewCabecera.TabIndex = 66
        Me.ListViewCabecera.UseCompatibleStateImageBehavior = False
        Me.ListViewCabecera.View = System.Windows.Forms.View.Details
        '
        'IdFactureColum
        '
        Me.IdFactureColum.Text = "ID"
        Me.IdFactureColum.Width = 150
        '
        'FacturColum
        '
        Me.FacturColum.Text = "Num documeto"
        Me.FacturColum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.FacturColum.Width = 188
        '
        'Nom_Docu
        '
        Me.Nom_Docu.Text = "Tipo documento"
        Me.Nom_Docu.Width = 180
        '
        'ClienteColumn
        '
        Me.ClienteColumn.Text = "Cliente"
        Me.ClienteColumn.Width = 150
        '
        'fecDesColum
        '
        Me.fecDesColum.Text = "Fecha Desde"
        Me.fecDesColum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.fecDesColum.Width = 100
        '
        'FecHastColumn
        '
        Me.FecHastColumn.Text = "Fecha Hasta "
        Me.FecHastColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.FecHastColumn.Width = 100
        '
        'Base0Colum
        '
        Me.Base0Colum.Text = "Exento de IVA"
        Me.Base0Colum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Base0Colum.Width = 70
        '
        'Base12Column
        '
        Me.Base12Column.Text = "Base IVA"
        Me.Base12Column.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Base12Column.Width = 81
        '
        'IvaColum
        '
        Me.IvaColum.Text = "IVA"
        Me.IvaColum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'clmOtroValor
        '
        Me.clmOtroValor.Text = "OtrosValores"
        Me.clmOtroValor.Width = 95
        '
        'TotalColum
        '
        Me.TotalColum.Text = "Total"
        Me.TotalColum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TotalColum.Width = 104
        '
        'DireccColumn
        '
        Me.DireccColumn.Text = "Direccion"
        Me.DireccColumn.Width = 0
        '
        'TipVentcolumn
        '
        Me.TipVentcolumn.Text = "tipoVenta"
        Me.TipVentcolumn.Width = 0
        '
        'RucColumn
        '
        Me.RucColumn.Text = "Ruc"
        Me.RucColumn.Width = 0
        '
        'ListViewDetail
        '
        Me.ListViewDetail.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.CantidadColum, Me.clmEmpaque, Me.ArticuloColumn, Me.pvpColumn, Me.TotalColumn, Me.ivaColumn, Me.clmTotalDecimal})
        Me.ListViewDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewDetail.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListViewDetail.FullRowSelect = True
        Me.ListViewDetail.GridLines = True
        Me.ListViewDetail.HideSelection = False
        Me.ListViewDetail.Location = New System.Drawing.Point(0, 35)
        Me.ListViewDetail.Name = "ListViewDetail"
        Me.ListViewDetail.Size = New System.Drawing.Size(321, 383)
        Me.ListViewDetail.TabIndex = 67
        Me.ListViewDetail.UseCompatibleStateImageBehavior = False
        Me.ListViewDetail.View = System.Windows.Forms.View.Details
        '
        'CantidadColum
        '
        Me.CantidadColum.Text = "Cantidad"
        Me.CantidadColum.Width = 74
        '
        'clmEmpaque
        '
        Me.clmEmpaque.Text = "Epm"
        Me.clmEmpaque.Width = 45
        '
        'ArticuloColumn
        '
        Me.ArticuloColumn.Text = "Articulo"
        Me.ArticuloColumn.Width = 164
        '
        'pvpColumn
        '
        Me.pvpColumn.Text = "P.Und"
        Me.pvpColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TotalColumn
        '
        Me.TotalColumn.Text = "Total"
        Me.TotalColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TotalColumn.Width = 100
        '
        'ivaColumn
        '
        Me.ivaColumn.Text = "Iva"
        Me.ivaColumn.Width = 0
        '
        'clmTotalDecimal
        '
        Me.clmTotalDecimal.Text = "total2Dijitos"
        '
        'lblCountItem
        '
        Me.lblCountItem.AutoSize = True
        Me.lblCountItem.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCountItem.Location = New System.Drawing.Point(3, 3)
        Me.lblCountItem.Margin = New System.Windows.Forms.Padding(3)
        Me.lblCountItem.Name = "lblCountItem"
        Me.lblCountItem.Size = New System.Drawing.Size(104, 16)
        Me.lblCountItem.TabIndex = 24
        Me.lblCountItem.Text = "Total articulos :0"
        '
        'bntBuscar
        '
        Me.bntBuscar.BackColor = System.Drawing.Color.Transparent
        Me.bntBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.bntBuscar.Enabled = False
        Me.bntBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bntBuscar.Location = New System.Drawing.Point(996, 3)
        Me.bntBuscar.Name = "bntBuscar"
        Me.bntBuscar.Size = New System.Drawing.Size(78, 26)
        Me.bntBuscar.TabIndex = 70
        Me.bntBuscar.Text = "Buscar...."
        Me.bntBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.bntBuscar.UseVisualStyleBackColor = True
        '
        'txtbuscar
        '
        Me.txtbuscar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtbuscar.Location = New System.Drawing.Point(210, 3)
        Me.txtbuscar.Name = "txtbuscar"
        Me.txtbuscar.Size = New System.Drawing.Size(431, 26)
        Me.txtbuscar.TabIndex = 0
        '
        'CmbOptionSelect
        '
        Me.CmbOptionSelect.FormattingEnabled = True
        Me.CmbOptionSelect.Items.AddRange(New Object() {"ID", "Cliente", "Número de Factura", "Ruc (o) C.I", "Fecha del documento", "No Impresas"})
        Me.CmbOptionSelect.Location = New System.Drawing.Point(3, 3)
        Me.CmbOptionSelect.Name = "CmbOptionSelect"
        Me.CmbOptionSelect.Size = New System.Drawing.Size(201, 24)
        Me.CmbOptionSelect.TabIndex = 71
        Me.CmbOptionSelect.Text = "Número de Factura"
        '
        'paneTitulo
        '
        Me.paneTitulo.BackColor = System.Drawing.Color.Gainsboro
        Me.paneTitulo.Controls.Add(Me.lblTitle)
        Me.paneTitulo.Dock = System.Windows.Forms.DockStyle.Top
        Me.paneTitulo.Location = New System.Drawing.Point(0, 0)
        Me.paneTitulo.Name = "paneTitulo"
        Me.paneTitulo.Size = New System.Drawing.Size(1215, 39)
        Me.paneTitulo.TabIndex = 77
        '
        'PanePie
        '
        Me.PanePie.BackColor = System.Drawing.SystemColors.Control
        Me.PanePie.Controls.Add(Me.btnClose)
        Me.PanePie.Controls.Add(Me.lbltotalFactur)
        Me.PanePie.Controls.Add(Me.lblNoInforcion)
        Me.PanePie.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanePie.Location = New System.Drawing.Point(0, 564)
        Me.PanePie.Name = "PanePie"
        Me.PanePie.Size = New System.Drawing.Size(1215, 42)
        Me.PanePie.TabIndex = 78
        '
        'btnClose
        '
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClose.Image = Global.DanashaBasicSignature.My.Resources.Resources.Close_32x32
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(1133, 0)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(82, 42)
        Me.btnClose.TabIndex = 33
        Me.btnClose.Text = "&Cerrar"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'lbltotalFactur
        '
        Me.lbltotalFactur.AutoSize = True
        Me.lbltotalFactur.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltotalFactur.Location = New System.Drawing.Point(30, 13)
        Me.lbltotalFactur.Name = "lbltotalFactur"
        Me.lbltotalFactur.Size = New System.Drawing.Size(100, 16)
        Me.lbltotalFactur.TabIndex = 24
        Me.lbltotalFactur.Text = "Total factura : 0"
        '
        'lblNoInforcion
        '
        Me.lblNoInforcion.AutoSize = True
        Me.lblNoInforcion.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoInforcion.ForeColor = System.Drawing.Color.Red
        Me.lblNoInforcion.Location = New System.Drawing.Point(10, 9)
        Me.lblNoInforcion.Name = "lblNoInforcion"
        Me.lblNoInforcion.Size = New System.Drawing.Size(237, 24)
        Me.lblNoInforcion.TabIndex = 73
        Me.lblNoInforcion.Text = "No existe información...."
        Me.lblNoInforcion.Visible = False
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.FlowLayoutPanel1.Controls.Add(Me.btnSelectAll)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnNoselect)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnCopy)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 39)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(1215, 32)
        Me.FlowLayoutPanel1.TabIndex = 79
        '
        'btnNoselect
        '
        Me.btnNoselect.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.btnNoselect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnNoselect.Image = Global.DanashaBasicSignature.My.Resources.Resources.SelectTable_16x16
        Me.btnNoselect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNoselect.Location = New System.Drawing.Point(123, 3)
        Me.btnNoselect.Name = "btnNoselect"
        Me.btnNoselect.Size = New System.Drawing.Size(143, 25)
        Me.btnNoselect.TabIndex = 24
        Me.btnNoselect.Text = "No seleccionar ninguno"
        Me.btnNoselect.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNoselect.UseVisualStyleBackColor = True
        '
        'btnCopy
        '
        Me.btnCopy.Image = Global.DanashaBasicSignature.My.Resources.Resources.Action_Copy
        Me.btnCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCopy.Location = New System.Drawing.Point(272, 3)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(60, 25)
        Me.btnCopy.TabIndex = 11
        Me.btnCopy.Text = "Copiar"
        Me.btnCopy.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCopy.UseVisualStyleBackColor = True
        '
        'FlowLayoutPanel3
        '
        Me.FlowLayoutPanel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.FlowLayoutPanel3.Controls.Add(Me.lblCountItem)
        Me.FlowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.FlowLayoutPanel3.Location = New System.Drawing.Point(0, 418)
        Me.FlowLayoutPanel3.Name = "FlowLayoutPanel3"
        Me.FlowLayoutPanel3.Size = New System.Drawing.Size(321, 42)
        Me.FlowLayoutPanel3.TabIndex = 1
        '
        'FlowLayoutPanel4
        '
        Me.FlowLayoutPanel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.FlowLayoutPanel4.Controls.Add(Me.CmbOptionSelect)
        Me.FlowLayoutPanel4.Controls.Add(Me.txtbuscar)
        Me.FlowLayoutPanel4.Controls.Add(Me.PanelImputDate)
        Me.FlowLayoutPanel4.Controls.Add(Me.bntBuscar)
        Me.FlowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.FlowLayoutPanel4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FlowLayoutPanel4.Location = New System.Drawing.Point(0, 71)
        Me.FlowLayoutPanel4.Name = "FlowLayoutPanel4"
        Me.FlowLayoutPanel4.Size = New System.Drawing.Size(1215, 33)
        Me.FlowLayoutPanel4.TabIndex = 81
        '
        'PanelImputDate
        '
        Me.PanelImputDate.Controls.Add(Me.Label5)
        Me.PanelImputDate.Controls.Add(Me.Label4)
        Me.PanelImputDate.Controls.Add(Me.DateTimePickerStar)
        Me.PanelImputDate.Controls.Add(Me.DateTimePickerEnd)
        Me.PanelImputDate.Location = New System.Drawing.Point(647, 3)
        Me.PanelImputDate.Name = "PanelImputDate"
        Me.PanelImputDate.Size = New System.Drawing.Size(343, 27)
        Me.PanelImputDate.TabIndex = 76
        Me.PanelImputDate.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(179, 6)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 17)
        Me.Label5.TabIndex = 75
        Me.Label5.Text = "Hasta: "
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(3, 6)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 17)
        Me.Label4.TabIndex = 74
        Me.Label4.Text = "Desde: "
        '
        'DateTimePickerStar
        '
        Me.DateTimePickerStar.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerStar.Location = New System.Drawing.Point(60, 3)
        Me.DateTimePickerStar.Name = "DateTimePickerStar"
        Me.DateTimePickerStar.Size = New System.Drawing.Size(99, 23)
        Me.DateTimePickerStar.TabIndex = 73
        '
        'DateTimePickerEnd
        '
        Me.DateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerEnd.Location = New System.Drawing.Point(234, 3)
        Me.DateTimePickerEnd.Name = "DateTimePickerEnd"
        Me.DateTimePickerEnd.Size = New System.Drawing.Size(99, 23)
        Me.DateTimePickerEnd.TabIndex = 72
        '
        'PaneCentral
        '
        Me.PaneCentral.Controls.Add(Me.PanelView)
        Me.PaneCentral.Controls.Add(Me.Panel1)
        Me.PaneCentral.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PaneCentral.Location = New System.Drawing.Point(0, 0)
        Me.PaneCentral.Name = "PaneCentral"
        Me.PaneCentral.Size = New System.Drawing.Size(890, 460)
        Me.PaneCentral.TabIndex = 82
        '
        'PanelView
        '
        Me.PanelView.Controls.Add(Me.ListViewCabecera)
        Me.PanelView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelView.Location = New System.Drawing.Point(0, 44)
        Me.PanelView.Name = "PanelView"
        Me.PanelView.Size = New System.Drawing.Size(890, 416)
        Me.PanelView.TabIndex = 68
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.PanelControls)
        Me.Panel1.Controls.Add(Me.detailButton)
        Me.Panel1.Controls.Add(Me.selectAllCheckBox)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(890, 44)
        Me.Panel1.TabIndex = 67
        '
        'PanelControls
        '
        Me.PanelControls.Controls.Add(Me.ChangCustomerButton)
        Me.PanelControls.Controls.Add(Me.setIsPrinterButton)
        Me.PanelControls.Controls.Add(Me.anulaButton)
        Me.PanelControls.Controls.Add(Me.printTicket)
        Me.PanelControls.Controls.Add(Me.printMatricialButton)
        Me.PanelControls.Controls.Add(Me.viewReportButton)
        Me.PanelControls.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelControls.Location = New System.Drawing.Point(33, 0)
        Me.PanelControls.Name = "PanelControls"
        Me.PanelControls.Size = New System.Drawing.Size(465, 42)
        Me.PanelControls.TabIndex = 28
        '
        'printMatricialButton
        '
        Me.printMatricialButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.printMatricialButton.Image = Global.DanashaBasicSignature.My.Resources.Resources.printMatrix_32
        Me.printMatricialButton.Location = New System.Drawing.Point(53, 0)
        Me.printMatricialButton.Name = "printMatricialButton"
        Me.printMatricialButton.Size = New System.Drawing.Size(63, 42)
        Me.printMatricialButton.TabIndex = 25
        Me.printMatricialButton.UseVisualStyleBackColor = True
        '
        'viewReportButton
        '
        Me.viewReportButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.viewReportButton.Image = Global.DanashaBasicSignature.My.Resources.Resources.fin_deudor_32
        Me.viewReportButton.Location = New System.Drawing.Point(0, 0)
        Me.viewReportButton.Name = "viewReportButton"
        Me.viewReportButton.Size = New System.Drawing.Size(53, 42)
        Me.viewReportButton.TabIndex = 24
        Me.viewReportButton.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(8, 42)
        Me.Panel3.TabIndex = 30
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Panel2.Controls.Add(Me.lblTituloDetalle)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(321, 35)
        Me.Panel2.TabIndex = 68
        '
        'lblTituloDetalle
        '
        Me.lblTituloDetalle.AutoSize = True
        Me.lblTituloDetalle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTituloDetalle.Location = New System.Drawing.Point(4, 8)
        Me.lblTituloDetalle.Name = "lblTituloDetalle"
        Me.lblTituloDetalle.Size = New System.Drawing.Size(213, 20)
        Me.lblTituloDetalle.TabIndex = 78
        Me.lblTituloDetalle.Text = "Detalle de la factura: 000035"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 104)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.PaneCentral)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.PaneDetalle)
        Me.SplitContainer1.Panel2.Margin = New System.Windows.Forms.Padding(3)
        Me.SplitContainer1.Size = New System.Drawing.Size(1215, 460)
        Me.SplitContainer1.SplitterDistance = 890
        Me.SplitContainer1.TabIndex = 82
        '
        'PaneDetalle
        '
        Me.PaneDetalle.Controls.Add(Me.ListViewDetail)
        Me.PaneDetalle.Controls.Add(Me.FlowLayoutPanel3)
        Me.PaneDetalle.Controls.Add(Me.Panel2)
        Me.PaneDetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PaneDetalle.Location = New System.Drawing.Point(0, 0)
        Me.PaneDetalle.Name = "PaneDetalle"
        Me.PaneDetalle.Size = New System.Drawing.Size(321, 460)
        Me.PaneDetalle.TabIndex = 0
        '
        'ChangCustomerButton
        '
        Me.ChangCustomerButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.ChangCustomerButton.Image = Global.DanashaBasicSignature.My.Resources.Resources.Users_20
        Me.ChangCustomerButton.Location = New System.Drawing.Point(277, 0)
        Me.ChangCustomerButton.Name = "ChangCustomerButton"
        Me.ChangCustomerButton.Size = New System.Drawing.Size(54, 42)
        Me.ChangCustomerButton.TabIndex = 28
        Me.ToolTip1.SetToolTip(Me.ChangCustomerButton, "Establecer como documentos ya impresos.")
        Me.ChangCustomerButton.UseVisualStyleBackColor = True
        '
        'frmList_Facturas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(1215, 606)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.FlowLayoutPanel4)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Controls.Add(Me.PanePie)
        Me.Controls.Add(Me.paneTitulo)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmList_Facturas"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmListFactura"
        Me.paneTitulo.ResumeLayout(False)
        Me.paneTitulo.PerformLayout()
        Me.PanePie.ResumeLayout(False)
        Me.PanePie.PerformLayout()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel3.ResumeLayout(False)
        Me.FlowLayoutPanel3.PerformLayout()
        Me.FlowLayoutPanel4.ResumeLayout(False)
        Me.FlowLayoutPanel4.PerformLayout()
        Me.PanelImputDate.ResumeLayout(False)
        Me.PanelImputDate.PerformLayout()
        Me.PaneCentral.ResumeLayout(False)
        Me.PanelView.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.PanelControls.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.PaneDetalle.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents printTicket As System.Windows.Forms.Button
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents ListViewCabecera As System.Windows.Forms.ListView
    Friend WithEvents IdFactureColum As System.Windows.Forms.ColumnHeader
    Friend WithEvents FacturColum As System.Windows.Forms.ColumnHeader
    Friend WithEvents ClienteColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents fecDesColum As System.Windows.Forms.ColumnHeader
    Friend WithEvents FecHastColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents Base0Colum As System.Windows.Forms.ColumnHeader
    Friend WithEvents Base12Column As System.Windows.Forms.ColumnHeader
    Friend WithEvents IvaColum As System.Windows.Forms.ColumnHeader
    Friend WithEvents TotalColum As System.Windows.Forms.ColumnHeader
    Friend WithEvents DireccColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents TipVentcolumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents RucColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents ListViewDetail As System.Windows.Forms.ListView
    Friend WithEvents CantidadColum As System.Windows.Forms.ColumnHeader
    Friend WithEvents ArticuloColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents pvpColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents TotalColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents ivaColumn As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblCountItem As System.Windows.Forms.Label
    Friend WithEvents bntBuscar As System.Windows.Forms.Button
    Friend WithEvents txtbuscar As System.Windows.Forms.TextBox
    Friend WithEvents CmbOptionSelect As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents paneTitulo As System.Windows.Forms.Panel
    Friend WithEvents PanePie As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents btnSelectAll As System.Windows.Forms.Button
    Friend WithEvents btnNoselect As System.Windows.Forms.Button
    Friend WithEvents btnCopy As System.Windows.Forms.Button
    Friend WithEvents FlowLayoutPanel3 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel4 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents PaneCentral As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblTituloDetalle As System.Windows.Forms.Label
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents clmEmpaque As System.Windows.Forms.ColumnHeader
    Friend WithEvents lbltotalFactur As System.Windows.Forms.Label
    Friend WithEvents lblNoInforcion As System.Windows.Forms.Label
    Friend WithEvents clmOtroValor As System.Windows.Forms.ColumnHeader
    Friend WithEvents PaneDetalle As System.Windows.Forms.Panel
    Friend WithEvents clmTotalDecimal As System.Windows.Forms.ColumnHeader
    Friend WithEvents Nom_Docu As ColumnHeader
    Friend WithEvents Panel1 As Panel
    Friend WithEvents anulaButton As Button
    Friend WithEvents printMatricialButton As Button
    Friend WithEvents viewReportButton As Button
    Friend WithEvents PanelView As Panel
    Friend WithEvents detailButton As Button
    Friend WithEvents PanelControls As Panel
    Friend WithEvents setIsPrinterButton As Button
    Friend WithEvents selectAllCheckBox As CheckBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents PanelImputDate As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents DateTimePickerStar As DateTimePicker
    Friend WithEvents DateTimePickerEnd As DateTimePicker
    Friend WithEvents ChangCustomerButton As Button
End Class
