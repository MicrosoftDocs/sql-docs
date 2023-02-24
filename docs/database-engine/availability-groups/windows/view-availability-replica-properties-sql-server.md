---
title: "View availability replica properties"
description: "Instructions for viewing the properties for an availability group replica SQL Server Management Studio (SSMS), Transact-SQL (T-SQL), or SQL PowerShell."
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - ", policies"
---
# View Availability Replica Properties (SQL Server)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to view the properties of an availability replica for an Always On availability group by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)] in [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)].  
  
 
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 **To view and change properties an availability replica**  
  
1.  In Object Explorer, connect to the server instance that hosts the primary replica, and expand the server tree.  
  
2.  Expand the **Always On High Availability** node and the **Availability Groups** node.  
  
3.  Expand the availability group to which the availability replica belongs, and expand the **Availability Replicas** node.  
  
4.  Right-click the availability replica whose properties you want to view, and select the **Properties** command.  
  
5.  In the **Availability Replica Properties** dialog box, use the **General** page to view the properties of this replica. If you are connected to the primary replica, you can change the following properties: availability mode, failover mode, connection access for the primary role, read-access for the secondary role (readable-secondary), and the session-timeout value. For more information, see [Availability Replica Properties &#40;General Page&#41;](../../../database-engine/availability-groups/windows/availability-replica-properties-general-page.md).  

   [!NOTE]
   >If the cluster type is none, you cannot change the failover mode.
  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To view properties and states of availability replicas**  
  
 To view the properties and states of availability replicas, use the following views and system function:  
  
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
 Determines whether the current replica is the preferred backup replica. Returns 1 if the database on the current server instance is the preferred replica. Otherwise, it returns 0.  
  
> [!NOTE]  
>  For information about performance counters for availability replicas (the **SQLServer:Availability Replica**  performance object), see [SQL Server, Availability Replica](../../../relational-databases/performance-monitor/sql-server-availability-replica.md).  
  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **To view information about availability groups**  
  
-   [View Availability Group Properties &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/view-availability-group-properties-sql-server.md)  
  
-   [View Availability Group Listener Properties &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/view-availability-group-listener-properties-sql-server.md)  
  
-   [Always On Policies for Operational Issues with Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-policies-for-operational-issues-always-on-availability.md)  
  
-   [Use the Always On Dashboard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-always-on-dashboard-sql-server-management-studio.md)  
  
-   [Monitor Availability Groups &#40;Transact-SQL&#41;](../../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)  
  
 **To manage availability replicas**  
  
-   [Add a Secondary Replica to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/add-a-secondary-replica-to-an-availability-group-sql-server.md)  
  
-   [Join a Secondary Replica to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/join-a-secondary-replica-to-an-availability-group-sql-server.md)  
  
-   [Configure Read-Only Access on an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/configure-read-only-access-on-an-availability-replica-sql-server.md)  
  
-   [Change the Availability Mode of an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/change-the-availability-mode-of-an-availability-replica-sql-server.md)  
  
-   [Change the Failover Mode of an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/change-the-failover-mode-of-an-availability-replica-sql-server.md)  
  
-   [Change the Session-Timeout Period for an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/change-the-session-timeout-period-for-an-availability-replica-sql-server.md)  
  
-   [Remove a Secondary Replica from an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/remove-a-secondary-replica-from-an-availability-group-sql-server.md)  
  
 **To manage an availability database**  
  
-   [Add a Database to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/availability-group-add-a-database.md)  
  
-   [Join a Secondary Database to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/join-a-secondary-database-to-an-availability-group-sql-server.md)  
  
-   [Suspend an Availability Database &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/suspend-an-availability-database-sql-server.md)  
  
-   [Resume an Availability Database &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/resume-an-availability-database-sql-server.md)  
  
-   [Remove a Secondary Database from an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/remove-a-secondary-database-from-an-availability-group-sql-server.md)  
  
-   [Remove a Primary Database from an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/remove-a-primary-database-from-an-availability-group-sql-server.md)  
  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Monitor Availability Groups &#40;Transact-SQL&#41;](../../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)   
 [Always On Policies for Operational Issues with Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-policies-for-operational-issues-always-on-availability.md)   
 [Administration of an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/administration-of-an-availability-group-sql-server.md)  
  
  
