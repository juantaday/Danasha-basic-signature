Imports CADsisVenta.Helpers.FInicio

Public Class frmListadoGuiasRemision
    Inherits Form

    Private DgvGuias As DataGridView
    Private btnActualizar As Button
    Private btnReimprimir As Button
    Private btnCerrar As Button

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub InitializeComponent()
        Text = "LISTADO DE GUIAS DE REMISION"
        StartPosition = FormStartPosition.CenterParent
        Size = New Drawing.Size(860, 520)

        DgvGuias = New DataGridView With {
            .Location = New Drawing.Point(20, 20),
            .Size = New Drawing.Size(800, 380),
            .ReadOnly = True,
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            .AutoGenerateColumns = False
        }

        Dim colId As New DataGridViewTextBoxColumn With {.Name = "ColId", .DataPropertyName = "idTransferencia", .Visible = False}
        Dim colNum As New DataGridViewTextBoxColumn With {.Name = "ColNum", .HeaderText = "Guía", .DataPropertyName = "NumTransferencia", .Width = 160}
        Dim colOrigen As New DataGridViewTextBoxColumn With {.Name = "ColOrigen", .HeaderText = "Origen", .DataPropertyName = "Origen", .Width = 200}
        Dim colDestino As New DataGridViewTextBoxColumn With {.Name = "ColDestino", .HeaderText = "Destino", .DataPropertyName = "Destino", .Width = 200}
        Dim colEstado As New DataGridViewTextBoxColumn With {.Name = "ColEstado", .HeaderText = "Estado", .DataPropertyName = "EstadoEnvio", .Width = 120}
        DgvGuias.Columns.AddRange(New DataGridViewColumn() {colId, colNum, colOrigen, colDestino, colEstado})

        btnActualizar = New Button With {.Text = "Actualizar", .Location = New Drawing.Point(20, 420), .Width = 120}
        btnReimprimir = New Button With {.Text = "Reimprimir", .Location = New Drawing.Point(150, 420), .Width = 120}
        btnCerrar = New Button With {.Text = "Cerrar", .Location = New Drawing.Point(700, 420), .Width = 120}

        Controls.Add(DgvGuias)
        Controls.Add(btnActualizar)
        Controls.Add(btnReimprimir)
        Controls.Add(btnCerrar)

        AddHandler Load, AddressOf frmListadoGuiasRemision_Load
        AddHandler btnActualizar.Click, AddressOf btnActualizar_Click
        AddHandler btnReimprimir.Click, AddressOf btnReimprimir_Click
        AddHandler btnCerrar.Click, Sub(sender, e) Close()
    End Sub

    Private Sub frmListadoGuiasRemision_Load(sender As Object, e As EventArgs)
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
