---
title: "Troubleshoot Full Transaction Log Error 9002"
description: Learn how to resolve a full SQL Server transaction log, and how to avoid the problem in the future.
author: "MashaMSFT"
ms.author: "mathoma"
ms.reviewer: randolphwest
ms.date: 01/26/2026
ms.service: sql
ms.subservice: supportability
ms.topic: troubleshooting
helpviewer_keywords:
  - "logs [SQL Server], full"
  - "troubleshooting [SQL Server], full transaction log"
  - "9002 (Database Engine error)"
  - "transaction logs [SQL Server], truncation"
  - "back up transaction logs [SQL Server], full logs"
  - "transaction logs [SQL Server], full log"
  - "full transaction logs [SQL Server]"
monikerRange: ">=sql-server-2017 || >=sql-server-linux-2017"
---
# Troubleshoot a full transaction log (SQL Server Error 9002)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article applies to SQL Server instances.

> [!NOTE]  
> **This article is focused on SQL Server**. For more specific information on this error in Azure SQL platforms, see [Troubleshooting transaction log errors with Azure SQL Database](/azure/azure-sql/database/troubleshoot-transaction-log-errors-issues?view=azuresql-db&preserve-view=true) and [Troubleshooting transaction log errors with Azure SQL Managed Instance](/azure/azure-sql/managed-instance/troubleshoot-transaction-log-errors-issues?view=azuresql-mi&preserve-view=true). Azure SQL Database and Azure SQL Managed Instance are based on the latest stable version of the Microsoft SQL Server database engine, so much of the content is similar though troubleshooting options and tools might differ.

This article discusses possible responses to a full transaction log and suggests how to avoid it in the future.

When the transaction log becomes full, the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] issues a **9002 error**. The log can fill when the database is online, or in recovery. If the log fills while the database is online, the database remains online but can only be read, not updated. If the log fills during recovery, the [!INCLUDE [ssDE](../../includes/ssde-md.md)] marks the database as `RESOURCE PENDING`. In either case, user action is required to make log space available.

## Common reasons for a full transaction log

The appropriate response to a full transaction log depends on what conditions caused the log to fill. Common causes include:

- Log not being truncated
- Disk volume is full
- Log size is set to a fixed maximum value or autogrow is disabled
- Replication or availability group synchronization that is unable to complete

<a id="how-to-resolve-a-full-transaction-log"></a>

Follow these specific steps to help you find the reason for a full transaction log and resolve the issue.

- [Truncate the log](#1-truncate-the-log)
- [Resolve a full disk volume](#2-resolve-full-disk-volume)
- [Change the log size limit or enable autogrow](#3-change-log-size-limit-or-enable-autogrow)

## 1. Truncate the log

A common solution to this problem is to ensure transaction log backups are performed for your database, which ensures the log is truncated. If no recent transaction log history is indicated for the database with a full transaction log, the solution to the problem is straightforward: resume regular transaction log backups of the database.

For more information, review [Manage the size of the transaction log file](manage-the-size-of-the-transaction-log-file.md) and [Shrink a file](../databases/shrink-a-file.md).

### Log truncation explained

There's a difference between truncating a transaction log and shrinking a transaction log. [Log truncation](the-transaction-log-sql-server.md#Truncation) occurs normally during a transaction log backup, and is a logical operation that removes committed records inside the log, whereas [log shrinking](../../t-sql/database-console-commands/dbcc-shrinkfile-transact-sql.md#shrinking-a-log-file) reclaims physical space on the file system by reducing the file size. Log truncation occurs on a [virtual-log-file (VLF)](../sql-server-transaction-log-architecture-and-management-guide.md#physical_arch) boundary, and a log file might contain many VLFs. A log file can be shrunk only if there's empty space inside the log file to reclaim. Shrinking a log file alone can't solve the problem of a full log file. Instead, you must discover why the log file is full and can't be truncated.

> [!WARNING]  
> Data that is moved to shrink a file can be scattered to any available location in the file. This causes index fragmentation and might slow the performance of queries that search a range of the index. To eliminate the fragmentation, consider rebuilding the indexes on the file after shrinking. For more information, see [Shrink a database](../databases/shrink-a-database.md).

### What is preventing log truncation?

To discover what is preventing log truncation in a given case, use the `log_reuse_wait` and `log_reuse_wait_desc` columns of the `sys.databases` catalog view. For more information, see [sys.databases](../system-catalog-views/sys-databases-transact-sql.md). For descriptions of factors that can delay log truncation, see [The transaction log](the-transaction-log-sql-server.md).

The following set of T-SQL commands helps you identify if a database transaction log isn't truncated and the reason for it. The following script also recommends steps to resolve the issue:

```sql
SET NOCOUNT ON;

DECLARE
    @SQL AS VARCHAR (8000),
    @log_reuse_wait AS TINYINT,
    @log_reuse_wait_desc AS NVARCHAR (120),
    @dbname AS SYSNAME,
    @database_id AS INT,
    @recovery_model_desc AS VARCHAR (24);

IF (OBJECT_id(N'tempdb..#CannotTruncateLog_Db') IS NOT NULL)
    BEGIN
        DROP TABLE #CannotTruncateLog_Db;
    END

--get info about transaction logs in each database.
IF (OBJECT_id(N'tempdb..#dm_db_log_space_usage') IS NOT NULL)
BEGIN
    DROP TABLE #dm_db_log_space_usage;
END

SELECT *
INTO #dm_db_log_space_usage
FROM sys.dm_db_log_space_usage
WHERE 1 = 0;

DECLARE log_space CURSOR
    FOR SELECT NAME
        FROM sys.databases;

OPEN log_space;

FETCH NEXT FROM log_space INTO @dbname;

WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @SQL = '
            INSERT INTO #dm_db_log_space_usage (
                database_id,
                total_log_size_in_bytes,
                used_log_space_in_bytes,
                used_log_space_in_percent,
                log_space_in_bytes_since_last_backup
                )
            SELECT database_id,
                total_log_size_in_bytes,
                used_log_space_in_bytes,
                used_log_space_in_percent,
                log_space_in_bytes_since_last_backup
            FROM ' + QUOTENAME(@dbname) + '.sys.dm_db_log_space_usage;';
        BEGIN TRY
            EXECUTE (@SQL);
        END TRY
        BEGIN CATCH
            SELECT ERROR_MESSAGE() AS ErrorMessage;
        END CATCH
        FETCH NEXT FROM log_space INTO @dbname;
    END

CLOSE log_space;

DEALLOCATE log_space;

--select the affected databases
SELECT sdb.name AS DbName,
       sdb.log_reuse_wait,
       sdb.log_reuse_wait_desc,
       CASE
           WHEN log_reuse_wait = 1 THEN 'No checkpoint has occurred since the last log truncation, or the head of the log has not yet moved beyond'
           WHEN log_reuse_wait = 2 THEN 'A log backup is required before the transaction log can be truncated.'
           WHEN log_reuse_wait = 3 THEN 'A data backup or a restore is in progress (all recovery models). Please wait or cancel backup'
           WHEN log_reuse_wait = 4 THEN 'A long-running active transaction or a deferred transaction is keeping log from being truncated. You can attempt a log backup to free space or complete/rollback long transaction'
           WHEN log_reuse_wait = 5 THEN 'Database mirroring is paused, or under high-performance mode, the mirror database is significantly behind the principal database. (Full recovery model only)'
           WHEN log_reuse_wait = 6 THEN 'During transactional replication, transactions relevant to the publications are still undelivered to the distribution database. Investigate the status of agents involved in replication or Changed Data Capture (CDC). (Full recovery model only.)'
           WHEN log_reuse_wait = 7 THEN 'A database snapshot is being created. This is a routine, and typically brief, cause of delayed log truncation.'
           WHEN log_reuse_wait = 8 THEN 'A transaction log scan is occurring. This is a routine, and typically a brief cause of delayed log truncation.'
           WHEN log_reuse_wait = 9 THEN 'A secondary replica of an availability group is applying transaction log records of this database to a corresponding secondary database. (Full recovery model only.)'
           WHEN log_reuse_wait = 13 THEN 'If a database is configured to use indirect checkpoints, the oldest page on the database might be older than the checkpoint log sequence number (LSN).'
           WHEN log_reuse_wait = 16 THEN 'An In-Memory OLTP checkpoint has not occurred since the last log truncation, or the head of the log has not yet moved beyond a VLF.'
           ELSE 'None'
       END AS log_reuse_wait_explanation,
       sdb.database_id,
       sdb.recovery_model_desc,
       lsu.used_log_space_in_bytes / 1024. / 1024. AS Used_log_size_MB,
       lsu.total_log_size_in_bytes / 1024. / 1024. AS Total_log_size_MB,
       100 - lsu.used_log_space_in_percent AS Percent_Free_Space
INTO #CannotTruncateLog_Db
FROM sys.databases AS sdb
     INNER JOIN #dm_db_log_space_usage AS lsu
         ON sdb.database_id = lsu.database_id
WHERE log_reuse_wait > 0;

SELECT *
FROM #CannotTruncateLog_Db;

DECLARE no_truncate_db CURSOR FOR
    SELECT log_reuse_wait,
            log_reuse_wait_desc,
            DbName,
            database_id,
            recovery_model_desc
    FROM #CannotTruncateLog_Db;

OPEN no_truncate_db;

FETCH NEXT FROM no_truncate_db
INTO
    @log_reuse_wait,
    @log_reuse_wait_desc,
    @dbname,
    @database_id,
    @recovery_model_desc;

WHILE @@FETCH_STATUS = 0
    BEGIN
        IF (@log_reuse_wait > 0)
        BEGIN
            SELECT '-- ' + QUOTENAME(@dbname) + ' database has log_reuse_wait = ' + @log_reuse_wait_desc + ' --' AS 'Individual Database Report';
        END
        IF (@log_reuse_wait = 1)
        BEGIN
            SELECT 'Consider running the checkpoint command to attempt resolving this issue or further t-shooting may be required on the checkpoint process. Also, examine the log for active VLFs at the end of file' AS Recommendation;
            SELECT 'USE ' + QUOTENAME(@dbname) + '; CHECKPOINT' AS CheckpointCommand;
            SELECT 'SELECT * FROM sys.dm_db_log_info(' + CONVERT (VARCHAR, @database_id) + ')' AS VLF_LogInfo;
        END
        ELSE IF (@log_reuse_wait = 2)
        BEGIN
            SELECT 'Is ' + @recovery_model_desc + ' recovery model the intended choice for ' + QUOTENAME(@dbname) + ' database? Review recovery models and determine if you need to change it. https://learn.microsoft.com/sql/relational-databases/backup-restore/recovery-models-sql-server' AS RecoveryModelChoice;
            SELECT 'To truncate the log consider performing a transaction log backup on database ' + QUOTENAME(@dbname) + ' which is in ' + @recovery_model_desc + ' recovery model. Be mindful of any existing log backup chains that could be broken' AS Recommendation;
            SELECT 'BACKUP LOG ' + QUOTENAME(@dbname) + ' TO DISK = ''some_volume:\some_folder\' + QUOTENAME(@dbname) + '_LOG.trn '';' AS BackupLogCommand;
        END
        ELSE IF (@log_reuse_wait = 3)
        BEGIN
            SELECT 'Either wait for or cancel any active backups currently running for database ' + QUOTENAME(@dbname) + '. To check for backups, run this command:' AS Recommendation;
            SELECT 'SELECT * FROM sys.dm_exec_requests WHERE command LIKE ''backup%'' OR command LIKE ''restore%''' AS FindBackupOrRestore;
        END
        ELSE IF (@log_reuse_wait = 4)
        BEGIN
            SELECT 'Active transactions currently running for database ' + QUOTENAME(@dbname) + '. To check for active transactions, run these commands:' AS Recommendation;
            SELECT 'DBCC OPENTRAN (' + QUOTENAME(@dbname) + ')' AS FindOpenTran;
            SELECT 'SELECT database_id, db_name(database_id) AS dbname, database_transaction_begin_time, database_transaction_state, database_transaction_log_record_count, database_transaction_log_bytes_used, database_transaction_begin_lsn, stran.session_id FROM sys.dm_tran_database_transactions dbtran LEFT OUTER JOIN sys.dm_tran_session_transactions stran ON dbtran.transaction_id = stran.transaction_id WHERE database_id = ' + CONVERT (VARCHAR, @database_id) AS FindOpenTransAndSession;
        END
        ELSE IF (@log_reuse_wait = 5)
        BEGIN
            SELECT 'Database Mirroring for database ' + QUOTENAME(@dbname) + ' is behind on synchronization. To check the state of DBM, run the commands below:' AS Recommendation;
            SELECT 'SELECT db_name(database_id), mirroring_state_desc, mirroring_role_desc, mirroring_safety_level_desc FROM sys.database_mirroring WHERE mirroring_guid IS NOT NULL and mirroring_state <> 4 AND database_id = ' + CONVERT (sysname, @database_id) AS CheckMirroringStatus;
            SELECT 'Database Mirroring for database ' + QUOTENAME(@dbname) + ' may be behind: check unsent_log, send_rate, unrestored_log, recovery_rate, average_delay in this output' AS Recommendation;
            SELECT 'EXECUTE msdb.sys.sp_dbmmonitoraddmonitoring 1; EXECUTE msdb.sys.sp_dbmmonitorresults ' + QUOTENAME(@dbname) + ', 5, 0; WAITFOR DELAY ''00:01:01''; EXECUTE msdb.sys.sp_dbmmonitorresults ' + QUOTENAME(@dbname) + '; EXECUTE msdb.sys.sp_dbmmonitordropmonitoring' AS CheckMirroringStatusAnd;
        END
        ELSE IF (@log_reuse_wait = 6)
        BEGIN
            SELECT 'Replication transactions still undelivered FROM publisher database ' + QUOTENAME(@dbname) + ' to Distribution database. Check the oldest non-distributed replication transaction. Also check if the Log Reader Agent is running and if it has encountered any errors' AS Recommendation;
            SELECT 'DBCC OPENTRAN  (' + QUOTENAME(@dbname) + ')' AS CheckOldestNonDistributedTran;
            SELECT 'SELECT top 5 * FROM distribution..MSlogreader_history WHERE runstatus in (6, 5) OR error_id <> 0 AND agent_id = find_in_mslogreader_agents_table ORDER BY time desc ' AS LogReaderAgentState;
        END
        ELSE IF (@log_reuse_wait = 9)
        BEGIN
            SELECT 'Always On transactions still undelivered FROM primary database ' + QUOTENAME(@dbname) + ' to Secondary replicas. Check the Health of AG nodes and if there is latency is Log block movement to Secondaries' AS Recommendation;
            SELECT 'SELECT availability_group = CAST(ag.name AS VARCHAR(30)), primary_replica = CAST(ags.primary_replica AS VARCHAR(30)), primary_recovery_health_desc = CAST(ags.primary_recovery_health_desc AS VARCHAR(30)), synchronization_health_desc = CAST(ags.synchronization_health_desc AS VARCHAR(30)), ag.failure_condition_level, ag.health_check_timeout, automated_backup_preference_desc = CAST(ag.automated_backup_preference_desc AS VARCHAR(10)) FROM sys.availability_groups ag join sys.dm_hadr_availability_group_states ags on ag.group_id = ags.group_id' AS CheckAGHealth;
            SELECT 'SELECT  group_name = CAST(arc.group_name AS VARCHAR(30)), replica_server_name = CAST(arc.replica_server_name AS VARCHAR(30)), node_name = CAST(arc.node_name AS VARCHAR(30)), role_desc = CAST(ars.role_desc AS VARCHAR(30)), ar.availability_mode_Desc, operational_state_desc = CAST(ars.operational_state_desc AS VARCHAR(30)), connected_state_desc = CAST(ars.connected_state_desc AS VARCHAR(30)), recovery_health_desc = CAST(ars.recovery_health_desc AS VARCHAR(30)), synchronization_health_desc = CAST(ars.synchronization_health_desc AS VARCHAR(30)), ars.last_connect_error_number, last_connect_error_description = CAST(ars.last_connect_error_description AS VARCHAR(30)), ars.last_connect_error_timestamp, primary_role_allow_connections_desc = CAST(ar.primary_role_allow_connections_desc AS VARCHAR(30)) FROM sys.dm_hadr_availability_replica_cluster_nodes arc join sys.dm_hadr_availability_replica_cluster_states arcs on arc.replica_server_name = arcs.replica_server_name join sys.dm_hadr_availability_replica_states ars on arcs.replica_id = ars.replica_id join sys.availability_replicas ar on ars.replica_id = ar.replica_id join sys.availability_groups ag on ag.group_id = arcs.group_id and ag.name = arc.group_name ORDER BY CAST(arc.group_name AS VARCHAR(30)), CAST(ars.role_desc AS VARCHAR(30))' AS CheckReplicaHealth;
            SELECT 'SELECT database_name = CAST(drcs.database_name AS VARCHAR(30)), drs.database_id, drs.group_id, drs.replica_id, drs.is_local, drcs.is_failover_ready, drcs.is_pending_secondary_suspend, drcs.is_database_joined, drs.is_suspended, drs.is_commit_participant, suspend_reason_desc = CAST(drs.suspend_reason_desc AS VARCHAR(30)), synchronization_state_desc = CAST(drs.synchronization_state_desc AS VARCHAR(30)), synchronization_health_desc = CAST(drs.synchronization_health_desc AS VARCHAR(30)), database_state_desc = CAST(drs.database_state_desc AS VARCHAR(30)), drs.last_sent_lsn, drs.last_sent_time, drs.last_received_lsn, drs.last_received_time, drs.last_hardened_lsn, drs.last_hardened_time, drs.last_redone_lsn, drs.last_redone_time, drs.log_send_queue_size, drs.log_send_rate, drs.redo_queue_size, drs.redo_rate, drs.filestream_send_rate, drs.end_of_log_lsn, drs.last_commit_lsn, drs.last_commit_time, drs.low_water_mark_for_ghosts, drs.recovery_lsn, drs.truncation_lsn, pr.file_id, pr.error_type, pr.page_id, pr.page_status, pr.modification_time FROM sys.dm_hadr_database_replica_cluster_states drcs join sys.dm_hadr_database_replica_states drs on drcs.replica_id = drs.replica_id and drcs.group_database_id = drs.group_database_id left outer join sys.dm_hadr_auto_page_repair pr on drs.database_id = pr.database_id  order by drs.database_id' AS LogMovementHealth;
            SELECT 'For more information see https://learn.microsoft.com/troubleshoot/sql/availability-groups/error-9002-transaction-log-large' AS OnlineDOCResource;
        END
        ELSE IF (@log_reuse_wait IN (10, 11, 12, 14))
        BEGIN
            SELECT 'This state is not documented and is expected to be rare and short-lived' AS Recommendation;
        END
        ELSE IF (@log_reuse_wait = 13)
        BEGIN
            SELECT 'The oldest page on the database might be older than the checkpoint log sequence number (LSN). In this case, the oldest page can delay log truncation.' AS Finding;
            SELECT 'This state should be short-lived, but if you find it is taking a long time, you can consider disabling Indirect Checkpoint temporarily' AS Recommendation;
            SELECT 'ALTER DATABASE ' + QUOTENAME(@dbname) + ' SET TARGET_RECOVERY_TIME = 0 SECONDS;' AS DisableIndirectCheckpointTemporarily;
        END
        ELSE IF (@log_reuse_wait = 16)
        BEGIN
            SELECT 'For memory-optimized tables, an automatic checkpoint is taken when transaction log file becomes bigger than 1.5 GB since the last checkpoint (includes both disk-based and memory-optimized tables)' AS Finding;
            SELECT 'Review https://learn.microsoft.com/archive/blogs/sqlcat/logging-and-checkpoint-process-for-memory-optimized-tables-2' AS ReviewBlog;
            SELECT 'USE ' + QUOTENAME(@dbname) + '; CHECKPOINT;' AS RunCheckpoint;
        END
        FETCH NEXT FROM no_truncate_db INTO
            @log_reuse_wait,
            @log_reuse_wait_desc,
            @dbname,
            @database_id,
            @recovery_model_desc;
    END

CLOSE no_truncate_db;

DEALLOCATE no_truncate_db;
```

> [!IMPORTANT]  
> If the database was in recovery when the 9002 error occurred, after resolving the problem, recover the database by using [ALTER DATABASE *database_name* SET ONLINE](../../t-sql/statements/alter-database-transact-sql-set-options.md).

### LOG_BACKUP log_reuse_wait

The most common action to consider if you see `LOG_BACKUP` or `log_reuse_wait` is to review your database recovery model, and back up the transaction log of your database.

#### Consider the database's recovery model

The transaction log might be failing to truncate with `LOG_BACKUP` or `log_reuse_wait` category, because you have never backed it up. In many of those cases, your database is using the `FULL` or `BULK_LOGGED` recovery model, but you didn't back up your transaction log. You should consider each database recovery model carefully: perform regular transaction log backups on all databases in `FULL` or `BULK_LOGGED` recovery models, to minimize occurrences of error 9002. For more information, see [Recovery models](../backup-restore/recovery-models-sql-server.md).

#### Back up the log

Under the `FULL` or `BULK_LOGGED` recovery model, if the transaction log hasn't been backed up recently, the backup might be what is preventing log truncation. You must back up the transaction log to allow log records to be released and the log truncated. If the log has never been backed up, you **must create two log backups** to permit the [!INCLUDE [ssDE](../../includes/ssde-md.md)] to truncate the log to the point of the last backup. Truncating the log frees logical space for new log records. To keep the log from filling up again, take log backups regularly and more frequently. For more information, see [Recovery models](../backup-restore/recovery-models-sql-server.md).

A complete history of all SQL Server backup and restore operations on a server instance is stored in the `msdb` system database. To review the complete backup history of a database, use the following sample script:

```sql
SELECT bs.database_name,
       CASE
           WHEN bs.type = 'D' AND bs.is_copy_only = 0 THEN 'Full Database'
           WHEN bs.type = 'D' AND bs.is_copy_only = 1 THEN 'Full Copy-Only Database'
           WHEN bs.type = 'I' THEN 'Differential database backup'
           WHEN bs.type = 'L' THEN 'Transaction Log'
           WHEN bs.type = 'F' THEN 'File or filegroup'
           WHEN bs.type = 'G' THEN 'Differential file'
           WHEN bs.type = 'P' THEN 'Partial'
           WHEN bs.type = 'Q' THEN 'Differential partial'
       END + ' Backup' AS backuptype,
       bs.recovery_model,
       bs.Backup_Start_Date AS BackupStartDate,
       bs.Backup_Finish_Date AS BackupFinishDate,
       bf.physical_device_name AS LatestBackupLocation,
       bs.backup_size / 1024. / 1024. AS backup_size_mb,
       bs.compressed_backup_size / 1024. / 1024. AS compressed_backup_size_mb,
       database_backup_lsn, -- For tlog and differential backups, this is the checkpoint_lsn of the FULL backup it is based on.
       checkpoint_lsn,
       begins_log_chain
FROM msdb.dbo.backupset AS bs
     LEFT OUTER JOIN msdb.dbo.backupmediafamily AS bf
         ON bs.[media_set_id] = bf.[media_set_id]
WHERE recovery_model IN ('FULL', 'BULK-LOGGED')
      AND bs.backup_start_date > DATEADD(month, -2, SYSDATETIME()) --only look at last two months
ORDER BY bs.database_name ASC, bs.Backup_Start_Date DESC;
```

A complete history of all SQL Server backup and restore operations on a server instance is stored in the `msdb` system database. For more information on backup history, see [Backup History and Header Information](../backup-restore/backup-history-and-header-information-sql-server.md).

#### Create a transaction log backup

Example of how to back up the log:

```sql
BACKUP LOG [dbname]
    TO DISK = 'some_volume:\some_folder\dbname_LOG.trn';
```

- [Back up a transaction log](../backup-restore/back-up-a-transaction-log-sql-server.md)
- <xref:Microsoft.SqlServer.Management.Smo.Backup.SqlBackup%2A> (SMO)

> [!IMPORTANT]  
> If the database is damaged, see [Tail-log backups](../backup-restore/tail-log-backups-sql-server.md).

### ACTIVE_TRANSACTION log_reuse_wait

The steps to troubleshoot `ACTIVE_TRANSACTION` reason include discovering the long running transaction and resolving it (in some case using the `KILL` command to do so).

#### Discover long-running transactions

A long-running transaction can cause the transaction log to fill. To look for long-running transactions, use one of the following options:

- **[sys.dm_tran_database_transactions](../system-dynamic-management-views/sys-dm-tran-database-transactions-transact-sql.md)**:

  This dynamic management view returns information about transactions at the database level. For a long-running transaction, columns of particular interest include the time of the first log record (`database_transaction_begin_time`), the current state of the transaction (`database_transaction_state`), and the [log sequence number (LSN)](../backup-restore/recover-to-a-log-sequence-number-sql-server.md) of the `BEGIN` record in the transaction log (`database_transaction_begin_lsn`).

- **[DBCC OPENTRAN](../../t-sql/database-console-commands/dbcc-opentran-transact-sql.md)**:

  This statement lets you identify the user ID of the owner of the transaction, so you can potentially track down the source of the transaction for a more orderly termination (committing it rather than rolling it back).

#### Kill a transaction

Sometimes you just have to end the transaction; you might have to use the [KILL](../../t-sql/language-elements/kill-transact-sql.md) statement. Use the `KILL` statement with extreme caution, especially when critical processes are running that you don't want to end.

### CHECKPOINT log_reuse_wait

No checkpoint has occurred since the last log truncation, or the head of the log hasn't yet moved beyond a virtual log file (VLF), in all recovery models.

This is a routine reason for delaying log truncation. If delayed, consider executing the `CHECKPOINT` command on the database or examining the log [VLFs](../sql-server-transaction-log-architecture-and-management-guide.md#physical_arch).

```sql
USE dbname;
CHECKPOINT;
SELECT *
FROM sys.dm_db_log_info(db_id('dbname'));
```

### AVAILABILITY_REPLICA log_reuse_wait

When transaction changes at the primary [availability group](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md) replica aren't yet hardened on the secondary replica, the primary replica transaction log can't be truncated. This can cause the log to grow, and occurs whether the secondary replica is set for synchronous or asynchronous commit mode. For information on how to troubleshoot this type of issue see [Error 9002. The transaction log for database is full due to AVAILABILITY_REPLICA error](/troubleshoot/sql/availability-groups/error-9002-transaction-log-large).

### Replication, change tracking, or CDC

Features such as [replication](../replication/sql-server-replication.md), [change tracking](../track-changes/about-change-tracking-sql-server.md), and [change data capture (CDC)](../track-changes/about-change-data-capture-sql-server.md) rely on the transaction log, so if transactions or changes aren't delivered, it can prevent the transaction log from truncating.

Use [DBCC OPENTRAN](../../t-sql/database-console-commands/dbcc-opentran-transact-sql.md), [Replication Monitor](../replication/monitor/monitor-performance-with-replication-monitor.md), or stored procedures for [change tracking](../system-stored-procedures/change-tracking-stored-procedures-transact-sql.md) and [CDC](../system-stored-procedures/change-data-capture-stored-procedures-transact-sql.md) to investigate and resolve any issues with these features.

### Find information on log_reuse_wait factors

For more information, see [Factors that can delay log truncation](the-transaction-log-sql-server.md#FactorsThatDelayTruncation).

## 2. Resolve full disk volume

In some situations, the disk volume that hosts the transaction log file might fill up. You can take one of the following actions to resolve the log-full scenario that results from a full disk:

### Free disk space

You might be able to free disk space on the disk drive that contains the transaction log file for the database by deleting or moving other files. The freed disk space allows the recovery system to enlarge the log file automatically.

### Move the log file to a different disk

If you can't free enough disk space on the drive that currently contains the log file, consider moving the file to another drive with sufficient space.

> [!IMPORTANT]  
> Log files should never be placed on compressed file systems.

See [Move database files](../databases/move-database-files.md) for information on how to change the location of a log file.

### Add a log file on a different disk

Add a new log file to the database on a different disk that has sufficient space by using `ALTER DATABASE <database_name> ADD LOG FILE`. Multiple log files for a single database should be considered a temporary condition to resolve a space issue, not a long-term condition. Most databases should only have one transaction log file. Continue to investigate the reason why the transaction log is full and can't be truncated. Consider adding additional temporary transaction log files only as an advanced troubleshooting step.

For more information, see [Add Data or Log Files to a Database](../databases/add-data-or-log-files-to-a-database.md).

### Utility script for recommended actions

These steps can be partly automated by running the following T-SQL script to identify logs files that use a large percentage of disk space and suggest actions:

```sql
DECLARE @log_reached_disk_size AS BIT = 0;

SELECT [name] AS LogName,
       physical_name,
       CONVERT (BIGINT, size) * 8 / 1024 AS LogFile_Size_MB,
       volume_mount_point,
       available_bytes / 1024 / 1024 AS Available_Disk_space_MB,
       (CONVERT (BIGINT, size) * 8.0 / 1024) / (available_bytes / 1024 / 1024) * 100 AS file_size_as_percentage_of_disk_space,
       db_name(mf.database_id) AS DbName
FROM sys.master_files AS mf
CROSS APPLY sys.dm_os_volume_stats(mf.database_id, file_id)
WHERE mf.[type_desc] = 'LOG'
      AND (CONVERT (BIGINT, size) * 8.0 / 1024) / (available_bytes / 1024 / 1024) * 100 > 90 --log is 90% of disk drive
ORDER BY size DESC;

IF @@ROWCOUNT > 0
    BEGIN
        SET @log_reached_disk_size = 1;

        -- Discover if any logs have filled the volume they reside on, or are close to filling the volume.
        -- Either add a new file to a new drive, or shrink an existing file.
        -- If it cannot shrink, direct the script to recommend next steps.
        DECLARE
            @db_name_filled_disk AS sysname,
            @log_name_filled_disk AS sysname,
            @go_beyond_size AS BIGINT;

        DECLARE log_filled_disk CURSOR FOR
            SELECT db_name(mf.database_id),
                    name
            FROM sys.master_files AS mf
            CROSS APPLY sys.dm_os_volume_stats(mf.database_id, file_id)
            WHERE mf.[type_desc] = 'LOG'
                    AND (CONVERT (BIGINT, size) * 8.0 / 1024) / (available_bytes / 1024 / 1024) * 100 > 90 --log is 90% of disk drive
            ORDER BY size DESC;

        OPEN log_filled_disk;

        FETCH NEXT FROM log_filled_disk INTO @db_name_filled_disk, @log_name_filled_disk;

        WHILE @@FETCH_STATUS = 0
            BEGIN
                SELECT 'Transaction log for database "' + @db_name_filled_disk + '" has nearly or completely filled disk volume it resides on!' AS Finding;
                SELECT 'Consider using one of the below commands to shrink the "' + @log_name_filled_disk + '" transaction log file size or add a new file to a NEW volume' AS Recommendation;
                SELECT 'DBCC SHRINKFILE(''' + @log_name_filled_disk + ''')' AS Shrinkfile_Command;
                SELECT 'ALTER DATABASE ' + @db_name_filled_disk + ' ADD LOG FILE ( NAME = N''' + @log_name_filled_disk + '_new'', FILENAME = N''NEW_VOLUME_AND_FOLDER_LOCATION\' + @log_name_filled_disk + '_NEW.LDF'', SIZE = 81920KB , FILEGROWTH = 65536KB )' AS AddNewFile;
                SELECT 'If shrink does not reduce the file size, likely it is because it has not been truncated. Please review next section below. See https://learn.microsoft.com/sql/t-sql/database-console-commands/dbcc-shrinkfile-transact-sql' AS TruncateFirst;
                SELECT 'Can you free some disk space on this volume? If so, do this to allow for the log to continue growing when needed.' AS FreeDiskSpace;
                FETCH NEXT FROM log_filled_disk INTO @db_name_filled_disk, @log_name_filled_disk;
            END

        CLOSE log_filled_disk;

        DEALLOCATE log_filled_disk;
    END
```

## 3. Change log size limit or enable autogrow

Error 9002 can be generated if the transaction log size is set to an upper limit, or the autogrow feature isn't allowed. In this case, enabling autogrow or increasing the log size manually can help resolve the issue. Use this T-SQL command to find such log files and follow the recommendations provided:

```sql
SELECT DB_NAME(database_id) AS DbName,
       name AS LogName,
       physical_name,
       type_desc,
       CONVERT (BIGINT, SIZE) * 8 / 1024 AS LogFile_Size_MB,
       CONVERT (BIGINT, max_size) * 8 / 1024 AS LogFile_MaxSize_MB,
       (SIZE * 8.0 / 1024) / (max_size * 8.0 / 1024) * 100 AS percent_full_of_max_size,
       CASE WHEN growth = 0 THEN 'AUTOGROW_DISABLED' ELSE 'Autogrow_Enabled' END AS AutoGrow
FROM sys.master_files
WHERE file_id = 2
      AND (SIZE * 8.0 / 1024) / (max_size * 8.0 / 1024) * 100 > 90
      AND max_size NOT IN (-1, 268435456)
      OR growth = 0;

IF @@ROWCOUNT > 0
    BEGIN
        DECLARE @db_name_max_size AS sysname, @log_name_max_size AS sysname, @configured_max_log_boundary AS BIGINT, @auto_grow AS INT;
        DECLARE reached_max_size CURSOR FOR
            SELECT db_name(database_id),
                    name,
                    CONVERT (BIGINT, SIZE) * 8 / 1024,
                    growth
            FROM sys.master_files
            WHERE file_id = 2
                AND ((SIZE * 8.0 / 1024) / (max_size * 8.0 / 1024) * 100 > 90
                    AND max_size NOT IN (-1, 268435456)
                    OR growth = 0);
        OPEN reached_max_size;
        FETCH NEXT FROM reached_max_size INTO @db_name_max_size, @log_name_max_size, @configured_max_log_boundary, @auto_grow;
        WHILE @@FETCH_STATUS = 0
        BEGIN
            IF @auto_grow = 0
                BEGIN
                    SELECT 'The database "' + @db_name_max_size + '" contains a log file "' + @log_name_max_size + '" whose autogrow has been DISABLED' AS Finding;
                    SELECT 'Consider enabling autogrow or increasing file size via these ALTER DATABASE commands' AS Recommendation;
                    SELECT 'ALTER DATABASE ' + @db_name_max_size + ' MODIFY FILE ( NAME = N''' + @log_name_max_size + ''', FILEGROWTH = 65536KB)' AS AutoGrowth;
                END
            ELSE
                BEGIN
                    SELECT 'The database "' + @db_name_max_size + '" contains a log file "' + @log_name_max_size + '" whose max limit is set to ' + CONVERT (VARCHAR (24), @configured_max_log_boundary) + ' MB and this limit has been reached!' AS Finding;
                    SELECT 'Consider using one of the below ALTER DATABASE commands to either change the log file size or add a new file' AS Recommendation;
                END
            SELECT 'ALTER DATABASE ' + @db_name_max_size + ' MODIFY FILE ( NAME = N''' + @log_name_max_size + ''', MAXSIZE = UNLIMITED)' AS UnlimitedSize;
            SELECT 'ALTER DATABASE ' + @db_name_max_size + ' MODIFY FILE ( NAME = N''' + @log_name_max_size + ''', MAXSIZE = something_larger_than_' + CONVERT (VARCHAR (24), @configured_max_log_boundary) + 'MB )' AS IncreasedSize;
            SELECT 'ALTER DATABASE ' + @db_name_max_size + ' ADD LOG FILE ( NAME = N''' + @log_name_max_size + '_new'', FILENAME = N''SOME_FOLDER_LOCATION\' + @log_name_max_size + '_NEW.LDF'', SIZE = 81920KB , FILEGROWTH = 65536KB )' AS AddNewFile;
            FETCH NEXT FROM reached_max_size INTO @db_name_max_size, @log_name_max_size, @configured_max_log_boundary, @auto_grow;
        END

        CLOSE reached_max_size;

        DEALLOCATE reached_max_size;
    END
ELSE
    SELECT 'Found no files that have reached max log file size' AS Findings;
```

### Increase log file size or enable autogrow

If space is available on the log disk, you can increase the size of the log file. The maximum size for log files is 2 terabytes (TB) per log file.

If autogrow is disabled, the database is online, and sufficient space is available on the disk, consider taking the following steps:

- Manually increase the file size to produce a single growth increment. These are [general recommendations](manage-the-size-of-the-transaction-log-file.md#Recommendations) on log size growth and size.

- Turn on autogrow by using the `ALTER DATABASE` statement to set a nonzero growth increment for the `FILEGROWTH` option. See [Considerations for the autogrow and autoshrink settings in SQL Server](/troubleshoot/sql/admin/considerations-autogrow-autoshrink).

> [!NOTE]  
> In either case, if the current size limit is reached, increase the `MAXSIZE` value.

## Related content

- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [Manage the size of the transaction log file](manage-the-size-of-the-transaction-log-file.md)
- [Transaction log backups (SQL Server)](../backup-restore/transaction-log-backups-sql-server.md)
- [sp_add_log_file_recover_suspect_db (Transact-SQL)](../system-stored-procedures/sp-add-log-file-recover-suspect-db-transact-sql.md)
- [MSSQLSERVER_9002](../errors-events/mssqlserver-9002-database-engine-error.md)
- [How a log file structure can affect database recovery time - Microsoft Tech Community](https://techcommunity.microsoft.com/blog/sqlserversupport/how-a-log-file-structure-can-affect-database-recovery-time/315780)
