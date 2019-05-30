---
title: "Always On availability group system object reference | Microsoft Docs"
ms.custom: ""
ms.date: "04/03/2010"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=sql-server-2014||=sqlallproducts-allversions"
---
# Always On availability group system object reference

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]
    
This topic serves as a reference page to all the various system objects that can be used when working with Always On availability groups. 

## System catalog views

| System catalog view | Description|
| :------ | :----------------------------- |
| [sys.availability_databases_cluster](../../../relational-databases/system-catalog-views/sys-availability-databases-cluster-transact-sql.md)   | Contains one row for each availability database on the instance of SQL Server that is hosting an availability replica for any Always On availability group in the Windows Server Failover Clustering (WSFC) cluster, regardless of whether the local copy database has been joined to the availability group yet. |
| [sys.availability_group_listener_ip_addresses](../../../relational-databases/system-catalog-views/sys-availability-group-listener-ip-addresses-transact-sql.md)  | Returns a row for every IP address that is associated with any Always On availability group listener in the Windows Server Failover Clustering (WSFC) cluster. |
| [sys.availability_group_listeners](../../../relational-databases/system-catalog-views/sys-availability-group-listeners-transact-sql.md)    | For each Always On availability group, returns either zero rows indicating that no network name is associated with the availability group, or returns a row for each availability-group listener configuration in the Windows Server Failover Clustering (WSFC) cluster.  |
| [sys.availability_groups](../../../relational-databases/system-catalog-views/sys-availability-groups-transact-sql.md)   | Returns a row for each availability group for which the local instance of SQL Server hosts an availability replica. Each row contains a cached copy of the availability group metadata. |
| [sys.availability_groups_cluster](../../../relational-databases/system-catalog-views/sys-availability-groups-cluster-transact-sql.md)    | Returns a row for each Always On availability group in the Windows Server Failover Clustering (WSFC) . Each row contains the availability group metadata from the WSFC cluster. |
| [sys.availability_read_only_routing_lists](../../../relational-databases/system-catalog-views/sys-availability-read-only-routing-lists-transact-sql.md)    | Returns a row for the read only routing list of each availability replica in an Always On availability group in the WSFC failover cluster. |
| [sys.availability_replicas](../../../relational-databases/system-catalog-views/sys-availability-replicas-transact-sql.md)    | Returns a row for each of the availability replicas that belong to any Always On availability group in the WSFC failover cluster. |
| &nbsp; | &nbsp; |

## System dynamic management views


| System dynamic management view | Description|
| :------ | :----------------------------- |
| [sys.dm_hadr_auto_page_repair](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-auto-page-repair-transact-sql.md)   | Returns a row for every automatic page-repair attempt on any availability database on an availability replica that is hosted for any availability group by the server instance.  |
| [sys.dm_hadr_availability_group_states](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-group-states-transact-sql.md)    | Returns a row for each Always On availability group that possesses an availability replica on the local instance of SQL Server. Each row displays the states that define the health of a given availability group. |
| [sys.dm_hadr_availability_replica_cluster_nodes](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-replica-cluster-nodes-transact-sql.md)     | Returns a row for every availability replica (regardless of join state) of the Always On availability groups in the Windows Server Failover Clustering (WSFC) cluster |
| [sys.dm_hadr_availability_replica_cluster_states](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-replica-cluster-states-transact-sql.md)     | Returns a row for each Always On availability replica (regardless of its join state) of all Always On availability groups (regardless of replica location) in the Windows Server Failover Clustering (WSFC) cluster. |
| [sys.dm_hadr_availability_replica_states](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-replica-states-transact-sql.md)    | Returns a row for each local replica and a row for each remote replica in the same Always On availability group as a local replica. Each row contains information about the state of a given replica. |
| [sys.dm_hadr_cluster](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-cluster-transact-sql.md)    | Returns a row that exposes the cluster name and information about the quorum |
| [sys.dm_hadr_cluster_members](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-cluster-members-transact-sql.md)    | Returns a row for each of the members that constitute the quorum and the state of each of them |
| [sys.dm_hadr_cluster_networks](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-cluster-networks-transact-sql.md)    | Returns a row for every WSFC cluster member that is participating in an availability group's subnet configuration.  |
| [sys.dm_hadr_database_replica_cluster_states](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-database-replica-cluster-states-transact-sql.md)     | Returns a row containing information intended to provide you with insight into the health of the availability databases in the Always On availability groups in each Always On availability group on the Windows Server Failover Clustering (WSFC) cluster.  |
| [sys.dm_hadr_database_replica_states](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-database-replica-states-transact-sql.md)    | Returns a row for each database that is participating in an Always On availability group for which the local instance of SQL Server is hosting an availability replica. |
| [sys.dm_hadr_instance_node_map](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-instance-node-map-transact-sql.md)    | For every instance of SQL Server that hosts an availability replica that is joined to its Always On availability group, returns the name of the Windows Server Failover Cluster (WSFC) node that hosts the server instance. |
| [sys.dm_hadr_name_id_map](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-name-id-map-transact-sql.md)    | Shows the mapping of Always On availability groups that the current instance of SQL Server has joined to three unique IDs: an availability group ID, a WSFC resource ID, and a WSFC Group ID. |
| [sys.dm_tcp_listener_states](../../../relational-databases/system-dynamic-management-views/sys-dm-tcp-listener-states-transact-sql.md)    | Returns a row containing dynamic-state information for each TCP listener. |
| &nbsp; | &nbsp; |

## System functions


| System function | Description|
| :------ | :----------------------------- |
| [sys.fn_hadr_is_primary_replica](../../../relational-databases/system-functions/sys-fn-hadr-is-primary-replica-transact-sql.md)  | Used to determine if the current replica is the primary replica. |
| [sys.fn_hadr_backup_is_preferred_replica](../../../relational-databases/system-functions/sys-fn-hadr-backup-is-preferred-replica-transact-sql.md)    | Used to determine if the current replica is the preferred backup replica. |
| [sys.fn_hadr_distributed_ag_replica](../../../relational-databases/system-functions/sys-fn-hadr-distributed-ag-replica-transact-sql.md)    | Used to map a replica in a distributed availability group to the local availability group. |
| &nbsp; | &nbsp; |


  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   

  
