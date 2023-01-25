---
title: Monitor performance using DMVs
titleSuffix: Azure SQL Managed Instance
description: Learn how to detect and diagnose common performance problems by using dynamic management views to monitor Microsoft Azure SQL Managed Instance.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: wiassaf, mathoma
ms.date: 08/03/2022
ms.service: sql-managed-instance
ms.subservice: performance
ms.topic: how-to
ms.custom:
  - "azure-sql-split"
  - "sqldbrb=2"
monikerRange: "= azuresql || = azuresql-mi"
---
# Monitoring Microsoft Azure SQL Managed Instance performance using dynamic management views
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/monitoring-with-dmvs.md)
> * [Azure SQL Managed Instance](monitoring-with-dmvs.md)

Microsoft Azure SQL Managed Instance enables a subset of dynamic management views (DMVs) to diagnose performance problems, which might be caused by blocked or long-running queries, resource bottlenecks, poor query plans, and so on. This article provides information on how to detect common performance problems by using dynamic management views.

This article is about Azure SQL Managed Instance, see also [Monitoring Microsoft Azure SQL Database performance using dynamic management views](../database/monitoring-with-dmvs.md).

## Permissions

In Azure SQL Managed Instance, querying a dynamic management view requires **VIEW SERVER STATE** permissions. 

```sql
GRANT VIEW SERVER STATE TO database_user;
```

In an instance of SQL Server and in Azure SQL Managed Instance, dynamic management views return server state information. 

## Identify CPU performance issues

If CPU consumption is above 80% for extended periods of time, consider the following troubleshooting steps:

### The CPU issue is occurring now

If issue is occurring right now, there are two possible scenarios:

#### Many individual queries that cumulatively consume high CPU

Use the following query to identify top query hashes:

```sql
PRINT '-- top 10 Active CPU Consuming Queries (aggregated)--';
SELECT TOP 10 GETDATE() runtime, *
FROM (SELECT query_stats.query_hash, SUM(query_stats.cpu_time) 'Total_Request_Cpu_Time_Ms', SUM(logical_reads) 'Total_Request_Logical_Reads', MIN(start_time) 'Earliest_Request_start_Time', COUNT(*) 'Number_Of_Requests', SUBSTRING(REPLACE(REPLACE(MIN(query_stats.statement_text), CHAR(10), ' '), CHAR(13), ' '), 1, 256) AS "Statement_Text"
    FROM (SELECT req.*, SUBSTRING(ST.text, (req.statement_start_offset / 2)+1, ((CASE statement_end_offset WHEN -1 THEN DATALENGTH(ST.text)ELSE req.statement_end_offset END-req.statement_start_offset)/ 2)+1) AS statement_text
          FROM sys.dm_exec_requests AS req
                CROSS APPLY sys.dm_exec_sql_text(req.sql_handle) AS ST ) AS query_stats
    GROUP BY query_hash) AS t
ORDER BY Total_Request_Cpu_Time_Ms DESC;
```

#### Long running queries that consume CPU are still running

Use the following query to identify these queries:

```sql
PRINT '--top 10 Active CPU Consuming Queries by sessions--';
SELECT TOP 10 req.session_id, req.start_time, cpu_time 'cpu_time_ms', OBJECT_NAME(ST.objectid, ST.dbid) 'ObjectName', SUBSTRING(REPLACE(REPLACE(SUBSTRING(ST.text, (req.statement_start_offset / 2)+1, ((CASE statement_end_offset WHEN -1 THEN DATALENGTH(ST.text)ELSE req.statement_end_offset END-req.statement_start_offset)/ 2)+1), CHAR(10), ' '), CHAR(13), ' '), 1, 512) AS statement_text
FROM sys.dm_exec_requests AS req
    CROSS APPLY sys.dm_exec_sql_text(req.sql_handle) AS ST
ORDER BY cpu_time DESC;
GO
```

### The CPU issue occurred in the past

If the issue occurred in the past and you want to do a root cause analysis, use [Query Store](/sql/relational-databases/performance/monitoring-performance-by-using-the-query-store). Users with database access can use T-SQL to query Query Store data. Query Store default configurations use a granularity of 1 hour. Use the following query to look at activity for high CPU consuming queries. This query returns the top 15 CPU consuming queries. Remember to change `rsi.start_time >= DATEADD(hour, -2, GETUTCDATE()`:

```sql
-- Top 15 CPU consuming queries by query hash
-- note that a query  hash can have many query id if not parameterized or not parameterized properly
-- it grabs a sample query text by min
WITH AggregatedCPU AS (SELECT q.query_hash, SUM(count_executions * avg_cpu_time / 1000.0) AS total_cpu_millisec, SUM(count_executions * avg_cpu_time / 1000.0)/ SUM(count_executions) AS avg_cpu_millisec, MAX(rs.max_cpu_time / 1000.00) AS max_cpu_millisec, MAX(max_logical_io_reads) max_logical_reads, COUNT(DISTINCT p.plan_id) AS number_of_distinct_plans, COUNT(DISTINCT p.query_id) AS number_of_distinct_query_ids, SUM(CASE WHEN rs.execution_type_desc='Aborted' THEN count_executions ELSE 0 END) AS Aborted_Execution_Count, SUM(CASE WHEN rs.execution_type_desc='Regular' THEN count_executions ELSE 0 END) AS Regular_Execution_Count, SUM(CASE WHEN rs.execution_type_desc='Exception' THEN count_executions ELSE 0 END) AS Exception_Execution_Count, SUM(count_executions) AS total_executions, MIN(qt.query_sql_text) AS sampled_query_text
                       FROM sys.query_store_query_text AS qt
                            JOIN sys.query_store_query AS q ON qt.query_text_id=q.query_text_id
                            JOIN sys.query_store_plan AS p ON q.query_id=p.query_id
                            JOIN sys.query_store_runtime_stats AS rs ON rs.plan_id=p.plan_id
                            JOIN sys.query_store_runtime_stats_interval AS rsi ON rsi.runtime_stats_interval_id=rs.runtime_stats_interval_id
                       WHERE rs.execution_type_desc IN ('Regular', 'Aborted', 'Exception')AND rsi.start_time>=DATEADD(HOUR, -2, GETUTCDATE())
                       GROUP BY q.query_hash), OrderedCPU AS (SELECT query_hash, total_cpu_millisec, avg_cpu_millisec, max_cpu_millisec, max_logical_reads, number_of_distinct_plans, number_of_distinct_query_ids, total_executions, Aborted_Execution_Count, Regular_Execution_Count, Exception_Execution_Count, sampled_query_text, ROW_NUMBER() OVER (ORDER BY total_cpu_millisec DESC, query_hash ASC) AS RN
                                                              FROM AggregatedCPU)
SELECT OD.query_hash, OD.total_cpu_millisec, OD.avg_cpu_millisec, OD.max_cpu_millisec, OD.max_logical_reads, OD.number_of_distinct_plans, OD.number_of_distinct_query_ids, OD.total_executions, OD.Aborted_Execution_Count, OD.Regular_Execution_Count, OD.Exception_Execution_Count, OD.sampled_query_text, OD.RN
FROM OrderedCPU AS OD
WHERE OD.RN<=15
ORDER BY total_cpu_millisec DESC;
```

Once you identify the problematic queries, it's time to tune those queries to reduce CPU utilization.  If you don't have time to tune the queries, you may also choose to upgrade the SLO of the managed instance to work around the issue.

## Identify IO performance issues

When identifying IO performance issues, the top wait types associated with IO issues are:

- `PAGEIOLATCH_*`

  For data file IO issues (including `PAGEIOLATCH_SH`, `PAGEIOLATCH_EX`, `PAGEIOLATCH_UP`).  If the wait type name has **IO** in it, it points to an IO issue. If there is no **IO** in the page latch wait name, it points to a different type of problem (for example, `tempdb` contention).

- `WRITE_LOG`

  For transaction log IO issues.

### If the IO issue is occurring right now

Use the [sys.dm_exec_requests](/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql) or [sys.dm_os_waiting_tasks](/sql/relational-databases/system-dynamic-management-views/sys-dm-os-waiting-tasks-transact-sql) to see the `wait_type` and `wait_time`.

#### View buffer-related IO using the Query Store

For option 2, you can use the following query against Query Store for buffer-related IO to view the last two hours of tracked activity:

```sql
-- top queries that waited on buffer
-- note these are finished queries
WITH Aggregated AS (SELECT q.query_hash, SUM(total_query_wait_time_ms) total_wait_time_ms, SUM(total_query_wait_time_ms / avg_query_wait_time_ms) AS total_executions, MIN(qt.query_sql_text) AS sampled_query_text, MIN(wait_category_desc) AS wait_category_desc
                    FROM sys.query_store_query_text AS qt
                         JOIN sys.query_store_query AS q ON qt.query_text_id=q.query_text_id
                         JOIN sys.query_store_plan AS p ON q.query_id=p.query_id
                         JOIN sys.query_store_wait_stats AS waits ON waits.plan_id=p.plan_id
                         JOIN sys.query_store_runtime_stats_interval AS rsi ON rsi.runtime_stats_interval_id=waits.runtime_stats_interval_id
                    WHERE wait_category_desc='Buffer IO' AND rsi.start_time>=DATEADD(HOUR, -2, GETUTCDATE())
                    GROUP BY q.query_hash), Ordered AS (SELECT query_hash, total_executions, total_wait_time_ms, sampled_query_text, wait_category_desc, ROW_NUMBER() OVER (ORDER BY total_wait_time_ms DESC, query_hash ASC) AS RN
                                                        FROM Aggregated)
SELECT OD.query_hash, OD.total_executions, OD.total_wait_time_ms, OD.sampled_query_text, OD.wait_category_desc, OD.RN
FROM Ordered AS OD
WHERE OD.RN<=15
ORDER BY total_wait_time_ms DESC;
GO
```

#### View total log IO for WRITELOG waits

If the wait type is `WRITELOG`, use the following query to view total log IO by statement:

```sql
-- Top transaction log consumers
-- Adjust the time window by changing
-- rsi.start_time >= DATEADD(hour, -2, GETUTCDATE())
WITH AggregatedLogUsed
AS (SELECT q.query_hash,
           SUM(count_executions * avg_cpu_time / 1000.0) AS total_cpu_millisec,
           SUM(count_executions * avg_cpu_time / 1000.0) / SUM(count_executions) AS avg_cpu_millisec,
           SUM(count_executions * avg_log_bytes_used) AS total_log_bytes_used,
           MAX(rs.max_cpu_time / 1000.00) AS max_cpu_millisec,
           MAX(max_logical_io_reads) max_logical_reads,
           COUNT(DISTINCT p.plan_id) AS number_of_distinct_plans,
           COUNT(DISTINCT p.query_id) AS number_of_distinct_query_ids,
           SUM(   CASE
                      WHEN rs.execution_type_desc = 'Aborted' THEN
                          count_executions
                      ELSE
                          0
                  END
              ) AS Aborted_Execution_Count,
           SUM(   CASE
                      WHEN rs.execution_type_desc = 'Regular' THEN
                          count_executions
                      ELSE
                          0
                  END
              ) AS Regular_Execution_Count,
           SUM(   CASE
                      WHEN rs.execution_type_desc = 'Exception' THEN
                          count_executions
                      ELSE
                          0
                  END
              ) AS Exception_Execution_Count,
           SUM(count_executions) AS total_executions,
           MIN(qt.query_sql_text) AS sampled_query_text
    FROM sys.query_store_query_text AS qt
        JOIN sys.query_store_query AS q
            ON qt.query_text_id = q.query_text_id
        JOIN sys.query_store_plan AS p
            ON q.query_id = p.query_id
        JOIN sys.query_store_runtime_stats AS rs
            ON rs.plan_id = p.plan_id
        JOIN sys.query_store_runtime_stats_interval AS rsi
            ON rsi.runtime_stats_interval_id = rs.runtime_stats_interval_id
    WHERE rs.execution_type_desc IN ( 'Regular', 'Aborted', 'Exception' )
          AND rsi.start_time >= DATEADD(HOUR, -2, GETUTCDATE())
    GROUP BY q.query_hash),
     OrderedLogUsed
AS (SELECT query_hash,
           total_log_bytes_used,
           number_of_distinct_plans,
           number_of_distinct_query_ids,
           total_executions,
           Aborted_Execution_Count,
           Regular_Execution_Count,
           Exception_Execution_Count,
           sampled_query_text,
           ROW_NUMBER() OVER (ORDER BY total_log_bytes_used DESC, query_hash ASC) AS RN
    FROM AggregatedLogUsed)
SELECT OD.total_log_bytes_used,
       OD.number_of_distinct_plans,
       OD.number_of_distinct_query_ids,
       OD.total_executions,
       OD.Aborted_Execution_Count,
       OD.Regular_Execution_Count,
       OD.Exception_Execution_Count,
       OD.sampled_query_text,
       OD.RN
FROM OrderedLogUsed AS OD
WHERE OD.RN <= 15
ORDER BY total_log_bytes_used DESC;
GO
```

## Identify `tempdb` performance issues

When identifying IO performance issues, the top wait types associated with `tempdb` issues is `PAGELATCH_*` (not `PAGEIOLATCH_*`). However, `PAGELATCH_*` waits do not always mean you have `tempdb` contention.  This wait may also mean that you have user-object data page contention due to concurrent requests targeting the same data page. To further confirm `tempdb` contention, use [sys.dm_exec_requests](/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql) to confirm that the wait_resource value begins with `2:x:y` where 2 is `tempdb` is the database ID, `x` is the file ID, and `y` is the page ID.  

For `tempdb` contention, a common method is to reduce or rewrite application code that relies on `tempdb`.  Common `tempdb` usage areas include:

- Temp tables
- Table variables
- Table-valued parameters
- Version store usage (associated with long running transactions)
- Queries that have query plans that use sorts, hash joins, and spools

### Top queries that use table variables and temporary tables

Use the following query to identify top queries that use table variables and temporary tables:

```sql
SELECT plan_handle, execution_count, query_plan
INTO #tmpPlan
FROM sys.dm_exec_query_stats
     CROSS APPLY sys.dm_exec_query_plan(plan_handle);
GO

WITH XMLNAMESPACES('http://schemas.microsoft.com/sqlserver/2004/07/showplan' AS sp)
SELECT plan_handle, stmt.stmt_details.value('@Database', 'varchar(max)') 'Database', stmt.stmt_details.value('@Schema', 'varchar(max)') 'Schema', stmt.stmt_details.value('@Table', 'varchar(max)') 'table'
INTO #tmp2
FROM(SELECT CAST(query_plan AS XML) sqlplan, plan_handle FROM #tmpPlan) AS p
    CROSS APPLY sqlplan.nodes('//sp:Object') AS stmt(stmt_details);
GO

SELECT t.plan_handle, [Database], [Schema], [table], execution_count
FROM(SELECT DISTINCT plan_handle, [Database], [Schema], [table]
     FROM #tmp2
     WHERE [table] LIKE '%@%' OR [table] LIKE '%#%') AS t
    JOIN #tmpPlan AS t2 ON t.plan_handle=t2.plan_handle;
```

### Identify long running transactions

Use the following query to identify long running transactions. Long running transactions prevent version store cleanup.

```sql
SELECT DB_NAME(dtr.database_id) 'database_name',
       sess.session_id,
       atr.name AS 'tran_name',
       atr.transaction_id,
       transaction_type,
       transaction_begin_time,
       database_transaction_begin_time, 
       transaction_state,
       is_user_transaction,
       sess.open_transaction_count,
       TRIM(REPLACE(
                REPLACE(
                            SUBSTRING(
                                        SUBSTRING(
                                                    txt.text,
                                                    (req.statement_start_offset / 2) + 1,
                                                    ((CASE req.statement_end_offset
                                                            WHEN -1 THEN
                                                                DATALENGTH(txt.text)
                                                            ELSE
                                                                req.statement_end_offset
                                                        END - req.statement_start_offset
                                                    ) / 2
                                                    ) + 1
                                                ),
                                        1,
                                        1000
                                    ),
                            CHAR(10),
                            ' '
                        ),
                CHAR(13),
                ' '
            )
            ) Running_stmt_text,
       recenttxt.text 'MostRecentSQLText'
FROM sys.dm_tran_active_transactions AS atr
    INNER JOIN sys.dm_tran_database_transactions AS dtr
        ON dtr.transaction_id = atr.transaction_id
    LEFT JOIN sys.dm_tran_session_transactions AS sess
        ON sess.transaction_id = atr.transaction_id
    LEFT JOIN sys.dm_exec_requests AS req
        ON req.session_id = sess.session_id
           AND req.transaction_id = sess.transaction_id
    LEFT JOIN sys.dm_exec_connections AS conn
        ON sess.session_id = conn.session_id
    OUTER APPLY sys.dm_exec_sql_text(req.sql_handle) AS txt
    OUTER APPLY sys.dm_exec_sql_text(conn.most_recent_sql_handle) AS recenttxt
WHERE atr.transaction_type != 2
      AND sess.session_id != @@spid
ORDER BY start_time ASC;
```

## Identify memory grant wait performance issues

If your top wait type is `RESOURCE_SEMAHPORE` and you don't have a high CPU usage issue, you may have a memory grant waiting issue.

### Determine if a `RESOURCE_SEMAHPORE` wait is a top wait

Use the following query to determine if a `RESOURCE_SEMAHPORE` wait is a top wait

```sql
SELECT wait_type,
       SUM(wait_time) AS total_wait_time_ms
FROM sys.dm_exec_requests AS req
    JOIN sys.dm_exec_sessions AS sess
        ON req.session_id = sess.session_id
WHERE is_user_process = 1
GROUP BY wait_type
ORDER BY SUM(wait_time) DESC;
```

### Identify high memory-consuming statements

If you encounter out of memory errors, review [sys.dm_os_out_of_memory_events](/sql/relational-databases/system-dynamic-management-views/sys-dm-os-out-of-memory-events).

Use the following query to identify high memory-consuming statements:

```sql
SELECT IDENTITY(INT, 1, 1) rowId,
    CAST(query_plan AS XML) query_plan,
    p.query_id
INTO #tmp
FROM sys.query_store_plan AS p
    JOIN sys.query_store_runtime_stats AS r
        ON p.plan_id = r.plan_id
    JOIN sys.query_store_runtime_stats_interval AS i
        ON r.runtime_stats_interval_id = i.runtime_stats_interval_id
WHERE start_time > '2018-10-11 14:00:00.0000000'
      AND end_time < '2018-10-17 20:00:00.0000000';
GO
;WITH cte
AS (SELECT query_id,
        query_plan,
        m.c.value('@SerialDesiredMemory', 'INT') AS SerialDesiredMemory
    FROM #tmp AS t
        CROSS APPLY t.query_plan.nodes('//*:MemoryGrantInfo[@SerialDesiredMemory[. > 0]]') AS m(c) )
SELECT TOP 50
    cte.query_id,
    t.query_sql_text,
    cte.query_plan,
    CAST(SerialDesiredMemory / 1024. AS DECIMAL(10, 2)) SerialDesiredMemory_MB
FROM cte
    JOIN sys.query_store_query AS q
        ON cte.query_id = q.query_id
    JOIN sys.query_store_query_text AS t
        ON q.query_text_id = t.query_text_id
ORDER BY SerialDesiredMemory DESC;
```

### Identify the memory grants

Use the following query to identify the top 10 active memory grants:

```sql
SELECT TOP 10
    CONVERT(VARCHAR(30), GETDATE(), 121) AS runtime,
       r.session_id,
       r.blocking_session_id,
       r.cpu_time,
       r.total_elapsed_time,
       r.reads,
       r.writes,
       r.logical_reads,
       r.row_count,
       wait_time,
       wait_type,
       r.command,
       OBJECT_NAME(txt.objectid, txt.dbid) 'Object_Name',
       TRIM(REPLACE(
                REPLACE(
                            SUBSTRING(
                                        SUBSTRING(
                                                    text,
                                                    (r.statement_start_offset / 2) + 1,
                                                    ((CASE r.statement_end_offset
                                                            WHEN -1 THEN
                                                                DATALENGTH(text)
                                                            ELSE
                                                                r.statement_end_offset
                                                        END - r.statement_start_offset
                                                    ) / 2
                                                    ) + 1
                                                ),
                                        1,
                                        1000
                                    ),
                            CHAR(10),
                            ' '
                        ),
                CHAR(13),
                ' '
            )
            ) stmt_text,
       mg.dop,                                               --Degree of parallelism
       mg.request_time,                                      --Date and time when this query requested the memory grant.
       mg.grant_time,                                        --NULL means memory has not been granted
       mg.requested_memory_kb / 1024.0 requested_memory_mb,  --Total requested amount of memory in megabytes
       mg.granted_memory_kb / 1024.0 AS granted_memory_mb,   --Total amount of memory actually granted in megabytes. NULL if not granted
       mg.required_memory_kb / 1024.0 AS required_memory_mb, --Minimum memory required to run this query in megabytes.
       max_used_memory_kb / 1024.0 AS max_used_memory_mb,
       mg.query_cost,                                        --Estimated query cost.
       mg.timeout_sec,                                       --Time-out in seconds before this query gives up the memory grant request.
       mg.resource_semaphore_id,                             --Non-unique ID of the resource semaphore on which this query is waiting.
       mg.wait_time_ms,                                      --Wait time in milliseconds. NULL if the memory is already granted.
       CASE mg.is_next_candidate --Is this process the next candidate for a memory grant
           WHEN 1 THEN
               'Yes'
           WHEN 0 THEN
               'No'
           ELSE
               'Memory has been granted'
       END AS 'Next Candidate for Memory Grant',
       qp.query_plan
FROM sys.dm_exec_requests AS r
    JOIN sys.dm_exec_query_memory_grants AS mg
        ON r.session_id = mg.session_id
           AND r.request_id = mg.request_id
    CROSS APPLY sys.dm_exec_sql_text(mg.sql_handle) AS txt
    CROSS APPLY sys.dm_exec_query_plan(r.plan_handle) AS qp
ORDER BY mg.granted_memory_kb DESC;
```

## Calculating database and objects sizes

The following query returns the size of your database (in megabytes):

```sql
-- Calculates the size of the database.
SELECT SUM(CAST(FILEPROPERTY(name, 'SpaceUsed') AS bigint) * 8192.) / 1024 / 1024 AS DatabaseSizeInMB
FROM sys.database_files
WHERE type_desc = 'ROWS';
GO
```

The following query returns the size of individual objects (in megabytes) in your database:

```sql
-- Calculates the size of individual database objects.
SELECT sys.objects.name, SUM(reserved_page_count) * 8.0 / 1024
FROM sys.dm_db_partition_stats, sys.objects
WHERE sys.dm_db_partition_stats.object_id = sys.objects.object_id
GROUP BY sys.objects.name;
GO
```

## Monitoring connections

You can use the [sys.dm_exec_connections](/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-connections-transact-sql) view to retrieve information about the connections established to a specific managed instance and the details of each connection. In addition, the [sys.dm_exec_sessions](/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-sessions-transact-sql) view is helpful when retrieving information about all active user connections and internal tasks.

The following query retrieves information on the current connection:

```sql
SELECT
    c.session_id, c.net_transport, c.encrypt_option,
    c.auth_scheme, s.host_name, s.program_name,
    s.client_interface_name, s.login_name, s.nt_domain,
    s.nt_user_name, s.original_login_name, c.connect_time,
    s.login_time
FROM sys.dm_exec_connections AS c
JOIN sys.dm_exec_sessions AS s
    ON c.session_id = s.session_id
WHERE c.session_id = @@SPID;
```

## Monitor resource use

You can monitor resource usage using the [Query Store](/sql/relational-databases/performance/monitoring-performance-by-using-the-query-store), just as you would in SQL Server.

You can also monitor usage using [sys.dm_db_resource_stats](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-resource-stats-azure-sql-database) and [sys.server_resource_stats](/sql/relational-databases/system-catalog-views/sys-server-resource-stats-azure-sql-database).

### sys.dm_db_resource_stats

You can use the [sys.dm_db_resource_stats](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-resource-stats-azure-sql-database) view in every database. The `sys.dm_db_resource_stats` view shows recent resource use data relative to the service tier. Average percentages for CPU, data IO, log writes, and memory are recorded every 15 seconds and are maintained for 1 hour.

Because this view provides a more granular look at resource use, use `sys.dm_db_resource_stats` first for any current-state analysis or troubleshooting. For example, this query shows the average and maximum resource use for the current database over the past hour:

```sql
SELECT  
    AVG(avg_cpu_percent) AS 'Average CPU use in percent',
    MAX(avg_cpu_percent) AS 'Maximum CPU use in percent',
    AVG(avg_data_io_percent) AS 'Average data IO in percent',
    MAX(avg_data_io_percent) AS 'Maximum data IO in percent',
    AVG(avg_log_write_percent) AS 'Average log write use in percent',
    MAX(avg_log_write_percent) AS 'Maximum log write use in percent',
    AVG(avg_memory_usage_percent) AS 'Average memory use in percent',
    MAX(avg_memory_usage_percent) AS 'Maximum memory use in percent'
FROM sys.dm_db_resource_stats;  
```

For other queries, see the examples in [sys.dm_db_resource_stats](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-resource-stats-azure-sql-database).

### sys.server_resource_stats

You can use [sys.server_resource_stats](/sql/relational-databases/system-catalog-views/sys-server-resource-stats-azure-sql-database) to return CPU usage, IO, and storage data for an Azure SQL Managed Instance. The data is collected and aggregated within five-minute intervals. There is one row for every 15 seconds reporting. The data returned includes CPU usage, storage size, IO utilization, and managed instance SKU. Historical data is retained for approximately 14 days.

The examples show you different ways that you can use the `sys.server_resource_stats` catalog view to get information about how your instance uses resources.

1. The following example returns the average CPU usage over the last seven days:

    ```sql
    DECLARE @s datetime;  
    DECLARE @e datetime;  
    SET @s= DateAdd(d,-7,GetUTCDate());  
    SET @e= GETUTCDATE();  
    SELECT AVG(avg_cpu_percent) AS Average_Compute_Utilization   
    FROM sys.server_resource_stats   
    WHERE start_time BETWEEN @s AND @e;
    GO
    ```

2. The following example returns the average storage space used by your instance per day, to allow for growth trending analysis:

    ```sql
    DECLARE @s datetime;  
    DECLARE @e datetime;  
    SET @s= DateAdd(d,-7,GetUTCDate());  
    SET @e= GETUTCDATE();  
    SELECT Day = convert(date, start_time), AVG(storage_space_used_mb) AS Average_Space_Used_mb
    FROM sys.server_resource_stats   
    WHERE start_time BETWEEN @s AND @e
    GROUP BY convert(date, start_time)
    ORDER BY convert(date, start_time);
    GO
    ```

### Maximum concurrent requests

To see the current number of concurrent requests, run this Transact-SQL query on your database:

```sql
SELECT COUNT(*) AS [Concurrent_Requests]
FROM sys.dm_exec_requests R;
```

To analyze the workload of an individual database, modify this query to filter on the specific database you want to analyze. For example, if you have a database named `MyDatabase`, this Transact-SQL query returns the count of concurrent requests in that database:

```sql
SELECT COUNT(*) AS [Concurrent_Requests]
FROM sys.dm_exec_requests R
INNER JOIN sys.databases D ON D.database_id = R.database_id
AND D.name = 'MyDatabase';
```

This is just a snapshot at a single point in time. To get a better understanding of your workload and concurrent request requirements, you'll need to collect many samples over time.

### Maximum concurrent logins

You can analyze your user and application patterns to get an idea of the frequency of logins. You also can run real-world loads in a test environment to make sure that you're not hitting this or other limits we discuss in this article. There isn't a single query or dynamic management view (DMV) that can show you concurrent login counts or history.

If multiple clients use the same connection string, the service authenticates each login. If 10 users simultaneously connect to a database by using the same username and password, there would be 10 concurrent logins. This limit applies only to the duration of the login and authentication. If the same 10 users connect to the database sequentially, the number of concurrent logins would never be greater than 1.

### Maximum sessions

To see the number of current active sessions, run this Transact-SQL query on your database:

```sql
SELECT COUNT(*) AS [Sessions]
FROM sys.dm_exec_connections;
```

If you're analyzing a SQL Server workload, modify the query to focus on a specific database. This query helps you determine possible session needs for the database if you are considering moving it to Azure.

```sql
SELECT COUNT(*) AS [Sessions]
FROM sys.dm_exec_connections C
INNER JOIN sys.dm_exec_sessions S ON (S.session_id = C.session_id)
INNER JOIN sys.databases D ON (D.database_id = S.database_id)
WHERE D.name = 'MyDatabase';
```

Again, these queries return a point-in-time count. If you collect multiple samples over time, you'll have the best understanding of your session use.

You can get historical statistics on sessions by querying the [sys.resource_stats](/sql/relational-databases/system-catalog-views/sys-resource-stats-azure-sql-database) view and reviewing the `active_session_count` column.

## Monitoring query performance

Slow or long running queries can consume significant system resources. This section demonstrates how to use dynamic management views to detect a few common query performance problems.

### Finding top N queries

The following example returns information about the top five queries ranked by average CPU time. This example aggregates the queries according to their query hash, so that logically equivalent queries are grouped by their cumulative resource consumption.

```sql
SELECT TOP 5 query_stats.query_hash AS "Query Hash",
    SUM(query_stats.total_worker_time) / SUM(query_stats.execution_count) AS "Avg CPU Time",
     MIN(query_stats.statement_text) AS "Statement Text"
FROM
    (SELECT QS.*,
        SUBSTRING(ST.text, (QS.statement_start_offset/2) + 1,
            ((CASE statement_end_offset
                WHEN -1 THEN DATALENGTH(ST.text)
                ELSE QS.statement_end_offset END
            - QS.statement_start_offset)/2) + 1) AS statement_text
FROM sys.dm_exec_query_stats AS QS
CROSS APPLY sys.dm_exec_sql_text(QS.sql_handle) as ST) as query_stats
GROUP BY query_stats.query_hash
ORDER BY 2 DESC;
```

### Monitoring blocked queries

Slow or long-running queries can contribute to excessive resource consumption and be the consequence of blocked queries. The cause of the blocking can be poor application design, bad query plans, the lack of useful indexes, and so on. You can use the sys.dm_tran_locks view to get information about the current locking activity in database. For example code, see [sys.dm_tran_locks](/sql/relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql). For more information on troubleshooting blocking, see [Understand and resolve Azure SQL blocking problems](/troubleshoot/sql/performance/understand-resolve-blocking).

### Monitoring deadlocks

In some cases, two or more queries may mutually block one another, resulting in a deadlock. 

You can create an Extended Events trace a database to capture deadlock events, then find related queries and their execution plans in Query Store. 

For Azure SQL Managed Instance, refer to the [Deadlocks](/sql/relational-databases/sql-server-transaction-locking-and-row-versioning-guide#deadlock_tools) in the [Transaction locking and row versioning guide](/sql/relational-databases/sql-server-transaction-locking-and-row-versioning-guide).

### Monitoring query plans

An inefficient query plan also may increase CPU consumption. The following example uses the [sys.dm_exec_query_stats](/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql) view to determine which query uses the most cumulative CPU.

```sql
SELECT
    highest_cpu_queries.plan_handle,
    highest_cpu_queries.total_worker_time,
    q.dbid,
    q.objectid,
    q.number,
    q.encrypted,
    q.[text]
FROM
    (SELECT TOP 50
        qs.plan_handle,
        qs.total_worker_time
    FROM
        sys.dm_exec_query_stats qs
ORDER BY qs.total_worker_time desc) AS highest_cpu_queries
CROSS APPLY sys.dm_exec_sql_text(plan_handle) AS q
ORDER BY highest_cpu_queries.total_worker_time DESC;
```

## Other monitoring options

### Monitor with SQL Insights (preview)

[Azure Monitor SQL Insights (preview)](/azure/azure-monitor/insights/sql-insights-overview) is a tool for monitoring instances of Azure SQL Managed Instance, databases in Azure SQL Database, and SQL Server on Azure SQL VMs. This service uses a remote agent to capture data from dynamic management views (DMVs) and routes the data to Azure Log Analytics, where it can be monitored and analyzed. You can view this data from [Azure Monitor](/azure/azure-monitor/overview) in provided views, or access the Log data directly to run queries and analyze trends. To start using Azure Monitor SQL Insights (preview), see [Enable SQL Insights (preview)](/azure/azure-monitor/insights/sql-insights-enable).

### Monitor with Azure Monitor

Azure Monitor provides a variety of diagnostic data collection groups, metrics, and endpoints for monitoring Azure SQL Managed Instance. For more information, see [Monitor Azure SQL Managed Instance with Azure Monitor](monitoring-sql-managed-instance-azure-monitor.md). Azure SQL Analytics (preview) is an integration with Azure Monitor, where many monitoring solutions are no longer in active development. For more monitoring options, see [Monitoring and performance tuning in Azure SQL Managed Instance and Azure SQL Database](../database/monitor-tune-overview.md).

## See also

- [Dynamic Management Views and Functions (Transact-SQL)](/sql/relational-databases/system-dynamic-management-views/system-dynamic-management-views)
- [System Dynamic Management Views](/sql/relational-databases/system-dynamic-management-views/system-dynamic-management-views#required-permissions)

## Next steps

- [Introduction to Azure SQL Database and Azure SQL Managed Instance](../database/sql-database-paas-overview.md)
- [Tune applications and databases for performance in Azure SQL Database and Azure SQL Managed Instance](../database/performance-guidance.md)
- [Understand and resolve SQL Server blocking problems](/troubleshoot/sql/performance/understand-resolve-blocking)
- [Analyze and prevent deadlocks in Azure SQL Managed Instance](/sql/relational-databases/sql-server-transaction-locking-and-row-versioning-guide#deadlock_tools)
- [sys.server_resource_stats (Azure SQL Managed Instance)](/sql/relational-databases/system-catalog-views/sys-server-resource-stats-azure-sql-database)