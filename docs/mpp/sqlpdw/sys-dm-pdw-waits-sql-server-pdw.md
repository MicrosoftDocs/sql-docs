---
title: "sys.dm_pdw_waits (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: d10e5fbc-5dd6-46f6-8139-46210fd43b06
caps.latest.revision: 14
author: BarbKess
---
# sys.dm_pdw_waits (SQL Server PDW)
Holds information about all wait states encountered during execution of a request or query, including locks, waits on transmission queues, and so on.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|wait_id|**bigint**|Unique numeric id associated with the wait state.<br /><br />Key for this view.|Unique across all waits in the system.|  
|session_id|**nvarchar(32)**|ID of the session on which the wait state occurred.|See session_id in [sys.dm_pdw_exec_sessions &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-exec-sessions-sql-server-pdw.md).|  
|type|**nvarchar(255)**|Type of wait this entry represents.|Information not available.|  
|object_type|**nvarchar(255)**|Type of object that is affected by the wait.|Information not available.|  
|object_name|**nvarchar(386)**|Name or GUID of the specified object that was affected by the wait.||  
|request_id|**nvarchar(32)**|ID of the request on which the wait state occurred.|See request_id in [sys.dm_pdw_exec_requests &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-exec-requests-sql-server-pdw.md).|  
|request_time|**datetime**|Time at which the wait state was requested.||  
|acquire_time|**datetime**|Time at which the lock or resource was acquired.||  
|state|**nvarchar(50)**|State of the wait state.|Information not available.|  
|priority|**int**|Priority of the waiting item.|Information not available.|  
  
## See Also  
[sys.dm_pdw_wait_stats &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-wait-stats-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
