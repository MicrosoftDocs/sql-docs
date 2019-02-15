---
title: "Save Deadlock Graphs (SQL Server Profiler) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "deadlocks [SQL Server], saving deadlock graphs"
  - "graphs [SQL Server]"
  - "saving deadlock graphs"
ms.assetid: bf1fc906-abd6-4a89-842e-da0d66b2defe
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Save Deadlock Graphs (SQL Server Profiler)
  This topic describes how to save a deadlock graph by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. Deadlock graphs are saved as XML files.  
  
### To save deadlock graph events separately  
  
1.  On the **File** menu, click **New Trace**, and then connect to an instance of SQL Server.  
  
     The **Trace Properties**dialog box appears.  
  
    > [!NOTE]  
    >  If **Start tracing immediately after making connection** is selected, the **Trace Properties**dialog box fails to appear, and the trace begins instead. To turn off this setting, on the **Tools**menu, click **Options**, and clear the **Start tracing immediately after making connection** check box.  
  
2.  In the Trace Properties dialog box, type a name for the trace in the**Trace name** box.  
  
3.  In the **Use the template** list, select a trace template on which to base the trace, or select **Blank** if you do not want to use a template.  
  
4.  Do one of the following:  
  
    -   Select the**Save to file** check box to capture the trace to a file. Specify a value for **Set maximum file size**.  
  
         Optionally, select **Enable file rollover** and **Server processes trace data**.  
  
    -   Select the **Save to table** check box to capture the trace to a database table.  
  
         Optionally, click **Set maximum rows**, and specify a value.  
  
5.  Optionally, select the **Enable trace stop time** check box, and specify a stop date and time.  
  
6.  Click the **Events Selection**tab.  
  
7.  In the **Events**data column, expand the **Locks**event category, and then select the **Deadlock graph**check box. If the Locks event category is not available, check **Show all events** to display it.  
  
     The **Events Extraction Settings**tab is added to the **Trace Properties**dialog box.  
  
8.  On the **Events Extraction Settings**tab, click **Save Deadlock XML Events Separately**.  
  
9. In the **Save As** dialog box, enter the name of the file in which to store the deadlock graph events.  
  
10. Click **All Deadlock XML batches in a single file** to save all deadlock graph events in a single XML file, or click **Each Deadlock XML batch in a distinct file**to create a new XML file for each deadlock graph.  
  
 After you have saved the deadlock file, you can open the file in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For more information, see [Open, View, and Print a Deadlock File &#40;SQL Server Management Studio&#41;](open-view-and-print-a-deadlock-file-sql-server-management-studio.md).  
  
## See Also  
 [Analyze Deadlocks with SQL Server Profiler](../../tools/sql-server-profiler/analyze-deadlocks-with-sql-server-profiler.md)  
  
  
