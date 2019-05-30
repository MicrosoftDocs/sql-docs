---
title: "Memory Management Architecture Guide | Microsoft Docs"
ms.custom: ""
ms.date: 01/09/2019
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "guide, memory management architecture"
  - "memory management architecture guide"
ms.assetid: 7b0d0988-a3d8-4c25-a276-c1bdba80d6d5
author: "rothja"
ms.author: "jroth"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Memory Management Architecture Guide

[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

## Windows Virtual Memory Manager  
The committed regions of address space are mapped to the available physical memory by the Windows Virtual Memory Manager (VMM).  
  
For more information on the amount of physical memory supported by different operating systems, see the Windows documentation on [Memory Limits for Windows Releases](/windows/desktop/Memory/memory-limits-for-windows-releases).  
  
Virtual memory systems allow the over-commitment of physical memory, so that the ratio of virtual-to-physical memory can exceed 1:1. As a result, larger programs can run on computers with a variety of physical memory configurations. However, using significantly more virtual memory than the combined average working sets of all the processes can cause poor performance. 

## SQL Server Memory Architecture

[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] dynamically acquires and frees memory as required. Typically, an administrator does not have to specify how much memory should be allocated to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], although the option still exists and is required in some environments.

One of the primary design goals of all database software is to minimize disk I/O because disk reads and writes are among the most resource-intensive operations. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] builds a buffer pool in memory to hold pages read from the database. Much of the code in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is dedicated to minimizing the number of physical reads and writes between the disk and the buffer pool. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] tries to reach a balance between two goals:

* Keep the buffer pool from becoming so big that the entire system is low on memory.
* Minimize physical I/O to the database files by maximizing the size of the buffer pool.

> [!NOTE]
> In a heavily loaded system, some large queries that require a large amount of memory to run cannot get the minimum amount of requested memory and receive a time-out error while waiting for memory resources. To resolve this, increase the [query wait Option](../database-engine/configure-windows/configure-the-query-wait-server-configuration-option.md). For a parallel query, consider reducing the [max degree of parallelism Option](../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md).
 
> [!NOTE]
> In a heavily loaded system under memory pressure, queries with merge join, sort and bitmap in the query plan can drop the bitmap when the queries do not get the minimum required memory for the bitmap. This can affect the query performance and if the sorting process can not fit in memory, it can increase the usage of worktables in tempdb database, causing tempdb to grow. To resolve this problem add physical memory or tune the queries to use a different and faster query plan.
 
### Providing the maximum amount of memory to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]

By using AWE and the Locked Pages in Memory privilege, you can provide the following amounts of memory to the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Database Engine. 

> [!NOTE]
> The following table includes a column for 32-bit versions, which are no longer available.

| |32-bit <sup>1</sup> |64-bit|
|-------|-------|-------| 
|Conventional memory |All [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] editions. Up to process virtual address space limit: <br>- 2 GB<br>- 3 GB with /3gb boot parameter <sup>2</sup> <br>- 4 GB on WOW64 <sup>3</sup> |All [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] editions. Up to process virtual address space limit: <br>- 7 TB with IA64 architecture (IA64 not supported in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] and above)<br>- Operating system maximum with x64 architecture <sup>4</sup>
|AWE mechanism (Allows [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to go beyond the process virtual address space limit on 32-bit platform.) |[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Standard, Enterprise, and Developer editions: Buffer pool is capable of accessing up to 64 GB of memory.|Not applicable <sup>5</sup> |
|Lock pages in memory operating system (OS) privilege (allows locking physical memory, preventing OS paging of the locked memory.) <sup>6</sup> |[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Standard, Enterprise, and Developer editions: Required for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] process to use AWE mechanism. Memory allocated through AWE mechanism cannot be paged out. <br> Granting this privilege without enabling AWE has no effect on the server. | Only used when necessary, namely if there are signs that sqlservr process is being paged out. In this case, error 17890 will be reported in the Errorlog, resembling the following example: `A significant part of sql server process memory has been paged out. This may result in a performance degradation. Duration: #### seconds. Working set (KB): ####, committed (KB): ####, memory utilization: ##%.`|

<sup>1</sup> 32-bit versions are not available starting with [!INCLUDE[ssSQL14](../includes/sssql14-md.md)].  
<sup>2</sup> /3gb is an operating system boot parameter. For more information, visit the MSDN Library.  
<sup>3</sup> WOW64 (Windows on Windows 64) is a mode in which 32-bit [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] runs on a 64-bit operating system.  
<sup>4</sup> [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Standard Edition supports up to 128 GB. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Enterprise Edition supports the operating system maximum.  
<sup>5</sup> Note that the sp_configure awe enabled option was present on 64-bit [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], but it is ignored.    
<sup>6</sup> If lock pages in memory privilege (LPIM) is granted (either on 32-bit for AWE support or on 64-bit by itself), we recommend also setting max server memory. For more information on LPIM, refer to [Server Memory Server Configuration Options](../database-engine/configure-windows/server-memory-server-configuration-options.md#lock-pages-in-memory-lpim)

> [!NOTE]
> Older versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] could run on a 32-bit operating system. Accessing more than 4 gigabytes (GB) of memory on a 32-bit operating system required Address Windowing Extensions (AWE) to manage the memory. This is not necessary when [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is running on 64-bit operation systems. For more information about AWE, see [Process Address Space](https://msdn.microsoft.com/library/ms189334.aspx) and [Managing Memory for Large Databases](https://msdn.microsoft.com/library/ms191481.aspx) in the [!INCLUDE[ssKatmai](../includes/ssKatmai-md.md)] documentation.   

<a name="changes-to-memory-management-starting-2012-11x-gm"></a>

## Changes to Memory Management starting with [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]

In earlier versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] ( [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)], [!INCLUDE[ssKatmai](../includes/ssKatmai-md.md)] and [!INCLUDE[ssKilimanjaro](../includes/ssKilimanjaro-md.md)]), memory allocation was done using five different mechanisms:
-  **Single-page Allocator (SPA)**, including only memory allocations that were less than, or equal to 8-KB in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] process. The *max server memory (MB)* and *min server memory (MB)* configuration options determined the limits of physical memory that the SPA consumed. THe buffer pool was simultaneously the mechanism for SPA, and the largest consumer of single-page allocations.
-  **Multi-Page Allocator (MPA)**, for memory allocations that request more than 8-KB.
-  **CLR Allocator**, including the SQL CLR heaps and its global allocations that are created during CLR initialization.
-  Memory allocations for **[thread stacks](../relational-databases/memory-management-architecture-guide.md#stacksizes)** in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] process.
-  **Direct Windows allocations (DWA)**, for memory allocation requests made directly to Windows. These include Windows heap usage and direct virtual allocations made by modules that are loaded into the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] process. Examples of such memory allocation requests include allocations from extended stored procedure DLLs, objects that are created by using Automation procedures (sp_OA calls), and allocations from linked server providers.

Starting with [!INCLUDE[ssSQL11](../includes/sssql11-md.md)],  Single-lage allocations, Multi-Page allocations and CLR allocations are all consolidated into a **"Any size" Page Allocator**, and it's included in memory limits that are controlled by *max server memory (MB)* and *min server memory (MB)* configuration options. This change provided a more accurate sizing ability for all memory requirements that go through the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] memory manager. 

> [!IMPORTANT]
> Carefully review your current *max server memory (MB)* and *min server memory (MB)* configurations after you upgrade to [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)]. This is because starting in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], such configurations now include and account for more memory allocations compared to earlier versions. 
> These changes apply to both 32-bit and 64-bit versions of [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] and [!INCLUDE[ssSQL14](../includes/sssql14-md.md)], and 64-bit versions of [!INCLUDE[ssSQL15](../includes/sssql15-md.md)] through [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)].

The following table indicates whether a specific type of memory allocation is controlled by the *max server memory (MB)* and *min server memory (MB)* configuration options:

|Type of memory allocation| [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)], [!INCLUDE[ssKatmai](../includes/ssKatmai-md.md)] and [!INCLUDE[ssKilimanjaro](../includes/ssKilimanjaro-md.md)]| Starting with [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]|
|-------|-------|-------|
|Single-page allocations|Yes|Yes, consolidated into "any size" page allocations|
|Multi-page allocations|No|Yes, consolidated into "any size" page allocations|
|CLR allocations|No|Yes|
|Thread stacks memory|No|No|
|Direct allocations from Windows|No|No|

Starting with [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] might allocate more memory than the value specified in the max server memory setting. This behavior may occur when the **_Total Server Memory (KB)_** value has already reached the **_Target Server Memory (KB)_** setting (as specified by max server memory). If there is insufficient contiguous free memory to meet the demand of multi-page memory requests (more than 8 KB) because of memory fragmentation, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] can perform over-commitment instead of rejecting the memory request. 

As soon as this allocation is performed, the *Resource Monitor* background task starts to signal all memory consumers to release the allocated memory, and tries to bring the *Total Server Memory (KB)* value below the *Target Server Memory (KB)* specification. Therefore, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] memory usage could briefly exceed the max server memory setting. In this situation, the *Total Server Memory (KB)* performance counter reading will exceed the max server memory and *Target Server Memory (KB)* settings.

This behavior is typically observed during the following operations: 
-  Large Columnstore index queries.
-  Columnstore index (re)builds, which use large volumes of memory to perform Hash and Sort operations.
-  Backup operations that require large memory buffers.
-  Tracing operations that have to store large input parameters.

<a name="#changes-to-memory-management-starting-with-includesssql11includessssql11-mdmd"></a>
## Changes to "memory_to_reserve" starting with [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]
In earlier versions of SQL Server ( [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)], [!INCLUDE[ssKatmai](../includes/ssKatmai-md.md)] and [!INCLUDE[ssKilimanjaro](../includes/ssKilimanjaro-md.md)]), the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] memory manager set aside a part of the process virtual address space (VAS) for use by the **Multi-Page Allocator (MPA)**, **CLR Allocator**, memory allocations for **thread stacks** in the SQL Server process, and **Direct Windows allocations (DWA)**. This part of the virtual address space is also known as "Mem-To-Leave" or "non-Buffer Pool" region.

The virtual address space that is reserved for these allocations is determined by the _**memory\_to\_reserve**_ configuration option. The default value that [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] uses is 256 MB. To override the default value, use the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] *-g* startup parameter. Refer to the documentation page on [Database Engine Service Startup Options](../database-engine/configure-windows/database-engine-service-startup-options.md) for information on the *-g* startup parameter.

Because starting with [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], the new "any size" page allocator also handles allocations greater than 8 KB, the *memory_to_reserve* value does not include the multi-page allocations. Except for this change, everything else remains the same with this configuration option.

The following table indicates whether a specific type of memory allocation falls into the *memory_to_reserve* region of the virtual address space for the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] process:

|Type of memory allocation| [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)], [!INCLUDE[ssKatmai](../includes/ssKatmai-md.md)] and [!INCLUDE[ssKilimanjaro](../includes/ssKilimanjaro-md.md)]| Starting with [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]|
|-------|-------|-------|
|Single-page allocations|No|No, consolidated into "any size" page allocations|
|Multi-page allocations|Yes|No, consolidated into "any size" page allocations|
|CLR allocations|Yes|Yes|
|Thread stacks memory|Yes|Yes|
|Direct allocations from Windows|Yes|Yes|

## <a name="dynamic-memory-management"></a> Dynamic Memory Management
The default memory management behavior of the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] is to acquire as much memory as it needs without creating a memory shortage on the system. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] does this by using the Memory Notification APIs in Microsoft Windows.

When [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is using memory dynamically, it queries the system periodically to determine the amount of free memory. Maintaining this free memory prevents the operating system (OS) from paging. If less memory is free, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] releases memory to the OS. If more memory is free, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] may allocate more memory. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] adds memory only when its workload requires more memory; a server at rest does not increase the size of its virtual address space.  
  
**[Max server memory](../database-engine/configure-windows/server-memory-server-configuration-options.md)** controls the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] memory allocation, compile memory, all caches (including the buffer pool), [query execution memory grants](#effects-of-min-memory-per-query), [lock manager memory](#memory-used-by-sql-server-objects-specifications), and CLR<sup>1</sup> memory (essentially any memory clerk found in **[sys.dm_os_memory_clerks](../relational-databases/system-dynamic-management-views/sys-dm-os-memory-clerks-transact-sql.md)**). 

<sup>1</sup> CLR memory is managed under max_server_memory allocations starting with [!INCLUDE[ssSQL11](../includes/sssql11-md.md)].

The following query returns information about currently allocated memory:  
  
```sql  
SELECT 
  physical_memory_in_use_kb/1024 AS sql_physical_memory_in_use_MB, 
	large_page_allocations_kb/1024 AS sql_large_page_allocations_MB, 
	locked_page_allocations_kb/1024 AS sql_locked_page_allocations_MB,
	virtual_address_space_reserved_kb/1024 AS sql_VAS_reserved_MB, 
	virtual_address_space_committed_kb/1024 AS sql_VAS_committed_MB, 
	virtual_address_space_available_kb/1024 AS sql_VAS_available_MB,
	page_fault_count AS sql_page_fault_count,
	memory_utilization_percentage AS sql_memory_utilization_percentage, 
	process_physical_memory_low AS sql_process_physical_memory_low, 
	process_virtual_memory_low AS sql_process_virtual_memory_low
FROM sys.dm_os_process_memory;  
```  
 
<a name="stacksizes"></a> Memory for thread stacks<sup>1</sup>, CLR<sup>2</sup>, extended procedure .dll files, the OLE DB providers referenced by distributed queries, automation objects referenced in [!INCLUDE[tsql](../includes/tsql-md.md)] statements, and any memory allocated by a non [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] DLL are **not** controlled by max server memory.

<sup>1</sup> Refer to the documentation page on how to [Configure the max worker threads Server Configuration Option](../database-engine/configure-windows/configure-the-max-worker-threads-server-configuration-option.md), for information on the calculated default worker threads for a given number of affinitized CPUs in the current host. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] stack sizes are as follows:

|SQL Server Architecture|OS Architecture|Stack Size|  
|--------------------|----------------------|----------------------|
|x86 (32-bit)|x86 (32-bit)|512 KB|
|x86 (32-bit)|x64 (64-bit)|768 KB| 
|x64 (64-bit)|x64 (64-bit)|2048 KB|
|IA64 (Itanium)|IA64 (Itanium)|4096 KB|

<sup>2</sup> CLR memory is managed under max_server_memory allocations starting with [!INCLUDE[ssSQL11](../includes/sssql11-md.md)].

[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] uses the memory notification API **QueryMemoryResourceNotification** to determine when the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Memory Manager may allocate memory and release memory.  

When [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] starts, it computes the size of virtual address space for the buffer pool based on a number of parameters such as amount of physical memory on the system, number of server threads and various startup parameters. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] reserves the computed amount of its process virtual address space for the buffer pool, but it acquires (commits) only the required amount of physical memory for the current load.

The instance then continues to acquire memory as needed to support the workload. As more users connect and run queries, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] acquires the additional physical memory on demand. A [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance continues to acquire physical memory until it either reaches its max server memory allocation target or the OS indicates there is no longer an excess of free memory; it frees memory when it has more than the min server memory setting, and the OS indicates that there is a shortage of free memory. 

As other applications are started on a computer running an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], they consume memory and the amount of free physical memory drops below the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] target. The instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] adjusts its memory consumption. If another application is stopped and more memory becomes available, the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] increases the size of its memory allocation. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] can free and acquire several megabytes of memory each second, allowing it to quickly adjust to memory allocation changes.

## Effects of min and max server memory
The *min server memory* and *max server memory* configuration options establish upper and lower limits to the amount of memory used by the buffer pool and other caches of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Database Engine. The buffer pool does not immediately acquire the amount of memory specified in min server memory. The buffer pool starts with only the memory required to initialize. As the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] workload increases, it keeps acquiring the memory required to support the workload. The buffer pool does not free any of the acquired memory until it reaches the amount specified in min server memory. Once min server memory is reached, the buffer pool then uses the standard algorithm to acquire and free memory as needed. The only difference is that the buffer pool never drops its memory allocation below the level specified in min server memory, and never acquires more memory than the level specified in max server memory.

> [!NOTE]
> [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] as a process acquires more memory than specified by max server memory option. Both internal and external components can allocate memory outside of the buffer pool, which consumes additional memory, but the memory allocated to the buffer pool usually still represents the largest portion of memory consumed by [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].

The amount of memory acquired by the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] is entirely dependent on the workload placed on the instance. A [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance that is not processing many requests may never reach min server memory.

If the same value is specified for both min server memory and max server memory, then once the memory allocated to the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] reaches that value, the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] stops dynamically freeing and acquiring memory for the buffer pool.

If an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is running on a computer where other applications are frequently stopped or started, the allocation and deallocation of memory by the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] may slow the startup times of other applications. Also, if [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is one of several server applications running on a single computer, the system administrators may need to control the amount of memory allocated to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. In these cases, you can use the min server memory and max server memory options to control how much memory [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] can use. The **min server memory** and **max server memory** options are specified in megabytes. For more information, see [Server Memory Configuration Options](../database-engine/configure-windows/server-memory-server-configuration-options.md).

## Memory used by SQL Server objects specifications
The following list describes the approximate amount of memory used by different objects in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. The amounts listed are estimates and can vary depending on the environment and how objects are created:

* Lock (as maintained by the Lock Manager): 64 bytes + 32 bytes per owner   
* User connection: Approximately (3 \* network_packet_size + 94 kb)    

The **network packet size** is the size of the tabular data scheme (TDS) packets that are used to communicate between applications and the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Database Engine. The default packet size is 4 KB, and is controlled by the network packet size configuration option.

When multiple active result sets (MARS) are enabled, the user connection is approximately (3 + 3 \* num_logical_connections) \* network_packet_size + 94 KB

## Effects of min memory per query
The *min memory per query* configuration option establishes the minimum amount of memory (in kilobytes) that will be allocated for the execution of a query. This is also known as the minimum memory grant. All queries must wait until the minimum memory requested can be secured, before execution can start, or until the value specified in the query wait server configuration option is exceeded. The wait type that is accumulated in this scenario is RESOURCE_SEMAPHORE.

> [!IMPORTANT]
> Do not set the min memory per query server configuration option too high, especially on very busy systems, because doing so could lead to:         
> - Increased competition for memory resources.         
> - Decreased concurrency by increasing the amount of memory for every single query, even if the required memory at runtime is lower that this configuration.    
>    
> For recommendations on using this configuration, see [Configure the min memory per query Server Configuration Option](../database-engine/configure-windows/configure-the-min-memory-per-query-server-configuration-option.md#Recommendations).

### <a name="memory-grant-considerations"></a>Memory grant considerations
For **row mode execution**, the initial memory grant cannot be exceeded under any condition. If more memory than the initial grant is needed to execute **hash** or **sort** operations, then these will spill to disk. A hash operation that spills is supported by a Workfile in TempDB, while a sort operation that spills is supported by a [Worktable](../relational-databases/query-processing-architecture-guide.md#worktables).   

A spill that occurs during a Sort operation is known as a [Sort Warning](../relational-databases/event-classes/sort-warnings-event-class.md). Sort warnings indicate that sort operations do not fit into memory. This does not include sort operations involving the creation of indexes, only sort operations within a query (such as an `ORDER BY` clause used in a `SELECT` statement).

A spill that occurs during a hash operation is known as a [Hash Warning](../relational-databases/event-classes/hash-warning-event-class.md). These occur when a hash recursion or cessation of hashing (hash bailout) has occurred during a hashing operation.
-  Hash recursion occurs when the build input does not fit into available memory, resulting in the split of input into multiple partitions that are processed separately. If any of these partitions still do not fit into available memory, it is split into sub-partitions, which are also processed separately. This splitting process continues until each partition fits into available memory or until the maximum recursion level is reached.
-  Hash bailout occurs when a hashing operation reaches its maximum recursion level and shifts to an alternate plan to process the remaining partitioned data. These events can cause reduced performance in your server.

For **batch mode execution**, the initial memory grant can dynamically increase up to a certain internal threshold by default. This dynamic memory grant mechanism is designed to allow memory resident execution of **hash** or **sort** operations running in batch mode. If these operations still do not fit into memory, then these will spill to disk.

For more information on execution modes, see the [Query Processing Architecture Guide](../relational-databases/query-processing-architecture-guide.md#execution-modes).

## Buffer management
The primary purpose of a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database is to store and retrieve data, so intensive disk I/O is a core characteristic of the Database Engine. And because disk I/O operations can consume many resources and take a relatively long time to finish, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] focuses on making I/O highly efficient. Buffer management is a key component in achieving this efficiency. The buffer management component consists of two mechanisms: the **buffer manager** to access and update database pages, and the **buffer cache** (also called the **buffer pool**), to reduce database file I/O. 

### How buffer management works
A buffer is an 8 KB page in memory, the same size as a data or index page. Thus, the buffer cache is divided into 8 KB pages. The buffer manager manages the functions for reading data or index pages from the database disk files into the buffer cache and writing modified pages back to disk. A page remains in the buffer cache until the buffer manager needs the buffer area to read in more data. Data is written back to disk only if it is modified. Data in the buffer cache can be modified multiple times before being written back to disk. For more information, see [Reading Pages](../relational-databases/reading-pages.md) and [Writing Pages](../relational-databases/writing-pages.md).

When [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] starts, it computes the size of virtual address space for the buffer cache based on a number of parameters such as the amount of physical memory on the system, the configured number of maximum server threads, and various startup parameters. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] reserves this computed amount of its process virtual address space (called the memory target) for the buffer cache, but it acquires (commits) only the required amount of physical memory for the current load. You can query the **bpool_commit_target** and **bpool_committed columns** in the [sys.dm_os_sys_info](../relational-databases/system-dynamic-management-views/sys-dm-os-sys-info-transact-sql.md) catalog view to return the number of pages reserved as the memory target and the number of pages currently committed in the buffer cache, respectively.

The interval between [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] startup and when the buffer cache obtains its memory target is called ramp-up. During this time, read requests fill the buffers as needed. For example, a single 8 KB page read request fills a single buffer page. This means the ramp-up depends on the number and type of client requests. Ramp-up is expedited by transforming single page read requests into aligned eight page requests (making up one extent). This allows the ramp-up to finish much faster, especially on machines with a lot of memory. For more information about pages and extents, refer to [Pages and Extents Architecture Guide](../relational-databases/pages-and-extents-architecture-guide.md#pages-and-extents).

Because the buffer manager uses most of the memory in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] process, it cooperates with the memory manager to allow other components to use its buffers. The buffer manager interacts primarily with the following components:

* Resource manager to control overall memory usage and, in 32-bit platforms, to control address space usage.  
* Database manager and the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Operating System (SQLOS) for low-level file I/O operations.  
* Log manager for write-ahead logging.  

### Supported Features
The buffer manager supports the following features:

* The buffer manager is **non-uniform memory access (NUMA)** aware. Buffer cache pages are distributed across hardware NUMA nodes, which allows a thread to access a buffer page that is allocated on the local NUMA node rather than from foreign memory. 
* The buffer manager supports **Hot Add Memory**, which allows users to add physical memory without restarting the server. 
* The buffer manager supports **large pages** on 64-bit platforms. The page size is specific to the version of Windows.

  > [!NOTE]
  > Prior to [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], enabling large pages in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] requires [trace flag 834](../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).  

* The buffer manager provides additional diagnostics that are exposed through dynamic management views. You can use these views to monitor a variety of operating system resources that are specific to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For example, you can use the [sys.dm_os_buffer_descriptors](../relational-databases/system-dynamic-management-views/sys-dm-os-buffer-descriptors-transact-sql.md) view to monitor the pages in the buffer cache.   

### Disk I/O
The buffer manager only performs reads and writes to the database. Other file and database operations such as open, close, extend, and shrink are performed by the database manager and file manager components. 

Disk I/O operations by the buffer manager have the following characteristics:

* All I/Os are performed asynchronously, which allows the calling thread to continue processing while the I/O operation takes place in the background.
* All I/Os are issued in the calling threads unless the affinity I/O  option is in use. The affinity I/O mask option binds [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] disk I/O to a specified subset of CPUs. In high-end [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] online transactional processing (OLTP) environments, this extension can enhance the performance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] threads issuing I/Os.
* Multiple page I/Os are accomplished with scatter-gather I/O, which allows data to be transferred into or out of noncontiguous areas of memory. This means that [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] can quickly fill or flush the buffer cache while avoiding multiple physical I/O requests. 

#### Long I/O requests  
The buffer manager reports on any I/O request that has been outstanding for at least 15 seconds. This helps the system administrator distinguish between [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] problems and I/O subsystem problems. Error message 833 is reported and appears in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] error log as follows:

`SQL Server has encountered ## occurrence(s) of I/O requests taking longer than 15 seconds to complete on file [##] in database [##] (#). The OS file handle is 0x00000. The offset of the latest long I/O is: 0x00000.` 

A long I/O may be either a read or a write; it is not currently indicated in the message. Long-I/O messages are warnings, not errors. They do not indicate problems with [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] but with the underlying I/O system. The messages are reported to help the system administrator find the cause of poor [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] response times more quickly, and to distinguish problems that are outside the control of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. As such, they do not require any action, but the system administrator should investigate why the I/O request took so long, and whether the time is justifiable.

#### Causes of Long-I/O Requests  
A long-I/O message may indicate that an I/O is permanently blocked and will never complete (known as lost I/O), or merely that it just has not completed yet. It is not possible to tell from the message which scenario is the case, although a lost I/O will often lead to a latch timeout.

Long I/Os often indicate a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] workload that is too intense for the disk subsystem. An inadequate disk subsystem may be indicated when:

* Multiple long I/O messages appear in the error log during a heavy [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] workload.
* Perfmon counters show long disk latencies, long disk queues, or no disk idle time.  

Long I/Os may also be caused by a component in the I/O path (for example, a driver, controller, or firmware) continually postponing servicing an old I/O request in favor of servicing newer requests that are closer to the current position of the disk head. The common technique of processing requests in priority based upon which ones are closest to the current position of the read/write head is known as "elevator seeking." This may be difficult to corroborate with the Windows System Monitor (PERFMON.EXE) tool because most I/Os are being serviced promptly. Long I/O requests can be aggravated by workloads that perform large amounts of sequential I/O, such as backup and restore, table scans, sorting, creating indexes, bulk loads, and zeroing out files.

Isolated long I/Os that do not appear related to any of the previous conditions may be caused by a hardware or driver problem. The system event log may contain a related event that helps to diagnose the problem.

### Memory pressure detection
Memory pressure is a condition resulting from memory shortage, and can result in:
- Extra I/Os (such as very active lazy writer background thread)
- Higher recompile ratio
- Longer running queries (if memory grant waits exist)
- Extra CPU cycles

This situation can be trigerred by external or internal causes. External causes include:
- Available physical memory (RAM) is low. This causes the system to trim working sets of currently running processes, which may result in overall slowdown. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] may reduce the commit target of the buffer pool and start trimming internal caches more often. 
- Overall available system memory (which includes the system page file) is low. This may cause the system to fail memory allocations, as it is unable to page out currently allocated memory.
Internal causes include:
- Responding to the external memory pressure, when the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] sets lower memory usage caps.
- Memory settings were manually lowered by reducing the *max server memory* configuration. 
- Changes in memory distribution of internal components between the several caches.

The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] implements a framework dedicated to detecting and handling memory pressure, as part of its dynamic memory management. This framework includes the backgroud task called **Resource Monitor**. The Resource Monitor task monitors the state of external and internal memory indicators. Once one of these indicators changes status, it calculates the corresponding notification and it broadcasts it. These notifications are internal messages from each of the engine components, and stored in ring buffers. 

Two ring buffers hold information relevant to dynamic memory management: 
- The Resource Monitor ring buffer, which tracks Resource Monitor activity like was memory pressure signaled or not. This ring buffer has status information depending on the current condition of *RESOURCE_MEMPHYSICAL_HIGH*, *RESOURCE_MEMPHYSICAL_LOW*, *RESOURCE_MEMPHYSICAL_STEADY*, or *RESOURCE_MEMVIRTUAL_LOW*.
- The Memory Broker ring buffer, which contains records of memory notifications for each Resource Governor resource pool. As internal memory pressure is detected, low memory notification is turned on for components that allocate memory, to trigger actions meant to balance the memory between caches. 

Memory brokers monitor the demand consumption of memory by each component and then based on the information collected, it calculates and optimal value of memory for each of these components. There is a set of brokers for each Resource Governor resource pool. This information is then broadcast to each of the components, which grow or shrink their usage as required.
For more information about memory brokers, see [sys.dm_os_memory_brokers](../relational-databases/system-dynamic-management-views/sys-dm-os-memory-brokers-transact-sql.md). 

### Error Detection  
Database pages can use one of two optional mechanisms that help insure the integrity of the page from the time it is written to disk until it is read again: torn page protection and checksum protection. These mechanisms allow an independent method of verifying the correctness of not only the data storage, but hardware components such as controllers, drivers, cables, and even the operating system. The protection is added to the page just before writing it to disk, and verified after it is read from disk.

[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] will retry any read that fails with a checksum, torn page, or other I/O error four times. If the read is successful in any one of the retry attempts, a message will be written to the error log and the command that triggered the read will continue. If the retry attempts fail, the command will fail with error message 824. 

The kind of page protection used is an attribute of the database containing the page. Checksum protection is the default protection for databases created in [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)] and later. The page protection mechanism is specified at database creation time, and may be altered by using ALTER DATABASE SET. You can determine the current page protection setting by querying the *page_verify_option* column in the [sys.databases](../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view or the *IsTornPageDetectionEnabled* property of the [DATABASEPROPERTYEX](../t-sql/functions/databasepropertyex-transact-sql.md) function. 

> [!NOTE]
> If the page protection setting is changed, the new setting does not immediately affect the entire database. Instead, pages adopt the current protection level of the database whenever they are written next. This means that the database may be composed of pages with different kinds of protection. 

#### Torn Page Protection  
Torn page protection, introduced in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] 2000, is primarily a way of detecting page corruptions due to power failures. For example, an unexpected power failure may leave only part of a page written to disk. When torn page protection is used, a specific 2-bit signature pattern for each 512-byte sector in the 8-kilobyte (KB) database page and stored in the database page header when the page is written to disk. When the page is read from disk, the torn bits stored in the page header are compared to the actual page sector information. The signature pattern alternates between binary 01 and 10 with every write, so it is always possible to tell when only a portion of the sectors made it to disk: if a bit is in the wrong state when the page is later read, the page was written incorrectly and a torn page is detected. Torn page detection uses minimal resources; however, it does not detect all errors caused by disk hardware failures. For information on setting torn page detection, see [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-set-options.md#page_verify).

#### Checksum Protection  
Checksum protection, introduced in [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)], provides stronger data integrity checking. A checksum is calculated for the data in each page that is written, and stored in the page header. Whenever a page with a stored checksum is read from disk, the database engine recalculates the checksum for the data in the page and raises error 824 if the new checksum is different from the stored checksum. Checksum protection can catch more errors than torn page protection because it is affected by every byte of the page, however, it is moderately resource intensive. When checksum is enabled, errors caused by power failures and flawed hardware or firmware can be detected any time the buffer manager reads a page from disk. For information on setting checksum, see [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-set-options.md#page_verify).

> [!IMPORTANT]
> When a user or system database is upgraded to [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)] or a later version, the [PAGE_VERIFY](../t-sql/statements/alter-database-transact-sql-set-options.md#page_verify) value (NONE or TORN_PAGE_DETECTION) is retained. We recommend that you use CHECKSUM.
> TORN_PAGE_DETECTION may use fewer resources but provides a minimal subset of the CHECKSUM protection.

## Understanding Non-uniform Memory Access
[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is non-uniform memory access (NUMA) aware, and performs well on NUMA hardware without special configuration. As clock speed and the number of processors increase, it becomes increasingly difficult to reduce the memory latency required to use this additional processing power. To circumvent this, hardware vendors provide large L3 caches, but this is only a limited solution. NUMA architecture provides a scalable solution to this problem. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] has been designed to take advantage of NUMA-based computers without requiring any application changes. For more information, see [How to: Configure SQL Server to Use Soft-NUMA](../database-engine/configure-windows/soft-numa-sql-server.md).

## See Also
[Server Memory Server Configuration Options](../database-engine/configure-windows/server-memory-server-configuration-options.md)   
[Reading Pages](../relational-databases/reading-pages.md)   
[Writing Pages](../relational-databases/writing-pages.md)   
[How to: Configure SQL Server to Use Soft-NUMA](../database-engine/configure-windows/soft-numa-sql-server.md)   
[Requirements for Using Memory-Optimized Tables](../relational-databases/in-memory-oltp/requirements-for-using-memory-optimized-tables.md)   
[Resolve Out Of Memory Issues Using Memory-Optimized Tables](../relational-databases/in-memory-oltp/resolve-out-of-memory-issues.md)
