---
title: "MSSQLSERVER_3414 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "3414 (Database Engine error)"
ms.assetid: f25852f9-b91c-4356-b817-78bec9ec8db4
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_3414
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|3414|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|REC_GIVEUP|  
|Message Text|An error occurred during recovery, preventing the database '%.*ls' (database ID %d) from restarting. Diagnose the recovery errors and fix them, or restore from a known good backup. If errors are not corrected or expected, contact Technical Support.|  
  
## Explanation  
 The specified database was recovered, but failed to start, because errors occurred during recovery. This error has placed the database into the SUSPECT state. The primary filegroup, and possibly other filegroups, are suspect and may be damaged. The database cannot be recovered during startup of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and is therefore unavailable. User action is required to resolve the problem.  
  
 Note that if this error occurs for **tempdb**, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance shuts down.  
  
## User Action  
 This error can be caused by a transient condition that existed on the system during a given attempt to start up the server instance or to recover a database. This error can also be caused by a permanent failure that occurs every time that you attempt to start the database. For information about the cause, examine the Windows Event Log for a previous error that indicates the specific failure.  
  
 For information about the cause of this occurrence of error 3414, examine the Windows Event Log for a previous error that indicates the specific failure. The appropriate user action depends on whether the information in the Windows Event Log indicates that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error was caused by a transient condition or a permanent failure. For information about the user actions for troubleshooting error 3414, see [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## See Also  
 [ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql)   
 [DBCC CHECKDB &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-checkdb-transact-sql)   
 [Complete Database Restores &#40;Simple Recovery Model&#41;](../backup-restore/complete-database-restores-simple-recovery-model.md)   
 [MSSQLSERVER_824](mssqlserver-824-database-engine-error.md)   
 [sys.databases &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-databases-transact-sql)  
  
  
