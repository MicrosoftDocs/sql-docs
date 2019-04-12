---
title: "Create a Full Database Backup (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: sql
ms.prod_service: backup-restore
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
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
 > For SQL Server 2014, go to [Create a Full Database Backup (SQL Server)](create-a-full-database-backup-sql-server.md).

  This topic describes how to create a full database backup in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../includes/tsql-md.md)], or PowerShell.  
  
>  For information on SQL Server backup to the Azure Blob storage service, see [SQL Server Backup and Restore with Microsoft Azure Blob Storage Service](../../relational-databases/backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md) and [SQL Server Backup to URL](../../relational-databases/backup-restore/sql-server-backup-to-url.md).  
  
##  <a name="BeforeYouBegin"></a> Before You begin! 
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The BACKUP statement is not allowed in an explicit or implicit transaction.  
  
-   Backups created by more recent version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot be restored in earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   For and overview of, and deeper dive into, backup concepts and tasks, see [Backup Overview &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-overview-sql-server.md) before proceding.  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   As a database increases in size full database backups take more time to complete, and require more storage space. For a large database, consider supplementing a full database backup with a series of [differential database backups](../../relational-databases/backup-restore/differential-backups-sql-server.md). For more information, see [SQL Server Backup to URL](../../relational-databases/backup-restore/sql-server-backup-to-url.md).  
  
-   Estimate the size of a full database backup by using the [sp_spaceused](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md) system stored procedure.  
  
-   By default, every successful backup operation adds an entry in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log and in the system event log. If you back up frequently, these success messages will accumulate quickly, resulting in huge error logs! This can make finding other messages difficult. In such cases you can suppress these backup log entries by using trace flag 3226 if none of your scripts depend on those entries. For more information, see [Trace Flags &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).  
  
###  <a name="Security"></a> Security  
 TRUSTWORTHY is set to OFF on a database backup. For information about how to set TRUSTWORTHY to ON, see [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md).  
  
 Beginning with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] the **PASSWORD** and **MEDIAPASSWORD** options are discontinued for creating backups. You can still restore backups created with passwords.  
  
####  <a name="Permissions"></a> Permissions  
 BACKUP DATABASE and BACKUP LOG permissions default to members of the **sysadmin** fixed server role and the **db_owner** and **db_backupoperator** fixed database roles.  
  
 Ownership and permission problems on the backup device's physical file can interfere with a backup operation. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be able to read and write to the device; the account under which the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service runs **must** have write permissions. However, [sp_addumpdevice](../../relational-databases/system-stored-procedures/sp-addumpdevice-transact-sql.md), which adds an entry for a backup device in the system tables, does not check file access permissions. Such problems on the backup device's physical file may not appear until the physical resource is accessed when the backup or restore is attempted.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
>  When you specify a back up task by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you can generate the corresponding [!INCLUDE[tsql](../../includes/tsql-md.md)] [BACKUP](../../t-sql/statements/backup-transact-sql.md) script by clicking the **Script** button and selecting a script destination.  
  
### Back up a database  
  
1.  After connecting to the appropriate instance of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], in **Object Explorer**, click the server name to expand the server tree.  
  
2.  Expand **Databases**, and either select a user database or expand **System Databases** and select a system database.  
  
3.  Right-click the database, point to **Tasks**, and then click **Back Up**. The **Back Up Database** dialog box appears.  

  #### **General Page**
  
4.  In the **Database** drop-down list, verify the database name. Optionally, you can select a different database from the list.  
  
5.  The **Recovery model** text box is for reference only.  You can perform a database backup for any recovery model (**FULL**, **BULK_LOGGED**, or **SIMPLE**).  
  
6.  In the **Backup type** drop-down list, select **Full**.  
  
     Note that after creating a full database backup, you can create a differential database backup; for more information, see [Create a Differential Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-differential-database-backup-sql-server.md).  
  
7.  Optionally, you can select the **Copy-only backup** checkbox to create a copy-only backup. A *copy-only backup* is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup that is independent of the sequence of conventional [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backups. For more information, see [Copy-Only Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/copy-only-backups-sql-server.md).  A copy-only backup is not available for the **Differential** backup type.  

8.  For **Backup component**, select the **Database** radio button.  
  
9. In the **Destination** section, use the **Back up to** drop-down list to select the backup destination. Click **Add** to add additional backup ojects and/or destinations.
  
     To remove a backup destination, select it and click **Remove**. To view the contents of an existing backup destination, select it and click **Contents**.  

  #### **Media Options Page**  
10. To view or select the media options, click **Media Options** in the **Select a page** pane.   
    
11. Select an **Overwrite Media** option, by clicking one of the following: 

    > [!IMPORTANT]  
    >  The **Overwrite media** option is disabled if you selected **URL** as the backup destination in the **General** page. For more information, see [Back Up Database &#40;Media Options Page&#41;](../../relational-databases/backup-restore/back-up-database-media-options-page.md)  


  -   **Back up to the existing media set**  
  
      > [!IMPORTANT]  
      >  If you plan to use encryption, do not select this option. If you select this option, the encryption options in the **Backup Options** page will be disabled. Encryption is not supported when appending to the existing backup set.  
  
         For this option, click either **Append to the existing backup set** or **Overwrite all existing backup sets**. For more information, see [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](../../relational-databases/backup-restore/media-sets-media-families-and-backup-sets-sql-server.md).  
  
         Optionally, select **Check media set name and backup set expiration** to cause the backup operation to verify the date and time at which the media set and backup set expire.  
  
         Optionally, enter a name in the **Media set name** text box. If no name is specified, a media set with a blank name is created. If you specify a media set name, the media (tape or disk) is checked to see whether the actual name matches the name you enter here.  
  
-   **Back up to a new media set, and erase all existing backup sets**  
  
    For this option, enter a name in the **New media set name** text box, and, optionally, describe the media set in the **New media set description** text box.  
  
14. In the **Reliability** section, optionally check:  
  
    -   **Verify backup when finished**.  
  
    -   **Perform checksum before writing to media**.  For information on checksums, see [Possible Media Errors During Backup and Restore &#40;SQL Server&#41;](../../relational-databases/backup-restore/possible-media-errors-during-backup-and-restore-sql-server.md).  
    
    -   **Continue on error**. 

15. The **Transaction log** section is inactive unless you are backing up a transaction log (as specified in the **Backup type** section of the **General** page).  
      
16. In the **Tape drive** section, the **Unload the tape after backup** option is active if you are backing up to a tape drive (as specified in the **Destination** section of the **General** page). Clicking this option activates the **Rewind the tape before unloading** option.   

  #### **Backup Options Page**  

17. To view or select the backup options, click **Backup Options** in the **Select a page** pane.  
  
18. In the **Name** text box either accept the default backup set name, or enter a different name for the backup set.  
  
19. In the **Description** text box, you can optionally enter a description of the backup set.  
  
20. Specify when the backup set will expire and can be overwritten without explicitly skipping verification of the expiration data:  
  
    -   To have the backup set expire after a specific number of days, click **After** (the default option), and enter the number of days after set creation that the set will expire. This value can be from 0 to 99999 days; a value of 0 days means that the backup set will never expire.  
  
         The default value is set in the **Default backup media retention (in days)** option of the **Server Properties** dialog box (Database Settings Page). To access this, right-click the server name in Object Explorer and select properties; then select the **Database Settings** page.  
  
    -   To have the backup set expire on a specific date, click **On**, and enter the date on which the set will expire.  
  
         For more information about backup expiration dates, see [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md).  
  
21. In the **Compression** section, use the **Set backup compression** drop-down list to select the desired compression level.  [!INCLUDE[ssEnterpriseEd10](../../includes/ssenterpriseed10-md.md)] and later supports [backup compression](../../relational-databases/backup-restore/backup-compression-sql-server.md). By default, whether a backup is compressed depends on the value of the **backup-compression default** server configuration option. However, regardless of the current server-level default, you can compress a backup by checking **Compress backup**, and you can prevent compression by checking **Do not compress backup**.  
  
     For more information on backup compression settings, see [View or Configure the backup compression default Server Configuration Option](../../database-engine/configure-windows/view-or-configure-the-backup-compression-default-server-configuration-option.md)  
  
22. In the **Encryption** section, use the **Encrypt backup** checkbox to decide whether to use encryption for the backup. Use the **Algorithm** drop-down list to select an encryption algorithm.  Use the **Certificate or Asymmetric key** drop-down list, to select an existing Certificate or Asymmetric key. Encryption is supported in SQL Server 2014 or later. For more details on the Encryption options, see [Back Up Database &#40;Backup Options Page&#41;](../../relational-databases/backup-restore/back-up-database-backup-options-page.md).  
  
  
You can use the [Maintenance Plan Wizard](../maintenance-plans/use-the-maintenance-plan-wizard.md) to create database backups. 

### Examples  
#### **A.  Full back up to disk to default location**
In this example the `Sales` database will be backed up to disk at the default backup location.  A back up of `Sales` has never been taken.
1.  In **Object Explorer**, connect to an instance of the SQL Server Database Engine and then expand that instance.

2.  Expand **Databases**, right-click `Sales`, point to **Tasks**, and then click **Back Up...**.

3.  Click **OK**.

#### **B.  Full back up to disk to non-default location**
In this example the `Sales` database will be backed up to disk at `E:\MSSQL\BAK`.  Previous back ups of `Sales` have been taken.
1.  In **Object Explorer**, connect to an instance of the SQL Server Database Engine and then expand that instance.

2.  Expand **Databases**, right-click `Sales`, point to **Tasks**, and then click **Back Up...**.

3.  On the **General** page in the **Destination** section select **Disk** from the **Back up to:** drop-down list.

4.  Click **Remove** until all existing backup files have been removed.

5.  Click **Add** and the **Select Backup Destination** dialog box will open.

6.  Enter `E:\MSSQL\BAK\Sales_20160801.bak` in the **file name** text box.

7.  Click **OK**.

8.  Click **OK**.

#### **C.  Create an encrypted backup**
In this example the `Sales` database will be backed up with encryption to the default backup location.  A  [**database master key**](../../relational-databases/security/encryption/create-a-database-master-key.md) has already been created.  A  [**certificate**](../../t-sql/statements/create-certificate-transact-sql.md) has already been created called `MyCertificate`. A T-SQL example of creating a **database master key** and **certificate** can be seen at [Create an Encrypted Backup](../../relational-databases/backup-restore/create-an-encrypted-backup.md).  
1.  In **Object Explorer**, connect to an instance of the SQL Server Database Engine and then expand that instance.

2.  Expand **Databases**, right-click `Sales`, point to **Tasks**, and then click **Back Up...**.

3.  On the **Media Options** page in the **Overwrite media** section select **Back up to a new media set, and erase all existing backup sets**.

4.  On the **Backup Options** page in the **Encryption** section select the **Encrypt backup** check box.

5.  From the **Algorithm** drop-down list select **AES 256**.

6.  From the **Certificate or Asymmetric key** drop-down list select `MyCertificate`.

7.  Click **OK**.

#### **D.  Back up to the Azure Blob storage service**
#### **Common Steps**  
The three examples below perform a full database backup of `Sales` to the Microsoft Azure Blob storage service.  The storage Account name is `mystorageaccount`.  The container is called `myfirstcontainer`.  For brevity, the first four steps are listed here once and all examples will start on **Step 5**.
1.  In **Object Explorer**, connect to an instance of the SQL Server Database Engine and then expand that instance.

2.  Expand **Databases**, right-click `Sales`, point to **Tasks**, and then click **Back Up...**.

3.  On the **General** page in the **Destination** section select **URL** from the **Back up to:** drop-down list.

4.  Click **Add** and the **Select Backup Destination** dialog box will open.

    **D1.  Striped Backup to URL and a SQL Server credential already exists**  
A stored access policy has been created with read, write, and list rights.  The SQL Server credential, `https://mystorageaccount.blob.core.windows.net/myfirstcontainer`, was created using a Shared Access Signature that is associated with the Stored Access Policy.  
*
    5.	Select `https://mystorageaccount.blob.core.windows.net/myfirstcontainer` from the **Azure storage container:** text box

   6.  In the **Backup File:** text box enter `Sales_stripe1of2_20160601.bak`.

   7.  Click **OK**.

   8.  Repeat Steps **4** and **5**.

   9.  In the **Backup File:** text box enter `Sales_stripe2of2_20160601.bak`.

   10.  Click **OK**.

   11.   Click **OK**.

   **D2.  A shared access signature exists and a SQL Server Credential does not exist**
  5.	Enter `https://mystorageaccount.blob.core.windows.net/myfirstcontainer` in the **Azure storage container:** text box
  
  6.	Enter the shared access signature in the **Shared Access Policy:** text box.
  
  7.	Click **OK**.
  
  8.	Click **OK**.

    **D3.  A shared access signature does not exist**
  5.	Click the **New container** button and the **Connect to a Microsoft Subscription** dialog box will open.  
  
  6.	Complete the **Connect to a Microsoft Subscription** dialog box and then click **OK** to return the **Select Backup Destination** dialog box.  See [See Connect to a Microsoft Azure Subscription](../../relational-databases/backup-restore/connect-to-a-microsoft-azure-subscription.md) for additional information.
  
  7.	Click **OK** at the **Select Backup Destination** dialog box.
  
  8.	Click **OK**.

  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
### Create a full database backup  
  
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
    |*backup_device* [ **,**...*n* ]|Specifies a list of from 1 to 64 backup devices to use for the backup operation. You can specify a physical backup device, or you can specify a corresponding logical backup device, if already defined. To specify a physical backup device, use the DISK or TAPE option:<br /><br /> { DISK &#124; TAPE } **=**_physical\_backup\_device\_name_<br /><br /> For more information, see [Backup Devices &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-devices-sql-server.md).|  
    |WITH *with_options* [ **,**...*o* ]|Optionally, specifies one or more additional options, *o*. For information about some of the basic with options, see step 2.|  
  
2.  Optionally, specify one or more WITH options. A few basic WITH options are described here. For information about all the WITH options, see [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md).  
  
    -   Basic backup set WITH options:  
  
         { COMPRESSION | NO_COMPRESSION }  
         In [!INCLUDE[ssEnterpriseEd10](../../includes/ssenterpriseed10-md.md)] and later only, specifies whether [backup compression](../../relational-databases/backup-restore/backup-compression-sql-server.md) is performed on this backup, overriding the server-level default.  
  
         ENCRYPTION (ALGORITHM,  SERVER CERTIFICATE |ASYMMETRIC KEY)  
         In SQL Server 2014 or later only, specify the encryption algorithm to use, and the Certificate or Asymmetric key to use to secure the encryption.  
  
         DESCRIPTION **=** { **'**_text_**'** | **@**_text\_variable_ }  
         Specifies the free-form text that describes the backup set. The string can have a maximum of 255 characters.  
  
         NAME **=** { *backup_set_name* | **@**_backup\_set\_name\_var_ }  
         Specifies the name of the backup set. Names can have a maximum of 128 characters. If NAME is not specified, it is blank.  
  
    -   Basic backup set WITH options:  
  
         By default, BACKUP appends the backup to an existing media set, preserving existing backup sets. To explicitly specify this, use the NOINIT option. For information about appending to existing backup sets, see [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](../../relational-databases/backup-restore/media-sets-media-families-and-backup-sets-sql-server.md).  
  
         Alternatively, to format the backup media, use the FORMAT option:  
  
         FORMAT [ **,** MEDIANAME**=** { *media_name* | **@**_media\_name\_variable_ } ] [ **,** MEDIADESCRIPTION **=** { *text* | **@**_text\_variable_ } ]  
         Use the FORMAT clause when you are using media for the first time or you want to overwrite all existing data. Optionally, assign the new media a media name and description.  
  
        > [!IMPORTANT]  
        >  Use extreme caution when you are using the FORMAT clause of the BACKUP statement because this destroys any backups that were previously stored on the backup media.  
  
###  <a name="TsqlExample"></a> Examples (Transact-SQL)  
  
#### **A. Back up to a disk device**  
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
  
#### **B. Back up to a tape device**  
 The following example backs up the complete [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database to tape, appending the backup to the previous backups.  
  
```sql  
USE AdventureWorks2012;  
GO  
BACKUP DATABASE AdventureWorks2012  
   TO TAPE = '\\.\Tape0'  
   WITH NOINIT,  
      NAME = 'Full Backup of AdventureWorks2012';  
GO  
```  
  
#### **C. Back up to a logical tape device**  
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
Use the **Backup-SqlDatabase** cmdlet. To explicitly indicate that this is a full database backup, specify the **-BackupAction**  parameter with its default value, **Database**. This parameter is optional for full database backups.  

### Examples
#### **A.  Full local backup**  
The following example creates a full database backup of the `MyDB` database to the default backup location of the server instance `Computer\Instance`. Optionally, this example specifies **-BackupAction Database**.  
```powershell 
Backup-SqlDatabase -ServerInstance Computer\Instance -Database MyDB -BackupAction Database  
```
 
#### **B.  Full backup to Microsoft Azure**  
The following example creates a full backup of the database `Sales` on the `MyServer` instance to the Microsoft Azure Blob Storage service.  A stored access policy has been created with read, write, and list rights.  The SQL Server credential, `https://mystorageaccount.blob.core.windows.net/myfirstcontainer`, was created using a Shared Access Signature that is associated with the Stored Access Policy.  The PowerShell command uses the **BackupFile** parameter to specify the location (URL) and the backup file name.

```powershell  
import-module sqlps;
$container = 'https://mystorageaccount.blob.core.windows.net/myfirstcontainer';
$FileName = 'Sales.bak';
$database = 'Sales';
$BackupFile = $container + '/' + $FileName ;
  
Backup-SqlDatabase -ServerInstance "MyServer" -Database $database -BackupFile $BackupFile;
```
 
 **To set up and use the SQL Server PowerShell provider**  
  
-   [SQL Server PowerShell Provider](../../relational-databases/scripting/sql-server-powershell-provider.md)  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Back Up a Database (SQL Server)](../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md)  
  
-   [Create a Differential Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-differential-database-backup-sql-server.md)  
  
-   [Restore a Database Backup Using SSMS](../../relational-databases/backup-restore/restore-a-database-backup-using-ssms.md)  
  
-   [Restore a Database Backup Under the Simple Recovery Model &#40;Transact-SQL&#41;](../../relational-databases/backup-restore/restore-a-database-backup-under-the-simple-recovery-model-transact-sql.md)  
  
-   [Restore a Database to the Point of Failure Under the Full Recovery Model &#40;Transact-SQL&#41;](../../relational-databases/backup-restore/restore-database-to-point-of-failure-full-recovery.md)  
  
-   [Restore a Database to a New Location &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-a-database-to-a-new-location-sql-server.md)  
  
-   [Use the Maintenance Plan Wizard](../../relational-databases/maintenance-plans/use-the-maintenance-plan-wizard.md)  
  
## See also  
**[Troubleshooting SQL Server backup and restore operations](https://support.microsoft.com/kb/224071)**          
[Backup Overview &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-overview-sql-server.md)   
 [Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/transaction-log-backups-sql-server.md)   
 [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](../../relational-databases/backup-restore/media-sets-media-families-and-backup-sets-sql-server.md)   
 [sp_addumpdevice &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addumpdevice-transact-sql.md)   
 [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
 [Back Up Database &#40;General Page&#41;](../../relational-databases/backup-restore/back-up-database-general-page.md)   
 [Back Up Database &#40;Backup Options Page&#41;](../../relational-databases/backup-restore/back-up-database-backup-options-page.md)   
 [Differential Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/differential-backups-sql-server.md)   
 [Full Database Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/full-database-backups-sql-server.md)  
  
  
