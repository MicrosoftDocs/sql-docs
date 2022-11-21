---
title: "sys.dm_db_index_operational_stats (Transact-SQL)"
description: sys.dm_db_index_operational_stats (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_db_index_operational_stats"
  - "sys.dm_db_index_operational_stats_TSQL"
  - "sys.dm_db_index_operational_stats"
  - "dm_db_index_operational_stats_TSQL"
helpviewer_keywords:
  - "sys.dm_db_index_operational_stats dynamic management function"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_db_index_operational_stats (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns current lower-level I/O, locking, latching, and access method activity for each partition of a table or index in the database.    
    
 Memory-optimized indexes do not appear in this DMV.    
    
> [!NOTE]    
>  **sys.dm_db_index_operational_stats** does not return information about memory-optimized indexes. For information about memory-optimized index use, see [sys.dm_db_xtp_index_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-xtp-index-stats-transact-sql.md).    
        
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)    
    
## Syntax    
    
```    
sys.dm_db_index_operational_stats (    
    { database_id | NULL | 0 | DEFAULT }    
  , { object_id | NULL | 0 | DEFAULT }    
  , { index_id | 0 | NULL | -1 | DEFAULT }    
  , { partition_number | NULL | 0 | DEFAULT }    
)    
```    
    
## Arguments    

*database_id* | NULL | 0 | DEFAULT

  ID of the database. *database_id* is **smallint**. Valid inputs are the ID number of a database, NULL, 0, or DEFAULT. The default is 0. NULL, 0, and DEFAULT are equivalent values in this context.    
    
 Specify NULL to return information for all databases in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If you specify NULL for *database_id*, you must also specify NULL for *object_id*, *index_id*, and *partition_number*.    
    
 The built-in function [DB_ID](../../t-sql/functions/db-id-transact-sql.md) can be specified.    

*object_id* | NULL | 0 | DEFAULT

 Object ID of the table or view the index is on. *object_id* is **int**.    
    
 Valid inputs are the ID number of a table and view, NULL, 0, or DEFAULT. The default is 0. NULL, 0, and DEFAULT are equivalent values in this context.    
    
 Specify NULL to return cached information for all tables and views in the specified database. If you specify NULL for *object_id*, you must also specify NULL for *index_id* and *partition_number*.    

*index_id* | 0 | NULL | -1 | DEFAULT

 ID of the index. *index_id* is **int**. Valid inputs are the ID number of an index, 0 if *object_id* is a heap, NULL, -1, or DEFAULT. The default is -1, NULL, -1, and DEFAULT are equivalent values in this context.    
    
 Specify NULL to return cached information for all indexes for a base table or view. If you specify NULL for *index_id*, you must also specify NULL for *partition_number*.    

*partition_number* | NULL | 0 | DEFAULT

 Partition number in the object. *partition_number* is **int**. Valid inputs are the *partion_number* of an index or heap, NULL, 0, or DEFAULT. The default is 0. NULL, 0, and DEFAULT are equivalent values in this context.    
    
 Specify NULL to return cached information for all partitions of the index or heap.    
    
 *partition_number* is 1-based. A nonpartitioned index or heap has *partition_number* set to 1.    
    
## Table Returned    
    
|Column name|Data type|Description|    
|-----------------|---------------|-----------------|    
|**database_id**|**smallint**|Database ID.|    
|**object_id**|**int**|ID of the table or view.|    
|**index_id**|**int**|ID of the index or heap.<br /><br /> 0 = Heap| 
|**partition_number**|**int**|1-based partition number within the index or heap.| 
|**hobt_id**|**bigint**|[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)], [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].<br /><br /> ID of the data heap or B-tree rowset that tracks internal data for a columnstore index.<br /><br /> NULL - this is not an internal columnstore rowset.<br /><br /> For more details, see [sys.internal_partitions &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-internal-partitions-transact-sql.md)|       
|**leaf_insert_count**|**bigint**|Cumulative count of leaf-level inserts.|    
|**leaf_delete_count**|**bigint**|Cumulative count of leaf-level deletes. leaf_delete_count is only incremented for deleted records that are not marked as ghost first. For deleted records that are ghosted first, **leaf_ghost_count** is incremented instead.|    
|**leaf_update_count**|**bigint**|Cumulative count of leaf-level updates.|    
|**leaf_ghost_count**|**bigint**|Cumulative count of leaf-level rows that are marked as deleted, but not yet removed. This count does not include records that are immediately deleted without being marked as ghost. These rows are removed by a cleanup thread at set intervals. This value does not include rows that are retained, because of an outstanding snapshot isolation transaction.|    
|**nonleaf_insert_count**|**bigint**|Cumulative count of inserts above the leaf level.<br /><br /> 0 = Heap or columnstore|    
|**nonleaf_delete_count**|**bigint**|Cumulative count of deletes above the leaf level.<br /><br /> 0 = Heap or columnstore|    
|**nonleaf_update_count**|**bigint**|Cumulative count of updates above the leaf level.<br /><br /> 0 = Heap or columnstore|    
|**leaf_allocation_count**|**bigint**|Cumulative count of leaf-level page allocations in the index or heap.<br /><br /> For an index, a page allocation corresponds to a page split.|    
|**nonleaf_allocation_count**|**bigint**|Cumulative count of page allocations caused by page splits above the leaf level.<br /><br /> 0 = Heap or columnstore|    
|**leaf_page_merge_count**|**bigint**|Cumulative count of page merges at the leaf level. Always 0 for  columnstore index.|    
|**nonleaf_page_merge_count**|**bigint**|Cumulative count of page merges above the leaf level.<br /><br /> 0 = Heap or columnstore|    
|**range_scan_count**|**bigint**|Cumulative count of range and table scans started on the index or heap.|    
|**singleton_lookup_count**|**bigint**|Cumulative count of single row retrievals from the index or heap.|    
|**forwarded_fetch_count**|**bigint**|Count of rows that were fetched through a forwarding record.<br /><br /> 0 = Indexes|    
|**lob_fetch_in_pages**|**bigint**|Cumulative count of large object (LOB) pages retrieved from the LOB_DATA allocation unit. These pages contain data that is stored in columns of type **text**, **ntext**, **image**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, and **xml**. For more information, see [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md).|    
|**lob_fetch_in_bytes**|**bigint**|Cumulative count of LOB data bytes retrieved.|    
|**lob_orphan_create_count**|**bigint**|Cumulative count of orphan LOB values created for bulk operations.<br /><br /> 0 = Nonclustered index|    
|**lob_orphan_insert_count**|**bigint**|Cumulative count of orphan LOB values inserted during bulk operations.<br /><br /> 0 = Nonclustered index|    
|**row_overflow_fetch_in_pages**|**bigint**|Cumulative count of row-overflow data pages retrieved from the ROW_OVERFLOW_DATA allocation unit.<br /><br /> These pages contain data stored in columns of type **varchar(n)**, **nvarchar(n)**, **varbinary(n)**, and **sql_variant** that has been pushed off-row.|    
|**row_overflow_fetch_in_bytes**|**bigint**|Cumulative count of row-overflow data bytes retrieved.|    
|**column_value_push_off_row_count**|**bigint**|Cumulative count of column values for LOB data and row-overflow data that is pushed off-row to make an inserted or updated row fit within a page.|    
|**column_value_pull_in_row_count**|**bigint**|Cumulative count of column values for LOB data and row-overflow data that is pulled in-row. This occurs when an update operation frees up space in a record and provides an opportunity to pull in one or more off-row values from the LOB_DATA or ROW_OVERFLOW_DATA allocation units to the IN_ROW_DATA allocation unit.|    
|**row_lock_count**|**bigint**|Cumulative number of row locks requested.|    
|**row_lock_wait_count**|**bigint**|Cumulative number of times the [!INCLUDE[ssDE](../../includes/ssde-md.md)] waited on a row lock.|    
|**row_lock_wait_in_ms**|**bigint**|Total number of milliseconds the [!INCLUDE[ssDE](../../includes/ssde-md.md)] waited on a row lock.|    
|**page_lock_count**|**bigint**|Cumulative number of page locks requested.|    
|**page_lock_wait_count**|**bigint**|Cumulative number of times the [!INCLUDE[ssDE](../../includes/ssde-md.md)] waited on a page lock.|    
|**page_lock_wait_in_ms**|**bigint**|Total number of milliseconds the [!INCLUDE[ssDE](../../includes/ssde-md.md)] waited on a page lock.|    
|**index_lock_promotion_attempt_count**|**bigint**|Cumulative number of times the [!INCLUDE[ssDE](../../includes/ssde-md.md)] tried to escalate locks.|    
|**index_lock_promotion_count**|**bigint**|Cumulative number of times the [!INCLUDE[ssDE](../../includes/ssde-md.md)] escalated locks.|    
|**page_latch_wait_count**|**bigint**|Cumulative number of times the [!INCLUDE[ssDE](../../includes/ssde-md.md)] waited, because of latch contention.|    
|**page_latch_wait_in_ms**|**bigint**|Cumulative number of milliseconds the [!INCLUDE[ssDE](../../includes/ssde-md.md)] waited, because of latch contention.|    
|**page_io_latch_wait_count**|**bigint**|Cumulative number of times the [!INCLUDE[ssDE](../../includes/ssde-md.md)] waited on an I/O page latch.|    
|**page_io_latch_wait_in_ms**|**bigint**|Cumulative number of milliseconds the [!INCLUDE[ssDE](../../includes/ssde-md.md)] waited on a page I/O latch.|    
|**tree_page_latch_wait_count**|**bigint**|Subset of **page_latch_wait_count** that includes only the upper-level B-tree pages. Always 0 for a heap or columnstore index.|    
|**tree_page_latch_wait_in_ms**|**bigint**|Subset of **page_latch_wait_in_ms** that includes only the upper-level B-tree pages. Always 0 for a heap or columnstore index.|    
|**tree_page_io_latch_wait_count**|**bigint**|Subset of **page_io_latch_wait_count** that includes only the upper-level B-tree pages. Always 0 for a heap or columnstore index.|    
|**tree_page_io_latch_wait_in_ms**|**bigint**|Subset of **page_io_latch_wait_in_ms** that includes only the upper-level B-tree pages. Always 0 for a heap or columnstore index.|    
|**page_compression_attempt_count**|**bigint**|Number of pages that were evaluated for PAGE level compression for specific partitions of a table, index, or indexed view. Includes pages that were not compressed because significant savings could not be achieved. Always 0 for  columnstore index.|    
|**page_compression_success_count**|**bigint**|Number of data pages that were compressed by using PAGE compression for specific partitions of a table, index, or indexed view. Always 0 for  columnstore index.|    

[!INCLUDE [sql-b-tree](../../includes/sql-b-tree.md)]
  
## Remarks    
 This dynamic management object does not accept correlated parameters from `CROSS APPLY` and `OUTER APPLY`.    
    
 You can use **sys.dm_db_index_operational_stats** to track the length of time that users must wait to read or write to a table, index, or partition, and identify the tables or indexes that are encountering significant I/O activity or hot spots.    
    
 Use the following columns to identify areas of contention.    
    
 **To analyze a common access pattern to the table or index partition**, use these columns:    
    
-   **leaf_insert_count**    
    
-   **leaf_delete_count**    
    
-   **leaf_update_count**    
    
-   **leaf_ghost_count**    
    
-   **range_scan_count**    
    
-   **singleton_lookup_count**    
    
 To identify latching and locking contention, use these columns:    
    
-   **page_latch_wait_count** and **page_latch_wait_in_ms**    
    
     These columns indicate whether there is latch contention on the index or heap, and the significance of the contention.    
    
-   **row_lock_count** and **page_lock_count**    
    
     These columns indicate how many times the [!INCLUDE[ssDE](../../includes/ssde-md.md)] tried to acquire row and page locks.    
    
-   **row_lock_wait_in_ms** and **page_lock_wait_in_ms**    
    
     These columns indicate whether there is lock contention on the index or heap, and the significance of the contention.    
    
 **To analyze statistics of physical I/Os on an index or heap partition**    
    
-   **page_io_latch_wait_count** and **page_io_latch_wait_in_ms**    
    
     These columns indicate whether physical I/Os were issued to bring the index or heap pages into memory and how many I/Os were issued.    
    
## Column Remarks    
 The values in **lob_orphan_create_count** and **lob_orphan_insert_count** should always be equal.    
    
 The value in the columns **lob_fetch_in_pages** and **lob_fetch_in_bytes** can be greater than zero for nonclustered indexes that contain one or more LOB columns as included columns. For more information, see [Create Indexes with Included Columns](../../relational-databases/indexes/create-indexes-with-included-columns.md). Similarly, the value in the columns **row_overflow_fetch_in_pages** and **row_overflow_fetch_in_bytes** can be greater than 0 for nonclustered indexes if the index contains columns that can be pushed off-row.    
    
## How the counters in the Metadata Cache are reset    
 The data returned by **sys.dm_db_index_operational_stats** exists only as long as the metadata cache object that represents the heap or index is available. This data is neither persistent nor transactionally consistent. This means you cannot use these counters to determine whether an index has been used or not, or when the index was last used. For information about this, see [sys.dm_db_index_usage_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-usage-stats-transact-sql.md).    
    
 The values for each column are set to zero whenever the metadata for the heap or index is brought into the metadata cache and statistics are accumulated until the cache object is removed from the metadata cache. Therefore, an active heap or index will likely always have its metadata in the cache, and the cumulative counts may reflect activity since the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] was last started. The metadata for a less active heap or index will move in and out of the cache as it is used. As a result, it may or may not have values available. Dropping an index will cause the corresponding statistics to be removed from memory and no longer be reported by the function. Other DDL operations against the index may cause the value of the statistics to be reset to zero.    
    
## Using system functions to specify parameter values    
 You can use the [!INCLUDE[tsql](../../includes/tsql-md.md)] functions [DB_ID](../../t-sql/functions/db-id-transact-sql.md) and [OBJECT_ID](../../t-sql/functions/object-id-transact-sql.md) to specify a value for the *database_id* and *object_id* parameters. However, passing values that are not valid to these functions may cause unintended results. Always make sure that a valid ID is returned when you use DB_ID or OBJECT_ID. For more information, see the Remarks section in [sys.dm_db_index_physical_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md).    
    
## Permissions    
 Requires the following permissions:    
    
-   `CONTROL` permission on the specified object within the database    
    
-   `VIEW DATABASE STATE` permission to return information about all objects within the specified database, by using the object wildcard @*object_id* = NULL    
    
-   `VIEW SERVER STATE` permission to return information about all databases, by using the database wildcard @*database_id* = NULL    
    
 Granting `VIEW DATABASE STATE` allows all objects in the database to be returned, regardless of any CONTROL permissions denied on specific objects.    
    
 Denying `VIEW DATABASE STATE` disallows all objects in the database to be returned, regardless of any CONTROL permissions granted on specific objects. Also, when the database wildcard `@database_id=NULL` is specified, the database is omitted.    
    
 For more information, see [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md).    
    
## Examples    
    
### A. Returning information for a specified table    
 The following example returns information for all indexes and partitions of the `Person.Address` table in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database. Executing this query requires, at a minimum, CONTROL permission on `Person.Address` table.    
    
> [!IMPORTANT]    
> When you are using the [!INCLUDE[tsql](../../includes/tsql-md.md)] functions DB_ID and OBJECT_ID to return a parameter value, always ensure that a valid ID is returned. If the database or object name cannot be found, such as when they do not exist or are spelled incorrectly, both functions will return NULL. The **sys.dm_db_index_operational_stats** function interprets NULL as a wildcard value that specifies all databases or all objects. Because this can be an unintentional operation, the examples in this section demonstrate the safe way to determine database and object IDs.    
    
```sql    
DECLARE @db_id int;    
DECLARE @object_id int;    
SET @db_id = DB_ID(N'AdventureWorks2012');    
SET @object_id = OBJECT_ID(N'AdventureWorks2012.Person.Address');    
IF @db_id IS NULL     
  BEGIN;    
    PRINT N'Invalid database';    
  END;    
ELSE IF @object_id IS NULL    
  BEGIN;    
    PRINT N'Invalid object';    
  END;    
ELSE    
  BEGIN;    
    SELECT * FROM sys.dm_db_index_operational_stats(@db_id, @object_id, NULL, NULL);    
  END;    
GO    
```    
    
### B. Returning information for all tables and indexes    
 The following example returns information for all tables and indexes within the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Executing this query requires VIEW SERVER STATE permission.    
    
```sql    
SELECT * FROM sys.dm_db_index_operational_stats( NULL, NULL, NULL, NULL);    
GO        
```    
    
## See Also    
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)     
 [Index Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/index-related-dynamic-management-views-and-functions-transact-sql.md)     
 [Monitor and Tune for Performance](../../relational-databases/performance/monitor-and-tune-for-performance.md)     
 [sys.dm_db_index_physical_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md)     
 [sys.dm_db_index_usage_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-usage-stats-transact-sql.md)     
 [sys.dm_os_latch_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-latch-stats-transact-sql.md)     
 [sys.dm_db_partition_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-partition-stats-transact-sql.md)     
 [sys.allocation_units &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-allocation-units-transact-sql.md)     
 [sys.indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)    
    
