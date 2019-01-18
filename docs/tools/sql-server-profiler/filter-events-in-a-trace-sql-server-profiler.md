---
title: "Filter Events in a Trace (SQL Server Profiler) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: profiler
ms.topic: conceptual
helpviewer_keywords: 
  - "filters [SQL Server], traces"
  - "traces [SQL Server], filters"
  - "traces [SQL Server], events"
ms.assetid: 0fd63573-3b35-4f67-9e1e-ed9aabee11a8
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Filter Events in a Trace (SQL Server Profiler)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Filters limit the events collected in a trace. If a filter is not set, all events of the selected event classes are returned in the trace output. It is not mandatory to set a filter for a trace. However, a filter minimizes the overhead that is incurred during tracing.  
  
 You add filters to trace definitions by using the **Events Selection** tab of the **Trace Properties** or **Trace Template Properties** dialog box.  
  
### To filter events in a trace  
  
1.  In the **Trace Properties** or **Trace Template Properties** dialog box, click the **Events Selection** tab.  
  
     The **Events Selection** tab contains a grid control. The grid control is a table that contains each of the traceable event classes. The table contains one row for each event class. The event classes may differ slightly, depending on the type and version of server to which you are connected. The event classes are identified in the **Events**column of the grid and are grouped by event category. The remaining columns list the data columns that can be returned for each event class.  
  
2.  Click **Column Filters.**  
  
     The **Edit Filter**dialog box appears. The **Edit Filter**dialog box contains a list of comparison operators that you can use to filter events in a trace.  
  
3.  To apply a filter, click the comparison operator, and type a value to use for the filter.  
  
4.  Click **OK**.  
  
 **Considerations:**  
  
-   If you set filter conditions on the **StartTime** and **EndTime** data columns of the Events Selection tab, then make sure that:  
  
    -   The date you enter matches this format: `YYYY/MM/DD HH:mm:sec`.  
  
         -OR-  
  
    -   **Use regional settings to display date and time values** is checked in the **General Options** dialog box. To view the **General Options** dialog box, on the [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] **Tools** menu, click **Option**.  
  
         -AND-  
  
    -   The date you enter is between January 1, 1753 and December 31, 9999.  
  
-   If tracing events from the **osql** utility or from the **sqlcmd** utility, always append **%** to filters on the **TextData** data column.  
  
## See Also  
 [SQL Server Profiler](../../tools/sql-server-profiler/sql-server-profiler.md)  
  
  
