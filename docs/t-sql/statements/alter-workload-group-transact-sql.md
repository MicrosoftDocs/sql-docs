---
title: ALTER WORKLOAD GROUP (Transact-SQL) 
ms.custom: ""
ms.date: "05/05/2020"
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
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azure-sqldw-latest||=azuresqldb-mi-current"
---
# ALTER WORKLOAD GROUP (Transact-SQL)

## Click a product!

In the following row, click whichever product name you're interested in. The click displays different content here on this webpage, appropriate for whichever product you click.

::: moniker range=">=sql-server-2016||>=sql-server-linux-2017||=sqlallproducts-allversions"

> |||||
> |---|---|---|---|
> |**_\* SQL Server \*_** &nbsp;||[SQL Database<br />managed instance](alter-workload-group-transact-sql.md?view=azuresqldb-mi-current)||[Azure Synapse<br />Analytics](alter-workload-group-transact-sql.md?view=azure-sqldw-latest)|

&nbsp;

## SQL Server 

  Changes an existing Resource Governor workload group configuration, and optionally assigns it to a Resource Governor resource pool.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).  
  
## Syntax  
  
```syntaxsql
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
  
 IMPORTANCE = { LOW | **MEDIUM** | HIGH }       
 Specifies the relative importance of a request in the workload group. Importance is one of the following:  
  
-   LOW  
-   MEDIUM (default)  
-   HIGH  
  
> [!NOTE]  
> Internally each importance setting is stored as a number that is used for calculations.  
  
 IMPORTANCE is local to the resource pool; workload groups of different importance inside the same resource pool affect each other, but do not affect workload groups in another resource pool.  
  
 REQUEST_MAX_MEMORY_GRANT_PERCENT = *value*     
 Specifies the maximum amount of memory that a single request can take from the pool. *value* is a percentage relative to the resource pool size specified by MAX_MEMORY_PERCENT.  

*value* is an integer up to [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] and a float starting with [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)]. Default value is 25. The allowed range for *value* is from 1 through 100.
  
> [!NOTE]  
> The amount specified only refers to query execution grant memory.  
  
> [!IMPORTANT]
> Setting *value* to 0 prevents queries with SORT and HASH JOIN operations in user-defined workload groups from running.     
>
> It is not recommended to set *value* greater than 70 because the server may be unable to set aside enough free memory if other concurrent queries are running. This may eventually lead to query time-out error 8645.      
  
> [!NOTE]  
> If the query memory requirements exceed the limit that is specified by this parameter, the server does the following:  
>   
> -  For user-defined workload groups, the server tries to reduce the query degree of parallelism until the memory requirement falls under the limit, or until the degree of parallelism equals 1. If the query memory requirement is still greater than the limit, error 8657 occurs.  
>   
> -  For internal and default workload groups, the server permits the query to obtain the required memory.  
>   
> Be aware that both cases are subject to time-out error 8645 if the server has insufficient physical memory.  
  
 REQUEST_MAX_CPU_TIME_SEC = *value*       
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
  
-   If the query is marked as serial (MAX_DOP = 1) at compile time, it cannot be changed back to parallel at run time regardless of the workload group or sp_configure setting.  
  
 After DOP is configured, it can only be lowered on grant memory pressure. Workload group reconfiguration is not visible while waiting in the grant memory queue.  
  
 GROUP_MAX_REQUESTS = *value*      
 Specifies the maximum number of simultaneous requests that are allowed to execute in the workload group. *value* must be 0 or a positive integer. The default setting for *value*, 0, allows unlimited requests. When the maximum concurrent requests are reached, a user in that group can log in, but is placed in a wait state until concurrent requests are dropped below the value specified.  
  
 USING { *pool_name* | "**default**" }      
 Associates the workload group with the user-defined resource pool identified by *pool_name*, which in effect puts the workload group in the resource pool. If *pool_name* is not provided or if the USING argument is not used, the workload group is put in the predefined Resource Governor default pool.  
  
 The option "default" must be enclosed by quotation marks ("") or brackets ([]) when used with ALTER WORKLOAD GROUP to avoid conflict with DEFAULT, which is a system reserved word. For more information, see [Database Identifiers](../../relational-databases/databases/database-identifiers.md).  
  
> [!NOTE]  
> The option "default" is case-sensitive.  
  
## Remarks  
 ALTER WORKLOAD GROUP is allowed on the default group.  
  
 Changes to the workload group configuration do not take effect until after ALTER RESOURCE GOVERNOR RECONFIGURE is executed. When changing a plan affecting setting, the new setting will only take effect in previously cached plans after executing DBCC FREEPROCCACHE (*pool_name*), where *pool_name* is the name of a Resource Governor resource pool on which the workload group is associated with.  
  
-   If changing MAX_DOP to 1, executing DBCC FREEPROCCACHE is not required because parallel plans can run in serial mode. However, it may not be as efficient as a plan compiled as a serial plan.  
  
-   If changing MAX_DOP from 1 to 0 or a value greater than 1, executing DBCC FREEPROCCACHE is not required. However, serial plans cannot run in parallel, so clearing the respective cache will allow new plans to potentially be compiled using parallelism.  
  
> [!CAUTION]  
> Clearing cached plans from a resource pool that is associated with more than one workload group will affect all workload groups with the user-defined resource pool identified by *pool_name*.  
  
 When executing DDL statements, we recommend that you be familiar with Resource Governor states. For more information, see [Resource Governor](../../relational-databases/resource-governor/resource-governor.md).  
  
 REQUEST_MEMORY_GRANT_PERCENT: In [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], index creation is allowed to use more workspace memory than initially granted for improved performance. This special handling is supported by Resource Governor in later versions, however, the initial grant and any additional memory grant are limited by resource pool and workload group settings.  
  
 **Index Creation on a Partitioned Table**  
  
 The memory consumed by index creation on non-aligned partitioned table is proportional to the number of partitions involved.  If the total required memory exceeds the per-query limit (REQUEST_MAX_MEMORY_GRANT_PERCENT) imposed by the Resource Governor workload group setting, this index creation may fail to execute. Because the "default" workload group allows a query to exceed the per-query limit with the minimum required memory to start for [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] compatibility, the user may be able to run the same index creation in "default" workload group, if the "default" resource pool has enough total memory configured to run such query.  
  
## Permissions  
 Requires `CONTROL SERVER` permission.  
  
## Examples  
 The following example shows how to change the importance of requests in the default group from `MEDIUM` to `LOW`.  
  
```sql  
ALTER WORKLOAD GROUP "default"  
WITH (IMPORTANCE = LOW);  
GO  
ALTER RESOURCE GOVERNOR RECONFIGURE;  
GO  
```  
  
 The following example shows how to move a workload group from the pool that it's in to the default pool.  
  
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
  
::: moniker-end
::: moniker range="=azuresqldb-mi-current||=sqlallproducts-allversions"

> ||||
> |---|---|---|
> |[SQL Server](alter-workload-group-transact-sql.md?view=sql-server-2017)||**_\* SQL Database<br />managed instance \*_** &nbsp;||[Azure Synapse<br />Analytics](alter-workload-group-transact-sql.md?view=azure-sqldw-latest)||||

&nbsp;

## SQL Database managed instance

[!INCLUDE [ALTER workload group](../../includes/alter-workload-group.md)]

::: moniker-end
::: moniker range="=azure-sqldw-latest||=sqlallproducts-allversions"

> ||||
> |---|---|---|
> |[SQL Server](alter-workload-group-transact-sql.md?view=sql-server-2017)||[SQL Database<br />managed instance](alter-workload-group-transact-sql.md?view=azuresqldb-mi-current)||**_\* Azure Synapse<br />Analytics \*_** &nbsp;||||

&nbsp;

## Azure Synapse Analytics

Alters an existing workload group.

See the ALTER WORKLOAD GROUP Behavior section in this doc for further details on how ALTER WORKLOAD GROUP behaves on a system with running and queued requests. 

Restrictions in place for CREATE WORKLOAD GROUP also apply to ALTER WORKLOAD GROUP.  Prior to modifying parameters, query sys.workload_management_workload_groups to ensure the values are within acceptable ranges.

## Syntax

```syntaxsql
ALTER WORKLOAD GROUP group_name
 WITH
 ([       MIN_PERCENTAGE_RESOURCE = value ]
  [ [ , ] CAP_PERCENTAGE_RESOURCE = value ]
  [ [ , ] REQUEST_MIN_RESOURCE_GRANT_PERCENT = value ]
  [ [ , ] REQUEST_MAX_RESOURCE_GRANT_PERCENT = value ] 
  [ [ , ] IMPORTANCE = { LOW | BELOW_NORMAL | NORMAL | ABOVE_NORMAL | HIGH }]
  [ [ , ] QUERY_EXECUTION_TIMEOUT_SEC = value ] )
  [ ; ]
  ```

## Arguments

group_name
Is the name of the existing user-defined workload group being altered.  group_name is not alterable. 

MIN_PERCENTAGE_RESOURCE = value 
value is an integer range from 0 to 100.  When altering min_percentage_resource, the sum of min_percentage_resource across all workload groups cannot exceed 100.  Altering min_percentage_resource requires all running queries to complete in the workload group before the command will complete.  See the ALTER WORKLOAD GROUP Behavior section in this doc for further details.

CAP_PERCENTAGE_RESOURCE = value
value is an integer range from 1 through 100.  The value for cap_percentage_resource must be greater than min_percentage_resource.  Altering cap_percentage_resource requires all running queries to complete in the workload group before the command will complete.  See the ALTER WORKLOAD GROUP Behavior section in this doc for further details. 

REQUEST_MIN_RESOURCE_GRANT_PERCENT = value
value is a decimal with a range between 0.75 to 100.00.  The value for request_min_resource_grant_percent needs to be a factor of min_percentage_resource and be less than cap_percentage_resource. 
  
REQUEST_MAX_RESOURCE_GRANT_PERCENT = value
value is a decimal and must be greater than request_min_resource_grant_percent

IMPORTANCE = { LOW |  BELOW_NORMAL | NORMAL | ABOVE_NORMAL | HIGH }
Alters the default importance of a request for the workload group.

QUERY_EXECUTION_TIMEOUT_SEC = value
Alters the maximum time, in seconds, that a query can execute before it is cancelled.  value must be 0 or a positive integer.  The default setting for value is 0, which means unlimited.   

## Permissions

Requires CONTROL DATABASE permission

## Example

The below example checks the values in the catalog view for wgDataLoads and changes the values.

```sql
SELECT *
FROM sys.workload_management_workload_groups  
WHERE [name] = 'wgDataLoads'

ALTER WORKLOAD GROUP wgDataLoads WITH
( MIN_PERCENTAGE_RESOURCE            = 40
 ,CAP_PERCENTAGE_RESOURCE            = 80
 ,REQUEST_MIN_RESOURCE_GRANT_PERCENT = 10 )
 ```

## ALTER WORKLOAD GROUP Behavior

At any point in time there are 3 types of requests in the system
- Requests which have not been classified yet
- Requests which are classified - and waiting - for object locks or system resources
- Requests which are classified - and running

Based on the properties of a workload group being altered, the timing of when the settings take effect will differ.

**Importance or Query_execution_timeout**
For the importance and query_execution_timeout properties, non-classified requests pick up the new config values.  Waiting and running requests execute with the old configuration.  The ALTER WORKLOAD GROUP request executes immediately regardless if there are running queries in the workload group.

**Request_min_resource_grant_percent or Request_max_resource_grant_percent**
For request_min_resource_grant_percent and request_max_resource_grant_percent, running requests execute with the old configuration.  Waiting requests and non-classified requests pick up the new config values.  The ALTER WORKLOAD GROUP request executes immediately regardless if there are running queries in the workload group.

**Min_percentage_resource or Cap_percentage_resource**
For min_percentage_resource and cap_percentage_resource, running requests execute with the old configuration.  Waiting requests and non-classified requests pick up the new config values. 

Changing min_percentage_resource and cap_percentage_resource requires draining of running requests in the workload group that is being altered.  When decreasing min_percentage_resource, the freed resources are returned to the share pool allowing requests from other workload groups the ability to utilize.  Conversely, increasing the min_percentage_resource will wait until requests utilizing only the needed resources from the shared pool to complete.  Alter workload group operation will have prioritized access to shared resources over other requests waiting to be executed on shared pool.  If the sum of min_percentage_resource exceeds 100%, the ALTER WORKLOAD GROUP request fails immediately. 

**Locking Behavior**
Altering a workload group requires a global lock across all workload groups.  A request to alter a workload group would queue behind already submitted create or drop workload group requests.  If a batch of alter statements is submitted at once, they are processed in the order in which they are submitted.  

## Next steps
::: moniker-end