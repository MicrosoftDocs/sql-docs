---
title: "View Filter Information (SQL Server Profiler) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: profiler
ms.topic: conceptual
helpviewer_keywords: 
  - "displaying filter information"
  - "filters [SQL Server], viewing"
  - "filters [SQL Server], traces"
  - "traces [SQL Server], filters"
  - "viewing filter information"
ms.assetid: 8d002dea-376a-452c-b3ca-3e93656ed75f
author: stevestein
ms.author: sstein
manager: craigg
---
# View Filter Information (SQL Server Profiler)
  This topic describes how to view filters on data columns for event classes by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].  
  
### To view filter information  
  
1.  Open a trace file, trace table, or SQL script, and on the **File** menu, click **Properties**. If you are editing a trace template or creating a new trace, skip this step.  
  
2.  On the **Events Selection** tab, right-click the data column name for the filter you want to view, and click **Edit Column Filter**.  
  
3.  In the **Edit Filter** dialog box, expand the filter comparison operators to see the assigned value for the specified criterion. Repeat Steps 2 and 3 for all columns for which you want to view filter information.  
  
> [!NOTE]  
>  Comparison operators with assigned values are formatted bold.  
  
## See Also  
 [SQL Server Profiler](sql-server-profiler.md)  
  
  
