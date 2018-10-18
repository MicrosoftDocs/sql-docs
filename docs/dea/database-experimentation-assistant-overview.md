---
title: Overview of Database Experimentation Assistant solution for SQL Server upgrades
description: Overview of Database Experimentation Assistant
ms.custom: ""
ms.date: 10/12/2018
ms.prod: sql
ms.prod_service: dea
ms.suite: sql
ms.technology: dea
ms.tgt_pltfrm: ""
ms.topic: conceptual
author: HJToland3
ms.author: jtoland
ms.reviewer: douglasl
manager: craigg
---

# Overview of Database Experimentation Assistant
Database Experimentation Assistant (DEA) is an Experimentation solution for SQL Server upgrades. It assists in evaluating a targeted version of SQL for a given workload. Customers who are upgrading from previous SQL Server versions (starting 2005 and above) to any new version of the SQL Server will be able to use these analysis metrics provided by the tool, such as queries that have compatibility errors, degraded queries and query plans, and other workload comparison data to help them build higher confidence, making it a successful upgrade experience.

For a 19-minute introduction and demonstration of this feature, watch the following video:

> [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Introducing-the-Database-Experimentation-Assistant/player]

## Get the Database Experimentation Assistant
To install DEA, download the latest version of the tool from the [Microsoft Download Center](https://www.microsoft.com/en-us/download/details.aspx?id=54090) and then run the DatabaseExperimentationAssistant.exe file.

## Solution architecture for comparing workloads
The following diagram shows the solution architecture comparing workloads between the different versions of SQL Server using DEA and distributed replay when upgrading from SQL Server 2008 to SQL Server 2016.

![workloadcomparesolutionarchitecture](./media/database-experimentation-assistant-overview/dea-overview-compare-solution-architecture.png)

## Database Experimentation Assistant prerequisites
Following are some prerequisites for running DEA:
- A minimum hardware requirement of single core 3.5-GB RAM machines.
- An ideal hardware requirement of an 8-core CPU (with RAM 3.5 GB or higher). More than eight cores do not help DEA runtimes.
- An additional 33% of performance trace size is needed to store A, B, and Analysis databases.

## Set up and configure DEA
In the above environment architecture, we recommended that you install **DEA on the same machine as Distributed Replay controller**. This avoids cross-machine calls and simplifies configuration.

### Configuration requirement for workload comparison using DEA
DEA connects to database servers using Windows authentication. Ensure that a user running DEA can connect to database servers (source/target/analysis) using Windows authentication.

Capture: [Learn More](database-experimentation-assistant-capture-trace.md#frequently-asked-questions-about-capture-trace)

*   User running DEA can connect to source database server using *Windows authentication.*
*   User running DEA has *sysadmin* rights on the source database server.
*   Service account running source database server has *write access* to the trace folder path.

Replay: [Learn More](database-experimentation-assistant-replay-trace.md#frequently-asked-questions-about-replay-trace)

*   User running DEA can connect to target database server using *Windows authentication*.
*   User running DEA has *sysadmin* rights on the target database server.
*   Service account running target database servers have *write access* to the trace folder path.
*   Service account running Distributed Replay clients can connect to target database server using *Windows authentication*.
*   DEA communicates with Distributed Replay controller using COM interfaces. Ensure that TCP ports are opened for incoming requests on the distributed replay controller.

Analysis: [Learn More](database-experimentation-assistant-create-report.md#frequently-asked-questions-about-analysis-reports)

*   User running DEA can connect to the analysis database server using *Windows authentication.*
*   User running DEA has *sysadmin* rights on the source database server.

## Set up telemetry
Database Experimentation Assistant contains an internet-enabled feature that can send telemetry information back to Microsoft. Microsoft collects telemetry to enhance the product experience. It is optional, and the information collected is also saved on your computer for Local Audit so that you can always see what gets collected. All log files from DEA are saved in %temp%\\DEA folder.

In addition, you can decide which events get collected, and whether the collected events get sent to Microsoft or not. There are four types of events:

*   TraceEvent: usage events for the application (for example “triggered stop capture”)
*   Exception: exception thrown during application usage
*   DiagnosticEvent: event log to help in diagnosing when problems occur – NOT sent to Microsoft
*   FeedbackEvent: user feedback submitted through the application

These steps show you how to choose which events are collected, and whether they are sent to Microsoft.

1.  Go to the location where DEA is installed (for example, C:\\Program Files (x86)\\Microsoft Corporation\\Database Experimentation Assistant).
2.  Open .config files. There are two: DEA.exe.config (for the application), DEACmd.exe.config (for the CLI).
3.  To stop collecting a type of event, set the value of \[event\] (for example, TraceEvent) to “false”. To start collecting the event again, set the value to “true”.
4.  To stop saving local copies of events, set the value of TraceLoggerEnabled to “false”. To start saving local copies again, set the value to “true”.
5.  To stop sending events to Microsoft, set the value of AppInsightsLoggerEnabled to “false”. To start sending events to Microsoft again, set the value to “true”.

DEA is governed by [Microsoft's Online Privacy Policy](https://go.microsoft.com/fwlink/?LinkId=521839​).

## Next steps

[Get started](database-experimentation-assistant-get-started.md) walks you through the steps required to capture, replay, and analyze a trace.
