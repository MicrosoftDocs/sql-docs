---
title: "Maintaining indexes optimally to improve performance and reduce resource utilization"
description: This article describes index maintenance concepts, and a recommended strategy to maintain indexes.
author: dimitri-furman
ms.author: dfurman
ms.date: "04/16/2021"
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.index.rebuild.f1"
  - "sql13.swb.indexproperties.fragmentation.f1"
  - "sql13.swb.index.reorg.f1"
helpviewer_keywords:
  - "large object defragmenting"
  - "indexes [SQL Server], reorganizing"
  - "index reorganization [SQL Server]"
  - "reorganizing indexes"
  - "defragmenting large object data types"
  - "index fragmentation [SQL Server]"
  - "index rebuilding [SQL Server]"
  - "rebuilding indexes"
  - "indexes [SQL Server], rebuilding"
  - "defragmenting indexes"
  - "nonclustered indexes [SQL Server], defragmenting"
  - "fragmentation [SQL Server]"
  - "index defragmenting [SQL Server]"
  - "LOB data [SQL Server], defragmenting"
  - "clustered indexes, defragmenting"
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-current||=azuresqldb-mi-current||>=aps-pdw-2016"
---
# Optimize index maintenance to improve query performance and reduce resource consumption

[!INCLUDE[SQL Server Azure SQL Database PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-pdw.md)]

This article helps you decide when and how to perform index maintenance. It covers concepts such as index fragmentation and page density, and their impact on query performance and resource consumption. It describes index maintenance methods, [reorganizing an index](#reorganize-an-index) and [rebuilding an index](#rebuild-an-index), and suggests an index maintenance strategy that balances potential performance improvements against resource consumption required for maintenance.

> [!Note]
> The information in this article does not apply to a dedicated SQL pool in [!INCLUDE[ssazuresynapse_md](../../includes/ssazuresynapse_md.md)]. For information on index maintenance for a dedicated SQL pool in [!INCLUDE[ssazuresynapse_md](../../includes/ssazuresynapse_md.md)], see [Indexing dedicated SQL pool tables in [!INCLUDE[ssazuresynapse_md](../../includes/ssazuresynapse_md.md)]](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-tables-index).

## Concepts: index fragmentation and page density

What is **index fragmentation** and how it impacts performance:

- In B-tree (rowstore) indexes, fragmentation exists when indexes have pages in which the logical ordering within the index, based on the key values of the index, does not match the physical ordering of index pages.

   [!INCLUDE [sql-b-tree](../../includes/sql-b-tree.md)]

- The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] automatically modifies indexes whenever insert, update, or delete operations are made to the underlying data. For example, the addition of rows in a table may cause existing pages in [rowstore indexes](clustered-and-nonclustered-indexes-described.md) to split, making room for the insertion of new rows. Over time these modifications can cause the data in the index to become scattered in the database (fragmented).
- For queries that read many pages using full or range index scans, heavily fragmented indexes can degrade query performance because additional I/O may be required to read the data required by the query. Instead of a small number of large I/O requests, the query would require a larger number of small I/O requests to read the same amount of data.
- When the storage subsystem provides better sequential I/O performance than random I/O performance, index fragmentation can degrade performance because more random I/O is required to read fragmented indexes.

What is **page density** (also known as page fullness) and how it impacts performance:

- Each [page](../pages-and-extents-architecture-guide.md#pages) in the database can contain a variable number of rows. If rows take all space on a page, page density is 100%. If a page is empty, page density is 0%. If a page with 100% density is split in two pages to accommodate a new row, the density of the two new pages is approximately 50%.
- When page density is low, more pages are required to store the same amount of data. This means that more I/O is necessary to read and write this data, and more memory is necessary to cache this data. When memory is limited, fewer pages required by a query will be cached, causing even more disk I/O. Consequently, low page density negatively impacts performance.
- When [!INCLUDE[ssde_md](../../includes/ssde_md.md)] adds rows to a page, it will not fill the page fully if the [fill factor](specify-fill-factor-for-an-index.md) for the index is set to a value other than 100 (or 0, which is equivalent in this context). This causes lower page density, and similarly adds I/O overhead and negatively impacts performance.
- Low page density may increase the number of intermediate B-tree levels. This moderately increases CPU and I/O cost of finding leaf level pages in index scans and seeks.
- When the Query Optimizer compiles a query plan, it considers the cost of I/O needed to read the data required by the query. With low page density, there are more pages to read, therefore the cost of I/O is higher. This can impact query plan choice. For example, as page density decreases over time due to page splits, the optimizer may compile a different plan for the same query, with a different performance and resource consumption profile.

> [!TIP]
> In many workloads, increasing page density results in a greater positive performance impact than reducing fragmentation.
>
> To avoid lowering page density unnecessarily, Microsoft does not recommend setting fill factor to values other than 100 or 0, except in certain cases for indexes experiencing a high number of [page splits](specify-fill-factor-for-an-index.md#page-splits), for example frequently modified indexes with leading columns containing non-sequential GUID values.

### Measuring index fragmentation and page density

Both fragmentation and page density are among the factors to consider when deciding whether to perform index maintenance, and which maintenance method to use.

Fragmentation is defined differently for [rowstore](clustered-and-nonclustered-indexes-described.md) and [columnstore](columnstore-indexes-overview.md) indexes. For rowstore indexes, [sys.dm_db_index_physical_stats()](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md) lets you determine fragmentation and page density in a specific index, all indexes on a table or indexed view, all indexes in a database, or all indexes in all databases. For partitioned indexes, `sys.dm_db_index_physical_stats()` provides this information for each partition.

The result set returned by `sys.dm_db_index_physical_stats` includes the following columns:

|Column|Description|
|------------|-----------------|
|`avg_fragmentation_in_percent`|Logical fragmentation (out-of-order pages in the index).|
|`avg_page_space_used_in_percent`|Average page density.|

For compressed row groups in columnstore indexes, fragmentation is defined as the ratio of deleted rows to total rows, expressed as a percentage. [sys.dm_db_column_store_row_group_physical_stats](../../relational-databases/system-dynamic-management-views/sys-dm-db-column-store-row-group-physical-stats-transact-sql.md) lets you determine the number of total and deleted rows per row group in a specific index, all indexes on a table, or all indexes in a database.

The result set returned by `sys.dm_db_column_store_row_group_physical_stats` includes the following columns:

|Column|Description|
|------------|-----------------|
|`total_rows`|Number of rows physically stored in the row group. For compressed row groups, this includes the rows that are marked as deleted.|
|`deleted_rows`|Number of rows physically stored in a compressed row group that are marked for deletion. 0 for row groups that are in delta store.|

Compressed row group fragmentation in a columnstore index can be computed using this formula:

```sql
100.0*(ISNULL(deleted_rows,0))/NULLIF(total_rows,0)
```

> [!TIP]
> For both rowstore and columnstore indexes, it is especially important to review index or heap fragmentation and page density after a large number of rows has been deleted or updated. For heaps, if there are frequent updates, it may also be needed to review fragmentation periodically to avoid proliferation of forwarding records. For more information about heaps, see [Heaps (Tables without Clustered Indexes)](../../relational-databases/indexes/heaps-tables-without-clustered-indexes.md#heap-structures).

See [Examples](#examples) for sample queries to determine fragmentation and page density.

## Index maintenance methods: reorganize and rebuild

You can reduce index fragmentation and increase page density by using one of the following methods:

- Reorganize an index
- Rebuild an index

> [!NOTE]
> For [partitioned](../../relational-databases/partitions/partitioned-tables-and-indexes.md) indexes, you can use either of the following methods on all partitions or a single partition of an index.

### Reorganize an index

Reorganizing an index is less resource intensive than rebuilding an index. For that reason it should be your preferred index maintenance method, unless there is a specific reason to use index rebuild. Reorganize is always an online operation. This means long-term object-level locks are not held and queries or updates to the underlying table can continue during the `ALTER INDEX ... REORGANIZE` operation.

- For [rowstore indexes](clustered-and-nonclustered-indexes-described.md), the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] defragments only the leaf level of clustered and nonclustered indexes on tables and views by physically reordering the leaf-level pages to match the logical order of the leaf nodes (left to right). Reorganizing also compacts index pages to make page density equal to the [fill factor](../../relational-databases/indexes/specify-fill-factor-for-an-index.md) of the index. To view the fill factor setting, use [sys.indexes](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md). For syntax examples, see [Examples - Rowstore reorganize](../../t-sql/statements/alter-index-transact-sql.md#examples-rowstore-indexes).
- When using [columnstore indexes](columnstore-indexes-overview.md), the delta store may end up with multiple small row groups after inserting, updating, and deleting data over time. Reorganizing a columnstore index forces delta store row groups into compressed row groups in columnstore, and combines smaller compressed row groups into larger row groups. The reorganize operation also physically removes rows that have been marked as deleted in the columnstore. Reorganizing a columnstore index may require additional CPU resources to compress data, which may slow the overall system performance while the operation is running. However, once data is compressed, query performance improves. For syntax examples, see [Examples - Columnstore reorganize](../../t-sql/statements/alter-index-transact-sql.md#examples-columnstore-indexes).

> [!NOTE]
> <a name="bckmergetsk"></a> Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)], [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], and [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)], the tuple-mover is helped by a background merge task that automatically compresses smaller open delta rowgroups that have existed for some time as determined by an internal threshold, or merges compressed rowgroups from where a large number of rows has been deleted. This improves the columnstore index quality over time. For most cases this dismisses the need for issuing `ALTER INDEX ... REORGANIZE` commands.

> [!TIP]
> If you cancel a reorganize operation, or if it is otherwise interrupted, the progress it made to that point is persisted in the database. To reorganize large indexes, the operation can be started and stopped multiple times until it completes.

### Rebuild an index

Rebuilding an index drops and re-creates the index. Depending on the type of index and the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] version, a rebuild operation can be done offline or online. An offline index rebuild usually takes less time than an online rebuild, but it holds object-level locks for the duration of the rebuild operation, blocking queries from accessing the table or view.

An online index rebuild does not require object-level locks until the end of the operation, when a lock must be held for a short duration to complete the rebuild. Depending on the version of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], an online index rebuild can be started as a resumable operation. A resumable index rebuild can be paused, keeping the progress made to that point. A resumable rebuild operation can be resumed after having been paused or interrupted, or aborted if completing the rebuild becomes unnecessary.

For [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax, see [ALTER INDEX REBUILD](../../t-sql/statements/alter-index-transact-sql.md). For more information about online index rebuilds, see [Perform Index Operations Online](../../relational-databases/indexes/perform-index-operations-online.md).

> [!NOTE]
> While an index is being rebuilt online, every modification of data in indexed columns must update an additional copy of the index. This may result in a minor performance degradation of data modification statements during online rebuild.
>
> If an online resumable index operation is paused, this performance impact persists until the resumable operation either completes or is aborted. If you do not intend to complete a resumable index operation, abort it instead of pausing it.

> [!TIP]
> Depending on available resources and workload patterns, specifying a higher than the default `MAXDOP` value in the [ALTER INDEX REBUILD](../../t-sql/statements/alter-index-transact-sql.md) statement may shorten the duration of rebuild at the expense of higher CPU utilization.

- For [rowstore indexes](clustered-and-nonclustered-indexes-described.md), rebuilding removes fragmentation in all levels of the index, and compacts pages based on the specified or current fill factor. When `ALL` is specified, all indexes on the table are dropped and rebuilt in a single operation. When indexes with 128 or more extents are rebuilt, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] defers page deallocations and acquiring the associated locks until after the rebuild completes. For syntax examples, see [Examples - Rowstore rebuild](../../t-sql/statements/alter-index-transact-sql.md#examples-rowstore-indexes).
- For [columnstore indexes](columnstore-indexes-overview.md), rebuilding removes fragmentation, moves any delta store rows into columnstore, and physically deletes rows that have been marked for deletion. For syntax examples, see [Examples - Columnstore rebuild](../../t-sql/statements/alter-index-transact-sql.md#examples-columnstore-indexes).

  > [!TIP]
  > Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], rebuilding the columnstore index is usually not needed since `REORGANIZE` performs the essentials of a rebuild as an online operation.

#### Using index rebuild to recover from data corruption

In earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you could sometimes rebuild a rowstore nonclustered index to correct inconsistencies due to data corruption in the index.

Starting with [!INCLUDE[ssKatmai](../../includes/ssKatmai-md.md)], you may still be able to repair such inconsistencies in the nonclustered index by rebuilding a nonclustered index offline. However, you cannot repair nonclustered index inconsistencies by rebuilding the index online, because the online rebuild mechanism uses the existing nonclustered index as the basis for the rebuild and thus carries over the inconsistency. Rebuilding the index offline can sometimes force a scan of the clustered index (or heap) and so replace the inconsistent data in the nonclustered index with the data from the clustered index or heap.

To ensure that the clustered index or heap is used as the source of data, drop and recreate the nonclustered index instead of rebuilding it. As with earlier versions, we recommend recovering from inconsistencies by restoring the affected data from a backup; however, you may be able to repair nonclustered index inconsistencies by rebuilding it offline or recreating it. For more information, see [DBCC CHECKDB &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md).

### Automatic index and statistics management

Leverage solutions such as [Adaptive Index Defrag](https://github.com/Microsoft/tigertoolbox/tree/master/AdaptiveIndexDefrag) to automatically manage index fragmentation and statistics updates for one or more databases. This procedure automatically chooses whether to rebuild or reorganize an index according to its fragmentation level, amongst other parameters, and update statistics with a linear threshold.

### Considerations specific to rebuilding and reorganizing rowstore indexes

The following scenarios cause all rowstore nonclustered indexes on a table to be automatically rebuilt:

- Creating a clustered index on a table, including recreating the clustered index with a different key using `CREATE CLUSTERED INDEX ... WITH (DROP_EXISTING = ON)`
- Dropping a clustered index, which causes the table to be stored as a heap

The following scenarios do not automatically rebuild all rowstore nonclustered indexes on the same table:

- Rebuilding a clustered index
- Changing the clustered index storage, such as applying a partitioning scheme or moving the clustered index to a different filegroup

> [!IMPORTANT]
> An index cannot be reorganized or rebuilt if the filegroup on which it is located is offline or read-only. When the keyword ALL is specified and one or more indexes are on an offline or read-only filegroup, the statement fails.
>
> While an index rebuild occurs, the physical media must have enough space to store two copies of the index. When the rebuild is finished, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] deletes the original index.

When `ALL` is specified with the `ALTER INDEX ... REORGANIZE` statement, clustered, nonclustered, and XML indexes on the table are reorganized.

Rebuilding or reorganizing small rowstore indexes may not reduce fragmentation. Up to, and including, [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] allocates space using mixed extents. Therefore, pages of small indexes are sometimes stored on mixed extents, which implicitly makes such indexes fragmented. Mixed extents are shared by up to eight objects, so the fragmentation in a small index might not be reduced after reorganizing or rebuilding it.

### Considerations specific to rebuilding a columnstore index

When rebuilding a columnstore index, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] reads all data from the original columnstore index, including the delta store. It combines data into new row groups, and compresses all row groups into columnstore. The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] defragments the columnstore by physically deleting rows that have been marked as deleted.

> [!NOTE]
> Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)], the tuple mover is helped by a background merge task that automatically compresses smaller open delta store row groups that have existed for some time as determined by an internal threshold, or merges compressed row groups where a large number of rows has been deleted. This improves columnstore index quality over time.
> For more information about columnstore terms and concepts, see [Columnstore indexes: Overview](../../relational-databases/indexes/columnstore-indexes-overview.md).

#### Rebuild a partition instead of the entire table

Rebuilding the entire table takes a long time if the index is large, and requires enough disk space to store an additional copy of the entire index during the rebuild.

For partitioned tables, you do not need to rebuild the entire columnstore index if fragmentation is present only in some partitions, for example in partitions where `UPDATE`, `DELETE`, or `MERGE` statements have affected a large number of rows.

Rebuilding a partition after loading or modifying data ensures all data is stored in compressed row groups in columnstore. When the data load process inserts data into a partition using  batches smaller than 102,400 rows, the partition can end up with multiple open row groups in delta store. Rebuilding moves all delta store rows into compressed row groups in columnstore.

### Considerations specific to reorganizing a columnstore index

When reorganizing a columnstore index, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] compresses each closed row group in delta store into columnstore as a compressed row group. Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)], the `REORGANIZE` command performs the following additional defragmentation optimizations online:

- Physically removes rows from a row group when 10% or more of the rows have been logically deleted. For example, if a compressed row group of 1 million rows has 100,000 rows deleted, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] will remove the deleted rows and recompress the row group with 900,000 rows, reducing storage footprint.
- Combines one or more compressed row groups to increase rows per rowgroup, up to the maximum of 1,048,576 rows. For example, if you bulk insert five batches of 102,400 rows each, you will get five compressed row groups. If you run REORGANIZE, these row groups will be merged into one compressed rowgroup with 512,000 rows. This assumes there were no dictionary size or memory limitations.
- The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] attempts to combine row groups in which 10% or more of the rows have been marked as deleted with other row groups. For example, row group 1 is compressed and has 500,000 rows, while rowgroup 21 is compressed and has 1,048,576 rows. Rowgroup 21 has 60% of its rows marked as deleted, which leaves 409,830 rows. The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] favors combining these two row groups to compress a new row group that has 909,830 rows.

After performing data loads, you can have multiple small row groups in the delta store. You can use `ALTER INDEX REORGANIZE` to force these row groups into columnstore, and then combine smaller compressed row groups into larger compressed row groups. The reorganize operation will also remove rows that have been marked as deleted from the columnstore.

> [!NOTE]
> Reorganizing a columnstore index using [!INCLUDE[ssManStudio](../../includes/ssManStudio-md.md)] combines compressed row groups together, but does not force all row groups to be compressed into the columnstore. Closed row groups will be compressed, but open row groups will not be compressed into columnstore.
> To forcibly compress all row groups, use the [!INCLUDE[tsql](../../includes/tsql-md.md)] [example](#TsqlProcedureReorg) that includes `COMPRESS_ALL_ROW_GROUPS = ON`.

## What to consider before performing index maintenance

Index maintenance, performed by either reorganizing or rebuilding an index, is resource-intensive. It causes a significant increase in CPU utilization, memory used, and storage I/O. However, depending on the database workload and other factors, the benefits it provides range from vitally important to minuscule.

To avoid unnecessary resource utilization that may be detrimental to query workloads, Microsoft does not recommend performing index maintenance indiscriminately. Instead, performance benefits from index maintenance should be determined empirically for each workload using the recommended [strategy](#index-maintenance-strategy), and weighed against resource costs and workload impact needed to achieve these benefits.

The likelihood of seeing performance benefits from reorganizing or rebuilding an index is higher when the index is heavily fragmented, or when its page density is low. However, these are not the only things to consider. Factors such as query patterns (transaction processing vs. analytics and reporting), storage subsystem behavior, available memory, and database engine improvements over time all play a role. 

> [!IMPORTANT]
> Index maintenance decisions should be made after considering multiple factors in the specific context of each workload, including the resource cost of maintenance. They should not be based on fixed fragmentation or page density thresholds alone.

### A positive side effect of index rebuild

Customers often observe performance improvements after rebuilding indexes. However, in many cases these improvements are unrelated to reducing fragmentation or increasing page density.

An index rebuild has an important benefit: it updates [statistics](../statistics/statistics.md) on key columns of the index by scanning all rows in the index. This is the equivalent of executing `UPDATE STATISTICS ... WITH FULLSCAN`, which makes statistics current and sometimes improves their quality compared to the default sampled statistics update. When statistics are updated, query plans that reference them are recompiled. If the previous plan for a query was not optimal because of stale statistics, insufficient statistics sampling ratio, or for other reasons, the recompiled plan will often perform better.

Customers often incorrectly attribute this improvement to the index rebuild itself, taking it to be result of reduced fragmentation and increased page density. In reality, the same benefit can often be achieved at much cheaper resource cost by [updating statistics](../statistics/update-statistics.md) instead of rebuilding indexes.

> [!TIP]
> Resource cost of updating statistics is minor compared to index rebuild, and the operation often completes in minutes instead of hours that may be required for index rebuilds.

## Index maintenance strategy

Microsoft recommends that customers consider and adopt the following index maintenance strategy:

- Do not assume that index maintenance will always noticeably improve your workload.
- Measure the specific impact of reorganizing or rebuilding indexes on query performance in your workload. Query Store is a good way to measure the "before maintenance" and "after maintenance" performance using the [A/B testing](../performance/query-store-usage-scenarios.md#ab-testing) technique.
- If you observe that rebuilding indexes improves performance, try replacing it with updating statistics. This may result in a similar improvement. In that case, you may not need to rebuild indexes as frequently, or at all, and instead can perform periodic statistics updates. For some statistics, you may need to increase the sampling ratio using the `WITH SAMPLE ... PERCENT` or `WITH FULLSCAN` clauses (this is not common).
- Monitor index fragmentation and page density over time to see if there is a correlation between these values trending up or down, and query performance. If higher fragmentation or lower page density degrade performance unacceptably, reorganize or rebuild indexes. It is often sufficient to only reorganize or rebuild specific indexes used by queries with degraded performance. This avoids a higher resource cost of maintaining every index in the database.
- Establishing a correlation between fragmentation/page density and performance also lets you determine the frequency of index maintenance. Do not assume that maintenance must be performed on a fixed schedule. A better strategy is to monitor fragmentation and page density, and run index maintenance as needed before performance degrades unacceptably.
- If you have determined that index maintenance is needed and its resource cost is acceptable, perform maintenance during low resource usage times, if any, keeping in mind that resource usage patterns may change over time.

### Index maintenance in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSmifull](../../includes/ssSDSmifull-md.md)]

In addition to the above considerations and strategy, in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSmifull](../../includes/ssSDSmifull-md.md)] it is particularly important to consider the costs and benefits of index maintenance. Customers should perform it only when there is a demonstrated need, and taking into account the following points.

- [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSmifull](../../includes/ssSDSmifull-md.md)] implement [resource governance](/azure/azure-sql/database/resource-limits-logical-server#resource-governance) to set bounds on CPU, memory, and I/O consumption according to the provisioned pricing tier. These bounds apply to all user workloads, including index maintenance. If cumulative resource consumption by all workloads approaches resource bounds, the rebuild or reorganize operation may degrade performance of other workloads due to resource contention. For example, bulk data loads may become slower because transaction log I/O is at 100% due to a concurrent index rebuild. In [!INCLUDE[ssSDSmifull](../../includes/ssSDSmifull-md.md)], this impact can be reduced by running index maintenance in a separate Resource Governor workload group with restricted resource allocation, at the expense of extending index maintenance duration.
- For cost savings, customers often provision databases, elastic pools, and managed instances with minimal resource headroom. The pricing tier is chosen to be sufficient for application workloads. To accommodate a significant increase in resource usage due to index maintenance without degrading application performance, customers may have to provision more resources and increase costs, without necessarily improving application performance.
- In elastic pools, resources are shared across all databases in a pool. Even if a particular database is idle, performing index maintenance on that database may impact application workloads running concurrently in other databases in the same pool. See [Resource management in dense elastic pools](/azure/azure-sql/database/elastic-pool-resource-management) for more information.
- For most types of storage used in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSmifull](../../includes/ssSDSmifull-md.md)], there is no difference in performance between sequential I/O and random I/O. This reduces the impact of index fragmentation on query performance.
- When using either [Read Scale-out](/azure/azure-sql/database/read-scale-out) or [Geo-replication](/azure/azure-sql/database/active-geo-replication-overview) replicas, data latency on replicas often increases while index maintenance is being performed on the primary replica. If a geo-replica is provisioned with insufficient resources to sustain an increase in transaction log generation caused by index maintenance, it may lag far behind the primary, causing the system to reseed it. That makes the replica unavailable until reseeding is complete. Additionally, in Premium and Business Critical service tiers, replicas used for high availability may similarly get far behind the primary during index maintenance. If a failover is required during or soon after index maintenance, it can take longer than expected.
- If an index rebuild runs on the primary replica, and a long-running query executes on a readable replica at the same time, the query may get automatically terminated to prevent blocking the redo thread on the replica.

There are specific but uncommon scenarios when one-time or periodic index maintenance may be needed in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSmifull](../../includes/ssSDSmifull-md.md)]:

- Index maintenance may be required to increase page density and reduce used space in the database, and thus stay within the size limit of the pricing tier. This avoids having to scale up to a higher pricing tier with a higher size limit.
- If it becomes necessary to [shrink data files](/azure/azure-sql/database/file-space-manage), rebuilding or reorganizing indexes before shrinking files will increase page density. This makes the shrink operation faster, because it will need to move fewer pages.

> [!TIP]
> If you have determined that index maintenance is necessary for your [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] and [!INCLUDE[ssSDSmifull](../../includes/ssSDSmifull-md.md)] workloads, you should either reorganize indexes, or use online index rebuild. This lets query workloads access tables while indexes are being rebuilt.
>
> Additionally, making the operation resumable lets you avoid restarting it from the beginning if it gets interrupted by a planned or unplanned database failover. Using resumable index operations is particularly important when indexes are large.

> [!TIP]
> Offline index operations typically complete faster than online operations. They should be used when tables will not be accessed by queries during the operation, for example after loading data into staging tables as part of a sequential ETL process.

## <a name="Restrictions"></a> Limitations and restrictions

Rowstore indexes with more than 128 extents are rebuilt in two separate phases: logical and physical. In the logical phase, the existing allocation units used by the index are marked for deallocation, the data rows are copied and sorted, then moved to new allocation units created to store the rebuilt index. In the physical phase, the allocation units previously marked for deallocation are physically dropped in short transactions that happen in the background, and do not require many locks. For more information about allocation units, see [Pages and Extents Architecture Guide](../../relational-databases/pages-and-extents-architecture-guide.md).

The `ALTER INDEX REORGANIZE` statement requires the data file containing the index to have space available, because the operation can only allocate temporary work pages in the same file, not in another file within the same filegroup. Even though the filegroup might have free space available, the user can still encounter error 1105: `Could not allocate space for object '###' in database '###' because the '###' filegroup is full. Create disk space by deleting unneeded files, dropping objects in the filegroup, adding additional files to the filegroup, or setting autogrowth on for existing files in the filegroup` during the reorganize operation if a data file is out of space.

An index cannot be reorganized when `ALLOW_PAGE_LOCKS` is set to OFF.

Up to [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)], rebuilding a clustered columnstore index is an offline operation. The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] has to acquire an exclusive lock on the table or partition while the rebuild occurs. The data is offline and unavailable during the rebuild even when using `NOLOCK`, Read-committed Snapshot Isolation (RCSI), or Snapshot Isolation. Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)], a clustered columnstore index can be rebuilt using the `ONLINE = ON` option.

> [!WARNING]
> Creating and rebuilding nonaligned indexes on a table with more than 1,000 partitions is possible, but is not supported. Doing so may cause degraded performance or excessive memory consumption during these operations. Microsoft recommends using only [aligned indexes](../partitions/partitioned-tables-and-indexes.md#aligned-index) when the number of partitions exceeds 1,000.

<!--
For an [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] table with an ordered clustered columnstore index, `ALTER INDEX REBUILD` will re-sort the data using tempdb. Monitor tempdb during rebuild operations. If you need more tempdb space, scale up the data warehouse. Scale back down once the index rebuild is complete.

For an [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] table with an ordered clustered columnstore index, `ALTER INDEX REORGANIZE` does not re-sort the data. To resort data, use `ALTER INDEX REBUILD`.
-->

### Statistics limitations

- When an index is **created** or **rebuilt**, statistics are created or updated by scanning all the rows in the table, which is equivalent to using the `FULLSCAN` clause in `CREATE STATISTICS` or `UPDATE STATISTICS`. However, starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], when a partitioned index is created or rebuilt, statistics are not created or updated by scanning all rows in the table. Instead, the default sampling ratio is used. To create or update statistics on partitioned indexes by scanning all rows in the table, use [CREATE STATISTICS](../../t-sql/statements/create-statistics-transact-sql.md) or [UPDATE STATISTICS](../../t-sql/statements/update-statistics-transact-sql.md) with the `FULLSCAN` clause.
- Similarly, when the index creation or rebuild operation is resumable, statistics are created or updated with the default sampling ratio. If statistics were created or last updated with the `PERSIST_SAMPLE_PERCENT` clause set to `ON`, resumable index operations use the persisted sampling ratio to create or update statistics.
- When an index is **reorganized**, statistics are not updated.

## Examples

### To check the fragmentation and page density of a rowstore index using [!INCLUDE[tsql](../../includes/tsql-md.md)]

The following example determines the average fragmentation and page density for all rowstore indexes in the current database. It uses the `SAMPLED` mode to return actionable results quickly. For more accurate results, use the `DETAILED` mode. This requires scanning all index pages, and may take a long time.

```sql
SELECT OBJECT_SCHEMA_NAME(ips.object_id) AS schema_name,
       OBJECT_NAME(ips.object_id) AS object_name,
       i.name AS index_name,
       i.type_desc AS index_type,
       ips.avg_fragmentation_in_percent,
       ips.avg_page_space_used_in_percent,
       ips.page_count,
       ips.alloc_unit_type_desc
FROM sys.dm_db_index_physical_stats(DB_ID(), default, default, default, 'SAMPLED') AS ips
INNER JOIN sys.indexes AS i 
ON ips.object_id = i.object_id
   AND
   ips.index_id = i.index_id
ORDER BY page_count DESC;
```

The previous statement returns a result set similar to the following:

```
schema_name  object_name           index_name                               index_type    avg_fragmentation_in_percent avg_page_space_used_in_percent page_count  alloc_unit_type_desc
------------ --------------------- ---------------------------------------- ------------- ---------------------------- ------------------------------ ----------- --------------------
dbo          FactProductInventory  PK_FactProductInventory                  CLUSTERED     0.390015600624025            99.7244625648629               3846        IN_ROW_DATA
dbo          DimProduct            PK_DimProduct_ProductKey                 CLUSTERED     0                            89.6839757845318               497         LOB_DATA
dbo          DimProduct            PK_DimProduct_ProductKey                 CLUSTERED     0                            80.7132814430442               251         IN_ROW_DATA
dbo          FactFinance           NULL                                     HEAP          0                            99.7982456140351               239         IN_ROW_DATA
dbo          ProspectiveBuyer      PK_ProspectiveBuyer_ProspectiveBuyerKey  CLUSTERED     0                            98.1086236718557               79          IN_ROW_DATA
dbo          DimCustomer           IX_DimCustomer_CustomerAlternateKey      NONCLUSTERED  0                            99.5197553743514               78          IN_ROW_DATA
```

For more information, see [sys.dm_db_index_physical_stats](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md).

### To check the fragmentation of a columnstore index using [!INCLUDE[tsql](../../includes/tsql-md.md)]

The following example determines the average fragmentation for all columnstore indexes with compressed row groups in the current database.

```sql
SELECT OBJECT_SCHEMA_NAME(i.object_id) AS schema_name,
       OBJECT_NAME(i.object_id) AS object_name,
       i.name AS index_name,
       i.type_desc AS index_type,
       100.0 * (ISNULL(SUM(rgs.deleted_rows), 0)) / NULLIF(SUM(rgs.total_rows), 0) AS avg_fragmentation_in_percent
FROM sys.indexes AS i
INNER JOIN sys.dm_db_column_store_row_group_physical_stats AS rgs
ON i.object_id = rgs.object_id
   AND
   i.index_id = rgs.index_id
WHERE rgs.state_desc = 'COMPRESSED'
GROUP BY i.object_id, i.index_id, i.name, i.type_desc
ORDER BY schema_name, object_name, index_name, index_type;
```

The previous statement returns a result set similar to the following:

```
schema_name  object_name            index_name                           index_type                avg_fragmentation_in_percent
------------ ---------------------- ------------------------------------ ------------------------- ----------------------------
Sales        InvoiceLines           NCCX_Sales_InvoiceLines              NONCLUSTERED COLUMNSTORE  0.000000000000000
Sales        OrderLines             NCCX_Sales_OrderLines                NONCLUSTERED COLUMNSTORE  0.000000000000000
Warehouse    StockItemTransactions  CCX_Warehouse_StockItemTransactions  CLUSTERED COLUMNSTORE     4.225346161484279
```

### <a name="SSMSProcedureReorg"></a> Maintaining indexes using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]

#### To reorganize or rebuild an index

1. In Object Explorer, expand the database that contains the table on which you want to reorganize an index.
2. Expand the **Tables** folder.
3. Expand the table on which you want to reorganize an index.
4. Expand the **Indexes** folder.
5. Right-click the index you want to reorganize and select **Reorganize**.
6. In the **Reorganize Indexes** dialog box, verify that the correct index is in the **Indexes to be reorganized** grid and click **OK**.
7. Select the **Compact large object column data** check box to specify that all pages that contain large object (LOB) data are also compacted.
8. Click **OK.**

#### To reorganize all indexes in a table

1. In Object Explorer, Expand the database that contains the table on which you want to reorganize the indexes.
2. Expand the **Tables** folder.
3. Expand the table on which you want to reorganize the indexes.
4. Right-click the **Indexes** folder and select **Reorganize All**.
5. In the **Reorganize Indexes** dialog box, verify that the correct indexes are in the **Indexes to be reorganized**. To remove an index from the **Indexes to be reorganized** grid, select the index and then press the Delete key.
6. Select the **Compact large object column data** check box to specify that all pages that contain large object (LOB) data are also compacted.
7. Click **OK.**

### <a name="TsqlProcedureReorg"></a> Maintaining indexes using [!INCLUDE[tsql](../../includes/tsql-md.md)]

> [!NOTE]
> For more examples about using [!INCLUDE[tsql](../../includes/tsql-md.md)] to rebuild or reorganize indexes, see [ALTER INDEX Examples - Rowstore Indexes](../../t-sql/statements/alter-index-transact-sql.md#examples-rowstore-indexes) and [ALTER INDEX Examples - Columnstore Indexes](../../t-sql/statements/alter-index-transact-sql.md#examples-columnstore-indexes).

#### To reorganize an index

The following example reorganizes the `IX_Employee_OrganizationalLevel_OrganizationalNode` index on the `HumanResources.Employee` table in the `AdventureWorks2016` database.

```sql
ALTER INDEX IX_Employee_OrganizationalLevel_OrganizationalNode
    ON HumanResources.Employee
    REORGANIZE;
```

The following example reorganizes the `IndFactResellerSalesXL_CCI` columnstore index on the `dbo.FactResellerSalesXL_CCI` table in the `AdventureWorksDW2016` database.

```sql
-- This command will force all closed and open row groups into columnstore.
ALTER INDEX IndFactResellerSalesXL_CCI
    ON FactResellerSalesXL_CCI
    REORGANIZE WITH (COMPRESS_ALL_ROW_GROUPS = ON);
```

#### To reorganize all indexes in a table

The following example reorganizes all indexes on the `HumanResources.Employee` table in the `AdventureWorks2016` database.

```sql
ALTER INDEX ALL ON HumanResources.Employee
   REORGANIZE;
```

#### To rebuild an index

The following example rebuilds a single index on the `Employee` table in the `AdventureWorks2016` database.

[!code-sql[IndexDDL#AlterIndex1](../../relational-databases/indexes/codesnippet/tsql/reorganize-and-rebuild-i_1.sql)]

#### To rebuild all indexes in a table

The following example rebuilds all indexes associated with the table in the `AdventureWorks2016` database using the `ALL` keyword. Three options are specified.

[!code-sql[IndexDDL#AlterIndex2](../../relational-databases/indexes/codesnippet/tsql/reorganize-and-rebuild-i_2.sql)]

For more information, see [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md).

## See also

- [SQL Server Index Architecture and Design Guide](../../relational-databases/sql-server-index-design-guide.md)
- [Perform Index Operations Online](../../relational-databases/indexes/perform-index-operations-online.md)
- [ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md)
- [Adaptive Index Defrag](https://github.com/Microsoft/tigertoolbox/tree/master/AdaptiveIndexDefrag)
- [CREATE STATISTICS &#40;Transact-SQL&#41;](../../t-sql/statements/create-statistics-transact-sql.md)
- [UPDATE STATISTICS &#40;Transact-SQL&#41;](../../t-sql/statements/update-statistics-transact-sql.md)
- [Columnstore Indexes Query Performance](../../relational-databases/indexes/columnstore-indexes-query-performance.md)
- [Get started with Columnstore for real-time operational analytics](../../relational-databases/indexes/get-started-with-columnstore-for-real-time-operational-analytics.md)
- [Columnstore Indexes for Data Warehousing](../../relational-databases/indexes/columnstore-indexes-data-warehouse.md)
- [Columnstore indexes and the merge policy for row groups](/archive/blogs/sqlserverstorageengine/columnstore-index-merge-policy-for-reorganize)
