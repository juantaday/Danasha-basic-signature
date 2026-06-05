Imports BrightIdeasSoftware
Imports CADsisVenta.Helpers.FInicio
Imports CADsisVenta.Statics
Imports InterfaceSignatureAndSRI.Utils
Imports SupabaseDataAccess.Models
Imports SupabaseDataAccess.Repositories
Imports System.Linq


Public Class frmRecibirTransferencia

    Private _actualizandoHeader As Boolean = False
    Private _transferencias As List(Of Transferencia)
    Private _transferenciaSeleccionada As Transferencia
    Private _productosNuevosCount As Integer = 0
    Private _listCabecera As List(Of TransferenciaItem) = New List(Of TransferenciaItem)
    Private _items As List(Of DetalleTransfItem)
    Private _colorEstato As Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(115, Byte), Integer))
    Private _timerReintento As Timer

    ' Transferencias que quedaron en estado GENERADO (Supabase caído)
    Private _pendientesEnvio As New List(Of PendienteEnvio)
    Private _listMotivos As List(Of MotivoItem)

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
        Public Property DetalleActualizado As Object
    End Class

    Private Class TransferenciaItem
        Public Property Id As Guid
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
                If _items Is Nothing OrElse _actualizandoHeader Then Return

                _actualizandoHeader = True
                Try
                    Dim isChecked = (e.NewCheckState = CheckState.Checked)
                    _items.ForEach(Sub(i) i.Seleccionado = isChecked)
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
                        Dim oldVal = item.CantRecibida
                        item.CantRecibida = decVal

                        '  si las cantidades son concordantes, no se requiere motivo ni estado
                        If (item.CantRecibida = item.CantEnviada) Then
                            item.IdMotivo = Nothing
                            item.EstadoItem = Nothing
                            Return
                        End If

                        Using motivoForm As New frmMotivo(_listMotivos)
                            motivoForm.Configurar(item.Producto, item.CantEnviada, item.CantRecibida, False)
                            If motivoForm.ShowDialog() = DialogResult.OK Then
                                item.IdMotivo = motivoForm.MotivoSeleccionadoId
                                item.EstadoItem = If(item.CantRecibida < item.CantEnviada, "PARCIAL", "RECIBIDO")
                                item.DescMotivo = motivoForm.DescMotivoSeleccionado
                            Else
                                ' Si el usuario cancela el motivo, revertir al valor anterior
                                item.CantRecibida = oldVal
                                OlvDetalle.RefreshObject(item)
                            End If
                        End Using

                    End If

                End Sub
    End Sub

    ' ── Load ────────────────────────────────────────────────────────────────────
    Private Sub frmRecibirTransferencia_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblBodega.Text = "Terminal: " & Dominio._HotName &
                         "   |   Bodega ID: " & TerminalActivo.idBodega &
                         "   |   Nombre: " & TerminalActivo.nombreBodega
        CargarTransferenciasPendientes()
        'ActualizarBotonEnviar()
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
            _transferencias = Await Task.Run(Function()
                                                 ' 1. Traer de Supabase
                                                 Dim todas = TransferenciaRepository.ObtenerPendientesComoObjetos(TerminalActivo.idBodega)

                                                 If todas Is Nothing OrElse todas.Count = 0 Then Return todas

                                                 ' 2. Armar lista de IDs que vinieron de Supabase
                                                 Dim idsSupabase = todas.Select(Function(t) t.Id.ToString()).ToList()

                                                 ' 3. Preguntar a BD local cuáles de esos IDs YA fueron recibidos
                                                 Dim yaRecibidas = ObtenerYaRecibidasDeLista(idsSupabase)

                                                 ' 4. Filtrar en memoria solo las que no están recibidas
                                                 Return todas.Where(Function(t) Not yaRecibidas.Contains(t.Id.ToString())).ToList()
                                             End Function)


            If _listCabecera Is Nothing Then
                _listCabecera = New List(Of TransferenciaItem)
            Else
                _listCabecera.Clear()
            End If




            For Each t As Transferencia In _transferencias
                Dim fecha As String = CDate(t.FechaEmision.ToString()).ToString("dd/MM/yyyy HH:mm")
                _listCabecera.Add(New TransferenciaItem With {
                .Id = t.Id,
                .Accion = "⋯",
                .Numero = t.NumTransferencia,
                .Origen = t.BodegaOrigenNom,
                .Fecha = fecha,
                .Json = t
            })
            Next

            OlvTransferencias.SetObjects(_listCabecera)
            lblEstado.Text = _listCabecera.Count & " transferencia(s) pendiente(s)"

        Catch ex As TimeoutException
            lblEstado.Text = "⚠  Tiempo de espera agotado — Supabase tardó demasiado"
            lblEstado.ForeColor = Color.OrangeRed

        Catch ex As Exception When ex.Message.Contains("Timeout") OrElse
                               ex.Message.Contains("timeout") OrElse
                               ex.Message.Contains("reading attempt")
            ' NpgsqlException de timeout viene como Exception genérica
            lblEstado.Text = "⚠  Sin respuesta de Supabase (timeout)"
            lblEstado.ForeColor = Color.OrangeRed

        Catch ex As Exception When ex.Message.Contains("connection") OrElse
                               ex.Message.Contains("network") OrElse
                               ex.Message.Contains("host")
            lblEstado.Text = "⚠  Sin conexión a Supabase"
            lblEstado.ForeColor = Color.Red

        Catch ex As Exception
            ' Cualquier otro error inesperado — mostrar detalle para diagnóstico
            lblEstado.Text = "⚠  Error al cargar: " & ex.Message
            lblEstado.ForeColor = Color.Red

        Finally
            OcultarLoading()
        End Try
    End Sub

    ' ── Recibe los IDs de Supabase y devuelve solo los que ya están en local ─────
    Private Function ObtenerYaRecibidasDeLista(idsSupabase As List(Of String)) As HashSet(Of String)
        Dim ids As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
        If idsSupabase Is Nothing OrElse idsSupabase.Count = 0 Then Return ids

        Try
            ' Armar IN ('id1','id2','id3'...)
            Dim parametros = String.Join(",", idsSupabase.Select(Function(id) "'" & id & "'"))

            Using cmd As New CADsisVenta.Funtions.SqlComandExec
                Dim dt = cmd.RetornaTabla(
                "SELECT DISTINCT SupabaseId FROM TransferenciaEncabezado " &
                "WHERE TipoMovimiento = 'R' " &
                "AND SupabaseId IN (" & parametros & ")")

                For Each row As DataRow In dt.Rows
                    ids.Add(row("SupabaseId").ToString())
                Next
            End Using
        Catch
            ' Si falla, no filtrar
        End Try
        Return ids
    End Function

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

        Try
            _transferenciaSeleccionada = sel.Json
            OlvTransferencias.SelectedObject = sel
            CargarDetalleEnOLV()
        Catch ex As Exception
            Interaction.MsgBox("Error al cargar detalle: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try


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
                .Seleccionado = True,
                .IdProducto = idProd,
                .Producto = item.NombreProducto,
                .NomComun = item.NomComun,
                .CodProducto = item.CodProducto,
                .IdUnidad = If(item.IdUnidad = 0, 1, item.IdUnidad),
                .IdSubCategoria = If(item.IdSubCategoria = 0, 1, item.IdSubCategoria),
                .IvaPorcentaje = item.IvaPorcentaje,
                .Facturable = item.Facturable,
                .PrecioCompra = item.PrecioCompra,
                .PrecioVenta = item.PrecioVenta,
                .PrecioTotal = item.PrecioTotal,
                .Unidad = If(String.IsNullOrEmpty(item.Unidad), "UN", item.Unidad),
                .CantPresent = If(item.CantPresent = 0, 1, item.CantPresent),
                .CantEnviada = item.CantidadEnviada,
                .CantRecibida = item.CantidadEnviada,
                .EsNuevo = esNuevo,
                .EstadoItem = If(esNuevo, "✦ NUEVO", "En stock")
            })
        Next

        OlvDetalle.SetObjects(_items)

        btnAceptar.Enabled = _items?.Any(Function(x) x.Seleccionado)

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


    ' ── ACEPTAR ─────────────────────────────────────────────────────────────────
    Private Async Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click

        If _transferenciaSeleccionada Is Nothing Then
            MsgBox("Seleccione una transferencia.", MsgBoxStyle.Exclamation, "Requerido")
            Exit Sub
        End If

        Dim hayDiscrepancia = _items IsNot Nothing AndAlso
                          _items.Any(Function(i)
                                         Return (Not i.Seleccionado) OrElse
                                                (i.Seleccionado AndAlso i.CantRecibida < i.CantEnviada)
                                     End Function)

        If hayDiscrepancia AndAlso String.IsNullOrWhiteSpace(txtNovedad.Text) Then
            MsgBox("Hay discrepancias en la recepción." & vbNewLine &
               "Debe ingresar una observación general.",
               MsgBoxStyle.Exclamation, "Observación requerida")
            txtNovedad.Focus()
            Return
        End If

        Dim confirm = MsgBox(
        "¿Confirma la recepción?" & vbNewLine &
        "Los ítems desmarcados no se acreditarán al inventario.",
        MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Confirmar")
        If confirm <> MsgBoxResult.Yes Then Exit Sub

        Dim novedad As String = txtNovedad.Text.Trim()
        Dim nuevosReg As Integer = 0
        Dim estado As String = ""

        Dim detalleActualizado = _items.Select(Function(i)
                                                   Return New DetalleTransferencia With {
                    .IdProducto = i.IdProducto,
                    .NombreProducto = i.Producto,
                    .CantidadEnviada = i.CantEnviada,
                    .CantidadRecibida = If(i.Seleccionado, i.CantRecibida, 0D),
                    .Unidad = i.Unidad,
                    .Estado = If(Not i.Seleccionado, "RECHAZADO",
                                          If(i.CantRecibida < i.CantEnviada, "PARCIAL", "RECIBIDO")),
                    .IdMotivo = If(i.IdMotivo.HasValue, i.IdMotivo, Nothing),
                    .DescMotivo = i.DescMotivo
                }
                                               End Function).ToList()

        Await EjecutarRecepcion(
        mensajeLoading:="Procesando recepción...",
        logicaLocal:=
            Sub(conn, tran)
                Dim idTransfLocal As Integer =
                    InsertarEncabezadoTran(conn, tran, _transferenciaSeleccionada, novedad, "RECIBIDO")

                For Each item As DetalleTransfItem In _items
                    Dim esRecibido = item.Seleccionado AndAlso item.CantRecibida = item.CantEnviada
                    Dim esParcial = item.Seleccionado AndAlso item.CantRecibida < item.CantEnviada
                    Dim esRechazado = Not item.Seleccionado
                    Dim estadoItem = If(esRecibido, "RECIBIDO",
                                     If(esParcial, "PARCIAL",
                                     If(esRechazado, "RECHAZADO", "PENDIENTE")))

                    InsertarDetalleTran(conn, tran, idTransfLocal, item, estadoItem)

                    If item.EsNuevo Then
                        RegistrarProductoNuevo(conn, tran, item)
                        nuevosReg += 1
                    Else
                        ActualizarPrecioCompraTran(conn, tran, item.IdProducto, item.PrecioCompra)
                    End If

                    If item.Seleccionado AndAlso item.CantRecibida > 0 Then
                        AcreditarStockTran(conn, tran, item.IdProducto, item.CantRecibida)
                    End If
                Next

                estado = If(hayDiscrepancia OrElse Not String.IsNullOrEmpty(novedad),
                            "CON_NOVEDAD", "RECIBIDO")
                ActualizarEncabezadoTran(conn, tran, idTransfLocal, estado, novedad)
            End Sub,
        supabaseId:=_transferenciaSeleccionada.Id.ToString(),
        estadoFinal:=Function() estado,
        novedad:=novedad,
        detalleActualizado:=detalleActualizado,
        mensajeFinal:=
        Function(enviado, msgSupa)
            Dim msg = "✔  Recepción guardada localmente.  Estado: " & estado
            If Not enviado Then msg &= vbNewLine & msgSupa
            If nuevosReg > 0 Then msg &= vbNewLine & "✦  " & nuevosReg & " producto(s) nuevo(s) registrados."
            Return msg
        End Function)
    End Sub

    Private Function ObtenerDescripcionMotivo(idMotivo As Integer) As String
        If _listMotivos Is Nothing Then Return Nothing
        Dim motivo = _listMotivos.FirstOrDefault(Function(m) m.Id = idMotivo)
        Return If(motivo IsNot Nothing, motivo.Descripcion, Nothing)
    End Function

    ' ── RECHAZAR ─────────────────────────────────────────────────────────────────
    Private Async Sub mnuRechazar_Click(sender As Object, e As EventArgs) Handles mnuRechazar.Click

        If _transferenciaSeleccionada Is Nothing Then
            MsgBox("Seleccione una transferencia.", MsgBoxStyle.Exclamation, "Requerido")
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtNovedad.Text) Then
            MsgBox("Debe ingresar una descripción general del rechazo.",
               MsgBoxStyle.Exclamation, "Observación requerida")
            txtNovedad.Focus()
            Exit Sub
        End If

        If _items Is Nothing OrElse _items.Count = 0 Then
            MsgBox("No hay detalles para registrar el rechazo.", MsgBoxStyle.Exclamation, "Sin datos")
            Exit Sub
        End If

        Dim novedad As String = "RECHAZADO: " & txtNovedad.Text.Trim()
        Dim estado As String = "RECHAZADO"

        btnAceptar.Enabled = False
        Dim detalleRechazo = _items.Select(Function(i) New With {
            .idProducto = i.IdProducto,
            .nombre = i.Producto,
            .cantidadEnviada = i.CantEnviada,
            .cantidadRecibida = 0D,
            .unidad = i.Unidad,
            .estado = "RECHAZADO"
        }).ToList()

        Await EjecutarRecepcion(
        mensajeLoading:="Registrando rechazo...",
        logicaLocal:=
            Sub(conn, tran)
                Dim idTransfLocal As Integer =
                    InsertarEncabezadoTran(conn, tran, _transferenciaSeleccionada, novedad, estado)

                For Each item As DetalleTransfItem In _items
                    Dim itemDb As New DetalleTransfItem With {
                        .IdProducto = item.IdProducto,
                        .Producto = item.Producto,
                        .Unidad = item.Unidad,
                        .CantEnviada = item.CantEnviada,
                        .CantRecibida = 0D,
                        .Seleccionado = False,
                        .EsNuevo = item.EsNuevo
                    }
                    InsertarDetalleTran(conn, tran, idTransfLocal, itemDb, "RECHAZADO")
                Next

                ActualizarEncabezadoTran(conn, tran, idTransfLocal, estado, novedad)
            End Sub,
        supabaseId:=_transferenciaSeleccionada.Id.ToString(),
        estadoFinal:=Function() estado,
        novedad:=novedad,
        detalleActualizado:=detalleRechazo,
        mensajeFinal:=
        Function(enviado, msgSupa)
            Dim msg = "✔  Rechazo registrado localmente."
            If Not enviado Then msg &= vbNewLine & msgSupa
            Return msg
        End Function)
        btnAceptar.Enabled = True
    End Sub


    ' ── NÚCLEO CENTRALIZADO ───────────────────────────────────────────────────────
    Private Async Function EjecutarRecepcion(
        mensajeLoading As String,
        logicaLocal As Action(Of SqlClient.SqlConnection, SqlClient.SqlTransaction),
        supabaseId As String,
        estadoFinal As Func(Of String),
        novedad As String,
        detalleActualizado As Object,
        mensajeFinal As Func(Of Boolean, String, String)) As Task

        MostrarLoading(mensajeLoading)
        btnAceptar.Enabled = False
        Try
            ' 1. Transacción local
            Await Task.Run(Sub()
                               Using conn As New SqlClient.SqlConnection(
                                   DomainSQLite.Setting.Configuration.ConectionString)
                                   conn.Open()
                                   Using tran As SqlClient.SqlTransaction = conn.BeginTransaction()
                                       Try
                                           logicaLocal(conn, tran)
                                           tran.Commit()
                                       Catch
                                           tran.Rollback()
                                           Throw
                                       End Try
                                   End Using
                               End Using
                           End Sub)

            ' 2. Notificar Supabase
            MostrarLoading("Notificando a Supabase...")
            Dim estado As String = estadoFinal()
            Dim enviado As Boolean = False
            Dim mensajeSupabase As String = Nothing

            Try
                enviado = Await Task.Run(Function()
                                             Return TransferenciaRepository.ActualizarEstado(supabaseId, estado, novedad, detalleActualizado)
                                         End Function)
            Catch ex As Exception When ex.Message.Contains("Timeout") OrElse
                           ex.Message.Contains("timeout") OrElse
                           ex.Message.Contains("reading attempt")
                enviado = False
                mensajeSupabase = "⚠  Supabase no respondió (timeout). Quedó en cola."
            Catch ex As Exception When ex.Message.Contains("connection") OrElse
                           ex.Message.Contains("network") OrElse
                           ex.Message.Contains("host")
                enviado = False
                mensajeSupabase = "⚠  Sin conexión a Supabase. Quedó en cola."
            Catch ex As Exception
                enviado = False
                mensajeSupabase = "⚠  Supabase: " & ex.Message & ". Quedó en cola."
            End Try

            If Not enviado Then
                SupabasePendientesRepository.Guardar(
                    supabaseId, estado, novedad, detalleActualizado)
            End If

            ' 3. Actualizar UI
            OcultarLoading()
            MsgBox(mensajeFinal(enviado, mensajeSupabase), MsgBoxStyle.Information, "Listo")
            txtNovedad.Clear()
            OlvDetalle.ClearObjects()
            _transferenciaSeleccionada = Nothing
            CargarTransferenciasPendientes()

        Catch ex As Exception
            OcultarLoading()
            Log.Error("EjecutarRecepcion", "Transacción cancelada — rollback ejecutado", ex)

            Dim mensajeUsuario As String = TraducirError(ex)
            MsgBox("Error — ningún cambio fue aplicado." & vbNewLine & mensajeUsuario,
            MsgBoxStyle.Critical, "Error")
        Finally
            btnAceptar.Enabled = True
        End Try
    End Function

    ' Función traductora — agregar en el mismo Form o en una clase Helper
    Private Function TraducirError(ex As Exception) As String
        ' Buscar en toda la cadena de excepciones
        Dim actual As Exception = ex
        Do While actual IsNot Nothing
            Dim msg = actual.Message

            If msg.Contains("NULL") AndAlso msg.Contains("columna") Then
                Dim col = ExtraerEntreComillas(msg)
                Return $"Falta un dato requerido ({col}). Contacte al administrador."
            End If

            If msg.Contains("PRIMARY KEY") OrElse msg.Contains("duplicate key") Then
                Return "El registro ya existe. No se puede duplicar."
            End If

            If msg.Contains("FOREIGN KEY") OrElse msg.Contains("REFERENCE") Then
                Return "Un dato relacionado no existe en el sistema. Verifique la configuración."
            End If

            If msg.Contains("SqlTransaction se completó") OrElse
           msg.Contains("ya no se puede utilizar") Then
                Return "Ocurrió un error previo que canceló la operación. Revise el log."
            End If

            actual = actual.InnerException  ' ← subir un nivel
        Loop

        Return "Ocurrió un error inesperado. Contacte al administrador."
    End Function

    Private Function ExtraerEntreComillas(texto As String) As String
        Dim match = System.Text.RegularExpressions.Regex.Match(texto, "'([^']+)'")
        Return If(match.Success, match.Groups(1).Value, "campo desconocido")
    End Function

    Private Sub ReintentarPendientesSupabase(sender As Object, e As EventArgs)
        Dim pendientes = SupabasePendientesRepository.ObtenerPendientes("TRANSFERENCIA")
        If pendientes.Count = 0 Then Return

        For Each p In pendientes
            Try
                Dim detalle As Object = Nothing
                If Not String.IsNullOrEmpty(p.DetalleJson) Then
                    detalle = Newtonsoft.Json.JsonConvert.DeserializeObject(p.DetalleJson)
                End If

                Dim enviado = TransferenciaRepository.ActualizarEstado(
                p.SupabaseId, p.Estado, p.Novedad, detalle)

                If enviado Then
                    SupabasePendientesRepository.MarcarEnviado(p.Id)
                Else
                    SupabasePendientesRepository.RegistrarFallo(p.Id)
                End If
            Catch
                SupabasePendientesRepository.RegistrarFallo(p.Id)
            End Try
        Next
    End Sub

    ' ── Helpers BD local ─────────────────────────────────────────────────────────
    Private Function ProductoExisteLocal(idProducto As Integer) As Boolean
        Dim sql As String = "SELECT COUNT(1) FROM Productos WHERE idProducto = @id"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            Return CInt(cmd.RetornaEscalarConParams(sql, {"@id"}, {idProducto})) > 0
        End Using
    End Function

    Private Function InsertarEncabezadoTran(conn As SqlClient.SqlConnection,
                                         tran As SqlClient.SqlTransaction,
                                         t As Transferencia,
                                         novedad As String,
                                         estado As String) As Integer
        Dim sql =
        "INSERT INTO TransferenciaEncabezado " &
        "  (NumTransferencia, idBodegaOrigen, idBodegaDestino, " &
        "   BodegaOrigenNom, BodegaDestinoNom, FechaEmision, " &
        "   CodUser, EstadoEnvio, Novedad, TipoMovimiento, SupabaseId) " &
        "VALUES (@num,@orig,@dest,@origNom,@destNom,@fec,@usr,@est,@nov,'R',@sid); " &
        "SELECT SCOPE_IDENTITY();"

        Using cmd As New SqlClient.SqlCommand(sql, conn, tran)
            cmd.Parameters.AddWithValue("@num", t.NumTransferencia)
            cmd.Parameters.AddWithValue("@orig", t.BodegaOrigenId)
            cmd.Parameters.AddWithValue("@dest", t.BodegaDestinoId)
            cmd.Parameters.AddWithValue("@origNom", t.BodegaOrigenNom)
            cmd.Parameters.AddWithValue("@destNom", t.BodegaDestinoNom)
            cmd.Parameters.AddWithValue("@fec", t.FechaEmision)
            cmd.Parameters.AddWithValue("@usr", UsuarioActivo.codUser)
            cmd.Parameters.AddWithValue("@est", estado)
            cmd.Parameters.AddWithValue("@nov", If(String.IsNullOrEmpty(novedad),
                                                   CObj(DBNull.Value), CObj(novedad)))
            cmd.Parameters.AddWithValue("@sid", t.Id.ToString())
            Return CInt(cmd.ExecuteScalar())
        End Using
    End Function

    Private Sub InsertarDetalleTran(conn As SqlClient.SqlConnection,
                                 tran As SqlClient.SqlTransaction,
                                 idTransfLocal As Integer,
                                 item As DetalleTransfItem,
                                 estadoItem As String)
        Dim sql =
        "INSERT INTO TransferenciaDetalle " &
        "  (idTransferencia, idProducto, NombreProducto, Unidad, " &
        "   CantidadEnviada, CantidadRecibida, EstadoItem, " &
        "   idMotivoDiscrepancia, EsNuevo, Seleccionado) " &
        "VALUES (@idT,@idP,@nom,@und,@env,@rec,@est,@mot,@nuevo,@sel)"

        Using cmd As New SqlClient.SqlCommand(sql, conn, tran)
            cmd.Parameters.AddWithValue("@idT", idTransfLocal)
            cmd.Parameters.AddWithValue("@idP", item.IdProducto)
            cmd.Parameters.AddWithValue("@nom", item.Producto)
            cmd.Parameters.AddWithValue("@und", If(String.IsNullOrEmpty(item.Unidad),
                                               CObj(DBNull.Value), CObj(item.Unidad)))
            cmd.Parameters.AddWithValue("@env", item.CantEnviada)
            cmd.Parameters.AddWithValue("@rec", If(item.Seleccionado,
                                               CObj(item.CantRecibida), CObj(DBNull.Value)))
            cmd.Parameters.AddWithValue("@est", estadoItem)
            cmd.Parameters.AddWithValue("@mot", If(item.IdMotivo.HasValue,
                                               CObj(item.IdMotivo.Value), CObj(DBNull.Value)))
            cmd.Parameters.AddWithValue("@nuevo", If(item.EsNuevo, 1, 0))
            cmd.Parameters.AddWithValue("@sel", If(item.Seleccionado, 1, 0))
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub ActualizarEncabezadoTran(conn As SqlClient.SqlConnection,
                                      tran As SqlClient.SqlTransaction,
                                      idTransfLocal As Integer,
                                      estado As String,
                                      novedad As String)
        Dim sql =
        "UPDATE TransferenciaEncabezado " &
        "SET EstadoEnvio=@est, Novedad=@nov, FechaRecepcion=GETDATE() " &
        "WHERE idTransferencia=@id"

        Using cmd As New SqlClient.SqlCommand(sql, conn, tran)
            cmd.Parameters.AddWithValue("@est", estado)
            cmd.Parameters.AddWithValue("@nov", If(String.IsNullOrEmpty(novedad),
                                               CObj(DBNull.Value), CObj(novedad)))
            cmd.Parameters.AddWithValue("@id", idTransfLocal)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub AcreditarStockTran(conn As SqlClient.SqlConnection,
                                tran As SqlClient.SqlTransaction,
                                idProducto As Integer,
                                cantidad As Decimal)
        Dim sql =
        "IF EXISTS (SELECT 1 FROM ProductosStock WHERE idProducto=@p AND idBodega=@b) " &
        "    UPDATE ProductosStock SET stock = stock + @c " &
        "    WHERE idProducto=@p AND idBodega=@b " &
        "ELSE " &
        "    INSERT INTO ProductosStock (idProducto,idBodega,stock,pvpUND,Und,idProUndMed) " &
        "    VALUES (@p,@b,@c,0,'UN',1)"

        Using cmd As New SqlClient.SqlCommand(sql, conn, tran)
            cmd.Parameters.AddWithValue("@p", idProducto)
            cmd.Parameters.AddWithValue("@b", TerminalActivo.idBodega)
            cmd.Parameters.AddWithValue("@c", cantidad)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub RegistrarProductoNuevo(
        conn As SqlClient.SqlConnection,
        tran As SqlClient.SqlTransaction,
        item As DetalleTransfItem)

        Dim nomComercial As String = item.Producto
        Dim nomComun As String = If(String.IsNullOrEmpty(item.NomComun),
                                    nomComercial.Substring(0, Math.Min(20, nomComercial.Length)),
                                    item.NomComun)
        Dim codProducto As String = If(String.IsNullOrEmpty(item.CodProducto),
                                    "SN" & item.IdProducto.ToString("D5"),
                                    item.CodProducto)
        Dim unidadPres As String = If(String.IsNullOrEmpty(item.Unidad), "UN", item.Unidad)
        Dim idUnidad As Integer = If(item.IdUnidad = 0, 1, item.IdUnidad)
        Dim idSubcat As Integer = If(item.IdSubCategoria = 0, 1, item.IdSubCategoria)
        Dim precioC As Decimal = If(item.PrecioCompra > 0, item.PrecioCompra, 0)
        Dim precioV As Decimal = item.PrecioVenta
        Dim coduser As String = UsuarioActivo.codUser

        ' ══════════════════════════════════════════════════════════════════
        '  1. Productos — con Deft en 0 temporalmente (FK desactivada)
        ' ══════════════════════════════════════════════════════════════════
        Try
            Dim sql =
            "ALTER TABLE ProductoPresentacion NOCHECK CONSTRAINT FK_PRODUCTO_PRESENTACION; " &
            "SET IDENTITY_INSERT Productos ON; " &
            "INSERT INTO Productos " &
            "  (idProducto,Nom_Comercial,Nom_Comun,Cant_minima," &
            "   idUnidad,IdSubCategoria,ivaPorcentaje,Facturable,Activo," &
            "   Deft_idPresenCompra,Deft_idPresenVenta,coduser) " &
            "VALUES (@id,@nom,@nomC,1,@und,@sub,@iva,1,1,0,0,@cu); " &
            "SET IDENTITY_INSERT Productos OFF;"

            Using cmd As New SqlClient.SqlCommand(sql, conn, tran)
                cmd.Parameters.AddWithValue("@id", item.IdProducto)
                cmd.Parameters.AddWithValue("@nom", nomComercial)
                cmd.Parameters.AddWithValue("@nomC", nomComun)
                cmd.Parameters.AddWithValue("@und", idUnidad)
                cmd.Parameters.AddWithValue("@sub", idSubcat)
                cmd.Parameters.AddWithValue("@iva", item.IvaPorcentaje)
                cmd.Parameters.AddWithValue("@cu", coduser)
                cmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            Log.Error("RegistrarProductoNuevo.Productos",
                  $"INSERT Productos falló — IdProducto={item.IdProducto} | Nom='{nomComercial}'", ex)
            Throw New Exception($"No se pudo registrar el producto '{nomComercial}'.", ex)
        End Try

        ' ══════════════════════════════════════════════════════════════════
        '  2. Presentación — ahora Productos ya existe
        ' ══════════════════════════════════════════════════════════════════
        Dim idPresentacion As Integer
        Try
            Dim sql =
            "INSERT INTO ProductoPresentacion " &
            "  (codProducto,idProducto,idProUndMed,idProUndReferen,Cant_Present," &
            "   precioCompra,precioVenta,Empaquetado,Presentacion,PresentacionPrint," &
            "   isPresentFactory,CodUser) " &
            "VALUES (@cod,@idP,@und,@und,@cantP,@pc,@pv,1,@pres,@presp,1,@cu); " &
            "SELECT SCOPE_IDENTITY();"

            Using cmd As New SqlClient.SqlCommand(sql, conn, tran)
                cmd.Parameters.AddWithValue("@cod", codProducto)
                cmd.Parameters.AddWithValue("@idP", item.IdProducto)
                cmd.Parameters.AddWithValue("@und", idUnidad)
                cmd.Parameters.AddWithValue("@cantP", If(item.CantPresent = 0, 1, item.CantPresent))
                cmd.Parameters.AddWithValue("@pc", precioC)
                cmd.Parameters.AddWithValue("@pv", precioV)
                cmd.Parameters.AddWithValue("@pres", unidadPres)
                cmd.Parameters.AddWithValue("@presp", "[" & unidadPres & "]")
                cmd.Parameters.AddWithValue("@cu", coduser)
                idPresentacion = CInt(cmd.ExecuteScalar())
            End Using

        Catch ex As Exception
            Log.Error("RegistrarProductoNuevo.Presentacion",
                  $"INSERT ProductoPresentacion falló — IdProducto={item.IdProducto} | Cod={codProducto}", ex)
            Throw New Exception($"No se pudo registrar la presentación de '{nomComercial}'.", ex)
        End Try

        ' ══════════════════════════════════════════════════════════════════
        '  3. Actualizar Productos con los Deft reales + reactivar FK
        ' ══════════════════════════════════════════════════════════════════
        Try
            Dim sql =
            "UPDATE Productos " &
            "SET Deft_idPresenCompra=@deftC, Deft_idPresenVenta=@deftV " &
            "WHERE idProducto=@id; " &
            "ALTER TABLE ProductoPresentacion CHECK CONSTRAINT FK_PRODUCTO_PRESENTACION;"

            Using cmd As New SqlClient.SqlCommand(sql, conn, tran)
                cmd.Parameters.AddWithValue("@deftC", idPresentacion)
                cmd.Parameters.AddWithValue("@deftV", idPresentacion)
                cmd.Parameters.AddWithValue("@id", item.IdProducto)
                cmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            Log.Error("RegistrarProductoNuevo.UpdateDeft",
                  $"UPDATE Deft_idPresen falló — IdProducto={item.IdProducto} | idPresentacion={idPresentacion}", ex)
            Throw New Exception($"No se pudo vincular la presentación de '{nomComercial}'.", ex)
        End Try

        ' ══════════════════════════════════════════════════════════════════
        '  4. Stock — solo si no existe (puede haberlo creado un trigger)
        ' ══════════════════════════════════════════════════════════════════
        Try
            Dim sql =
            "IF NOT EXISTS (SELECT 1 FROM ProductosStock WHERE idProducto=@idP AND idBodega=@idB) " &
            "    INSERT INTO ProductosStock " &
            "      (idProducto,idBodega,stock,pvpUND,Und,idProUndMed) " &
            "    VALUES (@idP,@idB,0,@pvp,@und,@undM)"

            Using cmd As New SqlClient.SqlCommand(sql, conn, tran)
                cmd.Parameters.AddWithValue("@idP", item.IdProducto)
                cmd.Parameters.AddWithValue("@idB", TerminalActivo.idBodega)
                cmd.Parameters.AddWithValue("@pvp", precioV)
                cmd.Parameters.AddWithValue("@und", unidadPres)
                cmd.Parameters.AddWithValue("@undM", idUnidad)
                cmd.ExecuteNonQuery()
            End Using

        Catch ex As Exception
            Log.Error("RegistrarProductoNuevo.Stock",
                  $"INSERT ProductosStock falló — IdProducto={item.IdProducto} | Bodega={TerminalActivo.idBodega}", ex)
            Throw New Exception($"No se pudo inicializar el stock de '{nomComercial}' en bodega.", ex)
        End Try

    End Sub
    Private Sub ActualizarPrecioCompraTran(conn As SqlClient.SqlConnection,
                                        tran As SqlClient.SqlTransaction,
                                        idProducto As Integer,
                                        precioCompra As Decimal)
        Dim sql =
        "UPDATE ProductoPresentacion SET precioCompra=@pc WHERE idProducto=@id; " &
        "UPDATE ProductosStock SET ultiMovi=GETDATE() WHERE idProducto=@id;"

        Using cmd As New SqlClient.SqlCommand(sql, conn, tran)
            cmd.Parameters.AddWithValue("@pc", precioCompra)
            cmd.Parameters.AddWithValue("@id", idProducto)
            cmd.ExecuteNonQuery()
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