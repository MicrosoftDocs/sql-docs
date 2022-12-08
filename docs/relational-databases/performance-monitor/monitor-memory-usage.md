---
title: "Monitor Memory Usage"
description: "Monitor a SQL Server instance to confirm that memory usage is within typical ranges. Use the Memory: Available Bytes and Memory: Pages/sec counters."
ms.custom: ""
ms.date: "07/13/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
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
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Monitor memory usage
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Monitor an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] periodically to confirm that memory usage is within typical ranges. 

## Configuring [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] max memory

By default, a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance may over time consume most of the available Windows operating system memory in the server. Once the memory is acquired, it will not be released unless memory pressure is detected. This is by design and does not indicate a memory leak in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process. Use [the **max server memory** option](../../database-engine/configure-windows/server-memory-server-configuration-options.md) to limit the amount of memory that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is allowed to acquire for most of its uses. For more information, see the [Memory Management Architecture Guide](../../relational-databases/memory-management-architecture-guide.md#changes-to-memory-management-starting-with-).

In SQL Server on Linux, [set the memory limit](../../linux/sql-server-linux-performance-best-practices.md#advanced-configuration) with the mssql-conf tool and the [memory.memorylimitmb setting](../../linux/sql-server-linux-configure-mssql-conf.md#memorylimit).  

## Monitor operating system memory   
 To monitor for a low-memory condition, use the following Windows server counters. Many operating system memory counters can be queried via the dynamic management views [sys.dm_os_process_memory](../system-dynamic-management-views/sys-dm-os-process-memory-transact-sql.md) and [sys.dm_os_sys_memory](../system-dynamic-management-views/sys-dm-os-sys-memory-transact-sql.md).

-   **Memory: Available Bytes**  
This counter indicates how many bytes of memory are currently available for use by processes. Low values for the **Available Bytes** counter can indicate an overall shortage of operating system memory. This value can be queried via T-SQL using [sys.dm_os_sys_memory](../system-dynamic-management-views/sys-dm-os-sys-memory-transact-sql.md).available_physical_memory_kb.

-   **Memory: Pages/sec**  
This counter indicates the number of pages that either were retrieved from disk due to hard page faults or written to disk to free space in the working set due to page faults. A high rate for the **Pages/sec** counter could indicate excessive paging.  

-   **Memory: Page Faults/sec**
This counter indicates the rate of Page Faults for all processes including system processes. A low but non-zero rate of paging to disk (and hence page faults) is typical, even if the computer has plenty of available memory. The Microsoft Windows Virtual Memory Manager (VMM) takes pages from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and other processes as it trims the working-set sizes of those processes. This VMM activity tends to cause page faults.  

-   **Process: Page Faults/sec**
This counter indicates the rate of Page Faults for a given user process. Monitor **Process: Page Faults/sec** to determine if disk activity is caused by paging by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. To determine whether [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or another process is the cause of excessive paging, monitor the **Process: Page Faults/sec** counter for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process instance.  
  
 For more information about resolving excessive paging, see the operating system documentation.  
  
## Isolating memory used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 

 To monitor [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory usage, use the following [SQL Server object counters](use-sql-server-objects.md). Many SQL Server object counters can be queried via the dynamic management views [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) or [sys.dm_os_process_memory](../system-dynamic-management-views/sys-dm-os-process-memory-transact-sql.md).

 By default, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] manages its memory requirements dynamically, based on available system resources. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] needs more memory, it queries the operating system to determine whether free physical memory is available and uses the available memory. If there is low free memory for the OS, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will release memory back to the operating system until the low memory condition is alleviated, or until [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] reaches the **min server memory** limit. However, you can override the option to dynamically use memory by using the **min server memory**, and **max server memory** server configuration options. For more information, see [Server Memory Options](../../database-engine/configure-windows/server-memory-server-configuration-options.md).  
  
 To monitor the amount of memory that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses, examine the following performance counters:  
  
-   **SQL Server: Memory Manager: Total Server Memory (KB)**  
This counter indicates the amount of the operating system's memory the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory manager currently has committed to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This number is expected to grow as required by actual activity, and will grow following SQL Server startup. Query this counter using the [sys.dm_os_sys_info](../system-dynamic-management-views/sys-dm-os-sys-info-transact-sql.md) dynamic management view, observing the **committed_kb** column.

-   **SQL Server: Memory Manager: Target Server Memory (KB)**  
This counter indicates an ideal amount of memory [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] could consume, based on recent workload. Compare to **Total Server Memory** after a period of typical operation to determine whether [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has a desired amount of memory allocated. After typical operation, **Total Server Memory** and **Target Server Memory** should be similar. If **Total Server Memory** is significantly lower than **Target Server Memory**, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance may be experiencing memory pressure. During a period after [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is started, **Total Server Memory** is expected to be lower than **Target Server Memory**, as **Total Server Memory** grows. Query this counter using the [sys.dm_os_sys_info](../system-dynamic-management-views/sys-dm-os-sys-info-transact-sql.md) dynamic management view, observing the **committed_target_kb** column. For more information and best practices configuring memory, see the [Server memory configuration options](../../database-engine/configure-windows/server-memory-server-configuration-options.md#manually).

-   **Process: Working Set**  
This counter indicates the amount of physical memory that is in use by a process currently, according to the operating system. Observe the sqlservr.exe instance of this counter. Query this counter using the [sys.dm_os_process_memory](../system-dynamic-management-views/sys-dm-os-process-memory-transact-sql.md) dynamic management view, observing the **physical_memory_in_use_kb** column.

-   **Process: Private Bytes**  
This counter indicates the amount of memory that a process has requested for its own use to the operating system. Observe the sqlservr.exe instance of this counter. Because this counter includes all memory allocations requested by sqlservr.exe, including those not limited by [the **max server memory** option](../../database-engine/configure-windows/server-memory-server-configuration-options.md), this counter can report values larger than [the **max server memory** option](../../database-engine/configure-windows/server-memory-server-configuration-options.md).

-   **SQL Server: Buffer Manager: Database Pages**  
This counter indicates the number of pages in the buffer pool with database content. Does not include other nonbuffer pool memory within the SQL Server process. Query this counter using the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view.

-   **SQL Server: Buffer Manager: Buffer Cache Hit Ratio**  
This counter is specific to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. A ratio of 90 or higher is desirable. A value greater than 90 indicates that more than 90 percent of all requests for data were satisfied from the data cache in memory without having to read from disk. Find more information on the SQL Server Buffer Manager, see the [SQL Server Buffer Manager Object](sql-server-buffer-manager-object.md). Query this counter using the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view.  
 
-   **SQL Server: Buffer Manager: Page life expectancy**  
This counter measures amount of time in seconds that the oldest page stays in the buffer pool. For systems that use a NUMA architecture, this is the average across the all NUMA nodes. A higher, growing value is best. A sudden dip indicates a significant churn of data in and out of the buffer pool, indicating the workload could not fully benefit from data already in memory. Each NUMA node has its own node of the buffer pool. On servers with more than one NUMA node, view each buffer pool node's page life expectancy using **SQL Server: Buffer Node: Page life expectancy**. Query this counter using the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view.

  
## Examples 

### Determining current memory allocation  
 The following queries return information about currently allocated memory.  
  
```sql  
SELECT
(total_physical_memory_kb/1024) AS Total_OS_Memory_MB,
(available_physical_memory_kb/1024)  AS Available_OS_Memory_MB
FROM sys.dm_os_sys_memory;

SELECT  
(physical_memory_in_use_kb/1024) AS Memory_used_by_Sqlserver_MB,  
(locked_page_allocations_kb/1024) AS Locked_pages_used_by_Sqlserver_MB,  
(total_virtual_address_space_kb/1024) AS Total_VAS_in_MB,
process_physical_memory_low,  
process_virtual_memory_low  
FROM sys.dm_os_process_memory;  
```  

### Determining current SQL Server memory utilization   
 The following query returns information about current SQL Server memory utilization.  

```sql  
SELECT
sqlserver_start_time,
(committed_kb/1024) AS Total_Server_Memory_MB,
(committed_target_kb/1024)  AS Target_Server_Memory_MB
FROM sys.dm_os_sys_info;
```   

### Determining page life expectancy
 The following query uses `sys.dm_os_performance_counters` to observe the SQL Server instance's current **page life expectancy** value at the overall buffer manager level, and at each NUMA node level.

```sql
SELECT
CASE instance_name WHEN '' THEN 'Overall' ELSE instance_name END AS NUMA_Node, cntr_value AS PLE_s
FROM sys.dm_os_performance_counters    
WHERE counter_name = 'Page life expectancy';
```

## See also
- [sys.dm_os_sys_memory (Transact-SQL)](../system-dynamic-management-views/sys-dm-os-sys-memory-transact-sql.md)
- [sys.dm_os_process_memory (Transact-SQL)](../system-dynamic-management-views/sys-dm-os-process-memory-transact-sql.md)
- [sys.dm_os_sys_info (Transact-SQL)](../system-dynamic-management-views/sys-dm-os-sys-info-transact-sql.md)
- [sys.dm_os_performance_counters (Transact-SQL)](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md)
- [SQL Server, Memory Manager Object](sql-server-memory-manager-object.md)
- [SQL Server, Buffer Manager Object](sql-server-buffer-manager-object.md)   
- [Server memory configuration options](../../database-engine/configure-windows/server-memory-server-configuration-options.md)
- [Memory Management Architecture Guide](../../relational-databases/memory-management-architecture-guide.md)
