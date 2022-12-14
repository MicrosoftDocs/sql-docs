---
title: "sys.resource_governor_external_resource_pools (Transact-SQL)"
description: sys.resource_governor_external_resource_pools (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "11/13/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.resource_governor_external_resource_pools"
  - "sys.resource_governor_external_resource_pools_TSQL"
  - "resource_governor_external_resource_pools"
  - "resource_governor_external_resource_pools_TSQL"
helpviewer_keywords:
  - "sys.resource_governor_external_resource_pools"
  - "resource_governor_external_resource_pools"
dev_langs:
  - "TSQL"
ms.assetid: 75063e36-a91b-496f-9936-88f3d57bd447
---
# sys.resource_governor_external_resource_pools (Transact-SQL)
[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]
**Applies to:** [!INCLUDE[sssql15-md](../../includes/sssql16-md.md)] [!INCLUDE[rsql-productname-md](../../includes/rsql-productname-md.md)] and [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)] [!INCLUDE[rsql-productnamenew-md](../../includes/rsql-productnamenew-md.md)]

Returns the stored external resource pool configuration in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Each row of the view determines the configuration of a pool.
  
|Column name|Data type|Description|
|-----------------|---------------|-----------------|
|external_pool_id|**int**|Unique ID of the resource pool. Is not nullable.|
|name|**sysname**|Name of the resource pool. Is not nullable.|
|max_cpu_percent|**int**|Maximum average CPU bandwidth allowed for all requests in the resource pool when there is CPU contention. Is not nullable.|
|max_memory_percent|**int**|Percentage of total server memory that can be used by requests in this resource pool. Is not nullable. The effective maximum depends on the pool minimums. For example, max_memory_percent can be set to 100, but the effective maximum is lower.|
|max_processes|**int**|Maximum number of concurrent external processes. The default value, 0, specifies no limit. Is not nullable.|
|version|**bigint**|Internal version number.|
  
## Permissions

Requires VIEW SERVER STATE permission.

## See also

[Resource governance for machine learning in SQL Server](../../machine-learning/administration/resource-governor.md)

[Resource Governor Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/resource-governor-catalog-views-transact-sql.md)

[sys.dm_resource_governor_resource_pools &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-resource-pools-transact-sql.md)

[Resource Governor](../../relational-databases/resource-governor/resource-governor.md)

[sys.dm_resource_governor_resource_pool_affinity &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-resource-pool-affinity-transact-sql.md)

[external scripts enabled Server Configuration Option](../../database-engine/configure-windows/external-scripts-enabled-server-configuration-option.md)

[ALTER EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/alter-external-resource-pool-transact-sql.md)
