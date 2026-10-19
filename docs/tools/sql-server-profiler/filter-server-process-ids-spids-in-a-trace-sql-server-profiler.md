---
title: Filter Session IDs in a Trace File
titleSuffix: SQL Server Profiler
description: Learn how to limit trace output in SQL Server Profiler by applying a filter on the Session ID (SPID).
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# Filter session IDs in a trace (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to filter session identifiers (SPIDs) in a trace by using [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)].

## Filter system IDs in a trace

1. On the **File** menu, select **New Trace**, and then connect to an instance of SQL Server.

   The **Trace Properties** dialog box appears.

   If **Start tracing immediately after making connection** is selected, the **Trace Properties** dialog box fails to appear, and the trace begins instead. To turn off this setting, on the **Tools** menu, select **Options**, and clear the **Start tracing immediately after making connection** check box.

1. In the **Trace name** box, type a name for the trace.

1. In the **Use the template** name list, select a trace template.

1. Optionally, specify a destination file or table in which to save the trace results.

1. On the **Events Selection** tab, select the **SPID** column heading to launch the **Edit Filter** dialog box. You can also right-click the column heading and choose **Edit Column Filter**. If the **SPID** column doesn't appear, check the **Show all columns** box.

1. In the **Edit Filter** dialog box, expand the appropriate comparison operator, and enter a session ID as a value for the comparison.

## Related content

- [SQL Server Profiler](sql-server-profiler.md)
