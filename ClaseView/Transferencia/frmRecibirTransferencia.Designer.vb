Imports BrightIdeasSoftware

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
        Me.components = New System.ComponentModel.Container()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.lblSubtitulo = New System.Windows.Forms.Label()
        Me.pnlInfo = New System.Windows.Forms.Panel()
        Me.lblBodega = New System.Windows.Forms.Label()
        Me.lblEstado = New System.Windows.Forms.Label()
        Me.btnActualizar = New System.Windows.Forms.Button()
        Me.pnlLista = New System.Windows.Forms.Panel()
        Me.OlvTransferencias = New BrightIdeasSoftware.ObjectListView()
        Me.colAccion = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.colNumero = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.colOrigen = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.colFecha = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.lblListaTitle = New System.Windows.Forms.Label()
        Me.mnuTransferencias = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuImprimirDetalle = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuRechazar = New System.Windows.Forms.ToolStripMenuItem()
        Me.splitter = New System.Windows.Forms.Panel()
        Me.pnlDetalle = New System.Windows.Forms.Panel()
        Me.OlvDetalle = New BrightIdeasSoftware.ObjectListView()
        Me.colCodProducto = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.colProducto = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.colEnviado = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.colRecibido = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.colUnidad = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.colEstado = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
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
        CType(Me.OlvTransferencias, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuTransferencias.SuspendLayout()
        Me.pnlDetalle.SuspendLayout()
        CType(Me.OlvDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlNovedad.SuspendLayout()
        Me.pnlFooter.SuspendLayout()
        Me.pnlLoading.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.White
        Me.pnlHeader.Controls.Add(Me.lblTitulo)
        Me.pnlHeader.Controls.Add(Me.lblSubtitulo)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(944, 60)
        Me.pnlHeader.TabIndex = 4
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(115, Byte), Integer))
        Me.lblTitulo.Location = New System.Drawing.Point(20, 8)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(306, 25)
        Me.lblTitulo.TabIndex = 0
        Me.lblTitulo.Text = "RECEPCIÓN DE TRANSFERENCIAS"
        '
        'lblSubtitulo
        '
        Me.lblSubtitulo.AutoSize = True
        Me.lblSubtitulo.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.lblSubtitulo.Location = New System.Drawing.Point(22, 36)
        Me.lblSubtitulo.Name = "lblSubtitulo"
        Me.lblSubtitulo.Size = New System.Drawing.Size(290, 19)
        Me.lblSubtitulo.TabIndex = 1
        Me.lblSubtitulo.Text = "Productos en tránsito hacia esta sucursal/local"
        '
        'pnlInfo
        '
        Me.pnlInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlInfo.Controls.Add(Me.lblBodega)
        Me.pnlInfo.Controls.Add(Me.lblEstado)
        Me.pnlInfo.Controls.Add(Me.btnActualizar)
        Me.pnlInfo.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlInfo.Location = New System.Drawing.Point(0, 60)
        Me.pnlInfo.Name = "pnlInfo"
        Me.pnlInfo.Padding = New System.Windows.Forms.Padding(20, 0, 20, 0)
        Me.pnlInfo.Size = New System.Drawing.Size(944, 46)
        Me.pnlInfo.TabIndex = 3
        '
        'lblBodega
        '
        Me.lblBodega.AutoSize = True
        Me.lblBodega.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblBodega.ForeColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(85, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.lblBodega.Location = New System.Drawing.Point(20, 14)
        Me.lblBodega.Name = "lblBodega"
        Me.lblBodega.Size = New System.Drawing.Size(81, 19)
        Me.lblBodega.TabIndex = 0
        Me.lblBodega.Text = "Terminal: —"
        '
        'lblEstado
        '
        Me.lblEstado.AutoSize = True
        Me.lblEstado.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblEstado.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(115, Byte), Integer))
        Me.lblEstado.Location = New System.Drawing.Point(701, 16)
        Me.lblEstado.Name = "lblEstado"
        Me.lblEstado.Size = New System.Drawing.Size(33, 15)
        Me.lblEstado.TabIndex = 1
        Me.lblEstado.Text = "Listo"
        '
        'btnActualizar
        '
        Me.btnActualizar.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnActualizar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnActualizar.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.btnActualizar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.btnActualizar.Location = New System.Drawing.Point(575, 8)
        Me.btnActualizar.Name = "btnActualizar"
        Me.btnActualizar.Size = New System.Drawing.Size(120, 30)
        Me.btnActualizar.TabIndex = 2
        Me.btnActualizar.Text = "↻  Actualizar"
        Me.btnActualizar.UseVisualStyleBackColor = False
        '
        'pnlLista
        '
        Me.pnlLista.BackColor = System.Drawing.Color.White
        Me.pnlLista.Controls.Add(Me.OlvTransferencias)
        Me.pnlLista.Controls.Add(Me.lblListaTitle)
        Me.pnlLista.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlLista.Location = New System.Drawing.Point(0, 106)
        Me.pnlLista.Name = "pnlLista"
        Me.pnlLista.Padding = New System.Windows.Forms.Padding(20, 10, 20, 0)
        Me.pnlLista.Size = New System.Drawing.Size(944, 157)
        Me.pnlLista.TabIndex = 2
        '
        'OlvTransferencias
        '
        Me.OlvTransferencias.AlternateRowBackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(253, Byte), Integer))
        Me.OlvTransferencias.BackColor = System.Drawing.Color.FromArgb(CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(253, Byte), Integer))
        Me.OlvTransferencias.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.OlvTransferencias.CellEditUseWholeCell = False
        Me.OlvTransferencias.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colAccion, Me.colNumero, Me.colOrigen, Me.colFecha})
        Me.OlvTransferencias.Cursor = System.Windows.Forms.Cursors.Default
        Me.OlvTransferencias.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OlvTransferencias.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.OlvTransferencias.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.OlvTransferencias.FullRowSelect = True
        Me.OlvTransferencias.GridLines = True
        Me.OlvTransferencias.HeaderMinimumHeight = 30
        Me.OlvTransferencias.HideSelection = False
        Me.OlvTransferencias.Location = New System.Drawing.Point(20, 33)
        Me.OlvTransferencias.Name = "OlvTransferencias"
        Me.OlvTransferencias.RowHeight = 32
        Me.OlvTransferencias.ShowGroups = False
        Me.OlvTransferencias.Size = New System.Drawing.Size(904, 124)
        Me.OlvTransferencias.TabIndex = 0
        Me.OlvTransferencias.UseCompatibleStateImageBehavior = False
        Me.OlvTransferencias.View = System.Windows.Forms.View.Details
        '
        'colAccion
        '
        Me.colAccion.AspectName = "Accion"
        Me.colAccion.Text = ""
        Me.colAccion.Width = 40
        '
        'colNumero
        '
        Me.colNumero.AspectName = "Numero"
        Me.colNumero.Text = "NÚMERO"
        Me.colNumero.Width = 150
        '
        'colOrigen
        '
        Me.colOrigen.AspectName = "Origen"
        Me.colOrigen.Text = "ORIGEN"
        Me.colOrigen.Width = 260
        '
        'colFecha
        '
        Me.colFecha.AspectName = "Fecha"
        Me.colFecha.Text = "FECHA"
        Me.colFecha.Width = 160
        '
        'lblListaTitle
        '
        Me.lblListaTitle.AutoSize = True
        Me.lblListaTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblListaTitle.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblListaTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(105, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.lblListaTitle.Location = New System.Drawing.Point(20, 10)
        Me.lblListaTitle.Name = "lblListaTitle"
        Me.lblListaTitle.Padding = New System.Windows.Forms.Padding(0, 0, 0, 4)
        Me.lblListaTitle.Size = New System.Drawing.Size(213, 23)
        Me.lblListaTitle.TabIndex = 1
        Me.lblListaTitle.Text = "TRANSFERENCIAS PENDIENTES"
        '
        'mnuTransferencias
        '
        Me.mnuTransferencias.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuImprimirDetalle, Me.mnuRechazar})
        Me.mnuTransferencias.Name = "mnuTransferencias"
        Me.mnuTransferencias.Size = New System.Drawing.Size(224, 80)
        '
        'mnuImprimirDetalle
        '
        Me.mnuImprimirDetalle.Image = Global.DanashaBasicSignature.My.Resources.Resources.Action_Printing_Print_32x32
        Me.mnuImprimirDetalle.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnuImprimirDetalle.Name = "mnuImprimirDetalle"
        Me.mnuImprimirDetalle.Size = New System.Drawing.Size(223, 38)
        Me.mnuImprimirDetalle.Text = "Imprimir detalle"
        '
        'mnuRechazar
        '
        Me.mnuRechazar.Image = Global.DanashaBasicSignature.My.Resources.Resources.Action_Cancel_32x32
        Me.mnuRechazar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.mnuRechazar.Name = "mnuRechazar"
        Me.mnuRechazar.Size = New System.Drawing.Size(223, 38)
        Me.mnuRechazar.Text = "Rechazar todo lo enviado"
        '
        'splitter
        '
        Me.splitter.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.splitter.Dock = System.Windows.Forms.DockStyle.Top
        Me.splitter.Location = New System.Drawing.Point(0, 263)
        Me.splitter.Name = "splitter"
        Me.splitter.Size = New System.Drawing.Size(944, 2)
        Me.splitter.TabIndex = 1
        '
        'pnlDetalle
        '
        Me.pnlDetalle.BackColor = System.Drawing.Color.White
        Me.pnlDetalle.Controls.Add(Me.OlvDetalle)
        Me.pnlDetalle.Controls.Add(Me.lblDetalleTitle)
        Me.pnlDetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlDetalle.Location = New System.Drawing.Point(0, 265)
        Me.pnlDetalle.Name = "pnlDetalle"
        Me.pnlDetalle.Padding = New System.Windows.Forms.Padding(20, 10, 20, 0)
        Me.pnlDetalle.Size = New System.Drawing.Size(944, 314)
        Me.pnlDetalle.TabIndex = 0
        '
        'OlvDetalle
        '
        Me.OlvDetalle.AlternateRowBackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.OlvDetalle.BackColor = System.Drawing.Color.White
        Me.OlvDetalle.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick
        Me.OlvDetalle.CellEditUseWholeCell = False
        Me.OlvDetalle.CheckBoxes = True
        Me.OlvDetalle.CheckedAspectName = "Seleccionado"
        Me.OlvDetalle.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colCodProducto, Me.colProducto, Me.colEnviado, Me.colRecibido, Me.colUnidad, Me.colEstado})
        Me.OlvDetalle.Cursor = System.Windows.Forms.Cursors.Default
        Me.OlvDetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OlvDetalle.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.OlvDetalle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.OlvDetalle.FullRowSelect = True
        Me.OlvDetalle.GridLines = True
        Me.OlvDetalle.HideSelection = False
        Me.OlvDetalle.Location = New System.Drawing.Point(20, 35)
        Me.OlvDetalle.Name = "OlvDetalle"
        Me.OlvDetalle.RowHeight = 32
        Me.OlvDetalle.ShowGroups = False
        Me.OlvDetalle.Size = New System.Drawing.Size(904, 279)
        Me.OlvDetalle.TabIndex = 0
        Me.OlvDetalle.UseAlternatingBackColors = True
        Me.OlvDetalle.UseCompatibleStateImageBehavior = False
        Me.OlvDetalle.View = System.Windows.Forms.View.Details
        '
        'colCodProducto
        '
        Me.colCodProducto.AspectName = "CodProducto"
        Me.colCodProducto.HeaderCheckBox = True
        Me.colCodProducto.MinimumWidth = 100
        Me.colCodProducto.Text = "COD_PRODUCTO"
        Me.colCodProducto.Width = 100
        '
        'colProducto
        '
        Me.colProducto.AspectName = "Producto"
        Me.colProducto.MinimumWidth = 250
        Me.colProducto.Text = "PRODUCTO"
        Me.colProducto.Width = 300
        '
        'colEnviado
        '
        Me.colEnviado.AspectName = "CantEnviada"
        Me.colEnviado.Text = "ENVIADO"
        Me.colEnviado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.colEnviado.Width = 90
        '
        'colRecibido
        '
        Me.colRecibido.AspectName = "CantRecibida"
        Me.colRecibido.Text = "RECIBIDO"
        Me.colRecibido.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.colRecibido.Width = 90
        '
        'colUnidad
        '
        Me.colUnidad.AspectName = "Unidad"
        Me.colUnidad.Text = "UNIDAD"
        Me.colUnidad.Width = 80
        '
        'colEstado
        '
        Me.colEstado.AspectName = "EstadoItem"
        Me.colEstado.Text = "ESTADO"
        Me.colEstado.Width = 100
        '
        'lblDetalleTitle
        '
        Me.lblDetalleTitle.AutoSize = True
        Me.lblDetalleTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblDetalleTitle.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblDetalleTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(105, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.lblDetalleTitle.Location = New System.Drawing.Point(20, 10)
        Me.lblDetalleTitle.Name = "lblDetalleTitle"
        Me.lblDetalleTitle.Padding = New System.Windows.Forms.Padding(0, 0, 0, 6)
        Me.lblDetalleTitle.Size = New System.Drawing.Size(588, 25)
        Me.lblDetalleTitle.TabIndex = 1
        Me.lblDetalleTitle.Text = "DETALLE DE LA TRANSFERENCIA  —  productos nuevos se registran automáticamente ✦"
        '
        'pnlNovedad
        '
        Me.pnlNovedad.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlNovedad.Controls.Add(Me.lblNovedad)
        Me.pnlNovedad.Controls.Add(Me.txtNovedad)
        Me.pnlNovedad.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlNovedad.Location = New System.Drawing.Point(0, 579)
        Me.pnlNovedad.Name = "pnlNovedad"
        Me.pnlNovedad.Padding = New System.Windows.Forms.Padding(20, 6, 20, 6)
        Me.pnlNovedad.Size = New System.Drawing.Size(944, 68)
        Me.pnlNovedad.TabIndex = 5
        '
        'lblNovedad
        '
        Me.lblNovedad.AutoSize = True
        Me.lblNovedad.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblNovedad.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.lblNovedad.Location = New System.Drawing.Point(20, 8)
        Me.lblNovedad.Name = "lblNovedad"
        Me.lblNovedad.Size = New System.Drawing.Size(161, 15)
        Me.lblNovedad.TabIndex = 0
        Me.lblNovedad.Text = "NOVEDAD / OBSERVACIÓN:"
        '
        'txtNovedad
        '
        Me.txtNovedad.BackColor = System.Drawing.Color.White
        Me.txtNovedad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNovedad.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.txtNovedad.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.txtNovedad.Location = New System.Drawing.Point(20, 26)
        Me.txtNovedad.Multiline = True
        Me.txtNovedad.Name = "txtNovedad"
        Me.txtNovedad.Size = New System.Drawing.Size(906, 34)
        Me.txtNovedad.TabIndex = 1
        '
        'pnlFooter
        '
        Me.pnlFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.pnlFooter.Controls.Add(Me.btnCancelar)
        Me.pnlFooter.Controls.Add(Me.lblNuevosAviso)
        Me.pnlFooter.Controls.Add(Me.btnAceptar)
        Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFooter.Location = New System.Drawing.Point(0, 647)
        Me.pnlFooter.Name = "pnlFooter"
        Me.pnlFooter.Padding = New System.Windows.Forms.Padding(20, 12, 20, 12)
        Me.pnlFooter.Size = New System.Drawing.Size(944, 64)
        Me.pnlFooter.TabIndex = 6
        '
        'btnCancelar
        '
        Me.btnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancelar.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelar.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(95, Byte), Integer), CType(CType(115, Byte), Integer))
        Me.btnCancelar.Location = New System.Drawing.Point(510, 13)
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
        Me.lblNuevosAviso.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(80, Byte), Integer))
        Me.lblNuevosAviso.Location = New System.Drawing.Point(145, 22)
        Me.lblNuevosAviso.Name = "lblNuevosAviso"
        Me.lblNuevosAviso.Size = New System.Drawing.Size(0, 13)
        Me.lblNuevosAviso.TabIndex = 1
        '
        'btnAceptar
        '
        Me.btnAceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAceptar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAceptar.Enabled = False
        Me.btnAceptar.FlatAppearance.BorderSize = 0
        Me.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAceptar.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold)
        Me.btnAceptar.ForeColor = System.Drawing.Color.White
        Me.btnAceptar.Location = New System.Drawing.Point(643, 13)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(278, 38)
        Me.btnAceptar.TabIndex = 2
        Me.btnAceptar.Text = "✔  Aceptar productos llegados"
        Me.btnAceptar.UseVisualStyleBackColor = False
        '
        'pnlLoading
        '
        Me.pnlLoading.BackColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(248, Byte), Integer))
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
        Me.lblLoadingMsg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(105, Byte), Integer))
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
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(944, 711)
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
        CType(Me.OlvTransferencias, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuTransferencias.ResumeLayout(False)
        Me.pnlDetalle.ResumeLayout(False)
        Me.pnlDetalle.PerformLayout()
        CType(Me.OlvDetalle, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents OlvTransferencias As BrightIdeasSoftware.ObjectListView
    Friend WithEvents colAccion As BrightIdeasSoftware.OLVColumn
    Friend WithEvents colNumero As BrightIdeasSoftware.OLVColumn
    Friend WithEvents colOrigen As BrightIdeasSoftware.OLVColumn
    Friend WithEvents colFecha As BrightIdeasSoftware.OLVColumn
    Friend WithEvents mnuTransferencias As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuImprimirDetalle As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRechazar As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents splitter As System.Windows.Forms.Panel
    Friend WithEvents pnlDetalle As System.Windows.Forms.Panel
    Friend WithEvents lblDetalleTitle As System.Windows.Forms.Label
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

    Friend WithEvents OlvDetalle As BrightIdeasSoftware.ObjectListView
    Friend WithEvents colProducto As BrightIdeasSoftware.OLVColumn
    Friend WithEvents colCodProducto As BrightIdeasSoftware.OLVColumn
    Friend WithEvents colEnviado As BrightIdeasSoftware.OLVColumn
    Friend WithEvents colRecibido As BrightIdeasSoftware.OLVColumn
    Friend WithEvents colUnidad As BrightIdeasSoftware.OLVColumn
    Friend WithEvents colEstado As BrightIdeasSoftware.OLVColumn


End Class