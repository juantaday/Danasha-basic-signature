Imports System.Data.SqlClient
Imports CADsisVenta
Imports CADsisVenta.Class
Imports CADsisVenta.Data.Emuns.EnumSatateModule
Imports CADsisVenta.Data.ModelsSend
Imports CADsisVenta.Funtions

Public Class frmList_clientes
    Protected Friend txtFlag As String
    Protected Friend idPersona As Integer
    Private idCliente As Integer
    Private stateClient As stateClient
    Sub New(stateClient As stateClient)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        Me.stateClient = stateClient
    End Sub
    Private Sub frmList_Empleados_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargaListClientes("")
    End Sub

    Private Sub btnBuscar_Click(sender As System.Object, e As System.EventArgs) Handles btnBuscar.Click
        Try

            If (txtCliente_Select.Text.Trim().Length < 4) Then
                ErrorProvider1.SetError(txtCliente_Select, "Pocos argumentos para consultar..")
                Return
            End If

            ErrorProvider1.SetError(txtCliente_Select, String.Empty)


            ' Evalua los datos a buscar 
            Dim response = GeneratedSplit.GenerateSpliter(txtCliente_Select.Text)
            If Not response.IsSucces Then
                MsgBox("No se pudo analizar los datos a consultar..")
                Return
            ElseIf (response.IsNumeric) Then
                CargaListClientes($"Where p.Ruc_Ci like '{response.Spliter(0)}%' ")
            Else
                Me.Cursor = Cursors.WaitCursor

                Dim data = ClsPerson.getDataLikePerson(response.Spliter(0), response.Spliter(1), response.Spliter(2))
                If (data Is Nothing OrElse data.Rows.Count = 0) Then
                    DataGridView1.Visible = False
                    lblnoExiste.Visible = True
                    Return
                ElseIf (data.Rows.Count > 25) Then
                    DataGridView1.Visible = False
                    lblnoExiste.Visible = True
                    MsgBox("Pocos argumentos." & vbNewLine & "Introduzca mas argumentos para buscar..")
                    Return
                Else  'Aseguro que toda la lista pertenece a clientes...

                    Dim info As New List(Of CheckClinetWithItemSend)

                    For Each row As DataRow In data.Rows
                        info.Add(New CheckClinetWithItemSend With {
                                 .idPersona = row.Field(Of Integer)("idPersona")})
                    Next

                    GetClientWithList(info)
                End If
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub CargaListClientes(ByVal FilterString As String)

        sql = "Select top(50) c.idCliente, p.Ruc_Ci, p.Apellidos + ' ' + p.Nombre AS [Cliente], "
        sql = sql & "p.telefono,p.Apellidos,p.Nombre,p.Direccion, p.idPersona, cast(c.credito as bit) as Credito, c.monto_Max "
        sql = sql & "FROM  dbo.Clientes as c INNER JOIN dbo.Personas as p ON c.idPersona = p.idPersona  "

        'filtro si me ponen condicion en el txtProduc_Select
        If Len(FilterString) > 0 Then
            sql = sql & FilterString &
                "ORDER BY p.Apellidos, p.Nombre;"
        Else
            sql = sql & "ORDER BY [Cliente] desc;"
        End If

        Try

            lblTotal.Text = "Total de registro: 0"
            lblnoExiste.Visible = False
            DataGridView1.DataSource = Nothing

            Using cmd As New CADsisVenta.Funtions.SqlComandExec()
                ViewData(cmd.RetornaTabla(sql))
            End Using
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
            lblTotal.Text = "Total de registro: 0"
        End Try
    End Sub


    Private Sub GetClientWithList(ByVal data As List(Of CheckClinetWithItemSend))
        Try
            Dim dt = CADsisVenta.Helpers.DataHelper.ToDataTable(data)

            Using cmd As New SqlComandExec()

                cmd.CommandType = CommandType.StoredProcedure

                cmd.ParameterCollection = New SqlParameter() {
                    New SqlParameter With
                    {
                            .ParameterName = "@datalis",
                            .Value = dt,
                            .SqlDbType = SqlDbType.Structured,
                            .TypeName = "[dbo].[CheckClinetWithData]"
                       }
                }

                ViewData(cmd.RetornaTabla("[dbo].[GetClientWithDataList]"))

            End Using
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
            lblTotal.Text = "Total de registro: 0"
        End Try
    End Sub

    Private Sub ViewData(ByVal dt As DataTable)
        If Not IsNothing(dt) Then
            If dt.Rows.Count > 0 Then
                With DataGridView1
                    .Visible = True
                    .DataSource = dt
                    .AutoSizeColumnsMode =
                        DataGridViewAutoSizeColumnsMode.AllCells
                    .Columns(0).Visible = False  'IdCliente
                    .Columns(4).Visible = False
                    .Columns(5).Visible = False
                    .Columns(6).Visible = True
                    .Columns(7).Visible = False  'id persona

                    If Not (Me.stateClient = stateClient.Admin) Then
                        .Columns(8).Visible = False  'permiso para creidito
                        .Columns(9).Visible = False  'monto maximo deuda
                    End If

                    clm = .Columns("monto_Max")
                    clm.HeaderText = "Monto máximo"
                    clm.DefaultCellStyle = myStileMoney

                    lblTotal.Text = "Total de registro: " & dt.Rows.Count
                    lblnoExiste.Visible = False
                End With
            End If

        End If
    End Sub


    Private Sub VisibleCabecera(ByVal Visible As Boolean)
        btnEditarCliente.Visible = Visible
        btnEliminarCliente.Visible = Visible
    End Sub
    Private Sub txtCliente_Select_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtCliente_Select.TextChanged
        Me.AcceptButton = btnBuscar
    End Sub


    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If Me.txtFlag = "Lote" Then

        ElseIf txtFlag = "Ventas" Then
            OK_Button.PerformClick()
        End If
    End Sub
    Private Sub Factura_AlCliente()
        If LoadOptionsPrint(1) Then
            If Me.txtFlag = "Lote" Then
                If MsgBox("Esta seguro de facturar a nombre de " & Me.DataGridView1.SelectedCells(2).Value, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Responda..") = MsgBoxResult.Yes Then
                    FacturVenta.idCliente = Me.DataGridView1.SelectedCells(0).Value
                    Me.DialogResult = DialogResult.OK
                    Me.Close()
                End If
            ElseIf Me.txtFlag = "Ventas" Then
                FacturVenta.idCliente = Me.DataGridView1.SelectedCells(0).Value
                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If
        End If
    End Sub

    Private Sub btnNuevo_Click(sender As System.Object, e As System.EventArgs) Handles btnNuevo.Click
        Try

            Cursor = Cursors.WaitCursor

            Dim idCliente As Integer = 0

            Using listClient As New frmAdd_Personas(stateOperation.Insert, 0)
                With listClient
                    .ShowDialog()
                    If .DialogResult = System.Windows.Forms.DialogResult.OK Then
                        idCliente = ClsClientes.isClinteBypersonAdmin(.idPersona)
                    End If
                End With
            End Using

            If (idCliente > 0) Then
                CargaListClientes("Where c.idCliente =" & idCliente.ToString())
            End If

        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub btnclose_Click(sender As System.Object, e As System.EventArgs)
        Me.Close()
    End Sub
    Private Sub btnEditarCliente_Click(sender As Object, e As EventArgs) Handles btnEditarCliente.Click
        Try
            Cursor = Cursors.WaitCursor
            If Me.idPersona > 0 Then
                Using EddPerson As New frmAdd_Personas(stateOperation.Update, Me.idPersona)
                    With EddPerson
                        .ShowDialog()
                        If .DialogResult = System.Windows.Forms.DialogResult.OK Then
                            txtCliente_Select.Text = String.Format("{0} {1}", .ApellidosText.Text, .NombreText.Text)
                            btnBuscar.PerformClick()
                        End If
                    End With
                End Using
            End If

        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnEliminarCliente_Click(sender As Object, e As EventArgs) Handles btnEliminarCliente.Click
        Try
            If Me.idPersona > 0 Then
                sql = DataGridView1.SelectedRows(0).Cells(2).Value
                If MsgBox("Esta sueguro de eliminar al cliente " + sql, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Responda.") = MsgBoxResult.Yes Then
                    If Eliminar_clinete(Me.idPersona) Then
                        Me.NotifyIcon1.BalloonTipText = "Cliente eliminado"
                        Me.NotifyIcon1.ShowBalloonTip(2000)
                        DataGridView1.Rows.Remove(DataGridView1.SelectedRows(0))
                    End If
                End If
            End If

        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub
    Private Function Eliminar_clinete(idPersona As Integer) As Boolean
        sql = "Delete Clientes from Clientes where idPersona=" & idPersona & ""


        Try
            Using cnn = New SqlConnection(DomainSQLite.Setting.Configuration.ConectionString)
                cnn.Open()

                Using cmd As New SqlCommand(sql)
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = cnn
                    If cmd.ExecuteNonQuery() Then
                        Return True
                    Else
                        Return False
                    End If
                End Using

            End Using



        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Erro en el Eliminar_clinete")
            Return True
        Finally

        End Try

    End Function

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If Me.DataGridView1.SelectedRows.Count = 1 Then
            If e.KeyCode = Keys.Enter Then
                sql = DataGridView1.SelectedCells(2).Value
                OK_Button.PerformClick()
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        If txtFlag = "Lote" And FacturVenta.idFormPago > 0 Then
            Factura_AlCliente()
        ElseIf txtFlag = "Ventas" Then
            Factura_AlCliente()
        ElseIf txtFlag = "Lote" Then
            MsgBox("Determine la forma de pago", MsgBoxStyle.Information, "Aviso")
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If sender.Columns(e.ColumnIndex).name = "btnCredito" And e.RowIndex >= 0 Then
            e.Value = "Otorgar Crédito"
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If sender.Columns(e.ColumnIndex).name = "btnCredito" And e.RowIndex >= 0 And txtFlag = "Creditos" Then
            If MsgBox("Está seguro de habilitar para que el cliente pueda comprar a crédito", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then

                DataGridView1.Enabled = False
                FlowMenu.Enabled = False

                sql = (DataGridView1.SelectedCells.Item(0).Value)
                sql = DataGridView1.SelectedCells.Item(8).Value

            End If
        End If
    End Sub
    Private Sub DataGridView1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.RowEnter
        If txtFlag = "Creditos" Then

        ElseIf txtFlag = "Lote" Then
            lblformaPago.Visible = False
            FacturVenta.idFormPago = 0
        End If
    End Sub

    Private Sub btnCredit_Click(sender As Object, e As EventArgs) Handles btnCredit.Click
        Try
            If DataGridView1.SelectedRows.Count = 1 Then
                Me.idCliente = DataGridView1.SelectedRows(0).Cells(DataGridView1.Columns("idCliente").Index).Value
                If Not (stateClient = stateClient.Admin) Then
                    MsgBox("No tiene permiso para esta opción.", MsgBoxStyle.Exclamation, "Aviso")
                    Return
                End If
                Using frmdata As New frmImputData
                    With frmdata
                        .txtNumber.Value = DataGridView1.SelectedCells.Item(9).Value
                        .Text = "Determine el monto máximo de crédito."
                        .ShowDialog()
                        If .DialogResult = DialogResult.OK Then
                            If Otorga_Credito(Me.idCliente, .txtNumber.Value) Then
                                btnBuscar.PerformClick()
                            End If
                        End If
                    End With
                End Using
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Function Otorga_Credito(idCliente As Integer, monto As Double) As Boolean
        Try
            sql = "Update [dbo].[Clientes] set monto_Max =@monto_Max, credito =@credito "
            sql = sql & "Where idCliente = @idCliente "
            Using cnn As New SqlConnection(SimpleDataApp.Utility.GetConnectionString())
                cnn.Open()
                Using cmd As New SqlCommand(sql, cnn)
                    cmd.Parameters.Add("@credito", SqlDbType.Bit)
                    cmd.Parameters.Add("@monto_Max", SqlDbType.Decimal, 18, 2)
                    cmd.Parameters.Add("@idCliente", SqlDbType.Int)

                    If monto = 0 Then
                        cmd.Parameters("@credito").Value = 0
                    Else
                        cmd.Parameters("@credito").Value = 1
                    End If
                    cmd.Parameters("@monto_Max").Value = monto
                    cmd.Parameters("@idCliente").Value = idCliente
                    If cmd.ExecuteNonQuery() = 1 Then
                        Return True
                    Else
                        Return False
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function

    Private Sub Cancel_Button_Click(sender As Object, e As EventArgs) Handles Cancel_Button.Click
        Me.Close()
    End Sub

    Private Sub btnCobro_Click(sender As Object, e As EventArgs) Handles btnCobro.Click
        Try
            Cursor = Cursors.WaitCursor

            If Me.idPersona > 0 Then
                Using fornew As New frmCobro
                    With fornew
                        .Text = "ESTADO DE CUENTA DEL: " & DataGridView1.SelectedCells.Item(2).Value
                        .idCliente = DataGridView1.SelectedCells.Item(0).Value
                        .ShowDialog()
                    End With
                End Using
            End If

        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            idPersona = 0
            idCliente = 0
            If DataGridView1.SelectedRows.Count = 1 Then
                Me.VisibleCabecera(True)
                idPersona = DataGridView1.SelectedRows(0).Cells(DataGridView1.Columns("idPersona").Index).Value
                idCliente = DataGridView1.SelectedRows(0).Cells(DataGridView1.Columns("idCliente").Index).Value
            Else
                Me.VisibleCabecera(False)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub btnDetail_Click(sender As Object, e As EventArgs) Handles btnDetail.Click
        Try
            Cursor = Cursors.WaitCursor
            If Me.idPersona > 0 Then
                Using newClient As New frmAdd_Personas(stateOperation.View, Me.idPersona)
                    With newClient
                        .ShowDialog()
                        If .DialogResult = DialogResult.OK Then

                        End If
                    End With
                End Using
            End If

        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub
End Class