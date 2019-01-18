---
title: "SQL Server, Resource Pool Stats Object | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Reosurce Pool Stats object"
  - "SQLServer: Resource Pool Stats object"
ms.assetid: bb46e029-fcf9-4aeb-a066-be41e7668fb9
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# SQL Server, Resource Pool Stats Object
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The SQLServer:Resource Pool Stats object contains performance counters that report information about Resource Governor resource pool statistics.  
  
 Each active resource pool creates an instance of the SQLServer:Resource Pool Stats performance object that has the same instance name as the Resource Governor resource pool name. The following table describes counters supported on this instance.  
  
|Counter name|Description|  
|------------------|-----------------|  
|**Active memory grant amount (KB)**|The current total amount, in kilobytes (KB), of granted memory. This information is also available in [sys.dm_exec_query_resource_semaphores](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-resource-semaphores-transact-sql.md).| 
|**Active memory grants count**|Current total count of memory grants. This information is also available in [sys.dm_exec_query_memory_grants](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-memory-grants-transact-sql.md).|  
|**Avg Disk Read IO (ms)**|Average time, in milliseconds, of a read operation from the disk.|  
|**Avg Disk Read IO (ms) Base**|For internal use only.|
|**Avg Disk Write IO (ms)**|Average time, in milliseconds, of a write operation to the disk.|  
|**Avg Disk Write IO (ms) Base**|For internal use only.|
|**Cache memory target (KB)**|The current memory broker target, in kilobytes (KB), for cache.|  
|**Compile memory target (KB)**|The current memory broker target, in kilobytes (KB), for query compiles.|  
|**CPU control effect %**|The effect of Resource Governor on the resource pool. Calculated as (CPU usage %) / (CPU usage % without Resource Governor.|  
|**CPU delayed %**|System CPU delayed for all requests in the specified instance of the performance object as a percentage of the total time active.|
|**CPU delayed % base**|For internal use only.|
|**CPU effective %**|System CPU usage by all requests in the specified instance of the performance object as a percentage of the total time active.|
|**CPU effective % base**|For internal use only.|
|**CPU usage %**|The CPU bandwidth usage by all requests in all workload groups belonging to this pool. This is measured relative to the computer and normalized to all CPUs on the system. This value will change as the amount of CPU available to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process changes. It is not normalized to what the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process receives.|  
|**CPU usage % base**|For internal use only.|
|**CPU usage target %**|The target value of CPU usage % for the resource pool based on the resource pool configuration settings and system load.|  
|**CPU violated %**|The difference between the CPU reservation and the effective scheduling percentage.|
|**Disk Read Bytes/sec**|Number of bytes read from the disk in the last second.|  
|**Disk Read IO Throttled/sec**|Number of read operations throttled in the last second.|  
|**Disk Read IO/sec**|Number of read operations from the disk in the last second.| 
|**Disk Write Bytes/sec**|Number of bytes written to the disk in the last second.|  
|**Disk Write IO Throttled/sec**|Number of write operations throttled in the last second.| 
|**Disk Write IO/sec**|Number of write operations to the disk in the last second.|
|**Max memory (KB)**|The maximum amount, in kilobytes (KB), of memory that the resource pool can have based on the resource pool settings and server state.| 
|**Memory grant timeouts/sec**|The number of memory grant time-outs per second.|
|**Memory grants/sec**|The number of memory grants occurring in this resource pool per second.| 
|**Pending memory grant count**|The number of requests for memory grants pending in the queues. This information is also available in [sys.dm_exec_query_resource_semaphores](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-resource-semaphores-transact-sql.md).|
|**Query exec memory target (KB)**|The current memory broker target, in kilobytes (KB), for query execution memory grant. This information is also available in [sys.dm_exec_query_memory_grants](../../relational-databases/system-dynamic-management-views/sys-dm-exec-query-memory-grants-transact-sql.md).|  
|**Target memory (KB)**|The target amount, in kilobytes (KB), of memory the resource pool is trying to obtain based on the resource pool settings and server state.|   
|**Used memory (KB)**|The amount of memory used, in kilobytes (KB), for the resource pool.|  

  
## See Also  
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)   
 [SQL Server, Workload Group Stats Object](../../relational-databases/performance-monitor/sql-server-workload-group-stats-object.md)   
 [Resource Governor](../../relational-databases/resource-governor/resource-governor.md)  
  
  
