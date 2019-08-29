---
title: "Create a Full Database Backup (SQL Server) | Microsoft Docs"
ms.custom: "sqlfreshmay19"
ms.date: "05/29/2019"
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
---
# Create a Full Database Backup (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

This topic describes how to create a full database backup in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../includes/tsql-md.md)], or PowerShell.  
  

For information on SQL Server backup to the Azure Blob storage service, see [SQL Server Backup and Restore with Microsoft Azure Blob Storage Service](../../relational-databases/backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md) and [SQL Server Backup to URL](../../relational-databases/backup-restore/sql-server-backup-to-url.md).  
  
##  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   The BACKUP statement is not allowed in an explicit or implicit transaction.    
-   Backups created by more recent version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot be restored in earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].   
-   For an overview of, and deeper dive into, backup concepts and tasks, see [Backup Overview &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-overview-sql-server.md) before proceeding.  
  
## <a name="Recommendations"></a> Recommendations  
  
-   As a database increases in size full database backups take more time to complete, and require more storage space. For a large database, consider supplementing a full database backup with a series of [differential database backups](../../relational-databases/backup-restore/differential-backups-sql-server.md). For more information, see [SQL Server Backup to URL](../../relational-databases/backup-restore/sql-server-backup-to-url.md).    
-   Estimate the size of a full database backup by using the [sp_spaceused](../../relational-databases/system-stored-procedures/sp-spaceused-transact-sql.md) system stored procedure.    
-   By default, every successful backup operation adds an entry in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log and in the system event log. If you back up frequently, these success messages will accumulate quickly, resulting in huge error logs! This can make finding other messages difficult. In such cases, you can suppress these backup log entries by using trace flag 3226 if none of your scripts depend on those entries. For more information, see [Trace Flags &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).  
  
##  <a name="Security"></a> Security  
 TRUSTWORTHY is set to OFF on a database backup. For information about how to set TRUSTWORTHY to ON, see [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md).  
  
 Beginning with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] the **PASSWORD** and **MEDIAPASSWORD** options are discontinued for creating backups. You can still restore backups created with passwords.  
  
##  <a name="Permissions"></a> Permissions  
 BACKUP DATABASE and BACKUP LOG permissions default to members of the **sysadmin** fixed server role and the **db_owner** and **db_backupoperator** fixed database roles.  
  
 Ownership and permission problems on the backup device's physical file can interfere with a backup operation. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be able to read and write to the device; the account under which the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service runs **must** have write permissions. However, [sp_addumpdevice](../../relational-databases/system-stored-procedures/sp-addumpdevice-transact-sql.md), which adds an entry for a backup device in the system tables, does not check file access permissions. Such problems on the backup device's physical file may not appear until the physical resource is accessed when the backup or restore is attempted.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
>  When you specify a back up task by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], you can generate the corresponding [!INCLUDE[tsql](../../includes/tsql-md.md)] [BACKUP](../../t-sql/statements/backup-transact-sql.md) script by clicking the **Script** button and selecting a script destination.  
  
### Back up a database  
  
1.  After connecting to the appropriate instance of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], in **Object Explorer**, click the server name to expand the server tree.    
1.  Expand **Databases**, and either select a user database or expand **System Databases** and select a system database.    
1.  Right-click the database, point to **Tasks**, and then click **Back Up**. The **Back Up Database** dialog box appears.  
1. Select the **Database** from the drop-down list. 
1. In the **Backup type** drop-down list, select **Full**. 
1. Under **Backup component**, select **Database**. 
1. In the **Destination** section, use the **Back up to** drop-down list to select the backup destination. Click **Add** to add additional backup objects and/or destinations. To remove a backup destination, select it and click **Remove**. To view the contents of an existing backup destination, select it and click **Contents**.
1. (Optionally) Review the other available settings under the **Media Options** and **Backup Options** pages. For more information about the various backup options, see [General page](back-up-database-general-page.md), [Media options page](back-up-database-media-options-page.md), and [Backup options page](back-up-database-backup-options-page.md). 



### Additional information
- After creating a full database backup, you can create a differential database backup; for more information, see [Create a Differential Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-differential-database-backup-sql-server.md).  
- Optionally, you can select the **Copy-only backup** checkbox to create a copy-only backup. A *copy-only backup* is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup that is independent of the sequence of conventional [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backups. For more information, see [Copy-Only Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/copy-only-backups-sql-server.md).  A copy-only backup is not available for the **Differential** backup type.  
- The **Overwrite media** option may be disabled on the **Media Options** page if you are backing up to URL. 


## SSMS Examples  

For the following examples, create a test database with the following Transact-SQL code:

```sql
USE [master]
GO

CREATE DATABASE [SQLTestDB]
GO

USE [SQLTestDB]
GO
CREATE TABLE SQLTest (
	ID INT NOT NULL PRIMARY KEY,
	c1 VARCHAR(100) NOT NULL,
	dt1 DATETIME NOT NULL DEFAULT getdate()
)
GO


USE [SQLTestDB]
GO

INSERT INTO SQLTest (ID, c1) VALUES (1, 'test1')
INSERT INTO SQLTest (ID, c1) VALUES (2, 'test2')
INSERT INTO SQLTest (ID, c1) VALUES (3, 'test3')
INSERT INTO SQLTest (ID, c1) VALUES (4, 'test4')
INSERT INTO SQLTest (ID, c1) VALUES (5, 'test5')
GO

SELECT * FROM SQLTest
GO
```

### A. Full back up to disk to default location
In this example, the `SQLTestDB` database will be backed up to disk at the default backup location.  A backup of `SQLTestDB` has never been taken.
1.  In **Object Explorer**, connect to an instance of the SQL Server Database Engine and then expand that instance.
2.  Expand **Databases**, right-click `SQLTestDB`, point to **Tasks**, and then click **Back Up...**.
3.  Select **OK**.

![Take SQL backup](media/quickstart-backup-restore-database/backup-db-ssms.png) 
 

### **B.  Full back up to disk to non-default location**
In this example, the `SQLTestDB` database will be backed up to disk at `F:\MSSQL\BAK`.  Previous back ups of `SQLTestDB` have been taken.
1.  In **Object Explorer**, connect to an instance of the SQL Server Database Engine and then expand that instance.
2.  Expand **Databases**, right-click `Sales`, point to **Tasks**, and then click **Back Up...**.
3.  On the **General** page in the **Destination** section select **Disk** from the **Back up to:** drop-down list.
4.  Select **Remove** until all existing backup files have been removed.
5.  Select **Add** and the **Select Backup Destination** dialog box will open.
6.  Enter `F:\MSSQL\BAK\Sales_20160801.bak` in the **file name** text box.
7.  Select **OK**.
8.  Select **OK**.

![Change DB location](media/create-a-full-database-backup-sql-server/change-db-location.png)

### **C.  Create an encrypted backup**
In this example, the `SQLTestDB` database will be backed up with encryption to the default backup location.  

1.  In **Object Explorer**, connect to an instance of the SQL Server Database Engine and then expand that instance.
1. Open a **New Query** window and execute the following commands to create a [**database master key**](../../relational-databases/security/encryption/create-a-database-master-key.md) and a [**certificate**](../../t-sql/statements/create-certificate-transact-sql.md) within your `SQLTestDB` database. 

    ```sql
    USE [SQLTestDB]
    	
    -- Create the database master key
    CREATE MASTER KEY ENCRYPTION BY PASSWORD = '23987hxJ#KL95234nl0zBe';  
    
    
    -- Create the certificate
    CREATE CERTIFICATE MyCertificate   
    ENCRYPTION BY PASSWORD = 'pGFD4bb925DGvbd2439587y'  
    EXPIRY_DATE = '20201031';  
    GO  
    ```

1.  In **Object Explorer**, Expand **Databases**, right-click `SQLTestDB`, point to **Tasks**, and then click **Back Up...**.
1.  On the **Media Options** page, in the **Overwrite media** section select **Back up to a new media set, and erase all existing backup sets**.
1.  On the **Backup Options** page in the **Encryption** section select the **Encrypt backup** check box.
1.  From the Algorithm drop-down list, select **AES 256**.
1.  From the **Certificate or Asymmetric key** drop-down list select `MyCertificate`.
1.  Select **OK**.

![Encrypted backup](media/create-a-full-database-backup-sql-server/encrypted-backup.png)

### **D.  Back up to the Azure Blob storage service**


The three examples below perform a full database backup of `Sales` to the Microsoft Azure Blob storage service.  The storage Account name is `mystorageaccount`.  The container is called `myfirstcontainer`.  For brevity, the first four steps are listed here once and all examples will start on **Step 5**.

1.  In **Object Explorer**, connect to an instance of the SQL Server Database Engine and then expand that instance.
2.  Expand **Databases**, right-click `Sales`, point to **Tasks**, and then click **Back Up...**.
3.  On the **General** page in the **Destination** section select **URL** from the **Back up to:** drop-down list.
4.  Click **Add** and the **Select Backup Destination** dialog box will open.

#### Striped backup to URL and a SQL Server credential already exists

A stored access policy has been created with read, write, and list rights.  The SQL Server credential, `https://mystorageaccount.blob.core.windows.net/myfirstcontainer`, was created using a Shared Access Signature that is associated with the Stored Access Policy.  

   5.	Select `https://mystorageaccount.blob.core.windows.net/myfirstcontainer` from the **Azure storage container:** text box
   6.  In the **Backup File:** text box enter `Sales_stripe1of2_20160601.bak`.
   7.  Click **OK**.
   8.  Repeat Steps **4** and **5**.
   9.  In the **Backup File:** text box enter `Sales_stripe2of2_20160601.bak`.
   10.  Click **OK**.
   11.   Click **OK**.

#### A shared access signature exists and a SQL Server Credential does not exist

  5.	Enter `https://mystorageaccount.blob.core.windows.net/myfirstcontainer` in the **Azure storage container:** text box  
  6.	Enter the shared access signature in the **Shared Access Policy:** text box.  
  7.	Click **OK**.  
  8.	Click **OK**.


#### A shared access signature does not exist

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

Basic backup set WITH options:

- **{ COMPRESSION | NO_COMPRESSION }**: In [!INCLUDE[ssEnterpriseEd10](../../includes/ssenterpriseed10-md.md)] and later only, specifies whether [backup compression](../../relational-databases/backup-restore/backup-compression-sql-server.md) is performed on this backup, overriding the server-level default. 
- **ENCRYPTION (ALGORITHM,  SERVER CERTIFICATE |ASYMMETRIC KEY)**: In SQL Server 2014 or later only, specify the encryption algorithm to use, and the Certificate or Asymmetric key to use to secure the encryption.
- **DESCRIPTION** **=** { **'**_text_**'** | **@**_text\_variable_ }: Specifies the free-form text that describes the backup set. The string can have a maximum of 255 characters.  
- **NAME = { *backup_set_name* | **@**_backup\_set\_name\_var_ }**: Specifies the name of the backup set. Names can have a maximum of 128 characters. If NAME is not specified, it is blank. 
  
 
By default, BACKUP appends the backup to an existing media set, preserving existing backup sets. To explicitly specify this, use the NOINIT option. For information about appending to existing backup sets, see [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](../../relational-databases/backup-restore/media-sets-media-families-and-backup-sets-sql-server.md).  

Alternatively, to format the backup media, use the FORMAT option:  

FORMAT [ **,** MEDIANAME**=** { *media_name* | **@**_media\_name\_variable_ } ] [ **,** MEDIADESCRIPTION **=** { *text* | **@**_text\_variable_ } ]  
Use the FORMAT clause when you are using media for the first time or you want to overwrite all existing data. Optionally, assign the new media a media name and description.  

> [!IMPORTANT]  
>  Use extreme caution when you are using the FORMAT clause of the BACKUP statement because this destroys any backups that were previously stored on the backup media.  
  
##  <a name="TsqlExample"></a> Transact-SQL Examples  
For the following examples, create a test database with the following Transact-SQL code:

```sql
USE [master]
GO

CREATE DATABASE [SQLTestDB]
GO

USE [SQLTestDB]
GO
CREATE TABLE SQLTest (
	ID INT NOT NULL PRIMARY KEY,
	c1 VARCHAR(100) NOT NULL,
	dt1 DATETIME NOT NULL DEFAULT getdate()
)
GO


USE [SQLTestDB]
GO

INSERT INTO SQLTest (ID, c1) VALUES (1, 'test1')
INSERT INTO SQLTest (ID, c1) VALUES (2, 'test2')
INSERT INTO SQLTest (ID, c1) VALUES (3, 'test3')
INSERT INTO SQLTest (ID, c1) VALUES (4, 'test4')
INSERT INTO SQLTest (ID, c1) VALUES (5, 'test5')
GO

SELECT * FROM SQLTest
GO
```
  
#### **A. Back up to a disk device**  
 The following example backs up the complete `SQLTestDB` database to disk, by using `FORMAT` to create a new media set.  
  
```sql  
USE SQLTestDB;  
GO  
BACKUP DATABASE SQLTestDB  
TO DISK = 'Z:\SQLServerBackups\SQLTestDB.Bak'  
   WITH FORMAT,  
      MEDIANAME = 'Z_SQLServerBackups',  
      NAME = 'Full Backup of SQLTestDB';  
GO  
```  
  
#### **B. Back up to a tape device**  
 The following example backs up the complete `SQLTestDB` database to tape, appending the backup to the previous backups.  
  
```sql  
USE SQLTestDB;  
GO  
BACKUP DATABASE SQLTestDB  
   TO TAPE = '\\.\Tape0'  
   WITH NOINIT,  
      NAME = 'Full Backup of SQLTestDB';  
GO  
```  
  
#### **C. Back up to a logical tape device**  
 The following example creates a logical backup device for a tape drive. The example then backs up the complete SQLTestDB database to that device.  
  
```sql  
-- Create a logical backup device,   
-- SQLTestDB_Bak_Tape, for tape device \\.\tape0.  
USE master;  
GO  
EXEC sp_addumpdevice 'tape', 'SQLTestDB_Bak_Tape', '\\.\tape0'; USE SQLTestDB;  
GO  
BACKUP DATABASE SQLTestDB  
   TO SQLTestDB_Bak_Tape  
   WITH FORMAT,  
      MEDIANAME = 'SQLTestDB_Bak_Tape',  
      MEDIADESCRIPTION = '\\.\tape0',   
      NAME = 'Full Backup of SQLTestDB';  
GO  
```  
  
##  <a name="PowerShellProcedure"></a> Using PowerShell  
Use the **Backup-SqlDatabase** cmdlet. To explicitly indicate that this is a full database backup, specify the **-BackupAction**  parameter with its default value, **Database**. This parameter is optional for full database backups.  

## Powershell Examples

### A.  Full local backup 
The following example creates a full database backup of the `MyDB` database to the default backup location of the server instance `Computer\Instance`. Optionally, this example specifies **-BackupAction Database**.  
```powershell 
Backup-SqlDatabase -ServerInstance Computer\Instance -Database MyDB -BackupAction Database  
```
 
### B.  Full backup to Microsoft Azure 
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
- [Troubleshooting SQL Server backup and restore operations](https://support.microsoft.com/kb/224071)
- [Backup Overview &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-overview-sql-server.md)
- [Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/transaction-log-backups-sql-server.md)
- [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](../../relational-databases/backup-restore/media-sets-media-families-and-backup-sets-sql-server.md)
- [sp_addumpdevice &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addumpdevice-transact-sql.md)
- [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
- [Back Up Database &#40;General Page&#41;](../../relational-databases/backup-restore/back-up-database-general-page.md)
- [Back Up Database &#40;Backup Options Page&#41;](../../relational-databases/backup-restore/back-up-database-backup-options-page.md)
- [Differential Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/differential-backups-sql-server.md)
- [Full Database Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/full-database-backups-sql-server.md)  
  
  
