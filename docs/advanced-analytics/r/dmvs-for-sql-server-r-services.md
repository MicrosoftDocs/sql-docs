---
title: "DMVs for SQL Server R Services | Microsoft Docs"
ms.custom: ""
ms.date: "11/29/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: b3643ea0-d9f3-463f-8ece-572127f32a24
caps.latest.revision: 13
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# DMVs for SQL Server R Services

The topic lists the system catalog views and DMVs that are related to [!INCLUDE[rsql_productname_md](../../includes/rsql-productname-md.md)]. 


For information about extended events, see [Extended Events for SQL Server R Services](../../advanced-analytics/r-services/extended-events-for-sql-server-r-services.md).

> [!TIP]
> The product team has provided custom reports that you can use to monitor R Services sessions and packages. For more information, see [Monitor R Services using Custom Reports in Management Studio](../../advanced-analytics/r-services/monitor-r-services-using-custom-reports-in-management-studio.md).
> 

## System Configuration and System Resources

You can monitor and analyze the resources used by R scripts by using [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] system catalog views and DMVs.


**General**
+ [ sys.dm_exec_sessions](../../relational-databases/system-dynamic-management-views/sys-dm-exec-sessions-transact-sql.md)

  Returns information for both user connections and system sessions. You can identify the system sessions by looking at the *session_id* column; values greater than or equal to 51 are user connections and values lower than 51 are system processes. 



+ [sys.dm_os_performance_counters (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md)

  Returns a row for each system performance counter being used by the server.  You can use this information to see how many scripts ran, which scripts were run using which authentication mode, or how many R calls were issued on the instance overall.

  This example gets just the counters related to R script:

  ```
  SELECT * from sys.dm_os_performance_counters WHERE object_name LIKE '%Extended Scripts%'
  ```

  The following counters are reported by this DMV for external scripts per instance:

  + **Total Executions**: Number of R processes started by local or remote calls
  + **Parallel Executions**: Number of times that a script included the @parallel specification and that [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] was able to generate and use a parallel query plan
  + **Streaming Executions**: Number of times that the streaming feature has been invoked. 
  + **SQL CC Executions**: Number of R scripts run where the call was instantiated remotely and SQL Server used as the compute context 
  + **Implied Auth. Logins**: Number of times that an ODBC loopback call was made using implied authentication; that is, the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] executed the call on behalf of the user sending the script request
  + **Total Execution Time (ms)**: Time elapsed between the call and completion of call.
  + **Execution Errors**: Number of times scripts reported errors. This count does not include R errors.


+ [sys.dm_external_script_requests](../../relational-databases/system-dynamic-management-views/sys-dm-external-script-requests.md)

  This DMV reports a single row for each worker account that is currently running an external script. Note that this worker account is different from the credentials of the person sending the script. If a single Windows user sends multiple script requests, only one worker account would be assigned to handle all requests from that user. If a different Windows user logs in to run an external script, the request would be handled by a separate worker account. 
  This DMV does not return any results if no scripts are currently being executed; thus, it is most useful for monitoring long-running scripts. It returns these values:
  + **external_script_request_id**: A GUID, which is also used as the  temporary name of the working directory used to store scripts and intermediate results.  
  + **language**: A value such as `R` that denotes the language of the external script.
  + **degree_of_parallelism**:  An integer indicating the number of parallel processes that were used. 
  + **external_user_name**: A Launchpad worker account, such as SQLRUser01. 
  

+ [sys.dm_external_script_execution_stats (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-external-script-execution-stats.md)

  This DMV is provided for internal monitoring (telemetry) to track how many R calls are made on an instance. The telemetry service starts when [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] does and increments a disk-based counter each time a specific R function is called.

  The counter is incremented per call to a function. For example, if `rxLinMod` is called and run in parallel, the counter is incremented by 1.
  
  Generally speaking, performance counters are valid only as long as the process that generated them is active. Therefore, a query on a DMV cannot show detailed data for  services that have stopped running. For example, if the Launchpad creates multiple parallel R jobs and yet they are very quickly executed and then cleaned up by the Windows job object, a DMV might not show any data.
 
  However, the counters tracked by this DMV are kept running, and state for dm_external_script _execution counter is preserved by using writes to disk, even if the instance is shut down.
 
 For more information about system performance counters used by [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], see [Use SQL Server Objects](../../relational-databases/performance-monitor/use-sql-server-objects.md).

**Resource Governor views**

+ [sys.resource_governor_resource_pools](../../relational-databases/system-catalog-views/sys-resource-governor-resource-pools-transact-sql.md)

  Returns information about the current resource pool state, the current configuration of resource pools, and resource pool statistics.

  > [!IMPORTANT]
  > 
  > You must modify resource pools that apply to other server services before you can allocate additional resources to R Services.


+ [sys.resource_governor_external_resource_pools](../../relational-databases/system-catalog-views/sys-resource-governor-external-resource-pools-transact-sql.md)

  A new catalog view that shows the current configuration values for external resource pools.
  In Enterprise Edition, you can configure additional external resource pools: for example, you might decide to handle resources for R jobs running in [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] separately from those that originate from a remote client. 

  > [!NOTE]
  > 
  > In Standard Edition all R jobs are run in the same external default resource pool.

+ [sys.resource_governor_workload_groups](../../relational-databases/system-catalog-views/sys-resource-governor-workload-groups-transact-sql.md)

  Returns workload group statistics and the current configuration of the workload group. This view can be joined with sys.dm_resource_governor_resource_pools to get the resource pool name.
  For external scripts, a new column has been added that shows the id of the external pool associated with the workload group. 


+ [sys.dm_resource_governor_external_resource_pool_affinity](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-external-resource-pool-affinity-transact-sql.md)

  A new system catalog view that lets you see the processors and resources that are affinitized to a particular resource pool.

  Returns one row per scheduler in [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] where each scheduler is mapped to an individual processor. Use this view to monitor the condition of a scheduler or to identify runaway tasks.

  Under the default configuration, workload pools are automatically assigned to processors and therefore there are no affinity values to return.

  The affinity schedule maps the resource pool to the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] schedules identified by the given IDs. These IDs map to the values in the scheduler_id column in sys.dm_os_schedulers (Transact-SQL).


> [!NOTE] 
> 
> Although the ability to configure and customize resource pools is available only in Enterprise and Developer editions, the default pools as well as the DMVs are available in all editions. Therefore, you can use these DMVs in Standard Edition to determine resource caps for your R jobs. 

For general information about monitoring [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] instances, see [Catalog Views](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md) and [Resource Governor Related Dynamic Management Views](../../relational-databases/system-dynamic-management-views/resource-governor-related-dynamic-management-views-transact-sql.md).

## R Script Execution and Monitoring

R scripts that run in [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] are started by the [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] interface. However, the Launchpad is not resource governed or monitored separately, as it is assumed to be a secure service provided by Microsoft that manages resources appropriately.

Individual R scripts that run under the Launchpad service are managed using the 
[Windows job object](https://msdn.microsoft.com/library/windows/desktop/ms684161.aspx). A job object allows groups of processes to be managed as a unit. Each job object is hierarchical and controls the attributes of all processes associated with it. Operations performed on a job object affect all processes associated with the job object. 

Thus, if you need to terminate one job associated with an object, be aware that all related processes will also be terminated. If you are running an R script that is assigned to a Windows job object and that script runs a related ODBC job which must be terminated, the parent R script process will be terminated as well. 

If you start an R script that uses parallel processing, a single Windows job object manages all  parallel child processes.

To determine if a process is running in a job, use the `IsProcessInJob` function.

## See Also
[Managing and Monitoring](../../advanced-analytics/r-services/managing-and-monitoring-r-solutions.md)


