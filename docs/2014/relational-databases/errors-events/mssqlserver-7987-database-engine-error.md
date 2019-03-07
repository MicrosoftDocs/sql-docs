---
title: "MSSQLSERVER_7987 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "7987 (Database Engine error)"
ms.assetid: 314aebf1-6cdf-488d-a274-ce967fadb57b
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_7987
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|7987|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC2_PRE_CHECKS_CHAIN_LINKAGE_MISMATCH|  
|Message Text|System table pre-checks: Object ID O_ID has chain linkage mismatch. P_ID1->next = P_ID2, but P_ID2->prev = P_ID3. Check statement terminated because of an irreparable error.|  
  
## Explanation  
 The first phase of a DBCC CHECKDB is to do primitive checks on the data pages of critical system tables. If any errors are found, they cannot be repaired; therefore, the DBCC CHECKDB terminates immediately.  
  
 The next page pointer of page *P_ID1* points to page *P_ID2*; however, the previous page pointer of page *P_ID2* points to page *P_ID3* but not back to page *P_ID1*, as it should.  
  
## User Action  
  
### Look for Hardware Failure  
 Run hardware diagnostics and correct any problems. Also examine the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows system and application logs and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log to see whether the error occurred as the result of hardware failure. Fix any hardware-related problems that are contained in the logs.  
  
 If you have persistent data corruption problems, try to swap out different hardware components to isolate the problem. Check to make sure that the system does not have write-caching enabled on the disk controller. If you suspect write-caching to be the problem, contact your hardware vendor.  
  
 Finally, you might find it useful to switch to a new hardware system. This switch may include reformatting the disk drives and reinstalling the operating system.  
  
### Restore from Backup  
 If the problem is not hardware related and a known clean backup is available, restore the database from the backup.  
  
### Run DBCC CHECKDB  
 Not applicable. This error cannot be repaired automatically. If you cannot restore the database from a backup, contact [!INCLUDE[msCoName](../../includes/msconame-md.md)] Service and Support (CSS).  
  
  
