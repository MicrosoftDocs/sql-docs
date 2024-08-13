---
title: "sys.dm_exec_session_wait_stats (Transact-SQL)"
description: sys.dm_exec_session_wait_stats returns information about all the waits encountered by threads that executed for each session.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/08/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_exec_session_wait_stats"
  - "sys.dm_exec_session_wait_stats_tsql"
  - "dm_exec_session_wait_stats"
  - "dm_exec_session_wait_stats_tsql"
helpviewer_keywords:
  - "sys.dm_exec_session_wait_stats"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sys.dm_exec_session_wait_stats (Transact-SQL)

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

Returns information about all the waits encountered by threads that executed for each session. You can use this view to diagnose performance issues with the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] session and also with specific queries and batches. This view returns the same information that is aggregated for [sys.dm_os_wait_stats](sys-dm-os-wait-stats-transact-sql.md), and provides the `session_id` number as well.

| Column name | Data type | Description |
| --- | --- | --- |
| `session_id` | **smallint** | The ID of the session. |
| `wait_type` | **nvarchar(60)** | Name of the wait type. For more information, see [sys.dm_os_wait_stats](sys-dm-os-wait-stats-transact-sql.md). |
| `waiting_tasks_count` | **bigint** | Number of waits on this wait type. This counter is incremented at the start of each wait. |
| `wait_time_ms` | **bigint** | Total wait time for this wait type in milliseconds. This time is inclusive of `signal_wait_time_ms`. |
| `max_wait_time_ms` | **bigint** | Maximum wait time on this wait type. |
| `signal_wait_time_ms` | **bigint** | Difference between the time that the waiting thread was signaled and when it started running. |

## Remarks

This DMV resets the information for a session when the session is opened, or when the session is reset (if connection pooling),

For information about the wait types, see [sys.dm_os_wait_stats](sys-dm-os-wait-stats-transact-sql.md).

## Permissions

For [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and previous versions, if you have `VIEW SERVER STATE` permission on the server, you see all executing sessions on the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]; otherwise, you see only the current session.

For [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, you require `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Related content

- [System Dynamic Management Views](system-dynamic-management-views.md)
- [SQL Server Operating System Related Dynamic Management Views (Transact-SQL)](sql-server-operating-system-related-dynamic-management-views-transact-sql.md)
- [sys.dm_os_wait_stats (Transact-SQL)](sys-dm-os-wait-stats-transact-sql.md)
