---
title: "sys.dm_hadr_availability_replica_cluster_nodes (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_hadr_availability_replica_cluster_nodes"
  - "sys.dm_hadr_availability_replica_cluster_nodes_TSQL"
  - "dm_hadr_availability_replica_cluster_nodes_TSQL"
  - "sys.dm_hadr_availability_replica_cluster_nodes"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], monitoring"
  - "Availability Groups [SQL Server], WSFC clusters"
  - "sys.dm_hadr_availability_replica_cluster_nodes dynamic management view"
ms.assetid: dbd7e416-badd-4332-a45c-438aa0145a99
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# sys.dm_hadr_availability_replica_cluster_nodes (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Returns a row for every availability replica (regardless of join state) of the Always On availability groups in the Windows Server Failover Clustering (WSFC) cluster.  

 ##  <a name="connected_state"></a>  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**group_name**|**nvarchar(256)**|Name of the availability group.|  
|**replica_server_name**|**nvarchar(256)**|Name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] hosting the replica.|  
|**node_name**|**nvarchar(256)**|Name of the cluster node.|  
  
## Security  
  
### Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## See Also  
 [Monitor Availability Groups &#40;Transact-SQL&#41;](../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)   
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)  
  
  
