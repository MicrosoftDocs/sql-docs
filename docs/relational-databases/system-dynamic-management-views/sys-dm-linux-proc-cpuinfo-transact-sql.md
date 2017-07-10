---
title: "sys.dm_linux_proc_cpuinfo (Transact-SQL) | Microsoft Docs"
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
  - "sys.dm_linux_proc_cpuinfo"
  - "sys.dm_linux_proc_cpuinfo_TSQL"
  - "dm_linux_proc_cpuinfo"
  - "dm_linux_proc_cpuinfo_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_linux_proc_cpuinfo dynamic management view"
ms.assetid: 65758974-0992-4512-966f-ce52aa377f65
caps.latest.revision: 12
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
ROBOTS: NOINDEX
---
# sys.dm_linux_proc_cpuinfo (Transact-SQL)
[!INCLUDE[tsql-appliesto-ssLinx-xxxx-xxxx-xxx](../../includes/tsql-appliesto-sslinx-xxxx-xxxx-xxx.md)]

Returns information about CPU allocation when [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] is running on Linux.  
This DMV is based off of the linux `/proc/cpuinfo` file. It provides information about the processors in your system. There will be one row for every logical processor on the machine. If a field is misisng from the file, it will be set to NULL in the DMV.				

> [!NOTE]  
> Implementation of this DMV has been delayed. Expect it, or something similar in a future CTP.

|Column name |Data type |Description |  
|-----------------|---------------|-----------------|  
|**processor** |**bigint** |Provides each processor with an identifying number. On systems that have one processor, only a 0 is present. |
|**vendor_id** |**nvarchar(256)** |The name of the CPU vendor. |
|**cpu_family** |**bigint** |Authoritatively identifies the type of processor in the system. For an Intel-based system, place the number in front of `86` to determine the value. This is particularly helpful for those attempting to identify the architecture of an older system such as a 586, 486, or 386. Because some RPM packages are compiled for each of these particular architectures, this value also helps users determine which packages to install. |
|**model** |**bigint** |An identification number more specific than `cpu_family`. It identifies a specific model of processor. |
|**model_name** |**nvarchar(256)** |Displays the common name of the processor, including its project name. |
|**stepping** |**bigint** |An identification number more specific than "model". It identifies the version or iteration number of processors released under the same or similar model names. |
|**microcode** |**nvarchar(256)** |The version of the processor's firmware. |
|**cpu_mhz** |**float**|Shows the precise speed in megahertz for the processor to the thousandths decimal. |
|**cache_size_kb** |**nvarchar(256)** |Displays the amount of level 2 memory cache available to the processor. |
|**physical_id** |**bigint** |An identification number corresponding to the chip that this core belongs to. On a single processor system, this value will be 0 for all cores. |
|**siblings** |**bigint** |Displays the total number of sibling CPUs on the same physical CPU for architectures which use hyper-threading. |
|**core_id** |**bigint** |An identification number that uniquely identifies a core on a specific physical processor |
|**cpu_cores** |**bigint** |The number of physical cores on this processor. |
|**apicid** |**bigint** |The Advanced Programmable Interrupt Controller ID. It is a unique ID given to each logical processor upon startup. This value can be changed from its initial value. |
|**initial_apicid** |**bigint** |The APICID initially given to this processor upon startup. |
|**fpu** |**nvarchar(256)** |Denotes whether this core is using a hardware floating point unit.  |
|**fpu_exception** |**nvarchar(256)** |The `fpu_exception` field from the `/proc/cpu_info` file. |	
|**cpuid_level** |**bigint** |The `cpuid_level` field from the `/proc/cpu_info` file. |	
|**wp** |**nvarchar(256)** |Denotes whether the Write Protect bit is set for this core. |
|**flags** |**nvarchar(4000)** |Defines a number of different qualities about the processor, such as the presence of a floating point unit (FPU) and the ability to process MMX instructions. |
|**bugs** |**nvarchar(256)** |The bugs field from the `/proc/cpu_info` file. |	
|**bogomips** |**float** |A system constant that is calculated during kernel initialization. |
|**clflush_size** |**bigint** |The smallest block of cache data that can be cache-line flushed. |
|**cache_alignment** |**bigint** |The size of a cache line in this processor. |
|**address_sizes** |**nvarchar(256)** |The sizes in bits of the physical and virtual addresses used by this processor. |
|**power_management** |**nvarchar(256)** |Abbreviations  for the specific power management features supported by this processor. |
|**overflow** |**nvarchar(4000)** |Any unrecognized data we encountered in `/proc/cpuinfo`. These are in JSON key-value pair format. |

> [!NOTE]  
> Your Linux documentation may be able to provide additional information. Some of the information for this topic was extracted from **Red Hat®, Inc** documentation at [E.2.18/proc/cpuinfo](https://access.redhat.com/documentation/Red_Hat_Enterprise_Linux/6/html/Deployment_Guide/s2-proc-cpuinfo.html).

## Remarks

Returns an empty row set when called on a Windows computer.

## Permissions

Requires `VIEW SERVER STATE` permission.

## Examples

The following queries return the number of logical cores on the system and the processor speeds in GHz:  

```
SELECT COUNT(*) AS num_cpus FROM sys.dm_linux_proc_cpuinfo;
SELECT (cpu_mhz / 1000) AS cpu_ghz FROM sys.dm_linux_proc_cpuinfo;
```

## See Also  

[Linux Process Dynamic Management Views (Transact-SQL)](../../relational-databases/system-dynamic-management-views/linux-process-dynamic-management-views-transact-sql.md)   

