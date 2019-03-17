---
title: "SQL Server Profiler - Organize Columns | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "sql12.pro.organize.columns.f1"
ms.assetid: bf5674f4-da5e-43f9-aeb2-76ca37993790
author: mashamsft
ms.author: mathoma
manager: craigg
---
# SQL Server Profiler - Organize Columns
  Use the **Organize Columns** dialog box to select data columns for grouping or aggregating events that are displayed in a trace, which makes large trace files or tables easier to view and analyze.  
  
 Aggregating moves and collapses all events in the trace under its respective event class type. A plus sign (**+**) appears to the left of the event class name. Clicking the plus sign expands the event class so you can view all events of that type.  
  
 Grouping organizes all event classes of a specific type together in the trace window display. However, the events are not collapsed under the event class type.  
  
 When you group or aggregate events in a trace window display, the columns selected for grouping or aggregating remain fixed in the display window, but you can scroll to the right or left to view all other data columns.  
  
 To access this dialog box, open an existing trace file or table, and click **Properties** on the [!INCLUDE[ssSqlProfiler](../includes/sssqlprofiler-md.md)] **File** menu. In the **Trace Properties** dialog box, click the **Events Selection** tab, and then click **Organize Columns**. You can also click **Organize Columns** on the **Events Selection** tab when you are creating a new trace.  
  
## Options  
 **Groups**  
 Move data column names under **Groups** to group or aggregate event classes in the trace window.  
  
 To aggregate events, move one data column into **Groups**. This causes all events of a specific type to be collapsed under event class type name in the trace window display. A plus sign (**+**) appears to the left of the event class name. Click the plus sign to expand the event class type and view all events. You can set aggregation and grouping on and off by clicking **Aggregated View** or **Grouped View** on the **View** menu.  
  
 To group events, move more than one data column into **Groups**. This causes all events of a specific type to be grouped together in the trace window display, but does not collapse the events under each event class type name. You can switch back and forth between a grouped view and an ungrouped view by clicking **Grouped View** on the View menu. When more than one data column is moved into **Groups**, the option to switch to **Aggregated View** is not available.  
  
 **Columns**  
 List of data columns available to move into **Groups**. Click the plus sign (**+**) to the left of **Columns** to expand the list.  
  
 **Up**  
 After selecting a data column, click **Up** to move data columns up into **Groups**. You can also click **Up** to rearrange the display of columns in the trace window display.  
  
 **Down**  
 After selecting a data column, click **Down** to move data columns out of **Groups**. You can also click **Down** to rearrange the display of columns in the trace window display.  
  
## See Also  
 [Organize Columns Displayed in a Trace &#40;SQL Server Profiler&#41;](../tools/sql-server-profiler/organize-columns-displayed-in-a-trace-sql-server-profiler.md)   
 [Create a Trace &#40;SQL Server Profiler&#41;](../tools/sql-server-profiler/create-a-trace-sql-server-profiler.md)   
 [Create a Trace Template &#40;SQL Server Profiler&#41;](../tools/sql-server-profiler/create-a-trace-template-sql-server-profiler.md)   
 [Open a Trace File &#40;SQL Server Profiler&#41;](../tools/sql-server-profiler/open-a-trace-file-sql-server-profiler.md)   
 [Open a Trace Table &#40;SQL Server Profiler&#41;](../tools/sql-server-profiler/open-a-trace-table-sql-server-profiler.md)  
  
  
