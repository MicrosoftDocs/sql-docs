---
title: "Statistics | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
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
ms.assetid: b86a88ba-4f7c-4e19-9fbd-2f8bcd3be14a
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Statistics
  The query optimizer uses statistics to create query plans that improve query performance. For most queries, the query optimizer already generates the necessary statistics for a high quality query plan; in a few cases, you need to create additional statistics or modify the query design for best results. This topic discusses statistics concepts and provides guidelines for using query optimization statistics effectively.  
  
##  <a name="DefinitionQOStatistics"></a> Components and Concepts  
 Statistics  
 Statistics for query optimization are objects that contain statistical information about the distribution of values in one or more columns of a table or indexed view. The query optimizer uses these statistics to estimate the *cardinality*, or number of rows, in the query result. These *cardinality estimates* enable the query optimizer to create a high-quality query plan. For example, the query optimizer could use cardinality estimates to choose the index seek operator instead of the more resource-intensive index scan operator, and in doing so improve query performance.  
  
 Each statistics object is created on a list of one or more table columns and includes a histogram displaying the distribution of values in the first column. Statistics objects on multiple columns also store statistical information about the correlation of values among the columns. These correlation statistics, or *densities*, are derived from the number of distinct rows of column values. For more information about statistics objects, see [DBCC SHOW_STATISTICS &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-show-statistics-transact-sql).  
  
 Filtered Statistics  
 Filtered statistics can improve query performance for queries that select from well-defined subsets of data. Filtered statistics use a filter predicate to select the subset of data that is included in the statistics. Well-designed filtered statistics can improve the query execution plan compared with full-table statistics. For more information about the filter predicate, see [CREATE STATISTICS &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-statistics-transact-sql). For more information about when to create filtered statistics, see the [When to Create Statistics](#UpdateStatistics) section in this topic. For a case study, see the blog entry, [Using Filtered Statistics with Partitioned Tables](https://go.microsoft.com/fwlink/?LinkId=178505), on the SQLCAT Web site.  
  
 Statistics Options  
 There are three options that you can set that affect when and how statistics are created and updated. These options are set at the database level only.  
  
 AUTO_CREATE_STATISTICS Option  
 When the automatic create statistics option, AUTO_CREATE_STATISTICS, is on, the query optimizer creates statistics on individual columns in the query predicate, as necessary, to improve cardinality estimates for the query plan. These single-column statistics are created on columns that do not already have a histogram in an existing statistics object. The AUTO_CREATE_STATISTICS option does not determine whether statistics get created for indexes. This option also does not generate filtered statistics. It applies strictly to single-column statistics for the full table.  
  
 When the query optimizer creates statistics as a result of using the AUTO_CREATE_STATISTICS option, the statistics name starts with `_WA`. You can use the following query to determine if the query optimizer has created statistics for a query predicate column.  
  
```  
SELECT OBJECT_NAME(s.object_id) AS object_name,  
    COL_NAME(sc.object_id, sc.column_id) AS column_name,  
    s.name AS statistics_name  
FROM sys.stats AS s JOIN sys.stats_columns AS sc  
    ON s.stats_id = sc.stats_id AND s.object_id = sc.object_id  
WHERE s.name like '_WA%'  
ORDER BY s.name;  
```  
  
 AUTO_UPDATE_STATISTICS Option  
 When the automatic update statistics option, AUTO_UPDATE_STATISTICS, is on, the query optimizer determines when statistics might be out-of-date and then updates them when they are used by a query. Statistics become out-of-date after insert, update, delete, or merge operations change the data distribution in the table or indexed view. The query optimizer determines when statistics might be out-of-date by counting the number of data modifications since the last statistics update and comparing the number of modifications to a threshold. The threshold is based on the number of rows in the table or indexed view.  
  
 The query optimizer checks for out-of-date statistics before compiling a query and before executing a cached query plan. Before compiling a query, the query optimizer uses the columns, tables, and indexed views in the query predicate to determine which statistics might be out-of-date. Before executing a cached query plan, the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] verifies that the query plan references up-to-date statistics.  
  
 The AUTO_UPDATE_STATISTICS option applies to statistics objects created for indexes, single-columns in query predicates, and statistics created with the [CREATE STATISTICS](/sql/t-sql/statements/create-statistics-transact-sql) statement. This option also applies to filtered statistics.  
  
 AUTO_UPDATE_STATISTICS_ASYNC  
 The asynchronous statistics update option, AUTO_UPDATE_STATISTICS_ASYNC, determines whether the query optimizer uses synchronous or asynchronous statistics updates. By default, the asynchronous statistics update option is off, and the query optimizer updates statistics synchronously. The AUTO_UPDATE_STATISTICS_ASYNC option applies to statistics objects created for indexes, single columns in query predicates, and statistics created with the [CREATE STATISTICS](/sql/t-sql/statements/create-statistics-transact-sql) statement.  
  
 Statistics updates can be either synchronous (the default) or asynchronous. With synchronous statistics updates, queries always compile and execute with up-to-date statistics; When statistics are out-of-date, the query optimizer waits for updated statistics before compiling and executing the query. With asynchronous statistics updates, queries compile with existing statistics even if the existing statistics are out-of-date; The query optimizer could choose a suboptimal query plan if statistics are out-of-date when the query compiles. Queries that compile after the asynchronous updates have completed will benefit from using the updated statistics.  
  
 Consider using synchronous statistics when you perform operations that change the distribution of data, such as truncating a table or performing a bulk update of a large percentage of the rows. If you do not update the statistics after completing the operation, using synchronous statistics will ensure statistics are up-to-date before executing queries on the changed data.  
  
 Consider using asynchronous statistics to achieve more predictable query response times for the following scenarios:  
  
-   Your application frequently executes the same query, similar queries, or similar cached query plans. Your query response times might be more predictable with asynchronous statistics updates than with synchronous statistics updates because the query optimizer can execute incoming queries without waiting for up-to-date statistics. This avoids delaying some queries and not others.  
  
-   Your application has experienced client request time outs caused by one or more queries waiting for updated statistics. In some cases, waiting for synchronous statistics could cause applications with aggressive time outs to fail.  
  
 INCREMENTAL STATS  
 When ON, the statistics created are per partition statistics. When OFF, the statistics tree is dropped and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] re-computes the statistics. The default is OFF. This setting overrides the database level INCREMENTAL property.  
  
 When new partitions are added to a large table, statistics should be updated to include the new partitions. However the time required to scan the entire table (FULLSCAN or SAMPLE option) might be quite long. Also, scanning the entire table isn't necessary because only the statistics on the new partitions might be needed. The incremental option creates and stores statistics on a per partition basis, and when updated, only refreshes statistics on those partitions that need new statistics  
  
 If per partition statistics are not supported the option is ignored and a warning is generated. Incremental stats are not supported for following statistics types:  
  
-   Statistics created with indexes that are not partition-aligned with the base table.  
  
-   Statistics created on AlwaysOn readable secondary databases.  
  
-   Statistics created on read-only databases.  
  
-   Statistics created on filtered indexes.  
  
-   Statistics created on views.  
  
-   Statistics created on internal tables.  
  
-   Statistics created with spatial indexes or XML indexes.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
  
 ![Arrow icon used with Back to Top link](../../2014-toc/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [Back to Top](#statistics)  
  
##  <a name="CreateStatistics"></a> When to Create Statistics  
 The query optimizer already creates statistics in the following ways:  
  
1.  The query optimizer creates statistics for indexes on tables or views when the index is created. These statistics are created on the key columns of the index. If the index is a filtered index, the query optimizer creates filtered statistics on the same subset of rows specified for the filtered index. For more information about filtered indexes, see [Create Filtered Indexes](../indexes/create-filtered-indexes.md) and [CREATE INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-index-transact-sql).  
  
2.  The query optimizer creates statistics for single columns in query predicates when AUTO_CREATE_STATISTICS is on.  
  
 For most queries, these two methods for creating statistics ensure a high-quality query plan; in a few cases, you can improve query plans by creating additional statistics with the [CREATE STATISTICS](/sql/t-sql/statements/create-statistics-transact-sql) statement. These additional statistics can capture statistical correlations that the query optimizer does not account for when it creates statistics for indexes or single columns. Your application might have additional statistical correlations in the table data that, if calculated into a statistics object, could enable the query optimizer to improve query plans. For example, filtered statistics on a subset of data rows or multicolumn statistics on query predicate columns might improve the query plan.  
  
 When creating statistics with the CREATE STATISTICS statement, we recommend keeping the AUTO_CREATE_STATISTICS option on so that the query optimizer continues to routinely create single-column statistics for query predicate columns. For more information about query predicates, see [Search Condition &#40;Transact-SQL&#41;](/sql/t-sql/queries/search-condition-transact-sql).  
  
 Consider creating statistics with the CREATE STATISTICS statement when any of the following applies:  
  
-   The [!INCLUDE[ssDE](../../../includes/ssde-md.md)] Tuning Advisor suggests creating statistics.  
  
-   The query predicate contains multiple correlated columns that are not already in the same index.  
  
-   The query selects from a subset of data.  
  
-   The query has missing statistics.  
  
### Query Predicate Contains Multiple Correlated Columns  
 When a query predicate contains multiple columns that have cross-column relationships and dependencies, statistics on the multiple columns might improve the query plan. Statistics on multiple columns contain cross-column correlation statistics, called *densities*, that are not available in single-column statistics. Densities can improve cardinality estimates when query results depend on data relationships among multiple columns.  
  
 If the columns are already in the same index, the multicolumn statistics object already exists and it is not necessary to create it manually. If the columns are not already in the same index, you can create multicolumn statistics by creating an index on the columns or by using the CREATE STATISTICS statement. It requires more system resources to maintain an index than a statistics object. If the application does not require the multicolumn index, you can economize on system resources by creating the statistics object without creating the index.  
  
 When creating multicolumn statistics, the order of the columns in the statistics object definition affects the effectiveness of densities for making cardinality estimates. The statistics object stores densities for each prefix of key columns in the statistics object definition. For more information about densities, see [DBCC SHOW_STATISTICS &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-show-statistics-transact-sql).  
  
 To create densities that are useful for cardinality estimates, the columns in the query predicate must match one of the prefixes of columns in the statistics object definition. For example, the following creates a multicolumn statistics object on the columns `LastName`, `MiddleName`, and `FirstName`.  
  
```  
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
  
 In this example, the statistics object `LastFirst` has densities for the following column prefixes: (`LastName`), (`LastName, MiddleName`), and (`LastName, MiddleName, FirstName`). The density is not available for (`LastName, FirstName`). If the query uses `LastName` and `FirstName` without using `MiddleName`, the density is not available for cardinality estimates.  
  
### Query Selects from a Subset of Data  
 When the query optimizer creates statistics for single columns and indexes, it creates the statistics for the values in all rows. When queries select from a subset of rows, and that subset of rows has a unique data distribution, filtered statistics can improve query plans. You can create filtered statistics by using the CREATE STATISTICS statement with the WHERE clause to define the filter predicate expression.  
  
 For example, using [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)], each product in the Production.Product table belongs to one of four categories in the Production.ProductCategory table: Bikes, Components, Clothing, and Accessories. Each of the categories has a different data distribution for weight: bike weights range from 13.77 to 30.0, component weights range from 2.12 to 1050.00 with some NULL values, clothing weights are all NULL, and accessory weights are also NULL.  
  
 Using Bikes as an example, filtered statistics on all bike weights will provide more accurate statistics to the query optimizer and can improve the query plan quality compared with full-table statistics or nonexistent statistics on the Weight column. The bike weight column is a good candidate for filtered statistics but not necessarily a good candidate for a filtered index if the number of weight lookups is relatively small. The performance gain for lookups that a filtered index provides might not outweigh the additional maintenance and storage cost for adding a filtered index to the database.  
  
 The following statement creates the `BikeWeights` filtered statistics on all of the subcategories for Bikes. The filtered predicate expression defines bikes by enumerating all of the bike subcategories with the comparison `Production.ProductSubcategoryID IN (1,2,3)`. The predicate cannot use the Bikes category name because it is stored in the Production.ProductCategory table, and all columns in the filter expression must be in the same table.  
  
 [!code-sql[StatisticsDDL#FilteredStats2](../../snippets/tsql/SQL14/tsql/statisticsddl/transact-sql/filteredstats.sql#filteredstats2)]  
  
 The query optimizer can use the `BikeWeights` filtered statistics to improve the query plan for the following query that selects all of the bikes that weigh more than `25`.  
  
```  
SELECT P.Weight AS Weight, S.Name AS BikeName  
FROM Production.Product AS P  
    JOIN Production.ProductSubcategory AS S   
    ON P.ProductSubcategoryID = S.ProductSubcategoryID  
WHERE P.ProductSubcategoryID IN (1,2,3) AND P.Weight > 25  
ORDER BY P.Weight;  
GO  
```  
  
### Query Identifies Missing Statistics  
 If an error or other event prevents the query optimizer from creating statistics, the query optimizer creates the query plan without using statistics. The query optimizer marks the statistics as missing and attempts to regenerate the statistics the next time the query is executed.  
  
 Missing statistics are indicated as warnings (table name in red text) when the execution plan of a query is graphically displayed using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. Additionally, monitoring the **Missing Column Statistics** event class by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] indicates when statistics are missing. For more information, see [Errors and Warnings Event Category &#40;Database Engine&#41;](../event-classes/errors-and-warnings-event-category-database-engine.md).  
  
 If statistics are missing, perform the following steps:  
  
-   Verify that AUTO_CREATE_STATISTICS and AUTO_UPDATE_STATISTICS are on.  
  
-   Verify that the database is not read-only. If the database is read-only, the query optimizer cannot save statistics.  
  
-   Create the missing statistics by using the CREATE STATISTICS statement.  
  
 When statistics on a read-only database or read-only snapshot are missing or stale, the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] creates and maintains temporary statistics in `tempdb`. When the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] creates temporary statistics, the statistics name is appended with the suffix _readonly_database_statistic to differentiate the temporary statistics from the permanent statistics. The suffix _readonly_database_statistic is reserved for statistics generated by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. Scripts for the temporary statistics can be created and reproduced on a read-write database. When scripted, [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] changes the suffix of the statistics name from _readonly_database_statistic to _readonly_database_statistic_scripted.  
  
 Only [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] can create and update temporary statistics. However, you can delete temporary statistics and monitor statistics properties using the same tools that you use for permanent statistics:  
  
-   Delete temporary statistics using the [DROP STATISTICS &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-statistics-transact-sql) statement.  
  
-   Monitor statistics using the **sys.stats** and **sys.stats_columns** catalog views. **sys_stats** includes the **is_temporary** column, to indicate which statistics are permanent and which are temporary.  
  
 Because temporary statistics are stored in `tempdb`, a restart of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service causes all temporary statistics to disappear.  
  
 ![Arrow icon used with Back to Top link](../../2014-toc/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [Back to Top](#statistics)  
  
##  <a name="UpdateStatistics"></a> When to Update Statistics  
 The query optimizer determines when statistics might be out-of-date and then updates them when they are needed for a query plan. In some cases you can improve the query plan and therefore improve query performance by updating statistics more frequently than occur when AUTO_UPDATE_STATISTICS is on. You can update statistics with the UPDATE STATISTICS statement or the stored procedure sp_updatestats.  
  
 Updating statistics ensures that queries compile with up-to-date statistics. However, updating statistics causes queries to recompile. We recommend not updating statistics too frequently because there is a performance tradeoff between improving query plans and the time it takes to recompile queries. The specific tradeoffs depend on your application.  
  
 When updating statistics with UPDATE STATISTICS or sp_updatestats, we recommend keeping AUTO_UPDATE_STATISTICS set to ON so that the query optimizer continues to routinely update statistics. For more information about how to update statistics on a column, an index, a table, or an indexed view, see [UPDATE STATISTICS &#40;Transact-SQL&#41;](/sql/t-sql/statements/update-statistics-transact-sql). For information about how to update statistics for all user-defined and internal tables in the database, see the stored procedure [sp_updatestats &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-updatestats-transact-sql).  
  
 To determine when statistics were last updated, use the [STATS_DATE](/sql/t-sql/functions/stats-date-transact-sql) function.  
  
 Consider updating statistics for the following conditions:  
  
-   Query execution times are slow.  
  
-   Insert operations occur on ascending or descending key columns.  
  
-   After maintenance operations.  
  
### Query Execution Times Are Slow  
 If query response times are slow or unpredictable, ensure that queries have up-to-date statistics before performing additional troubleshooting steps.  
  
### Insert Operations Occur on Ascending or Descending Key Columns  
 Statistics on ascending or descending key columns, such as IDENTITY or real-time timestamp columns, might require more frequent statistics updates than the query optimizer performs. Insert operations append new values to ascending or descending columns. The number of rows added might be too small to trigger a statistics update. If statistics are not up-to-date and queries select from the most recently added rows, the current statistics will not have cardinality estimates for these new values. This can result in inaccurate cardinality estimates and slow query performance.  
  
 For example, a query that selects from the most recent sales order dates will have inaccurate cardinality estimates if the statistics are not updated to include cardinality estimates for the most recent sales order dates.  
  
### After Maintenance Operations  
 Consider updating statistics after performing maintenance procedures that change the distribution of data, such as truncating a table or performing a bulk insert of a large percentage of the rows. This can avoid future delays in query processing while queries wait for automatic statistics updates.  
  
 Operations such as rebuilding, defragmenting, or reorganizing an index do not change the distribution of data. Therefore, you do not need to update statistics after performing ALTER INDEX REBUILD, DBCC REINDEX, DBCC INDEXDEFRAG, or ALTER INDEX REORGANIZE operations. The query optimizer updates statistics when you rebuild an index on a table or view with ALTER INDEX REBUILD or DBCC DBREINDEX, however; this statistics update is a byproduct of re-creating the index. The query optimizer does not update statistics after DBCC INDEXDEFRAG or ALTER INDEX REORGANIZE operations.  
  
 ![Arrow icon used with Back to Top link](../../2014-toc/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [Back to Top](#statistics)  
  
##  <a name="DesignStatistics"></a> Queries That Use Statistics Effectively  
 Certain query implementations, such as local variables and complex expressions in the query predicate, can lead to suboptimal query plans. Following query design guidelines for using statistics effectively can help to avoid this. For more information about query predicates, see [Search Condition &#40;Transact-SQL&#41;](/sql/t-sql/queries/search-condition-transact-sql).  
  
 You can improve query plans by applying query design guidelines that use statistics effectively to improve *cardinality estimates* for expressions, variables, and functions used in query predicates. When the query optimizer does not know the value of an expression, variable, or function, it does not know which value to lookup in the histogram and therefore cannot retrieve the best cardinality estimate from the histogram. Instead, the query optimizer bases the cardinality estimate on the average number of rows per distinct value for all of the sampled rows in the histogram. This leads to suboptimal cardinality estimates and can hurt query performance.  
  
 The following guidelines describe how to write queries to improve query plans by improving cardinality estimates.  
  
### Improving Cardinality Estimates for Expressions  
 To improve cardinality estimates for expressions, follow these guidelines:  
  
-   Whenever possible, simplify expressions with constants in them. The query optimizer does not evaluate all functions and expressions containing constants prior to determining cardinality estimates. For example, simplify the expression ABS(`-100) to 100`.  
  
-   If the expression uses multiple variables, consider creating a computed column for the expression and then create statistics or an index on the computed column. For example, the query predicate `WHERE PRICE + Tax > 100` might have a better cardinality estimate if you create a computed column for the expression `Price + Tax`.  
  
### Improving Cardinality Estimates for Variables and Functions  
 To improve the cardinality estimates for variables and functions, follow these guidelines:  
  
-   If the query predicate uses a local variable, consider rewriting the query to use a parameter instead of a local variable. The value of a local variable is not known when the query optimizer creates the query execution plan. When a query uses a parameter, the query optimizer uses the cardinality estimate for the first actual parameter value that is passed to the stored procedure.  
  
-   Consider using a standard table or temporary table to hold the results of multi-statement table-valued functions. The query optimizer does not create statistics for multi-statement table-valued functions. With this approach the query optimizer can create statistics on the table columns and use them to create a better query plan.  
  
-   Consider using a standard table or temporary table as a replacement for table variables. The query optimizer does not create statistics for table variables. With this approach the query optimizer can create statistics on the table columns and use them to create a better query plan. There are tradeoffs in determining whether to use a temporary table or a table variable; Table variables used in stored procedures cause fewer recompilations of the stored procedure than temporary tables. Depending on the application, using a temporary table instead of a table variable might not improve performance.  
  
-   If a stored procedure contains a query that uses a passed-in parameter, avoid changing the parameter value within the stored procedure before using it in the query. The cardinality estimates for the query are based on the passed-in parameter value and not the updated value. To avoid changing the parameter value, you can rewrite the query to use two stored procedures.  
  
     For example, the following stored procedure `Sales.GetRecentSales` changes the value of the parameter `@date` when `@date is NULL`.  
  
    ```  
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
    END  
    GO  
    ```  
  
     If the first call to the stored procedure `Sales.GetRecentSales` passes a NULL for the `@date` parameter, the query optimizer will compile the stored procedure with the cardinality estimate for `@date = NULL` even though the query predicate is not called with `@date = NULL`. This cardinality estimate might be significantly different than the number of rows in the actual query result. As a result, the query optimizer might choose a suboptimal query plan. To help avoid this, you can rewrite the stored procedure into two procedures as follows:  
  
    ```  
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
  
### Improving Cardinality Estimates with Query Hints  
 To improve cardinality estimates for local variables, you can use the OPTIMIZE FOR or OPTIMIZE FOR UNKNOWN query hints with RECOMPILE. For more information, see [Query Hints &#40;Transact-SQL&#41;](/sql/t-sql/queries/hints-transact-sql-query).  
  
 For some applications, recompiling the query each time it executes might take too much time. The OPTIMIZER FOR query hint can help even if you don't use the RECOMPILE option. For example, you could add an OPTIMIZER FOR option to the stored procedure Sales.GetRecentSales to specify a specific date. The following example adds the OPTIMIZE FOR option to the Sales.GetRecentSales procedure.  
  
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
  
### Improving Cardinality Estimates with Plan Guides  
 For some applications, query design guidelines might not apply because you cannot change the query or using the RECOMPILE query hint might be cause too many recompiles. You can use plan guides to specify other hints, such as USE PLAN, to control the behavior of the query while investigating application changes with the application vendor. For more information about plan guides, see [Plan Guides](../performance/plan-guides.md).  
  
 ![Arrow icon used with Back to Top link](../../2014-toc/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [Back to Top](#statistics)  
  
## See Also  
 [CREATE STATISTICS &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-statistics-transact-sql)   
 [UPDATE STATISTICS &#40;Transact-SQL&#41;](/sql/t-sql/statements/update-statistics-transact-sql)   
 [sp_updatestats &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-updatestats-transact-sql)   
 [DBCC SHOW_STATISTICS &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-show-statistics-transact-sql)   
 [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-set-options)   
 [DROP STATISTICS &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-statistics-transact-sql)   
 [CREATE INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-index-transact-sql)   
 [ALTER INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-index-transact-sql)   
 [Create Filtered Indexes](../indexes/create-filtered-indexes.md)  
