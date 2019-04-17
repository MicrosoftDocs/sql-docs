---
title: "sys.dm_pdw_node_status (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: sql
ms.technology: data-warehouse
ms.reviewer: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
ms.assetid: 8e263b65-81d0-49d0-8873-62ef424369d6
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">= aps-pdw-2016 || = sqlallproducts-allversions"
---
# sys.dm_pdw_node_status (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md](../../includes/tsql-appliesto-xxxxxx-xxxx-xxxx-pdw-md.md)]

  Holds additional information (over [sys.dm_pdw_nodes &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-nodes-transact-sql.md)) about the performance and status of all appliance nodes. It lists one row per node in the appliance.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|pdw_node_id|**int**|Unique numeric id associated with the node.<br /><br /> Key for this view.|Unique across the appliance, regardless of type.|  
|process_id|**int**|[!INCLUDE[ssInfoNA](../../includes/ssinfona-md.md)]||  
|process_name|**nvarchar(255)**|[!INCLUDE[ssInfoNA](../../includes/ssinfona-md.md)]||  
|allocated_memory|**bigint**|Total allocated memory on this node.||  
|available_memory|**bigint**|Total available memory on this node.||  
|process_cpu_usage|**bigint**|Total process CPU usage, in ticks.||  
|total_cpu_usage|**bigint**|Total CPU usage, in ticks.||  
|thread_count|**bigint**|Total number of threads in use on this node.||  
|handle_count|**bigint**|Total number of handles in use on this node.||  
|total_elapsed_time|**bigint**|Total time elapsed since system start or restart.|Total time elapsed since system start or restart. If total_elapsed_time exceeds the maximum value for an integer (24.8 days in milliseconds), it will cause materialization failure due to overflow.<br /><br /> The maximum value in milliseconds is equivalent to 24.8 days.|  
|is_available|**bit**|Flag indicating whether this node is available.||  
|sent_time|**datetime**|Last time a network package was sent by this node.||  
|received_time|**datetime**|Last time a network package was received by this node.||  
|error_id|**nvarchar(36)**|Unique identifier of the last error that occurred on this node.||  
  
## See Also  
 [SQL Data Warehouse and Parallel Data Warehouse Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)  
  
  
