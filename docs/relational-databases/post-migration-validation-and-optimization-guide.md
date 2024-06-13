---
title: Post-migration validation and optimization guide
description: Learn about the post-migration validation and optimization steps in SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/13/2024
ms.service: sql
ms.topic: conceptual
helpviewer_keywords:
  - "post-migration validation and optimization"
  - "guide, post-migration validation and optimization"
---
# Post-migration validation and optimization guide

[!INCLUDE [sqlserver](../includes/applies-to-version/sqlserver.md)]

The [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] post migration step is crucial for reconciling any data accuracy and completeness, and uncovering performance issues with the workload.

## Common performance scenarios

Following are some of the common performance scenarios encountered after migrating to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Platform and how to resolve them. These include scenarios that are specific to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] migration (older versions to newer versions), and foreign platform (such as Oracle, DB2, MySQL, and Sybase) to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] migration.

## <a id="CEUpgrade"></a> Query regressions due to change in cardinality estimator (CE) version

**Applies to:** [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] migration.

When migrating from an older version of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] to [!INCLUDE [ssSQL14](../includes/sssql14-md.md)] or later versions, and upgrading the [database compatibility level](databases/view-or-change-the-compatibility-level-of-a-database.md) to the latest available, a workload might be exposed to the risk of performance regression.

This is because starting with [!INCLUDE [ssSQL14](../includes/sssql14-md.md)], all Query Optimizer changes are tied to the latest [database compatibility level](databases/view-or-change-the-compatibility-level-of-a-database.md), so plans aren't changed right at point of upgrade but rather when a user changes the `COMPATIBILITY_LEVEL` database option to the latest one. This capability, in combination with Query Store gives you a great level of control over the query performance in the upgrade process.

For more information on Query Optimizer changes introduced in [!INCLUDE [ssSQL14](../includes/sssql14-md.md)], see [Optimizing Your Query Plans with the SQL Server 2014 Cardinality Estimator](/previous-versions/dn673537(v=msdn.10)).

For more information about the CE, see [Cardinality Estimation (SQL Server)](performance/cardinality-estimation-sql-server.md).

### Steps to resolve

Change the [database compatibility level](databases/view-or-change-the-compatibility-level-of-a-database.md) to the source version, and follow the recommended upgrade workflow as shown in the following picture:

:::image type="content" source="performance/media/query-store-usage-5.png" alt-text="Diagram showing the recommended upgrade workflow." lightbox="performance/media/query-store-usage-5.png":::

For more information on this article, see [Keep performance stability during the upgrade to newer SQL Server](../relational-databases/performance/query-store-usage-scenarios.md#CEUpgrade).

## <a id="ParameterSniffing"></a> Sensitivity to parameter sniffing

**Applies to:** Foreign platform (such as Oracle, DB2, MySQL, and Sybase) to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] migration.

> [!NOTE]  
> For [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] migrations, if this issue existed in the source [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)], migrating to a newer version of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] as-is will not address this scenario.

[!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] compiles query plans on stored procedures by using sniffing the input parameters at the first compile, generating a parameterized and reusable plan, optimized for that input data distribution. Even if not stored procedures, most statements generating trivial plans are parameterized. After a plan is first cached, any future execution maps to a previously cached plan.

A potential problem arises when that first compilation doesn't use the most common sets of parameters for the usual workload. For different parameters, the same execution plan becomes inefficient. For more information on this article, see [Parameter sensitivity](../relational-databases/query-processing-architecture-guide.md#parameter-sensitivity).

### Steps to resolve

1. Use the `RECOMPILE` hint. A plan is calculated every time adapted to each parameter value.
1. Rewrite the stored procedure to use the option `(OPTIMIZE FOR(<input parameter> = <value>))`. Decide which value to use that suits most of the relevant workload, creating and maintaining one plan that becomes efficient for the parameterized value.
1. Rewrite the stored procedure using local variable inside the procedure. Now the optimizer uses the density vector for estimations, resulting in the same plan regardless of the parameter value.
1. Rewrite the stored procedure to use the option `(OPTIMIZE FOR UNKNOWN)`. Same effect as using the local variable technique.
1. Rewrite the query to use the hint `DISABLE_PARAMETER_SNIFFING`. Same effect as using the local variable technique by totally disabling parameter sniffing, unless `OPTION(RECOMPILE)`, `WITH RECOMPILE` or `OPTIMIZE FOR <value>` is used.

> [!TIP]  
> Use the [!INCLUDE [ssManStudio](../includes/ssmanstudio-md.md)] Plan Analysis feature to quickly identify if this is an issue. For more information, see [New in SSMS: Query Performance Troubleshooting made easier!](/archive/blogs/sql_server_team/new-in-ssms-query-performance-troubleshooting-made-easier).

## <a id="MissingIndexes"></a> Missing indexes

**Applies to:** Foreign platform (such as Oracle, DB2, MySQL, and Sybase) and [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] migration.

Incorrect or missing indexes cause extra I/O that leads to extra memory and CPU being wasted. This might be because the workload profile has changed, such as using different predicates, invalidating existing index design. Evidence of a poor indexing strategy or changes in workload profile include:

- Look for duplicate, redundant, rarely used and completely unused indexes.
- Special care with unused indexes with updates.

### Steps to resolve

1. Use the graphical execution plan for any Missing Index references.
1. Indexing suggestions generated by [Database Engine Tuning Advisor](../tools/dta/tutorial-database-engine-tuning-advisor.md).
1. Use the [sys.dm_db_missing_index_details](system-dynamic-management-views/sys-dm-db-missing-index-details-transact-sql.md) or through the [SQL Server Performance Dashboard](https://www.microsoft.com/download/details.aspx?id=29063).
1. Use pre-existing scripts that can use existing DMVs to provide insight into any missing, duplicate, redundant, rarely used and completely unused indexes, but also if any index reference is hinted/hard-coded into existing procedures and functions in your database.

> [!TIP]  
> Examples of such pre-existing scripts include [Index Creation](https://github.com/Microsoft/tigertoolbox/tree/master/Index-Creation) and [Index Information](https://github.com/Microsoft/tigertoolbox/tree/master/Index-Information).

## <a id="InabilityPredicates"></a> Inability to use predicates to filter data

**Applies to:** Foreign platform (such as Oracle, DB2, MySQL, and Sybase) and [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] migration.

> [!NOTE]  
> For [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] migrations, if this issue existed in the source [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)], migrating to a newer version of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] as-is will not address this scenario.

[!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Query Optimizer can only account for information that is known at compile time. If a workload relies on predicates that can only be known at execution time, then the potential for a poor plan choice increases. For a better-quality plan, predicates must be **SARGable**, or **S**earch **Arg**ument**able**.

Some examples of non-SARGable predicates:

- Implicit data conversions, like **varchar** to **nvarchar**, or **int** to **varchar**. Look for runtime `CONVERT_IMPLICIT` warnings in the Actual Execution Plans. Converting from one type to another can also cause a loss of precision.
- Complex undetermined expressions such as `WHERE UnitPrice + 1 < 3.975`, but not `WHERE UnitPrice < 320 * 200 * 32`.
- Expressions using functions, such as `WHERE ABS(ProductID) = 771` or `WHERE UPPER(LastName) = 'Smith'`
- Strings with a leading wildcard character, such as `WHERE LastName LIKE '%Smith'`, but not `WHERE LastName LIKE 'Smith%'`.

### Steps to resolve

1. Always declare variables/parameters as the intended target [Data types](../t-sql/data-types/data-types-transact-sql.md).

   This might involve comparing any user-defined code construct that is stored in the database (such as stored procedures, user-defined functions, or views) with system tables that hold information on data types used in underlying tables (such as [sys.columns (Transact-SQL)](system-catalog-views/sys-columns-transact-sql.md)).

1. If unable to traverse all code to the previous point, then for the same purpose, change the data type on the table to match any variable/parameter declaration.

1. Reason out the usefulness of the following constructs:

   - Functions being used as predicates;
   - Wildcard searches;
   - Complex expressions based on columnar data - evaluate the need to instead create persisted computed columns, which can be indexed;

> [!NOTE]  
> All of these steps can be done programmatically.

## <a id="TableValuedFunctions"></a> Use of table valued functions (multi-statement vs inline)

**Applies to:** Foreign platform (such as Oracle, DB2, MySQL, and Sybase) and [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] migration.

> [!NOTE]  
> For [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] to [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] migrations, if this issue existed in the source [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)], migrating to a newer version of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] as-is will not address this scenario.

Table Valued Functions return a table data type that can be an alternative to views. While views are limited to a single `SELECT` statement, user-defined functions can contain additional statements that allow more logic than is possible in views.

> [!IMPORTANT]  
> Since the output table of an multi-statement table valued function (MSTVF) isn't created at compile time, the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Query Optimizer relies on heuristics, and not actual statistics, to determine row estimations.
> Even if indexes are added to the base table(s), this isn't going to help.
> For MSTVFs, [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] uses a fixed estimation of 1 for the number of rows expected to be returned by an MSTVF (starting with [!INCLUDE [ssSQL14](../includes/sssql14-md.md)] that fixed estimation is 100 rows).

### Steps to resolve

1. If the MSTVF is single statement only, convert to an inline table valued function.

    ```sql
    CREATE FUNCTION dbo.tfnGetRecentAddress(@ID int)
    RETURNS @tblAddress TABLE
    ([Address] VARCHAR(60) NOT NULL)
    AS
    BEGIN
      INSERT INTO @tblAddress ([Address])
      SELECT TOP 1 [AddressLine1]
      FROM [Person].[Address]
      WHERE  AddressID = @ID
      ORDER BY [ModifiedDate] DESC
    RETURN
    END
    ```

    The inline format example is displayed next.

    ```sql
    CREATE FUNCTION dbo.tfnGetRecentAddress_inline(@ID int)
    RETURNS TABLE
    AS
    RETURN (
      SELECT TOP 1 [AddressLine1] AS [Address]
      FROM [Person].[Address]
      WHERE  AddressID = @ID
      ORDER BY [ModifiedDate] DESC
    )
    ```

1. If more complex, consider using intermediate results stored in Memory-Optimized tables or temporary tables.

## Related content

- [Best Practice with the Query Store](performance/best-practice-with-the-query-store.md)  
- [Memory-Optimized Tables](in-memory-oltp/sample-database-for-in-memory-oltp.md)  
- [User-Defined Functions](user-defined-functions/user-defined-functions.md)  
- [Table Variables and Row Estimations - Part 1](/archive/blogs/blogdoezequiel/table-variables-and-row-estimations-part-1)  
- [Table Variables and Row Estimations - Part 2](/archive/blogs/blogdoezequiel/table-variables-and-row-estimations-part-2)  
- [Execution Plan Caching and Reuse](../relational-databases/query-processing-architecture-guide.md#execution-plan-caching-and-reuse)
