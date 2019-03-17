---
title: "Back Up Database (General Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.backupdatabase.general.f1"
ms.assetid: 5c344dfd-1ad3-41cc-98cd-732973b4a162
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Back Up Database (General Page)
  Use the **General** page of the **Back Up Database** dialog box to view or modify settings for a database back up operation.  
  
 For more information about basic backup concepts, see [Backup Overview &#40;SQL Server&#41;](backup-overview-sql-server.md).  
  
> [!NOTE]  
>  When you specify a backup task by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you can generate the corresponding [!INCLUDE[tsql](../../includes/tsql-md.md)][BACKUP](/sql/t-sql/statements/backup-transact-sql) script by clicking the **Script** button and then selecting a destination for the script.  
  
 **To use SQL Server Management Studio to create a backup**  
  
-   [Create a Full Database Backup &#40;SQL Server&#41;](create-a-full-database-backup-sql-server.md)  
  
-   [Create a Differential Database Backup &#40;SQL Server&#41;](create-a-differential-database-backup-sql-server.md)  
  
    > [!IMPORTANT]  
    >  You can define a database maintenance plan to create database backups. For more information, see [Database Maintenance Plans](../maintenance-plans/maintenance-plans.md) in [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] Books Online.  
  
 **To create a partial backup**  
  
-   For a partial backup, you must use the [!INCLUDE[tsql](../../includes/tsql-md.md)] [BACKUP](/sql/t-sql/statements/backup-transact-sql) statement with the PARTIAL option.  
  
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
 Select to create a copy-only backup. A *copy-only backup* is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup that is independent of the sequence of conventional [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backups. For more information, see [Copy-Only Backups &#40;SQL Server&#41;](copy-only-backups-sql-server.md).  
  
> [!NOTE]  
>  When the **Differential** option is selected, you cannot create a copy-only backup.  
  
 **Backup component**  
 Select the database component to be backed up. If **Transaction Log** is selected in the **Backup type** list, this option is not activated.  
  
 Select one of the following option buttons:  
  
|||  
|-|-|  
|**Database**|Specifies that the entire database be backed up.|  
|**Files and filegroups**|Specifies that the specified files and/or filegroups be backed up.<br /><br /> Selecting this option, opens the **Select Files and Filegroups** dialog box. After you select the filegroups or files you want to back up and click **Ok**, your selections appear in the **Filegroups and files** box.|  
  
### Destination  
 The options of the **Destination** panel allow for you to specify the type of backup device for the back up operation and find an existing logical or physical backup device.  
  
> [!NOTE]  
>  For information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup devices, see [Backup Devices &#40;SQL Server&#41;](backup-devices-sql-server.md).  
  
 **Back up to**  
 Select one of the following types of media to which to back up. The destinations you select appear in the **Back up to** list.  
  
|||  
|-|-|  
|**Disk**|Backs up to disk. This may be a system file or a disk-based logical backup device created for the database. The currently selected disks appear in the **Back up to** list. You can select up to 64 disk devices for your backup operation.|  
|**Tape**|Backs up to tape. This may be a local tape drive or a tape-based logical backup device created for the database. The currently selected tapes appear in the **Back up to** list. The maximum number is 64. If there are no tape devices attached to the server, this option is deactivated. The tapes you select are listed in the **Back up to** list.<br /><br /> Note: Support for tape backup devices will be removed in a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using this feature in new development work, and plan to modify applications that currently use this feature.|  
|**URL**|Backs up to Windows Azure Blob storage.|  
  
 The next set of options displayed depends on the type of destination selected. If you select Disk or Tape, the following options are displayed.  
  
 **Add**  
 Adds a file or device to the **Back up to** list. You can back up to 64 devices simultaneously on a local disk or remote disk. To specify a file on a remote disk, use the fully qualified universal naming convention (UNC) name. For more information, see [Backup Devices &#40;SQL Server&#41;](backup-devices-sql-server.md).  
  
 **Remove**  
 Removes one or more currently selected devices from the **Back up to** list.  
  
 **Contents**  
 Displays the media contents for the selected device.  
  
 If you select URL as the backup destination, the following options are displayed:  
  
 **File Name**  
 Specify the name of the backup file.  
  
 **SQL credential**  
 Select a SQL Credential used to authenticate to  Windows Azure Storage. If you do not have an existing SQL Credential you can use, click the **Create** button to create a new SQL Credential.  
  
> [!IMPORTANT]  
>  The dialog that opens when you click **Create** requires a management certificate or the publishing profile for the subscription. If you do not have access to the management certificate or publishing profile, you can create a SQL Credential by specifying the storage account name and access key information using Transact-SQL or SQL Server Management Studio. See the sample code in the in the [To create a credential](../security/authentication-access/create-a-credential.md#Credential) topic to create a credential using Transact-SQL. Alternatively, using SQL Server Management Studio, from the database engine instance, right click **Security**, select **New**, and select **Credential**. Specify the storage account name for **Identity** and the access key in the **Password** field.  
  
 **Azure storage container**  
 Specify the name of the Windows Azure storage container  
  
 **URL prefix:**  
 This is automatically generated based on the storage account information stored in the SQL Credential, and Azure storage container name you specified. We recommend that you do not edit the information in this field unless you are using a domain that uses a format other than **\<storage account>.blob.core.windows.net**.  
  
## See Also  
 [Back Up a Transaction Log &#40;SQL Server&#41;](back-up-a-transaction-log-sql-server.md)   
 [Back Up Files and Filegroups &#40;SQL Server&#41;](back-up-files-and-filegroups-sql-server.md)   
 [Define a Logical Backup Device for a Disk File &#40;SQL Server&#41;](define-a-logical-backup-device-for-a-disk-file-sql-server.md)   
 [Define a Logical Backup Device for a Tape Drive &#40;SQL Server&#41;](define-a-logical-backup-device-for-a-tape-drive-sql-server.md)   
 [Recovery Models &#40;SQL Server&#41;](recovery-models-sql-server.md)  
  
  
