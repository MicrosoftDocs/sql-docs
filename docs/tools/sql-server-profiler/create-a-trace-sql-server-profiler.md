---
title: "Create a Trace (SQL Server Profiler) | Microsoft Docs"
ms.custom: ""
ms.date: "08/01/2016"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: profiler
ms.topic: conceptual
helpviewer_keywords: 
  - "traces [SQL Server], creating"
ms.assetid: 0302fa6d-d2b5-43fe-ad70-7a337575b112
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Create a Trace (SQL Server Profiler)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to use [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to create a trace.  
  
### To create a trace  
  
1.  On the **File** menu, click **New Trace**, and connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
     The **Trace Properties** dialog box appears.  
  
    > **NOTE:** The **Trace Properties** dialog box fails to appear, and the trace begins instead, if **Start tracing immediately after making connection** is selected. To turn off this setting, on the **Tools* *menu, click **Options**, and clear the Start tracing immediately after making connection check box.  
  
2.  In the **Trace name** box, type a name for the trace.  
  
3.  In the **Use the template** list, select a trace template on which to base the trace, or select **Blank** if you do not want to use a template.  
  
4.  To save the trace results, do one of the following:  
  
    -   Click **Save to file** to capture the trace to a file. Specify a value for **Set maximum file size**. The default value is 5 megabytes (MB).  
  
         Optionally, select **Enable file rollover** to automatically create new files when the maximum file size is reached. You can also optionally select **Server processes trace data**, which causes the service that is running the trace to process trace data instead of the client application. When the server processes trace data, no events are skipped even under stress conditions, but server performance may be affected.  
  
    -   Click **Save to table** to capture the trace to a database table.  
  
         Optionally, click **Set maximum rows**, and specify a value.  
  
    > **CAUTION!!** When you do not save the trace results to a file or table, you can view the trace while [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] is open. However, you lose the trace results after you stop the trace and close [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. To avoid losing the trace results in this way, click **Save** on the **File** menu to save the results before you close [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].  
  
5.  Optionally, select the **Enable trace stop time** check box, and specify a stop date and time.  
  
6.  To add or remove events, data columns or filters, click the **Events Selection**  tab. For more information, see: [Specify Events and Data Columns for a Trace File &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/specify-events-and-data-columns-for-a-trace-file-sql-server-profiler.md)  
  
7.  Click **Run** to start the trace.  
  
## See also  
 [Permissions Required to Run SQL Server Profiler](../../tools/sql-server-profiler/permissions-required-to-run-sql-server-profiler.md)   
 [SQL Server Profiler Templates and Permissions](../../tools/sql-server-profiler/sql-server-profiler-templates-and-permissions.md)   
 [SQL Server Profiler](../../tools/sql-server-profiler/sql-server-profiler.md)   
 [Correlate a Trace with Windows Performance Log Data &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/correlate-a-trace-with-windows-performance-log-data-sql-server-profiler.md)  
  
  
