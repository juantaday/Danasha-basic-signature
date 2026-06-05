<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMyCommerce
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
        Me.components = New System.ComponentModel.Container()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.txtNomComercial = New System.Windows.Forms.TextBox()
        Me.NoteTextBox = New System.Windows.Forms.TextBox()
        Me.representanteTextBox = New System.Windows.Forms.TextBox()
        Me.DomicilioTextBox = New System.Windows.Forms.TextBox()
        Me.DateStar = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.razonSoacialTextBox = New System.Windows.Forms.TextBox()
        Me.rucTextBox = New System.Windows.Forms.TextBox()
        Me.lemaTextBox = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.phoneTextBox = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CancelSalesCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.registerInSystemLabel = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.OptionButto = New JMControls.Controls.RJButton()
        Me.SendEmailButton = New JMControls.Controls.RJButton()
        Me.btnConnectionRemote = New JMControls.Controls.RJButton()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(595, 414)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(219, 45)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(4, 5)
        Me.OK_Button.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(100, 35)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "Aceptar"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(114, 5)
        Me.Cancel_Button.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(100, 35)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancelar"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.25107!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.74893!))
        Me.TableLayoutPanel2.Controls.Add(Me.txtNomComercial, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.NoteTextBox, 1, 8)
        Me.TableLayoutPanel2.Controls.Add(Me.representanteTextBox, 1, 7)
        Me.TableLayoutPanel2.Controls.Add(Me.DomicilioTextBox, 1, 6)
        Me.TableLayoutPanel2.Controls.Add(Me.DateStar, 1, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.razonSoacialTextBox, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.rucTextBox, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.lemaTextBox, 1, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.Label6, 0, 8)
        Me.TableLayoutPanel2.Controls.Add(Me.Label4, 0, 7)
        Me.TableLayoutPanel2.Controls.Add(Me.Label3, 0, 6)
        Me.TableLayoutPanel2.Controls.Add(Me.Label7, 0, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.Label8, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.phoneTextBox, 1, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.Label5, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.Label2, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.CancelSalesCheckBox, 1, 9)
        Me.TableLayoutPanel2.Controls.Add(Me.Label9, 0, 1)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(12, 7)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 10
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(785, 347)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'txtNomComercial
        '
        Me.txtNomComercial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtNomComercial.Location = New System.Drawing.Point(194, 39)
        Me.txtNomComercial.MaxLength = 20
        Me.txtNomComercial.Multiline = True
        Me.txtNomComercial.Name = "txtNomComercial"
        Me.txtNomComercial.Size = New System.Drawing.Size(587, 26)
        Me.txtNomComercial.TabIndex = 19
        Me.ToolTip1.SetToolTip(Me.txtNomComercial, "Nombre de la empresa que saldra impreso en los ticket...")
        '
        'NoteTextBox
        '
        Me.NoteTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NoteTextBox.Location = New System.Drawing.Point(194, 282)
        Me.NoteTextBox.Multiline = True
        Me.NoteTextBox.Name = "NoteTextBox"
        Me.NoteTextBox.Size = New System.Drawing.Size(587, 28)
        Me.NoteTextBox.TabIndex = 15
        '
        'representanteTextBox
        '
        Me.representanteTextBox.Location = New System.Drawing.Point(194, 247)
        Me.representanteTextBox.Name = "representanteTextBox"
        Me.representanteTextBox.Size = New System.Drawing.Size(336, 26)
        Me.representanteTextBox.TabIndex = 13
        '
        'DomicilioTextBox
        '
        Me.DomicilioTextBox.Location = New System.Drawing.Point(194, 212)
        Me.DomicilioTextBox.Name = "DomicilioTextBox"
        Me.DomicilioTextBox.Size = New System.Drawing.Size(514, 26)
        Me.DomicilioTextBox.TabIndex = 12
        '
        'DateStar
        '
        Me.DateStar.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateStar.Location = New System.Drawing.Point(194, 177)
        Me.DateStar.Name = "DateStar"
        Me.DateStar.ShowCheckBox = True
        Me.DateStar.Size = New System.Drawing.Size(146, 26)
        Me.DateStar.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(132, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "RAZON SOCIAL:"
        '
        'razonSoacialTextBox
        '
        Me.razonSoacialTextBox.Location = New System.Drawing.Point(194, 4)
        Me.razonSoacialTextBox.Name = "razonSoacialTextBox"
        Me.razonSoacialTextBox.Size = New System.Drawing.Size(514, 26)
        Me.razonSoacialTextBox.TabIndex = 5
        '
        'rucTextBox
        '
        Me.rucTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rucTextBox.Location = New System.Drawing.Point(194, 72)
        Me.rucTextBox.Multiline = True
        Me.rucTextBox.Name = "rucTextBox"
        Me.rucTextBox.Size = New System.Drawing.Size(587, 28)
        Me.rucTextBox.TabIndex = 6
        '
        'lemaTextBox
        '
        Me.lemaTextBox.Location = New System.Drawing.Point(194, 107)
        Me.lemaTextBox.Name = "lemaTextBox"
        Me.lemaTextBox.Size = New System.Drawing.Size(514, 26)
        Me.lemaTextBox.TabIndex = 10
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(4, 279)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 20)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Nota:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(4, 244)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(163, 20)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Representante Legal:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 209)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(147, 20)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Domicilio comercial:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(4, 174)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(139, 20)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "Fecha de aperura:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(4, 139)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 20)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "Teléfono:"
        '
        'phoneTextBox
        '
        Me.phoneTextBox.Location = New System.Drawing.Point(194, 142)
        Me.phoneTextBox.Name = "phoneTextBox"
        Me.phoneTextBox.Size = New System.Drawing.Size(378, 26)
        Me.phoneTextBox.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(4, 104)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 20)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Lema:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 20)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "RUC:"
        '
        'CancelSalesCheckBox
        '
        Me.CancelSalesCheckBox.AutoSize = True
        Me.CancelSalesCheckBox.Location = New System.Drawing.Point(194, 317)
        Me.CancelSalesCheckBox.Name = "CancelSalesCheckBox"
        Me.CancelSalesCheckBox.Size = New System.Drawing.Size(300, 24)
        Me.CancelSalesCheckBox.TabIndex = 17
        Me.CancelSalesCheckBox.Text = "Cancelar venta cuando no existe stock" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.CancelSalesCheckBox.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(4, 36)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(140, 20)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "Nombre comercial:"
        '
        'registerInSystemLabel
        '
        Me.registerInSystemLabel.AutoSize = True
        Me.registerInSystemLabel.Location = New System.Drawing.Point(8, 414)
        Me.registerInSystemLabel.Name = "registerInSystemLabel"
        Me.registerInSystemLabel.Size = New System.Drawing.Size(168, 20)
        Me.registerInSystemLabel.TabIndex = 2
        Me.registerInSystemLabel.Text = "registerInSystemLabel"
        '
        'OptionButto
        '
        Me.OptionButto.BackColor = System.Drawing.Color.MediumSlateBlue
        Me.OptionButto.BackgroundColor = System.Drawing.Color.MediumSlateBlue
        Me.OptionButto.BorderColor = System.Drawing.Color.PaleVioletRed
        Me.OptionButto.BorderRadius = 6
        Me.OptionButto.BorderSize = 1
        Me.OptionButto.FlatAppearance.BorderSize = 0
        Me.OptionButto.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OptionButto.ForeColor = System.Drawing.Color.White
        Me.OptionButto.Location = New System.Drawing.Point(185, 360)
        Me.OptionButto.Name = "OptionButto"
        Me.OptionButto.Size = New System.Drawing.Size(172, 34)
        Me.OptionButto.TabIndex = 3
        Me.OptionButto.Text = "Opciones avanzadas"
        Me.OptionButto.TextColor = System.Drawing.Color.White
        Me.OptionButto.UseVisualStyleBackColor = False
        '
        'SendEmailButton
        '
        Me.SendEmailButton.BackColor = System.Drawing.Color.ForestGreen
        Me.SendEmailButton.BackgroundColor = System.Drawing.Color.ForestGreen
        Me.SendEmailButton.BorderColor = System.Drawing.Color.PaleVioletRed
        Me.SendEmailButton.BorderRadius = 6
        Me.SendEmailButton.BorderSize = 1
        Me.SendEmailButton.FlatAppearance.BorderSize = 0
        Me.SendEmailButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.SendEmailButton.ForeColor = System.Drawing.Color.White
        Me.SendEmailButton.Location = New System.Drawing.Point(370, 360)
        Me.SendEmailButton.Name = "SendEmailButton"
        Me.SendEmailButton.Size = New System.Drawing.Size(172, 34)
        Me.SendEmailButton.TabIndex = 3
        Me.SendEmailButton.Text = "Envios de  correos"
        Me.SendEmailButton.TextColor = System.Drawing.Color.White
        Me.SendEmailButton.UseVisualStyleBackColor = False
        '
        'btnConnectionRemote
        '
        Me.btnConnectionRemote.BackColor = System.Drawing.Color.Orange
        Me.btnConnectionRemote.BackgroundColor = System.Drawing.Color.Orange
        Me.btnConnectionRemote.BorderColor = System.Drawing.Color.PaleVioletRed
        Me.btnConnectionRemote.BorderRadius = 6
        Me.btnConnectionRemote.BorderSize = 1
        Me.btnConnectionRemote.FlatAppearance.BorderSize = 0
        Me.btnConnectionRemote.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConnectionRemote.ForeColor = System.Drawing.Color.Black
        Me.btnConnectionRemote.Location = New System.Drawing.Point(565, 360)
        Me.btnConnectionRemote.Name = "btnConnectionRemote"
        Me.btnConnectionRemote.Size = New System.Drawing.Size(191, 34)
        Me.btnConnectionRemote.TabIndex = 4
        Me.btnConnectionRemote.Text = "Credenciales remotas"
        Me.btnConnectionRemote.TextColor = System.Drawing.Color.Black
        Me.btnConnectionRemote.UseVisualStyleBackColor = False
        '
        'frmMyCommerce
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(827, 465)
        Me.Controls.Add(Me.btnConnectionRemote)
        Me.Controls.Add(Me.SendEmailButton)
        Me.Controls.Add(Me.OptionButto)
        Me.Controls.Add(Me.registerInSystemLabel)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMyCommerce"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Mi negocio"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents NoteTextBox As TextBox
    Friend WithEvents representanteTextBox As TextBox
    Friend WithEvents DomicilioTextBox As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents razonSoacialTextBox As TextBox
    Friend WithEvents rucTextBox As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lemaTextBox As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents phoneTextBox As TextBox
    Friend WithEvents DateStar As DateTimePicker
    Friend WithEvents registerInSystemLabel As Label
    Friend WithEvents CancelSalesCheckBox As CheckBox
    Friend WithEvents txtNomComercial As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents OptionButto As JMControls.Controls.RJButton
    Friend WithEvents SendEmailButton As JMControls.Controls.RJButton
    Friend WithEvents btnConnectionRemote As JMControls.Controls.RJButton
End Class
