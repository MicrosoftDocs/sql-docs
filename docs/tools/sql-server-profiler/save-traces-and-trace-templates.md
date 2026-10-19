---
title: Save Traces and Trace Templates
titleSuffix: SQL Server Profiler
description: Discover how to save captured event data to a trace file or a database table in SQL Server Profiler and how to save user-defined trace templates.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: concept-article
ms.collection:
  - data-tools
---

# Save traces and trace templates

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

It's important to distinguish saving trace files from saving trace templates. Saving a trace file involves saving the captured event data to a specified place. Saving a trace template involves saving the definition of the trace, such as specified data columns, event classes, or filters.

## Save traces

Save captured event data to a file or a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] table when you need to analyze or replay the captured data at a later time. Use a trace file to do the following:

- Use a trace file or trace table to create a workload that is used as input for Database Engine Tuning Advisor.

- Use a trace file to capture events and send the trace file to the support provider for analysis.

- Use the query processing tools in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to access the data or to view the data in [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. Only members of the **sysadmin** fixed server role or the table creator can access the trace table directly.

Capturing trace data to a table is a slower operation than capturing trace data to a file. An alternative is to capture trace data to a file, open the trace file, and then save the trace as a trace table.

When you use a trace file, [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] saves captured event data (not trace definitions) to a [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] Trace (\*.trc) file. The extension is added to the end of the file automatically when the trace file is saved, regardless of any other specified extension. For example, if you specify a trace file called `Trace.dat`, the file created is called `Trace.dat.trc`.

> [!IMPORTANT]  
> Users who have the SHOWPLAN, the ALTER TRACE, or the VIEW SERVER STATE permission can view queries that are captured in Showplan output. These queries might contain sensitive information such as passwords. Therefore, we recommend that you only grant these permissions to users who are authorized to view sensitive information, such as members of the **db_owner** fixed database role, or members of the **sysadmin** fixed server role. Additionally, we recommend that you only save Showplan files or trace files that contain Showplan-related events to a location that uses the NTFS file system, and that you restrict access to users who are authorized to view sensitive information.

## Save templates

The template definition of a trace includes the event classes, data columns, filters, and all other properties (except the captured event data) that are used to create a trace. [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] provides predefined system templates for common tracing tasks and for specific tasks, such as creating a workload that Database Engine Tuning Advisor can use to tune the physical database design. You can also create and save user-defined templates.

## Import and export templates

[!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] allows you to import and export templates from one server to another. Exporting a template moves a copy of an existing template to a directory that you specify. Importing a template makes a copy of a template that you specify. When these templates are viewed in [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)], you can distinguish them from system templates by the term"(user)" that follows the template name. You can't overwrite or directly modify a predefined system template.

## Analyze performance with templates

If you frequently monitor [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], use templates to analyze performance. The templates capture the same event data each time and use the same trace definition to monitor the same events. You don't need to define the event classes and data columns every time you create a trace. Also, a template can be given to another user to monitor specific [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] events. For example, a support provider can supply a customer with a template. The customer uses the template to capture the required event data, which is then sent to the support provider for analysis.

## Save a trace to a file

- [Save trace results to a file (SQL Server Profiler)](save-trace-results-to-a-file-sql-server-profiler.md)
- [sp_trace_create](../../relational-databases/system-stored-procedures/sp-trace-create-transact-sql.md)

## Related content

- [Save trace results to a table (SQL Server Profiler)](save-trace-results-to-a-table-sql-server-profiler.md)
- [Create a trace template (SQL Server Profiler)](create-a-trace-template-sql-server-profiler.md)
- [Derive a template from a running trace (SQL Server Profiler)](derive-a-template-from-a-running-trace-sql-server-profiler.md)
- [Derive a template from a trace file or trace table (SQL Server Profiler)](derive-a-template-from-a-trace-file-or-trace-table-sql-server-profiler.md)
- [Export a trace template (SQL Server Profiler)](export-a-trace-template-sql-server-profiler.md)
- [Import a trace template (SQL Server Profiler)](import-a-trace-template-sql-server-profiler.md)
