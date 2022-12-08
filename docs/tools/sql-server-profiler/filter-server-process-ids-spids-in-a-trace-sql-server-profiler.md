---
title: Filter Server Process IDs (SPIDs) in a Trace File
titleSuffix: SQL Server Profiler
description: Learn how to limit trace output in SQL Server Profiler by applying a filter on the Server Process ID (SPID).
ms.service: sql
ms.reviewer: ""
ms.subservice: profiler
ms.topic: conceptual
ms.assetid: f5945c39-be6b-4632-91cb-92066c80e188
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 03/01/2017
---

# Filter Server Process IDs (SPIDs) in a Trace (SQL Server Profiler)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This topic describes how to filter server process identifiers (SPIDs) in a trace by using [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].  
  
### To filter system IDs in a trace  
  
1.  On the **File** menu, click **New Trace**, and then connect to an instance of SQL Server.  
  
     The **Trace Properties** dialog box appears.  
  
    > [!NOTE]  
    >  if **Start tracing immediately after making connection** is selected, the **Trace Properties** dialog box fails to appear, and the trace begins instead. To turn off this setting, on the **Tools** menu, click **Options**, and clear the **Start tracing immediately after making connection** check box.  
  
2.  In the **Trace name** box, type a name for the trace.  
  
3.  In the **Use the template** name list, select a trace template.  
  
4.  Optionally, specify a destination file or table in which to save the trace results.  
  
5.  On the **Events Selection** tab, click the **SPID** column heading to launch the **Edit Filter** dialog box. You can also right-click the column heading and choose **Edit Column Filter**. If the **SPID** column does not appear, check the **Show all columns** box.  
  
6.  In the **Edit Filter** dialog box, expand the appropriate comparison operator, and enter a SPID as a value for the comparison.  
  
## See Also  
 [SQL Server Profiler](../../tools/sql-server-profiler/sql-server-profiler.md)  
  
  
