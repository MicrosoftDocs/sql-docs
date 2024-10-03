---
title: Monitor SQL Server
description: Learn about the monitoring capabilities of SQL Server enabled by Azure Arc.
author: lcwright
ms.author: lancewright
ms.reviewer: mikeray, randolphwest
ms.date: 09/09/2024
ms.topic: conceptual
ms.custom:
  - ignite-2023
---

# Monitor SQL Server enabled by Azure Arc (preview)

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

You can monitor [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] by using the performance dashboard in the Azure portal. Performance metrics are automatically collected from dynamic management view (DMV) datasets on eligible instances of [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)]. The metrics are then sent to the Azure telemetry pipeline for near real-time processing.

[!INCLUDE [azure-arc-sql-preview](includes/azure-arc-sql-preview.md)]

During the feature preview, monitoring is available for free. Fees for this feature after general availability are to be determined.

To view metrics in the portal:

1. Select an instance of [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)].
1. Select **Monitoring** > **Performance Dashboard**.

Monitoring is automatic, assuming that you meet all prerequisites.

:::image type="content" source="media/overview/performance-dashboard.png" alt-text="Screenshot of the performance dashboard for SQL Server enabled by Azure Arc." lightbox="media/overview/performance-dashboard.png":::

## Prerequisites

To collect monitoring data for [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] and view the performance metrics in Azure, you must meet the following conditions:

- The version of Azure Extension for SQL Server (`WindowsAgent.SqlServer`) is v1.1.2504.99 or later.
- [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] is running on the Windows operating system.

[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] running on [!INCLUDE [winserver2012-md](../../includes/winserver2012-md.md)] R2 and older versions aren't supported.

- [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] is a Standard or Enterprise edition.
- The SQL Server version is 2016 or later.
- The server has connectivity to `*.<region>.arcdataservices.com`. For more information, see the [network requirements](/azure/azure-arc/servers/network-requirements?tabs=azure-cloud).
- The license type on [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] is Software Assurance or pay-as-you-go.
- You have an Azure role with the action `Microsoft.AzureArcData/sqlServerInstances/getTelemetry/`. You can use the following built-in role, which includes this action: *Azure Hybrid Database Administrator - Read Only Service Role*. For more information, see [Azure built-in roles](/azure/role-based-access-control/built-in-roles).

### Current limitations

Failover cluster instances aren't supported at this time.

## Disable or enable collection

### Azure portal

- On the resource page for [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)], select **Performance Dashboard (preview)**.
- At the top of the **Performance Dashboard** pane, select **Configure**.
- On the **Configure monitoring settings** pane, use the toggle to turn off or turn on the collection of monitoring data.
- Select **Apply settings**.

### Azure CLI

To disable the collection of monitoring data for [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)], run the following command in the Azure CLI. Replace the placeholders for subscription ID, resource group, and resource name.

```azurecli
az resource update --ids "/subscriptions/<sub_id>/resourceGroups/<resource_group>/providers/Microsoft.AzureArcData/SqlServerInstances/<resource_name>" --set 'properties.monitoring.enabled=false' --api-version 2023-09-01-preview
```

To enable the collection of monitoring data for [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)], run the following command. Replace the placeholders for subscription ID, resource group, and resource name.

```azurecli
az resource update --ids "/subscriptions/<sub_id>/resourceGroups/<resource_group>/providers/Microsoft.AzureArcData/SqlServerInstances/<resource_name>" --set 'properties.monitoring.enabled=true' --api-version 2023-09-01-preview
```

The command to enable collection might run successfully, but the Azure portal will collect and show monitoring data only if you meet all the [prerequisites](#prerequisites) listed earlier.

## Collected data

The following lists reflect the monitoring data that the Azure portal collects from DMV datasets on [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] when you enable the monitoring feature. The portal doesn't collect any personal data or customer content.

[!INCLUDE [azure-arc-data-regions](includes/dmv-collection.md)]

## Related content

- [Use activity logs with SQL Server enabled by Azure Arc](activity-logs.md)
- [Data collection and reporting for SQL Server enabled by Azure Arc](data-collection.md)
- [System dynamic management views](../../relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)
