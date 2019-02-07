---
title: "Piecemeal Restores (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: backup-restore
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "partial updates [SQL Server]"
  - "staged restores [SQL Server]"
  - "piecemeal restores [SQL Server]"
  - "restoring [SQL Server], piecemeal restore scenario"
ms.assetid: 208f55e0-0762-4cfb-85c4-d36a76ea0f5b
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Piecemeal Restores (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic is relevant for databases in the Enterprise edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] (online restore) or Standard edition (offline restore) that contain multiple files or filegroups; and, under the simple model, only for read-only filegroups.  
  
 For information about piecemeal restore and memory-optimized tables, see [Piecemeal Restore of Databases With Memory-Optimized Tables](../../relational-databases/in-memory-oltp/piecemeal-restore-of-databases-with-memory-optimized-tables.md).  
  
 *Piecemeal restore* allows databases that contain multiple filegroups to be restored and recovered in stages. Piecemeal restore involves a series of restore sequences, starting with the primary filegroup and, in some cases, one or more secondary filegroups. Piecemeal restore maintains checks to ensure that the database will be consistent in the end. After the restore sequence is completed, recovered files, if they are valid and consistent with the database, can be brought online directly.  
  
 Piecemeal restore works with all recovery models, but is more flexible for the full and bulk-logged models than for the simple model.  
  
 Every piecemeal restore starts with an initial restore sequence called the *partial-restore sequence*. Minimally, the partial-restore sequence restores and recovers the primary filegroup and, under the simple recovery model, all read/write filegroups. During the piecemeal-restore sequence, the whole database must go offline. Thereafter, the database is online and restored filegroups are available. However, any unrestored filegroups remain offline and are not accessible. Any offline filegroups, however, can be restored and brought online later by a file restore.  
  
 Regardless of the recovery model that is used by the database, the partial-restore sequence starts with a RESTORE DATABASE statement that restores a full backup and specifies the PARTIAL option. The PARTIAL option always starts a new piecemeal restore; therefore, you must specify PARTIAL only one time in the initial statement of the partial-restore sequence. When the partial restore sequence finishes and the database is brought online, the state of the remaining files becomes "recovery pending" because their recovery has been postponed.  
  
 Subsequently, a piecemeal restore typically includes one or more restore sequences, which are called *filegroup-restore sequences*. You can wait to perform a specific filegroup-restore sequence for as long as you want. Each filegroup-restore sequence restores and recovers one or more offline filegroups to a point consistent with the database. The timing and number of filegroup-restore sequences depends on your recovery goal, the number of offline filegroups you want to restore, and on how many of them you restore per filegroup-restore sequence.  
  
 The exact requirements for performing a piecemeal restore depend on the recovery model of the database. For more information, see "Piecemeal Restore Under the Simple Recovery Model" and "Piecemeal Restore Under the Full Recovery Model," later in this topic.  
  
## Piecemeal Restore Scenarios  
 All editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] support offline piecemeal restores. In the Enterprise edition, a piecemeal restore can be either online or offline. The implications of offline and online piecemeal restores are as follows:  
  
-   Offline piecemeal restore scenario  
  
     In an offline piecemeal restore, the database is online after the partial-restore sequence. Filegroups that have not yet been restored remain offline, but they can be restored as you need them after taking the database offline.  
  
-   Online piecemeal restore scenario  
  
     In an online piecemeal restore, after the partial-restore sequence, the database is online, and the primary filegroup and any recovered secondary filegroups are available. Filegroups that have not yet been restored remain offline, but they can be restored as needed while the database remains online.  
  
     Online piecemeal restores can involve deferred transactions. When only a subset of filegroups has been restored, transactions in the database that depend on online filegroups might become deferred. This is typical, because the whole database must be consistent. For more information, see [Deferred Transactions &#40;SQL Server&#41;](../../relational-databases/backup-restore/deferred-transactions-sql-server.md).  
  
-   [!INCLUDE[hek_1](../../includes/hek-1-md.md)] piecemeal restore scenario  
  
     For information on Piecemeal Restores of In-Memory OLTP databases see [Piecemeal Backup and Restore of Databases With Memory-Optimized Tables](../../relational-databases/in-memory-oltp/piecemeal-restore-of-databases-with-memory-optimized-tables.md).  
  
## Restrictions  
 If a partial restore sequence excludes any [FILESTREAM](../../relational-databases/blob/filestream-sql-server.md) filegroup, point-in-time restore is not supported. You can force the restore sequence to continue. However the FILESTREAM filegroups that are omitted from your RESTORE statement can never be restored. To force a point-in-time restore, specify the CONTINUE_AFTER_ERROR option together with the STOPAT, STOPATMARK, or STOPBEFOREMARK option, which you must also specify in your subsequent RESTORE LOG statements. If you specify CONTINUE_AFTER_ERROR, the partial restore sequence succeeds and the FILESTREAM filegroup becomes unrecoverable.  
  
## Piecemeal Restore Under the Simple Recovery Model  
 Under the simple recovery model, the piecemeal restore sequence must start with a full database or partial backup. Then, if the restored backup is a differential base, restore the latest differential backup next.  
  
 During the first partial restore sequence, if you restore only a subset of read/write filegroups, any unrestored filegroups become defunct when you recover the partially restored database. Omitting a read/write filegroup from the partial-restore sequence is appropriate only in the following cases:  
  
-   You intend for the unrestored filegroups to become defunct.  
  
-   The restore sequence will arrive at a recovery point at which each unrestored filegroup has become read-only, dropped, or defunct (during a previous restore in the partial-restore sequence).  
  
-   The full backup was taken while the database was using the simple recovery model, but the recovery point is at a time when the database is using the full recovery model. For more information, see "Performing a Piecemeal Restore of a Database Whose Recovery Model Has Been Switched from Simple to Full," later in this topic.  
  
### Requirements for Piecemeal Restore Under the Simple Recovery Model  
 Under the simple recovery model, the initial stage restores and recovers the primary filegroup and all read/write secondary filegroups. After the initial stage is completed, recovered files, if they are valid and consistent with the database, can be brought online directly.  
  
 Thereafter, read-only filegroups can be restored in one or more additional stages.  
  
 Piecemeal restore is available for a read-only secondary filegroup only if the following are true:  
  
-   Was read-only when backed up.  
  
-   Has remained read-only (keeping it logically consistent with the primary filegroup).  
  
 To perform a piecemeal restore, the following guidelines must be followed:  
  
-   A complete set of backups for the piecemeal restore of a simple recovery model database must contain the following:  
  
    -   A partial or full database backup that contains the primary filegroup and all filegroups that were read/write at the time of the backup.  
  
    -   A backup of each read-only file.  
  
-   For the backup of a read-only file to be consistent with the primary filegroup, the secondary filegroup must have been read-only from when it was backed up until the backup that contains the primary filegroup was completed. You can use differential file backups, if they were taken after the filegroup became read-only.  
  
### Piecemeal Restore Stages (Simple Recovery Model)  
 The piecemeal restore scenario involves the following stages:  
  
-   Initial stage (restore and recover the primary filegroup and all read/write filegroups)  
  
     The initial stage performs a partial restore. The partial restore sequence restores the primary filegroup, all read/write secondary filegroups, and (optionally) some of the read-only filegroups. During the initial stage, the whole database must go offline. After the initial stage, the database is online, and restored filegroups are available. However, any read-only filegroups that have not yet been restored, remain offline.  
  
     The first RESTORE statement in the initial stage must do the following:  
  
    -   Use a partial or full database backup that contains the primary filegroup and all filegroups that were read/write at the time of the backup. It is common to start a partial restore sequence by restoring a partial backup.  
  
    -   Specify the PARTIAL option, which indicates the start of a piecemeal restore.  
  
    > [!NOTE]  
    >  The PARTIAL option performs safety checks that ensure that the resulting database is suited for use as a production database.  
  
    -   Specify the READ_WRITE_FILEGROUPS option if the backup is a full database backup.  
  
-   While the database is online, you can use one or more online file restores to restore and recover offline read-only files that were read-only at the time of backup. The timing of the online file restores depends on when you want to have the data online.  
  
     Whether you must restore data to a file depends on the following:  
  
    -   Valid read-only files that are consistent with the database can be brought online directly by recovering them without restoring any data.  
  
    -   Files that are damaged or inconsistent with the database must be restored before they are recovered.  
  
### Examples  
  
-   [Example: Piecemeal Restore of Database &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/example-piecemeal-restore-of-database-simple-recovery-model.md)  
  
-   [Example: Piecemeal Restore of Only Some Filegroups &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/example-piecemeal-restore-of-only-some-filegroups-simple-recovery-model.md)  
  
## Piecemeal Restore Under the Full Recovery Model  
 Under the full recovery model or bulk-logged recovery model, piecemeal restore is available for any database that contains multiple filegroups and you can restore a database to any point in time. The restore sequences of a piecemeal restore behave as follows:  
  
-   Partial-restore sequence  
  
     The partial restore sequence restores the primary filegroup and, optionally, some of the secondary filegroups.  
  
     The first RESTORE DATABASE statement must do the following:  
  
    -   Specify the PARTIAL option. This indicates the start of a piecemeal restore.  
  
    -   Use any full database backup that contains the primary filegroup. The common practice is to start a partial restore sequence by restoring a partial backup.  
  
    -   To restore to a specific point in time, you must specify the time in the partial restore sequence. Every successive step of the restore sequence must specify the same point in time.  
  
-   Filegroup-restore sequences bring additional filegroups online to a point consistent with the database.  
  
     In the Enterprise edition, any offline secondary filegroup can be restored and recovered while the database remains online. If a specific read-only file is undamaged and consistent with the database, the file does not have to be restored. For more information, see [Recover a Database Without Restoring Data &#40;Transact-SQL&#41;](../../relational-databases/backup-restore/recover-a-database-without-restoring-data-transact-sql.md).  
  
### Applying Log Backups  
 If a read-only filegroup has been read-only since before the file backup was created, applying log backups to the filegroup is unnecessary and is skipped by file restore. If the filegroup is read/write, an unbroken chain of log backups must be applied to the last full or differential restore to bring the filegroup forward to the current log file.  
  
### Examples  
  
-   [Example: Piecemeal Restore of Database &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/example-piecemeal-restore-of-database-full-recovery-model.md)  
  
-   [Example: Piecemeal Restore of Only Some Filegroups &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/example-piecemeal-restore-of-only-some-filegroups-full-recovery-model.md)  
  
## Performing a Piecemeal Restore of a Database Whose Recovery Model Has Been Switched from Simple to Full  
 You can perform a piecemeal restore of a database that has been switched from the simple recovery model to the full recovery model since the full partial or database backup. For example, consider a database for which you take the following steps:  
  
1.  Create a partial backup (backup_1) of a simple-model database.  
  
2.  After some time, change the recovery model to full.  
  
3.  Create a differential backup.  
  
4.  Start taking log backups.  
  
 Thereafter, the following sequence is valid:  
  
1.  A partial restore that omits some secondary filegroups.  
  
2.  A differential restore followed by any other needed restores.  
  
3.  Later, a file restore of a read/write secondary filegroup WITH NORECOVERY from the backup_1 partial backup  
  
4.  The differential backup followed by any other backups that were restored in the original piecemeal restore sequence to restore the data up to the original recovery point.  
  
## See Also  
 [Apply Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/apply-transaction-log-backups-sql-server.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)   
 [Restore a SQL Server Database to a Point in Time &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/restore-a-sql-server-database-to-a-point-in-time-full-recovery-model.md)   
 [Restore and Recovery Overview &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-and-recovery-overview-sql-server.md)   
 [Plan and Perform Restore Sequences &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/plan-and-perform-restore-sequences-full-recovery-model.md)  
  
  
