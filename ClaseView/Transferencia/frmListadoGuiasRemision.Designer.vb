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
        Me.components = New System.ComponentModel.Container()

        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.lblSubtitulo = New System.Windows.Forms.Label()
        Me.pnlFiltros = New System.Windows.Forms.Panel()
        Me.lblFiltro = New System.Windows.Forms.Label()
        Me.cboFiltroEstado = New System.Windows.Forms.ComboBox()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.pnlGrid = New System.Windows.Forms.Panel()
        Me.DgvGuias = New System.Windows.Forms.DataGridView()
        Me.ColId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColNum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColFecha = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColOrigen = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColDestino = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColEstado = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColNovedad = New System.Windows.Forms.DataGridViewTextBoxColumn()
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

        ' ── Form ──────────────────────────────────────────────────────────────
        Me.Text = "Listado de Guías de Remisión"
        Me.Size = New System.Drawing.Size(920, 580)
        Me.MinimumSize = New System.Drawing.Size(920, 580)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.BackColor = System.Drawing.Color.FromArgb(18, 22, 30)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)

        ' ── pnlHeader ─────────────────────────────────────────────────────────
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Height = 72
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(24, 30, 42)

        Me.lblTitulo.Text = "GUÍAS DE REMISIÓN"
        Me.lblTitulo.Font = New System.Drawing.Font("Segoe UI Semibold", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(120, 160, 255)
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.Location = New System.Drawing.Point(20, 14)

        Me.lblSubtitulo.Text = "Historial de transferencias enviadas y recibidas"
        Me.lblSubtitulo.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(110, 130, 160)
        Me.lblSubtitulo.AutoSize = True
        Me.lblSubtitulo.Location = New System.Drawing.Point(22, 42)

        Me.pnlHeader.Controls.Add(Me.lblTitulo)
        Me.pnlHeader.Controls.Add(Me.lblSubtitulo)

        ' ── pnlFiltros ────────────────────────────────────────────────────────
        Me.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlFiltros.Height = 48
        Me.pnlFiltros.BackColor = System.Drawing.Color.FromArgb(22, 28, 40)
        Me.pnlFiltros.Padding = New System.Windows.Forms.Padding(20, 8, 20, 8)

        Me.lblFiltro.Text = "ESTADO:"
        Me.lblFiltro.Font = New System.Drawing.Font("Segoe UI", 7.5!, System.Drawing.FontStyle.Bold)
        Me.lblFiltro.ForeColor = System.Drawing.Color.FromArgb(100, 120, 150)
        Me.lblFiltro.AutoSize = True
        Me.lblFiltro.Location = New System.Drawing.Point(20, 16)

        Me.cboFiltroEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFiltroEstado.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboFiltroEstado.BackColor = System.Drawing.Color.FromArgb(32, 40, 58)
        Me.cboFiltroEstado.ForeColor = System.Drawing.Color.FromArgb(210, 220, 240)
        Me.cboFiltroEstado.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.cboFiltroEstado.Location = New System.Drawing.Point(76, 12)
        Me.cboFiltroEstado.Width = 160
        Me.cboFiltroEstado.Items.AddRange(New Object() {"Todos", "PENDIENTE", "ENVIADO", "RECIBIDO", "CON_NOVEDAD"})
        Me.cboFiltroEstado.SelectedIndex = 0

        Me.lblTotal.Text = ""
        Me.lblTotal.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
        Me.lblTotal.ForeColor = System.Drawing.Color.FromArgb(120, 160, 255)
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Location = New System.Drawing.Point(260, 16)

        Me.pnlFiltros.Controls.Add(Me.lblFiltro)
        Me.pnlFiltros.Controls.Add(Me.cboFiltroEstado)
        Me.pnlFiltros.Controls.Add(Me.lblTotal)

        ' ── pnlGrid ───────────────────────────────────────────────────────────
        Me.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlGrid.BackColor = System.Drawing.Color.FromArgb(18, 22, 30)
        Me.pnlGrid.Padding = New System.Windows.Forms.Padding(20, 12, 20, 0)

        ' DgvGuias
        Me.DgvGuias.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DgvGuias.ReadOnly = True
        Me.DgvGuias.AllowUserToAddRows = False
        Me.DgvGuias.AllowUserToDeleteRows = False
        Me.DgvGuias.AllowUserToResizeRows = False
        Me.DgvGuias.AutoGenerateColumns = False
        Me.DgvGuias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DgvGuias.MultiSelect = False
        Me.DgvGuias.RowHeadersVisible = False
        Me.DgvGuias.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DgvGuias.BackgroundColor = System.Drawing.Color.FromArgb(22, 28, 40)
        Me.DgvGuias.GridColor = System.Drawing.Color.FromArgb(35, 44, 62)
        Me.DgvGuias.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
        Me.DgvGuias.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.DgvGuias.EnableHeadersVisualStyles = False
        Me.DgvGuias.ColumnHeadersHeight = 36
        Me.DgvGuias.RowTemplate.Height = 34
        Me.DgvGuias.Font = New System.Drawing.Font("Segoe UI", 9.5!)

        Me.DgvGuias.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(30, 38, 55)
        Me.DgvGuias.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(120, 160, 255)
        Me.DgvGuias.ColumnHeadersDefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 8.5!, System.Drawing.FontStyle.Bold)
        Me.DgvGuias.ColumnHeadersDefaultCellStyle.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)

        Me.DgvGuias.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(22, 28, 40)
        Me.DgvGuias.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(210, 220, 240)
        Me.DgvGuias.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(60, 90, 200)
        Me.DgvGuias.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White
        Me.DgvGuias.DefaultCellStyle.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.DgvGuias.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(26, 33, 48)

        ' Columns
        Me.ColId.Name = "ColId"
        Me.ColId.DataPropertyName = "idTransferencia"
        Me.ColId.Visible = False

        Me.ColNum.Name = "ColNum"
        Me.ColNum.HeaderText = "N° GUÍA"
        Me.ColNum.DataPropertyName = "NumTransferencia"
        Me.ColNum.Width = 160

        Me.ColFecha.Name = "ColFecha"
        Me.ColFecha.HeaderText = "FECHA"
        Me.ColFecha.DataPropertyName = "FechaEmision"
        Me.ColFecha.Width = 130
        Me.ColFecha.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"

        Me.ColOrigen.Name = "ColOrigen"
        Me.ColOrigen.HeaderText = "ORIGEN"
        Me.ColOrigen.DataPropertyName = "Origen"
        Me.ColOrigen.Width = 180

        Me.ColDestino.Name = "ColDestino"
        Me.ColDestino.HeaderText = "DESTINO"
        Me.ColDestino.DataPropertyName = "Destino"
        Me.ColDestino.Width = 180

        Me.ColEstado.Name = "ColEstado"
        Me.ColEstado.HeaderText = "ESTADO"
        Me.ColEstado.DataPropertyName = "EstadoEnvio"
        Me.ColEstado.Width = 110

        Me.ColNovedad.Name = "ColNovedad"
        Me.ColNovedad.HeaderText = "NOVEDAD"
        Me.ColNovedad.DataPropertyName = "Novedad"
        Me.ColNovedad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ColNovedad.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(255, 140, 60)

        Me.DgvGuias.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {
            Me.ColId, Me.ColNum, Me.ColFecha, Me.ColOrigen,
            Me.ColDestino, Me.ColEstado, Me.ColNovedad})

        Me.pnlGrid.Controls.Add(Me.DgvGuias)

        ' ── pnlFooter ─────────────────────────────────────────────────────────
        Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFooter.Height = 64
        Me.pnlFooter.BackColor = System.Drawing.Color.FromArgb(24, 30, 42)
        Me.pnlFooter.Padding = New System.Windows.Forms.Padding(20, 12, 20, 12)

        Me.btnActualizar.Text = "↻  Actualizar"
        Me.btnActualizar.Size = New System.Drawing.Size(120, 38)
        Me.btnActualizar.Location = New System.Drawing.Point(20, 13)
        Me.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnActualizar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(60, 90, 140)
        Me.btnActualizar.BackColor = System.Drawing.Color.FromArgb(30, 44, 80)
        Me.btnActualizar.ForeColor = System.Drawing.Color.FromArgb(120, 160, 255)
        Me.btnActualizar.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand

        Me.btnReimprimir.Text = "🖨  Reimprimir"
        Me.btnReimprimir.Size = New System.Drawing.Size(140, 38)
        Me.btnReimprimir.Location = New System.Drawing.Point(152, 13)
        Me.btnReimprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnReimprimir.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(60, 90, 140)
        Me.btnReimprimir.BackColor = System.Drawing.Color.FromArgb(30, 44, 80)
        Me.btnReimprimir.ForeColor = System.Drawing.Color.FromArgb(120, 160, 255)
        Me.btnReimprimir.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnReimprimir.Cursor = System.Windows.Forms.Cursors.Hand

        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(110, 38)
        Me.btnCerrar.Anchor = System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right
        Me.btnCerrar.Location = New System.Drawing.Point(786, 13)
        Me.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCerrar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(60, 75, 100)
        Me.btnCerrar.BackColor = System.Drawing.Color.FromArgb(35, 44, 62)
        Me.btnCerrar.ForeColor = System.Drawing.Color.FromArgb(160, 175, 200)
        Me.btnCerrar.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand

        Me.pnlFooter.Controls.Add(Me.btnActualizar)
        Me.pnlFooter.Controls.Add(Me.btnReimprimir)
        Me.pnlFooter.Controls.Add(Me.btnCerrar)

        ' ── Form Controls ─────────────────────────────────────────────────────
        Me.Controls.Add(Me.pnlGrid)
        Me.Controls.Add(Me.pnlFiltros)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.pnlFooter)

        Me.pnlHeader.ResumeLayout(False)
        Me.pnlFiltros.ResumeLayout(False)
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