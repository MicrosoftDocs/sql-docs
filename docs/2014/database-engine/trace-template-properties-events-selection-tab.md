---
title: "Trace Template Properties (Events Selection Tab) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "sql12.pro.tracetemplateproperties.eventsselection.f1"
helpviewer_keywords: 
  - "Trace Template Properties dialog box"
ms.assetid: 5b202457-ab42-4902-8012-7f3f40aa09f5
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Trace Template Properties (Events Selection Tab)
  Use the **Events Selection** tab of the **Trace Template Properties** dialog box to view, edit, or specify event classes and data columns to include in a [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)] trace template.  
  
## Options  
 **Events** column  
 Specify events that should be traced by selecting or clearing the check box in the event column. Events are organized by event category.  
  
 If you selected **Base new template on existing one** on the **General** tab, events are automatically selected according to the specified template. For more information about event classes, see [SQL Server Event Class Reference](../relational-databases/event-classes/sql-server-event-class-reference.md).  
  
 Data columns  
 Specify data columns that should be traced by checking the box that corresponds with the event and the data column you need. All relevant event columns are checked by default for each event included in the trace, if the checkbox corresponding to the event is checked. If you checked **Base new template on existing one** on the **General** tab, data columns and filters are automatically selected according to the specified template.  
  
 Specify filters by clicking the data column heading and entering the filter criteria. Filtered data columns are indicated by a filter icon to the left of the column label in the **Edit Filter** dialog box.  
  
 **Show all events**  
 Show all available events. This option is checked by default if you are creating a new template that is not based on an existing template. Uncheck to hide all unselected events in the **Events Selection** grid.  
  
 **Show all columns**  
 Show all available data columns. This option is checked by default if you are creating a new template that is not based on an existing template. Uncheck to hide all unselected data columns in the **Events Selection** grid.  
  
 **Column Filters**  
 Launches the **Edit Filter** dialog box, which displays a filter icon to the left of the data column label. Use the **Edit Filter** dialog box to edit data column filters.  
  
 **Organize Columns**  
 Changes the order of columns in the trace and groups results by one or more columns.  
  
## See Also  
 [Specify Events and Data Columns for a Trace File &#40;SQL Server Profiler&#41;](../tools/sql-server-profiler/specify-events-and-data-columns-for-a-trace-file-sql-server-profiler.md)   
 [Organize Columns Displayed in a Trace &#40;SQL Server Profiler&#41;](../tools/sql-server-profiler/organize-columns-displayed-in-a-trace-sql-server-profiler.md)   
 [Filter Events in a Trace &#40;SQL Server Profiler&#41;](../tools/sql-server-profiler/filter-events-in-a-trace-sql-server-profiler.md)   
 [View Filter Information &#40;SQL Server Profiler&#41;](../tools/sql-server-profiler/view-filter-information-sql-server-profiler.md)   
 [Modify a Filter &#40;SQL Server Profiler&#41;](../tools/sql-server-profiler/modify-a-filter-sql-server-profiler.md)   
 [SQL Server Profiler Templates and Permissions](../tools/sql-server-profiler/sql-server-profiler-templates-and-permissions.md)   
 [SQL Server Profiler](../tools/sql-server-profiler/sql-server-profiler.md)  
  
  
