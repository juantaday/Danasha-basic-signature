<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMotivo
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
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblDetalle = New System.Windows.Forms.Label()
        Me.lblTitulo = New System.Windows.Forms.Label()
        Me.pnlBody = New System.Windows.Forms.Panel()
        Me.lblError = New System.Windows.Forms.Label()
        Me.cboMotivo = New System.Windows.Forms.ComboBox()
        Me.lblMotivoLabel = New System.Windows.Forms.Label()
        Me.pnlFooter = New System.Windows.Forms.Panel()
        Me.btnConfirmar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.pnlAccent = New System.Windows.Forms.Panel()
        Me.btnActualizar = New System.Windows.Forms.Button()
        Me.pnlHeader.SuspendLayout()
        Me.pnlBody.SuspendLayout()
        Me.pnlFooter.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblDetalle)
        Me.pnlHeader.Controls.Add(Me.lblTitulo)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(4, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Padding = New System.Windows.Forms.Padding(14, 12, 14, 12)
        Me.pnlHeader.Size = New System.Drawing.Size(394, 69)
        Me.pnlHeader.TabIndex = 1
        '
        'lblDetalle
        '
        Me.lblDetalle.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblDetalle.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblDetalle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(159, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.lblDetalle.Location = New System.Drawing.Point(14, 40)
        Me.lblDetalle.Name = "lblDetalle"
        Me.lblDetalle.Size = New System.Drawing.Size(366, 17)
        Me.lblDetalle.TabIndex = 0
        Me.lblDetalle.Text = "Producto  |  Enviado: 0  ·  Recibido: 0  ·  Diferencia: −0"
        '
        'lblTitulo
        '
        Me.lblTitulo.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTitulo.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
        Me.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.lblTitulo.Location = New System.Drawing.Point(14, 12)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.lblTitulo.Size = New System.Drawing.Size(366, 23)
        Me.lblTitulo.TabIndex = 1
        Me.lblTitulo.Text = "Indique el motivo de la diferencia"
        '
        'pnlBody
        '
        Me.pnlBody.BackColor = System.Drawing.Color.White
        Me.pnlBody.Controls.Add(Me.lblError)
        Me.pnlBody.Controls.Add(Me.cboMotivo)
        Me.pnlBody.Controls.Add(Me.lblMotivoLabel)
        Me.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlBody.Location = New System.Drawing.Point(4, 69)
        Me.pnlBody.Name = "pnlBody"
        Me.pnlBody.Padding = New System.Windows.Forms.Padding(17, 16, 17, 0)
        Me.pnlBody.Size = New System.Drawing.Size(394, 156)
        Me.pnlBody.TabIndex = 0
        '
        'lblError
        '
        Me.lblError.AutoSize = True
        Me.lblError.Font = New System.Drawing.Font("Segoe UI", 8.5!)
        Me.lblError.ForeColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(75, Byte), Integer), CType(CType(74, Byte), Integer))
        Me.lblError.Location = New System.Drawing.Point(17, 62)
        Me.lblError.Name = "lblError"
        Me.lblError.Size = New System.Drawing.Size(255, 15)
        Me.lblError.TabIndex = 0
        Me.lblError.Text = "⚠  Debe seleccionar un motivo para continuar."
        Me.lblError.Visible = False
        '
        'cboMotivo
        '
        Me.cboMotivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMotivo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cboMotivo.Font = New System.Drawing.Font("Segoe UI", 9.5!)
        Me.cboMotivo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(55, Byte), Integer), CType(CType(70, Byte), Integer))
        Me.cboMotivo.FormattingEnabled = True
        Me.cboMotivo.Location = New System.Drawing.Point(17, 35)
        Me.cboMotivo.Name = "cboMotivo"
        Me.cboMotivo.Size = New System.Drawing.Size(360, 25)
        Me.cboMotivo.TabIndex = 0
        '
        'lblMotivoLabel
        '
        Me.lblMotivoLabel.AutoSize = True
        Me.lblMotivoLabel.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.lblMotivoLabel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(115, Byte), Integer))
        Me.lblMotivoLabel.Location = New System.Drawing.Point(17, 16)
        Me.lblMotivoLabel.Name = "lblMotivoLabel"
        Me.lblMotivoLabel.Size = New System.Drawing.Size(145, 15)
        Me.lblMotivoLabel.TabIndex = 1
        Me.lblMotivoLabel.Text = "Motivo de la discrepancia:"
        '
        'pnlFooter
        '
        Me.pnlFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(250, Byte), Integer))
        Me.pnlFooter.Controls.Add(Me.btnActualizar)
        Me.pnlFooter.Controls.Add(Me.btnConfirmar)
        Me.pnlFooter.Controls.Add(Me.btnCancelar)
        Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFooter.Location = New System.Drawing.Point(4, 225)
        Me.pnlFooter.Name = "pnlFooter"
        Me.pnlFooter.Padding = New System.Windows.Forms.Padding(0, 9, 14, 9)
        Me.pnlFooter.Size = New System.Drawing.Size(394, 52)
        Me.pnlFooter.TabIndex = 2
        '
        'btnConfirmar
        '
        Me.btnConfirmar.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnConfirmar.BackColor = System.Drawing.Color.FromArgb(CType(CType(15, Byte), Integer), CType(CType(110, Byte), Integer), CType(CType(86, Byte), Integer))
        Me.btnConfirmar.FlatAppearance.BorderSize = 0
        Me.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnConfirmar.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnConfirmar.ForeColor = System.Drawing.Color.White
        Me.btnConfirmar.Location = New System.Drawing.Point(293, 10)
        Me.btnConfirmar.Name = "btnConfirmar"
        Me.btnConfirmar.Size = New System.Drawing.Size(89, 28)
        Me.btnConfirmar.TabIndex = 2
        Me.btnConfirmar.Text = "Confirmar"
        Me.btnConfirmar.UseVisualStyleBackColor = False
        '
        'btnCancelar
        '
        Me.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(220, Byte), Integer))
        Me.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelar.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(115, Byte), Integer))
        Me.btnCancelar.Location = New System.Drawing.Point(206, 10)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(75, 28)
        Me.btnCancelar.TabIndex = 1
        Me.btnCancelar.Text = "Cancelar"
        '
        'pnlAccent
        '
        Me.pnlAccent.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(159, Byte), Integer), CType(CType(39, Byte), Integer))
        Me.pnlAccent.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlAccent.Location = New System.Drawing.Point(0, 0)
        Me.pnlAccent.Name = "pnlAccent"
        Me.pnlAccent.Size = New System.Drawing.Size(4, 277)
        Me.pnlAccent.TabIndex = 3
        '
        'btnActualizar
        '
        Me.btnActualizar.BackColor = System.Drawing.Color.FromArgb(CType(CType(225, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnActualizar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(130, Byte), Integer))
        Me.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnActualizar.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.btnActualizar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.btnActualizar.Location = New System.Drawing.Point(8, 9)
        Me.btnActualizar.Name = "btnActualizar"
        Me.btnActualizar.Size = New System.Drawing.Size(109, 30)
        Me.btnActualizar.TabIndex = 3
        Me.btnActualizar.Text = "↻  Actualizar"
        Me.btnActualizar.UseVisualStyleBackColor = False
        '
        'frmMotivo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(398, 277)
        Me.Controls.Add(Me.pnlBody)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.pnlFooter)
        Me.Controls.Add(Me.pnlAccent)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMotivo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Motivo de discrepancia"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlBody.ResumeLayout(False)
        Me.pnlBody.PerformLayout()
        Me.pnlFooter.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlAccent As System.Windows.Forms.Panel
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents lblDetalle As System.Windows.Forms.Label
    Friend WithEvents pnlBody As System.Windows.Forms.Panel
    Friend WithEvents lblMotivoLabel As System.Windows.Forms.Label
    Friend WithEvents cboMotivo As System.Windows.Forms.ComboBox
    Friend WithEvents lblError As System.Windows.Forms.Label
    Friend WithEvents pnlFooter As System.Windows.Forms.Panel
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnConfirmar As System.Windows.Forms.Button
    Friend WithEvents btnActualizar As Button
End Class