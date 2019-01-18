---
title: "Specify Events and Data Columns for a Trace File (SQL Server Profiler) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: profiler
ms.topic: conceptual
helpviewer_keywords: 
  - "adding events"
  - "traces [SQL Server], data columns"
  - "deleting events"
  - "removing events"
  - "traces [SQL Server], events"
ms.assetid: 7da715a3-2f03-4063-b6a4-20fd7b44e675
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Specify Events and Data Columns for a Trace File (SQL Server Profiler)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to specify event classes and data columns for traces by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].  
  
### To specify events and data columns for a trace  
  
1.  On the **Trace Properties** or **Trace Template Properties** dialog box, click the **Events Selection** tab.  
  
     The **Events Selection** tab contains a grid control. The grid control is a table that contains each of the traceable event classes. The table contains one row for each event class. The event classes can differ slightly depending on the type and version of server to which you are connected. The event classes are identified in the **Events**column of the grid and are grouped by event category. The remaining columns list the data columns that can be returned for each event class.  
  
2.  On the **Events Selection**tab, use the grid control to add or remove events and data columns from the trace file.  
  
3.  To remove events from the trace, clear the check box in the **Events** column for each event class.  
  
4.  To include events in a trace, check the box in the **Events** column for each event class, or check a data column that corresponds to an event.  
  
> [!IMPORTANT]  
>  If the trace is going be correlated with System Monitor or Performance Monitor data, both **Start Time** and **End Time** data columns must be included in the trace.  
  
 When you include an event class, every associated data column is also included to the trace, if you have checked the box corresponding to an event. If you checked the box for a particular column, only that column is included in the trace.  
  
1.  To remove data columns from an event class, clear the check boxes from the data column in the event class row, or right-click on the column header and select the **Deselect column** option.  
  
2.  Optionally, apply filters to the trace. For more information, see [Filter Events in a Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/filter-events-in-a-trace-sql-server-profiler.md)  
  
## See Also  
 [SQL Server Profiler](../../tools/sql-server-profiler/sql-server-profiler.md)  
  
  
