Imports CADsisVenta.Funtions
Imports CrystalDecisions.CrystalReports.Engine
Imports CADsisVenta.Helpers.FInicio
Imports CADsisVenta.Statics

Public Class frmSalesWithDiscount
    Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub
    Private Sub frmSalesWithDiscount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ViewReport()
    End Sub

    Private Async Sub ViewReport()
        Me.Cursor = Cursors.WaitCursor

        Try

            Dim lsiPurchAndSale = Await StoreProcedure.GetSalesWithDiscount()

            Dim rpt As New ReportDocument
            rpt.Load(file_rptSalesWithDiscount)
            rpt.SetDataSource(lsiPurchAndSale)
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