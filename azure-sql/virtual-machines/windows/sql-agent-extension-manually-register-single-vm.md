---
title: Register with SQL IaaS Agent Extension (Windows)
description: Learn how to register your SQL Server on Azure Windows VM with the SQL IaaS Agent extension to enable Azure features, for compliance, and improved manageability.
author: adbadram
ms.author: adbadram
ms.reviewer: mathoma, randolphwest
ms.date: 07/31/2023
ms.service: virtual-machines-sql
ms.subservice: management
ms.topic: how-to
ms.custom: devx-track-azurecli, devx-track-azurepowershell
tags: azure-resource-manager
---
# Register Windows SQL Server VM with SQL IaaS Agent extension

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

> [!div class="op_single_selector"]
> * [Windows](sql-agent-extension-manually-register-single-vm.md)
> * [Linux](../linux/sql-iaas-agent-extension-register-vm-linux.md)

Register your SQL Server VM with the [SQL IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md) to unlock a wealth of feature benefits for your SQL Server on Azure Windows VM.

This article teaches you to register a single SQL Server VM with the SQL IaaS Agent extension. Alternatively, you can register all SQL Server VMs in a subscription [automatically](sql-agent-extension-automatic-registration-all-vms.md) or [multiple VMs in bulk using a script](sql-agent-extension-manually-register-vms-bulk.md).

[!INCLUDE [SQL VM feature benefits](../../includes/sql-vm-iaas-extension-permissions.md)]

## Overview

Registering with the [SQL Server IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md) creates the [**SQL virtual machine** *resource*](manage-sql-vm-portal.md) within your subscription, which is a *separate* resource from the virtual machine resource. Unregistering your SQL Server VM from the extension removes the **SQL virtual machine** *resource* but won't drop the actual virtual machine.

Deploying a SQL Server VM Azure Marketplace image through the Azure portal automatically registers the SQL Server VM with the extension. However, if you choose to self-install SQL Server on an Azure virtual machine, or provision an Azure virtual machine from a custom VHD, then you must register your SQL Server VM with the SQL IaaS Agent extension to unlock full feature benefits and manageability. By default, Azure VMs that have SQL Server 2016 or later installed will be automatically registered with the SQL IaaS Agent extension when detected by the [CEIP service](/sql/sql-server/usage-and-diagnostic-data-configuration-for-sql-server).  See the [SQL Server privacy supplement](/sql/sql-server/sql-server-privacy#non-personal-data) for more information. For information about privacy, see the [SQL IaaS Agent extension privacy statements](sql-server-iaas-agent-extension-automate-management.md#in-region-data-residency).

To utilize the SQL IaaS Agent extension, you must first [register your subscription with the **Microsoft.SqlVirtualMachine** provider](#register-subscription-with-rp), which gives the SQL IaaS Agent extension the ability to create resources within that specific subscription. Then you can register your SQL Server VM with the extension.

## Prerequisites

To register your SQL Server VM with the extension, you'll need the following:

- An [Azure subscription](https://azure.microsoft.com/free/).
- An Azure Resource Model [supported](/lifecycle/products/?terms=windows%20server) [Windows Server virtual machine](/azure/virtual-machines/windows/quick-create-portal) with a [supported](/lifecycle/products/?terms=sql%20server) [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) version deployed to the public or Azure Government cloud.
- Ensure the Azure VM is running.
- The client credentials used to register the virtual machine exists in any of the following Azure roles: **Virtual Machine contributor**, **Contributor**, or **Owner**.
- The latest version of [Azure CLI](/cli/azure/install-azure-cli) or [Azure PowerShell (5.0 minimum)](/powershell/azure/install-az-ps).
- A minimum of .NET Framework 4.5.1 or later.
- To verify that none of the [limitations](sql-server-iaas-agent-extension-automate-management.md#limitations) apply to you.

## Register subscription with RP

To register your SQL Server VM with the SQL IaaS Agent extension, you must first register your subscription with the **Microsoft.SqlVirtualMachine** resource provider (RP). This gives the SQL IaaS Agent extension the ability to create resources within your subscription. You can do so by using the Azure portal, the Azure CLI, or Azure PowerShell.

### [Azure portal](#tab/azure-portal)

Register your subscription with the resource provider by using the Azure portal:

1. Open the Azure portal and go to **All Services**.
1. Go to **Subscriptions** and select the subscription of interest.
1. On the **Subscriptions** page, select **Resource providers** under **Settings**.
1. Enter **sql** in the filter to bring up the SQL-related resource providers.
1. Select **Register**, **Re-register**, or **Unregister** for the  **Microsoft.SqlVirtualMachine** provider, depending on your desired action.

   :::image type="content" source="./media/sql-agent-extension-manually-register-single-vm/select-resource-provider-sql.png" alt-text="Screenshot showing how to modify the provider.":::

### [Azure PowerShell](#tab/azure-powershell)

Register your subscription with the resource provider by using Azure PowerShell:

```powershell-interactive
# Register the SQL IaaS Agent extension to your subscription
Register-AzResourceProvider -ProviderNamespace Microsoft.SqlVirtualMachine
```

### [Azure CLI](#tab/azure-cli)

Register your subscription with the resource provider by using the Azure CLI:

```azurecli-interactive
# Register the SQL IaaS Agent extension to your subscription
az provider register --namespace Microsoft.SqlVirtualMachine
```

---

## Register with extension

You can manually register your SQL Server VM with the SQL IaaS Agent extension by using Azure PowerShell or the Azure CLI.

Provide the SQL Server license type as either pay-as-you-go (`PAYG`) to pay per usage, Azure Hybrid Benefit (`AHUB`) to use your own license, or disaster recovery (`DR`) to activate the [free DR replica license](business-continuity-high-availability-disaster-recovery-hadr-overview.md#free-dr-replica-in-azure).

### [Azure PowerShell](#tab/azure-powershell)

Register a SQL Server VM with Azure PowerShell:

```powershell-interactive
# Get the existing Compute VM
$vm = Get-AzVM -Name <vm_name> -ResourceGroupName <resource_group_name>

# Register SQL Server VM with the extension
New-AzSqlVM -Name $vm.Name -ResourceGroupName $vm.ResourceGroupName -Location $vm.Location `
-LicenseType <license_type>
```

### [Azure CLI](#tab/azure-cli)

Register a SQL Server with the Azure CLI:

```azurecli-interactive
# Register SQL Server VM with the extension
az sql vm create --name <vm_name> --resource-group <resource_group_name> --location <vm_location> --license-type <license_type>
```

### [Azure portal](#tab/azure-portal)

It's not currently possible to register your SQL Server VM with the SQL IaaS Agent extension by using the Azure portal.

---

## Verify registration status

You can verify if your SQL Server VM has already been registered with the SQL IaaS Agent extension by using the Azure portal, the Azure CLI, or Azure PowerShell.

### [Azure portal](#tab/azure-portal)

Verify the registration status with the Azure portal:

1. Sign in to the [Azure portal](https://portal.azure.com).
1. Go to your [SQL Server VMs](manage-sql-vm-portal.md).
1. Select your SQL Server VM from the list. If your SQL Server VM isn't listed here, it likely hasn't been registered with the SQL IaaS Agent extension.
1. View the value under **Status**. If **Status** is **Succeeded**, then the SQL Server VM has been registered with the SQL IaaS Agent extension successfully.

   :::image type="content" source="./media/sql-agent-extension-manually-register-single-vm/verify-registration-status.png" alt-text="Screenshot showing how to verify status with SQL RP registration.":::

Alternatively, you can check the status by choosing **Repair** under the **Support + troubleshooting** pane in the **SQL virtual machine** resource. The provisioning state for the SQL IaaS Agent extension can be **Succeeded** or **Failed**.

### [Azure PowerShell](#tab/azure-powershell)

Verify current SQL Server VM registration status by using Azure PowerShell. `ProvisioningState` shows as `Succeeded` if registration was successful.

Verify the registration status with Azure PowerShell:

```powershell-interactive
Get-AzSqlVM -Name <vm_name> -ResourceGroupName <resource_group>
```

#### [Azure CLI](#tab/azure-cli)

Verify current SQL Server VM registration status by using the Azure CLI.  `ProvisioningState` shows as `Succeeded` if registration was successful.

```azurecli-interactive
az sql vm show -n <vm_name> -g <resource_group>
```

---

An error indicates that the SQL Server VM hasn't been registered with the extension.

## Unregister from extension

To unregister your SQL Server VM with the SQL IaaS Agent extension, delete the SQL virtual machine *resource* using the Azure portal or Azure CLI. Deleting the SQL virtual machine *resource* doesn't delete the SQL Server VM.

> [!CAUTION]  
> **Use extreme caution** when unregistering your SQL Server VM from the extension. Follow the steps carefully because **it is possible to inadvertently delete the virtual machine** when attempting to remove the *resource*.

### [Azure portal](#tab/azure-portal)

Unregister your SQL Server VM from the extension using the Azure portal:

1. Sign into the [Azure portal](https://portal.azure.com).
1. Navigate to the SQL VM resource.

   :::image type="content" source="./media/sql-agent-extension-manually-register-single-vm/sql-vm-manage.png" alt-text="Screenshot of SQL virtual machines resource.":::

1. Select **Delete**.

   :::image type="content" source="./media/sql-agent-extension-manually-register-single-vm/delete-sql-vm-resource.png" alt-text="Screenshot showing how to select delete in the top navigation.":::

1. Type the name of the SQL virtual machine and **clear the check box next to the virtual machine**.

   :::image type="content" source="./media/sql-agent-extension-manually-register-single-vm/confirm-delete-of-resource-uncheck-box.png" alt-text="Screenshot showing how to uncheck the VM to prevent deleting the actual virtual machine, and then select Delete to proceed with deleting the SQL VM resource.":::

   > [!WARNING]  
   > Failure to clear the checkbox next to the virtual machine name will *delete* the virtual machine entirely. Clear the checkbox to unregister the SQL Server VM from the extension but *not delete the actual virtual machine*.

1. Select **Delete** to confirm the deletion of the SQL virtual machine *resource*, and not the SQL Server VM.

### [Azure PowerShell](#tab/azure-powershell)

To unregister your SQL Server VM from the extension with Azure PowerShell, use the [Remove-AzSqlVM](/powershell/module/az.sqlvirtualmachine/remove-azsqlvm) command. This removes the SQL Server VM *resource* but won't delete the virtual machine.

To unregister your SQL Server VM with Azure PowerShell:

```powershell-interactive
Remove-AzSqlVM -ResourceGroupName <resource_group_name> -Name <SQL VM resource name>
```

### [Azure CLI](#tab/azure-cli)

To unregister your SQL Server VM from the extension with the Azure CLI, use the [az sql vm delete](/cli/azure/sql/vm#az-sql-vm-delete) command. This removes the SQL Server VM *resource* but doesn't delete the virtual machine.

To unregister your SQL Server VM with the Azure CLI:

```azurecli-interactive
az sql vm delete
  --name <SQL VM resource name> |
  --resource-group <Resource group name> |
  --yes
```

---

## Next steps

- Review the benefits provided by the [SQL IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md).
- [Automatically register all VMs in a subscription](sql-agent-extension-automatic-registration-all-vms.md).
- [Troubleshoot known issues with the extension](sql-agent-extension-troubleshoot-known-issues.md).
- Review the [SQL IaaS Agent extension privacy statements](sql-server-iaas-agent-extension-automate-management.md#in-region-data-residency).
- Review the [best practices checklist](performance-guidelines-best-practices-checklist.md) to optimize for performance and security.

To learn more, review the following articles:

- [Overview of SQL Server on Windows VMs](sql-server-on-azure-vm-iaas-what-is-overview.md)
- [FAQ for SQL Server on Windows VMs](frequently-asked-questions-faq.yml)
- [Pricing guidance for SQL Server on Azure VMs](../windows/pricing-guidance.md)
- [What's new for SQL Server on Azure VMs](../windows/doc-changes-updates-release-notes-whats-new.md)
