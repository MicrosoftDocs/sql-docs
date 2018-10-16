Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBCollections2

    Sub Main()
        ' <snippetSMO_VBCollections2>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        Dim count As Integer
        Dim total As Integer
        'Iterate through the databases and call the GetActiveDBConnectionCount method.
        Dim db As Database
        For Each db In srv.Databases
            count = srv.GetActiveDBConnectionCount(db.Name)
            total = total + count
            'Display the number of connections for each database.
            Console.WriteLine(count & " connections on " & db.Name)
        Next
        'Display the total number of connections on the instance of SQL Server.
        Console.WriteLine("Total connections =" & total)
        ' </snippetSMO_VBCollections2>
    End Sub

End Module
