---
title: Manage SQL VMs in Azure by using the portal
description: Learn how to manage SQL Server on Azure VMs in the Azure portal by accessing the SQL virtual machines resource to modify SQL Server settings.
author: bluefooted
ms.author: pamela
ms.reviewer: mathoma
ms.date: 10/16/2023
ms.service: virtual-machines-sql
ms.subservice: management
ms.topic: how-to
tags: azure-resource-manager
---
# Manage SQL Server VMs by using the Azure portal
[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

In the [Azure portal](https://portal.azure.com), the [**SQL virtual machines**](https://portal.azure.com/#blade/HubsExtension/BrowseResource/resourceType/Microsoft.SqlVirtualMachine%2FSqlVirtualMachines) resource is an independent management service to manage SQL Server on Azure Virtual Machines (VMs) that have been registered with the SQL Server IaaS Agent extension. You can use the resource to view all of your SQL Server VMs simultaneously and modify settings dedicated to SQL Server: 

:::image type="content" source="./media/manage-sql-vm-portal/sql-vm-manage.png" alt-text="Screenshot of accessing the SQL virtual machines resource in the Azure portal.":::

The **SQL virtual machines** resource management point is different to the **Virtual machine** resource used to manage the underlying VM such as to start it, stop it, or restart it. 

## Prerequisites

The **SQL virtual machines** resource is only available to SQL Server VMs that have been [registered with the SQL IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md). 

## Access the resource

To access the **SQL virtual machines** resource, follow these steps:

1. Open the [Azure portal](https://portal.azure.com). 
1. Select **All Services**. 
1. Enter **SQL virtual machines** in the search box.
1. (Optional): Select the star next to **SQL virtual machines** to add this option to your **Favorites** menu. 
1. Select **SQL virtual machines**. 

   :::image type="content" source="./media/manage-sql-vm-portal/sql-vm-search.png" alt-text="Screenshot of the Azure portal, All services selected, and the search box highlighted.":::

1. The portal lists all SQL Server VMs available within the selected subscription. Choose the one you want to manage to open the **SQL virtual machines** resource. Use the search box if your SQL Server VM isn't appearing, and make sure you've selected the correct subscription.  

   :::image type="content" source="./media/manage-sql-vm-portal/all-sql-vms.png" alt-text="Screenshot of the Azure portal, the SQL virtual machines resource page, with a VM selected.":::

Selecting your SQL Server VM opens the **SQL virtual machines** resource. 

> [!TIP]
> The **SQL virtual machines** resource is for dedicated SQL Server settings. Select the name of the VM under **Virtual machine** on the **Overview** page to open settings that are specific to the underlying virtual machine. 

## Overview page

The **Overview** page of the SQL virtual machines resource provides basic information about the SQL Server VM, such as the resource group, location, license type, the name of the underlying Azure virtual machine, and storage utilization metrics. 

You can also see the status of the SQL Iaas Agent extension under **Extension health status**. If your status is **Unhealthy**, or **Failed**, you can find out more information from the **Notifications** tab. 

The **Notifications** tab displays information from [SQL best practices assessments](sql-assessment-for-sql-vm.md) and about issues with the [Extension health](sql-agent-extension-troubleshoot-known-issues.md#check-extension-health). 

   :::image type="content" source="./media/manage-sql-vm-portal/sql-vm-resource.png" alt-text="Screenshot of the Azure portal, the overview pane of the SQL virtual machines resource." lightbox="./media/manage-sql-vm-portal/sql-vm-resource.png":::

## License and edition 

Use the **Configure** page of the SQL virtual machines resource to change your SQL Server licensing metadata to **Pay as you go**, **Azure Hybrid Benefit**, or **HA/DR** for your [free Azure replica for disaster recovery](business-continuity-high-availability-disaster-recovery-hadr-overview.md#free-dr-replica-in-azure).

:::image type="content" source="./media/manage-sql-vm-portal/sql-vm-license-edition.png" alt-text="Screenshot of the Azure portal, SQL virtual machines resource, showing where to change the version and edition of SQL Server VM metadata.":::

You can also modify the edition of SQL Server from the **Configure** page as well, such as **Enterprise**, **Standard**, or **Developer**. 

Changing the license and edition metadata in the Azure portal is only supported once the version and edition of SQL Server has been modified internally to the VM. To learn more see, change the [version](change-sql-server-version.md) and [edition](change-sql-server-edition.md) of SQL Server on Azure VMs. 

## Storage configuration

> [!NOTE]
> Making changes to [Premium SSD v2](storage-configuration-premium-ssd-v2.md) for SQL Server VMs in the Azure portal is not currently supported so the **Storage Configuration** page of the SQL virtual machines resource shows **Not extendable** for Premium SSD v2 disks.  Review [Adjust performance](/azure/virtual-machines/disks-deploy-premium-v2?tabs=azure-cli#adjust-disk-performance) to learn more.

Use the **Storage Configuration** page of the SQL virtual machines resource to extend your data, log, and `tempdb` drives. For guidance, review [storage configuration](storage-configuration.md) and [Storage: Performance best practices for SQL Server on Azure VMs](performance-guidelines-best-practices-storage.md).

For example, you can extend your storage:

:::image type="content" source="./media/manage-sql-vm-portal/sql-vm-storage-configuration.png" alt-text="Screenshot of the Azure portal, SQL virtual machines resource, showing where to extend storage.":::


> [!NOTE]
> Storage is only extendable for SQL Server VMs that were deployed from a SQL Server image in Azure Marketplace, and not currently supported for [Premium SSD v2](storage-configuration-premium-ssd-v2.md) disks.



## Updates

You have two different options when automatically patching your SQL Server on Azure VMs - the new integrated [Azure Update Manager](../azure-update-manager-sql-vm.md) experience currently in preview, and the existing [Automated Patching](automated-patching.md) feature. 

Update Manager allows you to choose which updates and patches to apply to multiple SQL Server VMs at scale, including _Cumulative Updates_. **Automated Patching** lets you manage patches for a single VM and only applies updates that are marked as Critical or Important (which doesn't include Cumulative Updates for SQL Server). 

Choose the experience that best suits your business needs as enabling both can lead to unexpected behaviors, scheduling conflicts, and unintentionally applying patches outside of maintenance windows. 

To use the Update Manager, select **Updates** under settings in the resource menu, make sure automated patching is disabled and then select **Try Azure Update Manager** in the navigation bar to integrate Update Manager into your **Updates** page. Once you enable this for any VM, all your other SQL Server VMs will also integrate the new experience unless you've already enabled Automated Patching for any specific VM. If you see **Leave new experience** instead of **Try Azure Update Manager** then you're already using the integrated **Azure Update Manager** experience. 

:::image type="content" source="media/manage-sql-vm-portal/updates-update-manager.png" alt-text="Screenshot of the updates page in the SQL virtual machines resource in the Azure portal with Try Azure Update Manager highlighted.":::

If you've never enabled Update Manager before, then to enable Automated Patching, select **Enable** on the **Updates** page. Otherwise, to go back to the **Automated Patching** page, choose **Leave new experience**: 

:::image type="content" source="media/manage-sql-vm-portal/updates-automated-patching.png" alt-text="Screenshot of the updates page in the SQL virtual machines resource in the Azure portal with leave new experience highlighted.":::

## Backups

Use the **Backups** page of the SQL virtual machines resource to configure your automated backup settings, such as the retention period, which storage account to use, encryption, whether or not to back up system databases, and a backup schedule. 

:::image type="content" source="./media/manage-sql-vm-portal/sql-vm-automated-backup.png" alt-text="Screenshot of the Azure portal, SQL virtual machines resource, showing where to configure automated backup and schedule.":::

To learn more, see, [Automated patching](automated-backup.md). 


## High availability

Once you've configured your [availability group by using the Azure portal](availability-group-azure-portal-configure.md), use the **High Availability** page of the SQL virtual machines resource to monitor the health of your existing Always On availability group. 

:::image type="content" source="media/availability-group-az-portal-configure/healthy-availability-group.png" alt-text="Screenshot of the Azure portal, SQL virtual machines resource, showing where to check the status of your availability group from the high availability page.":::

## SQL best practices assessment

Use the **SQL best practices assessment** page of the SQL virtual machines resource to assess the health of your SQL Server VM. Once the feature is enabled, your SQL Server instances and databases are scanned and recommendations are surfaced to improve performance (indexes, statistics, trace flags, and so on) and identify missing best practices configurations.  

To learn more, see [SQL best practices assessment for SQL Server on Azure VMs](sql-assessment-for-sql-vm.md).

## Security Configuration 

Use the **Security Configuration** page of the SQL virtual machines resource to configure SQL Server security settings such as Azure Key Vault integration, [least privilege mode](sql-server-iaas-agent-extension-automate-management.md) or if you're on SQL Server 2022, [authentication](configure-azure-ad-authentication-for-sql-vm.md) with Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)). 

:::image type="content" source="./media/manage-sql-vm-portal/sql-vm-security-configuration.png" alt-text="Screenshot of the Azure portal, the SQL Server security page, where you can enable authentication.":::

To learn more, see the [Security best practices](security-considerations-best-practices.md).

> [!NOTE]
> The ability to change the connectivity and SQL Server authentication settings after the SQL Server VM is deployed was removed from the Azure portal in April 2023. You can still specify these settings during SQL Server VM deployment, or use SQL Server Management Studio (SSMS) to update these settings manually from within the SQL Server VM after deployment. 

<a name="security-center"></a>

## Defender for Cloud 

Use the **Defender for SQL** page of the SQL virtual machine's resource to view Defender for Cloud recommendations directly in the SQL virtual machine pane. Enable [Microsoft Defender for SQL](/azure/security-center/defender-for-sql-usage) to leverage this feature. 

:::image type="content" source="./media/manage-sql-vm-portal/sql-vm-security-center.png" alt-text="Screenshot of the Azure portal, SQL virtual machines resource, showing where to configure SQL Server Defender for Cloud settings.":::

## SQL IaaS Agent Extension Settings 

From the **SQL IaaS Agent Extension Settings** page, you can [repair the extension](sql-agent-extension-troubleshoot-known-issues.md#repair-extension) and you can enable auto upgrade to ensure you're automatically receiving updates for the extension each month. 

:::image type="content" source="media/manage-sql-vm-portal/sql-iaas-agent-settings.png" alt-text="Screenshot of the SQL IaaS Agent Extension Settings page for your SQL virtual machines resource in the Azure portal.":::

## Related content

- [Overview of SQL Server on Windows VMs](sql-server-on-azure-vm-iaas-what-is-overview.md)
- [FAQ for SQL Server on Windows VMs](frequently-asked-questions-faq.yml)
- [Pricing guidance for SQL Server on Windows VMs](pricing-guidance.md)
- [What's new for SQL Server on Azure VMs](doc-changes-updates-release-notes-whats-new.md)
- [Checklist: Best practices for SQL Server on Azure VMs](performance-guidelines-best-practices-checklist.md)
