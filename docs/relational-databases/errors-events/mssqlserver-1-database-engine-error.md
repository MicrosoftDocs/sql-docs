---
title: "MSSQLSERVER_-1 | Microsoft Docs"
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
  - "-1"
helpviewer_keywords: 
  - "-1 (Database Engine error)"
ms.assetid: bad25b91-eaed-46c0-a5b7-71117a32304c
caps.latest.revision: 12
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_-1
  
## Details  
  
|||  
|-|-|  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|-1|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name||  
|Message Text|An error has occurred while establishing a connection to the server.  When connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this failure may be caused by the fact that under the default settings [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not allow remote connections. (provider: SQL Network Interfaces, error: 28 - Server doesn't support requested protocol) (Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], Error: -1)|  
  
## Explanation  
The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] client cannot connect to the server. This error could be caused by one of the following reasons:  
  
-   A specified [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance name is not valid.  
  
-   The TCP, or named pipes protocols are not enabled.  
  
-   The firewall on the server has refused the connection.  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service (sqlbrowser) is not started.  
  
## User Action  
To resolve this error, try one of the following actions:  
  
-   Check the spelling of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance name that is specified in the connection string.  
  
-   Use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager tool to enable [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to accept remote connections over the TCP or named pipes protocols. For more information about the accepting remote connections, see [Enable or Disable a Server Network Protocol](../Topic/Enable%20or%20Disable%20a%20Server%20Network%20Protocol.md).  
  
-   Make sure that you have configured the firewall on the server instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to open ports for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser port (UDP 1434).  
  
-   Make sure that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service is started on the server.  
  
## See Also  
[Configure a Windows Firewall for Database Engine Access](../Topic/Configure%20a%20Windows%20Firewall%20for%20Database%20Engine%20Access.md)  
[Configure Client Protocols](../Topic/Configure%20Client%20Protocols.md)  
[Network Protocols and Network Libraries](../Topic/Network%20Protocols%20and%20Network%20Libraries.md)  
[Client Network Configuration](../Topic/Client%20Network%20Configuration.md)  
[Configure Client Protocols](../Topic/Configure%20Client%20Protocols.md)  
[Enable or Disable a Server Network Protocol](../Topic/Enable%20or%20Disable%20a%20Server%20Network%20Protocol.md)  
  
