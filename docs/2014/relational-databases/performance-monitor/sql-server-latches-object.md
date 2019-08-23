---
title: "SQL Server, Latches Object | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Latches object"
  - "SQLServer:Latches"
ms.assetid: 2393ea1c-2bf3-41c3-9f37-b9761144eeca
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# SQL Server, Latches Object
  The **SQLServer:Latches** object in Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides counters to monitor internal [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resource locks called latches. Monitoring the latches to determine user activity and resource usage can help you to identify performance bottlenecks.  
  
 This table describes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **Latches** counters.  
  
|SQL Server Latches counters|Description|  
|---------------------------------|-----------------|  
|**Average Latch Wait Time (ms)**|Average latch wait time (in milliseconds) for latch requests that had to wait.|  
|**Latch Waits/sec**|Number of latch requests that could not be granted immediately.|  
|**Number of SuperLatches**|Number of latches that are currently SuperLatches.|  
|**SuperLatch Demotions/sec**|Number of SuperLatches that have been demoted to regular latches in the last second.|  
|**SuperLatch Promotions/sec**|Number of latches that have been promoted to SuperLatches in the last second.|  
|**Total Latch Wait Time (ms)**|Total latch wait time (in milliseconds) for latch requests in the last second.|  
  
## See Also  
 [Monitor Resource Usage &#40;System Monitor&#41;](monitor-resource-usage-system-monitor.md)  
  
  
