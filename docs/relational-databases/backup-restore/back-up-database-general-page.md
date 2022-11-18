---
title: "Back Up Database (General Page) | Microsoft Docs"
description: In SQL Server, use the General page of the Back Up Database dialog box to view or modify source and destination settings for a database back up operation.
ms.custom: ""
ms.date: "07/01/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.backupdatabase.general.f1"
ms.assetid: 5c344dfd-1ad3-41cc-98cd-732973b4a162
author: MashaMSFT
ms.author: mathoma
---
# Back Up Database (General Page)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Use the **General** page of the **Back Up Database** dialog box to view or modify settings for a database back up operation.  
  
 For more information about basic backup concepts, see [Backup Overview &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-overview-sql-server.md).  
  
> [!NOTE]  
>  When you specify a backup task by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you can generate the corresponding [!INCLUDE[tsql](../../includes/tsql-md.md)] [BACKUP](../../t-sql/statements/backup-transact-sql.md) script by clicking the **Script** button and then selecting a destination for the script.  
  
 **To use SQL Server Management Studio to create a backup**  
  
-   [Create a Full Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md)  
  
-   [Create a Differential Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-differential-database-backup-sql-server.md)  
  
    > [!IMPORTANT]  
    >  You can define a database maintenance plan to create database backups. For more information, see [Database Maintenance Plans](../maintenance-plans/maintenance-plans.md) in [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] Books Online.  
  
 **To create a partial backup**  
  
-   For a partial backup, you must use the [!INCLUDE[tsql](../../includes/tsql-md.md)] [BACKUP](../../t-sql/statements/backup-transact-sql.md) statement with the PARTIAL option.  
  
## Options  
  
### Source  
 The options of the **Source** panel identify the database and specify the backup type and component for the back up operation.  
  
 **Database**  
 Select the database to back up.  
  
 **Recovery model**  
 View the recovery model (SIMPLE, FULL, or BULK_LOGGED) displayed for the selected database.  
  
 **Backup type**  
 Select the type of backup you want to perform on the specified database.  
  
|Backup type|Available for|Restrictions|  
|-----------------|-------------------|------------------|  
|Full|Databases, files, and filegroups|On the **master** database, only full backups are possible.<br /><br /> Under the Simple Recovery Model, file and filegroup backups are available only for read-only filegroups.|  
|Differential|Databases, files, and filegroups|Under the Simple Recovery Model, file and filegroup backups are available only for read-only filegroups.|  
|Transaction Log|Transaction logs|Transaction log backups are not available for the Simple Recovery Model.|  
  
 **Copy Only Backup**  
 Select to create a copy-only backup. A *copy-only backup* is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup that is independent of the sequence of conventional [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backups. For more information, see [Copy-Only Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/copy-only-backups-sql-server.md).  
  
> [!NOTE]  
>  When the **Differential** option is selected, you cannot create a copy-only backup.  
  
 **Backup component**  
 Select the database component to be backed up. If **Transaction Log** is selected in the **Backup type** list, this option is not activated.  
  
 Select one of the following option buttons:  
  
|Option|Description|  
|-|-|  
|**Database**|Specifies that the entire database be backed up.|  
|**Files and filegroups**|Specifies that the specified files and/or filegroups be backed up.<br /><br /> Selecting this option, opens the **Select Files and Filegroups** dialog box. After you select the filegroups or files you want to back up and click **Ok**, your selections appear in the **Filegroups and files** box.|  
  
### Destination  
 The options of the **Destination** panel allow for you to specify the type of backup device for the back up operation and find an existing logical or physical backup device.  
  
> [!NOTE]  
>  For information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup devices, see [Backup Devices &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-devices-sql-server.md).  
  
 **Back up to**  
 Select one of the following types of media to which to back up. The destinations you select appear in the **Back up to** list.  
  
|Media type|Description|  
|-|-|  
|**Disk**|Backs up to disk. This may be a system file or a disk-based logical backup device created for the database. The currently selected disks appear in the **Back up to** list. You can select up to 64 disk devices for your backup operation.|  
|**Tape**|Backs up to tape. This may be a local tape drive or a tape-based logical backup device created for the database. The currently selected tapes appear in the **Back up to** list. The maximum number is 64. If there are no tape devices attached to the server, this option is deactivated. The tapes you select are listed in the **Back up to** list.<br /><br /> Note: Support for tape backup devices will be removed in a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using this feature in new development work, and plan to modify applications that currently use this feature.|  
|**URL**|Backs up to Microsoft Azure Blob storage.|  
  
 The next set of options displayed depends on the type of destination selected. If you select Disk or Tape, the following options are displayed.  
  
 **Add**  
 Adds a file or device to the **Back up to** list. You can back up to 64 devices simultaneously on a local disk or remote disk. To specify a file on a remote disk, use the fully qualified universal naming convention (UNC) name. For more information, see [Backup Devices &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-devices-sql-server.md).  
 
 
  
 **Remove**  
 Removes one or more currently selected devices from the **Back up to** list.  
  
 **Contents**  
Displays the media contents for the selected device if it exists.  The button does not perform a function when a **URL** is specified. 
   
**Select Backup Destination** dialog box
The **Select Backup Destination** dialog box appears after you select **Add**.   The set of options displayed depends on the type of destination selected. 

If you selected **Disk** or **Tape** as the backup destination, the following option is displayed.  

*
  **File Name**  
    Specify the name of the backup file.

If you selected **URL** as the backup destination, the following options are displayed:
*
  **Azure storage container**  
  The name of the Microsoft Azure storage container to store the backup files. 
   
*
  **Shared Access Policy:**  
  Enter the shared access signature for a manually entered container.  This field is not available if an existing container was chosen.
  
*
  **Backup File:**  
  Name of the backup file.

*
  **New Container:**  
Used to register an existing container that you do not have a shared access signature for.  See [Connect to a Microsoft Azure Subscription](../../relational-databases/backup-restore/connect-to-a-microsoft-azure-subscription.md).
  
## See Also  
 [Back Up a Transaction Log &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-a-transaction-log-sql-server.md)   
 [Back Up Files and Filegroups &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-files-and-filegroups-sql-server.md)   
 [Define a Logical Backup Device for a Disk File &#40;SQL Server&#41;](../../relational-databases/backup-restore/define-a-logical-backup-device-for-a-disk-file-sql-server.md)   
 [Define a Logical Backup Device for a Tape Drive &#40;SQL Server&#41;](../../relational-databases/backup-restore/define-a-logical-backup-device-for-a-tape-drive-sql-server.md)   
 [Recovery Models &#40;SQL Server&#41;](../../relational-databases/backup-restore/recovery-models-sql-server.md)  
  
  
