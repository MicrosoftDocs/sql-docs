---
title: Register with SQL Server IaaS Agent extension (Linux)
description: Learn how to register your SQL Server on Linux Azure Virtual Machines with the SQL Server IaaS Agent extension to enable Azure features, compliance, and improved manageability.
author: adbadram
ms.author: adbadram
ms.reviewer: mathoma, randolphwest
ms.date: 03/05/2026
ms.service: azure-vm-sql-server
ms.subservice: management
ms.topic: how-to
ms.custom:
  - devx-track-azurecli, devx-track-azurepowershell, linux-related-content
tags: azure-resource-manager
---
# Register a Linux SQL Server VM with the SQL Server IaaS Agent extension

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

> [!div class="op_single_selector"]
> - [Windows](../windows/sql-agent-extension-manually-register-single-vm.md)
> - [Linux](sql-iaas-agent-extension-register-vm-linux.md)

Register your SQL Server VM with the [SQL Server IaaS Agent extension](sql-server-iaas-agent-extension-linux.md) to enable additional management features for your SQL Server on Linux Azure VM.

## Overview

When you register with the extension, you create the **SQL virtual machine** resource within your subscription. This resource is separate from the virtual machine resource. When you unregister your SQL Server VM from the extension, you remove the **SQL virtual machine** resource but keep the actual virtual machine.

To use the extension, you must first [register your subscription with the **Microsoft.SqlVirtualMachine** provider](#register-your-subscription-with-the-resource-provider). This registration grants the extension the ability to create resources within that subscription.

> [!IMPORTANT]  
> The SQL Server IaaS Agent extension collects data to provide customers with optional benefits when using SQL Server in Azure Virtual Machines. Microsoft doesn't use this data for licensing audits without your advance consent. To learn more, see the [SQL Server privacy supplement](/sql/sql-server/sql-server-privacy#non-personal-data).

## Prerequisites

To register your SQL Server VM with the extension, you need:

- An [Azure subscription](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn).
- A Linux virtual machine running [SQL Server 2017 or a later version](https://www.microsoft.com/sql-server/sql-server-downloads) deployed in the public or Azure Government cloud.
- The latest version of [Azure CLI](/cli/azure/install-azure-cli) or [Azure PowerShell (5.0 minimum)](/powershell/azure/install-az-ps).

## Register your subscription with the resource provider

To register your SQL Server VM with the extension, first register your subscription with the **Microsoft.SqlVirtualMachine** resource provider. This registration grants the extension the ability to create resources within your subscription. You can register using the Azure portal, Azure CLI, or Azure PowerShell.

### Azure portal

Register your subscription with the resource provider by using the Azure portal:

1. Open the Azure portal and go to **All Services**.
1. Go to **Subscriptions** and select the subscription you want to use.
1. On the **Subscriptions** page, select **Resource providers** under **Settings**.
1. Enter **sql** in the filter to bring up the SQL-related resource providers.
1. Select **Register**, **Re-register**, or **Unregister** for the **Microsoft.SqlVirtualMachine** provider, depending on your desired action.

:::image type="content" source="../windows/media/sql-agent-extension-manually-register-single-vm/select-resource-provider-sql.png" alt-text="Screenshot of how to modify the provider." lightbox="../windows/media/sql-agent-extension-manually-register-single-vm/select-resource-provider-sql.png":::

### Command line

Register your Azure subscription with the **Microsoft.SqlVirtualMachine** provider using either Azure CLI or Azure PowerShell.

# [Azure CLI](#tab/bash)

Register your subscription with the resource provider by using Azure CLI:

```azurecli-interactive
az provider register --namespace Microsoft.SqlVirtualMachine
```

# [Azure PowerShell](#tab/powershell)

Register your subscription with the resource provider by using Azure PowerShell:

```powershell-interactive
Register-AzResourceProvider -ProviderNamespace Microsoft.SqlVirtualMachine
```

---

## Register VM

The extension is only available in lightweight mode on Linux, which supports only changing the license type and edition of SQL Server. Use the Azure CLI or Azure PowerShell to register your SQL Server VM with the extension in lightweight mode for limited functionality.

Provide the SQL Server license type as either pay-as-you-go (`PAYG`) to pay per usage, Azure Hybrid Benefit (`AHUB`) to allocate your own license, or disaster recovery (`DR`) to activate the [free DR replica license](../windows/business-continuity-high-availability-disaster-recovery-hadr-overview.md#free-dr-replica-in-azure).

# [Azure CLI](#tab/bash)

Register a SQL Server VM in lightweight mode with the Azure CLI:

```azurecli-interactive
# Register Enterprise or Standard self-installed VM in Lightweight mode
az sql vm create --name <vm_name> --resource-group <resource_group_name> --location <vm_location> --license-type <license_type>
```

# [Azure PowerShell](#tab/powershell)

Register a SQL Server VM in lightweight mode with Azure PowerShell:

1. Get the existing compute VM:

   ```powershell-interactive
   $vm = Get-AzVM -Name <vm_name> -ResourceGroupName <resource_group_name>
   ```

1. Register SQL VM with the extension:

   ```powershell-interactive
   New-AzSqlVM -Name $vm.Name -ResourceGroupName $vm.ResourceGroupName -Location $vm.Location `
     -LicenseType <license_type>
   ```

---

## Verify registration status

You can verify if your SQL Server VM is registered with the extension by using the Azure portal, the Azure CLI, or Azure PowerShell.

### Azure portal

Verify the registration status by using the Azure portal:

1. Sign in to the [Azure portal](https://portal.azure.com).
1. Go to your SQL virtual machines resource.
1. Select your SQL Server VM from the list. If your SQL Server VM isn't listed here, it likely isn't registered with the extension.

### Command line

Verify current SQL Server VM registration status using either Azure CLI or Azure PowerShell. `ProvisioningState` shows as `Succeeded` if registration was successful.

# [Azure CLI](#tab/bash)

Verify the registration status with Azure CLI:

```azurecli-interactive
az sql vm show -n <vm_name> -g <resource_group>
```

# [Azure PowerShell](#tab/powershell)

Verify the registration status with Azure PowerShell:

```powershell-interactive
Get-AzSqlVM -Name <vm_name> -ResourceGroupName <resource_group>
```

---

An error indicates that the SQL Server VM isn't registered with the extension.

## Automatic registration

[Automatic registration](../windows/sql-agent-extension-automatic-registration-all-vms.md) with the extension enables Azure to register all existing and future SQL Server VMs in a subscription with the extension, helping you maintain consistent management.

## Related content

- [Overview of SQL Server on Linux Azure Virtual Machines](sql-server-on-linux-vm-what-is-iaas-overview.md)
- [FAQ for SQL Server on Windows VMs](frequently-asked-questions-faq.yml)
- [Pricing guidance for SQL Server on Azure VMs](../windows/pricing-guidance.md)
- [What's new with SQL Server on Azure Virtual Machines?](../windows/doc-changes-updates-release-notes-whats-new.md)
