---
title: "Extended Events Equivalents to SQL Trace Event Classes"
description: This article shows you how to view the Extended Events actions and events that are equivalent to each SQL Trace event and its associated columns.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 09/23/2025
ms.service: sql
ms.subservice: xevents
ms.topic: concept-article
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "SQL Trace, extended events equivalents"
  - "extended events [SQL Server], SQL Trace equivalents"
  - "extended events [SQL Server], user configurable events"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# View the Extended Events Equivalents to SQL Trace Event Classes

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

 Learn how the SQL Trace events map to Extended Events events and actions. You can collect event data that is equivalent to SQL Trace event classes and columns.

 You can use the following procedure to view the Extended Events events and actions that are equivalent to each SQL Trace event and its associated columns.

 To run T-SQL commands, use [SQL Server Management Studio (SSMS)](https://aka.ms/ssms), the [MSSQL extension for Visual Studio Code](../../tools/visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md), [sqlcmd](../../tools/sqlcmd/sqlcmd-utility.md), or your favorite T-SQL querying tool.

<a id="to-view-the-extended-events-equivalents-to-sql-trace-events-using-query-editor"></a>

## View the Extended Events equivalents to SQL Trace events using Query Editor

- From a **Query Editor** in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], run the following query:

   ```sql
   SELECT DISTINCT
      tb.trace_event_id,
      te.name            AS 'Event Class',
      em.package_name    AS 'Package',
      em.xe_event_name   AS 'XEvent Name',
      tb.trace_column_id,
      tc.name            AS 'SQL Trace Column',
      am.xe_action_name  AS 'Extended Events action'
   FROM
                sys.trace_events         AS te
      LEFT JOIN sys.trace_xe_event_map   AS em ON te.trace_event_id  = em.trace_event_id
      LEFT JOIN sys.trace_event_bindings AS tb ON em.trace_event_id  = tb.trace_event_id
      LEFT JOIN sys.trace_columns        AS tc ON tb.trace_column_id = tc.trace_column_id
      LEFT JOIN sys.trace_xe_action_map  AS am ON tc.trace_column_id = am.trace_column_id
   ORDER BY te.name, tc.name;
   ```

Note:

- If all columns return `NULL` except for the `Event Class` column, the event class was not migrated from SQL Trace. 

- If only the value in the `Extended Events action` column is `NULL`, either of the following conditions is true:

  - The `SQL Trace column` maps to one of the data fields that is associated with the Extended Events event.

  - Each Extended Events event has a default set of data fields that are automatically included in the result set.

  - The `action` column does not have a meaningful Extended Events equivalent. An example is the `EventClass` column in SQL Trace. This column is not needed in Extended Events because the event name serves the same purpose.

- Extended Events uses a single event to replace user configurable SQL Trace event classes (`UserConfigurable:1` through `UserConfigurable:9`). The event is named `user_event`. This event is raised by using `sp_trace_generateevent`, which is the same stored procedure that is used by SQL Trace. The `user_event` event is returned regardless of which event ID is passed to the stored procedure. However, an `event_id` field is returned as part of the event data, which you can use to build a predicate based on the event ID. For example, if you use `UserConfigurable:0` (event ID = 82) in the code, you can add the `user_event` event to the session, and specify a predicate of `event_id = 82`. Therefore, you do not have to change the code because the `sp_trace_generateevent` stored procedure generates the Extended Events `user_event` event, and the equivalent SQL Trace event class.    

## Related content

- [sp_trace_generateevent (Transact-SQL)](../system-stored-procedures/sp-trace-generateevent-transact-sql.md)
