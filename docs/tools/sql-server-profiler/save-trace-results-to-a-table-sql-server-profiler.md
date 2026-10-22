---
title: Save Trace Results to a Table
titleSuffix: SQL Server Profiler
description: Learn how to use SQL Server Profiler to save trace results to a table in a SQL Server database. Find out how you can specify the maximum number of rows to save.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# Save trace results to a table (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to save trace results to a database table by using [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)].

## Save trace results to a table

1. On the **File** menu, select **New Trace**, and then connect to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

   The **Trace Properties** dialog box appears.

   If **Start tracing immediately after making connection** is selected, the **Trace Properties** dialog box fails to appear and the trace begins instead. To turn off this setting, on the **Tools** menu, select **Options**, and clear the **Start tracing immediately after making connection** check box.

1. In the **Trace name** box, type a name for the trace, and then select **Save to table**.

1. In the **Connect to server** dialog box, connect to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database that will contain the trace table.

1. In the **Destination Table** dialog box, select a database from the **Database** list.

1. In the **Owner** list, select the owner for the trace.

1. In the **Table** list, type or select the table name for the trace results. Select **OK**.

1. In the **Trace Properties** dialog box, select the **Set maximum rows (in thousands)** check box to specify the maximum number of rows to save.

## Related content

- [SQL Server Profiler](sql-server-profiler.md)
