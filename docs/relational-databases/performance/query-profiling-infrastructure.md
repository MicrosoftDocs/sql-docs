---
title: "Query Profling Infrastruture | Microsoft Docs"
ms.custom: ""
ms.date: "11/26/2018"
ms.prod: sql
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "query plans [SQL Server]"
  - "execution plans [SQL Server]"
  - "query profiling"
  - "lightweight query profiling"
  - "lightweight profiling"
  - "lwp"
ms.assetid: 07f8f594-75b4-4591-8c29-d63811d7753e
author: pmasl
ms.author: pelopes
manager: amitban
---
# Query Profiling Infrastructure
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] provides the ability to access runtime information on query execution plans. One of the most important actions when a performance issue occurs, is to get precise understanding on the workload that is executing and how resource usage is being driven. For this, access to the [actual execution plan](../../relational-databases/performance/display-an-actual-execution-plan.md) is important.

While query completion is a prerequisite for the availability of an actual query plan, [live query statistics](../../relational-databases/performance/live-query-statistics.md) can provide real-time insights into the query execution process as the data flows from one [query plan operator](../../relational-databases/showplan-logical-and-physical-operators-reference.md) to another. The live query plan displays the overall query progress and operator-level run-time execution statistics such as the number of rows produced, elapsed time, operator progress, etc. Because this data is available in real time without needing to wait for the query to complete, these execution statistics are extremely useful for debugging query performance issues, such as long running queries, and queries that run indefinitely and never finish.

## The standard query execution statistics profiling infrastructure

The *query execution statistics profile infrastructure*, or standard profiling, must be enabled to collect information about execution plans, namely row count, CPU and I/O usage. The following methods of collecting execution plan information for a **target session** leverage the standard profiling infrastructure:

- [SET STATISTICS XML](../../t-sql/statements/set-statistics-xml-transact-sql.md) 
- [SET STATISTICS PROFILE](../../t-sql/statements/set-statistics-profile-transact-sql.md)
- [Live Query Statistics](../../relational-databases/performance/live-query-statistics.md)

> [!NOTE]
> Using live query statistics with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] leverages the standard profiling infrastructure.    
> In higher versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], if [lightweight profiling infrastructure](#lwp) is enabled, then it is leveraged by Live Query Statistics instead of standard profiling.

The following methods of collecting execution plan information globally for **all sessions** leverage the standard profiling infrastructure:

-  The ***query_post_execution_showplan*** extended event. To enable extended events, see [Monitor System Activity Using Extended Events](../../relational-databases/extended-events/monitor-system-activity-using-extended-events.md).  
- The **Showplan XML** trace event in [SQL Trace](../../relational-databases/sql-trace/sql-trace.md) and [SQL Server Profiler](../../tools/sql-server-profiler/sql-server-profiler.md). For more information on this trace event, see [Showplan XML Event Class](../../relational-databases/event-classes/showplan-xml-event-class.md).

When running an extended event session that uses the *query_post_execution_showplan* event, then the [sys.dm_exec_query_profiles](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-profiles-transact-sql.md) DMV is also populated, which enables live query statistics for all sessions, using [Activity Monitor](../../relational-databases/performance-monitor/activity-monitor.md) or directly querying the DMV. For more information, see [Live Query Statistics](../../relational-databases/performance/live-query-statistics.md).

## <a name="lwp"></a> The lightweight query execution statistics profiling infrastructure

Starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP2 and [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], a new *lightweight query execution statistics profiling infrastructure*, or **lightweight profiling** was introduced. 

> [!NOTE]
> Natively compiled stored procedures are not supported with lightweight profiling.  

### Lightweight query execution statistics profiling infrastructure v1

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP2 through [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]). 
  
Starting with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP2 and [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], the performance overhead to collect information about execution plans was reduced with the introduction of lightweight profiling. Unlike standard profiling, lightweight profiling does not collect CPU runtime information. However, lightweight profiling still collects row count and I/O usage information.

A new ***query_thread_profile*** extended event was also introduced that leverages lightweight profiling. This extended event exposes per-operator execution statistics allowing more insight on the performance of each node and thread. A sample session using this extended event can be configured as in the below example:

```sql
CREATE EVENT SESSION [NodePerfStats] ON SERVER
ADD EVENT sqlserver.query_thread_profile(
  ACTION(sqlos.scheduler_id,sqlserver.database_id,sqlserver.is_system,
    sqlserver.plan_handle,sqlserver.query_hash_signed,sqlserver.query_plan_hash_signed,
    sqlserver.server_instance_name,sqlserver.session_id,sqlserver.session_nt_username,
    sqlserver.sql_text))
ADD TARGET package0.ring_buffer(SET max_memory=(25600))
WITH (MAX_MEMORY=4096 KB,
  EVENT_RETENTION_MODE=ALLOW_SINGLE_EVENT_LOSS,
  MAX_DISPATCH_LATENCY=30 SECONDS,
  MAX_EVENT_SIZE=0 KB,
  MEMORY_PARTITION_MODE=NONE,
  TRACK_CAUSALITY=OFF,
  STARTUP_STATE=OFF);
```

> [!NOTE]
> For more information on the performance overhead of query profiling, see the blog post [Developers Choice: Query progress - anytime, anywhere](https://blogs.msdn.microsoft.com/sql_server_team/query-progress-anytime-anywhere/). 

When running an extended event session that uses the *query_thread_profile* event, then the [sys.dm_exec_query_profiles](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-profiles-transact-sql.md) DMV is also populated using lightweight profiling, which enables live query statistics for all sessions, using [Activity Monitor](../../relational-databases/performance-monitor/activity-monitor.md) or directly querying the DMV.

### Lightweight query execution statistics profiling infrastructure v2

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP1 through [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)]). 

[!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP1 includes a revised version of lightweight profiling with minimal overhead. Lightweight profiling can also be enabled globally using [trace flag 7412](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) for the versions stated above in *Applies to*. A new DMF [sys.dm_exec_query_statistics_xml](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-statistics-xml-transact-sql.md) is introduced to return the query execution plan for in-flight requests.

Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2 CU3 and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU11, if lightweight profiling is not enabled globally then the new [USE HINT query hint](../../t-sql/queries/hints-transact-sql-query.md#use_hint) argument **QUERY_PLAN_PROFILE** can be used to enable lightweight profiling at the query level, for any session. When a query that contains this new hint finishes, a new ***query_plan_profile*** extended event is also output that provides an actual execution plan XML similar to the *query_post_execution_showplan* extended event. A sample session using this extended event can be configured as in the below example:

```sql
CREATE EVENT SESSION [PerfStats_LWP_Plan] ON SERVER
ADD EVENT sqlserver.query_plan_profile(
  ACTION(sqlos.scheduler_id,sqlserver.database_id,sqlserver.is_system,
    sqlserver.plan_handle,sqlserver.query_hash_signed,sqlserver.query_plan_hash_signed,
    sqlserver.server_instance_name,sqlserver.session_id,sqlserver.session_nt_username,
    sqlserver.sql_text))
ADD TARGET package0.ring_buffer(SET max_memory=(25600))
WITH (MAX_MEMORY=4096 KB,
  EVENT_RETENTION_MODE=ALLOW_SINGLE_EVENT_LOSS,
  MAX_DISPATCH_LATENCY=30 SECONDS,
  MAX_EVENT_SIZE=0 KB,
  MEMORY_PARTITION_MODE=NONE,
  TRACK_CAUSALITY=OFF,
  STARTUP_STATE=OFF);
```

### Lightweight query execution statistics profiling infrastructure v3

**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (Starting with [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)])

[!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)] includes a newly revised version of lightweight profiling collecting row count information for all executions. Lightweight profiling is enabled by default on [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)] and trace flag 7412 has no effect.

## Remarks

> [!IMPORTANT]
> Due to a possible random AV while executing a monitoring stored procedure that references [sys.dm_exec_query_statistics_xml](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-statistics-xml-transact-sql.md), ensure [KB 4078596](http://support.microsoft.com/help/4078596) is installed in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)].

Starting with lightweight profiling v2 and its low overhead, any server that is not already CPU bound can run lightweight profiling **continuously**, and allow database professionals to tap into any running execution at any time, for example using Activity Monitor or directly querying `sys.dm_exec_query_profiles`, and get the query plan with runtime statistics.

For more information on the performance overhead of query profiling, see the blog post [Developers Choice: Query progress - anytime, anywhere](https://blogs.msdn.microsoft.com/sql_server_team/query-progress-anytime-anywhere/). 

## See Also  
 [Monitor and Tune for Performance](../../relational-databases/performance/monitor-and-tune-for-performance.md)     
 [Performance Monitoring and Tuning Tools](../../relational-databases/performance/performance-monitoring-and-tuning-tools.md)     
 [Open Activity Monitor &#40;SQL Server Management Studio&#41;](../../relational-databases/performance-monitor/open-activity-monitor-sql-server-management-studio.md)     
 [Activity Monitor](../../relational-databases/performance-monitor/activity-monitor.md)     
 [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)     
 [Monitor System Activity Using Extended Events](../../relational-databases/extended-events/monitor-system-activity-using-extended-events.md)      
 [sys.dm_exec_query_statistics_xml](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-statistics-xml-transact-sql.md)     
 [sys.dm_exec_query_profiles](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-profiles-transact-sql.md)     
 [Trace flags](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md)    
 [Showplan Logical and Physical Operators Reference](../../relational-databases/showplan-logical-and-physical-operators-reference.md)    
 [actual execution plan](../../relational-databases/performance/display-an-actual-execution-plan.md)    
 [Live Query Statistics](../../relational-databases/performance/live-query-statistics.md)      
 [Developers Choice: Query progress - anytime, anywhere](https://blogs.msdn.microsoft.com/sql_server_team/query-progress-anytime-anywhere/)
