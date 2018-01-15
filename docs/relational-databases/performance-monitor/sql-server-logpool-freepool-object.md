---
title: "SQL Server, LogPool FreePool Object | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine"
ms.service: ""
ms.component: "performance-monitor"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "SQLServer:LogPool FreePool"
ms.assetid: 8ffd569b-045f-4c3f-a473-4a491d6a1d80
caps.latest.revision: 3
author: "dagiro"
ms.author: "v-dagir"
manager: "jhubbard"
ms.workload: "Inactive"
---
# SQL Server, LogPool FreePool Object
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
The **SQLServer:LogPool FreePool** performance object provides counters for statistics for the free pool inside the Log Pool.

This following table describes the SQL Server **LogPool FreePool** performance objects.

|**SQL Server LogPool FreePool counters**|Description|  
|-------------|-----------------|  
|**Free Buffer Refills/sec**|Number of buffers being allocated for refill, per second.|
|**Free List Length**|Length of the free list.|

There is one instance of the counter for each category of log pool.

## See Also  
[Monitor Resource Usage (System Monitor)](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)

