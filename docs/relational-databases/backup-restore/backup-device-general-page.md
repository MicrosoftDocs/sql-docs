---
title: "Backup Device (General Page) | Microsoft Docs"
description: In SQL Server, use the General page to specify or view the general properties of a logical backup device, including specifying the device name.
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.backupdevice.general.f1"
ms.assetid: c557e37d-319e-4adb-a0c1-94189b15d2ac
author: MashaMSFT
ms.author: mathoma
---
# Backup Device (General Page)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Use the **General** page to specify or view the general properties of a logical backup device.  
  
 **To use SQL Server Management Studio to view the contents of a backup device**  
  
-   [View the Contents of a Backup Tape or File &#40;SQL Server&#41;](../../relational-databases/backup-restore/view-the-contents-of-a-backup-tape-or-file-sql-server.md)  
  
-   [View the Properties and Contents of a Logical Backup Device &#40;SQL Server&#41;](../../relational-databases/backup-restore/view-the-properties-and-contents-of-a-logical-backup-device-sql-server.md)  
  
## Options  
 **Device name**  
 View the name of an existing logical backup device or specify the name of a new logical backup device.  
  
 **Tape**  
 View or select the destination tape device in the **Tape** list. This option is available only if a tape drive is attached to the computer that is running the instance of the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)].  
  
> [!NOTE]  
>  Tape backup devices on remote computers are not valid backup destinations.  
  
 **File**  
 View the destination file of an existing logical backup device, or specify a destination file for a new logical backup device.  
  
-   For an existing logical backup device, the path of the backup file is displayed. The **File** field is not editable, and the Browse button is unavailable.  
  
-   For a new logical backup device, you must supply the path of the backup file for which you are defining the logical backup device. This file does not have to exist yet.  
  
     To specify a local backup file, you can click the Browse button to the right of the **File** text box. Then, in the **Locate Database Files** dialog box, you can navigate to any location on any of the fixed drives on the computer running the server instance. If the backup file does not exist yet, you must enter the filename you want to use in the **File name** field of that dialog box.  
  
     Alternatively, you can edit the **File** field manually to override the default path, file name, and extension. To specify a remote file as your backup destination, enter its fully qualified universal naming convention (UNC) name. For more information, see [Backup Devices &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-devices-sql-server.md).  
  
    > [!IMPORTANT]  
    >  Backing up data over a network can be subject to network errors; therefore, we recommend that you verify the backup operation after it finishes. For more information, see [RESTORE VERIFYONLY &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-verifyonly-transact-sql.md).  
  
## Remarks  
 The backups on a set of one or more backup devices compose a single media set. A *media set* is an ordered collection of backup media, tapes or disk files, to which one or more backup operations have written using a fixed type and number of backup devices. For information about media sets, see [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](../../relational-databases/backup-restore/media-sets-media-families-and-backup-sets-sql-server.md).  
  
 The physical backup device corresponding to a logical backup device is initialized when the first backup in the media set is written to the logical backup device. If the physical backup device is a file that does not exist yet, it is created at that time.  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Define a Logical Backup Device for a Disk File &#40;SQL Server&#41;](../../relational-databases/backup-restore/define-a-logical-backup-device-for-a-disk-file-sql-server.md)  
  
-   [Define a Logical Backup Device for a Tape Drive &#40;SQL Server&#41;](../../relational-databases/backup-restore/define-a-logical-backup-device-for-a-tape-drive-sql-server.md)  
  
-   [Specify a Disk or Tape As a Backup Destination &#40;SQL Server&#41;](../../relational-databases/backup-restore/specify-a-disk-or-tape-as-a-backup-destination-sql-server.md)  
  
-   [Delete a Backup Device &#40;SQL Server&#41;](../../relational-databases/backup-restore/delete-a-backup-device-sql-server.md)  
  
-   [Set the Expiration Date on a Backup &#40;SQL Server&#41;](../../relational-databases/backup-restore/set-the-expiration-date-on-a-backup-sql-server.md)  
  
-   [View the Contents of a Backup Tape or File &#40;SQL Server&#41;](../../relational-databases/backup-restore/view-the-contents-of-a-backup-tape-or-file-sql-server.md)  
  
-   [View the Data and Log Files in a Backup Set &#40;SQL Server&#41;](../../relational-databases/backup-restore/view-the-data-and-log-files-in-a-backup-set-sql-server.md)  
  
-   [View the Properties and Contents of a Logical Backup Device &#40;SQL Server&#41;](../../relational-databases/backup-restore/view-the-properties-and-contents-of-a-logical-backup-device-sql-server.md)  
  
-   [Restore a Backup from a Device &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-a-backup-from-a-device-sql-server.md)  
  
## See Also  
 [Backup Devices &#40;SQL Server&#41;](../../relational-databases/backup-restore/backup-devices-sql-server.md)   
 [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](../../relational-databases/backup-restore/media-sets-media-families-and-backup-sets-sql-server.md)  
  
  
