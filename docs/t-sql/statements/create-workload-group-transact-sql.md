---
title: "CREATE WORKLOAD GROUP (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/04/2018"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "WORKLOAD GROUP"
  - "WORKLOAD_GROUP_TSQL"
  - "CREATE WORKLOAD GROUP"
  - "CREATE_WORKLOAD_GROUP_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CREATE WORKLOAD GROUP statement"
ms.assetid: d949e540-9517-4bca-8117-ad8358848baa
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# CREATE WORKLOAD GROUP (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

Creates a Resource Governor workload group and associates the workload group with a Resource Governor resource pool. Resource Governor is not available in every edition of [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).  
  
## Syntax  
```  
CREATE WORKLOAD GROUP group_name  
[ WITH  
    ( [ IMPORTANCE = { LOW | MEDIUM | HIGH } ]  
      [ [ , ] REQUEST_MAX_MEMORY_GRANT_PERCENT = value ]  
      [ [ , ] REQUEST_MAX_CPU_TIME_SEC = value ]  
      [ [ , ] REQUEST_MEMORY_GRANT_TIMEOUT_SEC = value ]   
      [ [ , ] MAX_DOP = value ]  
      [ [ , ] GROUP_MAX_REQUESTS = value ] )  
 ]  
[ USING {   
    [ pool_name | "default" ]    
    [ [ , ] EXTERNAL external_pool_name | "default" ] ]   
    } ]  
[ ; ]  
```  
  
## Arguments  
*group_name*  
Is the user-defined name for the workload group. *group_name* is alphanumeric, can be up to 128 characters, must be unique within an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md).  
  
IMPORTANCE = { LOW | **MEDIUM** | HIGH }  
Specifies the relative importance of a request in the workload group. Importance is one of the following, with MEDIUM being the default:  

-   LOW  
-   MEDIUM (default)    
-   HIGH  
  
> [!NOTE]  
> Internally each importance setting is stored as a number that is used for calculations.  
  
IMPORTANCE is local to the resource pool; workload groups of different importance inside the same resource pool affect each other, but do not affect workload groups in another resource pool.  
  
REQUEST_MAX_MEMORY_GRANT_PERCENT = *value*  
Specifies the maximum amount of memory that a single request can take from the pool. *value* is a percentage relative to the resource pool size specified by MAX_MEMORY_PERCENT. *value* is an integer up to [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] and afloat starting with  [!INCLUDE[sql-server-2019](../../includes/sssqlv15-md.md)], with a default setting of 25. The allowed range for *value* is from 1 through 100.  
  
> [!NOTE]  
> The amount specified only refers to query execution grant memory.  
  
Note the following:  
 
-   Setting *value* to 0 prevents queries with SORT and HASH MATCH operations in user-defined workload groups from running.  
  
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
Specifies the maximum time, in seconds, that a query can wait for a memory grant (work buffer memory) to become available. *value* must be 0 or a positive integer. The default setting for *value*, 0, uses an internal calculation based on query cost to determine the maximum time.    
  
> [!NOTE]  
>  A query does not always fail when memory grant time-out is reached. A query will only fail if there are too many concurrent queries running. Otherwise, the query may only get the minimum memory grant, resulting in reduced query performance.  
  
MAX_DOP =*value*  
Specifies the maximum degree of parallelism (DOP) for parallel requests. *value* must be 0 or a positive integer. The allowed range for *value* is from 0 through 64. The default setting for *value*, 0, uses the global setting. MAX_DOP is handled as follows:  
  
-   MAX_DOP as a query hint is effective as long as it does not exceed workload group MAX_DOP. If the MAXDOP query hint value exceeds the value that is configured by using the Resource Governor, the Database Engine uses the Resource Governor MAXDOP value.  
  
-   MAX_DOP as a query hint always overrides sp_configure 'max degree of parallelism'.  
  
-   Workload group MAX_DOP overrides sp_configure 'max degree of parallelism'.  
  
-   If the query is marked as serial at compile time, it cannot be changed back to parallel at run time regardless of the workload group or sp_configure setting.  
  
-   After DOP is configured, it can only be lowered on grant memory pressure. Workload group reconfiguration is not visible while waiting in the grant memory queue.  
  
GROUP_MAX_REQUESTS =*value*  
Specifies the maximum number of simultaneous requests that are allowed to execute in the workload group. *value* must be a 0 or a positive integer. The default setting for *value* is 0, and allows unlimited requests. When the maximum concurrent requests are reached, a user in that group can log in, but is placed in a wait state until concurrent requests are dropped below the value specified.  
  
USING { *pool_name* | **"default"** }  
Associates the workload group with the user-defined resource pool identified by *pool_name*. This in effect puts the workload group in the resource pool. If *pool_name* is not provided, or if the USING argument is not used, the workload group is put in the predefined Resource Governor default pool.  
  
"default" is a reserved word and when used with USING, must be enclosed by quotation marks ("") or brackets ([]).  
  
> [!NOTE]  
> Predefined workload groups and resource pools all use lower case names, such as "default". This should be taken into account for servers that use case-sensitive collation. Servers with case-insensitive collation, such as SQL_Latin1_General_CP1_CI_AS, will treat "default" and "Default" as the same.  
  
EXTERNAL external_pool_name | "default"  
**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ( [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]).  
  
Workload group can specify an external resource pool. You can define a workload group and associate with 2 pools:  
  
-   A resource pool for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] workloads and queries  
  
-   An external resource pool for external processes. For more information, see [sp_execute_external_script &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).  
  
## Remarks  
When `REQUEST_MEMORY_GRANT_PERCENT` is used, index creation is allowed to use more workspace memory than what is initially granted for improved performance. This special handling is supported by Resource Governor in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. However, the initial grant and any additional memory grant are limited by resource pool and workload group settings.  
  
**Index Creation on a Partitioned Table**  
The memory consumed by index creation on non-aligned partitioned table is proportional to the number of partitions involved. If the total required memory exceeds the per-query limit `REQUEST_MAX_MEMORY_GRANT_PERCENT` imposed by the Resource Governor workload group setting, this index creation may fail to execute. Because the *"default"* workload group allows a query to exceed the per-query limit with the minimum required memory, the user may be able to run the same index creation in *"default"* workload group, if the *"default"* resource pool has enough total memory configured to run such query.  
  
## Permissions  
Requires `CONTROL SERVER` permission.  
  
## Examples  
### 1. Shows how to create a workload group named newReports    
It uses the Resource Governor default settings and is in the Resource Governor default pool. The example specifies the `default` pool, but this is not required.  
  
```sql  
CREATE WORKLOAD GROUP newReports  
    USING "default" ;  
GO  
```  
  
## See Also  
 [ALTER WORKLOAD GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/alter-workload-group-transact-sql.md)   
 [DROP WORKLOAD GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/drop-workload-group-transact-sql.md)   
 [CREATE RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/create-resource-pool-transact-sql.md)   
 [ALTER RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/alter-resource-pool-transact-sql.md)   
 [DROP RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/drop-resource-pool-transact-sql.md)   
 [ALTER RESOURCE GOVERNOR &#40;Transact-SQL&#41;](../../t-sql/statements/alter-resource-governor-transact-sql.md)  
  
