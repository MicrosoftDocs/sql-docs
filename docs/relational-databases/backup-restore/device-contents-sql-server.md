---
title: "Device Contents (SQL Server) | Microsoft Docs"
description: In SQL Server, use the Device Contents dialog box to view the backup information describing the device, the media, the media set, and the backup set or sets.
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.bnrdevicecontents.f1"
ms.assetid: 95e1902e-8c7a-4830-bdf9-1a6aca414a24
author: MashaMSFT
ms.author: mathoma
---
# Device Contents (SQL Server)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Use this dialog box to view the backup information. This information describes the device, the media, the media set, and the backup set or sets.  
  
 **To use SQL Server Management Studio to view the contents of a backup device**  
  
-   [View the Contents of a Backup Tape or File &#40;SQL Server&#41;](../../relational-databases/backup-restore/view-the-contents-of-a-backup-tape-or-file-sql-server.md)  
  
-   [View the Properties and Contents of a Logical Backup Device &#40;SQL Server&#41;](../../relational-databases/backup-restore/view-the-properties-and-contents-of-a-logical-backup-device-sql-server.md)  
  
## Options  
 **Media**  
 A disk or set of tapes on which backup information is stored.  
  
 **Media sequence**  
 Lists the media sequence number, the family sequence number, and the mirror identifier, if any. The physical backup media are each tagged with a media sequence number that indicates the order in which the media were used. The initial backup medium is tagged with 1, the second (the first continuation tape) is tagged with 2, and so forth. When the backup set is restored, the media sequence numbers ensure that the operator that restores the backup mounts the correct media in the correct order.  
  
 **Created on**  
 Displays the media date.  
  
 **Media Set**  
 A media set is an ordered collection of backup media to which one or more backup operations have written using a constant number of backup devices.  
  
 **Name**  
 Displays the name of the media set.  
  
 **Description**  
 Displays the description media set.  
  
 **Media family count**  
 Displays the media family count. Each media set is a collection of one or more media families. All the output to a given single backup device (or group of mirrored backup devices) forms a single media family. Each media set contains one media family per separate device (or group of mirrored devices); for example, if a media set uses two nonmirrored backup devices, the media set contains two media families.  
  
 **Backup sets**  
 Displays information about the backup set or sets contained on the media. A backup set is the result of a successful backup operation whose content is distributed among the media on the set of backup devices.  
  
|Header|Values|  
|------------|------------|  
|**Name**|The name of the backup set.|  
|**Type**|The type of backup performed: Full, Differential or Transaction Log.|  
|**Component**|The backed-up component: Database, File, or *\<blank>* (for transaction logs).|  
|**Server**|The name of the instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] that performed the backup operation.|  
|**Database**|The name of the database that was backed up.|  
|**Position**|The position of the backup set in the volume.|  
|**Date**|The date and time when the backup operation finished, presented in the regional setting of the client.|  
|**Size**|The size of the backup set in bytes.|  
|**User Name**|The name of the user who performed the backup operation.|  
|**Expiration**|The date and time the backup set expires.|  
  
## See Also  
 [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](../../relational-databases/backup-restore/media-sets-media-families-and-backup-sets-sql-server.md)  
  
  
