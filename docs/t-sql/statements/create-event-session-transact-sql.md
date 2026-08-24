---
title: "CREATE EVENT SESSION (Transact-SQL)"
description: Creates an Extended Events session that identifies the source of the events, the event session targets, and the event session options.
author: markingmyname
ms.author: maghan
ms.reviewer: dfurman, randolphwest
ms.date: 10/02/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "CREATE EVENT SESSION"
  - "SESSION"
  - "EVENT SESSION"
  - "SESSION_TSQL"
  - "EVENT_SESSION_TSQL"
  - "CREATE_EVENT_SESSION_TSQL"
helpviewer_keywords:
  - "event sessions [SQL Server]"
  - "CREATE EVENT SESSION statement"
dev_langs:
  - TSQL
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# CREATE EVENT SESSION (Transact-SQL)

[!INCLUDE [SQL Server SQL Database SQL MI FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Creates an Extended Events session that identifies the events to collect, the event session targets, and the event session options.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
CREATE EVENT SESSION event_session_name
ON { SERVER | DATABASE }
{
    <event_definition> [ , ...n ]
    [ <event_target_definition> [ , ...n ] ]
    [ WITH ( <event_session_options> [ , ...n ] ) ]
}
;

<event_definition>::=
{
    ADD EVENT [event_module_guid].event_package_name.event_name
         [ ( {
                 [ SET { event_customizable_attribute = <value> [ , ...n ] } ]
                 [ ACTION ( { [event_module_guid].event_package_name.action_name [ , ...n ] } ) ]
                 [ WHERE <predicate_expression> ]
        } ) ]
}

<predicate_expression> ::=
{
    [ NOT ] <predicate_factor> | { ( <predicate_expression> ) }
    [ { AND | OR } [ NOT ] { <predicate_factor> | ( <predicate_expression> ) } ]
    [ , ...n ]
}

<predicate_factor>::=
{
    <predicate_leaf> | ( <predicate_expression> )
}

<predicate_leaf>::=
{
      <predicate_source_declaration> { = | < > | != | > | >= | < | <= } <value>
    | [event_module_guid].event_package_name.predicate_compare_name ( <predicate_source_declaration> , <value> )
}

<predicate_source_declaration>::=
{
    event_field_name | ( [event_module_guid].event_package_name.predicate_source_name )
}

<value>::=
{
    number | 'string'
}

<event_target_definition>::=
{
    ADD TARGET [event_module_guid].event_package_name.target_name
        [ ( SET { target_parameter_name = <value> [ , ...n ] } ) ]
}

<event_session_options>::=
{
    [       MAX_MEMORY = size [ KB | MB ] ]
    [ [ , ] EVENT_RETENTION_MODE = { ALLOW_SINGLE_EVENT_LOSS | ALLOW_MULTIPLE_EVENT_LOSS | NO_EVENT_LOSS } ]
    [ [ , ] MAX_DISPATCH_LATENCY = { seconds SECONDS | INFINITE } ]
    [ [ , ] MAX_EVENT_SIZE = size [ KB | MB ] ]
    [ [ , ] MEMORY_PARTITION_MODE = { NONE | PER_NODE | PER_CPU } ]
    [ [ , ] TRACK_CAUSALITY = { ON | OFF } ]
    [ [ , ] STARTUP_STATE = { ON | OFF } ]
    [ [ , ] MAX_DURATION = { <time duration> { SECONDS | MINUTES | HOURS | DAYS } | UNLIMITED } ]
}
```

## Arguments

#### *event_session_name*

The user-defined name for the event session. *event_session_name* is alphanumeric, can be up to 128 characters, must be unique within an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], and must comply with the rules for [Database identifiers](../../relational-databases/databases/database-identifiers.md).

#### ON { SERVER | DATABASE }

Determines whether the event session is in the context of the server or database.

[!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)] require `DATABASE`.

#### ADD EVENT [*event_module_guid*].*event_package_name*.*event_name*

The event to associate with the event session, where:

- *event_module_guid* is the GUID for the module that contains the event.
- *event_package_name* is the package that contains the event.
- *event_name* is the event name.

Available events can be found by executing the following query:

```sql
SELECT o.name AS event_name,
       o.description AS event_description,
       p.name AS package_name,
       p.description AS package_description
FROM sys.dm_xe_objects AS o
     INNER JOIN sys.dm_xe_packages AS p
         ON o.package_guid = p.guid
WHERE o.object_type = 'event'
ORDER BY event_name ASC;
```

#### SET { *event_customizable_attribute* = \<value> [ ,...*n* ] }

Customizable attributes for the event.

Customizable attributes for a given event can be found by executing the following query:

```sql
SELECT object_name,
       name AS column_name,
       type_name,
       column_value,
       description
FROM sys.dm_xe_object_columns
WHERE object_name = 'event-name-placeholder'
      AND column_type = 'customizable'
ORDER BY column_name ASC;
```

#### ACTION ( { [*event_module_guid*].*event_package_name*.*action_name* [ ,...*n* ] })

The action to associate with the event, where:

- *event_module_guid* is the GUID for the module that contains the action.
- *event_package_name* is the package that contains the action.
- *action_name* is the name of the action.

Available actions can be found by executing the following query:

```sql
SELECT o.name AS action_name,
       o.description AS action_description,
       p.name AS package_name,
       p.description AS package_description
FROM sys.dm_xe_objects AS o
     INNER JOIN sys.dm_xe_packages AS p
         ON o.package_guid = p.guid
WHERE o.object_type = 'action'
ORDER BY action_name ASC;
```

#### WHERE \<predicate_expression>

Specifies the predicate expression used to determine if an event should be processed. If \<predicate_expression> is true, the event is processed further by the actions and targets for the session. If \<predicate_expression> is false, the event is dropped, avoiding additional action and target processing. Each predicate expression is limited to 3,000 characters.

#### *event_field_name*

The name of the event field that identifies the predicate source.

The fields for an event can be found by executing the following query:

```sql
SELECT oc.name AS field_name,
       oc.type_name AS field_type,
       oc.description AS field_description
FROM sys.dm_xe_objects AS o
INNER JOIN sys.dm_xe_packages AS p
ON o.package_guid = p.guid
INNER JOIN sys.dm_xe_object_columns AS oc
ON o.name = oc.object_name
   AND
   o.package_guid = oc.object_package_guid
WHERE o.object_type = 'event'
      AND
      o.name = 'event-name-placeholder'
      AND
      oc.column_type = 'data'
ORDER BY field_name ASC;
```

#### [*event_module_guid*].*event_package_name*.*predicate_source_name*

The name of the global predicate source where:

- *event_module_guid* is the GUID for the module that contains the event.
- *event_package_name* is the package that contains the predicate source object.
- *predicate_source_name* is the name of the predicate source.

Predicate sources can be found by executing the following query:

```sql
SELECT o.name AS predicate_source_name,
       o.description AS predicate_source_description,
       p.name AS package_name,
       p.description AS package_description
FROM sys.dm_xe_objects AS o
     INNER JOIN sys.dm_xe_packages AS p
         ON o.package_guid = p.guid
WHERE o.object_type = 'pred_source'
ORDER BY predicate_source ASC;
```

#### [*event_module_guid*].*event_package_name*.*predicate_compare_name*

The name of the predicate comparator object, where:

- *event_module_guid* is the GUID for the module that contains the event.
- *event_package_name* is the package that contains the predicate comparator object.
- *predicate_compare_name* is the predicate comparator name.

Predicate comparators can be found by executing the following query:

```sql
SELECT o.name AS predicate_comparator_name,
       o.description AS predicate_comparator_description,
       p.name AS package_name,
       p.description AS package_description
FROM sys.dm_xe_objects AS o
     INNER JOIN sys.dm_xe_packages AS p
         ON o.package_guid = p.guid
WHERE o.object_type = 'pred_compare'
ORDER BY predicate_comparator ASC;
```

#### *number*

Any numeric type that can be represented as a 64-bit integer.

#### '*string*'

Either an ANSI or Unicode string as required by the predicate comparator. No implicit string type conversion is performed for the predicate compare functions. Passing the value of an unexpected type results in an error.

#### ADD TARGET [*event_module_guid*].*event_package_name*.*target_name*

Is the target to associate with the event session, where:

- *event_module_guid* is the GUID for the module that contains the target.
- *event_package_name* is the package that contains the target.
- *target_name* is the target name.

Available targets can be found by executing the following query:

```sql
SELECT o.name AS target_name,
       o.description AS target_description,
       o.capabilities_desc,
       p.name AS package_name,
       p.description AS package_description
FROM sys.dm_xe_objects AS o
     INNER JOIN sys.dm_xe_packages AS p
         ON o.package_guid = p.guid
WHERE o.object_type = 'target'
ORDER BY target_name ASC;
```

An event session can have zero, one, or many [targets](../../relational-databases/extended-events/targets-for-extended-events-in-sql-server.md). All targets added to an event session must be different. For example, you cannot add a second `event_file` target to a session that already has an `event_file` target.

For more information, including usage examples for commonly used targets, see [Extended Events targets](../../relational-databases/extended-events/targets-for-extended-events-in-sql-server.md).

#### SET { *target_parameter_name* = \<value> [ , ...*n* ] }

Sets a target parameter.

To see all target parameters and their descriptions, execute the following query, replacing `target-name-placeholder` with the target name, such as `event_file`, `ring_buffer`, `histogram`, etc.:

```sql
SELECT name AS target_parameter_name,
       column_value AS default_value,
       description
FROM sys.dm_xe_object_columns
WHERE column_type = 'customizable'
      AND object_name = 'target-name-placeholder';
```

> [!IMPORTANT]  
> If you're using the ring buffer target, we recommend that you set the `MAX_MEMORY` *target* parameter (distinct from the `MAX_MEMORY` *session* parameter) to 1,024 kilobytes (KB) or less to help avoid possible data truncation of the XML output.

For more information about target types, see [Extended Events targets](../../relational-databases/extended-events/targets-for-extended-events-in-sql-server.md).

#### WITH ( \<event_session_options> [ ,...*n* ] )

Specifies the options to use with the event session.

#### MAX_MEMORY = *size* [ KB | MB ]

Specifies the maximum amount of memory to allocate to the session for event buffering. The default is 4 MB. *size* is a whole number and can be a kilobyte (KB) or a megabyte (MB) value. The maximum amount can't exceed 2 GB (2,048 MB). However, using memory values in GB range isn't recommended.

#### EVENT_RETENTION_MODE = { ALLOW_SINGLE_EVENT_LOSS | ALLOW_MULTIPLE_EVENT_LOSS | NO_EVENT_LOSS }

Specifies the event retention mode to use for handling event loss.

- ALLOW_SINGLE_EVENT_LOSS

  An event can be lost from the session. A single event is only dropped when all the event buffers are full. Losing a single event when event buffers are full minimizes the performance impact while also minimizing the loss of data in the processed eventstream.

- ALLOW_MULTIPLE_EVENT_LOSS

  Full event buffers containing multiple events can be lost from the session. The number of events lost is dependent upon the memory size allocated to the session, the partitioning of the memory, and the size of the events in the buffer. This option generally avoids performance impact on the server when event buffers are quickly filled, but large numbers of events can be lost from the session.

- NO_EVENT_LOSS

  No event loss is allowed. This option ensures that all events raised are retained. Using this option forces all tasks that fire events to wait until space is available in an event buffer. Using NO_EVENT_LOSS can cause detectable performance issues while the event session is active. User sessions and queries might stall while waiting for events to be flushed from the buffer.

  > [!NOTE]  
  > For the event file targets in Azure SQL Database, [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], and Azure SQL Managed Instance (with the **SQL Server 2025** or **Always-up-to-date** [update policy](/azure/azure-sql/managed-instance/update-policy)), starting from June 2024, `NO_EVENT_LOSS` behaves the same as `ALLOW_SINGLE_EVENT_LOSS`. If you specify `NO_EVENT_LOSS`, a warning with message ID 25665, severity 10, and message `This target doesn't support the NO_EVENT_LOSS event retention mode. The ALLOW_SINGLE_EVENT_LOSS retention mode is used instead.` is returned, and the session is created.
  >
  > This change avoids connection timeouts, failover delays, and other issues that can reduce database availability when `NO_EVENT_LOSS` is used with event file targets in Azure blob storage.
  >
  > `NO_EVENT_LOSS` is planned for removal as a supported `EVENT_RETENTION_MODE` argument in future updates to Azure SQL Database, [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)], and Azure SQL Managed Instance. Avoid using this feature in new development work, and plan to modify applications that currently use this feature.

#### MAX_DISPATCH_LATENCY = { *seconds* SECONDS | INFINITE }

Specifies the amount of time that events are buffered in memory before being dispatched to event session targets. By default, this value is set to 30 seconds.

- *seconds* `SECONDS`

  The time, in seconds, to wait before starting to flush buffers to targets. *seconds* is a whole number. The minimum latency value is 1 second. However, 0 can be used to specify INFINITE latency.

- INFINITE

  Flush buffers to targets only when the buffers are full, or when the event session closes.

#### MAX_EVENT_SIZE = *size* [ KB | MB ]

Specifies the maximum allowable size for events. MAX_EVENT_SIZE should only be set to allow single events larger than MAX_MEMORY; setting it to less than MAX_MEMORY raises an error. *size* is a whole number and can be a kilobyte (KB) or a megabyte (MB) value. If *size* is specified in kilobytes, the minimum allowable size is 64 KB. When MAX_EVENT_SIZE is set, two buffers of *size* are created in addition to MAX_MEMORY, and the total memory used for event buffering is MAX_MEMORY + 2 * MAX_EVENT_SIZE.

#### MEMORY_PARTITION_MODE = { NONE | PER_NODE | PER_CPU }

Specifies the affinity of event buffers. Options other than `NONE` result in more buffers and higher memory consumption, but can avoid contention and improve performance on larger machines.

- NONE

  A single set of buffers are created within the [!INCLUDE [ssDE](../../includes/ssde-md.md)] instance.

- PER_NODE

  A set of buffers is created for each NUMA node.

- PER_CPU

  A set of buffers is created for each CPU.

#### TRACK_CAUSALITY = { ON | OFF }

Specifies whether or not causality is tracked. If enabled, causality allows related events on different server connections to be correlated together.

#### STARTUP_STATE = { ON | OFF }

Specifies whether or not to start this event session automatically when [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] starts.

> [!NOTE]  
> If `STARTUP_STATE = ON`, the event session starts when the [!INCLUDE [ssDE](../../includes/ssde-md.md)] is stopped and then restarted. To start the event session immediately, use `ALTER EVENT SESSION ... ON SERVER STATE = START`.

- ON

  The event session is started at startup.

- OFF

  The event session isn't started at startup.

#### MAX_DURATION = { *time duration* { SECONDS | MINUTES | HOURS | DAYS } | UNLIMITED }

**Applies to**: [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], [!INCLUDE [ssazure-sqlmi-autd](../../includes/applies-to-version/ssazure-sqlmi-autd.md)], [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)] and later versions

- UNLIMITED

  Creates an event session that runs indefinitely once started, until stopped using the `ALTER EVENT SESSION ... STATE = STOP` statement. This is the default if `MAX_DURATION` isn't specified.

- *time duration* SECONDS | MINUTES | HOURS | DAYS

  Creates an event session that stops automatically after the specified time elapses after the session start. The maximum supported duration is 2,147,483 seconds, or 35,792 minutes, or 596 hours, or 24 days.

For more information, see [Time-bound event sessions](../../relational-databases/extended-events/sql-server-extended-events-sessions.md#time-bound-event-sessions).

## Remarks

For more information about event session arguments, see [Extended Events sessions](../../relational-databases/extended-events/sql-server-extended-events-sessions.md).

The order of precedence for the logical operators is `NOT` (highest), followed by `AND`, followed by `OR`.

## Permissions

SQL Server and Azure SQL Managed Instance require the `CREATE ANY EVENT SESSION` (introduced in SQL Server 2022), or `ALTER ANY EVENT SESSION` permission.

Azure SQL Database and [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)] require the `CREATE ANY DATABASE EVENT SESSION` permission in the database.

> [!TIP]  
> SQL Server 2022 introduced more granular permissions for Extended Events. For more information, see [Blog: New granular permissions for SQL Server 2022 and Azure SQL to improve adherence with PoLP](https://techcommunity.microsoft.com/blog/sqlserver/new-granular-permissions-for-sql-server-2022-and-azure-sql-to-improve-adherence-/3607507).

## Examples

### A. SQL Server and Azure SQL Managed Instance example

The following example shows how to create an event session named `test_session`. This example adds two events and uses the `event_file` target, limiting the size of each file to 256 MB and limiting the retained number of files to 10.

```sql
IF EXISTS (SELECT 1
           FROM sys.server_event_sessions
           WHERE name = 'test_session')
    DROP EVENT SESSION test_session ON SERVER;

CREATE EVENT SESSION test_session ON SERVER
ADD EVENT sqlserver.rpc_starting,
ADD EVENT sqlserver.sql_batch_starting,
ADD EVENT sqlserver.error_reported
ADD TARGET package0.event_file
    (
    SET filename = N'C:\xe\test_session.xel',
        max_file_size = 256,
        max_rollover_files = 10
    )
WITH (MAX_MEMORY = 4 MB);
```

### B. Azure SQL Database examples

For example walkthroughs, review [Create an event session with an event_file target in Azure Storage](/azure/azure-sql/database/xevent-code-event-file) and [Create an event session with a ring_buffer target in memory](/azure/azure-sql/database/xevent-code-ring-buffer).

### Code examples can differ for Azure SQL Database and SQL Managed Instance

[!INCLUDE [sql-on-premises-vs-azure-similar-sys-views-include](../../includes/paragraph-content/sql-on-premises-vs-azure-similar-sys-views-include.md)]

## Related content

- [Create an event session with an event_file target in Azure Storage](/azure/azure-sql/database/xevent-code-event-file)
- [Extended Events targets](../../relational-databases/extended-events/targets-for-extended-events-in-sql-server.md)
- [sys.server_event_sessions (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-event-sessions-transact-sql.md)
- [sys.dm_xe_objects (Transact-SQL)](../../relational-databases/system-dynamic-management-objects/sys-dm-xe-objects-transact-sql.md)
- [sys.dm_xe_object_columns (Transact-SQL)](../../relational-databases/system-dynamic-management-objects/sys-dm-xe-object-columns-transact-sql.md)
- [ALTER EVENT SESSION (Transact-SQL)](alter-event-session-transact-sql.md)
- [DROP EVENT SESSION (Transact-SQL)](drop-event-session-transact-sql.md)
