---
title: "MSSQLSERVER_8651 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "8651 (Database Engine error)"
ms.assetid: 4cc3498d-5449-4c4e-b1f9-3271831c725a
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_8651
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|8651|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|MEMGRANT_ERR|  
|Message Text|Could not perform the requested operation because the minimum query memory is not available. Decrease the configured value for the 'min memory per query' server configuration option.|  
  
## Explanation  
 Other processes are consuming server memory (exerting memory pressure in the server).  
  
## User Action  
 Either decrease the configured value for the min memory per query' server configuration option or reduce the query load to the server.  
  
 The following list outlines general steps that will help in troubleshooting memory errors:  
  
1.  Verify whether other applications or services are consuming memory on this server. Reconfigure less critical applications or services to consume less memory.  
  
2.  Start collecting performance monitor counters for **SQL Server: Buffer Manager**, **SQL Server: Memory Manager**.  
  
3.  Check the following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory configuration parameters:  
  
    -   **max server memory**  
  
    -   **min server memory**  
  
    -   **min memory per query**  
  
     Notice unusual settings. Correct them as necessary. Default settings are listed in "Setting Server Configuration Options" in SQL Server Books Online.  
  
4.  Check the workload (for example, number of concurrent sessions, currently executing queries).  
  
 The following actions may make more memory available to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
-   If applications besides [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are consuming resources, try stopping running these applications or consider running them on a separate server. This will remove external memory pressure.  
  
-   If you have configured **max server memory,** increase its setting.  
  
 Run the following DBCC commands to free several [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory caches.  
  
-   DBCC FREESYSTEMCACHE  
  
-   DBCC FREESESSIONCACHE  
  
-   DBCC FREEPROCCACHE  
  
 If the problem continues, you will need to investigate further and possibly reduce workload.  
  
## See Also  
 [DBCC FREESYSTEMCACHE &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-freesystemcache-transact-sql)   
 [DBCC FREESESSIONCACHE &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-freesessioncache-transact-sql)   
 [DBCC FREEPROCCACHE &#40;Transact-SQL&#41;](/sql/t-sql/database-console-commands/dbcc-freeproccache-transact-sql)   
 [Server Configuration Options &#40;SQL Server&#41;](../../database-engine/configure-windows/server-configuration-options-sql-server.md)   
 [SQL Server, Buffer Manager Object](../performance-monitor/sql-server-buffer-manager-object.md)   
 [SQL Server, Memory Manager Object](../performance-monitor/sql-server-memory-manager-object.md)  
  
  
