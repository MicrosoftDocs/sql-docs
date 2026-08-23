---
title: "ALTER RESOURCE POOL (Transact-SQL)"
description: ALTER RESOURCE POOL (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.reviewer: dfurman
ms.date: 02/17/2025
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

[!INCLUDE [SQL Server](../../includes/applies-to-version/sql-asdbmi.md)]

Changes an existing [resource governor](../../relational-databases/resource-governor/resource-governor.md) resource pool configuration for a [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance.

> [!NOTE]
> To modify resource governor configuration in [!INCLUDE[ssazuremi-md.md](../../includes/ssazuremi-md.md)], you must be in the context of the `master` database on the primary replica.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
ALTER RESOURCE POOL { pool_name | [default] }
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

## Arguments

#### { *pool_name* | [default] }

Is the name of an existing user-defined resource pool or the built-in `default` resource pool.

`default` must be in brackets (`[]`) or quotation marks (`""`) when used with `ALTER RESOURCE POOL` to avoid a conflict with `DEFAULT`, which is a system reserved word. For more information, see [Database identifiers](../../relational-databases/databases/database-identifiers.md).

Built-in resource pools and workload groups use all lowercase names, such as `default`. Use the lower case `default` on servers that use a case-sensitive collation. Servers with case-insensitive collation treat `default`, `Default`, and `DEFAULT` as the same value.

#### MIN_CPU_PERCENT = *value*

Specifies the guaranteed average CPU bandwidth for all requests in the resource pool when there is CPU contention. *value* is an integer with a default setting of 0. The allowed range for *value* is from 0 through 100.

#### MAX_CPU_PERCENT = *value*

Specifies the maximum average CPU bandwidth that all requests in resource pool receive when there is CPU contention. *value* is an integer with a default setting of 100. The allowed range for *value* is from 1 through 100.

#### CAP_CPU_PERCENT = *value*

**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.

Specifies a hard cap on the CPU bandwidth that all requests in the resource pool receive. Limits the maximum CPU bandwidth level to be the same as the specified value. *value* is an integer with a default setting of 100. The allowed range for *value* is from 1 through 100.

> [!NOTE]
>  Due to the statistical nature of CPU governance, you might notice occasional short spikes exceeding the value specified in `CAP_CPU_PERCENT`.

#### AFFINITY {SCHEDULER = AUTO | ( <scheduler_range_spec> ) | NUMANODE = (<NUMA_node_range_spec>)}

**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.

Attach the resource pool to specific schedulers. The default value is `AUTO`.

Specifying `<scheduler_range_spec>` for `AFFINITY SCHEDULER` affinitizes the resource pool to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] schedulers identified by the given IDs. These IDs map to the values in the `scheduler_id` column in [sys.dm_os_schedulers](../../relational-databases/system-dynamic-management-views/sys-dm-os-schedulers-transact-sql.md).

Specifying `<NUMA_node_range_spec>` for `AFFINITY NUMANODE` affinitizes the resource pool to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] schedulers that map to the logical CPUs that correspond to the given NUMA node or a range of nodes. You can use the following [!INCLUDE[tsql](../../includes/tsql-md.md)] query to discover the mapping between the physical NUMA configuration and the [!INCLUDE[ssDE](../../includes/ssde-md.md)] scheduler IDs.

```sql
SELECT osn.memory_node_id AS numa_node_id,
       sc.cpu_id,
       sc.scheduler_id
FROM sys.dm_os_nodes AS osn
INNER JOIN sys.dm_os_schedulers AS sc
ON osn.node_id = sc.parent_node_id
   AND
   sc.scheduler_id < 1048576;
```

#### MIN_MEMORY_PERCENT = *value*

Specifies the minimum amount of query workspace memory reserved for the resource pool that can't be shared with other resource pools. *value* is an integer with a default setting of 0. The allowed range for *value* is from 0 to 100.

#### MAX_MEMORY_PERCENT = *value*

Specifies the maximum amount of query workspace memory that requests in this resource pool can use. *value* is an integer with a default setting of 100. The allowed range for *value* is from 1 through 100.

#### MIN_IOPS_PER_VOLUME = *value*

**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later.  

Specifies the minimum I/O operations per second (IOPS) per disk volume to reserve for the resource pool. The allowed range for *value* is from 0 through 2^31-1 (2,147,483,647). Specify 0 to indicate no minimum for the pool. The default is 0.

#### MAX_IOPS_PER_VOLUME = *value*

**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later.  

Specifies the maximum I/O operations per second (IOPS) per disk volume to allow for the resource pool. The allowed range for *value* is from 0 through 2^31-1 (2,147,483,647). Specify 0 to remove an IOPS limit for the pool. The default is 0.

If the `MAX_IOPS_PER_VOLUME` for a pool is set to 0, the pool isn't IO-governed at all and can take all the IOPS in the system even if other pools have `MIN_IOPS_PER_VOLUME` set. For this case, we recommend that you set the `MAX_IOPS_PER_VOLUME` value for this pool to a high number (for example, the maximum value 2^31-1) to make this pool IO-governed and to honor the IOPS reservations that might exist for other pools.

## Remarks

The sum of `MIN_CPU_PERCENT` or `MIN_MEMORY_PERCENT` for all resource pools can't exceed 100 percent.

`MIN_IOPS_PER_VOLUME` and `MAX_IOPS_PER_VOLUME` specify the minimum and maximum IOs per second. The IOs can be either reads or writes, and can be of any size. Therefore, with the same IOPS limits, the minimum and maximum IO throughput can vary depending on the mix of IO sizes in the workload.

The values for `MAX_CPU_PERCENT` and `MAX_MEMORY_PERCENT` must be greater than or equal to the values for `MIN_CPU_PERCENT` and `MIN_MEMORY_PERCENT`, respectively.

`CAP_CPU_PERCENT` differs from `MAX_CPU_PERCENT` in that workloads associated with the pool can use CPU capacity above the value of `MAX_CPU_PERCENT` if it is available, but not above the value of `CAP_CPU_PERCENT`. Although there might be short spikes higher than `CAP_CPU_PERCENT`, workloads can't exceed `CAP_CPU_PERCENT` for extended periods of time, even when additional CPU capacity is available.

The total CPU percentage for each affinitized component (scheduler(s) or NUMA node(s)) can't exceed 100 percent.

For more information, see [Resource governor](../../relational-databases/resource-governor/resource-governor.md) and [Resource governor resource pool](../../relational-databases/resource-governor/resource-governor-resource-pool.md).

### Cached plans

When you change a plan affecting setting, the new setting takes effect in previously cached plans only after executing `DBCC FREEPROCCACHE (<pool_name>)`, where `<pool_name>` is the name of a resource governor resource pool.

- If you are changing `AFFINITY` from multiple schedulers to a single scheduler, executing `DBCC FREEPROCCACHE` isn't required because parallel plans can run in serial mode. However, such a plan might be less efficient than a plan compiled as a serial plan.
- If you are changing `AFFINITY` from a single scheduler to multiple schedulers, executing `DBCC FREEPROCCACHE` isn't required. However, serial plans can't run in parallel, so clearing the respective cache allows new plans to potentially be compiled using parallelism.

> [!WARNING]
> Clearing cached plans from a resource pool that is associated with more than one workload group affects all workload groups using the user-defined resource pool identified by `<pool_name>`.

## Permissions

Requires the `CONTROL SERVER` permission.

## Examples

The following example keeps all current resource pool settings for the `default` pool, except for `MAX_CPU_PERCENT`, which is changed to 25 percent.

```sql
ALTER RESOURCE POOL [default] WITH (MAX_CPU_PERCENT = 25);

ALTER RESOURCE GOVERNOR RECONFIGURE;
```

The following example modifies the `adhocPool` resource pool as follows:
- Reserves 10 percent of CPU and 5 percent of query workspace memory using `MIN_CPU_PERCENT` and `MIN_MEMORY_PERCENT` respectively.
- Sets a 15 percent query workspace memory limit using `MAX_MEMORY_PERCENT`.
- Sets a 20 percent soft CPU cap a 30 percent hard CPU cap using `MAX_CPU_PERCENT` and `CAP_CPU_PERCENT` respectively.
- Affinitizes the pool to two ranges of logical CPUs (0 to 63 and 128 to 191) using `AFFINITY SCHEDULER`.

**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.

```sql
ALTER RESOURCE POOL adhocPool
WITH (
     MIN_CPU_PERCENT = 10,
     MAX_CPU_PERCENT = 20,
     CAP_CPU_PERCENT = 30,
     MIN_MEMORY_PERCENT = 5,
     MAX_MEMORY_PERCENT = 15,
     AFFINITY SCHEDULER = (0 TO 63, 128 TO 191)
     );

ALTER RESOURCE GOVERNOR RECONFIGURE;
```

## Related content

- [Resource governor](../../relational-databases/resource-governor/resource-governor.md)
- [Resource governor resource pool](../../relational-databases/resource-governor/resource-governor-resource-pool.md)
- [Change resource pool settings](../../relational-databases/resource-governor/change-resource-pool-settings.md)
- [CREATE RESOURCE POOL (Transact-SQL)](create-resource-pool-transact-sql.md)
- [DROP RESOURCE POOL (Transact-SQL)](drop-resource-pool-transact-sql.md)
- [CREATE WORKLOAD GROUP (Transact-SQL)](create-workload-group-transact-sql.md)
- [ALTER WORKLOAD GROUP (Transact-SQL)](alter-workload-group-transact-sql.md)
- [DROP WORKLOAD GROUP (Transact-SQL)](drop-workload-group-transact-sql.md)
- [ALTER RESOURCE GOVERNOR (Transact-SQL)](alter-resource-governor-transact-sql.md)
