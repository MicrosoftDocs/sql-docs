---
title: Create an event session with a ring_buffer target in memory
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: Provides example steps to create an  event session in Azure SQL, using the in-memory event_file target.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: wiassaf, mathoma, randolphwest
ms.date: 12/26/2023
ms.service: azure-sql
ms.subservice: performance
ms.topic: sample
ms.custom: sqldbrb=1
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---
# Create an event session with a ring_buffer target in memory

[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

[!INCLUDE [sql-database-xevents-selectors-1-include](../includes/sql-database-xevents-selectors-1-include.md)]

The high-level steps in this walkthrough are:

1. Create and start an event session with a `ring_buffer` target
1. View captured event data as XML
1. View captured event data as a relational rowset

With the `ring_buffer` target, the steps are simpler than with the `event_file` target because you don't need to store event data in Azure Storage.

## Create and start an event session with a ring_buffer target

To create a new event session in SQL Server Management Studio (SSMS), expand the **Extended Events** node. This node is under the database folder in Azure SQL Database, and under the **Management** folder in Azure SQL Managed Instance. Right-click on the **Sessions** folder, and select **New Session...**. On the **General** page, enter a name for the session, which is `example-session` in this example. On the **Events** page, select one or more events to add to the session. In this example, we selected the `sql_batch_starting` event.

:::image type="content" source="media/xevents/create-event-session-events.png" alt-text="Screenshot of the New Session SSMS dialog showing the event selection page with the sql_batch_starting event selected.":::

On the **Data Storage** page, select `ring_buffer` as the target type. To conserve memory, we recommend that you keep the number of events to a small number (1,000 by default), and to set the maximum buffer memory to 1 MB or less. For details, see [ring_buffer target](/sql/relational-databases/extended-events/targets-for-extended-events-in-sql-server#ring_buffer-target).

:::image type="content" source="media/xevents/create-event-session-data-storage-ring-buffer.png" alt-text="Screenshot of the New Session SSMS dialog showing the data storage selection page with a ring_buffer target selected.":::

Now that the session is configured, you can optionally select the **Script** button to create a T-SQL script of the session to save it for later. Here's the script for our example session:

# [SQL Database](#tab/sqldb)

```sql
CREATE EVENT SESSION [example-session] ON DATABASE
ADD EVENT sqlserver.sql_batch_starting
ADD TARGET package0.ring_buffer(SET max_memory=(1024))
GO
```

# [SQL Managed Instance](#tab/sqlmi)

```sql
CREATE EVENT SESSION [example-session] ON SERVER
ADD EVENT sqlserver.sql_batch_starting
ADD TARGET package0.ring_buffer(SET max_memory=(1024))
GO
```

---

Select **OK** to create the session.

## View session data as XML

In Object Explorer, expand the **Sessions** folder to see the event session you created. By default, the session isn't started when it's created. To start the session, right-click on the session name, and select **Start Session**. You can later stop it by similarly selecting **Stop Session**, once the session is running.

As T-SQL batches are executed in this database or managed instance, the session writes events in a memory buffer. Because the size of the memory buffer is finite, once all memory is used the older events are discarded to make room for newer events.

In Object Explorer, expand the session to see the `package0.ring_buffer` target, and double-click on the target. You can also right-click and select **View Target Data...**. This opens a grid with an XML fragment shown. Select this XML fragment to see an XML document representing the memory buffer contents.

The first line of the XML document describes session and target metadata:

```xml
<RingBufferTarget truncated="0" processingTime="0" totalEventsProcessed="17" eventCount="17" droppedCount="0" memoryUsed="32070">
```

In this example, we see that 17 events were processed by the `ring_buffer` target. No events were dropped because the buffer memory wasn't exhausted, and the maximum number of events we configured (1,000) hasn't been reached.

> [!TIP]  
> Pay attention to the `truncated` attribute. If it's set to `1`, it means that the XML representation of the memory buffer isn't showing the entire buffer contents. For more information, see [ring_buffer target](/sql/relational-databases/extended-events/targets-for-extended-events-in-sql-server#ring_buffer-target).

The rest of the XML document contains events. A representation of a single event in XML might look like this:

```xml
  <event name="sql_batch_starting" package="sqlserver" timestamp="2023-10-18T17:43:34.079Z">
    <data name="batch_text">
      <type name="unicode_string" package="package0"></type>
      <value><![CDATA[SELECT
'DatabaseXEStore[@Name=' + quotename(CAST(db_name() AS sysname),'''') +' and @ServerName=' + quotename(CAST(SERVERPROPERTY('servername') AS sysname),'''') + ']' AS [Urn],
CAST(db_name() AS sysname) AS [Name],
CAST(SERVERPROPERTY('servername') AS sysname) AS [ServerName],
(SELECT count(*) FROM sys.dm_xe_database_sessions) AS [RunningSessionCount]]]></value>
    </data>
  </event>
```

Here, the `value` attribute contains the T-SQL batch (a single query in this example).

## View session data as a relational rowset

To see event data from a `ring_buffer` target in a relational rowset, you need to write a T-SQL query that uses [XQuery](/sql/xquery/xquery-language-reference-sql-server) expressions to convert XML to relational data.

Here's an example for the session we created, displaying the most recent events first:

# [SQL Database](#tab/sqldb)

```sql
WITH
/* An XML document representing memory buffer contents */
RingBuffer AS
(
SELECT CAST(xst.target_data AS xml) AS TargetData
FROM sys.dm_xe_database_session_targets AS xst
INNER JOIN sys.dm_xe_database_sessions AS xs
ON xst.event_session_address = xs.address
WHERE xs.name = N'example-session'
),
/* A row for each event in the buffer, represented as an XML fragment */
EventNode AS
(
SELECT CAST(NodeData.query('.') AS xml) AS EventInfo
FROM RingBuffer AS rb
CROSS APPLY rb.TargetData.nodes('/RingBufferTarget/event') AS n(NodeData)
)
/* A relational rowset formed by using the XQuery value method */
SELECT EventInfo.value('(event/@timestamp)[1]','datetimeoffset') AS timestamp,
       EventInfo.value('(event/@name)[1]','sysname') AS event_name,
       EventInfo.value('(event/data/value)[1]','nvarchar(max)') AS sql_batch_text
FROM EventNode
ORDER BY timestamp DESC;
```

# [SQL Managed Instance](#tab/sqlmi)

```sql
WITH
/* An XML document representing memory buffer contents */
RingBuffer AS
(
SELECT CAST(xst.target_data AS xml) AS TargetData
FROM sys.dm_xe_session_targets AS xst
INNER JOIN sys.dm_xe_sessions AS xs
ON xst.event_session_address = xs.address
WHERE xs.name = N'example-session'
),
/* A row for each event in the buffer, represented as an XML fragment */
EventNode AS
(
SELECT CAST(NodeData.query('.') AS xml) AS EventInfo
FROM RingBuffer AS rb
CROSS APPLY rb.TargetData.nodes('/RingBufferTarget/event') AS n(NodeData)
)
/* A relational rowset formed by using the XQuery value method */
SELECT EventInfo.value('(event/@timestamp)[1]','datetimeoffset') AS timestamp,
       EventInfo.value('(event/@name)[1]','sysname') AS event_name,
       EventInfo.value('(event/data/value)[1]','nvarchar(max)') AS sql_batch_text
FROM EventNode
ORDER BY timestamp DESC;
```

---

## Related content

- [Extended Events in Azure SQL Database](xevent-db-diff-from-svr.md)
- [Extended Events](/sql/relational-databases/extended-events/extended-events)
- [ring_buffer target](/sql/relational-databases/extended-events/targets-for-extended-events-in-sql-server#ring_buffer-target)
- [CREATE EVENT SESSION (Transact-SQL)](/sql/t-sql/statements/create-event-session-transact-sql)
- [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](/sql/t-sql/statements/create-database-scoped-credential-transact-sql)
- [CREATE CREDENTIAL (Transact-SQL)](/sql/t-sql/statements/create-credential-transact-sql)
