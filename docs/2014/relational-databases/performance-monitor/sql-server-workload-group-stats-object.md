---
title: "SQL Server, Workload Group Stats Object | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Workload Group Stats object"
  - "SQLServer: Workload Group Stats"
ms.assetid: ca20e4f6-50ec-4456-900d-87d280fde2b3
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# SQL Server, Workload Group Stats Object
  The SQLServer:Workload Group Stats object contains performance counters that report information about Resource Governor workload group statistics.  
  
 Each active workload group creates an instance of the SQLServer:Workload Group Stats performance object that has the same instance name as the Resource Governor workload group name. The following table describes counters supported on this instance.  
  
|Counter name|Description|  
|------------------|-----------------|  
|Queued requests|The current number of queued requests that is waiting to be picked up. This count can be non-zero if throttling occurs after the GROUP_MAX_REQUESTS limit is reached.|  
|Active requests|The number of requests that are currently running in this workload group. This should be equivalent to the count of rows from sys.dm_exec_requests filtered by group ID.|  
|Requests completed/sec|The number of requests that have completed in this workload group. This number is cumulative.|  
|CPU usage %|The CPU bandwidth usage by all requests in this workload group measured relative to the computer and normalized to all the CPUs on the system. This value will change as the amount of CPU available to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process changes. It is not normalized to what the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] process receives.|  
|Max request CPU time (ms)|The maximum CPU time, in milliseconds, used by a request currently running in the workload group.|  
|Blocked requests|The current number of blocked requests in the workload group. This can be used to determine workload characteristics.|  
|Reduced memory grants/sec|The number of queries that are getting less than ideal amount of memory grants per second.|  
|Max request memory grant (KB)|The maximum value of memory grant, in kilobytes (KB), for a query.|  
|Query optimizations/sec|The number of query optimizations that have happened in this workload group per second. This can be used to determine workload characteristics.|  
|Suboptimal plans/sec|The number of suboptimal plans that are generated in this workload group per second.|  
|Active parallel threads|The current count of parallel threads usage.|  
  
## See Also  
 [Monitor Resource Usage &#40;System Monitor&#41;](monitor-resource-usage-system-monitor.md)   
 [SQL Server, Resource Pool Stats Object](sql-server-resource-pool-stats-object.md)   
 [Resource Governor](../resource-governor/resource-governor.md)  
  
  
