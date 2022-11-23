<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmList_clientes
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnNuevo = New System.Windows.Forms.Button()
        Me.btnEditarCliente = New System.Windows.Forms.Button()
        Me.btnEliminarCliente = New System.Windows.Forms.Button()
        Me.btnCredit = New System.Windows.Forms.Button()
        Me.btnCobro = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCliente_Select = New System.Windows.Forms.TextBox()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.lblnoExiste = New System.Windows.Forms.Label()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.lblformaPago = New System.Windows.Forms.Label()
        Me.FlowMenu = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnDetail = New System.Windows.Forms.Button()
        Me.PaneBusca = New System.Windows.Forms.Panel()
        Me.PanePie = New System.Windows.Forms.Panel()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlowMenu.SuspendLayout()
        Me.PaneBusca.SuspendLayout()
        Me.PanePie.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.InactiveBorder
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridView1.BackgroundColor = System.Drawing.Color.White
        Me.DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 124)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DataGridView1.RowsDefaultCellStyle = DataGridViewCellStyle6
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(948, 316)
        Me.DataGridView1.TabIndex = 18
        '
        'btnBuscar
        '
        Me.btnBuscar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnBuscar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuscar.ForeColor = System.Drawing.Color.White
        Me.btnBuscar.Location = New System.Drawing.Point(507, 9)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(69, 37)
        Me.btnBuscar.TabIndex = 22
        Me.btnBuscar.Text = "Buscar"
        Me.btnBuscar.UseVisualStyleBackColor = False
        '
        'btnNuevo
        '
        Me.btnNuevo.BackColor = System.Drawing.SystemColors.Control
        Me.btnNuevo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnNuevo.Dock = System.Windows.Forms.DockStyle.Top
        Me.btnNuevo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNuevo.ForeColor = System.Drawing.Color.Black
        Me.btnNuevo.Image = Global.DanashaBasic.My.Resources.Resources.add_client_48
        Me.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnNuevo.Location = New System.Drawing.Point(227, 1)
        Me.btnNuevo.Margin = New System.Windows.Forms.Padding(1)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(66, 68)
        Me.btnNuevo.TabIndex = 29
        Me.btnNuevo.Text = "Agregar"
        Me.btnNuevo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnNuevo, "Agregar Nuevos Clientes")
        Me.btnNuevo.UseVisualStyleBackColor = False
        '
        'btnEditarCliente
        '
        Me.btnEditarCliente.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEditarCliente.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.btnEditarCliente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnEditarCliente.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnEditarCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditarCliente.ForeColor = System.Drawing.Color.Black
        Me.btnEditarCliente.Image = Global.DanashaBasic.My.Resources.Resources.Edd_client_48
        Me.btnEditarCliente.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnEditarCliente.Location = New System.Drawing.Point(295, 1)
        Me.btnEditarCliente.Margin = New System.Windows.Forms.Padding(1)
        Me.btnEditarCliente.Name = "btnEditarCliente"
        Me.btnEditarCliente.Size = New System.Drawing.Size(66, 68)
        Me.btnEditarCliente.TabIndex = 24
        Me.btnEditarCliente.Text = "Editar"
        Me.btnEditarCliente.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnEditarCliente, "Editar cliente")
        Me.btnEditarCliente.UseVisualStyleBackColor = False
        '
        'btnEliminarCliente
        '
        Me.btnEliminarCliente.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEliminarCliente.BackColor = System.Drawing.Color.Red
        Me.btnEliminarCliente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnEliminarCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEliminarCliente.ForeColor = System.Drawing.Color.White
        Me.btnEliminarCliente.Image = Global.DanashaBasic.My.Resources.Resources.Delete_cliente_48
        Me.btnEliminarCliente.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnEliminarCliente.Location = New System.Drawing.Point(363, 1)
        Me.btnEliminarCliente.Margin = New System.Windows.Forms.Padding(1)
        Me.btnEliminarCliente.Name = "btnEliminarCliente"
        Me.btnEliminarCliente.Size = New System.Drawing.Size(66, 68)
        Me.btnEliminarCliente.TabIndex = 25
        Me.btnEliminarCliente.Text = "Eliminar"
        Me.btnEliminarCliente.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnEliminarCliente, "Eliminar el cliente selecionado")
        Me.btnEliminarCliente.UseVisualStyleBackColor = False
        '
        'btnCredit
        '
        Me.btnCredit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCredit.BackColor = System.Drawing.Color.White
        Me.btnCredit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCredit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCredit.ForeColor = System.Drawing.Color.Black
        Me.btnCredit.Image = Global.DanashaBasic.My.Resources.Resources.Credid_48
        Me.btnCredit.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCredit.Location = New System.Drawing.Point(525, 1)
        Me.btnCredit.Margin = New System.Windows.Forms.Padding(1)
        Me.btnCredit.Name = "btnCredit"
        Me.btnCredit.Size = New System.Drawing.Size(66, 68)
        Me.btnCredit.TabIndex = 25
        Me.btnCredit.Text = "Credito"
        Me.btnCredit.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnCredit, "Administrar creditos ")
        Me.btnCredit.UseVisualStyleBackColor = False
        '
        'btnCobro
        '
        Me.btnCobro.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCobro.BackColor = System.Drawing.Color.White
        Me.btnCobro.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCobro.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCobro.ForeColor = System.Drawing.Color.Black
        Me.btnCobro.Image = Global.DanashaBasic.My.Resources.Resources.cobro_48
        Me.btnCobro.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCobro.Location = New System.Drawing.Point(593, 1)
        Me.btnCobro.Margin = New System.Windows.Forms.Padding(1)
        Me.btnCobro.Name = "btnCobro"
        Me.btnCobro.Size = New System.Drawing.Size(66, 68)
        Me.btnCobro.TabIndex = 25
        Me.btnCobro.Text = "Cobro"
        Me.btnCobro.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.btnCobro, "Estado de cuenta (deudas)")
        Me.btnCobro.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(23, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 17)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "Cliente buscado:"
        '
        'txtCliente_Select
        '
        Me.txtCliente_Select.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCliente_Select.Location = New System.Drawing.Point(149, 16)
        Me.txtCliente_Select.Name = "txtCliente_Select"
        Me.txtCliente_Select.Size = New System.Drawing.Size(331, 24)
        Me.txtCliente_Select.TabIndex = 19
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblTotal.Location = New System.Drawing.Point(12, 12)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(69, 20)
        Me.lblTotal.TabIndex = 31
        Me.lblTotal.Text = "Total: 0"
        '
        'lblnoExiste
        '
        Me.lblnoExiste.AutoSize = True
        Me.lblnoExiste.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblnoExiste.ForeColor = System.Drawing.Color.Red
        Me.lblnoExiste.Location = New System.Drawing.Point(12, 143)
        Me.lblnoExiste.Name = "lblnoExiste"
        Me.lblnoExiste.Size = New System.Drawing.Size(183, 20)
        Me.lblnoExiste.TabIndex = 32
        Me.lblnoExiste.Text = "No Existe información"
        Me.lblnoExiste.Visible = False
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Text = "NotifyIcon1"
        Me.NotifyIcon1.Visible = True
        '
        'Cancel_Button
        '
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Dock = System.Windows.Forms.DockStyle.Right
        Me.Cancel_Button.Location = New System.Drawing.Point(865, 0)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(83, 42)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancelar"
        '
        'OK_Button
        '
        Me.OK_Button.Dock = System.Windows.Forms.DockStyle.Right
        Me.OK_Button.Location = New System.Drawing.Point(789, 0)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(76, 42)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "Aceptar"
        '
        'lblformaPago
        '
        Me.lblformaPago.AutoSize = True
        Me.lblformaPago.BackColor = System.Drawing.Color.SpringGreen
        Me.lblformaPago.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblformaPago.Location = New System.Drawing.Point(605, 292)
        Me.lblformaPago.Name = "lblformaPago"
        Me.lblformaPago.Size = New System.Drawing.Size(0, 20)
        Me.lblformaPago.TabIndex = 32
        Me.lblformaPago.Visible = False
        '
        'FlowMenu
        '
        Me.FlowMenu.BackColor = System.Drawing.SystemColors.HotTrack
        Me.FlowMenu.Controls.Add(Me.Label5)
        Me.FlowMenu.Controls.Add(Me.btnNuevo)
        Me.FlowMenu.Controls.Add(Me.btnEditarCliente)
        Me.FlowMenu.Controls.Add(Me.btnEliminarCliente)
        Me.FlowMenu.Controls.Add(Me.Label4)
        Me.FlowMenu.Controls.Add(Me.btnDetail)
        Me.FlowMenu.Controls.Add(Me.btnCredit)
        Me.FlowMenu.Controls.Add(Me.btnCobro)
        Me.FlowMenu.Dock = System.Windows.Forms.DockStyle.Top
        Me.FlowMenu.Location = New System.Drawing.Point(0, 0)
        Me.FlowMenu.Margin = New System.Windows.Forms.Padding(1)
        Me.FlowMenu.Name = "FlowMenu"
        Me.FlowMenu.Size = New System.Drawing.Size(948, 72)
        Me.FlowMenu.TabIndex = 38
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(3, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(220, 29)
        Me.Label5.TabIndex = 31
        Me.Label5.Text = "Listado de clientes."
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Location = New System.Drawing.Point(433, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(20, 70)
        Me.Label4.TabIndex = 30
        '
        'btnDetail
        '
        Me.btnDetail.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDetail.BackColor = System.Drawing.Color.White
        Me.btnDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDetail.ForeColor = System.Drawing.Color.Black
        Me.btnDetail.Image = Global.DanashaBasic.My.Resources.Resources.detail_user_48
        Me.btnDetail.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnDetail.Location = New System.Drawing.Point(457, 1)
        Me.btnDetail.Margin = New System.Windows.Forms.Padding(1)
        Me.btnDetail.Name = "btnDetail"
        Me.btnDetail.Size = New System.Drawing.Size(66, 68)
        Me.btnDetail.TabIndex = 25
        Me.btnDetail.Text = "Detalle"
        Me.btnDetail.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnDetail.UseVisualStyleBackColor = False
        '
        'PaneBusca
        '
        Me.PaneBusca.BackColor = System.Drawing.Color.AliceBlue
        Me.PaneBusca.Controls.Add(Me.btnBuscar)
        Me.PaneBusca.Controls.Add(Me.txtCliente_Select)
        Me.PaneBusca.Controls.Add(Me.Label1)
        Me.PaneBusca.Dock = System.Windows.Forms.DockStyle.Top
        Me.PaneBusca.Location = New System.Drawing.Point(0, 72)
        Me.PaneBusca.Name = "PaneBusca"
        Me.PaneBusca.Size = New System.Drawing.Size(948, 52)
        Me.PaneBusca.TabIndex = 39
        '
        'PanePie
        '
        Me.PanePie.Controls.Add(Me.OK_Button)
        Me.PanePie.Controls.Add(Me.Cancel_Button)
        Me.PanePie.Controls.Add(Me.lblTotal)
        Me.PanePie.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanePie.Location = New System.Drawing.Point(0, 440)
        Me.PanePie.Name = "PanePie"
        Me.PanePie.Size = New System.Drawing.Size(948, 42)
        Me.PanePie.TabIndex = 40
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'frmList_clientes
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.HighlightText
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(948, 482)
        Me.Controls.Add(Me.lblnoExiste)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.PaneBusca)
        Me.Controls.Add(Me.FlowMenu)
        Me.Controls.Add(Me.lblformaPago)
        Me.Controls.Add(Me.PanePie)
        Me.Name = "frmList_clientes"
        Me.Text = "Listado de Clientes"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlowMenu.ResumeLayout(False)
        Me.FlowMenu.PerformLayout()
        Me.PaneBusca.ResumeLayout(False)
        Me.PaneBusca.PerformLayout()
        Me.PanePie.ResumeLayout(False)
        Me.PanePie.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnNuevo As System.Windows.Forms.Button
    Friend WithEvents btnEditarCliente As System.Windows.Forms.Button
    Friend WithEvents btnEliminarCliente As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCliente_Select As System.Windows.Forms.TextBox
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents lblnoExiste As System.Windows.Forms.Label
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents lblformaPago As System.Windows.Forms.Label
    Friend WithEvents FlowMenu As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents PaneBusca As System.Windows.Forms.Panel
    Friend WithEvents PanePie As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnDetail As System.Windows.Forms.Button
    Friend WithEvents btnCredit As System.Windows.Forms.Button
    Friend WithEvents btnCobro As System.Windows.Forms.Button
    Friend WithEvents Label5 As Label
    Friend WithEvents ErrorProvider1 As ErrorProvider
End Class
