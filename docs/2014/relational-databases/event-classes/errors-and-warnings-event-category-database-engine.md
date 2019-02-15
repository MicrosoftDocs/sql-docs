---
title: "Errors and Warnings Event Category (Database Engine) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "Errors and Warnings event category [SQL Server]"
  - "SQL Server event classes, Errors and Warnings event category"
  - "event classes [SQL Server], Errors and Warnings event category"
ms.assetid: 249c19b5-af68-4433-80f6-337395176641
author: stevestein
ms.author: sstein
manager: craigg
---
# Errors and Warnings Event Category (Database Engine)
  The **Errors and Warnings** event category contains general error and warning events.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Attention Event Class](attention-event-class.md)|Indicates that an **Attention** event has occurred.|  
|[Background Job Error Event Class](background-job-error-event-class.md)|Indicates that a background job has terminated abnormally.|  
|[Bitmap Warning Event Class](bitmap-warning-event-class.md)|Indicates that bitmap filtering has been disabled in a query.|  
|[Blocked Process Report Event Class](blocked-process-report-event-class.md)|Indicates that a task has been blocked for more than a specified amount of time.|  
|[CPU Threshold Exceeded Event Class](cpu-threshold-exceeded-event-class.md)|Indicates that the Resource Governor detects a query that exceeds the specified CPU threshold.|  
|[ErrorLog Event Class](errorlog-event-class.md)|Indicates that error events have been logged in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log.|  
|[EventLog Event Class](eventlog-event-class.md)|Indicates that events have been logged in the Windows event log.|  
|[Exception Event Class](exception-event-class.md)|Indicates that an exception has occurred in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|[Exchange Spill Event Class](exchange-spill-event-class.md)|Indicates that communication buffers in a parallel query plan have been written to the tempdb database.|  
|[Execution Warnings Event Class](execution-warnings-event-class.md)|Indicates that memory grant warnings occurred during the execution of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] statement or stored procedure.|  
|[Hash Warning Event Class](hash-warning-event-class.md)|Indicates that a hash recursion or hash bailout has occurred during a hashing operation.|  
|[Missing Column Statistics Event Class](missing-column-statistics-event-class.md)|Indicates that column statistics that could have been useful for the optimizer are not available.|  
|[Missing Join Predicate Event Class](missing-join-predicate-event-class.md)|Indicates that a query is being executed that has no join predicate.|  
|[Sort Warnings Event Class](sort-warnings-event-class.md)|Indicates that sort operations do not fit into memory.|  
|[User Error Message Event Class](user-error-message-event-class.md)|Displays error messages that are seen by the user.|  
  
## See Also  
 [sp_trace_setevent &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql)  
  
  
