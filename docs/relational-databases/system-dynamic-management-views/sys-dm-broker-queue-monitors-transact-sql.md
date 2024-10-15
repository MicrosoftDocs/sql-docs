---
title: "sys.dm_broker_queue_monitors (Transact-SQL)"
description: sys.dm_broker_queue_monitors (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "02/24/2023"
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

### Permissions for SQL Server 2022 and later
Requires VIEW SERVER PERFORMANCE STATE permission on the server.

## Examples
### A. Current status queue monitor
This scenario provides the current status of all message queues.

```
SELECT  DB_NAME() AS [Database_Name]
	,sch.[name] AS [Schema_Name]
	,s.[name] AS [Service_Name]
	,q.[name] AS [Queue_Name]
	,m.[state] AS [Queue_State]
	,m.tasks_waiting
	,m.last_activated_time
	,m.last_empty_rowset_time
	,(
		SELECT COUNT(1)
		FROM sys.transmission_queue t6
		WHERE t6.from_service_name = s.[name]
		) AS Tran_Message_Count
FROM sys.services s
JOIN sys.databases d
	ON d.database_id = DB_ID()
JOIN sys.service_queues q
	ON s.service_queue_id = q.[object_id]
JOIN sys.schemas sch
	ON q.[schema_id] = sch.[schema_id]
LEFT JOIN sys.dm_broker_queue_monitors m
	ON q.[object_id] = m.queue_id
	AND m.database_id = d.database_id
```

## See Also
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)
 [Service Broker Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/service-broker-related-dynamic-management-views-transact-sql.md)
