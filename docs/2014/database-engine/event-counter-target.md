---
title: "Event Counter Target | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "synchronous event counter target [SQL Server extended events]"
  - "targets [SQL Server extended events], synchronous event counter target"
ms.assetid: 342e08d1-dcca-4a71-ae64-cb61b55b85bc
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Event Counter Target
  The event counter target counts all events that occur during an Extended Events session. By using the event counter target, you can obtain information about workload characteristics without adding the overhead of full event collection. This target has no customizable parameters.  
  
## Adding the Target to a Session  
 To add the event counter target to an Extended Events session, you must include the following statement when you create or alter an event session:  
  
```  
ADD TARGET package0.event_counter  
```  
  
## Reviewing the Target Output  
 To review the output from the event counter target, you can use the following query, replacing *session_name* with the name of the event session:  
  
```  
SELECT name, target_name, CAST(xet.target_data AS xml)  
FROM sys.dm_xe_session_targets AS xet  
JOIN sys.dm_xe_sessions AS xe  
   ON (xe.address = xet.event_session_address)  
WHERE xe.name = 'session_name'  
```  
  
 The following example shows the event counter target output format.  
  
```  
<CounterTarget truncated = "0">  
  <Packages>  
    <Package name = "[package name]">  
      <Event name = "[event name]" count = "[number]" />  
    </Package>  
  </Packages>  
</CounterTarget>  
```  
  
## See Also  
 [SQL Server Extended Events Targets](../../2014/database-engine/sql-server-extended-events-targets.md)   
 [sys.dm_xe_session_targets &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-xe-session-targets-transact-sql)   
 [CREATE EVENT SESSION &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-event-session-transact-sql)   
 [ALTER EVENT SESSION &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-event-session-transact-sql)  
  
  
