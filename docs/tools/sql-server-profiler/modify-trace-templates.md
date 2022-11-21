---
title: Modify Trace Templates
titleSuffix: SQL Server Profiler
description: Discover how to modify trace templates in SQL Server Profiler. Add or remove event classes and data columns, and change filters.
ms.service: sql
ms.reviewer: ""
ms.subservice: profiler
ms.topic: conceptual
ms.assetid: 75b62a54-8d16-4599-bd2d-c42cfcc209f4
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 07/12/2017
---

# Modify trace templates
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  You can modify templates that are saved in a file on the local computer on which [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] is running. You can also modify templates derived from those files. When you modify existing templates, you edit template properties such as event classes and data columns, in the same order that the properties were set originally, on the **Events Selection** tab of the **Trace Properties** dialog box. Event classes and data columns can be added or removed, and filters can be changed. After the template is modified, a user-specific template is created and the original system template is left intact. For more information, see [Save Traces and Trace Templates](../../tools/sql-server-profiler/save-traces-and-trace-templates.md).  
  
 You may need to derive a template from an existing trace file if you cannot remember (or have not saved) the original template that was used to create the trace, or if you want to run the same trace at a later date. When working with existing traces, you can view the properties, but you cannot modify the properties. To modify the properties, stop or pause the trace. For more information, see [Derive a Template from a Trace File or Trace Table &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/derive-a-template-from-a-trace-file-or-trace-table-sql-server-profiler.md) and [Derive a Template from a Running Trace &#40;SQL Server Profiler&#41;](../../tools/sql-server-profiler/derive-a-template-from-a-running-trace-sql-server-profiler.md).  
  
## To modify a trace template  
  
1.  On the **File** menu, point to **Templates**, and then click **Edit Template**.  
  
2.  In the **Trace Template Properties** dialog box, on the **General** tab, you can modify the server type and template name, or choose to use a default template for the server type.  
  
3.  On the **Events Selection** tab, use the grid control to add or remove events and data columns from the trace file as follows.  
  
    -   To add an event, expand the appropriate event category in the **Events** column, and then select the event name.  
  
    -   When you add an event, all relevant data columns are included by default. To remove a data column for an event from a trace, clear the check box in the data column for the event.  
  
    -   To add filters, click the data column name and specify the filter criteria in the **Edit Filter** dialog box. You can also right-click the data column name, and click **Edit Column Filter** to launch the **Edit Filter** dialog box. Click **OK** to add the filter.  
  
4.  Click **Save**, or click **Save As** to save the trace template under another name.  
  
## Next steps  
[Start a trace](../../tools/sql-server-profiler/start-a-trace.md)  
[Create a trace](../../tools/sql-server-profiler/create-a-trace-sql-server-profiler.md)  
[Modify an existing trace using Transact-SQL](../../relational-databases/sql-trace/modify-an-existing-trace-transact-sql.md)  
[Specify events and data columns for a trace using SQL Server Profiler](../../tools/sql-server-profiler/specify-events-and-data-columns-for-a-trace-file-sql-server-profiler.md)  
[sp-trace-setevent-transact-sql](../../relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql.md)  
  
