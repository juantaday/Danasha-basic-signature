Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading
Imports CADsisVenta.Data.Models
Imports CADsisVenta.Statics
Imports DomainSQLite
Imports DomainSQLite.Funtions
Imports DomainSQLite.Models
Imports DomainSQLite.Setting

Public NotInheritable Class SplashScreen1

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

    Private registered As Boolean
    Private _LoginForm As LoginForm
    Private aplication As MDIPareInicio


    Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().

    End Sub
    Private Sub SplashScreen1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Título de la aplicación
        Me.ViewLoadingLabel.Text = String.Empty
        Me.progressBar1.Value = 0

        Try
            If (OnInit() = True) Then

                LoadDashaboar()

                If Not (backgroundWorker1.IsBusy) Then
                    backgroundWorker1.RunWorkerAsync()
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message & vbNewLine & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try


        If My.Application.Info.Title <> "" Then
            ApplicationTitle.Text = My.Application.Info.Title
        Else
            'Si falta el título de la aplicación, utilice el nombre de la aplicación sin la extensión
            ApplicationTitle.Text = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        '
        Version.Text = System.String.Format("{0}.{1}.{2}.{3}", My.Application.Info.Version.Major, My.Application.Info.Version.Minor, My.Application.Info.Version.Build, My.Application.Info.Version.Revision)

        'Información de Copyright
        Copyright.Text = My.Application.Info.Copyright
    End Sub

    Private Async Sub LoadDashaboar()
        Try

            _LoginForm = New LoginForm()
            _LoginForm.StartPosition = FormStartPosition.CenterScreen

            aplication = New MDIPareInicio()
            aplication.WindowState = FormWindowState.Maximized


            Await Task.FromResult(True)

        Catch ex As Exception
            Interaction.MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try


    End Sub

    Private Function OnInit() As Boolean
        Try
            Dim Process As Process = Process.GetCurrentProcess()
            InitTrial(Setting.AppSetting.klibraryKey, Process.MainWindowHandle)

            Return True

        Catch ex As DllNotFoundException
            MessageBox.Show(ex.ToString())
            Process.GetCurrentProcess().Kill()
            Return False
        Catch ex1 As Exception
            MessageBox.Show(ex1.ToString())
            Process.GetCurrentProcess().Kill()
            Return False
        End Try

    End Function

    Private Sub backgroundWorker1_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs) Handles backgroundWorker1.DoWork

        Dim helperBW As BackgroundWorker = TryCast(sender, BackgroundWorker)
        Dim arg As Integer = CInt(e.Argument)

        Dim progress As Progress(Of StepsModels) = New Progress(Of StepsModels)(Sub(op)
                                                                                    Me.ViewLoadingLabel.Invoke(New MethodInvoker(Sub()
                                                                                                                                     Me.ViewLoadingLabel.Text = op.DescripState
                                                                                                                                 End Sub))

                                                                                    Me.progressBar1.Invoke(New MethodInvoker(Sub()
                                                                                                                                 Me.progressBar1.Value = op.LongState
                                                                                                                             End Sub))

                                                                                End Sub)


        e.Result = BackgroundProcessLogicMethod(helperBW, arg, e, progress)

        If helperBW.CancellationPending Then
            e.Cancel = True
        End If

    End Sub

    Private Function BackgroundProcessLogicMethod(ByVal worker As BackgroundWorker,
                                                  ByVal a As Integer,
                                                  ByVal e As DoWorkEventArgs,
                                                  ByVal progress As IProgress(Of StepsModels)) As Integer


        Dim result As Integer = 1
        If worker.CancellationPending Then
            e.Cancel = True
        End If

        Thread.Sleep(25)
        progress.Report(New StepsModels With {.LongState = 5, .DescripState = "Initializing.."})
        worker.ReportProgress(5)
        Thread.Sleep(25)

        Try
            worker.ReportProgress(10)
            progress.Report(New StepsModels With {.LongState = 10, .DescripState = "Get Config.."})
            Thread.Sleep(500)

            Dim cnn = Task(Of Conection).Run(Async Function() As Task(Of Conection)
                                                 Return Await FunctionSQLite.GetDefaultConectionInLine()
                                             End Function).GetAwaiter().GetResult()

            worker.ReportProgress(15)

            If (cnn.Id = 1) Then

                progress.Report(New StepsModels With {.LongState = 15, .DescripState = "Config success"})
                Thread.Sleep(500)
                Configuration.ConectionString = String.Format(
                           "Data Source={0};" +
                           "Initial Catalog={1};Persist Security Info=True;" +
                           "User ID={2};Password={3};",
                           cnn.IpConection, cnn.NameDatabase, cnn.UserId, cnn.Password)

                Configuration.IpServer = cnn.IpConection
            ElseIf (cnn.Id = 2) Then

                progress.Report(New StepsModels With {.LongState = 15, .DescripState = "Config success"})
                Thread.Sleep(25)
                Configuration.ConectionString = cnn.FilePath

                Configuration.IpServer = cnn.IpConection

            Else
                progress.Report(New StepsModels With {.LongState = 15, .DescripState = "Config failred.."})
                Thread.Sleep(25)


                If (_LoginForm Is Nothing) Then
                    _LoginForm = New LoginForm()
                End If

                Dim sql As String = "Conexiones a base de datos con errores"
                sql = sql & vbLf & "Comuníquese con el siseñador del software.."
                Interaction.MsgBox(sql, MsgBoxStyle.Information, "Alert..!!")
                _LoginForm.ShowDialog()
                Application.Exit()
                worker.ReportProgress(100)
                Return 0
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)
        End Try

        progress.Report(New StepsModels With {.LongState = 40, .DescripState = "Connecting to the database"})
        worker.ReportProgress(50)

        Thread.Sleep(5)

        Try

            Using fini As New FunInicio

                progress.Report(New StepsModels With {.LongState = 45, .DescripState = "Get My commerce info.."})

                SettingObject.EcommerceActive = Task.Run(Async Function()
                                                             Return Await fini.GetInfoEcommerce(False)
                                                         End Function).GetAwaiter().GetResult()

                progress.Report(New StepsModels With {.LongState = 55, .DescripState = "Get WareHouse.."})

                SettingObject.WareHouseActive = Task.Run(Async Function()
                                                             Return Await fini.GetBodegas(False)
                                                         End Function).GetAwaiter().GetResult()

                progress.Report(New StepsModels With {.LongState = 65, .DescripState = "Get Option Signature.."})

                SettingObject.SignatureOptios = Task.Run(Async Function()
                                                             Return Await fini.GetOptionSignature(False)
                                                         End Function).GetAwaiter().GetResult()

            End Using


        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)
        End Try


        progress.Report(New StepsModels With {.LongState = 75, .DescripState = "Error with connection.."})

        Thread.Sleep(500)


        progress.Report(New StepsModels With {.LongState = 100, .DescripState = "Laoding dashboar"})
        Thread.Sleep(25)


        Return result

    End Function


    Private Sub BGW_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles backgroundWorker1.RunWorkerCompleted

        If e.Cancelled Then

            ' MessageBox.Show("Operation was canceled")
        ElseIf e.[Error] IsNot Nothing Then
            MessageBox.Show(e.[Error].Message)
        Else
            Me.Hide()

            Try
                _LoginForm.ShowDialog(Me)

                If _LoginForm.DialogResult = DialogResult.OK Then
                    If Carga_DominioMaquina() Then
                        Application.DoEvents()
                        'Application.Run(aplication)
                        aplication.ShowDialog()
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message & vbLet & ex.StackTrace)
            Finally
                Application.Exit()
            End Try

            ' Main()
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not backgroundWorker1.IsBusy Then
            backgroundWorker1.CancelAsync()
        End If
        Application.Exit()
    End Sub

    Private Sub backgroundWorker1_ProgressChanged(sender As Object, e As ComponentModel.ProgressChangedEventArgs) Handles backgroundWorker1.ProgressChanged
        ' Me.progressBar1.Value = e.ProgressPercentage
    End Sub


End Class
