---
title: "sys.dm_pdw_node_status (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 629d1dea-842c-4a9a-89e7-578c581b937f
caps.latest.revision: 14
author: BarbKess
---
# sys.dm_pdw_node_status (SQL Server PDW)
Holds additional information (over [sys.dm_pdw_nodes &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/sys-dm-pdw-nodes-sql-server-pdw.md)) about the performance and status of all appliance nodes. It lists one row per node in the appliance.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|pdw_node_id|**int**|Unique numeric id associated with the node.<br /><br />Key for this view.|Unique across the appliance, regardless of type.|  
|process_id|**int**|Information not available.||  
|process_name|**nvarchar(255)**|Information not available.||  
|allocated_memory|**bigint**|Total allocated memory on this node.||  
|available_memory|**bigint**|Total available memory on this node.||  
|process_cpu_usage|**bigint**|Total process CPU usage, in ticks.||  
|total_cpu_usage|**bigint**|Total CPU usage, in ticks.||  
|thread_count|**bigint**|Total number of threads in use on this node.||  
|handle_count|**bigint**|Total number of handles in use on this node.||  
|total_elapsed_time|**bigint**|Total time elapsed since system start or restart.|Total time elapsed since system start or restart. If total_elapsed_time exceeds the maximum value for an integer (24.8 days in milliseconds), it will cause materialization failure due to overflow.<br /><br />The maximum value in milliseconds is equivalent to 24.8 days.|  
|is_available|**bit**|Flag indicating whether this node is available.||  
|sent_time|**datetime**|Last time a network package was sent by this node.||  
|received_time|**datetime**|Last time a network package was received by this node.||  
|error_id|**nvarchar(36)**|Unique identifier of the last error that occurred on this node.||  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
  
