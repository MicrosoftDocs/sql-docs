---
title: Unified Inventory for SQL Server on Azure VMs (preview)
description: Learn about unified inventory for SQL Server on Azure VMs, which lets you view your SQL Server VMs and SQL Server enabled by Azure Arc instances together.
author: lcwright
ms.author: lancewright
ms.date: 03/18/2026
ms.service: azure-vm-sql-server
ms.subservice: management
ms.topic: concept-article
ai-usage: ai-assisted
ms.custom: references_regions
monikerRange: ">=azuresql-vm"
---

# Unified inventory for SQL Server on Azure Virtual Machines (preview)

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article describes the unified inventory for SQL Server on Azure Virtual Machines (VMs). The unified inventory introduces the **SQL Server instances** resource to bring together an inventory of SQL Server instances on your Azure VMs and Arc-enabled machines into a single, consistent inventory model.

> [!IMPORTANT]
> The unified inventory for SQL Server on Azure VMs is currently in [preview](doc-changes-updates-release-notes-whats-new.md#preview).

## Overview

Previously, managing SQL Server on Azure VMs and SQL Server on-premises or in other clouds (with Azure Arc) used two separate experiences:

- **[SQL virtual machines](manage-sql-vm-portal.md) resource**: SQL Server instances on Azure VMs with management through the SQL IaaS Agent extension.
- **[SQL Server - Azure Arc](/sql/sql-server/azure-arc/assess) resource**: SQL Server enabled by Azure Arc instances on machines outside Azure with management through the Azure Arc agent and Azure extension for SQL Server.

The **unified inventory** merges these two approaches to introduce the [SQL Server instance](https://portal.azure.com/#servicemenu/SqlAzureExtension/AzureSqlHub/SqlServerInstance) resource. The **SQL Server instance** resource brings consistency to viewing SQL Server on Azure VM instances and SQL Server enabled by Azure Arc instances in the Azure portal.

This unified resource model means that every SQL Server instance, regardless of where it physically exists, is individually visible in the Azure portal:

:::image type="content" source="media/unified-inventory-sql-vm/unified-inventory.png" alt-text="Screenshot of the SQL Server instances resource in the Azure portal.":::

While you can view all your SQL Server instances in one place, SQL Server VM management is available through the [SQL virtual machines](manage-sql-vm-portal.md) resource.

### Key benefits

The following table lists the key benefits of the unified inventory for SQL Server on Azure VMs:

| Benefit | Description |
|---------|-------------|
| **Multiple instance support** | Unified inventory automatically discovers and displays all SQL Server instances on a single Azure VM. You're no longer limited to one SQL Server instance per VM. |
| **Consistent resource model** | SQL Server instances on Azure VMs now appear as first-class Azure resources, just like SQL Server enabled by Azure Arc instances. This consistent resource model enables uniform Azure Policy, Azure Resource Graph queries, and role-based access control (RBAC) across your entire SQL Server estate. |
| **Unified Azure portal experience** | SQL Server instance management provides a seamless experience regardless of whether your SQL Server is running on an Azure VM or an Arc-enabled machine outside of Azure. |
| **Per-instance management** | You can independently monitor and govern each SQL Server instance. This capability provides granular control for customers running multiple SQL Server workloads on a single VM. |
| **Azure Policy management** | By exposing instance-level resources in Azure Resource Graph, you can create and apply Azure Policy definitions by using instance-level properties (version, edition, authentication mode, and so on) for SQL Server on Azure VMs. |
| **Database inventory** | The experience automatically discovers and displays all databases on each SQL Server instance as Azure resources. You have visibility into database-level properties such as compatibility level, recovery model, and encryption status. |

## Supported features

Unified inventory supports the following capabilities for each SQL Server instance on your Azure VM:

- **Automatic discovery:** When you install the extension on your Azure VM, it automatically discovers all SQL Server instances and databases and surfaces them in the Azure portal.
- **Dynamic inventory:** As you add or remove SQL Server instances and databases on a registered Azure VM, the changes appear in the Azure portal and Azure CLI.
- **Instance properties:** View key information about each instance, including:
  - Instance name
  - SQL Server version (including patch level and build number)
  - Edition (Enterprise, Standard, Developer, or Express)
  - Authentication mode
- **Database properties:** View key information about each database on each SQL Server VM instance, including:
  - Database name
  - Compatibility level
  - Recovery model
  - Encryption status

> [!NOTE]
> To make changes to SQL Server VM [features](sql-server-iaas-agent-extension-automate-management.md#feature-benefits), such as automated backup or automated patching, continue to use the [SQL virtual machines](manage-sql-vm-portal.md) resource. Features for the SQL Server instance resource are not currently available for SQL Server on Azure VMs.

## Prerequisites

Before you use the unified inventory, ensure the following prerequisites are met:

- Your SQL Server on Azure VM has the [SQL IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md) version `2.0.226.0` or later installed.
- A [system-assigned managed identity](/entra/identity/managed-identities-azure-resources/how-to-configure-managed-identities#enable-system-assigned-managed-identity-on-an-existing-vm) is provisioned for the virtual machine.
- The [Microsoft.AzureArcData resource provider](/azure/azure-arc/data/plan-azure-arc-data-services) is registered in your subscription.
- Outbound connectivity to Azure Arc Data Processing Service and telemetry endpoints. For more information, see [Network requirements for SQL Server enabled by Azure Arc](/sql/sql-server/azure-arc/prerequisites#network-requirements). Missing connectivity might cause inventory not to appear even if other prerequisites are met.
- The SQL virtual machine resource is in a region that supports unified inventory.

### Supported regions

The following regions currently support unified inventory:

#### [Americas](#tab/americas)

- Brazil South
- Canada Central
- Canada East
- Central US
- East US
- East US 2
- North Central US
- South Central US
- West Central US
- West US
- West US 2
- West US 3

#### [Europe, the Middle East, and Africa](#tab/emea)

- France Central
- North Europe
- Norway East
- South Africa North
- Sweden Central
- Switzerland North
- UAE North
- UK South
- UK West
- West Europe

#### [Asia Pacific](#tab/asia)

- Australia East
- Central India
- Japan East
- Korea Central
- Southeast Asia

---

## Set up unified inventory

You can use unified inventory when your SQL Server on Azure VM has the SQL IaaS Agent extension version `2.0.226.0` or later installed. You can check the version of the extension by using the Azure portal, Azure CLI, or PowerShell.

### [Azure portal](#tab/portal)

To check the version of the SQL IaaS Agent extension by using the Azure portal, follow these steps:

1. Go to your [Virtual Machine](https://portal.azure.com/#view/Microsoft_Azure_ComputeHub/ComputeHubMenuBlade/~/virtualMachinesBrowse) in the Azure portal.
1. Under **Settings**, select **Extensions + applications**.
1. Verify that **SqlIaasExtension** appears in the list, has a version of `2.0.226.0` or later, and shows a status of **Provisioning succeeded**.

If you need to update the extension, select the radio button next to the **SqlIaasExtension** name and then select **Update** from the command bar:

:::image type="content" source="media/unified-inventory-sql-vm/update-extension.png" alt-text="Screenshot of updating the SQL IaaS Agent extension from the virtual machine pane in the Azure portal." lightbox="media/unified-inventory-sql-vm/update-extension.png":::

If **Update** is grayed out, you have the latest version of the extension, or the latest version isn't yet available in your region. Check back later or contact support for assistance.

### [Azure CLI](#tab/cli)

To check the version of your extension, use the [az vm extension show](/cli/azure/vm/extension#az-vm-extension-show) command with the name `SqlIaasExtension`.

The following example shows how to check the extension version by using the Azure CLI:

```azurecli
az vm extension show \
  --resource-group <resource-group> \
  --vm-name <vm-name> \
  --name SqlIaasExtension
```

To update the extension to the latest version, use the [az vm extension set](/cli/azure/vm/extension#az-vm-extension-set) command with the name `SqlIaasExtension` and omit the `--version` parameter.

The following example shows how to update the extension to the latest version by using the Azure CLI:

```azurecli
az vm extension set \
  --resource-group <resource-group> \
  --vm-name <vm-name> \
  --name SqlIaasExtension \
  --publisher Microsoft.SqlServer.Management
```

### [PowerShell](#tab/powershell)

To check the version of your extension, use the [Get-AzVMExtension](/powershell/module/az.compute/get-azvmextension) cmdlet with the name `SqlIaasExtension`.

The following example shows how to check the version of your extension by using PowerShell:

```azurepowershell
Get-AzVMExtension -ResourceGroupName <resource-group> `
  -VMName <vm-name> `
  -Name SqlIaasExtension
```

To update the extension to the latest version, use the [Set-AzVMExtension](/powershell/module/az.compute/set-azvmextension) cmdlet with the name `SqlIaasExtension` and without the `-Version` parameter.

The following example shows how to update the extension to the latest version by using PowerShell:

```azurepowershell
Set-AzVMExtension -ResourceGroupName <resource-group> `
  -VMName <vm-name> `
  -Name SqlIaasExtension `
  -Publisher Microsoft.SqlServer.Management
```

---

## Inventory discovery enabled by default

Beginning with SQL IaaS Agent extension version `2.0.226.0`, inventory discovery is enabled by default. When you upgrade to version `2.0.226.0` or later, the extension automatically discovers and surfaces SQL Server instance and database inventory to the Azure portal unless you explicitly opt out.

### Turn inventory on or off

You can turn inventory discovery on or off by using the Azure CLI 

The following Azure CLI command disables inventory discovery:

```azurecli
az vm extension set \
  --resource-group "[resourceGroupName]" \
  --vm-name "[vmResourceName]" \
  --name "[SqlInstanceName]" \
  --publisher "Microsoft.SqlServer.Management" \
  --version "2.0" \
  --settings '{
    "FeatureFlags": [{"Name": "InventoryUploadOnAzureVM", "Enable": false}]
  }'
```

The following Azure CLI command enables inventory discovery:

```azurecli
az vm extension set \
  --resource-group "[resourceGroupName]" \
  --vm-name "[vmResourceName]" \
  --name "[SqlInstanceName]" \
  --publisher "Microsoft.SqlServer.Management" \
  --version "2.0" \
  --settings '{
    "FeatureFlags": [{"Name": "InventoryUploadOnAzureVM", "Enable": true}]
  }'
```

### Cleanup behavior

Turning off inventory doesn't delete previously discovered SQL Server instances or databases from the Azure portal. To remove previously discovered resources, you must delete them manually.

You can delete previously discovered resources by using the following methods:

- **Azure portal**: Delete individual SQL Server instance resources from the portal.
- **Azure CLI**: Use existing Azure CLI commands for bulk deletion of SQL Server instances or databases.

The following Azure CLI command deletes a SQL Server instance and all its child database resources:
```azurecli
   az resource delete -g {resource_group} \
     -n {instance_name} \
     --resource-type "Microsoft.AzureArcData/SqlServerInstances" 
```


The following Azure CLI command only deletes a specific database resource:

```azurecli
   az resource delete -g {resource_group} \
     -n {database_name} \
     --resource-type "Microsoft.AzureArcData/SqlServerInstances/databases" \
     --parent "sqlServerInstances/{instance_name}" 
```

## View SQL Server instances

When the extension is updated to support unified inventory, it automatically discovers your SQL Server instances and databases. You can view your instances in the Azure portal.

Select a specific instance to: 
- View instance properties and status.
- View databases and their properties.

To view your instances through the unified inventory in the Azure portal, follow these steps: 

1. Go to the [Azure SQL hub at aka.ms/azuresqlhub](https://aka.ms/azuresqlhub).
1. Under **SQL Server**, select **SQL Server instances** to open the **SQL Server instances** pane.
1. On the **SQL Server instances** pane, view a list of all your SQL Server instances across Azure VMs and Arc-enabled machines. The list includes key information such as instance name, resource group, location, and host resource type.
1. Select a specific instance to view instance properties, status, and databases.
    - Under **Settings**, select **Properties** to view specific information about the instance.
    - Under **Data management**, select **Databases** to view a list of databases on the instance. Select a specific database to view properties such as compatibility level, recovery model, and encryption status.

Alternatively, you can view your SQL Server instance information from the **Virtual machines** resource by following these steps:

1. Go to your [Virtual Machine](https://portal.azure.com/#view/Microsoft_Azure_ComputeHub/ComputeHubMenuBlade/~/virtualMachinesBrowse) in the Azure portal.
1. Under **Settings**, select **SQL Server configuration**.
1. The **Installed SQL Server instances** section displays a table with:
   - Instance name (clickable to navigate to the instance resource)
   - Version
   - Edition
   - Service type
1. (Optional) Select **Manage** to go to the [SQL virtual machines resource](manage-sql-vm-portal.md) for SQL Server instance management options.

## Navigate between experiences

Use the [Azure SQL hub at aka.ms/azuresqlhub](https://aka.ms/azuresqlhub) to navigate between the unified inventory of SQL Server instances and the [SQL virtual machines resource](manage-sql-vm-portal.md) for management options listed under **SQL Server on Azure VMs**.

## Use Azure Policy with SQL Server instances

By using the unified resource model, you can write [Azure Policy](/azure/governance/policy/overview) definitions that target individual SQL Server instances on both Azure VMs and Azure Arc-enabled machines by using the `Microsoft.AzureArcData/sqlServerInstances` resource type. 

For example, you can:

- Enforce that all SQL Server instances run a minimum version.
- Require Microsoft Defender for SQL to be enabled on all instances.
- Audit authentication modes across your SQL Server estate.

```json
{
  "if": {
    "allOf": [
      {
        "field": "type",
        "equals": "Microsoft.AzureArcData/sqlServerInstances"
      },
      {
        "field": "Microsoft.AzureArcData/sqlServerInstances/version",
        "less": "16.0"
      }
    ]
  },
  "then": {
    "effect": "audit"
  }
}
```

## Query instances with Azure Resource Graph

Use [Azure Resource Graph](/azure/governance/resource-graph/overview) to query all your SQL Server instances across Azure VMs and Arc-enabled servers in a single query. 

To query your instance with Azure Resource Graph, use the [Azure Resource Graph Explorer](https://portal.azure.com/#servicemenu/Microsoft_Azure_Resources/ResourceManager/resourcegraphexplorer) in the Azure portal or the [az graph query](/cli/azure/graph#az-graph-query) command in the Azure CLI.

Use KQL queries with the `Microsoft.AzureArcData/sqlServerInstances` resource type to query instance properties such as version, edition, authentication mode, and more.

## Limitations

The unified inventory for SQL Server instances has the following limitations:

- Some management features that exist for the SQL virtual machines resource aren't yet available for the SQL Server instance resources.
- Feature availability might differ for named instances versus the default instance during the preview period.
- Not all SQL VM-specific features (such as storage configuration) are currently supported for secondary or named instances.

## Related content

- [SQL Server on Azure Virtual Machines overview](sql-server-on-azure-vm-iaas-what-is-overview.md)
- [Azure Arc-enabled SQL Server overview](/sql/sql-server/azure-arc/overview)
- [SQL IaaS Agent extension overview](sql-server-iaas-agent-extension-automate-management.md)
- [Register SQL Server VM with the SQL IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md)
- [Azure Policy for SQL Server](/azure/azure-arc/servers/policy-reference)
