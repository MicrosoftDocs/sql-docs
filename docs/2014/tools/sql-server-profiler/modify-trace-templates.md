---
title: "Modify Trace Templates | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: profiler
ms.topic: conceptual
helpviewer_keywords: 
  - "templates [SQL Server], SQL Server Profiler"
  - "Profiler [SQL Server Profiler], templates"
  - "trace templates [SQL Server]"
  - "modifying trace templates"
  - "SQL Server Profiler, templates"
ms.assetid: 75b62a54-8d16-4599-bd2d-c42cfcc209f4
author: stevestein
ms.author: sstein
manager: craigg
---
# Modify Trace Templates
  You can modify templates that are saved in a file on the local computer on which [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] is running. You can also modify templates derived from those files. When you modify existing templates, you edit template properties such as event classes and data columns, in the same order that the properties were set originally, on the **Events Selection** tab of the **Trace Properties** dialog box. Event classes and data columns can be added or removed, and filters can be changed. After the template is modified, a user-specific template is created and the original system template is left intact. For more information, see [Save Traces and Trace Templates](save-traces-and-trace-templates.md).  
  
 You may need to derive a template from an existing trace file if you cannot remember (or have not saved) the original template that was used to create the trace, or if you want to run the same trace at a later date. When working with existing traces, you can view the properties, but you cannot modify the properties. To modify the properties, stop or pause the trace. For more information, see [Derive a Template from a Trace File or Trace Table &#40;SQL Server Profiler&#41;](sql-server-profiler.md) and [Derive a Template from a Running Trace &#40;SQL Server Profiler&#41;](derive-a-template-from-a-running-trace-sql-server-profiler.md).  
  
 **To create a trace template**  
  
 [Create a Trace Template &#40;SQL Server Profiler&#41;](create-a-trace-template-sql-server-profiler.md)  
  
 **To run a trace from a trace template**  
  
 [Create a Trace &#40;SQL Server Profiler&#41;](create-a-trace-sql-server-profiler.md)  
  
 **To modify a trace template**  
  
 [Using SQL Server Profiler](../../database-engine/modify-a-trace-template-sql-server-profiler.md)  
  
 [Using Transact-SQL](../../relational-databases/sql-trace/modify-an-existing-trace-transact-sql.md)  
  
 **To add or remove events from a trace template or trace file**  
  
 [Using SQL Server Profiler](specify-events-and-data-columns-for-a-trace-file-sql-server-profiler.md)  
  
 [Using Transact-SQL](/sql/relational-databases/system-stored-procedures/sp-trace-setevent-transact-sql)  
  
## See Also  
 [Start a Trace](start-a-trace.md)  
  
  
