---
title: "Backup Device (Media Contents Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: backup-restore
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.backupdevice.contents.f1"
ms.assetid: 5fc7bd22-b6d8-4af1-8a58-2e7d0b994d08
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Backup Device (Media Contents Page)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Use the **Backup Device** dialog box to view the backup information. This information describes the device, the media, the media set, and the backup set or sets.  
  
 **To use SQL Server Management Studio to view the contents of a backup device**  
  
-   [View the Contents of a Backup Tape or File &#40;SQL Server&#41;](../../relational-databases/backup-restore/view-the-contents-of-a-backup-tape-or-file-sql-server.md)  
  
-   [View the Properties and Contents of a Logical Backup Device &#40;SQL Server&#41;](../../relational-databases/backup-restore/view-the-properties-and-contents-of-a-logical-backup-device-sql-server.md)  
  
## Options  
 View information about the individual media, media set, and backup sets.  
  
 **Media**  
 A disk or set of tapes on which backup information is stored.  
  
 **Media sequence**  
 Lists the media sequence number, the family sequence number, and the mirror identifier, if any. The physical backup media are each tagged with a media sequence number that indicates the order in which the media were used. The initial backup media is tagged with 1, the second (the first continuation tape) is tagged with 2, and so forth. When the backup set is restored, the media sequence numbers ensure that the operator restoring the backup mounts the correct media in the correct order.  
  
 **Created on**  
 Displays the creation date and time of the media set.  
  
 **Media Set**  
 A media set is an ordered collection of backup media to which one or more backup operations have written by using a constant number of backup devices.  
  
 **Name**  
 Displays the name of the media set, if any.  
  
 **Description**  
 Displays the description of the media set, if any.  
  
 **Media family count**  
 Displays the number of families in the media set. Each media set is a collection of one or more media families. All the output to a given single backup device (or group of mirrored backup devices) forms a single media family. Each media set contains one media family per separate device (or group of mirrored devices); for instance, if a media set uses two non-mirrored backup devices, the media set contains two media families.  
  
 **Backup sets**  
 Displays information about the backup set or sets contained on the media. A backup set is the result of a successful backup operation, whose content is distributed among the media on the set of backup devices.  
  
|Header|Values|  
|------------|------------|  
|**Name**|The name of the backup set.|  
|**Type**|The backed-up object: Database, File, or *\<blank>* (for transaction logs).|  
|**Component**|The type of backup performed: Full, Differential or Transaction Log.|  
|**Server**|The name of the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] that performed the backup operation.|  
|**Database**|The name of the database that was backed up.|  
|**Position**|The position of the backup set in the volume.|  
|**Date**|The date and time when the backup operation finished, presented in the regional setting of the client.|  
|**Size**|The size of the backup set in bytes.|  
|**User Name**|The name of the user who performed the backup operation.|  
|**Expiration**|The date and time the backup set expires.|  
  
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
  
  
