---
title: Filter Events Based on the Event Start Time
titleSuffix: SQL Server Profiler
description: Filter events by start time during a trace. Learn how to set up a filter on the event start time in SQL Server Profiler.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# Filter events based on the event start time (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to filter trace events based on the event start time by using [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)].

## Filter an event based on the event start time

1. On the **File** menu, select **New Trace**, and then connect to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

   The **Trace Properties** dialog box appears.

   If **Start tracing immediately after making connection** is selected, the **Trace Properties** dialog box fails to appear, and the trace begins instead. To turn off this setting, on the **Tools** menu, select **Options**, and clear the **Start tracing immediately after making connection** check box.

1. In the **Trace name** box, type a name for the trace.

1. In the **Use the template** name list, select a trace template.

1. Optionally specify a destination for the trace results.

1. On the **Events Selection** tab, select the **StartTime** column heading. You can also right-click the column heading, and then select **Edit Column Filter** to launch the **Edit Filter** dialog box.

1. Expand **Greater than** or **Less than**, and then enter a **datetime** value in the field that appears beneath the comparison operator.

## Related content

- [SQL Server Profiler](sql-server-profiler.md)
