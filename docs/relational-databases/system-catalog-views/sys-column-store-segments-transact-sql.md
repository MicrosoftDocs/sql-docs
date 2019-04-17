---
title: "sys.column_store_segments (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/15/2018"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "column_store_segments"
  - "sys.column_store_segments_TSQL"
  - "sys.column_store_segments"
  - "column_store_segments_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.column_store_segments catalog view"
ms.assetid: 1253448c-2ec9-4900-ae9f-461d6b51b2ea
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# sys.column_store_segments (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2014-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2014-xxxx-xxxx-xxx-md.md)]

Returns one row for each column segment in a columnstore index. There is one column segment per column per rowgroup. For example, a table with 10 rowgroups and 34 columns returns 340 rows. 
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**partition_id**|**bigint**|Indicates the partition ID. Is unique within a database.|  
|**hobt_id**|**bigint**|ID of the heap or B-tree index (hobt) for the table that has this columnstore index.|  
|**column_id**|**int**|ID of the columnstore column.|  
|**segment_id**|**int**|ID of the rowgroup. For backward compatibility, the column name continues to be called segment_id even though this is the rowgroup ID. You can uniquely identify a segment using \<hobt_id, partition_id, column_id>, <segment_id>.|  
|**version**|**int**|Version of the column segment format.|  
|**encoding_type**|**int**|Type of encoding used for that segment:<br /><br /> 1 = VALUE_BASED     -  non-string/binary with no dictionary (very similar to 4 with some internal variations)<br /><br /> 2 = VALUE_HASH_BASED   - non-string/binary column with common values in dictionary<br /><br /> 3 = STRING_HASH_BASED  - string/binary column with common values in dictionary<br /><br /> 4 = STORE_BY_VALUE_BASED - non-string/binary with no dictionary<br /><br /> 5 = STRING_STORE_BY_VALUE_BASED - string/binary with no dictionary<br /><br /> All encodings take advantage of bit-packing and run-length encoding when possible.|  
|**row_count**|**int**|Number of rows in the row group.|  
|**has_nulls**|**int**|1 if the column segment has null values.|  
|**base_id**|**bigint**|Base value id if encoding type 1 is being used.  If encoding type 1 is not being used, base_id is set to -1.|  
|**magnitude**|**float**|Magnitude if encoding type 1 is being used.  If encoding type 1 is not being used, magnitude is set to -1.|  
|**primary_dictionary_id**|**int**|A value of 0 represents the global dictionary. A value of -1 indicates that there is no global dictionary created for this column.|  
|**secondary_dictionary_id**|**int**|A non-zero value points to the local dictionary for this column in the current segment (i.e. the rowgroup). A value of -1 indicates that there is no local dictionary for this segment.|  
|**min_data_id**|**bigint**|Minimum data id in the column segment.|  
|**max_data_id**|**bigint**|Maximum data id in the column segment.|  
|**null_value**|**bigint**|Value used to represent nulls.|  
|**on_disk_size**|**bigint**|Size of segment in bytes.|  
  
## Remarks  
 The following query returns information about segments of a columnstore index.  
  
```sql  
SELECT i.name, p.object_id, p.index_id, i.type_desc,   
    COUNT(*) AS number_of_segments  
FROM sys.column_store_segments AS s   
INNER JOIN sys.partitions AS p   
    ON s.hobt_id = p.hobt_id   
INNER JOIN sys.indexes AS i   
    ON p.object_id = i.object_id  
WHERE i.type = 5 OR i.type = 6  
GROUP BY i.name, p.object_id, p.index_id, i.type_desc ;  
GO  
```  
  
## Permissions  
 All columns require at least **VIEW DEFINITION** permission on the table. The following columns return null unless the user also has **SELECT** permission: has_nulls, base_id, magnitude, min_data_id, max_data_id, and null_value.  
  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.md)   
 [sys.columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-columns-transact-sql.md)   
 [sys.all_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-all-columns-transact-sql.md)   
 [sys.computed_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-computed-columns-transact-sql.md)   
 [Columnstore Indexes Guide](~/relational-databases/indexes/columnstore-indexes-overview.md)    
 [sys.column_store_dictionaries &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-column-store-dictionaries-transact-sql.md)  
  
  

