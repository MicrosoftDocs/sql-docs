---
title: Overview of the workload comparison process
description: Database Experimentation Assistant (DEA) is an A/B testing solution for changes in SQL Server environments, such as upgrades or new indexes.
ms.date: 11/16/2019
ms.prod: sql
ms.prod_service: dea
ms.suite: sql
ms.technology: dea
ms.tgt_pltfrm: ""
ms.topic: conceptual
author: HJToland3
ms.author: jtoland
ms.reviewer: mathoma
ms.custom: "seo-lt-2019"
---

# Overview of the workload comparison process

Database Experimentation Assistant (DEA) helps you evaluate how the workload on your source server (in your current environment) will perform in your new environment. DEA guides you through running an A/B test by completing three stages:

- Capturing a workload trace on the source server.
- Replaying the captured workload trace on target 1 and target 2.
- Analyzing the replayed workload traces collected from target 1 and target 2.

This article provides an overview of this process.

## Capturing a workload trace

The first stage of SQL Server A/B testing is to capture a trace on your source server. The source server usually is the production server. Trace files capture the entire query workload on that server, including timestamps.

Considerations:

- Before you start capturing your workload trace, be sure to back up the databases from which you're capturing the trace.
- A DEA user must be configured to connect to the database by using Windows authentication.
- A SQL Server service account requires access to the source trace file path.
- For DEA to determine whether the performance of a query is improved or degraded, that query must execute at least 15 times during the capture period.

## Replaying a workload trace

The second stage of SQL Server A/B testing is to replay the trace file you captured on two target servers:

Target 1, which mimics your source server
Target 2, which mimics your proposed target environment.

The hardware configurations of Target 1 and Target 2 should be as similarly as possible so SQL Server can accurately analyze the performance effect of your proposed changes.

Considerations:

- To replay a workload trace, your computers must be set up to run Distributed Replay (DReplay) traces.
- Be sure to restore the databases on your target servers by using the backup from the source server.
- It is recommended to restart the SQL Server service (MSSQLSERVER) in the services application to improve consistency in evaluation results. Query caching in SQL Server can affect evaluation results.

## Analyzing the replayed workload traces

The final stage in the process is to generate an analysis report using the replay traces. You can then review the  analysis report for insights about the potential performance implications of the proposed change.

Considerations:

- If one or more components are missing, a prerequisites page with links for downloads appears when you try to generate a new analysis report (internet connection required).
- To view a report that was generated in an earlier version of the tool, you must first update the schema.

## See also

- > [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Introducing-the-Database-Experimentation-Assistant/player?WT.mc_id=dataexposed-c9-niner]
- To learn how to produce a trace file with a log of events that occur on a server, see [Capture trace](database-experimentation-assistant-capture-trace.md).

