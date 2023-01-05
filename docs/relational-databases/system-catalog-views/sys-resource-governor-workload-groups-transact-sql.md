---
title: "sys.resource_governor_workload_groups (Transact-SQL)"
description: sys.resource_governor_workload_groups (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/16/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.resource_governor_workload_groups"
  - "resource_governor_workload_groups_TSQL"
  - "sys.resource_governor_workload_groups_TSQL"
  - "resource_governor_workload_groups"
helpviewer_keywords:
  - "sys.resource_governor_workload_groups catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 619ba4b7-868f-4784-b527-ec1dfd703c4f
---
# sys.resource_governor_workload_groups (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns the stored workload group configuration in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Each workload group can subscribe to one and only one resource pool.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|group_id|**int**|Unique ID of the workload group. Is not nullable.|  
|name|**sysname**|Name of the workload group. Is not nullable.|  
|importance|**sysname**|**Note:** Importance only applies to workload groups in the same resource pool.<br /><br /> Is the relative importance of a request in this workload group. Importance is one of the following, with MEDIUM being the default: LOW, MEDIUM, HIGH.<br /><br /> Is not nullable.|  
|request_max_memory_grant_percent|**int**|Maximum memory grant, as a percentage, for a single request. The default value is 25. Is not nullable.<br /><br /> **Note:** If this setting is higher than 50 percent, large queries will run one at a time. Therefore, there is greater risk of getting an out-of-memory error while the query is running.|  
|request_max_cpu_time_sec|**int**|Maximum CPU use limit, in seconds, for a single request. The default value, 0, specifies no limit. Is not nullable.<br /><br /> **Note:** For more information, see [CPU Threshold Exceeded Event Class](../../relational-databases/event-classes/cpu-threshold-exceeded-event-class.md).|  
|request_memory_grant_timeout_sec|**int**|Memory grant time-out, in seconds, for a single request. The default value, 0, uses an internal calculation based on query cost. Is not nullable.|  
|max_dop|**int**|Maximum degree of parallelism for the workload group. The default value, 0, uses global settings. Is not nullable.<br /><br /> **Note:** This setting will override the query option **maxdop**.|  
|group_max_requests|**int**|Maximum number of concurrent requests. The default value, 0, specifies no limit. Is not nullable.|  
|pool_id|**int**|ID of the resource pool that this workload group uses.|  
|external_pool_id|**int**|**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later.<br /><br /> ID of the external resource pool that this workload group uses.|  
|request_max_memory_grant_percent_numeric|**float**|**Applies to**: [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and later.<br /><br /> Maximum memory grant, as a percentage, for a single request. The default value is 25. Is not nullable.<br /><br /> **Note:** If this setting is higher than 50 percent, large queries will run one at a time. Therefore, there is greater risk of getting an out-of-memory error while the query is running.|  
  
## Remarks  
 The catalog view displays the stored metadata. To see the in-memory configuration, use the corresponding dynamic management view, [sys.dm_resource_governor_workload_groups &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-workload-groups-transact-sql.md).  
  
 The stored and in-memory configuration can be different if the Resource Governor configuration has been changed but the ALTER RESOURCE GOVERNOR RECONFIGURE statement has not been applied.  
  
## Permissions  
 Requires VIEW ANY DEFINITION permission to view contents, requires CONTROL SERVER permission to change contents.  
  
## See Also  
 [sys.dm_resource_governor_workload_groups &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-workload-groups-transact-sql.md)   
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Resource Governor Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/resource-governor-catalog-views-transact-sql.md)  
  
  
