---
description: "MSSQLSERVER_18264"
title: "MSSQLSERVER_18264 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "18264 (Database Engine error)"
ms.assetid: 3050fc56-2be5-43cf-916b-50a3ac5f89aa
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_18264
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|Microsoft SQL Server|  
|Event ID|18264|  
|Event Source|MSSQLENGINE|  
|Component|SQLEngine|  
|Symbolic Name|STRMIO_DBDUMP|  
|Message Text|Database backed up. Database: %s, creation date(time): %s(%s), pages dumped: %d, first LSN: %s, last LSN: %s, number of dump devices: %d, device information: (%s). This is an informational message only. No user action is required.|  
  
## Explanation  
By default, every successful backup adds this informational message to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log and the system event log. If you very frequently back up the transaction log, these messages can accumulate quickly, creating very large error logs that can make finding other messages difficult.  
  
## User Action  
You can suppress these log entries by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] trace flag **3226**. Enabling this trace flag is useful if you are running frequent log backups and if none of your scripts depend on those entries.  
  
For information about using trace flags, see SQL Server Books Online.  
  
## See Also  
[Trace Flags &#40;Transact-SQL&#41;](~/t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md)  
  
