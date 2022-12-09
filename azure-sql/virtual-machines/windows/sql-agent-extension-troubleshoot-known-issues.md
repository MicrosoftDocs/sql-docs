---
title: Known issues & troubleshooting the SQL Server IaaS agent extension
description: Learn about the known issues and how to troubleshoot errors with the SQL Server Iaas Agent extension. 
author: adbadram
ms.author: adbadram
ms.reviewer: mathoma
ms.date: 12/12/2022
ms.service: virtual-machines-sql
ms.subservice: management
ms.topic: how-to
ms.custom:
---
# Known issues & troubleshooting the SQL Server IaaS agent extension
[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article helps you resolve known issues and troubleshoot errors when using the [SQL Server IaaS agent extension](sql-server-iaas-agent-extension-automate-management.md). 

For answers to frequently asked questions about the extension, check out the [FAQ](frequently-asked-questions-faq.yml#sql-server-iaas-agent-extension). 

## Check prerequisites 

To avoid errors due to unsupported options or limitations, verify the [prerequisites](sql-agent-extension-manually-register-single-vm.md#prerequisites) for the extension. 

## Not valid state for management

[Repair the extension](sql-agent-extension-manually-register-single-vm.md#repair-extension) if you see the following error message: 

`The SQL virtual machine resource is not in a valid state for management`

## Underlying virtual machine is invalid

If you see the following error message: 

`SQL management operations are disabled because the state of underlying virtual machine is invalid`

Consider the following:

- The SQL VM may be stopped, deallocated, in a failed state, or not found. Validate the underlying virtual machine is running. 
- Your SQL IaaS extension may be in a failed state. [Repair the extension](sql-agent-extension-manually-register-single-vm.md#repair-extension). 

[Unregister your SQL VM from the extension](sql-agent-extension-manually-register-single-vm.md#unregister-from-extension) and then register the SQL VM with the extension again if you did any of the following:

- Migrated your VM from one subscription to the other.
- Changed the locale or collation of SQL Server. 
- Changed the version of your SQL Server instance. 
- Changed the edition of your SQL Server instance. 

## Provisioning failed 

[Repair the extension](sql-agent-extension-manually-register-single-vm.md#repair-extension) if the SQL IaaS extension status shows as **Provisioning failed** in the Azure portal. 

## SQL VM resource unavailable in portal 

If the SQL IaaS extension is installed, and the VM is online, but the SQL VM resource is unavailable in the Azure portal, verify that your SQL Server and SQL Browser service are started within the VM. If this doesn't resolve the issue, [repair the extension](sql-agent-extension-manually-register-single-vm.md#repair-extension). 

## Features are grayed out

If you navigate to your [SQL VM resource](manage-sql-vm-portal.md) in the Azure portal, and there are features that are grayed out, verify that the SQL VM is running, and that you have the extension registered in [Full mode](sql-server-iaas-agent-extension-automate-management.md#management-modes). 

Grayed out features are expected if your SQL VM is registered in lightweight mode as the extension doesn't offer full capability, and many features are [unavailable](sql-server-iaas-agent-extension-automate-management.md#feature-benefits). 

Upgrade your extension to full mode to gain access to any unavailable features. Lightweight mode is the default mode for SQL Server instances that participate in a failover cluster instance (FCI) and can't be upgraded, so some features will always be grayed out. 

## Changing service account

Changing the service accounts for either of the two services associated with the extension can cause the extension to fail or behave unpredictably. 

The two services should run under the following accounts:

- **Microsoft SQL Server IaaS agent** is the main service for the SQL IaaS agent extension and should run under the **Local System** account. 
- **Microsoft SQL Server IaaS Query Service** is a helper service that helps the extension run queries within SQL Server and should run under the **NT Service** account `NT Service\SqlIaaSExtensionQuery`. 


## Automatic registration failed

If you have a few SQL Server VMs that failed to [register automatically](sql-agent-extension-automatic-registration-all-vms.md), check the version of SQL Server on the VMs that failed to register. By default, Azure VMs with SQL Server 2016 or later are automatically registered with the SQL IaaS agent extension when detected by the [CEIP service](/sql/sql-server/usage-and-diagnostic-data-configuration-for-sql-server). SQL Server VMs that have versions earlier than 2016 have to be manually registered [individually](sql-agent-extension-manually-register-single-vm.md) or [in bulk](sql-agent-extension-manually-register-vms-bulk.md).

## High resource consumption 

If you notice that the SQL IaaS agent extension is consuming unexpectedly high CPU or memory, verify the extension is on the latest version. If so, restart **Microsoft SQL Server IaaS Agent** from services.msc. 

## Error upgrading to full 

If you receive an error when attempting to upgrade your extension mode from lightweight to full, check to see if your SQL Server is installed as a failover cluster instance (FCI). SQL Server FCI only supports registering with the extension in lightweight mode. 

## Can't extend disks 

Extending your disks from the **Storage Configuration** page of the [SQL VM resource](manage-sql-vm-portal.md) is unavailable under the following conditions: 

- If you uninstall and reinstall the SQL IaaS agent extension. 
- If you uninstall and reinstall your instance of SQL Server. 
- If you used custom naming conventions for the disk/storage pool name when deploying your SQL Server image from the Azure Marketplace. 
- If your extension is registered in lightweight mode. 

## Disk configuration grayed out during deployment

If you create your SQL Server VM by using an unmanaged disk, disk configuration is grayed out by design. 

## Automated backup disabled

If your [SQL VM resource](manage-sql-vm-portal.md) displays **Automated backup is currently disabled**, check to see if your SQL Server instance has [managed backups](/sql/relational-databases/backup-restore/enable-sql-server-managed-backup-to-microsoft-azure) enabled. To use Automated backups from the Azure portal, disable managed backups in SQL Server. 

## Extension stuck in transition 

Your SQL IaaS agent extension may get stuck in a transitioning state in the following scenarios:

- You've removed the `NT service\SQLIaaSExtension` service from the SQL Server logins and/or the local administrator's group.
- Either of these two services are stopped in services.msc 
   - Microsoft SQL Server IaaS Agent 
   - Microsoft SQL Server IaaS Query Service 

## Fails to install on domain controller

Registering your SQL Server instance installed to your domain controller with the SQL IaaS agent extension isn't supported. Registering with the extension creates the user `NT Service\SQLIaaSExtension` and since this user can't be created on the domain controller, registering this VM with the SQL IaaS agent isn't supported. 


## Next steps

For answers to frequently asked questions about the extension, check out the [FAQ](frequently-asked-questions-faq.yml#sql-server-iaas-agent-extension). 

For more information, see the following articles:

* [Overview of SQL Server on a Windows VM](sql-server-on-azure-vm-iaas-what-is-overview.md)
* [Overview of the SQL IaaS agent extension](sql-server-iaas-agent-extension-automate-management.md)
* [Pricing guidance for SQL Server on a Azure VMs](../windows/pricing-guidance.md)
* [What's new for SQL Server on Azure VMs](../windows/doc-changes-updates-release-notes-whats-new.md)
