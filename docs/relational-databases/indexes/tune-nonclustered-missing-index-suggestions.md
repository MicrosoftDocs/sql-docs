---
title: Tune nonclustered indexes with missing index suggestions
description: How to use missing index suggestions to create and tune nonclustered indexes.
author: MikeRayMSFT
ms.author: mikeray
ms.date: 3/15/2022
ms.service: sql
ms.topic: how-to
ms.custom: "template-how-to #Required; leave this attribute/value as-is."
---

# Tune nonclustered indexes with missing index suggestions
[!INCLUDE [SQL Server Azure SQL Database Azure SQL MI](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The missing indexes feature is a lightweight tool for finding missing indexes that might significantly improve query performance. This article describes how to use missing index suggestions to effectively tune indexes and improve query performance.

## Limitations of the missing index feature

When the query optimizer generates a query plan, it analyzes what the best indexes are for a particular filter condition. If the best indexes don't exist, the query optimizer still generates a query plan using the least-costly access methods available, but also stores information about these indexes. The missing indexes feature enables you to access that information about best possible indexes so you can decide whether they should be implemented.

Query optimization is a time sensitive process, so there are limitations to the missing index feature. Limitations include:

- Missing index suggestions are based on estimates made during the optimization of a single query, prior to query execution. Missing index suggestions aren't tested or updated after query execution.
- The missing index feature suggests only nonclustered disk-based rowstore indexes. [Unique](../sql-server-index-design-guide.md#Unique) and [filtered indexes](../sql-server-index-design-guide.md#Filtered) aren't suggested.
- [Key columns](../sql-server-index-design-guide.md#key-columns) are suggested, but the suggestion doesn't specify an order for those columns. For information on ordering columns, see the [Apply missing index suggestions](#apply-missing-index-suggestions) section of this article.
- [Included columns](../sql-server-index-design-guide.md#Included_Columns) are suggested, but SQL Server performs no cost-benefit analysis regarding the size of the resulting index when a large number of included columns are suggested.
- Missing index requests may offer similar variations of indexes on the same table and column(s) across queries. It's important to [review index suggestions and combine where possible](#review-indexes-and-combine-where-possible).
- Suggestions aren't made for trivial query plans.
- Cost information is less accurate for queries involving only inequality predicates.
- Suggestions are gathered for a maximum of 500 missing index groups. After this threshold is reached, no more missing index group data is gathered.

Due to these limitations, missing index suggestions are best treated as one of several sources of information when performing index analysis, design, tuning, and testing. Missing index suggestions are not prescriptions to create indexes exactly as suggested.

> [!NOTE]
> Azure SQL Database offers [automatic index tuning](/azure/azure-sql/database/automatic-tuning-overview). Automatic index tuning uses machine learning to learn horizontally from all databases in Azure SQL Database through AI and dynamically improve its tuning actions. Automatic index tuning includes a verification process to ensure there is a positive improvement to the workload performance from indexes created.

## View missing index recommendations

The missing indexes feature consists of two components:

- The `MissingIndexes` element in the XML of [execution plans](../performance/execution-plans.md). This allows you to correlate indexes that the query optimizer considers missing with the queries for which they are missing.
- A set of dynamic management views (DMVs) that can be queried to return information about missing indexes. This allows you to view all of the missing index recommendations for a database.

### View missing index suggestions in execution plans

[Query execution plans](../performance/execution-plans.md) can be generated or obtained in multiple ways:

- When writing or tuning a query, you can use [SQL Server Management Studio](../../ssms/menu-help/about-sql-server-management-studio.md) (SSMS) to [display the estimated execution plan](../performance/display-the-estimated-execution-plan.md) without running the query, or execute the query and [display an actual execution plan](../performance/display-an-actual-execution-plan.md).
- [Query Store](../performance/monitoring-performance-by-using-the-query-store.md), when enabled, collects execution plans.
- You can identify cached execution plans by querying DMVs such as [sys.dm_exec_text_query_plan](../system-dynamic-management-views/sys-dm-exec-text-query-plan-transact-sql.md).

For example, you can use the following query to generate missing index requests against the [AdventureWorks sample database](../../samples/adventureworks-install-configure.md).

```sql
SELECT City, StateProvinceID, PostalCode  
FROM Person.Address as a
JOIN Person.BusinessEntityAddress as ba on
	a.AddressID = ba.AddressID
JOIN Person.Person as  p on
	ba.BusinessEntityID = p.BusinessEntityID
WHERE p.FirstName like 'K%' and
	StateProvinceID = 9;  
GO 
```

To generate and view the missing index requests:

1. Open SSMS and connect a session to your copy of the [AdventureWorks sample database](../../samples/adventureworks-install-configure.md).
1. Paste the query into the session and [generate an estimated execution plan](../performance/display-the-estimated-execution-plan.md) in SSMS for the query by selecting the **Display Estimated Execution Plan** toolbar button.
    The execution plan will display in a pane in the current session. A green **Missing Index** statement will appear near the top of the graphic plan.

    :::image type="content" source="media/missing-index-graphic-execution-plan-ssms.png" alt-text="A graphic execution plan in SQL Server Management Studio. A missing index request appears at the top of the missing index request in green font, directly below the Transact-SQL statement.":::
    
    A single execution plan may contain multiple missing index requests, but only one missing index request can be displayed in the graphic execution plan. One option to view a full list of missing indexes for an execution plan is to view the execution plan XML.

1. Right-click on the execution plan and select **Show Execution Plan XML...** from the menu.
    
    :::image type="content" source="media/missing-index-graphic-execution-plan-show-xml.png" alt-text="Screenshot showing the menu that appears after right clicking on an execution plan.":::
    
    The execution plan XML will open as a new tab inside SSMS.

    > [!NOTE]
    > Only a single missing index suggestion will be shown in the **Missing Index Details...** menu option, even if multiple suggestions are present in the execution plan XML. The missing index suggestion displayed may not be the one with the highest estimated improvement for the query.
    
1. Display the **Find** dialog by using the **CTRL+f** shortcut.
1. Search for `MissingIndex`.

    :::image type="content" source="media/missing-index-node-in-execution-plan-xml.png" alt-text="Screenshot of the XML for an execution plan. The Find dialog has been opened, and the term MissingIndex has been searched for in the document."  lightbox="media/missing-index-node-in-execution-plan-xml.png":::

    In this example, there are two `MissingIndex` elements.

    - The first missing index suggests the query might use an index on the `Person.Address` table that supports an equality search on the `StateProvinceID` column, which includes two more columns, `City` and `PostalCode`'. At the time of optimization, the query optimizer believed that this index might reduce the [estimated cost](../query-processing-architecture-guide.md#optimizing-select-statements) of the query by 34.2737%. 
    - The second missing index suggests the query might use an index on the `Person.Person` table that supports an inequality search on the FirstName column.  At the time of optimization, the query optimizer believed that this index might reduce the [estimated cost](../query-processing-architecture-guide.md#optimizing-select-statements) of the query by 18.1102%.

Each disk-based nonclustered index in your database takes up space, adds overhead for inserts, updates, and deletes, and may require maintenance. For these reasons, it's a best practice to review all the missing index requests for a table and the existing indexes on a table before adding an index based on a query execution plan.

### View missing index suggestions in DMVs

You can retrieve information about missing indexes by querying the dynamic management objects listed in the following table.

|Dynamic management view |Information returned |
|---------|---------|
|[sys.dm_db_missing_index_group_stats (Transact-SQL)](../system-dynamic-management-views/sys-dm-db-missing-index-group-stats-transact-sql.md) |Returns summary information about missing index groups, for example, the performance improvements that could be gained by implementing a specific group of missing indexes. |
|[sys.dm_db_missing_index_groups (Transact-SQL)](../system-dynamic-management-views/sys-dm-db-missing-index-groups-transact-sql.md)| Returns information about a specific group of missing indexes, such as the group identifier and the identifiers of all missing indexes that are contained in that group.|
|[sys.dm_db_missing_index_details (Transact-SQL)](../system-dynamic-management-views/sys-dm-db-missing-index-details-transact-sql.md) | Returns detailed information about a missing index; for example, it returns the name and identifier of the table where the index is missing, and the columns and column types that should make up the missing index. |
|[sys.dm_db_missing_index_columns (Transact-SQL)](../system-dynamic-management-views/sys-dm-db-missing-index-columns-transact-sql.md) |Returns information about the database table columns that are missing an index. |

The following query uses the missing index DMVs to generate CREATE INDEX statements. The index creation statements here are intended to assist you in crafting your own DDL after examining all of the requests for the table along with existing indexes on the table. 

```sql
SELECT TOP 20
    CONVERT (varchar(30), getdate(), 126) AS runtime,
    CONVERT (decimal (28, 1), 
        migs.avg_total_user_cost * migs.avg_user_impact * (migs.user_seeks + migs.user_scans) 
        ) AS estimated_improvement,
    'CREATE INDEX missing_index_' + 
        CONVERT (varchar, mig.index_group_handle) + '_' + 
        CONVERT (varchar, mid.index_handle) + ' ON ' + 
        mid.statement + ' (' + ISNULL (mid.equality_columns, '') + 
        CASE
            WHEN mid.equality_columns IS NOT NULL
            AND mid.inequality_columns IS NOT NULL THEN ','
            ELSE ''
        END + ISNULL (mid.inequality_columns, '') + ')' + 
        ISNULL (' INCLUDE (' + mid.included_columns + ')', '') AS create_index_statement
FROM sys.dm_db_missing_index_groups mig
JOIN sys.dm_db_missing_index_group_stats migs ON 
    migs.group_handle = mig.index_group_handle
JOIN sys.dm_db_missing_index_details mid ON 
    mig.index_handle = mid.index_handle
ORDER BY estimated_improvement DESC;
GO
```

This query orders the suggestions by a column named `estimated_improvement`. The estimated improvement is based on a combination of:

- The estimated query cost of queries associated with the missing index request.
- The estimated impact of adding the index. This is an estimate of how much the nonclustered index would reduce the query cost.
- The sum of executions of query operators (seeks and scans) that have been run for queries associated with the missing index request. As we discuss in [persist missing indexes with Query Store](#persist-missing-indexes-with-query-store), this information is periodically cleared.

> [!NOTE]
> The [Index-Creation](https://github.com/microsoft/tigertoolbox/tree/master/Index-Creation) script in Microsoft's [Tiger Toolbox](https://github.com/microsoft/tigertoolbox) examines missing index DMVs and automatically removes any redundant suggested indexes, parses out low impact indexes, and generates index creation scripts for your review. As in the query above, it does NOT execute index creation commands. The [Index-Creation](https://github.com/microsoft/tigertoolbox/tree/master/Index-Creation) script is suitable for SQL Server and  Azure SQL Managed Instance. For Azure SQL Database, consider implementing [automatic index tuning](/azure/azure-sql/database/automatic-tuning-overview).

Review [Limitations of the missing index feature](#limitations-of-the-missing-index-feature) and how to [apply missing index suggestions](#apply-missing-index-suggestions) before creating indexes, and modify the index name to match the naming convention for your database.

## Persist missing indexes with Query Store

Missing index suggestions in DMVs are cleared by events such as instance restarts, failovers, and setting a database offline. Additionally, when the metadata for a table changes, all missing index information about that table is deleted from these dynamic management objects. Table metadata changes can occur when columns are added or dropped from a table, for example, or when an index is created on a column of a table. Performing an [ALTER INDEX REBUILD](../../t-sql/statements/alter-index-transact-sql.md) operation on an index on a table also clears missing index requests for that table.

Similarly, execution plans stored in the plan cache are cleared by events such as instance restarts, failovers, and setting a database offline. Execution plans may be removed from cache due to memory pressure and recompilations.

Missing index suggestions in execution plans can be persisted across these events by enabling [Query Store](../performance/monitoring-performance-by-using-the-query-store.md). 

The following query retrieves the top 20 query plans containing missing index requests from query store based on a rough estimate of total logical reads for the query. The data is limited to query executions within the past 48 hours. 

```sql
SELECT TOP 20
	qsq.query_id,
    SUM(qrs.count_executions) * AVG(qrs.avg_logical_io_reads) as est_logical_reads,
    SUM(qrs.count_executions) AS sum_executions,
    AVG(qrs.avg_logical_io_reads) AS avg_avg_logical_io_reads,
    SUM(qsq.count_compiles) AS sum_compiles,
    (SELECT TOP 1 qsqt.query_sql_text FROM sys.query_store_query_text qsqt
        WHERE qsqt.query_text_id = MAX(qsq.query_text_id)) AS query_text,    
    TRY_CONVERT(XML, (SELECT TOP 1 qsp2.query_plan from sys.query_store_plan qsp2
        WHERE qsp2.query_id=qsq.query_id
        ORDER BY qsp2.plan_id DESC)) AS query_plan
FROM sys.query_store_query qsq
JOIN sys.query_store_plan qsp on qsq.query_id=qsp.query_id
CROSS APPLY (SELECT TRY_CONVERT(XML, qsp.query_plan) AS query_plan_xml) AS qpx
JOIN sys.query_store_runtime_stats qrs on 
    qsp.plan_id = qrs.plan_id
JOIN sys.query_store_runtime_stats_interval qsrsi on 
    qrs.runtime_stats_interval_id=qsrsi.runtime_stats_interval_id
WHERE    
    qsp.query_plan like N'%<MissingIndexes>%'
    and qsrsi.start_time >= DATEADD(HH, -48, SYSDATETIME())
GROUP BY qsq.query_id, qsq.query_hash
ORDER BY est_logical_reads DESC;
GO
```

## Apply missing index suggestions

To effectively use missing index suggestions, follow [nonclustered index design guidelines](../sql-server-index-design-guide.md#Nonclustered). When tuning nonclustered indexes with missing index suggestions, review the base table structure, carefully combine indexes, consider key column order, and review included column suggestions.

### Review the base table structure

Before creating nonclustered indexes on a table based on missing index suggestions, review the table's [clustered index](../sql-server-index-design-guide.md#Clustered). 

One way to check for a clustered index is by using the [sp_helpindex](../system-stored-procedures/sp-helpindex-transact-sql.md) system stored procedure. For example, we can view a summary of the indexes on the `Person.Address` table by executing the following statement:

```sql
exec sp_helpindex 'Person.Address';
GO
```
Review the `index_description` column. A table can have only one clustered index. If a clustered index has been implemented for the table, the `index_description` will contain the word 'clustered'. 

:::image type="content" source="media/sp-helpindex.png" alt-text="A screenshot of the sp_helpindex being run against the `Person.Address` table in the AdventureWorks database. The table returns four indexes. The fourth index has an index_description that shows that it's a clustered, unique primary key.":::

If no clustered index is present, the table is a [heap](heaps-tables-without-clustered-indexes.md). In this case, review if the table was intentionally created as a heap to solve a specific performance problem. Most tables benefit from clustered indexes: often, tables are implemented as heaps by accident. Consider implementing a clustered index based on the [clustered index design guidelines](../sql-server-index-design-guide.md#Clustered).

### Review missing indexes and existing indexes for overlap

Missing indexes may offer similar variations of nonclustered indexes on the same table and column(s) across queries. Missing indexes may also be similar to existing indexes on a table. For optimal performance, it is best to examine missing indexes and existing indexes for overlap and avoid creating duplicate indexes.

#### Script out existing indexes on a table

One way to examine the definition of existing indexes on a table is to script out the indexes with Object Explorer Details:

1. Connect **Object Explorer** to your instance or database.
1. Expand the node for the database in question in **Object Explorer**.
1. Expand the **Tables** folder.
1. Expand the table for which you would like to script out indexes.
1. Select the **Indexes** folder.
1. If the Object Explorer Details pane is not already open, on the **View** menu, select **Object Explorer Details** or press **F7**.
1. Select all indexes listed on the Object Explorer Details pane with the shortcut **CTRL+a**.
1. Right-click anywhere in the selected region and select the menu option **Script index as**, then **CREATE To** and **New Query Editor Window**.

:::image type="content" source="media/object-explorer-details-script-all-indexes.png" alt-text="A screenshot of scripting out all indexes on a table using the Object Explorer Details pane in SSMS."  lightbox="media/object-explorer-details-script-all-indexes.png":::

#### Review indexes and combine where possible

Review the missing index recommendations for a table as a group, along with the definitions of existing indexes on the table. Remember that when defining indexes, generally equality columns should be put before the inequality columns, and together they should form the key of the index. To determine an effective order for the equality columns, order them based on their selectivity: list the most selective columns first (leftmost in the column list). Unique columns are most selective, while columns with many repeating values are less selective.

Included columns should be added to the CREATE INDEX statement using the INCLUDE clause. The order of included columns doesn't affect query performance. Therefore, when combining indexes, included columns may be combined without worrying about order. Learn more in [included columns guidelines](../sql-server-index-design-guide.md#index-with-included-columns-guidelines).

For example, you may have a table, `Person.Address`, with an existing index on the key column `StateProvinceID`. You may see missing index recommendations for the `Person.Address` table for the following columns:

   - EQUALITY filters for `StateProvinceID` and `City`
   - EQUALITY filters for `StateProvinceID` and `City`,  INCLUDE `PostalCode`

Modifying the existing index to match the second recommendation, an index with keys on `StateProvinceID` and `City` including `PostalCode`, would likely satisfy the queries that generated both index suggestions.

Tradeoffs are common in index tuning. It is likely that for many datasets, the `City` column is more selective than the `StateProvinceID` column. However, if our existing index on `StateProvinceID` is heavily used, and other requests largely search on both `StateProvinceID` and `City`, it is lower overhead for the database in general to have a single index with both columns in the key, leading on `StateProvinceID`, although it is not the most selective column.

Indexes may be modified in multiple ways:

- You can use the [CREATE INDEX Statement with the DROP_EXISTING clause](../../t-sql/statements/create-index-transact-sql.md#drop_existing-clause). You may wish to [rename the indexes](rename-indexes.md) following the modification so that the name still accurately describes the index definition, depending on your naming convention.
- You can use the [DROP INDEX (Transact-SQL)](../../t-sql/statements/drop-index-transact-sql.md) statement followed by a [CREATE INDEX Statement](../../odbc/microsoft/create-index-statement.md).

The order of index keys matters when combining the index suggestions: `City` as a leading column is different from `StateProvinceID` as a leading column. Learn more in [nonclustered index design guidelines](../sql-server-index-design-guide.md#Nonclustered).

When creating indexes, consider using [online index operations](guidelines-for-online-index-operations.md) when they are available.

While indexes can dramatically improve query performance in some cases, indexes also have overhead and management costs. Review [general index design guidelines](../sql-server-index-design-guide.md#General_Design) to help assess the benefit of indexes before creating them.

## Verify if your index change is successful

It's important to confirm if your index changes have been successful: is the query optimizer using your indexes?

One way to validate your index changes is to use [Query Store](#persist-missing-indexes-with-query-store) to identify queries with missing index requests. Note the query_id for the queries. Use the Tracked Queries view in Query Store to check if execution plans have changed for a query and if the optimizer is using your new or modified index. Learn more about Tracked Queries in [start with query performance troubleshooting](../performance/best-practice-with-the-query-store.md#start-with-query-performance-troubleshooting).


## Next steps

Learn more about index and performance tuning in the following articles:

- [SQL Server and Azure SQL index architecture and design guide](../sql-server-index-design-guide.md)
- [sys.dm_db_missing_index_details (Transact-SQL)](../system-dynamic-management-views/sys-dm-db-missing-index-details-transact-sql.md)
- [Monitoring performance by using the Query Store](../performance/monitoring-performance-by-using-the-query-store.md)

