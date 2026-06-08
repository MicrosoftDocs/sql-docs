---
title: "UPDATE STATISTICS (Transact-SQL)"
description: UPDATE STATISTICS updates query optimization statistics on a table or indexed view. Updating statistics ensures that queries compile with up-to-date statistics.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: derekw, randolphwest
ms.date: 07/07/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "UPDATE STATISTICS"
  - "UPDATE_STATISTICS_TSQL"
helpviewer_keywords:
  - "updating statistics"
  - "query optimization statistics [SQL Server], updating"
  - "UPDATE STATISTICS statement"
  - "statistical information [SQL Server], updating"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---

# UPDATE STATISTICS (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Updates query optimization [statistics](../../relational-databases/statistics/statistics.md) on a table or indexed view. By default, the query optimizer already updates statistics as necessary to improve the query plan; in some cases you can improve query performance by using `UPDATE STATISTICS` or the stored procedure [sp_updatestats](../../relational-databases/system-stored-procedures/sp-updatestats-transact-sql.md) to update statistics more frequently than the default updates.

Updating statistics ensures that queries compile with up-to-date statistics. Updating statistics via any process can cause query plans to recompile automatically. We recommend not updating statistics too frequently because there's a performance tradeoff between improving query plans and the time it takes to recompile queries. The specific tradeoffs depend on your application. `UPDATE STATISTICS` can use `tempdb` to sort the sample of rows for building statistics.

::: moniker range="=fabric"

> [!NOTE]  
> For more information on statistics in [!INCLUDE [fabric](../../includes/fabric.md)], see [Statistics in Fabric Data Warehouse](/fabric/data-warehouse/statistics).

::: moniker-end

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for SQL Server and Azure SQL Database.

```syntaxsql
UPDATE STATISTICS table_or_indexed_view_name
    [
        {
            { index_or_statistics__name }
          | ( { index_or_statistics_name } [ , ...n ] )
                }
    ]
    [ WITH
        [
            FULLSCAN
              [ [ , ] PERSIST_SAMPLE_PERCENT = { ON | OFF } ]
            | SAMPLE number { PERCENT | ROWS }
              [ [ , ] PERSIST_SAMPLE_PERCENT = { ON | OFF } ]
            | RESAMPLE
              [ ON PARTITIONS ( { <partition_number> | <range> } [ , ...n ] ) ]
            | <update_stats_stream_option> [ , ...n ]
        ]
        [ [ , ] [ ALL | COLUMNS | INDEX ]
        [ [ , ] NORECOMPUTE ]
        [ [ , ] INCREMENTAL = { ON | OFF } ]
        [ [ , ] MAXDOP = max_degree_of_parallelism ]
        [ [ , ] AUTO_DROP = { ON | OFF } ]
    ] ;

<update_stats_stream_option> ::=
    [ STATS_STREAM = stats_stream ]
    [ ROWCOUNT = numeric_constant ]
    [ PAGECOUNT = numeric_constant ]
```

Syntax for Azure Synapse Analytics and Parallel Data Warehouse.

```syntaxsql
UPDATE STATISTICS [ schema_name . ] table_name
    [ ( { statistics_name | index_name } ) ]
    [ WITH
       {
              FULLSCAN
            | SAMPLE number PERCENT
            | RESAMPLE
        }
    ]
[;]
```

Syntax for Microsoft Fabric.

```syntaxsql
UPDATE STATISTICS [ schema_name . ] table_name
    [ ( { statistics_name } ) ]
    [ WITH
       {
              FULLSCAN
            | SAMPLE number PERCENT
        }
    ]
[;]
```

> [!NOTE]
> [!INCLUDE [synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]

## Arguments

#### *table_or_indexed_view_name*

The name of the table or indexed view that contains the statistics object.

<a id="index_or_statistics_name"></a>

#### *index_or_statistics_name* or *statistics_name | index_name* or *statistics_name*

The name of the index to update statistics on or name of the statistics to update. If *index_or_statistics_name* or *statistics_name* isn't specified, the query optimizer updates all statistics for the table or indexed view. This includes statistics created using the `CREATE STATISTICS` statement, single-column statistics created when `AUTO_CREATE_STATISTICS` is on, and statistics created for indexes.

For more information about `AUTO_CREATE_STATISTICS`, see [ALTER DATABASE SET options](alter-database-transact-sql-set-options.md). To view all indexes for a table or view, you can use [sp_helpindex](../../relational-databases/system-stored-procedures/sp-helpindex-transact-sql.md).

#### FULLSCAN

Compute statistics by scanning all rows in the table or indexed view. `FULLSCAN` and `SAMPLE 100 PERCENT` have the same results. `FULLSCAN` can't be used with the `SAMPLE` option.

#### SAMPLE *number* { PERCENT | ROWS }

Specifies the approximate percentage or number of rows in the table or indexed view for the query optimizer to use when it updates statistics. For `PERCENT`, *number* can be from 0 through 100 and for `ROWS`, *number* can be from 0 to the total number of rows. The actual percentage or number of rows the query optimizer samples might not match the percentage or number specified. For example, the query optimizer scans all rows on a data page.

`SAMPLE` is useful for special cases in which the query plan, based on default sampling, isn't optimal. In most situations, it isn't necessary to specify `SAMPLE` because the query optimizer uses sampling and determines the statistically significant sample size by default, as required to create high-quality query plans.

> [!NOTE]  
> In [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] when using database compatibility level 130, sampling of data to build statistics is done in parallel to improve the performance of statistics collection. The query optimizer will use parallel sample statistics whenever a table size exceeds a certain threshold. Starting with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], regardless of database compatibility level, the behavior was changed back to using a serial scan in order to avoid potential performance issues with excessive `LATCH` waits. The rest of the query plan while updating statistics will maintain parallel execution if qualified.

`SAMPLE` can't be used with the `FULLSCAN` option. When neither `SAMPLE` nor `FULLSCAN` is specified, the query optimizer uses sampled data and computes the sample size by default.

We recommend against specifying `0 PERCENT` or `0 ROWS`. When `0 PERCENT` or `0 ROWS` is specified, the statistics object is updated but doesn't contain statistics data.

For most workloads, a full scan isn't required, and default sampling is adequate. However, certain workloads that are sensitive to widely varying data distributions might require an increased sample size, or even a full scan. While estimates might become more accurate with a full scan than a sampled scan, complex plans might not substantially benefit.

For more information, see [Components and concepts of statistics](../../relational-databases/statistics/statistics.md#DefinitionQOStatistics).

#### RESAMPLE

Update each statistic using its most recent sample rate.

Using `RESAMPLE` can result in a full-table scan. For example, statistics for indexes use a full-table scan for their sample rate. When none of the sample options (`SAMPLE`, `FULLSCAN`, `RESAMPLE`) are specified, the query optimizer samples the data and computes the sample size by default.

In [!INCLUDE [fabricdw](../../includes/fabric-dw.md)], `RESAMPLE` isn't supported.

#### PERSIST_SAMPLE_PERCENT = { ON | OFF }

**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] (starting with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] Service Pack 1 CU 4, and [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] CU 1), [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)]

When `ON`, the statistics will retain the set sampling percentage for subsequent updates that don't explicitly specify a sampling percentage. When `OFF`, statistics sampling percentage will get reset to default sampling in subsequent updates that don't explicitly specify a sampling percentage. The default is `OFF`.

[DBCC SHOW_STATISTICS](../database-console-commands/dbcc-show-statistics-transact-sql.md) and [sys.dm_db_stats_properties](../../relational-databases/system-dynamic-management-views/sys-dm-db-stats-properties-transact-sql.md) expose the persisted sample percent value for the selected statistic.

If `AUTO_UPDATE_STATISTICS` is executed, it uses the persisted sampling percentage if available, or use default sampling percentage if not. `RESAMPLE` behavior isn't affected by this option.

If the table is truncated, all statistics built on the truncated heap or B-tree (HoBT) will revert to using the default sampling percentage. Similarly, if statistics are updated on an object with no rows, it reverts to using the default sampling percentage even if `PERSIST_SAMPLE_PERCENT` was previously configured.

> [!NOTE]  
> In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], when rebuilding an index which previously had statistics updated with `PERSIST_SAMPLE_PERCENT`, the persisted sample percent is reset back to default. Starting with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] SP2 CU17, [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] CU26, and [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] CU10, the persisted sample percent is kept even when rebuilding an index.

#### ON PARTITIONS ( { \<partition_number> | \<range> } [ , ...n ] ) ]

**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions

Forces the leaf-level statistics covering the partitions specified in the `ON PARTITIONS` clause to be recomputed, and then merged to build the global statistics. `WITH RESAMPLE` is required because partition statistics built with different sample rates can't be merged together.

#### ALL | COLUMNS | INDEX

Update all existing statistics, statistics created on one or more columns, or statistics created for indexes. If none of the options are specified, the `UPDATE STATISTICS` statement updates all statistics on the table or indexed view.

#### NORECOMPUTE

Disable the automatic statistics update option, `AUTO_UPDATE_STATISTICS`, for the specified statistics. If this option is specified, the query optimizer completes this statistics update and disables future updates.

To re-enable the `AUTO_UPDATE_STATISTICS` option behavior, run `UPDATE STATISTICS` again without the `NORECOMPUTE` option or run `sp_autostats`.

> [!WARNING]  
> Using this option can produce suboptimal query plans. We recommend using this option sparingly, and then only by a qualified system administrator.

For more information about the `AUTO_STATISTICS_UPDATE` option, see [ALTER DATABASE SET options](alter-database-transact-sql-set-options.md).

#### INCREMENTAL = { ON | OFF }

**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions

When `ON`, the statistics are recreated as per partition statistics. When `OFF`, the statistics tree is dropped and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] re-computes the statistics. The default is `OFF`.

If per partition statistics aren't supported an error is generated. Incremental stats aren't supported for following statistics types:

- Statistics created with indexes that aren't partition-aligned with the base table.
- Statistics created on Always On readable secondary databases.
- Statistics created on read-only databases.
- Statistics created on filtered indexes.
- Statistics created on views.
- Statistics created on internal tables.
- Statistics created with spatial indexes or XML indexes.

#### MAXDOP = *max_degree_of_parallelism*

**Applies to**: [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] SP2 and [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] CU3).

Overrides the `max degree of parallelism` configuration option for the duration of the statistic operation. For more information, see [Server configuration: max degree of parallelism](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md). Use `MAXDOP` to limit the number of processors used in a parallel plan execution. The maximum is 64 processors.

*max_degree_of_parallelism* can be:

#### `1`

Suppresses parallel plan generation.

#### `>1`

Restricts the maximum number of processors used in a parallel statistic operation to the specified number or fewer based on the current system workload.

#### `0` (default)

Uses the actual number of processors or fewer based on the current system workload.

#### update_stats_stream_option

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

#### AUTO_DROP = { ON | OFF }

**Applies to**: [!INCLUDE [ssSQL22](../../includes/sssql22-md.md)] and later versions

Currently, if statistics are created by a third party tool on a customer database, those statistics objects can block or interfere with schema changes the customer might desire.

(Starting with [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)])| This feature allows the creation of statistics objects in a mode such that a schema change will *not* be blocked by the statistics, but instead the statistics will be dropped. In this way, auto drop statistics behave like auto created statistics.

> [!NOTE]  
> Trying to set or unset the `AUTO_DROP` property on auto created statistics might raise errors. Auto created statistics always uses auto drop. Some backups, when restored, might have this property set incorrectly until the next time the statistics object is updated (manually or automatically). However, in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], and [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, automatically created statistics always behave as though the [AUTO_DROP](../../relational-databases/statistics/statistics.md#auto_drop-option) has been set.

## Remarks

### When to UPDATE STATISTICS

For more information about when to use `UPDATE STATISTICS`, see [When to update statistics](../../relational-databases/statistics/statistics.md#UpdateStatistics).

### Limitations

- Updating statistics isn't supported on external tables. To update statistics on an external table, drop and re-create the statistics.

- Updating the statistics created automatically on a columnstore index isn't supported. Attempting this results in error 35337: `UPDATE STATISTICS failed because statistics can't be updated on a columnstore index. UPDATE STATISTICS is valid only when used with the STATS_STREAM option.` For more information, see [Index statistics](create-index-transact-sql.md#index-statistics).

  Updating statistics on individual columns, or sets of columns of a columnstore index is supported.

- The `MAXDOP` option isn't compatible with `STATS_STREAM`, `ROWCOUNT` and `PAGECOUNT` options.

- The `MAXDOP` option is limited by the Resource Governor workload group `MAX_DOP` setting, if used.

### Update all statistics with sp_updatestats

For information about how to update statistics for all user-defined and internal tables in the database, see the stored procedure [sp_updatestats](../../relational-databases/system-stored-procedures/sp-updatestats-transact-sql.md). For example, the following command calls `sp_updatestats` to update all statistics for the database.

```sql
EXECUTE sp_updatestats;
```

### Automatic index and statistics management

Use solutions such as [Adaptive Index Defrag](https://github.com/Microsoft/tigertoolbox/tree/master/AdaptiveIndexDefrag) to automatically manage index defragmentation and statistics updates for one or more databases. This procedure automatically chooses whether to rebuild or reorganize an index according to its fragmentation level, among other parameters, and update statistics with a linear threshold.

### Determine the Last Statistics Update

To determine when statistics were last updated, use the [STATS_DATE](../functions/stats-date-transact-sql.md) function.

### PDW / Azure Synapse Analytics

The following syntax isn't supported by [!INCLUDE [ssPDW](../../includes/sspdw-md.md)] / [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]:

```sql
UPDATE STATISTICS t1 (a, b);
```

```sql
UPDATE STATISTICS t1 (a) WITH SAMPLE 10 ROWS;
```

```sql
UPDATE STATISTICS t1 (a) WITH NORECOMPUTE;
```

```sql
UPDATE STATISTICS t1 (a) WITH INCREMENTAL = ON;
```

```sql
UPDATE STATISTICS t1 (a) WITH STATS_STREAM = 0x01;
```

## Permissions

Requires `ALTER` permission on the table or view.

## Examples

### A. Update all statistics on a table

The following example updates all statistics on the `SalesOrderDetail` table.

```sql
USE AdventureWorks2022;
GO

UPDATE STATISTICS Sales.SalesOrderDetail;
GO
```

### B. Update the statistics for an index

The following example updates the statistics for the `AK_SalesOrderDetail_rowguid` index of the `SalesOrderDetail` table.

```sql
USE AdventureWorks2022;
GO

UPDATE STATISTICS Sales.SalesOrderDetail (AK_SalesOrderDetail_rowguid);
GO
```

### C. Update statistics by using 50 percent sampling

The following example creates and then updates the statistics for the `Name` and `ProductNumber` columns in the `Product` table.

```sql
USE AdventureWorks2022;
GO

CREATE STATISTICS Products
    ON Production.Product([Name], ProductNumber)
    WITH SAMPLE 50 PERCENT;

-- Time passes. The UPDATE STATISTICS statement is then executed.
UPDATE STATISTICS Production.Product (Products)
    WITH SAMPLE 50 PERCENT;
```

### D. Update statistics by using FULLSCAN and NORECOMPUTE

The following example updates the `Products` statistics in the `Product` table, forces a full scan of all rows in the `Product` table, and turns off automatic statistics for the `Products` statistics.

```sql
USE AdventureWorks2022;
GO

UPDATE STATISTICS Production.Product (Products)
    WITH FULLSCAN, NORECOMPUTE;
GO
```

## Examples: Azure Synapse Analytics and Analytics Platform System (PDW)

### E. Update statistics on a table

The following example updates the `CustomerStats1` statistics on the `Customer` table.

```sql
UPDATE STATISTICS Customer (CustomerStats1);
```

### F. Update statistics by using a full scan

The following example updates the `CustomerStats1` statistics, based on scanning all of the rows in the `Customer` table.

```sql
UPDATE STATISTICS Customer (CustomerStats1) WITH FULLSCAN;
```

### G. Update all statistics on a table

The following example updates all statistics on the `Customer` table.

```sql
UPDATE STATISTICS Customer;
```

### H. Use CREATE STATISTICS with AUTO_DROP

To use auto drop statistics, just add the following to the "WITH" clause of statistics create or update.

```sql
UPDATE STATISTICS Customer (CustomerStats1) WITH AUTO_DROP = ON;
```

## Related content

- [Statistics](../../relational-databases/statistics/statistics.md)
- [Statistics in Fabric Data Warehouse](/fabric/data-warehouse/statistics)
- [ALTER DATABASE (Transact-SQL)](alter-database-transact-sql.md)
- [sys.dm_db_stats_properties (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-stats-properties-transact-sql.md)
- [sys.dm_db_stats_histogram (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-stats-histogram-transact-sql.md)
- [CREATE STATISTICS (Transact-SQL)](create-statistics-transact-sql.md)
- [DBCC SHOW_STATISTICS (Transact-SQL)](../database-console-commands/dbcc-show-statistics-transact-sql.md)
- [DROP STATISTICS (Transact-SQL)](drop-statistics-transact-sql.md)
- [sp_autostats (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-autostats-transact-sql.md)
- [sp_updatestats (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-updatestats-transact-sql.md)
- [STATS_DATE (Transact-SQL)](../functions/stats-date-transact-sql.md)
