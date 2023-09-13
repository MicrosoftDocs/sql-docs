---
title: Monitoring Azure Arc-enabled SQL Servers
description: How to enable or disable monitoring on Azure Arc-enabled SQL Servers
author: lcwright
ms.author: lancewright
ms.reviewer: mikeray
ms.date: 09/08/2023
ms.topic: conceptual
---

# Monitor Azure Arc-enabled SQL Servers

In a future release, you will be able to review some data from SQL Server dynamic management views (DMVs) in Azure portal. This capability requires Azure to collect data from instances of Microsoft Azure Arc-enabled SQL Server. This article explains how to configure or prevent this collection and describes what data is collected.

## Disable or enable collection

When the feature is released for Azure Arc-enabled SQL Server, the data collection will be automatic. You will not be required to set up or configure anything. You can turn off the monitoring data collection at any time. You can also turn collection off before the feature is released.

To disable the collection of monitoring data, run the following command in the Azure CLI. Replace the placeholders for subscription ID, resource group, and resource name:

```azurecli
az resource patch --ids "/subscriptions/<sub_id>/resourceGroups/<resource_group>/providers/Microsoft.AzureArcData/SqlServerInstances/<resource_name>" -p '{\"monitoring\": {\"enabled\": false}}' --api-version 2023-09-01-preview
```

To enable the monitoring data collection for an Azure Arc-enabled SQL Server, run the following command in the Azure CLI. Replace the placeholders for subscription ID, resource group, and resource name:

```azurecli
az resource patch --ids "/subscriptions/<sub_id>/resourceGroups/<resource_group>/providers/Microsoft.AzureArcData/SqlServerInstances/<resource_name>" -p '{\"monitoring\": {\"enabled\": true}}' --api-version 2023-09-01-preview
```

## Collected data

Below is a list of monitoring data that will be collected on Azure Arc-enabled SQL Server when this feature is released in the future.  No personally identifiable information (PII), end-user identifiable information, or customer content is collected.
  
|Dataset name|Description|
|----|----|
|Active sessions|Includes data on requests, blockers, and open transactions.|
|CPU utilization|CPU utilization over time.|
|Database properties|Includes database options and other database metadata.|
|Database storage utilization|Includes its storage usage and persistent version store.|
|Memory utilization|Memory clerks and memory consumption by the clerk.|
|Performance counters (common & detailed)|Includes performance counters (both common and detailed) recorded by SQL Server.|
|Storage IO|Includes cumulative IOPS, throughput, and latency statistics.|
|Wait statistics|Includes wait types and wait statistics for the database engine instance.|

## Limitations
  
Initally, the monitoring data will:

- Be collected only for Enterprise Edition and Standard Edition instances of SQL Server.
- Only be enabled for SQL Server instances with Software Assurance or pay-as-you-go licensing.  
- Only work on SQL Server on Windows.  

## Next steps
  
- [Azure Arc-enabled SQL Server and Databases activity logs](activity-logs.md)
- [Azure Arc-enabled SQL Server data collection and reporting](data-collection.md)