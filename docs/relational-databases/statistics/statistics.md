---
title: Statistics
description: The Query Optimizer uses statistics to create query plans that improve query performance. Learn about concepts and guidelines for using query optimization.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: derekw, randolphwest
ms.date: 06/12/2026
ms.service: sql
ms.subservice: performance
ms.topic: concept-article
ai-usage: ai-assisted
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "statistical information [SQL Server], query optimization"
  - "query performance [SQL Server], statistics"
  - "query optimization statistics [SQL Server]"
  - "statistical information [SQL Server], database options"
  - "query optimization statistics [SQL Server], about query optimization statistics"
  - "statistical information [SQL Server], guidelines"
  - "statistical information [SQL Server]"
  - "using statistics [SQL Server]"
  - "statistical information [SQL Server], indexes"
  - "index statistics [SQL Server]"
  - "query optimizer [SQL Server], statistics"
  - "statistics [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# Statistics

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Synapse Analytics FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricsqldb.md)]

The Query Optimizer uses statistics to create query plans that improve query performance. For most queries, the Query Optimizer already generates the necessary statistics for a high-quality query plan. In some cases, you need to create extra statistics or modify the query design for best results. This article discusses statistics concepts and provides guidelines for using query optimization statistics effectively.

<a id="DefinitionQOStatistics"></a>

## Components and concepts

### Statistics

Statistics for query optimization are binary large objects (BLOBs) that contain statistical information about the distribution of values in one or more columns of a table or indexed view. The Query Optimizer uses these statistics to estimate the *cardinality*, or number of rows, in the query result. These *cardinality estimates* enable the Query Optimizer to create a high-quality query plan. For example, depending on your predicates, the Query Optimizer could use cardinality estimates to choose the index seek operator instead of the more resource-intensive index scan operator, if doing so improves query performance.

Each statistics object is created on a list of one or more table columns and includes a *histogram* displaying the distribution of values in the first column. Statistics objects on multiple columns also store statistical information about the correlation of values among the columns. These correlation statistics, or *densities*, are derived from the number of distinct rows of column values.

<a id="histogram"></a>

#### Histogram

A **histogram** measures the frequency of occurrence for each distinct value in a data set. The Query Optimizer computes a histogram on the column values in the first key column of the statistics object, selecting the column values by statistically sampling the rows or by performing a full scan of all rows in the table or view. If the histogram is created from a sampled set of rows, the stored totals for number of rows and number of distinct values are estimates and don't need to be whole integers.

<a id="frequency"></a>

> [!NOTE]  
> SQL Server builds histograms for only a single column - the first column in the set of key columns of the statistics object.

To create the histogram, the Query Optimizer sorts the column values, computes the number of values that match each distinct column value, and then aggregates the column values into a maximum of 200 contiguous histogram steps. Each histogram step includes a range of column values followed by an upper bound column value. The range includes all possible column values between boundary values, excluding the boundary values themselves. The lowest of the sorted column values is the upper boundary value for the first histogram step.

In more detail, SQL Server creates the **histogram** from the sorted set of column values in three steps:

- **Histogram initialization**: In the first step, a sequence of values starting at the beginning of the sorted set is processed, and up to 200 values of *range_high_key*, *equal_rows*, *range_rows*, and *distinct_range_rows* are collected (*range_rows* and *distinct_range_rows* are always zero during this step). The first step ends either when all input is exhausted, or when 200 values are found.
- **Scan with bucket merge**: Each additional value from the leading column of the statistics key is processed in the second step, in sorted order. Each successive value is either added to the last range or a new range at the end is created (this ordering is possible because the input values are sorted). If a new range is created, the process collapses one pair of existing, neighboring ranges into a single range. This pair of ranges is selected in order to minimize information loss. This method uses a *maximum difference* algorithm to minimize the number of steps in the histogram while maximizing the difference between the boundary values. The number of steps after collapsing ranges stays at 200 throughout this step.
- **Histogram consolidation**: In the third step, more ranges can be collapsed if a significant amount of information isn't lost. The number of histogram steps can be fewer than the number of distinct values, even for columns with fewer than 200 boundary points. Therefore, even if the column has more than 200 unique values, the histogram might have fewer than 200 steps. For a column consisting of only unique values, the consolidated histogram has a minimum of three steps.

> [!NOTE]  
> If the histogram is built using a sample rather than fullscan, the values of *equal_rows*, *range_rows*, *distinct_range_rows*, and *average_range_rows* are estimates, and therefore they don't need to be whole integers.

The following diagram shows a histogram with six steps. The area to the left of the first upper boundary value is the first step.

:::image type="content" source="media/statistics/sample-histogram.svg" alt-text="Diagram of how a histogram is calculated from sampled column values.":::

For each histogram step in the previous example:

- The bold line represents the upper boundary value (*range_high_key*) and the number of times it occurs (*equal_rows*).

- The solid area to the left of *range_high_key* represents the range of column values and the average number of times each column value occurs (*average_range_rows*). The *average_range_rows* for the first histogram step is always 0.

- The dotted lines represent the sampled values used to estimate total number of distinct values in the range (*distinct_range_rows*) and total number of values in the range (*range_rows*). The Query Optimizer uses *range_rows* and *distinct_range_rows* to compute *average_range_rows* and doesn't store the sampled values.

<a id="density"></a>

#### Density vector

**Density** is information about the number of duplicates in a given column or combination of columns and it's calculated as 1/(number of distinct values). The Query Optimizer uses densities to enhance cardinality estimates for queries that return multiple columns from the same table or indexed view. As density decreases, selectivity of a value increases. For example, in a table representing cars, many cars have the same manufacturer, but each car has a unique vehicle identification number (VIN). An index on the VIN is more selective than an index on the manufacturer, because VIN has lower density than manufacturer.

> [!NOTE]  
> Frequency is information about the occurrence of each distinct value in the first key column of the statistics object, and is calculated as `row count * density`. A maximum frequency of 1 can be found in columns with unique values.

The density vector contains one density for each prefix of columns in the statistics object. For example, if a statistics object has the key columns `CustomerId`, `ItemId`, and `Price`, density is calculated on each of the following column prefixes.

| Column prefix | Density calculated on |
| --- | --- |
| (`CustomerId`) | Rows with matching values for `CustomerId` |
| (`CustomerId`, `ItemId`) | Rows with matching values for `CustomerId` and `ItemId` |
| (`CustomerId`, `ItemId`, `Price`) | Rows with matching values for `CustomerId`, `ItemId`, and `Price` |

### Filtered statistics

Filtered statistics can improve query performance for queries that select from well-defined subsets of data. Filtered statistics use a filter predicate to select the subset of data that is included in the statistics. Well-designed filtered statistics can improve the query execution plan compared with full-table statistics. For more information about the filter predicate, see [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md). For more information about when to create filtered statistics, see the [When to Create Statistics](#CreateStatistics) section in this article.

## Statistics options

You can configure options that affect when and how the system creates and updates statistics. You can set these options only at the database level.

<a id="AutoUpdateStats"></a>

### AUTO_CREATE_STATISTICS option

When you turn on the automatic create statistics option, [AUTO_CREATE_STATISTICS](../../t-sql/statements/alter-database-transact-sql-set-options.md#auto_create_statistics), the Query Optimizer creates statistics on individual columns in the query predicate, as necessary, to improve cardinality estimates for the query plan. These single-column statistics are created on columns that don't already have a [histogram](#histogram) in an existing statistics object. The `AUTO_CREATE_STATISTICS` option doesn't determine whether the database creates statistics for indexes. This option also doesn't generate filtered statistics. It applies strictly to single-column statistics for the full table.

When the Query Optimizer creates statistics as a result of using the `AUTO_CREATE_STATISTICS` option, the statistics name starts with `_WA`. You can use the following query to determine if the Query Optimizer created statistics for a query predicate column.

```sql
SELECT OBJECT_NAME(s.object_id) AS object_name,
    COL_NAME(sc.object_id, sc.column_id) AS column_name,
    s.name AS statistics_name
FROM sys.stats AS s
    INNER JOIN sys.stats_columns AS sc
        ON s.stats_id = sc.stats_id
        AND s.object_id = sc.object_id
WHERE s.name LIKE '_WA%'
ORDER BY s.name;
```

### AUTO_UPDATE_STATISTICS option

When you turn on the automatic update statistics option, [AUTO_UPDATE_STATISTICS](../../t-sql/statements/alter-database-transact-sql-set-options.md#auto_update_statistics), the Query Optimizer determines when statistics might be out-of-date and updates them when a query uses them. This action is also known as statistics recompilation. Statistics become out-of-date after modifications from insert, update, delete, or merge operations change the data distribution in the table or indexed view. The Query Optimizer counts the number of row modifications since the last statistics update and compares that number to a threshold to determine if statistics might be out-of-date. The threshold is based on the table cardinality, which is the number of rows in the table or indexed view.

Marking statistics as out-of-date based on row modifications happens even when the `AUTO_UPDATE_STATISTICS` option is `OFF`. When the `AUTO_UPDATE_STATISTICS` option is `OFF`, the system doesn't update statistics, even when it marks them as out-of-date. Plans continue to use the out-of-date statistics objects. Setting `AUTO_UPDATE_STATISTICS` to `OFF` can cause suboptimal query plans and degraded query performance. Set the `AUTO_UPDATE STATISTICS` option to `ON`.

- Up to [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)], the [!INCLUDE [ssDE-md](../../includes/ssde-md.md)] uses a recompilation threshold based on the number of rows in the table or indexed view at the time statistics were evaluated. The threshold is different depending on whether a table is temporary or permanent.

  | Table type | Table cardinality (*n*) | Recompilation threshold (# modifications) |
  | --- | --- | --- |
  | Temporary | *n* < 6 | 6 |
  | Temporary | 6 <= *n* <= 500 | 500 |
  | Permanent | *n* <= 500 | 500 |
  | Temporary or permanent | *n* > 500 | 500 + (0.20 * *n*) |

  For example, if your table contains 20,000 rows, the calculation is `500 + (0.2 * 20,000) = 4,500` and the statistics are updated every 4,500 modifications.

- Starting with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and with the [database compatibility level](../databases/view-or-change-the-compatibility-level-of-a-database.md) 130, the [!INCLUDE [ssDE-md](../../includes/ssde-md.md)] uses a decreasing, dynamic statistics recompilation threshold that adjusts according to the table cardinality at the time statistics were evaluated. With this change, statistics on large tables are updated more frequently. However, if a database has a compatibility level below 130, the [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] thresholds apply.

  | Table type | Table cardinality (*n*) | Recompilation threshold (# modifications) |
  | --- | --- | --- |
  | Temporary | `n < 6` | 6 |
  | Temporary | `6 <= n <= 500` | 500 |
  | Permanent | `n <= 500` | 500 |
  | Temporary or permanent | `n > 500` | `MIN ( 500 + (0.20 * n), SQRT(1,000 * n) )` |

  For example, if your table contains 2 million rows, the calculation is the minimum of `500 + (0.20 * 2,000,000) = 400,500` and `SQRT(1,000 * 2,000,000) = 44,721`. This means the statistics are updated every 44,721 modifications.

> [!IMPORTANT]  
> In [!INCLUDE [sql2008r2](../../includes/sql2008r2-md.md)] through [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)], or in [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions with [database compatibility level](../databases/view-or-change-the-compatibility-level-of-a-database.md) 120 and lower versions, enable [trace flag 2371](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) so that SQL Server uses a decreasing, dynamic statistics update threshold.

While recommended for all scenarios, enabling trace flag 2371 is optional. However, you can use the following guidance for enabling the trace flag 2371 in your pre-[!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] environment:

- If you're on an SAP system, enable this trace. For more information, see this [blog on trace flag 2371](/archive/blogs/saponsqlserver/changes-to-automatic-update-statistics-in-sql-server-traceflag-2371).
- If you have to rely on a nightly job to update statistics because the current automatic update isn't triggered frequently enough, consider enabling trace flag 2371 to adjust the threshold to table cardinality.

The Query Optimizer checks for out-of-date statistics before compiling a query and before executing a cached query plan. Before it compiles a query, the Query Optimizer uses the columns, tables, and indexed views in the query predicate to determine which statistics might be out-of-date. Before it executes a cached query plan, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] verifies that the query plan references up-to-date statistics.

The `AUTO_UPDATE_STATISTICS` option applies to statistics objects created for indexes, single-columns in query predicates, and statistics created with the [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md) statement. This option also applies to filtered statistics.

You can use the [sys.dm_db_stats_properties](../system-dynamic-management-views/sys-dm-db-stats-properties-transact-sql.md) to accurately track the number of rows changed in a table and decide if you want to update statistics manually.

`AUTO_UPDATE_STATISTICS` is always `OFF` for memory-optimized tables.

### AUTO_UPDATE_STATISTICS_ASYNC

The asynchronous statistics update option, [AUTO_UPDATE_STATISTICS_ASYNC](../../t-sql/statements/alter-database-transact-sql-set-options.md#auto_update_statistics_async), determines whether the Query Optimizer uses synchronous or asynchronous statistics updates. By default, the asynchronous statistics update option is `OFF`, and the Query Optimizer updates statistics synchronously. The `AUTO_UPDATE_STATISTICS_ASYNC` option applies to statistics objects created for indexes, single columns in query predicates, and statistics created with the [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md) statement.



> [!NOTE]  
> To set the asynchronous statistics update option in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], in the *Options* page of the *Database Properties* window, set both *Auto Update Statistics* and *Auto Update Statistics Asynchronously* to **True**.



Statistics updates can be either synchronous (the default) or asynchronous.

- With synchronous statistics updates, queries always compile and execute with up-to-date statistics. When statistics are out-of-date, the Query Optimizer waits for updated statistics before compiling and executing the query.

- With asynchronous statistics updates, queries compile with existing statistics even if the existing statistics are out-of-date. The Query Optimizer could choose a suboptimal query plan if statistics are out-of-date when the query compiles. Statistics are typically updated soon thereafter. Queries that compile after the stats updates complete benefit from using the updated statistics.

Consider using synchronous statistics when you perform operations that change the distribution of data, such as truncating a table or performing a bulk update of a large percentage of the rows. If you don't manually update the statistics after completing the operation, using synchronous statistics ensures statistics are up-to-date before queries need them.

Consider using asynchronous statistics to achieve more predictable query response times for the following scenarios:

- Your application frequently executes the same query, similar queries, or similar cached query plans. Your query response times might be more predictable with asynchronous statistics updates than with synchronous statistics updates because the Query Optimizer can execute incoming queries without waiting for up-to-date statistics. This avoids delaying some queries and not others.

- Your application has experienced client request timeouts caused by one or more queries waiting for updated statistics. In some cases, waiting for synchronous statistics could cause applications with aggressive timeouts to fail.

> [!NOTE]  
> Statistics on local temporary tables are always updated synchronously regardless of the `AUTO_UPDATE_STATISTICS_ASYNC` option. Statistics on global temporary tables are updated synchronously or asynchronously according to the `AUTO_UPDATE_STATISTICS_ASYNC` option set for the user database.

Asynchronous statistics update is performed by a background request. When the request is ready to write updated statistics to the database, it attempts to acquire a schema modification lock on the statistics metadata object. If a different session is already holding a lock on the same object, asynchronous statistics update is blocked until the schema modification lock can be acquired. Similarly, sessions that need to acquire a schema stability (Sch-S) lock on the statistics metadata object to compile a query can be blocked by the asynchronous statistics update background session, which is already holding or waiting to acquire the schema modification lock. Therefore, for workloads with very frequent query compilations and frequent statistics updates, using asynchronous statistics can increase the likelihood of concurrency issues due to lock blocking.

In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)], and beginning in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], you can avoid potential concurrency issues using asynchronous statistics update if you enable the `ASYNC_STATS_UPDATE_WAIT_AT_LOW_PRIORITY` [database-scoped configuration](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md). With this configuration enabled, the background request waits to acquire the schema modification (Sch-M) lock and persist the updated statistics on a separate low-priority queue, allowing other requests to continue compiling queries with existing statistics. Once no other session is holding a lock on the statistics metadata object, the background request acquires its schema modification lock and update statistics. In the unlikely event that the background request can't acquire the lock within a timeout period of several minutes, the asynchronous statistics update is aborted, and the statistics aren't updated until another automatic statistics update is triggered, or until statistics are [updated manually](update-statistics.md).

> [!NOTE]  
> The `ASYNC_STATS_UPDATE_WAIT_AT_LOW_PRIORITY` database scoped configuration option is available in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)], and in SQL Server beginning with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

### AUTO_DROP option

**Applies to**: [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], and starting with [!INCLUDE [ssSQL22](../../includes/sssql22-md.md)]

In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] prior to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], if you manually create statistics or use a third-party tool on a user database, those statistics objects can block or interfere with schema changes.

Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], the auto drop option is enabled by default on all new and migrated databases. If you enable the `AUTO_DROP` property, the database creates statistics objects in a mode such that a subsequent schema change *isn't* blocked by the statistic object, but instead the statistics are dropped as necessary. In this way, manually created statistics with auto drop enabled behave like auto-created statistics.

In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], and [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, automatically created statistics always behave as though the [AUTO_DROP](../../relational-databases/statistics/statistics.md#auto_drop-option) is enabled.

> [!NOTE]  
> Trying to set or unset the auto drop property on auto-created statistics can raise errors. Auto-created statistics always uses auto drop. Some backups, when restored, can have this property set incorrectly until the next time the statistics object is updated (manually or automatically). However, auto-created statistics always behave like auto drop statistics. When restoring a database to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] from a previous version, it's recommended to execute `sp_updatestats` on the database, setting the proper metadata for the statistics auto drop feature.

For example, to manually create a statistics object on the `dbo.DatabaseLog` table:

```sql
CREATE STATISTICS [mystats]
    ON [dbo].[DatabaseLog]([DatabaseLogID], [PostTime], [DatabaseUser])
    WITH AUTO_DROP = ON;
```

For example, to update a statistics object auto drop setting on the `dbo.DatabaseLog` table:

```sql
UPDATE STATISTICS [dbo].[DatabaseLog] ([mystats])
    WITH AUTO_DROP = ON;
```

To evaluate the auto drop setting on existing statistics, use the `auto_drop` column in `sys.stats`:

```sql
SELECT object_id,
       [name],
       auto_drop
FROM sys.stats;
```

For more information, see [AUTO_DROP](../../t-sql/statements/create-statistics-transact-sql.md#auto_drop---on--off-).

### INCREMENTAL

**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions.

When you set the `INCREMENTAL` option of `CREATE STATISTICS` to `ON`, you create per partition statistics. When you set it to `OFF`, the database drops the statistics tree and recomputes the statistics. The default is `OFF`. This setting overrides the database level `INCREMENTAL` property. 

- For more information about creating incremental statistics, see [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md). 
- For more information about creating per partition statistics automatically, see [Database Properties (Options Page)](../../relational-databases/databases/database-properties-options-page.md#automatic) and [ALTER DATABASE SET options](../../t-sql/statements/alter-database-transact-sql-set-options.md).

When you add new partitions to a large table, you should update statistics to include the new partitions. However, the time required to scan the entire table (`FULLSCAN` or `SAMPLE` options) can be long. Also, scanning the entire table isn't necessary because only the statistics on the new partitions might be needed. The incremental option creates and stores statistics on a per partition basis, and when updated, only refreshes statistics on those partitions that need new statistics.

If per partition statistics aren't supported, the database ignores the option and generates a warning. Incremental stats aren't supported for the following statistics types:

- Statistics created with indexes that aren't partition-aligned with the base table.
- Statistics created on Always On readable secondary databases.
- Statistics created on read-only databases.
- Statistics created on filtered indexes.
- Statistics created on views.
- Statistics created on internal tables.
- Statistics created with spatial indexes or XML indexes.

<a id="CreateStatistics"></a>

## When to create statistics

The Query Optimizer already creates statistics in the following ways:

1. When you create an index on tables or views, the Query Optimizer creates statistics for indexes. These statistics are created on the key columns of the index. If the index is a filtered index, the Query Optimizer creates filtered statistics on the same subset of rows specified for the filtered index. For more information about filtered indexes, see [Create filtered indexes](../indexes/create-filtered-indexes.md) and [CREATE INDEX](../../t-sql/statements/create-index-transact-sql.md).

   > [!NOTE]  
   > In [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions, the database doesn't create statistics by scanning all rows in the table when you create or rebuild a partitioned index. Instead, the Query Optimizer uses the default sampling algorithm to generate statistics. After upgrading a database with partitioned indexes, you might notice a difference in the histogram data for these indexes. This change in behavior might not affect query performance. To get statistics on partitioned indexes by scanning all the rows in the table, use `CREATE STATISTICS` or `UPDATE STATISTICS` with the `FULLSCAN` clause.

1. The Query Optimizer creates statistics for single columns in query predicates when [AUTO_CREATE_STATISTICS](../../t-sql/statements/alter-database-transact-sql-set-options.md#auto_create_statistics) is on.

For most queries, these two methods for creating statistics ensure a high-quality query plan. In a few cases, you can improve query plans by creating additional statistics by using the [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md) statement. These additional statistics can capture statistical correlations that the Query Optimizer doesn't account for when it creates statistics for indexes or single columns. Your application might have additional statistical correlations in the table data that, if calculated into a statistics object, could enable the Query Optimizer to improve query plans. For example, filtered statistics on a subset of data rows or multicolumn statistics on query predicate columns might improve the query plan.

When you create statistics by using the CREATE STATISTICS statement, keep the `AUTO_CREATE_STATISTICS` option `ON` so that the Query Optimizer continues to routinely create single-column statistics for query predicate columns. For more information about query predicates, see [Search condition](../../t-sql/queries/search-condition-transact-sql.md).

Consider creating statistics by using the CREATE STATISTICS statement when any of the following conditions apply:

- The [!INCLUDE [ssDE](../../includes/ssde-md.md)] Tuning Advisor suggests creating statistics.
- The query predicate contains multiple correlated columns that aren't already keys in the same index.
- The query selects from a subset of data.
- The query has missing statistics.

> [!NOTE]  
> For information specific to In-Memory OLTP related tables and statistics, see [Statistics for Memory-Optimized Tables](../in-memory-oltp/statistics-for-memory-optimized-tables.md).

### Query predicate contains multiple correlated columns

When a query predicate contains multiple columns that have cross-column relationships and dependencies, statistics on the multiple columns might improve the query plan. Statistics on multiple columns contain cross-column correlation statistics, called *densities*, that aren't available in single-column statistics. Densities can improve cardinality estimates when query results depend on data relationships among multiple columns.

If the columns are already in the same index, the multicolumn statistics object already exists and you don't need to create it manually. If the columns aren't already in the same index, you can create multicolumn statistics by creating an index on the columns or by using the [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md) statement. It requires more system resources to maintain an index than a statistics object. If the application doesn't require the multicolumn index, you can economize on system resources by creating the statistics object without creating the index.

When you create multicolumn statistics, the order of the columns in the statistics object definition affects the effectiveness of densities for making cardinality estimates. The statistics object stores densities for each prefix of key columns in the statistics object definition. For more information about densities, see the [Density](#density) section in this article.

To create densities that are useful for cardinality estimates, the columns in the query predicate must match one of the prefixes of columns in the statistics object definition. For example, the following example creates a multicolumn statistics object on the columns `LastName`, `MiddleName`, and `FirstName`.

```sql
USE AdventureWorks2022;
GO

IF EXISTS (SELECT name
           FROM sys.stats
           WHERE name = 'LastFirst'
                 AND object_ID = OBJECT_ID('Person.Person'))
    DROP STATISTICS Person.Person.LastFirst;
GO

CREATE STATISTICS LastFirst
    ON Person.Person(LastName, MiddleName, FirstName);
GO
```

In this example, the statistics object `LastFirst` has densities for the following column prefixes: `(LastName)`, `(LastName, MiddleName)`, and `(LastName, MiddleName, FirstName)`. The density isn't available for `(LastName, FirstName)`. If the query uses `LastName` and `FirstName` without using `MiddleName`, the density isn't available for cardinality estimates.

### Query selects from a subset of data

When the Query Optimizer creates statistics for single columns and indexes, it creates the statistics for the values in all rows. When queries select from a subset of rows, and that subset of rows has a unique data distribution, filtered statistics can improve query plans. You can create filtered statistics by using the [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md) statement with the [WHERE](../../t-sql/queries/where-transact-sql.md) clause to define the filter predicate expression.

For example, using [!INCLUDE [ssSampleDBnormal](../../includes/sssampledbnormal-md.md)], each product in the `Production.Product` table belongs to one of four categories in the `Production.ProductCategory` table: `Bikes`, `Components`, `Clothing`, and `Accessories`. Each of the categories has a different data distribution for weight: bike weights range from 13.77 to 30.0, component weights range from 2.12 to 1050.00 with some `NULL` values, clothing weights are all `NULL`, and accessory weights are also `NULL`.

Using `Bikes` as an example, filtered statistics on all bike weights provide more accurate statistics to the Query Optimizer and can improve the query plan quality compared with full-table statistics or nonexistent statistics on the Weight column. The bike weight column is a good candidate for filtered statistics but not necessarily a good candidate for a filtered index if the number of weight lookups is relatively small. The performance gain for lookups that a filtered index provides might not outweigh the additional maintenance and storage cost for adding a filtered index to the database.

The following statement creates the `BikeWeights` filtered statistics on all of the subcategories for `Bikes`. The filtered predicate expression defines bikes by enumerating all of the bike subcategories with the comparison `Production.ProductSubcategoryID IN (1,2,3)`. The predicate can't use the `Bikes` category name because it's stored in the `Production.ProductCategory` table, and all columns in the filter expression must be in the same table.

:::code language="sql" source="codesnippet/tsql/statistics_1.sql":::

The Query Optimizer can use the `BikeWeights` filtered statistics to improve the query plan for the following query that selects all of the bikes that weigh more than `25`.

```sql
SELECT P.Weight AS Weight,
       S.Name AS BikeName
FROM Production.Product AS P
     INNER JOIN Production.ProductSubcategory AS S
         ON P.ProductSubcategoryID = S.ProductSubcategoryID
WHERE P.ProductSubcategoryID IN (1, 2, 3)
      AND P.Weight > 25
ORDER BY P.Weight;
GO
```

### Query identifies missing statistics

If an error or other event prevents the Query Optimizer from creating statistics, the Query Optimizer creates the query plan without using statistics. The Query Optimizer marks the statistics as missing and attempts to regenerate the statistics the next time the query is executed.

Missing statistics are indicated as warnings (table name in red text) when the execution plan of a query is graphically displayed using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Additionally, monitoring the **Missing Column Statistics** event class by using [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] indicates when statistics are missing. For more information, see [Errors and Warnings Event Category (Database Engine)](../event-classes/errors-and-warnings-event-category-database-engine.md).

If statistics are missing, perform the following steps:

- Verify that [AUTO_CREATE_STATISTICS](../../t-sql/statements/alter-database-transact-sql-set-options.md#auto_create_statistics) and [AUTO_UPDATE_STATISTICS](../../t-sql/statements/alter-database-transact-sql-set-options.md#auto_update_statistics) are ON.
- Verify that the database isn't read-only. If the database is read-only, a new statistics object can't be saved.
- Create the missing statistics by using the [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md) statement.

#### Temporary statistics

When statistics on a read-only database or read-only snapshot are missing or stale, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] creates and maintains temporary statistics in `tempdb`. When the [!INCLUDE [ssDE](../../includes/ssde-md.md)] creates temporary statistics, the statistics name is appended with the suffix *_readonly_database_statistic* to differentiate the temporary statistics from the permanent statistics. The suffix *_readonly_database_statistic* is reserved for statistics generated by the [!INCLUDE [ssDE](../../includes/ssde-md.md)]. Scripts for the temporary statistics can be created and executed on a read-write database. When scripted, [!INCLUDE [ssManStudio](../../includes/ssmanstudio-md.md)] changes the suffix of the statistics name from *_readonly_database_statistic* to *_readonly_database_statistic_scripted*.

Only the [!INCLUDE [ssDE](../../includes/ssde-md.md)] can create and update temporary statistics. However, you can delete temporary statistics and monitor statistics properties using the same tools that you use for permanent statistics:

- Delete temporary statistics using the [DROP STATISTICS](../../t-sql/statements/drop-statistics-transact-sql.md) statement.
- Monitor statistics using the [sys.stats](../system-catalog-views/sys-stats-transact-sql.md) and [sys.stats_columns](../system-catalog-views/sys-stats-columns-transact-sql.md) catalog views. The `sys.stats` system catalog view includes the `is_temporary` column, to indicate which statistics are permanent and which are temporary.

Because temporary statistics are stored in `tempdb`, a restart of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] removes all temporary statistics.

Just like for all statistics, creating and updating temporary statistics requires a schema modification (`Sch-M`) lock on the object. This lock might block other queries and processes, including the system redo process on secondary replicas that applies transactions from the primary replica. If this blocking affects query workloads or data propagation, you can disable the automatic creation and update of temporary statistics using the `READABLE_SECONDARY_TEMPORARY_STATS_AUTO_CREATE` and `READABLE_SECONDARY_TEMPORARY_STATS_AUTO_UPDATE` [database-scoped configurations](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md) respectively.

<a id="UpdateStatistics"></a>

## When to update statistics

The Query Optimizer determines when statistics might be out-of-date and then updates them when needed for a query plan. In some cases, you can improve the query plan and therefore improve query performance by updating statistics more frequently than when [AUTO_UPDATE_STATISTICS](../../t-sql/statements/alter-database-transact-sql-set-options.md#auto_update_statistics) is `ON`. You can update statistics by using the `UPDATE STATISTICS` statement or the stored procedure `sp_updatestats`.

Updating statistics ensures that queries compile with up-to-date statistics. Updating statistics through any process can cause query plans to recompile automatically. Don't manually update statistics too frequently because there's a performance tradeoff between improving query plans and the time it takes to recompile queries. The specific tradeoffs depend on your application.

When you update statistics by using `UPDATE STATISTICS` or `sp_updatestats`, keep `AUTO_UPDATE_STATISTICS` set to `ON` so that the Query Optimizer routinely updates statistics.

- For more information about how to update statistics on a column, an index, a table, or an indexed view, see [UPDATE STATISTICS](../../t-sql/statements/update-statistics-transact-sql.md).
- For information about how to update statistics for all user-defined and internal tables in the database, see the stored procedure [sp_updatestats](../system-stored-procedures/sp-updatestats-transact-sql.md).

- For more information on the thresholds for automatic statistics updates, see [AUTO_UPDATE_STATISTICS Option](#auto_update_statistics-option).

When you set `AUTO_UPDATE_STATISTICS` to `OFF`, plan recompilation can still occur for various other reasons, but it doesn't occur automatically due to out-of-date statistics updates. When you set `AUTO_UPDATE_STATISTICS` to `OFF`, statistics updates only occur through other manually scheduled processes, such as maintenance plans. Setting `AUTO_UPDATE_STATISTICS` to `OFF` can therefore cause suboptimal query plans and degraded query performance.

<a id="detecting-out-of-date-statistics"></a>

### Detect out-of-date statistics

To determine when statistics were last updated, use the [sys.dm_db_stats_properties](../system-dynamic-management-views/sys-dm-db-stats-properties-transact-sql.md) or [STATS_DATE](../../t-sql/functions/stats-date-transact-sql.md) functions.

Consider updating statistics for the following conditions:

- Query execution times are slow.
- Insert operations occur on ascending or descending key columns.
- After maintenance operations.

For examples of updating statistics manually, see [UPDATE STATISTICS](../../t-sql/statements/update-statistics-transact-sql.md).

### Query execution times are slow

If query response times are slow or unpredictable, ensure that queries have up-to-date statistics before performing additional troubleshooting steps.

### Insert operations occur on ascending or descending key columns

Statistics on ascending or descending key columns, such as IDENTITY or real-time timestamp columns, might require more frequent statistics updates than the Query Optimizer performs. Insert operations append new values to ascending or descending columns. The number of rows added might be too small to trigger a statistics update. If statistics aren't up-to-date and queries select from the most recently added rows, the current statistics don't have cardinality estimates for these new values. This condition can result in inaccurate cardinality estimates and slow query performance.

For example, a query that selects from the most recent sales order dates has inaccurate cardinality estimates if the statistics aren't updated to include cardinality estimates for the most recent sales order dates.

### After maintenance operations

Consider updating statistics after performing maintenance procedures that change the distribution of data, such as truncating a table or performing a bulk insert of a large percentage of the rows. Proactively updating statistics can avoid future delays in query processing while queries wait for automatic statistics updates.

Operations such as rebuilding, defragmenting, or reorganizing an index don't change the distribution of data. Therefore, you don't need to update statistics after performing [ALTER INDEX REBUILD](../../t-sql/statements/alter-index-transact-sql.md#rebuilding-indexes), [DBCC DBREINDEX](../../t-sql/database-console-commands/dbcc-dbreindex-transact-sql.md), [DBCC INDEXDEFRAG](../../t-sql/database-console-commands/dbcc-indexdefrag-transact-sql.md), or [ALTER INDEX REORGANIZE](../../t-sql/statements/alter-index-transact-sql.md#reorganizing-indexes) operations. The Query Optimizer updates statistics when you rebuild an index on a table or view with `ALTER INDEX REBUILD` or `DBCC DBREINDEX`, however this statistics update is a byproduct of re-creating the index. The Query Optimizer doesn't update statistics after `DBCC INDEXDEFRAG` or `ALTER INDEX REORGANIZE` operations.

> [!TIP]  
> Starting with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] SP1 CU4, use the PERSIST_SAMPLE_PERCENT option of [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md) or [UPDATE STATISTICS](../../t-sql/statements/update-statistics-transact-sql.md) to set and retain a specific sampling percentage for subsequent statistic updates that don't explicitly specify a sampling percentage.

### Automatic index and statistics management

Use smart solutions such as [Adaptive Index Defrag](https://github.com/Microsoft/tigertoolbox/tree/master/AdaptiveIndexDefrag) to automatically manage index defragmentation and statistics updates for one or more databases. This procedure automatically chooses whether to rebuild or reorganize an index according to its fragmentation level, among other parameters, and updates statistics with a linear threshold.

<a id="DetermineStatisticsUsed"></a>

## Determine which statistics the Query Optimizer used

You can find the statistics objects the Query Optimizer uses when it compiles a query by inspecting an estimated or actual [execution plan](../performance/execution-plans.md). When you inspect an execution plan, the `OptimizerStatsUage` element contains `StatisticsInfo` elements which contain information on the statistics objects the Query Optimizer loaded during compilation. The `StatisticsInfo` elements contain the statistics object name, the database, schema, and table it belongs to, the modification count at compile time, the sampling percent, and when it was last updated.

Use any of the following techniques to inspect the execution plan:

- In SQL Server Management Studio, select **Include Actual Execution Plan** (Ctrl+M) before running the query. In the **Execution plan** tab that appears with the results, you can: 
    - Right-click inside the graphical plan and select **Show Execution Plan XML**. Look for the `OptimizerStatsUsage` element and each child `StatisticsInfo` element.
    - Select the final (left-most) operator. (In the case of a `SELECT` query, this operator is a `SELECT` node.) In the **Properties** window, expand the **OptimizerStatsUsage** node, and view information about the statistics objects used in the query.
- Run [SET STATISTICS XML ON](../../t-sql/statements/set-statistics-xml-transact-sql.md) before the query. Select the hyperlink that appears with the results to view the execution plan XML.
- Query [sys.dm_exec_query_plan](../system-dynamic-management-views/sys-dm-exec-query-plan-transact-sql.md) or [sys.dm_exec_query_statistics_xml](../system-dynamic-management-views/sys-dm-exec-query-statistics-xml-transact-sql.md) for recent queries.
- Read a previously captured plan from [Query Store](../performance/monitoring-performance-by-using-the-query-store.md) using [sys.query_store_plan](../system-catalog-views/sys-query-store-plan-transact-sql.md).

Each `StatisticsInfo` element looks like the following XML fragment from a query on the `AdventureWorks2022` sample database:

```xml
<StatisticsInfo 
    Database="[AdventureWorks2022]" 
    Schema="[Sales]" 
    Table="[SalesOrderDetail]" 
    Statistics="[IX_SalesOrderDetail_ProductID]" 
    ModificationCount="0" 
    SamplingPercent="100" 
    LastUpdate="2025-09-07T15:32:16.89" />
```

| Attribute | Meaning |
| --- | --- |
| `Database`, `Schema`, `Table` | The object the statistic belongs to. |
| `Statistics` | Name of the statistics object in the database. Use this name with [DBCC SHOW_STATISTICS](../../t-sql/database-console-commands/dbcc-show-statistics-transact-sql.md) or [sys.stats](../system-catalog-views/sys-stats-transact-sql.md) to inspect the histogram and density vector. |
| `ModificationCount` | Number of data modifications since the statistic was last updated, at the time the plan compiled. A large value relative to table size indicates that the statistic was stale during compilation. |
| `SamplingPercent` | Percentage of rows sampled to build the statistic. Lower values can produce less accurate histograms for skewed data. |
| `LastUpdate` | Timestamp of the last statistics update. If the [AUTO_UPDATE_STATISTICS](../../t-sql/statements/alter-database-transact-sql-set-options.md#auto_update_statistics--on--off--1) option is enabled on the database, the database automatically updates statistics when needed.|

> [!NOTE]
> `StatisticsInfo` reflects statistics that were available and considered during plan compilation. If a `StatisticsInfo` entry is missing for a column your query filters on, the query optimizer didn't identify relevant statistics, which is a potential source of poor performance.

To check current freshness and modification counts for a statistic object, use [sys.dm_db_stats_properties](../system-dynamic-management-views/sys-dm-db-stats-properties-transact-sql.md). For example, the following query provides the current metrics for a statistic object named `IX_SalesOrderDetail_ProductID` on the table `Sales.SalesOrderDetail`:

```sql
SELECT
    OBJECT_SCHEMA_NAME(s.object_id) AS schema_name,
    OBJECT_NAME(s.object_id)        AS table_name,
    s.name                          AS statistics_name,
    sp.last_updated,
    sp.rows,
    sp.rows_sampled,
    sp.modification_counter
FROM sys.stats AS s
OUTER APPLY sys.dm_db_stats_properties(s.object_id, s.stats_id) AS sp
WHERE s.object_id = OBJECT_ID(N'Sales.SalesOrderDetail')
  AND s.name = N'IX_SalesOrderDetail_ProductID';
```

To determine whether the current database's [automatic statistics creation and update options](../../t-sql/statements/alter-database-transact-sql.md#set-auto_create_statistics--on--off-) are enabled, use: 

```sql
SELECT [name],
    is_auto_create_stats_on,
    is_auto_update_stats_on,
    is_auto_update_stats_async_on
FROM sys.databases
WHERE [name] = DB_NAME();
```

<a id="DesignStatistics"></a>

## Queries that use statistics effectively

Certain query implementations, such as local variables and complex expressions in the query predicate, can lead to suboptimal query plans. To avoid these problems, follow query design guidelines for using statistics effectively. For more information about query predicates, see [Search condition](../../t-sql/queries/search-condition-transact-sql.md).

You can improve query plans by applying query design guidelines that use statistics effectively to improve *cardinality estimates* for expressions, variables, and functions used in query predicates. When the Query Optimizer doesn't know the value of an expression, variable, or function, it doesn't know which value to look up in the histogram and therefore can't retrieve the best cardinality estimate from the histogram. Instead, the Query Optimizer bases the cardinality estimate on the average number of rows per distinct value for all of the sampled rows in the histogram. This situation leads to suboptimal cardinality estimates and can hurt query performance. For more information about histograms, see the [histogram](#histogram) section in this article or [sys.dm_db_stats_histogram](../system-dynamic-management-views/sys-dm-db-stats-histogram-transact-sql.md).

The following guidelines describe how to write queries to improve query plans by improving cardinality estimates.

<a id="improving-cardinality-estimates-for-expressions"></a>

### Improve cardinality estimates for expressions

To improve cardinality estimates for expressions, follow these guidelines:

- Whenever possible, simplify expressions that contain constants. The Query Optimizer doesn't evaluate all functions and expressions that contain constants before it determines cardinality estimates. For example, simplify the expression `ABS(-100)` to `100`.
- If the expression uses multiple variables, consider creating a computed column for the expression, and then create statistics or an index on the computed column. For example, the query predicate `WHERE PRICE + Tax > 100` might have a better cardinality estimate if you create a computed column for the expression `Price + Tax`.

<a id="improving-cardinality-estimates-for-variables-and-functions"></a>

### Improve cardinality estimates for variables and functions

To improve cardinality estimates for variables and functions, follow these guidelines:

- If the query predicate uses a local variable, consider rewriting the query to use a parameter instead of a local variable. The Query Optimizer doesn't know the value of a local variable when it creates the query execution plan. When a query uses a parameter, the Query Optimizer uses the cardinality estimate for the first actual parameter value that the stored procedure receives.

- Consider using a standard table or temporary table to hold the results of multistatement table-valued functions. The Query Optimizer doesn't create statistics for multistatement table-valued functions. By using this approach, the Query Optimizer can create statistics on the table columns and use them to create a better query plan.

- Consider using a standard table or temporary table as a replacement for table variables. The Query Optimizer doesn't create statistics for table variables. By using this approach, the Query Optimizer can create statistics on the table columns and use them to create a better query plan. There are tradeoffs in determining whether to use a temporary table or a table variable. Table variables used in stored procedures cause fewer recompilations of the stored procedure than temporary tables. Depending on the application, using a temporary table instead of a table variable might not improve performance.

- If a stored procedure contains a query that uses a passed-in parameter, avoid changing the parameter value within the stored procedure before using it in the query. The cardinality estimates for the query are based on the passed-in parameter value and not the updated value. To avoid changing the parameter value, you can rewrite the query to use two stored procedures.

  For example, the following stored procedure `Sales.GetRecentSales` changes the value of the parameter `@date` when `@date` is `NULL`.

  ```sql
  USE AdventureWorks2022;
  GO

  IF OBJECT_ID('Sales.GetRecentSales', 'P') IS NOT NULL
      DROP PROCEDURE Sales.GetRecentSales;
  GO

  CREATE PROCEDURE Sales.GetRecentSales
  @date DATETIME
  AS
  BEGIN
      IF @date IS NULL
          SET @date = DATEADD(MONTH, -3,
              (SELECT MAX(ORDERDATE)
              FROM Sales.SalesOrderHeader));
      SELECT *
      FROM Sales.SalesOrderHeader AS h, Sales.SalesOrderDetail AS d
      WHERE h.SalesOrderID = d.SalesOrderID
          AND h.OrderDate > @date;
  END
  GO
  ```

  If the first call to the stored procedure `Sales.GetRecentSales` passes a `NULL` for the `@date` parameter, the Query Optimizer compiles the stored procedure with the cardinality estimate for `@date = NULL` even though the query predicate isn't called with `@date = NULL`. This cardinality estimate might be significantly different from the number of rows in the actual query result. As a result, the Query Optimizer might choose a suboptimal query plan. To help avoid this problem, you can rewrite the stored procedure into two procedures as follows:

  ```sql
  USE AdventureWorks2022;
  GO

  IF OBJECT_ID('Sales.GetNullRecentSales', 'P') IS NOT NULL
      DROP PROCEDURE Sales.GetNullRecentSales;
  GO

  CREATE PROCEDURE Sales.GetNullRecentSales
  @date DATETIME
  AS
  BEGIN
      IF @date IS NULL
          SET @date = DATEADD(MONTH, -3,
              (SELECT MAX(ORDERDATE)
              FROM Sales.SalesOrderHeader));
      EXECUTE Sales.GetNonNullRecentSales @date;
  END
  GO

  IF OBJECT_ID('Sales.GetNonNullRecentSales', 'P') IS NOT NULL
      DROP PROCEDURE Sales.GetNonNullRecentSales;
  GO

  CREATE PROCEDURE Sales.GetNonNullRecentSales
  @date DATETIME
  AS
  BEGIN
      SELECT *
      FROM Sales.SalesOrderHeader AS h, Sales.SalesOrderDetail AS d
      WHERE h.SalesOrderID = d.SalesOrderID
          AND h.OrderDate > @date;
  END
  GO
  ```

<a id="improving-cardinality-estimates-with-query-hints"></a>

### Improve cardinality estimates with query hints

To improve cardinality estimates for local variables, use the `OPTIMIZE FOR <value>` or `OPTIMIZE FOR UNKNOWN` query hints with `RECOMPILE`. For more information, see [Query hints](../../t-sql/queries/hints-transact-sql-query.md).

For some applications, recompiling the query each time it executes might take too much time. The `OPTIMIZE FOR` query hint can help even if you don't use the `RECOMPILE` option. For example, you could add an `OPTIMIZE FOR` option to the stored procedure `Sales.GetRecentSales` to specify a specific date. The following example adds the `OPTIMIZE FOR` option to the `Sales.GetRecentSales` procedure.

```sql
USE AdventureWorks2022;
GO

IF OBJECT_ID('Sales.GetRecentSales', 'P') IS NOT NULL
    DROP PROCEDURE Sales.GetRecentSales;
GO

CREATE PROCEDURE Sales.GetRecentSales
@date DATETIME
AS
BEGIN
    IF @date IS NULL
        SET @date = DATEADD(MONTH, -3,
            (SELECT MAX(ORDERDATE)
            FROM Sales.SalesOrderHeader));
    SELECT *
    FROM Sales.SalesOrderHeader AS h, Sales.SalesOrderDetail AS d
    WHERE h.SalesOrderID = d.SalesOrderID AND h.OrderDate > @date
    OPTION (OPTIMIZE FOR (@date = '2004-05-01 00:00:00.000'));
END
GO
```

<a id="improving-cardinality-estimates-with-plan-guides"></a>

### Improve cardinality estimates with plan guides

For some applications, query design guidelines might not apply because you can't change the query or the `RECOMPILE` query hint might cause too many recompiles. Use plan guides to specify other hints, such as `USE PLAN`, to control the behavior of the query while investigating application changes with the application vendor. For more information about plan guides, see [Plan Guides](../performance/plan-guides.md).

In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], consider Query Store hints to force plans, instead of plan guides. For more information, see [Query Store hints](../performance/query-store-hints.md).

## Related content

- [Statistics for Memory-Optimized Tables](../in-memory-oltp/statistics-for-memory-optimized-tables.md)
- [CREATE STATISTICS (Transact-SQL)](../../t-sql/statements/create-statistics-transact-sql.md)
- [UPDATE STATISTICS (Transact-SQL)](../../t-sql/statements/update-statistics-transact-sql.md)
- [sys.sp_updatestats (Transact-SQL)](../system-stored-procedures/sp-updatestats-transact-sql.md)
- [DBCC SHOW_STATISTICS (Transact-SQL)](../../t-sql/database-console-commands/dbcc-show-statistics-transact-sql.md)
- [ALTER DATABASE SET options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md)
- [DROP STATISTICS (Transact-SQL)](../../t-sql/statements/drop-statistics-transact-sql.md)
- [CREATE INDEX (Transact-SQL)](../../t-sql/statements/create-index-transact-sql.md)
- [ALTER INDEX (Transact-SQL)](../../t-sql/statements/alter-index-transact-sql.md)
- [Create filtered indexes](../indexes/create-filtered-indexes.md)
- [STATS_DATE (Transact-SQL)](../../t-sql/functions/stats-date-transact-sql.md)
- [sys.dm_db_stats_properties (Transact-SQL)](../system-dynamic-management-views/sys-dm-db-stats-properties-transact-sql.md)
- [sys.dm_db_stats_histogram (Transact-SQL)](../system-dynamic-management-views/sys-dm-db-stats-histogram-transact-sql.md)
- [sys.stats (Transact-SQL)](../system-catalog-views/sys-stats-transact-sql.md)
- [sys.stats_columns (Transact-SQL)](../system-catalog-views/sys-stats-columns-transact-sql.md)
- [Adaptive Index Defrag](https://github.com/Microsoft/tigertoolbox/tree/master/AdaptiveIndexDefrag)
