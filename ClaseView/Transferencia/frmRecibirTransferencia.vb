Imports CADsisVenta.Helpers
Imports Newtonsoft.Json.Linq
Imports CADsisVenta.Helpers.FInicio

Public Class frmRecibirTransferencia
    Inherits Form

    Private _transferenciasJson As JArray
    Private _transferenciaSeleccionada As JObject

    Private lblBodega As Label
    Private lblEstado As Label
    Private ListBoxTransf As ListBox
    Private DgvDetalle As DataGridView
    Private txtNovedad As TextBox
    Private btnActualizar As Button
    Private btnAceptar As Button
    Private btnCancelar As Button

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmRecibirTransferencia_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblBodega.Text = "Terminal: " & Dominio._HotName & " — Bodega: " & TerminalActivo.idBodega
        CargarTransferenciasPendientes()
    End Sub

    Private Sub InitializeComponent()
        Text = "RECEPCIÓN DE TRANSFERENCIAS"
        StartPosition = FormStartPosition.CenterParent
        Size = New Drawing.Size(900, 620)

        lblBodega = New Label With {.AutoSize = True, .Location = New Drawing.Point(20, 15)}
        lblEstado = New Label With {.AutoSize = True, .Location = New Drawing.Point(20, 40)}
        btnActualizar = New Button With {.Text = "↻ Actualizar", .Location = New Drawing.Point(760, 12), .Width = 110}

        ListBoxTransf = New ListBox With {.Location = New Drawing.Point(20, 70), .Size = New Drawing.Size(850, 120)}

        DgvDetalle = New DataGridView With {
            .Location = New Drawing.Point(20, 210),
            .Size = New Drawing.Size(850, 260),
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .AutoGenerateColumns = False
        }

        Dim colCheck As New DataGridViewCheckBoxColumn With {.Name = "ColCheck", .HeaderText = "✔", .Width = 40}
        Dim colProducto As New DataGridViewTextBoxColumn With {.Name = "ColProducto", .HeaderText = "Producto", .Width = 320}
        Dim colEnviado As New DataGridViewTextBoxColumn With {.Name = "ColEnviado", .HeaderText = "Enviado", .Width = 90}
        Dim colRecibido As New DataGridViewTextBoxColumn With {.Name = "ColRecibido", .HeaderText = "Recibido", .Width = 90}
        Dim colUnidad As New DataGridViewTextBoxColumn With {.Name = "ColUnidad", .HeaderText = "Unidad", .Width = 90}
        DgvDetalle.Columns.AddRange(New DataGridViewColumn() {colCheck, colProducto, colEnviado, colRecibido, colUnidad})

        Dim lblNovedad As New Label With {.Text = "Novedad general:", .AutoSize = True, .Location = New Drawing.Point(20, 480)}
        txtNovedad = New TextBox With {.Location = New Drawing.Point(20, 505), .Size = New Drawing.Size(850, 40), .Multiline = True}

        btnCancelar = New Button With {.Text = "Cancelar", .Location = New Drawing.Point(20, 555), .Width = 120}
        btnAceptar = New Button With {.Text = "✔ Aceptar productos llegados", .Location = New Drawing.Point(600, 555), .Width = 270}

        Controls.Add(lblBodega)
        Controls.Add(lblEstado)
        Controls.Add(btnActualizar)
        Controls.Add(ListBoxTransf)
        Controls.Add(DgvDetalle)
        Controls.Add(lblNovedad)
        Controls.Add(txtNovedad)
        Controls.Add(btnCancelar)
        Controls.Add(btnAceptar)

        AddHandler btnActualizar.Click, AddressOf btnActualizar_Click
        AddHandler btnAceptar.Click, AddressOf btnAceptar_Click
        AddHandler btnCancelar.Click, Sub(sender, e) Close()
        AddHandler ListBoxTransf.SelectedIndexChanged, AddressOf ListBoxTransf_SelectedIndexChanged
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs)
        CargarTransferenciasPendientes()
    End Sub

    Private Sub CargarTransferenciasPendientes()
        Try
            Cursor = Cursors.WaitCursor
            lblEstado.Text = "Consultando Supabase..."
            Dim json As String = SupabaseHelper.ObtenerTransferenciasPendientesAsync(TerminalActivo.idBodega).Result
            If String.IsNullOrEmpty(json) Then
                lblEstado.Text = "Sin transferencias pendientes."
                ListBoxTransf.DataSource = Nothing
                Return
            End If

            _transferenciasJson = JArray.Parse(json)
            Dim lista As New List(Of String)
            For Each t As JObject In _transferenciasJson
                lista.Add(t("num_transferencia").ToString() & " | " &
                          "Desde: " & t("bodega_origen_nom").ToString() & " | " &
                          CDate(t("fecha_emision").ToString()).ToString("dd/MM/yyyy HH:mm"))
            Next
            ListBoxTransf.DataSource = lista
            lblEstado.Text = lista.Count & " transferencia(s) pendiente(s)."

        Catch ex As Exception
            lblEstado.Text = "Error al conectar con Supabase."
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error Supabase")
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ListBoxTransf_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ListBoxTransf.SelectedIndex < 0 Then Exit Sub
        _transferenciaSeleccionada = _transferenciasJson(ListBoxTransf.SelectedIndex)
        CargarDetalleEnGrid()
    End Sub

    Private Sub CargarDetalleEnGrid()
        DgvDetalle.Rows.Clear()
        Dim detalle As JArray = _transferenciaSeleccionada("detalle")
        For Each item As JObject In detalle
            Dim rowIdx As Integer = DgvDetalle.Rows.Add()
            With DgvDetalle.Rows(rowIdx)
                .Cells("ColCheck").Value = True
                .Cells("ColProducto").Value = item("nombre").ToString()
                .Cells("ColEnviado").Value = item("cantidadEnviada").ToString()
                .Cells("ColRecibido").Value = item("cantidadEnviada").ToString()
                .Cells("ColUnidad").Value = If(item("unidad")?.ToString(), String.Empty)
                .Tag = item("idProducto").ToString()
            End With
        Next
    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As EventArgs)
        If _transferenciaSeleccionada Is Nothing Then
            MsgBox("Seleccione una transferencia.", MsgBoxStyle.Exclamation, "Requerido")
            Exit Sub
        End If

        Dim novedad As String = txtNovedad.Text.Trim()
        Dim confirm As MsgBoxResult = MsgBox(
            "¿Confirma la recepción de los productos marcados?" & vbNewLine &
            "Los productos NO marcados NO se acreditarán al inventario.",
            MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Confirmar recepción")

        If confirm <> MsgBoxResult.Yes Then Exit Sub

        Try
            Dim supabaseId As String = _transferenciaSeleccionada("id").ToString()
            Dim hayNovedad As Boolean = False

            Dim detalleActualizado As New List(Of Object)
            For Each row As DataGridViewRow In DgvDetalle.Rows
                If row.IsNewRow Then Continue For
                Dim cheked As Boolean = CBool(row.Cells("ColCheck").Value)
                Dim idProd As Integer = CInt(row.Tag)
                Dim cantEnviada As Decimal = CDec(row.Cells("ColEnviado").Value)
                Dim cantRecibida As Decimal = If(cheked,
                    CDec(row.Cells("ColRecibido").Value), 0D)

                If cantRecibida <> cantEnviada Then hayNovedad = True

                If cheked AndAlso cantRecibida > 0 Then
                    AsegurarProductoExiste(idProd, row.Cells("ColProducto").Value?.ToString())
                    AcreditarStockLocal(idProd, cantRecibida)
                End If

                detalleActualizado.Add(New With {
                    .idProducto = idProd,
                    .nombre = row.Cells("ColProducto").Value?.ToString(),
                    .cantidadEnviada = cantEnviada,
                    .cantidadRecibida = cantRecibida,
                    .unidad = row.Cells("ColUnidad").Value?.ToString()
                })
            Next

            Dim estado As String = If(hayNovedad OrElse Not String.IsNullOrEmpty(novedad),
                                      "CON_NOVEDAD", "RECIBIDO")
            SupabaseHelper.ActualizarEstadoAsync(supabaseId, estado, novedad).Wait()

            RegistrarRecepcionLocal(supabaseId, estado, novedad, detalleActualizado)

            MsgBox("Recepción confirmada. Estado: " & estado, MsgBoxStyle.Information, "Listo")
            CargarTransferenciasPendientes()

        Catch ex As Exception
            MsgBox("Error al procesar recepción: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub AcreditarStockLocal(idProducto As Integer, cantidad As Decimal)
        Dim sql As String =
            "IF EXISTS (SELECT 1 FROM Stock WHERE idProducto=@p AND idBodega=@b) " &
            "    UPDATE Stock SET stock = stock + @c WHERE idProducto=@p AND idBodega=@b " &
            "ELSE " &
            "    INSERT INTO Stock (idProducto, idBodega, stock) VALUES (@p, @b, @c)"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(sql,
                {"@p", "@b", "@c"},
                {idProducto, TerminalActivo.idBodega, cantidad})
        End Using
    End Sub

    Private Function AsegurarProductoExiste(idProducto As Integer, nombreProducto As String) As Boolean
        Dim sqlCheck As String = "SELECT COUNT(1) FROM Productos WHERE idProducto = @id"
        Dim existe As Integer
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            existe = CInt(cmd.RetornaEscalarConParams(sqlCheck, {"@id"}, {idProducto}))
        End Using
        If existe > 0 Then Return True

        Dim nom_comercial As String = nombreProducto
        Dim nom_comun As String = nombreProducto
        Dim cant_minima As Decimal = 1
        Dim id_unidad As Integer = 1
        Dim id_subcateg As Integer = 1
        Dim iva_porc As Decimal = 0
        Dim facturable As Integer = 1
        Dim cod_producto As String = "SN" & idProducto.ToString("D5")
        Dim precio_compra As Decimal = 0
        Dim precio_venta As Decimal = 0
        Dim unidad_pres As String = "UN"

        Dim sqlIns As String =
            "SET IDENTITY_INSERT Productos ON; " &
            "INSERT INTO Productos (idProducto, Nom_Comercial, Nom_Comun, Cant_minima, " &
            "  idUnidad, IdSubCategoria, ivaPorcentaje, Facturable, Activo) " &
            "VALUES (@id, @nom_c, @nom_u, @cant, @unidad, @subcat, @iva, @fact, 1); " &
            "SET IDENTITY_INSERT Productos OFF;"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(sqlIns,
                {"@id", "@nom_c", "@nom_u", "@cant", "@unidad", "@subcat", "@iva", "@fact"},
                {idProducto, nom_comercial, nom_comun, cant_minima, id_unidad, id_subcateg, iva_porc, facturable})
        End Using

        Dim sqlPres As String =
            "INSERT INTO ProductoPresentacion " &
            "  (codProducto, idProducto, idProUndMed, idProUndReferen, " &
            "   Cant_Present, precioCompra, precioVenta, Empaquetado, " &
            "   Presentacion, PresentacionPrint, isPresentFactory) " &
            "VALUES (@cod, @idP, @und, @und, 1, @pc, @pv, 1, @pres, @presp, 1)"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(sqlPres,
                {"@cod", "@idP", "@und", "@pc", "@pv", "@pres", "@presp"},
                {cod_producto, idProducto, id_unidad, precio_compra, precio_venta, unidad_pres, "[" & unidad_pres & "]"})
        End Using

        Dim sqlStock As String =
            "INSERT INTO ProdcutStock (idProducto, idBodega, stock, pvpUND, Und, idProUndMed) " &
            "VALUES (@idP, @idB, 0, @pvp, @und, @undM)"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(sqlStock,
                {"@idP", "@idB", "@pvp", "@und", "@undM"},
                {idProducto, TerminalActivo.idBodega, precio_venta, unidad_pres, id_unidad})
        End Using

        Return True
    End Function

    Private Sub RegistrarRecepcionLocal(supabaseId As String, estado As String, novedad As String, detalle As List(Of Object))
        Dim sql As String =
            "UPDATE TransferenciaEncabezado SET EstadoEnvio=@est, Novedad=@nov, " &
            "FechaRecepcion=GETDATE() WHERE SupabaseId=@sid"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(sql,
                {"@est", "@nov", "@sid"},
                {estado, novedad, supabaseId})
        End Using
    End Sub
End Class
