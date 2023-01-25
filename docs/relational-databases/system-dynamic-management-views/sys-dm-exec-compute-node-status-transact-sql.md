---
title: "sys.dm_exec_compute_node_status (Transact-SQL)"
description: sys.dm_exec_compute_node_status (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/04/2019
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "DM_EXEC_COMPUTE_NODE_STATUS_TSQL"
  - "DM_EXEC_COMPUTE_NODE_STATUS"
helpviewer_keywords:
  - "PolyBase,views"
  - "PolyBase"
  - "dm_exec_compute_node_status"
  - "sys.dm_exec_compute_node_status management view"
dev_langs:
  - "TSQL"
ms.assetid: b606f91f-3a08-4a4f-bb57-32ae155b3738
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_exec_compute_node_status (Transact-SQL)
[!INCLUDE [sqlserver2016-asdbmi](../../includes/applies-to-version/sqlserver2016-asdbmi.md)]

  Holds additional information about the performance and status of all PolyBase nodes. Lists one row per node.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|compute_node_id|`int`|Unique numeric id associated with the node.|Unique across scale-out cluster regardless of type.|  
|process_id|`int`|||  
|process_name|`nvarchar(255)`|Logical name of the node.|Any string of appropriate length.|  
|allocated_memory|`bigint`|Total allocated memory on this node.||  
|available_memory|`bigint`|Total available memory on this node.||  
|process_cpu_usage|`bigint`|Total process CPU usage, in ticks.||  
|total_cpu_usage|`bigint`|Total CPU usage, in ticks.||  
|thread_count|`bigint`|Total number of threads in use on this node.||  
|handle_count|`bigint`|Total number of handles in use on this node.||  
|total_elapsed_time|`bigint`|Total time elapsed since system start or restart.|Total time elapsed since system start or restart. If total_elapsed_time exceeds the maximum value for an integer (24.8 days in milliseconds), it will cause materialization failure due to overflow.The maximum value in milliseconds is equivalent to 24.8 days.|  
|is_available|`bit`|Flag indicating whether this node is available.||  
|sent_time|`datetime`|Last time a network package was sent by this||  
|received_time|`datetime`|Last time a network package was sent by this node.||  
|error_id|`nvarchar(36)`|Unique identifier of the last error that occurred on this node.||
|compute_pool_id|`int`|Unique identifier for the pool.|

## See Also  
 [PolyBase troubleshooting with dynamic management views](/previous-versions/sql/sql-server-2016/mt146389(v=sql.130))   
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Database Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/database-related-dynamic-management-views-transact-sql.md)  
  
