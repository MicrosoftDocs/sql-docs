---
title: Statistics
description: The Query Optimizer uses statistics to create query plans that improve query performance. Learn about concepts and guidelines for using query optimization.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: katsmith
ms.date: 10/12/2022
ms.service: sql
ms.subservice: performance
ms.topic: conceptual
ms.custom: event-tier1-build-2022
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
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Statistics

[!INCLUDE [sqlserver2022-asdb-asmi](../../includes/applies-to-version/sqlserver2022-asdb-asmi.md)]

The Query Optimizer uses statistics to create query plans that improve query performance. For most queries, the Query Optimizer already generates the necessary statistics for a high-quality query plan; in some cases, you need to create additional statistics or modify the query design for best results. This article discusses statistics concepts and provides guidelines for using query optimization statistics effectively.

## <a id="DefinitionQOStatistics"></a> Components and concepts

### Statistics

Statistics for query optimization are binary large objects (BLOBs) that contain statistical information about the distribution of values in one or more columns of a table or indexed view. The Query Optimizer uses these statistics to estimate the *cardinality*, or number of rows, in the query result. These *cardinality estimates* enable the Query Optimizer to create a high-quality query plan. For example, depending on your predicates, the Query Optimizer could use cardinality estimates to choose the index seek operator instead of the more resource-intensive index scan operator, if doing so improves query performance.

Each statistics object is created on a list of one or more table columns and includes a *histogram* displaying the distribution of values in the first column. Statistics objects on multiple columns also store statistical information about the correlation of values among the columns. These correlation statistics, or *densities*, are derived from the number of distinct rows of column values.

#### <a id="histogram"></a> Histogram

A **histogram** measures the frequency of occurrence for each distinct value in a data set. The Query Optimizer computes a histogram on the column values in the first key column of the statistics object, selecting the column values by statistically sampling the rows or by performing a full scan of all rows in the table or view. If the histogram is created from a sampled set of rows, the stored totals for number of rows and number of distinct values are estimates and do not need to be whole integers.

> [!NOTE]  
> <a name="frequency"></a> Histograms in SQL Server are only built for a single column-the first column in the set of key columns of the statistics object.

To create the histogram, the Query Optimizer sorts the column values, computes the number of values that match each distinct column value, and then aggregates the column values into a maximum of 200 contiguous histogram steps. Each histogram step includes a range of column values followed by an upper bound column value. The range includes all possible column values between boundary values, excluding the boundary values themselves. The lowest of the sorted column values is the upper boundary value for the first histogram step.

In more detail,  SQL Server  creates the **histogram** from the sorted set of column values in three steps:

- **Histogram initialization**: In the first step, a sequence of values starting at the beginning of the sorted set is processed, and up to 200 values of *range_high_key*, *equal_rows*, *range_rows*, and *distinct_range_rows* are collected (*range_rows* and *distinct_range_rows* are always zero during this step). The first step ends either when all input has been exhausted, or when 200 values have been found. 
- **Scan with bucket merge**: Each additional value from the leading column of the statistics key is processed in the second step, in sorted order; each successive value is either added to the last range or a new range at the end is created (this is possible because the input values are sorted). If a new range is created, then one pair of existing, neighboring ranges is collapsed into a single range. This pair of ranges is selected in order to minimize information loss. This method uses a *maximum difference* algorithm to minimize the number of steps in the histogram while maximizing the difference between the boundary values. The number of steps after collapsing ranges stays at 200 throughout this step.
- **Histogram consolidation**: In the third step, more ranges may be collapsed if a significant amount of information is not lost. The number of histogram steps can be fewer than the number of distinct values, even for columns with fewer than 200 boundary points. Therefore, even if the column has more than 200 unique values, the histogram may have fewer than 200 steps. For a column consisting of only unique values, then the consolidated histogram will have a minimum of three steps.

> [!NOTE]  
> If the histogram has been built using a sample rather than fullscan, then the values of *equal_rows*, *range_rows*, and *distinct_range_rows* and *average_range_rows* are estimated, and therefore they do not need to be whole integers.

The following diagram shows a histogram with six steps. The area to the left of the first upper boundary value is the first step.

:::image type="content" source="../../relational-databases/system-dynamic-management-views/media/histogram-2.svg" alt-text="Image of how a histogram is calculated from sampled column values.":::

For each histogram step above:

- Bold line represents the upper boundary value (*range_high_key*) and the number of times it occurs (*equal_rows*)

- Solid area left of *range_high_key* represents the range of column values and the average number of times each column value occurs (*average_range_rows*). The *average_range_rows* for the first histogram step is always 0.

- Dotted lines represent the sampled values used to estimate total number of distinct values in the range (*distinct_range_rows*) and total number of values in the range (*range_rows*). The Query Optimizer uses *range_rows* and *distinct_range_rows* to compute *average_range_rows* and does not store the sampled values.

#### <a id="density"></a> Density vector

**Density** is information about the number of duplicates in a given column or combination of columns and it is calculated as 1/(number of distinct values). The Query Optimizer uses densities to enhance cardinality estimates for queries that return multiple columns from the same table or indexed view. As density decreases, selectivity of a value increases. For example, in a table representing cars, many cars have the same manufacturer, but each car has a unique vehicle identification number (VIN). An index on the VIN is more selective than an index on the manufacturer, because VIN has lower density than manufacturer.

> [!NOTE]  
> Frequency is information about the occurrence of each distinct value in the first key column of the statistics object, and is calculated as row count * density. A maximum frequency of 1 can be found in columns with unique values.

The density vector contains one density for each prefix of columns in the statistics object. For example, if a statistics object has the key columns `CustomerId`, `ItemId` and `Price`, density is calculated on each of the following column prefixes.

|Column prefix|Density calculated on|
|---|---|
|(`CustomerId`)|Rows with matching values for `CustomerId`|
|(`CustomerId`, `ItemId`)|Rows with matching values for `CustomerId` and `ItemId`|
|(`CustomerId`, `ItemId`, `Price`)|Rows with matching values for `CustomerId`, `ItemId`, and `Price`|

### Filtered statistics

Filtered statistics can improve query performance for queries that select from well-defined subsets of data. Filtered statistics use a filter predicate to select the subset of data that is included in the statistics. Well-designed filtered statistics can improve the query execution plan compared with full-table statistics. For more information about the filter predicate, see [CREATE STATISTICS (Transact-SQL)](../../t-sql/statements/create-statistics-transact-sql.md). For more information about when to create filtered statistics, see the [When to Create Statistics](#CreateStatistics) section in this article.

## Statistics options

There are options that affect when and how statistics are created and updated. These options are configurable at the database level only.

### <a id="AutoUpdateStats"></a> AUTO_CREATE_STATISTICS option

When the automatic create statistics option, [AUTO_CREATE_STATISTICS](../../t-sql/statements/alter-database-transact-sql-set-options.md#auto_create_statistics) is ON, the Query Optimizer creates statistics on individual columns in the query predicate, as necessary, to improve cardinality estimates for the query plan. These single-column statistics are created on columns that don't already have a [histogram](#histogram) in an existing statistics object. The AUTO_CREATE_STATISTICS option does not determine whether statistics get created for indexes. This option also does not generate filtered statistics. It applies strictly to single-column statistics for the full table.

When the Query Optimizer creates statistics as a result of using the AUTO_CREATE_STATISTICS option, the statistics name starts with `_WA`. You can use the following query to determine if the Query Optimizer has created statistics for a query predicate column.

```sql
SELECT OBJECT_NAME(s.object_id) AS object_name,
    COL_NAME(sc.object_id, sc.column_id) AS column_name,
    s.name AS statistics_name
FROM sys.stats AS s
INNER JOIN sys.stats_columns AS sc
    ON s.stats_id = sc.stats_id AND s.object_id = sc.object_id
WHERE s.name like '_WA%'
ORDER BY s.name;
```

### AUTO_UPDATE_STATISTICS option

When the automatic update statistics option, [AUTO_UPDATE_STATISTICS](../../t-sql/statements/alter-database-transact-sql-set-options.md#auto_update_statistics) is ON, the Query Optimizer determines when statistics might be out-of-date and then updates them when they are used by a query. This action is also known as statistics recompilation. Statistics become out-of-date after modifications from insert, update, delete, or merge operations change the data distribution in the table or indexed view. The Query Optimizer determines when statistics might be out-of-date by counting the number of row modifications since the last statistics update and comparing the number of row modifications to a threshold. The threshold is based on the table cardinality, which can be defined as the number of rows in the table or indexed view.

Marking statistics as out-of-date based on row modifications occurs even when the AUTO_UPDATE_STATISTICS option is OFF. When the AUTO_UPDATE STATISTICS option is OFF, statistics are not updated, even when they are marked as out-of-date. Plans will continue to use the out-of-date statistics objects. Setting AUTO_UPDATE_STATISTICS to OFF can cause suboptimal query plans and degraded query performance. Setting the AUTO_UPDATE STATISTICS option to ON is recommended.

- Up to [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] uses a recompilation threshold based on the number of rows in the table or indexed view at the time statistics were evaluated. The threshold is different whether a table is temporary or permanent.

  |Table type|Table cardinality (*n*)|Recompilation threshold (# modifications)|
  |-----------|-----------|-----------|
  |Temporary|*n* < 6|6|
  |Temporary|6 <= *n* <= 500|500|
  |Permanent|*n* <= 500|500|
  |Temporary or permanent|*n* > 500|500 + (0.20 * *n*)|

  For example if your table contains 20 thousand rows, then the calculation is `500 + (0.2 * 20,000) = 4,500` and the statistics will be updated every 4,500 modifications.

- Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and under the [database compatibility level](../../relational-databases/databases/view-or-change-the-compatibility-level-of-a-database.md) 130, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] also uses a decreasing, dynamic statistics recompilation threshold that adjusts according to the table cardinality at the time statistics were evaluated. With this change, statistics on large tables will be updated more frequently. However, if a database has a compatibility level below 130, then the [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] thresholds apply.

  |Table type|Table cardinality (*n*)|Recompilation threshold (# modifications)|
  |-----------|-----------|-----------|
  |Temporary|*n* < 6|6|
  |Temporary|6 <= *n* <= 500|500|
  |Permanent|*n* <= 500|500|
  |Temporary or permanent|*n* > 500|MIN ( 500 + (0.20 * *n*), SQRT(1,000 * *n*) ) |

  For example if your table contains 2 million rows, then the calculation is the minimum of `500 + (0.20 * 2,000,000) = 400,500` and `SQRT(1,000 * 2,000,000) = 44,721`. This means the statistics will be updated every 44,721 modifications.

> [!IMPORTANT]  
> In [!INCLUDE[ssKilimanjaro](../../includes/ssKilimanjaro-md.md)] through [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], or in [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later under [database compatibility level](../../relational-databases/databases/view-or-change-the-compatibility-level-of-a-database.md) 120 and lower, enable [trace flag 2371](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) so that  SQL Server  uses a decreasing, dynamic statistics update threshold.

While recommended for all scenarios, enabling trace flag 2371 is optional. However, you can use the following guidance for enabling the trace flag 2371 in your pre-[!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] environment:

- If you are on an SAP system, enable this trace. For more information, see this [blog on trace flag 2371](/archive/blogs/saponsqlserver/changes-to-automatic-update-statistics-in-sql-server-traceflag-2371).
- If you have to rely on nightly job to update statistics because current automatic update isn't triggered frequently enough, consider enabling trace flag 2371 to adjust the threshold to table cardinality.

The Query Optimizer checks for out-of-date statistics before compiling a query and before executing a cached query plan. Before compiling a query, the Query Optimizer uses the columns, tables, and indexed views in the query predicate to determine which statistics might be out-of-date. Before executing a cached query plan, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] verifies that the query plan references up-to-date statistics.

The AUTO_UPDATE_STATISTICS option applies to statistics objects created for indexes, single-columns in query predicates, and statistics created with the [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md) statement. This option also applies to filtered statistics.

You can use the [sys.dm_db_stats_properties](../../relational-databases/system-dynamic-management-views/sys-dm-db-stats-properties-transact-sql.md) to accurately track the number of rows changed in a table and decide if you wish to update statistics manually.

AUTO_UPDATE_STATISTICS is always OFF for memory-optimized tables.

### AUTO_UPDATE_STATISTICS_ASYNC

The asynchronous statistics update option, [AUTO_UPDATE_STATISTICS_ASYNC](../../t-sql/statements/alter-database-transact-sql-set-options.md#auto_update_statistics_async), determines whether the Query Optimizer uses synchronous or asynchronous statistics updates. By default, the asynchronous statistics update option is OFF, and the Query Optimizer updates statistics synchronously. The AUTO_UPDATE_STATISTICS_ASYNC option applies to statistics objects created for indexes, single columns in query predicates, and statistics created with the [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md) statement.

> [!NOTE]  
> To set the asynchronous statistics update option in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], in the *Options* page of the *Database Properties* window, both *Auto Update Statistics* and *Auto Update Statistics Asynchronously* options need to be set to **True**.

Statistics updates can be either synchronous (the default) or asynchronous.

- With synchronous statistics updates, queries always compile and execute with up-to-date statistics. When statistics are out-of-date, the Query Optimizer waits for updated statistics before compiling and executing the query.

- With asynchronous statistics updates, queries compile with existing statistics even if the existing statistics are out-of-date. The Query Optimizer could choose a suboptimal query plan if statistics are out-of-date when the query compiles. Statistics are typically updated soon thereafter. Queries that compile after the stats updates complete will benefit from using the updated statistics.

Consider using synchronous statistics when you perform operations that change the distribution of data, such as truncating a table or performing a bulk update of a large percentage of the rows. If you do not manually update the statistics after completing the operation, using synchronous statistics will ensure statistics are up-to-date before queries are executed on the changed data.

Consider using asynchronous statistics to achieve more predictable query response times for the following scenarios:

- Your application frequently executes the same query, similar queries, or similar cached query plans. Your query response times might be more predictable with asynchronous statistics updates than with synchronous statistics updates because the Query Optimizer can execute incoming queries without waiting for up-to-date statistics. This avoids delaying some queries and not others.

- Your application has experienced client request time outs caused by one or more queries waiting for updated statistics. In some cases, waiting for synchronous statistics could cause applications with aggressive time outs to fail.

> [!NOTE]  
> Statistics on local temporary tables are always updated synchronously regardless of AUTO_UPDATE_STATISTICS_ASYNC option. Statistics on global temporary tables are updated synchronously or asynchronously according to the AUTO_UPDATE_STATISTICS_ASYNC option set for the user database.

Asynchronous statistics update is performed by a background request. When the request is ready to write updated statistics to the database, it attempts to acquire a schema modification lock on the statistics metadata object. If a different session is already holding a lock on the same object, asynchronous statistics update is blocked until the schema modification lock can be acquired. Similarly, sessions that need to acquire a schema stability (Sch-S) lock on the statistics metadata object to compile a query may be blocked by the asynchronous statistics update background session, which is already holding or waiting to acquire the schema modification lock. Therefore, for workloads with very frequent query compilations and frequent statistics updates, using asynchronous statistics may increase the likelihood of concurrency issues due to lock blocking.

In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)], and beginning in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], you can avoid potential concurrency issues using asynchronous statistics update if you enable the ASYNC_STATS_UPDATE_WAIT_AT_LOW_PRIORITY [database-scoped configuration](../../t-sql/statements/alter-database-scoped-configuration-transact-sql.md). With this configuration enabled, the background request will wait to acquire the schema modification (Sch-M) lock and persist the updated statistics on a separate low-priority queue, allowing other requests to continue compiling queries with existing statistics. Once no other session is holding a lock on the statistics metadata object, the background request will acquire its schema modification lock and update statistics. In the unlikely event that the background request cannot acquire the lock within a timeout period of several minutes, the asynchronous statistics update will be aborted, and the statistics will not be updated until another automatic statistics update is triggered, or until statistics are [updated manually](update-statistics.md).

> [!NOTE]  
> The ASYNC_STATS_UPDATE_WAIT_AT_LOW_PRIORITY database scoped configuration option is available in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)], and in SQL Server beginning with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

### AUTO_DROP option

*Applies to**: [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], [!INCLUDE[ssazuremi_md](../../includes/ssazuremi_md.md)], and starting with [!INCLUDE[ssSQL22](../../includes/sssql22-md.md)]

In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] prior to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], if statistics are manually created by a user or third party tool on a user database, those statistics objects can block or interfere with schema changes the customer may desire.

Starting with [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], the auto drop option is enabled by default on all new and migrated databases. The AUTO_DROP property allows the creation of statistics objects in a mode such that a subsequent schema change will *not* be blocked by the statistic object, but instead the statistics will be dropped as necessary. In this way, manually created statistics with auto drop enabled behave like auto-created statistics.

> [!NOTE]  
> Trying to set or unset the auto drop property on auto-created statistics may raise errors. Auto-created statistics always uses auto drop. Some backups, when restored, may have this property set incorrectly until the next time the statistics object is updated (manually or automatically). However, auto-created statistics always behave like auto drop statistics. When restoring a database to [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] from a previous version, it is recommended to execute `sp_updatestats` on the database, setting the proper metadata for the statistics auto drop feature.

For example, to manually create a statistics object on the `dbo.DatabaseLog` table:

```sql
CREATE STATISTICS [mystats] ON [dbo].[DatabaseLog]([DatabaseLogID], [PostTime], [DatabaseUser]) WITH AUTO_DROP = ON;
```

For example, to update a statistics object auto drop setting on the `dbo.DatabaseLog` table:

```sql
UPDATE STATISTICS [dbo].[DatabaseLog] [mystats] WITH AUTO_DROP = ON;
```

To evaluate the auto drop setting on existing statistics, use the `auto_drop` column in `sys.stats`:

```sql
SELECT object_id, [name], auto_drop
FROM sys.stats;
```

For more information, see [CREATE STATISTICS (Transact-SQL)](../../t-sql/statements/create-statistics-transact-sql.md#auto_drop---on--off-)

### INCREMENTAL

**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later.

When INCREMENTAL option of CREATE STATISTICS is ON, the statistics created are per partition statistics. When OFF, the statistics tree is dropped and  SQL Server  recomputes the statistics. The default is OFF. This setting overrides the database level INCREMENTAL property. For more information about creating incremental statistics, see [CREATE STATISTICS (Transact-SQL)](../../t-sql/statements/create-statistics-transact-sql.md). For more information about creating per partition statistics automatically, see [Database Properties (Options Page)](../../relational-databases/databases/database-properties-options-page.md#automatic) and [ALTER DATABASE SET Options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md).

When new partitions are added to a large table, statistics should be updated to include the new partitions. However the time required to scan the entire table (FULLSCAN or SAMPLE option) might be quite long. Also, scanning the entire table isn't necessary because only the statistics on the new partitions might be needed. The incremental option creates and stores statistics on a per partition basis, and when updated, only refreshes statistics on those partitions that need new statistics

If per partition statistics are not supported, the option is ignored and a warning is generated. Incremental stats are not supported for following statistics types:

- Statistics created with indexes that are not partition-aligned with the base table.  
- Statistics created on Always On readable secondary databases.  
- Statistics created on read-only databases.  
- Statistics created on filtered indexes.  
- Statistics created on views.  
- Statistics created on internal tables.  
- Statistics created with spatial indexes or XML indexes.

## <a id="CreateStatistics"></a> When to create statistics

The Query Optimizer already creates statistics in the following ways:

1. The Query Optimizer creates statistics for indexes on tables or views when the index is created. These statistics are created on the key columns of the index. If the index is a filtered index, the Query Optimizer creates filtered statistics on the same subset of rows specified for the filtered index. For more information about filtered indexes, see [Create Filtered Indexes](../../relational-databases/indexes/create-filtered-indexes.md) and [CREATE INDEX (Transact-SQL)](../../t-sql/statements/create-index-transact-sql.md).

    > [!NOTE]  
    > Starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], statistics are not created by scanning all rows in the table when a partitioned index is created or rebuilt. Instead, the Query Optimizer uses the default sampling algorithm to generate statistics. After upgrading a database with partitioned indexes, you may notice a difference in the histogram data for these indexes. This change in behavior may not affect query performance. To obtain statistics on partitioned indexes by scanning all the rows in the table, use `CREATE STATISTICS` or `UPDATE STATISTICS` with the `FULLSCAN` clause.

1. The Query Optimizer creates statistics for single columns in query predicates when [AUTO_CREATE_STATISTICS](../../t-sql/statements/alter-database-transact-sql-set-options.md#auto_create_statistics) is on.

For most queries, these two methods for creating statistics ensure a high-quality query plan; in a few cases, you can improve query plans by creating additional statistics with the [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md) statement. These additional statistics can capture statistical correlations that the Query Optimizer does not account for when it creates statistics for indexes or single columns. Your application might have additional statistical correlations in the table data that, if calculated into a statistics object, could enable the Query Optimizer to improve query plans. For example, filtered statistics on a subset of data rows or multicolumn statistics on query predicate columns might improve the query plan.

When creating statistics with the CREATE STATISTICS statement, we recommend keeping the AUTO_CREATE_STATISTICS option ON so that the Query Optimizer continues to routinely create single-column statistics for query predicate columns. For more information about query predicates, see [Search Condition (Transact-SQL)](../../t-sql/queries/search-condition-transact-sql.md).

Consider creating statistics with the CREATE STATISTICS statement when any of the following applies:

- The [!INCLUDE[ssDE](../../includes/ssde-md.md)] Tuning Advisor suggests creating statistics.
- The query predicate contains multiple correlated columns that are not already in the same index.  
- The query selects from a subset of data.  
- The query has missing statistics.

> [!NOTE]  
> For information specific to In-Memory OLTP related tables and statistics, see [Statistics for Memory-Optimized Tables](../../relational-databases/in-memory-oltp/statistics-for-memory-optimized-tables.md).

### Query Predicate contains multiple correlated columns

When a query predicate contains multiple columns that have cross-column relationships and dependencies, statistics on the multiple columns might improve the query plan. Statistics on multiple columns contain cross-column correlation statistics, called *densities*, that are not available in single-column statistics. Densities can improve cardinality estimates when query results depend on data relationships among multiple columns.

If the columns are already in the same index, the multicolumn statistics object already exists and it is not necessary to create it manually. If the columns are not already in the same index, you can create multicolumn statistics by creating an index on the columns or by using the [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md) statement. It requires more system resources to maintain an index than a statistics object. If the application does not require the multicolumn index, you can economize on system resources by creating the statistics object without creating the index.

When creating multicolumn statistics, the order of the columns in the statistics object definition affects the effectiveness of densities for making cardinality estimates. The statistics object stores densities for each prefix of key columns in the statistics object definition. For more information about densities, see [Density](#density) section in this page.

To create densities that are useful for cardinality estimates, the columns in the query predicate must match one of the prefixes of columns in the statistics object definition. For example, the following example creates a multicolumn statistics object on the columns `LastName`, `MiddleName`, and `FirstName`.

```sql
USE AdventureWorks2012;
GO
IF EXISTS (SELECT name FROM sys.stats
    WHERE name = 'LastFirst'
    AND object_ID = OBJECT_ID ('Person.Person'))
DROP STATISTICS Person.Person.LastFirst;
GO
CREATE STATISTICS LastFirst ON Person.Person (LastName, MiddleName, FirstName);
GO
```

In this example, the statistics object `LastFirst` has densities for the following column prefixes: `(LastName)`, `(LastName, MiddleName)`, and `(LastName, MiddleName, FirstName)`. The density is not available for `(LastName, FirstName)`. If the query uses `LastName` and `FirstName` without using `MiddleName`, the density is not available for cardinality estimates.

### Query Selects from a subset of data

When the Query Optimizer creates statistics for single columns and indexes, it creates the statistics for the values in all rows. When queries select from a subset of rows, and that subset of rows has a unique data distribution, filtered statistics can improve query plans. You can create filtered statistics by using the [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md) statement with the [WHERE](../../t-sql/queries/where-transact-sql.md) clause to define the filter predicate expression.

For example, using [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)], each product in the `Production.Product` table belongs to one of four categories in the `Production.ProductCategory` table: Bikes, Components, Clothing, and Accessories. Each of the categories has a different data distribution for weight: bike weights range from 13.77 to 30.0, component weights range from 2.12 to 1050.00 with some NULL values, clothing weights are all NULL, and accessory weights are also NULL.

Using Bikes as an example, filtered statistics on all bike weights will provide more accurate statistics to the Query Optimizer and can improve the query plan quality compared with full-table statistics or nonexistent statistics on the Weight column. The bike weight column is a good candidate for filtered statistics but not necessarily a good candidate for a filtered index if the number of weight lookups is relatively small. The performance gain for lookups that a filtered index provides might not outweigh the additional maintenance and storage cost for adding a filtered index to the database.

The following statement creates the `BikeWeights` filtered statistics on all of the subcategories for Bikes. The filtered predicate expression defines bikes by enumerating all of the bike subcategories with the comparison `Production.ProductSubcategoryID IN (1,2,3)`. The predicate cannot use the Bikes category name because it is stored in the Production.ProductCategory table, and all columns in the filter expression must be in the same table.

[!code-sql[StatisticsDDL#FilteredStats2](../../relational-databases/statistics/codesnippet/tsql/statistics_1.sql)]

The Query Optimizer can use the `BikeWeights` filtered statistics to improve the query plan for the following query that selects all of the bikes that weigh more than `25`.

```sql
SELECT P.Weight AS Weight, S.Name AS BikeName
FROM Production.Product AS P
    JOIN Production.ProductSubcategory AS S
    ON P.ProductSubcategoryID = S.ProductSubcategoryID
WHERE P.ProductSubcategoryID IN (1,2,3) AND P.Weight > 25
ORDER BY P.Weight;
GO
```

### Query identifies missing statistics

If an error or other event prevents the Query Optimizer from creating statistics, the Query Optimizer creates the query plan without using statistics. The Query Optimizer marks the statistics as missing and attempts to regenerate the statistics the next time the query is executed.

Missing statistics are indicated as warnings (table name in red text) when the execution plan of a query is graphically displayed using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Additionally, monitoring the **Missing Column Statistics** event class by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] indicates when statistics are missing. For more information, see [Errors and Warnings Event Category &#40;Database Engine&#41;](../../relational-databases/event-classes/errors-and-warnings-event-category-database-engine.md).

 If statistics are missing, perform the following steps:

- Verify that [AUTO_CREATE_STATISTICS](../../t-sql/statements/alter-database-transact-sql-set-options.md#auto_create_statistics) and [AUTO_UPDATE_STATISTICS](../../t-sql/statements/alter-database-transact-sql-set-options.md#auto_update_statistics) are ON.  
- Verify that the database is not read-only. If the database is read-only, a new statistics object cannot be saved.  
- Create the missing statistics by using the [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md) statement.

When statistics on a read-only database or read-only snapshot are missing or stale, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] creates and maintains temporary statistics in `tempdb`. When the [!INCLUDE[ssDE](../../includes/ssde-md.md)] creates temporary statistics, the statistics name is appended with the suffix *_readonly_database_statistic* to differentiate the temporary statistics from the permanent statistics. The suffix *_readonly_database_statistic* is reserved for statistics generated by SQL Server. Scripts for the temporary statistics can be created and reproduced on a read-write database. When scripted, [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] changes the suffix of the statistics name from *_readonly_database_statistic* to *_readonly_database_statistic_scripted*.

Only  SQL Server  can create and update temporary statistics. However, you can delete temporary statistics and monitor statistics properties using the same tools that you use for permanent statistics:

- Delete temporary statistics using the [DROP STATISTICS](../../t-sql/statements/drop-statistics-transact-sql.md) statement.  
- Monitor statistics using the **[sys.stats](../../relational-databases/system-catalog-views/sys-stats-transact-sql.md)** and **[sys.stats_columns](../../relational-databases/system-catalog-views/sys-stats-columns-transact-sql.md)** catalog views. The `sys.stats` system catalog view includes the `is_temporary` column, to indicate which statistics are permanent and which are temporary.

 Because temporary statistics are stored in `tempdb`, a restart of the  SQL Server  service causes all temporary statistics to disappear.

## <a id="UpdateStatistics"></a> When to update statistics

The Query Optimizer determines when statistics might be out-of-date and then updates them when they are needed for a query plan. In some cases, you can improve the query plan and therefore improve query performance by updating statistics more frequently than occur when [AUTO_UPDATE_STATISTICS](../../t-sql/statements/alter-database-transact-sql-set-options.md#auto_update_statistics) is on. You can update statistics with the UPDATE STATISTICS statement or the stored procedure `sp_updatestats`.

Updating statistics ensures that queries compile with up-to-date statistics. Updating statistics via any process may cause query plans to recompile automatically. We recommend not manually updating statistics too frequently because there is a performance tradeoff between improving query plans and the time it takes to recompile queries. The specific tradeoffs depend on your application.

 When updating statistics with `UPDATE STATISTICS` or `sp_updatestats`, we recommend keeping AUTO_UPDATE_STATISTICS set to ON so that the Query Optimizer routinely updates statistics.

- For more information about how to update statistics on a column, an index, a table, or an indexed view, see [UPDATE STATISTICS (Transact-SQL)](../../t-sql/statements/update-statistics-transact-sql.md).
- For information about how to update statistics for all user-defined and internal tables in the database, see the stored procedure [sp_updatestats (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-updatestats-transact-sql.md).

- For more information on the thresholds for automatic statistics updates, see [AUTO_UPDATE_STATISTICS Option](#auto_update_statistics-option).

 When AUTO_UPDATE_STATISTICS is set to OFF, plan recompilation can still occur for a variety of other reasons, but will not occur automatically due to out-of-date statistics updates. When AUTO_UPDATE_STATISTICS is set to OFF, statistics updates will only occur via other manually scheduled processes, such as maintenance plans. Setting AUTO_UPDATE_STATISTICS to OFF can therefore cause suboptimal query plans and degraded query performance.

### <a id="detecting-out-of-date-statistics"></a> Detect out-of-date statistics

To determine when statistics were last updated, use the [sys.dm_db_stats_properties](../../relational-databases/system-dynamic-management-views/sys-dm-db-stats-properties-transact-sql.md) or [STATS_DATE](../../t-sql/functions/stats-date-transact-sql.md) functions.

Consider updating statistics for the following conditions:

- Query execution times are slow.  
- Insert operations occur on ascending or descending key columns.  
- After maintenance operations.

For examples updating statistics manually, see [UPDATE STATISTICS (Transact-SQL)](../../t-sql/statements/update-statistics-transact-sql.md).

### Query execution times are slow

If query response times are slow or unpredictable, ensure that queries have up-to-date statistics before performing additional troubleshooting steps.

### Insert operations occur on ascending or descending key columns

Statistics on ascending or descending key columns, such as IDENTITY or real-time timestamp columns, might require more frequent statistics updates than the Query Optimizer performs. Insert operations append new values to ascending or descending columns. The number of rows added might be too small to trigger a statistics update. If statistics are not up-to-date and queries select from the most recently added rows, the current statistics will not have cardinality estimates for these new values. This can result in inaccurate cardinality estimates and slow query performance.

For example, a query that selects from the most recent sales order dates will have inaccurate cardinality estimates if the statistics are not updated to include cardinality estimates for the most recent sales order dates.

### After maintenance operations

Consider updating statistics after performing maintenance procedures that change the distribution of data, such as truncating a table or performing a bulk insert of a large percentage of the rows. This can avoid future delays in query processing while queries wait for automatic statistics updates.

Operations such as rebuilding, defragmenting, or reorganizing an index do not change the distribution of data. Therefore, you do not need to update statistics after performing [ALTER INDEX REBUILD](../../t-sql/statements/alter-index-transact-sql.md#rebuilding-indexes), [DBCC DBREINDEX](../../t-sql/database-console-commands/dbcc-dbreindex-transact-sql.md), [DBCC INDEXDEFRAG](../../t-sql/database-console-commands/dbcc-indexdefrag-transact-sql.md), or [ALTER INDEX REORGANIZE](../../t-sql/statements/alter-index-transact-sql.md#reorganizing-indexes) operations. The Query Optimizer updates statistics when you rebuild an index on a table or view with ALTER INDEX REBUILD or DBCC DBREINDEX, however this statistics update is a byproduct of re-creating the index. The Query Optimizer does not update statistics after DBCC INDEXDEFRAG or ALTER INDEX REORGANIZE operations.

> [!TIP]  
> Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] SP1 CU4, use the PERSIST_SAMPLE_PERCENT option of [CREATE STATISTICS (Transact-SQL)](../../t-sql/statements/create-statistics-transact-sql.md) or [UPDATE STATISTICS (Transact-SQL)](../../t-sql/statements/update-statistics-transact-sql.md), to set and retain a specific sampling percentage for subsequent statistic updates that do not explicitly specify a sampling percentage.

### Automatic index and statistics management

Leverage solutions such as [Adaptive Index Defrag](https://github.com/Microsoft/tigertoolbox/tree/master/AdaptiveIndexDefrag) to automatically manage index defragmentation and statistics updates for one or more databases. This procedure automatically chooses whether to rebuild or reorganize an index according to its fragmentation level, amongst other parameters, and update statistics with a linear threshold.

## <a id="DesignStatistics"></a> Queries that use statistics effectively

Certain query implementations, such as local variables and complex expressions in the query predicate, can lead to suboptimal query plans. Following query design guidelines for using statistics effectively can help to avoid this. For more information about query predicates, see [Search Condition (Transact-SQL)](../../t-sql/queries/search-condition-transact-sql.md).

 You can improve query plans by applying query design guidelines that use statistics effectively to improve *cardinality estimates* for expressions, variables, and functions used in query predicates. When the Query Optimizer does not know the value of an expression, variable, or function, it does not know which value to look up in the histogram and therefore cannot retrieve the best cardinality estimate from the histogram. Instead, the Query Optimizer bases the cardinality estimate on the average number of rows per distinct value for all of the sampled rows in the histogram. This leads to suboptimal cardinality estimates and can hurt query performance. For more information about histograms, see [histogram](#histogram) section in this page or [sys.dm_db_stats_histogram](../../relational-databases/system-dynamic-management-views/sys-dm-db-stats-histogram-transact-sql.md).

 The following guidelines describe how to write queries to improve query plans by improving cardinality estimates.

### <a id="improving-cardinality-estimates-for-expressions"></a> Improve cardinality estimates for expressions

To improve cardinality estimates for expressions, follow these guidelines:

- Whenever possible, simplify expressions with constants in them. The Query Optimizer does not evaluate all functions and expressions containing constants prior to determining cardinality estimates. For example, simplify the expression `ABS(-100)` to `100`.  
- If the expression uses multiple variables, consider creating a computed column for the expression, and then create statistics or an index on the computed column. For example, the query predicate `WHERE PRICE + Tax > 100` might have a better cardinality estimate if you create a computed column for the expression `Price + Tax`.

### <a id="improving-cardinality-estimates-for-variables-and-functions"></a>  Improve cardinality estimates for variables and functions

To improve the cardinality estimates for variables and functions, follow these guidelines:

- If the query predicate uses a local variable, consider rewriting the query to use a parameter instead of a local variable. The value of a local variable is not known when the Query Optimizer creates the query execution plan. When a query uses a parameter, the Query Optimizer uses the cardinality estimate for the first actual parameter value that is passed to the stored procedure.  
- Consider using a standard table or temporary table to hold the results of multi-statement table-valued functions (mstvf). The Query Optimizer does not create statistics for multi-statement table-valued functions. With this approach, the Query Optimizer can create statistics on the table columns and use them to create a better query plan.  
- Consider using a standard table or temporary table as a replacement for table variables. The Query Optimizer does not create statistics for table variables. With this approach, the Query Optimizer can create statistics on the table columns and use them to create a better query plan. There are tradeoffs in determining whether to use a temporary table or a table variable; Table variables used in stored procedures cause fewer recompilations of the stored procedure than temporary tables. Depending on the application, using a temporary table instead of a table variable might not improve performance.  
- If a stored procedure contains a query that uses a passed-in parameter, avoid changing the parameter value within the stored procedure before using it in the query. The cardinality estimates for the query are based on the passed-in parameter value and not the updated value. To avoid changing the parameter value, you can rewrite the query to use two stored procedures.

    For example, the following stored procedure `Sales.GetRecentSales` changes the value of the parameter `@date` when `@date` is NULL.

    ```sql
    USE AdventureWorks2012;
    GO
    IF OBJECT_ID ( 'Sales.GetRecentSales', 'P') IS NOT NULL
        DROP PROCEDURE Sales.GetRecentSales;
    GO
    CREATE PROCEDURE Sales.GetRecentSales (@date datetime)
    AS BEGIN
        IF @date IS NULL
            SET @date = DATEADD(MONTH, -3, (SELECT MAX(ORDERDATE) FROM Sales.SalesOrderHeader))
        SELECT * FROM Sales.SalesOrderHeader h, Sales.SalesOrderDetail d
        WHERE h.SalesOrderID = d.SalesOrderID
        AND h.OrderDate > @date
    END
    GO
    ```

    If the first call to the stored procedure `Sales.GetRecentSales` passes a NULL for the `@date` parameter, the Query Optimizer will compile the stored procedure with the cardinality estimate for `@date = NULL` even though the query predicate is not called with `@date = NULL`. This cardinality estimate might be significantly different than the number of rows in the actual query result. As a result, the Query Optimizer might choose a suboptimal query plan. To help avoid this, you can rewrite the stored procedure into two procedures as follows:

    ```sql
    USE AdventureWorks2012;
    GO
    IF OBJECT_ID ( 'Sales.GetNullRecentSales', 'P') IS NOT NULL
        DROP PROCEDURE Sales.GetNullRecentSales;
    GO
    CREATE PROCEDURE Sales.GetNullRecentSales (@date datetime)
    AS BEGIN
        IF @date is NULL
            SET @date = DATEADD(MONTH, -3, (SELECT MAX(ORDERDATE) FROM Sales.SalesOrderHeader))
        EXEC Sales.GetNonNullRecentSales @date;
    END
    GO
    IF OBJECT_ID ( 'Sales.GetNonNullRecentSales', 'P') IS NOT NULL
        DROP PROCEDURE Sales.GetNonNullRecentSales;
    GO
    CREATE PROCEDURE Sales.GetNonNullRecentSales (@date datetime)
    AS BEGIN
        SELECT * FROM Sales.SalesOrderHeader h, Sales.SalesOrderDetail d
        WHERE h.SalesOrderID = d.SalesOrderID
        AND h.OrderDate > @date
    END
    GO
    ```

### <a id="improving-cardinality-estimates-with-query-hints"></a> Improve cardinality estimates with query hints

To improve cardinality estimates for local variables, you can use the `OPTIMIZE FOR <value>` or `OPTIMIZE FOR UNKNOWN` query hints with RECOMPILE. For more information, see [Query Hints (Transact-SQL)](../../t-sql/queries/hints-transact-sql-query.md).

For some applications, recompiling the query each time it executes might take too much time. The `OPTIMIZE FOR` query hint can help even if you don't use the `RECOMPILE` option. For example, you could add an `OPTIMIZE FOR` option to the stored procedure `Sales.GetRecentSales` to specify a specific date. The following example adds the `OPTIMIZE FOR` option to the `Sales.GetRecentSales` procedure.

```sql
USE AdventureWorks2012;
GO
IF OBJECT_ID ( 'Sales.GetRecentSales', 'P') IS NOT NULL
    DROP PROCEDURE Sales.GetRecentSales;
GO
CREATE PROCEDURE Sales.GetRecentSales (@date datetime)
AS BEGIN
    IF @date is NULL
        SET @date = DATEADD(MONTH, -3, (SELECT MAX(ORDERDATE) FROM Sales.SalesOrderHeader))
    SELECT * FROM Sales.SalesOrderHeader h, Sales.SalesOrderDetail d
    WHERE h.SalesOrderID = d.SalesOrderID
    AND h.OrderDate > @date
    OPTION ( OPTIMIZE FOR ( @date = '2004-05-01 00:00:00.000'))
END;
GO
```

### <a id="improving-cardinality-estimates-with-plan-guides"></a> Improve cardinality estimates with plan guides

For some applications, query design guidelines might not apply because you cannot change the query or the RECOMPILE query hint might cause too many recompiles. You can use plan guides to specify other hints, such as USE PLAN, to control the behavior of the query while investigating application changes with the application vendor. For more information about plan guides, see [Plan Guides](../../relational-databases/performance/plan-guides.md).

 In [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], consider Query Store hints to force plans, instead of plan guides. For more information, see [Query Store hints](../performance/query-store-hints.md).

## See also

- [Statistics for Memory-Optimized Tables](../../relational-databases/in-memory-oltp/statistics-for-memory-optimized-tables.md)
- [CREATE STATISTICS (Transact-SQL)](../../t-sql/statements/create-statistics-transact-sql.md)
- [UPDATE STATISTICS (Transact-SQL)](../../t-sql/statements/update-statistics-transact-sql.md)
- [sp_updatestats (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-updatestats-transact-sql.md)
- [DBCC SHOW_STATISTICS (Transact-SQL)](../../t-sql/database-console-commands/dbcc-show-statistics-transact-sql.md)
- [ALTER DATABASE SET Options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-set-options.md)
- [DROP STATISTICS (Transact-SQL)](../../t-sql/statements/drop-statistics-transact-sql.md)
- [CREATE INDEX (Transact-SQL)](../../t-sql/statements/create-index-transact-sql.md)
- [ALTER INDEX (Transact-SQL)](../../t-sql/statements/alter-index-transact-sql.md)
- [Create Filtered Indexes](../../relational-databases/indexes/create-filtered-indexes.md)
- [Controlling Autostat (AUTO_UPDATE_STATISTICS) behavior in SQL Server](https://support.microsoft.com/help/2754171)
- [STATS_DATE (Transact-SQL)](../../t-sql/functions/stats-date-transact-sql.md)
- [sys.dm_db_stats_properties (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-stats-properties-transact-sql.md)
- [sys.dm_db_stats_histogram (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-stats-histogram-transact-sql.md)
- [sys.stats](../../relational-databases/system-catalog-views/sys-stats-transact-sql.md)
- [sys.stats_columns (Transact-SQL)](../../relational-databases/system-catalog-views/sys-stats-columns-transact-sql.md)

## Next steps

- [Adaptive Index Defrag](https://github.com/Microsoft/tigertoolbox/tree/master/AdaptiveIndexDefrag) from the Microsoft SQL Server Tiger team toolbox