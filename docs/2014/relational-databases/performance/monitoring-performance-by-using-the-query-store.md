---
title: "Monitoring Performance By Using the Query Store | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
ms.assetid: e06344a4-22a5-4c67-b6c6-a7060deb5de6
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Monitoring Performance By Using the Query Store
  The query store feature provides DBAs with insight on query plan choice and performance. It simplifies performance troubleshooting by enabling you to quickly find performance differences caused by changes in query plans. The feature automatically captures a history of queries, plans, and runtime statistics, and retains these for your review. It separates data by time windows, allowing you to see database usage patterns and understand when query plan changes happened on the server. The query store can be configured by using the [ALTER DATABASE SET](/sql/t-sql/statements/alter-database-transact-sql-set-options) option.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)] ([Get it](http://azure.micosoft.com/documentation/articles/sql-database-preview-whats-new/?WT.mc_id=TSQL_GetItTag)).|  
  
> [!IMPORTANT]  
>  This is currently a preview feature. To use the Query Store you must acknowledge and agree that implementation of Query Store is subject to the preview terms in your license agreement (e.g. the Enterprise Agreement, Microsoft Azure Agreement, or Microsoft Online Subscription Agreement), as well as any applicable [Supplemental Terms of Use for Microsoft Azure Preview](http://azure.microsoft.com/en-us/support/legal/preview-supplemental-terms/).  
  
##  <a name="Enabling"></a> Enabling the Query Store  
 Query Store is not active for new databases by default.  
  
#### By Using the Query Store Page in Management Studio  
  
1.  In Object Explorer, right-click a database, and then click **Properties**. (Requires [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] 2016 version of [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)].)  
  
2.  In the **Database Properties** dialog box, select the **Query Store** page.  
  
3.  In the **Enable** box, select **True**.  
  
#### By Using Transact-SQL Statements  
  
1.  Use the `ALTER DATABASE` statement to enable the query store. For example:  
  
    ```  
    ALTER DATABASE AdventureWorks2012 SET QUERY_STORE = ON;  
    ```  
  
     For more syntax options related to the query store, see [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-set-options).  
  
> [!NOTE]  
>  You cannot enable the query store for the master database.  
  

  
##  <a name="About"></a> Information in the Query Store  
 Execution plans for any specific query in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] typically evolve over time due to a number of different reasons such as statistics changes, schema changes, creation/deletion of indexes, etc. The procedure cache (where cached query plans are stored) only stores the latest execution plan. Plans also get evicted from the plan cache due to memory pressure. As a result, query performance regressions caused by execution plan changes can be non-trivial and time consuming to resolve.  
  
 Since the query store retains multiple execution plans per query, it can enforce policies to direct the query processor to use a specific execution plan for a query. This is referred to as plan forcing. Plan forcing in Query Store is provided by using a mechanism similar to the [USE PLAN](/sql/t-sql/queries/hints-transact-sql-query) query hint, but it does not require any change in user applications. Plan forcing can resolve a query performance regression caused by a plan change in a very short period of time.  
  
 Common scenarios for using the Query Store feature are:  
  
-   Quickly find and fix a plan performance regression by forcing the previous query plan. Fix queries that have recently regressed in performance due to execution plan changes.  
  
-   Determine the number of times a query was executed in a given time window, assisting a DBA in troubleshooting performance resource problems.  
  
-   Identify top *n* queries (by execution time, memory consumption, etc.) in the past *x* hours.  
  
-   Audit the history of query plans for a given query.  
  
-   Analyze the resource (CPU, I/O, and Memory) usage patterns for a particular database.  
  
 The query store contains two stores; a **plan store** for persisting the execution plan information, and a **runtime stats store** for persisting the execution statistics information. The number of unique plans that can be stored for a query in the plan store is limited by the `max_plans_per_query` configuration option. To enhance performance, the information is written to the two stores asynchronously. To minimize space usage, the runtime execution statistics in the runtime stats store are aggregated over a fixed time window. The information in these stores is visible by querying the query store catalog views.  
  
 The following query returns information about queries and plans in the query store.  
  
```  
SELECT Txt.query_text_id, Txt.query_sql_text, Pl.plan_id, Qry.*  
FROM sys.query_store_plan AS Pl  
JOIN sys.query_store_query AS Qry  
    ON Pl.query_id = Qry.query_id  
JOIN sys.query_store_query_text AS Txt  
    ON Qry.query_text_id = Txt.query_text_id ;  
```  
  

  
## Using the Regressed Queries Feature  
 After enabling the query store, refresh the database portion of the Object Explorer pane to add the **Query Store** section.  
  
 ![QueryStore](../../database-engine/media/querystore.PNG "QueryStore")  
  
 Selecting **Regressed Queries**, opens the **Regressed Queries** pane in [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)]. The Regressed Queries pane shows you the queries, and plans in the query store. Drop down boxes at the top allow you to select queries based on various criteria. Select a plan to see the graphical query plan. Buttons are available to view the source query, force, and unforce a query plan, and refresh the display.  
  
 ![RegressedQueries](../../database-engine/media/regressedqueries.PNG "RegressedQueries")  
  
 To force a plan, select a query and plan, and then click **Force Plan.** You can only force plans that were saved by the query plan feature and are still retained in the query plan cache.  
  

  
##  <a name="Options"></a> Configuration Options  
 OPERATION_MODE  
 Can be READ_WRITE or READ_ONLY.  
  
 CLEANUP_POLICY  
 Configure the STALE_QUERY_THRESHOLD_DAYS argument to specify the number of days to retain data in the query store.  
  
 DATA_FLUSH_INTERVAL_SECONDS  
 Determines the frequency at which data written to the query store is persisted to disk. To optimize for performance, data collected by the query store is asynchronously written to the disk. The frequency at which this asynchronous transfer occurs is configured via DATA_FLUSH_INTERVAL_SECONDS.  
  
 MAX_SIZE_MB  
 Configures the maximum size of the query store. If the data in the query store hits the MAX_SIZE_MB limit, the query store automatically changes the state from read-write to read-only and stops collecting new data.  
  
 INTERVAL_LENGTH_MINUTES  
 Determines the time interval at which runtime execution statistics data is aggregated into the query store. To optimize for space usage, the runtime execution statistics in the Runtime Stats Store are aggregated over a fixed time window. This fixed time window is configured via INTERVAL_LENGTH_MINUTES.  
  
 Query the `sys.database_query_store_options` view to determine the current options of the query store.  
  
 For more information about setting options by using [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, see [Option Management](#OptionMgmt).  
  
 
  
##  <a name="Related"></a> Related Views, Functions, and Procedures  
 The Query Store can be viewed and managed through [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)] or by using the following views and procedures.  
  
-   [sys.fn_stmt_sql_handle_from_sql_stmt &#40;Transact-SQL&#41;](/sql/relational-databases/system-functions/sys-fn-stmt-sql-handle-from-sql-stmt-transact-sql)  
  
### Query Store Catalog Views  
 Seven catalog views present information about the Query Store.  
  
-   [sys.database_query_store_options &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-database-query-store-options-transact-sql)  
  
-   [sys.query_context_settings &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-query-context-settings-transact-sql)  
  
-   [sys.query_store_plan &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-query-store-plan-transact-sql)  
  
-   [sys.query_store_query &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-query-store-query-transact-sql)  
  
-   [sys.query_store_query_text &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-query-store-query-text-transact-sql)  
  
-   [sys.query_store_runtime_stats &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-query-store-runtime-stats-transact-sql)  
  
-   [sys.query_store_runtime_stats_interval &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-query-store-runtime-stats-interval-transact-sql)  
  
### Query Store Stored Procedures  
 Six stored procedures configure the Query Store.  
  
-   [sp_query_store_flush_db &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-query-store-flush-db-transact-sql)  
  
-   [sp_query_store_reset_exec_stats &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-query-store-reset-exec-stats-transact-sql)  
  
-   [sp_query_store_force_plan &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-query-store-force-plan-transact-sql)  
  
-   [sp_query_store_unforce_plan &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-query-store-unforce-plan-transact-sql)  
  
-   [sp_query_store_remove_plan &#40;Transct-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-query-store-remove-plan-transct-sql)  
  
-   [sp_query_store_remove_query &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-query-store-remove-query-transact-sql)  
  

  
##  <a name="Scenarios"></a> Key Usage Scenarios  
  
###  <a name="OptionMgmt"></a> Option Management  
 This section provides some guidelines on managing Query Store feature itself.  
  
 **Is Query Store currently active?**  
  
 Query Store stores its data inside the user database and that is why it has size limit (configured  with `MAX_STORAGE_SIZE_MB`). If data in Query Store hits that limit Query Store will automatically change state from read-write to read-only and stop collecting new data.  
  
 Execute the following script to determine if Query Store is currently active, and whether it is currently collects runtime stats or not.  
  
```  
DECLARE @x nvarchar(100) = CAST(newid() AS nvarchar(100));  
DECLARE @query nvarchar(max) =   
N'IF EXISTS  
(  
    SELECT *   
    FROM sys.query_store_query_text   
    WHERE query_sql_text LIKE ''%' + @x + N'%''  
)  
SELECT ''Query Store is active'' ;  
ELSE SELECT ''Query Store is NOT active''' ;  
EXEC sp_executesql @query;  
```  
  
 **Get Query Store options**  
  
 To find out detailed information about Query Store status, execute following in a user database.  
  
```  
SELECT * FROM sys.database_query_store_options;  
```  
  
 **Setting Query Store interval**  
  
 You can override interval for aggregating query runtime statistics (default is 60 minutes).  
  
```  
  
      USE master;  
GO  
  
ALTER DATABASE <database_name>   
SET QUERY_STORE (INTERVAL_LENGTH_MINUTES = 15);  
```  
  
 Note that arbitrary values are not allowed - you should use one of the following: 1, 5, 10, 15, 30 and 60.  
  
 New value for interval is exposed through `sys.database_query_store_options` view.  
  
 If the Query Store storage is full use the following statement to extend the storage.  
  
```  
ALTER DATABASE <database_name>   
SET QUERY_STORE (MAX_STORAGE_SIZE_MB = <new_size>);  
```  
  
 **Set all Query Store options**  
  
 You can set multiple Query Store options at once with a single ALTER DATABASE statement.  
  
```  
ALTER DATABASE <database name>   
SET QUERY_STORE (  
    OPERATION_MODE = READ_WRITE,  
    CLEANUP_POLICY =   
    (STALE_QUERY_THRESHOLD_DAYS = 30),  
    DATA_FLUSH_INTERVAL_SECONDS = 3000,  
    MAX_STORAGE_SIZE_MB = 500,  
    INTERVAL_LENGTH_MINUTES = 15  
);  
```  
  
 **Cleaning up the space**  
  
 Query Store internal tables are created in the PRIMARY filegroup during database creation and that configuration cannot be changed later. If you are running out of space you might want to clear older Query Store data by using the following statement.  
  
```  
ALTER DATABASE <db_name> SET QUERY_STORE CLEAR;  
```  
  
 Alternatively, you might want to clear up only ad-hoc query data, since it is less relevant for query optimizations and plan analysis but takes up just as much space.  
  
 **Delete ad-hoc queries** This deletes the queries that were only executed only once and that are more than 24 hours old.  
  
```  
DECLARE @id int  
DECLARE adhoc_queries_cursor CURSOR   
FOR   
SELECT q.query_id  
FROM sys.query_store_query_text AS qt  
JOIN sys.query_store_query AS q   
    ON q.query_text_id = qt.query_text_id  
JOIN sys.query_store_plan AS p   
    ON p.query_id = q.query_id  
JOIN sys.query_store_runtime_stats AS rs   
    ON rs.plan_id = p.plan_id  
GROUP BY q.query_id  
HAVING SUM(rs.count_executions) < 2   
AND MAX(rs.last_execution_time) < DATEADD (hour, -24, GETUTCDATE())  
ORDER BY q.query_id ;  
  
OPEN adhoc_queries_cursor ;  
FETCH NEXT FROM adhoc_queries_cursor INTO @id;  
WHILE @@fetch_status = 0  
    BEGIN   
        PRINT @id  
        EXEC sp_query_store_remove_query @id  
        FETCH NEXT FROM adhoc_queries_cursor INTO @id  
    END   
CLOSE adhoc_queries_cursor ;  
DEALLOCATE adhoc_queries_cursor;  
```  
  
 You can define your own procedure with different logic for clearing up the data that is no longer important for you.  
  
 The example above uses the `sp_query_store_remove_query` extended stored procedure for removing unnecessary data. You can also use two other procedures.  
  
-   `sp_query_store_reset_exec_stats` - clear runtime statistics for a given plan.  
  
-   `sp_query_store_remove_plan` - removes a single plan.  
  

  
###  <a name="Peformance"></a> Performance Auditing and Troubleshooting  
 Because Query Store keeps history of compilation and runtime metrics throughout query executions there are many different questions you can easily answer with regards to your workload.  
  
 **Last *n* queries executed on the database.**  
  
```  
SELECT TOP 10 qt.query_sql_text, q.query_id,   
    qt.query_text_id, p.plan_id, rs.last_execution_time  
FROM sys.query_store_query_text AS qt   
JOIN sys.query_store_query AS q   
    ON qt.query_text_id = q.query_text_id   
JOIN sys.query_store_plan AS p   
    ON q.query_id = p.query_id   
JOIN sys.query_store_runtime_stats AS rs   
    ON p.plan_id = rs.plan_id  
ORDER BY rs.last_execution_time DESC;  
```  
  
 **Number of executions for each query.**  
  
```  
SELECT q.query_id, qt.query_text_id, qt.query_sql_text,   
    SUM(rs.count_executions) AS total_execution_count  
FROM sys.query_store_query_text AS qt   
JOIN sys.query_store_query AS q   
    ON qt.query_text_id = q.query_text_id   
JOIN sys.query_store_plan AS p   
    ON q.query_id = p.query_id   
JOIN sys.query_store_runtime_stats AS rs   
    ON p.plan_id = rs.plan_id  
GROUP BY q.query_id, qt.query_text_id, qt.query_sql_text  
ORDER BY total_execution_count DESC;  
```  
  
 **The number of queries with the longest average execution time within last hour.**  
  
```  
SELECT TOP 10 rs.avg_duration, qt.query_sql_text, q.query_id,  
    qt.query_text_id, p.plan_id, GETUTCDATE() AS CurrentUTCTime,   
    rs.last_execution_time   
FROM sys.query_store_query_text AS qt   
JOIN sys.query_store_query AS q   
    ON qt.query_text_id = q.query_text_id   
JOIN sys.query_store_plan AS p   
    ON q.query_id = p.query_id   
JOIN sys.query_store_runtime_stats AS rs   
    ON p.plan_id = rs.plan_id  
WHERE rs.last_execution_time > DATEADD(hour, -1, GETUTCDATE())  
ORDER BY rs.avg_duration DESC;  
```  
  
 **The number of queries that had the biggest average physical IO reads in last 24 hours, with corresponding average row count and execution count.**  
  
```  
SELECT TOP 10 rs.avg_physical_io_reads, qt.query_sql_text,   
    q.query_id, qt.query_text_id, p.plan_id, rs.runtime_stats_id,   
    rsi.start_time, rsi.end_time, rs.avg_rowcount, rs.count_executions  
FROM sys.query_store_query_text AS qt   
JOIN sys.query_store_query AS q   
    ON qt.query_text_id = q.query_text_id   
JOIN sys.query_store_plan AS p   
    ON q.query_id = p.query_id   
JOIN sys.query_store_runtime_stats AS rs   
    ON p.plan_id = rs.plan_id   
JOIN sys.query_store_runtime_stats_interval AS rsi   
    ON rsi.runtime_stats_interval_id = rs.runtime_stats_interval_id  
WHERE rsi.start_time >= DATEADD(hour, -24, GETUTCDATE())   
ORDER BY rs.avg_physical_io_reads DESC;  
```  
  
 **Queries with multiple plans.** These queries are especially interesting because they are candidates for regressions due to plan choice change. The following query identifies these queries along with all plans:  
  
```  
WITH Query_MultPlans  
AS  
(  
    SELECT COUNT(*) AS cnt, q.query_id   
    FROM sys.query_store_query_text AS qt  
    JOIN sys.query_store_query AS q  
        ON qt.query_text_id = q.query_text_id  
    JOIN sys.query_store_plan AS p  
        ON p.query_id = q.query_id  
    GROUP BY q.query_id  
    HAVING COUNT(distinct plan_id) > 1  
)  
  
SELECT q.query_id, object_name(object_id) AS ContainingObject, query_sql_text,  
plan_id, p.query_plan AS plan_xml,  
p.last_compile_start_time, p.last_execution_time  
FROM Query_MultPlans AS qm  
JOIN sys.query_store_query AS q  
    ON qm.query_id = q.query_id  
JOIN sys.query_store_plan AS p  
    ON q.query_id = p.query_id  
JOIN sys.query_store_query_text qt   
    ON qt.query_text_id = q.query_text_id  
ORDER BY query_id, plan_id;  
```  
  
 **Queries that recently regressed in performance (comparing different point in time).** The following query example returns all queries for which execution time doubled in last 48 hours due to a plan choice change. Query compares all runtime stat intervals side by side.  
  
```  
SELECT   
    qt.query_sql_text,   
    q.query_id,   
    qt.query_text_id,   
    rs1.runtime_stats_id AS runtime_stats_id_1,  
    rsi1.start_time AS interval_1,   
    p1.plan_id AS plan_1,   
    rs1.avg_duration AS avg_duration_1,   
    rs2.avg_duration AS avg_duration_2,  
    p2.plan_id AS plan_2,   
    rsi2.start_time AS interval_2,   
    rs2.runtime_stats_id AS runtime_stats_id_2  
FROM sys.query_store_query_text AS qt   
JOIN sys.query_store_query AS q   
    ON qt.query_text_id = q.query_text_id   
JOIN sys.query_store_plan AS p1   
    ON q.query_id = p1.query_id   
JOIN sys.query_store_runtime_stats AS rs1   
    ON p1.plan_id = rs1.plan_id   
JOIN sys.query_store_runtime_stats_interval AS rsi1   
    ON rsi1.runtime_stats_interval_id = rs1.runtime_stats_interval_id   
JOIN sys.query_store_plan AS p2   
    ON q.query_id = p2.query_id   
JOIN sys.query_store_runtime_stats AS rs2   
    ON p2.plan_id = rs2.plan_id   
JOIN sys.query_store_runtime_stats_interval AS rsi2   
    ON rsi2.runtime_stats_interval_id = rs2.runtime_stats_interval_id  
WHERE rsi1.start_time > DATEADD(hour, -48, GETUTCDATE())   
    AND rsi2.start_time > rsi1.start_time   
    AND p1.plan_id <> p2.plan_id  
    AND rs2.avg_duration > 2*rs1.avg_duration  
ORDER BY q.query_id, rsi1.start_time, rsi2.start_time;  
```  
  
 If you want to see performance all regressions (not only those related to plan choice change) than just remove condition `AND p1.plan_id <> p2.plan_id` from the previous query.  
  
 **Queries that recently regressed in performance (comparing recent vs. history execution).** The next query compares query execution based periods of execution. In this particular example the query compares execution in recent period (1 hour) vs. history period (last day) and identifies those that introduced additional_duration_workload. This metrics is calculated as a difference between recent average execution and history average execution multiplied by the number of recent executions. It actually represents how much of additional duration recent executions introduced compared to history:  
  
```  
--- "Recent" workload - last 1 hour  
DECLARE @recent_start_time datetimeoffset;  
DECLARE @recent_end_time datetimeoffset;  
SET @recent_start_time = DATEADD(hour, -1, SYSUTCDATETIME());  
SET @recent_end_time = SYSUTCDATETIME();  
  
--- "History" workload  
DECLARE @history_start_time datetimeoffset;  
DECLARE @history_end_time datetimeoffset;  
SET @history_start_time = DATEADD(hour, -24, SYSUTCDATETIME());  
SET @history_end_time = SYSUTCDATETIME();  
  
WITH  
hist AS  
(  
    SELECT   
        p.query_id query_id,   
        CONVERT(float, SUM(rs.avg_duration*rs.count_executions)) total_duration,   
        SUM(rs.count_executions) count_executions,  
        COUNT(distinct p.plan_id) num_plans   
     FROM sys.query_store_runtime_stats AS rs  
        JOIN sys.query_store_plan p ON p.plan_id = rs.plan_id  
    WHERE  (rs.first_execution_time >= @history_start_time AND rs.last_execution_time < @history_end_time)  
        OR (rs.first_execution_time <= @history_start_time AND rs.last_execution_time > @history_start_time)  
        OR (rs.first_execution_time <= @history_end_time AND rs.last_execution_time > @history_end_time)  
    GROUP BY p.query_id  
),  
recent AS  
(  
    SELECT   
        p.query_id query_id,   
        CONVERT(float, SUM(rs.avg_duration*rs.count_executions)) total_duration,   
        SUM(rs.count_executions) count_executions,  
        COUNT(distinct p.plan_id) num_plans   
    FROM sys.query_store_runtime_stats AS rs  
        JOIN sys.query_store_plan p ON p.plan_id = rs.plan_id  
    WHERE  (rs.first_execution_time >= @recent_start_time AND rs.last_execution_time < @recent_end_time)  
        OR (rs.first_execution_time <= @recent_start_time AND rs.last_execution_time > @recent_start_time)  
        OR (rs.first_execution_time <= @recent_end_time AND rs.last_execution_time > @recent_end_time)  
    GROUP BY p.query_id  
)  
SELECT   
    results.query_id query_id,  
    results.query_text query_text,  
    results.additional_duration_workload additional_duration_workload,  
    results.total_duration_recent total_duration_recent,  
    results.total_duration_hist total_duration_hist,  
    ISNULL(results.count_executions_recent, 0) count_executions_recent,  
    ISNULL(results.count_executions_hist, 0) count_executions_hist   
FROM  
(  
    SELECT  
        hist.query_id query_id,  
        qt.query_sql_text query_text,  
        ROUND(CONVERT(float, recent.total_duration/recent.count_executions-hist.total_duration/hist.count_executions)*(recent.count_executions), 2) AS additional_duration_workload,  
        ROUND(recent.total_duration, 2) total_duration_recent,   
        ROUND(hist.total_duration, 2) total_duration_hist,  
        recent.count_executions count_executions_recent,  
        hist.count_executions count_executions_hist     
    FROM hist   
        JOIN recent   
            ON hist.query_id = recent.query_id   
        JOIN sys.query_store_query AS q   
            ON q.query_id = hist.query_id  
        JOIN sys.query_store_query_text AS qt   
            ON q.query_text_id = qt.query_text_id      
) AS results  
WHERE additional_duration_workload > 0  
ORDER BY additional_duration_workload DESC  
OPTION (MERGE JOIN);  
```  
  

  
###  <a name="Stability"></a> Maintaining Query Performance Stability  
 For queries that are executed multiple times you may notice that [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] used different plans which resulted in different resource utilization and duration. With Query Store you can easily detect when the query performance regressed and determine the optimal plan within a period of interest. Then you can force that optimal plan for future query execution.  
  
 You can also identify inconsistent query performance for a query with parameters (either auto- parameterized or manually parameterized). Among different plans you can identify plan which is fast and optimal enough for all or most of the parameter values and force that plan; keeping predictable performance for the wider set of user scenarios.  
  
 **Force or a plan for a query (apply forcing policy).** When a plan is forced for a certain query, every time a query comes to execution it will be executed with the plan that is forced.  
  
```  
EXEC sp_query_store_force_plan @query_id = 48, @plan_id = 49;  
```  
  
 When using `sp_query_store_force_plan` you can only force plans that were recorded by Query Store as a plan for that query. In other words, the only plans available for a query are those that were already used to execute Q1 while Query Store was active.  
  
 **Remove plan forcing for a query.** To rely again on the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] query optimizer to calculate the optimal query plan, use `sp_query_store_unforce_plan` to unforce the plan that was selected for the query.  
  
```  
EXEC sp_query_store_unforce_plan @query_id = 48, @plan_id = 49;  
```  
  

  
## See Also  
 [Monitor and Tune for Performance](../performance/monitor-and-tune-for-performance.md)   
 [Performance Monitoring and Tuning Tools](../performance/performance-monitoring-and-tuning-tools.md)   
 [Open Activity Monitor &#40;SQL Server Management Studio&#41;](../performance-monitor/open-activity-monitor-sql-server-management-studio.md)   
 [Activity Monitor](../performance-monitor/activity-monitor.md)  
  
  
