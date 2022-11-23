Imports System.Data.SqlClient
Imports CADsisVenta.Funtions
Imports CrystalDecisions.CrystalReports.Engine
Imports CADsisVenta.Helpers.FInicio
Imports CADsisVenta.Statics

Public Class frmReportViewUtilidad

    Dim rpt As ReportDocument
    Dim dsSource As DataTable

    Private isTime As Boolean
    Private dateIni As DateTime
    Private dateFin As DateTime

    Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        Me.rpt = New rptVentaConUtilidad()


        dateIni = DateTime.Now().AddHours(-8)
        dateFin = DateTime.Now()

    End Sub

    Private Sub frmReportViewUtilidad_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Using newfecha As New FrmImputDate
            With newfecha
                If isTime Then
                    .DateIni = dateIni
                    .DateFin = dateFin
                Else
                    .DateIni = CDate(FormatDateTime(dateIni, DateFormat.ShortDate))
                    .DateFin = CDate(FormatDateTime(dateFin, DateFormat.ShortDate))
                End If
                .IsHourCheckBox.Checked = isTime
                .ShowDialog()
                If .DialogResult = System.Windows.Forms.DialogResult.OK Then
                    dateIni = .DateIni
                    dateFin = .DateFin
                    isTime = .IsHourCheckBox.Checked
                    ViewDate(isTime)
                    rptViewer.ReportSource = Nothing
                End If
            End With
        End Using
    End Sub

    Sub ViewDate(isTime As Boolean)
        If isTime Then
            sql = "Desde: " & FormatDateTime(dateIni, DateFormat.ShortDate)
            sql = sql & " " & Format(dateIni, "hh:mm tt") & vbNewLine + vbNewLine
            sql = sql & "Hasta: " & FormatDateTime(dateFin, DateFormat.ShortDate)
            sql = sql & " " & Format(dateFin, "hh:mm tt")
            LinkLabel1.Text = sql
        Else
            sql = "Desde: " & FormatDateTime(dateIni, DateFormat.ShortDate) & vbNewLine + vbNewLine
            sql = sql & "Hasta: " & FormatDateTime(dateFin, DateFormat.ShortDate)
            LinkLabel1.Text = sql
        End If
    End Sub

    Private Sub FindButton_Click(sender As Object, e As EventArgs) Handles FindButton.Click
        Try
            If LinkLabel1.Text.Equals("Opciones de fecha:") Then
                Return
            End If

            Me.Cursor = Cursors.WaitCursor
            rptViewer.Cursor = Cursors.WaitCursor
            Using cmd As New SqlComandExec()
                cmd.CommandType = CommandType.StoredProcedure
                cmd.ParameterCollection = New SqlParameter() {
                    New SqlParameter With {
                        .ParameterName = "@dateStar",
                        .SqlDbType = SqlDbType.DateTime,
                        .Value = dateIni
                    },
                            New SqlParameter With {
                        .ParameterName = "@DateEnd",
                        .SqlDbType = SqlDbType.DateTime,
                        .Value = dateFin
                    }
                }
                dsSource = cmd.RetornaTabla("GetDataVentaUtilidad")
            End Using

            rpt.SetDataSource(dsSource)
            Me.rpt.SetParameterValue(2, If(SettingObject.EcommerceActive.Company, ""))

            rptViewer.ReportSource = rpt

            Me.Cursor = Cursors.Default
            rptViewer.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            rptViewer.Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


End Class