Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VB2

    Sub Main()

        Dim strServer As String
        strServer = "SqlSampTest"
        ' <snippetSMO_VB2>
        'Connect to a remote instance of SQL Server.
        Dim srv As Server
        'The strServer string variable contains the name of a remote instance of SQL Server.
        srv = New Server(strServer)
        'The actual connection is made when a property is retrieved. 
        Console.WriteLine(srv.Information.Version)
        'The connection is automatically disconnected when the Server variable goes out of scope.
        ' </snippetSMO_VB2>



    End Sub

End Module
