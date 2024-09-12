---
title: Configure zone redundancy
description: Configure zone redundancy for your Azure SQL Managed Instance by using the Azure portal, PowerShell, Azure CLI, and REST API. 
author: Stralle
ms.author: strrodic
ms.reviewer: urmilano, mathoma, randolphwest
ms.date: 06/15/2024
ms.service: azure-sql-managed-instance
ms.subservice: high-availability
ms.topic: conceptual
---
# Configure zone redundancy - Azure SQL Managed Instance

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article teaches you to configure [zone redundancy](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability) Azure SQL Managed Instance by using the Azure portal, PowerShell, Azure CLI, and REST API.

By using a zone-redundant configuration, you can make your Business Critical or General Purpose instances highly available and resilient to a much larger set of failures, including catastrophic datacenter outages, without any changes to the application logic. You can convert any existing Business Critical or General Purpose instances to the zone-redundant configuration.

> [!NOTE]  
> Zone-redundancy is currently in public preview for the General Purpose service tier.

## Considerations

Consider the following when using zone redundancy for SQL Managed Instance:

- Zone redundancy is available in [select regions](#supported-regions).
- Zone redundancy is only available to instances that have enabled the [November 2022 feature wave](november-2022-feature-wave-enroll.md).
- Zone redundancy is currently in public preview for the General Purpose service tier and not currently available for the Next-gen General Purpose service tier.
- Zone redundancy can be enabled, and disabled. The operation to enable or disable zone redundancy is a fully online [scaling operation](../database/scale-resources.md) executed in the background.
- To enable zone redundancy, your SQL managed instance **Backup storage redundancy** must use _Zone-redundant_ or _Geo-zone-redundant_ storage.

## New instance

You can create a new zone-redundant instance by using the Azure portal, PowerShell, Azure CLI, and REST API.

### [Azure portal](#tab/azure-portal)

To configure a new zone-redundant SQL managed instance in the Azure portal, follow these steps:

1. Go to the [Azure SQL](https://portal.azure.com/#view/HubsExtension/BrowseResource/resourceType/Microsoft.Sql%2Fazuresql) page in the Azure portal.
1. On the **Azure SQL** page, select **+ Create** to open the **Select SQL deployment option** page.
1. On the **Select SQL deployment option** page, choose **Single instance** in the **Resource type** dropdown list on the **SQL managed instances** tile and then select **Create** to open the **Create Azure SQL Managed Instance** page.
1. On the **Basics** tab of the **Create Azure SQL Managed Instance** page, select **Configure Managed Instance** under **Compute + storage** to open the **Compute + storage** page.
1. On the **Compute + storage page**:
    1. For **Backup storage redundancy** under **Backup**, choose `Zone-redundant` or `Geo-zone-redundant` backup storage. Backups have to be configured before you can enable zone redundancy.
    1. For **Zone redundancy** under **Compute Hardware**, choose **Enabled**.
    1. Configure the remaining instance settings based on your business needs and then use **Apply** to save your configuration and go back to the **Create Azure SQL Managed Instance** page.

   :::image type="content" source="media/instance-zone-redundancy-configure/instance-new-portal-compute-storage.png" alt-text="Screenshot of the backup and zone redundancy options selected on the compute + storage page of the Azure portal." lightbox="media/instance-zone-redundancy-configure/instance-new-portal-compute-storage.png":::

1. On the **Create Azure SQL Managed Instance** page, configure the remaining instance settings based on your business needs, select **Review + Create** to review your settings, and then use **Create** to deploy your instance configured with zone redundancy. For more information about deploying a managed instance, review [Create Azure SQL Managed Instance](instance-create-quickstart.md).

### [PowerShell](#tab/powershell)

To create a new zone-redundant SQL managed instance by using PowerShell, use the `-ZoneRedundant` switch when using the [New-AzSqlInstance](/powershell/module/az.sql/new-azsqlinstance) command. For a full PowerShell sample, review [Create Azure SQL Managed Instance with PowerShell](scripts/create-configure-managed-instance-powershell.md).

Omit `-ZoneRedundant` if you don't want your new SQL managed instance to be zone redundant.

### [Azure CLI](#tab/azure-cli)

To create a new zone-redundant SQL managed instance by using the Azure CLI, set the `--zone-redundant` parameter to `true` when using the [az sql mi create](/cli/azure/sql/mi#az-sql-mi-create) command. For a full Azure CLI sample, review [Create Azure SQL Managed Instance with the Azure CLI](scripts/create-configure-managed-instance-cli.md).

Set `--zone-redundant` to `false` if you don't want your new SQL managed instance to be zone redundant.

### [REST API](#tab/rest-api)

To create a new zone-redundant SQL managed instance by using the REST API, set the `zoneRedundant` parameter to `true` when using the [Managed Instances - Create Or Update](/rest/api/sql/managed-instances/create-or-update) command.

Set `zoneRedundant` to `false` if you don't want your new SQL managed instance to be zone redundant.

---

## Existing instance

You can enable or disable zone redundancy for an existing SQL managed instance by using the Azure portal, PowerShell, Azure CLI, and REST API.

### [Azure portal](#tab/azure-portal)

To update your zone redundancy configuration for an existing SQL managed instance by using the Azure portal, follow these steps.

1. Go to your [SQL managed instance](https://portal.azure.com/#view/HubsExtension/BrowseResource/resourceType/Microsoft.Sql%2FmanagedInstances) resource in the Azure portal.
1. On the **Compute + storage** pane:
    1. To enable zone redundancy, first ensure the **Backup storage redundancy** under **Backup** is set to `Zone-redundant` or `Geo-zone-redundant`. If it's not already, choose your new backup storage redundancy option and apply your settings. Wait for the operation to complete, and then refresh your page before enabling zone redundancy.
    1. Under **Compute Hardware**, use the **Zone redundancy** toggle to either enable or disable zone redundancy.
  
   :::image type="content" source="media/instance-zone-redundancy-configure/instance-existing-portal-compute-storage.png" alt-text="Screenshot of the compute + storage page for an existing instance in the Azure portal with zone redundancy and backups selected. " lightbox="media/instance-zone-redundancy-configure/instance-existing-portal-compute-storage.png":::

### [PowerShell](#tab/powershell)

To update an existing SQL managed instance to be zone-redundant by using PowerShell, use the `-ZoneRedundant` switch when using the [Set-AzSqlInstance](/powershell/module/az.sql/set-azsqlinstance) command. For a full PowerShell sample, review [Create Azure SQL Managed Instance with PowerShell](scripts/create-configure-managed-instance-powershell.md).

Omit `-ZoneRedundant` if you want to disable zone redundancy for your existing SQL managed instance.

### [Azure CLI](#tab/azure-cli)

To update an existing SQL managed instance to be zone-redundant by using the Azure CLI, set the `--zone-redundant` parameter to `true` when using the [az sql mi update](/cli/azure/sql/mi#az-sql-mi-update) command. For a full Azure CLI sample, review [Create Azure SQL Managed Instance with the Azure CLI](scripts/create-configure-managed-instance-cli.md).

Set `--zone-redundant` to `false` if you want to disable zone redundancy for your existing SQL managed instance.

### [REST API](#tab/rest-api)

To update an existing SQL managed instance to be zone-redundant by using the REST API, set the `zoneRedundant` parameter to `true` when using the [Managed Instances - Update](/rest/api/sql/managed-instances/update) command.

Set `zoneRedundant` to `false` if you want to disable zone redundancy for your existing SQL managed instance.

---

## Check zone redundancy

You can check the current zone redundancy setting for your SQL managed instance by using the Azure portal, PowerShell, Azure CLI, and the REST API.

### [Azure portal](#tab/azure-portal)

To check the zone redundancy configuration for an existing SQL managed instance by using the Azure portal, follow these steps.

1. Go to your [SQL managed instance](https://portal.azure.com/#view/HubsExtension/BrowseResource/resourceType/Microsoft.Sql%2FmanagedInstances) resource in the Azure portal.
1. On the **Compute + storage** page under settings, check the **Zone redundancy** toggle in the **Compute Hardware** section.

### [PowerShell](#tab/powershell)

To check the zone redundancy configuration for an existing SQL managed instance, validate the existence, or absence, of the `Zone-redundant` switch when using the [Get-AzSqlInstance](/powershell/module/az.sql/get-azsqlinstance) PowerShell command.

If the `Zone-redundant` switch is visible, zone redundancy is enabled.

### [Azure CLI](#tab/azure-cli)

To check the zone redundancy configuration for an existing SQL managed instance, validate what the `--zone-redundant` parameter is set to when using the [az sql mi show](/cli/azure/sql/mi#az-sql-mi-show) PowerShell command.

Zone redundancy is enabled if `--zone-redundant` is set to `true`.

### [REST API](#tab/rest-api)

To check the zone redundancy configuration for an existing SQL managed instance, validate what the`zoneRedundant` parameter is set to when using the [Managed Instances - Get](/rest/api/sql/managed-instances/get) REST API command

Zone redundancy is enabled if `zoneRedundant` is set to `true`.

---

## Supported regions

Zone redundancy for SQL Managed Instance varies for instances in the Business Critical and General Purpose service tier: 

### [Business Critical service tier](#tab/bc)

Zone redundancy for Business Critical SQL Managed Instance is supported in the following regions:

| Americas | Europe | Middle East and Africa | Asia Pacific |
| --- | --- | --- | --- | --- |
| Brazil South | France Central | Israel Central | Australia East |
| Canada Central | Germany West Central | Qatar Central | Central India |
| Central US | Italy North | South Africa North | East Asia |
| East US | North Europe | UAE North | Japan West |
| East US 2 | Norway East | | Korea Central |
| Mexico Central | Poland Central | | New Zealand North |
| West Central US | Spain Central | | Southeast Asia |
| West US 2 | Sweden Central | | |
| West US 3 | Switzerland North | | |
| | UK South | | |
| | West Europe | | |

### [General Purpose service tier](#tab/gp)

> [!NOTE]  
> Zone-redundant configuration is in public preview for the General Purpose service tier.

| Americas | Europe | Middle East and Africa | Asia Pacific |
| --- | --- | --- | --- | --- |
| Brazil South | France Central | Israel Central | Australia East |
| Canada Central | Germany West Central | Qatar Central | Central India |
| East US | Italy North | South Africa North | East Asia |
| East US 2 | North Europe | UAE North | Japan West |
| Mexico Central | Norway East | | Korea Central |
| West Central US | Poland Central | | New Zealand North |
| West US 2 | Spain Central | | Southeast Asia |
| West US 3 | Sweden Central | | |
| | Switzerland North | | |
| | UK South | | |
| | West Europe | | |

---

## Related content

To learn more, review: 

- [High availability for Azure SQL Managed Instance](high-availability-sla-local-zone-redundancy.md)
- [Continuity scenarios](business-continuity-high-availability-disaster-recover-hadr-overview.md)
- [Automated backups](automated-backups-overview.md)
- [Restore a database from the service-initiated backups](recovery-using-backups.md)
- [Failover groups](failover-group-sql-mi.md)
- [Geo-restore](recovery-using-backups.md#point-in-time-restore)

