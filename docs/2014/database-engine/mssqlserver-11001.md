---
title: "MSSQLSERVER_11001 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "12001"
helpviewer_keywords: 
  - "11001 (Database Engine error)"
ms.assetid: 53d4d63a-61e3-441f-bfe9-9d44f7a05fd4
caps.latest.revision: 10
author: "craigg-msft"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_11001
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|11001|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name||  
|Message Text|An error has occurred while establishing a connection to the server.  When connecting to SQL Server, this failure may be caused by the fact that under the default settings SQL Server does not allow remote connections. (provider: TCP Provider, error: 0 - No such host is known.) (.Net SqlClient Data Provider)|  
  
## Explanation  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] client cannot connect to the server. The error could occur because either the client cannot resolve the name of the server or the name of the server is incorrect.  
  
## User Action  
 Make sure that you have entered the correct server name on the client, and that you can resolve the name of the server from the client. To check TCP/IP name resolution, you can use the **ping** command in the Windows operating system.  
  
## See Also  
 [Network Protocols and Network Libraries](../../2014/sql-server/install/network-protocols-and-network-libraries.md)   
 [Client Network Configuration](../../2014/database-engine/client-network-configuration.md)   
 [Configure Client Protocols](../../2014/database-engine/configure-client-protocols.md)   
 [Enable or Disable a Server Network Protocol](../../2014/database-engine/enable-or-disable-a-server-network-protocol.md)  
  
  