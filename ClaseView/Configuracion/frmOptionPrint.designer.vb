<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOptionPrint
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
        Me.PaneTicketDetail = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.typePrintComboBox = New System.Windows.Forms.ComboBox()
        Me.txtItems = New System.Windows.Forms.NumericUpDown()
        Me.txtidMaquina = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chekColor = New System.Windows.Forms.CheckedListBox()
        Me.btnSeleciona = New System.Windows.Forms.Button()
        Me.txtNameTicket = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.okBooton = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.PanelMenu = New System.Windows.Forms.Panel()
        Me.RefreshListDocumentButton = New System.Windows.Forms.Button()
        Me.DocumentListBox = New System.Windows.Forms.ListBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.isDEfaultCheckedListBox = New System.Windows.Forms.CheckedListBox()
        Me.PrintLogoCheck = New System.Windows.Forms.CheckBox()
        Me.PaneTicketDetail.SuspendLayout()
        CType(Me.txtItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.PanelMenu.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PaneTicketDetail
        '
        Me.PaneTicketDetail.BackColor = System.Drawing.SystemColors.ControlLight
        Me.PaneTicketDetail.Controls.Add(Me.PrintLogoCheck)
        Me.PaneTicketDetail.Controls.Add(Me.Label4)
        Me.PaneTicketDetail.Controls.Add(Me.typePrintComboBox)
        Me.PaneTicketDetail.Controls.Add(Me.txtItems)
        Me.PaneTicketDetail.Controls.Add(Me.txtidMaquina)
        Me.PaneTicketDetail.Controls.Add(Me.Label3)
        Me.PaneTicketDetail.Controls.Add(Me.Label1)
        Me.PaneTicketDetail.Controls.Add(Me.chekColor)
        Me.PaneTicketDetail.Controls.Add(Me.btnSeleciona)
        Me.PaneTicketDetail.Controls.Add(Me.txtNameTicket)
        Me.PaneTicketDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PaneTicketDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PaneTicketDetail.Location = New System.Drawing.Point(167, 98)
        Me.PaneTicketDetail.Name = "PaneTicketDetail"
        Me.PaneTicketDetail.Size = New System.Drawing.Size(607, 238)
        Me.PaneTicketDetail.TabIndex = 1
        Me.PaneTicketDetail.TabStop = False
        Me.PaneTicketDetail.Text = "Impresora de Ticket"
        Me.PaneTicketDetail.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(170, 113)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(110, 15)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Tipo de impresora:"
        '
        'typePrintComboBox
        '
        Me.typePrintComboBox.FormattingEnabled = True
        Me.typePrintComboBox.Items.AddRange(New Object() {"Ticket", "Matricial", "Tinta"})
        Me.typePrintComboBox.Location = New System.Drawing.Point(303, 110)
        Me.typePrintComboBox.Name = "typePrintComboBox"
        Me.typePrintComboBox.Size = New System.Drawing.Size(171, 23)
        Me.typePrintComboBox.TabIndex = 9
        '
        'txtItems
        '
        Me.txtItems.Location = New System.Drawing.Point(186, 178)
        Me.txtItems.Maximum = New Decimal(New Integer() {50, 0, 0, 0})
        Me.txtItems.Minimum = New Decimal(New Integer() {8, 0, 0, 0})
        Me.txtItems.Name = "txtItems"
        Me.txtItems.Size = New System.Drawing.Size(94, 21)
        Me.txtItems.TabIndex = 8
        Me.txtItems.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'txtidMaquina
        '
        Me.txtidMaquina.Location = New System.Drawing.Point(14, 99)
        Me.txtidMaquina.Name = "txtidMaquina"
        Me.txtidMaquina.Size = New System.Drawing.Size(19, 21)
        Me.txtidMaquina.TabIndex = 7
        Me.txtidMaquina.Text = "0"
        Me.txtidMaquina.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(168, 159)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(112, 15)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Items de impresión"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(303, 154)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 15)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Color de tinta"
        '
        'chekColor
        '
        Me.chekColor.BackColor = System.Drawing.SystemColors.ControlLight
        Me.chekColor.Items.AddRange(New Object() {"Negro", "Rojo"})
        Me.chekColor.Location = New System.Drawing.Point(303, 172)
        Me.chekColor.Name = "chekColor"
        Me.chekColor.Size = New System.Drawing.Size(105, 52)
        Me.chekColor.TabIndex = 2
        '
        'btnSeleciona
        '
        Me.btnSeleciona.Location = New System.Drawing.Point(383, 27)
        Me.btnSeleciona.Name = "btnSeleciona"
        Me.btnSeleciona.Size = New System.Drawing.Size(92, 25)
        Me.btnSeleciona.TabIndex = 1
        Me.btnSeleciona.Text = "Seleccionar...."
        Me.btnSeleciona.UseVisualStyleBackColor = True
        '
        'txtNameTicket
        '
        Me.txtNameTicket.Location = New System.Drawing.Point(16, 29)
        Me.txtNameTicket.Name = "txtNameTicket"
        Me.txtNameTicket.ReadOnly = True
        Me.txtNameTicket.Size = New System.Drawing.Size(361, 21)
        Me.txtNameTicket.TabIndex = 0
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(683, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(81, 27)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "Cancelar.."
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'okBooton
        '
        Me.okBooton.Location = New System.Drawing.Point(602, 3)
        Me.okBooton.Name = "okBooton"
        Me.okBooton.Size = New System.Drawing.Size(75, 27)
        Me.okBooton.TabIndex = 4
        Me.okBooton.Text = "Aplicar.."
        Me.okBooton.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(60, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(193, 20)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Selecciona uno del listado"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 88.0117!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.9883!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 93.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.btnCancel, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.okBooton, 1, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 336)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(774, 33)
        Me.TableLayoutPanel2.TabIndex = 8
        '
        'PanelMenu
        '
        Me.PanelMenu.Controls.Add(Me.RefreshListDocumentButton)
        Me.PanelMenu.Controls.Add(Me.Label2)
        Me.PanelMenu.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelMenu.Location = New System.Drawing.Point(0, 0)
        Me.PanelMenu.Name = "PanelMenu"
        Me.PanelMenu.Size = New System.Drawing.Size(774, 48)
        Me.PanelMenu.TabIndex = 9
        '
        'RefreshListDocumentButton
        '
        Me.RefreshListDocumentButton.Image =  Global.DanashaBasicSignature.My.Resources.Resources.Refresh_32png
        Me.RefreshListDocumentButton.Location = New System.Drawing.Point(13, 5)
        Me.RefreshListDocumentButton.Name = "RefreshListDocumentButton"
        Me.RefreshListDocumentButton.Size = New System.Drawing.Size(41, 37)
        Me.RefreshListDocumentButton.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.RefreshListDocumentButton, "Actualizar lista de documentos..")
        Me.RefreshListDocumentButton.UseVisualStyleBackColor = True
        '
        'DocumentListBox
        '
        Me.DocumentListBox.Dock = System.Windows.Forms.DockStyle.Left
        Me.DocumentListBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DocumentListBox.FormattingEnabled = True
        Me.DocumentListBox.ItemHeight = 16
        Me.DocumentListBox.Location = New System.Drawing.Point(0, 48)
        Me.DocumentListBox.Name = "DocumentListBox"
        Me.DocumentListBox.Size = New System.Drawing.Size(167, 288)
        Me.DocumentListBox.TabIndex = 10
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.isDEfaultCheckedListBox)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(167, 48)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(607, 50)
        Me.Panel1.TabIndex = 11
        '
        'isDEfaultCheckedListBox
        '
        Me.isDEfaultCheckedListBox.BackColor = System.Drawing.SystemColors.Control
        Me.isDEfaultCheckedListBox.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.isDEfaultCheckedListBox.Dock = System.Windows.Forms.DockStyle.Left
        Me.isDEfaultCheckedListBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.isDEfaultCheckedListBox.FormattingEnabled = True
        Me.isDEfaultCheckedListBox.Items.AddRange(New Object() {"Predeterminado en el sistema", "Configurar"})
        Me.isDEfaultCheckedListBox.Location = New System.Drawing.Point(0, 0)
        Me.isDEfaultCheckedListBox.Name = "isDEfaultCheckedListBox"
        Me.isDEfaultCheckedListBox.Size = New System.Drawing.Size(279, 48)
        Me.isDEfaultCheckedListBox.TabIndex = 0
        '
        'PrintLogoCheck
        '
        Me.PrintLogoCheck.AutoSize = True
        Me.PrintLogoCheck.Location = New System.Drawing.Point(303, 70)
        Me.PrintLogoCheck.Name = "PrintLogoCheck"
        Me.PrintLogoCheck.Size = New System.Drawing.Size(102, 19)
        Me.PrintLogoCheck.TabIndex = 11
        Me.PrintLogoCheck.Text = "Imprimir logo."
        Me.PrintLogoCheck.UseVisualStyleBackColor = True
        '
        'frmOptionPrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(774, 369)
        Me.Controls.Add(Me.PaneTicketDetail)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.DocumentListBox)
        Me.Controls.Add(Me.PanelMenu)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOptionPrint"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Configuración de impresora de ticket"
        Me.PaneTicketDetail.ResumeLayout(False)
        Me.PaneTicketDetail.PerformLayout()
        CType(Me.txtItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.PanelMenu.ResumeLayout(False)
        Me.PanelMenu.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PaneTicketDetail As System.Windows.Forms.GroupBox
    Friend WithEvents btnSeleciona As System.Windows.Forms.Button
    Friend WithEvents txtNameTicket As System.Windows.Forms.TextBox
    Friend WithEvents okBooton As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents chekColor As System.Windows.Forms.CheckedListBox
    Friend WithEvents txtidMaquina As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtItems As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents PanelMenu As System.Windows.Forms.Panel
    Friend WithEvents DocumentListBox As System.Windows.Forms.ListBox
    Friend WithEvents RefreshListDocumentButton As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label4 As Label
    Friend WithEvents typePrintComboBox As ComboBox
    Friend WithEvents Panel1 As Windows.Forms.Panel
    Friend WithEvents isDEfaultCheckedListBox As CheckedListBox
    Friend WithEvents PrintLogoCheck As CheckBox
End Class
