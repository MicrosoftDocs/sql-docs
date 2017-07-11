---
title: "Columnstore indexes - overview | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "03/07/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "indexes creation, columnstore"
  - "indexes [SQL Server], columnstore"
  - "columnstore index"
  - "columnstore index, described"
  - "xVelocity, columnstore indexes"
ms.assetid: f98af4a5-4523-43b1-be8d-1b03c3217839
caps.latest.revision: 80
author: "barbkess"
ms.author: "barbkess"
manager: "jhubbard"
---
# Columnstore indexes - overview
[!INCLUDE[tsql-appliesto-ss2012-all_md](../../includes/tsql-appliesto-ss2012-all-md.md)]

  The *columnstore index* is the standard for storing and querying large data warehousing fact tables. It uses column-based data storage and query processing to achieve up to **10x query performance** gains in your data warehouse over traditional row-oriented storage, and up to **10x data compression** over the uncompressed data size. Beginning with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], columnstore indexes enable operational analytics, the ability to run performant real-time analytics on a transactional workload.  
  
 Jump to scenarios:  
  
-   [Columnstore Indexes for Data Warehousing](~/relational-databases/indexes/columnstore-indexes-data-warehouse.md)  
  
-   [Get started with Columnstore for real time operational analytics](../../relational-databases/indexes/get-started-with-columnstore-for-real-time-operational-analytics.md)  
  
## What is a columnstore index?  
 A *columnstore index* is a technology for storing, retrieving and managing data by using a columnar data format, called a columnstore.  
  
### Key terms and concepts  
 These are key terms and concepts are associated with columnstore indexes.  
  
 columnstore  
 A *columnstore* is data that is logically organized as a table with rows and columns, and physically stored in a column-wise data format.  
  
 rowstore  
 A *rowstore* is data that is logically organized as a table with rows and columns, and then physically stored in a row-wise data format. This has been the traditional way to store relational table data. In SQL Server, rowstore refers to table where the underlying data storage format is a heap, a clustered index, or a memory-optimized table.  
  
> [!NOTE]  
>  In discussions about columnstore indexes, we use the terms *rowstore* and *columnstore* to emphasize the format for the data storage.  
  
 rowgroup  
 A *row group* is a group of rows that are compressed into columnstore format at the same time. A rowgroup usually contains the maximum number of rows per rowgroup which is 1,048,576 rows.  
  
 For high performance and high compression rates, the columnstore index slices the table into groups of rows, called rowgroups, and then compresses each rowgroup in a column-wise manner. The number of rows in the rowgroup must be large enough to improve compression rates, and small enough to benefit from in-memory operations.  
  
 column segment  
 A *column segment* is a column of data from within the rowgroup.  
  
-   Each rowgroup contains one column segment for every column in the table.  
  
-   Each column segment is compressed together and stored on physical media.  
  
 ![Column segment](../../relational-databases/indexes/media/sql-server-pdw-columnstore-columnsegment.gif "Column segment")  
  
 clustered columnstore index  
 A *clustered columnstore index* is the physical storage for the entire table.  
  
 ![Clustered Columnstore Index](../../relational-databases/indexes/media/sql-server-pdw-columnstore-physicalstorage.gif "Clustered Columnstore Index")  
  
 To reduce fragmentation of the column segments and improve performance, the columnstore index might store some data temporarily into a clustered index, which is called a deltastore, and a btree list of IDs for deleted rows. The deltastore operations are handled behind the scenes. To return the correct query results, the clustered columnstore index combines query results from both the columnstore and the deltastore.  
  
 deltastore  
 Used with clustered column store indexes only, a *deltastore* is a clustered index that improves columnstore compression and performance by storing rows until the number of rows reaches a threshold and are then moved into the columnstore.  
  
 During a large bulk load, most of the rows go directly to the columnstore without passing through the deltastore. Some rows at the end of the bulk load might be too few in number to meet the minimum size of a rowgroup which is 102,400 rows. When this happens, the final rows go to the deltastore instead of the columnstore. For small bulk loads with less than 102,400 rows, all of the rows go directly to the deltastore.  
  
 When the deltastore reaches the maximum number of rows, it becomes closed. A tuple-mover process checks for closed row groups. When it finds the closed rowgroup, it compresses it and stores it into the columnstore.  
  
 nonclustered columnstore index  
 A *nonclustered columnstore index* and a clustered columnstore index function the same. The difference is a nonclustered index is a secondary index created on a rowstore table, whereas a clustered columnstore index is the primary storage for the entire table.  
  
 The nonclustered index contains a copy of part or all of the rows and columns in the underlying table. The index is defined as one or more columns of the table, and has an optional condition that filters the rows.  
  
 A nonclustered columnstore index enables real-time operational analytics in which the OLTP workload uses the underlying clustered index, while analytics run concurrently on the columnstore index. For more information, see [Get started with Columnstore for real time operational analytics](../../relational-databases/indexes/get-started-with-columnstore-for-real-time-operational-analytics.md).  
  
 batch execution  
 *Batch execution* is a query processing method in which queries process multiple rows together. Queries on columnstore indexes use batch mode execution which improves query performance typically 2-4x. Batch execution is closely integrated with, and optimized around, the columnstore storage format. Batch-mode execution is sometimes known as vector-based or vectorized execution.  
  
##  <a name="benefits"></a> Why should I use a columnstore index?  
 A columnstore index can provide a very high level of data compression, typically 10x, to reduce your data warehouse storage cost significantly. Plus, for analytics they offer an order of magnitude better performance than a btree index. They are the preferred data storage format for data warehousing and analytics workloads. Starting with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], you can use columnstore indexes for real-time analytics on your operational workload.  
  
 Reasons why columnstore indexes are so fast:  
  
-   Columns store values from the same domain and commonly have similar values, which results in high compression rates. This minimizes or eliminates IO bottleneck in your system while reducing the memory footprint significantly.  
  
-   High compression rates improve query performance by using a smaller in-memory footprint. In turn, query performance can improve because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can perform more query and data operations in-memory.  
  
-   Batch execution improves query performance, typically 2-4x, by processing multiple rows together.  
  
-   Queries often select only a few columns from a table, which reduces total I/O from the physical media.  
  
## When should I use a columnstore index?  
 Recommended use cases:  
  
-   Use a clustered columnstore index to store fact tables and large dimension tables for data warehousing workloads. This improves query performance and data compression by up to 10x. See [Columnstore Indexes for Data Warehousing](~/relational-databases/indexes/columnstore-indexes-data-warehouse.md).  
  
-   Use a nonclustered columnstore index to perform analysis in real-time on an OLTP workload. See [Get started with Columnstore for real time operational analytics](../../relational-databases/indexes/get-started-with-columnstore-for-real-time-operational-analytics.md).  
  
### How do I choose between a rowstore index and a columnstore index?  
 Rowstore indexes perform best on queries that seek into the data, searching for a particular value, or for queries on a small range of values. Use rowstore indexes with transactional workloads since they tend to require mostly table seeks instead of table scans.  
  
 Columnstore indexes give high performance gains for analytic queries that scan large amounts of data, especially on large tables.  Use columnstore indexes on data warehousing and analytics workloads, especially on fact tables, since they tend to require full table scans rather than table seeks.  
  
### Can I combine rowstore and columnstore on the same table?  
 Yes. Beginning with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], you can  create an updatable  nonclustered columnstore index on a rowstore table. The columnstore index stores a copy of the chosen columns so you do need extra space for this but it will be compressed on average by 10x. By doing this, you can run analytics on the columnstore index and transactions on the rowstore index at the same time. The column store is updated when data changes in the rowstore table, so both indexes are working against the same data.  
  
 Beginning with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], you can have one or more nonclustered rowstore indexes on a columnstore index. By doing this, you can perform efficient table seeks on the underlying columnstore. Other options become available too. For example, you can enforce a primary key constraint by using a UNIQUE constraint on the rowstore table. Since an non-unique value will fail to insert into the rowstore table, SQL Server cannot insert the value into the columnstore.  
  
## Metadata  
 All of the columns in a columnstore index are stored in the metadata as included columns. The columnstore index does not have key columns.  
  
-   [sys.indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)  
  
-   [sys.index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-index-columns-transact-sql.md)  
  
-   [sys.partitions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-partitions-transact-sql.md)  
  
-   [sys.internal_partitions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-internal-partitions-transact-sql.md)  
  
-   [sys.column_store_segments &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-column-store-segments-transact-sql.md)  
  
-   [sys.column_store_dictionaries &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-column-store-dictionaries-transact-sql.md)  
  
-   [sys.column_store_row_groups &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-column-store-row-groups-transact-sql.md)  
  
-   [sys.dm_db_column_store_row_group_operational_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-column-store-row-group-operational-stats-transact-sql.md)  
  
-   [sys.dm_db_column_store_row_group_physical_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-column-store-row-group-physical-stats-transact-sql.md)  
  
-   [sys.dm_column_store_object_pool &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-column-store-object-pool-transact-sql.md)  
  
-   [sys.dm_db_column_store_row_group_operational_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-column-store-row-group-operational-stats-transact-sql.md)  
  
-   [sys.dm_db_index_operational_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-operational-stats-transact-sql.md)  
  
-   [sys.dm_db_index_physical_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md)  
  
## Related Tasks  
 All relational tables, unless you specify them as a clustered columnstore index, use rowstore as the underlying data format. CREATE TABLE creates a rowstore table unless you specify the WITH CLUSTERED COLUMNSTORE INDEX option.  
  
 When you create a table with the CREATE TABLE statement you can create the table as a columnstore by specifying the WITH CLUSTERED COLUMNSTORE INDEX option. If you already have a rowstore table and want to convert it to a columnstore, you can use the CREATE COLUMNSTORE INDEX statement. For examples, see.  
  
|Task|Reference Topics|Notes|  
|----------|----------------------|-----------|  
|Create a table as a columnstore.|[CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)|Beginning with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], you can create the table as a clustered columnstore index. You do not have to first create a rowstore table and then convert it to columnstore.|  
|Create a memory table with a columnstore index.|[CREATE TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-table-transact-sql.md)|Beginning with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], you can create a memory-optimized table with a columnstore index. The columnstore index can also be added after the table is created, using the ALTER TABLE ADD INDEX syntax.|  
|Convert a rowstore table to a columnstore.|[CREATE COLUMNSTORE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-columnstore-index-transact-sql.md)|Convert an existing heap or binary tree to a columnstore. Examples show how to handle existing indexes and also the name of the index when performing this conversion.|  
|Convert a columnstore table to a rowstore.|[CREATE COLUMNSTORE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-columnstore-index-transact-sql.md)|Usually this is not necessary, but there can be times when you need to perform this conversion. Examples show how to convert a columnstore to a heap or clustered index.|  
|Create a columnstore index on a rowstore table.|[CREATE COLUMNSTORE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-columnstore-index-transact-sql.md)|A rowstore table can have one columnstore index.  Beginning with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], the columnstore index can have a filtered condition. Examples show the basic syntax.|  
|Create performant indexes for operational analytics.|[Get started with Columnstore for real time operational analytics](../../relational-databases/indexes/get-started-with-columnstore-for-real-time-operational-analytics.md)|Describes how to create complementary columnstore and btree indexes so that OLTP queries use btree indexes and analytics queries use columnstore indexes.|  
|Create performant columnstore indexes for data warehousing.|[Columnstore Indexes for Data Warehousing](~/relational-databases/indexes/columnstore-indexes-data-warehouse.md)|Describes how to use btree indexes on columnstore tables to create performant data warehousing queries.|  
|Use a btree index to enforce a primary key constraint on a columnstore index.|[Columnstore Indexes for Data Warehousing](~/relational-databases/indexes/columnstore-indexes-data-warehouse.md)|Shows how to combine btree and columnstore indexes to enforce primary key constraints on the columnstore index.|  
|Drop a columnstore index|[DROP INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/drop-index-transact-sql.md)|Dropping a columnstore index uses the standard DROP INDEX syntax that btree indexes use. Dropping a clustered columnstore index will convert the columnstore table to a heap.|  
|Delete a row from a columnstore index|[DELETE &#40;Transact-SQL&#41;](../../t-sql/statements/delete-transact-sql.md)|Use [DELETE &#40;Transact-SQL&#41;](../../t-sql/statements/delete-transact-sql.md) to delete a row.<br /><br /> **columnstore** row: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] marks the row as logically deleted but does not reclaim the physical storage for the row until the index is rebuilt.<br /><br /> **deltastore** row: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logically and physically deletes the row.|  
|Update a row in the columnstore index|[UPDATE &#40;Transact-SQL&#41;](../../t-sql/queries/update-transact-sql.md)|Use [UPDATE &#40;Transact-SQL&#41;](../../t-sql/queries/update-transact-sql.md) to update a row.<br /><br /> **columnstore** row:  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] marks the row as logically deleted, and then inserts the updated row into the deltastore.<br /><br /> **deltastore** row: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] updates the row in the deltastore.|  
|Load data into a columnstore index|[Columnstore Indexes Data Loading](~/relational-databases/indexes/columnstore-indexes-data-loading-guidance.md)||  
|Force all rows in the deltastore to go into the columnstore.|[ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md) ... REBUILD<br /><br /> [Columnstore Indexes Defragmentation](~/relational-databases/indexes/columnstore-indexes-defragmentation.md)|ALTER INDEX with the REBUILD option forces all rows to go into the columnstore.|  
|Defragment a columnstore index|[ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md)|ALTER INDEX … REORGANIZE  defragments columnstore indexes online.|  
|Merge tables with columnstore indexes.|[MERGE &#40;Transact-SQL&#41;](../../t-sql/statements/merge-transact-sql.md)||  
  
## See Also  
 [Columnstore Indexes Data Loading](~/relational-databases/indexes/columnstore-indexes-data-loading-guidance.md)   
 [Columnstore Indexes Versioned Feature Summary](~/relational-databases/indexes/columnstore-indexes-what-s-new.md)   
 [Columnstore Indexes Query Performance](~/relational-databases/indexes/columnstore-indexes-query-performance.md)   
 [Get started with Columnstore for real time operational analytics](../../relational-databases/indexes/get-started-with-columnstore-for-real-time-operational-analytics.md)   
 [Columnstore Indexes for Data Warehousing](~/relational-databases/indexes/columnstore-indexes-data-warehouse.md)   
 [Columnstore Indexes Defragmentation](~/relational-databases/indexes/columnstore-indexes-defragmentation.md)  
  
  





