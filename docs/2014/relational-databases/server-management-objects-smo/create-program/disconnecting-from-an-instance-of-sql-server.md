---
title: "Disconnecting from an Instance of SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Management Objects, disconnecting"
  - "SMO [SQL Server], disconnecting"
  - "instances of SQL Server, disconnecting"
  - "disconnecting [SMO]"
ms.assetid: 4ca7f7eb-6b3f-4c73-ac63-88afa8570b61
author: stevestein
ms.author: sstein
manager: craigg
---
# Disconnecting from an Instance of SQL Server
  Manually closing and disconnecting [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Management Objects (SMO) objects is not required. Connections are opened and closed as required.  
  
## Connection Pooling  
 When the <xref:Microsoft.SqlServer.Management.Common.ConnectionManager.Connect%2A> method is called, the connection is not automatically released. The <xref:Microsoft.SqlServer.Management.Common.ConnectionManager.Disconnect%2A> method must be called explicitly to release the connection to the connection pool. Also, you can request a non-pooled connection. You do this by setting the <xref:Microsoft.SqlServer.Management.Common.ConnectionSettings.NonPooledConnection%2A> property of the <xref:Microsoft.SqlServer.Management.Smo.Server.ConnectionContext%2A> property that references the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> object.  
  
## Disconnecting from an Instance of SQL Server for RMO  
 Closing server connections when you are programming with RMO works slightly different from SMO.  
  
 Because the server connection for an RMO object is maintained by the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> object, this object is also used when disconnecting from an instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] when you program by using RMO. To close a connection by using the <xref:Microsoft.SqlServer.Management.Common.ServerConnection> object, call the <xref:Microsoft.SqlServer.Management.Common.ConnectionManager.Disconnect%2A> method of the RMO object. After the connection has been closed, RMO objects cannot be used.  
  
## Example  
 [!INCLUDE[ssChooseProgEnv](../../../includes/sschooseprogenv-md.md)]  
  
## Closing and Disconnecting an SMO Object in Visual Basic  
 This code example shows how to request a non-pooled connection by setting the <xref:Microsoft.SqlServer.Management.Common.ConnectionSettings.NonPooledConnection%2A> property of the <xref:Microsoft.SqlServer.Management.Smo.Server.ConnectionContext%2A> object property.  
  
<!-- TODO: review snippet reference  [!CODE [SMO How to#SMO_VB4](SMO How to#SMO_VB4)]  -->  
  
## Closing and Disconnecting an SMO Object in Visual C#  
 This code example shows how to request a non-pooled connection by setting the <xref:Microsoft.SqlServer.Management.Common.ConnectionSettings.NonPooledConnection%2A> property of the <xref:Microsoft.SqlServer.Management.Smo.Server.ConnectionContext%2A> object property.  
  
```  
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
 <xref:Microsoft.SqlServer.Management.Common.ServerConnection>  
  
  
