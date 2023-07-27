---
title: What's new?
description: Learn about the new features for and improvements to SQL Server on Azure Virtual Machines.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 07/12/2023
ms.service: virtual-machines-sql
ms.subservice: service-overview
ms.topic: whats-new
ms.custom: ignite-2022
tags: azure-service-management
---
# What's new with SQL Server on Azure Virtual Machines?

[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

When you deploy an Azure virtual machine (VM) with SQL Server installed on it, either manually, or through a built-in image, you can use Azure features to improve your experience. This article summarizes the documentation changes associated with new features and improvements in the recent releases of [SQL Server on Azure Virtual Machines (VMs)](https://azure.microsoft.com/services/virtual-machines/sql-server/). To learn more about SQL Server on Azure VMs, see the [overview](sql-server-on-azure-vm-iaas-what-is-overview.md). 

For updates made in previous years, see the [What's new archive](doc-changes-updates-release-notes-whats-new-archive.md). 

## July 2023

[!INCLUDE[appliesto-sqlvm](../../includes/virtual-machines-2008-end-of-support.md)]

## May 2023

| Changes | Details |
| --- | --- |
| **Azure SQL bindings for Azure Functions GA** | Azure Functions supports input bindings, and output bindings for the Azure SQL and SQL Server products. This feature is now generally available. Review [Azure SQL bindings for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql) to learn more.  | 
| **Azure SQL triggers for Azure Functions preview** Azure Functions supports function triggers for the Azure SQL and SQL Server products. This feature is currently in preview. Review [Azure SQL triggers for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql-trigger) to learn more. | 


## April 2023

| Changes | Details |
| --- | --- |
| **Auto upgrade SQL IaaS Agent extension** | It's now possible to enable auto upgrade for your SQL IaaS Agent extension to ensure you're automatically receiving the latest updates to the extension every month. Review [SQL IaaS Agent Settings](manage-sql-vm-portal.md#sql-iaas-agent-extension-settings) to learn more. 
| **Azure AD authentication GA** | Azure Active Directory (Azure AD) authentication is now generally available. Review [Configure Azure AD](configure-azure-ad-authentication-for-sql-vm.md) to learn more. |  
| **Migrate AG to multi-subnet** | Learn how to migrate your single-subnet Always On availability group to multiple subnets to remove the reliance on an Azure Load Balancer or Distributed Network Name (DNN) to route traffic to your listener. See [Migrate availability group to a multi-subnet environment](availability-group-manually-migrate-multi-subnet.md) to learn more. | 


## March 2023

| Changes | Details |
| --- | --- |
| **Removed extension management modes** | The architecture for the SQL IaaS Agent extension has been updated to remove management modes. All newly deployed SQL Server VMs are registered with the extension by using the same default configuration and least privileged security model. To learn more, review [Management modes](sql-server-iaas-agent-extension-automate-management.md#management-modes). |

## February 2023 

| Changes | Details |
| --- | --- |
| **Enable Azure AD for SQL Server** | We've published a guide to help you enable Azure AD authentication for your SQL Server VM. Review [Configure Azure AD](configure-azure-ad-authentication-for-sql-vm.md) to learn more. | 

## January 2023

| Changes | Details |
| --- | --- |
| **Extend your multi-subnet AG to multiple regions** | Extend an existing multi-subnet availability group, either on Azure virtual machines, or on-premises, to another region in Azure. To learn more, review [Multi-subnet availability group in multiple regions](availability-group-manually-configure-multi-subnet-multiple-regions.md). | 


## 2022

| Changes | Details |
| --- | --- |
| **Troubleshoot SQL IaaS Agent extension** | We've added an article to help you troubleshoot and address some known issues with the SQL Server IaaS agent extension. To learn more, read [Troubleshoot known issues](sql-agent-extension-troubleshoot-known-issues.md). |
| **Configure AG from Azure portal** | There is a new experience to deploy an Always On availability group to multiple subnets by using the Azure portal. The new availability group deployment method replaces the previous deployment through the SQL virtual machines resource. This feature is currently in preview. To learn more, review [Configure availability group through the Azure portal](availability-group-azure-portal-configure.md). | 
| **Azure AD authentication** | It's now possible to configure Azure Active Directory (Azure AD) authentication to your SQL Server 2022 on Azure VM by using the Azure portal. This feature is currently in preview. To get started, review [Azure AD with SQL Server VMs](security-considerations-best-practices.md#azure-ad-authentication). | 
| **Least privilege permission model for SQL IaaS Agent extension** | There is a new permissions model available for the SQL Server IaaS Agent extension that grants the least privileged permission for each feature used by the extension. To learn more, review [SQL IaaS Agent extension permissions](sql-server-iaas-agent-extension-automate-management.md#permissions-models). | 
| **Confidential VMs** | SQL Server on Azure VMs has added support to deploy to [SQL Server on Azure confidential VMs](sql-vm-create-confidential-vm-how-to.md). To get started, review the [Quickstart: Deploy SQL Server to an Azure confidential VM](sql-vm-create-portal-quickstart.md?tabs=confidential-vm). 
| **Azure CLI for SQL best practices assessment**| It's now possible to configure the [SQL best practices assessment](sql-assessment-for-sql-vm.md) feature using the Azure CLI. |
| **Configure tempdb from Azure portal** | It's now possible to configure your `tempdb` settings, such as the number of files, initial size, and autogrowth ratio for an existing SQL Server instance by using the Azure portal. See [manage SQL Server VM from portal](manage-sql-vm-portal.md#storage) to learn more. |
| **SDK-style SQL projects**| Use [Microsoft.Build.Sql](https://www.nuget.org/packages/Microsoft.Build.Sql) for SDK-style SQL projects in the SQL Database Projects extension in Azure Data Studio or VS Code. This feature is currently in preview. To learn more, see [SDK-style SQL projects](/sql/azure-data-studio/extensions/sql-database-project-extension-sdk-style-projects). |
| **Ebdsv5-series** | The new [Ebdsv5-series](/azure/virtual-machines/ebdsv5-ebsv5-series#ebdsv5-series) provides the highest I/O throughput-to-vCore ratio in Azure along with a memory-to-vCore ratio of 8. This series offers the best price-performance for SQL Server workloads on Azure VMs. Consider this series first for most SQL Server workloads. To learn more, see the updates in [VM sizes](performance-guidelines-best-practices-vm-size.md). |
| **Security best practices** | The [SQL Server VM security best practices](security-considerations-best-practices.md) have been rewritten and refreshed! |
| **Migrate with distributed AG** | It's now possible to migrate your database(s) from a [standalone instance](../../migration-guides/virtual-machines/sql-server-distributed-availability-group-migrate-standalone-instance.md) of SQL Server or an [entire availability group](../../migration-guides/virtual-machines/sql-server-distributed-availability-group-migrate-ag.md) over to SQL Server on Azure VMs using a distributed availability group! See the [prerequisites](../../migration-guides/virtual-machines/sql-server-distributed-availability-group-migrate-prerequisites.md) to get started. |



## Contribute to content

To contribute to the Azure SQL documentation, see the [Docs contributor guide](/contribute/).

## Additional resources

**Windows VMs**:

* [Overview of SQL Server on a Windows VM](sql-server-on-azure-vm-iaas-what-is-overview.md)
* [Provision SQL Server on a Windows VM](create-sql-vm-portal.md)
* [Migration guide: SQL Server to SQL Server on Azure Virtual Machines](../../migration-guides/virtual-machines/sql-server-to-sql-on-azure-vm-individual-databases-guide.md)
* [High availability and disaster recovery for SQL Server on Azure Virtual Machines](business-continuity-high-availability-disaster-recovery-hadr-overview.md)
* [Performance best practices for SQL Server on Azure Virtual Machines](./performance-guidelines-best-practices-checklist.md)
* [Application patterns and development strategies for SQL Server on Azure Virtual Machines](application-patterns-development-strategies.md)

**Linux VMs**:

* [Overview of SQL Server on a Linux VM](../linux/sql-server-on-linux-vm-what-is-iaas-overview.md)
* [Provision SQL Server on a Linux virtual machine](../linux/sql-vm-create-portal-quickstart.md)
* [FAQ (Linux)](../linux/frequently-asked-questions-faq.yml)
* [SQL Server on Linux documentation](/sql/linux/sql-server-linux-overview)
