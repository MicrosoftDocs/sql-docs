---
title: What's new?
description: Learn about the new features for and improvements to SQL Server on Azure Virtual Machines.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest, dpless
ms.date: 03/02/2026
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
| [VM vCore customization](vm-vcore-customization-for-sql.md) |Customize the number of vCPUs presented to the guest OS for SQL Server workloads with configurable constrained cores (CCC), and disable Simultaneous Multithreading (SMT). This capability allows you to appropriately size the vCPU count to match your SQL Server licensing needs while preserving the VM's memory and I/O capabilities. |



## General availability (GA)

The following table lists features of SQL Server on Azure VMs that have been made generally available (GA) within the last 12 months:

| Changes | Month | Details |
| --- | --- |--- |
| [I/O Analysis](storage-performance-analysis.md) | April 2025 | Use the Azure portal to identify performance issues with your SQL Server workloads from exceeding virtual machine and data disk limits. | 
| [Azure Elastic SAN](storage-configuration-azure-elastic-san.md) | March 2025 | Place your SQL Server workloads on an Azure Elastic SAN for improved performance, throughput, and cost. |
| [FCI with Azure Elastic SAN](failover-cluster-instance-azure-elastic-san-manually-configure.md) | March 2025 | Deploy your SQL Server failover cluster instance (FCI) by using an Azure Elastic SAN. |
| [Managed identity support for SQL Server 2022 on Azure VM](managed-identity-extensible-key-management.md) | January 2025 | Starting with SQL Server 2022 Cumulative Update 17 (CU17), managed identities are supported for SQL Server on Azure VMs (Windows only). Managed identities can be used with [SQL Server credentials](/sql/t-sql/statements/create-credential-transact-sql) to [back up to and restore SQL Server on Azure VM databases from Azure Blob storage](backup-restore-to-url-using-managed-identities.md). Support for managed identities also enables functionalities like [Extensible Key Management (EKM) with Azure Key Vault (AKV) and Managed Hardware Security Modules (HSM)](managed-identity-extensible-key-management.md) to be used with SQL Server on Azure VMs. |

## Documentation changes 

Learn about significant changes to the SQL Server on Azure VMs documentation. For previous years, see the [What's new archive](doc-changes-updates-release-notes-whats-new-archive.md).

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
