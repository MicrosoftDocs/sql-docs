---
title: "sys.dm_hadr_instance_node_map (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.sys.dm_hadr_instance_node_map_TSQL"
  - "sys.sys.dm_hadr_instance_node_map"
  - "sys.dm_hadr_instance_node_map_TSQL"
  - "sys.dm_hadr_instance_node_map"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], monitoring"
  - "Availability Groups [SQL Server], WSFC"
  - "sys.sys.dm_hadr_instance_node_map dynamic management view"
ms.assetid: ccfaf62c-9f87-43cf-a5e7-8942e91dd041
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# sys.dm_hadr_instance_node_map (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  For every instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that hosts an availability replica that is joined to its Always On availability group, returns the name of the Windows Server Failover Cluster (WSFC) node that hosts the server instance. This dynamic management view has the following uses:  
  
-   This dynamic management view is useful for detecting an availability group with multiple availability replicas that are hosted on the same WSFC node, which is an unsupported configuration that could occur after an FCI failover if the availability group is incorrectly configured. For more information, see [Failover Clustering and Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/failover-clustering-and-always-on-availability-groups-sql-server.md).  
  
-   When multiple SQL Server instances are hosted on the same WSFC node, the Resource DLL uses this dynamic management view to determine the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to connect to.  
   
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**ag_resource_id**|**nvarchar(256)**|Unique ID of the availability group as a resource in the WSFC.|  
|**instance_name**|**nvarchar(256)**|Name-*server*/*instance*-of a server instance that hosts a replica for the availability group.|  
|**node_name**|**nvarchar(256)**|Name of the WSFC node.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## See Also  
 [Always On Availability Groups Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/always-on-availability-groups-dynamic-management-views-functions.md)   
 [Always On Availability Groups Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/always-on-availability-groups-catalog-views-transact-sql.md)   
 [Monitor Availability Groups &#40;Transact-SQL&#41;](../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)   
 [Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md)  
  
  
