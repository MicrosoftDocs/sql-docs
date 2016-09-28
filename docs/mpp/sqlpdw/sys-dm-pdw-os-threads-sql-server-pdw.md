---
title: "sys.dm_pdw_os_threads (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 8ae4b2e9-34ca-4170-9c1f-1ee1af3c32e4
caps.latest.revision: 9
author: BarbKess
---
# sys.dm_pdw_os_threads (SQL Server PDW)
  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|pdw_node_id|**int**|The ID of the affected node.<br /><br />pdw_node_id and thread_id form the key for this view.|See node_id in sys.[sys.dm_pdw_nodes &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-nodes-sql-server-pdw.md).|  
|thread_id|**int**|pdw_node_id and thread_id form the key for this view.||  
|process_id|**int**|||  
|name|**nvarchar(255)**|||  
|priority|**int**|||  
|start_time|**datetime**|||  
|state|**nvarchar(32)**|||  
|wait_reason|**nvarchar(32)**|||  
|total_processor_elapsed_time|**bigint**|Total kernel time used by the thread.||  
|total_user_elapsed_time|**bigint**|Total user time used by the thread||  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
