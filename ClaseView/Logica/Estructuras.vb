Module Estructuras
    Public Structure Compra
        Public idBodega As Integer
        Public idPedido As Integer
        Public idProveedor As Integer
        Public idConsumo As Integer
        Public idDeclaracion As Integer
        Public idFormaPago As Integer
        Public FechaComra As Date
        Public Tipo_Doc As Byte
        Public Num_Doc As String
        Public codUser As String
    End Structure
    Public FacturCompra As Compra

End Module

Module Estrucrura2
    Public Structure Pago
        Public idPago As Integer
        Public idformaPago As Integer
        Public idProveedor As Integer
        Public val_Factura As Double
        'para detail
        Public val_Pagado As Double
        Public fech_Pago As Date
        Public fech_Factura As Date
        Public fech_Cheq As Date
        Public tipo_Pago As Integer
        Public num_pago As String   '12 caracteres

        'Para llevar cheque
        Public idPagoDetail As Integer 'es el id de los pagos de detalle
        Public idBancoCuenta As Integer  'id de cuenta de los bancos
        Public Num_Documento As String ' 15 caracteres
        Public loginUser As String      ' 8 caracteres para usuarios
        Public idCliente As Integer 'es el id de los pagos de detalle

    End Structure
    Public Pagos As Pago
End Module
Module Printer
    Public Structure optPrintFac
        Public idPrint As Integer    '_idde row
        Public NamePrint As String   'Nombre de la impresona
        Public Color As String       'color de tinta
        Public items As Integer      'Items de prodcuto por página
        Public typePrint As String   'Items de prodcuto por página
        Public isDefaultConfig As Boolean
        Public PrintLogo As Boolean
        Public PrintEmptySpaces As Boolean
        Public PaperSizeWidth As Byte 'ancho de papel en caracteres
    End Structure
    Public myOptnsPrint As optPrintFac
End Module

Module Ventas
    Public Structure FacVenta
        Public idFactur As Integer   '-idde row
        Public nameDocunt As String   'Nombre de la impresona
        Public NumFactur As String
        Public idCliente As Integer       'color de tinta
        Public idFormPago As Integer      'Items de prodcuto por página
        Public idBodega As Integer      'Items de prodcuto por página
        Public fechDesde As Date
        Public fechHasta As Date
        Public OtroValor As Double
        Public Ba00 As Double
        Public Ba12 As Double
        Public iva As Double
        Public total As Double
        Public State As Int32             'estado de factura 255 en cola de impresion
    End Structure
    Public FacturVenta As FacVenta
End Module