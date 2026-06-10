Imports System.Data.SqlClient
Imports CADsisVenta.Helpers.FInicio
Imports Domain.Data.Entities
Imports Domain.Data.Repositories
Imports DomainSQLite.Crypto
Imports InterfaceSignatureAndSRI.Utils

Public Class frmOtrosLocales

    ' ── Estado ────────────────────────────────────────────────────────────────
    Private _isLoted As Boolean
    Private listBodega As List(Of Bodega)

    ' Loading overlay (creado en código, no en el designer)
    Private pnlLoading As Panel
    Private pnlLoadingBox As Panel
    Private lblLoadingMsg As Label
    Private tmrDots As System.Windows.Forms.Timer
    Private _dotCount As Integer

    ' ── Constructor ───────────────────────────────────────────────────────────
    Sub New()
        InitializeComponent()
    End Sub

    ' ── Carga del formulario ──────────────────────────────────────────────────
    Private Sub frmOtrosLocales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _isLoted = True
        RjRadioButton1.Checked = True
        ApplyGridStyle()
        BuildLoadingOverlay()
        SetStatus("Listo · Ingrese un producto para consultar el inventario")
    End Sub

    ' ── Estilo del grid ───────────────────────────────────────────────────────
    Private Sub ApplyGridStyle()
        With dgvOtrosLocales
            .EnableHeadersVisualStyles = False
            .ReadOnly = True
            .AllowUserToAddRows = False
            .AllowUserToDeleteRows = False
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
        End With
    End Sub

    ' ── Radio buttons ─────────────────────────────────────────────────────────
    Private Sub RjRadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RjRadioButton1.CheckedChanged
        If Not _isLoted Then Return
        If RjRadioButton1.Checked Then
            ComboBox1.Enabled = False
            ComboBox1.Text = String.Empty
        End If
    End Sub

    Private Sub RjRadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RjRadioButton2.CheckedChanged
        If Not _isLoted Then Return
        If RjRadioButton2.Checked Then
            ComboBox1.Enabled = True
            If ComboBox1.DataSource Is Nothing Then
                GetDataWareHouse()
            End If
        End If
    End Sub

    ' ── Carga bodegas ─────────────────────────────────────────────────────────
    Private Sub GetDataWareHouse()
        Try
            Me.listBodega = BodegaRepository.TraeListaExeptEsta(TerminalActivo.idBodega,
                                                                       DomainSQLite.Setting.Configuration.ConectionString)
            ComboBox1.DataSource = listBodega
            ComboBox1.DisplayMember = "NomBodega"
            ComboBox1.ValueMember = "IdBodega"
        Catch ex As Exception
            Interaction.MsgBox("Error al cargar los locales: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    ' ── Búsqueda ──────────────────────────────────────────────────────────────
    Private Sub TextBoxRounded1_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles TextBoxRounded1.KeyDown
        If e.KeyCode = Keys.Enter AndAlso TextBoxRounded1.Text.Length >= 3 Then
            TextBoxRounded1.Button.PerformClick()
        End If
    End Sub

    Private Async Sub TextBoxRounded1_ButtonClick(sender As Object, e As EventArgs) Handles TextBoxRounded1.ButtonClick
        Dim filterText = TextBoxRounded1.Text?.Trim()

        If String.IsNullOrWhiteSpace(filterText) OrElse
           filterText.Equals(TextBoxRounded1.PlaceHolderText) OrElse
           filterText.Length < 3 Then
            Return
        End If

        Try
            TextBoxRounded1.Enabled = False
            ShowLoading("Consultando inventario")

            If RjRadioButton1.Checked Then
                If listBodega Is Nothing OrElse listBodega.Count = 0 Then
                    GetDataWareHouse()
                End If

                Dim result = Await Task.Run(Function() BuildCombinedInventory(filterText))

                If (Not result.ErrorMsg Is Nothing AndAlso Not String.IsNullOrEmpty(result.ErrorMsg)) Then
                    Interaction.MsgBox(result.ErrorMsg, MsgBoxStyle.Critical, "Error")
                End If

                dgvOtrosLocales.DataSource = result.Data
                ConfigureGridColumns()
                ColorizeStockRows()
                SetStatus($"Se encontraron {dgvOtrosLocales.Rows.Count} registros en todos los locales")

            ElseIf RjRadioButton2.Checked Then
                Dim selectedBodega = TryCast(ComboBox1.SelectedItem, Bodega)
                If selectedBodega Is Nothing Then
                    Interaction.MsgBox("Seleccione una bodega.", MsgBoxStyle.Exclamation, "Aviso")
                    Return
                End If

                Dim result = Await Task.Run(Function() QueryRemoteInventory(selectedBodega, filterText))

                If result.ErrorMsg IsNot Nothing Then
                    MsgBox(result.ErrorMsg, MsgBoxStyle.Critical, "Error de conexión")
                End If
                dgvOtrosLocales.DataSource = result.Data
                ConfigureGridColumns()
                ColorizeStockRows()
                SetStatus($"Se encontraron {dgvOtrosLocales.Rows.Count} registros en {selectedBodega.NomBodega}")
            End If
        Finally
            HideLoading()
            TextBoxRounded1.Enabled = True
        End Try
    End Sub

    ' ── Configuración de columnas ─────────────────────────────────────────────
    Private Sub ConfigureGridColumns()
        If dgvOtrosLocales Is Nothing OrElse dgvOtrosLocales.Columns.Count = 0 Then Return

        Dim idx As Integer = 0

        If dgvOtrosLocales.Columns.Contains("NombreBodega") Then
            With dgvOtrosLocales.Columns("NombreBodega")
                .DisplayIndex = idx : .HeaderText = "Bodega"
            End With
            idx += 1
        End If

        If dgvOtrosLocales.Columns.Contains("Producto") Then
            With dgvOtrosLocales.Columns("Producto")
                .DisplayIndex = idx : .HeaderText = "Producto"
            End With
            idx += 1
        End If

        If dgvOtrosLocales.Columns.Contains("Unidad") Then
            With dgvOtrosLocales.Columns("Unidad")
                .DisplayIndex = idx : .HeaderText = "Unidad"
            End With
            idx += 1
        End If

        If dgvOtrosLocales.Columns.Contains("Stock") Then
            With dgvOtrosLocales.Columns("Stock")
                .DisplayIndex = idx : .HeaderText = "Stock"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            idx += 1
        End If

        If dgvOtrosLocales.Columns.Contains("PrecioPromedio") Then
            With dgvOtrosLocales.Columns("PrecioPromedio")
                .DisplayIndex = idx : .HeaderText = "Precio Promedio"
                .DefaultCellStyle.Format = "N2"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            idx += 1
        End If



        If dgvOtrosLocales.Columns.Contains("UltimoMovimiento") Then
            With dgvOtrosLocales.Columns("UltimoMovimiento")
                .DisplayIndex = idx : .HeaderText = "Último movimiento"
            End With
        End If

        If dgvOtrosLocales.Columns.Contains("IdBodega") Then
            dgvOtrosLocales.Columns("IdBodega").Visible = False
        End If
    End Sub

    ' ── Colorear filas por stock ──────────────────────────────────────────────
    '    Stock > 0  → fondo blanco/normal, texto verde oscuro en la celda Stock
    '    Stock = 0  → muestra "–" y texto gris claro (sin llamar la atención)
    Private Sub ColorizeStockRows()
        ' El handler se registra con Handles directamente, no hace falta hacer nada aquí.
    End Sub

    Private Sub dgvOtrosLocales_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvOtrosLocales.CellFormatting
        If dgvOtrosLocales.Columns.Count = 0 Then Return
        If e.RowIndex < 0 Then Return

        Dim colName = dgvOtrosLocales.Columns(e.ColumnIndex).Name

        ' ── Precio Promedio ─────────────────────────────────────────────────────
        If colName = "PrecioPromedio" Then
            Dim rawP = e.Value
            Dim precio As Double = 0
            If rawP IsNot Nothing AndAlso rawP IsNot DBNull.Value Then
                Double.TryParse(rawP.ToString(), precio)
            End If
            If precio <= 0 Then
                e.Value = "—"
                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(190, 195, 210)
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                e.FormattingApplied = True
            Else
                e.Value = precio.ToString("N2")
                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(40, 50, 80)
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                e.FormattingApplied = True
            End If
        End If

        ' ── Stock ────────────────────────────────────────────────────────────────
        If colName = "Stock" Then
            Dim raw = e.Value
            Dim stock As Double = 0

            If raw IsNot Nothing AndAlso raw IsNot DBNull.Value Then
                Double.TryParse(raw.ToString(), stock)
            End If

            If stock <= 0 Then
                e.Value = "—"
                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(190, 195, 210)
                e.CellStyle.Font = New System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Regular)
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                e.FormattingApplied = True
            Else
                e.Value = stock.ToString("N1")
                e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(34, 139, 87)
                e.CellStyle.Font = New System.Drawing.Font("Segoe UI", 10.5!, System.Drawing.FontStyle.Bold)
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                e.FormattingApplied = True
            End If
        End If
    End Sub

    ' ── Construcción de inventario combinado ──────────────────────────────────
    Private Function BuildCombinedInventory(filterText As String) As (Data As DataTable, ErrorMsg As String)
        Dim combined As New DataTable()
        Dim processedIps As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)

        For Each bodega In listBodega.Where(Function(x) x.IdBodega <> TerminalActivo.idBodega)
            If String.IsNullOrWhiteSpace(bodega.TailscaleIp) Then Continue For
            If processedIps.Contains(bodega.TailscaleIp) Then Continue For

            Dim result = QueryRemoteInventory(bodega, filterText)

            If result.ErrorMsg IsNot Nothing Then
                Return (combined, result.ErrorMsg)
            End If

            If result.Data IsNot Nothing Then
                If combined.Columns.Count = 0 Then
                    combined = result.Data.Clone()
                    combined.Columns.Add("NombreBodega", GetType(String))
                End If

                For Each row As DataRow In result.Data.Rows
                    Dim newRow = combined.NewRow()
                    newRow.ItemArray = row.ItemArray
                    newRow("NombreBodega") = bodega.NomBodega
                    combined.Rows.Add(newRow)
                Next
            End If

            processedIps.Add(bodega.TailscaleIp)
        Next

        Return (combined, "")
    End Function

    ' ── Consulta remota ───────────────────────────────────────────────────────
    Private Function QueryRemoteInventory(bodega As Bodega, filterText As String) As (Data As DataTable, ErrorMsg As String)

        If String.IsNullOrWhiteSpace(bodega.TailscaleIp) OrElse
       String.IsNullOrWhiteSpace(bodega.TailscaleUsuario) OrElse
       String.IsNullOrWhiteSpace(bodega.TailscalePassword) OrElse
       String.IsNullOrWhiteSpace(bodega.TailscaleDatabase) Then
            Return (Nothing, Nothing)   ' sin config → sin error que mostrar
        End If

        Dim tailscaleIp = DomainSQLite.Crypto.Encriptador.DesencriptarValor(bodega.TailscaleIp)
        Dim userName = bodega.TailscaleUsuario
        Dim password = DomainSQLite.Crypto.Encriptador.DesencriptarValor(bodega.TailscalePassword)
        Dim databaseName = bodega.TailscaleDatabase

        Dim connectionString = $"Data Source={tailscaleIp};Initial Catalog={databaseName};User ID={userName};Password={password};TrustServerCertificate=True;Timeout=7"

        Dim sql = "SELECT s.idBodega        AS IdBodega, " &
              "       p.Nom_Comercial       AS Producto, " &
              "       s.Und                 AS Unidad, " &
              "       ROUND(s.stock,  1)    AS Stock, " &
              "       ROUND(s.pvpUND, 2)    AS PrecioPromedio, " &
              "       s.ultiMovi            AS UltimoMovimiento " &
              "FROM   Productos             AS p " &
              "INNER JOIN ProductosStock     AS s ON s.idProducto = p.idProducto " &
              "WHERE  s.idBodega = @idBodega"

        If Not String.IsNullOrWhiteSpace(filterText) AndAlso
       Not filterText.Equals(TextBoxRounded1.PlaceHolderText) Then
            sql &= " AND p.Nom_Comercial LIKE @filter"
        End If

        Dim dt As New DataTable()

        Try
            Using cnn As New SqlConnection(connectionString)
                cnn.Open()
                Using cmd As New SqlCommand(sql, cnn)
                    cmd.Parameters.AddWithValue("@idBodega", bodega.IdBodega)
                    If sql.Contains("@filter") Then
                        cmd.Parameters.AddWithValue("@filter", $"%{filterText}%")
                    End If
                    Using adapter As New SqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            Log.Error($"QueryRemoteInventory", $"Al consultar en la bodega {bodega.NomBodega}", ex)
            ' ← devuelve el mensaje en lugar de mostrarlo aquí
            Return (Nothing, $"Error al consultar {bodega.NomBodega}:{vbCrLf}{ex.Message}")
        End Try

        Return (dt, Nothing)
    End Function
    ' ── Loading overlay animado ───────────────────────────────────────────────
    Private Sub BuildLoadingOverlay()
        ' Panel semitransparente que cubre todo el formulario
        pnlLoading = New Panel()
        pnlLoading.Dock = DockStyle.Fill
        pnlLoading.BackColor = System.Drawing.Color.FromArgb(100, 235, 238, 248)
        pnlLoading.Visible = False

        ' Caja central con mensaje
        pnlLoadingBox = New Panel()
        pnlLoadingBox.Size = New System.Drawing.Size(320, 90)
        pnlLoadingBox.BackColor = System.Drawing.Color.White
        ' Borde redondeado aproximado vía Paint
        AddHandler pnlLoadingBox.Paint, AddressOf DrawRoundedBox

        lblLoadingMsg = New Label()
        lblLoadingMsg.AutoSize = False
        lblLoadingMsg.Dock = DockStyle.Fill
        lblLoadingMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        lblLoadingMsg.ForeColor = System.Drawing.Color.FromArgb(67, 97, 238)
        lblLoadingMsg.Font = New System.Drawing.Font("Segoe UI", 13, System.Drawing.FontStyle.Bold)
        lblLoadingMsg.Text = "Consultando inventario..."

        ' Centrar pnlLoadingBox al redimensionar
        AddHandler pnlLoading.Resize, Sub(s, ev) CenterLoadingBox()

        pnlLoadingBox.Controls.Add(lblLoadingMsg)
        pnlLoading.Controls.Add(pnlLoadingBox)
        Controls.Add(pnlLoading)
        pnlLoading.BringToFront()

        ' Timer para animar los puntos
        tmrDots = New System.Windows.Forms.Timer()
        tmrDots.Interval = 500
        AddHandler tmrDots.Tick, AddressOf AnimateDots
    End Sub

    Private Sub CenterLoadingBox()
        If pnlLoading Is Nothing OrElse pnlLoadingBox Is Nothing Then Return
        pnlLoadingBox.Location = New System.Drawing.Point(
            (pnlLoading.Width - pnlLoadingBox.Width) \ 2,
            (pnlLoading.Height - pnlLoadingBox.Height) \ 2)
    End Sub

    Private Sub DrawRoundedBox(sender As Object, e As System.Windows.Forms.PaintEventArgs)
        Dim p = CType(sender, Panel)
        Dim g = e.Graphics
        Dim rect = New System.Drawing.Rectangle(1, 1, p.Width - 3, p.Height - 3)
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
        Using pen As New System.Drawing.Pen(System.Drawing.Color.FromArgb(67, 97, 238), 2)
            DrawRoundRect(g, pen, rect, 14)
        End Using
    End Sub

    Private Sub DrawRoundRect(g As System.Drawing.Graphics, pen As System.Drawing.Pen,
                               rect As System.Drawing.Rectangle, radius As Integer)
        Dim d = radius * 2
        Using path As New System.Drawing.Drawing2D.GraphicsPath()
            path.AddArc(rect.X, rect.Y, d, d, 180, 90)
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90)
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90)
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90)
            path.CloseFigure()
            g.DrawPath(pen, path)
        End Using
    End Sub

    Private Sub AnimateDots(sender As Object, e As EventArgs)
        _dotCount = (_dotCount + 1) Mod 4
        If lblLoadingMsg IsNot Nothing Then
            Dim base As String
            If lblLoadingMsg.Tag IsNot Nothing Then
                base = lblLoadingMsg.Tag.ToString()
            Else
                base = "Consultando inventario"
            End If
            lblLoadingMsg.Text = base & New String("."c, _dotCount + 1)
        End If
    End Sub

    Private Sub ShowLoading(message As String)
        If pnlLoading Is Nothing Then BuildLoadingOverlay()
        lblLoadingMsg.Tag = message
        lblLoadingMsg.Text = message & "."
        _dotCount = 0
        CenterLoadingBox()
        pnlLoading.BringToFront()
        pnlLoading.Visible = True
        tmrDots.Start()
        pnlLoading.Refresh()
    End Sub

    Private Sub HideLoading()
        tmrDots?.Stop()
        If pnlLoading IsNot Nothing Then
            pnlLoading.Visible = False
        End If
    End Sub

    ' ── Barra de estado ───────────────────────────────────────────────────────
    Private Sub SetStatus(text As String)
        If lblStatusInfo IsNot Nothing Then
            lblStatusInfo.Text = "  " & text
        End If
    End Sub

End Class
