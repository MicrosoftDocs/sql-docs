---
title: "Restore a Database Backup (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.locatebackupfileazure.f1"
  - "sql12.swb.specifybackup.f1"
helpviewer_keywords: 
  - "full backups [SQL Server]"
  - "database restores [SQL Server], full backups"
  - "backing up databases [SQL Server], full backups"
  - "database backups [SQL Server], full backups"
  - "restoring databases [SQL Server], full backups"
ms.assetid: 24b3311d-5ce0-4581-9a05-5c7c726c7b21
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Restore a Database Backup (SQL Server Management Studio)
  This topic explains how to restore a full database backup.  
  
> [!IMPORTANT]  
>  Under the full or bulk-logged recovery model, before you can restore a database in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you must back up the active transaction log (known as the tail of the log). For more information, see [Back Up a Transaction Log &#40;SQL Server&#41;](back-up-a-transaction-log-sql-server.md). To restore a database that is encrypted, you must have access to the certificate or asymmetric key that was used to encrypt the database. Without the certificate or asymmetric key, the database cannot be restored. As a result, the certificate that is used to encrypt the database encryption key must be retained as long as the backup is needed. For more information, see [SQL Server Certificates and Asymmetric Keys](../security/sql-server-certificates-and-asymmetric-keys.md).  
  
 Note that if you restore a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or higher database to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], the database is automatically upgraded. Typically, the database becomes available immediately. However, if a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database has full-text indexes, the upgrade process either imports, resets, or rebuilds them, depending on the setting of the **Full-Text Upgrade Option** server property. If the upgrade option is set to **Import** or **Rebuild**, the full-text indexes will be unavailable during the upgrade. Depending upon the amount of data being indexed, importing can take several hours, and rebuilding can take up to ten times longer. Note also that when the upgrade option is set to **Import**, if a full-text catalog is not available, the associated full-text indexes are rebuilt. For information about viewing or changing the setting of the **Full-Text Upgrade Option** property, see [Manage and Monitor Full-Text Search for a Server Instance](../search/manage-and-monitor-full-text-search-for-a-server-instance.md).  
  
### To restore a full database backup  
  
1.  After you connect to the appropriate instance of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], in Object Explorer, click the server name to expand the server tree.  
  
2.  Expand **Databases**. Depending on the database, either select a user database or expand **System Databases**, and then select a system database.  
  
3.  Right-click the database, point to **Tasks**, point to **Restore**, and then click **Database**, which opens the **Restore Database** dialog box.  
  
4.  On the **General** page, use the **Source** section to specify the source and location of the backup sets to restore. Select one of the following options:  
  
    -   **Database**  
  
         Select the database to restore from the drop-down list. The list contains only databases that have been backed up according to the **msdb** backup history.  
  
    > [!NOTE]  
    >  If the backup is taken from a different server, the destination server will not have the backup history information for the specified database. In this case, select **Device** to manually specify the file or device to restore.  
  
    -   **Device**  
  
         Click the browse (**...**) button to open the **Select backup devices** dialog box. In the **Backup media type** box, select one of the listed device types. To select one or more devices for the **Backup media** box, click **Add**.  
  
         After you add the devices you want to the **Backup media** list box, click **OK** to return to the **General** page.  
  
         In the **Source: Device: Database** list box, select the name of the database which should be restored.  
  
        > [!NOTE]  
        >  This list is only available when **Device** is selected. Only databases that have backups on the selected device will be available.  
  
         **Backup media**  
         Select the medium for the restore operation: **File**, **Tape**, **URL**or **Backup Device**. The **Tape** option appears only if a tape drive is mounted on the computer, and the **Backup Device** option appears, only if at least one backup device exists.  
  
         **Backup location**  
         View, add, or remove media for the restore operation. The list can contain up to 64 files, tapes, or backup devices.  
  
         **Add**  
         Adds the location of a backup device to the **Backup location** list. Depending on the type of media you select in the **Backup media** field, clicking **Add** opens one of the following dialog boxes.  
  
        |Media type|Dialog box|Description|  
        |----------------|----------------|-----------------|  
        |**File**|**Locate Backup File**|In this dialog box, you can select a local file from the tree or specify a remote file using its fully qualified universal naming convention (UNC) name. For more information, see [Backup Devices &#40;SQL Server&#41;](backup-devices-sql-server.md).|  
        |**Device**|**Select Backup Device**|In this dialog box, you can select from a list of the logical backup devices defined on the server instance.|  
        |**Tape**|**Select Backup Tape**|In this dialog box, you can select from a list of the tape drives that are physically connected to the computer running the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
        |**URL**|This launches two dialog boxes in the following order:<br /><br /> 1) **Connect to Windows Azure Storage**<br /><br /> 2) **Locate Backup File in Windows Azure**|In the **Connect to Windows Azure Storage**  dialog box, Select an existing SQL Credential that stores the Windows Azure storage account name and access key information, or create new SQL Credential by specifying the storage account name and storage access key information. For more information, see [Connect to Windows Azure Storage &#40;Restore&#41;](connect-to-microsoft-azure-storage-restore.md).<br /><br /> In the **Locate Backup File** dialog box, you can select a file from the list of containers displayed on the left frame.|  
  
         If the list is full, the **Add** button is unavailable.  
  
         **Remove**  
         Removes one or more selected files, tapes, or logical backup devices.  
  
         **Contents**  
         Displays the media contents of a selected file, tape, or logical backup device.  
  
5.  In the **Destination** section, the **Database** box is automatically populated with the name of the database to be restored. To change the name of the database, enter the new name in the **Database** box.  
  
6.  In the **Restore to** box, leave the default as **To the last backup taken** or click on **Timeline** to access the **Backup Timeline** dialog box to manually select a point in time to stop the recovery action. For more information on designating a specific point in time, see [Backup Timeline](backup-timeline.md).  
  
7.  In the **Backup sets to restore** grid, select the backups to restore. This grid displays the backups available for the specified location. By default, a recovery plan is suggested. To override the suggested recovery plan, you can change the selections in the grid. Backups that depend on the restoration of an earlier backup are automatically deselected when the earlier backup is deselected. For information about the columns in the **Backup sets to restore** grid, see [Restore Database &#40;General Page&#41;](../../integration-services/general-page-of-integration-services-designers-options.md).  
  
8.  Optionally, click **Files** in the **Select a page** pane to access the **Files** dialog box. From here, you can restore the database to a new location by specifying a new restore destination for each file in the **Restore the database files as** grid. For more information about this grid, see [Restore Database &#40;Files Page&#41;](restore-database-files-page.md).  
  
9. To view or select the advanced options, on the **Options** page, in the **Restore options** panel, you can select any of the following options, if appropriate for your situation:  
  
    1.  `WITH` options (not required):  
  
        -   **Overwrite the existing database (WITH REPLACE)**  
  
        -   **Preserve the replication settings (WITH KEEP_REPLICATION)**  
  
        -   **Restrict access to the restored database (WITH RESTRICTED_USER)**  
  
    2.  Select an option for the **Recovery state** box. This box determines the state of the database after the restore operation.  
  
        -   **RESTORE WITH RECOVERY** is the default behavior which leaves the database ready for use by rolling back the uncommitted transactions. Additional transaction logs cannot be restored. Select this option if you are restoring all of the necessary backups now.  
  
        -   **RESTORE WITH NORECOVERY** which leaves the database non-operational, and does not roll back the uncommitted transactions. Additional transaction logs can be restored. The database cannot be used until it is recovered.  
  
        -   **RESTORE WITH STANDBY** which leaves the database in read-only mode. It undoes uncommitted transactions, but saves the undo actions in a standby file so that recovery effects can be reverted.  
  
    3.  **Take tail-log backup before restore** will be selected if it is necessary for the point in time that you have selected. You do not need to modify this setting, but you can choose to backup the tail of the log even if it is not required. filename here? If the first backup set in the **General** page is in Windows Azure, the tail log will also be backed up to the same storage container.  
  
    4.  Restore operations may fail if there are active connections to the database. Check the **Close existing connections option** to ensure that all active connections between [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and the database are closed. This check box sets the database to single user mode before performing the restore operations, and sets the database to multi-user mode when complete.  
  
    5.  Select **Prompt before restoring each backup** if you wish to be prompted between each restore operation. This is not usually necessary unless the database is large and you wish to monitor the status of the restore operation.  
  
     For more information about these restore options, see [Restore Database &#40;Options Page&#41;](restore-database-options-page.md).  
  
10. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
## See Also  
 [Back Up a Transaction Log &#40;SQL Server&#41;](back-up-a-transaction-log-sql-server.md)   
 [Create a Full Database Backup &#40;SQL Server&#41;](create-a-full-database-backup-sql-server.md)   
 [Restore a Database to a New Location &#40;SQL Server&#41;](restore-a-database-to-a-new-location-sql-server.md)   
 [Restore a Transaction Log Backup &#40;SQL Server&#41;](restore-a-transaction-log-backup-sql-server.md)   
 [RESTORE &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-transact-sql)   
 [Restore Database &#40;Options Page&#41;](restore-database-options-page.md)   
 [Restore Database &#40;General Page&#41;](../../integration-services/general-page-of-integration-services-designers-options.md)  
  
  
