---
title: "Save Showplan XML Statistics Profile events separately (SQL Server Profiler) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "Showplan XML events"
  - "saving Showplan XML events"
  - "events [SQL Server], Showplan XML"
ms.assetid: df393f13-d538-4d94-8155-9c2fdf5f755d
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# Save Showplan XML Statistics Profile events separately (SQL Server Profiler)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to save **Showplan XML Statistics Profile** events that are captured in traces into separate .SQLPlan files by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. You can open the **Showplan XML Statistics Profile** event files in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] so that you can view the graphical execution plan for each event.  
  
## Save Showplan XML Statistics Profile events separately  
  
1. On the **File** menu, select **New Trace**, and then connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
     The **Trace Properties** dialog box appears.  
  
    > [!NOTE]  
    >  If you select **Start tracing immediately after making connection**, the **Trace Properties** dialog box doesn't appear, and the trace begins instead. To turn off this setting, on the **Tools** menu, select **Options**, and clear the **Start tracing immediately after making connection** check box.  
  
2. In the **Trace Properties** dialog box, type a name for the trace in the **Trace name** box.  
  
3. In the **Use the template** list, select a trace template on which to base the trace. If you don't want to use a template, select **Blank**.  
  
4. Do one of the following:  
  
    -   To capture the trace to a file, select the **Save to file** check box. Specify a value for **Set maximum file size**.  
  
         Optionally, select the **Enable file rollover** and **Server processes trace data** check boxes. 
  
    -   To capture the trace to a database table, select the **Save to table** check box.  
  
         Optionally, select **Set maximum rows**, and specify a value.  
  
5. Optionally, select the **Enable trace stop time** check box, and specify a stop date and time. 
  
6. Select the **Events Selection** tab.  
  
7. In the **Events** data column, expand the **Performance** event category, and then select the **Showplan XML Statistics Profile** check box. If the **Performance** event category isn't available, check **Show all events** to display it.  
  
     The **Events Extraction Settings** tab is added to the **Trace Properties** dialog box.  
  
8. On the **Events Extraction Settings** tab, select **Save XML Showplan events separately**.  
  
9. In the **Save As** dialog box, enter the file name to store the **Showplan XML Statistics Profile** events.  
  
10. Select **All batches in a single file** to save all **Showplan XML Statistics Profile** events in a single XML file. Or select **Each XML Showplan batch in a distinct file** to create a new XML file for each **Showplan XML Statistics Profile** event.  
  
11. To view the **Showplan XML Statistics Profile** event file in SQL Server Management Studio, on the **File** menu, point to **Open**, and select **File**. Browse to the directory where you saved the **Showplan XML Statistics Profile** event file or files to select one and open it. **Showplan XML Statistics Profile** event files have a .SQLPlan file extension.  
  
## See also  
 [Analyze queries with Showplan results in SQL Server Profiler](../../tools/sql-server-profiler/analyze-queries-with-showplan-results-in-sql-server-profiler.md)  
  
  
