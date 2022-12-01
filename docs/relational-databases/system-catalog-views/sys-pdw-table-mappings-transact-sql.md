---
title: "sys.pdw_table_mappings (Transact-SQL)"
description: sys.pdw_table_mappings (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "06/01/2018"
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "reference"
dev_langs:
  - "TSQL"
ms.assetid: 1af14fe0-e562-4f48-a7f0-783f300a88ac
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest"
---
# sys.pdw_table_mappings (Transact-SQL)
[!INCLUDE[applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

  Ties user tables to internal object names by **object_id**.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|physical_name|**nvarchar(36)**|The physical name for the table.<br /><br /> **physical_name** and **object_id** form the key for this view.||  
|object_id|**int**|The object ID for the table. See [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md).<br /><br /> **physical_name** and **object_id** form the key for this view.||  
  
## See Also  
 [Azure Synapse Analytics and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)   
 [sys.pdw_index_mappings &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-index-mappings-transact-sql.md)   
 [sys.pdw_database_mappings &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-database-mappings-transact-sql.md)  
  
  
