---
description: "Errors and Warnings Event Category (Database Engine)"
title: "Errors and Warnings Event Category"
ms.date: 06/03/2020
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: reference
helpviewer_keywords: 
  - "Errors and Warnings event category [SQL Server]"
  - "SQL Server event classes, Errors and Warnings event category"
  - "event classes [SQL Server], Errors and Warnings event category"
ms.assetid: 249c19b5-af68-4433-80f6-337395176641
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
ms.custom: "seo-lt-2019"
---
# Errors and Warnings Event Category (Database Engine)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  The **Errors and Warnings** event category contains general error and warning events.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Attention Event Class](../../relational-databases/event-classes/attention-event-class.md)|Indicates that an **Attention** event has occurred.|  
|[Background Job Error Event Class](../../relational-databases/event-classes/background-job-error-event-class.md)|Indicates that a background job has terminated abnormally.|  
|[Bitmap Warning Event Class](../../relational-databases/event-classes/bitmap-warning-event-class.md)|Indicates that bitmap filtering has been disabled in a query.|  
|[Blocked Process Report Event Class](../../relational-databases/event-classes/blocked-process-report-event-class.md)|Indicates that a task has been blocked for more than a specified amount of time.|  
|[CPU Threshold Exceeded Event Class](../../relational-databases/event-classes/cpu-threshold-exceeded-event-class.md)|Indicates that the Resource Governor detects a query that exceeds the specified CPU threshold.|  
|[ErrorLog Event Class](../../relational-databases/event-classes/errorlog-event-class.md)|Indicates that error events have been logged in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log.|  
|[EventLog Event Class](../../relational-databases/event-classes/eventlog-event-class.md)|Indicates that events have been logged in the Windows event log.|  
|[Exception Event Class](../../relational-databases/event-classes/exception-event-class.md)|Indicates that an exception has occurred in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|[Exchange Spill Event Class](../../relational-databases/event-classes/exchange-spill-event-class.md)|Indicates that communication buffers in a parallel query plan have been written to the tempdb database.|  
|[Execution Warnings Event Class](../../relational-databases/event-classes/execution-warnings-event-class.md)|Indicates that memory grant warnings occurred during the execution of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] statement or stored procedure.|  
|[Hash Warning Event Class](../../relational-databases/event-classes/hash-warning-event-class.md)|Indicates that a hash recursion or hash bailout has occurred during a hashing operation.|  
|[Missing Column Statistics Event Class](../../relational-databases/event-classes/missing-column-statistics-event-class.md)|Indicates that column statistics that could have been useful for the optimizer are not available.|  
|[Missing Join Predicate Event Class](../../relational-databases/event-classes/missing-join-predicate-event-class.md)|Indicates that a query is being executed that has no join predicate.|  
|[Sort Warnings Event Class](../../relational-databases/event-classes/sort-warnings-event-class.md)|Indicates that sort operations do not fit into memory.|  
|[User Error Message Event Class](../../relational-databases/event-classes/user-error-message-event-class.md)|Displays error messages that are seen by the user.|  
  
## See Also  
 [sp_trace_setevent &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)  
  
  
