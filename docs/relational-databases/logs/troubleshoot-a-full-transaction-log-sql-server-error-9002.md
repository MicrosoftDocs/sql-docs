---
title: "Troubleshoot full transaction log error 9002"
description: Learn about possible responses to a full transaction log in SQL Server and how to avoid the problem in the future.
ms.date: "06/22/2021"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "logs [SQL Server], full"
  - "troubleshooting [SQL Server], full transaction log"
  - "9002 (Database Engine error)"
  - "transaction logs [SQL Server], truncation"
  - "backing up transaction logs [SQL Server], full logs"
  - "transaction logs [SQL Server], full log"
  - "full transaction logs [SQL Server]"
author: "MashaMSFT"
ms.author: "mathoma"
ms.custom: "seo-lt-2019"
---
# Troubleshoot a Full Transaction Log (SQL Server Error 9002)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic discusses possible responses to a full transaction log and suggests how to avoid it in the future. 
  
  When the transaction log becomes full, [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] issues a **9002 error**. The log can fill when the database is online, or in recovery. If the log fills while the database is online, the database remains online but can only be read, not updated. If the log fills during recovery, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] marks the database as RESOURCE PENDING. In either case, user action is required to make log space available.  

> [!NOTE]
> **This article is focused on SQL Server.** For more specific information on this error in Azure SQL Database and Azure SQL Managed Instance, see [Troubleshooting transaction log errors with Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/database/troubleshoot-transaction-log-errors-issues). Azure SQL Database and Azure SQL Managed Instance are based on the latest stable version of the Microsoft SQL Server database engine, so much of the content is similar though troubleshooting options and tools may differ.
  
## Common reasons for a full transaction log
 The appropriate response to a full transaction log depends on what conditions caused the log to fill.  Common causes include: 
 - Log not being truncated
 - Disk volume is full
 - Log size is set to a fixed maximum value (autogrow is disabled)
 - Replication or availability group synchronization that is unable to complete

## Resolving a full transaction log

The following specific steps will help you find the reason for a full transaction log and resolve the issue.

## Truncate the Log

There is a difference between truncating a transaction log and shrinking a transaction log. [Truncation](the-transaction-log-sql-server.md#Truncation) is a logical operation which removes committed records inside the log, whereas [log shrinking](../../t-sql/database-console-commands/dbcc-shrinkfile-transact-sql.md#shrinking-a-log-file) reclaims physical space on the file system by reducing the file size. Log truncation occurs on [virtual-log-file (VLF)](../sql-server-transaction-log-architecture-and-management-guide.md#physical_arch) boundary. A log file can be shrunk only if it has been truncated. Thus there is a sequence: first you truncate a log and then you can optionally shrink it. Shrinking may not always be a desired step as the file may need to grow again in the future. Therefore in some cases you may decide to not shrink the file.

To discover what is preventing log truncation in a given case, use the `log_reuse_wait` and `log_reuse_wait_desc` columns of the `sys.databases` catalog view. For more information, see [sys.databases &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md). For descriptions of factors that can delay log truncation, see [The Transaction Log &#40;SQL Server&#41;](../../relational-databases/logs/the-transaction-log-sql-server.md). 

The following set of T-SQL commands will help you identify if a database transaction log is not truncated and the reason for it. The following script will also recommend steps to resolve the issue:


```tsql
SET NOCOUNT ON
DECLARE @SQL VARCHAR (8000), @log_reuse_wait tinyint, @log_reuse_wait_desc nvarchar(120), @dbname sysname, @database_id int, @recovery_model_desc varchar (24)

DROP TABLE IF EXISTS #CannotTruncateLog_Db

SELECT 
    sdb.name as DbName, 
    sdb.log_reuse_wait, sdb.log_reuse_wait_desc, 
    log_reuse_wait_explanation = CASE

        when log_reuse_wait = 1 then 'No checkpoint has occurred since the last log truncation, or the head of the log has not yet moved beyond'
        when log_reuse_wait = 2 then 'A log backup is required before the transaction log can be truncated.'
        when log_reuse_wait = 3 then 'A data backup or a restore is in progress (all recovery models). Please wait or cancel backup'
        when log_reuse_wait = 4 then 'A long-running active transaction or a defferred transaction is keeping log from being truncated. You can attempt a log backup to free space or complete/rollback long transaction'
        when log_reuse_wait = 5 then 'Database mirroring is paused, or under high-performance mode, the mirror database is significantly behind the principal database. (Full recovery model only)'        
        when log_reuse_wait = 6 then 'During transactional replications, transactions relevant to the publications are still undelivered to the distribution database. (Full recovery model only)'        
        when log_reuse_wait = 7 then 'A database snapshot is being created. This is a routine, and typically brief, cause of delayed log truncation.'
        when log_reuse_wait = 8 then 'A transaction log scan is occurring. This is a routine, and typically a brief cause of delayed log truncation.'
        when log_reuse_wait = 9 then 'A secondary replica of an availability group is applying transaction log records of this database to a corresponding secondary database. (Full recovery model)'
        when log_reuse_wait = 13 then 'If a database is configured to use indirect checkpoints, the oldest page on the database might be older than the checkpoint log sequence number (LSN)'
        when log_reuse_wait = 16 then 'An In-Memory OLTP checkpoint has not occurred since the last log truncation, or the head of the log has not yet moved beyond a VLF)'
    else 'None' end,
    sdb.database_id,
    sdb.recovery_model_desc,
    ls.total_log_size_mb,
    ls.active_log_size_mb,
    ls.total_log_size_mb - ls.active_log_size_mb as Free_Space_mb
INTO #CannotTruncateLog_Db
FROM sys.databases sdb cross apply sys.dm_db_log_stats(database_id) ls
WHERE sdb.log_reuse_wait != 0

select * from #CannotTruncateLog_Db

DECLARE no_truncate_db CURSOR FOR
    select log_reuse_wait, log_reuse_wait_desc, dbname, database_id, recovery_model_desc from #CannotTruncateLog_Db

OPEN no_truncate_db

FETCH NEXT FROM no_truncate_db into @log_reuse_wait, @log_reuse_wait_desc, @dbname, @database_id, @recovery_model_desc

WHILE @@FETCH_STATUS = 0
BEGIN
    if (@log_reuse_wait is not null)
        select '-- ''' + @dbname +  ''' database --' as "Individual Database Report"

    if (@log_reuse_wait = 1)
    BEGIN
        select 'Consider running the checkpoint command to attempt resolving this issue or further t-shooting may be required on the checkpoint process. Also, examine the log for active VLFs at the end of file' as Recommendation
        select 'USE ''' + @dbname+ '''; CHECKPOINT' as CheckpointCommand
        select 'select * from sys.dm_db_log_info(' + convert(varchar,@database_id)+ ')' as VLF_LogInfo
    END
    else if (@log_reuse_wait = 2)
    BEGIN
        select 'Is '+ @recovery_model_desc +' recovery model the intended choice for your database? Review recovery models and determine if you need to change it. https://docs.microsoft.com/en-us/sql/relational-databases/backup-restore/recovery-models-sql-server?view=sql-server-ver15'
        select 'To truncate the log consider performing a transaction log backup on database ''' + @dbname+ ''' which is in ' + @recovery_model_desc +' recovery model. Be mindful of any existing log backup chains that could be broken' as Recommendation
        select 'BACKUP LOG [' + @dbname + '] TO DISK = ''some_volume:\some_folder' + @dbname + '_LOG.trn''' as BackupLogCommand
    END
    else if (@log_reuse_wait = 3)
    BEGIN
        select 'Either wait for or cancel any active backups currently running for database ''' +@dbname+ '''. To check for backups, run this command:' as Recommendation
        select 'select * from sys.dm_exec_requests where command like ''backup%'' or command like ''restore%''' as FindBackupOrRestore
    END
    else if (@log_reuse_wait = 4)
    BEGIN
        select 'Active transactions currently running  for database ''' +@dbname+ '''. To check for active transactions, run these commands:' as Recommendation
        select 'DBCC OPENTRAN (''' +@dbname+ ''')' as FindOpenTran
        select 'select database_id, db_name(database_id) dbname, database_transaction_begin_time, database_transaction_state, database_transaction_log_record_count, database_transaction_log_bytes_used, database_transaction_begin_lsn, stran.session_id from sys.dm_tran_database_transactions dbtran left outer join sys.dm_tran_session_transactions stran on dbtran.transaction_id = stran.transaction_id where database_id = ' + convert(varchar, @database_id) as FindOpenTransAndSession
    END

    else if (@log_reuse_wait = 5)
    BEGIN
        select 'Database Mirroring for database ''' +@dbname+ ''' is behind on synchronization. To check the state of DBM, run the commands below:' as Recommendation
        select 'select db_name(database_id), mirroring_state_desc, mirroring_role_desc, mirroring_safety_level_desc from sys.database_mirroring where mirroring_guid is not null and mirroring_state <> 4 and database_id = ' + convert(sysname, @database_id)  as CheckMirroringStatus
        
        select 'Database Mirroring for database ''' +@dbname+ ''' may be behind: check unsent_log, send_rate, unrestored_log, recovery_rate, average_delay in this output' as Recommendation
        select 'exec msdb.sys.sp_dbmmonitoraddmonitoring 1; exec msdb.sys.sp_dbmmonitorresults ''' + @dbname+ ''', 5, 0; waitfor delay ''00:01:01''; exec msdb.sys.sp_dbmmonitorresults ''' + @dbname+ '''; exec msdb.sys.sp_dbmmonitordropmonitoring'   as CheckMirroringStatusAnd
    END

    else if (@log_reuse_wait = 6)
    BEGIN
        select 'Replication transactions still undelivered from publisher database ''' +@dbname+ ''' to Distribution database. Check the oldest non-distributed replication transaction. Also check if the Log Reader Agent is running and if it has encoutered any errors' as Recommendation
        select 'DBCC OPENTRAN  (''' + @dbname + ''')' as CheckOldestNonDistributedTran
        select 'select top 5 * from distribution..MSlogreader_history where runstatus in (6, 5) or error_id <> 0 and agent_id = find_in_mslogreader_agents_table  order by time desc ' as LogReaderAgentState
    END
    
    else if (@log_reuse_wait = 9)
    BEGIN
        select 'Always On transactions still undelivered from primary database ''' +@dbname+ ''' to Secondary replicas. Check the Health of AG nodes and if there is latency is Log block movement to Secondaries' as Recommendation
        select 'select availability_group=cast(ag.name as varchar(30)), primary_replica=cast(ags.primary_replica as varchar(30)),primary_recovery_health_desc=cast(ags.primary_recovery_health_desc as varchar(30)), synchronization_health_desc=cast(ags.synchronization_health_desc as varchar(30)),ag.failure_condition_level, ag.health_check_timeout, automated_backup_preference_desc=cast(ag.automated_backup_preference_desc as varchar(10))  from sys.availability_groups ag join sys.dm_hadr_availability_group_states ags on ag.group_id=ags.group_id' as CheckAGHealth
        select 'SELECT  group_name=cast(arc.group_name as varchar(30)), replica_server_name=cast(arc.replica_server_name as varchar(30)), node_name=cast(arc.node_name as varchar(30)),role_desc=cast(ars.role_desc as varchar(30)), ar.availability_mode_Desc, operational_state_desc=cast(ars.operational_state_desc as varchar(30)), connected_state_desc=cast(ars.connected_state_desc as varchar(30)), recovery_health_desc=cast(ars.recovery_health_desc as varchar(30)), synhcronization_health_desc=cast(ars.synchronization_health_desc as varchar(30)), ars.last_connect_error_number, last_connect_error_description=cast(ars.last_connect_error_description as varchar(30)), ars.last_connect_error_timestamp, primary_role_allow_connections_desc=cast(ar.primary_role_allow_connections_desc as varchar(30)) from sys.dm_hadr_availability_replica_cluster_nodes arc join sys.dm_hadr_availability_replica_cluster_states arcs on arc.replica_server_name=arcs.replica_server_name join sys.dm_hadr_availability_replica_states ars on arcs.replica_id=ars.replica_id join sys.availability_replicas ar on ars.replica_id=ar.replica_id join sys.availability_groups ag on ag.group_id = arcs.group_id and ag.name = arc.group_name ORDER BY cast(arc.group_name as varchar(30)), cast(ars.role_desc as varchar(30))' as CheckReplicaHealth
        select 'select database_name=cast(drcs.database_name as varchar(30)), drs.database_id, drs.group_id, drs.replica_id, drs.is_local,drcs.is_failover_ready,drcs.is_pending_secondary_suspend, drcs.is_database_joined, drs.is_suspended, drs.is_commit_participant, suspend_reason_desc=cast(drs.suspend_reason_desc as varchar(30)), synchronization_state_desc=cast(drs.synchronization_state_desc as varchar(30)), synchronization_health_desc=cast(drs.synchronization_health_desc as varchar(30)), database_state_desc=cast(drs.database_state_desc as varchar(30)), drs.last_sent_lsn, drs.last_sent_time, drs.last_received_lsn, drs.last_received_time, drs.last_hardened_lsn, drs.last_hardened_time,drs.last_redone_lsn, drs.last_redone_time, drs.log_send_queue_size, drs.log_send_rate, drs.redo_queue_size, drs.redo_rate, drs.filestream_send_rate, drs.end_of_log_lsn, drs.last_commit_lsn, drs.last_commit_time, drs.low_water_mark_for_ghosts, drs.recovery_lsn, drs.truncation_lsn, pr.file_id, pr.error_type, pr.page_id, pr.page_status, pr.modification_time from sys.dm_hadr_database_replica_cluster_states drcs join sys.dm_hadr_database_replica_states drs on drcs.replica_id=drs.replica_id and drcs.group_database_id=drs.group_database_id left outer join sys.dm_hadr_auto_page_repair pr on drs.database_id=pr.database_id  order by drs.database_id' as LogMovementHealth
    END    
    else if (@log_reuse_wait in (10, 11, 12, 14))
    BEGIN
        select 'This state is not documented and is expected to be rare and short-lived' as Recommendation
    END    
    else if (@log_reuse_wait = 13)
    BEGIN
        select 'The oldest page on the database might be older than the checkpoint log sequence number (LSN). In this case, the oldest page can delay log truncation.' as Finding
        select 'This state should be short-lived, but if you find it is taking a long time, you can consider disabling Indirect Checkpoint temporarily' as Recommendation
        select 'ALTER DATABASE [' +@dbname+ '] SET TARGET_RECOVERY_TIME = 0' as DisableIndirectCheckpointTemporarily
    END    
    else if (@log_reuse_wait = 16)
    BEGIN
        select 'For memory-optimized tables, an automatic checkpoint is taken when transaction log file becomes bigger than 1.5 GB since the last checkpoint (includes both disk-based and memory-optimized tables)' as Finding
        select 'Review https://blogs.msdn.microsoft.com/sqlcat/2016/05/20/logging-and-checkpoint-process-for-memory-optimized-tables-2/' as ReviewBlog
        select 'use ' +@dbname+ ' CHECKPOINT' as RunCheckpoint
    END    

    FETCH NEXT FROM no_truncate_db into @log_reuse_wait, @log_reuse_wait_desc, @dbname, @database_id, @recovery_model_desc

END

CLOSE no_truncate_db
DEALLOCATE no_truncate_db

```

> [!IMPORTANT]  
>  If the database was in recovery when the 9002 error occurred, after resolving the problem, recover the database by using [ALTER DATABASE *database_name* SET ONLINE.](../../t-sql/statements/alter-database-transact-sql-set-options.md)  
  
 More information about the following two actions is provided below:  

- Backing up the log 
- Completing or killing a long-running transaction

### Backing up the log  

Under the full recovery model or bulk-logged recovery model, if the transaction log has not been backed up recently, backup might be what is preventing log truncation. If the log has never been backed up, you **must create two log backups** to permit the [!INCLUDE[ssDE](../../includes/ssde-md.md)] to truncate the log to the point of the last backup. Truncating the log frees logical space for new log records. To keep the log from filling up again, take log backups regularly and more frequently.  
  
 **To create a transaction log backup**  
  
> [!IMPORTANT]  
> If the database is damaged, see [Tail-Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/tail-log-backups-sql-server.md).  
  
- [Back Up a Transaction Log &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-a-transaction-log-sql-server.md)  
  
- <xref:Microsoft.SqlServer.Management.Smo.Backup.SqlBackup%2A> (SMO)  
  

### Discovering long-running transactions

A very long-running transaction can cause the transaction log to fill. To look for long-running transactions, use one of the following:

- **[sys.dm_tran_database_transactions](../system-dynamic-management-views/sys-dm-tran-database-transactions-transact-sql.md).**

This dynamic management view returns information about transactions at the database level. For a long-running transaction, columns of particular interest include the time of the first log record [(database_transaction_begin_time)](../system-dynamic-management-views/sys-dm-tran-database-transactions-transact-sql.md), the current state of the transaction [(database_transaction_state)](../system-dynamic-management-views/sys-dm-tran-database-transactions-transact-sql.md), and the [log sequence number (LSN)](../backup-restore/recover-to-a-log-sequence-number-sql-server.md) of the begin record in the transaction log [(database_transaction_begin_lsn)](../system-dynamic-management-views/sys-dm-tran-database-transactions-transact-sql.md).

- **[DBCC OPENTRAN](../../t-sql/database-console-commands/dbcc-opentran-transact-sql.md).**
This statement lets you identify the user ID of the owner of the transaction, so you can potentially track down the source of the transaction for a more orderly termination (committing it rather than rolling it back).

### Kill a transaction

Sometimes you just have to end the process; you may have to use the [KILL](../../t-sql/language-elements/kill-transact-sql.md) statement. Please use this statement very carefully,  especially when critical processes are running that you don't want to kill. For more information, see [KILL (Transact-SQL)](../../t-sql/language-elements/kill-transact-sql.md)

## Disk volume is full

In some situations the disk volume that hosts the transaction log file may fill up. You can take one of the following actions to resolve the log-full scenario that results from a full disk:

### Free disk space  

 You might be able to free disk space on the disk drive that contains the transaction log file for the database by deleting or moving other files. The freed disk space allows the recovery system to enlarge the log file automatically.  
  
### Move the log file to a different disk  

If you cannot free enough disk space on the drive that currently contains the log file, consider moving the file to another drive with sufficient space.  
  
> [!IMPORTANT]
> Log files should never be placed on compressed file systems.  
  
See [Move Database Files](../../relational-databases/databases/move-database-files.md) for information on how to change the location of a log file.
  
### Add a log file on a different disk  

Add a new log file to the database on a different disk that has sufficient space by using ALTER DATABASE <database_name> ADD LOG FILE.  
  
For more information see [Add Data or Log Files to a Database](../../relational-databases/databases/add-data-or-log-files-to-a-database.md)  

### Use T-SQL to automate

These steps can be partly-automated by running this T-SQL script which will indentify logs files that using a large percentage of disk space and suggest actions:

```tsql
DECLARE @log_reached_disk_size BIT = 0

SELECT 
    name LogName, 
    physical_name, 
    convert(bigint, size)*8/1024 LogFile_Size_MB, 
    volume_mount_point, 
    available_bytes/1024/1024 Available_Disk_space_MB,
    (convert(bigint, size)*8.0/1024)/(available_bytes/1024/1024 )*100 file_size_as_percentage_of_disk_space,
    db_name(mf.database_id) DbName
from sys.master_files mf cross apply sys.dm_os_volume_stats (mf.database_id, file_id)
where mf.[type_desc] = 'LOG'
    and (convert(bigint, size)*8.0/1024)/(available_bytes/1024/1024 )*100 > 90 --log is 90% of disk drive
order by size desc

if @@ROWCOUNT > 0
BEGIN

    set @log_reached_disk_size = 1

    -- Discover if any logs have are close to or completely filled disk volume they reside on.
    -- Either Add A New File To A New Drive, Or Shrink Existing File
    -- If Cannot Shrink, Go To Cannot Truncate Section

    DECLARE @db_name_filled_disk sysname, @log_name_filled_disk sysname, @go_beyond_size bigint 
    
    DECLARE log_filled_disk CURSOR FOR
        select 
            db_name(mf.database_id),
            name
        from sys.master_files mf cross apply sys.dm_os_volume_stats (mf.database_id, file_id)
        where mf.[type_desc] = 'LOG'
            and (convert(bigint, size)*8.0/1024)/(available_bytes/1024/1024 )*100 > 90 --log is 90% of disk drive
        order by size desc

    OPEN log_filled_disk

    FETCH NEXT FROM log_filled_disk into @db_name_filled_disk , @log_name_filled_disk

    WHILE @@FETCH_STATUS = 0
    BEGIN
        
        SELECT 'Transaction log for database "' + @db_name_filled_disk + '" has nearly or comletely filled disk volume it resides on!' as Finding
        SELECT 'Consider using one of the below commands to shrink the "' + @log_name_filled_disk +'" transaction log file size or add a new file to a NEW volume' as Recommendation
        SELECT 'DBCC SHRINKFILE(''' + @log_name_filled_disk + ''')' as Shrinfile_Command
        SELECT 'ALTER DATABASE ' + @db_name_filled_disk + ' ADD LOG FILE ( NAME = N''' + @log_name_filled_disk + '_new'', FILENAME = N''NEW_VOLUME_AND_FOLDER_LOCATION\' + @log_name_filled_disk + '_NEW.LDF'', SIZE = 81920KB , FILEGROWTH = 65536KB )' as AddNewFile
        SELECT 'If shrink does not reduce the file size, likely it is because it has not been truncated. Please review next section below. See https://docs.microsoft.com/en-us/sql/t-sql/database-console-commands/dbcc-shrinkfile-transact-sql?view=sql-server-ver15' TruncateFirst
        SELECT 'Can you free some disk space on this volume? If so, do this to allow for the log to continue growing when needed.' FreeDiskSpace


         FETCH NEXT FROM log_filled_disk into @db_name_filled_disk , @log_name_filled_disk

    END

    CLOSE log_filled_disk
    DEALLOCATE log_filled_disk

END

```

## Log size is set to a fixed maximum value

Error 9002 can be generated if the transaction log size has been set to an upper limit and autogrow is not allowed. In this case, enabling autogrow or increasing the log size manually can help resolve the issue. Use this T-SQL command to find such log files and follow the recommendations provided:

```tsql
--are any files set to a fixed size and what percentage of max size are they 
SELECT db_name(database_id) DbName,
       name LogName,
       physical_name,
       type_desc ,
       convert(bigint, SIZE)*8/1024 LogFile_Size_MB ,
       convert(bigint,max_size)*8/1024 LogFile_MaxSize_MB ,
       (SIZE*8.0/1024)/(max_size*8.0/1024)*100 percent_full_of_max_size
FROM sys.master_files
WHERE file_id = 2
  AND max_size not in (-1,
                       268435456)
  AND (SIZE*8.0/1024)/(max_size*8.0/1024)*100 > 90

if @@ROWCOUNT > 0
BEGIN
    DECLARE @db_name_max_size sysname, @log_name_max_size sysname, @configured_max_log_boundary bigint 
    
    DECLARE reached_max_size CURSOR FOR
        SELECT db_name(database_id),
               name,
               convert(bigint, SIZE)*8/1024
        FROM sys.master_files
        WHERE file_id = 2
          AND max_size not in (-1,
                               268435456)
          AND (SIZE*8.0/1024)/(max_size*8.0/1024)*100 > 90

    OPEN reached_max_size

    FETCH NEXT FROM reached_max_size into @db_name_max_size , @log_name_max_size, @configured_max_log_boundary 

    WHILE @@FETCH_STATUS = 0
    BEGIN
        SELECT 'The database "' + @db_name_max_size+'" contains a log file "' + @log_name_max_size + '" whose max limit is set to ' + convert(varchar(24), @configured_max_log_boundary) + ' MB and this limit has been reached!' as Finding
        SELECT 'Consider using one of the below ALTER DATABASE commands to either change the log file size or add a new file' as Recommendation
        SELECT 'ALTER DATABASE ' + @db_name_max_size + ' MODIFY FILE ( NAME = N''' + @log_name_max_size + ''', MAXSIZE = UNLIMITED)' as UnlimitedSize
        SELECT 'ALTER DATABASE ' + @db_name_max_size + ' MODIFY FILE ( NAME = N''' + @log_name_max_size + ''', MAXSIZE = something_larger_than_' + CONVERT(varchar(24), @configured_max_log_boundary) +'MB )' as IncreasedSize
        SELECT 'ALTER DATABASE ' + @db_name_max_size + ' ADD LOG FILE ( NAME = N''' + @log_name_max_size + '_new'', FILENAME = N''SOME_FOLDER_LOCATION\' + @log_name_max_size + '_NEW.LDF'', SIZE = 81920KB , FILEGROWTH = 65536KB )' as AddNewFile

        FETCH NEXT FROM reached_max_size into @db_name_max_size , @log_name_max_size, @configured_max_log_boundary 

    END

    CLOSE reached_max_size
    DEALLOCATE reached_max_size
END
ELSE
    SELECT 'Found no files that have reached max log file size' as Findings
```

### Increase log file size  

If space is available on the log disk, you can increase the size of the log file. The maximum size for log files is two terabytes (TB) per log file.  
  
If autogrow is disabled, the database is online, and sufficient space is available on the disk, do either of these:  
  
- Manually increase the file size to produce a single growth increment.  
- Turn on autogrow by using the ALTER DATABASE statement to set a non-zero growth increment for the FILEGROWTH option.  
  
> [!NOTE]
> In either case, if the current size limit has been reached, increase the MAXSIZE value.  
  
## See also

 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 [Manage the Size of the Transaction Log File](../../relational-databases/logs/manage-the-size-of-the-transaction-log-file.md)   
 [Transaction Log Backups &#40;SQL Server&#41;](../../relational-databases/backup-restore/transaction-log-backups-sql-server.md)   
 [sp_add_log_file_recover_suspect_db &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-log-file-recover-suspect-db-transact-sql.md)  
