---
description: "MSSQLSERVER_9004"
title: "MSSQLSERVER_9004 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "9004 (Database Engine error)"
ms.assetid: b528bb49-340c-4a81-9c8d-cefce6562f16
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_9004
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|9004|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|LOG_CORRUPT|  
|Message Text|An error occurred while processing the log for database '%.*ls'.  If possible, restore from backup. If a backup is not available, it might be necessary to rebuild the log.|  
  
## Explanation  
An error was encountered while processing the log during rollback, recovery, or replication. This could indicate an error detected by the operating system or an internal consistency error detected by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] performs logical checks on the consistency of the transaction log contents as it reads and processes it. Not all aspects of the log header, log blocks, and log records are checked. The State number provides more information on the type of failure:

 - **State 1** The log file header of the Virtual Log File (VLF) was damaged.  If a damaged log file header is found as part of starting up the database on service startup, you may only see Error 9004 in the ERRORLOG. The Log File Header is the first portion of each VLF inside of a transaction log. The log file header is a not the same as the single file header, or the first 8 KB, of the log file. If the file header of the log file is damaged, you may get Msg 5172, similar to a database-file header-page corruption.
 - **State 2 and 3**  A log block was invalid when performing recovery during a RESTORE operation.
 - **State 4 through 12**  These are all various checks on log blocks when processing log records. These including parity, sector, and other logical checks on the consistency of the transaction log

In most cases, this error is only seen in the ERRORLOG or Windows Application Event Log with EventID = 9004 because the operation processing the log is not based on a direct user command (such as recovery running when the SQL Server Engine starts). In these situations, error 9004 is often seen together with Error 3414. However, some queries such as ALTER DATABASE could require a processing of the log and therefore will see these errors. Since the error is at Severity=21, the user session is disconnected.

## Cause
Error 9004 is a general error indicating the contents of the transaction log are damaged. The reason for the log to become inconsistent are similar to any database corruption problem detected by the SQL Server Engine. To find the cause for the log damage, you should follow similar techniques used for database corruption including an analysis of possible hardware, filesystem, and I/O problems. Note that DBCC CHECKDB does not check the transaction log as part of its operations and cannot detect log corruption errors. Error 9004 is raised by SQL Server Engine itself.

## User Action  
One of the following actions will correct this error:  
  
-   **Restore from a backup**:  Restore from a known good backup to recover from this problem. It is possible that, if the log portion of a database or log backup contains damaged contents, you encounter an Error 9004 on RESTORE. In this situation, the transaction log in the backup is damaged.
  
-   **Rebuild the log**:  If you cannot restore from a backup, you may be able to bring the database online by rebuilding the transaction log. You should carefully understand the ramifications of rebuilding the transaction log. This includes *possible loss of transactional consistency in your database*. For more information on how to rebuild the transaction log, please see [Resolving Errors in Database Emergency Mode](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md#resolving-errors-in-database-emergency-mode).
  
-   **Examine Logs for system issues**: Also, check the System Event log and Errorlogs to identify issues within the system that may have caused the problem.  
  
