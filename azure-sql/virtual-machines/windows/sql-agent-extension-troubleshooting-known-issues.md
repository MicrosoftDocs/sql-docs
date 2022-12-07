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

## Provisioning failed 

[Repair the extension](sql-agent-extension-manually-register-single-vm.md#repair-extension) if the SQL IaaS extension status shows as **Provisioning failed** in the Azure portal. 

## SQL VM resource unavailable in portal 

If the SQL IaaS extension is installed, and the VM is online, but the SQL VM resource is unavailable in the Azure portal, verify that your SQL Server and SQL Browser service are started within the VM. If this does not resolve the issue, [repair the extension](sql-agent-extension-manually-register-single-vm.md#repair-extension). 

## Features are grayed out

If you navigate to your [SQL VM resource](manage-sql-vm-portal.md) in the Azure portal, and there are features that are grayed out, verify that that the SQL VM is running, and that you have the extension registered in [Full mode](sql-server-iaas-agent-extension-automate-management.md#management-modes). 

Grayed out features are expected if your SQL VM is registered in lightweight mode as the extension does not offer full capability, and many features are [unavailable](sql-server-iaas-agent-extension-automate-management.md#feature-benefits). 

Upgrade your extension to full mode to gain access to any unavailable features. Lightweight mode is the default mode for SQL Server instances that participate in a failover cluster instance (FCI) and cannot be upgraded, so some features will always be grayed out. 





## Next steps

For more information, see the following articles:

* [Overview of SQL Server on a Windows VM](sql-server-on-azure-vm-iaas-what-is-overview.md)
* [FAQ for SQL Server on a Windows VM](frequently-asked-questions-faq.yml)
* [Pricing guidance for SQL Server on a Azure VMs](../windows/pricing-guidance.md)
* [What's new for SQL Server on Azure VMs](../windows/doc-changes-updates-release-notes-whats-new.md)
