---
title: "sys.pdw_diag_events (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: d813aac0-cea1-4f53-b8e8-d26824bc2587
caps.latest.revision: 7
author: BarbKess
---
# sys.pdw_diag_events (SQL Server PDW)
Holds information about events that can be included in diagnostic sessions on the system.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|**name**|**nvarchar(255)**|Name of the specific diagnostics event.||  
|**source**|**nvarchar(255)**|Source of the event (engine, general, dms, etc.)||  
|**is_enabled**|**bit**|Whether the event is being published.||  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
