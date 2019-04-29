---
title: "Monitor Memory Usage | Microsoft Docs"
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
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# Monitor Memory Usage
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Monitor an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] periodically to confirm that memory usage is within typical ranges.  
  
 To monitor for a low-memory condition, use the following object counters:  
  
-   **Memory: Available Bytes**  
  
-   **Memory: Pages/sec**  
  
 The **Available Bytes** counter indicates how many bytes of memory are currently available for use by processes. The **Pages/sec** counter indicates the number of pages that either were retrieved from disk due to hard page faults or written to disk to free space in the working set due to page faults.  
  
 Low values for the **Available Bytes** counter can indicate that there is an overall shortage of memory on the computer or that an application is not releasing memory. A high rate for the **Pages/sec** counter could indicate excessive paging. Monitor the **Memory: Page Faults/sec** counter to make sure that the disk activity is not caused by paging.  
  
 A low rate of paging (and hence page faults) is typical, even if the computer has plenty of available memory. The Microsoft Windows Virtual Memory Manager (VMM) takes pages from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and other processes as it trims the working-set sizes of those processes. This VMM activity tends to cause page faults. To determine whether [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or another process is the cause of excessive paging, monitor the **Process: Page Faults/sec** counter for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process instance.  
  
 For more information about resolving excessive paging, see the Windows operating system documentation.  
  
## Isolating Memory Used by SQL Server  
 By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] changes its memory requirements dynamically, on the basis of available system resources. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] needs more memory, it queries the operating system to determine whether free physical memory is available and uses the available memory. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not need the memory currently allocated to it, it releases the memory to the operating system. However, you can override the option to dynamically use memory by using the **minservermemory**, and **maxservermemory** server configuration options. For more information, see [Server Memory Options](../../database-engine/configure-windows/server-memory-server-configuration-options.md).  
  
 To monitor the amount of memory that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses, examine the following performance counters:  
  
-   **Process: Working Set**  
  
-   **SQL Server: Buffer Manager: Buffer Cache Hit Ratio**  
  
-   **SQL Server: Buffer Manager: Database Pages**  
  
-   **SQL Server: Memory Manager: Total Server Memory (KB)**  
  
 The **WorkingSet** counter shows the amount of memory that is used by a process. If this number is consistently below the amount of memory that is set by the **min server memory** and **max server memory** server options, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is configured to use too much memory.  
  
 The **Buffer Cache Hit Ratio** counter is specific to an application. However, a rate of 90 percent or higher is desirable. Add more memory until the value is consistently greater than 90 percent. A value greater than 90 percent indicates that more than 90 percent of all requests for data were satisfied from the data cache.  
  
 If the **TotalServerMemory (KB)** counter is consistently high compared to the amount of physical memory in the computer, it may indicate that more memory is required.  
  
## Determining Current Memory Allocation  
 The following query returns information about currently allocated memory.  
  
```  
SELECT  
(physical_memory_in_use_kb/1024) AS Memory_usedby_Sqlserver_MB,  
(locked_page_allocations_kb/1024) AS Locked_pages_used_Sqlserver_MB,  
(total_virtual_address_space_kb/1024) AS Total_VAS_in_MB,  
process_physical_memory_low,  
process_virtual_memory_low  
FROM sys.dm_os_process_memory;  
```  
  
  
