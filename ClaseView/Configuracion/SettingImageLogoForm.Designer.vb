<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettingImageLogoForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SettingImageLogoForm))
        Me.backgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.PasswordTextBox = New System.Windows.Forms.TextBox()
        Me.EmailTextBox = New System.Windows.Forms.TextBox()
        Me.ServidorTextBox = New System.Windows.Forms.TextBox()
        Me.tabPage2 = New System.Windows.Forms.TabPage()
        Me.PuertoTextBox = New System.Windows.Forms.TextBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label4 = New System.Windows.Forms.Label()
        Me.label3 = New System.Windows.Forms.Label()
        Me.label5 = New System.Windows.Forms.Label()
        Me.panel2 = New System.Windows.Forms.Panel()
        Me.label1 = New System.Windows.Forms.Label()
        Me.button1 = New System.Windows.Forms.Button()
        Me.tabPage1 = New System.Windows.Forms.TabPage()
        Me.DeleteImageButton = New System.Windows.Forms.Button()
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.circularProgressBar1 = New CircularProgressBar.CircularProgressBar()
        Me.txtCellPhone = New System.Windows.Forms.TextBox()
        Me.label8 = New System.Windows.Forms.Label()
        Me.txtPhone = New System.Windows.Forms.TextBox()
        Me.label7 = New System.Windows.Forms.Label()
        Me.txtCompanyName = New System.Windows.Forms.TextBox()
        Me.label6 = New System.Windows.Forms.Label()
        Me.tabPage3 = New System.Windows.Forms.TabPage()
        Me.PanelViewEmail = New System.Windows.Forms.Panel()
        Me.tabControl1 = New System.Windows.Forms.TabControl()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.button_Accep = New System.Windows.Forms.Button()
        Me.button_Cancel = New System.Windows.Forms.Button()
        Me.tabPage2.SuspendLayout()
        Me.panel2.SuspendLayout()
        Me.tabPage1.SuspendLayout()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPage3.SuspendLayout()
        Me.PanelViewEmail.SuspendLayout()
        Me.tabControl1.SuspendLayout()
        Me.panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'backgroundWorker1
        '
        '
        'PasswordTextBox
        '
        Me.PasswordTextBox.Location = New System.Drawing.Point(120, 56)
        Me.PasswordTextBox.Name = "PasswordTextBox"
        Me.PasswordTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.PasswordTextBox.Size = New System.Drawing.Size(256, 23)
        Me.PasswordTextBox.TabIndex = 9
        Me.toolTip1.SetToolTip(Me.PasswordTextBox, "Contraseña del correo.")
        '
        'EmailTextBox
        '
        Me.EmailTextBox.Location = New System.Drawing.Point(120, 25)
        Me.EmailTextBox.Name = "EmailTextBox"
        Me.EmailTextBox.Size = New System.Drawing.Size(256, 23)
        Me.EmailTextBox.TabIndex = 7
        Me.toolTip1.SetToolTip(Me.EmailTextBox, "Correo de donde se va eminti facturas electronicas..")
        '
        'ServidorTextBox
        '
        Me.ServidorTextBox.Location = New System.Drawing.Point(150, 130)
        Me.ServidorTextBox.Name = "ServidorTextBox"
        Me.ServidorTextBox.Size = New System.Drawing.Size(226, 23)
        Me.ServidorTextBox.TabIndex = 11
        Me.ServidorTextBox.Text = "smtp.gmail.com"
        Me.toolTip1.SetToolTip(Me.ServidorTextBox, resources.GetString("ServidorTextBox.ToolTip"))
        '
        'tabPage2
        '
        Me.tabPage2.Controls.Add(Me.PasswordTextBox)
        Me.tabPage2.Controls.Add(Me.PuertoTextBox)
        Me.tabPage2.Controls.Add(Me.label2)
        Me.tabPage2.Controls.Add(Me.label4)
        Me.tabPage2.Controls.Add(Me.EmailTextBox)
        Me.tabPage2.Controls.Add(Me.ServidorTextBox)
        Me.tabPage2.Controls.Add(Me.label3)
        Me.tabPage2.Controls.Add(Me.label5)
        Me.tabPage2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabPage2.Location = New System.Drawing.Point(4, 25)
        Me.tabPage2.Name = "tabPage2"
        Me.tabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPage2.Size = New System.Drawing.Size(678, 232)
        Me.tabPage2.TabIndex = 1
        Me.tabPage2.Text = "    Avanzado...."
        Me.tabPage2.UseVisualStyleBackColor = True
        '
        'PuertoTextBox
        '
        Me.PuertoTextBox.Location = New System.Drawing.Point(150, 159)
        Me.PuertoTextBox.Name = "PuertoTextBox"
        Me.PuertoTextBox.Size = New System.Drawing.Size(130, 23)
        Me.PuertoTextBox.TabIndex = 13
        Me.PuertoTextBox.Text = "587"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(41, 25)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(51, 17)
        Me.label2.TabIndex = 6
        Me.label2.Text = "E-mail:"
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(37, 159)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(54, 17)
        Me.label4.TabIndex = 12
        Me.label4.Text = "Puerto:"
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(41, 56)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(73, 17)
        Me.label3.TabIndex = 8
        Me.label3.Text = "Password:"
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.Location = New System.Drawing.Point(37, 130)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(111, 17)
        Me.label5.TabIndex = 10
        Me.label5.Text = "Servidor SMTP: "
        '
        'panel2
        '
        Me.panel2.Controls.Add(Me.label1)
        Me.panel2.Controls.Add(Me.button1)
        Me.panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel2.Location = New System.Drawing.Point(3, 3)
        Me.panel2.Name = "panel2"
        Me.panel2.Size = New System.Drawing.Size(672, 43)
        Me.panel2.TabIndex = 6
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label1.Location = New System.Drawing.Point(3, 5)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(171, 20)
        Me.label1.TabIndex = 0
        Me.label1.Text = "Configuración de logo.."
        '
        'button1
        '
        Me.button1.FlatAppearance.BorderColor = System.Drawing.Color.Navy
        Me.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Blue
        Me.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.button1.Location = New System.Drawing.Point(196, 3)
        Me.button1.Name = "button1"
        Me.button1.Size = New System.Drawing.Size(148, 35)
        Me.button1.TabIndex = 1
        Me.button1.Text = "Ruta del archivo.."
        Me.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.button1.UseVisualStyleBackColor = True
        '
        'tabPage1
        '
        Me.tabPage1.Controls.Add(Me.DeleteImageButton)
        Me.tabPage1.Controls.Add(Me.pictureBox1)
        Me.tabPage1.Controls.Add(Me.panel2)
        Me.tabPage1.Location = New System.Drawing.Point(4, 25)
        Me.tabPage1.Name = "tabPage1"
        Me.tabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPage1.Size = New System.Drawing.Size(678, 232)
        Me.tabPage1.TabIndex = 0
        Me.tabPage1.Text = "      Logo...."
        Me.tabPage1.UseVisualStyleBackColor = True
        '
        'DeleteImageButton
        '
        Me.DeleteImageButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DeleteImageButton.BackColor = System.Drawing.Color.White
        Me.DeleteImageButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DeleteImageButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray
        Me.DeleteImageButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.DeleteImageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.DeleteImageButton.ForeColor = System.Drawing.Color.Red
        Me.DeleteImageButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DeleteImageButton.Location = New System.Drawing.Point(522, 50)
        Me.DeleteImageButton.Name = "DeleteImageButton"
        Me.DeleteImageButton.Size = New System.Drawing.Size(78, 28)
        Me.DeleteImageButton.TabIndex = 5
        Me.DeleteImageButton.Text = "Eliminar"
        Me.DeleteImageButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.DeleteImageButton.UseVisualStyleBackColor = False
        Me.DeleteImageButton.Visible = False
        '
        'pictureBox1
        '
        Me.pictureBox1.BackColor = System.Drawing.Color.White
        Me.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pictureBox1.Location = New System.Drawing.Point(3, 46)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Padding = New System.Windows.Forms.Padding(10, 40, 10, 10)
        Me.pictureBox1.Size = New System.Drawing.Size(672, 183)
        Me.pictureBox1.TabIndex = 2
        Me.pictureBox1.TabStop = False
        '
        'circularProgressBar1
        '
        Me.circularProgressBar1.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner
        Me.circularProgressBar1.AnimationSpeed = 500
        Me.circularProgressBar1.BackColor = System.Drawing.Color.Transparent
        Me.circularProgressBar1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.circularProgressBar1.ForeColor = System.Drawing.Color.White
        Me.circularProgressBar1.InnerColor = System.Drawing.Color.Transparent
        Me.circularProgressBar1.InnerMargin = 2
        Me.circularProgressBar1.InnerWidth = -1
        Me.circularProgressBar1.Location = New System.Drawing.Point(19, 63)
        Me.circularProgressBar1.MarqueeAnimationSpeed = 2000
        Me.circularProgressBar1.Name = "circularProgressBar1"
        Me.circularProgressBar1.OuterColor = System.Drawing.Color.Gray
        Me.circularProgressBar1.OuterMargin = -25
        Me.circularProgressBar1.OuterWidth = 26
        Me.circularProgressBar1.ProgressColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.circularProgressBar1.ProgressWidth = 15
        Me.circularProgressBar1.SecondaryFont = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.circularProgressBar1.Size = New System.Drawing.Size(127, 127)
        Me.circularProgressBar1.StartAngle = 270
        Me.circularProgressBar1.SubscriptColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.circularProgressBar1.SubscriptMargin = New System.Windows.Forms.Padding(10, -35, 0, 0)
        Me.circularProgressBar1.SubscriptText = ""
        Me.circularProgressBar1.SuperscriptColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.circularProgressBar1.SuperscriptMargin = New System.Windows.Forms.Padding(10, 35, 0, 0)
        Me.circularProgressBar1.SuperscriptText = ""
        Me.circularProgressBar1.TabIndex = 17
        Me.circularProgressBar1.Text = "Reading"
        Me.circularProgressBar1.TextMargin = New System.Windows.Forms.Padding(0)
        Me.circularProgressBar1.Value = 25
        Me.circularProgressBar1.Visible = False
        '
        'txtCellPhone
        '
        Me.txtCellPhone.Location = New System.Drawing.Point(188, 116)
        Me.txtCellPhone.MaxLength = 20
        Me.txtCellPhone.Name = "txtCellPhone"
        Me.txtCellPhone.Size = New System.Drawing.Size(219, 23)
        Me.txtCellPhone.TabIndex = 1
        '
        'label8
        '
        Me.label8.AutoSize = True
        Me.label8.Location = New System.Drawing.Point(126, 118)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(56, 17)
        Me.label8.TabIndex = 0
        Me.label8.Text = "Celular:"
        '
        'txtPhone
        '
        Me.txtPhone.Location = New System.Drawing.Point(188, 72)
        Me.txtPhone.MaxLength = 20
        Me.txtPhone.Name = "txtPhone"
        Me.txtPhone.Size = New System.Drawing.Size(219, 23)
        Me.txtPhone.TabIndex = 1
        '
        'label7
        '
        Me.label7.AutoSize = True
        Me.label7.Location = New System.Drawing.Point(114, 74)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(68, 17)
        Me.label7.TabIndex = 0
        Me.label7.Text = "Teléfono:"
        '
        'txtCompanyName
        '
        Me.txtCompanyName.Location = New System.Drawing.Point(188, 28)
        Me.txtCompanyName.MaxLength = 30
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.Size = New System.Drawing.Size(348, 23)
        Me.txtCompanyName.TabIndex = 1
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Location = New System.Drawing.Point(39, 30)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(143, 17)
        Me.label6.TabIndex = 0
        Me.label6.Text = "Nombre de companía"
        '
        'tabPage3
        '
        Me.tabPage3.Controls.Add(Me.circularProgressBar1)
        Me.tabPage3.Controls.Add(Me.txtCellPhone)
        Me.tabPage3.Controls.Add(Me.label8)
        Me.tabPage3.Controls.Add(Me.txtPhone)
        Me.tabPage3.Controls.Add(Me.label7)
        Me.tabPage3.Controls.Add(Me.txtCompanyName)
        Me.tabPage3.Controls.Add(Me.label6)
        Me.tabPage3.Location = New System.Drawing.Point(4, 25)
        Me.tabPage3.Name = "tabPage3"
        Me.tabPage3.Size = New System.Drawing.Size(678, 232)
        Me.tabPage3.TabIndex = 2
        Me.tabPage3.Text = "General"
        Me.tabPage3.UseVisualStyleBackColor = True
        '
        'PanelViewEmail
        '
        Me.PanelViewEmail.Controls.Add(Me.tabControl1)
        Me.PanelViewEmail.Controls.Add(Me.panel1)
        Me.PanelViewEmail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelViewEmail.ForeColor = System.Drawing.Color.Black
        Me.PanelViewEmail.Location = New System.Drawing.Point(0, 0)
        Me.PanelViewEmail.Name = "PanelViewEmail"
        Me.PanelViewEmail.Size = New System.Drawing.Size(686, 294)
        Me.PanelViewEmail.TabIndex = 17
        '
        'tabControl1
        '
        Me.tabControl1.Controls.Add(Me.tabPage3)
        Me.tabControl1.Controls.Add(Me.tabPage1)
        Me.tabControl1.Controls.Add(Me.tabPage2)
        Me.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabControl1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tabControl1.Location = New System.Drawing.Point(0, 0)
        Me.tabControl1.Name = "tabControl1"
        Me.tabControl1.SelectedIndex = 0
        Me.tabControl1.Size = New System.Drawing.Size(686, 261)
        Me.tabControl1.TabIndex = 14
        '
        'panel1
        '
        Me.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.panel1.Controls.Add(Me.button_Accep)
        Me.panel1.Controls.Add(Me.button_Cancel)
        Me.panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panel1.Location = New System.Drawing.Point(0, 261)
        Me.panel1.Name = "panel1"
        Me.panel1.Padding = New System.Windows.Forms.Padding(2)
        Me.panel1.Size = New System.Drawing.Size(686, 33)
        Me.panel1.TabIndex = 15
        '
        'button_Accep
        '
        Me.button_Accep.Dock = System.Windows.Forms.DockStyle.Right
        Me.button_Accep.Location = New System.Drawing.Point(494, 2)
        Me.button_Accep.Name = "button_Accep"
        Me.button_Accep.Size = New System.Drawing.Size(102, 27)
        Me.button_Accep.TabIndex = 4
        Me.button_Accep.Text = "Guardar.."
        Me.button_Accep.UseVisualStyleBackColor = True
        '
        'button_Cancel
        '
        Me.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.button_Cancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.button_Cancel.Location = New System.Drawing.Point(596, 2)
        Me.button_Cancel.Name = "button_Cancel"
        Me.button_Cancel.Size = New System.Drawing.Size(86, 27)
        Me.button_Cancel.TabIndex = 3
        Me.button_Cancel.Text = "Cancelar.."
        Me.button_Cancel.UseVisualStyleBackColor = True
        '
        'SettingImageLogoForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(686, 294)
        Me.Controls.Add(Me.PanelViewEmail)
        Me.Name = "SettingImageLogoForm"
        Me.Text = "SettingImageLogoForm"
        Me.tabPage2.ResumeLayout(False)
        Me.tabPage2.PerformLayout()
        Me.panel2.ResumeLayout(False)
        Me.panel2.PerformLayout()
        Me.tabPage1.ResumeLayout(False)
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPage3.ResumeLayout(False)
        Me.tabPage3.PerformLayout()
        Me.PanelViewEmail.ResumeLayout(False)
        Me.tabControl1.ResumeLayout(False)
        Me.panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents backgroundWorker1 As System.ComponentModel.BackgroundWorker
    Private WithEvents toolTip1 As ToolTip
    Private WithEvents PasswordTextBox As TextBox
    Private WithEvents EmailTextBox As TextBox
    Private WithEvents ServidorTextBox As TextBox
    Private WithEvents tabPage2 As TabPage
    Private WithEvents PuertoTextBox As TextBox
    Private WithEvents label2 As Label
    Private WithEvents label4 As Label
    Private WithEvents label3 As Label
    Private WithEvents label5 As Label
    Private WithEvents panel2 As Panel
    Private WithEvents label1 As Label
    Private WithEvents button1 As Button
    Private WithEvents tabPage1 As TabPage
    Private WithEvents DeleteImageButton As Button
    Private WithEvents pictureBox1 As PictureBox
    Private WithEvents circularProgressBar1 As CircularProgressBar.CircularProgressBar
    Private WithEvents txtCellPhone As TextBox
    Private WithEvents label8 As Label
    Private WithEvents txtPhone As TextBox
    Private WithEvents label7 As Label
    Private WithEvents txtCompanyName As TextBox
    Private WithEvents label6 As Label
    Private WithEvents tabPage3 As TabPage
    Public WithEvents PanelViewEmail As Panel
    Private WithEvents tabControl1 As TabControl
    Private WithEvents panel1 As Panel
    Private WithEvents button_Accep As Button
    Private WithEvents button_Cancel As Button
End Class
