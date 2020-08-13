Creates a Resource Governor workload group and associates the workload group with a Resource Governor resource pool. Resource Governor is not available in every edition of [!INCLUDE[msCoName](msconame-md.md)][!INCLUDE[ssNoVersion](ssnoversion-md.md)]. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).

![Topic link icon](../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).

## Syntax

```syntaxsql
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

*group_name*</br>
Is the user-defined name for the workload group. *group_name* is alphanumeric, can be up to 128 characters, must be unique within an instance of [!INCLUDE[ssNoVersion](ssnoversion-md.md)], and must comply with the rules for [identifiers](../relational-databases/databases/database-identifiers.md).

IMPORTANCE = { LOW | **MEDIUM** | HIGH }</br>
Specifies the relative importance of a request in the workload group. Importance is one of the following, with MEDIUM being the default:

- LOW
- MEDIUM (default)
- HIGH

> [!NOTE]
> Internally each importance setting is stored as a number that is used for calculations.

IMPORTANCE is local to the resource pool; workload groups of different importance inside the same resource pool affect each other, but do not affect workload groups in another resource pool.

REQUEST_MAX_MEMORY_GRANT_PERCENT = *value*</br>
Specifies the maximum amount of memory that a single request can take from the pool. *value* is a percentage relative to the resource pool size specified by MAX_MEMORY_PERCENT.

*value* is an integer up to [!INCLUDE[ssSQL17](sssql17-md.md)] and a float starting with [!INCLUDE[sql-server-2019](sssqlv15-md.md)] and in Azure SQL Managed Instance. Default value is 25. The allowed range for *value* is from 1 through 100.

> [!IMPORTANT]  
> The amount specified only refers to query execution grant memory.
>
> Setting *value* to 0 prevents queries with SORT and HASH JOIN operations in user-defined workload groups from running.
>
> It is not recommended to set *value* greater than 70 because the server may be unable to set aside enough free memory if other concurrent queries are running. This may eventually lead to query time-out error 8645.
>
> If the query memory requirements exceed the limit that is specified by this parameter, the server does the following:
>
> - For user-defined workload groups, the server tries to reduce the query degree of parallelism until the memory requirement falls under the limit, or until the degree of parallelism equals 1. If the query memory requirement is still greater than the limit, error 8657 occurs.
>
> - For internal and default workload groups, the server permits the query to obtain the required memory.
>
> Be aware that both cases are subject to time-out error 8645 if the server has insufficient physical memory.

REQUEST_MAX_CPU_TIME_SEC = *value*</br>
Specifies the maximum amount of CPU time, in seconds, that a request can use. *value* must be 0 or a positive integer. The default setting for *value* is 0, which means unlimited.

> [!NOTE]
> By default, Resource Governor will not prevent a request from continuing if the maximum time is exceeded. However, an event will be generated. For more information, see [CPU Threshold Exceeded Event Class](../relational-databases/event-classes/cpu-threshold-exceeded-event-class.md).
> [!IMPORTANT]
> Starting with [!INCLUDE[ssSQL15](sssql15-md.md)] SP2 and [!INCLUDE[ssSQL17](sssql17-md.md)] CU3, and using [trace flag 2422](../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md), Resource Governor will abort a request when the maximum time is exceeded.

REQUEST_MEMORY_GRANT_TIMEOUT_SEC = *value*</br>
Specifies the maximum time, in seconds, that a query can wait for a memory grant (work buffer memory) to become available. *value* must be 0 or a positive integer. The default setting for *value*, 0, uses an internal calculation based on query cost to determine the maximum time.

> [!NOTE]
> A query does not always fail when memory grant time-out is reached. A query will only fail if there are too many concurrent queries running. Otherwise, the query may only get the minimum memory grant, resulting in reduced query performance.

MAX_DOP = *value*</br>
Specifies the **maximum degree of parallelism (MAXDOP)** for parallel query execution. *value* must be 0 or a positive integer. The allowed range for *value* is from 0 through 64. The default setting for *value*, 0, uses the global setting. MAX_DOP is handled as follows:

> [!NOTE]
> Workload group MAX_DOP overrides the [server configuration for max degree of parallelism](../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md) and the **MAXDOP** [database scoped configuration](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md).

> [!TIP]
> To accomplish this at the query level, use the **MAXDOP** [query hint](../t-sql/queries/hints-transact-sql-query.md). Setting the maximum degree of parallelism as a query hint is effective as long as it does not exceed the workload group MAX_DOP. If the MAXDOP query hint value exceeds the value that is configured by using the Resource Governor, the [!INCLUDE[ssDEnoversion](ssdenoversion-md.md)] uses the Resource Governor `MAX_DOP` value. The MAXDOP [query hint](../t-sql/queries/hints-transact-sql-query.md) always overrides the [server configuration for max degree of parallelism](../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md).
>
> To accomplish this at the database level, use the **MAXDOP** [database scoped configuration](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md).
>
> To accomplish this at the server level, use the **max degree of parallelism (MAXDOP)** [server configuration option](../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md).

GROUP_MAX_REQUESTS = *value*</br>
Specifies the maximum number of simultaneous requests that are allowed to execute in the workload group. *value* must be a 0 or a positive integer. The default setting for *value* is 0, and allows unlimited requests. When the maximum concurrent requests are reached, a user in that group can log in, but is placed in a wait state until concurrent requests are dropped below the value specified.

USING { *pool_name* | **"default"** }</br>
Associates the workload group with the user-defined resource pool identified by *pool_name*. This in effect puts the workload group in the resource pool. If *pool_name* is not provided, or if the USING argument is not used, the workload group is put in the predefined Resource Governor default pool.

"default" is a reserved word and when used with USING, must be enclosed by quotation marks ("") or brackets ([]).

> [!NOTE]
> Predefined workload groups and resource pools all use lower case names, such as "default". This should be taken into account for servers that use case-sensitive collation. Servers with case-insensitive collation, such as SQL_Latin1_General_CP1_CI_AS, will treat "default" and "Default" as the same.

EXTERNAL external_pool_name | "default"</br>
**Applies to**: [!INCLUDE[ssNoVersion](ssnoversion-md.md)] (starting with [!INCLUDE[ssSQL15](sssql15-md.md)]).

Workload group can specify an external resource pool. You can define a workload group and associate with two pools:

- A resource pool for [!INCLUDE[ssNoVersion](ssnoversion-md.md)] workloads and queries
- An external resource pool for external processes. For more information, see [sp_execute_external_script &#40;Transact-SQL&#41;](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

## Remarks

When `REQUEST_MEMORY_GRANT_PERCENT` is used, index creation is allowed to use more workspace memory than what is initially granted for improved performance. This special handling is supported by Resource Governor in [!INCLUDE[ssCurrent](sscurrent-md.md)]. However, the initial grant and any additional memory grant are limited by resource pool and workload group settings.

The `MAX_DOP` limit is set per [task](../relational-databases/system-dynamic-management-views/sys-dm-os-tasks-transact-sql.md). It is not a per [request](../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md) or per query limit. This means that during a parallel query execution, a single request can spawn multiple tasks which are assigned to a [scheduler](../relational-databases/system-dynamic-management-views/sys-dm-os-tasks-transact-sql.md). For more information, see the [Thread and Task Architecture Guide](../relational-databases/thread-and-task-architecture-guide.md).

When `MAX_DOP` is used and a query is marked as serial at compile time, it cannot be changed back to parallel at run time regardless of the workload group or server configuration setting. After `MAX_DOP` is configured, it can only be lowered due to memory pressure. Workload group reconfiguration is not visible while waiting in the grant memory queue.

### Index Creation on a Partitioned Table

The memory consumed by index creation on non-aligned partitioned table is proportional to the number of partitions involved. If the total required memory exceeds the per-query limit `REQUEST_MAX_MEMORY_GRANT_PERCENT` imposed by the Resource Governor workload group setting, this index creation may fail to execute. Because the *"default"* workload group allows a query to exceed the per-query limit with the minimum required memory, the user may be able to run the same index creation in *"default"* workload group, if the *"default"* resource pool has enough total memory configured to run such query.

## Permissions

Requires `CONTROL SERVER` permission.

## Example

Create a workload group named `newReports` which uses the Resource Governor default settings, and is in the Resource Governor default pool. The example specifies the `default` pool, but this is not required.

```sql
CREATE WORKLOAD GROUP newReports
WITH
    (REQUEST_MAX_MEMORY_GRANT_PERCENT = 2.5
      , REQUEST_MAX_CPU_TIME_SEC = 100
      , MAX_DOP = 4)    
USING "default" ;
GO
```

## See Also

- [ALTER WORKLOAD GROUP &#40;Transact-SQL&#41;](../t-sql/statements/alter-workload-group-transact-sql.md)
- [DROP WORKLOAD GROUP &#40;Transact-SQL&#41;](../t-sql/statements/drop-workload-group-transact-sql.md)
- [CREATE RESOURCE POOL &#40;Transact-SQL&#41;](../t-sql/statements/create-resource-pool-transact-sql.md)
- [ALTER RESOURCE POOL &#40;Transact-SQL&#41;](../t-sql/statements/alter-resource-pool-transact-sql.md)
- [DROP RESOURCE POOL &#40;Transact-SQL&#41;](../t-sql/statements/drop-resource-pool-transact-sql.md)
- [ALTER RESOURCE GOVERNOR &#40;Transact-SQL&#41;](../t-sql/statements/alter-resource-governor-transact-sql.md)