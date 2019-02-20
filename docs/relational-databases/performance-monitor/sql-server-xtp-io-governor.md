---
title: "SQL Server XTP IO Governor | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
ms.assetid: 91e176fe-c838-44e9-b4fc-2814a0551ca3
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# SQL Server XTP IO Governor
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

The SQL Server XTP IO Governor performance object contains counters related to the In-Memory OLTP IO Rate Governor.

This table describes the **SQL Server XTP IO Governor** counters.

|Counter|Description|  
|-------------|-----------------|  
|**Insufficient Credits Waits/sec**|Number of waits due to insufficient credits in the rate objects (per second).|
|**Io Issued/sec**|Number of Io issued per second by flush threads.|
|**Log Blocks/sec**|Number of log blocks processed by controller per second.|
|**Missed Credit Slots**|Number of credit slots missed because of wait for credits from rate object.|
|**Stale Rate Object Waits/sec**|Number of waits due to stale rate objects (per second).|
|**Total Rate Objects Published**|Total number of Rate objects published.|
 

## See Also  
[SQL Server XTP &#40;In-Memory OLTP&#41; Performance Counters](../../relational-databases/performance-monitor/sql-server-xtp-in-memory-oltp-performance-counters.md)
