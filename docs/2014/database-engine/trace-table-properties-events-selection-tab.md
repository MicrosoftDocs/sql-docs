---
title: "Trace Table Properties (Events Selection Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "sql12.pro.tracetableproperties.eventsselection.f1"
helpviewer_keywords: 
  - "Trace Table Properties dialog box"
ms.assetid: fa21df6a-b6b5-4b15-9104-957385821594
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Trace Table Properties (Events Selection Tab)
  Use the **Events Selection** tab of the **Trace Table Properties** dialog box to view the events and data column properties of the trace or to remove events or columns from the trace.  
  
 To view this window, use [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)] to open a trace table. Then on the **File** menu, click **Properties**, and then click the **Events Selection** tab.  
  
## Options  
 **Events** column  
 View traced events which are organized by event category. Events can be selected by checking the box or by checking a data column for an event. If the event box is checked, all data columns available for that event are selected. If the data column for an event is checked, the event is checked and any other required column is also automatically checked. If you are viewing a trace file or table, clearing check boxes for events or data columns reduces the amount of visible data in the trace window for easier analysis. You can also change column filters to reduce the amount of visible data in the trace window. For more information about event classes, see [SQL Server Event Class Reference](../relational-databases/event-classes/sql-server-event-class-reference.md).  
  
 Other data columns  
 View traced data columns. All relevant data columns in the trace are checked by default for each event included in the trace.  
  
 Specify filters by clicking the data column heading and entering the filter criteria. Filtered data columns are indicated by a filter icon to the left of the column label in the **Edit Filter** dialog box.  
  
 **Show all events**  
 Show all available events. By default, only rows in the **Events Selection** grid that are selected display. Uncheck this box to hide all unselected events in the **Events Selection** grid. If **Show all events** is checked and you are viewing a trace file or table, all events that were recorded in the trace display in the trace window.  
  
 **Show all columns**  
 Show all available data columns. By default, only data columns that are selected display. Uncheck this box to hide all unselected data columns in the **Events Selection** grid.  
  
 **Column Filters**  
 Launches the **Edit Filter** dialog box, which displays a filter icon to the left of the column label. You can use this dialog box to edit data column filters.  
  
 **Organize Columns**  
 After selecting **Events** and data columns to trace, click **Organize Columns** to force the grid to reorder the column in the trace results window.  
  
## See Also  
 [Open a Trace Table &#40;SQL Server Profiler&#41;](../tools/sql-server-profiler/open-a-trace-table-sql-server-profiler.md)   
 [SQL Server Profiler Templates and Permissions](../tools/sql-server-profiler/sql-server-profiler-templates-and-permissions.md)   
 [SQL Server Profiler](../tools/sql-server-profiler/sql-server-profiler.md)  
  
  
