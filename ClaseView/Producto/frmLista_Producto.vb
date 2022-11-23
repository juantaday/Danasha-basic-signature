Imports System.Data.SqlClient
Imports CADsisVenta.Data.Emuns.EnumSatateModule
Imports CADsisVenta.DataSetVentas
Imports CADsisVenta.DataSetVentasTableAdapters
Imports CADsisVenta.Helpers.FInicio

Public Class frmLista_Producto
    Protected Friend id_proveedor As Integer
    Protected Friend id_Producto As Integer
    Protected Friend id_Product_return As Integer
    Public flag As String

    Private Sub btnBuscar_Click(sender As System.Object, e As System.EventArgs) Handles btnBuscar.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Me.Label2.Text = String.Format("Total registros: {0:N0}", 0)

            Me.datalistado.DataSource = Nothing
            Me.id_Producto = 0
            If MySelectProduct(txtProduc_Select.Text) Then
                Carga_ListProducto()
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub Carga_ListProducto()
        Try

            Using adat As New pcdGetListProductRentableTableAdapter()
                Using dt As New pcdGetListProductRentableDataTable
                    adat.Fill(dt, codTerminal:=TerminalActivo.codTerminal, codUser:=UsuarioActivo.codUser)
                    If dt.Rows.Count > 0 Then
                        With datalistado
                            .DataSource = dt
                            .AutoSizeColumnsMode =
                          DataGridViewAutoSizeColumnsMode.AllCells
                            clm = .Columns("Rentabilidad")
                            clm.DefaultCellStyle = myStilePercentage
                            Me.Label2.Text = String.Format("Total registros: {0:N0}", dt.Rows.Count)
                        End With
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message + " en el Carga_ListProducto del " + Me.Name, MsgBoxStyle.Critical, "Error al cargar Proveedor")
        End Try
    End Sub
    Private Sub frmLista_Producto_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim MenuaDMIN As ContextMenuStrip = ContextMenuAdministra
        Dim MenualECT As ContextMenuStrip = ContextMenuLectura
        Try
            Select Case flag
                Case "Lectura"
                    Me.datalistado.ContextMenuStrip = MenualECT
                Case "Administrar"
                    Me.datalistado.ContextMenuStrip = MenuaDMIN
            End Select
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error en el frmLista_Producto_Load")
        End Try

        Me.txtProduc_Select.Focus()
    End Sub
    Private Sub txtProduc_Select_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtProduc_Select.TextChanged
        SetButtonDefaul(Me.btnBuscar)
    End Sub
    Private Sub SetButtonDefaul(ByVal btn As Button)
        Me.AcceptButton = btn
    End Sub

    Private Sub btnOk_Click(sender As System.Object, e As System.EventArgs)
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
    Private Sub datalistado_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles datalistado.CellDoubleClick
        If IsNumeric(flag) Then
            If flag = 4 Then
                Me.DialogResult = DialogResult.OK
                sql = Me.datalistado.SelectedCells.Item(0).Value
                Me.Close()
            End If
        End If
    End Sub
    Private Sub edirPreciSalesButton_Click(sender As Object, e As EventArgs) Handles edirPreciSalesButton.Click
        Try
            If Not (datalistado.SelectedRows.Count = 1) Then
                MsgBox("Selecccione uno del listado ", MsgBoxStyle.Exclamation, "Importante")
                Return
            End If

            Me.id_Producto = datalistado.SelectedCells.Item(datalistado.Columns("idProducto").Index).Value

            'emviaamos el prodcuto a la tabla temporal
            Using cnn As New SqlConnection(SimpleDataApp.Utility.GetConnectionString())
                cnn.Open()
                sql = "Delete [tmp].[SelectMyProduct]  WHERE ((codUser=@codUser) AND (codTerminal=@codTerminal))"
                Using cmd As New SqlCommand(sql, cnn)
                    cmd.CommandType = CommandType.Text
                    'eLIMINACION
                    cmd.Parameters.AddWithValue("@codUser", UsuarioActivo.codUser)
                    cmd.Parameters.AddWithValue("@codTerminal", TerminalActivo.codTerminal)
                    cmd.ExecuteNonQuery()
                    'agregaremos
                    sql = "Insert [tmp].[SelectMyProduct] (idProducto,codUser,codTerminal) "
                    sql = sql & "Values (@idProducto,@codUser,@codTerminal)"
                    cmd.CommandText = sql
                    cmd.Parameters.AddWithValue("@idProducto", Me.id_Producto)
                    If Not (cmd.ExecuteNonQuery() = 1) Then
                        Return
                    End If
                End Using
            End Using



            Me.Cursor = Cursors.WaitCursor
            Using forAdminPrice As New frmAdministrarPrecios(stateLoad.Dialogo)
                With forAdminPrice
                    .Text = "Administradondo precios"
                    .txtProduc_Select.Text = datalistado.SelectedCells(datalistado.Columns("Nom_Comercial").Index).Value
                    .StartPosition = FormStartPosition.CenterScreen
                    .FormBorderStyle = FormBorderStyle.Fixed3D
                    .WindowState = FormWindowState.Normal
                    .Height = 800
                    .Width = 3000
                    .Text = String.Format("Administrando precios de venta.")
                    .ShowDialog()
                    If .DialogResult = DialogResult.OK Then
                        btnBuscar.PerformClick()
                    End If
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub AgregarButton_Click(sender As Object, e As EventArgs) Handles AgregarButton.Click
        Try

            Cursor = Cursors.WaitCursor
            Using MDI_AddProdcutos
                With MDI_AddProdcutos
                    .id_Proveedor = 0
                    .id_Producto = 0
                    .flag = "Agregar"
                    .ShowDialog()
                    If .DialogResult = DialogResult.OK Then
                        txtProduc_Select.Text = .Nom_Comerial
                        btnBuscar.PerformClick()
                    End If
                End With
            End Using
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub datalistado_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles datalistado.CellClick
        Me.id_Producto = 0
        If Me.datalistado.SelectedRows.Count = 1 Then
            Me.id_Producto = datalistado.SelectedCells.Item(datalistado.Columns("idProducto").Index).Value
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Me.datalistado.SelectedRows.Count = 1 Then
            Me.id_Producto = datalistado.SelectedCells.Item(datalistado.Columns("idProducto").Index).Value
        End If

        If Me.id_Producto > 0 Then
            Try
                Me.Cursor = Cursors.WaitCursor
                Using mwd As New MDI_AddProdcutos()
                    With mwd
                        .flag = "Modificar"
                        .id_Producto = Me.id_Producto
                        .ShowDialog()
                    End With
                End Using
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
            Finally
                Me.Cursor = Cursors.Default
            End Try
        Else
            MsgBox("Seleccione uno del listado", MsgBoxStyle.Exclamation, "Importante")
            Me.datalistado.Focus()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        If Me.id_Producto > 0 Then
            Try
                Using MDI_AddProdcutos
                    With MDI_AddProdcutos
                        .flag = "Lectura"
                        .id_Producto = Me.id_Producto
                        .ShowDialog()
                    End With
                End Using
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            End Try
        Else
            MsgBox("Seleccione uno del listado", MsgBoxStyle.Exclamation, "Importante")
            Me.datalistado.Focus()
        End If
    End Sub

    Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

End Class