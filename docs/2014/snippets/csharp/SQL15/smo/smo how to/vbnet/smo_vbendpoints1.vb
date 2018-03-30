Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.SqlServer.Management.Smo
Module SMO_VBEndpoints1

    Sub Main()
        ' <snippetSMO_VBEndpoints1>
        'Set up a database mirroring endpoint on the server before setting up a database mirror.
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Define an Endpoint object variable for database mirroring.
        Dim ep As Endpoint
        ep = New Endpoint(srv, "Mirroring_Endpoint")
        ep.ProtocolType = ProtocolType.Tcp
        ep.EndpointType = EndpointType.DatabaseMirroring
        'Specify the protocol ports.
        ep.Protocol.Http.SslPort = 5024
        ep.Protocol.Tcp.ListenerPort = 6666
        'Specify the role of the payload.
        ep.Payload.DatabaseMirroring.ServerMirroringRole = ServerMirroringRole.All
        'Create the endpoint on the instance of SQL Server.
        ep.Create()
        'Start the endpoint.
        ep.Start()
        Console.WriteLine(ep.EndpointState)
        ' </snippetSMO_VBEndpoints1>
        ep.Stop()
        ep.Drop()

    End Sub

End Module
