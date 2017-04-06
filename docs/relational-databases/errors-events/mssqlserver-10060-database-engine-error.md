---
title: "MSSQLSERVER_10060 | Microsoft Docs"
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
  - "10060"
helpviewer_keywords: 
  - "10060 (Database Engine error)"
ms.assetid: 28c21277-cad8-406c-a955-07933a56982a
caps.latest.revision: 13
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_10060
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|10060|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name||  
|Message Text|An error has occurred while establishing a connection to the server.  When connecting to SQL Server, this failure may be caused by the fact that under the default settings SQL Server does not allow remote connections. (provider: TCP Provider, error: 0 - A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond.) (Microsoft SQL Server, Error: 10060)|  
  
## Explanation  
The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] client cannot connect to the server. This error could occur because either the firewall on the server has refused the connection or the server is not configured to accept remote connections.  
  
## User Action  
To resolve this error, try one of the following actions:  
  
-   Make sure that you have configured the firewall on the computer to allow this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to accept connections.  
  
-   Use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager tool to allow [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to accept remote connections.  
  
## See Also  
[Configure a Windows Firewall for Database Engine Access](../Topic/Configure%20a%20Windows%20Firewall%20for%20Database%20Engine%20Access.md)  
[Configure Client Protocols](../Topic/Configure%20Client%20Protocols.md)  
[Network Protocols and Network Libraries](../Topic/Network%20Protocols%20and%20Network%20Libraries.md)  
[Client Network Configuration](../Topic/Client%20Network%20Configuration.md)  
[Configure Client Protocols](../Topic/Configure%20Client%20Protocols.md)  
[Enable or Disable a Server Network Protocol](../Topic/Enable%20or%20Disable%20a%20Server%20Network%20Protocol.md)  
  
