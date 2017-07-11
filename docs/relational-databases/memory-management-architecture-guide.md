---
title: "Memory Management Architecture Guide | Microsoft Docs"
ms.custom: ""
ms.date: "10/21/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "guide, memory management architecture"
  - "memory management architecture guide"
ms.assetid: 7b0d0988-a3d8-4c25-a276-c1bdba80d6d5
caps.latest.revision: 6
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Memory Management Architecture Guide
[!INCLUDE[tsql-appliesto-ss2008-all_md](../includes/tsql-appliesto-ss2008-all-md.md)]

## Memory Architecture

[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] dynamically acquires and frees memory as required. Typically, an administrator does not have to specify how much memory should be allocated to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], although the option still exists and is required in some environments.

One of the primary design goals of all database software is to minimize disk I/O because disk reads and writes are among the most resource-intensive operations. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] builds a buffer pool in memory to hold pages read from the database. Much of the code in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is dedicated to minimizing the number of physical reads and writes between the disk and the buffer pool. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] tries to reach a balance between two goals:

* Keep the buffer pool from becoming so big that the entire system is low on memory.
* Minimize physical I/O to the database files by maximizing the size of the buffer pool.


> [!NOTE]
> In a heavily loaded system, some large queries that require a large amount of memory to run cannot get the minimum amount of requested memory and receive a time-out error while waiting for memory resources. To resolve this, increase the [query wait Option](../database-engine/configure-windows/configure-the-query-wait-server-configuration-option.md). For a parallel query, consider reducing the [max degree of parallelism Option](../database-engine/configure-windows/configure-the-max-degree-of-parallelism-server-configuration-option.md).
 
> [!NOTE]
> In a heavily loaded system under memory pressure, queries with merge join, sort and bitmap in the query plan can drop the bitmap when the queries do not get the minimum required memory for the bitmap. This can affect the query performance and if the sorting process can not fit in memory, it can increase the usage of worktables in tempdb database, causing tempdb to grow. To resolve this problem add physical memory or tune the queries to use a different and faster query plan.
 
### Providing the Maximum Amount of Memory to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]

By using AWE and the Locked Pages in Memory privilege, you can provide the following amounts of memory to the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Database Engine. (The following table includes a column for 32-bit versions which are no longer available.)

| |32-bit <sup>1</sup> |64-bit
|-------|-------|-------| 
|Conventional memory |All [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] editions. Up to process virtual address space limit: <br>- 2 GB<br>- 3 GB with /3gb boot parameter <sup>2</sup> <br>- 4 GB on WOW64 <sup>3</sup> |All [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] editions. Up to process virtual address space limit: <br>- 7 TB with IA64 architecture (IA64 not supported in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] and above)<br>- Operating system maximum with x64 architecture <sup>4</sup>
|AWE mechanism (Allows [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to go beyond the process virtual address space limit on 32-bit platform.) |[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Standard, Enterprise, and Developer editions: Buffer pool is capable of accessing up to 64 GB of memory.|Not applicable <sup>5</sup> |
|Lock pages in memory operating system (OS) privilege (Allows locking physical memory, preventing OS paging of the locked memory.) <sup>6</sup> |[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Standard, Enterprise, and Developer editions: Required for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] process to use AWE mechanism. Memory allocated through AWE mechanism cannot be paged out. <br> Granting this privilege without enabling AWE has no effect on the server. |[!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Enterprise and Developer editions: Recommended, to avoid operating system paging. Might provide a performance benefit depending on the workload. The amount of memory accessible is similar to conventional memory case. |

<sup>1</sup> 32-bit versions are not available starting with [!INCLUDE[ssSQL14](../includes/sssql14-md.md)].  
<sup>2</sup> /3gb is an operating system boot parameter. For more information, visit the MSDN Library.  
<sup>3</sup> WOW64 (Windows on Windows 64) is a mode in which 32-bit [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] runs on a 64-bit operating system.  
<sup>4</sup> [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Standard Edition supports up to 128 GB. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Enterprise Edition supports the maximum of the operating system maximum..  
<sup>5</sup> Note that the sp_configure awe enabled option was present on 64-bit [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], but it is ignored.    
<sup>6</sup> If lock pages in memory privilege (LPIM) is granted (either on 32-bit for AWE support or on 64-bit by itself), we recommend also setting max server memory.

> [!NOTE]
> Older versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] could run on a 32-bit operating system. Accessing more than 4 gigabytes of memory on a 32-bit operating system requires Address Windowing Extensions (AWE) to manage the memory. This is not necessary when [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is running on 64-bit operation systems. For more information about AWE, see [Process Address Space](https://msdn.microsoft.com/library/ms189334) and [Managing Memory for Large Databases](https://msdn.microsoft.com/library/ms191481) in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] 2008 documentation.   


## Dynamic Memory Management

The default memory management behavior of the Microsoft [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Database Engine is to acquire as much memory as it needs without creating a memory shortage on the system. The Database Engine does this by using the Memory Notification APIs in Microsoft Windows.

Virtual address space of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] can be divided into two distinct regions: space occupied by the buffer pool and the rest. If AWE mechanism is enabled, the buffer pool may reside in AWE mapped memory, providing additional space for database pages. 

The buffer pool serves as a primary memory allocation source of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. External components that reside inside [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] process, such as COM objects, and not aware of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] memory management facilities, use memory outside of the virtual address space occupied by the buffer pool.

When [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] starts, it computes the size of virtual address space for the buffer pool based on a number of parameters such as amount of physical memory on the system, number of server threads and various startup parameters. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] reserves the computed amount of its process virtual address space for the buffer pool, but it acquires (commits) only the required amount of physical memory for the current load.

The instance then continues to acquire memory as needed to support the workload. As more users connect and run queries, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] acquires the additional physical memory on demand. A [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance continues to acquire physical memory until it either reaches its max server memory allocation target or Windows indicates there is no longer an excess of free memory; it frees memory when it has more than the min server memory setting, and Windows indicates that there is a shortage of free memory.

As other applications are started on a computer running an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], they consume memory and the amount of free physical memory drops below the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] target. The instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] adjusts its memory consumption. If another application is stopped and more memory becomes available, the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] increases the size of its memory allocation. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] can free and acquire several megabytes of memory each second, allowing it to quickly adjust to memory allocation changes.


## Effects of min and max server memory

The min server memory and max server memory configuration options establish upper and lower limits to the amount of memory used by the buffer pool of the Microsoft [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Database Engine. The buffer pool does not immediately acquire the amount of memory specified in min server memory. The buffer pool starts with only the memory required to initialize. As the Database Engine workload increases, it keeps acquiring the memory required to support the workload. The buffer pool does not free any of the acquired memory until it reaches the amount specified in min server memory. Once min server memory is reached, the buffer pool then uses the standard algorithm to acquire and free memory as needed. The only difference is that the buffer pool never drops its memory allocation below the level specified in min server memory, and never acquires more memory than the level specified in max server memory.

> [!NOTE]
> [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] as a process acquires more memory than specified by max server memory option. Both internal and external components can allocate memory outside of the buffer pool, which consumes additional memory, but the memory allocated to the buffer pool usually represents the largest portion of memory consumed by [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].


The amount of memory acquired by the Database Engine is entirely dependent on the workload placed on the instance. A [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance that is not processing many requests may never reach min server memory.

If the same value is specified for both min server memory and max server memory, then once the memory allocated to the Database Engine reaches that value, the Database Engine stops dynamically freeing and acquiring memory for the buffer pool.

If an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is running on a computer where other applications are frequently stopped or started, the allocation and deallocation of memory by the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] may slow the startup times of other applications. Also, if [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is one of several server applications running on a single computer, the system administrators may need to control the amount of memory allocated to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. In these cases, you can use the min server memory and max server memory options to control how much memory [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] can use. For more information, see [Server Memory Configuration Options](../database-engine/configure-windows/server-memory-server-configuration-options.md).

The min server memory and max server memory options are specified in megabytes.

## Memory Used by [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Objects Specifications

The following list describes the approximate amount of memory used by different objects in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. The amounts listed are estimates and can vary depending on the environment and how objects are created.

* Lock: 64 bytes + 32 bytes per owner   
* User connection: Approximately (3* *network_packet_size + 94 kb)    

The network packet size is the size of the tabular data scheme (TDS) packets that are used to communicate between applications and the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Database Engine. The default packet size is 4 KB, and is controlled by the network packet size configuration option.

When multiple active result sets (MARS) are enabled, the user connection is approximately (3 + 3 * num_logical_connections) * network_packet_size + 94 KB

## Buffer Management

The primary purpose of a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database is to store and retrieve data, so intensive disk I/O is a core characteristic of the Database Engine. And because disk I/O operations can consume many resources and take a relatively long time to finish, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] focuses on making I/O highly efficient. Buffer management is a key component in achieving this efficiency. The buffer management component consists of two mechanisms: the buffer manager to access and update database pages, and the buffer cache (also called the buffer pool), to reduce database file I/O. 

### How Buffer Management Works

A buffer is an 8-KB page in memory, the same size as a data or index page. Thus, the buffer cache is divided into 8-KB pages. The buffer manager manages the functions for reading data or index pages from the database disk files into the buffer cache and writing modified pages back to disk. A page remains in the buffer cache until the buffer manager needs the buffer area to read in more data. Data is written back to disk only if it is modified. Data in the buffer cache can be modified multiple times before being written back to disk. For more information, see [Reading Pages](../relational-databases/reading-pages.md) and [Writing Pages](../relational-databases/writing-pages.md).

When [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] starts, it computes the size of virtual address space for the buffer cache based on a number of parameters such as the amount of physical memory on the system, the configured number of maximum server threads, and various startup parameters. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] reserves this computed amount of its process virtual address space (called the memory target) for the buffer cache, but it acquires (commits) only the required amount of physical memory for the current load. You can query the **bpool_commit_target** and **bpool_committed columns** in the [sys.dm_os_sys_info](../relational-databases/system-dynamic-management-views/sys-dm-os-sys-info-transact-sql.md) catalog view to return the number of pages reserved as the memory target and the number of pages currently committed in the buffer cache, respectively.

The interval between [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] startup and when the buffer cache obtains its memory target is called ramp-up. During this time, read requests fill the buffers as needed. For example, a single-page read request fills a single buffer page. This means the ramp-up depends on the number and type of client requests. Ramp-up is expedited by transforming single-page read requests into aligned eight-page requests. This allows the ramp-up to finish much faster, especially on machines with a lot of memory.

Because the buffer manager uses most of the memory in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] process, it cooperates with the memory manager to allow other components to use its buffers. The buffer manager interacts primarily with the following components:

* Resource manager to control overall memory usage and, in 32-bit platforms, to control address space usage.  
* Database manager and the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Operating System (SQLOS) for low-level file I/O operations.  
* Log manager for write-ahead logging.  

### Supported Features

The buffer manager supports the following features:

* The buffer manager is non-uniform memory access (NUMA) aware. Buffer cache pages are distributed across hardware NUMA nodes, which allows a thread to access a buffer page that is allocated on the local NUMA node rather than from foreign memory. 
* The buffer manager supports Hot Add Memory, which allows users to add physical memory without restarting the server. 
* The buffer manager supports large pages on 64-bit platforms. The page size is specific to the version of Windows. 
* The buffer manager provides additional diagnostics that are exposed through dynamic management views. You can use these views to monitor a variety of operating system resources that are specific to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For example, you can use the sys.dm_os_buffer_descriptors view to monitor the pages in the buffer cache.   

### Disk I/O
The buffer manager only performs reads and writes to the database. Other file and database operations such as open, close, extend, and shrink are performed by the database manager and file manager components. 

Disk I/O operations by the buffer manager have the following characteristics:

* All I/Os are performed asynchronously, which allows the calling thread to continue processing while the I/O operation takes place in the background.
* All I/Os are issued in the calling threads unless the affinity I/O  option is in use. The affinity I/O mask option binds [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] disk I/O to a specified subset of CPUs. In high-end [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] online transactional processing (OLTP) environments, this extension can enhance the performance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] threads issuing I/Os.
* Multiple page I/Os are accomplished with scatter-gather I/O, which allows data to be transferred into or out of noncontiguous areas of memory. This means that [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] can quickly fill or flush the buffer cache while avoiding multiple physical I/O requests. 

#### Long I/O Requests  
The buffer manager reports on any I/O request that has been outstanding for at least 15 seconds. This helps the system administrator distinguish between [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] problems and I/O subsystem problems. Error message 833 is reported and appears in the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] error log as follows:

`` 
SQL Server has encountered %d occurrence(s) of I/O requests taking longer than %d seconds to complete on file [%ls] in database [%ls] (%d). The OS file handle is 0x%p. The offset of the latest long I/O is: %#016I64x.
`` 

A long I/O may be either a read or a write; it is not currently indicated in the message. Long-I/O messages are warnings, not errors. They do not indicate problems with [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. The messages are reported to help the system administrator find the cause of poor [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] response times more quickly, and to distinguish problems that are outside the control of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. As such, they do not require any action, but the system administrator should investigate why the I/O request took so long, and whether the time is justifiable.

#### Causes of Long-I/O Requests  
A long-I/O message may indicate that an I/O is permanently blocked and will never complete (known as lost I/O), or merely that it just has not completed yet. It is not possible to tell from the message which scenario is the case, although a lost I/O will often lead to a latch time-out.

Long I/Os often indicate a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] workload that is too intense for the disk subsystem. An inadequate disk subsystem may be indicated when:

* Multiple long I/O messages appear in the error log during a heavy [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] workload.
* Perfmon counters show long disk latencies, long disk queues, or no disk idle time.  

Long I/Os may also be caused by a component in the I/O path (for example, a driver, controller, or firmware) continually postponing servicing an old I/O request in favor of servicing newer requests that are closer to the current position of the disk head. The common technique of processing requests in priority based upon which ones are closest to the current position of the read/write head is known as "elevator seeking." This may be difficult to corroborate with the Windows System Monitor (PERFMON.EXE) tool because most I/Os are being serviced promptly. Long I/O requests can be aggravated by workloads that perform large amounts of sequential I/O, such as backup and restore, table scans, sorting, creating indexes, bulk loads, and zeroing out files.

Isolated long I/Os that do not appear related to any of the previous conditions may be caused by a hardware or driver problem. The system event log may contain a related event that helps to diagnose the problem.

#### Error Detection  
Database pages can use one of two optional mechanisms that help insure the integrity of the page from the time it is written to disk until it is read again: torn page protection and checksum protection. These mechanisms allow an independent method of verifying the correctness of not only the data storage, but hardware components such as controllers, drivers, cables, and even the operating system. The protection is added to the page just before writing it to disk, and verified after it is read from disk.

#### Torn Page Protection  
Torn page protection, introduced in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] 2000, is primarily a way of detecting page corruptions due to power failures. For example, an unexpected power failure may leave only part of a page written to disk. When torn page protection is used, a 2-bit signature is placed at the end of each 512-byte sector in the page (after having copied the original two bits into the page header). The signature alternates between binary 01 and 10 with every write, so it is always possible to tell when only a portion of the sectors made it to disk: if a bit is in the wrong state when the page is later read, the page was written incorrectly and a torn page is detected. Torn page detection uses minimal resources; however, it does not detect all errors caused by disk hardware failures.

#### Checksum Protection  
Checksum protection, introduced in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] 2005, provides stronger data integrity checking. A checksum is calculated for the data in each page that is written, and stored in the page header. Whenever a page with a stored checksum is read from disk, the database engine recalculates the checksum for the data in the page and raises error 824 if the new checksum is different from the stored checksum. Checksum protection can catch more errors than torn page protection because it is affected by every byte of the page, however, it is moderately resource intensive. When checksum is enabled, errors caused by power failures and flawed hardware or firmware can be detected any time the buffer manager reads a page from disk.

The kind of page protection used is an attribute of the database containing the page. Checksum protection is the default protection for databases created in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] 2005 and later. The page protection mechanism is specified at database creation time, and may be altered by using ALTER DATABASE. You can determine the current page protection setting by querying the page_verify_option column in the [sys.databases](../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view or the IsTornPageDetectionEnabled property of the [DATABASEPROPERTYEX](../t-sql/functions/databasepropertyex-transact-sql.md) function. If the page protection setting is changed, the new setting does not immediately affect the entire database. Instead, pages adopt the current protection level of the database whenever they are written next. This means that the database may be composed of pages with different kinds of protection. 

## Understanding Non-uniform Memory Access

Microsoft [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] is non-uniform memory access (NUMA) aware, and performs well on NUMA hardware without special configuration. As clock speed and the number of processors increase, it becomes increasingly difficult to reduce the memory latency required to use this additional processing power. To circumvent this, hardware vendors provide large L3 caches, but this is only a limited solution. NUMA architecture provides a scalable solution to this problem. [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] has been designed to take advantage of NUMA-based computers without requiring any application changes. For more information, see [How to: Configure SQL Server to Use Soft-NUMA](../database-engine/configure-windows/soft-numa-sql-server.md).

## See Also
[Reading Pages](../relational-databases/reading-pages.md)   
 [Writing Pages](../relational-databases/writing-pages.md)

