---
title: "Plan and Perform Restore Sequences (Full Recovery Model) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "restore sequences [SQL Server], planning for"
  - "full recovery model [SQL Server], planning restore sequences"
ms.assetid: 9cbefaf8-d2b6-41c9-83fc-b3807a841fe2
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Plan and Perform Restore Sequences (Full Recovery Model)
  This topic explains how to plan and perform a restore sequence for a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases that ordinarily uses the full recovery model. A *restore sequence* is a sequence of one or more [RESTORE](/sql/t-sql/statements/restore-statements-transact-sql) statements. Typically, a restore sequences initializes the contents of the database, files, and/or pages being restored (the data-copy phase), rolls forward logged transactions (the redo phase), and rolls back uncommitted transactions (the undo phase).  
  
 In simple cases, a restore sequence requires only a full database backup, a differential database backup, and the subsequent log backups. In these cases, constructing a correct restore sequence is easy. For example, to restore a whole database to the point of a failure, start by backing up the active transaction log (the *tail* of the log). Then, restore the most recent full database backup, the most recent differential backup (if any), and all subsequent log backups in the order in which they were taken.  
  
 In more complex cases, constructing a correct restore sequence can be a complex process. For example, a restore sequence might require multiple file backups or restoring data to a specific point in time. In very complex cases, you might even have to traverse a forked recovery path that spans one or more recovery forks.  
  
> [!NOTE]  
>  A *recovery path* is the sequence of data and log backups that have brought a database to a particular point in time (known as a recovery point). A recovery path is a specific set of transformations that have evolved the database over time, yet have maintained the consistency of the database. A recovery path describes a range of LSNs from a start point (LSN,GUID) to an end point (LSN,GUID). The range of LSNs in a recovery path can traverse one or more recovery branches from start to end.  
  
## To Plan a Restore Sequence  
 Before you start a restore sequence, follow these steps:  
  
1.  Create a tail-log backup of the database, if you can. For more information, see [Tail-Log Backups &#40;SQL Server&#41;](tail-log-backups-sql-server.md).  
  
2.  Determine the target recovery point.  
  
     The target recovery point can be any point in time or mark within a transaction log backup. For more information, see [Restore a SQL Server Database to a Point in Time &#40;Full Recovery Model&#41;](restore-a-sql-server-database-to-a-point-in-time-full-recovery-model.md) or [Use Marked Transactions to Recover Related Databases Consistently &#40;Full Recovery Model&#41;](use-marked-transactions-to-recover-related-databases-consistently.md).  
  
3.  Determine the type of restore you want to perform. For more information, see [Restore and Recovery Overview &#40;SQL Server&#41;](restore-and-recovery-overview-sql-server.md).  
  
4.  Identify which backups you require and make sure that the necessary media sets and backup devices are available. For more information, see [Backup Devices &#40;SQL Server&#41;](backup-devices-sql-server.md) and [Media Sets, Media Families, and Backup Sets &#40;SQL Server&#41;](media-sets-media-families-and-backup-sets-sql-server.md).  
  
## To Perform a Restore Sequence  
 To perform a restore sequence, follow these steps:  
  
1.  To start the sequence, restore a one or more data backups, such as: a database backup, a partial backup, one or more file backups.  
  
2.  Optionally, restore the latest differential backups that are based on these full backups.  
  
     For each full backup that you plan to restore, determine whether it is the base for any differential backups. If so, restore most recent differential backup, if you can. For more information, see [Differential Backups &#40;SQL Server&#41;](differential-backups-sql-server.md).  
  
3.  Roll forward the database by restoring log backups in sequence, finishing with the backup that contains the recovery point. Whether you have to apply all the log backups depends on what log backup contains the target recovery point, as follows:  
  
    -   If the recovery point is the point of a failure, you must restore every log backup that was created since the last data (full or differential) backup you restored. For more information, see [Apply Transaction Log Backups &#40;SQL Server&#41;](transaction-log-backups-sql-server.md).  
  
    -   For a point-in-time restore, you might not require the most recent log backups. If you use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], the Database Recovery Advisor ensures that only backups that are required to restore to your specified point in time are selected. These backups make up the recommended restore plan for your point-in-time restore. For more information, see [Restore a SQL Server Database to a Point in Time &#40;Full Recovery Model&#41;](restore-a-sql-server-database-to-a-point-in-time-full-recovery-model.md).  
  
## Restarting a Restore Sequence  
 If you encounter a problem with the outcome of a restore sequence, you can quit it and restart the restore sequence over from the start. For example, if you accidentally restore too many log backups and overshoot the intended recovery point, you must restart the restore sequence up to log backup that contains the target recovery point.  
  
## See Also  
 [Backup Overview &#40;SQL Server&#41;](backup-overview-sql-server.md)   
 [Restore and Recovery Overview &#40;SQL Server&#41;](restore-and-recovery-overview-sql-server.md)   
 [Complete Database Restores &#40;Full Recovery Model&#41;](complete-database-restores-full-recovery-model.md)   
 [Online Restore &#40;SQL Server&#41;](online-restore-sql-server.md)   
 [File Restores &#40;Full Recovery Model&#41;](file-restores-full-recovery-model.md)   
 [Restore Pages &#40;SQL Server&#41;](restore-pages-sql-server.md)   
 [Piecemeal Restores &#40;SQL Server&#41;](piecemeal-restores-sql-server.md)  
  
  
