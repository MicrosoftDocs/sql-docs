---
title: "MSSQLSERVER_-2_deleted | Microsoft Docs"
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
  - "-2"
helpviewer_keywords: 
  - "-2 (Database Engine error)"
ms.assetid: f37a7b7d-26e1-4b9e-bcb4-57f7805393d2
caps.latest.revision: 11
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_-2_deleted
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|-2|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name||  
|Message Text|Timeout expired.  The timeout period elapsed prior to completion of the operation or the server is not responding. (Microsoft SQL Server, Error: -2)|  
  
## Explanation  
The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] client cannot connect to the server. This error could occur because the firewall on the server has refused the connection.  
  
## User Action  
Make sure that you have configured the firewall on the server instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to accept connections.  
  
## See Also  
[Configure the Windows Firewall to Allow SQL Server Access](../Topic/Configure%20the%20Windows%20Firewall%20to%20Allow%20SQL%20Server%20Access.md)  
[Configure a Windows Firewall for Database Engine Access](../Topic/Configure%20a%20Windows%20Firewall%20for%20Database%20Engine%20Access.md)  
[Configure Client Protocols](../Topic/Configure%20Client%20Protocols.md)  
[Network Protocols and Network Libraries](../Topic/Network%20Protocols%20and%20Network%20Libraries.md)  
[Client Network Configuration](../Topic/Client%20Network%20Configuration.md)  
[Configure Client Protocols](../Topic/Configure%20Client%20Protocols.md)  
[Enable or Disable a Server Network Protocol](../Topic/Enable%20or%20Disable%20a%20Server%20Network%20Protocol.md)  
  
