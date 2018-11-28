---
title: "Columnstore indexes - what&#39;s new | Microsoft Docs"
ms.custom: ""
ms.date: "03/20/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Columnstore indexes - what&#39;s new
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

  Summary of columnstore features available for each version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and the latest releases of [!INCLUDE[ssSDS](../../includes/sssds-md.md)], [!INCLUDE[ssSDW](../../includes/sssdw-md.md)], and [!INCLUDE[ssPDW](../../includes/sspdw-md.md)].  

 > [!NOTE]
 > For [!INCLUDE[ssSDS](../../includes/sssds-md.md)], columnstore indexes are available in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] Premium tiers, Standard tiers - S3 and above, and all vCore tiers. For [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] SP1 and above, columnstore indexes are available in all editions. For [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] (before SP1) and earlier versions, columnstore indexes are only available in Enterprise Edition.
 
## Feature Summary for Product Releases  
 This table summarizes key features for columnstore indexes and the products in which they are available.  

|Columnstore Index Feature|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]|[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]|[!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]|[!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)]|[!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)]|[!INCLUDE[ssSDW](../../includes/sssdw-md.md)]|  
|-------------------------------|---------------------------|---------------------------|---------------------------|--------------------------------------------|-------------------------|---|  
|Batch mode execution for multi-threaded queries|yes|yes|yes|yes|yes|yes| 
|Batch mode execution for single-threaded queries|||yes|yes|yes|yes|  
|Archival compression option||yes|yes|yes|yes|yes|  
|Snapshot isolation and read-committed snapshot isolation|||yes|yes|yes|yes| 
|Specify columnstore index when creating a table|||yes|yes|yes|yes|  
|Always On supports columnstore indexes|yes|yes|yes|yes|yes|yes| 
|Always On readable secondary supports read-only nonclustered columnstore index|yes|yes|yes|yes|yes|yes|  
|Always On readable secondary supports updateable columnstore indexes|||yes|yes|||  
|Read-only nonclustered columnstore index on heap or B-tree|yes|yes|yes <sup>1</sup>|yes <sup>1</sup>|yes <sup>1</sup>|yes <sup>1</sup>|  
|Updateable nonclustered columnstore index on heap or B-tree|||yes|yes|yes|yes|  
|Additional B-tree indexes allowed on a heap or B-tree that has a nonclustered columnstore index|yes|yes|yes|yes|yes|yes|  
|Updateable clustered columnstore index||yes|yes|yes|yes|yes|  
|B-tree index on a clustered columnstore index|||yes|yes|yes|yes|  
|Columnstore index on a memory-optimized table|||yes|yes|yes|yes|  
|Nonclustered columnstore index definition supports using a filtered condition|||yes|yes|yes|yes|  
|Compression delay option for columnstore indexes in `CREATE TABLE` and `ALTER TABLE`|||yes|yes|yes|yes|
|Columnstore index can have a non-persisted computed column||||yes|||   
  
 <sup>1</sup> To create a read-only nonclustered columnstore index, store the index on a read-only filegroup.  

## [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] 
 [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] adds these new features.

### Functional
- [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)] supports non-persisted computed columns in clustered columnstore indexes. Persisted columns are not supported in clustered columnstore indexes.You cannot create a nonclustered index on a columnstore index that has a computed column. 

## [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]  
 [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] adds key enhancements to improve the performance and flexibility of columnstore indexes. These improvements enhance data warehousing scenarios and enable real-time operational analytics.  
  
### Functional  
  
-   A rowstore table can have one updateable nonclustered columnstore index. Previously, the nonclustered columnstore index was read-only.  
  
-   The nonclustered columnstore index definition supports using a filtered condition. To minimize the performance impact of adding a columnstore index on an OLTP table, use a filtered condition to create a nonclustered columnstore index on only the cold data of your operational workload. 
  
-   An in-memory table can have one columnstore index. You can create it when the table is created or add it later with [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md). Previously, only a disk-based table could have a columnstore index.  
  
-   A clustered columnstore index can have one or more nonclustered rowstore indexes. Previously, the columnstore index did not support nonclustered indexes. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] automatically maintains the nonclustered indexes for DML operations.  
  
-   Support for primary keys and foreign keys by using a B-tree index to enforce these constraints on a clustered columnstore index.  
  
-   Columnstore indexes have a compression delay option that minimizes the impact of the transactional workload on real-time operational analytics.  This option allows for frequently changing rows to stabilize before compressing them into the columnstore. For details, see [CREATE COLUMNSTORE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-columnstore-index-transact-sql.md) and [Get started with Columnstore for real-time operational analytics](../../relational-databases/indexes/get-started-with-columnstore-for-real-time-operational-analytics.md).  
  
### Performance for database compatibility level 120 or 130  
  
-   Columnstore indexes support read committed snapshot isolation level (RCSI) and snapshot isolation (SI). This enables transactional consistent analytics queries with no locks.  
  
-   Columnstore supports index defragmentation by removing deleted rows without the need to explicitly rebuild the index. The `ALTER INDEX ... REORGANIZE` statement removes deleted rows, based on an internally defined policy, from the columnstore as an online operation  
  
-   Columnstore indexes can be access on an AlwaysOn readable secondary replica. You can improve performance for operational analytics by offloading analytics queries to an AlwaysOn secondary replica.  
  
-   To improve performance, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computes the aggregate functions `MIN`, `MAX`, `SUM`, `COUNT`, and `AVG` during table scans when the data type uses no more than 8 bytes, and is not of a string type. Aggregate pushdown is supported with or  without Group By clause for both clustered columnstore indexes and nonclustered columnstore indexes.  
  
-   Predicate pushdown speeds up queries that compare strings of type [v]archar or n[v]archar. This applies to the common comparison operators and includes operators such as LIKE that use bitmap filters. This works with all collations that SQL Server supports.  
  
### Performance for database compatibility level 130  
  
-   New batch mode execution support for queries using any of these operations:  
    -   SORT  
    -   Aggregates with multiple distinct functions. Some examples: `COUNT/COUNT`, `AVG/SUM`, `CHECKSUM_AGG`, `STDEV/STDEVP`.  
    -   Window aggregate functions: `COUNT`, `COUNT_BIG`, `SUM`, `AVG`, `MIN`, `MAX`, and `CLR`.  
    -   Window user-defined aggregates: `CHECKSUM_AGG`, `STDEV`, `STDEVP`, `VAR`, `VARP`, and `GROUPING`.  
    -   Window aggregate analytic functions:  `LAG`, `LEAD`, `FIRST_VALUE`, `LAST_VALUE`, `PERCENTILE_CONT`, `PERCENTILE_DISC`, `CUME_DIST`, and `PERCENT_RANK`.  
-   Single-threaded queries running under `MAXDOP 1` or with a serial query plan execute in batch mode. Previously-only multi-threaded queries ran with batch execution.  
-   Memory optimized table queries can have parallel plans in SQL InterOp mode both when accessing data in rowstore or in columnstore index  
  
### Supportability  
These system views are new for columnstore:  
||| 
|-|-|
|[sys.column_store_row_groups &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-column-store-row-groups-transact-sql.md)|[sys.dm_column_store_object_pool &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-column-store-object-pool-transact-sql.md)|  
|[sys.dm_db_column_store_row_group_operational_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-column-store-row-group-operational-stats-transact-sql.md)|[sys.dm_db_column_store_row_group_physical_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-column-store-row-group-physical-stats-transact-sql.md)|  
|[sys.dm_db_index_operational_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-operational-stats-transact-sql.md)|[sys.dm_db_index_physical_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md)|  
|[sys.internal_partitions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-internal-partitions-transact-sql.md)||  
  
These in-memory OLTP-based DMVs contain updates for columnstore:  
||| 
|-|-|
|[sys.dm_db_xtp_hash_index_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-xtp-hash-index-stats-transact-sql.md)|[sys.dm_db_xtp_index_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-xtp-index-stats-transact-sql.md)|  
|[sys.dm_db_xtp_memory_consumers &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-xtp-memory-consumers-transact-sql.md)|[sys.dm_db_xtp_nonclustered_index_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-xtp-nonclustered-index-stats-transact-sql.md)|  
|[sys.dm_db_xtp_object_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-xtp-object-stats-transact-sql.md)|[sys.dm_db_xtp_table_memory_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-xtp-table-memory-stats-transact-sql.md)|  
  
### Limitations  
  

  
-   For in-memory tables, a columnstore index must include all the columns; the columnstore index cannot have a filtered condition.  
-   For in-memory tables, queries on columnstore indexes run only in InterOP mode, and not in the in-memory native mode. Parallel execution is supported.  
  
## [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]  
 [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] introduced the clustered column store index as the primary storage format. This allowed regular loads as well as update, delete, and insert operations.  
  
-   The table can use a clustered column store index as the primary table storage. No other indexes are allowed on the table, but the clustered column store index is updateable so you can perform regular loads and make changes to individual rows.  
-   The nonclustered column store index continues to have the same functionality as in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] except for additional operators that can now be executed in batch mode. It is still not updateable except by rebuilding, and by using partition switching. The nonclustered columnstore index is supported on disk-based tables only, and not on in-memory tables.  
-   The clustered and nonclustered column store index has an archival compression option that further compresses the data. The archival option is useful for reducing the data size both in memory and on disk, but does slow query performance. It works well for data that is accessed infrequently.  
-   The clustered columnstore index and the nonclustered columnstore index function in a very similar way; they use the same columnar storage format, same query processing engine, and the same set of dynamic management views. The difference is primary versus secondary index types, and the nonclustered columnstore index is read-only.  
-   These operators run in batch mode for multi-threaded queries: scan, filter, project, join, group by, and union all.  
  
## [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]  
 [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] introduced the nonclustered columnstore index as another index type on rowstore tables and batch processing for queries on columnstore data.  
  
-   A rowstore table can have one nonclustered columnstore index.  
-   The columnstore index is read-only. After you create the columnstore index, you cannot update the table by `INSERT`, `DELETE`, and `UPDATE` operations; to perform these operations you must drop the index, update the table and rebuild the columnstore index. You can load additional data into the table by using partition switching. The advantage of partition switching is you can load data without dropping and rebuilding the columnstore index.  
-   The column store index always requires extra storage, typically an additional 10% over rowstore, because it stores a copy of the data.  
-   Batch processing provides 2x or better query performance, but it is only available for parallel query execution.  
  
## See Also  
 [Columnstore Indexes Design Guidance](../../relational-databases/indexes/columnstore-indexes-design-guidance.md)   
 [Columnstore Indexes Data Loading Guidance](../../relational-databases/indexes/columnstore-indexes-data-loading-guidance.md)   
 [Columnstore Indexes Query Performance](../../relational-databases/indexes/columnstore-indexes-query-performance.md)   
 [Get started with Columnstore for real-time operational analytics](../../relational-databases/indexes/get-started-with-columnstore-for-real-time-operational-analytics.md)   
 [Columnstore Indexes for Data Warehousing](../../relational-databases/indexes/columnstore-indexes-data-warehouse.md)   
 [Columnstore Indexes Defragmentation](../../relational-databases/indexes/columnstore-indexes-defragmentation.md) 
  
  
  
