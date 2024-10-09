---
title: Monitor Azure SQL Database
description: Start here to learn how to monitor Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma, dfurman
ms.date: 08/23/2024
ms.service: azure-sql-database
ms.subservice: monitoring
ms.topic: conceptual
ms.custom:
  - horz-monitor
---
# Monitor Azure SQL Database
[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

[!INCLUDE [horz-monitor-intro](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-intro.md)]

The SQL Server database engine has its own monitoring and diagnostic capabilities that Azure SQL Database uses, such as Query Store and dynamic management views (DMVs). For more information, see [Monitor performance by using the Query Store](/sql/relational-databases/performance/monitoring-performance-by-using-the-query-store) and [Monitor Azure SQL Database performance using dynamic management views](monitoring-with-dmvs.md).

For a detailed discussion of all monitoring and performance aspects of Azure SQL Database and Azure SQL Managed Instance, see [Monitor and performance tuning in Azure SQL Database and Azure SQL Managed Instance](monitor-tune-overview.md).

> [!IMPORTANT]
> For a set of recommended alert rules, see [Monitor Azure SQL Database with Azure Monitor metrics and alerts](monitoring-metrics-alerts.md).

## Database watcher (preview)

Database watcher collects in-depth workload monitoring data to give you a detailed view of database performance, configuration, and health. Dashboards in the Azure portal provide a single-pane-of-glass view of your Azure SQL estate and a detailed view of each monitored resource. Data is collected into a central data store in your Azure subscription. You can query, analyze, export, visualize collected data and integrate it with downstream systems.

For more information about database watcher, see the following articles:

- [Monitor Azure SQL workloads with database watcher (preview)](../database-watcher-overview.md)
- [Quickstart: Create a database watcher to monitor Azure SQL (preview)](../database-watcher-quickstart.md)
- [Create and configure a database watcher (preview)](../database-watcher-manage.md)
- [Database watcher data collection and datasets (preview)](../database-watcher-data.md)
- [Analyze database watcher monitoring data (preview)](../database-watcher-analyze.md)
- [Database watcher FAQ](../database-watcher-faq.yml)

## Query performance insight

Query performance insight uses the SQL Server Query Store to provide intelligent query analysis and insight on query plan choice and performance for single and pooled databases. For more information, see [Query Performance Insight for Azure SQL Database](query-performance-insight-use.md).

[!INCLUDE [horz-monitor-resource-types](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-resource-types.md)]
For more information about the resource types for SQL Database, see [SQL Database monitoring data reference](monitoring-sql-database-azure-monitor-reference.md).

[!INCLUDE [horz-monitor-data-storage](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-data-storage.md)]

[!INCLUDE [horz-monitor-platform-metrics](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-platform-metrics.md)]

## Azure SQL Database metrics

You can use metrics to monitor database and elastic pool resource consumption and health. For example, you can:

- Right-size the database or elastic pool to your application workload
- Detect a gradual increase in resource consumption, and proactively scale up the database or elastic pool
- Detect and troubleshoot a performance problem

For a list and descriptions of commonly used metrics in Azure SQL Database, see [Azure SQL Database metrics](monitoring-metrics-alerts.md#use-metrics-to-monitor-databases-and-elastic-pools).

For tables of all available metrics for SQL Database, see [SQL Database monitoring data reference](monitoring-sql-database-azure-monitor-reference.md#metrics).

[!INCLUDE [horz-monitor-resource-logs](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-resource-logs.md)]

## Azure SQL Database logs

Auditing for Azure SQL Database tracks database events and writes them to an audit log in your Azure storage account, Log Analytics workspace, or Event Hubs. For more information, see [Auditing for Azure SQL Database](auditing-overview.md).

For more information on the resource logs and diagnostics available for Azure SQL Database, see [Configure streaming export of diagnostic telemetry](metrics-diagnostic-telemetry-logging-streaming-export-configure.md).

For the available resource log categories, their associated Log Analytics tables, and the log schemas for SQL Database, see [SQL Database monitoring data reference](monitoring-sql-database-azure-monitor-reference.md#resource-logs).

[!INCLUDE [horz-monitor-activity-log](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-activity-log.md)]

<a id="analyzing-metrics"></a>
[!INCLUDE [horz-monitor-analyze-data](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-analyze-data.md)]

[!INCLUDE [horz-monitor-external-tools](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-external-tools.md)]

[!INCLUDE [horz-monitor-kusto-queries](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-kusto-queries.md)]

> [!NOTE]
> Occasionally, it might take up to 15 minutes between when an event is emitted and when it [appears in a Log Analytics workspace](/azure/azure-monitor/logs/data-ingestion-time).

Use the following queries to monitor your database. You might see different options available depending on your purchasing model.

Example A: **Log_write_percent** from the past hour

```Kusto
AzureMetrics
| where ResourceProvider == "MICROSOFT.SQL"
| where TimeGenerated >= ago(60min)
| where MetricName in ('log_write_percent')
| parse _ResourceId with * "/microsoft.sql/servers/" Resource
| summarize Log_Maximum_last60mins = max(Maximum), Log_Minimum_last60mins = min(Minimum), Log_Average_last60mins = avg(Average) by Resource, MetricName
```

Example B: **SQL Server wait types** from the past 15 minutes

```Kusto
AzureDiagnostics
| where ResourceProvider == "MICROSOFT.SQL"
| where TimeGenerated >= ago(15min)
| parse _ResourceId with * "/microsoft.sql/servers/" LogicalServerName "/databases/" DatabaseName
| summarize Total_count_15mins = sum(delta_waiting_tasks_count_d) by LogicalServerName, DatabaseName, wait_type_s
```

Example C: **SQL Server deadlocks** from the past 60 minutes

```Kusto
AzureMetrics
| where ResourceProvider == "MICROSOFT.SQL"
| where TimeGenerated >= ago(60min)
| where MetricName in ('deadlock')
| parse _ResourceId with * "/microsoft.sql/servers/" Resource
| summarize Deadlock_max_60Mins = max(Maximum) by Resource, MetricName
```

Example D: **Avg CPU usage** from the past hour

```Kusto
AzureMetrics
| where ResourceProvider == "MICROSOFT.SQL"
| where TimeGenerated >= ago(60min)
| where MetricName in ('cpu_percent')
| parse _ResourceId with * "/microsoft.sql/servers/" Resource
| summarize CPU_Maximum_last60mins = max(Maximum), CPU_Minimum_last60mins = min(Minimum), CPU_Average_last60mins = avg(Average) by Resource, MetricName
```

[!INCLUDE [horz-monitor-alerts](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-alerts.md)]

### SQL Database alert rules

The following table lists common and recommended alert rules for SQL Database. You might see different options available depending on your purchasing model.

| Signal name | Operator | Aggregation type  | Threshold value | Description |
|:---|:---|:---|:---|:---|
| DTU Percentage | Greater than | Average | 80 | Whenever the average DTU percentage is greater than 80% |
| Log IO percentage | Greater than | Average | 80 | Whenever the average log io percentage is greater than 80% | 
| Deadlocks\* | Greater than | Count | 1 | Whenever the count of deadlocks is greater than 1. |
| CPU percentage | Greater than | Average | 80 | Whenever the average cpu percentage is greater than 80% | 

\* Alerting on deadlocks might be unnecessary and noisy in some applications where deadlocks are expected and properly handled.

> [!TIP]
> For recommended alert rules and typical alert rule configurations for SQL Database, see [Monitor Azure SQL Database with Azure Monitor metrics and alerts](monitoring-metrics-alerts.md?view=azuresql-db&preserve-view=true#recommended-alert-rules).

[!INCLUDE [horz-monitor-advisor-recommendations](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-advisor-recommendations.md)]

## Related content

- See [SQL Database monitoring data reference](monitoring-sql-database-azure-monitor-reference.md) for a reference of the metrics, logs, and other important values created for SQL Database.
- See [Monitoring Azure resources with Azure Monitor](/azure/azure-monitor/essentials/monitor-azure-resource) for general details on monitoring Azure resources.
- [Monitor Azure SQL workloads with database watcher (preview)](../database-watcher-overview.md) 
- Review [Azure Monitor metrics and alerts](monitoring-metrics-alerts.md) including [recommended alert rules](monitoring-metrics-alerts.md#recommended-alert-rules) for SQL Database.
- Learn how to [Monitor Azure SQL Managed Instance with Azure Monitor](../managed-instance/monitoring-sql-managed-instance-azure-monitor.md).
