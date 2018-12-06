---
title: "sys.availability_databases_cluster (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.availability_databases_cluster_TSQL"
  - "sys.availability_databases_cluster"
  - "availability_databases_cluster_TSQL"
  - "availability_databases_cluster"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], monitoring"
  - "Availability Groups [SQL Server], WSFC clusters"
  - "sys.availability_databases_cluster catalog view"
  - "Availability Groups [SQL Server], databases"
ms.assetid: 8d9c57e5-7f39-4315-b466-92748231140a
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# sys.availability_databases_cluster (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Contains one row for each availability database on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is hosting an availability replica for any Always On availability group in the Windows Server Failover Clustering (WSFC) cluster, regardless of whether the local copy database has been joined to the availability group yet.  
  
> [!NOTE]  
>  When a database is added to an availability group, the primary database is automatically joined to the group. Secondary databases must be prepared on each secondary replica before they can be joined to the availability group.   
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**group_id**|**uniqueidentifier**|Unique identifier of the availability group in which the availability group, if any, in which the database is participating.<br /><br /> NULL = database is not part of an availability replica of in availability group.|  
|**group_database_id**|**uniqueidentifier**|Unique identifier of the database within the availability group, if any, in which the database is participating. **group_database_id** is the same for this database on the primary replica and on every secondary replica on which the database has been joined to the availability group.<br /><br /> NULL = database is not part of an availability replica in any availability group.|  
|**database_name**|**sysname**|Name of the database that was added to the availability group.|  
  
## Permissions  
 If the caller of **sys.availability_databases_cluster** is not the owner of the database, the minimum permissions required to see the corresponding row are ALTER ANY DATABASE or VIEW ANY DATABASE server-level permission, or CREATE DATABASE permission in the **master** database.  
  
## See Also  
 [sys.availability_groups &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-availability-groups-transact-sql.md)   
 [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)   
 [sys.dm_hadr_database_replica_states &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-hadr-database-replica-states-transact-sql.md)   
 [sys.dm_hadr_database_replica_cluster_states &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-hadr-database-replica-cluster-states-transact-sql.md)   
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)  
  
  
