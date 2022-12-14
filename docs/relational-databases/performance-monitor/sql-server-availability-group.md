---
title: "SQL Server, Availability Group object"
description: Learn about SQLServer:Availability Group performance object, which contains performance counters about Always On availability groups.
ms.custom: ""
ms.date: "07/12/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], monitoring"
  - "performance counters [SQL Server], AlwaysOn Availability Groups"
  - "SQLServer:Availability Group"
  - "Availability Groups [SQL Server], performance counters"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server, Availability Group object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **SQLServer:Availability Group** performance object contains performance counters that report information about Always On availability groups in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)]. 
  
|Counter Name|Description|  
|------------------|-----------------|  
|**Active Hadr Threads**|Number of active Hadr threads used by AG|  
  
## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%Availability Group%';
```  

  
## See also 

 - [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)   
 - [Availability Replica Object](../../relational-databases/performance-monitor/sql-server-availability-replica.md)
 - [Always On Availability Groups (SQL Server)](../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md)  
