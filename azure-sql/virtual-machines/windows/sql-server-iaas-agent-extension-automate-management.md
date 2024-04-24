---
title: What is the SQL Server IaaS Agent extension? (Windows)
description: This article describes how the SQL Server IaaS Agent extension helps automate management specific administration tasks of SQL Server on Azure Windows VMs. These include features such as automated backup, automated patching, Azure Key Vault integration, licensing management, storage configuration, and central management of all SQL Server VM instances.
author: ebruersan
ms.author: ebrue
ms.reviewer: mathoma
ms.date: 10/16/2023
ms.service: virtual-machines-sql
ms.subservice: management
ms.topic: conceptual
tags: azure-resource-manager
---
# Automate management with the Windows SQL Server IaaS Agent extension
[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

> [!div class="op_single_selector"]
> * [Windows](sql-server-iaas-agent-extension-automate-management.md)
> * [Linux](../linux/sql-server-iaas-agent-extension-linux.md)


The SQL Server IaaS Agent extension (SqlIaasExtension) runs on SQL Server on Azure Windows Virtual Machines (VMs) to automate management and administration tasks. 

This article provides an overview of the extension. To install the SQL Server IaaS Agent extension to SQL Server on Azure VMs, see the articles for [Automatic registration](sql-agent-extension-automatic-registration-all-vms.md), [Register single VMs](sql-agent-extension-manually-register-single-vm.md),  or [Register VMs in bulk](sql-agent-extension-manually-register-vms-bulk.md). 

To learn more about the Azure VM deployment and management experience, including recent improvements, see:

- [Azure SQL VM: Automate Management with the SQL Server IaaS Agent extension (Ep. 2)](/shows/data-exposed/azure-sql-vm-automate-management-with-the-sql-server-iaas-agent-extension-ep-2?WT.mc_id=dataexposed-c9-niner-mighub)
- [Azure SQL VM: New and Improved SQL on Azure VM deployment and management experience (Ep.8) | Data Exposed](/shows/data-exposed/new-and-improved-sql-on-azure-vm-deployment-and-management-experience?WT.mc_id=dataexposed-c9-niner-mighub).


## Overview

The SQL Server IaaS Agent extension allows for integration with the Azure portal, and unlocks a number of benefits for SQL Server on Azure VMs: 

- **Feature benefits**: The extension unlocks a number of automation feature benefits, such as portal management, license flexibility, automated backup, automated patching and more. See [Feature benefits](#feature-benefits) later in this article for details. 

- **Compliance**: The extension offers a simplified method to fulfill the requirement of notifying Microsoft that the Azure Hybrid Benefit has been enabled as is specified in the product terms. This process negates needing to manage licensing registration forms for each resource.  

- **Free**: The extension is completely free. There's no additional cost associated with the extension. 

- **Integration with centrally managed Azure Hybrid Benefit**: SQL Server VMs registered with the extension can integrate with [Centrally managed Azure Hybrid Benefit](licensing-model-azure-hybrid-benefit-ahb-change.md#integration-with-centrally-managed-azure-hybrid-benefit), making it easy manage the Azure Hybrid Benefit for your SQL Server VMs at scale. 

- **Simplified license management**: The extension simplifies SQL Server license management, and allows you to quickly identify SQL Server VMs with the Azure Hybrid Benefit enabled using: 

   ### [Azure portal](#tab/azure-portal)

   You can use the [SQL virtual machines resource](manage-sql-vm-portal.md) in the Azure portal to quickly identify SQL Server VMs that are using the Azure Hybrid Benefit. 

   ### [PowerShell](#tab/azure-powershell)

   ```powershell-interactive
   Get-AzSqlVM | Where-Object {$_.LicenseType -eq 'AHUB'}
   ```

   ### [Azure CLI](#tab/azure-cli)

   ```azurecli-interactive
   $ az sql vm list --query "[?sqlServerLicenseType=='AHUB']"
   ```
   ---

Enable [auto upgrade](manage-sql-vm-portal.md#sql-iaas-agent-extension-settings) to ensure you're getting the latest updates to the extension each month. 

## Feature benefits

The SQL Server IaaS Agent extension unlocks a number of feature benefits for managing your SQL Server VM, letting you pick and choose which benefit suits your business needs. When you first register with the extension, the functionality is limited to a few features that don't rely on the SQL IaaS Agent. Once you enable a feature that requires it, the agent is installed to the SQL Server VM. 

The following table details the benefits available through the SQL IaaS Agent extension, and whether or not the agent is required: 

[!INCLUDE [SQL VM feature benefits](../../includes/sql-vm-feature-benefits.md)]

## Permissions models

By default, the SQL IaaS Agent extension uses the least privilege mode permission model. The least privilege permission model grants the minimum permissions required for each feature that you enable. Each feature that you use is assigned a custom role in SQL Server, and the custom role is only granted permissions that are required to perform actions related to the feature.

SQL Server VMs deployed prior to October 2022 use the older sysadmin model where the SQL IaaS Agent extension takes sysadmin rights by default. For SQL Server VMs provisioned before October 2022, you can enable the least privilege permissions model by going to your [SQL virtual machines resource](manage-sql-vm-portal.md), choosing **Security Configuration** under **Security** and then checking the box next to **Enable least privilege mode**.

To enable the least privilege permissions model, go to your [SQL virtual machines resource](manage-sql-vm-portal.md), choose **Security Configuration** under **Security** and then check the box next to **Enable least privilege mode**: 

:::image type="content" source="media/sql-server-iaas-agent-extension-automate-management/least-privilege.png" alt-text="Screenshot of the Azure portal SQL virtual machines resource, Security Configuration page, enable least privilege highlighted.":::

The following table defines the permissions and custom roles used by each feature of the extension:

|Feature  |Permissions  |Custom role (Server / DB)  |
|---------|---------|---------|
|[SQL best practices assessment](sql-assessment-for-sql-vm.md) | Server permission - CONTROL SERVER | SqlIaaSExtension_Assessment   |
|[Automated backups](automated-backup.md) |  Server permission - CONTROL SERVER  <br /> Database permission - `db_ddladmin` on `master`,  `db_backupoperator` on `msdb` | SqlIaaSExtension_AutoBackup |
|[Azure Backup Service](/azure/backup/backup-overview) | sysadmin | |
|[Credential management](azure-key-vault-integration-configure.md)  | Server permission - CONTROL SERVER|SqlIaaSExtension_CredentialMgmt |
|[Availability group portal management](manage-sql-vm-portal.md#high-availability) |sysadmin| |
|[R Service](/sql/machine-learning/r/sql-server-r-services)| Server permission - ALTER SETTINGS        | SqlIaaSExtension_RService |
|[SQL authentication](manage-sql-vm-portal.md#security-configuration) | sysadmin | |
|[SQL Server instance settings](manage-sql-vm-portal.md#license-and-edition)|Server permission - ALTER ANY LOGIN, ALTER SETTINGS | SqlIaaSExtension_SqlInstanceSetting |
|[Storage configuration](storage-configuration.md)|Server permission - ALTER ANY DATABASE|SqlIaaSExtension_StorageConfig       |
|[Status reporting](manage-sql-vm-portal.md#access-the-resource) |Server permission - VIEW ANY DEFINITION, VIEW SERVER STATE, ALTER ANY LOGIN, CONNECT SQL | SqlIaaSExtension_StatusReporting |

## Installation

When you register your SQL Server VM with the SQL IaaS Agent extension, binaries are copied to the VM. Once you enable a feature that relies on it, the SQL IaaS Agent extension is installed to the VM and has access to SQL Server. By default, the agent follows the model of least privilege, and only has permissions within SQL Server that are associated with the features that you enable - unless you manually installed SQL Server to the VM yourself, or deployed a SQL Server image from the marketplace prior to October 2022, in which case the agent has sysadmin rights within SQL Server. 

Deploying a SQL Server VM Azure Marketplace image through the Azure portal automatically registers the SQL Server VM with the extension. However, if you choose to self-install SQL Server on an Azure virtual machine, or provision an Azure virtual machine from a custom VHD, then you must register your SQL Server VM with the SQL IaaS Agent extension to unlock feature benefits. By default, self-installed Azure VMs with SQL Server 2016 or later are automatically registered with the SQL IaaS Agent extension when detected by the [CEIP service](/sql/sql-server/usage-and-diagnostic-data-configuration-for-sql-server). SQL Server VMs not detected by the CEIP should be manually registered. 

When you register with the SQL IaaS Agent extension, binaries are copied to the virtual machine, but the agent is not installed by default. The agent will only be installed when you enable one of the [SQL IaaS Agent extension features](#feature-benefits) that require it, and the following two services will then run on the virtual machine: 

- **Microsoft SQL Server IaaS agent** is the main service for the SQL IaaS Agent extension and should run under the **Local System** account. 
- **Microsoft SQL Server IaaS Query Service** is a helper service that helps the extension run queries within SQL Server and should run under the **NT Service** account `NT Service\SqlIaaSExtensionQuery`. 

There are three ways to register with the extension: 

- [Automatically for all current and future VMs in a subscription](sql-agent-extension-automatic-registration-all-vms.md)
- [Manually for a single VM](sql-agent-extension-manually-register-single-vm.md)
- [Manually for multiple VMs in bulk](sql-agent-extension-manually-register-vms-bulk.md)

Registering your SQL Server VM with the SQL Server IaaS Agent extension creates the [**SQL virtual machine** _resource_](manage-sql-vm-portal.md) within your subscription, which is a _separate_ resource from the virtual machine resource. Unregistering your SQL Server VM from the extension removes the **SQL virtual machine** _resource_ from your subscription but won't drop the underlying virtual machine.

### Multiple instance support

The SQL IaaS Agent extension only works on virtual machines with multiple instances if there is a default instance. When you register your virtual machine with the SQL IaaS Agent extension, it registers the default instance, and that's the instance you'll be able to manage from the Azure portal. 

The SQL IaaS Agent extension does not support virtual machines with multiple named instances if there is no default instance. 


### Named instance support

The SQL IaaS Agent extension works with a named instance of SQL Server if it's the only SQL Server instance available on the virtual machine. The SQL IaaS Agent extension does not support VMs with multiple named instances. 

To use a named instance of SQL Server, deploy an Azure virtual machine, install a single named SQL Server instance to it, and then register it with the [SQL IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md).

Alternatively, to use a named instance with an Azure Marketplace SQL Server image, follow these steps: 

   1. Deploy a SQL Server VM from Azure Marketplace. 
   1. [Unregister the SQL Server VM from the SQL IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md#unregister-from-extension). 
   1. Uninstall SQL Server completely within the SQL Server VM.
   1. Restart the virtual machine. 
   1. Install SQL Server with a named instance within the SQL Server VM. 
   1. Restart the virtual machine. 
   1. [Register the VM with the SQL IaaS Agent Extension](sql-agent-extension-manually-register-single-vm.md#register-with-extension). 

### Failover Clustered Instance support

Registering your SQL Server Failover Clustered Instance (FCI) is supported with limited functionality. Due to the limited functionality, SQL Server FCIs registered with the extension do not support features that require the agent, such as automated backup, patching, Microsoft Entra authentication and advanced portal management. 

If your SQL Server VM has already been registered with the SQL IaaS Agent extension and you've enabled any features that require the agent, you'll need to [unregister the SQL Server VM from the extension](sql-agent-extension-manually-register-single-vm.md#unregister-from-extension) and register it again after your FCI is installed.

## Verify status of extension

Use the Azure portal, Azure PowerShell or the Azure CLI to check the status of the extension. 

### [Azure portal](#tab/azure-portal)

Verify the extension is installed in the Azure portal. 

Go to your **Virtual machine** resource in the Azure portal (not the *SQL virtual machines* resource, but the resource for your VM). Select **Extensions** under **Settings**.  You should see the **SqlIaasExtension** extension listed, as in the following example: 

:::image type="content" source="./media/sql-server-iaas-agent-extension-automate-management/azure-rm-sql-server-iaas-agent-portal.png" alt-text="Screenshot from the Azure portal of the status of the SQL Server IaaS Agent extension.":::

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

Before March 2023, the SQL IaaS Agent extension relied on management modes to define the security model, and unlock feature benefits. In March 2023, the extension architecture was updated to remove management modes entirely, instead relying on the principle of least privilege to give customers control over how they want to use the extension on a feature-by-feature basis. 

Starting in March 2023, when you first register with the extension, binaries are saved to your virtual machine to provide you with basic functionality such as license management. Once you enable any feature that relies on the agent, the binaries are used to install the SQL IaaS Agent to your virtual machine, and [permissions](#permissions-models) are assigned to the SQL IaaS Agent service as needed by each feature that you enable. 

## Limitations

The SQL IaaS Agent extension only supports: 

- SQL Server VMs deployed through the Azure Resource Manager. SQL Server VMs deployed through the classic model aren't supported. 
- SQL Server VMs deployed to the public or Azure Government cloud. Deployments to other private or government clouds aren't supported. 
- SQL Server FCIs with limited functionality. SQL Server FCIs registered with the extension do not support features that require the agent, such as automated backup, patching, and advanced portal management. 
- VMs with a default instance, or a single named instance. 
- If the VM has multiple named instances, then one of the instances must be the default instance to work with the SQL IaaS Agent extension. 
- SQL Server instance images only. The SQL IaaS Agent extension does not support Reporting Services or Analysis services, such as the following images: SQL Server Reporting Services, Power BI Report Server, SQL Server Analysis Services. 

## <a id="in-region-data-residency"></a> Privacy statements

When using SQL Server on Azure VMs and the SQL IaaS Agent extension, consider the following privacy statements: 

- **Automatic registration**: By default, Azure VMs with SQL Server 2016 or later are automatically registered with the SQL IaaS Agent extension when detected by the [CEIP service](/sql/sql-server/usage-and-diagnostic-data-configuration-for-sql-server). Review the [SQL Server privacy supplement](/sql/sql-server/sql-server-privacy#non-personal-data) for more information.

- **Data collection**: The SQL IaaS Agent extension collects data for the express purpose of giving customers optional benefits when using SQL Server on Azure Virtual Machines. Microsoft **will not use this data for licensing audits** without the customer's advance consent. See the [SQL Server privacy supplement](/sql/sql-server/sql-server-privacy#non-personal-data) for more information.

- **In-region data residency**: SQL Server on Azure VMs and the SQL IaaS Agent extension don't move or store customer data out of the region in which the VMs are deployed.

## Related content

To install the SQL Server IaaS extension to SQL Server on Azure VMs, see the articles for [Automatic installation](sql-agent-extension-automatic-registration-all-vms.md), [Single VMs](sql-agent-extension-manually-register-single-vm.md), or [VMs in bulk](sql-agent-extension-manually-register-vms-bulk.md). For problem resolution, read [Troubleshoot known issues with the extension](sql-agent-extension-troubleshoot-known-issues.md).

To learn more, review the following articles:

- [Overview of SQL Server on Windows VMs](sql-server-on-azure-vm-iaas-what-is-overview.md)
- [FAQ for SQL Server on Windows VMs](frequently-asked-questions-faq.yml)
- [Pricing guidance for SQL Server on Azure VMs](../windows/pricing-guidance.md)
- [What's new for SQL Server on Azure VMs](../windows/doc-changes-updates-release-notes-whats-new.md)
