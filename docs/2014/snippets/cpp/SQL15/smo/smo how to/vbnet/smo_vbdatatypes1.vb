Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBDataTypes1

    Sub Main()
        ' <snippetSMO_VBDataTypes1>
        'Declare a DataType object variable and define the data type in the constructor.
        Dim dt As DataType
        'For the decimal data type the following two arguments specify precision, and scale.
        dt = New DataType(SqlDataType.Decimal, 10, 2)
        ' </snippetSMO_VBDataTypes1>
    End Sub

End Module
