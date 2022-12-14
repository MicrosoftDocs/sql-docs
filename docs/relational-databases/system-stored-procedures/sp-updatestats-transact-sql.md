---
title: "sp_updatestats (Transact-SQL)"
description: "sp_updatestats (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.reviewer: wiassaf
ms.date: 10/12/2022
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_updatestats_TSQL"
  - "sp_updatestats"
helpviewer_keywords:
  - "sp_updatestats"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_updatestats (Transact-SQL)

[!INCLUDE [SQL Server SQL Database](../../includes/applies-to-version/sql-asdb.md)]

Runs `UPDATE STATISTICS` against all user-defined and internal tables in the current database.

For more information about `UPDATE STATISTICS`, see [UPDATE STATISTICS (Transact-SQL)](../../t-sql/statements/update-statistics-transact-sql.md). For more information about statistics, see [Statistics](../../relational-databases/statistics/statistics.md).

 :::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_updatestats [ [ @resample = ] 'resample']
```

## Return Code Values

 0 (success) or 1 (failure)

## Arguments

#### [ @resample = ] 'resample'

 Specifies that `sp_updatestats` will use the RESAMPLE option of the [UPDATE STATISTICS](../../t-sql/statements/update-statistics-transact-sql.md) statement. If `'resample'` is not specified, `sp_updatestats` updates statistics by using the default sampling. The **resample** argument is **varchar(8)** with a default value of `NO`.

## Remarks

`sp_updatestats` executes `UPDATE STATISTICS`, by specifying the `ALL` keyword, on all user-defined and internal tables in the database. `sp_updatestats` displays messages that indicate its progress. When the update is completed, it reports that statistics have been updated for all tables.

`sp_updatestats` updates statistics on disabled nonclustered indexes and does not update statistics on disabled clustered indexes.

For disk-based tables, `sp_updatestats` updates statistics based on the **modification_counter** information in the [sys.dm_db_stats_properties](../system-dynamic-management-views/sys-dm-db-stats-properties-transact-sql.md) catalog view, updating statistics where at least one row has been modified. Statistics on memory-optimized tables are always updated when executing `sp_updatestats`. Therefore do not execute `sp_updatestats` more than necessary.

`sp_updatestats` can trigger a recompile of stored procedures or other compiled code. However, `sp_updatestats` might not cause a recompile, if only one query plan is possible for the tables referenced and the indexes on them. A recompilation would be unnecessary in these cases even if statistics are updated.

For databases with a compatibility level below 90, executing `sp_updatestats` does not preserve the latest NORECOMPUTE setting for specific statistics. For databases with a compatibility level of 90 or higher, `sp_updatestats` does preserve the latest NORECOMPUTE option for specific statistics. For more information about disabling and re-enabling statistics updates, see [Statistics](../../relational-databases/statistics/statistics.md).

When restoring a database to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] from a previous version, it is recommended to execute `sp_updatestats` on the database. This is related to setting proper metadata for the [statistics auto drop feature](../statistics/statistics.md#auto_drop-option) introduced in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)]. The auto drop feature is available in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)], and starting with [!INCLUDE[ssSQL22](../../includes/sssql22-md.md)].

## Permissions

To run `sp_updatestats`, the user must be the owner of the database (the `dbo`, not just member of the role `db_owner`) or to be member of the sysadmin fixed server role.

## Examples

The following example updates the statistics for all tables the database:

```sql
USE AdventureWorks2012;
GO
EXEC sp_updatestats;
```

## Automatic index and statistics management

Leverage solutions such as [Adaptive Index Defrag](https://github.com/Microsoft/tigertoolbox/tree/master/AdaptiveIndexDefrag) to automatically manage index defragmentation and statistics updates for one or more databases. This procedure automatically chooses whether to rebuild or reorganize an index according to its fragmentation level, amongst other parameters, and update statistics with a linear threshold.

## See also

- [System Stored Procedures](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)

## Next steps

- [ALTER DATABASE SET Options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md)
- [CREATE STATISTICS (Transact-SQL)](../../t-sql/statements/create-statistics-transact-sql.md)
- [DBCC SHOW_STATISTICS (Transact-SQL)](../../t-sql/database-console-commands/dbcc-show-statistics-transact-sql.md)
- [DROP STATISTICS (Transact-SQL)](../../t-sql/statements/drop-statistics-transact-sql.md)
- [sp_autostats (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-autostats-transact-sql.md)
- [sp_createstats (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-createstats-transact-sql.md)
- [sys.dm_db_stats_properties](../system-dynamic-management-views/sys-dm-db-stats-properties-transact-sql.md)
- [UPDATE STATISTICS (Transact-SQL)](../../t-sql/statements/update-statistics-transact-sql.md)
