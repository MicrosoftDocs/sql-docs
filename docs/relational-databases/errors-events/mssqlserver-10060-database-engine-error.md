---
title: "MSSQLSERVER_10060"
description: The SQL Server client cannot connect to the server. See an explanation of error 10060 and possible resolutions.
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
f1_keywords: 
  - "10060"
helpviewer_keywords: 
  - "10060 (Database Engine error)"
author: MashaMSFT
ms.author: mathoma
ms.custom: ""
ms.date: "04/04/2017"
---
# MSSQLSERVER_10060

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
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
[Configure a Windows Firewall for Database Engine Access](~/database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md)  
[Configure Client Protocols](~/database-engine/configure-windows/configure-client-protocols.md)  
[Network Protocols and Network Libraries](~/sql-server/install/network-protocols-and-network-libraries.md)  
[Client Network Configuration](~/database-engine/configure-windows/client-network-configuration.md)  
[Configure Client Protocols](~/database-engine/configure-windows/configure-client-protocols.md)  
[Enable or Disable a Server Network Protocol](~/database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md)  
  
