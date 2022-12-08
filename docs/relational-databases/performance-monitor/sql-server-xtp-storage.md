---
title: "SQL Server XTP Storage object"
description: Learn about the SQL Server XTP Storage performance object, which contains counters related to on-disk storage for In-Memory OLTP in SQL Server.
ms.custom: ""
ms.date: "07/13/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server 2016 XTP Storage"
  - "SQL Server 2017 XTP Storage"
  - "SQL Server XTP Storage"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server XTP Storage object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **SQL Server XTP Storage** performance object contains counters related to on-disk storage for In-Memory OLTP in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 This table describes the **SQL Server XTP Storage** counters.  
  
|Counter|Description|  
|-------------|-----------------|  
|**Checkpoints Closed**|Count of checkpoints closed done by online agent.|  
|**Checkpoints Completed**|Count of checkpoints processed by offline checkpoint thread.|  
|**Core Merges Completed**|The number of core merges completed by the merge worker thread. These merges still need to be installed.|  
|**Merge Policy Evaluations**|The number of merge policy evaluations since the server started.|  
|**Merge Requests Outstanding**|The number of merge requests outstanding since the server started.|  
|**Merges Abandoned**|The number of merges abandoned due to failure.|  
|**Merges Installed**|The number of merges successfully installed.|  
|**Total Files Merged**|The total number of source files merged. This count can be used to find the average number of source files in the merge.|  
  
 
## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%XTP Storage%';
``` 

## See also  
- [In-Memory OLTP and Memory-Optimization](../in-memory-oltp/overview-and-usage-scenarios.md)
- [SQL Server XTP &#40;In-Memory OLTP&#41; Performance Counters](../../relational-databases/performance-monitor/sql-server-xtp-in-memory-oltp-performance-counters.md)
