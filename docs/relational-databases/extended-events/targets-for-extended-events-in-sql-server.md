---
title: "Targets for Extended Events"
description: This article explains package0 targets for Extended Events. Learn about target abilities in gathering and reporting data and target parameters.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 01/22/2024
ms.service: sql
ms.subservice: xevents
ms.topic: conceptual
monikerRange: "=azuresqldb-current || =azuresqldb-mi-current || >=sql-server-2016 || >=sql-server-linux-2017"
---
# Targets for Extended Events

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article explains when and how to use Extended Events targets. For each target, the present article explains:

- Its abilities in gathering and reporting the data sent by events
- Its parameters, except where the parameter is self-explanatory

The following table describes the availability of each target type in different database engines.

| Target type | SQL Server | Azure SQL Database | Azure SQL Managed Instance |
| :--- | --- | --- | --- |
| [etw_classic_sync_target](#etw_classic_sync_target-target) | Yes | No | No |
| [event_counter](#event_counter-target) | Yes | Yes | Yes |
| [event_file](#event_file-target) | Yes | Yes | Yes |
| [event_stream](#event_stream-target) | Yes | Yes | Yes |
| [histogram](#histogram-target) | Yes | Yes | Yes |
| [pair_matching](#pair_matching-target) | Yes | No | No |
| [ring_buffer](#ring_buffer-target) | Yes | Yes | Yes |

### Prerequisites

To make the most use of this article, you should:

- Be familiar with the basics of Extended Events, as described in [Quickstart: Extended Events](quick-start-extended-events-in-sql-server.md).

- Have installed a recent version of SQL Server Management Studio (SSMS). For more information, see [Download SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md).

- In SSMS, know how to use Object Explorer to right-click the target node under your event session, for [easy viewing of the output data](advanced-viewing-of-target-data-from-extended-events-in-sql-server.md).

## Parameters, actions, and fields

The [CREATE EVENT SESSION](../../t-sql/statements/create-event-session-transact-sql.md) statement is central to Extended Events. To write the statement, you need the following:

- The events you want to add to the session
- The fields associated with each chosen event
- The parameters associated with each target you want to add to the sessions

SELECT statements, which return such lists from system views are available to copy from the following article, in its section C:

- [SELECTs and JOINs From System Views for Extended Events in SQL Server](selects-and-joins-from-system-views-for-extended-events-in-sql-server.md)
  - [C.4](selects-and-joins-from-system-views-for-extended-events-in-sql-server.md#section_C_4_data_fields) SELECT fields for an event.
  - [C.6](selects-and-joins-from-system-views-for-extended-events-in-sql-server.md#section_C_6_parameters_targets) SELECT parameters for a target.
  - [C.3](selects-and-joins-from-system-views-for-extended-events-in-sql-server.md#section_C_3_select_all_available_objects) SELECT actions.

You can see parameters, fields, and actions used in the context of an actual `CREATE EVENT SESSION` statement, from [section B2](selects-and-joins-from-system-views-for-extended-events-in-sql-server.md#section_B_2_TSQL_perspective) (T-SQL perspective).

## etw_classic_sync_target target

In SQL Server, Extended Events can interoperate with Event Tracing for Windows (ETW) to monitor system activity. For more information, see:

- [Event Tracing for Windows Target](event-tracing-for-windows-target.md)
- [Monitor System Activity Using Extended Events](monitor-system-activity-using-extended-events.md)

This ETW target processes the data it receives *synchronously*, whereas most targets process *asynchronously*.

::: moniker range="= azuresqldb-current || = azuresqldb-mi-current "
> [!NOTE]  
> [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] don't support the `etw_classic_sync_target` target. As an alternative, use the `event_file` target with blobs stored in Azure Storage.
::: moniker-end

## event_counter target

The `event_counter` target counts how many times each specified event occurs.

Unlike most other targets:

- The `event_counter` target has no parameters.
- The `event_counter` target processes the data it receives *synchronously*.

#### Example output captured by the event_counter target

```output
package_name   event_name         count
------------   ----------         -----
sqlserver      checkpoint_begin   4
```

Next is the `CREATE EVENT SESSION` statement that returned the previous results. For this example, the `package0.counter` field was used in the `WHERE` clause predicate to stop counting after the count reaches 4.

```sql
CREATE EVENT SESSION [event_counter_1]
    ON SERVER
    ADD EVENT sqlserver.checkpoint_begin   -- Test by issuing CHECKPOINT; statements.
    (
        WHERE [package0].[counter] <= 4   -- A predicate filter.
    )
    ADD TARGET package0.event_counter
    WITH
    (
        MAX_MEMORY = 4096 KB,
        MAX_DISPATCH_LATENCY = 3 SECONDS
    );
```

## event_file target

The `event_file` target writes event session output from buffer to a disk file or to a blob in Azure Storage:

- You specify the `filename` parameter in the `ADD TARGET` clause. The file extension must be `xel`.
- The file name you choose is used by the system as a prefix to which a date-time based long integer is appended, followed by the `xel` extension.

::: moniker range="= azuresqldb-current || = azuresqldb-mi-current "

> [!NOTE]  
> [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] only blobs in Azure Storage as the value of the `filename` parameter.
>  
> For an `event_file` code example for SQL Database or SQL Managed Instance, see [Event File target code for Extended Events in SQL Database](/azure/sql-database/sql-database-xevent-code-event-file).

::: moniker-end

#### CREATE EVENT SESSION with event_file target

Here's an example of the `CREATE EVENT SESSION` with an `ADD TARGET` clause that adds an `event_file` target.

```sql
CREATE EVENT SESSION [locks_acq_rel_eventfile_22]
    ON SERVER
    ADD EVENT sqlserver.lock_acquired
    (
        SET
            collect_database_name=(1),
            collect_resource_description=(1)
        ACTION (sqlserver.sql_text,sqlserver.transaction_id)
        WHERE
        (
            [database_name]=N'InMemTest2'
            AND
            [object_id]=370100359
        )
    ),
    ADD EVENT sqlserver.lock_released
    (
        SET
            collect_database_name=1,
            collect_resource_description=1
        ACTION(sqlserver.sql_text,sqlserver.transaction_id)
        WHERE
        (
            [database_name]=N'InMemTest2'
            AND
            [object_id]=370100359
        )
    )
    ADD TARGET package0.event_counter,
    ADD TARGET package0.event_file
    (
        SET filename=N'C:\temp\locks_acq_rel_eventfile_22-.xel'
    )
    WITH
    (
        MAX_MEMORY=4096 KB,
        MAX_DISPATCH_LATENCY=10 SECONDS
    );
```

#### sys.fn_xe_file_target_read_file() function

The `event_file` target stores the data it receives in a binary format that's not human readable. The [sys.fn_xe_file_target_read_file](../system-functions/sys-fn-xe-file-target-read-file-transact-sql.md) function lets you represent the contents of an `xel` file as a relational rowset.

For SQL Server 2016 and later versions, use a `SELECT` statement similar to the following example.

```sql
SELECT f.*
--,CAST(f.event_data AS XML)  AS [Event-Data-Cast-To-XML]  -- Optional
FROM sys.fn_xe_file_target_read_file(
    'C:\temp\locks_acq_rel_eventfile_22-*.xel', NULL, NULL, NULL)  AS f;
```

For SQL Server 2014, use a `SELECT` statement similar to the following example. After SQL Server 2014, the `xem` files are no longer used.

```sql
SELECT f.*
--,CAST(f.event_data AS XML)  AS [Event-Data-Cast-To-XML]  -- Optional
FROM sys.fn_xe_file_target_read_file(
    'C:\temp\locks_acq_rel_eventfile_22-*.xel', 'C:\temp\metafile.xem', NULL, NULL) AS f;
```

In both of these examples, the `*` wildcard is used to read all `xel` files that start with the specified prefix.

In Azure SQL Database, you can call the `sys.fn_xe_file_target_read_file()` function after you create a *database-scoped* credential containing a SAS token with the `Read` and `List` permissions on the container with the `xel` blobs:

```sql
/*
Create a master key to protect the secret of the credential
*/
IF NOT EXISTS (
    SELECT 1
    FROM sys.symmetric_keys
    WHERE name = '##MS_DatabaseMasterKey##'
)
CREATE MASTER KEY;

/*
(Re-)create a database scoped credential.
The name of the credential must match the URI of the blob container.
*/
IF EXISTS (
    SELECT *
    FROM sys.database_credentials
    WHERE name = 'https://exampleaccount4xe.blob.core.windows.net/extended-events-container'
)
DROP DATABASE SCOPED CREDENTIAL [https://exampleaccount4xe.blob.core.windows.net/extended-events-container];

/*
The secret is the SAS token for the container. The Read and List permissions are set.
*/
CREATE DATABASE SCOPED CREDENTIAL [https://exampleaccount4xe.blob.core.windows.net/extended-events-container]
    WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
        SECRET = 'sp=rl&st=2023-10-09T22:12:54Z&se=2023-10-10T06:12:54Z&spr=https&sv=2022-11-02&sr=c&sig=REDACTED';

/*
Return event session data
*/
SELECT f.*
--,CAST(f.event_data AS XML)  AS [Event-Data-Cast-To-XML]  -- Optional
FROM sys.fn_xe_file_target_read_file('https://exampleaccount4xe.blob.core.windows.net/extended-events-container/event-session-1', DEFAULT, DEFAULT, DEFAULT) AS f;
```

In Azure SQL Managed Instance, you can call the `sys.fn_xe_file_target_read_file()` function after you create a *server* credential containing a SAS token with the `Read` and `List` permissions on the container with the `xel` blobs:

```sql
IF NOT EXISTS (
    SELECT 1
    FROM sys.symmetric_keys
    WHERE name = '##MS_DatabaseMasterKey##'
)
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'REDACTED';

/*
(Re-)create a database scoped credential.
The name of the credential must match the URI of the blob container.
*/
IF EXISTS (
    SELECT *
    FROM sys.credentials
    WHERE name = 'https://exampleaccount4xe.blob.core.windows.net/extended-events-container'
)
DROP CREDENTIAL [https://exampleaccount4xe.blob.core.windows.net/extended-events-container];

/*
The secret is the SAS token for the container. The Read and List permissions are set.
*/
CREATE CREDENTIAL [https://exampleaccount4xe.blob.core.windows.net/extended-events-container]
    WITH IDENTITY = 'SHARED ACCESS SIGNATURE',
        SECRET = 'sp=rl&st=2023-10-09T22:12:54Z&se=2023-10-10T06:12:54Z&spr=https&sv=2022-11-02&sr=c&sig=REDACTED';

/*
Return event session data
*/
SELECT f.*
--,CAST(f.event_data AS XML)  AS [Event-Data-Cast-To-XML]  -- Optional
FROM sys.fn_xe_file_target_read_file('https://exampleaccount4xe.blob.core.windows.net/extended-events-container/event-session-1', DEFAULT, DEFAULT, DEFAULT) AS f;
```

> [!TIP]  
> If you specify a blob name prefix instead of the full blob name in the first argument of `sys.fn_xe_file_target_read_file()`, the function will return data from all blobs in the container that match the prefix. This lets you retrieve data from all rollover files of a given event session without using the `*` wildcard, which isn't supported by Azure Storage.
>  
> The previous Azure SQL examples omit the `xel` extension to read all rollover files for a session named `event-session-1`.

#### Data stored in the event_file target

This is an example of data returned from `sys.fn_xe_file_target_read_file` in [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions.

```output
module_guid                            package_guid                           object_name     event_data                                                                                                                                                                                                                                                                                          file_name                                                      file_offset
-----------                            ------------                           -----------     ----------                                                                                                                                                                                                                                                                                          ---------                                                      -----------
D5149520-6282-11DE-8A39-0800200C9A66   03FDA7D0-91BA-45F8-9875-8B6DD0B8E9F2   lock_acquired   <event name="lock_acquired" package="sqlserver" timestamp="2016-08-07T20:13:35.827Z"><action name="transaction_id" package="sqlserver"><value>39194</value></action><action name="sql_text" package="sqlserver"><value><![CDATA[  select top 1 * from dbo.T_Target;  ]]></value></action></event>   C:\temp\locks_acq_rel_eventfile_22-_0_131150744126230000.xel   11776
D5149520-6282-11DE-8A39-0800200C9A66   03FDA7D0-91BA-45F8-9875-8B6DD0B8E9F2   lock_released   <event name="lock_released" package="sqlserver" timestamp="2016-08-07T20:13:35.832Z"><action name="transaction_id" package="sqlserver"><value>39194</value></action><action name="sql_text" package="sqlserver"><value><![CDATA[  select top 1 * from dbo.T_Target;  ]]></value></action></event>   C:\temp\locks_acq_rel_eventfile_22-_0_131150744126230000.xel   11776
```

## histogram target

The `histogram` target can:

- Count occurrences for several items separately
- Count occurrences of different types of items:
  - Event fields
  - Actions

The `histogram` target processes the data it receives *synchronously*.

The `source_type` parameter is the key to controlling the histogram target:

- `source_type=0`: collect data for an *event field*.
- `source_type=1`: collect data for an *action*. This is the default.

The `slots` parameter default is 256. If you assign another value, the value is rounded up to the next power of 2. For example, slots=59 would be rounded up to 64. The maximum number of histogram slots for a `histogram` target is 16384.

When using `histogram` as the target, you might sometimes see unexpected results. Some events might not appear in the expected slots, while other slots might show a higher than expected count of events.

This might happen if a hash collision occurs when assigning events to slots. While this is rare, if a hash collision occurs, an event that should be counted in one slot is counted in another. For this reason, care should be taken assuming that an event didn't occur just because the count in a particular slot shows as zero.

As an example, consider the following scenario:

- You set up an Extended Events session, using `histogram` as the target and grouping by `object_id`, to collect stored procedure execution.
- You execute the Stored Procedure A. Then, you execute Stored Procedure B.

If the hash function returns the same value for the `object_id` of both stored procedures, the histogram shows Stored Procedure A being executed twice, and Stored Procedure B doesn't appear.

To mitigate this problem when the number of distinct values is relatively small, set the number of histogram slots higher than the square of expected distinct values. For example, if the `histogram` target has its `source` set to the `table_name` event field, and there are 20 tables in the database, then 20*20 = 400. The next power of 2 greater than 400 is 512, which is the recommended number of slots in this example.

### Histogram target with an action

In its `ADD TARGET ... (SET ...)` clause, the following `CREATE EVENT SESSION` statement specifies the target parameter assignment `source_type=1`. This means that the histogram target tracks an action.

In the present example, the `ADD EVENT ... (ACTION ...)` clause happens to offer only one action to choose, namely `sqlos.system_thread_id`. In the `ADD TARGET ... (SET ...)` clause, we see the assignment `source=N'sqlos.system_thread_id'`.

> [!NOTE]  
> It isn't possible to add more than one target of the same type per event session. This includes the `histogram` target. It's also not possible to have more than one source (action / event field) per `histogram` target. Therefore, a new event session is required to track any additional actions or event fields in a separate `histogram` target.

```sql
CREATE EVENT SESSION [histogram_lockacquired]
    ON SERVER
    ADD EVENT sqlserver.lock_acquired
        (
        ACTION
            (
            sqlos.system_thread_id
            )
        )
    ADD TARGET package0.histogram
        (
        SET
            filtering_event_name=N'sqlserver.lock_acquired',
            slots=16,
            source=N'sqlos.system_thread_id',
            source_type=1
        );
```

The following data was captured. The values in the `value` column are `system_thread_id` values. For instance, a total of 236 locks were taken under thread 6540.

```output
value   count
-----   -----
 6540     236
 9308      91
 9668      74
10144      49
 5244      44
 2396      28
```

#### SELECT to discover available actions

The [C.3](selects-and-joins-from-system-views-for-extended-events-in-sql-server.md#section_C_3_select_all_available_objects) `SELECT` statement can find the actions that the system has available for you to specify in the `CREATE EVENT SESSION` statement. In the `WHERE` clause, you would first edit the `o.name LIKE` filter to match the actions that interest you.

Next is a sample rowset returned by the C.3 `SELECT`. The `system_thread_id` action is seen in the second row.

```output
Package-Name   Action-Name                 Action-Description
------------   -----------                 ------------------
package0       collect_current_thread_id   Collect the current Windows thread ID
sqlos          system_thread_id            Collect current system thread ID
sqlserver      create_dump_all_threads     Create mini dump including all threads
sqlserver      create_dump_single_thread   Create mini dump for the current thread
```

### Histogram target with an event field

The following example sets `source_type=0`. The value assigned to `source` is an event field.

```sql
CREATE EVENT SESSION [histogram_checkpoint_dbid]
    ON SERVER
    ADD EVENT  sqlserver.checkpoint_begin
    ADD TARGET package0.histogram
    (
    SET
        filtering_event_name = N'sqlserver.checkpoint_begin',
        source               = N'database_id',
        source_type          = 0
    );
```

The following data was captured by the `histogram` target. The data shows that the database with ID 5 experienced 7 `checkpoint_begin` events.

```output
value   count
-----   -----
5       7
7       4
6       3
```

#### SELECT to discover available fields on your chosen event

The [C.4](selects-and-joins-from-system-views-for-extended-events-in-sql-server.md#section_C_4_data_fields) `SELECT` statement shows event fields that you can choose from. You would first edit the `o.name LIKE` filter to be your chosen event name.

The following rowset was returned by the C.4 `SELECT`. The rowset shows that `database_id` is the only field on the `checkpoint_begin` event that can supply values for the `histogram` target.

```output
Package-Name   Event-Name         Field-Name   Field-Description
------------   ----------         ----------   -----------------
sqlserver      checkpoint_begin   database_id  NULL
sqlserver      checkpoint_end     database_id  NULL
```

## pair_matching target

The `pair_matching` target enables you to detect start events that occur without a corresponding end event. For instance, it might be a problem when a `lock_acquired` event occurs but no matching `lock_released` event follows in a timely manner.

The system doesn't automatically match start and end events. Instead, you explain the matching to the system in your `CREATE EVENT SESSION` statement. When a start and end event are matched, the pair is discarded to focus on the unmatched start events.

#### Find matchable fields for the start and end event pair

By using the [C.4 SELECT](selects-and-joins-from-system-views-for-extended-events-in-sql-server.md#section_C_4_data_fields), we see in the following rowset there are about 16 fields for the `lock_acquired` event. The rowset displayed here has been manually split to show which fields our example matched on. For some fields such as `duration`, attempting to match is meaningless.

```output
Package-Name   Event-Name   Field-Name               Field-Description
------------   ----------   ----------               -----------------
sqlserver   lock_acquired   database_name            NULL
sqlserver   lock_acquired   mode                     NULL
sqlserver   lock_acquired   resource_0               The ID of the locked object, when lock_resource_type is OBJECT.
sqlserver   lock_acquired   resource_1               NULL
sqlserver   lock_acquired   resource_2               The ID of the lock partition, when lock_resource_type is OBJECT, and resource_1 is 0.
sqlserver   lock_acquired   transaction_id           NULL

sqlserver   lock_acquired   associated_object_id     The ID of the object that requested the lock that was acquired.
sqlserver   lock_acquired   database_id              NULL
sqlserver   lock_acquired   duration                 The time (in microseconds) between when the lock was requested and when it was canceled.
sqlserver   lock_acquired   lockspace_nest_id        NULL
sqlserver   lock_acquired   lockspace_sub_id         NULL
sqlserver   lock_acquired   lockspace_workspace_id   NULL
sqlserver   lock_acquired   object_id                The ID of the locked object, when lock_resource_type is OBJECT. For other lock resource types it will be 0
sqlserver   lock_acquired   owner_type               NULL
sqlserver   lock_acquired   resource_description     The description of the lock resource. The description depends on the type of lock. This is the same value as the resource_description column in the sys.dm_tran_locks view.
sqlserver   lock_acquired   resource_type            NULL
```

### An example of the pair_matching target

The following `CREATE EVENT SESSION` statement specifies two events, and two targets. The `pair_matching` target specifies two sets of fields to match the events into pairs. The sequence of comma-delimited fields assigned to `begin_matching_columns` and `end_matching_columns` must be the same. No tabs or newlines are allowed between the fields mentioned in the comma-delimited value, although spaces are allowed.

To narrow the results, we first selected from `sys.objects` to find the `object_id` of our test table. We added a filter for that one object ID in the `ADD EVENT ... (WHERE ...)` clause.

```sql
CREATE EVENT SESSION [pair_matching_lock_a_r_33]
    ON SERVER
    ADD EVENT sqlserver.lock_acquired
    (
        SET
            collect_database_name = 1,
            collect_resource_description = 1
        ACTION (sqlserver.transaction_id)
        WHERE
        (
            [database_name] = 'InMemTest2'
            AND
            [object_id] = 370100359
        )
    ),
    ADD EVENT sqlserver.lock_released
    (
        SET
            collect_database_name = 1,
            collect_resource_description = 1
        ACTION (sqlserver.transaction_id)
        WHERE
        (
            [database_name] = 'InMemTest2'
            AND
            [object_id] = 370100359
        )
    )
    ADD TARGET package0.event_counter,
    ADD TARGET package0.pair_matching
    (
        SET
            begin_event = N'sqlserver.lock_acquired',
            begin_matching_columns =
                N'resource_0, resource_1, resource_2, transaction_id, database_id',
            end_event = N'sqlserver.lock_released',
            end_matching_columns =
                N'resource_0, resource_1, resource_2, transaction_id, database_id',
            respond_to_memory_pressure = 1
    )
    WITH
    (
        MAX_MEMORY = 8192 KB,
        MAX_DISPATCH_LATENCY = 15 SECONDS
    );
```

To test the event session, we purposefully prevented two acquired locks from being released. We did this with the following T-SQL steps:

1. `BEGIN TRANSACTION`.
1. `UPDATE MyTable...`.
1. Purposefully not issue a `COMMIT TRANSACTION`, until after we examined the targets.
1. Later after testing, we issued a `COMMIT TRANSACTION`.

The simple `event_counter` target provided the following output rows. Because 52-50=2, the output implies we see 2 unpaired lock_acquired events when we examine the output from the pair-matching target.

```output
package_name   event_name      count
------------   ----------      -----
sqlserver      lock_acquired   52
sqlserver      lock_released   50
```

The `pair_matching` target provided the following output. As suggested by the `event_counter` output, we do indeed see the two `lock_acquired` rows. The fact that we see these rows at all means these two `lock_acquired` events are unpaired.

```output
package_name   event_name      timestamp                     database_name   duration   mode   object_id   owner_type   resource_0   resource_1   resource_2   resource_description   resource_type   transaction_id
------------   ----------      ---------                     -------------   --------   ----   ---------   ----------   ----------   ----------   ----------   --------------------   -------------   --------------
sqlserver      lock_acquired   2016-08-05 12:45:47.9980000   InMemTest2      0          S      370100359   Transaction  370100359    3            0            [INDEX_OPERATION]      OBJECT          34126
sqlserver      lock_acquired   2016-08-05 12:45:47.9980000   InMemTest2      0          IX     370100359   Transaction  370100359    0            0                                   OBJECT          34126
```

The rows for the unpaired `lock_acquired` events could include the T-SQL text provided by the `sqlserver.sql_text` action. This captures the query that acquired the locks.

## ring_buffer target

The `ring_buffer` target is handy for a quick and simple event collection in memory only. When you stop the event session, the stored output is discarded.

In this section, we also show how you can use XQuery to convert the XML representation of the ring buffer contents into a more readable relational rowset.

> [!TIP]  
> When adding a `ring_buffer` target, set its `MAX_MEMORY` parameter to 1024 KB or less. Using larger values might increase memory consumption unnecessarily.
>
> By default, `MAX_MEMORY` for a `ring_buffer` target isn't limited in SQL Server, and is limited to 32 MB in Azure SQL Database and Azure SQL Managed Instance.

You consume data from a `ring_buffer` target by converting it to XML, as shown in the following example. During this conversion, any data that doesn't fit into a 4-MB XML document is omitted. Therefore, even if you capture more events in the ring buffer by using larger `MAX_MEMORY` values (or by leaving this parameter at its default value), you might not be able to consume all of them because of the 4-MB limit on the XML document size, considering the overhead of XML markup and Unicode strings.

You know that the contents of the ring buffer are omitted during conversion to XML if the `truncated` attribute in the XML document is set to `1`, for example:

```xml
<RingBufferTarget truncated="1" processingTime="0" totalEventsProcessed="284" eventCount="284" droppedCount="0" memoryUsed="64139">
```

#### CREATE EVENT SESSION with a ring_buffer target

Here's an example of creating an event session with a `ring_buffer` target. In this example, the `MAX_MEMORY` parameter appears twice: once to set the `ring_buffer` target memory to 1024 KB, and once to set the event session buffer memory to 2 MB.

```sql
CREATE EVENT SESSION [ring_buffer_lock_acquired_4]
    ON SERVER
    ADD EVENT sqlserver.lock_acquired
    (
        SET collect_resource_description=(1)
        ACTION(sqlserver.database_name)
        WHERE
        (
            [object_id]=(370100359)  -- ID of MyTable
            AND
            sqlserver.database_name='InMemTest2'
        )
    )
    ADD TARGET package0.ring_buffer
    (
        SET MAX_EVENTS_LIMIT = 98,
            MAX_MEMORY = 1024
    )
    WITH
    (
        MAX_MEMORY = 2 MB,
        MAX_DISPATCH_LATENCY = 3 SECONDS
    );
```

### XML output received for lock_acquired by the ring_buffer target

When retrieved by a `SELECT` statement, the content of a ring buffer is presented as an XML document. An example is shown next. However, for brevity, all but two `<event>` elements have been removed. Further, within each `<event>`, a handful of `<data>` elements have been removed as well.

```xml
<RingBufferTarget truncated="0" processingTime="0" totalEventsProcessed="6" eventCount="6" droppedCount="0" memoryUsed="1032">
  <event name="lock_acquired" package="sqlserver" timestamp="2016-08-05T23:59:53.987Z">
    <data name="mode">
      <type name="lock_mode" package="sqlserver"></type>
      <value>1</value>
      <text><![CDATA[SCH_S]]></text>
    </data>
    <data name="transaction_id">
      <type name="int64" package="package0"></type>
      <value>111030</value>
    </data>
    <data name="database_id">
      <type name="uint32" package="package0"></type>
      <value>5</value>
    </data>
    <data name="resource_0">
      <type name="uint32" package="package0"></type>
      <value>370100359</value>
    </data>
    <data name="resource_1">
      <type name="uint32" package="package0"></type>
      <value>0</value>
    </data>
    <data name="resource_2">
      <type name="uint32" package="package0"></type>
      <value>0</value>
    </data>
    <data name="database_name">
      <type name="unicode_string" package="package0"></type>
      <value><![CDATA[]]></value>
    </data>
    <action name="database_name" package="sqlserver">
      <type name="unicode_string" package="package0"></type>
      <value><![CDATA[InMemTest2]]></value>
    </action>
  </event>
  <event name="lock_acquired" package="sqlserver" timestamp="2016-08-05T23:59:56.012Z">
    <data name="mode">
      <type name="lock_mode" package="sqlserver"></type>
      <value>1</value>
      <text><![CDATA[SCH_S]]></text>
    </data>
    <data name="transaction_id">
      <type name="int64" package="package0"></type>
      <value>111039</value>
    </data>
    <data name="database_id">
      <type name="uint32" package="package0"></type>
      <value>5</value>
    </data>
    <data name="resource_0">
      <type name="uint32" package="package0"></type>
      <value>370100359</value>
    </data>
    <data name="resource_1">
      <type name="uint32" package="package0"></type>
      <value>0</value>
    </data>
    <data name="resource_2">
      <type name="uint32" package="package0"></type>
      <value>0</value>
    </data>
    <data name="database_name">
      <type name="unicode_string" package="package0"></type>
      <value><![CDATA[]]></value>
    </data>
    <action name="database_name" package="sqlserver">
      <type name="unicode_string" package="package0"></type>
      <value><![CDATA[InMemTest2]]></value>
    </action>
  </event>
</RingBufferTarget>
```

To see the preceding XML, you can issue the following `SELECT` while the event session is active. The XML data is retrieved from the system view `sys.dm_xe_session_targets`.

```sql
SELECT CAST(LocksAcquired.TargetXml AS XML) AS RBufXml
INTO #XmlAsTable
FROM (
    SELECT CAST(t.target_data AS XML) AS TargetXml
    FROM sys.dm_xe_session_targets AS t
    INNER JOIN sys.dm_xe_sessions AS s
        ON s.address = t.event_session_address
    WHERE t.target_name = 'ring_buffer'
        AND s.name = 'ring_buffer_lock_acquired_4'
) AS LocksAcquired;

SELECT *
FROM #XmlAsTable;
```

### XQuery to see the XML as a rowset

To see the preceding XML as a relational rowset, continue from the preceding `SELECT` statement by issuing the following T-SQL. The commented lines explain each use of XQuery.

```sql
SELECT
    -- (A)
    ObjectLocks.value('(@timestamp)[1]', 'datetime') AS [OccurredDtTm],
    -- (B)
    ObjectLocks.value('(data[@name="mode"]/text)[1]', 'nvarchar(32)') AS [Mode],
    -- (C)
    ObjectLocks.value('(data[@name="transaction_id"]/value)[1]', 'bigint') AS [TxnId],
    -- (D)
    ObjectLocks.value('(action[@name="database_name" and @package="sqlserver"]/value)[1]', 'nvarchar(128)') AS [DatabaseName]
FROM #XmlAsTable
CROSS APPLY
    -- (E)
    TargetDateAsXml.nodes('/RingBufferTarget/event[@name="lock_acquired"]') AS T(ObjectLocks);
```

#### XQuery notes from preceding SELECT

(A)

- timestamp= attribute's value, on `<event>` element.
- The `'(...)[1]'` construct ensures only one value returned per iteration, as is a required limitation of the `.value()` XQuery method of XML data type variable and columns.

(B)

- `<text>` element's inner value, within a `<data>` element, which has its name= attribute equal to `mode`.

(C)

- `<value>` elements inner value, within a `<data>` element, which has its name= attribute equal to `transaction_id`.

(D)

- `<event>` contains `<action>`.
- `<action>` having name= attribute equal to `database_name`, and package= attribute equal to `sqlserver` (not `package0`), get the inner value of `<value>` element.

(E)

- `CROSS APPLY` causes processing to repeat for every individual `<event>` element, which has its `name` attribute equal to `lock_acquired`.
- This applies to the XML returned by the preceding `FROM` clause.

#### Output from XQuery SELECT

Next is the rowset generated by the preceding T-SQL, which includes XQuery.

```output
OccurredDtTm              Mode    DatabaseName
------------              ----    ------------
2016-08-05 23:59:53.987   SCH_S   InMemTest2
2016-08-05 23:59:56.013   SCH_S   InMemTest2
```

## event_stream target

The `event_stream` target can only be used in .NET programs written in languages like C#. C# and other .NET developers can access an event stream through .NET Framework classes in the `Microsoft.SqlServer.XEvents.Linq` namespace. This target can't be used in T-SQL.

If you encounter error 25726, `The event data stream was disconnected because there were too many outstanding events. To avoid this error either remove events or actions from your session or add a more restrictive predicate filter to your session.` when reading from the `event_stream` target, it means that the event stream filled up with data faster than the client could consume the data. This causes the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] to disconnect from the event stream to avoid affecting [!INCLUDE [ssde-md](../../includes/ssde-md.md)] performance.

### XEvent namespaces

- [Microsoft.SqlServer.Management.XEvent Namespace](/dotnet/api/microsoft.sqlserver.management.xevent)
- [Microsoft.SqlServer.XEvent.Linq Namespace](/dotnet/api/microsoft.sqlserver.xevent.linq)
