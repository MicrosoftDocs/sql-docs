---
description: "MSSQLSERVER_17890"
title: MSSQLSERVER_17890
ms.custom: ""
ms.date: 12/25/2020
ms.service: sql
ms.reviewer: ramakoni1, pijocoder, suresh-kandoth, vencher, tejasaks, docast
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "17890 (Database Engine error)"
ms.assetid: 
author: suresh-kandoth
ms.author: ramakoni
---
# MSSQLSERVER_17890
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

|Attribute|Value|
|---|---|
|Product Name|SQL Server|
|Event ID|17890|
|Event Source|MSSQLSERVER|
|Component|SQLEngine|
|Symbolic Name|SRV_WS_TRIMMED|
|Message Text|A significant part of SQL Server process memory has been paged out. This may result in a performance degradation. Duration: %d seconds. Working set (KB): %I64d, committed (KB): %I64d, memory utilization: %d%%.|

## Explanation

You might encounter the following error message in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log or the Windows Application event log.

> A significant part of SQL Server  process memory has been paged out. This may result in a performance degradation. Duration: 0 seconds. Working set (KB): 3383250, committed (KB): 9112480, memory utilization: 37%.

You might also notice a sudden performance degradation with query execution and all other operations on the SQL Server.

## Cause

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] monitors the various memories related information about the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process. In this case, it has detected that the working set of the process is less than 50% of the committed process memory. As a result this warning is printed. The normal causes of this warning are:

- The operating system pages out large portions of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] committed memory to the paging file.
- This could be due to sudden increased demand for memory from other applications or operating system needs.
- This could also happen when certain device drivers request contiguous memory allocations for their needs.

## User action

You can prevent the Windows operating system from paging out the buffer pool memory of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process by locking the memory that is allocated for the buffer pool in physical memory. You lock the memory by assigning the Lock pages in memory user right to the user account that is used as the startup account of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service. But before you implement this solution, review the sections [What causes SQL Server memory to be paged out](#what-causes-sql-server-memory-to-be-paged-out) and Important considerations before you assign the "Lock pages in memory" user right for an instance of SQL Server

> [!NOTE]
> Using Lock Pages in Memory ensure that the memory managed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not paged out. However, thread stacks, the EXE and any DLL images, heap memory, CLR memory can still be paged out by the OS.
>
> Starting with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 2008 SP1 Cumulative Update 2, both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Standard and Enterprise editions can use the Lock pages in memory user right. For more information about support for locked pages, view [KB970070 - Support for Locked Pages on SQL Server Standard Edition (64-bit) systems](https://support.microsoft.com/help/970070).

To assign the Lock pages in memory user right, follow these steps:

1. Click **Start**, click **Run**, type *gpedit.msc*, and then click **OK**.
1. Note The Group Policy dialog box appears.
1. Expand **Computer Configuration**, and then expand **Windows Settings**.
1. Expand **Security Settings**, and then expand **Local Policies**.
1. Click User Rights Assignment, and then double-click **Lock pages** in memory.
1. In the **Local Security Policy Setting** dialog box, click **Add User** or **Group**.
1. In the **Select Users** or **Groups** dialog box, add the account that has permission to run the Sqlservr.exe file, and then click **OK**.
1. Close the **Group Policy** dialog box.
1. Restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service.

After you assign the Lock pages in memory user right and you restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service, the Windows operating system no longer pages out the buffer pool memory within the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process. However, the Windows operating system can still page out the nonbuffer pool memory within the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process.

You can validate that the user right is used by the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by making sure that the following message is written in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Error Log at startup: "Using locked pages for buffer pool"

This message applies only to SQL Server. For more information about this message in the ERRORLOG, visit the following:
[Do I have to assign the Lock pages for Memory privilege in Local System](https://techcommunity.microsoft.com/t5/sql-server-support/do-i-have-to-assign-the-lock-pages-in-memory-privilege-for-local/ba-p/315426)

When the Windows operating system pages out the nonbuffer pool memory, you may still encounter performance issues. However, the error messages that are mentioned in the "Explanation" section are not logged in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log.

## What causes SQL Server memory to be paged out

There are three broad categories of problems that can cause this issue:

- Application-Related Issues: All applications together have exhausted the available physical memory and the OS must free some memory for new application requests for resources. Typically, the approach here is to find what applications are exhausting the memory and take necessary steps to balance the memory among them without leading to RAM exhaustion.
- Device Driver Issues: Device Drivers may cause working set paging of all processes if the driver calls a memory allocation function incorrectly.
- Operation System Issues

Below, you can find information on each of these categories

- **Application-Related issues**: Applications together may consume all of the RAM on the system. If new requests for memory are made, the OS attempts to satisfy them and if there is no free memory, it will trim the working set of running applications to satisfy the memory requests. In such cases, you may observe that the working set for most if not all applications drop significantly. To observe this, collect the following Performance Monitor counter for all applications on the system:

  - Performance object: Process
  - Counter: Working Set
  
  Also, monitor the following counter to correlate how much physical memory is available on the system.
  
  - Performance object: Memory
  - Counter: Available Memory (MB)
  
  The typical behavior that you may observe is reduction of Available memory close to 0 MB while at the same time a sudden drop of the Working Set counters for most (all) processes on the system. If you observe such behavior, you may need to take steps to reduce memory usage on the system, which includes for example reducing Max Server Memory for SQL Server.
  
    Applications may also use the system cache too much, and may cause a large growth of the system cache. To respond to the growth of the system cache, the system pages out the working set of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process or of other applications. If you experience this problem, you can use some memory management functions in the application. These functions control the system cache space that file I/O operations can use in the application. For example, you can use the SetSystemFileCacheSize function and the GetSystemFileCacheSize function to control the system cache space that file I/O operations can use.
  
    You can use the Memory performance object to view the values of various counters in this object to determine whether the system cache working set uses too much memory. For example, you can view the Cache Bytes and System Cache Resident Bytes counters. For more information about this topic, see:
  
    - [Too Much Cache](/archive/blogs/ntdebugging/too-much-cache)
    - [Microsoft Windows Dynamic Cache Service](/archive/blogs/ntdebugging/microsoft-windows-dynamic-cache-service)
    - [You experience performance issues in applications and services when the system file cache consumes most of the physical RAM](https://support.microsoft.com/help/976618)
  
    You can download and deploy the "Microsoft Windows Dynamic Cache Service" to control the memory that is consumed by the system cache.

- **Device Driver Issues**: If a device driver uses the `MmAllocateContiguousMemory` function, and if it sets the value of the HighestAcceptableAddress parameter to less than 4 gigabytes (GB), the Windows operating system may page out the working set of the processes on the system including [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process. To resolve this problem, contact the vendor of the device driver for driver updates.

    When a device driver tries to allocate memory, the Windows operating system may page out the working set of other applications. This Windows hotfix lets you use event tracing to find the device driver that causes problem. To find more information about the specific driver that causes the working set trimming behavior, see [Identifying Drivers That Allocate Contiguous Memory](/previous-versions/windows/desktop/xperf/identifying-drivers-that-allocate-contiguous-memory).

- **Operating System Issues**: To resolve the known issues that cause the Windows operating system to page out the working set of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process, apply the hotfixes that are described in the following Microsoft Knowledge Base articles.

  > [!NOTE]
  > Hotfixes are cumulative. A later version of a hotfix contains the earlier versions of that hotfix.

  - The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] set may be trimmed when the system is using some advanced TCP features. For more information, see [How to troubleshoot advanced network performance features such as RSS and NetDMA](/troubleshoot/windows-server/networking/troubleshoot-network-performance-features-rss-netdma).

  - If you are running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on Windows Server 2008, you must apply fixes for known issues that can lead to working set trimming or unnecessary excessive memory consumption by other operating system components. For more information, review the following articles
    [The report generation process may stop responding when you run Perfmon.exe with the Active Directory Diagnostics template to generate a report on a Windows Server 2008-based domain controller](/troubleshoot/windows-server/performance/report-generation-process-stops-responding).

  - If you are running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on Windows Serve 2008 R2, you must apply fixes for known issues that can lead to working set trimming. For more information review the following articles:

    - [A computer that is running Windows 7 or Windows Server 2008 R2 becomes unresponsive when you run a large application](https://support.microsoft.com/help/979149)
    - [Poor performance occurs on a computer that has NUMA-based processors and that is running Windows Server 2008 R2 or Windows 7 if a thread requests lots of memory that is within the first 4 GB of memory](https://support.microsoft.com/help/2155311)
    - [Computer intermittently performs poorly or stops responding when the Storport driver is used in Windows Server 2008 R2](https://support.microsoft.com/help/2468345)

## Important considerations before you assign the "Lock pages in memory" user right

You should make additional considerations before you assign the Lock pages in memory user right. If you assign this user right on systems that are configured incorrectly, the system may become unstable or experience a performance decrease of the whole system. Additionally, event ID 333 may be logged in the event log.

If you contact Microsoft Customer Support Service (CSS) for these problems, CSS engineers may ask you to revoke this user right for the user account that is used as the startup account of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service. This step may be necessary to collect important performance data that CSS engineers can use for necessary configuration of the various options for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and for other applications that are running on the system. After CSS engineers collect the performance data, you can assign the Lock pages in memory user right to the startup account of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service.

Before you assign the Lock pages in memory user right, make sure that you capture a Performance Monitor log to determine the memory requirements of various applications and services that are installed on the system. These applications also include SQL Server. To determine the memory requirements, collect the following baseline information:

- Make sure that you set the max server memory option and the min server memory option correctly. These options reflect only the memory requirement of the buffer pool of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process. These options do not include the memory that is allocated for other components within the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process. These components include the following:

  - The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] worker threads
  - Various DLLs and components that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process loads within the address space of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process
  - The Backup and restore operations

- The DLLs and components include various OLE DB providers, extended stored procedures, Microsoft COM objects that are used for the sp_OACreate stored procedure, linked servers, and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] CLR. Memory that is allocated for these components falls under the nonbuffer pool region of the address space of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process. To ideally determine the maximum amount of memory that the whole [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process can use, you must subtract the memory that is allocated for components that do not use the buffer pool from the total memory that you want the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process to use. Then, you can use the remainder value to set the max server memory option. Before you set the max server memory option and the min server memory option, you should carefully review the "Setting the memory options manually" topic in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.

- Determine the memory requirement of other applications and of the Windows operating system components. Applications may include other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components, for example, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Replication Agents, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Reporting Services, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Analysis Services, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Integration Services, and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Full Text Search. Applications that perform Backup operations and file copy operations may use lots of memories. Consider operations such as bulk copy and the Snapshot Agent that generate file IO. You must consider the memory requirement of all these applications when you determine the value of the max server memory option and of the min server memory option. You can use the Private Bytes counter and the Working Set counter under the Process object for every process to determine the memory requirement for a specific process.

- By default, the Lock pages in memory user right have already been assigned to the built-in Local System account. For more information, visit the following Microsoft Web site:
[Do I have to assign the Lock pages in Memory privilege for Local system?](https://techcommunity.microsoft.com/t5/sql-server-support/do-i-have-to-assign-the-lock-pages-in-memory-privilege-for-local/ba-p/315426?advanced=false&collapse_discussion=true&search_type=thread)

- If you use a Windows user account globally for all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] processes in a domain, determine the user rights that are assigned by using a Group Policy configuration. A 32-bit [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process may use this account as the startup account. However, this account requires the Lock pages in memory user right to enable the `Address Windowing Extensions` (AWE) feature. For more information, see the "Providing the maximum amount of memory to SQL Server" topic in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.

- Before you configure the max server memory option and the min server memory option for multiple [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances, consider the memory requirements of the nonbuffer pool for each instance of SQL Server. Then, configure these options for each instance of SQL Server.

Ideally, you collect this baseline information during peak loads. Therefore, you can determine the memory requirements for various applications and components to support the peak load. The memory requirements vary from one system to another system, depending on the activities and the applications that are running on the system. You can query the information that is provided in the dynamic management view sys.dm_os_process_memory to understand whether the system is encountering low memory conditions. For more information, see [sys.dm_os_process_memory (Transact-SQL)](../system-dynamic-management-views/sys-dm-os-process-memory-transact-sql.md).

## Improvements added in Windows Server 2008 and R2 version

Windows Server 2008 and Windows Server 2008 R2 improve the contiguous memory allocation mechanism. This improvement lets Windows Server 2008 and Windows Server 2008 R2 reduce to a certain extent the effects of paging out the working set of applications when new memory requests arrive.

The following is an explanation of the improvements from the Microsoft whitepaper "Advances in Memory Management in Windows":

*In Windows Server 2008, the allocation of physically contiguous memory is greatly enhanced. Requests to allocate contiguous memory are much more likely to succeed because the memory manager now dynamically replaces pages, typically without trimming the working set or performing I/O operations. In addition, many more types of pages—such as kernel stacks and file system metadata pages, among others—are now candidates for replacement. Consequently, more contiguous memory is generally available at any given time. In addition, the cost to obtain such allocations is greatly reduced.*

For more information, view [SQL Server Working Set Trim Problems](https://techcommunity.microsoft.com/t5/sql-server-support/sql-server-working-set-trim-problems-consider/ba-p/315462?advanced=false&collapse_discussion=true&search_type=thread).

The third-party products that this article discusses are manufactured by companies that are independent of Microsoft. Microsoft makes no warranty, implied or otherwise, about the performance or reliability of these products.
