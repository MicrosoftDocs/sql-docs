---
title: "Memory Management Architecture Guide"
description: Learn about memory management architecture in SQL Server, including changes to memory management in previous versions.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/25/2022
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords:
  - "guide, memory management architecture"
  - "memory management architecture guide"
  - "PMO"
  - "Partitioned Memory Objects"
  - "cmemthread"
  - "AWE"
  - "SPA, Single Page Allocator"
  - "MPA, Multi Page Allocator"
  - "memory allocation, SQL Server"
  - "memory pressure, SQL Server"
  - "stack size, SQL Server"
  - "buffer manager, SQL Server"
  - "buffer pool, SQL Server"
  - "resource monitor, SQL Server"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Memory management architecture guide

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

## Windows Virtual Memory Manager

The committed regions of address space are mapped to the available physical memory by the Windows Virtual Memory Manager (VMM).

For more information on the amount of physical memory supported by different operating systems, see the Windows documentation on [Memory Limits for Windows Releases](/windows/desktop/Memory/memory-limits-for-windows-releases).

Virtual memory systems allow the over-commitment of physical memory, so that the ratio of virtual-to-physical memory can exceed 1:1. As a result, larger programs can run on computers with various physical memory configurations. However, using significantly more virtual memory than the combined average working sets of all the processes can cause poor performance.

## SQL Server memory architecture

[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] dynamically acquires and frees memory as required. Typically, an administrator doesn't have to specify how much memory should be allocated to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], although the option still exists and is required in some environments.

One of the primary design goals of all database software is to minimize disk I/O because disk reads and writes are among the most resource-intensive operations. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] builds a buffer pool in memory to hold pages read from the database. Much of the code in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is dedicated to minimizing the number of physical reads and writes between the disk and the buffer pool. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] tries to reach a balance between two goals:

- Keep the buffer pool from becoming so large that the entire system is low on memory.
- Minimize physical I/O to the database files by maximizing the size of the buffer pool.

In a heavily loaded system, some large queries that require a large amount of memory to run can't get the minimum amount of requested memory, and receive a time-out error while waiting for memory resources. To resolve this, increase the [query wait Option](../database-engine/configure-windows/configure-the-query-wait-server-configuration-option.md). For a parallel query, consider reducing the [max degree of parallelism Option](../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md).

In a heavily loaded system under memory pressure, queries with merge join, sort and bitmap in the query plan can drop the bitmap when the queries don't get the minimum required memory for the bitmap. This can affect the query performance and if the sorting process can't fit in memory, it can increase the usage of worktables in `tempdb` database, causing `tempdb` to grow. To resolve this problem, add physical memory, or tune the queries to use a different and faster query plan.

### Conventional (virtual) memory

All SQL Server editions support conventional memory on 64-bit platform. The SQL Server process can access virtual address space up to Operating System maximum on x64 architecture (SQL Server Standard Edition supports up to 128 GB). With IA64 architecture, the limit was 7 TB (IA64 not supported in SQL Server 2012 (11.x) and above). See [Memory Limits for Windows](/windows/win32/memory/memory-limits-for-windows-releases) for more information.

### Address Windows Extensions (AWE) memory

By using [Address Windowing Extensions](/windows/win32/memory/address-windowing-extensions) (AWE) and the *Lock pages in memory* (LPIM) privilege required by AWE, you can keep most of SQL Server process memory *locked* in physical RAM under low virtual memory conditions. This happens in both 32-bit and 64-bit AWE allocations. The locking of memory occurs because AWE memory doesn't go through the Virtual Memory Manager in Windows, which controls paging of memory. The AWE memory allocation API requires the *Lock pages in memory* (SeLockMemoryPrivilege) privilege; see [AllocateUserPhysicalPages notes](/windows/win32/api/memoryapi/nf-memoryapi-allocateuserphysicalpages#remarks). Therefore, the main benefit of using the AWE API is to keep most of the memory resident in RAM if there is memory pressure on the system. For information on how to allow SQL Server to use AWE, see [Enable the Lock pages in memory option](../database-engine/configure-windows/enable-the-lock-pages-in-memory-option-windows.md).

If LPIM is granted, we strongly recommend that you set **max server memory (MB)** to a specific value, rather than leaving the default of 2,147,483,647 megabytes (MB). For more information, see [Server Memory Server Configuration: Set options manually](../database-engine/configure-windows/server-memory-server-configuration-options.md#manually) and [Lock pages in memory (LPIM)](../database-engine/configure-windows/server-memory-server-configuration-options.md#lock-pages-in-memory-lpim).

If LPIM isn't enabled, SQL Server will switch to using conventional memory and in cases of OS memory exhaustion, error [17890](errors-events/mssqlserver-17890-database-engine-error.md) may be reported in the error log. The error resembles the following example:

```output
A significant part of SQL Server process memory has been paged out. This may result in a performance degradation. Duration: #### seconds. Working set (KB): ####, committed (KB): ####, memory utilization: ##%.
```

## <a id="changes-to-memory-management-starting-2012-11x-gm"></a> Changes to memory management starting with [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]

In older versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], memory allocation was done using five different mechanisms:

- **Single-Page Allocator (SPA)**, including only memory allocations that were less than, or equal to 8 KB in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] process. The **max server memory (MB)** and **min server memory (MB)** configuration options determined the limits of physical memory that the SPA consumed. The Buffer Pool was simultaneously the mechanism for SPA, and the largest consumer of single-page allocations.
- **Multi-Page Allocator (MPA)**, for memory allocations that request more than 8 KB.
- **CLR Allocator**, including the SQL CLR heaps and its global allocations that are created during CLR initialization.
- Memory allocations for **[thread stacks](../relational-databases/memory-management-architecture-guide.md#stacksizes)** in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] process.
- **Direct Windows allocations (DWA)**, for memory allocation requests made directly to Windows. These include Windows heap usage and direct virtual allocations made by modules that are loaded into the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] process. Examples of such memory allocation requests include allocations from extended stored procedure DLLs, objects that are created by using Automation procedures (sp_OA calls), and allocations from linked server providers.

Starting with [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], Single-Page allocations, Multi-Page allocations and CLR allocations are all consolidated into an **"Any size" Page Allocator**, and included in memory limits controlled by **max server memory (MB)** and **min server memory (MB)** configuration options. This change provided a more accurate sizing ability for all memory requirements that go through the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] memory manager.

> [!IMPORTANT]  
> Carefully review your current **max server memory (MB)** and **min server memory (MB)** configurations after you upgrade to [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] and later. This is because starting in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], such configurations now include and account for more memory allocations compared to earlier versions.
> These changes apply to both 32-bit and 64-bit versions of [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] and [!INCLUDE[ssSQL14](../includes/sssql14-md.md)], and 64-bit versions of [!INCLUDE[sssql16-md](../includes/sssql16-md.md)] and later.

The following table indicates whether a specific type of memory allocation is controlled by the **max server memory (MB)** and **min server memory (MB)** configuration options:

|Type of memory allocation| [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)], [!INCLUDE [sql2008-md](../includes/sql2008-md.md)] and [!INCLUDE [sql2008r2-md](../includes/sql2008r2-md.md)]| Starting with [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]|
|-------|-------|-------|
|Single-page allocations|Yes|Yes, consolidated into "any size" page allocations|
|Multi-page allocations|No|Yes, consolidated into "any size" page allocations|
|CLR allocations|No|Yes|
|Thread stacks memory|No|No|
|Direct allocations from Windows|No|No|

Starting with [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] might allocate more memory than the value specified in the **max server memory (MB)** setting. This behavior may occur when the **Total Server Memory (KB)** value has already reached the **Target Server Memory (KB)** setting, as specified by **max server memory (MB)**. If there is insufficient contiguous free memory to meet the demand of multi-page memory requests (more than 8 KB) because of memory fragmentation, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] can perform over-commitment instead of rejecting the memory request.

As soon as this allocation is performed, the Resource Monitor background task starts to signal all memory consumers to release the allocated memory, and tries to bring the **Total Server Memory (KB)** value below the **Target Server Memory (KB)** specification. Therefore, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] memory usage could briefly exceed the **max server memory (MB)** setting. In this situation, the **Total Server Memory (KB)** performance counter reading will exceed the **max server memory (MB)** and **Target Server Memory (KB)** settings.

This behavior is typically observed during the following operations:

- Large columnstore index queries
- Large [batch mode on rowstore](../relational-databases/performance/intelligent-query-processing-details.md#batch-mode-on-rowstore) queries
- Columnstore index (re)builds, which use large volumes of memory to perform Hash and Sort operations
- Backup operations that require large memory buffers
- Tracing operations that have to store large input parameters

## <a id="changes-to-memory-management-starting-with-"></a> Changes to memory_to_reserve starting with [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]

In older versions of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] memory manager set aside a part of the process virtual address space (VAS) for use by the **Multi-Page Allocator (MPA)**, **CLR Allocator**, memory allocations for **thread stacks** in the SQL Server process, and **Direct Windows allocations (DWA)**. This part of the virtual address space is also known as "Mem-To-Leave" or "non-Buffer Pool" region.

The virtual address space that is reserved for these allocations is determined by the **memory_to_reserve** configuration option. The default value that [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] uses is 256 MB.

Because the "any size" page allocator also handles allocations greater than 8 KB, the **memory_to_reserve** value doesn't include the multi-page allocations. Except for this change, everything else remains the same with this configuration option.

The following table indicates whether a specific type of memory allocation falls into the **memory_to_reserve** region of the virtual address space for the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] process:

|Type of memory allocation| [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)], [!INCLUDE [sql2008-md](../includes/sql2008-md.md)] and [!INCLUDE [sql2008r2-md](../includes/sql2008r2-md.md)]| Starting with [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]|
|-------|-------|-------|
|Single-page allocations|No|No, consolidated into "any size" page allocations|
|Multi-page allocations|Yes|No, consolidated into "any size" page allocations|
|CLR allocations|Yes|Yes|
|Thread stacks memory|Yes|Yes|
|Direct allocations from Windows|Yes|Yes|

## Dynamic memory management

The default memory management behavior of the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] is to acquire as much memory as it needs without creating a memory shortage on the system. The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] does this by using the Memory Notification APIs in Microsoft Windows.

When [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is using memory dynamically, it queries the system periodically to determine the amount of free memory. Maintaining this free memory prevents the operating system (OS) from paging. If less memory is free, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] releases memory to the OS. If more memory is free, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] may allocate more memory. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] adds memory only when its workload requires more memory; a server at rest doesn't increase the size of its virtual address space. If you notice that Task Manager and Performance Monitor show a steady decrease in available memory when [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is using dynamic memory management, this is the default behavior and shouldn't be perceived as a memory leak.

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

### <a id="stacksizes"></a> Stack sizes

 Memory for thread stacks <sup>1</sup>, CLR <sup>2</sup>, extended procedure .dll files, the OLE DB providers referenced by distributed queries, automation objects referenced in [!INCLUDE[tsql](../includes/tsql-md.md)] statements, and any memory allocated by a non [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] DLL, are *not* controlled by **max server memory (MB)**.

<sup>1</sup> Refer to the article on how to [Configure the max worker threads Server Configuration Option](../database-engine/configure-windows/configure-the-max-worker-threads-server-configuration-option.md), for information on the calculated default worker threads for a given number of affinitized CPUs in the current host. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] stack sizes are as follows:

|SQL Server architecture|OS architecture|Stack size|
|--------------------|----------------------|----------------------|
|x86 (32-bit)|x86 (32-bit)|512 KB|
|x86 (32-bit)|x64 (64-bit)|768 KB|
|x64 (64-bit)|x64 (64-bit)|2048 KB|
|IA64 (Itanium)|IA64 (Itanium)|4096 KB|

<sup>2</sup> CLR memory is managed under max_server_memory allocations starting with [!INCLUDE[ssSQL11](../includes/sssql11-md.md)].

[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] uses the memory notification API **QueryMemoryResourceNotification** to determine when the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] memory manager may allocate memory and release memory.

When [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] starts, it computes the size of virtual address space for the buffer pool based on several parameters such as amount of physical memory on the system, number of server threads and various startup parameters. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] reserves the computed amount of its process virtual address space for the buffer pool, but it acquires (commits) only the required amount of physical memory for the current load.

The instance then continues to acquire memory as needed to support the workload. As more users connect and run queries, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] acquires more physical memory on demand. A [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance continues to acquire physical memory until it either reaches its **max server memory (MB)** allocation target or the OS indicates there is no longer an excess of free memory; it frees memory when it has more than the min server memory setting, and the OS indicates that there is a shortage of free memory.

As other applications are started on a computer running an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], they consume memory and the amount of free physical memory drops below the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] target. The instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] adjusts its memory consumption. If another application is stopped and more memory becomes available, the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] increases the size of its memory allocation. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] can free and acquire several megabytes of memory each second, allowing it to quickly adjust to memory allocation changes.

## Effects of min and max server memory

The *min server memory* and *max server memory* configuration options establish upper and lower limits to the amount of memory used by the buffer pool and other caches of the [!INCLUDE[ssde_md](../includes/ssde_md.md)]. The buffer pool doesn't immediately acquire the amount of memory specified in min server memory. The buffer pool starts with only the memory required to initialize. As the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] workload increases, it keeps acquiring the memory required to support the workload. The buffer pool doesn't free any of the acquired memory until it reaches the amount specified in min server memory. Once min server memory is reached, the buffer pool then uses the standard algorithm to acquire and free memory as needed. The only difference is that the buffer pool never drops its memory allocation below the level specified in min server memory, and never acquires more memory than the level specified in **max server memory (MB)**.

> [!NOTE]  
> [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] as a process acquires more memory than specified by **max server memory (MB)** option. Both internal and external components can allocate memory outside of the buffer pool, which consumes additional memory, but the memory allocated to the buffer pool usually still represents the largest portion of memory consumed by [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].

The amount of memory acquired by the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] is entirely dependent on the workload placed on the instance. A [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance that isn't processing many requests may never reach min server memory.

If the same value is specified for both min server memory and **max server memory (MB)**, then once the memory allocated to the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] reaches that value, the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] stops dynamically freeing and acquiring memory for the buffer pool.

If an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is running on a computer where other applications are frequently stopped or started, the allocation and deallocation of memory by the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] may slow the startup times of other applications. Also, if [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is one of several server applications running on a single computer, the system administrators may need to control the amount of memory allocated to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. In these cases, you can use the min server memory and **max server memory (MB)** options to control how much memory [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] can use. The **min server memory** and **max server memory** options are specified in megabytes. For more information including recommendations on how to set these memory configurations, see [Server Memory Configuration Options](../database-engine/configure-windows/server-memory-server-configuration-options.md).

## Memory used by SQL Server objects specifications

The following list describes the approximate amount of memory used by different objects in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. The amounts listed are estimates and can vary depending on the environment and how objects are created:

- Lock (as maintained by the Lock Manager): 64 bytes + 32 bytes per owner
- User connection: Approximately (3 \* *network_packet_size* + 94 KB)

The *network packet size* is the size of the tabular data stream (TDS) packets that are used to communicate between applications and the [!INCLUDE[ssde_md](../includes/ssde_md.md)]. The default packet size is 4 KB, and is controlled by the network packet size configuration option.

When multiple active result sets (MARS) are enabled, the user connection is approximately (3 + 3 \* *num_logical_connections*) * network_packet_size + 94 KB.

## Effects of min memory per query

The **min memory per query** configuration option establishes the minimum amount of memory (in kilobytes) that will be allocated for the execution of a query. This is also known as the minimum memory grant. All queries must wait until the minimum memory requested can be secured, before execution can start, or until the value specified in the query wait server configuration option is exceeded. The wait type that is accumulated in this scenario is `RESOURCE_SEMAPHORE`.

> [!IMPORTANT]
> Don't set the **min memory per query** server configuration option too high, especially on very busy systems, because doing so could lead to:
>
> - Increased competition for memory resources.
> - Decreased concurrency by increasing the amount of memory for every single query, even if the required memory at runtime is lower that this configuration.
>
> For recommendations on using this configuration, see [Configure the min memory per query Server Configuration Option](../database-engine/configure-windows/configure-the-min-memory-per-query-server-configuration-option.md#Recommendations).

### Memory grant considerations

For *row mode execution*, the initial memory grant can't be exceeded under any condition. If more memory than the initial grant is needed to execute *hash* or *sort* operations, then these will spill to disk. A hash operation that spills is supported by a Workfile in `tempdb`, while a sort operation that spills is supported by a [Worktable](../relational-databases/query-processing-architecture-guide.md#worktables).

A spill that occurs during a Sort operation is known as a [Sort warning](../relational-databases/event-classes/sort-warnings-event-class.md). Sort warnings indicate that sort operations don't fit into memory. This doesn't include sort operations involving the creation of indexes, only sort operations within a query (such as an `ORDER BY` clause used in a `SELECT` statement).

A spill that occurs during a hash operation is known as a [Hash warning](../relational-databases/event-classes/hash-warning-event-class.md). These occur when a hash recursion or cessation of hashing (hash bailout) has occurred during a hashing operation.

- Hash recursion occurs when the build input doesn't fit into available memory, resulting in the split of input into multiple partitions that are processed separately. If any of these partitions still don't fit into available memory, it is split into subpartitions, which are also processed separately. This splitting process continues until each partition fits into available memory or until the maximum recursion level is reached.
- Hash bailout occurs when a hashing operation reaches its maximum recursion level and shifts to an alternate plan to process the remaining partitioned data. These events can cause reduced performance in your server.

For *batch mode execution*, the initial memory grant can dynamically increase up to a certain internal threshold by default. This dynamic memory grant mechanism is designed to allow memory-resident execution of *hash* or *sort* operations running in batch mode. If these operations still don't fit into memory, then these will spill to disk.

For more information on execution modes, see the [Query Processing Architecture Guide](../relational-databases/query-processing-architecture-guide.md#execution-modes).

## Buffer management

The primary purpose of a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database is to store and retrieve data, so intensive disk I/O is a core characteristic of the Database Engine. And because disk I/O operations can consume many resources and take a relatively long time to finish, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] focuses on making I/O highly efficient. Buffer management is a key component in achieving this efficiency. The buffer management component consists of two mechanisms: the *buffer manager* to access and update database pages, and the *buffer cache* (also called the *buffer pool*), to reduce database file I/O.

### How buffer management works

A buffer is an 8-KB page in memory, the same size as a data or index page. Thus, the buffer cache is divided into 8-KB pages. The buffer manager manages the functions for reading data or index pages from the database disk files into the buffer cache and writing modified pages back to disk. A page remains in the buffer cache until the buffer manager needs the buffer area to read in more data. Data is written back to disk only if it is modified. Data in the buffer cache can be modified multiple times before being written back to disk. For more information, see [Reading Pages](../relational-databases/reading-pages.md) and [Writing Pages](../relational-databases/writing-pages.md).

When [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] starts, it computes the size of virtual address space for the buffer cache based on several parameters such as the amount of physical memory on the system, the configured number of maximum server threads, and various startup parameters. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] reserves this computed amount of its process virtual address space (called the memory target) for the buffer cache, but it acquires (commits) only the required amount of physical memory for the current load. You can query the **committed_target_kb** and **committed_kb** columns in the [sys.dm_os_sys_info](../relational-databases/system-dynamic-management-views/sys-dm-os-sys-info-transact-sql.md) catalog view to return the number of pages reserved as the memory target and the number of pages currently committed in the buffer cache, respectively.

The interval between [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] startup and when the buffer cache obtains its memory target is called ramp-up. During this time, read requests fill the buffers as needed. For example, a single 8-KB page read request fills a single buffer page. This means the ramp-up depends on the number and type of client requests. Ramp-up is expedited by transforming single page read requests into aligned eight page requests (making up one extent). This allows the ramp-up to finish much faster, especially on machines with a lot of memory. For more information about pages and extents, see [Pages and Extents Architecture Guide](../relational-databases/pages-and-extents-architecture-guide.md#pages-and-extents).

Because the buffer manager uses most of the memory in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] process, it cooperates with the memory manager to allow other components to use its buffers. The buffer manager interacts primarily with the following components:

- Resource Manager to control overall memory usage and, in 32-bit platforms, to control address space usage.
- Database Manager and the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Operating System (SQLOS) for low-level file I/O operations.
- Log Manager for write-ahead logging.

### Supported features

The buffer manager supports the following features:

- The buffer manager is *non-uniform memory access (NUMA)* aware. Buffer cache pages are distributed across hardware NUMA nodes, which allows a thread to access a buffer page that is allocated on the local NUMA node rather than from foreign memory.
- The buffer manager supports *Hot Add Memory*, which allows users to add physical memory without restarting the server.
- The buffer manager supports *large pages* on 64-bit platforms. The page size is specific to the version of Windows.

  > [!NOTE]  
  > Prior to [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], enabling large pages in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] requires [trace flag 834](../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md).

- The buffer manager provides extra diagnostics that are exposed through dynamic management views. You can use these views to monitor various operating system resources that are specific to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For example, you can use the [sys.dm_os_buffer_descriptors](../relational-databases/system-dynamic-management-views/sys-dm-os-buffer-descriptors-transact-sql.md) view to monitor the pages in the buffer cache.

### Disk I/O

The buffer manager only performs reads and writes to the database. Other file and database operations such as open, close, extend, and shrink are performed by the database manager and file manager components.

Disk I/O operations by the buffer manager have the following characteristics:

- All I/Os are performed asynchronously, which allows the calling thread to continue processing while the I/O operation takes place in the background.
- All I/Os are issued in the calling threads unless the affinity I/O  option is in use. The affinity I/O mask option binds [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] disk I/O to a specified subset of CPUs. In high-end [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] online transactional processing (OLTP) environments, this extension can enhance the performance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] threads issuing I/Os.
- Multiple page I/Os are accomplished with scatter-gather I/O, which allows data to be transferred into or out of noncontiguous areas of memory. This means that [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] can quickly fill or flush the buffer cache while avoiding multiple physical I/O requests.

#### Long I/O requests

The buffer manager reports on any I/O request that has been outstanding for at least 15 seconds. This helps the system administrator distinguish between [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] problems and I/O subsystem problems. Error message 833 is reported and appears in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] error log as follows:

```output
SQL Server has encountered ## occurrence(s) of I/O requests taking longer than 15 seconds to complete on file [##] in database [##] (#). The OS file handle is 0x00000. The offset of the latest long I/O is: 0x00000.
```

A long I/O may be either a read or a write; it isn't currently indicated in the message. Long-I/O messages are warnings, not errors. They don't indicate problems with [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] but with the underlying I/O system. The messages are reported to help the system administrator find the cause of poor [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] response times more quickly, and to distinguish problems that are outside the control of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. As such, they don't require any action, but the system administrator should investigate why the I/O request took so long, and whether the time is justifiable.

#### Causes of long I/O requests

A long I/O message may indicate that an I/O is permanently blocked and will never complete (known as lost I/O), or merely that it just hasn't completed yet. It isn't possible to tell from the message which scenario is the case, although a lost I/O will often lead to a latch timeout.

Long I/Os often indicate a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] workload that is too intense for the disk subsystem. An inadequate disk subsystem may be indicated when:

- Multiple long I/O messages appear in the error log during a heavy [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] workload.
- Performance Monitor counters show long disk latencies, long disk queues, or no disk idle time.

Long I/Os may also be caused by a component in the I/O path (for example, a driver, controller, or firmware) continually postponing servicing an old I/O request in favor of servicing newer requests that are closer to the current position of the disk head. The common technique of processing requests in priority based upon which ones are closest to the current position of the read/write head is known as "elevator seeking." This may be difficult to corroborate with the Performance Monitor tool because most I/Os are being serviced promptly. Long I/O requests can be aggravated by workloads that perform large amounts of sequential I/O, such as backup and restore, table scans, sorting, creating indexes, bulk loads, and zeroing out files.

Isolated long I/Os that don't appear related to any of the previous conditions may be caused by a hardware or driver problem. The system event log may contain a related event that helps to diagnose the problem.

### Memory pressure detection

Memory pressure is a condition resulting from memory shortage, and can result in:

- Extra I/Os (such as very active lazy writer background thread)
- Higher recompile ratio
- Longer running queries (if memory grant waits exist)
- Extra CPU cycles

This situation can be triggered by external or internal causes. External causes include:

- Available physical memory (RAM) is low. This causes the system to trim working sets of currently running processes, which may result in overall slowdown. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] may reduce the commit target of the buffer pool and start trimming internal caches more often.
- Overall available system memory (which includes the system page file) is low. This may cause the system to fail memory allocations, as it is unable to page out currently allocated memory.

Internal causes include:

- Responding to the external memory pressure, when the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] sets lower memory usage caps.
- Memory settings were manually lowered by reducing the *max server memory* configuration.
- Changes in memory distribution of internal components between the several caches.

The [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] implements a framework dedicated to detecting and handling memory pressure, as part of its dynamic memory management. This framework includes the background task called Resource Monitor. The Resource Monitor task monitors the state of external and internal memory indicators. Once one of these indicators changes status, it calculates the corresponding notification and it broadcasts it. These notifications are internal messages from each of the engine components, and stored in ring buffers.

Two ring buffers hold information relevant to dynamic memory management:

- The Resource Monitor ring buffer, which tracks Resource Monitor activity like was memory pressure signaled or not. This ring buffer has status information depending on the current condition of `RESOURCE_MEMPHYSICAL_HIGH`, `RESOURCE_MEMPHYSICAL_LOW`, `RESOURCE_MEMPHYSICAL_STEADY`, or `RESOURCE_MEMVIRTUAL_LOW`.
- The Memory Broker ring buffer, which contains records of memory notifications for each Resource Governor resource pool. As internal memory pressure is detected, low memory notification is turned on for components that allocate memory, to trigger actions meant to balance the memory between caches.

Memory brokers monitor the demand consumption of memory by each component and then based on the information collected, it calculates and optimal value of memory for each of these components. There is a set of brokers for each Resource Governor resource pool. This information is then broadcast to each of the components, which grow or shrink their usage as required.

For more information about memory brokers, see [sys.dm_os_memory_brokers](../relational-databases/system-dynamic-management-views/sys-dm-os-memory-brokers-transact-sql.md).

### Error detection

Database pages can use one of two optional mechanisms that help ensure the integrity of the page from the time it is written to disk until it is read again: torn page protection and checksum protection. These mechanisms allow an independent method of verifying the correctness of not only the data storage, but hardware components such as controllers, drivers, cables, and even the operating system. The protection is added to the page just before writing it to disk, and verified after it is read from disk.

[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] will retry any read that fails with a checksum, torn page, or other I/O error four times. If the read is successful in any one of the retry attempts, a message will be written to the error log and the command that triggered the read will continue. If the retry attempts fail, the command will fail with error message 824.

The kind of page protection used is an attribute of the database containing the page. Checksum protection is the default protection for databases created in [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)] and later. The page protection mechanism is specified at database creation time, and may be altered by using `ALTER DATABASE SET`. You can determine the current page protection setting by querying the `page_verify_option` column in the [sys.databases](../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view or the `IsTornPageDetectionEnabled` property of the [DATABASEPROPERTYEX](../t-sql/functions/databasepropertyex-transact-sql.md) function.

> [!NOTE]  
> If the page protection setting is changed, the new setting does not immediately affect the entire database. Instead, pages adopt the current protection level of the database whenever they are written next. This means that the database may be composed of pages with different kinds of protection.

#### Torn page protection

Torn page protection, introduced in [!INCLUDE [ssversion2000-md](../includes/ssversion2000-md.md)], is primarily a way of detecting page corruptions due to power failures. For example, an unexpected power failure may leave only part of a page written to disk. When torn page protection is used, a specific 2-bit signature pattern for each 512-byte sector in the 8-kilobyte (KB) database page and stored in the database page header when the page is written to disk.

When the page is read from disk, the torn bits stored in the page header are compared to the actual page sector information. The signature pattern alternates between binary `01` and `10` with every write, so it is always possible to tell when only a portion of the sectors made it to disk: if a bit is in the wrong state when the page is later read, the page was written incorrectly and a torn page is detected. Torn page detection uses minimal resources; however, it doesn't detect all errors caused by disk hardware failures. For information on setting torn page detection, see [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-set-options.md#page_verify).

#### Checksum protection

Checksum protection, introduced in [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)], provides stronger data integrity checking. A checksum is calculated for the data in each page that is written, and stored in the page header. Whenever a page with a stored checksum is read from disk, the database engine recalculates the checksum for the data in the page and raises error 824 if the new checksum is different from the stored checksum. Checksum protection can catch more errors than torn page protection because it is affected by every byte of the page, however, it is moderately resource-intensive.

When checksum is enabled, errors caused by power failures and flawed hardware or firmware can be detected any time the buffer manager reads a page from disk. For information on setting checksum, see [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../t-sql/statements/alter-database-transact-sql-set-options.md#page_verify).

> [!IMPORTANT]  
> When a user or system database is upgraded to [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)] or later, the [PAGE_VERIFY](../t-sql/statements/alter-database-transact-sql-set-options.md#page_verify) value (`NONE` or `TORN_PAGE_DETECTION`) is retained. We highly recommend that you use `CHECKSUM`. `TORN_PAGE_DETECTION` may use fewer resources, but provides a minimal subset of the `CHECKSUM` protection.

## Understand non-uniform memory access

[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is non-uniform memory access (NUMA) aware, and performs well on NUMA hardware without special configuration. As clock speed and the number of processors increase, it becomes increasingly difficult to reduce the memory latency required to use this additional processing power. To circumvent this, hardware vendors provide large L3 caches, but this is only a limited solution. NUMA architecture provides a scalable solution to this problem.

[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] has been designed to take advantage of NUMA-based computers without requiring any application changes. For more information, see [How to: Configure SQL Server to Use Soft-NUMA](../database-engine/configure-windows/soft-numa-sql-server.md).

## Dynamic partition of memory objects

Heap allocators, called memory objects in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], allow the [!INCLUDE[ssde_md](../includes/ssde_md.md)] to allocate memory from the heap. These can be tracked using the [sys.dm_os_memory_objects](../relational-databases/system-dynamic-management-views/sys-dm-os-memory-objects-transact-sql.md) DMV.

CMemThread is a thread-safe memory object type that allows concurrent memory allocations from multiple threads. For correct tracking, CMemThread objects rely on synchronization constructs (a mutex) to ensure only a single thread is updating critical pieces of information at a time.

> [!NOTE]  
> The CMemThread object type is utilized throughout the [!INCLUDE[ssde_md](../includes/ssde_md.md)] code base for many different allocations, and can be partitioned globally, by node or by CPU.

However, the use of mutexes can lead to contention if many threads are allocating from the same memory object in a highly concurrent fashion. Therefore, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] has the concept of partitioned memory objects (PMO) and each partition is represented by a single CMemThread object. The partitioning of a memory object is statically defined and can't be changed after creation. As memory allocation patterns vary widely based on aspects like hardware and memory usage, it is impossible to come up with the perfect partitioning pattern upfront.

In most cases, using a single partition will suffice, but in some scenarios this may lead to contention, which can be prevented only with a highly partitioned memory object. It isn't desirable to partition each memory object as more partitions may result in other inefficiencies and increase memory fragmentation.

> [!NOTE]  
> Before [!INCLUDE[sssql16-md](../includes/sssql16-md.md)], trace flag 8048 could be used to force a node-based PMO to become a CPU-based PMO. Starting with [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] SP2 and [!INCLUDE[sssql16-md](../includes/sssql16-md.md)], this behavior is dynamic and controlled by the engine.

Starting with [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] SP2 and [!INCLUDE[sssql16-md](../includes/sssql16-md.md)], the [!INCLUDE[ssde_md](../includes/ssde_md.md)] can dynamically detect contention on a specific CMemThread object and promote the object to a per-node or a per-CPU based implementation. Once promoted, the PMO remains promoted until the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] process is restarted. CMemThread contention can be detected by the presence of high CMEMTHREAD waits in the [sys.dm_os_wait_stats](../relational-databases/system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql.md) DMV, and by observing the [sys.dm_os_memory_objects](../relational-databases/system-dynamic-management-views/sys-dm-os-memory-objects-transact-sql.md) DMV columns `contention_factor`, `partition_type`, `exclusive_allocations_count`, and `waiting_tasks_count`.

## Next steps

- [Server Memory Server Configuration Options](../database-engine/configure-windows/server-memory-server-configuration-options.md)
- [Reading Pages](../relational-databases/reading-pages.md)
- [Writing Pages](../relational-databases/writing-pages.md)
- [How to: Configure SQL Server to Use Soft-NUMA](../database-engine/configure-windows/soft-numa-sql-server.md)
- [Requirements for Using Memory-Optimized Tables](../relational-databases/in-memory-oltp/requirements-for-using-memory-optimized-tables.md)
- [Troubleshoot out of memory or low memory issues in SQL Server](/troubleshoot/sql/performance/troubleshoot-memory-issues)
- [Resolve Out Of Memory Issues Using Memory-Optimized Tables](../relational-databases/in-memory-oltp/resolve-out-of-memory-issues.md)
