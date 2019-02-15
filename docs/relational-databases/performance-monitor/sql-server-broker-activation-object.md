---
title: "SQL Server, Broker Activation Object | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLServer:Broker Activation"
  - "Broker Activation object"
ms.assetid: cd9b6880-c924-42c7-b333-09c303317c0b
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# SQL Server, Broker Activation Object
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The **SQLServer:BrokerActivation** performance object contains performance counters that report information on stored procedure activation. The table below lists the counters that this object contains.  
  
|SQL Server Broker Activation counters|Description|  
|-------------------------------------------|-----------------|  
|**Stored Procedures Invoked/sec**|This counter reports the total number of activation stored procedures invoked by all queue monitors in the instance per second.|  
|**Task Limit Reached**|This counter reports the total number of times that a queue monitor would have started a new task, but did not because the maximum number of tasks for the queue is already running.|  
|**Task Limit Reached/sec**|This counter reports the number of times per second that a queue monitor would have started a new task, but did not because the maximum number of tasks for the queue is already running.|  
|**Tasks Aborted/sec**|This counter reports the number of activation stored procedure tasks that end with an error, or are aborted by a queue monitor for failing to receive messages.|  
|**Tasks Running**|This counter reports the number of activation stored procedures that are currently running.|  
|**Tasks Started/sec**|This counter reports the number of activation stored procedures started per second by all queue monitors in the instance.|  
  
## See Also  
 [sys.dm_broker_activated_tasks &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-broker-activated-tasks-transact-sql.md)   
 [sys.dm_broker_queue_monitors &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-broker-queue-monitors-transact-sql.md)   
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
  
  
