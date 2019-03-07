---
title: "Create a Differential Database Backup (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: backup-restore
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "full differential backups [SQL Server]"
  - "database backups [SQL Server], full differential backups"
  - "backing up databases [SQL Server], full differential backups"
  - "backups [SQL Server], creating"
ms.assetid: 70f49794-b217-4519-9f2a-76ed61fa9f99
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Create a Differential Database Backup (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Create a differential database backup in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **Sections in this topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Prerequisites](#Prerequisites)  
  
     [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
-   **To create a differential database backup, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before you begin  
  
###  <a name="Restrictions"></a> Limitations and restrictions  
  
-   The BACKUP statement is not allowed in an explicit or implicit transaction.  
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   Creating a differential database backup requires a previous full database backup. If your database has never been backed up, run a full database backup before creating any differential backups. For more information, see [Create a Full Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md).  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   As the differential backups increase in size, restoring a differential backup will significantly increase the time  required to restore a database. We recommend that you take a new full backup at set intervals to establish a new differential base for the data. For example, you might take a weekly full backup of the whole database (that is, a full database backup) followed by a regular series of differential database backups during the week.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Check your permissions first!  
 BACKUP DATABASE and BACKUP LOG permissions default to members of the **sysadmin** fixed server role, the **db_owner** and **db_backupoperator** fixed database roles.  
  
 Ownership and permission problems on the backup device's physical file will interfere with a backup operation. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] needs to be able to read and write to the device; the account under which the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service runs must have write permissions. However, [sp_addumpdevice](../../relational-databases/system-stored-procedures/sp-addumpdevice-transact-sql.md), which adds an entry for a backup device in the system tables, does **not** check file access permissions. Permissions problems on the backup device's physical file will not be obvious until the physical resource is accessed when you attempt the backup or restore.  
  
##  <a name="SSMSProcedure"></a> SQL Server Management Studio  
  
#### Create a differential database backup  
  
1.  After connecting to the appropriate instance of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], in Object Explorer, click the server name to expand the server tree.  
  
2.  Expand **Databases**, and depending on the database, either select a user database or expand **System Databases** and select a system database.  
  
3.  Right-click the database, point to **Tasks**, and then click **Back Up**. The **Back Up Database** dialog box appears.  
  
4.  In the **Database** list box, verify the database name. You can optionally select a different database from the list.  
  
     You can perform a differential backup for any recovery model (full, bulk-logged, or simple).  
  
5.  In the **Backup type** list box, select **Differential**.  
  
    > [!IMPORTANT]  
    >  When you select**Differential** , verify that the **Copy Only Backup** check box is cleared.  
  
6.  For **Backup component**, click **Database**.  
  
7.  Either accept the default backup set name suggested in the **Name** text box, or enter a different name for the backup set.  
  
8.  Optionally, in the **Description** text box, enter a description of the backup set.  
  
9. Specify when the backup set will expire:  
  
    -   To have the backup set expire after a specific number of days, click **After** (the default option), and enter the number of days after set creation that the set will expire. This value can be from 0 to 99999 days;  0 days means the backup set will never expire.  
  
         The default value is set in the **Default backup media retention (in days)** option of the **Server Properties** dialog box (**Database Settings** page). To access this, right-click the server name in Object Explorer and select properties; then select the **Database Settings** page.  
  
    -   To have the backup set expire on a specific date, click **On**, and enter the date on which the set will expire.  
  
10. Choose the type of backup destination by clicking **Disk** or **Tape**. To select the path of up to 64 disk or tape drives containing a single media set, click **Add**. The selected paths are displayed in the **Backup to** list box.  
  
     To remove a backup destination, select it and click **Remove**. To view the contents of a backup destination, select it and click **Contents**.  
  
11. To view or select the advanced options, click **Options** in the **Select a page** pane.  
  
12. Select an **Overwrite Media** option, by clicking one of the following:  
  
    -   **Back up to the existing media set**  
  
         For this option, click either **Append to the existing backup set** or **Overwrite all existing backup sets**. Optionally, check the **Check media set name and backup set expiration** check box and, optionally, enter a name in the **Media set name** text box. If no name is specified, a media set with a blank name is created. If you specify a media set name, the media (tape or disk) is checked to see if the actual name matches the name you enter here.  
  
         If you leave the media name blank and check the box to check it against the media, success will equal the media name on the media also being blank.  
  
    -   **Back up to a new media set, and erase all existing backup sets**  
  
         For this option, enter a name in the **New media set name** text box, and, optionally, describe the media set in the **New media set description** text box.  
  
13. In the **Reliability** section, optionally, check:  
  
    -   **Verify backup when finished**.  
  
    -   **Perform checksum before writing to media**, and, optionally, **Continue on checksum error**. For information about checksums, see [Possible Media Errors During Backup and Restore &#40;SQL Server&#41;](../../relational-databases/backup-restore/possible-media-errors-during-backup-and-restore-sql-server.md).  
  
14. If you are backing up to a tape drive (as specified in the **Destination** section of the **General** page), the **Unload the tape after backup** option is active. Clicking this option activates the **Rewind the tape before unloading** option.  
  
    > [!NOTE]  
    >  The options in the **Transaction log** section are inactive unless you are backing up a transaction log (as specified in the **Backup type** section of the **General** page).  
  
15. [!INCLUDE[ssEnterpriseEd10](../../includes/ssenterpriseed10-md.md)] and later supports [backup compression](../../relational-databases/backup-restore/backup-compression-sql-server.md). By default, whether a backup is compressed depends on the value of the **backup-compression default** server configuration option. However, regardless of the current server-level default, you can compress a backup by checking **Compress backup**, and you can prevent compression by checking **Do not compress backup**.  
  
     **To view the current backup compression default**  
  
    -   [View or Configure the backup compression default Server Configuration Option](../../database-engine/configure-windows/view-or-configure-the-backup-compression-default-server-configuration-option.md)  
  
    > [!NOTE]  
    >  Alternatively, you can use the Maintenance Plan Wizard to create differential database backups.  
  
##  <a name="TsqlProcedure"></a> Transact-SQL  
  
#### Create a differential database backup  
  
1.  Execute the BACKUP DATABASE statement to create the differential database backup, specifying:  
  
    -   The name of the database to back up.  
  
    -   The backup device where the full database backup is written.  
  
    -   The DIFFERENTIAL clause, to specify that only the parts of the database that have changed after the last full database backup was created are backed up.  
  
     The required syntax is:  
  
     BACKUP DATABASE *database_name* TO <backup_device> WITH DIFFERENTIAL  
  
###  <a name="TsqlExample"></a> Example (Transact-SQL)  
 This example creates a full and a differential database backup for the `MyAdvWorks` database.  
  
```sql  
-- Create a full database backup first.  
BACKUP DATABASE MyAdvWorks   
   TO MyAdvWorks_1   
   WITH INIT;  
GO  
-- Time elapses.  
-- Create a differential database backup, appending the backup  
-- to the backup device containing the full database backup.  
BACKUP DATABASE MyAdvWorks  
   TO MyAdvWorks_1  
   WITH DIFFERENTIAL;  
GO  
```  
  
## See Also  
 [Differential Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/differential-backups-sql-server.md)   
 [Create a Full Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md)   
 [Back Up Files and Filegroups &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-files-and-filegroups-sql-server.md)   
 [Restore a Differential Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-a-differential-database-backup-sql-server.md)   
 [Restore a Transaction Log Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-a-transaction-log-backup-sql-server.md)   
 [Maintenance Plans](../../relational-databases/maintenance-plans/maintenance-plans.md)   
 [Full File Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/full-file-backups-sql-server.md)  
  
  
