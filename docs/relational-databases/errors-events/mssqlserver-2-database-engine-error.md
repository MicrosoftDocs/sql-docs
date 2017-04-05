---
title: "MSSQLSERVER_2 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "2"
helpviewer_keywords: 
  - "2 (Database Engine error)"
ms.assetid: 567fb571-7cda-4ce8-a702-cdff2df5d419
caps.latest.revision: 10
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
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
[Manage the Database Engine Services](../Topic/Manage%20the%20Database%20Engine%20Services.md)  
[Configure Client Protocols](../Topic/Configure%20Client%20Protocols.md)  
[Network Protocols and Network Libraries](../Topic/Network%20Protocols%20and%20Network%20Libraries.md)  
[Client Network Configuration](../Topic/Client%20Network%20Configuration.md)  
[Configure Client Protocols](../Topic/Configure%20Client%20Protocols.md)  
[Enable or Disable a Server Network Protocol](../Topic/Enable%20or%20Disable%20a%20Server%20Network%20Protocol.md)  
  
