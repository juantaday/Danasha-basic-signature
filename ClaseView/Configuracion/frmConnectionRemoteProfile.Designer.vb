<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmConnectionRemoteProfile
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConnectionRemoteProfile))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtPassword = New JMControls.Controls.TextBoxRounded()
        Me.txtDatabase = New JMControls.Controls.TextBoxRounded()
        Me.txtUserName = New JMControls.Controls.TextBoxRounded()
        Me.txtHost = New JMControls.Controls.TextBoxRounded()
        Me.pnlRunning = New System.Windows.Forms.Panel()
        Me.lblRunningIcon = New System.Windows.Forms.Label()
        Me.lblRunningMsg = New System.Windows.Forms.Label()
        Me.progressRunning = New System.Windows.Forms.ProgressBar()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.pnlRunning.SuspendLayout()
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(280, 382)
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(34, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Host:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(34, 120)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 20)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Database:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(34, 206)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 20)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Username:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(34, 295)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 20)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Password:"
        '
        'txtPassword
        '
        Me.txtPassword.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.txtPassword.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.txtPassword.BackColor = System.Drawing.Color.White
        Me.txtPassword.BorderColorActive = System.Drawing.Color.Red
        Me.txtPassword.BorderColorDisable = System.Drawing.Color.LightGray
        Me.txtPassword.BorderColorHover = System.Drawing.Color.Orange
        Me.txtPassword.BorderColorIdle = System.Drawing.Color.Gray
        Me.txtPassword.BorderRadius = 14
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPassword.BorderThickness = 2
        Me.txtPassword.ButtonImage = Global.DanashaBasicSignature.My.Resources.Resources.eyes_32
        Me.txtPassword.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtPassword.DecimalPosition = 2
        Me.txtPassword.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.txtPassword.ForeColor = System.Drawing.Color.Black
        Me.txtPassword.IconLeft = CType(resources.GetObject("txtPassword.IconLeft"), System.Drawing.Image)
        Me.txtPassword.IconLeftBackColor = System.Drawing.Color.White
        Me.txtPassword.IconLeftVisible = False
        Me.txtPassword.Location = New System.Drawing.Point(34, 321)
        Me.txtPassword.MaxLength = 32767
        Me.txtPassword.Multiline = False
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtPassword.PlaceHolderColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.txtPassword.PlaceHolderText = "Contraseña"
        Me.txtPassword.ReadOnly = False
        Me.txtPassword.SelectedText = ""
        Me.txtPassword.SelectionLength = 0
        Me.txtPassword.Size = New System.Drawing.Size(408, 36)
        Me.txtPassword.TabIndex = 3
        Me.txtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtPassword.ToolTipButton = ""
        Me.txtPassword.TypeData = JMControls.Enums.TypeDataEnum.VarChar
        Me.txtPassword.UseSystemPasswordChar = False
        Me.txtPassword.VisibleButton = True
        '
        'txtDatabase
        '
        Me.txtDatabase.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.txtDatabase.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.txtDatabase.BackColor = System.Drawing.Color.White
        Me.txtDatabase.BorderColorActive = System.Drawing.Color.Red
        Me.txtDatabase.BorderColorDisable = System.Drawing.Color.LightGray
        Me.txtDatabase.BorderColorHover = System.Drawing.Color.Orange
        Me.txtDatabase.BorderColorIdle = System.Drawing.Color.Gray
        Me.txtDatabase.BorderRadius = 14
        Me.txtDatabase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDatabase.BorderThickness = 2
        Me.txtDatabase.ButtonImage = CType(resources.GetObject("txtDatabase.ButtonImage"), System.Drawing.Image)
        Me.txtDatabase.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDatabase.DecimalPosition = 2
        Me.txtDatabase.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.txtDatabase.ForeColor = System.Drawing.Color.Black
        Me.txtDatabase.IconLeft = CType(resources.GetObject("txtDatabase.IconLeft"), System.Drawing.Image)
        Me.txtDatabase.IconLeftBackColor = System.Drawing.Color.White
        Me.txtDatabase.IconLeftVisible = False
        Me.txtDatabase.Location = New System.Drawing.Point(34, 146)
        Me.txtDatabase.MaxLength = 32767
        Me.txtDatabase.Multiline = False
        Me.txtDatabase.Name = "txtDatabase"
        Me.txtDatabase.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtDatabase.PlaceHolderColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.txtDatabase.PlaceHolderText = "Base de datos"
        Me.txtDatabase.ReadOnly = False
        Me.txtDatabase.SelectedText = ""
        Me.txtDatabase.SelectionLength = 0
        Me.txtDatabase.Size = New System.Drawing.Size(408, 36)
        Me.txtDatabase.TabIndex = 1
        Me.txtDatabase.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtDatabase.ToolTipButton = ""
        Me.txtDatabase.TypeData = JMControls.Enums.TypeDataEnum.VarChar
        Me.txtDatabase.UseSystemPasswordChar = False
        Me.txtDatabase.VisibleButton = False
        '
        'txtUserName
        '
        Me.txtUserName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.txtUserName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.txtUserName.BackColor = System.Drawing.Color.White
        Me.txtUserName.BorderColorActive = System.Drawing.Color.Red
        Me.txtUserName.BorderColorDisable = System.Drawing.Color.LightGray
        Me.txtUserName.BorderColorHover = System.Drawing.Color.Orange
        Me.txtUserName.BorderColorIdle = System.Drawing.Color.Gray
        Me.txtUserName.BorderRadius = 14
        Me.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUserName.BorderThickness = 2
        Me.txtUserName.ButtonImage = CType(resources.GetObject("txtUserName.ButtonImage"), System.Drawing.Image)
        Me.txtUserName.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtUserName.DecimalPosition = 2
        Me.txtUserName.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.txtUserName.ForeColor = System.Drawing.Color.Black
        Me.txtUserName.IconLeft = CType(resources.GetObject("txtUserName.IconLeft"), System.Drawing.Image)
        Me.txtUserName.IconLeftBackColor = System.Drawing.Color.White
        Me.txtUserName.IconLeftVisible = False
        Me.txtUserName.Location = New System.Drawing.Point(34, 233)
        Me.txtUserName.MaxLength = 32767
        Me.txtUserName.Multiline = False
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtUserName.PlaceHolderColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.txtUserName.PlaceHolderText = "Usuario supabase"
        Me.txtUserName.ReadOnly = False
        Me.txtUserName.SelectedText = ""
        Me.txtUserName.SelectionLength = 0
        Me.txtUserName.Size = New System.Drawing.Size(408, 36)
        Me.txtUserName.TabIndex = 2
        Me.txtUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtUserName.ToolTipButton = ""
        Me.txtUserName.TypeData = JMControls.Enums.TypeDataEnum.VarChar
        Me.txtUserName.UseSystemPasswordChar = False
        Me.txtUserName.VisibleButton = False
        '
        'txtHost
        '
        Me.txtHost.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.txtHost.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.txtHost.BackColor = System.Drawing.Color.White
        Me.txtHost.BorderColorActive = System.Drawing.Color.Red
        Me.txtHost.BorderColorDisable = System.Drawing.Color.LightGray
        Me.txtHost.BorderColorHover = System.Drawing.Color.Orange
        Me.txtHost.BorderColorIdle = System.Drawing.Color.Gray
        Me.txtHost.BorderRadius = 14
        Me.txtHost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHost.BorderThickness = 2
        Me.txtHost.ButtonImage = CType(resources.GetObject("txtHost.ButtonImage"), System.Drawing.Image)
        Me.txtHost.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtHost.DecimalPosition = 2
        Me.txtHost.Font = New System.Drawing.Font("Arial", 12.0!)
        Me.txtHost.ForeColor = System.Drawing.Color.Black
        Me.txtHost.IconLeft = CType(resources.GetObject("txtHost.IconLeft"), System.Drawing.Image)
        Me.txtHost.IconLeftBackColor = System.Drawing.Color.White
        Me.txtHost.IconLeftVisible = False
        Me.txtHost.Location = New System.Drawing.Point(34, 59)
        Me.txtHost.MaxLength = 32767
        Me.txtHost.Multiline = False
        Me.txtHost.Name = "txtHost"
        Me.txtHost.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtHost.PlaceHolderColor = System.Drawing.Color.FromArgb(CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(180, Byte), Integer))
        Me.txtHost.PlaceHolderText = "Host supabase"
        Me.txtHost.ReadOnly = False
        Me.txtHost.SelectedText = ""
        Me.txtHost.SelectionLength = 0
        Me.txtHost.Size = New System.Drawing.Size(408, 36)
        Me.txtHost.TabIndex = 0
        Me.txtHost.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtHost.ToolTipButton = ""
        Me.txtHost.TypeData = JMControls.Enums.TypeDataEnum.VarChar
        Me.txtHost.UseSystemPasswordChar = False
        Me.txtHost.VisibleButton = False
        '
        'pnlRunning
        '
        Me.pnlRunning.BackColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer), CType(CType(30, Byte), Integer))
        Me.pnlRunning.Controls.Add(Me.lblRunningIcon)
        Me.pnlRunning.Controls.Add(Me.lblRunningMsg)
        Me.pnlRunning.Controls.Add(Me.progressRunning)
        Me.pnlRunning.Location = New System.Drawing.Point(22, 170)
        Me.pnlRunning.Name = "pnlRunning"
        Me.pnlRunning.Size = New System.Drawing.Size(480, 88)
        Me.pnlRunning.TabIndex = 0
        Me.pnlRunning.Visible = False
        '
        'lblRunningIcon
        '
        Me.lblRunningIcon.AutoSize = True
        Me.lblRunningIcon.Font = New System.Drawing.Font("Segoe UI Emoji", 22.0!)
        Me.lblRunningIcon.ForeColor = System.Drawing.Color.White
        Me.lblRunningIcon.Location = New System.Drawing.Point(20, 18)
        Me.lblRunningIcon.Name = "lblRunningIcon"
        Me.lblRunningIcon.Size = New System.Drawing.Size(58, 40)
        Me.lblRunningIcon.TabIndex = 0
        Me.lblRunningIcon.Text = "⏳"
        '
        'lblRunningMsg
        '
        Me.lblRunningMsg.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lblRunningMsg.ForeColor = System.Drawing.Color.White
        Me.lblRunningMsg.Location = New System.Drawing.Point(60, 22)
        Me.lblRunningMsg.Name = "lblRunningMsg"
        Me.lblRunningMsg.Size = New System.Drawing.Size(300, 28)
        Me.lblRunningMsg.TabIndex = 1
        Me.lblRunningMsg.Text = "Procesando..."
        '
        'progressRunning
        '
        Me.progressRunning.Location = New System.Drawing.Point(20, 58)
        Me.progressRunning.MarqueeAnimationSpeed = 30
        Me.progressRunning.Name = "progressRunning"
        Me.progressRunning.Size = New System.Drawing.Size(440, 12)
        Me.progressRunning.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.progressRunning.TabIndex = 2
        '
        'frmConnectionRemoteProfile
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(516, 441)
        Me.Controls.Add(Me.pnlRunning)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtDatabase)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtUserName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtHost)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmConnectionRemoteProfile"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Configuraciones Remotas"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.pnlRunning.ResumeLayout(False)
        Me.pnlRunning.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As Label
    Friend WithEvents txtHost As JMControls.Controls.TextBoxRounded
    Friend WithEvents Label2 As Label
    Friend WithEvents txtDatabase As JMControls.Controls.TextBoxRounded
    Friend WithEvents Label3 As Label
    Friend WithEvents txtUserName As JMControls.Controls.TextBoxRounded
    Friend WithEvents Label4 As Label
    Friend WithEvents txtPassword As JMControls.Controls.TextBoxRounded

    ' Panel Running
    Friend WithEvents pnlRunning As System.Windows.Forms.Panel
    Friend WithEvents lblRunningMsg As System.Windows.Forms.Label
    Friend WithEvents lblRunningIcon As System.Windows.Forms.Label
    Friend WithEvents progressRunning As System.Windows.Forms.ProgressBar

End Class
