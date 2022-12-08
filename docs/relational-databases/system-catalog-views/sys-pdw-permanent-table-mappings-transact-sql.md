---
title: "sys.pdw_permanent_table_mappings (Transact-SQL)"
description: Ties permanent user tables to internal object names by **object_id**.
author: mstehrani
ms.author: emtehran
ms.date: "07/24/2020"
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: "=azure-sqldw-latest"
---
# sys.pdw_permanent_table_mappings (Transact-SQL)
[!INCLUDE [applies-to-version/asa](../../includes/applies-to-version/asa.md)]

Ties permanent user tables to internal object names by **object_id**.  
  
> [!NOTE]
> **sys.pdw_permanent_table_mappings** holds mappings to permanent tables and does not include temporary or external table mappings.

|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|physical_name|**nvarchar(36)**|The physical name for the table.<br /><br /> **physical_name** and **object_id** form the key for this view.|  
|object_id|**int**|The object ID for the table. See [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md).<br /><br /> **physical_name** and **object_id** form the key for this view.|  
  
## See Also  
 [Azure Synapse Analytics and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)   
 [sys.pdw_index_mappings &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-index-mappings-transact-sql.md)  
  
  
