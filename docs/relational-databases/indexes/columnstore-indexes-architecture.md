---
title: "Columnstore indexes - architecture | Microsoft Docs"
ms.custom: ""
ms.date: "01/17/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 96b8e884-8244-425f-b856-72a8ff6895a6
caps.latest.revision: 8
author: "barbkess"
ms.author: "barbkess"
manager: "jhubbard"
---
# Columnstore indexes - architecture
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

Learn how a columnstore index is architected. Knowing these basics will make it easier to understand other columnstore articles that explain how to use them effectively.

## Data storage uses columnstore and rowstore compression
In discussions about columnstore indexes, we use the terms *rowstore* and *columnstore* to emphasize the format for the data storage.  Columnstore indexes use both types of storage.

 ![Clustered Columnstore Index](../../relational-databases/indexes/media/sql-server-pdw-columnstore-physicalstorage.gif "Clustered Columnstore Index")

- A **columnstore** is data that is logically organized as a table with rows and columns, and physically stored in a column-wise data format.
  
A columnstore index physically stores most of the data in columnstore format. In columnstore format, the data is compressed and uncompressed as columns. There is no need to uncompress other values in each row that are not requested by the query. This makes it fast to scan an entire column of a large table. 

- A **rowstore** is data that is logically organized as a table with rows and columns, and then physically stored in a row-wise data format. This has been the traditional way to store relational table data such as a heap or clustered "btree" index.

A columnstore index also physically stores some rows in a rowstore format called a deltastore. The deltastore,also called delta rowgroups, is a holding place for rows that are too few in number to qualify for compression into the columnstore. Each delta rowgroup is implemented as a clustered btree index. 

- A **deltastore** is a a holding place for rows that are too few in number to be compressed into the columnstore. The deltastore is a rowstore. 
  
## Operations are performed on rowgroups and column segments

The columnstore index groups rows into manageable units. Each of these units is called a rowgroup. For best performance, the number of rows in a rowgroup is large enough to improve compression rates and small enough to benefit from in-memory operations.

* A **rowgroup** is a group of rows on which the columnstore index performs management and compression operations. 

For example, the columnstore index performs these operations on rowgroups:

* Compresses rowgroups into the columnstore. Compression is performed on each column segment within a rowgroup.
* Merges rowgroups during an ALTER INDEX REORGANIZE operation.
* Creates new rowgroups during an ALTER INDEX REBUILD operation.
* Reports on rowgroup health and fragmentation in the dynamic management views (DMVs).

The deltastore is comprised of one or more rowgroups called delta rowgroups. Each delta rowgroup is a clustered btree index that stores rows when they are too few in number for compression into the columnstore.  

* A **delta rowgroup** is a clustered btree index that stores small bulk loads and inserts until the rowgroup contains 1,048,576 rows or until the index is rebuilt.  When a delta rowgroup contains 1,048,576 rows it is marked as closed and waits for a process called the tuple-mover to compress it into the columnstore. 


Each column has some of its values in each rowgroup. These values are called column segments. When the columnstore index compresses a rowgroup, it compresses each column segment separately. To uncompress an entire column, the columnstore index only needs to uncompress one column segment from each rowgroup.

* A **column segment** is the portion of column values in a rowgroup. Each rowgroup contains one column segment for every column in the table. Each column has one column segment in each rowgroup.| 
  
 ![Column segment](../../relational-databases/indexes/media/sql-server-pdw-columnstore-columnsegment.gif "Column segment")  
 
## Small loads and inserts go to the deltastore
A columnstore index improves columnstore compression and performance by compressing at least 102,400 rows at a time into the columnstore index. To compress rows in bulk, the columnstore index accumulates small loads and inserts in the deltastore. The deltastore operations are handled behind the scenes. To return the correct query results, the clustered columnstore index combines query results from both the columnstore and the deltastore. 

Rows go to the deltastore when they are:
* Inserted with the INSERT INTO VALUES statement.
* At the end of a bulk load and they number less than 102,400.
* Updated. Each update is implemented as a delete and an insert.

The deltastore also stores a list of IDs for deleted rows that have been marked as deleted but not yet physically deleted from the columnstore. 

## When delta rowgroups are full they get compressed into the columnstore

Clustered columnstore indexes collect up to 1,048,576 rows in each delta rowgroup before compressing the rowgroup into the columnstore. This improves the compression of the columnstore index. When a delta rowgroup  contains 1,048,576 rows, the columnstore index marks the rowgroup as closed. A background process, called the *tuple-mover*, finds each closed rowgroup and compresses it into the columnstore. 

You can force delta rowgroups into the columnstore by using [ALTER INDEX](../../t-sql/statements/alter-index-transact-sql.md) to rebuild or reorganize the index.  Note that if there is memory pressure during compression, the columnstore index might reduce the number of rows in the compressed rowgroup.

## Each table partition has its own rowgroups and delta rowgroups

The concept of partitioning is the same in both a clustered index, a heap, and a columnstore index. Partitioning a table divides the table into smaller groups of rows according to a range of column values. It is often used for managing the data. For example, you could create a partition for each year of data, and then use partition switching to archive data to less expensive storage. Partition switching works on columnstore indexes and makes it easy to move a partition of data to another location.

Rowgroups are always defined within a table partition. When a columnstore index is partitioned, each partition has its own compressed rowgroups and delta rowgroups.

### Each partition can have multiple delta rowgroups
Each partition can have more than one delta rowgroups. When the columnstore index needs to add data to a delta rowgroup and the delta rowgroup is locked, the columnstore index will try to obtain a lock on a different delta rowgroup. If there are no delta rowgroups available, the columnstore index will create a new delta rowgroup.  For example, a table with 10 partitions could easily have 20 or more delta rowgroups. 

  
## You can combine columnstore and rowstore indexes on the same table
A nonclustered index contains a copy of part or all of the rows and columns in the underlying table. The index is defined as one or more columns of the table, and has an optional condition that filters the rows. 

Beginning with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], you can  create an updatable  nonclustered columnstore index on a rowstore table. The columnstore index stores a copy of the data so you do need extra storage. However, the data in the columnstore index will compress to a smaller size than the rowstore table requires.  By doing this, you can run analytics on the columnstore index and transactions on the rowstore index at the same time. The column store is updated when data changes in the rowstore table, so both indexes are working against the same data.  
  
 Beginning with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], you can have one or more nonclustered rowstore indexes on a columnstore index. By doing this, you can perform efficient table seeks on the underlying columnstore. Other options become available too. For example, you can enforce a primary key constraint by using a UNIQUE constraint on the rowstore table. Since an non-unique value will fail to insert into the rowstore table, SQL Server cannot insert the value into the columnstore.  
 

## Metadata  
Use these metadata views to see attributes of columnstore indexes. More architectural information is embedded in some of these views.

Note, all of the columns in a columnstore index are stored in the metadata as included columns. The columnstore index does not have key columns.  
  
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
  
|  


## Next steps
 For guidance on designing your columnstore indexes, see [Columnstore indexes - design guidance](../../relational-databases/indexes/columnstore-indexes-design-guidance.md)
