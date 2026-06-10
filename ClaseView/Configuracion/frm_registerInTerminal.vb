Imports CADsisVenta.ClsSystem
Imports CADsisVenta.DataSetSystem
Imports CADsisVenta.DataSetSystemTableAdapters
Imports CADsisVenta.Helpers.FInicio
Imports Domain.Data.Repositories

Public Class frm_registerInTerminal
    Protected Friend Operation As _operation
    Protected Friend idEquipo As Integer

    Dim estaCargado As Boolean

    Private ReadOnly IsMain As Boolean
    Private ReadOnly IdSucursal As Integer

    Sub New(Optional isMain As Boolean = True, Optional idSucursal As Integer = -1)

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        Me.IsMain = isMain
        Me.IdSucursal = idSucursal
    End Sub

    Private Sub frm_registerInTerminal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        codTerminalTextBox.CharacterCasing = CharacterCasing.Upper

inicia:
        If IsNothing(Dominio._HotName) Then
            If Not Carga_DominioMaquina() Then
                Me.Close()
                Return
            End If
        End If

        Dim tapt As New EquiposTableAdapter
        idEquipo = tapt.idEquipoByDominio(Dominio._HotName)

        If idEquipo = 0 Then
            MsgBox("Equipo no resgistrado. Registrese por favor.", MsgBoxStyle.Critical, "Importante")
            Using nnewregister As New frmRegistroEquipo
                With nnewregister
                    .Operation = _operation.Insert
                    .StartPosition = FormStartPosition.CenterParent
                    .ShowDialog()
                    If .DialogResult = Windows.Forms.DialogResult.OK Then
                        GoTo inicia
                    Else
                        Me.Close()
                        Return
                    End If
                End With

            End Using
        End If

        If isRegisterInTerminal(Dominio._HotName) And Me.IsMain Then
            sql = "Este equipo ya esta ubicada en una estacinó." & vbNewLine
            sql = sql & "Puede cambiar su ubicación desde menu Configuración."
            MsgBox(sql, MsgBoxStyle.Exclamation, "Aviso")
            Me.Operation = _operation.Update
        End If

        Carga_Domicio()
        Carga_bodega()
        Carga_Location()
        Carga_Datos()

        BodegaComboBo.Enabled = Me.IsMain
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub OK_Button_Click(sender As Object, e As EventArgs) Handles OK_Button.Click
        Try
            If Valida_datos() Then
                Dim tadp As New TerminalTableAdapter
                Select Case Operation
                    Case _operation.Insert
                        If Not (tadp.Insert1(idEquipo, BodegaComboBo.SelectedValue, LocattionComboBox.SelectedValue,
                                             codTerminalTextBox.Text, txtPuntoEmision.Text) = 0) Then
                            Me.DialogResult = Windows.Forms.DialogResult.OK
                            Me.Close()
                            Application.Restart()
                        End If
                    Case _operation.Update
                        If Not (tadp.UpdateTerminal(idEquipo, BodegaComboBo.SelectedValue, LocattionComboBox.SelectedValue,
                                             codTerminalTextBox.Text, txtPuntoEmision.Text, TerminalActivo.idTerminal) = 0) Then
                            Me.DialogResult = Windows.Forms.DialogResult.OK
                            Me.Close()
                            Application.Restart()
                        End If
                End Select
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Function Valida_datos() As Boolean
        Try
            'bodega 
            If Not BodegaComboBo.SelectedIndex >= 0 Then
                ErrorProvider1.SetError(BodegaComboBo, "Selecccione uno del listado")
                BodegaComboBo.Focus()
                Return False
            Else
                ErrorProvider1.SetError(BodegaComboBo, "")
            End If
            'location
            If Not LocattionComboBox.SelectedIndex >= 0 Then
                ErrorProvider1.SetError(LocattionComboBox, "Selecccione uno del listado")
                LocattionComboBox.Focus()
                Return False
            Else
                ErrorProvider1.SetError(LocattionComboBox, "")
            End If
            'cod terminal

            If Not codTerminalTextBox.TextLength = 8 Then
                ErrorProvider1.SetError(codTerminalTextBox, "Debe tener 8 caracteres")
                codTerminalTextBox.Focus()
                Return False
            ElseIf String.IsNullOrWhiteSpace(codTerminalTextBox.Text) Then
                ErrorProvider1.SetError(codTerminalTextBox, "Invalido")
                codTerminalTextBox.Focus()
                Return False
            Else
                For Each tex In codTerminalTextBox.Text
                    If Not (Asc(tex) > 64 And Asc(tex) < 91) Then
                        If Not (Asc(tex) > 96 And Asc(tex) < 123) Then
                            If Not (Asc(tex) > 47 And Asc(tex) < 58) Then
                                If Not (Asc(tex) = 95) Then
                                    ErrorProvider1.SetError(codTerminalTextBox, "Invalido.. Sin espacios Letras o números")
                                    codTerminalTextBox.Focus()
                                    Return False
                                End If
                            End If
                        End If
                    End If
                Next
            End If


            ' valido cod pnt emision
            If Not txtPuntoEmision.Text.Trim().Length = 3 Then
                ErrorProvider1.SetError(txtPuntoEmision, "Determine el punto de emisión válido")
                Return False
            End If

            ErrorProvider1.SetError(txtPuntoEmision, String.Empty)

            Dim result = MessageBox.Show(
                "Se realizarán cambios estructurales en el sistema." & Environment.NewLine &
                "La aplicación deberá reiniciarse para que los cambios surtan efecto." & Environment.NewLine & Environment.NewLine &
                "¿Desea continuar?",
                "Confirmación requerida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)


            If result = DialogResult.Yes Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return False
        End Try
    End Function
    Private Sub Carga_Domicio()
        DominioTextBox.Text = Dominio._HotName
        IpTextBox.Text = Dominio._ip
        IsValidatedWepTextBox.Text = Convert.ToString(Dominio.isWep)
    End Sub

    Private Sub Carga_bodega()
        estaCargado = False
        Try

            Dim currentBodega As Integer = CInt(TerminalActivo.idBodega)

            Dim listBodega = BodegaRepository.TraeListaExepRemoto(DomainSQLite.Setting.Configuration.ConectionString)

            BodegaComboBo.DataSource = listBodega
            If BodegaComboBo.Items.Count > 0 Then
                BodegaComboBo.DisplayMember = "NomBodega"
                BodegaComboBo.ValueMember = "IdBodega"

                If (currentBodega > 0) Then
                    BodegaComboBo.SelectedValue = currentBodega
                End If

                If Not Me.IsMain Then
                    BodegaComboBo.SelectedValue = Me.IdSucursal
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        Finally
            estaCargado = True
        End Try
    End Sub

    Private Sub Carga_Location()
        estaCargado = False
        Try
            Dim tadp As New LocationTableAdapter
            LocattionComboBox.DataSource = tadp.GetData()
            If LocattionComboBox.Items.Count > 0 Then
                LocattionComboBox.DisplayMember = "Des_Location"
                LocattionComboBox.ValueMember = "idLocation"
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            estaCargado = True
        End Try
    End Sub
    Private Sub Carga_Datos()
        Try
            Dim dt As New DataTable()
            If Me.IsMain Then
                dt = GetTerminalDataByDominio(Dominio._HotName, TerminalActivo.idBodega)
            Else
                dt = GetTerminalDataByDominio(Dominio._HotName, TerminalActivo.idBodega)
            End If

            If dt.Rows.Count = 1 Then
                BodegaComboBo.SelectedValue = CInt(dt.Rows(0)("idBodega"))
                LocattionComboBox.SelectedValue = CInt(dt.Rows(0)("idLocation"))
                codTerminalTextBox.Text = dt.Rows(0)("codTerminal").ToString
                txtPuntoEmision.Text = dt.Rows(0)("CodPntoEmision").ToString
            End If

            If Not IsNothing(dt) Then
                dt = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message & vbLf & ex.StackTrace, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
    Private Sub codTerminalTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles codTerminalTextBox.KeyPress
        If InStr(" ", e.KeyChar) = 1 Then
            e.Handled = True
        End If
    End Sub
End Class