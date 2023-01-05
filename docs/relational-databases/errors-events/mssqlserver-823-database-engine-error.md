---
title: "MSSQLSERVER error 823  | mssqlserver_823"
description: A description and some common solutions to Microsoft SQL Server Error 823 (mssqlserver_823), which is a severe system-level error condition that threatens database integrity and must be addressed immediately. 
ms.custom: ""
ms.date: "01/27/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "823 (Database Engine error)"
ms.assetid: 0d9fce3c-3772-46ce-a7a3-4f4988dc6cae
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER error 823
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|823|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|B_HARDERR|  
|Message Text|The operating system returned error %ls to SQL Server during a %S_MSG at offset %#016I64x in file '%ls'. Additional messages in the SQL Server error log and system event log may provide more detail. This is a severe system-level error condition that threatens database integrity and must be corrected immediately. Complete a full database consistency check (DBCC CHECKDB). This error can be caused by many factors; for more information, see SQL Server Books Online.|  
  
## Explanation  
SQL Server uses Windows APIs (for example,  [ReadFile](/windows/win32/api/fileapi/nf-fileapi-readfile), [WriteFile](/windows/win32/api/fileapi/nf-fileapi-writefile), [ReadFileScatter](/windows/win32/api/fileapi/nf-fileapi-readfilescatter), [WriteFileGather](/windows/win32/api/fileapi/nf-fileapi-writefilegather)) to perform file I/O operations. After performing these I/O operations, SQL Server checks for any error conditions associated with these API calls. If the API calls fail with an Operating System error, then SQL Server reports Error 823.

 The 823 error message contains the following information:
 - The database file against which the I/O operation was performed
 - The offset within the file where the I/O operation was attempted. This is the physical byte offset from the start of the file. Dividing this number by 8192 will give you the logical page number that is affected by the error.
 - Whether the I/O operation is a read or write request
 - The Operating System error code and error description in parentheses
 

**Operating system error:** A read or write Windows API call is not successful, and SQL Server encounters an operating system error that is related to the Windows API call. The following message is an example of an 823 error:

```
Error: 823, Severity: 24, State: 2.
2010-03-06 22:41:19.55 spid58 The operating system returned error 1117 (The request could not be performed because of an I/O device error.) to SQL Server during a read at offset 0x0000002d460000 in file 'e:\program files\Microsoft SQL Server\mssql\data\mydb.MDF'. Additional messages in the SQL Server error log and system event log may provide more detail. This is a severe, system-level error condition that threatens database integrity and must be corrected immediately. It is recommended to complete a full database consistency check (DBCC CHECKDB). This error can be caused by many factors; for more information, see SQL Server Books Online.
```

You may or may not see errors from the DBCC CHECKDB statement on the database that is associated with the file in the error message. You can run the DBCC CHECKDB statement when you see an 823 error. If the DBCC CHECKDB statement does not report any errors, you probably have an intermittent system problem or a disk problem.

Additional diagnostic information for 823 errors may be written to the SQL Server error log file when you use trace flag 818.
For more information, see [KB 826433: Additional SQL Server diagnostics added to detect unreported I/O problems](https://support.microsoft.com/help/826433/sql-server-diagnostics-added-to-detect-unreported-i-o-problems-due-to)


## Cause
The 823 error message usually indicates that there is a problem with underlying storage system or the hardware or a driver that is in the path of the I/O request. You can encounter this error when there are inconsistencies in the file system or if the database file is damaged. In the case of a file read, SQL Server will have already retried the read request four times before it returns 823. If the retry operation succeeds, the query will not fail but message [825](mssqlserver-825-database-engine-error.md) will be written into the ERRORLOG and Event Log.

## User Action  
 - Review the [suspect_pages](../system-tables/suspect-pages-transact-sql.md) table in MSDB for other pages that are encountering this problem (in the same database or different databases).
 - Check the consistency of databases located on the same volume (as the one reported in the 823 message) using DBCC CHECKDB command. If you find inconsistencies from the DBCC CHECKDB command, use the guidance from [How to troubleshoot database consistency errors reported by DBCC CHECKB](https://support.microsoft.com/help/2015748/how-to-troubleshoot-database-consistency-errors-reported-by-dbcc-check). 
 - Review the Windows Event logs for any errors or messages reported from the Operating System or a Storage Device or a Device Driver. If they are related to this error in some manner, address those errors first. For example, apart from the 823 message, you may also notice an event like "The driver detected a controller error on \Device\Harddisk4\DR4" reported by the Disk source in the Event Log. In that case, you have to evaluate if this file is present on this device and then first correct those disk errors.
 - Use the [SQLIOSim](https://support.microsoft.com/help/231619/how-to-use-the-sqliosim-utility-to-simulate-sql-server-activity-on-a-d) utility to find out if these 823 errors can be reproduced outside of regular SQL Server I/O requests. The SQLIOSim utility ships with SQL Server 2008 and later versions so there is no need for a separate download. You can typically find it in your `C:\Program Files\Microsoft SQL Server\MSSQLxx.MSSQLSERVER\MSSQL\Binn` folder.
 - Work with your hardware vendor or device manufacturer to ensure
   - The hardware devices and the configuration conforms to the I/O requirements of SQL Server
   - The device drivers and other supporting software components of all devices in the I/O path are up to date
 - If the hardware vendor or device manufacturer provided you with any diagnostic utilities, use them to evaluate the health of the I/O system
 - Evaluate if there are [filter drivers](https://support.microsoft.com/help/2454053/use-of-system-filter-drivers-can-lead-to-sql-server-database-engine-pe) that exist in the path of these I/O requests that encounter problems.
   - Check if there are any updates to these filter drivers
   - Can these filter drivers be removed or disabled to observe if the problem that results in the 823 error goes away  
