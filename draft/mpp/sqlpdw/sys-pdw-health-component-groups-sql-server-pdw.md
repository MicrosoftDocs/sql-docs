---
title: "sys.pdw_health_component_groups (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 6e2a3634-ad16-4ddb-b7dc-0e21d5a3c31d
caps.latest.revision: 10
author: BarbKess
---
# sys.pdw_health_component_groups (SQL Server PDW)
Stores information about logical groupings of components and devices.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|group_id|**int**|Unique identifier for components and devices.<br /><br />Key for this view.|NOT NULL|  
|group_name|**nvarchar(255)**|Logical group name for the components and devices.|NOT NULL|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
