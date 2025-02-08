<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MyCommerceForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MyCommerceForm))
        Me.panel3 = New System.Windows.Forms.Panel()
        Me.txtHuella = New JMControls.Controls.TextBoxRounded()
        Me.label18 = New System.Windows.Forms.Label()
        Me.expandCollapsePanel1 = New JMControls.ExpandCollapsePanel.ExpandCollapsePanel()
        Me.label16 = New System.Windows.Forms.Label()
        Me.TokenListComboBox = New JMControls.Controls.RJComboBox()
        Me.label17 = New System.Windows.Forms.Label()
        Me.tabPageEx2 = New JMControls.TabControlGRD.TabPageEx()
        Me.cmbTypeBusiness = New JMControls.Controls.RJComboBox()
        Me.txtRegimenMicro = New System.Windows.Forms.TextBox()
        Me.txtAgentRetenNum = New System.Windows.Forms.TextBox()
        Me.txtNumResolucion = New System.Windows.Forms.TextBox()
        Me.ContabiliteChecBox = New System.Windows.Forms.CheckBox()
        Me.label14 = New System.Windows.Forms.Label()
        Me.label13 = New System.Windows.Forms.Label()
        Me.label12 = New System.Windows.Forms.Label()
        Me.ContabiliteLabel = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.altoNumericUpDown1 = New JMControls.Controls.AltoNumericUpDown()
        Me.rjRadioButton3 = New JMControls.Controls.RJRadioButton()
        Me.rjRadioButton2 = New JMControls.Controls.RJRadioButton()
        Me.rjRadioButton1 = New JMControls.Controls.RJRadioButton()
        Me.groupBoxLiner1 = New JMControls.Controls.GroupBoxLiner()
        Me.label20 = New System.Windows.Forms.Label()
        Me.tipoAmbienteComboBox = New JMControls.Controls.RJComboBox()
        Me.label10 = New System.Windows.Forms.Label()
        Me.timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.tabPageEx1 = New JMControls.TabControlGRD.TabPageEx()
        Me.circularProgressBar1 = New CircularProgressBar.CircularProgressBar()
        Me.lblRucCount = New System.Windows.Forms.Label()
        Me.txtDirMatriz = New JMControls.Controls.RJTextBox()
        Me.label4 = New System.Windows.Forms.Label()
        Me.txtCompany = New JMControls.Controls.RJTextBox()
        Me.label8 = New System.Windows.Forms.Label()
        Me.txtNomComercial = New JMControls.Controls.RJTextBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.txtRazonSocial = New JMControls.Controls.RJTextBox()
        Me.label2 = New System.Windows.Forms.Label()
        Me.txtRuc = New JMControls.Controls.RJTextBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.errorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.backgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.label21 = New System.Windows.Forms.Label()
        Me.panel2 = New System.Windows.Forms.Panel()
        Me.groupBoxLiner3 = New JMControls.Controls.GroupBoxLiner()
        Me.logoPictureBox1 = New System.Windows.Forms.PictureBox()
        Me.rjButton3 = New JMControls.Controls.RJButton()
        Me.rjButton4 = New JMControls.Controls.RJButton()
        Me.lblState = New System.Windows.Forms.Label()
        Me.rjButton1 = New JMControls.Controls.RJButton()
        Me.Accep_Button = New JMControls.Controls.RJButton()
        Me.logoPictureBox = New System.Windows.Forms.PictureBox()
        Me.rjButton2 = New JMControls.Controls.RJButton()
        Me.groupBoxLiner2 = New JMControls.Controls.GroupBoxLiner()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.tabPageEx3 = New JMControls.TabControlGRD.TabPageEx()
        Me.panel4 = New System.Windows.Forms.Panel()
        Me.jmTabControl1 = New JMControls.TabControlGRD.JMTabControl()
        Me.tabPageEx4 = New JMControls.TabControlGRD.TabPageEx()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtRegimenRIMPE = New System.Windows.Forms.TextBox()
        Me.panel3.SuspendLayout()
        Me.expandCollapsePanel1.SuspendLayout()
        Me.tabPageEx2.SuspendLayout()
        Me.groupBoxLiner1.SuspendLayout()
        Me.tabPageEx1.SuspendLayout()
        CType(Me.errorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panel2.SuspendLayout()
        Me.groupBoxLiner3.SuspendLayout()
        CType(Me.logoPictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.logoPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupBoxLiner2.SuspendLayout()
        Me.panel1.SuspendLayout()
        Me.tabPageEx3.SuspendLayout()
        Me.jmTabControl1.SuspendLayout()
        Me.tabPageEx4.SuspendLayout()
        Me.SuspendLayout()
        '
        'panel3
        '
        Me.panel3.Controls.Add(Me.expandCollapsePanel1)
        Me.panel3.Location = New System.Drawing.Point(32, 86)
        Me.panel3.Name = "panel3"
        Me.panel3.Size = New System.Drawing.Size(565, 227)
        Me.panel3.TabIndex = 6
        '
        'txtHuella
        '
        Me.txtHuella.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None
        Me.txtHuella.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None
        Me.txtHuella.BackColor = System.Drawing.Color.White
        Me.txtHuella.BorderColorActive = System.Drawing.Color.Empty
        Me.txtHuella.BorderColorDisable = System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.txtHuella.BorderColorHover = System.Drawing.Color.Empty
        Me.txtHuella.BorderColorIdle = System.Drawing.Color.Teal
        Me.txtHuella.BorderRadius = 8
        Me.txtHuella.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHuella.BorderThickness = 1
        Me.txtHuella.ButtonImage = Nothing
        Me.txtHuella.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtHuella.DecimalPosition = 2
        Me.txtHuella.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!)
        Me.txtHuella.IconLeft = CType(resources.GetObject("txtHuella.IconLeft"), System.Drawing.Image)
        Me.txtHuella.IconLeftBackColor = System.Drawing.Color.White
        Me.txtHuella.IconLeftVisible = False
        Me.txtHuella.Location = New System.Drawing.Point(25, 139)
        Me.txtHuella.MaxLength = 32767
        Me.txtHuella.Multiline = False
        Me.txtHuella.Name = "txtHuella"
        Me.txtHuella.Padding = New System.Windows.Forms.Padding(5, 3, 5, 3)
        Me.txtHuella.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtHuella.PlaceHolderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.txtHuella.PlaceHolderText = "Busque la ruta del archivo "
        Me.txtHuella.ReadOnly = False
        Me.txtHuella.SelectedText = ""
        Me.txtHuella.SelectionLength = 0
        Me.txtHuella.Size = New System.Drawing.Size(523, 36)
        Me.txtHuella.TabIndex = 1
        Me.txtHuella.TabStop = False
        Me.txtHuella.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtHuella.ToolTipButton = ""
        Me.txtHuella.TypeData = JMControls.Enums.TypeDataEnum.VarChar
        Me.txtHuella.UseSystemPasswordChar = False
        Me.txtHuella.VisibleButton = True
        '
        'label18
        '
        Me.label18.AutoSize = True
        Me.label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.label18.Location = New System.Drawing.Point(21, 116)
        Me.label18.Name = "label18"
        Me.label18.Size = New System.Drawing.Size(97, 20)
        Me.label18.TabIndex = 0
        Me.label18.Text = "Huella dijital:"
        '
        'expandCollapsePanel1
        '
        Me.expandCollapsePanel1.AlignmentIcon = JMControls.ExpandCollapsePanel.ExpandCollapseButton.ExpandIconAlignment.Right
        Me.expandCollapsePanel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(153, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.expandCollapsePanel1.BottonTitleLocation = New System.Drawing.Point(888, 405)
        Me.expandCollapsePanel1.ButtonBackColor = System.Drawing.Color.FromArgb(CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer), CType(CType(60, Byte), Integer))
        Me.expandCollapsePanel1.ButtonBackColorHover = System.Drawing.Color.FromArgb(CType(CType(153, Byte), Integer), CType(CType(180, Byte), Integer), CType(CType(209, Byte), Integer))
        Me.expandCollapsePanel1.ButtonImage = Nothing
        Me.expandCollapsePanel1.ButtonImageLocation = New System.Drawing.Point(76, 137)
        Me.expandCollapsePanel1.ButtonImageSize = New System.Drawing.Size(765, 1350)
        Me.expandCollapsePanel1.ButtonLogoSize = JMControls.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonSize.Normal
        Me.expandCollapsePanel1.ButtonLogoStyle = JMControls.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonStyle.Arrow
        Me.expandCollapsePanel1.Controls.Add(Me.label16)
        Me.expandCollapsePanel1.Controls.Add(Me.TokenListComboBox)
        Me.expandCollapsePanel1.Controls.Add(Me.txtHuella)
        Me.expandCollapsePanel1.Controls.Add(Me.label18)
        Me.expandCollapsePanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.expandCollapsePanel1.ExpandedHeight = 102
        Me.expandCollapsePanel1.IsExpanded = True
        Me.expandCollapsePanel1.Location = New System.Drawing.Point(0, 0)
        Me.expandCollapsePanel1.Name = "expandCollapsePanel1"
        Me.expandCollapsePanel1.Size = New System.Drawing.Size(565, 207)
        Me.expandCollapsePanel1.TabIndex = 5
        Me.expandCollapsePanel1.Text = "Instalada en el equipo"
        Me.expandCollapsePanel1.UseAnimation = True
        Me.expandCollapsePanel1.VisibleDefaultButton = False
        Me.expandCollapsePanel1.VisibleIconButton = False
        '
        'label16
        '
        Me.label16.AutoSize = True
        Me.label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.label16.Location = New System.Drawing.Point(3, 57)
        Me.label16.Name = "label16"
        Me.label16.Size = New System.Drawing.Size(137, 20)
        Me.label16.TabIndex = 0
        Me.label16.Text = "Token para firmar:"
        '
        'TokenListComboBox
        '
        Me.TokenListComboBox.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TokenListComboBox.BorderColor = System.Drawing.Color.MediumSlateBlue
        Me.TokenListComboBox.BorderThickness = 1
        Me.TokenListComboBox.ButtonImage = Nothing
        Me.TokenListComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.TokenListComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown
        Me.TokenListComboBox.DroppedDown = False
        Me.TokenListComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.TokenListComboBox.ForeColor = System.Drawing.Color.DimGray
        Me.TokenListComboBox.IconColor = System.Drawing.Color.MediumSlateBlue
        Me.TokenListComboBox.ListBackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.TokenListComboBox.ListTextColor = System.Drawing.Color.DimGray
        Me.TokenListComboBox.Location = New System.Drawing.Point(146, 57)
        Me.TokenListComboBox.MinimumSize = New System.Drawing.Size(200, 30)
        Me.TokenListComboBox.Name = "TokenListComboBox"
        Me.TokenListComboBox.Padding = New System.Windows.Forms.Padding(1)
        Me.TokenListComboBox.Size = New System.Drawing.Size(390, 30)
        Me.TokenListComboBox.TabIndex = 1
        Me.TokenListComboBox.VisibleButtonOption = False
        Me.TokenListComboBox.WidthButton = 30
        '
        'label17
        '
        Me.label17.AutoSize = True
        Me.label17.Location = New System.Drawing.Point(32, 352)
        Me.label17.Name = "label17"
        Me.label17.Size = New System.Drawing.Size(168, 32)
        Me.label17.TabIndex = 0
        Me.label17.Text = "Tiempo máximo de " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "espera para autorización:"
        '
        'tabPageEx2
        '
        Me.tabPageEx2.AutoScroll = True
        Me.tabPageEx2.BackColor = System.Drawing.Color.White
        Me.tabPageEx2.Controls.Add(Me.cmbTypeBusiness)
        Me.tabPageEx2.Controls.Add(Me.txtRegimenRIMPE)
        Me.tabPageEx2.Controls.Add(Me.txtRegimenMicro)
        Me.tabPageEx2.Controls.Add(Me.txtAgentRetenNum)
        Me.tabPageEx2.Controls.Add(Me.txtNumResolucion)
        Me.tabPageEx2.Controls.Add(Me.ContabiliteChecBox)
        Me.tabPageEx2.Controls.Add(Me.Label5)
        Me.tabPageEx2.Controls.Add(Me.label14)
        Me.tabPageEx2.Controls.Add(Me.label13)
        Me.tabPageEx2.Controls.Add(Me.label12)
        Me.tabPageEx2.Controls.Add(Me.ContabiliteLabel)
        Me.tabPageEx2.Controls.Add(Me.Label11)
        Me.tabPageEx2.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.tabPageEx2.ImageLocation = New System.Drawing.Point(15, 5)
        Me.tabPageEx2.IsClosable = False
        Me.tabPageEx2.Location = New System.Drawing.Point(1, 41)
        Me.tabPageEx2.Name = "tabPageEx2"
        Me.tabPageEx2.Size = New System.Drawing.Size(615, 528)
        Me.tabPageEx2.TabIndex = 1
        Me.tabPageEx2.Text = "Métodos tributarios"
        '
        'cmbTypeBusiness
        '
        Me.cmbTypeBusiness.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cmbTypeBusiness.BorderColor = System.Drawing.Color.MediumSlateBlue
        Me.cmbTypeBusiness.BorderThickness = 1
        Me.cmbTypeBusiness.ButtonImage = Nothing
        Me.cmbTypeBusiness.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cmbTypeBusiness.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown
        Me.cmbTypeBusiness.DroppedDown = False
        Me.cmbTypeBusiness.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.cmbTypeBusiness.ForeColor = System.Drawing.Color.DimGray
        Me.cmbTypeBusiness.IconColor = System.Drawing.Color.MediumSlateBlue
        Me.cmbTypeBusiness.Items.AddRange(New Object() {"ONTRIBUYENTE RÉGIMEN RIMPE", "CONTRIBUYENTE NEGOCIO POPULAR - RÉGIMEN RIMPE"})
        Me.cmbTypeBusiness.ListBackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.cmbTypeBusiness.ListTextColor = System.Drawing.Color.DimGray
        Me.cmbTypeBusiness.Location = New System.Drawing.Point(85, 70)
        Me.cmbTypeBusiness.MinimumSize = New System.Drawing.Size(200, 30)
        Me.cmbTypeBusiness.Name = "cmbTypeBusiness"
        Me.cmbTypeBusiness.Padding = New System.Windows.Forms.Padding(1)
        Me.cmbTypeBusiness.Size = New System.Drawing.Size(425, 30)
        Me.cmbTypeBusiness.TabIndex = 35
        Me.cmbTypeBusiness.VisibleButtonOption = False
        Me.cmbTypeBusiness.WidthButton = 32
        '
        'txtRegimenMicro
        '
        Me.txtRegimenMicro.ForeColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.txtRegimenMicro.Location = New System.Drawing.Point(102, 390)
        Me.txtRegimenMicro.Name = "txtRegimenMicro"
        Me.txtRegimenMicro.Size = New System.Drawing.Size(408, 23)
        Me.txtRegimenMicro.TabIndex = 36
        Me.txtRegimenMicro.Text = "CONTRIBUYENTE RÉGIMEN MICROEMPRESAS"
        '
        'txtAgentRetenNum
        '
        Me.txtAgentRetenNum.ForeColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.txtAgentRetenNum.Location = New System.Drawing.Point(158, 295)
        Me.txtAgentRetenNum.Name = "txtAgentRetenNum"
        Me.txtAgentRetenNum.Size = New System.Drawing.Size(284, 23)
        Me.txtAgentRetenNum.TabIndex = 37
        '
        'txtNumResolucion
        '
        Me.txtNumResolucion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.txtNumResolucion.Location = New System.Drawing.Point(158, 209)
        Me.txtNumResolucion.Name = "txtNumResolucion"
        Me.txtNumResolucion.Size = New System.Drawing.Size(284, 23)
        Me.txtNumResolucion.TabIndex = 38
        '
        'ContabiliteChecBox
        '
        Me.ContabiliteChecBox.AutoSize = True
        Me.ContabiliteChecBox.Location = New System.Drawing.Point(359, 122)
        Me.ContabiliteChecBox.Name = "ContabiliteChecBox"
        Me.ContabiliteChecBox.Size = New System.Drawing.Size(15, 14)
        Me.ContabiliteChecBox.TabIndex = 45
        Me.ContabiliteChecBox.UseVisualStyleBackColor = True
        '
        'label14
        '
        Me.label14.AutoSize = True
        Me.label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.label14.Location = New System.Drawing.Point(85, 41)
        Me.label14.Name = "label14"
        Me.label14.Size = New System.Drawing.Size(146, 20)
        Me.label14.TabIndex = 39
        Me.label14.Text = "Tipo Contribuyente:"
        '
        'label13
        '
        Me.label13.AutoSize = True
        Me.label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.label13.Location = New System.Drawing.Point(102, 361)
        Me.label13.Name = "label13"
        Me.label13.Size = New System.Drawing.Size(246, 20)
        Me.label13.TabIndex = 40
        Me.label13.Text = "Etiqueta régimen microempresas:"
        '
        'label12
        '
        Me.label12.AutoSize = True
        Me.label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.label12.Location = New System.Drawing.Point(176, 271)
        Me.label12.Name = "label12"
        Me.label12.Size = New System.Drawing.Size(212, 15)
        Me.label12.TabIndex = 41
        Me.label12.Text = "Agente de Retencion Nro Resolucion:"
        '
        'ContabiliteLabel
        '
        Me.ContabiliteLabel.AutoSize = True
        Me.ContabiliteLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ContabiliteLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.ContabiliteLabel.Location = New System.Drawing.Point(176, 120)
        Me.ContabiliteLabel.Name = "ContabiliteLabel"
        Me.ContabiliteLabel.Size = New System.Drawing.Size(172, 15)
        Me.ContabiliteLabel.TabIndex = 34
        Me.ContabiliteLabel.Text = "Obligado a llevar contabilidad:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(188, 182)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(223, 15)
        Me.Label11.TabIndex = 42
        Me.Label11.Text = "Contribuyente Especial Nro Resolucion:"
        '
        'altoNumericUpDown1
        '
        Me.altoNumericUpDown1.ButtonBackColor = System.Drawing.Color.Gray
        Me.altoNumericUpDown1.DecimalPlace = 0
        Me.altoNumericUpDown1.Font = New System.Drawing.Font("Comic Sans MS", 12.0!)
        Me.altoNumericUpDown1.Location = New System.Drawing.Point(210, 354)
        Me.altoNumericUpDown1.Maximum = New Decimal(New Integer() {6, 0, 0, 0})
        Me.altoNumericUpDown1.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.altoNumericUpDown1.Name = "altoNumericUpDown1"
        Me.altoNumericUpDown1.ReadOnly = False
        Me.altoNumericUpDown1.SignColor = System.Drawing.Color.White
        Me.altoNumericUpDown1.Size = New System.Drawing.Size(100, 30)
        Me.altoNumericUpDown1.TabIndex = 2
        Me.altoNumericUpDown1.Text = "altoNumericUpDown1"
        Me.altoNumericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.altoNumericUpDown1.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'rjRadioButton3
        '
        Me.rjRadioButton3.AutoSize = True
        Me.rjRadioButton3.CheckedColor = System.Drawing.Color.MediumSlateBlue
        Me.rjRadioButton3.Location = New System.Drawing.Point(381, 22)
        Me.rjRadioButton3.MinimumSize = New System.Drawing.Size(0, 21)
        Me.rjRadioButton3.Name = "rjRadioButton3"
        Me.rjRadioButton3.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.rjRadioButton3.Size = New System.Drawing.Size(94, 21)
        Me.rjRadioButton3.TabIndex = 5
        Me.rjRadioButton3.TabStop = True
        Me.rjRadioButton3.Text = "Red local"
        Me.rjRadioButton3.UnCheckedColor = System.Drawing.Color.Gray
        Me.rjRadioButton3.UseVisualStyleBackColor = True
        '
        'rjRadioButton2
        '
        Me.rjRadioButton2.AutoSize = True
        Me.rjRadioButton2.CheckedColor = System.Drawing.Color.MediumSlateBlue
        Me.rjRadioButton2.Location = New System.Drawing.Point(210, 22)
        Me.rjRadioButton2.MinimumSize = New System.Drawing.Size(0, 21)
        Me.rjRadioButton2.Name = "rjRadioButton2"
        Me.rjRadioButton2.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.rjRadioButton2.Size = New System.Drawing.Size(149, 21)
        Me.rjRadioButton2.TabIndex = 4
        Me.rjRadioButton2.Text = "En ruta específica"
        Me.rjRadioButton2.UnCheckedColor = System.Drawing.Color.Gray
        Me.rjRadioButton2.UseVisualStyleBackColor = True
        '
        'rjRadioButton1
        '
        Me.rjRadioButton1.AutoSize = True
        Me.rjRadioButton1.Checked = True
        Me.rjRadioButton1.CheckedColor = System.Drawing.Color.MediumSlateBlue
        Me.rjRadioButton1.Location = New System.Drawing.Point(23, 22)
        Me.rjRadioButton1.MinimumSize = New System.Drawing.Size(0, 21)
        Me.rjRadioButton1.Name = "rjRadioButton1"
        Me.rjRadioButton1.Padding = New System.Windows.Forms.Padding(10, 0, 0, 0)
        Me.rjRadioButton1.Size = New System.Drawing.Size(166, 21)
        Me.rjRadioButton1.TabIndex = 3
        Me.rjRadioButton1.TabStop = True
        Me.rjRadioButton1.Text = "Instalda en el equipo"
        Me.rjRadioButton1.UnCheckedColor = System.Drawing.Color.Gray
        Me.rjRadioButton1.UseVisualStyleBackColor = True
        '
        'groupBoxLiner1
        '
        Me.groupBoxLiner1.BorderColor = System.Drawing.Color.Black
        Me.groupBoxLiner1.BorderRadius = 8
        Me.groupBoxLiner1.BorderThickness = 1
        Me.groupBoxLiner1.Controls.Add(Me.rjRadioButton3)
        Me.groupBoxLiner1.Controls.Add(Me.rjRadioButton2)
        Me.groupBoxLiner1.Controls.Add(Me.rjRadioButton1)
        Me.groupBoxLiner1.Location = New System.Drawing.Point(32, 21)
        Me.groupBoxLiner1.Name = "groupBoxLiner1"
        Me.groupBoxLiner1.Size = New System.Drawing.Size(566, 59)
        Me.groupBoxLiner1.TabIndex = 4
        Me.groupBoxLiner1.TabStop = False
        Me.groupBoxLiner1.Text = "Alojamiento de firma."
        '
        'label20
        '
        Me.label20.AutoSize = True
        Me.label20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.label20.Location = New System.Drawing.Point(313, 361)
        Me.label20.Name = "label20"
        Me.label20.Size = New System.Drawing.Size(71, 16)
        Me.label20.TabIndex = 7
        Me.label20.Text = "Segundos"
        '
        'tipoAmbienteComboBox
        '
        Me.tipoAmbienteComboBox.BackColor = System.Drawing.Color.WhiteSmoke
        Me.tipoAmbienteComboBox.BorderColor = System.Drawing.Color.MediumSlateBlue
        Me.tipoAmbienteComboBox.BorderThickness = 1
        Me.tipoAmbienteComboBox.ButtonImage = Nothing
        Me.tipoAmbienteComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.tipoAmbienteComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown
        Me.tipoAmbienteComboBox.DroppedDown = False
        Me.tipoAmbienteComboBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.tipoAmbienteComboBox.ForeColor = System.Drawing.Color.DimGray
        Me.tipoAmbienteComboBox.IconColor = System.Drawing.Color.MediumSlateBlue
        Me.tipoAmbienteComboBox.ListBackColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(228, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.tipoAmbienteComboBox.ListTextColor = System.Drawing.Color.DimGray
        Me.tipoAmbienteComboBox.Location = New System.Drawing.Point(210, 429)
        Me.tipoAmbienteComboBox.MinimumSize = New System.Drawing.Size(200, 30)
        Me.tipoAmbienteComboBox.Name = "tipoAmbienteComboBox"
        Me.tipoAmbienteComboBox.Padding = New System.Windows.Forms.Padding(1)
        Me.tipoAmbienteComboBox.Size = New System.Drawing.Size(287, 30)
        Me.tipoAmbienteComboBox.TabIndex = 1
        Me.tipoAmbienteComboBox.VisibleButtonOption = False
        Me.tipoAmbienteComboBox.WidthButton = 30
        '
        'label10
        '
        Me.label10.AutoSize = True
        Me.label10.Location = New System.Drawing.Point(32, 436)
        Me.label10.Name = "label10"
        Me.label10.Size = New System.Drawing.Size(120, 16)
        Me.label10.TabIndex = 0
        Me.label10.Text = "Tipo de ambiente:"
        '
        'timer1
        '
        Me.timer1.Interval = 3000
        '
        'tabPageEx1
        '
        Me.tabPageEx1.AutoScroll = True
        Me.tabPageEx1.BackColor = System.Drawing.Color.White
        Me.tabPageEx1.Controls.Add(Me.circularProgressBar1)
        Me.tabPageEx1.Controls.Add(Me.lblRucCount)
        Me.tabPageEx1.Controls.Add(Me.txtDirMatriz)
        Me.tabPageEx1.Controls.Add(Me.label4)
        Me.tabPageEx1.Controls.Add(Me.txtCompany)
        Me.tabPageEx1.Controls.Add(Me.label8)
        Me.tabPageEx1.Controls.Add(Me.txtNomComercial)
        Me.tabPageEx1.Controls.Add(Me.label3)
        Me.tabPageEx1.Controls.Add(Me.txtRazonSocial)
        Me.tabPageEx1.Controls.Add(Me.label2)
        Me.tabPageEx1.Controls.Add(Me.txtRuc)
        Me.tabPageEx1.Controls.Add(Me.label1)
        Me.tabPageEx1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.tabPageEx1.ImageLocation = New System.Drawing.Point(15, 5)
        Me.tabPageEx1.IsClosable = False
        Me.tabPageEx1.Location = New System.Drawing.Point(1, 41)
        Me.tabPageEx1.Name = "tabPageEx1"
        Me.tabPageEx1.Size = New System.Drawing.Size(615, 528)
        Me.tabPageEx1.TabIndex = 0
        Me.tabPageEx1.Text = "Opciones generales"
        '
        'circularProgressBar1
        '
        Me.circularProgressBar1.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner
        Me.circularProgressBar1.AnimationSpeed = 500
        Me.circularProgressBar1.BackColor = System.Drawing.SystemColors.Window
        Me.circularProgressBar1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold)
        Me.circularProgressBar1.ForeColor = System.Drawing.Color.White
        Me.circularProgressBar1.InnerColor = System.Drawing.SystemColors.Window
        Me.circularProgressBar1.InnerMargin = 2
        Me.circularProgressBar1.InnerWidth = -1
        Me.circularProgressBar1.Location = New System.Drawing.Point(272, 375)
        Me.circularProgressBar1.MarqueeAnimationSpeed = 2000
        Me.circularProgressBar1.Name = "circularProgressBar1"
        Me.circularProgressBar1.OuterColor = System.Drawing.Color.Gray
        Me.circularProgressBar1.OuterMargin = -25
        Me.circularProgressBar1.OuterWidth = 25
        Me.circularProgressBar1.ProgressColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.circularProgressBar1.ProgressWidth = 20
        Me.circularProgressBar1.SecondaryFont = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.circularProgressBar1.Size = New System.Drawing.Size(127, 127)
        Me.circularProgressBar1.StartAngle = 270
        Me.circularProgressBar1.SubscriptColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.circularProgressBar1.SubscriptMargin = New System.Windows.Forms.Padding(10, -35, 0, 0)
        Me.circularProgressBar1.SubscriptText = ""
        Me.circularProgressBar1.SuperscriptColor = System.Drawing.Color.FromArgb(CType(CType(166, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.circularProgressBar1.SuperscriptMargin = New System.Windows.Forms.Padding(10, 35, 0, 0)
        Me.circularProgressBar1.SuperscriptText = ""
        Me.circularProgressBar1.TabIndex = 12
        Me.circularProgressBar1.Text = "Reading"
        Me.circularProgressBar1.TextMargin = New System.Windows.Forms.Padding(0)
        Me.circularProgressBar1.Value = 25
        '
        'lblRucCount
        '
        Me.lblRucCount.AutoSize = True
        Me.lblRucCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.lblRucCount.Location = New System.Drawing.Point(379, 45)
        Me.lblRucCount.Name = "lblRucCount"
        Me.lblRucCount.Size = New System.Drawing.Size(16, 17)
        Me.lblRucCount.TabIndex = 2
        Me.lblRucCount.Text = "0"
        '
        'txtDirMatriz
        '
        Me.txtDirMatriz.BackColor = System.Drawing.SystemColors.Window
        Me.txtDirMatriz.BorderColor = System.Drawing.Color.MediumSlateBlue
        Me.txtDirMatriz.BorderFocusColor = System.Drawing.Color.HotPink
        Me.txtDirMatriz.BorderRadius = 6
        Me.txtDirMatriz.BorderThickness = 1
        Me.txtDirMatriz.CharacterCasin = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDirMatriz.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtDirMatriz.DecimalPosition = 2
        Me.txtDirMatriz.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDirMatriz.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtDirMatriz.Location = New System.Drawing.Point(148, 219)
        Me.txtDirMatriz.Margin = New System.Windows.Forms.Padding(1)
        Me.txtDirMatriz.MaxLength = 32767
        Me.txtDirMatriz.Multiline = True
        Me.txtDirMatriz.Name = "txtDirMatriz"
        Me.txtDirMatriz.Padding = New System.Windows.Forms.Padding(5, 3, 5, 3)
        Me.txtDirMatriz.PasswordChar = False
        Me.txtDirMatriz.PlaceHolderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.txtDirMatriz.PlaceHolderText = "Obligatorio"
        Me.txtDirMatriz.ReadOnly = False
        Me.txtDirMatriz.SelectionLength = 0
        Me.txtDirMatriz.Size = New System.Drawing.Size(427, 86)
        Me.txtDirMatriz.TabIndex = 4
        Me.txtDirMatriz.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtDirMatriz.TypeData = JMControls.Enums.TypeDataEnum.VarChar
        Me.txtDirMatriz.UnderlinedStyle = False
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(14, 223)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(113, 17)
        Me.label4.TabIndex = 0
        Me.label4.Text = "Dirección Matriz:"
        '
        'txtCompany
        '
        Me.txtCompany.BackColor = System.Drawing.SystemColors.Window
        Me.txtCompany.BorderColor = System.Drawing.Color.MediumSlateBlue
        Me.txtCompany.BorderFocusColor = System.Drawing.Color.HotPink
        Me.txtCompany.BorderRadius = 6
        Me.txtCompany.BorderThickness = 1
        Me.txtCompany.CharacterCasin = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCompany.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCompany.DecimalPosition = 2
        Me.txtCompany.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompany.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtCompany.Location = New System.Drawing.Point(148, 177)
        Me.txtCompany.Margin = New System.Windows.Forms.Padding(1)
        Me.txtCompany.MaxLength = 20
        Me.txtCompany.Multiline = False
        Me.txtCompany.Name = "txtCompany"
        Me.txtCompany.Padding = New System.Windows.Forms.Padding(5, 3, 5, 3)
        Me.txtCompany.PasswordChar = False
        Me.txtCompany.PlaceHolderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.txtCompany.PlaceHolderText = "Nombre a imprimir en ticket"
        Me.txtCompany.ReadOnly = False
        Me.txtCompany.SelectionLength = 0
        Me.txtCompany.Size = New System.Drawing.Size(226, 24)
        Me.txtCompany.TabIndex = 3
        Me.txtCompany.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtCompany.TypeData = JMControls.Enums.TypeDataEnum.VarChar
        Me.txtCompany.UnderlinedStyle = False
        '
        'label8
        '
        Me.label8.AutoSize = True
        Me.label8.Location = New System.Drawing.Point(13, 181)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(107, 17)
        Me.label8.TabIndex = 0
        Me.label8.Text = "Compaía (Alias)"
        '
        'txtNomComercial
        '
        Me.txtNomComercial.BackColor = System.Drawing.SystemColors.Window
        Me.txtNomComercial.BorderColor = System.Drawing.Color.MediumSlateBlue
        Me.txtNomComercial.BorderFocusColor = System.Drawing.Color.HotPink
        Me.txtNomComercial.BorderRadius = 6
        Me.txtNomComercial.BorderThickness = 1
        Me.txtNomComercial.CharacterCasin = System.Windows.Forms.CharacterCasing.Normal
        Me.txtNomComercial.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtNomComercial.DecimalPosition = 2
        Me.txtNomComercial.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNomComercial.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtNomComercial.Location = New System.Drawing.Point(148, 132)
        Me.txtNomComercial.Margin = New System.Windows.Forms.Padding(1)
        Me.txtNomComercial.MaxLength = 50
        Me.txtNomComercial.Multiline = False
        Me.txtNomComercial.Name = "txtNomComercial"
        Me.txtNomComercial.Padding = New System.Windows.Forms.Padding(5, 3, 5, 3)
        Me.txtNomComercial.PasswordChar = False
        Me.txtNomComercial.PlaceHolderColor = System.Drawing.Color.DarkGray
        Me.txtNomComercial.PlaceHolderText = "Obligatorio"
        Me.txtNomComercial.ReadOnly = False
        Me.txtNomComercial.SelectionLength = 0
        Me.txtNomComercial.Size = New System.Drawing.Size(426, 24)
        Me.txtNomComercial.TabIndex = 2
        Me.txtNomComercial.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtNomComercial.TypeData = JMControls.Enums.TypeDataEnum.VarChar
        Me.txtNomComercial.UnderlinedStyle = False
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(14, 136)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(126, 17)
        Me.label3.TabIndex = 0
        Me.label3.Text = "Nombre comercial:"
        '
        'txtRazonSocial
        '
        Me.txtRazonSocial.BackColor = System.Drawing.SystemColors.Window
        Me.txtRazonSocial.BorderColor = System.Drawing.Color.MediumSlateBlue
        Me.txtRazonSocial.BorderFocusColor = System.Drawing.Color.HotPink
        Me.txtRazonSocial.BorderRadius = 6
        Me.txtRazonSocial.BorderThickness = 1
        Me.txtRazonSocial.CharacterCasin = System.Windows.Forms.CharacterCasing.Normal
        Me.txtRazonSocial.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtRazonSocial.DecimalPosition = 2
        Me.txtRazonSocial.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRazonSocial.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtRazonSocial.Location = New System.Drawing.Point(148, 78)
        Me.txtRazonSocial.Margin = New System.Windows.Forms.Padding(1)
        Me.txtRazonSocial.MaxLength = 32767
        Me.txtRazonSocial.Multiline = False
        Me.txtRazonSocial.Name = "txtRazonSocial"
        Me.txtRazonSocial.Padding = New System.Windows.Forms.Padding(5, 3, 5, 3)
        Me.txtRazonSocial.PasswordChar = False
        Me.txtRazonSocial.PlaceHolderColor = System.Drawing.Color.DarkGray
        Me.txtRazonSocial.PlaceHolderText = "Obligatorio"
        Me.txtRazonSocial.ReadOnly = False
        Me.txtRazonSocial.SelectionLength = 0
        Me.txtRazonSocial.Size = New System.Drawing.Size(426, 24)
        Me.txtRazonSocial.TabIndex = 1
        Me.txtRazonSocial.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtRazonSocial.TypeData = JMControls.Enums.TypeDataEnum.VarChar
        Me.txtRazonSocial.UnderlinedStyle = False
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(14, 73)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(130, 34)
        Me.label2.TabIndex = 0
        Me.label2.Text = "Apellidos y nombes" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Razon social:"
        '
        'txtRuc
        '
        Me.txtRuc.BackColor = System.Drawing.SystemColors.Window
        Me.txtRuc.BorderColor = System.Drawing.Color.MediumSlateBlue
        Me.txtRuc.BorderFocusColor = System.Drawing.Color.HotPink
        Me.txtRuc.BorderRadius = 6
        Me.txtRuc.BorderThickness = 1
        Me.txtRuc.CharacterCasin = System.Windows.Forms.CharacterCasing.Normal
        Me.txtRuc.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtRuc.DecimalPosition = 2
        Me.txtRuc.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRuc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtRuc.Location = New System.Drawing.Point(148, 41)
        Me.txtRuc.Margin = New System.Windows.Forms.Padding(1)
        Me.txtRuc.MaxLength = 13
        Me.txtRuc.Multiline = False
        Me.txtRuc.Name = "txtRuc"
        Me.txtRuc.Padding = New System.Windows.Forms.Padding(5, 3, 5, 3)
        Me.txtRuc.PasswordChar = False
        Me.txtRuc.PlaceHolderColor = System.Drawing.Color.DarkGray
        Me.txtRuc.PlaceHolderText = "Obligatorio"
        Me.txtRuc.ReadOnly = False
        Me.txtRuc.SelectionLength = 0
        Me.txtRuc.Size = New System.Drawing.Size(223, 24)
        Me.txtRuc.TabIndex = 0
        Me.txtRuc.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtRuc.TypeData = JMControls.Enums.TypeDataEnum.Numeric
        Me.txtRuc.UnderlinedStyle = False
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(14, 45)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(37, 17)
        Me.label1.TabIndex = 0
        Me.label1.Text = "Ruc:"
        '
        'errorProvider1
        '
        Me.errorProvider1.ContainerControl = Me
        '
        'backgroundWorker1
        '
        '
        'label21
        '
        Me.label21.AutoSize = True
        Me.label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.label21.Location = New System.Drawing.Point(30, 4)
        Me.label21.Name = "label21"
        Me.label21.Size = New System.Drawing.Size(235, 22)
        Me.label21.TabIndex = 1
        Me.label21.Text = "CONFIGURACIÓN EMISOR"
        '
        'panel2
        '
        Me.panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.panel2.Controls.Add(Me.label21)
        Me.panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.panel2.Location = New System.Drawing.Point(0, 0)
        Me.panel2.Name = "panel2"
        Me.panel2.Size = New System.Drawing.Size(685, 37)
        Me.panel2.TabIndex = 6
        '
        'groupBoxLiner3
        '
        Me.groupBoxLiner3.BackColor = System.Drawing.SystemColors.Control
        Me.groupBoxLiner3.BorderColor = System.Drawing.Color.Black
        Me.groupBoxLiner3.BorderRadius = 8
        Me.groupBoxLiner3.BorderThickness = 1
        Me.groupBoxLiner3.Controls.Add(Me.logoPictureBox1)
        Me.groupBoxLiner3.Controls.Add(Me.rjButton3)
        Me.groupBoxLiner3.Controls.Add(Me.rjButton4)
        Me.groupBoxLiner3.Location = New System.Drawing.Point(34, 253)
        Me.groupBoxLiner3.Name = "groupBoxLiner3"
        Me.groupBoxLiner3.Size = New System.Drawing.Size(527, 211)
        Me.groupBoxLiner3.TabIndex = 0
        Me.groupBoxLiner3.TabStop = False
        Me.groupBoxLiner3.Text = "Logo para ticket"
        '
        'logoPictureBox1
        '
        Me.logoPictureBox1.BackColor = System.Drawing.Color.White
        Me.logoPictureBox1.Location = New System.Drawing.Point(212, 34)
        Me.logoPictureBox1.Name = "logoPictureBox1"
        Me.logoPictureBox1.Size = New System.Drawing.Size(300, 144)
        Me.logoPictureBox1.TabIndex = 1
        Me.logoPictureBox1.TabStop = False
        '
        'rjButton3
        '
        Me.rjButton3.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(164, Byte), Integer), CType(CType(183, Byte), Integer))
        Me.rjButton3.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(164, Byte), Integer), CType(CType(183, Byte), Integer))
        Me.rjButton3.BorderColor = System.Drawing.Color.Red
        Me.rjButton3.BorderRadius = 12
        Me.rjButton3.BorderSize = 1
        Me.rjButton3.FlatAppearance.BorderSize = 0
        Me.rjButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rjButton3.ForeColor = System.Drawing.Color.White
        Me.rjButton3.Location = New System.Drawing.Point(15, 83)
        Me.rjButton3.Name = "rjButton3"
        Me.rjButton3.Size = New System.Drawing.Size(173, 33)
        Me.rjButton3.TabIndex = 0
        Me.rjButton3.Text = "Borrar imagen"
        Me.rjButton3.TextColor = System.Drawing.Color.White
        Me.rjButton3.UseVisualStyleBackColor = False
        '
        'rjButton4
        '
        Me.rjButton4.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(164, Byte), Integer), CType(CType(183, Byte), Integer))
        Me.rjButton4.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(164, Byte), Integer), CType(CType(183, Byte), Integer))
        Me.rjButton4.BorderColor = System.Drawing.Color.Red
        Me.rjButton4.BorderRadius = 12
        Me.rjButton4.BorderSize = 1
        Me.rjButton4.FlatAppearance.BorderSize = 0
        Me.rjButton4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rjButton4.ForeColor = System.Drawing.Color.White
        Me.rjButton4.Location = New System.Drawing.Point(15, 34)
        Me.rjButton4.Name = "rjButton4"
        Me.rjButton4.Size = New System.Drawing.Size(173, 33)
        Me.rjButton4.TabIndex = 0
        Me.rjButton4.Text = "Cargar imagen"
        Me.rjButton4.TextColor = System.Drawing.Color.White
        Me.rjButton4.UseVisualStyleBackColor = False
        '
        'lblState
        '
        Me.lblState.AutoSize = True
        Me.lblState.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblState.ForeColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.lblState.Location = New System.Drawing.Point(208, 13)
        Me.lblState.Name = "lblState"
        Me.lblState.Size = New System.Drawing.Size(225, 19)
        Me.lblState.TabIndex = 1
        Me.lblState.Text = "Guardado exitosamente...."
        Me.lblState.Visible = False
        '
        'rjButton1
        '
        Me.rjButton1.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(164, Byte), Integer), CType(CType(183, Byte), Integer))
        Me.rjButton1.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(164, Byte), Integer), CType(CType(183, Byte), Integer))
        Me.rjButton1.BorderColor = System.Drawing.Color.Red
        Me.rjButton1.BorderRadius = 12
        Me.rjButton1.BorderSize = 1
        Me.rjButton1.FlatAppearance.BorderSize = 0
        Me.rjButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rjButton1.ForeColor = System.Drawing.Color.White
        Me.rjButton1.Location = New System.Drawing.Point(15, 34)
        Me.rjButton1.Name = "rjButton1"
        Me.rjButton1.Size = New System.Drawing.Size(173, 33)
        Me.rjButton1.TabIndex = 0
        Me.rjButton1.Text = "Cargar imagen"
        Me.rjButton1.TextColor = System.Drawing.Color.White
        Me.rjButton1.UseVisualStyleBackColor = False
        '
        'Accep_Button
        '
        Me.Accep_Button.BackColor = System.Drawing.Color.MediumSlateBlue
        Me.Accep_Button.BackgroundColor = System.Drawing.Color.MediumSlateBlue
        Me.Accep_Button.BorderColor = System.Drawing.Color.PaleVioletRed
        Me.Accep_Button.BorderRadius = 8
        Me.Accep_Button.BorderSize = 0
        Me.Accep_Button.FlatAppearance.BorderSize = 0
        Me.Accep_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Accep_Button.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Accep_Button.ForeColor = System.Drawing.Color.White
        Me.Accep_Button.Location = New System.Drawing.Point(7, 3)
        Me.Accep_Button.Name = "Accep_Button"
        Me.Accep_Button.Size = New System.Drawing.Size(195, 37)
        Me.Accep_Button.TabIndex = 0
        Me.Accep_Button.Text = "Guardar..."
        Me.Accep_Button.TextColor = System.Drawing.Color.White
        Me.Accep_Button.UseVisualStyleBackColor = False
        '
        'logoPictureBox
        '
        Me.logoPictureBox.BackColor = System.Drawing.Color.White
        Me.logoPictureBox.Location = New System.Drawing.Point(212, 34)
        Me.logoPictureBox.Name = "logoPictureBox"
        Me.logoPictureBox.Size = New System.Drawing.Size(300, 144)
        Me.logoPictureBox.TabIndex = 1
        Me.logoPictureBox.TabStop = False
        '
        'rjButton2
        '
        Me.rjButton2.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(164, Byte), Integer), CType(CType(183, Byte), Integer))
        Me.rjButton2.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(164, Byte), Integer), CType(CType(183, Byte), Integer))
        Me.rjButton2.BorderColor = System.Drawing.Color.Red
        Me.rjButton2.BorderRadius = 12
        Me.rjButton2.BorderSize = 1
        Me.rjButton2.FlatAppearance.BorderSize = 0
        Me.rjButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rjButton2.ForeColor = System.Drawing.Color.White
        Me.rjButton2.Location = New System.Drawing.Point(15, 83)
        Me.rjButton2.Name = "rjButton2"
        Me.rjButton2.Size = New System.Drawing.Size(173, 33)
        Me.rjButton2.TabIndex = 0
        Me.rjButton2.Text = "Borrar imagen"
        Me.rjButton2.TextColor = System.Drawing.Color.White
        Me.rjButton2.UseVisualStyleBackColor = False
        '
        'groupBoxLiner2
        '
        Me.groupBoxLiner2.BackColor = System.Drawing.SystemColors.Control
        Me.groupBoxLiner2.BorderColor = System.Drawing.Color.Black
        Me.groupBoxLiner2.BorderRadius = 8
        Me.groupBoxLiner2.BorderThickness = 1
        Me.groupBoxLiner2.Controls.Add(Me.logoPictureBox)
        Me.groupBoxLiner2.Controls.Add(Me.rjButton2)
        Me.groupBoxLiner2.Controls.Add(Me.rjButton1)
        Me.groupBoxLiner2.Location = New System.Drawing.Point(34, 29)
        Me.groupBoxLiner2.Name = "groupBoxLiner2"
        Me.groupBoxLiner2.Size = New System.Drawing.Size(527, 211)
        Me.groupBoxLiner2.TabIndex = 0
        Me.groupBoxLiner2.TabStop = False
        Me.groupBoxLiner2.Text = "Logo para PDF"
        '
        'panel1
        '
        Me.panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.panel1.Controls.Add(Me.lblState)
        Me.panel1.Controls.Add(Me.Accep_Button)
        Me.panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panel1.Location = New System.Drawing.Point(0, 607)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(685, 44)
        Me.panel1.TabIndex = 5
        '
        'tabPageEx3
        '
        Me.tabPageEx3.AutoScroll = True
        Me.tabPageEx3.BackColor = System.Drawing.Color.White
        Me.tabPageEx3.Controls.Add(Me.label20)
        Me.tabPageEx3.Controls.Add(Me.panel3)
        Me.tabPageEx3.Controls.Add(Me.groupBoxLiner1)
        Me.tabPageEx3.Controls.Add(Me.altoNumericUpDown1)
        Me.tabPageEx3.Controls.Add(Me.label17)
        Me.tabPageEx3.Controls.Add(Me.tipoAmbienteComboBox)
        Me.tabPageEx3.Controls.Add(Me.label10)
        Me.tabPageEx3.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.tabPageEx3.ImageLocation = New System.Drawing.Point(15, 5)
        Me.tabPageEx3.IsClosable = False
        Me.tabPageEx3.Location = New System.Drawing.Point(1, 41)
        Me.tabPageEx3.Name = "tabPageEx3"
        Me.tabPageEx3.Size = New System.Drawing.Size(615, 528)
        Me.tabPageEx3.TabIndex = 2
        Me.tabPageEx3.Text = "Ambiente y firma dijital"
        '
        'panel4
        '
        Me.panel4.BackColor = System.Drawing.Color.LightSteelBlue
        Me.panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.panel4.Location = New System.Drawing.Point(0, 651)
        Me.panel4.Name = "panel4"
        Me.panel4.Size = New System.Drawing.Size(685, 10)
        Me.panel4.TabIndex = 7
        '
        'jmTabControl1
        '
        Me.jmTabControl1.AllowDrop = True
        Me.jmTabControl1.BackgroundHatcher.HatchType = System.Drawing.Drawing2D.HatchStyle.DashedVertical
        Me.jmTabControl1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.jmTabControl1.Controls.Add(Me.tabPageEx1)
        Me.jmTabControl1.Controls.Add(Me.tabPageEx2)
        Me.jmTabControl1.Controls.Add(Me.tabPageEx3)
        Me.jmTabControl1.Controls.Add(Me.tabPageEx4)
        Me.jmTabControl1.Dock = System.Windows.Forms.DockStyle.Left
        Me.jmTabControl1.IsCaptionVisible = False
        Me.jmTabControl1.IsDrawHeader = False
        Me.jmTabControl1.IsDrawTabSeparator = True
        Me.jmTabControl1.ItemSize = New System.Drawing.Size(300, 35)
        Me.jmTabControl1.Location = New System.Drawing.Point(0, 37)
        Me.jmTabControl1.Name = "jmTabControl1"
        Me.jmTabControl1.SelectedIndex = 2
        Me.jmTabControl1.Size = New System.Drawing.Size(617, 570)
        Me.jmTabControl1.TabBorderColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(65, Byte), Integer))
        Me.jmTabControl1.TabGradient.ColorEnd = System.Drawing.Color.MediumSlateBlue
        Me.jmTabControl1.TabGradient.ColorStart = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(201, Byte), Integer))
        Me.jmTabControl1.TabGradient.TabPageSelectedTextColor = System.Drawing.Color.White
        Me.jmTabControl1.TabIndex = 4
        Me.jmTabControl1.TabStyles = JMControls.TabControlGRD.JMTabControl.TabStyle.OfficeXP
        Me.jmTabControl1.UpDownStyle = JMControls.TabControlGRD.JMTabControl.UpDown32Style.KRBBlue
        '
        'tabPageEx4
        '
        Me.tabPageEx4.AutoScroll = True
        Me.tabPageEx4.BackColor = System.Drawing.Color.White
        Me.tabPageEx4.Controls.Add(Me.groupBoxLiner3)
        Me.tabPageEx4.Controls.Add(Me.groupBoxLiner2)
        Me.tabPageEx4.Font = New System.Drawing.Font("Arial", 10.0!)
        Me.tabPageEx4.ImageLocation = New System.Drawing.Point(15, 5)
        Me.tabPageEx4.IsClosable = False
        Me.tabPageEx4.Location = New System.Drawing.Point(1, 36)
        Me.tabPageEx4.Name = "tabPageEx4"
        Me.tabPageEx4.Size = New System.Drawing.Size(615, 499)
        Me.tabPageEx4.TabIndex = 3
        Me.tabPageEx4.Text = "Perfiles visuales"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(106, 454)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(189, 20)
        Me.Label5.TabIndex = 40
        Me.Label5.Text = "Etiqueta régimen RIMPE:"
        '
        'txtRegimenRIMPE
        '
        Me.txtRegimenRIMPE.ForeColor = System.Drawing.Color.FromArgb(CType(CType(34, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(38, Byte), Integer))
        Me.txtRegimenRIMPE.Location = New System.Drawing.Point(106, 483)
        Me.txtRegimenRIMPE.Name = "txtRegimenRIMPE"
        Me.txtRegimenRIMPE.Size = New System.Drawing.Size(408, 23)
        Me.txtRegimenRIMPE.TabIndex = 36
        Me.txtRegimenRIMPE.Text = "CONTRIBUYENTE RÉGIMEN MICROEMPRESAS"
        '
        'MyCommerceForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(685, 661)
        Me.Controls.Add(Me.jmTabControl1)
        Me.Controls.Add(Me.panel2)
        Me.Controls.Add(Me.panel1)
        Me.Controls.Add(Me.panel4)
        Me.Name = "MyCommerceForm"
        Me.Text = "MyCommerceForm"
        Me.panel3.ResumeLayout(False)
        Me.expandCollapsePanel1.ResumeLayout(False)
        Me.expandCollapsePanel1.PerformLayout()
        Me.tabPageEx2.ResumeLayout(False)
        Me.tabPageEx2.PerformLayout()
        Me.groupBoxLiner1.ResumeLayout(False)
        Me.groupBoxLiner1.PerformLayout()
        Me.tabPageEx1.ResumeLayout(False)
        Me.tabPageEx1.PerformLayout()
        CType(Me.errorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panel2.ResumeLayout(False)
        Me.panel2.PerformLayout()
        Me.groupBoxLiner3.ResumeLayout(False)
        CType(Me.logoPictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.logoPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupBoxLiner2.ResumeLayout(False)
        Me.panel1.ResumeLayout(False)
        Me.panel1.PerformLayout()
        Me.tabPageEx3.ResumeLayout(False)
        Me.tabPageEx3.PerformLayout()
        Me.jmTabControl1.ResumeLayout(False)
        Me.tabPageEx4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents panel3 As Panel
    Private WithEvents txtHuella As JMControls.Controls.TextBoxRounded
    Private WithEvents label18 As Label
    Private WithEvents expandCollapsePanel1 As JMControls.ExpandCollapsePanel.ExpandCollapsePanel
    Private WithEvents label16 As Label
    Private WithEvents TokenListComboBox As JMControls.Controls.RJComboBox
    Private WithEvents label17 As Label
    Private WithEvents tabPageEx2 As JMControls.TabControlGRD.TabPageEx
    Friend WithEvents cmbTypeBusiness As JMControls.Controls.RJComboBox
    Friend WithEvents txtRegimenMicro As TextBox
    Friend WithEvents txtAgentRetenNum As TextBox
    Friend WithEvents txtNumResolucion As TextBox
    Friend WithEvents ContabiliteChecBox As CheckBox
    Friend WithEvents label14 As Label
    Friend WithEvents label13 As Label
    Friend WithEvents label12 As Label
    Friend WithEvents ContabiliteLabel As Label
    Friend WithEvents Label11 As Label
    Private WithEvents altoNumericUpDown1 As JMControls.Controls.AltoNumericUpDown
    Private WithEvents rjRadioButton3 As JMControls.Controls.RJRadioButton
    Private WithEvents rjRadioButton2 As JMControls.Controls.RJRadioButton
    Private WithEvents rjRadioButton1 As JMControls.Controls.RJRadioButton
    Private WithEvents groupBoxLiner1 As JMControls.Controls.GroupBoxLiner
    Private WithEvents label20 As Label
    Private WithEvents tipoAmbienteComboBox As JMControls.Controls.RJComboBox
    Private WithEvents label10 As Label
    Private WithEvents timer1 As Timer
    Private WithEvents tabPageEx1 As JMControls.TabControlGRD.TabPageEx
    Private WithEvents lblRucCount As Label
    Private WithEvents txtDirMatriz As JMControls.Controls.RJTextBox
    Private WithEvents label4 As Label
    Private WithEvents txtCompany As JMControls.Controls.RJTextBox
    Private WithEvents label8 As Label
    Private WithEvents txtNomComercial As JMControls.Controls.RJTextBox
    Private WithEvents label3 As Label
    Private WithEvents txtRazonSocial As JMControls.Controls.RJTextBox
    Private WithEvents label2 As Label
    Private WithEvents txtRuc As JMControls.Controls.RJTextBox
    Private WithEvents label1 As Label
    Private WithEvents errorProvider1 As ErrorProvider
    Private WithEvents panel2 As Panel
    Private WithEvents label21 As Label
    Private WithEvents panel1 As Panel
    Private WithEvents lblState As Label
    Private WithEvents Accep_Button As JMControls.Controls.RJButton
    Private WithEvents panel4 As Panel
    Private WithEvents jmTabControl1 As JMControls.TabControlGRD.JMTabControl
    Private WithEvents tabPageEx3 As JMControls.TabControlGRD.TabPageEx
    Private WithEvents tabPageEx4 As JMControls.TabControlGRD.TabPageEx
    Private WithEvents groupBoxLiner3 As JMControls.Controls.GroupBoxLiner
    Private WithEvents logoPictureBox1 As PictureBox
    Private WithEvents rjButton3 As JMControls.Controls.RJButton
    Private WithEvents rjButton4 As JMControls.Controls.RJButton
    Private WithEvents groupBoxLiner2 As JMControls.Controls.GroupBoxLiner
    Private WithEvents logoPictureBox As PictureBox
    Private WithEvents rjButton2 As JMControls.Controls.RJButton
    Private WithEvents rjButton1 As JMControls.Controls.RJButton
    Private WithEvents backgroundWorker1 As System.ComponentModel.BackgroundWorker
    Private WithEvents circularProgressBar1 As CircularProgressBar.CircularProgressBar
    Friend WithEvents txtRegimenRIMPE As TextBox
    Friend WithEvents Label5 As Label
End Class
