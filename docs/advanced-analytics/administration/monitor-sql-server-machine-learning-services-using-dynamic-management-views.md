---
title: Monitor SQL Server Machine Learning Services using dynamic management views (DMVs) | Microsoft Docs
description: Use dynamic management views (DMVs) to monitor SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning

ms.date: 10/29/2018  
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Monitor SQL Server Machine Learning Services using dynamic management views (DMVs)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

Dynamic management views (DMVs) return server state information that can be used to monitor the health of a server instance, diagnose problems, and tune performance. The article lists the DMVs that are related to machine learning in SQL Server. For more general information about DMVs, see [System Dynamic Management Views](../../relational-databases/system-dynamic-management-views/system-dynamic-management-views.md).

> [!TIP]
> Use the built-in reports to monitor machine learning sessions and package utilization. For more information, see [Monitor machine learning using custom reports in Management Studio](../../advanced-analytics/r/monitor-r-services-using-custom-reports-in-management-studio.md).

## Dynamic management views

You can monitor the resources used by external scripts by using the dynamic management views below. To query the DMVs, you need `VIEW SERVER STATE` permission on the server.

| Dynamic management view | Type | Description |
|-------------------------|------|-------------|
| [sys.dm_external_script_requests](../../relational-databases/system-dynamic-management-views/sys-dm-external-script-requests.md) | Execution | Returns a row for each active worker account that is running an external script. |
| [sys.dm_external_script_execution_stats](../../relational-databases/system-dynamic-management-views/sys-dm-external-script-execution-stats.md) | Execution | Returns one row for each type of external script request. |
| [sys.dm_os_performance_counters](../../relational-databases/system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) | Execution | Returns a row per performance counter maintained by the server. You can use this information to see how many scripts ran, which scripts were run using which authentication mode, or how many R or Python calls were issued on the instance overall. |
| [sys.resource_governor_external_resource_pools](../../relational-databases/system-catalog-views/sys-resource-governor-external-resource-pools-transact-sql.md) | Resource Governor | Returns information about the current external resource pool state in Resource Governor, the current configuration of resource pools, and resource pool statistics. |
| [sys.dm_resource_governor_external_resource_pool_affinity](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-external-resource-pool-affinity-transact-sql.md) | Resource Governor | Returns CPU affinity information about the current external resource pool configuration in Resource Governor. Returns one row per scheduler in [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] where each scheduler is mapped to an individual processor. Use this view to monitor the condition of a scheduler or to identify runaway tasks. |

For information about monitoring [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] instances, see [Catalog Views](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md) and [Resource Governor Related Dynamic Management Views](../../relational-databases/system-dynamic-management-views/resource-governor-related-dynamic-management-views-transact-sql.md).

> [!NOTE]  
> Users who run external scripts must have the additional permission `EXECUTE ANY EXTERNAL SCRIPT`, however, these DMVs can be used by administrators without this permission.

## Active sessions

The following example returns the active sessions that are running external scripts:

```SQL
SELECT r.session_id, r.blocking_session_id, r.status
	, DB_NAME(s.database_id) AS database_name, s.login_name
	, r.wait_time, r.wait_type, r.last_wait_type
	, r.total_elapsed_time, r.cpu_time, r.reads, r.logical_reads, r.writes
	, er.language, er.degree_of_parallelism, er.external_user_name
FROM sys.dm_exec_requests AS r
INNER JOIN sys.dm_external_script_requests AS er
ON r.external_script_request_id = er.external_script_request_id
INNER JOIN sys.dm_exec_sessions AS s
ON s.session_id = r.session_id
```

The query joins three [sys.dm_exec_requests](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md), [sys.dm_external_script_requests](../../relational-databases/system-dynamic-management-views/sys-dm-external-script-requests.md), and [sys.dm_exec_sessions](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sessions-transact-sql.md). It returns the following columns :

| Column | Description |
|--------|-------------|
| session_id | Identifies the session associated with each active primary connection. |
| blocking_session_id | ID of the session that is blocking the request. If this column is NULL, the request is not blocked, or the session information of the blocking session is not available (or cannot be identified). |
| status | Status of the request. |
| database_name | Name of the current database for each session. |
| login_name | SQL Server login name under which the session is currently executing. |
| wait_time | If the request is currently blocked, this column returns the duration in milliseconds, of the current wait. Is not nullable. |
| wait_type | If the request is currently blocked, this column returns the type of wait. For information about types of waits, see [sys.dm_os_wait_stats](../../relational-databases/system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql.md). |
| last_wait_type | If this request has previously been blocked, this column returns the type of the last wait. |
| total_elapsed_time | Total time elapsed in milliseconds since the request arrived. |
| cpu_time | CPU time in milliseconds that is used by the request. |
| reads | Number of reads performed by this request. |
| logical_reads | Number of logical reads that have been performed by the request. |
| writes | Number of writes performed by this request. |
| language | Keyword that represents a supported script language. |
| degree_of_parallelism | Number indicating the number of parallel processes that were created. This value might be different from the number of parallel processes that were requested. |
| external_user_name | The Windows worker account under which the script was executed. |


## Performance counters

The following example returns the performance counters related to external scripts:

```SQL
SELECT *
FROM sys.dm_os_performance_counters 
WHERE object_name LIKE '%External Scripts%'
```

The counters below are reported by **sys.dm_os_performance_counters**  for external scripts:

| Counter | Description |
|---------|-------------|
| Total Executions | Number of external processes started by local or remote calls. |
| Parallel Executions | Number of times that a script included the _@parallel_ specification and that [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] was able to generate and use a parallel query plan. |
| Streaming Executions | Number of times that the streaming feature has been invoked. |
| SQL CC Executions | Number of external scripts run where the call was instantiated remotely and SQL Server was used as the compute context. |
| Implied Auth. Logins | Number of times that an ODBC loopback call was made using implied authentication; that is, the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] executed the call on behalf of the user sending the script request. |
| Total Execution Time (ms) | Time elapsed between the call and completion of call. |
| Execution Errors | Number of times scripts reported errors. This count does not include R or Python errors. |

## Next steps

+ [Managing and monitoring machine learning solutions](../../advanced-analytics/r/managing-and-monitoring-r-solutions.md)
+ [Extended events for machine learning](../../advanced-analytics/r/extended-events-for-sql-server-r-services.md)
+ [Resource Governor Related Dynamic Management Views](../../relational-databases/system-dynamic-management-views/resource-governor-related-dynamic-management-views-transact-sql.md)
+ [System Dynamic Management Views](../../relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)