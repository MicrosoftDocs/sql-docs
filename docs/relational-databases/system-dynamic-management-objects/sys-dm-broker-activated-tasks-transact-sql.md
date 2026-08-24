---
title: "sys.dm_broker_activated_tasks (Transact-SQL)"
description: sys.dm_broker_activated_tasks returns a row for each stored procedure activated by Service Broker.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/16/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sys.dm_broker_activated_tasks_TSQL"
  - "sys.dm_broker_activated_tasks"
  - "dm_broker_activated_tasks_TSQL"
  - "dm_broker_activated_tasks"
helpviewer_keywords:
  - "sys.dm_broker_activated_tasks dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2017 || =azuresqldb-mi-current"
---
# sys.dm_broker_activated_tasks (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns a row for each stored procedure activated by Service Broker.

| Column name | Data type | Nullable | Description |
| --- | --- | --- | --- |
| `spid` | **int** | Yes | ID of the session of the activated stored procedure. |
| `database_id` | **smallint** | Yes | ID of the database in which the queue is defined. |
| `queue_id` | **int** | Yes | ID of the object of the queue for which the stored procedure was activated. |
| `procedure_name` | **nvarchar(325)** | Yes | Name of the activated stored procedure. |
| `execute_as` | **int** | Yes | ID of the user that the stored procedure runs as. |

## Permissions

[!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and previous versions require `VIEW SERVER STATE` permission on the server.

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions require `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Physical joins

:::image type="content" source="media/join-dm-broker-activated-tasks.svg" alt-text="Diagram of physical joins for sys.dm_broker_activated_tasks.":::

## Relationship cardinalities

| From | To | Relationship |
| --- | --- | --- |
| `dm_broker_activated_tasks`.`spid` | `dm_exec_sessions`.`session_id` | One-to-one |

## Related content

- [System dynamic management views and functions](system-dynamic-management-objects.md)
- [Service Broker related dynamic management views (Transact-SQL)](service-broker-related-dynamic-management-views-transact-sql.md)
