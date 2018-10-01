---
title: "MSSQLSERVER_3456 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "3456 (Database Engine error)"
ms.assetid: d11b2b2c-3ae4-4023-b82f-05b561bfacce
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_3456
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|3456|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|REC_REDOLSNMISMATCH|  
|Message Text|Could not redo log record %S_LSN, for transaction ID %S_XID, on page %S_PGID, database '%.*ls' (database ID %d). Page: LSN = %S_LSN, type = %ld. Log: OpCode = %ld, context %ld, PrevPageLSN: %S_LSN. Restore from a backup of the database, or repair the database.|  
  
## Explanation  
The restore operation was unable to redo the transaction log. This error has placed the database into the SUSPECT state. The primary filegroup, and possibly other filegroups, are suspect and may be damaged. The database cannot be recovered during startup of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and is therefore unavailable. User action is required to resolve the problem.  
  
Note that if this error occurs for **tempdb**, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance shuts down.  
  
## User Action  
This error can be caused by a transient condition that existed on the system during a given attempt to start up the server instance or to recover a database. This error can also be caused by a permanent failure that occurs every time that you attempt to start the database. For information about the cause, examine the Windows Event Log for a previous error that indicates the specific failure.  
  
For information about the cause of this occurrence of error 3456, examine the Windows Event Log for a previous error that indicates the specific failure. The appropriate user action depends on whether the information in the Windows Event Log indicates that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error was caused by a transient condition or a permanent failure. For information about the user actions for troubleshooting error 3456, see [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## See Also  
[ALTER DATABASE &#40;Transact-SQL&#41;](~/t-sql/statements/alter-database-transact-sql-set-options.md)  
[DBCC CHECKDB &#40;Transact-SQL&#41;](~/t-sql/database-console-commands/dbcc-checkdb-transact-sql.md)  
[Complete Database Restores &#40;Simple Recovery Model&#41;](~/relational-databases/backup-restore/complete-database-restores-simple-recovery-model.md)  
[MSSQLSERVER_824](~/relational-databases/errors-events/mssqlserver-824-database-engine-error.md)  
[sys.databases &#40;Transact-SQL&#41;](~/relational-databases/system-catalog-views/sys-databases-transact-sql.md)  
  
