Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.SqlServer.Management.smo

Module SMO_VBMethods4

    Sub Main()
        ' <snippetSMO_VBMethods4>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Declare and define a Database object by supplying the parent server and the database name arguments in the constructor.
        Dim d As Database
        d = New Database(srv, "Test_SMO_Database")
        'Create the database on the instance of SQL Server.
        d.Create()
        Console.WriteLine(d.Name)
        ' </snippetSMO_VBMethods4>
        d.Drop()

    End Sub

End Module
