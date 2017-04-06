---
title: "MSSQLSERVER_10061_deleted | Microsoft Docs"
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
  - "10061"
helpviewer_keywords: 
  - "10061 (Database Engine error)"
ms.assetid: 729602f3-08df-474c-8740-8dea13c1eee3
caps.latest.revision: 11
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_10061_deleted
  
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
[Manage the Database Engine Services](../Topic/Manage%20the%20Database%20Engine%20Services.md)  
[Configure Client Protocols](../Topic/Configure%20Client%20Protocols.md)  
[Network Protocols and Network Libraries](../Topic/Network%20Protocols%20and%20Network%20Libraries.md)  
[Client Network Configuration](../Topic/Client%20Network%20Configuration.md)  
[Configure Client Protocols](../Topic/Configure%20Client%20Protocols.md)  
[Enable or Disable a Server Network Protocol](../Topic/Enable%20or%20Disable%20a%20Server%20Network%20Protocol.md)  
  
