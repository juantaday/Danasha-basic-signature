Imports System.Data.SqlClient
Imports CADsisVenta.Data.Emuns.EnumSatateModule
Imports CADsisVenta.DataSetMonedas
Imports CADsisVenta.DataSetMonedasTableAdapters
Imports ClaseView.FrmMonedas

Public Class frmCajaDetail
    Protected Friend idCajaStado As Integer
    Private totalSaldoEfectivo As Double
    Private totalArqueoEfectivo As Double
    Protected Friend totalDifereniaEfectivo As Double
    Protected Friend totalDiferenciaCaheque As Double
    Protected Friend totalTargeta As Double
    Protected Friend totalDiferenciaGeneral As Double
    Protected Friend totalSaldoSistema As Double
    Sub New(idCajaStado As Integer)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()
        Me.idCajaStado = idCajaStado
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        totalDifereniaEfectivo = 0
        totalDiferenciaCaheque = 0
        totalDiferenciaGeneral = 0
        totalSaldoSistema = 0
    End Sub
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Try
            Dim codUserClose As String = String.Empty
            Using forlogin As New LoginForm(stateReturn._response, "cajas")
                With forlogin
                    .StartPosition = FormStartPosition.CenterParent
                    .ShowDialog()
                    If (.DialogResult = DialogResult.OK) Then
                        If closeTerminal(.UsernameTextBox.Text) Then
                            MsgBox("Cierre de caja efectuado correctament", MsgBoxStyle.Information, "Aviso")
                            If LoadOptionsPrint(0, "Reporte de cierre de caja") Then
                                sql = "Desea imprimir el Reporte de cierre de caja" & vbNewLine
                                sql = sql & "En impresora " & myOptnsPrint.typePrint & " " & myOptnsPrint.NamePrint
                                If (MsgBox(sql, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Responda")) = MsgBoxResult.Yes Then
                                    PrintArqueoTerminal(Me.idCajaStado, myOptnsPrint)
                                End If
                            End If
                            Me.DialogResult = DialogResult.OK
                            Me.Close()
                        End If
                    End If
                End With

            End Using
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub
    Private Function closeTerminal(codUserClose As String) As Boolean
        Try
            Using cnn As New SqlConnection(SimpleDataApp.Utility.GetConnectionString())
                cnn.FireInfoMessageEventOnUserErrors = True
                AddHandler cnn.InfoMessage, New SqlInfoMessageEventHandler(AddressOf OnInfoMessage)

                cnn.Open()
                Using cmd As New SqlCommand("prcCloseTerminal", cnn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@codUserClose", codUserClose)
                    cmd.Parameters.AddWithValue("@idCajaStado", Me.idCajaStado)
                    cmd.Parameters.AddWithValue("@QuantityClosed", Me.totalArqueoEfectivo)
                    cmd.Parameters.AddWithValue("@Qntt_difference", Me.totalDiferenciaGeneral)
                    Dim resul = cmd.ExecuteNonQuery()

                    If resul >= 1 Then
                        Return True
                    Else
                        Return False
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
            Return False
        End Try

    End Function
    Private Sub OnInfoMessage(sender As Object, args As SqlInfoMessageEventArgs)
        Dim Err_code = args.Errors.Item(0).Number
        Dim err As SqlError
        For Each err In args.Errors
            MsgBox(err.Message, MsgBoxStyle.Exclamation, "Error")
        Next
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Anular_Button_Click(sender As Object, e As EventArgs)
        Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmCajaDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Carga_ListDatos()
        Me.Carga_SaldoSytema()
        SplitContainer1.Panel2.Controls.Clear()
        Me.TitleLabel.Text = "Arqueo del terminal: " & Me.MyTerminalDescrip(Me.idCajaStado) & " Registra un saldo de: " & totalSaldoSistema.ToString("C2")
    End Sub
    Private Sub Carga_SaldoSytema()
        Try

            Using cmd As New SaldoCajaTableAdapter
                Using dt As New SaldoCajaDataTable
                    cmd.Fill(dt, Me.idCajaStado)
                    For Each rowSaldo As DataRow In dt.Rows
                        Me.totalSaldoSistema += (rowSaldo("SaldoInicial") + rowSaldo("Debe")) - rowSaldo("Haber")
                    Next
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Function MyTerminalDescrip(idCajaStado As Integer) As String
        Try
            Using cmd As New CajaDescripTableAdapter
                Using dt As New CajaDescripDataTable
                    cmd.Fill(dt, Me.idCajaStado)
                    If dt.Rows.Count > 0 Then
                        userManupuleButton.Tag = dt.Rows(0)("idTerminal").ToString
                        Me.TitleLabel.Tag = dt.Rows(0)("idTerminal").ToString
                        Return dt.Rows(0)("codTerminal").ToString
                    Else
                        Return String.Empty
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return String.Empty
        End Try
    End Function

    Private Sub Carga_ListDatos()
        Try
            ListViewList.View = View.Details
            ListViewList.Columns.Add("Arquear", 300)
            ListViewList.GridLines = True
            ListViewList.FullRowSelect = True
            sql = "select * from stm.FormaPago  where Not (FormaPago = 'Crédito')"

            Using cmd As New ClassCargadorProducto()
                Dim dt As DataTable = cmd.RetornaTabla(sql)
                For i = 0 To dt.Rows.Count - 1
                    Dim item As New ListViewItem
                    item.Text = dt.Rows(i)("formaPago")
                    item.SubItems.Add(dt.Rows(i)("idformaPago"))
                    item.SubItems.Add(dt.Rows(i)("formaPago"))
                    ListViewList.Items.Add(item)
                Next

            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ListViewList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListViewList.SelectedIndexChanged
        If ListViewList.SelectedItems.Count > 0 Then
            Dim response As ListViewItem = ListViewList.SelectedItems(0)
            Select Case response.SubItems(2).Text
                Case "Efectivo"
                    Carga_Efectivo(Integer.Parse(response.SubItems(1).Text))
                Case "Cheque"
                    Carga_Cheche(Integer.Parse(response.SubItems(1).Text))
                Case "Tarjeta Crédito"
                    Carga_Tarjeta(Integer.Parse(response.SubItems(1).Text))
            End Select
        End If
    End Sub
    Private Sub Carga_Efectivo(id As Integer)
        Try
            sql = "SELECT "
            sql = sql & "(sum(Debe)- sum(Haber)) as saldo "
            sql = sql & "FROM Cajas "
            sql = sql & "where idCajaStado =" & idCajaStado & " And idFormaPago = " & id & " "
            Dim cmd As New ClassCargadorProducto()
            Dim dt As DataTable = cmd.RetornaTabla(sql)
            If dt.Rows.Count > 0 Then
                SplitContainer1.Panel2.Controls.Clear()
                Dim myPanelEfective As New System.Windows.Forms.Panel
                myPanelEfective = PanelEfectico
                myPanelEfective.Dock = DockStyle.Fill
                SplitContainer1.Panel2.Controls.Add(myPanelEfective)
                totalSaldoEfectivo = dt.Rows(0)("saldo")
                SaldoEfectivoTextBox.Text = totalSaldoEfectivo.ToString("C2")
            End If
            EfectivoButton.Tag = id
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub Carga_Cheche(p1 As Integer)
        Try
            Dim forms As New frmChequeContab()
            Dim palecheche As New System.Windows.Forms.Panel With {.Dock = DockStyle.Fill}
            SplitContainer1.Panel2.Controls.Clear()
            If forms.Load_Cheque(idCajaStado) Then
                palecheche = forms.PanelData
                palecheche.Parent = Me
                For Each control In palecheche.Controls
                    If control.Name.ToString.Equals("PanelPie") Then
                        For Each control2 In control.Controls
                            If control2.Name.ToString.Equals("ChequeContabButton") Then
                                control2.tag = p1
                            End If
                        Next
                    End If
                Next
                SplitContainer1.Panel2.Controls.Add(palecheche)
            End If
            forms = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub Carga_Tarjeta(p1 As Integer)
        Try
            Dim forms As New frmTargetContab()
            Dim palecheche As New System.Windows.Forms.Panel With {.Dock = DockStyle.Fill}
            SplitContainer1.Panel2.Controls.Clear()
            If forms.Load_Cheque(idCajaStado) Then
                palecheche = forms.PanelTarget
                SplitContainer1.Panel2.Controls.Add(palecheche)
            End If
            Ok_List(p1, "0.00")
            forms = Nothing
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub EfectivoButton_Click(sender As Object, e As EventArgs) Handles EfectivoButton.Click

        ListViewList.AutoResizeColumns(
        ColumnHeaderAutoResizeStyle.HeaderSize)

        ' Resize the second column to the column content.
        If IsNumeric(sender.tag) Then
            If String.IsNullOrEmpty(DiferenciaTextBox.Text) Then
                MsgBox("Determine la cantidad de efectivo encotrado en la caja.", MsgBoxStyle.Exclamation, "Importante")
                Return
            End If
            Ok_List(sender.tag, DiferenciaTextBox.Text)
        End If
    End Sub
    Private Sub Ok_List(id As Integer, stotal As String)
        Try
            For i = 0 To ListViewList.Items.Count - 1
                If Integer.Parse(ListViewList.Items(i).SubItems(1).Text) = id Then
                    ListViewList.Items(i).ImageKey = "ok_16_png"
                    ListViewList.Items(i).Text = String.Format("{0} ({1})", ListViewList.Items(i).SubItems(2).Text, stotal)
                    ListViewList.Refresh()
                End If
            Next
            Dim isArquedototo As Boolean = True
            For i = 0 To ListViewList.Items.Count - 1
                If String.IsNullOrEmpty(ListViewList.Items(i).ImageKey.ToString()) Then
                    isArquedototo = False
                End If
            Next
            Me.totalDiferenciaGeneral = Math.Round(Me.totalDiferenciaCaheque + Me.totalDifereniaEfectivo + Me.totalTargeta, 2)
            Me.OK_Button.Enabled = isArquedototo
            Me.totalDiferenciaArqueoLabel.Text = String.Format("Diferencia General: {0:C2}", Me.totalDiferenciaGeneral)

        Catch ex As Exception

        End Try
    End Sub
    Private Sub ContarButton_Click(sender As Object, e As EventArgs) Handles ContarButton.Click
        Using frmmoney As New frmMonedasAdmin
            With frmmoney
                .idCajaStada = idCajaStado
                .callOpen = frmMonedasAdmin.callShow.arqueoTerminal
                .ShowDialog()
                If .DialogResult = DialogResult.OK Then
                    totalArqueoEfectivo = .total
                    totalDifereniaEfectivo = totalArqueoEfectivo - totalSaldoEfectivo
                    ArqueroTextBox.Text = FormatCurrency(totalArqueoEfectivo, 2)
                    DiferenciaTextBox.Text = totalDifereniaEfectivo.ToString("C2")
                End If
            End With
        End Using
    End Sub

    Private Sub userManupuleButton_Click(sender As Object, e As EventArgs) Handles userManupuleButton.Click
        Try
            Using cmd As New UserMoviInTerminalTableAdapter
                Using dt As New UserMoviInTerminalDataTable
                    cmd.Fill(dt, Me.idCajaStado)
                    If dt.Rows.Count > 0 Then
                        DeleteUserLisControl()
                        Dim panelList As New System.Windows.Forms.Panel _
                        With {.Dock = DockStyle.Right,
                             .Name = "panelListUser",
                             .BorderStyle = BorderStyle.FixedSingle,
                             .Width = 300}
                        Dim dtg As New System.Windows.Forms.DataGridView _
                             With {.Dock = DockStyle.Fill,
                              .DataSource = dt}

                        Dim closeButton As New System.Windows.Forms.Button _
                            With {.Dock = DockStyle.Top,
                              .Text = "Cerrar",
                              .TextAlign = ContentAlignment.MiddleCenter,
                              .Image = My.Resources.Arrow_Forward_48,
                              .ImageAlign = ContentAlignment.MiddleRight,
                              .Height = 45
                                }
                        AddHandler closeButton.Click, AddressOf loseButton_Click

                        applyGridTheme(dtg)
                        panelList.Controls.Add(dtg)
                        panelList.Controls.Add(closeButton)
                        Me.SplitContainer1.Panel2.Controls.Add(panelList)
                    Else
                        MsgBox("No se registra Usuarios en el idStadoCaja: " & sender.tag, MsgBoxStyle.Exclamation, "Aviso")
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub loseButton_Click(sender As Object, e As EventArgs)
        DeleteUserLisControl()
    End Sub
    Private Sub DeleteUserLisControl()
        Try
            For Each control In Me.SplitContainer1.Panel2.Controls
                If control.name.Equals("panelListUser") Then
                    Me.SplitContainer1.Panel2.Controls.Remove(control)
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
End Class
