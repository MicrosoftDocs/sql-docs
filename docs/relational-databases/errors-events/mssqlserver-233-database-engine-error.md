---
title: "MSSQLSERVER_233 | Microsoft Docs"
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
  - "233"
helpviewer_keywords: 
  - "233 (Database Engine error)"
ms.assetid: 201665dc-7ac8-4c19-90d3-33354c5caa72
caps.latest.revision: 13
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_233
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|233|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name||  
|Message Text|A connection was successfully established with the server, but then an error occurred during the login process. (provider: Shared Memory Provider, error: 0 - No process is on the other end of the pipe.) (Microsoft SQL Server, Error: 233)|  
  
## Explanation  
The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] client cannot connect to the server. This error could occur because the server is not configured to accept remote connections.  
  
## User Action  
Use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager tool to allow [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to accept remote connections.  
  
## See Also  
[Network Protocols and Network Libraries](../Topic/Network%20Protocols%20and%20Network%20Libraries.md)  
[Client Network Configuration](../Topic/Client%20Network%20Configuration.md)  
[Configure Client Protocols](../Topic/Configure%20Client%20Protocols.md)  
[Enable or Disable a Server Network Protocol](../Topic/Enable%20or%20Disable%20a%20Server%20Network%20Protocol.md)  
  
