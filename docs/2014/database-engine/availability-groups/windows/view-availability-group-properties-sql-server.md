---
title: "View Availability Group Properties (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server]"
ms.assetid: 61243c87-bd62-4510-863f-2a8f347caf1f
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# View Availability Group Properties (SQL Server)
  This topic describes how to view the properties of an availability group for an AlwaysOn availability group by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../../includes/tsql-md.md)] in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)].  
  

  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 **To view and change the properties an availability group**  
  
1.  In Object Explorer, connect to the server instance that hosts the primary replica, and expand the server tree.  
  
2.  Expand the **AlwaysOn High Availability** node and the **Availability Groups** node.  
  
3.  Right-click the availability group whose properties you want to view, and select the **Properties** command.  
  
4.  In the **Availability Group Properties** dialog box, use the **General** and **Backup Preferences** pages to view and, in some cases, change properties of the selected availability group. For more information, see [Availability Group Properties and New Availability Group &#40;General Page&#41;](availability-group-properties-new-availability-group-general-page.md) and [Availability Group Properties: New Availability Group &#40;Backup Preferences Page&#41;](availability-group-properties-new-availability-group-backup-preferences-page.md).  
  
     Use the **Permissions** page to view the current logins, roles, and explicit permissions associated with the availability group. For more information, see [Permissions or Securables Page](../../../relational-databases/security/permissions-or-securables-page.md).  
  

  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To view the properties and state of an availability group**  
  
 To query the properties and states of the availability groups for which the server instance hosts an availability replica, use the following views:  
  
 [sys.availability_groups](/sql/relational-databases/system-catalog-views/sys-availability-groups-transact-sql)  
 Returns a row for each availability group for which the local instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] hosts an availability replica. Each row contains a cached copy of the availability group metadata.  
  
 **Column names:** group_id, name, resource_id, resource_group_id, failure_condition_level, health_check_timeout, automated_backup_preference, automated_backup_preference_desc  
  
 [sys.availability_groups_cluster](/sql/relational-databases/system-catalog-views/sys-availability-groups-cluster-transact-sql)  
 Returns a row for each availability group in the WSFC cluster. Each row contains the availability group metadata from the Windows Server Failover Clustering (WSFC) cluster.  
  
 **Column names:** group_id, name, resource_id, resource_group_id, failure_condition_level, health_check_timeout, automated_backup_preference, automated_backup_preference_desc  
  
 [sys.dm_hadr_availability_group_states](/sql/relational-databases/system-dynamic-management-views/sys-dm-hadr-availability-group-states-transact-sql)  
 Returns a row for each availability group that possesses an availability replica on the local instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. Each row displays the states that define the health of a given availability group.  
  
 **Column names:** group_id, primary_replica, primary_recovery_health, primary_recovery_health_desc, secondary_recovery_health, secondary_recovery_health_desc, synchronization_health, synchronization_health_desc  
  

  
##  <a name="RelatedTasks"></a> Related Tasks  
 **To view information about availability groups**  
  
-   [View Availability Replica Properties &#40;SQL Server&#41;](view-availability-replica-properties-sql-server.md)  
  
-   [View Availability Group Listener Properties &#40;SQL Server&#41;](view-availability-group-listener-properties-sql-server.md)  
  
-   [AlwaysOn Policies for Operational Issues with AlwaysOn Availability Groups &#40;SQL Server&#41;](always-on-policies-for-operational-issues-always-on-availability.md)
  
-   [Use the AlwaysOn Dashboard &#40;SQL Server Management Studio&#41;](use-the-always-on-dashboard-sql-server-management-studio.md)  
  
-   [Monitor Availability Groups &#40;Transact-SQL&#41;](monitor-availability-groups-transact-sql.md)  
  
 **To configure an existing availability group**  
  
-   [Add a Secondary Replica to an Availability Group &#40;SQL Server&#41;](add-a-secondary-replica-to-an-availability-group-sql-server.md)  
  
-   [Remove a Secondary Replica from an Availability Group &#40;SQL Server&#41;](remove-a-secondary-replica-from-an-availability-group-sql-server.md)  
  
-   [Add a Database to an Availability Group &#40;SQL Server&#41;](availability-group-add-a-database.md)  
  
-   [Remove a Secondary Database from an Availability Group &#40;SQL Server&#41;](remove-a-secondary-database-from-an-availability-group-sql-server.md)  
  
-   [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](create-or-configure-an-availability-group-listener-sql-server.md)  
  
-   [Remove an Availability Group Listener &#40;SQL Server&#41;](remove-an-availability-group-listener-sql-server.md)  
  
-   [Remove a Primary Database from an Availability Group &#40;SQL Server&#41;](remove-a-primary-database-from-an-availability-group-sql-server.md)  
  
-   [Remove an Availability Group &#40;SQL Server&#41;](remove-an-availability-group-sql-server.md)  
  
 **To manually fail over an availability group**  
  
-   [Perform a Planned Manual Failover of an Availability Group &#40;SQL Server&#41;](perform-a-planned-manual-failover-of-an-availability-group-sql-server.md)  
  
-   [Perform a Forced Manual Failover of an Availability Group &#40;SQL Server&#41;](perform-a-forced-manual-failover-of-an-availability-group-sql-server.md)  
  

  
## See Also  
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)
 [Monitor Availability Groups &#40;Transact-SQL&#41;](monitor-availability-groups-transact-sql.md)
 [AlwaysOn Policies for Operational Issues with AlwaysOn Availability Groups &#40;SQL Server&#41;](always-on-policies-for-operational-issues-always-on-availability.md) 
  
  
