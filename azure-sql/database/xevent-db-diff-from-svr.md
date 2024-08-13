---
title: Extended Events in Azure SQL
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: Describes extended events (XEvents) in Azure SQL Database and Azure SQL Managed Instance
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: wiassaf, mathoma, randolphwest
ms.date: 05/15/2024
ms.service: azure-sql
ms.subservice: performance
ms.topic: reference
ms.custom: sqldbrb=1
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---

# Extended Events in Azure SQL Database and Azure SQL Managed Instance

[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

[!INCLUDE [sql-database-xevents-selectors-1-include](../includes/sql-database-xevents-selectors-1-include.md)]

For an introduction to Extended Events, see:

- [Extended Events](/sql/relational-databases/extended-events/extended-events)
- [Quick Start: Extended events](/sql/relational-databases/extended-events/quick-start-extended-events-in-sql-server)

The feature set, functionality, and usage scenarios for Extended Events in Azure SQL Database and Azure SQL Managed Instance are similar to what is available in SQL Server. The main differences are:

- The `event_file` target always uses blobs in Azure Storage, rather than files on disk.
- In Azure SQL Database, event sessions are always database-scoped. This means that:
  - An event session in one database can't collect events from another database.
  - An event must occur in the context of a user database to be included in a session.
- In Azure SQL Managed Instance, you can create both server-scoped and database-scoped event sessions. We recommend using server-scoped event sessions for most scenarios.

## Get started

There are two examples to help you get started with Extended Events in Azure SQL Database and Azure SQL Managed Instance quickly:

- [Create a session with an event_file target in Azure Storage](xevent-code-event-file.md). This example shows you how to capture event data in a file (blob) in Azure Storage using the `event_file` target. Use this if you need to persist captured event data, or if you want to use event viewer in SQL Server Management Studio (SSMS) to analyze captured data.
- [Create a session with a ring_buffer target in memory](xevent-code-ring-buffer.md). This example shows you how to capture the latest events from an event session in memory using the `ring_buffer` target. Use this as a quick way to look at recent events during ad hoc investigations or troubleshooting, without having to store captured event data.

Extended Events can be used to monitor read-only replicas. For more information, see [Read queries on replicas](read-scale-out.md#monitor-read-only-replicas-with-extended-events).

## Best practices

Adopt the following best practices to use Extended Events in Azure SQL Database and Azure SQL Managed Instance reliably and without affecting database engine health and workload performance.

- If you use the `event_file` target:
  - Use a storage account in the same Azure region as the database or managed instance where you create event sessions.
  - Align the redundancy of the storage account with the redundancy of the database, elastic pool, or managed instance. For [locally redundant](high-availability-sla-local-zone-redundancy.md#locally-redundant-availability) resources, use LRS, GRS, or RA-GRS. For [zone-redundant](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability) resources, use ZRS, GZRS, or RA-GZRS. See [Azure Storage redundancy](/azure/storage/common/storage-redundancy) for details.
  - Don't use any [blob access tier](/azure/storage/blobs/access-tiers-overview) other than `Hot`.
- If you want to create a continuously running event session that starts automatically after each [!INCLUDE [ssde-md](../../docs/includes/ssde-md.md)] restart (for example, after a failover or a maintenance event), include the event session option of `STARTUP_STATE = ON` in your `CREATE EVENT SESSION`  or `ALTER EVENT SESSION` statements.
- Conversely, use `STARTUP_STATE = OFF` for short-term event sessions such as those used in ad hoc troubleshooting.
- In Azure SQL Database, do not read deadlock events from the built-in `dl` event session. If there is a large number of deadlock events collected, reading them with the [sys.fn_xe_file_target_read_file()](/sql/relational-databases/system-functions/sys-fn-xe-file-target-read-file-transact-sql) function can cause an out-of-memory error in the `master` database. This might impact login processing and result in an application outage. For the recommended ways to monitor deadlocks, see [Collect deadlock graphs in Azure SQL Database with Extended Events](analyze-prevent-deadlocks.md#collect-deadlock-graphs-in-azure-sql-database-with-extended-events).

## Event session targets

Azure SQL Database and Azure SQL Managed Instance support the following targets:

- [event_file](/sql/relational-databases/extended-events/targets-for-extended-events-in-sql-server#event_file-target) target. Writes complete buffers to a blob in an Azure Storage container.
- [ring_buffer](/sql/relational-databases/extended-events/targets-for-extended-events-in-sql-server#ring_buffer-target) target. Holds event data in memory until replaced by new event data.
- [event_counter](/sql/relational-databases/extended-events/targets-for-extended-events-in-sql-server#event_counter-target) target. Counts all events that occur during an extended events session.
- [histogram](/sql/relational-databases/extended-events/targets-for-extended-events-in-sql-server#histogram-target) target. Counts the occurrences of different values of fields or actions in separate buckets.
- [event_stream](/sql/relational-databases/extended-events/targets-for-extended-events-in-sql-server#event_stream-target). Streams event data to a .Net application.

> [!NOTE]
> The `event_stream` target in Azure SQL Database and Azure SQL Managed Instance is in preview.

## Transact-SQL differences

When you execute the [CREATE EVENT SESSION](/sql/t-sql/statements/create-event-session-transact-sql), [ALTER EVENT SESSION](/sql/t-sql/statements/alter-event-session-transact-sql), and [DROP EVENT SESSION](/sql/t-sql/statements/drop-event-session-transact-sql) statements in SQL Server and in Azure SQL Managed Instance, you use the `ON SERVER` clause. In Azure SQL Database, you use the `ON DATABASE` clause instead, because in Azure SQL Database event sessions are database-scoped.

## Extended Events catalog views

Extended Events provides several [catalog views](/sql/relational-databases/system-catalog-views/catalog-views-transact-sql). Catalog views tell you about event session *metadata* or *definition*. These views don't return information about instances of active event sessions.

# [SQL Database](#tab/sqldb)

| Name of catalog view | Description |
| :--- | :--- |
| [sys.database_event_session_actions](/sql/relational-databases/system-catalog-views/sys-database-event-session-actions-azure-sql-database) | Returns a row for each action on each event of an event session. |
| [sys.database_event_session_events](/sql/relational-databases/system-catalog-views/sys-database-event-session-events-azure-sql-database) | Returns a row for each event in an event session. |
| [sys.database_event_session_fields](/sql/relational-databases/system-catalog-views/sys-database-event-session-fields-azure-sql-database) | Returns a row for each customize-able column that was explicitly set on events and targets. |
| [sys.database_event_session_targets](/sql/relational-databases/system-catalog-views/sys-database-event-session-targets-azure-sql-database) | Returns a row for each event target for an event session. |
| [sys.database_event_sessions](/sql/relational-databases/system-catalog-views/sys-database-event-sessions-azure-sql-database) | Returns a row for each event session in the database. |

# [SQL Managed Instance](#tab/sqlmi)

| Name of catalog view | Description |
| :--- | :--- |
| [sys.server_event_session_actions](/sql/relational-databases/system-catalog-views/sys-server-event-session-actions-transact-sql) | Returns a row for each action on each event of an event session. |
| [sys.server_event_session_events](/sql/relational-databases/system-catalog-views/sys-server-event-session-events-transact-sql) | Returns a row for each event in an event session. |
| [sys.server_event_session_fields](/sql/relational-databases/system-catalog-views/sys-server-event-session-fields-transact-sql) | Returns a row for each customizable column that was explicitly set on events and targets. |
| [sys.server_event_session_targets](/sql/relational-databases/system-catalog-views/sys-server-event-session-targets-transact-sql) | Returns a row for each event target for an event session. |
| [sys.server_event_sessions](/sql/relational-databases/system-catalog-views/sys-server-event-sessions-transact-sql) | Returns a row for each event session on the server. |

---

## Extended Events dynamic management views

Extended Events provides several [dynamic management views (DMVs)](/sql/relational-databases/system-dynamic-management-views/extended-events-dynamic-management-views). DMVs return information about *started* event sessions.

# [SQL Database](#tab/sqldb)

| Name of DMV | Description |
| :--- | :--- |
| [sys.dm_xe_database_session_event_actions](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-database-session-event-actions-azure-sql-database) | Returns information about event session actions. |
| [sys.dm_xe_database_session_events](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-database-session-events-azure-sql-database) | Returns information about session events. |
| [sys.dm_xe_database_session_object_columns](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-database-session-object-columns-azure-sql-database) | Shows the configuration values for objects that are bound to a session. |
| [sys.dm_xe_database_session_targets](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-database-session-targets-azure-sql-database) | Returns information about session targets. |
| [sys.dm_xe_database_sessions](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-database-sessions-azure-sql-database) | Returns a row for each event session running in the current database. |

# [SQL Managed Instance](#tab/sqlmi)

| Name of DMV | Description |
| :--- | :--- |
| [sys.dm_xe_session_event_actions](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-session-event-actions-transact-sql) | Returns information about event session actions. |
| [sys.dm_xe_session_events](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-session-events-transact-sql) | Returns information about session events. |
| [sys.dm_xe_session_object_columns](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-session-object-columns-transact-sql) | Shows the configuration values for objects that are bound to a session. |
| [sys.dm_xe_session_targets](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-session-targets-transact-sql) | Returns information about session targets. |
| [sys.dm_xe_sessions](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-sessions-transact-sql) | Returns a row for each event session running on the server. |

---

### Common DMVs

There are additional Extended Events DMVs that are common to Azure SQL Database, Azure SQL Managed Instance, and SQL Server:

- [sys.dm_xe_map_values](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-map-values-transact-sql)
- [sys.dm_xe_object_columns](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-object-columns-transact-sql)
- [sys.dm_xe_objects](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-objects-transact-sql)
- [sys.dm_xe_packages](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-packages-transact-sql)

<a name="sqlfindseventsactionstargets" id="sqlfindseventsactionstargets"></a>

## Available events, actions, and targets

Just like in SQL Server, you can obtain available events, actions, and targets using this query:

```sql
SELECT o.object_type,
       p.name AS package_name,
       o.name AS db_object_name,
       o.description AS db_obj_description
FROM sys.dm_xe_objects AS o
INNER JOIN sys.dm_xe_packages AS p
ON p.guid = o.package_guid
WHERE o.object_type IN ('action','event','target')
ORDER BY o.object_type,
         p.name,
         o.name;
```

## Permissions

In Azure SQL Database and Azure SQL Managed Instance, Extended Events support a granular permission model. The following permissions can be granted:

# [SQL Database](#tab/sqldb)

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

# [SQL Managed Instance](#tab/sqlmi)

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

For information on what each of these permissions controls, see [CREATE EVENT SESSION](/sql/t-sql/statements/create-event-session-transact-sql), [ALTER EVENT SESSION](/sql/t-sql/statements/alter-event-session-transact-sql), and [DROP EVENT SESSION](/sql/t-sql/statements/drop-event-session-transact-sql).

All of these permissions are included in the `CONTROL` permission on the database or managed instance. In Azure SQL Database, the database owner (`dbo`), members of the `db_owner` database role, and the administrators of the logical server hold the database `CONTROL` permission. In Azure SQL Managed Instance, members of the `sysadmin` server role hold the `CONTROL` permission on the instance.

## Storage container authorization and control

When you use the `event_file` target, event data is stored in blobs in an Azure Storage container. The [!INCLUDE [ssde-md](../../docs/includes/ssde-md.md)] running the event session must have specific access to this container. You grant this access by creating a [SAS token](/azure/storage/common/storage-sas-overview#sas-token) for the container, and storing the token in a [credential](/sql/relational-databases/security/authentication-access/credentials-database-engine).

In Azure SQL Database, you must use a database-scoped credential. In Azure SQL Managed Instance, use a server-scoped credential.

The SAS token you create for your Azure Storage container must satisfy the following requirements:

- Have the `rwl` (`Read`, `Write`, `List`) permissions.
- Have the start time and expiry time that encompass the lifetime of the event session.
- Have no IP address restrictions.

## Resource governance

In Azure SQL Database, memory consumption by extended event sessions is dynamically controlled by the [!INCLUDE [ssde-md](../../docs/includes/ssde-md.md)] to minimize resource contention.

There's a limit on memory available to event sessions:

- In a single database, total session memory is limited to 128 MB.
- In an elastic pool, individual databases are limited by the single database limits, and in total they can't exceed 512 MB.

If you receive an error message referencing a memory limit, the corrective actions you can take are:

- Run fewer concurrent event sessions.
- Using `CREATE` and `ALTER` statements for event sessions, reduce the amount of memory you specify in the `MAX_MEMORY` clause for the session.

> [!NOTE]
> In Extended Events, the `MAX_MEMORY` clause appears in two contexts: when creating or altering a session (at the session level), and when using the `ring_buffer` target (at the target level). The above limits apply to the session level memory.

There's a limit on the number of started event sessions in Azure SQL Database:

- In a single database, the limit is 100.
- In an elastic pool, the limit is 100 database-scoped sessions per pool.

In [dense elastic pools](elastic-pool-resource-management.md), starting a new extended event session might fail due to memory constraints even when the total number of started sessions is below 100.

To find the total memory consumed by an event session, execute the following query while connected to the database where the event session is started:

```sql
SELECT name AS session_name,
       total_buffer_size + total_target_memory AS total_session_memory
FROM sys.dm_xe_database_sessions;
```

To find the total event session memory for an elastic pool, this query needs to be executed in every database in the pool.

## Related content

- [Extended Events](/sql/relational-databases/extended-events/extended-events)
- [Quick Start: Extended events](/sql/relational-databases/extended-events/quick-start-extended-events-in-sql-server)
