---
title: "SQL Server, Batch Resp Statistics Object | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLServer:Batch Resp Statistics"
ms.assetid: a58e8733-6a8d-4b47-b5cb-042e813d808a
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# SQL Server, Batch Resp Statistics Object
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
The **SQLServer:Batch Resp Statistics** performance object provides counters to track SQL Server batch response times.

This following table describes the SQL Server **Batch Resp Statistics** performance objects.


|**SQL Server Batch Resp Statistics counters**|Description|  
|-------------|-----------------|  
|**Batches >=000000ms & \<000001ms**|Number of SQL Batches having response time greater than or equal to 0ms but less than 1ms|
|**Batches >=000001ms & \<000002ms**|Number of SQL Batches having response time greater than or equal to 1ms but less than 2ms|
|**Batches >=000002ms & \<000005ms**|Number of SQL Batches having response time greater than or equal to 2ms but less than 5ms|
|**Batches >=000005ms & \<000010ms**|Number of SQL Batches having response time greater than or equal to 5ms but less than 10ms|
|**Batches >=000010ms & \<000020ms**|Number of SQL Batches having response time greater than or equal to 10ms but less than 20ms|
|**Batches >=000020ms & \<000050ms**|Number of SQL Batches having response time greater than or equal to 20ms but less than 50ms|
|**Batches >=000050ms & \<000100ms**|Number of SQL Batches having response time greater than or equal to 50ms but less than 100ms|
|**Batches >=000100ms & \<000200ms**|Number of SQL Batches having response time greater than or equal to 100ms but less than 200ms|
|**Batches >=000200ms & \<000500ms**|Number of SQL Batches having response time greater than or equal to 200ms but less than 500ms|
|**Batches >=000500ms & \<001000ms**|Number of SQL Batches having response time greater than or equal to 500ms but less than 1,000ms|
|**Batches >=001000ms & \<002000ms**|Number of SQL Batches having response time greater than or equal to 1,000ms but less than 2,000ms|
|**Batches >=002000ms & \<005000ms**|Number of SQL Batches having response time greater than or equal to 2,000ms but less than 5,000ms|
|**Batches >=005000ms & \<010000ms**|Number of SQL Batches having response time greater than or equal to 5,000ms but less than 10,000ms|
|**Batches >=010000ms & \<020000ms**|Number of SQL Batches having response time greater than or equal to 10,000ms but less than 20,000ms|
|**Batches >=020000ms & \<050000ms**|Number of SQL Batches having response time greater than or equal to 20,000ms but less than 50,000ms|
|**Batches >=050000ms & \<100000ms**|Number of SQL Batches having response time greater than or equal to 50,000ms but less than 100,000ms| 
|**Batches >=100000ms**|Number of SQL Batches having response time greater than or equal to 100,000ms| 

Each counter in the object contains the following instances:  
  
|Item|Description|  
|----------|-----------------|  
|**CPU Time:Requests**|The time the CPU spent on the request.|  
|**CPU Time:Total(ms)**|The total time the CPU spent on the batch.|  
|**Elapsed Time:Requests**|The elapsed time of the request.|  
|**Elapsed Time:Total(ms)**|The elapsed time of the batch.|  

## See Also
[SQL Server, Plan Cache Object](../../relational-databases/performance-monitor/sql-server-plan-cache-object.md)  
[Monitor Resource Usage (System Monitor)](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
