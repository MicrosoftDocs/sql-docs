---
title: "sys.sp_updatestats (Transact-SQL)"
description: "Runs UPDATE STATISTICS against all user-defined and internal tables in the current database."
author: markingmyname
ms.author: maghan
ms.reviewer: wiassaf, randolphwest
ms.date: 08/06/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "sp_updatestats_TSQL"
  - "sp_updatestats"
helpviewer_keywords:
  - "sp_updatestats"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.sp_updatestats (Transact-SQL)

[!INCLUDE [SQL Server SQL Database-fabricsqldb](../../includes/applies-to-version/sql-asdb-fabricsqldb.md)]

Runs `UPDATE STATISTICS` against all user-defined and internal tables in the current database.

For more information about `UPDATE STATISTICS`, see [UPDATE STATISTICS](../../t-sql/statements/update-statistics-transact-sql.md). For more information about statistics, see [Statistics](../statistics/statistics.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_updatestats [ [ @resample = ] 'resample' ]
[ ; ]
```

## Return code values

`0` (success) or `1` (failure).

## Arguments

#### [ @resample = ] 'resample'

Specifies that `sp_updatestats` uses the `RESAMPLE` option of the [UPDATE STATISTICS](../../t-sql/statements/update-statistics-transact-sql.md) statement. If `resample` isn't specified, `sp_updatestats` updates statistics by using the default sampling. The `resample` argument is **varchar(8)** with a default value of `NO`.

## Remarks

`sp_updatestats` executes `UPDATE STATISTICS`, by specifying the `ALL` keyword, on all user-defined and internal tables in the database. `sp_updatestats` displays messages that indicate its progress. When the update is completed, it reports that statistics are updated for all tables.

`sp_updatestats` updates statistics on disabled nonclustered indexes and doesn't update statistics on disabled clustered indexes.

For disk-based tables, `sp_updatestats` updates statistics based on the `modification_counter` information in the [sys.dm_db_stats_properties](../system-dynamic-management-views/sys-dm-db-stats-properties-transact-sql.md) catalog view, updating statistics where at least one row is modified.

For memory-optimized tables, `sp_updatestats` updates all statistics unconditionally. However, if [Memory-optimized TempDB metadata](../databases/tempdb-database.md#memory-optimized-tempdb-metadata) is enabled, `sp_updatestats` fails with error 41317 because a single transaction isn't allowed to access memory-optimized tables in more than one database. For more information, see [Limitations of Memory-optimized TempDB metadata](../databases/tempdb-database.md#limitations-of-memory-optimized-tempdb-metadata).

`sp_updatestats` can trigger a recompile of stored procedures or other compiled code. However, `sp_updatestats` might not cause a recompile, if only one query plan is possible for the tables referenced and the indexes on them. A recompilation would be unnecessary in these cases even if statistics are updated.

`sp_updatestats` preserves the latest `NORECOMPUTE` option for specific statistics. For more information about disabling and re-enabling statistics updates, see [Statistics](../statistics/statistics.md).

When restoring a database to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] from a previous version, you should execute `sp_updatestats` on the database. This is related to setting proper metadata for the [statistics auto drop feature](../statistics/statistics.md#auto_drop-option) introduced in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], and [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, automatically created statistics always behave as though the [AUTO_DROP](../../relational-databases/statistics/statistics.md#auto_drop-option) has been set.

## Permissions

For [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], you must be the owner of the database (**dbo**), or a member of the **sysadmin** fixed server role.

For [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], you must be a member of the **db_owner** fixed database role.

## Examples

The following example updates the statistics for all tables the database:

```sql
USE AdventureWorks2022;
GO

EXECUTE sp_updatestats;
```

## Automatic index and statistics management

Use solutions such as [Adaptive Index Defrag](https://github.com/Microsoft/tigertoolbox/tree/master/AdaptiveIndexDefrag) to automatically manage index defragmentation and statistics updates for one or more databases. This procedure automatically chooses whether to rebuild or reorganize an index according to its fragmentation level, among other parameters, and update statistics with a linear threshold.

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [ALTER DATABASE SET options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md)
- [CREATE STATISTICS (Transact-SQL)](../../t-sql/statements/create-statistics-transact-sql.md)
- [DBCC SHOW_STATISTICS (Transact-SQL)](../../t-sql/database-console-commands/dbcc-show-statistics-transact-sql.md)
- [DROP STATISTICS (Transact-SQL)](../../t-sql/statements/drop-statistics-transact-sql.md)
- [sys.sp_autostats (Transact-SQL)](sp-autostats-transact-sql.md)
- [sys.sp_createstats (Transact-SQL)](sp-createstats-transact-sql.md)
- [sys.dm_db_stats_properties](../system-dynamic-management-views/sys-dm-db-stats-properties-transact-sql.md)
- [UPDATE STATISTICS (Transact-SQL)](../../t-sql/statements/update-statistics-transact-sql.md)
