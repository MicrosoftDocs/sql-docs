---
title: "ALTER WORKLOAD GROUP (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/23/2018"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER_WORKLOAD_GROUP_TSQL"
  - "ALTER WORKLOAD GROUP"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ALTER WORKLOAD GROUP statement"
ms.assetid: 957addce-feb0-4e54-893e-5faca3cd184c
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# ALTER WORKLOAD GROUP (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Changes an existing Resource Governor workload group configuration, and optionally assigns it to a to a Resource Governor resource pool.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).  
  
## Syntax  
  
```  
ALTER WORKLOAD GROUP { group_name | "default" }  
[ WITH  
    ([ IMPORTANCE = { LOW | MEDIUM | HIGH } ]  
      [ [ , ] REQUEST_MAX_MEMORY_GRANT_PERCENT = value ]  
      [ [ , ] REQUEST_MAX_CPU_TIME_SEC = value ]  
      [ [ , ] REQUEST_MEMORY_GRANT_TIMEOUT_SEC = value ]   
      [ [ , ] MAX_DOP = value ]  
      [ [ , ] GROUP_MAX_REQUESTS = value ] )  
 ]  
[ USING { pool_name | "default" } ]  
[ ; ]  
```  
  
## Arguments  
 *group_name* | "**default**"  
 Is the name of an existing user-defined workload group or the Resource Governor default workload group.  
  
> [!NOTE]  
> Resource Governor creates the "default" and internal groups when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed.  
  
 The option "default" must be enclosed by quotation marks ("") or brackets ([]) when used with ALTER WORKLOAD GROUP to avoid conflict with DEFAULT, which is a system reserved word. For more information, see [Database Identifiers](../../relational-databases/databases/database-identifiers.md).  
  
> [!NOTE]  
> Predefined workload groups and resource pools all use lowercase names, such as "default". This should be taken into account for servers that use case-sensitive collation. Servers with case-insensitive collation, such as SQL_Latin1_General_CP1_CI_AS, will treat "default" and "Default" as the same.  
  
 IMPORTANCE = { LOW | MEDIUM | HIGH }  
 Specifies the relative importance of a request in the workload group. Importance is one of the following:  
  
-   LOW  
-   MEDIUM (default)  
-   HIGH  
  
> [!NOTE]  
> Internally each importance setting is stored as a number that is used for calculations.  
  
 IMPORTANCE is local to the resource pool; workload groups of different importance inside the same resource pool affect each other, but do not affect workload groups in another resource pool.  
  
 REQUEST_MAX_MEMORY_GRANT_PERCENT =*value*  
 Specifies the maximum amount of memory that a single request can take from the pool. This percentage is relative to the resource pool size specified by MAX_MEMORY_PERCENT.  
  
> [!NOTE]  
> The amount specified only refers to query execution grant memory.  
  
 *value* must be 0 or a positive integer. The allowed range for *value* is from 0 through 100. The default setting for *value* is 25.  
  
 Note the following:  
  
-   Setting *value* to 0 prevents queries with SORT and HASH JOIN operations in user-defined workload groups from running.  
  
-   We do not recommend setting *value* greater than 70 because the server may be unable to set aside enough free memory if other concurrent queries are running. This may eventually lead to query time-out error 8645.  
  
> [!NOTE]  
>  If the query memory requirements exceed the limit that is specified by this parameter, the server does the following:  
>   
>  For user-defined workload groups, the server tries to reduce the query degree of parallelism until the memory requirement falls under the limit, or until the degree of parallelism equals 1. If the query memory requirement is still greater than the limit, error 8657 occurs.  
>   
>  For internal and default workload groups, the server permits the query to obtain the required memory.  
>   
>  Be aware that both cases are subject to time-out error 8645 if the server has insufficient physical memory.  
  
 REQUEST_MAX_CPU_TIME_SEC =*value*  
 Specifies the maximum amount of CPU time, in seconds, that a request can use. *value* must be 0 or a positive integer. The default setting for *value* is 0, which means unlimited.  
  
> [!NOTE]  
> By default, Resource Governor will not prevent a request from continuing if the maximum time is exceeded. However, an event will be generated. For more information, see [CPU Threshold Exceeded Event Class](../../relational-databases/event-classes/cpu-threshold-exceeded-event-class.md). 

> [!IMPORTANT]
> Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP2 and [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] CU3, and using [trace flag 2422](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md), Resource Governor will abort a request when the maximum time is exceeded.
  
 REQUEST_MEMORY_GRANT_TIMEOUT_SEC =*value*  
 Specifies the maximum time, in seconds, that a query can wait for memory grant (work buffer memory) to become available.  
  
> [!NOTE]  
> A query does not always fail when memory grant time-out is reached. A query will only fail if there are too many concurrent queries running. Otherwise, the query may only get the minimum memory grant, resulting in reduced query performance.  
  
 *value* must be a positive integer. The default setting for *value*, 0, uses an internal calculation based on query cost to determine the maximum time.  
  
 MAX_DOP =*value*  
 Specifies the maximum degree of parallelism (DOP) for parallel requests. *value* must be 0 or a positive integer, 1 though 255. When *value* is 0, the server chooses the max degree of parallelism. This is the default and recommended setting.  
  
> [!NOTE]  
> The actual value that the [!INCLUDE[ssDE](../../includes/ssde-md.md)] sets for MAX_DOP by might be less than the specified value. The final value is determined by the formula min(255, *number of CPUs)*.  
  
> [!CAUTION]  
> Changing MAX_DOP can adversely affect a server's performance. If you must change MAX_DOP, we recommend that it be set to a value that is less than or equal to the maximum number of hardware schedulers that are present in a single NUMA node. We recommend that you do not set MAX_DOP to a value greater than 8.  
  
 MAX_DOP is handled as follows:  
  
-   MAX_DOP as a query hint is honored as long as it does not exceed workload group MAX_DOP.  
  
-   MAX_DOP as a query hint always overrides sp_configure 'max degree of parallelism'.  
  
-   Workload group MAX_DOP overrides sp_configure 'max degree of parallelism'.  
  
-   If the query is marked as serial (MAX_DOP = 1 ) at compile time, it cannot be changed back to parallel at run time regardless of the workload group or sp_configure setting.  
  
 After DOP is configured, it can only be lowered on grant memory pressure. Workload group reconfiguration is not visible while waiting in the grant memory queue.  
  
 GROUP_MAX_REQUESTS =*value*  
 Specifies the maximum number of simultaneous requests that are allowed to execute in the workload group. *value* must be 0 or a positive integer. The default setting for *value*, 0, allows unlimited requests. When the maximum concurrent requests are reached, a user in that group can log in, but is placed in a wait state until concurrent requests are dropped below the value specified.  
  
 USING { *pool_name* | "**default**" }  
 Associates the workload group with the user-defined resource pool identified by *pool_name*, which in effect puts the workload group in the resource pool. If *pool_name* is not provided or if the USING argument is not used, the workload group is put in the predefined Resource Governor default pool.  
  
 The option "default" must be enclosed by quotation marks ("") or brackets ([]) when used with ALTER WORKLOAD GROUP to avoid conflict with DEFAULT, which is a system reserved word. For more information, see [Database Identifiers](../../relational-databases/databases/database-identifiers.md).  
  
> [!NOTE]  
> The option "default" is case-sensitive.  
  
## Remarks  
 ALTER WORKLOAD GROUP is allowed on the default group.  
  
 Changes to the workload group configuration do not take effect until after ALTER RESOURCE GOVERNOR RECONFIGURE is executed. When changing a plan affecting setting, the new setting will only take effect in previously cached plans after executing DBCC FREEPROCCACHE (*pool_name*), where *pool_name* is the name of a Resource Governor resource pool on which the workload group is associated with.  
  
-   If you are changing MAX_DOP to 1, executing DBCC FREEPROCCACHE is not required because parallel plans can run in serial mode. However, it may not be as efficient as a plan compiled as a serial plan.  
  
-   If you are changing MAX_DOP from 1 to 0 or a value greater than 1, executing DBCC FREEPROCCACHE is not required. However, serial plans cannot run in parallel, so clearing the respective cache will allow new plans to potentially be compiled using parallelism.  
  
> [!CAUTION]  
> Clearing cached plans from a resource pool that is associated with more than one workload group will affect all workload groups with the user-defined resource pool identified by *pool_name*.  
  
 When you are executing DDL statements, we recommend that you be familiar with Resource Governor states. For more information, see [Resource Governor](../../relational-databases/resource-governor/resource-governor.md).  
  
 REQUEST_MEMORY_GRANT_PERCENT: In [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], index creation is allowed to use more workspace memory than initially granted for improved performance. This special handling is supported by Resource Governor in later versions, however, the initial grant and any additional memory grant are limited by resource pool and workload group settings.  
  
 **Index Creation on a Partitioned Table**  
  
 The memory consumed by index creation on non-aligned partitioned table is proportional to the number of partitions involved.  If the total required memory exceeds the per-query limit (REQUEST_MAX_MEMORY_GRANT_PERCENT) imposed by the Resource Governor workload group setting, this index creation may fail to execute. Because the "default" workload group allows a query to exceed the per-query limit with the minimum required memory to start for [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] compatibility, the user may be able to run the same index creation in "default" workload group, if the "default" resource pool has enough total memory configured to run such query.  
  
## Permissions  
 Requires CONTROL SERVER permission.  
  
## Examples  
 The following example shows how to change the importance of requests in the default group from `MEDIUM` to `LOW`.  
  
```sql  
ALTER WORKLOAD GROUP "default"  
WITH (IMPORTANCE = LOW);  
GO  
ALTER RESOURCE GOVERNOR RECONFIGURE;  
GO  
```  
  
 The following example shows how to move a workload group from the pool that it is in to the default pool.  
  
```sql  
ALTER WORKLOAD GROUP adHoc  
USING [default];  
GO  
ALTER RESOURCE GOVERNOR RECONFIGURE;  
GO  
```  
  
## See Also  
 [Resource Governor](../../relational-databases/resource-governor/resource-governor.md)   
 [CREATE WORKLOAD GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/create-workload-group-transact-sql.md)   
 [DROP WORKLOAD GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/drop-workload-group-transact-sql.md)   
 [CREATE RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/create-resource-pool-transact-sql.md)   
 [ALTER RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/alter-resource-pool-transact-sql.md)   
 [DROP RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/drop-resource-pool-transact-sql.md)   
 [ALTER RESOURCE GOVERNOR &#40;Transact-SQL&#41;](../../t-sql/statements/alter-resource-governor-transact-sql.md)  
  
  
