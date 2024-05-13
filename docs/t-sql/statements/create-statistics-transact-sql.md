---
title: CREATE STATISTICS (Transact-SQL)
description: Creates query optimization statistics on one or more columns of a table, an indexed view, or an external table.
author: markingmyname
ms.author: maghan
ms.reviewer: derekw, wiassaf, randolphwest
ms.date: 05/21/2024
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "STATISTICS"
  - "STATISTICS_TSQL"
  - "CREATE STATISTICS"
  - "CREATE_STATISTICS_TSQL"
helpviewer_keywords:
  - "query optimization statistics [SQL Server], creating"
  - "indexed views [SQL Server], statistics"
  - "FULLSCAN option"
  - "CREATE STATISTICS statement"
  - "filtered statistics [SQL Server]"
  - "creating statistics [SQL Server]"
  - "NORECOMPUTE clause"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---

# CREATE STATISTICS (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asmi-asa-fabricse-fabricdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricse-fabricdw.md)]

Creates query optimization statistics on one or more columns of a table, an indexed view, or an external table. For most queries, the query optimizer already generates the necessary statistics for a high-quality query plan; in a few cases, you need to create extra statistics with `CREATE STATISTICS` or modify the query design to improve query performance.

To learn more, see [Statistics](../../relational-databases/statistics/statistics.md).

::: moniker range="=fabric"

> [!NOTE]  
> For more information on statistics in [!INCLUDE [fabric](../../includes/fabric.md)], see [Statistics in Fabric data warehousing](/fabric/data-warehouse/statistics).

::: moniker-end

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for SQL Server, Azure SQL Database, and Azure SQL Managed Instance.

```syntaxsql
-- Create statistics on an external table
CREATE STATISTICS statistics_name
ON { table_or_indexed_view_name } ( column [ , ...n ] )
    [ WITH FULLSCAN ] ;

-- Create statistics on a regular table or indexed view
CREATE STATISTICS statistics_name
ON { table_or_indexed_view_name } ( column [ , ...n ] )
    [ WHERE <filter_predicate> ]
    [ WITH
        [ FULLSCAN
            [ [ , ] PERSIST_SAMPLE_PERCENT = { ON | OFF } ]
          | SAMPLE number { PERCENT | ROWS }
            [ [ , ] PERSIST_SAMPLE_PERCENT = { ON | OFF } ]
          | <update_stats_stream_option> [ , ...n ]
        [ [ , ] NORECOMPUTE ]
        [ [ , ] INCREMENTAL = { ON | OFF } ]
        [ [ , ] MAXDOP = max_degree_of_parallelism ]
        [ [ , ] AUTO_DROP = { ON | OFF } ]
        ]
    ];

<filter_predicate> ::=
    <conjunct> [ AND <conjunct> ]

<conjunct> ::=
    <disjunct> | <comparison>

<disjunct> ::=
        column_name IN (constant , ...)

<comparison> ::=
        column_name <comparison_op> constant

<comparison_op> ::=
    IS | IS NOT | = | <> | != | > | >= | !> | < | <= | !<

<update_stats_stream_option> ::=
    [ STATS_STREAM = stats_stream ]
    [ ROWCOUNT = numeric_constant ]
    [ PAGECOUNT = numeric_contant ]
```

Syntax for Azure Synapse Analytics and Analytics Platform System (PDW).

```syntaxsql
CREATE STATISTICS statistics_name
    ON { database_name.schema_name.table_name | schema_name.table_name | table_name }
    ( column_name  [ , ...n ] )
    [ WHERE <filter_predicate> ]
    [ WITH {
           FULLSCAN
           | SAMPLE number PERCENT
      }
    ]
[ ; ]

<filter_predicate> ::=
    <conjunct> [ AND <conjunct> ]

<conjunct> ::=
    <disjunct> | <comparison>

<disjunct> ::=
        column_name IN (constant , ...)

<comparison> ::=
        column_name <comparison_op> constant

<comparison_op> ::=
    IS | IS NOT | = | <> | != | > | >= | !> | < | <= | !<
```

Syntax for Microsoft Fabric.

```syntaxsql
CREATE STATISTICS statistics_name
    ON { database_name.schema_name.table_name | schema_name.table_name | table_name }
    ( column_name )
    [ WITH {
           FULLSCAN
           | SAMPLE number PERCENT
      }
    ]
[ ; ]
```

[!INCLUDE [sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### statistics_name

The name of the statistics to create.

#### table_or_indexed_view_name

The name of the table, indexed view, or external table on which to create the statistics. To create statistics on another database, specify a qualified table name.

#### column [ ,...n ]

One or more columns to be included in the statistics. The columns should be in priority order from left to right. Only the first column is used for creating the histogram. All columns are used for cross-column correlation statistics called densities.

You can specify any column that can be specified as an index key column with the following exceptions:

- **xml**, full-text, and FILESTREAM columns can't be specified.

- Computed columns can be specified only if the `ARITHABORT` and `QUOTED_IDENTIFIER` database settings are `ON`.

- CLR user-defined type columns can be specified if the type supports binary ordering. Computed columns defined as method invocations of a user-defined type column can be specified if the methods are marked deterministic.

#### WHERE <filter_predicate>

Specifies an expression for selecting a subset of rows to include when creating the statistics object. Statistics that are created with a filter predicate are called filtered statistics. The filter predicate uses simple comparison logic and can't reference a computed column, a UDT column, a spatial data type column, or a **hierarchyID** data type column. Comparisons using `NULL` literals aren't allowed with the comparison operators. Use the `IS NULL` and `IS NOT NULL` operators instead.

Here are some examples of filter predicates for the `Production.BillOfMaterials` table:

- `WHERE StartDate > '20000101' AND EndDate <= '20000630'`
- `WHERE ComponentID IN (533, 324, 753)`
- `WHERE StartDate IN ('20000404', '20000905') AND EndDate IS NOT NULL`

For more information about filter predicates, see [Create filtered indexes](../../relational-databases/indexes/create-filtered-indexes.md).

#### FULLSCAN

**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] SP 1 CU 4, [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] CU 1, and later versions

Compute statistics by scanning all rows. `FULLSCAN` and `SAMPLE 100 PERCENT` have the same results. `FULLSCAN` can't be used with the `SAMPLE` option.

When omitted, SQL Server uses sampling to create the statistics, and determines the sample size that is required to create a high quality query plan.

In [!INCLUDE [fabricdw](../../includes/fabric-dw.md)] in [!INCLUDE [fabric](../../includes/fabric.md)], only single-column `FULLSCAN` and single-column `SAMPLE`-based statistics are supported. When no option is included, `SAMPLE` statistics are created.

#### SAMPLE number { PERCENT | ROWS }

Specifies the approximate percentage, or number of rows, in the table or indexed view for the query optimizer to use when it creates statistics. For `PERCENT`, *number* can be from 0 through 100 and for `ROWS`, *number* can be from 0 to the total number of rows. The actual percentage or number of rows the query optimizer samples might not match the percentage or number specified. For example, the query optimizer scans all rows on a data page.

`SAMPLE` is useful for special cases in which the query plan, based on default sampling, isn't optimal. In most situations, it's not necessary to specify `SAMPLE` because the query optimizer already uses sampling and determines the statistically significant sample size by default, as required to create high-quality query plans.

`SAMPLE` can't be used with the FULLSCAN option. When `SAMPLE` or `FULLSCAN` aren't specified, the query optimizer uses sampled data and computes the sample size by default.

We recommend against specifying `0 PERCENT` or `0 ROWS`. When `0 PERCENT` or `0 ROWS` is specified, the statistics object is created, but doesn't contain statistics data.

In [!INCLUDE [fabricdw](../../includes/fabric-dw.md)] in [!INCLUDE [fabric](../../includes/fabric.md)], only single-column `FULLSCAN` and single-column `SAMPLE`-based statistics are supported. When no option is included, `FULLSCAN` statistics are created.

#### PERSIST_SAMPLE_PERCENT = { ON | OFF }

When `ON`, the statistics retain the creation sampling percentage for subsequent updates that don't explicitly specify a sampling percentage. When `OFF`, statistics sampling percentage gets reset to default sampling in subsequent updates that don't explicitly specify a sampling percentage. The default is `OFF`.

> [!NOTE]  
> If the table is truncated, all statistics built on the truncated HoBT will revert to using the default sampling percentage.

#### STATS_STREAM = *stats_stream*

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

#### NORECOMPUTE

Disable the automatic statistics update option, `AUTO_STATISTICS_UPDATE`, for *statistics_name*. If this option is specified, the query optimizer will complete any in-progress statistics updates for *statistics_name* and disable future updates.

To re-enable statistics updates, remove the statistics with [DROP STATISTICS](drop-statistics-transact-sql.md) and then run `CREATE STATISTICS` without the `NORECOMPUTE` option.

> [!WARNING]  
> If you disable automatic updating of statistics, it might prevent the Query Optimizer from picking optimal execution plans for queries that involve the table. You should use this option sparingly, and only by a qualified database administrator.

For more information about the `AUTO_STATISTICS_UPDATE` option, see [ALTER DATABASE SET options](alter-database-transact-sql-set-options.md). For more information about disabling and re-enabling statistics updates, see [Statistics](../../relational-databases/statistics/statistics.md).

#### INCREMENTAL = { ON | OFF }

**Applies to**: [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and later versions

When `ON`, the statistics created are per partition statistics. When `OFF`, stats are combined for all partitions. The default is `OFF`.

If per partition statistics aren't supported, an error is generated. Incremental stats aren't supported for following statistics types:

- Statistics created with indexes that aren't partition-aligned with the base table.
- Statistics created on Always On readable secondary databases.
- Statistics created on read-only databases.
- Statistics created on filtered indexes.
- Statistics created on views.
- Statistics created on internal tables.
- Statistics created with spatial indexes or XML indexes.

#### MAXDOP = *max_degree_of_parallelism*

**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] SP 2, [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] CU 3, and later versions

Overrides the **max degree of parallelism** configuration option during the statistic operation. For more information, see [Configure the max degree of parallelism (server configuration option)](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md). Use `MAXDOP` to limit the number of processors used in a parallel plan execution. The maximum is 64 processors.

*max_degree_of_parallelism* can be:

- `1`: Suppresses parallel plan generation.
- `>1`: Restricts the maximum number of processors used in a parallel index operation to the specified number.
- `0` (default): Uses the actual number of processors or fewer based on the current system workload.

#### update_stats_stream_option

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

#### AUTO_DROP = { ON | OFF }

**Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)]

Before [!INCLUDE [ssSQL22](../../includes/sssql22-md.md)], if statistics are manually created by a user or third party tool on a user database, those statistics objects can block or interfere with schema changes the customer might desire.

Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], the `AUTO_DROP` option is enabled by default on all new and migrated databases. The `AUTO_DROP` property allows the creation of statistics objects in a mode such that a subsequent schema change is *not* blocked by the statistic object, but instead the statistics are dropped as necessary. In this way, manually created statistics with `AUTO_DROP` enabled behave like autocreated statistics.

> [!NOTE]  
> Trying to set or unset the *Auto_Drop* property on auto-created statistics might raise errors. Auto-created statistics always uses auto drop. Some backups, when restored, might have this property set incorrectly until the next time the statistics object is updated (manually or automatically). However, auto-created statistics always behave like auto drop statistics. When restoring a database to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] from a previous version, it's recommended to execute `sp_updatestats` on the database, setting the proper metadata for the statistics `AUTO_DROP` feature.

For more information, see [AUTO_DROP option](../../relational-databases/statistics/statistics.md#auto_drop-option).

## Permissions

Requires one of these permissions:

- `ALTER TABLE`
- User is the table owner
- Membership in the **db_ddladmin** fixed database role

## Remarks

SQL Server can use `tempdb` to sort the sampled rows before building statistics.

### Statistics for external tables

When creating external table statistics, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] imports the external table into a temporary [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] table, and then creates the statistics. For samples statistics, only the sampled rows are imported. If you have a large external table, it's faster to use the default sampling instead of the full scan option.

When the external table is using `DELIMITEDTEXT`, `CSV`, `PARQUET`, or `DELTA` as data types, external tables only support statistics for one column per `CREATE STATISTICS` command.

### Statistics with a filtered condition

Filtered statistics can improve query performance for queries that select from well-defined subsets of data. Filtered statistics use a filter predicate in the WHERE clause to select the subset of data that is included in the statistics.

### When to use CREATE STATISTICS

For more information about when to use `CREATE STATISTICS`, see [Statistics](../../relational-databases/statistics/statistics.md).

### Reference dependencies for filtered statistics

The [sys.sql_expression_dependencies](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md) catalog view tracks each column in the filtered statistics predicate as a referencing dependency. Consider the operations that you perform on table columns, before creating filtered statistics. You can't drop, rename, or alter the definition of a table column that is defined in a filtered statistics predicate.

## Limitations

- Updating statistics isn't supported on external tables. To update statistics on an external table, drop and re-create the statistics.
- You can list up to 64 columns per statistics object.
- The `MAXDOP` option isn't compatible with `STATS_STREAM`, `ROWCOUNT`, and `PAGECOUNT` options.
- The `MAXDOP` option is limited by the Resource Governor workload group `MAX_DOP` setting, if used.
- `CREATE` and `DROP STATISTICS` on external tables aren't supported in Azure SQL Database.

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Use CREATE STATISTICS with SAMPLE number PERCENT

The following example creates the `ContactMail1` statistics, using a random sample of 5 percent of the `BusinessEntityID` and `EmailPromotion` columns of the `Person` table of the [!INCLUDE [ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.

```sql
CREATE STATISTICS ContactMail1
    ON Person.Person (BusinessEntityID, EmailPromotion)
    WITH SAMPLE 5 PERCENT;
```

### B. Use CREATE STATISTICS with FULLSCAN and NORECOMPUTE

The following example creates the `NamePurchase` statistics for all rows in the `BusinessEntityID` and `EmailPromotion` columns of the `Person` table and disables automatic recomputing of statistics.

```sql
CREATE STATISTICS NamePurchase
    ON AdventureWorks2022.Person.Person (BusinessEntityID, EmailPromotion)
    WITH FULLSCAN, NORECOMPUTE;
```

### C. Use CREATE STATISTICS to create filtered statistics

The following example creates the filtered statistics `ContactPromotion1`. The [!INCLUDE [ssDE](../../includes/ssde-md.md)] samples 50 percent of the data and then selects the rows with `EmailPromotion` equal to 2.

```sql
CREATE STATISTICS ContactPromotion1
    ON Person.Person (BusinessEntityID, LastName, EmailPromotion)
WHERE EmailPromotion = 2
WITH SAMPLE 50 PERCENT;
GO
```

### D. Create statistics on an external table

The only decision you need to make when you create statistics on an external table, besides providing the list of columns, is whether to create the statistics by sampling the rows or by scanning all of the rows. `CREATE` and `DROP STATISTICS` on external tables aren't supported in Azure SQL Database.

Since [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] imports data from the external table into a temporary table to create statistics, the full scan option takes much longer. For a large table, the default sampling method is usually sufficient.

```sql
--Create statistics on an external table and use default sampling.
CREATE STATISTICS CustomerStats1 ON DimCustomer (CustomerKey, EmailAddress);

--Create statistics on an external table and scan all the rows
CREATE STATISTICS CustomerStats1 ON DimCustomer (CustomerKey, EmailAddress) WITH FULLSCAN;
```

### E. Use CREATE STATISTICS with FULLSCAN and PERSIST_SAMPLE_PERCENT

The following example creates the `NamePurchase` statistics for all rows in the `BusinessEntityID` and `EmailPromotion` columns of the `Person` table and sets a 100 percent sampling percentage for all subsequent updates that don't explicitly specify a sampling percentage.

```sql
CREATE STATISTICS NamePurchase
    ON AdventureWorks2022.Person.Person (BusinessEntityID, EmailPromotion)
    WITH FULLSCAN, PERSIST_SAMPLE_PERCENT = ON;
```

### Examples using AdventureWorksDW database

### F. Create statistics on two columns

The following example creates the `CustomerStats1` statistics, based on the `CustomerKey` and `EmailAddress` columns of the `DimCustomer` table. The statistics are created based on a statistically significant sampling of the rows in the `Customer` table.

```sql
CREATE STATISTICS CustomerStats1 ON DimCustomer (CustomerKey, EmailAddress);
```

### G. Create statistics by using a full scan

The following example creates the `CustomerStatsFullScan` statistics, based on scanning all of the rows in the `DimCustomer` table.

```sql
CREATE STATISTICS CustomerStatsFullScan
ON DimCustomer (CustomerKey, EmailAddress) WITH FULLSCAN;
```

### H. Create statistics by specifying the sample percentage

The following example creates the `CustomerStatsSampleScan` statistics, based on scanning 50 percent of the rows in the `DimCustomer` table.

```sql
CREATE STATISTICS CustomerStatsSampleScan
ON DimCustomer (CustomerKey, EmailAddress) WITH SAMPLE 50 PERCENT;
```

### I. Use CREATE STATISTICS with AUTO_DROP

To use [auto drop statistics](#auto_drop---on--off-), just add the following to the "WITH" clause of statistics create or update.

```sql
CREATE STATISTICS CustomerStats1 ON DimCustomer (CustomerKey, EmailAddress) WITH AUTO_DROP = ON
```

To evaluate the auto drop setting on existing statistics, use the `auto_drop` column in [sys.stats](../../relational-databases/system-catalog-views/sys-stats-transact-sql.md):

```sql
SELECT object_id, [name], auto_drop
FROM sys.stats;
```

## Related content

- [Statistics](../../relational-databases/statistics/statistics.md)
- [Statistics in Fabric data warehousing](/fabric/data-warehouse/statistics)
- [UPDATE STATISTICS (Transact-SQL)](update-statistics-transact-sql.md)
- [sp_updatestats (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-updatestats-transact-sql.md)
- [DBCC SHOW_STATISTICS (Transact-SQL)](../database-console-commands/dbcc-show-statistics-transact-sql.md)
- [DROP STATISTICS (Transact-SQL)](drop-statistics-transact-sql.md)
- [sys.stats (Transact-SQL)](../../relational-databases/system-catalog-views/sys-stats-transact-sql.md)
- [sys.stats_columns (Transact-SQL)](../../relational-databases/system-catalog-views/sys-stats-columns-transact-sql.md)
