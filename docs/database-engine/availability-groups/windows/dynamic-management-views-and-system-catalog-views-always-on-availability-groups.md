---
title: "DMVs and system catalog views for availability groups"
description: "A collection of dynamic management and catalog views that can help you monitor and diagnose the health of an Always On availability group."
author: MashaMSFT
ms.author: mathoma
ms.date: "06/13/2017"
ms.service: sql
ms.subservice: availability-groups
ms.topic: troubleshooting
ms.custom: seo-lt-2019
---
# Dynamic management views and system catalog views (Always On Availability Groups)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  This topic shows you some of the common queries on the Always On dynamic management views (DMV) that you can use to monitor and troubleshoot availability groups.  
  
> [!TIP]  
>  In the Always On Dashboard, you can easily configure the GUI to display many of the DMVs for the availability replicas and availability databases by right-clicking the respective table header and selecting the DMV you wish to display or hide.  
  
 For more information on the availability group DMVs, see [Always On Availability Groups dynamic management views and functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/always-on-availability-groups-dynamic-management-views-functions.md). For more information on the availability groups catalog views, see [Always On Availability Groups catalog views &#40;Transact-SQL&#41;](~/relational-databases/system-catalog-views/always-on-availability-groups-catalog-views-transact-sql.md).  
  
## Check the WSFC cluster node configuration  
 The following Transact-SQL (T-SQL) query retrieves the status of all the nodes in the current Windows Server Failover Clustering (WSFC) cluster.  
  
```sql  
use master  
go  
select * from sys.dm_hadr_cluster_members  
go  
```  
  
 This result set reports the status of each member node of the current WSFC cluster. If the quorum is defined as **Node and File Share Majority**, even the file share is reported. You can see the status of each node, including the voting weight of each node (the [number_of_quorum_votes](~/relational-databases/system-dynamic-management-views/sys-dm-hadr-cluster-members-transact-sql.md) value).  
  
## Explore the cluster network  
 The following query retrieves the network configuration of the current WSFC cluster.  
  
```sql  
select * from sys.dm_hadr_cluster_networks  
```  
  
 The result set contains one row for each network adapter in the WSFC cluster. For example, in a two-node cluster that contains two network adapters on each node, this query returns four rows.  
  
## Explore the availability groups  
 The following query retrieves information about an availability group.  
  
```sql  
select primary_replica, primary_recovery_health_desc, synchronization_health_desc from sys.dm_hadr_availability_group_states  
go  
select * from sys.availability_groups  
go  
select * from sys.availability_groups_cluster  
go  
```  
  
 The DMVs [sys.dm_hadr_availability_group_states &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-group-states-transact-sql.md), [sys.availability_groups &#40;Transact-SQL&#41;](~/relational-databases/system-catalog-views/sys-availability-groups-transact-sql.md), and [sys.availability_groups_cluster](~/relational-databases/system-catalog-views/sys-availability-groups-cluster-transact-sql.md) all return information about the availability groups in the current WSFC cluster. In fact, [sys.availability_groups &#40;Transact-SQL&#41;](~/relational-databases/system-catalog-views/sys-availability-groups-transact-sql.md), and [sys.availability_groups_cluster](~/relational-databases/system-catalog-views/sys-availability-groups-cluster-transact-sql.md) seem to return identical information.  
  
 However, [sys.availability_groups_cluster](~/relational-databases/system-catalog-views/sys-availability-groups-cluster-transact-sql.md) reports availability group metadata stored in the WSFC Cluster, whereas [sys.availability_groups &#40;Transact-SQL&#41;](~/relational-databases/system-catalog-views/sys-availability-groups-transact-sql.md) reports availability group metadata that is cached in the SQL Server process space. Furthermore, these two DMVs report configuration information whereas [sys.dm_hadr_availability_group_states &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-group-states-transact-sql.md) reports the current health statuses of the availability groups.  
  
> [!IMPORTANT]  
>  This nomenclature carries forward with the DMVs that document availability replicas and availability databases.  
  
## Explore the availability replicas  
 The following query retrieves information about the availability replicas defined in your availability groups.  
  
```sql  
select replica_id, role_desc, connected_state_desc, synchronization_health_desc from sys.dm_hadr_availability_replica_states  
go  
select replica_server_name, replica_id, availability_mode_desc, endpoint_url from sys.availability_replicas  
go  
select replica_server_name, join_state_desc from sys.dm_hadr_availability_replica_cluster_states  
go  
```  
  
 Similar to the availability group DMVs, you find three DMVs that report on availability replicas. [sys.dm_hadr_availability_replica_states](~/relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-replica-states-transact-sql.md) reports state information about the availability replicas that is locally cached in SQL Server, and [sys.dm_hadr_availability_replica_cluster_states](~/relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-replica-cluster-states-transact-sql.md) reports state information about the availability replicas from the WSFC cluster. Finally, [sys.availability_replicas](~/relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-replica-cluster-states-transact-sql.md) reports configuration data on the availability replicas, which are cached locally in SQL Server.  
  
## Explore availability replica health  
 The following query retrieves current health information about the availability replicas.  
  
```sql  
select replica_id, role_desc, recovery_health_desc, synchronization_health_desc from sys.dm_hadr_availability_replica_states  
go  
```  
  
 Compare the query results on the primary replica and on the secondary replica and note that on the secondary replica, health information is reported only for that replica and not for any other replica in the availability group.  
  
## Explore the availability databases  
 The following query retrieves information about the availability replicas defined in your availability group. You can observe the change in the query results before and after you suspend data movement on an availability database.  
  
```sql
select * from sys.availability_databases_cluster  
go  
select group_database_id, database_name, is_failover_ready  from sys.dm_hadr_database_replica_cluster_states  
go  
select database_id, synchronization_state_desc, synchronization_health_desc, last_hardened_lsn, redo_queue_size, log_send_queue_size from sys.dm_hadr_database_replica_states  
go  
```  
  
 Here again, three Always On DMVs report on availability databases. [sys.availability_databases_cluster](~/relational-databases/system-catalog-views/sys-availability-databases-cluster-transact-sql.md) reports configuration information about availability databases from the WSFC cluster. [sys.dm_hadr_database_replica_cluster_states](~/relational-databases/system-dynamic-management-views/sys-dm-hadr-database-replica-cluster-states-transact-sql.md) reports state information about the database replicas, which are locally cached in SQL Server. It contains some important state information, such as the availability replica's failover readiness. Finally, [sys.dm_hadr_database_replica_states](~/relational-databases/system-dynamic-management-views/sys-dm-hadr-database-replica-states-transact-sql.md) is a very verbose result set which reports identity and state information on each availability database, such as LSN progress information for the logs of the primary and secondary database replicas.  
  
## Explore availability database health  
 The following query retrieves information about the health of each availability databases on the replicas. You can observe the change in the query results before and after you suspend data movement on an availability database.  
  
```sql  
select dc.database_name, dr.database_id, dr.synchronization_state_desc,   
dr.suspend_reason_desc, dr.synchronization_health_desc  
from sys.dm_hadr_database_replica_states dr  join sys.availability_databases_cluster dc  
on dr.group_database_id=dc.group_database_id   
where is_local=1  
go  
```  
  
  
