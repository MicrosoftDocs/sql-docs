---
title: Import a Trace Template
titleSuffix: SQL Server Profiler
description: Learn how to import a user-defined template in SQL Server Profiler to make it available for traces along with the predefined system templates.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# Import a trace template (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to import a trace template file (.tdf) by using [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)].

## Import a trace template

1. On the **File** menu, select **Templates**, and then select **Import Template**.

1. In the **Open File** dialog box, select the trace template file (.tdf file) to import and select **Open**.

   The trace template file is added to the available templates in the **Use the template** list in the **Trace Properties** dialog box. User defined templates have **(user)** appended to the template name.

## Related content

- [Create a trace template (SQL Server Profiler)](create-a-trace-template-sql-server-profiler.md)
- [Modify trace templates](modify-trace-templates.md)
- [Derive a template from a running trace (SQL Server Profiler)](derive-a-template-from-a-running-trace-sql-server-profiler.md)
- [Derive a template from a trace file or trace table (SQL Server Profiler)](derive-a-template-from-a-trace-file-or-trace-table-sql-server-profiler.md)
- [Export a trace template (SQL Server Profiler)](export-a-trace-template-sql-server-profiler.md)
- [SQL Server Profiler templates and permissions](sql-server-profiler-templates-and-permissions.md)
- [SQL Server Profiler](sql-server-profiler.md)
