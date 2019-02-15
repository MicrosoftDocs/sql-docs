---
title: "Monitor availability groups using Transact-SQL (T-SQL)"
description: "A description of how to monitor Always On availability groups using Transact-SQL (T-SQL)."
ms.custom: "seodec18"
ms.date: "05/17/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], monitoring"
  - "dynamic management views [SQL Server], AlwaysOn Availability Groups"
  - "Availability Groups [SQL Server], availability replicas"
  - "Availability Groups [SQL Server], listeners"
  - "Availability Groups [SQL Server], databases"
  - "catalog views [SQL Server], AlwaysOn Availability Groups"
ms.assetid: 881a34de-8461-4811-8c62-322bf7226bed
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Monitor Availability Groups (Transact-SQL)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  For monitoring availability groups and replicas and the associated databases by using [!INCLUDE[tsql](../../../includes/tsql-md.md)], [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] provides a set of catalog and dynamic management views and server properties. Using [!INCLUDE[tsql](../../../includes/tsql-md.md)] SELECT statements, you can use the views to monitor availability groups and their replicas and databases. The information returned for a given availability group depends on whether you are connected to the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that is hosting the primary replica or a secondary replica.  
  
> [!TIP]  
>  Many of these views can be joined using their ID columns to return information from multiple views in a single query.  
  
 **In This Topic:**  
  
-   [Permissions](#Permissions)  
  
-   **Using Transact-SQL to monitor:**  
  
     [Always On Availability Groups feature on a server instance](#AoAgFeatureOnSI)  
  
     [Availability groups on the WSFC cluster](#WSFC)  
  
     [Availability groups](#AvGroups)  
  
     [Availability replicas](#AvReplicas)  
  
     [Availability databases](#AvDbs)  
  
     [Availability group listeners](#AGlisteners)  
  
-   [Related Tasks](#RelatedTasks)  
  
##  <a name="Permissions"></a> Permissions  
 [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] catalog views require VIEW ANY DEFINITION permission on the server instance. [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] dynamic management views require VIEW SERVER STATE permission on the server.  
  
##  <a name="AoAgFeatureOnSI"></a> Monitoring the Always On Availability Groups Feature on a Server Instance  
 To monitor the [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] feature on a server instance, use the following built-in function:  
  
 [SERVERPROPERTY](../../../t-sql/functions/serverproperty-transact-sql.md) function  
 Returns server property information about whether [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] is enabled and, if so, whether it has started on the server instance.  
  
 **Column names:** IsHadrEnabled, HadrManagerStatus  
  
##  <a name="WSFC"></a> Monitoring Availability Groups on the WSFC Cluster  
 To monitor the Windows Server Failover Clustering (WSFC) cluster that hosts a local server instance that is enabled for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], use the following views:  
  
 [sys.dm_hadr_cluster](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-cluster-transact-sql.md)  
 If the Windows Server Failover Clustering (WSFC) node that hosts an instance of SQL Server with [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] enabled has WSFC quorum, **sys.dm_hadr_cluster** returns a row that exposes the cluster name and information about the quorum. If the WSFC node has no quorum, no rows are returned.  
  
 **Column names:** cluster_name, quorum_type, quorum_type_desc, quorum_state, quorum_state_desc  
  
 [sys.dm_hadr_cluster_members](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-cluster-members-transact-sql.md)  
 If the WSFC node that hosts the local Always On-enabled instance of SQL Server has WSFC quorum, returns a row for each of the members that constitute the quorum and the state of each of them.  
  
 **Column names:** member_name, member_type, member_type_desc, member_state, member_state_desc, number_of_quorum_votes  
  
 [sys.dm_hadr_cluster_networks](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-cluster-networks-transact-sql.md)  
 Returns a row for every member that is participating in an availability group's subnet configuration. You can use this dynamic management view to validate the network virtual IP that is configured for each availability replica.  
  
 **Column names:** member_name, network_subnet_ip, network_subnet_ipv4_mask, network_subnet_prefix_length, is_public, is_ipv4  
  
 **Primary key:** member_name + network_subnet_IP + network_subnet_prefix_length  
  
 [sys.dm_hadr_instance_node_map](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-instance-node-map-transact-sql.md)  
 For every instance of SQL Server that hosts an availability replica that is joined to its Always On availability group, returns the name of the Windows Server Failover Clustering (WSFC) node that hosts the server instance. This dynamic management view has the following uses:  
  
-   This dynamic management view is useful for detecting an availability group with multiple availability replicas that are hosted on the same WSFC node, which is an unsupported configuration that could occur after an FCI failover if the availability group is incorrectly configured.  
  
-   When multiple SQL Server instances are hosted on the same WSFC node, the Resource DLL uses this dynamic management view to determine the instance of SQL Server to connect to.  
  
 **Column names:** ag_resource_id, instance_name, node_name  
  
 [sys.dm_hadr_name_id_map](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-name-id-map-transact-sql.md)  
 Shows the mapping of Always On availability groups that the current instance of SQL Server has joined to three unique IDs: an availability group ID, a WSFC resource ID, and a WSFC Group ID. The purpose of this mapping is to handle the scenario in which the WSFC resource/group is renamed.  
  
 **Column names:** ag_name, ag_id, ag_resource_id, ag_group_id  
  
> [!NOTE]  
>  Also see **sys.dm_hadr_availability_replica_cluster_nodes** and **sys.dm_hadr_availability_replica_cluster_states** in the [Monitoring Availability Replicas](#AvReplicas) section and **sys.availability_databases_cluster** and **sys.dm_hadr_database_replica_cluster_states** in the [Monitoring Availability Databases](#AvDbs) section, later in this topic.  
  
 For information about WSFC clusters and [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], see [Windows Server Failover Clustering &#40;WSFC&#41; with SQL Server](../../../sql-server/failover-clusters/windows/windows-server-failover-clustering-wsfc-with-sql-server.md) and [Failover Clustering and Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/failover-clustering-and-always-on-availability-groups-sql-server.md).  
  
##  <a name="AvGroups"></a> Monitoring Availability Groups  
 To monitor the availability groups for which the server instance hosts an availability replica, use the following views:  
  
 [sys.availability_groups](../../../relational-databases/system-catalog-views/sys-availability-groups-transact-sql.md)  
 Returns a row for each availability group for which the local instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] hosts an availability replica. Each row contains a cached copy of the availability group metadata.  
  
 **Column names:** group_id, name, resource_id, resource_group_id, failure_condition_level, health_check_timeout, automated_backup_preference, automated_backup_preference_desc  
  
 [sys.availability_groups_cluster](../../../relational-databases/system-catalog-views/sys-availability-groups-cluster-transact-sql.md)  
 Returns a row for each availability group in the WSFC cluster. Each row contains the availability group metadata from the Windows Server Failover Clustering (WSFC) cluster.  
  
 **Column names:** group_id, name, resource_id, resource_group_id, failure_condition_level, health_check_timeout, automated_backup_preference, automated_backup_preference_desc  
  
 [sys.dm_hadr_availability_group_states](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-group-states-transact-sql.md)  
 Returns a row for each availability group that possesses an availability replica on the local instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. Each row displays the states that define the health of a given availability group.  
  
 **Column names:** group_id, primary_replica, primary_recovery_health, primary_recovery_health_desc, secondary_recovery_health, secondary_recovery_health_desc, synchronization_health, synchronization_health_desc  
  
##  <a name="AvReplicas"></a> Monitoring Availability Replicas  
 To monitor availability replicas, use the following views and system function:  
  
 [sys.availability_replicas](../../../relational-databases/system-catalog-views/sys-availability-replicas-transact-sql.md)  
 Returns a row for every availability replica in each availability group for which the local instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] hosts an availability replica.  
  
 **Column names:** replica_id, group_id, replica_metadata_id, replica_server_name, owner_sid, endpoint_url, availability_mode, availability_mode_desc, failover_mode, failover_mode_desc, session_timeout, primary_role_allow_connections, primary_role_allow_connections_desc, secondary_role_allow_connections, secondary_role_allow_connections_desc, create_date, modify_date, backup_priority, read_only_routing_url  
  
 [sys.availability_read_only_routing_lists](../../../relational-databases/system-catalog-views/sys-availability-read-only-routing-lists-transact-sql.md)  
 Returns a row for the read only routing list of each availability replica in an Always On availability group in the WSFC failover cluster.  
  
 **Column names:** replica_id, routing_priority, read_only_replica_id  
  
 [sys.dm_hadr_availability_replica_cluster_nodes](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-replica-cluster-nodes-transact-sql.md)  
 Returns a row for every availability replica (regardless of join state) of the Always On availability groups in the Windows Server Failover Clustering (WSFC) cluster.  
  
 **Column names:** group_name, replica_server_name, node_name  
  
 [sys.dm_hadr_availability_replica_cluster_states](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-replica-cluster-states-transact-sql.md)  
 Returns a row for each replica (regardless of join state) of all Always On availability groups (regardless of replica location) in the Windows Server Failover Clustering (WSFC) cluster.  
  
 **Column names:** replica_id, replica_server_name, group_id, join_state, join_state_desc  
  
 [sys.dm_hadr_availability_replica_states](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-replica-states-transact-sql.md)  
 Returns a row showing the state of each local availability replica and a row for each remote availability replica in the same availability group.  
  
 **Column names:** replica_id, group_id, is_local, role, role_desc, operational_state, operational_state_desc, connected_state, connected_state_desc, recovery_health, recovery_health_desc, synchronization_health, synchronization_health_desc, last_connect_error_number, last_connect_error_description, and last_connect_error_timestamp  
  
 [sys.fn_hadr_backup_is_preferred_replica](../../../relational-databases/system-functions/sys-fn-hadr-backup-is-preferred-replica-transact-sql.md)  
 Determines whether the current replica is the preferred backup replica.  
  
> [!NOTE]  
>  For information about performance counters for availability replicas (the **SQLServer:Availability Replica**  performance object), see [SQL Server, Availability Replica](../../../relational-databases/performance-monitor/sql-server-availability-replica.md).  
  
##  <a name="AvDbs"></a> Monitoring Availability Databases  
 To monitor availability databases, use the following views:  
  
 [sys.availability_databases_cluster](../../../relational-databases/system-catalog-views/sys-availability-databases-cluster-transact-sql.md)  
 Contains one row for each database on the instance of SQL Server that are part of all Always On Availability Groups in the cluster, regardless of whether the local copy database has been joined to the availability group yet.  
  
> [!NOTE]  
>  When a database is added to an availability group, the primary database is automatically joined to the group. Secondary databases must be prepared on each secondary replica before they can be joined to the availability group.  
  
 **Column names:** group_id, group_database_id, database_name  
  
 [sys.databases](../../../relational-databases/system-catalog-views/sys-databases-transact-sql.md)  
 Contains one row per database in the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. If a database belongs to an availability replica, the row for that database displays the GUID of the replica and the unique identifier of the database within its availability group.  
  
 **[!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] column names:** replica_id, group_database_id  
  
 [sys.dm_hadr_auto_page_repair](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-auto-page-repair-transact-sql.md)  
 Returns a row for every automatic page-repair attempt on any availability database on an availability replica that is hosted for any availability group by the server instance. This view contains rows for the latest automatic page-repair attempts on a given primary or secondary database, with a maximum of 100 rows per database. As soon as a database reaches the maximum, the row for its next automatic page-repair attempt replaces one of the existing entries.  
  
 **Column names:** database_id, file_id, page_id, error_type, page_status, modification_time  
  
 [sys.dm_hadr_database_replica_states](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-database-replica-states-transact-sql.md)  
 Returns a row for each database that is participating in any availability group for which the local instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is hosting an availability replica.  
  
 **Column names:** database_id, group_id, replica_id, group_database_id, is_local, synchronization_state, synchronization_state_desc, is_commit_participant, synchronization_health, synchronization_health_desc, database_state, database_state_desc, is_suspended, suspend_reason, suspend_reason_desc, recovery_lsn, truncation_lsn, last_sent_lsn, last_sent_time, last_received_lsn, last_received_time, last_hardened_lsn, last_hardened_time, last_redone_lsn, last_redone_time, log_send_queue_size, log_send_rate, redo_queue_size, redo_rate, filestream_send_rate, end_of_log_lsn, last_commit_lsn, last_commit_time, low_water_mark_for_ghosts  
  
 [sys.dm_hadr_database_replica_cluster_states](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-database-replica-cluster-states-transact-sql.md)  
 Returns a row containing information intended to provide you with insight into the health of the availability databases in each availability group on the Windows Server Failover Clustering (WSFC) cluster. This dynamic management view is useful when planning or responding to a failover or for discovering which secondary replica in an availability group is holding up log truncation on a given primary database.  
  
 **Column names:** replica_id, group_database_id, database_name, is_failover_ready, is_pending_secondary_suspend, is_database_joined, recovery_lsn, truncation_lsn  
  
> [!NOTE]  
>  The primary replica location is the authoritative source for an availability group.  
  
> [!NOTE]  
>  For information about the [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] performance counters for availability databases (the **SQLServer:Database Replica** performance object), see [SQL Server, Database Replica](../../../relational-databases/performance-monitor/sql-server-database-replica.md). Also, to monitor transaction-log activity on availability databases, use the following counters of the **SQLServer:Databases** performance object: **Log Flush Write Time (ms)**, **Log Flushes/sec**, **Log Pool Cache Misses/sec**, **Log Pool Disk Reads/sec**, and **Log Pool Requests/sec**. For more information, see [SQL Server, Databases Object](../../../relational-databases/performance-monitor/sql-server-databases-object.md).  
  
##  <a name="AGlisteners"></a> Monitoring Availability Group Listeners  
 To monitor the availability group listeners on subnets of the WSFC cluster, use the following views:  
  
 [sys.availability_group_listener_ip_addresses](../../../relational-databases/system-catalog-views/sys-availability-group-listener-ip-addresses-transact-sql.md)  
 Returns a row for every conformant virtual IP address that is currently online for an availability group listener.  
  
 **Column names:** listener_id, ip_address, ip_subnet_mask, is_dhcp, network_subnet_ip, network_subnet_prefix_length, network_subnet_ipv4_mask, state, state_desc  
  
 [sys.availability_group_listeners](../../../relational-databases/system-catalog-views/sys-availability-group-listeners-transact-sql.md)  
 For a given availability group, returns either zero rows indicating that no network name is associated with the availability group, or returns a row for each availability-group listener configuration in the WSFC cluster.  
  
 **Column names:** group_id, listener_id, dns_name, port, is_conformant, ip_configuration_string_from_cluster  
  
 [sys.dm_tcp_listener_states](../../../relational-databases/system-dynamic-management-views/sys-dm-tcp-listener-states-transact-sql.md)  
 Returns a row containing dynamic-state information for each TCP listener.  
  
 **Column names:** listener_id, ip_address, is_ipv4, port, type, type_desc, state, state_desc, start_time  
  
 **Primary key:** listener_id  
  
 For information about availability group listeners, see [Availability Group Listeners, Client Connectivity, and Application Failover &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/listeners-client-connectivity-application-failover.md).  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **Always On Availability Groups monitoring tasks:**  
  
-   [Use the Object Explorer Details to Monitor Availability Groups &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-object-explorer-details-to-monitor-availability-groups.md)  
  
-   [View Availability Group Properties &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/view-availability-group-properties-sql-server.md)  
  
-   [View Availability Replica Properties &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/view-availability-replica-properties-sql-server.md)  
  
-   [View Availability Group Listener Properties &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/view-availability-group-listener-properties-sql-server.md)  
  
 **Always On Availability Groups monitoring reference (Transact-SQL):**  
  
-   [SERVERPROPERTY &#40;Transact-SQL&#41;](../../../t-sql/functions/serverproperty-transact-sql.md)  
  
-   [sys.availability_group_listener_ip_addresses &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-availability-group-listener-ip-addresses-transact-sql.md)  
  
-   [sys.availability_group_listeners &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-availability-group-listeners-transact-sql.md)  
  
-   [sys.availability_databases_cluster &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-availability-databases-cluster-transact-sql.md)  
  
-   [sys.availability_groups &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-availability-groups-transact-sql.md)  
  
-   [sys.availability_read_only_routing_lists &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-availability-read-only-routing-lists-transact-sql.md)  
  
-   [sys.availability_replicas &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-availability-replicas-transact-sql.md)  
  
-   [sys.dm_hadr_availability_replica_cluster_nodes &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-replica-cluster-nodes-transact-sql.md)  
  
-   [sys.dm_hadr_availability_replica_cluster_states &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-replica-cluster-states-transact-sql.md)  
  
-   [sys.database_mirroring_endpoints &#40;Transact-SQL&#41;](../../../relational-databases/system-catalog-views/sys-database-mirroring-endpoints-transact-sql.md)  
  
-   [sys.dm_hadr_auto_page_repair &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-auto-page-repair-transact-sql.md)  
  
-   [sys.dm_hadr_availability_group_states &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-group-states-transact-sql.md)  
  
-   [sys.dm_hadr_availability_replica_cluster_states &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-replica-cluster-states-transact-sql.md)  
  
-   [sys.dm_hadr_availability_replica_states &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-replica-states-transact-sql.md)  
  
-   [sys.dm_hadr_database_replica_states &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-database-replica-states-transact-sql.md)  
  
-   [sys.dm_hadr_database_replica_cluster_states &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-database-replica-cluster-states-transact-sql.md)  
  
-   [sys.dm_hadr_cluster &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-cluster-transact-sql.md)  
  
-   [sys.dm_hadr_cluster_members &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-cluster-members-transact-sql.md)  
  
-   [sys.dm_hadr_cluster_networks &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-cluster-networks-transact-sql.md)  
  
-   [sys.dm_hadr_database_replica_cluster_states &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-database-replica-cluster-states-transact-sql.md)  
  
-   [sys.dm_hadr_database_replica_states &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-database-replica-states-transact-sql.md)  
  
-   [sys.dm_hadr_instance_node_map &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-instance-node-map-transact-sql.md)  
  
-   [sys.dm_hadr_name_id_map &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-hadr-name-id-map-transact-sql.md)  
  
-   [sys.dm_os_performance_counters &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md)  
  
-   [sys.dm_tcp_listener_states &#40;Transact-SQL&#41;](../../../relational-databases/system-dynamic-management-views/sys-dm-tcp-listener-states-transact-sql.md)  
  
-   [sys.fn_hadr_backup_is_preferred_replica  &#40;Transact-SQL&#41;](../../../relational-databases/system-functions/sys-fn-hadr-backup-is-preferred-replica-transact-sql.md)  
  
 **Always On performance counters:**  
  
-   [SQL Server, Availability Replica](../../../relational-databases/performance-monitor/sql-server-availability-replica.md)  
  
-   [SQL Server, Database Replica](../../../relational-databases/performance-monitor/sql-server-database-replica.md)  
  
-   [SQL Server, Databases Object](../../../relational-databases/performance-monitor/sql-server-databases-object.md)  
  
 **Policy-based management for Always On Availability Groups**  
  
-   [Use Always On Policies to View the Health of an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/use-always-on-policies-to-view-the-health-of-an-availability-group-sql-server.md)  
  
-   [Use the Always On Dashboard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-always-on-dashboard-sql-server-management-studio.md)  
  
## See Also  
 [Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md)   
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Monitoring of Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/monitoring-of-availability-groups-sql-server.md)  
  
  
