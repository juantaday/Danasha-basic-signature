Public Class frmEditCompra
    Public Enum Estado
        Unitario = 0
        Total = 1
    End Enum
    Protected Friend list As ListView
    Public _flag As Estado
    Private Property Flag As Estado
        Get
            Return _flag
        End Get
        Set(value As Estado)
            _flag = value
        End Set
    End Property

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub Carga_Datos()
        Try
            If IsNothing(Flag) Then
                MsgBox("No se ha determinado el valor para flag ")
                Return
            End If
            If Not IsNothing(list) Then
                If list.Items.Count > 0 Then
                    Dim dt As New DataTable()
                    'columna de [Productos] --------- 0
                    Dim Column1 As New DataColumn()
                    Column1.ColumnName = "Producto"
                    Column1.DataType = GetType(String)
                    'columna de [P/Unitario] ---------1
                    Dim Column2 As New DataColumn()
                    Column2.ColumnName = "P/Unitario"
                    Column2.DataType = GetType(Double)
                    'columna de [P/Total]----------------2
                    Dim Column3 As New DataColumn()
                    Column3.ColumnName = "P/Total"
                    Column3.DataType = GetType(Double)
                    'columna de [IvaPorcent]---------------3
                    Dim Column4 As New DataColumn()
                    Column4.ColumnName = "ivaPorcent"
                    Column4.DataType = GetType(Double)
                    'columna de [desCuento]--------------4
                    Dim Column5 As New DataColumn()
                    Column5.ColumnName = "desCuento"
                    Column5.DataType = GetType(Double)
                    'columna de cantidad------------------5
                    Dim Column6 As New DataColumn()
                    Column6.ColumnName = "cant"
                    Column6.DataType = GetType(Double)
                    'cargamos datos............................................................................
                    dt.Columns.AddRange({Column1, Column2, Column3, Column4, Column5, Column6})
                    For i = 0 To list.Items.Count - 1
                        dt.Rows.Add()
                        dt.Rows(i)("Producto") = list.Items(i).SubItems(1).Text
                        dt.Rows(i)("P/Unitario") = list.Items(i).SubItems(3).Text
                        dt.Rows(i)("P/Total") = list.Items(i).SubItems(6).Text
                        dt.Rows(i)("desCuento") = list.Items(i).SubItems(4).Text
                        dt.Rows(i)("ivaPorcent") = list.Items(i).SubItems(10).Text
                        dt.Rows(i)("cant") = list.Items(i).SubItems(2).Text
                    Next
                    With DataGridView1
                        .DataSource = dt
                        .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
                        For x = 0 To .Columns.Count - 1
                            .Columns(x).ReadOnly = True
                        Next
                        Select Case Flag.value__
                            Case Estado.Unitario
                                .Columns("P/Unitario").ReadOnly = False
                            Case Estado.Total
                                .Columns("P/Total").ReadOnly = False
                                .TabIndex = 0
                                .Columns("P/Total").Selected = True
                        End Select
                        .Columns(3).Visible = False  '---oculto la comuna de porcentaje de iva
                        .Columns(4).Visible = False  '--- oculto la columna de descuento
                        .Columns(5).Visible = False  '--- oculto la columna de cantidad
                        ImpideOrdenamiento(DataGridView1)
                        SumaTotal()
                    End With
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub
    Private Sub frmList_Compra_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Carga_Datos()
    End Sub
    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        Dim IvaPor As Double = 0
        Dim IvaReal As Double = 0
        Dim Ptotals As Double = 0
        Dim pUnit As Double = 0
        Dim cant As Double = 0
        Dim desc As Double = 0
        Dim estado As Boolean = False
        'columna de [Productos] - 0
        'columna de [P/Unitario] --1
        'columna de [P/Total]------2
        'columna de [IvaPorcent]---3
        'columna de [desCuento]----4
        'columna de cantidad-------5
        Try
            With DataGridView1
                If IsDBNull(sender.rows(e.RowIndex).cells(e.ColumnIndex).value) Then
                    .Rows(e.RowIndex).Cells(e.ColumnIndex).Value = 0
                End If
                IvaPor = .Rows(e.RowIndex).Cells(3).Value
                Ptotals = .Rows(e.RowIndex).Cells(2).Value
                pUnit = .Rows(e.RowIndex).Cells(1).Value
                cant = .Rows(e.RowIndex).Cells(5).Value
                desc = .Rows(e.RowIndex).Cells(4).Value

                If .Columns(e.ColumnIndex).Name.Equals("P/Unitario") And Not estado Then
                    estado = True
                    If ivaCheckBox.Checked Then
                        'Cantidad po valor unirario
                        Ptotals = FormatNumber((pUnit * cant), 5)
                        If Not descCheckBox.Checked Then
                            .Rows(e.RowIndex).Cells(2).Value = FormatNumber((Ptotals), 5)
                        Else
                            .Rows(e.RowIndex).Cells(2).Value = FormatNumber((Ptotals - desc), 5)
                            Ptotals -= .Rows(e.RowIndex).Cells(4).Value
                        End If
                    Else
                        Ptotals = FormatNumber((pUnit * cant), 5)
                        If Not descCheckBox.Checked Then
                            .Rows(e.RowIndex).Cells(2).Value = FormatNumber((Ptotals), 5)
                        Else
                            .Rows(e.RowIndex).Cells(2).Value = FormatNumber((Ptotals - desc), 5)
                        End If
                    End If
                ElseIf .Columns(e.ColumnIndex).Name.Equals("P/Total") And Not estado Then
                    estado = True
                End If
            End With
            SumaTotal()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub SumaTotal()
        Dim total As Double = 0
        For i = 0 To DataGridView1.RowCount - 1
            total += DataGridView1.Rows(i).Cells(2).Value
        Next
        TotalTextBox.Text = Convert.ToString(total)
    End Sub


    Private Sub DataGridView1_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DataGridView1.DataError

        If e.Exception.Message.Contains("formato correcto") Then
            MsgBox("La celda acepta solo valores numéricos", MsgBoxStyle.Exclamation, "Error")
        Else
            MsgBox(e.Exception.Message, MsgBoxStyle.Critical, "Error")
        End If
    End Sub

    Private Sub PegarButton_Click(sender As Object, e As EventArgs) Handles PegarButton.Click
        Try
            Dim buffer As New System.Text.StringBuilder

            If Clipboard.ContainsText Then
                sql = Clipboard.GetText()
            End If

            Dim data As String() = Split(sql, vbNewLine)


            Dim columnEdit As String = String.Empty
            Select Case Flag.value__
                Case Estado.Unitario
                    columnEdit = "P/Unitario"
                Case Estado.Total
                    columnEdit = "P/Total"
            End Select
            If String.IsNullOrWhiteSpace(columnEdit) Then
                Return
            End If

            For i = 0 To data.Length - 1
                If i > DataGridView1.RowCount - 1 Then
                    Exit For
                End If
                DataGridView1.Rows(i).Cells(DataGridView1.Columns(columnEdit).Index).Value = data(i)
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
End Class

