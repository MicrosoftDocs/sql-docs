---
title: "SQL Server, Memory Broker Clerks Object | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLServer:Memory Broker Clerks"
ms.assetid: 47b9c236-66a3-4c42-97ee-da5555bdc046
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# SQL Server, Memory Broker Clerks Object
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
The **SQLServer:Memory Broker Clerks** performance object provides counters for statistics related to memory broker clerks.

This following table describes the SQL Server **Memory Broker Clerks** performance objects.

|**SQL Server Memory Broker Clerks counters**|Description|  
|-------------|-----------------|  
|**Internal benefit**|The internal value of memory for entry count pressure, in ms per page per ms, multiplied by 10 billion and truncated to an integer.|
|**Memory broker clerk size**|The size of the clerk, in pages.|
|**Periodic evictions (pages)**|The number of pages evicted from the broker clerk by last periodic eviction.|
|**Pressure evictions (pages/sec)**|TThe number of pages per second evicted from the broker clerk by memory pressure.|
|**Simulation benefit**|The value of memory to the clerk, in ms per page per ms, multiplied by 10 billion and truncated to an integer.|
|**Simulation size**|The current size of the clerk simulation, in pages.|

There is an instance of the counter for the buffer pool, and the column store object pool.

## See Also  
[Monitor Resource Usage (System Monitor)](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)
