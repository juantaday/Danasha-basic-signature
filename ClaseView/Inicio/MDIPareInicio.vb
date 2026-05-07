Imports System.IO
Imports System.Net
Imports CADsisVenta.Class
Imports CADsisVenta.Data.Emuns.EnumSatateModule
Imports CADsisVenta.Data.Models
Imports CADsisVenta.Funtions
Imports CADsisVenta.Helpers
Imports CADsisVenta.Helpers.FInicio
Imports InterfaceSignatureAndSRI.Views

Public Class MDIPareInicio

    Public Sub New()

        ' Llamada necesaria para el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
    End Sub

    Private Sub StatusBarToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles StatusBarToolStripMenuItem.Click
        Me.StatusStrip.Visible = Me.StatusBarToolStripMenuItem.Checked
    End Sub

    Private Sub CascadeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CascadeToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub TileVerticalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileVerticalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub TileHorizontalToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TileHorizontalToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub ArrangeIconsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ArrangeIconsToolStripMenuItem.Click
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CloseAllToolStripMenuItem.Click
        ' Cierre todos los formularios secundarios del principal.
        For Each ChildForm As form_Code In Me.MdiChildren
            If Not ChildForm.Name.Equals("frmPanel") Then
                ChildForm.Close()
            End If
        Next
    End Sub

    Private m_ChildFormNumber As Integer
    Private Sub MDIParent1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            System.Windows.Forms.Application.EnableVisualStyles()
            DatosUsuario()
            Hide_PagesOpenView()

            Me.paneMuestra.Visible = False
            Dim located As Point
            located.Y = 0
            located.X = 0
            Dim formnew As New frmPanel()

            With formnew
                .MdiParent = Me
                .Width = Me.paneMuestra.Width
                .Height = Me.paneMuestra.Height
                .Location = located
                .Show()
            End With


            If FInicio.TerminalActivo.idTerminal = 0 Then
                Using fini As New FunInicio()
                    If Not fini.Inicia_Terminal() Then

                    End If
                End Using

            End If

            StatusLabelUsuario.Text = String.Format("USER:{0} {1} ,TERMINAL: {2}, EQUIPO:[{3}]  DataSource:[{4}]", UsuarioActivo.Apellido, UsuarioActivo.Nombre, TerminalActivo.codTerminal, Dominio._HotName, UsuarioActivo.DataSource)
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
            Me.Close()
        End Try
    End Sub

    Private Sub mnuRecibirTransferencia_Click(sender As Object, e As EventArgs) Handles mnuRecibirTransferencia.Click
        Try
            Dim frm As New frmRecibirTransferencia()
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub mnuListadoGuiasRemision_Click(sender As Object, e As EventArgs) Handles mnuListadoGuiasRemision.Click
        Try
            Dim frm As New frmListadoGuiasRemision()
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub MDIPareInicio_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

    End Sub

    Private Sub DatosUsuario()
        Try
            ValidarPermisosUsuario()
        Catch ex As Exception
            Interaction.MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Public Async Sub ValidarPermisosUsuario()
        Try
            Dim listMenu As List(Of ItemMenu) = Await StoreProcedure.GetListMenuActivated(FInicio.UsuarioActivo.codUser)

            For Each MenuPrincipal As ToolStripMenuItem In MenuStrip1.Items
                MenuPrincipal.Enabled = listMenu.Where(Function(x) x.MenuStripName.Equals(MenuPrincipal.Name)).ToList().Count > 0

                For Each SubMenu In MenuPrincipal.DropDownItems

                    If SubMenu.[GetType]().Equals(GetType(ToolStripMenuItem)) Then
                        SubMenu = CType(SubMenu, ToolStripMenuItem)

                        If SubMenu.HasDropDownItems Then
                            Dim subItem As ToolStripMenuItem = CType(SubMenu, ToolStripMenuItem)
                            subItem.Enabled = listMenu.Where(Function(x) x.DropDownName.Equals(subItem.Name)).ToList().Count > 0

                            If subItem.Text = "Entrar" OrElse subItem.Text = "Salir" OrElse subItem.Text = "Cambiar de Usuario" Then
                                subItem.Enabled = True
                            End If
                        End If
                    End If
                Next

                If MenuPrincipal.Text = "Inicio" Then
                    MenuPrincipal.Enabled = True
                End If
            Next

            Return
        Catch ex As Exception
            Interaction.MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Sub ProcesarSesion()
        Try
            Dim Forms() As form_Code
            Forms = Me.MdiChildren

            For Each form As form_Code In Forms
                If Not form.Name.ToString.Equals("frmPanel") Then
                    form.Close()
                End If
            Next
        Catch ex As Exception
        End Try
        Try
            CambiarDeUsuarioToolStripMenuItem.Text = "Entrar"
            StatusLabelUsuario.Text = "No hay sesión activa"
            Using modalFormLogin As New LoginForm
                modalFormLogin.ShowDialog()
                If modalFormLogin.DialogResult = DialogResult.OK Then
                    CambiarDeUsuarioToolStripMenuItem.Text = "Cambiar de Usuario"
                    StatusLabelUsuario.Text = String.Format("USER:{0} {1} ,TERMINAL: {2}, EQUIPO:[{3}]  DataSource:[{4}]", UsuarioActivo.Apellido, UsuarioActivo.Nombre, TerminalActivo.codTerminal, Dominio._HotName, UsuarioActivo.DataSource)
                End If

                Dim hos As String = Dns.GetHostName()
                If Not hos.Equals(Dominio._HotName) Then
                    If Not Carga_DominioMaquina() Then
                        Me.Close()
                    End If
                End If


                Using fini As New FunInicio()
                    If Not fini.Inicia_Terminal() Then
                        Me.Close()
                    End If
                End Using
                DatosUsuario()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


    Private Sub CambiarDeUsuarioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CambiarDeUsuarioToolStripMenuItem.Click
        Try
            If CambiarDeUsuarioToolStripMenuItem.Text = "Cambiar de Usuario" Then
                If MsgBox("Confirma Cambiar de Usuario?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, "Confirme") = MsgBoxResult.Yes Then
                    ProcesarSesion()
                End If
            ElseIf CambiarDeUsuarioToolStripMenuItem.Text = "Entrar" Then
                ProcesarSesion()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub SalirToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles SalirToolStripMenuItem2.Click
        Me.Close()
    End Sub


    Private Sub OrdenCompraNew_Click(sender As Object, e As EventArgs) Handles OrdenCompraNew.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim inicia As Boolean = False
            With frmAdquisicion
                If .Register_inTerminal() = True Then
                    inicia = True
                    .FechaPedidoDatatime.Value = Now
                    .txtFalg.Text = 1
                    .MdiParent = Me
                    .WindowState = FormWindowState.Maximized
                    .Show()
                End If
            End With
            If inicia = False Then
                frmAdquisicion = Nothing
            End If
            Cursor = Cursors.Default
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub OrdenDeTrabajoToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Try
            Dim idDeclaracion As Integer
            Dim idConsumo As Integer
            'determinamos lass declaraciones posible------
            Using newform = New frmImput_Consumo()
                With newform
                    .ShowDialog()
                    If .DialogResult = System.Windows.Forms.DialogResult.OK Then
                        idDeclaracion = .DeclaracionComboBox.SelectedValue
                        idConsumo = .ConsumoComboBox.SelectedValue
                    Else
                        Return
                    End If
                End With
            End Using
            'Abrimos el formulario con las declaraciones establecidas
            Using newform = New frmAdquisicion
                With newform
                    .iniciado = frmAdquisicion.state.gasto_Personal
                    .Carga_Declaracion()
                    .cmbDeclaracion.SelectedValue = idDeclaracion
                    .Carga_Tipo_Consumo()
                    .cmbItmTipconsumo.SelectedValue = idConsumo
                    .ShowDialog()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub OrdenDeEntregaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrdenCompraToolStripMenuItem.Click
        MsgBox(msgNextVersion, MsgBoxStyle.Exclamation, "Ups..!")
        Return
        Try
            Using newPesido As New frmListPedido()
                With newPesido
                    .ShowDialog()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub ListadoDeOrdenesDeEntregaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EmpleadoListToolStripMenuItem.Click
        Try
            Dim listEmployee = New frmList_Empleados(stateClient.Admin)
            With listEmployee
                .MdiParent = Me
                .WindowState = FormWindowState.Maximized
                .Show()
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub ListadoDeOrdenesDeTrabajoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClinetListToolStripMenuItem.Click
        Try
            Dim listClient As New frmList_clientes(stateClient.User)
            With listClient
                .txtFlag = "Listado"
                .MdiParent = Me
                .Show()
                .WindowState = FormWindowState.Maximized
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ProveedoresToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProveedorListToolStripMenuItem.Click
        Try
            Using frmNew As New frmList_Proveedores(stateLoad.List, stateClient.Cliente)
                With frmNew
                    .ShowDialog()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub DiariaPorCajaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DiariaPorCajaToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim myTerminalActivo As ResponseTerminal = isTerminalHabil(UsuarioActivo.codUser, TerminalActivo.codTerminal)
            If myTerminalActivo.Success Then
                Dim frmVentaNew = New frmVentas()
                With frmVentaNew
                    If .Carga_idStadoCaja(myTerminalActivo.DataDb) Then
                        .MdiParent = Me
                        .Show()
                        .WindowState = FormWindowState.Maximized
                    End If
                End With
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub OrdenDeVentaNoFacturadaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrdenDeVentaNoFacturadaToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.Default
            Using frmList_OrdenVenta
                With frmList_OrdenVenta
                    .ShowDialog()
                End With
            End Using
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub PreciosDeVentasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PreciosDeVentasToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim fraVentas As New frmList_ProductPrecioVenta()

            With fraVentas
                .MdiParent = Me
                .txtFlag.Text = "1"
                .WindowState = FormWindowState.Maximized
                .Show()
            End With
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub UsuariosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UsuariosToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Using frmUsuarios
                With frmUsuarios
                    .ShowDialog()
                End With
            End Using
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub PermisosDeUsuarioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PermisosDeUsuarioToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            With frmPermisos
                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
            End With
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub RespaldarBaseDeDatosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RespaldarBaseDeDatosToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Using frmbackup_Base
                frmbackup_Base.StartPosition = FormStartPosition.CenterParent
                frmbackup_Base.ShowDialog()
            End Using
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub AcercaDeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AcercaDeToolStripMenuItem.Click

        Try
            Using AboutBox
                AboutBox.ShowDialog()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ImpresoraDeTicketToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImpresonaTicketToolStripMenuItem.Click
        Try
            Using frm As New frmOptionPrint
                With frm
                    .StartPosition = FormStartPosition.CenterScreen
                    frm.ShowDialog()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub AdministrarBodegasAlmacenesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdministrarBodegasAlmacenesToolStripMenuItem.Click

        Try
            Using whereHouse As New frmAdd_Almacen
                whereHouse.ShowDialog()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub NumeracionDeFacturaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NumeracionDeFacturaToolStripMenuItem.Click
        Using Form As New frmConfFactura
            Form.ShowDialog()
        End Using
    End Sub
    Private Sub VentasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VentasToolStripMenuItem.Click

    End Sub

    Private Sub AsignacionBodegaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AsignacionBodegaToolStripMenuItem.Click
        Dim message As String = "Ups!" & vbLf & "Disponible en la proxima versión."

        MsgBox(message, MsgBoxStyle.Information, "Aviso")
        Return

        Using Form As New frmBodegas
            Form.ShowDialog()
        End Using
    End Sub

    Private Sub RegistrarEsteEquipoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RegistrarEsteEquipoToolStripMenuItem.Click
        Try
            Dim r As Boolean = False
            Using Form As New frmRegistroEquipo
                With Form
                    .ShowDialog()
                    If .DialogResult = DialogResult.OK Then
                        r = True
                    End If
                End With
            End Using
            If r Then
                Me.notificacion.Visible = True
                Me.notificacion.ShowBalloonTip(2000, "Aviso", "Modificación exitosa", ToolTipIcon.Info)
                Me.notificacion.Visible = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub AdministrarPrecioProductoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdministrarPrecioProductoToolStripMenuItem.Click

        MsgBox("Disponible en la proxima version..", MsgBoxStyle.Information, "Aviso")
        Return


        Try
            Dim Formnew = New frmAdministrarPrecios(stateLoad.List)
            With Formnew
                .MdiParent = Me
                .Show()
                .WindowState = FormWindowState.Maximized
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub



    Private Sub AdministrarCreditoParaClientesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdministrarCreditoParaClientesToolStripMenuItem.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim Neworm As New frmList_clientes(stateClient.Admin)
            With Neworm
                .MdiParent = Me
                .txtFlag = "Creditos"
                .WindowState = FormWindowState.Maximized
                .Show()
            End With

        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Cursor = Cursors.Default
        End Try


    End Sub

    Private Sub CuentasPorCobrarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CuentasPorCobrarToolStripMenuItem.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim listClientCobro As New frmList_DeudaClientes()
            With listClientCobro
                .MdiParent = Me
                .Show()
            End With
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub CuentasPoPagarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CuentasPoPagarToolStripMenuItem.Click
        Try
            Cursor = Cursors.WaitCursor
            Using newform As New frmList_DeudaProveedor()
                With newform
                    .StartPosition = FormStartPosition.CenterScreen
                    .FormBorderStyle = FormBorderStyle.Sizable
                    .ShowDialog()
                End With
            End Using
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Cursor = Cursors.Default
        End Try


    End Sub

    Private Sub ContagtoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContagtoToolStripMenuItem.Click
        Using ContagFrm
            ContagFrm.ShowDialog()
        End Using
    End Sub

    Private Sub FacturasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FacturasToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Using Form As New frmList_Facturas()
                With Form
                    .ShowDialog()
                End With
            End Using
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub SelectToolStripMenuItem_Click(e As ToolStripDropDownItem)
        Try
            Hide_PagesOpenView()
            Dim i As Integer = 0
            Dim index As Integer = 1
            Dim ActiveIndex As Integer = 1
            For Each desactive In Me.MdiChildren
                desactive.Tag = Nothing
            Next

            If Me.MdiChildren.Count > 0 Then
                Dim j As Integer = 0
                Me.ActiveMdiChild.Tag = 1

                For Each ActivateIndex In Me.MdiChildren
                    If Not (ActivateIndex.Tag Is Nothing) Then
                        ActiveIndex = j
                        Exit For
                    End If
                    j += 1
                Next
            End If

            For Each SubMenu In e.DropDownItems
                If i > 6 Then
                    If Not (MenuBarr.Items(i).Name.Contains("ToolStripSeparator")) Then
                        MenuBarr.Items(i).ForeColor = Color.Black
                        MenuBarr.Items(i).Name = "i" & SubMenu.Name
                        MenuBarr.Items(i).Text = SubMenu.Text
                        MenuBarr.Items(i).ToolTipText = SubMenu.Text
                        MenuBarr.Items(i).ImageScaling = SubMenu.ImageScaling
                        MenuBarr.Items(i).Image = SubMenu.Image
                        MenuBarr.Items(i).Visible = True
                        MenuBarr.Items(i).Enabled = SubMenu.Enabled
                        MenuBarr.Items(i).Tag = index
                        If ActiveIndex = index Then
                            MenuBarr.Items(i).ForeColor = Color.White
                            MenuBarr.Items(i).BackColor = System.Drawing.Color.FromArgb(CType(CType(76, Byte), Integer), CType(CType(159, Byte), Integer), CType(CType(139, Byte), Integer))
                        End If
                        index += 1
                    End If
                End If
                i += 1
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub Hide_PagesOpenView()
        Try
            For m = 0 To MenuBarr.Items.Count - 1
                MenuBarr.Items(m).Visible = False
                MenuBarr.Items(m).BackColor = MenuBarr.BackColor
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub Oculta_SubMenu()
        Try
            For m = 0 To MenuBarr.Items.Count - 1
                MenuBarr.Items(m).Visible = False
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub MenuBarr_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuBarr.ItemClicked
        Try
            If Not (e.ClickedItem.Tag Is Nothing) Then
                Dim index As Integer = e.ClickedItem.Tag
                Me.MdiChildren(index).Select()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub
    Private Sub ReporteDeVentasPorCajaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReporteDeVentasPorCajaToolStripMenuItem.Click
        Try
            Dim detailCaja As New FrmVentaXcaja()
            With detailCaja
                .MdiParent = Me
                .WindowState = FormWindowState.Maximized
                .Show()
            End With
        Catch ex As Exception
            MsgBox(ex.Message, "Error")
        End Try
    End Sub

    Private Sub ReporteDeVentasPorClienteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReporteDeVentasPorClienteToolStripMenuItem.Click
        Try
            Dim detaicliente As New FrmVentaXcliente()
            With detaicliente
                .MdiParent = Me
                .WindowState = FormWindowState.Maximized
                .Show()
            End With
        Catch ex As Exception
            MsgBox(ex.Message, "Error")
        End Try
    End Sub
    Private Sub ReporteDeVentasPorProdcutoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReporteDeVentasPorProdcutoToolStripMenuItem.Click
        Try
            Dim detaiproduct As New FrmVentaXProducto()
            With detaiproduct
                .MdiParent = Me
                .WindowState = FormWindowState.Maximized
                .Show()
            End With
        Catch ex As Exception
            MsgBox(ex.Message, "Error")
        End Try
    End Sub
    Private Sub PorEmpleadoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PorEmpleadoToolStripMenuItem.Click
        Try
            Dim detailEmpledo As New FrmVentaXEmpleado()
            With detailEmpledo
                .MdiParent = Me
                .WindowState = FormWindowState.Maximized
                .Show()
            End With
        Catch ex As Exception
            MsgBox(ex.Message, "Error")
        End Try
    End Sub

    Private Sub PorProveedorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PorProveedorToolStripMenuItem.Click
        Try
            Dim detaiproductProvider As New FrmFacturCompProvider()
            With detaiproductProvider
                .MdiParent = Me
                .WindowState = FormWindowState.Maximized
                .Show()
            End With
        Catch ex As Exception
            MsgBox(ex.Message, "Error")
        End Try
    End Sub
    Private Sub PorProdcutoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PorProdcutoToolStripMenuItem.Click
        Try
            Dim detaiproductProduc As New FrmFacturCompProducto()
            With detaiproductProduc
                .MdiParent = Me
                .WindowState = FormWindowState.Maximized
                .Show()
            End With
        Catch ex As Exception
            MsgBox(ex.Message, "Error")
        End Try
    End Sub

    Private Sub PorDocumentoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PorDocumentoToolStripMenuItem.Click
        Try
            Dim factCompraTypeCocument As New FrmFacturCompTypoDocument()
            With factCompraTypeCocument
                .MdiParent = Me
                .WindowState = FormWindowState.Maximized
                .Show()
            End With
        Catch ex As Exception
            MsgBox(ex.Message, "Error")
        End Try
    End Sub
    Private Function WhatIsMyIP() As String

        Dim WhatIsMyIPUrl As String = "http://checkip.dyndns.org/"
        Dim req As HttpWebRequest
        Dim res As HttpWebResponse
        Dim Stream As IO.Stream
        Dim PublicIP As String = String.Empty
        Dim sr As StreamReader

        Try
            req = WebRequest.Create(WhatIsMyIPUrl)
            res = req.GetResponse()
            Stream = res.GetResponseStream()
            sr = New StreamReader(Stream)
            PublicIP = sr.ReadToEnd()
            PublicIP = PublicIP.Replace("<html><head><title>Current IP Check</title></head><body>", "").Replace("</body></html>", "").ToString()
            sr.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
        Return PublicIP
    End Function
    Private Sub CambiarDeContraseñaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CambiarDeContraseñaToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.Default
            Dim dta As New GetInicio
            dta.GUsuario = UsuarioActivo.codUser
            Using newChangePassword As New frmChangePassword(frmChangePassword.stateOperation.changePassword, dta)
                With newChangePassword
                    .StartPosition = FormStartPosition.CenterScreen
                    .ShowDialog()
                End With
            End Using
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub RegistrarEnUnaEstaciónToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RegistrarEnUnaEstaciónToolStripMenuItem.Click
        Try
            Using newOpera As New frm_registerInTerminal
                With newOpera
                    .Operation = _operation.Insert
                    .StartPosition = FormStartPosition.CenterParent
                    .ShowDialog()
                    If .DialogResult = Windows.Forms.DialogResult.OK Then

                        Using fini As New FunInicio()
                            If Not fini.Inicia_Terminal() Then

                            End If

                        End Using
                    End If
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Error")
        End Try
    End Sub

    Private Sub StockDeProductosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StockDeProductosToolStripMenuItem.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim stocProductos As New frmStocProductos()
            With stocProductos
                .MdiParent = Me
                .WindowState = FormWindowState.Maximized
                .Show()
            End With


        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ArqueoDeCajaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ArqueoDeCajaToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Using mycaja As New MDIcajas(stateClient.Cliente)
                With mycaja
                    .ShowDialog()
                    Me.Cursor = Cursors.Default
                End With
            End Using
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub CopyToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles CopyToolStripMenuItem.Click
        Try
            Cursor = Cursors.WaitCursor
            Clipboard.Clear()
            Dim i As Integer
            Dim List As New ListView
            Dim buffer As New System.Text.StringBuilder
            Dim datagrid As New DataGridView
            Dim comboBox As New ComboBox

            Dim cuttentChild As Form = Me.ActiveMdiChild

            If cuttentChild IsNot Nothing AndAlso Not (cuttentChild.Name.ToString.Equals("frmPanel")) Then

                If TypeOf (cuttentChild.ActiveControl) Is TextBox Or TypeOf (cuttentChild.ActiveControl) Is ComboBox Then
                    My.Computer.Clipboard.SetText(cuttentChild.ActiveControl.Text)
                    GoTo Salida
                ElseIf TypeOf (cuttentChild.ActiveControl) _
                      Is ListView Then
                    List = cuttentChild.ActiveControl
                    GoTo Carga_Listview
                ElseIf TypeOf (cuttentChild.ActiveControl) _
                    Is DataGridView Then
                    datagrid = cuttentChild.ActiveControl
                    GoTo Carga_DataGrid
                ElseIf TypeOf (cuttentChild.ActiveControl) _
                    Is BrightIdeasSoftware.ObjectListView Then
                    GoTo Salida
                End If

            End If

            GoTo Salida

Carga_Listview:
            For c = 0 To List.Columns.Count - 1
                buffer.Append(List.Columns(c).Text)
                buffer.Append(vbTab)
            Next
            buffer.Append(vbNewLine)
            For i = 0 To List.Items.Count - 1
                For Col = 0 To List.Columns.Count - 1
                    buffer.Append(List.Items(i).SubItems(Col).Text)
                    buffer.Append(vbTab)
                Next
                buffer.Append(vbNewLine)
            Next
            Clipboard.SetText(buffer.ToString())
            GoTo Salida
Carga_DataGrid:

            For c = 0 To datagrid.Columns.Count - 1
                buffer.Append(datagrid.Columns(c).HeaderText)
                buffer.Append(vbTab)
            Next
            buffer.Append(vbNewLine)
            For i = 0 To datagrid.Rows.Count - 1
                For Col = 0 To datagrid.Columns.Count - 1
                    buffer.Append(datagrid.Rows(i).Cells(Col).Value)
                    buffer.Append(vbTab)
                Next
                buffer.Append(vbNewLine)
            Next
            Clipboard.SetText(buffer.ToString())
            GoTo Salida
Salida:

            Cursor = Cursors.Default
            Exit Sub
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub PasteToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles PasteToolStripMenuItem.Click
        Try
            Dim data As New Object
            data = My.Computer.Clipboard.GetText()
            If (Me.ActiveMdiChild IsNot Nothing) AndAlso (Me.ActiveMdiChild.ActiveControl IsNot Nothing) Then
                If (Me.ActiveMdiChild.ActiveControl.GetType().Name.Equals("TextBox")) Then
                    Dim textbos = CType(Me.ActiveMdiChild.ActiveControl, TextBox)
                    textbos.Text = My.Computer.Clipboard.GetText()
                    textbos.Select(textbos.Text.Length, 0)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub WindowsMenu_Paint(sender As Object, e As PaintEventArgs) Handles WindowsMenu.Paint
        If WindowsMenu.HasDropDownItems Then
            SelectToolStripMenuItem_Click(sender)
        End If
    End Sub

    Private Sub ContadorBillteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ContadorBillteToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Using forcontador As New frmMonedasAdmin
                forcontador.AcceptButton = Nothing
                forcontador.ShowDialog(Me)
            End Using
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


    Private Sub ProductosToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Try
            Dim Form = New frmLista_Producto
            With Form
                .flag = 2
                .MdiParent = Me
                .Show()
                .BringToFront()
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub MDIPareInicio_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        CierraUserActivo()
    End Sub
    Private Function CierraUserActivo() As Boolean
        Try
            Using cierrra As New FunInicio
                Return cierrra.CierraSecion(UsuarioActivo.codUser)
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function


    Private Sub SaldosEnTerminalesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaldosEnTerminalesToolStripMenuItem.Click
        Try
            Using mycaja As New MDIcajas(stateClient.Admin)
                With mycaja
                    .ShowDialog()
                End With
            End Using
        Catch ex As Exception
            MsgBox(ex.Message & " " & ex.StackTrace, MsgBoxStyle.Exclamation, "Error")
        End Try
    End Sub

    Private Sub ListadoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ListadoToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim NewListProdcut = New frmLista_Producto()
            With NewListProdcut
                .flag = 2
                .MdiParent = Me
                .Show()
                .BringToFront()
            End With
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub InventarioToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InventarioToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim inventario = New frmInventario()
            With inventario
                .MdiParent = Me
                .Show()
                .BringToFront()
            End With
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub AdministrarProductosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdministrarProductosToolStripMenuItem.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim Formnew = New frmAdministrarPrecios(stateLoad.List, True)
            With Formnew
                .MdiParent = Me
                .Show()
                .WindowState = FormWindowState.Maximized
            End With
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub UtilidadesUltimosMesesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UtilidadesUltimosMesesToolStripMenuItem.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim formUtilidad = New frmViewUtilidadxMes()
            With formUtilidad
                .MdiParent = Me
                .Show()
                .WindowState = FormWindowState.Maximized
            End With

        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub VentasGeneralesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VentasGeneralesToolStripMenuItem.Click
        Try
            Cursor = Cursors.WaitCursor

            Dim formVentasG = New frmViewVentasGeneral()
            With formVentasG
                .MdiParent = Me
                .Show()
                .WindowState = FormWindowState.Maximized
            End With
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Cursor = Cursors.Default
        End Try


    End Sub



    Private Sub VerConUtilidadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VerConUtilidadToolStripMenuItem.Click

        Try
            Cursor = Cursors.WaitCursor
            Dim viewVentas = New frmReportViewUtilidad()
            With viewVentas
                .MdiParent = Me
                .Show()
                .WindowState = FormWindowState.Maximized
            End With
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub


    Private Sub MiNegocioToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles MiNegocioToolStripMenuItem.Click
        Try
            Using newmyCommerce As New frmMyCommerce
                newmyCommerce.StartPosition = FormStartPosition.CenterScreen
                newmyCommerce.ShowDialog()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub VentasConDescuentoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VentasConDescuentoToolStripMenuItem.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim viewSalesDiscount = New frmSalesWithDiscount()
            With viewSalesDiscount
                .MdiParent = Me
                .Show()
                .WindowState = FormWindowState.Maximized
            End With
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub VentasPorOperaciónToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VentasPorOperaciónToolStripMenuItem.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim viewSalesOperation = New FrmVentaXoperation()
            With viewSalesOperation
                .MdiParent = Me
                .Show()
                .WindowState = FormWindowState.Maximized
            End With
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub DocumentosElectrónicosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DocumentosElectrónicosToolStripMenuItem.Click
        Try
            Cursor = Cursors.WaitCursor
            Dim viewManagerSingature = New CheckEnvoyForm(TerminalActivo.codTerminal)
            viewManagerSingature.MdiParent = Me
            viewManagerSingature.Show()
            viewManagerSingature.WindowState = FormWindowState.Maximized
            viewManagerSingature.[Select]()
        Catch ex As Exception
            Cursor = Cursors.[Default]
            MsgBox(ex.Message & vbCrLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Cursor = Cursors.[Default]
        End Try
    End Sub

    Private Sub AjecutarScripToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AjecutarScripToolStripMenuItem.Click
        Try

            Dim codUserClose As String = String.Empty
            Using forlogin As New LoginForm(stateReturn._response, "cajas")
                With forlogin
                    .StartPosition = FormStartPosition.CenterParent
                    .ShowDialog()
                    If (.DialogResult = DialogResult.OK) Then
                        codUserClose = .UsernameTextBox.Text
                    End If
                End With

            End Using

            If (String.IsNullOrEmpty(codUserClose) OrElse Not codUserClose.Equals("JTADAYMA")) Then
                MsgBox("Usuario no autorizado para esta accion", MsgBoxStyle.Critical, "Error")
                Return
            End If

            Using excecScrip As New UpdateApp.Views.ExecuteScripForm(DomainSQLite.Setting.Configuration.ConectionString)
                excecScrip.StartPosition = FormStartPosition.CenterScreen
                excecScrip.ShowDialog()
            End Using
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub
End Class
