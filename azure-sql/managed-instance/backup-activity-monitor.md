---
title: "Monitor backup activity"
titleSuffix: Azure SQL Managed Instance
description: Learn how to monitor Azure SQL Managed Instance backup activity using extended events.
author: MilanMSFT
ms.author: mlazic
ms.reviewer: mathoma, nvraparl
ms.date: 11/16/2022
ms.service: sql-managed-instance
ms.subservice: backup-restore
ms.topic: quickstart
ms.custom: mode-other
---
# Monitor backup activity for Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article teaches you how to monitor backup activity for [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md) by either querying the msdb database or by configuring extended event (XEvent) sessions.

## Overview

Azure SQL Managed Instance stores backup information in the [msdb database](backup-transparency.md) and also emits events (also known as [Extended Events or XEvents](../database/xevent-db-diff-from-svr.md)) during backup activity for the purpose of reporting. Configure an XEvent session to track information such as backup status, backup type, size, time, and location within the msdb database. This information can be integrated with backup monitoring software and also used for the purpose of Enterprise Audit. 

Enterprise Audits may require proof of successful backups, time of backup, and duration of the backup.

## Query msdb database

To view backup activity, query the msdb database: 

```sql
SELECT bs.database_name,
    backuptype = CASE 
        WHEN bs.type = 'D' AND bs.is_copy_only = 0 THEN 'Full Database'
        WHEN bs.type = 'D' AND bs.is_copy_only = 1 THEN 'Full Copy-Only Database'
        WHEN bs.type = 'I' THEN 'Differential database backup'
        WHEN bs.type = 'L' THEN 'Transaction Log'
        WHEN bs.type = 'F' THEN 'File or filegroup'
        WHEN bs.type = 'G' THEN 'Differential file'
        WHEN bs.type = 'P' THEN 'Partial'
        WHEN bs.type = 'Q' THEN 'Differential partial'
        END + ' Backup',
    CASE bf.device_type
        WHEN 2 THEN 'Disk'
        WHEN 5 THEN 'Tape'
        WHEN 7 THEN 'Virtual device'
        WHEN 9 THEN 'Azure Storage'
        WHEN 105 THEN 'A permanent backup device'
        ELSE 'Other Device'
        END AS DeviceType,
    bms.software_name AS backup_software,
    bs.recovery_model,
    bs.compatibility_level,
    BackupStartDate = bs.Backup_Start_Date,
    BackupFinishDate = bs.Backup_Finish_Date,
    LatestBackupLocation = bf.physical_device_name,
    backup_size_mb = CONVERT(DECIMAL(10, 2), bs.backup_size / 1024. / 1024.),
    compressed_backup_size_mb = CONVERT(DECIMAL(10, 2), bs.compressed_backup_size / 1024. / 1024.),
    database_backup_lsn, -- For tlog and differential backups, this is the checkpoint_lsn of the FULL backup it is based on.
    checkpoint_lsn,
    begins_log_chain,
    bms.is_password_protected
FROM msdb.dbo.backupset bs
LEFT JOIN msdb.dbo.backupmediafamily bf
    ON bs.[media_set_id] = bf.[media_set_id]
INNER JOIN msdb.dbo.backupmediaset bms
    ON bs.[media_set_id] = bms.[media_set_id]
WHERE bs.backup_start_date > DATEADD(MONTH, - 2, sysdatetime()) --only look at last two months
ORDER BY bs.database_name ASC,
    bs.Backup_Start_Date DESC;
```

## Configure XEvent session

Use the extended event `backup_restore_progress_trace` to record the progress of your SQL Managed Instance back up. Modify the XEvent sessions as needed to track the information you're interested in for your business. These T-SQL snippets store the XEvent sessions in the ring buffer, but it's also possible to write to [Azure Blob Storage](../database/xevent-code-event-file.md). XEvent sessions storing data in the ring buffer have a limit of about 1000 messages so should only be used to track recent activity. Additionally, ring buffer data is lost upon failover. As such, for a historical record of backups, write to an event file instead. 

### Simple tracking

Configure a simple XEvent session to capture simple events about complete full backups. This script collects the name of the database, the total number of bytes processed, and the time the backup completed.

Use Transact-SQL (T-SQL) to configure the simple XEvent session: 


```sql
CREATE EVENT SESSION [Simple backup trace] ON SERVER
ADD EVENT sqlserver.backup_restore_progress_trace(
WHERE operation_type = 0
AND trace_message LIKE '%100 percent%')
ADD TARGET package0.ring_buffer
WITH(STARTUP_STATE=ON)
GO
ALTER EVENT SESSION [Simple backup trace] ON SERVER
STATE = start;
```



### Verbose tracking

Configure a verbose XEvent session to track greater details about your backup activity. This script captures start and finish of both full, differential and log backups. Since this script is more verbose, it fills up the ring buffer faster, so entries may recycle faster than with the simple script. 

Use Transact-SQL (T-SQL) to configure the verbose XEvent session: 

```sql
CREATE EVENT SESSION [Verbose backup trace] ON SERVER 
ADD EVENT sqlserver.backup_restore_progress_trace(
    WHERE (
              [operation_type]=(0) AND (
              [trace_message] like '%100 percent%' OR 
              [trace_message] like '%BACKUP DATABASE%' OR [trace_message] like '%BACKUP LOG%'))
       )
ADD TARGET package0.ring_buffer
WITH (MAX_MEMORY=4096 KB,EVENT_RETENTION_MODE=ALLOW_SINGLE_EVENT_LOSS,
       MAX_DISPATCH_LATENCY=30 SECONDS,MAX_EVENT_SIZE=0 KB,MEMORY_PARTITION_MODE=NONE,
       TRACK_CAUSALITY=OFF,STARTUP_STATE=ON)

ALTER EVENT SESSION [Verbose backup trace] ON SERVER
STATE = start;

```

## Monitor backup progress 

After the XEvent session is created, you can use Transact-SQL (T-SQL) to query ring buffer results and monitor the progress of the backup. Once the XEvent starts, it collects all backup events so entries are added to the session roughly every 5-10 minutes.  

### Simple tracking

The following Transact-SQL (T-SQL) code queries the simple XEvent session and returns the name of the database, the total number of bytes processed, and the time the backup completed: 

```sql 
WITH
a AS (SELECT xed = CAST(xet.target_data AS xml)
FROM sys.dm_xe_session_targets AS xet
JOIN sys.dm_xe_sessions AS xe
ON (xe.address = xet.event_session_address)
WHERE xe.name = 'Backup trace'),
b AS(SELECT
d.n.value('(@timestamp)[1]', 'datetime2') AS [timestamp],
ISNULL(db.name, d.n.value('(data[@name="database_name"]/value)[1]', 'varchar(200)')) AS database_name,
d.n.value('(data[@name="trace_message"]/value)[1]', 'varchar(4000)') AS trace_message
FROM a
CROSS APPLY  xed.nodes('/RingBufferTarget/event') d(n)
LEFT JOIN master.sys.databases db
ON db.physical_database_name = d.n.value('(data[@name="database_name"]/value)[1]', 'varchar(200)'))
SELECT * FROM b
```

The following screenshot shows an example of the output of the above query: 

![Screenshot of the xEvent output](./media/backup-activity-monitor/present-xevents-output.png)

In this example, five databases were automatically backed up over the course of 2 hours and 30 minutes, and there are 130 entries in the XEvent session. 

### Verbose tracking 

The following Transact-SQL (T-SQL) code queries the verbose XEvent session and returns the name of the database, as well as the start and finish of both full, differential and log backups. 


```sql
WITH
a AS (SELECT xed = CAST(xet.target_data AS xml)
FROM sys.dm_xe_session_targets AS xet
JOIN sys.dm_xe_sessions AS xe
ON (xe.address = xet.event_session_address)
WHERE xe.name = 'Verbose backup trace'),
b AS(SELECT
d.n.value('(@timestamp)[1]', 'datetime2') AS [timestamp],
ISNULL(db.name, d.n.value('(data[@name="database_name"]/value)[1]', 'varchar(200)')) AS database_name,
d.n.value('(data[@name="trace_message"]/value)[1]', 'varchar(4000)') AS trace_message
FROM a
CROSS APPLY  xed.nodes('/RingBufferTarget/event') d(n)
LEFT JOIN master.sys.databases db
ON db.physical_database_name = d.n.value('(data[@name="database_name"]/value)[1]', 'varchar(200)'))
SELECT * FROM b
```

The following screenshot shows an example of a full backup in the XEvent session:

:::image type="content" source="media/backup-activity-monitor/output-with-full.png" alt-text="XEvent output showing full backups":::

The following screenshot shows an example of an output of a differential backup in the XEvent session:

:::image type="content" source="media/backup-activity-monitor/output-with-differential.png" alt-text="XEvent output showing differential backups":::


## Next steps

Once your backup has completed, you can then [restore to a point in time](point-in-time-restore.md) or [configure a long-term retention policy](long-term-backup-retention-configure.md). 

To learn more, see [automated backups](../database/automated-backups-overview.md). 
