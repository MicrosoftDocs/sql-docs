---
title: "Disconnecting from an Instance of SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "08/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 

ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Management Objects, disconnecting"
  - "SMO [SQL Server], disconnecting"
  - "instances of SQL Server, disconnecting"
  - "disconnecting [SMO]"
ms.assetid: 4ca7f7eb-6b3f-4c73-ac63-88afa8570b61
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Disconnecting from an Instance of SQL Server
[!INCLUDE[appliesto-ss-asdb-asdw-xxx-md](../../../includes/appliesto-ss-asdb-asdw-xxx-md.md)]

  Manually closing and disconnecting [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Management Objects (SMO) objects is not required. Connections are opened and closed as required.  
  
## Connection Pooling  
 When the [Connect](https://msdn.microsoft.com/library/microsoft.sqlserver.management.common.connectionmanager.connect) method is called, the connection is not automatically released. The [Disconnect](https://msdn.microsoft.com/library/microsoft.sqlserver.management.common.connectionmanager.disconnect) method must be called explicitly to release the connection to the connection pool. Also, you can request a non-pooled connection. You do this by setting the [NonPooledConnection](https://msdn.microsoft.com/library/microsoft.sqlserver.management.common.connectionsettings.nonpooledconnection) property of the <xref:Microsoft.SqlServer.Management.Smo.Server.ConnectionContext%2A> property that references the [ServerConnection](https://msdn.microsoft.com/library/microsoft.sqlserver.management.common.serverconnection.aspx) object.  
  
## Disconnecting from an Instance of SQL Server for RMO  
 Closing server connections when you are programming with RMO works slightly different from SMO.  
  
 Because the server connection for an RMO object is maintained by the [ServerConnection](https://msdn.microsoft.com/library/microsoft.sqlserver.management.common.serverconnection.aspx) object, this object is also used when disconnecting from an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] when you program by using RMO. To close a connection by using the [ServerConnection](https://msdn.microsoft.com/library/microsoft.sqlserver.management.common.serverconnection.aspx) object, call the [Disconnect](https://msdn.microsoft.com/library/microsoft.sqlserver.management.common.connectionmanager.disconnect) method of the RMO object. After the connection has been closed, RMO objects cannot be used.  
  
## Example  
To use any code example that is provided, you will have to choose the programming environment, the programming template, and the programming language in which to create your application. For more information, see [Create a Visual C&#35; SMO Project in Visual Studio .NET](../../../relational-databases/server-management-objects-smo/how-to-create-a-visual-csharp-smo-project-in-visual-studio-net.md).  
 
  
## Closing and Disconnecting an SMO Object in Visual Basic  
 This code example shows how to request a non-pooled connection by setting the [NonPooledConnection](https://msdn.microsoft.com/library/microsoft.sqlserver.management.common.connectionsettings.nonpooledconnection) property of the <xref:Microsoft.SqlServer.Management.Smo.Server.ConnectionContext%2A> object property.  
  
```VBNET
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
```
  
## Closing and Disconnecting an SMO Object in Visual C#  
 This code example shows how to request a non-pooled connection by setting the [NonPooledConnection](https://msdn.microsoft.com/library/microsoft.sqlserver.management.common.connectionsettings.nonpooledconnection) property of the <xref:Microsoft.SqlServer.Management.Smo.Server.ConnectionContext%2A> object property.  
  
```csharp  
{   
Server srv;   
srv = new Server();   
//Disable automatic disconnection.   
srv.ConnectionContext.AutoDisconnectMode = AutoDisconnectMode.NoAutoDisconnect;   
//Connect to the local, default instance of SQL Server.   
srv.ConnectionContext.Connect();   
//The actual connection is made when a property is retrieved.   
Console.WriteLine(srv.Information.Version);   
//Disconnect explicitly.   
srv.ConnectionContext.Disconnect();  
}  
```  
  
## See Also  
 <xref:Microsoft.SqlServer.Management.Smo.Server>   
 [ServerConnection](https://msdn.microsoft.com/library/microsoft.sqlserver.management.common.serverconnection.aspx)  
  
  
