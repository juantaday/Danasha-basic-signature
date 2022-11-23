<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFacturCompProvider
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ListProviderComboBox = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TipoDocumentComboBox = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.LocalBodegaComboBox = New System.Windows.Forms.ComboBox()
        Me.FindProviderButton = New System.Windows.Forms.Button()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.FindButton = New System.Windows.Forms.Button()
        Me.PanePie = New System.Windows.Forms.Panel()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.TotalLabel = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.PaneData = New System.Windows.Forms.Panel()
        Me.panelView = New System.Windows.Forms.Panel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanePie.SuspendLayout()
        Me.PaneData.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(638, 55)
        Me.Label1.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Proveedor:"
        '
        'ListProviderComboBox
        '
        Me.ListProviderComboBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListProviderComboBox.FormattingEnabled = True
        Me.ListProviderComboBox.Location = New System.Drawing.Point(726, 48)
        Me.ListProviderComboBox.Margin = New System.Windows.Forms.Padding(5)
        Me.ListProviderComboBox.Name = "ListProviderComboBox"
        Me.ListProviderComboBox.Size = New System.Drawing.Size(287, 24)
        Me.ListProviderComboBox.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.TipoDocumentComboBox)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.LocalBodegaComboBox)
        Me.Panel1.Controls.Add(Me.FindProviderButton)
        Me.Panel1.Controls.Add(Me.LinkLabel1)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.ListProviderComboBox)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1105, 89)
        Me.Panel1.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(348, 6)
        Me.Label3.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(148, 17)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Tipos de documentos:"
        '
        'TipoDocumentComboBox
        '
        Me.TipoDocumentComboBox.FormattingEnabled = True
        Me.TipoDocumentComboBox.Location = New System.Drawing.Point(351, 29)
        Me.TipoDocumentComboBox.Margin = New System.Windows.Forms.Padding(5)
        Me.TipoDocumentComboBox.Name = "TipoDocumentComboBox"
        Me.TipoDocumentComboBox.Size = New System.Drawing.Size(250, 24)
        Me.TipoDocumentComboBox.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(635, 20)
        Me.Label2.Margin = New System.Windows.Forms.Padding(5, 0, 5, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 17)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Local o bodega.."
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage =  Global.DanashaBasic.My.Resources.Resources.Proveedor_450x270_Jpg
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(82, 80)
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'LocalBodegaComboBox
        '
        Me.LocalBodegaComboBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LocalBodegaComboBox.FormattingEnabled = True
        Me.LocalBodegaComboBox.Location = New System.Drawing.Point(759, 17)
        Me.LocalBodegaComboBox.Margin = New System.Windows.Forms.Padding(5)
        Me.LocalBodegaComboBox.Name = "LocalBodegaComboBox"
        Me.LocalBodegaComboBox.Size = New System.Drawing.Size(254, 24)
        Me.LocalBodegaComboBox.TabIndex = 8
        '
        'FindProviderButton
        '
        Me.FindProviderButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FindProviderButton.BackgroundImage =  Global.DanashaBasic.My.Resources.Resources.Proveedorsicon
        Me.FindProviderButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.FindProviderButton.Location = New System.Drawing.Point(1021, 29)
        Me.FindProviderButton.Name = "FindProviderButton"
        Me.FindProviderButton.Size = New System.Drawing.Size(47, 48)
        Me.FindProviderButton.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.FindProviderButton, "Buscar proveedor")
        Me.FindProviderButton.UseVisualStyleBackColor = True
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.LinkLabel1.Location = New System.Drawing.Point(92, 9)
        Me.LinkLabel1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(234, 68)
        Me.LinkLabel1.TabIndex = 0
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Opciones de fecha:"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'FindButton
        '
        Me.FindButton.Image =  Global.DanashaBasic.My.Resources.Resources.zoom_icon_24
        Me.FindButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.FindButton.Location = New System.Drawing.Point(4, 4)
        Me.FindButton.Margin = New System.Windows.Forms.Padding(4)
        Me.FindButton.Name = "FindButton"
        Me.FindButton.Size = New System.Drawing.Size(96, 36)
        Me.FindButton.TabIndex = 5
        Me.FindButton.Text = "Buscar"
        Me.FindButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.FindButton, "Extraer informacion de venta..")
        Me.FindButton.UseVisualStyleBackColor = True
        '
        'PanePie
        '
        Me.PanePie.Controls.Add(Me.CloseButton)
        Me.PanePie.Controls.Add(Me.TotalLabel)
        Me.PanePie.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanePie.Location = New System.Drawing.Point(0, 391)
        Me.PanePie.Margin = New System.Windows.Forms.Padding(4)
        Me.PanePie.Name = "PanePie"
        Me.PanePie.Size = New System.Drawing.Size(1105, 34)
        Me.PanePie.TabIndex = 1
        '
        'CloseButton
        '
        Me.CloseButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.CloseButton.Dock = System.Windows.Forms.DockStyle.Right
        Me.CloseButton.ForeColor = System.Drawing.Color.White
        Me.CloseButton.Location = New System.Drawing.Point(1027, 0)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(78, 34)
        Me.CloseButton.TabIndex = 1
        Me.CloseButton.Text = "Cerrar"
        Me.CloseButton.UseVisualStyleBackColor = False
        '
        'TotalLabel
        '
        Me.TotalLabel.Dock = System.Windows.Forms.DockStyle.Left
        Me.TotalLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TotalLabel.ForeColor = System.Drawing.Color.Red
        Me.TotalLabel.Location = New System.Drawing.Point(0, 0)
        Me.TotalLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.TotalLabel.Name = "TotalLabel"
        Me.TotalLabel.Size = New System.Drawing.Size(397, 34)
        Me.TotalLabel.TabIndex = 0
        Me.TotalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PaneData
        '
        Me.PaneData.BackColor = System.Drawing.Color.White
        Me.PaneData.Controls.Add(Me.panelView)
        Me.PaneData.Controls.Add(Me.FlowLayoutPanel1)
        Me.PaneData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PaneData.Location = New System.Drawing.Point(0, 89)
        Me.PaneData.Name = "PaneData"
        Me.PaneData.Size = New System.Drawing.Size(1105, 302)
        Me.PaneData.TabIndex = 7
        '
        'panelView
        '
        Me.panelView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panelView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelView.Location = New System.Drawing.Point(0, 47)
        Me.panelView.Name = "panelView"
        Me.panelView.Size = New System.Drawing.Size(1105, 255)
        Me.panelView.TabIndex = 1
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.FlowLayoutPanel1.Controls.Add(Me.FindButton)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(1105, 47)
        Me.FlowLayoutPanel1.TabIndex = 0
        '
        'FrmFacturCompProvider
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1105, 425)
        Me.Controls.Add(Me.PaneData)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PanePie)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FrmFacturCompProvider"
        Me.Text = "Compra de productos...."
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanePie.ResumeLayout(False)
        Me.PaneData.ResumeLayout(False)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ListProviderComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents FindButton As System.Windows.Forms.Button
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents PanePie As System.Windows.Forms.Panel
    Friend WithEvents TotalLabel As System.Windows.Forms.Label
    Friend WithEvents FindProviderButton As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents PaneData As System.Windows.Forms.Panel
    Friend WithEvents panelView As System.Windows.Forms.Panel
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents CloseButton As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents LocalBodegaComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TipoDocumentComboBox As System.Windows.Forms.ComboBox
End Class
