---
title: "SQL Server, Batch Resp Statistics object"
description: Learn about the SQLServer:Batch Resp Statistics performance object, which provides counters to track SQL Server batch response times.
ms.custom: ""
ms.date: "07/13/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLServer:Batch Resp Statistics"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# SQL Server, Batch Resp Statistics object
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The **SQLServer:Batch Resp Statistics** performance object provides counters to track SQL Server batch response times.

This following table describes the SQL Server **Batch Resp Statistics** performance objects.


|**SQL Server Batch Resp Statistics** counter|Description|  
|-------------|-----------------|  
|**Batches >=000000ms & \<000001ms**|Number of SQL Batches having response time greater than or equal to 0 ms but less than 1 ms|
|**Batches >=000001ms & \<000002ms**|Number of SQL Batches having response time greater than or equal to 1 ms but less than 2 ms|
|**Batches >=000002ms & \<000005ms**|Number of SQL Batches having response time greater than or equal to 2 ms but less than 5 ms|
|**Batches >=000005ms & \<000010ms**|Number of SQL Batches having response time greater than or equal to 5 ms but less than 10 ms|
|**Batches >=000010ms & \<000020ms**|Number of SQL Batches having response time greater than or equal to 10 ms but less than 20 ms|
|**Batches >=000020ms & \<000050ms**|Number of SQL Batches having response time greater than or equal to 20 ms but less than 50 ms|
|**Batches >=000050ms & \<000100ms**|Number of SQL Batches having response time greater than or equal to 50 ms but less than 100 ms|
|**Batches >=000100ms & \<000200ms**|Number of SQL Batches having response time greater than or equal to 100 ms but less than 200 ms|
|**Batches >=000200ms & \<000500ms**|Number of SQL Batches having response time greater than or equal to 200 ms but less than 500 ms|
|**Batches >=000500ms & \<001000ms**|Number of SQL Batches having response time greater than or equal to 500 ms but less than 1,000 ms|
|**Batches >=001000ms & \<002000ms**|Number of SQL Batches having response time greater than or equal to 1,000 ms but less than 2,000 ms|
|**Batches >=002000ms & \<005000ms**|Number of SQL Batches having response time greater than or equal to 2,000 ms but less than 5,000 ms|
|**Batches >=005000ms & \<010000ms**|Number of SQL Batches having response time greater than or equal to 5,000 ms but less than 10,000 ms|
|**Batches >=010000ms & \<020000ms**|Number of SQL Batches having response time greater than or equal to 10,000 ms but less than 20,000 ms|
|**Batches >=020000ms & \<050000ms**|Number of SQL Batches having response time greater than or equal to 20,000 ms but less than 50,000 ms|
|**Batches >=050000ms & \<100000ms**|Number of SQL Batches having response time greater than or equal to 50,000 ms but less than 100,000 ms| 
|**Batches >=100000ms**|Number of SQL Batches having response time greater than or equal to 100,000 ms| 

Each counter in the object contains the following instances:  
  
|Item|Description|  
|----------|-----------------|  
|**CPU Time:Requests**|The number of requests based on CPU time.|  
|**CPU Time:Total(ms)**|The total time the CPU spent on the batch.|  
|**Elapsed Time:Requests**|The number of requests based on elapsed time.|  
|**Elapsed Time:Total(ms)**|The elapsed time of the batch.|  

  
## Example

You begin to explore the query performance counters in this object using this T-SQL query on the [sys.dm_os_performance_counters](../system-dynamic-management-views/sys-dm-os-performance-counters-transact-sql.md) dynamic management view:

```sql
SELECT * FROM sys.dm_os_performance_counters
WHERE object_name LIKE '%Batch Resp Statistics%';
```  


## See also
[SQL Server, Plan Cache Object](../../relational-databases/performance-monitor/sql-server-plan-cache-object.md)  
[Monitor Resource Usage (System Monitor)](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
