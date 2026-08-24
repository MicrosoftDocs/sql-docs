---
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dfurman
ms.date: 11/20/2025
ms.service: sql
ms.topic: include
ms.custom:
  - build-2025
---

Changes an existing resource governor workload group configuration, and optionally assigns it to a different resource governor resource pool.

> [!NOTE]
> To modify resource governor configuration in [!INCLUDE [ssazuremi-md.md](../../includes/ssazuremi-md.md)], you must be in the context of the `master` database on the primary replica.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../language-elements/transact-sql-syntax-conventions-transact-sql.md).

## Syntax

```syntaxsql
ALTER WORKLOAD GROUP { group_name | [ default ] }
[ WITH
    ( [ IMPORTANCE = { LOW | MEDIUM | HIGH } ]
      [ [ , ] REQUEST_MAX_MEMORY_GRANT_PERCENT = value ]
      [ [ , ] REQUEST_MAX_CPU_TIME_SEC = value ]
      [ [ , ] REQUEST_MEMORY_GRANT_TIMEOUT_SEC = value ]
      [ [ , ] MAX_DOP = value ]
      [ [ , ] GROUP_MAX_REQUESTS = value ]
      [ [ , ] GROUP_MAX_TEMPDB_DATA_MB = value ]
      [ [ , ] GROUP_MAX_TEMPDB_DATA_PERCENT = value ] )
]
[ USING { pool_name | [default] } ]
[ ; ]
```

## Arguments

#### *group_name* | [default]

The name of an existing user-defined workload group or the resource governor built-in `default` workload group.

`default` must be in brackets (`[]`) or quotation marks (`""`) when used with `ALTER WORKLOAD GROUP` to avoid a conflict with `DEFAULT`, which is a system reserved word. For more information, see [Database identifiers](../../relational-databases/databases/database-identifiers.md).

Built-in resource pools and workload groups use all lowercase names, such as `default`. Use the lower case `default` on servers that use a case-sensitive collation. Servers with case-insensitive collation treat `default`, `Default`, and `DEFAULT` as the same value.

#### IMPORTANCE = { LOW | MEDIUM | HIGH }

Specifies the relative importance of a request in the workload group. The default value is `MEDIUM`.

`IMPORTANCE` is local to the resource pool that contains the workload group. Workload groups of different importance inside the same resource pool affect each other, but don't affect workload groups in other resource pools.

#### REQUEST_MAX_MEMORY_GRANT_PERCENT = *value*

Specifies the maximum amount of query workspace memory that a single request can take from the pool. *value* is a percentage of the resource pool size defined by `MAX_MEMORY_PERCENT`. Default value is 25.

In [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and older, *value* is an integer and the allowed range is from 1 through 100.

Starting with [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)], the value can be fractional using the `float` data type. The allowed range is from 0 through 100.

> [!IMPORTANT]
> The amount specified only refers to query workspace memory obtained via query memory grants.
>
> It is not recommended to set *value* too large (for example, greater than 70) because the server may be unable to set aside enough free memory for other concurrent queries. This can lead to a memory grant time out [error 8645](../../relational-databases/errors-events/mssqlserver-8645-database-engine-error.md).
>
> Setting *value* to 0 or a small value might prevent queries with operators that require workspace memory, such as `sort` and `hash`, from running in user-defined workload groups. If the query memory requirements exceed the limit defined by this parameter, the following behavior occurs:
>
> - For user-defined workload groups, the server tries to reduce the degree of parallelism (DOP) of the request (query) until the memory requirement falls under the limit, or until DOP equals 1. If the query memory requirement is still greater than the limit, error 8657 occurs and the query fails.
> - For the `internal` and `default` workload groups, the server permits the query to obtain the required memory.
>
> In either case, [error 8645](../../relational-databases/errors-events/mssqlserver-8645-database-engine-error.md) might occur if the server has insufficient physical memory.

#### REQUEST_MAX_CPU_TIME_SEC = *value*

Specifies the maximum amount of CPU time, in seconds, that a batch request can use. *value* must be 0 or a positive integer. The default setting for *value* is 0, which means unlimited.

When the maximum CPU time is exceeded, the `cpu_threshold_exceeded` extended event and a trace event are generated. For more information, see [CPU threshold exceeded event class](../../relational-databases/event-classes/cpu-threshold-exceeded-event-class.md).

In Azure SQL Managed Instance, when the maximum CPU time is exceeded, resource governor aborts the request with error 10961.

In [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], resource governor doesn't abort the request by default. However, starting with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] SP2 and [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] CU3, resource governor aborts a request with error 10961 when [trace flag 2422](../database-console-commands/dbcc-traceon-trace-flags-transact-sql.md#tf2422) is enabled and the maximum CPU time is exceeded.

#### REQUEST_MEMORY_GRANT_TIMEOUT_SEC = *value*

Specifies the maximum time, in seconds, that a query can wait for a memory grant from the query workspace memory to become available. *value* must be 0 or a positive integer. The default setting for *value*, 0, uses an internal calculation based on query cost to determine the maximum time.

A query doesn't always fail when a memory grant time out is reached. A query only fails if there are too many concurrent queries running. Otherwise, the query might only get the minimum memory grant, resulting in reduced query performance.

#### MAX_DOP = *value*

Specifies the maximum degree of parallelism (`MAXDOP`) for parallel query execution. The allowed range for *value* is from 0 through 64. The default setting for *value*, 0, uses the global setting.

For more information, see [MAXDOP](#maxdop).

#### GROUP_MAX_REQUESTS = *value*

Specifies the maximum number of simultaneous requests that are allowed to execute in the workload group. *value* must be 0 or a positive integer. The default setting for *value* is 0, and allows unlimited requests. When the maximum concurrent requests are reached, a session in that group can be created, but is placed in a wait state until the number of concurrent requests drops below the value specified.

#### GROUP_MAX_TEMPDB_DATA_MB = *value*

Specifies the maximum amount of space that a workload group can consume in the `tempdb` data files, in megabytes. *value* must be 0, a positive number, or NULL. Fractional values are allowed.

When the value is 0, `tempdb` space allocations by sessions in the workload group are not allowed. When a value isn't set, resource governor doesn't limit `tempdb` space consumption by the workload group.

The limit is for the total space consumed in `tempdb` by all sessions in a workload group.

When a request running in a workload group attempts to increase `tempdb` data space consumption by the workload group above the limit set by `GROUP_MAX_TEMPDB_DATA_MB`, resource governor aborts the request with error 1138. For more information, see [Tempdb space resource governance](../../relational-databases/resource-governor/tempdb-space-resource-governance.md).

#### GROUP_MAX_TEMPDB_DATA_PERCENT = *value*

Specifies the maximum amount of space that a workload group can consume in the `tempdb` data files, in percent of the maximum `tempdb` size. For the definition of the maximum `tempdb` size, see [Percent limit configuration](../../relational-databases/resource-governor/tempdb-space-resource-governance.md#percent-limit-configuration). *value* must be in the range from 0 to 100, or NULL. Fractional values are allowed.

When the value is 0, `tempdb` space allocations by sessions in the workload group are not allowed. When a value isn't set, resource governor doesn't limit `tempdb` space consumption by the workload group. When `GROUP_MAX_TEMPDB_DATA_MB` is set, or when `tempdb` maximum size isn't defined, `GROUP_MAX_TEMPDB_DATA_PERCENT` has no effect.

The limit is for the total space consumed in `tempdb` by all sessions in a workload group.

When a request running in a workload group attempts to increase `tempdb` data space consumption by the workload group above the limit set by `GROUP_MAX_TEMPDB_DATA_PERCENT`, resource governor aborts the request with error 1138. For more information, see [Tempdb space resource governance](../../relational-databases/resource-governor/tempdb-space-resource-governance.md).

#### USING { *pool_name* | [ default ] }

Associates the workload group with the user-defined resource pool identified by *pool_name*, or with the `default` resource pool. If *pool_name* isn't provided, or if the `USING` argument isn't specified, the workload group is associated with the built-in `default` pool.

`default` is a reserved word and when specified in `USING`, must be enclosed in brackets (`[]`) or quotation marks (`""`).

Built-in resource pools and workload groups use all lowercase names, such as `default`. Use the lower case `default` on servers that use a case-sensitive collation. Servers with case-insensitive collation treat `default`, `Default`, and `DEFAULT` as the same value.

## Remarks

`ALTER WORKLOAD GROUP` is allowed on the `default` workload group, but not on the `internal` group.

Changes to the workload group configuration don't take effect until after `ALTER RESOURCE GOVERNOR RECONFIGURE` is executed.

For more information, see [Resource governor](../../relational-databases/resource-governor/resource-governor.md) and [Resource governor workload group](../../relational-databases/resource-governor/resource-governor-workload-group.md).

### MAXDOP

For a given query, effective `MAXDOP` is determined as follows:

- `MAXDOP` as a query hint is honored as long as it doesn't exceed the workload group `MAX_DOP` setting.
- `MAXDOP` as a query hint always overrides the `max degree of parallelism` server configuration. For more information, see [Server configuration: max degree of parallelism](../../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md).
- Workload group `MAX_DOP` overrides the `max degree of parallelism` server configuration and the `MAXDOP` [database scoped configuration](../statements/alter-database-scoped-configuration-transact-sql.md).

The `MAXDOP` limit is set per [task](../../relational-databases/system-dynamic-management-views/sys-dm-os-tasks-transact-sql.md). It isn't a per [request](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md) or per query limit. During an execution of a parallel query, a single request can spawn multiple tasks that are assigned to a [scheduler](../../relational-databases/system-dynamic-management-views/sys-dm-os-tasks-transact-sql.md). For more information, see the [Thread and task architecture guide](../../relational-databases/thread-and-task-architecture-guide.md).

When a query is marked as serial at compile time (`MAXDOP = 1`), it can't execute with parallelism at run time regardless of the workload group or server configuration setting. After `MAXDOP` is determined for a query, it can only be lowered due to memory pressure. Workload group reconfiguration does not affect queries waiting in the memory grant queue.

### Cached plans

When you change a plan affecting setting such as `MAX_DOP`, the new setting takes effect in previously cached plans only after executing `DBCC FREEPROCCACHE (<pool_name>)`, where `<pool_name>` is the name of a resource governor resource pool used by the current workload group.

- If changing `MAX_DOP` to 1, executing `DBCC FREEPROCCACHE` isn't required because parallel plans can run in serial mode. However, such a plan might be less efficient than a plan compiled as a serial plan.
- If changing `MAX_DOP` from 1 to 0 or a value greater than 1, executing `DBCC FREEPROCCACHE` isn't required. However, serial plans can't run in parallel, so clearing the respective cache allows new plans to potentially be compiled using parallelism.

> [!WARNING]
> Clearing cached plans from a resource pool that is associated with more than one workload group affects all workload groups using the user-defined resource pool identified by `<pool_name>`.

### Index creation

For performance reasons, index creation is allowed to use more memory workspace than initially granted. Resource governor supports this special handling. However, the initial grant and any additional memory grants are limited by the workload group and resource pool settings.

The memory consumed to create a nonaligned index on a partitioned table is proportional to the number of partitions involved. If the total required memory exceeds the per-query limit enforced by the `REQUEST_MAX_MEMORY_GRANT_PERCENT` workload group setting, index creation might fail. Because the `default` workload group allows a query to exceed the per-query limit with the minimum required memory to start for backward compatibility, you might be able to create the same index using the `default` workload group if the `default` resource pool has enough total memory.

## Permissions

Requires the `CONTROL SERVER` permission.

## Examples

The following example shows how to change the importance of requests in the default group from `MEDIUM` to `LOW`.

```sql
ALTER WORKLOAD GROUP [default]
WITH (IMPORTANCE = LOW);

ALTER RESOURCE GOVERNOR RECONFIGURE;
```

The following example shows how to move a workload group from the pool that it's currently into the `default` pool.

```sql
ALTER WORKLOAD GROUP adHoc
USING [default];

ALTER RESOURCE GOVERNOR RECONFIGURE;
```

## Related content

- [Resource governor](../../relational-databases/resource-governor/resource-governor.md)
- [Resource governor workload group](../../relational-databases/resource-governor/resource-governor-workload-group.md)
- [Create a workload group](../../relational-databases/resource-governor/create-a-workload-group.md)
- [Move a workload group](../../relational-databases/resource-governor/move-a-workload-group.md)
- [CREATE WORKLOAD GROUP (Transact-SQL)](../statements/create-workload-group-transact-sql.md)
- [DROP WORKLOAD GROUP (Transact-SQL)](../statements/drop-workload-group-transact-sql.md)
- [CREATE RESOURCE POOL (Transact-SQL)](../statements/create-resource-pool-transact-sql.md)
- [ALTER RESOURCE POOL (Transact-SQL)](../statements/alter-resource-pool-transact-sql.md)
- [DROP RESOURCE POOL (Transact-SQL)](../statements/drop-resource-pool-transact-sql.md)
- [ALTER RESOURCE GOVERNOR (Transact-SQL)](../statements/alter-resource-governor-transact-sql.md)
- [sys.resource_governor_workload_groups (Transact-SQL)](../../relational-databases/system-catalog-views/sys-resource-governor-workload-groups-transact-sql.md)
- [sys.dm_resource_governor_workload_groups](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-workload-groups-transact-sql.md)
