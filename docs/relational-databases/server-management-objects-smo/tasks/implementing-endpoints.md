---
description: "Implementing Endpoints"
title: "Implementing Endpoints"
ms.custom: seo-dt-2019
ms.date: "08/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: 

ms.topic: "reference"
helpviewer_keywords: 
  - "endpoints [SMO]"
ms.assetid: f8674dbb-9bc0-488f-9def-e9e0ce1ddf86
author: "markingmyname"
ms.author: "maghan"
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Implementing Endpoints
[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW ](../../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

  An endpoint is a service that can listen natively for requests. SMO supports various types of endpoints by using the <xref:Microsoft.SqlServer.Management.Smo.Endpoint> object. You can create an endpoint service that handles a specific type of payload, which uses a specific protocol, by creating an instance of an <xref:Microsoft.SqlServer.Management.Smo.Endpoint> object and setting its properties.  
  
 The <xref:Microsoft.SqlServer.Management.Smo.Endpoint.EndpointType%2A> property of the <xref:Microsoft.SqlServer.Management.Smo.Endpoint> object can be used to specify on of the following payload types:  
  
-   Database mirroring  
  
-   SOAP (support for SOAP endpoints is present in [!INCLUDE[ssKilimanjaro](../../../includes/sskilimanjaro-md.md)] and earlier [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] versions)  
  
-   Service Broker  
  
-   [!INCLUDE[tsql](../../../includes/tsql-md.md)]  
  
 Also, the <xref:Microsoft.SqlServer.Management.Smo.Endpoint.ProtocolType%2A> property can be used to specify the following two supported protocols:  
  
-   HTTP protocol  
  
-   TCP protocol  
  
 Having specified the type of payload, the actual payload can be set by using the <xref:Microsoft.SqlServer.Management.Smo.Endpoint.Payload%2A> object property. The <xref:Microsoft.SqlServer.Management.Smo.Payload> object property provides a reference to a payload object of the specified type, for which the properties can be modified.  
  
 For the <xref:Microsoft.SqlServer.Management.Smo.DatabaseMirroringPayload> object, you must specify the mirroring role and whether encryption is enabled. The <xref:Microsoft.SqlServer.Management.Smo.ServiceBrokerPayload> object requires information about message forwarding, maximum number of connections allowed and the authentication mode. The <xref:Microsoft.SqlServer.Management.Smo.SoapPayloadMethod.%23ctor%2A> object requires various properties to be set including the <xref:Microsoft.SqlServer.Management.Smo.SoapPayloadMethodCollection.Add%2A> object property that specifies the SOAP payload methods available to clients (stored procedures and user-defined functions).  
  
 Similarly, the actual protocol can be set by using the <xref:Microsoft.SqlServer.Management.Smo.Endpoint.Protocol%2A> object property that references a protocol object of the type specified by <xref:Microsoft.SqlServer.Management.Smo.Endpoint.ProtocolType%2A> property. The <xref:Microsoft.SqlServer.Management.Smo.HttpProtocol> object requires a list of restricted IP addresses, and port, website, and authentication information. The <xref:Microsoft.SqlServer.Management.Smo.TcpProtocol> object also requires a list of restricted IP addresses and port information.  
  
 When the endpoint has been created and fully defined, access can be granted to, revoked from, and denied to database users, groups, roles, and logons.  
  
## Example  
 For the following code example, you will have to select the programming environment, programming template and the programming language to create your application. For more information, see [Create a Visual C&#35; SMO Project in Visual Studio .NET](../../../relational-databases/server-management-objects-smo/how-to-create-a-visual-csharp-smo-project-in-visual-studio-net.md).  
  
## Creating a Database Mirroring Endpoint Service in Visual Basic  
 The code example demonstrates how to create a Database Mirroring endpoint in SMO. This is necessary before you create a database mirror. Use the <xref:Microsoft.SqlServer.Management.Smo.Database.IsMirroringEnabled%2A> and other properties on the <xref:Microsoft.SqlServer.Management.Smo.Database> object to create a database mirror.  
  
```VBNET
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
``` 
  
## Creating a Database Mirroring Endpoint Service in Visual C#  
 The code example demonstrates how to create a Database Mirroring endpoint in SMO. This is necessary before you create a database mirror. Use the <xref:Microsoft.SqlServer.Management.Smo.Database.IsMirroringEnabled%2A> and other properties on the <xref:Microsoft.SqlServer.Management.Smo.Database> object to create a database mirror.  
  
```csharp  
{  
            //Set up a database mirroring endpoint on the server before   
        //setting up a database mirror.   
        //Connect to the local, default instance of SQL Server.   
            Server srv = new Server();  
            //Define an Endpoint object variable for database mirroring.   
            Endpoint ep = default(Endpoint);  
            ep = new Endpoint(srv, "Mirroring_Endpoint");  
            ep.ProtocolType = ProtocolType.Tcp;  
            ep.EndpointType = EndpointType.DatabaseMirroring;  
            //Specify the protocol ports.   
            ep.Protocol.Http.SslPort = 5024;  
            ep.Protocol.Tcp.ListenerPort = 6666;  
            //Specify the role of the payload.   
            ep.Payload.DatabaseMirroring.ServerMirroringRole = ServerMirroringRole.All;  
            //Create the endpoint on the instance of SQL Server.   
            ep.Create();  
            //Start the endpoint.   
            ep.Start();  
            Console.WriteLine(ep.EndpointState);  
        }  
```  
  
## Creating a Database Mirroring Endpoint Service in PowerShell  
 The code example demonstrates how to create a Database Mirroring endpoint in SMO. This is necessary before you create a database mirror. Use the <xref:Microsoft.SqlServer.Management.Smo.Database.IsMirroringEnabled%2A> and other properties on the <xref:Microsoft.SqlServer.Management.Smo.Database> object to create a database mirror.  
  
```powershell  
# Set the path context to the local, default instance of SQL Server.  
CD \sql\localhost\  
$srv = get-item default  
  
#Get a new endpoint to congure and add  
$ep = New-Object -TypeName Microsoft.SqlServer.Management.SMO.Endpoint -argumentlist $srv,"Mirroring_Endpoint"  
  
#Set some properties  
$ep.ProtocolType = [Microsoft.SqlServer.Management.SMO.ProtocolType]::Tcp  
$ep.EndpointType = [Microsoft.SqlServer.Management.SMO.EndpointType]::DatabaseMirroring  
$ep.Protocol.Http.SslPort = 5024  
$ep.Protocol.Tcp.ListenerPort = 6666 #inline comment  
$ep.Payload.DatabaseMirroring.ServerMirroringRole = [Microsoft.SqlServer.Management.SMO.ServerMirroringRole]::All  
  
# Create the endpoint on the instance  
$ep.Create()  
  
# Start the endpoint  
$ep.Start()  
  
# Report its state  
$ep.EndpointState;  
```  
  
## See Also  
 [The Database Mirroring Endpoint &#40;SQL Server&#41;](../../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md)  
  
  
