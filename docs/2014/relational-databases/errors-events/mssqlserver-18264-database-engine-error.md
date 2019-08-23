---
title: "MSSQLSERVER_18264 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "18264 (Database Engine error)"
ms.assetid: 3050fc56-2be5-43cf-916b-50a3ac5f89aa
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_18264
    
## Details  
  
|||  
|-|-|  
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
 [Trace Flags &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql)  
  
  
