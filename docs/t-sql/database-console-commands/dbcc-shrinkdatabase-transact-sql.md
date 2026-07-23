---
title: DBCC SHRINKDATABASE (Transact-SQL)
description: DBCC SHRINKDATABASE shrinks the size of the data and log files in the specified database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: umajay, KevinConanMSFT, dplessMSFT, randolphwest
ms.date: 07/23/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "DBCC_SHRINKDATABASE_TSQL"
  - "DBCC SHRINKDATABASE"
  - "SHRINKDATABASE_TSQL"
  - "SHRINKDATABASE"
helpviewer_keywords:
  - "data shrinking [SQL Server]"
  - "shrinking files"
  - "shrinking databases"
  - "DBCC SHRINKDATABASE statement"
  - "decreasing database size"
  - "file shrinking [SQL Server]"
  - "database shrinking [SQL Server]"
  - "logs [SQL Server], shrinking"
  - "reducing database size"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azure-sqldw-latest || =azuresqldb-mi-current || =fabric-sqldb"
---

# DBCC SHRINKDATABASE (Transact-SQL)

[!INCLUDE [SQL Server SQL Database Azure SQL Managed Instance Azure Synapse Analytics FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricsqldb.md)]

Shrinks the size of the data and log files in the specified database.

Don't consider shrink operations a regular maintenance operation. Data and log files that grow due to regular, recurring business operations don't require shrink operations.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for SQL Server:

```syntaxsql
DBCC SHRINKDATABASE
( database_name | database_id | 0
     [ , target_percent ]
     [ , { NOTRUNCATE | TRUNCATEONLY } ]
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
    | <wait_at_low_priority_option_list>
      , <wait_at_low_priority_option>

<wait_at_low_priority_option> ::=
  ABORT_AFTER_WAIT = { SELF | BLOCKERS }
```

Syntax for Azure Synapse Analytics:

```syntaxsql
DBCC SHRINKDATABASE
( database_name
     [ , target_percent ]
)
[ WITH NO_INFOMSGS ]
```

## Arguments

#### { *database_name* | *database_id* | 0 }

The name or ID of the database to shrink. A value of 0 specifies the current database.

#### *target_percent*

The percentage of free space to leave in the database file after the shrink operation completes.

If you specify *target_percent* with `TRUNCATEONLY`, the shrink operation might not release free space at the end of the file.

#### NOTRUNCATE

Moves assigned pages from the file's end to unassigned pages in the front of the file. This action compacts the data within the file. *target_percent* is optional. [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] doesn't support this option.

The free space at the end of the file isn't returned to the operating system, and the physical size of the file doesn't change. As such, the database appears not to shrink when you specify `NOTRUNCATE`.

`NOTRUNCATE` applies only to data files. `NOTRUNCATE` doesn't affect the log file.

#### TRUNCATEONLY

Releases all free space at the end of the file to the operating system. Doesn't move any pages inside the file. The data file shrinks only to the last assigned extent. [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] doesn't support this option.

If you specify *target_percent* with `TRUNCATEONLY`, the shrink operation might not release free space at the end of the file.

#### WITH NO_INFOMSGS

Suppresses all informational messages that have severity levels from 0 through 10.

### WAIT_AT_LOW_PRIORITY with shrink operations

**Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)]

The wait at low priority feature reduces lock contention during the shrink operation. For more information, see [Understanding concurrency issues with DBCC SHRINKDATABASE](#understand-concurrency-issues-with-dbcc-shrinkdatabase).

This feature is similar to the [WAIT_AT_LOW_PRIORITY with online index operations](../statements/alter-table-transact-sql.md#wait_at_low_priority), with some differences.

- You can't specify `ABORT_AFTER_WAIT` option `NONE`.
- You can't set the `MAX_DURATION` option. The low priority lock timeout for a shrink operation is always one minute.

#### WAIT_AT_LOW_PRIORITY

When a shrink command is executed in `WAIT_AT_LOW_PRIORITY` mode, queries requiring schema stability (`Sch-S`) locks on the [Index Allocation Map (IAM) pages](../../relational-databases/pages-and-extents-architecture-guide.md#iam-pages) aren't blocked by the shrink operation. However, the shrink operation can be blocked by a `Sch-S` lock on an IAM page. Shrink continues to execute only when it's able to obtain a schema modify lock (`Sch-M`) lock on an IAM page it requires.

If a shrink operation in `WAIT_AT_LOW_PRIORITY` mode can't obtain this lock due to a long-running query holding a `Sch-S` lock, the shrink operation times out with error 49516, for example: `Msg 49516, Level 16, State 1, Line 134 Shrink timeout waiting to acquire schema modify lock in WLP mode to process IAM pageID 1:2865 on database ID 5`.

#### { ABORT_AFTER_WAIT = [ SELF | BLOCKERS ] }

- `SELF`

  `SELF` is the default option. Exit the shrink database operation currently being executed without taking any further action.

- `BLOCKERS`

  Kill all user transactions that block the shrink file operation so that the operation can continue. The `BLOCKERS` option requires the login to have the `ALTER ANY CONNECTION` or `KILL DATABASE CONNECTION` permission.

<a id="result-sets"></a>

## Result set

The following table describes the columns in the result set.

| Column name | Description |
| --- | --- |
| `DbId` | Database identification number of the file the [!INCLUDE [ssDE](../../includes/ssde-md.md)] tried to shrink. |
| `FileId` | File identification number of the file the [!INCLUDE [ssDE](../../includes/ssde-md.md)] tried to shrink. |
| `CurrentSize` | Number of 8-KB pages the file currently occupies. |
| `MinimumSize` | Number of 8-KB pages the file could occupy, at minimum. This value corresponds to the minimum size or originally created size of a file. |
| `UsedPages` | Number of 8-KB pages currently used by the file. |
| `EstimatedPages` | Number of 8-KB pages that the [!INCLUDE [ssDE](../../includes/ssde-md.md)] estimates the file could be shrunk down to. |

> [!NOTE]  
> The [!INCLUDE [ssDE](../../includes/ssde-md.md)] doesn't display rows for files that aren't shrunk.

## Remarks

To shrink all data and log files for a specific database, execute the `DBCC SHRINKDATABASE` command. To shrink one data or log file at a time for a specific database, execute the [DBCC SHRINKFILE](dbcc-shrinkfile-transact-sql.md) command.

To view the current amount of free (unallocated) space in the database, run [sp_spaceused](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md).

`DBCC SHRINKDATABASE` operations can be stopped at any point in the process, and any completed work is kept.

The database can't be smaller than the configured minimum size of the database. You specify the minimum size when the database is originally created. Or, the minimum size can be the last size explicitly set by using a file size changing operation. Operations like `DBCC SHRINKFILE` or `ALTER DATABASE` are examples of file-size changing operations.

Consider a database is originally created with a size of 10 MB in size. Then, it grows to 100 MB. The smallest the database can be reduced to is 10 MB, even if all the data in the database has been deleted.

You can specify the `NOTRUNCATE` option or the `TRUNCATEONLY` option when you run `DBCC SHRINKDATABASE`. If you don't specify either option, the result is the same as if you run a `DBCC SHRINKDATABASE` operation with `NOTRUNCATE` followed by running a `DBCC SHRINKDATABASE` operation with `TRUNCATEONLY`.

The shrunk database doesn't have to be in single user mode. Other users can be working in the database when it's shrunk, including system databases.

You can't shrink a database while the database is being backed up. Conversely, you can't back up a database while a shrink operation on the database is in process.

In Azure Synapse SQL pools, avoid running a shrink command because it's an I/O intensive operation that can take your dedicated SQL pool (formerly SQL DW) offline. This command also affects the cost of your data warehouse snapshots.

## Known issues

**Applies to**: [!INCLUDE [sql-server](../../includes/ssnoversion-md.md)], [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] dedicated SQL pool

- In [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and earlier versions, the pages used by LOB column types (**varbinary(max)**, **varchar(max)**, and **nvarchar(max)**) in compressed columnstore segments can't be moved by `DBCC SHRINKDATABASE` and `DBCC SHRINKFILE`. For more information, see [What's new in columnstore indexes](../../relational-databases/indexes/columnstore-indexes-what-s-new.md#sql-server-2025-17x).

## How DBCC SHRINKDATABASE works

`DBCC SHRINKDATABASE` shrinks data files on a per-file basis, but shrinks log files as if all the log files existed in one contiguous log pool. Files are always shrunk from the end.

Assume you have two log files and a data file in a database named `mydb`. The data and log files are 10 MB each and the data file contains 6 MB of data. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] calculates a target size for each file. This value is the target size for the file after shrinking. When you specify `DBCC SHRINKDATABASE` with *target_percent*, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] calculates target size to be the *target_percent* amount of space free in the file after shrinking.

For example, if you specify a *target_percent* of 25 for shrinking `mydb`, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] calculates the target size for the data file to be 8 MB (6 MB of data plus 2 MB of free space). As such, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] moves any data from the data file's last 2 MB to any free space in the data file's first 8 MB and then shrinks the file.

Assume the data file of `mydb` contains 7 MB of data. Specifying a *target_percent* of 30 allows for this data file to be shrunk to the free percentage of 30. However, specifying a *target_percent* of 40 doesn't shrink the data file because not enough free space can be created in the current total size of the data file.

You can think of this issue another way: 40 percent wanted free space + 70 percent full data file (7 MB out of 10 MB) is more than 100 percent. Any *target_percent* greater than 30 won't shrink the data file. It won't shrink because the percentage free you want plus the current percentage that the data file occupies is over 100 percent.

For log files, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] uses *target_percent* to calculate the target size for the whole log. That's why *target_percent* is the amount of free space in the log after the shrink operation. Target size for the whole log is then translated to a target size for each log file.

`DBCC SHRINKDATABASE` tries to shrink each physical log file to its target size immediately. If no part of the logical log stays in the virtual logs beyond the target size of the log file, `DBCC SHRINKDATABASE` successfully truncates the file and finishes without any messages. However, if part of the logical log stays in the virtual logs beyond the target size, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] frees as much space as possible, and then issues an informational message. The message describes the actions to move the logical log out of the virtual logs at the end of the file. After the actions are run, use `DBCC SHRINKDATABASE` to free the remaining space.

You can only shrink a log file to a virtual log file boundary. That's why shrinking a log file to a size smaller than the size of a virtual log file isn't possible. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] dynamically chooses the size of the virtual log file when creating or extending log files.

### Understand concurrency issues with DBCC SHRINKDATABASE

The shrink database and shrink file commands can lead to concurrency issues, especially with active maintenance such as rebuilding indexes, or in busy OLTP environments.

For example, a user query might acquire a schema stability (`Sch-S`) lock on an Index Allocation Map (IAM) page and hold it until completion. When attempting to reclaim space during regular usage, shrink database and shrink file operations require a schema modification (`Sch-M`) lock when moving or deleting IAM pages, blocking the `Sch-S` locks needed by user queries. As a result, long-running queries can block a shrink operation. This behavior also means that any new query requiring a `Sch-S` lock on an IAM page can queue behind the shrink operation, further exacerbating this concurrency issue.

Introduced in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], the wait at low priority feature for shrink operations addresses this problem by taking the schema modify lock on IAM pages in the `WAIT_AT_LOW_PRIORITY` mode. For more information, see [WAIT_AT_LOW_PRIORITY with shrink operations](#wait_at_low_priority-with-shrink-operations).

For more information on `Sch-S` and `Sch-M` locks, see [Transaction locking and row versioning guide](../../relational-databases/sql-server-transaction-locking-and-row-versioning-guide.md).

## Best practices

Consider the following information when you plan to shrink a database:

- A shrink operation is most effective after an operation that creates unused space, such as a truncate table or a drop table operation.

- Most databases require some free space for regular day-to-day operations. If you shrink a database file repeatedly and notice that the database size grows again, this growth indicates that regular operations require the free space. In these cases, repeatedly shrinking the database file is counterproductive. The file growth necessary to allocate new space after shrink can hinder performance.

- A shrink operation doesn't preserve the fragmentation state of indexes in the database, and can increase index fragmentation, which might reduce read I/O throughput for queries using large scans.

- Unless you have a specific requirement, don't set the `AUTO_SHRINK` database option to `ON`.

- If you need to shrink the data files of a large database, consider using the [ShrinkDriver](https://github.com/microsoft/sql-server-samples/tree/master/samples/features/shrink/shrink-driver) PowerShell script. The script automates and simplifies the shrink process, turning it into a single, observable, and resumable operation. The script shrinks multiple files in parallel, retries when interrupted, and outputs detailed status reports as it runs.

## Troubleshoot

A transaction running under a [row versioning-based isolation level](../statements/set-transaction-isolation-level-transact-sql.md) can block shrink operations. For example, you run `DBCC SHRINKDATABASE` while a large delete operation running under a row versioning-based isolation level is in progress. In this case, the shrink operation waits for the delete operation to complete before it shrinks the files. When the shrink operation waits, `DBCC SHRINKFILE` and `DBCC SHRINKDATABASE` operations print an informational message (5202 for `SHRINKDATABASE` and 5203 for `SHRINKFILE`). This message prints to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] error log every five minutes in the first hour and then every hour after that. For example, if the error log contains the following error message:

```output
DBCC SHRINKDATABASE for database ID 9 is waiting for the snapshot
transaction with timestamp 15 and other snapshot transactions linked to
timestamp 15 or with timestamps older than 109 to finish.
```

This error means snapshot transactions with timestamps older than 109 block the shrink operation. That transaction is the last transaction that the shrink operation completed. It also indicates the `transaction_sequence_num` or `first_snapshot_sequence_num` columns in the [sys.dm_tran_active_snapshot_database_transactions](../../relational-databases/system-dynamic-management-objects/sys-dm-tran-active-snapshot-database-transactions-transact-sql.md) dynamic management view contain a value of 15. The `transaction_sequence_num` or `first_snapshot_sequence_num` column in the view might contain a number that is less than the last transaction completed by a shrink operation (109). If so, the shrink operation waits for those transactions to finish.

To resolve the issue, you can do one of the following:

- End the transaction that is blocking the shrink operation.
- End the shrink operation. Any completed work is kept.
- Do nothing and allow the shrink operation to wait until the blocking transaction completes.

## Permissions

Requires membership in the **sysadmin** fixed server role or the **db_owner** fixed database role.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Shrink a database and specifying a percentage of free space

The following example reduces the size of the data and log files in the `UserDB` user database to allow for 10 percent free space in the database.

```sql
DBCC SHRINKDATABASE (UserDB, 10);
GO
```

### B. Truncate a database

The following example shrinks the data and log files in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] sample database to the last assigned extent.

```sql
DBCC SHRINKDATABASE (AdventureWorks2025, TRUNCATEONLY);
```

### C. Shrink an Azure Synapse Analytics database

```sql
DBCC SHRINKDATABASE (database_A);
DBCC SHRINKDATABASE (database_B, 10);
```

### D. Shrink a database with `WAIT_AT_LOW_PRIORITY`

The following example attempts to reduce the size of the data and log files in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database to allow for 20% free space in the database. If a lock can't be obtained within one minute, the shrink operation aborts.

```sql
DBCC SHRINKDATABASE ([AdventureWorks2025], 20) WITH WAIT_AT_LOW_PRIORITY (ABORT_AFTER_WAIT = SELF);
```

## Related content

- [Shrink a database](../../relational-databases/databases/shrink-a-database.md)
- [Shrink a file](../../relational-databases/databases/shrink-a-file.md)
- [DBCC SHRINKFILE (Transact-SQL)](dbcc-shrinkfile-transact-sql.md)
- [Considerations for the autogrow and autoshrink settings in SQL Server](/troubleshoot/sql/admin/considerations-autogrow-autoshrink)
- [Database files and filegroups](../../relational-databases/databases/database-files-and-filegroups.md)
- [sys.databases (Transact-SQL)](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)
- [sys.database_files (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-files-transact-sql.md)
- [ALTER DATABASE (Transact-SQL)](../statements/alter-database-transact-sql.md)
- [Manage file space for databases in Azure SQL Database](/azure/azure-sql/database/file-space-manage)
