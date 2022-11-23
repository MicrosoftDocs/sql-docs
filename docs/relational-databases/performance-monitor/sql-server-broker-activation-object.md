---
title: "SQL Server, Broker Activation object"
description: Learn about the SQLServer:Broker Activation performance object, which contains performance counters that report information on stored procedure activation. 
ms.custom: ""
ms.date: "07/12/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLServer:Broker Activation"
  - "Broker Activation object"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server, Broker Activation object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **SQLServer:Broker Activation** performance object contains performance counters that report information on stored procedure activation. The table below lists the counters that this object contains.  
  
|**SQL Server Broker Activation** counters|Description|  
|-------------------------------------------|-----------------|  
|**Stored Procedures Invoked/sec**|This counter reports the total number of activation stored procedures invoked by all queue monitors in the instance per second.|  
|**Task Limit Reached**|This counter reports the total number of times that a queue monitor would have started a new task, but did not because the maximum number of tasks for the queue is already running.|  
|**Task Limit Reached/sec**|This counter reports the number of times per second that a queue monitor would have started a new task, but did not because the maximum number of tasks for the queue is already running.|  
|**Tasks Aborted/sec**|This counter reports the number of activation stored procedure tasks that end with an error, or are aborted by a queue monitor for failing to receive messages.|  
|**Tasks Running**|This counter reports the number of activation stored procedures that are currently running.|  
|**Tasks Started/sec**|This counter reports the number of activation stored procedures started per second by all queue monitors in the instance.|  
  
  
## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%Broker Activation%';
```  

## See also  
 - [sys.dm_broker_activated_tasks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-broker-activated-tasks-transact-sql.md)   
 - [sys.dm_broker_queue_monitors &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-broker-queue-monitors-transact-sql.md)   
 - [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
  
  
