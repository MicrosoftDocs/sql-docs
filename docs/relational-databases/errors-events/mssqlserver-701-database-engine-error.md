---
description: "MSSQLSERVER_701"
title: "MSSQLSERVER_701"
ms.custom: ""
ms.date: "11/04/2021"
ms.prod: sql
ms.technology: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "701 (Database Engine error)"
author: MashaMSFT
ms.author: mathoma
ms.reviewer: wiassaf
---
# MSSQLSERVER_701
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|701|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|NOSYSMEM|  
|Message Text|There is insufficient system memory to run this query.|  

> [!NOTE]
> **This article is focused on SQL Server.** For information on troubleshooting out of memory issues in Azure SQL Database, see [Troubleshoot out of memory errors with Azure SQL Database](/azure/azure-sql/database/troubleshoot-memory-errors-issues).

## Explanation  
This error occurs when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has failed to allocate sufficient memory to run a query. This can be caused by various reasons including operating system settings, physical memory availability, too many other components using memory inside SQL Server, or memory limits on the current workload. In most cases, the transaction that failed is not the cause of this error. Overall, the caused can be grouped into three:

### External or OS memory pressure
  External pressure refers high memory utilizaton coming from a component outside of the SQL Server process. This means, you have to find if other applications on the system are consuming memory and leading to low memory availability. SQL Server is one of the very few applications written to respond to OS memory pressure by cutting back its memory use.This means, if some application or driver asks for memory, the OS sends a signal to everyone to free up memory and SQL Server will respond by reducing its own memory usage. Very few applications respond because they are not designed to listen for that notification. SQL Server is designed this way. So if SQL starts cutting back its memory usage, it’s memory pool is reduced and whichever components need memory may not get it. You start getting 701 and other memory-related errors.

> [!NOTE]
> On PaaS systems like Azure SQL Database it is very difficult for you to diagnose external pressure since such metrics are not exposed

### Internal memory pressure, not coming from SQL Server

Internal memory pressure refers to low memory availability inside the SQL Server process. But there are components that may run inside the SQL Server process that are "external" to the SQL Server engine and yet run inside the SQL Server process. Examples include DLLs like Linked Servers, SQLCLR components, Extended procedures (XPs), OLE Automation (sp_OA*). Others include anti-virus or other security programs that inject DLLs inside a process for monitoring purposes. A issue or poor design in any of these components could lead to large memory consumption. For example, think of a linked server caching 20 million rows of data that come from an external source into SQL Server memory. As far as SQL Server is concerned, no memory clerk will report high memory usage, but memory consumed inside the SQL Server process will be high. This memory growth from a linked server DLL, for example, would cause SQL Server to start cutting its memory usage (see above) and will create low-memory conditions of for components inside SQL Server, causing errors like 701.

### Internal memory pressure, coming from SQL Server component(s)
  
Internal memory pressure coming from components inside SQL Server Engine can also lead to error 701. There are hundreds of components, tracked via [memory clerks](../system-dynamic-management-views/sys-dm-os-memory-clerks-transact-sql.md), that allocate memory in SQL Server. You must identify which memory clerk(s) are responsible for the majority of memory allocations to be able to resolve this further. For example, if you find that the OBJECTSTORE_LOCK_MANAGER memory clerk is showing the large memory allocation, you need to further understand why the Lock Manager is consuming so much memory.

  
    If lock manager OBJECTSTORE_LOCK_MANAGER consuming memory)
    
                   {
    
                       Find which queries use a lot of locks and opitimize them (say indexes)
    
                    }
    
                    elseIf (reservations MEMORYCLERK_SQLQERESERVATIONS consuming memory)
    
                    {
    
    Find queries that are doing huge memory grants and optimize them (indexes) , re-write them (remove ORDER by , e.g), or apply query hints
    
     
    
    }
    
    elseIf (ad-hoc query plan - CACHESTORE_SQLCP- consuming lots of memory)
    
    {
    
                    Identify queries that are doing that – most commonly non-parameterized queries whose plans cannot be reused and parameterize them (convert to stored procs, or sp_executesql, or use FORCED parameterization, etc
    
     
    
    }
    
    Elseif (object plan cache - CACHESTORE_OBJCP – consuming lots of memory)
    
    {
    
                    Identify which stored proc, functions, triggers are using lots of memory and possibly redesign app to not have stored procs. Commonly this is because people have hundreds of DBs with hundreds of procs in each db, etc.
    
     
    
    }
    
     
    
    and on and on….
    
    …..
    
     
    
    Elseif (memory_clerk_590 consumes lots of memory)
    
    {
    
                    You tackle depending on what its doing
    
    }
    
    Elseif (memory_clerk_591 consumes lots of memory)
    
    {
    
                    You tackle depending on what it’s doing
    
    }
    
    Elseif (memory_clerk_N consumes lots of memory)
    
    {
    
                    You tackle depending on what N is doing
    
    }
    
 

  
## User action  
If you are not using Resource Governor, we recommend that you verify the overall server state and load, or check the resource pool or workload group settings.  
  
The following list outlines general steps that will help in troubleshooting memory errors:  

External pressure diagnostics and solutions
In general we collect Perfmon data and look for Available memory and Process: Working Set sizes and Private bytes consumed by others. Unfortunately, AWE memory is not reflected by any Perfmon counters, so if some App , not SQL, is using AWE, we are out of luck. Saving grace is sysinternal tools like RAM Map, but that’s not perfect too.

  
1.  Verify whether other applications or services are consuming memory on this server. Reconfigure less critical applications or services to consume less memory.  
  
2.  Start collecting performance monitor counters for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]**: Buffer Manager**, **SQL Server: Memory Manager**.  
  
3.  Check the following SQL Server memory configuration parameters:  
  
    -   **max server memory**  
  
    -   **min server memory**  
  
    -   **min memory per query**  
  
    Notice unusual settings. Correct them as necessary. Account for increased memory requirements. Default settings are listed in [Server memory configuration options](../../database-engine/configure-windows/server-memory-server-configuration-options.md).
  
4.  Observe DBCC MEMORYSTATUS output and the way it changes when you see these error messages.  
  
5.  Check the workload (for example, number of concurrent sessions, currently executing queries).  

The following actions may make more memory available to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
-   If applications besides [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are consuming resources, try stopping running these applications or consider running them on a separate server. These steps will remove external memory pressure.  
  
-   If you have configured **max server memory**, increase its setting. For more information, see [Set options manually](../../database-engine/configure-windows/server-memory-server-configuration-options.md#manually).
  
Run the following DBCC commands to free several [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory caches.  
  
-   DBCC FREESYSTEMCACHE  
  
-   DBCC FREESESSIONCACHE  
  
-   DBCC FREEPROCCACHE  
  
If the problem continues, you will need to investigate further and possibly increase server resources or reduce workload.  
  



