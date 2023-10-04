---
title: "MSSQLSERVER_9001"
description: "MSSQLSERVER_9001"
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 02/17/2023
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "9001 (Database Engine error)"
---
# MSSQLSERVER_9001

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| :--- | :--- |
| Product Name | SQL Server |
| Event ID | 9001 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | LOG_NOT_AVAIL |
| Message Text | The log for database '%.*ls' is not available. Check the event log for related error messages. Resolve any errors and restart the database. |

## Explanation

Error 9001 occurs when the database log file becomes unavailable. When the database log goes offline that means a serious failure has occurred that prevents transactions to occur in the database. Such a failure requires the database to restart or for you to restore a backup. The error shows the end result, but doesn't explain what led to this state. Some other issue has caused the log to not be available and you must investigate the underlying issue. Here's an example of how the error appears in the SQL error log

```output
Error: 9001, Severity: 21, State: 5.
The log for database 'ContosoDb' is not available. Check the operating system error log for related error messages. Resolve any errors and restart the database.
```

Commonly error 9001 occurs together with other errors that provide more specific explanation about the root cause. Examples include errors [9002](mssqlserver-9002-database-engine-error.md), [3313](mssqlserver-3313-database-engine-error.md), [3314](mssqlserver-3314-database-engine-error.md), [17204](mssqlserver-17204-database-engine-error.md) (shows OS error when accessing a file), [17053](mssqlserver-17053-database-engine-error.md) (shows OS error), [823](mssqlserver-823-database-engine-error.md).

In certain situations SQL Server attempts to restart the database at runtime and perform recovery, or it may restart itself (the entire service). If an automatic database restart is unsuccessful or didn't occur, you may attempt to restart SQL Server and see if a recovery of the database successfully brings the database online. If not, you must address the underlying cause for the transaction log being unavailable. This is an example of error message 3422 that shows a database restart:

```output
Database ContosoDb was shutdown due to error 9001 in routine 'XdesRMFull::CommitInternal'. Restart for non-snapshot databases will be attempted after all connections to the database are aborted.
```

The following message indicates a SQL Server restart is about to occur:

```output
Error: 3449, Severity: 21, State: 1.
SQL Server must shut down in order to recover a database (database ID 2). The database is either a user database that could not be shut down or a system database. Restart SQL Server. If the database fails to recover after another startup, repair or restore the database.
```

## Cause

The transaction log of the database can become unavailable for many reasons. Some examples include

- The transaction log file resides on a storage device that failed or isn't available
- A physically damaged transaction log file that leads to inability to write to or read from the log file
- Inability to access the file due to a failed encryption/decryption via Transparent Data Encryption (TDE)
  - Key vault service isn't accessible
  - The EKM provider module runs into an exception, error or other issue that prevents successful operation
- A full transaction log due to large transactions, low disk space, or file size limits imposed on the transaction log. Error 9002 may be found in the SQL Server error log prior to 9001. For more information, see [MSSQLSERVER_9002](mssqlserver-9002-database-engine-error.md)

## User action

Resolve the errors that precede 9001 first. Then attempt to restart the SQL Server instance to recover the database, if that hasn't occurred already.

### Resolve full transaction log errors

You may observe error 9002 prior to error 9001. Here's an example:

```output
Error: 9002, Severity: 17, State: 9.
The transaction log for database 'ContosoDb' is full due to 'AVAILABILITY_REPLICA'.
Error: 3314, Severity: 21, State: 3.
During undoing of a logged operation in database 'ContosoDb' (page (1:32573799) if any), an error occurred at log record ID (7672713:36228:159). Typically, the specific failure is logged previously as an error in the operating system error log. Restore the database or file from a backup, or repair the database.
State information for database 'ContosoDb' - Hardened Lsn: '(7672713:38265:1)'    Commit LSN: '(7672712:1683087:46)'    Commit Time: 'Jul  1 2021  5:51AM'
Database ContosoDb was shutdown due to error 3314 in routine 'XdesRMReadWrite::RollbackToLsn'. Restart for non-snapshot databases will be attempted after all connections to the database are aborted.
Always On Availability Groups connection with secondary database terminated for primary database 'ContosoDb' on the availability replica 'PRDAT1ANLYSQL05' with Replica ID: {38a71ff9-f0ee-4737-9255-bb6a73e1c5d5}. This is an informational message only. No user action is required.
Error during rollback. shutting down database (location: 1).

Error: 9001, Severity: 21, State: 5.
The log for database 'ContosoDb' is not available. Check the operating system error log for related error messages. Resolve any errors and restart the database.

Recovery of database 'ContosoDb' (6) is 0% complete (approximately 60466 seconds remain). Phase 2 of 3. This is an informational message only. No user action is required.
```

In such cases, focus on resolving the root cause - a full transaction log

To resolve full transaction log, see [Troubleshoot a Full Transaction Log (SQL Server Error 9002)](../logs/troubleshoot-a-full-transaction-log-sql-server-error-9002.md)

- Ensure that you free up transaction log space, and find out why it wasn't freed
- Free up disk space where the transaction log resides
- Expand existing log file or add a new one if necessary in some cases

### Resolve hardware and OS issues and restore from a backup if needed

Commonly error 9001 occurs when a transaction log file is damaged or due to storage device issues that make the log file unavailable. Here are two examples of errors that you may observe:

An example where the storage volume became unavailable and OS returned error "The device is not ready". You can see other errors that resulted from the disks being damaged on unavailable. These examples provide context so you can understand that error 9001 is just one of the many symptoms of a larger issue.

```output
Error: 823, Severity: 24, State: 2.
The operating system returned error 21(The device is not ready.) to SQL Server during a read at offset 0x000009afde6000 in file 'G:\Data\Files\ContosoDb_4.ldf'. Additional messages in the SQL Server error log and system event log may provide more detail. This is a severe system-level error condition that threatens database integrity and must be corrected immediately. Complete a full database consistency check (DBCC CHECKDB). This error can be caused by many factors; for more information, see SQL Server Books Online.

Error: 9001, Severity: 21, State: 3.
The log for database 'ContosoDb' is not available. Check the event log for related error messages. Resolve any errors and restart the database.

Database ContosoDb was shutdown due to error 9001 in routine 'XdesRMFull::CommitInternal'. Restart for non-snapshot databases will be attempted after all connections to the database are aborted.

Starting up database 'ContosoDb'.
Error: 17204, Severity: 16, State: 1.
FCB::Open failed: Could not open file G:\Data\Files\ContosoDb.mdf for file number 1.  OS error: 3(The system cannot find the path specified.).
Error: 5120, Severity: 16, State: 101.
Unable to open the physical file "G:\Data\Files\ContosoDb.mdf". Operating system error 3: "3(The system cannot find the path specified.)".
Error: 17207, Severity: 16, State: 1.
FileMgr::StartPrimaryDataFiles: Operating system error 2(The system cannot find the file specified.) occurred while creating or opening file 'G:\Data\Files\ContosoDb_0.ndf'. Diagnose and correct the operating system error, and retry the operation.
```

Here's another example where the OS reports device errors leading to transaction log for multiple databases to be inaccessible:

```output
Error: 17053, Severity: 16, State: 1.
SQLServerLogMgr::LogWriter: Operating system error 1117(The request could not be performed because of an I/O device error.) encountered.


Error: 9001, Severity: 21, State: 4.
The log for database 'ContosoDb' is not available. Check the event log for related error messages. Resolve any errors and restart the database.
Always On Availability Groups data movement for database 'ContosoDb' has been suspended for the following reason: "system" (Source ID 2; Source string: 'SUSPEND_FROM_REDO'). To resume data movement on the database, you will need to resume the database manually. For information about how to resume an availability database, see SQL Server Books Online.


Error: 9001, Severity: 21, State: 16.
The log for database 'tempdb' is not available. Check the event log for related error messages. Resolve any errors and restart the database.
Error: 3449, Severity: 21, State: 1.
SQL Server must shut down in order to recover a database (database ID 2). The database is either a user database that could not be shut down or a system database. Restart SQL Server. If the database fails to recover after another startup, repair or restore the database.
```

At the same time, the Windows System event log reports storage device errors:

```output
Warning       NODEDB1 129     pvscsi     Reset to device, \Device\RaidPort2, was issued.
Warning       NODEDB1 153     Disk       The IO operation at logical block address 0xxxxxxxx for Disk 4 (PDO name: \Device\0000007f) was retried.
```

To address such issues:

- Ensure that the storage volumes where the database and log files reside are online, that the entire I/O path from machine to storage is stable and reliable, and that it doesn't lead to physical file damage.
- Work with your hardware and device manufacturer to ensure that hardware and its configuration is suitable to the I/O requirements of a database system. Ensure that device drivers, firmware, BIOS and other supporting software components in the I/O path are up to date.
- Run DBCC CHECKDB to check the consistency of the database, if it can be brought online after a restart
- If the database and log files aren't intact and as a result the database can't come online, restore the last known good backup of the database
- For troubleshooting suggestions, see [MSSQLSERVER error 823](mssqlserver-823-database-engine-error.md) and [Troubleshoot database consistency errors reported by DBCC CHECKDB](/troubleshoot/sql/database-engine/database-file-operations/troubleshoot-dbcc-checkdb-errors)

### Resolve TDE encryption or description failure

If you're using an external Extensible Key Management (EKM)/Hardware Security Modules (HSM) service or provider, ensure the modules provided by the service are stable and updated. Work with the EKM/HSM provider vendor to resolve any issues with the modules that perform the encryption/decryption of files.

You may observe the following symptoms in the SQL error log when this issue occurs:

```output
**Dump thread - spid = 0, EC = 0x0000023FDA293320
***Stack Dump being sent to F:\Data\MSSQL13.INST1\MSSQL\LOG\SQLDump0007.txt
* *******************************************************************************
*
* BEGIN STACK DUMP:
*   11/22/22 12:04:58 spid 1
*
* Crypto Exception
*

00007FFBA0C81791 Module(ntdll+0000000000051791)
Stack Signature for the dump is 0x00000000D3AC1708
External dump process return code 0x20000001.  External dump process returned no errors.

Error: 15466, Severity: 16, State: 28.
An error occurred during decryption.
Error: 9001, Severity: 21, State: 16.
The log for database 'ContosoDb' is not available. Check the event log for related error messages. Resolve any errors and restart the database.
```
