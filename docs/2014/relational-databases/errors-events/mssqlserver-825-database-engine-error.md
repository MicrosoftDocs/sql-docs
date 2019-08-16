---
title: "MSSQLSERVER_825 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "825 (Database Engine error)"
ms.assetid: f69f8214-5af1-4769-878b-117ad6eaff52
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_825
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|825|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|B_RETRYWORKED|  
|Message Text|A read of the file '%ls' at offset %#016I64x succeeded after failing %d time(s) with error: %ls. Additional messages in the SQL Server error log and system event log may provide more detail. This error condition threatens database integrity and must be corrected. Complete a full database consistency check (DBCC CHECKDB). This error can be caused by many factors; for more information, see SQL Server Books Online.|  
  
## Explanation  
 This message indicates that the read operation had to be reissued at least one time, and indicates a major problem with the disk hardware. This message does not currently indicate a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] problem, but the disk problem could cause data loss or database corruption if it is not resolved. The system event log may contain related events that help to diagnose the problem. For more information about I/O errors, see [Microsoft SQL Server I/O Basics, Chapter 2](/previous-versions/sql/sql-server-2005/administrator/cc917726(v=technet.10)).  
  
## User Action  
 The following actions may help you identify and resolve the underlying problem:  
  
-   Review the error log and the variable text in this message for clues that explain the problem.  
  
-   Check your disk system. The problem could be related to the disks, the disk controllers, array cards, or disk drivers.  
  
-   Contact the disk manufacturer for the latest utilities for checking the status of your disk system.  
  
-   Contact the disk manufacturer for the latest driver updates.  
  
  
