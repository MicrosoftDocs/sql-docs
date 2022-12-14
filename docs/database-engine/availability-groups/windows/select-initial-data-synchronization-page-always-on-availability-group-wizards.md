---
title: "Select Initial Data Sync Page (Availability Group Wizard)"
description: A description of the 'Select Initial Data Sync Page' of the Always On Availability Group Wizard in SQL Server Management Studio (SSMS).
author: MashaMSFT
ms.author: mathoma
ms.date: "05/17/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: conceptual
ms.custom: seo-lt-2019
f1_keywords:
  - "sql13.swb.adddatabasewizard.selectinitialdatasync.f1"
  - "sql13.swb.newagwizard.selectinitialdatasync.f1"
  - "sql13.swb.addreplicawizard.selectinitialdatasync.f1"
---
# Select Initial Data Synchronization Page (Always On Availability Group Wizards)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

  Use the Always On **Select Initial Data Synchronization** page to indicate your preference for initial data synchronization of new secondary databases. This page is shared by three wizards-the [!INCLUDE[ssAoNewAgWiz](../../../includes/ssaonewagwiz-md.md)], the [!INCLUDE[ssAoAddRepWiz](../../../includes/ssaoaddrepwiz-md.md)], and the [!INCLUDE[ssAoAddDbWiz](../../../includes/ssaoadddbwiz-md.md)].  
  
 The possible choices include **Automatic seeding**, **Full database and log backup**, **Join only**, or **Skip initial data synchronization**. Before you select **Automatic seeding**, **Full**, or **Join only** ensure that your environment meets the prerequisites.  
    
##  <a name="Recommendations"></a> Recommendations  
  
-   Suspend log backup tasks for the primary databases during initial data synchronization.  
  
-   For large database, full backup and restore operations can take extensive time and resources. In such cases, we recommend that you prepare secondary databases yourself. For more information, see [To Prepare Secondary Databases Manually](#PrepareSecondaryDbs), later in this topic.  
  
-   Full initial data synchronization requires you to specify a network share. Before you use a wizard to perform full initial data synchronization, we recommend that you implement a security plan for the access permissions on the network share folder. This precaution is important because potentially sensitive data in the backup file can be accessed by anyone who has a READ permission on the folder. Also, to protect your backup and restore operations, we recommend that you secure the network channels between every server instance that hosts an availability replica and the network share folder.  
  
     If your backup and restore operations must be highly secured, we recommend that you select either the **Join only** or **Skip initial data synchronization** option.  
  
## <a name="Auto"></a> Automatic seeding
 
 SQL Server automatically creates the secondary replicas for every database in the group. Automatic seeding requires that the data and log file paths are the same on every SQL Server instance participating in the group. Available on [!INCLUDE[sssql16-md.md](../../../includes/sssql16-md.md)] and later. See [Automatically initialize Always On Availability group](automatically-initialize-always-on-availability-group.md).

##  <a name="Full"></a> Full database and log backup 
 For each primary database, the **Full database and log backup** option performs several operations in one workflow: create a full and log backup of the primary database, create the corresponding secondary databases by restoring these backups on every server instance that is hosting a secondary replica, and join each secondary database to availability group.  
  
 Select this option only if your environment meets the following prerequisites for using full initial data synchronization, and you want the wizard to automatically start data synchronization.  
  
 **Prerequisites for using full database and log backup initial data synchronization**  
  
-   All the database-file paths must be identical on every server instance that hosts a replica for the availability group.  
  
    > [!NOTE]  
    >  If the backup and restore file paths differ between the server instance where you run the wizard and any server instance that is to host a secondary replica. The backup and restore operations must be performed manually using the WITH MOVE option. For more information, see [To Prepare Secondary Databases Manually](#PrepareSecondaryDbs), later in this topic.  
  
-   No primary database name can exist on any server instance that hosts a secondary replica. This means that none of the new secondary databases can exist yet.  
  
-   You will need to specify a network share in order for the wizard to create and access backups. For the primary replica, the account used to start the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] must have read and write file-system permissions on a network share. For secondary replicas, the account must have read permission on the network share.  
  
    > [!IMPORTANT]  
    >  The log backups will be part of your log backup chain. Store the log backup files appropriately.  
  
 **If prerequisites are not met**  
  
 The wizard cannot create the secondary databases for this availability group. For information on how to prepare them, see [To Prepare Secondary Databases Manually](#PrepareSecondaryDbs), later in this topic.  
  
 **If prerequisites are met**  
  
 If these prerequisites are all met and you want the wizard to perform full initial data synchronization, select the **Full database and log backup** option and specify a network share. This will cause  the wizard to create full database and log backups of every selected database and to place these backups on the network share that you specify. Then, on every server instance that hosts one of the new secondary replicas, the wizard will create the secondary databases by restoring backups using RESTORE WITH NORECOVERY. After creating each of the secondary databases, the wizard will join the new secondary database to the availability group. As soon as a secondary database is joined, data synchronizations starts on that database.  
  
 **Specify a shared network location accessible by all replicas**  
 To create and restore backups, the wizard requires that you specify a network share. The account used to start the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] on each server instance that will host an availability replica must have read and write file-system permissions on the network share.  
  
> [!IMPORTANT]  
>  The log backups will be part of your log backup chain. Store their backup files appropriately.  
  
##  <a name="Joinonly"></a> Join only  
 Select this option only if the new secondary databases already exist on each server instance that hosts a secondary replica for the availability group. For information about preparing secondary databases, see [To Prepare Secondary Databases Manually](#PrepareSecondaryDbs), later in this section.  
  
 If you select **Join only**, the wizard will attempt to join each existing secondary database to the availability group.  
  
## <a name="Skip"></a> Skip initial data synchronization  
 Select this option if you want to perform your own database and log backups of every primary database, restore them to every server instance that hosts a secondary replica. After you exit the wizard, you will then need to join every secondary database on every secondary replica.  
  
> [!NOTE]  
>  For more information, see [Start Data Movement on an Always On Secondary Database &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/start-data-movement-on-an-always-on-secondary-database-sql-server.md).  
  
##  <a name="PrepareSecondaryDbs"></a> To Prepare Secondary Databases Manually  
 To prepare secondary databases independently of any [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] wizard, you can use either of the following approaches:  
  
-   Manually restore a recent database backup of the primary database using RESTORE WITH NORECOVERY, and then restore each subsequent log backup using RESTORE WITH NORECOVERY. If the primary and secondary databases have different file paths, you must use the WITH MOVE option. Perform this restore sequence on every server instance that hosts a secondary replica for the availability group.  You can use [!INCLUDE[tsql](../../../includes/tsql-md.md)] or PowerShell to perform these backup and restore operations.  
  
     **For more information:**  
  
     [Manually Prepare a Secondary Database for an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/manually-prepare-a-secondary-database-for-an-availability-group-sql-server.md)  
  
-   If you are adding one or more log shipping primary databases to an availability group, you might be able to migrate one or more of the corresponding secondary databases from log shipping to [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]. For more information, see [Prerequisites for Migrating from Log Shipping to Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-migrating-log-shipping-to-always-on-availability-groups.md).  
  
    > [!NOTE]  
    >  After you have created all the secondary databases for the availability group, if you want to perform backups on secondary replicas, you will need to re-configure the automated backup preference of the availability group.  
  
     **For more information:**  
  
     [Prerequisites for Migrating from Log Shipping to Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-migrating-log-shipping-to-always-on-availability-groups.md)  
  
     [Configure Backup on Availability Replicas &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/configure-backup-on-availability-replicas-sql-server.md)  
  
 After creating a secondary database, apply all current log backups to the new secondary database.  
  
 Optionally, you can prepare all the secondary databases before you run the wizard. Then, on the wizard's **Specify Initial Data Synchronization** page, select **Join only** to automatically join your new secondary databases to the availability group.  
  
##  <a name="LaunchWiz"></a> Related Tasks  
  
-   [Use the New Availability Group Dialog Box &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-new-availability-group-dialog-box-sql-server-management-studio.md)  
  
-   [Use the Add Replica to Availability Group Wizard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-add-replica-to-availability-group-wizard-sql-server-management-studio.md)  
  
-   [Use the Add Database to Availability Group Wizard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/availability-group-add-database-to-group-wizard.md)  
  
-   [Use the Fail Over Availability Group Wizard &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-fail-over-availability-group-wizard-sql-server-management-studio.md)  
  
-   [Start Data Movement on an Always On Secondary Database &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/start-data-movement-on-an-always-on-secondary-database-sql-server.md)  
  
-   [Join a Secondary Database to an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/join-a-secondary-database-to-an-availability-group-sql-server.md)  
  
-   [Use the New Availability Group Dialog Box &#40;SQL Server Management Studio&#41;](../../../database-engine/availability-groups/windows/use-the-new-availability-group-dialog-box-sql-server-management-studio.md)  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)  
  
  
