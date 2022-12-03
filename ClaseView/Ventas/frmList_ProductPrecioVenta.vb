Imports CADsisVenta.DataSetVentas
Imports CADsisVenta.DataSetVentasTableAdapters
Imports CADsisVenta.Helpers.FInicio

Public Class frmList_ProductPrecioVenta
    Protected Friend idProducto As Integer
    Protected Friend idPresent As Integer
    Protected Friend flag As String
    Protected Friend State As _state

    Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub
    Private Sub frmPreciosVenta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        idProducto = 0
        idPresent = 0
        Try
            If txtProduc_Select.Text.Length > 0 Then
                btnBuscar.PerformClick()
            Else
                txtProduc_Select.TabIndex = 0
                txtProduc_Select.Focus()
                datalistado.TabIndex = 1
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnBuscar_Click(sender As System.Object, e As System.EventArgs) Handles btnBuscar.Click
        Try
            If (txtProduc_Select.Text.Length = 0) Or (String.IsNullOrEmpty(txtProduc_Select.Text)) Then
                Return
            End If

            If (txtProduc_Select.Text.Length < 3) Then
                MsgBox("Pocos parámetros para buscar.", MsgBoxStyle.Information, "Aviso")
                Return
            End If

            Me.Cursor = Cursors.WaitCursor
            Me.datalistado.DataSource = Nothing

            If MySelectProduct(txtProduc_Select.Text) Then
                Carga_ListProducto()
            End If

        Catch ex As Exception
        Finally
            Me.Cursor = Cursors.Default
            If datalistado.RowCount = 0 Then
                txtProduc_Select.TabIndex = 0
                datalistado.TabIndex = 1
                txtProduc_Select.Focus()
            Else
                datalistado.TabIndex = 0
                txtProduc_Select.TabIndex = 1
                datalistado.Focus()
            End If
        End Try
    End Sub

    Private Sub Carga_ListProductoWithCodigo(codigo As String)
        Try
            Using adat As New pcdGetListProductVentaTableAdapter
                Using dt As New pcdGetListProductVentaDataTable
                    adat.Fill(dt, codUser:=UsuarioActivo.codUser, codTerminal:=TerminalActivo.codTerminal, idBodega:=TerminalActivo.idBodega)
                    If dt.Rows.Count > 0 Then
                        With datalistado
                            .DataSource = dt
                            .AutoSizeColumnsMode =
                          DataGridViewAutoSizeColumnsMode.AllCells
                            .Columns(4).Visible = False  'id otra presentacion
                            .Columns(5).Visible = False  'id oferta
                            .Columns(6).Visible = False  'id producto REAL
                            .Columns(7).Visible = False  'id presentacion REAL
                        End With
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message + " en el Carga_ListProductoWithCodigo del " + Me.Name, MsgBoxStyle.Critical, "Error al cargar Proveedor")
        End Try
    End Sub
    Private Sub Carga_ListProducto()
        Try
            Using adat As New pcdGetListProductVentaTableAdapter
                Using dt As New pcdGetListProductVentaDataTable
                    adat.Fill(dt, codUser:=UsuarioActivo.codUser, codTerminal:=TerminalActivo.codTerminal, idBodega:=TerminalActivo.idBodega)
                    If dt.Rows.Count > 0 Then
                        With datalistado
                            .DataSource = dt
                            .AutoSizeColumnsMode =
                          DataGridViewAutoSizeColumnsMode.AllCells
                            .Columns(5).Visible = False  'id otra presentacion
                            .Columns(6).Visible = False  'id oferta
                            .Columns(7).Visible = False  'id producto REAL
                            .Columns(8).Visible = False  'id presentacion REAL
                        End With
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message + " en el Carga_ListProducto del " + Me.Name, MsgBoxStyle.Critical, "Error al cargar Proveedor")
        End Try
    End Sub

    Private Sub frmLista_Producto_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load


        Try

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error en el frmLista_Producto_Load")
        End Try

        Me.txtProduc_Select.Focus()

    End Sub

    Private Sub cmbBucarPro_LostFocus(sender As Object, e As System.EventArgs)
        If sender.SelectedIndex < 0 Then
            MsgBox("Seleccione una de la lista despegable", MsgBoxStyle.Exclamation, "Por vafor")
        End If
    End Sub

    Private Sub txtProduc_Select_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtProduc_Select.TextChanged
        SetButtonDefaul(Me.btnBuscar)
    End Sub
    Private Sub SetButtonDefaul(ByVal btn As Button)
        Me.AcceptButton = btn
    End Sub

    Private Sub btnOk_Click(sender As System.Object, e As System.EventArgs) Handles btnOk.Click

        If Me.datalistado.SelectedRows.Count = 1 Then

            idProducto = datalistado.SelectedRows(0).Cells(datalistado.Columns("idProducto").Index).Value
            idPresent = datalistado.SelectedRows(0).Cells(datalistado.Columns("idPresentacion").Index).Value

            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If

    End Sub


    Private Sub datalistado_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles datalistado.CellDoubleClick
        If flag = "Ventas" Then
            btnOk.PerformClick()
        End If
    End Sub
    Private Sub Admin_Controls()
        Try
            If Me.datalistado.SelectedRows.Count = 1 Then
                idProducto = datalistado.SelectedRows(0).Cells(datalistado.Columns("idProducto").Index).Value
                idPresent = datalistado.SelectedRows(0).Cells(datalistado.Columns("idPresentacion").Index).Value
                If Not IsDBNull(Me.datalistado.SelectedCells.Item(4).Value) Then
                    Me.btnEmpaque.Enabled = True
                Else
                    Me.btnEmpaque.Enabled = False
                End If

                If Not IsDBNull(Me.datalistado.SelectedCells.Item(5).Value) Then
                    Me.btnOferta.Enabled = True
                Else
                    Me.btnOferta.Enabled = False
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub btnOferta_Click(sender As Object, e As EventArgs) Handles btnEmpaque.Click
        Try
            Using fornew As New frmProductoPresentacion()
                With fornew
                    .Text = "  UNIDAD DE MEDIDA Y OFERTAS DISPONIBLES"
                    .flag = "Operando"
                    .lblProducto.Text = datalistado.SelectedCells.Item(1).Value
                    .idproducto = datalistado.SelectedCells.Item(6).Value
                    .ShowDialog()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub datalistado_KeyDown(sender As Object, e As KeyEventArgs) Handles datalistado.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                e.SuppressKeyPress = False
                If datalistado.SelectedRows.Count = 1 Then
                    Admin_Controls()
                    btnOk.PerformClick()
                End If
            ElseIf e.KeyCode = Keys.Back Or e.KeyCode = Keys.Tab Then
                txtProduc_Select.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub datalistado_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles datalistado.CellClick
        If sender.SelectedRows.Count = 1 Then
            Admin_Controls()
        End If
    End Sub
    Private Sub btnOferta_Click_1(sender As Object, e As EventArgs) Handles btnOferta.Click

    End Sub
End Class