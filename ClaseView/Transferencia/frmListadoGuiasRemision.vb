Imports CADsisVenta.Helpers.FInicio

Public Class frmListadoGuiasRemision

    Public Sub New()
        InitializeComponent()
    End Sub


    Private Sub frmListadoGuiasRemision_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarGuias()
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs)
        CargarGuias()
    End Sub

    Private Sub CargarGuias()
        Try
            Dim sql As String =
                "SELECT t.idTransferencia, t.NumTransferencia, " &
                "bo.Nom_Bodega AS Origen, bd.Nom_Bodega AS Destino, t.EstadoEnvio " &
                "FROM TransferenciaEncabezado t " &
                "LEFT JOIN Bodegas bo ON bo.idBodega = t.idBodegaOrigen " &
                "LEFT JOIN Bodegas bd ON bd.idBodega = t.idBodegaDestino " &
                "ORDER BY t.idTransferencia DESC"

            Using cmd As New CADsisVenta.Funtions.SqlComandExec
                Dim dt As DataTable = cmd.RetornaTabla(sql)
                DgvGuias.DataSource = dt

                ' ── Ancho automático por contenido ──────────────────────────
                DgvGuias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

                ' Opcional: que la última columna ocupe el espacio restante
                DgvGuias.Columns(DgvGuias.Columns.Count - 1).AutoSizeMode =
                DataGridViewAutoSizeColumnMode.Fill
            End Using
        Catch ex As Exception
            MsgBox("Error al cargar guías: " & ex.Message, MsgBoxStyle.Critical, "Error")
            DgvGuias.DataSource = Nothing
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

        ImprimirGuiaRemision(idTransf, numTransf, nomOrigen, nomDestino)
    End Sub


End Class
