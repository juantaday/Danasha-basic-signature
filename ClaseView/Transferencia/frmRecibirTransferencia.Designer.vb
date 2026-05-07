<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmRecibirTransferencia
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso (components IsNot Nothing) Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.lblSubtitulo = New System.Windows.Forms.Label()
        Me.pnlInfo = New System.Windows.Forms.Panel()
        Me.lblBodega = New System.Windows.Forms.Label()
        Me.lblEstado = New System.Windows.Forms.Label()
        Me.btnActualizar = New System.Windows.Forms.Button()
        Me.pnlLista = New System.Windows.Forms.Panel()
        Me.ListBoxTransf = New System.Windows.Forms.ListBox()
        Me.lblListaTitle = New System.Windows.Forms.Label()
        Me.splitter = New System.Windows.Forms.Panel()
        Me.pnlDetalle = New System.Windows.Forms.Panel()
        Me.DgvDetalle = New System.Windows.Forms.DataGridView()
        Me.ColCheck = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ColProducto = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColEnviado = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColRecibido = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColUnidad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColEsNuevo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.lblDetalleTitle = New System.Windows.Forms.Label()
        Me.pnlNovedad = New System.Windows.Forms.Panel()
        Me.lblNovedad = New System.Windows.Forms.Label()
        Me.txtNovedad = New System.Windows.Forms.TextBox()
        Me.pnlFooter = New System.Windows.Forms.Panel()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.lblNuevosAviso = New System.Windows.Forms.Label()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.pnlLoading = New System.Windows.Forms.Panel()
        Me.lblLoadingMsg = New System.Windows.Forms.Label()
        Me.progressBar = New System.Windows.Forms.ProgressBar()
        Me.pnlHeader.SuspendLayout()
        Me.pnlInfo.SuspendLayout()
        Me.pnlLista.SuspendLayout()
        Me.pnlDetalle.SuspendLayout()
        CType(Me.DgvDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlNovedad.SuspendLayout()
        Me.pnlFooter.SuspendLayout()
        Me.pnlLoading.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblTitulo)
        Me.pnlHeader.Controls.Add(Me.lblSubtitulo)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(944, 72)
        Me.pnlHeader.TabIndex = 4
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.lblTitulo.Location = New System.Drawing.Point(20, 14)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(306, 25)
        Me.lblTitulo.TabIndex = 0
        Me.lblTitulo.Text = "RECEPCIÓN DE TRANSFERENCIAS"
        '
        'lblSubtitulo
        '
        Me.lblSubtitulo.AutoSize = True
        Me.lblSubtitulo.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.lblSubtitulo.Location = New System.Drawing.Point(22, 42)
        Me.lblSubtitulo.Name = "lblSubtitulo"
        Me.lblSubtitulo.Size = New System.Drawing.Size(221, 15)
        Me.lblSubtitulo.TabIndex = 1
        Me.lblSubtitulo.Text = "Productos en tránsito hacia esta sucursal"
        '
        'pnlInfo
        '
        Me.pnlInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.pnlInfo.Controls.Add(Me.lblBodega)
        Me.pnlInfo.Controls.Add(Me.lblEstado)
        Me.pnlInfo.Controls.Add(Me.btnActualizar)
        Me.pnlInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInfo.Location = New System.Drawing.Point(0, 72)
        Me.pnlInfo.Name = "pnlInfo"
        Me.pnlInfo.Padding = New System.Windows.Forms.Padding(20, 0, 20, 0)
        Me.pnlInfo.Size = New System.Drawing.Size(944, 46)
        Me.pnlInfo.TabIndex = 3
        '
        'lblBodega
        '
        Me.lblBodega.AutoSize = True
        Me.lblBodega.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblBodega.ForeColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(195, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.lblBodega.Location = New System.Drawing.Point(20, 14)
        Me.lblBodega.Name = "lblBodega"
        Me.lblBodega.Size = New System.Drawing.Size(71, 15)
        Me.lblBodega.TabIndex = 0
        Me.lblBodega.Text = "Terminal: —"
        '
        'lblEstado
        '
        Me.lblEstado.AutoSize = True
        Me.lblEstado.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblEstado.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.lblEstado.Location = New System.Drawing.Point(340, 14)
        Me.lblEstado.Name = "lblEstado"
        Me.lblEstado.Size = New System.Drawing.Size(33, 15)
        Me.lblEstado.TabIndex = 1
        Me.lblEstado.Text = "Listo"
        '
        'btnActualizar
        '
        Me.btnActualizar.BackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnActualizar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnActualizar.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.btnActualizar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.btnActualizar.Location = New System.Drawing.Point(808, 8)
        Me.btnActualizar.Name = "btnActualizar"
        Me.btnActualizar.Size = New System.Drawing.Size(120, 30)
        Me.btnActualizar.TabIndex = 2
        Me.btnActualizar.Text = "↻  Actualizar"
        Me.btnActualizar.UseVisualStyleBackColor = False
        '
        'pnlLista
        '
        Me.pnlLista.BackColor = System.Drawing.Color.FromArgb(CType(CType(18, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.pnlLista.Controls.Add(Me.ListBoxTransf)
        Me.pnlLista.Controls.Add(Me.lblListaTitle)
        Me.pnlLista.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlLista.Location = New System.Drawing.Point(0, 118)
        Me.pnlLista.Name = "pnlLista"
        Me.pnlLista.Padding = New System.Windows.Forms.Padding(20, 10, 20, 0)
        Me.pnlLista.Size = New System.Drawing.Size(944, 130)
        Me.pnlLista.TabIndex = 2
        '
        'ListBoxTransf
        '
        Me.ListBoxTransf.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.ListBoxTransf.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBoxTransf.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBoxTransf.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.ListBoxTransf.ForeColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.ListBoxTransf.ItemHeight = 17
        Me.ListBoxTransf.Location = New System.Drawing.Point(20, 26)
        Me.ListBoxTransf.Name = "ListBoxTransf"
        Me.ListBoxTransf.Size = New System.Drawing.Size(904, 104)
        Me.ListBoxTransf.TabIndex = 0
        '
        'lblListaTitle
        '
        Me.lblListaTitle.AutoSize = True
        Me.lblListaTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblListaTitle.Font = New System.Drawing.Font("Segoe UI", 7.5!, System.Drawing.FontStyle.Bold)
        Me.lblListaTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.lblListaTitle.Location = New System.Drawing.Point(20, 10)
        Me.lblListaTitle.Name = "lblListaTitle"
        Me.lblListaTitle.Padding = New System.Windows.Forms.Padding(0, 0, 0, 4)
        Me.lblListaTitle.Size = New System.Drawing.Size(153, 16)
        Me.lblListaTitle.TabIndex = 1
        Me.lblListaTitle.Text = "TRANSFERENCIAS PENDIENTES"
        '
        'splitter
        '
        Me.splitter.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.splitter.Dock = System.Windows.Forms.DockStyle.Top
        Me.splitter.Location = New System.Drawing.Point(0, 248)
        Me.splitter.Name = "splitter"
        Me.splitter.Size = New System.Drawing.Size(944, 2)
        Me.splitter.TabIndex = 1
        '
        'pnlDetalle
        '
        Me.pnlDetalle.BackColor = System.Drawing.Color.FromArgb(CType(CType(18, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.pnlDetalle.Controls.Add(Me.DgvDetalle)
        Me.pnlDetalle.Controls.Add(Me.lblDetalleTitle)
        Me.pnlDetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDetalle.Location = New System.Drawing.Point(0, 250)
        Me.pnlDetalle.Name = "pnlDetalle"
        Me.pnlDetalle.Padding = New System.Windows.Forms.Padding(20, 10, 20, 0)
        Me.pnlDetalle.Size = New System.Drawing.Size(944, 279)
        Me.pnlDetalle.TabIndex = 0
        '
        'DgvDetalle
        '
        Me.DgvDetalle.AllowUserToAddRows = False
        Me.DgvDetalle.AllowUserToDeleteRows = False
        Me.DgvDetalle.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(26, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(48, Byte), Integer))
        Me.DgvDetalle.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvDetalle.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.DgvDetalle.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DgvDetalle.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DgvDetalle.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(38, Byte), Integer), CType(CType(55, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(40, Byte), Integer))
        DataGridViewCellStyle2.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvDetalle.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DgvDetalle.ColumnHeadersHeight = 36
        Me.DgvDetalle.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColCheck, Me.ColProducto, Me.ColEnviado, Me.ColRecibido, Me.ColUnidad, Me.ColEsNuevo})
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(40, Byte), Integer))
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(240, Byte), Integer))
        DataGridViewCellStyle6.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvDetalle.DefaultCellStyle = DataGridViewCellStyle6
        Me.DgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvDetalle.EnableHeadersVisualStyles = False
        Me.DgvDetalle.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.DgvDetalle.GridColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.DgvDetalle.Location = New System.Drawing.Point(20, 28)
        Me.DgvDetalle.MultiSelect = False
        Me.DgvDetalle.Name = "DgvDetalle"
        Me.DgvDetalle.RowHeadersVisible = False
        Me.DgvDetalle.RowTemplate.Height = 34
        Me.DgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvDetalle.Size = New System.Drawing.Size(904, 251)
        Me.DgvDetalle.TabIndex = 0
        '
        'ColCheck
        '
        Me.ColCheck.HeaderText = "✔"
        Me.ColCheck.Name = "ColCheck"
        Me.ColCheck.Width = 38
        '
        'ColProducto
        '
        Me.ColProducto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ColProducto.HeaderText = "PRODUCTO"
        Me.ColProducto.Name = "ColProducto"
        Me.ColProducto.ReadOnly = True
        '
        'ColEnviado
        '
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ColEnviado.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColEnviado.HeaderText = "ENVIADO"
        Me.ColEnviado.Name = "ColEnviado"
        Me.ColEnviado.ReadOnly = True
        Me.ColEnviado.Width = 90
        '
        'ColRecibido
        '
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(28, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(52, Byte), Integer))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.ColRecibido.DefaultCellStyle = DataGridViewCellStyle4
        Me.ColRecibido.HeaderText = "RECIBIDO"
        Me.ColRecibido.Name = "ColRecibido"
        Me.ColRecibido.Width = 90
        '
        'ColUnidad
        '
        Me.ColUnidad.HeaderText = "UNIDAD"
        Me.ColUnidad.Name = "ColUnidad"
        Me.ColUnidad.ReadOnly = True
        Me.ColUnidad.Width = 80
        '
        'ColEsNuevo
        '
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(140, Byte), Integer))
        Me.ColEsNuevo.DefaultCellStyle = DataGridViewCellStyle5
        Me.ColEsNuevo.HeaderText = "ESTADO"
        Me.ColEsNuevo.Name = "ColEsNuevo"
        Me.ColEsNuevo.ReadOnly = True
        Me.ColEsNuevo.Width = 90
        '
        'lblDetalleTitle
        '
        Me.lblDetalleTitle.AutoSize = True
        Me.lblDetalleTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblDetalleTitle.Font = New System.Drawing.Font("Segoe UI", 7.5!, System.Drawing.FontStyle.Bold)
        Me.lblDetalleTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.lblDetalleTitle.Location = New System.Drawing.Point(20, 10)
        Me.lblDetalleTitle.Name = "lblDetalleTitle"
        Me.lblDetalleTitle.Padding = New System.Windows.Forms.Padding(0, 0, 0, 6)
        Me.lblDetalleTitle.Size = New System.Drawing.Size(413, 18)
        Me.lblDetalleTitle.TabIndex = 1
        Me.lblDetalleTitle.Text = "DETALLE DE LA TRANSFERENCIA  —  productos nuevos se registran automáticamente ✦"
        '
        'pnlNovedad
        '
        Me.pnlNovedad.BackColor = System.Drawing.Color.FromArgb(CType(CType(22, Byte), Integer), CType(CType(28, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.pnlNovedad.Controls.Add(Me.lblNovedad)
        Me.pnlNovedad.Controls.Add(Me.txtNovedad)
        Me.pnlNovedad.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlNovedad.Location = New System.Drawing.Point(0, 529)
        Me.pnlNovedad.Name = "pnlNovedad"
        Me.pnlNovedad.Padding = New System.Windows.Forms.Padding(20, 6, 20, 6)
        Me.pnlNovedad.Size = New System.Drawing.Size(944, 68)
        Me.pnlNovedad.TabIndex = 5
        '
        'lblNovedad
        '
        Me.lblNovedad.AutoSize = True
        Me.lblNovedad.Font = New System.Drawing.Font("Segoe UI", 7.5!, System.Drawing.FontStyle.Bold)
        Me.lblNovedad.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.lblNovedad.Location = New System.Drawing.Point(20, 8)
        Me.lblNovedad.Name = "lblNovedad"
        Me.lblNovedad.Size = New System.Drawing.Size(137, 12)
        Me.lblNovedad.TabIndex = 0
        Me.lblNovedad.Text = "NOVEDAD / OBSERVACIÓN:"
        '
        'txtNovedad
        '
        Me.txtNovedad.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.txtNovedad.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtNovedad.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.txtNovedad.ForeColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.txtNovedad.Location = New System.Drawing.Point(20, 26)
        Me.txtNovedad.Multiline = True
        Me.txtNovedad.Name = "txtNovedad"
        Me.txtNovedad.Size = New System.Drawing.Size(906, 34)
        Me.txtNovedad.TabIndex = 1
        '
        'pnlFooter
        '
        Me.pnlFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(24, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.pnlFooter.Controls.Add(Me.btnCancelar)
        Me.pnlFooter.Controls.Add(Me.lblNuevosAviso)
        Me.pnlFooter.Controls.Add(Me.btnAceptar)
        Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFooter.Location = New System.Drawing.Point(0, 597)
        Me.pnlFooter.Name = "pnlFooter"
        Me.pnlFooter.Padding = New System.Windows.Forms.Padding(20, 12, 20, 12)
        Me.pnlFooter.Size = New System.Drawing.Size(944, 64)
        Me.pnlFooter.TabIndex = 6
        '
        'btnCancelar
        '
        Me.btnCancelar.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(44, Byte), Integer), CType(CType(62, Byte), Integer))
        Me.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelar.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(175, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.btnCancelar.Location = New System.Drawing.Point(20, 13)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(110, 38)
        Me.btnCancelar.TabIndex = 0
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = False
        '
        'lblNuevosAviso
        '
        Me.lblNuevosAviso.AutoSize = True
        Me.lblNuevosAviso.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblNuevosAviso.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(140, Byte), Integer))
        Me.lblNuevosAviso.Location = New System.Drawing.Point(145, 22)
        Me.lblNuevosAviso.Name = "lblNuevosAviso"
        Me.lblNuevosAviso.Size = New System.Drawing.Size(0, 13)
        Me.lblNuevosAviso.TabIndex = 1
        '
        'btnAceptar
        '
        Me.btnAceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAceptar.FlatAppearance.BorderSize = 0
        Me.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAceptar.Font = New System.Drawing.Font("Segoe UI Semibold", 9.5!, System.Drawing.FontStyle.Bold)
        Me.btnAceptar.ForeColor = System.Drawing.Color.White
        Me.btnAceptar.Location = New System.Drawing.Point(1438, 13)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(240, 38)
        Me.btnAceptar.TabIndex = 2
        Me.btnAceptar.Text = "✔  Aceptar productos llegados"
        Me.btnAceptar.UseVisualStyleBackColor = False
        '
        'pnlLoading
        '
        Me.pnlLoading.BackColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(10, Byte), Integer), CType(CType(14, Byte), Integer), CType(CType(20, Byte), Integer))
        Me.pnlLoading.Controls.Add(Me.lblLoadingMsg)
        Me.pnlLoading.Controls.Add(Me.progressBar)
        Me.pnlLoading.Location = New System.Drawing.Point(0, 0)
        Me.pnlLoading.Name = "pnlLoading"
        Me.pnlLoading.Size = New System.Drawing.Size(960, 700)
        Me.pnlLoading.TabIndex = 7
        Me.pnlLoading.Visible = False
        '
        'lblLoadingMsg
        '
        Me.lblLoadingMsg.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold)
        Me.lblLoadingMsg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.lblLoadingMsg.Location = New System.Drawing.Point(250, 315)
        Me.lblLoadingMsg.Name = "lblLoadingMsg"
        Me.lblLoadingMsg.Size = New System.Drawing.Size(460, 34)
        Me.lblLoadingMsg.TabIndex = 0
        Me.lblLoadingMsg.Text = "Consultando Supabase..."
        Me.lblLoadingMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'progressBar
        '
        Me.progressBar.Location = New System.Drawing.Point(250, 355)
        Me.progressBar.MarqueeAnimationSpeed = 25
        Me.progressBar.Name = "progressBar"
        Me.progressBar.Size = New System.Drawing.Size(460, 6)
        Me.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.progressBar.TabIndex = 1
        '
        'frmRecibirTransferencia
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(18, Byte), Integer), CType(CType(22, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(944, 661)
        Me.Controls.Add(Me.pnlDetalle)
        Me.Controls.Add(Me.splitter)
        Me.Controls.Add(Me.pnlLista)
        Me.Controls.Add(Me.pnlInfo)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.pnlNovedad)
        Me.Controls.Add(Me.pnlFooter)
        Me.Controls.Add(Me.pnlLoading)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(960, 700)
        Me.Name = "frmRecibirTransferencia"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Recepción de Transferencias"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlInfo.ResumeLayout(False)
        Me.pnlInfo.PerformLayout()
        Me.pnlLista.ResumeLayout(False)
        Me.pnlLista.PerformLayout()
        Me.pnlDetalle.ResumeLayout(False)
        Me.pnlDetalle.PerformLayout()
        CType(Me.DgvDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlNovedad.ResumeLayout(False)
        Me.pnlNovedad.PerformLayout()
        Me.pnlFooter.ResumeLayout(False)
        Me.pnlFooter.PerformLayout()
        Me.pnlLoading.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents lblSubtitulo As System.Windows.Forms.Label
    Friend WithEvents pnlInfo As System.Windows.Forms.Panel
    Friend WithEvents lblBodega As System.Windows.Forms.Label
    Friend WithEvents lblEstado As System.Windows.Forms.Label
    Friend WithEvents btnActualizar As System.Windows.Forms.Button
    Friend WithEvents pnlLista As System.Windows.Forms.Panel
    Friend WithEvents lblListaTitle As System.Windows.Forms.Label
    Friend WithEvents ListBoxTransf As System.Windows.Forms.ListBox
    Friend WithEvents splitter As System.Windows.Forms.Panel
    Friend WithEvents pnlDetalle As System.Windows.Forms.Panel
    Friend WithEvents lblDetalleTitle As System.Windows.Forms.Label
    Friend WithEvents DgvDetalle As System.Windows.Forms.DataGridView
    Friend WithEvents ColCheck As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ColProducto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColEnviado As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColRecibido As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColUnidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColEsNuevo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pnlNovedad As System.Windows.Forms.Panel
    Friend WithEvents lblNovedad As System.Windows.Forms.Label
    Friend WithEvents txtNovedad As System.Windows.Forms.TextBox
    Friend WithEvents pnlFooter As System.Windows.Forms.Panel
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents lblNuevosAviso As System.Windows.Forms.Label
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents pnlLoading As System.Windows.Forms.Panel
    Friend WithEvents lblLoadingMsg As System.Windows.Forms.Label
    Friend WithEvents progressBar As System.Windows.Forms.ProgressBar

End Class