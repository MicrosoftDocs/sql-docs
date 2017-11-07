---
title: "SQL Server, Memory Broker Clerks Object | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "SQLServer:Memory Broker Clerks"
ms.assetid: 47b9c236-66a3-4c42-97ee-da5555bdc046
caps.latest.revision: 4
author: "dagiro"
ms.author: "v-dagir"
manager: "jhubbard"
---
# SQL Server, Memory Broker Clerks Object
The **SQLServer:Memory Broker Clerks** performance object provides counters for statistics related to memory broker clerks.

This following table describes the SQL Server **Memory Broker Clerks** performance objects.

|**SQL Server Memory Broker Clerks counters**|Description|  
|-------------|-----------------|  
|**Internal benefit**|The internal value of memory for entry count pressure, in ms per page per ms, multiplied by 10 billion and truncated to an integer.|
|**Memory broker clerk size**|The size of the the clerk, in pages.|
|**Periodic evictions (pages)**|The number of pages evicted from the broker clerk by last periodic eviction.|
|**Pressure evictions (pages/sec)**|TThe number of pages per second evicted from the broker clerk by memory pressure.|
|**Simulation benefit**|The value of memory to the clerk, in ms per page per ms, multiplied by 10 billion and truncated to an integer.|
|**Simulation size**|The current size of the clerk simulation, in pages.|

There is an instance of the counter for the buffer pool, and the column store object pool.

## See Also  
[Monitor Resource Usage (System Monitor)](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)