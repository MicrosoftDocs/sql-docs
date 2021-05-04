---
title: "SQL Server Back up Applications - SQL Writer log"
description: Describes the log feature of SQL Writer. 
ms.custom: ""
ms.date: "05/04/2021"
ms.prod: sql
ms.prod_service: backup-restore
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
author: Guillaume-Fourrat
ms.author: gfourrat
---
# SQL Server Back up Applications - SQL Writer logging
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Overview

[!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] introduces a new Logging feature for its VSS Writer “SQLWriter” service. Its main characteristics are :

- It is on by default
- It is system-wide (it will trace SQL Writer activity against all SQL Server instances running on the server)
- It is text-based
- Its working directory is **“C:\Program Files\Microsoft SQL Server\90\Shared\”** (fixed)
- Within that directory :
  - Logging takes place in file **"SqlWriterLogger.txt"**
  - This file gets renamed to “SqlWriterLogger1.txt” when reaching a maximum size (by default 1MB), with logging continuing in main SqlWriterLogger.txt. 
  - There is only one rollover file, so the second rollover would overwrite the existing SqlWriterLogger1.txt.
  - Parameters are managed by file **"SqlWriterConfig.ini"**

SQL Writer being a [!INCLUDE[ssSQL11](../../includes/ssnoversion-md.md)] shared component, it is ‘unique’ on a system and its major version will be the same as the highest major version of any installed [!INCLUDE[ssSQL11](../../includes/ssnoversion-md.md)] Instance. Therefore, if instances of, say,[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], [!INCLUDE[sssql15-md](../../includes/sssql16-md.md)] SP2 and [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] are collocated on the same system, the SQL Writer binary will be the one provided by [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] and will service all running instances from all major versions (even though its home directory remains under \90). This means that all local instances, whichever their versions, benefit from the new [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] tracing described here. It also implies that only [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] cumulative updates will upgrade SQL Writer binaries in this situation.

> [!NOTE]
> The following paragraphs describe the situation starting with [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] Cumulative Update 4 (**CU4**). Earlier [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)] builds will not have the same amount of information in log file under default settings.

## Basic Operations

You should be able to benefit from the new logging without any manual change. You can simply directly open, or get a copy of, the main SqlWriterLogger.txt log file in “C:\Program Files\Microsoft SQL Server\90\Shared\”.
The file will reflect all VSS events reaching SQL Writer, which would mainly be :
- “OnIdentify” events, as triggered by command “vssadmin list writers”
- Backup events
- Restore events

That is, even if these operations take place successfully the log file will record detailed entries. It will allow to confirm VSS operations are indeed happening and successfully involving SQL Writer, while also collecting additional details on the requests. It is an important improvement as previously there was no easy built-in way of establishing these details at [!INCLUDE[ssSQL11](../../includes/ssnoversion-md.md)] level.
Additionally, SQLWriter service starts will also be recorded and will report active logging parameters.
Obviously in case of a VSS operation failure that involves [!INCLUDE[ssSQL11](../../includes/ssnoversion-md.md)], the SqlWriterLogger becomes an important place to check for any information.

> [!NOTE]
> This new logging infrastructure *complements* the existing error reporting one for [!INCLUDE[ssSQL11](../../includes/ssnoversion-md.md)], it doesn’t *replace* it. Therefore in case of error, the **Application Event Logs** of Windows remain the first place to check (filtering on Sources like ”SQLWRITER” and “VSS”). SqlWriterLogger.txt would provide additional information to this initial set.

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

The above entry will be observed for each start of SQL Writer Service (it may even be logged twice per service start, it is a minor known cosmetic issue).
In order of appearance, we can see the following information is logged :
- A time stamp (date + time) in local server time, as well as the threadID originating the entry for each line.
- The ProcessID of SQLWriter process being started.
- The fact the service started in ‘normal’ mode (‘not running as WIDWriter’) or in Windows Internal Database mode
- The version of SQL Writer binaries.
- All parameters set by the SqlWriterConfig.INI file :
  - The target path of the active log file
  - The details level of tracing, here DEFAULT
  - The maximum size of the file before rollover happens, here 1 MB
  - The option to ForceFlush every single update to the log file vs a more relaxed/buffered approach, False by default.
- A reminder that logging is local time along with the UTC Offset of that local time.

### VSS ‘OnIdentify’ event

``` txt
[01/12/2021 08:23:40, TID 464c] Entering SQL Writer OnIdentify.
[01/12/2021 08:23:40, TID 464c] Service: MSSQLSERVER Server: GF19. Version=15
[01/12/2021 08:23:40, TID 464c] Instance MSSQL15.MSSQLSERVER Edition: Developer Edition
[01/12/2021 08:23:40, TID 464c] Instance MSSQL15.NAMED1 Edition: Enterprise Edition: Core-based Licensing
[01/12/2021 08:23:40, TID 464c] Skip User Instances Enumeration
```

OnIdentify is a very common VSS operation. It is triggered by “vssadmin list writers” command, and most VSS requestors would start any VSS backup or restore operation by a “OnIdentify” event.

Previously, only active profiler tracing would allow to detect such an event. With the new logging feature, each event will lead to the above entry.

In order of appearance, we can see the following information is logged :
- An explicit mention of the OnIdentify VSS event.
- A list of all active (running) [!INCLUDE[ssSQL11](../../includes/ssnoversion-md.md)] instances, along with their instance name, major version, and Edition.
- The indication we didn’t attempt to list “User Instances” – a specific [!INCLUDE[ssSQL11](../../includes/ssnoversion-md.md)] feature / pseudo-instance usually not a good candidate for VSS backups.

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
In order of appearance, we can see the following information is logged :

- A full “OnIdentify” section, which as already indicated often leads a backup.
- Mention of every main VSS phase of backup, with pattern “Entering SQL Writer xxxx”.
  -  First one here being ‘Entering SQL Writer OnPrepareBackup.
- A very conspicuous entry indicating the start of a VSS SQL Backup 
  - (ID being the threadId that is doing the logging for that backup attempt in SQLWriter)
- The VSS backup API selected by the VSS requestor, “component” or “non-component / Volume”
- The count of Databases included in the component-list sent by VSS requestor, here a single DB (1).
- A confirmation that each Requestor-provided database name (here ‘db_on_G’) is found (or not found) on the [!INCLUDE[ssSQL11](../../includes/ssnoversion-md.md)] instance it’s been associated with by the VSS requestor (here default instance ‘GF19’).
- The Flavor of VSS backup requested. Usually VSS_BT_FULL or VSS_BT_COPY. See the VSS_BACKUP_TYPE enum. 
- Another OnIdentify section
- A block reporting main phases of VSS Backup (Freeze, Thaw, .)
- A final OnIdentify Section.
- A final VSS phase report, which names makes it a useful “closing event”: OnBackupComplete.

These entries provide details on the VSS operations that were previously very difficult to establish on the fly and required advanced tracing to do so, most notably the "Component" vs "non-Component" mode of any backup request. With [!INCLUDE[ssql19-md](../../includes/sssql19-md.md)] SQL Writer, they are logged for every single VSS request by default and are easily accessible.



### Failure Situation: torn database

In order to illustrate the earlier statement that SQL Writer logging complements original EventLog architecture, let’s look at the entries associated to a well-known failure situation: a Torn Database. This can occur when a volume-based VSS backup attempts to snapshot a drive where only a subset of Database Files is present (as opposed to making sure all drives supporting the Databases Files are included to the snapshot set). SQL Writer will block it as per VSS conventions.

This extract is the content of **SqlWriterLogger.txt** for the operation :

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

From SqlWriterLogger.txt we see that a failure occurred, however the only details we have on failure is 0x80780002 HResult. This value is difficult to interpret without the error code references (it *does* identify the Torn Database situation though).

Now let’s check the content of Windows Application Event Logs :

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

The OS VSS framework will also report the issue in Event Logs, using its own nomenclature (VSS manages ‘components’, which in context of SQL Server are ‘databases’).

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
A VSS writer has rejected an event with error 0x800423f0, The shadow-copy set only contains only a subset of the
volumes needed to correctly backup the selected components
of the writer.
. Changes that the writer made to the writer components while handling the event will not be available to the requester. Check the event log for related events from the application hosting the VSS writer. 

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

Event Log is therefore the better source of information on the error itself here. However, the SqlWriterLogger content provide details on the backup request (a VSS_BT_FULL, non-component VSS backup request that failed during the OnPrepareSnapshot phase of SQL Writer). Any investigation of VSS errors involving SQL Server should therefore collect and review *both* sources.

## Modifying SQL Writer logging parameters

SQL Writer logging can be configured by editing the SqlWriterConfig.INI text file. The file itself contains a quick inline description of the available parameters which we’ll review below.

> [!IMPORTANT]
> The .ini file is under a Windows-Protected folder (Program Files). As such it requires elevated administrator privileges to edit. A double-click in explorer will open Notepad without elevation: it will allow to read the content, but attempts to save any change will fail.   
Either start Notepad as administrator and then open SqlWriterConfig.INI, or use a text editor which is able to prompt for elevation as needed at ‘save file’ time.

Duplicating the SqlWriterConfig.ini file comments here :

``` txt
; EnableLogging = TRUE | FALSE(true is the default behavior)
; TraceFile = C:\Program Files\Microsoft SQL Server\90\Shared\SqlWriterLog.txt // default location for the trace file is the SQLWriter.exe folder)
; TraceLevel = DEFAULT | MINIMAL | VERBOSE // DEFAULT is the initial value
; TraceFileSizeMb = Default size is 1MB. Once the log is full it will be written to old_TraceFile. Any pre existing old_TraceFile will be over written.
; ForceFlush = TRUE | FALSE(FALSE is the default value, force flush is used when a crash is expected and the log will flush for each write)
```

- **EnableLogging** allows to disable the whole new logging feature, in the unlikely case it is needed.
- **TraceFile** would allow to change the path and filename of the trace file. It is not recommended to change it as the fixed location makes it easy to go straight to the right place on any SQL Server for DBAs and Logs Collection tools alike.
- **Tracelevel** is the verbosity of the logging. More details further on.
- **TraceFileSizeMb** is the max file size before rollover. Note that the .txt file uses UTF-8 encoding and would therefore consume 2 bytes per character. Increasing this value is valid for instance with intense VSS activity or where it is required to retain very long periods of log entries, or if non-default TraceLevels are enabled for long durations. The default 1MB value with default TraceLevel should provide ample history for most situations.
- **ForceFlush** setting this option to TRUE would only be useful in the very rare circumstances where the SQL Writer service would crash before it got the chance to flush its last log entries. Otherwise retain the default value.

### Applying Changes
 
Any change to the settings requires a restart of SQL Writer service to activate. 

> [!TIP]
>Restarting SQL Writer is extremely fast and can be done at will as SQL Writer doesn’t have any activity and doesn’t retain any stateful information in-between VSS calls. The only precaution is to avoid a restart while a VSS operation (backup, restore) is taking place.

SQL Writer will report active parameters in its log file upon (re)start, as can be seen in ‘Service Start’ sample extract.

## TraceLevel details

The INI file lists the following level: DEFAULT | MINIMAL | VERBOSE.

- **Default**: The default verbosity parameters should be adequate for most needs: refer to the ‘Review of typical logging entries’ section to observe what is already generated by default. On top of any error, successful VSS calls along with key VSS metadata will be logged by default.
- **Minimal**: This level will retain the formatting of Default mode, as well as all its events. In addition, it will also generate output in many key places of the code. Most notably it will log all the files and database iterations that are commonplace in the SQLWriter logic. It can increase the number of entries logged for each VSS operation (including mundane OnIdentify event) by a very large margin, especially on instances hosting a large number of databases, as every single file of every single Database may be reported more than once over the course of a VSS backup. This level only helps to give a more precise idea of the logical position of SQL Writer logic at the time of a failure or for exploration purposes. As such it’s usually not useful to keep it active beyond momentary investigations. Note that keeping it active for long duration will greatly reduce the history depth of the default 1MB file size as each operation will generate at least five times as many entries: increasing the TraceFileSizeMb value may be relevant.
- **Verbose**: Currently this level reports the very same events as Minimal, but prefixes each entry with source code object and methods descriptors. It makes the output wider (and therefore even larger in size than Minimal) and less readable. The added information would not be very useful outside of interactions with Microsoft Support Services. The same comment as minimal would apply: keeping this level active for long duration will greatly reduce the history depth of the default 1 MB file size as each operation will generate at least five times as many entries, and wider entries: increasing the TraceFileSizeMb value may be relevant.

> [!NOTE]
> **Minimal** (and **Verbose**) level will *not* provide additional error details in case of failure, only additional *progress details* for each low level operation related to SQL Writer activities.

    
## See Also  
 [SQL Server Back up Applications - Volume Shadow Copy Service (VSS) and SQL Writer](sql-server-vss-writer-backup-guide.md)   
 [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)   
 [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)   
 [Back Up and Restore of SQL Server Databases](../../relational-databases/backup-restore/back-up-and-restore-of-sql-server-databases.md)   
 [SQL Server Transaction Log Architecture and Management Guide](../../relational-databases/sql-server-transaction-log-architecture-and-management-guide.md)
  
