---
title: "MSSQLSERVER_7986 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "7986 (Database Engine error)"
ms.assetid: ae64276c-4e1e-4ef3-9ee9-ebeb2f61e565
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_7986
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|7986|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC2_PRE_CHECKS_CROSS_OBJECT_LINKAGE|  
|Message Text|System table pre-checks: Object ID O_ID has cross-object chain linkage. Page P_ID1 points to P_ID2 in alloc unit ID A_ID1 (should be A_ID2). Check statement terminated due to unrepairable error.|  
  
## Explanation  
 The first phase of a DBCC CHECKDB is to do primitive checks on the data pages of critical system tables. If any errors are found, they cannot be repaired; therefore, the DBCC CHECKDB terminates immediately. The next page pointer of page *P_ID1* in the data level of the specified object points to a page, *P_ID2*, in a different object.  
  
## User Action  
  
### Look for Hardware Failure  
 Run hardware diagnostics and correct any problems. Also examine the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows system and application logs and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log to see whether the error occurred as the result of hardware failure. Fix any hardware-related problems that are contained in the logs.  
  
 If you have persistent data corruption problems, try to swap out different hardware components to isolate the problem. Check to make sure that the system does not have write-caching enabled on the disk controller. If you suspect write-caching to be the problem, contact your hardware vendor.  
  
 Finally, you might find it useful to switch to a new hardware system. This switch may include reformatting the disk drives and reinstalling the operating system.  
  
### Restore from Backup  
 If the problem is not hardware related and a known clean backup is available, restore the database from the backup.  
  
### Run DBCC CHECKDB  
 Not applicable. This error cannot be repaired automatically. If you cannot restore the database from a backup, contact [!INCLUDE[msCoName](../../includes/msconame-md.md)] Customer Service and Support (CSS).  
  
  
