---
title: "Administration of an Availability Group (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], managing"
ms.assetid: 0b7542fa-235e-413d-81bf-3eff9ee07480
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Administration of an Availability Group (SQL Server)
  Managing an existing AlwaysOn availability group in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)] involves one or more of the following tasks:  
  
-   Altering the properties of an existing availability replica, for example to change client connection access (for configuring readable secondary replicas), changing its failover mode, availability mode, or session timeout setting.  
  
-   Adding or removing secondary replicas.  
  
-   Adding or removing a database.  
  
-   Suspending or resuming a database.  
  
-   Performing a planned manual failover (a *manual failover*) or a forced manual failover (a *forced failover*).  
  
-   Creating or configuring an availability group listener.  
  
-   Managing [readable secondary replicas](active-secondaries-readable-secondary-replicas-always-on-availability-groups.md) for a given availability group. This involves configuring one or more replicas to read-only access when running under the secondary role, and configuring read-only routing.  
  
-   Managing [backups on secondary replicas](active-secondaries-backup-on-secondary-replicas-always-on-availability-groups.md) for a given availability group. This involves configuring where you prefer that backup jobs run and then scripting backup jobs to implement your backup preference. you need to script backup jobs for every database in the availability group on every instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that hosts an availability replica.  
  
-   Deleting an availability group.  
  
-   Cross-cluster migration of AlwaysOn Availability Groups for OS upgrade  
  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **To configure an existing availability group**  
  
-   [Add a Secondary Replica to an Availability Group &#40;SQL Server&#41;](add-a-secondary-replica-to-an-availability-group-sql-server.md)  
  
-   [Remove a Secondary Replica from an Availability Group &#40;SQL Server&#41;](remove-a-secondary-replica-from-an-availability-group-sql-server.md)  
  
-   [Add a Database to an Availability Group &#40;SQL Server&#41;](availability-group-add-a-database.md)  
  
-   [Remove a Secondary Database from an Availability Group &#40;SQL Server&#41;](remove-a-secondary-database-from-an-availability-group-sql-server.md)  
  
-   [Remove a Primary Database from an Availability Group &#40;SQL Server&#41;](remove-a-primary-database-from-an-availability-group-sql-server.md)  
  
-   [Configure the Flexible Failover Policy to Control Conditions for Automatic Failover &#40;AlwaysOn Availability Groups&#41;](configure-flexible-automatic-failover-policy.md)  
  
 **To manage an availability group**  
  
-   [Configure Backup on Availability Replicas &#40;SQL Server&#41;](configure-backup-on-availability-replicas-sql-server.md)  
  
-   [Perform a Planned Manual Failover of an Availability Group &#40;SQL Server&#41;](perform-a-planned-manual-failover-of-an-availability-group-sql-server.md)  
  
-   [Perform a Forced Manual Failover of an Availability Group &#40;SQL Server&#41;](perform-a-forced-manual-failover-of-an-availability-group-sql-server.md)  
  
-   [Remove an Availability Group &#40;SQL Server&#41;](remove-an-availability-group-sql-server.md)  
  
 **To manage an availability replica**  
  
-   [Add a Secondary Replica to an Availability Group &#40;SQL Server&#41;](add-a-secondary-replica-to-an-availability-group-sql-server.md)  
  
-   [Join a Secondary Replica to an Availability Group &#40;SQL Server&#41;](join-a-secondary-replica-to-an-availability-group-sql-server.md)  
  
-   [Remove a Secondary Replica from an Availability Group &#40;SQL Server&#41;](remove-a-secondary-replica-from-an-availability-group-sql-server.md)  
  
-   [Change the Availability Mode of an Availability Replica &#40;SQL Server&#41;](change-the-availability-mode-of-an-availability-replica-sql-server.md)  
  
-   [Change the Failover Mode of an Availability Replica &#40;SQL Server&#41;](change-the-failover-mode-of-an-availability-replica-sql-server.md)  
  
-   [Configure Backup on Availability Replicas &#40;SQL Server&#41;](configure-backup-on-availability-replicas-sql-server.md)  
  
-   [Configure Read-Only Access on an Availability Replica &#40;SQL Server&#41;](configure-read-only-access-on-an-availability-replica-sql-server.md)  
  
-   [Configure Read-Only Routing for an Availability Group &#40;SQL Server&#41;](configure-read-only-routing-for-an-availability-group-sql-server.md)  
  
-   [Change the Session-Timeout Period for an Availability Replica &#40;SQL Server&#41;](change-the-session-timeout-period-for-an-availability-replica-sql-server.md)  
  
 **To manage an availability database**  
  
-   [Add a Database to an Availability Group &#40;SQL Server&#41;](availability-group-add-a-database.md)  
  
-   [Join a Secondary Database to an Availability Group &#40;SQL Server&#41;](join-a-secondary-database-to-an-availability-group-sql-server.md)  
  
-   [Remove a Primary Database from an Availability Group &#40;SQL Server&#41;](remove-a-primary-database-from-an-availability-group-sql-server.md)  
  
-   [Remove a Secondary Database from an Availability Group &#40;SQL Server&#41;](remove-a-secondary-database-from-an-availability-group-sql-server.md)  
  
-   [Suspend an Availability Database &#40;SQL Server&#41;](suspend-an-availability-database-sql-server.md)  
  
-   [Resume an Availability Database &#40;SQL Server&#41;](resume-an-availability-database-sql-server.md)  
  
 **To monitor an availability group**  
  
-   [Monitoring of Availability Groups &#40;SQL Server&#41;](monitoring-of-availability-groups-sql-server.md)  
  
 **To support migrating availability groups to a new WSFC cluster (cross-cluster migration)**  
  
-   [Change the HADR Cluster Context of Server Instance &#40;SQL Server&#41;](change-the-hadr-cluster-context-of-server-instance-sql-server.md)  
  
-   [Take an Availability Group Offline &#40;SQL Server&#41;](../../take-an-availability-group-offline-sql-server.md)  
  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   **Blogs:**  
  
     [SQL Server AlwaysOn Team Blogs: The official SQL Server AlwaysOn Team Blog](https://blogs.msdn.com/b/sqlalwayson/)  
  
     [CSS SQL Server Engineers Blogs](https://blogs.msdn.com/b/psssql/)  
  
-   **Videos:**  
  
     [Microsoft SQL Server Code-Named "Denali" AlwaysOn Series,Part 1: Introducing the Next Generation High Availability Solution](http://channel9.msdn.com/Events/TechEd/NorthAmerica/2011/DBI302)  
  
     [Microsoft SQL Server Code-Named "Denali" AlwaysOn Series,Part 2: Building a Mission-Critical High Availability Solution Using AlwaysOn](http://channel9.msdn.com/Events/TechEd/NorthAmerica/2011/DBI404)  
  
-   **White papers:**  
  
     [Microsoft White Papers for SQL Server 2012](https://msdn.microsoft.com/library/hh403491.aspx)  
  
     [SQL Server Customer Advisory Team Whitepapers](http://sqlcat.com/)  
  
  
## See Also  
 [AlwaysOn Availability Groups &#40;SQL Server&#41;](always-on-availability-groups-sql-server.md)   
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)   
 [Configuration of a Server Instance for AlwaysOn Availability Groups &#40;SQL Server&#41;](configuration-of-a-server-instance-for-always-on-availability-groups-sql-server.md)  
 [Creation and Configuration of Availability Groups &#40;SQL Server&#41;](creation-and-configuration-of-availability-groups-sql-server.md)   
 [Active Secondaries: Readable Secondary Replicas &#40;AlwaysOn Availability Groups&#41;](active-secondaries-readable-secondary-replicas-always-on-availability-groups.md)   
 [Active Secondaries: Backup on Secondary Replicas &#40;AlwaysOn Availability Groups&#41;](active-secondaries-backup-on-secondary-replicas-always-on-availability-groups.md)  
 [Availability Group Listeners, Client Connectivity, and Application Failover &#40;SQL Server&#41;](../../listeners-client-connectivity-application-failover.md)   
 [AlwaysOn Policies for Operational Issues with AlwaysOn Availability Groups &#40;SQL Server&#41;](always-on-policies-for-operational-issues-always-on-availability.md)   
 [Monitoring of Availability Groups &#40;SQL Server&#41;](monitoring-of-availability-groups-sql-server.md)   
 [AlwaysOn Availability Groups: Interoperability &#40;SQL Server&#41;](always-on-availability-groups-interoperability-sql-server.md)   
 [Overview of Transact-SQL Statements for AlwaysOn Availability Groups &#40;SQL Server&#41;](transact-sql-statements-for-always-on-availability-groups.md)   
 [Overview of PowerShell Cmdlets for AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-powershell-cmdlets-for-always-on-availability-groups-sql-server.md)  
  
  
