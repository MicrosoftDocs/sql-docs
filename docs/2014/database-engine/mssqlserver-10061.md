---
title: "MSSQLSERVER_10061 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "10061"
helpviewer_keywords: 
  - "10061 (Database Engine error)"
ms.assetid: 729602f3-08df-474c-8740-8dea13c1eee3
caps.latest.revision: 11
author: "craigg-msft"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_10061
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|10061|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name||  
|Message Text|An error has occurred while establishing a connection to the server.  When connecting to SQL Server, this failure may be caused by the fact that under the default settings SQL Server does not allow remote connections. (provider: TCP Provider, error: 0 - No connection could be made because the target machine actively refused it.) (Microsoft SQL Server, Error: 10061)|  
  
## Explanation  
 The server did not respond to the client request. This error could occur because the server is not started.  
  
## User Action  
 Make sure that the server is started.  
  
## See Also  
 [Manage the Database Engine Services](../../2014/database-engine/manage-the-database-engine-services.md)   
 [Configure Client Protocols](../../2014/database-engine/configure-client-protocols.md)   
 [Network Protocols and Network Libraries](../../2014/sql-server/install/network-protocols-and-network-libraries.md)   
 [Client Network Configuration](../../2014/database-engine/client-network-configuration.md)   
 [Configure Client Protocols](../../2014/database-engine/configure-client-protocols.md)   
 [Enable or Disable a Server Network Protocol](../../2014/database-engine/enable-or-disable-a-server-network-protocol.md)  
  
  