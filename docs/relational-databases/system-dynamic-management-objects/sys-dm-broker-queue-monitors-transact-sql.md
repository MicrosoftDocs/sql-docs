---
title: "sys.dm_broker_queue_monitors (Transact-SQL)"
description: sys.dm_broker_queue_monitors returns a row for each queue monitor in the instance.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/07/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "dm_broker_queue_monitors"
  - "sys.dm_broker_queue_monitors_TSQL"
  - "dm_broker_queue_monitors_TSQL"
  - "sys.dm_broker_queue_monitors"
helpviewer_keywords:
  - "sys.dm_broker_queue_monitors dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2017 || =azuresqldb-mi-current"
---
# sys.dm_broker_queue_monitors (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns a row for each queue monitor in the instance. A queue monitor manages activation for a queue.

| Column name | Data type | Nullable | Description |
| --- | --- | --- | --- |
| `database_id` | **int** | Yes | Object identifier for the database that contains the queue that the monitor watches. |
| `queue_id` | **int** | Yes | Object identifier for the queue that the monitor watches. |
| `state` | **nvarchar(32)** | Yes | State of the monitor. This value is one of the following options:<br /><br />`INACTIVE`<br />`NOTIFIED`<br />`RECEIVES_OCCURRING` |
| `last_empty_rowset_time` | **datetime** | Yes | Last time that a `RECEIVE` from the queue returned an empty result. |
| `last_activated_time` | **datetime** | Yes | Last time that this queue monitor activated a stored procedure. |
| `tasks_waiting` | **int** | Yes | Number of sessions that are currently waiting within a `RECEIVE` statement for this queue.<br /><br />**Note:** This value includes any session executing a receive statement, regardless of whether the queue monitor started the session. It's for when you use `WAITFOR` together with `RECEIVE`. In other words, these tasks are waiting for messages to arrive on the queue. |

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

- [System dynamic management views and functions](system-dynamic-management-objects.md)
- [Service Broker related dynamic management views (Transact-SQL)](service-broker-related-dynamic-management-views-transact-sql.md)
