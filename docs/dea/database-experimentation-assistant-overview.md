---
title: Overview of the Database Experimentation Assistant
description: Learn details about Database Experimentation Assistant (DEA), for example how to evaluate a targeted version of SQL Server for a specific workload.
author: pochiraju
ms.author: rajpo
ms.reviewer: mathoma
ms.date: 12/12/2019
ms.service: sql
ms.subservice: dea
ms.topic: conceptual
ms.custom: seo-lt-2019
---
# Overview of Database Experimentation Assistant

Database Experimentation Assistant (DEA) is an experimentation solution for SQL Server upgrades. DEA can help you evaluate a targeted version of SQL Server for a specific workload. Customers upgrading from earlier versions of SQL Server (starting with 2005) to more recent versions of SQL Server can use the analysis metrics that the tool provides.

DEA analysis metrics include:

- Queries that have compatibility errors.
- Degraded queries and query plans.
- Other workload comparison data.

Comparison data can lead to higher confidence and help ensure a successful upgrade experience.

## Get DEA

To install DEA, [download](https://www.microsoft.com/download/details.aspx?id=54090) the latest version of the tool. Then, run the **DatabaseExperimentationAssistant.exe** file.

## Solution architecture for comparing workloads

The following diagram shows the solution architecture for a workload comparison. The workload comparison uses DEA and Distributed Replay during an upgrade from SQL Server 2008 to SQL Server 2016.

![Workload comparison solution architecture](./media/database-experimentation-assistant-overview/dea-overview-compare-solution-architecture.png)

## DEA prerequisites

Following are some prerequisites for running DEA:

- Minimum hardware requirement: A single-core machine with 3.5 GB of RAM.
- Ideal hardware requirement: An eight-core CPU (with 3.5 GB of RAM or more). Processors with more than eight cores don't improve DEA run times.
- An additional 33% of performance trace size is needed to store A, B, and report analysis databases.

## Configure DEA

In the prerequisite environment architecture, we recommend that you install DEA *on the same machine as the Distributed Replay controller*. This practice avoids cross-computer calls and simplifies configuration.

### Required configuration for workload comparison using DEA

DEA connects to database servers using Windows authentication. Be sure that the user running DEA can connect to database servers (source, target, and analysis) using Windows authentication.

**Capture configuration requirements**

Capturing a trace requires that the user running DEA:

- Can connect to the source database server using Windows authentication.
- Has sysadmin rights on the source database server.

In addition, the service account running the source database server requires write access to the trace folder path.

For more information, see [Frequently asked questions about trace capture](database-experimentation-assistant-capture-trace.md#frequently-asked-questions-about-trace-capture).

**Replay configuration requirements**

Replaying a trace requires that the user running DEA:

- Can connect to the target database server using Windows authentication.
- Has sysadmin rights on the target database server.

In addition, replaying a trace requires that:

- The service account running the target database servers has write access to the trace folder path.
- The service account running Distributed Replay clients can connect to the target database server using Windows authentication.
- TCP ports are opened for incoming requests on the Distributed Replay controller. DEA communicates with the Distributed Replay controller by using COM interfaces.

For more information, see [Frequently asked questions about trace replay](database-experimentation-assistant-replay-trace.md#frequently-asked-questions-about-trace-replay).

**Analysis configuration requirements**

Performing the analysis requires that the user running DEA:

- Can connect to the analysis database server using Windows authentication.
- Has sysadmin rights on the source database server.

For more information, see [Frequently asked questions about analysis reports](database-experimentation-assistant-create-report.md#frequently-asked-questions-about-analysis-reports).

## Set up telemetry

DEA has an internet-enabled feature that can send telemetry information to Microsoft for use in enhancing the product experience. The information that's collected is also saved on your computer for local audit, so you can always see what's collected. All DEA log files are saved in the %temp%\\DEA folder.

Telemetry data can be collected on four types of events:

- **TraceEvent**: Usage events for the application (for example, "triggered stop capture").
- **Exception**: Exception thrown during application usage.
- **DiagnosticEvent**: An event log to assist with diagnosis when problems occur (*not* sent to Microsoft).
- **FeedbackEvent**: User feedback that's submitted through the application.

Collecting and sending telemetry data is optional. To specify which events are collected and whether collected events are sent to Microsoft, use the following steps:

1. Go to the location in which DEA is installed (for example, C:\\Program Files (x86)\\Microsoft Corporation\\Database Experimentation Assistant).
2. Open and modify the .config files **DEA.exe.config** (for the application) and **DEACmd.exe.config** (for the CLI) to address your scenario as appropriate:
    - To stop collecting a type of event, set the value of *event* (for example, **TraceEvent**) to **false**. To start collecting the event again, set the value to **true**.
    - To stop saving local copies of events, set the value of **TraceLoggerEnabled** to **false**. To start saving local copies again, set the value to **true**.
    - To stop sending events to Microsoft, set the value of **AppInsightsLoggerEnabled** to **false**. To start sending events to Microsoft again, set the value to **true**.

DEA is governed by the [Microsoft Privacy Statement](https://aka.ms/dea-privacy).

## See also

- The article [Overview of the workload comparison process](database-experimentation-assistant-get-started.md), which explains the process involved in comparing workloads in two environments.
