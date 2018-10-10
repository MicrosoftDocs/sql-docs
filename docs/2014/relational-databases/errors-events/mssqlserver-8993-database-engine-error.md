---
title: "MSSQLSERVER_8993 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "8993 (Database Engine error)"
ms.assetid: 06aac110-a41c-4853-bc8e-a83e8535b8be
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_8993
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|8993|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC3_MISSING_FORWARDED_ROW|  
|Message Text|Object ID O_ID, forwarding row page P_ID1, slot S_ID1 points to page P_ID2, slot S_ID2. Did not encounter forwarded row. Possible allocation error.|  
  
## Explanation  
 A forwarding row in a heap points to a nonexistent forwarded row.  
  
## User Action  
  
### Look for Hardware Failure  
 Run hardware diagnostics and correct any problems. Also examine the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows system and application logs and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log to see whether the error occurred as the result of hardware failure. Fix any hardware-related problems that are contained in the logs.  
  
 If you have persistent data corruption problems, try to swap out different hardware components to isolate the problem. Check to make sure that the system does not have write-caching enabled on the disk controller. If you suspect write-caching to be the problem, contact your hardware vendor.  
  
 Finally, you might find it useful to switch to a new hardware system. This switch may include reformatting the disk drives and reinstalling the operating system.  
  
### Restore from Backup  
 If the problem is not hardware related and a known clean backup is available, restore the database from the backup.  
  
### Run DBCC CHECKDB  
 If no clean backup is available, run DBCC CHECKDB without a REPAIR clause to determine the extent of the corruption. DBCC CHECKDB will recommend a REPAIR clause to use. Then, run DBCC CHECKDB with the appropriate REPAIR clause to repair the corruption.  
  
> [!CAUTION]  
>  If you are not sure what effect DBCC CHECKDB with a REPAIR clause has on your data, contact your primary support provider before running this statement.  
  
 If running DBCC CHECKDB with one of the REPAIR clauses does not correct the problem, contact your primary support provider.  
  
#### Results of Running REPAIR Options  
 The forwarding row will be deleted and any nonclustered indexes will be rebuilt.  
  
  
