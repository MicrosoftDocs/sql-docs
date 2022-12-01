---
title: "SQL Server XTP Phantom Processor object"
description: Learn about the SQL Server XTP Phantom Processor performance object, which contains counters for the In-Memory OLTP engine's phantom processing subsystem.
ms.custom: ""
ms.date: "07/13/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server 2016 XTP Phantom Processor"
  - "SQL Server 2017 XTP Phantom Processor"
  - "SQL Server XTP Phantom Processor"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server XTP Phantom Processor object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **SQL Server XTP Phantom Processor** performance object contains counters related to the In-Memory OLTP engine's phantom processing subsystem. This component is responsible for detecting phantom rows in transactions running at the SERIALIZABLE isolation level, as well as constraint validation in concurrency scenarios.  
  
 This table describes the **SQL Server XTP Phantom Processor** counters.  
  
|Counter|Description|  
|-------------|-----------------|  
|**Dusty corner scan retries/sec (Phantom-issued)**|The number of scan retries due to write conflicts during dusty corner sweeps issued by the phantom processor (on average), per second. This is a very low-level counter, not intended for customer use.|  
|**Phantom expired rows removed/sec**|The number of expired rows removed by phantom scans (on average), per second.|  
|**Phantom expired rows touched/sec**|The number of expired rows touched by phantom scans (on average), per second.|  
|**Phantom expiring rows touched/sec**|The number of expiring rows touched by phantom scans (on average), per second.|  
|**Phantom rows touched/sec**|The number of rows touched by phantom scans (on average), per second.|  
|**Phantom scans started/sec**|The number of phantom scans started (on average), per second.|  
 
## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%XTP Phantom Processor%';
```  
  
## See also  
- [In-Memory OLTP and Memory-Optimization](../in-memory-oltp/overview-and-usage-scenarios.md)
- [SQL Server XTP &#40;In-Memory OLTP&#41; Performance Counters](../../relational-databases/performance-monitor/sql-server-xtp-in-memory-oltp-performance-counters.md)
  
