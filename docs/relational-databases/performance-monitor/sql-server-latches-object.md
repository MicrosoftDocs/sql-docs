---
title: "SQL Server, Latches object"
description: Learn about the SQLServer:Latches object, which provides counters to monitor internal SQL Server resource locks called latches.
ms.custom: ""
ms.date: "07/13/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Latches object"
  - "SQLServer:Latches"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server, Latches object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **SQLServer:Latches** object in Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides counters to monitor internal [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resource locks called latches. Monitoring the latches to determine user activity and resource usage can help you to identify performance bottlenecks.  
  
 This table describes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **Latches** counters.  
  
|SQL Server Latches counters|Description|  
|---------------------------------|-----------------|  
|**Average Latch Wait Time (ms)**|Average latch wait time (in milliseconds) for latch requests that had to wait.|  
|**Average Latch Wait Time Base**|For internal use only.| 
|**Latch Waits/sec**|Number of latch requests that could not be granted immediately.|  
|**Number of SuperLatches**|Number of latches that are currently SuperLatches.|  
|**SuperLatch Demotions/sec**|Number of SuperLatches that have been demoted to regular latches in the last second.|  
|**SuperLatch Promotions/sec**|Number of latches that have been promoted to SuperLatches in the last second.|  
|**Total Latch Wait Time (ms)**|Total latch wait time (in milliseconds) for latch requests in the last second.|  

## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%Latches%';
```  
  
## See also  
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
  
  
