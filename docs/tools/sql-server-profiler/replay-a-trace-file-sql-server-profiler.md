---
title: Replay a Trace File
titleSuffix: SQL Server Profiler
description: Get help troubleshooting problems by replaying trace files in SQL Server Profiler. Learn about replay capabilities and options.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# Replay a trace file (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

Replay is the ability to open a saved trace and replay it again. [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] features a multithreaded playback engine that can simulate user connections and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. Replay is useful to troubleshoot an application or process problem. When you identify the problem and implement corrections, run the trace that found the potential problem against the corrected application or process. Then, replay the original trace and compare results.

In addition to any other event classes you want to monitor, specific event classes must be captured to enable replay. These events are captured by default if you use the **TSQL_Replay** trace template. For more information, see [Replay Requirements](replay-requirements.md).

## Replay a trace file

1. On the **File** menu, point to **Open**, and then select **Trace File**. Select a trace file that contains the event classes necessary for replay.

1. On the **Replay** menu, select **Start**, and connect to the server instance where you want to replay the trace.

1. In the **Replay Configuration** dialog box, on the **Basic Replay Options** tab, specify the **Replay server**. Select **Change** to change the server displayed in the **Replay server** box.

1. Optionally, select one of the following destinations in which to save the replay:

   - **Save to file**, which specifies a file in which to save the replay.
   - **Save to table**, which specifies a database table in which to save the replay.

1. Choose either **Replay the events in the order they were traced** or **Replay events using multiple threads**. The following table explains the difference between these settings.

   | Option | Description |
   | --- | --- |
   | **Replay events in the order they were traced** | Replays events in the order they were recorded. This option enables debugging. |
   | **Replay events using multiple threads** | This option uses multiple threads to replay each event regardless of the sequence. This option optimizes performance. |

1. Select **Display replay results** to view the replay as it occurs.

1. Optionally select the **Advanced Replay Options** tab to configure the following options:

   - To replay all session IDs (SPIDs), select **Replay system SPIDs**.

   - To limit the replay to processes belonging to a specific session ID, select **Replay one SPID only**. In the **SPID to replay** box, type the session ID.

   - To replay events that occurred during a specific time period, select **Limit the replay by date and time**. Select a date and time for the **Start time** and **End time** to specify the time period to include in the replay.

   - To control how SQL Server manages processes during replay, configure **Health Monitor Options**.

## Related content

- [Permissions required to run SQL Server Profiler](permissions-required-to-run-sql-server-profiler.md)
- [Replay Traces](replay-traces.md)
- [Open a trace file (SQL Server Profiler)](open-a-trace-file-sql-server-profiler.md)
- [SQL Server Profiler](sql-server-profiler.md)
