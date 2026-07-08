---
title: "SQL Server Operating System Related Dynamic Management Views (Transact-SQL)"
description: Dynamic management views (DMVs) associated with SQL Server Operating System (SQLOS).
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/19/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - build-2025
helpviewer_keywords:
  - "operating systems [SQL Server], dynamic management objects"
  - "SQL Operating System dynamic management objects [SQL Server]"
  - "SQL OS dynamic management objects [SQL Server]"
  - "dynamic management objects [SQL Server], SQL OS"
dev_langs:
  - "TSQL"
---
# SQL Server Operating System related dynamic management views (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This section documents dynamic management views (DMVs) that are associated with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Operating System (SQLOS). SQLOS manages operating system resources that are specific to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

## Memory management and memory topology

These DMVs show how SQL Server allocates, tracks, caches, and structures memory across clerks, caches, NUMA nodes, and virtual address spaces.

| Dynamic management view | Description |
| --- | --- |
| [sys.dm_os_memory_clerks](sys-dm-os-memory-clerks-transact-sql.md) | Reports memory usage by memory clerks - internal SQLOS objects allocating memory - useful for pinpointing memory consumers. |
| [sys.dm_os_memory_nodes](sys-dm-os-memory-nodes-transact-sql.md) | Displays memory distribution across NUMA nodes, showing how memory is allocated per node. |
| [sys.dm_os_nodes](sys-dm-os-nodes-transact-sql.md) | Returns NUMA node information visible to SQL Server, including node IDs and memory partitioning. |
| [sys.dm_os_memory_brokers](sys-dm-os-memory-brokers-transact-sql.md) | Shows memory broker objects within SQLOS that manage allocation units for different consumers. |
| [sys.dm_os_memory_cache_clock_hands](sys-dm-os-memory-cache-clock-hands-transact-sql.md) | Provides clock sweep positions for memory cache objects, useful for diagnosing cache eviction behavior. |
| [sys.dm_os_memory_cache_counters](sys-dm-os-memory-cache-counters-transact-sql.md) | Returns size and usage metrics for memory caches, aiding analysis of cache utilization patterns. |
| [sys.dm_os_memory_cache_entries](sys-dm-os-memory-cache-entries-transact-sql.md) | Shows details about individual entries in memory caches, enabling granular analysis of what data and objects are cached. |
| [sys.dm_os_memory_cache_hash_tables](sys-dm-os-memory-cache-hash-tables-transact-sql.md) | Lists hash tables used by memory caches and shows metrics for distribution and load factors. |
| [sys.dm_os_sys_memory](sys-dm-os-sys-memory-transact-sql.md) | Provides system memory metrics reported from the OS that SQLOS uses for memory allocation decisions. |
| [sys.dm_os_process_memory](sys-dm-os-process-memory-transact-sql.md) | Provides process-level memory metrics such as physical and virtual memory usage for SQL Server. |
| [sys.dm_os_virtual_address_dump](sys-dm-os-virtual-address-dump-transact-sql.md) | Offers a dump of virtual address descriptors, helpful in low-level memory diagnostics. |

## Buffer pool and storage cache

These DMVs report on the buffer pool, data pages in cache, and buffer pool extension (BPE) configurations.

| Dynamic management view | Description |
| --- | --- |
| [sys.dm_os_buffer_descriptors](sys-dm-os-buffer-descriptors-transact-sql.md) | Returns metadata for all data pages currently cached in the SQL Server buffer pool, including database and file associations and page type details. This view is useful for analyzing cache usage and distribution. |
| [sys.dm_os_buffer_pool_extension_configuration](sys-dm-os-buffer-pool-extension-configuration-transact-sql.md) | Provides configuration and state for the buffer pool extension (BPE), which allows the buffer pool to extend to disk storage. This view is useful for understanding how extended cache is configured. |

## Scheduling, workers, tasks, and concurrency

These DMVs describe SQLOS scheduling, worker threads, tasks, queues, dispatcher pools, spinlocks, latches, and low-level concurrency mechanisms.

| Dynamic management view | Description |
| --- | --- |
| [sys.dm_os_schedulers](sys-dm-os-schedulers-transact-sql.md) | Displays scheduler state and run queue lengths for CPU scheduling by SQLOS. This view is critical for CPU and parallelism diagnostics. |
| [sys.dm_os_workers](sys-dm-os-workers-transact-sql.md) | Lists workers managed by SQLOS, including state and flags that indicate exceptions or execution conditions. |
| [sys.dm_os_threads](sys-dm-os-threads-transact-sql.md) | Returns information about worker threads within SQLOS, useful for thread and scheduler analysis. |
| [sys.dm_os_tasks](sys-dm-os-tasks-transact-sql.md) | Shows tasks currently managed by SQLOS, providing state and scheduling details. |
| [sys.dm_os_dispatcher_pools](sys-dm-os-dispatcher-pools-transact-sql.md) | Provides statistics about internal dispatcher pools that handle work distribution across schedulers. This view helps you analyze parallelism and scheduling load. |
| [sys.dm_os_spinlock_stats](sys-dm-os-spinlock-stats-transact-sql.md) | Aggregates spinlock contention statistics, which helps identify low-level synchronization bottlenecks. |
| [sys.dm_os_latch_stats](sys-dm-os-latch-stats-transact-sql.md) | Aggregates latch wait statistics, showing contention and distribution of low-level synchronization primitives. |
| [sys.dm_os_waiting_tasks](sys-dm-os-waiting-tasks-transact-sql.md) | Shows tasks that are currently waiting along with wait types and resource details, enabling real-time wait analysis. |

## Waits and performance diagnostics

These DMVs expose wait statistics, ring buffer diagnostics, counters, or server-level data that you can use to troubleshoot performance problems.

| Dynamic management view | Description |
| --- | --- |
| [sys.dm_os_wait_stats](sys-dm-os-wait-stats-transact-sql.md) | Aggregates wait statistics for thread waits across the instance, a foundational view for diagnosing CPU, memory, or I/O bottlenecks. |
| [sys.dm_os_ring_buffers](sys-dm-os-ring-buffers-transact-sql.md) | Returns entries from the internal ring buffer, useful for diagnosing system-level events like memory pressure or scheduler alerts. |
| [sys.dm_os_performance_counters](sys-dm-os-performance-counters-transact-sql.md) | Exposes performance counter values for SQL Server - often used to correlate SQL-level activity with Windows performance monitoring metrics. |
| [sys.dm_os_server_diagnostics_log_configurations](sys-dm-os-server-diagnostics-log-configurations.md) | Shows configuration for server diagnostics logs, which capture critical internal events and diagnostics. |

## Host, cluster, and environment information

These DMVs provide information about host OS characteristics, cluster configuration, multihost participation, and general instance and system metadata.

| Dynamic management view | Description |
| --- | --- |
| [sys.dm_os_host_info](sys-dm-os-host-info-transact-sql.md) | Exposes host OS details, such as OS version and configuration, relevant to the local SQL Server instance. |
| [sys.dm_os_hosts](sys-dm-os-hosts-transact-sql.md) | Returns a row for each host known to the SQL Server instance, useful in multihost or clustered environments. |
| [sys.dm_os_cluster_nodes](sys-dm-os-cluster-nodes-transact-sql.md) | Returns information about cluster node names and roles when SQL Server is running in a Windows failover cluster. |
| [sys.dm_os_cluster_properties](sys-dm-os-cluster-properties-transact-sql.md) | Shows cluster-level properties and settings relevant to the SQL Server node, such as cluster state and configured behaviors. |
| [sys.dm_os_sys_info](sys-dm-os-sys-info-transact-sql.md) | Returns high-level SQL Server instance information such as CPU count, memory configuration, and SQLOS version. |
| [sys.dm_os_windows_info](sys-dm-os-windows-info-transact-sql.md) | Provides OS-specific information about the Windows environment hosting SQL Server, such as version and machine characteristics. |
| [sys.dm_os_child_instances](sys-dm-os-child-instances-transact-sql.md) | Lists resource and state information about child SQLOS instances in environments like distributed or multi-instance deployments. |

## Disk, volume, and I/O environment

These DMVs relate to storage devices, available volumes, and file-level or volume-level statistics.

| Dynamic management view | Description |
| --- | --- |
| [sys.dm_os_enumerate_fixed_drives](sys-dm-os-enumerate-fixed-drives.md) | Returns a list of fixed disk drives accessible to the SQL Server instance, along with basic space information. |
| [sys.dm_os_volume_stats](sys-dm-os-volume-stats-transact-sql.md) | Returns I/O statistics for database files by volume, helpful in analyzing disk activity and performance. |

## Loaded modules (executable code context)

This DMV exposes DLLs and modules currently loaded and active execution components.

| Dynamic management view | Description |
| --- | --- |
| [sys.dm_os_loaded_modules](sys-dm-os-loaded-modules-transact-sql.md) | Lists modules (DLLs/assemblies) that SQL Server workers load, providing insight into code executing within the server. |

## Low-level debugging and diagnostics

This DMV is useful primarily for escalation, debugging, crash analysis, or deep SQL Server engine troubleshooting.

| Dynamic management view | Description |
| --- | --- |
| [sys.dm_os_stacks](sys-dm-os-stacks-transact-sql.md) | Displays stack information for workers, often used in deep troubleshooting or crash dump analysis. |

## Internal and unsupported DMVs

The following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] operating system-related dynamic management views are [!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]:

- `sys.dm_os_function_symbolic_name`
- `sys.dm_os_memory_allocations`
- `sys.dm_os_sublatches`
- `sys.dm_os_worker_local_storage`

## Related content

- [System dynamic management views](system-dynamic-management-objects.md)
