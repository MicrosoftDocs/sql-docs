---
title: "Backup Timeline | Microsoft Docs"
description: In SQL Server, the Backup Timeline dialog box allows you to locate and specify backups to restore a database to a point-in-time. 
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
f1_keywords: 
  - "sql13.SWB.POINTINTIMERESTORE.F1"
  - "sql13.swb.backuptimeline.f1"
helpviewer_keywords: 
  - "Backup Timeline"
ms.assetid: ae3565f2-ddb2-4469-a992-7531d4f9ebb8
author: MashaMSFT
ms.author: mathoma
---
# Backup Timeline
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Use the **Backup Timeline** dialog box to locate and specify backups to restore a database to a point-in-time. The **Backup Timeline** dialog box is accessed by clicking **Timeline** on the **Restore Database (General Page)** pane. This dialog box allows you to view a timeline of the restore operations performed on the database.  
  
 The Database Recovery Advisor ensures that only backups that are required for restoring to that point in time are selected. These selected backups make up the recommended restore plan for your restore operation. You should use only the selected backups. For information about the Database Recovery Advisor, see [Restore and Recovery Overview &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-and-recovery-overview-sql-server.md).  
  
## Restore to  
 **Last backup taken** is selected by default. [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] will select the appropriate backups to restore the database, and will restore the database to the point of the last backup. Click **A specific date and time** to manually set the date and time (selecting a specific point in time).  
  
 **Specific date and time** permits you stop the restore at a specific date and time that you select. The timeline shows a representation of the backup operations performed in the 24 hours around the select date and time.  
  
 **Date**  
 Enter or select a date from the drop-down list.  
  
 **Time**  
 Enter or select a date to designate the specific point-in-time for the restore to stop.  
  
 **Timeline Interval**  
 Displays the options for the interval types viewable on the timeline.  
  
## Timeline and Legend  
 Use the scroll bar beneath the timeline to move the cursor forward and backward along the timeline. Click on a backup to move the scroll bar to the end of that backup. Hover the mouse over a marker to display a ScreenTip providing information about the selected backup set. Backup information is shown on the timeline by the following markers.  
  
 Larger triangle  
 Represents the full backups performed on the database, denoting the specific point in time each full backup was performed.  
  
 Smaller triangle  
 Represents the differential backups performed on the database, denoting the specific point in time that each differential backup was performed.  
  
 Green shaded areas  
 Represents the transaction log backup coverage.  
  
 Red line  
 Can only be positioned along the timeline where the restore is possible. Moving the red line along the timeline adjusts the date and time displayed in the **Date** and **Time** boxes.  
  
## See Also  
 [Restore Database &#40;General Page&#41;](../../relational-databases/backup-restore/restore-database-general-page.md)  
  
  
