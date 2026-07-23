---
title: Database File Space Management
description: This page describes how to manage file space with single and pooled databases in Azure SQL Database. It provides code examples to determine if you need to shrink a single or a pooled database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma, dfurman, randolphwest
ms.date: 07/24/2026
ms.service: azure-sql-database
ms.subservice: deployment-configuration
ms.topic: how-to
ms.custom:
  - sqldbrb=1
monikerRange: "=azuresql-db"
---

# Manage file space for databases in Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> - [Azure SQL Database](file-space-manage.md?view=azuresql-db&preserve-view=true)
> - [Azure SQL Managed Instance](../managed-instance/file-space-manage.md?view=azuresql-mi&preserve-view=true)

This article describes different types of storage space for databases in Azure SQL Database. You might occasionally need to explicitly manage the allocated file space. This article includes the steps to do so.

## Overview

Certain workload patterns can cause the space allocated to data files to become larger than the used space. This condition occurs when the used space increases because of data growth, but you later delete or compress data. The allocated but unused space isn't automatically reclaimed because reclamation is resource-intensive and would slow down future file growth.

You might need to shrink data files and reclaim unused space in the following scenarios:

- To enable data growth for databases in an elastic pool when a large allocated space for some databases in the pool causes the pool to approach its maximum size.
- To enable a decrease in the maximum size of a single database or elastic pool.
- To change the database or an elastic pool to a tier with a lower maximum size limit.
- To reduce storage costs when using the Hyperscale service tier.

> [!CAUTION]  
> Don't consider shrink operations a regular maintenance operation. Data and log files that grow due to regular, recurring business operations don't require shrink operations.

### Monitor file space usage

Azure Resource Manager (ARM) APIs, including PowerShell [get-metrics](/powershell/module/az.monitor/get-azmetric), return the used and allocated space for [databases](/azure/azure-monitor/reference/supported-metrics/microsoft-sql-servers-databases-metrics) and [elastic pools](/azure/azure-monitor/reference/supported-metrics/microsoft-sql-servers-elasticpools-metrics).

The following system views also return the size of used and allocated space for databases and elastic pools:

- [sys.resource_stats](/sql/relational-databases/system-catalog-views/sys-resource-stats-azure-sql-database?view=azuresqldb-current&preserve-view=true)
- [sys.elastic_pool_resource_stats](/sql/relational-databases/system-catalog-views/sys-elastic-pool-resource-stats-azure-sql-database?view=azuresqldb-current&preserve-view=true)
- [sys.dm_elastic_pool_resource_stats](/sql/relational-databases/system-dynamic-management-objects/sys-dm-elastic-pool-resource-stats-azure-sql-database?view=azuresqldb-current&preserve-view=true)

<a id="understanding-types-of-storage-space-for-a-database"></a>

## Understand types of storage space for a database

Understanding the following storage space quantities is important for managing the file space of a database.

| Database quantity | Definition | Comments |
| --- | --- | --- |
| **Data space used** | The amount of space used to store data. | Generally, space used increases (decreases) on inserts (deletes). In some cases, the space used doesn't change on inserts or deletes depending on the amount and pattern of data involved in the operation and any fragmentation. For example, deleting one row from every data page doesn't necessarily decrease the space used. |
| **Data space allocated** | The amount of storage space taken by data files. | The amount of space allocated grows automatically, but never decreases automatically after deletes. This behavior ensures that future inserts are faster since space doesn't need to be re-allocated. |
| **Data space allocated but unused** | The difference between the amount of data space allocated and data space used. | This quantity represents the maximum amount of free space that can be reclaimed by shrinking database data files. |
| **Data max size** | The maximum amount of space that can be used for storing data. | The amount of data space allocated can't grow beyond the data max size. |

The following diagram illustrates the relationship between the different types of storage space for a database.

:::image type="content" source="media/file-space-manage/understand-database-space-quantities.png" alt-text="Diagram that demonstrates the size of difference database space concepts in the database quantity table.":::

### Query a single database for file space information

Use the following query on [sys.database_files](/sql/relational-databases/system-catalog-views/sys-database-files-transact-sql?view=azuresqldb-current&preserve-view=true) to return the amount of database file space allocated and the amount of unused space allocated.

```sql
-- Connect to a user database
SELECT file_id,
       type_desc,
       CAST (FILEPROPERTY(name, 'SpaceUsed') AS DECIMAL (19, 4)) * 8 / 1024. AS space_used_mb,
       CAST (size / 128.0 - CAST (FILEPROPERTY(name, 'SpaceUsed') AS INT) / 128.0 AS DECIMAL (19, 4)) AS space_unused_mb,
       CAST (size AS DECIMAL (19, 4)) * 8 / 1024. AS space_allocated_mb,
       CAST (max_size AS DECIMAL (19, 4)) * 8 / 1024. AS max_size_mb
FROM sys.database_files;
```

<a id="understanding-types-of-storage-space-for-an-elastic-pool"></a>

## Understand types of storage space for an elastic pool

Understanding the following storage space quantities is important for managing the file space of an elastic pool.

| Elastic pool quantity | Definition | Comments |
| --- | --- | --- |
| **Data space used** | The summation of data space used by all databases in the elastic pool. | |
| **Data space allocated** | The summation of storage space taken by data files in all databases in the elastic pool. | |
| **Data space allocated but unused** | The difference between the amount of data space allocated and data space used by all databases in the elastic pool. | This quantity represents the maximum amount of space allocated for the elastic pool that can be reclaimed by shrinking database data files. |
| **Data max size** | The maximum amount of data space that an elastic pool uses for all of its databases. | The space allocated for the elastic pool shouldn't exceed the elastic pool maximum size. If this condition occurs, then the allocated but unused can be reclaimed by shrinking data files. |

The error message "The elastic pool has reached its storage limit" indicates that the database objects consume enough space to meet the elastic pool maximum storage size limit. Consider increasing the storage limit, or free up data space as described in [Reclaim unused allocated space](#reclaim-unused-allocated-space).

## Query an elastic pool for storage space information

Use the following queries to determine storage space quantities for an elastic pool.

### Elastic pool data space used

Use the following example query to return the amount of elastic pool data space used. Modify the elastic pool name parameter to match the name of your pool.

```sql
-- Connect to master
SELECT TOP (1) avg_storage_percent / 100.0 * elastic_pool_storage_limit_mb AS elastic_pool_space_used_mb,
               avg_allocated_storage_percent / 100.0 * elastic_pool_storage_limit_mb AS elastic_pool_space_allocated_mb,
               elastic_pool_storage_limit_mb AS elastic_pool_maximum_size_mb
FROM sys.elastic_pool_resource_stats
WHERE elastic_pool_name = 'ep1'
ORDER BY end_time DESC;
```

## Reclaim unused allocated space

> [!IMPORTANT]  
> Shrink operations consume resources and might affect database performance while running. When possible, run shrink during periods of low usage.

<a id="shrinking-data-files"></a>

### Shrink data files

Because shrinking data files can affect database performance, Azure SQL Database doesn't automatically shrink data files. If necessary, you can shrink data files at a time that you choose. Don't make shrink a regularly scheduled operation. Instead, consider using it only after a major reduction in the used space consumption.

> [!TIP]  
> Don't waste compute resources and time shrinking data files if the regular application workload causes the files to grow to the same allocated size again.

To shrink files, use either `DBCC SHRINKDATABASE` or `DBCC SHRINKFILE` T-SQL commands:

- `DBCC SHRINKDATABASE` shrinks all data and log files in a database with a single command. The command shrinks one data file at a time, which can take a long time for larger databases. It also [shrinks the log file](#shrink-transaction-log-file), which is usually unnecessary because Azure SQL Database shrinks log files automatically as needed.
- `DBCC SHRINKFILE` command supports more advanced scenarios:
  - It can target individual files as needed, rather than shrinking all files in the database.
  - Each `DBCC SHRINKFILE` command can run in parallel with other `DBCC SHRINKFILE` commands to reduce the total time of shrink, at the expense of higher resource usage and a higher chance of temporarily blocking user queries and concurrent `DBCC SHRINKFILE` commands.
  - If the tail of the file doesn't contain data, you can reduce the allocated file size faster by specifying the `TRUNCATEONLY` argument. `TRUNCATEONLY` doesn't require data movement within the file, but it also doesn't reduce the allocated size as much.
- For more information about these shrink commands, see [DBCC SHRINKDATABASE](/sql/t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql?view=azuresqldb-current&preserve-view=true) and [DBCC SHRINKFILE](/sql/t-sql/database-console-commands/dbcc-shrinkfile-transact-sql?view=azuresqldb-current&preserve-view=true).

Run the following examples while connected to the target user database, not the `master` database.

To use `DBCC SHRINKDATABASE` to shrink all data and log files in a given database:

```sql
DBCC SHRINKDATABASE (N'database_name');
```

A database might have one or more data files, created automatically as data grows. To determine the file layout of your database, including the used and allocated size of each file, query the `sys.database_files` catalog view by using the following example script:

```sql
-- Review file properties, including the file_id and name values to use in shrink commands
SELECT file_id,
       name,
       CAST (FILEPROPERTY(name, 'SpaceUsed') AS BIGINT) * 8 / 1024. AS space_used_mb,
       CAST (size AS BIGINT) * 8 / 1024. AS space_allocated_mb,
       CAST (max_size AS BIGINT) * 8 / 1024. AS max_file_size_mb
FROM sys.database_files
WHERE type_desc IN ('ROWS', 'LOG');
```

To shrink a single file, use the `DBCC SHRINKFILE` command, for example:

```sql
-- Shrink database data file named 'data_0` by removing all unused at the end of the file, if any.
DBCC SHRINKFILE ('data_0', TRUNCATEONLY);
```

### Shrink transaction log file

Unlike data files, Azure SQL Database automatically shrinks transaction log file to avoid excessive space usage that can lead to out-of-space errors. In most cases, you don't need to shrink the transaction log file.

In the Premium and Business Critical service tiers, if the transaction log becomes large, it might significantly contribute to local storage consumption toward the [maximum local storage](resource-limits-logical-server.md#storage-space-governance) limit. If local storage consumption is close to the limit, you might choose to shrink transaction log using the `DBCC SHRINKFILE` command as shown in the following example. This releases local storage as soon as the command completes, without waiting for the periodic automatic shrink operation.

Run the following example while connected to the target user database, not the `master` database.

```sql
-- Shrink the database log file (always file_id 2), by removing all unused space at the end of the file, if any.
DBCC SHRINKFILE (2, TRUNCATEONLY);
```

### Auto-shrink

As an alternative to shrinking data files manually, auto-shrink can be enabled for a database. However, auto shrink can be less effective in reclaiming file space than `DBCC SHRINKDATABASE` and `DBCC SHRINKFILE`.

By default, auto-shrink is disabled, which is recommended for most databases. If it becomes necessary to enable auto-shrink, it's recommended to disable it once space management goals are achieved, instead of keeping it enabled permanently. For more information, see [Considerations for AUTO_SHRINK](/troubleshoot/sql/admin/considerations-autogrow-autoshrink#considerations-for-auto_shrink).

For example, auto-shrink can be helpful if an elastic pool contains many databases that continuously experience significant growth and reduction of the used space, causing the pool to approach its maximum size limit. This scenario isn't common.

The auto-shrink database option has no effect in Hyperscale databases.

To enable auto-shrink, execute the following command while connected to your database (not the `master` database).

```sql
-- Enable auto-shrink for the current database.
ALTER DATABASE CURRENT
    SET AUTO_SHRINK ON;
```

For more information about this command, see [DATABASE SET options](/sql/t-sql/statements/alter-database-transact-sql-set-options?view=azuresqldb-current&preserve-view=true).

### Index maintenance after shrink

After a shrink operation completes, indexes might become fragmented. For most workloads on modern platforms, index fragmentation isn't likely to affect performance. For workloads that use large index scans, fragmentation might reduce read I/O throughput. If performance degradation occurs after the shrink operation completes, consider index maintenance to rebuild or reorganize indexes. Index rebuilds require free space in the database, so they might cause the allocated space to increase, counteracting the effect of a shrink.

For more information about index maintenance, see [Optimize index maintenance to improve query performance and reduce resource consumption](/sql/relational-databases/indexes/reorganize-and-rebuild-indexes?view=azuresqldb-current&preserve-view=true).

## Shrink large databases

When the allocated space in a database is in hundreds of gigabytes or higher, shrink might take a long time. Shrink operations can span hours, days, or weeks for multi-terabyte databases. This section describes process optimizations and best practices that make this process more efficient and less impactful to application workloads.

> [!TIP]  
> [ShrinkDriver](https://github.com/microsoft/sql-server-samples/tree/master/samples/features/shrink/shrink-driver) is a PowerShell script that automates and simplifies the shrink process for large databases, turning it into a single, observable, and resumable operation. The script shrinks multiple files in parallel, retries when interrupted, and outputs detailed status reports as it runs.

### Capture space usage baseline

Before starting shrink, capture the current used and allocated space in each database file by executing the following space usage query:

```sql
SELECT file_id,
       CAST (FILEPROPERTY(name, 'SpaceUsed') AS BIGINT) * 8 / 1024. AS space_used_mb,
       CAST (size AS BIGINT) * 8 / 1024. AS space_allocated_mb,
       CAST (max_size AS BIGINT) * 8 / 1024. AS max_size_mb
FROM sys.database_files
WHERE type_desc = 'ROWS';
```

Once shrink has completed, you can execute this query again and compare the result to the initial baseline.

### Truncate data files for a fast but limited gain

If you want to achieve some reduction in the allocated space quickly, consider executing `DBCC SHRINKFILE` with the `TRUNCATEONLY` parameter. If there's any allocated but unused space at the end of the file, the operation removes that space quickly and without any data movement.

However, don't use `TRUNCATEONLY` if your goal is to maximize the reduction in the allocated space. To achieve that goal, you need to run the full shrink process as described later in this section. Because that process truncates files at the end, a separate shrink with `TRUNCATEONLY` has no benefit.

The following example command truncates file ID 4:

```sql
DBCC SHRINKFILE (4, TRUNCATEONLY);
```

After you run this command for every data file, rerun the space usage query to see the reduction in allocated space, if any. You can also view allocated space for the database in the Azure portal.

### Evaluate index page density

As an optional but recommended step, determine the average page density for indexes in the database. For the same amount of data, shrink operations finish faster if page density is high, because the operation moves fewer pages within each file. If page density is low for some indexes, consider performing maintenance on these indexes to increase page density before shrinking data files. A higher page density lets shrink achieve a deeper reduction in the allocated storage space.

To determine page density for all indexes in the database, use the following query. Page density is reported in the `avg_page_space_used_in_percent` column.

```sql
SELECT OBJECT_SCHEMA_NAME(ips.object_id) AS schema_name,
       OBJECT_NAME(ips.object_id) AS object_name,
       i.name AS index_name,
       i.type_desc AS index_type,
       ips.avg_page_space_used_in_percent,
       ips.avg_fragmentation_in_percent,
       ips.page_count,
       ips.alloc_unit_type_desc,
       ips.ghost_record_count
FROM sys.dm_db_index_physical_stats(DB_ID(), DEFAULT, DEFAULT, DEFAULT, 'SAMPLED') AS ips
     INNER JOIN sys.indexes AS i
         ON ips.object_id = i.object_id
        AND ips.index_id = i.index_id
ORDER BY page_count DESC;
```

If there are indexes with high page count (as reported in the `page_count` column) that have page density lower than 60-70%, consider rebuilding or reorganizing these indexes before shrinking data files.

For larger databases, the query to determine page density might take a long time to complete. Rebuilding or reorganizing large indexes also requires substantial time and resource usage. However, index maintenance prior to shrink can reduce shrink duration and achieve higher space savings.

If there are multiple indexes with low page density, you might be able to rebuild them in parallel on multiple database sessions to speed up the process. However, make sure that you aren't approaching database resource limits by doing so. Leave sufficient resource headroom for application workloads that might be running. Monitor resource consumption (CPU, Data IO, Log IO) in the Azure portal or by using the [sys.dm_db_resource_stats](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-resource-stats-azure-sql-database?view=azuresqldb-current&preserve-view=true) view. Start additional index operations only if resource utilization on each of these dimensions remains substantially lower than 100%.

<a id="rebuild-indexes"></a>

#### Example index rebuild command

The following example command uses the [ALTER INDEX](/sql/t-sql/statements/alter-index-transact-sql?view=azuresqldb-current&preserve-view=true) statement to rebuild an index and increase its page density:

```sql
ALTER INDEX [index_name] ON [schema_name].[table_name]
REBUILD WITH (
    FILLFACTOR = 100, MAXDOP = 8, ONLINE = ON (
        WAIT_AT_LOW_PRIORITY (MAX_DURATION = 5 MINUTES, ABORT_AFTER_WAIT = NONE)),
    RESUMABLE = ON
);
```

This command initiates an online and resumable index rebuild. This operation lets concurrent workloads continue using the table while the rebuild is in progress, and lets you resume the rebuild if it gets interrupted for any reason. However, this type of rebuild is slower than an offline rebuild, which blocks access to the table. If no other workloads need to access the table during rebuild, set the `ONLINE` and `RESUMABLE` options to `OFF` and remove the `WAIT_AT_LOW_PRIORITY` clause.

To learn more about index maintenance, see [Optimize index maintenance to improve query performance and reduce resource consumption](/sql/relational-databases/indexes/reorganize-and-rebuild-indexes?view=azuresqldb-current&preserve-view=true).

### Reorganize indexes before shrink

Reorganizing indexes before shrinking can make the shrink operation significantly faster in two scenarios.

1. If the database matches all of the following criteria:

   - It has a large number of data files (more than 10).
   - It has a large number of tables in the database (several hundred or more), collectively using a large amount of space (hundreds of gigabytes or more).
   - A large amount of data is deleted from some tables.

   For such databases, reorganizing indexes on the tables where you deleted data shortens a long-running phase in the shrink process.

1. If the database contains:

   - Large object (LOB) data types such as **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **xml**, or similar data types stored in the `LOB_DATA` allocation unit.
   - [Large rows](/sql/relational-databases/pages-and-extents-architecture-guide#large-row-support) stored in a `ROW_OVERFLOW_DATA` allocation unit.
   - Columnstore indexes.

   To make shrink run faster and release more space in this scenario, make sure to include the `LOB_COMPACTION` clause when you reorganize indexes. LOB compaction before shrink is recommended for all indexes that contain LOB columns or large rows.

   Reorganizing or rebuilding columnstore indexes before shrink can similarly increase shrink speed and effectiveness.

The following example shows a command to reorganize an index and perform LOB compaction:

```sql
ALTER INDEX [index_name] ON [schema_name].[table_name]
REORGANIZE WITH(LOB_COMPACTION = ON);
```

### Shrink multiple data files in parallel

A shrink operation that requires data movement is a long-running process. If the database has multiple data files, you can speed up the process by shrinking multiple data files in parallel. Open multiple database sessions, and use `DBCC SHRINKFILE` on each session with a different `file_id` value. Similar to rebuilding indexes earlier, make sure you have sufficient resource headroom (CPU, Data IO, Log IO) before starting each new parallel shrink command.

The following example command shrinks file ID 4, attempting to reduce its allocated size to 52,000 MB:

```sql
DBCC SHRINKFILE (4, 52000);
```

To reduce allocated space for the file to the minimum possible, execute the statement without specifying the target size:

```sql
DBCC SHRINKFILE (4);
```

If you start too many parallel shrink operations, you might observe high resource utilization and lock contention among shrink operations. For most scenarios, the optimal number of parallel shrink operations is in the four to eight range.

### Shrink in incremental steps

If a shrink operation stops unexpectedly (for example, because of planned or unplanned maintenance), a workload might start using the space freed by shrink before shrink truncates the file, losing some of the progress shrink made so far. Because shrink often runs for a long time, the chances of an interruption are higher.

To avoid this issue, shrink each file in smaller, incremental steps. In the `DBCC SHRINKFILE` command, set the target that is smaller than the current allocated space for the file, but larger than the used space that the [baseline space usage query](#capture-space-usage-baseline) returns.

For example, if allocated space for file ID 4 is 200,000 MB, and you want to shrink it to 100,000 MB, you can first set the target to 180,000 MB:

```sql
DBCC SHRINKFILE (4, 180000);
```

After this command reduces the allocated size to 180,000 MB, you can run shrink again, setting the target first to 160,000 MB, then to 140,000 MB, and continue reducing the target until the file reaches the desired size.

Shrinking files in increments might take longer, but it reduces the risk of repeating shrink for the entire file because of an unexpected interruption.

As a starting point, use an increment in the 10-20 gigabyte range. You can adjust the increment as necessary for your scenario. Larger increments might let you complete file shrink faster, smaller increments reduce the risk of losing progress if shrink is interrupted.

### Monitor shrink operations

To monitor shrink progress for all concurrently running shrink sessions, use the following query:

```sql
SELECT command,
       percent_complete,
       status,
       wait_resource,
       session_id,
       wait_type,
       blocking_session_id,
       cpu_time,
       reads,
       writes,
       CAST (((DATEDIFF(s, start_time, GETDATE())) / 3600) AS VARCHAR) + ' hour(s), '
               + CAST ((DATEDIFF(s, start_time, GETDATE()) % 3600) / 60 AS VARCHAR) + 'min, '
               + CAST ((DATEDIFF(s, start_time, GETDATE()) % 60) AS VARCHAR) + ' sec'
           AS running_time
FROM sys.dm_exec_requests AS r
     LEFT OUTER JOIN sys.databases AS d
         ON r.database_id = d.database_id
WHERE r.command IN ('DbccSpaceReclaim', 'DbccFilesCompact', 'DbccLOBCompact', 'DBCC');
```

> [!NOTE]  
> Shrink progress might be nonlinear, and the value in the `percent_complete` column might remain unchanged for long periods, even though shrink is still in progress. An increase in the `cpu_time`, `reads`, or `writes` values for the same `session_id` between two executions of the query means that shrink continues making progress.

When shrink finishes for all data files successfully, rerun the [space usage query](#capture-space-usage-baseline) (or check in the Azure portal) to see the resulting reduction in allocated storage size. If there's still a large difference between used space and allocated space, [rebuild](#example-index-rebuild-command) or [reorganize](#reorganize-indexes-before-shrink) indexes. An index rebuild might temporarily increase the allocated space. However, shrinking data files again after rebuilding indexes often results in a deeper reduction in the allocated space.

## Transient errors during shrink

Occasionally, a shrink command can fail with errors such as timeouts and deadlocks. These errors are often transient and don't occur again if you repeat the same command. If shrink fails with an error, it retains the progress it made so far. Run the same shrink command again to continue shrinking the file.

The [ShrinkDriver](https://github.com/microsoft/sql-server-samples/tree/master/samples/features/shrink/shrink-driver) PowerShell script automatically retries shrink when a transient error occurs. Use this script to shrink large databases.

The following example T-SQL script shows how to run shrink for a single file in a retry loop. The loop automatically retries the operation up to a configurable number of times when a timeout error or a deadlock error occurs. This retry approach applies to many other errors that might occur during shrink.

```sql
DECLARE @RetryCount AS INT = 3; -- adjust to configure desired number of retries
DECLARE @Delay AS CHAR (12);

-- Retry loop
WHILE @RetryCount >= 0
BEGIN
    BEGIN TRY
        DBCC SHRINKFILE (1); -- adjust file_id and other shrink parameters

        -- Exit retry loop on successful execution
        SELECT @RetryCount = -1;

    END TRY
    BEGIN CATCH
        -- Retry for the declared number of times without raising
        -- an error if deadlocked or timed out waiting for a lock
        IF ERROR_NUMBER() IN (1205, 49516) AND @RetryCount > 0
        BEGIN
            SELECT @RetryCount -= 1;

            PRINT CONCAT('Retry at ', SYSUTCDATETIME());

            -- Wait for a random period of time between 1 and 10 seconds before retrying
            SELECT @Delay = '00:00:0' + CAST (CAST (1 + RAND() * 8.999 AS DECIMAL (5, 3)) AS VARCHAR (5));

            WAITFOR DELAY @Delay;

        END
        ELSE -- Raise error and exit loop
        BEGIN
            SELECT @RetryCount = -1;

            THROW;

        END
    END CATCH
END
```

In addition to timeouts and deadlocks, shrink can encounter errors due to certain known issues.

Review the errors and mitigation steps in the following sections.

### Error number 49503

```output
%.*ls: Page %d:%d could not be moved because it is an off-row persistent version store page. Page holdup reason: %ls. Page holdup timestamp: %I64d.
```

This error occurs when long running active transactions generate row versions in the persistent version store (PVS). Shrink can't move the pages containing row versions.

To mitigate this error, wait until long running transactions complete. Alternatively, identify and terminate long running transactions, but this action can affect your application if it doesn't handle transaction failures gracefully.

For more information about troubleshooting PVS cleanup delays that might affect shrink, see [Monitor and troubleshoot accelerated database recovery](/sql/relational-databases/accelerated-database-recovery-troubleshoot).

### Error number 5223

```output
%.*ls: Empty page %d:%d could not be deallocated.
```

This error can occur during ongoing index maintenance operations such as `ALTER INDEX`. Retry the shrink command after these operations are complete.

If this error persists, you might have to rebuild the associated index. To find the index to rebuild, execute the following query in the same database where you ran the shrink command:

```sql
SELECT OBJECT_SCHEMA_NAME(pg.object_id) AS schema_name,
       OBJECT_NAME(pg.object_id) AS object_name,
       i.name AS index_name,
       p.partition_number
FROM sys.dm_db_page_info(DB_ID(), <file_id>, <page_id>, default) AS pg
INNER JOIN sys.indexes AS i
ON pg.object_id = i.object_id
   AND
   pg.index_id = i.index_id
INNER JOIN sys.partitions AS p
ON pg.partition_id = p.partition_id;
```

Before executing this query, replace the `<file_id>` and `<page_id>` placeholders with the actual values from the error message. For example, if the message is: `Empty page 1:62669 could not be deallocated`, then `<file_id>` is `1` and `<page_id>` is `62669`.

Rebuild the index identified by the query, and retry the shrink command.

### Error number 5201

```output
DBCC SHRINKDATABASE: File ID %d of database ID %d was skipped because the file does not have enough free space to reclaim.
```

This error means that the data file can't be shrunk further. You can move on to the next data file.

## Related content

- [Resource limits for single databases using the vCore purchasing model](resource-limits-vcore-single-databases.md)
- [Resource limits for single databases using the DTU purchasing model - Azure SQL Database](resource-limits-dtu-single-databases.md)
- [Resource limits for elastic pools using the vCore purchasing model](resource-limits-vcore-elastic-pools.md)
- [Resource limits for elastic pools using the DTU purchasing model](resource-limits-dtu-elastic-pools.md)
