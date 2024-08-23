---
title: "sys.dm_xe_database_session_targets"
titleSuffix: Azure SQL Database and Azure SQL Managed Instance
description: sys.dm_xe_database_session_targets (Azure SQL Database and Azure SQL Managed Instance)
author: rwestMSFT
ms.author: randolphwest
ms.date: 10/27/2023
ms.service: azure-sql-database
ms.topic: "reference"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || =azuresqldb-mi-current"
---
# sys.dm_xe_database_session_targets (Azure SQL Database and Azure SQL Managed Instance)

[!INCLUDE [Azure SQL Database and Azure SQL Managed Instance](../../includes/applies-to-version/asdb-asdbmi.md)]

Returns information about *active* database-scoped session targets. For information about targets for all database-scoped sessions, see [sys.database_event_session_targets](../system-catalog-views/sys-database-event-session-targets-azure-sql-database.md).

[!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] supports only [database-scoped sessions](/azure/azure-sql/database/xevent-db-diff-from-svr). [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] supports both database-scoped sessions and [server-scoped sessions](../extended-events/extended-events.md). Server-scoped sessions are recommended for SQL managed instances. For more information, see [CREATE EVENT SESSION](../../t-sql/statements/create-event-session-transact-sql.md#code-examples-can-differ-for-azure-sql-database-and-sql-managed-instance).

| Column name | Data type | Description |
| --- | --- | --- |
| `event_session_address` | **varbinary(8)** | The memory address of the event session. Has a many-to-one relationship with `sys.dm_xe_database_sessions`.address. Isn't nullable. |
| `target_name` | **nvarchar(60)** | The name of the target within a session. Not nullable. |
| `target_package_guid` | **uniqueidentifier** | The GUID of the package that contains the target. Not nullable. |
| `execution_count` | **bigint** | The number of times the target has been executed for the session. Not nullable. |
| `execution_duration_ms` | **bigint** | The total amount of time, in milliseconds, that the target has been executing. Not nullable. |
| `target_data` | **nvarchar(max)** | The data that the target maintains, such as event aggregation information. Nullable. |
| `bytes_written` | **bigint** | Number of bytes written to target. Not nullable. |
| `failed_buffer_count` | **bigint** | Number of buffers that have failed to be processed by the target. This value is target-specific, and is different from `dropped_buffer_count` in [sys.dm_xe_database_sessions](sys-dm-xe-database-sessions-azure-sql-database.md). If a target fails to process a buffer, another target of the same session may independently process the same buffer successfully.|

## Permissions

Requires the VIEW DATABASE STATE permission.

### Relationship cardinalities

| From | To | Relationship |
| --- | --- | --- |
| `sys.dm_xe_database_session_targets.event_session_address` | `sys.dm_xe_database_sessions.address` | Many-to-one |

## Next steps

- [Extended events in Azure SQL Database](/azure/azure-sql/database/xevent-db-diff-from-svr)
- [Event File target code for extended events in Azure SQL Database and SQL Managed Instance](/azure/azure-sql/database/xevent-code-event-file)
- [sys.dm_xe_database_sessions (Azure SQL Database and Azure SQL Managed Instance)](sys-dm-xe-database-sessions-azure-sql-database.md)
- [sys.dm_xe_database_session_object_columns (Azure SQL Database and Azure SQL Managed Instance)](sys-dm-xe-database-session-object-columns-azure-sql-database.md)
- [Monitoring Microsoft Azure SQL Database and Azure SQL Managed Instance performance using dynamic management views](/azure/azure-sql/database/monitoring-with-dmvs)
