---
title: Resource Governor Resource Pool
description: Resource governor limits the amount of CPU, physical IO, and memory that application requests can use within the resource pool.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jopilov, dfurman
ms.date: 01/02/2025
ms.service: sql
ms.subservice: performance
ms.topic: concept-article
helpviewer_keywords:
  - "Resource Governor, resource pool"
  - "resource pool [SQL Server], overview"
  - "resource pool [SQL Server]"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---

# Resource governor resource pool

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

In resource governor, a resource pool represents a subset of the physical resources of a [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance. Resource governor lets you specify limits on the total amount of CPU, physical IO, and memory that application requests can use within the resource pool.

Each resource pool can contain one or more [workload groups](resource-governor-workload-group.md). When a session is created, it is classified into a specific workload group. Workload group policies govern the requests executing on a session. Requests use the resources from the underlying resource pool.

## Resource pool concepts

A resource pool represents the physical resources of the server such as CPU, memory, and I/O. Depending on configuration, resources in a resource pool can be reserved or shared with other pools. The pool configuration is defined by specifying one or more of the following settings for each type of resource (CPU, memory, and physical I/O):

### MIN_CPU_PERCENT and MAX_CPU_PERCENT

These settings define the minimum and maximum guaranteed average CPU bandwidth for all requests in the resource pool when there is CPU contention. `MIN_CPU_PERCENT` is a reservation of CPU bandwidth for the resource pool that can't be used by other pools when contention is present. `MAX_CPU_PERCENT` is a soft limit for CPU bandwidth in the pool. The limit is enforced only if there is CPU contention with other pools.

For example, assume the Sales and Marketing departments in a company share the same [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. The Sales department has a CPU-intensive workload with high-priority queries. The Marketing department also has a CPU-intensive workload, but has lower priority queries. By creating a separate resource pool for each department, you can assign a *minimum* CPU percentage of 40 for the Sales resource pool and a *maximum* CPU percentage of 30 for the Marketing resource pool. This configuration ensures that the Sales workload receives the CPU resources it requires and the Marketing workload doesn't affect the CPU demands of the Sales workload.

The maximum CPU percentage is an opportunistic maximum. If there is available CPU capacity, the requests use all of it, up to 100 percent. The maximum value only applies when there is contention for CPU resources. In the previous example, if the Sales workload isn't running, the Marketing workload can use 100 percent of the CPU if needed.

### CAP_CPU_PERCENT

The `CAP_CPU_PERCENT` setting is a hard limit on the CPU bandwidth for all requests in the resource pool. Workloads associated with the pool can use CPU capacity above the value of `MAX_CPU_PERCENT` if it's available, but not above the value of `CAP_CPU_PERCENT`. Based on the example in the previous section, let's assume that the Marketing department is being charged for their resource usage. They want predictable billing and don't want to pay for more than 30 percent of the CPU. This goal can be accomplished by setting `CAP_CPU_PERCENT` to 30 for the Marketing resource pool.

### MIN_MEMORY_PERCENT and MAX_MEMORY_PERCENT

These settings are the minimum and maximum amount of memory reserved for the resource pool that can't be shared with other resource pools.

Setting a minimum memory value for a pool reserves the memory for requests that execute in this resource pool. This setting is different from `MIN_CPU_PERCENT`, because the reserved memory might remain in the pool even when there are no requests in the workload groups belonging to this pool. Be careful when using this setting as this memory is unavailable for use by any other pool, even when there are no active requests. Setting a maximum memory value for a pool means that when requests are running in this pool, they never get more than this percentage of the overall memory.

For databases without [memory-optimized tables](../in-memory-oltp/introduction-to-memory-optimized-tables.md), the memory governed by `MIN_MEMORY_PERCENT` and `MAX_MEMORY_PERCENT` is specifically the query workspace memory, or query execution grant memory. The buffer pool memory (data and index pages) is always shared among all resource pools and isn't reserved or limited by resource governor. For more information on query execution memory grants, see [Memory grant considerations](../memory-management-architecture-guide.md#memory-grant-considerations). For more information about using resource pools with memory-optimized tables, see [Bind a Database with Memory-Optimized Tables to a Resource Pool](../in-memory-oltp/bind-a-database-with-memory-optimized-tables-to-a-resource-pool.md).

### AFFINITY

This setting lets you affinitize a resource pool to one or more schedulers or NUMA nodes for greater isolation of CPU resources. To use the Sales and Marketing scenario from previous sections, let's assume that the Sales department needs a more isolated environment and wants 100 percent of a logical CPU at all times. By using the `AFFINITY` option, the Sales and Marketing workloads can be scheduled on different logical CPUs. Assuming the `CAP_CPU_PERCENT` on the Marketing pool is specified, the Marketing workload continues to use a maximum of 30 percent of one CPU, while the Sales workload uses 100 percent of another CPU.

### MIN_IOPS_PER_VOLUME and MAX_IOPS_PER_VOLUME

These settings are the minimum and maximum physical IO operations per second (IOPS) per disk volume for a resource pool. You can use these settings to control the physical IOs issued by user requests in a given resource pool. For example, the Sales department generates several end-of-month reports in large batches. The queries in these batches can generate IOs that can saturate the disk volume and affect the performance of other higher priority workloads on the same [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. To isolate this workload, the `MIN_IOPS_PER_VOLUME` is set to 500 and the `MAX_IOPS_PER_VOLUME` is set to 2,000 for the Sales department resource pool.

### System and user-defined resource pools

Resource governor has two built-in resource pools, the `internal` pool and the `default` pool. You can create additional user-defined pools.

### Internal pool

The `internal` pool governs the resources consumed by the [!INCLUDE[ssDE](../../includes/ssde-md.md)] itself. This pool always contains only the `internal` group, and the pool can't be modified in any way. Resource consumption by the `internal` pool isn't restricted. Any workloads in the pool are considered critical for the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to function. Resource governor allows the `internal` pool to pressure other pools even if it means a violation of limits set for the other pools.

> [!NOTE]
> The `internal` pool and `internal` group resource usage isn't subtracted from the overall resource usage. Percentages are calculated from the overall resources available.

### Default pool

Initially, the `default` pool only contains the `default` workload group. You can't create or drop the `default` pool, but you can modify it. The `default` pool can contain user-defined workload groups in addition to the `default` group. Beginning with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], in addition to the `default` resource pool for [!INCLUDE[ssDE](../../includes/ssde-md.md)] operations, there is a `default` external resource pool specifically for external processes, such as executing R scripts.

> [!NOTE]
> The `default` group can be modified, but can't be moved out of the `default` pool.

### External pool

Users can create an external pool to define resources for the external processes. For R Services, this pool specifically governs `rterm.exe`, `BxlServer.exe`, `python.exe`, and other processes spawned by them. For more information, see [CREATE EXTERNAL RESOURCE POOL](../../t-sql/statements/create-external-resource-pool-transact-sql.md).

### User-defined resource pools

You can create user-defined resource pools for specific workloads in your environment. Resource governor provides DDL statements for creating, modifying, and deleting resource pools. For more information, see [Create a resource pool](create-a-resource-pool.md), [Delete a resource pool](delete-a-resource-pool.md), and [Change resource pool settings](change-resource-pool-settings.md).

## Resource allocation among resource pools

When you configure CPU and memory limits and reservations, the sum of `MIN` values across all pools can't exceed 100 percent of the server resources. `MAX` and `CAP` values can be set anywhere in the range between the `MIN` value and 100 percent inclusive.

If a pool has a resource reservation by specifying a nonzero `MIN` value, the effective `MAX` value of other pools might be reduced. The least of the configured `MAX` value of a pool, and the sum of the `MIN` values of other pools is subtracted from 100 percent.

The following tables illustrate this concept. In these tables, `LEAST(X, Y)` means the smaller value of `X` and `Y`. All numeric values are percentages.

The first table shows the settings for the `internal` pool, the `default` pool, and two user-defined pools.

| Pool name | `MIN` | `MAX` | `Effective MAX` | `Shared %` | Comment |
| :-- | --: | --: | --: | --: | :-- |
| `internal` | 0 | 100 | 100 | 0 | `Effective MAX` and `Shared %` aren't applicable to the `internal` pool. |
| `default` | 0 | 100 | 30 | 30 | `Effective MAX = LEAST(100, 100 - (20 + 50)) = 30`<br />`Shared % = Effective MAX - MIN = 30` |
| `Pool 1` | 20 | 100 | 50 | 30 | `Effective MAX = LEAST(100, 100 - 50) = 50`<br />`Shared % = Effective MAX - MIN = 30` |
| `Pool 2` | 50 | 70 | 70 | 20 | `Effective MAX = LEAST(70, 100 - 20) = 70`<br />`Shared % = Effective MAX - MIN = 20` |

Using the preceding table as an example, we can further illustrate the adjustments that take place when another resource pool is created. This pool is named `Pool 3` and has a `MIN` setting of 5.

| Pool name | `MIN` | `MAX` | `Effective MAX` | `Shared %` | Comment |
| :-- | --: | --: | --: | --: | :-- |
| `internal` | 0 | 100 | 100 | 0 | `Effective MAX` and `Shared %` aren't applicable to the `internal` pool. |
| `default` | 0 | 100 | 25 | 25 | `Effective MAX = LEAST(100, 100 - (20 + 50 + 5)) = 25`<br />`Shared % = Effective MAX - MIN = 25` |
| `Pool 1` | 20 | 100 | 45 | 25 | `Effective MAX = LEAST(100, 100 - (50 + 5))) = 45`<br />`Shared % = Effective MAX - MIN = 25` |
| `Pool 2` | 50 | 70 | 70 | 20 | `Effective MAX = LEAST(70, 100 - (20 + 5))) = 70`<br />`Shared % = Effective MAX - MIN = 20` |
| `Pool 3` | 5 | 100 | 30 | 25 | `Effective MAX = LEAST(100, 100 - (50 + 20))) = 30`<br />`Shared % = Effective MAX - MIN = 25` |

The shared part of the pool is where the available resources can go if resources are available. However, when resources are consumed they go to the specified pool and aren't shared. This behavior can improve resource utilization in cases where there are no requests in a given pool and the resources not reserved to the pool can be freed up for other pools.

Some edge cases of pool configuration are:

- All pools define minimums that in total represent 100 percent of the server resources. In this case, the effective maximums are equal to minimums. This is equivalent to dividing the server resources into nonoverlapping pieces regardless of how resources are consumed inside any given pool.
- All pools have zero minimums. All pools share and compete for available resources and their runtime sizes are based on resource consumption in each pool. Other factors such as workload group policies play a role in shaping pool sizes.

## Resource pool tasks

Resource pools can govern a variety of system resources. For more information, see [CREATE RESOURCE POOL](../../t-sql/statements/create-resource-pool-transact-sql.md).

For more samples and a complete walkthrough, see [Tutorial: Resource governor configuration examples and best practices](resource-governor-walkthrough.md).

| Task description | Article |
|:--|:--|
| Create a resource pool | [Create a resource pool](create-a-resource-pool.md) |
| Modify resource pool settings | [Change resource pool settings](change-resource-pool-settings.md) |
| Delete a resource pool | [Delete a resource pool](delete-a-resource-pool.md) |

Resource governor provides DDL statements for creating, modifying, and deleting resource pools.

For more information, including the details about resource pool reservations and limits, see:

- [CREATE RESOURCE POOL](../../t-sql/statements/create-resource-pool-transact-sql.md)
- [ALTER RESOURCE POOL](../../t-sql/statements/alter-resource-pool-transact-sql.md)
- [DROP RESOURCE POOL](../../t-sql/statements/drop-resource-pool-transact-sql.md)

## Related content

- [Resource governor](resource-governor.md)
- [Resource governor workload group](resource-governor-workload-group.md)
- [Resource governor classifier function](resource-governor-classifier-function.md)
- [Configure resource governor using a template](configure-resource-governor-using-a-template.md)
- [View and modify resource governor properties](view-resource-governor-properties.md)
