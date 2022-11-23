Imports CADsisVenta.Helpers.FInicio
Imports CADsisVenta.Data.Entyties
Imports CADsisVenta.Funtions
Imports BrightIdeasSoftware.Implementation
Imports BrightIdeasSoftware
Imports SpreadsheetLight
Imports System.IO
Imports CADsisVenta.DataSetVentasTableAdapters
Imports CADsisVenta.DataSetVentas
Imports DocumentFormat.OpenXml.Spreadsheet
Imports System.Data.SqlClient
Imports DocumentFormat.OpenXml
Imports JMControls.Implementation

Public Class frmInventario
    Private typoData As Byte
    Private typoArchivo As String
    Private DowloadFile As String
    Private dt As DataTable
    Private ReadOnly IdBodega As Integer
    Private ListData As ObservableCollectionEx(Of ItemStockViewModel)

    Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

        IdBodega = TerminalActivo.idBodega

        ListData = New ObservableCollectionEx(Of ItemStockViewModel)
        Label1.Text = String.Empty
        GetFileDowload()
    End Sub
    Private Sub frmInventario_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If IdBodega = 0 Then
            MsgBox("Este equipo no esta registrado en nigun local o bodega no se puede admisntrar..")
            Me.Close()
        End If

        SelectAllButton.PerformClick()
    End Sub

    Private Async Sub GetFileDowload()
        Await Task.Factory.StartNew(Sub()
                                        Me.DowloadFile = System.Convert.ToString(Microsoft.Win32.Registry.GetValue($"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "{374DE290-123F-4565-9164-39C4925E467B}", String.Empty))
                                    End Sub)
    End Sub


    Private Async Sub GetResum()
        Me.Cursor = Cursors.WaitCursor

        typoData = 1

        dt = Nothing


        Me.Label2.Text = "Stock de productos: resumen por categoría"

        sql = $"select tb1.idCategoria,  tb1.idSubCategoria, tb1.Nom_Categoria, tb1.Nom_SubCategoria, 
                count(tb1.idProducto) as Articulos, sum(tb1.CostoTotal) as CostoTotal
                from (
                select ca.idCategoria, ca.Nom_Categoria, sub.idSubCategoria, sub.Nom_SubCategoria,
                p.idProducto, s.stock * s.pvpUND as CostoTotal, p.Nom_Comercial
                from productos as p 
                inner join ProductosStock as s on s.idProducto  = p.idProducto
                inner join ProductoSubCategoria as sub  on sub.idSubCategoria = p.IdSubCategoria
                inner join ProductoCategoria as ca on ca.idCategoria  = sub.idCategoria
                where s.idBodega = @idBodega) as tb1
                group by tb1.idCategoria,  tb1.idSubCategoria, tb1.Nom_Categoria, tb1.Nom_SubCategoria"

        Using cmd As New SqlComandExec()

            cmd.ParameterCollection = New SqlParameter() {
                New SqlParameter With {
                        .ParameterName = "@idBodega",
                        .SqlDbType = SqlDbType.Int,
                        .Value = Me.IdBodega
                    }
            }

            dt = Await cmd.RetornaTablaAsync(sql)

        End Using
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each cls As BrightIdeasSoftware.OLVColumn In ObjectListView1.Columns
                cls.IsVisible = False
                cls.AspectName = String.Empty
                cls.Sortable = True
                cls.Width = 0
            Next

            ObjectListView1.Sort(Nom_CategoriaClm, SortOrder.Descending)
            ObjectListView1.ShowGroups = True

            Nom_CategoriaClm.IsVisible = False
            Nom_CategoriaClm.AspectName = "Nom_Categoria"
            Nom_CategoriaClm.Width = 0

            Nom_SubCategoriaClm.IsVisible = True
            Nom_SubCategoriaClm.AspectName = "Nom_SubCategoria"
            Nom_SubCategoriaClm.Sortable = False
            Nom_SubCategoriaClm.Width = 200

            Articulosclm.IsVisible = True
            Articulosclm.AspectName = "Articulos"
            Articulosclm.Sortable = False
            Articulosclm.Width = 200

            CostoTotalClm.IsVisible = True
            CostoTotalClm.Width = 200
            CostoTotalClm.AspectName = "CostoTotal"
            CostoTotalClm.Sortable = False
            CostoTotalClm.AspectToStringFormat = "{0:C2}"


            Me.ObjectListView1.SetObjects(dt.AsEnumerable())
            Dim total = dt.AsEnumerable().Sum(Function(x) x.Field(Of Decimal)("CostoTotal"))

            Label1.Text = "Total costo en productos: " & total.ToString("C2")
        Else
            dt = Nothing
            Me.ObjectListView1.ClearObjects()
        End If

        Me.Cursor = Cursors.Default


    End Sub

    Private Sub SelectAllButton_Click(sender As Object, e As EventArgs) Handles SelectAllButton.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            typoArchivo = "Resumen"
            GetResum()
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ObjectListView1_CellClick(sender As Object, e As CellClickEventArgs) Handles ObjectListView1.CellClick
        If e.ClickCount = 1 AndAlso e.RowIndex >= 0 AndAlso Me.typoData = 3 Then
            Me.EditCountStockButton.Enabled = True
            Me.EditPvPStockButton.Enabled = True
        Else
            Me.EditCountStockButton.Enabled = False
            Me.EditPvPStockButton.Enabled = False
        End If

    End Sub

    Private Sub CategoryButton_Click(sender As Object, e As EventArgs) Handles CategoryButton.Click

        Try
            Me.Cursor = Cursors.WaitCursor

            Using formCategories As New frm_Categoria(Nothing)

                formCategories.StartPosition = FormStartPosition.CenterScreen
                formCategories.ShowDialog()
                If formCategories.DialogResult = DialogResult.OK Then
                    Dim id As Integer = formCategories.SelectedNode.Tag
                    If formCategories.isSubCategory Then
                        Me.Cursor = Cursors.WaitCursor
                        typoArchivo = "SubCategoria"
                        GetDataWithIdSubCate(id, formCategories.SelectedNode.Text)
                    Else
                        Me.Cursor = Cursors.WaitCursor
                        typoArchivo = "Categoria"
                        GetDataWithIdCateg(id, formCategories.SelectedNode.Text)
                    End If
                End If

            End Using
            Me.Cursor = Cursors.Default

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub


    Private Async Sub GetDataWithIdSubCate(idSubCatego As Integer, nameSubCategory As String)

        Me.Label2.Text = $"Stock de productos: detalle de {nameSubCategory}"

        typoData = 3

        sql = $"select top(1000) b.Nom_Bodega, p.idProducto,p.Nom_Comercial, s.Stock ,s.pvpUND as Costo ,  
                (s.Stock * s.pvpUND) as  CostoTotal , s.idProdcutStock
                from Productos as p
                inner join ProductosStock as s on s.idProducto  = p.idProducto
                inner join Bodegas  as b on b.idBodega  = s.idBodega
                where p.IdSubCategoria = {idSubCatego} and s.idBodega ={Me.IdBodega}"
        Using cmd As New SqlComandExec()
            dt = Await cmd.RetornaTablaAsync(sql)

        End Using
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each cls As BrightIdeasSoftware.OLVColumn In ObjectListView1.Columns
                cls.IsVisible = False
                cls.AspectName = String.Empty
                cls.Sortable = True
                cls.Width = 0
            Next


            ObjectListView1.Sort(Nom_ComercialClm, SortOrder.Descending)
            ObjectListView1.ShowGroups = False

            idProductoClm.IsVisible = True
            idProductoClm.Sortable = True
            idProductoClm.AspectName = "idProducto"
            idProductoClm.Width = 100


            Nom_BodegaClm.IsVisible = True
            Nom_BodegaClm.AspectName = "Nom_Bodega"
            Nom_BodegaClm.Width = 200

            Nom_ComercialClm.IsVisible = False
            Nom_ComercialClm.AspectName = "Nom_Comercial"
            Nom_ComercialClm.Width = 250

            StockCm.IsVisible = True
            StockCm.AspectName = "Stock"
            StockCm.Sortable = False
            StockCm.Width = 120


            CostoClm.IsVisible = True
            CostoClm.AspectName = "Costo"
            CostoClm.Sortable = False
            CostoClm.Width = 150


            CostoTotalClm.IsVisible = True
            CostoTotalClm.Width = 200
            CostoTotalClm.AspectName = "CostoTotal"
            CostoTotalClm.Sortable = False

            Me.ObjectListView1.SetObjects(dt.AsEnumerable())
            Dim total = dt.AsEnumerable().Sum(Function(x) x.Field(Of Decimal)("CostoTotal"))

            Label1.Text = "Total costo en productos: " & total.ToString("C2")
        Else
            dt = Nothing
            Me.ObjectListView1.ClearObjects()
        End If

        Me.Cursor = Cursors.Default

    End Sub

    Private Async Sub GetDataWithIdCateg(idSubCatego As Integer, nameSubCategory As String)

        typoData = 2

        Me.Label2.Text = $"Stock de productos: detalle de {nameSubCategory}"

        sql = $"select top(1000)  sca.Nom_SubCategoria, p.idProducto,p.Nom_Comercial, s.Stock ,s.pvpUND as Costo,  
                (s.Stock * s.pvpUND) as  CostoTotal 
                from Productos as p
                inner join ProductosStock as s on s.idProducto  = p.idProducto
                inner join ProductoSubCategoria as sca on sca.idSubCategoria =p.IdSubCategoria
                where sca.idCategoria = {idSubCatego} and s.idBodega = {Me.IdBodega}"
        Using cmd As New SqlComandExec()
            dt = Await cmd.RetornaTablaAsync(sql)

        End Using
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each cls As BrightIdeasSoftware.OLVColumn In ObjectListView1.Columns
                cls.IsVisible = False
                cls.AspectName = String.Empty
                cls.Sortable = True
                cls.Width = 0
            Next


            ObjectListView1.Sort(Nom_SubCategoriaClm, SortOrder.Descending)
            ObjectListView1.ShowGroups = True

            Nom_SubCategoriaClm.IsVisible = True
            Nom_SubCategoriaClm.Sortable = True
            Nom_SubCategoriaClm.AspectName = "Nom_SubCategoria"
            Nom_SubCategoriaClm.Width = 150

            idProductoClm.IsVisible = True
            idProductoClm.Sortable = False
            idProductoClm.AspectName = "idProducto"
            idProductoClm.Width = 100


            Nom_ComercialClm.IsVisible = False
            Nom_ComercialClm.Sortable = False
            Nom_ComercialClm.AspectName = "Nom_Comercial"
            Nom_ComercialClm.Width = 250

            StockCm.IsVisible = True
            StockCm.AspectName = "Stock"
            StockCm.Sortable = False
            StockCm.Width = 120


            CostoClm.IsVisible = True
            CostoClm.AspectName = "Costo"
            CostoClm.Sortable = False
            CostoClm.Width = 150


            CostoTotalClm.IsVisible = True
            CostoTotalClm.Width = 200
            CostoTotalClm.AspectName = "CostoTotal"
            CostoTotalClm.Sortable = False

            Me.ObjectListView1.SetObjects(dt.AsEnumerable())
            Dim total = dt.AsEnumerable().Sum(Function(x) x.Field(Of Decimal)("CostoTotal"))

            Label1.Text = "Total costo en productos: " & total.ToString("C2")
        Else
            dt = Nothing
            Me.ObjectListView1.ClearObjects()
        End If

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub PrintButton_Click(sender As Object, e As EventArgs) Handles PrintButton.Click

        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            MsgBox("No hay datos", MsgBoxStyle.Exclamation, "Error")
            Return
        End If

        Try

            Dim saveDlg As SaveFileDialog = New SaveFileDialog()
            saveDlg.InitialDirectory = Me.DowloadFile
            saveDlg.Filter = "Excel files (*.xlsx)|*.xlsx" 'xlsx
            saveDlg.FilterIndex = 0
            saveDlg.FileName = Me.typoArchivo & " " & DateTime.Now.ToString("yyyy-MM-mm HH_MM_s")
            saveDlg.RestoreDirectory = True
            saveDlg.Title = "Export el archivo"

            If saveDlg.ShowDialog() = DialogResult.OK Then

                If (File.Exists(saveDlg.FileName)) Then
                    MsgBox("Ya existe este archivo")
                    Return
                End If

                If typoData = 1 Then
                    ExpotCategory(saveDlg.FileName)
                ElseIf typoData = 2 Then
                    ExpotSubCategoty(saveDlg.FileName)
                ElseIf typoData = 3 Then
                    ExpotProduct(saveDlg.FileName)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error ")
        End Try

    End Sub

    Private Async Sub ExpotCategory(fileName As String)

        Await Task.Factory.StartNew(Sub()
                                        Try
                                            Dim sl As New SLDocument()
                                            Dim style As New SLStyle()

                                            style.Font.FontSize = 12
                                            style.Font.Bold = True


                                            sl.SetCellValue(1, 1, "CATEGORIA")
                                            sl.SetCellStyle(1, 1, style)
                                            sl.SetColumnWidth(1, 20)


                                            sl.SetCellValue(1, 2, "ID SUBCATEGORIA")
                                            sl.SetCellStyle(1, 2, style)
                                            sl.SetColumnWidth(2, 20)

                                            sl.SetCellValue(1, 3, "SUB CATEGORIA")
                                            sl.SetCellStyle(1, 3, style)
                                            sl.SetColumnWidth(3, 25)


                                            sl.SetCellValue(1, 4, "NUM ARTICULOS")
                                            sl.SetCellStyle(1, 4, style)
                                            sl.SetColumnWidth(4, 20)

                                            sl.SetCellValue(1, 5, "COSTO TOTAL")
                                            sl.SetCellStyle(1, 5, style)
                                            sl.SetColumnWidth(5, 20)

                                            Dim rowIndex As Integer = 2
                                            For Each row As DataRow In Me.dt.Rows
                                                sl.SetCellValue(rowIndex, 1, row.Field(Of String)("Nom_Categoria"))
                                                sl.SetCellValue(rowIndex, 2, row.Field(Of Integer)("idSubCategoria"))
                                                sl.SetCellValue(rowIndex, 3, row.Field(Of String)("Nom_SubCategoria"))
                                                sl.SetCellValue(rowIndex, 4, row.Field(Of Integer)("Articulos"))
                                                sl.SetCellValue(rowIndex, 5, row.Field(Of Decimal)("CostoTotal"))
                                                rowIndex += 1
                                            Next
                                            sl.SaveAs(fileName)
                                            Me.Invoke(New MethodInvoker(Sub()
                                                                            Me.Cursor = Cursors.Default
                                                                            MsgBox("Exportado.!!", MsgBoxStyle.Information, "Aviso")
                                                                        End Sub))
                                        Catch ex As Exception
                                            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error ")
                                        End Try

                                    End Sub)


    End Sub
    Private Async Sub ExpotSubCategoty(filleName As String)

        Await Task.Factory.StartNew(Sub()
                                        Try
                                            Dim sl As New SLDocument()
                                            Dim style As New SLStyle()

                                            style.Font.FontSize = 12
                                            style.Font.Bold = True

                                            sl.SetCellValue(1, 1, "SUB CATEGORIA")
                                            sl.SetCellStyle(1, 1, style)


                                            sl.SetCellValue(1, 2, "ID PRODUCTO")
                                            sl.SetCellStyle(1, 2, style)

                                            sl.SetCellValue(1, 3, "PRODUCTO")
                                            sl.SetCellStyle(1, 3, style)


                                            sl.SetCellValue(1, 4, "STOCK")
                                            sl.SetCellStyle(1, 4, style)

                                            sl.SetCellValue(1, 5, "COSTO UNITARIO")
                                            sl.SetCellStyle(1, 5, style)

                                            sl.SetCellValue(1, 6, "COSTO TOTAL")
                                            sl.SetCellStyle(1, 6, style)

                                            Dim rowIndex As Integer = 2
                                            For Each row As DataRow In Me.dt.Rows
                                                sl.SetCellValue(rowIndex, 1, row.Field(Of String)("Nom_SubCategoria"))
                                                sl.SetCellValue(rowIndex, 2, row.Field(Of Integer)("idProducto"))
                                                sl.SetCellValue(rowIndex, 3, row.Field(Of String)("Nom_Comercial"))
                                                sl.SetCellValue(rowIndex, 4, row.Field(Of Decimal)("Stock"))
                                                sl.SetCellValue(rowIndex, 5, row.Field(Of Decimal)("Costo"))
                                                sl.SetCellValue(rowIndex, 6, row.Field(Of Decimal)("CostoTotal"))
                                                rowIndex += 1
                                            Next
                                            sl.SaveAs(filleName)
                                            Me.Invoke(New MethodInvoker(Sub()
                                                                            Me.Cursor = Cursors.Default
                                                                            MsgBox("Exportado.!!", MsgBoxStyle.Information, "Aviso")
                                                                        End Sub))
                                        Catch ex As Exception
                                            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error ")
                                        End Try

                                    End Sub)


    End Sub
    Private Async Sub ExpotProduct(fileName As String)

        Await Task.Factory.StartNew(Sub()
                                        Try
                                            Dim sl As New SLDocument()
                                            Dim style As New SLStyle()

                                            style.Font.FontSize = 12
                                            style.Font.Bold = True


                                            sl.SetCellValue(1, 1, "Id producto")
                                            sl.SetCellStyle(1, 1, style)
                                            sl.SetColumnWidth(1, 20)


                                            sl.SetCellValue(1, 2, "Producto")
                                            sl.SetCellStyle(1, 2, style)
                                            sl.SetColumnWidth(2, 20)

                                            sl.SetCellValue(1, 3, "Stock")
                                            sl.SetCellStyle(1, 3, style)
                                            sl.SetColumnWidth(3, 25)


                                            sl.SetCellValue(1, 4, "Costo Promedio")
                                            sl.SetCellStyle(1, 4, style)
                                            sl.SetColumnWidth(4, 20)

                                            sl.SetCellValue(1, 5, "COSTO TOTAL")
                                            sl.SetCellStyle(1, 5, style)
                                            sl.SetColumnWidth(5, 20)

                                            Dim rowIndex As Integer = 2
                                            For Each row As DataRow In Me.dt.Rows
                                                sl.SetCellValue(rowIndex, 1, row.Field(Of Integer)("idProducto"))
                                                sl.SetCellValue(rowIndex, 2, row.Field(Of String)("Nom_Comercial"))
                                                sl.SetCellValue(rowIndex, 3, row.Field(Of Decimal)("Stock"))
                                                sl.SetCellValue(rowIndex, 4, row.Field(Of Decimal)("Costo"))
                                                sl.SetCellValue(rowIndex, 5, row.Field(Of Decimal)("CostoTotal"))
                                                rowIndex += 1
                                            Next
                                            sl.SaveAs(fileName)
                                            Me.Invoke(New MethodInvoker(Sub()
                                                                            Me.Cursor = Cursors.Default
                                                                            MsgBox("Exportado.!!", MsgBoxStyle.Information, "Aviso")
                                                                        End Sub))
                                        Catch ex As Exception
                                            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error ")
                                        End Try
                                    End Sub)


    End Sub

    Private Sub findButton_Click(sender As Object, e As EventArgs) Handles findButton.Click
        Try
            txtProduc_Select.Text = txtProduc_Select.Text.Trim()
            If txtProduc_Select.Text.Length = 0 Then Exit Sub


            Me.Cursor = Cursors.WaitCursor
            Me.Label2.Text = String.Format("Total registros: {0:N0}", 0)

            Me.ObjectListView1.ClearObjects()
            Me.dt = Nothing

            If MySelectProduct(txtProduc_Select.Text) Then
                Carga_ListProducto()
                typoArchivo = "SubCategoria"
                typoData = 3
            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub Carga_ListProducto()
        Try
            Using adat As New pcdGetListProductRentableTableAdapter
                Using dt As New pcdGetListProductRentableDataTable
                    adat.Fill(dt, codTerminal:=TerminalActivo.codTerminal, codUser:=UsuarioActivo.codUser)
                    If dt.Rows.Count > 0 Then
                        GetDataSelect()
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message + " en el Carga_ListProducto del " + Me.Name, MsgBoxStyle.Critical, "Error al cargar Proveedor")
        End Try
    End Sub

    Private Async Sub GetDataSelect()

        dt = Nothing
        sql = $"select top(1000)  b.Nom_Bodega, p.idProducto,p.Nom_Comercial, s.Stock ,s.pvpUND as Costo ,  
                (s.Stock * s.pvpUND) as  CostoTotal, s.idProdcutStock
                 from Productos as p
                 inner join ProductosStock as s on s.idProducto  = p.idProducto
				 inner join Bodegas  as b on b.idBodega = s.idBodega
                 INNER JOIN [tmp].SelectMyProduct as myPd on myPd.idProducto = p.idProducto
	             where ((myPd.codTerminal='{TerminalActivo.codTerminal}') and (myPd.codUser ='{UsuarioActivo.codUser}')
                 and s.idBodega = {Me.IdBodega})"

        Using cmd As New SqlComandExec()
            dt = Await cmd.RetornaTablaAsync(sql)

        End Using
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each cls As BrightIdeasSoftware.OLVColumn In ObjectListView1.Columns
                cls.IsVisible = False
                cls.AspectName = String.Empty
                cls.Sortable = True
                cls.Width = 0
            Next


            ObjectListView1.Sort(Nom_ComercialClm, SortOrder.Descending)
            ObjectListView1.ShowGroups = False

            idProductoClm.IsVisible = True
            idProductoClm.Sortable = True
            idProductoClm.AspectName = "idProducto"
            idProductoClm.Width = 100




            Nom_BodegaClm.IsVisible = True
            Nom_BodegaClm.AspectName = "Nom_Bodega"
            Nom_BodegaClm.Width = 200


            Nom_ComercialClm.IsVisible = False
            Nom_ComercialClm.AspectName = "Nom_Comercial"
            Nom_ComercialClm.Width = 250

            StockCm.IsVisible = True
            StockCm.AspectName = "Stock"
            StockCm.Sortable = False
            StockCm.Width = 120


            CostoClm.IsVisible = True
            CostoClm.AspectName = "Costo"
            CostoClm.Sortable = False
            CostoClm.Width = 150


            CostoTotalClm.IsVisible = True
            CostoTotalClm.Width = 200
            CostoTotalClm.AspectName = "CostoTotal"
            CostoTotalClm.Sortable = False

            Me.ObjectListView1.SetObjects(dt.AsEnumerable())
            Dim total = dt.AsEnumerable().Sum(Function(x) x.Field(Of Decimal)("CostoTotal"))

            Label1.Text = "Total costo en productos: " & total.ToString("C2")
        Else
            dt = Nothing
            Me.ObjectListView1.ClearObjects()
        End If

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub ObjectListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ObjectListView1.SelectedIndexChanged

    End Sub

    Private Sub txtProduc_Select_TextChanged(sender As Object, e As EventArgs) Handles txtProduc_Select.TextChanged

        If txtProduc_Select.Text.Trim().Length > 2 Then
            Me.AcceptButton = findButton
        Else
            Me.AcceptButton = Nothing
        End If

    End Sub



    Private Async Sub ChangeStockFromExcel(fileName As String, idBodega As Integer)
        Try
            Dim sl As New SLDocument(fileName)

            Dim iRow As Integer = 2
            Dim idProducto As Integer = 0
            Dim stock As Decimal = 0
            Using cmd As New SqlComandExec
                cmd.BeginTransaction()

                While (Not String.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))

                    idProducto = sl.GetCellValueAsInt64(iRow, 1)
                    If (idProducto = 0) Then Exit While
                    stock = sl.GetCellValueAsDecimal(iRow, 3)

                    sql = $"update ProductosStock set stock ={stock}
				          where idProducto = {idProducto} and idBodega = {idBodega}"
                    cmd.ExecuteComand(sql)
                    iRow += 1
                End While

                cmd.Commit()
            End Using

            Await Task.Factory.StartNew(Sub()
                                            If iRow > 0 Then
                                                MsgBox("Procesos ejecutado exitosamente!!", MsgBoxStyle.Exclamation, "Aviso")
                                            End If
                                            Me.Invoke(New MethodInvoker(Sub()
                                                                            Me.Cursor = Cursors.Default
                                                                        End Sub))

                                        End Sub)

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")

        End Try
    End Sub

    Private Sub EditCountStockButton_Click(sender As Object, e As EventArgs) Handles EditCountStockButton.Click
        Try
            If Me.ObjectListView1.SelectedObjects.Count = 1 And typoData = 3 Then
                Dim row = ObjectListView1.SelectedObjects(0)

                Dim dataRow = CType(row, DataRow)

                Using newform As New frmImputData()
                    With newform
                        .txtNumber.Value = dataRow.Field(Of Decimal)("Stock")
                        .ShowDialog()
                        If .DialogResult = DialogResult.OK Then
                            sql = $"update ProductosStock set stock =  { .txtNumber.Value}
				                    where idProdcutStock = {dataRow.Field(Of Integer)("idProdcutStock")}"
                            Using cmd As New SqlComandExec
                                cmd.ExecuteComand(sql)
                            End Using
                            dataRow("Stock") = .txtNumber.Value
                            ObjectListView1.UpdateObject(dataRow)
                        End If
                    End With
                End Using
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub EditPvPStockButton_Click(sender As Object, e As EventArgs) Handles EditPvPStockButton.Click
        Try
            If Me.ObjectListView1.SelectedObjects.Count = 1 And typoData = 3 Then
                Dim row = ObjectListView1.SelectedObjects(0)

                Dim dataRow = CType(row, DataRow)

                Dim idProducto As Integer = dataRow.Field(Of Integer)("idProdcutStock")

                Using newform As New frmImputData()
                    With newform
                        .txtNumber.Value = dataRow.Field(Of Decimal)("Costo")
                        .ShowDialog()
                        If .DialogResult = DialogResult.OK Then
                            sql = $"update ProductosStock set pvpUND =  { .txtNumber.Value}
				                    where idProdcutStock = {idProducto}"
                            Using cmd As New SqlComandExec
                                cmd.BeginTransaction()
                                cmd.ExecuteComand(sql)
                                sql = $"update ProductoPresentacion set precioCompra = { .txtNumber.Value}
				                         where idProUndMed = (select top(1) s.idProUndMed  
				                         from ProductosStock as s where s.idProdcutStock  = {idProducto});"
                                cmd.ExecuteComand(sql)
                                cmd.Commit()
                            End Using
                            dataRow("Costo") = .txtNumber.Value
                            ObjectListView1.UpdateObject(dataRow)
                        End If
                    End With
                End Using
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ExportarCategoriasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportarCategoriasToolStripMenuItem.Click
        Try
            Dim saveDlg As SaveFileDialog = New SaveFileDialog()
            saveDlg.InitialDirectory = Me.DowloadFile
            saveDlg.Filter = "Excel files (*.xlsx)|*.xlsx|Excel others (*.xls)|*.xls"
            saveDlg.FilterIndex = 0
            saveDlg.FileName = "Categorias " & DateTime.Now.ToString("yyyy-MM-mm HH_MM_s")
            saveDlg.RestoreDirectory = True
            saveDlg.Title = "Export el archivo"

            If saveDlg.ShowDialog() = DialogResult.OK Then

                Me.Cursor = Cursors.WaitCursor
                If Not File.Exists(saveDlg.FileName) Then
                    ExportCategories(saveDlg.FileName)
                End If

            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message + " en el Carga_ListProducto del " + Me.Name, MsgBoxStyle.Critical, "Error al cargar Proveedor")
        End Try
    End Sub

    Private Async Sub ExportCategories(fileName As String)

        Await Task.Factory.StartNew(Sub()
                                        Try
                                            sql = $"select ca.idCategoria , ca.Nom_Categoria , su.idSubCategoria , su.Nom_SubCategoria
                                                    from ProductoCategoria as ca 
                                                    inner join ProductoSubCategoria as su on su.idCategoria   =ca.idCategoria"
                                            Dim dt_local As DataTable

                                            Using cmd As New SqlComandExec
                                                dt_local = cmd.RetornaTabla(sql)
                                            End Using


                                            If dt_local Is Nothing AndAlso dt_local.Rows.Count = 0 Then Return


                                            Dim sl As New SLDocument()
                                            Dim style As New SLStyle()

                                            style.Font.FontSize = 12
                                            style.Font.Bold = True


                                            sl.SetCellValue(1, 1, "ID CATEGORIA")
                                            sl.SetCellStyle(1, 1, style)
                                            sl.SetColumnWidth(1, 10)

                                            sl.SetCellValue(1, 2, "CATEGORIA")
                                            sl.SetCellStyle(1, 2, style)
                                            sl.SetColumnWidth(2, 20)


                                            sl.SetCellValue(1, 3, "ID SUBCATEGORIA")
                                            sl.SetCellStyle(1, 3, style)
                                            sl.SetColumnWidth(3, 10)

                                            sl.SetCellValue(1, 4, "SUBCATEGORIA")
                                            sl.SetCellStyle(1, 4, style)
                                            sl.SetColumnWidth(4, 20)

                                            Dim rowIndex As Integer = 2
                                            For Each row As DataRow In dt_local.Rows
                                                sl.SetCellValue(rowIndex, 1, row.Field(Of Integer)("idCategoria"))
                                                sl.SetCellValue(rowIndex, 2, row.Field(Of String)("Nom_Categoria"))
                                                sl.SetCellValue(rowIndex, 3, row.Field(Of Integer)("idSubCategoria"))
                                                sl.SetCellValue(rowIndex, 4, row.Field(Of String)("Nom_SubCategoria"))
                                                rowIndex += 1
                                            Next
                                            sl.SaveAs(fileName)
                                            Me.Invoke(New MethodInvoker(Sub()
                                                                            Me.Cursor = Cursors.Default
                                                                            MsgBox("Exportado.!!", MsgBoxStyle.Information, "Aviso")
                                                                        End Sub))
                                        Catch ex As Exception
                                            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error ")
                                        End Try
                                    End Sub)

    End Sub

    Private Sub ProductoConCategoriaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductoConCategoriaToolStripMenuItem.Click
        Try
            Dim saveDlg As SaveFileDialog = New SaveFileDialog()
            saveDlg.InitialDirectory = Me.DowloadFile
            saveDlg.Filter = "Excel files (*.xlsx)|*.xlsx|Excel others (*.xls)|*.xls"
            saveDlg.FilterIndex = 0
            saveDlg.FileName = "Producto con Categoria " & DateTime.Now.ToString("yyyy-MM-mm HH_MM_s")
            saveDlg.RestoreDirectory = True
            saveDlg.Title = "Export el archivo"

            If saveDlg.ShowDialog() = DialogResult.OK Then

                Me.Cursor = Cursors.WaitCursor
                If Not File.Exists(saveDlg.FileName) Then
                    ExportProductWithCategory(saveDlg.FileName)
                End If

            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message + " en el Carga_ListProducto del " + Me.Name, MsgBoxStyle.Critical, "Error al cargar Proveedor")
        End Try
    End Sub

    Private Async Sub ExportProductWithCategory(fileName As String)

        Await Task.Factory.StartNew(Sub()
                                        Try
                                            sql = $"select p.idProducto , p.Nom_Comercial , su.idSubCategoria , su.Nom_SubCategoria,s.stock
                                                    from Productos as p 
                                                    inner join ProductoSubCategoria as su on su.idSubCategoria  =p.IdSubCategoria
                                                    inner join ProductosStock as s on s.idProducto = p.idProducto"
                                            Dim dt_local As DataTable

                                            Using cmd As New SqlComandExec
                                                dt_local = cmd.RetornaTabla(sql)
                                            End Using


                                            If dt_local Is Nothing AndAlso dt_local.Rows.Count = 0 Then Return


                                            Dim sl As New SLDocument()
                                            Dim style As New SLStyle()

                                            style.Font.FontSize = 12
                                            style.Font.Bold = True


                                            sl.SetCellValue(1, 1, "ID PRODUCTO")
                                            sl.SetCellStyle(1, 1, style)
                                            sl.SetColumnWidth(1, 8)

                                            sl.SetCellValue(1, 2, "PRODUCTO")
                                            sl.SetCellStyle(1, 2, style)
                                            sl.SetColumnWidth(2, 22)


                                            sl.SetCellValue(1, 3, "ID SUBCATEGORIA")
                                            sl.SetCellStyle(1, 3, style)
                                            sl.SetColumnWidth(3, 8)

                                            sl.SetCellValue(1, 4, "SUBCATEGORIA")
                                            sl.SetCellStyle(1, 4, style)
                                            sl.SetColumnWidth(4, 20)


                                            sl.SetCellValue(1, 5, "STOCK")
                                            sl.SetCellStyle(1, 5, style)
                                            sl.SetColumnWidth(5, 10)

                                            Dim rowIndex As Integer = 2
                                            For Each row As DataRow In dt_local.Rows
                                                sl.SetCellValue(rowIndex, 1, row.Field(Of Integer)("idProducto"))
                                                sl.SetCellValue(rowIndex, 2, row.Field(Of String)("Nom_Comercial"))
                                                sl.SetCellValue(rowIndex, 3, row.Field(Of Integer)("idSubCategoria"))
                                                sl.SetCellValue(rowIndex, 4, row.Field(Of String)("Nom_SubCategoria"))
                                                sl.SetCellValue(rowIndex, 5, row.Field(Of Decimal)("stock"))
                                                rowIndex += 1
                                            Next
                                            sl.SaveAs(fileName)
                                            Me.Invoke(New MethodInvoker(Sub()
                                                                            Me.Cursor = Cursors.Default
                                                                            MsgBox("Exportado.!!", MsgBoxStyle.Information, "Aviso")
                                                                        End Sub))
                                        Catch ex As Exception
                                            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error ")
                                        End Try
                                    End Sub)

    End Sub

    Private Sub PrintButton_MouseDown(sender As Object, e As MouseEventArgs) Handles PrintButton.MouseDown
        If e.Button = MouseButtons.Right Then
            ContextMenuStrip1.Show(Cursor.Position)
        End If
    End Sub


    Private Async Sub ChangeCatgoryFromExcel(fileName As String)
        Try
            Dim sl As New SLDocument(fileName)

            Dim iRow As Integer = 2
            Dim idProducto As Integer = 0
            Dim idSubCategory As Decimal = 0
            Using cmd As New SqlComandExec
                cmd.BeginTransaction()

                While (Not String.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))

                    idProducto = sl.GetCellValueAsInt64(iRow, 1)
                    idSubCategory = sl.GetCellValueAsDecimal(iRow, 3)

                    sql = $"update Productos set IdSubCategoria ={idSubCategory}
	                        where idProducto = {idProducto}"

                    cmd.ExecuteComand(sql)
                    iRow += 1
                End While

                cmd.Commit()
            End Using

            Await Task.Factory.StartNew(Sub()
                                            If iRow > 0 Then
                                                MsgBox("Procesos ejecutado exitosamente!!", MsgBoxStyle.Exclamation, "Aviso")
                                            End If
                                            Me.Invoke(New MethodInvoker(Sub()
                                                                            Me.Cursor = Cursors.Default
                                                                        End Sub))

                                        End Sub)

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")

        End Try
    End Sub

    Private Sub ActualizarStockDeProductosDesdeUnListadoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActualizarStockDeProductosDesdeUnListadoToolStripMenuItem.Click
        Try
            'actualizar stock
            Dim saveDlg As OpenFileDialog = New OpenFileDialog()
            saveDlg.InitialDirectory = Me.DowloadFile
            saveDlg.Filter = "Excel files (*.xlsx)|*.xlsx|Excel others (*.xls)|*.xls"
            saveDlg.FilterIndex = 0
            saveDlg.FileName = Me.typoArchivo & " " & DateTime.Now.ToString("yyyy-MM-mm HH_MM_s")
            saveDlg.RestoreDirectory = True
            saveDlg.Title = "Export el archivo"

            If saveDlg.ShowDialog() = DialogResult.OK Then

                sql = "Esto altera el stock de acuerdo a la informacion existente en excel." & vbLf
                sql = sql & "Esta seguro de continuar con el proceso.?"

                If Not (MsgBox(sql, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes) Then
                    Return
                End If
                Me.Cursor = Cursors.WaitCursor
                If File.Exists(saveDlg.FileName) Then
                    Dim idBodega As Integer = 0
                    Using selecWareH As New frmSelectWareHouse()
                        selecWareH.ShowDialog()
                        If Not (selecWareH.DialogResult = DialogResult.OK) Then
                            Me.Cursor = Cursors.Default
                            Exit Sub
                        End If
                        idBodega = selecWareH.IdBodega
                    End Using

                    ChangeStockFromExcel(saveDlg.FileName, idBodega)
                End If

            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")

        End Try
    End Sub

    Private Sub ActualizarCategoriasDesdeUnListadoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ActualizarCategoriasDesdeUnListadoToolStripMenuItem.Click
        Try
            ' actualiar categoria
            Dim saveDlg As OpenFileDialog = New OpenFileDialog()
            saveDlg.InitialDirectory = Me.DowloadFile
            saveDlg.Filter = "Excel files (*.xlsx)|*.xlsx|Excel others (*.xls)|*.xls"
            saveDlg.FilterIndex = 0
            saveDlg.RestoreDirectory = True
            saveDlg.Title = "Leer Datos"

            If saveDlg.ShowDialog() = DialogResult.OK Then

                sql = "Esto cambiará la ubicacion del producto dentro de una categoría" & vbLf
                sql = sql & "Tomando referencia la información que esta en excel. " & vbLf
                sql = sql & "Esta seguro de continuar con el proceso.?"

                If Not (MsgBox(sql, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes) Then
                    Return
                End If
                Me.Cursor = Cursors.WaitCursor
                If File.Exists(saveDlg.FileName) Then
                    ChangeCatgoryFromExcel(saveDlg.FileName)
                End If

            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & +ex.StackTrace, MsgBoxStyle.Critical, "Error ")

        End Try
    End Sub

    Private Sub UpdateFromExelButton_MouseDown(sender As Object, e As MouseEventArgs) Handles UpdateFromExelButton.MouseDown
        If e.Button = MouseButtons.Right Then
            ContextMenuStrip2.Show(Cursor.Position)
        End If
    End Sub

    Private Async Sub ExportarTodaLaListaDeProductosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportarTodaLaListaDeProductosToolStripMenuItem.Click
        Try
            Dim idBodega As Integer = 0
            Dim nomBodega As String = ""
            Me.Cursor = Cursors.WaitCursor

            Using selecWareH As New frmSelectWareHouse()
                selecWareH.ShowDialog()
                If Not (selecWareH.DialogResult = DialogResult.OK) Then
                    Me.Cursor = Cursors.Default
                    Exit Sub
                End If
                idBodega = selecWareH.IdBodega
                nomBodega = selecWareH.NomBodega
                nomBodega = nomBodega.Replace("|", "")
            End Using


            Dim saveDlg As SaveFileDialog = New SaveFileDialog()
            saveDlg.InitialDirectory = Me.DowloadFile
            saveDlg.Filter = "Excel files (*.xlsx)|*.xlsx" 'xlsx
            saveDlg.FilterIndex = 0
            saveDlg.FileName = "Stock " + nomBodega & " " & DateTime.Now.ToString("yyyy-MM-mm HH_mm_ss")
            saveDlg.RestoreDirectory = True
            saveDlg.Title = "Export el archivo"

            If saveDlg.ShowDialog() = DialogResult.OK Then

                If (File.Exists(saveDlg.FileName)) Then
                    MsgBox("Ya existe este archivo")
                    Return
                End If

                sql = $"select p.idProducto,p.Nom_Comercial,s.Stock,s.pvpUND as [Costo],
                s.Stock * s.pvpUND [CostoTotal]
                from ProductosStock as s
                inner join Productos as  p on s.idProducto = p.idProducto
                where idBodega = @idBodega;"

                Using cmd As SqlComandExec = New SqlComandExec()

                    cmd.ParameterCollection = New SqlParameter() {New SqlParameter With
                            {
                                .ParameterName = "@idBodega",
                                .SqlDbType = SqlDbType.Int,
                                .Value = idBodega
                         }}

                    Me.dt = Await cmd.RetornaTablaAsync(sql)

                End Using

                If (Me.dt IsNot Nothing AndAlso Me.dt.Rows.Count > 0) Then
                    ExpotProduct(saveDlg.FileName)
                End If

            End If

            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub UpdateFromExelButton_Click(sender As Object, e As EventArgs) Handles UpdateFromExelButton.Click
        'Dim rand As Random = New Random()

        'Dim dt As System.Data.DataTable = New System.Data.DataTable()
        'dt.Columns.Add("Product", GetType(String))
        'dt.Columns.Add("IP Address", GetType(String))
        'dt.Columns.Add("Date (UTC)", GetType(DateTime))
        'dt.Columns.Add("Size (MB)", GetType(Double))
        'dt.Columns.Add("Cost", GetType(Decimal))

        'For i As Integer = 0 To 20 - 1
        '    dt.Rows.Add(String.Format("Prod{0}", i + 1),
        '                String.Format("{0}.{1}.{2}.{3}",
        '                              rand.[Next](256), rand.[Next](256), rand.[Next](256),
        '                              rand.[Next](256)), DateTime.UtcNow.AddDays(rand.NextDouble() * 20),
        '                                                 Decimal.Round(CDec((rand.NextDouble() * 500 + 200)), 4),
        '                                                 Decimal.Round(CDec((rand.NextDouble() * 20 + 5)), 2))
        'Next

        'Dim stSettings As SLThemeSettings = BuildTheme()
        'Dim sheet As SLDocument = New SLDocument(stSettings)
        'sheet.ImportDataTable(1, 1, dt, True)
        'sheet.SetColumnWidth(1, 5, 12)
        'Dim style As SLStyle = sheet.CreateStyle()
        'style.FormatCode = "MM/dd/yyyy"
        'sheet.SetColumnStyle(3, style)
        'sheet.FreezePanes(1, 4)
        'Dim headerstyle As SLStyle = sheet.CreateStyle()
        'headerstyle.Font.Bold = True
        'headerstyle.Font.FontColor = System.Drawing.Color.IndianRed
        'headerstyle.Fill.SetPattern(PatternValues.Solid, SLThemeColorIndexValues.Light2Color, SLThemeColorIndexValues.Light2Color)
        'sheet.SetRowStyle(1, headerstyle)
        'Dim redrowstyle As SLStyle = sheet.CreateStyle()
        'redrowstyle.Font.FontColor = System.Drawing.Color.Black
        'redrowstyle.Fill.SetPattern(PatternValues.Solid, SLThemeColorIndexValues.Accent1Color, SLThemeColorIndexValues.Accent1Color)
        'sheet.SetCellStyle("A9", "E15", redrowstyle)
        'Dim yellowrowstyle As SLStyle = sheet.CreateStyle()
        'redrowstyle.Font.FontColor = System.Drawing.Color.Black
        'redrowstyle.Fill.SetPattern(PatternValues.Solid, SLThemeColorIndexValues.Accent3Color, SLThemeColorIndexValues.Accent3Color)
        'sheet.SetCellStyle("A16", "E18", redrowstyle)
        'Dim cellstyle As SLStyle = sheet.CreateStyle()
        'cellstyle.Font.FontColor = System.Drawing.Color.Black
        'cellstyle.Fill.SetPattern(PatternValues.Solid, SLThemeColorIndexValues.Accent4Color, SLThemeColorIndexValues.Accent4Color)
        'sheet.SetCellStyle("A19", cellstyle)
        'Dim standardstyle As SLStyle = New SLStyle()
        'standardstyle.FormatCode = "#,##0.000;[Red](-#,##0.000);#,##0.000"
        'sheet.SetCellStyle("D1", "D4", standardstyle)
        'Dim CurrencySignstyle As SLStyle = New SLStyle()
        'CurrencySignstyle.FormatCode = "$#,##0.000;[Red]$(-#,##0.000);$#,##0.000"
        'sheet.SetCellStyle("D5", "D6", CurrencySignstyle)
        'Dim PercentageSignstyle As SLStyle = New SLStyle()
        'PercentageSignstyle.FormatCode = "0.00%;[Red](-0.00%);0.00%"
        'sheet.SetCellStyle("D7", "D10", PercentageSignstyle)
        'sheet.SaveAs("C:\Users\Juan Taday\Documents\Adobe\SpreadsheetLight.xlsx")
        'MessageBox.Show("Done")
    End Sub

    Private Function BuildTheme() As SLThemeSettings

        Dim theme As SLThemeSettings = New SLThemeSettings()
        theme.ThemeName = "RDSColourTheme"
        '//theme.MajorLatinFont = "Impact"
        ' //theme.MinorLatinFont = "Harrington"
        '// this Is recommended to be pure white
        theme.Light1Color = System.Drawing.Color.White
        ' // this Is recommended to be pure black
        theme.Dark1Color = System.Drawing.Color.Black
        theme.Light2Color = System.Drawing.Color.LightGray
        theme.Dark2Color = System.Drawing.Color.IndianRed
        theme.Accent1Color = System.Drawing.Color.Red
        theme.Accent2Color = System.Drawing.Color.Tomato
        theme.Accent3Color = System.Drawing.Color.Yellow
        theme.Accent4Color = System.Drawing.Color.LawnGreen
        theme.Accent5Color = System.Drawing.Color.DeepSkyBlue
        theme.Accent6Color = System.Drawing.Color.DarkViolet
        theme.Hyperlink = System.Drawing.Color.Blue
        theme.FollowedHyperlinkColor = System.Drawing.Color.Purple
        Return theme
    End Function

End Class