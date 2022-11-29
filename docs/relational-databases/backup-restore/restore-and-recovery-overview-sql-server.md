---
title: "Restore and recovery overview (SQL Server)"
description: This article is an overview of the operations involved in recovering a SQL Server database from a failure by restoring a set of SQL Server backups in sequence.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 10/19/2022
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
ms.custom: event-tier1-build-2022
helpviewer_keywords:
  - "restoring tables [SQL Server]"
  - "backups [SQL Server], restore scenarios"
  - "database backups [SQL Server], restore scenarios"
  - "database restores [SQL Server]"
  - "restoring [SQL Server]"
  - "restores [SQL Server]"
  - "table restores [SQL Server]"
  - "restoring databases [SQL Server], about restoring databases"
  - "database restores [SQL Server], scenarios"
  - "accelerated database recovery"
---
# Restore and recovery overview (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

To recover a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database from a failure, a database administrator has to restore a set of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backups in a logically correct and meaningful restore sequence. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] restore and recovery supports restoring data from backups of a whole database, a data file, or a data page, as follows:

- The database (a *complete database restore*)

  The whole database is restored and recovered, and the database is offline during the restore and recovery operations.

- The data file (a *file restore*)

  A data file or a set of files is restored and recovered. During a file restore, the filegroups that contain the files are automatically offline during the restore. Any attempt to access an offline filegroup causes an error.

- The data page (a *page restore*)

  Under the full recovery model or bulk-logged recovery model, you can restore individual pages. Page restores can be performed on any database, regardless of the number of filegroups.

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup and restore work across all supported operating systems. For information about the supported operating systems, see [Hardware and Software Requirements for Installing SQL Server 2016](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md). For information about support for backups from earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see the "Compatibility Support" section of [RESTORE (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md).

## <a id="RestoreScenariosOv"></a> Overview of restore scenarios

A *restore scenario* in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is the process of restoring data from one or more backups and then recovering the database. The supported restore scenarios depend on the recovery model of the database and the edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

The following table introduces the possible restore scenarios that are supported for different recovery models.

|Restore scenario|Under simple recovery model|Under full/bulk-logged recovery models|
|---|---|---|
|Complete database restore|This is the basic restore strategy. A complete database restore might involve simply restoring and recovering a full database backup. Alternatively, a complete database restore might involve restoring a full database backup followed by restoring and recovering a differential backup.<br /><br />For more information, see [Complete Database Restores (Simple Recovery Model)](../../relational-databases/backup-restore/complete-database-restores-simple-recovery-model.md).|This is the basic restore strategy. A complete database restore involves restoring a full database backup and, optionally, a differential backup (if any), followed by restoring all subsequent log backups (in sequence). The complete database restore is finished by recovering the last log backup and also restoring it (RESTORE WITH RECOVERY).<br /><br />For more information, see [Complete Database Restores (Full Recovery Model)](../../relational-databases/backup-restore/complete-database-restores-full-recovery-model.md)|
|File restore <sup>1</sup>|Restore one or more damaged read-only files, without restoring the entire database. File restore is available only if the database has at least one read-only filegroup.|Restores one or more files, without restoring the entire database. File restore can be performed while the database is offline or, for some editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], while the database remains online. During a file restore, the filegroups that contain the files that are being restored are always offline.|
|Page restore|Not applicable|Restores one or more damaged pages. Page restore can be performed while the database is offline or, for some editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], while the database remains online. During a page restore, the pages that are being restored are always offline.<br /><br />An unbroken chain of log backups must be available, up to the current log file, and they must all be applied to bring the page up-to-date with the current log file.<br /><br />For more information, see [Restore Pages (SQL Server)](../../relational-databases/backup-restore/restore-pages-sql-server.md).|
|Piecemeal restore <sup>1</sup>|Restore and recover the database in stages at the filegroup level, starting with the primary and all read/write, secondary filegroups.|Restore and recover the database in stages at the filegroup level, starting with the primary filegroup.<br /><br />For more information, see [Piecemeal Restores (SQL Server)](../../relational-databases/backup-restore/piecemeal-restores-sql-server.md)|

<sup>1</sup> Online restore is supported only in Enterprise edition.

### Steps to restore a database

To perform a file restore, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] executes two steps:

- Creates any missing database file(s).

- Copies the data from the backup devices to the database file(s).

To perform a database restore, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] executes three steps:

- Creates the database and transaction log files if they don't already exist.

- Copies all the data, log, and index pages from the backup media of a database to the database files.

- Applies the transaction log, in what is known as the [recovery process](#TlogAndRecovery).

Regardless of how data is restored, before a database can be recovered, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] guarantees that the whole database is logically consistent. For example, if you restore a file, you can't recover it and bring it online until it has been rolled far enough forward to be consistent with the database.

### Advantages of a file or page restore

Restoring and recovering files or pages, instead of the whole database, provides the following advantages:

- Restoring less data reduces the time required to copy and recover it.

- On [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] restoring files or pages might allow other data in the database to remain online during the restore operation.

## <a id="TlogAndRecovery"></a> Recovery and the transaction log

For most restore scenarios, it is necessary to apply a transaction log backup and allow the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] to run the **recovery process** for the database to be brought online. Recovery is the process used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for each database to start in a transactionally consistent - or clean - state.

In case of a failover or other non-clean shut down, the databases may be left in a state where some modifications were never written from the buffer cache to the data files, and there may be some modifications from incomplete transactions in the data files. When an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is started, it runs a recovery of each database, which consists of three phases, based on the last [database checkpoint](../../relational-databases/logs/database-checkpoints-sql-server.md):

- **Phase 1** is the **Analysis Phase** that analyzes the transaction log to determine what is the last checkpoint, and creates the *Dirty Page Table* (DPT) and the *Active Transaction Table* (ATT). The DPT contains records of pages that were dirty at the time the database was shut down. The ATT contains records of transactions that were active at the time the database wasn't cleanly shut down.

- **Phase 2** is the **Redo Phase** that rolls forwards every modification recorded in the log that may not have been written to the data files at the time the database was shut down. The [minimum log sequence number](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md#minlsn) (minLSN) required for a successful database-wide recovery is found in the DPT, and marks the start of the redo operations needed on all dirty pages. At this phase, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] writes to disk all dirty pages belonging to committed transactions.

- **Phase 3** is the **Undo Phase** that rolls back incomplete transactions found in the ATT to make sure the integrity of the database is preserved. After rollback, the database goes online, and no more transaction log backups can be applied to the database.

Information about the progress of each database recovery stage is logged in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [error log](../../tools/configuration-manager/viewing-the-sql-server-error-log.md). The database recovery progress can also be tracked using Extended Events. For more information, see the blog post [New extended events for database recovery progress](/archive/blogs/sql_server_team/new-extended-events-for-database-recovery-progress).

> [!NOTE]  
> For a Piecemeal restore scenario, if a read-only filegroup has been read-only since before the file backup was created, applying log backups to the filegroup is unnecessary and is skipped by file restore.

<a id="FastRecovery"></a>
> [!NOTE]  
> To maximize the availability of databases in an enterprise environment after the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service is started, such as after a failover of an [Always On Failover Cluster Instance](../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md) or in-place restart, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Enterprise Edition can bring a database online after the Redo Phase, while the Undo Phase is still executing. This is known as Fast Recovery.  
> However, Fast Recovery is not available when the database transitions to an online state but the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service has not been restarted. For example, executing `ALTER DATABASE AdventureWorks SET ONLINE;` will not allow the database to be in read-write state until all three phases of recovery have completed.

## <a id="RMsAndSupportedRestoreOps"></a> Recovery models and supported restore operations

The restore operations that are available for a database depend on its recovery model. The following table summarizes whether and to what extent each of the recovery models supports a given restore scenario.

|Restore operation|Full recovery model|Bulk-logged recovery model|Simple recovery model|
|---|---|---|---|
|Data recovery|Complete recovery (if the log is available).|Some data-loss exposure.|Any data since last full or differential backup is lost.|
|Point-in-time restore|Any time covered by the log backups.|Disallowed if the log backup contains any bulk-logged changes.|Not supported.|
|File restore <sup>1</sup>|Full support.|Sometimes.<sup>2</sup>|Available only for read-only secondary files.|
|Page restore <sup>1</sup>|Full support.|Sometimes.<sup>2</sup>|None.|
|Piecemeal (filegroup-level) restore <sup>1</sup>|Full support.|Sometimes.<sup>2</sup>|Available only for read-only secondary files.|

<sup>1</sup> Available only in the Enterprise edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

<sup>2</sup> For the required conditions, see [Restore Restrictions Under the Simple Recovery Model](#RMsimpleScenarios), later in this article.

> [!IMPORTANT]  
> Regardless of the recovery model of a database, a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup cannot be restored to a [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] version that is older than the version that created the backup.

## <a id="RMsimpleScenarios"></a> Restore scenarios under the simple recovery model

The simple recovery model imposes the following restrictions on restore operations:

- File restore and piecemeal restore are available only for read-only secondary filegroups. For information about these restore scenarios, see [File Restores (Simple Recovery Model)](../../relational-databases/backup-restore/file-restores-simple-recovery-model.md) and [Piecemeal Restores (SQL Server)](../../relational-databases/backup-restore/piecemeal-restores-sql-server.md).

- Page restore isn't allowed.

- Point-in-time restore isn't allowed.

If any of these restrictions are inappropriate for your recovery needs, we recommend that you consider using the full recovery model. For more information, see [Backup Overview (SQL Server)](../../relational-databases/backup-restore/backup-overview-sql-server.md).

> [!IMPORTANT]  
> Regardless of the recovery model of a database, a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] backup cannot be restored by a version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is older than the version that created the backup.

## <a id="RMblogRestore"></a> Restore under the bulk-logged recovery model

This section discusses restore considerations that are unique to bulk-logged recovery model, which is intended exclusively as a supplement to the full recovery model.

> [!NOTE]  
> For an introduction to the bulk-logged recovery model, see [The Transaction Log (SQL Server)](../../relational-databases/logs/the-transaction-log-sql-server.md).

Generally, the bulk-logged recovery model is similar to the full recovery model, and the information described for the full recovery model also applies to both. However, point-in-time recovery and online restore are affected by the bulk-logged recovery model.

### Restrictions for point-in-time recovery

If a log backup taken under the bulk-logged recovery model contains bulk-logged changes, point-in-time recovery isn't allowed. Trying to perform point-in-time recovery on a log backup that contains bulk changes will cause the restore operation to fail.

### Restrictions for online restore

An online restore sequence works only if the following conditions are met:

- All required log backups must have been taken before the restore sequence starts.

- Bulk changes must be backed before starting the online restore sequence.

- If bulk changes exist in the database, all files must be either online or [defunct](../../relational-databases/backup-restore/remove-defunct-filegroups-sql-server.md). (This means that it is no longer part of the database.)

If these conditions aren't met, the online restore sequence fails.

> [!NOTE]  
> We recommend switching to the full recovery model before starting an online restore. For more information, see [Recovery Models (SQL Server)](../../relational-databases/backup-restore/recovery-models-sql-server.md).

For information about how to perform an online restore, see [Online Restore (SQL Server)](../../relational-databases/backup-restore/online-restore-sql-server.md).

## <a id="DRA"></a> Database Recovery Advisor (SQL Server Management Studio)

The Database Recovery Advisor facilitates constructing restore plans that implement optimal correct restore sequences. Many known database restore issues and enhancements requested by customers have been addressed. Major enhancements introduced by the Database Recovery Advisor include the following:

- **Restore-plan algorithm:**  The algorithm used to construct restore plans has improved significantly, particularly for complex restore scenarios. Many edge cases, including forking scenarios in point-in-time restores, are handled more efficiently than in previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

- **Point-in-time restores:**  The Database Recovery Advisor greatly simplifies restoring a database to a given point in time. A visual backup timeline significantly enhances support for point-in-time restores. This visual timeline allows you to identify a feasible point in time as the target recovery point for restoring a database. The timeline facilitates traversing a forked recovery path (a path that spans recovery forks). A given point-in-time restore plan automatically includes the backups that are relevant to the restoring to your target point in time (date and time). For more information, see [Restore a SQL Server Database to a Point in Time (Full Recovery Model)](../../relational-databases/backup-restore/restore-a-sql-server-database-to-a-point-in-time-full-recovery-model.md).

For more information, see about the Database Recovery Advisor, see the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Manageability blogs:

- [Recovery Advisor: An Introduction](/archive/blogs/managingsql/recovery-advisor-an-introduction)

- [Recovery Advisor: Using SSMS to create/restore split backups](/archive/blogs/managingsql/recovery-advisor-using-ssms-to-createrestore-split-backups)

## <a id="adr"></a> Accelerated database recovery

[Accelerated database recovery](/azure/sql-database/sql-database-accelerated-database-recovery/) is available beginning in [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] and in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]. Accelerated database recovery greatly improves database availability, especially in the presence of long-running transactions, by redesigning the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] [recovery process](#TlogAndRecovery). A database for which accelerated database recovery was enabled completes the recovery process significantly faster after a failover or other non-clean shut down. When enabled, Accelerated database recovery also completes rollback of canceled long-running transactions significantly faster.

You can enable accelerated database recovery per-database on [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] using the following syntax:

```sql
ALTER DATABASE [<db_name>] SET ACCELERATED_DATABASE_RECOVERY = ON;
```

> [!NOTE]  
> Accelerated database recovery is enabled by default on [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].

## Next steps

- [Backup Overview (SQL Server)](../../relational-databases/backup-restore/backup-overview-sql-server.md)  
- [The Transaction Log (SQL Server)](../../relational-databases/logs/the-transaction-log-sql-server.md)  
- [SQL Server Transaction Log Architecture and Management Guide](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md)  
- [Back Up and Restore of SQL Server Databases](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md)  
- [Apply Transaction Log Backups (SQL Server)](../../relational-databases/backup-restore/apply-transaction-log-backups-sql-server.md)
