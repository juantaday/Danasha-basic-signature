Imports CADsisVenta.Data.Emuns.EnumSatateModule
Imports CADsisVenta.Helpers
Imports CADsisVenta.Helpers.FInicio
Imports CADsisVenta.Setting
Imports DomainSQLite.Setting

Public Class LoginForm
    Protected Friend setValueFilter As String
    Private IndexInitializerConfig As Integer

    Private stateOpen As stateReturn
    Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub
    Sub New(stateOpen As stateReturn, setValueFilter As String)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        Me.stateOpen = stateOpen
        Me.setValueFilter = setValueFilter
    End Sub

    Dim actualVisible As Boolean = False

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Try
            Me.Cursor = New Cursor(Me.Cursor.Handle)
            Cursor.Position = New Point(Me.Location.X + OK.Location.X + 20, Me.Location.Y + OK.Location.Y + 30)
            Me.Cursor = Cursors.WaitCursor
            If Not Validar() Then
                Exit Sub
            End If
            If IsNothing(Me.stateOpen) Then
                IniciaSecion()
                Return
            End If
            Select Case Me.stateOpen
                Case stateReturn._nothing
                    IniciaSecion()
                    Return
                Case stateReturn._response
                    restunrResponse(Me.setValueFilter)
                    Return
            End Select
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private Sub restunrResponse(setValueFilter As String)
        Dim dts As New GetInicio
        Dim func As New FunInicio

        dts.GUsuario = (UsernameTextBox.Text)
        dts.gContrasena = PasswordTextBox.Text

        If func.IniciarUsuario(dts) Then
            If func.UserAccesoResponse(dts, setValueFilter) Then
                DialogResult = DialogResult.OK
            End If
        End If
    End Sub
    Private Sub IniciaSecion()
        Try
            Dim dts As New GetInicio
            Using func As New FunInicio
                'si no abre la conncion salimos
                If Not (func.connecction.State = ConnectionState.Open) Then
                    Return
                End If

                dts.GUsuario = (UsernameTextBox.Text)
                dts.gContrasena = PasswordTextBox.Text
                sql = dts.gContrasena
                If func.IniciarUsuario(dts) Then
                    If SHA1(dts.GUsuario).ToString.Equals(dts.gContrasena) Then
                        MsgBox("Por su seguridad la contraseña debe ser cambiada", MsgBoxStyle.Exclamation, "Aviso")
                        Using newChangePassword As New _
                                  frmChangePassword(frmChangePassword.stateOperation.runAplicatio, dts)
                            With newChangePassword
                                .StartPosition = FormStartPosition.CenterScreen
                                .ShowDialog()
                                Me.DialogResult = .DialogResult
                            End With
                        End Using
                    End If
                    DialogResult = DialogResult.OK
                End If
            End Using
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        If IsNothing(Me.stateOpen) Then
            FInicio.UsuarioActivo = Nothing
        End If
        If (Me.stateOpen = stateReturn._nothing) Then
            FInicio.UsuarioActivo = Nothing
        End If
        Me.Close()
    End Sub

    Private Sub LoginForm_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        System.Windows.Forms.Application.EnableVisualStyles()
        Me.UsernameTextBox.CharacterCasing = CharacterCasing.Upper
    End Sub

    Private Function Validar() As Boolean
        If UsernameTextBox.Text = "" Then
            UsernameTextBox.Focus()
            Return False
        End If
        If PasswordTextBox.Text = "" Then
            PasswordTextBox.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub UsernameTextBox_TextChanged(sender As System.Object, e As System.EventArgs) Handles UsernameTextBox.TextChanged, PasswordTextBox.TextChanged
        If actualVisible Then
            Me.PictureBox1.Visible = True
            Me.PictureBox2.Visible = False
            actualVisible = False
        Else
            Me.PictureBox1.Visible = False
            Me.PictureBox2.Visible = True
            actualVisible = True
        End If
    End Sub
    Private Sub UsernameTextBox_Leave(sender As Object, e As EventArgs) Handles UsernameTextBox.Leave
        UsernameTextBox.Text = UCase(Trim(UsernameTextBox.Text))
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click, PictureBox1.Click

        Try

            Timer1.Stop()
            IndexInitializerConfig += 1
            If (IndexInitializerConfig = 7) Then

                'Interaction.MsgBox("No disponible en esta versión..", MsgBoxStyle.Information, "Alet..")
                'Return

                Using config As DefautConnectionForm = New DefautConnectionForm()
                    config.ShowDialog()
                    If (config.DialogResult = DialogResult.OK) Then
                        Application.Exit()
                    End If
                End Using
            End If

            Timer1.Start()

        Catch ex As Exception

        End Try

    End Sub

    Private Sub LoginForm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        If (Me.Owner IsNot Nothing AndAlso Me.Owner.Name.Equals("SplashScreen1")) Then
            Me.Owner.Hide()
        End If

    End Sub


End Class
