<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmList_Empleados
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmList_Empleados))
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.notificacion = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.txtIdCliente = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtEmple_Busca = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnok = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.FechaIngresoDateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.EditDetailButton = New System.Windows.Forms.Button()
        Me.EdidEmployeeCliente = New System.Windows.Forms.Button()
        Me.AddNewEmplToList = New System.Windows.Forms.Button()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.DeleteReportToButton = New System.Windows.Forms.Button()
        Me.ReportToButton = New System.Windows.Forms.Button()
        Me.deleteEmployeeButton = New System.Windows.Forms.Button()
        Me.EditCardButton = New System.Windows.Forms.Button()
        Me.TituloComboBox = New System.Windows.Forms.ComboBox()
        Me.CargoComboBox = New System.Windows.Forms.ComboBox()
        Me.SueldoNumericUpDown = New System.Windows.Forms.NumericUpDown()
        Me.ReportToTextBox = New System.Windows.Forms.TextBox()
        Me.PanelDetail = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PanelMenuDetail = New System.Windows.Forms.Panel()
        Me.CandelDeatilButton = New System.Windows.Forms.Button()
        Me.UpdateDetailButton = New System.Windows.Forms.Button()
        Me.DescriptionLabel = New System.Windows.Forms.Label()
        Me.Panelpie = New System.Windows.Forms.Panel()
        Me.PaneMenu = New System.Windows.Forms.Panel()
        Me.PanelBusq = New System.Windows.Forms.Panel()
        Me.PanelPieList = New System.Windows.Forms.Panel()
        Me.Total_listLabel = New System.Windows.Forms.Label()
        Me.PanelMenuList = New System.Windows.Forms.Panel()
        Me.adminEmployeePanel = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PanelList = New System.Windows.Forms.Panel()
        Me.PanelView = New System.Windows.Forms.Panel()
        Me.dtg = New System.Windows.Forms.DataGridView()
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.deleteContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.BorrarDeLaListaDeEmpleadosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EliminarTotaLaInformacionDeEsteEmpleadoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.SueldoNumericUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelDetail.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.PanelMenuDetail.SuspendLayout()
        Me.Panelpie.SuspendLayout()
        Me.PaneMenu.SuspendLayout()
        Me.PanelBusq.SuspendLayout()
        Me.PanelPieList.SuspendLayout()
        Me.PanelMenuList.SuspendLayout()
        Me.adminEmployeePanel.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelList.SuspendLayout()
        Me.PanelView.SuspendLayout()
        CType(Me.dtg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.deleteContextMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Location = New System.Drawing.Point(-10, 706)
        Me.lblTotal.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(57, 20)
        Me.lblTotal.TabIndex = 47
        Me.lblTotal.Text = "Label2"
        '
        'notificacion
        '
        Me.notificacion.Icon = CType(resources.GetObject("notificacion.Icon"), System.Drawing.Icon)
        Me.notificacion.Text = "NotifyIcon1"
        Me.notificacion.Visible = True
        '
        'txtIdCliente
        '
        Me.txtIdCliente.Location = New System.Drawing.Point(1186, 14)
        Me.txtIdCliente.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtIdCliente.Name = "txtIdCliente"
        Me.txtIdCliente.Size = New System.Drawing.Size(10, 26)
        Me.txtIdCliente.TabIndex = 44
        Me.txtIdCliente.Text = "0"
        Me.txtIdCliente.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(2, 2)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(111, 16)
        Me.Label3.TabIndex = 37
        Me.Label3.Text = "Buscar empleado:"
        '
        'txtEmple_Busca
        '
        Me.txtEmple_Busca.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmple_Busca.Location = New System.Drawing.Point(4, 33)
        Me.txtEmple_Busca.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtEmple_Busca.Name = "txtEmple_Busca"
        Me.txtEmple_Busca.Size = New System.Drawing.Size(288, 24)
        Me.txtEmple_Busca.TabIndex = 0
        '
        'btnok
        '
        Me.btnok.BackColor = System.Drawing.Color.Black
        Me.btnok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnok.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnok.Enabled = False
        Me.btnok.ForeColor = System.Drawing.Color.White
        Me.btnok.Location = New System.Drawing.Point(725, 0)
        Me.btnok.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnok.Name = "btnok"
        Me.btnok.Size = New System.Drawing.Size(125, 32)
        Me.btnok.TabIndex = 50
        Me.btnok.Text = "Seleccionar.."
        Me.ToolTip1.SetToolTip(Me.btnok, "Seleccionar..")
        Me.btnok.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.Black
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCancel.Dock = System.Windows.Forms.DockStyle.Right
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(850, 0)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 32)
        Me.btnCancel.TabIndex = 49
        Me.btnCancel.Text = "Cerrar"
        Me.ToolTip1.SetToolTip(Me.btnCancel, "Cancelar..")
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'FechaIngresoDateTimePicker
        '
        Me.FechaIngresoDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.FechaIngresoDateTimePicker.Location = New System.Drawing.Point(13, 249)
        Me.FechaIngresoDateTimePicker.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.FechaIngresoDateTimePicker.Name = "FechaIngresoDateTimePicker"
        Me.FechaIngresoDateTimePicker.Size = New System.Drawing.Size(136, 23)
        Me.FechaIngresoDateTimePicker.TabIndex = 54
        Me.ToolTip1.SetToolTip(Me.FechaIngresoDateTimePicker, "Fecha de ingreso")
        '
        'EditDetailButton
        '
        Me.EditDetailButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.EditDetailButton.Image =  Global.DanashaBasic.My.Resources.Resources.Detail_Employee_64
        Me.EditDetailButton.Location = New System.Drawing.Point(74, 0)
        Me.EditDetailButton.Margin = New System.Windows.Forms.Padding(5)
        Me.EditDetailButton.Name = "EditDetailButton"
        Me.EditDetailButton.Size = New System.Drawing.Size(72, 74)
        Me.EditDetailButton.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.EditDetailButton, "Editar información del empleado")
        Me.EditDetailButton.UseVisualStyleBackColor = False
        '
        'EdidEmployeeCliente
        '
        Me.EdidEmployeeCliente.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.EdidEmployeeCliente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.EdidEmployeeCliente.Cursor = System.Windows.Forms.Cursors.Default
        Me.EdidEmployeeCliente.Dock = System.Windows.Forms.DockStyle.Left
        Me.EdidEmployeeCliente.Enabled = False
        Me.EdidEmployeeCliente.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EdidEmployeeCliente.ForeColor = System.Drawing.Color.Blue
        Me.EdidEmployeeCliente.Image =  Global.DanashaBasic.My.Resources.Resources.edid_pencil_64
        Me.EdidEmployeeCliente.Location = New System.Drawing.Point(72, 0)
        Me.EdidEmployeeCliente.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.EdidEmployeeCliente.Name = "EdidEmployeeCliente"
        Me.EdidEmployeeCliente.Size = New System.Drawing.Size(64, 73)
        Me.EdidEmployeeCliente.TabIndex = 40
        Me.EdidEmployeeCliente.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.EdidEmployeeCliente, "Editar información personal")
        Me.EdidEmployeeCliente.UseVisualStyleBackColor = False
        '
        'AddNewEmplToList
        '
        Me.AddNewEmplToList.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.AddNewEmplToList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.AddNewEmplToList.Dock = System.Windows.Forms.DockStyle.Left
        Me.AddNewEmplToList.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AddNewEmplToList.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.AddNewEmplToList.Image =  Global.DanashaBasic.My.Resources.Resources.New_green_64
        Me.AddNewEmplToList.Location = New System.Drawing.Point(0, 0)
        Me.AddNewEmplToList.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.AddNewEmplToList.Name = "AddNewEmplToList"
        Me.AddNewEmplToList.Size = New System.Drawing.Size(72, 73)
        Me.AddNewEmplToList.TabIndex = 35
        Me.AddNewEmplToList.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.AddNewEmplToList, "Agregar Nuevos Usuarios")
        Me.AddNewEmplToList.UseVisualStyleBackColor = False
        '
        'btnBuscar
        '
        Me.btnBuscar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBuscar.BackColor = System.Drawing.Color.White
        Me.btnBuscar.BackgroundImage =  Global.DanashaBasic.My.Resources.Resources.zoom_Grin_24
        Me.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBuscar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuscar.ForeColor = System.Drawing.Color.White
        Me.btnBuscar.Location = New System.Drawing.Point(295, 21)
        Me.btnBuscar.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(49, 38)
        Me.btnBuscar.TabIndex = 38
        Me.ToolTip1.SetToolTip(Me.btnBuscar, "Buscar....")
        Me.btnBuscar.UseVisualStyleBackColor = False
        '
        'DeleteReportToButton
        '
        Me.DeleteReportToButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.DeleteReportToButton.Dock = System.Windows.Forms.DockStyle.Top
        Me.DeleteReportToButton.ForeColor = System.Drawing.Color.White
        Me.DeleteReportToButton.Image =  Global.DanashaBasic.My.Resources.Resources.Delete_cliente_48
        Me.DeleteReportToButton.Location = New System.Drawing.Point(0, 33)
        Me.DeleteReportToButton.Name = "DeleteReportToButton"
        Me.DeleteReportToButton.Size = New System.Drawing.Size(50, 33)
        Me.DeleteReportToButton.TabIndex = 57
        Me.DeleteReportToButton.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.DeleteReportToButton, "Buscar a la persona con la que se reporta")
        Me.DeleteReportToButton.UseVisualStyleBackColor = False
        '
        'ReportToButton
        '
        Me.ReportToButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.ReportToButton.Dock = System.Windows.Forms.DockStyle.Top
        Me.ReportToButton.ForeColor = System.Drawing.Color.White
        Me.ReportToButton.Image =  Global.DanashaBasic.My.Resources.Resources.feje_24pgn
        Me.ReportToButton.Location = New System.Drawing.Point(0, 0)
        Me.ReportToButton.Name = "ReportToButton"
        Me.ReportToButton.Size = New System.Drawing.Size(50, 33)
        Me.ReportToButton.TabIndex = 56
        Me.ReportToButton.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ToolTip1.SetToolTip(Me.ReportToButton, "Buscar a la persona con la que se reporta")
        Me.ReportToButton.UseVisualStyleBackColor = False
        '
        'deleteEmployeeButton
        '
        Me.deleteEmployeeButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.deleteEmployeeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.deleteEmployeeButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.deleteEmployeeButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.deleteEmployeeButton.Enabled = False
        Me.deleteEmployeeButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.deleteEmployeeButton.ForeColor = System.Drawing.Color.Blue
        Me.deleteEmployeeButton.Image =  Global.DanashaBasic.My.Resources.Resources.delete_red_64
        Me.deleteEmployeeButton.Location = New System.Drawing.Point(136, 0)
        Me.deleteEmployeeButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.deleteEmployeeButton.Name = "deleteEmployeeButton"
        Me.deleteEmployeeButton.Size = New System.Drawing.Size(72, 73)
        Me.deleteEmployeeButton.TabIndex = 42
        Me.deleteEmployeeButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.deleteEmployeeButton, "Editar información personal")
        Me.deleteEmployeeButton.UseVisualStyleBackColor = False
        '
        'EditCardButton
        '
        Me.EditCardButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.EditCardButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.EditCardButton.Dock = System.Windows.Forms.DockStyle.Left
        Me.EditCardButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.EditCardButton.ForeColor = System.Drawing.Color.Red
        Me.EditCardButton.Image =  Global.DanashaBasic.My.Resources.Resources.Edit_cart_Employee_64
        Me.EditCardButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.EditCardButton.Location = New System.Drawing.Point(0, 0)
        Me.EditCardButton.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.EditCardButton.Name = "EditCardButton"
        Me.EditCardButton.Size = New System.Drawing.Size(74, 74)
        Me.EditCardButton.TabIndex = 42
        Me.EditCardButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolTip1.SetToolTip(Me.EditCardButton, "Ver detalle de la lista seleccionada")
        Me.EditCardButton.UseVisualStyleBackColor = False
        '
        'TituloComboBox
        '
        Me.TituloComboBox.FormattingEnabled = True
        Me.TituloComboBox.Items.AddRange(New Object() {"Dr.", "Dra.", "Lc.", "Lcda.", "Sr.", "Sra.", "Srta."})
        Me.TituloComboBox.Location = New System.Drawing.Point(13, 77)
        Me.TituloComboBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TituloComboBox.Name = "TituloComboBox"
        Me.TituloComboBox.Size = New System.Drawing.Size(184, 24)
        Me.TituloComboBox.Sorted = True
        Me.TituloComboBox.TabIndex = 51
        '
        'CargoComboBox
        '
        Me.CargoComboBox.FormattingEnabled = True
        Me.CargoComboBox.Items.AddRange(New Object() {"Administrador", "Cajero", "Empledo", "Representante de ventas"})
        Me.CargoComboBox.Location = New System.Drawing.Point(13, 140)
        Me.CargoComboBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CargoComboBox.Name = "CargoComboBox"
        Me.CargoComboBox.Size = New System.Drawing.Size(250, 24)
        Me.CargoComboBox.Sorted = True
        Me.CargoComboBox.TabIndex = 52
        '
        'SueldoNumericUpDown
        '
        Me.SueldoNumericUpDown.DecimalPlaces = 2
        Me.SueldoNumericUpDown.Location = New System.Drawing.Point(62, 183)
        Me.SueldoNumericUpDown.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SueldoNumericUpDown.Maximum = New Decimal(New Integer() {100000, 0, 0, 0})
        Me.SueldoNumericUpDown.Name = "SueldoNumericUpDown"
        Me.SueldoNumericUpDown.Size = New System.Drawing.Size(132, 23)
        Me.SueldoNumericUpDown.TabIndex = 53
        Me.SueldoNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ReportToTextBox
        '
        Me.ReportToTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReportToTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ReportToTextBox.Location = New System.Drawing.Point(3, 19)
        Me.ReportToTextBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ReportToTextBox.Multiline = True
        Me.ReportToTextBox.Name = "ReportToTextBox"
        Me.ReportToTextBox.Size = New System.Drawing.Size(196, 66)
        Me.ReportToTextBox.TabIndex = 55
        '
        'PanelDetail
        '
        Me.PanelDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(230, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.PanelDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelDetail.Controls.Add(Me.GroupBox1)
        Me.PanelDetail.Controls.Add(Me.Label5)
        Me.PanelDetail.Controls.Add(Me.Label4)
        Me.PanelDetail.Controls.Add(Me.Label2)
        Me.PanelDetail.Controls.Add(Me.Label1)
        Me.PanelDetail.Controls.Add(Me.PanelMenuDetail)
        Me.PanelDetail.Controls.Add(Me.TituloComboBox)
        Me.PanelDetail.Controls.Add(Me.CargoComboBox)
        Me.PanelDetail.Controls.Add(Me.FechaIngresoDateTimePicker)
        Me.PanelDetail.Controls.Add(Me.SueldoNumericUpDown)
        Me.PanelDetail.Dock = System.Windows.Forms.DockStyle.Left
        Me.PanelDetail.Enabled = False
        Me.PanelDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelDetail.Location = New System.Drawing.Point(0, 0)
        Me.PanelDetail.Name = "PanelDetail"
        Me.PanelDetail.Size = New System.Drawing.Size(283, 428)
        Me.PanelDetail.TabIndex = 56
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ReportToTextBox)
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(13, 302)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(252, 88)
        Me.GroupBox1.TabIndex = 60
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Se reporta á:"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.DeleteReportToButton)
        Me.Panel1.Controls.Add(Me.ReportToButton)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel1.Location = New System.Drawing.Point(199, 19)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(50, 66)
        Me.Panel1.TabIndex = 65
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 227)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(122, 17)
        Me.Label5.TabIndex = 59
        Me.Label5.Text = "Fecha de ingreso:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 184)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 17)
        Me.Label4.TabIndex = 58
        Me.Label4.Text = "Suelo:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 120)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 17)
        Me.Label2.TabIndex = 57
        Me.Label2.Text = "Cargo:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 17)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "Título:"
        '
        'PanelMenuDetail
        '
        Me.PanelMenuDetail.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.PanelMenuDetail.Controls.Add(Me.CandelDeatilButton)
        Me.PanelMenuDetail.Controls.Add(Me.UpdateDetailButton)
        Me.PanelMenuDetail.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelMenuDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelMenuDetail.Location = New System.Drawing.Point(0, 0)
        Me.PanelMenuDetail.Margin = New System.Windows.Forms.Padding(5)
        Me.PanelMenuDetail.Name = "PanelMenuDetail"
        Me.PanelMenuDetail.Size = New System.Drawing.Size(281, 52)
        Me.PanelMenuDetail.TabIndex = 56
        '
        'CandelDeatilButton
        '
        Me.CandelDeatilButton.Location = New System.Drawing.Point(109, 10)
        Me.CandelDeatilButton.Margin = New System.Windows.Forms.Padding(5)
        Me.CandelDeatilButton.Name = "CandelDeatilButton"
        Me.CandelDeatilButton.Size = New System.Drawing.Size(84, 31)
        Me.CandelDeatilButton.TabIndex = 2
        Me.CandelDeatilButton.Text = "Cancelar"
        Me.CandelDeatilButton.UseVisualStyleBackColor = True
        '
        'UpdateDetailButton
        '
        Me.UpdateDetailButton.Location = New System.Drawing.Point(16, 10)
        Me.UpdateDetailButton.Margin = New System.Windows.Forms.Padding(5)
        Me.UpdateDetailButton.Name = "UpdateDetailButton"
        Me.UpdateDetailButton.Size = New System.Drawing.Size(83, 31)
        Me.UpdateDetailButton.TabIndex = 1
        Me.UpdateDetailButton.Text = "Guardar"
        Me.UpdateDetailButton.UseVisualStyleBackColor = True
        '
        'DescriptionLabel
        '
        Me.DescriptionLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.DescriptionLabel.Dock = System.Windows.Forms.DockStyle.Right
        Me.DescriptionLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DescriptionLabel.Location = New System.Drawing.Point(542, 0)
        Me.DescriptionLabel.Name = "DescriptionLabel"
        Me.DescriptionLabel.Size = New System.Drawing.Size(100, 115)
        Me.DescriptionLabel.TabIndex = 62
        '
        'Panelpie
        '
        Me.Panelpie.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panelpie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panelpie.Controls.Add(Me.btnok)
        Me.Panelpie.Controls.Add(Me.btnCancel)
        Me.Panelpie.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panelpie.Location = New System.Drawing.Point(0, 428)
        Me.Panelpie.Name = "Panelpie"
        Me.Panelpie.Size = New System.Drawing.Size(927, 34)
        Me.Panelpie.TabIndex = 57
        '
        'PaneMenu
        '
        Me.PaneMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PaneMenu.Controls.Add(Me.deleteEmployeeButton)
        Me.PaneMenu.Controls.Add(Me.EdidEmployeeCliente)
        Me.PaneMenu.Controls.Add(Me.PanelBusq)
        Me.PaneMenu.Controls.Add(Me.AddNewEmplToList)
        Me.PaneMenu.Dock = System.Windows.Forms.DockStyle.Top
        Me.PaneMenu.Location = New System.Drawing.Point(283, 0)
        Me.PaneMenu.Name = "PaneMenu"
        Me.PaneMenu.Size = New System.Drawing.Size(644, 75)
        Me.PaneMenu.TabIndex = 0
        '
        'PanelBusq
        '
        Me.PanelBusq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelBusq.Controls.Add(Me.Label3)
        Me.PanelBusq.Controls.Add(Me.txtEmple_Busca)
        Me.PanelBusq.Controls.Add(Me.btnBuscar)
        Me.PanelBusq.Dock = System.Windows.Forms.DockStyle.Right
        Me.PanelBusq.Location = New System.Drawing.Point(292, 0)
        Me.PanelBusq.Name = "PanelBusq"
        Me.PanelBusq.Size = New System.Drawing.Size(350, 73)
        Me.PanelBusq.TabIndex = 41
        '
        'PanelPieList
        '
        Me.PanelPieList.Controls.Add(Me.Total_listLabel)
        Me.PanelPieList.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelPieList.Location = New System.Drawing.Point(0, 313)
        Me.PanelPieList.Name = "PanelPieList"
        Me.PanelPieList.Size = New System.Drawing.Size(642, 38)
        Me.PanelPieList.TabIndex = 42
        '
        'Total_listLabel
        '
        Me.Total_listLabel.Dock = System.Windows.Forms.DockStyle.Left
        Me.Total_listLabel.Location = New System.Drawing.Point(0, 0)
        Me.Total_listLabel.Name = "Total_listLabel"
        Me.Total_listLabel.Size = New System.Drawing.Size(194, 38)
        Me.Total_listLabel.TabIndex = 0
        Me.Total_listLabel.Text = "Label1"
        Me.Total_listLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PanelMenuList
        '
        Me.PanelMenuList.Controls.Add(Me.adminEmployeePanel)
        Me.PanelMenuList.Controls.Add(Me.PictureBox1)
        Me.PanelMenuList.Controls.Add(Me.DescriptionLabel)
        Me.PanelMenuList.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelMenuList.Location = New System.Drawing.Point(0, 0)
        Me.PanelMenuList.Name = "PanelMenuList"
        Me.PanelMenuList.Size = New System.Drawing.Size(642, 115)
        Me.PanelMenuList.TabIndex = 43
        '
        'adminEmployeePanel
        '
        Me.adminEmployeePanel.Controls.Add(Me.EditDetailButton)
        Me.adminEmployeePanel.Controls.Add(Me.EditCardButton)
        Me.adminEmployeePanel.Dock = System.Windows.Forms.DockStyle.Top
        Me.adminEmployeePanel.Location = New System.Drawing.Point(0, 0)
        Me.adminEmployeePanel.Name = "adminEmployeePanel"
        Me.adminEmployeePanel.Size = New System.Drawing.Size(422, 74)
        Me.adminEmployeePanel.TabIndex = 63
        '
        'PictureBox1
        '
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Right
        Me.PictureBox1.Location = New System.Drawing.Point(422, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(120, 115)
        Me.PictureBox1.TabIndex = 61
        Me.PictureBox1.TabStop = False
        '
        'PanelList
        '
        Me.PanelList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelList.Controls.Add(Me.PanelView)
        Me.PanelList.Controls.Add(Me.PanelPieList)
        Me.PanelList.Controls.Add(Me.PanelMenuList)
        Me.PanelList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelList.Location = New System.Drawing.Point(283, 75)
        Me.PanelList.Name = "PanelList"
        Me.PanelList.Size = New System.Drawing.Size(644, 353)
        Me.PanelList.TabIndex = 59
        '
        'PanelView
        '
        Me.PanelView.Controls.Add(Me.dtg)
        Me.PanelView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelView.Location = New System.Drawing.Point(0, 115)
        Me.PanelView.Margin = New System.Windows.Forms.Padding(5)
        Me.PanelView.Name = "PanelView"
        Me.PanelView.Size = New System.Drawing.Size(642, 198)
        Me.PanelView.TabIndex = 44
        '
        'dtg
        '
        Me.dtg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dtg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dtg.Location = New System.Drawing.Point(0, 0)
        Me.dtg.Name = "dtg"
        Me.dtg.Size = New System.Drawing.Size(642, 198)
        Me.dtg.TabIndex = 0
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'deleteContextMenuStrip
        '
        Me.deleteContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BorrarDeLaListaDeEmpleadosToolStripMenuItem, Me.EliminarTotaLaInformacionDeEsteEmpleadoToolStripMenuItem})
        Me.deleteContextMenuStrip.Name = "deleteContextMenuStrip"
        Me.deleteContextMenuStrip.Size = New System.Drawing.Size(318, 48)
        '
        'BorrarDeLaListaDeEmpleadosToolStripMenuItem
        '
        Me.BorrarDeLaListaDeEmpleadosToolStripMenuItem.Image =  Global.DanashaBasic.My.Resources.Resources.delete_card_32
        Me.BorrarDeLaListaDeEmpleadosToolStripMenuItem.Name = "BorrarDeLaListaDeEmpleadosToolStripMenuItem"
        Me.BorrarDeLaListaDeEmpleadosToolStripMenuItem.Size = New System.Drawing.Size(317, 22)
        Me.BorrarDeLaListaDeEmpleadosToolStripMenuItem.Text = "Borrar de la lista de empleados."
        '
        'EliminarTotaLaInformacionDeEsteEmpleadoToolStripMenuItem
        '
        Me.EliminarTotaLaInformacionDeEsteEmpleadoToolStripMenuItem.Image =  Global.DanashaBasic.My.Resources.Resources.Delete_32
        Me.EliminarTotaLaInformacionDeEsteEmpleadoToolStripMenuItem.Name = "EliminarTotaLaInformacionDeEsteEmpleadoToolStripMenuItem"
        Me.EliminarTotaLaInformacionDeEsteEmpleadoToolStripMenuItem.Size = New System.Drawing.Size(317, 22)
        Me.EliminarTotaLaInformacionDeEsteEmpleadoToolStripMenuItem.Text = "Eliminar tota la informacion de este empleado"
        '
        'frmList_Empleados
        '
        Me.AcceptButton = Me.btnok
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(927, 462)
        Me.Controls.Add(Me.PanelList)
        Me.Controls.Add(Me.PaneMenu)
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.txtIdCliente)
        Me.Controls.Add(Me.PanelDetail)
        Me.Controls.Add(Me.Panelpie)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmList_Empleados"
        Me.Text = "Listado de empleados.."
        CType(Me.SueldoNumericUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelDetail.ResumeLayout(False)
        Me.PanelDetail.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.PanelMenuDetail.ResumeLayout(False)
        Me.Panelpie.ResumeLayout(False)
        Me.PaneMenu.ResumeLayout(False)
        Me.PanelBusq.ResumeLayout(False)
        Me.PanelBusq.PerformLayout()
        Me.PanelPieList.ResumeLayout(False)
        Me.PanelMenuList.ResumeLayout(False)
        Me.adminEmployeePanel.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelList.ResumeLayout(False)
        Me.PanelView.ResumeLayout(False)
        CType(Me.dtg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.deleteContextMenuStrip.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents notificacion As System.Windows.Forms.NotifyIcon
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents AddNewEmplToList As System.Windows.Forms.Button
    Friend WithEvents EdidEmployeeCliente As System.Windows.Forms.Button
    Friend WithEvents txtIdCliente As System.Windows.Forms.TextBox
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtEmple_Busca As System.Windows.Forms.TextBox
    Friend WithEvents btnok As System.Windows.Forms.Button
    Private WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents TituloComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents CargoComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents SueldoNumericUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents FechaIngresoDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents ReportToTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PanelDetail As System.Windows.Forms.Panel
    Friend WithEvents Panelpie As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PanelMenuDetail As System.Windows.Forms.Panel
    Friend WithEvents CandelDeatilButton As System.Windows.Forms.Button
    Friend WithEvents UpdateDetailButton As System.Windows.Forms.Button
    Friend WithEvents EditDetailButton As System.Windows.Forms.Button
    Friend WithEvents PaneMenu As System.Windows.Forms.Panel
    Friend WithEvents PanelMenuList As System.Windows.Forms.Panel
    Friend WithEvents PanelPieList As System.Windows.Forms.Panel
    Friend WithEvents Total_listLabel As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ReportToButton As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents PanelBusq As System.Windows.Forms.Panel
    Friend WithEvents PanelList As System.Windows.Forms.Panel
    Friend WithEvents PanelView As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents DescriptionLabel As System.Windows.Forms.Label
    Friend WithEvents dtg As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents DeleteReportToButton As System.Windows.Forms.Button
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents deleteEmployeeButton As Button
    Friend WithEvents adminEmployeePanel As Windows.Forms.Panel
    Friend WithEvents EditCardButton As Button
    Friend WithEvents deleteContextMenuStrip As ContextMenuStrip
    Friend WithEvents BorrarDeLaListaDeEmpleadosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EliminarTotaLaInformacionDeEsteEmpleadoToolStripMenuItem As ToolStripMenuItem
End Class
