---
title: "sys.pdw_nodes_column_store_dictionaries (Transact-SQL) | Microsoft Docs"
ms.date: "03/03/2017"
ms.prod: ""
ms.prod_service: "sql-data-warehouse, pdw"
ms.reviewer: ""
ms.custom: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 7ae1c2e4-45c0-4880-a692-1f299fbcfd19
author: ronortloff
ms.author: rortloff
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azure-sqldw-latest || = sqlallproducts-allversions"
---
# sys.pdw_nodes_column_store_dictionaries (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-pdw-md.md)]

  Contains a row for each dictionary used in columnstore indexes. Dictionaries are used to encode some, but not all data types, therefore not all columns in a columnstore index have dictionaries. A dictionary can exist as a primary dictionary (for all segments) and possibly for other secondary dictionaries used for a subset of the column's segments.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**partition_id**|**bigint**|Indicates the partition ID. Is unique within a database.|  
|**hobt_id**|**bigint**|ID of the heap or B-tree index (hobt) for the table that has this columnstore index.|  
|**column_id**|**int**|ID of the columnstore column.|  
|**dictionary_id**|**int**|Id of the dictionary.|  
|**version**|**int**|Version of the dictionary format.|  
|**type**|**int**|Dictionary type:<br /><br /> 1 - Hash dictionary containing **int** values<br /><br /> 2 - Not used<br /><br /> 3 - Hash dictionary containing string values<br /><br /> 4 - Hash dictionary containing **float** values|  
|**last_id**|**int**|The last data id in the dictionary.|  
|**entry_count**|**bigint**|Number of entries in the dictionary.|  
|**on_disc_size**|**bigint**|Size of dictionary in bytes.|  
|**pdw_node_id**|**int**|Unique identifier of a [!INCLUDE[ssSDW](../../includes/sssdw-md.md)] node.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission.  
  
## See Also  
 [SQL Data Warehouse and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)   
 [CREATE COLUMNSTORE INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/create-columnstore-index-transact-sql.md)   
 [sys.pdw_nodes_column_store_segments &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-nodes-column-store-segments-transact-sql.md)   
 [sys.pdw_nodes_column_store_row_groups &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-nodes-column-store-row-groups-transact-sql.md)  
  
  
