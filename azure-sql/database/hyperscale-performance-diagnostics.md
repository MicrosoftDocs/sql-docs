---
title: Performance Diagnostics in Hyperscale
description: This article describes how to troubleshoot Hyperscale performance problems in Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: denzilr, dfurman, adbadram
ms.date: 11/03/2025
ms.service: azure-sql-database
ms.subservice: performance
ms.topic: troubleshooting
ms.custom:
  - sqldbrb=1
  - build-2025
---

# SQL Hyperscale performance troubleshooting diagnostics

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

To troubleshoot performance problems in a Hyperscale database, the [general SQL performance tuning methodologies](monitor-tune-overview.md) is the starting point of any performance investigation. However, given the [distributed architecture](service-tier-hyperscale.md#distributed-functions-architecture) of Hyperscale, additional diagnostic data might need to be considered. This article describes Hyperscale-specific diagnostic data.

## Reduced log rate waits

Every database and elastic pool in Azure SQL Database manages log generation rate via [log rate governance](resource-limits-logical-server.md#transaction-log-rate-governance). The log rate limit is exposed in the `primary_max_log_rate` column in [sys.dm_user_db_resource_governance](/sql/relational-databases/system-dynamic-management-views/sys-dm-user-db-resource-governor-azure-sql-database/).

At times, log generation rate on the primary compute replica must be reduced to maintain recoverability service-level agreements (SLAs). For example, this can happen when a [page server or another compute replica](service-tier-hyperscale.md#distributed-functions-architecture) is significantly behind applying new log records from the log service. If no Hyperscale components are behind, the log rate governance mechanism allows log generation rate to reach 150 MiB/s per database for premium-series and premium-series memory optimized hardware. For standard-series hardware, the maximum log rate is 100 MiB/s per database. For elastic pools, the maximum log rate is 150 MiB/s per pool for premium-series and premium-series memory optimized hardware, and 125 MiB/s per pool for other hardware.

The following wait types appear in [sys.dm_os_wait_stats](/sql/relational-databases/system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql/) when the log rate is reduced:

| Wait type | Reason |
| --- | --- |
| `RBIO_RG_STORAGE` | Delayed log consumption by a page server |
| `RBIO_RG_DESTAGE` | Delayed log consumption by the long-term log storage |
| `RBIO_RG_REPLICA` | Delayed log consumption by an HA secondary replica or a named replica |
| `RBIO_RG_GEOREPLICA` | Delayed log consumption by a geo-secondary replica |
| `RBIO_RG_DESTAGE` | Delayed log consumption by the log service |
| `RBIO_RG_LOCALDESTAGE` | Delayed log consumption by the log service |
| `RBIO_RG_STORAGE_CHECKPOINT` | Delayed log consumption on by a page server due to slow database checkpoint |
| `RBIO_RG_MIGRATION_TARGET` | Delayed log consumption by the non-Hyperscale database during reverse migration |

The [sys.dm_hs_database_log_rate()](/sql/relational-databases/system-functions/sys-dm-hs-database-log-rate) dynamic management function (DMF) provides more details to help you understand log rate reduction, if any. For example, it can tell you which specific secondary replica is behind applying log records, and what is the total size of the not yet applied transaction log.

## Page server reads

The compute replicas don't cache a full copy of the database locally. The data local to the compute replica is stored in the buffer pool (in memory) and in the local resilient buffer pool extension (RBPEX) cache that contains a subset of the most frequently accessed data pages. This local SSD cache is sized proportionally to the compute size. Each page server, on the other hand, has a complete SSD cache for the portion of the database it maintains.

When a read IO is issued on a compute replica, if the data doesn't exist in the buffer pool or in the local SSD cache, the page at the requested [Log Sequence Number (LSN)](/sql/relational-databases/sql-server-transaction-log-architecture-and-management-guide/) is fetched from the corresponding page server. Reads from page servers are remote and are slower than reads from the local SSD cache. When troubleshooting I/O-related performance problems, we need to be able to tell how many IOs were done via the relatively slower page server reads.

Several dynamic managed views (DMVs) and extended events have columns and fields that specify the number of remote reads from a page server, which can be compared against the total reads. Query Store also captures page server reads in query runtime statistics.

- Columns to report page server reads are available in execution DMVs and catalog views:
  - [sys.dm_exec_requests](/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql/)
  - [sys.dm_exec_query_stats](/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql/)
  - [sys.dm_exec_procedure_stats](/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-procedure-stats-transact-sql/)
  - [sys.dm_exec_trigger_stats](/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-trigger-stats-transact-sql/)
  - [sys.query_store_runtime_stats](/sql/relational-databases/system-catalog-views/sys-query-store-runtime-stats-transact-sql/)

- Page server reads fields are present in the following extended events:
  - `sql_statement_completed`
  - `sp_statement_completed`
  - `sql_batch_completed`
  - `rpc_completed`
  - `scan_stopped`
  - `query_store_begin_persist_runtime_stat`
  - `query_store_execution_runtime_info`

- `ActualPageServerReads`/`ActualPageServerReadAheads` attributes are present in the query plan XML for plans that include runtime statistics. For example:

  ```xml
  <RunTimeCountersPerThread Thread="8" ActualRows="90466461" [...] ActualPageServerReads="0" ActualPageServerReadAheads="5687297" ActualLobPageServerReads="0" ActualLobPageServerReadAheads="0" />
  ```

  > [!TIP]  
  > To view these attributes in the query plan properties window, SSMS 18.3 or later is required.

## Virtual file stats and IO accounting

In Azure SQL Database, the [sys.dm_io_virtual_file_stats()](/sql/relational-databases/system-dynamic-management-views/sys-dm-io-virtual-file-stats-transact-sql/) DMF is one way to monitor database I/O statistics such as IOPS, throughput, and latency. I/O characteristics in Hyperscale are different due to its [distributed architecture](service-tier-hyperscale.md#distributed-functions-architecture). In this section, we focus on read and write I/O as seen in this DMF.

For Hyperscale, the relevant data in `sys.dm_io_virtual_file_stats()` is as follows:

- The rows where the `database_id` value matches the value returned by the [DB_ID](/sql/t-sql/functions/db-id-transact-sql) function, and where the `file_id` value is other than 2, correspond to page servers. Commonly, each row corresponds to one page server. However, for larger files, multiple page servers are used.
  - The row with `file_id` 2 corresponds to the transaction log.

- The rows where the value in the `database_id` column is 0 correspond to the local SSD cache on the compute replica.

### Local SSD cache usage

Because the local SSD cache exists on the same compute replica where the database engine is processing queries, I/O against this cache is faster than I/O against page servers. In a Hyperscale database or elastic pool, `sys.dm_io_virtual_file_stats()` has special rows reporting I/O statistics for the local SSD cache. These rows have the value of `0` for the `database_id` column. For example, the following query returns the local SSD cache I/O statistics since database startup.

```sql
SELECT *
FROM sys.dm_io_virtual_file_stats(0, NULL);
```

A ratio of the aggregated reads from the local SSD cache files to the aggregated reads from all other data files is the local SSD cache hit ratio. This metric is provided by the `RBPEX cache hit ratio` and `RBPEX cache hit ratio base` performance counters, available in the [sys.dm_os_performance_counters](/sql/relational-databases/system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql/) DMV.

### Data reads

- When reads are issued by the database engine on a compute replica, they might be served either by the local SSD cache, or by page servers, or by a combination of the two if reading multiple pages.

- When the compute replica reads some pages from a specific data file (for example, the file with `file_id` 1), if this data resides solely in the local SSD cache, all IO for this read is accounted against the local SSD cache files where `database_id` is 0. If some part of that data is in the local SSD cache, and some part is on page servers, then IO is accounted partially toward the local SSD cache files and partially toward the data files corresponding to page servers.

- When a compute replica requests a page at a particular LSN from a page server, if the page server hasn't yet caught up to the LSN requested, the read on the compute replica waits until the page server catches up before the page is returned. For any read from a page server on the compute replica, you see a `PAGEIOLATCH_*` wait type if it's waiting on that IO. In Hyperscale, this wait time includes both the time to catch up the requested page on the page server to the LSN required, and the time needed to transfer the page from the page server to the compute replica.

- Large reads such as read-aheads are often done using [scatter-gather reads](/sql/relational-databases/reading-pages/). This allows reading up to 4 MB as a single read IO. However, when the data being read is in the local SSD cache, these reads are accounted as multiple individual 8-KB reads, since the buffer pool and the local SSD cache always use 8-KB pages. As the result, the number of read IOs seen against the local SSD cache might be larger than the actual number of IOs performed by the engine.

### Data writes

- The primary compute replica doesn't write directly to page servers. Instead, log records from the log service are replayed on the corresponding page servers.

- Writes on the compute replica are predominantly writes to the local SSD cache (`database_id` 0). For writes that are larger than 8 KB, in other words those done using [gather-write](/sql/relational-databases/writing-pages/), each write operation is translated into multiple 8-KB individual writes to the local SSD cache since the buffer pool and the local SSD cache always use 8-KB pages. As the result, the number of write IOs seen against the local SSD cache might be larger than the actual number of IOs performed by the engine.

- Data files other than `database_id` 0 that correspond to page servers might also show writes. In Hyperscale, these writes are simulated, because compute replicas never write directly to page servers. I/O statistics are accounted as they occur on the compute replica. Write IOPS, throughput, and latency seen on a compute replica for data files other than `database_id` 0 don't reflect the actual I/O statistics of writes that occur on page servers.

### Log writes

- On the primary compute replica, log writes are accounted in `sys.dm_io_virtual_file_stats()` under `file_id` 2.

- Unlike in availability groups, when a transaction commits on the primary compute replica, log records aren't hardened on the secondary replica. In Hyperscale, log is hardened in the log service, and applied to the secondary replicas asynchronously. Because log writes don't actually occur on secondary replicas, any accounting of log IOs in `sys.dm_io_virtual_file_stats()` on the secondary replicas shouldn't be used as transaction log I/O statistics.

## Data IO in resource utilization statistics

In a non-Hyperscale database, combined read and write IOPS against data files relative to the [resource governance](resource-limits-logical-server.md#resource-governance) data IO limit, are reported in [sys.dm_db_resource_stats](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-resource-stats-azure-sql-database/) and [sys.resource_stats](/sql/relational-databases/system-catalog-views/sys-resource-stats-azure-sql-database/) views, in the `avg_data_io_percent` column. The corresponding DMVs for elastic pools are [sys.dm_elastic_pool_resource_stats](/sql/relational-databases/system-dynamic-management-views/sys-dm-elastic-pool-resource-stats-azure-sql-database/) and [sys.elastic_pool_resource_stats](/sql/relational-databases/system-catalog-views/sys-elastic-pool-resource-stats-azure-sql-database/). The same values are reported as the **Data IO Percentage** Azure Monitor metrics for databases and elastic pools.

In a Hyperscale database, these columns and metrics report on the data IO utilization relative to the limit for local SSD storage on compute replica only, which includes I/O against the local SSD cache and the `tempdb` database. A 100% value in this column indicates that resource governance is limiting local storage IOPS. If this is correlated with a performance problem, tune the workload to generate less IO, or increase the compute size to increase the resource governance **Max Data IOPS** [limit](resource-limits-vcore-single-databases.md). For resource governance of local SSD cache reads and writes, the system counts individual 8-KB IOs, rather than larger IOs that might be issued by the database engine.

Data IO against page servers isn't reported in resource utilization views or via Azure Monitor metrics, but is reported in `sys.dm_io_virtual_file_stats()` as described earlier.

## Related content

- [Hyperscale service tier vCore Limits](resource-limits-vcore-single-databases.md#hyperscale---provisioned-compute---gen5)
- [Monitor Azure SQL workloads with database watcher (preview)](../database-watcher-overview.md)
- [Tune applications and databases for performance in Azure SQL Database](performance-guidance.md)
- [Performance monitoring using Query store](/sql/relational-databases/performance/monitoring-performance-by-using-the-query-store/)
- [Monitor performance using dynamic management views](monitoring-with-dmvs.md)
