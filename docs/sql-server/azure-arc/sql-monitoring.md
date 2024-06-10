---
title: Monitor in Azure portal
description: Describes the monitoring capabilities of SQL Server enabled by Azure Arc.
author: lcwright
ms.author: lancewright
ms.reviewer: mikeray
ms.date: 11/26/2023
ms.topic: conceptual
ms.custom: ignite-2023
---

# Monitor SQL Server enabled by Azure Arc (preview)

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

Monitor [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] with performance dashboard in the Azure portal. Performance metrics are automatically collected from DMV datasets on eligible instances of [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] and sent to the Azure telemetry pipeline for near real-time processing.

[!INCLUDE [azure-arc-sql-preview](includes/azure-arc-sql-preview.md)]

During the feature preview, monitoring is available for free. Fees for this feature after general availability are to be determined.

To view metrics in the portal:

1. Select an instance of [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)]
1. Select **Monitoring** > **Performance Dashboard**

Monitoring is automatic, assuming all prerequisites are met.

:::image type="content" source="media/overview/performance-dashboard.png" alt-text="Screenshot of performance dashboard for SQL Server enabled by Azure Arc." lightbox="media/overview/performance-dashboard.png":::

## Prerequisites

In order for monitoring data to be collected on a [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] and view the performance metrics in Azure, the following conditions must be met:

* The version of Azure Extension for SQL Server (WindowsAgent.SqlServer) is v1.1.2504.99 or later
* [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] is running on Windows operating system
   - [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] running on [!INCLUDE [winserver2012-md](../../includes/winserver2012-md.md)] R2 and older versions aren't supported
* [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] is a Standard or Enterprise Edition
* SQL Server version must be 2016 or later
* The server has connectivity to `*.<region>.arcdataservices.com` (for more information, see [Network Requirements ](/azure/azure-arc/servers/network-requirements?tabs=azure-cloud))
* The license type on the [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] is set to "License with Software Assurance" or "Pay-as-you-go"
* To view the performance dashboard in the Azure portal, you must be assigned an Azure role with the action `Microsoft.AzureArcData/sqlServerInstances/getTelemetry/` assigned. For convenience, you can use the built-in role "Azure Hybrid Database Administrator - Read Only Service Role", which includes this action. (For more information, see [Learn more about Azure built-in roles](/azure/role-based-access-control/built-in-roles))

### Current Limitations

Failover cluster instances (FCI) aren't supported at this time.

## Disable or enable collection

> [!IMPORTANT]
> In order to disable or enable data collection, the `sqlServer` extension must be on v1.1.2504.99 or later.

### Using the Azure portal

* On the resource page for a [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)], select the **Performance Dashboard (preview)** section.
* At the top of the **Performance Dashboard** page, select **Configure**. The portal opens **Configure monitoring settings** on the right-hand side.
* In **Configure monitoring settings**, toggle the option for monitoring data collection on or off.
* Select **Apply settings**.

### Using the Azure CLI

#### Disable monitoring data collection

To disable monitoring data collection for your [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)], run the following command in the Azure CLI . Replace the placeholders for subscription ID, resource group, and resource name:

```azurecli
az resource update --ids "/subscriptions/<sub_id>/resourceGroups/<resource_group>/providers/Microsoft.AzureArcData/SqlServerInstances/<resource_name>" --set 'properties.monitoring.enabled=false' --api-version 2023-09-01-preview
```

#### Enable monitoring data collection

To enable the monitoring data collection for a [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)], run the following command in the Azure CLI. Replace the placeholders for subscription ID, resource group, and resource name:

```azurecli
az resource update --ids "/subscriptions/<sub_id>/resourceGroups/<resource_group>/providers/Microsoft.AzureArcData/SqlServerInstances/<resource_name>" --set 'properties.monitoring.enabled=true' --api-version 2023-09-01-preview
```

This command might run successfully, but all [prerequisites]](#prerequisites) must be met for monitoring data to be collected and shown in the Azure portal.

## Collected data

The following lists reflect the monitoring data that is collected from DMV datasets on [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] when the monitoring feature is enabled. No personally identifiable information (PII), end-user identifiable information (EUII), or customer content is collected.

[!INCLUDE [azure-arc-data-regions](includes/dmv-collection.md)]

## Next steps
  
* [[!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] and Databases activity logs](activity-logs.md)
* [[!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] data collection and reporting](data-collection.md)
* [Dynamic management views (DMVs)](../../relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)

