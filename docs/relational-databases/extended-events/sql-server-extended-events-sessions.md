---
title: "Extended Events sessions"
description: An Extended Events session is created in the Database Engine process that hosts the Extended Events engine.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 10/22/2023
ms.service: sql
ms.subservice: xevents
ms.topic: conceptual
helpviewer_keywords:
  - "xe"
  - "sessions"
  - "extend events [SQL Server]"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Extended Events sessions

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

An Extended Events session is created in the [!INCLUDE [ssdenoversion-md](../../includes/ssdenoversion-md.md)] process hosting the Extended Events engine. The following aspects of an Extended Events session provide a context for understanding the Extended Events infrastructure and the processing that takes place:

- **Session states.** The different states that an Extended Events session is in when `CREATE EVENT SESSION` and `ALTER EVENT SESSION` statements are executed.

- **Session content and characteristics.** The content of an Extended Events session, such as targets and events, and how these objects are related in a session or among sessions.

## Session states

The following illustration shows the various states of an Extended Events session.

:::image type="content" source="media/xesessionstate.png" alt-text="Diagram showing Extended Events session state.":::

Referring to the preceding figure, observe that session state changes as the different data definition language (DDL) commands are issued for an event session. The following table describes these changes in state.

| Illustration label | DDL statement | Description |
| --- | --- | --- |
| **Create** | `CREATE EVENT SESSION` | The host process creates a session object that contains the metadata provided by `CREATE EVENT SESSION`. The host process validates the session definition, validates the user permission level, and stores the metadata in the `master` database. At this point, the session isn't active. |
| **Alter** | `ALTER EVENT SESSION`, `STATE=START` | The host process starts the session. The host process reads the stored metadata, validates the session definition, verifies the level of user permission level, and creates the session. Session objects, such as events and targets, are loaded and event handling is active. |
| **Alter** | `ALTER EVENT SESSION`, `STATE=STOP` | The host process stops the active session but retains the metadata. |
| **Drop** | `DROP EVENT SESSION` | Depending on whether or not the session is active, Drop (`DROP SESSION`) deletes the metadata and closes the active session, or deletes the session metadata. |

## Session content and characteristics

Extended Events sessions have implied boundaries in that the configuration of one session doesn't change the configuration of another session. However, these boundaries don't prevent an event or a target type from being used in more than one session.

The following illustration shows session content and the relationship between packages and sessions.

:::image type="content" source="media/xesessions.gif" alt-text="Diagram showing object coexistence and sharing in sessions.":::

Referring to the preceding illustration, keep in mind that:

- The mapping between package objects and sessions is many to many, which means that an object of a particular type can appear in several sessions, and a session can contain several objects.
- The same event (Event 1) or target type (Target 1) can be used in more than one session.

Sessions have the following characteristics:

- Actions and predicates are bound to events on a per-session basis. If you have Event 1 in Session A with Action 1 and Predicate Z, this doesn't in any way affect having Event 1 in Session B with Action 2 and Action 3 with no predicate.
- Policies are attached to sessions to handle buffering and dispatch, and causality tracking.

*Buffering* refers to how event data is stored while an event session is running. Buffering policies specify how much memory to use for event data, and the loss policy for the events. *Dispatch* refers to the duration of time events stay in buffers before being served to targets for processing.

*Causality tracking* tracks work across multiple tasks. When causality tracking is enabled, each event fired has a unique activity ID across the system. The activity ID is a combination of a GUID value that remains constant across all events for a task, and a sequence number that is incremented each time an event is fired. When one task causes work to be done on another, the activity ID of the parent is sent to the child task. The child task outputs the parent's activity ID the first time it fires an event.

## Related content

- [Extended Events overview](extended-events.md)
