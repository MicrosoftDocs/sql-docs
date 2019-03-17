---
title: "MSSQLSERVER_5243 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "5243 (Database Engine error)"
ms.assetid: e04a1934-e57d-420e-ac79-97071745824e
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_5243
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|5243|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name||  
|Message Text|An inconsistency was detected during an internal operation. Please contact technical support. Reference number %ld.|  
  
## Explanation  
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] detected a structural inconsistency in an in-memory storage engine structure.  
  
## User Action  
Look for Hardware Failure. Run hardware diagnostics and correct any problems. Also examine the Windows system and application logs and the SQL Server error log to see whether the error occurred as the result of hardware failure. Fix any hardware\-related problems that are contained in the logs.

If you have persistent data corruption problems, try to swap out different hardware components to isolate the problem. Check to make sure that the system does not have write\-caching enabled on the disk controller. If you suspect write\-caching to be the problem, contact your hardware vendor.

Finally, you might find it useful to switch to a new hardware system. This switch may include reformatting the disk drives and reinstalling the operating system.

Restore from BackupIf the problem is not hardware related and a known clean backup is available, restore the database from the backup.

Run DBCC CHECKDBIf no clean backup is available, run DBCC CHECKDB without a REPAIR clause to determine the extent of the corruption. DBCC CHECKDB will recommend a REPAIR clause to use. Then, run DBCC CHECKDB with the appropriate REPAIR clause to repair the corruption.

> **alert tag is not supported!!!!**
> **tr tag is not supported!!!!**
> **tr tag is not supported!!!!**

If running DBCC CHECKDB with one of the REPAIR clauses does not correct the problem, contact your primary support provider.
  
## See Also  
[DBCC CHECKDB &#40;Transact-SQL&#41;](~/t-sql/database-console-commands/dbcc-checkdb-transact-sql.md)  
[Database Files and Filegroups](~/relational-databases/databases/database-files-and-filegroups.md)  
  
