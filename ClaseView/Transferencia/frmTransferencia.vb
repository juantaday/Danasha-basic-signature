Imports CADsisVenta.DataSetComprasTableAdapters
Imports CADsisVenta.Helpers
Imports CADsisVenta.Helpers.FInicio

Public Class frmTransferencia
    Inherits Form

    Private ReadOnly _detalle As List(Of DetalleTransferenciaItem)
    Private cboOrigen As ComboBox
    Private cboDestino As ComboBox
    Private DgvDetalle As DataGridView
    Private btnConfirmar As Button
    Private btnCancelar As Button
    Private btnGuias As Button

    Public Sub New(detalle As List(Of DetalleTransferenciaItem))
        InitializeComponent()
        _detalle = detalle
    End Sub

    Private Sub frmTransferencia_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarOrigen()
        CargarDestinos()
        MostrarDetalle()
    End Sub

    Private Sub InitializeComponent()
        Text = "TRANSFERENCIA DE PRODUCTOS"
        StartPosition = FormStartPosition.CenterParent
        Size = New Drawing.Size(760, 520)

        Dim lblOrigen As New Label With {.Text = "DESDE:", .AutoSize = True, .Location = New Drawing.Point(20, 20)}
        cboOrigen = New ComboBox With {.DropDownStyle = ComboBoxStyle.DropDownList, .Location = New Drawing.Point(90, 16), .Width = 280}

        Dim lblDestino As New Label With {.Text = "HACIA:", .AutoSize = True, .Location = New Drawing.Point(400, 20)}
        cboDestino = New ComboBox With {.DropDownStyle = ComboBoxStyle.DropDownList, .Location = New Drawing.Point(460, 16), .Width = 260}

        DgvDetalle = New DataGridView With {
            .Location = New Drawing.Point(20, 60),
            .Size = New Drawing.Size(700, 340),
            .ReadOnly = True,
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .AutoGenerateColumns = False
        }

        Dim colProducto As New DataGridViewTextBoxColumn With {.Name = "ColProducto", .HeaderText = "Producto", .DataPropertyName = "NombreProducto", .Width = 380}
        Dim colCantidad As New DataGridViewTextBoxColumn With {.Name = "ColCantidad", .HeaderText = "Cantidad", .DataPropertyName = "Cantidad", .Width = 120}
        Dim colUnidad As New DataGridViewTextBoxColumn With {.Name = "ColUnidad", .HeaderText = "Unidad", .DataPropertyName = "Unidad", .Width = 120}
        DgvDetalle.Columns.AddRange(New DataGridViewColumn() {colProducto, colCantidad, colUnidad})

        btnCancelar = New Button With {.Text = "Cancelar", .Location = New Drawing.Point(20, 420), .Width = 120}
        btnGuias = New Button With {.Text = "Guías", .Location = New Drawing.Point(150, 420), .Width = 120}
        btnConfirmar = New Button With {.Text = "✔ Confirmar Envío", .Location = New Drawing.Point(560, 420), .Width = 160}

        Controls.Add(lblOrigen)
        Controls.Add(cboOrigen)
        Controls.Add(lblDestino)
        Controls.Add(cboDestino)
        Controls.Add(DgvDetalle)
        Controls.Add(btnCancelar)
        Controls.Add(btnGuias)
        Controls.Add(btnConfirmar)

        AddHandler btnConfirmar.Click, AddressOf btnConfirmar_Click
        AddHandler btnGuias.Click, AddressOf btnGuias_Click
        AddHandler btnCancelar.Click, AddressOf btnCancelar_Click
    End Sub

    Private Sub btnGuias_Click(sender As Object, e As EventArgs)
        Using form As New frmListadoGuiasRemision
            form.ShowDialog()
        End Using
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs)
        Close()
    End Sub

    Private Sub CargarOrigen()
        If FInicio.TerminalActivo.idTerminal = 0 Then
            MessageBox.Show("No hay terminal activa.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim tap As New BodegasTableAdapter
        Dim dt As DataTable = tap.GetBodegasByHostName(TerminalActivo.idTerminal)
        cboOrigen.DataSource = dt
        cboOrigen.DisplayMember = "nom_bodega"
        cboOrigen.ValueMember = "idBodega"
        cboOrigen.SelectedValue = TerminalActivo.idBodega
    End Sub

    Private Sub CargarDestinos()
        Dim tap As New BodegasTableAdapter
        Dim dt As DataTable = tap.GetAllBodegas()
        Dim dv As New DataView(dt)
        dv.RowFilter = "idBodega <> " & TerminalActivo.idBodega.ToString()
        cboDestino.DataSource = dv.ToTable()
        cboDestino.DisplayMember = "nom_bodega"
        cboDestino.ValueMember = "idBodega"
    End Sub

    Private Sub MostrarDetalle()
        DgvDetalle.DataSource = _detalle
    End Sub

    Private Sub btnConfirmar_Click(sender As Object, e As EventArgs)
        If cboOrigen.SelectedIndex < 0 OrElse cboDestino.SelectedIndex < 0 Then
            MsgBox("Seleccione origen y destino.", MsgBoxStyle.Exclamation, "Requerido")
            Exit Sub
        End If

        Dim idOrigen As Integer = CInt(cboOrigen.SelectedValue)
        Dim idDestino As Integer = CInt(cboDestino.SelectedValue)
        Dim nomOrigen As String = cboOrigen.Text
        Dim nomDestino As String = cboDestino.Text

        Dim confirm As MsgBoxResult = MsgBox(
            "¿Confirma la transferencia de " & _detalle.Count & " producto(s)" & vbNewLine &
            "DESDE: " & nomOrigen & vbNewLine &
            "HACIA: " & nomDestino & "?",
            MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Confirmar transferencia")

        If confirm <> MsgBoxResult.Yes Then Exit Sub

        Try
            Dim tap As New BodegasTableAdapter
            Dim dtDest As DataTable = tap.GetDataByIdBodega(idDestino)
            Dim esRemota As Boolean = CBool(dtDest.Rows(0)("EsSucursalRemota"))

            Dim numTransf As String = GenerarNumTransferencia()
            Dim idTransf As Integer = InsertarEncabezado(numTransf, idOrigen, idDestino)
            InsertarDetalle(idTransf)

            EjecutarSP("sp_TransferenciaDescontarStock", idTransf)

            If esRemota Then
                Dim payload = New With {
                    .num_transferencia = numTransf,
                    .bodega_origen_id = idOrigen,
                    .bodega_origen_nom = nomOrigen,
                    .bodega_destino_id = idDestino,
                    .bodega_destino_nom = nomDestino,
                    .detalle = _detalle.Select(Function(d) New With {
                        .idProducto = d.idProducto,
                        .nombre = d.NombreProducto,
                        .cantidadEnviada = d.Cantidad,
                        .cantidadRecibida = CType(Nothing, Object),
                        .unidad = d.Unidad
                    }).ToList()
                }

                Dim supabaseId As String = SupabaseHelper.SubirTransferenciaAsync(payload).Result
                ActualizarSupabaseId(idTransf, supabaseId)
            Else
                EjecutarSP("sp_TransferenciaAcreditarStockLocal", idTransf)
                ActualizarEstado(idTransf, "RECIBIDO")
            End If

            ImprimirGuiaRemision(idTransf, numTransf, nomOrigen, nomDestino)

            MsgBox("Transferencia registrada correctamente." & vbNewLine &
                   "Número: " & numTransf, MsgBoxStyle.Information, "Éxito")


            DialogResult = DialogResult.OK
            Close()

        Catch ex As Exception
            MsgBox("Error al procesar transferencia: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Function GenerarNumTransferencia() As String
        Dim fecha As String = DateTime.Now.ToString("yyyyMMdd")
        Dim sql As String = "SELECT ISNULL(MAX(CAST(RIGHT(NumTransferencia,4) AS INT)),0)+1 " &
                            "FROM TransferenciaEncabezado " &
                            "WHERE NumTransferencia LIKE 'TRF-" & fecha & "-%'"
        Dim n As Integer
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            n = CInt(cmd.RetornaEscalar(sql))
        End Using
        Return "TRF-" & fecha & "-" & n.ToString("D4")
    End Function

    Private Function InsertarEncabezado(num As String, origen As Integer, destino As Integer) As Integer
        Dim sql As String =
            "INSERT INTO TransferenciaEncabezado " &
            "(NumTransferencia, idBodegaOrigen, idBodegaDestino, idUsuario, EstadoEnvio) " &
            "VALUES (@num, @origen, @destino, @usr, 'PENDIENTE'); SELECT SCOPE_IDENTITY();"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            Return CInt(cmd.RetornaEscalarConParams(sql,
                {"@num", "@origen", "@destino", "@usr"},
                {num, origen, destino, UsuarioActivo.IdUsuario}))
        End Using
    End Function

    Private Sub InsertarDetalle(idTransf As Integer)
        For Each item In _detalle
            Dim sql As String =
                "INSERT INTO TransferenciaDetalle " &
                "(idTransferencia, idProducto, CantidadEnviada, Unidad) " &
                "VALUES (@idT, @idP, @cant, @unidad)"
            Using cmd As New CADsisVenta.Funtions.SqlComandExec
                cmd.EjecutarConParams(sql,
                    {"@idT", "@idP", "@cant", "@unidad"},
                    {idTransf, item.idProducto, item.Cantidad, item.Unidad})
            End Using
        Next
    End Sub

    Private Sub EjecutarSP(spName As String, idTransf As Integer)
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarSP(spName, {"@idTransferencia"}, {idTransf})
        End Using
    End Sub

    Private Sub ActualizarSupabaseId(idTransf As Integer, supabaseId As String)
        Dim sql As String = "UPDATE TransferenciaEncabezado SET SupabaseId=@sid, " &
                            "EstadoEnvio='ENVIADO' WHERE idTransferencia=@id"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(sql, {"@sid", "@id"}, {supabaseId, idTransf})
        End Using
    End Sub

    Private Sub ActualizarEstado(idTransf As Integer, estado As String)
        Dim sql As String = "UPDATE TransferenciaEncabezado SET EstadoEnvio=@est WHERE idTransferencia=@id"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(sql, {"@est", "@id"}, {estado, idTransf})
        End Using
    End Sub
End Class
