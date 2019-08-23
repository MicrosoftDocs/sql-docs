---
title: "MSSQLSERVER_7903 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "7903 (Database Engine error)"
ms.assetid: 991a86df-42cd-435e-85b3-f42e4cb13039
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_7903
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|7903|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC2_FS_ORPHANED_FILE|  
|Message Text|Table error: The orphaned file 'FILE' was found in the Filestream directory for object ID O_ID, index ID I_ID, partition ID PN_ID, column ID C_ID.|  
  
## Explanation  
 A FILESTREAM file was found in a FILESTREAM column directory; however, the corresponding column value in the partition is missing.  
  
## User Action  
  
### Look for Hardware Failure  
 Run hardware diagnostics and correct any problems. Also examine the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows system and application logs and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log to see whether the error occurred as the result of hardware failure. Fix any hardware-related problems that are contained in the logs.  
  
 If you have persistent data corruption problems, try to swap out different hardware components to isolate the problem. Check to make sure that the system does not have write-caching enabled on the disk controller. If you suspect write-caching to be the problem, contact your hardware vendor.  
  
 Finally, you might find it useful to switch to a new hardware system. This switch may include reformatting the disk drives and reinstalling the operating system.  
  
### Restore from Backup  
 If the problem is not hardware related and a known clean backup is available, restore the database from the backup.  
  
### Run DBCC CHECKDB  
 Not applicable. This error cannot be repaired. If you cannot restore the database from a backup, contact [!INCLUDE[msCoName](../../includes/msconame-md.md)] Customer Service and Support (CSS).  
  
  
