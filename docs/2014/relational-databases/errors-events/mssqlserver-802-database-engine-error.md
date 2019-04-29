---
title: "MSSQLSERVER_802 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "802 (Database Engine error)"
ms.assetid: 5892ed24-4dcb-4bf9-a8a4-a7ca898832d5
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_802
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|802|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|NO_BUFS|  
|Message Text|There is insufficient memory available in the buffer pool.|  
  
## Explanation  
 This is caused when the buffer pool is full and the buffer pool can not grow any larger.  
  
## User Action  
 The following list outlines general steps that will help in troubleshooting memory errors:  
  
1.  Verify whether other applications or services are consuming memory on this server. Reconfigure less critical applications or services to consume less memory.  
  
2.  Start collecting performance monitor counters for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]**: Buffer Manager**, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]**: Memory Manager**.  
  
3.  Check the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory configuration parameters:  
  
    -   **max server memory**  
  
    -   **min server memory**  
  
    -   **min memory per query**  
  
     Notice any unusual settings and correct them as necessary. Account for increased memory requirements for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Default settings are listed in "Setting Server Configuration Options" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
4.  Observe DBCC MEMORYSTATUS output and the way it changes when you see these error messages.  
  
5.  Check the workload (number of concurrent sessions, currently executing queries).  
  
 The following actions may make more memory available to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
-   If applications besides [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are consuming resources, try stopping these applications or running them on a separate server.  
  
-   If you have configured **max server memory,** increase the setting.  
  
 Run the following DBCC commands to free several [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory caches.  
  
-   DBCC FREESYSTEMCACHE  
  
-   DBCC FREESESSIONCACHE  
  
-   DBCC FREEPROCCACHE  
  
 If the problem continues, you will need to investigate further and possibly reduce workload.  
  
  
