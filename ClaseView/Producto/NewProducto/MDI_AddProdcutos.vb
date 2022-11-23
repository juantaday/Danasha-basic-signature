Imports System.Data.SqlClient

Public Class MDI_AddProdcutos
    Private Ultimo_menu As String
    Private m_ChildFormNumber As Integer
    Protected Friend id_Producto As Integer     ' el ID de producto
    Protected Friend id_Presentation As Integer 'IP Presentation del producto
    Protected Friend Estado As Boolean          'es true todavia no se se fa finalizado el proceso de ingreso
    Protected Friend flag As String             'descrión de inicializacion [Agregar] or [Modificar]
    Protected Friend id_seCompra As Integer     'ID que normalmente de compro
    Protected Friend id_seVende As Integer      'ID en la  que normalmente se vende
    Protected Friend id_Proveedor As Integer    'IP proveedor de de la que se viene
    Protected Friend Nom_Comerial As String     'nombre comercial del producto 
    Protected Friend idSubCategorySelect As Integer  'ID cuando selecciona el suB categorya del id
    Protected Friend isActive As Boolean    ' determina si esta activo el productos
    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs) Handles NewToolStripButton.Click
        Using newfor As New frmLista_Producto()
            With newfor
                flag = "Concula"
                .StartPosition = FormStartPosition.CenterScreen
                .FormBorderStyle = FormBorderStyle.Fixed3D
                .Height = 500
                .Width = 800
                .PanelAdmin.Visible = False
                .btnCancelar = .btnCancelar
                .ShowDialog()
            End With
        End Using
    End Sub


    Private Sub menuDescription_Click(sender As Object, e As EventArgs) Handles menuDetalle.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim frmdetalla As New frm_detalle(Me)
            With frmdetalla
                CierroIndesable(.Name)
                .MdiParent = Me
                .WindowState = FormWindowState.Maximized
                .flag = flag
                If id_Producto > 0 Then
                    AtrasButton.Enabled = False
                    SiguienteButton.Enabled = True
                    CancelButon.Enabled = True
                ElseIf id_Producto = 0 Then
                    AtrasButton.Enabled = False
                    SiguienteButton.Enabled = True
                    OkButton.Enabled = False
                    CancelButon.Enabled = True
                End If
                .Show()
                Pinta_Menu(sender.name)
            End With
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub CierroIndesable(ByRef name As String)
        Try
            For i = 0 To Me.MdiChildren.Count - 1
                If Not MdiChildren(i).Name = name Then
                    MdiChildren(i).Close()
                    Exit For
                End If
            Next
            For i = 0 To Me.MdiChildren.Count - 1
                If Not MdiChildren(i).Name = name Then
                    MdiChildren(i).Close()
                    Exit For
                End If
            Next
            For i = 0 To Me.MdiChildren.Count - 1
                If Not MdiChildren(i).Name = name Then
                    MdiChildren(i).Close()
                    Exit For
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message + " Al cerrar el hijo", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub MDI_AddProdcutos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' si no ha determinado al id de producto
        If IsNothing(id_Producto) Then
            id_Producto = 0
        End If
        'Si no se ha determinado el flad
        If IsNothing(flag) Then
            flag = "Lectura"
        End If

        If flag = "Agregar" Then
            id_Producto = 0
        End If
        If id_Producto > 0 Then
            If Not ExisteProducto(id_Producto) Then
                MsgBox("No esxite este producto [ID] : " + Convert.ToString(id_Producto), MsgBoxStyle.Exclamation, "Importante")
                Me.Close()
            End If
            OkButton.Enabled = False
        End If

        ControlMenu()
        If lblProdcutodesc.Text.Contains("Label") Then
            lblProdcutodesc.Text = String.Empty
        End If
    End Sub

    Public Function ExisteProducto(idProducto As Integer) As Boolean
        Try
            sql = "Select top(1) p.idProducto,p.Deft_idPresenVenta, p.Deft_idPresenCompra, p.IdSubCategoria, p.Activo "
            sql = sql & "From [dbo].[Productos] as p Where p.idProducto = " & idProducto & " "

            Using cmd As New CADsisVenta.Funtions.SqlComandExec()
                Dim dt As DataTable = cmd.RetornaTabla(sql)
                If Not IsNothing(dt) Then
                    If dt.Rows.Count > 0 Then
                        Me.idSubCategorySelect = dt(0)("IdSubCategoria")
                        Me.id_seCompra = dt(0)("Deft_idPresenCompra")
                        Me.id_seVende = dt(0)("Deft_idPresenVenta")
                        Me.isActive = dt(0)("Activo")

                        Return True
                    End If
                End If
            End Using

            Return False
        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function
    Private Sub ControlMenu()

        If flag.Equals("Lectura") And id_Producto = 0 Then
            MsgBox("No se ha determinado los parametros necesarios", MsgBoxStyle.Exclamation, "Importante")
            Close()
            Return
        ElseIf flag.Equals("Agregar") And id_Producto = 0 Then
            menuCategory.Enabled = False
            menuPresentacion.Enabled = False
            menuDetalle.PerformClick()
            Estado = True
            Return
        ElseIf flag.Equals("Modificar") And id_Producto > 0 Then
            menuDetalle.Enabled = True
            menuCategory.Enabled = True
            menuPresentacion.Enabled = True
            menuDetalle.PerformClick()
            AtrasButton.Visible = False
            SiguienteButton.Visible = False
            OkButton.Enabled = False
        End If
    End Sub
    Private Sub Pinta_Menu(ByVal NameNemu As String)
        Try
            For i = 0 To MenuStrip1.Items.Count - 1
                If MenuStrip1.Items(i).Name = NameNemu Then
                    MenuStrip1.Items(i).BackColor = Color.Aquamarine
                    Ultimo_menu = NameNemu
                Else
                    MenuStrip1.Items(i).BackColor = MenuStrip1.BackColor
                    If Me.lblProdcutodesc.Text.Contains("Agregando") Then
                        MenuStrip1.Items(i).Enabled = False
                    End If
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


    Private Sub menuCategory_Click(sender As Object, e As EventArgs) Handles menuCategory.Click

        Dim myFormName As String = String.Empty
        Try
            Me.Cursor = Cursors.WaitCursor

            Dim sddv = Me.id_Producto
            Dim frmCategoria As New frm_Categoria(Me)
            myFormName = frmCategoria.Name
            If IsOpenMychildren(myFormName) Then
                frmCategoria = Nothing
                Return
            End If
            With frmCategoria
                .MdiParent = Me
                .WindowState = FormWindowState.Maximized
                If Estado Then
                    menuDetalle.Enabled = False
                    AtrasButton.Enabled = True
                    SiguienteButton.Enabled = True
                    OkButton.Enabled = False
                Else
                    SiguienteButton.Visible = False
                End If
                .Show()
                Pinta_Menu(sender.name)
            End With
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            CierroIndesable(myFormName)
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub menuPresentacion_Click(sender As Object, e As EventArgs) Handles menuPresentacion.Click
        Dim myFormName As String = String.Empty
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim frmPresentacion As New frm_Presentacion(Me, Me.id_Producto)
            myFormName = frmPresentacion.Name
            If IsOpenMychildren(myFormName) Then
                frmPresentacion = Nothing
                Return
            End If
            With frmPresentacion
                myFormName = .Name
                .MdiParent = Me
                .WindowState = FormWindowState.Maximized
                .Show()
                Pinta_Menu(sender.name)
                SiguienteButton.Enabled = True
                OkButton.Enabled = False
                If Not Estado Then
                    SiguienteButton.Visible = False
                End If
                If Me.lblProdcutodesc.Text.Contains("Agregando") Then
                    SiguienteButton.Text = "Siguiente=>"
                End If
            End With
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            CierroIndesable(myFormName)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Function IsOpenMychildren(myNameforms) As Boolean
        Try
            For i = 0 To Me.MdiChildren.Count - 1
                If MdiChildren(i).Name = myNameforms Then
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function


    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles CancelButon.Click
        MyBase.Close()
    End Sub
    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles OkButton.Click
        If Ok_Dialogo() Then
            Estado = False
            DialogResult = DialogResult.OK
        End If
    End Sub

    Private Function Ok_Dialogo() As Boolean
        If Estado Then
            If id_Producto > 0 Then
                Return True
            Else
                MsgBox(msgFalta, MsgBoxStyle.Information, "Aviso")
            End If
        Else
            Return True
        End If
        Return False
    End Function



    Private Sub MDI_AddProdcutos_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Estado Then
            If id_Producto > 0 Then
                If MsgBox("Está seguro de salir sin guardar ésta información", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                    If Elimina_Producto(id_Producto) Then
                        id_Producto = 0
                        Close()
                    End If
                Else
                    e.Cancel = True
                End If
            End If
        End If
    End Sub

    Private Sub menuProveedor_Click(sender As Object, e As EventArgs)
        Dim frmProveedor As New frm_Proveedor(Me)
        With frmProveedor
            CierroIndesable(.Name)
            .MdiParent = Me
            .id_producto = id_Producto
            .WindowState = FormWindowState.Maximized
            .Show()
            Pinta_Menu(sender.name)
            SiguienteButton.Enabled = False
        End With
    End Sub
    Private Sub SiguienteButton_Click(sender As Object, e As EventArgs) Handles SiguienteButton.Click
        Try
            If MdiChildren.Count > 0 Then
                Dim bottoonmy As Button
                Select Case MdiChildren(0).Name
                    Case "frm_detalle"
                        If id_Producto > 0 Then
                            bottoonmy = MdiChildren(0).Controls("SaveButton")
                            bottoonmy.Enabled = True
                        Else
                            bottoonmy = MdiChildren(0).Controls("SigienteButton")
                        End If
                        bottoonmy.PerformClick()
                        If Me.lblProdcutodesc.Text.Contains("Agregando") Then
                            menuCategory.Enabled = True
                            menuCategory.PerformClick()
                        End If
                    Case "frm_Categoria"
                        If Estado Then
                            MdiChildren(0).Update()
                            bottoonmy = MdiChildren(0).Controls("SiguientButton")
                            bottoonmy.PerformClick()
                            menuCategory.Enabled = False
                            menuPresentacion.Enabled = True
                            menuPresentacion.PerformClick()
                        Else
                            bottoonmy = MdiChildren(0).Controls("SaveButton")
                            bottoonmy.PerformClick()
                        End If
                    Case "frm_Presentacion"
                        If Estado Then
                            bottoonmy = MdiChildren(0).Controls("SiguientButton")
                            bottoonmy.PerformClick()
                            If (bottoonmy.Tag = 0) Then 'si no se ha determinado las fomas mas usuales de compra y venta
                                Return
                            End If
                            menuPresentacion.Enabled = False
                            OkButton.Enabled = True
                            OkButton.PerformClick()
                        Else
                            bottoonmy = MdiChildren(0).Controls("SaveButton")
                            bottoonmy.PerformClick()
                        End If
                    Case "frm_Presentacion"
                End Select
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub AtrasButton_Click(sender As Object, e As EventArgs) Handles AtrasButton.Click
        If MdiChildren.Count > 0 Then
            OkButton.Enabled = False
            Select Case MdiChildren(0).Name
                Case "frm_Categoria"
                    menuDetalle.Enabled = True
                    menuDetalle.PerformClick()
                    menuCategory.Enabled = False
                Case "frm_Presentacion"
                    menuCategory.Enabled = True
                    menuCategory.PerformClick()
                    menuPresentacion.Enabled = False
            End Select
        End If
    End Sub

    Private Sub MDI_AddProdcutos_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            Using cnn As New SqlConnection(SimpleDataApp.Utility.GetConnectionString())
                cnn.Open()
                Using cmd As New SqlCommand("[dbo].[prcDeleteProducMalEnter]", cnn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
End Class
