---
title: "SQL Server, Cursor Manager by Type object"
description: Learn about the SQLServer:Cursor Manager by Type object, which provides counters to monitor cursors, grouped by type.
ms.custom: ""
ms.date: "07/12/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Cursor Manager by Type object"
  - "SQLServer:Cursor Manager by Type"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server, Cursor Manager by Type object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **SQLServer:Cursor Manager by Type** object provides counters to monitor cursors, grouped by type.  
  
 This table describes the SQL Server **Cursor Manager by Type** counters.  
  
|**Cursor Manager by Type** counters|Description|  
|-------------------------------------|-----------------|  
|**Active cursors**|Number of active cursors.|  
|**Cache Hit Ratio**|Ratio between cache hits and lookups.|  
|**Cache Hit Ratio Base**|For internal use only.| 
|**Cached Cursor Counts**|Number of cursors of a given type in the cache.|  
|**Cursor Cache Use Count/sec**|Times each type of cached cursor has been used.|  
|**Cursor memory usage**|Amount of memory consumed by cursors in kilobytes (KB).|  
|**Cursor Requests/sec**|Number of SQL cursor requests received by server.|  
|**Cursor worktable usage**|Number of worktables used by cursors.|  
|**Number of active cursor plans**|Number of cursor plans.|  
  
 Each counter in the object contains the following instances:  
  
|**Cursor Manager** instance|Description|  
|-----------------------------|-----------------|  
|**_Total**|Information for all cursors.|  
|**API Cursor**|Only the API cursor information.|  
|**TSQL Global Cursor**|Only the [!INCLUDE[tsql](../../includes/tsql-md.md)] global cursor information.|  
|**TSQL Local Cursor**|Only the [!INCLUDE[tsql](../../includes/tsql-md.md)] local cursor information.|  

## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%Cursor Manager by Type%';
```  
  
## See also  
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
  
  
