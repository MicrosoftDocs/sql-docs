---
title: "sp_createstats (Transact-SQL)"
description: Calls the CREATE STATISTICS Transact-SQL statement to create single-column statistics on columns that aren't already the first column in a statistics object.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_createstats_TSQL"
  - "sp_createstats"
helpviewer_keywords:
  - "sp_createstats"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_createstats (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Calls the [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md) statement to create single-column statistics on columns that aren't already the first column in a statistics object. Creating single-column statistics increases the number of histograms, which can improve cardinality estimates, query plans, and query performance. The first column of a statistics object has a histogram; other columns don't have a histogram.

`sp_createstats` is useful for applications such as benchmarking when query execution times are critical and can't wait for the query optimizer to generate single-column statistics. In most cases, it's not necessary to use `sp_createstats`; the query optimizer generates single-column statistics as necessary to improve query plans when the `AUTO_CREATE_STATISTICS` option is on.

For more information about statistics, see [Statistics](../statistics/statistics.md). For more information about generating single-column statistics, see the `AUTO_CREATE_STATISTICS` option in [ALTER DATABASE SET Options](../../t-sql/statements/alter-database-transact-sql-set-options.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_createstats
    [ [ @indexonly = ] 'indexonly' ]
    [ , [ @fullscan = ] 'fullscan' ]
    [ , [ @norecompute = ] 'norecompute' ]
    [ , [ @incremental = ] 'incremental' ]
[ ; ]
```

## Arguments

#### [ @indexonly = ] '*indexonly*'

Creates statistics only on columns that are in an existing index and aren't the first column in any index definition. *@indexonly* is **char(9)**, with a default of `NO`.

#### [ @fullscan = ] '*fullscan*'

Uses the [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md) statement with the `FULLSCAN` option. *@fullscan* is **char(9)**, with a default of `NO`.

#### [ @norecompute = ] '*norecompute*'

Uses the [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md) statement with the `NORECOMPUTE` option. *@norecompute* is **char(12)**, with a default of `NO`.

#### [ @incremental = ] '*incremental*'

Uses the [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md) statement with the `INCREMENTAL = ON` option. *@incremental* is **char(12)**, with a default of `NO`.

## Return code values

`0` (success) or `1` (failure).

## Result set

Each new statistics object has the same name as the column it's created on.

## Remarks

`sp_createstats` doesn't create or update statistics on columns that are the first column in an existing statistics object. This includes the first column of statistics created for indexes, columns with single-column statistics generated with `AUTO_CREATE_STATISTICS` option, and the first column of statistics created with the `CREATE STATISTICS` statement. `sp_createstats` doesn't create statistics on the first columns of disabled indexes unless that column is used in another enabled index. `sp_createstats` doesn't create statistics on tables with a disabled clustered index.

When the table contains a column set, `sp_createstats` doesn't create statistics on sparse columns. For more information about column sets and sparse columns, see [Use column sets](../tables/use-column-sets.md) and [Use sparse columns](../tables/use-sparse-columns.md).

## Permissions

Requires membership in the **db_owner** fixed database role.

## Examples

### A. Create single-column statistics on all eligible columns

The following example creates single-column statistics on all eligible columns in the current database.

```sql
EXEC sp_createstats;
GO
```

### B. Create single-column statistics on all eligible index columns

The following example creates single-column statistics on all eligible columns that are already in an index and aren't the first column in the index.

```sql
EXEC sp_createstats 'indexonly';
GO
```

## Related content

- [Statistics](../statistics/statistics.md)
- [CREATE STATISTICS (Transact-SQL)](../../t-sql/statements/create-statistics-transact-sql.md)
- [ALTER DATABASE SET options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md)
- [DBCC SHOW_STATISTICS (Transact-SQL)](../../t-sql/database-console-commands/dbcc-show-statistics-transact-sql.md)
- [DROP STATISTICS (Transact-SQL)](../../t-sql/statements/drop-statistics-transact-sql.md)
- [UPDATE STATISTICS (Transact-SQL)](../../t-sql/statements/update-statistics-transact-sql.md)
- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
