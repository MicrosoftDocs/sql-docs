---
title: "sys.pdw_replicated_table_cache_state (Transact-SQL)"
description: sys.pdw_replicated_table_cache_state (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "07/03/2017"
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "reference"
ms.custom: seo-dt-2019
dev_langs:
  - "TSQL"
monikerRange: "=azure-sqldw-latest"
---
# sys.pdw_replicated_table_cache_state (Transact-SQL)
[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

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
 For a list of all the catalog views for Azure Synapse Analytics and Parallel Data Warehouse, see [Azure Synapse Analytics and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md).   
  
