---
title: View Filter Information
titleSuffix: SQL Server Profiler
description: Find out how to view the filters that SQL Server Profiler is currently applying to data columns to limit the events that are traced.
author: markingmyname
ms.author: maghan
ms.date: 03/01/2017
ms.service: sql
ms.subservice: profiler
ms.topic: conceptual
---

# View Filter Information (SQL Server Profiler)

 [!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This topic describes how to view filters on data columns for event classes by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].  
  
### To view filter information  
  
1.  Open a trace file, trace table, or SQL script, and on the **File** menu, click **Properties**. If you are editing a trace template or creating a new trace, skip this step.  
  
2.  On the **Events Selection** tab, right-click the data column name for the filter you want to view, and click **Edit Column Filter**.  
  
3.  In the **Edit Filter** dialog box, expand the filter comparison operators to see the assigned value for the specified criterion. Repeat Steps 2 and 3 for all columns for which you want to view filter information.  
  
> [!NOTE]  
>  Comparison operators with assigned values are formatted bold.  
  
## See Also  
 [SQL Server Profiler](../../tools/sql-server-profiler/sql-server-profiler.md)  
  
  
