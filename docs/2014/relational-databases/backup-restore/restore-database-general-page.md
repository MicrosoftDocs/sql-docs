---
title: "Restore Database (General Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.restoredb.general.f1"
ms.assetid: 160cf58c-b06a-475f-9a69-2b051e5767ab
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Restore Database (General Page)
  Use the **General** page to specify information about the target and source databases for a database-restore operation.  
  
 **To use SQL Server Management Studio to restore a database backup**  
  
-   [Restore a Database Backup &#40;SQL Server Management Studio&#41;](restore-a-database-backup-using-ssms.md)  
  
-   [Define a Logical Backup Device for a Tape Drive &#40;SQL Server&#41;](define-a-logical-backup-device-for-a-tape-drive-sql-server.md)  
  
> [!NOTE]  
>  When you specify a restore task by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you can generate the corresponding [!INCLUDE[tsql](../../includes/tsql-md.md)][RESTORE](/sql/t-sql/statements/restore-statements-transact-sql) script by clicking **Script** and then selecting a destination for the script.  
  
## Permissions  
 If the database being restored does not exist, the user must have CREATE DATABASE permissions to be able to execute RESTORE. If the database exists, RESTORE permissions default to members of the **sysadmin** and **dbcreator** fixed server roles and the owner (**dbo**) of the database.  
  
 RESTORE permissions are given to roles in which membership information is always readily available to the server. Because fixed database role membership can be checked only when the database is accessible and undamaged, which is not always the case when RESTORE is executed, members of the **db_owner** fixed database role do not have RESTORE permissions.  
  
 Restoring from an encrypted backup requires `VIEW DEFINITION` permissions to the certificate or asymmetric key used to encrypt during backup.  
  
## Options  
  
### Source  
 The options of the **Restore from**panel identify the location of the backup sets for the database and which backup sets you want to restore.  
  
|Term|Definition|  
|----------|----------------|  
|**Database**|Select the database to restore from the drop-down list. The list contains only databases that have been backed up according to the **msdb** backup history.|  
|**Device**|Select the logical or physical backup devices (tapes, URL or files) that contain the backup or backups you want to restore. This is required if the database backup was taken on a different instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> To select one or more logical or physical backup devices, click the browse button which opens the **Select backup devices** dialog box. There, you can select up to 64 devices that belong to a single media set. Tape devices must be physically connected to the computer that is running the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. A backup file can be on a local or remove disk device. For more information, see [Backup Devices &#40;SQL Server&#41;](backup-devices-sql-server.md). You can also select **URL** as the  device type for backup files stored in Windows Azure storage.<br /><br /> When you exit the **Select backup devices** dialog box, the selected device will appear as read-only values in the **Device** list.|  
|**Database**|Select the database name from which the backups should be restored from the dropdown list.<br /><br /> Note: This list is only available when **Device** is selected. Only databases that have backups on the selected devices will be available.|  
  
### Destination  
 The options of the **Restore to** panel identify the database and restore point.  
  
|Term|Definition|  
|----------|----------------|  
|**Database**|Enter the database to restore in the list. You can enter a new database or choose an existing database from the drop-down list. The list includes all databases on the server, excluding the system databases **master** and **tempdb**.<br /><br /> Note: To restore a password-protected backup, you must use the [RESTORE](/sql/t-sql/statements/restore-statements-transact-sql) statement.|  
|**Restore to**|The **Restore to** box will be set "To the last backup taken" by default. You can also click **Timeline** to show the **Backup Timeline** dialog box, which displays the database backup history in the form of a timeline. Click **Timeline** to designate a specific `datetime` to which you want to restore the database. The database will then be restored to the state it was in at this specified point in time. See [Backup Timeline](backup-timeline.md).|  
  
### Restore Plan  
  
|Term|Definition|  
|----------|----------------|  
|**Backup sets to restore**|Displays the backup sets available for the specified location. Each backup set, the result of a single backup operation, is distributed across all of the devices in the media set. By default, a recovery plan is suggested to achieve the goal of the restore operation that is based on the selection of the required backup sets. [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] uses the backup history in **msdb** to identify which backups are required to restore a database, and creates a restore plan. For example, for a database restore, the restore plan selects the most recent full database backup followed by the most recent subsequent differential database backup, if any. Under the full recovery model, the restore plan then selects all subsequent log backups.<br /><br /> To override the suggested recovery plan, you can change the following selections in the grid. Any backups that depend on a deselected backup are deselected automatically.<br /><br /> **Restore**:<br />                          The selected check boxes indicate the backup sets to be restored.<br />**Name**: The name of the backup set.<br />**Component**: The backed-up component: **Database**, **File**, or **\<blank>** (for transaction logs).<br />**Type**: The type of backup performed: **Full**, **Differential**, or **Transaction Log**.<br />**Server**: The name of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] instance that performed the backup operation.<br />**Database**: The name of the database involved in the backup operation.<br />**Position**: The position of the backup set in the volume.<br />**First LSN**: The log sequence number of the first transaction in the backup set. Blank for file backups.<br />**Last LSN**: The log sequence number of the last transaction in the backup set. Blank for file backups.<br />**Checkpoint LSN**: The log sequence number (LSN) of the most recent checkpoint at the time the backup was created.<br />**Full LSN**: The log sequence number of the most recent full database backup.<br />**Start Date**: The date and time when the backup operation began, presented in the regional setting of the client.<br />**Finish Date**: The date and time when the backup operation finished, presented in the regional setting of the client.<br />**Size**: The size of the backup set in bytes.<br />**User Name**: The name of the user who performed the backup operation.<br /><br /> **Expiration**: The date and time the backup set expires.<br /><br /> The checkboxes are only enabled when the **Manual Selection** box is checked. This allows you to select which backup-sets are to be restored.<br /><br /> When the **Manual Selection** box is checked, the accuracy of the Restore Plan is checked each time it is modified. If the sequence of backups is incorrect, an error message will appear.|  
|**Verify Backup Media**|Calls a RESTORE VERIFY_ONLY statement on the selected backup-sets.<br /><br /> Note: This is a long-running operation, and its progress can be tracked and cancelled by using the Progress Monitor on the Dialog Framework.<br /><br /> This button allows you to check the integrity of the selected backup files prior to restoring them.<br /><br /> When checking the integrity of backup sets, the progress status at the bottom left of the dialog box will read "Verifying" rather than "Executing."|  
  
## Compatibility Support  
 In [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], you can restore a user database from a database backup that was created by using [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or a later version. However, backups of **master**, **model** and **msdb** that were created by using [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] through [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] cannot be restored by [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. Also, backups created in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] cannot be restored by any earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] uses a different default path than earlier versions. Therefore, to restore a database that was created in the default location of an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you must use the MOVE option.  
  
 After you restore an earlier version database to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], the database is automatically upgraded. Typically, the database becomes available immediately. However, if a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database has full-text indexes, the upgrade process either imports, resets, or rebuilds them, depending on the setting of the **Full-Text Upgrade Option** server property. If the upgrade option is set to **Import** or **Rebuild**, the full-text indexes will be unavailable during the upgrade. Depending upon the amount of data being indexed, importing can take several hours, and rebuilding can take up to ten times longer. Note also that when the upgrade option is set to **Import**, if a full-text catalog is not available, the associated full-text indexes are rebuilt.  
  
## Restoring from an Encrypted Backup  
 Restore requires that the certificate or asymmetric key that was originally used to create the backup is available on the instance you are restoring to. The account performing the restore should have `VIEW DEFINITIONS` on the certificate or asymmetric key. Certificates used to encrypt backup should not be renewed or updated.  
  
## Restoring from Windows Azure Storage  
 When restoring a backup stored in Windows Azure storage, the Restore UI has a new backup device option. **URL** on the **Select backup devices** dialog. When you click **Add**, this takes you to the **Connect to Windows Azure** dialog that allows you to specify the SQL Credential information to authenticate to the storage account.  Once connected to the storage account, the backup files are displayed in the **Locate a Backup file in Windows Azure** dialog where you can select the file to use for the restore.  
  
## See Also  
 [Backup Devices &#40;SQL Server&#41;](backup-devices-sql-server.md)   
 [Restore a Backup from a Device &#40;SQL Server&#41;](restore-a-backup-from-a-device-sql-server.md)   
 [Restore a Database to a Marked Transaction &#40;SQL Server Management Studio&#41;](restore-a-database-to-a-marked-transaction-sql-server-management-studio.md)   
 [Restore a Transaction Log Backup &#40;SQL Server&#41;](restore-a-transaction-log-backup-sql-server.md)   
 [View the Contents of a Backup Tape or File &#40;SQL Server&#41;](view-the-contents-of-a-backup-tape-or-file-sql-server.md)   
 [View the Properties and Contents of a Logical Backup Device &#40;SQL Server&#41;](view-the-properties-and-contents-of-a-logical-backup-device-sql-server.md)   
 [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](media-sets-media-families-and-backup-sets-sql-server.md)   
 [RESTORE Arguments &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-arguments-transact-sql)   
 [Apply Transaction Log Backups &#40;SQL Server&#41;](transaction-log-backups-sql-server.md)  
  
  
