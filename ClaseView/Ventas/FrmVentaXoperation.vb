Imports CADsisVenta

Public Class FrmVentaXoperation
    Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub
    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Try

            If String.IsNullOrEmpty(txtNumOpeartion.Text) Then
                ErrorProvider1.SetError(txtNumOpeartion, "Debe ingresar el número de operación a consultar..")
                Return
            End If

            Dim num_Op As String = txtNumOpeartion.Text.Trim()

            If (Not IsNumeric(num_Op)) Then
                ErrorProvider1.SetError(txtNumOpeartion, "Número de operación inválido..")
                Return
            End If

            ErrorProvider1.SetError(txtNumOpeartion, String.Empty)

            Dim _operation As Integer = 0
            Integer.TryParse(num_Op, _operation)

            btnBuscar.Enabled = False

            Me.Cursor = Cursors.WaitCursor

            Task.Run(Async Function()
                         Try

                             Dim data = Await Funtions.StoreProcedure.GetSalesWithOperation(_operation)
                             If (data IsNot Nothing AndAlso data.Rows.Count > 0) Then
                                 olvVentas.SetObjects(data.AsEnumerable())
                             Else
                                 olvVentas.ClearObjects()
                                 olvVentas.EmptyListMsg = "No hay información con esta operación" & vbNewLine & $"Num. operación {_operation}"
                             End If

                         Catch ex As Exception
                             olvVentas.EmptyListMsg = ex.Message & vbLf & ex.StackTrace
                         Finally
                             Me.Invoke(New MethodInvoker(Sub()
                                                             Me.Cursor = Cursors.Default
                                                         End Sub))

                             Me.btnBuscar.Invoke(New MethodInvoker(Sub()
                                                                       Me.btnBuscar.Enabled = True
                                                                   End Sub))
                             SumTotal()
                         End Try

                     End Function)

        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Async Sub SumTotal()
        Await Task.Run(Sub()
                           Dim total As Decimal = 0
                           Dim message As String = String.Empty
                           Try

                               If olvVentas.GetItemCount > 0 Then
                                   total = olvVentas.Objects().AsQueryable().
                                              Cast(Of DataRow).
                                              Sum(Function(x) x.Field(Of Decimal)("Total"))
                               End If
                           Catch ex As Exception
                               message = ex.Message & vbLf & ex.StackTrace
                           End Try

                           lblTotal.Invoke(New MethodInvoker(Sub()
                                                                 If (Not String.IsNullOrEmpty(message)) Then
                                                                     lblTotal.Text = message
                                                                 Else
                                                                     lblTotal.Text = $"Total General: {total.ToString("C2")}"
                                                                 End If

                                                             End Sub))


                       End Sub)

    End Sub

    Private Sub txtNumOpeartion_Leave(sender As Object, e As EventArgs) Handles txtNumOpeartion.Leave
        Me.AcceptButton = Nothing
    End Sub

    Private Sub txtNumOpeartion_Enter(sender As Object, e As EventArgs) Handles txtNumOpeartion.Enter
        Me.AcceptButton = btnBuscar
    End Sub

    Private Sub FrmVentaXoperation_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub CopyClickBoarButton_Click(sender As Object, e As EventArgs) Handles CopyClickBoarButton.Click
        Me.olvVentas.CopySelectionToClipboard()
    End Sub
End Class