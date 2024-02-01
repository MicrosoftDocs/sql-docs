---
title: Known issues and troubleshooting the SQL Server IaaS agent extension
description: Learn about the known issues and how to troubleshoot errors with the SQL Server Iaas Agent extension.
author: adbadram
ms.author: adbadram
ms.reviewer: mathoma, randolphwest
ms.date: 09/06/2023
ms.service: virtual-machines-sql
ms.subservice: management
ms.topic: how-to
---
# Known issues and troubleshooting the SQL Server IaaS Agent extension

[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article helps you resolve known issues and troubleshoot errors when using the [SQL Server IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md).

For answers to frequently asked questions about the extension, check out the [FAQ](frequently-asked-questions-faq.yml#sql-server-iaas-agent-extension).

## Check prerequisites

To avoid errors due to unsupported options or limitations, verify the [prerequisites](sql-agent-extension-manually-register-single-vm.md#prerequisites) for the extension. 

If you repair, or reinstall the SQL IaaS Agent extension, your setting won't be preserved, other than licensing changes. If you've repaired or reinstalled the extension, you'll have to reconfigure automated backup, automated patching, and any other services you had configured prior to the repair or reinstall. 

## Check extension health

You can check the health of your extension on the **Overview** page of your [SQL virtual machines](manage-sql-vm-portal.md#overview-page) resource in the Azure portal, under **Extension health status**. 

:::image type="content" source="./media/manage-sql-vm-portal/sql-vm-resource.png" alt-text="Screenshot of the Azure portal, the overview pane of the SQL virtual machines resource." lightbox="./media/manage-sql-vm-portal/sql-vm-resource.png":::

> [!NOTE]
> You can also use a PowerShell script to check the extension health status on your virtual machines. You can find the full script on GitHub, see [Get SQL IaaS Agent extension health status with Az PowerShell](https://github.com/Azure/azure-docs-powershell-samples/blob/master/sql-virtual-machine/get-sqliaasextension-healthstatus/GetSqlVirtualMachinesExtensionHealthStatus.psm1).


The status of the SQL IaaS Agent extension can be: 

- **Healthy**: Everything is working as expected. 
- **Failed**: The main SQL IaaS Agent service is not running on the SQL Server VM. 
- **Unhealthy**: One or more subservices has a problem. 


If the state of the SQL IaaS Agent extension is either **Unhealthy** or **Failed**, check **Notifications** on the **Overview** page to find out more details.  

The rest of this section provides information about each error condition notification. 

### The main SQL IaaS Agent extension service isn't running

The main service for the SQL IaaS Agent extension (**Microsoft SQL Server IaaS agent**) is in a stopped state. The SQL IaaS Agent extension status is _failed_ due to this error. 

To resolve this error condition, [repair](#repair-extension) the extension. 


### SQL Server is not running 

The SQL Server service is stopped. The SQL IaaS Agent extension status is _unhealthy_ due to this error. 

Investigate further, and [restart the service](/sql/database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services). 


### The SQL IaaS Agent extension query service is not running 

The SQL IaaS Agent extension uses the query service (**Microsoft SQL Server IaaS Query Service**) to communicate with SQL Server. If the query service is in a stopped state, features that rely on communication with SQL Server won't work. The SQL IaaS Agent extension status is _unhealthy_ due to this error. 

To resolve this error condition, [repair](#repair-extension) the extension. 


### The SQL IaaS Agent extension doesn't have correct permissions

The SQL IaaS Agent extension query service (**Microsoft SQL Server IaaS Query Service**) uses the `NT Service\SQLIaaSExtensionQuery` account to query the SQL Server instance. If this login is removed from SQL Server, or if a user or domain policy changes permissions for the login, you'll see the error that the extension doesn't have correct permissions. The SQL IaaS Agent extension status is _unhealthy_ due to this error. 

For SQL Server VMs that use the least privilege permissions model, check to make sure the `NT Service\SQLIaaSExtensionQuery` account has the proper [permissions](sql-server-iaas-agent-extension-automate-management.md#permissions-models) associated with each enabled feature. If no features are enabled, then you'll see the error if the `NT Service\SQLIaaSExtensionQuery` login doesn't exist within SQL Server or if **Microsoft SQL Server IaaS Query Service** is running under a different username than `NT Service\SQLIaaSExtensionQuery`. 

Some SQL Server VMs deployed before October 2022 may still use the [older sysadmin permissions model](sql-server-iaas-agent-extension-automate-management.md#permissions-models). For these older VMs, you'll see the permissions error if the `NT Service\SQLIaaSExtensionQuery` doesn't exist, or doesn't have sysadmin rights within SQL Server, or if **Microsoft SQL Server IaaS Query Service** is running under a different username than `NT Service\SQLIaaSExtensionQuery`. 

To resolve this error condition, confirm the login exists in SQL Server, and that it has the correct [permissions](sql-server-iaas-agent-extension-automate-management.md#permissions-models) based on the features you've enabled. You may need to recreate the login, and/or assign correct permissions. Additionally, validate **Microsoft SQL Server IaaS Query Service** is running under the username `NT Service\SQLIaaSExtensionQuery`. 


## Repair extension

It's possible for your SQL IaaS Agent extension to be in a failed state. Use the Azure portal to repair the SQL IaaS Agent extension. 

To repair the extension with the Azure portal:  

1. Sign in to the [Azure portal](https://portal.azure.com).
1. Go to your [SQL virtual machines](manage-sql-vm-portal.md) resource.
1. Select your SQL Server VM from the list. If your SQL Server VM isn't listed here, it likely hasn't been registered with the SQL IaaS Agent extension.
1. Select **SQL IaaS Agent Extension Settings** under **Help**. 
1. If your provisioning state shows as **Failed**, choose **Repair** to repair the extension. If your state is **Succeeded** you can check the box next to **Force repair** to repair the extension regardless of state. 

   :::image type="content" source="media/sql-agent-extension-troubleshoot-known-issues/repair-extension.png" alt-text="Screenshot of the SQL IaaS Agent extension settings page of the SQL virtual machines extension in the Azure portal showing where to repair the extension.":::   

## SQL IaaS Agent extension registration fails with error "Creating SQL Virtual Machine resource for Power BI VM images is not supported"

Note that SQL IaaS Agent extension registration is blocked and not supported on Power BI VM, SQL Server Reporting Server and SQL Server Analysis Service Images deployed from Azure Marketplace.

## Not valid state for management

[Repair the extension](#repair-extension) if you see the following error message:

`The SQL virtual machines resource is not in a valid state for management`

## Underlying virtual machine is invalid

If you see the following error message:

`SQL management operations are disabled because the state of underlying virtual machine is invalid`

Consider the following:

- The SQL VM may be stopped, deallocated, in a failed state, or not found. Validate the underlying virtual machine is running.
- Your SQL IaaS Agent extension may be in a failed state. [Repair the extension](#repair-extension).

[Unregister your SQL VM from the extension](sql-agent-extension-manually-register-single-vm.md#unregister-from-extension) and then register the SQL VM with the extension again if you did any of the following:

- Migrated your VM from one subscription to the other.
- Changed the locale or collation of SQL Server.
- Changed the version of your SQL Server instance.
- Changed the edition of your SQL Server instance.

## Provisioning failed 

[Repair the extension](#repair-extension) if the SQL IaaS Agent extension status shows as **Provisioning failed** in the Azure portal.

## SQL VM resource unavailable in portal

If the SQL IaaS Agent extension is installed, and the VM is online, but the SQL VM resource is unavailable in the Azure portal. Verify that your SQL Server and SQL Browser service are started within the VM. If this doesn't resolve the issue, [repair the extension](#repair-extension).

## Features are grayed out

If you navigate to your [SQL VM resource](manage-sql-vm-portal.md) in the Azure portal, and there are features that are grayed out, verify that the SQL VM is running, and that you have the latest version of the SQL IaaS Agent extension.

## Changed service account

Changing the service accounts for either of the two services associated with the extension can cause the extension to fail or behave unpredictably.

The two services should run under the following accounts:

- **Microsoft SQL Server IaaS agent** is the main service for the SQL IaaS Agent extension and should run under the **Local System** account.
- **Microsoft SQL Server IaaS Query Service** is a helper service that helps the extension run queries within SQL Server and should run under the **NT Service** account `NT Service\SqlIaaSExtensionQuery`.

## Automatic registration failed

If you have a few SQL Server VMs that failed to [register automatically](sql-agent-extension-automatic-registration-all-vms.md), check the version of SQL Server on the VMs that failed to register. By default, Azure VMs with SQL Server 2016 or later are automatically registered with the SQL IaaS Agent extension when detected by the [CEIP service](/sql/sql-server/usage-and-diagnostic-data-configuration-for-sql-server). SQL Server VMs that have versions earlier than 2016 have to be manually registered [individually](sql-agent-extension-manually-register-single-vm.md) or [in bulk](sql-agent-extension-manually-register-vms-bulk.md).

## High resource consumption

If you notice that the SQL IaaS Agent extension is consuming unexpectedly high CPU or memory, verify the extension is on the latest version. If so, restart **Microsoft SQL Server IaaS Agent** from `services.msc`.

## Can't extend disks

Extending your disks from the **Storage Configuration** page of the [SQL VM resource](manage-sql-vm-portal.md) is unavailable under the following conditions:

- If you uninstall and reinstall the SQL IaaS Agent extension.
- If you uninstall and reinstall your instance of SQL Server.
- If you used custom naming conventions for the disk/storage pool name when deploying your SQL Server image from the Azure Marketplace.

## Disk configuration grayed out during deployment

If you create your SQL Server VM by using an unmanaged disk, disk configuration is grayed out by design.

## Automated backup disabled

If your [SQL VM resource](manage-sql-vm-portal.md) displays **Automated backup is currently disabled**, check to see if your SQL Server instance has [managed backups](/sql/relational-databases/backup-restore/enable-sql-server-managed-backup-to-microsoft-azure) enabled. To use Automated backups from the Azure portal, disable managed backups in SQL Server.

## Extension stuck in transition

Your SQL IaaS Agent extension may get stuck in a transitioning state in the following scenarios:

- You've removed the `NT service\SQLIaaSExtension` service from the SQL Server logins and/or the local administrator's group.
- Either of these two services are stopped in services.msc
  - Microsoft SQL Server IaaS Agent
  - Microsoft SQL Server IaaS Query Service

## Fails to install on domain controller

Registering your SQL Server instance installed to your domain controller with the SQL IaaS Agent extension isn't supported. Registering with the extension creates the user `NT Service\SQLIaaSExtension` and since this user can't be created on the domain controller, registering this VM with the SQL IaaS Agent isn't supported.



## Next steps

- Review the benefits provided by the [SQL IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md).
- [Manually register a single VM](sql-agent-extension-manually-register-single-vm.md)
- [Automatically register all VMs in a subscription](sql-agent-extension-automatic-registration-all-vms.md).
- Review the [SQL IaaS Agent extension privacy statements](sql-server-iaas-agent-extension-automate-management.md#in-region-data-residency).
- Review the [best practices checklist](performance-guidelines-best-practices-checklist.md) to optimize for performance and security.

To learn more, review the following articles:

- [Overview of SQL Server on Windows VMs](sql-server-on-azure-vm-iaas-what-is-overview.md)
- [FAQ for SQL Server on Windows VMs](frequently-asked-questions-faq.yml)
- [Pricing guidance for SQL Server on Azure VMs](../windows/pricing-guidance.md)
- [What's new for SQL Server on Azure VMs](../windows/doc-changes-updates-release-notes-whats-new.md)
