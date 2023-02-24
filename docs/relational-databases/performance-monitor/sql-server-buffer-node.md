---
title: "SQL Server:Buffer Node object"
description: Learn about the Buffer Node object, which provides counters to monitor the SQL Server buffer pool page distribution for each NUMA node.
ms.custom: ""
ms.date: "07/12/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLServer:Buffer Node"
  - "Buffer Node object"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server:Buffer Node object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **Buffer Node** object provides counters that complement counters provided by the **Buffer Manager** object. It allows you to monitor the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] buffer pool page distribution for each non-uniform memory access (NUMA) node. There is an instance of the **Buffer Node** object for each NUMA node in use. On non-NUMA architecture, there will be a single instance of the **Buffer Node** object.  
  
## Buffer Node Performance Objects  
 This table describes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **Buffer Node** performance objects.  
  
|**SQL Server Buffer Node** counters|Description|  
|-------------------------------------|-----------------|  
|**Database pages**|Indicates the number of pages in the buffer pool on this node with database content.|  
|**Local node page lookups/sec**|Indicates the number of lookup requests from this node which were satisfied from this node.|  
|**Page life expectancy**|Indicates the minimum number of seconds a page will stay in the buffer pool on this node without references.|  
|**Remote node page lookups/sec**|Indicates the number of lookup requests from this node which were satisfied from other nodes.|  
  
 If SQL Server is running on non-NUMA hardware, the counters of **Buffer Node** and **Buffer Manager** objects should match.  
  
 On NUMA hardware, sums of respective counters of all **Buffer Nodes** should match their counterparts of **Buffer Manager**.  
  
> [!NOTE]  
>  The counter values and sums may not match precisely due to the dynamic nature of the counters and the sampling accuracy.  
  
  
## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%Buffer Node%';
```  

## See also  
 [SQL Server, Buffer Manager Object](../../relational-databases/performance-monitor/sql-server-buffer-manager-object.md)   
 [Server Memory Server Configuration Options](../../database-engine/configure-windows/server-memory-server-configuration-options.md)   
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)   
 [sys.dm_os_performance_counters &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md)  
  
  
