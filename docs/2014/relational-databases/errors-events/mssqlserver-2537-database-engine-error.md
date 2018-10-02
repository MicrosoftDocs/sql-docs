---
title: "MSSQLSERVER_2537 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "2537 (Database Engine error)"
ms.assetid: 0af6ff69-d75a-4c39-8da2-9bd0695277c6
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_2537
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|2537|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC_RECORD_CHECK_FAILED|  
|Message Text|Table error: Object ID O_ID, index ID I_ID, partition ID PN_ID, alloc unit ID A_ID (type TYPE), page P_ID, row ROW_ID. Record check (CHECK_TEXT) failed. Values are VALUE1 and VALUE2.|  
  
## Explanation  
 The row ROW_ID (or a column in the row) failed the test or condition described by CHECK_TEXT.  
  
## User Action  
  
### Look for Hardware Failure  
 Run hardware diagnostics and correct any problems. Also examine the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows system and application logs and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log to see whether the error occurred as the result of hardware failure. Fix any hardware-related problems that are contained in the logs.  
  
 If you have persistent data corruption problems, try to swap out different hardware components to isolate the problem. Check to make sure that the system does not have write-caching enabled on the disk controller. If you suspect that write-caching is the problem, contact your hardware vendor.  
  
 Finally, you might find it useful to switch to a new hardware system. This switch may include reformatting the disk drives and reinstalling the operating system.  
  
### Restore from Backup  
 If the problem is not hardware related and a known clean backup is available, restore the database from the backup.  
  
### Run DBCC CHECKDB  
 If no clean backup is available, run DBCC CHECKDB without a REPAIR clause to determine the extent of the corruption. DBCC CHECKDB will recommend a REPAIR clause to use. Then, run DBCC CHECKDB with the recommended REPAIR clause.  
  
> [!CAUTION]  
>  If you are not sure what effect DBCC CHECKDB with a REPAIR clause has on your data, contact your primary support provider before running this statement.  
  
 If running DBCC CHECKDB with one of the REPAIR clauses does not correct the problem, contact your primary support provider.  
  
### Results of Running REPAIR Options  
 REPAIR will rebuild the index if an index exists.  
  
 **Caution** This repair may cause data loss.  
  
  
