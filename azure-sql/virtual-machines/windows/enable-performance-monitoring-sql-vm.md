---
title: Enable Performance Monitoring for SQL Server on Azure VMs (Preview)
description: Learn how to enable, verify, and disable performance monitoring for SQL Server on Azure virtual machines by using the SQL IaaS Agent extension, and how to view and query the collected data.
author: lcwright
ms.author: lancewright
ms.reviewer: wiassaf
ms.date: 09/28/2026
ms.service: azure-vm-sql-server
ms.subservice: management
ms.topic: how-to
ms.custom:
  - devx-track-azurecli
  - devx-track-azurepowershell
ai-usage: ai-assisted
monikerRange: "=azuresql-vm"
---

# Enable performance monitoring for SQL Server on Azure VMs (preview)

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article explains how to enable, verify, and disable performance monitoring for SQL Server on Azure Virtual Machines (VMs), and how to view and query the data that it collects.

Performance monitoring provides a Microsoft-managed monitoring experience for your SQL Server instances. After you enable monitoring on a VM, the SQL IaaS Agent extension collects performance data from the SQL Server instance and sends it to Azure for processing and storage. You don't have to deploy or maintain monitoring agents, data stores, or other monitoring infrastructure.

The collected data provides visibility into resource utilization, database activity, storage performance, active sessions, and wait statistics. Use this data to establish a performance baseline, identify bottlenecks, investigate the cause of performance issues, and find where tuning can improve performance. You can view the data in Fabric Database Hub, or query it directly by using Kusto Query Language (KQL) through an Azure Data Explorer query endpoint.

> [!NOTE]
> Performance monitoring for SQL Server on Azure VMs is currently in preview. Availability, prerequisites, and supported configurations might change before general availability. For more information, see [Supplemental Terms of Use for Microsoft Azure Previews](https://azure.microsoft.com/support/legal/preview-supplemental-terms/).

## How performance monitoring works

You enable performance monitoring by adding the `DatabaseWatcheronAzureVM` feature flag to the public settings of the [SQL IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md) on the VM. The extension detects the setting and starts collecting performance data from the SQL Server instance that it manages. The extension then uses the VM's system-assigned managed identity to upload the data to a regional telemetry endpoint.

Monitoring is configured per VM. Enabling the feature flag on one VM doesn't enable monitoring for other VMs.

## Supported configurations

During the preview, performance monitoring supports the following SQL Server on Azure VM configurations:

| Configuration                                                                                                        | Supported |
| -------------------------------------------------------------------------------------------------------------------- | --------- |
| SQL Server 2016 and later versions                                                                                   | Yes       |
| SQL Server versions earlier than SQL Server 2016                                                                     | No        |
| SQL Server Enterprise and Standard editions                                                                          | Yes       |
| SQL Server Developer, Express, and Evaluation editions                                                               | Partial. Only client connection data (`SqlServerClientConnections`) is collected. The other [collected datasets](#collected-datasets) aren't available. |

<!-- The following sentences repeat and rephrase the content in the preceding table for maximum context clarity. Keep this prose summary synchronized with the preceding table. -->

- Performance monitoring supports SQL Server 2016 and later versions on Azure VMs.
- Performance monitoring doesn't support SQL Server versions earlier than SQL Server 2016 on Azure VMs.
- Performance monitoring supports SQL Server Enterprise and Standard editions on Azure VMs.
- For SQL Server Developer, Express, and Evaluation editions on Azure VMs, performance monitoring only collects client connection data in `SqlServerClientConnections`. The other [collected datasets](#collected-datasets) aren't available for these editions.

## Prerequisites

Before you enable performance monitoring, ensure that you have the following items:

- A SQL Server on Azure VM in a [supported configuration](#supported-configurations).
- The [SQL IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md) version `2.0.229.0` or later installed in full management mode. The extension must be enabled and its provisioning state must be **Succeeded**.
- A [system-assigned managed identity](/entra/identity/managed-identities-azure-resources/how-to-configure-managed-identities#enable-system-assigned-managed-identity-on-an-existing-vm) enabled for the VM.
- A SQL Server instance resource available through [unified inventory](unified-inventory-sql-vm.md).
- The `Microsoft.AzureArcData` resource provider [registered](/azure/azure-resource-manager/management/resource-providers-and-types#register-resource-provider) for the subscription.

  To register the resource provider, run the following Azure CLI command:

  ```azurecli
  az provider register --namespace Microsoft.AzureArcData
  ```

  To check the registration state, run the following command:

  ```azurecli
  az provider show --namespace Microsoft.AzureArcData --query "registrationState" --output tsv
  ```

  Registration is complete when the command returns `Registered`.

- Outbound HTTPS connectivity on port `443` to `telemetry.<region>.arcdataservices.com`, where `<region>` is the Azure region that hosts the VM.
- The latest version of the [Azure CLI](/cli/azure/install-azure-cli).
- Permission to view and update extensions on the VM, such as membership in the [Virtual Machine Contributor](/azure/role-based-access-control/built-in-roles/compute#virtual-machine-contributor) role.

To view or query the collected data, you also need the [access prerequisites](#access-prerequisites) described later in this article.

## Enable performance monitoring collection

> [!IMPORTANT]
> SQL IaaS Agent extension settings aren't cumulative. When you update the extension, include all existing public settings to avoid unintentionally disabling another feature.

The following PowerShell steps retrieve the current public settings, add the performance monitoring feature flag, and apply the merged settings to the extension.

1. Open PowerShell in an environment where the Azure CLI is installed, and then sign in to Azure.

   ```powershell
   az login
   ```

1. Set the variables for your SQL Server VM.

   ```powershell
   $subscriptionId = "<subscription-id>"
   $resourceGroup = "<resource-group>"
   $vmName = "<vm-name>"

   az account set --subscription $subscriptionId
   ```

1. Retrieve the current SQL IaaS Agent extension settings.

   ```powershell
   $extension = az vm extension show `
     --resource-group $resourceGroup `
     --vm-name $vmName `
     --name SqlIaasExtension `
     --query "{settings:settings,typeHandlerVersion:typeHandlerVersion}" `
     --output json | ConvertFrom-Json

   if ($null -eq $extension.settings) {
     $settings = [pscustomobject]@{}
   }
   else {
     $settings = $extension.settings
   }
   ```

1. Add the `DatabaseWatcheronAzureVM` feature flag while preserving the existing feature flags.

   ```powershell
   $monitoringFlag = [pscustomobject]@{
     Name = "DatabaseWatcheronAzureVM"
     Enable = $true
   }

   $featureFlags = @(
     $settings.FeatureFlags |
       Where-Object { $_.Name -ne $monitoringFlag.Name }
   )
   $featureFlags += $monitoringFlag

   $settings | Add-Member `
     -MemberType NoteProperty `
     -Name FeatureFlags `
     -Value $featureFlags `
     -Force
   ```

1. Save the merged settings and apply them to the SQL IaaS Agent extension.

   ```powershell
   $settingsPath = Join-Path $env:TEMP "sqlvm-performance-monitoring-settings.json"

   try {
     $settings |
       ConvertTo-Json -Depth 50 |
       Out-File -FilePath $settingsPath -Encoding utf8

     az vm extension set `
       --resource-group $resourceGroup `
       --vm-name $vmName `
       --publisher Microsoft.SqlServer.Management `
       --name SqlIaaSAgent `
       --extension-instance-name SqlIaasExtension `
       --settings "@$settingsPath"
   }
   finally {
     Remove-Item $settingsPath -ErrorAction SilentlyContinue
   }
   ```

1. Repeat these steps for each VM that you want to monitor.

The extension reloads the public settings automatically. You don't need to restart the SQL Server IaaS Agent service or the VM. Most performance data is available to query within three to five minutes after the setting takes effect. Some inventory-based data can take up to 15 minutes.

## Verify performance monitoring collection

Verify the extension version and the monitoring status after you enable performance monitoring.

### Check the extension version

Use `instanceView.typeHandlerVersion` to get the full runtime version of the extension:

```powershell
az vm extension show `
  --resource-group $resourceGroup `
  --vm-name $vmName `
  --name SqlIaasExtension `
  --instance-view `
  --query "instanceView.typeHandlerVersion" `
  --output tsv
```

Ensure that the command returns version `2.0.229.0` or later. The top-level `typeHandlerVersion` property returns only the major and minor version, such as `2.0`.

### Check the monitoring status

Run the following command to view the SQL IaaS Agent extension status:

```powershell
az vm extension show `
  --resource-group $resourceGroup `
  --vm-name $vmName `
  --name SqlIaasExtension `
  --instance-view `
  --query "instanceView.statuses[0].message" `
  --output tsv
```

When performance monitoring is running and uploading metrics successfully, the status includes the following output:

```output
DatabaseMonitorArcPlugin: {"State":"Running","MetricsUploadStatus":"OK"}
```

A `MetricsUploadStatus` value of `OK` confirms that the extension successfully sent data to the regional telemetry endpoint. To confirm that data is available, [query the performance data](#query-performance-data-with-azure-data-explorer) for the SQL Server instance.

## Disable performance monitoring collection

To stop collecting new performance data for a VM, set the `DatabaseWatcheronAzureVM` feature flag to `false`. As when you enable monitoring, merge the change with the existing public settings so that other SQL IaaS Agent extension features aren't affected.

1. Complete steps 1 through 3 in [Enable performance monitoring collection](#enable-performance-monitoring-collection) to sign in, set the variables, and retrieve the current settings.

1. Set the `DatabaseWatcheronAzureVM` feature flag to `false` while preserving the existing feature flags.

   ```powershell
   $monitoringFlag = [pscustomobject]@{
     Name = "DatabaseWatcheronAzureVM"
     Enable = $false
   }

   $featureFlags = @(
     $settings.FeatureFlags |
       Where-Object { $_.Name -ne $monitoringFlag.Name }
   )
   $featureFlags += $monitoringFlag

   $settings | Add-Member `
     -MemberType NoteProperty `
     -Name FeatureFlags `
     -Value $featureFlags `
     -Force
   ```

1. Complete step 5 in [Enable performance monitoring collection](#enable-performance-monitoring-collection) to apply the merged settings to the extension.

To confirm the change, [check the monitoring status](#check-the-monitoring-status) again. Allow a few minutes for the new setting to take effect.

## View and query performance data

Enabling performance monitoring starts data collection. To view or query the collected data, you need the access prerequisites in this section. All dashboards and query tools read from the same telemetry endpoint, which enforces [Azure role-based access control (Azure RBAC)](/azure/role-based-access-control/overview).

### Access prerequisites

- Your account must be a member of the [Reader](/azure/role-based-access-control/built-in-roles/general#reader) role, or a role with higher privileges, on the subscription that contains the SQL Server resources you want to query. Alternatively, assign your account a [custom role](/azure/role-based-access-control/custom-roles) on the subscription that includes the following actions:
  - `Microsoft.AzureArcData/sqlServerInstances/read`
  - `Microsoft.Sql/servers/read`

### View performance data in Fabric Database Hub

[Fabric Database Hub](/fabric/database/hub/overview) provides built-in dashboards that show the performance of your SQL Server instances in one place. Use the dashboards to review resource utilization, database activity, storage performance, active sessions, and wait statistics across your SQL Server estate. Drill into an individual instance to investigate a performance issue. Fabric Database Hub reads from the same telemetry endpoint described in this article, so the same [access prerequisites](#access-prerequisites) apply.

You can also create a [Real-Time Dashboard](/fabric/real-time-intelligence/dashboard-real-time-create) in Microsoft Fabric that uses the telemetry endpoint as its data source.

### Query performance data with Azure Data Explorer

You can connect directly to the telemetry endpoint and query the performance data by using [KQL](/kusto/query/). Use this option for ad hoc analysis, to build your own queries, or to integrate the data with other tools. For the schema, rules for correct results, and ready-to-run queries, see [Query performance monitoring telemetry](../../database/query-performance-monitoring-telemetry.md).

> [!NOTE]
> Use the [Azure Data Explorer web UI](/azure/data-explorer/web-ui-query-overview). The Kusto.Explorer desktop client isn't currently supported.

To connect to the telemetry endpoint:

1. Go to the [Azure Data Explorer web UI](https://dataexplorer.azure.com/).
1. In the **Connections** pane, select **Add**, and then select **Connection**.
1. For **Connection URI**, enter `https://adx.centralus.arcdataservices.com/kusto/`.

   > [!NOTE]
   > Use this connection URI for all SQL Server VMs, regardless of the Azure region that hosts the VM.

1. Optionally, enter a display name for the connection, and then select **Add**. If prompted, add the URI as a trusted source.
1. Expand the connection, and then select the `ArcSqlTelemetry` database.
1. Select a table, and then use the query window to write and run KQL queries against your performance data.

## Collected datasets

Performance monitoring collects the following datasets for SQL Server on Azure VMs. Each dataset is stored in a table in the `ArcSqlTelemetry` database on the [telemetry endpoint](#query-performance-data-with-azure-data-explorer). For the columns in each table, see [Performance monitoring data schema](../../database/query-performance-monitoring-telemetry.md#schema).

| Table                                  | Data collected                           |
| -------------------------------------- | ---------------------------------------- |
| `SqlServerActiveSessions` | Active sessions                          |
| `SqlServerAvailabilityGroupStates` | Availability group state                 |
| `SqlServerAvailabilityReplicaStates` | Availability replica state               |
| `SqlServerClientConnections` | Client connections                       |
| `SqlServerCPUUtilization` | CPU utilization                          |
| `SqlServerDatabaseProperties` | Database properties                      |
| `SqlServerDatabaseReplicaStates` | Database replica state                   |
| `SqlServerDatabaseStorageUtilization` | Database storage utilization             |
| `SqlServerMemoryUtilization` | Memory utilization                       |
| `SqlServerPerformanceCountersCommon` | Common SQL Server performance counters   |
| `SqlServerPerformanceCountersDetailed` | Detailed SQL Server performance counters |
| `SqlServerStorageIO` | Data and log storage I/O                 |
| `SqlServerWaitStats` | Wait statistics                          |

<!-- The following sentences repeat and rephrase the content in the preceding table for maximum context clarity. Keep this prose summary synchronized with the preceding table. -->

- The `SqlServerActiveSessions` table contains active session data for SQL Server on Azure VMs performance monitoring.
- The `SqlServerAvailabilityGroupStates` table contains availability group state data for SQL Server on Azure VMs performance monitoring.
- The `SqlServerAvailabilityReplicaStates` table contains availability replica state data for SQL Server on Azure VMs performance monitoring.
- The `SqlServerClientConnections` table contains client connection data for SQL Server on Azure VMs performance monitoring.
- The `SqlServerCPUUtilization` table contains CPU utilization data for SQL Server on Azure VMs performance monitoring.
- The `SqlServerDatabaseProperties` table contains database property data for SQL Server on Azure VMs performance monitoring.
- The `SqlServerDatabaseReplicaStates` table contains database replica state data for SQL Server on Azure VMs performance monitoring.
- The `SqlServerDatabaseStorageUtilization` table contains database storage utilization data for SQL Server on Azure VMs performance monitoring.
- The `SqlServerMemoryUtilization` table contains memory utilization data for SQL Server on Azure VMs performance monitoring.
- The `SqlServerPerformanceCountersCommon` table contains common SQL Server performance counter data for SQL Server on Azure VMs performance monitoring.
- The `SqlServerPerformanceCountersDetailed` table contains detailed SQL Server performance counter data for SQL Server on Azure VMs performance monitoring.
- The `SqlServerStorageIO` table contains data and log storage I/O data for SQL Server on Azure VMs performance monitoring.
- The `SqlServerWaitStats` table contains wait statistics for SQL Server on Azure VMs performance monitoring.

Except for `SqlServerClientConnections`, the tables in this list require SQL Server Standard or Enterprise edition. Client connection data uses a separate collection path and isn't subject to this edition requirement.

## Troubleshoot performance monitoring

Use the following table to troubleshoot common issues:

| Symptom | Possible cause | Recommended action |
| ------- | -------------- | ------------------ |
| The monitoring state is `NotRunning`. | The feature flag isn't enabled, the setting hasn't applied yet, or the SQL Server version or edition isn't supported. | Confirm the extension settings, and verify that the VM is in a [supported configuration](#supported-configurations). |
| `MetricsUploadStatus` contains an HTTP error. | The extension can't reach the telemetry endpoint or authenticate by using the managed identity. | Confirm that the system-assigned managed identity is enabled and that outbound HTTPS connectivity to `telemetry.<region>.arcdataservices.com:443` is allowed. |
| Dashboards or queries don't return data for the SQL Server instance. | Data is still being processed, or the SQL Server instance resource is missing. | Wait at least 15 minutes, and then confirm that the instance appears in [unified inventory](unified-inventory-sql-vm.md). |
| Developer, Express, or Evaluation edition only shows client connection data. | These editions only support client connection data. The other [collected datasets](#collected-datasets) aren't available. | Use SQL Server Standard or Enterprise edition. |
| Azure Data Explorer can't connect to the query endpoint. | The account doesn't meet the [access prerequisites](#access-prerequisites), or the required resource provider isn't registered. | Confirm that the account has the **Reader** role or higher, or a custom role with the required actions, and that `Microsoft.AzureArcData` is registered for the subscription. |
| Another SQL IaaS Agent extension feature stops working after monitoring is enabled or disabled. | Existing public settings were replaced instead of merged. | Restore the previous extension settings, and then update the performance monitoring feature flag by using the merge procedure in this article. |

<!-- The following sentences repeat and rephrase the content in the preceding table for maximum context clarity. Keep this prose summary synchronized with the preceding table. -->

- If the monitoring state is `NotRunning`, the feature flag might not be enabled, the setting might not have applied yet, or the SQL Server version or edition might not be supported. Confirm the extension settings, and verify that the VM is in a [supported configuration](#supported-configurations).
- If `MetricsUploadStatus` contains an HTTP error, the extension can't reach the telemetry endpoint or authenticate by using the managed identity. Confirm that the system-assigned managed identity is enabled and that outbound HTTPS connectivity to `telemetry.<region>.arcdataservices.com:443` is allowed.
- If dashboards or queries don't return data for the SQL Server instance, the data might still be processing or the SQL Server instance resource might be missing. Wait at least 15 minutes, and then confirm that the instance appears in [unified inventory](unified-inventory-sql-vm.md).
- If Developer, Express, or Evaluation edition only shows client connection data, these editions don't support the other [collected datasets](#collected-datasets). Use SQL Server Standard or Enterprise edition to collect those datasets.
- If Azure Data Explorer can't connect to the query endpoint, the account might not meet the [access prerequisites](#access-prerequisites), or the required resource provider might not be registered. Confirm that the account has the **Reader** role or higher, or a custom role with the required actions, and that `Microsoft.AzureArcData` is registered for the subscription.
- If another SQL IaaS Agent extension feature stops working after monitoring is enabled or disabled, existing public settings were replaced instead of merged. Restore the previous extension settings, and then update the performance monitoring feature flag by using the merge procedure in this article.

## Platform support

The `DatabaseWatcheronAzureVM` SQL IaaS Agent extension feature flag doesn't enable performance monitoring for SQL Server instances that aren't on Azure virtual machines, or for Azure SQL Database, Azure SQL Managed Instance, SQL database in Fabric, or Fabric Data Warehouse.

## Related content

- [Query performance monitoring telemetry (preview)](../../database/query-performance-monitoring-telemetry.md)
- [Enable performance monitoring for Azure SQL Database (preview)](../../database/enable-performance-monitoring-sql-database.md)
- [Automate management with the SQL IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md)
- [Unified inventory for SQL Server on Azure VMs](unified-inventory-sql-vm.md)
- [Performance best practices checklist for SQL Server on Azure VMs](performance-guidelines-best-practices-checklist.md)
