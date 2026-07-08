---
title: "sys.dm_os_linux_vm_stats (Transact-SQL)"
description: sys.dm_os_linux_vm_stats returns a table with detailed Linux CPU statistics, offering system-level insights beyond SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/19/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_os_linux_vm_stats"
  - "sys.dm_os_linux_vm_stats_TSQL"
  - "dm_os_linux_vm_stats_TSQL"
  - "sys.dm_os_linux_vm_stats"
helpviewer_keywords:
  - "sys.dm_os_linux_vm_stats dynamic management view"
dev_langs:
  - "TSQL"
---
# sys.dm_os_linux_vm_stats (Transact-SQL)

[!INCLUDE [sqlserver2025-linux](../../includes/applies-to-version/sqlserver2025-linux.md)]

Returns Linux operating system-level virtual memory statistics, including metrics related to SQL Server and other processes running on the system, in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] Cumulative Update (CU) 1 and later versions.

| Column name | Data type | Nullable | Description |
| --- | --- | --- | --- |
| `vm_metric_name` | **nvarchar(256)** | No | Virtual memory metric name. |
| `count` | **bigint** | No | Corresponding statistic for that metric. |

## Permissions

Requires `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Remarks

`sys.dm_os_linux_vm_stats` provides system-wide memory observability to help you analyze memory pressure, page faults, reclaim activity, non-uniform memory access (NUMA) behavior, and to correlate SQL Server performance with overall OS memory health.

Each row represents a single virtual memory metric, typically sourced from Linux interfaces such as `/proc/vmstat`. Metric availability and meaning can vary by Linux distribution, kernel version, and configuration.

Use this DMV with other Linux-specific DMVs for holistic monitoring:

- [sys.dm_os_linux_cpu_stats](sys-dm-os-linux-cpu-stats-transact-sql.md)
- [sys.dm_os_linux_disk_stats](sys-dm-os-linux-disk-stats-transact-sql.md)
- [sys.dm_os_linux_net_stats](sys-dm-os-linux-net-stats-transact-sql.md)

### Usage scenarios

Common scenarios for using `sys.dm_os_linux_vm_stats` include:

- Investigating out-of-memory (OOM) events on Linux hosts.
- Correlating SQL Server memory symptoms with OS reclaim activity.
- Understanding NUMA-related memory behavior on multinode systems.
- Performing deep OS-level observability directly from Transact-SQL.

## Examples

### A. View all virtual memory statistics

The following query returns all available virtual memory metrics reported by the Linux kernel:

```sql
SELECT *
FROM sys.dm_os_linux_vm_stats;
```

### B. Identify page fault activity

The following query highlights page fault-related metrics, which can help identify memory pressure or inefficient memory access patterns:

```sql
SELECT vm_metric_name,
       count
FROM sys.dm_os_linux_vm_stats
WHERE vm_metric_name IN ('pgfault', 'pgmajfault');
```

### C. Monitor locality of NUMA memory

Returns NUMA-related virtual memory metrics to help understand memory locality across nodes:

```sql
SELECT vm_metric_name,
       count
FROM sys.dm_os_linux_vm_stats
WHERE vm_metric_name LIKE 'numa%';
```

### D. Analyze activity for memory reclaim and compaction

The following query helps you diagnose memory reclaim behavior and compaction pressure on the system:

```sql
SELECT vm_metric_name,
       count
FROM sys.dm_os_linux_vm_stats
WHERE vm_metric_name LIKE 'pgsteal%'
      OR vm_metric_name LIKE 'pgscan%'
      OR vm_metric_name LIKE 'compact%';
```

## Related content

- [sys.dm_os_linux_cpu_stats (Transact-SQL)](sys-dm-os-linux-cpu-stats-transact-sql.md)
- [sys.dm_os_linux_disk_stats (Transact-SQL)](sys-dm-os-linux-disk-stats-transact-sql.md)
- [sys.dm_os_linux_net_stats (Transact-SQL)](sys-dm-os-linux-net-stats-transact-sql.md)
- [Performance best practices: Storage, kernel, CPU, and network for SQL Server on Linux](../../linux/configure/performance-best-practices-operating-system.md)
- [Performance best practices: SQL Server memory on Linux](../../linux/configure/performance-best-practices-sql-server-memory.md)
