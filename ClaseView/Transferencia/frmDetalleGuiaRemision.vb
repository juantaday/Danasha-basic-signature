Imports CADsisVenta.Funtions

Public Class frmDetalleGuiaRemision
    Inherits Form

    Private ReadOnly idTransferencia As Integer
    Private ReadOnly numTransferencia As String
    Private ReadOnly origen As String
    Private ReadOnly destino As String

    Private ReadOnly lblHeader As New Label()
    Private ReadOnly dgvDetalle As New DataGridView()

    Public Sub New(idTransf As Integer, numTransf As String, nomOrigen As String, nomDestino As String)
        idTransferencia = idTransf
        numTransferencia = numTransf
        origen = nomOrigen
        destino = nomDestino

        InitializeComponent()
    End Sub

    Private Sub InitializeComponent()
        Text = "Detalle de guía de remisión"
        StartPosition = FormStartPosition.CenterParent
        Size = New Size(860, 520)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False

        Dim pnlHeader As New Panel()
        pnlHeader.Dock = DockStyle.Top
        pnlHeader.Height = 70
        pnlHeader.BackColor = Color.White

        lblHeader.Dock = DockStyle.Fill
        lblHeader.Font = New Font("Segoe UI Semibold", 11.0!, FontStyle.Bold)
        lblHeader.ForeColor = Color.FromArgb(45, 55, 70)
        lblHeader.Padding = New Padding(18, 16, 18, 16)
        lblHeader.TextAlign = ContentAlignment.MiddleLeft

        pnlHeader.Controls.Add(lblHeader)

        Dim pnlGrid As New Panel()
        pnlGrid.Dock = DockStyle.Fill
        pnlGrid.Padding = New Padding(18, 10, 18, 18)

        dgvDetalle.Dock = DockStyle.Fill
        dgvDetalle.ReadOnly = True
        dgvDetalle.AllowUserToAddRows = False
        dgvDetalle.AllowUserToDeleteRows = False
        dgvDetalle.AllowUserToResizeRows = False
        dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvDetalle.RowHeadersVisible = False
        dgvDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        dgvDetalle.BackgroundColor = Color.White

        pnlGrid.Controls.Add(dgvDetalle)

        Controls.Add(pnlGrid)
        Controls.Add(pnlHeader)

        AddHandler Load, AddressOf frmDetalleGuiaRemision_Load
    End Sub

    Private Sub frmDetalleGuiaRemision_Load(sender As Object, e As EventArgs)
        lblHeader.Text = String.Format("Guía: {0} | Origen: {1} | Destino: {2}", numTransferencia, origen, destino)
        CargarDetalle()
    End Sub

    Private Sub CargarDetalle()
        Try
            Dim sql As String =
                "SELECT d.idProducto, p.Nom_Comercial AS Producto, d.CantidadEnviada, d.CantidadRecibida, d.Unidad " &
                "FROM TransferenciaDetalle d " &
                "LEFT JOIN Productos p ON p.idProducto = d.idProducto " &
                "WHERE d.idTransferencia = @id " &
                "ORDER BY d.idDetalle"

            Using cmd As New SqlComandExec
                Dim dt As DataTable = cmd.RetornaTablaConParams(sql, {"@id"}, {idTransferencia})
                dgvDetalle.DataSource = dt

                If dgvDetalle.Columns.Count > 0 Then
                    dgvDetalle.Columns(dgvDetalle.Columns.Count - 1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End If
            End Using
        Catch ex As Exception
            MsgBox("Error al cargar el detalle: " & ex.Message, MsgBoxStyle.Critical, "Error")
            dgvDetalle.DataSource = Nothing
        End Try
    End Sub
End Class
