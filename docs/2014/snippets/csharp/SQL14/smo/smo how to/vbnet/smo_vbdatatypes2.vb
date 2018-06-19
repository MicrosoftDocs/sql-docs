Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBDataTypes2

    Sub Main()
        ' <snippetSMO_VBDataTypes2>
        'Declare and create a DataType object variable.
        Dim dt As DataType
        dt = New DataType
        'Define the data type by setting the SqlDataType property.
        dt.SqlDataType = SqlDataType.VarChar
        'The VarChar data type requires a value for the MaximumLength property.
        dt.MaximumLength = 100
        ' </snippetSMO_VBDataTypes2>
    End Sub

End Module
