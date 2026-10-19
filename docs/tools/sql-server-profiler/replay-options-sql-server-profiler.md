---
title: Replay Options
titleSuffix: SQL Server Profiler
description: Explore the settings that SQL Server Profiler uses when replaying a captured trace. Learn how to use the Replay Configuration dialog box to adjust the settings.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: concept-article
ms.collection:
  - data-tools
---

# Replay options (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

Before replaying a captured trace with [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)], specify replay options in the **Replay Configuration** dialog box. To launch this dialog box, open the replay trace file or table in [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)], and on the **Replay** menu, select **Start**. For information about what permissions are required to replay a trace, see [Permissions required to run SQL Server Profiler](permissions-required-to-run-sql-server-profiler.md).

This article describes the options specified with the **Replay Configuration** dialog box.

You should use the Distributed Replay Utility for replaying an intensive OLTP application (with many active concurrent connections or high throughput). The Distributed Replay Utility can replay trace data from multiple computers, better simulating a mission-critical workload. For more information, see [SQL Server Distributed Replay overview](../distributed-replay/sql-server-distributed-replay.md).

## Basic replay options

#### Replay server

The server is the name of the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] against which you want to replay the trace. The server must adhere to the replay requirements described in [Replay Requirements](replay-requirements.md)."

#### Save to file

The output file where the result of replaying the trace is written for later viewing. By default, [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays only the results of replaying the trace on the screen.

#### Save to table  

The database table where the result of replaying the trace is written for later viewing.

#### Number of replay threads  

Specify the number of replay threads to use concurrently. A higher number consumes more resources during replay, but replay is faster. Event ordering isn't fully maintained when multiple threads are used.

#### Replay events in the order they were traced  

Allows you to use debugging methods such as stepping through each trace. If this option isn't selected, replay doesn't guarantee that events are replayed in an order that is consistent with the order in which events were originally captured.

#### Replay events using multiple threads  

Optimizes performance and disables debugging. Events are replayed in the order they were recorded for a particular session ID (SPID), but ordering of session ID isn't guaranteed.

#### Display replay results  

Display the results of the replay. This is the default option. If the trace you're replaying is very large, you might want to disable this to save disk space.

For best replay performance, you should choose to replay events using multiple threads, and don't display the replay results.

## Advanced replay options

#### Replay system SPIDs

Replay all session IDs. This is the default option.

#### Replay one SPID only

Replays the session ID you choose from the list.

#### Limit replay by date and time

Replays the trace for the specified **Start time** and **End time**.

#### Health monitor wait interval

Sets the amount of time a process is allowed to run before the health monitor terminates it.

#### Health monitor poll interval

Sets how often the health monitor polls candidates for termination.

#### Enable SQL Server blocked processes monitor

Sets how often the blocked processes monitor searches for blocked or blocking processes.

## About the health monitor

The health monitor is an application thread that monitors the simulated processes involved in replaying a trace, and ends those processes that are blocked within the replay. In the **Advanced Replay Options** tab of the **Replay Configuration** dialog box, you can specify how long the health monitor should wait in seconds before ending a blocked process (**Health monitor wait interval**). If you set this interval to 0, the health monitor never ends simulated blocking processes in the replaying trace.

## Related content

- [Replay Traces](replay-traces.md)
- [Replay Requirements](replay-requirements.md)
- [Considerations for replaying traces (SQL Server Profiler)](considerations-for-replaying-traces-sql-server-profiler.md)
