---
title: "Memory Properties | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "LowMemoryLimit property"
  - "MinimumAllocatedMemory property"
  - "MidMemoryPrice property"
  - "MemoryHeapType property"
  - "memory [Analysis Services]"
  - "DefaultPagesCountToReuse property"
  - "TotalMemoryLimit property"
  - "SessionMemoryLimit property"
  - "VirtualMemoryLimit property"
  - "WaitCountIfHighMemory property"
  - "HighMemoryPrice property"
  - "HeapTypeForObjects property"
ms.assetid: 085f5195-7b2c-411a-9813-0ff5c6066d13
author: minewiskan
ms.author: owend
manager: craigg
---
# Memory Properties
  [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] supports the server memory properties listed in the following table. For guidance in setting these properties, see [SQL Server 2008 R2 Analysis Services Operations Guide](https://go.microsoft.com/fwlink/?LinkID=225539).  
  
 Values between 1 and 100 represent percentages of **Total Physical Memory** or **Virtual Address Space**, whichever is less. Values over 100 represent memory limits in bytes.  
  
 **Applies to:** Multidimensional and Tabular server mode, unless noted otherwise.  
  
## Properties  
 `LowMemoryLimit`  
 A signed 64-bit double-precision floating-point number property that defines the point at which the server is low on memory, expressed as percentage of total physical memory. When this limit is reached, the instance will start to slowly clear memory out of caches by closing expired sessions and unloading unused calculations. The server will not release memory below this limit. The default value is 65; which indicates the low memory limit is 65% of physical memory or the virtual address space, whichever is less.  
  
 `TotalMemoryLimit`  
 Defines a threshold that when reached, causes the server to deallocate memory more aggressively. The default value 80% of physical memory or the virtual address space, whichever is less.  
  
 Note that `TotalMemoryLimit` must always be less than `HardMemoryLimit`  
  
 `HardMemoryLimit`  
 Specifies a memory threshold after which the instance aggressively terminates active user sessions to reduce memory usage. All terminated sessions will receive an error about being cancelled by memory pressure. The default value, zero (0), means the `HardMemoryLimit` will be set to a midway value between `TotalMemoryLimit` and the total physical memory of the system; if the physical memory of the system is larger than the virtual address space of the process, then virtual address space will be used instead to calculate `HardMemoryLimit`.  
  
 `VirtualMemoryLimit`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `VertiPaqPagingPolicy`  
 Specifies the paging behavior in the event the server runs low on memory. Valid values are as follows:  
  
 Zero (**0**) disables paging. If memory is insufficient, processing fails with an out-of-memory error. If you disable paging, you must grant Windows privileges to the service account. See [Configure Service Accounts &#40;Analysis Services&#41;](../instances/configure-service-accounts-analysis-services.md) for instructions.  
  
 **1** is the default. This property enables paging to disk using the operating system page file (pagefile.sys).  
  
 When `VertiPaqPagingPolicy` is set to 1, processing is less likely to fail due to memory constraints because the server will try to page to disk using the method that you specified. Setting the `VertiPaqPagingPolicy` property does not guarantee that memory errors will never happen. Out of memory errors can still occur under the following conditions:  
  
-   There is not enough memory for all dictionaries. During processing, Analysis Services locks the dictionaries for each column in memory, and all of these together cannot be more than the value specified for `VertiPaqMemoryLimit`.  
  
-   There is insufficient virtual address space to accommodate the process.  
  
 To resolve persistent out of memory errors, you can either try to redesign the model to reduce the amount of data that needs processing, or you can add more physical memory to the computer.  
  
 Applies to tabular server mode only.  
  
 `VertiPaqMemoryLimit`  
 If paging to disk is allowed, this property specifies the level of memory consumption (as a percentage of total memory) at which paging starts. The default is 60. If memory consumption is less than 60 percent, the server will not page to disk.  
  
 This property depends on the `VertiPaqPagingPolicyProperty`, which must be set to 1 in order for paging to occur.  
  
 Applies to tabular server mode only.  
  
 `HighMemoryPrice`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `MemoryHeapType`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 Applies to multidimensional server mode only.  
  
 `HeapTypeForObjects`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 Applies to multidimensional server mode only.  
  
 `DefaultPagesCountToReuse`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `HandleIA64AlignmentFaults`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `MidMemoryPrice`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `MinimumAllocatedMemory`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `PreAllocate`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `SessionMemoryLimit`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
 `WaitCountIfHighMemory`  
 An advanced property that you should not change, except under the guidance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] support.  
  
## See Also  
 [Configure Server Properties in Analysis Services](server-properties-in-analysis-services.md)   
 [Determine the Server Mode of an Analysis Services Instance](../instances/determine-the-server-mode-of-an-analysis-services-instance.md)  
  
  
