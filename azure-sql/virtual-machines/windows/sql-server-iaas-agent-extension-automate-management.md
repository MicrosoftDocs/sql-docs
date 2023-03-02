---
title: What is the SQL Server IaaS Agent extension? (Windows)
description: This article describes how the SQL Server IaaS Agent extension helps automate management specific administration tasks of SQL Server on Azure Windows VMs. These include features such as automated backup, automated patching, Azure Key Vault integration, licensing management, storage configuration, and central management of all SQL Server VM instances.
author: adbadram
ms.author: adbadram
ms.reviewer: mathoma
ms.date: 03/26/2023
ms.service: virtual-machines-sql
ms.subservice: management
ms.topic: conceptual
ms.custom:
  - seo-lt-2019
  - ignite-fall-2021
tags: azure-resource-manager
---
# Automate management with the Windows SQL Server IaaS Agent extension
[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

> [!div class="op_single_selector"]
> * [Windows](sql-server-iaas-agent-extension-automate-management.md)
> * [Linux](../linux/sql-server-iaas-agent-extension-linux.md)


The SQL Server IaaS Agent extension (SqlIaasExtension) runs on SQL Server on Azure Windows Virtual Machines (VMs) to automate management and administration tasks. 

This article provides an overview of the extension. To install the SQL Server IaaS extension to SQL Server on Azure VMs, see the articles for [Automatic installation](sql-agent-extension-automatic-registration-all-vms.md), [Single VMs](sql-agent-extension-manually-register-single-vm.md),  or [VMs in bulk](sql-agent-extension-manually-register-vms-bulk.md). 

To learn more about the Azure VM deployment and management experience, including recent improvements, see:
- [Azure SQL VM: Automate Management with the SQL Server IaaS Agent extension (Ep. 2)](/shows/data-exposed/azure-sql-vm-automate-management-with-the-sql-server-iaas-agent-extension-ep-2?WT.mc_id=dataexposed-c9-niner-mighub)
- [Azure SQL VM: New and Improved SQL on Azure VM deployment and management experience (Ep.8) | Data Exposed](/shows/data-exposed/new-and-improved-sql-on-azure-vm-deployment-and-management-experience?WT.mc_id=dataexposed-c9-niner-mighub).

## Overview

The SQL Server IaaS Agent extension allows for integration with the Azure portal, and unlocks a number of benefits for SQL Server on Azure VMs: 

- **Feature benefits**: The extension unlocks a number of automation feature benefits, such as portal management, license flexibility, automated backup, automated patching and more. See [Feature benefits](#feature-benefits) later in this article for details. 

- **Compliance**: The extension offers a simplified method to fulfill the requirement of notifying Microsoft that the Azure Hybrid Benefit has been enabled as is specified in the product terms. This process negates needing to manage licensing registration forms for each resource.  

- **Free**: The extension is completely free. There's no additional cost associated with the extension. 

- **Integration with centrally managed Azure Hybrid Benefit**: SQL Server VMs registered with the extension can integrate with [Centrally managed Azure Hybrid Benefit](sql-agent-extension-automatic-registration-all-vms.md#integration-with-centrally-managed-azure-hybrid-benefit), making it easy manage the Azure Hybrid Benefit for your SQL Server VMs at scale. 

- **Simplified license management**: The extension simplifies SQL Server license management, and allows you to quickly identify SQL Server VMs with the Azure Hybrid Benefit enabled using: 

   ### [Azure portal](#tab/azure-portal)

   You can use the [SQL virtual machine resource](manage-sql-vm-portal.md) in the Azure portal to quickly identify SQL Server VMs that are using the Azure Hybrid Benefit. 

   ### [PowerShell](#tab/azure-powershell)

   ```powershell-interactive
   Get-AzSqlVM | Where-Object {$_.LicenseType -eq 'AHUB'}
   ```

   ### [Azure CLI](#tab/azure-cli)

   ```azurecli-interactive
   $ az sql vm list --query "[?sqlServerLicenseType=='AHUB']"
   ```
   ---

## Feature benefits 

The SQL Server IaaS Agent extension unlocks a number of feature benefits for managing your SQL Server VM, letting you pick and choose which benefit suits your business needs. 

The following table details the benefits available through the SQL IaaS agent extension: 

[!INCLUDE [SQL VM feature benefits](../../includes/sql-vm-feature-benefits.md)]


## Permissions models

There are two permission models for the SQL Server IaaS agent extension - either full sysadmin rights, or the principle of least privilege. The least privileged permission model grants the minimum permissions required for each feature used by the extension that a customer enables. Each feature that a customer uses is assigned a custom role, and the custom role only has the required permissions to perform actions related to the feature. 

The principle of least privilege model is enabled by default for SQL Server VMs deployed via Azure Marketplace after October 2022. Existing SQL Server VMs deployed prior to this date, or ones with SQL Server self-installed, use the sysadmin model by default and can enable the least privileged permissions model in the Azure portal. 

To enable the least privilege permissions model, go to your [SQL virtual machines resource](manage-sql-vm-portal.md), choose **Additional features** under **Settings** and then check the box next to **SQL IaaS extension least privilege mode**: 

:::image type="content" source="media/sql-server-iaas-agent-extension-automate-management/least-privilege.png" alt-text="Screenshot of the Azure portal SQL virtual machines resource, Additional features page, enable least privilege highlighted.":::


The following table defines the permissions and custom roles used by each feature of the extension: 

|Feature  |Permissions  |Custom role (Server / DB)  |
|---------|---------|---------|
|[SQL best practices assessment](sql-assessment-for-sql-vm.md) | Server permission - CONTROL SERVER         | SqlIaaSExtension_Assessment   |
|[Automated backups](automated-backup.md) |  Server permission - CONTROL SERVER  </br> Database permission -   `db_ddladmin` on master,  `db_backupoperator` on msdb  | SqlIaaSExtension_AutoBackup         |
|[Azure Backup Service](/azure/backup/backup-overview) | sysadmin         |         |
|[Credential management](azure-key-vault-integration-configure.md)  | Server permission - CONTROL SERVER|SqlIaaSExtension_CredentialMgmt          |
|[Availability group portal management](manage-sql-vm-portal.md#high-availability-preview) |sysadmin|         |
|[R Service](/sql/machine-learning/r/sql-server-r-services)| Server permission - ALTER SETTINGS        | SqlIaaSExtension_RService         |
|[SQL authentication](manage-sql-vm-portal.md#security-configuration) | sysadmin        |         |
|[SQL Server instance settings](manage-sql-vm-portal.md#license-and-edition)|Server permission - ALTER ANY LOGIN, ALTER SETTINGS | SqlIaaSExtension_SqlInstanceSetting          |
|[Storage configuration](storage-configuration.md)|Server permission - ALTER ANY DATABASE|SqlIaaSExtension_StorageConfig       |
|[Status reporting](manage-sql-vm-portal.md#access-the-resource) |Server permission - VIEW ANY DEFINITION, VIEW SERVER STATE, ALTER ANY LOGIN, CONNECT SQL         | SqlIaaSExtension_StatusReporting          |


## Installation

When you register your SQL Server VM with the SQL IaaS agent extension, binaries are copied to the VM. Once you enable a feature that relies on it, the SQL IaaS agent extension is installed to the VM and has access to SQL Server. By default, the agent follows the model of least privilege, and only has permissions within SQL Server that are associated with the features that you enable - unless you manually installed SQL Server to the VM yourself, or deployed a SQL Server image from the marketplace prior to October 2022, in which case the agent has sysadmin rights within SQL Server. 

Deploying a SQL Server VM Azure Marketplace image through the Azure portal automatically registers the SQL Server VM with the extension. However, if you choose to self-install SQL Server on an Azure virtual machine, or provision an Azure virtual machine from a custom VHD, then you must register your SQL Server VM with the SQL IaaS extension to unlock feature benefits. By default, self-installed Azure VMs with SQL Server 2016 or later are automatically registered with the SQL IaaS Agent extension when detected by the [CEIP service](/sql/sql-server/usage-and-diagnostic-data-configuration-for-sql-server). SQL Server VMs not detected by the CEIP should be manually registered. 

When you register with the SQL IaaS agent extension, two services run on the virtual machine: 

- **Microsoft SQL Server IaaS agent** is the main service for the SQL IaaS agent extension and should run under the **Local System** account. 
- **Microsoft SQL Server IaaS Query Service** is a helper service that helps the extension run queries within SQL Server and should run under the **NT Service** account `NT Service\SqlIaaSExtensionQuery`. 

There are three ways to register with the extension: 

- [Automatically for all current and future VMs in a subscription](sql-agent-extension-automatic-registration-all-vms.md)
- [Manually for a single VM](sql-agent-extension-manually-register-single-vm.md)
- [Manually for multiple VMs in bulk](sql-agent-extension-manually-register-vms-bulk.md)

Registering your SQL Server VM with the SQL Server IaaS Agent extension creates the [**SQL virtual machine** _resource_](manage-sql-vm-portal.md) within your subscription, which is a _separate_ resource from the virtual machine resource. Unregistering your SQL Server VM from the extension removes the **SQL virtual machine** _resource_ from your subscription but won't drop the underlying virtual machine.


### Named instance support

The SQL Server IaaS Agent extension works with a named instance of SQL Server if it's the only SQL Server instance available on the virtual machine. If a VM has multiple named SQL Server instances and no default instance, then the SQL IaaS extension registers in lightweight mode and pick either the instance with the highest edition, or the first instance, if all the instances have the same edition. 

To use a named instance of SQL Server, deploy an Azure virtual machine, install a single named SQL Server instance to it, and then register it with the [SQL IaaS Extension](sql-agent-extension-manually-register-single-vm.md).

Alternatively, to use a named instance with an Azure Marketplace SQL Server image, follow these steps: 

   1. Deploy a SQL Server VM from Azure Marketplace. 
   1. [Unregister](sql-agent-extension-manually-register-single-vm.md#unregister-from-extension) the SQL Server VM from the SQL IaaS Agent extension. 
   1. Uninstall SQL Server completely within the SQL Server VM.
   1. Install SQL Server with a named instance within the SQL Server VM. 
   1. [Register the VM with the SQL IaaS Agent Extension](sql-agent-extension-manually-register-single-vm.md#register-with-extension). 

## Verify status of extension

Use the Azure portal, Azure PowerShell or the Azure CLI to check the status of the extension. 

### [Azure portal](#tab/azure-portal)

Verify the extension is installed in the Azure portal. 

Go to your **Virtual machine** resource in the Azure portal (not the *SQL virtual machines* resource, but the resource for your VM). Select **Extensions** under **Settings**.  You should see the **SqlIaasExtension** extension listed, as in the following example: 

![Status of the SQL Server IaaS Agent extension in the Azure portal](./media/sql-server-iaas-agent-extension-automate-management/azure-rm-sql-server-iaas-agent-portal.png)

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

## Limitations

The SQL IaaS Agent extension only supports: 

- SQL Server VMs deployed through the Azure Resource Manager. SQL Server VMs deployed through the classic model aren't supported. 
- SQL Server VMs deployed to the public or Azure Government cloud. Deployments to other private or government clouds aren't supported. 
- Failover cluster instances (FCIs) in lightweight mode. 
- Named instances with multiple instances on a single VM in lightweight mode. 

## <a id="in-region-data-residency"></a> Privacy statements

When using SQL Server on Azure VMs and the SQL IaaS extension, consider the following privacy statements: 

- **Automatic registration**: By default, Azure VMs with SQL Server 2016 or later are automatically registered with the SQL IaaS Agent extension when detected by the [CEIP service](/sql/sql-server/usage-and-diagnostic-data-configuration-for-sql-server). Review the [SQL Server privacy supplement](/sql/sql-server/sql-server-privacy#non-personal-data) for more information.

- **Data collection**: The SQL IaaS Agent extension collects data for the express purpose of giving customers optional benefits when using SQL Server on Azure Virtual Machines. Microsoft **will not use this data for licensing audits** without the customer's advance consent. See the [SQL Server privacy supplement](/sql/sql-server/sql-server-privacy#non-personal-data) for more information.

- **In-region data residency**: SQL Server on Azure VMs and the SQL IaaS Agent Extension don't move or store customer data out of the region in which the VMs are deployed.

## Management modes

Prior to March 2023, the SQL Iaas Agent extension relied on management modes to define the security model, and unlock certain benefits. In March 2023, the extension architecture was updated to remove management modes entirely, instead relying on the principle of least privilege to give customers control over how they want to use the extension on a feature by feature basis. 

Starting in March 2023, newly deployed SQL Server VMs are registered with the extension all using the same default configuration and least privileged security model. Permissions are assigned to the SQL IaaS agent as features are enabled. 

## Next steps

To install the SQL Server IaaS extension to SQL Server on Azure VMs, see the articles for [Automatic installation](sql-agent-extension-automatic-registration-all-vms.md), [Single VMs](sql-agent-extension-manually-register-single-vm.md), or [VMs in bulk](sql-agent-extension-manually-register-vms-bulk.md). For problem resolution, read [Troubleshoot known issues with the extension](sql-agent-extension-troubleshoot-known-issues.md).

To learn more, review the following articles:

* [Overview of SQL Server on a Windows VM](sql-server-on-azure-vm-iaas-what-is-overview.md)
* [FAQ for SQL Server on a Windows VM](frequently-asked-questions-faq.yml)
* [Pricing guidance for SQL Server on a Azure VMs](../windows/pricing-guidance.md)
* [What's new for SQL Server on Azure VMs](../windows/doc-changes-updates-release-notes-whats-new.md)
