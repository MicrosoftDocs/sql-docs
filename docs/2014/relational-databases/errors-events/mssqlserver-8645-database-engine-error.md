---
title: "MSSQLSERVER_8645 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "8645 (Database Engine error)"
ms.assetid: 63d6d6d7-3850-4061-8e96-b1fa665e3180
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_8645
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|8645|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|MEMTIMEDOUT_ERR|  
|Message Text|A time out occurred while waiting for memory resources to execute the query. Rerun the query.|  
  
## Explanation  
 A timeout occurred while waiting for memory resources to execute the query in the resource pool 'default'.  
  
## User Action  
 If you are not using Resource Governor, we recommend that you verify the overall server state and load, or check the resource pool or workload group settings.  
  
 The following list outlines general steps that will help in troubleshooting memory errors:  
  
1.  Verify whether other applications or services are consuming memory on this server. Reconfigure less critical applications or services to consume less memory.  
  
2.  Start collecting performance monitor counters for **SQL Server: Buffer Manager**, **SQL Server: Memory Manager**.  
  
3.  Check the following SQL Server memory configuration parameters:  
  
    -   **max server memory**  
  
    -   **min server memory**  
  
    -   **min memory per query**  
  
     Notice unusual settings. Correct them as necessary. Account for increased memory requirements for [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. Default settings are listed in "Setting Server Configuration Options" in SQL Server Books Online.  
  
4.  Observe DBCC MEMORYSTATUS output and the way it changes when you see these error messages.  
  
5.  Check the workload (for example, number of concurrent sessions, currently executing queries).  
  
 The following actions may make more memory available to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
-   If applications besides SQL Server are consuming resources, try stopping running these applications or consider running them on a separate server. This will remove external memory pressure.  
  
-   If you have configured **max server memory,** increase its setting.  
  
 Run the following DBCC commands to free several SQL Server memory caches.  
  
-   DBCC FREESYSTEMCACHE  
  
-   DBCC FREESESSIONCACHE  
  
-   DBCC FREEPROCCACHE  
  
 If the problem continues, you will need to investigate further and possibly reduce workload.  
  
  
