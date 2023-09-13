---
title: Monitoring Arc-enabled SQL Servers
description: How to enable or disable monitoring on Arc-enabled SQL Servers
author: lcwright
ms.author: lancewright
ms.reviewer: mikeray
ms.date: 09/08/2023
ms.topic: conceptual
---

# Monitoring Arc-enabled SQL Servers

In a future release, Arc-enabled SQL Servers will provide a feature that will automatically collect SQL Server monitoring data from dynamic management views and send it to Azure. Customers will then be able to see monitoring dashboards in the Azure Portal. No setup or configuration will be required. 

When released, this monitoring feature will be enabled by default and the monitoring data will automatically be collected on Arc-enabled SQL Servers. Customers can turn off the monitoring data collection at any time, including turning it off in advance of the feature being released. To disable the collection of monitoring data run the following command in the Azure CLI, replacing the placeholders for subscription ID, resource group, and resource name:

```az
az resource patch --ids "/subscriptions/<sub_id>/resourceGroups/<resource_group>/providers/Microsoft.AzureArcData/SqlServerInstances/<resource_name>" -p '{\"monitoring\": {\"enabled\": false}}' --api-version 2023-09-01-preview
```

To enable the monitoring data collection for an Arc-enabled SQL Server, customers can run the following command in the Azure CLI, replacing the placeholders for subscription ID, resource group, and resource name:

```az
az resource patch --ids "/subscriptions/<sub_id>/resourceGroups/<resource_group>/providers/Microsoft.AzureArcData/SqlServerInstances/<resource_name>" -p '{\"monitoring\": {\"enabled\": true}}' --api-version 2023-09-01-preview
```

Below is a list of monitoring data that will be collected on Arc-enabled SQL Server when this feature is released in the future.  No Personally Identifiable Information (PII), End-User Identifiable  Information, or Customer Content is collected.
  
|Dataset name|Description|
|----|----|
|Active sessions|Includes data on requests, blockers, and open transactions.|
|CPU utilization|CPU utilization over time.|
|Database properties|Includes database options and other database metadata.|
|Database storage utilization|Includes its storage usage and Persistent Version Store.|
|Memory utilization|Memory clerks and memory consumption by the clerk.|
|Performance counters (common & detailed)|Includes performance counters (both common and detailed) recorded by SQL Server.|
|Storage IO|Includes cumulative IOPS, throughput, and latency statistics.|
|Wait statistics|Includes wait types and wait statistics for the database engine instance.|

## Limitations
  
- The monitoring data will be collected only for Enterprise Edition and Standard Edition instances of SQL Server for now.  
- This feature will also only be enabled for SQL Server instances with Software Assurance or Pay-as-you-go licensing.  
- This feature will work only on SQL Server on Windows initially.  

## Next steps
  
- [Learn more about using activity logs to provide insights into Arc-enabled SQL Server events](docs/sql-server/azure-arc/activity-logs.md)
- [Learn more about Azure Arc-enabled SQL Server data collection and reporting](docs/sql-server/azure-arc/data-collection.md)
