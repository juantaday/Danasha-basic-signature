Imports CADsisVenta.Helpers
Imports CADsisVenta.Helpers.FInicio
Imports Newtonsoft.Json.Linq

Public Class frmRecibirTransferencia

    Private _transferenciasJson As JArray
    Private _transferenciaSeleccionada As JObject
    Private _productosNuevosCount As Integer = 0

    Public Sub New()
        InitializeComponent()
    End Sub

    ' ── Load ────────────────────────────────────────────────────────────────────
    Private Sub frmRecibirTransferencia_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblBodega.Text = "Terminal: " & Dominio._HotName & "   |   Bodega ID: " & TerminalActivo.idBodega
        CargarTransferenciasPendientes()
    End Sub

    ' ── Botones ─────────────────────────────────────────────────────────────────
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Close()
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        CargarTransferenciasPendientes()
    End Sub

    ' ── Cargar lista de pendientes ───────────────────────────────────────────────
    Private Sub CargarTransferenciasPendientes()
        MostrarLoading("Consultando transferencias pendientes...")
        Try
            Dim json As String = SupabaseHelper.ObtenerTransferenciasPendientesAsync(
                                     TerminalActivo.idBodega).Result

            If String.IsNullOrEmpty(json) OrElse json = "[]" Then
                lblEstado.Text = "Sin transferencias pendientes"
                ListBoxTransf.DataSource = Nothing
                DgvDetalle.Rows.Clear()
                OcultarLoading()
                Return
            End If

            _transferenciasJson = JArray.Parse(json)
            Dim lista As New List(Of String)
            For Each t As JObject In _transferenciasJson
                Dim fecha As String = CDate(t("fecha_emision").ToString()).ToString("dd/MM/yyyy HH:mm")
                lista.Add(String.Format("  {0}   |   Desde: {1}   |   {2}",
                          t("num_transferencia"), t("bodega_origen_nom"), fecha))
            Next
            ListBoxTransf.DataSource = lista
            lblEstado.Text = _transferenciasJson.Count & " transferencia(s) pendiente(s)"

        Catch ex As Exception
            lblEstado.Text = "⚠  Error al conectar con Supabase"
            MsgBox("Error Supabase: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            OcultarLoading()
        End Try
    End Sub

    ' ── Selección en lista ──────────────────────────────────────────────────────
    Private Sub ListBoxTransf_SelectedIndexChanged(sender As Object, e As EventArgs) _
            Handles ListBoxTransf.SelectedIndexChanged
        If ListBoxTransf.SelectedIndex < 0 Then Exit Sub
        _transferenciaSeleccionada = _transferenciasJson(ListBoxTransf.SelectedIndex)
        CargarDetalleEnGrid()
    End Sub

    Private Sub CargarDetalleEnGrid()
        DgvDetalle.Rows.Clear()
        _productosNuevosCount = 0

        If _transferenciaSeleccionada Is Nothing Then Exit Sub

        Dim detalle As JArray = _transferenciaSeleccionada("detalle")
        For Each item As JObject In detalle
            Dim idProd As Integer = CInt(item("idProducto").ToString())
            Dim esNuevo As Boolean = Not ProductoExisteLocal(idProd)
            If esNuevo Then _productosNuevosCount += 1

            Dim rowIdx As Integer = DgvDetalle.Rows.Add()
            With DgvDetalle.Rows(rowIdx)
                .Cells("ColCheck").Value = True
                .Cells("ColProducto").Value = item("nombre").ToString()
                .Cells("ColEnviado").Value = item("cantidadEnviada").ToString()
                .Cells("ColRecibido").Value = item("cantidadEnviada").ToString()
                .Cells("ColUnidad").Value = If(item("unidad")?.ToString(), "")
                .Cells("ColEsNuevo").Value = If(esNuevo, "✦ NUEVO", "En stock")
                .Tag = idProd

                ' Resaltar filas de productos nuevos
                If esNuevo Then
                    .DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(100, 220, 140)
                End If
            End With
        Next

        ' Mostrar aviso si hay productos nuevos
        If _productosNuevosCount > 0 Then
            lblNuevosAviso.Text = "✦ " & _productosNuevosCount &
                                  " producto(s) nuevo(s) se registrarán automáticamente"
        Else
            lblNuevosAviso.Text = ""
        End If
    End Sub

    ' ── Aceptar recepción ────────────────────────────────────────────────────────
    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
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
            Dim supabaseId As String = _transferenciaSeleccionada("id").ToString()
            Dim novedad As String = txtNovedad.Text.Trim()
            Dim hayNovedad As Boolean = False
            Dim nuevosRegistrados As Integer = 0

            Dim detalleActualizado As New List(Of Object)

            For Each row As DataGridViewRow In DgvDetalle.Rows
                If row.IsNewRow Then Continue For

                Dim checked As Boolean = CBool(row.Cells("ColCheck").Value)
                Dim idProd As Integer = CInt(row.Tag)
                Dim cantEnviada As Decimal = CDec(row.Cells("ColEnviado").Value)
                Dim cantRecibida As Decimal = If(checked, CDec(row.Cells("ColRecibido").Value), 0D)

                If cantRecibida <> cantEnviada Then hayNovedad = True

                If checked AndAlso cantRecibida > 0 Then
                    MostrarLoading("Verificando producto ID " & idProd & "...")

                    ' ── Auto-registrar producto si no existe ──────────────────
                    If Not ProductoExisteLocal(idProd) Then
                        RegistrarProductoNuevo(idProd,
                            row.Cells("ColProducto").Value?.ToString(),
                            row.Cells("ColUnidad").Value?.ToString())
                        nuevosRegistrados += 1
                    End If

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

            ' Actualizar Supabase
            MostrarLoading("Actualizando estado en Supabase...")
            Dim estado As String = If(hayNovedad OrElse Not String.IsNullOrEmpty(novedad),
                                      "CON_NOVEDAD", "RECIBIDO")
            SupabaseHelper.ActualizarEstadoAsync(supabaseId, estado, novedad).Wait()

            ' Registro local para trazabilidad
            RegistrarRecepcionLocal(supabaseId, estado, novedad)

            OcultarLoading()

            Dim msgFinal As String = "✔  Recepción confirmada." & vbNewLine &
                                     "Estado: " & estado
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

    ' ── Helpers ─────────────────────────────────────────────────────────────────

    ''' <summary>Verifica si el producto ya existe en la BD local.</summary>
    Private Function ProductoExisteLocal(idProducto As Integer) As Boolean
        Dim sql As String = "SELECT COUNT(1) FROM Productos WHERE idProducto = @id"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            Return CInt(cmd.RetornaEscalarConParams(sql, {"@id"}, {idProducto})) > 0
        End Using
    End Function

    ''' <summary>
    ''' Registra un producto nuevo en la BD local cuando llega por primera vez
    ''' en una transferencia. Intenta obtener datos completos desde productos_sync
    ''' en Supabase; si no hay datos, inserta con valores mínimos.
    ''' </summary>
    Private Sub RegistrarProductoNuevo(idProducto As Integer,
                                       nombreProducto As String,
                                       unidad As String)
        ' Valores por defecto
        Dim nomComercial As String = If(String.IsNullOrEmpty(nombreProducto), "Producto " & idProducto, nombreProducto)
        Dim codProducto As String = "SN" & idProducto.ToString("D5")
        Dim unidadPres As String = If(String.IsNullOrEmpty(unidad), "UN", unidad)
        Dim idUnidad As Integer = 1
        Dim idSubcat As Integer = 1
        Dim iva As Decimal = 0
        Dim precioC As Decimal = 0
        Dim precioV As Decimal = 0

        ' Intentar obtener datos más completos desde Supabase productos_sync
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
                ' Marcar como aplicado para no repetir
                SupabaseDataAccess.Repositories.ProductoSyncRepository.MarcarAplicado(idProducto)
            End If
        Catch
            ' Sin conexión o sin datos en productos_sync → continúa con mínimos
        End Try

        ' INSERT en Productos
        Dim sqlP As String =
            "SET IDENTITY_INSERT Productos ON; " &
            "INSERT INTO Productos (idProducto,Nom_Comercial,Nom_Comun,Cant_minima," &
            "  idUnidad,IdSubCategoria,ivaPorcentaje,Facturable,Activo) " &
            "VALUES (@id,@nom,@nom,1,@und,@sub,@iva,1,1); " &
            "SET IDENTITY_INSERT Productos OFF;"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(sqlP,
                {"@id", "@nom", "@und", "@sub", "@iva"},
                {idProducto, nomComercial, idUnidad, idSubcat, iva})
        End Using

        ' INSERT en ProductoPresentacion
        Dim sqlPr As String =
            "INSERT INTO ProductoPresentacion " &
            "(codProducto,idProducto,idProUndMed,idProUndReferen,Cant_Present," &
            " precioCompra,precioVenta,Empaquetado,Presentacion,PresentacionPrint,isPresentFactory) " &
            "VALUES (@cod,@idP,@und,@und,1,@pc,@pv,1,@pres,@presp,1)"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(sqlPr,
                {"@cod", "@idP", "@und", "@pc", "@pv", "@pres", "@presp"},
                {codProducto, idProducto, idUnidad, precioC, precioV,
                 unidadPres, "[" & unidadPres & "]"})
        End Using

        ' INSERT en ProdcutStock (nombre real de la tabla)
        Dim sqlS As String =
            "INSERT INTO ProdcutStock (idProducto,idBodega,stock,pvpUND,Und,idProUndMed) " &
            "VALUES (@idP,@idB,0,@pvp,@und,@undM)"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(sqlS,
                {"@idP", "@idB", "@pvp", "@und", "@undM"},
                {idProducto, TerminalActivo.idBodega, precioV, unidadPres, idUnidad})
        End Using
    End Sub

    Private Sub AcreditarStockLocal(idProducto As Integer, cantidad As Decimal)
        Dim sql As String =
            "IF EXISTS (SELECT 1 FROM ProdcutStock WHERE idProducto=@p AND idBodega=@b) " &
            "    UPDATE ProdcutStock SET stock = stock + @c WHERE idProducto=@p AND idBodega=@b " &
            "ELSE " &
            "    INSERT INTO ProdcutStock (idProducto,idBodega,stock,pvpUND,Und,idProUndMed) " &
            "    VALUES (@p,@b,@c,0,'UN',1)"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(sql,
                {"@p", "@b", "@c"},
                {idProducto, TerminalActivo.idBodega, cantidad})
        End Using
    End Sub

    Private Sub RegistrarRecepcionLocal(supabaseId As String, estado As String, novedad As String)
        Dim sql As String =
            "UPDATE TransferenciaEncabezado SET EstadoEnvio=@est,Novedad=@nov," &
            "FechaRecepcion=GETDATE() WHERE SupabaseId=@sid"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(sql,
                {"@est", "@nov", "@sid"},
                {estado, If(String.IsNullOrEmpty(novedad), DBNull.Value, CObj(novedad)), supabaseId})
        End Using
    End Sub

    ' ── Loading overlay ──────────────────────────────────────────────────────────
    Private Sub MostrarLoading(mensaje As String)
        lblLoadingMsg.Text = mensaje
        pnlLoading.BringToFront()
        pnlLoading.Visible = True
        Application.DoEvents()
    End Sub

    Private Sub OcultarLoading()
        pnlLoading.Visible = False
    End Sub

End Class