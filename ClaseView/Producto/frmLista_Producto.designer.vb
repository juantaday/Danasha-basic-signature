<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLista_Producto
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLista_Producto))
        Me.menuQuienVende = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuLectura = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.menuDetalleLect = New System.Windows.Forms.ToolStripMenuItem()
        Me.nemuQuienVendeLect = New System.Windows.Forms.ToolStripMenuItem()
        Me.datalistado = New System.Windows.Forms.DataGridView()
        Me.menuEditar = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.txtProduc_Select = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.menuNuevo = New System.Windows.Forms.ToolStripMenuItem()
        Me.ContextMenuAdministra = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PanelAdmin = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.AgregarButton = New System.Windows.Forms.Button()
        Me.edirPreciSalesButton = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.ContextMenuLectura.SuspendLayout()
        CType(Me.datalistado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuAdministra.SuspendLayout()
        Me.PanelAdmin.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'menuQuienVende
        '
        Me.menuQuienVende.Name = "menuQuienVende"
        Me.menuQuienVende.Size = New System.Drawing.Size(141, 22)
        Me.menuQuienVende.Text = "Quien vende"
        '
        'ContextMenuLectura
        '
        Me.ContextMenuLectura.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuDetalleLect, Me.nemuQuienVendeLect})
        Me.ContextMenuLectura.Name = "ContextMenuAdministra"
        Me.ContextMenuLectura.Size = New System.Drawing.Size(142, 48)
        '
        'menuDetalleLect
        '
        Me.menuDetalleLect.Name = "menuDetalleLect"
        Me.menuDetalleLect.Size = New System.Drawing.Size(141, 22)
        Me.menuDetalleLect.Text = "Ver detalle..."
        '
        'nemuQuienVendeLect
        '
        Me.nemuQuienVendeLect.Name = "nemuQuienVendeLect"
        Me.nemuQuienVendeLect.Size = New System.Drawing.Size(141, 22)
        Me.nemuQuienVendeLect.Text = "Quien vende"
        '
        'datalistado
        '
        Me.datalistado.AllowUserToAddRows = False
        Me.datalistado.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.datalistado.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.datalistado.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.datalistado.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.datalistado.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.datalistado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.datalistado.GridColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.datalistado.Location = New System.Drawing.Point(7, 87)
        Me.datalistado.Name = "datalistado"
        Me.datalistado.ReadOnly = True
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.datalistado.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.datalistado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.datalistado.Size = New System.Drawing.Size(1088, 419)
        Me.datalistado.TabIndex = 8
        '
        'menuEditar
        '
        Me.menuEditar.Name = "menuEditar"
        Me.menuEditar.Size = New System.Drawing.Size(141, 22)
        Me.menuEditar.Text = "Editar"
        '
        'btnBuscar
        '
        Me.btnBuscar.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btnBuscar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuscar.ForeColor = System.Drawing.Color.White
        Me.btnBuscar.Location = New System.Drawing.Point(550, 55)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(69, 30)
        Me.btnBuscar.TabIndex = 12
        Me.btnBuscar.Text = "Buscar"
        Me.ToolTip1.SetToolTip(Me.btnBuscar, "Buscar el producto")
        Me.btnBuscar.UseVisualStyleBackColor = False
        '
        'txtProduc_Select
        '
        Me.txtProduc_Select.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtProduc_Select.Location = New System.Drawing.Point(163, 57)
        Me.txtProduc_Select.Name = "txtProduc_Select"
        Me.txtProduc_Select.Size = New System.Drawing.Size(387, 26)
        Me.txtProduc_Select.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtProduc_Select, "Escribe el produco o odigos o barra de producto a buscar")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(154, 20)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Producto buscado"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(10, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 16)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Total Listado: 0"
        '
        'menuNuevo
        '
        Me.menuNuevo.Name = "menuNuevo"
        Me.menuNuevo.Size = New System.Drawing.Size(141, 22)
        Me.menuNuevo.Text = "Nuevo"
        '
        'ContextMenuAdministra
        '
        Me.ContextMenuAdministra.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.menuNuevo, Me.menuEditar, Me.menuQuienVende})
        Me.ContextMenuAdministra.Name = "ContextMenuAdministra"
        Me.ContextMenuAdministra.Size = New System.Drawing.Size(142, 70)
        '
        'PanelAdmin
        '
        Me.PanelAdmin.BackColor = System.Drawing.Color.Silver
        Me.PanelAdmin.Controls.Add(Me.Button1)
        Me.PanelAdmin.Controls.Add(Me.AgregarButton)
        Me.PanelAdmin.Controls.Add(Me.edirPreciSalesButton)
        Me.PanelAdmin.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelAdmin.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelAdmin.Location = New System.Drawing.Point(0, 0)
        Me.PanelAdmin.Name = "PanelAdmin"
        Me.PanelAdmin.Size = New System.Drawing.Size(1098, 51)
        Me.PanelAdmin.TabIndex = 17
        '
        'Button1
        '
        Me.Button1.Image =  Global.DanashaBasicSignature.My.Resources.Resources.edid_pencil_32
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(173, 6)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(115, 38)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Modificar"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button1.UseVisualStyleBackColor = True
        '
        'AgregarButton
        '
        Me.AgregarButton.Image =  Global.DanashaBasicSignature.My.Resources.Resources.New_green_32
        Me.AgregarButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.AgregarButton.Location = New System.Drawing.Point(6, 6)
        Me.AgregarButton.Name = "AgregarButton"
        Me.AgregarButton.Size = New System.Drawing.Size(161, 38)
        Me.AgregarButton.TabIndex = 1
        Me.AgregarButton.Text = "Nuevo producto"
        Me.AgregarButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.AgregarButton.UseVisualStyleBackColor = True
        '
        'edirPreciSalesButton
        '
        Me.edirPreciSalesButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.edirPreciSalesButton.Image =  Global.DanashaBasicSignature.My.Resources.Resources.Villetes_44
        Me.edirPreciSalesButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.edirPreciSalesButton.Location = New System.Drawing.Point(294, 4)
        Me.edirPreciSalesButton.Name = "edirPreciSalesButton"
        Me.edirPreciSalesButton.Size = New System.Drawing.Size(239, 44)
        Me.edirPreciSalesButton.TabIndex = 0
        Me.edirPreciSalesButton.Text = "Modificar precio de venta"
        Me.edirPreciSalesButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.edirPreciSalesButton.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ControlDark
        Me.Panel2.Controls.Add(Me.btnCancelar)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 512)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1098, 22)
        Me.Panel2.TabIndex = 18
        '
        'btnCancelar
        '
        Me.btnCancelar.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancelar.Location = New System.Drawing.Point(1016, 0)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(82, 22)
        Me.btnCancelar.TabIndex = 14
        Me.btnCancelar.Text = "Cancel"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'frmLista_Producto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1098, 534)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.PanelAdmin)
        Me.Controls.Add(Me.datalistado)
        Me.Controls.Add(Me.btnBuscar)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtProduc_Select)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmLista_Producto"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Listado de productos."
        Me.ContextMenuLectura.ResumeLayout(False)
        CType(Me.datalistado, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuAdministra.ResumeLayout(False)
        Me.PanelAdmin.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents menuQuienVende As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuLectura As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents menuDetalleLect As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents nemuQuienVendeLect As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents datalistado As System.Windows.Forms.DataGridView
    Friend WithEvents menuEditar As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
    Friend WithEvents txtProduc_Select As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents menuNuevo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuAdministra As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents PanelAdmin As Panel
    Friend WithEvents edirPreciSalesButton As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents AgregarButton As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents btnCancelar As Button
End Class
