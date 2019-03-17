---
title: "MSSQLSERVER_845 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "845 (Database Engine error)"
ms.assetid: 8fff6ad4-234c-44be-b123-e25d5e1cd63e
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_845
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|845|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|BUFLATCH_TIMEOUT|  
|Message Text|Time-out occurred while waiting for buffer latch type %d for page %S_PGID, database ID %d.|  
  
## Explanation  
A process was waiting to acquire a latch, but the process waited until the time limit expired and failed to acquire one. This can occur if an I/O operation takes too long to complete, usually as a result of other tasks blocking system processes. In some instances, this error may be the result of hardware failure.  
  
## User Action  
Performing the following tasks may prevent this error:  
  
-   Reduce the workload.  
  
-   Verify whether there are associated I/O failures in the error log or event log. I/O failures are typically caused by disk malfunction.  
  
-   Check error log for non-yielding tasks and other critical errors.  
  
-   If critical errors such as asserts frequently occur, resolve these problems.  
  
If the error persists, contact Microsoft Customer Service and Support.  
  
