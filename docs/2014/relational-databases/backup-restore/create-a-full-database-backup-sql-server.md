---
title: "Create a Full Database Backup (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "backing up databases [SQL Server], full backups"
  - "backing up databases [SQL Server], SQL Server Management Studio"
  - "backups [SQL Server], creating"
  - "database backups [SQL Server], SQL Server Management Studio"
ms.assetid: 586561fc-dfbb-4842-84f8-204a9100a534
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Create a Full Database Backup (SQL Server)
  This topic describes how to create a full database backup in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../includes/tsql-md.md)], or PowerShell.  
  
> [!NOTE]  
>  For information on SQL Server backup to the Windows Azure Blob storage service, see, [SQL Server Backup and Restore with Windows Azure Blob Storage Service](sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md).  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
-   **To create a full database backup, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
     [PowerShell](#PowerShellProcedure)  
  
-   [Related Tasks](#RelatedTasks)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The BACKUP statement is not allowed in an explicit or implicit transaction.  
  
-   Backups that are created by more recent version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot be restored in earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   For more information, see [Backup Overview &#40;SQL Server&#41;](backup-overview-sql-server.md).  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   As a database increases in size full database backups take more time to finish and require more storage space. Therefore, for a large database, you might want to supplement a full database backup with a series of *differential database backups*. For more information, see [Differential Backups &#40;SQL Server&#41;](differential-backups-sql-server.md).  
  
-   You can estimate the size of a full database backup by using the [sp_spaceused](/sql/relational-databases/system-stored-procedures/sp-spaceused-transact-sql) system stored procedure.  
  
-   By default, every successful backup operation adds an entry in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log and in the system event log. If back up the log very frequently, these success messages accumulate quickly, resulting in huge error logs that can make finding other messages difficult. In such cases you can suppress these log entries by using trace flag 3226 if none of your scripts depend on those entries. For more information, see [Trace Flags &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql).  
  
###  <a name="Security"></a> Security  
 TRUSTWORTHY is set to OFF on a database backup. For information about how to set TRUSTWORTHY to ON, see [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-set-options).  
  
 Beginning with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] the `PASSWORD` and `MEDIAPASSWORD` options are discontinued for creating backups. You can still restore backups created with passwords.  
  
####  <a name="Permissions"></a> Permissions  
 BACKUP DATABASE and BACKUP LOG permissions default to members of the **sysadmin** fixed server role and the **db_owner** and **db_backupoperator** fixed database roles.  
  
 Ownership and permission problems on the backup device's physical file can interfere with a backup operation. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be able to read and write to the device; the account under which the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service runs must have write permissions. However, [sp_addumpdevice](/sql/relational-databases/system-stored-procedures/sp-addumpdevice-transact-sql), which adds an entry for a backup device in the system tables, does not check file access permissions. Such problems on the backup device's physical file may not appear until the physical resource is accessed when the backup or restore is attempted.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
> [!NOTE]  
>  When you specify a back up task by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you can generate the corresponding [!INCLUDE[tsql](../../includes/tsql-md.md)] [BACKUP](/sql/t-sql/statements/backup-transact-sql) script by clicking the **Script** button and selecting a script destination.  
  
#### To back up a database  
  
1.  After connecting to the appropriate instance of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], in Object Explorer, click the server name to expand the server tree.  
  
2.  Expand **Databases**, and depending on the database, either select a user database or expand **System Databases** and select a system database.  
  
3.  Right-click the database, point to **Tasks**, and then click **Back Up**. The **Back Up Database** dialog box appears.  
  
4.  In the `Database` list box, verify the database name. You can optionally select a different database from the list.  
  
5.  You can perform a database backup for any recovery model (**FULL**, **BULK_LOGGED**, or **SIMPLE**).  
  
6.  In the **Backup type** list box, select **Full**.  
  
     Note that after creating a full database backup, you can create a differential database backup; for more information, see [Create a Differential Database Backup &#40;SQL Server&#41;](create-a-differential-database-backup-sql-server.md).  
  
7.  Optionally, you can select **Copy Only Backup** to create a copy-only backup. A *copy-only backup* is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup that is independent of the sequence of conventional [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backups. For more information, see [Copy-Only Backups &#40;SQL Server&#41;](copy-only-backups-sql-server.md).  
  
    > [!NOTE]  
    >  When the **Differential** option is selected, you cannot create a copy-only backup.  
  
8.  For **Backup component**, click `Database`.  
  
9. Either accept the default backup set name suggested in the **Name** text box, or enter a different name for the backup set.  
  
10. Optionally, in the **Description** text box, enter a description of the backup set.  
  
11. Choose the type of backup destination by clicking **Disk**, **Tape** or **URL**. To select the paths of up to 64 disk or tape drives containing a single media set, click **Add**. The selected paths are displayed in the **Backup to** list box.  
  
     To remove a backup destination, select it and click **Remove**. To view the contents of a backup destination, select it and click **Contents**.  
  
12. To view or select the media options, click **Media Options** in the **Select a page** pane.  
  
13. Select an **Overwrite Media** option, by clicking one of the following:  
  
    -   **Back up to the existing media set**  
  
         For this option, click either **Append to the existing backup set** or **Overwrite all existing backup sets**. For more information, see [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](media-sets-media-families-and-backup-sets-sql-server.md).  
  
         Optionally, select **Check media set name and backup set expiration** to cause the backup operation to verify the date and time at which the media set and backup set expire.  
  
         Optionally, enter a name in the **Media set name** text box. If no name is specified, a media set with a blank name is created. If you specify a media set name, the media (tape or disk) is checked to see whether the actual name matches the name you enter here.  
  
        > [!IMPORTANT]  
        >  This option is disabled if you selected **URL** as the backup destination in the **General** page. For more information, see [Back Up Database &#40;Media Options Page&#41;](back-up-database-media-options-page.md)  
        >   
        >  If you plan to use encryption, do not select this option. If you select this option, the encryption options in the **Backup Options** page will be disabled. Encryption is not supported when appending to the existing backup set.  
  
    -   **Back up to a new media set, and erase all existing backup sets**  
  
         For this option, enter a name in the **New media set name** text box, and, optionally, describe the media set in the **New media set description** text box.  
  
        > [!IMPORTANT]  
        >  This option is disabled if you selected **URL** in the **General** page. These actions are not supported when backing up to Windows Azure storage.  
  
14. In the **Reliability** section, optionally check:  
  
    -   **Verify backup when finished**.  
  
    -   **Perform checksum before writing to media**, and, optionally, **Continue on checksum error**. For information on checksums, see [Possible Media Errors During Backup and Restore &#40;SQL Server&#41;](possible-media-errors-during-backup-and-restore-sql-server.md).  
  
15. If you are backing up to a tape drive (as specified in the **Destination** section of the **General** page), the **Unload the tape after backup** option is active. Clicking this option activates the **Rewind the tape before unloading** option.  
  
    > [!NOTE]  
    >  The options in the **Transaction log** section are inactive unless you are backing up a transaction log (as specified in the **Backup type** section of the **General** page).  
  
16. To view or select the backup options, click **Backup Options** in the **Select a page** pane.  
  
17. Specify when the backup set will expire and can be overwritten without explicitly skipping verification of the expiration data:  
  
    -   To have the backup set expire after a specific number of days, click **After** (the default option), and enter the number of days after set creation that the set will expire. This value can be from 0 to 99999 days; a value of 0 days means that the backup set will never expire.  
  
         The default value is set in the **Default backup media retention (in days)** option of the **Server Properties** dialog box (Database Settings Page). To access this, right-click the server name in Object Explorer and select properties; then select the **Database Settings** page.  
  
    -   To have the backup set expire on a specific date, click **On**, and enter the date on which the set will expire.  
  
         For more information about backup expiration dates, see [BACKUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/backup-transact-sql).  
  
18. [!INCLUDE[ssEnterpriseEd10](../../../includes/ssenterpriseed10-md.md)] and later supports [backup compression](backup-compression-sql-server.md). By default, whether a backup is compressed depends on the value of the **backup-compression default** server configuration option. However, regardless of the current server-level default, you can compress a backup by checking **Compress backup**, and you can prevent compression by checking **Do not compress backup**.  
  
     **To view or change the current backup compression default**  
  
    -   [View or Configure the backup compression default Server Configuration Option](../../database-engine/configure-windows/view-or-configure-the-backup-compression-default-server-configuration-option.md)  
  
19. Specify whether to use encryption for the backup. Select an encryption algorithm to use for the encryption step, and provide a Certificate or Asymmetric key from a list of existing certificates or asymmetric keys. Encryption is supported in SQL Server 2014 or later. For more details on the Encryption options, see [Back Up Database &#40;Backup Options Page&#41;](back-up-database-backup-options-page.md).  
  
> [!NOTE]  
>  Alternatively, you can use the Maintenance Plan Wizard to create database backups.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To create a full database backup  
  
1.  Execute the BACKUP DATABASE statement to create the full database backup, specifying:  
  
    -   The name of the database to back up.  
  
    -   The backup device where the full database backup is written.  
  
     The basic [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax for a full database backup is:  
  
     BACKUP DATABASE *database*  
  
     TO *backup_device* [ **,**...*n* ]  
  
     [ WITH *with_options* [ **,**...*o* ] ] ;  
  
    |Option|Description|  
    |------------|-----------------|  
    |*database*|Is the database that is to be backed up.|  
    |*backup_device* [ **,**...*n* ]|Specifies a list of from 1 to 64 backup devices to use for the backup operation. You can specify a physical backup device, or you can specify a corresponding logical backup device, if already defined. To specify a physical backup device, use the DISK or TAPE option:<br /><br /> { DISK &#124; TAPE } **=**_physical_backup_device_name_<br /><br /> For more information, see [Backup Devices &#40;SQL Server&#41;](backup-devices-sql-server.md).|  
    |WITH *with_options* [ **,**...*o* ]|Optionally, specifies one or more additional options, *o*. For information about some of the basic with options, see step 2.|  
  
2.  Optionally, specify one or more WITH options. A few basic WITH options are described here. For information about all the WITH options, see [BACKUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/backup-transact-sql).  
  
    -   Basic backup set WITH options:  
  
         { COMPRESSION | NO_COMPRESSION }  
         In [!INCLUDE[ssEnterpriseEd10](../../../includes/ssenterpriseed10-md.md)] and later only, specifies whether [backup compression](backup-compression-sql-server.md) is performed on this backup, overriding the server-level default.  
  
         ENCRYPTION (ALGORITHM,  SERVER CERTIFICATE |ASYMMETRIC KEY)  
         In SQL Server 2014 or later only, specify the encryption algorithm to use, and the Certificate or Asymmetric key to use to secure the encryption.  
  
         DESCRIPTION **=** { **'*`text`*'** | **@**_text_variable_ }  
         Specifies the free-form text that describes the backup set. The string can have a maximum of 255 characters.  
  
         NAME **=** { *backup_set_name* | **@**_backup_set_name_var_ }  
         Specifies the name of the backup set. Names can have a maximum of 128 characters. If NAME is not specified, it is blank.  
  
    -   Basic backup set WITH options:  
  
         By default, BACKUP appends the backup to an existing media set, preserving existing backup sets. To explicitly specify this, use the NOINIT option. For information about appending to existing backup sets, see [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](media-sets-media-families-and-backup-sets-sql-server.md).  
  
         Alternatively, to format the backup media, use the FORMAT option:  
  
         FORMAT [ **,** MEDIANAME**=** { *media_name* | **@**_media_name_variable_ } ] [ **,** MEDIADESCRIPTION **=** { *text* | **@**_text_variable_ } ]  
         Use the FORMAT clause when you are using media for the first time or you want to overwrite all existing data. Optionally, assign the new media a media name and description.  
  
        > [!IMPORTANT]  
        >  Use extreme caution when you are using the FORMAT clause of the BACKUP statement because this destroys any backups that were previously stored on the backup media.  
  
###  <a name="TsqlExample"></a> Examples (Transact-SQL)  
  
#### A. Backing up to a disk device  
 The following example backs up the complete [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database to disk, by using `FORMAT` to create a new media set.  
  
```sql  
USE AdventureWorks2012;  
GO  
BACKUP DATABASE AdventureWorks2012  
TO DISK = 'Z:\SQLServerBackups\AdventureWorks2012.Bak'  
   WITH FORMAT,  
      MEDIANAME = 'Z_SQLServerBackups',  
      NAME = 'Full Backup of AdventureWorks2012';  
GO  
```  
  
#### B. Backing up to a tape device  
 The following example backs up the complete [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)]database to tape, appending the backup to the previous backups.  
  
```sql  
USE AdventureWorks2012;  
GO  
BACKUP DATABASE AdventureWorks2012  
   TO TAPE = '\\.\Tape0'  
   WITH NOINIT,  
      NAME = 'Full Backup of AdventureWorks2012';  
GO  
```  
  
#### C. Backing up to a logical tape device  
 The following example creates a logical backup device for a tape drive. The example then backs up the complete [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database to that device.  
  
```sql  
-- Create a logical backup device,   
-- AdventureWorks2012_Bak_Tape, for tape device \\.\tape0.  
USE master;  
GO  
EXEC sp_addumpdevice 'tape', 'AdventureWorks2012_Bak_Tape', '\\.\tape0'; USE AdventureWorks2012;  
GO  
BACKUP DATABASE AdventureWorks2012  
   TO AdventureWorks2012_Bak_Tape  
   WITH FORMAT,  
      MEDIANAME = 'AdventureWorks2012_Bak_Tape',  
      MEDIADESCRIPTION = '\\.\tape0',   
      NAME = 'Full Backup of AdventureWorks2012';  
GO  
```  
  
##  <a name="PowerShellProcedure"></a> Using PowerShell  
  
1.  Use the `Backup-SqlDatabase` cmdlet. To explicitly indicate that this is a full database backup, specify the **-BackupAction**  parameter with its default value, `Database`. This parameter is optional for full database backups.  
  
     The following example creates a full database backup of the `MyDB` database to the default backup location of the server instance `Computer\Instance`. Optionally, this example specifies `-BackupAction Database`.  
  
    ```  
    --Enter this command at the PowerShell command prompt, C:\PS>  
    Backup-SqlDatabase -ServerInstance Computer\Instance -Database MyDB -BackupAction Database  
    ```  
  
 **To set up and use the SQL Server PowerShell provider**  
  
-   [SQL Server PowerShell Provider](../../powershell/sql-server-powershell-provider.md)  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Back Up a Database (SQL Server)](create-a-full-database-backup-sql-server.md)  
  
-   [Create a Differential Database Backup &#40;SQL Server&#41;](create-a-differential-database-backup-sql-server.md)  
  
-   [Restore a Database Backup &#40;SQL Server Management Studio&#41;](restore-a-database-backup-using-ssms.md)  
  
-   [Restore a Database Backup Under the Simple Recovery Model &#40;Transact-SQL&#41;](restore-a-database-backup-under-the-simple-recovery-model-transact-sql.md)  
  
-   [Restore a Database to the Point of Failure Under the Full Recovery Model &#40;Transact-SQL&#41;](restore-database-to-point-of-failure-full-recovery.md)  
  
-   [Restore a Database to a New Location &#40;SQL Server&#41;](restore-a-database-to-a-new-location-sql-server.md)  
  
-   [Use the Maintenance Plan Wizard](../maintenance-plans/use-the-maintenance-plan-wizard.md)  
  
## See Also  
 [Backup Overview &#40;SQL Server&#41;](backup-overview-sql-server.md)   
 [Transaction Log Backups &#40;SQL Server&#41;](transaction-log-backups-sql-server.md)   
 [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](media-sets-media-families-and-backup-sets-sql-server.md)   
 [sp_addumpdevice &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-addumpdevice-transact-sql)   
 [BACKUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/backup-transact-sql)   
 [Back Up Database &#40;General Page&#41;](../../integration-services/general-page-of-integration-services-designers-options.md)   
 [Back Up Database &#40;Backup Options Page&#41;](back-up-database-backup-options-page.md)   
 [Differential Backups &#40;SQL Server&#41;](differential-backups-sql-server.md)   
 [Full Database Backups &#40;SQL Server&#41;](full-database-backups-sql-server.md)  
  
  
