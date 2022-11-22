---
title: "sys.dm_pdw_os_threads (Transact-SQL)"
description: sys.dm_pdw_os_threads (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/07/2017"
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "reference"
dev_langs:
  - "TSQL"
ms.assetid: ddc12f05-edeb-4848-b6d7-e851684cf044
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest"
---
# sys.dm_pdw_os_threads (Transact-SQL)
[!INCLUDE[applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

> [!NOTE]
> [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|pdw_node_id|**int**|The ID of the affected node.<br /><br /> pdw_node_id and thread_id form the key for this view.|See node_id in [sys.dm_pdw_nodes &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-pdw-nodes-transact-sql.md).|  
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
 [Azure Synapse Analytics and Parallel Data Warehouse Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)  
  
  
