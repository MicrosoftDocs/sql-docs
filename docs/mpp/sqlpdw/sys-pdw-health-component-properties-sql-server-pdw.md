---
title: "sys.pdw_health_component_properties (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 1de9aba8-723c-458f-a6b5-d72a5d684784
caps.latest.revision: 10
author: BarbKess
---
# sys.pdw_health_component_properties (SQL Server PDW)
Stores properties that describe a device. Some properties show device status and some properties describe the device itself.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|property_id|**int**|Unique identifier of the property of a component.<br /><br />property_id and component_id form the key for this view.|NOT NULL|  
|component_id|**int**|The ID of the component. See [sys.pdw_health_components &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-pdw-health-components-sql-server-pdw.md).<br /><br />property_id and component_id form the key for this view.|NOT NULL|  
|property_name|**nvarchar(255)**|Name of the property.|NOT NULL|  
|physical_name|**nvarchar(32)**|Property name as defined by the manufacturer.|NOT NULL|  
|is_key|**bit**|Determines whether the device instance is unique or not unique.|NOT NULL<br /><br />0 - Device instance is unique.<br /><br />1 - Device instance is not unique.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
  
