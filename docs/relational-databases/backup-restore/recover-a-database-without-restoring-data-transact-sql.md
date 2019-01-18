---
title: "Recover a Database Without Restoring Data (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: backup-restore
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "restoring [SQL Server], recovery-only"
  - "recovery-only scenario [SQL Server]"
  - "restoring databases [SQL Server], recovery-only"
  - "recovery [SQL Server], recovery-only"
  - "database recovery [SQL Server]"
  - "database restores [SQL Server], recovery-only"
  - "recovery [SQL Server], without restoring data"
ms.assetid: 7e8fa620-315d-4e10-a718-23fa5171c09e
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Recover a Database Without Restoring Data (Transact-SQL)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Usually, all of the data in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database is restored before the database is recovered. However, a restore operation can recover a database without actually restoring a backup; for example, when recovering a read-only file that is consistent with the database. This is referred to as a *recovery-only restore*. When offline data is already consistent with the database and needs only to be made available, a recovery-only restore operation completes the recovery of the database and bring the data online.  
  
 A recovery-only restore can occur for a whole database or for one or more a files or filegroups.  
  
## Recovery-Only Database Restore  
 A recovery-only database restore can be useful in the following situations:  
  
-   You did not recover the database when restoring the last backup in a restore sequence, and you now want to recover the database to bring it online.  
  
-   The database is in standby mode, and you want to make the database updatable without applying another log backup.  
  
 The [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md) syntax for a recovery-only database restore is as follows:  
  
 RESTORE DATABASE *database_name* WITH RECOVERY  
  
> [!NOTE]  
>  The FROM **=** \<*backup_device>* clause is not used for recovery-only restores because no backup is necessary.  
  
 **Example**  
  
 The following example recovers the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database in a restore operation without restoring data.  
  
```  
-- Restore database using WITH RECOVERY.  
RESTORE DATABASE AdventureWorks2012  
   WITH RECOVERY  
```  
  
## Recovery-Only File Restore  
 A recovery-only file restore can be useful in the following situation:  
  
 A database is restored piecemeal. After restore of the primary filegroup is complete, one or more of the unrestored files are consistent with the new database state, perhaps because it has been read-only for some time. These files only have to be recovered; data copying is unnecessary.  
  
 A recovery-only restore operation brings the data in the offline filegroup online; no data-copy, redo, or undo phase occurs. For information about the phases of restore, see [Restore and Recovery Overview &#40;SQL Server&#41;](../../relational-databases/backup-restore/restore-and-recovery-overview-sql-server.md).  
  
 The [RESTORE](../../t-sql/statements/restore-statements-transact-sql.md) syntax for a recovery-only file restore is:  
  
 RESTORE DATABASE *database_name* { FILE **=**_logical_file_name_ | FILEGROUP **=**_logical_filegroup_name_ }[ **,**...*n* ] WITH RECOVERY  
  
 **Example**  
  
 The following example illustrates a recovery-only file restore of the files in a secondary filegroup, `SalesGroup2`, in the `Sales` database. The primary filegroup has already been restored as the initial step of a piecemeal restore, and `SalesGroup2` is consistent with the restored primary filegroup. Recovering this filegroup and bringing it online requires only a single statement.  
  
```  
RESTORE DATABASE Sales FILEGROUP=SalesGroup2 WITH RECOVERY;  
```  
  
## Examples of Completing a Piecemeal Restore Scenario with a Recovery-Only Restore  
 **Simple recovery model**  
  
-   [Example: Piecemeal Restore of Database &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/example-piecemeal-restore-of-database-simple-recovery-model.md)  
  
-   [Example: Piecemeal Restore of Only Some Filegroups &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/example-piecemeal-restore-of-only-some-filegroups-simple-recovery-model.md)  
  
 **Full recovery model**  
  
-   [Example: Piecemeal Restore of Database &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/example-piecemeal-restore-of-database-full-recovery-model.md)  
  
-   [Example: Piecemeal Restore of Only Some Filegroups &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/example-piecemeal-restore-of-only-some-filegroups-full-recovery-model.md)  
  
-   <xref:Microsoft.SqlServer.Management.Smo.Restore.SqlRestore%2A>  
  
## See Also  
 [Online Restore &#40;SQL Server&#41;](../../relational-databases/backup-restore/online-restore-sql-server.md)   
 [Piecemeal Restores &#40;SQL Server&#41;](../../relational-databases/backup-restore/piecemeal-restores-sql-server.md)   
 [File Restores &#40;Simple Recovery Model&#41;](../../relational-databases/backup-restore/file-restores-simple-recovery-model.md)   
 [File Restores &#40;Full Recovery Model&#41;](../../relational-databases/backup-restore/file-restores-full-recovery-model.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)  
  
  
