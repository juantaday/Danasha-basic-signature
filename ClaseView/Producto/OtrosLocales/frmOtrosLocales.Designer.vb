<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmOtrosLocales
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOtrosLocales))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblSubtitle = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlFilters = New System.Windows.Forms.Panel()
        Me.pnlSearch = New System.Windows.Forms.Panel()
        Me.lblBodega = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.lblProducto = New System.Windows.Forms.Label()
        Me.TextBoxRounded1 = New JMControls.Controls.TextBoxRounded()
        Me.pnlOptions = New System.Windows.Forms.Panel()
        Me.lblOpcionesTitle = New System.Windows.Forms.Label()
        Me.RjRadioButton1 = New JMControls.Controls.RJRadioButton()
        Me.RjRadioButton2 = New JMControls.Controls.RJRadioButton()
        Me.pnlGrid = New System.Windows.Forms.Panel()
        Me.pnlGridInner = New System.Windows.Forms.Panel()
        Me.dgvOtrosLocales = New System.Windows.Forms.DataGridView()
        Me.pnlStatusBar = New System.Windows.Forms.Panel()
        Me.lblStatusInfo = New System.Windows.Forms.Label()
        Me.pnlHeader.SuspendLayout()
        Me.pnlFilters.SuspendLayout()
        Me.pnlSearch.SuspendLayout()
        Me.pnlOptions.SuspendLayout()
        Me.pnlGrid.SuspendLayout()
        Me.pnlGridInner.SuspendLayout()
        CType(Me.dgvOtrosLocales, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlStatusBar.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblSubtitle)
        Me.pnlHeader.Controls.Add(Me.lblTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Padding = New System.Windows.Forms.Padding(28, 0, 0, 0)
        Me.pnlHeader.Size = New System.Drawing.Size(1080, 64)
        Me.pnlHeader.TabIndex = 0
        '
        'lblSubtitle
        '
        Me.lblSubtitle.AutoSize = True
        Me.lblSubtitle.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.lblSubtitle.Location = New System.Drawing.Point(30, 40)
        Me.lblSubtitle.Name = "lblSubtitle"
        Me.lblSubtitle.Size = New System.Drawing.Size(264, 15)
        Me.lblSubtitle.TabIndex = 1
        Me.lblSubtitle.Text = "Consulta de stock en todos los locales y bodegas"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Segoe UI Semibold", 15.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.lblTitle.Location = New System.Drawing.Point(28, 12)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(168, 28)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Inventario Global"
        '
        'pnlFilters
        '
        Me.pnlFilters.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.pnlFilters.Controls.Add(Me.pnlSearch)
        Me.pnlFilters.Controls.Add(Me.pnlOptions)
        Me.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFilters.Location = New System.Drawing.Point(0, 64)
        Me.pnlFilters.Name = "pnlFilters"
        Me.pnlFilters.Padding = New System.Windows.Forms.Padding(20, 16, 20, 16)
        Me.pnlFilters.Size = New System.Drawing.Size(1080, 120)
        Me.pnlFilters.TabIndex = 1
        '
        'pnlSearch
        '
        Me.pnlSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.pnlSearch.Controls.Add(Me.lblBodega)
        Me.pnlSearch.Controls.Add(Me.ComboBox1)
        Me.pnlSearch.Controls.Add(Me.lblProducto)
        Me.pnlSearch.Controls.Add(Me.TextBoxRounded1)
        Me.pnlSearch.Location = New System.Drawing.Point(360, 14)
        Me.pnlSearch.Name = "pnlSearch"
        Me.pnlSearch.Size = New System.Drawing.Size(676, 90)
        Me.pnlSearch.TabIndex = 1
        '
        'lblBodega
        '
        Me.lblBodega.AutoSize = True
        Me.lblBodega.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
        Me.lblBodega.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(135, Byte), Integer))
        Me.lblBodega.Location = New System.Drawing.Point(11, 0)
        Me.lblBodega.Name = "lblBodega"
        Me.lblBodega.Size = New System.Drawing.Size(107, 15)
        Me.lblBodega.TabIndex = 0
        Me.lblBodega.Text = "BODEGA O LOCAL"
        '
        'ComboBox1
        '
        Me.ComboBox1.BackColor = System.Drawing.Color.White
        Me.ComboBox1.Enabled = False
        Me.ComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ComboBox1.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.ComboBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(11, 18)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(286, 28)
        Me.ComboBox1.TabIndex = 1
        '
        'lblProducto
        '
        Me.lblProducto.AutoSize = True
        Me.lblProducto.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
        Me.lblProducto.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(135, Byte), Integer))
        Me.lblProducto.Location = New System.Drawing.Point(345, -3)
        Me.lblProducto.Name = "lblProducto"
        Me.lblProducto.Size = New System.Drawing.Size(132, 15)
        Me.lblProducto.TabIndex = 2
        Me.lblProducto.Text = "PRODUCTO BUSCADO"
        '
        'TextBoxRounded1
        '
        Me.TextBoxRounded1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.TextBoxRounded1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.TextBoxRounded1.BackColor = System.Drawing.Color.White
        Me.TextBoxRounded1.BorderColorActive = System.Drawing.Color.FromArgb(CType(CType(67, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.TextBoxRounded1.BorderColorDisable = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(214, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TextBoxRounded1.BorderColorHover = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.TextBoxRounded1.BorderColorIdle = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(214, Byte), Integer), CType(CType(226, Byte), Integer))
        Me.TextBoxRounded1.BorderRadius = 8
        Me.TextBoxRounded1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxRounded1.BorderThickness = 2
        Me.TextBoxRounded1.ButtonImage = CType(resources.GetObject("TextBoxRounded1.ButtonImage"), System.Drawing.Image)
        Me.TextBoxRounded1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.TextBoxRounded1.DecimalPosition = 2
        Me.TextBoxRounded1.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.TextBoxRounded1.IconLeft = CType(resources.GetObject("TextBoxRounded1.IconLeft"), System.Drawing.Image)
        Me.TextBoxRounded1.IconLeftBackColor = System.Drawing.Color.White
        Me.TextBoxRounded1.IconLeftVisible = False
        Me.TextBoxRounded1.Location = New System.Drawing.Point(345, 15)
        Me.TextBoxRounded1.MaxLength = 32767
        Me.TextBoxRounded1.Multiline = False
        Me.TextBoxRounded1.Name = "TextBoxRounded1"
        Me.TextBoxRounded1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.TextBoxRounded1.PlaceHolderColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.TextBoxRounded1.PlaceHolderText = "Buscar producto..."
        Me.TextBoxRounded1.ReadOnly = False
        Me.TextBoxRounded1.SelectedText = ""
        Me.TextBoxRounded1.SelectionLength = 0
        Me.TextBoxRounded1.Size = New System.Drawing.Size(310, 38)
        Me.TextBoxRounded1.TabIndex = 3
        Me.TextBoxRounded1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TextBoxRounded1.ToolTipButton = ""
        Me.TextBoxRounded1.TypeData = JMControls.Enums.TypeDataEnum.VarChar
        Me.TextBoxRounded1.UseSystemPasswordChar = False
        Me.TextBoxRounded1.VisibleButton = True
        '
        'pnlOptions
        '
        Me.pnlOptions.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.pnlOptions.Controls.Add(Me.lblOpcionesTitle)
        Me.pnlOptions.Controls.Add(Me.RjRadioButton1)
        Me.pnlOptions.Controls.Add(Me.RjRadioButton2)
        Me.pnlOptions.Location = New System.Drawing.Point(20, 14)
        Me.pnlOptions.Name = "pnlOptions"
        Me.pnlOptions.Size = New System.Drawing.Size(320, 90)
        Me.pnlOptions.TabIndex = 0
        '
        'lblOpcionesTitle
        '
        Me.lblOpcionesTitle.AutoSize = True
        Me.lblOpcionesTitle.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
        Me.lblOpcionesTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(135, Byte), Integer))
        Me.lblOpcionesTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblOpcionesTitle.Name = "lblOpcionesTitle"
        Me.lblOpcionesTitle.Size = New System.Drawing.Size(151, 15)
        Me.lblOpcionesTitle.TabIndex = 0
        Me.lblOpcionesTitle.Text = "OPCIONES DE BÚSQUEDA"
        '
        'RjRadioButton1
        '
        Me.RjRadioButton1.AutoSize = True
        Me.RjRadioButton1.Checked = True
        Me.RjRadioButton1.CheckedColor = System.Drawing.Color.FromArgb(CType(CType(67, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.RjRadioButton1.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.RjRadioButton1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.RjRadioButton1.Location = New System.Drawing.Point(2, 22)
        Me.RjRadioButton1.MinimumSize = New System.Drawing.Size(0, 24)
        Me.RjRadioButton1.Name = "RjRadioButton1"
        Me.RjRadioButton1.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.RjRadioButton1.Size = New System.Drawing.Size(140, 24)
        Me.RjRadioButton1.TabIndex = 1
        Me.RjRadioButton1.TabStop = True
        Me.RjRadioButton1.Text = "En cualquier local"
        Me.RjRadioButton1.UnCheckedColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(200, Byte), Integer))
        '
        'RjRadioButton2
        '
        Me.RjRadioButton2.AutoSize = True
        Me.RjRadioButton2.CheckedColor = System.Drawing.Color.FromArgb(CType(CType(67, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.RjRadioButton2.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.RjRadioButton2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.RjRadioButton2.Location = New System.Drawing.Point(2, 56)
        Me.RjRadioButton2.MinimumSize = New System.Drawing.Size(0, 24)
        Me.RjRadioButton2.Name = "RjRadioButton2"
        Me.RjRadioButton2.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        Me.RjRadioButton2.Size = New System.Drawing.Size(205, 24)
        Me.RjRadioButton2.TabIndex = 2
        Me.RjRadioButton2.Text = "En local o bodega específico"
        Me.RjRadioButton2.UnCheckedColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(185, Byte), Integer), CType(CType(200, Byte), Integer))
        '
        'pnlGrid
        '
        Me.pnlGrid.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(238, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlGrid.Controls.Add(Me.pnlGridInner)
        Me.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGrid.Location = New System.Drawing.Point(0, 184)
        Me.pnlGrid.Name = "pnlGrid"
        Me.pnlGrid.Padding = New System.Windows.Forms.Padding(20, 16, 20, 8)
        Me.pnlGrid.Size = New System.Drawing.Size(1080, 446)
        Me.pnlGrid.TabIndex = 2
        '
        'pnlGridInner
        '
        Me.pnlGridInner.BackColor = System.Drawing.Color.White
        Me.pnlGridInner.Controls.Add(Me.dgvOtrosLocales)
        Me.pnlGridInner.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGridInner.Location = New System.Drawing.Point(20, 16)
        Me.pnlGridInner.Name = "pnlGridInner"
        Me.pnlGridInner.Size = New System.Drawing.Size(1040, 422)
        Me.pnlGridInner.TabIndex = 0
        '
        'dgvOtrosLocales
        '
        Me.dgvOtrosLocales.AllowUserToAddRows = False
        Me.dgvOtrosLocales.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(249, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvOtrosLocales.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvOtrosLocales.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvOtrosLocales.BackgroundColor = System.Drawing.Color.White
        Me.dgvOtrosLocales.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvOtrosLocales.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(67, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(238, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(67, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(238, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvOtrosLocales.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvOtrosLocales.ColumnHeadersHeight = 40
        Me.dgvOtrosLocales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(80, Byte), Integer))
        DataGridViewCellStyle3.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(227, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(100, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvOtrosLocales.DefaultCellStyle = DataGridViewCellStyle3
        Me.dgvOtrosLocales.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvOtrosLocales.GridColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(233, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.dgvOtrosLocales.Location = New System.Drawing.Point(0, 0)
        Me.dgvOtrosLocales.MultiSelect = False
        Me.dgvOtrosLocales.Name = "dgvOtrosLocales"
        Me.dgvOtrosLocales.ReadOnly = True
        Me.dgvOtrosLocales.RowHeadersVisible = False
        Me.dgvOtrosLocales.RowTemplate.Height = 38
        Me.dgvOtrosLocales.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvOtrosLocales.Size = New System.Drawing.Size(1040, 422)
        Me.dgvOtrosLocales.TabIndex = 0
        '
        'pnlStatusBar
        '
        Me.pnlStatusBar.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.pnlStatusBar.Controls.Add(Me.lblStatusInfo)
        Me.pnlStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlStatusBar.Location = New System.Drawing.Point(0, 630)
        Me.pnlStatusBar.Name = "pnlStatusBar"
        Me.pnlStatusBar.Padding = New System.Windows.Forms.Padding(28, 0, 0, 0)
        Me.pnlStatusBar.Size = New System.Drawing.Size(1080, 30)
        Me.pnlStatusBar.TabIndex = 3
        '
        'lblStatusInfo
        '
        Me.lblStatusInfo.AutoSize = True
        Me.lblStatusInfo.Dock = System.Windows.Forms.DockStyle.Left
        Me.lblStatusInfo.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblStatusInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(140, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(168, Byte), Integer))
        Me.lblStatusInfo.Location = New System.Drawing.Point(28, 0)
        Me.lblStatusInfo.Name = "lblStatusInfo"
        Me.lblStatusInfo.Size = New System.Drawing.Size(0, 15)
        Me.lblStatusInfo.TabIndex = 0
        Me.lblStatusInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmOtrosLocales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1080, 660)
        Me.Controls.Add(Me.pnlGrid)
        Me.Controls.Add(Me.pnlFilters)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.pnlStatusBar)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.MinimumSize = New System.Drawing.Size(900, 520)
        Me.Name = "frmOtrosLocales"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Inventario Global"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlFilters.ResumeLayout(False)
        Me.pnlSearch.ResumeLayout(False)
        Me.pnlSearch.PerformLayout()
        Me.pnlOptions.ResumeLayout(False)
        Me.pnlOptions.PerformLayout()
        Me.pnlGrid.ResumeLayout(False)
        Me.pnlGridInner.ResumeLayout(False)
        CType(Me.dgvOtrosLocales, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlStatusBar.ResumeLayout(False)
        Me.pnlStatusBar.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    ' ── Declaraciones de controles ────────────────────────────────────────────
    Friend WithEvents pnlHeader        As Panel
    Friend WithEvents lblTitle         As Label
    Friend WithEvents lblSubtitle      As Label
    Friend WithEvents pnlFilters       As Panel
    Friend WithEvents pnlOptions       As Panel
    Friend WithEvents lblOpcionesTitle As Label
    Friend WithEvents RjRadioButton1   As JMControls.Controls.RJRadioButton
    Friend WithEvents RjRadioButton2   As JMControls.Controls.RJRadioButton
    Friend WithEvents pnlSearch        As Panel
    Friend WithEvents lblBodega        As Label
    Friend WithEvents ComboBox1        As ComboBox
    Friend WithEvents lblProducto      As Label
    Friend WithEvents TextBoxRounded1  As JMControls.Controls.TextBoxRounded
    Friend WithEvents pnlGrid          As Panel
    Friend WithEvents pnlGridInner     As Panel
    Friend WithEvents dgvOtrosLocales  As DataGridView
    Friend WithEvents pnlStatusBar     As Panel
    Friend WithEvents lblStatusInfo    As Label

End Class
