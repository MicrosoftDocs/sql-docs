---
title: Overview of the workload comparison process
description: Database Experimentation Assistant (DEA) is an A/B testing solution for changes in SQL Server environments, such as upgrades or new indexes.
author: pochiraju
ms.author: rajpo
ms.reviewer: mathoma
ms.date: 12/12/2019
ms.service: sql
ms.subservice: dea
ms.topic: conceptual
ms.custom: seo-lt-2019
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

- Before you starting, be sure to back up the databases from which you'll be capturing the trace.
- The DEA user must be able to connect to the database by using Windows authentication.
- A SQL Server service account must be able to access the source trace file path.
- For DEA to determine whether the performance of a query is improved or degraded, that query must execute at least 15 times during the capture period.

## Replaying a workload trace

The second stage of SQL Server A/B testing is to replay the trace file you captured on two target servers:

Target 1, which mimics your source server
Target 2, which mimics your proposed target environment.

The hardware configurations of Target 1 and Target 2 should be as similar as possible so that SQL Server can accurately analyze the performance effect of your proposed changes.

Considerations:

- To replay a workload trace, your computers must be set up to run Distributed Replay (DReplay) traces.
- Be sure to restore the databases on your target servers by using the backup from the source server.
- It's recommended to restart the SQL Server service (MSSQLSERVER) in the services application to improve consistency in evaluation results. Query caching in SQL Server can affect evaluation results.

## Analyzing the replayed workload traces

The final stage in the process is to generate an analysis report using the replay traces and to review the report for insights about the potential performance implications of the proposed change.

Considerations:

- If one or more components are missing, a prerequisites page with links for downloads appears when you try to generate a new analysis report (Internet connection required).
- To view a report generated in an earlier version of the tool, you must first update the schema.

## See also

- To learn how to produce a trace file with a log of events that occur on a server, see the article [Capture a trace in Database Experimentation Assistant](database-experimentation-assistant-capture-trace.md).