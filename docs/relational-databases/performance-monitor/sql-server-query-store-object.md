---
title: "SQL Server, Query Store object"
description: Learn about the Query Store object, which provides counters to monitor resource usage of SQL Server to store query texts, execution plans and runtime stats.
ms.custom: ""
ms.date: "10/01/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Query Store object"
  - "SQL Server:Query Store"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server, Query Store object

 [!INCLUDE [SQL Server 2016](../../includes/applies-to-version/sqlserver2016.md)]

The Query Store object provides counters to monitor resource utilization of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to store query texts, execution plans, and runtime stats for objects such as stored procedures, ad hoc and prepared [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, and triggers.  
  
This table describes are the **SQLServer:Query Store** counters.  
  
|SQL Server Query Store counters|Description|  
|-------------------------------------|-----------------|  
|**Query Store CPU usage**|Execution time of Query Store operations expressed in hundredths of seconds.|  
|**Query Store logical reads**|Number of physical read operations from Query Store.|  
|**Query Store logical writes**|Number of logical write operations from Query Store. The frequency and delay of adding items (that represent runtime stats) to the queue is controlled by Data Flush Interval setting.|  
|**Query Store physical reads**|Number of physical read operations from Query Store.|  
  
 Each counter in the object contains the following instances:  
  
|Query Store instance|Description|  
|--------------------------|-----------------|  
|**_Total**|Information for the Query Store for this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|\<database name>|Query Store information for this database.|  

  
## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%Query Store%';
```  
  
## See also  

- [Monitoring Performance By Using the Query Store](../../relational-databases/performance/monitoring-performance-by-using-the-query-store.md)
- [Query Store Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/query-store-stored-procedures-transact-sql.md)
- [Query Store Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/query-store-catalog-views-transact-sql.md)
- [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
