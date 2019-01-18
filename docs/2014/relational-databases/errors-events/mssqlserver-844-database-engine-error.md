---
title: "MSSQLSERVER_844 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "844 (Database Engine error)"
ms.assetid: 2060c886-1226-4066-bc0c-de90a1cfb82b
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_844
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|844|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|BUFLATCH_TIMEOUT_CONTINUE|  
|Message Text|Time-out occurred while waiting for buffer latch -- type %d, bp %p, page %d:%d, stat %#x, database id: %d, allocation unit id: %I64d%ls, task 0x%p : %d, waittime %d, flags 0x%I64x, owning task 0x%p.  Continuing to wait.|  
  
## Explanation  
 A process is waiting to acquire a latch. This problem can be caused by an I/O operation taking too long to complete. Typically this type of error is the result of other tasks blocking system processes. In some instances, this error may be the result of hardware failure.  
  
## User Action  
 Try the following to prevent this error from occurring:  
  
-   Reduce workload.  
  
-   Check for associated I/O failures in error log or event log. I/O failures typically point to a disk malfunction.  
  
-   Check the error log for non-yielding tasks and other critical errors.  
  
-   If critical errors such as asserts frequently occur, resolve these problems.  
  
 If the error persists, contact Microsoft Customer Service and Support.  
  
  
