---
title: "MSSQLSERVER_2574 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "2574 (Database Engine error)"
ms.assetid: efba507a-b5ad-4f1d-b0c8-f73b663a0562
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_2574
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|2574|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC_EMPTY_INDEX_TREE_LEVEL_PAGE|  
|Message Text|Table error: Page P_ID is empty in object ID O_ID, index ID I_ID, partition ID PN_ID, alloc unit ID A_ID (type TYPE). This is not permitted at level LEVEL of the B-tree.|  
  
## Explanation  
 A B-tree page above the leaf level of the specified index is empty, that is it has no rows. This behavior is possible for leaf-level pages in [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)], but has never been possible in tree levels.  
  
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
  
### Results of Running REPAIR Options  
 DBCC will rebuild the index.  
  
  
