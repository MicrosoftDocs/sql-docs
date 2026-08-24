---
title: Improve the Performance of Full-Text Indexes
description: Improve the Performance of Full-Text Indexes
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/20/2026
ms.service: sql
ms.subservice: search
ms.topic: how-to
helpviewer_keywords:
  - "performance [SQL Server], full-text search"
  - "full-text queries [SQL Server], performance"
  - "crawls [full-text search]"
  - "full-text indexes [SQL Server], performance"
  - "full-text search [SQL Server], performance"
  - "batches [SQL Server], full-text search"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-current || =azuresqldb-mi-current"
---
# Improve the performance of full-text indexes

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article covers common causes of poor performance for full-text indexes and queries, and how to mitigate them.

<a id="causes"></a>

## Common causes of performance issues

This section describes causes for common performance issues when you use full-text indexes.

### Hardware resource issues

Hardware resources such as memory, disk speed, CPU speed, and machine architecture affect the performance of full-text indexing and full-text queries.

Hardware resource limits cause reduced full-text indexing performance.

- **CPU**. If CPU usage by the filter daemon host process (`fdhost.exe`) or the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] process (`sqlservr.exe`) is close to 100 percent, the CPU is the bottleneck.

- **Memory**. A shortage of physical memory can cause a bottleneck.

- **Disk**. If the average disk-waiting queue length is more than two times the number of disk heads, there's a bottleneck on the disk. The primary workaround is to create full-text catalogs that are separate from the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database files and logs. Put the logs, database files, and full-text catalogs on separate disks. Installing faster disks and using RAID can also help improve indexing performance.

### Full-text batching issues

If the system has no hardware bottlenecks, the indexing performance of full-text search mostly depends on the following factors:

- How long it takes the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] to create full-text batches.

- How quickly the filter daemon consumes those batches.

### Full-text index population issues

- **Type of population**. Unlike full population, incremental, manual, and auto change tracking population aren't designed to maximize hardware resources to achieve faster speed. Therefore, the tuning suggestions in this article might not enhance performance for full-text indexing when it uses incremental, manual, or auto change tracking population.

- **Master merge**. When a population finishes, a final merge process merges the index fragments together into one *master* full-text index. This process results in improved query performance since only the *master* index needs to be queried rather than a number of index fragments. Better scoring statistics might be used for relevance ranking. However, the master merge can be I/O intensive because large amounts of data must be written and read when index fragments are merged. Though, it doesn't block incoming queries.

  Master merging a large amount of data can create a long-running transaction, delaying truncation of the transaction log during checkpoint. In this case, under the full recovery model, the transaction log might grow significantly. As a best practice, before reorganizing a large full-text index in a database that uses the full recovery model, ensure that your transaction log contains sufficient space for a long-running transaction. For more information, see [Manage the size of the transaction log file](../logs/manage-the-size-of-the-transaction-log-file.md).

<a id="tuning"></a>

## Tune the performance of full-text indexes

To maximize the performance of your full-text indexes, implement the following best practices:

- To use all CPU cores to the maximum, change `max full-text crawl range` to the number of cores on the system. For more information, see [Server configuration: max full-text crawl range](../../database-engine/configure-windows/max-full-text-crawl-range-server-configuration-option.md).

- Make sure that the base table has a clustered index. Use an integer data type for the first column of the clustered index. Avoid using GUIDs in the first column of the clustered index. A multi-range population on a clustered index can produce the highest population speed. Use an integer data type for the column serving as the full-text key.

- Update the statistics of the base table by using the [UPDATE STATISTICS](../../t-sql/statements/update-statistics-transact-sql.md) statement. More important, update the statistics on the clustered index or the full-text key for a full population. This action helps a multi-range population to generate good partitions on the table.

- Before you perform a full population on a large multi-core computer, temporarily limit the size of the buffer pool by setting the `max server memory` value to leave enough memory for the `fdhost.exe` process and operating system use. For more information, see [Estimate the memory requirements of the filter daemon host process (`fdhost.exe`)](#estimate), later in this article.

- If you use incremental population based on a timestamp column, build a secondary index on the **timestamp** column to improve the performance of incremental population.

<a id="full"></a>

## Troubleshoot the performance of full populations

Refer to the following section to resolve performance issues with full populations.

### Review the full-text crawl logs

To help diagnose performance issues, review the full-text crawl logs.

When an error occurs during a crawl, the Full-Text Search crawl logging facility creates and maintains a crawl log, which is a plain text file. Each crawl log corresponds to a particular full-text catalog. By default, crawl logs for a given instance (in this example, the default instance) are located in `%ProgramFiles%\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\LOG` folder.

The crawl log file follows the following naming scheme:

`SQLFT<DatabaseID><FullTextCatalogID>.[<n>]`

The variable parts of the crawl log file name are the following.

- `<DatabaseID>`: The ID of a database, as a five digit number with leading zeros.

- `<FullTextCatalogID>`: Full-text catalog ID, as a five digit number with leading zeros.

- `<n>`: An integer that indicates one or more crawl logs of the same full-text catalog exist.

For example, `SQLFT0000500008.2` is the crawl log file for a database with database ID = 5, and full-text catalog ID = 8. The 2 at the end of the file name indicates that there are two crawl log files for this database/catalog pair.

### Check physical memory usage

During a full-text population, the `fdhost.exe` or `sqlservr.exe` process can run low on memory, or even run out of memory.

- If the full-text crawl log shows that `fdhost.exe` restarts often or returns error code 8007008, it means one of these processes is running out of memory.

- If `fdhost.exe` produces dumps, particularly on large, multi-core systems, it might be running out of memory.

- For information about memory buffers used by a full-text crawl, see [sys.dm_fts_memory_buffers](../system-dynamic-management-objects/sys-dm-fts-memory-buffers-transact-sql.md).

The possible causes of low memory or out-of-memory issues include the following items:

- **Insufficient memory**. If the amount of physical memory that is available during a full population is zero, the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] buffer pool might be consuming most of the physical memory on the system.

  The `sqlservr.exe` process tries to grab all available memory for the buffer pool, up to the configured maximum server memory. If the `max server memory` allocation is too large, out-of-memory conditions and failure to allocate shared memory can occur for the `fdhost.exe` process.

  Set the `max server memory` value of the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] buffer pool appropriately to solve this issue. For more information, see [Estimate the memory requirements of the filter daemon host process (`fdhost.exe`)](#estimate), later in this article. Reducing the batch size used for full-text indexing might also help.

- **Memory contention**. During a full-text population on a multi-core system, `fdhost.exe` and `sqlservr.exe` can contend for buffer pool memory. The resulting lack of shared memory causes batch retries, memory thrashing, and dumps by the `fdhost.exe` process.

- **Paging issues**. Insufficient page-file size, such as on a system that has a small page file with restricted growth, can also cause the `fdhost.exe` or `sqlservr.exe` process to run out of memory. If the crawl logs don't indicate any memory-related failures, excessive paging is likely causing slow performance.

<a id="estimate"></a>

### Estimate the memory requirements of the filter daemon host process (`fdhost.exe`)

The amount of memory that the `fdhost.exe` process needs for populating depends mainly on the number of full-text crawl ranges it uses, the size of inbound shared memory (ISM), and the maximum number of ISM instances.

You can roughly estimate the memory consumption of the filter daemon host by using the following formula:

`number_of_crawl_ranges * ism_size * max_outstanding_isms * 2`

The default values for the variables in the preceding formula are as follows:

| **Variable** | **Default value** |
| --- | --- |
| *number_of_crawl_ranges* | The number of CPU cores |
| *ism_size* | 1 MB for x86 computers<br /><br />4 MB, 8 MB, or 16 MB for x64 computers, depending on the total physical memory |
| *max_outstanding_isms* | 25 for x86 computers<br /><br />5 for x64 computers |

The following table presents guidelines for estimating the memory requirements of `fdhost.exe`. The formulas in this table use the following values:

- *F*, which is an estimate of memory needed by `fdhost.exe` (in MB).

- *T*, which is the total physical memory available on the system (in MB).

- *M*, which is the optimal `max server memory` setting.

For essential information about the following formulas, see the notes that follow the table.

| Platform | Estimate `fdhost.exe` memory requirements in MB: *F*^1 | Formula for calculating max server memory: *M*^2 |
| --- | --- | --- |
| x86 | *F* = *Number of crawl ranges* \* 50 | *M* = minimum(*T*, 2000) - F - 500 |
| x64 | *F* = *Number of crawl ranges* \* 10 \* 8 | *M* = *T* - *F* - 500 |

1. If multiple full populations are in progress, calculate the `fdhost.exe` memory requirements of each separately, as *F1*, *F2*, and so forth. Then calculate *M* as *T* - &Sigma;(*F*i).

1. 500 MB is an estimate of the memory required by other processes in the system. If the system is doing additional work, increase this value accordingly.

1. *ism_size* is assumed to be 8 MB for x64 platforms.

#### Example: Estimate the memory requirements of `fdhost.exe`

This example is for a 64-bit computer that has 8 GB of RAM and 4 dual-core processors. The first calculation estimates the memory needed by `fdhost.exe` *F*. The number of crawl ranges is `8`.

`F = 8 * 10 * 8 = 640`

The next calculation obtains the optimal value for `max server memory` (*M*). The total physical memory available on this system in MB, (*T*), is `8192`.

`M = 8192 - 640 - 500 = 7052`

#### Example: Set `max server memory`

This example uses the [sp_configure](../system-stored-procedures/sp-configure-transact-sql.md) and [RECONFIGURE](../../t-sql/language-elements/reconfigure-transact-sql.md) [!INCLUDE [tsql](../../includes/tsql-md.md)] statements to set `max server memory` to the value calculated for *M* in the preceding example, `7052`:

```sql
USE master;
GO

EXECUTE sp_configure 'max server memory', 7052;
GO

RECONFIGURE;
GO
```

For more information about the server memory options, see [Server memory configuration options](../../database-engine/configure-windows/server-memory-server-configuration-options.md).

### Check CPU usage

The performance of full populations isn't optimal when the average CPU consumption is lower than about 30 percent. Here are some factors that affect CPU consumption.

- High wait time for pages

  To find out whether a page wait time is high, run the following [!INCLUDE [tsql](../../includes/tsql-md.md)] statement:

  ```sql
  SELECT TOP 10 *
  FROM sys.dm_os_wait_stats
  ORDER BY wait_time_ms DESC;
  ```

  The following table describes the wait types of interest.

  | Wait type | Description | Possible resolution |
  | --- | --- | --- |
  | `PAGEIO_LATCH_SH` (`_EX` or `_UP`) | This wait type could indicate an I/O bottleneck, in which case you typically also see a high average disk-queue length. | Moving the full-text index to a different filegroup on a different disk could help reduce the I/O bottleneck. |
  | `PAGELATCH_EX` (or `_UP`) | This wait type could indicate a lot of contention among threads that are trying to write to the same database file. | Adding files to the filegroup on which the full-text index resides could help alleviate such contention. |

  For more information, see [sys.dm_os_wait_stats](../system-dynamic-management-objects/sys-dm-os-wait-stats-transact-sql.md).

- Inefficiencies in scanning the base table

  A full population scans the base table to produce batches. This table scanning might be inefficient in the following scenarios:

  - If the base table has a high percentage of out-of-row columns that are being full-text indexed, scanning the base table to produce batches might be the bottleneck. In this case, moving the smaller data in-row by using **varchar(max)** or **nvarchar(max)** might help.

  - If the base table is very fragmented, scanning might be inefficient. For information about computing out-of-row data and index fragmentation, see [sys.dm_db_partition_stats](../system-dynamic-management-objects/sys-dm-db-partition-stats-transact-sql.md) and [sys.dm_db_index_physical_stats](../system-dynamic-management-objects/sys-dm-db-index-physical-stats-transact-sql.md).

    To reduce fragmentation, you can reorganize or rebuild the clustered index. For more information, see [Optimize index maintenance to improve query performance and reduce resource consumption](../indexes/reorganize-and-rebuild-indexes.md).

<a id="filters"></a>

## Troubleshoot slow indexing of documents

> [!NOTE]  
> This section describes an issue that only affects customers who index documents (such as Microsoft Word documents) in which other document types are embedded.

The Full-Text Engine uses two types of filters when it populates a full-text index: multithreaded filters and single-threaded filters.

- Some documents, such as Word documents, use multithreaded filters.
- Other documents, such as Adobe Acrobat Portable Document Format (PDF) documents, use single-threaded filters.

For security reasons, filters are loaded by the filter daemon host processes. A server instance uses a multithreaded process for all multithreaded filters and a single-threaded process for all single-threaded filters. When a document that uses a multithreaded filter contains an embedded document that uses a single-threaded filter, the Full-Text Engine launches a single-threaded process for the embedded document. For example, on encountering a Word document that contains a PDF document, the Full-Text Engine uses the multithreaded process for the Word content and launches a single-threaded process for the PDF content. However, a single-threaded filter might not work well in this environment and could destabilize the filtering process.

In certain circumstances where such embedding is common, destabilization might lead to crashes of the process. When this condition occurs, the Full-Text Engine re-routes any failed document (for example, a Word document that contains embedded PDF content) to the single-threaded filtering process. If re-routing occurs frequently, it results in performance degradation of the full-text indexing process.

To work around this issue, mark the filter for the container document (the Word document, in this example) as a single-threaded filter. To mark a filter as a single-threaded filter, set the `ThreadingModel` registry value for the filter to `Apartment Threaded`. For information about single-threaded apartments, see [Understanding and Using COM Threading Models](/previous-versions/ms809971(v=msdn.10)).

## Related content

- [Server memory configuration options](../../database-engine/configure-windows/server-memory-server-configuration-options.md)
- [Server configuration: max full-text crawl range](../../database-engine/configure-windows/max-full-text-crawl-range-server-configuration-option.md)
- [Populate Full-Text Indexes](populate-full-text-indexes.md)
- [Create and manage full-text indexes](create-and-manage-full-text-indexes.md)
- [sys.dm_fts_memory_buffers (Transact-SQL)](../system-dynamic-management-objects/sys-dm-fts-memory-buffers-transact-sql.md)
- [sys.dm_fts_memory_pools (Transact-SQL)](../system-dynamic-management-objects/sys-dm-fts-memory-pools-transact-sql.md)
- [Troubleshoot full-text indexing](troubleshoot-full-text-indexing.md)
- [Full-Text Search architecture](full-text-search.md#architecture)
