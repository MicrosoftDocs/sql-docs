---
title: "View the Extended Events Equivalents to SQL Trace Event Classes | Microsoft Docs"
ms.custom: ""
ms.date: "03/05/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xevents
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Trace, extended events equivalents"
  - "extended events [SQL Server], SQL Trace equivalents"
  - "extended events [SQL Server], user configurable events"
ms.assetid: 7f24104c-201d-4361-9759-f78a27936011
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# View the Extended Events Equivalents to SQL Trace Event Classes
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

  If you want to use Extended Events to collect event data that is equivalent to SQL Trace event classes and columns, it is useful to understand how the SQL Trace events map to Extended Events events and actions.  
  
 You can use the following procedure to view the Extended Events events and actions that are equivalent to each SQL Trace event and its associated columns.  
  
## To view the Extended Events equivalents to SQL Trace events using Query Editor

- From Query Editor in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], run the following query:

   ```sql
   USE MASTER;
   GO
   SELECT DISTINCT
      tb.trace_event_id,
      te.name            AS 'Event Class',
      em.package_name    AS 'Package',
      em.xe_event_name   AS 'XEvent Name',
      tb.trace_column_id,
      tc.name            AS 'SQL Trace Column',
      am.xe_action_name  AS 'Extended Events action'
   FROM
                      sys.trace_events         te
      LEFT OUTER JOIN sys.trace_xe_event_map   em ON te.trace_event_id  = em.trace_event_id
      LEFT OUTER JOIN sys.trace_event_bindings tb ON em.trace_event_id  = tb.trace_event_id
      LEFT OUTER JOIN sys.trace_columns        tc ON tb.trace_column_id = tc.trace_column_id
      LEFT OUTER JOIN sys.trace_xe_action_map  am ON tc.trace_column_id = am.trace_column_id
   ORDER BY te.name, tc.name
   ```

When you view the results, note the following:  

- If all columns return NULL except for the Event Class column, this indicates that the event class was not migrated from SQL Trace.  
  
-   If only the value in the Extended Events action column is NULL, this indicates that either of the following conditions is true:  
  
    -   The SQL Trace column maps to one of the data fields that is associated with the Extended Events event.  
  
        > [!NOTE]  
        > Each Extended Events event has a default set of data fields that are automatically included in the result set.  
  
    -   The action column does not have a meaningful Extended Events equivalent. An example of this is the EventClass column in SQL Trace. This column is not needed in Extended Events because the event name serves the same purpose.  
  
-   For user configurable SQL Trace event classes (UserConfigurable:1 through UserConfigurable:9), Extended Events uses a single event to replace these. The event is named user_event. This event is raised by using sp_trace_generateevent, which is the same stored procedure that is used by SQL Trace. The user_event event is returned regardless of which event ID is passed to the stored procedure. However, an event_id field is returned as part of the event data. This enables you to build a predicate that is based on the event ID. For example, if you use UserConfigurable:0 (event ID = 82) in the code, you can add the user_event event to the session, and specify a predicate of 'event_id = 82'. Therefore, you do not have to change the code because the sp_trace_generateevent stored procedure generates the Extended Events user_event event, and the equivalent SQL Trace event class.  
  
## See Also  
 [sp_trace_generateevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-generateevent-transact-sql.md)  
  
  
