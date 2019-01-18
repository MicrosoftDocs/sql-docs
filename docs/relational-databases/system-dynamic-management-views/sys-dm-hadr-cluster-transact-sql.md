---
title: "sys.dm_hadr_cluster (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_hadr_cluster"
  - "dm_hadr_cluster_HADR"
  - "sys.dm_hadr_cluster_TSQL"
  - "dm_hadr_cluster"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], monitoring"
  - "sys.dm_hadr_cluster catalog view"
  - "Availability Groups [SQL Server], WSFC clusters"
ms.assetid: 13ce70e4-9d43-4a80-a826-099e6213bf85
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# sys.dm_hadr_cluster (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  If the Windows Server Failover Clustering (WSFC) node that hosts an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is enabled for [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] has WSFC quorum, **sys.dm_hadr_cluster** returns a row that exposes the cluster name and information about the quorum. If the WSFC node has no quorum, no row is returned.  
 > [!TIP]
 > Beginning in [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], this dynamic management view supports Always On Failover Cluster Instances in addition to Always On Availability Groups.

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**cluster_name**|**nvarchar(128)**|Name of the WSFC cluster that hosts the instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that are enabled for [!INCLUDE[ssHADR](../../includes/sshadr-md.md)].|  
|**quorum_type**|**tinyint**|Type of quorum used by this WSFC cluster, one of:<br /><br /> 0 = Node Majority. This quorum configuration can sustain failures of half the nodes (rounding up) minus one. For example, on a seven node cluster, this quorum configuration can sustain three node failures.<br /><br /> 1 = Node and Disk Majority. If the disk witness remains on line, this quorum configuration can sustain failures of half the nodes (rounding up). For example, a six node cluster in which the disk witness is online could sustain three node failures. If the disk witness goes offline or fails, this quorum configuration can sustain failures of half the nodes (rounding up) minus one. For example, a six node cluster with a failed disk witness could sustain two (3-1=2) node failures.<br /><br /> 2 = Node and File Share Majority. This quorum configuration works in a similar way to Node and Disk Majority, but uses a file-share witness instead of a disk witness.<br /><br /> 3 = No Majority: Disk Only. If the quorum disk is online, this quorum configuration can sustain failures of all nodes except one.|  
|**quorum_type_desc**|**varchar(50)**|Description of **quorum_type**, one of:<br /><br /> NODE_MAJORITY<br /><br /> NODE_AND_DISK_MAJORITY<br /><br /> NODE_AND_FILE_SHARE_MAJORITY<br /><br /> NO_MAJORITY:_DISK_ONLY|  
|**quorum_state**|**tinyint**|State of the WSFC quorum, one of:<br /><br /> 0 = Unknown quorum state<br /><br /> 1 = Normal quorum<br /><br /> 2 = Forced quorum|  
|**quorum_state_desc**|**varchar(50)**|Description of **quorum_state**, one of:<br /><br /> UNKNOWN_QUORUM_STATE<br /><br /> NORMAL_QUORUM<br /><br /> FORCED_QUORUM|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## See Also  
 [Always On Availability Groups Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/always-on-availability-groups-dynamic-management-views-functions.md)   
 [Always On Availability Groups Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/always-on-availability-groups-catalog-views-transact-sql.md)   
 [Monitor Availability Groups &#40;Transact-SQL&#41;](../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)   
 [sys.dm_hadr_cluster_members &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-hadr-cluster-members-transact-sql.md)  
  
  
