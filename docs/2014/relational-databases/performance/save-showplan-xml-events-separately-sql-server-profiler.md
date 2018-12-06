---
title: "Save Showplan XML Events Separately (SQL Server Profiler) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Showplan XML events"
  - "saving Showplan XML events"
  - "events [SQL Server], Showplan XML"
ms.assetid: 33320a7a-36e8-401c-876d-5b82c49abd85
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Save Showplan XML Events Separately (SQL Server Profiler)
  This topic describes how to save **Showplan XML** events that are captured in traces into separate .SQLPlan files by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. You can open the **Showplan XML** event files in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], which enables you to view the graphical execution plan for each event.  
  
### To save Showplan XML events separately  
  
1.  On the **File** menu, click **New Trace**, and then connect to an instance of SQL Server.  
  
     The **Trace Properties**dialog box appears.  
  
    > [!NOTE]  
    >  If **Start tracing immediately after making connection**is selected, the **Trace Properties**dialog box fails to appear and the trace begins instead. To turn off this setting, on the **Tools**menu, click **Options**, and clear the **Start tracing immediately after making connection** check box.  
  
2.  In the **Trace Properties** dialog box, type a name for the trace in the **Trace name** box.  
  
3.  In the **Use the template** list, select a trace template on which to base the trace, or select **Blank** if you do not want to use a template.  
  
4.  Do one of the following:  
  
    -   Select the**Save to file** check box to capture the trace to a file. Specify a value for **Set maximum file size**. Optionally, select the **Enable file rollover** and **Server processes trace data** check boxes.  
  
    -   Select the**Save to table** check box to capture the trace to a database table. Optionally click **Set maximum rows**, and specify a value.  
  
5.  Optionally, select the **Enable trace stop time** check box, and specify a stop date and time.  
  
6.  Click the **Events Selection**tab.  
  
7.  In the **Events**data column, expand the **Performance**event category, and then select the **Showplan XML**check box. If the **Performance** event category is not available, check **Show all events** to display it.  
  
     The **Events Extraction Settings**tab is added to the **Trace Properties**dialog box.  
  
8.  On the **Events Extraction Settings**tab, click **Save XML Showplan events separately**.  
  
9. In the **Save As** dialog box, enter the name of the file in which to store the **Showplan XML** events.  
  
10. Click **All XML Showplan batches in a single file** to save all **Showplan XML** events in a single XML file, or click **Each XML Showplan batch in a distinct file**to create a new XML file for each **Showplan XML** event.  
  
11. To view the **Showplan XML** event file in SQL Server Management Studio, on the **File** menu, point to **Open**, and click **File**. Navigate to the directory where you saved the **Showplan XML** event file or files to select one and open it. **Showplan XML** event files have a .SQLPlan file extension.  
  
## See Also  
 [Analyze Queries with SHOWPLAN Results in SQL Server Profiler](../../tools/sql-server-profiler/analyze-queries-with-showplan-results-in-sql-server-profiler.md)  
  
  
