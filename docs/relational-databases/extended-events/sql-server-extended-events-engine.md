---
title: "SQL Server Extended Events Engine | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: xevents
ms.topic: conceptual
helpviewer_keywords: 
  - "extended events [SQL Server], engine"
ms.assetid: d74642a5-42b9-4a15-aa3d-f98bfe695050
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQL Server Extended Events Engine

[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Extended Events engine is a collection of services and objects that:  
  
-   Enable the definition of events.  
  
-   Enable processing event data.  
  
-   Manage Extended Events services and objects in the system.  
  
-   Maintain a list of Extended Events sessions and manage access to that list.  
  
 The Extended Events engine itself does not provide any events or actions to take when an event fires. The processes that use the Extended Events engine define interaction with the engine. These processes add event points and supply the actions to take in response to event firing.  
  
 The following illustration shows a simplified view of an Extended Events session. For more information, see [SQL Server Extended Events Sessions](../../relational-databases/extended-events/sql-server-extended-events-sessions.md).  
  
 ![Detailed extended events architecture](../../relational-databases/extended-events/media/xearchitecturedetailed.gif "Detailed extended events architecture")  
  
 Note the following:  
  
-   Each Windows process can have one or more modules (**Win32 process**, **Win32 module**). These are also known as *binaries* or *executable modules*.  
  
-   Each of the Windows process modules can contain one or more Extended Events packages (**Package**), which contain one or more Extended Events objects (**Type**, **Target**, **Action**, **Map**, **Predicate**, and **Event**).  
  
-   Inside a host process there can only be one instance of the Extended Events engine (**Extended event engine**), which:  
  
    -   Manages some aspects of the session (for example, enumerating sessions).  
  
    -   Handles dispatching (**Dispatcher**). This is similar to a thread pool.  
  
    -   Handles memory buffers (**Buffer**) for events. When buffers are filled, the buffers are dispatched to targets.  
  
-   After a session is created and events are optionally bound to the session (**Session context**):  
  
    -   Instances of targets (**Target instance**) may be also be created and added to the session.  
  
    -   When buffers are filled, those buffers are dispatched to targets.  
  
## See Also  
 [Extended Events](../../relational-databases/extended-events/extended-events.md)  
  
  
