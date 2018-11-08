---
title: "Select Backup Destination | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.selectbackupdest.f1"
ms.assetid: f79e824b-1525-45de-8ede-513563af41b6
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Select Backup Destination
  Use the **Select Backup Destination** dialog box to select a device as your backup destination. A backup destination can be either a disk or a logical backup device.  
  
 **To use SQL Server Management Studio to back up a database**  
  
-   [Create a Full Database Backup &#40;SQL Server&#41;](create-a-full-database-backup-sql-server.md)  
  
-   [Create a Differential Database Backup &#40;SQL Server&#41;](create-a-differential-database-backup-sql-server.md)  
  
-   [Back Up Files and Filegroups &#40;SQL Server&#41;](back-up-files-and-filegroups-sql-server.md)  
  
-   [Back Up a Transaction Log &#40;SQL Server&#41;](back-up-a-transaction-log-sql-server.md)  
  
## Options  
 The options of this dialog box depend on whether you are selecting a destination on disk or on tape.  
  
 **Destination on disk**  
 To specify a backup destination, choose one of the following options.  
  
|||  
|-|-|  
|**File name**|Choose this option to enter a local or remote file as the backup destination in the text box.<br /><br /> To specify a local file, click the browse button to the right of the text box and navigate to a file on the fixed drives of the computer running the server, or enter the full path and file name directly; for example, `C:\Program Files\Microsoft SQL Server\MSSQL\Backup\AdventureWorksBackup.bak`.<br /><br /> To specify a remote file as your backup destination, enter its fully qualified universal naming convention (UNC) name. For more information, see [Backup Devices &#40;SQL Server&#41;](backup-devices-sql-server.md).<br /><br /> **\*\* Important \*\*** Backing up data over a network can be subject to network errors; therefore, we recommend that you verify the backup operation after it finishes. For more information, see [RESTORE VERIFYONLY &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-verifyonly-transact-sql).|  
|**Backup device**|Choose this option to select a logical backup device.<br /><br /> Note: For information about how to create a disk backup device, see [Define a Logical Backup Device for a Disk File &#40;SQL Server&#41;](define-a-logical-backup-device-for-a-disk-file-sql-server.md).|  
  
 **Destination on tape**  
 Specify a backup destination on a tape drive physically connected to the computer running the server (that is, the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)]). Choose one of the following options.  
  
|||  
|-|-|  
|**Tape drive**|Choose this option to select a tape drive as the backup destination from the list of tape drives that are physically connected to the computer that is running the server instance.<br /><br /> Note: Tape backup devices on remote computers are not valid backup destinations.|  
|**Backup device**|Choose this option to select an existing logical backup device. These logical backup devices correspond to tape drives that are physically connected to the computer that is running the server instance.<br /><br /> Note: For information about how to create a tape backup device, see [Define a Logical Backup Device for a Tape Drive &#40;SQL Server&#41;](define-a-logical-backup-device-for-a-tape-drive-sql-server.md).|  
  
## See Also  
 [Backup Devices &#40;SQL Server&#41;](backup-devices-sql-server.md)   
 [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](media-sets-media-families-and-backup-sets-sql-server.md)  
  
  
