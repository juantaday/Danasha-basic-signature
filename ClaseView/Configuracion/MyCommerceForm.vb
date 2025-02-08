Imports System.ComponentModel
Imports System.IO
Imports System.Threading
Imports CADsisVenta
Imports Domain.Data.Enums
Imports Domain.Extensions
Imports Domain.Models
Imports ec.gob.sri.comprobantes.Enum

Public Class MyCommerceForm
    Private _currentCommerce As CADsisVenta.myCommerce
    Private _signatureOption As CADsisVenta.SignatureOptions
    Private _imagePDFChange As Boolean
    Private _imageTicketChange As Boolean

    Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().


        txtRuc.CharacterCasing = CharacterCasing.Upper
        txtRazonSocial.CharacterCasing = CharacterCasing.Upper
        txtNomComercial.CharacterCasing = CharacterCasing.Upper
        txtCompany.CharacterCasing = CharacterCasing.Upper

        AddHandler rjRadioButton1.CheckedChanged, AddressOf RjRadioButton1_CheckedChanged
        AddHandler rjRadioButton2.CheckedChanged, AddressOf RjRadioButton1_CheckedChanged
        AddHandler rjRadioButton3.CheckedChanged, AddressOf RjRadioButton1_CheckedChanged

        txtHuella.Text = String.Empty
        jmTabControl1.SelectedIndex = 0

    End Sub

    Private Sub MyCommerceForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        jmTabControl1.TabIndex = 0
        rjRadioButton1.Checked = True

        If Not backgroundWorker1.IsBusy Then
            Me.circularProgressBar1.Visible = True
            Me.circularProgressBar1.Value = 0
            Me.circularProgressBar1.Text = "Starting"
            backgroundWorker1.RunWorkerAsync()
        End If
    End Sub


    Private Sub RjRadioButton1_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Not CType(sender, JMControls.Controls.RJRadioButton).Checked Then Return

        Dim mss = String.Empty
        Dim nameControl As String = CType(sender, JMControls.Controls.RJRadioButton).Name

        If nameControl.Equals(rjRadioButton1.Name) Then


            expandCollapsePanel1.IsExpanded = True
            expandCollapsePanel1.Enabled = True
        ElseIf nameControl.Equals(rjRadioButton2.Name) Then


            expandCollapsePanel1.IsExpanded = False
            expandCollapsePanel1.Enabled = False
        ElseIf nameControl.Equals(rjRadioButton3.Name) Then
            MsgBox("Ups!!" & vbLf & "No disponible en esta versión..")
            rjRadioButton1.Checked = True
        End If


    End Sub

#Region "Back run 1"

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

        e.Result = BackgroundProcessLogicMethod(helperBW, arg, e, progress)

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
            Me._currentCommerce = CADsisVenta.Funtions.Funtion.GetMyCommerceFirst()
            progress.Report(New StepsModels() With {.LongState = 65, .DescripState = "Get my commerce options.."
        })
            Me._signatureOption = _currentCommerce.SignatureOptions.FirstOrDefault()
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
            If _currentCommerce IsNot Nothing Then ViewData()
        End If
    End Sub
    Private Sub ViewData()
        Try
            If _currentCommerce.SignatureOptions IsNot Nothing AndAlso _currentCommerce.SignatureOptions.Count > 0 Then
                _signatureOption = _currentCommerce.SignatureOptions.SingleOrDefault()
            End If

            Dim typeRegimen As UShort = _currentCommerce.IdTypeRegimen

            txtRuc.Text = _currentCommerce.Ruc
            txtRazonSocial.Text = _currentCommerce.RazonSocial
            txtNomComercial.Text = _currentCommerce.NameComercial
            txtCompany.Text = _currentCommerce.Company
            txtDirMatriz.Text = _currentCommerce.Domicilio
            txtNumResolucion.Text = _currentCommerce.SpecialTaxNumber
            txtAgentRetenNum.Text = _currentCommerce.AgenteRetencion
            ContabiliteChecBox.Checked = _currentCommerce.KeepAccounting


            If _signatureOption IsNot Nothing Then
                GetDataSourceComboBox()

                If Not String.IsNullOrEmpty(_signatureOption.TOKEN) Then
                    rjRadioButton2.Checked = False
                    rjRadioButton1.Checked = True

                    TokenListComboBox.SelectedItem = TokensValidos.valueOf(_signatureOption.TOKEN)
                End If
                If Not String.IsNullOrEmpty(_signatureOption.RUTA_ARCHIVO) Then
                    rjRadioButton1.Checked = False
                    rjRadioButton2.Checked = True

                    txtHuella.Text = _signatureOption.RUTA_ARCHIVO
                End If

                altoNumericUpDown1.Value = _signatureOption.TIEMPO_ESPERA
                tipoAmbienteComboBox.SelectedItem = ec.gob.sri.comprobantes.Enum.TipoAmbienteEnum.obtenerAmbiente(_signatureOption.TIPO_AMBIENTE.ToString())

            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try

    End Sub

#End Region

    Private Sub jmTabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles jmTabControl1.SelectedIndexChanged

        If jmTabControl1.SelectedIndex = 1 Then
            GetTypeEcommerceComboBox()
        ElseIf jmTabControl1.SelectedIndex = 2 Then
            GetDataSourceComboBox()
        ElseIf jmTabControl1.SelectedIndex = 3 Then
            ViewImage()
        End If
    End Sub


    Private Sub GetDataSourceComboBox()
        If TokenListComboBox.DataSource Is Nothing Then
            TokenListComboBox.DataSource = TokensValidos.values().ToList()
            If Not String.IsNullOrEmpty(_signatureOption?.TOKEN) Then
                Dim token = TokensValidos.obtenerToken(_signatureOption.TOKEN, _signatureOption.THUMBPRINT)
                TokenListComboBox.SelectedItem = token
            End If
        End If

        If tipoAmbienteComboBox.DataSource Is Nothing Then
            tipoAmbienteComboBox.DataSource = ec.gob.sri.comprobantes.Enum.TipoAmbienteEnum.values().ToList()
            If Not String.IsNullOrEmpty(_signatureOption?.TIPO_AMBIENTE.ToString()) Then
                Dim ambiente = ec.gob.sri.comprobantes.Enum.TipoAmbienteEnum.obtenerAmbiente(_signatureOption.TIPO_AMBIENTE.ToString())
                tipoAmbienteComboBox.SelectedItem = ambiente
            End If
        End If
    End Sub


    Private Sub GetTypeEcommerceComboBox()

        Dim listTypes As List(Of SelectListItemModel) = [Enum].GetValues(GetType(Domain.Data.Enums.TypeECommerceEnum)) _
            .Cast(Of Domain.Data.Enums.TypeECommerceEnum)() _
            .Select(Function(c) New SelectListItemModel With {
                .Value = CInt(c),
                .Text = c.GetDisplayName()
            }) _
            .ToList()

        Me.cmbTypeBusiness.DisplayMember = NameOf(SelectListItemModel.Text)
        Me.cmbTypeBusiness.ValueMember = NameOf(SelectListItemModel.Value)
        Me.cmbTypeBusiness.DataSource = listTypes


        If _currentCommerce IsNot Nothing Then
            Me.cmbTypeBusiness.SelectedValue = CInt(_currentCommerce.IdTypeRegimen)
        End If

    End Sub

    Private Sub ViewImage()
        If _currentCommerce?.LogoPDF IsNot Nothing AndAlso _currentCommerce.LogoPDF.Length > 0 AndAlso Me.logoPictureBox.Image Is Nothing Then
            Dim ms As MemoryStream = New System.IO.MemoryStream(_currentCommerce.LogoPDF.ToArray())
            Me.logoPictureBox.Image = Image.FromStream(ms)
            logoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage
        End If

        If _currentCommerce?.LogoTicket IsNot Nothing AndAlso _currentCommerce.LogoTicket.Length > 0 AndAlso Me.logoPictureBox1.Image Is Nothing Then
            Dim ms As MemoryStream = New MemoryStream(_currentCommerce.LogoTicket.ToArray())
            Me.logoPictureBox1.Image = Image.FromStream(ms)
            logoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage
        End If

    End Sub

    Private Sub rjButton1_Click(sender As Object, e As EventArgs) Handles rjButton1.Click
        Try
            Dim imglocation As String

            Using _openFile = New OpenFileDialog()

                _openFile.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif| GIF All (*.*)|*.*"
                If _openFile.ShowDialog() = DialogResult.OK Then
                    imglocation = _openFile.FileName.ToString()
                    logoPictureBox.ImageLocation = imglocation
                    logoPictureBox.SizeMode = PictureBoxSizeMode.Normal
                    _imagePDFChange = True
                End If

            End Using


        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub rjButton2_Click(sender As Object, e As EventArgs) Handles rjButton2.Click
        logoPictureBox.Image = Nothing
        _imagePDFChange = True
    End Sub

    Private Sub rjButton4_Click(sender As Object, e As EventArgs) Handles rjButton4.Click
        Try
            Dim imglocation As String

            Using _openFile = New OpenFileDialog()

                _openFile.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif| GIF All (*.*)|*.*"
                If _openFile.ShowDialog() = DialogResult.OK Then
                    imglocation = _openFile.FileName.ToString()
                    logoPictureBox1.ImageLocation = imglocation
                    logoPictureBox1.SizeMode = PictureBoxSizeMode.Normal
                    _imageTicketChange = True
                End If

            End Using


        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub rjButton3_Click(sender As Object, e As EventArgs) Handles rjButton3.Click
        logoPictureBox1.Image = Nothing
        _imageTicketChange = True
    End Sub

    Private Sub Accep_Button_Click(sender As Object, e As EventArgs) Handles Accep_Button.Click
        Try
            If Not ValidateData() Then
                Return
            End If
        Catch ex As Exception

            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
            Return
        End Try


        Accep_Button.Enabled = False
        Me.Cursor = Cursors.WaitCursor
        lblState.Text = "Procesando su pedido.."
        RunProccessBack()
    End Sub

    Private Function ValidateData() As Boolean

        'tab 1
        If True Then
            ' ruc
            If String.IsNullOrEmpty(txtRuc.Text) Then
                errorProvider1.SetError(txtRuc, "Ingrese el numero de RUC")
                Return False
            Else
                errorProvider1.SetError(txtRuc, "")
            End If

            If txtRuc.Text.Length <> 13 Then
                errorProvider1.SetError(txtRuc, "El ruc debe contener 13 números")
                Return False
            Else
                errorProvider1.SetError(txtRuc, "")
            End If

            'Rozon socila
            If String.IsNullOrEmpty(txtRazonSocial.Text) Then
                errorProvider1.SetError(txtRazonSocial, "Ingrese el nombre o la Razon social")
                Return False
            Else
                errorProvider1.SetError(txtRazonSocial, "")
            End If

            'Nombre comercial
            If String.IsNullOrEmpty(txtNomComercial.Text) Then
                errorProvider1.SetError(txtNomComercial, "Ingrese el nombre comercial")
                Return False
            Else
                errorProvider1.SetError(txtNomComercial, "")
            End If

            'Nombre comercial
            If String.IsNullOrEmpty(txtCompany.Text) Then
                errorProvider1.SetError(txtCompany, "Ingrese un alias referente a la companía para imprimir en ticket..")
                Return False
            Else
                errorProvider1.SetError(txtCompany, "")
            End If


            'Direccion matriz
            If String.IsNullOrEmpty(txtDirMatriz.Text) Then
                errorProvider1.SetError(txtDirMatriz, "Ingrese la dirección pricipal del negocio")
                Return False
            Else
                errorProvider1.SetError(txtDirMatriz, "")
            End If

        End If


        ' Tab 2
        Dim typeECommerce As TypeECommerceEnum = TypeECommerceEnum.Regimen_General

        ' Validar tipo de negocio
        If cmbTypeBusiness.SelectedIndex = -1 Then
            errorProvider1.SetError(cmbTypeBusiness, "Seleccione el tipo de negocio..")
            Return False
        End If
        errorProvider1.SetError(cmbTypeBusiness, "")

        Dim selectedValue As TypeECommerceEnum = CType(cmbTypeBusiness.SelectedValue, TypeECommerceEnum)

        If [Enum].IsDefined(GetType(TypeECommerceEnum), selectedValue) Then
            typeECommerce = CType(selectedValue, TypeECommerceEnum)

            If typeECommerce = TypeECommerceEnum.Regimen_General Then
                txtNumResolucion.Text = Nothing
                txtAgentRetenNum.Text = Nothing

                txtNumResolucion.Enabled = False
                txtAgentRetenNum.Enabled = False
            End If
        Else
            Interaction.MsgBox("Valor fuera de rango..", MsgBoxStyle.Exclamation, "Alerta..")
            Return False
        End If

        ' Validar número de resolución
        If typeECommerce = TypeECommerceEnum.SpecialTaxpayer AndAlso String.IsNullOrEmpty(txtNumResolucion.Text) Then
            errorProvider1.SetError(txtNumResolucion, "Ingrese el número de resolución")
            Return False
        End If
        errorProvider1.SetError(txtNumResolucion, "")

        ' Validar número de resolución para Microenterprise
        If typeECommerce = TypeECommerceEnum.Microenterprise AndAlso String.IsNullOrEmpty(txtAgentRetenNum.Text) Then
            errorProvider1.SetError(txtAgentRetenNum, "Ingrese el número de resolución")
            Return False
        End If
        errorProvider1.SetError(txtAgentRetenNum, "")

        If typeECommerce = TypeECommerceEnum.Regimen_General Then
            txtNumResolucion.Text = Nothing
            txtAgentRetenNum.Text = Nothing
        End If

        ' Validar RIMPE Taxpayer
        If typeECommerce = TypeECommerceEnum.RIMPE_Taxpayer AndAlso String.IsNullOrEmpty(txtRegimenRIMPE.Text) Then
            errorProvider1.SetError(txtRegimenRIMPE, "Determine la etiqueta del régimen")
            Return False
        End If
        errorProvider1.SetError(txtRegimenRIMPE, "")

        ' Tab 3
        If rjRadioButton1.Checked AndAlso (TokenListComboBox.SelectedIndex = -1 OrElse TokenListComboBox.SelectedValue Is Nothing) Then
            jmTabControl1.SelectedIndex = 2
            errorProvider1.SetError(TokenListComboBox, "Seleccione el tipo de token válido para firmar.")
            Return False
        ElseIf rjRadioButton2.Checked Then
            jmTabControl1.SelectedIndex = 2

            Return False
        End If

        If tipoAmbienteComboBox.SelectedIndex = -1 Then
            jmTabControl1.SelectedIndex = 2
            errorProvider1.SetError(tipoAmbienteComboBox, "Seleccione el tipo de ambiente a emitir los documentos..")
            Return False
        End If

        errorProvider1.SetError(tipoAmbienteComboBox, "")
        errorProvider1.SetError(TokenListComboBox, "")

        Dim idTypeTributario As TypeECommerceEnum
        If cmbTypeBusiness.SelectedValue IsNot Nothing Then
            idTypeTributario = CType(cmbTypeBusiness.SelectedValue, TypeECommerceEnum)
        Else
            MessageBox.Show("Seleccione un tipo de negocio válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

        If _currentCommerce Is Nothing Then
            _currentCommerce = New CADsisVenta.myCommerce()
            _currentCommerce.DateStar = Date.Now

            _signatureOption = New CADsisVenta.SignatureOptions()
        End If


        _currentCommerce.Company = txtCompany.Text.Trim()
        _currentCommerce.AgenteRetencion = If(txtAgentRetenNum.Text Is "", Nothing, txtAgentRetenNum.Text.Trim())

        _currentCommerce.IdTypeRegimen = idTypeTributario
        _currentCommerce.KeepAccounting = ContabiliteChecBox.Checked
        _currentCommerce.note = Nothing
        _currentCommerce.SpecialTaxNumber = If(String.IsNullOrWhiteSpace(txtNumResolucion.Text.Trim()), Nothing, txtNumResolucion.Text.Trim())
        _currentCommerce.RegimenMicroempresas = txtRegimenMicro.Text
        _currentCommerce.ContribuyenteRimpe = cmbTypeBusiness.SelectedItem.ToString().Trim()
        _currentCommerce.RazonSocial = txtRazonSocial.Text.Trim()
        _currentCommerce.Ruc = txtRuc.Text
        _currentCommerce.NameComercial = txtNomComercial.Text.Trim()



        If _signatureOption Is Nothing Then
            _signatureOption = New SignatureOptions()
            _currentCommerce.SignatureOptions.Add(_signatureOption)
        End If



        _signatureOption.TOKEN = TokenListComboBox.SelectedItem.ToString()

        _signatureOption.CLAVE_INTERNA = Nothing

        _signatureOption.TIPO_EMISION = "1"

        _signatureOption.TIPO_AMBIENTE = ec.gob.sri.comprobantes.Enum.TipoAmbienteEnum.GetValueByName(tipoAmbienteComboBox.SelectedItem.ToString())

        _signatureOption.THUMBPRINT = If(txtHuella.Text.Trim().Equals(txtHuella.PlaceHolderText.Trim()), Nothing, txtHuella.Text.Trim())

        _signatureOption.TIEMPO_ESPERA = CByte(altoNumericUpDown1.Value)


        'logo pdf
        If True Then
            Dim logo As Byte() = Nothing

            If logoPictureBox.Image IsNot Nothing Then
                Using ms = New MemoryStream()
                    logoPictureBox.Image.Save(ms, logoPictureBox.Image.RawFormat)
                    logo = ms.GetBuffer()
                End Using
            End If
            _currentCommerce.LogoPDF = If(_imagePDFChange, logo, _currentCommerce.LogoPDF)
        End If
        ' logo ticket 
        If True Then
            Dim logo As Byte() = Nothing

            If logoPictureBox1.Image IsNot Nothing Then
                Using ms = New MemoryStream()
                    logoPictureBox1.Image.Save(ms, logoPictureBox1.Image.RawFormat)
                    logo = ms.GetBuffer()
                End Using
            End If
            _currentCommerce.LogoTicket = If(Me._imageTicketChange, logo, _currentCommerce.LogoTicket)
        End If


        Return True
    End Function

    Private Sub RunProccessBack()
        Task.Run(Async Function()
                     Try
                         Dim result As Tuple(Of Boolean, CADsisVenta.myCommerce) = Nothing
                         result = Await CADsisVenta.Funtions.Funtion.SaveAndUpdateECommerceAsync(_currentCommerce)

                         If result.Item1 Then
                             _currentCommerce = result.Item2
                             lblState.Invoke(New MethodInvoker(Sub()
                                                                   Me.lblState.Visible = True
                                                                   Me.lblState.Text = "Guardado exitosamente.."
                                                               End Sub))

                             Me.Invoke(New MethodInvoker(Sub()
                                                             Me.timer1.Start()
                                                         End Sub))

                         End If

                     Catch ex As Exception

                         Dim mss = ""
                         If ex.InnerException?.InnerException?.InnerException IsNot Nothing Then
                             mss = ex.InnerException?.InnerException?.InnerException.Message
                         ElseIf ex.InnerException?.InnerException IsNot Nothing Then
                             mss = ex.InnerException?.InnerException?.Message
                         ElseIf ex.InnerException IsNot Nothing Then
                             mss = ex.InnerException?.Message
                         Else
                             mss = ex.Message
                         End If

                         Me.Invoke(New MethodInvoker(Sub()
                                                         If mss.Contains("UQ_Num_Identity_Client") Then
                                                             MsgBox("El número de identificación ya esta registrado", MsgBoxStyle.Information, "Aviso")
                                                         Else
                                                             MsgBox(mss, MsgBoxStyle.Critical, "Error")
                                                         End If
                                                     End Sub))
                     Finally
                         Me.Invoke(New MethodInvoker(Sub() Me.Cursor = Cursors.[Default]))
                         Accep_Button.Invoke(New MethodInvoker(Sub() Me.Accep_Button.Enabled = True))
                     End Try

                 End Function)

    End Sub

    Private Sub timer1_Tick(sender As Object, e As EventArgs) Handles timer1.Tick
        timer1.Stop()
        lblState.Visible = False
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub cmbTypeBusiness_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTypeBusiness.SelectedIndexChanged
        If String.IsNullOrEmpty(cmbTypeBusiness.DisplayMember) OrElse
     String.IsNullOrEmpty(cmbTypeBusiness.ValueMember) OrElse
     cmbTypeBusiness.SelectedIndex = -1 Then
            Exit Sub
        End If

        Dim selectedValue As TypeECommerceEnum = CType(cmbTypeBusiness.SelectedValue, TypeECommerceEnum)

        If [Enum].IsDefined(GetType(TypeECommerceEnum), selectedValue) Then
            Dim typeECommerce As TypeECommerceEnum = CType(selectedValue, TypeECommerceEnum)

            If typeECommerce = TypeECommerceEnum.Regimen_General Then
                txtNumResolucion.Text = Nothing
                txtAgentRetenNum.Text = Nothing

                txtNumResolucion.Enabled = False
                txtAgentRetenNum.Enabled = False
            End If
        Else
            Interaction.MsgBox("Valor fuera de rango..", MsgBoxStyle.Exclamation, "Alerta..")
        End If
    End Sub
End Class