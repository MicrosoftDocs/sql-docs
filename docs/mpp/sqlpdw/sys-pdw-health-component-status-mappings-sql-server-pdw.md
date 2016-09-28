---
title: "sys.pdw_health_component_status_mappings (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: a98bb9e6-7e95-4330-b8e3-abb9badac89a
caps.latest.revision: 11
author: BarbKess
---
# sys.pdw_health_component_status_mappings (SQL Server PDW)
Defines the mapping between the SQL Server PDW component statuses and the manufacturer-defined component names.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|property_id|**int**|Unique identifier of the property.<br /><br />property_id, component_id, and physical_name form the key for this view.|NOT NULL|  
|component_id|**int**|The ID of the component. See [sys.pdw_health_components &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-health-components-sql-server-pdw.md).<br /><br />property_id, component_id, and physical_name form the key for this view.|NOT NULL|  
|physical_name|**nvarchar(32)**|Property name as defined by the manufacturer.<br /><br />property_id, component_id, and physical_name form the key for this view.|NOT NULL|  
|logical_name|**nvarchar(255)**|Property name as defined by SQL Server PDW.|NOT NULL<br /><br />0 - Device instance is unique.<br /><br />1 - Device instance is not unique.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
