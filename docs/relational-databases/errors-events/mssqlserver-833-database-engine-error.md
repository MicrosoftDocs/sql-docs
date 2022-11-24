---
description: "MSSQLSERVER_833"
title: "MSSQLSERVER_833 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "833 (Database Engine error)"
ms.assetid: 14129cc4-be80-4772-9e3f-0e5da4d0696b
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_833
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sql-asdbmi.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|833|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|BUF_LONG_IO|  
|Message Text|SQL Server has encountered %d occurrence(s) of I/O requests taking longer than %d seconds to complete on file [%ls] in database `[%ls] (%d)`.  The OS file handle is 0x%p.  The offset of the latest long I/O is: %#016I64x.|  
  
## Explanation  
This message indicates that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has issued a read or write request from disk, and that the request has taken longer than 15 seconds to return. This error is reported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and indicates a problem with the I/O subsystem. You might also notice other symptoms associated with this message: high wait times for PAGEIOLATCH waits, warnings, or errors in the system event log, indications of disk latency issues in system monitor counters. Monitor [sys.dm_io_virtual_file_stats](../system-dynamic-management-views/sys-dm-io-virtual-file-stats-transact-sql.md) and choose appropriate storage tier and IOPS for your storage throughput. 
  
### Possible Causes  
This problem can be caused by operating system performance issues, hardware errors, firmware errors, device driver problems, or filter driver intervention in the I/O process or storage path of database files. SQL Server records the time that it initiated an I/O request and records the time that the I/O was completed. If that difference is 15 seconds or longer, this condition is detected. This also means that SQL Server is not the cause of a delayed I/O condition that this message describes and reports. This condition is known as "stalled I/O." Most disk requests occur within the typical speed of the disk. This typical disk speed is frequently known as "disk seek time." Disk seek time for most standard disks occurs in 10 milliseconds or less. Therefore, 15 seconds is a very long time for the system I/O path to return to SQL Server. 
  
## User Action  
Troubleshoot this error by examining the system event log for hardware-related error messages. Also, examine hardware-specific logs if they are available. You should use the necessary methods and techniques to determine the cause of the delay in the operating system, with the drivers, or with the I/O hardware. Resolution of this problem could involve updating all device drivers and firmware or performing other diagnostics that are associated with your disk system. 
  
Use Performance Monitor to examine the following counters:  
  
-   **Average Disk Sec/Transfer**  
  
-   **Average Disk Queue Length**  
  
-   **Current Disk Queue Length**  
  
For example, the **Average Disk Sec/Transfer** time on a computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is typically less than 15 milliseconds. If the **Average Disk Sec/Transfer** value increases, this indicates that the I/O subsystem is not optimally keeping up with the I/O demand.

You also can use facilities like [Storport ETW logging](/archive/blogs/ntdebugging/storport-etw-logging-to-measure-requests-made-to-a-disk-unit) to measure the latency of requests that are made to a disk unit. Another similar disk I/O troubleshooting kit is available as a built-in profile of [Windows Performance Recorder](/windows-hardware/test/wpt/introduction-to-wpr).
  
> [!NOTE]  
> Disk access can be slowed by an antivirus program. To increase access speed, exclude the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data files that are specified in the error message from active virus scans. You can use the [fltmc.exe command line utility](/windows-hardware/drivers/ifs/development-and-testing-tools#fltmcexe-control-program) to query all the filter drivers installed on the system and to understand the functions it performs on the storage path to the database files. 
  
For more information about I/O errors, see [Microsoft SQL Server I/O Basics, Chapter 2](/previous-versions/sql/sql-server-2005/administrator/cc917726(v=technet.10)) and the Knowledge Base article at [https://support.microsoft.com/kb/897284/en-us](https://support.microsoft.com/kb/897284/en-us).  
