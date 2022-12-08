---
description: "MSSQLSERVER_3414"
title: "MSSQLSERVER_3414 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "3414 (Database Engine error)"
ms.assetid: f25852f9-b91c-4356-b817-78bec9ec8db4
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_3414
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|3414|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|REC_GIVEUP|  
|Message Text|An error occurred during recovery, preventing the database '%.*ls' (database ID %d) from restarting. Diagnose the recovery errors and fix them, or restore from a known good backup. If errors are not corrected or expected, contact Technical Support.|  
  
## Explanation  
The specified database was recovered, but failed to start, because errors occurred during recovery. This error has put the database in the SUSPECT state. The primary filegroup, and possibly other filegroups, are suspect and may be damaged. The database cannot be recovered during startup of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and is therefore unavailable. User action is required to resolve the problem. You will see the SUSPECT status in both SQL Server Management Studio (next to the database icon) and when you look at the sys.databases.state_desc column. Any attempt to use a database in this state will result in the following error:

```
Msg 926, Level 14, State 1, Line 1 
Database 'mydb' cannot be opened. It has been marked SUSPECT by recovery. See the SQL Server errorlog for more information
```
  
Note that when this error occurs in **tempdb**, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance shuts down.  

## Cause
This error can be caused by a transient condition that existed on the system during a given attempt to start up the server instance or to recover a database. This error can also be caused by a permanent failure that occurs every time that you attempt to start the database. The cause of the recovery failure is typically found in error(s) that precedes Error 3414 in the ERRORLOG or Event Log. The preceding error in the log file contains the same spid\<n\> value. For example, the following recovery failure is due to a checksum error when trying to read a log block. Note *spid15s* is present in all lines:

```
2020-03-31 17:33:13.00 spid15s     Error: 824, Severity: 24, State: 4.  
2020-03-31 17:33:13.00 spid15s     SQL Server detected a logical consistency-based I/O error: (bad checksum). It occurred during a read of page (0:-1) in database ID 13 at offset 0x0000000000b800 in file 'C:\Program Files\Microsoft SQL Server\MSSQL10.SQL2008\MSSQL\DATA\mydb_log.LDF'.  Additional messages in the SQL Server error log or system event log may provide more detail. This is a severe error condition that threatens database integrity and must be corrected immediately. Complete a full database consistency check (DBCC CHECKDB). This error can be caused by many factors; for more information, see SQL Server Books Online.   
2020-03-31 17:33:13.16 spid15s     Error: 3414, Severity: 21, State: 1.  
2020-03-31 17:33:13.16 spid15s     An error occurred during recovery, preventing the database 'mydb' (database ID 13) from restarting. Diagnose the recovery errors and fix them, or restore from a known good backup. If errors are not corrected or expected, contact Technical Support
```


There are a wide range of errors that could cause database recovery to fail. While you must evaluate each error on a case by case basis, the resolution to a database recovery failure is typically the same as described in the User Action section below.

## User action  
 
For information about the cause of this occurrence of error 3414, examine the Windows Event Log or ERRORLOG for a previous error that indicates the specific failure. The appropriate user action depends on whether the information in the Windows Event Log indicates that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error was caused by a transient condition or a permanent failure. 
The error message states to "diagnose recovery errors and fix them, or restore from a known good backup". Therefore, you can attempt to correct the error that you encounter to allow recovery to complete (see [Correctable errors and deferred transactions](#correctable-errors-and-deferred-transactions)).

If the errors cannot be corrected, the first and best option to resolve this problem is to restore from a good backup. However, if you cannot recover from a backup, you have two additional options, which do not guarantee full data recovery: use emergency repair with [DBCC CHECKDB](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md) or attempt to copy out as much data as possible to another database. 

 1. Restore from the last known good database backup
 1. Use the emergency repair method provided by [DBCC CHECKDB](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md)
 1. Attempt to copy out as much data as possible to another database.

The first method of restoring a good database backup is the best choice to bring a database to a known consistent state.  

The second best choice, if no backup is available, is to get the database online and accessible. However, you must realize that transactional consistency cannot be guaranteed since recovery failed. There is no way to know what transactions should have been rolled back or rolled forward but were not allowed because of the recovery failure. The steps to proceed with emergency repair are described in the section titled [Resolving Database Errors in Emergency Mode](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md#resolving-errors-in-database-emergency-mode) in the DBCC CHECKDB documentation. 

If emergency repair does not work and you want to try to salvage some data to another database, the way to get access to the database is by setting the database in emergency mode via the ALTER DATABASE \<dbname\> SET EMERGENCY command. Then you can attempt to copy data out from tables.

### Correctable errors and deferred transactions
Not all errors encountered during database recovery will result in a recovery failure and a suspect database:

Errors when opening the database and/or transaction log files for the first time, occur before recovery. Examples of such errors are  [17204](mssqlserver-17204-database-engine-error.md) and [17207](mssqlserver-17207-database-engine-error.md). Once these errors are corrected, recovery may be allowed to proceed (but not guaranteed to complete if other recovery errors occur). Errors such as 17204 and 17207 do not result in a SUSPECT database. In fact, the status of the database is RECOVERY_PENDING when these problems occur. 

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] allows recovery to complete even when a page-level error occurs and will still maintain transactional consistency. This process has reduced the number of scenarios resulting in a SUSPECT database. This concept is referred to as [deferred transactions](../backup-restore/deferred-transactions-sql-server.md).

If the error encountered during recovery indicates a problem with a database page, for example as a checksum error or Msg 824, recovery may be allowed to complete with errors pending. In the case where a transaction is uncommitted, an error on a page can result in a [deferred transaction](../backup-restore/deferred-transactions-sql-server.md) allowing recovery to complete.  

The following ERRORLOG entries show an example of a Msg [824](mssqlserver-824-database-engine-error.md) error encountered during recovery but recovery was allowed to complete with a deferred transaction. Note the absence of Error 3414 in this situation and the message that recovery has completed for the database:

```2010-03-31 19:17:18.45 spid7s      Error: 824, Severity: 24, State: 2.   
2010-03-31 19:17:18.45 spid7s      SQL Server detected a logical consistency-based I/O error: incorrect checksum (expected: 0xb2c87a0a; actual: 0xb6c0a5e2). It occurred during a read of page (1:153) in database ID 13 at offset 0x00000000132000 in file 'C:\Program Files\Microsoft SQL Server\MSSQL10.SQL2008\MSSQL\DATA\mydb.mdf'.  Additional messages in the SQL Server error log or system event log may provide more detail. This is a severe error condition that threatens database integrity and must be corrected immediately. Complete a full database consistency check (DBCC CHECKDB). This error can be caused by many factors; for more information, see SQL Server Books Online.   
2010-03-31 19:17:18.45 spid7s      Error: 3314, Severity: 21, State: 1.   
2010-03-31 19:17:18.45 spid7s      During undoing of a logged operation in database 'mydb', an error occurred at log record ID (25:100:19). Typically, the specific failure is logged previously as an error in the Windows Event Log service. Restore the database or file from a backup, or repair the database.
2010-03-31 19:17:18.45 spid7s      Errors occurred during recovery while rolling back a transaction. The transaction was deferred. Restore the bad page or file, and re-run recovery.   
2010-03-31 19:17:18.45 spid7s      Recovery completed for database mydb (database ID 13) in 2 second(s) (analysis 204 ms, redo 25 ms, undo 1832 ms.) This is an informational message only. No user action is required.   
```

If a committed transaction is to be rolled forward, the page can be marked inaccessible (any future attempts to access the page result in Msg 829) and recovery can complete. In this situation, the error must be corrected by restoring the page from a backup or by deallocating the page using DBCC CHECKDB with repair.


  
## See also  
[ALTER DATABASE &#40;Transact-SQL&#41;](~/t-sql/statements/alter-database-transact-sql-set-options.md)  
[DBCC CHECKDB &#40;Transact-SQL&#41;](~/t-sql/database-console-commands/dbcc-checkdb-transact-sql.md)  
[Complete Database Restores &#40;Simple Recovery Model&#41;](~/relational-databases/backup-restore/complete-database-restores-simple-recovery-model.md)  
[Deferred Transactions](../backup-restore/deferred-transactions-sql-server.md)  
[MSSQLSERVER_824](~/relational-databases/errors-events/mssqlserver-824-database-engine-error.md)  
[sys.databases &#40;Transact-SQL&#41;](~/relational-databases/system-catalog-views/sys-databases-transact-sql.md)

  
