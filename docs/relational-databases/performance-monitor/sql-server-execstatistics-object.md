---
title: "SQL Server, Exec Statistics object"
description: Learn about the SQLServer:Exec Statistics object, which provides counters to monitor various executions.
ms.custom: ""
ms.date: "07/13/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLServer:ExecStatistics"
  - "SQLServer:Exec Statistics"
  - "ExecStatistics object"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server, Exec Statistics object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
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
  

## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%Exec Statistics%';
```  

## See also  
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
  
  
