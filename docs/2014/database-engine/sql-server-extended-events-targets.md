---
title: "SQL Server Extended Events Targets | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "targets [SQL Server extended events]"
  - "extended events [SQL Server], targets"
ms.assetid: e281684c-40d1-4cf9-a0d4-7ea1ecffa384
author: mashamsft
ms.author: mathoma
manager: craigg
---
# SQL Server Extended Events Targets
  [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Extended Events targets are event consumers. Targets can write to a file, store event data in a memory buffer, or aggregate event data. Targets can process data synchronously or asynchronously.  
  
 The Extended Events design ensures that targets are guaranteed to receive events once and only once per session.  
  
 Extended Events provide the following targets that you can use for an Extended Events session:  
  
-   [Event counter](../../2014/database-engine/event-counter-target.md)  
  
     Counts all specified events that occur during an Extended Events session. Use to obtain information about workload characteristics without adding the overhead of full event collection. This is a synchronous target.  
  
-   [Event file](../../2014/database-engine/event-file-target.md)  
  
     Use to write event session output from complete memory buffers to disk. This is an asynchronous target.  
  
-   [Event pairing](../../2014/database-engine/event-pairing-target.md)  
  
     Many kinds of events occur in pairs, such as lock acquires and lock releases. Use to determine when a specified paired event does not occur in a matched set. This is an asynchronous target.  
  
-   [Event Tracing for Windows (ETW)](../relational-databases/extended-events/event-tracing-for-windows-target.md)  
  
     Use to correlate [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] events with Windows operating system or application event data. This is a synchronous target.  
  
-   [Histogram](../../2014/database-engine/histogram-target.md)  
  
     Use to count the number of times that a specified event occurs, based on a specified event column or action. This is an asynchronous target.  
  
-   [Ring buffer](../../2014/database-engine/ring-buffer-target.md)  
  
     Use to hold the event data in memory on a first-in first-out (FIFO) basis, or on a per-event FIFO basis. This is an asynchronous target.  
  
## See Also  
 [Extended Events](../relational-databases/extended-events/extended-events.md)   
 [SQL Server Extended Events Packages](../relational-databases/extended-events/sql-server-extended-events-packages.md)   
 [SQL Server Extended Events Sessions](../relational-databases/extended-events/sql-server-extended-events-sessions.md)   
 [SQL Server Extended Events Engine](../relational-databases/extended-events/sql-server-extended-events-engine.md)  
  
  
