---
title: "sys.dm_broker_queue_monitors (Transact-SQL)"
description: sys.dm_broker_queue_monitors returns a row for each queue monitor in the instance.
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/15/2024
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

| Column name | Data type | Description |
| --- | --- | --- |
| `database_id` | **int** | Object identifier for the database that contains the queue that the monitor watches. Nullable. |
| `queue_id` | **int** | Object identifier for the queue that the monitor watches. Nullable. |
| `state` | **nvarchar(32)** | State of the monitor. Nullable. This value is one of the following options:<br /><br />`INACTIVE`<br />`NOTIFIED`<br />`RECEIVES_OCCURRING` |
| `last_empty_rowset_time` | **datetime** | Last time that a `RECEIVE` from the queue returned an empty result. Nullable. |
| `last_activated_time` | **datetime** | Last time that this queue monitor activated a stored procedure. Nullable. |
| `tasks_waiting` | **int** | Number of sessions that are currently waiting within a `RECEIVE` statement for this queue. Nullable.<br /><br />**Note:** This number includes any session executing a receive statement, regardless of whether the queue monitor started the session. This is for when you use `WAITFOR` together with `RECEIVE`. In other words, these tasks are waiting for messages to arrive on the queue. |

## Permissions

[!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and earlier versions require `VIEW SERVER STATE` permission on the server.

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions require `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Examples

### A. Current status queue monitor

This scenario provides the current status of all message queues.

```sql
SELECT DB_NAME() AS [Database_Name],
       s.[name] AS [Service_Name],
       sch.[name] AS [Schema_Name],
       q.[name] AS [Queue_Name],
       ISNULL(m.[state], N'Not available') AS [Queue_State],
       m.tasks_waiting,
       m.last_activated_time,
       m.last_empty_rowset_time,
       (SELECT COUNT(1)
        FROM sys.transmission_queue AS t6
        WHERE t6.from_service_name = s.[name]) AS Tran_Message_Count
FROM sys.services AS s
     INNER JOIN sys.databases AS d
         ON d.database_id = DB_ID()
     INNER JOIN sys.service_queues AS q
         ON s.service_queue_id = q.[object_id]
     INNER JOIN sys.schemas AS sch
         ON q.[schema_id] = sch.[schema_id]
     LEFT OUTER JOIN sys.dm_broker_queue_monitors AS m
         ON q.[object_id] = m.queue_id
        AND m.database_id = d.database_id;
```

## Related content

- [System dynamic management views](system-dynamic-management-views.md)
- [Service Broker Related Dynamic Management Views (Transact-SQL)](service-broker-related-dynamic-management-views-transact-sql.md)
