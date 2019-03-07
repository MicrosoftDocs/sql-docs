---
title: "SQL Server, Memory Node | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
ms.assetid: 55b28ba9-b6d5-4ea9-8103-db8a72f42982
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# SQL Server, Memory Node
  The **Memory Node** object in Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides counters to monitor server memory usage on NUMA nodes.  
  
## Memory Node Counters  
 This table describes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **Memory Node** counters.  
  
|SQL Server Memory Manager counters|Description|  
|----------------------------------------|-----------------|  
|**Database Node Memory (KB)**|Specifies the amount of memory the server is currently using on this node for database pages.|  
|**Free Node Memory (KB)**|Specifies the amount of memory the server is not using on this node.|  
|**Foreign Node Memory (KB)**|Specifies the amount of non NUMA-local memory on this node.|  
|**Stolen Memory Node (KB)**|Specifies the amount of memory the server is using on this node for purposes other than database pages.|  
|**Target Node Memory**|Specifies the ideal amount of memory for this node.|  
|**Total Node Memory**|Indicates the total amount of memory the server has committed on this node.|  
  
## See Also  
 [Monitor Resource Usage &#40;System Monitor&#41;](monitor-resource-usage-system-monitor.md)   
 [SQL Server, Buffer Manager Object](sql-server-buffer-manager-object.md)   
 [sys.dm_os_performance_counters &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql)  
  
  
