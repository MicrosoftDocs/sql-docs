---
title: "sys.pdw_loader_backup_run_details (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 6751cfb0-6616-4ec4-91cc-e102acc64ef7
caps.latest.revision: 20
author: BarbKess
---
# sys.pdw_loader_backup_run_details (SQL Server PDW)
Contains further detailed information, beyond the information in [sys.pdw_loader_backup_runs &#40;SQL Server PDW&#41;](../sqlpdw/sys-pdw-loader-backup-runs-sql-server-pdw.md), about ongoing and completed backup, restore, and load operations in SQL Server PDW. The information persists across system restarts.  
  
|Column Name|Data Type|Description|Range|  
|---------------|-------------|---------------|---------|  
|run_id|**int**|Unique identifier for a specific backup or restore run.<br /><br />run_id and pdw_node_id form the key for this view.||  
|pdw_node_id|**int**|Unique identifier of an appliance node for which this record holds details.<br /><br />run_id and pdw_node_id form the key for this view.|See node_id in [sys.dm_pdw_nodes &#40;SQL Server PDW&#41;](../sqlpdw/sys-dm-pdw-nodes-sql-server-pdw.md).|  
|status|**nvarchar(16)**|The current status of the run.|'CANCELLED', 'COMPLETED', 'FAILED', 'QUEUED', 'RUNNING'|  
|start_time|**datetime**|Time at which the operation started on this particular node.||  
|end_time|**datetime**|Time at which the operation ended on this particular node, if any.||  
|total_elapsed_time|**int**|Total time the operation has been running on this particular node.|If total_elapsed_time exceeds the maximum value for an integer (24.8 days in milliseconds), it will cause materialization failure due to overflow.<br /><br />The maximum value in milliseconds is equivalent to 24.8 days.|  
|progress|**int**|Progress of the operation expressed as a percentage.|0 to 100|  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
  
