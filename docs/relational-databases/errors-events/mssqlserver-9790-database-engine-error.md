---
title: "MSSQLSERVER_9790"
description: "MSSQLSERVER_9790"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "9790 (Database Engine error)"
---
# MSSQLSERVER_9790
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|9790|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SB3_XMIT_ERROR_ENTRANCE_BROKER_SINGLE_USER|  
|Message Text|Unable to route the incoming message. The system database MSDB containing routing information is in SINGLE USER mode.|  
  
## Explanation  
An error occurred while trying to classify a message received off the wire because the MSDB database was in Single User mode.  
  
## User Action  
Change MSDB to be in MULTI USER mode with the ALTER DATABASE command.  
  
