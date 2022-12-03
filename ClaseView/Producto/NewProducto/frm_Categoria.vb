Imports System.Data.SqlClient
Imports CADsisVenta.Funtions

Public Class frm_Categoria
    Private myPadre As MDI_AddProdcutos
    Protected Friend SelectedNode As TreeNode
    Dim Nodes As String()
    Dim categoria As String
    Private ReadOnly id_producto As Integer
    Protected Friend id_subCategory As Integer
    Protected Friend isSubCategory As Boolean
    Private estado As Boolean
    Private isLoad As Boolean

    Sub New(myPadre As MDI_AddProdcutos)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        Me.myPadre = myPadre
        id_subCategory = If(Me.myPadre?.idSubCategorySelect, 0)
        Me.id_producto = If(Me.myPadre?.id_Producto, 0)
        ExpantButton.Image = My.Resources.Expant_Treeview_24

        Me.SaveButton.Height = 0
        Me.SaveButton.Width = 0

        Me.SiguientButton.Height = 0
        Me.SiguientButton.Width = 0

    End Sub
    Private Sub frm_Categoria_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        If Not IsNothing(Me.myPadre) Then
            With Me.myPadre
                estado = .Estado
                SiguientButton.Visible = .Estado
                SaveButton.Enabled = Not estado
            End With

            PanelComannSelect.Visible = False
            PanelDesciption.Visible = True
            PanelMenuAdd.Visible = True

            PanelExpantTreeView.Visible = False
        Else
            Me.MenuStripCatego.Visible = False
            Me.lblCategoria.Visible = False
            Me.lblnodes.Visible = False
            PanelComannSelect.Visible = True
            PanelDesciption.Visible = False
            PanelMenuAdd.Visible = False
            PanelExpantTreeView.Visible = True
        End If
        CargaControlCategoria()
        SelectedNode = Nothing
        isLoad = True
    End Sub
    Private Sub CategoriaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CategoriaToolStripMenuItem.Click
        AvilitaDialogo()
        categoria = "Categoria"
    End Sub
    Private Sub AvilitaDialogo()
        Me.TreeViewCatgoria.Enabled = False
        Me.txtNameCategoria.Visible = True
        btnCancelCategory.Visible = True
        btnAcepCategoria.Visible = True
        MenuStripCatego.Enabled = False
        txtNameCategoria.Text = ""
        Me.txtNameCategoria.Focus()
    End Sub
    Private Function Cargar_Categorias()

        sql = "Select c. idCategoria, Nom_Categoria ,sc.idSubCategoria,sc.Nom_SubCategoria
        from ProductoCategoria  as c
        LEFT join ProductoSubCategoria  as sc on sc.idCategoria =c.idCategoria
        order by Nom_Categoria, sc.Nom_SubCategoria"

        Try

            Dim oldCategory As String = String.Empty
            Dim trynode As TreeNode

            TreeViewCatgoria.Nodes.Clear()
            Using cnn As New SqlConnection(SimpleDataApp.Utility.GetConnectionString())
                cnn.Open()
                Using cmd As New SqlCommand(sql, cnn)

                    Using dar As SqlDataReader = cmd.ExecuteReader()
                        While dar.Read()

                            If (Not oldCategory.Equals(dar(1).ToString)) Then
                                trynode = New TreeNode()
                                trynode.Tag = dar(0).ToString
                                trynode.Text = dar(1).ToString
                                trynode.BackColor = Color.AliceBlue
                                TreeViewCatgoria.Nodes.Add(trynode)
                            End If


                            ' add sub categorias
                            If (dar(2) IsNot Nothing) Then
                                Dim tvNode As New TreeNode

                                tvNode.Tag = (dar(2).ToString)
                                tvNode.Text = (dar(3).ToString)
                                tvNode.BackColor = Color.AntiqueWhite

                                trynode.Nodes.Add(tvNode)

                            End If
                            trynode.Checked = True

                            oldCategory = dar(1).ToString
                        End While
                    End Using
                End Using
            End Using
            Return TreeViewCatgoria.Nodes.Count > 0
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Aviso")
            Return False
        End Try
    End Function


    Private Sub btnAcepCategoria_Click(sender As Object, e As EventArgs) Handles btnAcepCategoria.Click

        If txtNameCategoria.TextLength > 0 Then
            If Agregar_Categoria(categoria) Then
                Cargar_Categorias()
                SeleccionaCategoria(myPadre.idSubCategorySelect)
                btnCancelCategory.PerformClick()
            End If
        Else
            MsgBox("Ingrese los datos", MsgBoxStyle.Information, "Aviso")
            txtNameCategoria.Focus()
        End If
    End Sub

    Private Function Agregar_Categoria(ByVal Tipo As String) As Boolean



        Try
            Using cnn = New SqlConnection(DomainSQLite.Setting.Configuration.ConectionString)
                cnn.Open()


                Dim cmd As New SqlCommand()
                cmd.CommandType = CommandType.Text
                cmd.Connection = cnn


                Select Case Tipo
                    Case "Categoria"
                        sql = "Insert into ProductoCategoria (Nom_Categoria) "
                        sql = sql & "Values ('" & Me.txtNameCategoria.Text & "') "
                        sql = sql & "SET @identity = SCOPE_IDENTITY() "

                        cmd.CommandText = sql
                        cmd.Parameters.Add(New SqlParameter("@identity", SqlDbType.Int))
                        cmd.Parameters("@identity").Direction = ParameterDirection.Output
                        If cmd.ExecuteNonQuery Then
                            id_subCategory = (cmd.Parameters("@identity").Value)
                            Return True
                        End If
                    Case "SubCategoria"

                        sql = "Insert into ProductoSubCategoria (idCategoria, Nom_SubCategoria) "
                        sql = sql & "Values ((Select idCategoria from ProductoCategoria where Nom_Categoria = '" & Nodes(0).ToString() & "'),'" & Me.txtNameCategoria.Text & "') "
                        sql = sql & "SET @identity = SCOPE_IDENTITY() "

                        cmd.CommandText = sql
                        cmd.Parameters.Add(New SqlParameter("@identity", SqlDbType.Int))
                        cmd.Parameters("@identity").Direction = ParameterDirection.Output
                        If cmd.ExecuteNonQuery Then
                            id_subCategory = (cmd.Parameters("@identity").Value)
                            Return True
                        End If
                    Case "Modifica"
                        Select Case Nodes.Length
                            Case 1
                                sql = "Update ProductoCategoria set Nom_Categoria = '" & Me.txtNameCategoria.Text & "' where  Nom_Categoria = '" & Nodes(0).ToString & "' "
                                cmd.CommandText = sql

                                If cmd.ExecuteNonQuery Then
                                    Return True
                                End If
                            Case 2
                                sql = "Update ProductoSubCategoria set "
                                sql = sql & "Nom_SubCategoria = '" & Me.txtNameCategoria.Text & "' "
                                sql = sql & "where  ((Nom_SubCategoria = '" & Nodes(1).ToString & "') and "
                                sql = sql & " (idCategoria = (Select idCategoria from ProductoCategoria where Nom_Categoria = '" & Nodes(0).ToString & "'))) "

                                cmd.CommandText = sql
                                If cmd.ExecuteNonQuery Then
                                    Return True
                                End If
                        End Select
                End Select
                Return False
            End Using



        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try

    End Function

    Private Sub TreeViewCatgoria_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeViewCatgoria.AfterSelect

        Try

            If Not IsNothing(Me.myPadre) Then
                With Me.myPadre
                    Nodes = Split(TreeViewCatgoria.SelectedNode.FullPath, "\")
                    SelectedNode = TreeViewCatgoria.SelectedNode
                    If Not IsNothing(Nodes) Then
                        If Nodes.Length > 1 Then
                            Integer.TryParse(SelectedNode.Tag, Me.myPadre.idSubCategorySelect)

                            lblnodes.Location = lblCategoria.Location
                            lblnodes.Text = "Designar a: " + TreeViewCatgoria.SelectedNode.FullPath

                            If Not estado And id_subCategory <> Me.myPadre.idSubCategorySelect Then
                                .SiguienteButton.Visible = True
                                .SiguienteButton.Enabled = True
                                .SiguienteButton.Text = "Guardar"
                            ElseIf Not estado And id_subCategory = Me.myPadre.idSubCategorySelect Then
                                .SiguienteButton.Enabled = False
                                .OkButton.Visible = False
                            ElseIf (estado And id_subCategory = 1) Then
                                .SiguienteButton.Visible = True
                                .SiguienteButton.Enabled = True
                                .OkButton.Visible = True
                                .OkButton.Enabled = False
                                .OkButton.Text = "Aplicar"
                            End If
                        Else
                            .OkButton.Enabled = False
                            lblnodes.Text = ""
                            If Not estado Then
                                .OkButton.Visible = False
                            End If
                        End If
                    End If
                End With
            Else
                lblNodes2.Text = TreeViewCatgoria.SelectedNode.FullPath
                lblNodes2.Visible = True
                SelectedNode = TreeViewCatgoria.SelectedNode
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function idSelectdCategory(nameCate As String, nameSubCate As String) As Integer
        Try
            sql = "Select  s.idSubCategoria from ProductoCategoria  As c "
            sql = sql & "inner Join ProductoSubCategoria as s on  c.idCategoria  = s.idCategoria "
            sql = sql & "where c.Nom_Categoria ='" & nameCate & "' and s.Nom_SubCategoria ='" & nameSubCate & "' "
            Dim cmd As New ClassCargadorProducto()
            Dim dt As DataTable = cmd.RetornaTabla(sql)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0)(0)
                End If
            End If
            Return 0
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return 0
        End Try
    End Function

    Private Sub btnCancelCategory_Click(sender As Object, e As EventArgs) Handles btnCancelCategory.Click
        Me.TreeViewCatgoria.Enabled = True
        Me.txtNameCategoria.Visible = False
        btnCancelCategory.Visible = False
        btnAcepCategoria.Visible = False
        categoria = "SubCategoria"
        MenuStripCatego.Enabled = True
        Me.lblCategoria.Visible = False
        Me.CancelButton = Nothing
        SiguientButton.Enabled = True
    End Sub
    Private Sub txtNameCategoria_TextChanged(sender As Object, e As EventArgs) Handles txtNameCategoria.TextChanged
        If Me.txtNameCategoria.TextLength > 0 Then
            Me.AcceptButton = btnAcepCategoria
        Else
            Me.AcceptButton = Nothing
        End If
    End Sub
    Private Sub ModificarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModificarToolStripMenuItem.Click
        If Not IsNothing(Nodes) Then
            If Nodes.Length > 0 Then
                AvilitaDialogo()
                categoria = "Modifica"
                lblCategoria.Visible = True
                SiguientButton.Enabled = False
                lblCategoria.Text = "Modificando: " + Me.TreeViewCatgoria.SelectedNode.FullPath
                txtNameCategoria.Text = Nodes(Nodes.Length - 1).ToString
            End If
        End If
    End Sub
    Private Sub CargaControlCategoria()
        'cargamos todas las categorias posibles
        Cargar_Categorias()

        If id_producto > 0 Then
            SeleccionaCategoria(Me.myPadre.idSubCategorySelect)
        End If
    End Sub
    Private Function Default_CategoryName(ByVal idCategory As Integer) As String
        Try
            sql = "Select c.Nom_SubCategoria from ProductoSubCategoria as c where c.idSubCategoria = " & idCategory & "	"
            Dim cmd As New ClassCargadorProducto()
            Dim dt As DataTable = cmd.RetornaTabla(sql)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    Return dt.Rows(0)("Nom_SubCategoria")
                End If
            End If
            Return ""
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return ""
        End Try
    End Function

    Private Sub SeleccionaCategoria(ByVal idBubCategoria As Integer)
        Try
            Dim idSub As Integer = 0

            For Each Nodeshi As TreeNode In TreeViewCatgoria.Nodes
                Nodeshi.Collapse()

                For Each subNodeshi As TreeNode In Nodeshi.Nodes

                    Integer.TryParse(subNodeshi.Tag, idSub)

                    If (idSub = idBubCategoria) Then
                        Nodeshi.Expand()
                        TreeViewCatgoria.SelectedNode = subNodeshi
                    Else
                        subNodeshi.Collapse()
                    End If
                Next

            Next


        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub SubCategoríaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SubCategoríaToolStripMenuItem.Click
        If Not IsNothing(Nodes) Then
            If Nodes.Length > 0 Then
                AvilitaDialogo()
                categoria = "SubCategoria"
                lblCategoria.Text = "[" + Nodes(0).ToString + "] Sub categoría"
                Exit Sub
            End If
        Else
            'si no salio antes dice lo siguiente
            MsgBox("Seleccione dentro de que categoría va agregar", MsgBoxStyle.Information, "Aviso")
        End If
    End Sub
    Private Sub EliminarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EliminarToolStripMenuItem.Click
        If Elimina_Categoria() Then
            Cargar_Categorias()
            btnCancelCategory.PerformClick()
        End If
    End Sub
    Private Function Elimina_Categoria() As Boolean

        Dim cmd As New ClassCargadorProducto
        Try
            If Not IsNothing(Nodes) Then
                If Nodes.Length > 0 Then
                    If MsgBox("Esta seguro de eliminar", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Responda") = MsgBoxResult.Yes Then
                        Select Case Nodes.Length
                            Case 1
                                sql = "Delete ProductoCategoria where Nom_Categoria = '" & Nodes(0).ToString & "'"
                                Return cmd.ExecuteComand(sql)
                            Case 2
                                sql = "Delete ProductoSubCategoria where Nom_SubCategoria = '" & Nodes(1).ToString & "' and  "
                                sql = sql & "idCategoria = (Select idCategoria from ProductoCategoria where Nom_Categoria = '" & Nodes(0).ToString & "') "
                                Return cmd.ExecuteComand(sql)
                        End Select
                    End If
                End If
            End If
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try

    End Function

    Private Sub btnSiguientCategor_Click(sender As Object, e As EventArgs) Handles SiguientButton.Click
        Try
            If Not IsNothing(Nodes) Then
                If Nodes.Length > 1 Then
                    If Modifica_Categor_delProdcuto() Then
                        With Me.myPadre
                            If .Estado Then 'si estoy agregando 
                                .SiguienteButton.Enabled = False
                            End If
                            .OkButton.Enabled = True
                        End With
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Function Modifica_Categor_delProdcuto() As Boolean

        Try
            sql = $"Update Productos set IdSubCategoria ={myPadre.idSubCategorySelect}
                    where idproducto ={Me.id_producto}"

            Using cmd As New SqlComandExec
                Return cmd.ExecuteComand(sql)
            End Using
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
            Return False
        End Try

    End Function



    Private Sub TreeViewCatgoria_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles TreeViewCatgoria.MouseDoubleClick
        Try
            okButton.PerformClick()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub closeButton_Click(sender As Object, e As EventArgs) Handles closeButton.Click
        SelectedNode = Nothing
        isSubCategory = Nothing
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub okButton_Click(sender As Object, e As EventArgs) Handles okButton.Click
        If Not IsNothing(SelectedNode) Then
            isSubCategory = False
            If SelectedNode.BackColor = Color.AntiqueWhite Then
                isSubCategory = True
            End If
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If
    End Sub
    Private Sub ExpantButton_Click(sender As Object, e As EventArgs) Handles ExpantButton.Click
        Try
            Dim expant As Boolean = ExpantButton.Tag
            If ExpantButton.Tag = 0 Then
                ExpantButton.Tag = 1
                ExpantButton.Image = My.Resources.Contraint_Treeview_24
                For Each nodes As TreeNode In TreeViewCatgoria.Nodes
                    nodes.Expand()
                Next
            Else
                ExpantButton.Tag = 0
                ExpantButton.Image = My.Resources.Expant_Treeview_24
                For Each nodes As TreeNode In TreeViewCatgoria.Nodes
                    nodes.Collapse()
                Next
            End If



        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        AvilitaDialogo()
        categoria = "Categoria"
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles MenuStripCatego.Click
        If Not IsNothing(Nodes) Then
            If Nodes.Length > 0 Then
                AvilitaDialogo()
                categoria = "SubCategoria"
                lblCategoria.Text = "[" + Nodes(0).ToString + "] Sub categoría"
                Exit Sub
            End If
        Else
            'si no salio antes dice lo siguiente
            MsgBox("Seleccione dentro de que categoría va agregar", MsgBoxStyle.Information, "Aviso")
        End If
    End Sub

    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        If Not IsNothing(Nodes) Then
            If Nodes.Length > 0 Then
                AvilitaDialogo()
                categoria = "Modifica"
                lblCategoria.Visible = True
                SiguientButton.Enabled = False
                lblCategoria.Text = "Modificando: " + Me.TreeViewCatgoria.SelectedNode.FullPath
                txtNameCategoria.Text = Nodes(Nodes.Length - 1).ToString
            End If
        End If
    End Sub

    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click
        If Elimina_Categoria() Then
            Cargar_Categorias()
            btnCancelCategory.PerformClick()
        End If
    End Sub

    Private Sub SaveButton_Click_1(sender As Object, e As EventArgs) Handles SaveButton.Click
        If Not IsNothing(Nodes) Then
            If Nodes.Length > 1 Then
                If MsgBox(msgSave, MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.Exclamation, msgRespond) = MsgBoxResult.Yes Then
                    If Modifica_Categor_delProdcuto() Then
                        With Me.myPadre
                            .SiguienteButton.Enabled = False
                            .OkButton.Visible = True
                            .OkButton.Enabled = True
                            .OkButton.Text = "Aplicar"
                        End With
                    End If
                End If
            End If
        End If
    End Sub
End Class