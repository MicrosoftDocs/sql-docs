---
title: "sys.column_store_dictionaries (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.column_store_dictionaries_TSQL"
  - "column_store_dictionaries"
  - "column_store_dictionaries_TSQL"
  - "sys.column_store_dictionaries"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.column_store_dictionaries catalog view"
ms.assetid: 56efd563-2f72-4caf-94e3-8a182385c173
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# sys.column_store_dictionaries (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Contains a row for each dictionary used in xVelocity memory optimized columnstore indexes. Dictionaries are used to encode some, but not all data types, therefore not all columns in a columnstore index have dictionaries. A dictionary can exist as a primary dictionary (for all segments) and possibly for other secondary dictionaries used for a subset of the column's segments.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**hobt_id**|**bigint**|ID of the heap or B-tree index (hobt) for the table that has this columnstore index.|  
|**column_id**|**int**|ID of the columnstore column starting with 1. The first column has ID = 1, the second column has ID = 2, etc.|  
|**dictionary_id**|**int**|There can be two kinds of dictionaries, global and local, associated with a column segment. A dictionary_id of 0 represents the global dictionary that is shared across all column segments (one for each row group) for that column.|  
|**version**|**int**|Version of the dictionary format.|  
|**type**|**int**|Dictionary type:<br /><br /> 1 - Hash dictionary containing **int** values<br /><br /> 2 - Not used<br /><br /> 3 - Hash dictionary containing string values<br /><br /> 4 - Hash dictionary containing **float** values<br /><br /> For more information about dictionaries, see [Columnstore Indexes Guide](~/relational-databases/indexes/columnstore-indexes-overview.md).|  
|**last_id**|**int**|The last data ID in the dictionary.|  
|**entry_count**|**bigint**|Number of entries in the dictionary.|  
|**on_disc_size**|**bigint**|Size of dictionary in bytes.|  
|**partition_id**|**bigint**|Indicates the partition ID. Is unique within a database.|  
  
## Permissions  
 All columns require at least VIEW DEFINITION permission on the table. The following columns return null unless the user also has **SELECT** permission: last_id, entry_count, data_ptr.  
  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Querying the SQL Server System Catalog FAQ](../../relational-databases/system-catalog-views/querying-the-sql-server-system-catalog-faq.md)   
 [sys.columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-columns-transact-sql.md)   
 [sys.all_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-all-columns-transact-sql.md)   
 [sys.computed_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-computed-columns-transact-sql.md)   
 [Columnstore Indexes Guide](~/relational-databases/indexes/columnstore-indexes-overview.md)   
 [Columnstore Indexes Guide](~/relational-databases/indexes/columnstore-indexes-overview.md)   
 [sys.column_store_segments &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-column-store-segments-transact-sql.md)  
  
  

