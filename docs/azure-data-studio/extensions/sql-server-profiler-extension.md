---
title: SQL Server Profiler extension
description: Learn how to install and use the SQL Server Profiler extension. An easy-to-use SQL Server tracing solution similar to the SQL Server Management (SSMS) Profiler.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan
ms.date: 08/07/2023
ms.service: azure-data-studio
ms.topic: conceptual
---

# SQL Server Profiler extension (Preview)

The SQL Server Profiler extension (preview) provides a simple SQL Server tracing solution similar to [SQL Server Management Studio (SSMS) Profiler](../../tools/sql-server-profiler/sql-server-profiler.md) except built using [Extended Events](../../relational-databases/extended-events/extended-events.md). SQL Server Profiler is easy to use and has good default values for the most common tracing configurations. The UX is optimized for browsing through events and viewing the associated Transact-SQL (T-SQL) text. The SQL Server Profiler for Azure Data Studio also assumes good default values for collecting T-SQL execution activities with an easy to use UX. This extension is currently in preview.

**Common SQL Profiler use-cases:**

- Stepping through problem queries to find the cause of the problem.
- Finding and diagnosing slow-running queries.
- Capturing the series of Transact-SQL statements that lead to a problem.
- Monitoring the performance of SQL Server to tune workloads.
- Correlating performance counters to diagnose problems.
- Opening an existing XEL file for review.

## Install the SQL Server Profiler extension

1. To open the extensions manager and access the available extensions, select the extensions icon, or select **Extensions** in the **View** menu.
2. Select an available extension to view its details.

    ![Profiler Extension Manager](media/sql-server-profiler-extension/profiler-extension.png)

3. Select the extension you want and **Install** it.
4. Select **Reload** to enable the extension (only required the first time you install an extension).

## Start Profiler

1. To start Profiler, first make a connection to a server in the Servers tab.
2. After you make a connection, type **Alt + P** to launch Profiler, or right-click on the server connection and select **Launch Profiler.**
3. Select a session template from the dropdown.  For Azure SQL DB, Standard_Azure is the only template available.
4. Edit the session name if appropriate, and select Start.
5. The session will be started automatically and you will start to see events in the viewer.
6. To stop Profiler, type **Alt + S** or use the Stop button in the toolbar.  
7. The hotkey **Alt + S** is a toggle. To restart Profiler, type **Alt + S** or use the Start button in the toolbar.  

    ![View profiler](media/sql-server-profiler-extension/view-profiler.png)

## Open a saved XEL file

1. To view a XEL file that you have saved locally, open the Command Palette using **Ctrl/CMD + Shift + P** and then type **Profiler: Open XEL File** and select the command.
2. Browse to the saved XEL file and select Open.
3. The file will open in the viewer. 

## Next steps

To learn more about Profiler and extended events, see [Extended Events](../../relational-databases/extended-events/extended-events.md).
