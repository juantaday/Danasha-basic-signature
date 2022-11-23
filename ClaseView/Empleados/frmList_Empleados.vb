Imports System.Data.SqlClient
Imports CADsisVenta
Imports CADsisVenta.DataSetEmployeeTableAdapters
Imports CADsisVenta.DataSetEmployee
Imports CADsisVenta.Data.Emuns.EnumSatateModule

Public Class frmList_Empleados

    Protected Friend idEmpleado As Integer
    Protected Friend idPersona As Integer
    Private indexRow As Integer
    Protected Friend stateClient As stateClient
    Private estaCargado As Boolean
    Sub New(stateClient As stateClient)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        Me.stateClient = stateClient
    End Sub
    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles AddNewEmplToList.Click
        Try
            Using listPerson As New frmList_Person(stateLoad.Dialogo)
                With listPerson
                    .StartPosition = FormStartPosition.CenterScreen
                    .ShowDialog()
                    If .DialogResult = DialogResult.OK Then
                        Using addEmployee As New frmAdd_Empleados(stateLoad.Dialogo, stateClient.Admin, stateOperation.Insert)
                            addEmployee.idPersona = listPerson.idPersona
                            With addEmployee
                                .ShowDialog()
                                If addEmployee.DialogResult = DialogResult.OK Then
                                    Carga_ListEmployee()
                                Else
                                    sql = sql
                                End If
                            End With
                        End Using
                    End If
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.No
        Me.Close()
    End Sub

    Private Sub btnok_Click(sender As Object, e As EventArgs) Handles btnok.Click
        Try
            idPersona = dtg.SelectedCells(dtg.Columns("idPersona").Index).Value
            idEmpleado = dtg.SelectedCells(dtg.Columns("idEmpleado").Index).Value
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub txtCliente_Select_TextChanged(sender As Object, e As EventArgs) Handles txtEmple_Busca.TextChanged
        Act_BtnBuscar()
    End Sub

    Private Sub txtEmple_Busca_Enter(sender As Object, e As EventArgs) Handles txtEmple_Busca.Enter
        Act_BtnBuscar()
    End Sub
    Private Sub Act_BtnBuscar()
        If Me.txtEmple_Busca.Text.Length > 0 Then
            Me.AcceptButton = Me.btnBuscar
        Else
            Me.AcceptButton = Nothing
        End If
    End Sub

    Private Sub txtEmple_Busca_Leave(sender As Object, e As EventArgs) Handles txtEmple_Busca.Leave
        Me.AcceptButton = Nothing
    End Sub

    Private Sub frmList_Empleados_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PanelDetail.Width = 0
        Carga_ListEmployee()
        indexRow = -1
        ActivaControl()
    End Sub
    Private Sub ActivaControl()
        If stateClient = stateClient.Admin Then
            AddNewEmplToList.Enabled = True
        Else
            AddNewEmplToList.Enabled = False
        End If
    End Sub

    Private Sub Carga_ListEmployee()
        Try
            'clearFields()
            estaCargado = False
            Dim totalDeuda As Double = 0
            Using adat As New EmpleadoNameTableAdapter
                Using dt As New EmpleadoNameDataTable
                    adat.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        dtg.DataSource = dt
                        'estilo de datagrid
                        MyThemDataGridview()
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message + " en el carga_Empleados del " + Me.Name, MsgBoxStyle.Critical, "Error")
            dtg.DataSource = Nothing
        Finally
            estaCargado = True
            Selection_Row(idEmpleado)
        End Try
    End Sub
    Private Sub MyThemDataGridview()
        Try
            applyGridTheme(dtg)
            For Each col In dtg.Columns
                col.visible = False
            Next
            'visibleColumna de ruc
            dtg.Columns(dtg.Columns("Ruc_Ci").Index).Visible = True
            dtg.Columns(dtg.Columns("Ruc_Ci").Index).HeaderText = "Ruc. C.I"
            dtg.Columns(dtg.Columns("Ruc_Ci").Index).Width = 120
            'visibleColumna de elpledo
            dtg.Columns(dtg.Columns("Nombres").Index).Visible = True
            dtg.Columns(dtg.Columns("Nombres").Index).HeaderText = "Empleado"
            dtg.Columns(dtg.Columns("Nombres").Index).Width = 200
            'visibleColumna de Telefono
            dtg.Columns(dtg.Columns("telefono").Index).Visible = True
            dtg.Columns(dtg.Columns("telefono").Index).HeaderText = "Teléfono personal"
            dtg.Columns(dtg.Columns("telefono").Index).Width = 150
            'visibleColumna de Telefono casa
            dtg.Columns(dtg.Columns("telef_casa").Index).Visible = True
            dtg.Columns(dtg.Columns("telef_casa").Index).HeaderText = "Teléfono casa"
            dtg.Columns(dtg.Columns("telef_casa").Index).Width = 100
            'evento para contar lon mumeros de row
            AddHandler dtg.RowPostPaint, AddressOf rowPostPaint_HeaderCount
            Dim i As Integer
            For i = 0 To dtg.RowCount - 1
            Next
            Total_listLabel.Text = "Total lista : " + Convert.ToString(1)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Sub rowPostPaint_HeaderCount(sender As Object, e As DataGridViewRowPostPaintEventArgs)
        'set rowheader count
        Dim grid As DataGridView = CType(sender, DataGridView)
        Dim rowIdx As String = (e.RowIndex + 1).ToString()
        Dim centerFormat = New StringFormat()
        centerFormat.Alignment = StringAlignment.Center
        centerFormat.LineAlignment = StringAlignment.Center
        Dim headerBounds As Rectangle = New Rectangle(e.RowBounds.Left, e.RowBounds.Top,
            grid.RowHeadersWidth, e.RowBounds.Height - sender.rows(e.RowIndex).DividerHeight)
        e.Graphics.DrawString(rowIdx, grid.Font, SystemBrushes.ControlText,
            headerBounds, centerFormat)
    End Sub
    Sub clearFields()
        PanelView.Controls.Clear()
        Refresh()
    End Sub
    Private Function Insert_Employee(idpersona As Integer, genero As String) As Boolean
        Try
            Dim repotTo As Nullable(Of Integer) = Nothing
            Dim data As New EmpleadosTableAdapter
            Dim respont As Integer
            respont = data.Insert(idpersona, genero, "Empleado", 0, Date.Now, repotTo)
            If respont > 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function
    Private Function Update_Employee(idempledo As Integer) As Boolean
        Try
            'identidad de persona
            If Not (idempledo > 0) Then
                MsgBox("No se pudo Validar el id del empleado")
            End If
            'validacionde datos
            If TituloComboBox.SelectedIndex < 0 Then
                ErrorProvider.SetError(TituloComboBox, "Seleccione uno de la lista")
                Return False
            Else
                ErrorProvider.SetError(TituloComboBox, String.Empty)
            End If
            'validacion de cargo
            If CargoComboBox.SelectedIndex < 0 Then
                ErrorProvider.SetError(CargoComboBox, "Seleccione uno de la lista")
                Return False
            Else
                ErrorProvider.SetError(CargoComboBox, String.Empty)
            End If
            'inserto los datos
            Dim data As New EmpleadosTableAdapter

            Dim respont As Integer
            respont = data.UpdateEmpledo(TituloComboBox.Text,
                                         CargoComboBox.Text,
                                         SueldoNumericUpDown.Value,
                                         FechaIngresoDateTimePicker.Value,
                                         idempledo)

            If respont > 0 Then
                Return True
            End If
            Return False

Salida_Validacion:
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function
    Private Sub datalistado_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        btnok.PerformClick()
    End Sub
    Private Sub btnEliminarCliente_Click(sender As Object, e As EventArgs)
        If (MsgBox("Está seguro de Eliminar?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Exclamation, "Responda")) = MsgBoxResult.Yes Then
        End If
    End Sub

    Private Function Elimina_Empleado(ByVal idempleado As Integer) As Boolean
        conecta_sql()

        sql = "Delete Empleados from Empleados where idEmpleado = " & idempleado & " "
        Try
            Using cmd As New SqlCommand(sql, Cnn_sql)
                cmd.CommandType = CommandType.Text
                If cmd.ExecuteNonQuery Then
                    Return True
                Else
                    Return False
                End If

            End Using
        Catch ex As Exception
            MsgBox(ex.Message + " en el Elimina_Empleado del " + Me.Name, MsgBoxStyle.Critical, "Error")
            Return False
        End Try

    End Function

    Private Function Elimina_Persona(ByVal idpersona As Integer) As Boolean
        conecta_sql()

        sql = "Delete personas from personas where idPersona = " & idpersona & ""
        Try
            Using cmd As New SqlCommand(sql, Cnn_sql)
                cmd.CommandType = CommandType.Text
                If cmd.ExecuteNonQuery Then
                    Return True
                Else
                    Return False
                End If

            End Using

        Catch ex As Exception
            MsgBox(ex.Message + " en el Elimina_Persona del " + Me.Name, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function

    Private Sub btnEditarCliente_Click(sender As Object, e As EventArgs) Handles EdidEmployeeCliente.Click
        Using UpdateEmploye As New frmAdd_Personas(stateOperation.Update, Me.idPersona)
            With UpdateEmploye
                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
                If .DialogResult = DialogResult.OK Then
                    Me.notificacion.Visible = True
                    Me.notificacion.ShowBalloonTip(1000, "Aviso", "Modificación exitosa", ToolTipIcon.Info)
                End If
            End With
        End Using
    End Sub

    Private Sub datalistado_Leave(sender As Object, e As EventArgs)
        Me.AcceptButton = Nothing
    End Sub

    Private Sub Carga_Image(sender As System.Windows.Forms.DataGridView, index As Integer)
        Try
            If Not (sender.SelectedRows.Count = 1) Then
                Return
            End If

            Dim img() As Byte = Nothing
            If IsArray(sender.SelectedCells.Item(sender.Columns("foto").Index).Value) Then
                img = sender.SelectedCells.Item(sender.Columns("foto").Index).Value
            End If

            If Not IsNothing(img) Then
                If img.Length > 0 Then
                    Dim ms As New IO.MemoryStream(img)
                    PictureBox1.Image = Image.FromStream(ms)
                    PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
                Else
                    If Boolean.Parse(sender.SelectedCells.Item(sender.Columns("genero").Index).Value) Then
                        PictureBox1.Image =  Global.DanashaBasic.My.Resources.Person_128png
                        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
                    Else
                        PictureBox1.Image =  Global.DanashaBasic.My.Resources.Person_128_Won_png
                        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
                    End If
                End If
            Else
                If Boolean.Parse(sender.SelectedCells.Item(sender.Columns("genero").Index).Value) Then
                    PictureBox1.Image =  Global.DanashaBasic.My.Resources.Person_128png
                    PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
                Else
                    PictureBox1.Image =  Global.DanashaBasic.My.Resources.Person_128_Won_png
                    PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        sql = ""
        Dim ultimotexto As String = ""

        If String.IsNullOrWhiteSpace(txtEmple_Busca.Text) Then
            Carga_ListEmployee()
        ElseIf txtEmple_Busca.Text.Length > 2 Then
            For Each texto In txtEmple_Busca.Text
                If String.IsNullOrWhiteSpace(texto) Then
                    If Not (ultimotexto = "%") Then
                        sql = sql & "%"
                    End If
                Else
                    sql = sql & texto
                End If
                ultimotexto = texto
            Next
            Carga_ListEmployeeByWhere(sql)
        Else
            MsgBox("Pocos parámetros para consultar", MsgBoxStyle.Information, "Aviso")
            txtEmple_Busca.Focus()
        End If
    End Sub
    Private Sub Carga_ListEmployeeByWhere(where As String)
        Try
            Try
                'clearFields()
                estaCargado = False
                Dim totalDeuda As Double = 0
                Using adat As New EmpleadoNameTableAdapter
                    Using dt As New EmpleadoNameDataTable
                        adat.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            dtg.DataSource = dt
                            'estilo de datagrid
                            MyThemDataGridview()
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MsgBox(ex.Message + " en el carga_Empleados del " + Me.Name, MsgBoxStyle.Critical, "Error")
                dtg.DataSource = Nothing
            Finally
                estaCargado = True
                Selection_Row(idEmpleado)
            End Try
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            dtg.DataSource = Nothing
        End Try
    End Sub
    Private Function Delete_Employee(idperson As Integer) As Boolean
        Try
            Dim data As New EmpleadosTableAdapter
            Dim respont As Integer
            respont = data.Insert(idPersona, String.Empty, "Empleado", 0, Date.Now, vbNull)
            If respont > 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function

    Private Sub Selection_Row(iempl As Integer)
        For index = 0 To dtg.RowCount - 1
            If iempl = dtg.Rows(index).Cells(dtg.Columns("idEmpleado").Index).Value Then
                dtg.Rows(index).Selected = True
                Return
            Else
                If dtg.Rows(index).Selected Then
                    dtg.Rows(index).Selected = False
                End If
            End If
        Next
    End Sub

    Private Sub ReportToButton_Click(sender As Object, e As EventArgs) Handles ReportToButton.Click
        Try
            Dim reportTo As Nullable(Of Integer)
            Using newListEm As New frmList_Empleados(stateClient.User)
                With newListEm
                    .ShowDialog()
                    If .DialogResult = System.Windows.Forms.DialogResult.OK Then
                        reportTo = .idEmpleado
                        If reportTo > 0 Then
                            Dim dat As New EmpleadosTableAdapter()
                            If dat.UpdateReportTo(reportTo, idEmpleado) = 1 Then
                                Carga_ListEmployee()
                            End If
                            dat = Nothing
                        End If
                    End If
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub dtg_RowStateChanged(sender As Object, e As DataGridViewRowStateChangedEventArgs) Handles dtg.RowStateChanged
        btnok.Enabled = False
        adminEmployeePanel.Enabled = False
        If stateClient = stateClient.Admin Then
            adminEmployeePanel.Enabled = True
        End If
        adminEmployeePanel.Enabled = False
        If e.Row.Selected And estaCargado Then
            If dtg.SelectedRows.Count = 1 Then
                Carga_Detail(sender, e.Row.Index)
                btnok.Enabled = True
                If stateClient = stateClient.Admin Then
                    EditDetailButton.Enabled = True
                    PanelMenuList.Enabled = True
                    If dtg.RowCount > 0 Then
                        adminEmployeePanel.Enabled = True
                        adminEmployeePanel.Enabled = True
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub Carga_Detail(sender As System.Windows.Forms.DataGridView, index As Integer)
        Try
            Dim reportTo As String = String.Empty
            'id de persona seleccionada
            idEmpleado = Convert.ToInt32(sender.Rows(index).Cells(sender.Columns("IdEmpleado").Index).Value)
            idPersona = Convert.ToInt32(sender.Rows(index).Cells(sender.Columns("IdPersona").Index).Value)
            indexRow = index
            'titulo
            TituloComboBox.Text = Convert.ToString(sender.Rows(index).Cells(sender.Columns("titulo").Index).Value)
            'cargo
            CargoComboBox.Text = Convert.ToString(sender.Rows(index).Cells(sender.Columns("cargo").Index).Value)
            'sueldo
            SueldoNumericUpDown.Value = Convert.ToDecimal(sender.Rows(index).Cells(sender.Columns("sueldo").Index).Value)
            'fecha de ingreso
            FechaIngresoDateTimePicker.Value = Convert.ToDateTime(sender.Rows(index).Cells(sender.Columns("fecha_ingre").Index).Value)
            'reporta a
            reportTo = Convert.ToString(sender.Rows(index).Cells(sender.Columns("reporta_A").Index).Value)
            If Not (String.IsNullOrWhiteSpace(reportTo)) Then
                Carga_DerportTo(Convert.ToInt32(reportTo))
            Else
                ReportToTextBox.Text = String.Empty
            End If
            'cargamos la imagen
            Carga_Image(sender, index)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub Carga_DerportTo(idempledo As Integer)
        Try
            ReportToTextBox.Text = String.Empty
            Dim tap As New EmpleadoNameTableAdapter
            Dim dt As New EmpleadoNameDataTable
            tap.FillByIdEmpleado(dt, idempledo)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    sql = dt.Rows(0)("cargo") + ":" + vbNewLine
                    sql = sql & dt.Rows(0)("Nombres") + vbNewLine
                    sql = sql & "(" + dt.Rows(0)("Ruc_Ci") + ")"
                    ReportToTextBox.Text = sql
                End If
                tap = Nothing
                dt = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")

        End Try
        Dim data As New EmpleadosTableAdapter
    End Sub

    Private Sub DeleteReportToButton_Click(sender As Object, e As EventArgs) Handles DeleteReportToButton.Click
        Try
            Dim repotTo As Nullable(Of Integer) = Nothing
            Dim dat As New EmpleadosTableAdapter()
            If dat.UpdateReportTo(repotTo, idEmpleado) = 1 Then
                Carga_ListEmployee()
            End If
            dat = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub EditDetailButton_Click(sender As Object, e As EventArgs) Handles EditDetailButton.Click
        Try
            Using EditEmployee As New frmAdd_Empleados(stateLoad.Dialogo, stateClient.Admin, stateOperation.Update)
                With EditEmployee
                    .idEmpleado = Me.idEmpleado
                    .StartPosition = FormStartPosition.CenterScreen
                    .ShowDialog()
                    If .DialogResult = DialogResult.OK Then
                        btnBuscar.PerformClick()
                    End If
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub UpdateDetailButton_Click(sender As Object, e As EventArgs) Handles UpdateDetailButton.Click
        Try
            If Update_Employee(idEmpleado) Then
                Carga_ListEmployee()
                PaneMenu.Enabled = True
                PanelList.Enabled = True
                PanelDetail.Enabled = False
                PanelDetail.Width = 0
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub CandelDeatilButton_Click(sender As Object, e As EventArgs) Handles CandelDeatilButton.Click
        PanelDetail.Width = 0
        PanelDetail.Enabled = False
        If dtg.RowCount > 0 Then
            EditDetailButton.Enabled = True
        End If
        dtg.Focus()
        PanelList.Enabled = True
        PanelBusq.Enabled = True
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs)

    End Sub


    Private Sub dtg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dtg.CellClick
        btnok.Enabled = False
        adminEmployeePanel.Enabled = False
        If stateClient = stateClient.Admin Then
            adminEmployeePanel.Enabled = True
            Me.EdidEmployeeCliente.Enabled = True
            Me.deleteEmployeeButton.Enabled = True
        End If
        If dtg.SelectedRows.Count = 1 And estaCargado Then
            If dtg.SelectedRows.Count = 1 Then
                btnok.Enabled = True
                If stateClient = stateClient.Admin Then
                    EditDetailButton.Enabled = True
                    PanelMenuList.Enabled = True
                    If dtg.RowCount > 0 Then
                        adminEmployeePanel.Enabled = True
                        adminEmployeePanel.Enabled = True
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub EditCardButton_Click(sender As Object, e As EventArgs) Handles EditCardButton.Click
        Try
            Using EditEmployee As New frmAdd_Empleados(stateLoad.Dialogo, stateClient.Admin, stateOperation.View)
                With EditEmployee
                    .idEmpleado = Me.idEmpleado
                    .StartPosition = FormStartPosition.CenterScreen
                    .ShowDialog()
                    If .DialogResult = DialogResult.OK Then
                        btnBuscar.PerformClick()
                    End If
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub BorrarDeLaListaDeEmpleadosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BorrarDeLaListaDeEmpleadosToolStripMenuItem.Click
        Try
            Dim resp As Integer
            If (MsgBox("Está seguro de eliminar..?",
            MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo +
            MsgBoxStyle.DefaultButton2, "Responda")) = MsgBoxResult.Yes Then
                Dim dap As New EmpleadosTableAdapter
                resp = dap.DeleteEmployee(idEmpleado)
                If resp = 1 Then
                    Carga_ListEmployee()
                End If
            End If
        Catch ex As Exception
            If ex.Message.Contains("REFERENCE") Then
                MsgBox("Este empleado tiene información realacionado con otros datos" + vbNewLine +
                       "No se puede borrar.", MsgBoxStyle.Exclamation, "Importante")
            Else
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            End If
        End Try
    End Sub
    Private Sub EliminarTotaLaInformacionDeEsteEmpleadoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EliminarTotaLaInformacionDeEsteEmpleadoToolStripMenuItem.Click
        Try
            Dim rest As Integer
            If (MsgBox("Está seguro de eliminar toda su informacíon..?",
            MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo +
            MsgBoxStyle.DefaultButton2, "Responda")) = MsgBoxResult.Yes Then
                rest = ClsEmployee.DeleteEmployeeAll(idEmpleado)
                If rest = 1 Then
                    Carga_ListEmployee()
                ElseIf rest = -1 Then
                    MsgBox("Hay personas que se reporta a este empleado.." + vbNewLine +
                           "borra esa opcíón para poder eliminar.", MsgBoxStyle.Exclamation, "Aviso")
                End If
            End If
        Catch ex As Exception
            If ex.Message.Contains("REFERENCE") Then
                MsgBox("Este empleado tiene información realacionado con otros datos" + vbNewLine +
                       "No se puede borrar.", MsgBoxStyle.Exclamation, "Importante")
            Else
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            End If
        End Try
    End Sub

    Private Sub deleteEmployeeButton_MouseDown(sender As Object, e As MouseEventArgs) Handles deleteEmployeeButton.MouseDown
        If (e.Button = System.Windows.Forms.MouseButtons.Left) Then
            Dim Menu As ContextMenuStrip = Me.deleteContextMenuStrip()
            Menu.Show(Cursor.Position)
        End If
    End Sub

    Private Sub dtg_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles dtg.DataError
        Try
            If Not e.ColumnIndex = 8 Then
                MsgBox(e.Exception.ToString, MsgBoxStyle.Critical, "Error")
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class