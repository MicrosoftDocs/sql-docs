---
title: "XEvents related system objects | Microsoft Docs"
ms.custom: ""
ms.date: 03/24/2019
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: jukoesma
ms.technology: xevents
ms.topic: reference
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# System objects that support extended events

[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

The present article provides links to other articles that relate to extended events. They are articles that describe the following:

- System objects that provide support for the extended events feature.
- Parts of SQL Server itself that use extended events.
- Aspects of extended events that are particular to Azure SQL Database in the cloud.

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

## Uses of extended events by SQL Server itself

This list is not meant to be complete.

- [Accessing Diagnostic Information in the Extended Events Log](../native-client/features/accessing-diagnostic-information-in-the-extended-events-log.md)

- [Configure extended events for Always On availability groups](../../database-engine/availability-groups/windows/always-on-extended-events.md)

- [Extended Events for Stretch Database](../../sql-server/stretch-database/extended-events-for-stretch-database.md)

## Azure SQL Database and extended events

- [Extended events in Azure SQL Database](/azure/sql-database/sql-database-xevent-db-diff-from-svr)

- [sys.database_event_session_targets (Azure SQL Database)](../system-catalog-views/sys-database-event-session-targets-azure-sql-database.md)

- [sys.database_event_session_fields (Azure SQL Database)](../system-catalog-views/sys-database-event-session-fields-azure-sql-database.md)

- [sys.database_event_session_events (Azure SQL Database)](../system-catalog-views/sys-database-event-session-events-azure-sql-database.md)

- [sys.database_event_session_actions (Azure SQL Database)](../system-catalog-views/sys-database-event-session-actions-azure-sql-database.md)
