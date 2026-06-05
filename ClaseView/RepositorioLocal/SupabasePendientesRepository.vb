Imports Newtonsoft.Json

Public Class SupabasePendientesRepository

    ' Guarda un pendiente cuando Supabase falla
    Public Shared Sub Guardar(supabaseId As String,
                               estado As String,
                               novedad As String,
                               detalle As Object,
                               Optional modulo As String = "TRANSFERENCIA")
        Dim json As String = If(detalle IsNot Nothing,
                                JsonConvert.SerializeObject(detalle), Nothing)
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(
                "INSERT INTO SupabasePendientes " &
                "  (SupabaseId, Modulo, Estado, Novedad, DetalleJson, EstadoEnvio) " &
                "VALUES (@sid, @mod, @est, @nov, @det, 'PENDIENTE')",
                {"@sid", "@mod", "@est", "@nov", "@det"},
                {supabaseId,
                 modulo,
                 estado,
                 If(String.IsNullOrEmpty(novedad), CObj(DBNull.Value), CObj(novedad)),
                 If(json Is Nothing, CObj(DBNull.Value), CObj(json))})
        End Using
    End Sub

    ' Lee todos los PENDIENTE de un módulo
    Public Shared Function ObtenerPendientes(
            Optional modulo As String = "TRANSFERENCIA") As List(Of SupabasePendienteDto)

        Dim lista As New List(Of SupabasePendienteDto)
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            Dim dt = cmd.RetornaTablaConParams(
                "SELECT Id, SupabaseId, Estado, Novedad, DetalleJson, Intentos " &
                "FROM SupabasePendientes " &
                "WHERE EstadoEnvio='PENDIENTE' AND Modulo=@mod " &
                "ORDER BY FechaCreacion",
                {"@mod"}, {modulo})

            For Each row As DataRow In dt.Rows
                lista.Add(New SupabasePendienteDto With {
                    .Id = CInt(row("Id")),
                    .SupabaseId = row("SupabaseId").ToString(),
                    .Estado = row("Estado").ToString(),
                    .Novedad = If(IsDBNull(row("Novedad")), Nothing, row("Novedad").ToString()),
                    .DetalleJson = If(IsDBNull(row("DetalleJson")), Nothing, row("DetalleJson").ToString()),
                    .Intentos = CInt(row("Intentos"))
                })
            Next
        End Using
        Return lista
    End Function

    ' Marca como ENVIADO
    Public Shared Sub MarcarEnviado(id As Integer)
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(
                "UPDATE SupabasePendientes " &
                "SET EstadoEnvio='ENVIADO', UltimoIntento=GETDATE() WHERE Id=@id",
                {"@id"}, {id})
        End Using
    End Sub

    ' Actualiza intento fallido
    Public Shared Sub RegistrarFallo(id As Integer)
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            cmd.EjecutarConParams(
                "UPDATE SupabasePendientes " &
                "SET Intentos=Intentos+1, UltimoIntento=GETDATE() WHERE Id=@id",
                {"@id"}, {id})
        End Using
    End Sub

    ' Cuenta pendientes (para el aviso al abrir la app)
    Public Shared Function ContarPendientes() As Integer
        Using cmd As New CADsisVenta.Funtions.SqlComandExec
            Dim result = cmd.RetornaEscalarConParams(
                "SELECT COUNT(1) FROM SupabasePendientes WHERE EstadoEnvio='PENDIENTE'",
                {}, {})
            Return CInt(result)
        End Using
    End Function

End Class

Public Class SupabasePendienteDto
    Public Property Id As Integer
    Public Property SupabaseId As String
    Public Property Estado As String
    Public Property Novedad As String
    Public Property DetalleJson As String
    Public Property Intentos As Integer
End Class