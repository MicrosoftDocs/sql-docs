---
title: SQL Server IaaS Agent Extension for Linux
description: This article describes how the SQL Server IaaS Agent extension helps automate management specific administration tasks of SQL Server on Linux Azure VMs.
author: bluefooted
ms.author: pamela
ms.reviewer: mathoma, randolphwest
ms.date: 01/27/2026
ms.service: azure-vm-sql-server
ms.subservice: management
ms.topic: concept-article
ms.custom:
  - linux-related-content
tags: azure-resource-manager
---
# SQL Server IaaS Agent extension for Linux

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

> [!div class="op_single_selector"]
> - [Automate management with the Windows SQL Server IaaS Agent extension](../windows/sql-server-iaas-agent-extension-automate-management.md)
> - [SQL Server IaaS Agent extension for Linux](sql-server-iaas-agent-extension-linux.md)

Run the SQL Server IaaS Agent extension (SqlIaasExtension) on SQL Server on Linux Azure Virtual Machines (VMs) to automate management and administration tasks.

This article provides an overview of the extension. For more information, see [Register Linux SQL Server VM with SQL IaaS Agent extension](sql-iaas-agent-extension-register-vm-linux.md).

## Overview

The SQL Server IaaS Agent extension enables integration with the Azure portal and unlocks several benefits for SQL Server on Linux Azure VMs.

### Compliance

The extension offers a simplified method to fulfill the requirement of notifying Microsoft that the Azure Hybrid Benefit is enabled as specified in the product terms. This process negates the need to manage licensing registration forms for each resource.

### Simplified license management

The extension simplifies SQL Server license management, and allows you to quickly identify SQL Server VMs with the Azure Hybrid Benefit enabled by using the Azure portal, Azure PowerShell, or the Azure CLI.

### [PowerShell](#tab/azure-powershell)

```powershell-interactive
Get-AzSqlVM | Where-Object {$_.LicenseType -eq 'AHUB'}
```

### [Azure CLI](#tab/azure-cli)

```azurecli-interactive
az sql vm list --query "[?sqlServerLicenseType=='AHUB']"
```

---

### Free

There's no extra cost for using the extension.

## Manage license types at scale

[!INCLUDE [manage-sql-license-types-at-scale](../../../docs/includes/manage-sql-license-types-at-scale.md)]

## Installation

[Register](sql-iaas-agent-extension-register-vm-linux.md) your SQL Server VM with the SQL Server IaaS Agent extension to create the **SQL virtual machine** *resource* within your subscription. This resource is *separate* from the virtual machine resource. When you unregister your SQL Server VM from the extension, you remove the **SQL virtual machine** *resource* from your subscription but don't delete the actual virtual machine.

The SQL Server IaaS Agent extension for Linux is currently available with limited functionality.

## Verify extension status

Use the Azure portal or Azure PowerShell to check the status of the extension.

### Azure portal

Verify the extension is installed by using the Azure portal.

Go to your **Virtual machine** resource in the Azure portal (not the *SQL virtual machines* resource, but the resource for your VM). Select **Extensions** under **Settings**. You should see the **SqlIaasExtension** extension listed, as in the following example:

:::image type="content" source="../windows/media/sql-server-iaas-agent-extension-automate-management/azure-rm-sql-server-iaas-agent-portal.png" alt-text="Screenshot of Check the Status of the SQL Server IaaS Agent extension SqlIaaSExtension in the Azure portal." lightbox="../windows/media/sql-server-iaas-agent-extension-automate-management/azure-rm-sql-server-iaas-agent-portal.png":::

### Azure PowerShell

You can also use the **Get-AzVMSqlServerExtension** Azure PowerShell cmdlet:

```powershell-interactive
  Get-AzVMSqlServerExtension -VMName "vmname" -ResourceGroupName "resourcegroupname"
```

The previous command confirms that the agent is installed and provides general status information. You can get specific status information about automated backup and patching by using the following commands:

```powershell-interactive
$sqlext = Get-AzVMSqlServerExtension -VMName "vmname" -ResourceGroupName "resourcegroupname"
$sqlext.AutoPatchingSettings
$sqlext.AutoBackupSettings
```

## Limitations

The Linux SQL IaaS Agent extension has the following limitations:

- SQL Server with only a single instance. Multiple instances aren't supported.

<a id="in-region-data-residency"></a>

## Privacy statement

When using SQL Server on Azure VMs and the SQL IaaS Agent extension, consider the following privacy statements:

- **Data collection**: The SQL IaaS Agent extension collects data to provide optional benefits when using SQL Server on Azure Virtual Machines. Microsoft **does not use this data for licensing audits** without the customer's advance consent. For more information, see the [SQL Server privacy supplement](/sql/sql-server/sql-server-privacy#non-personal-data).

- **In-region data residency**: SQL Server on Azure VMs and SQL IaaS Agent Extension don't move or store customer data outside the region where you deploy the VMs.

## Related content

- [Overview of SQL Server on Linux Azure Virtual Machines](sql-server-on-linux-vm-what-is-iaas-overview.md)
- [Frequently asked questions for SQL Server on Linux virtual machines](frequently-asked-questions-faq.yml)
