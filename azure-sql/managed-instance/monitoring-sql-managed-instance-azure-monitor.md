---
title: Monitor Azure SQL Managed Instance
description: Start here to learn how to monitor Azure SQL Managed Instance.
ms.date: 08/16/2024
ms.custom: horz-monitor
ms.topic: conceptual
author: MashaMSFT
ms.author: mathoma
ms.reviewer: wiassaf
ms.service: azure-sql-managed-instance
ms.subservice: monitoring
---

# Monitor Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

[!INCLUDE [horz-monitor-intro](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-intro.md)]

In addition to the features in this article, the SQL Server database engine has its own monitoring and diagnostic capabilities that Azure SQL Managed Instance uses, such as query store and dynamic management views (DMVs). For more information, see the following articles:

- [Monitor performance by using the Query Store](/sql/relational-databases/performance/monitoring-performance-by-using-the-query-store)
- [Monitor Azure SQL Managed Instance performance using dynamic management views](monitoring-with-dmvs.md)

For a detailed discussion of all monitoring and performance aspects of Azure SQL Managed Instance, see [Monitor and performance tuning in Azure SQL Database and Azure SQL Managed Instance](../database/monitor-tune-overview.md).
[!INCLUDE [horz-monitor-insights](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-insights.md)]

## Database watcher (preview)

Database watcher collects in-depth workload monitoring data to give you a detailed view of database performance, configuration, and health. Dashboards in the Azure portal provide a single-pane-of-glass view of your Azure SQL estate and a detailed view of each monitored resource. Data is collected into a central data store in your Azure subscription. You can query, analyze, export, visualize collected data and integrate it with downstream systems.

For more information about database watcher, see the following articles:

- [Monitor Azure SQL workloads with database watcher (preview)](../database-watcher-overview.md)
- [Quickstart: Create a database watcher to monitor Azure SQL (preview)](../database-watcher-quickstart.md)
- [Create and configure a database watcher (preview)](../database-watcher-manage.md)
- [Database watcher data collection and datasets (preview)](../database-watcher-data.md)
- [Analyze database watcher monitoring data (preview)](../database-watcher-analyze.md)
- [Database watcher FAQ](../database-watcher-faq.yml)

[!INCLUDE [horz-monitor-resource-types](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-resource-types.md)]
For more information about the resource types for SQL Managed Instance, see [SQL Managed Instance monitoring data reference](monitoring-sql-managed-instance-azure-monitor-reference.md).

[!INCLUDE [horz-monitor-data-storage](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-data-storage.md)]

[!INCLUDE [horz-monitor-platform-metrics](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-platform-metrics.md)]
For a list of available metrics for SQL Managed Instance, see [SQL Managed Instance monitoring data reference](monitoring-sql-managed-instance-azure-monitor-reference.md#metrics).

[!INCLUDE [horz-monitor-resource-logs](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-resource-logs.md)]

## Azure SQL Managed Instance logs

Auditing for Azure SQL Managed Instance tracks database events and writes them to an audit log in your Azure storage account. For more information, see [Get started with SQL Managed Instance auditing](auditing-configure.md).
For more information on the resource logs and diagnostics available for Azure SQL Managed Instance, see [Configure streaming export of diagnostic telemetry](../database/metrics-diagnostic-telemetry-logging-streaming-export-configure.md).

For the available resource log categories, their associated Log Analytics tables, and the logs schemas for SQL Managed Instance, see [SQL Managed Instance monitoring data reference](monitoring-sql-managed-instance-azure-monitor-reference.md#resource-logs).

[!INCLUDE [horz-monitor-activity-log](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-activity-log.md)]

<a id="analyzing-metrics"></a>
[!INCLUDE [horz-monitor-analyze-data](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-analyze-data.md)]

[!INCLUDE [horz-monitor-external-tools](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-external-tools.md)]

[!INCLUDE [horz-monitor-kusto-queries](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-kusto-queries.md)]

Use the following sample queries to help you monitor your Azure SQL Managed Instance.

**Example A:** Display all managed instances with `avg_cpu` utilization over 95%. 

```Kusto
let cpu_percentage_threshold = 95;
let time_threshold = ago(1h);
AzureDiagnostics
| where Category == "ResourceUsageStats" and TimeGenerated > time_threshold
| summarize avg_cpu = max(todouble(avg_cpu_percent_s)) by _ResourceId
| where avg_cpu > cpu_percentage_threshold
```

**Example B:** Display all managed instances with storage utilization over 90%, dividing `storage_space_used_mb_s` by `reserved_storage_mb_s`.

```Kusto
let storage_percentage_threshold = 90;
AzureDiagnostics
| where Category =="ResourceUsageStats"
| summarize (TimeGenerated, calculated_storage_percentage) = arg_max(TimeGenerated, todouble(storage_space_used_mb_s) *100 / todouble (reserved_storage_mb_s))
   by _ResourceId
| where calculated_storage_percentage > storage_percentage_threshold
```

[!INCLUDE [horz-monitor-alerts](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-alerts.md)]

[!INCLUDE [horz-monitor-insights-alerts](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-insights-alerts.md)]

### SQL Managed Instance alert rules
The following table lists common and recommended alert rules for Azure SQL Managed Instance. You may see different options available depending on your purchasing model.

| Signal name | Operator | Aggregation type  | Threshold value | Description |
|:---|:---|:---|:---|:---|
| Average CPU percentage | Greater than | Average | 80 | Whenever the average CPU utilization percentage is greater than 80% | 
| Resource Health | Current Resource Status | NA | Degraded or Unavailable | Detect resources outages, whether they be Azure initiated or user initiated |

[!INCLUDE [horz-monitor-advisor-recommendations](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-advisor-recommendations.md)]

## Related content

- See [SQL Managed Instance monitoring data reference](monitoring-sql-managed-instance-azure-monitor-reference.md) for a reference of the metrics, logs, and other important values created for SQL Managed Instance.
- See [Monitoring Azure resources with Azure Monitor](/azure/azure-monitor/essentials/monitor-azure-resource) for general details on monitoring Azure resources.
