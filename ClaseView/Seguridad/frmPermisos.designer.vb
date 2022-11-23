<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPermisos
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNom_Grup = New System.Windows.Forms.TextBox()
        Me.btnNuevo = New System.Windows.Forms.Button()
        Me.btnCancela = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.dataGridGrupo = New System.Windows.Forms.DataGridView()
        Me.btnModif = New System.Windows.Forms.Button()
        Me.btnElimi = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.GrupList = New System.Windows.Forms.GroupBox()
        Me.btnRestableceMenu = New System.Windows.Forms.Button()
        Me.txtIdPermiso = New System.Windows.Forms.TextBox()
        Me.txtEstado = New System.Windows.Forms.TextBox()
        Me.GropMunuList = New System.Windows.Forms.GroupBox()
        Me.btnAplicar = New System.Windows.Forms.Button()
        Me.DataGridMenu = New System.Windows.Forms.DataGridView()
        Me.btnDenegar = New System.Windows.Forms.Button()
        Me.txtIdUltimo = New System.Windows.Forms.TextBox()
        Me.btnPermitir = New System.Windows.Forms.Button()
        Me.txtIdGrupo = New System.Windows.Forms.TextBox()
        CType(Me.dataGridGrupo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrupList.SuspendLayout()
        Me.GropMunuList.SuspendLayout()
        CType(Me.DataGridMenu, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Label1.Location = New System.Drawing.Point(8, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Grupo:"
        '
        'txtNom_Grup
        '
        Me.txtNom_Grup.Enabled = False
        Me.txtNom_Grup.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.txtNom_Grup.Location = New System.Drawing.Point(59, 26)
        Me.txtNom_Grup.Name = "txtNom_Grup"
        Me.txtNom_Grup.Size = New System.Drawing.Size(318, 23)
        Me.txtNom_Grup.TabIndex = 3
        '
        'btnNuevo
        '
        Me.btnNuevo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNuevo.Location = New System.Drawing.Point(292, 63)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Size = New System.Drawing.Size(85, 31)
        Me.btnNuevo.TabIndex = 4
        Me.btnNuevo.Text = "Nuevo.."
        Me.btnNuevo.UseVisualStyleBackColor = True
        '
        'btnCancela
        '
        Me.btnCancela.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancela.Location = New System.Drawing.Point(292, 104)
        Me.btnCancela.Name = "btnCancela"
        Me.btnCancela.Size = New System.Drawing.Size(85, 31)
        Me.btnCancela.TabIndex = 5
        Me.btnCancela.Text = "Cancelar"
        Me.btnCancela.UseVisualStyleBackColor = True
        '
        'btnGuardar
        '
        Me.btnGuardar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGuardar.Location = New System.Drawing.Point(292, 144)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(85, 31)
        Me.btnGuardar.TabIndex = 5
        Me.btnGuardar.Text = "Guardar.."
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'dataGridGrupo
        '
        Me.dataGridGrupo.AllowUserToAddRows = False
        Me.dataGridGrupo.AllowUserToDeleteRows = False
        Me.dataGridGrupo.AllowUserToOrderColumns = True
        Me.dataGridGrupo.AllowUserToResizeColumns = False
        Me.dataGridGrupo.AllowUserToResizeRows = False
        Me.dataGridGrupo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dataGridGrupo.BackgroundColor = System.Drawing.Color.White
        Me.dataGridGrupo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataGridGrupo.GridColor = System.Drawing.Color.White
        Me.dataGridGrupo.Location = New System.Drawing.Point(11, 63)
        Me.dataGridGrupo.MultiSelect = False
        Me.dataGridGrupo.Name = "dataGridGrupo"
        Me.dataGridGrupo.ReadOnly = True
        Me.dataGridGrupo.RowHeadersVisible = False
        Me.dataGridGrupo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dataGridGrupo.Size = New System.Drawing.Size(275, 264)
        Me.dataGridGrupo.TabIndex = 7
        '
        'btnModif
        '
        Me.btnModif.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnModif.Enabled = False
        Me.btnModif.Location = New System.Drawing.Point(201, 330)
        Me.btnModif.Name = "btnModif"
        Me.btnModif.Size = New System.Drawing.Size(85, 31)
        Me.btnModif.TabIndex = 5
        Me.btnModif.Text = "Modificar.."
        Me.btnModif.UseVisualStyleBackColor = True
        '
        'btnElimi
        '
        Me.btnElimi.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnElimi.Enabled = False
        Me.btnElimi.Location = New System.Drawing.Point(110, 330)
        Me.btnElimi.Name = "btnElimi"
        Me.btnElimi.Size = New System.Drawing.Size(85, 31)
        Me.btnElimi.TabIndex = 5
        Me.btnElimi.Text = "Eliminar.."
        Me.btnElimi.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(357, 618)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(85, 31)
        Me.Button6.TabIndex = 5
        Me.Button6.Text = "Guardar.."
        Me.Button6.UseVisualStyleBackColor = True
        '
        'GrupList
        '
        Me.GrupList.Controls.Add(Me.dataGridGrupo)
        Me.GrupList.Controls.Add(Me.btnElimi)
        Me.GrupList.Controls.Add(Me.btnModif)
        Me.GrupList.Controls.Add(Me.btnGuardar)
        Me.GrupList.Controls.Add(Me.btnCancela)
        Me.GrupList.Controls.Add(Me.txtNom_Grup)
        Me.GrupList.Controls.Add(Me.Label1)
        Me.GrupList.Controls.Add(Me.btnNuevo)
        Me.GrupList.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.GrupList.Location = New System.Drawing.Point(4, 32)
        Me.GrupList.Name = "GrupList"
        Me.GrupList.Size = New System.Drawing.Size(383, 369)
        Me.GrupList.TabIndex = 8
        Me.GrupList.TabStop = False
        Me.GrupList.Text = "Listado de grupos"
        '
        'btnRestableceMenu
        '
        Me.btnRestableceMenu.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRestableceMenu.Location = New System.Drawing.Point(6, 18)
        Me.btnRestableceMenu.Name = "btnRestableceMenu"
        Me.btnRestableceMenu.Size = New System.Drawing.Size(189, 31)
        Me.btnRestableceMenu.TabIndex = 5
        Me.btnRestableceMenu.Text = "Copiar los Menus de este sistema"
        Me.btnRestableceMenu.UseVisualStyleBackColor = True
        '
        'txtIdPermiso
        '
        Me.txtIdPermiso.Enabled = False
        Me.txtIdPermiso.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.txtIdPermiso.Location = New System.Drawing.Point(84, 3)
        Me.txtIdPermiso.Name = "txtIdPermiso"
        Me.txtIdPermiso.Size = New System.Drawing.Size(61, 23)
        Me.txtIdPermiso.TabIndex = 3
        Me.txtIdPermiso.Text = "0"
        Me.txtIdPermiso.Visible = False
        '
        'txtEstado
        '
        Me.txtEstado.Enabled = False
        Me.txtEstado.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.txtEstado.Location = New System.Drawing.Point(172, 3)
        Me.txtEstado.Name = "txtEstado"
        Me.txtEstado.Size = New System.Drawing.Size(61, 23)
        Me.txtEstado.TabIndex = 3
        Me.txtEstado.Text = "0"
        Me.txtEstado.Visible = False
        '
        'GropMunuList
        '
        Me.GropMunuList.Controls.Add(Me.btnAplicar)
        Me.GropMunuList.Controls.Add(Me.DataGridMenu)
        Me.GropMunuList.Controls.Add(Me.btnRestableceMenu)
        Me.GropMunuList.Controls.Add(Me.btnDenegar)
        Me.GropMunuList.Controls.Add(Me.txtIdUltimo)
        Me.GropMunuList.Controls.Add(Me.btnPermitir)
        Me.GropMunuList.Location = New System.Drawing.Point(405, 32)
        Me.GropMunuList.Name = "GropMunuList"
        Me.GropMunuList.Size = New System.Drawing.Size(406, 361)
        Me.GropMunuList.TabIndex = 8
        Me.GropMunuList.TabStop = False
        Me.GropMunuList.Text = "Listado de Menu en el sistema"
        '
        'btnAplicar
        '
        Me.btnAplicar.Enabled = False
        Me.btnAplicar.Location = New System.Drawing.Point(329, 323)
        Me.btnAplicar.Name = "btnAplicar"
        Me.btnAplicar.Size = New System.Drawing.Size(69, 31)
        Me.btnAplicar.TabIndex = 8
        Me.btnAplicar.Text = "Aplicar"
        Me.btnAplicar.UseVisualStyleBackColor = True
        '
        'DataGridMenu
        '
        Me.DataGridMenu.AllowUserToAddRows = False
        Me.DataGridMenu.AllowUserToDeleteRows = False
        Me.DataGridMenu.AllowUserToResizeColumns = False
        Me.DataGridMenu.AllowUserToResizeRows = False
        Me.DataGridMenu.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridMenu.BackgroundColor = System.Drawing.Color.White
        Me.DataGridMenu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridMenu.GridColor = System.Drawing.Color.White
        Me.DataGridMenu.Location = New System.Drawing.Point(21, 55)
        Me.DataGridMenu.MultiSelect = False
        Me.DataGridMenu.Name = "DataGridMenu"
        Me.DataGridMenu.ReadOnly = True
        Me.DataGridMenu.RowHeadersVisible = False
        Me.DataGridMenu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridMenu.Size = New System.Drawing.Size(377, 263)
        Me.DataGridMenu.TabIndex = 7
        '
        'btnDenegar
        '
        Me.btnDenegar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDenegar.Enabled = False
        Me.btnDenegar.Location = New System.Drawing.Point(165, 323)
        Me.btnDenegar.Name = "btnDenegar"
        Me.btnDenegar.Size = New System.Drawing.Size(70, 31)
        Me.btnDenegar.TabIndex = 5
        Me.btnDenegar.Text = "Denegar"
        Me.btnDenegar.UseVisualStyleBackColor = True
        '
        'txtIdUltimo
        '
        Me.txtIdUltimo.Enabled = False
        Me.txtIdUltimo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.txtIdUltimo.Location = New System.Drawing.Point(291, 18)
        Me.txtIdUltimo.Name = "txtIdUltimo"
        Me.txtIdUltimo.Size = New System.Drawing.Size(61, 23)
        Me.txtIdUltimo.TabIndex = 3
        Me.txtIdUltimo.Text = "0"
        Me.txtIdUltimo.Visible = False
        '
        'btnPermitir
        '
        Me.btnPermitir.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPermitir.Enabled = False
        Me.btnPermitir.Location = New System.Drawing.Point(241, 323)
        Me.btnPermitir.Name = "btnPermitir"
        Me.btnPermitir.Size = New System.Drawing.Size(82, 31)
        Me.btnPermitir.TabIndex = 5
        Me.btnPermitir.Text = "Permitir"
        Me.btnPermitir.UseVisualStyleBackColor = True
        '
        'txtIdGrupo
        '
        Me.txtIdGrupo.Enabled = False
        Me.txtIdGrupo.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.txtIdGrupo.Location = New System.Drawing.Point(239, 3)
        Me.txtIdGrupo.Name = "txtIdGrupo"
        Me.txtIdGrupo.Size = New System.Drawing.Size(61, 23)
        Me.txtIdGrupo.TabIndex = 3
        Me.txtIdGrupo.Text = "0"
        Me.txtIdGrupo.Visible = False
        '
        'frmPermisos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(818, 403)
        Me.Controls.Add(Me.GropMunuList)
        Me.Controls.Add(Me.GrupList)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.txtIdGrupo)
        Me.Controls.Add(Me.txtEstado)
        Me.Controls.Add(Me.txtIdPermiso)
        Me.Name = "frmPermisos"
        Me.Text = "Administrando Permisos  para grupos.."
        CType(Me.dataGridGrupo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrupList.ResumeLayout(False)
        Me.GrupList.PerformLayout()
        Me.GropMunuList.ResumeLayout(False)
        Me.GropMunuList.PerformLayout()
        CType(Me.DataGridMenu, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtNom_Grup As System.Windows.Forms.TextBox
    Friend WithEvents btnNuevo As System.Windows.Forms.Button
    Friend WithEvents btnCancela As System.Windows.Forms.Button
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents dataGridGrupo As System.Windows.Forms.DataGridView
    Friend WithEvents btnModif As System.Windows.Forms.Button
    Friend WithEvents btnElimi As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents GrupList As System.Windows.Forms.GroupBox
    Friend WithEvents txtIdPermiso As System.Windows.Forms.TextBox
    Friend WithEvents txtEstado As System.Windows.Forms.TextBox
    Friend WithEvents btnRestableceMenu As System.Windows.Forms.Button
    Friend WithEvents GropMunuList As System.Windows.Forms.GroupBox
    Friend WithEvents DataGridMenu As System.Windows.Forms.DataGridView
    Friend WithEvents btnDenegar As System.Windows.Forms.Button
    Friend WithEvents btnPermitir As System.Windows.Forms.Button
    Friend WithEvents txtIdGrupo As System.Windows.Forms.TextBox
    Friend WithEvents txtIdUltimo As System.Windows.Forms.TextBox
    Friend WithEvents btnAplicar As System.Windows.Forms.Button
End Class
