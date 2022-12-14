---
title: "SQL Server XTP Transaction Log object"
description: Learn about the SQL Server XTP Transaction Log performance object, which contains counters related to In-Memory OLTP transaction log activity in SQL Server.
ms.custom: ""
ms.date: "07/13/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server 2016 XTP Transaction Log"
  - "SQL Server 2017 XTP Transaction Log"
  - "SQL Server XTP Transaction Log"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server XTP Transaction Log object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **SQL Server XTP Transaction Log** performance object contains counters related to In-Memory OLTP transaction log activity in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 This table describes the **SQL Server XTP Transaction Log** counters.  
  
|Counter|Description|  
|-------------|-----------------|  
|**Log bytes written/sec**|The number of bytes written to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] transaction log by the In-Memory OLTP engine (on average), per second.|  
|**Log records written/sec**|The number of records written to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] transaction log by the In-Memory OLTP engine (on average), per second.|  
  
 
## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%XTP Transaction Log%';
``` 

## See also  
- [In-Memory OLTP and Memory-Optimization](../in-memory-oltp/overview-and-usage-scenarios.md)
- [SQL Server XTP &#40;In-Memory OLTP&#41; Performance Counters](../../relational-databases/performance-monitor/sql-server-xtp-in-memory-oltp-performance-counters.md)
  
