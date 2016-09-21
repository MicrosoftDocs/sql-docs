---
title: "sys.dm_pdw_component_health_status (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: aa5ed25b-b5d2-4f6d-a7ff-fbbd27f21dbd
caps.latest.revision: 13
author: BarbKess
---
# sys.dm_pdw_component_health_status (SQL Server PDW)
Holds information about the current health of appliance components.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|pdw_node_id|**int**||Not NULL|  
|component_id|int|The ID of the component. See [sys.pdw_health_components &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-health-components-sql-server-pdw.md).<br /><br />pdw_node_id, component_id, property_id, and component_instance_id form the key for this view.|Not NULL|  
|property_id|**int**|The ID of the property. See [sys.pdw_health_component_properties &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-health-component-properties-sql-server-pdw.md).|NOT NULL|  
|component_instance_id|**nvarchar(255)**|Identifies an instance of a component. For example, an instance of a CPU might be identified by component_instance_id='CPU1'.<br /><br />pdw_node_id, component_id, property_id, and component_instance_id form the key for this view.|NOT NULL|  
|property_value|**nvarchar(255)**|The current property value.|NULL|  
|update_time|**datetime**|The last time the metric was updated.|NOT NULL|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
