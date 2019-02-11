---
title: "sys.pdw_replicated_table_cache_state (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "07/03/2017"
ms.prod: sql
ms.technology: data-warehousee"
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
author: "ronortloff"
ms.author: "rortloff"
manager: craigg
monikerRange: "= azure-sqldw-latest || = sqlallproducts-allversions"
---
# sys.pdw_replicated_table_cache_state (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md](../../includes/tsql-appliesto-xxxxxx-xxxx-asdw-xxx-md.md)]

  Returns the state of the cache associated with a replicated table by **object_id**.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|object_id|**int**|The object ID for the table. See [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md).<br /><br /> **object_id** is the key for this view.||  
|state|**nvarchar(40)**|The replicated table cache state for this table.|'NotReady','Ready'|  
  
## Example
This example joins sys.pdw_replicated_table_cache_state with sys.tables to retrieve the table name and the state of the replicated table cache.

```sql
SELECT t.[name], p.[object_id], p.[state]
  FROM sys.pdw_replicated_table_cache_state p 
  JOIN sys.tables t ON t.object_id = p.object_id
```



## Next steps  
 For a list of all the catalog views for SQL Data Warehouse and Parallel Data Warehouse, see [SQL Data Warehouse and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md).   
  
