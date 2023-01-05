---
title: Monitoring Azure SQL Managed Instance with Azure Monitor reference
description: Important reference material needed when you monitor Azure SQL Managed Instance with Azure Monitor
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma, dfurman
ms.date: 07/29/2022
ms.service: sql-managed-instance
ms.subservice: performance
ms.topic: conceptual
ms.custom: subject-monitoring
monikerRange: "= azuresql || = azuresql-mi"
---

# Monitoring Azure SQL Managed Instance data reference
[!INCLUDE [sqlmi](../includes/appliesto-sqlmi.md)]

This article contains reference for monitoring Azure SQL Managed Instance with Azure Monitor. See [Monitoring Azure SQL Managed Instance](monitoring-sql-managed-instance-azure-monitor.md) for details on collecting and analyzing monitoring data for Azure SQL Managed Instance with Azure Monitor SQL Insights (preview).

## Metrics

For more on using Azure Monitor SQL Insights (preview) for all products in the [Azure SQL family](../index.yml), see [Monitor your SQL deployments with SQL Insights (preview)](/azure/azure-monitor/insights/sql-insights-overview).

For data specific to Azure SQL Managed Instance, see [Data for Azure SQL Managed Instance](/azure/azure-monitor/insights/sql-insights-overview#data-for-azure-sql-managed-instance).

For a complete list of metrics, see [Microsoft.Sql/managedInstances](/azure/azure-monitor/essentials/metrics-supported#microsoftsqlmanagedinstances).

## Resource logs

This section lists the types of resource logs you can collect for Azure SQL Managed Instance. 

For reference, see a list of [all resource logs category types supported in Azure Monitor](/azure/azure-monitor/essentials/resource-logs-schema).

For a reference of resource log types collected for Azure SQL Managed Instance, see [Streaming export of Azure SQL Managed Instance Diagnostic telemetry for export](../database/metrics-diagnostic-telemetry-logging-streaming-export-configure.md#diagnostic-telemetry-for-export)

## Azure Monitor Logs tables

This section refers to all of the Azure Monitor Logs tables relevant to Azure SQL Managed Instance and available for query by Log Analytics, which can be queried with KQL.

Tables for all resources types are referenced here, for example, [Azure Monitor tables for Azure SQL Managed Instances](/azure/azure-monitor/reference/tables/tables-resourcetype#sql-managed-instances).

|Resource Type | Notes |
|-------|-----|
| [AzureActivity](/azure/azure-monitor/reference/tables/azureactivity) | Entries from the Azure Activity log that provides insight into any subscription-level or management group level events that have occurred in Azure. |
| [AzureDiagnostics](/azure/azure-monitor/reference/tables/azurediagnostics) | Azure Diagnostics reveals diagnostic data of specific resources and features for numerous Azure products including Azure SQL databases, elastic pools, and managed instances. For more information, see [Diagnostics metrics](../database/metrics-diagnostic-telemetry-logging-streaming-export-configure.md?tabs=azure-portal#basic-metrics).|
| [AzureMetrics](/azure/azure-monitor/reference/tables/azuremetrics) | Metric data emitted by Azure services that measure their health and performance. Activity from Azure products including Azure SQL databases, elastic pools, and managed instances.|

## Activity log

The Activity log contains records of management operations performed on your Azure SQL Managed Instance resources. All maintenance operations related to Azure SQL Managed Instance that have been implemented here may appear in the Activity log.

For more information on the schema of Activity Log entries, see [Activity Log schema](/azure/azure-monitor/essentials/activity-log-schema).

## Next steps

- [Monitoring Azure SQL Managed Instance with Azure Monitor](monitoring-sql-managed-instance-azure-monitor.md)
- [Monitoring Azure SQL Database with Azure Monitor](../database/monitoring-sql-database-azure-monitor.md)
- [Monitoring Azure resources with Azure Monitor](/azure/azure-monitor/essentials/monitor-azure-resource)
