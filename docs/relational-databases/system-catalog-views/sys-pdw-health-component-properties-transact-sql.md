---
title: "sys.pdw_health_component_properties (Transact-SQL)"
description: sys.pdw_health_component_properties (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "05/12/2022"
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
dev_langs:
  - "TSQL"
ms.assetid: 66999c0c-dc43-4327-99fb-8366f465e69d
monikerRange: ">=aps-pdw-2016"
---
# sys.pdw_health_component_properties (Transact-SQL)
[!INCLUDE [pdw](../../includes/applies-to-version/pdw.md)]

  Stores properties that describe a device. Some properties show device status and some properties describe the device itself.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|property_id|**int**|Unique identifier of the property of a component.<br /><br /> property_id and component_id form the key for this view.|NOT NULL|  
|component_id|**int**|The ID of the component. See [sys.pdw_health_components &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-pdw-health-components-transact-sql.md).<br /><br /> property_id and component_id form the key for this view.|NOT NULL|  
|property_name|**nvarchar(255)**|Name of the property.|NOT NULL|  
|physical_name|**nvarchar(32)**|Property name as defined by the manufacturer.|NOT NULL|  
|is_key|**bit**|Determines whether the device instance is unique or not unique.|NOT NULL<br /><br /> 0 - Device instance is unique.<br /><br /> 1 - Device instance is not unique.|  
  
## See Also  
 [Azure Synapse Analytics and Parallel Data Warehouse Catalog Views](../../relational-databases/system-catalog-views/sql-data-warehouse-and-parallel-data-warehouse-catalog-views.md)  
  
  
