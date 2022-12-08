---
title: "sys.resource_governor_resource_pools (Transact-SQL)"
description: sys.resource_governor_resource_pools (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "08/09/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.resource_governor_resource_pools"
  - "resource_governor_resource_pools"
  - "sys.resource_governor_resource_pools_TSQL"
  - "resource_governor_resource_pools_TSQL"
helpviewer_keywords:
  - "sys.resource_governor_resource_pools catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 56793e9c-aa90-452e-88c6-d9b799239888
---
# sys.resource_governor_resource_pools (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns the stored resource pool configuration in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Each row of the view determines the configuration of a pool.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|pool_id|**int**|Unique ID of the resource pool. Is not nullable.|  
|name|**sysname**|Name of the resource pool. Is not nullable.|  
|min_cpu_percent|**int**|Guaranteed average CPU bandwidth for all requests in the resource pool when there is CPU contention. Is not nullable.|  
|max_cpu_percent|**int**|Maximum average CPU bandwidth allowed for all requests in the resource pool when there is CPU contention. Is not nullable.|  
|min_memory_percent|**int**|Guaranteed amount of memory for all requests in the resource pool. This is not shared with other resource pools. Is not nullable.|  
|max_memory_percent|**int**|Percentage of total server memory that can be used by requests in this resource pool. Is not nullable. The effective maximum depends on the pool minimums. For example, max_memory_percent can be set to 100, but the effective maximum is lower.|  
|cap_cpu_percent|**int**|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.<br /><br /> Hard cap on the CPU bandwidth that all requests in the resource pool will receive. Limits the maximum CPU bandwidth to the specified level. The allowed range for value is from 1 through 100.|  
|min_iops_per_volume|**int**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later.<br /><br /> The minimum I/O operations per second (IOPS) per volume setting for this pool. 0 = no reservation. Cannot be null.|  
|max_iops_per_volume|**int**|**Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later.<br /><br /> The maximum I/O operations per second (IOPS) per volume setting for this pool. 0 = unlimited. Cannot be null.|  
  
## Remarks  
 The catalog view displays the stored metadata. To see the in-memory configuration, use the corresponding dynamic management view, [sys.dm_resource_governor_resource_pools &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-resource-pools-transact-sql.md).  
  
## Permissions  
 Requires VIEW ANY DEFINITION permission to view contents, requires CONTROL SERVER permission to change contents.  
  
## See Also  
 [Resource Governor Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/resource-governor-catalog-views-transact-sql.md)   
 [sys.dm_resource_governor_resource_pools &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-resource-pools-transact-sql.md)   
 [Resource Governor](../../relational-databases/resource-governor/resource-governor.md)   
 [sys.resource_governor_external_resource_pools &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-resource-governor-external-resource-pools-transact-sql.md)  
  
  
