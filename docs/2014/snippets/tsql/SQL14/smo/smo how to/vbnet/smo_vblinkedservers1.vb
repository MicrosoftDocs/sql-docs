Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBLinkedServers1

    Sub Main()
        ' <snippetSMO_VBLinkedServers1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Create a linked server.
        Dim lsrv As LinkedServer
        lsrv = New LinkedServer(srv, "OLEDBSRV")
        'When the product name is SQL Server the remaining properties are not required to be set.
        lsrv.ProductName = "SQL Server"
        lsrv.Create()
        ' </snippetSMO_VBLinkedServers1>
        lsrv.Drop()


    End Sub

End Module
