---
title: "sys.dm_hadr_availability_replica_cluster_states (Transact-SQL)"
description: sys.dm_hadr_availability_replica_cluster_states (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "02/27/2023"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_hadr_availability_replica_cluster_states_TSQL"
  - "dm_hadr_availability_replica_cluster_states"
  - "sys.dm_hadr_availability_replica_cluster_states"
  - "dm_hadr_availability_replica_cluster_states_TSQL"
helpviewer_keywords:
  - "Availability Groups [SQL Server], monitoring"
  - "Availability Groups [SQL Server], WSFC clusters"
  - "sys.dm_hadr_availability_replica_cluster_states dynamic management view"
dev_langs:
  - "TSQL"
---
# sys.dm_hadr_availability_replica_cluster_states (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a row for each Always On availability replica (regardless of its join state) of all Always On availability groups (regardless of replica location) in the Windows Server Failover Clustering (WSFC) cluster.  
  
##  <a name="connected_state"></a>  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**replica_id**|**uniqueidentifier**|Unique identifier of the availability replica.|  
|**replica_server_name**|**nvarchar(256)**|Name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] hosting the replica.|  
|**group_id**|**uniqueidentifier**|Unique identifier of the availability group.|  
|**join_state**|**tinyint**|0 = Not joined<br /><br /> 1 = Joined, standalone<br /><br /> 2 = Joined, failover cluster instance|  
|**join_state_desc**|**nvarchar(60)**|NOT_JOINED<br /><br /> JOINED_STANDALONE<br /><br /> JOINED_FAILOVER_CLUSTER_INSTANCE|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
### Permissions for SQL Server 2022 and later

Requires VIEW SERVER PERFORMANCE STATE permission on the server.

## See also  
 [Monitor Availability Groups &#40;Transact-SQL&#41;](../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)  
  
  
