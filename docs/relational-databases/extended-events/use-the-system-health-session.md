---
title: Use the system_health session
description: The system_health Extended Events session is included with SQL Server. This session collects system data to troubleshoot database engine performance.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 10/22/2023
ms.service: sql
ms.subservice: xevents
ms.topic: tutorial
helpviewer_keywords:
  - "extended events [SQL Server], system health session"
  - "extended events [SQL Server], system_health session"
  - "system_health session [SQL Server extended events]"
  - "system health session [SQL Server extended events]"
monikerRange: ">=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Use the system_health session

[!INCLUDE [SQL Server SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

The `system_health` session is an Extended Events session that is included by default with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and Azure SQL Managed Instance. This session starts automatically when the [!INCLUDE [ssDE](../../includes/ssde-md.md)] starts, and runs without any noticeable performance overhead. The session collects system data that you can use to help troubleshoot performance issues in the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

> [!IMPORTANT]  
> We recommend that you don't stop, alter, or delete the `system_health` session. Any changes made to the `system_health` session settings might be overwritten by a future product update.

The session collects information that includes the following information:

- The `sql_text` and `session_id` for any sessions that encounter an error that has a severity >= 20.
- The `sql_text` and `session_id` for any sessions that encounter a memory-related error. The errors include 17803, 701, 802, 8645, 8651, 8657 and 8902.
- A record of any non-yielding scheduler problems. These appear in the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] error log as error 17883.
- Any deadlocks that are detected, including the deadlock graph.
- The `callstack`, `sql_text`, and `session_id` for any sessions that have waited on latches (or other interesting resources) for > 15 seconds.
- The `callstack`, `sql_text`, and `session_id` for any sessions that have waited on locks for > 30 seconds.
- The `callstack`, `sql_text`, and `session_id` for any sessions that have waited for a long time for preemptive waits. The duration varies by wait type. A preemptive wait is where [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is waiting for external API calls.
- The `callstack` and `session_id` for CLR allocation and virtual allocation failures.
- The ring buffer events for the memory broker, scheduler monitor, memory node OOM, security, and connectivity.
- System component results from `sp_server_diagnostics`.
- Instance health collected by `scheduler_monitor_system_health_ring_buffer_recorded`.
- CLR Allocation failures.
- Connectivity errors using `connectivity_ring_buffer_recorded`.
- Security errors using `security_error_ring_buffer_recorded`.

> [!NOTE]  
> For more information on deadlocks, see the [Deadlocks guide](../sql-server-deadlocks-guide.md).
> For more information on SQL error messages, see [Database Engine events and errors](../errors-events/database-engine-events-and-errors.md).

## View the system_health session data

The session uses both the ring buffer target and event file target to store the data. The event file target is configured with a maximum size of 5 MB and a file retention policy of 4 files.

To view the session data from the ring buffer target with the Extended Events user interface available in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], see [Advanced Viewing of Target Data from Extended Events in SQL Server - Watch live data](../../relational-databases/extended-events/advanced-viewing-of-target-data-from-extended-events-in-sql-server.md#watch-live-data).

To view the session data from the ring buffer target with [!INCLUDE [tsql](../../includes/tsql-md.md)], use the following query:

```sql
SELECT CAST(xet.target_data as xml) AS target_data
FROM sys.dm_xe_session_targets xet
JOIN sys.dm_xe_sessions xe
ON xe.address = xet.event_session_address
WHERE xe.name = 'system_health'
```

To view the session data from the event file, use the Extended Events event viewer UI available in [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For more information, see [View event data in SQL Server Management Studio](advanced-viewing-of-target-data-from-extended-events-in-sql-server.md).

## Restore the system_health session

If you delete the `system_health` session, you can restore it by executing the `u_tables.sql` script. This file is located in the following folder, where `C:` represents the drive where you installed the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] program files, and `MSSQLnn` the major version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]:

`C:\Program Files\Microsoft SQL Server\MSSQLnn.\<instanceid>\MSSQL\Install`

After you restore the session, you must start it by using the `ALTER EVENT SESSION` statement or by using the **Extended Events** node in Object Explorer. Otherwise, the session starts automatically the next time that you restart the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service.

## The system_health session in Azure SQL

In [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)], the `system_health` session data can be viewed by right-clicking either `event_file` or `ring_buffer` target in Object Explorer, and selecting **View Target Data**.

There's no built-in `system_health` Extended Event session in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], but you can use the `sys.fn_xe_file_target_read_file()` function to read from Extended Event sessions you create yourself and store in Azure Storage. For a walkthrough, see [Event File target code for Extended Events in Azure SQL Database](/azure/azure-sql/database/xevent-code-event-file).

## Related content

- [Extended Events overview](extended-events.md)
- [Database Engine events and errors](../errors-events/database-engine-events-and-errors.md)
- [Messages (for errors) Catalog Views - sys.messages](../system-catalog-views/messages-for-errors-catalog-views-sys-messages.md)
- [Extended Events Tools](extended-events-tools.md)
- [sys.fn_xe_file_target_read_file (Transact-SQL)](../system-functions/sys-fn-xe-file-target-read-file-transact-sql.md)
