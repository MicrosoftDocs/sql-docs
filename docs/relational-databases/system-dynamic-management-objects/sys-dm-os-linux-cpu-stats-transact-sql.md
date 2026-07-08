---
title: "sys.dm_os_linux_cpu_stats (Transact-SQL)"
description: sys.dm_os_linux_cpu_stats returns a table with detailed Linux CPU statistics, offering system-level insights beyond SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/13/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_os_linux_cpu_stats"
  - "sys.dm_os_linux_cpu_stats_TSQL"
  - "dm_os_linux_cpu_stats_TSQL"
  - "sys.dm_os_linux_cpu_stats"
helpviewer_keywords:
  - "sys.dm_os_linux_cpu_stats dynamic management view"
dev_langs:
  - "TSQL"
---
# sys.dm_os_linux_cpu_stats (Transact-SQL)

[!INCLUDE [sqlserver2025-linux](../../includes/applies-to-version/sqlserver2025-linux.md)]

Returns a table with detailed Linux CPU statistics, offering system-level insights beyond SQL Server, in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] Cumulative Update (CU) 1 and later versions.

| Column name | Data type | Nullable | Description |
| --- | --- | --- | --- |
| `uptime_secs` | **float** | No | Seconds since system startup. |
| `loadavg_1min` | **float** | No | Number of jobs in the run queue (ready or waiting for disk I/O) over 1 minute. |
| `user_time_cs` | **bigint** | No | `USER_HZ` spent in user mode. |
| `nice_time_cs` | **bigint** | No | `USER_HZ` spent in user mode with low priority (`nice`). |
| `system_time_cs` | **bigint** | No | `USER_HZ` spent in system mode. |
| `idle_time_cs` | **bigint** | No | `USER_HZ` spent in the idle task. |
| `iowait_time_cs` | **bigint** | No | `USER_HZ` waiting for I/O to complete. |
| `irq_time_cs` | **bigint** | No | `USER_HZ` servicing interrupts. |
| `softirq_time_cs` | **bigint** | No | `USER_HZ` servicing SoftIRQs. |
| `interrupt_cnt` | **bigint** | No | Number of interrupts serviced since system startup. |
| `csw_cnt` | **bigint** | No | Number of context switches since system startup. |
| `boot_time_secs` | **bigint** | No | Boot time, in seconds since Unix epoch. |
| `total_forks_cnt` | **bigint** | No | Number of forks since system startup. |
| `proc_runable_cnt` | **bigint** | No | Number of processes in runnable state. |
| `proc_ioblocked_cnt` | **bigint** | No | Number of processes blocked waiting for I/O. |
| `C3_time` | **bigint** | No | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] |
| `C2_time` | **bigint** | No | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] |
| `C1_time` | **bigint** | No | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] |
| `C3_count` | **bigint** | No | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] |
| `C2_count` | **bigint** | No | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] |
| `C1_count` | **bigint** | No | [!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)] |

## Permissions

Requires `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Remarks

This DMV provides cumulative statistics since the system started.

Values aren't limited to SQL Server activity. They reflect the entire Linux host.

For more information on interpreting Linux CPU statistics, see the `proc_stat(5)` manual page for your distribution.

Use this DMV with other Linux-specific DMVs for holistic monitoring:

- [sys.dm_os_linux_disk_stats](sys-dm-os-linux-disk-stats-transact-sql.md)
- [sys.dm_os_linux_net_stats](sys-dm-os-linux-net-stats-transact-sql.md)
- [sys.dm_os_linux_vm_stats](sys-dm-os-linux-vm-stats-transact-sql.md)

### Usage scenarios

- **Diagnose CPU saturation**: Use `loadavg_1min`, `proc_runable_cnt`, and `user_time_cs` to identify if the system is under heavy CPU load.

- **Investigate I/O waits**: High `iowait_time_cs` or `proc_ioblocked_cnt` values could indicate storage bottlenecks.

- **Analyze system responsiveness**: Frequent context switches (`csw_cnt`) or interrupts (`interrupt_cnt`) can signal problems with system scheduling or hardware.

- **Correlate SQL Server performance with system activity**: Since these metrics are system-wide, they help you distinguish between SQL Server-specific and broader OS-level problems.

## Examples

### A. Current CPU statistics for the system

Use `sys.dm_os_linux_cpu_stats` to get the current CPU statistics for the system. This example is a one-time snapshot that shows common signals that operations teams frequently monitor.

```sql
SELECT GETDATE() AS sample_time,
       uptime_secs,
       loadavg_1min,
       proc_runable_cnt,
       proc_ioblocked_cnt,
       interrupt_cnt,
       csw_cnt,
       user_time_cs,
       system_time_cs,
       idle_time_cs,
       iowait_time_cs
FROM sys.dm_os_linux_cpu_stats;
```

### B. Correlate host pressure with SQL schedulers

Run the following example query to get a host snapshot.

```sql
SELECT loadavg_1min,
       proc_runable_cnt,
       proc_ioblocked_cnt,
       interrupt_cnt,
       csw_cnt
FROM sys.dm_os_linux_cpu_stats;
```

The following query returns information about SQL scheduler pressure, excluding DAC and hidden schedulers.

If schedulers show high `runnable_tasks_count` and host `loadavg_1min` is more than the CPU count with elevated `proc_runable_cnt`, the system is likely CPU-bound. If SQL runnable is low but the host `proc_ioblocked_cnt` and I/O wait rates are high, storage is your suspect.

> [!NOTE]  
> Host metrics are system wide, and aren't limited to SQL Server.

```sql
SELECT scheduler_id,
       runnable_tasks_count,
       current_tasks_count,
       work_queue_count,
       is_online,
       is_scheduler_online,
       load_factor
FROM sys.dm_os_schedulers
WHERE scheduler_id < 255;
```

## Related content

- [sys.dm_os_linux_disk_stats (Transact-SQL)](sys-dm-os-linux-disk-stats-transact-sql.md)
- [sys.dm_os_linux_net_stats (Transact-SQL)](sys-dm-os-linux-net-stats-transact-sql.md)
- [sys.dm_os_linux_vm_stats (Transact-SQL)](sys-dm-os-linux-vm-stats-transact-sql.md)
- [Performance best practices: Storage, kernel, CPU, and network for SQL Server on Linux](../../linux/configure/performance-best-practices-operating-system.md)
- [Performance best practices: SQL Server memory on Linux](../../linux/configure/performance-best-practices-sql-server-memory.md)
