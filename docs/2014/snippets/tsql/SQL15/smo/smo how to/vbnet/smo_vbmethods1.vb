Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBMethods1

    Sub Main()
        ' <snippetSMO_VBMethods1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Define a Database object variable by supplying the parent server and the database name arguments in the constructor.
        Dim db As Database
        db = New Database(srv, "Test_SMO_Database")
        'Call the Create method to create the database on the instance of SQL Server. 
        db.Create()
        ' </snippetSMO_VBMethods1>
        Console.WriteLine(db.Name)


        db.Drop()
    End Sub

End Module
