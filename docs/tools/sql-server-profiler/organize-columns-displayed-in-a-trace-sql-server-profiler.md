---
title: "Organize Columns Displayed in a Trace (SQL Server Profiler) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: profiler
ms.topic: conceptual
helpviewer_keywords: 
  - "organizing trace columns displayed [SQL Server]"
  - "arranging trace columns displayed"
  - "traces [SQL Server], data columns"
ms.assetid: 6b923f94-0eb1-467e-82f6-ceed43f77017
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Organize Columns Displayed in a Trace (SQL Server Profiler)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  You can group data columns in a trace by selecting **Organize Columns** in the trace table or **Trace File Properties** dialog box, or when you define a trace. Grouping the data columns enables you to better analyze [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] trace output. For more information, see [View and Analyze Traces with SQL Server Profiler](../../tools/sql-server-profiler/view-and-analyze-traces-with-sql-server-profiler.md).  
  
 **Organize Columns** enables you to either group the trace events, or to group and aggregate them by the data columns you select.  
  
-   Choose multiple data columns for grouping to only group trace events. When you choose multiple data columns for grouping, the trace window displays events grouped by the values in the data columns you selected for grouping. The following example shows how the trace window grid would appear if you chose the **Duration** and **StartTime** data columns for grouping. Note that the **Duration** column values are displayed in ascending order, then the **StartTime** values.  
  
|Duration|StartTime|EventClass|ClientProcessID|  
|--------------|---------------|----------------|---------------------|  
||12/12/2006 3:16:43 PM|SQL:StmtStarting|2124|  
|0|12/12/2006 5:39:23 PM|Audit Login|648|  
|1|12/12/2006 5:24:44 PM|SQL:StmtStarting|2124|  
|25|12/12/2006 5:24:44 PM|SQL:StmtCompleted|648|  
  
-   Choose only one column for grouping to group and aggregate trace events. When you choose only one data column for grouping, the trace window displays events grouped by the values in that data column and collapses all events under it. A plus sign (**+**) appears to the left of the event in the data column you chose for grouping, and the number of events collapsed under it appears in parentheses to the right of the event. The following example shows how the trace window grid would appear if you chose only the **EventClass** data column for grouping. Note that all events are organized under the **EventClass** data column. To view all events, click the plus sign to expand and display all event classes of that type.  
  
|EventClass|StartTime|Duration|ClientProcessID|  
|----------------|---------------|--------------|---------------------|  
|+ ExistingConnection (6)||||  
|+ SQL:BatchStarting (25)||||  
|+ SQL:StmtCompleted (11)||||  
|+ SQL:SmtStarting (21)||||  
  
### To group data columns displayed in a trace  
  
1.  Open an existing trace file or table.  
  
2.  On the **File** menu, click **Properties**.  
  
3.  In the **Trace File Properties** or **Trace Table Properties** dialog box, click the **Events Selection** tab.  
  
4.  On the **Events Selection** tab, click **Organize Columns**.  
  
5.  In the **Organize Columns** dialog box, select the columns you want to display in a group, and click **Up** to move them under **Groups**. After you have moved all the columns you want to move under **Groups**, you can use the **Up** and **Down** buttons to rearrange their order.  
  
     Moving the data column names into the **Groups** list means that the displayed trace is first organized by the values in the top-most data column appearing in the **Groups** list, then by the second data column in the **Groups** list, and so on.  
  
6.  Click **OK** in the **Organize Columns** dialog box, and then click **OK** in the **Trace Table Properties** or **Trace File Properties** dialog box.  
  
     After clicking **OK** in the **Trace Table Properties** or the **Trace File Properties** dialog box, the data columns are reorganized in the displayed trace. The data column that you moved to the top-most position in the **Groups** list is located first in the trace display when you are reading the grid from left to right. The rows in the trace are organized in ascending order by the values contained in the data columns that you included in the **Groups** list. The columns chosen for grouping remain fixed in the display, but you can scroll right or left to view the other columns.  
  
7.  To ungroup the displayed trace data, click **Grouped View** on the **View** menu to cancel the selection. If you want to revert to the grouped view, click **Grouped View** on the **View** menu again to reselect it.  
  
### To group and aggregate data columns in a trace  
  
1.  Open an existing trace file or table.  
  
2.  On the **File** menu, click **Properties**.  
  
3.  In the **Trace File Properties** or **Trace Table Properties** dialog box, click the **Events Selection** tab.  
  
4.  On the **Events Selection** tab, click **Organize Columns**.  
  
5.  In the **Organize Columns** dialog box, select one column by which you want to group and aggregate the displayed trace events. Click **Up** to move the column name under **Groups**. You can use the **Up** and **Down** buttons to rearrange the remaining columns under **Columns** if needed.  
  
6.  Click **OK** in the **Organize Columns** dialog box, and then click **OK** in the **Trace Table Properties** or **Trace File Properties** dialog box.  
  
     After clicking **OK** in the **Trace Table Properties** or the **Trace File Properties** dialog box, the data columns are reorganized in the displayed trace. All other data column events are aggregated under the data column that you moved into the **Groups** list. Click the plus sign (**+**) to the left of the event in the data column you chose for aggregation to expand it and view all events of that type. The column chosen for aggregation remains fixed in the display, but you can scroll right or left to view the other columns.  
  
7.  To revert to a normal view of the trace data, click **Aggregated View** on the **View** menu, which cancels the selection. If you want to revert to the aggregated view, click **Aggregated View** on the **View** menu again to reselect it. Note that you can also click **Grouped View** on the **View** menu to display the grouped trace events without collapsing them.  
  
## See Also  
 [Create a Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/create-a-trace-sql-server-profiler.md)   
 [Open a Trace Table &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/open-a-trace-table-sql-server-profiler.md)   
 [Open a Trace File &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/open-a-trace-file-sql-server-profiler.md)  
  
  
