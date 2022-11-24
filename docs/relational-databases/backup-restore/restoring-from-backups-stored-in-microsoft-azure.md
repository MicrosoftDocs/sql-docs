---
title: "Restoring From Backups Stored in Microsoft Azure | Microsoft Docs"
description: Understand considerations for restoring a SQL Server database using a backup stored in Azure Blob storage. 
ms.custom: ""
ms.date: 11/10/2022
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
ms.assetid: 6ae358b2-6f6f-46e0-a7c8-f9ac6ce79a0e
author: MashaMSFT
ms.author: mathoma
---
# Restoring From Backups Stored in Microsoft Azure
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic outlines the considerations when restoring a database using a backup stored in Azure Blob Storage. This applies to backups created either by using SQL Server Backup to URL backup or by [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)].  
  
 We recommend reviewing this topic if you have backups stored in Azure Blob Storage that you plan to restore, and then review the topics that describe the steps on how to restore a database which is the same for both on-premises and azure backups.  
  
## Overview  
 The tools and methods that are used to restore a database from an on-premises backup apply to restoring a database from a cloud backup.  The following sections describe these considerations and any differences you should know about when you use backups stored in Azure Blob Storage.  
  
### Using Transact-SQL  
  
-   Since SQL Server must connect to an external source to retrieve the backup files, SQL Credential is used to authenticate to the storage account. Consequently, the RESTORE statement requires WITH CREDENTIAL option. For more information, see [SQL Server Backup and Restore with Microsoft Azure Blob Storage](../../relational-databases/backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md).  
  
-   If you are using the [!INCLUDE[ss_smartbackup](../../includes/ss-smartbackup-md.md)] to manage your backups to the cloud, you can review all the available backups in the storage, by using the **smart_admin.fn_available_backups** system function. This system function returns all the available backups for a database in a table. As the results are returned in a table, you can filter or sort the results. For more information, see [managed_backup.fn_available_backups &#40;Transact-SQL&#41;](../../relational-databases/system-functions/managed-backup-fn-available-backups-transact-sql.md).  
  
### Using SQL Server Management Studio  
  
-   The restore task is used to restore a database using the SQL Server Management Studio. The backup media page now includes the **URL** option to show backup files stored in Azure Blob Storage. You also must provide the SQL Credential that is used to authenticate to the storage account. The **Backup sets to restore** grid is then populated with the available backups in the Azure Blob storage. For more information, see [Restoring from Azure storage Using SQL Server Management Studio](../../relational-databases/backup-restore/sql-server-backup-to-url.md#RestoreSSMS).  
  
### Optimizing Restores  
 To reduce restore write time, Add **perform volume maintenance tasks** user right to the SQL Server user account. For more information, see [Database File Initialization](../databases/database-instant-file-initialization.md). If restore is still slow with instant file initialization turned on, look at the size of the log file on the instance where the database was backed up. If the log is very large in size (multiple GBs), it would be expected that restore would be slow. During restore the log file must be zeroed which takes a significant amount of time. The initial restore of database transaction log files cannot benefit from instant file initialization.
  
 To reduce restore times it is recommended that you use compressed backups.  For backup sizes exceeding 25 GB, use [AzCopy utility](/archive/blogs/windowsazurestorage/azcopy-uploadingdownloading-files-for-windows-azure-blobs) to download to the local drive and then perform the restore. For other backup best practices and recommendations, see [SQL Server Backup to URL Best Practices and Troubleshooting](../../relational-databases/backup-restore/sql-server-backup-to-url-best-practices-and-troubleshooting.md).  
  
 You can also turn on Trace Flag 3051 when doing the restore to generate a detailed log. This log file is placed in the log directory, and is named using the format: BackupToUrl-\<instancename>-\<dbname>-action-\<PID>.log. The log file includes information about each round trip to Azure Storage including timing that can be helpful in diagnosing the issue.  
  
### Topics on Performing Restore Operations  
  
-   [Complete Database Restores &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/complete-database-restores-simple-recovery-model.md)  
  
-   [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)  
  
-   [Complete Database Restores &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/complete-database-restores-full-recovery-model.md)  
  
-   [File Restores &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/file-restores-simple-recovery-model.md)  
  
-   [File Restores &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/file-restores-full-recovery-model.md)  
  
-   [Piecemeal Restores &#40;SQL Server&#41;](../../relational-databases/backup-restore/piecemeal-restores-sql-server.md)  
  
