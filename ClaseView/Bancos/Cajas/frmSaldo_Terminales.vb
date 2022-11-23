Imports CADsisVenta.DataSetMonedasTableAdapters
Imports CADsisVenta.DataSetSystemTableAdapters

Public Class frmSaldo_Terminales
    Private saldoTotal As Double
    Private miPadre As MDIcajas
    Private IsLoad As Boolean
    Public Sub New(ByVal mipadre As MDIcajas)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        Me.miPadre = mipadre
    End Sub
    Private Sub frmSaldo_Terminales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar_Datos()
        IsLoad = True
    End Sub
    Private Sub Cargar_Datos()
        Try
            saldoTotal = 0
            Dim dat As New prcSaldoTerminalesTableAdapter
            Dim dt As New DataTable
            dt = dat.GetData()
            If dt.Rows.Count > 0 Then

                For i = 0 To dt.Rows.Count - 1
                    saldoTotal += dt.Rows(i)("Saldo")
                Next
                dtg.DataSource = dt

                applyGridTheme(dtg)

                clm = dtg.Columns("CodTerminal")
                clm.HeaderText = "Terminal"

                clm = dtg.Columns("DateStar")
                clm.HeaderText = "Fecha de apertura"

                clm = dtg.Columns("dateHiberna")
                clm.HeaderText = "Fecha suspendida" 'QuantityOpen


                clm = dtg.Columns("QuantityOpen")
                clm.HeaderText = "Saldo inicial" '
                clm.DefaultCellStyle = myStileMoney


                clm = dtg.Columns("Ingresos")
                clm.HeaderText = "Ingresó"
                clm.DefaultCellStyle = myStileMoney

                clm = dtg.Columns("Saldo")
                clm.DefaultCellStyle = myStileMoney

            End If

            TotalSaldoLabel.Text = "Saldo en terminales: " & saldoTotal.ToString("C2")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ArqueoDeCajaToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Try
            Dim id As Nullable(Of Integer) = dtg.SelectedRows(0).Cells(dtg.Columns("idCajaStado").Index).Value

            Using newform As New frmCajaDetail(id)
                With newform
                    .ShowDialog()
                    If .DialogResult = DialogResult.OK Then
                        Cargar_Datos()
                    End If
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message + "\n" + ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub HibernarToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Try
            If Not dtg.SelectedRows.Count = 1 Then
                MsgBox("Seleccione uno de la lista. ", MsgBoxStyle.Exclamation, "Importante")
                Return
            End If


            Dim id As Nullable(Of Integer) = dtg.SelectedRows(0).Cells(dtg.Columns("idCajaStado").Index).Value
            If Not id = 0 Then
                sql = "Está seguro de hibernar (suspender)," + vbNewLine
                sql = sql & "El terminal: "
                sql = sql & dtg.SelectedRows(0).Cells(dtg.Columns("codTerminal").Index).Value
                sql = sql & ", Operación: "
                sql = sql & id
                If (MsgBox(sql, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Responda")) = MsgBoxResult.Yes Then
                    Dim tap As New CajaStadoTableAdapter
                    If Not tap.UpdateCajaStado_State(id) = 0 Then
                        Cargar_Datos()
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub EliminarToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Try
            Dim id As Nullable(Of Integer) = dtg.SelectedRows(0).Cells(dtg.Columns("idCajaStado").Index).Value

            sql = "Está seguro de eliminar la Operación: " & id & vbNewLine
            sql = sql & "Del terminal: " & dtg.SelectedRows(0).Cells(dtg.Columns("codTerminal").Index).Value
            If (MsgBox(sql & vbNewLine,
                MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Responda")) = MsgBoxResult.Yes Then
                If Not id = 0 Then
                    Dim tap As New CajaStadoTableAdapter
                    If Not tap.DeleteCajaStado(id) = 0 Then
                        Cargar_Datos()
                    End If
                End If
            End If
        Catch ex As Exception
            If ex.Message.Contains("REFERENCE") Then
                sql = "Esta operación ya tiene movimientos." & vbNewLine
                sql = sql & "Imposible eliminar.."
                MsgBox(sql, MsgBoxStyle.Critical, "Error")
            Else
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            End If

        End Try
    End Sub

    Private Sub AbrirCajaToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Try
            Dim idCajaStado As Nullable(Of Integer) = dtg.SelectedRows(0).Cells(dtg.Columns("idCajaStado").Index).Value

            Dim frmCaja_stado As New FrmSaldo_caja(idCajaStado)

            With frmCaja_stado
                .MdiParent = Me.miPadre
                .codTerminal = dtg.SelectedRows(0).Cells(dtg.Columns("codTerminal").Index).Value
                .WindowState = FormWindowState.Maximized
                .Show()
            End With

        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error")
        End Try
    End Sub

    Private Sub MivimientosToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Try
            If Not dtg.SelectedRows.Count = 1 Then
                MsgBox(msgSelect_list, MsgBoxStyle.Exclamation, "Aviso")
                Return
            End If
            Dim idTerminal As Integer? = dtg.SelectedRows(0).Cells("idTerminal").Value


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try


    End Sub
End Class