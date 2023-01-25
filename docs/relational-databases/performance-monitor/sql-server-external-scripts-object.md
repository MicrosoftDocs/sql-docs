---
title: "SQL Server, External Scripts object"
description: Learn about the SQLServer:External Scripts object, which provides counters to monitor the actions associated with executing external scripts.
ms.custom: ""
ms.date: "07/13/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "External Scripts object"
  - "SQLServer:External Scripts"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server, External Scripts object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **SQLServer:External Scripts** object in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides counters to monitor the actions associated with executing external scripts. For information about executing external scripts, see [sp_execute_external_script &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).  
  
 This table describes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **External Scripts** counters.  
  
|SQL Server External Scripts counters|Description|  
|------------------------------------------|-----------------|  
|**Execution Errors**|The number of errors in executing external scripts.|  
|**Implied Auth. Logins**|The number of logins from satellite processes authenticated by using implied authentication.|  
|**Parallel Executions**|The number of external scripts executed with @parallel = 1.|  
|**Partition By Executions**|Number of external scripts executed with @input_data_1_partition_by_columns parameter.|
|**SQL CC Executions**|The number of external scripts executed using SQL Compute Context.|  
|**Streaming Executions**|The number of external scripts executed with the @r_rowsPerRead parameter.|  
|**Total Execution Time (ms)**|The total time spent in executing external scripts.|  
|**Total Executions**|The number of external scripts executed.|  


## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%External Scripts%';
```  
  
## See also  
 - [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)   
 - [sys.resource_governor_external_resource_pools &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-resource-governor-external-resource-pools-transact-sql.md)   
 - [sys.dm_resource_governor_external_resource_pool_affinity &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-external-resource-pool-affinity-transact-sql.md)  
  