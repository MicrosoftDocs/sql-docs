---
title: Modify a Filter
titleSuffix: SQL Server Profiler
description: Learn how to modify a trace filter in SQL Server Profiler so you can gather the information you need. Read about how trace filters affect database performance.
ms.service: sql
ms.reviewer: ""
ms.subservice: profiler
ms.topic: conceptual
ms.assetid: 8b317813-4918-4485-b930-77b1951aa00c
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 03/01/2017
---

# Modify a Filter (SQL Server Profiler)

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

You add filters to trace templates, which contain the trace definitions, to limit the number of events that are gathered by a trace. Limiting the number of events gathered can reduce the performance effects of tracing. If you set filters for a trace template and find that the trace is not gathering the kind of information that you need, you can edit the filter.  
  
### To modify a filter  
  
1.  In [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], open the template for the trace filter that you want to modify. On the **File** menu, click **Templates**, and then choose **Edit Template**.  
  
2.  In the **General** tab of the **Trace Template Properties** dialog, select a template from the **Select template name** list.  
  
3.  Click the **Events Selection** tab.  
  
     The **Events Selection** tab contains a grid control. The grid control is a table that contains each of the traceable event classes. The table contains one row for each event class. The event classes may differ slightly, depending on the type and version of server to which you are connected. The event classes are identified in the **Events**column of the grid and are grouped by event category. The remaining columns list the data columns that can be returned for each event class.  
  
4.  Click **Column Filters**.  
  
5.  In the **Edit Filter** dialog box, click the value next to the comparison operator that you want to edit, and type the new value or delete a value. You can also add additional filters.  
  
6.  Click **OK** and save the template.  
  
## See Also  
 [SQL Server Profiler](../../tools/sql-server-profiler/sql-server-profiler.md)  
  
  
