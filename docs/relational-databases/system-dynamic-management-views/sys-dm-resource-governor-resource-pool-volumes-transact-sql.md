---
title: "sys.dm_resource_governor_resource_pool_volumes (Transact-SQL)"
description: sys.dm_resource_governor_resource_pool_volumes (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "08/09/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_resource_governor_resource_pool_volumes_TSQL"
  - "dm_resource_governor_resource_pool_volumes_TSQL"
  - "dm_resource_governor_resource_pool_volumes"
  - "sys.dm_resource_governor_resource_pool_volumes"
helpviewer_keywords:
  - "dm_resource_governor_resource_pool_volumes"
  - "sys.dm_resource_governor_resource_pool_volumes"
dev_langs:
  - "TSQL"
ms.assetid: fa692e56-c561-4533-97c5-bc12c600553f
---
# sys.dm_resource_governor_resource_pool_volumes (Transact-SQL)
[!INCLUDE[sqlserver](../../includes/applies-to-version/sqlserver.md)]

  Returns information about the current resource pool IO statistics for each disk volume. This information is also available at the resource pool level in [sys.dm_resource_governor_resource_pools &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-resource-pools-transact-sql.md).  
  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|pool_id|**int**|The ID of the resource pool. Is not nullable.|  
|volume_name|**sysname**|The name of the disk volue. Is not nullable.|  
|read_io_queued_total|**int**|The total read IOs enqueued since the Resource Govenor is reset. Is not nullable.|  
|read_io_issued_total|**int**|The total read IOs issued since the Resource Govenor statistics were reset. Is not nullable.|  
|read_ios_completed_total|**int**|The total read IOs completed since the Resource Govenor statistics were reset. Is not nullable.|  
|read_ios_throttled_total|**int**|The total read IOs throttled since the Resource Govenor statistics were reset. Is not nullable.|  
|read_bytes_total|**bigint**|The total number of bytes read since the Resource Govenor statistics were reset. Is not nullable.|  
|read_io_stall_total_ms|**bigint**|Total time (in milliseconds) between read IO arrival and completion. Is not nullable.|  
|read_io_stall_queued_ms|**bigint**|Total time (in milliseconds) between read IO arrival and issue. This is the delay introduced by IO Resource Governance. Is not nullable.|  
|write_io_queued_total|**int**|The total write IOs enqueued since the Resource Govenor statistics were reset. Is not nullable.|  
|write_io_issued_total|**int**|The total write IOs issued since the Resource Govenor statistics were reset. Is not nullable.|  
|write_io_completed_total|**int**|The total write IOs completed since the Resource Govenor statistics were reset. Is not nullable|  
|write_io_throttled_total|**int**|The total write IOs throttled since the Resource Govenor statistics were reset. Is not nullable|  
|write_bytes_total|**bigint**|The total number of bytes written since the Resource Govenor statistics were reset. Is not nullable.|  
|write_io_stall_total_ms|**bigint**|Total time (in milliseconds) between write IO issue and completion. Is not nullable.|  
|write_io_stall_queued_ms|**bigint**|Total time (in milliseconds) between write IO arrival and issue. This is the delay introduced by IO Resource Governance. Is not nullable.|  
|io_issue_violations_total|**int**|Total IO issue violations. That is, the number of times when the rate of IO issue was lower than the reserved rate. Is not nullable.|  
|io_issue_delay_total_ms|**bigint**|Total time (in milliseconds) between the scheduled issue and actual issue of IO. Is not nullable.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission.  
  
## See also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [sys.dm_resource_governor_workload_groups &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-workload-groups-transact-sql.md)   
 [sys.resource_governor_resource_pools &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-resource-governor-resource-pools-transact-sql.md)   
 [ALTER RESOURCE GOVERNOR &#40;Transact-SQL&#41;](../../t-sql/statements/alter-resource-governor-transact-sql.md)  
  
  

