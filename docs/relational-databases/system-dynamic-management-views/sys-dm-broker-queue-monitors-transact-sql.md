---
title: "sys.dm_broker_queue_monitors (Transact-SQL)"
description: sys.dm_broker_queue_monitors (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_broker_queue_monitors"
  - "sys.dm_broker_queue_monitors_TSQL"
  - "dm_broker_queue_monitors_TSQL"
  - "sys.dm_broker_queue_monitors"
helpviewer_keywords:
  - "sys.dm_broker_queue_monitors dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: 401207dc-ef4a-4a3f-879c-76dcbb52d6bc
---
# sys.dm_broker_queue_monitors (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns a row for each queue monitor in the instance. A queue monitor manages activation for a queue.  
  

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**database_id**|**int**|Object identifier for the database that contains the queue that the monitor watches. NULLABLE.|  
|**queue_id**|**int**|Object identifier for the queue that the monitor watches. NULLABLE.|  
|**state**|**nvarchar(32)**|State of the monitor. NULLABLE. This is one of the following:<br /><br /> **INACTIVE**<br /><br /> **NOTIFIED**<br /><br /> **RECEIVES_OCCURRING**|  
|**last_empty_rowset_time**|**datetime**|Last time that a RECEIVE from the queue returned an empty result. NULLABLE.|  
|**last_activated_time**|**datetime**|Last time that this queue monitor activated a stored procedure. NULLABLE.|  
|**tasks_waiting**|**int**|Number of sessions that are currently waiting within a RECEIVE statement for this queue. NULLABLE.<br /><br /> Note: This number includes any session executing a receive statement, regardless of whether the queue monitor started the session. This is if you use WAITFOR together with RECEIVE. Basically, these tasks are waiting for messages to arrive on the queue.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## Examples  
  
### A. Current status queue monitor  
 This scenario provides the current status of all message queues.  
  
```  
SELECT t1.name AS [Service_Name],  t3.name AS [Schema_Name],  t2.name AS [Queue_Name],    
CASE WHEN t4.state IS NULL THEN 'Not available'   
ELSE t4.state   
END AS [Queue_State],    
CASE WHEN t4.tasks_waiting IS NULL THEN '--'   
ELSE CONVERT(VARCHAR, t4.tasks_waiting)   
END AS tasks_waiting,   
CASE WHEN t4.last_activated_time IS NULL THEN '--'   
ELSE CONVERT(varchar, t4.last_activated_time)   
END AS last_activated_time ,    
CASE WHEN t4.last_empty_rowset_time IS NULL THEN '--'   
ELSE CONVERT(varchar,t4.last_empty_rowset_time)   
END AS last_empty_rowset_time,   
(   
SELECT COUNT(*)   
FROM sys.transmission_queue t6   
WHERE (t6.from_service_name = t1.name) ) AS [Tran_Message_Count]   
FROM sys.services t1    INNER JOIN sys.service_queues t2   
ON ( t1.service_queue_id = t2.object_id )     
INNER JOIN sys.schemas t3 ON ( t2.schema_id = t3.schema_id )    
LEFT OUTER JOIN sys.dm_broker_queue_monitors t4   
ON ( t2.object_id = t4.queue_id  AND t4.database_id = DB_ID() )    
INNER JOIN sys.databases t5 ON ( t5.database_id = DB_ID() );  
```  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Service Broker Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/service-broker-related-dynamic-management-views-transact-sql.md)  
  
  

