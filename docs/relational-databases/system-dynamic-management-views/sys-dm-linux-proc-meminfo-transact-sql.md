---
title: "sys.dm_linux_proc_meminfo (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/29/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sys.dm_linux_proc_meminfo"
  - "sys.dm_linux_proc_meminfo_TSQL"
  - "dm_linux_proc_meminfo"
  - "dm_linux_proc_meminfo_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_linux_proc_meminfo dynamic management view"
ms.assetid: a9c5f63c-2526-4c1c-8905-ede06b3e385e
caps.latest.revision: 12
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
ROBOTS: NOINDEX
---
# sys.dm_linux_proc_meminfo (Transact-SQL)
[!INCLUDE[tsql-appliesto-ssLinx-xxxx-xxxx-xxx](../../includes/tsql-appliesto-sslinx-xxxx-xxxx-xxx.md)]

Returns information about memory page allocation when [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] is running on Linux.  
This dmv is based off of the linux `/proc/meminfo` file. It provides information about your system's memory. If a field is missing from the file, it will be set to NULL in the DMV.				

> [!NOTE]  
> Implementation of this DMV has been delayed. Expect it, or something similar in a future CTP.

|Column name |Data type |Description |  
|---------------- |-------------- |---------------- |  
|**memtotal_kb** |**bigint**  |Total usable ram (physical ram minus reserved bits and the kernel binary code) in kilobytes.  |
|**memfree_kb** |**bigint**  |The amount of physical RAM, in kilobytes, left unused by the system.  |
|**memavailable_kb** |**bigint**  |An estimate of how much memory is available for starting new applications, without swapping.  |
|**buffers_kb** |**bigint**  |The amount of physical RAM, in kilobytes, used for file buffers. |
|**cached_kb** |**bigint**  |Memory in the pagecache (diskcache) minus the SwapCache, in kilobytes.  |
|**swapcached_kb** |**bigint** |Cached memory that once was swapped out. When swapped back in it remains in the swapfile. If memory is needed it doesn't need to be swapped out again because it is already in the swapfile. This saves I/O. |
|**active_kb** |**bigint**  |The total amount of buffer or page cache memory, in kilobytes, that is in active use. This is memory that has been recently used and is usually not reclaimed for other purposes.  |
|**inactive_kb** |**bigint**  |The total amount of buffer or page cache memory, in kilobytes, that are free and available. This is memory that has not been recently used and can be reclaimed for other purposes.  |
|**active(anon)_kb** |**bigint** |The amount of anonymous and tmpfs/shmem memory, in kilobytes, that is in active use, or was in active use since the last time the system moved something to swap. |
|**inactive(anon)_kb** |**bigint** |The amount of anonymous and tmpfs/shmem memory, in kilobytes, that is a candidate for eviction. |
|**active(file)_kb** |**bigint** |The amount of file cache memory, in kilobytes, that is in active use, or was in active use since the last time the system reclaimed memory. |
|**inactive(file)_kb** |**bigint** |The amount of file cache memory, in kilobytes, that is newly loaded from the disk, or is a candidate for reclaiming. |
|**unevictable_kb** |**bigint** |The amount of memory, in kilobytes, discovered by the pageout code, that is not evictable because it is locked into memory by user programs. |
|**mlocked_kb** |**bigint** |The total amount of memory, in kilobytes, that is not evictable because it is locked into memory by user programs. |
|**swaptotal_kb** |**bigint**  |The total amount of swap available, in kilobytes.  |
|**swapfree_kb** |**bigint**  |The total amount of swap free, in kilobytes.  |
|**dirty_kb** |**bigint**  |The total amount of memory, in kilobytes, waiting to be written back to the disk. |
|**writeback_kb** |**bigint**  |The total amount of memory, in kilobytes, actively being written back to the disk. |
|**anonpages_kb** |**bigint** |The total amount of memory, in kilobytes, used by pages that are not backed by files and are mapped into userspace page tables. |
|**mapped_kb** |**bigint**  |The total amount of memory, in kilobytes, which have been used to map devices, files, or libraries using the mmap command. |
|**shmem_kb** |**bigint** |The total amount of memory, in kilobytes, used by shared memory (shmem) and tmpfs. |
|**slab_kb** |**bigint**  |The total amount of memory, in kilobytes, used by the kernel to cache data structures for its own use. |
|**sreclaimable_kb** |**bigint** |The part of Slab that can be reclaimed, such as caches. |
|**sunreclaim_kb** |**bigint** |The part of Slab that cannot be reclaimed even when lacking memory. |
|**kernelstack_kb** |**bigint** |The amount of memory, in kilobytes, used by the kernel stack allocations done for each task in the system. |
|**pagetables_kb** |**bigint** |The total amount of memory, in kibibytes, dedicated to the lowest page table level. |
|**nfs_unstable_kb** |**bigint** |The amount, in kibibytes, of NFS pages sent to the server but not yet committed to the stable storage. |
|**bounce_kb** |**bigint** |The amount of memory, in kilobytes, used for the block device "bounce buffers". |
|**writebacktmp_kb** |**bigint** |The amount of memory, in kilobytes, used by FUSE for temporary writeback buffers. |
|**commitlimit_kb** |**bigint** |The total amount of memory currently available to be allocated on the system based on the overcommit ratio (vm.overcommit_ratio). This limit is only adhered to if strict overcommit accounting is enabled (mode 2 in vm.overcommit_memory). |
|**committed_as_kb** |**bigint**  |The total amount of memory, in kilobytes, estimated to complete the workload. This value represents the worst case scenario value, and also includes swap memory.  |
|**vmalloctotal_kb** |**bigint** |The total amount of memory, in kilobytes, of total allocated virtual address space. |
|**vmallocused_kb** |**bigint**  |The total amount of memory, in kilobytes, of used virtual address sp. |
|**vmallocchunk_kb** |**bigint** |The largest contiguous block of memory, in kilobytes, of available virtual address space. |
|**hardwarecorrupted_kb** |**bigint** |The amount of memory, in kibibytes, with physical memory corruption problems, identified by the hardware and set aside by the kernel so it does not get used. |
|**anonhugepages_kb** |**bigint** |The total amount of memory, in kibibytes, used by huge pages that are not backed by files and are mapped into userspace page tables. |
|**cmatotal_kb** |**bigint**  |Total CMA (Contiguous Memory Allocator) pages. (`CONFIG_CMA` is required.)   |
|**cmafree_kb** |**bigint**  |Free CMA (Contiguous Memory Allocator) pages. (`CONFIG_CMA` is required.)  |
|**hugepages_total** |**bigint**  |The total number of hugepages for the system. The number is derived by dividing `Hugepagesize` by the megabytes set aside for `hugepages` specified in `/proc/sys/vm/hugetlb_pool`. This statistic only appears on the x86, Itanium, and AMD64 architectures.  |
|**hugepages_free** |**bigint**  |The total number of `hugepages` available for the system. This statistic only appears on the x86, Itanium, and AMD64 architectures.  |
|**hugepages_rsvd** |**bigint**  |The number of unused `hugepages` reserved for `hugetlbfs`.  |
|**hugepages_surp** |**bigint**  |The number of surplus `hugepages`.  |
|**hugepagesize_kb** |**bigint**  |The size for each `hugepages` unit in kilobytes. By default, the value is 4096 KB on uniprocessor kernels for 32 bit architectures. For SMP, `hugemem` kernels, and AMD64, the default is 2048 KB. For Itanium architectures, the default is 262144 KB. This statistic only appears on the x86, Itanium, and AMD64 architectures.  |
|**directmap4k_kb** |**bigint**  |The amount of memory, in kilobytes, mapped into kernel address space with 4 kB page mappings.  |
|**directmap2m_kb** |**bigint**|The amount of memory, in kilobytes, mapped into kernel address space with 2 MB page mappings. |
|**overflow** |**nvarchar(4000)**|Any unrecognized data we encountered in `/proc/meminfo`. These are in JSON key-value pair format. | 

> [!NOTE]   
> Your Linux documentation may be able to provide additional information. Some of the information for this topic was extracted from **Red Hat®, Inc** documentation at [E.2.18/proc/meminfo](https://access.redhat.com/documentation/Red_Hat_Enterprise_Linux/6/html/Deployment_Guide/s2-proc-meminfo.html).



## Remarks

Returns an empty row set when called on a Windows computer.

## Permissions

Requires `VIEW SERVER STATE` permission.

## Examples   

The following query returns high-level system memory information.

```tsql
SELECT memtotal_kb, memfree_kb, memavailable_kb 
FROM sys.dm_linux_proc_meminfo;
```

## See Also  

[Linux Process Dynamic Management Views (Transact-SQL)](../../relational-databases/system-dynamic-management-views/linux-process-dynamic-management-views-transact-sql.md)   

