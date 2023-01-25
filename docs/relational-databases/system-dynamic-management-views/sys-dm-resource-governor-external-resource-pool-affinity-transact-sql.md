---
title: "sys.dm_resource_governor_external_resource_pool_affinity (Transact-SQL)"
description: sys.dm_resource_governor_external_resource_pool_affinity (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "11/13/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_resource_governor_external_resource_pool_affinity"
  - "sys.dm_resource_governor_external_resource_pool_affinity_TSQL"
  - "dm_resource_governor_external_resource_pool_affinity"
  - "dm_resource_governor_external_resource_pool_affinity_TSQL"
helpviewer_keywords:
  - "sys.dm_resource_governor_external_resource_pool_affinity"
  - "dm_resource_governor_external_resource_pool_affinity"
dev_langs:
  - "TSQL"
ms.assetid: e32fac49-5161-47c0-8540-af3fe730c00c
---
# sys.dm_resource_governor_external_resource_pool_affinity (Transact-SQL)
[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]
**Applies to:** [!INCLUDE[sssql15-md](../../includes/sssql16-md.md)] [!INCLUDE[rsql-productname-md](../../includes/rsql-productname-md.md)] and [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)] [!INCLUDE[rsql-productnamenew-md](../../includes/rsql-productnamenew-md.md)]

Returns CPU affinity information about the current external resource pool configuration.
  
|Column name|Data type|Description|
|----------------|---------------|-----------------|
|pool_id|**int**|The ID of the external resource pool. Is not nullable.|
|processor_group|**smallint**|The ID of the Windows logical processor group. Is not nullable.|
|cpu_mask|**bigint**|The binary mask representing the CPUs associated with this pool. Is not nullable.|
  
## Remarks

Pools that are created with an affinity of `AUTO` do not appear in this view because they have no affinity. For more information, see the [CREATE EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-resource-pool-transact-sql.md) and [ALTER EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/alter-external-resource-pool-transact-sql.md) statements.

## Permissions

Requires `VIEW SERVER STATE` permission.

## See also

[Resource governance for machine learning in SQL Server](../../machine-learning/administration/resource-governor.md)

[sys.dm_resource_governor_resource_pool_affinity &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-resource-pool-affinity-transact-sql.md)

[external scripts enabled Server Configuration Option](../../database-engine/configure-windows/external-scripts-enabled-server-configuration-option.md)

[ALTER EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/alter-external-resource-pool-transact-sql.md)
