---
title: "sys.dm_resource_governor_external_resource_pool_affinity (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/29/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_resource_governor_external_resource_pool_affinity"
  - "sys.dm_resource_governor_external_resource_pool_affinity_TSQL"
  - "dm_resource_governor_external_resource_pool_affinity"
  - "dm_resource_governor_external_resource_pool_affinity_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_resource_governor_external_resource_pool_affinity"
  - "dm_resource_governor_external_resource_pool_affinity"
ms.assetid: e32fac49-5161-47c0-8540-af3fe730c00c
caps.latest.revision: 8
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# sys.dm_resource_governor_external_resource_pool_affinity (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  Returns CPU affinity information about the current external resource pool configuration.  
  
|Colmn name|Data type|Description|  
|----------------|---------------|-----------------|  
|pool_id|**int**|The ID of the external resource pool. Is not nullable.|  
|processor_group|**smallint**|The ID of the Windows logical processor group. Is not nullable.|  
|cpu_mask|**bigint**|The binary mask representing the CPUs associated with this pool. Is not nullable.|  
  
## Remarks  
 Pools that are created with an affinity of `AUTO` do not appear in this view because they have no affinity. For more information, see the [CREATE EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-resource-pool-transact-sql.md) and [ALTER EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/alter-external-resource-pool-transact-sql.md) statements.  
  
## Permissions  
 Requires `VIEW SERVER STATE` permission.  
  
## See Also  
 [sys.dm_resource_governor_resource_pool_affinity &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-resource-pool-affinity-transact-sql.md)   
 [external scripts enabled Server Configuration Option](../../database-engine/configure-windows/external-scripts-enabled-server-configuration-option.md)   
 [SQL Server R Services](../../advanced-analytics/r-services/sql-server-r-services.md)   
 [Known Issues for SQL Server R Services](../../advanced-analytics/r-services/known-issues-for-sql-server-r-services.md)   
 [ALTER EXTERNAL RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/alter-external-resource-pool-transact-sql.md)  
  
  
