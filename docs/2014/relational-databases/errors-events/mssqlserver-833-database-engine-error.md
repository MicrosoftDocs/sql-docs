---
title: "MSSQLSERVER_833 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "833 (Database Engine error)"
ms.assetid: 14129cc4-be80-4772-9e3f-0e5da4d0696b
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_833
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|833|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|BUF_LONG_IO|  
|Message Text|SQL Server has encountered %d occurrence(s) of I/O requests taking longer than %d seconds to complete on file [%ls] in database`[%ls] (%d)`.  The OS file handle is 0x%p.  The offset of the latest long I/O is: %#016I64x.|  
  
## Explanation  
 This message indicates that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has issued a read or write request from disk, and that the request has taken longer than 15 seconds to return. This error is reported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and indicates a problem with the IO subsystem.  
  
### Possible Causes  
 This problem can be caused system performance issues, hardware errors, firmware errors, device driver problems, or filter driver intervention in the IO process.  
  
## User Action  
 Troubleshoot this error by examining the system event log for hardware-related error messages. Also, examine hardware-specific logs if they are available.  
  
 Use Performance Monitor to examine the following counters:  
  
-   **Average Disk Sec/Transfer**  
  
-   **Average Disk Queue Length**  
  
-   **Current Disk Queue Length**  
  
 For example, the **Average Disk Sec/Transfer** time on a computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is typically less than 15 milliseconds. If the **Average Disk Sec/Transfer** value increases, this indicates that the I/O subsystem is not optimally keeping up with the I/O demand.  
  
> [!NOTE]  
>  Disk access can be slowed by an antivirus program. To increase access speed, exclude the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data files that are specified in the error message from active virus scans.  
  
 For more information about I/O errors, see [Microsoft SQL Server I/O Basics, Chapter 2](https://go.microsoft.com/fwlink/?LinkId=69370) and the Knowledge Base article at [https://support.microsoft.com/kb/897284/en-us](https://support.microsoft.com/kb/897284/en-us).  
  
  
