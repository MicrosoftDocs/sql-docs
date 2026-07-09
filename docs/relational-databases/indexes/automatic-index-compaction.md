---
title: Automatic Index Compaction
description: Describes the automatic index compaction feature in the SQL Server database engine.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dfurman
ms.date: 07/08/2026
ms.service: sql
ms.topic: concept-article
monikerRange: "=azuresqldb-current || =azuresqldb-mi-current || =fabric-sqldb"
---

# Automatic index compaction (preview)

[!INCLUDE [ssazure-sqldb-sqlmi-autd-fabricsqldb](../../includes/applies-to-version/ssazure-sqldb-sqlmi-autd-fabricsqldb.md)]

Automatic index compaction helps you reduce the consumption of storage space, disk I/O, CPU, memory, and improve workload performance without investing time and effort into index maintenance jobs. Index compaction is performed continuously and with low overhead as the data in the database changes.

> [!NOTE]  
> Automatic index compaction is currently in preview in Azure SQL Database, Azure SQL Managed Instance with the Always-up-to-date [update policy](/azure/azure-sql/managed-instance/update-policy), and SQL database in Fabric.

For answers to common questions, see [Frequently asked questions (FAQ)](#frequently-asked-questions-faq).

## Enable and disable

Automatic index compaction is disabled by default. You can enable or disable it for a database by running the following Transact-SQL statements.

- Enable automatic index compaction:

  ```sql
  ALTER DATABASE [database-name-placeholder]
  SET AUTOMATIC_INDEX_COMPACTION = ON;
  ```

- Disable automatic index compaction:

  ```sql
  ALTER DATABASE [database-name-placeholder]
  SET AUTOMATIC_INDEX_COMPACTION = OFF;
  ```

To see if automatic index compaction is enabled, use the [sys.databases](../system-catalog-views/sys-databases-transact-sql.md) catalog view. For example, to see which databases have automatic index compaction enabled, run the following query:

```sql
SELECT database_id,
       name,
       is_automatic_index_compaction_on
FROM sys.databases;
```

You can also use the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function to check the `IsAutomaticIndexCompactionOn` property.

## Benefits and considerations

Automatic index compaction provides the following benefits:

- You don't need to set up and maintain index maintenance jobs.
- It avoids high resource consumption by index maintenance jobs.
- It reduces the space growth that can occur when data in the database is modified.
- It improves query performance.
  - A query reading a compact index reads fewer pages and therefore requires less disk I/O, CPU, and memory.
  - A compact index is more likely to be selected to improve a query plan.

> [!IMPORTANT]  
> Automatic compaction acts on recently modified pages only. As a result, the overhead of compaction is minimal compared to index rebuild or index reorganization, which process all pages.

While the overhead of the compaction process is minimal, it's not zero. When you enable automatic index compaction, consider:

- If the compaction process moves many rows, you might see an increase in the amount of transaction log write I/O and the size of transaction log backups.
- You might notice a small increase in CPU utilization when compaction occurs, within the low single-digit percent range. This increase isn't common.
- Similar to index reorganization, the compaction process acquires short-term exclusive (`X`) page locks to move rows from one page to another.
  - The concurrency impact is minimal. If a lock on a page can't be acquired immediately, the page is skipped to avoid blocking other queries and processes.
    - Queries might occasionally experience short-term (milliseconds) blocking. This blocking happens if a query tries to acquire a page or a row lock after the compaction process already took an exclusive short-term lock on the page, and isn't common.

## How it works

Automatic index compaction is part of the background [persistent version store (PVS)](../accelerated-database-recovery-concepts.md#adr-recovery-components) cleaner process. This process periodically removes obsolete row versions from data pages. If you enable automatic index compaction for the database, the PVS cleaner also compacts indexes.

As the cleaner visits each page with recently inserted, updated, or deleted rows, it checks whether the current page has free space available, excluding the free space reserved by the fill factor. If so, the cleaner moves rows from the next page to the current page as long as they fit into the free space. This process then advances forward and repeats for a small number of consecutive page pairs following the page being cleaned.

If a page becomes empty after its rows are moved, it is deallocated. As a result, the total number of used pages in the database decreases, [page density](reorganize-and-rebuild-indexes.md#concepts-index-fragmentation-and-page-density) increases, and the consumption of storage space, disk I/O, CPU, and buffer pool memory is reduced.

The following diagram shows a conceptual view of data pages in an index before and after compaction.

:::image type="content" source="media/automatic-index-compaction-before-after.svg" alt-text="Diagram showing a conceptual view of data pages before and after automatic index compaction.":::

The compaction process continues in the background as data is modified and obsolete row versions are cleaned up.

The compaction process might skip some pages because of concurrent activity, such as:

- An active transaction using the page.
- An index build or reorganization in progress.
- A shrink operation in progress.
- A large PVS size or a large number of aborted transactions to clean up from PVS.
  - PVS cleanup is prioritized over automatic compaction. Compaction is suspended if PVS size is 150 GB or greater, or if the number of aborted transactions is 1,000 or greater.

For less common reasons why the compaction process might skip pages, see [Use an extended event to monitor compaction statistics](#use-an-extended-event-to-monitor-compaction-statistics).

If a page is skipped, it's considered for compaction the next time it's processed by the PVS cleaner.

Automatic index compaction isn't available for system tables and for system databases other than `msdb`. Indexes with [disabled page locks](../../t-sql/statements/alter-index-transact-sql.md#row-and-page-locks-options) aren't eligible for automatic compaction.

For more information about data pages, see [Page and extent architecture guide](../pages-and-extents-architecture-guide.md).

For more information about indexes, see [Index architecture and design guide](../sql-server-index-design-guide.md).

## Comparison with index reorganization and index rebuild

Consider the following differences between the traditional index maintenance operations (index reorganization, index rebuild) and automatic index compaction:

| Considerations | Recommendations |
| --- | --- |
| Compaction occurs continuously and with minimal overhead as long as data in the database is modified. | You don't need to set up, monitor, and maintain index maintenance jobs to gain the benefits that these jobs might provide. |
| Unlike index reorganization and rebuild which process all pages, the compaction process only considers the pages modified after you enable automatic index compaction. | If the page density for an index is already low, consider running a one-time index reorganization or index rebuild to increase it. This one-time operation is an extra optimization to increase page density right away. From that point on, automatic compaction keeps indexes compact without any user action. |
| Each index rebuild operation requires a substantial free space in the data files, commonly equal to the size of the index or partition being rebuilt. | You don't need to allocate free space in data files for automatic index compaction or for index reorganization. |
| Unlike index rebuild or index reorganization, compaction doesn't reduce index fragmentation. | Increased page density after compaction is more important than index fragmentation. For most workloads, a higher index fragmentation doesn't affect query performance or resource consumption. |
| When the fill factor for an index is less than 100 percent but the amount of data on a page exceeds the fill factor, neither index compaction nor reorganization moves rows away from the page. An index rebuild creates new pages and fills them according to the fill factor. | For most workloads, a higher page density is preferred. Workloads that require a lower fill factor to reduce page splits might benefit from an occasional index rebuild. The rebuild creates pages with a lower page density that matches the fill factor. |
| Unlike index rebuild, compaction doesn't update statistics on the index. | If [automatic statistics update](../statistics/statistics.md#auto_update_statistics-option) is insufficient for your workload and you rely on the [index rebuild to update statistics](reorganize-and-rebuild-indexes.md#a-positive-side-effect-of-index-rebuild), consider using automatic compaction in combination with a statistics update job. |

For more information about index reorganization and rebuild, see [Optimize index maintenance to improve query performance and reduce resource consumption](reorganize-and-rebuild-indexes.md).

## Page density and index fragmentation

[Page density](reorganize-and-rebuild-indexes.md#concepts-index-fragmentation-and-page-density) and [index fragmentation](reorganize-and-rebuild-indexes.md#concepts-index-fragmentation-and-page-density) are the two metrics that reflect space consumption by the index and can affect query performance. The [sys.dm_db_index_physical_stats](../system-dynamic-management-objects/sys-dm-db-index-physical-stats-transact-sql.md) dynamic management function (DMF) reports these metrics in the `avg_page_space_used_in_percent` and `avg_fragmentation_in_percent` columns respectively.

Compaction increases page density by storing more rows on the same page, which improves performance and reduces resource consumption.

You might observe that index fragmentation is higher when you enable automatic index compaction. If this condition occurs, consider:

| Considerations | Recommendations |
| --- | --- |
| In some write-intensive workloads, pages might split again soon after compaction. Page splits increase index fragmentation. | If workload performance is affected as a result, use a one-time index rebuild with a slightly reduced [fill factor](specify-fill-factor-for-an-index.md) to reduce page splits after compaction.<br /><br />For example, set the fill factor in the 70-95 percent range. However, don't reduce the fill factor unnecessarily or set it too low. Most workloads achieve optimal performance and resource utilization with the fill factor set to the default 100 percent. |
| Deallocating an empty page during compaction can create a gap in the page numbering sequence within an [extent](../pages-and-extents-architecture-guide.md#extents). Gaps within extents increase index fragmentation, potentially reducing the size of [read-ahead](../reading-pages.md#read-ahead) I/O. | Even for workloads that benefit from read-ahead, the performance impact of higher fragmentation is minimized because queries read fewer pages after compaction. |

> [!TIP]  
> For most workloads, the benefits of higher page density outweigh any performance impact from higher index fragmentation.

## Monitor automatic index compaction

To see the effectiveness of automatic index compaction, you can monitor key index metrics such as the number of pages, the average page density, and the average index fragmentation over time. For more information, see the [Determine key index metrics](#determine-key-index-metrics) example.

You can monitor cumulative compaction statistics for each partition of an index by using the [sys.dm_db_index_operational_stats](../system-dynamic-management-objects/sys-dm-db-index-operational-stats-transact-sql.md) dynamic management function. These statistics include completed and skipped compaction attempts, moved rows, and deallocated pages. For more information, see the [View compaction statistics for each index partition](#view-compaction-statistics-for-each-index-partition) example.

You can also monitor detailed compaction statistics using [Extended Events](../extended-events/extended-events.md). For more information, see the [Use an extended event to monitor compaction statistics](#use-an-extended-event-to-monitor-compaction-statistics) example.

## Limitations

The automatic index compaction process considers only the pages in the leaf level of a B-tree index that are contained in an `IN_ROW_DATA` allocation unit. This includes pages in:

- Clustered indexes and constraints.
- Nonclustered indexes and constraints.
- B-tree indexes on the internal tables that store special index types such as XML, full-text, spatial, and columnstore internal rowsets.

The following types of pages aren't eligible for automatic index compaction:

- Pages in heap tables.
- Pages in the `ROW_OVERFLOW_DATA` or `LOB_DATA` allocation units.
- Pages in the compressed rowgroups of columnstore indexes.
- Pages in memory-optimized tables.

## Frequently asked questions (FAQ)

This section answers common questions about automatic index compaction.

### Is a restart or an exclusive database access required to enable or disable auto compaction?

No. Compaction starts or stops within minutes after executing the `ALTER DATABASE ... SET AUTOMATIC_INDEX_COMPACTION = ...` command.

### How does it change query performance?

Queries tend to run faster and use less disk I/O, memory, and CPU. This improvement is most noticeable in workloads with significant write activity that would otherwise cause index bloat.

### How does it change the consumption of storage space?

Compaction reduces the growth of used space within data files. However, unlike [database shrink](../databases/shrink-a-database.md), it doesn't reduce the allocated size of data files.

### Is there an overhead?

For most workloads, the overhead isn't noticeable. For write-intensive workloads, you might notice an increase in the transaction log I/O and in the size of transaction log backups.

### Can it cause blocking?

Blocking due to automatic compaction is unlikely. If any blocking occurs, it's short-term and transient (milliseconds).

If a query is blocked, check the command of the head blocker in [sys.dm_exec_requests](../system-dynamic-management-objects/sys-dm-exec-requests-transact-sql.md). Auto compaction might be blocking queries if the command is `VERSION_CLEANER_MAIN` or `VERSION_CLEANER_WORKER`.

### Does it honor the fill factor?

Auto compaction doesn't use the free page space reserved by [fill factor](specify-fill-factor-for-an-index.md). However, if that reserved space is already used by the previous DML statements, then compaction doesn't free it up.

### Does it work if an index uses row or page compression?

Yes. Automatic compaction removes empty space from pages in an index. It doesn't matter if the data on pages is compressed or not.

### How is it different from ghost cleanup?

Ghost cleanup removes soft-deleted rows from pages, which leaves empty space on a page. Automatic compaction removes empty space on pages by consolidating data on fewer pages.

### What happens if I run an index rebuild or an index reorganization while auto compaction is enabled?

Auto compaction skips indexes that are being rebuilt or reorganized, including the indexes in the middle of paused resumable index operations.

## Examples

Run the following T-SQL examples in the user database, not in the `master` database.

### Determine key index metrics

The following query returns the number of pages in the leaf level of an index, its average page density and fragmentation in the `page_count`, `avg_page_space_used_in_percent`, and `avg_fragmentation_in_percent` columns respectively, for indexes that are eligible for automatic compaction. The query also returns a totals row containing these metrics in aggregate for all indexes in the database.

```sql
SELECT COALESCE (OBJECT_SCHEMA_NAME(ips.object_id), '<Total>') AS schema_name,
       COALESCE (OBJECT_NAME(ips.object_id), '<Total>') AS object_name,
       COALESCE (i.name, '<Total>') AS index_name,
       COALESCE (i.type_desc, '<Total>') AS index_type,
       COALESCE (ips.partition_number, NULL) AS partition_number,
       AVG(ips.avg_page_space_used_in_percent) AS avg_page_space_used_in_percent,
       AVG(ips.avg_fragmentation_in_percent) AS avg_fragmentation_in_percent,
       SUM(ips.record_count) AS record_count,
       SUM(ips.page_count) AS page_count
FROM sys.dm_db_index_physical_stats(DB_ID(), DEFAULT, DEFAULT, DEFAULT, 'SAMPLED') AS ips
     INNER JOIN sys.indexes AS i
         ON ips.object_id = i.object_id
        AND ips.index_id = i.index_id
WHERE i.type_desc IN ('CLUSTERED', 'NONCLUSTERED', 'XML', 'SPATIAL')
      AND ips.index_level = 0
      AND ips.page_count > 0
      AND ips.alloc_unit_type_desc = 'IN_ROW_DATA'
GROUP BY ROLLUP(ips.object_id, i.name, i.type_desc, ips.partition_number)
HAVING ips.object_id IS NULL
       AND ips.object_id IS NULL
       AND i.name IS NULL
       AND i.type_desc IS NULL
       AND ips.partition_number IS NULL
       OR ips.object_id IS NOT NULL
          AND ips.object_id IS NOT NULL
          AND i.name IS NOT NULL
          AND i.type_desc IS NOT NULL
          AND ips.partition_number IS NOT NULL
ORDER BY IIF (ips.object_id IS NULL, 0, 1), page_count DESC;
```

The query returns approximate results by sampling a subset of pages. Change `SAMPLED` to `DETAILED` to get more precise results. Using `DETAILED` for large databases can take much longer because all eligible indexes in database are fully scanned. For more information, see [sys.dm_db_index_physical_stats](../system-dynamic-management-objects/sys-dm-db-index-physical-stats-transact-sql.md).

### View compaction statistics for each index partition

You can see automatic compaction statistics for each partition of an index in the current database by using the following query:

```sql
SELECT OBJECT_SCHEMA_NAME(ios.object_id) AS schema_name,
       OBJECT_NAME(ios.object_id) AS object_name,
       i.name AS index_name,
       i.type_desc AS index_type,
       ios.partition_number AS partition_number,
       ios.compaction_attempt_count,
       ios.compaction_complete_count,
       ios.compaction_skip_count,
       ios.compaction_ineligible_count,
       ios.compaction_failure_count,
       ios.compaction_row_move_count,
       ios.compaction_page_deallocation_count
FROM sys.dm_db_index_operational_stats(DB_ID(), DEFAULT, DEFAULT, DEFAULT) AS ios
     INNER JOIN sys.indexes AS i
         ON ios.object_id = i.object_id
        AND ios.index_id = i.index_id
WHERE i.type_desc IN ('CLUSTERED', 'NONCLUSTERED', 'XML', 'SPATIAL')
      AND OBJECT_SCHEMA_NAME(ios.object_id) <> 'sys';
```

For more information, see [sys.dm_db_index_operational_stats](../system-dynamic-management-objects/sys-dm-db-index-operational-stats-transact-sql.md).

### Use an extended event to monitor compaction statistics

You can use the `auto_index_compaction_stats` extended event to monitor compaction statistics for a database. The event fires every 10 minutes. It contains data such as the number of rows moved between pages to compact an index, the number of deallocated pages, and the number of compaction attempts skipped for various reasons. The statistics reported by each event are cumulative since the database engine startup.

The following T-SQL example creates and starts an event session that collects the `auto_index_compaction_stats` event data in a [ring_buffer](../extended-events/targets-for-extended-events-in-sql-server.md#ring_buffer-target) target. A query compares the current and the previous event to return compaction statistics for each 10-minute interval. Because the query requires at least two events to calculate statistics for a time interval, it might not return any data for up to 20 minutes after the event session starts.

```sql
/*
Create and start an event session collecting the auto_index_compaction_stats event
into a ring_buffer target
*/
IF NOT EXISTS (SELECT 1
               FROM sys.dm_xe_database_sessions
               WHERE name = N'automatic_index_compaction')
  BEGIN
      CREATE EVENT SESSION automatic_index_compaction ON DATABASE
      ADD EVENT sqlserver.auto_index_compaction_stats
      ADD TARGET package0.ring_buffer (SET MAX_MEMORY = 1024);

      ALTER EVENT SESSION automatic_index_compaction ON DATABASE STATE = START;
  END

/* Get event data from the ring_buffer target */
DECLARE @EventData AS XML = (SELECT CAST (xst.target_data AS XML) AS TargetData
                             FROM sys.dm_xe_database_session_targets AS xst
                                  INNER JOIN sys.dm_xe_database_sessions AS xs
                                      ON xst.event_session_address = xs.address
                             WHERE xs.name = N'automatic_index_compaction');

/* Return statistics for each 10-minute interval */
WITH compaction_stats_event AS (
    SELECT d.value('@timestamp', 'datetimeoffset') AS timestamp,
           d.value('(data[@name = "database_id"]/value/text())[1]', 'smallint') AS database_id,
           d.value('(data[@name = "compact_attempts"]/value/text())[1]', 'bigint') AS compact_attempts,
           d.value('(data[@name = "compact_completed"]/value/text())[1]', 'bigint') AS compact_completed,
           d.value('(data[@name = "pages_deallocated_compaction"]/value/text())[1]', 'bigint') AS pages_deallocated_compaction,
           d.value('(data[@name = "rows_moved"]/value/text())[1]', 'bigint') AS rows_moved
    FROM @EventData.nodes('/RingBufferTarget/event') AS e(d)
    WHERE e.d.value('@name', 'sysname') = 'auto_index_compaction_stats'
),
timestamp_map AS (
    SELECT database_id,
           timestamp,
           LAG(timestamp) OVER (PARTITION BY database_id ORDER BY timestamp) AS previous_timestamp
    FROM compaction_stats_event
)
SELECT c.timestamp,
       c.database_id,
       c.compact_attempts - p.compact_attempts AS compact_attempts,
       c.compact_completed - p.compact_completed AS compact_completed,
       c.pages_deallocated_compaction - p.pages_deallocated_compaction AS pages_deallocated_compaction,
       c.rows_moved - p.rows_moved AS rows_moved
FROM compaction_stats_event AS c
     INNER JOIN timestamp_map AS tm
         ON c.timestamp = tm.timestamp
        AND c.database_id = tm.database_id
     INNER JOIN compaction_stats_event AS p
         ON tm.previous_timestamp = p.timestamp
        AND tm.database_id = p.database_id
ORDER BY timestamp DESC;
```

You can modify the previous query to include other event fields and return more statistics, such as the number of compaction attempts skipped for various reasons. Use the following query to see all fields of the `auto_index_compaction_stats` event and their descriptions.

```sql
SELECT name,
       type_name,
       description
FROM sys.dm_xe_object_columns
WHERE object_name = N'auto_index_compaction_stats'
      AND column_type = N'data';
```

## Send feedback

Microsoft is eager to hear your feedback regarding automatic index compaction. Send product feedback by posting a new idea in the [SQL feedback forum](https://aka.ms/sqlfeedback). Other community members can upvote and comment on your ideas and suggestions. Community votes and comments help Microsoft plan and prioritize product improvements.

## Related content

- [Optimize index maintenance to improve query performance and reduce resource consumption](reorganize-and-rebuild-indexes.md)
- [Accelerated database recovery](../accelerated-database-recovery-concepts.md)
- [Page and extent architecture guide](../pages-and-extents-architecture-guide.md)
- [Index architecture and design guide](../sql-server-index-design-guide.md)
