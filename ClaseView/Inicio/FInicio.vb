Imports System.Runtime.InteropServices
Imports System.Text
Imports CADsisVenta

Public Class Validatios
    <DllImport("Trial.dll", EntryPoint:="ReadSettingsStr", CharSet:=CharSet.Ansi)>
    Private Shared Function InitTrial(ByVal akeyCode As String, ByVal aHWnd As IntPtr) As UInteger
    End Function

    <DllImport("Trial.dll", EntryPoint:="DisplayRegistrationStr", CharSet:=CharSet.Ansi)>
    Private Shared Function DisplayRegistration(ByVal akeyCode As String, ByVal aHWnd As IntPtr) As UInteger
    End Function

    <DllImport("Trial.dll", EntryPoint:="DisplayRegistrationStr", CharSet:=CharSet.Ansi)>
    Private Shared Function GetPropertyValue(ByVal aPropName As String, ByVal aResult As StringBuilder,
      ByRef aResultLen As UInt32) As UInteger

    End Function

    Private Shared klibraryKey As String = "E81BCCCDCD696FDECB9E3D71D9DE976F162FE4C3A51F6D802D51A6606B205B0932D7AAAF6822"
    Private registered As Boolean
    Public Function isloated() As Boolean
        Try
            Dim Proc = Process.GetCurrentProcess()
            Dim ret = InitTrial(klibraryKey, Proc.MainWindowHandle)

            If ret = 0 Then
                Return True
            End If

            Dim val As StringBuilder = New StringBuilder
            Dim len As UInt32 = CType(val.Capacity, UInt32)
            If GetPropertyValue("TrialName", val, len) = 234 Then
                val = New StringBuilder(CType(len, Int32))
                GetPropertyValue("TrialName", val, len)
            End If

            Debug.WriteLine(val)
            Return False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Process.GetCurrentProcess().Kill()
            Return False
        End Try

    End Function
End Class
Module FInicio

    Private Property LoginForm As UserControl

    Public Function SHA1(ByVal strToHash As String) As String
        Using sha1Obj As New Security.Cryptography.SHA1CryptoServiceProvider()
            Dim bytesToHash() As Byte = System.Text.Encoding.ASCII.GetBytes(strToHash)
            bytesToHash = sha1Obj.ComputeHash(bytesToHash)
            Dim strResult As String = ""
            For Each b As Byte In bytesToHash
                strResult += b.ToString("x2")
            Next
            Return strResult
        End Using
    End Function

    Public Sub Main()
        Try
            If My.Forms.LoginForm.ShowDialog = DialogResult.OK Then
                If Carga_DominioMaquina() Then
                    Application.DoEvents()
                    Using aplication As New MDIPareInicio
                        aplication.ShowDialog()
                    End Using
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            Application.Exit()
        End Try
    End Sub 'Main
End Module

Public Structure Terminal
    Public idTerminal As Integer
    Public idBodega As Integer
    Public codTerminal As String
    Public Dominio As String
    Public idCajaStado As Integer
End Structure

Public Structure Usuario
    Public codUser As String
    Public DataSource As String
    Public Apellido As String
    Public Nombre As String
    Public IdUsuario As Integer
End Structure

Public Structure _dominio
    Public _HotName As String
    Public _ip As String
    Public isWep As Boolean
End Structure

Public Structure Cliente
    Public id As Integer
    Public Nombres As String
    Public Ruc As String
    Public Direcc As String
    Public Telf As String
    Public itemsTotal As Integer
    Public Total As Decimal
    Public OtroValor As Decimal
End Structure


Public Module SettinObject
    Public UsuarioActivo As Usuario
    Public TerminalActivo As Terminal
    Public EcommerceActive As myCommerce
    Public Dominio As _dominio
    Public ClienteActivo As Cliente
End Module