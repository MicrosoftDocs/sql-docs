---
title: Save Trace Results to a File
titleSuffix: SQL Server Profiler
description: Find out how to save captured event data to a trace file, specify a maximum trace file size, and enable the file rollover option in SQL Server Profiler.
ms.service: sql
ms.reviewer: ""
ms.subservice: profiler
ms.topic: conceptual
ms.assetid: ac528747-0c19-4f3d-96f5-44c762a4abed
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 03/14/2017
---

# Save Trace Results to a File (SQL Server Profiler)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This topic describes how to save trace results to a file by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].  
  
### To save trace results to a file  
  
1.  On the **File** menu, click **New Trace**, and then connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
     The **Trace Properties**dialog box appears.  
  
    > [!NOTE]  
    >  If **Start tracing immediately after making connection**is selected, the **Trace Properties**dialog box fails to appear and the trace begins instead. To turn off this setting, on the **Tools**menu, click **Options**, and clear the **Start tracing immediately after making connection** check box.  
  
2.  In the **Trace name** box, type a name for the trace.  
  
3.  Select the **Save to file** check box.  
  
     The **Save As**dialog box appears.  
  
4.  Specify a path and filename in the **Save As**dialog box. Click **Save.**  
  
    > [!NOTE]  
    >  Ensure that the SQL Server service has sufficient permissions to write to a file in the directory specified.  
  
5.  In the **Trace Properties** dialog box, enter the maximum file size in the **Set maximum file size (MB)** text box. The default value is 5 megabytes (MB).  
  
6.  Optionally, specify the following options:  
  
    -   Select the **Enable file rollover** check box to have [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] create new files for trace data once the maximum file size is reached. This option is selected by default.  
  
    -   Select the **Server processes trace data** check box to ensure that the server records each trace event.  
  
        > [!NOTE]  
        >  When **Server processes trace data**is cleared, the server does not record events if recording events significantly degrades performance.  
  
## See Also  
 [SQL Server Profiler](../../tools/sql-server-profiler/sql-server-profiler.md)  
  
  
