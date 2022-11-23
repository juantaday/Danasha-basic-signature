Imports CADsisVenta.Funtions
Imports CrystalDecisions.CrystalReports.Engine
Imports CADsisVenta.Helpers.FInicio
Imports CADsisVenta.Statics

Public Class frmViewVentasGeneral

    Private fromDate As Date
    Private LastDateSales As Date

    Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        fromDate = Date.Now
        LastDateSales = fromDate.AddMonths(-12)
    End Sub


    Private Sub frmViewVentasGeneral_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ViewReport()
    End Sub

    Private Async Sub ViewReport()
        Me.Cursor = Cursors.WaitCursor

        Try

            Dim lsiPurchAndSale = Await StoreProcedure.GetVentaTipoXMes(LastDateSales.Year, LastDateSales.Month,
                                    fromDate.Year, fromDate.Month)

            Dim rpt As New ReportDocument
            rpt.Load(file_rptVentaTipoXMes)
            rpt.SetDataSource(lsiPurchAndSale.OrderByDescending(Function(x) x.DeteArgument))

            rpt.SetParameterValue(0, If(SettingObject.EcommerceActive?.RazonSocial, ""))
            Me.rptViewer.ReportSource = rpt


            Me.Cursor = Cursors.Default

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ViewReport()
    End Sub
End Class