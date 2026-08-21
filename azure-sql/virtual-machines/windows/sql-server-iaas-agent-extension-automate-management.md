---
title: What Is the SQL Server IaaS Agent Extension? (Windows)
description: This article describes how the SQL Server IaaS Agent extension helps automate management specific administration tasks of SQL Server on Azure Windows VMs. These include features such as automated backup, automated patching, Azure Key Vault integration, licensing management, storage configuration, and central management of all SQL Server VM instances.
author: dplessMSFT
ms.author: dpless
ms.reviewer: mathoma, randolphwest
ms.date: 05/06/2026
ms.service: azure-vm-sql-server
ms.subservice: management
ms.topic: concept-article
tags: azure-resource-manager
---
# Automate management with the Windows SQL Server IaaS Agent extension

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

> [!div class="op_single_selector"]
> - [Automate management with the Windows SQL Server IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md)
> - [SQL Server IaaS Agent extension for Linux](../linux/sql-server-iaas-agent-extension-linux.md)

The SQL Server IaaS Agent extension (`SqlIaasExtension`) runs on SQL Server on Azure Windows Virtual Machines (VMs) to automate management and administration tasks.

This article provides an overview of the extension. To install the SQL Server IaaS Agent extension to SQL Server on Azure VMs, see the articles for [Automatic registration](sql-agent-extension-automatic-registration-all-vms.md), [Register a single VM](sql-agent-extension-manually-register-single-vm.md), or [Register VMs in bulk](sql-agent-extension-manually-register-vms-bulk.md).

[!INCLUDE [sql-vm-deployment-failure](../../includes/sql-vm-deployment-failure.md)]

To learn more about the SQL Server on Azure VM deployment and management experience, watch the following Data Exposed videos:

- [Automate Management with the SQL Server IaaS Agent extension](/shows/data-exposed/azure-sql-vm-automate-management-with-the-sql-server-iaas-agent-extension-ep-2?WT.mc_id=dataexposed-c9-niner-mighub)
- [New and Improved SQL on Azure VM deployment and management experience](/shows/data-exposed/new-and-improved-sql-on-azure-vm-deployment-and-management-experience?WT.mc_id=dataexposed-c9-niner-mighub).

[!INCLUDE [unified-inventory](../../includes/sql-virtual-machines/unified-inventory.md)]

## Overview

The SQL Server IaaS Agent extension integrates with the Azure portal and unlocks several benefits for SQL Server on Azure VMs:

- **Feature benefits**: The extension unlocks automation feature benefits, such as portal management, license flexibility, automated backup, automated patching, and more. For details, see [Feature benefits](#feature-benefits).

- **Compliance**: The extension offers a simplified method to fulfill the requirement of notifying Microsoft that you enabled the Azure Hybrid Benefit, as specified in the product terms. This process negates the need to manage licensing registration forms for each resource.

- **Free**: The extension is free. There's no additional cost associated with the extension.

- **Integration with centrally managed Azure Hybrid Benefit**: SQL Server VMs registered with the extension can integrate with [Centrally managed Azure Hybrid Benefit](licensing-model-azure-hybrid-benefit-ahb-change.md#integration-with-centrally-managed-azure-hybrid-benefit), so it's easy to manage the Azure Hybrid Benefit for your SQL Server VMs at scale.

- **Simplified license management**: The extension simplifies SQL Server license management, and you can quickly identify SQL Server VMs with the Azure Hybrid Benefit enabled by using:

  ### [Azure portal](#tab/azure-portal)

  Use the [SQL Server virtual machines resource](manage-sql-vm-portal.md) in the Azure portal to quickly identify SQL Server VMs that use the Azure Hybrid Benefit.

  ### [PowerShell](#tab/azure-powershell)

  ```powershell-interactive
  Get-AzSqlVM | Where-Object {$_.LicenseType -eq 'AHUB'}
  ```

  ### [Azure CLI](#tab/azure-cli)

  ```azurecli-interactive
  az sql vm list --query "[?sqlServerLicenseType=='AHUB']"
  ```

  ---

Enable [auto upgrade](manage-sql-vm-portal.md#sql-iaas-agent-extension-settings) to ensure you get the latest updates to the extension each month.

## Feature benefits

The SQL Server IaaS Agent extension provides several feature benefits for managing your SQL Server VM. You can choose the benefits that best suit your business needs. When you first register with the extension, you get access to features that don't rely on the SQL IaaS Agent. When you enable a feature that requires the agent, the agent is installed on the SQL Server VM.

The following table lists the benefits you can get through the SQL IaaS Agent extension and whether the agent is required for each benefit:

[!INCLUDE [SQL VM feature benefits](../../includes/sql-vm-feature-benefits.md)]

## Permission models

By default, the SQL IaaS Agent extension uses the least privilege mode permission model. The least privilege permission model grants the minimum permissions required for each feature that you enable. Each feature that you use is assigned a custom role in SQL Server, and the custom role only has permissions that are required to perform actions related to the feature.

The following table defines the SQL Server permissions and custom roles used by each feature of the extension:

| Feature | Server and database permissions | Custom role (server / database) |
| --- | --- | --- |
| [Automated backups](automated-backup.md) | Server:<br />- `CONTROL SERVER`<br /><br />Database:<br />- `db_ddladmin` (`master`)<br />- `db_backupoperator` (`msdb`) | `SqlIaaSExtension_AutoBackup` |
| [Availability group portal management](availability-group-azure-portal-configure.md) | Server:<br />- `sysadmin` | |
| [Azure Backup Service](/azure/backup/backup-azure-sql-database#set-vm-permissions) | Server:<br />- `sysadmin` for `NT SERVICE\AzureWLBackupPluginSvc` account | |
| [Credential management](azure-key-vault-integration-configure.md) | Server:<br />- `CONTROL SERVER` | `SqlIaaSExtension_CredentialMgmt` |
| [I/O related best practices](sql-assessment-for-sql-vm.md) | Server:<br />- `CONTROL SERVER` | `SqlIaaSExtension_ThrottlingAssessment` |
| [SQL best practices assessment](sql-assessment-for-sql-vm.md) | Server:<br />- `CONTROL SERVER` | `SqlIaaSExtension_Assessment` |
| [SQL Server instance settings](manage-sql-vm-portal.md#license-and-edition) | Server:<br />- `ALTER ANY LOGIN`<br />- `ALTER SETTINGS` | `SqlIaaSExtension_SqlInstanceSetting` |
| [Storage configuration](storage-configuration.md) | Server:<br />- `ALTER ANY DATABASE` | `SqlIaaSExtension_StorageConfig` |
| [Status reporting](manage-sql-vm-portal.md#access-the-resource) | Server:<br />- `VIEW ANY DEFINITION`<br />- `VIEW SERVER STATE`<br />- `ALTER ANY LOGIN`<br />- `CONNECT SQL` | `SqlIaaSExtension_StatusReporting` |

> [!NOTE]
> SQL Server VMs provisioned *before October 2022* use the older `sysadmin` model by default. You can enable least privilege mode manually for these VMs. If this option isn't visible in your environment, your SQL Server VM already has least privilege mode enabled by default. 

To enable the least privilege permissions model, go to your [SQL Server virtual machine resource](manage-sql-vm-portal.md), choose **Security Configuration** under **Security**, and then select the **Enable least privilege mode** checkbox:

:::image type="content" source="media/sql-server-iaas-agent-extension-automate-management/least-privilege.png" alt-text="Screenshot of the Azure portal SQL virtual machines resource, Security Configuration page, enable least privilege highlighted." lightbox="media/sql-server-iaas-agent-extension-automate-management/least-privilege.png":::

## Installation

When you register your SQL Server VM with the SQL IaaS Agent extension, the process copies binaries to the virtual machine but doesn't install the agent by default. The agent installs only when you enable one of the [SQL IaaS Agent extension features](#feature-benefits) that require it. After installation, the following two services run on the virtual machine:

- **Microsoft SQL Server IaaS agent** is the main service for the SQL IaaS Agent extension. It runs under the **Local System** account.
- **Microsoft SQL Server IaaS Query Service** is a helper service that runs queries within SQL Server. It runs under the **NT Service** account `NT Service\SqlIaaSExtensionQuery`.

By default, the agent follows the principle of least privilege and only has permissions within SQL Server that are associated with the features you enable. However, if you manually install SQL Server on the VM yourself, or deploy a SQL Server image from the marketplace before October 2022, the agent has sysadmin rights within SQL Server.

When you deploy a SQL Server VM Azure Marketplace image through the Azure portal, it's automatically registered with the extension. However, if you choose to self-install SQL Server on an Azure virtual machine, or provision an Azure virtual machine from a custom VHD, you must register your SQL Server VM with the SQL IaaS Agent extension to unlock feature benefits. By default, self-installed Azure VMs with SQL Server 2016 or later automatically register with the SQL IaaS Agent extension when detected by the [CEIP service](/sql/sql-server/usage-and-diagnostic-data-configuration-for-sql-server). You should manually register SQL Server VMs that the CEIP doesn't detect.

You can register by using one of the following methods:

- [Automatically for all current and future VMs in a subscription](sql-agent-extension-automatic-registration-all-vms.md)
- [Manually for a single VM](sql-agent-extension-manually-register-single-vm.md)
- [Manually for multiple VMs in bulk](sql-agent-extension-manually-register-vms-bulk.md)

When you register your SQL Server VM with the SQL Server IaaS Agent extension, you create the **[SQL Server virtual machine](manage-sql-vm-portal.md)** *resource* within your subscription. This resource is *separate* from the virtual machine resource. If you delete the extension from your SQL Server VM, you remove the **SQL virtual machine** *resource* from your subscription but don't delete the underlying virtual machine.

### Multiple instance support

The SQL IaaS Agent extension supports the following environments:

- One default instance.
- Multiple instances, but only the default instance is supported and managed by the extension in the Azure portal. The extension doesn't support environments with multiple named instances without a default instance.
- One named instance, if it's the only installed instance.

### Named instance support

To manage a single named instance in the Azure portal, install SQL Server with a non-default name on an Azure virtual machine and then [register it with the SQL IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md).

To manage a single named instance in a SQL Server image from Azure Marketplace, uninstall the existing SQL Server instance, install SQL Server with a named instance, and then register it with the SQL IaaS Agent extension.

To use a single named instance with SQL Server on Azure VMs, follow these steps:

1. Deploy a SQL Server VM from Azure Marketplace.
1. [Delete the SQL IaaS Agent extension from the SQL Server VM](sql-agent-extension-manually-register-single-vm.md#delete-the-extension).
1. Connect to the virtual machine and uninstall SQL Server completely.
1. Restart the virtual machine.
1. Connect to the virtual machine and then use the setup media (typically located in `C:\SQLServerFull`) to install a [named SQL Server instance](/sql/sql-server/install/instance-configuration#options).
1. Restart the virtual machine.
1. [Register the VM with the SQL IaaS Agent Extension](sql-agent-extension-manually-register-single-vm.md#register-with-extension).

### Failover Clustered Instance support

Registering your SQL Server Failover Clustered Instance (FCI) is supported with limited functionality. Due to the limited functionality, SQL Server FCIs registered with the extension don't support features that require the agent, such as automated backup, patching, Microsoft Entra authentication, and advanced portal management.

If you register your SQL Server VM with the SQL IaaS Agent extension and enable any features that require the agent, you need to [delete the extension from the SQL Server VM](sql-agent-extension-manually-register-single-vm.md#delete-the-extension). Then register it again after your FCI is installed.

## Verify status of extension

Use the Azure portal, Azure PowerShell, or the Azure CLI to check the status of the extension.

### [Azure portal](#tab/azure-portal)

Verify the extension is installed in the Azure portal.

Go to your **Virtual machine** resource in the Azure portal (not the *SQL virtual machines* resource, but the resource for your VM). Select **Extensions** under **Settings**. You should see the **SqlIaasExtension** extension listed, as in the following screenshot:

:::image type="content" source="media/sql-server-iaas-agent-extension-automate-management/azure-rm-sql-server-iaas-agent-portal.png" alt-text="Screenshot from the Azure portal of the status of the SQL Server IaaS Agent extension." lightbox="media/sql-server-iaas-agent-extension-automate-management/azure-rm-sql-server-iaas-agent-portal.png":::

### [PowerShell](#tab/azure-powershell)

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

### [Azure CLI](#tab/azure-cli)

It's not currently possible to check the status of the extension by using the Azure CLI.

---

## Management modes

Before March 2023, the SQL IaaS Agent extension relied on management modes to define the security model, and unlock feature benefits. In March 2023, Microsoft updated the extension architecture to remove management modes entirely. Instead, it relies on the principle of least privilege to give you control over how you want to use the extension on a feature-by-feature basis.

Starting in March 2023, when you first register with the extension, it saves binaries to your virtual machine to provide basic functionality such as license management. When you enable any feature that relies on the agent, the extension uses the binaries to install the SQL IaaS Agent to your virtual machine. It also assigns [permissions](#permission-models) to the SQL IaaS Agent service as needed by each feature that you enable.

## Supported regions

The SQL IaaS Agent extension is supported in a limited set of Azure regions. You can only install the SQL IaaS Agent extension if your SQL Server VM is in a supported region. You can use Azure PowerShell to list the supported regions for the SQL IaaS Agent extension.

The following [Get-AzResourceProvider](/powershell/module/az.resources/get-azresourceprovider) Azure PowerShell command lists the supported regions:

```powershell-interactive
(Get-AzResourceProvider -ProviderNamespace Microsoft.SqlVirtualMachine).ResourceTypes |
      Where-Object { $_.ResourceTypeName -eq "SqlVirtualMachines" } |
      Select-Object -ExpandProperty Locations
```

## Limitations

The SQL IaaS Agent extension only supports:

- SQL Server VMs deployed through the Azure Resource Manager. It doesn't support SQL Server VMs deployed through the classic model.
- SQL Server VMs deployed to the public cloud, Azure Government cloud, and 21Vianet (Azure in China). It doesn't support deployments to other private or government clouds.
- A limited set of Azure [regions](#supported-regions). You can only install the SQL IaaS Agent extension if your SQL Server VM is in a supported region.
- TCP/IP must be enabled in SQL Server Configuration Manager and for the VM for the extension to work with your SQL Server on Azure VMs.
- SQL Server FCIs with limited functionality. SQL Server FCIs registered with the extension don't support features that require the agent, such as automated backup, patching, and advanced portal management.
- VMs with a default instance, or a single named instance when no default instance is present.
- If the VM has multiple named instances, then one of the instances must be the default instance to work with the SQL IaaS Agent extension.
- SQL Server instance images only. The SQL IaaS Agent extension doesn't support Reporting Services or Analysis services, such as the following images: SQL Server Reporting Services, Power BI Report Server, SQL Server Analysis Services.
- On SQL Server instances using binary collations (`BIN`/`BIN2`), databases and availability groups with trailing whitespace in their names are excluded by the extension. Rename or drop the affected objects to include them. On non-binary collations, trailing whitespace is automatically trimmed.

<a id="in-region-data-residency"></a>

## Privacy statements

When you use SQL Server on Azure VMs and the SQL IaaS Agent extension, consider the following privacy statements:

- **Automatic registration**: By default, Azure VMs with SQL Server 2016 or later are automatically registered with the SQL IaaS Agent extension when detected by the [CEIP service](/sql/sql-server/usage-and-diagnostic-data-configuration-for-sql-server). Review the [SQL Server privacy supplement](/sql/sql-server/sql-server-privacy#non-personal-data) for more information.

- **Data collection**: The SQL IaaS Agent extension collects data for the express purpose of giving customers optional benefits when using SQL Server on Azure Virtual Machines. Microsoft **will not use this data for licensing audits** without the customer's advance consent. See the [SQL Server privacy supplement](/sql/sql-server/sql-server-privacy#non-personal-data) for more information.

- **In-region data residency**: SQL Server on Azure VMs and the SQL IaaS Agent extension don't move or store customer data outside of the region where you deployed the VMs.

## Related tasks

To install the SQL Server IaaS extension to SQL Server on Azure VMs, see the following articles:

- [Automatic installation](sql-agent-extension-automatic-registration-all-vms.md)
- [Single VMs](sql-agent-extension-manually-register-single-vm.md)
- [VMs in bulk](sql-agent-extension-manually-register-vms-bulk.md)

For problem resolution, see [Troubleshoot known issues with the extension](sql-agent-extension-troubleshoot-known-issues.md).

## Related content

- [Updating SQL Server on Azure VMs](servicing-updates-guidelines.md)
- [What is SQL Server on Azure Windows Virtual Machines?](sql-server-on-azure-vm-iaas-what-is-overview.md)
- [FAQ for SQL Server on Windows VMs](frequently-asked-questions-faq.yml)
- [Pricing guidance for SQL Server on Azure VMs](pricing-guidance.md)
- [What's new with SQL Server on Azure Virtual Machines?](doc-changes-updates-release-notes-whats-new.md)
