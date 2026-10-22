---
title: "sys.server_event_sessions (Transact-SQL)"
description: sys.server_event_sessions lists all the server-scoped event session definitions that exist in SQL Server or Azure SQL Managed Instance.
author: dimitri-furman
ms.author: dfurman
ms.date: 07/28/2025
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "server_event_sessions"
  - "server_event_sessions_TSQL"
  - "sys.server_event_sessions_TSQL"
  - "sys.server_event_sessions"
helpviewer_keywords:
  - "sys.server_event_sessions catalog view"
  - "xe"
dev_langs:
  - "TSQL"
---

# sys.server_event_sessions (Transact-SQL)

[!INCLUDE [SQL Server SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

Lists all the server-scoped event session definitions that exist in SQL Server or Azure SQL Managed Instance.

> [!NOTE]  
> Azure SQL Database supports only database-scoped event sessions. See the related view, [sys.database_event_sessions](sys-database-event-sessions-azure-sql-database.md).

| Column name | Data type | Description |
| --- | --- | --- |
| `event_session_id` | **int** | The unique ID of the event session. Not nullable. |
| `name` | **sysname** | The user-defined name for identifying the event session. name is unique. Not nullable. |
| `event_retention_mode` | **nchar(1)** | Determines how event loss is handled. The default is `S`. Not nullable. Can be one of the following values:<br /><br />`S`. Maps to `event_retention_mode_desc = ALLOW_SINGLE_EVENT_LOSS`<br /><br />`M`. Maps to `event_retention_mode_desc = ALLOW_MULTIPLE_EVENT_LOSS`<br /><br />`N`. Maps to `event_retention_mode_desc = NO_EVENT_LOSS` |
| `event_retention_mode_desc` | **sysname** | Describes how event loss is handled. The default is `ALLOW_SINGLE_EVENT_LOSS`. Not nullable. Can be one of the following values:<br /><br />`ALLOW_SINGLE_EVENT_LOSS`. Events can be lost from the session. Single events are dropped only when all event buffers are full. Losing single events when buffers are full allows for acceptable [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] performance characteristics, while minimizing the loss in the processed event stream.<br /><br />`ALLOW_MULTIPLE_EVENT_LOSS`. Full event buffers can be lost from the session. The number of events lost depends on the memory size allocated to the session, the partitioning of the memory, and the size of the events in the buffer. This option minimizes the performance impact on the server when event buffers are quickly filled. However, large numbers of events can be lost from the session.<br /><br />`NO_EVENT_LOSS`. No event loss is allowed. This option ensures that all events raised are retained. Using this option forces all the tasks that fire events to wait until space is available in an event buffer. This might lead to detectable performance degradation while the event session is active. |
| `max_dispatch_latency` | **int** | The amount of time, in milliseconds, that events are buffered in memory before they're served to session targets. Valid values are from 0 to 2,147,483,648, and 0. A value of `0` indicates that dispatch latency is infinite. Nullable. |
| `max_memory` | **int** | The amount of memory allocated to the session for event buffering. The default value is 4 MB. Nullable. |
| `max_event_size` | **int** | The amount of memory set aside for events that don't fit in event session buffers. If `max_event_size` exceeds the calculated buffer size, two additional buffers of `max_event_size` are allocated to the event session. Nullable. |
| `memory_partition_mode` | **nchar(1)** | The location in memory where event buffers are created. The default partition mode is `G`. Not nullable. `memory_partition_mode` is one of:<br /><br />`G` - `NONE`<br />`C` - `PER_CPU`<br />`N` - `PER_NODE` |
| `memory_partition_mode_desc` | **sysname** | The default is `NONE`. Not nullable. Can be one of the following values:<br /><br />`NONE`. A single set of buffers are created within a SQL Server instance.<br /><br />`PER_CPU`. A set of buffers is created for each CPU.<br /><br />`PER_NODE`. A set of buffers is created for each non-uniform memory access (NUMA) node. |
| `track_causality` | **bit** | Enable or disable causality tracking. If set to `1` (`ON`), tracking is enabled and related events on different server connections can be correlated. The default setting is `0` (`OFF`). Not nullable. |
| `startup_state` | **bit** | Value determines whether or not session is started automatically when the server starts. The default is `0`. Not nullable. Can be one of:<br /><br />`0` (`OFF`). The session doesn't start when the server starts.<br /><br />`1` (`ON`). The event session starts when the server starts. |
| `max_duration` | **bigint** | **Applies to**: [!INCLUDE [ssazure-sqlmi-autd](../../includes/applies-to-version/ssazure-sqlmi-autd.md)], [!INCLUDE[sssql25-md](../../includes/sssql25-md.md)] and later versions.<br /><br />The value that determines the maximum duration of an event session once it's started, in seconds. Set to `0` when `MAX_DURATION` is not specified or is set to `UNLIMITED`. For more information, see [Time-bound event sessions](../extended-events/sql-server-extended-events-sessions.md#time-bound-event-sessions). |

## Permissions

[!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and previous versions require `VIEW SERVER STATE` permission on the server.

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions require `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Related content

- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [Extended Events Catalog Views (Transact-SQL)](extended-events-catalog-views-transact-sql.md)
- [Extended Events overview](../extended-events/extended-events.md)
