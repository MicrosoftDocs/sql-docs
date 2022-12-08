---
title: "ALTER RESOURCE POOL (Transact-SQL)"
description: ALTER RESOURCE POOL (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "05/01/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ALTER_RESOURCE_POOL_TSQL"
  - "ALTER RESOURCE POOL"
helpviewer_keywords:
  - "ALTER RESOURCE POOL"
dev_langs:
  - "TSQL"
---
# ALTER RESOURCE POOL (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Changes an existing Resource Governor resource pool configuration in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).  
  
## Syntax  
  
```syntaxsql
ALTER RESOURCE POOL { pool_name | "default" }  
[WITH  
    ( [ MIN_CPU_PERCENT = value ]  
    [ [ , ] MAX_CPU_PERCENT = value ]   
    [ [ , ] CAP_CPU_PERCENT = value ]   
    [ [ , ] AFFINITY {
                        SCHEDULER = AUTO 
                      | ( <scheduler_range_spec> ) 
                      | NUMANODE = ( <NUMA_node_range_spec> )
                      }]   
    [ [ , ] MIN_MEMORY_PERCENT = value ]  
    [ [ , ] MAX_MEMORY_PERCENT = value ]   
    [ [ , ] MIN_IOPS_PER_VOLUME = value ]  
    [ [ , ] MAX_IOPS_PER_VOLUME = value ]  
)]   
[;]  
  
<scheduler_range_spec> ::=  
{SCHED_ID | SCHED_ID TO SCHED_ID}[,...n]  
  
<NUMA_node_range_spec> ::=  
{NUMA_node_ID | NUMA_node_ID TO NUMA_node_ID}[,...n]  
```  
  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 { *pool_name* | **"default"** }  
 Is the name of an existing user-defined resource pool or the default resource pool that is created when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed.  
  
 "default" must be enclosed by quotation marks ("") or brackets ([]) when used with ALTER RESOURCE POOL to avoid conflict with DEFAULT, which is a system reserved word. For more information, see [Database Identifiers](../../relational-databases/databases/database-identifiers.md).  
  
> [!NOTE]  
>  Predefined workload groups and resource pools all use lowercase names, such as "default". This should be taken into account for servers that use case-sensitive collation. Servers with case-insensitive collation, such as SQL_Latin1_General_CP1_CI_AS, will treat "default" and "Default" as the same.  
  
 MIN_CPU_PERCENT =*value*  
 Specifies the guaranteed average CPU bandwidth for all requests in the resource pool when there is CPU contention. *value* is an integer with a default setting of 0. The allowed range for *value* is from 0 through 100.  
  
 MAX_CPU_PERCENT =*value*  
 Specifies the maximum average CPU bandwidth that all requests in the resource pool will receive when there is CPU contention. *value* is an integer with a default setting of 100. The allowed range for *value* is from 1 through 100.  
  
 CAP_CPU_PERCENT =*value*  
 **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.  
  
 Specifies the target maximum CPU capacity for requests in the resource pool. *value* is an integer with a default setting of 100. The allowed range for *value* is from 1 through 100.  
  
> [!NOTE]  
>  Due to the statistical nature of CPU governance, you may notice occasional spikes exceeding the value specified in CAP_CPU_PERCENT.  
  
 AFFINITY {SCHEDULER = AUTO | (Scheduler_range_spec) | NUMANODE = (NUMA_node_range_spec)}  
 **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.  
  
 Attach the resource pool to specific schedulers. The default value is AUTO.  
  
 AFFINITY SCHEDULER = (Scheduler_range_spec) maps the resource pool to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] schedules identified by the given IDs. These IDs map to the values in the scheduler_id column in [sys.dm_os_schedulers &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-schedulers-transact-sql.md).  
  
 When you use AFFINITY NAMANODE = (NUMA_node_range_spec), the resource pool is affinitized to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] schedulers that map to the physical CPUs that correspond to the given NUMA node or range of nodes. You can use the following Transact-SQL query to discover the mapping between the physical NUMA configuration and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] scheduler IDs.  
  
```sql  
SELECT osn.memory_node_id AS [numa_node_id], sc.cpu_id, sc.scheduler_id  
FROM sys.dm_os_nodes AS osn  
INNER JOIN sys.dm_os_schedulers AS sc 
   ON osn.node_id = sc.parent_node_id 
      AND sc.scheduler_id < 1048576;  
```  
  
 MIN_MEMORY_PERCENT =*value*  
 Specifies the minimum amount of memory reserved for this resource pool that can not be shared with other resource pools. *value* is an integer with a default setting of 0. The allowed range for *value* is from 0 through 100.  
  
 MAX_MEMORY_PERCENT =*value*  
 Specifies the total server memory that can be used by requests in this resource pool. *value* is an integer with a default setting of 100. The allowed range for *value* is from 1 through 100.  
  
 MIN_IOPS_PER_VOLUME =*value*  
 **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later.  
  
 Specifies the minimum I/O operations per second (IOPS) per disk volume to reserve for the resource pool. The allowed range for *value* is from 0 through 2^31-1 (2,147,483,647). Specify 0 to indicate no minimum threshold for the pool.  
  
 MAX_IOPS_PER_VOLUME =*value*  
 **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later.  
  
 Specifies the maximum I/O operations per second (IOPS) per disk volume to allow for the resource pool. The allowed range for *value* is from 0 through 2^31-1 (2,147,483,647). Specify 0 to set an unlimited threshold for the pool. The default is 0.  
  
 If the MAX_IOPS_PER_VOLUME for a pool is set to 0, the pool is not governed at all and can take all the IOPS in the system even if other pools have MIN_IOPS_PER_VOLUME set. For this case, we recommend that you set the MAX_IOPS_PER_VOLUME value for this pool to a high number (for example, the maximum value 2^31-1) if you want this pool to be governed for IO.  
  
## Remarks  
 MAX_CPU_PERCENT and MAX_MEMORY_PERCENT must be greater than or equal to MIN_CPU_PERCENT and MIN_MEMORY_PERCENT, respectively.  
  
 MAX_CPU_PERCENT can use CPU capacity above the value of MAX_CPU_PERCENT if it is available. Although there may be periodic spikes above CAP_CPU_PERCENT, workloads should not exceed CAP_CPU_PERCENT for extended periods of time, even when additional CPU capacity is available.  
  
 The total CPU percentage for each affinitized component (scheduler(s) or NUMA node(s)) should not exceed 100%.  
  
 When you are executing DDL statements, we recommend that you be familiar with Resource Governor states. For more information, see [Resource Governor](../../relational-databases/resource-governor/resource-governor.md).  
  
 When changing a plan affecting setting, the new setting will only take effect in previously cached plans after executing DBCC FREEPROCCACHE (*pool_name*), where *pool_name* is the name of a Resource Governor resource pool.  
  
-   If you are changing AFFINITY from multiple schedulers to a single scheduler, executing DBCC FREEPROCCACHE is not required because parallel plans can run in serial mode. However, it may not be as efficient as a plan compiled as a serial plan.  
  
-   If you are changing AFFINITY from a single scheduler to multiple schedulers, executing DBCC FREEPROCCACHE is not required. However, serial plans cannot run in parallel, so clearing the respective cache will allow new plans to potentially be compiled using parallelism.  
  
> [!CAUTION]  
>  Clearing cached plans from a resource pool that is associated with more than one workload group will affect all workload groups with the user-defined resource pool identified by *pool_name*.  
  
## Permissions  
 Requires CONTROL SERVER permission.  
  
## Examples  
 The following example keeps all the default resource pool settings on the `default` pool except for `MAX_CPU_PERCENT`, which is changed to `25`.  
  
```sql  
ALTER RESOURCE POOL "default"  
WITH  
     ( MAX_CPU_PERCENT = 25);  
GO  
ALTER RESOURCE GOVERNOR RECONFIGURE;  
GO  
```  
  
 In the following example, the `CAP_CPU_PERCENT` sets the hard cap to 80% and `AFFINITY SCHEDULER` is set to an individual value of 8 and a range of 12 to 16.  
  
**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.  
  
```sql  
ALTER RESOURCE POOL Pool25  
WITH(   
     MIN_CPU_PERCENT = 5,  
     MAX_CPU_PERCENT = 10,       
     CAP_CPU_PERCENT = 80,  
     AFFINITY SCHEDULER = (8, 12 TO 16),   
     MIN_MEMORY_PERCENT = 5,  
     MAX_MEMORY_PERCENT = 15  
);  
  
GO  
ALTER RESOURCE GOVERNOR RECONFIGURE;  
GO  
```  
  
## See Also  
 [Resource Governor](../../relational-databases/resource-governor/resource-governor.md)   
 [CREATE RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/create-resource-pool-transact-sql.md)   
 [DROP RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/drop-resource-pool-transact-sql.md)   
 [CREATE WORKLOAD GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/create-workload-group-transact-sql.md)   
 [ALTER WORKLOAD GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/alter-workload-group-transact-sql.md)   
 [DROP WORKLOAD GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/drop-workload-group-transact-sql.md)   
 [ALTER RESOURCE GOVERNOR &#40;Transact-SQL&#41;](../../t-sql/statements/alter-resource-governor-transact-sql.md)  
  
  
