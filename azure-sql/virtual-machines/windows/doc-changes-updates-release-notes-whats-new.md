---
title: What's new?
description: Learn about the new features for and improvements to SQL Server on Azure Virtual Machines.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest, dpless
ms.date: 07/10/2026
ms.service: azure-vm-sql-server
ms.topic: whats-new
ms.custom:
  - ignite-2025
tags: azure-service-management
---
# What's new with SQL Server on Azure Virtual Machines?

[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](../../database/doc-changes-updates-release-notes-whats-new.md?view=azuresql&preserve-view=true)
> * [Azure SQL Managed Instance](../../managed-instance/doc-changes-updates-release-notes-whats-new.md?view=azuresql&preserve-view=true)
> * [SQL Server on Azure VMs](doc-changes-updates-release-notes-whats-new.md?view=azuresql&preserve-view=true)

When you deploy an Azure virtual machine (VM) with SQL Server installed on it, either manually, or through a built-in image, you can use Azure features to improve your experience. This article summarizes the documentation changes associated with new features and improvements in the recent releases of [SQL Server on Azure Virtual Machines (VMs)](https://azure.microsoft.com/services/virtual-machines/sql-server/). To learn more about SQL Server on Azure VMs, see the [overview](sql-server-on-azure-vm-iaas-what-is-overview.md). 

For updates made in previous years, see the [What's new archive](doc-changes-updates-release-notes-whats-new-archive.md). 

[!INCLUDE [sql-vm-deployment-failure](../../includes/sql-vm-deployment-failure.md)]


## Preview

The following table lists the features of SQL Server on Azure VMs that are currently in preview.

> [!NOTE]  
> Features currently in preview are available under [supplemental terms of use](https://azure.microsoft.com/support/legal/preview-supplemental-terms/), review for legal terms that apply to Azure features that are in beta, preview, or otherwise not yet released into general availability. SQL Server on Azure VMs provide previews to give you a chance to evaluate and [share feedback with the product group](https://feedback.azure.com/d365community/forum/04fe6ee0-3b25-ec11-b6e6-000d3a4f0da0) on features before they become generally available (GA).

| Feature | Details |
| --- | --- |
| [Modernization Advisor](../modernization-advisor.md) | Use the Modernization Advisor in the Azure portal to help you determine if migrating to Azure SQL Managed Instance saves you money or optimizes performance. |
| [Premium SSD v2 in the Azure portal](storage-configuration-premium-ssd-v2.md) | Deploy your SQL Server on Azure VM with Premium SSD v2 disks in the Azure portal for improved throughput and performance. |  
| [Unified inventory](unified-inventory-sql-vm.md) | View your SQL Server on Azure VM and SQL Server enabled by Azure Arc resources in a single pane in the Azure portal. With unified inventory, you can view your *SQL Server instance* resources, making it easier to monitor and maintain your SQL Server workloads in Azure. |

## General availability (GA)

The following table lists features of SQL Server on Azure VMs that have been made generally available (GA) within the last 12 months:

| Changes | Month | Details |
| --- | --- |--- |
| [Migration from the Azure portal](/sql/sql-server/azure-arc/migrate-to-sql-server-on-azure-vms) | June 2026 | Migrate your SQL Server databases to SQL Server on Azure VMs directly from the Azure portal. |
| [VM vCore customization](vm-vcore-customization-for-sql.md) | May 2026 | Customize the number of vCPUs presented to the guest OS for SQL Server workloads with configurable constrained cores (CCC), and disable Simultaneous Multithreading (SMT). This capability allows you to appropriately size the vCPU count to match your SQL Server licensing needs while preserving the VM's memory and I/O capabilities. |

## Documentation changes

Learn about significant changes to the SQL Server on Azure VMs documentation. For previous years, see the [What's new archive](doc-changes-updates-release-notes-whats-new-archive.md).

### June 2026

| Changes | Details |
| --- | --- |
| **Migration through the Azure portal GA** | Migrate your SQL Server databases to SQL Server on Azure VMs directly from the Azure portal. This feature is now generally available (GA). To learn more, see [Migration from the Azure portal](/sql/sql-server/azure-arc/migrate-to-sql-server-on-azure-vms). |

### May 2026

| Changes | Details |
| --- | --- |
| **VM vCore customization GA** |Customize the number of vCPUs presented to the guest OS for SQL Server workloads with configurable constrained cores (CCC), and disable Simultaneous Multithreading (SMT). This capability allows you to appropriately size the vCPU count to match your SQL Server licensing needs while preserving the VM's memory and I/O capabilities. VM vCore customization is now generally available (GA) for SQL Server on Azure VMs. To learn more, review [VM vCore customization](vm-vcore-customization-for-sql.md). |

### April 2026

| Changes | Details |
| --- | --- |
| **Migration through the Azure portal preview**| Migrate your SQL Server databases to SQL Server on Azure VMs directly from the Azure portal. This feature is currently in preview.  To learn more, see [Migration from the Azure portal](/sql/sql-server/azure-arc/migrate-to-sql-server-on-azure-vms). |
| **SQL Server servicing and updates guidance** | New article that describes the supported methods to keep SQL Server up to date on Azure VMs, including Azure Update Manager, Automated Patching, custom images, winget for client tools, and post-deployment automation. SQL Server images from Azure Marketplace are deployed at RTM and aren't updated over time. For more information, see [Updating SQL Server on Azure VMs](servicing-updates-guidelines.md). |

### March 2026

| Changes | Details |
| --- | --- | --- |
| **Unified inventory preview** | You can now view your SQL Server on Azure VM and SQL Server enabled by Azure Arc resources in a single pane in the Azure portal. With unified inventory, you can view your *SQL Server instance* resources, making it easier to monitor and maintain your SQL Server workloads in Azure. This feature is now in preview. For more information, see [Unified inventory for SQL Server on Azure VMs](unified-inventory-sql-vm.md). |

### January 2026

| Changes | Details |
| --- | --- | --- |
| **VM vCore customization for SQL Server on Azure VMs preview** | You can now customize the number of vCPUs presented to the guest OS for SQL Server workloads with configurable constrained cores (CCC), and disable Simultaneous Multithreading (SMT). This capability allows you to appropriately size the vCPU count to match your SQL Server licensing needs while preserving the VM's memory and I/O capabilities. This feature is now in preview. For more information, see [VM vCore customization for SQL Server on Azure VMs](vm-vcore-customization-for-sql.md). |

### September 2025

| Changes | Details |
| --- | --- | --- |
| **New Azure SQL hub** | Choosing the right Azure SQL service can be challenging. To make this easier, we built the [Azure SQL hub at aka.ms/azuresqlhub](https://aka.ms/azuresqlhub), a new home for everything related to Azure SQL in the Azure portal. For more information, read the blog post [Introducing the Azure SQL hub: A simpler, guided entry into Azure SQL](https://aka.ms/azuresqlhubblog). |


## Archive

For updates made in previous years, see the [What's new archive](doc-changes-updates-release-notes-whats-new-archive.md). 

## Contribute to content

To contribute to the Azure SQL documentation, see the [Docs contributor guide](/contribute/).

## Additional resources

**Windows VMs**:

* [Overview of SQL Server on Windows VMs](sql-server-on-azure-vm-iaas-what-is-overview.md)
* [Provision SQL Server on Windows VMs](create-sql-vm-portal.md)
* [Migration guide: SQL Server to SQL Server on Azure Virtual Machines](../../migration-guides/virtual-machines/sql-server-to-sql-on-azure-vm-individual-databases-guide.md)
* [High availability and disaster recovery for SQL Server on Azure Virtual Machines](business-continuity-high-availability-disaster-recovery-hadr-overview.md)
* [Performance best practices for SQL Server on Azure Virtual Machines](./performance-guidelines-best-practices-checklist.md)
* [Application patterns and development strategies for SQL Server on Azure Virtual Machines](application-patterns-development-strategies.md)

**Linux VMs**:

* [Overview of SQL Server on a Linux VM](../linux/sql-server-on-linux-vm-what-is-iaas-overview.md)
* [Provision SQL Server on a Linux virtual machine](../linux/sql-vm-create-portal-quickstart.md)
* [FAQ (Linux)](../linux/frequently-asked-questions-faq.yml)
* [SQL Server on Linux documentation](/sql/linux/sql-server-linux-overview)
