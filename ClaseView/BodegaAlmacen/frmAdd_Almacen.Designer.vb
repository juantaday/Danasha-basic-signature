<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdd_Almacen
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
        Me.pnlEntrada = New System.Windows.Forms.Panel()
        Me.chkEsSucursalRemota = New System.Windows.Forms.CheckBox()
        Me.txtCiudadSucursal = New System.Windows.Forms.TextBox()
        Me.lblEsSucursalRemota = New System.Windows.Forms.Label()
        Me.lblCiudadSucursal = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtCodigoEstab = New JMControls.Controls.RJTextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TypoBodegaComboBox = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtCheque = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.txtFecha_Apert = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.telefono3TextBox = New System.Windows.Forms.TextBox()
        Me.telefono2Text = New System.Windows.Forms.TextBox()
        Me.telefono1Text = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.brnAddPesponsable = New System.Windows.Forms.Button()
        Me.txtresponsable = New System.Windows.Forms.TextBox()
        Me.DireccionText = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DescripcionBodegaText = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.NomBodegaText = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtidbodega = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ErrorIcono = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.datalistado = New System.Windows.Forms.DataGridView()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.PnlListado = New System.Windows.Forms.Panel()
        Me.btnElimina = New System.Windows.Forms.Button()
        Me.btnNuevo = New System.Windows.Forms.Button()
        Me.btnModifica = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.pnlEntrada.SuspendLayout()
        CType(Me.ErrorIcono, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.datalistado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PnlListado.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlEntrada
        '
        Me.pnlEntrada.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlEntrada.CausesValidation = False
        Me.pnlEntrada.Controls.Add(Me.chkEsSucursalRemota)
        Me.pnlEntrada.Controls.Add(Me.txtCiudadSucursal)
        Me.pnlEntrada.Controls.Add(Me.lblEsSucursalRemota)
        Me.pnlEntrada.Controls.Add(Me.lblCiudadSucursal)
        Me.pnlEntrada.Controls.Add(Me.Label10)
        Me.pnlEntrada.Controls.Add(Me.txtCodigoEstab)
        Me.pnlEntrada.Controls.Add(Me.Label8)
        Me.pnlEntrada.Controls.Add(Me.TypoBodegaComboBox)
        Me.pnlEntrada.Controls.Add(Me.Label7)
        Me.pnlEntrada.Controls.Add(Me.Button1)
        Me.pnlEntrada.Controls.Add(Me.txtCheque)
        Me.pnlEntrada.Controls.Add(Me.btnCancel)
        Me.pnlEntrada.Controls.Add(Me.Label6)
        Me.pnlEntrada.Controls.Add(Me.btnAceptar)
        Me.pnlEntrada.Controls.Add(Me.txtFecha_Apert)
        Me.pnlEntrada.Controls.Add(Me.Label5)
        Me.pnlEntrada.Controls.Add(Me.telefono3TextBox)
        Me.pnlEntrada.Controls.Add(Me.telefono2Text)
        Me.pnlEntrada.Controls.Add(Me.telefono1Text)
        Me.pnlEntrada.Controls.Add(Me.Label4)
        Me.pnlEntrada.Controls.Add(Me.brnAddPesponsable)
        Me.pnlEntrada.Controls.Add(Me.txtresponsable)
        Me.pnlEntrada.Controls.Add(Me.DireccionText)
        Me.pnlEntrada.Controls.Add(Me.Label3)
        Me.pnlEntrada.Controls.Add(Me.DescripcionBodegaText)
        Me.pnlEntrada.Controls.Add(Me.Label2)
        Me.pnlEntrada.Controls.Add(Me.NomBodegaText)
        Me.pnlEntrada.Controls.Add(Me.Label1)
        Me.pnlEntrada.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlEntrada.Location = New System.Drawing.Point(12, 5)
        Me.pnlEntrada.Name = "pnlEntrada"
        Me.pnlEntrada.Size = New System.Drawing.Size(492, 560)
        Me.pnlEntrada.TabIndex = 0
        '
        'chkEsSucursalRemota
        '
        Me.chkEsSucursalRemota.AutoSize = True
        Me.chkEsSucursalRemota.Location = New System.Drawing.Point(160, 36)
        Me.chkEsSucursalRemota.Name = "chkEsSucursalRemota"
        Me.chkEsSucursalRemota.Size = New System.Drawing.Size(39, 21)
        Me.chkEsSucursalRemota.TabIndex = 25
        Me.chkEsSucursalRemota.Text = "Sí"
        Me.chkEsSucursalRemota.UseVisualStyleBackColor = True
        '
        'txtCiudadSucursal
        '
        Me.txtCiudadSucursal.Enabled = False
        Me.txtCiudadSucursal.Location = New System.Drawing.Point(162, 75)
        Me.txtCiudadSucursal.Name = "txtCiudadSucursal"
        Me.txtCiudadSucursal.Size = New System.Drawing.Size(305, 23)
        Me.txtCiudadSucursal.TabIndex = 28
        '
        'lblEsSucursalRemota
        '
        Me.lblEsSucursalRemota.AutoSize = True
        Me.lblEsSucursalRemota.Location = New System.Drawing.Point(12, 38)
        Me.lblEsSucursalRemota.Name = "lblEsSucursalRemota"
        Me.lblEsSucursalRemota.Size = New System.Drawing.Size(115, 17)
        Me.lblEsSucursalRemota.TabIndex = 26
        Me.lblEsSucursalRemota.Text = "Sucursal remota:"
        '
        'lblCiudadSucursal
        '
        Me.lblCiudadSucursal.AutoSize = True
        Me.lblCiudadSucursal.Location = New System.Drawing.Point(12, 78)
        Me.lblCiudadSucursal.Name = "lblCiudadSucursal"
        Me.lblCiudadSucursal.Size = New System.Drawing.Size(142, 17)
        Me.lblCiudadSucursal.TabIndex = 27
        Me.lblCiudadSucursal.Text = "Ciudad de Ubicación:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(57, 169)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(158, 17)
        Me.Label10.TabIndex = 24
        Me.Label10.Text = "Código establecimiento:"
        '
        'txtCodigoEstab
        '
        Me.txtCodigoEstab.BackColor = System.Drawing.SystemColors.Window
        Me.txtCodigoEstab.BorderColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.txtCodigoEstab.BorderFocusColor = System.Drawing.Color.FromArgb(CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(40, Byte), Integer))
        Me.txtCodigoEstab.BorderRadius = 0
        Me.txtCodigoEstab.BorderThickness = 1
        Me.txtCodigoEstab.CharacterCasin = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCodigoEstab.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.txtCodigoEstab.DecimalPosition = 2
        Me.txtCodigoEstab.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.5!)
        Me.txtCodigoEstab.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtCodigoEstab.Location = New System.Drawing.Point(222, 162)
        Me.txtCodigoEstab.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCodigoEstab.MaxLength = 3
        Me.txtCodigoEstab.Multiline = False
        Me.txtCodigoEstab.Name = "txtCodigoEstab"
        Me.txtCodigoEstab.Padding = New System.Windows.Forms.Padding(10, 7, 10, 7)
        Me.txtCodigoEstab.PasswordChar = False
        Me.txtCodigoEstab.PlaceHolderColor = System.Drawing.Color.DarkGray
        Me.txtCodigoEstab.PlaceHolderText = ""
        Me.txtCodigoEstab.ReadOnly = False
        Me.txtCodigoEstab.SelectionLength = 0
        Me.txtCodigoEstab.Size = New System.Drawing.Size(132, 31)
        Me.txtCodigoEstab.TabIndex = 2
        Me.txtCodigoEstab.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtCodigoEstab.TypeData = JMControls.Enums.TypeDataEnum.Numeric
        Me.txtCodigoEstab.UnderlinedStyle = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 9)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(40, 17)
        Me.Label8.TabIndex = 22
        Me.Label8.Text = "Tipo:"
        '
        'TypoBodegaComboBox
        '
        Me.TypoBodegaComboBox.FormattingEnabled = True
        Me.TypoBodegaComboBox.Location = New System.Drawing.Point(72, 7)
        Me.TypoBodegaComboBox.Name = "TypoBodegaComboBox"
        Me.TypoBodegaComboBox.Size = New System.Drawing.Size(235, 24)
        Me.TypoBodegaComboBox.TabIndex = 21
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 445)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(197, 17)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "Atorizado para emitir cheques"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(330, 464)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(58, 27)
        Me.Button1.TabIndex = 18
        Me.Button1.Text = "...."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtCheque
        '
        Me.txtCheque.Location = New System.Drawing.Point(12, 466)
        Me.txtCheque.Name = "txtCheque"
        Me.txtCheque.ReadOnly = True
        Me.txtCheque.Size = New System.Drawing.Size(315, 23)
        Me.txtCheque.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.txtCheque, "Al momento de adquirir productos será la persona autorizado a emitir queches al p" &
        "roveedor")
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackgroundImage = Global.DanashaBasicSignature.My.Resources.Resources.Action_Cancel_32x32
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCancel.Location = New System.Drawing.Point(430, 502)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(50, 50)
        Me.btnCancel.TabIndex = 16
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(355, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(110, 15)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Fecha de apertura:"
        '
        'btnAceptar
        '
        Me.btnAceptar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAceptar.BackgroundImage = Global.DanashaBasicSignature.My.Resources.Resources.Save2_icon_48
        Me.btnAceptar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAceptar.Location = New System.Drawing.Point(375, 502)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(50, 50)
        Me.btnAceptar.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.btnAceptar, "Guardar informacion")
        Me.btnAceptar.UseVisualStyleBackColor = False
        '
        'txtFecha_Apert
        '
        Me.txtFecha_Apert.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFecha_Apert.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFecha_Apert.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.txtFecha_Apert.Location = New System.Drawing.Point(368, 35)
        Me.txtFecha_Apert.Name = "txtFecha_Apert"
        Me.txtFecha_Apert.Size = New System.Drawing.Size(97, 21)
        Me.txtFecha_Apert.TabIndex = 14
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 386)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(117, 17)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Persona a cargo:"
        '
        'telefono3TextBox
        '
        Me.telefono3TextBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.telefono3TextBox.Location = New System.Drawing.Point(366, 357)
        Me.telefono3TextBox.MaxLength = 12
        Me.telefono3TextBox.Name = "telefono3TextBox"
        Me.telefono3TextBox.Size = New System.Drawing.Size(107, 23)
        Me.telefono3TextBox.TabIndex = 7
        '
        'telefono2Text
        '
        Me.telefono2Text.Location = New System.Drawing.Point(235, 357)
        Me.telefono2Text.MaxLength = 12
        Me.telefono2Text.Name = "telefono2Text"
        Me.telefono2Text.Size = New System.Drawing.Size(119, 23)
        Me.telefono2Text.TabIndex = 6
        '
        'telefono1Text
        '
        Me.telefono1Text.Location = New System.Drawing.Point(88, 357)
        Me.telefono1Text.MaxLength = 12
        Me.telefono1Text.Name = "telefono1Text"
        Me.telefono1Text.Size = New System.Drawing.Size(127, 23)
        Me.telefono1Text.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 355)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 17)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Teléfeno:"
        '
        'brnAddPesponsable
        '
        Me.brnAddPesponsable.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.brnAddPesponsable.Location = New System.Drawing.Point(416, 406)
        Me.brnAddPesponsable.Name = "brnAddPesponsable"
        Me.brnAddPesponsable.Size = New System.Drawing.Size(57, 25)
        Me.brnAddPesponsable.TabIndex = 7
        Me.brnAddPesponsable.Text = "...."
        Me.brnAddPesponsable.UseVisualStyleBackColor = True
        '
        'txtresponsable
        '
        Me.txtresponsable.Location = New System.Drawing.Point(12, 408)
        Me.txtresponsable.Name = "txtresponsable"
        Me.txtresponsable.ReadOnly = True
        Me.txtresponsable.Size = New System.Drawing.Size(395, 23)
        Me.txtresponsable.TabIndex = 6
        '
        'DireccionText
        '
        Me.DireccionText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DireccionText.Location = New System.Drawing.Point(109, 208)
        Me.DireccionText.MaxLength = 500
        Me.DireccionText.Multiline = True
        Me.DireccionText.Name = "DireccionText"
        Me.DireccionText.Size = New System.Drawing.Size(364, 61)
        Me.DireccionText.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 289)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 17)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Observación:"
        '
        'DescripcionBodegaText
        '
        Me.DescripcionBodegaText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DescripcionBodegaText.Location = New System.Drawing.Point(126, 286)
        Me.DescripcionBodegaText.MaxLength = 200
        Me.DescripcionBodegaText.Multiline = True
        Me.DescripcionBodegaText.Name = "DescripcionBodegaText"
        Me.DescripcionBodegaText.Size = New System.Drawing.Size(347, 55)
        Me.DescripcionBodegaText.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 208)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Dirección:"
        '
        'NomBodegaText
        '
        Me.NomBodegaText.Location = New System.Drawing.Point(12, 126)
        Me.NomBodegaText.MaxLength = 20
        Me.NomBodegaText.Name = "NomBodegaText"
        Me.NomBodegaText.Size = New System.Drawing.Size(340, 23)
        Me.NomBodegaText.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 105)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(224, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Nombre: Local, Sucursal, Bodega:"
        '
        'txtidbodega
        '
        Me.txtidbodega.Location = New System.Drawing.Point(527, 5)
        Me.txtidbodega.Name = "txtidbodega"
        Me.txtidbodega.Size = New System.Drawing.Size(21, 20)
        Me.txtidbodega.TabIndex = 13
        Me.txtidbodega.Text = "0"
        Me.txtidbodega.Visible = False
        '
        'ErrorIcono
        '
        Me.ErrorIcono.ContainerControl = Me
        '
        'datalistado
        '
        Me.datalistado.AllowUserToAddRows = False
        Me.datalistado.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.datalistado.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.datalistado.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.datalistado.BackgroundColor = System.Drawing.Color.White
        Me.datalistado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.datalistado.DefaultCellStyle = DataGridViewCellStyle2
        Me.datalistado.Location = New System.Drawing.Point(3, 12)
        Me.datalistado.MultiSelect = False
        Me.datalistado.Name = "datalistado"
        Me.datalistado.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.datalistado.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.datalistado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.datalistado.Size = New System.Drawing.Size(537, 357)
        Me.datalistado.TabIndex = 9
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Text = "NotifyIcon1"
        Me.NotifyIcon1.Visible = True
        '
        'PnlListado
        '
        Me.PnlListado.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PnlListado.Controls.Add(Me.btnElimina)
        Me.PnlListado.Controls.Add(Me.datalistado)
        Me.PnlListado.Controls.Add(Me.btnNuevo)
        Me.PnlListado.Controls.Add(Me.btnModifica)
        Me.PnlListado.Location = New System.Drawing.Point(539, 59)
        Me.PnlListado.Name = "PnlListado"
        Me.PnlListado.Size = New System.Drawing.Size(547, 407)
        Me.PnlListado.TabIndex = 17
        '
        'btnElimina
        '
        Me.btnElimina.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnElimina.BackgroundImage = Global.DanashaBasicSignature.My.Resources.Resources.Action_Delete_16x16
        Me.btnElimina.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnElimina.Enabled = False
        Me.btnElimina.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnElimina.Location = New System.Drawing.Point(162, 375)
        Me.btnElimina.Name = "btnElimina"
        Me.btnElimina.Size = New System.Drawing.Size(68, 28)
        Me.btnElimina.TabIndex = 16
        Me.btnElimina.Text = "Eliminar"
        Me.btnElimina.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnElimina.UseVisualStyleBackColor = True
        '
        'btnNuevo
        '
        Me.btnNuevo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNuevo.BackgroundImage = Global.DanashaBasicSignature.My.Resources.Resources.nuevo_16
        Me.btnNuevo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnNuevo.Image = Global.DanashaBasicSignature.My.Resources.Resources.nuevo_16
        Me.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNuevo.Location = New System.Drawing.Point(14, 375)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(68, 28)
        Me.btnNuevo.TabIndex = 15
        Me.btnNuevo.Text = "Nuevo"
        Me.btnNuevo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnNuevo.UseVisualStyleBackColor = True
        '
        'btnModifica
        '
        Me.btnModifica.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnModifica.BackgroundImage = Global.DanashaBasicSignature.My.Resources.Resources.edir_16
        Me.btnModifica.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnModifica.Enabled = False
        Me.btnModifica.Image = Global.DanashaBasicSignature.My.Resources.Resources.edir_16
        Me.btnModifica.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnModifica.Location = New System.Drawing.Point(88, 375)
        Me.btnModifica.Name = "btnModifica"
        Me.btnModifica.Size = New System.Drawing.Size(70, 28)
        Me.btnModifica.TabIndex = 14
        Me.btnModifica.Text = "Modificar"
        Me.btnModifica.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnModifica.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancelar.BackgroundImage = Global.DanashaBasicSignature.My.Resources.Resources.Exit_icon
        Me.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCancelar.Location = New System.Drawing.Point(1021, 474)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(65, 65)
        Me.btnCancelar.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(534, 27)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(352, 29)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "Listado de bodegas registradas"
        '
        'frmAdd_Almacen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1097, 594)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.pnlEntrada)
        Me.Controls.Add(Me.txtidbodega)
        Me.Controls.Add(Me.PnlListado)
        Me.Name = "frmAdd_Almacen"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Agregando punto de cargo ..(Bodegas-Sucursales- Locales Comerciales)"
        Me.pnlEntrada.ResumeLayout(False)
        Me.pnlEntrada.PerformLayout()
        CType(Me.ErrorIcono, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.datalistado, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PnlListado.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlEntrada As System.Windows.Forms.Panel
    Friend WithEvents brnAddPesponsable As System.Windows.Forms.Button
    Friend WithEvents txtresponsable As System.Windows.Forms.TextBox
    Friend WithEvents DireccionText As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DescripcionBodegaText As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents NomBodegaText As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents telefono1Text As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents txtidbodega As System.Windows.Forms.TextBox
    Friend WithEvents ErrorIcono As System.Windows.Forms.ErrorProvider
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtFecha_Apert As System.Windows.Forms.DateTimePicker
    Friend WithEvents datalistado As System.Windows.Forms.DataGridView
    Friend WithEvents btnElimina As System.Windows.Forms.Button
    Friend WithEvents btnNuevo As System.Windows.Forms.Button
    Friend WithEvents btnModifica As System.Windows.Forms.Button
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents PnlListado As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtCheque As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TypoBodegaComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents telefono3TextBox As System.Windows.Forms.TextBox
    Friend WithEvents telefono2Text As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents txtCodigoEstab As JMControls.Controls.RJTextBox
    Friend WithEvents chkEsSucursalRemota As System.Windows.Forms.CheckBox
    Friend WithEvents txtCiudadSucursal As System.Windows.Forms.TextBox
    Friend WithEvents lblEsSucursalRemota As System.Windows.Forms.Label
    Friend WithEvents lblCiudadSucursal As System.Windows.Forms.Label
End Class
