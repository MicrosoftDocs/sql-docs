---
title: "sys.dm_os_linux_disk_stats (Transact-SQL)"
description: sys.dm_os_linux_disk_stats returns a table with detailed Linux CPU statistics, offering system-level insights beyond SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/13/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_os_linux_disk_stats"
  - "sys.dm_os_linux_disk_stats_TSQL"
  - "dm_os_linux_disk_stats_TSQL"
  - "sys.dm_os_linux_disk_stats"
helpviewer_keywords:
  - "sys.dm_os_linux_disk_stats dynamic management view"
dev_langs:
  - "TSQL"
---
# sys.dm_os_linux_disk_stats (Transact-SQL)

[!INCLUDE [sqlserver2025-linux](../../includes/applies-to-version/sqlserver2025-linux.md)]

Returns a table with disk I/O statistics for each Linux device, showing total activity beyond SQL Server, in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] Cumulative Update (CU) 1 and later versions.

The DMV returns one row per Linux disk device.

| Column name | Data type | Nullable | Description |
| --- | --- | --- | --- |
| `dev_name` | **nvarchar(256)** | No | Device name. |
| `major_num` | **bigint** | No | Major device number. |
| `minor_num` | **bigint** | No | Minor device number. |
| `reads_completed` | **bigint** | No | Number of reads completed. |
| `reads_merged` | **bigint** | No | Number of adjacent reads merged into a single request. |
| `sectors_read` | **bigint** | No | Number of sectors read. |
| `read_time_ms` | **bigint** | No | Milliseconds spent servicing reads. |
| `writes_completed` | **bigint** | No | Number of writes completed. |
| `writes_merged` | **bigint** | No | Number of adjacent writes merged into a single request. |
| `sectors_written` | **bigint** | No | Number of sectors written. |
| `write_time_ms` | **bigint** | No | Milliseconds spent servicing writes. |
| `ios_in_progress` | **bigint** | No | Number of IOs currently in request queues. |
| `io_time_ms` | **bigint** | No | Milliseconds the device spent doing I/O. |
| `weighted_io_time_ms` | **bigint** | No | Weighted number of milliseconds spent doing I/Os. |

## Permissions

Requires `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Remarks

- **Host level scope**:
    The results reflect all activity on the device, including SQL Server, other services, and background system operations. To attribute database workload effects, correlate these results with SQL Server wait statistics, such as `WRITELOG` and `PAGEIOLATCH_*`.    

- **Interpret time columns**:  

  | Measure | Calculation |
  | --- | --- |
  | Average read latency (ms/op) | `read_time_ms / NULLIF(reads_completed, 0)` |
  | Average write latency (ms/op) | `write_time_ms / NULLIF(writes_completed, 0)` |
  | Device utilization (%) over an interval | `io_time_ms / (elapsed_ms) * 100` |
  | Average queue length | `weighted_io_time_ms / elapsed_ms` |

- **Sector units**: Linux commonly reports sectors in 512-byte units. Validate your environment's sector size and adjust calculations accordingly.

- **Use with care in multitenant hosts**: High values can originate from non-SQL Server workloads. Correlate with system tools or other DMVs for attribution.

Use this DMV with other Linux-specific DMVs for holistic monitoring:

- [sys.dm_os_linux_cpu_stats](sys-dm-os-linux-cpu-stats-transact-sql.md)
- [sys.dm_os_linux_net_stats](sys-dm-os-linux-net-stats-transact-sql.md)
- [sys.dm_os_linux_vm_stats](sys-dm-os-linux-vm-stats-transact-sql.md)

### Usage scenarios

- **Log flush slowness (availability groups or standalone)**: High `write_time_ms` / `writes_completed` with elevated `io_time_ms` indicates device-level write latency. Corroborate with `WRITELOG` waits.  

- **Checkpoint or read heavy workloads**: Rising `read_time_ms` / `reads_completed` with sustained `read_MBps` suggests throughput constrained by latency. Consider storage tier or queue settings.  

- **Noisy neighbors on shared hosts**: Spikes in `device_utilization_pct` and `avg_queue_length` without corresponding SQL Server workload changes imply external I/O pressure. Validate with host monitoring.

## Examples

### A. Current device activity snapshot

This query returns an activity snapshot for the current storage device.

```sql
SELECT dev_name,
       reads_completed,
       read_time_ms,
       writes_completed,
       write_time_ms,
       ios_in_progress,
       io_time_ms,
       weighted_io_time_ms
FROM sys.dm_os_linux_disk_stats
ORDER BY io_time_ms DESC;
```

### B. Latency and throughput over a 10-second sample window

The script samples the DMV twice and computes deltas for latency (milliseconds per operation), throughput (MB per second), utilization (percentage), and queue length. Adjust `@SectorBytes` to match your device configuration.

```sql
DECLARE @SectorBytes AS INT = 512;

-- verify sector size for your environment
DECLARE @SampleMs AS INT = 10000;

IF OBJECT_ID('tempdb..#before') IS NOT NULL
    DROP TABLE #before;

IF OBJECT_ID('tempdb..#after') IS NOT NULL
    DROP TABLE #after;

SELECT dev_name,
       reads_completed,
       reads_merged,
       sectors_read,
       read_time_ms,
       writes_completed,
       writes_merged,
       sectors_written,
       write_time_ms,
       ios_in_progress,
       io_time_ms,
       weighted_io_time_ms
INTO #before
FROM sys.dm_os_linux_disk_stats;

WAITFOR DELAY '00:00:10';

SELECT dev_name,
       reads_completed,
       reads_merged,
       sectors_read,
       read_time_ms,
       writes_completed,
       writes_merged,
       sectors_written,
       write_time_ms,
       ios_in_progress,
       io_time_ms,
       weighted_io_time_ms
INTO #after
FROM sys.dm_os_linux_disk_stats;

WITH deltas
AS (SELECT a.dev_name,
           a.reads_completed - b.reads_completed AS d_reads,
           a.read_time_ms - b.read_time_ms AS d_read_ms,
           a.sectors_read - b.sectors_read AS d_read_sectors,
           a.writes_completed - b.writes_completed AS d_writes,
           a.write_time_ms - b.write_time_ms AS d_write_ms,
           a.sectors_written - b.sectors_written AS d_write_sectors,
           a.io_time_ms - b.io_time_ms AS d_io_ms,
           a.weighted_io_time_ms - b.weighted_io_time_ms AS d_weighted_io_ms
    FROM #after AS a
         INNER JOIN #before AS b
             ON a.dev_name = b.dev_name)
SELECT dev_name,
       -- latency (ms/op)
       CAST (d_read_ms / NULLIF (d_reads, 0) AS DECIMAL (18, 2)) AS avg_read_latency_ms,
       CAST (d_write_ms / NULLIF (d_writes, 0) AS DECIMAL (18, 2)) AS avg_write_latency_ms,
       -- throughput (MB/s)
       CAST ((d_read_sectors * @SectorBytes) / (@SampleMs / 1000.0) / 1048576.0 AS DECIMAL (18, 2)) AS read_MBps,
       CAST ((d_write_sectors * @SectorBytes) / (@SampleMs / 1000.0) / 1048576.0 AS DECIMAL (18, 2)) AS write_MBps,
       -- utilization (%)
       CAST (d_io_ms / @SampleMs * 100.0 AS DECIMAL (5, 2)) AS device_utilization_pct,
       -- average queue length
       CAST (d_weighted_io_ms / @SampleMs AS DECIMAL (18, 2)) AS avg_queue_length
FROM deltas
ORDER BY device_utilization_pct DESC;
```

### C. Identify devices with high merge ratios

This example identifies devices with high merge ratios, indicating possible I/O coalescing.

```sql
SELECT dev_name,
       reads_completed,
       reads_merged,
       writes_completed,
       writes_merged,
       CAST (reads_merged / NULLIF (reads_completed, 0) AS DECIMAL (10, 2)) AS read_merge_ratio,
       CAST (writes_merged / NULLIF (writes_completed, 0) AS DECIMAL (10, 2)) AS write_merge_ratio
FROM sys.dm_os_linux_disk_stats
ORDER BY write_merge_ratio DESC, read_merge_ratio DESC;
```

### D. Watch queue depth live

This example shows the live queue depth, which is useful for incident triage.

```sql
SELECT TOP (20) dev_name,
                ios_in_progress,
                io_time_ms,
                weighted_io_time_ms
FROM sys.dm_os_linux_disk_stats
ORDER BY ios_in_progress DESC,
         weighted_io_time_ms DESC;
```

## Related content

- [sys.dm_os_linux_cpu_stats (Transact-SQL)](sys-dm-os-linux-cpu-stats-transact-sql.md)
- [sys.dm_os_linux_net_stats (Transact-SQL)](sys-dm-os-linux-net-stats-transact-sql.md)
- [sys.dm_os_linux_vm_stats (Transact-SQL)](sys-dm-os-linux-vm-stats-transact-sql.md)
- [Performance best practices: Storage, kernel, CPU, and network for SQL Server on Linux](../../linux/configure/performance-best-practices-operating-system.md)
- [Performance best practices: SQL Server memory on Linux](../../linux/configure/performance-best-practices-sql-server-memory.md)
