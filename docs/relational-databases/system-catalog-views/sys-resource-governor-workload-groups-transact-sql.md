---
title: "sys.resource_governor_workload_groups (Transact-SQL)"
description: sys.resource_governor_workload_groups (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dfurman
ms.date: 06/09/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.resource_governor_workload_groups"
  - "resource_governor_workload_groups_TSQL"
  - "sys.resource_governor_workload_groups_TSQL"
  - "resource_governor_workload_groups"
helpviewer_keywords:
  - "sys.resource_governor_workload_groups catalog view"
dev_langs:
  - "TSQL"
ms.custom:
  - build-2025
---

# sys.resource_governor_workload_groups (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Returns the stored workload group configuration. Each row represents a workload group. Each workload group uses one resource pool.

| Column name | Data type | Description |
| --- | --- | --- |
| `group_id` | **int** | Unique ID of the workload group. Not nullable. |
| `name` | **sysname** | Name of the workload group. Not nullable. |
| `importance` | **sysname** | Is the relative importance of a request in this workload group. Importance is one of the following: `Low`, `Medium`, `High`. `Medium` is the default. <br /><br /> **Note:** Importance is relative to other workload groups in the same resource pool.<br /><br />Not nullable. |
| `request_max_memory_grant_percent` | **int** | Maximum memory grant for a single request, as a percentage of the total query workspace memory for a resource pool. The default value is 25. Not nullable.<br /><br /> **Note:** If this setting is excessively high, queries requiring memory grants might be blocked until other queries complete, and in some cases might get an out-of-memory error. |
| `request_max_cpu_time_sec` | **int** | Maximum CPU use limit, in seconds, for a single request. The default value, 0, specifies no limit. Not nullable.<br /><br />For more information, see [REQUEST_MAX_CPU_TIME_SEC](../../t-sql/statements/create-workload-group-transact-sql.md#request_max_cpu_time_sec--value). |
| `request_memory_grant_timeout_sec` | **int** | Memory grant time-out, in seconds, for a single request. The default value, 0, uses an internal calculation based on query cost. Not nullable. |
| `max_dop` | **int** | Maximum degree of parallelism for a request executing in the workload group. The default value, 0, uses global settings in the server or database scope. Not nullable.<br /><br /> **Note:** If set to a value other than 0, overrides the global settings and the `MAXDOP` query hint. |
| `group_max_requests` | **int** | Maximum number of concurrent requests executing in the workload group. The default value, 0, specifies no limit. Not nullable. |
| `pool_id` | **int** | ID of the resource pool that this workload group uses. |
| `external_pool_id` | **int** | **Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later.<br /><br /> ID of the external resource pool that this workload group uses. |
| `request_max_memory_grant_percent_numeric` | **float** | **Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later.<br /><br /> Maximum memory grant for a single request, as a percentage of the total query workspace memory for a resource pool. The default value is 25. Not nullable.<br /><br /> **Note:** Matches `request_max_memory_grant_percent`, but includes fractions of a percent if specified when creating or modifying a workload group. |
| `group_max_tempdb_data_percent` | **float** | **Applies to**: Starting with [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)]<br /><br />The maximum amount of space in the `tempdb` data files that can be consumed by sessions in a given workload group, in percent of the [maximum tempdb size](../resource-governor/tempdb-space-resource-governance.md#percent-limit-configuration). When `group_max_tempdb_data_percent` and `group_max_tempdb_data_mb` are both `NULL`, resource governor doesn't limit space consumption in `tempdb`. Nullable. |
| `group_max_tempdb_data_mb` | **float** | **Applies to**: Starting with [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)]<br /><br />The maximum amount of space in the `tempdb` data files that can be consumed by sessions in a given workload group, in megabytes. When `group_max_tempdb_data_percent` and `group_max_tempdb_data_mb` are both `NULL`, resource governor doesn't limit space consumption in `tempdb`. Nullable. |

## Remarks

The catalog view displays the stored metadata. To see the currently effective configuration, use the corresponding dynamic management view, [sys.dm_resource_governor_workload_groups (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-workload-groups-transact-sql.md).

The stored and effective configurations can be different if the resource governor configuration has been changed but the `ALTER RESOURCE GOVERNOR RECONFIGURE` statement has not been executed.

## Permissions

Requires the `VIEW ANY DEFINITION` permission.

## Related content

- [sys.dm_resource_governor_workload_groups (Transact-SQL)](../system-dynamic-management-objects/sys-dm-resource-governor-workload-groups-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Resource governor catalog views (Transact-SQL)](resource-governor-catalog-views-transact-sql.md)
