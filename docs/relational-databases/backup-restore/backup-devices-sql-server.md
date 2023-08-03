---
title: "Backup Devices (SQL Server)"
description: This article describes backup devices for a SQL Server database, including terminology and working with backup devices.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/17/2022
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
helpviewer_keywords:
  - "tape backup devices, about tape backup devices"
  - "backup devices [SQL Server]"
  - "disk backup devices [SQL Server]"
  - "database backups [SQL Server], backup devices"
  - "logical devices [SQL Server]"
  - "backup devices [SQL Server], about backup devices"
  - "backing up [SQL Server], backup devices"
  - "removable media [SQL Server]"
  - "network shares [SQL Server]"
  - "backups [SQL Server], backup devices"
  - "tape backup devices"
  - "physical devices [SQL Server]"
  - "backing up databases [SQL Server], backup devices"
  - "devices [SQL Server]"
---
# Backup Devices (SQL Server)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  During a backup operation on a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, the backed up data (the *backup*) is written to a physical backup device. This physical backup device is initialized when the first backup in a media set is written to it. Backups on a set of one or more backup devices compose a single media set.  
   
##  <a name="TermsAndDefinitions"></a> Terms and definitions  
 backup disk  
 A hard disk or other disk storage media that contains one or more backup files. A backup file is a regular operating system file.  
  
 media set  
 An ordered collection of backup media, tapes or disk files, that uses a fixed type and number of backup devices. For more information about media sets, see [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](../../relational-databases/backup-restore/media-sets-media-families-and-backup-sets-sql-server.md).  
  
 physical backup device  
 Either a tape drive or a disk file that is provided by the operating system. A backup can be written to from 1 to 64 backup devices. If a backup requires multiple backup devices, the devices all must correspond to a single type of device (disk or tape).  
  
 SQL Server Backups can also be written to Azure Blob Storage in addition to disk or tape.  
 
  
##  <a name="DiskBackups"></a> Using disk backup devices  
  
 If a disk file fills while a backup operation is appending a backup to the media set, the backup operation fails. The maximum size of a backup file is determined by the free disk space available on the disk device; therefore, the appropriate size for a backup disk device depends on the size of your backups.  
  
 A disk backup device could be a simple disk device, such as an ATA drive. Alternatively, you could use a hot-swappable disk drive that would let you transparently replace a full disk on the drive with an empty disk. A backup disk can be a local disk on the server or a remote disk that is a shared network resource. For information about how to use a remote disk, see [Backing Up to a File on a Network Share](#NetworkShare), later in this topic.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] management tools are very flexible at handling disk backup devices because they automatically generate a time-stamped name on the disk file.  
  
> [!IMPORTANT]  
> We recommend that a backup disk be a different disk than the database data and log disks. This is necessary to make sure that you can access the backups if the data or log disk fails. 
>
>If database files and backup files are on the same device and the device fails, the database and backups will be unavailable. Also, putting the database and backup files on the separate devices optimizes the I/O performance for both the production use of the database and the writing of backups.
  
##  <a name="BackupFileUsingPhysicalName"></a> Specify a backup file using its physical name (Transact-SQL)  
 The basic [BACKUP](../../t-sql/statements/backup-transact-sql.md) syntax for specifying a backup file by using its physical device name is:  
  
 BACKUP DATABASE *database_name*  
  
 TO DISK **=** { **'**_physical_backup_device_name_**'** | **@**_physical_backup_device_name_var_ }  
  
 For example:  
  
```sql  
BACKUP DATABASE AdventureWorks2012   
   TO DISK = 'Z:\SQLServerBackups\AdventureWorks2012.bak';  
GO  
```  
  
 To specify a physical disk device in a [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md) statement, the basic syntax is:  
  
 RESTORE { DATABASE | LOG } *database_name*  
  
 FROM DISK **=** { **'**_physical_backup_device_name_**'** | **@**_physical_backup_device_name_var_ }  
  
 For example,  
  
```sql  
RESTORE DATABASE AdventureWorks2012   
   FROM DISK = 'Z:\SQLServerBackups\AdventureWorks2012.bak';   
```  
  
  
##  <a name="BackupFileDiskPath"></a> Specify the disk backup file path 
 When you are specifying a backup file, you should enter its full path and file name. If you specify only the file name or a relative path when you are backing up to a file, the backup file is put in the default backup directory. The default backup directory is C:\Program Files\Microsoft SQL Server\MSSQL.*n*\MSSQL\Backup, where *n* is the number of the server instance. Therefore, for the default server instance, the default backup directory is: C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Backup.  
  
 To avoid ambiguity, especially in scripts, we recommend that you explicitly specify the path of the backup directory in every DISK clause. However, this is less important when you are using Query Editor. In that case, if you are sure that the backup file resides in the default backup directory, you can omit the path from a DISK clause. For example, the following `BACKUP` statement backs up the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database to the default backup directory.  
  
```sql  
BACKUP DATABASE AdventureWorks2012   
   TO DISK = 'AdventureWorks2012.bak';  
GO  
```  
  
> [!NOTE]  
> The default location is stored in the **BackupDirectory** registry key under **HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL.n\MSSQLServer**.  
  
   
###  <a name="NetworkShare"></a> Back up to a network share file  
 For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to access a remote disk file, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account must have access to the network share. This includes having the permissions needed for backup operations to write to the network share and for restore operations to read from it. The availability of network drives and permissions depends on the context is which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service is running:  
  
-   To back up to a network drive when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running in a domain user account, the shared drive must be mapped as a network drive in the session where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running. If you start Sqlservr.exe from command line, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] sees any network drives you have mapped in your login session.  
  
-   When you run Sqlservr.exe as a service, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] runs in a separate session that has no relation to your login session. The session in which a service runs can have its own mapped drives, although it usually does not.  
  
-   You can connect with the network service account by using the computer account instead of a domain user. To enable backups from specific computers to a shared drive, grant access to the computer accounts. As long as the Sqlservr.exe process that is writing the backup has access, it is irrelevant whether the user sending the BACKUP command has access.  
  
    > [!IMPORTANT]  
    > Backing up data over a network can be subject to network errors; therefore, we recommend that when you are using a remote disk you verify the backup operation after it finishes. For more information, see [RESTORE VERIFYONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md).  
  
## Specify a Universal Naming Convention (UNC) name  
 To specify a network share in a backup or restore command, use the fully qualified universal naming convention (UNC) name of the file for the backup device. A UNC name has the form **\\\\**_Systemname_**\\**_ShareName_**\\**_Path_**\\**_FileName_.  
  
 For example:  
  
```sql  
BACKUP DATABASE AdventureWorks2012   
   TO DISK = '\\BackupSystem\BackupDisk1\AW_backups\AdventureWorksData.Bak';  
GO  
```  
  
 
##  <a name="TapeDevices"></a> Using tape devices  
  
> [!NOTE]  
> Support for tape backup devices will be removed in a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using this feature in new development work, and plan to modify applications that currently use this feature.  
   
 Backing up [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data to tape requires that the tape drive or drives be supported by the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows operating system. Additionally, for the given tape drive, we recommend that you use only tapes recommended by the drive manufacturer. For more information about how to install a tape drive, see the documentation for the Windows operating system.  
  
 When a tape drive is used, a backup operation may fill one tape and continue onto another tape. Each tape contains a media header. The first media used is called the *initial tape*. Each successive tape is known as a *continuation tape* and has a media sequence number that is one higher than the previous tape. For example, a media set associated with four tape devices contains at least four initial tapes (and, if the database does not fit, four series of continuation tapes). When appending a backup set, you must mount the last tape in the series. If the last tape is not mounted, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] scans forward to the end of the mounted tape and then requires that you change the tape. At that point, mount the last tape.  
  
 Tape backup devices are used like disk devices, with the following exceptions:  
  
-   The tape device must be connected physically to the computer that is running an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Backing up to remote tape devices is not supported.  
  
-   If a tape backup device is filled during the backup operation, but more data still must be written, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] prompts for a new tape and continues the backup operation after a new tape is loaded.  
  
##  <a name="BackupTapeUsingPhysicalName"></a> Specify a backup tape using its physical name (Transact-SQL)  
 The basic [BACKUP](../../t-sql/statements/backup-transact-sql.md) syntax for specifying a backup tape using the physical device name of the tape drive is:  
  
 BACKUP { DATABASE | LOG } *database_name*  
  
 TO TAPE **=** { **'**_physical_backup_device_name_**'** | **@**_physical_backup_device_name_var_ }  
  
 For example:  
  
```sql  
BACKUP LOG AdventureWorks2012   
   TO TAPE = '\\.\tape0';  
GO  
```  
  
 To specify a physical tape device in a [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md) statement, the basic syntax is:  
  
 RESTORE { DATABASE | LOG } *database_name*  
  
 FROM TAPE **=** { **'**_physical_backup_device_name_**'** | **@**_physical_backup_device_name_var_ }  
  
###  <a name="TapeOptions"></a> Tape-Specific BACKUP and RESTORE options (Transact-SQL)  
 To facilitate tape management, the BACKUP statement provides the following tape-specific options:  
  
-   { NOUNLOAD | **UNLOAD** }  
  
     You can control whether a backup tape is unloaded automatically from the tape drive after a backup or restore operation. UNLOAD/NOUNLOAD is a session setting that persists for the life of the session or until it is reset by specifying the alternative.  
  
-   { **REWIND** | NOREWIND }  
  
     You can control whether [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] keeps the tape remains open after the backup or restore operation or releases and rewinds the tape after it fills. The default behavior is to rewind the tape (REWIND).  
  
> [!NOTE]  
> For more information about the BACKUP syntax and arguments, see [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md). For more information about the RESTORE syntax and arguments, see [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md) and [RESTORE Arguments &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-arguments-transact-sql.md), respectively.  
  
###  <a name="OpenTapes"></a> Managing open tapes  
 To view a list of open tape devices and the status of mount requests, query the [sys.dm_io_backup_tapes](../../relational-databases/system-dynamic-management-views/sys-dm-io-backup-tapes-transact-sql.md) dynamic management view. This view shows all the open tapes. These include in-use tapes that are temporarily idle while they wait for the next BACKUP or RESTORE operation.  
  
 If a tape has been accidentally left open, the fastest way to release the tape is by using the following command: RESTORE REWINDONLY FROM TAPE **=**_backup_device_name_. For more information, see [RESTORE REWINDONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-rewindonly-transact-sql.md).  
  
  
## Using the Azure Blob Storage  
 SQL Server Backups can be written to Azure Blob Storage.  For more information on how to use Azure Blob Storage for your backups, see [SQL Server Backup and Restore with Microsoft Azure Blob Storage](../../relational-databases/backup-restore/sql-server-backup-and-restore-with-microsoft-azure-blob-storage-service.md).  
  
##  <a name="LogicalBackupDevice"></a> Use a logical backup device  
 A *logical backup device* is an optional, user-defined name that points to a specific physical backup device (a disk file or tape drive). A logical backup device lets you use indirection when referencing the corresponding physical backup device.  
  
 Defining a logical backup device involves assigning a logical name to a physical device. For example, a logical device, AdventureWorksBackups, could be defined to point to the Z:\SQLServerBackups\AdventureWorks2012.bak file or the \\\\.\tape0 tape drive. Backup and restore commands can then specify AdventureWorksBackups as the backup device, instead of DISK = 'Z:\SQLServerBackups\AdventureWorks2012.bak' or TAPE = '\\\\.\tape0'.  
  
 The logical device name must be unique among all the logical backup devices on the server instance. To view the existing logical device names, query the [sys.backup_devices](../../relational-databases/system-catalog-views/sys-backup-devices-transact-sql.md) catalog view. This view displays the name of each logical backup device and describes the type and physical file name or path of the corresponding physical backup device.  
  
 After a logical backup device is defined, in a BACKUP or RESTORE command, you can specify the logical backup device instead of the physical name of the device. For example, the following statement backs up the `AdventureWorks2012` database to the `AdventureWorksBackups` logical backup device.  
  
```sql  
BACKUP DATABASE AdventureWorks2012   
   TO AdventureWorksBackups;  
GO  
```  
  
> [!NOTE]  
> In a given BACKUP or RESTORE statement, the logical backup device name and the corresponding physical backup device name are interchangeable.  
  
 One advantage of using a logical backup device is that it is simpler to use than a long path. Using a logical backup device can help if you plan to write a series of backups to the same path or to a tape device. Logical backup devices are especially useful for identifying tape backup devices.  
  
 A backup script can be written to use a particular logical backup device. This lets you switch to a new physical backup devices without updating the script. Switching involves the following process:  
  
1.  Dropping the original logical backup device.  
  
2.  Defining a new logical backup device that uses the original logical device name but maps to a different physical backup device. Logical backup devices are especially useful for identifying tape backup devices.  

##  <a name="MirroredMediaSets"></a> Mirrored backup media sets  
 Mirroring of backup media sets reduces the effect of backup-device malfunctions. These malfunctions are especially serious because backups are the last line of defense against data loss. As the sizes of databases grow, the probability increases that a failure of a backup device or media will make a backup nonrestorable. Mirroring backup media increases the reliability of backups by providing redundancy for the physical backup device. For more information, see [Mirrored Backup Media Sets &#40;SQL Server&#41;](../../relational-databases/backup-restore/mirrored-backup-media-sets-sql-server.md).  
  
> [!NOTE]  
> Mirrored backup media sets are supported only in [!INCLUDE[ssEnterpriseEd2005](../../includes/ssenterpriseed2005-md.md)] and later versions.  
  
  
##  <a name="Archiving"></a> Archive SQL Server backups  
 We recommend that you use a file system backup utility to archive the disk backups and that you store the archives off-site. Using disk has the advantage that you use the network to write the archived backups onto an off-site disk. Azure Blob Storage can be used as off-site archival option.  You can either upload your disk backups, or directly write the backups to Azure Blob Storage.  
  
 Another common archiving approach is to write [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backups onto a local backup disk, archive them to tape, and then store the tapes off-site.  

  
##  <a name="RelatedTasks"></a> Related tasks  
 **To specify a disk device (SQL Server Management Studio)**  
  
-   [Specify a Disk or Tape As a Backup Destination &#40;SQL Server&#41;](../../relational-databases/backup-restore/specify-a-disk-or-tape-as-a-backup-destination-sql-server.md)  
  
 **To specify a tape device (SQL Server Management Studio)**  
  
-   [Specify a Disk or Tape As a Backup Destination &#40;SQL Server&#41;](../../relational-databases/backup-restore/specify-a-disk-or-tape-as-a-backup-destination-sql-server.md)  
  
 **To define a logical backup device**  
  
-   [sp_addumpdevice &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addumpdevice-transact-sql.md)  
  
-   [Define a Logical Backup Device for a Disk File &#40;SQL Server&#41;](../../relational-databases/backup-restore/define-a-logical-backup-device-for-a-disk-file-sql-server.md)  
  
-   [Define a Logical Backup Device for a Tape Drive &#40;SQL Server&#41;](../../relational-databases/backup-restore/define-a-logical-backup-device-for-a-tape-drive-sql-server.md)  
  
-   <xref:Microsoft.SqlServer.Management.Smo.BackupDevice> (SMO)  
  
 **To use a logical backup device**  
  
-   [Specify a Disk or Tape As a Backup Destination &#40;SQL Server&#41;](../../relational-databases/backup-restore/specify-a-disk-or-tape-as-a-backup-destination-sql-server.md)  
  
-   [Restore a Backup from a Device &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-a-backup-from-a-device-sql-server.md)  
  
-   [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)  
  
-   [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)  
  
 **To View Information About Backup Devices**  
  
-   [Backup History and Header Information &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-history-and-header-information-sql-server.md)  
  
-   [View the Properties and Contents of a Logical Backup Device &#40;SQL Server&#41;](../../relational-databases/backup-restore/view-the-properties-and-contents-of-a-logical-backup-device-sql-server.md)  
  
-   [View the Contents of a Backup Tape or File &#40;SQL Server&#41;](../../relational-databases/backup-restore/view-the-contents-of-a-backup-tape-or-file-sql-server.md)  
  
 **To delete a logical backup device**  
  
-   [sp_dropdevice &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropdevice-transact-sql.md)  
  
-   [Delete a Backup Device &#40;SQL Server&#41;](../../relational-databases/backup-restore/delete-a-backup-device-sql-server.md)  
  
## See also  
 [SQL Server, Backup Device Object](../../relational-databases/performance-monitor/sql-server-backup-device-object.md)   
 [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
 [Maintenance Plans](../../relational-databases/maintenance-plans/maintenance-plans.md)   
 [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](../../relational-databases/backup-restore/media-sets-media-families-and-backup-sets-sql-server.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)   
 [RESTORE LABELONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-labelonly-transact-sql.md)   
 [sys.backup_devices &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-backup-devices-transact-sql.md)   
 [sys.dm_io_backup_tapes &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-io-backup-tapes-transact-sql.md)   
 [Mirrored Backup Media Sets &#40;SQL Server&#41;](../../relational-databases/backup-restore/mirrored-backup-media-sets-sql-server.md)  
  
  
