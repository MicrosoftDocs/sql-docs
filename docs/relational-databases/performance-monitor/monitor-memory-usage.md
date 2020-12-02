---
title: "Monitor Memory Usage | Microsoft Docs"
description: "Monitor a SQL Server instance to confirm that memory usage is within typical ranges. Use the Memory: Available Bytes and Memory: Pages/sec counters."
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "tuning databases [SQL Server], memory"
  - "monitoring server performance [SQL Server], memory usage"
  - "isolating memory [SQL Server]"
  - "paging rate [SQL Server]"
  - "memory [SQL Server], monitoring usage"
  - "monitoring [SQL Server], memory usage"
  - "low-memory conditions"
  - "database monitoring [SQL Server], memory usage"
  - "available memory [SQL Server]"
  - "page faults [SQL Server]"
  - "monitoring performance [SQL Server], memory usage"
  - "server performance [SQL Server], memory"
ms.assetid: 1aee3933-a11c-4b87-91b7-32f5ea38c87f
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Monitor Memory Usage
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Monitor an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] periodically to confirm that memory usage is within typical ranges. Be aware that each SQL Server instance may over time consume all the operating system memory allowed by [the **max server memory** option](../../database-engine/configure-windows/server-memory-server-configuration-options.md).  

## Monitor Operating System Memory   
 To monitor for a low-memory condition, use the following Windows server counters. Many operating system memory counters can be queried via the dynamic management view [sys.dm_os_process_memory](../system-dynamic-management-views/sys-dm-os-process-memory-transact-sql.md) and [sys.dm_os_sys_memory](../system-dynamic-management-views/sys-dm-os-sys-memory-transact-sql.md).

-   **Memory: Available Bytes**  
The **Available Bytes** counter indicates how many bytes of memory are currently available for use by processes.  Low values for the **Available Bytes** counter can indicate that there is an overall shortage of memory on the computer or that an application is not releasing memory. This value can be queried using [sys.dm_os_sys_memory](../system-dynamic-management-views/sys-dm-os-sys-memory-transact-sql.md).available_physical_memory_kb.

-   **Memory: Pages/sec**  
The **Pages/sec** counter indicates the number of pages that either were retrieved from disk due to hard page faults or written to disk to free space in the working set due to page faults. A high rate for the **Pages/sec** counter could indicate excessive paging.  

-   **Memory: Page Faults/sec**
This indicates **Page Faults/sec** for all processes including system processes. A low but non-zero rate of paging to disk (and hence page faults) is typical, even if the computer has plenty of available memory. The Microsoft Windows Virtual Memory Manager (VMM) takes pages from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md) and other processes as it trims the working-set sizes of those processes. This VMM activity tends to cause page faults. 

-   **Process: Page Faults/sec**
This indicates the rate of Page Faults for a given user process. Monitor the **Process: Page Faults/sec** counter to make sure that disk activity is not caused by paging by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. To determine whether [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or another process is the cause of excessive paging, monitor the **Process: Page Faults/sec** counter for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process instance.  
  
 For more information about resolving excessive paging, see the operating system documentation.  
  
## Isolating Memory Used by SQL Server  

 To monitor [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory usage, use the following [SQL Server object counters](use-sql-server-objects.md). Many SQL Server object counters can be queried via the dynamic management view [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md).

 By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] changes its memory requirements dynamically, on the basis of available system resources. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] needs more memory, it queries the operating system to determine whether free physical memory is available and uses the available memory. If there is low free memory for the OS, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will release memory back to the operating system until the low memory condition is alleviated, or until [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] reaches the minservermemory limit. However, you can override the option to dynamically use memory by using the **minservermemory**, and **maxservermemory** server configuration options. For more information, see [Server Memory Options](../../database-engine/configure-windows/server-memory-server-configuration-options.md).  
  
 To monitor the amount of memory that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses, examine the following performance counters:  
  
-   **SQL Server: Memory Manager: Total Server Memory (KB)**  
This is amount of the operating system's memory the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory manager has committed to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. If the **TotalServerMemory (KB)** counter is consistently high compared to the amount of physical memory in the computer, it may indicate that more memory is required. This number is expected to grow as required by actual activity, and will grow following SQL Server startup. A high **Total Server Memory** is not necessarily indicative of any memory pressure or memory constraint, only indicative of actual activity. It is normal for the **Total Server Memory** to be near the **Max Server Memory** server configuration option after a period of typical activity. If this number is consistently below the amount of memory that is set by the **max server memory** server options and the operating system's **Available Bytes** are low, the **max server memory** may be set too high.

-   **Process: Working Set**  
This is the amount of memory that is used by a process currently, according to the operating system. Query this counter using [sys.dm_os_process_memory](../system-dynamic-management-views/sys-dm-os-process-memory-transact-sql.md).physical_memory_in_use_kb.

-   **SQL Server: Buffer Manager: Database Pages**  
This is the number of pages in the buffer pool with database content. Does not include other nonbuffer pool memory within the SQL Server process. Query this counter using the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view.

-   **SQL Server: Buffer Manager: Buffer Cache Hit Ratio**  
 The **Buffer Cache Hit Ratio** counter is specific to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. A ratio of 90 or higher is desirable. A value greater than 90 percent indicates that more than 90 percent of all requests for data were satisfied from the data cache in memory without having to read from disk. Find more information on the SQL Server Buffer Manager, see the [SQL Server Buffer Manager Object](sql-server-buffer-manager-object.md). Query this counter using the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view.
 
-   **SQL Server: Buffer Manager: Page life expectancy**  
 The **Page life expectancy** counter measures amount of time in seconds that pages stay in cache, on average across the buffer pool. A higher, growing value is best. A sudden dip in the **Page life expectancy** indicates a significant churn of data in and out of the buffer pool, indicating that a momentary workload needed fresh pages in the buffer pool and could not benefit from the data cache. Query this counter using the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view.

  
## Determining Current Memory Allocation  
 The following query returns information about currently allocated memory.  
  
```  
SELECT  
(physical_memory_in_use_kb/1024) AS Memory_used_by_Sqlserver_MB,  
(locked_page_allocations_kb/1024) AS Locked_pages_used_by_Sqlserver_MB,  
(total_virtual_address_space_kb/1024) AS Total_VAS_in_MB,  
process_physical_memory_low,  
process_virtual_memory_low  
FROM sys.dm_os_process_memory;  
```  

##See Also
- [sys.dm_os_sys_memory (Transact-SQL)](../system-dynamic-management-views/sys-dm-os-sys-memory-transact-sql.md)
- [sys.dm_os_process_memory (Transact-SQL)](../system-dynamic-management-views/sys-dm-os-process-memory-transact-sql.md)
- [sys.dm_os_performance_counters (Transact-SQL)](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md)
- [SQL Server, Memory Manager Object](sql-server-memory-manager-object.md)
- [SQL Server, Buffer Manager Object](sql-server-buffer-manager-object.md)   