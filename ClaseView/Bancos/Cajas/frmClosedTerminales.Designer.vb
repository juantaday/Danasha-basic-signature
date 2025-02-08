<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmClosedTerminales
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.BeforeDayRadioButton = New System.Windows.Forms.RadioButton()
        Me.PrintTicketButton = New System.Windows.Forms.Button()
        Me.ByDateRadioButton = New System.Windows.Forms.RadioButton()
        Me.YesterdayRadioButton = New System.Windows.Forms.RadioButton()
        Me.NewRadioButton = New System.Windows.Forms.RadioButton()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.dtg = New System.Windows.Forms.DataGridView()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.dtg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(54, Byte), Integer), CType(CType(56, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(2, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(880, 47)
        Me.Panel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(202, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(3, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(368, 26)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Operaciones cerradas en terminales."
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(107, Byte), Integer), CType(CType(107, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(2, 49)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(880, 29)
        Me.Panel2.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(14, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(236, 17)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Ultimas 200 operaciones cerradas..."
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(126, Byte), Integer))
        Me.Panel4.Controls.Add(Me.DateTimePicker1)
        Me.Panel4.Controls.Add(Me.BeforeDayRadioButton)
        Me.Panel4.Controls.Add(Me.PrintTicketButton)
        Me.Panel4.Controls.Add(Me.ByDateRadioButton)
        Me.Panel4.Controls.Add(Me.YesterdayRadioButton)
        Me.Panel4.Controls.Add(Me.NewRadioButton)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(2, 78)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(880, 51)
        Me.Panel4.TabIndex = 2
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(447, 13)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(106, 23)
        Me.DateTimePicker1.TabIndex = 12
        Me.DateTimePicker1.Visible = False
        '
        'BeforeDayRadioButton
        '
        Me.BeforeDayRadioButton.AutoSize = True
        Me.BeforeDayRadioButton.Location = New System.Drawing.Point(199, 13)
        Me.BeforeDayRadioButton.Name = "BeforeDayRadioButton"
        Me.BeforeDayRadioButton.Size = New System.Drawing.Size(118, 21)
        Me.BeforeDayRadioButton.TabIndex = 11
        Me.BeforeDayRadioButton.Text = "Antes de ayer:"
        Me.BeforeDayRadioButton.UseVisualStyleBackColor = True
        '
        'PrintTicketButton
        '
        Me.PrintTicketButton.Image = Global.DanashaBasicSignature.My.Resources.Resources.Action_Printing_Print_16x16
        Me.PrintTicketButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.PrintTicketButton.Location = New System.Drawing.Point(568, 13)
        Me.PrintTicketButton.Name = "PrintTicketButton"
        Me.PrintTicketButton.Size = New System.Drawing.Size(81, 24)
        Me.PrintTicketButton.TabIndex = 10
        Me.PrintTicketButton.Text = "Imprimir"
        Me.PrintTicketButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.PrintTicketButton.UseVisualStyleBackColor = True
        '
        'ByDateRadioButton
        '
        Me.ByDateRadioButton.AutoSize = True
        Me.ByDateRadioButton.Location = New System.Drawing.Point(342, 13)
        Me.ByDateRadioButton.Name = "ByDateRadioButton"
        Me.ByDateRadioButton.Size = New System.Drawing.Size(87, 21)
        Me.ByDateRadioButton.TabIndex = 7
        Me.ByDateRadioButton.Text = "Por fecha"
        Me.ByDateRadioButton.UseVisualStyleBackColor = True
        '
        'YesterdayRadioButton
        '
        Me.YesterdayRadioButton.AutoSize = True
        Me.YesterdayRadioButton.Location = New System.Drawing.Point(117, 13)
        Me.YesterdayRadioButton.Name = "YesterdayRadioButton"
        Me.YesterdayRadioButton.Size = New System.Drawing.Size(59, 21)
        Me.YesterdayRadioButton.TabIndex = 8
        Me.YesterdayRadioButton.Text = "Ayer:"
        Me.YesterdayRadioButton.UseVisualStyleBackColor = True
        '
        'NewRadioButton
        '
        Me.NewRadioButton.AutoSize = True
        Me.NewRadioButton.Checked = True
        Me.NewRadioButton.Location = New System.Drawing.Point(23, 13)
        Me.NewRadioButton.Name = "NewRadioButton"
        Me.NewRadioButton.Size = New System.Drawing.Size(78, 21)
        Me.NewRadioButton.TabIndex = 9
        Me.NewRadioButton.TabStop = True
        Me.NewRadioButton.Text = "Hoy día:"
        Me.NewRadioButton.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(2, 282)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(880, 30)
        Me.Panel3.TabIndex = 3
        '
        'dtg
        '
        Me.dtg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtg.Location = New System.Drawing.Point(2, 129)
        Me.dtg.Name = "dtg"
        Me.dtg.Size = New System.Drawing.Size(880, 153)
        Me.dtg.TabIndex = 4
        '
        'frmClosedTerminales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(47, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(884, 314)
        Me.Controls.Add(Me.dtg)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmClosedTerminales"
        Me.Padding = New System.Windows.Forms.Padding(2)
        Me.Text = "Terminales cerradas.."
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.dtg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents dtg As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents BeforeDayRadioButton As RadioButton
    Friend WithEvents PrintTicketButton As Button
    Friend WithEvents ByDateRadioButton As RadioButton
    Friend WithEvents YesterdayRadioButton As RadioButton
    Friend WithEvents NewRadioButton As RadioButton
    Friend WithEvents DateTimePicker1 As DateTimePicker
End Class
