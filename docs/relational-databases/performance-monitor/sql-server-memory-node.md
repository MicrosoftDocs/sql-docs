---
title: "SQL Server, Memory Node object"
description: Learn about the Memory Node object, which provides counters to monitor server memory usage on NUMA nodes in SQL Server.
ms.custom: ""
ms.date: "07/13/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server, Memory Node object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The **Memory Node** object in Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides counters to monitor server memory usage on NUMA nodes.  
  
## Memory Node Counters  
 This table describes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **Memory Node** counters.  
  
|SQL Server Memory Manager counters|Description|  
|----------------------------------------|-----------------|  
|**Database Node Memory (KB)**|Specifies the amount of memory the server is currently using on this node for database pages.|  
|**Foreign Node Memory (KB)**|Specifies the amount of non NUMA-local memory on this node.|  
|**Free Node Memory (KB)**|Specifies the amount of memory the server is not using on this node.|  
|**Stolen Memory Node (KB)**|Specifies the amount of memory the server is using on this node for purposes other than database pages.|  
|**Target Node Memory**|Specifies the ideal amount of memory for this node.|  
|**Total Node Memory**|Indicates the total amount of memory the server has committed on this node.|  

  
## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%Memory Node%';
```    


## See also  
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)   
 [SQL Server, Buffer Manager Object](../../relational-databases/performance-monitor/sql-server-buffer-manager-object.md)   
 [sys.dm_os_performance_counters &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md)  
  
  
