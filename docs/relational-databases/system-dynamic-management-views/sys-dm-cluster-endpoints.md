---
title: "sys.dm_cluster_endpoints (Transact-SQL)"
description: sys.dm_cluster_endpoints (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/04/2019
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_cluster_endpoints"
  - "dm_cluster_endpoints_TSQL"
  - "dm_cluster_endpoints"
helpviewer_keywords:
  - "sys.dm_cluster_endpoints dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver15||>=sql-server-linux-2017"
---
# sys.dm_cluster_endpoints (Transact-SQL)
[!INCLUDE[sqlserver2019](../../includes/applies-to-version/sqlserver2019.md)]

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|name|`sysname`|Name of the service exposed externally in a SQL big data cluster. Unique identifier for the endpoint. Key for this view. Is not nullable. |  
|description|`nvarchar(4000)`|Description of the service. Is not nullable. |
|endpoint|`sysname`|Endpoint url or connection attribute. Is not nullable. |
|protocol_desc|`sysname`|Description of the endpoint protocol |

## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)], requires `VIEW SERVER STATE` permission.

## See Also

[What are [!INCLUDE[big-data-clusters-2019](../../includes/ssbigdataclusters-ss-nover.md)]](../../big-data-cluster/big-data-cluster-overview.md)?
