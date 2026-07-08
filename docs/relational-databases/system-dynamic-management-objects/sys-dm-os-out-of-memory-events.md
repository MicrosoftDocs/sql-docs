---
title: "sys.dm_os_out_of_memory_events"
description: sys.dm_os_out_of_memory_events returns a log of out of memory (OOM) events, including a predicted out of memory cause.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 06/11/2025
ms.service: azure-sql-database
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.dm_os_out_of_memory_events"
  - "sys.dm_os_out_of_memory_events_TSQL"
  - "dm_os_out_of_memory_events"
  - "dm_os_out_of_memory_events_TSQL"
helpviewer_keywords:
  - "sys.dm_os_out_of_memory_events"
  - "dm_os_out_of_memory_events"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---
# sys.dm_os_out_of_memory_events

[!INCLUDE [Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/asdb-asmi-fabricsqldb.md)]

Returns a set of recent out of memory (OOM) events.

For more information on out of memory conditions in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], see [Troubleshoot out of memory errors in Azure SQL Database](/azure/azure-sql/database/troubleshoot-memory-errors-issues).

| Column name | Data type | Description |
|-------------|---------------|-----------------|
| `event_time` | **datetime**  | OOM event time. Is not nullable.|
| `oom_cause` | **tinyint**| A numeric value indicating OOM root cause. OOM cause is determined by a heuristic algorithm and is provided with a finite degree of confidence. Is not nullable.|
| `oom_cause_desc` | **nvarchar(30)** | Description of `oom_cause`. Is not nullable.<BR>0. `UNKNOWN` -  OOM cause could not be determined<BR>1. HEKATON_POOL_MEMORY_LOW - Insufficient memory in the resource pool used for In-Memory OLTP. For more information, see [Monitor In-Memory OLTP](/azure/azure-sql/database/in-memory-oltp-monitor-space?view=azuresql-db&preserve-view=true).<BR>2. `MEMORY_LOW` - Insufficient memory available to the database engine process<BR>3. `OS_MEMORY_PRESSURE` - OOM due to external memory pressure from the operating system<BR>4. `OS_MEMORY_PRESSURE_SQL` - OOM due to external memory pressure from other database engine instance(s)<BR>5. `NON_SOS_MEMORY_LEAK` - OOM due to a leak in non-SOS memory, for example, loaded modules<BR>6. `SERVERLESS_MEMORY_RECLAMATION` - OOM related to memory reclamation in a serverless database<BR>7. `MEMORY_LEAK` - OOM due to a leak in SOS memory<BR>8. `SLOW_BUFFER_POOL_SHRINK` - OOM due to the buffer pool not releasing memory fast enough under memory pressure<BR>9. `INTERNAL_POOL` - Insufficient memory in the internal resource pool<BR>10. `SYSTEM_POOL` - Insufficient memory in a system resource pool<BR>11. `QUERY_MEMORY_GRANTS` - OOM due to large memory grants held by queries<BR>12. `REPLICAS_AND_AVAILABILITY` - OOM due to workloads in SloSecSharedPool resource pool |
| `available_physical_memory_mb` | **int** | Available physical memory, in megabytes. Is not nullable.|
| `initial_job_object_memory_limit_mb` | **int** | Job object memory limit on database engine startup, in megabytes. For more information on Job Objects, see [Resource governance](/azure/azure-sql/database/resource-limits-logical-server#resource-governance). Nullable.|
| `current_job_object_memory_limit_mb` | **int** | Job object current memory limit, in megabytes. Nullable.|
| `process_memory_usage_mb` | **int** | Total process memory usage in megabytes by the instance. Is not nullable.|
| `non_sos_memory_usage_mb` | **int** | Non-SOS usage in megabytes, including SOS created threads, threads created by non-SOS components, loaded DLLs, etc. Is not nullable.|
| `committed_memory_target_mb` | **int** | SOS target memory in megabytes. Is not nullable.|
| `committed_memory_mb` | **int** | SOS committed memory in megabytes. Is not nullable.|
| `allocation_potential_memory_mb` | **int** | Memory available to the database engine instance for new allocations, in megabytes. Is not nullable.|
| `oom_factor` | **tinyint** | A value that provides additional information related to the OOM event, for internal use only. Is not nullable.|
| `oom_factor_desc` | **nvarchar(30)** | Description of `oom_factor`. For internal use only. Is not nullable.<BR>0 - `UNDEFINED`<BR>1 - `ALLOCATION_POTENTIAL`<BR>2 - `BLOCK_ALLOCATOR`<BR>3 - `ESCAPE_TIMEOUT`<BR>4 - `FAIL_FAST`<BR>5 - `MEMORY_POOL`<BR>6 - `EMERGENCY_ALLOCATOR`<BR>7 - `VIRTUAL_ALLOC`<BR>8 - `SIMULATED`<BR>9 - `BUF_ALLOCATOR`<BR>10 - `QUERY_MEM_QUEUE`<BR>11 - `FRAGMENT`<BR>12 - `INIT_DESCRIPTOR`<BR>13 - `MEMORY_POOL_PRESSURE`<BR>14 - `DESCRIPTOR_ALLOCATOR`<BR>15 - `DESCRIPTOR_ALLOCATOR_ESCAPE` |
| `oom_resource_pools` | **nvarchar(4000)** | Resource pools that are out of memory, including memory usage statistics for each pool. This information is provided as a JSON value. Nullable.|
| `top_memory_clerks` | **nvarchar(4000)** | Top memory clerks by memory consumption, including memory usage statistics for each clerk. This information is provided as a JSON value. Nullable.|
| `top_resource_pools` | **nvarchar(4000)** | Top resource pools by memory consumption, including memory usage statistics for each resource pool. This information is provided as a JSON value. Nullable.|
| `possible_leaked_memory_clerks` | **nvarchar(4000)** | Memory clerks that have leaked memory. Based on heuristics and provided with a finite degree of confidence. This information is provided as a JSON value. Nullable.|
| `possible_non_sos_leaked_memory_mb` | **int** | Leaked non-SOS memory in megabytes, if any. Based on heuristics and provided with a finite degree of confidence. Nullable.|

## Permissions

On [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], requires `VIEW SERVER PERFORMANCE STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Microsoft Entra admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerPerformanceStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE PERFORMANCE STATE` permission on the database, or membership in the `##MS_ServerPerformanceStateReader##` server role is required.

## Remarks

Each row in this view represents an out of memory (OOM) event that has occurred in the database engine. Not all OOM events might be captured. Older OOM events can disappear from the result set as more recent OOM events occur. Result set is not persisted across restarts of the database engine.

Currently, this DMV is visible but not supported in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and [!INCLUDE[sssql25-md](../../includes/sssql25-md.md)].

### summarized_oom_snapshot extended event

The `summarized_oom_snapshot` extended event is a part of the `system_health` event session to simplify detection of out of memory events. Each `summarized_oom_snapshot` extended event corresponds to a row in `sys.dm_os_out_of_memory_events`. For more information, see [Blog: A new way to troubleshoot out-of-memory errors in the database engine](https://techcommunity.microsoft.com/blog/azuresqlblog/a-new-way-to-troubleshoot-out-of-memory-errors-in-the-database-engine/3271926).

## Examples

### A. Get all available OOM events

The following example returns all event data ordered by the most recent time for the database engine hosting the currently connected database.

```sql
SELECT *
FROM sys.dm_os_out_of_memory_events
ORDER BY event_time DESC;
```

### B. Get top memory clerks for each OOM event

The following example returns a subset of event data and expands the JSON data in the `top_memory_clerks` column. Each row in the result set represents a top memory clerk for a specific OOM event.

```sql
SELECT event_time,
       oom_cause_desc,
       oom_factor_desc,
       oom_resource_pools,
       top_resource_pools,
       clerk_type_name,
       clerk_page_allocated_mb,
       clerk_vm_committed_mb
FROM sys.dm_os_out_of_memory_events
CROSS APPLY OPENJSON(top_memory_clerks)
                    WITH (
                         clerk_type_name sysname '$.clerk_type_name',
                         clerk_page_allocated_mb bigint '$.page_allocated_mb',
                         clerk_vm_committed_mb bigint '$.vm_committed_mb'
                         )
ORDER BY event_time DESC, clerk_page_allocated_mb DESC;
```

### C. Get top resource pools for each OOM event

The following example returns a subset of event data and expands the JSON data in the `top_resource_pools` column. Each row in the result set represents a top resource pool for a specific OOM event.

```sql
SELECT event_time,
       oom_cause_desc,
       oom_factor_desc,
       oom_resource_pools,
       top_memory_clerks,
       pool_name,
       pool_allocations_mb,
       pool_target_mb,
       pool_is_oom
FROM sys.dm_os_out_of_memory_events
CROSS APPLY OPENJSON(top_resource_pools)
                    WITH (
                         pool_name sysname '$.pool_name',
                         pool_allocations_mb bigint '$.allocations_mb',
                         pool_target_mb bigint '$.pool_target_mb',
                         pool_is_oom bit '$.is_oom'
                         )
ORDER BY event_time DESC, pool_allocations_mb DESC;
```

## Related content

- [sys.resource_stats](../system-catalog-views/sys-resource-stats-azure-sql-database.md)
- [sys.server_resource_stats](../system-catalog-views/sys-server-resource-stats-azure-sql-database.md)
- [sys.dm_db_resource_stats (Azure SQL Database)](sys-dm-db-resource-stats-azure-sql-database.md?view=azuresqldb-current&preserve-view=true)
- [Optimize performance by using in-memory technologies in Azure SQL Database](/azure/azure-sql/database/in-memory-oltp-overview?view=azuresql-db&preserve-view=true)
- [Optimize performance by using in-memory technologies in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/in-memory-oltp-overview?view=azuresql-mi&preserve-view=true)
- [Monitor In-Memory OLTP storage in Azure SQL Database](/azure/azure-sql/database/in-memory-oltp-monitor-space?view=azuresql-db&preserve-view=true)
- [Monitor In-Memory OLTP storage in Azure SQL Managed Instance](/azure/azure-sql/managed-instance/in-memory-oltp-monitor-space?view=azuresql-mi&preserve-view=true)
- [Troubleshoot out of memory errors with Azure SQL Database](/azure/azure-sql/database/troubleshoot-memory-errors-issues)

