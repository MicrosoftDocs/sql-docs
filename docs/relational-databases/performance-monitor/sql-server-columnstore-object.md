---
title: "SQL Server, Columnstore object"
description: Learn about the SQLServer:Columnstore object, which provides counters to monitor columnstore index execution in SQL Server.
ms.custom: ""
ms.date: "07/12/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server, Columnstore object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]


  The **SQLServer:Columnstore** object provides counters to monitor columnstore index execution in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 The following table describes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **Columnstore** counters.  
  
|**Columnstore** counters|Description|  
|--------------------------|-----------------|  
|**Delta Rowgroups Closed**|Number of delta rowgroups closed.|  
|**Delta Rowgroups Compressed**|Number of delta rowgroups compressed.|  
|**Delta Rowgroups Created**|Number of delta rowgroups created.|  
|**Segment Cache Hit Ratio**|Percentage of column segments that were found in the columnstore pool without having to incur a read from disk.|  
|**Segment Cache Hit Ratio Base**|For internal use only.|
|**Segment Reads/Sec**|Number of physical segment reads issued.|  
|**Total Delete Buffers Migrated**|Number of times tuple mover has cleaned up the delete buffer.|  
|**Total Merge Policy Evaluations**|Number of times the merge policy for columnstore was evaluated.|  
|**Total Rowgroups Compressed**|Total number of rowgroups compressed.|  
|**Total Rowgroups Fit For Merge**|Number of source rowgroups fit for MERGE since the start of SQL Server.|  
|**Total Rowgroups Merge Compressed**|Number of compressed target rowgroups created with MERGE since the start of SQL Server.|  
|**Total Source Rowgroups Merged**|Number of source rowgroups merged since the start of SQL Server.|  

## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%Columnstore%';
```  
  
## See also  
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
  
  
