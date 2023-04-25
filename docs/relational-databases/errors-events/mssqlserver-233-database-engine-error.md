---
title: "MSSQLSERVER_233"
description: The SQL Server client cannot connect to the server. See an explanation of error 233 and possible resolutions.
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
f1_keywords:
  - "233"
helpviewer_keywords:
  - "233 (Database Engine error)"
---
# MSSQLSERVER_233
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|233|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name||  
|Message Text|A connection was successfully established with the server, but then an error occurred during the login process. (provider: Shared Memory Provider, error: 0 - No process is on the other end of the pipe.) (Microsoft SQL Server, Error: 233)|  
  
## Explanations  
The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] client cannot connect to an instance residing on the same host as the client. 
 
### Potential causes:
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]
- Unable to connect through the Shared Memory protocol.
- The instance has reached its maximum concurrent user connections limit
  
## User Action 
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]
Depending on the cause:
- Try to disable the Shared Memory protocol in Configuration Manager and use TCP/IP instead.
- Restart the instance in single user mode and make sure that the maximum number of concurrent user connections has not been changed from its default value of 0 (0 = unlimited)
  
## See Also  
[Network Protocols and Network Libraries](~/sql-server/install/network-protocols-and-network-libraries.md)
[Shared Memory Properties](~/tools/configuration-manager/shared-memory-properties.md)
[Client Network Configuration](~/database-engine/configure-windows/client-network-configuration.md)  
[Configure Client Protocols](~/database-engine/configure-windows/configure-client-protocols.md)  
[Enable or Disable a Server Network Protocol](~/database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md)
[Configure the user connections Server Configuration Option](~/database-engine/configure-windows/configure-the-user-connections-server-configuration-option.md)
  
