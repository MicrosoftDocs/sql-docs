---
title: "sys.pdw_health_component_status_mappings (Transact-SQL)"
description: sys.pdw_health_component_status_mappings (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "05/12/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
dev_langs:
  - "TSQL"
ms.assetid: 4272cfad-5ad7-493d-9edd-d9111619bda0
monikerRange: ">=aps-pdw-2016"
---
# sys.pdw_health_component_status_mappings (Transact-SQL)
[!INCLUDE [pdw](../../includes/applies-to-version/pdw.md)]

  Defines the mapping between the [!INCLUDE[ssDW](../../includes/ssdw-md.md)] component statuses and the manufacturer-defined component names.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|property_id|**int**|Unique identifier of the property.<br /><br /> property_id, component_id, and physical_name form the key for this view.|NOT NULL|  
|component_id|**int**|The ID of the component. See [sys.pdw_health_components &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-health-components-transact-sql.md).<br /><br /> property_id, component_id, and physical_name form the key for this view.|NOT NULL|  
|physical_name|**nvarchar(32)**|Property name as defined by the manufacturer.<br /><br /> property_id, component_id, and physical_name form the key for this view.|NOT NULL|  
|logical_name|**nvarchar(255)**|Property name as defined by [!INCLUDE[ssDW](../../includes/ssdw-md.md)].|NOT NULL<br /><br /> 0 - Device instance is unique.<br /><br /> 1 - Device instance is not unique.|  
  
## See Also  
 [Azure Synapse Analytics and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)  
  
  
