---
title: "sys.dm_pdw_wait_stats (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 5829338a-14ff-4e16-a82b-b0baaf62cd70
caps.latest.revision: 7
author: BarbKess
---
# sys.dm_pdw_wait_stats (SQL Server PDW)
Holds information related to the SQL Server OS state related to instances running on the different nodes. For a list of waits types and their description, see [sys.dm_os_wait_stats](http://msdn.microsoft.com/en-us/library/ms179984(v=sql.120).aspx).  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|**pdw_node_id**|**int**|ID of the node this entry refers to.||  
|**wait_name**|**nvarchar(255)**|Name of the wait type.||  
|**max_wait_time**|**bigint**|Maximum wait time of this wait type.||  
|**request_count**|**bigint**|Number of waits of this wait type outstanding.||  
|**signal_time**|**bigint**|Difference between the time that the waiting thread was signaled and when it started running.||  
|**completed_count**|**bigint**|Total number of waits of this type completed since the last server restart.||  
|**wait_time**|**bigint**|Total wait time for this wait type in millisecons. Inclusive of signal_time.||  
  
## See Also  
[sys.dm_pdw_waits &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-waits-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
