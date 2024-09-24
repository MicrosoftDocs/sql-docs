---
title: "Extended Events packages"
description: A package is a container for Extended Events objects. This article describes the objects a package can contain.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 09/12/2024
ms.service: sql
ms.subservice: xevents
ms.topic: conceptual
helpviewer_keywords:
  - "extended events [SQL Server], packages"
  - "xe"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Extended Events packages

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

A package is a container for Extended Events objects in the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)]. For example, the following packages exist in any [!INCLUDE [ssde-md](../../includes/ssde-md.md)] that supports Extended Events:

| Package | Description |
| --- | --- |
| `package0` (default) | Extended Events system objects. |
| `sqlserver` | Objects related to the [!INCLUDE [ssde-md](../../includes/ssde-md.md)]. |
| `sqlos` | SQL Operating System (SQLOS) related objects. |

> [!NOTE]  
> The `SecAudit` package is used internally by the Audit feature. None of the objects in this package are available through the Extended Events data definition language (DDL).

Packages are identified by a name, a GUID, and the binary module that contains the package. A module can be an executable or a dynamic link library. For more information, see [sys.dm_xe_packages](../system-dynamic-management-views/sys-dm-xe-packages-transact-sql.md).

A package can contain any or all of the following objects, which are discussed in greater detail later in this article:

- Events
- Targets
- Actions
- Types
- Predicates
- Maps

Objects from different packages can be mixed in an event session. For more information, see [Extended Events sessions](sql-server-extended-events-sessions.md).

## Package contents

The following illustration shows the objects that can exist in a package.

:::image type="content" source="media/sql-server-extended-events-packages/extended-events-packages-objects.png" alt-text="Diagram that shows the relationship of a module, packages, and objects.":::

### Events

Events are monitoring points of interest in the execution path of a program, such as [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. When an event fires, it contains the fact that the point of interest was reached, and state information from the time the event was fired.

Events can be used solely for tracing purposes or for triggering actions. These actions can either be synchronous or asynchronous.

> [!NOTE]  
> An event doesn't have any knowledge of the actions that can be triggered in response to the event firing.

A set of events in a package can't change after the package is registered with Extended Events.

All events have a versioned schema that defines their contents. This schema is composed of event columns with defined types. An event of a specific type must always provide its data in exactly the same order that is specified in the schema. However, an event target doesn't have to consume all the data that is provided.

#### Event categorization

Extended Events uses an event categorization model similar to Event Tracing for Windows (ETW). Two event properties are used for categorization, channel and keyword. Using these properties supports the integration of Extended Events with ETW and its tools.

A *channel* identifies the audience for an event. Channels are described in the following table.

| Term | Definition |
| --- | --- |
| **Admin** | Admin events are primarily targeted to end users, administrators, and support. The events that are found in the Admin channel can indicate a problem with a well-defined solution that an administrator can act on. An example of an admin event is when an application fails to connect. These events are either documented or have a message associated with them that tells the reader what to do to rectify the problem. |
| **Operational** | Operational events are used for analyzing and diagnosing a problem or occurrence. They can be used to trigger tools or tasks based on the problem or occurrence. |
| **Analytic** | Analytic events are published in high volume. They describe program operation and are typically used in performance investigations. |
| **Debug** | Debug events are used primarily by developers to diagnose a problem for debugging.<br /><br />Events in the Debug channel return internal implementation-specific state data. The schemas and data that the events return can change, become invalid, or be removed in future versions of the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] without notice. |

A *keyword* is application-specific and enables a finer-grained grouping of related events, which makes it easier for you to specify and retrieve an event that you want to use in a session. You can use the following query to obtain keyword information.

```sql
SELECT map_value AS Keyword
FROM sys.dm_xe_map_values
WHERE name = 'keyword_map';
```

### Targets

Targets are event consumers. Targets process events, either synchronously on the thread that fires the event or asynchronously on a system provided thread. Extended Events provides several target types that you can use as appropriate for directing event output. For more information, see [Targets for Extended Events](targets-for-extended-events-in-sql-server.md).

You use the `ADD TARGET` clause to add targets to an event session.

### Actions

An action is a programmatic response or series of responses to an event. Actions are bound to an event, and each event can have its own set of actions.

> [!NOTE]  
> Actions that are intended for specific events can't bind to other events.

An action bound to an event is invoked synchronously on the thread that fired the event. There are many types of actions and they have a wide range of capabilities. Actions can:

- Capture a process dump
- Store state information in a local context using variable storage
- Aggregate event data
- Append data to event data

Common examples of using actions include:

- Collect SQL text of a query being executed by the thread firing the event
- Collect query plan handle, query hash, and query plan hash
- Collect attributes of a session that causes the event to be fired, including client host name, principal name, connection ID, etc.
- Collect the call stack
- Capture a process dump when a specific error occurs

You use the `ACTION` clause to add actions to an event session.

### Predicates

Predicates are a set of logical rules that are used to evaluate events when they're processed. Predicates enable the Extended Events user to selectively capture event data based on specific criteria.

Predicates can store data in a local context, which can be used to create predicates that return true once every *n* minutes or every *n* times that an event fires. You can also use this local context storage to dynamically update the predicate, which suppresses future event firing if the events contain similar data.

Predicates have the ability to retrieve context information, such as the thread ID, and event specific data. Predicates are evaluated as full Boolean expressions, and support short circuiting at the first point where the entire expression is found to be false.

> [!NOTE]  
> Predicates with side effects might not be evaluated if an earlier predicate check fails.

You use the `WHERE` clause to add predicates to an event session.

### Types

In a package, each Extended Events object has a type. The following types are used:

- `action`
- `event`
- `message`
- `pred_compare`
- `pred_source`
- `target`
- `type`

For more information, see [sys.dm_xe_objects](../system-dynamic-management-views/sys-dm-xe-objects-transact-sql.md).

### Maps

A map table maps an internal value to a string, which enables a user to know what the value represents. Instead of only being able to obtain a numeric value, a user can get a meaningful description of the internal value. The following query shows how to obtain map values.

```sql
SELECT map_key,
       map_value
FROM sys.dm_xe_map_values
WHERE name = 'lock_mode';
```

The preceding query produces the following output:

| map_key | map_value |
| --- | --- |
| 0 | `NL` |
| 1 | `SCH_S` |
| 2 | `SCH_M` |
| 3 | `S` |
| 4 | `U` |
| 5 | `X` |
| 6 | `IS` |
| 7 | `IU` |
| 8 | `IX` |
| 9 | `SIU` |
| 10 | `SIX` |
| 11 | `UIX` |
| 12 | `BU` |
| 13 | `RS_S` |
| 14 | `RS_U` |
| 15 | `RI_NL` |
| 16 | `RI_S` |
| 17 | `RI_U` |
| 18 | `RI_X` |
| 19 | `RX_S` |
| 20 | `RX_U` |
| 21 | `LAST_MODE` |

Using this table as an example, assume that you have a column named `lock_mode`, and its value is `5`. The table indicates that `5` maps to `X`, which means the lock type is *Exclusive*.

## Related content

- [Extended Events sessions](sql-server-extended-events-sessions.md)
- [Extended Events engine](sql-server-extended-events-engine.md)
- [Targets for Extended Events](targets-for-extended-events-in-sql-server.md)
