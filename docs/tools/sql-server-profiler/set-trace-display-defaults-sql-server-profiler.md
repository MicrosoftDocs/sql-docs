---
title: Set Trace Display Defaults
titleSuffix: SQL Server Profiler
description: Explore the tool options that SQL Server Profiler uses by default in traces, such as the font type, size, and style, and learn how to adjust them.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# Set trace display defaults (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to set tool options, which specify the font types, size, and style that [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] uses to display traces in the trace window. You can also specify that [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays all dates and times with the regional settings that are configured for your operating system.

## Set trace display defaults

1. On the **Tools** menu, select **Options**.

1. In the **General Options** dialog box, select **Choose Font**.

1. In the **Font** dialog box, select the **Font**, the **Font style**, and **Size** to be used by [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to display traces.

1. Select **OK** to apply the settings and close the **Font** dialog box.

1. In the **General Options** dialog box, check **Use regional settings to display date and time values** to configure [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to use the regional settings that are configured for your system when it displays traces.

1. Select **OK** to close the **General Options** dialog box.

## Related content

- [SQL Server Profiler](sql-server-profiler.md)
