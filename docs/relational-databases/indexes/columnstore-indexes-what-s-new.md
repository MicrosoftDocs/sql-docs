---
title: "What's new in columnstore indexes"
description: "This article explains features by version and the latest new features of SQL Server columnstore indexes."
author: MikeRayMSFT
ms.author: mikeray
ms.date: 10/14/2022
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
ms.custom: intro-whats-new
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# What's new in columnstore indexes

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Learn about which columnstore features available for each version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and the latest releases of [!INCLUDE[ssSDS](../../includes/sssds-md.md)], [!INCLUDE[ssSDW](../../includes/sssdw-md.md)], and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)].

## Feature summary for product releases

 This table summarizes key features for columnstore indexes and the products in which they are available.

|Columnstore Index Feature|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|[!INCLUDE[sssql16-md](../../includes/sssql16-md.md)]<sup>1</sup>|[!INCLUDE[ssSQL17](../../includes/sssql17-md.md)]|[!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]|[!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)]|[!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)]<sup>1</sup>|[!INCLUDE[ssSDW](../../includes/sssdw-md.md)]|
|*----|---|---|---|---|---|---|---|---|
|Batch mode execution for multi-threaded queries<sup>2</sup>|yes|yes|yes|yes|yes|yes|yes|yes|
|Batch mode execution for single-threaded queries|||yes|yes|yes|yes|yes|yes|
|Archival compression option||yes|yes|yes|yes|yes|yes|yes|
|Snapshot isolation and read-committed snapshot isolation|||yes|yes|yes|yes|yes|yes|
|Specify columnstore index when creating a table|||yes|yes|yes|yes|yes|yes|
|Always On supports columnstore indexes|yes|yes|yes|yes|yes|yes|yes|yes|
|Always On readable secondary supports read-only nonclustered columnstore index|yes|yes|yes|yes|yes|yes|yes|yes|
|Always On readable secondary supports updateable columnstore indexes|||yes||yes|yes|||
|Read-only nonclustered columnstore index on heap or B-tree|yes|yes|yes <sup>3</sup>|yes <sup>3</sup>|yes <sup>3</sup>|yes <sup>3</sup>|yes <sup>3</sup>|yes <sup>3</sup>|
|Updateable nonclustered columnstore index on heap or B-tree|||yes|yes|yes|yes|yes|yes|
|Additional B-tree indexes allowed on a heap or B-tree that has a nonclustered columnstore index|yes|yes|yes|yes|yes|yes|yes|yes|
|Updateable clustered columnstore index||yes|yes|yes|yes|yes|yes|yes|
|B-tree index on a clustered columnstore index|||yes|yes|yes|yes|yes|yes|
|Columnstore index on a memory-optimized table|||yes|yes|yes|yes|yes|yes|
|Nonclustered columnstore index definition supports using a filtered condition|||yes|yes|yes|yes|yes|yes|
|Compression delay option for columnstore indexes in `CREATE TABLE` and `ALTER TABLE`|||yes|yes|yes|yes|yes|yes|
|Columnstore index can have a non-persisted computed column||||yes|yes|yes|||
|Tuple mover background merge support|||||yes|yes|yes|yes|
|Ordered clustered columnstore indexes||||||yes|yes|yes|

 <sup>1</sup> For [!INCLUDE[ssSDS](../../includes/sssds-md.md)], columnstore indexes are available in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] Premium tiers, Standard tiers - S3 and above, and all vCore tiers. For [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] SP1 and above, columnstore indexes are available in all editions. For [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] (before SP1) and earlier versions, columnstore indexes are only available in Enterprise Edition.

 <sup>2</sup> The degree of parallelism (DOP) for [batch mode](../../relational-databases/query-processing-architecture-guide.md#batch-mode-execution) operations is limited to 2 for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Standard Edition and 1 for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Web and Express Editions. This refers to columnstore indexes created over disk-based tables and memory-optimized tables.

 <sup>3</sup> To create a read-only nonclustered columnstore index, store the index on a read-only filegroup.

## SQL Server 2022 (16.x)

[!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)] adds these new features. For more information, see [What's new in [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)]](../../sql-server/what-s-new-in-sql-server-2022.md).

- Ordered clustered columnstore indexes improve performance for queries based on ordered column predicates. Ordered columnstore indexes can improve performance by skipping segments of data altogether. This can drastically reduce IO needed to complete queries on columnstore data. For more information, see [segment elimination](columnstore-indexes-query-performance.md#segment-elimination). Ordered cluster columnstore indexes are available in [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)]. For more information, see [CREATE COLUMNSTORE INDEX](../../t-sql/statements/create-columnstore-index-transact-sql.md#order) and [Performance tuning with ordered clustered columnstore index](/azure/synapse-analytics/sql-data-warehouse/performance-tuning-ordered-cci).

- Predicate pushdown with clustered columnstore rowgroup elimination of strings uses boundary values to optimize string searches. All columnstore indexes benefit from enhanced segment elimination by data type. Starting with [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], these segment elimination capabilities extend to string, binary, and guid data types, and the datetimeoffset data type for scale greater than two. Previously, columnstore segment elimination applied only to numeric, date, and time data types, and the datetimeoffset data type with scale less than or equal to two. After upgrading to a version of SQL Server that supports string min/max segment elimination ([!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] and later), the columnstore index will not benefit this feature until it is rebuilt using a REBUILD or DROP/CREATE.

## SQL Server 2019 (15.x)

[!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] adds these new features:

### Functional

Starting with [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)], the tuple mover is helped by a background merge task that automatically compresses smaller OPEN delta rowgroups that have existed for some time as determined by an internal threshold, or merges COMPRESSED rowgroups from where a large number of rows has been deleted. Previously, an index reorganize operation was needed to merge rowgroups with partially deleted data. This improves the columnstore index quality over time.

## SQL Server 2017 (14.x)

[!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] adds these new features.

### Functional

- [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)] supports non-persisted computed columns in clustered columnstore indexes. Persisted computed columns are not supported in clustered columnstore indexes. You cannot create a nonclustered columnstore index on a computed column.

## SQL Server 2016 (13.x)

[!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] adds key enhancements to improve the performance and flexibility of columnstore indexes. These improvements enhance data warehousing scenarios and enable real-time operational analytics.

### Functional

- A rowstore table can have one updateable nonclustered columnstore index. Previously, the nonclustered columnstore index was read-only.

- The nonclustered columnstore index definition supports using a filtered condition. To minimize the performance impact of adding a columnstore index on an OLTP table, use a filtered condition to create a nonclustered columnstore index on only the cold data of your operational workload.

- An in-memory table can have one columnstore index. You can create it when the table is created or add it later with [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md). Previously, only a disk-based table could have a columnstore index.

- A clustered columnstore index can have one or more nonclustered rowstore indexes. Previously, the columnstore index did not support nonclustered indexes. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] automatically maintains the nonclustered indexes for DML operations.

- Support for primary keys and foreign keys by using a B-tree index to enforce these constraints on a clustered columnstore index.

- Columnstore indexes have a compression delay option that minimizes the impact of the transactional workload on real-time operational analytics.  This option allows for frequently changing rows to stabilize before compressing them into the columnstore. For details, see [CREATE COLUMNSTORE INDEX (Transact-SQL)](../../t-sql/statements/create-columnstore-index-transact-sql.md) and [Get started with Columnstore for real-time operational analytics](../../relational-databases/indexes/get-started-with-columnstore-for-real-time-operational-analytics.md).

### Performance for database compatibility level 120 or 130

- Columnstore indexes support read committed snapshot isolation level (RCSI) and snapshot isolation (SI). This enables transactional consistent analytics queries with no locks.

- Columnstore supports index defragmentation by removing deleted rows without the need to explicitly rebuild the index. The `ALTER INDEX ... REORGANIZE` statement removes deleted rows, based on an internally defined policy, from the columnstore as an online operation

- Columnstore indexes can be access on an Always On readable secondary replica. You can improve performance for operational analytics by offloading analytics queries to an Always On secondary replica.

- Aggregate Pushdown computes the aggregate functions `MIN`, `MAX`, `SUM`, `COUNT`, and `AVG` during table scans when the data type uses no more than 8 bytes, and is not a string data type. Aggregate pushdown is supported with or without `GROUP BY` clause for both clustered columnstore indexes and nonclustered columnstore indexes. On [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this enhancement is reserved for Enterprise edition.

- String Predicate pushdown speeds up queries that compare strings of type VARCHAR/CHAR or NVARCHAR/NCHAR. This applies to the common comparison operators and includes operators such as `LIKE` that use bitmap filters. This works with all supported collations. On [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this enhancement is reserved for Enterprise edition.

- Enhancements for batch mode operations by leveraging vector based hardware capabilities. The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] detects the level of CPU support for AVX 2 (Advanced Vector Extensions) and SSE 4 (Streaming SIMD Extensions 4) hardware extensions, and uses them if supported. On [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this enhancement is reserved for Enterprise edition.

### Performance for database compatibility level 130

- New batch mode execution support for queries using any of these operations:
    -   `SORT`  
    -   Aggregates with multiple distinct functions. Some examples: `COUNT/COUNT`, `AVG/SUM`, `CHECKSUM_AGG`, `STDEV/STDEVP`  
    -   Window aggregate functions: `COUNT`, `COUNT_BIG`, `SUM`, `AVG`, `MIN`, `MAX`, and `CLR`  
    -   Window user-defined aggregates: `CHECKSUM_AGG`, `STDEV`, `STDEVP`, `VAR`, `VARP`, and `GROUPING`  
    -   Window aggregate analytic functions:  `LAG`, `LEAD`, `FIRST_VALUE`, `LAST_VALUE`, `PERCENTILE_CONT`, `PERCENTILE_DISC`, `CUME_DIST`, and `PERCENT_RANK`

- Single-threaded queries running under `MAXDOP 1` or with a serial query plan execute in batch mode. Previously, only multi-threaded queries ran with batch execution.

- Memory optimized table queries can have parallel plans in SQL InterOp mode both when accessing data in rowstore or in columnstore index.

### Supportability

These system views are new for columnstore:

:::row:::
    :::column:::
        [sys.column_store_row_groups (Transact-SQL)](../../relational-databases/system-catalog-views/sys-column-store-row-groups-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.dm_column_store_object_pool (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-column-store-object-pool-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sys.dm_db_column_store_row_group_operational_stats (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-column-store-row-group-operational-stats-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.dm_db_column_store_row_group_physical_stats (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-column-store-row-group-physical-stats-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sys.dm_db_index_operational_stats (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-operational-stats-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.dm_db_index_physical_stats (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sys.internal_partitions (Transact-SQL)](../../relational-databases/system-catalog-views/sys-internal-partitions-transact-sql.md)
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
    :::column-end:::
    :::column:::
    :::column-end:::
:::row-end:::

These in-memory OLTP-based DMVs contain updates for columnstore:

:::row:::
    :::column:::
        [sys.dm_db_xtp_hash_index_stats (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-xtp-hash-index-stats-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.dm_db_xtp_index_stats (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-xtp-index-stats-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sys.dm_db_xtp_memory_consumers (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-xtp-memory-consumers-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.dm_db_xtp_nonclustered_index_stats (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-xtp-nonclustered-index-stats-transact-sql.md)
    :::column-end:::
:::row-end:::
:::row:::
    :::column:::
        [sys.dm_db_xtp_object_stats (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-xtp-object-stats-transact-sql.md)
    :::column-end:::
    :::column:::
        [sys.dm_db_xtp_table_memory_stats (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-db-xtp-table-memory-stats-transact-sql.md)
    :::column-end:::
:::row-end:::

### Limitations

- For in-memory tables, a columnstore index must include all the columns; the columnstore index cannot have a filtered condition.
- For in-memory tables, queries on columnstore indexes run only in InterOP mode, and not in the in-memory native mode. Parallel execution is supported.

## SQL Server 2014 (12.x)

[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] introduced the clustered column store index as the primary storage format. This allowed regular loads as well as update, delete, and insert operations.

- The table can use a clustered column store index as the primary table storage. No other indexes are allowed on the table, but the clustered column store index is updateable so you can perform regular loads and make changes to individual rows.
- The nonclustered column store index continues to have the same functionality as in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] except for additional operators that can now be executed in batch mode. It is still not updateable except by rebuilding, and by using partition switching. The nonclustered columnstore index is supported on disk-based tables only, and not on in-memory tables.
- The clustered and nonclustered column store index has an archival compression option that further compresses the data. The archival option is useful for reducing the data size both in memory and on disk, but does slow query performance. It works well for data that is accessed infrequently.
- The clustered columnstore index and the nonclustered columnstore index function in a very similar way; they use the same columnar storage format, same query processing engine, and the same set of dynamic management views. The difference is primary versus secondary index types, and the nonclustered columnstore index is read-only.
- These operators run in batch mode for multi-threaded queries: scan, filter, project, join, group by, and union all.

## SQL Server 2012 (11.x)

[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] introduced the nonclustered columnstore index as another index type on rowstore tables and batch processing for queries on columnstore data.

- A rowstore table can have one nonclustered columnstore index.
- The columnstore index is read-only. After you create the columnstore index, you cannot update the table by `INSERT`, `DELETE`, and `UPDATE` operations; to perform these operations you must drop the index, update the table and rebuild the columnstore index. You can load additional data into the table by using partition switching. The advantage of partition switching is you can load data without dropping and rebuilding the columnstore index.
- The column store index always requires extra storage, typically an additional 10% over rowstore, because it stores a copy of the data.
- Batch processing provides 2x or better query performance, but it is only available for parallel query execution.

## Next steps

- [Columnstore Indexes Design Guidance](../../relational-databases/indexes/columnstore-indexes-design-guidance.md)
- [Columnstore Indexes Data Loading Guidance](../../relational-databases/indexes/columnstore-indexes-data-loading-guidance.md)
- [Columnstore Indexes Query Performance](../../relational-databases/indexes/columnstore-indexes-query-performance.md)
- [Get started with Columnstore for real-time operational analytics](../../relational-databases/indexes/get-started-with-columnstore-for-real-time-operational-analytics.md)
- [Columnstore Indexes for Data Warehousing](../../relational-databases/indexes/columnstore-indexes-data-warehouse.md)
- [Reorganize and Rebuild Indexes](../../relational-databases/indexes/reorganize-and-rebuild-indexes.md)
