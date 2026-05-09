Imports System.Data.SqlClient

Public Class frmMotivo

    Public Property MotivoSeleccionadoId As Integer?
    Public Property DescMotivoSeleccionado As String

    Private _listMotivos As List(Of MotivoItem)

    Sub New(Optional listMotivos As List(Of MotivoItem) = Nothing)

        InitializeComponent()

        _listMotivos = listMotivos
    End Sub


    Private Sub frmMotivo_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If (_listMotivos Is Nothing OrElse _listMotivos.Count = 0) Then
            CargarMotivos()
        End If
    End Sub

    Public Sub Configurar(nombreProducto As String,
                          cantEnviada As Decimal,
                          cantRecibida As Decimal?,
                          esRechazo As Boolean)
        If esRechazo Then
            lblTitulo.Text = "¿Por qué no se recibe este producto?"
            lblDetalle.Text = $"{nombreProducto}  |  Enviado: {cantEnviada}"
            lblDetalle.ForeColor = Color.FromArgb(226, 75, 74)
        Else
            lblTitulo.Text = "Indique el motivo de la diferencia"
            Dim diff = cantEnviada - cantRecibida.GetValueOrDefault()
            lblDetalle.Text = $"{nombreProducto}  |  Enviado: {cantEnviada}  ·  Recibido: {cantRecibida}  ·  Diferencia: −{diff}"
            lblDetalle.ForeColor = Color.FromArgb(239, 159, 39)
        End If
    End Sub

    Private Sub CargarMotivos()
        Dim sql = "SELECT idMotivo, Descripcion FROM MotivoTransferencia WHERE Activo=1 ORDER BY idMotivo"
        Using conn As New SqlClient.SqlConnection(DomainSQLite.Setting.Configuration.ConectionString)
            conn.Open()
            Using cmd As New SqlClient.SqlCommand(sql, conn)
                Using reader = cmd.ExecuteReader()
                    cboMotivo.Items.Clear()
                    While reader.Read()
                        cboMotivo.Items.Add(New MotivoItem With {
                            .Id = reader.GetInt32(0),
                            .Descripcion = reader.GetString(1)
                        })
                    End While
                End Using
            End Using
        End Using
        cboMotivo.DisplayMember = "Descripcion"
        cboMotivo.ValueMember = "Id"
        cboMotivo.SelectedIndex = -1
    End Sub

    Private Sub btnConfirmar_Click(sender As Object, e As EventArgs) Handles btnConfirmar.Click
        If cboMotivo.SelectedIndex < 0 Then
            lblError.Visible = True
            cboMotivo.Focus()
            Return
        End If
        Dim sel = CType(cboMotivo.SelectedItem, MotivoItem)
        MotivoSeleccionadoId = sel.Id
        DescMotivoSeleccionado = sel.Descripcion
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        MotivoSeleccionadoId = Nothing
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        CargarMotivos()
    End Sub


End Class