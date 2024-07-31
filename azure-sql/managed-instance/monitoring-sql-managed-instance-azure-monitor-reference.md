---
title: Monitoring data reference for Azure SQL Managed Instance
description: This article contains important reference material you need when you monitor Azure SQL Managed Instance.
ms.date: 03/13/2024
ms.custom: horz-monitor
ms.topic: reference
author: MashaMSFT
ms.author: mathoma
ms.reviewer: wiassaf
ms.service: azure-sql-managed-instance
ms.subservice: monitoring
monikerRange: "= azuresql || = azuresql-mi"
---

# Azure SQL Managed Instance monitoring data reference

[!INCLUDE [horz-monitor-ref-intro](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-intro.md)]

See [Monitor Azure SQL Managed Instance](monitoring-sql-managed-instance-azure-monitor.md) for details on the data you can collect for SQL Managed Instance and how to use it.

[!INCLUDE [horz-monitor-ref-metrics-intro](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-metrics-intro.md)]

### Supported metrics for Microsoft.Sql/managedInstances
The following table lists the metrics available for the Microsoft.Sql/managedInstances resource type.
[!INCLUDE [horz-monitor-ref-metrics-tableheader](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-metrics-tableheader.md)]
[!INCLUDE [microsoft-sql-managedinstances-metrics-include](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/reference/metrics/microsoft-sql-managedinstances-metrics-include.md)]

[!INCLUDE [horz-monitor-ref-metrics-dimensions-intro](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-metrics-dimensions-intro.md)]
[!INCLUDE [horz-monitor-ref-no-metrics-dimensions](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-no-metrics-dimensions.md)]

[!INCLUDE [horz-monitor-ref-resource-logs](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-resource-logs.md)]

### Supported resource logs for Microsoft.Sql/managedInstances
[!INCLUDE [microsoft-sql-managedinstances-logs-include](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/reference/logs/microsoft-sql-managedinstances-logs-include.md)]

### Supported resource logs for Microsoft.Sql/managedInstances/databases
[!INCLUDE [microsoft-sql-managedinstances-databases-logs-include](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/reference/logs/microsoft-sql-managedinstances-databases-logs-include.md)]

[!INCLUDE [horz-monitor-ref-logs-tables](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-logs-tables.md)]

|Table | Notes |
|-------|-----|
| [AzureActivity](/azure/azure-monitor/reference/tables/azureactivity) | Entries from the Azure Activity log that provides insight into any subscription-level or management group level events that have occurred in Azure. |
| [AzureDiagnostics](/azure/azure-monitor/reference/tables/azurediagnostics) | Azure Diagnostics reveals diagnostic data of specific resources and features for numerous Azure products including Azure SQL databases, elastic pools, and managed instances. For more information, see [Diagnostics metrics](../database/metrics-diagnostic-telemetry-logging-streaming-export-configure.md?tabs=azure-portal#basic-metrics).|
| [AzureMetrics](/azure/azure-monitor/reference/tables/azuremetrics) | Metric data emitted by Azure services that measure their health and performance. Activity from Azure products including Azure SQL databases, elastic pools, and managed instances.|

[!INCLUDE [horz-monitor-ref-activity-log](~/../azure-sql/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-activity-log.md)]

- [Microsoft.Sql resource provider operations](/azure/role-based-access-control/resource-provider-operations#microsoftsql)

## Related content

- See [Monitor SQL Managed Instance](monitoring-sql-managed-instance-azure-monitor.md) for a description of monitoring Azure SQL Managed Instance.
- See [Monitor Azure resources with Azure Monitor](/azure/azure-monitor/essentials/monitor-azure-resource) for details on monitoring Azure resources.
