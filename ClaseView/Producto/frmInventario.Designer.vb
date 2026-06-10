<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInventario
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
        Me.PanelMenu = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbLocalbodega = New System.Windows.Forms.ComboBox()
        Me.EditCountStockButton = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.txtProduc_Select = New System.Windows.Forms.TextBox()
        Me.findButton = New System.Windows.Forms.Button()
        Me.PrintButton = New System.Windows.Forms.Button()
        Me.CategoryButton = New System.Windows.Forms.Button()
        Me.SelectAllButton = New System.Windows.Forms.Button()
        Me.EditPvPStockButton = New System.Windows.Forms.Button()
        Me.UpdateFromExelButton = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ObjectListView1 = New BrightIdeasSoftware.ObjectListView()
        Me.Nom_CategoriaClm = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.Nom_SubCategoriaClm = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.Nom_BodegaClm = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.idProductoClm = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.Nom_ComercialClm = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.Articulosclm = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.StockCm = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.CostoClm = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.CostoTotalClm = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.idProdcutStockClm = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ExportarCategoriasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProductoConCategoriaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportarTodaLaListaDeProductosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ActualizarStockDeProductosDesdeUnListadoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ActualizarCategoriasDesdeUnListadoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.PanelMenu.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.ObjectListView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.ContextMenuStrip2.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelMenu
        '
        Me.PanelMenu.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.PanelMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelMenu.Controls.Add(Me.Label3)
        Me.PanelMenu.Controls.Add(Me.cmbLocalbodega)
        Me.PanelMenu.Controls.Add(Me.EditCountStockButton)
        Me.PanelMenu.Controls.Add(Me.Panel3)
        Me.PanelMenu.Controls.Add(Me.PrintButton)
        Me.PanelMenu.Controls.Add(Me.CategoryButton)
        Me.PanelMenu.Controls.Add(Me.SelectAllButton)
        Me.PanelMenu.Controls.Add(Me.EditPvPStockButton)
        Me.PanelMenu.Controls.Add(Me.UpdateFromExelButton)
        Me.PanelMenu.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelMenu.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelMenu.ForeColor = System.Drawing.Color.Black
        Me.PanelMenu.Location = New System.Drawing.Point(1, 1)
        Me.PanelMenu.Name = "PanelMenu"
        Me.PanelMenu.Padding = New System.Windows.Forms.Padding(2)
        Me.PanelMenu.Size = New System.Drawing.Size(1359, 40)
        Me.PanelMenu.TabIndex = 41
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(673, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 20)
        Me.Label3.TabIndex = 44
        Me.Label3.Text = "Local:"
        '
        'cmbLocalbodega
        '
        Me.cmbLocalbodega.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbLocalbodega.FormattingEnabled = True
        Me.cmbLocalbodega.Location = New System.Drawing.Point(741, 4)
        Me.cmbLocalbodega.Name = "cmbLocalbodega"
        Me.cmbLocalbodega.Size = New System.Drawing.Size(263, 28)
        Me.cmbLocalbodega.TabIndex = 43
        '
        'EditCountStockButton
        '
        Me.EditCountStockButton.Dock = System.Windows.Forms.DockStyle.Right
        Me.EditCountStockButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EditCountStockButton.ForeColor = System.Drawing.Color.Black
        Me.EditCountStockButton.Image = Global.DanashaBasicSignature.My.Resources.Resources.Action_Inline_Edit
        Me.EditCountStockButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.EditCountStockButton.Location = New System.Drawing.Point(1064, 2)
        Me.EditCountStockButton.Margin = New System.Windows.Forms.Padding(0)
        Me.EditCountStockButton.Name = "EditCountStockButton"
        Me.EditCountStockButton.Size = New System.Drawing.Size(115, 34)
        Me.EditCountStockButton.TabIndex = 41
        Me.EditCountStockButton.Text = "&Cambiar cant."
        Me.EditCountStockButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.EditCountStockButton, "Modificar stock")
        Me.EditCountStockButton.UseVisualStyleBackColor = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Panel3.Controls.Add(Me.txtProduc_Select)
        Me.Panel3.Controls.Add(Me.findButton)
        Me.Panel3.ForeColor = System.Drawing.Color.Black
        Me.Panel3.Location = New System.Drawing.Point(270, 5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(2)
        Me.Panel3.Size = New System.Drawing.Size(319, 31)
        Me.Panel3.TabIndex = 39
        '
        'txtProduc_Select
        '
        Me.txtProduc_Select.BackColor = System.Drawing.Color.White
        Me.txtProduc_Select.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtProduc_Select.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProduc_Select.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.txtProduc_Select.Location = New System.Drawing.Point(2, 2)
        Me.txtProduc_Select.Name = "txtProduc_Select"
        Me.txtProduc_Select.Size = New System.Drawing.Size(284, 26)
        Me.txtProduc_Select.TabIndex = 3
        '
        'findButton
        '
        Me.findButton.BackColor = System.Drawing.Color.White
        Me.findButton.Dock = System.Windows.Forms.DockStyle.Right
        Me.findButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.findButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.findButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.findButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.findButton.ForeColor = System.Drawing.Color.FromArgb(CType(CType(204, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.findButton.Image = Global.DanashaBasicSignature.My.Resources.Resources.zoom_Grin_24
        Me.findButton.Location = New System.Drawing.Point(286, 2)
        Me.findButton.Name = "findButton"
        Me.findButton.Size = New System.Drawing.Size(31, 27)
        Me.findButton.TabIndex = 2
        Me.findButton.UseVisualStyleBackColor = False
        '
        'PrintButton
        '
        Me.PrintButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.PrintButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PrintButton.ForeColor = System.Drawing.Color.Black
        Me.PrintButton.Image = Global.DanashaBasicSignature.My.Resources.Resources.Excel_24
        Me.PrintButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.PrintButton.Location = New System.Drawing.Point(171, 2)
        Me.PrintButton.Margin = New System.Windows.Forms.Padding(0)
        Me.PrintButton.Name = "PrintButton"
        Me.PrintButton.Size = New System.Drawing.Size(93, 34)
        Me.PrintButton.TabIndex = 38
        Me.PrintButton.Text = "&Exportar"
        Me.PrintButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.PrintButton.UseVisualStyleBackColor = False
        '
        'CategoryButton
        '
        Me.CategoryButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.CategoryButton.ForeColor = System.Drawing.Color.Black
        Me.CategoryButton.Image = Global.DanashaBasicSignature.My.Resources.Resources.Category_TreeView24
        Me.CategoryButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CategoryButton.Location = New System.Drawing.Point(82, 2)
        Me.CategoryButton.Margin = New System.Windows.Forms.Padding(0)
        Me.CategoryButton.Name = "CategoryButton"
        Me.CategoryButton.Size = New System.Drawing.Size(89, 34)
        Me.CategoryButton.TabIndex = 36
        Me.CategoryButton.Text = "Categoria"
        Me.CategoryButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CategoryButton.UseVisualStyleBackColor = False
        '
        'SelectAllButton
        '
        Me.SelectAllButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.SelectAllButton.ForeColor = System.Drawing.Color.Black
        Me.SelectAllButton.Image = Global.DanashaBasicSignature.My.Resources.Resources.Resum_28
        Me.SelectAllButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SelectAllButton.Location = New System.Drawing.Point(2, 2)
        Me.SelectAllButton.Margin = New System.Windows.Forms.Padding(0)
        Me.SelectAllButton.Name = "SelectAllButton"
        Me.SelectAllButton.Size = New System.Drawing.Size(80, 34)
        Me.SelectAllButton.TabIndex = 37
        Me.SelectAllButton.Text = "Resume"
        Me.SelectAllButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SelectAllButton.UseVisualStyleBackColor = False
        '
        'EditPvPStockButton
        '
        Me.EditPvPStockButton.Dock = System.Windows.Forms.DockStyle.Right
        Me.EditPvPStockButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EditPvPStockButton.ForeColor = System.Drawing.Color.Black
        Me.EditPvPStockButton.Image = Global.DanashaBasicSignature.My.Resources.Resources.confirCost_32
        Me.EditPvPStockButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.EditPvPStockButton.Location = New System.Drawing.Point(1179, 2)
        Me.EditPvPStockButton.Margin = New System.Windows.Forms.Padding(0)
        Me.EditPvPStockButton.Name = "EditPvPStockButton"
        Me.EditPvPStockButton.Size = New System.Drawing.Size(135, 34)
        Me.EditPvPStockButton.TabIndex = 42
        Me.EditPvPStockButton.Text = "&Modificar costo"
        Me.EditPvPStockButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.EditPvPStockButton, "Modificar el costo unitario del producto.")
        Me.EditPvPStockButton.UseVisualStyleBackColor = False
        '
        'UpdateFromExelButton
        '
        Me.UpdateFromExelButton.Dock = System.Windows.Forms.DockStyle.Right
        Me.UpdateFromExelButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UpdateFromExelButton.ForeColor = System.Drawing.Color.Black
        Me.UpdateFromExelButton.Image = Global.DanashaBasicSignature.My.Resources.Resources.Option_20
        Me.UpdateFromExelButton.Location = New System.Drawing.Point(1314, 2)
        Me.UpdateFromExelButton.Margin = New System.Windows.Forms.Padding(0)
        Me.UpdateFromExelButton.Name = "UpdateFromExelButton"
        Me.UpdateFromExelButton.Size = New System.Drawing.Size(41, 34)
        Me.UpdateFromExelButton.TabIndex = 40
        Me.ToolTip1.SetToolTip(Me.UpdateFromExelButton, "Actualizar mediante un listado existentes")
        Me.UpdateFromExelButton.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(1, 414)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1359, 32)
        Me.Panel1.TabIndex = 42
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(4, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Label1"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(48, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(1, 41)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1359, 26)
        Me.Panel2.TabIndex = 43
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(4, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 16)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Label2"
        '
        'ObjectListView1
        '
        Me.ObjectListView1.AllColumns.Add(Me.Nom_CategoriaClm)
        Me.ObjectListView1.AllColumns.Add(Me.Nom_SubCategoriaClm)
        Me.ObjectListView1.AllColumns.Add(Me.Nom_BodegaClm)
        Me.ObjectListView1.AllColumns.Add(Me.idProductoClm)
        Me.ObjectListView1.AllColumns.Add(Me.Nom_ComercialClm)
        Me.ObjectListView1.AllColumns.Add(Me.Articulosclm)
        Me.ObjectListView1.AllColumns.Add(Me.StockCm)
        Me.ObjectListView1.AllColumns.Add(Me.CostoClm)
        Me.ObjectListView1.AllColumns.Add(Me.CostoTotalClm)
        Me.ObjectListView1.AllColumns.Add(Me.idProdcutStockClm)
        Me.ObjectListView1.AlternateRowBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ObjectListView1.CellEditUseWholeCell = False
        Me.ObjectListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Nom_CategoriaClm, Me.Nom_SubCategoriaClm, Me.Nom_BodegaClm, Me.idProductoClm, Me.Nom_ComercialClm, Me.Articulosclm, Me.StockCm, Me.CostoClm, Me.CostoTotalClm, Me.idProdcutStockClm})
        Me.ObjectListView1.Cursor = System.Windows.Forms.Cursors.Default
        Me.ObjectListView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ObjectListView1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ObjectListView1.FullRowSelect = True
        Me.ObjectListView1.RowHeight = 30
        Me.ObjectListView1.GridLines = True
        Me.ObjectListView1.HideSelection = False
        Me.ObjectListView1.Location = New System.Drawing.Point(1, 67)
        Me.ObjectListView1.Name = "ObjectListView1"
        Me.ObjectListView1.Size = New System.Drawing.Size(1359, 347)
        Me.ObjectListView1.TabIndex = 44
        Me.ObjectListView1.UseAlternatingBackColors = True
        Me.ObjectListView1.UseCompatibleStateImageBehavior = False
        Me.ObjectListView1.View = System.Windows.Forms.View.Details
        '
        'Nom_CategoriaClm
        '
        Me.Nom_CategoriaClm.AspectName = ""
        Me.Nom_CategoriaClm.Text = "Categoria"
        Me.Nom_CategoriaClm.Width = 120
        '
        'Nom_SubCategoriaClm
        '
        Me.Nom_SubCategoriaClm.AspectName = ""
        Me.Nom_SubCategoriaClm.Text = "Sub Categoria"
        Me.Nom_SubCategoriaClm.Width = 120
        '
        'Nom_BodegaClm
        '
        Me.Nom_BodegaClm.AspectName = "Nom_Bodega"
        Me.Nom_BodegaClm.Text = "Local o  Bodega"
        Me.Nom_BodegaClm.Width = 101
        '
        'idProductoClm
        '
        Me.idProductoClm.AspectName = ""
        Me.idProductoClm.Text = "Id Producto"
        '
        'Nom_ComercialClm
        '
        Me.Nom_ComercialClm.AspectName = ""
        Me.Nom_ComercialClm.Text = "Producto"
        Me.Nom_ComercialClm.Width = 250
        '
        'Articulosclm
        '
        Me.Articulosclm.AspectName = ""
        Me.Articulosclm.Text = "Articulos"
        Me.Articulosclm.Width = 100
        '
        'StockCm
        '
        Me.StockCm.AspectName = ""
        Me.StockCm.AspectToStringFormat = ""
        Me.StockCm.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.StockCm.Text = "Stock"
        Me.StockCm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.StockCm.Width = 100
        '
        'CostoClm
        '
        Me.CostoClm.AspectName = "{0:C5}"
        Me.CostoClm.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.CostoClm.Text = "Costo Unitario"
        Me.CostoClm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.CostoClm.Width = 100
        '
        'CostoTotalClm
        '
        Me.CostoTotalClm.AspectName = ""
        Me.CostoTotalClm.AspectToStringFormat = ""
        Me.CostoTotalClm.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.CostoTotalClm.Text = "Costo Total"
        Me.CostoTotalClm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.CostoTotalClm.Width = 100
        '
        'idProdcutStockClm
        '
        Me.idProdcutStockClm.AspectName = "idProdcutStock"
        Me.idProdcutStockClm.Text = "Id"
        Me.idProdcutStockClm.Width = 0
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExportarCategoriasToolStripMenuItem, Me.ProductoConCategoriaToolStripMenuItem, Me.ExportarTodaLaListaDeProductosToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(254, 70)
        '
        'ExportarCategoriasToolStripMenuItem
        '
        Me.ExportarCategoriasToolStripMenuItem.Image = Global.DanashaBasicSignature.My.Resources.Resources.Category_TreeView24
        Me.ExportarCategoriasToolStripMenuItem.Name = "ExportarCategoriasToolStripMenuItem"
        Me.ExportarCategoriasToolStripMenuItem.Size = New System.Drawing.Size(253, 22)
        Me.ExportarCategoriasToolStripMenuItem.Text = "Exportar categorias"
        '
        'ProductoConCategoriaToolStripMenuItem
        '
        Me.ProductoConCategoriaToolStripMenuItem.Image = Global.DanashaBasicSignature.My.Resources.Resources.List_32x32
        Me.ProductoConCategoriaToolStripMenuItem.Name = "ProductoConCategoriaToolStripMenuItem"
        Me.ProductoConCategoriaToolStripMenuItem.Size = New System.Drawing.Size(253, 22)
        Me.ProductoConCategoriaToolStripMenuItem.Text = "Producto con categoria"
        '
        'ExportarTodaLaListaDeProductosToolStripMenuItem
        '
        Me.ExportarTodaLaListaDeProductosToolStripMenuItem.Image = Global.DanashaBasicSignature.My.Resources.Resources.producto_241
        Me.ExportarTodaLaListaDeProductosToolStripMenuItem.Name = "ExportarTodaLaListaDeProductosToolStripMenuItem"
        Me.ExportarTodaLaListaDeProductosToolStripMenuItem.Size = New System.Drawing.Size(253, 22)
        Me.ExportarTodaLaListaDeProductosToolStripMenuItem.Text = "Exportar toda la lista de productos"
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ActualizarStockDeProductosDesdeUnListadoToolStripMenuItem, Me.ActualizarCategoriasDesdeUnListadoToolStripMenuItem})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(320, 48)
        '
        'ActualizarStockDeProductosDesdeUnListadoToolStripMenuItem
        '
        Me.ActualizarStockDeProductosDesdeUnListadoToolStripMenuItem.Name = "ActualizarStockDeProductosDesdeUnListadoToolStripMenuItem"
        Me.ActualizarStockDeProductosDesdeUnListadoToolStripMenuItem.Size = New System.Drawing.Size(319, 22)
        Me.ActualizarStockDeProductosDesdeUnListadoToolStripMenuItem.Text = "Actualizar stock de productos desde un listado"
        '
        'ActualizarCategoriasDesdeUnListadoToolStripMenuItem
        '
        Me.ActualizarCategoriasDesdeUnListadoToolStripMenuItem.Name = "ActualizarCategoriasDesdeUnListadoToolStripMenuItem"
        Me.ActualizarCategoriasDesdeUnListadoToolStripMenuItem.Size = New System.Drawing.Size(319, 22)
        Me.ActualizarCategoriasDesdeUnListadoToolStripMenuItem.Text = "Actualizar categorias desde un listado"
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'frmInventario
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1361, 450)
        Me.Controls.Add(Me.ObjectListView1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PanelMenu)
        Me.Name = "frmInventario"
        Me.Padding = New System.Windows.Forms.Padding(1, 1, 1, 4)
        Me.Text = "Inventario"
        Me.PanelMenu.ResumeLayout(False)
        Me.PanelMenu.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.ObjectListView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ContextMenuStrip2.ResumeLayout(False)
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PanelMenu As Panel
    Friend WithEvents PrintButton As Button
    Friend WithEvents CategoryButton As Button
    Friend WithEvents SelectAllButton As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents txtProduc_Select As TextBox
    Friend WithEvents findButton As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents ObjectListView1 As BrightIdeasSoftware.ObjectListView
    Friend WithEvents Nom_CategoriaClm As BrightIdeasSoftware.OLVColumn
    Friend WithEvents Nom_SubCategoriaClm As BrightIdeasSoftware.OLVColumn
    Friend WithEvents Nom_ComercialClm As BrightIdeasSoftware.OLVColumn
    Friend WithEvents Articulosclm As BrightIdeasSoftware.OLVColumn
    Friend WithEvents CostoTotalClm As BrightIdeasSoftware.OLVColumn
    Friend WithEvents CostoClm As BrightIdeasSoftware.OLVColumn
    Friend WithEvents StockCm As BrightIdeasSoftware.OLVColumn
    Friend WithEvents idProductoClm As BrightIdeasSoftware.OLVColumn
    Friend WithEvents EditPvPStockButton As Button
    Friend WithEvents EditCountStockButton As Button
    Friend WithEvents UpdateFromExelButton As Button
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents ExportarCategoriasToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProductoConCategoriaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Nom_BodegaClm As BrightIdeasSoftware.OLVColumn
    Friend WithEvents idProdcutStockClm As BrightIdeasSoftware.OLVColumn
    Friend WithEvents ContextMenuStrip2 As ContextMenuStrip
    Friend WithEvents ActualizarStockDeProductosDesdeUnListadoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ActualizarCategoriasDesdeUnListadoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportarTodaLaListaDeProductosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents cmbLocalbodega As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents ErrorProvider1 As ErrorProvider
End Class
