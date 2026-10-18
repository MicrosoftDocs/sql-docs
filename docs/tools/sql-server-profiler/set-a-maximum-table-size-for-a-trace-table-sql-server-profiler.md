---
title: Set a Maximum Table Size for a Trace Table
titleSuffix: SQL Server Profiler
description: Find out how to limit the size of a trace table. Use SQL Server Profiler to specify the maximum number of rows the table can have.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# Set a maximum table size for a trace table (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to set a maximum table size for trace tables by using [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)].

## Set a maximum table size for a trace table

1. On the **File** menu, select **New Trace**, and then connect to an instance of SQL Server.

   The **Trace Properties** dialog box appears.

   If **Start tracing immediately after making connection** is selected, the **Trace Properties** dialog box fails to appear and the trace begins instead. To turn off this setting, on the **Tools** menu, select **Options**, and clear the **Start tracing immediately after making connection** check box.

1. In the **Trace name** box, type a name for the trace.

1. In the **Template name** list, select a trace template.

1. Select the **Save to table** check box.

1. Connect to the server on which you want the trace to be stored.

   The **Destination Table** dialog box appears.

1. Select a database for the trace from the **Database** list.

1. In the **Table** box, type or select a table name.

1. Select the **Set maximum rows** check box, and specify a maximum number of rows for the trace table.

   When the number of rows in the table exceeds the maximum that you have specified, the trace events are no longer recorded. However, tracing continues.

## Related content

- [SQL Server Profiler](sql-server-profiler.md)
