Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VB1

    Sub Main()
        ' <snippetSMO_VB1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'The connection is established when a property is requested.
        Console.WriteLine(srv.Information.Version)
        'The connection is automatically disconnected when the Server variable goes out of scope.
        ' </snippetSMO_VB1>
    End Sub

End Module
