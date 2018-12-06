---
title: "Mirroring States (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "states [SQL Server], database mirroring"
  - "PENDING_FAILOVER state"
  - "mirroring states [SQL Server]"
  - "DISCONNECTED state"
  - "SYNCHRONIZING state"
  - "SYNCHRONIZED state"
  - "SUSPENDED state"
  - "database mirroring [SQL Server], states"
ms.assetid: 90062917-74f9-471b-b49e-bc153ae1a468
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Mirroring States (SQL Server)
  During a database mirroring session, the mirrored database is always in a specific state (the *mirroring state*). The state of the database reflects the communication status, data flow, and the difference in data between the partners. The database mirroring session adopts the same state as the principal database.  
  
 Throughout a database mirroring session, the server instances monitor each other. The partners use the mirroring state to monitor the database. With the exception of the PENDING_FAILOVER state, the principal and mirror database are always in the same state. If a witness is set for the session, each of the partners monitors the witness using its connection state (CONNECTED or DISCONNECTED).  
  
 The possible mirroring states of the database are as follows:  
  
|Mirroring state|Description|  
|---------------------|-----------------|  
|SYNCHRONIZING|The contents of the mirror database are lagging behind the contents of the principal database. The principal server is sending log records to the mirror server, which is applying the changes to the mirror database to roll it forward.<br /><br /> At the start of a database mirroring session, the database is in the SYNCHRONIZING state. The principal server is serving the database, and the mirror is trying to catch up.|  
|SYNCHRONIZED|When the mirror server becomes sufficiently caught up to the principal server, the mirroring state changes to SYNCHRONIZED. The database remains in this state as long as the principal server continues to send changes to the mirror server and the mirror server continues to apply changes to the mirror database.<br /><br /> If transaction safety is set to FULLautomatic failover and manual failover are both supported in the SYNCHRONIZED state, there is no data loss after a failover.<br /><br /> If transaction safety is off, some data loss is always possible, even in the SYNCHRONIZED state.|  
|SUSPENDED|The mirror copy of the database is not available. The principal database is running without sending any logs to the mirror server, a condition known as *running exposed*. This is the state after a failover.<br /><br /> A session can also become SUSPENDED as a result of redo errors or if the administrator pauses the session.<br /><br /> SUSPENDED is a persistent state that survives partner shutdowns and startups.|  
|PENDING_FAILOVER|This state is found only on the principal server after a failover has begun, but the server has not transitioned into the mirror role.<br /><br /> When the failover is initiated, the principal database goes into the PENDING_FAILOVER state, quickly terminates any user connections, and takes over the mirror role soon thereafter.|  
|DISCONNECTED|The partner has lost communication with the other partner.|  
  
## See Also  
 [Monitoring Database Mirroring &#40;SQL Server&#41;](database-mirroring-sql-server.md)  
  
  
