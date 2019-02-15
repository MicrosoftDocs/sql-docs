---
title: "Back Up Database (Media Options Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: backup-restore
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
f1_keywords: 
  - "swb.backupdatabase.mediaoptions.f1"
  - "sql13.swb.backupdatabase.mediaoptions.f1"
ms.assetid: eff36228-710c-4ed5-9af5-95859575dc0f
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Back Up Database (Media Options Page)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Use the  **Media Options** page of the **Back Up Database** dialog box to view or modify database media options.  
  
 **To create a backup by using SQL Server Management Studio**  
  
-   [Create a Full Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md)  
  
-   [Create a Differential Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-differential-database-backup-sql-server.md)  
  
> [!IMPORTANT]  
>  You can define a database maintenance plan to create database backups. For more information, see [Maintenance Plans](../../relational-databases/maintenance-plans/maintenance-plans.md) and [Use the Maintenance Plan Wizard](../../relational-databases/maintenance-plans/use-the-maintenance-plan-wizard.md).  
  
> [!NOTE]  
>  When you specify a backup task by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you can generate the corresponding [!INCLUDE[tsql](../../includes/tsql-md.md)][BACKUP](../../t-sql/statements/backup-transact-sql.md) script by clicking the **Script** button and then selecting a destination for the script.  
  
## Options  
  
### Overwrite media  
 The options of the **Overwrite media** panel control how the backup is written to the media. IF you selected URL (Windows Azure Storage) as the backup destination on the General page of the Back Up Database dialog box, the options under the Overwrite media section are disabled. You can overwrite a backup using the **BACKUP TO URL.. WITH FORMAT** Transact-SQL statement. For more information, see [SQL Server Backup to URL](../../relational-databases/backup-restore/sql-server-backup-to-url.md).  
  
 Only the option, **Backup to a new media, and erase all existing backup sets** is supported with encryption options. If you select the options under the **Back up to existing media** section, the encryptions options on the **Backup Options** page will be disabled.  
  
> [!NOTE]  
>  For information about media sets, see [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](../../relational-databases/backup-restore/media-sets-media-families-and-backup-sets-sql-server.md).  
  
 **Back up to the existing media set**  
 Back up a database to an existing media set. Selecting this option button activates three options.  
  
 Choose one of the following options:  
  
 **Append to the existing backup set**  
 Append the backup set to the existing media set, preserving any prior backups.  
  
 **Overwrite all existing backup sets**  
 Replace any prior backups on the existing media set with the current backup.  
  
 **Check media set name and backup set expiration**  
 Optionally, if backing up to an existing media set, require the backup operation to verify the name and the expiration date of the backup sets.  
  
 **Media set name**  
 If **Check media set name and backup set expiration** is selected, optionally, specify the name of the media set to be used for this backup operation.  
  
 **Back up to a new media set, and erase all existing backup sets**  
 Use a new media set, erasing the previous backup sets.  
  
 Clicking this option activates the following options:  
  
 **New media set name**  
 Optionally, enter a new name for the media set.  
  
 **New media set description**  
 Optionally, enter a meaningful description of the new media set. This description should be specific enough to communicate the contents accurately.  
  
### Reliability  
 The options of the **Transaction log** panel control error management by the backup operation.  
  
 **Verify backup when finished**  
 Verify that the backup set is complete and that all volumes are readable.  
  
 **Perform checksum before writing to media**  
 Verify the checksums before writing to the backup media. Selecting this option is equivalent to specifying the CHECKSUM option in the BACKUP statement of [!INCLUDE[tsql](../../includes/tsql-md.md)]. Selecting this option may increase the workload, and decrease the backup throughput of the backup operation. For information about backup checksums, see [Possible Media Errors During Backup and Restore &#40;SQL Server&#41;](../../relational-databases/backup-restore/possible-media-errors-during-backup-and-restore-sql-server.md).  
  
 **Continue on error**  
 The backup operation is to continue even after encountering one or more errors.  
  
### Transaction log  
 The options of the **Transaction log** panel control the behavior of a transaction log backup. These options are relevant only under the full recovery model or bulk-logged recovery model. They are activated only if **Transaction log** has been selected in the **Backup type** field on the [General](../../relational-databases/backup-restore/back-up-database-general-page.md) page of the **Back Up Database** dialog box.  
  
> [!NOTE]  
>  For information about transaction log backups, see [Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/transaction-log-backups-sql-server.md).  
  
 **Truncate the transaction log**  
 Back up the transaction log and truncate it to free log space. The database remains online. This is the default option.  
  
 **Back up the tail of the log, and leave the database in the restoring state**  
 Back up the tail of the log and leave the database in a restoring state. This option creates a *tail-log backup*, which backs up logs that have not yet been backed up (the active log), typically, in preparation for restoring a database. The database will be unavailable to users until it is completely restored.  
  
 Selecting this option is equivalent to specifying WITH NO_TRUNCATE, NORECOVERY in a [BACKUP](../../t-sql/statements/backup-transact-sql.md) statement ( [!INCLUDE[tsql](../../includes/tsql-md.md)]). For more information, see [Tail-Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/tail-log-backups-sql-server.md).  
  
### Tape drive  
 The options of the **Tape drive** panel control tape management during the backup operation. These options are activated only if **Tape** has been selected in the **Destination** panel on the [General](../../relational-databases/backup-restore/back-up-database-general-page.md) page of the **Back Up Database** dialog box.  
  
> [!NOTE]  
>  For information about how to use tape devices, see [Backup Devices &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-devices-sql-server.md).  
  
 **Unload the tape after backup**  
 After the backup is complete, unload the tape.  
  
 **Rewind the tape before unloading**  
 Before unloading the tape, release and rewind it. This is enabled only if **Unload the tape after backup** is selected.  
  
## See Also  
 [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
 [Back Up a Transaction Log &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-a-transaction-log-sql-server.md)   
 [Back Up Files and Filegroups &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-files-and-filegroups-sql-server.md)   
 [Back Up the Transaction Log When the Database Is Damaged &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-the-transaction-log-when-the-database-is-damaged-sql-server.md)  
  
  
