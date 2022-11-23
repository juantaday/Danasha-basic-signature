Imports CADsisVenta.DataSetMonedasTableAdapters
Imports CADsisVenta.Helpers.FInicio
Imports DanashaBasic.ClassView.Conexion

Public Class frmClosedTerminales
    Private isLoated As Boolean
    Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub
    Private Sub frmClosedTerminales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cargar_Datos()
        isLoated = True
    End Sub
    Private Sub Cargar_Datos()
        Try
            Dim dat As New prcClosedTerminalesTableAdapter
            Dim dt As New DataTable
            dt = dat.GetData()
            If dt.Rows.Count > 0 Then

                dtg.DataSource = dt

                applyGridTheme(dtg)

                clm = dtg.Columns("codTerminal")
                clm.HeaderText = "Terminal"

                clm = dtg.Columns("idCajaStado")
                clm.HeaderText = "N. Operación"

                clm = dtg.Columns("codUserOpen")
                clm.HeaderText = "Aperturó"

                clm = dtg.Columns("codUserClosed")
                clm.HeaderText = "Realizo el arqueo"


                clm = dtg.Columns("DateStar")
                clm.HeaderText = "Fecha de apertura"

                clm = dtg.Columns("DateEnd")
                clm.HeaderText = "Fecha de cierre" '

                clm = dtg.Columns("Qntt_difference")
                clm.HeaderText = "Direfencia" '
                clm.DefaultCellStyle = myStileMoney

            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub NewRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles NewRadioButton.CheckedChanged
        If NewRadioButton.Checked Then
            If isLoated Then
                Load_Data(Date.Now.AddDays(0), Date.Now.AddDays(0))
            End If
        End If
    End Sub

    Private Sub YesterdayRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles YesterdayRadioButton.CheckedChanged
        If YesterdayRadioButton.Checked Then
            If isLoated Then
                Load_Data(Date.Now.AddDays(-1), Date.Now.AddDays(-1))
            End If
        End If
    End Sub

    Private Sub BeforeDayRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles BeforeDayRadioButton.CheckedChanged
        If BeforeDayRadioButton.Checked Then
            If isLoated Then
                Load_Data(Date.Now.AddDays(-2), Date.Now.AddDays(-2))
            End If
        End If
    End Sub

    Private Sub ByDateRadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles ByDateRadioButton.CheckedChanged
        DateTimePicker1.Visible = ByDateRadioButton.Checked
    End Sub
    Private Sub Load_Data(ByVal dateStar As Date, ByVal dateEnd As Date)
        Try

            dtg.DataSource = Nothing
            Using db As New DataContext
                dtg.DataSource = db.getClosedTerminal(dateStar, dateEnd, TerminalActivo.idBodega)
                dtg.Columns("idCajaStado").Visible = False
            End Using
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub PrintTicketButton_Click(sender As Object, e As EventArgs) Handles PrintTicketButton.Click
        Try
            If dtg.SelectedRows.Count = 1 Then
                Dim idCajaStado As Integer = dtg.SelectedRows(0).Cells("idCajaStado").Value
                If LoadOptionsPrint("Reporte de cierre de caja") Then
                    sql = "Desea imprimir el Reporte de cierre de caja" & vbNewLine
                    sql = sql & "En impresora " & myOptnsPrint.typePrint & " " & myOptnsPrint.NamePrint
                    If (MsgBox(sql, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Responda")) = MsgBoxResult.Yes Then
                        PrintArqueoTerminal(idCajaStado, myOptnsPrint)
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
End Class