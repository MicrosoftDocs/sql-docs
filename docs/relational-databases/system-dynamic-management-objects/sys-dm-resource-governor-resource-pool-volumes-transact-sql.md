---
title: "sys.dm_resource_governor_resource_pool_volumes (Transact-SQL)"
description: sys.dm_resource_governor_resource_pool_volumes (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dfurman
ms.date: 02/10/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_resource_governor_resource_pool_volumes_TSQL"
  - "dm_resource_governor_resource_pool_volumes_TSQL"
  - "dm_resource_governor_resource_pool_volumes"
  - "sys.dm_resource_governor_resource_pool_volumes"
helpviewer_keywords:
  - "dm_resource_governor_resource_pool_volumes"
  - "sys.dm_resource_governor_resource_pool_volumes"
dev_langs:
  - "TSQL"
---

# sys.dm_resource_governor_resource_pool_volumes (Transact-SQL)

[!INCLUDE [sql-asdbmi](../../includes/applies-to-version/sql-asdbmi.md)]

Returns information about the current resource pool IO statistics for each disk volume. This information is also available at the resource pool level in [sys.dm_resource_governor_resource_pools (Transact-SQL)](sys-dm-resource-governor-resource-pools-transact-sql.md).

| Column name | Data type | Description |
|:--|:-|:--|
| `pool_id` | **int** | The ID of the resource pool. Not nullable. |
| `volume_name` | **sysname** | The name of the disk volume. Not nullable. |
| `read_io_queued_total` | **int** | The total read IOs enqueued since resource governor statistics were reset. Not nullable. |
| `read_io_issued_total` | **int** | The total read IOs issued since resource governor statistics were reset. Not nullable. |
| `read_ios_completed_total` | **int** | The total read IOs completed since resource governor statistics were reset. Not nullable. |
| `read_ios_throttled_total` | **int** | The total read IOs throttled since resource governor statistics were reset. Not nullable. |
| `read_bytes_total` | **bigint** | The total number of bytes read since resource governor statistics were reset. Not nullable. |
| `read_io_stall_total_ms` | **bigint** | Total time (in milliseconds) between read IO arrival and completion. Not nullable. |
| `read_io_stall_queued_ms` | **bigint** | Total time (in milliseconds) between read IO arrival and issue. This is the delay introduced by the IO resource governance. Not nullable. |
| `write_io_queued_total` | **int** | The total write IOs enqueued since resource governor statistics were reset. Not nullable. |
| `write_io_issued_total` | **int** | The total write IOs issued since resource governor statistics were reset. Not nullable. |
| `write_io_completed_total` | **int** | The total write IOs completed since resource governor statistics were reset. Not nullable. |
| `write_io_throttled_total` | **int** | The total write IOs throttled since resource governor statistics were reset. Not nullable. |
| `write_bytes_total` | **bigint** | The total number of bytes written since resource governor statistics were reset. Not nullable. |
| `write_io_stall_total_ms` | **bigint** | Total time (in milliseconds) between write IO issue and completion. Not nullable. |
| `write_io_stall_queued_ms` | **bigint** | Total time (in milliseconds) between write IO arrival and issue. This is the delay introduced by the IO resource governance. Not nullable. |
| `io_issue_violations_total` | **int** | Total IO issue violations. That is, the number of times when the rate of IO issue was lower than the reserved rate. Not nullable. |
| `io_issue_delay_total_ms` | **bigint** | Total time (in milliseconds) between the scheduled issue and actual issue of IO. Not nullable. |

## Permissions

Requires `VIEW SERVER STATE` permission.

### Permissions for SQL Server 2022 and later

Requires `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Related content

- [Dynamic Management Views and Functions (Transact-SQL)](system-dynamic-management-objects.md)
- [sys.dm_resource_governor_workload_groups (Transact-SQL)](sys-dm-resource-governor-workload-groups-transact-sql.md)
- [sys.resource_governor_resource_pools (Transact-SQL)](../system-catalog-views/sys-resource-governor-resource-pools-transact-sql.md)
- [ALTER RESOURCE GOVERNOR (Transact-SQL)](../../t-sql/statements/alter-resource-governor-transact-sql.md)
