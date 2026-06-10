Imports System.Data.SqlClient
Imports System.Text
Imports CADsisVenta.Data.Emuns.EnumSatateModule
Imports CADsisVenta.DataSetZonasTableAdapters
Imports Domain.Data.Entities
Imports Domain.Data.Repositories
Imports ec.gob.sri.Models
Public Class frmAdd_Almacen
    Private Const TailscaleKey As String = "fdg36125☺}♫825╩5-5645644○87m4:█J"
    Private estaCargado As Boolean
    Protected Friend idBodega As Integer
    Private idResponsable1, idResponsable2, idResponsable3 As Integer
    Private esSucursalRemota As Boolean
    Private ciudadSucursal As String

    Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub
    Private Sub Carga_Bodegas()
        estaCargado = False
        sql = "SELECT TOP (5) b.idBodega, b.Nom_Bodega AS [Bodega o local], b.Telef1_Bodega AS Telf_Bodega,  "
        sql = sql & "p.Apellidos + ' ' + p.Nombre AS Responable, b.Des_Bodega, b.Direc_Bodega, b.Resp1_idEmpleado, "
        sql = sql & "b.Fecha_Apertura, p.Ruc_Ci, Resp2_idEmpleado, "
        sql = sql & "(select top(1) per2.Ruc_Ci + ' ' +per2.Apellidos + ' ' + per2.Nombre from Empleados as em2  "
        sql = sql & "inner join Personas as per2 on em2.idPersona = per2.idPersona  "
        sql = sql & "where em2.idEmpleado = b.Resp2_idEmpleado) as [AutorCheque], tb.Nom_typoBodega as [Tipo], b.TypoBodega, b.CodEstablec "
        sql = sql & ", b.EsSucursalRemota, b.CiudadSucursal, b.TailscaleIp, b.TailscaleUsuario, b.TailscalePassword, b.TailscaleDatabase "
        sql = sql & "FROM  dbo.Bodegas AS b  "
        sql = sql & "INNER JOIN dbo.Empleados AS e ON b.Resp1_idEmpleado = e.idEmpleado  "
        sql = sql & "INNER JOIN  dbo.Personas AS p ON e.idPersona = p.idPersona "
        sql = sql & "INNER JOIN TypoBodega as tb on tb.idTypoBodega = b.TypoBodega "
        sql = sql & "ORDER BY b.idBodega;"


        Try
            Using cnn = New SqlConnection(DomainSQLite.Setting.Configuration.ConectionString)
                cnn.Open()
                Using cmd As New SqlCommand(sql, cnn)
                    Dim dat As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable
                    dat.Fill(dt)
                    Me.datalistado.Columns.Clear()
                    Me.datalistado.DataSource = Nothing
                    If dt.Rows.Count > 0 Then
                        Me.datalistado.DataSource = dt
                        If Not Me.datalistado.Columns.Contains("ConfigurarRemoto") Then
                            Dim configColumn As New DataGridViewButtonColumn()
                            configColumn.Name = "ConfigurarRemoto"
                            configColumn.HeaderText = "Remoto"
                            configColumn.Text = "Configurar"
                            configColumn.UseColumnTextForButtonValue = True
                            configColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                            configColumn.DisplayIndex = 0
                            Me.datalistado.Columns.Add(configColumn)
                        End If
                        Me.datalistado.Columns(0).Visible = False  'idBodega
                        Me.datalistado.Columns(4).Visible = False  'apellidos + nombre de responsable
                        Me.datalistado.Columns(5).Visible = False  'direccion de bodega
                        Me.datalistado.Columns(6).Visible = False  'id empleado del responsable
                        Me.datalistado.Columns(8).Visible = False  'Ruc de responsable
                        Me.datalistado.Columns(9).Visible = False  'id del emplado de cheque
                        Me.datalistado.Columns(10).Visible = False  'responsable del cheque
                        Me.datalistado.Columns(12).Visible = False  'tipo de bodegaS
                        Me.datalistado.Columns(13).Visible = False  'codigo establec
                        Me.datalistado.Columns(14).Visible = False  'EsSucursalRemota
                        Me.datalistado.Columns(15).Visible = False  'CiudadSucursal
                        Me.datalistado.Columns(16).Visible = False  'TailscaleIp
                        Me.datalistado.Columns(17).Visible = False  'TailscaleUsuario
                        Me.datalistado.Columns(18).Visible = False  'TailscalePassword
                        Me.datalistado.Columns(19).Visible = False  'TailscaleDatabase
                        Me.datalistado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                    End If
                End Using
            End Using

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Me.datalistado.DataSource = Nothing
        Finally
            estaCargado = True
        End Try
    End Sub

    Private Sub brnAddPesponsable_Click(sender As Object, e As EventArgs) Handles brnAddPesponsable.Click
        Using newResponsa As New frmList_Empleados(stateClient.Admin)
            With newResponsa
                .ShowDialog()
                If .DialogResult = DialogResult.OK Then
                    idResponsable1 = .idEmpleado
                    sql = "(" + .dtg.SelectedCells.Item(.dtg.Columns("Ruc_Ci").Index).Value & ") "
                    sql = sql & .dtg.SelectedCells.Item(.dtg.Columns("Nombres").Index).Value
                    Me.txtresponsable.Text = sql
                End If
            End With

        End Using
    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If Not ValidaDatos() Then Exit Sub
        Dim Exito As Boolean = False
        Try
            Me.Cursor = Cursors.WaitCursor
            esSucursalRemota = chkEsSucursalRemota.Checked
            ciudadSucursal = txtCiudadSucursal.Text.Trim()
            If idBodega = -1 Then
                If Agregar_Bodega() Then
                    Exito = True
                End If
            ElseIf idBodega > 0 Then
                If Modificar_Bodega() Then
                    Exito = True
                End If
            End If
            If Exito Then
                LimpiaContenido()
                BloqueaControles(False)
                Carga_Bodegas()
                Me.NotifyIcon1.Visible = True
                Me.NotifyIcon1.ShowBalloonTip(2000, "Aviso", "Operacion Exitosa", ToolTipIcon.Info)
            Else
                Me.NotifyIcon1.Visible = True
                Me.NotifyIcon1.ShowBalloonTip(2000, "Alerta", "La oreracion no fue realizada", ToolTipIcon.Error)
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message + " en el btnAceptar_Click del " + Me.Name, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub BloqueaControles(ByVal Estado As Boolean)
        For Each ctrls In Me.pnlEntrada.Controls
            ctrls.enabled = Estado
        Next
        For Each ctrls In Me.PnlListado.Controls
            ctrls.enabled = Not Estado
        Next
    End Sub
    Private Function Agregar_Bodega() As Boolean
        Try
            Dim resp1 As Nullable(Of Integer) = Nothing
            Dim resp2 As Nullable(Of Integer) = Nothing
            Dim resp3 As Nullable(Of Integer) = Nothing
            If idResponsable1 > 0 Then resp1 = idResponsable1
            If idResponsable2 > 0 Then resp2 = idResponsable2
            If idResponsable3 > 0 Then resp3 = idResponsable3
            Dim entidad As New Bodega With {
                .NomBodega = NomBodegaText.Text,
                .DescripcionBodega = DescripcionBodegaText.Text,
                .DireccionBodega = DireccionText.Text,
                .Telefono1Bodega = telefono1Text.Text,
                .Telefono2Bodega = telefono2Text.Text,
                .Telefono3Bodega = telefono3TextBox.Text,
                .Resp1IdEmpleado = resp1,
                .Resp2IdEmpleado = resp2,
                .Resp3IdEmpleado = resp3,
                .FechaApertura = txtFecha_Apert.Value,
                .FechaRegistro = DateTime.Now,
                .TypoBodega = TypoBodegaComboBox.SelectedValue,
                .CodEstablec = txtCodigoEstab.Text,
                .EsSucursalRemota = esSucursalRemota,
                .CiudadSucursal = ciudadSucursal
            }

            idBodega = BodegaRepository.Insert(entidad, DomainSQLite.Setting.Configuration.ConectionString)
            Return idBodega > 0
        Catch ex As Exception
            MsgBox(ex.Message + " en el Agregar_Bodega del " + Me.Name, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function

    Private Function ValidaDatos() As Boolean
        If Me.ValidateChildren And Not String.IsNullOrWhiteSpace(DireccionText.Text) And
            Not String.IsNullOrWhiteSpace(txtCodigoEstab.Text) And
            Me.NomBodegaText.Text.Length > 0 And idResponsable1 > 0 Then
            If TypoBodegaComboBox.SelectedIndex <= 0 Then
                MsgBox(msgSelect_list, MsgBoxStyle.Information, "Aviso")
                TypoBodegaComboBox.Focus()
                Return False
            End If

            If Not (txtCodigoEstab.Text.Length = 3) Then
                ErrorIcono.SetError(txtCodigoEstab, "Debe contener mínimo tres dijitos..")
                Return False
            End If

            Return True
        Else
            Return False
        End If
    End Function


    Private Function Modificar_Bodega() As Boolean
        Try
            Dim resp1 As Nullable(Of Integer) = Nothing
            Dim resp2 As Nullable(Of Integer) = Nothing
            Dim resp3 As Nullable(Of Integer) = Nothing
            If idResponsable1 > 0 Then resp1 = idResponsable1
            If idResponsable2 > 0 Then resp2 = idResponsable2
            If idResponsable3 > 0 Then resp3 = idResponsable3
            Dim entidad As New Bodega With {
                .IdBodega = idBodega,
                .NomBodega = NomBodegaText.Text,
                .DescripcionBodega = DescripcionBodegaText.Text,
                .DireccionBodega = DireccionText.Text,
                .Telefono1Bodega = telefono1Text.Text,
                .Telefono2Bodega = telefono2Text.Text,
                .Telefono3Bodega = telefono3TextBox.Text,
                .Resp1IdEmpleado = resp1,
                .Resp2IdEmpleado = resp2,
                .Resp3IdEmpleado = resp3,
                .FechaApertura = txtFecha_Apert.Value,
                .TypoBodega = TypoBodegaComboBox.SelectedValue,
                .CodEstablec = txtCodigoEstab.Text,
                .EsSucursalRemota = esSucursalRemota
            }

            Return BodegaRepository.Update(entidad, DomainSQLite.Setting.Configuration.ConectionString)
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function
    Private Sub txtNombre_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles NomBodegaText.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.ErrorIcono.SetError(sender, "")
        Else
            Me.ErrorIcono.SetError(sender, "Ingrese el nombre del almacen o bodega")
        End If
    End Sub

    Private Sub frmAdd_Almacen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.txtFecha_Apert.Value = Now()
        BloqueaControles(False)
        Carga_TypoBodega()
        Carga_Bodegas()
        Me.Cursor = Cursors.Default
    End Sub
    Private Sub Carga_TypoBodega()
        Try
            Dim data As New TypoBodegaTableAdapter
            TypoBodegaComboBox.DataSource = data.GetDataByAll()
            TypoBodegaComboBox.ValueMember = "idTypoBodega"
            TypoBodegaComboBox.DisplayMember = "Nom_typoBodega"
            TypoBodegaComboBox.SelectedIndex = 0
            data = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub txtresponsable_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtresponsable.Validating
        If DirectCast(sender, TextBox).Text.Length > 0 Then
            Me.ErrorIcono.SetError(sender, "")
        Else
            Me.ErrorIcono.SetError(sender, "Ingrese la persona responsable de este lugar")
        End If
    End Sub

    Private Sub txtNombre_Leave(sender As Object, e As EventArgs) Handles NomBodegaText.Leave
        Me.NomBodegaText.Text = Trim(Me.NomBodegaText.Text)
    End Sub
    Private Sub EstaSeleccionado()
        If Me.datalistado.SelectedRows.Count = 1 Then
            Me.btnElimina.Enabled = True
            Me.btnModifica.Enabled = True
            Me.txtidbodega.Text = Me.datalistado.SelectedCells.Item(0).Value
            CopiaDatos()
        Else
            Me.txtidbodega.Text = 0
            Me.btnElimina.Enabled = False
            Me.btnModifica.Enabled = False
            LimpiaContenido()
        End If
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        LimpiaContenido()
        BloqueaControles(True)
    End Sub
    Private Sub LimpiaContenido()
        idBodega = -1
        Me.txtidbodega.Text = 0
        Me.NomBodegaText.Text = ""
        Me.DescripcionBodegaText.Text = ""
        Me.DireccionText.Text = ""
        Me.telefono1Text.Text = ""
        Me.txtresponsable.Text = ""
        idResponsable1 = 0
        idResponsable2 = 0
        idResponsable3 = 0
        txtCheque.Text = ""
        chkEsSucursalRemota.Checked = False
        txtCiudadSucursal.Text = ""

    End Sub

    Private Sub btnElimina_Click(sender As Object, e As EventArgs) Handles btnElimina.Click
        If (MsgBox("Esta seguro de Eliminar", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Aviso")) = MsgBoxResult.Yes Then
            NotifyIcon1.Visible = True
            If EliminaBodega(Me.txtidbodega.Text) Then
                Carga_Bodegas()
                NotifyIcon1.ShowBalloonTip(2000, "Aviso", "Eliminacio Exitos", ToolTipIcon.Info)
            Else
                NotifyIcon1.ShowBalloonTip(2000, "Alerta", "No se elimino ", ToolTipIcon.Error)
            End If
        End If
    End Sub
    Private Function EliminaBodega(ByVal idBodega As Integer) As Boolean
        Try
            Return BodegaRepository.Delete(idBodega, DomainSQLite.Setting.Configuration.ConectionString)

        Catch ex As Exception
            MsgBox(ex.Message + "en el EliminaBodega del " + Me.Name, MsgBoxStyle.Critical, "Error")
            Return False
        End Try

    End Function

    Private Sub btnModifica_Click(sender As Object, e As EventArgs) Handles btnModifica.Click
        If Me.datalistado.SelectedRows.Count = 1 Then
            BloqueaControles(True)
            CopiaDatos()
        End If
    End Sub
    Private Sub CopiaDatos()
        Try
            idBodega = Me.datalistado.SelectedCells.Item(0).Value
            Me.NomBodegaText.Text = Me.datalistado.SelectedCells.Item(1).Value
            Me.telefono1Text.Text = Me.datalistado.SelectedCells.Item(2).Value.ToString()
            idResponsable1 = Me.datalistado.SelectedCells.Item(6).Value
            Me.txtresponsable.Text = Me.datalistado.SelectedCells.Item(8).Value + " " + Me.datalistado.SelectedCells.Item(3).Value
            Me.DescripcionBodegaText.Text = Me.datalistado.SelectedCells.Item(4).Value.ToString()
            Me.DireccionText.Text = Me.datalistado.SelectedCells.Item(5).Value.ToString()
            Me.txtFecha_Apert.Text = Me.datalistado.SelectedCells.Item(7).Value
            Me.TypoBodegaComboBox.SelectedValue = datalistado.SelectedCells.Item(12).Value
            Me.txtCodigoEstab.Text = datalistado.SelectedCells.Item(13).Value

            Me.esSucursalRemota = If(IsDBNull(datalistado.SelectedCells.Item(14).Value), False, CBool(datalistado.SelectedCells.Item(14).Value))
            Me.chkEsSucursalRemota.Checked = esSucursalRemota

            If IsNumeric(datalistado.SelectedCells.Item(9).Value) Then
                idResponsable2 = datalistado.SelectedCells.Item(9).Value
                Me.txtCheque.Text = Convert.ToString(datalistado.SelectedCells.Item(10).Value)
            Else
                idResponsable2 = 0
                Me.txtCheque.Text = ""
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        LimpiaContenido()
        BloqueaControles(False)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using Form = New frmList_Empleados(stateClient.Admin)

            Form.ShowDialog()

            If Form.DialogResult = DialogResult.OK Then
                idResponsable2 = id
                Me.txtCheque.Text = sql
            End If
        End Using
    End Sub

    Private Sub DireccionText_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles DireccionText.Validating
        If Not String.IsNullOrEmpty(DirectCast(sender, TextBox).Text) Then
            Me.ErrorIcono.SetError(sender, "")
        Else
            Me.ErrorIcono.SetError(sender, "Ingrese la dirección del establecimiento..")
        End If
    End Sub

    Private Sub RjTextBox1_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtCodigoEstab.Validating
        If Not String.IsNullOrEmpty(DirectCast(sender, JMControls.Controls.RJTextBox).Text) Then
            Me.ErrorIcono.SetError(sender, "")
        Else
            Me.ErrorIcono.SetError(sender, "Ingrese el código del establecimiento..")
        End If
    End Sub

    Private Sub datalistado_RowStateChanged(sender As Object, e As DataGridViewRowStateChangedEventArgs) Handles datalistado.RowStateChanged
        If e.StateChanged And estaCargado Then
            EstaSeleccionado()
        End If
    End Sub


    Private Sub AbrirConfiguracionAvanzada()

        If Not datalistado.SelectedRows.Count = 1 Then
            MsgBox("Seleccione una bodega para configurar la conexión remota.", MsgBoxStyle.Exclamation, "Aviso")
            Return
        End If

        If idBodega <= 0 Then
            MsgBox("Guarde la bodega antes de configurar la conexión remota.", MsgBoxStyle.Exclamation, "Aviso")
            Return
        End If

        Dim tailscaleIp As String = ""
        Dim tailscaleUsuario As String = ""
        Dim tailscalePassword As String = ""
        Dim tailscaleDatabase As String = ""

        ' --- NUEVO: Cargar campos EsSucursalRemota y CiudadSucursal ---
        If datalistado.Columns.Contains("EsSucursalRemota") Then
            chkEsSucursalRemota.Checked = CBool(Me.datalistado.SelectedRows(0).Cells("EsSucursalRemota").Value)
        End If
        If datalistado.Columns.Contains("CiudadSucursal") Then
            txtCiudadSucursal.Text = Me.datalistado.SelectedRows(0).Cells("CiudadSucursal").Value.ToString()
        End If
        If datalistado.Columns.Contains("TailscaleIp") Then
            tailscaleIp = Convert.ToString(Me.datalistado.SelectedRows(0).Cells("TailscaleIp").Value)
        End If
        If datalistado.Columns.Contains("TailscaleUsuario") Then
            tailscaleUsuario = Convert.ToString(Me.datalistado.SelectedRows(0).Cells("TailscaleUsuario").Value)
        End If
        If datalistado.Columns.Contains("TailscalePassword") Then
            tailscalePassword = Convert.ToString(Me.datalistado.SelectedRows(0).Cells("TailscalePassword").Value)
        End If
        If datalistado.Columns.Contains("TailscaleDatabase") Then
            tailscaleDatabase = Convert.ToString(Me.datalistado.SelectedRows(0).Cells("TailscaleDatabase").Value)
        End If

        Using frm As New frmConfigBodegaAvanzada(idBodega,
            tailscaleIp, tailscaleUsuario, tailscalePassword, tailscaleDatabase)
            If frm.ShowDialog(Me) = DialogResult.OK Then

                Carga_Bodegas()
            End If
        End Using
    End Sub

    Private Sub datalistado_CellsClick(sender As Object, e As DataGridViewCellEventArgs) Handles datalistado.CellClick
        If estaCargado Then
            EstaSeleccionado()
        End If
    End Sub



    Private Sub datalistado_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles datalistado.CellContentClick
        If e.RowIndex < 0 Then
            Return
        End If

        If datalistado.Columns(e.ColumnIndex).Name = "ConfigurarRemoto" Then
            datalistado.ClearSelection()
            datalistado.Rows(e.RowIndex).Selected = True
            EstaSeleccionado()
            AbrirConfiguracionAvanzada()
        End If
    End Sub

    Private Sub chkEsSucursalRemota_CheckedChanged(sender As Object, e As EventArgs) Handles chkEsSucursalRemota.CheckedChanged
        txtCiudadSucursal.Enabled = chkEsSucursalRemota.Checked
    End Sub


End Class