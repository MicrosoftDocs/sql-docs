---
title: Extended Events Overview - SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL Database in Fabric
description: Extended Events is a lightweight feature that collects detailed data necessary to monitor and troubleshoot database engine performance in SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Fabric.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest, dfurman
ms.date: 10/02/2025
ms.service: sql
ms.subservice: xevents
ms.topic: overview
ms.custom:
  - intro-overview
  - ignite-2025
helpviewer_keywords:
  - "extended events [SQL Server]"
  - "xe"
  - "XEvents"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# Extended Events overview

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

The Extended Events (XEvents) architecture enables users to collect as much or as little data as is necessary to monitor, identify, or troubleshoot performance in SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Fabric. Extended Events is highly configurable, lightweight, and scales well. For more information, see [Extended Events Architecture](#extended-events-architecture).

Extended Events replace the deprecated [SQL Trace](../sql-trace/sql-trace.md) and SQL Server Profiler features.

To get started with Extended Events, use [Quickstart: Extended Events](quick-start-extended-events-in-sql-server.md).

> [!NOTE]  
> For Azure SQL Database, SQL database in Fabric, and SQL Managed Instance, [code examples can differ](#code-examples-can-differ-for-azure-sql-database-and-sql-managed-instance) because the files for the `event_file` target are stored in Azure Storage. For more information, see [Extended Events in Azure SQL](/azure/azure-sql/database/xevent-db-diff-from-svr).

## Benefits of Extended Events

Extended Events is a lightweight performance monitoring system that uses minimal system resources while providing a detailed, in-depth view of the database engine. SQL Server Management Studio provides a graphical user interface for Extended Events to create, modify, and drop event sessions and to display and analyze session data. To learn more about Extended Events support in Management Studio, see:

- [Manage Event Sessions in the Object Explorer](manage-event-sessions-in-the-object-explorer.md)
- [Use the SSMS XEvent Profiler](use-the-ssms-xe-profiler.md)

## Extended Events concepts

Extended Events builds on the existing concepts from Event Tracing for Windows (ETW), such as *event* and *event consumer*, and introduces new concepts such as *action* and *predicate*.

The following table provides documentation references to understand the concepts in Extended Events.

| Article | Description |
| --- | --- |
| [Extended Events packages](sql-server-extended-events-packages.md) | Describes the Extended Events packages that contain objects. These objects are used to obtain and process data when an Extended Events session is running. |
| [Extended Events targets](targets-for-extended-events-in-sql-server.md) | Describes the event consumers that can receive data during an event session. |
| [Extended Events engine](sql-server-extended-events-engine.md) | Describes the engine that implements and manages an Extended Events session. |
| [Extended Events sessions](sql-server-extended-events-sessions.md) | Describes the Extended Events session. |

## Extended Events architecture

Extended Events is a name for a general event-handling system for server systems. The Extended Events infrastructure supports the correlation of data from the database engine, and under certain conditions, the correlation of data from the operating system and database applications. In the operating system case, Extended Events output must be directed to [Event Tracing for Windows (ETW)](/windows/win32/etw/event-tracing-portal). ETW can correlate the event data with operating system or application event data.

All applications have execution points that are useful both inside and outside an application. Inside the application, asynchronous processing can be enqueued using information that is collected during the initial execution of a task. Outside the application, execution points provide monitoring utilities with information. The information is about the behavioral and performance characteristics of the monitored application.

Extended Events supports using event data outside a process. This data is typically used by users either administering or supporting a product by doing performance monitoring or by user developing applications on a product for debugging purposes. Data is consumed or analyzed using tools such as SQL Server Management Studio, XEvent Profiler and Performance Monitor, and T-SQL or Windows command line tools.

Extended Events has the following key design aspects:

- The Extended Events engine is event agnostic. The engine can bind any event to any target, because the engine isn't constrained by event content. For more information about the Extended Events engine, see [Extended Events engine](sql-server-extended-events-engine.md).
- Events are separated from event consumers, which are called *targets* in Extended Events. This means that any target can receive any event. In addition, any event that is raised can be automatically consumed by the target, which can log or provide additional event context. For more information, see [Extended Events targets](targets-for-extended-events-in-sql-server.md).
- Events are distinct from the action to take when an event occurs. Therefore, any action can be associated with any event.
- Predicates can dynamically filter when event data should be captured. Dynamic filtering adds to the flexibility of the Extended Events infrastructure. For more information, see [Extended Events packages](sql-server-extended-events-packages.md).

Extended Events can synchronously generate event data (and asynchronously process that data), which provides a flexible solution for event handling. In addition, Extended Events provides the following features:

- A unified approach to handling events across the server system, while enabling users to isolate specific events for troubleshooting purposes.
- Integration with, and support for existing ETW tools.
- A fully configurable event handling mechanism that uses [!INCLUDE [tsql](../../includes/tsql-md.md)].
- The ability to dynamically monitor active processes, while having minimal effect on those processes.
- A default system health session that runs without any noticeable performance effects. The session collects system data that you can use to help troubleshoot performance issues. For more information, see [Use the system_health session](use-the-system-health-session.md).

## Extended Events tasks

Using [!INCLUDE [ssManStudio](../../includes/ssmanstudio-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)] to execute [!INCLUDE [tsql](../../includes/tsql-md.md)] Data Definition Language (DDL) statements, consume dynamic management views and functions, or catalog views, you can create simple or complex [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Extended Events troubleshooting solutions for your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] environment.

| Task description | Article |
| --- | --- |
| Use the **Object Explorer** to manage event sessions. | [Manage Event Sessions in the Object Explorer](manage-event-sessions-in-the-object-explorer.md) |
| Describes how to use available Extended Events targets. | [Extended Events targets](targets-for-extended-events-in-sql-server.md) |
| Describes how to view and refresh target data. | [View event data in SQL Server Management Studio](advanced-viewing-of-target-data-from-extended-events-in-sql-server.md) |
| Describes the architecture of Extended Events sessions. | [Extended Events sessions](sql-server-extended-events-sessions.md) |
| Describes how to use Extended Events tools to create and manage your Extended Events sessions. | [Extended Events Tools](extended-events-tools.md) |
| Describes how to alter an Extended Events session. | [Alter an Extended Events Session](alter-an-extended-events-session.md) |
| Describes how to get information about the fields associated with the events. | [Get the Fields for All Events](/previous-versions/sql/sql-server-2016/bb677249(v=sql.130)) |
| Describes how to find out what events are available in the registered packages. | [SELECTs and JOINs From System Views for Extended Events](selects-and-joins-from-system-views-for-extended-events-in-sql-server.md) |
| Describes how to view the Extended Events events and actions that are equivalent to each SQL Trace event and its associated columns. | [View the Extended Events Equivalents to SQL Trace Event Classes](view-the-extended-events-equivalents-to-sql-trace-event-classes.md) |
| Describes how to convert an existing SQL Trace script to an Extended Events session. | [Convert an Existing SQL Trace Script to an Extended Events Session](convert-an-existing-sql-trace-script-to-an-extended-events-session.md) |
| Describes how to determine which queries are holding the lock, the plan of the query, and the [!INCLUDE [tsql](../../includes/tsql-md.md)] stack at the time the lock was taken. | [Determine Which Queries Are Holding Locks](determine-which-queries-are-holding-locks.md) |
| Describes how to identify the source of locks. | [Find the Objects That Have the Most Locks Taken on Them](find-the-objects-that-have-the-most-locks-taken-on-them.md) |
| Describes how to use Extended Events with Event Tracing for Windows to monitor system activity. | [Monitor System Activity Using Extended Events](monitor-system-activity-using-extended-events.md) |
| Using Catalog Views and Dynamic Management Views (DMVs) for Extended Events | [SELECTs and JOINs From System Views for Extended Events](selects-and-joins-from-system-views-for-extended-events-in-sql-server.md) |

## Extended Events catalog views

Extended Events provides several [catalog views](../system-catalog-views/catalog-views-transact-sql.md). Catalog views tell you about event session *metadata* or *definition*. For information about instances of active event sessions, see [Extended Events dynamic management views](#extended-events-dynamic-management-views).

# [Azure SQL Database and SQL database in Fabric](#tab/sqldb)

| Name of catalog view | Description |
| --- | --- |
| [sys.database_event_session_actions](../system-catalog-views/sys-database-event-session-actions-azure-sql-database.md) | Returns a row for each action on each event of a database-scoped event session. |
| [sys.database_event_session_events](../system-catalog-views/sys-database-event-session-events-azure-sql-database.md) | Returns a row for each event in a database-scoped event session. |
| [sys.database_event_session_fields](../system-catalog-views/sys-database-event-session-fields-azure-sql-database.md) | Returns a row for each customizable column that was explicitly set on events and targets of a database-scoped session. |
| [sys.database_event_session_targets](../system-catalog-views/sys-database-event-session-targets-azure-sql-database.md) | Returns a row for each event target for a database-scoped event session. |
| [sys.database_event_sessions](../system-catalog-views/sys-database-event-sessions-azure-sql-database.md) | Returns a row for each database-scoped event session. |

# [Azure SQL Managed Instance](#tab/sqlmi)

| Name of catalog view | Description |
| --- | --- |
| [sys.server_event_session_actions](../system-catalog-views/sys-server-event-session-actions-transact-sql.md) | Returns a row for each action on each event of a server-scoped event session. |
| [sys.server_event_session_events](../system-catalog-views/sys-server-event-session-events-transact-sql.md) | Returns a row for each event in a server-scoped event session. |
| [sys.server_event_session_fields](../system-catalog-views/sys-server-event-session-fields-transact-sql.md) | Returns a row for each customizable column that was explicitly set on events and targets of a server-scoped event session. |
| [sys.server_event_session_targets](../system-catalog-views/sys-server-event-session-targets-transact-sql.md) | Returns a row for each event target for a server-scoped event session. |
| [sys.server_event_sessions](../system-catalog-views/sys-server-event-sessions-transact-sql.md) | Returns a row for each server-scoped event session. |
| [sys.database_event_session_actions](../system-catalog-views/sys-database-event-session-actions-azure-sql-database.md) | Returns a row for each action on each event of a database-scoped event session. |
| [sys.database_event_session_events](../system-catalog-views/sys-database-event-session-events-azure-sql-database.md) | Returns a row for each event in a database-scoped event session. |
| [sys.database_event_session_fields](../system-catalog-views/sys-database-event-session-fields-azure-sql-database.md) | Returns a row for each customizable column that was explicitly set on events and targets of a database-scoped session. |
| [sys.database_event_session_targets](../system-catalog-views/sys-database-event-session-targets-azure-sql-database.md) | Returns a row for each event target for a database-scoped event session. |
| [sys.database_event_sessions](../system-catalog-views/sys-database-event-sessions-azure-sql-database.md) | Returns a row for each database-scoped event session. |

# [SQL Server](#tab/sqlserver)

| Name of catalog view | Description |
| --- | --- |
| [sys.server_event_session_actions](../system-catalog-views/sys-server-event-session-actions-transact-sql.md) | Returns a row for each action on each event of a server-scoped event session. |
| [sys.server_event_session_events](../system-catalog-views/sys-server-event-session-events-transact-sql.md) | Returns a row for each event in a server-scoped event session. |
| [sys.server_event_session_fields](../system-catalog-views/sys-server-event-session-fields-transact-sql.md) | Returns a row for each customizable column that was explicitly set on events and targets of a server-scoped event session. |
| [sys.server_event_session_targets](../system-catalog-views/sys-server-event-session-targets-transact-sql.md) | Returns a row for each event target for a server-scoped event session. |
| [sys.server_event_sessions](../system-catalog-views/sys-server-event-sessions-transact-sql.md) | Returns a row for each server-scoped event session. |

---

## Extended Events dynamic management views

Extended Events provides several [dynamic management views (DMVs)](../system-dynamic-management-views/extended-events-dynamic-management-views.md). DMVs return information about *active* (started) event sessions, such as session and target statistics.

# [Azure SQL Database and SQL database in Fabric](#tab/sqldb)

| Name of DMV | Description |
| --- | --- |
| [sys.dm_xe_database_session_event_actions](../system-dynamic-management-views/sys-dm-xe-database-session-event-actions-azure-sql-database.md) | Returns information about database-scoped event session actions. |
| [sys.dm_xe_database_session_events](../system-dynamic-management-views/sys-dm-xe-database-session-events-azure-sql-database.md) | Returns information about database-scoped event session events. |
| [sys.dm_xe_database_session_object_columns](../system-dynamic-management-views/sys-dm-xe-database-session-object-columns-azure-sql-database.md) | Shows the configuration values for objects that are bound to a database-scoped session. |
| [sys.dm_xe_database_session_targets](../system-dynamic-management-views/sys-dm-xe-database-session-targets-azure-sql-database.md) | Returns information about database-scoped event session targets. |
| [sys.dm_xe_database_sessions](../system-dynamic-management-views/sys-dm-xe-database-sessions-azure-sql-database.md) | Returns a row for each database-scoped event session running in the current database. |

# [Azure SQL Managed Instance](#tab/sqlmi)

| Name of DMV | Description |
| --- | --- |
| [sys.dm_xe_session_event_actions](../system-dynamic-management-views/sys-dm-xe-session-event-actions-transact-sql.md) | Returns information about server-scoped event session actions. |
| [sys.dm_xe_session_events](../system-dynamic-management-views/sys-dm-xe-session-events-transact-sql.md) | Returns information about server-scoped event session events. |
| [sys.dm_xe_session_object_columns](../system-dynamic-management-views/sys-dm-xe-session-object-columns-transact-sql.md) | Shows the configuration values for objects that are bound to a server-scoped session. |
| [sys.dm_xe_session_targets](../system-dynamic-management-views/sys-dm-xe-session-targets-transact-sql.md) | Returns information about server-scoped event session targets. |
| [sys.dm_xe_sessions](../system-dynamic-management-views/sys-dm-xe-sessions-transact-sql.md) | Returns a row for each server-scoped event session running on the server. |
| [sys.dm_xe_database_session_event_actions](../system-dynamic-management-views/sys-dm-xe-database-session-event-actions-azure-sql-database.md) | Returns information about database-scoped event session actions. |
| [sys.dm_xe_database_session_events](../system-dynamic-management-views/sys-dm-xe-database-session-events-azure-sql-database.md) | Returns information about database-scoped event session events. |
| [sys.dm_xe_database_session_object_columns](../system-dynamic-management-views/sys-dm-xe-database-session-object-columns-azure-sql-database.md) | Shows the configuration values for objects that are bound to a database-scoped session. |
| [sys.dm_xe_database_session_targets](../system-dynamic-management-views/sys-dm-xe-database-session-targets-azure-sql-database.md) | Returns information about database-scoped event session targets. |
| [sys.dm_xe_database_sessions](../system-dynamic-management-views/sys-dm-xe-database-sessions-azure-sql-database.md) | Returns a row for each database-scoped event session running in the current database. |

# [SQL Server](#tab/sqlserver)

| Name of DMV | Description |
| --- | --- |
| [sys.dm_xe_session_event_actions](../system-dynamic-management-views/sys-dm-xe-session-event-actions-transact-sql.md) | Returns information about server-scoped event session actions. |
| [sys.dm_xe_session_events](../system-dynamic-management-views/sys-dm-xe-session-events-transact-sql.md) | Returns information about server-scoped event session events. |
| [sys.dm_xe_session_object_columns](../system-dynamic-management-views/sys-dm-xe-session-object-columns-transact-sql.md) | Shows the configuration values for objects that are bound to a server-scoped session. |
| [sys.dm_xe_session_targets](../system-dynamic-management-views/sys-dm-xe-session-targets-transact-sql.md) | Returns information about server-scoped event session targets. |
| [sys.dm_xe_sessions](../system-dynamic-management-views/sys-dm-xe-sessions-transact-sql.md) | Returns a row for each server-scoped event session running on the server. |

---

## Permissions

In Azure SQL Database, SQL database in Fabric, Azure SQL Managed Instance, and in SQL Server 2022 and later versions, Extended Events supports a granular permission model. The following permissions can be granted:

# [Azure SQL Database and SQL database in Fabric](#tab/sqldb)

```sql
CREATE ANY DATABASE EVENT SESSION
DROP ANY DATABASE EVENT SESSION
ALTER ANY DATABASE EVENT SESSION
ALTER ANY DATABASE EVENT SESSION ADD EVENT
ALTER ANY DATABASE EVENT SESSION DROP EVENT
ALTER ANY DATABASE EVENT SESSION ADD TARGET
ALTER ANY DATABASE EVENT SESSION DROP TARGET
ALTER ANY DATABASE EVENT SESSION ENABLE
ALTER ANY DATABASE EVENT SESSION DISABLE
ALTER ANY DATABASE EVENT SESSION OPTION
```

# [Azure SQL Managed Instance](#tab/sqlmi)

```sql
CREATE ANY EVENT SESSION
DROP ANY EVENT SESSION
ALTER ANY EVENT SESSION
ALTER ANY EVENT SESSION ADD EVENT
ALTER ANY EVENT SESSION DROP EVENT
ALTER ANY EVENT SESSION ADD TARGET
ALTER ANY EVENT SESSION DROP TARGET
ALTER ANY EVENT SESSION ENABLE
ALTER ANY EVENT SESSION DISABLE
ALTER ANY EVENT SESSION OPTION

CREATE ANY DATABASE EVENT SESSION
DROP ANY DATABASE EVENT SESSION
ALTER ANY DATABASE EVENT SESSION
ALTER ANY DATABASE EVENT SESSION ADD EVENT
ALTER ANY DATABASE EVENT SESSION DROP EVENT
ALTER ANY DATABASE EVENT SESSION ADD TARGET
ALTER ANY DATABASE EVENT SESSION DROP TARGET
ALTER ANY DATABASE EVENT SESSION ENABLE
ALTER ANY DATABASE EVENT SESSION DISABLE
ALTER ANY DATABASE EVENT SESSION OPTION
```

# [SQL Server](#tab/sqlserver)

```sql
CREATE ANY EVENT SESSION
DROP ANY EVENT SESSION
ALTER ANY EVENT SESSION
ALTER ANY EVENT SESSION ADD EVENT
ALTER ANY EVENT SESSION DROP EVENT
ALTER ANY EVENT SESSION ADD TARGET
ALTER ANY EVENT SESSION DROP TARGET
ALTER ANY EVENT SESSION ENABLE
ALTER ANY EVENT SESSION DISABLE
ALTER ANY EVENT SESSION OPTION
```

---

For information on what each of these permissions controls, see [CREATE EVENT SESSION](../../t-sql/statements/create-event-session-transact-sql.md), [ALTER EVENT SESSION](../../t-sql/statements/alter-event-session-transact-sql.md), and [DROP EVENT SESSION](../../t-sql/statements/drop-event-session-transact-sql.md).

All of these permissions are included in the `CONTROL` permission on the database, SQL managed instance, or SQL Server instance. In Azure SQL Database, the database owner (`dbo`), members of the `db_owner` database role, and the administrators of the logical server hold the database `CONTROL` permission. In Azure SQL Managed Instance and in SQL Server, members of the `sysadmin` server role hold the `CONTROL` permission on the instance.

<a id="code-examples-can-differ-for-azure-sql-database-and-sql-managed-instance"></a>

## Code examples can differ for Azure SQL Database, SQL database in Fabric, and SQL Managed Instance

[!INCLUDE [sql-on-premises-vs-azure-similar-sys-views-include](../../includes/paragraph-content/sql-on-premises-vs-azure-similar-sys-views-include.md)]

## Related content

- [Extended Events Dynamic Management Views](../system-dynamic-management-objects/extended-events-dynamic-management-views.md)
- [Extended Events Catalog Views (Transact-SQL)](../system-catalog-views/extended-events-catalog-views-transact-sql.md)
- [SQL Mysteries: Causality tracking vs Event Sequence for XEvent Sessions](https://techcommunity.microsoft.com/blog/sqlserver/sql-mysteries-causality-tracking-vs-event-sequence-for-xevent-sessions/3198826)
- [Analyze and prevent deadlocks in Azure SQL Database and Fabric SQL database](/azure/azure-sql/database/analyze-prevent-deadlocks)
- [Quickstart: Extended Events](quick-start-extended-events-in-sql-server.md)
- [Create an event session with an event_file target in Azure Storage](/azure/azure-sql/database/xevent-code-event-file)
- [Extended Events in Azure SQL](/azure/azure-sql/database/xevent-db-diff-from-svr)
- [XELite: Cross-platform library to read XEvents from XEL files or live SQL streams](https://www.nuget.org/packages/Microsoft.SqlServer.XEvent.XELite/)
