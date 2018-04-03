Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.SqlServer.Management.Smo
Module SMO_VBMethods6

    Sub Main()
        ' <snippetSMO_VCMethods6>
        'Connect to the local, default instance of SQL Server.
        Dim srv1 As Server
        srv1 = New Server()
        'Modify the default database and the timeout period for the connection.
        srv1.ConnectionContext.DatabaseName = "AdventureWorks2012"
        srv1.ConnectionContext.ConnectTimeout = 30
        'Make a second connection using a copy of the ConnectionContext property and verify settings.
        Dim srv2 As Server
        srv2 = New Server(srv1.ConnectionContext.Copy)
        Console.WriteLine(srv2.ConnectionContext.ConnectTimeout.ToString)
        ' </snippetSMO_VCMethods6>



    End Sub

End Module
