---
title: "MSSQLSERVER_823 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "823 (Database Engine error)"
ms.assetid: 0d9fce3c-3772-46ce-a7a3-4f4988dc6cae
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_823
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|823|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|B_HARDERR|  
|Message Text|The operating system returned error %ls to SQL Server during a %S_MSG at offset %#016I64x in file '%ls'. Additional messages in the SQL Server error log and system event log may provide more detail. This is a severe system-level error condition that threatens database integrity and must be corrected immediately. Complete a full database consistency check (DBCC CHECKDB). This error can be caused by many factors; for more information, see SQL Server Books Online.|  
  
## Explanation  
 A Windows read or write request has failed. The error code that is returned by Windows and the corresponding text are inserted into the message. In the read case, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will have already retried the read request four times. This error is often the result of a hardware error, but may be caused by the device driver. For more information about error 823, see [https://support.microsoft.com/kb/828339](https://support.microsoft.com/kb/828339). For more information about I/O errors, see [Microsoft SQL Server I/O Basics, Chapter 2](https://go.microsoft.com/fwlink/?LinkId=69370).  
  
## User Action  
 Check for additional information in the system event log. Contact the hardware manufacturer or Microsoft Customer Services and Support to determine the cause and corrective action. After the hardware error is fixed, restore all databases and run DBCC CHECKDB.  
  
  
