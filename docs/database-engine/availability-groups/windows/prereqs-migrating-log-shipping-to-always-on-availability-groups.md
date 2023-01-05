---
title: "Prerequisites to convert log shipping to availability groups"
description: "A description of the prerequisites necessary to convert log shipping to an Always On availability group."
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: conceptual
ms.custom:
  - seodec18
  - intro-migration
helpviewer_keywords:
  - "log shipping [SQL Server], AlwaysOn Availability Groups"
  - "log shipping [SQL Server], Always On Availability Groups"
  - "Availability Groups [SQL Server], interoperability"
---
# Prerequisites to convert log shipping to Always On availability groups
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

  This topic describes the prerequisites for converting a log shipping primary database along with one or more of its secondary databases to an Always On primary database and secondary database(s).  
  
> [!NOTE]  
>  You can configure any primary or secondary database (possibly readable) in an availability group as a log shipping primary database.  
  
  
##  <a name="AGPrereqsRealAddress"></a> Availability Group Prerequisites  
 To allow backup jobs to run on the primary replica of the availability group, use the following Always On Availability Groups backup settings:  
  
|Property|Setting|  
|--------------|-------------|  
|Automated backup preference of availability group|Only on the primary replica|  
|Backup priority of the primary replica.|>0|  
  
 **For more information:**  
  
 [View Availability Group Properties &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/view-availability-group-properties-sql-server.md)  
  
 [Configure Backup on Availability Replicas &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/configure-backup-on-availability-replicas-sql-server.md)  
  
##  <a name="LogShipPrereqs"></a> Log Shipping Prerequisites  
  
-   The log shipping primary database must reside on the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that hosts the initial/current primary replica of the availability group.  
  
-   For a given log shipping secondary database to be converted to an Always On secondary database, it must:  
  
    -   Use the same name as the primary database.  
  
    -   Reside on a server instance that hosts a secondary replica for the availability group.  
  
 Once the backup job has run on the primary database, disable the backup job, and once the restore job has run on a given secondary database, disable the restore job.  
  
 After you have created all the secondary databases for the availability group, if you want to perform backups on secondary replicas, you need to re-configure the automated backup preference of the availability group.  
  
 **For more information:**  
  
 [Converting a log shipping configuration to Availability Group](/archive/blogs/sqlalwayson/converting-a-logshipping-configuration-to-availability-group) (a SQL Server blog)  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **Log shipping**  
  
-   [Upgrading Log Shipping to SQL Server 2016 &#40;Transact-SQL&#41;](../../../database-engine/log-shipping/upgrading-log-shipping-to-sql-server-2016-transact-sql.md)  
  
-   [Remove Log Shipping &#40;SQL Server&#41;](../../../database-engine/log-shipping/remove-log-shipping-sql-server.md)  
  
 **Always On Availability Groups**  
  
-   [Use the Availability Group Wizard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-availability-group-wizard-sql-server-management-studio.md)  
  
-   [Use the New Availability Group Dialog Box &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-new-availability-group-dialog-box-sql-server-management-studio.md)  
  
-   [Create an Availability Group &#40;Transact-SQL&#41;](../../../database-engine/availability-groups/windows/create-an-availability-group-transact-sql.md)  
  
-   [Create an Availability Group &#40;SQL Server PowerShell&#41;](../../../database-engine/availability-groups/windows/create-an-availability-group-sql-server-powershell.md)  
  
-   [Join a Secondary Database to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/join-a-secondary-database-to-an-availability-group-sql-server.md)  
  
-   [Configure Backup on Availability Replicas &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/configure-backup-on-availability-replicas-sql-server.md)  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   **Blogs:**  
  
     [Converting a logshipping configuration to Availability Group](/archive/blogs/sqlalwayson/converting-a-logshipping-configuration-to-availability-group)  
  
     [Add a Log Shipping Primary Database and Secondary Database(s) to an Existing Availability Group](/archive/blogs/sqlalwayson/add-a-log-shipping-primary-database-and-secondary-databases-to-an-existing-availability-group)  
  
     [SQL Server Always On Team Blogs: The official SQL Server Always On Team Blog](/archive/blogs/sqlalwayson/)  
  
     [CSS SQL Server Engineers Blogs](/archive/blogs/psssql/)  
  
-   **Whitepapers:**  
  
     [Migration Guide: Migrating to Always On Availability Groups from Prior Deployments Combining Database Mirroring and Log Shipping](/previous-versions/sql/sql-server-2012/jj635217(v=msdn.10))  
  
     [Microsoft White Papers for SQL Server 2012](https://social.technet.microsoft.com/wiki/contents/articles/13146.white-paper-gallery-for-sql-server.aspx#[Category]SQLServer2012)  
  
     [SQL Server Customer Advisory Team Whitepapers](https://techcommunity.microsoft.com/t5/DataCAT/bg-p/DataCAT/)  
  
## See Also  
 [About Log Shipping &#40;SQL Server&#41;](../../../database-engine/log-shipping/about-log-shipping-sql-server.md)   
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Monitoring of Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/monitoring-of-availability-groups-sql-server.md)  
  
