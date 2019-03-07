---
title: "MSSQLSERVER_7932 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "7932 (Database Engine error)"
ms.assetid: e2ad218a-3249-4f18-8b32-09f0030765a5
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_7932
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|7932|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC2_FS_ROWSET_IN_WRONG_FILEGROUP|  
|Message Text|Table error: The FileStream directory ID F_ID for object ID O_ID, index ID I_ID, partition ID PN_ID is in filegroup FG_ID1, but should be in filegroup FG_ID2.|  
  
## Explanation  
 During DBCC CHECKDB, the FILESTREAM storage for the specified object was detected in the wrong filegroup. This could be a corruption in the metadata of the object.  
  
## User Action  
  
### Look for Hardware Failure  
 Run hardware diagnostics and correct any problems. Also examine the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows system and application logs and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log to see whether the error occurred as the result of hardware failure. Fix any hardware-related problems that are contained in the logs.  
  
 If you have persistent data corruption problems, try to swap out different hardware components to isolate the problem. Check to make sure that the system does not have write-caching enabled on the disk controller. If you suspect write-caching to be the problem, contact your hardware vendor.  
  
 Finally, you might find it useful to switch to a new hardware system. This switch may include reformatting the disk drives and reinstalling the operating system.  
  
### Restore from Backup  
 If the problem is not hardware related and a known clean backup is available, restore the database from the backup.  
  
### Run DBCC CHECKDB  
 Not applicable. This error cannot be repaired automatically. If you cannot restore the database from a backup, contact [!INCLUDE[msCoName](../../includes/msconame-md.md)] Customer Service and Support (CSS).  
  
  
