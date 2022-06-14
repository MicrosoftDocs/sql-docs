---
title: "sys.dm_os_job_object (Azure SQL Database)"
description: sys.dm_os_job_object (Azure SQL Database)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/03/2020"
ms.service: sql-database
ms.topic: "reference"
f1_keywords:
  - "sys.dm_db_resource_stats"
  - "sys.dm_db_resource_stats_TSQL"
  - "dm_db_resource_stats"
  - "dm_db_resource_stats_TSQL"
helpviewer_keywords:
  - "sys.dm_db_resource_stats"
  - "dm_db_resource_stats"
dev_langs:
  - "TSQL"
ms.assetid: 6e76b39f-236e-4bbf-b0b5-38be190d81e8
monikerRange: "=azuresqldb-current"
---
# sys.dm_os_job_object (Azure SQL Database)
[!INCLUDE[Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/asdb-asdbmi.md)]

Returns a single row describing the configuration of the job object that manages the SQL Server process, as well as certain resource consumption statistics at the job object level. Returns an empty set if SQL Server is not running in a job object.

A job object is a Windows construct that implements CPU, memory, and IO resource governance at the operating system level. For more information about job objects, see [Job Objects](/windows/desktop/ProcThread/job-objects).
  
|Columns|Data Type|Description|  
|-------------|---------------|-----------------|  
|cpu_rate|**int**|Specifies the portion of processor cycles that SQL Server threads can use during each scheduling interval. The value is reported as a percentage of available cycles within a 10000-cycle scheduling interval, multiplied by the number of logical CPUs. For example, the value 800 on a SQL Server instance with 8 logical CPUs means that threads can use CPUs are their full capacity.|
|cpu_affinity_mask|**bigint**|A bit mask describing which logical processors the SQL Server process can use within the processor group. For example, cpu_affinity_mask 255 (1111 1111 in binary) means that the first eight logical processors can be used. <br /><br />This column is provided for backwards compatibility. It does not report the processor group, and the reported value may be incorrect when a processor group contains more than 64 logical processors. Use the `process_physical_affinity` column to determine processor affinity instead.|
|cpu_affinity_group|**int**|The number of the processor group that is used by SQL Server.|
|memory_limit_mb|**bigint**|The maximum amount of committed memory, in MB, that all processes in the job object, including SQL Server, can use cumulatively.| 
|process_memory_limit_mb |**bigint**|The maximum amount of committed memory, in MB, that a single process in the job object, such as SQL Server, can use.|
|workingset_limit_mb |**bigint**|The maximum amount of memory, in MB, that the SQL Server working set can use.|
|non_sos_mem_gap_mb|**bigint**|The amount of memory, in MB, set aside for thread stacks, DLLs, and other non-SOS memory allocations. SOS target memory is the difference between `process_memory_limit_mb` and `non_sos_mem_gap_mb`.| 
|low_mem_signal_threshold_mb|**bigint**|A memory threshold, in MB. When the amount of available memory for the job object is below this threshold, a low memory notification signal is sent to the SQL Server process. |
|total_user_time|**bigint**|The total number of 100 ns ticks that threads within the job object have spent in user mode, since the job object was created. |
|total_kernel_time |**bigint**|The total number of 100 ns ticks that threads within the job object have spent in kernel mode, since the job object was created. |
|write_operation_count |**bigint**|The total number of write IO operations on local disks issued by SQL Server since the job object was created. |
|read_operation_count |**bigint**|The total number of read IO operations on local disks issued by SQL Server since the job object was created. |
|peak_process_memory_used_mb|**bigint**|The peak amount of memory, in MB, that a single process in the job object, such as SQL Server, has used since the job object was created.| 
|peak_job_memory_used_mb|**bigint**|The peak amount of memory, in MB, that all processes in the job object have used cumulatively since the job object was created.|
|process_physical_affinity|**nvarchar(3072)**|Bit masks describing which logical processors the SQL Server process can use in each processor group. The value in this column is formed by one or more value pairs, each enclosed in curly brackets. In each pair, the first value is the processor group number, and the second value is the affinity bit mask for that processor group. For example, the value `{{0,a}{1,2}}` means that the affinity mask for processor group `0` is `a` (`1010` in binary, indicating that processors 2 and 4 are used), and the affinity mask for processor group `1` is `2` (`10` in binary, indicating that processor 2 is used).|
  
## Permissions  
On SQL Managed Instance, requires `VIEW SERVER STATE` permission. 
On SQL Database, requires the `VIEW DATABASE STATE` permission in the database.  
 
## See Also  

For information on Managed Instances, see [SQL Managed Instance](/azure/sql-database/sql-database-managed-instance).
