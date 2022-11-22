---
title: "Copy Databases with Backup and Restore | Microsoft Docs"
description: In SQL Server, you can create a new database by restoring a backup of a user database created by using some previous versions.
ms.custom: ""
ms.date: "07/15/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "full-text search [SQL Server], back up and restore"
  - "restoring databases [SQL Server], previous SQL Server versions"
  - "database restores [SQL Server], copying databases"
  - "restoring databases [SQL Server], onto another server instance"
  - "restoring [SQL Server], onto another server instance"
  - "backing up databases [SQL Server], copying databases"
  - "database backups [SQL Server], copying databases"
ms.assetid: b93e9701-72a0-408e-958c-dc196872c040
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Copy Databases with Backup and Restore
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  In [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)], you can create a new database by restoring a backup of a user database created by using [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or a later version. However, backups of **master**, **model** and **msdb** that were created by using an earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot be restored by [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]. Also, [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] backups cannot be restored by any earlier version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!IMPORTANT]
> SQL Server 2016 uses a different default path than earlier versions. Therefore, to restore backups of a database created in the default location of earlier versions you must use the MOVE option. For information about the new default path see [File Locations for Default and Named Instances of SQL Server](../../sql-server/install/file-locations-for-default-and-named-instances-of-sql-server.md). For more information about moving database files, see "Moving the Database Files," later in this topic.  
  
## General steps for using Backup and Restore to copy a database  
 When you use backup and restore to copy a database to another instance of SQL Server, the source and destination computers can be any platform on which SQL Server runs.  
  
 The general steps are:  
  
1.  Back up the source database, which can reside on an instance of [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or later. The computer on which this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running is the **source computer**.  
  
2.  On the computer to which you want to copy the database (the **destination computer**), connect to the instance of SQL Server on which you plan to restore the database. If needed, on the **destination** server instance, create the same backup devices as used to the backup of the **source** databases.  
  
3.  Restore the backup of the **source** database on the **destination** computer. Restoring the database automatically creates all of the database files.  
  
Some additional considerations that may affect this process:
  
## Before You restore database files  
 Restoring a database automatically creates the database files needed by the restoring database. By default, the files created by SQL Server during the restoration process use the same names and paths as the backup files from the original database on the source computer.  
  
 Optionally, when restoring the database, you can specify the device mapping, file names, or path for the restoring database. 
 
 This might be necessary in the following situations:  
  
-   The directory structure or drive mapping used by the database on the original computer not exist on the other computer. For example, perhaps the backup contains a file that would be restored to drive E by default, but the destination computer lacks a drive E.  
  
-   The target location might have insufficient space.  
  
-   You are reusing a database name that exists on the restore destination and any of its files is named the same as a database file in the backup set, one of the following occurs:  
  
    -   If the existing database file can be overwritten, it will be overwritten (this would not affect a file that belongs to a different database name).  
  
    -   If the existing file cannot be overwritten, a restore error would occur.  
  
 To avoid errors and unpleasant consequences, before the restore operation, you can use the [backupfile](../../relational-databases/system-tables/backupfile-transact-sql.md) history table to find out the database and log files in the backup you plan to restore.  
  
## Moving the database files  
 If the files within the database backup cannot be restored onto the destination computer, it is necessary to move the files to a new location while they are being restored. For example:  
  
-   You want to restore a database from backups created in the default location of the earlier version.  
  
-   It may be necessary to restore some of the database files in the backup to a different drive because of capacity considerations. This is a common occurrence because most computers within an organization do not have the same number and size of disk drives or identical software configurations.  
  
-   It may be necessary to create a copy of an existing database on the same computer for testing purposes. In this case, the database files for the original database already exist, so different file names must be specified when the database copy is created during the restore operation.  
  
 For more information, see "To restore files and filegroups to a new location," later in this topic.  
  
## Changing the database name  
 The name of the database can be changed as it is restored to the destination computer, without having to restore the database first and then change the name manually. For example, it may be necessary to change the database name from **Sales** to **SalesCopy** to indicate that this is a copy of a database.  
  
 The database name explicitly supplied when you restore a database is used automatically as the new database name. Because the database name does not already exist, a new one is created by using the files in the backup.  
  
## When upgrading a database by using Restore  
 When restoring backups from an earlier version, it is helpful to know in advance whether the path (drive and directory) of each of the full-text catalogs in a backup exists on the destination computer. To list the logical names and physical names, path and file name) of every file in a backup, including the catalog files, use a RESTORE FILELISTONLY FROM *<backup_device>* statement. For more information, see [RESTORE FILELISTONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md).  
  
 If the same path does not exist on the destination computer, you have two alternatives:  
  
-   Create the equivalent drive/directory mapping on the destination computer.  
  
-   Move the catalog files to a new location during the restore operation, by using the WITH MOVE clause in your RESTORE DATABASE statement. For more information, see [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md).  
  
 For information about alternative options for upgrading full-text indexes, see [Upgrade Full-Text Search](../../relational-databases/search/upgrade-full-text-search.md).  
  
## Database ownership  
 When a database is restored on another computer, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login or [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows user who initiates the restore operation becomes the owner of the new database automatically. When the database is restored, the system administrator or the new database owner can change database ownership. To prevent unauthorized restoration of a database, use media or backup set passwords.  
  
## Managing metadata when restoring to another server instance  
 When you restore a database onto another server instance, to provide a consistent experience to users and applications, you might have to re-create some or all of the metadata for the database, such as logins and jobs, on the other server instance. For more information, see [Manage Metadata When Making a Database Available on Another Server Instance &#40;SQL Server&#41;](../../relational-databases/databases/manage-metadata-when-making-a-database-available-on-another-server.md).  
  
 **View the data and log files in a backup set**  
  
-   [RESTORE FILELISTONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md)  
  
 **Restore files and filegroups to a new location**  
  
-   [Restore Files to a New Location &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-files-to-a-new-location-sql-server.md)  
  
-   [Restore a Database Backup Using SSMS](../../relational-databases/backup-restore/restore-a-database-backup-using-ssms.md)  
  
 **Restore files and filegroups over existing files**  
  
-   [Restore Files and Filegroups over Existing Files &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-files-and-filegroups-over-existing-files-sql-server.md)  
  
 **Restore a database with a new name**  
  
-   [Restore a Database Backup Using SSMS](../../relational-databases/backup-restore/restore-a-database-backup-using-ssms.md)  
  
 **Restart an interrupted restore operation**  
  
-   [Restart an Interrupted Restore Operation &#40;Transact-SQL&#41;](../../relational-databases/backup-restore/restart-an-interrupted-restore-operation-transact-sql.md)  
  
 **Change database owner**  
  
-   [sp_changedbowner &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-changedbowner-transact-sql.md)  
  
 **Copy a database by using SQL Server Management Objects (SMO)**  
  
-   <xref:Microsoft.SqlServer.Management.Smo.Restore.ReadFileList%2A>  
  
-   <xref:Microsoft.SqlServer.Management.Smo.Restore.RelocateFiles%2A>  
  
-   <xref:Microsoft.SqlServer.Management.Smo.Restore.ReplaceDatabase%2A>  
  
-   <xref:Microsoft.SqlServer.Management.Smo.Restore>  
  
## See also  
 [Copy Databases to Other Servers](../../relational-databases/databases/copy-databases-to-other-servers.md)   
 [File Locations for Default and Named Instances of SQL Server](../../sql-server/install/file-locations-for-default-and-named-instances-of-sql-server.md)   
 [RESTORE FILELISTONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-filelistonly-transact-sql.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)  
  
  
