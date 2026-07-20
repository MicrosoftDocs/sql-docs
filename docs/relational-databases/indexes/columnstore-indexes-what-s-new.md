---
title: "What's New in Columnstore Indexes"
description: "This article explains features by version and the latest new features of SQL Server columnstore indexes."
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: wiassaf, dfurman, randolphwest
ms.date: 12/29/2025
ms.service: sql
ms.subservice: table-view-index
ms.topic: whats-new
ms.custom:
  - intro-whats-new
  - ignite-2025
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# What's new in columnstore indexes

[!INCLUDE [sql-asdb-asdbmi-asa-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricdw-fabricsqldb.md)]

Learn about which columnstore features available across SQL platforms and SQL Server versions.

## Feature summary for product releases

This table summarizes key features for columnstore indexes and the products in which they are available.

For feature availability in Azure SQL Managed Instance with a SQL Server [update policy](/azure/azure-sql/managed-instance/update-policy), refer to the column for the corresponding version of SQL Server.

| Columnstore Index Feature | [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)]<sup>1</sup> | [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] | [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] | [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] | [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)] | [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)]<sup>2</sup> and [!INCLUDE [ssazure-sqlmi-autd](../../includes/applies-to-version/ssazure-sqlmi-autd.md)] | [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] and [!INCLUDE [Warehouse](../../includes/fabric-dw.md)] |
| --- | --- | --- | --- | --- | --- | --- | --- |
| Batch mode execution for multi-threaded queries<sup>3</sup> | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Batch mode execution for single-threaded queries | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Archival compression option | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Snapshot isolation and read-committed snapshot isolation | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Specify columnstore index when creating a table | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Always On supports columnstore indexes | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Always On readable secondary supports read-only nonclustered columnstore index | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Always On readable secondary supports updateable columnstore indexes | Yes | Yes | Yes | Yes | Yes | No | No |
| Read-only nonclustered columnstore index on heap or B-tree | Yes<sup>4</sup> | Yes<sup>4</sup> | Yes<sup>4</sup> | Yes<sup>4</sup> | Yes<sup>4</sup> | Yes<sup>4</sup> | Yes<sup>4</sup> |
| Updateable nonclustered columnstore index on heap or B-tree | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Additional B-tree indexes allowed on a heap or B-tree that has a nonclustered columnstore index | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Updateable clustered columnstore index | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| B-tree index on a clustered columnstore index | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Columnstore index on a memory-optimized table | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Nonclustered columnstore index definition supports using a filtered condition | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Compression delay option for columnstore indexes in `CREATE TABLE` and `ALTER TABLE` | Yes | Yes | Yes | Yes | Yes | Yes | Yes |
| Support for nvarchar(max) type | No | Yes | Yes | Yes | Yes | Yes | No<sup>5</sup> |
| Columnstore index can have a non-persisted computed column | No | Yes | Yes | Yes | Yes | No | No |
| Tuple mover background merge support | No | No | Yes | Yes | Yes | Yes | Yes |
| Ordered clustered columnstore indexes | No | No | No | Yes | Yes | Yes | Yes |
| Ordered non-clustered columnstore indexes | No | No | No | No | Yes | Yes | No |
| Online columnstore index create and rebuild | No | No | No | Yes | Yes | Yes | No |
| Online ordered columnstore index create and rebuild | No | No | No | No | Yes | Yes | No |

<sup>1</sup> For [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] SP1 and later versions, columnstore indexes are available in all editions. For [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] (before SP1) and earlier versions, columnstore indexes are only available in the Enterprise Edition.
<sup>2</sup> For [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], columnstore indexes are available in the DTU Premium tiers, DTU Standard tiers - S3 and above, and all vCore tiers.
<sup>3</sup> The degree of parallelism (DOP) for [batch mode](../query-processing-architecture-guide.md#batch-mode-execution) operations is limited to 2 for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Standard Edition and 1 for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Web and Express Editions. This limitation refers to columnstore indexes created over disk-based tables and memory-optimized tables.
<sup>4</sup> To create a read-only nonclustered columnstore index, store the index on a read-only filegroup.
<sup>5</sup> Not supported in dedicated SQL pools but is supported in serverless SQL pool.

## SQL Server 2025 (17.x)

[!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)] added these features:

- Ordered nonclustered columnstore improve query performance in real-time operational analytics.

  For more information, see [Performance tuning with ordered columnstore indexes](ordered-columnstore-indexes.md).

- An ordered columnstore index (either clustered or nonclustered) can now be created or rebuilt online.

  You can specify `ONLINE = ON` in the [CREATE INDEX](../../t-sql/statements/create-index-transact-sql.md) statement when the `ORDER` clause is present. For more information about online index operations, see [Perform index operations online](perform-index-operations-online.md).

- Improved sort quality for ordered clustered columnstore indexes.

  In [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)], when an ordered clustered columnstore index is built online, the sort algorithm uses `tempdb` instead of sorting the data in memory. If `MAXDOP` for the index build is 1, the build produces a fully ordered clustered columnstore index that doesn't have overlapping segments. This can improve performance of queries using the index. However, index build might take longer because of the additional I/O required for spills to `tempdb`. If a clustered columnstore index already exists, queries can continue using it while the fully ordered online index rebuild is in progress.

  For more information, see [Reduce segment overlap and improve query performance](ordered-columnstore-indexes.md#reduce-segment-overlap-and-improve-query-performance).

- Improved database and file shrink operations.

  In previous versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], when a clustered columnstore index includes any columns with LOB data types such as **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, the data pages used by these columns can't be moved by the shrink operations. As the result, shrink might be less effective in reclaiming space in the data files.

  In [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)], both `DBCC SHRINKDATABASE` and `DBCC SHRINKFILE` commands can move data pages used by the LOB columns in columnstore indexes.

## SQL Server 2022 (16.x)

[!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] added these features:

- Ordered clustered columnstore indexes improve performance for queries based on ordered column predicates. Ordered columnstore indexes can improve performance by skipping segments of data altogether. This can drastically reduce IO needed to complete queries on columnstore data. For more information, see [segment elimination](columnstore-indexes-query-performance.md#segment-elimination). For more information, see [CREATE COLUMNSTORE INDEX](../../t-sql/statements/create-columnstore-index-transact-sql.md#order-for-clustered-columnstore) and [Performance tuning with ordered columnstore indexes](ordered-columnstore-indexes.md).
- Predicate pushdown with clustered columnstore rowgroup elimination of strings uses boundary values to optimize string searches. All columnstore indexes benefit from enhanced segment elimination by data type. Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], these segment elimination capabilities extend to string, binary, and GUID data types, and the **datetimeoffset** data type for scale greater than two. Previously, columnstore segment elimination applied only to numeric, date, and time data types, and the **datetimeoffset** data type with scale less than or equal to two. After upgrading to a version of SQL Server that supports string min/max segment elimination ([!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions), the columnstore index doesn't benefit from this feature until it's rebuilt using `ALTER INDEX REBUILD` or `CREATE INDEX WITH (DROP_EXISTING = ON)`.
- Columnstore rowgroup elimination for the prefix of `LIKE` predicates, for example `column LIKE 'string%'`. Segment elimination isn't supported for non-prefix use of `LIKE` such as `column LIKE '%string'`.
- For more information on added features, see [What's new in SQL Server 2022](../../sql-server/what-s-new-in-sql-server-2022.md).

## SQL Server 2019 (15.x)

[!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)] adds these new features:

### Functional

Starting with [!INCLUDE [sql-server-2019](../../includes/sssql19-md.md)], the tuple mover is helped by a background merge task that automatically compresses smaller *open* delta rowgroups that have existed for some time as determined by an internal threshold, or merges *compressed* rowgroups from where a large number of rows has been deleted. Previously, an index reorganize operation was needed to merge rowgroups with partially deleted data. This improves the columnstore index quality over time.

## SQL Server 2017 (14.x)

[!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] adds these new features.

### Functional

- [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)] supports nonpersisted computed columns in clustered columnstore indexes. Persisted computed columns aren't supported in clustered columnstore indexes. You can't create a nonclustered columnstore index on a computed column.

## SQL Server 2016 (13.x)

[!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] adds key enhancements to improve the performance and flexibility of columnstore indexes. These improvements enhance data warehousing scenarios and enable real-time operational analytics.

### Functional

- A rowstore table can have one updateable nonclustered columnstore index. Previously, the nonclustered columnstore index was read-only.

- The nonclustered columnstore index definition supports using a filtered condition. To minimize the performance impact of adding a columnstore index on an OLTP table, use a filtered condition to create a nonclustered columnstore index on only the cold data of your operational workload.

- An in-memory table can have one columnstore index. You can create it when the table is created or add it later with [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md). Previously, only a disk-based table could have a columnstore index.

- A clustered columnstore index can have one or more nonclustered rowstore indexes. Previously, the columnstore index didn't support nonclustered indexes. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] automatically maintains the nonclustered indexes for DML operations.

- Support for primary keys and foreign keys by using a B-tree index to enforce these constraints on a clustered columnstore index.

- Columnstore indexes have a compression delay option that minimizes the impact of the transactional workload on real-time operational analytics. This option allows for frequently changing rows to stabilize before compressing them into the columnstore. For details, see [CREATE COLUMNSTORE INDEX](../../t-sql/statements/create-columnstore-index-transact-sql.md) and [Get started with columnstore indexes for real-time operational analytics](get-started-with-columnstore-for-real-time-operational-analytics.md).

### Performance for database compatibility level 120 or 130

- Columnstore indexes support read committed snapshot isolation level (RCSI) and snapshot isolation (SI). This enables transactional consistent analytics queries with no locks.

- Columnstore supports index defragmentation by removing deleted rows without the need to explicitly rebuild the index. The `ALTER INDEX ... REORGANIZE` statement removes deleted rows, based on an internally defined policy, from the columnstore as an online operation

- Columnstore indexes can be access on an Always On readable secondary replica. You can improve performance for operational analytics by offloading analytics queries to an Always On secondary replica.

- Aggregate Pushdown computes the aggregate functions `MIN`, `MAX`, `SUM`, `COUNT`, and `AVG` during table scans when the data type uses no more than 8 bytes, and isn't a string data type. Aggregate pushdown is supported with or without `GROUP BY` clause for both clustered columnstore indexes and nonclustered columnstore indexes. On [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this enhancement is reserved for Enterprise edition.

- String Predicate pushdown speeds up queries that compare strings of type **varchar**/**char** or **nvarchar**/**nchar**. This applies to the common comparison operators and includes operators such as `LIKE` that use bitmap filters. This works with all supported collations. On [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this enhancement is reserved for Enterprise edition.

- Enhancements for batch mode operations by leveraging vector based hardware capabilities. The [!INCLUDE [ssDE-md](../../includes/ssde-md.md)] detects the level of CPU support for AVX 2 (Advanced Vector Extensions) and SSE 4 (Streaming SIMD Extensions 4) hardware extensions, and uses them if supported. On [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this enhancement is reserved for Enterprise edition.

### Performance for database compatibility level 130

- New batch mode execution support for queries using any of these operations:
  - `SORT`
  - Aggregates with multiple distinct functions. Some examples: `COUNT/COUNT`, `AVG/SUM`, `CHECKSUM_AGG`, `STDEV/STDEVP`
  - Window aggregate functions: `COUNT`, `COUNT_BIG`, `SUM`, `AVG`, `MIN`, `MAX`, and `CLR`
  - Window user-defined aggregates: `CHECKSUM_AGG`, `STDEV`, `STDEVP`, `VAR`, `VARP`, and `GROUPING`
  - Window aggregate analytic functions: `LAG`, `LEAD`, `FIRST_VALUE`, `LAST_VALUE`, `PERCENTILE_CONT`, `PERCENTILE_DISC`, `CUME_DIST`, and `PERCENT_RANK`

- Single-threaded queries running under `MAXDOP 1` or with a serial query plan execute in batch mode. Previously, only multi-threaded queries ran with batch execution.

- Memory optimized table queries can have parallel plans in SQL InterOp mode both when accessing data in rowstore or in columnstore index.

### Supportability

These system views are new for columnstore:

- [sys.column_store_row_groups](../system-catalog-views/sys-column-store-row-groups-transact-sql.md)
- [sys.dm_column_store_object_pool](../system-dynamic-management-views/sys-dm-column-store-object-pool-transact-sql.md)
- [sys.dm_db_column_store_row_group_operational_stats](../system-dynamic-management-views/sys-dm-db-column-store-row-group-operational-stats-transact-sql.md)
- [sys.dm_db_column_store_row_group_physical_stats](../system-dynamic-management-views/sys-dm-db-column-store-row-group-physical-stats-transact-sql.md)
- [sys.dm_db_index_operational_stats](../system-dynamic-management-views/sys-dm-db-index-operational-stats-transact-sql.md)
- [sys.dm_db_index_physical_stats](../system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md)
- [sys.internal_partitions](../system-catalog-views/sys-internal-partitions-transact-sql.md)

These in-memory OLTP-based DMVs contain updates for columnstore:

- [sys.dm_db_xtp_hash_index_stats](../system-dynamic-management-views/sys-dm-db-xtp-hash-index-stats-transact-sql.md)
- [sys.dm_db_xtp_index_stats](../system-dynamic-management-views/sys-dm-db-xtp-index-stats-transact-sql.md)
- [sys.dm_db_xtp_memory_consumers](../system-dynamic-management-views/sys-dm-db-xtp-memory-consumers-transact-sql.md)
- [sys.dm_db_xtp_nonclustered_index_stats](../system-dynamic-management-views/sys-dm-db-xtp-nonclustered-index-stats-transact-sql.md)
- [sys.dm_db_xtp_object_stats](../system-dynamic-management-views/sys-dm-db-xtp-object-stats-transact-sql.md)
- [sys.dm_db_xtp_table_memory_stats](../system-dynamic-management-views/sys-dm-db-xtp-table-memory-stats-transact-sql.md)

### Limitations

- For in-memory tables, a columnstore index must include all the columns; the columnstore index can't have a filtered condition.
- For in-memory tables, queries on columnstore indexes run only in interop mode, and not in the native compilation mode. Parallel execution is supported.

### Known issues

**Applies to**: [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] and older versions, [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)]<sup>2022</sup>

- Data pages used by LOB columns (**varbinary(max)**, **varchar(max)**, and **nvarchar(max)**) in compressed columnstore segments can't be moved by `DBCC SHRINKDATABASE` and `DBCC SHRINKFILE`. This issue is resolved in [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)].

## SQL Server 2014 (12.x)

[!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] introduced the clustered columnstore index as the primary storage format. This allowed regular loads as well as update, delete, and insert operations.

- The table can use a clustered columnstore index as the primary table storage. No other indexes are allowed on the table, but the clustered columnstore index is updateable so you can perform regular loads and make changes to individual rows.
- The nonclustered columnstore index continues to have the same functionality as in [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] except for additional operators that can now be executed in batch mode. It's still not updateable except by rebuilding, and by using partition switching. The nonclustered columnstore index is supported on disk-based tables only, and not on in-memory tables.
- The clustered and nonclustered columnstore index has an archival compression option that further compresses the data. The archival option is useful for reducing the data size both in memory and on disk, but does slow query performance. It works well for data that is accessed infrequently.
- The clustered columnstore index and the nonclustered columnstore index function in a very similar way; they use the same columnar storage format, same query processing engine, and the same set of dynamic management views. The difference is primary versus secondary index types, and the nonclustered columnstore index is read-only.
- These operators run in batch mode for multi-threaded queries: scan, filter, project, join, group by, and union all.

## SQL Server 2012 (11.x)

[!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] introduced the nonclustered columnstore index as another index type on rowstore tables and batch processing for queries on columnstore data.

- A rowstore table can have one nonclustered columnstore index.
- The columnstore index is read-only. After you create the columnstore index, you can't update the table by `INSERT`, `DELETE`, and `UPDATE` operations; to perform these operations you must drop the index, update the table and rebuild the columnstore index. You can load additional data into the table by using partition switching. The advantage of partition switching is you can load data without dropping and rebuilding the columnstore index.
- The columnstore index always requires extra storage, typically an additional 10% over rowstore, because it stores a copy of the data.
- Batch processing provides 2x or better query performance, but it's only available for parallel query execution.

## Related content

- [Columnstore indexes - design guidance](columnstore-indexes-design-guidance.md)
- [Columnstore indexes - data loading guidance](columnstore-indexes-data-loading-guidance.md)
- [Columnstore indexes - query performance](columnstore-indexes-query-performance.md)
- [Get started with columnstore indexes for real-time operational analytics](get-started-with-columnstore-for-real-time-operational-analytics.md)
- [Columnstore indexes in data warehousing](columnstore-indexes-data-warehouse.md)
- [Optimize index maintenance to improve query performance and reduce resource consumption](reorganize-and-rebuild-indexes.md)
