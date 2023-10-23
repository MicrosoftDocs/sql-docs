---
title: Monitor Azure Arc-enabled SQL Server in Azure portal
description: How to enable or disable monitoring on Azure Arc-enabled SQL Servers
author: lcwright
ms.author: lancewright
ms.reviewer: mikeray
ms.date: 09/08/2023
ms.topic: conceptual
---

# Monitor Azure Arc-enabled SQL Server

Microsoft Azure Arc-enabled SQL Server can post monitoring data to Azure. This feature is not currently available, but will be enabled in a future release. When it is available, the feature is automatic in order to allow you to view monitoring data in Azure portal.

This article explains how to prevent your Arc-enabled SQL Server instances from posting monitoring data to Azure. It also describes the data that is posted in the future feature.

## Disable or enable collection

> [!IMPORTANT]
> In order to disable or enable data collection, the `sqlServer` extension must be on `v1.1.2451.59` or later. [Upgrade VM extensions using the Azure Portal.](/azure/azure-arc/servers/manage-vm-extensions-portal)

### Enable the feature flag

Prior to disabling or enabling monitoring data collection, you must first enable the `SqlManagement` feature flag. This is a feature flag that will automatically be enabled in a future release. However, it is required to toggle the `monitoring.enabled` property on your Arc-enabled SQL Server using the Azure CLI.

1. Download the [`set-feature-flags.ps1`](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-arc-enabled-sql-server/monitoring/set-feature-flags.ps1) PowerShell script from GitHub. 

1. Configure and run `set-feature-flags.ps1` in a PowerShell terminal on your Azure Arc-enabled Server.

   ```powershell
   set-feature-flags.ps1 `
       -Subscription "<Your-subscription-ID>" `
       -ResourceGroup "contoso-rg" `
       -MachineName "contoso-sql-host" `
       -FeatureFlagsToEnable ("SqlManagement")
   ```

### Disable monitoring data collection

After enabling the SqlManagement feature flag, run the following command in the Azure CLI to disable monitoring data collection for your Azure Arc-enabled SQL Server. Replace the placeholders for subscription ID, resource group, and resource name:


```azurecli
az resource update --ids "/subscriptions/<sub_id>/resourceGroups/<resource_group>/providers/Microsoft.AzureArcData/SqlServerInstances/<resource_name>" --set 'properties.monitoring.enabled=false' --api-version 2023-09-01-preview
```

### Enable monitoring data collection

To enable the monitoring data collection for an Azure Arc-enabled SQL Server, run the following command in the Azure CLI. Replace the placeholders for subscription ID, resource group, and resource name:


```azurecli
az resource update --ids "/subscriptions/<sub_id>/resourceGroups/<resource_group>/providers/Microsoft.AzureArcData/SqlServerInstances/<resource_name>" --set 'properties.monitoring.enabled=true' --api-version 2023-09-01-preview
```

## Collected data

The following table identifies monitoring data that is planned for collection on Azure Arc-enabled SQL Server when this feature is released.  No personally identifiable information (PII), end-user identifiable information, or customer content is collected.
  
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
  
Initially, the monitoring data is:

- Collected only for Enterprise Edition and Standard Edition instances of SQL Server.
- Only enabled for SQL Server instances with Software Assurance or pay-as-you-go licensing.  
- Only available for  SQL Server instances on Windows.  

## Next steps
  
- [Azure Arc-enabled SQL Server and Databases activity logs](activity-logs.md)
- [Azure Arc-enabled SQL Server data collection and reporting](data-collection.md)


