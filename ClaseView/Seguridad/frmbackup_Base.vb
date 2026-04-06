Imports System.IO
Imports System.Runtime.CompilerServices

Public Class frmbackup_Base
    Private NameArchiv As String = String.Empty
    Private Sub btnbackup_Click(sender As Object, e As EventArgs) Handles btnbackup.Click
        If String.IsNullOrWhiteSpace(fileNameTextBox.Text) Then
            MessageBox.Show("Debe seleccionar la ubicación del archivo de respaldo.", "Importante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        Dim fullPath As String = fileNameTextBox.Text.Trim()
        Dim directoryPath As String = Path.GetDirectoryName(fullPath)
        Dim backupName As String = Path.GetFileNameWithoutExtension(fullPath)


        If String.IsNullOrWhiteSpace(directoryPath) OrElse String.IsNullOrWhiteSpace(backupName) Then
            MessageBox.Show("La ruta del respaldo no es válida.", "Importante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If Not Directory.Exists(directoryPath) Then
            Directory.CreateDirectory(directoryPath)
        End If

        If File.Exists(fullPath) Then
            Dim fileInfo As New FileInfo(fullPath)

            If fileInfo.Length > 0 Then
                MessageBox.Show("El archivo seleccionado ya tiene datos.", "Importante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return
            End If
        Else
            MessageBox.Show("El archivo  seleccionado no existe.", "Importante", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        Me.Cursor = Cursors.WaitCursor
        Dim result As Boolean = False

        Try
            Timer1.Start()
            Dim fun As New fbackup
            If fun.backupbase(fullPath, backupName) Then
                result = True
                MessageBox.Show("Backup generado satisfactoriamente", "BACKUP BD", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Backup NO PUEDE SER GENERADO", "BACKUP BD", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "ERROR")
        Finally
            Me.Cursor = Cursors.Default
            Timer1.Stop()
            ProgressBar1.Value = 0

            If (result) Then
                Me.Close()
            End If
        End Try

    End Sub

    Private Sub FileButton_Click(sender As Object, e As EventArgs) Handles FileButton.Click
        Try
            Dim saveFileDialog1 As New SaveFileDialog()
            saveFileDialog1.Filter = "Respaldo SQL (*.bak)|*.bak"
            saveFileDialog1.Title = "Seleccione la ubicación del respaldo"
            saveFileDialog1.AddExtension = True
            saveFileDialog1.DefaultExt = "bak"
            saveFileDialog1.OverwritePrompt = True
            saveFileDialog1.FileName = GenerateDefaultBackupName()

            If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                fileNameTextBox.Text = saveFileDialog1.FileName
                NameArchiv = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName)
                Using fs As FileStream = File.Create(saveFileDialog1.FileName)
                    ' no haces nada, solo creas y cierras
                End Using
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Function GenerateDefaultBackupName() As String
        Return $"Backup_{DateTime.Now:yyyyMMdd_HHmm}"
    End Function

    Private Function NormalizeDirectoryPathForSql(directoryPath As String) As String
        If String.IsNullOrWhiteSpace(directoryPath) Then
            Return String.Empty
        End If

        If directoryPath.EndsWith("\\") Then
            Return directoryPath
        End If

        Return directoryPath & "\\"
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If Me.ProgressBar1.Value = 100 Then
            Me.ProgressBar1.Value = 0
        Else
            Me.ProgressBar1.Value += 1
        End If
    End Sub
End Class