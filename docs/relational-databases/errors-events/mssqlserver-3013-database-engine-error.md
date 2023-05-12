---
title: MSSQLSERVER_3013
description: "MSSQLSERVER_3013"
author: pijocoder
ms.author: jopilov
ms.reviewer: sureshka, mathoma
ms.date: 03/23/2023
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "3013 (Database Engine error)"
---

# MSSQLSERVER_3013

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| --- | --- |
| Product Name | SQL Server |
| Event ID | 3013 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | DMP_ABORT |
| Message Text | BACKUP DATABASE is terminating abnormally /RESTORE DATABASE is terminating abnormally. |

## Explanation

This error is a generic error that occurs when a backup or restore operation is interrupted unexpectedly. You see 3013 raised together with other error messages that provide more specific insight into the cause of the backup failure. Examples would include read or writes failure from/to the backup media or other unexpected Win32 API call failures. 


## Cause

There could be many various causes for an abnormal termination of a backup or a restore in SQL Server. Here's a list of common reasons:

- Insufficient disk space
- Incorrect path to the backup storage device
- The backup file/device is already open by another program
- Backup media device failure or malfunction
- Database corruption - if the database is corrupt, the backup or restore operation may fail.
- Lack of BACKUP DATABASE, BACKUP LOG or CREATE DATABASE permissions to be able to back up or restore respectively
- SQL Server service account lack of access to the backup device

## User action


Examine the SQL Error log for other messages that occur alongside this error for additional information and troubleshooting.  


- For insufficient disk space, ensure sure that the drive where you're writing the backup has enough free space available or use a different device. See [Examples with errors 3203 and 3203](#examples-with-errors-3203-and-3203)

- For incorrect file path, double-check and correct the path and file name specified in the BACKUP or RESTORE command. 

- For backup media failure, if you're backing up to a tape drive or other backup device, make sure that the device is functioning properly and isn't experiencing any hardware errors. See [Examples with errors 3203 and 3203](#examples-with-errors-3203-and-3203) and [Example with error 3241](#example-with-error-3241)

- For database corruption issues, you're likely to observe other errors in SQL Server. Run DBCC CHECKDB to identify any errors in the database and resolve. For more information, see [Troubleshoot database consistency errors reported by DBCC CHECKDB](/troubleshoot/sql/database-engine/database-file-operations/troubleshoot-dbcc-checkdb-errors)

- If your server principal account lacks permissions to do a backup or a restore operation, ensure account that is granted those permissions. See [Backup permissions](../../t-sql/statements/backup-transact-sql.md#permissions) and [Restore permissions](../../t-sql/statements/restore-statements-transact-sql.md#permissions)

- For SQL Server service account permission issues, ensure that the SQL Server service account has read and write access to the backup device or the file system where the backup file is written. See [Backup permissions](../../t-sql/statements/backup-transact-sql.md#permissions). 


Here are examples of commonly observed errors together with 3013.

### Example with error 3241

In this scenario, error 3241 is raised with 3013 and indicates issues with the backup itself.

  ```output
  Msg 3241, Level 16, State 0, Line 2
  The media family on device 'G:\backup\ProdDB_backup.bak' is incorrectly formed. SQL Server cannot process this media family.
  Msg 3013, Level 16, State 1, Line 2
  RESTORE FILELIST is terminating abnormally.
  ```

**Resolution:**

This error typically indicates damaged backup(s) or the media that stores or transferred the backups malfunctioned. Find an alternative backup to restore either from different medium or try earlier or later backup.
Also, see KB5014298 for backup/restores of TDE databases - [FIX: Error 3241 occurs during executing RESTORE DATABASE OR RESTORE LOG](https://support.microsoft.com/topic/kb5014298-fix-error-3241-occurs-during-executing-restore-database-or-restore-log-8b6649d4-5de0-4105-96ac-85d4eaa4d00a)

For more troubleshooting ideas, see [Media-related errors when you restore a database from a backup](/troubleshoot/sql/database-engine/backup-restore/backup-restore-operations#media-related-errors-when-you-restore-a-database-from-a-backup)

### Examples with errors 3203 and 3203

Errors 3202 and 3203 are backup errors raised when there are I/O-related issues. These two errors indicate whether a read or a write request was performed and show the underlying OS error that resulted from the I/O failure. These examples have been observed:

  ```output
  Msg 3203, Level 16, State 1, Line 1
  Read on "G:\SQLDATA\ProductionDb.ndf" failed: 483(The request failed due to a fatal device hardware error.)
  Msg 3013, Level 16, State 1, Line 1
  BACKUP DATABASE is terminating abnormally.
  ```

  ```output
  Msg 3202, Level 16, State 1, Line 2
  Write on "Y:\SQLDATA\ProductionDb.bak" failed: 1117(The request could not be performed because of an I/O device error.)
  Msg 3013, Level 16, State 1, Line 2
  RESTORE DATABASE is terminating abnormally.
  ```

  ```output
  Msg 3202, Level 16, State 1, Line 14
  Write on "\\BackupServer\Share\ProdDb.bak" failed: 112(There is not enough space on the disk.)
  Msg 3013, Level 16, State 1, Line 14
  BACKUP DATABASE is terminating abnormally.
  ```

**Resolution:**

- The examples with OS 483 and 1117 indicate I/O device failure. Check for malfunction or damage of the storage media. Review System event logs, hardware configuration and logs and work with hardware administrator and vendor to address any issues with the media that stores the backups. Here's an example of a message you may find in the System Event log, which indicates of I/O issues that need to be addressed:

  ```
  Warning PM,Disk,153,None,The IO operation at logical block address 0xe90525a0 for Disk 3 (PDO name: \Device\00000017) was retried.
  ```

- If OS error 112 is raised indicating space issues, ensure sufficient disk space on the local or remote storage where the backup is sent. If sufficient space is available, ensure reliability of storage media.

### Example with 3624

In some cases error 3013 may be raised together with a system assertion. If a backup fails with an assertion, then the main focus is to address the assertion itself. Here's an example of an issue observed:

```output
Msg 3013, Sev 16, State 1, Line 1
VERIFY DATABASE is terminating abnormally.
Msg 3624, Sev 20, State 1, Line 1
A system assertion check has failed. Check the SQL Server error log for details. Typically, an assertion failure is caused by a software bug or data corruption. To check for database corruption, consider running DBCC CHECKDB. If you agreed to send dumps to Microsoft during setup, a mini dump will be sent to Microsoft. An update might be available from Microsoft in the latest Service Pack or in a Hotfix from Technical Support.
Error: 17066, Severity: 16, State: 1.
SQL Server Assertion: File:     mediaRead.cpp:429 Expression:     !m_ActiveConsumptionList.IsEmpty () || !m_ActiveReads.IsEmpty () || !m_DecodeOutputQ.IsEmpty () || (CFeatureSwitchesMin::GetCurrentInstance ()->FEnableCheckingActiveDecodeQueueEnabled () && !m_ActiveDecodeInput.IsEmpty ()) SPID:         74 Process ID:     25440
```

**Resolution:**

Review the SQL Server error log and use the methodology outlined in this article [MSSQLSERVER_3624](mssqlserver-3624-database-engine-error.md) to troubleshoot the assert failures:

- Run DBCC CHECKDB on your databases and ensure all components on the I/O path are functioning properly.
- Lookup part or all of the assert expression online for any known issues. For example in this case if you look up "m_ActiveConsumptionList.IsEmpty" you may find this article:

  [KB4469554 - FIX: Assertion error occurs during restore of compressed backups in SQL Server 2014, 2016 and 2017](https://prod.support.services.microsoft.com/topic/kb4469554-fix-assertion-error-occurs-during-restore-of-compressed-backups-in-sql-server-2014-2016-and-2017-37cb5d08-d697-c3e4-a598-cb797425615c)

- Update your SQL Server to a later build (Cumulative update)
- Ensure no external component is interfering and causing the failure

### Example with error 4303

This example illustrates a restore of a transaction log sequence that failed and raised error 3013. The specific error 4303 indicates that either more transaction log restores are missing prior to this one or that the transaction log backup file is damaged. For example, the LSN = 4294967295429496729565535 doesn't appear to be a valid LSN and that may be a result of a corrupt backup file or media.

```output
Msg 4303, Level 16, State 1, Line 3
The roll forward start point is now at log sequence number (LSN) 8177105000003941300003. Additional roll forward past LSN 4294967295429496729565535 is required to complete the restore sequence.
Msg 3013, Level 16, State 1, Line 3
RESTORE DATABASE is terminating abnormally.
```

**Resolution:**

If you encounter errors such as 4303 together with 3013, find an alternative good backup to restore. Also check the stability of the storage media where backups are placed and repair as necessary.


## See also

- [Troubleshoot SQL Server backup and restore operations](/troubleshoot/sql/database-engine/backup-restore/backup-restore-operations).
