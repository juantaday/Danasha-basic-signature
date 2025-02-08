<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVentaXoperation
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.txtNumOpeartion = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.panelView = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.CopyClickBoarButton = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.olvVentas = New BrightIdeasSoftware.ObjectListView()
        Me.QuantityClm = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.PresenColumn = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.Nom_ComercialClm = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.TotalClm = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.Panel1.SuspendLayout()
        Me.panelView.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.olvVentas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Gainsboro
        Me.Panel1.Controls.Add(Me.CopyClickBoarButton)
        Me.Panel1.Controls.Add(Me.btnBuscar)
        Me.Panel1.Controls.Add(Me.txtNumOpeartion)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(760, 39)
        Me.Panel1.TabIndex = 0
        '
        'btnBuscar
        '
        Me.btnBuscar.Location = New System.Drawing.Point(344, 8)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(89, 26)
        Me.btnBuscar.TabIndex = 2
        Me.btnBuscar.Text = "Buscar"
        Me.btnBuscar.UseVisualStyleBackColor = True
        '
        'txtNumOpeartion
        '
        Me.txtNumOpeartion.Location = New System.Drawing.Point(181, 10)
        Me.txtNumOpeartion.Name = "txtNumOpeartion"
        Me.txtNumOpeartion.Size = New System.Drawing.Size(133, 23)
        Me.txtNumOpeartion.TabIndex = 1
        Me.txtNumOpeartion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(25, 12)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(149, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Número de operación:"
        '
        'panelView
        '
        Me.panelView.Controls.Add(Me.olvVentas)
        Me.panelView.Controls.Add(Me.Panel2)
        Me.panelView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelView.Location = New System.Drawing.Point(0, 39)
        Me.panelView.Name = "panelView"
        Me.panelView.Padding = New System.Windows.Forms.Padding(2, 2, 2, 10)
        Me.panelView.Size = New System.Drawing.Size(760, 284)
        Me.panelView.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(120, Byte), Integer))
        Me.Panel2.Controls.Add(Me.lblTotal)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(2, 241)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(756, 33)
        Me.Panel2.TabIndex = 1
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.ForeColor = System.Drawing.Color.White
        Me.lblTotal.Location = New System.Drawing.Point(9, 5)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(153, 20)
        Me.lblTotal.TabIndex = 0
        Me.lblTotal.Text = "Total General 0.00 $"
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'CopyClickBoarButton
        '
        Me.CopyClickBoarButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CopyClickBoarButton.Image = Global.DanashaBasicSignature.My.Resources.Resources.Action_Copy
        Me.CopyClickBoarButton.Location = New System.Drawing.Point(722, 13)
        Me.CopyClickBoarButton.Name = "CopyClickBoarButton"
        Me.CopyClickBoarButton.Size = New System.Drawing.Size(33, 24)
        Me.CopyClickBoarButton.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.CopyClickBoarButton, "Copy in click boar")
        Me.CopyClickBoarButton.UseVisualStyleBackColor = True
        '
        'olvVentas
        '
        Me.olvVentas.AllColumns.Add(Me.QuantityClm)
        Me.olvVentas.AllColumns.Add(Me.PresenColumn)
        Me.olvVentas.AllColumns.Add(Me.Nom_ComercialClm)
        Me.olvVentas.AllColumns.Add(Me.TotalClm)
        Me.olvVentas.AlternateRowBackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.olvVentas.BackColor = System.Drawing.Color.White
        Me.olvVentas.CellEditUseWholeCell = False
        Me.olvVentas.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.QuantityClm, Me.PresenColumn, Me.Nom_ComercialClm, Me.TotalClm})
        Me.olvVentas.Cursor = System.Windows.Forms.Cursors.Default
        Me.olvVentas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.olvVentas.FullRowSelect = True
        Me.olvVentas.GridLines = True
        Me.olvVentas.HideSelection = False
        Me.olvVentas.IncludeColumnHeadersInCopy = True
        Me.olvVentas.Location = New System.Drawing.Point(2, 2)
        Me.olvVentas.Name = "olvVentas"
        Me.olvVentas.ShowGroups = False
        Me.olvVentas.Size = New System.Drawing.Size(756, 239)
        Me.olvVentas.TabIndex = 0
        Me.olvVentas.UseAlternatingBackColors = True
        Me.olvVentas.UseCompatibleStateImageBehavior = False
        Me.olvVentas.View = System.Windows.Forms.View.Details
        '
        'QuantityClm
        '
        Me.QuantityClm.AspectName = "Quantity"
        Me.QuantityClm.AspectToStringFormat = "{0:N3}"
        Me.QuantityClm.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.QuantityClm.Text = "Cantidad"
        Me.QuantityClm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.QuantityClm.Width = 120
        '
        'PresenColumn
        '
        Me.PresenColumn.AspectName = "PresentacionPrint"
        Me.PresenColumn.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.PresenColumn.Text = "Presentación"
        Me.PresenColumn.Width = 120
        '
        'Nom_ComercialClm
        '
        Me.Nom_ComercialClm.AspectName = "Nom_Comercial"
        Me.Nom_ComercialClm.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.Nom_ComercialClm.Text = "Producto"
        Me.Nom_ComercialClm.Width = 300
        '
        'TotalClm
        '
        Me.TotalClm.AspectName = "Total"
        Me.TotalClm.AspectToStringFormat = "{0:C2}"
        Me.TotalClm.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TotalClm.Text = "Total venta"
        Me.TotalClm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TotalClm.Width = 150
        '
        'FrmVentaXoperation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(760, 323)
        Me.Controls.Add(Me.panelView)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "FrmVentaXoperation"
        Me.Text = "FrmVentaXoperation"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.panelView.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.olvVentas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnBuscar As Button
    Friend WithEvents txtNumOpeartion As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents panelView As Panel
    Friend WithEvents olvVentas As BrightIdeasSoftware.ObjectListView
    Friend WithEvents QuantityClm As BrightIdeasSoftware.OLVColumn
    Friend WithEvents PresenColumn As BrightIdeasSoftware.OLVColumn
    Friend WithEvents Nom_ComercialClm As BrightIdeasSoftware.OLVColumn
    Friend WithEvents TotalClm As BrightIdeasSoftware.OLVColumn
    Friend WithEvents ErrorProvider1 As ErrorProvider
    Friend WithEvents Panel2 As Panel
    Friend WithEvents lblTotal As Label
    Friend WithEvents CopyClickBoarButton As Button
    Friend WithEvents ToolTip1 As ToolTip
End Class
