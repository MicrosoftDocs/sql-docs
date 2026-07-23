---
title: DBCC SHRINKFILE (Transact-SQL)
description: DBCC SHRINKFILE shrinks the size of a database file.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: umajay, dpless, randolphwest, dfurman
ms.date: 07/23/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "SHRINKFILE"
  - "DBCC_SHRINKFILE_TSQL"
  - "DBCC SHRINKFILE"
  - "SHRINKFILE_TSQL"
helpviewer_keywords:
  - "data shrinking [SQL Server]"
  - "TRUNCATEONLY option"
  - "shrinking files"
  - "NOTRUNCATE option"
  - "shrinking databases"
  - "decreasing database size"
  - "file shrinking [SQL Server]"
  - "database shrinking [SQL Server]"
  - "logs [SQL Server], shrinking"
  - "reducing database size"
  - "DBCC SHRINKFILE statement"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azure-sqldw-latest || =azuresqldb-mi-current || =fabric-sqldb"
---

# DBCC SHRINKFILE (Transact-SQL)

[!INCLUDE [SQL Server SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Shrinks the current database's specified data or log file size. You can use it to move data from one file to other files in the same filegroup, which empties the file and allows for its database removal. You can shrink a file to less than its size at creation, resetting the minimum file size to the new value.

Use `DBCC SHRINKFILE` only when necessary because shrink is a long-running and resource-intensive operation.

> [!NOTE]  
> Don't treat shrink operations as regular maintenance. Data and log files that grow due to regular, recurring business operations don't require shrink operations.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DBCC SHRINKFILE
(
    { file_name | file_id }
    { [ , EMPTYFILE ]
    | [ [ , target_size ] [ , { NOTRUNCATE | TRUNCATEONLY } ] ]
    }
)
[ WITH
  {
      [ WAIT_AT_LOW_PRIORITY
        [ (
            <wait_at_low_priority_option_list>
        ) ]
      ]
      [ , NO_INFOMSGS ]
  }
]

<wait_at_low_priority_option_list> ::=
    <wait_at_low_priority_option>
    | <wait_at_low_priority_option_list> , <wait_at_low_priority_option>

<wait_at_low_priority_option> ::=
    ABORT_AFTER_WAIT = { SELF | BLOCKERS }
```

## Arguments

#### *file_name*

The logical name of the file to shrink.

#### *file_id*

The identification (ID) number of the file to shrink. To get a file ID, use the [FILE_IDEX](../functions/file-idex-transact-sql.md) system function or query the [sys.database_files](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md) catalog view in the current database.

#### *target_size*

An integer representing the file's new megabyte size. If you set *target_size* to `0` or don't specify it, `DBCC SHRINKFILE` reduces the file to its creation size.

You can reduce an empty file's default size using `DBCC SHRINKFILE <target_size>`. For example, if you create a 5-MB file and then shrink the file to 3 MB while the file is still empty, the default file size is set to 3 MB. This applies only to empty files that have never contained data.

This option isn't supported for FILESTREAM filegroup containers.

If specified, `DBCC SHRINKFILE` tries to shrink the file to *target_size*. Used pages in the file's area to be freed are moved to free space in the file's kept areas. For example, with a 10-MB data file, a `DBCC SHRINKFILE` operation with an `8` *target_size* moves all used pages in the file's last 2 MB into any unallocated pages in the file's first 8 MB. `DBCC SHRINKFILE` doesn't shrink a file past the needed stored data size. For example, if 7 MB of a 10-MB data file is used, a `DBCC SHRINKFILE` statement with a *target_size* of 6 shrinks the file to only 7 MB, not 6 MB.

If you specify *target_size* with `TRUNCATEONLY`, `DBCC SHRINKFILE` might not release free space at the end of the file.

#### EMPTYFILE

Migrates all data from the specified file to other files in the *same filegroup*. In other words, `EMPTYFILE` migrates data from a specified file to other files in the same filegroup. `EMPTYFILE` assures you that no new data gets added to the file, despite this file not being read-only. You can use the [ALTER DATABASE](../statements/alter-database-transact-sql.md) statement to remove a file. If you use the [ALTER DATABASE](../statements/alter-database-transact-sql.md) statement to change file size, the read-only flag is reset, and data can be added.

For FILESTREAM filegroup containers, you can't use `ALTER DATABASE` to remove a file until the FILESTREAM Garbage Collector has run and deleted all the unnecessary filegroup container files that `EMPTYFILE` has copied to another container. For more information, see [sp_filestream_force_garbage_collection](../../relational-databases/system-stored-procedures/filestream-and-filetable-sp-filestream-force-garbage-collection.md). For information on removing a FILESTREAM container, see the corresponding section in [ALTER DATABASE File and Filegroup Options](../statements/alter-database-transact-sql-file-and-filegroup-options.md)

`EMPTYFILE` isn't supported in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] Hyperscale, or [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)].

#### NOTRUNCATE

Moves allocated pages from a data file's end to unallocated pages in a file's front with or without specifying *target_percent*. The free space at the file's end isn't returned to the operating system, and the file's physical size doesn't change. Therefore, if `NOTRUNCATE` is specified, the file appears not to shrink.

`NOTRUNCATE` is applicable only to data files. The log files aren't affected.

This option isn't supported for FILESTREAM filegroup containers.

#### TRUNCATEONLY

Releases all free space at the file's end to the operating system but doesn't perform any page movement inside the file. The data file is shrunk only to the last allocated extent.

If *target_size* is specified with `TRUNCATEONLY`, free space at the end of the file might not be released.

The `TRUNCATEONLY` option doesn't move information in the log, but does remove inactive virtual log files (VLFs) from the end of the log file. This option isn't supported for FILESTREAM filegroup containers.

#### WITH NO_INFOMSGS

Suppresses all informational messages.

### WAIT_AT_LOW_PRIORITY with shrink operations

**Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)]

The wait at low priority feature reduces lock contention during the shrink operation. For more information, see [Understanding concurrency issues with DBCC SHRINKFILE](#understand-concurrency-issues-with-dbcc-shrinkfile).

This feature is similar to the [WAIT_AT_LOW_PRIORITY with online index operations](../statements/alter-table-transact-sql.md#wait_at_low_priority), with some differences.

- You can't specify the `ABORT_AFTER_WAIT` option `NONE`.
- You can't set the `MAX_DURATION` option. The low priority lock timeout for a shrink operation is always one minute.

#### WAIT_AT_LOW_PRIORITY

When a shrink command is executed in `WAIT_AT_LOW_PRIORITY` mode, queries requiring schema stability (`Sch-S`) locks on the [Index Allocation Map (IAM) pages](../../relational-databases/pages-and-extents-architecture-guide.md#iam-pages) aren't blocked by the shrink operation. However, the shrink operation can be blocked by a `Sch-S` lock on an IAM page. Shrink continues to execute only when it's able to obtain a schema modify lock (`Sch-M`) lock on an IAM page it requires.

If a shrink operation in `WAIT_AT_LOW_PRIORITY` mode can't obtain this lock due to a long-running query holding a `Sch-S` lock, the shrink operation times out with error 49516, for example: `Msg 49516, Level 16, State 1, Line 134 Shrink timeout waiting to acquire schema modify lock in WLP mode to process IAM pageID 1:2865 on database ID 5`.

#### { ABORT_AFTER_WAIT = [ SELF | BLOCKERS ] *}*

**Applies to**: [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions), [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)].

- `SELF`

  `SELF` is the default option. Exit the shrink file operation currently being executed without taking any further action.

- `BLOCKERS`

  Kill all user transactions that block the shrink file operation so that the operation can continue. The `BLOCKERS` option requires the login to have the `ALTER ANY CONNECTION` or `KILL DATABASE CONNECTION` permission.

<a id="result-sets"></a>

## Result set

The following table describes result set columns.

| Column name | Description |
| --- | --- |
| `DbId` | Database identification number of the file the [!INCLUDE [ssDE](../../includes/ssde-md.md)] tried to shrink. |
| `FileId` | The file identification number of the file the [!INCLUDE [ssDE](../../includes/ssde-md.md)] tried to shrink. |
| `CurrentSize` | Number of 8-KB pages the file currently occupies. |
| `MinimumSize` | Number of 8-KB pages the file could occupy, at minimum. This number corresponds to the minimum size or originally created size of a file. |
| `UsedPages` | Number of 8-KB pages currently used by the file. |
| `EstimatedPages` | Number of 8-KB pages that the [!INCLUDE [ssDE](../../includes/ssde-md.md)] estimates the file could be shrunk down to. |

## Remarks

`DBCC SHRINKFILE` applies to the current database's files. For more information about how to change the current database, see [USE](../language-elements/use-transact-sql.md).

You can stop `DBCC SHRINKFILE` operations at any point and any completed work is preserved. If you use the `EMPTYFILE` parameter and cancel the operation, the file isn't marked to prevent additional data from being added.

Other users can work in the database during file shrinking; the database doesn't have to be in single-user mode. You don't have to run the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] in single-user mode to shrink the system databases.

### Known issues

**Applies to**: [!INCLUDE [sql-server](../../includes/ssnoversion-md.md)], [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] dedicated SQL pool

- In versions [!INCLUDE [sql-server](../../includes/ssnoversion-md.md)] earlier than [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)], the pages used by large object (LOB) column types (**varbinary(max)**, **varchar(max)**, and **nvarchar(max)**) in compressed columnstore segments can't be moved by `DBCC SHRINKDATABASE` and `DBCC SHRINKFILE`. For more information, see [What's new in columnstore indexes](../../relational-databases/indexes/columnstore-indexes-what-s-new.md#sql-server-2025-17x).

### Understand concurrency issues with DBCC SHRINKFILE

The shrink database and shrink file commands can lead to concurrency issues, especially with active maintenance such as rebuilding indexes, or in busy online transaction processing (OLTP) environments.

For example, a user query might acquire a schema stability (`Sch-S`) lock on an Index Allocation Map (IAM) page and hold it until completion. When attempting to reclaim space during regular usage, shrink database and shrink file operations require a schema modification (`Sch-M`) lock when moving or deleting IAM pages, blocking the `Sch-S` locks needed by user queries. As a result, long-running queries can block a shrink operation. This also means that any new query requiring a `Sch-S` lock on an IAM page can queue behind the shrink operation, further exacerbating this concurrency issue.

Introduced in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], the wait at low priority feature for shrink operations addresses this problem by taking the schema modify lock on IAM pages in the `WAIT_AT_LOW_PRIORITY` mode. For more information, see [WAIT_AT_LOW_PRIORITY with shrink operations](#wait_at_low_priority-with-shrink-operations).

For more information on `Sch-S` and `Sch-M` locks, see [Transaction locking and row versioning guide](../../relational-databases/sql-server-transaction-locking-and-row-versioning-guide.md).

<a id="shrinking-a-log-file"></a>

## Shrink a log file

For log files, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] uses *target_size* to calculate the whole log's target size. Therefore, *target_size* is the log's free space after the shrink operation. The whole log's target size is then translated to each log file's target size. `DBCC SHRINKFILE` tries to shrink each physical log file to its target size immediately. However, if part of the logical log resides in the virtual logs beyond the target size, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] frees as much space as possible, and then issues an informational message. The message describes what actions are required to move the logical log out of the virtual logs at the end of the file. After the actions are performed, `DBCC SHRINKFILE` can be used to free the remaining space.

Because a log file can only be shrunk to a virtual log file boundary, shrinking a log file to a size smaller than the size of a virtual log file might not be possible, even if it isn't being used. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] dynamically chooses the virtual file log size when log files are created or extended.

## Best practices

Consider the following information when you plan to shrink a file:

- A shrink operation is most effective after an operation that creates a large amount of unused space, such as a truncate table or a drop table operation.

- Most databases require some free space to be available for regular day-to-day operations. If you shrink a database file repeatedly and notice that the database size grows again, this indicates that the free space is required for regular operations. In these cases, repeatedly shrinking the database file is counterproductive. The file growth necessary to allocate new space after shrink can hinder performance.

- A shrink operation doesn't preserve the fragmentation state of indexes in the database, and can increase index fragmentation, which might reduce read I/O throughput for queries using large scans.

- If you need to shrink the data files of a large database, consider using the [ShrinkDriver](https://github.com/microsoft/sql-server-samples/tree/master/samples/features/shrink/shrink-driver) PowerShell script. The script automates and simplifies the shrink process, turning it into a single, observable, and resumable operation. The script shrinks multiple files in parallel, retries when interrupted, and outputs detailed status reports as it runs.

## Troubleshoot

This section describes how to diagnose and correct issues that can occur when running the `DBCC SHRINKFILE` command.

### The file doesn't shrink

If the file size doesn't change after an error-less shrink operation, try the following steps to verify that the file has sufficient free space:

- Run the following query.

  ```sql
  SELECT name,
         size / 128.0 - CAST (FILEPROPERTY(name, 'SpaceUsed') AS INT) / 128.0 AS AvailableSpaceInMB
  FROM sys.database_files;
  ```

- If you want to shrink the transaction log file, use the [sys.dm_db_log_space_usage](../../relational-databases/system-dynamic-management-objects/sys-dm-db-log-space-usage-transact-sql.md) dynamic management view (DMV) to see the space used in the transaction log.

The shrink operation can't reduce the file size any further if there's insufficient free space.

A common reason for a transaction log file not to shrink is the absence of regular transaction log backups. To truncate the log, back up the transaction log and then run the `DBCC SHRINKFILE` operation again. If point-in-time recovery isn't required, consider the [Recovery models (SQL Server)](../../relational-databases/backup-restore/recovery-models-sql-server.md) to avoid log file growth.

### The shrink operation is blocked

A transaction running under a [row versioning-based isolation level](../statements/set-transaction-isolation-level-transact-sql.md) can block shrink operations. For example, if a large delete operation running under a row versioning-based isolation level is in progress when a `DBCC SHRINKDATABASE` operation executes, the shrink operation waits for the delete to complete before continuing. When this blocking happens, `DBCC SHRINKFILE` and `DBCC SHRINKDATABASE` operations print an informational message (5202 for `SHRINKDATABASE` and 5203 for `SHRINKFILE`) to the SQL Server error log. This message is logged every five minutes in the first hour and then every hour. For example:

```output
DBCC SHRINKFILE for file ID 1 is waiting for the snapshot
transaction with timestamp 15 and other snapshot transactions linked to
timestamp 15 or with timestamps older than 109 to finish.
```

This message means snapshot transactions with timestamps older than 109 (the last transaction that the shrink operation completed) are blocking the shrink operation. It also indicates the `transaction_sequence_num`, or `first_snapshot_sequence_num` columns in the [sys.dm_tran_active_snapshot_database_transactions](../../relational-databases/system-dynamic-management-objects/sys-dm-tran-active-snapshot-database-transactions-transact-sql.md) dynamic management view contains a value of 15. If either the `transaction_sequence_num` or `first_snapshot_sequence_num` view column contains a number less than a shrink operation's last completed transaction (109), the shrink operation waits for those transactions to finish.

To resolve the issue, do one of the following steps:

- End the transaction that is blocking the shrink operation.
- End the shrink operation. Any completed work is kept if the shrink operation ends.
- Do nothing and allow the shrink operation to wait until the blocking transaction completes.

## Permissions

Requires membership in the **sysadmin** fixed server role or the **db_owner** fixed database role.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Shrink a data file to a specified target size

The following example shrinks the size of a data file named `DataFile1` in the `UserDB` user database to 7 MB.

```sql
USE UserDB;
GO

DBCC SHRINKFILE (DataFile1, 7);
GO
```

### B. Shrink a log file to a specified target size

The following example shrinks the log file in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database to 1 MB. To allow the `DBCC SHRINKFILE` command to shrink the file, the file is first truncated by setting the database recovery model to `SIMPLE`.

```sql
USE AdventureWorks2025;
GO

-- Truncate the log by changing the database recovery model to SIMPLE.
ALTER DATABASE AdventureWorks2025
    SET RECOVERY SIMPLE;
GO

-- Shrink the truncated log file to 1 MB.
DBCC SHRINKFILE (AdventureWorks2025_Log, 1);
GO

-- Reset the database recovery model.
ALTER DATABASE AdventureWorks2025
    SET RECOVERY FULL;
GO
```

### C. Truncate a data file

The following example truncates the primary data file in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database. The `sys.database_files` catalog view is queried to obtain the `file_id` of the data file.

```sql
USE AdventureWorks2025;
GO

SELECT file_id,
       name
FROM sys.database_files;
GO

DBCC SHRINKFILE (1, TRUNCATEONLY);
```

### D. Empty a file

The following example demonstrates emptying a file so it can be removed from the database. For this example's purposes, a data file is first created and contains data.

```sql
USE AdventureWorks2025;
GO

-- Create a data file and assume it contains data.
ALTER DATABASE AdventureWorks2025
    ADD FILE (NAME = Test1data, FILENAME = 'C:\t1data.ndf', SIZE = 5 MB);
GO

-- Empty the data file.
DBCC SHRINKFILE (Test1data, EMPTYFILE);
GO

-- Remove the data file from the database.
ALTER DATABASE AdventureWorks2025
     REMOVE FILE Test1data;
GO
```

### E. Shrink a database file with WAIT_AT_LOW_PRIORITY

The following example attempts to shrink the size of a data file in the current user database to 1 MB. The `sys.database_files` catalog view is queried to obtain the `file_id` of the data file, in this example, `file_id` 5. If a lock can't be obtained within one minute, the shrink operation aborts.

```sql
USE AdventureWorks2025;
GO

SELECT file_id,
       name
FROM sys.database_files;
GO

DBCC SHRINKFILE (5, 1) WITH WAIT_AT_LOW_PRIORITY (ABORT_AFTER_WAIT = SELF);
```

## Related content

- [Shrink a database](../../relational-databases/databases/shrink-a-database.md)
- [Shrink a file](../../relational-databases/databases/shrink-a-file.md)
- [DBCC SHRINKDATABASE (Transact-SQL)](dbcc-shrinkdatabase-transact-sql.md)
- [Considerations for the autogrow and autoshrink settings in SQL Server](/troubleshoot/sql/admin/considerations-autogrow-autoshrink)
- [Database files and filegroups](../../relational-databases/databases/database-files-and-filegroups.md)
- [sys.database_files (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)
- [sys.databases (Transact-SQL)](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)
- [FILE_ID (Transact-SQL)](../functions/file-id-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](../statements/alter-database-transact-sql.md)
- [Manage file space for databases in Azure SQL Database](/azure/azure-sql/database/file-space-manage)
