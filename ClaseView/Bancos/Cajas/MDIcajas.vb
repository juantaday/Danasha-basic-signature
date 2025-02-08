Imports CADsisVenta.Data.Emuns.EnumSatateModule
Imports CADsisVenta.Helpers.FInicio
Public Class MDIcajas
    Private cliente As stateClient
    Sub New(ByVal cliente As stateClient)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        Me.cliente = cliente
    End Sub
    Private Sub MycajaLabel_Click(sender As Object, e As EventArgs)
        If sender.tag = 1 Then
            PanelUser.Height = sender.Height
            sender.tag = 0
        Else
            If sender.tag = 0 Then
                PanelUser.Height = 140
                sender.tag = 1
            End If
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)
        If sender.tag = 1 Then
            Panel1.Height = sender.Height
            sender.tag = 0
        Else
            If sender.tag = 0 Then
                Panel1.Height = 140
                sender.tag = 1
            End If
        End If
    End Sub

    Private Sub CollapMycajaButton_Click(sender As Object, e As EventArgs) Handles CollapMycajaButton.Click
        CollapCotrol(sender)
    End Sub
    Sub CollapCotrol(sender As Object)
        Dim _control As System.Windows.Forms.Panel = sender.Parent
        Try
            If sender.tag = 0 Then
                sender.Image = DanashaBasicSignature.My.Resources.Resources.hamburger_22_white
                _control.Height = sender.Height
                _control.BackColor = Color.DimGray
                sender.BackColor = Color.DimGray
                sender.tag = 1
            Else
                sender.Image = DanashaBasicSignature.My.Resources.Resources.hamburger_22_Down_white
                _control.Height = 140
                _control.BackColor = PanelMenu.BackColor
                sender.BackColor = Color.Black
                sender.tag = 0
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub MySaldoButton_Click(sender As Object, e As EventArgs) Handles MySaldoButton.Click
        Try
            If IsNothing(TerminalActivo.idTerminal) Then
                MsgBox("No se ha tederminado el terminal", MsgBoxStyle.Exclamation, "Aviso")
                Return
            End If

            If Not (IsNumeric(TerminalActivo.idTerminal)) Then
                MsgBox("No se ha tederminado el terminal", MsgBoxStyle.Exclamation, "Aviso")
                Return
            End If
            Me.Cursor = Cursors.WaitCursor
            CierroIndeseales(String.Empty)

            Dim stateTerminal As New FrmSaldo_caja(TerminalActivo.idTerminal)
            With stateTerminal
                .MdiParent = Me
                .codTerminal = TerminalActivo.codTerminal
                .WindowState = FormWindowState.Maximized
                .Show()
            End With
            pintaControl(sender)
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & " " & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub MDIcajas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim formnew As New System.Windows.Forms.Form With {
            .Name = "frmPanelCaja",
            .WindowState = FormWindowState.Maximized,
            .BackColor = Color.White}
            With formnew
                .MdiParent = Me
                .Show()
            End With
            AddHandler formnew.FormClosing, AddressOf FormClosing_formnew
            ' Cierra collapsible
            Admin_Collapsible()
            If Me.cliente = stateClient.Cliente Then
                MySaldoButton.PerformClick()
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub FormClosing_formnew(sender As Object, e As FormClosingEventArgs)
        Try
            e.Cancel = True
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical, "Error")
        End Try
    End Sub

    Private Sub CollapAdminButton_Click(sender As Object, e As EventArgs) Handles CollapAdminButton.Click
        CollapCotrol(sender)
    End Sub

    Private Sub Admin_Collapsible()
        CollapCotrol(CollapMycajaButton)
        CollapCotrol(CollapAdminButton)
        CollapCotrol(CollapVirtualButton)
    End Sub

    Private Sub CollapVirtualButton_Click(sender As Object, e As EventArgs) Handles CollapVirtualButton.Click
        MsgBox("Disponible en proxima versión..", MsgBoxStyle.Exclamation, "Ups..!")
        Return
        CollapCotrol(sender)
    End Sub
    Private Sub pintaControl(sender As Object)
        Try
            For Each _control In PanelMenu.Controls
                If TypeOf (_control) Is Panel Then
                    For Each _control2 In _control.Controls
                        If _control2.NAME = sender.NAME Then
                            _control2.BackColor = Color.Blue
                        ElseIf _control2.name.ToString.Contains("Collap") Then
                        Else
                            _control2.BackColor = PanelUser.BackColor
                        End If
                    Next
                ElseIf _control.name = sender.Name Then
                    _control.BackColor = Color.Blue
                ElseIf _control.name.ToString.Contains("Collap") Then
                    sql = sql
                Else
                    _control.BackColor = PanelUser.BackColor
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub MovimButton_Click(sender As Object, e As EventArgs) Handles MovimButton.Click
        pintaControl(sender)
    End Sub

    Private Sub UltCierreButton_Click(sender As Object, e As EventArgs) Handles UltCierreButton.Click
        pintaControl(sender)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            CierroIndeseales(String.Empty)
            Dim frmCaja_stado As New frmSaldo_Terminales(Me)
            With frmCaja_stado
                .MdiParent = Me
                .WindowState = FormWindowState.Maximized
                .Show()
            End With
            pintaControl(sender)
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & " " & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub CierroIndeseales(ByVal notClosedName As String)
        Try
            For Each forms In Me.MdiChildren
                If Not forms.Name = notClosedName Then
                    forms.Close()
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            CierroIndeseales(String.Empty)
            Dim frmCaja_stado As New frmClosedTerminales()
            With frmCaja_stado
                .MdiParent = Me
                .WindowState = FormWindowState.Maximized
                .Show()
            End With
            pintaControl(sender)
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message & " " & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
End Class