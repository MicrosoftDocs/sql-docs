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
monikerRange: "=azure-sqldw-latest"
---
# sys.pdw_health_component_status_mappings (Transact-SQL)
[!INCLUDE [asa-md](../../includes/applies-to-version/asa.md)]

  Defines the mapping between the [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] component statuses and the manufacturer-defined component names.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|property_id|**int**|Unique identifier of the property.<br /><br /> property_id, component_id, and physical_name form the key for this view.|NOT NULL|  
|component_id|**int**|The ID of the component. See [sys.pdw_health_components &#40;Transact-SQL&#41;](sys-pdw-health-components-transact-sql.md).<br /><br /> property_id, component_id, and physical_name form the key for this view.|NOT NULL|  
|physical_name|**nvarchar(32)**|Property name as defined by the manufacturer.<br /><br /> property_id, component_id, and physical_name form the key for this view.|NOT NULL|  
|logical_name|**nvarchar(255)**|Property name as defined by [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)].|NOT NULL<br /><br /> 0 - Device instance is unique.<br /><br /> 1 - Device instance is not unique.|  
  
## Related content

- [Azure Synapse Analytics catalog views](azure-synapse-analytics-catalog-views.md)
