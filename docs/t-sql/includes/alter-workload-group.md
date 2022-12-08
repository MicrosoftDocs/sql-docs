---
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/15/2022
ms.service: sql
ms.topic: include
---
Changes an existing Resource Governor workload group configuration, and optionally assigns it to a Resource Governor resource pool.

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../language-elements/transact-sql-syntax-conventions-transact-sql.md).

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

#### *group_name* | "default"

The name of an existing user-defined workload group or the Resource Governor default workload group. Resource Governor creates the "default" and internal groups when [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is installed.

The option "default" must be enclosed by quotation marks (`""`) or brackets (`[]`) when used with `ALTER WORKLOAD GROUP` to avoid conflict with DEFAULT, which is a system reserved word. For more information, see [Database Identifiers](../../relational-databases/databases/database-identifiers.md).

Predefined workload groups and resource pools all use lowercase names, such as "default". This should be taken into account for servers that use case-sensitive collation. Servers with case-insensitive collation, such as `SQL_Latin1_General_CP1_CI_AS`, will treat `"default"` and `"Default"` as the same.

#### IMPORTANCE = { LOW | MEDIUM | HIGH }

Specifies the relative importance of a request in the workload group. Importance is one of the following:

- LOW
- MEDIUM (default)
- HIGH

Internally, each importance setting is stored as a number that is used for calculations.

IMPORTANCE is local to the resource pool; workload groups of different importance inside the same resource pool affect each other, but don't affect workload groups in another resource pool.

#### REQUEST_MAX_MEMORY_GRANT_PERCENT = *value*

Specifies the maximum amount of memory that a single request can take from the pool. *value* is a percentage relative to the resource pool size specified by MAX_MEMORY_PERCENT. The default value is 25. The amount specified only refers to query execution grant memory.

*value* is an **int** up to [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and the allowed range is from 1 through 100. Starting with [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)], the value is a **float** data type and the allowed range is from 0 through 100.

> [!IMPORTANT]  
> Setting *value* to 0 prevents queries with SORT and HASH JOIN operations in user-defined workload groups from running.
>
> It is not recommended to set *value* greater than 70 because the server may be unable to set aside enough free memory if other concurrent queries are running. This may eventually lead to query time-out error 8645.

If the query memory requirements exceed the limit that is specified by this parameter, the server does the following:

- For user-defined workload groups, the server tries to reduce the query degree of parallelism until the memory requirement falls under the limit, or until the degree of parallelism equals 1. If the query memory requirement is still greater than the limit, error 8657 occurs.
- For internal and default workload groups, the server permits the query to obtain the required memory.

Both cases are subject to [time-out error 8645](../../relational-databases/errors-events/mssqlserver-8645-database-engine-error.md) if the server has insufficient physical memory.

#### REQUEST_MAX_CPU_TIME_SEC = *value*

Specifies the maximum amount of CPU time, in seconds, that a request can use. *value* must be 0 or a positive integer. The default setting for *value* is 0, which means unlimited. By default, Resource Governor won't prevent a request from continuing if the maximum time is exceeded. However, an event will be generated. For more information, see [CPU Threshold Exceeded Event Class](../../relational-databases/event-classes/cpu-threshold-exceeded-event-class.md).

Starting with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] SP2 and [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] CU3, and using [trace flag 2422](../database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf2422), Resource Governor will abort a request when the maximum time is exceeded.

#### REQUEST_MEMORY_GRANT_TIMEOUT_SEC = *value*

Specifies the maximum time, in seconds, that a query can wait for memory grant (work buffer memory) to become available.

A query doesn't always fail when memory grant time-out is reached. A query will only fail if there are too many concurrent queries running. Otherwise, the query may only get the minimum memory grant, resulting in reduced query performance.

*value* must be a positive integer. The default setting for *value*, 0, uses an internal calculation based on query cost to determine the maximum time.

#### MAX_DOP = *value*

Specifies the maximum degree of parallelism (DOP) for parallel requests. *value* must be 0 or a positive integer, 1 though 255. When *value* is 0, the server chooses the max degree of parallelism. This is the default and recommended setting.

The actual value that the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] sets for MAX_DOP by might be less than the specified value. The final value is determined by the formula min(255, *number of CPUs)*.

> [!CAUTION]  
> Changing MAX_DOP can adversely affect a server's performance. If you must change MAX_DOP, we recommend that it be set to a value that is less than or equal to the maximum number of hardware schedulers that are present in a single NUMA node. We recommend that you do not set MAX_DOP to a value greater than 8.

MAX_DOP is handled as follows:

- MAX_DOP as a query hint is honored as long as it doesn't exceed workload group MAX_DOP.

- MAX_DOP as a query hint always overrides sp_configure 'max degree of parallelism'.

- Workload group MAX_DOP overrides sp_configure 'max degree of parallelism'.

- If the query is marked as serial `(MAX_DOP = 1)` at compile time, it can't be changed back to parallel at run time regardless of the workload group or sp_configure setting.

After DOP is configured, it can only be lowered on grant memory pressure. Workload group reconfiguration isn't visible while waiting in the grant memory queue.

#### GROUP_MAX_REQUESTS = *value*

Specifies the maximum number of simultaneous requests that are allowed to execute in the workload group. *value* must be 0 or a positive integer. The default setting for *value*, 0, allows unlimited requests. When the maximum concurrent requests are reached, a user in that group can sign in, but is placed in a wait state until concurrent requests are dropped below the value specified.

#### USING { *pool_name* | "default" }

Associates the workload group with the user-defined resource pool identified by *pool_name*, which in effect puts the workload group in the resource pool. If *pool_name* isn't provided or if the USING argument isn't used, the workload group is put in the predefined Resource Governor default pool.

The option "default" is case-sensitive and must be enclosed by quotation marks (`""`) or brackets (`[]`) when used with `ALTER WORKLOAD GROUP` to avoid conflict with DEFAULT, which is a system reserved word. For more information, see [Database Identifiers](../../relational-databases/databases/database-identifiers.md).

## Remarks

`ALTER WORKLOAD GROUP` is allowed on the default group.

Changes to the workload group configuration don't take effect until after `ALTER RESOURCE GOVERNOR RECONFIGURE` is executed. When changing a plan affecting setting, the new setting will only take effect in previously cached plans after executing `DBCC FREEPROCCACHE (*pool_name*)`, where *pool_name* is the name of a Resource Governor resource pool on which the workload group is associated with.

- If changing MAX_DOP to 1, executing `DBCC FREEPROCCACHE` isn't required because parallel plans can run in serial mode. However, it may not be as efficient as a plan compiled as a serial plan.

- If changing MAX_DOP from 1 to 0 or a value greater than 1, executing `DBCC FREEPROCCACHE` isn't required. However, serial plans can't run in parallel, so clearing the respective cache will allow new plans to potentially be compiled using parallelism.

> [!CAUTION]  
> Clearing cached plans from a resource pool that is associated with more than one workload group will affect all workload groups with the user-defined resource pool identified by *pool_name*.

When executing DDL statements, you should be familiar with Resource Governor states. For more information, see [Resource Governor](../../relational-databases/resource-governor/resource-governor.md).

`REQUEST_MEMORY_GRANT_PERCENT`: In [!INCLUDE [ssversion2005-md](../../includes/ssversion2005-md.md)], index creation is allowed to use more workspace memory than initially granted for improved performance. This special handling is supported by Resource Governor in later versions, however, the initial grant and any additional memory grant are limited by resource pool and workload group settings.

#### Index creation on a partitioned table

The memory consumed by index creation on non-aligned partitioned table is proportional to the number of partitions involved.  If the total required memory exceeds the per-query limit (`REQUEST_MAX_MEMORY_GRANT_PERCENT`) imposed by the Resource Governor workload group setting, this index creation may fail to execute. Because the "default" workload group allows a query to exceed the per-query limit with the minimum required memory to start for [!INCLUDE [ssversion2005-md](../../includes/ssversion2005-md.md)] compatibility, the user may be able to run the same index creation in "default" workload group, if the "default" resource pool has enough total memory configured to run such query.

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

The following example shows how to move a workload group from the pool that it's in, to the default pool.

```sql
ALTER WORKLOAD GROUP adHoc
USING [default];
GO
ALTER RESOURCE GOVERNOR RECONFIGURE;
GO
```

## See also

- [Resource Governor](../../relational-databases/resource-governor/resource-governor.md)
- [CREATE WORKLOAD GROUP (Transact-SQL)](../statements/create-workload-group-transact-sql.md)
- [CREATE WORKLOAD GROUP (Transact-SQL)](../statements/create-workload-group-transact-sql.md)
- [DROP WORKLOAD GROUP (Transact-SQL)](../statements/drop-workload-group-transact-sql.md)
- [CREATE RESOURCE POOL (Transact-SQL)](../statements/create-resource-pool-transact-sql.md)
- [ALTER RESOURCE POOL (Transact-SQL)](../statements/alter-resource-pool-transact-sql.md)
- [DROP RESOURCE POOL (Transact-SQL)](../statements/drop-resource-pool-transact-sql.md)
- [ALTER RESOURCE GOVERNOR (Transact-SQL)](../statements/alter-resource-governor-transact-sql.md)
