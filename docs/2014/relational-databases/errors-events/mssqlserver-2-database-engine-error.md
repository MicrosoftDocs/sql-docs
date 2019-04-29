---
title: "MSSQLSERVER_2 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
f1_keywords: 
  - "2"
helpviewer_keywords: 
  - "2 (Database Engine error)"
ms.assetid: 567fb571-7cda-4ce8-a702-cdff2df5d419
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_2
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|2|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name||  
|Message Text|An error has occurred while establishing a connection to the server.  When connecting to SQL Server, this failure may be caused by the fact that under the default settings SQL Server does not allow remote connections. (provider: Named Pipes Provider, error: 40 - Could not open a connection to SQL Server) (.Net SqlClient Data Provider)|  
  
## Explanation  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] did not respond to the client request because the server is probably not started.  
  
## User Action  
 Make sure that the server is started.  
  
## See Also  
 [Manage the Database Engine Services](../../database-engine/configure-windows/manage-the-database-engine-services.md)   
 [Configure Client Protocols](../../database-engine/configure-windows/configure-client-protocols.md)   
 [Network Protocols and Network Libraries](../../sql-server/install/network-protocols-and-network-libraries.md)   
 [Client Network Configuration](../../database-engine/configure-windows/client-network-configuration.md)   
 [Configure Client Protocols](../../database-engine/configure-windows/configure-client-protocols.md)   
 [Enable or Disable a Server Network Protocol](../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md)  
  
  
