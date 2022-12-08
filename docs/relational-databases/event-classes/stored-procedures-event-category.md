---
description: "Stored Procedures Event Category"
title: "Stored Procedures Event Category | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: reference
helpviewer_keywords: 
  - "Stored Procedures event category [SQL Server]"
  - "SQL Server event classes, Stored Procedures event category"
  - "event classes [SQL Server], Stored Procedures event category"
ms.assetid: 71bebaa3-a05a-4695-b349-078cecd0949a
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Stored Procedures Event Category
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  The **Stored Procedures** event category contains general stored procedure events.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[RPC:Completed Event Class](../../relational-databases/event-classes/rpc-completed-event-class.md)|Indicates that a remote procedure call (RPC) has been completed.|  
|[PreConnect:Completed Event Class](../../relational-databases/event-classes/preconnect-completed-event-class.md)|Indicates when the Resource Governor classifier function finishes execution.|  
|[PreConnect:Starting Event Class](../../relational-databases/event-classes/preconnect-starting-event-class.md)|Indicates when the Resource Governor classifier function starts execution.|  
|[RPC Output Parameter Event Class](../../relational-databases/event-classes/rpc-output-parameter-event-class.md)|Traces the output parameter values of remote procedure calls after execution.|  
|[RPC:Starting Event Class](../../relational-databases/event-classes/rpc-starting-event-class.md)|Indicates that a remote procedure call is starting.|  
|[SP:CacheHit Event Class](../../relational-databases/event-classes/sp-cachehit-event-class.md)|Indicates that the stored procedure is in the cache.|  
|[SP:CacheInsert Event Class](../../relational-databases/event-classes/sp-cacheinsert-event-class.md)|Indicates that the stored procedure has been brought into the cache.|  
|[SP:CacheMiss Event Class](../../relational-databases/event-classes/sp-cachemiss-event-class.md)|Indicates that the stored procedure was not found in the cache.|  
|[SP:CacheRemove Event Class](../../relational-databases/event-classes/sp-cacheremove-event-class.md)|Indicates that the stored procedure has been removed from the cache.|  
|[SP:Completed Event Class](../../relational-databases/event-classes/sp-completed-event-class.md)|Indicates that execution of the stored procedure has completed.|  
|[SP:Recompile Event Class](../../relational-databases/event-classes/sp-recompile-event-class.md)|Indicates that the stored procedure has been recompiled.|  
|[SP:Starting Event Class](../../relational-databases/event-classes/sp-starting-event-class.md)|Indicates that execution of the stored procedure is starting.|  
|[SP:StmtCompleted Event Class](../../relational-databases/event-classes/sp-stmtcompleted-event-class.md)|Indicates that a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement within a stored procedure has completed.|  
|[SP:StmtStarting Event Class](../../relational-databases/event-classes/sp-stmtstarting-event-class.md)|Indicates that a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement within a stored procedure has started.|  
  
## See Also  
 [Extended Events](../../relational-databases/extended-events/extended-events.md)   
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)  
  
  
