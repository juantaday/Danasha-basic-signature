Imports System.ComponentModel
Imports System.IO
Imports System.Threading
Imports CADsisVenta
Imports CADsisVenta.Statics
Imports Domain.Models
Imports InterfaceSignatureAndSRI.Helpers

Public Class SettingImageLogoForm
    Private ReadOnly _myCommerceId As Integer
    Private _currentMySetting As CADsisVenta.MySetting

    Sub New(myCommerceId As Integer)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()


        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

        Me._myCommerceId = myCommerceId


    End Sub
    Private Sub SettingImageLogoForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If (Not backgroundWorker1.IsBusy) Then
            Me.circularProgressBar1.Visible = True
            Me.circularProgressBar1.Text = "Starting..."
            Me.circularProgressBar1.Value = 0
            backgroundWorker1.RunWorkerAsync()
        End If
    End Sub





#Region "RunBack1"

    Private Sub backgroundWorker1_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles backgroundWorker1.DoWork

        Dim helperBW As BackgroundWorker = TryCast(sender, BackgroundWorker)
        Dim arg = Convert.ToInt32(e.Argument)

        Dim progress As Progress(Of StepsModels) = New Progress(Of StepsModels)(Sub(op)
                                                                                    If Not helperBW.CancellationPending Then
                                                                                        Me.circularProgressBar1.Invoke(New MethodInvoker(Sub()
                                                                                                                                             Me.circularProgressBar1.Value = op.LongState
                                                                                                                                             Me.circularProgressBar1.Text = op.DescripState
                                                                                                                                         End Sub))
                                                                                    End If

                                                                                End Sub)

        e.Result = Me.BackgroundProcessLogicMethod(helperBW, arg, e, progress)

        If helperBW.CancellationPending Then e.Cancel = True

    End Sub

    Private Function BackgroundProcessLogicMethod(ByVal worker As BackgroundWorker, ByVal a As Integer, ByVal e As DoWorkEventArgs, ByVal progress As IProgress(Of StepsModels)) As Integer
        Dim result = 1

        If worker.CancellationPending Then e.Cancel = True

        progress.Report(New StepsModels() With {
        .LongState = 5,
        .DescripState = "Reading connection.."
    })

        Thread.Sleep(25)
        Try
            'loading the zone list -.......
            progress.Report(New StepsModels() With {
            .LongState = 45,
            .DescripState = "Get my commerce"
        })
            Me._currentMySetting = CADsisVenta.Funtions.Funtion.GetMySetting(_myCommerceId)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

        Thread.Sleep(25)
        progress.Report(New StepsModels() With {
        .LongState = 75,
        .DescripState = "Reading the list data."
    })


        Thread.Sleep(25)
        progress.Report(New StepsModels() With {
        .LongState = 100,
        .DescripState = "Full succes.."
    })

        Thread.Sleep(25)

        Return result
    End Function

    Private Sub backgroundWorker1_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs) Handles backgroundWorker1.RunWorkerCompleted
        Me.circularProgressBar1.Visible = False
        If e.Cancelled Then
        ElseIf e.[Error] IsNot Nothing Then
            Interaction.MsgBox(e.[Error].Message & vbLf + e.[Error].StackTrace, MsgBoxStyle.Critical, "Error")
        Else
            If _currentMySetting IsNot Nothing Then
                ViewData()
            End If
        End If
    End Sub

    Private Sub ViewData()
        Try
            txtCompanyName.Text = _currentMySetting.CompanyName
            txtPhone.Text = _currentMySetting.Phone
            txtCellPhone.Text = _currentMySetting.CellPhone

            If _currentMySetting.ImageLogo IsNot Nothing AndAlso _currentMySetting.ImageLogo.Length > 0 Then
                Using ms As MemoryStream = New MemoryStream(_currentMySetting.ImageLogo.ToArray())
                    pictureBox1.Image = Image.FromStream(ms)
                    pictureBox1.SizeMode = PictureBoxSizeMode.Normal
                End Using
            End If

            Dim enc As µ = New µ()

            EmailTextBox.Text = _currentMySetting.Email
            PasswordTextBox.Text = enc.decrypt(_currentMySetting.Password)
            ServidorTextBox.Text = _currentMySetting.SMTP
            PuertoTextBox.Text = _currentMySetting.Port
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

    Private Sub button1_Click(sender As Object, e As EventArgs) Handles button1.Click
        Using fileDialog As OpenFileDialog = New OpenFileDialog()
            fileDialog.Filter = "JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|GIF All (*.*)|*.*"
            fileDialog.Title = "Seleccione el logo."
            If fileDialog.ShowDialog(Me) = DialogResult.OK Then
                pictureBox1.ImageLocation = fileDialog.FileName.ToString()
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal
            End If
        End Using
    End Sub

    Private Sub DeleteImageButton_Click(sender As Object, e As EventArgs) Handles DeleteImageButton.Click
        pictureBox1.Image = Nothing
    End Sub

    Private Sub pictureBox1_SizeChanged(sender As Object, e As EventArgs) Handles pictureBox1.SizeChanged
        DeleteImageButton.Visible = True
    End Sub

    Private Sub button_Cancel_Click(sender As Object, e As EventArgs) Handles button_Cancel.Click
        Me.Close()
    End Sub

    Private Async Sub button_Accep_Click(sender As Object, e As EventArgs) Handles button_Accep.Click
        Dim imageByt As Byte() = Nothing
        Dim sql = String.Empty

        Dim enc As µ = New µ()

        Dim passEncodig As String = enc.encrypt(PasswordTextBox.Text)
        Try
            Dim port = 0
            If Not Integer.TryParse(PuertoTextBox.Text, port) Then
                MessageBox.Show("Perto no valido..")
                Return
            End If


            If pictureBox1.Image IsNot Nothing Then
                Using ms = New MemoryStream()
                    pictureBox1.Image.Save(ms, Drawing.Imaging.ImageFormat.Png)
                    imageByt = ms.GetBuffer()
                End Using
            End If

            If imageByt Is Nothing Then
                MsgBox("No hay imagen para guardar")
                Return
            End If

            If String.IsNullOrEmpty(Me.txtCompanyName.Text.Trim()) Then

                MsgBox("Debe ingresar en nombre de la companía.")
                Return
            End If


            If _currentMySetting Is Nothing Then
                _currentMySetting = New MySetting()
                _currentMySetting.MyCommerceId = SettingObject.EcommerceActive.CommerceId
            End If

            _currentMySetting.SMTP = ServidorTextBox.Text.Trim()
            _currentMySetting.Password = passEncodig
            _currentMySetting.Port = PuertoTextBox.Text.Trim()
            _currentMySetting.Email = EmailTextBox.Text.Trim()
            _currentMySetting.CompanyName = txtCompanyName.Text.Trim().ToUpper()
            _currentMySetting.ImageLogo = imageByt
            _currentMySetting.MyCommerceId = _myCommerceId
            _currentMySetting.Phone = txtPhone.Text.Trim()
            _currentMySetting.CellPhone = txtCellPhone.Text.Trim()

            Dim restult = Await CADsisVenta.Funtions.Funtion.SaveAndUpdateMySettingAync(_currentMySetting)
            If restult.Item1 Then
                _currentMySetting.MySettingID = restult.Item2
                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace)
        End Try

    End Sub


#End Region


End Class