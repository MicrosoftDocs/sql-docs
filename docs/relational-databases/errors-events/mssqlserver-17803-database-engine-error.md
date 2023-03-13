---
title: "MSSQLSERVER_17803"
description: "MSSQLSERVER_17803"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "17803 (Database Engine error)"
---
# MSSQLSERVER_17803
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|17803|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SRV_NOMEMORY|  
|Message Text|There was a memory allocation failure during connection establishment. Reduce nonessential memory load, or increase system memory. The connection has been closed.%.*ls|  
  
## Explanation  
Server is running out of memory.  
  
## User Action  
Determine the cause for server running out of memory. Generic memory troubleshooting steps may be useful  
  
