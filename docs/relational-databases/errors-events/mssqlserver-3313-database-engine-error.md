---
description: "MSSQLSERVER_3313"
title: "MSSQLSERVER_3313 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "3313 (Database Engine error)"
ms.assetid: a244227b-8553-42df-9435-034f906c4c74
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_3313
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|3313|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|ERR_LOG_RID1|  
|Message Text|During redoing of a logged operation in database '%.*ls', an error occurred at log record ID %S_LSN. Typically, the specific failure is previously logged as an error in the Windows Event Log service. Restore the database from a full backup, or repair the database.|  
  
## Explanation  
This is a roll-up error for redo recovery. This error has placed the database into the SUSPECT state. The primary filegroup, and possibly other filegroups, are suspect and may be damaged. The database cannot be recovered during startup of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and is therefore unavailable. User action is required to resolve the problem.  
  
Note that if this error occurs for **tempdb**, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance shuts down.  
  
## User Action  
This error can be caused by a transient condition that existed on the system during a given attempt to start up the server instance or to recover a database. This error can also be caused by a permanent failure that occurs every time that you attempt to start the database. For information about the cause, examine the Windows Event Log for a previous error that indicates the specific failure.  
  
Note that when this error condition is encountered, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] typically generates three files in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **LOG** folder. The SQLDump*nnnn*.txt file contains advanced diagnostic information relating to the failures, including the details about the transaction and the page that encountered the problem. This information is typically used by the Product Support team to analyze the reason for the failure.  
  
For information about the cause of this occurrence of error 3313, examine the Windows Event Log for a previous error that indicates the specific failure. The appropriate user action depends on whether the information in the Windows Event Log indicates that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error was caused by a transient condition or a permanent failure. For information about the user actions for troubleshooting error 3313, see [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## See Also  
[ALTER DATABASE &#40;Transact-SQL&#41;](~/t-sql/statements/alter-database-transact-sql-set-options.md)  
[DBCC CHECKDB &#40;Transact-SQL&#41;](~/t-sql/database-console-commands/dbcc-checkdb-transact-sql.md)  
[Complete Database Restores &#40;Simple Recovery Model&#41;](~/relational-databases/backup-restore/complete-database-restores-simple-recovery-model.md)  
[MSSQLSERVER_824](~/relational-databases/errors-events/mssqlserver-824-database-engine-error.md)  
[sys.databases &#40;Transact-SQL&#41;](~/relational-databases/system-catalog-views/sys-databases-transact-sql.md)  
  
