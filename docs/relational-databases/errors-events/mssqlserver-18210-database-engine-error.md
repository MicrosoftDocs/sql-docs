---
title: "MSSQLSERVER_18210"
description: "MSSQLSERVER_18210"
author: suresh-kandoth
ms.author: sureshka
ms.date: "02/06/2023"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "18210 (Database Engine error)"
---
# MSSQLSERVER_18210
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|18210|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|STRMIO_IOFAILED|  
|Message Text|%s: %s failure on backup device '%s'. Operating system error %s.|  
  

## Explanation  

When a [Virtual device interface (VDI) backup](../backup-restore/vdi-reference/reference-virtual-device-interface.md) is terminated in SQL Server, you'll see SQL Server error 18210 in the SQL Server Error Log. VDI may be invoked from a third party application or from [SQLWriter](../../database-engine/configure-windows/sql-writer-service.md). An example:

 ```Output
 2022-05-29 15:55:42.89 Backup      Error: 18210, Severity: 16, State: 1.
 2022-05-29 15:55:42.89 Backup      BackupIoRequest::ReportIoError: write failure on backup device '{AA4B3232-1881-4F09-9DBA-0983D553BF46}2'. Operating system error 995(The I/O operation has been aborted because of either a thread exit or an application request.).
 2022-05-29 15:55:42.91 Backup      Error: 18210, Severity: 16, State: 1.
 2022-05-29 15:55:42.91 Backup      BackupIoRequest::ReportIoError: write failure on backup device '{AA4B3232-1881-4F09-9DBA-0983D553BF46}4'. Operating system error 995(The I/O operation has been aborted because of either a thread exit or an application request.).
 2022-05-29 15:55:42.91 Backup      Error: 3041, Severity: 16, State: 1.
 ```

It's common to see SQL Server Error 18210 with nested [OS error 995](/windows/win32/debug/system-error-codes--500-999-). The most common reason for OS error 995 is that the VDI application has aborted the backup process. Both errors are helpful in that you get a timestamp of when a backup failed. However, it doesn't give meaningful information as to root cause as these errors indicate the backup operation is aborting due to another error. Once you find the time frame of the first occurrence of the 18210 error, you then have a reference timestamp to review your backup application logs that may provide further root cause information.


## Cause

While the cause can be varied, ultimately the error is due to a failed IO submission to the Operating System. Some examples:

1. Backup virtual device IO failure.
1. A file-related operation failure in one or more of the following I/O API functions ([DeleteFile](/windows/win32/api/fileapi/nf-fileapi-deletefilea) , [ReadFile](/windows/win32/api/fileapi/nf-fileapi-readfile), or [WriteFile](/windows/win32/api/fileapi/nf-fileapi-writefile)).
1. Failure in freeing a memory buffer.


## User action  

Since the most common reason for an 18210 error is a VDI backup failure, the best starting point is to identify the component/service invoking VDI and checking the application log for that corresponding application. Some data points to check:

1. Most importantly, the backup application logs
1. Windows Application Event Log
1. Windows System Event Log
1. If the backup is being invoked by SQLWriter, review [SQL Server VSS Writer logging](../backup-restore/sql-server-vss-writer-logging.md) and troubleshoot accordingly.
1. Attempt to narrow the backup issue such as if the issue is specific to a given database and is reproducible? Does issue happen at a repeated time frame or interval?
1. Does running a VDI backup through [SQL Server Backup Simulator](https://github.com/microsoft/tigertoolbox/releases/tag/v2.0.0) also reproduce the error?
1. Check for system issues such as low system memory
1. Check for filter drivers locking a file (antivirus)
1. Check disk health
1. For advanced troubleshooting:
    1. Enable [Trace Flag 3605](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md) for more logging to the SQL Server Error Log prior to encountering the issue. Avoid keeping this TF enabled long-term.
    1. When issue is reproduced, capture [Process Monitor](/sysinternals/downloads/procmon)
    1. Capture [Extended Events](../extended-events/extended-events.md) or [SQL Server Profiler](../../tools/sql-server-profiler/sql-server-profiler.md) when the reproducing the error.
