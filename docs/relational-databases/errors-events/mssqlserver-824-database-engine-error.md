---
title: "MSSQLSERVER_824 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "824 (Database Engine error)"
ms.assetid: 2aa22246-2712-4fdb-9744-36e7e6f3175e
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_824
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|824|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|B_HARDSSERR|  
|Message Text|SQL Server detected a logical consistency-based I/O error: %ls. It occurred during a %S_MSG of page %S_PGID in database ID %d at offset %#016I64x in file '%ls'.  Additional messages in the SQL Server error log or system event log may provide more detail.|  
  
## Explanation  
This error indicates that Windows reports that the page is successfully read from disk, but [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has discovered something wrong with the page. This error is similar to error 823 except that Windows did not detect the error. This usually indicates a problem in the I/O subsystem, such as a failing disk drive, disk firmware problems, faulty device driver, and so on. For more information about I/O errors, see [Microsoft SQL Server I/O Basics, Chapter 2](/sql/sql-server-2005/administrator/cc917726(v=technet.10)).  
  
## User Action  
  
### Look for Hardware Failure  
Run hardware diagnostics and correct any problems. Also examine the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows system and application logs and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log to see whether the error occurred because of hardware failure. Fix any hardware-related problems that are contained in the logs.  
  
If you have persistent data corruption problems, try to swap out different hardware components to isolate the problem. Check to make sure that the system does not have write-caching enabled on the disk controller. If you suspect write-caching to be the problem, contact your hardware vendor.  
  
Finally, you might find it useful to switch to a new hardware system. This switch may include reformatting the disk drives and reinstalling the operating system.  
  
### Restore from Backup  
If the problem is not hardware-related and a known clean backup is available, restore the database from the backup.  
  
Consider changing the databases to use the PAGE_VERIFY CHECKSUM option. For information about PAGE_VERIFY, see [ALTER DATABASE &#40;Transact-SQL&#41;](~/t-sql/statements/alter-database-transact-sql-set-options.md).  
  
## See Also  
[Manage the suspect_pages Table &#40;SQL Server&#41;](~/relational-databases/backup-restore/manage-the-suspect-pages-table-sql-server.md)  
  
