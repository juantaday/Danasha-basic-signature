Imports SupabaseDataAccess.Settings
Imports System.Threading.Tasks

Public Class frmConnectionRemoteProfile

    Sub New()
        InitializeComponent()
        Me.txtHost.Text = ""
        Me.txtDatabase.Text = ""
        Me.txtUserName.Text = ""
        Me.txtPassword.Text = ""

        Me.txtHost.ForeColor = System.Drawing.Color.Black
        Me.txtDatabase.ForeColor = System.Drawing.Color.Black
        Me.txtUserName.ForeColor = System.Drawing.Color.Black
        Me.txtPassword.ForeColor = System.Drawing.Color.Black

    End Sub

    ' ─────────────────────────────────────────────
    '  LOAD — cargar credenciales guardadas + panel
    ' ─────────────────────────────────────────────
    Private Async Sub frmConnectionRemoteProfile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Ocultar caracteres password
        txtPassword.UseSystemPasswordChar = True
        txtPassword.Button.Text = If(txtPassword.UseSystemPasswordChar, "👁", "🙈")
        txtPassword.Button.Width = 28
        txtPassword.Button.Height = 22
        txtPassword.Button.Location = New Point(txtPassword.Width - 38, txtPassword.Height - 30)
        txtPassword.Button.Image = Nothing  ' Eliminar imagen predeterminada del botón  
        txtPassword.Button.BringToFront()

        AddHandler txtPassword.KeyDown, AddressOf Password_KeyDown

        ' Si ya existen credenciales guardadas, cargarlas
        If ConnectionCredentials.CredentialsExist() Then
            ShowRunningPanel("Cargando configuración...")
            Await Task.Run(Sub() CargarCredenciales())
            HideRunningPanel()
        End If
    End Sub

    ' ─────────────────────────────────────────────
    '  Cargar credenciales (se ejecuta en hilo aparte)
    ' ─────────────────────────────────────────────
    Private Sub CargarCredenciales()
        Try
            Dim creds = ConnectionCredentials.LoadCredentials()
            ' Volver al hilo UI para actualizar controles
            Me.Invoke(Sub()
                          txtHost.Text = creds.Host
                          txtDatabase.Text = creds.Database
                          txtUserName.Text = creds.Username
                          txtPassword.Text = creds.Password
                      End Sub)
        Catch ex As Exception
            Me.Invoke(Sub()
                          MessageBox.Show("No se pudieron cargar las credenciales guardadas." &
                                          vbNewLine & ex.Message,
                                          "Aviso", MessageBoxButtons.OK,
                                          MessageBoxIcon.Warning)
                      End Sub)
        End Try
    End Sub

    ' ─────────────────────────────────────────────
    '  Bloquear copiar/cortar en password
    ' ─────────────────────────────────────────────
    Private Sub Password_KeyDown(sender As Object, e As KeyEventArgs)
        If e.Control AndAlso (e.KeyCode = Keys.C OrElse e.KeyCode = Keys.X) Then
            e.SuppressKeyPress = True
        End If
    End Sub

    ' ─────────────────────────────────────────────
    '  CANCELAR
    ' ─────────────────────────────────────────────
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    ' ─────────────────────────────────────────────
    '  ACEPTAR — guardar + probar conexión con panel
    ' ─────────────────────────────────────────────
    Private Async Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click

        ' Validar campos vacíos
        If String.IsNullOrWhiteSpace(txtHost.Text) OrElse
           String.IsNullOrWhiteSpace(txtDatabase.Text) OrElse
           String.IsNullOrWhiteSpace(txtUserName.Text) OrElse
           String.IsNullOrWhiteSpace(txtPassword.Text) Then

            MessageBox.Show("Todos los campos son obligatorios.",
                            "Campos vacíos",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            Return
        End If

        ' Capturar valores ANTES del await (hilo UI)
        Dim host As String = txtHost.Text.Trim()
        Dim database As String = txtDatabase.Text.Trim()
        Dim username As String = txtUserName.Text.Trim()
        Dim password As String = txtPassword.Text

        ShowRunningPanel("Guardando credenciales...")

        ' Guardar en hilo aparte
        Dim saved As Boolean = Await Task.Run(Function()
                                                  Return ConnectionCredentials.SaveCredentials(
                                                      host, database, username, password)
                                              End Function)

        If Not saved Then
            HideRunningPanel()
            MessageBox.Show("Error al guardar las credenciales.",
                            "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
            Return
        End If

        ' Probar conexión en hilo aparte
        UpdateRunningPanel("Probando conexión...")

        Dim connected As Boolean = Await Task.Run(Function()
                                                      Return SupabasePgConnection.TestConnection()
                                                  End Function)
        HideRunningPanel()

        If connected Then
            MessageBox.Show("Credenciales guardadas y conexión exitosa.",
                            "Éxito", MessageBoxButtons.OK,
                            MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Else
            MessageBox.Show("Credenciales guardadas pero no se pudo conectar." &
                            vbNewLine & "Verifica los datos ingresados.",
                            "Error de conexión", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
        End If

    End Sub

    ' ─────────────────────────────────────────────
    '  Toggle ver/ocultar password
    ' ─────────────────────────────────────────────
    Private Sub txtPassword_ButtonClick(sender As Object, e As EventArgs) Handles txtPassword.ButtonClick
        txtPassword.UseSystemPasswordChar = Not txtPassword.UseSystemPasswordChar
        txtPassword.Button.Text = If(txtPassword.UseSystemPasswordChar, "👁", "🙈")
    End Sub

    ' ══════════════════════════════════════════════
    '  PANEL RUNNING — mostrar / actualizar / ocultar
    ' ══════════════════════════════════════════════
    Private Sub ShowRunningPanel(mensaje As String)
        ' Deshabilitar controles para evitar doble click
        OK_Button.Enabled = False
        Cancel_Button.Enabled = False
        txtHost.Enabled = False
        txtDatabase.Enabled = False
        txtUserName.Enabled = False
        txtPassword.Enabled = False

        ' Mostrar panel y etiqueta
        pnlRunning.Visible = True
        lblRunningMsg.Text = mensaje
        progressRunning.Style = ProgressBarStyle.Marquee  ' animación infinita
        progressRunning.MarqueeAnimationSpeed = 30

        Me.Refresh()
    End Sub

    Private Sub UpdateRunningPanel(mensaje As String)
        lblRunningMsg.Text = mensaje
        Me.Refresh()
    End Sub

    Private Sub HideRunningPanel()
        pnlRunning.Visible = False

        ' Re-habilitar controles
        OK_Button.Enabled = True
        Cancel_Button.Enabled = True
        txtHost.Enabled = True
        txtDatabase.Enabled = True
        txtUserName.Enabled = True
        txtPassword.Enabled = True
    End Sub

End Class
