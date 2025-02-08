
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmList_DeudaClientes
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
        Me.PieTableLayout = New System.Windows.Forms.TableLayoutPanel()
        Me._CancelButton = New System.Windows.Forms.Button()
        Me.PaneRigh = New System.Windows.Forms.Panel()
        Me.PaneBusqueda = New System.Windows.Forms.Panel()
        Me.TabControlBusqueda = New System.Windows.Forms.TabControl()
        Me.PageXCliente = New System.Windows.Forms.TabPage()
        Me.todosClientButton = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.NomApelliTextBox = New System.Windows.Forms.TextBox()
        Me.BuscNomApelliButton = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel()
        Me.ResumenFlowLayout = New System.Windows.Forms.FlowLayoutPanel()
        Me.totalLabel = New System.Windows.Forms.Label()
        Me.CobrarLinkLabel = New System.Windows.Forms.LinkLabel()
        Me.PaneLeft = New System.Windows.Forms.Panel()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.panelView = New System.Windows.Forms.Panel()
        Me.PieTableLayout.SuspendLayout()
        Me.PaneBusqueda.SuspendLayout()
        Me.TabControlBusqueda.SuspendLayout()
        Me.PageXCliente.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.ResumenFlowLayout.SuspendLayout()
        Me.SuspendLayout()
        '
        'PieTableLayout
        '
        Me.PieTableLayout.BackColor = System.Drawing.SystemColors.Control
        Me.PieTableLayout.ColumnCount = 3
        Me.PieTableLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 89.95381!))
        Me.PieTableLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.04619!))
        Me.PieTableLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82.0!))
        Me.PieTableLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.PieTableLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.PieTableLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.PieTableLayout.Controls.Add(Me._CancelButton, 2, 0)
        Me.PieTableLayout.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PieTableLayout.Location = New System.Drawing.Point(0, 449)
        Me.PieTableLayout.Name = "PieTableLayout"
        Me.PieTableLayout.RowCount = 1
        Me.PieTableLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.PieTableLayout.Size = New System.Drawing.Size(1134, 41)
        Me.PieTableLayout.TabIndex = 1
        '
        '_CancelButton
        '
        Me._CancelButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me._CancelButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me._CancelButton.ForeColor = System.Drawing.Color.White
        Me._CancelButton.Location = New System.Drawing.Point(1054, 3)
        Me._CancelButton.Name = "_CancelButton"
        Me._CancelButton.Size = New System.Drawing.Size(77, 35)
        Me._CancelButton.TabIndex = 0
        Me._CancelButton.Text = "Cancelar"
        Me._CancelButton.UseVisualStyleBackColor = False
        '
        'PaneRigh
        '
        Me.PaneRigh.Dock = System.Windows.Forms.DockStyle.Right
        Me.PaneRigh.Location = New System.Drawing.Point(1111, 101)
        Me.PaneRigh.Name = "PaneRigh"
        Me.PaneRigh.Size = New System.Drawing.Size(23, 348)
        Me.PaneRigh.TabIndex = 2
        '
        'PaneBusqueda
        '
        Me.PaneBusqueda.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.PaneBusqueda.Controls.Add(Me.TabControlBusqueda)
        Me.PaneBusqueda.Controls.Add(Me.Label1)
        Me.PaneBusqueda.Dock = System.Windows.Forms.DockStyle.Top
        Me.PaneBusqueda.Location = New System.Drawing.Point(0, 0)
        Me.PaneBusqueda.Name = "PaneBusqueda"
        Me.PaneBusqueda.Size = New System.Drawing.Size(1134, 101)
        Me.PaneBusqueda.TabIndex = 5
        '
        'TabControlBusqueda
        '
        Me.TabControlBusqueda.Controls.Add(Me.PageXCliente)
        Me.TabControlBusqueda.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlBusqueda.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControlBusqueda.Location = New System.Drawing.Point(0, 23)
        Me.TabControlBusqueda.Name = "TabControlBusqueda"
        Me.TabControlBusqueda.SelectedIndex = 0
        Me.TabControlBusqueda.Size = New System.Drawing.Size(1134, 78)
        Me.TabControlBusqueda.TabIndex = 0
        '
        'PageXCliente
        '
        Me.PageXCliente.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.PageXCliente.Controls.Add(Me.todosClientButton)
        Me.PageXCliente.Controls.Add(Me.Panel1)
        Me.PageXCliente.Controls.Add(Me.Label2)
        Me.PageXCliente.Location = New System.Drawing.Point(4, 29)
        Me.PageXCliente.Name = "PageXCliente"
        Me.PageXCliente.Padding = New System.Windows.Forms.Padding(3)
        Me.PageXCliente.Size = New System.Drawing.Size(1126, 45)
        Me.PageXCliente.TabIndex = 0
        Me.PageXCliente.Text = "  Por cliente.."
        '
        'todosClientButton
        '
        Me.todosClientButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.todosClientButton.Dock = System.Windows.Forms.DockStyle.Right
        Me.todosClientButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.todosClientButton.Image =  Global.DanashaBasicSignature.My.Resources.Resources.fin_deudor_32
        Me.todosClientButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.todosClientButton.Location = New System.Drawing.Point(958, 3)
        Me.todosClientButton.Name = "todosClientButton"
        Me.todosClientButton.Size = New System.Drawing.Size(165, 39)
        Me.todosClientButton.TabIndex = 7
        Me.todosClientButton.Text = "Todos los deudores"
        Me.todosClientButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.todosClientButton, "Todos los clientes")
        Me.todosClientButton.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.NomApelliTextBox)
        Me.Panel1.Controls.Add(Me.BuscNomApelliButton)
        Me.Panel1.Location = New System.Drawing.Point(191, 8)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(331, 26)
        Me.Panel1.TabIndex = 5
        '
        'NomApelliTextBox
        '
        Me.NomApelliTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NomApelliTextBox.Location = New System.Drawing.Point(0, 0)
        Me.NomApelliTextBox.Name = "NomApelliTextBox"
        Me.NomApelliTextBox.Size = New System.Drawing.Size(293, 26)
        Me.NomApelliTextBox.TabIndex = 1
        '
        'BuscNomApelliButton
        '
        Me.BuscNomApelliButton.BackgroundImage =  Global.DanashaBasicSignature.My.Resources.Resources.zoom_icon_24
        Me.BuscNomApelliButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.BuscNomApelliButton.Dock = System.Windows.Forms.DockStyle.Right
        Me.BuscNomApelliButton.Enabled = False
        Me.BuscNomApelliButton.Location = New System.Drawing.Point(293, 0)
        Me.BuscNomApelliButton.Name = "BuscNomApelliButton"
        Me.BuscNomApelliButton.Size = New System.Drawing.Size(38, 26)
        Me.BuscNomApelliButton.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.BuscNomApelliButton, "Buscar")
        Me.BuscNomApelliButton.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(179, 20)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Apellidos (y/o) Nombres:"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1134, 23)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "OPCIONES DE BUSQUEDA"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FlowLayoutPanel2
        '
        Me.FlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel2.Location = New System.Drawing.Point(11, 3)
        Me.FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        Me.FlowLayoutPanel2.Size = New System.Drawing.Size(0, 0)
        Me.FlowLayoutPanel2.TabIndex = 5
        '
        'ResumenFlowLayout
        '
        Me.ResumenFlowLayout.Controls.Add(Me.totalLabel)
        Me.ResumenFlowLayout.Controls.Add(Me.CobrarLinkLabel)
        Me.ResumenFlowLayout.Controls.Add(Me.FlowLayoutPanel2)
        Me.ResumenFlowLayout.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ResumenFlowLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.ResumenFlowLayout.Location = New System.Drawing.Point(37, 398)
        Me.ResumenFlowLayout.Name = "ResumenFlowLayout"
        Me.ResumenFlowLayout.Size = New System.Drawing.Size(1074, 51)
        Me.ResumenFlowLayout.TabIndex = 4
        '
        'totalLabel
        '
        Me.totalLabel.AutoSize = True
        Me.totalLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.totalLabel.Location = New System.Drawing.Point(4, 10)
        Me.totalLabel.Margin = New System.Windows.Forms.Padding(4, 10, 4, 2)
        Me.totalLabel.Name = "totalLabel"
        Me.totalLabel.Size = New System.Drawing.Size(0, 20)
        Me.totalLabel.TabIndex = 22
        '
        'CobrarLinkLabel
        '
        Me.CobrarLinkLabel.AutoSize = True
        Me.CobrarLinkLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CobrarLinkLabel.Location = New System.Drawing.Point(4, 32)
        Me.CobrarLinkLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.CobrarLinkLabel.Name = "CobrarLinkLabel"
        Me.CobrarLinkLabel.Size = New System.Drawing.Size(0, 17)
        Me.CobrarLinkLabel.TabIndex = 21
        '
        'PaneLeft
        '
        Me.PaneLeft.Dock = System.Windows.Forms.DockStyle.Left
        Me.PaneLeft.Location = New System.Drawing.Point(0, 101)
        Me.PaneLeft.Name = "PaneLeft"
        Me.PaneLeft.Size = New System.Drawing.Size(37, 348)
        Me.PaneLeft.TabIndex = 6
        '
        'panelView
        '
        Me.panelView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelView.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.panelView.Location = New System.Drawing.Point(37, 101)
        Me.panelView.Name = "panelView"
        Me.panelView.Size = New System.Drawing.Size(1074, 297)
        Me.panelView.TabIndex = 8
        '
        'frmList_DeudaClientes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1134, 490)
        Me.Controls.Add(Me.panelView)
        Me.Controls.Add(Me.ResumenFlowLayout)
        Me.Controls.Add(Me.PaneRigh)
        Me.Controls.Add(Me.PaneLeft)
        Me.Controls.Add(Me.PieTableLayout)
        Me.Controls.Add(Me.PaneBusqueda)
        Me.Name = "frmList_DeudaClientes"
        Me.Text = "Listado de deudores.."
        Me.PieTableLayout.ResumeLayout(False)
        Me.PaneBusqueda.ResumeLayout(False)
        Me.TabControlBusqueda.ResumeLayout(False)
        Me.PageXCliente.ResumeLayout(False)
        Me.PageXCliente.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumenFlowLayout.ResumeLayout(False)
        Me.ResumenFlowLayout.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PieTableLayout As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents PaneRigh As System.Windows.Forms.Panel
    Friend WithEvents PaneBusqueda As System.Windows.Forms.Panel
    Friend WithEvents TabControlBusqueda As System.Windows.Forms.TabControl
    Friend WithEvents PageXCliente As System.Windows.Forms.TabPage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BuscNomApelliButton As System.Windows.Forms.Button
    Friend WithEvents NomApelliTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents FlowLayoutPanel2 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents ResumenFlowLayout As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents todosClientButton As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents PaneLeft As System.Windows.Forms.Panel
    Friend WithEvents panelView As System.Windows.Forms.Panel
    Friend WithEvents totalLabel As System.Windows.Forms.Label
    Friend WithEvents CobrarLinkLabel As System.Windows.Forms.LinkLabel
    Friend WithEvents _CancelButton As System.Windows.Forms.Button
End Class
