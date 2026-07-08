---
title: "sys.dm_os_linux_net_stats (Transact-SQL)"
description: sys.dm_os_linux_net_stats returns real-time Linux network interface statistics, including bytes/packets sent and received, errors, drops, and collisions.
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/13/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_os_linux_net_stats"
  - "sys.dm_os_linux_net_stats_TSQL"
  - "dm_os_linux_net_stats_TSQL"
  - "sys.dm_os_linux_net_stats"
helpviewer_keywords:
  - "sys.dm_os_linux_net_stats dynamic management view"
dev_langs:
  - "TSQL"
---
# sys.dm_os_linux_net_stats (Transact-SQL)

[!INCLUDE [sqlserver2025-linux](../../includes/applies-to-version/sqlserver2025-linux.md)]

Returns real-time Linux network interface statistics, including bytes and packets sent and received, errors, drops, and collisions, in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] Cumulative Update (CU) 1 and later versions.

| Column name | Data type | Nullable | Description |
| --- | --- | --- | --- |
| `interface` | **nvarchar(256)** | No | Name of the network interface. |
| `recv_bytes` | **bigint** | No | Number of bytes received without error (might include dropped packets). |
| `recv_packets` | **bigint** | No | Number of packets received without error (might include dropped packets). |
| `recv_errors` | **bigint** | No | Number of bad packets received. |
| `recv_drops` | **bigint** | No | Number of packets received but dropped before processing. |
| `recv_fifo` | **bigint** | No | Number of receiver FIFO errors. |
| `recv_frame` | **bigint** | No | Number of receiver frame alignment errors. |
| `recv_compressed` | **bigint** | No | Number of correctly received compressed packets. |
| `recv_multicast` | **bigint** | No | Number of multicast packets received (might include dropped packets). |
| `tx_bytes` | **bigint** | No | Number of bytes transmitted without error. |
| `tx_packets` | **bigint** | No | Number of packets transmitted without error. |
| `tx_errors` | **bigint** | No | Number of bad packets transmitted. |
| `tx_drop` | **bigint** | No | Number of packets dropped before transmission. |
| `tx_fifo` | **bigint** | No | Number of frame transmission errors due to FIFO overrun or underflow. |
| `tx_collisions` | **bigint** | No | Number of collisions while transmitting packets. |
| `tx_carrier` | **bigint** | No | Aggregate number of "carrier" errors. |
| `tx_compressed` | **bigint** | No | Number of transmitted compressed packets. |

## Permissions

Requires `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Remarks

This DMV provides system-level network metrics and isn't limited to SQL Server activity. It reflects the state of all network interfaces on the Linux host.

`sys.dm_os_linux_net_stats` is useful for diagnosing network problems that might affect SQL Server connectivity, Always On availability groups, replication, or client/server communication.

Use this DMV with other Linux-specific DMVs for holistic monitoring:

- [sys.dm_os_linux_cpu_stats](sys-dm-os-linux-cpu-stats-transact-sql.md)
- [sys.dm_os_linux_disk_stats](sys-dm-os-linux-disk-stats-transact-sql.md)
- [sys.dm_os_linux_vm_stats](sys-dm-os-linux-vm-stats-transact-sql.md)

### Usage scenarios

Access Linux network statistics by using familiar T-SQL queries. You don't need to switch to OS-level tools like `ifconfig`, `ip`, or `/proc/net/dev`.

Establish and track network performance baselines for your SQL Server on Linux deployments. This approach supports proactive diagnostics and capacity planning.

- **Detect packet loss**: Monitor `recv_drops` and `tx_drop` for nonzero values. These values might indicate network congestion or hardware problems.

- **Troubleshoot errors**: Investigate nonzero `recv_errors`, `tx_errors`, `recv_fifo`, or `tx_fifo` values to identify faulty interfaces or driver problems.

- **Analyze performance**: Track `recv_bytes` and `tx_bytes` over time to understand network throughput and identify bottlenecks.

## Examples

### A. Network statistics for all interfaces

The following example returns network statistics for all interfaces on the Linux host:

```sql
SELECT *
FROM sys.dm_os_linux_net_stats;
```

### B. Identify interfaces with packet errors or drops

Use the following query to focus on interfaces experiencing problems. It filters for nonzero error or drop counts.

```sql
SELECT interface,
       recv_errors,
       recv_drops,
       tx_errors,
       tx_drop
FROM sys.dm_os_linux_net_stats
WHERE recv_errors > 0
      OR recv_drops > 0
      OR tx_errors > 0
      OR tx_drop > 0;
```

### C. Monitor network throughput over time

Measure network throughput for each interface over a specific interval. This measurement helps you in capacity planning, and identifies bottlenecks during peak workloads.

The following query tracks network usage trends. It periodically samples bytes sent and received, and calculates the delta.

Take a baseline snapshot:

```sql
SELECT interface,
       recv_bytes,
       tx_bytes
INTO #net_stats_baseline
FROM sys.dm_os_linux_net_stats;
```

Wait for a defined interval (for example, 60 seconds), and then run:

```sql
SELECT n.interface,
       n.recv_bytes - b.recv_bytes AS bytes_received_in_interval,
       n.tx_bytes - b.tx_bytes AS bytes_sent_in_interval
FROM sys.dm_os_linux_net_stats AS n
     INNER JOIN #net_stats_baseline AS b
         ON n.interface = b.interface;
```

### D. Detect multicast traffic

To see if your SQL Server is receiving multicast packets (which might be relevant for certain HA/DR configurations):

```sql
SELECT interface,
       recv_multicast
FROM sys.dm_os_linux_net_stats
WHERE recv_multicast > 0;
```

### E. Correlate network statistics with wait statistics

If you see high `NETWORK_IO` waits, check for matching network errors or drops to identify the root cause of query delays.

Combine network stats with wait statistics to diagnose if network problems are causing SQL Server waits:

```sql
SELECT w.wait_type,
       w.wait_time_ms,
       n.interface,
       n.recv_errors,
       n.tx_errors
FROM sys.dm_os_wait_stats AS w
CROSS JOIN sys.dm_os_linux_net_stats AS n
WHERE w.wait_type LIKE '%NETWORK_IO%';
```

### F. Find interfaces with high collision counts

Detect and fix network segments with excessive collisions, which might slow down SQL Server performance.

Collisions can show network congestion or misconfigured hardware:

```sql
SELECT interface,
       tx_collisions
FROM sys.dm_os_linux_net_stats
WHERE tx_collisions > 0;
```

### G. Baseline and alerting example

Integrate with monitoring tools to proactively notify database administrators about potential network problems before they affect SQL Server workloads.

Use this DMV in automated monitoring scripts to alert when error or drop counts unexpectedly increase.

Alert if any interface has more than 10 errors or drops:

```sql
SELECT interface,
       recv_errors,
       tx_errors,
       recv_drops,
       tx_drop
FROM sys.dm_os_linux_net_stats
WHERE recv_errors > 10
      OR tx_errors > 10
      OR recv_drops > 10
      OR tx_drop > 10;
```

Alert if any interface has more than 10 errors or drops:

```sql
SELECT interface,
       recv_errors,
       tx_errors,
       recv_drops,
       tx_drop
FROM sys.dm_os_linux_net_stats
WHERE recv_errors > 10
      OR tx_errors > 10
      OR recv_drops > 10
      OR tx_drop > 10;
```

## Related content

- [sys.dm_os_linux_cpu_stats (Transact-SQL)](sys-dm-os-linux-cpu-stats-transact-sql.md)
- [sys.dm_os_linux_disk_stats (Transact-SQL)](sys-dm-os-linux-disk-stats-transact-sql.md)
- [sys.dm_os_linux_vm_stats (Transact-SQL)](sys-dm-os-linux-vm-stats-transact-sql.md)
- [Performance best practices: Storage, kernel, CPU, and network for SQL Server on Linux](../../linux/configure/performance-best-practices-operating-system.md)
- [Performance best practices: SQL Server memory on Linux](../../linux/configure/performance-best-practices-sql-server-memory.md)
