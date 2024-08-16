---
title: Monitoring and performance tuning
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: An overview of monitoring and performance tuning capabilities and methodology in Azure SQL Database and Azure SQL Managed Instance.
author: dimitri-furman
ms.author: dfurman
ms.reviewer: wiassaf, mathoma, urmilano
ms.date: 08/16/2024
ms.service: azure-sql
ms.subservice: monitoring
ms.topic: conceptual
ms.custom: 
  - references_regions
  - sqldbrb=2
monikerRange: "=azuresql||=azuresql-db||=azuresql-mi"
---
# Monitor and performance tuning in Azure SQL Database and Azure SQL Managed Instance
[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

To monitor the performance of a database in Azure SQL Database and Azure SQL Managed Instance, start by monitoring the CPU and IO resources used by your workload relative to the level of database performance you chose in selecting a particular service tier and performance level. To accomplish this, Azure SQL Database and Azure SQL Managed Instance emit resource metrics that can be viewed in the Azure portal or by using one of these SQL Server management tools:

 - [Azure Data Studio](/azure-data-studio/what-is-azure-data-studio), based on [Visual Studio Code](https://code.visualstudio.com/).
 - [SQL Server Management Studio](/sql/ssms/sql-server-management-studio-ssms) (SSMS), based on [Microsoft Visual Studio](https://visualstudio.microsoft.com/downloads/).

The following table presents a summary of monitoring capabilities in Azure SQL Database and Azure SQL Managed Studio.

| Monitoring solution | SQL Database | SQL Managed Instance | Estate monitoring | Low latency | Comprehensive data |
|:--|:--|:--|:--|:--|:--|
| [Metrics and alerts](monitoring-metrics-alerts.md) | **Yes** | **Yes** | **Yes** | **Yes** | No |
| [Query Performance Insight](#generate-intelligent-assessments-of-performance-issues) | **Yes** | No | **Yes** | No | No |
| [Monitor using DMVs](monitoring-with-dmvs.md) | **Yes** | **Yes** | No | **Yes** | **Yes** |
| [Monitor using query store](/sql/relational-databases/performance/monitoring-performance-by-using-the-query-store)  | **Yes** | **Yes** | No | **Yes** | No |
| [Database watcher (preview)](../database-watcher-overview.md) | **Yes** | **Yes** | **Yes** | **Yes** | **Yes** |

## Database watcher (preview)

Database watcher collects in-depth workload monitoring data to give you a detailed view of database performance, configuration, and health. Dashboards in the Azure portal provide a single-pane-of-glass view of your Azure SQL estate and a detailed view of each monitored resource. Data is collected into a central data store in your Azure subscription. You can query, analyze, export, visualize collected data and integrate it with downstream systems.

For more information about database watcher, see the following articles:

- [Monitor Azure SQL workloads with database watcher (preview)](../database-watcher-overview.md)
- [Quickstart: Create a database watcher to monitor Azure SQL (preview)](../database-watcher-quickstart.md)
- [Create and configure a database watcher (preview)](../database-watcher-manage.md)
- [Database watcher data collection and datasets (preview)](../database-watcher-data.md)
- [Analyze database watcher monitoring data (preview)](../database-watcher-analyze.md)
- [Database watcher FAQ](../database-watcher-faq.yml)

## Database advisors in the Azure portal

Azure SQL Database provides a number of Database Advisors to provide intelligent performance tuning recommendations and automatic tuning options to improve performance. 

Additionally, the [Query Performance Insight](query-performance-insight-use.md) page shows you details about the queries responsible for the most CPU and IO usage for single and pooled databases. 

 - Query Performance Insight is available in the Azure portal in the Overview pane of your Azure SQL Database under "Intelligent Performance". Use the automatically collected information to identify queries and begin optimizing your workload performance. 
 - You can also configure [automatic tuning](automatic-tuning-overview.md) to implement these recommendations automatically, such as forcing a query execution plan to prevent regression, or creating and dropping nonclustered indexes based on workload patterns. Automatic tuning also is available in the Azure portal in the Overview pane of your Azure SQL Database under "Intelligent Performance".

Azure SQL Database and Azure SQL Managed Instance provide advanced monitoring and tuning capabilities to assist you in troubleshooting and maximizing the performance of your databases and solutions. You can choose to configure the [streaming export](metrics-diagnostic-telemetry-logging-streaming-export-configure.md#diagnostic-telemetry-for-export) of database resource logs and metrics to one of several destinations for consumption and analysis.

Outside of the Azure portal, the database engine has its own monitoring and diagnostic capabilities that Azure SQL Database and SQL Managed Instance use, such as [query store](/sql/relational-databases/performance/monitoring-performance-by-using-the-query-store) and [dynamic management views (DMVs)](/sql/relational-databases/system-dynamic-management-views/system-dynamic-management-views). See [Monitoring using DMVs](monitoring-with-dmvs.md) for scripts to monitor for a variety of performance issues in Azure SQL Database and Azure SQL Managed Instance.

### <a id="monitoring-and-diagnostic-telemetry"></a> Monitor and diagnostic telemetry

The following diagram details all the database engine, platform metrics, resource logs, and Azure activity logs generated by Azure SQL products, how they are processed, and how they can be surfaced for analysis.

:::image type="content" source="media/monitor-tune-overview/azure-sql-insights-horizontal-analytics-full-diagram.svg" alt-text="Diagram showing complete logging and diagnostic information paths for Azure SQL products.":::

## Monitor and tune Azure SQL in the Azure portal

In the Azure portal, Azure SQL Database and Azure SQL Managed Instance provide monitoring of resource metrics. Azure SQL Database provides database advisors, and Query Performance Insight provides query tuning recommendations and query performance analysis. In the Azure portal, you can enable automatic tuning for [logical SQL servers](logical-servers.md) and their single and pooled databases.

> [!NOTE]
> Databases with extremely low usage might show in the portal with less than actual usage. Due to the way telemetry is emitted when converting a double value to the nearest integer certain usage amounts less than 0.5 will be rounded to 0 which causes a loss in granularity of the emitted telemetry. For details, see [Low database and elastic pool metrics rounding to zero](#low-database-and-elastic-pool-metrics-rounding-to-zero).

### Azure SQL Database and Azure SQL Managed Instance resource monitoring

You can quickly monitor a variety of resource metrics in the Azure portal in the **Metrics** view. These metrics enable you to see if a database is approaching the limits of CPU, memory, IO, or storage resources. High DTU, CPU or IO utilization might indicate that your workload needs more resources. It might also indicate that queries need to be optimized. See [Microsoft.Sql/servers/databases](/azure/azure-monitor/essentials/metrics-supported#microsoftsqlserversdatabases), [Microsoft.Sql/servers/elasticPools](/azure/azure-monitor/essentials/metrics-supported#microsoftsqlserverselasticpools) and [Microsoft.Sql/managedInstances](/azure/azure-monitor/essentials/metrics-supported#microsoftsqlmanagedinstances) for supported metrics in Azure SQL Database and Azure SQL Managed Instance.

  :::image type="content" source="media/monitor-tune-overview/resource-metrics.png" alt-text="Screenshot from the Azure portal of resource metrics for a SQL database." lightbox="media/monitor-tune-overview/resource-metrics.png":::

For a set of recommended alert rules in Azure SQL Database, see [Monitor Azure SQL Database with Azure Monitor metrics and alerts](monitoring-metrics-alerts.md).

> [!NOTE]
> Storage-related metrics in Azure portal, such as **Data space used**, are reported in power-of-two values, but using power-of-ten units. For example, 1 MB of storage space refers to 1,048,576 bytes, not 1,000,000 bytes. The newer units used for power-of-two values are KiB, MiB, GiB, etc. For compatibility and consistency with the historically established usage within the database engine, Azure SQL storage metrics use the older units such as KB, MB, GB, etc.

### Database advisors in Azure SQL Database

Azure SQL Database includes [database advisors](database-advisor-implement-performance-recommendations.md) that provide performance tuning recommendations for single and pooled databases. These recommendations are available in the Azure portal as well as by using [PowerShell](/powershell/module/az.sql/get-azsqldatabaseadvisor). You can also enable [automatic tuning](automatic-tuning-overview.md) so that Azure SQL Database can automatically implement these tuning recommendations.

### Query Performance Insight in Azure SQL Database

[Query Performance Insight](query-performance-insight-use.md) shows the performance in the Azure portal of top consuming and longest running queries for single and pooled databases.

### Low database and elastic pool metrics rounding to zero

Starting in September 2020, databases with extremely low usage might show in the portal with less than actual usage. Due to the way telemetry is emitted when converting a double value to the nearest integer certain usage amounts less than 0.5 will be rounded to 0, which causes a loss in granularity of the emitted telemetry.

For example: Consider a 1-minute window with the following four data points: 0.1, 0.1, 0.1, 0.1, these low values are rounded down to 0, 0, 0, 0 and present an average of 0. If any of the data points are greater than 0.5, for example: 0.1, 0.1, 0.9, 0.1, they are rounded to 0, 0, 1, 0 and show an avg of 0.25.

## Enable the streaming export of metrics and resource logs

Diagnostic settings is a feature that contains Resource Log categories (formerly known as Diagnostic Logs). You can enable and configure the [streaming export of diagnostic telemetry](metrics-diagnostic-telemetry-logging-streaming-export-configure.md#diagnostic-telemetry-for-export) to one of several destinations, including Log Analytics, Event Hubs, and Azure Storage.

> [!NOTE]
> The resource log category for [Intelligent Insights](intelligent-insights-overview.md) is called `SQLInsights`. This is unrelated to the [SQL Insights (preview)](sql-insights-overview.md) monitoring solution.

You configure diagnostic settings to stream categories of metrics and resource logs for single databases, pooled databases, elastic pools, SQL managed instances, and instance databases to one of the following Azure services.

### Log Analytics workspace in Azure Monitor

You can stream metrics and resource logs to a [Log Analytics workspace in Azure Monitor](/azure/azure-monitor/essentials/resource-logs#send-to-log-analytics-workspace). Data streamed here can be consumed by [SQL Analytics (preview)](/azure/azure-monitor/insights/azure-sql), which is a cloud only monitoring solution that provides intelligent monitoring of your databases that includes performance reports, alerts, and mitigation recommendations. Data streamed to a Log Analytics workspace can be analyzed with other monitoring data collected and also enables you to use other Azure Monitor features such as alerts and visualizations.

> [!NOTE]
> Azure SQL Analytics (preview) is an integration with Azure Monitor, where many monitoring solutions are no longer in active development.

### Azure Event Hubs

You can stream metrics and resource logs to [Azure Event Hubs](/azure/azure-monitor/essentials/resource-logs#send-to-azure-event-hubs). Streaming diagnostic telemetry to event hubs to provide the following functionality:

- **Stream logs to third-party logging and telemetry systems**

  Stream all of your metrics and resource logs to a single event hub to pipe log data to a third-party SIEM or log analytics tool.
- **Build a custom telemetry and logging platform**

  The highly scalable publish-subscribe nature of event hubs allows you to flexibly ingest metrics and resource logs into a custom telemetry platform. For more information, see [Azure Event Hubs](/azure/event-hubs/event-hubs-about).
- **View service health by streaming data to Power BI**

  Use Event Hubs, Stream Analytics, and Power BI to transform your diagnostics data into near real-time insights on your Azure services. See [Stream Analytics and Power BI: A real-time analytics dashboard for streaming data](/azure/stream-analytics/stream-analytics-power-bi-dashboard) for details on this solution.

### Azure Storage

Stream metrics and resource logs to [Azure Storage](/azure/azure-monitor/essentials/resource-logs#send-to-azure-storage). Use Azure storage to archive vast amounts of diagnostic telemetry for a fraction of the cost of the previous two streaming options.

## Use Extended Events

Additionally, you can use [Extended Events](/sql/relational-databases/extended-events/extended-events) for advanced monitoring and troubleshooting in SQL Server, Azure SQL Database, and Azure SQL Managed Instance. Extended Events is a "tracing" tool and event architecture, superior to SQL Trace, that enables users to collect as much or as little data as is necessary to troubleshoot or identify a performance problem, while mitigating impact to ongoing application performance. Extended Events replace deprecated SQL Trace and SQL Server Profiler features. For information about using extended events in Azure SQL Database, see [Extended events in Azure SQL Database](xevent-db-diff-from-svr.md). In Azure SQL Database and SQL Managed Instance, use an [Event File target hosted in Azure Blob Storage](xevent-code-event-file.md).

## Related content

- For more information about intelligent performance recommendations for single and pooled databases, see [Database advisor performance recommendations](database-advisor-implement-performance-recommendations.md).
- [Monitor Azure SQL workloads with database watcher (preview)](../database-watcher-overview.md)
- [Monitor Azure SQL Database with metrics and alerts](monitoring-metrics-alerts.md)
- [Monitor Azure SQL Database](monitoring-sql-database-azure-monitor.md)
- [Monitor Azure SQL Managed Instance with Azure Monitor](../managed-instance/monitoring-sql-managed-instance-azure-monitor.md)
