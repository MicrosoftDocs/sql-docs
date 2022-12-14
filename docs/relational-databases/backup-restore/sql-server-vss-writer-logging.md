---
title: "SQL Server VSS Writer logging"
description: This article describes the new text file-based logging feature of the VSS writer of SQL Server, which was initially introduced in SQL server 2019.
ms.custom: ""
ms.date: "05/04/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
author: Guillaume-Fourrat
ms.author: gfourrat
---
# SQL Server VSS Writer logging
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

As [SQL Server Back up Applications - Volume Shadow Copy Service (VSS) and SQL Writer](sql-server-vss-writer-backup-guide.md) describes in depth, [!INCLUDE[ssSQL11](../../includes/ssnoversion-md.md)] can be involved in VSS (Volume Shadow Copy Service) backup and restore operations through its dedicated SQL Writer service. The service would report execution errors to Windows Application Event Logs with a `SQLWRITER` event source, but up until [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)], it had no dedicated activity log. It could make investigations against [!INCLUDE[ssSQL11](../../includes/ssnoversion-md.md)] as a participant in VSS operations challenging.
This article describes the new log introduced by [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] to provide better visibility on its SQLWriter operations.

> [!IMPORTANT]
>Following its introduction in SQL Server 2019, the new SQL Writer logging infrastructure described here was later made available in [!INCLUDE[sssql15-md](../../includes/sssql16-md.md)] Service Pack 3; and in [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)] starting with Cumulative Update 27.

## Overview

The main characteristics of  [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] SQLWriter logging are:

- It's on by default
- It's system-wide (it will trace SQL Writer activity against all SQL Server instances running on the server)
- It's text-based
- Its working directory is `C:\Program Files\Microsoft SQL Server\90\Shared`
- Within that directory:
  - Logging takes place in file `SqlWriterLogger.txt`
  - This file gets renamed to `SqlWriterLogger1.txt` when reaching a maximum size (by default 1 MB), with logging continuing in main `SqlWriterLogger.txt`. 
  - There's only one rollover file, so the second rollover would overwrite the existing `SqlWriterLogger1.txt`.
  - Parameters are managed by file `SqlWriterConfig.ini`

SQL Writer being a [!INCLUDE[ssSQL11](../../includes/ssnoversion-md.md)] shared component, it has a single instance on a system and its major version will be the same as the highest major version of any installed [!INCLUDE[ssSQL11](../../includes/ssnoversion-md.md)] Instance. For example, if instances of, say,[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], [!INCLUDE[sssql15-md](../../includes/sssql16-md.md)] SP2 and [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] are collocated on the same system, the SQL Writer binary will be the one provided by [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] and will service all running instances from all major versions (even though its home directory remains under \90). Local instances and versions will benefit from the new [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] tracing described here. It also implies that only [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] cumulative updates will upgrade SQL Writer binaries in this situation.

> [!NOTE]
> The following paragraphs describe the situation starting with [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] Cumulative Update 4 (**CU4**). Earlier [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] builds will not have the same amount of information in log file under default settings.

## Basic Operations

You can benefit from the new logging without any manual change. You can open, or get a copy of, the main `SqlWriterLogger.txt` log file in `C:\Program Files\Microsoft SQL Server\90\Shared\`.
The file will reflect all VSS events reaching SQL Writer, which would mainly be:
- `OnIdentify` events, as triggered by command `vssadmin list writers`
- Backup events
- Restore events

That is, even if these operations take place successfully the log file will record detailed entries. The log will allow the DBA to confirm VSS operations are occurring and successfully interacting with SQL Writer. It's an improvement that offers an easy built-in way of establishing these details at [!INCLUDE[ssSQL11](../../includes/ssnoversion-md.md)] level.

Additionally, SQLWriter service start-up events will also be recorded and will report active logging parameters.

Obviously if there's a VSS operation failure that involves [!INCLUDE[ssSQL11](../../includes/ssnoversion-md.md)], the SqlWriterLogger becomes an important place to check for any information.

> [!NOTE]
> This new logging infrastructure *complements* the existing error reporting one for [!INCLUDE[ssSQL11](../../includes/ssnoversion-md.md)], it doesn't *replace* it. Therefore in case of error, the **Application Event Logs** of Windows remain the first place to check (filtering on Sources like "SQLWRITER" and "VSS"). SqlWriterLogger.txt would provide additional information to this initial set.

## Review of typical logging entries

The following exports have been made under **default** setting.

### Service Start

``` txt
[01/11/2021 02:54:59, TID 61f8] ****************************************************************
[01/11/2021 02:54:59, TID 61f8] **  SQLWRITER TRACING STARTED - ProcessId: 0x4124
[01/11/2021 02:54:59, TID 61f8] **  Service is not running as WIDWriter.
[01/11/2021 02:54:59, TID 61f8] **  SQL Writer version is 15.0.4073.23
[01/11/2021 02:54:59, TID 61f8] **  MODERN LOGGER V2 ENABLED ON C:\Program Files\Microsoft SQL Server\90\Shared\SqlWriterLogger.txt
[01/11/2021 02:54:59, TID 61f8] **  With TraceLevel = DEFAULT, TraceFileSizeMb = 1, ForceFlush = False
[01/11/2021 02:54:59, TID 61f8] **  Recording events in Server Local Time. UTC OFFSET: -8:00
[01/11/2021 02:54:59, TID 61f8] ****************************************************************
```

The above entry will be observed for each start of SQL Writer Service (it may even be logged twice per service start, it's a minor known cosmetic issue).
In order of appearance, we can see the following information is logged:

- A time stamp (date + time) in local server time, and the threadID originating the entry for each line.
- The ProcessID of SQLWriter process being started.
- The fact the service started in 'normal' mode ('not running as WIDWriter') or in Windows Internal Database mode
- The version of SQL Writer binaries.
- All parameters set by the SqlWriterConfig.INI file:
  - The target path of the active log file
  - The details level of tracing, here DEFAULT
  - The maximum size of the file before rollover happens, here 1 MB
  - The option to ForceFlush every single update to the log file vs a more relaxed/buffered approach, False by default.
- A reminder that logging is local time along with the UTC Offset of that local time.

### VSS 'OnIdentify' event

``` txt
[01/12/2021 08:23:40, TID 464c] Entering SQL Writer OnIdentify.
[01/12/2021 08:23:40, TID 464c] Service: MSSQLSERVER Server: GF19. Version=15
[01/12/2021 08:23:40, TID 464c] Instance MSSQL15.MSSQLSERVER Edition: Developer Edition
[01/12/2021 08:23:40, TID 464c] Instance MSSQL15.NAMED1 Edition: Enterprise Edition: Core-based Licensing
[01/12/2021 08:23:40, TID 464c] Skip User Instances Enumeration
```

`OnIdentify` is a common VSS operation. It's triggered by `vssadmin list writers` command. Most VSS requestors would start any VSS backup or restore operation by a `OnIdentify` event.

Previously, only active profiler tracing would allow the DBA to detect such an event. With the new logging feature, each event will lead to the above entry.

In order of appearance, we can see the following information is logged:
- An explicit mention of the `OnIdentify` VSS event.
- A list of all active (running) [!INCLUDE[ssSQL11](../../includes/ssnoversion-md.md)] instances, along with their instance name, major version, and Edition.
- The indication we didn't attempt to list "User Instances" – a specific [!INCLUDE[ssSQL11](../../includes/ssnoversion-md.md)] feature also known as [LocalDB](../../database-engine/configure-windows/sql-server-express-localdb.md) and typically not involved on enterprise database servers.

### Successful Component-mode VSS backup

``` txt
[01/11/2021 02:30:19, TID 32c8] Entering SQL Writer Initialize.
[01/11/2021 02:33:33, TID 232c] Entering SQL Writer OnIdentify.
[01/11/2021 02:33:33, TID 232c] Service: MSSQLSERVER Server: GF19. Version=15
[01/11/2021 02:33:33, TID 232c] Instance MSSQL15.MSSQLSERVER Edition: Developer Edition
[01/11/2021 02:33:33, TID 232c] Instance MSSQL15.NAMED1 Edition: Enterprise Edition: Core-based Licensing
[01/11/2021 02:33:33, TID 232c] Skip User Instances Enumeration
[01/11/2021 02:33:37, TID 232c] Entering SQL Writer OnPrepareBackup.
[01/11/2021 02:33:37, TID 232c] ****************************************************************
[01/11/2021 02:33:37, TID 232c] **  VSS SQL BACKUP BEGIN - ID: 232c
[01/11/2021 02:33:37, TID 232c] ****************************************************************
[01/11/2021 02:33:37, TID 232c] Component based backup selected.
[01/11/2021 02:33:37, TID 232c] Database count from metadata is 1
[01/11/2021 02:33:37, TID 232c] Database db_on_G on instance GF19 found in metadata
[01/11/2021 02:33:37, TID 232c] Backup type is VSS_BT_COPY
[01/11/2021 02:33:38, TID 232c] Entering SQL Writer OnPrepareSnapshot.
[01/11/2021 02:33:38, TID 232c] Service: MSSQLSERVER Server: GF19. Version=15
[01/11/2021 02:33:38, TID 232c] Instance MSSQL15.MSSQLSERVER Edition: Developer Edition
[01/11/2021 02:33:38, TID 232c] Instance MSSQL15.NAMED1 Edition: Enterprise Edition: Core-based Licensing
[01/11/2021 02:33:38, TID 232c] Skip User Instances Enumeration
[01/11/2021 02:33:38, TID 232c] Entering SQL Writer OnFreeze.
[01/11/2021 02:33:38, TID 232c] Entering SQL Writer OnThaw.
[01/11/2021 02:33:38, TID 232c] Entering SQL Writer OnPostSnapshot.
[01/11/2021 02:33:38, TID 232c] Entering SQL Writer OnIdentify.
[01/11/2021 02:33:38, TID 232c] Service: MSSQLSERVER Server: GF19. Version=15
[01/11/2021 02:33:38, TID 232c] Instance MSSQL15.MSSQLSERVER Edition: Developer Edition
[01/11/2021 02:33:38, TID 232c] Instance MSSQL15.NAMED1 Edition: Enterprise Edition: Core-based Licensing
[01/11/2021 02:33:38, TID 232c] Skip User Instances Enumeration
[01/11/2021 02:33:40, TID 232c] Entering SQL Writer OnBackupComplete.
```

This event leads to a more sizeable set of entries.
In order of appearance, we can see the following information is logged:

- A full `OnIdentify` section, which as already indicated often leads a backup.
- Mention of every main VSS phase of backup, with pattern "Entering SQL Writer xxxx".
  -  First one here being `Entering SQL Writer OnPrepareBackup`.
- A conspicuous entry indicating the start of a VSS SQL Backup 
  - (ID being the threadId that is doing the logging for that backup attempt in SQLWriter)
- The VSS backup API selected by the VSS requestor, "component" or "non-component / Volume"
- The count of Databases included in the component-list sent by VSS requestor, here a single DB (1).
- A confirmation that each Requestor-provided database name (here 'db_on_G') is found (or not found) on the [!INCLUDE[ssSQL11](../../includes/ssnoversion-md.md)] instance it's been associated with by the VSS requestor (here default instance 'GF19').
- The Flavor of VSS backup requested. Usually `VSS_BT_FULL` or `VSS_BT_COPY`. See the [VSS_BACKUP_TYPE](/windows/win32/api/vss/ne-vss-vss_backup_type) enum. 
- Another `OnIdentify` section
- More entries identifying main phases of VSS Backup (`OnFreeze`, `OnThaw`, `OnPostSnapshot`)
- A final `OnIdentify` Section.
- A final VSS phase report, which names makes it a useful "closing event": `OnBackupComplete`.

These entries provide details on the VSS operations that were previously difficult to establish quickly and required advanced tracing to do so. A prime example is the "Component" or "non-Component" mode of any VSS backup request. With [!INCLUDE[ssql19-md](../../includes/sssql19-md.md)] SQL Writer, they're logged for every single VSS request by default and are easily accessible.



### Failure Situation: torn database

To illustrate the earlier statement that SQL Writer logging complements original EventLog architecture, let's look at the entries associated to a well-known failure situation: a Torn Database. It can occur when a  VSS backup attempts to create a snapshot set of volumes, which includes only a partial set of files of a given database. SQL Writer will block it as per VSS conventions.

This extract is the content of `SqlWriterLogger.txt` for the operation:

``` txt
[01/11/2021 02:57:00, TID 5a88] Entering SQL Writer OnIdentify.
[01/11/2021 02:57:00, TID 5a88] Service: MSSQLSERVER Server: GF19. Version=15
[01/11/2021 02:57:00, TID 5a88] Instance MSSQL15.MSSQLSERVER Edition: Developer Edition
[01/11/2021 02:57:00, TID 5a88] Instance MSSQL15.NAMED1 Edition: Enterprise Edition: Core-based Licensing
[01/11/2021 02:57:00, TID 5a88] Skip User Instances Enumeration
[01/11/2021 02:57:02, TID 5a88] Entering SQL Writer OnPrepareBackup.
[01/11/2021 02:57:02, TID 5a88] ****************************************************************
[01/11/2021 02:57:02, TID 5a88] **  VSS SQL BACKUP BEGIN - ID: 5a88
[01/11/2021 02:57:02, TID 5a88] ****************************************************************
[01/11/2021 02:57:02, TID 5a88] Volume based (= NonComponent) backup selected.
[01/11/2021 02:57:02, TID 5a88] Backup type is VSS_BT_FULL
[01/11/2021 02:57:03, TID 5a88] Entering SQL Writer OnPrepareSnapshot.
[01/11/2021 02:57:03, TID 5a88] Service: MSSQLSERVER Server: GF19. Version=15
[01/11/2021 02:57:03, TID 5a88] Instance MSSQL15.MSSQLSERVER Edition: Developer Edition
[01/11/2021 02:57:03, TID 5a88] Instance MSSQL15.NAMED1 Edition: Enterprise Edition: Core-based Licensing
[01/11/2021 02:57:03, TID 5a88] Skip User Instances Enumeration
[01/11/2021 02:57:03, TID 5a88] HRESULT EXCEPTION CAUGHT: hr: 0x80780002
[01/11/2021 02:57:03, TID 5a88] Entering SQL Writer OnAbort.
```

From SqlWriterLogger.txt we see that a failure occurred, however the only details we have on failure is the `0x80780002 HResult`. This value is difficult to interpret without the error code references. It *does* identify the Torn Database situation though.

Now let's check the contents of Windows Application Event Logs:

``` txt
Log Name:      Application
Source:        SQLWRITER
Date:          1/11/2021 02:57:03 AM
Event ID:      24579
Task Category: None
Level:         Error
Keywords:      Classic
User:          N/A
Computer:      GF19
Description:
Sqllib error: Database db_on_G_and_H of SQL Server instance GF19  is stored on multiple volumes, only some of which are being shadowed.
```
The event provides a full user-friendly formatted message explaining the situation.

The OS VSS framework will also report the issue in Event Logs, using its nomenclature (VSS manages 'components,' which are 'databases' in the context of SQL Server).

``` txt
Log Name:      Application
Source:        VSS
Date:          1/11/2021 02:57:03 AM
Event ID:      8229
Task Category: None
Level:         Warning
Keywords:      Classic
User:          N/A
Computer:      GF19
Description:
A VSS writer has rejected an event with error 0x800423f0, The shadow-copy set only
 contains only a subset of the volumes needed to correctly backup the selected 
components of the writer.
Changes that the writer made to the writer components while handling the event will
 not be available to the requester. 
Check the event log for related events from the application hosting the VSS writer. 

Operation:
   PrepareForSnapshot Event

Context:
   Execution Context: Writer
   Writer Class Id: {a65faa63-5ea8-4ebc-9dbd-a0c4db26912a}
   Writer Name: SqlServerWriter
   Writer Instance Name: Microsoft SQL Server 2019:SQLWriter
   Writer Instance ID: {a16fed29-e555-4cc5-8938-c89201f31f7e}
   Command Line: "C:\Program Files\Microsoft SQL Server\90\Shared\sqlwriter.exe"
   Process ID: 22628
```

Event Log is a better source of information on the error itself here. However, the SqlWriterLogger contents provide details on the backup request (a `VSS_BT_FULL`, non-component VSS backup request that failed during the `OnPrepareSnapshot` phase of SQL Writer). Any investigation of VSS errors involving SQL Server should therefore collect and review *both* sources.

## Modifying SQL Writer logging parameters

SQL Writer logging can be configured by editing the `SqlWriterConfig.INI` text file. The file itself contains a quick inline description of the available parameters, which we'll review below.

> [!IMPORTANT]
> The .ini file is under a Windows-Protected folder (Program Files). As such it requires elevated administrator privileges to edit. A double-click in explorer will open Notepad without elevation: it will allow the user to read the contents, but attempts to save any change will fail.   
Either start Notepad as administrator and then open SqlWriterConfig.INI, or use a text editor which is able to prompt for elevation as needed at 'save file' time.

Duplicating the `SqlWriterConfig.ini` file comments here:

``` txt
; EnableLogging = TRUE | FALSE(true is the default behavior)
; TraceFile = C:\Program Files\Microsoft SQL Server\90\Shared\SqlWriterLog.txt // default location for the trace file is the SQLWriter.exe folder)
; TraceLevel = DEFAULT | MINIMAL | VERBOSE // DEFAULT is the initial value
; TraceFileSizeMb = Default size is 1 MB. Once the log is full it will be written to old_TraceFile. Any pre existing old_TraceFile will be over written.
; ForceFlush = TRUE | FALSE(FALSE is the default value, force flush is used when a crash is expected and the log will flush for each write)
```

- **EnableLogging** allows the user to disable the whole new logging feature, in the unlikely case it's needed.
- **TraceFile** would allow the user to change the path and filename of the trace file. It's not recommended to change it as the default, well-known location makes it easy to go straight to the right place on any SQL Server.
- **Tracelevel** is the verbosity of the logging. More details further on.
- **TraceFileSizeMb** is the max file size before rollover. The .txt file uses UTF-8 encoding and consumes 2 bytes per character. Increasing this value is valid for instance with intense VSS activity or where it is required to retain long periods of log entries, or if non-default `TraceLevel` values are enabled for long durations. The default 1-MB value with default `TraceLevel` should provide ample history for most situations.
- **ForceFlush** setting this option to `TRUE` would only be useful in the  rare circumstances where the SQL Writer service would crash before it got the chance to flush its last log entries; otherwise keep the default value.

### Applying Changes
 
Any change to the settings requires a restart of SQL Writer service to activate. 

> [!TIP]
>Restarting SQL Writer is extremely fast and can be done at will as SQL Writer doesn't retain any stateful information nor has any activity in-between VSS calls. The only precaution is to avoid a restart while a VSS operation (backup, restore) is taking place.

SQL Writer will report active parameters in its log file upon (re)start, as can be seen in [Service Start](#service-start) sample extract.

## TraceLevel details

The INI file lists the following level: DEFAULT | MINIMAL | VERBOSE.

- **DEFAULT**: The default verbosity parameters should be adequate for most needs: refer to the [Review of typical logging entries](#review-of-typical-logging-entries) section to observe what is already generated by default. In addition to errors, successful VSS calls, along with VSS metadata, will be logged by default.
- **MINIMAL**: This level will keep the formatting of `DEFAULT` mode, and its events. It will also generate output in many key places of the code. Most notably it will log all the files and database iterations that are commonplace in the SQLWriter logic. It can increase the number of entries logged for each VSS operation (including mundane `OnIdentify` event) by a large margin, especially on instances hosting a large number of databases: every single physical file of every single Database may be reported more than once over the course of a VSS backup. This level only helps giving a more precise idea of the logical position of SQL Writer logic at the time of a failure. It's also convenient for exploration purposes. It's not useful to keep it active beyond momentary investigations, as the level of details will greatly reduce the history depth of the default 1-MB file size. Increasing the `TraceFileSizeMb` value may be relevant.
- **VERBOSE**: Currently this level reports the same events as `MINIMAL`, but prefixes each entry with source code object and methods descriptors. It makes the output wider (larger in size than Minimal) and less readable. The added information wouldn't be useful outside of interactions with Microsoft Support Services. The same comment as `MINIMAL` would apply: keep this level active for long duration will greatly reduce the history depth of the default 1-MB file size, and increasing the `TraceFileSizeMb` value may be relevant.

> [!NOTE]
> **MINIMAL** (and **VERBOSE**) level will *not* provide additional error details in case of failure, only additional *progress details* for each low level operation related to SQL Writer activities.

    
## See Also  
 [SQL Server Back up Applications - Volume Shadow Copy Service (VSS) and SQL Writer](sql-server-vss-writer-backup-guide.md)   
 [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)   
 [Back Up and Restore of SQL Server Databases](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md)   
 [SQL Server Transaction Log Architecture and Management Guide](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md)
