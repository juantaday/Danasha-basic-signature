Imports System.Data.SqlClient
Public Class frmListPedido

    Private Sub frmListPedido_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Carga_ListPedido()
    End Sub



    Private Sub Carga_ListPedido()

        sql = "SELECT dbo.Pedidos.idPedido AS Orden, dbo.Pedidos.fechaPedido, dbo.Proveedores.Razon_social AS Proveedor, dbo.Pedidos.base00Iva, dbo.Pedidos.base12Iva, "
        sql = sql & "dbo.Pedidos.iva, dbo.Pedidos.TotalPedido , dbo.Pedidos.idBodega as Bodega "
        sql = sql & "FROM   dbo.Pedidos INNER JOIN "
        sql = sql & "dbo.Proveedores ON dbo.Pedidos.idProveedor = dbo.Proveedores.idProveedor "


        Try

            Using cnn = New SqlConnection(DomainSQLite.Setting.Configuration.ConectionString)
                cnn.Open()

                Dim cmd As New SqlCommand(sql)
                cmd.CommandType = CommandType.Text
                cmd.Connection = cnn

                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable

                da.Fill(dt)

                If dt.Rows.Count > 0 Then
                    datalistado.DataSource = dt
                    datalistado.AutoSizeColumnsMode =
                                     DataGridViewAutoSizeColumnsMode.AllCells
                Else
                    datalistado.DataSource = Nothing
                End If

                dt = Nothing
            End Using


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error al cargar Listado")
            datalistado.DataSource = Nothing
        Finally

        End Try

    End Sub
    Private Sub btnEliminar_Click(sender As System.Object, e As System.EventArgs) Handles btnEliminar.Click
        If Len(datalistado.SelectedRows.Count) = 0 Then
            MsgBox("Seleccione uno de la lista", MsgBoxStyle.Information, "Aviso")
            Return
        End If
        If (MsgBox("Está seguro de eliminar el pedido", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Responda..")) = DialogResult.Yes Then
            Dim RowSelect As DataGridViewSelectedRowCollection = datalistado.SelectedRows
            Dim Rows As DataGridViewRow
            For Each Rows In RowSelect
                ELimina_Pedido(Rows.Cells(0).Value)
            Next
            Carga_ListPedido()
        End If
    End Sub


    Private Sub btnGuardar_Click(sender As System.Object, e As System.EventArgs) Handles ComprarButton.Click
        If Me.datalistado.SelectedRows.Count = 1 Then
            With frmAdquisicion
                .txtOrden.Text = Me.datalistado.SelectedCells.Item(0).Value
                .TotalPediText.Text = datalistado.SelectedCells.Item(6).Value
                .dtFechaPedido.Value = Me.datalistado.SelectedCells.Item(1).Value
                .dtFechaCompra.Value = Me.datalistado.SelectedCells.Item(1).Value
                .txtFalg.Text = 2
                .Carga_Tipo_Consumo()
                .Carga_Declaracion()

                .Width = 500
                .Height = 500
                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
            End With
            frmAdquisicion = Nothing
            Carga_ListPedido()
        Else
            MsgBox("No existe informacion en el listado", MsgBoxStyle.Information, "Aviso")
        End If
    End Sub


    Private Sub btnCancelar_Click(sender As System.Object, e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub

    Private Sub SelectAllButton_Click(sender As Object, e As EventArgs) Handles SelectAllButton.Click
        cmdSelectAll(datalistado)
    End Sub
End Class