---
title: Filter Events Based on the Event Start Time
titleSuffix: SQL Server Profiler
description: Filter events by start time during a trace. Learn how to set up a filter on the event start time in SQL Server Profiler.
ms.service: sql
ms.reviewer: ""
ms.subservice: profiler
ms.topic: conceptual
ms.assetid: e965579e-d006-41a3-89ec-cfd5398c67d2
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 03/14/2017
---

# Filter Events Based on the Event Start Time (SQL Server Profiler)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This topic describes how to filter trace events based on the event start time by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].  
  
### To filter an event based on the event start time  
  
1.  On the **File** menu, click **New Trace**, and then connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
     The **Trace Properties**dialog box appears.  
  
    > [!NOTE]  
    >  If **Start tracing immediately after making connection**is selected, the **Trace Properties**dialog box fails to appear, and the trace begins instead. To turn off this setting, on the **Tools**menu, click **Options**, and clear the **Start tracing immediately after making connection** check box.  
  
2.  In the **Trace name** box, type a name for the trace.  
  
3.  In the **Use the template**name list, select a trace template.  
  
4.  Optionally specify a destination for the trace results.  
  
5.  On the **Events Selection**tab, click the **StartTime** column heading. You can also right-click the column heading, and then click **Edit Column Filter** to launch the **Edit Filter** dialog box.  
  
6.  Expand **Greater than** or **Less than**, and then enter a **datetime** value in the field that appears beneath the comparison operator.  
  
## See Also  
 [SQL Server Profiler](../../tools/sql-server-profiler/sql-server-profiler.md)  
  
  
