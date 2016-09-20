---
title: "sys.pdw_health_alerts (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 6ffdbdc0-f87f-44f5-a280-5563d26def04
caps.latest.revision: 11
author: BarbKess
---
# sys.pdw_health_alerts (SQL Server PDW)
Stores properties for the different alerts that can occur on the system; this is a catalog table for alerts.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|alert_id|**int**|Unique identifier of the alert.<br /><br />Key for this view.|NOT NULL|  
|component_id|**int**|ID of the component this alert applies to. The component is a general component identifier, such as "Power Supply," and is not specific to an installation. See [sys.pdw_health_components &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-pdw-health-components-sql-server-pdw.md).|NOT NULL|  
|alert_name|**nvarchar(255)**|Name of the alert.|NOT NULL|  
|state|**nvarchar(32)**|State of the alert.|NOT NULL<br /><br />Possible values:<br /><br />'Operational'<br /><br />'NonOperational'<br /><br />'Degraded'<br /><br />'Failed'|  
|severity|**nvarchar(32)**|Severity of the alert.|NOT NULL<br /><br />Possible values:<br /><br />'Informational'<br /><br />'Warning'<br /><br />'Error'|  
|type|**nvarchar(32)**|Type of alert.|NOT NULL<br /><br />Possible values:<br /><br />StatusChange - The device status has changed.<br /><br />Threshold - A value has exceeded the threshold value.|  
|description|**nvarchar(4000)**|Description of the alert.|NOT NULL|  
|condition|**nvarchar(255)**|Used when type = Threshold. Defines how the alert threshold is calculated.|NULL|  
|status|**nvarchar(32)**|Alert status|NULL|  
|condition_value|**bit**|Indicates whether the alert is allowed to occur during system operation.|NULL<br /><br />Possible values<br /><br />0 - alert is not generated.<br /><br />1 - alert is generated.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
  
