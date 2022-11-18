---
title: "Select Backup Destination | Microsoft Docs"
description: For SQL Server restore, use the Select Backup Destination dialog box to select a either a disk or a logical backup device as your backup destination. 
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.selectbackupdest.f1"
ms.assetid: f79e824b-1525-45de-8ede-513563af41b6
author: MashaMSFT
ms.author: mathoma
---
# Select Backup Destination
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Use the **Select Backup Destination** dialog box to select a device as your backup destination. A backup destination can be either a disk or a logical backup device.  
  
 **To use SQL Server Management Studio to back up a database**  
  
-   [Create a Full Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-full-database-backup-sql-server.md)  
  
-   [Create a Differential Database Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/create-a-differential-database-backup-sql-server.md)  
  
-   [Back Up Files and Filegroups &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-files-and-filegroups-sql-server.md)  
  
-   [Back Up a Transaction Log &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-a-transaction-log-sql-server.md)  
  
## Options  
 The options of this dialog box depend on whether you are selecting a destination on disk or on tape.  
  
 **Destination on disk**  
 To specify a backup destination, choose one of the following options.  
  
|Option|Description|  
|-|-|  
|**File name**|Choose this option to enter a local or remote file as the backup destination in the text box to:<br /><br /> Specify a local file, click the browse button to the right of the text box and navigate to a file on the fixed drives of the computer running the server, or enter the full path and file name directly; for example, `C:\Program Files\Microsoft SQL Server\MSSQL\Backup\AdventureWorksBackup.bak`.<br /><br /> Specify a remote file as your backup destination, enter its fully qualified universal naming convention (UNC) name. For more information, see [Backup Devices &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-devices-sql-server.md).<br /><br /> <br /><br /> **\*\* Important \*\*** Backing up data over a network can be subject to network errors; therefore, we recommend that you verify the backup operation after it finishes. For more information, see [RESTORE VERIFYONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md).|  
|**Backup device**|Choose this option to select a logical backup device.<br /><br /> Note: For information about how to create a disk backup device, see [Define a Logical Backup Device for a Disk File &#40;SQL Server&#41;](../../relational-databases/backup-restore/define-a-logical-backup-device-for-a-disk-file-sql-server.md).|  
  
 **Destination on tape**  
 Specify a backup destination on a tape drive physically connected to the computer running the server (that is, the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]). Choose one of the following options.  
  
|Option|Description|  
|-|-|  
|**Tape drive**|Choose this option to select a tape drive as the backup destination from the list of tape drives that are physically connected to the computer that is running the server instance.<br /><br /> Note: Tape backup devices on remote computers are not valid backup destinations.|  
|**Backup device**|Choose this option to select an existing logical backup device. These logical backup devices correspond to tape drives that are physically connected to the computer that is running the server instance.<br /><br /> Note: For information about how to create a tape backup device, see [Define a Logical Backup Device for a Tape Drive &#40;SQL Server&#41;](../../relational-databases/backup-restore/define-a-logical-backup-device-for-a-tape-drive-sql-server.md).|  
  
## See Also  
 [Backup Devices &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-devices-sql-server.md)   
 [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](../../relational-databases/backup-restore/media-sets-media-families-and-backup-sets-sql-server.md)  
  
  
