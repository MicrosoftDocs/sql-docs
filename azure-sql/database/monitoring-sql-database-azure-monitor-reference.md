---
title: Monitoring data reference for Azure SQL Database
description: This article contains important reference material you need when you monitor Azure SQL Database.
ms.date: 03/01/2024
ms.custom: horz-monitor
ms.topic: reference
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.service: sql-database
ms.subservice: monitoring
ms.reviewer: mathoma
---


# Azure SQL Database monitoring data reference

[!INCLUDE [horz-monitor-ref-intro](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-intro.md)]

See [Monitor Azure SQL Database](monitoring-sql-database-azure-monitor.md) for details on the data you can collect for SQL Database and how to use it.

[!INCLUDE [horz-monitor-ref-metrics-intro](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-metrics-intro.md)]

For a list of commonly used metrics for Azure SQL Database, see [Azure SQL Database metrics](monitoring-metrics-alerts.md#use-metrics-to-monitor-databases-and-elastic-pools).

### Supported metrics for Microsoft.Sql/servers/databases
The following table lists the metrics available for the Microsoft.Sql/servers/databases resource type.
[!INCLUDE [horz-monitor-ref-metrics-tableheader](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-metrics-tableheader.md)]
[!INCLUDE [microsoft-sql-servers-databases-metrics-include](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/reference/metrics/microsoft-sql-servers-databases-metrics-include.md)]

### Supported metrics for Microsoft.Sql/servers/elasticpools
The following table lists the metrics available for the Microsoft.Sql/servers/elasticpools resource type.
[!INCLUDE [horz-monitor-ref-metrics-tableheader](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-metrics-tableheader.md)]
[!INCLUDE [microsoft-sql-servers-elasticpools-metrics-include](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/reference/metrics/microsoft-sql-servers-elasticpools-metrics-include.md)]

### Supported metrics for Microsoft.Sql/servers/jobAgents
The following table lists the metrics available for the Microsoft.Sql/servers/jobAgents resource type.
[!INCLUDE [horz-monitor-ref-metrics-tableheader](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-metrics-tableheader.md)]
[!INCLUDE [microsoft-sql-servers-jobagents-metrics-include](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/reference/metrics/microsoft-sql-servers-jobagents-metrics-include.md)]

[!INCLUDE [horz-monitor-ref-metrics-dimensions-intro](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-metrics-dimensions-intro.md)]

[!INCLUDE [horz-monitor-ref-metrics-dimensions](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-metrics-dimensions.md)]

| Metric | Dimension |
| ------ | --------- |
| Failed Connections : System Errors | `Error` <br> `ValidatedDriverNameAndVersion` |
| Failed Connections : User Errors | `Error` <br> `ValidatedDriverNameAndVersion` |
| Successful Connections | `SslProtocol` <br> `ValidatedDriverNameAndVersion` |
| Workload group active queries | `WorkloadGroupName` <br> `IsUserDefined` |
| Workload group query timeouts | `WorkloadGroupName` <br> `IsUserDefined` |
| Workload group allocation by system percent | `WorkloadGroupName` <br> `IsUserDefined` |
| Workload group allocation by cap resource percent | `WorkloadGroupName` <br> `IsUserDefined` |
| Effective cap resource percent | `WorkloadGroupName` <br> `IsUserDefined` |
| Effective min resource percent | `WorkloadGroupName` <br> `IsUserDefined` |
| Workload group queued queries | `WorkloadGroupName` <br> `IsUserDefined` |

[!INCLUDE [horz-monitor-ref-resource-logs](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-resource-logs.md)]

### Supported resource logs for Microsoft.Sql/servers/databases
[!INCLUDE [microsoft-sql-servers-databases-logs-include](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/reference/logs/microsoft-sql-servers-databases-logs-include.md)]

[!INCLUDE [horz-monitor-ref-logs-tables](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-logs-tables.md)]

|Table | Notes |
|-------|-----|
| [AzureActivity](/azure/azure-monitor/reference/tables/azureactivity#columns) | Entries from the Azure Activity log that provides insight into any subscription-level or management group level events that have occurred in Azure. |
| [AzureDiagnostics](/azure/azure-monitor/reference/tables/azurediagnostics#columns) | Azure Diagnostics reveals diagnostic data of specific resources and features for numerous Azure products including SQL databases, SQL elastic pools, and SQL managed instances. For more information, see [Diagnostics metrics](../database/metrics-diagnostic-telemetry-logging-streaming-export-configure.md?tabs=azure-portal#basic-metrics).|
| [AzureMetrics](/azure/azure-monitor/reference/tables/azuremetrics#columns) | Metric data emitted by Azure services that measure their health and performance. Activity from Azure products including SQL databases, SQL elastic pools, and SQL managed instances.|

[!INCLUDE [horz-monitor-ref-activity-log](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-activity-log.md)]

- [Microsoft.Sql resource provider operations](/azure/role-based-access-control/resource-provider-operations#microsoftsql)

## Related content

- See [Monitor SQL Database](monitoring-sql-database-azure-monitor.md) for a description of monitoring Azure SQL Database.
- See [Monitor Azure resources with Azure Monitor](/azure/azure-monitor/essentials/monitor-azure-resource) for details on monitoring Azure resources.
- [Monitor Azure SQL workloads with database watcher (preview)](../database-watcher-overview.md)
- Review [the Azure Monitor metrics and alerts](monitoring-metrics-alerts.md) including [Recommended alert rules](monitoring-metrics-alerts.md#recommended-alert-rules).