---
title: "Extended Events engine"
description: The Extended Events engine enables the definition of events and processing of event data, manages Extended Events, and maintains a list of sessions.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 10/22/2023
ms.service: sql
ms.subservice: xevents
ms.topic: conceptual
helpviewer_keywords:
  - "extended events [SQL Server], engine"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Extended Events engine

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The Extended Events engine is a collection of services and objects that:

- Enable the definition of events
- Enable processing event data
- Manage Extended Events services and objects in the system
- Maintain a list of Extended Events sessions and manage access to that list

The Extended Events engine itself doesn't provide any events or actions to take when an event fires. The processes that use the Extended Events engine define interaction with the engine. These processes add event points and supply the actions to take in response to event firing.

The following illustration shows a simplified view of an Extended Events session. For more information, see [Extended Events sessions](sql-server-extended-events-sessions.md).

:::image type="content" source="media/detailed-extended-event-architecture.png" alt-text="Diagram showing the detailed Extended Events architecture.":::

- Each Windows process can have one or more modules (`Win32 process`, `Win32 module`), also known as *binaries* or *executable modules*.
- Each of the Windows process modules can contain one or more Extended Events packages (`Package`), which contain one or more Extended Events objects (`Type`, `Target`, `Action`, `Map`, `Predicate`, and `Event`).
- Inside a host process, there can only be one instance of the Extended Events engine (`Extended event engine`), which:
  - Manages some aspects of the session (for example, enumerating sessions).
  - Handles dispatching (`Dispatcher`), similar to a thread pool.
  - Handles memory buffers (`Buffer`) for events. When buffers are filled, the buffers are dispatched to targets.
- After a session is created and events are optionally bound to the session (`Session context`):
  - Instances of targets (`Target instance`) can also be created and added to the session.
  - When buffers are filled, those buffers are dispatched to targets.

## Related content

- [Extended Events overview](extended-events.md)
