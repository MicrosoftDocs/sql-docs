---
title: "sys.dm_db_page_info (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/18/2018"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: conceptual
f1_keywords: 
  - "sys.dm_db_page_info"
  - "sys.dm_db_page_info_TSQL"
  - "dm_db_page_info"
  - "dm_db_page_info_TSQL"
  - "dbcc page"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_db_page_info dynamic management view"
author: ""
ms.author: "pamela"
manager: "amitban"
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions"
---
# sys.dm_db_page_info (Transact-SQL)
[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

Returns information about a page in a database.  The function returns one row that contains the header information from the page, including the `object_id`, `index_id`, and `partition_id`.  This function replaces the need to use `DBCC PAGE` in most cases.

## Syntax   
```  
sys.dm_db_page_info ( DatabaseId, FileId, PageId, Mode )  
``` 

## Arguments  
*DatabaseId* | NULL | DEFAULT     
Is the ID of the database. *DatabaseId* is **smallint**. Valid input is the ID number of a database. The default is NULL, however sending a NULL value for this parameter will result in an error.
 
*FileId* | NULL | DEFAULT   
Is the ID of the file. *FileId* is **int**.  Valid input is the ID number of a file in the database specified by *DatabaseId*. The default is NULL, however sending a NULL value for this parameter will result in an error.

*PageId* | NULL | DEFAULT   
Is the ID of the page.  *PageId* is **int**.  Valid input is the ID number of a page in the file specified by *FileId*. The default is NULL, however sending a NULL value for this parameter will result in an error.

*Mode* | NULL | DEFAULT   
Determines the level of detail in the output of the function. 'LIMITED' will return NULL values for all description columns, 'DETAILED' will populate description columns.  DEFAULT is 'LIMITED.'

## Table Returned  

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|database_id |int |Database ID |
|file_id |int |File ID |
|page_id |int |Page ID |
|page_type |int |Page Type |
|page_type_desc |nvarchar(64) |Description of the page type |
|page_flag_bits |nvarchar(64) |Flag bits in page header |
|page_flag_bits_desc |nvarchar(256) |Flag bits description in page header |
|page_type_flag_bits |nvarchar(64) |Type Flag bits in page header |
|page_type_flag_bits_desc |nvarchar(64) |Type flag bits description in page header |
|object_id |int |ID of the object owning the page |
|index_id |int |ID of the index (0 for heap data pages) |
|partition_id |bigint |ID of the partition |
|alloc_unit_id |bigint |ID of the allocation unit |
|page_level |int |Level of the page in index (leaf = 0) |
|slot_count |smallint |Total number of slots (used and unused) <br> For a data page, this number is equivalent to the number of rows. |
|ghost_rec_count |smallint |Number of records marked as ghost on the page <br> A ghosted record is one that has been marked for deletion but has yet to be removed. |
|torn_bits |int |1 bit per sector for detecting torn writes. Used also to store checksum <br> This value is used to detect data corruption |
|is_iam_pg |bit |Bit to indicate whether or not the page is an IAM page  |
|is_mixed_ext |bit |Bit to indicate if allocated in a mixed extent |
|pfs_file_id |smallint |File ID of corresponding PFS page |
|pfs_page_id |int |Page ID of corresponding PFS page |
|pfs_alloc_percent |int |Allocation percent as indicated by PFS byte |
|pfs_status |nvarchar(64) |PFS byte |
|pfs_status_desc |nvarchar(64) |Description of the PFS byte |
|gam_file_id |smallint |File ID of the corresponding GAM page |
|gam_page_id |int |Page ID of the corresponding GAM page |
|gam_status |bit |Bit to indicate if allocated in GAM |
|gam_status_desc |nvarchar(64) |Description of the GAM status bit |
|sgam_file_id |smallint |File ID of the corresponding SGAM page |
|sgam_page_id |int |Page ID of the corresponding SGAM page |
|sgam_status |bit |Bit to indicate if allocated in SGAM |
|sgam_status_desc |nvarchar(64) |Description of the SGAM status bit |
|diff_map_file_id |smallint |File ID of the corresponding differential bitmap page |
|diff_map_page_id |int |Page ID of the corresponding differential bitmap page |
|diff_status |bit |Bit to indicate if diff status is changed |
|diff_status_desc |nvarchar(64) |Description of the diff status bit |
|ml_file_id |smallint |File ID of the corresponding minimal logging bitmap page |
|ml_page_id |int |Page ID of the corresponding minimal logging bitmap page |
|ml_status |bit |Bit to indicate if the page is minimally logged |
|ml_status_desc |nvarchar(64) |Description of the minimal logging status bit |
|free_bytes |smallint |Number of free bytes on the page |
|free_data_offset |int |Offset of free space at end of data area |
|reserved_bytes |smallint |Number of free bytes reserved by all transactions (if heap) <br> Number of ghosted rows (if index leaf) |
|reserved_xdes_id |smallint |Space contributed by m_xdesID to m_reservedCnt <br> For debugging purposes only |
|xdes_id |nvarchar(64) |Latest transaction contributed by m_reserved <br> For debugging purposes only |
|prev_page_file_id |smallint |Previous page file ID |
|prev_page_page_id |int |Previous page page ID |
|next_page_file_id |smallint |Next page file ID |
|next_page_page_id |int |Next page page ID |
|min_len |smallint |Length of fixed size rows |
|lsn |nvarchar(64) |Log sequence number / timestamp |
|header_version |int |Page header version |

## Remarks
The `sys.dm_db_page_info` dynamic management function returns page information like `page_id`, `file_id`, `index_id`, `object_id` etc. that are present in a page header. This information is useful for troubleshooting and debugging various performance (lock and latch contention) and corruption issues.

`sys.dm_db_page_info` can be used in place of the `DBCC PAGE` statement in many cases, but it returns only the page header information, not the body of the page. `DBCC PAGE` will still be needed for use cases where the entire contents of the page are required.

## Using in Conjunction With Other DMVs
One of the important use cases of `sys.dm_db_page_info` is to join it with other DMVs that expose page information.  To facilitate this use case, a new column called `page_resource` has been added which exposes page information in an 8-byte hex format. This column has been added to `sys.dm_exec_requests` and `sys.sysprocesses` and will be added to other DMVs in the future as needed.

A new function, `sys.fn_PageResCracker`, takes the `page_resource` as input and outputs a single row that contains `database_id`, `file_id` and `page_id`.  This function can then be used to facilitate joins between `sys.dm_exec_requests` or `sys.sysprocesses` and `sys.dm_db_page_info`.

## Permissions  
Requires the `VIEW DATABASE STATE` permission in the database.  
  
## Examples  
  
### A. Displaying all the properties of a page
The following query returns one row with all the page information for a given `database_id`, `file_id`, `page_id` combination with default mode ('LIMITED')

```sql
SELECT *  
FROM sys.dm_db_page_info (5, 1, 15, DEFAULT)
```

### B. Using sys.dm_db_page_info with other DMVs 

The following query returns one row per `wait_resource` exposed by `sys.dm_exec_requests` when the row contains a non-null `page_resource`

```sql
SELECT page_info.* 
FROM sys.dm_exec_requests AS d  
CROSS APPLY sys.fn_PageResCracker (d.page_resource) AS r  
CROSS APPLY sys.dm_db_page_info(r.db_id, r.file_id, r.page_id, 'LIMITED') AS page_info
```

## See Also  
[Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
[Database Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/database-related-dynamic-management-views-transact-sql.md)   
[sys.dm_exec_requests &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql.md)     
[sys.fn_PageResCracker](../../relational-databases/system-functions/sys-fn-pagerescracker-transact-sql.md)


