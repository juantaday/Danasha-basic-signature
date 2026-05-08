<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTransferencia
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso (components IsNot Nothing) Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.lblSubtitulo = New System.Windows.Forms.Label()
        Me.pnlBody = New System.Windows.Forms.Panel()
        Me.pnlGridOuter = New System.Windows.Forms.Panel()
        Me.DgvDetalle = New System.Windows.Forms.DataGridView()
        Me.lblConteo = New System.Windows.Forms.Label()
        Me.lblGridTitle = New System.Windows.Forms.Label()
        Me.pnlRuta = New System.Windows.Forms.Panel()
        Me.lblDesde = New System.Windows.Forms.Label()
        Me.cboOrigen = New System.Windows.Forms.ComboBox()
        Me.lblFlecha = New System.Windows.Forms.Label()
        Me.lblHacia = New System.Windows.Forms.Label()
        Me.cboDestino = New System.Windows.Forms.ComboBox()
        Me.pnlFooter = New System.Windows.Forms.Panel()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnGuias = New System.Windows.Forms.Button()
        Me.btnConfirmar = New System.Windows.Forms.Button()
        Me.pnlLoading = New System.Windows.Forms.Panel()
        Me.lblLoadingMsg = New System.Windows.Forms.Label()
        Me.progressBar = New System.Windows.Forms.ProgressBar()
        Me.pnlHeader.SuspendLayout()
        Me.pnlBody.SuspendLayout()
        Me.pnlGridOuter.SuspendLayout()
        CType(Me.DgvDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlRuta.SuspendLayout()
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
        Me.pnlHeader.Padding = New System.Windows.Forms.Padding(20, 0, 20, 0)
        Me.pnlHeader.Size = New System.Drawing.Size(1031, 72)
        Me.pnlHeader.TabIndex = 1
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(115, Byte), Integer))
        Me.lblTitulo.Location = New System.Drawing.Point(20, 8)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(303, 25)
        Me.lblTitulo.TabIndex = 0
        Me.lblTitulo.Text = "TRANSFERENCIA DE PRODUCTOS"
        '
        'lblSubtitulo
        '
        Me.lblSubtitulo.AutoSize = True
        Me.lblSubtitulo.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.lblSubtitulo.Location = New System.Drawing.Point(22, 42)
        Me.lblSubtitulo.Name = "lblSubtitulo"
        Me.lblSubtitulo.Size = New System.Drawing.Size(285, 19)
        Me.lblSubtitulo.TabIndex = 1
        Me.lblSubtitulo.Text = "Seleccione origen, destino y confirme el envío"
        '
        'pnlBody
        '
        Me.pnlBody.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlBody.Controls.Add(Me.pnlGridOuter)
        Me.pnlBody.Controls.Add(Me.pnlRuta)
        Me.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlBody.Location = New System.Drawing.Point(0, 72)
        Me.pnlBody.Name = "pnlBody"
        Me.pnlBody.Padding = New System.Windows.Forms.Padding(20, 16, 20, 0)
        Me.pnlBody.Size = New System.Drawing.Size(1031, 496)
        Me.pnlBody.TabIndex = 0
        '
        'pnlGridOuter
        '
        Me.pnlGridOuter.BackColor = System.Drawing.Color.White
        Me.pnlGridOuter.Controls.Add(Me.DgvDetalle)
        Me.pnlGridOuter.Controls.Add(Me.lblConteo)
        Me.pnlGridOuter.Controls.Add(Me.lblGridTitle)
        Me.pnlGridOuter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGridOuter.Location = New System.Drawing.Point(20, 96)
        Me.pnlGridOuter.Name = "pnlGridOuter"
        Me.pnlGridOuter.Padding = New System.Windows.Forms.Padding(0, 12, 0, 0)
        Me.pnlGridOuter.Size = New System.Drawing.Size(991, 400)
        Me.pnlGridOuter.TabIndex = 0
        '
        'DgvDetalle
        '
        Me.DgvDetalle.AllowUserToAddRows = False
        Me.DgvDetalle.AllowUserToDeleteRows = False
        Me.DgvDetalle.AllowUserToResizeRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.DgvDetalle.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.DgvDetalle.BackgroundColor = System.Drawing.Color.White
        Me.DgvDetalle.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DgvDetalle.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DgvDetalle.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(125, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle5.Padding = New System.Windows.Forms.Padding(12, 0, 0, 0)
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvDetalle.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.DgvDetalle.ColumnHeadersHeight = 38
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(70, Byte), Integer))
        DataGridViewCellStyle6.Padding = New System.Windows.Forms.Padding(12, 0, 0, 0)
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(230, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(65, Byte), Integer))
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvDetalle.DefaultCellStyle = DataGridViewCellStyle6
        Me.DgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvDetalle.EnableHeadersVisualStyles = False
        Me.DgvDetalle.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.DgvDetalle.GridColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.DgvDetalle.Location = New System.Drawing.Point(0, 37)
        Me.DgvDetalle.MultiSelect = False
        Me.DgvDetalle.Name = "DgvDetalle"
        Me.DgvDetalle.ReadOnly = True
        Me.DgvDetalle.RowHeadersVisible = False
        Me.DgvDetalle.RowTemplate.Height = 36
        Me.DgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvDetalle.Size = New System.Drawing.Size(991, 363)
        Me.DgvDetalle.TabIndex = 0
        '
        'lblConteo
        '
        Me.lblConteo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblConteo.AutoSize = True
        Me.lblConteo.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.lblConteo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(115, Byte), Integer))
        Me.lblConteo.Location = New System.Drawing.Point(791, 8)
        Me.lblConteo.Name = "lblConteo"
        Me.lblConteo.Size = New System.Drawing.Size(69, 13)
        Me.lblConteo.TabIndex = 1
        Me.lblConteo.Text = "0 productos"
        '
        'lblGridTitle
        '
        Me.lblGridTitle.AutoSize = True
        Me.lblGridTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblGridTitle.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblGridTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(105, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.lblGridTitle.Location = New System.Drawing.Point(0, 12)
        Me.lblGridTitle.Name = "lblGridTitle"
        Me.lblGridTitle.Padding = New System.Windows.Forms.Padding(0, 0, 0, 6)
        Me.lblGridTitle.Size = New System.Drawing.Size(273, 25)
        Me.lblGridTitle.TabIndex = 2
        Me.lblGridTitle.Text = "DETALLE DE PRODUCTOS A TRANSFERIR"
        '
        'pnlRuta
        '
        Me.pnlRuta.BackColor = System.Drawing.Color.White
        Me.pnlRuta.Controls.Add(Me.lblDesde)
        Me.pnlRuta.Controls.Add(Me.cboOrigen)
        Me.pnlRuta.Controls.Add(Me.lblFlecha)
        Me.pnlRuta.Controls.Add(Me.lblHacia)
        Me.pnlRuta.Controls.Add(Me.cboDestino)
        Me.pnlRuta.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlRuta.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.pnlRuta.Location = New System.Drawing.Point(20, 16)
        Me.pnlRuta.Name = "pnlRuta"
        Me.pnlRuta.Padding = New System.Windows.Forms.Padding(16, 10, 16, 10)
        Me.pnlRuta.Size = New System.Drawing.Size(991, 80)
        Me.pnlRuta.TabIndex = 1
        '
        'lblDesde
        '
        Me.lblDesde.AutoSize = True
        Me.lblDesde.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblDesde.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.lblDesde.Location = New System.Drawing.Point(16, 10)
        Me.lblDesde.Name = "lblDesde"
        Me.lblDesde.Size = New System.Drawing.Size(44, 15)
        Me.lblDesde.TabIndex = 0
        Me.lblDesde.Text = "DESDE"
        '
        'cboOrigen
        '
        Me.cboOrigen.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.cboOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOrigen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboOrigen.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cboOrigen.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.cboOrigen.Location = New System.Drawing.Point(16, 37)
        Me.cboOrigen.Name = "cboOrigen"
        Me.cboOrigen.Size = New System.Drawing.Size(280, 25)
        Me.cboOrigen.TabIndex = 1
        '
        'lblFlecha
        '
        Me.lblFlecha.AutoSize = True
        Me.lblFlecha.Font = New System.Drawing.Font("Segoe UI", 18.0!)
        Me.lblFlecha.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.lblFlecha.Location = New System.Drawing.Point(331, 31)
        Me.lblFlecha.Name = "lblFlecha"
        Me.lblFlecha.Size = New System.Drawing.Size(38, 32)
        Me.lblFlecha.TabIndex = 2
        Me.lblFlecha.Text = "➜"
        '
        'lblHacia
        '
        Me.lblHacia.AutoSize = True
        Me.lblHacia.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblHacia.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.lblHacia.Location = New System.Drawing.Point(417, 10)
        Me.lblHacia.Name = "lblHacia"
        Me.lblHacia.Size = New System.Drawing.Size(43, 15)
        Me.lblHacia.TabIndex = 3
        Me.lblHacia.Text = "HACIA"
        '
        'cboDestino
        '
        Me.cboDestino.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.cboDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDestino.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboDestino.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cboDestino.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.cboDestino.Location = New System.Drawing.Point(417, 37)
        Me.cboDestino.Name = "cboDestino"
        Me.cboDestino.Size = New System.Drawing.Size(376, 25)
        Me.cboDestino.TabIndex = 4
        '
        'pnlFooter
        '
        Me.pnlFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.pnlFooter.Controls.Add(Me.btnCancelar)
        Me.pnlFooter.Controls.Add(Me.btnGuias)
        Me.pnlFooter.Controls.Add(Me.btnConfirmar)
        Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFooter.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.pnlFooter.Location = New System.Drawing.Point(0, 568)
        Me.pnlFooter.Name = "pnlFooter"
        Me.pnlFooter.Padding = New System.Windows.Forms.Padding(20, 12, 20, 12)
        Me.pnlFooter.Size = New System.Drawing.Size(1031, 64)
        Me.pnlFooter.TabIndex = 2
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
        Me.btnCancelar.Location = New System.Drawing.Point(503, 14)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(110, 38)
        Me.btnCancelar.TabIndex = 0
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = False
        '
        'btnGuias
        '
        Me.btnGuias.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGuias.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnGuias.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnGuias.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.btnGuias.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGuias.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.btnGuias.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.btnGuias.Location = New System.Drawing.Point(625, 14)
        Me.btnGuias.Name = "btnGuias"
        Me.btnGuias.Size = New System.Drawing.Size(172, 38)
        Me.btnGuias.TabIndex = 1
        Me.btnGuias.Text = "📋 Ver Guías"
        Me.btnGuias.UseVisualStyleBackColor = False
        '
        'btnConfirmar
        '
        Me.btnConfirmar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConfirmar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.btnConfirmar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnConfirmar.FlatAppearance.BorderSize = 0
        Me.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConfirmar.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnConfirmar.ForeColor = System.Drawing.Color.White
        Me.btnConfirmar.Location = New System.Drawing.Point(828, 14)
        Me.btnConfirmar.Name = "btnConfirmar"
        Me.btnConfirmar.Size = New System.Drawing.Size(180, 38)
        Me.btnConfirmar.TabIndex = 2
        Me.btnConfirmar.Text = "✔  Confirmar Envío"
        Me.btnConfirmar.UseVisualStyleBackColor = False
        '
        'pnlLoading
        '
        Me.pnlLoading.BackColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.pnlLoading.Controls.Add(Me.lblLoadingMsg)
        Me.pnlLoading.Controls.Add(Me.progressBar)
        Me.pnlLoading.Location = New System.Drawing.Point(0, 0)
        Me.pnlLoading.Name = "pnlLoading"
        Me.pnlLoading.Size = New System.Drawing.Size(820, 620)
        Me.pnlLoading.TabIndex = 3
        Me.pnlLoading.Visible = False
        '
        'lblLoadingMsg
        '
        Me.lblLoadingMsg.Font = New System.Drawing.Font("Segoe UI", 13.0!, System.Drawing.FontStyle.Bold)
        Me.lblLoadingMsg.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(130, Byte), Integer), CType(CType(105, Byte), Integer))
        Me.lblLoadingMsg.Location = New System.Drawing.Point(210, 270)
        Me.lblLoadingMsg.Name = "lblLoadingMsg"
        Me.lblLoadingMsg.Size = New System.Drawing.Size(400, 34)
        Me.lblLoadingMsg.TabIndex = 0
        Me.lblLoadingMsg.Text = "Enviando a Supabase..."
        Me.lblLoadingMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'progressBar
        '
        Me.progressBar.Location = New System.Drawing.Point(210, 310)
        Me.progressBar.MarqueeAnimationSpeed = 25
        Me.progressBar.Name = "progressBar"
        Me.progressBar.Size = New System.Drawing.Size(400, 6)
        Me.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.progressBar.TabIndex = 1
        '
        'frmTransferencia
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1031, 632)
        Me.Controls.Add(Me.pnlBody)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.pnlFooter)
        Me.Controls.Add(Me.pnlLoading)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(820, 620)
        Me.Name = "frmTransferencia"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Transferencia de Productos"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlBody.ResumeLayout(False)
        Me.pnlGridOuter.ResumeLayout(False)
        Me.pnlGridOuter.PerformLayout()
        CType(Me.DgvDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlRuta.ResumeLayout(False)
        Me.pnlRuta.PerformLayout()
        Me.pnlFooter.ResumeLayout(False)
        Me.pnlLoading.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    ' ── Control declarations ────────────────────────────────────────────────────
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents lblSubtitulo As System.Windows.Forms.Label
    Friend WithEvents pnlBody As System.Windows.Forms.Panel
    Friend WithEvents pnlRuta As System.Windows.Forms.Panel
    Friend WithEvents lblDesde As System.Windows.Forms.Label
    Friend WithEvents cboOrigen As System.Windows.Forms.ComboBox
    Friend WithEvents lblFlecha As System.Windows.Forms.Label
    Friend WithEvents lblHacia As System.Windows.Forms.Label
    Friend WithEvents cboDestino As System.Windows.Forms.ComboBox
    Friend WithEvents pnlGridOuter As System.Windows.Forms.Panel
    Friend WithEvents lblGridTitle As System.Windows.Forms.Label
    Friend WithEvents lblConteo As System.Windows.Forms.Label
    Friend WithEvents DgvDetalle As System.Windows.Forms.DataGridView
    Friend WithEvents ColProducto As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColCantidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColUnidad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pnlFooter As System.Windows.Forms.Panel
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnGuias As System.Windows.Forms.Button
    Friend WithEvents btnConfirmar As System.Windows.Forms.Button
    Friend WithEvents pnlLoading As System.Windows.Forms.Panel
    Friend WithEvents lblLoadingMsg As System.Windows.Forms.Label
    Friend WithEvents progressBar As System.Windows.Forms.ProgressBar

End Class