---
title: "sys.dm_exec_compute_pools (Transact-SQL)"
description: sys.dm_exec_compute_pools (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/04/2019
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_exec_compute_pools"
  - "dm_exec_compute_pools_TSQL"
  - "dm_exec_compute_pools"
helpviewer_keywords:
  - "sys.dm_exec_compute_pools dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver15||>=sql-server-linux-2017"
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
