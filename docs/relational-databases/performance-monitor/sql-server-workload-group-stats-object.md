---
title: "SQL Server, Workload Group Stats object"
description: Learn about the SQLServer:Workload Group Stats object, which contains performance counters that report about Resource Governor workload group statistics.
ms.custom: ""
ms.date: "07/13/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Workload Group Stats object"
  - "SQLServer: Workload Group Stats"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server, Workload Group Stats object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **SQLServer:Workload Group Stats** object contains performance counters that report information about Resource Governor workload group statistics.  
  
 Each active workload group creates an instance of the **SQLServer:Workload Group Stats** performance object that has the same instance name as the Resource Governor workload group name. The following table describes counters supported on this instance.  

|Counter name|Description|  
|------------------|-----------------|  
|**Active parallel threads**|The current count of parallel threads usage.|  
|**Active requests**|The number of requests that are currently running in this workload group. This should be equivalent to the count of rows from sys.dm_exec_requests filtered by group ID.|  
|**Blocked requests**|The current number of blocked requests in the workload group. This can be used to determine workload characteristics.|  
|**CPU delayed %**|System CPU delayed for all requests in the specified instance of the performance object as a percentage of the total time active.| 
|**CPU delayed % base**|For internal use only.| 
|**CPU effective %**|System CPU usage by all requests in the specified instance of the performance object as a percentage of the total time active.| 
|**CPU effective % base**|For internal use only.| 
|**CPU usage %**|The CPU bandwidth usage by all requests in this workload group measured relative to the computer and normalized to all the CPUs on the system. This value will change as the amount of CPU available to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process changes. It is not normalized to what the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process receives.| 
|**CPU usage % base**|For internal use only.| 
|**CPU violated %**|The difference between the CPU reservation and the effective scheduling percentage.|  
|**Max request CPU time (ms)**|The maximum CPU time, in milliseconds, used by a request currently running in the workload group.|  
|**Max request memory grant (KB)**|The maximum value of memory grant, in kilobytes (KB), for a query.|  
|**Query optimizations/sec**|The number of query optimizations that have happened in this workload group per second. This can be used to determine workload characteristics.|  
|**Queued requests**|The current number of queued requests that is waiting to be picked up. This count can be non-zero if throttling occurs after the GROUP_MAX_REQUESTS limit is reached.|  
|**Reduced memory grants/sec**|The number of queries that are getting less than ideal amount of memory grants per second.|  
|**Requests completed/sec**|The number of requests that have completed in this workload group. This number is cumulative.|  
|**Suboptimal plans/sec**|The number of suboptimal plans that are generated in this workload group per second.|  
  
## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%Workload Group Stats%';
```  

## See also  
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)   
 [SQL Server, Resource Pool Stats Object](../../relational-databases/performance-monitor/sql-server-resource-pool-stats-object.md)   
 [Resource Governor](../../relational-databases/resource-governor/resource-governor.md)  
  
  
