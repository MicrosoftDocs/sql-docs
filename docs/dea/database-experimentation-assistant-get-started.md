---
title: Get started with Database Experimentation Assistant for SQL Server upgrades
description: Get started with Database Experimentation Assistant
ms.custom: ""
ms.date: 10/22/2018
ms.prod: sql
ms.prod_service: dea
ms.suite: sql
ms.technology: dea
ms.tgt_pltfrm: ""
ms.topic: conceptual
author: HJToland3
ms.author: ajaykar
ms.reviewer: douglasl
manager: craigg
---

# Get started with Database Experimentation Assistant

Database Experimentation Assistant (DEA) is an A/B testing solution for changes in SQL Server environments, such as upgrades or new indexes. DEA helps you evaluate how the workload on your source server (in your current environment) will perform in your new environment. DEA guides you through running an A/B test by completing three steps: 

- Capture
- Replay
- Analysis

This article walks you through these steps.

## Capture

The first step of SQL Server A/B testing is to capture a trace on your source server. The source server usually is the production server. Trace files capture the entire query workload on that server, including timestamps. Later, this trace is replayed on your target servers for analysis. The analysis report provides insights on the difference in performance of the workload between your two target servers.

Considerations:

- Before you start your trace capture, make sure that you back up the databases from which you're capturing a trace.
- A DEA user must be configured to connect to the database by using Windows authentication.
- A SQL Server service account requires access to the source trace file path.

To capture a trace on your source server:

1. In DEA, go to **All Captures** by selecting the camera icon in the left menu.

   ![Left navigation menu](./media/database-experimentation-assistant-get-started/dea-get-started-leftnav.png)

1. Enter or select the following information:

   - **Trace name**: The file name for the new trace file you're creating. Avoid a trace name that uses the rollover file naming convention, for example, CaptureName\_NNN.
   - **Duration**: The duration for the capture.
   - **SQL Server instance name**: The SQL Server instance from which you want to capture a trace.
   - **Database name**: The name of the database on the computer running SQL Server that you want capture a trace of. If left blank, trace is captured from all databases on the server.
   - **Path to store source trace file on SQL Server machine**: The folder path where you want to save the trace file.

1. Make sure that the target database is backed up. Then, select the database check box.
1. Select **Start** to start the capture.

You can view the progress of your capture, including start time, duration, and time remaining. You also can start a new capture while you wait for this capture to finish. When your capture is finished, use the output trace file to start the second phase: replay the trace file on your target servers.

For common questions about trace capture, see the [capture FAQ](database-experimentation-assistant-capture-trace.md#frequently-asked-questions-about-trace-capture).

## Replay

The second step of SQL Server A/B testing is to replay the trace file that was captured to your target servers. Then,  collect extensive traces from the replays for analysis. 

You replay the trace file on two target servers: one that mimics your source server (Target 1) and one that mimics your proposed change (Target 2). The hardware configurations of Target 1 and Target 2 should be as similar as possible so SQL Server can accurately analyze the performance effect of your proposed changes.

Considerations:

- To run replay, your machines must be set up to run Distributed Replay (DReplay) traces. For more information, see [Distributed Replay controller and client setup](https://blogs.msdn.microsoft.com/datamigration/distributed-replay-controller-and-client-setup/).
- Make sure that you restore the databases on your target servers by using the backup from the source server.
- Query caching in SQL Server can affect evaluation results. We recommend that you restart the SQL Server service (MSSQLSERVER) in the services application to improve consistency in evaluation results.

To replay the trace file:

1. In DEA, select the play icon in the left menu to go to **All Replays**. The list of past replays that run during the session, if any, appear. To start a new replay, select **New Replay**.

1. Enter or select the following information:

   - **Replay name**: The file name for the replay trace.
   - **Controller machine name**: The name of the Distributed Replay controller machine.
   - **Path to source trace file on controller**: The file path for the source trace file from [Capture](#Capture).
   - **SQL Server instance name**: The name of the SQL Server instance on which to replay the source trace.
   - **Path to store target trace file on SQL Server machine**: The folder path for the resulting replay trace file.

1. Select the check box to restore the backup from the first step.

1. Select **Start** to start the replay. 

You can view the status of your replay. After you replay the source trace on both of your target servers, you're ready to generate an analysis report.

For common questions about replay, see the [replay FAQ](database-experimentation-assistant-replay-trace.md#frequently-asked-questions-about-trace-replay).

## Analysis

The final step is to generate an analysis report by using the replay traces. The analysis report can help you gain insight about the performance implications of the proposed change.

Considerations:

- If one or more components are missing, a prerequisites page with links for downloads appears when you try to generate a new analysis report (internet connection required).
- To view a report that was generated in an earlier version of the tool, you must first update the schema.

To generate an analysis report:

1. In the left menu, go to **Analysis Reports**. Connect to the computer running SQL Server where you store your report databases. A list of all reports in the server appears. To create a new report, select **New Report**.

1. Enter or select the information that's required to generate a report:

   - **Report name**: The name of the analysis report to create.
   - **Trace for Target 1 SQL Server**: The path for the trace file from replaying on Target 1.
   - **Trace for Target 2 SQL Server**: The path for the trace file from replaying on Target 2.

1. Select **Start** to generate the report. The new report appears at the top of the list. The icon next to the report becomes a green checkmark when the report has been generated.

Now, view the analysis report to gain insights provided by your A/B test.

For common questions about analysis reports, see the [analysis FAQ](database-experimentation-assistant-create-report.md#frequently-asked-questions-about-analysis-reports).

### Analysis report

On the first page of your report, version and build information for the target servers on which the experiment was run is shown. You can use threshold to adjust the sensitivity or tolerance of your A/B Test analysis. By default, the threshold is set at 5%. Any improvement in performance that is more than or equal to 5% is categorized as **Improved**. Select options in the drop-down menu to evaluate the report by using different performance thresholds.

![Threshold](https://msdnshared.blob.core.windows.net/media/2017/03/threshold.jpg)

Two pie charts demonstrate the performance implications of the difference between the two target servers for your workload. The left chart is based on execution count. The right chart is based on distinct queries. There are five possible categories:

- **Improved**:  Statistically, the query ran better on Target 2 than on Target 1.
- **Degraded**: Statistically, the query ran worse on Target 2 than on Target 1.
- **Same**: There's no statistical difference for the query between Target 1 and Target 2.
- **Cannot Evaluate**: The sample size for the query is too small for statistical analysis. For A/B testing analysis, DEA requires the same queries to have at least 30 executions on each target.
- **Error**: The query errored out at least once on one of the targets.

![Pie chart](./media/database-experimentation-assistant-get-started/dea-get-started-piechart.png)

Select a slice to drill down into a particular category and get performance metrics, including the **Cannot Evaluate** pie slice.

The drill-down page for a performance change category shows a list of queries in that category. The **Error** page has three tabs:

- **New Errors**: Errors that appeared on Target 2 but not on Target 1.
- **Existing Errors**: Errors that appeared on both Target 1 and Target 2.
- **Resolved Errors**: Errors that appeared on Target 1 but not on Target 2.

   ![Error page](./media/database-experimentation-assistant-get-started/dea-get-started-errorpage.png)

Select a query to go to a **Comparison Summary** page for that query.

The **Comparison Summary** page shows summary statistics for that query. The summary includes the number of executions, mean duration, mean CPU, mean reads/writes, and error count.

![Summary stats](./media/database-experimentation-assistant-get-started/dea-get-started-summarystats.png)

If the query is an error query, the **Error Information** tab shows more information about the error. The **Query Plan Information** tab shows information about the query plans that are used for the query on Target 1 and Target 2.

![Query plan](./media/database-experimentation-assistant-get-started/dea-get-started-queryplan.png)

On any page of the analysis report, select the **Print** button on the top right to print everything that's visible.

## Next steps

- To learn how to produce a trace file that has a log of events that occur on a server, see [Capture trace](database-experimentation-assistant-capture-trace.md).

- For a 19-minute introduction to DEA and demonstration, watch the following video:

  > [!VIDEO https://channel9.msdn.com/Shows/Data-Exposed/Introducing-the-Database-Experimentation-Assistant/player]
