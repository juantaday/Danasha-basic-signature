<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmListadoGuiasRemision
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.lblSubtitulo = New System.Windows.Forms.Label()
        Me.pnlFiltros = New System.Windows.Forms.Panel()
        Me.lblFiltro = New System.Windows.Forms.Label()
        Me.cboFiltroEstado = New System.Windows.Forms.ComboBox()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.pnlGrid = New System.Windows.Forms.Panel()
        Me.DgvGuias = New System.Windows.Forms.DataGridView()
        Me.pnlFooter = New System.Windows.Forms.Panel()
        Me.btnActualizar = New System.Windows.Forms.Button()
        Me.btnReimprimir = New System.Windows.Forms.Button()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.pnlHeader.SuspendLayout()
        Me.pnlFiltros.SuspendLayout()
        Me.pnlGrid.SuspendLayout()
        CType(Me.DgvGuias, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFooter.SuspendLayout()
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
        Me.pnlHeader.Size = New System.Drawing.Size(904, 72)
        Me.pnlHeader.TabIndex = 2
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(115, Byte), Integer))
        Me.lblTitulo.Location = New System.Drawing.Point(20, 14)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(192, 25)
        Me.lblTitulo.TabIndex = 0
        Me.lblTitulo.Text = "GUÍAS DE REMISIÓN"
        '
        'lblSubtitulo
        '
        Me.lblSubtitulo.AutoSize = True
        Me.lblSubtitulo.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(135, Byte), Integer), CType(CType(150, Byte), Integer))
        Me.lblSubtitulo.Location = New System.Drawing.Point(22, 42)
        Me.lblSubtitulo.Name = "lblSubtitulo"
        Me.lblSubtitulo.Size = New System.Drawing.Size(251, 15)
        Me.lblSubtitulo.TabIndex = 1
        Me.lblSubtitulo.Text = "Historial de transferencias enviadas y recibidas"
        '
        'pnlFiltros
        '
        Me.pnlFiltros.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlFiltros.Controls.Add(Me.lblFiltro)
        Me.pnlFiltros.Controls.Add(Me.cboFiltroEstado)
        Me.pnlFiltros.Controls.Add(Me.lblTotal)
        Me.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFiltros.Location = New System.Drawing.Point(0, 72)
        Me.pnlFiltros.Name = "pnlFiltros"
        Me.pnlFiltros.Padding = New System.Windows.Forms.Padding(20, 8, 20, 8)
        Me.pnlFiltros.Size = New System.Drawing.Size(904, 48)
        Me.pnlFiltros.TabIndex = 1
        '
        'lblFiltro
        '
        Me.lblFiltro.AutoSize = True
        Me.lblFiltro.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblFiltro.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(115, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.lblFiltro.Location = New System.Drawing.Point(20, 16)
        Me.lblFiltro.Name = "lblFiltro"
        Me.lblFiltro.Size = New System.Drawing.Size(66, 19)
        Me.lblFiltro.TabIndex = 0
        Me.lblFiltro.Text = "ESTADO:"
        '
        'cboFiltroEstado
        '
        Me.cboFiltroEstado.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.cboFiltroEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFiltroEstado.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboFiltroEstado.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.cboFiltroEstado.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.cboFiltroEstado.Items.AddRange(New Object() {"Todos", "PENDIENTE", "ENVIADO", "RECIBIDO", "CON_NOVEDAD"})
        Me.cboFiltroEstado.Location = New System.Drawing.Point(93, 12)
        Me.cboFiltroEstado.Name = "cboFiltroEstado"
        Me.cboFiltroEstado.Size = New System.Drawing.Size(197, 25)
        Me.cboFiltroEstado.TabIndex = 1
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(115, Byte), Integer))
        Me.lblTotal.Location = New System.Drawing.Point(315, 16)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(0, 15)
        Me.lblTotal.TabIndex = 2
        '
        'pnlGrid
        '
        Me.pnlGrid.BackColor = System.Drawing.Color.White
        Me.pnlGrid.Controls.Add(Me.DgvGuias)
        Me.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGrid.Location = New System.Drawing.Point(0, 120)
        Me.pnlGrid.Name = "pnlGrid"
        Me.pnlGrid.Padding = New System.Windows.Forms.Padding(20, 12, 20, 0)
        Me.pnlGrid.Size = New System.Drawing.Size(904, 357)
        Me.pnlGrid.TabIndex = 0
        '
        'DgvGuias
        '
        Me.DgvGuias.AllowUserToAddRows = False
        Me.DgvGuias.AllowUserToDeleteRows = False
        Me.DgvGuias.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(247, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.DgvGuias.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DgvGuias.BackgroundColor = System.Drawing.Color.White
        Me.DgvGuias.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DgvGuias.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DgvGuias.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(150, Byte), Integer), CType(CType(125, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DgvGuias.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DgvGuias.ColumnHeadersHeight = 36
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(70, Byte), Integer))
        DataGridViewCellStyle3.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(230, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(80, Byte), Integer), CType(CType(65, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DgvGuias.DefaultCellStyle = DataGridViewCellStyle3
        Me.DgvGuias.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvGuias.EnableHeadersVisualStyles = False
        Me.DgvGuias.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.DgvGuias.GridColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.DgvGuias.Location = New System.Drawing.Point(20, 12)
        Me.DgvGuias.MultiSelect = False
        Me.DgvGuias.Name = "DgvGuias"
        Me.DgvGuias.ReadOnly = True
        Me.DgvGuias.RowHeadersVisible = False
        Me.DgvGuias.RowTemplate.Height = 34
        Me.DgvGuias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvGuias.Size = New System.Drawing.Size(864, 345)
        Me.DgvGuias.TabIndex = 0
        '
        'pnlFooter
        '
        Me.pnlFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.pnlFooter.Controls.Add(Me.btnActualizar)
        Me.pnlFooter.Controls.Add(Me.btnReimprimir)
        Me.pnlFooter.Controls.Add(Me.btnCerrar)
        Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFooter.Location = New System.Drawing.Point(0, 477)
        Me.pnlFooter.Name = "pnlFooter"
        Me.pnlFooter.Padding = New System.Windows.Forms.Padding(20, 12, 20, 12)
        Me.pnlFooter.Size = New System.Drawing.Size(904, 64)
        Me.pnlFooter.TabIndex = 3
        '
        'btnActualizar
        '
        Me.btnActualizar.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnActualizar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnActualizar.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.btnActualizar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.btnActualizar.Location = New System.Drawing.Point(20, 13)
        Me.btnActualizar.Name = "btnActualizar"
        Me.btnActualizar.Size = New System.Drawing.Size(120, 38)
        Me.btnActualizar.TabIndex = 0
        Me.btnActualizar.Text = "↻  Actualizar"
        Me.btnActualizar.UseVisualStyleBackColor = False
        '
        'btnReimprimir
        '
        Me.btnReimprimir.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnReimprimir.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnReimprimir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.btnReimprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReimprimir.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.btnReimprimir.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.btnReimprimir.Location = New System.Drawing.Point(152, 13)
        Me.btnReimprimir.Name = "btnReimprimir"
        Me.btnReimprimir.Size = New System.Drawing.Size(140, 38)
        Me.btnReimprimir.TabIndex = 1
        Me.btnReimprimir.Text = "🖨  Reimprimir"
        Me.btnReimprimir.UseVisualStyleBackColor = False
        '
        'btnCerrar
        '
        Me.btnCerrar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCerrar.BackColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(225, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCerrar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(190, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCerrar.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnCerrar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(80, Byte), Integer), CType(CType(95, Byte), Integer), CType(CType(115, Byte), Integer))
        Me.btnCerrar.Location = New System.Drawing.Point(1490, 13)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(110, 38)
        Me.btnCerrar.TabIndex = 2
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.UseVisualStyleBackColor = False
        '
        'frmListadoGuiasRemision
        '
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(904, 541)
        Me.Controls.Add(Me.pnlGrid)
        Me.Controls.Add(Me.pnlFiltros)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.pnlFooter)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimumSize = New System.Drawing.Size(920, 580)
        Me.Name = "frmListadoGuiasRemision"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Listado de Guías de Remisión"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlFiltros.ResumeLayout(False)
        Me.pnlFiltros.PerformLayout()
        Me.pnlGrid.ResumeLayout(False)
        CType(Me.DgvGuias, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFooter.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents lblSubtitulo As System.Windows.Forms.Label
    Friend WithEvents pnlFiltros As System.Windows.Forms.Panel
    Friend WithEvents lblFiltro As System.Windows.Forms.Label
    Friend WithEvents cboFiltroEstado As System.Windows.Forms.ComboBox
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents pnlGrid As System.Windows.Forms.Panel
    Friend WithEvents DgvGuias As System.Windows.Forms.DataGridView
    Friend WithEvents ColId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColNum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColFecha As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColOrigen As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColDestino As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColEstado As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColNovedad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pnlFooter As System.Windows.Forms.Panel
    Friend WithEvents btnActualizar As System.Windows.Forms.Button
    Friend WithEvents btnReimprimir As System.Windows.Forms.Button
    Friend WithEvents btnCerrar As System.Windows.Forms.Button

End Class