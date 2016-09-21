---
title: "sys.dm_pdw_sys_info (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: a43da5bd-6d3d-448c-85ee-e869f0ccaa73
caps.latest.revision: 12
author: BarbKess
---
# sys.dm_pdw_sys_info (SQL Server PDW)
Provides a set of appliance-level counters that reflect overall activity on the appliance.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|total_sessions|**int**|Number of sessions currently in the system.|0 to max_active_sessions (see below).|  
|idle_sessions|**int**|Number of sessions currently idle.||  
|active_requests|**int**|Number of active requests currently running.||  
|queued_requests|**int**|Number of currently queued requests.||  
|active_loads|**int**|Number of loads currently running in the system.||  
|queued_loads|**int**|Number of queued loads waiting for execution.||  
|active_backups|**int**|Number of backups currently running.||  
|active_restores|**int**|Number of backup restores currently running.||  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
