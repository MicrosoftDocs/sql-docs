Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common

Module SMO_VBMethods3

    Sub Main()
        ' <snippetSMO_VBMethods3>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Call the EnumCollations method and return collation information to DataTable variable.
        Dim d As DataTable
        'Select the returned data into an array of DataRow.
        d = srv.EnumCollations
        'Iterate through the rows and display collation details for the instance of SQL Server.
        Dim r As DataRow
        Dim c As DataColumn
        For Each r In d.Rows
            Console.WriteLine("============================================")
            For Each c In r.Table.Columns
                Console.WriteLine(c.ColumnName + " = " + r(c).ToString)
            Next
        Next
        ' </snippetSMO_VBMethods3>
       
    End Sub

End Module
