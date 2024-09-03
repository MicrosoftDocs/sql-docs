---
title: "sys.dm_db_index_physical_stats (Transact-SQL)"
description: Returns size and fragmentation information for the data and indexes of the specified table or view in the SQL Server Database Engine.
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/03/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_db_index_physical_stats"
  - "sys.dm_db_index_physical_stats_TSQL"
  - "sys.dm_db_index_physical_stats"
  - "dm_db_index_physical_stats_TSQL"
helpviewer_keywords:
  - "sys.dm_db_index_physical_stats dynamic management function"
  - "fragmentation [SQL Server]"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sys.dm_db_index_physical_stats (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns size and fragmentation information for the data and indexes of the specified table or view in the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)]. For an index, one row is returned for each level of the B-tree in each partition. For a heap, one row is returned for the `IN_ROW_DATA` allocation unit of each partition. For large object (LOB) data, one row is returned for the `LOB_DATA` allocation unit of each partition. If row-overflow data exists in the table, one row is returned for the `ROW_OVERFLOW_DATA` allocation unit in each partition.

[!INCLUDE [sql-b-tree](../../includes/sql-b-tree.md)]

`sys.dm_db_index_physical_stats` doesn't return information about memory-optimized indexes. For information about memory-optimized index use, see [sys.dm_db_xtp_index_stats](sys-dm-db-xtp-index-stats-transact-sql.md).

If you query `sys.dm_db_index_physical_stats` on a server instance that is hosting an availability group [readable secondary replica](../../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md), you might encounter a `REDO` blocking issue. This is because this dynamic management view acquires an Intent-Shared (IS) lock on the specified user table or view that can block requests by a `REDO` thread for an Exclusive (X) lock on that user table or view.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.dm_db_index_physical_stats (
    { database_id | NULL | 0 | DEFAULT }
  , { object_id | NULL | 0 | DEFAULT }
  , { index_id | NULL | 0 | -1 | DEFAULT }
  , { partition_number | NULL | 0 | DEFAULT }
  , { mode | NULL | DEFAULT }
)
```

## Arguments

#### *database_id* \| NULL \| 0 \| DEFAULT

The ID of the database. *database_id* is **smallint**. Valid inputs are the ID of a database, `NULL`, `0`, or `DEFAULT`. The default is `0`. `NULL`, `0`, and `DEFAULT` are equivalent values in this context.

Specify `NULL` to return information for all databases in the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. If you specify `NULL` for *database_id*, you must also specify `NULL` for *object_id*, *index_id*, and *partition_number*.

The built-in function [DB_ID](../../t-sql/functions/db-id-transact-sql.md) can be specified. When you use `DB_ID` without specifying a database name, the compatibility level of the current database must be `90` or greater.

#### *object_id* \| NULL \| 0 \| DEFAULT

The object ID of the table or view the index is on. *object_id* is **int**. Valid inputs are the ID of a table and view, `NULL`, `0`, or `DEFAULT`. The default is `0`. `NULL`, `0`, and `DEFAULT` are equivalent values in this context.

In [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, valid inputs also include the service broker queue name or the queue internal table name. When default parameters are applied (that is, all objects, all indexes, etc.), fragmentation information for all queues is included in the result set.

Specify `NULL` to return information for all tables and views in the specified database. If you specify `NULL` for *object_id*, you must also specify `NULL` for *index_id* and *partition_number*.

#### *index_id* \| 0 \| NULL \| -1 \| DEFAULT

The ID of the index. *index_id* is **int**. Valid inputs are the ID of an index, `0` if *object_id* is a heap, `NULL`, `-1`, or `DEFAULT`. The default is `-1`. `NULL`, `-1`, and `DEFAULT` are equivalent values in this context.

Specify `NULL` to return information for all indexes for a base table or view. If you specify `NULL` for *index_id*, you must also specify `NULL` for *partition_number*.

#### *partition_number* \| NULL \| 0 \| DEFAULT

The partition number in the object. *partition_number* is **int**. Valid inputs are the *partion_number* of an index or heap, `NULL`, `0`, or `DEFAULT`. The default is `0`. `NULL`, `0`, and `DEFAULT` are equivalent values in this context.

Specify `NULL` to return information for all partitions of the owning object.

*partition_number* is 1-based. A nonpartitioned index or heap has *partition_number* set to `1`.

#### *mode* | NULL | DEFAULT

The name of the mode. *mode* specifies the scan level that is used to obtain statistics. *mode* is **sysname**. Valid inputs are `DEFAULT`, `NULL`, `LIMITED`, `SAMPLED`, or `DETAILED`. The default (`NULL`) is `LIMITED`.

## Table returned

| Column name | Data type | Description |
| --- | --- | --- |
| `database_id` | **smallint** | Database ID of the table or view.<br /><br />In [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], the values are unique within a single database or an elastic pool, but not within a logical server. |
| `object_id` | **int** | Object ID of the table or view that the index is on. |
| `index_id` | **int** | Index ID of an index.<br /><br />`0` = Heap. |
| `partition_number` | **int** | 1-based partition number within the owning object; a table, view, or index.<br /><br />`1` = Nonpartitioned index or heap. |
| `index_type_desc` | **nvarchar(60)** | Description of the index type:<br /><br />- `HEAP`<br />- `CLUSTERED INDEX`<br />- `NONCLUSTERED INDEX`<br />- `PRIMARY XML INDEX`<br />- `EXTENDED INDEX`<br />- `XML INDEX`<br />- `COLUMNSTORE MAPPING INDEX` (internal)<br />- `COLUMNSTORE DELETEBUFFER INDEX` (internal)<br />- `COLUMNSTORE DELETEBITMAP INDEX` (internal) |
| `alloc_unit_type_desc` | **nvarchar(60)** | Description of the allocation unit type:<br /><br />- `IN_ROW_DATA`<br />- `LOB_DATA`<br />- `ROW_OVERFLOW_DATA`<br /><br />The `LOB_DATA` allocation unit contains the data that is stored in columns of type **text**, **ntext**, **image**, **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, and **xml**. For more information, see [Data types](../../t-sql/data-types/data-types-transact-sql.md).<br /><br />The `ROW_OVERFLOW_DATA` allocation unit contains the data that is stored in columns of type **varchar(*n*)**, **nvarchar(*n*)**, **varbinary(*n*)**, and **sql_variant** that are pushed off-row. |
| `index_depth` | **tinyint** | Number of index levels.<br /><br />`1` = Heap, or `LOB_DATA` or `ROW_OVERFLOW_DATA` allocation unit. |
| `index_level` | **tinyint** | Current level of the index.<br /><br />`0` for index leaf levels, heaps, and `LOB_DATA` or `ROW_OVERFLOW_DATA` allocation units.<br /><br />Greater than `0` for nonleaf index levels. *index_level* is the highest at the root level of an index.<br /><br />The nonleaf levels of indexes are only processed when *mode* is `DETAILED`. |
| `avg_fragmentation_in_percent` | **float** | Logical fragmentation for indexes, or extent fragmentation for heaps in the `IN_ROW_DATA` allocation unit.<br /><br />The value is measured as a percentage and takes into account multiple files. For definitions of logical and extent fragmentation, see [Remarks](#remarks).<br /><br />`0` for `LOB_DATA` and `ROW_OVERFLOW_DATA` allocation units. `NULL` for heaps when *mode* is `SAMPLED`. |
| `fragment_count` | **bigint** | Number of fragments in the leaf level of an `IN_ROW_DATA` allocation unit. For more information about fragments, see [Remarks](#remarks).<br /><br />`NULL` for nonleaf levels of an index, and `LOB_DATA` or `ROW_OVERFLOW_DATA` allocation units. `NULL` for heaps when *mode* is `SAMPLED`. |
| `avg_fragment_size_in_pages` | **float** | Average number of pages in one fragment in the leaf level of an `IN_ROW_DATA` allocation unit.<br /><br />`NULL` for nonleaf levels of an index, and `LOB_DATA` or `ROW_OVERFLOW_DATA` allocation units. `NULL` for heaps when *mode* is `SAMPLED`. |
| `page_count` | **bigint** | Total number of index or data pages.<br /><br />For an index, the total number of index pages in the current level of the B-tree in the `IN_ROW_DATA` allocation unit.<br /><br />For a heap, the total number of data pages in the `IN_ROW_DATA` allocation unit.<br /><br />For `LOB_DATA` or `ROW_OVERFLOW_DATA` allocation units, total number of pages in the allocation unit. |
| `avg_page_space_used_in_percent` | **float** | Average percentage of available data storage space used in all pages.<br /><br />For an index, average applies to the current level of the B-tree in the `IN_ROW_DATA` allocation unit.<br /><br />For a heap, the average of all data pages in the `IN_ROW_DATA` allocation unit.<br /><br />For `LOB_DATA` or `ROW_OVERFLOW_DATA` allocation units, the average of all pages in the allocation unit. `NULL` when *mode* is `LIMITED`. |
| `record_count` | **bigint** | Total number of records.<br /><br />For an index, total number of records applies to the current level of the B-tree in the `IN_ROW_DATA` allocation unit.<br /><br />For a heap, the total number of records in the `IN_ROW_DATA` allocation unit.<br /><br />**Note:** For a heap, the number of records returned from this function might not match the number of rows that are returned by running a `SELECT COUNT(*)` against the heap. This is because a row can contain multiple records. For example, under some update situations, a single heap row might have a forwarding record and a forwarded record as a result of the update operation. Also, most large LOB rows are split into multiple records in `LOB_DATA` storage.<br /><br />For `LOB_DATA` or `ROW_OVERFLOW_DATA` allocation units, the total number of records in the complete allocation unit. `NULL` when *mode* is `LIMITED`. |
| `ghost_record_count` | **bigint** | Number of ghost records ready for removal by the ghost cleanup task in the allocation unit.<br /><br />`0` for nonleaf levels of an index in the `IN_ROW_DATA` allocation unit. `NULL` when *mode* is `LIMITED`. |
| `version_ghost_record_count` | **bigint** | Number of ghost records retained by an outstanding snapshot isolation transaction in an allocation unit.<br /><br />`0` for nonleaf levels of an index in the `IN_ROW_DATA` allocation unit. `NULL` when *mode* is `LIMITED`. |
| `min_record_size_in_bytes` | **int** | Minimum record size in bytes.<br /><br />For an index, minimum record size applies to the current level of the B-tree in the `IN_ROW_DATA` allocation unit.<br /><br />For a heap, the minimum record size in the `IN_ROW_DATA` allocation unit.<br /><br />For `LOB_DATA` or `ROW_OVERFLOW_DATA` allocation units, the minimum record size in the complete allocation unit. `NULL` when *mode* is `LIMITED`. |
| `max_record_size_in_bytes` | **int** | Maximum record size in bytes.<br /><br />For an index, the maximum record size applies to the current level of the B-tree in the `IN_ROW_DATA` allocation unit.<br /><br />For a heap, the maximum record size in the `IN_ROW_DATA` allocation unit.<br /><br />For `LOB_DATA` or `ROW_OVERFLOW_DATA` allocation units, the maximum record size in the complete allocation unit. `NULL` when *mode* is `LIMITED`. |
| `avg_record_size_in_bytes` | **float** | Average record size in bytes.<br /><br />For an index, the average record size applies to the current level of the B-tree in the `IN_ROW_DATA` allocation unit.<br /><br />For a heap, the average record size in the `IN_ROW_DATA` allocation unit.<br /><br />For `LOB_DATA` or `ROW_OVERFLOW_DATA` allocation units, the average record size in the complete allocation unit. `NULL` when *mode* is `LIMITED`. |
| `forwarded_record_count` | **bigint** | Number of records in a heap that have forward pointers to another data location. (This state occurs during an update, when there isn't enough room to store the new row in the original location.)<br /><br />`NULL` for any allocation unit other than the `IN_ROW_DATA` allocation units for a heap. `NULL` for heaps when *mode* is `LIMITED`. |
| `compressed_page_count` | **bigint** | The number of compressed pages.<br /><br />For heaps, newly allocated pages aren't `PAGE` compressed. A heap is `PAGE` compressed under two special conditions: when data is bulk imported or when a heap is rebuilt. Typical DML operations that cause page allocations aren't `PAGE` compressed. Rebuild a heap when the `compressed_page_count` value grows larger than the threshold you want.<br /><br />For tables that have a clustered index, the `compressed_page_count` value indicates the effectiveness of `PAGE` compression. |
| `hobt_id` | **bigint** | Heap or B-tree ID of the index or partition.<br /><br />For columnstore indexes, this is the ID for a rowset that tracks internal columnstore data for a partition. The rowsets are stored as data heaps or B-trees. They have the same index ID as the parent columnstore index. For more information, see [sys.internal_partitions](../system-catalog-views/sys-internal-partitions-transact-sql.md). |
| `columnstore_delete_buffer_state` | **tinyint** | `0` = `NOT_APPLICABLE`<br />`1` = `OPEN`<br />`2` = `DRAINING`<br />`3` = `FLUSHING`<br />`4` = `RETIRING`<br />`5` = `READY`<br /><br />**Applies to:** [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, Azure SQL Database, and Azure SQL Managed Instance |
| `columnstore_delete_buffer_state_desc` | **nvarchar(60)** | `NOT VALID` - the parent index isn't a columnstore index.<br /><br />`OPEN` - deleters and scanners use this.<br /><br />`DRAINING` - deleters are draining out but scanners still use it.<br /><br />`FLUSHING` - buffer is closed and rows in the buffer are being written to the delete bitmap.<br /><br />`RETIRING` - rows in the closed delete buffer were written to the delete bitmap, but the buffer hasn't been truncated because scanners are still using it. New scanners don't need to use the retiring buffer because the open buffer is enough.<br /><br />`READY` - This delete buffer is ready for use.<br /><br />**Applies to:** [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions, Azure SQL Database, and Azure SQL Managed Instance |
| `version_record_count` | **bigint** | This is the count of the row version records being maintained in this index. These row versions are maintained by the [Accelerated database recovery](../accelerated-database-recovery-concepts.md) feature.<br /><br />**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] |
| `inrow_version_record_count` | **bigint** | Count of ADR version records kept in the data row for fast retrieval.<br /><br />**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] |
| `inrow_diff_version_record_count` | **bigint** | Count of ADR version records kept in the form of differences from the base version.<br /><br />**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] |
| `total_inrow_version_payload_size_in_bytes` | **bigint** | Total size in bytes of the in-row version records for this index.<br /><br />**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] |
| `offrow_regular_version_record_count` | **bigint** | Count of version records being kept outside the original data row.<br /><br />**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] |
| `offrow_long_term_version_record_count` | **bigint** | Count of version records considered long term.<br /><br />**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] |

[!INCLUDE [sql-b-tree](../../includes/sql-b-tree.md)]

## Remarks

The `sys.dm_db_index_physical_stats` dynamic management function replaces the `DBCC SHOWCONTIG` statement.

## Scanning modes

The mode in which the function is executed determines the level of scanning performed to obtain the statistical data that is used by the function. *mode* is specified as `LIMITED`, `SAMPLED`, or `DETAILED`. The function traverses the page chains for the allocation units that make up the specified partitions of the table or index. `sys.dm_db_index_physical_stats` requires only an Intent-Shared (IS) table lock, regardless of the mode that it runs in.

The `LIMITED` mode is the fastest mode and scans the smallest number of pages. For an index, only the parent-level pages of the B-tree (that is, the pages above the leaf level) are scanned. For a heap, the associated PFS and IAM pages are examined and the data pages of a heap are scanned in `LIMITED` mode.

With `LIMITED` mode, `compressed_page_count` is `NULL` because the [!INCLUDE [ssDE](../../includes/ssde-md.md)] only scans nonleaf pages of the B-tree and the IAM and PFS pages of the heap. Use `SAMPLED` mode to get an estimated value for `compressed_page_count`, and use `DETAILED` mode to get the actual value for `compressed_page_count`. The `SAMPLED` mode returns statistics based on a 1 percent sample of all the pages in the index or heap. Results in `SAMPLED` mode should be regarded as approximate. If the index or heap has fewer than 10,000 pages, `DETAILED` mode is used instead of `SAMPLED`.

The `DETAILED` mode scans all pages and returns all statistics.

The modes are progressively slower from `LIMITED` to `DETAILED`, because more work is performed in each mode. To quickly gauge the size or fragmentation level of a table or index, use the `LIMITED` mode. It's the fastest and doesn't return a row for each nonleaf level in the `IN_ROW_DATA` allocation unit of the index.

## Use system functions to specify parameter values

You can use the [!INCLUDE [tsql](../../includes/tsql-md.md)] functions [DB_ID](../../t-sql/functions/db-id-transact-sql.md) and [OBJECT_ID](../../t-sql/functions/object-id-transact-sql.md) to specify a value for the *database_id* and *object_id* parameters. However, passing values that aren't valid to these functions can cause unintended results. For example, if the database or object name can't be found because they don't exist or are spelled incorrectly, both functions return `NULL`. The `sys.dm_db_index_physical_stats` function interprets `NULL` as a wildcard value specifying all databases or all objects.

Additionally, the `OBJECT_ID` function is processed before the `sys.dm_db_index_physical_stats` function is called and is therefore evaluated in the context of the current database, not the database specified in *database_id*. This behavior can cause the `OBJECT_ID` function to return a `NULL` value; or, if the object name exists in both the current database context and the specified database, an error message is returned. The following examples demonstrate these unintended results.

```sql
USE master;
GO
-- In this example, OBJECT_ID is evaluated in the context of the master database.
-- Because Person.Address does not exist in master, the function returns NULL.
-- When NULL is specified as an object_id, all objects in the database are returned.
-- The same results are returned when an object that is not valid is specified.
SELECT * FROM sys.dm_db_index_physical_stats
    (DB_ID(N'AdventureWorks2022'), OBJECT_ID(N'Person.Address'), NULL, NULL , 'DETAILED');
GO
-- This example demonstrates the results of specifying a valid object name
-- that exists in both the current database context and
-- in the database specified in the database_id parameter of the
-- sys.dm_db_index_physical_stats function.
-- An error is returned because the ID value returned by OBJECT_ID does not
-- match the ID value of the object in the specified database.
CREATE DATABASE Test;
GO
USE Test;
GO
CREATE SCHEMA Person;
GO
CREATE Table Person.Address(c1 int);
GO
USE AdventureWorks2022;
GO
SELECT * FROM sys.dm_db_index_physical_stats
    (DB_ID(N'Test'), OBJECT_ID(N'Person.Address'), NULL, NULL , 'DETAILED');
GO
-- Clean up temporary database.
DROP DATABASE Test;
GO
```

### Best practice

Always make sure that a valid ID is returned when you use `DB_ID` or `OBJECT_ID`. For example, when you use `OBJECT_ID`, specify a three-part name such as `OBJECT_ID(N'AdventureWorks2022.Person.Address')`, or test the value returned by the functions before you use them in the `sys.dm_db_index_physical_stats` function. Examples A and B that follow demonstrate a safe way to specify database and object IDs.

## Detect fragmentation

Fragmentation occurs through the process of data modifications (`INSERT`, `UPDATE`, and `DELETE` statements) that are made against the table and, therefore, to the indexes defined on the table. Because these modifications aren't ordinarily distributed equally among the rows of the table and indexes, the fullness of each page can vary over time. For queries that scan part or all of the indexes of a table, this kind of fragmentation can cause more page reads, which hinders parallel scanning of data.

The fragmentation level of an index or heap is shown in the `avg_fragmentation_in_percent` column. For heaps, the value represents the extent fragmentation of the heap. For indexes, the value represents the logical fragmentation of the index. Unlike `DBCC SHOWCONTIG`, the fragmentation calculation algorithms in both cases consider storage that spans multiple files and, therefore, are accurate.

### Logical fragmentation

This is the percentage of out-of-order pages in the leaf pages of an index. An out-of-order page is a page for which the next physical page allocated to the index isn't the page pointed to by the *next-page* pointer in the current leaf page.

### Extent fragmentation

This is the percentage of out-of-order extents in the leaf pages of a heap. An out-of-order extent is one for which the extent that contains the current page for a heap isn't physically the next extent after the extent that contains the previous page.

The value for `avg_fragmentation_in_percent` should be as close to zero as possible for maximum performance. However, values from 0 percent through 10 percent can be acceptable. All methods of reducing fragmentation, such as rebuilding, reorganizing, or recreating, can be used to reduce these values. For more information about how to analyze the degree of fragmentation in an index, see [Optimize index maintenance to improve query performance and reduce resource consumption](../indexes/reorganize-and-rebuild-indexes.md).

## Reduce fragmentation in an index

When an index is fragmented in a way that the fragmentation is affecting query performance, there are three choices for reducing fragmentation:

- Drop and recreate the clustered index.

  Recreating a clustered index redistributes the data and results in full data pages. The level of fullness can be configured by using the `FILLFACTOR` option in `CREATE INDEX`. The drawbacks in this method are that the index is offline during the drop and recreate cycle, and that the operation is atomic. If the index creation is interrupted, the index isn't recreated. For more information, see [CREATE INDEX](../../t-sql/statements/create-index-transact-sql.md).

- Use `ALTER INDEX REORGANIZE`, the replacement for `DBCC INDEXDEFRAG`, to reorder the leaf level pages of the index in a logical order. Because this is an online operation, the index is available while the statement is running. The operation can also be interrupted without losing work already completed. The drawback in this method is that it doesn't do as good a job of reorganizing the data as an index rebuild operation, and it doesn't update statistics.

- Use `ALTER INDEX REBUILD`, the replacement for `DBCC DBREINDEX`, to rebuild the index online or offline. For more information, see [ALTER INDEX (Transact-SQL)](../../t-sql/statements/alter-index-transact-sql.md).

Fragmentation alone isn't a sufficient reason to reorganize or rebuild an index. The main effect of fragmentation is that it slows down page read-ahead throughput during index scans. This causes slower response times. If the query workload on a fragmented table or index doesn't involve scans, because the workload is primarily singleton lookups, removing fragmentation can have no effect.

> [!NOTE]  
> Running `DBCC SHRINKFILE` or `DBCC SHRINKDATABASE` can introduce fragmentation if an index is partly or completely moved during the shrink operation. Therefore, if a shrink operation must be performed, you should do it before fragmentation is removed.

## Reduce fragmentation in a heap

To reduce the extent fragmentation of a heap, create a clustered index on the table and then drop the index. This redistributes the data while the clustered index is created. This also makes it as optimal as possible, considering the distribution of free space available in the database. When the clustered index is then dropped to recreate the heap, the data isn't moved and remains optimally in position. For information about how to perform these operations, see [CREATE INDEX](../../t-sql/statements/create-index-transact-sql.md) and [DROP INDEX](../../t-sql/statements/drop-index-transact-sql.md).

> [!CAUTION]  
> Creating and dropping a clustered index on a table, rebuilds all nonclustered indexes on that table twice.

## Compact large object data

By default, the `ALTER INDEX REORGANIZE` statement compacts pages that contain large object (LOB) data. Because LOB pages aren't deallocated when empty, compacting this data can improve disk space use if lots of LOB data is deleted, or a LOB column is dropped.

Reorganizing a specified clustered index compacts all LOB columns that are contained in the clustered index. Reorganizing a nonclustered index compacts all LOB columns that are nonkey (included) columns in the index. When `ALL` is specified in the statement, all indexes that are associated with the specified table or view are reorganized. Additionally, all LOB columns that are associated with the clustered index, underlying table, or nonclustered index with included columns, are compacted.

## Evaluate disk space use

The `avg_page_space_used_in_percent` column indicates page fullness. To achieve optimal disk space use, this value should be close to 100 percent for an index that doesn't have many random inserts. However, an index that has many random inserts and has very full pages have an increased number of page splits. This causes more fragmentation. Therefore, in order to reduce page splits, the value should be less than 100 percent. Rebuilding an index with the `FILLFACTOR` option specified allows the page fullness to be changed to fit the query pattern on the index. For more information about fill factor, see [Specify Fill Factor for an Index](../indexes/specify-fill-factor-for-an-index.md). Also, `ALTER INDEX REORGANIZE` will compact an index by trying to fill pages to the `FILLFACTOR` that was last specified. This increases the value in avg_space_used_in_percent. `ALTER INDEX REORGANIZE` can't reduce page fullness. Instead, an index rebuild must be performed.

## Evaluate index fragments

A fragment is made up of physically consecutive leaf pages in the same file for an allocation unit. An index has at least one fragment. The maximum fragments an index can have are equal to the number of pages in the leaf level of the index. Larger fragments mean that less disk I/O is required to read the same number of pages. Therefore, the larger the `avg_fragment_size_in_pages` value, the better the range scan performance. The `avg_fragment_size_in_pages` and `avg_fragmentation_in_percent` values are inversely proportional to each other. Therefore, rebuilding or reorganizing an index should reduce the amount of fragmentation and increase the fragment size.

## Limitations

Doesn't return data for clustered columnstore indexes.

## Permissions

Requires the following permissions:

- `CONTROL` permission on the specified object within the database.

- `VIEW DATABASE STATE` or `VIEW DATABASE PERFORMANCE STATE` (SQL Server 2022) permission to return information about all objects within the specified database, by using the object wildcard *@object_id* = `NULL`.

- `VIEW SERVER STATE` or `VIEW SERVER PERFORMANCE STATE` (SQL Server 2022) permission to return information about all databases, by using the database wildcard *@database_id* = `NULL`.

Granting `VIEW DATABASE STATE` allows all objects in the database to be returned, regardless of any `CONTROL` permissions denied on specific objects.

Denying `VIEW DATABASE STATE` disallows all objects in the database to be returned, regardless of any `CONTROL` permissions granted on specific objects. Also, when the database wildcard *@database_id* = `NULL` is specified, the database is omitted.

For more information, see [System dynamic management views](system-dynamic-management-views.md).

## Examples

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

### A. Return information about a specified table

The following example returns size and fragmentation statistics for all indexes and partitions of the `Person.Address` table. The scan mode is set to `LIMITED` for best performance and to limit the statistics that are returned. Executing this query requires, at a minimum, `CONTROL` permission on the `Person.Address` table.

```sql
DECLARE @db_id SMALLINT;
DECLARE @object_id INT;

SET @db_id = DB_ID(N'AdventureWorks2022');
SET @object_id = OBJECT_ID(N'AdventureWorks2022.Person.Address');

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
    SELECT * FROM sys.dm_db_index_physical_stats(@db_id, @object_id, NULL, NULL , 'LIMITED');
END;
GO
```

### B. Return information about a heap

The following example returns all statistics for the heap `dbo.DatabaseLog` in the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database. Because the table contains LOB data, a row is returned for the `LOB_DATA` allocation unit in addition to the row returned for the `IN_ROW_ALLOCATION_UNIT` that is storing the data pages of the heap. Executing this query requires, at a minimum, `CONTROL` permission on the `dbo.DatabaseLog` table.

```sql
DECLARE @db_id SMALLINT;
DECLARE @object_id INT;
SET @db_id = DB_ID(N'AdventureWorks2022');
SET @object_id = OBJECT_ID(N'AdventureWorks2022.dbo.DatabaseLog');
IF @object_id IS NULL
BEGIN;
    PRINT N'Invalid object';
END;
ELSE
BEGIN;
    SELECT * FROM sys.dm_db_index_physical_stats(@db_id, @object_id, 0, NULL , 'DETAILED');
END;
GO
```

### C. Return information for all databases

The following example returns all statistics for all tables and indexes within the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] by specifying the wildcard `NULL` for all parameters. Executing this query requires the `VIEW SERVER STATE` permission.

```sql
SELECT * FROM sys.dm_db_index_physical_stats (NULL, NULL, NULL, NULL, NULL);
GO
```

### D. Use sys.dm_db_index_physical_stats in a script to rebuild or reorganize indexes

The following example automatically reorganizes or rebuilds all partitions in a database that have an average fragmentation over 10 percent. Executing this query requires the `VIEW DATABASE STATE` permission. This example specifies `DB_ID` as the first parameter without specifying a database name.

```sql
-- Ensure a USE <databasename> statement has been executed first.
SET NOCOUNT ON;

DECLARE @objectid INT;
DECLARE @indexid INT;
DECLARE @partitioncount BIGINT;
DECLARE @schemaname NVARCHAR(130);
DECLARE @objectname NVARCHAR(130);
DECLARE @indexname NVARCHAR(130);
DECLARE @partitionnum BIGINT;
DECLARE @partitions BIGINT;
DECLARE @frag FLOAT;
DECLARE @command NVARCHAR(4000);

-- Conditionally select tables and indexes from the sys.dm_db_index_physical_stats function
-- and convert object and index IDs to names.
SELECT object_id AS objectid,
    index_id AS indexid,
    partition_number AS partitionnum,
    avg_fragmentation_in_percent AS frag
INTO #work_to_do
FROM sys.dm_db_index_physical_stats(DB_ID(), NULL, NULL, NULL, 'LIMITED')
WHERE avg_fragmentation_in_percent > 10.0
    AND index_id > 0;

-- Declare the cursor for the list of partitions to be processed.
DECLARE partitions CURSOR
FOR
SELECT *
FROM #work_to_do;

-- Open the cursor.
OPEN partitions;

-- Loop through the partitions.
WHILE (1 = 1)
BEGIN;

    FETCH NEXT
    FROM partitions
    INTO @objectid,
        @indexid,
        @partitionnum,
        @frag;

    IF @@FETCH_STATUS < 0
        BREAK;

    SELECT @objectname = QUOTENAME(o.name),
        @schemaname = QUOTENAME(s.name)
    FROM sys.objects AS o
    INNER JOIN sys.schemas AS s
        ON s.schema_id = o.schema_id
    WHERE o.object_id = @objectid;

    SELECT @indexname = QUOTENAME(name)
    FROM sys.indexes
    WHERE object_id = @objectid
        AND index_id = @indexid;

    SELECT @partitioncount = count(*)
    FROM sys.partitions
    WHERE object_id = @objectid
        AND index_id = @indexid;

    -- 30 is an arbitrary decision point at which to switch between reorganizing and rebuilding.
    IF @frag < 30.0
        SET @command = N'ALTER INDEX ' + @indexname + N' ON ' + @schemaname + N'.' + @objectname + N' REORGANIZE';

    IF @frag >= 30.0
        SET @command = N'ALTER INDEX ' + @indexname + N' ON ' + @schemaname + N'.' + @objectname + N' REBUILD';

    IF @partitioncount > 1
        SET @command = @command + N' PARTITION=' + CAST(@partitionnum AS NVARCHAR(10));

    EXEC (@command);

    PRINT N'Executed: ' + @command;
END;

-- Close and deallocate the cursor.
CLOSE partitions;

DEALLOCATE partitions;

-- Drop the temporary table.
DROP TABLE #work_to_do;
GO
```

### E. Use sys.dm_db_index_physical_stats to show the number of page-compressed pages

The following example shows how to display and compare the total number of pages against the pages that are row and page compressed. This information can be used to determine the benefit that compression is providing for an index or table.

```sql
SELECT o.name,
    ips.partition_number,
    ips.index_type_desc,
    ips.record_count,
    ips.avg_record_size_in_bytes,
    ips.min_record_size_in_bytes,
    ips.max_record_size_in_bytes,
    ips.page_count,
    ips.compressed_page_count
FROM sys.dm_db_index_physical_stats(DB_ID(), NULL, NULL, NULL, 'DETAILED') ips
INNER JOIN sys.objects o
    ON o.object_id = ips.object_id
ORDER BY record_count DESC;
```

### F. Use sys.dm_db_index_physical_stats in SAMPLED mode

The following example shows how `SAMPLED` mode returns an approximate that is different than the `DETAILED` mode results.

```sql
CREATE TABLE t3 (
    col1 INT PRIMARY KEY,
    col2 VARCHAR(500)
    )
    WITH (DATA_COMPRESSION = PAGE);
GO

BEGIN TRANSACTION

DECLARE @idx INT = 0;

WHILE @idx < 1000000
BEGIN
    INSERT INTO t3 (col1, col2)
    VALUES (
        @idx,
        REPLICATE('a', 100) + CAST(@idx AS VARCHAR(10)) + REPLICATE('a', 380)
        )

    SET @idx = @idx + 1
END

COMMIT;
GO

SELECT page_count,
    compressed_page_count,
    forwarded_record_count,
    *
FROM sys.dm_db_index_physical_stats(db_id(), object_id('t3'), NULL, NULL, 'SAMPLED');

SELECT page_count,
    compressed_page_count,
    forwarded_record_count,
    *
FROM sys.dm_db_index_physical_stats(db_id(), object_id('t3'), NULL, NULL, 'DETAILED');
```

### G. Query service broker queues for index fragmentation

**Applies to:** [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions

The following example shows how to query server broker queues for fragmentation.

```sql
--Using queue internal table name
SELECT *
FROM sys.dm_db_index_physical_stats(db_id(), object_id('sys.queue_messages_549576996'), DEFAULT, DEFAULT, DEFAULT);

--Using queue name directly
SELECT *
FROM sys.dm_db_index_physical_stats(db_id(), object_id('ExpenseQueue'), DEFAULT, DEFAULT, DEFAULT);
```

## Related content

- [System dynamic management views](system-dynamic-management-views.md)
- [Index Related Dynamic Management Views and Functions (Transact-SQL)](index-related-dynamic-management-views-and-functions-transact-sql.md)
- [sys.dm_db_index_operational_stats (Transact-SQL)](sys-dm-db-index-operational-stats-transact-sql.md)
- [sys.dm_db_index_usage_stats (Transact-SQL)](sys-dm-db-index-usage-stats-transact-sql.md)
- [sys.dm_db_partition_stats (Transact-SQL)](sys-dm-db-partition-stats-transact-sql.md)
- [sys.allocation_units (Transact-SQL)](../system-catalog-views/sys-allocation-units-transact-sql.md)
- [Transact-SQL reference (Database Engine)](../../t-sql/language-reference.md)
