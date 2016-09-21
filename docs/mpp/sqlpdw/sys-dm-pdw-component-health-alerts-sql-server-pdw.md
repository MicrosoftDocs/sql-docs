---
title: "sys.dm_pdw_component_health_alerts (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 89e1eab6-a557-4202-b471-0a55772c643b
caps.latest.revision: 13
author: BarbKess
---
# sys.dm_pdw_component_health_alerts (SQL Server PDW)
Stores previously issued alerts on appliance components.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|pdw_node_id|**int**|Unique identifier of a SQL Server PDW node.<br /><br />pdw_node_id, component_id, component_instance_id, alert_id, and alert_instance_id form the key for this view.|NOT NULL|  
|component_id|**int**|The ID of the component. See [sys.pdw_health_components &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-health-components-sql-server-pdw.md).<br /><br />pdw_node_id, component_id, component_instance_id, alert_id, and alert_instance_id form the key for this view.|NOT NULL|  
|component_instance_id|**nvarchar(255)**|pdw_node_id, component_id, component_instance_id, alert_id, and alert_instance_id form the key for this view.|NOT NULL|  
|alert_id|**int**|The ID for the alert type. See [sys.pdw_health_alerts &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-health-alerts-sql-server-pdw.md).<br /><br />pdw_node_id, component_id, component_instance_id, alert_id, and alert_instance_id form the key for this view.|NOT NULL|  
|alert_instance_id|**nvarchar(36)**|Identifies an instance of a given alert.<br /><br />pdw_node_id, component_id, component_instance_id, alert_id, and alert_instance_id form the key for this view.|NOT NULL|  
|previous_value|**nvarchar(255)**|Used when the alert is of type StatusChange. This is the previous component status. Value is NULL for alerts of type Threshold. See [sys.pdw_health_alerts &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-health-alerts-sql-server-pdw.md) for a list of alert types.|NULL|  
|current_value|**nvarchar(255)**|Used when the alert is of type StatusChange. This is the current component status. Value is NULL for alerts of type Threshold. See [sys.pdw_health_alerts &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-health-alerts-sql-server-pdw.md) for a list of alert types.|NULL|  
|create_time|**datetime**|Time and date when the alert was generated.|NOT NULL|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
