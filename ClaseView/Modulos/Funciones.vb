Imports CADsisVenta.Class

Module Funciones
    Public Function GenerateSpliter(myFindText As String, Optional isPreparatedStatement As Boolean = False) As ResponseSpliter
        Try
            Dim _responseSpliter As New ResponseSpliter
            'si no ha dada
            myFindText = Trim(myFindText)
            If myFindText.Length = 0 Or String.IsNullOrWhiteSpace(myFindText) Then
                _responseSpliter = New ResponseSpliter
            End If
            'preparamos el texto
            Dim isSpace As Boolean = False
            sql = ""
            For Each stri In myFindText
                If Not isSpace Then
                    sql += stri
                    isSpace = False
                End If
                If String.IsNullOrWhiteSpace(stri) Then
                    isSpace = True
                Else
                    If isSpace Then
                        sql += stri
                    End If
                    isSpace = False
                End If
            Next
            myFindText = sql

            'rebisamos si no es codigo munerico entonces es barra de codigo
            Dim isnumric As Boolean = True
            For Each texto In myFindText
                If InStr("0123456789", texto) = False Then
                    isnumric = False
                    Exit For
                End If
            Next
            If isnumric Then
                _responseSpliter = New ResponseSpliter With
                {
                .IsSucces = True,
                .IsNumeric = isnumric,
                .Spliter = myFindText.Split(" ")
                }
                GoTo Salida
            End If

            'para codigo de producto
            Dim isText As Boolean = False
            isnumric = False
            For Each texto In myFindText
                If String.IsNullOrWhiteSpace(texto) Then
                    isText = False
                    isnumric = False
                    Exit For
                ElseIf InStr("0123456789", texto) = False Then
                    If Not isText Then
                        isText = True
                    End If
                Else
                    If Not isnumric Then
                        isnumric = True
                    End If
                End If
            Next
            '// si es codigo
            If (isText = True) And (isnumric = True) Then
                _responseSpliter = New ResponseSpliter With
                {
                .IsSucces = True,
                .IsCode = True,
                .Spliter = myFindText.Split(" ")
                }
                GoTo Salida
            Else
                'si es nombre de producto covierto en una matriz
                _responseSpliter = New ResponseSpliter With
                    {
                    .IsSucces = True,
                    .Spliter = myFindText.Split(" ")
                    }
            End If
Salida:
            If isPreparatedStatement Then
                'If preparatedStatement(_responseSpliter) Then
                '    Return _responseSpliter
                'End If
            End If
            Return _responseSpliter
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Return New ResponseSpliter
        End Try
    End Function
End Module
