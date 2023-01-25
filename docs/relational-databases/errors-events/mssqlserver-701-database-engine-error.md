---
description: "MSSQLSERVER_701"
title: "MSSQLSERVER_701"
ms.custom: ""
ms.date: "03/16/2021"
ms.service: sql
ms.subservice: supportability
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
|Message Text|There's insufficient system memory to run this query.|  

> [!NOTE]
> **This article is focused on SQL Server.** For information on troubleshooting out of memory issues in Azure SQL Database, see [Troubleshoot out of memory errors with Azure SQL Database](/azure/azure-sql/database/troubleshoot-memory-errors-issues).

## Explanation  
Error 701 occurs when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has failed to allocate sufficient memory to run a query. Insufficient memory can be caused by a number of factors that include operating system settings, physical memory availability, other components use memory inside SQL Server, or memory limits on the current workload. In most cases, the transaction that failed isn't the cause of this error. Overall, the causes can be grouped into three:

### External or OS memory pressure
  External pressure refers to high memory utilization coming from a component outside of the process that leads to insufficient memory for SQL Server. You have to find if other applications on the system are consuming memory and contribute to low memory availability. SQL Server is one of the few applications designed to respond to OS memory pressure by cutting back its memory use. This means, if some application or driver asks for memory, the OS sends a signal to all applications to free up memory and SQL Server will respond by reducing its own memory usage. Very few other applications respond because they aren't designed to listen for that notification. So if SQL starts cutting back its memory usage, its memory pool is reduced and whichever components need memory may not get it. You start getting 701 and other memory-related errors. For more information, see [SQL Server Memory Architecture](../memory-management-architecture-guide.md#sql-server-memory-architecture)

### Internal memory pressure, not coming from SQL Server

Internal memory pressure refers to low memory availability caused by factors inside the SQL Server process. There are components that may run inside the SQL Server process that are "external" to the SQL Server engine. Examples include DLLs like linked servers, SQLCLR components, extended procedures (XPs), and OLE Automation (`sp_OA*`). Others include anti-virus or other security programs that inject DLLs inside a process for monitoring purposes. An issue or poor design in any of these components could lead to large memory consumption. For example, consider a linked server caching 20 million rows of data that come from an external source into SQL Server memory. As far as SQL Server is concerned, no memory clerk will report high memory usage, but memory consumed inside the SQL Server process will be high. This memory growth from a linked server DLL, for example, would cause SQL Server to start cutting its memory usage (see above) and will create low-memory conditions of for components inside SQL Server, causing errors like 701.


### Internal memory pressure, coming from SQL Server component(s)
  
Internal memory pressure coming from components inside SQL Server Engine can also lead to error 701. There are hundreds of components, tracked via [memory clerks](../system-dynamic-management-views/sys-dm-os-memory-clerks-transact-sql.md), that allocate memory in SQL Server. You must identify which memory clerk(s) are responsible for the largest memory allocations to be able to resolve this further. For example, if you find that the OBJECTSTORE_LOCK_MANAGER memory clerk is showing the large memory allocation, you need to further understand why the Lock Manager is consuming so much memory. You may find there are queries that acquire a large number of locks and optimize them by using indexes, or shorten transactions that hold locks for long periods, or check if lock escalation is disabled. Each memory clerk or component has a unique way of accessing and using memory. For more information, see [memory clerk types](../system-dynamic-management-views/sys-dm-os-memory-clerks-transact-sql.md) and their descriptions.

## User action  

If error 701 appears occasionally or for a brief period, there may be a short-lived memory issue that resolved itself. You may not need to take action in those cases. However, if the error occurs multiple times, on multiple connections and persists for periods of seconds or longer, follow the steps to troubleshoot further.

The following list outlines general steps that will help in troubleshooting memory errors. 

### Diagnostic tools and capture

The diagnostics tools that will allow you to collect troubleshooting data are **Performance Monitor**, **[sys.dm_os_memory_clerks](../system-dynamic-management-views/sys-dm-os-memory-clerks-transact-sql.md)**, and **[DBCC MEMORYSTATUS](/troubleshoot/sql/performance/dbcc-memorystatus-monitor-memory-usage)**. 

Configure and collect the following counters with Performance Monitor: 
  - **Memory:Available MB**
  - **Process:Working Set**
  - **Process:Private Bytes**
  - **SQL Server:Memory Manager: (all counters)**
  - **SQL Server:Buffer Manager: (all counters)**

Collect periodic outputs of this query on the impacted SQL Server

  ```tsql
  SELECT pages_kb, type, name, virtual_memory_committed_kb, awe_allocated_kb
  FROM sys.dm_os_memory_clerks
  ORDER BY pages_kb DESC
  ```

### Pssdiag or SQL LogScout

An alternative, automated way to capture these data points is to use tools like [PSSDIAG](https://github.com/microsoft/DiagManager/releases) or [SQL LogScout](https://github.com/microsoft/SQL_LogScout/releases). 

- If you use Pssdiag, configure to capture **Perfmon** collector and the **Custom Diagnostics\SQL Memory Error** collector
- If you use SQL LogScout, configure to capture the **Memory** scenario

The following sections describe more detailed steps for each scenario - external or internal memory pressure. 

### External pressure: diagnostics and solutions

- To diagnose low memory conditions on the system outside of SQL Server process, collect Performance monitor counters. Investigate if applications or services other than SQL Server are consuming memory on this server by looking at these counters:

  - **Memory:Available MB**
  - **Process:Working Set**
  - **Process:Private Bytes**

  Here is a sample Perfmon log collection using PowerShell

  ```PowerShell
  clear
  $serverName = $env:COMPUTERNAME
  $Counters = @(
     ("\\$serverName" +"\Memory\Available MBytes"),
     ("\\$serverName" +"\Process(*)\Working Set"),
     ("\\$serverName" +"\Process(*)\Private Bytes")
  )
  
  Get-Counter -Counter $Counters -SampleInterval 2 -MaxSamples 1 | ForEach-Object  {
  $_.CounterSamples | ForEach-Object 	  {
     [pscustomobject]@{
        TimeStamp = $_.TimeStamp
        Path = $_.Path
        Value = ([Math]::Round($_.CookedValue, 3)) }
   }
  }
  ```


- Review the System Event log and look for memory related errors (for example, low virtual memory). 
- Review the Application Event log for application-related memory issues.

  Here is a sample PowerShell script to query the System and Applicaiton Event logs for the keyword "memory". Feel free to use other strings like "resource" for your search:
  
  ```PowerShell
  Get-EventLog System -ComputerName "$env:COMPUTERNAME" -Message "*memory*"
  Get-EventLog Application -ComputerName "$env:COMPUTERNAME" -Message "*memory*"
  ```

- Address any code or configuration issues for less critical applications or services to reduce their memory usage.  
- If applications besides [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are consuming resources, try stopping or rescheduling these applications, or consider running them on a separate server. These steps will remove external memory pressure.

### Internal memory pressure, not coming from SQL Server: diagnostics and solutions

To diagnose internal memory pressure caused by modules (DLLs) inside SQL Server, use the following approach:

- If SQL Server isn't* using [Locked Pages in Memory](../../database-engine/configure-windows/enable-the-lock-pages-in-memory-option-windows.md) (AWE API), then most its memory is reflected in the **Process:Private Bytes** counter (`SQLServr` instance) in Performance Monitor. The overall memory usage coming from within SQL Server engine is reflected in **SQL Server:Memory Manager: Total Server Memory (KB)** counter. If you find a significant difference between the value **Process:Private Bytes** and **SQL Server:Memory Manager: Total Server Memory (KB)**, then that difference is likely coming from a DLL (linked server, XP, SQLCLR, etc.). For example if **Private bytes** is 300 GB and **Total Server Memory** is 250 GB, then approximately 50 GB of the overall memory in the process is coming from outside SQL Server engine.

- If SQL Server is using Locked Pages in Memory (AWE API), then it is more challenging to identify the issue because Performance monitor doesn't offer AWE counters that track memory usage for individual processes. The overall memory usage coming from within SQL Server engine is reflected in **SQL Server:Memory Manager: Total Server Memory (KB)** counter. Typical **Process:Private Bytes** values may vary between 300 MB and 1-2 GB overall. If you find a significant usage of **Process:Private Bytes** beyond this typical use, then the difference is likely coming from a DLL (linked server, XP, SQLCLR, etc.). For example, if **Private bytes** counter is 5-4 GB and SQL Server is using Locked Pages in Memory (AWE), then a large part of the Private bytes may be coming from outside SQL Server engine. This is an approximation technique.

- Use the Tasklist utility to identify any DLLs that are loaded inside SQL Server space:

  ```console
   tasklist /M /FI "IMAGENAME eq sqlservr.exe"
  ```

- You could also use this query to examine loaded modules (DLLs) and see if something isn't expected to be there

  ```sql
  SELECT * FROM sys.dm_os_loaded_modules
  ```

- If you suspect that a Linked Server module is causing significant memory consumption, then you can configure it to run out of process by disabling **Allow inprocess** option. See [Create Linked Servers](../linked-servers/create-linked-servers-sql-server-database-engine.md) for more information. Not all linked server OLEDB providers may run out of process; contact the product manufacturer for more information.

- In the rare case that OLE automation objects are used (`sp_OA*`), you may configure the object to run in a process outside SQL Server by setting *context* = 4 (Local (.exe) OLE server only.). For more information, see [sp_OACreate](../system-stored-procedures/sp-oacreate-transact-sql.md).


### Internal memory usage by SQL Server engine: diagnostics and solutions

- Start collecting performance monitor counters for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:**SQL Server:Buffer Manager**, **SQL Server: Memory Manager**.  

- Query the SQL Server memory clerks DMV multiple times to see where the highest consumption of memory occurs inside the engine:

  ```sql
  SELECT pages_kb, type, name, virtual_memory_committed_kb, awe_allocated_kb
  FROM sys.dm_os_memory_clerks
  ORDER BY pages_kb DESC
  ```

- Alternatively, you can observe the more detailed DBCC MEMORYSTATUS output and the way it changes when you see these error messages.  

  ```sql
  DBCC MEMORYSTATUS
  ```

- If you identify a clear offender among the memory clerks, focus on addressing the specifics of memory consumption for that component. Here are several examples:

  - If MEMORYCLERK_SQLQERESERVATIONS memory clerk is consuming memory, identify queries that are using huge memory grants and optimize them via indexes, rewrite them (remove ORDER by for example), or apply query hints.
  - If a large number of ad-hoc query plans are cached, then the CACHESTORE_SQLCP memory clerk would use large amounts of memory. Identify non-parameterized queries whose query plans can't be reused and parameterize them by either converting to stored procedures, or by using sp_executesql, or by using FORCED parameterization.
  - If object plan cache store CACHESTORE_OBJCP is consuming much memory, then do the following: identify which stored procedures, functions, or triggers are using lots of memory and possibly redesign the application. Commonly this may happen due to large amounts of database or schemas with hundreds of procedures in each.
  - If the OBJECTSTORE_LOCK_MANAGER memory clerk is showing the large memory allocations, identify queries that apply many locks and optimize them by using indexes. Shorten transactions that cause locks not to be released for long periods in certain isolation levels, or check if lock escalation is disabled.

## Quick relief that may make memory available

The following actions may free some memory and make it available to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  

- Check the following SQL Server memory configuration parameters and consider increasing **max server memory** if possible:  
  
  - **max server memory**  
  - **min server memory**  
  
    Notice unusual settings. Correct them as necessary. Account for increased memory requirements. Default settings are listed in [Server memory configuration options](../../database-engine/configure-windows/server-memory-server-configuration-options.md).
  
- If you haven't configured **max server memory** especially with Locked Pages in Memory, consider setting to a particular value to allow some memory for the OS. See [Locked Pages in Memory](../../database-engine/configure-windows/server-memory-server-configuration-options.md#lock-pages-in-memory-lpim) server configuration option. 

- Check the query workload: number of concurrent sessions, currently executing queries and see if there are less critical applications that may be stopped temporarily or moved to another SQL Server.

- If you're running SQL Server on a virtual machine (VM), ensure the memory for the VM isn't overcommitted. For ideas on how to configure memory for VMs, see this blog [Virtualization – Overcommitting memory and how to detect it within the VM](https://techcommunity.microsoft.com/t5/running-sap-applications-on-the/virtualization-8211-overcommitting-memory-and-how-to-detect-it/ba-p/367623) and  [Troubleshooting ESX/ESXi virtual machine performance issues (memory overcommitment)](https://kb.vmware.com/s/article/2001003#Memory) 

- You can run the following DBCC commands to free several [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory caches.  
  
  - DBCC FREESYSTEMCACHE
  
  - DBCC FREESESSIONCACHE  
  
  - DBCC FREEPROCCACHE  

- If you're using Resource Governor, we recommend that you check the resource pool or workload group settings and see if they aren't limiting memory too drastically. 
- If the problem continues, you'll need to investigate further and possibly increase server resources (RAM).
