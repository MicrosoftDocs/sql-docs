---
title: "Back Up Files and Filegroups (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2016"
ms.prod: sql
ms.prod_service: backup-restore
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "backing up filegroups [SQL Server]"
  - "file backups [SQL Server], how-to topics"
  - "backing up files [SQL Server]"
  - "backups [SQL Server], creating"
  - "filegroups [SQL Server], backing up"
ms.assetid: a0d3a567-7d8b-4cfe-a505-d197b9a51f70
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Back Up Files and Filegroups (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to back up files and filegroups in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../includes/tsql-md.md)], or PowerShell. When the database size and performance requirements make a full database backup impractical, you can create a file backup instead. A *file backup* contains all the data in one or more files (or filegroups). For more information about file backups, see [Full File Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/full-file-backups-sql-server.md) and [Differential Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/differential-backups-sql-server.md).  

  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The BACKUP statement is not allowed in an explicit or implicit transaction.  
  
-   Under the simple recovery model, read/write files must all be backed up together. This helps make sure that the database can be restored to a consistent point in time. Instead of individually specifying each read/write file or filegroup, use the READ_WRITE_FILEGROUPS option. This option backs up all the read/write filegroups in the database. A backup that is created by specifying READ_WRITE_FILEGROUPS is known as a *partial backup*. For more information, see [Partial Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/partial-backups-sql-server.md).  
  
-   For more information about limitations and restrictions, see [Backup Overview &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-overview-sql-server.md).  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   By default, every successful backup operation adds an entry in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log and in the system event log. If you back up the log very frequently, these success messages accumulate quickly, resulting in huge error logs that can make finding other messages difficult. In such cases you can suppress these log entries by using trace flag 3226 if none of your scripts depend on those entries. For more information, see [Trace Flags &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).  
  
 
##  <a name="Permissions"></a> Permissions  
 BACKUP DATABASE and BACKUP LOG permissions default to members of the **sysadmin** fixed server role and the **db_owner** and **db_backupoperator** fixed database roles.  
  
 Ownership and permission problems on the backup device's physical file can interfere with a backup operation. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be able to read and write to the device; the account under which the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service runs must have write permissions. However, [sp_addumpdevice](../../relational-databases/system-stored-procedures/sp-addumpdevice-transact-sql.md), which adds an entry for a backup device in the system tables, does not check file access permissions. Such problems on the backup device's physical file may not appear until the physical resource is accessed when the backup or restore is attempted.  

[!INCLUDE[Freshness](../includes/paragraph-content/fresh-note-steps-feedback.md)]

## Back up files and filegroups using SSMS   
  
1.  After connecting to the appropriate instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], in Object Explorer, click the server name to expand the server tree.  
  
2.  Expand **Databases**, and, depending on the database, either select a user database or expand **System Databases** and select a system database.  
  
3.  Right-click the database, point to **Tasks**, and then click **Back Up**. The **Back Up Database** dialog box appears.  
  
4.  In the **Database** list, verify the database name. You can optionally select a different database from the list.  
  
5.  In the **Backup type** list, select **Full** or **Differential**.  
  
6.  For the **Backup component** option, click **File and Filegroups**.  
  
7.  In the **Select Files and Filegroups** dialog box, select the files and filegroups you want to back up. You can select one or more individual files or check the box for a filegroup to automatically select all the files in that filegroup.  
  
8.  Either accept the default backup set name suggested in the **Name** text box, or enter a different name for the backup set.  
  
9. Optionally, in the **Description** text box, enter a description of the backup set.  
  
10. Specify when the backup set will expire:  
  
    -   To have the backup set expire after a specific number of days, click **After** (the default option). Then, enter the number of days after set creation that the set will expire. This value can be from 0 to 99999 days; a value of 0 days means that the backup set will never expire.  
  
         The default value is set in the **Default backup media retention (in days)** option of the **Server Properties** dialog box (**Database Settings** page). To access this option, right-click the server name in Object Explorer and select properties; then select the **Database Settings** page.  
  
    -   To have the backup set expire on a specific date, click **On**, and enter the date on which the set will expire.  
  
11. Choose the type of backup destination by clicking **Disk** or **Tape**. To select the paths of up to 64 disk or tape drives that contain a single media set, click **Add**. The selected paths are displayed in the **Backup to** list.  
  
    >**NOTE:** To remove a backup destination, select it and click **Remove**. To view the contents of a backup destination, select it and click **Contents**.  
  
12. To view or select the advanced options, click **Options** in the **Select a page** pane.  
  
13. Select an **Overwrite Media** option, by clicking one of the following:  
  
    -   **Back up to the existing media set**  
  
         For this option, click either **Append to the existing backup set** or **Overwrite all existing backup sets**. For information about backing up to an existing media set, see [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](../../relational-databases/backup-restore/media-sets-media-families-and-backup-sets-sql-server.md).  
  
         Optionally, select **Check media set name and backup set expiration** to cause the backup operation to verify the date and time at which the media set and backup set expire.  
  
         Optionally, enter a name in the **Media set name** text box. If no name is specified, a media set with a blank name is created. If you specify a media set name, the media (tape or disk) is checked to see whether the actual name matches the name that you enter here.  
  
         If you leave the media name blank and check the box to check it against the media, success will equal the media name on the media also being blank.  
  
    -   **Back up to a new media set, and erase all existing backup sets**  
  
         For this option, enter a name in the **New media set name** text box, and, optionally, describe the media set in the **New media set description** text box. For more information about creating a new media set, see [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](../../relational-databases/backup-restore/media-sets-media-families-and-backup-sets-sql-server.md).  
  
14. In the **Reliability** section, optionally check:  
  
    -   **Verify backup when finished**.  
  
    -   **Perform checksum before writing to media**, and, optionally, **Continue on checksum error**. For more information about checksums, see [Possible Media Errors During Backup and Restore &#40;SQL Server&#41;](../../relational-databases/backup-restore/possible-media-errors-during-backup-and-restore-sql-server.md).  
  
15. If you are backing up to a tape drive (as specified in the **Destination** section of the **General** page), the **Unload the tape after backup** option is active. Clicking this option enables the **Rewind the tape before unloading** option.  
  
    > **NOTE:** The options in the **Transaction log** section are inactive unless you are backing up a transaction log (as specified in the **Backup type** section of the **General** page).  
  
16. [!INCLUDE[ssEnterpriseEd10](../../includes/ssenterpriseed10-md.md)] and later versions support [backup compression](../../relational-databases/backup-restore/backup-compression-sql-server.md). By default, whether a backup is compressed depends on the value of the **backup-compression default** server configuration option. However, regardless of the current server-level default, you can compress a backup by checking **Compress backup**, and you can prevent compression by checking **Do not compress backup**.  
  
     **To view the current backup compression default**  
  
    -   [View or Configure the backup compression default Server Configuration Option](../../database-engine/configure-windows/view-or-configure-the-backup-compression-default-server-configuration-option.md)  
  
  
## Back up files and filegroups using T-SQL
  
1.  To create a file or filegroup backup, use a [BACKUP DATABASE <file_or_filegroup>](../../t-sql/statements/backup-transact-sql.md) statement. Minimally, this statement must specify the following:  
  
    -   The database name.  
  
    -   A FILE or FILEGROUP clause for each file or filegroup, respectively.  
  
    -   The backup device on which the full backup will be written.  
  
     The basic [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax for a file backup is:  
  
     BACKUP DATABASE *database*  
  
     { FILE _=_*logical_file_name* | FILEGROUP _=_*logical_filegroup_name* } [ **,**...*f* ]  
  
     TO *backup_device* [ **,**...*n* ]  
  
     [ WITH *with_options* [ **,**...*o* ] ] ;  
  
    |Option|Description|  
    |------------|-----------------|  
    |*database*|Is the database from which the transaction log, partial database, or complete database is backed up.|  
    |FILE _=_*logical_file_name*|Specifies the logical name of a file to include in the file backup.|  
    |FILEGROUP _=_*logical_filegroup_name*|Specifies the logical name of a filegroup to include in the file backup. Under the simple recovery model, a filegroup backup is allowed only for a read-only filegroup.|  
    |[ **,**...*f* ]|Is a placeholder that indicates that multiple files and filegroups may be specified. The number of files or filegroups is unlimited.|  
    |*backup_device* [ **,**...*n* ]|Specifies a list of from 1 to 64 backup devices to use for the backup operation. You can specify a physical backup device, or you can specify a corresponding logical backup device, if already defined. To specify a physical backup device, use the DISK or TAPE option:<br /><br /> { DISK &#124; TAPE } _=_*physical_backup_device_name*<br /><br /> For more information, see [Backup Devices &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-devices-sql-server.md).|  
    |WITH *with_options* [ **,**...*o* ]|Optionally, specifies one or more additional options, such as DIFFERENTIAL.<br /><br /> Note: A differential file backup requires a full file backup as a base. For more information, see [Create a Differential Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-differential-database-backup-sql-server.md).|  
  
2.  Under the full recovery model, you must also back up the transaction log. To use a complete set of full file backups to restore a database, you must also have enough log backups to span all the file backups, from the start of the first file backup. For more information, see [Back Up a Transaction Log &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-a-transaction-log-sql-server.md).  
  
###  <a name="TsqlExample"></a> Examples (Transact-SQL)  
 The following examples back up one or more files of the secondary filegroups of the `Sales` database. This database uses the full recovery model and contains the following secondary filegroups:  
  
-   A filegroup named `SalesGroup1` that has the files `SGrp1Fi1` and `SGrp1Fi2`.  
  
-   A filegroup named `SalesGroup2` that has the files `SGrp2Fi1` and `SGrp2Fi2`.  
  
#### A. Create a file backup of two files  
 The following example creates a differential file backup of only the `SGrp1Fi2` file of the `SalesGroup1` and the `SGrp2Fi2` file of the `SalesGroup2` filegroup.  
  
```sql  
--Backup the files in the SalesGroup1 secondary filegroup.  
BACKUP DATABASE Sales  
   FILE = 'SGrp1Fi2',   
   FILE = 'SGrp2Fi2'   
   TO DISK = 'G:\SQL Server Backups\Sales\SalesGroup1.bck';  
GO  
```  
  
#### B. Create a full file backup of the secondary filegroups  
 The following example creates a full file backup of every file in both of the secondary filegroups.  
  
```sql  
--Back up the files in SalesGroup1.  
BACKUP DATABASE Sales  
   FILEGROUP = 'SalesGroup1',  
   FILEGROUP = 'SalesGroup2'  
   TO DISK = 'C:\MySQLServer\Backups\Sales\SalesFiles.bck';  
GO  
```  
  
#### C. Create a differential file backup of the secondary filegroups  
 The following example creates a differential file backup of every file in both of the secondary filegroups.  
  
```sql  
--Back up the files in SalesGroup1.  
BACKUP DATABASE Sales  
   FILEGROUP = 'SalesGroup1',  
   FILEGROUP = 'SalesGroup2'  
   TO DISK = 'C:\MySQLServer\Backups\Sales\SalesFiles.bck'  
   WITH   
      DIFFERENTIAL;  
GO  
```  
  
##  <a name="PowerShellProcedure"></a> Using PowerShell  
  
1.  Use the **Backup-SqlDatabase** cmdlet and specify **Files** for the value of the **-BackupAction** parameter. Also, specify one of the following parameters:  
  
    -   To back up a specific file, specify the _-DatabaseFile_*String* parameter, where *String* is one or more database files to be backed up.  
  
    -   To back up all the files in a given filegroup, specify the _-DatabaseFileGroup_*String* parameter, where *String* is one or more database filegroups to be backed up.  
  
     The following example creates a full file backup of every file in the secondary filegroups 'FileGroup1' and 'FileGroup2' in the `MyDB` database. The backups are created on the default backup location of the server instance `Computer\Instance`.  
  
    ```sql  
    --Enter this command at the PowerShell command prompt, C:\PS>  
    Backup-SqlDatabase -ServerInstance Computer\Instance -Database MyDB -BackupAction Files -DatabaseFileGroup "FileGroup1","FileGroup2"  
    ```  
  
 **Set up and use the SQL Server PowerShell provider**  
  
-   [SQL Server PowerShell Provider](../../relational-databases/scripting/sql-server-powershell-provider.md)  
  
## See also  
 [Backup Overview &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-overview-sql-server.md)   
 [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)   
 [Backup History and Header Information &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-history-and-header-information-sql-server.md)   
 [Back Up Database &#40;General Page&#41;](../../relational-databases/backup-restore/back-up-database-general-page.md)   
 [Back Up Database &#40;Backup Options Page&#41;](../../relational-databases/backup-restore/back-up-database-backup-options-page.md)   
 [Full File Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/full-file-backups-sql-server.md)   
 [Differential Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/differential-backups-sql-server.md)   
 [File Restores &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/file-restores-full-recovery-model.md)   
 [File Restores &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/file-restores-simple-recovery-model.md)  
  
  
