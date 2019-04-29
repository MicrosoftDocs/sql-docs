---
title: "SQL Server:Buffer Node | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLServer:Buffer Node"
  - "Buffer Node object"
ms.assetid: fd3f9f0f-7c38-4cfd-a0c5-ee93dd52d9a5
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# SQL Server:Buffer Node
  The **Buffer Node** object provides counters that complement counters provided by the **Buffer Manager** object. It allows you to monitor the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] buffer pool page distribution for each non-uniform memory access (NUMA) node. There is an instance of the **Buffer Node** object for each NUMA node in use. On non-NUMA architecture, there will be a single instance of the **Buffer Node** object.  
  
## Buffer Node Performance Objects  
 This table describes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **Buffer Node** performance objects.  
  
|SQL Server Buffer Node counters|Description|  
|-------------------------------------|-----------------|  
|**Database pages**|Indicates the number of pages in the buffer pool on this node with database content.|  
|**Page life expectancy**|Indicates the minimum number of seconds a page will stay in the buffer pool on this node without references.|  
|**Local Node page lookups/sec**|Indicates the number of lookup requests from this node which were satisfied from this node.|  
|**Remote Note page lookups/sec**|Indicates the number of lookup requests from this node which were satisfied from other nodes.|  
  
 If SQL Server is running on non-NUMA hardware, the counters of **Buffer Node** and **Buffer Manager** objects should match.  
  
 On NUMA hardware, sums of respective counters of all **Buffer Nodes** should match their counterparts of **Buffer Manager**.  
  
> [!NOTE]  
>  The counter values and sums may not match precisely due to the dynamic nature of the counters and the sampling accuracy.  
  
## See Also  
 [SQL Server, Buffer Manager Object](sql-server-buffer-manager-object.md)   
 [Server Memory Server Configuration Options](../../database-engine/configure-windows/server-memory-server-configuration-options.md)   
 [Monitor Resource Usage &#40;System Monitor&#41;](monitor-resource-usage-system-monitor.md)   
 [sys.dm_os_performance_counters &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql)  
  
  
