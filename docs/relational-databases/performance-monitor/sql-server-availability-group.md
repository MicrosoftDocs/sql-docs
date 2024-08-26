---
title: "SQL Server, Availability Group object"
description: "Learn about SQLServer:Availability Group performance object, which contains performance counters about Always On availability groups."
author: MikeRayMSFT
ms.author: mikeray
ms.date: 12/04/2023
ms.service: sql
ms.subservice: performance
ms.topic: reference
helpviewer_keywords:
  - "Availability Groups [SQL Server], monitoring"
  - "performance counters [SQL Server], AlwaysOn Availability Groups"
  - "SQLServer:Availability Group"
  - "Availability Groups [SQL Server], performance counters"
---
# SQL Server, Availability Group object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **SQLServer:Availability Group** performance object contains performance counters that report information about Always On availability groups in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)]. 
  
|Counter Name|Description|  
|------------------|-----------------|  
|**Active Hadr Threads**|Number of active Hadr threads used by AG|  
  
## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%Availability Group%';
```  

  
## Related content

- [Monitor Resource Usage (Performance Monitor)](monitor-resource-usage-system-monitor.md)
- [SQL Server, Availability Replica object](sql-server-availability-replica.md)
- [What is an Always On availability group?](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)
