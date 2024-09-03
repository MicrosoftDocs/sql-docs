---
title: "DBCC CHECKFILEGROUP (Transact-SQL)"
description: DBCC CHECKFILEGROUP checks the allocation and structural integrity of all tables and indexed views in the specified filegroup of the current database.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/05/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: "language-reference"
f1_keywords:
  - "CHECKFILEGROUP_TSQL"
  - "DBCC_CHECKFILEGROUP_TSQL"
  - "DBCC CHECKFILEGROUP"
  - "CHECKFILEGROUP"
helpviewer_keywords:
  - "DBCC CHECKFILEGROUP statement"
  - "database objects [SQL Server], checking"
  - "allocation checks"
  - "integrity [SQL Server], database objects"
  - "filegroups [SQL Server], consistency checks"
  - "table integrity checks [SQL Server]"
  - "checking database objects"
dev_langs:
  - "TSQL"
---
# DBCC CHECKFILEGROUP (Transact-SQL)

[!INCLUDE [SQL Server SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Checks the allocation and structural integrity of all tables and indexed views in the specified filegroup of the current database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DBCC CHECKFILEGROUP
[
    [ ( { filegroup_name | filegroup_id | 0 }
        [ , NOINDEX ]
  ) ]
    [ WITH
        {
            [ ALL_ERRORMSGS | NO_INFOMSGS ]
            [ , TABLOCK ]
            [ , ESTIMATEONLY ]
            [ , PHYSICAL_ONLY ]
            [ , MAXDOP  = number_of_processors ]
        }
    ]
]
```

## Arguments

#### *filegroup_name*

The name of the filegroup in the current database for which to check table allocation and structural integrity. If not specified, or if 0 is specified, the default is the primary filegroup. Filegroup names must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md).  
*filegroup_name* can't be a FILESTREAM filegroup.

#### *filegroup_id*

The filegroup identification (ID) number in the current database for which to check table allocation and structural integrity.

#### NOINDEX

Specifies that intensive checks of nonclustered indexes for user tables shouldn't be performed. This decreases the overall execution time. `NOINDEX` doesn't affect system tables because `DBCC CHECKFILEGROUP` always checks all system table indexes.

#### ALL_ERRORMSGS

Displays an unlimited number of errors per object. All error messages are displayed by default. Specifying or omitting this option has no effect.

#### NO_INFOMSGS

Suppresses all informational messages.

#### TABLOCK

Causes `DBCC CHECKFILEGROUP` to obtain locks instead of using an internal database snapshot.

#### ESTIMATE ONLY

Displays the estimated amount of `tempdb` space required to run `DBCC CHECKFILEGROUP` with all the other specified options.

#### PHYSICAL_ONLY

Limits the checking to the integrity of the physical structure of the page, record headers and the physical structure of B-trees. Designed to provide a small overhead check of the physical consistency of the filegroup, this check can also detect torn pages, and common hardware failures that can compromise data. A full run of `DBCC CHECKFILEGROUP` may take considerably longer than in earlier versions. This behavior occurs because of the following reasons:

- The logical checks are more comprehensive.
- Some of the underlying structures to be checked are more complex.
- Many new checks have been introduced to include the new features.

[!INCLUDE [sql-b-tree](../../includes/sql-b-tree.md)]

Therefore, using the `PHYSICAL_ONLY` option may cause a much shorter run-time for `DBCC CHECKFILEGROUP` on large filegroups and is therefore recommended for frequent use on production systems. We still recommend that a full run of `DBCC CHECKFILEGROUP` is performed periodically. The frequency of these runs depends on factors specific to individual businesses and production environments. `PHYSICAL_ONLY` always implies `NO_INFOMSGS` and isn't allowed with any one of the repair options.

> [!NOTE]  
> Specifying `PHYSICAL_ONLY` causes `DBCC CHECKFILEGROUP` to skip all checks of FILESTREAM data.

#### MAXDOP

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2014 Service Pack 2 and later versions

Overrides the **max degree of parallelism** configuration option of `sp_configure` for the statement. The `MAXDOP` can exceed the value configured with `sp_configure`. If `MAXDOP` exceeds the value configured with Resource Governor, the Database Engine uses the Resource Governor `MAXDOP` value, described in [ALTER WORKLOAD GROUP (Transact-SQL)](../statements/alter-workload-group-transact-sql.md). All semantic rules used with the max degree of parallelism configuration option are applicable when you use the `MAXDOP` query hint. For more information, see [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md).

> [!CAUTION]  
> If `MAXDOP` is set to zero then the server chooses the max degree of parallelism.

## Remarks

`DBCC CHECKFILEGROUP` and `DBCC CHECKDB` are similar DBCC commands. The main difference is that `DBCC CHECKFILEGROUP` is limited to the single specified filegroup and required tables.

`DBCC CHECKFILEGROUP` performs the following commands:

- [DBCC CHECKALLOC](../../t-sql/database-console-commands/dbcc-checkalloc-transact-sql.md) of the filegroup.
- [DBCC CHECKTABLE](../../t-sql/database-console-commands/dbcc-checktable-transact-sql.md) of every table and indexed view in the filegroup.

Running `DBCC CHECKALLOC` or `DBCC CHECKTABLE` separately from `DBCC CHECKFILEGROUP` isn't required.

## Internal database snapshot

`DBCC CHECKFILEGROUP` uses an internal database snapshot to provide the transactional consistency that it must have to perform these checks. For more information, see [View the Size of the Sparse File of a Database Snapshot (Transact-SQL)](../../relational-databases/databases/view-the-size-of-the-sparse-file-of-a-database-snapshot-transact-sql.md) and the [DBCC internal database snapshot usage](../../t-sql/database-console-commands/dbcc-transact-sql.md#dbcc-internal-database-snapshot-usage) section in [DBCC (Transact-SQL)](../../t-sql/database-console-commands/dbcc-transact-sql.md).

If a snapshot can't be created, or the `TABLOCK` option is specified, `DBCC CHECKFILEGROUP` acquires locks to obtain the required consistency. In this case, an exclusive database lock is required to perform the allocation checks, and shared table locks are required to perform the table checks. `TABLOCK` causes `DBCC CHECKFILEGROUP` to run faster on a database under heavy load, but decreases the concurrency available on the database while `DBCC CHECKFILEGROUP` is running.

> [!NOTE]  
> Running `DBCC CHECKFILEGROUP` against `tempdb` does not perform any allocation checks and must acquire shared table locks to perform table checks. This is because, for performance reasons, database snapshots are not available on `tempdb`. This means that the required transactional consistency cannot be obtained.

## Check objects in parallel

By default, `DBCC CHECKFILEGROUP` performs parallel checking of objects. The degree of parallelism is automatically determined by the query processor. The maximum degree of parallelism is configured just like parallel queries. To restrict the maximum number of processors available for DBCC checking, use [sp_configure](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md). For more information, see [Configure the max degree of parallelism Server Configuration Option](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md).

Parallel checking can be disabled by using Trace Flag 2528. For more information, see [Trace Flags (Transact-SQL)](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).

## Nonclustered indexes on separate filegroups

If a nonclustered index in the specified filegroup is associated with a table in another filegroup, the index isn't checked because the base table isn't available for validation.

If a table in the specified filegroup has a nonclustered index in another filegroup, the nonclustered index isn't checked because of the following:

- The base table structure isn't dependent on the structure of a nonclustered index. Nonclustered indexes don't have to be scanned to validate the base table.
- The `DBCC CHECKFILEGROUP` command validates objects only in the specified filegroup.

A clustered index and a table can't be on different filegroups; therefore, the previous considerations apply only to nonclustered indexes.

## Partitioned tables on separate filegroups

When a partitioned table exists on multiple filegroups, `DBCC CHECKFILEGROUP` checks the partition rowsets that exist on the specified filegroup and ignores the rowsets in the other filegroups. Informational message 2594 indicates the partitions that weren't checked. Nonclustered indexes not resident on the specified filegroup, aren't checked.

## Understand DBCC error messages

After the `DBCC CHECKFILEGROUP` command finishes, a message is written to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log. If the DBCC command successfully executes, the message indicates a successful completion, and the amount of time that the command ran. If the DBCC command stops before completing the check because of an error, the message indicates the command was terminated, a state value, and the amount of time the command ran. The following table lists and describes the state values that can be included in the message.

| State | Description |
| --- | --- |
| 0 | Error number 8930 was raised. This indicates a metadata corruption that caused the DBCC command to terminate. |
| 1 | Error number 8967 was raised. There was an internal DBCC error. |
| 2 | A failure occurred during emergency mode database repair. |
| 3 | This indicates a metadata corruption that caused the DBCC command to terminate. |
| 4 | An assert or access violation was detected. |
| 5 | An unknown error occurred that terminated the DBCC command. |

## Error reporting

A mini-dump file (`SQLDUMP<nnnn>.txt`) is created in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] `LOG` directory whenever `DBCC CHECKFILEGROUP` detects a corruption error. When the Feature Usage data collection and Error Reporting features are enabled for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the file is automatically forwarded to [!INCLUDE[msCoName](../../includes/msconame-md.md)]. The collected data is used to improve [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] functionality.

The dump file contains the results of the `DBCC CHECKFILEGROUP` command and additional diagnostic output. The file has restricted discretionary access-control lists (DACLs). Access is limited to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account and members of the **sysadmin** role. By default, the **sysadmin** role contains all members of the Windows BUILTIN\Administrators group and the local administrator's group. The DBCC command doesn't fail if the data collection process fails.

## Resolve errors

If any errors are reported by `DBCC CHECKFILEGROUP`, we recommend restoring the database from the database backup. Repair options can't be specified to `DBCC CHECKFILEGROUP`.

If no backup exists, running `DBCC CHECKDB` with a repair option specified corrects the errors reported. The repair option to use is specified at the end of the list if reported errors. Correcting the errors by using the REPAIR_ALLOW_DATA_LOSS option might require that some pages, and therefore data, be deleted.

## Result sets

`DBCC CHECKFILEGROUP` returns the following result set (values may vary):

- Except when `ESTIMATEONLY` or `NO_INFOMSGS` is specified.
- For the current database, if no database is specified, whether or not any options (except `NOINDEX`) are specified.

```output
DBCC results for 'master'.
DBCC results for 'sys.sysrowsetcolumns'.
There are 630 rows in 7 pages for object 'sys.sysrowsetcolumns'.
DBCC results for 'sys.sysrowsets'.
There are 97 rows in 1 pages for object 'sys.sysrowsets'.
DBCC results for 'sysallocunits'.
There are 195 rows in 3 pages for object 'sysallocunits'.
  
There are 2340 rows in 16 pages for object 'spt_values'.
DBCC results for 'MSreplication_options'.
There are 2 rows in 1 pages for object 'MSreplication_options'.
CHECKFILEGROUP found 0 allocation errors and 0 consistency errors in database 'master'.
DBCC execution completed. If DBCC printed error messages, contact your system administrator.
```

If `NO_INFOMSGS` is specified, `DBCC CHECKFILEGROUP` returns:

```output
DBCC execution completed. If DBCC printed error messages, contact your system administrator.
```

 If `ESTIMATEONLY` is specified, `DBCC CHECKFILEGROUP` returns (values may vary):

```output
Estimated TEMPDB space needed for CHECKALLOC (KB)
-------------------------------------------------
15
  
(1 row(s) affected)
  
Estimated TEMPDB space needed for CHECKTABLES (KB)
--------------------------------------------------
207
  
(1 row(s) affected)
  
DBCC execution completed. If DBCC printed error messages, contact your system administrator.
```

## Permissions

Requires membership in the **sysadmin** fixed server role or the **db_owner** fixed database role.

## Examples

### A. Check the PRIMARY filegroup in the database

The following example checks the primary filegroup of the current database.

```sql
DBCC CHECKFILEGROUP;
GO
```

### B. Check the AdventureWorks PRIMARY filegroup without nonclustered indexes

The following example checks the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database primary filegroup (excluding nonclustered indexes) by specifying the identification number of the primary filegroup, and by specifying `NOINDEX`.

```sql
USE AdventureWorks2022;
GO
DBCC CHECKFILEGROUP (1, NOINDEX);
GO
```

### C. Check the PRIMARY filegroup with options

The following example checks the `master` database primary filegroup and specifies the option `ESTIMATEONLY`.

```sql
USE master;
GO
DBCC CHECKFILEGROUP (1)
WITH ESTIMATEONLY;
```

## See also

- [DBCC (Transact-SQL)](../../t-sql/database-console-commands/dbcc-transact-sql.md)
- [FILEGROUP_ID (Transact-SQL)](../../t-sql/functions/filegroup-id-transact-sql.md)
- [sp_helpfile (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-helpfile-transact-sql.md)
- [sp_helpfilegroup (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-helpfilegroup-transact-sql.md)
- [sys.sysfilegroups (Transact-SQL)](../../relational-databases/system-compatibility-views/sys-sysfilegroups-transact-sql.md)
- [DBCC CHECKDB (Transact-SQL)](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md)
- [DBCC CHECKALLOC (Transact-SQL)](../../t-sql/database-console-commands/dbcc-checkalloc-transact-sql.md)
- [DBCC CHECKTABLE (Transact-SQL)](../../t-sql/database-console-commands/dbcc-checktable-transact-sql.md)
