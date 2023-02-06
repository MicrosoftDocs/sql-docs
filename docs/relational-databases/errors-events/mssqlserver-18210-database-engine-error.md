---
description: "MSSQLSERVER_18210"
title: "MSSQLSERVER_18210 | Microsoft Docs"
ms.custom: ""
ms.date: "02/06/2023"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "18210 (Database Engine error)"
author: jaferebe
ms.author: jaferebe
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

When performing a VDI backup in SQL Server (which may be from SQLWriter or from some other application), if the backup is terminated you should see a SQL Server error 18210 which references nested OS error 995 such as below:

    2022-05-29 15:55:42.89 Backup      Error: 18210, Severity: 16, State: 1.
    2022-05-29 15:55:42.89 Backup      BackupIoRequest::ReportIoError: write failure on backup device '{AA4B3232-1881-4F09-9DBA-0983D553BF46}2'. Operating system error 995(The I/O operation has been aborted because of either a thread exit or an application request.).
    2022-05-29 15:55:42.91 Backup      Error: 18210, Severity: 16, State: 1.
    2022-05-29 15:55:42.91 Backup      BackupIoRequest::ReportIoError: write failure on backup device '{AA4B3232-1881-4F09-9DBA-0983D553BF46}4'. Operating system error 995(The I/O operation has been aborted because of either a thread exit or an application request.).
    2022-05-29 15:55:42.91 Backup      Error: 3041, Severity: 16, State: 1.

Both errors are helpful in that you get a timestamp of when a backup failed. However, it does NOT give meaningful information as to root cause. Once you find the time frame of the first 18210 error with the nested OS error 995, you have a reference data point to review your backup application logs which should provide further root cause information.

## Cause

While the cause can be varied, ultimately the error is due to a failed IO submission to the Operating System. Some example causes are below:

1. Backup virtual device IO failure.
1. Failure in freeing a buffer.
1. Delete file, read file, or write file failure.

 
## User action  

Since the most common reason for an 18210 error is a VDI backup failure, the best starting point is to identify the component/service invoking VDI and checking the application log for that corresponding application. Other logs to check include the application and system event logs, as well as SQL Server error log for the isolated time frame.

Also check for system issues such as low system memory, filter drivers locking a file (antivirus), disk health, and data corruption.