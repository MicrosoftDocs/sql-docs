---
title: "SQL Server, Advanced Analytics object"
description: "Learn about the SQLServer:Advanced Analytics object, which provides counters to monitor advanced analytics predictive functions."
author: MikeRayMSFT
ms.author: mikeray
ms.date: 12/04/2023
ms.service: sql
ms.subservice: performance
ms.topic: reference
helpviewer_keywords:
  - "Advanced Analytics object"
  - "SQLServer:Advanced Analytics"
dev_langs:
  - "TSQL"
---
# SQL Server, Advanced Analytics object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **SQLServer:Advanced Analytics** object in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] provides counters to monitor the actions associated with executing some analytical syntax. 
  
 This table describes the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] **Advanced Analytics** counters.  
  
|SQL Server Advanced Analytics counters|Description|  
|------------------------------------------|-----------------|  
|**Predictions/sec**|Number of predictions performed using [PREDICT](../../t-sql/queries/predict-transact-sql.md) function.|  


## Examples

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%Advanced Analytics%';
```  
  
## Related content

- [Native scoring using the PREDICT T-SQL function with SQL machine learning](../../machine-learning/predictions/native-scoring-predict-transact-sql.md)
- [Monitor Resource Usage (Performance Monitor)](monitor-resource-usage-system-monitor.md)
