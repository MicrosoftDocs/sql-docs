---
title: "sys.dm_pdw_sys_info (Transact-SQL)"
description: sys.dm_pdw_sys_info (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "03/07/2017"
ms.service: sql
ms.subservice: data-warehouse
ms.topic: "reference"
dev_langs:
  - "TSQL"
ms.assetid: 686976b4-2d5d-4d64-bf12-56eba1dc59b1
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest"
---
# sys.dm_pdw_sys_info (Transact-SQL)
[!INCLUDE[applies-to-version/asa-pdw](../../includes/applies-to-version/asa-pdw.md)]

  Provides a set of appliance-level counters that reflect overall activity on the appliance.  
  
|Column Name|Data Type|Description|Range|  
|-----------------|---------------|-----------------|-----------|  
|total_sessions|**int**|Number of sessions currently in the system.|0 to max_active_sessions (see below).|  
|idle_sessions|**int**|Number of sessions currently idle.||  
|active_requests|**int**|Number of active requests currently running.||  
|queued_requests|**int**|Number of currently queued requests.||  
|active_loads|**int**|Number of loads currently running in the system.||  
|queued_loads|**int**|Number of queued loads waiting for execution.||  
|active_backups|**int**|Number of backups currently running.||  
|active_restores|**int**|Number of backup restores currently running.||  
  
## See Also  
 [Azure Synapse Analytics and Parallel Data Warehouse Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-and-parallel-data-warehouse-dynamic-management-views.md)  
  
  
