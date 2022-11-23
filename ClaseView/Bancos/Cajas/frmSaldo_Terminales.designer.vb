<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSaldo_Terminales
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.dtg = New System.Windows.Forms.DataGridView()
        Me.TotalSaldoLabel = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.dtg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.dtg)
        Me.Panel1.Controls.Add(Me.TotalSaldoLabel)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(5)
        Me.Panel1.Size = New System.Drawing.Size(559, 152)
        Me.Panel1.TabIndex = 0
        '
        'dtg
        '
        Me.dtg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtg.Location = New System.Drawing.Point(5, 25)
        Me.dtg.Name = "dtg"
        Me.dtg.Size = New System.Drawing.Size(549, 122)
        Me.dtg.TabIndex = 1
        '
        'TotalSaldoLabel
        '
        Me.TotalSaldoLabel.AutoSize = True
        Me.TotalSaldoLabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.TotalSaldoLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TotalSaldoLabel.Location = New System.Drawing.Point(5, 5)
        Me.TotalSaldoLabel.Name = "TotalSaldoLabel"
        Me.TotalSaldoLabel.Size = New System.Drawing.Size(165, 20)
        Me.TotalSaldoLabel.TabIndex = 0
        Me.TotalSaldoLabel.Text = "Saldo en terminales...."
        '
        'frmSaldo_Terminales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(583, 176)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmSaldo_Terminales"
        Me.Text = "Saldo en terminales...."
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.dtg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents dtg As DataGridView
    Friend WithEvents TotalSaldoLabel As Label
End Class
