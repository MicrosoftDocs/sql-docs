---
title: Dynamic management views (DMVs) for SQL Server Machine Learning Services | Microsoft Docs
description: List of dynamic management views (DMVs) related to SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning

ms.date: 10/28/2018  
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Dynamic management views for SQL Server Machine Learning Services
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

Dynamic management views (DMVs) return server state information that can be used to monitor the health of a server instance, diagnose problems, and tune performance. The article lists the that are related to machine learning in SQL Server. For more information, see [System Dynamic Management Views](../../relational-databases/system-dynamic-management-views/system-dynamic-management-views.md).

> [!TIP]
> Use the built-in reports to monitor machine learning sessions and package utilization. For more information, see [Monitor machine learning using custom reports in Management Studio](../../advanced-analytics/r/monitor-r-services-using-custom-reports-in-management-studio.md).

## Monitor system resources

You can monitor the resources used by external scripts by using the dynamic management views below. To query the DMVs, you need VIEW SERVER STATE permission on server.

| Dynamic management view | Description |
|-------------------------|-----------------|
| [sys.dm_external_script_requests](../../relational-databases/system-dynamic-management-views/sys-dm-external-script-requests.md) | Returns a row for each active worker account that is running an external script. |
| [sys.dm_external_script_execution_stats](../../relational-databases/system-dynamic-management-views/sys-dm-external-script-execution-stats.md) | Returns one row for each type of external script request. |
| [sys.dm_os_performance_counters](../../relational-databases/system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) | Returns a row per performance counter maintained by the server. You can use this information to see how many scripts ran, which scripts were run using which authentication mode, or how many R calls were issued on the instance overall. |

> [!NOTE]  
> Users who run external scripts must have the additional permission EXECUTE ANY EXTERNAL SCRIPT, however, these DMVs can be used by administrators without this permission.

### Performance counters

The following example returns the performance counters related to external scripts:

```SQL
SELECT *
FROM sys.dm_os_performance_counters 
WHERE object_name LIKE '%External Scripts%'
```

The counters below are reported by **sys.dm_os_performance_counters** where the object name is **External scripts**:

| Counter | Description |
|---------|-------------|
| Total Executions | Number of external processes started by local or remote calls. |
| Parallel Executions | Number of times that a script included the _@parallel_ specification and that [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] was able to generate and use a parallel query plan. |
| Streaming Executions | Number of times that the streaming feature has been invoked. |
| SQL CC Executions | Number of external scripts run where the call was instantiated remotely and SQL Server was used as the compute context. |
| Implied Auth. Logins | Number of times that an ODBC loopback call was made using implied authentication; that is, the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] executed the call on behalf of the user sending the script request. |
| Total Execution Time (ms) | Time elapsed between the call and completion of call. |
| Execution Errors | Number of times scripts reported errors. This count does not include R or Python errors. |

## Resource Governor views

In editions that support Resource Governor, creating external resource pools for R or Python workloads can be en effective way to track and manage resources.

You can see configuration information about the external resource pools by using the following dynamic management views:

| Dynamic management view | Description |
|-------------------------|-------------|
| [sys.resource_governor_external_resource_pools](../../relational-databases/system-catalog-views/sys-resource-governor-external-resource-pools-transact-sql.md) | Returns information about the current external resource pool state, the current configuration of resource pools, and resource pool statistics. |
| [sys.dm_resource_governor_external_resource_pool_affinity](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-external-resource-pool-affinity-transact-sql.md) | Returns CPU affinity information about the current external resource pool configuration. Returns one row per scheduler in [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] where each scheduler is mapped to an individual processor. Use this view to monitor the condition of a scheduler or to identify runaway tasks.  Under the default configuration, workload pools are automatically assigned to processors and therefore there are no affinity values to return. The affinity schedule maps the resource pool to the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] schedules identified by the given IDs. These IDs map to the values in the `scheduler_id` column in `sys.dm_os_schedulers`. |

For general information about monitoring [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] instances, see [Catalog Views](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md) and [Resource Governor Related Dynamic Management Views](../../relational-databases/system-dynamic-management-views/resource-governor-related-dynamic-management-views-transact-sql.md).

## Monitoring script execution

R and Python scripts that run in [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] are started by the [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] interface. However, the Launchpad is not resource governed or monitored separately, as it is a secure service provided by Microsoft that manages resources appropriately.

External scripts that run under the Launchpad service are managed using the 
[Windows job object](/windows/desktop/ProcThread/job-objects). A job object allows groups of processes to be managed as a unit. Each job object is hierarchical and controls the attributes of all processes associated with it. Operations performed on a job object affect all processes associated with the job object.

Thus, if you need to terminate one job associated with an object, be aware that all related processes will also be terminated. If you are running an R script that is assigned to a Windows job object and that script runs a related ODBC job which must be terminated, the parent R script process will be terminated as well.

If you start an external script that uses parallel processing, a single Windows job object manages all  parallel child processes.

To determine if a process is running in a job, use the `IsProcessInJob` function.

## Next steps

+ [Managing and monitoring machine learning solutions](../../advanced-analytics/r/managing-and-monitoring-r-solutions.md)
+ [Extended events for machine learning](../../advanced-analytics/r/extended-events-for-sql-server-r-services.md)
+ [Resource Governor Related Dynamic Management Views](../../relational-databases/system-dynamic-management-views/resource-governor-related-dynamic-management-views-transact-sql.md)
+ [System Dynamic Management Views](../../relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)