---
title: What's new?
description: Learn about the new features for and improvements to SQL Server on Azure Virtual Machines.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 12/19/2023
ms.service: virtual-machines-sql
ms.subservice: service-overview
ms.topic: whats-new
ms.custom: ignite-2022, ignite-2023
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

[!INCLUDE [entra-id](../../includes/entra-id.md)]


## October 2023

| Changes | Details |
| --- | --- |
| **Azure Elastic SAN preview** | You can now place your SQL Server workloads on an Azure Elastic SAN for improved performance, throughput, and cost. The Azure Elastic SAN is currently in preview. Review [Azure Elastic SAN](performance-guidelines-best-practices-storage.md#azure-elastic-san) to learn more. |  

## September 2023

| Changes | Details |
| --- | --- |
| **SQL VM health notifications** |  The **Overview** page of the [SQL virtual machines](manage-sql-vm-portal.md#overview-page) resource in the Azure portal now displays information about the health of the [SQL IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md), as well as error conditions when the state is _unhealthy_ or _failed_. Review [Troubleshoot the extension](sql-agent-extension-troubleshoot-known-issues.md) to learn more. | 
| **Microsoft Entra ID rebrand**|  Azure Active Directory has been rebranded to [Microsoft Entra ID](/entra/fundamentals/new-name). | 



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
