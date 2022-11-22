---
title: "Save deadlock graphs (SQL Server Profiler) | Microsoft Docs"
description: Learn how to save a deadlock graph by using SQL Server Profiler. Deadlock graphs are saved as XML files.
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "deadlocks [SQL Server], saving deadlock graphs"
  - "graphs [SQL Server]"
  - "saving deadlock graphs"
ms.assetid: bf1fc906-abd6-4a89-842e-da0d66b2defe
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Save deadlock graphs (SQL Server Profiler)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to save a deadlock graph by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. Deadlock graphs are saved as XML files.  
  
## Save deadlock graph events separately  
  
1. On the **File** menu, select **New Trace**, and then connect to an instance of SQL Server.  
  
     The **Trace Properties** dialog box appears.  
  
    > [!NOTE]  
    >  If you select **Start tracing immediately after making connection**, the **Trace Properties** dialog box fails to appear, and the trace begins instead. To turn off this setting, on the **Tools** menu, select **Options**, and clear the **Start tracing immediately after making connection** check box.  
  
2. In the **Trace Properties** dialog box, type a name for the trace in the **Trace name** box.  
  
3. In the **Use the template** list, select a trace template on which to base the trace. If you don't want to use a template, select **Blank**.  
  
4. Do one of the following:  
  
    -   To capture the trace to a file, select the **Save to file** check box. Specify a value for **Set maximum file size**.  
  
         Optionally, select the **Enable file rollover** and **Server processes trace data** check boxes. 
  
    -   To capture the trace to a database table, select the **Save to table** check box.  
  
         Optionally, select **Set maximum rows**, and specify a value.  
  
5. Optionally, select the **Enable trace stop time** check box, and specify a stop date and time. 
  
6. Select the **Events Selection** tab.  
  
7. In the **Events** data column, expand the **Locks** event category, and then select the **Deadlock graph** check box. If the **Locks** event category isn't available, select the **Show all events** check box to display it.  
  
     The **Events Extraction Settings** tab is added to the **Trace Properties** dialog box.  
  
8. On the **Events Extraction Settings** tab, select **Save Deadlock XML Events Separately**.  
  
9. In the **Save As** dialog box, enter the name of the file where you want to store the deadlock graph events.  
  
10. Select **All Deadlock XML batches in a single file** to save all deadlock graph events in a single XML file. Or select **Each Deadlock XML batch in a distinct file** to create a new XML file for each deadlock graph.  
  
 After you save the deadlock file, you can open the file in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For more information, see [Open, view, and print a deadlock file &#40;SQL Server Management Studio&#41;](../../relational-databases/performance/open-view-and-print-a-deadlock-file-sql-server-management-studio.md).  
  
## See also  
 [Analyze deadlocks with SQL Server Profiler](../../tools/sql-server-profiler/analyze-deadlocks-with-sql-server-profiler.md)  
  
  
