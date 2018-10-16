---
title: "sys.dm_hadr_availability_replica_cluster_states (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_hadr_availability_replica_cluster_states_TSQL"
  - "dm_hadr_availability_replica_cluster_states"
  - "sys.dm_hadr_availability_replica_cluster_states"
  - "dm_hadr_availability_replica_cluster_states_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], monitoring"
  - "Availability Groups [SQL Server], WSFC clusters"
  - "sys.dm_hadr_availability_replica_cluster_states dynamic management view"
ms.assetid: 2e0dd780-6a71-4f4b-b7f7-6e063bec71d6
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# sys.dm_hadr_availability_replica_cluster_states (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Returns a row for each Always On availability replica (regardless of its join state) of all Always On availability groups (regardless of replica location) in the Windows Server Failover Clustering (WSFC) cluster.  
  
##  <a name="connected_state"></a>  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**replica_id**|**uniqueidentifier**|Unique identifier of the availability replica.|  
|**replica_server_name**|**nvarchar(256)**|Name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] hosting the replica.|  
|**group_id**|**uniqueidentifier**|Unique identifier of the availability group.|  
|**join_state**|**tinyint**|0 = Not joined<br /><br /> 1 = Joined, standalone instance<br /><br /> 2 = Joined, failover cluster instance|  
|**join_state_desc**|**nvarchar(60)**|NOT_JOINED<br /><br /> JOINED_STANDALONE_INSTANCE<br /><br /> JOINED_FAILOVER_CLUSTER_INSTANCE|  
  
## Security  
  
### Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## See Also  
 [Monitor Availability Groups &#40;Transact-SQL&#41;](../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)  
  
  
