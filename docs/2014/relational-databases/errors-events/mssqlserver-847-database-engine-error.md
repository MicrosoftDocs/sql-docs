---
title: "MSSQLSERVER_847 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "847 (Database Engine error)"
ms.assetid: 67208b7c-bd8d-48a1-9f70-a6488e0f5f9b
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_847
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|847|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|N/A|  
|Message Text|Time-out occurred while waiting for latch: class '%ls', id %p, type %d, Task 0x%p : %d, waittime %d, flags 0x%I64x, owning task 0x%p. Continuing to wait.|  
  
## Explanation  
 A computer might stop responding (hang), or a time-out or some other disruption of regular operations might occur at the same time that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] writes buffer latch errors to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log.  
  
 If the stat field in the message has the value of 0x04 on, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is waiting for an I/O operation. You may also receive message [MSSQLSERVER_833](mssqlserver-833-database-engine-error.md) in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log.  
  
 If the stat field in the message has the value 0x04 off, there is heavy contention for a page. If the object is a data page, this can be caused by inefficient code design. If the page is nondata, the error might be caused by server bottlenecks, such as insufficient hardware resources.  
  
## User Action  
 To work around this problem, depending on your environment, one or more of the following steps might reduce or eliminate the error messages:  
  
-   Determine whether you have any hardware bottlenecks. If it is necessary, upgrade your hardware so that it can support the configuration, query, and load requirements of your environment. For more information about bottlenecks, see [Identify Bottlenecks](../performance/identify-bottlenecks.md).  
  
-   Check for any logged errors and run any diagnostics provided by your hardware vendor.  
  
-   Make sure that your disk drives are not compressed. Storing data or log files on compressed drives is not supported. For more information about physical files, see [Database Files and Filegroups](../databases/database-files-and-filegroups.md).  
  
-   See whether the error messages disappear when you set the following options to off:  
  
    -   SQL Server priority boost configuration option  
  
    -   Lightweight pooling (fiber mode) option  
  
    -   Set working set size option  
  
    > [!NOTE]  
    >  The previous settings can frequently be counter-productive if you change them from their default setting of OFF. For more information about the settings, see [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md).  
  
-   Tune queries to reduce resources used on the system. Performance tuning will help reduce the stress on a system and improve response time for individual queries.  
  
-   Set the AUTO_SHRINK option to OFF to reduce the overhead of changes to the database size.  
  
-   Make sure that you set the FILEGROWTH option to increments that are large enough to be infrequent. Schedule a job to check the available space in the databases, and then increase the database size during nonpeak hours.  
  
  
