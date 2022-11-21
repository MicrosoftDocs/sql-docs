---
title: "Restore database (General page) | Microsoft Docs"
description: While restoring a database using SQL Server Management Studio, use the General page to specify information about the target and source databases for a database restore operation.
ms.custom: ""
ms.date: "1/05/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.restoredb.general.f1"
ms.assetid: 160cf58c-b06a-475f-9a69-2b051e5767ab
author: MashaMSFT
ms.author: mathoma
---

# Restore database (General page)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

When you [restore a full database backup using SQL Server Management Studio](../../relational-databases/backup-restore/restore-a-database-backup-using-ssms.md) (SSMS), the **General** page prompts you to specify information about the target and source databases for a database restore operation. This article provides details on how to use the **General** page as part of a database restore operations.

> [!NOTE]  
> When you specify a restore task by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you can generate the corresponding [!INCLUDE[tsql](../../includes/tsql-md.md)] [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md) script by clicking **Script** and then selecting a destination for the script.  
  
## Permissions

If the database being restored does not exist, the user must have `CREATE DATABASE` permissions to be able to successfully restore the database. If the database exists, RESTORE permissions default to members of the `sysadmin` and `dbcreator` fixed server roles and the owner (`dbo`) of the database.

RESTORE permissions are given to roles in which membership information is always readily available to the server. Because fixed database role membership can be checked only when the database is accessible and undamaged, which isn't always the case when RESTORE is executed, members of the `db_owner` fixed database role don't have RESTORE permissions.

Restoring from an encrypted backup requires the **VIEW DEFINITION** permission to the certificate or asymmetric key used to encrypt the backup.  
  
## Options  
  
### Source  

These options identify the location of the backup sets for the database and which backup sets you want to restore.  
  
|Term|Definition|  
|----------|----------------|  
|**Database**|Select the database to restore from the drop-down list. The list contains only databases that have been backed up based on **msdb** backup history.|  
|**Device**|Select the logical or physical backup devices: tapes, URL, or files that contain the backup or backups you want to restore. The device is required if the database backup was taken on a different instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> To select one or more logical or physical backup devices, select the browse button that opens the **Select backup devices** dialog box. You can select up to 64 devices that belong to a single media set. Tape devices must be physically connected to the computer that is running the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. A backup file can be on a local or remote disk device. For more information, see [Backup Devices &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-devices-sql-server.md). You can also select **URL** as the  device type for backup files stored in Azure storage.<br /><br /> When you exit the **Select backup devices** dialog box, the selected device will appear as read-only values in the **Device** list.|  
|**Database**|Select the database name from which the backups should be restored from the dropdown list.<br /><br /> Note: This list is only available when **Device** is selected. Only databases that have backups on the selected devices will be available.|  
  
### Destination  

 The options of the **Restore to** panel identify the database and restore point.  
  
|Term|Definition|  
|----------|----------------|  
|**Database**|Enter the database to restore in the list. You can enter a new database or choose an existing database from the drop-down list. The list includes all databases on the server, excluding the system databases **master** and **tempdb**.<br /><br /> Note: To restore a password-protected backup, you must use the [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md) statement.|  
|**Restore to**|The **Restore to** box will be set "To the last backup taken" by default. You can also select **Timeline** to show the **Backup Timeline** dialog box, which displays the database backup history in the form of a timeline. Select **Timeline** to choose a specific **datetime** to which you want to restore the database. The database will be restored to the state it was in at this specified time. See [Backup Timeline](../../relational-databases/backup-restore/backup-timeline.md).|  
  
### Restore Plan  
  
|Term|Definition|Values|  
|----------|----------------|------------|  
|**Back up sets to restore**|Displays the backup sets available for the specified location. A backup operation creates a backup set that is distributed across all of the devices in the media set. By default, a recovery plan is suggested to achieve the goal of the restore operation that is based on the selection of the required backup sets. [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] uses the backup history in **msdb**. The history is used to identify which backups are required to restore a database, and creates a restore plan. For example, for a database restore, the restore plan selects the most recent full database backup followed by the most recent differential database backup, if any. Under the full recovery model, the restore plan then selects all the log backups.<br /><br /> To override the suggested recovery plan, you can change the selections in the grid. Any backups that depend on a deselected backup are deselected automatically.<br /><br /> The checkboxes are only enabled when the **Manual Selection** box is checked. You can select which backup-sets are to be restored.<br /><br /> When the **Manual Selection** box is checked, the accuracy of the Restore Plan is checked each time it's modified. If the sequence of backups is incorrect, an error message will appear.|**Restore**: <br />                          The selected check boxes indicate the backup sets to be restored.<br /><br /> **Name**: <br />                          The name of the backup set.<br /><br /> **Component**: The backed-up component: **Database**, **File**, or **\<blank>** (for transaction logs).<br /><br /> **Type**: The type of backup: **Full**, **Differential**, or **Transaction Log**.<br /><br /> **Server**: The name of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance that completed the backup operation.<br /><br /> **Database**: <br />                          The name of the database involved in the backup operation.<br /><br /> **Position**: The position of the backup set in the volume.<br /><br /> **First LSN**: <br />                          The log sequence number of the first transaction in the backup set. Blank for file backups.<br /><br /> **Last LSN**: <br />                          The log sequence number of the last transaction in the backup set. Blank for file backups.<br /><br /> **Checkpoint LSN**: <br />                          The log sequence number (LSN) of the most recent checkpoint at the time the backup was created.<br /><br /> **Full LSN**: <br />                          The log sequence number of the most recent full database backup.<br /><br /> **Start Date**: <br />                          The date and time when the backup operation began, presented in the regional setting of the client.<br /><br /> **Finish Date**: <br />                          The date and time when the backup operation finished, presented in the regional setting of the client.<br /><br /> **Size**: <br />                          The size of the backup set in bytes.<br /><br /> **User Name**: <br />                          The name of the user who completed the backup operation.<br /><br /> **Expiration**: <br />                          The date and time the backup set expires.|  
|**Verify Backup Media**|Calls a RESTORE VERIFY_ONLY statement on the selected backup-sets.<br /><br /> Note: Verify is a long-running operation, and its progress can be tracked and canceled by using the Progress Monitor on the Dialog Framework.<br /><br /> The button allows you to check the integrity of the selected backup files prior to restoring them.<br /><br /> When checking the integrity of backup sets, the progress status at the bottom left of the dialog box will read "Verifying" rather than "Executing."||  
  
## Compatibility support  

In [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and greater, you can restore a user database from a database backup that was created by using [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or a later version. Backups of **master**, **model**, and **msdb** that were created by using [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] through [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] cannot be restored by [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and greater. Also, backups created in newer versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] cannot be restored by any earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
Newer versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] use a different default path than versions prior to [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)]. To restore a database that was created in the default location of an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you must use the MOVE option.  
  
 After you restore an earlier version database to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], the database internal version is automatically upgraded. Typically, the database becomes available immediately. However, if a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database has full-text indexes, the upgrade process either imports, resets, or rebuilds them, depending on the setting of the **Full-Text Upgrade Option** server property. If the upgrade option is set to **Import** or **Rebuild**, the full-text indexes will be unavailable during the upgrade. Depending upon the amount of data being indexed, importing can take several hours, and rebuilding can take up to 10 times longer. Note also that when the upgrade option is set to **Import**, if a full-text catalog is not available, the associated full-text indexes are rebuilt.  
  
## Restore from an encrypted backup  

 Restore requires that the certificate or asymmetric key that was originally used to create the backup is available on the instance you're restoring to. The account performing the restore should have the **VIEW DEFINITION** permission on the certificate or asymmetric key. Don't renew or update certificates used to encrypt backups.  
  
## Restore from Microsoft Azure Storage  

Select **URL** from the **Backup media type:** drop-down list from the **Select backup devices** dialog box.  Next, select **Add** to open the **Select a Backup File Location**. Select an existing SQL Server credential and Azure storage container. Add a new Azure storage container with a shared access signature, or generate a shared access signature and SQL Server credential for an existing storage container. Once connected to the storage account, the backup files are displayed in the **Locate Backup File in Microsoft Azure** dialog where you can select the file to use for the restore.  Find more information in [Connect to A Microsoft Azure Subscription](../../relational-databases/backup-restore/connect-to-a-microsoft-azure-subscription.md).
  
## Next steps

Learn more about restoring backups and related concepts in the following articles:

- [Backup Devices &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-devices-sql-server.md)
- [Restore a Backup from a Device &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-a-backup-from-a-device-sql-server.md)
- [RESTORE Arguments &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-arguments-transact-sql.md)
- [Restore a Database to a Marked Transaction &#40;SQL Server Management Studio&#41;](../../relational-databases/backup-restore/restore-a-database-to-a-marked-transaction-sql-server-management-studio.md)
- [Restore a Transaction Log Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-a-transaction-log-backup-sql-server.md)
- [View the Contents of a Backup Tape or File &#40;SQL Server&#41;](../../relational-databases/backup-restore/view-the-contents-of-a-backup-tape-or-file-sql-server.md)
- [View the Properties and Contents of a Logical Backup Device &#40;SQL Server&#41;](../../relational-databases/backup-restore/view-the-properties-and-contents-of-a-logical-backup-device-sql-server.md)
- [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](../../relational-databases/backup-restore/media-sets-media-families-and-backup-sets-sql-server.md)
- [Apply Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/apply-transaction-log-backups-sql-server.md)
