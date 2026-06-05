Imports System.Data.SqlClient
Imports CADsisVenta.Statics

Public Class frmMyCommerce
    Private CommerceId As Integer
    Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        CommerceId = 0
    End Sub
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        If GuardaDatos() Then

            Using fini As New FunInicio
                SettingObject.EcommerceActive = Task.Run(Async Function()
                                                             Return Await fini.GetInfoEcommerce(False)
                                                         End Function).GetAwaiter().GetResult()
            End Using

            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End If

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Private Function GuardaDatos() As Boolean
        Try
            If Not validateData() Then
                Return False
            End If
            If CommerceId = 0 Then
                Return AddInformation()
            Else
                Return UpdateInformation()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try

    End Function

    Private Function validateData() As Boolean
        If String.IsNullOrEmpty(razonSoacialTextBox.Text) Then
            MsgBox("Ingrese razon social", MsgBoxStyle.Exclamation, "Importante")
            razonSoacialTextBox.Focus()
            Return False
        End If
        If String.IsNullOrEmpty(rucTextBox.Text) Then
            MsgBox("Ingrese ruc", MsgBoxStyle.Exclamation, "Importante")
            rucTextBox.Focus()
            Return False
        End If
        Return True
    End Function

    Private Function UpdateInformation() As Boolean
        Try

            sql = "Update [cmc].[myCommerce] set "
            sql = sql & "RazonSocial=@RazonSocial,Ruc=@Ruc,lema=@lema,Phone=@Phone, IsCancelInSalesNotStock = @IsCancelInSalesNotStock,"
            sql = sql & "DateStar=@DateStar,Domicilio=@Domicilio,Representante=@Representante,note=@note , NameComercial=@NameComercial "
            sql = sql & "Where ((CommerceId =@CommerceId))"



            Using cnn As New SqlConnection(SimpleDataApp.Utility.GetConnectionString)
                cnn.Open()
                Using cmd As New SqlCommand(sql, cnn)
                    cmd.Parameters.Add("@RazonSocial", SqlDbType.VarChar, 80)
                    cmd.Parameters.Add("@Ruc", SqlDbType.VarChar, 13)
                    cmd.Parameters.Add("@lema", SqlDbType.VarChar, 100)
                    cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 50)
                    cmd.Parameters.Add("@DateStar", SqlDbType.Date)
                    cmd.Parameters.Add("@Domicilio", SqlDbType.VarChar, 150)
                    cmd.Parameters.Add("@Representante", SqlDbType.VarChar, 100)
                    cmd.Parameters.Add("@note", SqlDbType.VarChar, 255)
                    cmd.Parameters.Add("@CommerceId", SqlDbType.Int)
                    cmd.Parameters.Add("@IsCancelInSalesNotStock", SqlDbType.Bit)
                    cmd.Parameters.Add("@NameComercial", SqlDbType.VarChar)

                    cmd.Parameters("@RazonSocial").Value = razonSoacialTextBox.Text
                    cmd.Parameters("@Ruc").Value = rucTextBox.Text
                    'para lema
                    If String.IsNullOrEmpty(lemaTextBox.Text) Then
                        cmd.Parameters("@lema").Value = Global.System.DBNull.Value
                    Else
                        cmd.Parameters("@lema").Value = lemaTextBox.Text
                    End If
                    'para telefomo
                    If String.IsNullOrEmpty(phoneTextBox.Text) Then
                        cmd.Parameters("@Phone").Value = Global.System.DBNull.Value
                    Else
                        cmd.Parameters("@Phone").Value = phoneTextBox.Text
                    End If

                    'para fecha de apertura
                    If DateStar.Checked Then
                        cmd.Parameters("@DateStar").Value = DateStar.Value
                    Else
                        cmd.Parameters("@DateStar").Value = Global.System.DBNull.Value
                    End If

                    'para domicilio
                    If String.IsNullOrEmpty(DomicilioTextBox.Text) Then
                        cmd.Parameters("@Domicilio").Value = Global.System.DBNull.Value
                    Else
                        cmd.Parameters("@Domicilio").Value = DomicilioTextBox.Text
                    End If


                    'para representate
                    If String.IsNullOrEmpty(representanteTextBox.Text) Then
                        cmd.Parameters("@Representante").Value = Global.System.DBNull.Value
                    Else
                        cmd.Parameters("@Representante").Value = representanteTextBox.Text
                    End If
                    'para note
                    If String.IsNullOrEmpty(NoteTextBox.Text) Then
                        cmd.Parameters("@note").Value = Global.System.DBNull.Value
                    Else
                        cmd.Parameters("@note").Value = NoteTextBox.Text
                    End If

                    cmd.Parameters("@IsCancelInSalesNotStock").Value = Me.CancelSalesCheckBox.Checked
                    cmd.Parameters("@CommerceId").Value = Me.CommerceId
                    cmd.Parameters("@NameComercial").Value = Me.txtNomComercial.Text

                    If cmd.ExecuteNonQuery() = 1 Then
                        Return True
                    Else
                        Return False
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function

    Private Function AddInformation() As Boolean
        Try
            sql = "Insert [cmc].[myCommerce] "
            sql = sql & "(RazonSocial,Ruc,lema,Phone,DateStar,Domicilio,Representante,note,IsCancelInSalesNotStock, NameComercial) "
            sql = sql & "Values (@RazonSocial, @Ruc,@lema,@Phone,@DateStar,@Domicilio,@Representante,@note,@IsCancelInSalesNotStock,@NameComercial) "
            Using cnn As New SqlConnection(SimpleDataApp.Utility.GetConnectionString)
                cnn.Open()
                Using cmd As New SqlCommand(sql, cnn)
                    cmd.Parameters.Add("@RazonSocial", SqlDbType.VarChar, 80)
                    cmd.Parameters.Add("@Ruc", SqlDbType.VarChar, 13)
                    cmd.Parameters.Add("@lema", SqlDbType.VarChar, 100)
                    cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 50)
                    cmd.Parameters.Add("@DateStar", SqlDbType.Date)
                    cmd.Parameters.Add("@Domicilio", SqlDbType.VarChar, 150)
                    cmd.Parameters.Add("@Representante", SqlDbType.VarChar, 100)
                    cmd.Parameters.Add("@note", SqlDbType.VarChar, 255)
                    cmd.Parameters.Add("@NameComercial", SqlDbType.VarChar, 30)
                    cmd.Parameters.Add("@IsCancelInSalesNotStock", SqlDbType.Bit)

                    cmd.Parameters("@RazonSocial").Value = razonSoacialTextBox.Text
                    cmd.Parameters("@Ruc").Value = rucTextBox.Text
                    'para lema
                    If String.IsNullOrEmpty(lemaTextBox.Text) Then
                        cmd.Parameters("@lema").Value = Global.System.DBNull.Value
                    Else
                        cmd.Parameters("@lema").Value = lemaTextBox.Text
                    End If
                    'para telefomo
                    If String.IsNullOrEmpty(phoneTextBox.Text) Then
                        cmd.Parameters("@Phone").Value = Global.System.DBNull.Value
                    Else
                        cmd.Parameters("@Phone").Value = phoneTextBox.Text
                    End If

                    'para fecha de apertura
                    If DateStar.Checked Then
                        cmd.Parameters("@DateStar").Value = DateStar.Value
                    Else
                        cmd.Parameters("@DateStar").Value = Global.System.DBNull.Value
                    End If

                    'para domicilio
                    If String.IsNullOrEmpty(DomicilioTextBox.Text) Then
                        cmd.Parameters("@Domicilio").Value = Global.System.DBNull.Value
                    Else
                        cmd.Parameters("@Domicilio").Value = DomicilioTextBox.Text
                    End If


                    'para representate
                    If String.IsNullOrEmpty(representanteTextBox.Text) Then
                        cmd.Parameters("@Representante").Value = Global.System.DBNull.Value
                    Else
                        cmd.Parameters("@Representante").Value = representanteTextBox.Text
                    End If
                    'para note
                    If String.IsNullOrEmpty(NoteTextBox.Text) Then
                        cmd.Parameters("@note").Value = Global.System.DBNull.Value
                    Else
                        cmd.Parameters("@note").Value = NoteTextBox.Text
                    End If

                    cmd.Parameters("@IsCancelInSalesNotStock").Value = Me.CancelSalesCheckBox.Checked
                    cmd.Parameters("@NameComercial").Value = Me.txtNomComercial.Text

                    If cmd.ExecuteNonQuery() = 1 Then
                        Return True
                    Else
                        Return False
                    End If
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function
    Private Sub frmMyCommerce_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargaDatos()
    End Sub
    Private Sub CargaDatos()
        Try
            sql = "Select top(1) * from [cmc].[myCommerce];"
            Using cnn As New SqlConnection(SimpleDataApp.Utility.GetConnectionString)
                cnn.Open()
                Using cmd As New SqlCommand(sql, cnn)
                    Using datp As New SqlDataAdapter(cmd)
                        Dim dt As New DataTable
                        datp.Fill(dt)
                        If dt.Rows.Count > 0 Then
                            CommerceId = dt.Rows(0)("CommerceId").ToString
                            razonSoacialTextBox.Text = dt.Rows(0)("RazonSocial").ToString
                            rucTextBox.Text = dt.Rows(0)("Ruc").ToString
                            lemaTextBox.Text = dt.Rows(0)("lema").ToString
                            phoneTextBox.Text = dt.Rows(0)("Phone").ToString
                            If IsDate(dt.Rows(0)("DateStar")) Then
                                DateStar.Value = dt.Rows(0)("DateStar")
                                DateStar.Checked = True
                            Else
                                DateStar.Checked = False
                            End If

                            DomicilioTextBox.Text = dt.Rows(0)("Domicilio").ToString
                            representanteTextBox.Text = dt.Rows(0)("Representante").ToString
                            NoteTextBox.Text = dt.Rows(0)("note").ToString
                            Me.registerInSystemLabel.Text = String.Format("Registrado en el sistema: {0:M/d/yyyy H:mm}", dt.Rows(0)("dateRegister"))
                            CancelSalesCheckBox.Checked = CType(dt.Rows(0)("IsCancelInSalesNotStock"), Boolean)
                            txtNomComercial.Text = dt.Rows(0).Field(Of String)("NameComercial")
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub OptionButto_Click(sender As Object, e As EventArgs) Handles OptionButto.Click
        Try
            Cursor = Cursors.WaitCursor
            Using viewOp As New MyCommerceForm()
                viewOp.StartPosition = FormStartPosition.CenterScreen
                viewOp.ShowDialog()
                If (viewOp.DialogResult = DialogResult.OK) Then
                    Interaction.MsgBox("Para efectuar cambios es necesario que reinicien el aplicativo en todos los terminales", MsgBoxStyle.Information, "Importante")
                    Application.Exit()
                End If
            End Using


        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub


    Private Sub SendEmailButton_Click(sender As Object, e As EventArgs) Handles SendEmailButton.Click
        Try
            Cursor = Cursors.WaitCursor
            Using viewSenemail As New SettingImageLogoForm(SettingObject.EcommerceActive.CommerceId)
                viewSenemail.StartPosition = FormStartPosition.CenterScreen
                viewSenemail.ShowDialog()
                If (viewSenemail.DialogResult = DialogResult.Yes) Then

                End If
            End Using


        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnConnectionRemote_Click(sender As Object, e As EventArgs) Handles btnConnectionRemote.Click
        Try
            Using setting As New frmConnectionRemoteProfile()
                setting.StartPosition = FormStartPosition.CenterScreen
                setting.ShowDialog()
            End Using
        Catch ex As Exception
            Interaction.MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


End Class
