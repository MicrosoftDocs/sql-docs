---
title: "SQL Server, Catalog Metadata object"
description: Learn about the SQLServer:Catalog Metadata performance object, which provides counters for catalog metadata for SQL Server.
ms.custom: ""
ms.date: "07/12/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLServer:Catalog Metadata"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server, Catalog Metadata object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The **SQLServer:Catalog Metadata** performance object provides counters for catalog metadata for SQL Server.

This following table describes the SQL Server **Catalog Metadata** performance objects.

|**SQL Server Catalog Metadata counters**|Description|  
|-------------|-----------------|  
|**Cache Entries Count**|Number of entries in the catalog metadata cache.|
|**Cache Entries Pinned Count**|Number of catalog metadata cache entries that are pinned.|
|**Cache Hit Ratio**|Ratio between catalog metadata cache hits and lookups.|
|**Cache Hit Ratio Base**|For internal use only.|

There is one instance of the counter for each database.

## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%Catalog Metadata%';
```  

## See also  
[Monitor Resource Usage (System Monitor)](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)
