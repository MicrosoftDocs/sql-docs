---
title: SQL Server Profiler extension
titleSuffix: Azure Data Studio
description: Install and use the SQL Server Profiler extension (preview) for Azure Data Studio
ms.custom: "seodec18"
ms.date: "09/24/2018"
ms.reviewer: "alayu; sstein"
ms.prod: sql
ms.technology: azure-data-studio
ms.topic: conceptual
author: "yualan"
ms.author: "alayu"
manager: craigg
---
# SQL Server Profiler extension (preview)

The SQL Server Profiler extension (preview) provides a simple SQL Server tracing solution similar to SQL Server Management Studio (SSMS) Profiler except built using XEvents. SQL Server Profiler is very easy to use and has good default values for the most common tracing configurations. The UX is optimized for browsing through events and viewing the associated Transact-SQL (T-SQL) text. The SQL Server Profiler for Azure Data Studio also assumes good default values for collecting T-SQL execution activities with an easy to use UX. This extension is currently in preview.

**Common SQL Profiler use-cases:**

- Stepping through problem queries to find the cause of the problem.
- Finding and diagnosing slow-running queries.
- Capturing the series of Transact-SQL statements that lead to a problem.
- Monitoring the performance of SQL Server to tune workloads.
- Correlating performance counters to diagnose problems.


## Install the SQL Server Profiler extension

1. To open the extensions manager and access the available extensions, select the extensions icon, or select **Extensions** in the **View** menu.
2. Select an available extension to view its details.

   ![profiler extension manager](media/extensions/sql-server-profiler-extension/profiler-extension.png)

1. Select the extension you want and **Install** it.
2. Select **Reload** to enable the extension (only required the first time you install an extension).

## Start Profiler

1. To start Profiler, first make a connection to a server in the Servers tab.
2. After you make a connection, type **Alt + P** to launch Profiler.
3. To start Profiler, type **Alt + S.** You can now start seeing Extended Events.
    ![profiler extension manager](media/extensions/sql-server-profiler-extension/view-profiler.png)    
1. To stop Profiler, type **Alt + S.** This hotkey is a toggle.

## Next steps

To learn more about Profiler and extended events, see [Extended Events](https://docs.microsoft.com/sql/relational-databases/extended-events/extended-events).





