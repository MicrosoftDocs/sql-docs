---
title: Monitor Azure SQL Managed Instance
description: Start here to learn how to monitor Azure SQL Managed Instance.
ms.date: 02/12/2024
ms.custom: horz-monitor
ms.topic: conceptual
author: mathoma
ms.author: MashaMSFT
ms.service: sql-managed-instance
---

<!-- 
IMPORTANT 
To make this template easier to use, first:
1. Search and replace SQL Managed Instance with the official name of your service.
2. Search and replace sql-database with the service name to use in GitHub filenames.-->

<!-- VERSION 3.0 2024_01_07
For background about this template, see https://review.learn.microsoft.com/en-us/help/contribute/contribute-monitoring?branch=main -->

<!-- Most services can use the following sections unchanged. The sections use #included text you don't have to maintain, which changes when Azure Monitor functionality changes. Add info into the designated service-specific places if necessary. Remove #includes or template content that aren't relevant to your service.

At a minimum your service should have the following two articles:

1. The primary monitoring article (based on this template)
   - Title: "Monitor SQL Managed Instance"
   - TOC title: "Monitor"
   - Filename: "monitor-sql-database.md"

2. A reference article that lists all the metrics and logs for your service (based on the template data-reference-template.md).
   - Title: "SQL Managed Instance monitoring data reference"
   - TOC title: "Monitoring data reference"
   - Filename: "monitoring-sql-managed-instance-azure-monitor-reference.md".
-->

# Monitor Azure SQL Managed Instance

<!-- Intro. Required. -->
[!INCLUDE [horz-monitor-intro](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-intro.md)]

The SQL Server database engine has its own monitoring and diagnostic capabilities that Azure SQL Managed Instance uses, such as query store and dynamic management views (DMVs). For more information, see [Monitor performance by using the Query Store](/sql/relational-databases/performance/monitoring-performance-by-using-the-query-store) and [Monitor Azure SQL Managed Instance performance using dynamic management views](monitoring-with-dmvs.md).

For a detailed discussion of all monitoring and performance aspects of Azure SQL Managed Instance, see [Monitor and performance tuning in Azure SQL Database and Azure SQL Managed Instance](../database/monitor-tune-overview.md).

<!-- ## Insights. Optional section. If your service has insights, add the following include and information. -->
[!INCLUDE [horz-monitor-insights](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-insights.md)]

### Azure Monitor SQL Insights (preview)

You can configure Azure Monitor SQL Insights for SQL-specific metrics for Azure SQL Managed Instance, Azure SQL Database, and SQL Server on Azure VMs. For more information about Azure Monitor SQL Insights for all products in the [Azure SQL family](index.yml), see [Monitor your SQL deployments with SQL Insights (preview)](/azure/azure-monitor/insights/sql-insights-overview).

For more information on how to use SQL Insights, see the following articles:

- [Monitor your SQL deployments with SQL Insights (preview)](../database/sql-insights-overview.md)
- [How to enable SQL Insights](../database/sql-insights-enable.md)
- [Create alerts with SQL Insights](../database/sql-insights-alerts.md)
- [Troubleshoot SQL Insights](../database/sql-insights-troubleshoot.md)

### Intelligent Insights

Intelligent Insights for Azure SQL Database and Azure SQL Managed Instance is different from Azure Monitor SQL Insights. Intelligent Insights uses artificial intelligence to continuously monitor database usage and detect disruptive events that cause poor performance. Intelligent Insights generates a resource log called SQLInsights that provides an intelligent assessment, root cause analysis, and performance improvement recommendations.

For more information, see [Intelligent Insights using AI to monitor and troubleshoot database performance (preview)](../database/intelligent-insights-overview.md) and [Use the Intelligent Insights performance diagnostics log](../database/intelligent-insights-use-diagnostics-log.md).

> [!NOTE]
> Azure SQL Analytics is an integration with Azure Monitor that is no longer in active development. For more information, see [Monitor Azure SQL using Azure SQL Analytics (preview)](/previous-versions/azure/azure-monitor/insights/azure-sql).

<!-- ## Resource types. Required section. -->
[!INCLUDE [horz-monitor-resource-types](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-resource-types.md)]
For more information about the resource types for SQL Managed Instance, see [SQL Managed Instance monitoring data reference](monitoring-sql-managed-instance-azure-monitor-reference.md).

<!-- ## Data storage. Required section. Optionally, add service-specific information about storing your monitoring data after the include. -->
[!INCLUDE [horz-monitor-data-storage](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-data-storage.md)]
<!-- Add service-specific information about storing monitoring data here, if applicable. For example, SQL Server stores other monitoring data in its own databases. -->

<!-- METRICS SECTION START ------------------------------------->

<!-- ## Platform metrics. Required section.
  - If your service doesn't collect platform metrics, use the following include: [!INCLUDE [horz-monitor-no-platform-metrics](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-no-platform-metrics.md)]
  - If your service collects platform metrics, add the following include, statement, and service-specific information as appropriate. -->
[!INCLUDE [horz-monitor-platform-metrics](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-platform-metrics.md)]
For a list of available metrics for SQL Managed Instance, see [SQL Managed Instance monitoring data reference](monitoring-sql-managed-instance-azure-monitor-reference.md#metrics).
<!-- Platform metrics service-specific information. Add service-specific information about your platform metrics here.-->

<!-- ## Prometheus/container metrics. Optional. If your service uses containers/Prometheus metrics, add the following include and information. 
[!INCLUDE [horz-monitor-container-metrics](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-container-metrics.md)]
Add service-specific information about your container/Prometheus metrics here.-->

<!-- ## System metrics. Optional. If your service uses system-imported metrics, add the following include and information. 
[!INCLUDE [horz-monitor-system-metrics](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-system-metrics.md)]
Add service-specific information about your system-imported metrics here.-->

<!-- ## Custom metrics. Optional. If your service uses custom imported metrics, add the following include and information.
[!INCLUDE [horz-monitor-custom-metrics](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-custom-metrics.md)]
Custom imported service-specific information. Add service-specific information about your custom imported metrics here.-->

<!-- ## Non-Azure Monitor metrics. Optional. If your service uses any non-Azure Monitor based metrics, add the following include and information.
[!INCLUDE [horz-monitor-custom-metrics](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-non-monitor-metrics.md)]
Non-Monitor metrics service-specific information. Add service-specific information about your non-Azure Monitor metrics here.-->

<!-- METRICS SECTION END ------------------------------------->

<!-- LOGS SECTION START -------------------------------------->

<!-- ## Resource logs. Required section.
  - If your service doesn't collect resource logs, use the following include [!INCLUDE [horz-monitor-no-resource-logs](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-no-resource-logs.md)]
  - If your service collects resource logs, add the following include, statement, and service-specific information as appropriate. -->
[!INCLUDE [horz-monitor-resource-logs](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-resource-logs.md)]

### Azure SQL logs

Auditing for Azure SQL Managed Instance tracks database events and writes them to an audit log in your Azure storage account. For more information, see [Get started with SQL Managed Instance auditing](auditing-configure.md).

For more information on the resource logs and diagnostics available for Azure SQL Managed Instance, see [Configure streaming export of diagnostic telemetry](../database/metrics-diagnostic-telemetry-logging-streaming-export-configure.md).

For the available resource log categories, their associated Log Analytics tables, and the logs schemas for SQL Managed Instance, see [SQL Managed Instance monitoring data reference](monitoring-sql-managed-instance-azure-monitor-reference.md#resource-logs).

<!-- Resource logs service-specific information. Add service-specific information about your resource logs here.
NOTE: Azure Monitor already has general information on how to configure and route resource logs. See https://learn.microsoft.com/azure/azure-monitor/platform/diagnostic-settings. Ideally, don't repeat that information here. You can provide a single screenshot of the diagnostic settings portal experience if you want. -->

<!-- ## Activity log. Required section. Optionally, add service-specific information about your activity log after the include. -->
[!INCLUDE [horz-monitor-activity-log](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-activity-log.md)]
<!-- Activity log service-specific information. Add service-specific information about your activity log here. -->

<!-- ## Imported logs. Optional section. If your service uses imported logs, add the following include and information. 
[!INCLUDE [horz-monitor-imported-logs](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-imported-logs.md)]
Add service-specific information about your imported logs here. -->

<!-- ## Other logs. Optional section.
If your service has other logs that aren't resource logs or in the activity log, add information that states what they are and what they cover here. You can describe how to route them in a later section. -->

<!-- LOGS SECTION END ------------------------------------->

<!-- ANALYSIS SECTION START -------------------------------------->
<a id="analyzing-metrics"></a>
<!-- ## Analyze data. Required section. -->
[!INCLUDE [horz-monitor-analyze-data](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-analyze-data.md)]

<!-- ### External tools. Required section. -->
[!INCLUDE [horz-monitor-external-tools](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-external-tools.md)]

<!-- ### Sample Kusto queries. Required section. If you have sample Kusto queries for your service, add them after the include. -->
[!INCLUDE [horz-monitor-kusto-queries](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-kusto-queries.md)]
<!-- Add sample Kusto queries for your service here. -->
Use the following sample queries to help you monitor your Azure SQL Managed Instance:

Example A: Display all managed instances with `avg_cpu` utilization over 95%. 

```Kusto
let cpu_percentage_threshold = 95;
let time_threshold = ago(1h);
AzureDiagnostics
| where Category == "ResourceUsageStats" and TimeGenerated > time_threshold
| summarize avg_cpu = max(todouble(avg_cpu_percent_s)) by _ResourceId
| where avg_cpu > cpu_percentage_threshold
```

Example B: Display all managed instances with storage utilization over 90%, dividing `storage_space_used_mb_s` by `reserved_storage_mb_s`.

```Kusto
let storage_percentage_threshold = 90;
AzureDiagnostics
| where Category =="ResourceUsageStats"
| summarize (TimeGenerated, calculated_storage_percentage) = arg_max(TimeGenerated, todouble(storage_space_used_mb_s) *100 / todouble (reserved_storage_mb_s))
   by _ResourceId
| where calculated_storage_percentage > storage_percentage_threshold
```

<!-- ### SQL Managed Instance service-specific analytics. Optional section.
Add short information or links to specific articles that outline how to analyze data for your service. -->

<!-- ANALYSIS SECTION END ------------------------------------->

<!-- ALERTS SECTION START -------------------------------------->

<!-- ## Alerts. Required section. -->
[!INCLUDE [horz-monitor-alerts](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-alerts.md)]

<!-- ONLY if your service (Azure VMs, AKS, or Log Analytics workspaces) offer out-of-the-box recommended alerts, add the following include. 
[!INCLUDE [horz-monitor-insights-alerts](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-recommended-alert-rules.md)]

<!-- ONLY if applications run on your service that work with Application Insights, add the following include. -->
[!INCLUDE [horz-monitor-insights-alerts](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-insights-alerts.md)]

<!-- ### SQL Managed Instance alert rules. Required section.
**MUST HAVE** service-specific alert rules. Include useful alerts on metrics, logs, log conditions, or activity log.
Fill in the following table with metric and log alerts that would be valuable for your service. Change the format as necessary for readability. You can instead link to an article that discusses your common alerts in detail.
Ask your PMs if you don't know. This information is the BIGGEST request we get in Azure Monitor, so don't avoid it long term. People don't know what to monitor for best results. Be prescriptive. -->

### SQL Managed Instance alert rules
The following table lists common and recommended alert rules for Azure SQL Managed Instance. You may see different options available depending on your purchasing model.

| Signal name | Operator | Aggregation type  | Threshold value | Description |
|:---|:---|:---|:---|:---|
| Average CPU percentage | Greater than | Average | 80 | Whenever the average CPU utilization percentage is greater than 80% | 
| Resource Health | Current Resource Status | NA | Degraded or Unavailable | Detect resources outages, whether they be Azure initiated or user initiated |

<!-- ### Advisor recommendations. Required section. -->
[!INCLUDE [horz-monitor-advisor-recommendations](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-advisor-recommendations.md)]
<!-- Add any service-specific advisor recommendations or screenshots here. -->

<!-- ALERTS SECTION END -------------------------------------->

## Related content
<!-- You can change the wording and add more links if useful. -->

- Learn how to [Monitor Azure SQL Managed Instance with Azure Monitor](monitoring-sql-managed-instance-azure-monitor.md).
- See [SQL Managed Instance monitoring data reference](monitoring-sql-managed-instance-azure-monitor-reference.md) for a reference of the metrics, logs, and other important values created for SQL Managed Instance.
- See [Monitoring Azure resources with Azure Monitor](/azure/azure-monitor/essentials/monitor-azure-resource) for general details on monitoring Azure resources.
