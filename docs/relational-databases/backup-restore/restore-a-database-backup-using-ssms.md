---
title: "Restore a Database Backup Using SSMS | Microsoft Docs"
ms.custom: ""
ms.date: "11/16/2016"
ms.prod: sql
ms.prod_service: backup-restore
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.locatebackupfileazure.f1"
  - "sql13.swb.specifybackup.f1"
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
# Restore a Database Backup Using SSMS
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  This topic explains how to restore a full database backup using SQL Server Management Studio.    
       
### Important!    
Before you can restore a database under the full or bulk-logged recovery model, you may need to back up the active transaction log (known as [tail of the log](tail-log-backups-sql-server.md). For more information, see [Back Up a Transaction Log &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-a-transaction-log-sql-server.md).  

When restoring a database from another instance, consider the information from [Manage Metadata When Making a Database Available on Another Server Instance (SQL Server)](../../relational-databases/databases/manage-metadata-when-making-a-database-available-on-another-server.md).   
    
To restore an encrypted database, you need access to the certificate or asymmetric key used to encrypt that database. Without the certificate or asymmetric key, you cannot restore that database. You must retain the certificate used to encrypt the database encryption key for as long as you need to save the backup. For more information, see [SQL Server Certificates and Asymmetric Keys](../../relational-databases/security/sql-server-certificates-and-asymmetric-keys.md).    
    
If you restore an older version database to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], that database will automatically upgrade to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. This precludes the database from being used with an older version of the [!INCLUDE[ssde_md](../../includes/ssde_md.md)]. However, this relates to metadata upgrade and does not affect the [database compatibility level](../../relational-databases/databases/view-or-change-the-compatibility-level-of-a-database.md). If the compatibility level of a user database is 100 or higher before upgrade, it remains the same after upgrade. If the compatibility level is 90 before upgrade, in the upgraded database, the compatibility level is set to 100, which is the lowest supported compatibility level in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. For more information, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md).  
  
Typically, the database becomes available immediately. However, if a [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] database has full-text indexes, the upgrade process either imports, resets, or rebuilds the indexes, depending on the setting of the **Full-Text Upgrade Option** server property. If you set upgrade option to **Import** or **Rebuild**, the full-text indexes will be unavailable during the upgrade. Depending on the amount of data being indexed, importing can take several hours; rebuilding will take up to ten times longer.     
    
When you set upgrade option to **Import**, if a full-text catalog is not available, the associated full-text indexes are rebuilt. For information about viewing or changing the setting of the **Full-Text Upgrade Option** property, see [Manage and Monitor Full-Text Search for a Server Instance](../../relational-databases/search/manage-and-monitor-full-text-search-for-a-server-instance.md).    

For information on SQL Server restore from the Microsoft Azure Blob storage service, see [SQL Server Backup and Restore with Microsoft Azure Blob Storage Service](../../relational-databases/backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md).

## Examples
    
### A. Restore a full database backup   
    
1.  In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and then expand that instance.  
    
2.  Right-click **Databases** and select **Restore Database...**    
    
3.  On the **General** page, use the **Source** section to specify the source and location of the backup sets to restore. Select one of the following options:    
    
    -   **Database**    
    
         Select the database to restore from the drop-down list. The list contains only databases that have been backed up according to the **msdb** backup history.    
    
        > [!NOTE]
        > If the backup is taken from a different server, the destination server will not have the backup history information for the specified database. In this case, select **Device** to manually specify the file or device to restore. 
    
    -   **Device**    
    
         Click the browse (**...**) button to open the **Select backup devices** dialog box. 
         
        -   **Select backup devices** dialog box  
        
            **Backup media type**  
         Select a media type from the **Backup media type** drop-down list.  Note: The **Tape** option appears only if a tape drive is mounted on the computer, and the **Backup Device** option appears, only if at least one backup device exists.

            **Add**  
            Depending on the type of media you select from the **Backup media type** drop-down list, clicking **Add** opens one of the following dialog boxes. (If the list in the **Backup media** list box is full, the **Add** button is unavailable.)

            |Media type|Dialog box|Description|    
            |----------------|----------------|-----------------|    
            |**File**|**Locate Backup File**|In this dialog box, you can select a local file from the tree or specify a remote file using its fully qualified universal naming convention (UNC) name. For more information, see [Backup Devices &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-devices-sql-server.md).|    
            |**Device**|**Select Backup Device**|In this dialog box, you can select from a list of the logical backup devices defined on the server instance.|    
            |**Tape**|**Select Backup Tape**|In this dialog box, you can select from a list of the tape drives that are physically connected to the computer running the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|    
            |**URL**|**Select a Backup File Location**|In this dialog box, you can select an existing SQL Server credential/Azure storage container, add a new Azure storage container with a shared access signature, or generate a shared access signature and SQL Server credential for an existing storage container.  See also, [Connect to a Microsoft Azure Subscription](../../relational-databases/backup-restore/connect-to-a-microsoft-azure-subscription.md)|  
         
             **Remove**    
             Removes one or more selected files, tapes, or logical backup devices.    
                 
             **Contents**    
            Displays the media contents of a selected file, tape, or logical backup device.  This button may not function if the media type is **URL**.  

             **Backup media**   
             Lists the selected media.
    
             After you add the devices you want to the **Backup media** list box, click **OK** to return to the **General** page.    
    
         In the **Source: Device: Database** list box, select the name of the database which should be restored.    
    
         > [!NOTE]
         > This list is only available when **Device** is selected. Only databases that have backups on the selected device will be available.    
     
4.  In the **Destination** section, the **Database** box is automatically populated with the name of the database to be restored. To change the name of the database, enter the new name in the **Database** box.    
    
5.  In the **Restore to** box, leave the default as **To the last backup taken** or click on **Timeline** to access the **Backup Timeline** dialog box to manually select a point in time to stop the recovery action. For more information on designating a specific point in time, see [Backup Timeline](../../relational-databases/backup-restore/backup-timeline.md).    
    
6.  In the **Backup sets to restore** grid, select the backups to restore. This grid displays the backups available for the specified location. By default, a recovery plan is suggested. To override the suggested recovery plan, you can change the selections in the grid. Backups that depend on the restoration of an earlier backup are automatically deselected when the earlier backup is deselected. For information about the columns in the **Backup sets to restore** grid, see [Restore Database &#40;General Page&#41;](../../relational-databases/backup-restore/restore-database-general-page.md).    
    
7.  Optionally, click **Files** in the **Select a page** pane to access the **Files** dialog box. From here, you can restore the database to a new location by specifying a new restore destination for each file in the **Restore the database files as** grid. For more information about this grid, see [Restore Database &#40;Files Page&#41;](../../relational-databases/backup-restore/restore-database-files-page.md).    
    
8. To view or select the advanced options, on the **Options** page, in the **Restore options** panel, you can select any of the following options, if appropriate for your situation:    
    
    1.  **WITH** options (not required):    
    
        -   **Overwrite the existing database (WITH REPLACE)**    
    
        -   **Preserve the replication settings (WITH KEEP_REPLICATION)**    
    
        -   **Restrict access to the restored database (WITH RESTRICTED_USER)**    
    
    2.  Select an option for the **Recovery state** box. This box determines the state of the database after the restore operation.    
    
        -   **RESTORE WITH RECOVERY** is the default behavior which leaves the database ready for use by rolling back the uncommitted transactions. Additional transaction logs cannot be restored. Select this option if you are restoring all of the necessary backups now.    
    
        -   **RESTORE WITH NORECOVERY** which leaves the database non-operational, and does not roll back the uncommitted transactions. Additional transaction logs can be restored. The database cannot be used until it is recovered.    
    
        -   **RESTORE WITH STANDBY** which leaves the database in read-only mode. It undoes uncommitted transactions, but saves the undo actions in a standby file so that recovery effects can be reverted.    
    
    3.  **Take tail-log backup before restore.** Not all restore scenarios require a tail-log backup.  For more information, see **Scenarios That Require a Tail-Log Backup** from [Tail-Log Backups (SQL Server).](../../relational-databases/backup-restore/tail-log-backups-sql-server.md)
    4.  Restore operations may fail if there are active connections to the database. Check the **Close existing connections option** to ensure that all active connections between [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and the database are closed. This check box sets the database to single user mode before performing the restore operations, and sets the database to multi-user mode when complete.    
    5.  Select **Prompt before restoring each backup** if you wish to be prompted between each restore operation. This is not usually necessary unless the database is large and you wish to monitor the status of the restore operation.    
    
For more information about these restore options, see [Restore Database &#40;Options Page&#41;](../../relational-databases/backup-restore/restore-database-options-page.md).    
    
9. [!INCLUDE[clickOK](../../includes/clickok-md.md)] 

### B. Restore an earlier disk backup over an existing database
The following example restores an earlier disk backup of `Sales` and overwrites the existing `Sales` database.

1.  In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and then expand that instance.  
2.  Right-click **Databases** and select **Restore Database...**  
3.  On the **General** page, select **Device** under the **Source** section.
4.  Click the browse (**...**) button to open the **Select backup devices** dialog box. Click **Add** and navigate to your backup. Click **OK** after you have selected your disk backup file(s).
5.  Click **OK** to return to the **General** page.
6.  Click **Options** in the **Select a page** pane.
7.  Under the **Restore options** section, check **Overwrite the existing database (WITH REPLACE)**.

    > [!NOTE]
    > Not checking this option may result in the following error message: "System.Data.SqlClient.SqlError: The backup set holds a backup of a database other than the existing '`Sales`' database. (Microsoft.SqlServer.SmoExtended)"

8.  Under the **Tail-log backup** section, uncheck **Take tail-log backup before restore**.

    > [!NOTE]
    > Not all restore scenarios require a tail-log backup. You do not need a tail-log backup if the recovery point is contained in an earlier log backup. Also, a tail-log backup is unnecessary if you are moving or replacing (overwriting) a database and do not need to restore it to a point of time after its most recent backup. For more information, see [Tail-Log Backups (SQL Server)](../../relational-databases/backup-restore/tail-log-backups-sql-server.md).

    This option is not available for databases in the SIMPLE recovery model.

9.  Under the **Server connections** section, check **Close existing connections to destination database**.

    > [!NOTE]
    > Not checking this option may result in the following error message: "System.Data.SqlClient.SqlError: Exclusive access could not be obtained because the database is in use. (Microsoft.SqlServer.SmoExtended)"
    
10. [!INCLUDE[clickOK](../../includes/clickok-md.md)] 

### C.  Restore an earlier disk backup with a new database name where the original database still exists
The following example restores an earlier disk backup of `Sales` and creates a new database called `SalesTest`.  The original database, `Sales`, still exists on the server.

1.  In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and then expand that instance.  
2.  Right-click **Databases** and select **Restore Database...**  
3.  On the **General** page, select **Device** under the **Source** section.
4.  Click the browse (**...**) button to open the **Select backup devices** dialog box. Click **Add** and navigate to your backup. Click **OK** after you have selected your disk backup file(s).
5.  Click **OK** to return to the **General** page.
6.  In the **Destination** section, the **Database** box is automatically populated with the name of the database to be restored. To change the name of the database, enter the new name in the **Database** box.
7.  Click **Options** in the **Select a page** pane.
8.  Under the **Tail-log backup** section, uncheck "**Take tail-log backup before restore**".

    > [!IMPORTANT]
    > Not unchecking this option will result in the existing database, `Sales`, to change to the restoring state.

9. [!INCLUDE[clickOK](../../includes/clickok-md.md)] 

    > [!NOTE]
    > If you receive the following error message:      
    > "System.Data.SqlClient.SqlError: The tail of the log for the database "`Sales`" has not been backed up. Use `BACKUP LOG WITH NORECOVERY` to backup the log if it contains work you do not want to lose. Use the `WITH REPLACE` or `WITH STOPAT` clause of the `RESTORE` statement to just overwrite the contents of the log. (Microsoft.SqlServer.SmoExtended)".      
    > Then you likely did not enter the new database name from Step 6, above. Restore normally prevents accidentally overwriting a database with a different database. If the database specified in a `RESTORE` statement already exists on the current server and the specified database family GUID differs from the database family GUID recorded in the backup set, the database is not restored. This is an important safeguard.

### D.  Restore earlier disk backups to a point in time
The following example restores a database to its state as of `1:23:17 PM` on `May 30, 2016` and shows a restore operation that involves multiple log backups. The database does not currently exist on the server.

1.  In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and then expand that instance.  
2.  Right-click **Databases** and select **Restore Database...**  
3.  On the **General** page, select **Device** under the **Source** section.
4.  Click the browse (**...**) button to open the **Select backup devices** dialog box. Click **Add** and navigate to your full backup and all relevant transaction log backups.  Click **OK** after you have selected your disk backup files.
5.  Click **OK** to return to the **General** page.
6.  In the **Destination** section, click on **Timeline** to access the **Backup Timeline** dialog box to manually select a point in time to stop the recovery action.
7.  Select **Specific date and time**.  
8.  Change the **Timeline interval** to **Hour** in the drop down box (optional).  
9.  Move the slider to the desired time.
10. Click **OK** to return to the General page.
11. [!INCLUDE[clickOK](../../includes/clickok-md.md)] 

### E.  Restore a backup from the Microsoft Azure storage service

#### Common Steps
The two examples below perform a restore of `Sales` from a backup located in the Microsoft Azure storage service.  The storage Account name is `mystorageaccount`.  The container is called `myfirstcontainer`.  For brevity, the first six steps are listed here once and all examples will start on **Step 7**.
1.	In **Object Explorer**, connect to an instance of the SQL Server Database Engine and then expand that instance.
2.	Right-click **Databases** and select **Restore Database...**.
3.	On the **General** page, select **Device** under the **Source** section.
4.	Click the browse (...) button to open the **Select backup devices** dialog box.    
5.	Select **URL** from the **Backup media type:** drop-down list.
6.	Click **Add** and the **Select a Backup File Location** dialog box opens.

#### E1.   Restore a striped backup over an existing database and a shared access signature exists.
A stored access policy has been created with read, write, delete, and list rights.  A shared access signature that is associated with the stored access policy was created for the container `https://mystorageaccount.blob.core.windows.net/myfirstcontainer`.  The steps are mostly the same if a SQL Server credential already exists.  The database `Sales` currently exists on the server.  The backup files are `Sales_stripe1of2_20160601.bak` and `Sales_stripe2of2_20160601.bak`.  
*  
7.	Select `https://mystorageaccount.blob.core.windows.net/myfirstcontainer` from the **Azure storage container:** drop-down list if the SQL Server credential already exists, else manually enter the name of the container, `https://mystorageaccount.blob.core.windows.net/myfirstcontainer`. 
8.	Enter the shared access signature in the **Shared Access Signature:** rich-text box.
9.	Click **OK** and the **Locate Backup File in Microsoft Azure** dialog box opens.
10.	Expand **Containers** and navigate to `https://mystorageaccount.blob.core.windows.net/myfirstcontainer`.
11.	Hold ctrl and select files `Sales_stripe1of2_20160601.bak` and `Sales_stripe2of2_20160601.bak`.
12.	Click **OK**.
13.	Click **OK** to return to the **General** page.
14.	Click **Options** in the **Select a page** pane.
15.	Under the **Restore** options section, check **Overwrite the existing database (WITH REPLACE)**.
16.	Under the **Tail-log backup** section, uncheck **Take tail-log backup before restore**.
17.	Under the **Server connections** section, check **Close existing connections to destination database**.
18.	Click **OK**.

#### E2.   A shared access signature does not exist
In this example the `Sales` database does not currently exist on the server.
7.	Click **Add** and the **Connect to a Microsoft Subscription** dialog box will open.  
8.	Complete the **Connect to a Microsoft Subscription** dialog box and then click **OK** to return the **Select a Backup File Location** dialog box.  See [Connect to a Microsoft Azure Subscription](../../relational-databases/backup-restore/connect-to-a-microsoft-azure-subscription.md) for additional information.
9.	Click **OK** in the **Select a Backup File Location** dialog box and the **Locate Backup File in Microsoft Azure** dialog box opens.
10.	Expand **Containers** and navigate to `https://mystorageaccount.blob.core.windows.net/myfirstcontainer`.
11.	Select the backup file and then click **OK**.
12.	Click **OK** to return to the **General** page.
13.	Click **OK**.

#### F.	Restore local backup to Microsoft Azure storage (URL)
The `Sales` database will be restored to the Microsoft Azure storage container `https://mystorageaccount.blob.core.windows.net/myfirstcontainer` from a backup located at `E:\MSSQL\BAK`.  The SQL Server credential for the Azure container has already been created.  A SQL Server credential for the destination container must already exist as it cannot be created through the **Restore** task.  The `Sales` database does not currently exist on the server.
1.	In **Object Explorer**, connect to an instance of the SQL Server Database Engine and then expand that instance.
2.	Right-click **Databases** and select **Restore Database...**.
3.	On the **General** page, select **Device** under the **Source** section.
4.	Click the browse (...) button to open the **Select backup devices** dialog box.  
5.	Select **File** from the **Backup media type:** drop-down list.
6.	Click **Add** and the **Locate Backup File** dialog box opens.
7.	Navigate to `E:\MSSQL\BAK`, select the backup file and then click **OK**.
8.	Click **OK** to return to the **General** page.
9.	Click **Files** in the **Select a page** pane.
10.	Check the box **Relocate all files to folder**.
11.	Enter the container, `https://mystorageaccount.blob.core.windows.net/myfirstcontainer`, in the text boxes for **Data file folder:** and **Log file folder:**.
12.	Click **OK**.

## See Also    
 [Back Up a Transaction Log &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-a-transaction-log-sql-server.md)     
 [Create a Full Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md)     
 [Restore a Database to a New Location &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-a-database-to-a-new-location-sql-server.md)     
 [Restore a Transaction Log Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-a-transaction-log-backup-sql-server.md)     
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)     
 [Restore Database &#40;Options Page&#41;](../../relational-databases/backup-restore/restore-database-options-page.md)     
 [Restore Database &#40;General Page&#41;](../../relational-databases/backup-restore/restore-database-general-page.md)    
    
  
