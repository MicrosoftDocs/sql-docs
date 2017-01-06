---
title: "sys.dm_pdw_query_stats_xe (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 89690bc9-d3d3-4264-9ac1-60b23d0599e1
caps.latest.revision: 16
author: BarbKess
---
# sys.dm_pdw_query_stats_xe (SQL Server PDW)
This DMV is deprecated and will be removed in a future release. In this release, it returns 0 rows.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|event|**nvarchar(60)**|Key for this view.||  
|event_id|**nvarchar(36)**|||  
|create_time|**datetime**|||  
|session_id|**int**|The id for the session.|See session_id in [sys.dm_pdw_exec_sessions &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-exec-sessions-sql-server-pdw.md).|  
|cpu|**int**|||  
|reads|**int**|Number of logical reads since the start of the event.||  
|writes|**int**|Number of logical writes since the start of the event.||  
|sql_text|**nvarchar(4000)**|||  
|client_app_name|**nvarchar(255)**|||  
|tsql_stack|**nvarchar(255)**|||  
|pdw_node_id|**int**|Node on which this Xevent instance is running.|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
