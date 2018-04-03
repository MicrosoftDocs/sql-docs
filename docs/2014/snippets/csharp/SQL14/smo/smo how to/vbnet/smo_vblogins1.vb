Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBLogins1

    Sub Main()
       
        ' <snippetSMO_VBLogins1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Iterate through each database and display.
        Dim db As Database
        For Each db In srv.Databases
            Console.WriteLine("============================================")
            Console.WriteLine("Login Mappings for the database: " + db.Name)
            Console.WriteLine(" ")
            'Run the EnumLoginMappings method and return details of database user-login mappings to a DataTable object variable.
            Dim d As DataTable
            d = db.EnumLoginMappings
            'Display the mapping information.
            Dim r As DataRow
            Dim c As DataColumn
            For Each r In d.Rows
                For Each c In r.Table.Columns
                    Console.WriteLine(c.ColumnName + " = " + r(c))
                Next
                Console.WriteLine(" ")
            Next
        Next
        ' </snippetSMO_VBLogins1>
    End Sub

End Module
