---
description: "sys.dm_exec_compute_pools (Transact-SQL)"
title: "sys.dm_exec_compute_pools (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: 11/04/2019
ms.prod: sql
ms.prod_service: "database-engine, big-data-clusters"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_exec_compute_pools"
  - "dm_exec_compute_pools_TSQL"
  - "dm_exec_compute_pools"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_exec_compute_pools dynamic management view"
ms.assetid: 
author: CarlRabeler
ms.author: carlrab
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions||>=sql-server-linux-2017"
---
# sys.dm_exec_compute_pools (Transact-SQL)
[!INCLUDE[sqlserver2019](../../includes/applies-to-version/sqlserver2019.md)]

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|name|`sysname`|Name of the compute pool. Is not nullable. Returns `default` for the default compute pool. |
|compute_pool_id|`int`|Unique identifier for the pool. Key for this view.|  
|location|`sysname`|Endpoint to controller in a SQL Big Data cluster. Is not nullable. |

## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.

## See Also

[What are [!INCLUDE[big-data-clusters-2019](../../includes/ssbigdataclusters-ss-nover.md)]](../../big-data-cluster/big-data-cluster-overview.md)?
