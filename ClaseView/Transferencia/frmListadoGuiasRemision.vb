Imports Domain.Logica
Imports SupabaseDataAccess.Models
Imports SupabaseDataAccess.Repositories

Public Class frmListadoGuiasRemision

    Private ReadOnly menuAcciones As New ContextMenuStrip()
    Private consultarEstado As New ToolStripMenuItem("Consultar estado")
    Private _currentData As DataGridViewRow
    Private rowAccion As DataGridViewRow

    Public Sub New()
        InitializeComponent()
    End Sub


    Private Sub frmListadoGuiasRemision_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfigurarMenuAcciones()
        ConfigurarColumnaAcciones()
        CargarGuias()
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        CargarGuias()
    End Sub

    Private Sub CargarGuias()
        Try
            Dim sql As String =
                "SELECT t.idTransferencia, t.NumTransferencia, " &
                "bo.Nom_Bodega AS Origen, bd.Nom_Bodega AS Destino, t.EstadoEnvio, t.SupabaseId ,t.Novedad as Estado_Remoto " &
                "FROM TransferenciaEncabezado t " &
                "LEFT JOIN Bodegas bo ON bo.idBodega = t.idBodegaOrigen " &
                "LEFT JOIN Bodegas bd ON bd.idBodega = t.idBodegaDestino " &
                "ORDER BY t.idTransferencia DESC"

            Using cmd As New CADsisVenta.Funtions.SqlComandExec
                Dim dt As DataTable = cmd.RetornaTabla(sql)
                DgvGuias.DataSource = dt

                ConfigurarColumnaAcciones()

                AjustarColumnas()
            End Using
        Catch ex As Exception
            MsgBox("Error al cargar guías: " & ex.Message, MsgBoxStyle.Critical, "Error")
            DgvGuias.DataSource = Nothing
        End Try
    End Sub

    Private Sub AjustarColumnas()
        DgvGuias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

        For i As Integer = DgvGuias.Columns.Count - 1 To 0 Step -1
            Dim col = DgvGuias.Columns(i)
            If col.Name <> "ColAcciones" Then
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                Exit For
            End If
        Next
    End Sub

    Private Sub ConfigurarColumnaAcciones()
        If DgvGuias.Columns.Contains("ColAcciones") Then
            Return
        End If

        Dim colAcciones As New DataGridViewButtonColumn()
        colAcciones.Name = "ColAcciones"
        colAcciones.HeaderText = ""
        colAcciones.Text = "⋯"
        colAcciones.UseColumnTextForButtonValue = True
        colAcciones.ReadOnly = True
        colAcciones.Width = 40
        colAcciones.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells

        DgvGuias.Columns.Add(colAcciones)
        colAcciones.DisplayIndex = DgvGuias.Columns.Count - 1
    End Sub

    Private Sub ConfigurarMenuAcciones()
        If menuAcciones.Items.Count > 0 Then
            Return
        End If

        Dim verDetalle As New ToolStripMenuItem("Ver detalle")

        Dim imprimir As New ToolStripMenuItem("Imprimir")
        Dim eliminar As New ToolStripMenuItem("Eliminar")

        verDetalle.Image = My.Resources.Detail_32
        imprimir.Image = My.Resources.Action_Printing_Print_32x32
        eliminar.Image = My.Resources.Delete_32
        consultarEstado.Image = My.Resources.zoom_Grin_24

        AddHandler verDetalle.Click, AddressOf VerDetalle_Click
        AddHandler imprimir.Click, AddressOf Imprimir_Click
        AddHandler eliminar.Click, AddressOf Eliminar_Click
        AddHandler consultarEstado.Click, AddressOf ConsultarEstado_Click


        menuAcciones.Items.AddRange(New ToolStripItem() {verDetalle, imprimir, eliminar, consultarEstado})
        AddHandler menuAcciones.Opening, AddressOf consultarEstado_DropDownOpening

    End Sub

    Private Sub consultarEstado_DropDownOpening(sender As Object, e As EventArgs)

        If (_currentData Is Nothing) Then
            consultarEstado.Enabled = False
            Return
        End If

        Dim estadoEnvio As String = ObtenerValorCelda(_currentData, "EstadoEnvio")

        If (estadoEnvio Is Nothing) Then
            consultarEstado.Enabled = False
            Return
        End If

        If (Not estadoEnvio.Equals("ENVIADO", StringComparison.OrdinalIgnoreCase)) Then
            consultarEstado.Enabled = False
            Return
        End If

        consultarEstado.Enabled = True
    End Sub

    Private Sub DgvGuias_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvGuias.CellContentClick
        If e.RowIndex < 0 Then
            Return
        End If

        If DgvGuias.Columns(e.ColumnIndex).Name <> "ColAcciones" Then
            Return
        End If

        DgvGuias.Rows(e.RowIndex).Selected = True

        Dim rect = DgvGuias.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, True)
        Dim location = DgvGuias.PointToScreen(New Point(rect.Left, rect.Bottom))

        _currentData = DgvGuias.Rows(e.RowIndex)

        menuAcciones.Show(location)

    End Sub

    Private Function ObtenerValorCelda(row As DataGridViewRow, ParamArray nombres As String()) As Object
        For Each nombre In nombres
            If row.DataGridView.Columns.Contains(nombre) Then
                Return row.Cells(nombre).Value
            End If
        Next

        Return Nothing
    End Function

    Private Function ObtenerDatosSeleccionados(ByRef idTransf As Integer,
                                                ByRef numTransf As String,
                                                ByRef nomOrigen As String,
                                                ByRef nomDestino As String,
                                                ByRef estadoEnvio As String) As Boolean
        If rowAccion Is Nothing Then
            If DgvGuias.SelectedRows.Count > 0 Then
                rowAccion = DgvGuias.SelectedRows(0)
            Else
                Return False
            End If
        End If

        Dim idValue = ObtenerValorCelda(rowAccion, "ColId", "idTransferencia")
        Dim numValue = ObtenerValorCelda(rowAccion, "ColNum", "NumTransferencia")
        Dim origenValue = ObtenerValorCelda(rowAccion, "ColOrigen", "Origen")
        Dim destinoValue = ObtenerValorCelda(rowAccion, "ColDestino", "Destino")
        Dim estadoEnvioValue = ObtenerValorCelda(rowAccion, "ColEstadoEnvio", "EstadoEnvio")

        If idValue Is Nothing OrElse Not Integer.TryParse(idValue.ToString(), idTransf) Then
            Return False
        End If

        numTransf = If(numValue Is Nothing, String.Empty, numValue.ToString())
        nomOrigen = If(origenValue Is Nothing, String.Empty, origenValue.ToString())
        nomDestino = If(destinoValue Is Nothing, String.Empty, destinoValue.ToString())
        estadoEnvio = If(estadoEnvioValue Is Nothing, String.Empty, estadoEnvioValue.ToString())
        Return True

    End Function

    Private Sub VerDetalle_Click(sender As Object, e As EventArgs)
        Dim idTransf As Integer
        Dim numTransf As String
        Dim nomOrigen As String
        Dim nomDestino As String
        Dim estadoEnvio As String

        Try
            If Not ObtenerDatosSeleccionados(idTransf, numTransf, nomOrigen, nomDestino, estadoEnvio) Then
                MsgBox("No se pudo leer la guía seleccionada.", MsgBoxStyle.Exclamation, "Aviso")
                Return
            End If

            Me.Cursor = Cursors.WaitCursor
            Using frm As New frmDetalleGuiaRemision(idTransf, numTransf, nomOrigen, nomDestino)
                frm.ShowDialog(Me)
            End Using
        Catch ex As Exception
            Interaction.MsgBox("Error al abrir el detalle: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub Imprimir_Click(sender As Object, e As EventArgs)
        Dim idTransf As Integer
        Dim numTransf As String
        Dim nomOrigen As String
        Dim nomDestino As String
        Dim estadoEnvio As String

        Try
            Me.Cursor = Cursors.WaitCursor

            If Not ObtenerDatosSeleccionados(idTransf, numTransf, nomOrigen, nomDestino, estadoEnvio) Then
                MsgBox("No se pudo leer la guía seleccionada.", MsgBoxStyle.Exclamation, "Aviso")
                Return
            End If

            ' Verificar impresión de guía (igual que el código original)
            If myOptnsPrint.idTipoDocumento <> TipoDocumento.GuiaDeRemision Then
                myOptnsPrint.NamePrint = String.Empty
                LoadOptionsPrint(TipoDocumento.GuiaDeRemision)
            End If

            If (String.IsNullOrEmpty(myOptnsPrint.NamePrint)) Then
                Interaction.MsgBox("La impresora no esta configurado..", MsgBoxStyle.Exclamation, "Alerta")
            Else
                ImprimirGuiaRemision(idTransf, numTransf, nomOrigen, nomDestino)
            End If


        Catch ex As Exception
            Interaction.MsgBox("Error al imprimir la guía: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            Me.Cursor = Cursors.Default
        End Try


    End Sub

    Private Sub Eliminar_Click(sender As Object, e As EventArgs)
        Dim idTransf As Integer
        Dim numTransf As String
        Dim nomOrigen As String
        Dim nomDestino As String
        Dim estadoEnvio As String

        If Not ObtenerDatosSeleccionados(idTransf, numTransf, nomOrigen, nomDestino, estadoEnvio) Then
            MsgBox("No se pudo leer la guía seleccionada.", MsgBoxStyle.Exclamation, "Aviso")
            Return
        End If

        If MsgBox("¿Desea eliminar la guía seleccionada?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Confirmar") <> MsgBoxResult.Yes Then
            Return
        End If

        Try

            Me.Cursor = Cursors.WaitCursor

            Dim sql As String = "DELETE FROM TransferenciaDetalle WHERE idTransferencia=@id; " &
                                "DELETE FROM TransferenciaEncabezado WHERE idTransferencia=@id;"
            Using cmd As New CADsisVenta.Funtions.SqlComandExec
                cmd.EjecutarConParams(sql, {"@id"}, {idTransf})
            End Using

            If rowAccion IsNot Nothing Then
                DgvGuias.Rows.Remove(rowAccion)
                rowAccion = Nothing
            End If
        Catch ex As Exception
            Interaction.MsgBox("Error al eliminar la guía: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            Me.Cursor = Cursors.Default
        End Try



    End Sub


    Private Sub ConsultarEstado_Click(sender As Object, e As EventArgs)
        Dim SupabaseId As String


        Try
            Dim nombreColumna As String = "Estado_Remoto"
            SupabaseId = ObtenerValorCelda(_currentData, "SupabaseId")
            Dim estado As String = ObtenerValorCelda(_currentData, nombreColumna)?.ToString()


            If (SupabaseId Is Nothing) Then
                Return
            End If

            ' consultar estado en Supabase 
            Me.Cursor = Cursors.WaitCursor
            Me.DgvGuias.Cursor = Cursors.WaitCursor

            ' si ya tiene novedad o estado remoto, no consultar y mostrar lo que tiene
            If Not String.IsNullOrEmpty(estado) AndAlso Not estado.ToUpper().Equals("PENDIENTE") Then
                Return
            End If

            Dim estadoInfo As EstadoGuiaInfo = TransferenciaRepository.ConsultarEstadoGuia(SupabaseId)

            If estadoInfo IsNot Nothing AndAlso Not (ObtenerValorCelda(_currentData, nombreColumna) Is Nothing) Then
                Dim estadoMostrar As String

                If String.IsNullOrWhiteSpace(estadoInfo.Novedad) Then
                    estadoMostrar = estadoInfo.Estado
                Else
                    estadoMostrar = estadoInfo.Novedad
                End If

                _currentData.Cells(nombreColumna).Value = estadoMostrar

                ' Actualiza localmente el estado y novedad en la base de datos  
                Dim sql As String =
                "UPDATE TransferenciaEncabezado " &
                "    SET Novedad = @novedad, " &
                "    FechaRecepcion = @fechaRecepcion " &
                "WHERE SupabaseId = @supabaseId;"

                Using cmd As New CADsisVenta.Funtions.SqlComandExec
                    cmd.EjecutarConParams(sql,
                          {"@novedad", "@fechaRecepcion", "@supabaseId"},
                          {
                           estadoInfo.Estado + ": " + estadoMostrar,
                           If(estadoInfo.FechaRecepcion.HasValue, estadoInfo.FechaRecepcion.Value, DBNull.Value),
                           SupabaseId})
                End Using

            End If


        Catch ex As Exception
            Interaction.MsgBox("Error al eliminar la guía: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            Me.Cursor = Cursors.Default
            Me.DgvGuias.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub btnReimprimir_Click(sender As Object, e As EventArgs)
        If DgvGuias.SelectedRows.Count = 0 Then
            MsgBox("Seleccione una guía para reimprimir.", MsgBoxStyle.Exclamation, "Aviso")
            Return
        End If

        Dim row As DataGridViewRow = DgvGuias.SelectedRows(0)
        Dim idTransf As Integer = CInt(row.Cells("ColId").Value)
        Dim numTransf As String = row.Cells("ColNum").Value.ToString()
        Dim nomOrigen As String = row.Cells("ColOrigen").Value.ToString()
        Dim nomDestino As String = row.Cells("ColDestino").Value.ToString()


        ' Verificar impresión de guía (igual que el código original)
        If myOptnsPrint.idTipoDocumento <> TipoDocumento.GuiaDeRemision Then
            myOptnsPrint.NamePrint = String.Empty
            LoadOptionsPrint(TipoDocumento.GuiaDeRemision)
        End If

        If (String.IsNullOrEmpty(myOptnsPrint.NamePrint)) Then
            Interaction.MsgBox("La impresora no esta configurado..", MsgBoxStyle.Exclamation, "Alerta")
        Else
            ImprimirGuiaRemision(idTransf, numTransf, nomOrigen, nomDestino)
        End If
    End Sub


End Class
