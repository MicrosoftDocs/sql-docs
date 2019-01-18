Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBDatabase1

    Sub Main()

        ' <snippetSMO_VBDatabase1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Define a Database object variable by supplying the server and the database name arguments in the constructor.
        Dim db As Database
        db = New Database(srv, "Test_SMO_Database")
        'Create the database on the instance of SQL Server.
        db.Create()
        'Reference the database and display the date when it was created.
        db = srv.Databases("Test_SMO_Database")
        Console.WriteLine(db.CreateDate)
        'Remove the database.
        db.Drop()
        ' </snippetSMO_VBDatabase1>
    End Sub

End Module
