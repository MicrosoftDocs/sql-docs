---
description: "MSSQLSERVER_3023"
title: MSSQLSERVER_3023
ms.custom: ""
ms.date: 08/20/2020
ms.service: sql
ms.reviewer: ramakoni1, pijocoder, suresh-kandoth, Masha
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "3023 (Database Engine error)"
ms.assetid: 
author: suresh-kandoth
ms.author: ramakoni
---
# MSSQLSERVER_3023
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

|Attribute|Value|
|---|---|
|Product Name|SQL Server|
|Event ID|3023|
|Event Source|MSSQLSERVER|
|Component|SQLEngine|
|Symbolic Name|DB_IN_USE_DUMP|
|Message Text|Backup and file manipulation operations (such as ALTER DATABASE ADD FILE) on a database must be serialized. Reissue the statement after the current backup or file manipulation operation is completed|

## Explanation

You try to run a Backup, shrink, or alter database command in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and you encounter the following messages:

> Msg 3023, Level 16, State 2, Line 1  
Backup and file manipulation operations (such as ALTER DATABASE ADD FILE) on a database must be serialized. Reissue the statement after the current backup or file manipulation operation is completed.

> Msg 3013, Level 16, State 1, Line 1  
BACKUP DATABASE is terminating abnormally.

Additionally, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log contains messages like the following:

> \<Datetime> Backup Error: 3041, Severity: 16, State: 1.  
\<Datetime> Backup BACKUP failed to complete the command BACKUP DATABASE MyDatabase WITH DIFFERENTIAL. Check the backup application log for detailed messages.

You might also notice that these commands encounter a `wait_type = LCK_M_U` and a `wait_resource = DATABASE: <id> [BULKOP_BACKUP_DB]` when the status of these commands is viewed from the various dynamic management views (DMVs), such as from `sys.dm_exec_requests` or `sys.dm_os_waiting_tasks`.

## Possible causes

There are several rules on which operations are allowed or not allowed when a full database is currently in progress against a database. Some examples are as follows:

- Only one data Backup can occur at a time (when a full database Backup occurs, differential, or incremental Backups cannot occur at the same time).
- Only one-log Backup can happen at a time (a log Backup is allowed when a full database Backup is occurring).
- You cannot add or drop files to a database while a Backup is occurring.
- You cannot shrink files while database Backups are happening.
- There are limited recovery model changes allowed while Backups are occurring.

When any of these conflicting operations are performed, the commands will encounter the lock waits that are mentioned in the "Explanation" section followed by you receiving the 3023 and 3041 messages.

## User action

Examine the schedules of the various database maintenance activities, and then adjust the schedules so that these operations or commands do not conflict with each other.

## More information

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] records the start time and the end time of the Backup in the `msdb` database. You can examine the Backup history to determine whether there was a full database Backup occurring while an incremental Backup was attempted and therefore caused the error. You can use the following query to help you with this process:

```sql
select database_name, type, backup_start_date, backup_finish_date
from msdb.dbo.backupset
order by database_name, type, backup_start_date, backup_finish_date
go
```

You can also use the **User Error Message** event in SQL Profiler Trace or the **error_reported** event in Extended Events to track the reporting of the 3023 messages back to the application that initiated the Backup or other maintenance command.
