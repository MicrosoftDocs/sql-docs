---
title: Extended Events in Azure SQL Database
description: Describes extended events (XEvents) in Azure SQL Database, and how event sessions differ slightly from event sessions in Microsoft SQL Server.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: wiassaf, mathoma, randolphwest
ms.date: 11/28/2023
ms.service: sql-database
ms.subservice: performance
ms.topic: reference
ms.custom:
  - sqldbrb=1
monikerRange: "=azuresql || =azuresql-db"
---
# Extended Events in Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

[!INCLUDE [sql-database-xevents-selectors-1-include](../includes/sql-database-xevents-selectors-1-include.md)]

For an introduction to Extended Events, see:

- [Extended Events](/sql/relational-databases/extended-events/extended-events)
- [Quick Start: Extended events](/sql/relational-databases/extended-events/quick-start-extended-events-in-sql-server)

The feature set, functionality, and usage scenarios for Extended Events in Azure SQL Database are similar to what is available in SQL Server and Azure SQL Managed Instance. The main differences are:

- Event sessions are always database-scoped. This means that
  - An event session in one database can't collect events from another database
  - An event must occur in the context of a user database to be included in a session
- The `event_file` target always uses blobs in Azure Storage, rather than files on disk

Most of the Extended Event documentation applies to Azure SQL Database as much as it does to SQL Server and Azure SQL Managed Instance. The **Applies to** section of each article tells you whether an article applies to the database engine type you use.

## Get started

There are two examples to help you get started with Extended Events in Azure SQL Database quickly.

- [Create a session with an event_file target in Azure Storage](xevent-code-event-file.md). This example shows you how to capture event data in a file (blob) in Azure Storage using the `event_file` target. Use this if you need to persist captured event data, or if you want to use event viewer in SQL Server Management Studio (SSMS) to analyze captured data.
- [Create a session with a ring_buffer target in memory](xevent-code-ring-buffer.md). This example shows you how to capture the latest events from an event session in memory using the `ring_buffer` target. Use this as a quick way to look at recent events during ad hoc investigations or troubleshooting, without having to store captured event data.

Extended Events can be used to monitor read-only replicas. For more information, see [Read queries on replicas](read-scale-out.md#monitor-read-only-replicas-with-extended-events).

## Best practices

Adopt the following best practices to use Extended Events in Azure SQL Database reliably and without affecting database health and workload performance.

- If you use the `event_file` target:
  - Don't set the `EVENT_RETENTION_MODE` option to `NO_EVENT_LOSS`. This might cause connection timeouts and failover delays among other issues, affecting database availability.
  - Use a storage account in the same Azure region as the database where you create event sessions.
  - Align the redundancy of the storage account with the redundancy of the database or elastic pool. For [locally redundant](high-availability-sla.md#locally-redundant-availability) Azure SQL resources, use LRS, GRS, or RA-GRS. For [zone-redundant](high-availability-sla.md#zone-redundant-availability) Azure SQL resources, use ZRS, GZRS, or RA-GZRS. See [Azure Storage redundancy](/azure/storage/common/storage-redundancy) for details.
  - Don't use any [blob access tier](/azure/storage/blobs/access-tiers-overview) other than `Hot`.
- If you want to create a continuously running event session that starts automatically after each [!INCLUDE [ssde-md](../../docs/includes/ssde-md.md)] restart (for example, after a failover or a maintenance event), include the event session option of `STARTUP_STATE = ON` in your `CREATE EVENT SESSION`  or `ALTER EVENT SESSION` statements.
- Conversely, use `STARTUP_STATE = OFF` for short-term event sessions such as those used in ad hoc troubleshooting.

## Targets for Azure SQL Database event sessions

Azure SQL Database supports the following targets:

- [event_file](/sql/relational-databases/extended-events/targets-for-extended-events-in-sql-server#event_file-target) target. Writes complete buffers to a blob in an Azure Storage container.
- [ring_buffer](/sql/relational-databases/extended-events/targets-for-extended-events-in-sql-server#ring_buffer-target) target. Holds event data in memory until replaced by new event data.
- [event_counter](/sql/relational-databases/extended-events/targets-for-extended-events-in-sql-server#event_counter-target) target. Counts all events that occur during an extended events session.
- [histogram](/sql/relational-databases/extended-events/targets-for-extended-events-in-sql-server#histogram-target) target. Counts the occurrences of different values of fields or actions in separate buckets.
- [event_stream](/sql/relational-databases/extended-events/targets-for-extended-events-in-sql-server#event_stream-target). Streams event data to a .Net application.

> [!NOTE]
> The `event_stream` target in Azure SQL Database is in preview.

## Transact-SQL differences

- When you execute the [CREATE EVENT SESSION](/sql/t-sql/statements/create-event-session-transact-sql) command in SQL Server, you use the `ON SERVER` clause. In Azure SQL Database, you use the `ON DATABASE` clause instead, because in Azure SQL Database event sessions are database-scoped.
- The `ON DATABASE` clause also applies to the [ALTER EVENT SESSION](/sql/t-sql/statements/alter-event-session-transact-sql) and [DROP EVENT SESSION](/sql/t-sql/statements/drop-event-session-transact-sql) Transact-SQL commands.

## Extended Events catalog views in Azure SQL Database

Extended Events provides several [catalog views](/sql/relational-databases/system-catalog-views/catalog-views-transact-sql) in Azure SQL Database. Catalog views tell you about *metadata or definitions* of user-created event sessions in the current database. These views don't return information about instances of active event sessions.

| Name of catalog view | Description |
| :--- | :--- |
| [sys.database_event_session_actions](/sql/relational-databases/system-catalog-views/sys-database-event-session-actions-azure-sql-database) | Returns a row for each action on each event of an event session. |
| [sys.database_event_session_events](/sql/relational-databases/system-catalog-views/sys-database-event-session-events-azure-sql-database) | Returns a row for each event in an event session. |
| [sys.database_event_session_fields](/sql/relational-databases/system-catalog-views/sys-database-event-session-fields-azure-sql-database) | Returns a row for each customize-able column that was explicitly set on events and targets. |
| [sys.database_event_session_targets](/sql/relational-databases/system-catalog-views/sys-database-event-session-targets-azure-sql-database) | Returns a row for each event target for an event session. |
| [sys.database_event_sessions](/sql/relational-databases/system-catalog-views/sys-database-event-sessions-azure-sql-database) | Returns a row for each event session in the database. |

In SQL Server and Azure SQL Managed Instance, similar catalog views have names that include `sys.server_` instead of `sys.database_`, following the `sys.server_event_*` name pattern.

## Extended Events dynamic management views in Azure SQL Database

Extended Events provides several [dynamic management views (DMVs)](/sql/relational-databases/system-dynamic-management-views/extended-events-dynamic-management-views) in Azure SQL Database. DMVs return information about *started* event sessions.

| Name of DMV | Description |
| :--- | :--- |
| [sys.dm_xe_database_session_event_actions](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-database-session-event-actions-azure-sql-database) | Returns information about event session actions. |
| [sys.dm_xe_database_session_events](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-database-session-events-azure-sql-database) | Returns information about session events. |
| [sys.dm_xe_database_session_object_columns](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-database-session-object-columns-azure-sql-database) | Shows the configuration values for objects that are bound to a session. |
| [sys.dm_xe_database_session_targets](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-database-session-targets-azure-sql-database) | Returns information about session targets. |
| [sys.dm_xe_database_sessions](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-database-sessions-azure-sql-database) | Returns a row for each event session that is scoped to the current database. |

In SQL Server and Azure SQL Managed Instance, similar DMVs are named without the `_database` portion of the name, for example `sys.dm_xe_sessions` instead of `sys.dm_xe_database_sessions`.

### Common DMVs

There are additional Extended Events DMVs that are common to Azure SQL Database, Azure SQL Managed Instance, and SQL Server:

- [sys.dm_xe_map_values](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-map-values-transact-sql)
- [sys.dm_xe_object_columns](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-object-columns-transact-sql)
- [sys.dm_xe_objects](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-objects-transact-sql)
- [sys.dm_xe_packages](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-packages-transact-sql)

<a name="sqlfindseventsactionstargets" id="sqlfindseventsactionstargets"></a>

## Available events, actions, and targets

Just like in SQL Server, you can obtain available events, actions, and targets in Azure SQL Database using this sample query:

```sql
SELECT
    o.object_type,
    p.name         AS [package_name],
    o.name         AS [db_object_name],
    o.description  AS [db_obj_description]
FROM
                sys.dm_xe_objects  AS o
    INNER JOIN sys.dm_xe_packages AS p  ON p.guid = o.package_guid
WHERE
    o.object_type in
        (
        'action',  'event',  'target'
        )
ORDER BY
    o.object_type,
    p.name,
    o.name;
```

## Permissions

In Azure SQL Database, Extended Events support a granular permission model. The following permissions might be granted:

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

For information on what each of these permissions controls, see [CREATE EVENT SESSION](/sql/t-sql/statements/create-event-session-transact-sql), [ALTER EVENT SESSION](/sql/t-sql/statements/alter-event-session-transact-sql), and [DROP EVENT SESSION](/sql/t-sql/statements/drop-event-session-transact-sql).

All of these permissions are included in the database `CONTROL` permission. The database owner (`dbo`), members of the `db_owner` database role, and the administrator of the logical server hold the database `CONTROL` permission.

## Storage container authorization and control

When you use the `event_file` target, event data is stored in blobs in an Azure Storage container. The [!INCLUDE [ssde-md](../../docs/includes/ssde-md.md)] running the event session must have specific access to this container. You grant this access by creating a [SAS token](/azure/storage/common/storage-sas-overview#sas-token) for the container, and storing the token in a database-scoped [credential](/sql/relational-databases/security/authentication-access/credentials-database-engine).

The SAS token you create for your Azure Storage container must satisfy the following requirements:

- Have the `rwl` (`Read`, `Write`, `List`) permissions
- Have the start time and expiry time that encompass the lifetime of the event session
- Have no IP address restrictions

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
SELECT
    name AS session_name,
    total_buffer_size + total_target_memory AS total_session_memory
FROM sys.dm_xe_database_sessions;
```

To find the total event session memory for an elastic pool, this query needs to be executed in every database in the pool.

## Related content

- [Extended Events](/sql/relational-databases/extended-events/extended-events)
- [Quick Start: Extended events](/sql/relational-databases/extended-events/quick-start-extended-events-in-sql-server)
