---
title: "SQL Server, ExecStatistics Object | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLServer:ExecStatistics"
  - "ExecStatistics object"
ms.assetid: 4f8557a8-345f-4622-a8a5-763a0388ad94
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# SQL Server, ExecStatistics Object
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The **SQLServer:ExecStatistics** object in Microsoft SQL Server provides counters to monitor various executions.  
  
 This table describes the SQL Server **Exec Statistics** counters.  
  
|SQL Server Exec Statistics counters|Description|  
|-----------------------------------------|-----------------|  
|**Distributed Query**|Statistics relevant to execution of distributed queries.|  
|**DTC calls**|Statistics relevant to execution of DTC calls.|  
|**Extended Procedures**|Statistics relevant to execution of extended procedures.|  
|**OLEDB calls**|Statistics relevant to execution of OLEDB calls.|  
  
 Each counter in the object contains the following instances:  
  
|Item|Description|  
|----------|-----------------|  
|**Average execution time (ms)**|Average execution time of the selected type of execution.|  
|**Cumulative execution time (ms) per second**|Aggregated execution time per second, of the selected type of execution.|  
|**Execs in progress**|Number of execs in progress of the selected type of execution.|  
|**Exec started per second**|Number of exes started per second of the selected type of execution.|  
  
## See Also  
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
  
  
