---
title: "sys.dm_hadr_database_replica_cluster_states (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.dm_hadr_database_replica_cluster_states"
  - "dm_hadr_database_replica_cluster_states_TSQL"
  - "sys.dm_hadr_database_replica_cluster_states_TSQL"
  - "dm_hadr_database_replica_cluster_states"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], monitoring"
  - "Availability Groups [SQL Server], WSFC clusters"
  - "sys.dm_hadr_database_replica_cluster_states dynamic management view"
ms.assetid: 6f719071-ebce-470d-aebd-1f55ee8cd70a
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# sys.dm_hadr_database_replica_cluster_states (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Returns a row containing information intended to provide you with insight into the health of the availability databases in the Always On availability groups in each Always On availability group on the Windows Server Failover Clustering (WSFC) cluster. Query **sys.dm_hadr_database_replica_states** to answer the following questions:  
  
-   Are all databases in an availability group ready for a failover?  
  
-   After a forced failover, has a secondary database suspended itself locally and acknowledged its suspended state to the new primary replica?  
  
-   If the primary replica is currently unavailable, which secondary replica would allow the minimum data loss if it becomes the primary replica?  
  
-   When the value of the [sys.databases](~/relational-databases/system-catalog-views/sys-databases-transact-sql.md)   **log_reuse_wait_desc** column is "AVAILABILITY_REPLICA", which secondary replica in an availability group is holding up log truncation on a given primary database?  
   
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**replica_id**|**uniqueidentifier**|Identifier of the availability replica within the availability group.|  
|**group_database_id**|**uniqueidentifier**|Identifier of the database within the availability group. This identifier is identical on every replica to which this database is joined.|  
|**database_name**|**sysname**|Name of a database that belongs to the availability group.|  
|**is_failover_ready**|**bit**|Indicates whether the secondary database is synchronized with the corresponding primary database. one of:<br /><br /> 0 = The database is not marked as synchronized in the cluster. The database is not ready for a failover.<br /><br /> 1 = The database is marked as synchronized in the cluster. The database is ready for a failover.|  
|**is_pending_secondary_suspend**|**bit**|Indicates whether, after a forced failover, the database is pending suspension, one of:<br /><br /> 0 = Any states except for HADR_SYNCHRONIZED_ SUSPENDED.<br /><br /> 1 = HADR_SYNCHRONIZED_ SUSPENDED. When a forced failover completes, each of the secondary databases is set to HADR_SYNCHONIZED_SUSPENDED and remains in this state until the new primary replica receives an acknowledgement from that secondary database to the SUSPEND message.<br /><br /> NULL = Unknown (no quorum)|  
|**is_database_joined**|**bit**|Indicates whether the database on this availability replica has been joined to the availability group, one of:<br /><br /> 0 = Database is not joined to the availability group on this availability replica.<br /><br /> 1 = Database is joined to the availability group on this availability replica.<br /><br /> NULL = unknown (The availability replica lacks quorum.)|  
|**recovery_lsn**|**numeric(25,0)**|On the primary replica, the end of the transaction log before the replica writes any new log records after recovery or failover. On the primary replica, the row for a given secondary database will have the value to which the primary replica needs the secondary replica to synchronize to (that is, to revert to and reinitialize to).<br /><br /> On secondary replicas this value is NULL. Note that each secondary replica will have either the MAX value or a lower value that the primary replica has told the secondary replica to go back to.|  
|**truncation_lsn**|**numeric(25,0)**|The [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] log truncation value, which may be higher than the local truncation LSN if local log truncation is blocked (such as by a backup operation).|  
  
## Security  
  
### Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## See Also  
 [Always On Availability Groups Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/always-on-availability-groups-dynamic-management-views-functions.md)   
 [Always On Availability Groups Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/always-on-availability-groups-catalog-views-transact-sql.md)   
 [Monitor Availability Groups &#40;Transact-SQL&#41;](../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)   
 [Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md)   
 [sys.dm_hadr_database_replica_states &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-hadr-database-replica-states-transact-sql.md)  
  
  
