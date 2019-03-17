---
title: "SQL Server, External Scripts Object | Microsoft Docs"
ms.custom: ""
ms.date: "03/21/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: performance
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "External Scripts object"
  - "SQLServer:External Scripts"
ms.assetid: 8a75ccce-b174-4937-bc92-8e413b55afe1
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# SQL Server, External Scripts Object
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  The **SQLServer:External Scripts** object in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides counters to monitor the actions associated with executing external scripts. For information about executing external scripts, see [sp_execute_external_script &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).  
  
 This table describes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **External Scripts** counters.  
  
|SQL Server External Scripts counters|Description|  
|------------------------------------------|-----------------|  
|**Execution Errors**|The number of errors in executing external scripts.|  
|**Implied Auth. Logins**|The number of logins from satellite processes authenticated by using implied authentication.|  
|**Parallel Executions**|The number of external scripts executed with @parallel = 1.|  
|**SQL CC Executions**|The number of external scripts executed using SQL Compute Context.|  
|**Streaming Executions**|The number of external scripts executed with the @r_rowsPerRead parameter.|  
|**Total Execution Time (ms)**|The total time spent in executing external scripts.|  
|**Total Executions**|The number of external scripts executed.|  
  
## See Also  
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)   
 [sys.resource_governor_external_resource_pools &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-resource-governor-external-resource-pools-transact-sql.md)   
 [sys.dm_resource_governor_external_resource_pool_affinity &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-external-resource-pool-affinity-transact-sql.md)  
  
  
