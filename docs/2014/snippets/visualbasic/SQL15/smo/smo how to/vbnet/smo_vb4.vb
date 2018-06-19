
Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VB4

    Sub Main()
        ' <snippetSMO_VB4>

        Dim srv As Server
        srv = New Server
        'Disable automatic disconnection.
        srv.ConnectionContext.AutoDisconnectMode = AutoDisconnectMode.NoAutoDisconnect
        'Connect to the local, default instance of SQL Server.
        srv.ConnectionContext.Connect()
        'The actual connection is made when a property is retrieved.
        Console.WriteLine(srv.Information.Version)
        'Disconnect explicitly.
        srv.ConnectionContext.Disconnect()
        ' </snippetSMO_VB4>

    End Sub

End Module
