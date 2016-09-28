---
title: "sys.pdw_health_components (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 84b18b63-66f7-491a-a280-fc0c2b6e7faf
caps.latest.revision: 10
author: BarbKess
---
# sys.pdw_health_components (SQL Server PDW)
Stores information about all components and devices that exist in the system. These include hardware, storage devices, and network devices.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|component_id|**int**|Unique identifier of a component or device.<br /><br />Key for this view.|NOT NULL|  
|group_id|**Int**|The logical component group to which this component belongs. See [sys.pdw_health_component_groups &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-health-component-groups-sql-server-pdw.md).|NOT NULL|  
|component_name|**nvarchar(255)**|Name of the component.|NOT NULL|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
