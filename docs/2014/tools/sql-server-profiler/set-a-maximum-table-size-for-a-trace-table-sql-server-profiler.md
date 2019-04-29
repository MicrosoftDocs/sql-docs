---
title: "Set a Maximum Table Size for a Trace Table (SQL Server Profiler) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: profiler
ms.topic: conceptual
helpviewer_keywords: 
  - "size [SQL Server], trace tables"
  - "maximum table size for traces"
ms.assetid: d0ae83e5-1c88-4a2e-be05-2c341280b978
author: stevestein
ms.author: sstein
manager: craigg
---
# Set a Maximum Table Size for a Trace Table (SQL Server Profiler)
  This topic describes how to set a maximum table size for trace tables by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].  
  
### To set a maximum table size for a trace table  
  
1.  On the **File** menu, click **New Trace**, and then connect to an instance of SQL Server.  
  
     The **Trace Properties**dialog box appears.  
  
    > [!NOTE]  
    >  If **Start tracing immediately after making connection**is selected, the **Trace Properties**dialog box fails to appear and the trace begins instead. To turn off this setting, on the **Tools**menu, click **Options**, and clear the **Start tracing immediately after making connection** check box.  
  
2.  In the **Trace name** box, type a name for the trace.  
  
3.  In the **Template name**list, select a trace template.  
  
4.  Select the **Save to table**check box.  
  
5.  Connect to the server on which you want the trace to be stored.  
  
     The **Destination Table** dialog box appears.  
  
6.  Select a database for the trace from the **Database** list.  
  
7.  In the **Table** box, type or select a table name.  
  
8.  Select the **Set maximum rows** check box, and specify a maximum number of rows for the trace table.  
  
    > [!NOTE]  
    >  When the number of rows in the table exceeds the maximum that you have specified, the trace events are no longer recorded. However, tracing continues.  
  
## See Also  
 [SQL Server Profiler](sql-server-profiler.md)   
 [SQL Server Profiler](sql-server-profiler.md)  
  
  
