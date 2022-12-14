---
title: "MSSQLSERVER_10061"
description: The server did not respond to the client request in SQL Server. See an explanation of the error and possible resolutions.
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
f1_keywords: 
  - "10061"
helpviewer_keywords: 
  - "10061 (Database Engine error)"
author: MashaMSFT
ms.author: mathoma
ms.custom: ""
ms.date: "04/04/2017"
---

# MSSQLSERVER_10061

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
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
[Manage the Database Engine Services](~/database-engine/configure-windows/manage-the-database-engine-services.md)  
[Configure Client Protocols](~/database-engine/configure-windows/configure-client-protocols.md)  
[Network Protocols and Network Libraries](~/sql-server/install/network-protocols-and-network-libraries.md)  
[Client Network Configuration](~/database-engine/configure-windows/client-network-configuration.md)  
[Configure Client Protocols](~/database-engine/configure-windows/configure-client-protocols.md)  
[Enable or Disable a Server Network Protocol](~/database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md)  
  
