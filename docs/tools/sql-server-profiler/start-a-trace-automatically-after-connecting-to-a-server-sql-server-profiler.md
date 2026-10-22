---
title: Start a Trace Automatically After Connecting to a Server
titleSuffix: SQL Server Profiler
description: Learn how to start tracing event data automatically in SQL Server Profiler after connecting to an instance of SQL Server.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# Start a trace automatically after connecting to a server (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

This article describes how to start traces automatically after connecting to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)].

## Start a trace automatically after connecting to a server with SQL Server Profiler

1. On the **Tools** menu, select **Options**.

1. Select the **Start tracing immediately after making a connection** check box.

If **Start tracing immediately after making connection** is selected, the **Trace Properties** dialog box fails to appear and the trace begins instead. To edit trace properties, you must first turn this setting off.

## Related content

- [SQL Server Profiler](sql-server-profiler.md)
