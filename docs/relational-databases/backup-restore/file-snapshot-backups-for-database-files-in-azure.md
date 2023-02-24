---
title: "File-Snapshot Backups for Database Files in Azure | Microsoft Docs"
description: SQL Server file-snapshot backup uses Azure snapshots to provide fast backups & quicker restores for database files stored using Azure Blob Storage.
ms.custom: ""
ms.date: "05/23/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
ms.assetid: 17a81fcd-8dbd-458d-a9c7-2b5209062f45
author: MashaMSFT
ms.author: mathoma
---
# File-Snapshot Backups for Database Files in Azure
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] File-snapshot backup uses Azure snapshots to provide nearly instantaneous backups and quicker restores for database files stored using Azure Blob Storage. This capability enables you to simplify your backup and restore policies. For more information on storing database files using Azure Blob Storage, see [SQL Server Data Files in Microsoft Azure](../../relational-databases/databases/sql-server-data-files-in-microsoft-azure.md).  
  
 ![snapshot backup architectural diagram](../../relational-databases/backup-restore/media/snapshotbackups.PNG "snapshot backup architectural diagram")  
  
 **Download**  
  
-   To download [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], go to  **[Evaluation Center](https://www.microsoft.com/evalcenter/evaluate-sql-server-2016)**.  
  
-   Have an Azure account?  Then go **[Here](https://azure.microsoft.com/services/virtual-machines/sql-server/)** to spin up a Virtual Machine with [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] already installed.  
  
## Using Azure snapshots to back up database files stored in Azure  
  
### What is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] file-snapshot backup  
 A file-snapshot backup consists of a set of Azure snapshots of the blobs containing the database files plus a backup file containing pointers to these file-snapshots. Each file-snapshot is stored in the container with the base blob. You can specify that the backup file itself to be written to URL, disk or tape. Backup to URL is recommended. For more information on backing up, see [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md) and on backing up to URL, see [SQL Server Backup to URL](../../relational-databases/backup-restore/sql-server-backup-to-url.md).  
  
 ![architecture of snapshot feature](../../relational-databases/backup-restore/media/snapshotbackups-flat.png "architecture of snapshot feature")  
  
 Deleting the base blob will invalidate the backup set and you are prevented from dropping a blob that contains file-snapshots (unless you expressly choose to delete a blob with all of its file-snapshots). Furthermore, dropping a database or a data file does not delete the base blob or any of its file-snapshots. Also, deleting the backup file does not delete any of the file-snapshots in the backup set. To delete a file-snapshot backup set, use the **sys.sp_delete_backup** system stored procedure.  
  
 **Full database backup:** Performing a full database backup using file-snapshot backup creates an Azure snapshot of each data and log file comprising the database, establishes the transaction log backup chain, and writes the location of the file-snapshots into the backup file.  
  
 **Transaction log backup:** Performing a transaction log backup using file-snapshot backup creates a file-snapshot of each database file (not just the transaction log), records the file-snapshot location information into the backup file, and truncates the transaction log file.  
  
> [!IMPORTANT]  
>  After the initial full backup that is required to establish the transaction log backup chain (which can be a file-snapshot backup), you only need to perform transaction log backups because each transaction log file-snapshot backup set contains file-snapshots of all database files and can be used to perform a database restore or a log restore. After the initial full database backup, you do not need additional full or differential backups because Azure Blob Storage handles the differences between each file-snapshot and the current state of the base blob for each database file.  
  
> [!NOTE]  
>  For a tutorial on using SQL Server 2016 with Microsoft Azure Blob Storage, see [Tutorial: Using Microsoft Azure Blob Storage with SQL Server 2016 databases](../tutorial-use-azure-blob-storage-service-with-sql-server-2016.md)  
  
### Restore using file-snapshot backups  
 Because each file-snapshot backup set contains a file-snapshot of each database file, a restore process requires at most adjacent two file-snapshot backup sets. This is true regardless of whether the backup set is from a full database backup or a log backup. This is very different than the restore process when using traditional streaming backup files to perform the restore process. With traditional streaming backup, the restore process requires the use of an entire chain of backup sets: the full backup, a differential backup and one or more transaction log backups. The recovery portion of the restore process remains the same regardless of whether the restore is using a file-snapshot backup or a streaming backup set.  
  
 **To the time of any backup set:** In order to perform a RESTORE DATABASE operation to restore a database to the time of a specific file-snapshot backup set, only the specific backup set is required, plus the base blobs themselves. Because you can use a transaction log file-snapshot backup set to perform a RESTORE DATABASE operation, you will typically use a transaction log backup set to perform this type of RESTORE DATABASE operation and rarely use a full database backup set. An example appears at the end of this topic demonstrating this technique.  
  
 **To a point in time between two file-snapshot backup sets:** In order to perform a RESTORE DATABASE operation to restore a database to a specific point in time between the time of two adjacent transaction log backup sets, only two transaction log backup sets are required (one before and one after the point in time to which you wish to restore the database). To accomplish this, you would perform a RESTORE DATABASE operation WITH NORECOVERY using the transactional log file-snapshot backup set from the earlier point in time and perform a RESTORE LOG operation WITH RECOVERY using the transaction log file-snapshot backup set from the later point in time and using the STOPAT argument to specify the point in time at which to stop the recovery from the transaction log backup. An example appears at the end of this topic demonstrating this technique.
  
### File-backup set maintenance  
 **Deleting a file-snapshot backup set:** You cannot overwrite a file-snapshot backup set using the FORMAT argument. The FORMAT argument is not permitted to avoid leaving orphaned file-snapshots that were created with the original file-snapshot backup. To delete a file-snapshot backup set, use the **sys.sp_delete_backup** system stored procedure. This stored procedure deletes the backup file and the file-snapshots that comprise the backup set. Using another method to delete a file-snapshot backup set may delete the backup file without deleting the file-snapshots in the backup set.  
  
 **Deleting orphaned backup file-snapshots:** You may have orphaned file-snapshots if the backup file was deleted without using the **sys.sp_delete_backup** system stored procedure or if a database or database file was dropped while the blob(s) containing the database or database file had backup file-snapshots associated with them. To identify file-snapshots that may be orphaned, use the **sys.fn_db_backup_file_snapshots** system function to list all file-snapshots of the database files. To identify the file-snapshots that are part of a specific file-snapshot backup set, use the RESTORE FILELISTONLY system stored procedure. You can then use the **sys.sp_delete_backup_file_snapshot** system stored procedure to delete an individual backup file-snapshot that was orphaned. Examples using this system function and these system stored procedures are at the end of this topic. For more information, see [sp_delete_backup &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/snapshot-backup-sp-delete-backup.md), [sys.fn_db_backup_file_snapshots &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-db-backup-file-snapshots-transact-sql.md), [sp_delete_backup_file_snapshot &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/snapshot-backup-sp-delete-backup-file-snapshot.md), and [RESTORE FILELISTONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md).  
  
### Considerations and Limitations  
 **Premium storage:** When using premium storage, the following limitations apply:  
  
-   The backup file itself cannot be stored using premium storage.  
  
-   The frequency of backups can be no shorter than 10 minutes.  
  
-   The maximum number of snapshots that you can store is 100.  
  
-   RESTORE WITH MOVE is required.  
  
-   For additional information about premium storage, see [Premium Storage: High-Performance Storage for Azure Virtual Machine Workloads](/azure/virtual-machines/disks-types)  
  
 **Single storage account:** The file-snapshot and destination blobs must use the same storage account.  
  
 **Bulk recovery model:** When using bulk-logged recovery mode and working with a transaction log backup containing minimally logged transactions, you cannot do a log restore (including point in time recovery) using the transaction log backup. Rather, you perform a database restore to time of the file-snapshot backup set. This limitation is identical to the limitation with streaming backup.  
  
 **Online Restore:** When using file-snapshot backups, you cannot perform an Online Restore. For more information about Online Restore, see [Online Restore &#40;SQL Server&#41;](../../relational-databases/backup-restore/online-restore-sql-server.md).  
  
 **Billing:** When using SQL Server file-snapshot backup, additional charges will be incurred as data changes. For more information, see [Understanding How Snapshots Accrue Charges](/rest/api/storageservices/Understanding-How-Snapshots-Accrue-Charges).  
  
 **Archival:** If you wish to archive a file-snapshot backup, you can archive to blob storage or to streaming backup. To archive to blob storage, copy the snapshots in the file-snapshot backup set into separate blobs. To archive to streaming backup, restore the file-snapshot backup as a new database and then perform a normal streaming backup with compression and/or encryption and archive it for as long as desired, independent of the base blobs.  
  
> [!IMPORTANT]  
>  Maintaining multiple file-snapshot backups has only a small performance overhead. However, maintaining an excessive number of file-snapshot backups can have an I/O performance impact on the database. We recommend you maintain only those file-snapshot backups necessary to support your recovery point objective.  
  
## Backing up the database and log using a file-snapshot backup  
 The example below uses file-snapshot backup to back up the AdventureWorks2016 sample database to URL.  
  
```  
-- To permit log backups, before the full database backup, modify the database   
-- to use the full recovery model.  
USE master;  
GO  
ALTER DATABASE AdventureWorks2016  
   SET RECOVERY FULL;  
GO  
-- Back up the full AdventureWorks2016 database.  
BACKUP DATABASE AdventureWorks2016   
TO URL = 'https://<mystorageaccountname>.blob.core.windows.net/<mycontainername>/AdventureWorks2016.bak'   
WITH FILE_SNAPSHOT;  
GO  
-- Back up the AdventureWorks2016 log using a time stamp in the backup file name.  
DECLARE @Log_Filename AS VARCHAR (300);  
SET @Log_Filename = 'https://<mystorageaccountname>.blob.core.windows.net/<mycontainername>/AdventureWorks2016_Log_'+   
REPLACE (REPLACE (REPLACE (CONVERT (VARCHAR (40), GETDATE (), 120), '-','_'),':', '_'),' ', '_') + '.trn';  
BACKUP LOG AdventureWorks2016  
 TO URL = @Log_Filename WITH FILE_SNAPSHOT;  
GO  
```  
  
## Restoring from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] file-snapshot backup  
 The following example restores the AdventureWorks2016 database using a transaction log file-snapshot backup set, and shows a recovery operation. Notice that you can restore a database from a single transactional log file-snapshot backup set.  
  
```  
RESTORE DATABASE AdventureWorks2016 FROM URL = 'https://<mystorageaccountname>.blob.core.windows.net/<mycontainername>/AdventureWorks2016_2015_05_18_16_00_00.trn'   
WITH RECOVERY, REPLACE;  
GO  
```  
  
## Restoring from a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] file-snapshot backup to a point in time  
 The following example restores the AdventureWorks2016 to its state at a specified point in time using two transaction log file-snapshot backup sets, and shows a recovery operation.  
  
```  
RESTORE DATABASE AdventureWorks2016 FROM URL = 'https://<mystorageaccountname>.blob.core.windows.net/<mycontainername>/AdventureWorks2016_2015_05_18_16_00_00.trn'   
WITH NORECOVERY,REPLACE;  
GO   
  
RESTORE LOG AdventureWorks2016 FROM URL = 'https://<mystorageaccountname>.blob.core.windows.net/<mycontainername>/AdventureWorks2016_2015_05_18_18_00_00.trn'   
WITH RECOVERY,STOPAT = 'May 18, 2015 5:35 PM';  
GO  
```  
  
## Deleting a database file-snapshot backup set  
 To delete a file-snapshot backup set, use the **sys.sp_delete_backup** system stored procedure. Specify the database name to have the system verify that the specified file-snapshot backup set is a indeed a backup for the database specified. If no database name is specified, the specified backup set with its file-snapshots will be deleted without such a validation. For more information, see [sp_delete_backup &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/snapshot-backup-sp-delete-backup.md).  
  
> [!WARNING]  
>  Attempting to delete a file-snapshot backup set using another method, such as the Microsoft Azure Management Portal or the Azure Storage viewer in SQL Server Management Studio will not delete the file-snapshots in the backup set. These tools will only delete the backup file itself that contains the pointers to the file-snapshots in the file-snapshot backup set. To identify backup file-snapshots that remain after a backup file was improperly deleted, use the **sys.fn_db_backup_file_snapshots** system function and then use the **sys.sp_delete_backup_file_snapshot** system stored procedure to delete an individual backup file-snapshot.  
  
 The following example deletes the specified file-snapshot backup set, including the backup file and the file-snapshots comprising the specified backup set.  
  
```  
sys.sp_delete_backup 'https://<mystorageaccountname>.blob.core.windows.net/<mycontainername>/AdventureWorks2016.bak', 'adventureworks2016' ;  
GO  
  
```  
  
## Viewing database backup file-snapshots  
 To view file-snapshots of the base blob for each database file, use the **sys.fn_db_backup_file_snapshots** system function. This system function enables you to view all backup file-snapshots of each base blob for a database stored using Azure Blob Storage. A primary use case for this function is to identify backup file-snapshots of a database that remain when the backup file for a file-snapshot backup set is deleted using a mechanism other than the **sys.sp_delete_backup** system stored procedure. To determine the backup file-snapshots that are part of intact backup sets and the ones that are not part of intact backup sets, use the **RESTORE FILELISTONLY**  system stored procedure to list the file-snapshots belonging to each backup file. For more information, see [sys.fn_db_backup_file_snapshots &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-db-backup-file-snapshots-transact-sql.md) and [RESTORE FILELISTONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md).  
  
 The following example returns the list of all backup file-snapshots for the specified database.  
  
```  
--Either specify the database name or set the database context  
USE AdventureWorks2016  
select * from sys.fn_db_backup_file_snapshots (null) ;  
GO  
select * from sys.fn_db_backup_file_snapshots ('AdventureWorks2016') ;  
GO  
  
```  
  
## Deleting an individual database backup file-snapshot  
 To delete an individual backup file-snapshot of a database base blob, use the **sys.sp_delete_backup_file_snapshot** system stored procedure. A primary use case for this system stored procedure is to delete orphaned file-snapshot files that remain after a backup file was deleted using a method other than the **sys.sp_delete_backup** system stored procedure. For more information, see [sp_delete_backup_file_snapshot &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/snapshot-backup-sp-delete-backup-file-snapshot.md).  
  
> [!WARNING]  
>  Deleting an individual file-snapshot that is part of a file-snapshot backup set will invalidate the backup set.  
  
 The following example deletes the specified backup file-snapshot. The URL for the specified backup was obtained using the **sys.fn_db_backup_file_snapshots** system function.  
  
```  
sys.sp_delete_backup_file_snapshot N'adventureworks2016', N'https://<mystorageaccountname>.blob.core.windows.net/<mycontainername>/AdventureWorks2016Data.mdf?snapshot=2015-05-29T21:31:31.6502195Z';  
GO  
```  
  
## See Also  
 [Tutorial: Using Microsoft Azure Blob Storage with SQL Server 2016 databases](../tutorial-use-azure-blob-storage-service-with-sql-server-2016.md)  
  
