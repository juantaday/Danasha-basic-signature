Imports CADsisVenta.DataSetComprasTableAdapters
Imports CADsisVenta.Helpers
Imports SupabaseDataAccess.Repositories
Imports CADsisVenta.Helpers.FInicio
Imports Domain.Logica
Imports Domain.Helpers.Utilities
Imports System.Threading.Tasks
Imports System.Runtime.Remoting.Messaging

Public Class frmTransferencia

    Private ReadOnly _detalle As List(Of DetalleTransferenciaItem)

    Public Sub New(detalle As List(Of DetalleTransferenciaItem))
        InitializeComponent()
        _detalle = detalle
    End Sub

    Private Sub frmTransferencia_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarOrigen()
        CargarDestinos()
        MostrarDetalle()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Close()
    End Sub

    Private Sub btnGuias_Click(sender As Object, e As EventArgs) Handles btnGuias.Click
        Using form As New frmListadoGuiasRemision
            form.ShowDialog()
        End Using
    End Sub

    Private Async Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles btnConfirmar.Click
        ' Validaciones iniciales (UI thread)
        If cboOrigen.SelectedIndex < 0 OrElse cboDestino.SelectedIndex < 0 Then
            MsgBox("Seleccione origen y destino.", MsgBoxStyle.Exclamation, "Requerido")
            Exit Sub
        End If

        Dim idOrigen As Integer = CInt(cboOrigen.SelectedValue)
        Dim idDestino As Integer = CInt(cboDestino.SelectedValue)
        Dim nomOrigen As String = cboOrigen.Text
        Dim nomDestino As String = cboDestino.Text

        Dim confirm As MsgBoxResult = MsgBox(
            "¿Confirma la transferencia de " & _detalle.Count & " producto(s)?" & vbNewLine &
            "DESDE: " & nomOrigen & vbNewLine &
            "HACIA:  " & nomDestino,
            MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Confirmar transferencia")

        If confirm <> MsgBoxResult.Yes Then Exit Sub

        ' Mostrar overlay
        MostrarLoading("Procesando transferencia...")

        ' Capturar datos necesarios antes del hilo secundario
        Dim detalleCopy As List(Of DetalleTransferenciaItem) = _detalle.ToList()
        Dim idUsuario As Integer = UsuarioActivo.IdUsuario

        ' Ejecutar la lógica pesada en segundo plano
        Dim result As TransferResult = Await Task.Run(Function() ProcesarTransferenciaEnSegundoPlano(
            idOrigen, idDestino, nomOrigen, nomDestino, detalleCopy, idUsuario))

        ' Ocultar overlay siempre, incluso en error
        OcultarLoading()

        ' Manejar el resultado en el hilo UI
        If result.Exito Then

            ' Verificar impresión de guía (igual que el código original)
            If myOptnsPrint.idTipoDocumento <> TipoDocumento.GuiaDeRemision Then
                myOptnsPrint.NamePrint = String.Empty
                LoadOptionsPrint(TipoDocumento.GuiaDeRemision)
            End If

            If (String.IsNullOrEmpty(myOptnsPrint.NamePrint)) Then
                Interaction.MsgBox("La impresora no esta configurado..", MsgBoxStyle.Exclamation, "Alerta")
            Else
                ImprimirGuiaRemision(result.IdTransferencia, result.NumeroTransferencia, nomOrigen, nomDestino)
            End If

            MsgBox("✔ Transferencia registrada." & vbNewLine & "Número: " & result.NumeroTransferencia,
                   MsgBoxStyle.Information, "Éxito")

            DialogResult = DialogResult.OK
            Close()

        Else
            MsgBox("Error al procesar: " & result.MensajeError, MsgBoxStyle.Critical, "Error")
        End If
    End Sub

    ''' <summary>
    ''' Lógica completa de transferencia (se ejecuta en hilo de background)
    ''' </summary>
    Private Function ProcesarTransferenciaEnSegundoPlano(idOrigen As Integer, idDestino As Integer,
                                                         nomOrigen As String, nomDestino As String,
                                                         detalle As List(Of DetalleTransferenciaItem),
                                                         idUsuario As Integer) As TransferResult
        Try
            ' 1. Obtener si destino es remoto (consulta rápida)
            Dim tap As New BodegasTableAdapter
            Dim dtDest As DataTable = tap.GetDataByIdBodega(idDestino)
            Dim esRemota As Boolean = CBool(dtDest.Rows(0)("EsSucursalRemota"))

            ' 2. Generar número y crear encabezado
            Dim numTransf As String = GenerarNumTransferencia()
            Dim idTransf As Integer = InsertarEncabezado(numTransf, idOrigen, idDestino, idUsuario)

            ' 3. Insertar detalle
            InsertarDetalle(idTransf, detalle)

            ' 4. Descontar stock origen
            EjecutarSP("sp_TransferenciaDescontarStock", idTransf)

            ' 5. Si es remota, enviar a Supabase; si no, acreditar localmente
            If esRemota Then
                Dim payload = New With {
                    .num_transferencia = numTransf,
                    .bodega_origen_id = idOrigen,
                    .bodega_origen_nom = nomOrigen,
                    .bodega_destino_id = idDestino,
                    .bodega_destino_nom = nomDestino,
                    .detalle = detalle.Select(Function(d) New With {
                        .idProducto = d.idProducto,
                        .nombre = d.NombreProducto,
                        .cantidadEnviada = d.Cantidad,
                        .cantidadRecibida = CType(Nothing, Object),
                        .unidad = d.Unidad
                    }).ToList()
                }
                Dim supabaseId As String = TransferenciaRepository.SubirTransferencia(
                    numTransf, idOrigen, nomOrigen, idDestino, nomDestino, payload.detalle)
                ActualizarSupabaseId(idTransf, supabaseId)
            Else
                EjecutarSP("sp_TransferenciaAcreditarStockLocal", idTransf)
                ActualizarEstado(idTransf, "RECIBIDO")
            End If

            Return New TransferResult With {
                .Exito = True,
                .IdTransferencia = idTransf,
                .NumeroTransferencia = numTransf
            }
        Catch ex As Exception
            Return New TransferResult With {
                .Exito = False,
                .MensajeError = ex.Message
            }
        End Try
    End Function

    ' ── Carga combos (sincrónico, solo al inicio) ────────────────────────────────
    Private Sub CargarOrigen()
        If FInicio.TerminalActivo.idTerminal = 0 Then
            MessageBox.Show("No hay terminal activa.", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim tap As New BodegasTableAdapter
        Dim dt As DataTable = tap.GetBodegasByHostName(TerminalActivo.idTerminal)
        cboOrigen.DataSource = dt
        cboOrigen.DisplayMember = "Nom_Bodega"
        cboOrigen.ValueMember = "idBodega"
        cboOrigen.SelectedValue = TerminalActivo.idBodega
    End Sub

    Private Sub CargarDestinos()
        Dim tap As New BodegasTableAdapter
        Dim dt As DataTable = tap.GetAllBodegas()
        Dim dv As New DataView(dt)
        dv.RowFilter = "idBodega <> " & TerminalActivo.idBodega.ToString()
        cboDestino.DataSource = dv.ToTable()
        cboDestino.DisplayMember = "Nom_Bodega"
        cboDestino.ValueMember = "idBodega"
    End Sub

    Private Sub MostrarDetalle()
        DgvDetalle.DataSource = _detalle
        lblConteo.Text = _detalle.Count & " producto(s)"
    End Sub

    ' ── Loading overlay (sin DoEvents) ───────────────────────────────────────────
    Private Sub MostrarLoading(mensaje As String)
        lblLoadingMsg.Text = mensaje
        pnlLoading.BringToFront()
        pnlLoading.Visible = True
        ' No se necesita Application.DoEvents() con async
    End Sub

    Private Sub OcultarLoading()
        pnlLoading.Visible = False
    End Sub

    ' ── Helpers SQL (sincrónicos, se llaman dentro de Task.Run) ──────────────────
    Private Function GenerarNumTransferencia() As String
        Dim fecha As String = DateTime.Now.ToString("yyyyMMdd")
        Dim sql As String =
            "SELECT ISNULL(MAX(CAST(RIGHT(NumTransferencia,4) AS INT)),0)+1 " &
            "FROM TransferenciaEncabezado WHERE NumTransferencia LIKE 'TRF-" & fecha & "-%'"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            Dim n As Integer = CInt(cmd.RetornaEscalar(sql))
            Return "TRF-" & fecha & "-" & n.ToString("D4")
        End Using
    End Function

    Private Function InsertarEncabezado(num As String, origen As Integer, destino As Integer, idUsuario As Integer) As Integer
        Dim sql As String =
            "INSERT INTO TransferenciaEncabezado " &
            "(NumTransferencia,idBodegaOrigen,idBodegaDestino,idUsuario,EstadoEnvio) " &
            "VALUES (@num,@origen,@destino,@usr,'PENDIENTE'); SELECT SCOPE_IDENTITY();"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            Return CInt(cmd.RetornaEscalarConParams(sql,
                {"@num", "@origen", "@destino", "@usr"},
                {num, origen, destino, idUsuario}))
        End Using
    End Function

    Private Sub InsertarDetalle(idTransf As Integer, detalle As List(Of DetalleTransferenciaItem))
        For Each item In detalle
            Dim sql As String =
                "INSERT INTO TransferenciaDetalle " &
                "(idTransferencia,idProducto,CantidadEnviada,Unidad) " &
                "VALUES (@idT,@idP,@cant,@unidad)"
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
        Dim sql As String =
            "UPDATE TransferenciaEncabezado SET SupabaseId=@sid,EstadoEnvio='ENVIADO' " &
            "WHERE idTransferencia=@id"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(sql, {"@sid", "@id"}, {supabaseId, idTransf})
        End Using
    End Sub

    Private Sub ActualizarEstado(idTransf As Integer, estado As String)
        Dim sql As String =
            "UPDATE TransferenciaEncabezado SET EstadoEnvio=@est WHERE idTransferencia=@id"
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(sql, {"@est", "@id"}, {estado, idTransf})
        End Using
    End Sub

    ' ── Clase auxiliar para resultado de la operación asincrónica ────────────────
    Private Class TransferResult
        Public Property Exito As Boolean
        Public Property IdTransferencia As Integer
        Public Property NumeroTransferencia As String
        Public Property MensajeError As String
    End Class

End Class