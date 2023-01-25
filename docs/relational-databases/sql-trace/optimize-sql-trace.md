---
description: "Optimize SQL Trace"
title: "Optimize SQL Trace | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: conceptual
helpviewer_keywords: 
  - "time [SQL Server], traces"
  - "SQL Trace, performance"
  - "traces [SQL Server], performance"
  - "performance [SQL Server], trace"
ms.assetid: 50944218-925f-4576-aec8-4379846d7681
author: "MashaMSFT"
ms.author: "mathoma"
---
# Optimize SQL Trace
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Although running SQL Trace incurs a performance cost because it uses system resources to gather data, you can do many things to minimize it. To minimize the performance cost incurred by a trace, try the following:  
  
-   Consider using the command prompt to run traces. Using a graphical user interface hinders performance. For more information, see [sp_trace_create &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-trace-create-transact-sql.md).  
  
-   Avoid including events that occur frequently. If possible, narrow your trace by means of specific event classes and filters. If fewer trace events are gathered, fewer system resources are required to support tracing.  
  
-   Focus the trace to collect only events that provide relevant data. For example, if your trace is to identify deadlocks, include the **Lock:Deadlock** event class, but not the **Lock:Acquired** event class. If you include both event classes, the trace has to respond to every lock that is acquired, and your execution cost is doubled.  
  
-   Avoid collecting duplicate data. For example, if you collect **SQL:BatchStarted** and the **SQL:BatchCompleted**, you can minimize the size of the results set by collecting text data for the **SQL:BatchStarted** event class only.  
  
-   Use filters in the trace definition. For example, if you know that a certain user is reporting slow performance during ad hoc queries, create a filter on **LoginName**. Set the filter to include only events where the **LoginName** matches that user name.  
  
 If you need to run a trace for events that create a significant impact on performance, consider limiting the performance impact on the server by using one of the following methods:  
  
-   Run traces for shorter periods of time. You can control the length of time that a trace runs by enabling a stop time. This is especially important if you cannot limit the event classes or filter an event. Enabling a stop time ensures that the performance incurred does not last indefinitely.  
  
-   Limit the size of the trace results. You can limit the size of the trace results to a maximum file size. This strategy ensures that the performance cost stops when the file-size limit is reached (if file rollover is not enabled).  
  
-   Limit the number of events returned. With [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] you can limit the number of events returned by saving the trace to a table and setting the maximum number of rows. Trace results are still returned to the [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] screen after the maximum number of rows has been reached, but the cost of recording the results to a table is eliminated.  
  
## See Also  
 [Filter a Trace](../../relational-databases/sql-trace/filter-a-trace.md)  
  
  
