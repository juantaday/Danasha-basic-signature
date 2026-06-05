Imports System.Runtime.CompilerServices
Imports CADsisVenta.Helpers.FInicio
Imports Domain.Data.Entities
Imports Domain.Data.Repositories
Imports DomainSQLite.Crypto.Encriptador

Public Class frmConfigBodegaAvanzada
    Private ReadOnly _idBodega As Integer
    Private ip As String
    Private password As String
    Private database As String
    Private usuario As String

    Sub New(idBodega As Integer, ip As String, usuario As String, password As String, database As String)

        InitializeComponent()

        _idBodega = idBodega

        Me.ip = ip
        Me.usuario = usuario
        Me.password = password
        Me.database = database
    End Sub



    Private Sub frmConfigBodegaAvanzada_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtIp.Text = DesencriptarValor(Me.ip)
        txtUsuario.Text = Me.usuario
        txtPassword.Text = DesencriptarValor(Me.password)
        txtDatabase.Text = Me.database
    End Sub

    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If String.IsNullOrWhiteSpace(txtIp.Text) OrElse
            String.IsNullOrWhiteSpace(txtUsuario.Text) OrElse
            String.IsNullOrWhiteSpace(txtDatabase.Text) Then
            MsgBox("Complete IP, usuario y base de datos.", MsgBoxStyle.Exclamation, "Aviso")
            Return
        End If

        btnAceptar.Enabled = False
        Me.Cursor = Cursors.WaitCursor
        If (UpdateRemoteConfig() = True) Then
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If

        Me.Cursor = Cursors.Default

    End Sub

    Private Function UpdateRemoteConfig() As Boolean
        Dim ipEnc = EncriptarValor(Me.txtIp.Text.Trim())
        Dim usuarioEnc = Me.txtUsuario.Text.Trim()
        Dim passwordEnc = EncriptarValor(Me.txtPassword.Text.Trim())
        Dim db = Me.txtDatabase.Text.Trim()

        Try
            Return BodegaRepository.UpdateRemoteConfig(_idBodega, ipEnc, usuarioEnc, passwordEnc, db, DomainSQLite.Setting.Configuration.ConectionString)
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
            Return False
        Finally
            btnAceptar.Enabled = True
        End Try
    End Function



    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub


End Class