---
title: "XEvents related system objects"
description: These resources relate to Extended Events, including how system objects support them, how SQL Server uses them, and aspects particular to Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: jukoesma, dfurman
ms.date: 09/23/2025
ms.service: sql
ms.subservice: xevents
ms.topic: reference
ms.custom:
  - ignite-2025
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# System objects that support Extended Events

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

The present article provides links to other articles that relate to Extended Events.

The lists are not necessarily complete.

## System tables

- [Extended Events Tables - trace_xe_action_map](../system-tables/extended-events-tables-trace-xe-action-map.md)

- [Extended Events Tables - trace_xe_event_map](../system-tables/extended-events-tables-trace-xe-event-map.md)

## System catalog views

- [Extended Events Catalog Views (Transact-SQL)](../system-catalog-views/extended-events-catalog-views-transact-sql.md)

- [sys.server_event_sessions (Transact-SQL)](../system-catalog-views/sys-server-event-sessions-transact-sql.md)

- [sys.server_event_session_actions (Transact-SQL)](../system-catalog-views/sys-server-event-session-actions-transact-sql.md)

- [sys.server_event_session_events (Transact-SQL)](../system-catalog-views/sys-server-event-session-events-transact-sql.md)

- [sys.server_event_session_fields (Transact-SQL)](../system-catalog-views/sys-server-event-session-fields-transact-sql.md)

- [sys.server_event_session_targets (Transact-SQL)](../system-catalog-views/sys-server-event-session-targets-transact-sql.md)

## Other system objects

- [Extended Events Dynamic Management Views](../system-dynamic-management-views/extended-events-dynamic-management-views.md)

## Uses of Extended Events by SQL Server itself

This list is not meant to be complete.

- [Accessing Diagnostic Information in the Extended Events Log](../native-client/features/accessing-diagnostic-information-in-the-extended-events-log.md)

- [Configure Extended Events for Always On availability groups](../../database-engine/availability-groups/windows/always-on-extended-events.md)

- [Extended Events for Stretch Database](/previous-versions/sql/sql-server/stretch-database/extended-events-for-stretch-database)

<a id="azure-sql-database-and-extended-events"></a>

## Azure SQL Database and SQL database in Fabric extended events

- [Extended Events in Azure SQL Database](/azure/sql-database/sql-database-xevent-db-diff-from-svr)

- [sys.database_event_session_targets (Azure SQL Database)](../system-catalog-views/sys-database-event-session-targets-azure-sql-database.md)

- [sys.database_event_session_fields (Azure SQL Database)](../system-catalog-views/sys-database-event-session-fields-azure-sql-database.md)

- [sys.database_event_session_events (Azure SQL Database)](../system-catalog-views/sys-database-event-session-events-azure-sql-database.md)

- [sys.database_event_session_actions (Azure SQL Database)](../system-catalog-views/sys-database-event-session-actions-azure-sql-database.md)
