Imports BrightIdeasSoftware
Imports CADsisVenta.Helpers.FInicio
Imports SupabaseDataAccess.Models
Imports SupabaseDataAccess.Repositories


Public Class frmRecibirTransferencia

    Private _actualizandoHeader As Boolean = False
    Private _transferencias As List(Of Transferencia)
    Private _transferenciaSeleccionada As Transferencia
    Private _productosNuevosCount As Integer = 0
    Private _items As List(Of DetalleTransfItem)
    Private _colorEstato As Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(115, Byte), Integer))

    ' Transferencias que quedaron en estado GENERADO (Supabase caído)
    Private _pendientesEnvio As New List(Of PendienteEnvio)

    Public Sub New()
        InitializeComponent()
        ConfigurarOLV()
    End Sub

    ' ── Clase para transferencias pendientes de envío ────────────────────────────
    Private Class PendienteEnvio
        Public Property SupabaseId As String
        Public Property Estado As String
        Public Property Novedad As String
        Public Property FechaIntento As DateTime = DateTime.Now
    End Class

    Private Class TransferenciaItem
        Public Property Accion As String
        Public Property Numero As String
        Public Property Origen As String
        Public Property Fecha As String
        Public Property Json As Transferencia
    End Class

    ' ── Configurar ObjectListView ────────────────────────────────────────────────
    Private Sub ConfigurarOLV()

        Dim renderButton As ColumnButtonRenderer = New ColumnButtonRenderer()
        renderButton.SizingMode = OLVColumn.ButtonSizingMode.CellBounds
        renderButton.ButtonImage = Global.DanashaBasicSignature.My.Resources.Option_20
        renderButton.CellPadding = New Rectangle(1, 1, 1, 1)
        Me.colAccion.Renderer = renderButton
        ' Checkbox vinculado a la propiedad Seleccionado
        OlvDetalle.CheckedAspectName = "Seleccionado"
        OlvDetalle.CheckBoxes = True

        ' Permitir edición inline en ColRecibido con Enter/doble clic
        OlvDetalle.CellEditActivation = ObjectListView.CellEditActivateMode.DoubleClick

        AddHandler Me.OlvTransferencias.ButtonClick,
         Sub(sender As Object, e As CellClickEventArgs)

             _transferenciaSeleccionada = Nothing

             Dim model = TryCast(e.HitTest.RowObject, TransferenciaItem)

             If model Is Nothing Then
                 Return
             End If

             _transferenciaSeleccionada = model.Json

             Me.mnuTransferencias.Show(Cursor.Position)

         End Sub


        AddHandler Me.OlvDetalle.HeaderCheckBoxChanging,
            Sub(sender As Object, e As HeaderCheckBoxChangingEventArgs)
                If _items Is Nothing OrElse _actualizandoHeader OrElse (e.NewCheckState = CheckState.Unchecked) Then Return

                _actualizandoHeader = True
                Try
                    _items.ForEach(Sub(i) i.Seleccionado = (e.NewCheckState = CheckState.Checked))
                    OlvDetalle.SetObjects(_items)
                Finally
                    _actualizandoHeader = False
                End Try
            End Sub

        colRecibido.AspectPutter =
                Sub(row As Object, newValue As Object)

                    Dim item = TryCast(row, DetalleTransfItem)

                    If item Is Nothing Then
                        Return
                    End If

                    Dim strVal As String = newValue.ToString().Trim()

                    Dim decVal As Decimal

                    If Not Decimal.TryParse(strVal, decVal) OrElse decVal < 0 Then

                        ' Valor no válido: revertir al valor anterior
                        OlvDetalle.RefreshObject(item)

                    Else

                        item.CantRecibida = decVal

                    End If

                End Sub


    End Sub

    ' ── Load ────────────────────────────────────────────────────────────────────
    Private Sub frmRecibirTransferencia_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblBodega.Text = "Terminal: " & Dominio._HotName &
                         "   |   Bodega ID: " & TerminalActivo.idBodega &
                         "   |   Nombre: " & TerminalActivo.nombreBodega
        CargarTransferenciasPendientes()
        ActualizarBotonEnviar()
    End Sub

    ' ── Botones básicos ──────────────────────────────────────────────────────────
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Close()
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        CargarTransferenciasPendientes()
    End Sub

    ' ── Cargar lista de pendientes ───────────────────────────────────────────────
    Private Async Sub CargarTransferenciasPendientes()
        MostrarLoading("Consultando transferencias pendientes...")
        Try

            ' Ejecutar la lógica pesada en segundo plano
            _transferencias = Await Task.Run(Function() TransferenciaRepository.ObtenerPendientesComoObjetos(TerminalActivo.idBodega))

            Dim lista As New List(Of TransferenciaItem)
            For Each t As Transferencia In _transferencias
                Dim fecha As String = CDate(t.FechaEmision.ToString()).ToString("dd/MM/yyyy HH:mm")
                lista.Add(New TransferenciaItem With {
                    .Accion = "⋯",
                    .Numero = t.NumTransferencia,
                    .Origen = t.BodegaOrigenNom,
                    .Fecha = fecha,
                    .Json = t
                })
            Next

            OlvTransferencias.SetObjects(lista)
            lblEstado.Text = _transferencias.Count & " transferencia(s) pendiente(s)"

        Catch ex As Exception
            lblEstado.Text = "⚠  Sin conexión a Supabase"
            lblEstado.ForeColor = Color.Red
        Finally
            OcultarLoading()
        End Try
    End Sub

    ' ── Selección en lista ───────────────────────────────────────────────────────
    Private Sub OlvTransferencias_SelectedIndexChanged(sender As Object, e As EventArgs) _
            Handles OlvTransferencias.SelectedIndexChanged
        Dim sel = TryCast(OlvTransferencias.SelectedObject, TransferenciaItem)
        If sel Is Nothing Then
            _items.Clear()
            OlvDetalle.SetObjects(_items)
            Exit Sub
        End If
        _transferenciaSeleccionada = sel.Json
        CargarDetalleEnOLV()

    End Sub

    Private Sub OlvTransferencias_CellClick(sender As Object, e As CellClickEventArgs) _
            Handles OlvTransferencias.CellClick
        If e.Column IsNot colAccion OrElse e.Model Is Nothing Then Return

        Dim sel = TryCast(e.Model, TransferenciaItem)
        If sel Is Nothing Then Return

        _transferenciaSeleccionada = sel.Json
        OlvTransferencias.SelectedObject = sel
        CargarDetalleEnOLV()
        mnuTransferencias.Show(Cursor.Position)
    End Sub

    Private Sub CargarDetalleEnOLV()
        _productosNuevosCount = 0

        If (_items Is Nothing) Then
            _items = New List(Of DetalleTransfItem)
        Else
            _items.Clear()
        End If

        If _transferenciaSeleccionada Is Nothing Then Exit Sub

        For Each item As DetalleTransferencia In _transferenciaSeleccionada.Detalle
            Dim idProd As Integer = CInt(item.IdProducto)
            Dim esNuevo As Boolean = Not ProductoExisteLocal(idProd)
            If esNuevo Then _productosNuevosCount += 1

            _items.Add(New DetalleTransfItem With {
                .Seleccionado = False,
                .IdProducto = idProd,
                .Producto = item.Nombre,
                .CantEnviada = item.CantidadEnviada,
                .CantRecibida = item.CantidadEnviada,
                .Unidad = If(item.Unidad, ""),
                .EsNuevo = esNuevo,
                .EstadoItem = If(esNuevo, "✦ NUEVO", "En stock")
            })
        Next

        OlvDetalle.SetObjects(_items)

        lblNuevosAviso.Text = If(_productosNuevosCount > 0,
            "✦ " & _productosNuevosCount & " producto(s) nuevo(s) se registrarán automáticamente",
            "")
    End Sub

    Private Sub mnuImprimirDetalle_Click(sender As Object, e As EventArgs) _
            Handles mnuImprimirDetalle.Click

        If _transferenciaSeleccionada Is Nothing OrElse
           _items Is Nothing OrElse _items.Count = 0 Then
            MsgBox("Seleccione una transferencia para imprimir.",
                   MsgBoxStyle.Exclamation, "Sin datos")
            Exit Sub
        End If


    End Sub

    Private Async Sub mnuRechazar_Click(sender As Object, e As EventArgs) _
            Handles mnuRechazar.Click

        If _transferenciaSeleccionada Is Nothing Then
            MsgBox("Seleccione una transferencia.", MsgBoxStyle.Exclamation, "Requerido")
            Exit Sub
        End If

        Dim motivo As String = Interaction.InputBox(
            "Ingrese el motivo del rechazo:",
            "Rechazar transferencia")

        If String.IsNullOrWhiteSpace(motivo) Then Exit Sub

        Dim supabaseId As String = _transferenciaSeleccionada.Id.ToString()
        Dim estado As String = "CON_NOVEDAD"
        Dim novedad As String = "RECHAZADO: " & motivo.Trim()

        '  Actualizar en supabase 
        Dim result As Boolean = Await Task.Run(Function() TransferenciaRepository.ActualizarEstado(supabaseId, estado, novedad))
        If Not result Then
            MsgBox("No se pudo enviar el rechazo a Supabase.",
                   MsgBoxStyle.Exclamation, "Sin conexión")
            Exit Sub
        End If

        RegistrarRecepcionLocal(supabaseId, estado, novedad)
        MsgBox("Transferencia rechazada y notificada.", MsgBoxStyle.Information, "Listo")
        CargarTransferenciasPendientes()
    End Sub


    Private Sub ImprimirPagina(sender As Object,
                                e As System.Drawing.Printing.PrintPageEventArgs)
        Dim g = e.Graphics
        Dim x = 40, y = 40
        Dim anchoTotal = e.PageBounds.Width - 80
        Dim fTitulo = New System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold)
        Dim fNormal = New System.Drawing.Font("Segoe UI", 9)
        Dim fBold = New System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold)
        Dim colorTeal = System.Drawing.Color.FromArgb(0, 150, 125)
        Dim brushTeal = New System.Drawing.SolidBrush(colorTeal)
        Dim brushDark = New System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(45, 55, 70))
        Dim brushGray = New System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(100, 115, 130))

        ' ── Encabezado ──────────────────────────────────────────────────────────
        g.DrawString("GUÍA DE TRANSFERENCIA", fTitulo, brushTeal, x, y)
        y += 30
        Dim numGuia = _transferenciaSeleccionada.NumTransferencia
        Dim origen = _transferenciaSeleccionada.BodegaOrigenNom
        Dim fecha = _transferenciaSeleccionada.FechaEmision.ToString("dd/MM/yyyy HH:mm")
        g.DrawString("N°: " & numGuia, fBold, brushDark, x, y)
        g.DrawString("Origen: " & origen, fNormal, brushGray, x + 200, y)
        g.DrawString("Fecha: " & fecha, fNormal, brushGray, x + 420, y)
        y += 20
        g.DrawString("Destino: " & Dominio._HotName & "  (Bodega " & TerminalActivo.idBodega & ")",
                     fNormal, brushGray, x, y)
        y += 16
        g.DrawLine(New System.Drawing.Pen(colorTeal, 1.5!), x, y, x + anchoTotal, y)
        y += 12

        ' ── Encabezado tabla ────────────────────────────────────────────────────
        Dim colWidths = {anchoTotal - 250, 80, 80, 60, 80}  ' Producto,Env,Rec,Und,Estado
        Dim headers = {"PRODUCTO", "ENVIADO", "RECIBIDO", "UNIDAD", "ESTADO"}
        Dim cx = x
        Dim rectHdr = New System.Drawing.Rectangle(x, y, anchoTotal, 22)
        g.FillRectangle(brushTeal, rectHdr)
        Dim brushWhite = New System.Drawing.SolidBrush(System.Drawing.Color.White)
        For i = 0 To headers.Length - 1
            Dim align = If(i = 0, System.Drawing.StringAlignment.Near,
                           System.Drawing.StringAlignment.Far)
            g.DrawString(headers(i), fBold, brushWhite,
                         New System.Drawing.RectangleF(cx + 4, y + 3, colWidths(i) - 6, 18),
                         New System.Drawing.StringFormat() With {.Alignment = align})
            cx += colWidths(i)
        Next
        y += 24

        ' ── Filas ───────────────────────────────────────────────────────────────
        Dim altBrush = New System.Drawing.SolidBrush(
                            System.Drawing.Color.FromArgb(240, 247, 245))
        Dim rowNum = 0
        For Each item As DetalleTransfItem In _items
            cx = x
            If rowNum Mod 2 = 1 Then
                g.FillRectangle(altBrush,
                    New System.Drawing.Rectangle(x, y, anchoTotal, 20))
            End If
            Dim vals = {item.Producto,
                        item.CantEnviada.ToString("N2"),
                        item.CantRecibida.ToString("N2"),
                        item.Unidad,
                        item.EstadoItem}
            For i = 0 To vals.Length - 1
                Dim align = If(i = 0, System.Drawing.StringAlignment.Near,
                               System.Drawing.StringAlignment.Far)
                Dim brush = If(item.EsNuevo AndAlso i = 4,
                               New System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(0, 130, 80)),
                               brushDark)
                g.DrawString(vals(i), fNormal, brush,
                             New System.Drawing.RectangleF(cx + 4, y + 2, colWidths(i) - 6, 18),
                             New System.Drawing.StringFormat() With {.Alignment = align})
                cx += colWidths(i)
            Next
            y += 20
            rowNum += 1
            If y > e.PageBounds.Height - 80 Then
                e.HasMorePages = True
                Exit Sub
            End If
        Next

        ' ── Pie ─────────────────────────────────────────────────────────────────
        y += 16
        g.DrawLine(New System.Drawing.Pen(System.Drawing.Color.FromArgb(200, 210, 220), 1),
                   x, y, x + anchoTotal, y)
        y += 8
        Dim novedad = txtNovedad.Text.Trim()
        If Not String.IsNullOrEmpty(novedad) Then
            g.DrawString("Novedad: " & novedad, fNormal, brushGray, x, y)
            y += 18
        End If
        g.DrawString("Impreso: " & DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                     fNormal, brushGray, x, y)
    End Sub

    ' ── ACEPTAR recepción ────────────────────────────────────────────────────────
    Private Async Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If _transferenciaSeleccionada Is Nothing Then
            MsgBox("Seleccione una transferencia.", MsgBoxStyle.Exclamation, "Requerido")
            Exit Sub
        End If

        Dim avisoNuevos As String = ""
        If _productosNuevosCount > 0 Then
            avisoNuevos = vbNewLine & vbNewLine &
                          "✦ Se registrarán " & _productosNuevosCount &
                          " producto(s) nuevo(s) en la base de datos local."
        End If

        Dim confirm As MsgBoxResult = MsgBox(
            "¿Confirma la recepción de los productos marcados?" & vbNewLine &
            "Los NO marcados no se acreditarán al inventario." & avisoNuevos,
            MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Confirmar recepción")
        If confirm <> MsgBoxResult.Yes Then Exit Sub

        MostrarLoading("Procesando recepción...")

        Try
            Dim supabaseId As String = _transferenciaSeleccionada.Id.ToString()
            Dim novedad As String = txtNovedad.Text.Trim()
            Dim hayNovedad As Boolean = False
            Dim nuevosRegistrados As Integer = 0

            For Each item As DetalleTransfItem In _items
                If Not item.Seleccionado Then Continue For

                If item.CantRecibida <> item.CantEnviada Then hayNovedad = True

                MostrarLoading("Verificando producto ID " & item.IdProducto & "...")

                If item.EsNuevo Then
                    Await Task.Run(Sub() RegistrarProductoNuevo(item.IdProducto, item.Producto, item.Unidad))
                    nuevosRegistrados += 1
                End If

                If item.CantRecibida > 0 Then
                    Await Task.Run(Sub() AcreditarStockLocal(item.IdProducto, item.CantRecibida))
                End If
            Next

            Dim estado As String = If(hayNovedad OrElse Not String.IsNullOrEmpty(novedad),
                                      "CON_NOVEDAD", "RECIBIDO")

            ' ── Intentar enviar a Supabase ────────────────────────────────────
            MostrarLoading("Enviando a Supabase...")
            Dim enviado As Boolean = Await Task.Run(Function() TransferenciaRepository.ActualizarEstado(supabaseId, estado, novedad))

            If Not enviado Then
                ' Supabase caído → guardar como GENERADO y encolar para reintento
                estado = "GENERADO"
                _pendientesEnvio.Add(New PendienteEnvio With {
                    .SupabaseId = supabaseId,
                    .Estado = If(hayNovedad OrElse Not String.IsNullOrEmpty(novedad),
                                 "CON_NOVEDAD", "RECIBIDO"),
                    .Novedad = novedad
                })
                ActualizarBotonEnviar()
            End If

            ' Registro local para trazabilidad
            RegistrarRecepcionLocal(supabaseId, estado, novedad)

            OcultarLoading()

            Dim msgFinal As String = "✔  Recepción confirmada localmente." & vbNewLine &
                                     "Estado: " & estado
            If Not enviado Then
                msgFinal &= vbNewLine & vbNewLine &
                            "⚠  Supabase no disponible." & vbNewLine &
                            "La transferencia quedó como GENERADO." & vbNewLine &
                            "Use '↑ Enviar Pendientes' cuando haya conexión."
            End If
            If nuevosRegistrados > 0 Then
                msgFinal &= vbNewLine & "✦  " & nuevosRegistrados &
                            " producto(s) nuevo(s) registrados en la BD local."
            End If

            MsgBox(msgFinal, MsgBoxStyle.Information, "Recepción completada")
            txtNovedad.Clear()
            CargarTransferenciasPendientes()

        Catch ex As Exception
            OcultarLoading()
            MsgBox("Error al procesar: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub





    ''' <summary>Muestra u oculta el botón de reenvío según la cola pendiente.</summary>
    Private Sub ActualizarBotonEnviar()
        'dim hay = _pendientesenvio.count > 0
        'btnenviarpendientes.visible = hay
        'if hay then
        '    btnenviarpendientes.text = $"↑ enviar pendientes ({_pendientesenvio.count})"
        'end if
    End Sub

    ' ── Helpers BD local ─────────────────────────────────────────────────────────
    Private Function ProductoExisteLocal(idProducto As Integer) As Boolean
        Dim sql As String = "SELECT COUNT(1) FROM Productos WHERE idProducto = @id"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            Return CInt(cmd.RetornaEscalarConParams(sql, {"@id"}, {idProducto})) > 0
        End Using
    End Function

    Private Sub RegistrarProductoNuevo(idProducto As Integer,
                                       nombreProducto As String,
                                       unidad As String)

        Dim nomComercial As String = If(String.IsNullOrEmpty(nombreProducto),
                                        "Producto " & idProducto, nombreProducto)
        Dim codProducto As String = "SN" & idProducto.ToString("D5")
        Dim unidadPres As String = If(String.IsNullOrEmpty(unidad), "UN", unidad)
        Dim idUnidad As Integer = 1
        Dim idSubcat As Integer = 1
        Dim iva As Decimal = 0
        Dim precioC As Decimal = 0
        Dim precioV As Decimal = 0

        Try
            Dim ps = SupabaseDataAccess.Repositories.ProductoSyncRepository.ObtenerPorIdOrigen(idProducto)
            If ps IsNot Nothing Then
                nomComercial = If(String.IsNullOrEmpty(ps.NomComercial), nomComercial, ps.NomComercial)
                codProducto = If(String.IsNullOrEmpty(ps.CodProducto), codProducto, ps.CodProducto)
                unidadPres = If(String.IsNullOrEmpty(ps.UnidadPresent), unidadPres, ps.UnidadPresent)
                idUnidad = ps.IdUnidad
                idSubcat = If(ps.IdSubcategoria.HasValue, ps.IdSubcategoria.Value, 1)
                iva = ps.IvaPorcentaje
                precioC = ps.PrecioCompra
                precioV = ps.PrecioVenta
                SupabaseDataAccess.Repositories.ProductoSyncRepository.MarcarAplicado(idProducto)
            End If
        Catch
        End Try

        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(
                "SET IDENTITY_INSERT Productos ON; " &
                "INSERT INTO Productos (idProducto,Nom_Comercial,Nom_Comun,Cant_minima," &
                "  idUnidad,IdSubCategoria,ivaPorcentaje,Facturable,Activo) " &
                "VALUES (@id,@nom,@nom,1,@und,@sub,@iva,1,1); " &
                "SET IDENTITY_INSERT Productos OFF;",
                {"@id", "@nom", "@und", "@sub", "@iva"},
                {idProducto, nomComercial, idUnidad, idSubcat, iva})
        End Using

        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(
                "INSERT INTO ProductoPresentacion " &
                "(codProducto,idProducto,idProUndMed,idProUndReferen,Cant_Present," &
                " precioCompra,precioVenta,Empaquetado,Presentacion,PresentacionPrint,isPresentFactory) " &
                "VALUES (@cod,@idP,@und,@und,1,@pc,@pv,1,@pres,@presp,1)",
                {"@cod", "@idP", "@und", "@pc", "@pv", "@pres", "@presp"},
                {codProducto, idProducto, idUnidad, precioC, precioV,
                 unidadPres, "[" & unidadPres & "]"})
        End Using

        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(
                "INSERT INTO ProdcutStock (idProducto,idBodega,stock,pvpUND,Und,idProUndMed) " &
                "VALUES (@idP,@idB,0,@pvp,@und,@undM)",
                {"@idP", "@idB", "@pvp", "@und", "@undM"},
                {idProducto, TerminalActivo.idBodega, precioV, unidadPres, idUnidad})
        End Using
    End Sub

    Private Sub AcreditarStockLocal(idProducto As Integer, cantidad As Decimal)
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(
                "IF EXISTS (SELECT 1 FROM ProductosStock WHERE idProducto=@p AND idBodega=@b) " &
                "    UPDATE ProductosStock SET stock = stock + @c WHERE idProducto=@p AND idBodega=@b " &
                "ELSE " &
                "    INSERT INTO ProductosStock (idProducto,idBodega,stock,pvpUND,Und,idProUndMed) " &
                "    VALUES (@p,@b,@c,0,'UN',1)",
                {"@p", "@b", "@c"},
                {idProducto, TerminalActivo.idBodega, cantidad})
        End Using
    End Sub

    Private Sub RegistrarRecepcionLocal(supabaseId As String,
                                        estado As String,
                                        novedad As String)
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(
                "UPDATE TransferenciaEncabezado " &
                "SET EstadoEnvio=@est, Novedad=@nov, FechaRecepcion=GETDATE() " &
                "WHERE SupabaseId=@sid",
                {"@est", "@nov", "@sid"},
                {estado,
                 If(String.IsNullOrEmpty(novedad), DBNull.Value, CObj(novedad)),
                 supabaseId})
        End Using


    End Sub

    ' ── Loading overlay ──────────────────────────────────────────────────────────
    Private Sub MostrarLoading(mensaje As String)
        lblLoadingMsg.Text = mensaje
        pnlLoading.BringToFront()
        pnlLoading.Visible = True
        lblEstado.ForeColor = _colorEstato

    End Sub

    Private Sub OcultarLoading()
        pnlLoading.Visible = False
    End Sub

    Private Sub OlvDetalle_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles OlvDetalle.ItemChecked
        btnAceptar.Enabled =
                        _items IsNot Nothing AndAlso
                        _items.Any(Function(i) i.Seleccionado)
    End Sub


End Class