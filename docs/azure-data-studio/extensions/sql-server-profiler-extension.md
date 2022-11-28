---
title: SQL Server Profiler extension
description: Learn how to install and use the SQL Server Profiler extension. An easy-to-use SQL Server tracing solution similar to the SQL Server Management (SSMS) Profiler.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan
ms.date: 06/28/2021
ms.service: azure-data-studio
ms.topic: conceptual
ms.custom: seodec18
---

# SQL Server Profiler extension (Preview)

The SQL Server Profiler extension (preview) provides a simple SQL Server tracing solution similar to [SQL Server Management Studio (SSMS) Profiler](../../tools/sql-server-profiler/sql-server-profiler.md) except built using [Extended Events](../../relational-databases/extended-events/extended-events.md). SQL Server Profiler is easy to use and has good default values for the most common tracing configurations. The UX is optimized for browsing through events and viewing the associated Transact-SQL (T-SQL) text. The SQL Server Profiler for Azure Data Studio also assumes good default values for collecting T-SQL execution activities with an easy to use UX. This extension is currently in preview.

**Common SQL Profiler use-cases:**

- Stepping through problem queries to find the cause of the problem.
- Finding and diagnosing slow-running queries.
- Capturing the series of Transact-SQL statements that lead to a problem.
- Monitoring the performance of SQL Server to tune workloads.
- Correlating performance counters to diagnose problems.

## Install the SQL Server Profiler extension

1. To open the extensions manager and access the available extensions, select the extensions icon, or select **Extensions** in the **View** menu.
2. Select an available extension to view its details.

    ![Profiler Extension Manager](media/sql-server-profiler-extension/profiler-extension.png)

3. Select the extension you want and **Install** it.
4. Select **Reload** to enable the extension (only required the first time you install an extension).

## Start Profiler

1. To start Profiler, first make a connection to a server in the Servers tab.
2. After you make a connection, type **Alt + P** to launch Profiler.
3. To start Profiler, type **Alt + S.** You can now start seeing Extended Events.

    ![View profiler](media/sql-server-profiler-extension/view-profiler.png)

4. To stop Profiler, type **Alt + S.** This hotkey is a toggle.

## Next steps

To learn more about Profiler and extended events, see [Extended Events](../../relational-databases/extended-events/extended-events.md).