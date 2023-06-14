---
title: "MSSQLSERVER_3314"
description: "MSSQLSERVER_3314"
author: MashaMSFT
ms.author: mathoma
ms.reviewer: jopilov
ms.date: 02/16/2023
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "3314 (Database Engine error)"
---
# MSSQLSERVER_3314

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| :--- | :--- |
| Product Name | SQL Server |
| Event ID | 3314 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | ERR_LOG_RID2 |
| Message Text | During undoing of a logged operation in database '%.*ls', an error occurred at log record ID %S_LSN. Typically, the specific failure is logged previously as an error in the Windows Event Log service. Restore the database or file from a backup, or repair the database. |

## Explanation

This error is a rollup error for undo recovery. This error indicates that SQL Server has placed the database into the SUSPECT state when it fails to roll back uncommitted transactions from database (undo). The transaction log file, the primary filegroup, and possibly other filegroups, may be damaged. The database failed recovery during startup of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and is therefore unavailable. You must take action to resolve the problem.

If this error occurs for `tempdb`, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance shuts down.

This error may appear with other errors in the SQL Server error log. Examples of such errors include [9001](mssqlserver-9002-database-engine-error.md), [823](mssqlserver-823-database-engine-error.md), [824](mssqlserver-824-database-engine-error.md), [17204](mssqlserver-17204-database-engine-error.md) (shows OS error when accessing a file), [17053](mssqlserver-17053-database-engine-error.md) (shows OS error). These last two errors may provide insight into the underlying reason for I/O failures.


## Cause

This error can be caused by a transient condition that existed on the system during an attempt to recover a database. This error can also be a result a permanent failure that occurs every time that you attempt to start the database. Examples of common causes include:

- The transaction log or database file(s) reside on a storage device that failed or isn't available
- Physically damaged file(s) that leads to inability to write to or read from them


## User Action

For information about the cause of this occurrence of error 3314, examine the Windows System Event Log and SQL Server error log for a previous error that indicates the specific failure. The appropriate user action depends on whether the information in the Windows Event Log indicates that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error was caused by a transient condition or a permanent failure. 

To address issues that lead to this error:

- Ensure that
  - the storage volumes where the database and log files reside are online
  - the entire I/O path from machine to storage is stable and doesn't lead to physical file damage
- Work with your hardware and device manufacturer to ensure that hardware and its configuration is suitable to the I/O requirements of a database system. Ensure that device drivers, firmware, BIOS and other supporting software components in the I/O path are up to date.
- Run DBCC CHECKDB to check the consistency of the database, if you can bring it online with a restart
- If the database and log files aren't intact and as a result the database can't come online, restore the last known good backup of the database
- For troubleshooting suggestions, see [MSSQLSERVER error 823](mssqlserver-823-database-engine-error.md) and [Troubleshoot database consistency errors reported by DBCC CHECKDB](/troubleshoot/sql/database-engine/database-file-operations/troubleshoot-dbcc-checkdb-errors)

When this error condition is encountered, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] may generate dump-related files in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **LOG** folder. The SQLDump*nnnn*.mdmp and SQLDump*nnnn*.txt files contains advanced diagnostic information relating to the failures, including the details about the transaction and the page that encountered the problem. This information can be used by the Microsoft team to analyze the reason for the failure.

## See also

- [DBCC CHECKDB (Transact-SQL)](~/t-sql/database-console-commands/dbcc-checkdb-transact-sql.md)
- [Complete Database Restores (Simple Recovery Model)](~/relational-databases/backup-restore/complete-database-restores-simple-recovery-model.md)
- [MSSQLSERVER_9001](~/relational-databases/errors-events/mssqlserver-9001-database-engine-error.md)
- [MSSQLSERVER_824](~/relational-databases/errors-events/mssqlserver-824-database-engine-error.md)
- [MSSQLSERVER_823](~/relational-databases/errors-events/mssqlserver-823-database-engine-error.md)
- [sys.databases (Transact-SQL)](~/relational-databases/system-catalog-views/sys-databases-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](~/t-sql/statements/alter-database-transact-sql-set-options.md)