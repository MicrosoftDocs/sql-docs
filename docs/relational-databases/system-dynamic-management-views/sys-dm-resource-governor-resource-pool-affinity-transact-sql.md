---
title: "sys.dm_resource_governor_resource_pool_affinity (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_resource_governor_resource_pool_affinity_TSQL"
  - "sys.dm_resource_governor_resource_pool_affinity"
  - "dm_resource_governor_resource_pool_affinity"
  - "dm_resource_governor_resource_pool_affinity_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "dm_resource_governor_resource_pool_affinity"
  - "sys.dm_resource_governor_resource_pool_affinity"
ms.assetid: a197ec19-a2ba-44f5-a4f2-3eee33ebd77d
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_resource_governor_resource_pool_affinity (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Tracks resource pool affinity.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md).  
  
|Colmn name|Data type|Description|  
|----------------|---------------|-----------------|  
|Pool_id|**int**|The ID of the resource pool. Is not nullable.|  
|Processor_group|**smallint**|The ID of the Windows logical processor group. Is not nullable.|  
|Scheduler_mask|**bigint**|The binary mask representing the schedulers associated with this pool. Is not nullable.|  
  
## Remarks  
 Pools that are created with an affinity of AUTO will not appear in this view because they have no affinity. For more information, see the [CREATE RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/create-resource-pool-transact-sql.md) and [ALTER RESOURCE POOL &#40;Transact-SQL&#41;](../../t-sql/statements/alter-resource-pool-transact-sql.md) statements.  
  
## See Also  
 [sys.dm_resource_governor_external_resource_pool_affinity &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-external-resource-pool-affinity-transact-sql.md)  
  
  
