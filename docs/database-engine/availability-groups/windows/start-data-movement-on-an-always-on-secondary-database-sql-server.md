---
title: "Start Data Movement on an Always On Secondary Database (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "05/17/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], databases"
ms.assetid: 498eb3fb-6a43-434d-ad95-68a754232c45
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Start Data Movement on an Always On Secondary Database (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  This topic contains information about how to start data synchronization after you add a database to an Always On availability group. For each new primary replica, secondary databases must be prepared on the server instances that host the secondary replicas. Then each of these secondary databases must be manually joined to the availability group.  
  
> [!NOTE]  
>  If the file paths are identical on every server instance that hosts an availability replica for an availability group, the [New Availability Group Wizard](../../../database-engine/availability-groups/windows/use-the-availability-group-wizard-sql-server-management-studio.md), [Add Replica to Availability Group Wizard](../../../database-engine/availability-groups/windows/use-the-add-replica-to-availability-group-wizard-sql-server-management-studio.md), or [Add Database to Availability Group Wizard](../../../database-engine/availability-groups/windows/availability-group-add-database-to-group-wizard.md) might be able to automatically start data synchronization for you.  
  
 To start data synchronization manually, you need to connect, in turn, to each server instance that is hosting a secondary replica for the availability group and complete the following steps:  
  
1.  Restore current backups of each primary database and its transaction log (using RESTORE WITH NORECOVERY). You can use either of the following alternative approaches:  
  
    -   Manually restore a recent database backup of the primary database using RESTORE WITH NORECOVERY, and then restore each subsequent log backup using RESTORE WITH NORECOVERY. Perform this restore sequence on every server instance that hosts a secondary replica for the availability group.  
  
         **For more information:**  
  
         [Manually Prepare a Secondary Database for an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/manually-prepare-a-secondary-database-for-an-availability-group-sql-server.md)  
  
    -   If you are adding one or more log shipping primary databases to an availability group, you might be able to migrate one or more of the corresponding secondary databases from log shipping to Always On Availability Groups. Migrating a log shipping secondary database requires that it use the same database name as the primary database and that it reside on a server instance that is hosting a secondary replica for the availability group. Furthermore, the availability group must be configured so that the primary replica is preferred for backups and is a candidate for performing backups (that is, that has a backup priority that is >0). Once the backup job has run on the primary database, you will need to disable the backup job, and once the restore job has run on a given secondary database, you will need to disable the restore job.  
  
        > [!NOTE]  
        >  After you have created all the secondary databases for the availability group, if you want to perform backups on secondary replicas, you will need to re-configure the automated backup preference of the availability group.  
  
         **For more information:**  
  
         [Prerequisites for Migrating from Log Shipping to Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-migrating-log-shipping-to-always-on-availability-groups.md)  
  
         [Configure Backup on Availability Replicas &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/configure-backup-on-availability-replicas-sql-server.md)  
  
2.  As soon as possible, join each newly prepared secondary database to the availability group.  
  
     **For more information:**  
  
     [Join a Secondary Database to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/join-a-secondary-database-to-an-availability-group-sql-server.md)  
  
##  <a name="LaunchWiz"></a> Related Tasks  
  
-   [Use the New Availability Group Dialog Box &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-new-availability-group-dialog-box-sql-server-management-studio.md)  
  
-   [Use the Add Replica to Availability Group Wizard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-add-replica-to-availability-group-wizard-sql-server-management-studio.md)  
  
-   [Use the Add Database to Availability Group Wizard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/availability-group-add-database-to-group-wizard.md)  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)  
  
  
