---
title: What's new? (Archive)
description: Learn about the features and documentation improvements for SQL Server on Azure VMs made in previous years. (Archive)
author: MashaMSFT
ms.author: mathoma
ms.reviewer: wiassaf, mathoma
ms.date: 03/01/2024
ms.service: virtual-machines-sql
ms.subservice: service-overview
ms.topic: whats-new
ms.custom: references_regions
---
# What's new in SQL Server on Azure VMs? (Archive)
[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article summarizes older documentation changes associated with new features and improvements in the recent releases of [SQL Server on Azure VMs](https://azure.microsoft.com/updates/?product=sql-database&query=sql%20managed%20instance). To learn more about SQL Server on Azure VMs, see the [overview](sql-server-on-azure-vm-iaas-what-is-overview.md).

Return to [What's new in SQL Server on Azure VMs?](doc-changes-updates-release-notes-whats-new.md)

## 2023

[!INCLUDE[appliesto-sqlvm](../../includes/virtual-machines-2008-end-of-support.md)]

| Changes | Month | Details |
| --- | --- |--- |
| **Azure Elastic SAN preview** | October | You can now place your SQL Server workloads on an Azure Elastic SAN for improved performance, throughput, and cost. The Azure Elastic SAN is currently in preview. Review [Azure Elastic SAN](performance-guidelines-best-practices-storage.md#azure-elastic-san) to learn more. |  
| **SQL VM health notifications** | September | The **Overview** page of the [SQL virtual machines](manage-sql-vm-portal.md#overview-page) resource in the Azure portal now displays information about the health of the [SQL IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md), as well as error conditions when the state is _unhealthy_ or _failed_. Review [Troubleshoot the extension](sql-agent-extension-troubleshoot-known-issues.md) to learn more. | 
| **Microsoft Entra ID rebrand**| September | Azure Active Directory has been rebranded to [Microsoft Entra ID](/entra/fundamentals/new-name). | 
| **Azure Update Manager preview** | August | It's now possible to automatically patch multiple SQL Server VMs at scale with the Azure Update Manager integrated in to the [SQL virtual machines](manage-sql-vm-portal.md) resource in the Azure portal, including Cumulative Updates, which isn't currently possible with the existing Automated Patching feature. Using Azure Update Manager for your SQL Server on Azure VMs is currently in preview. To learn more, review [Azure Update Manager for SQL Server on Azure VMs](../azure-update-manager-sql-vm.md). | 
| **Configure AG from Azure portal GA** | August | The experience to deploy an Always On availability group to multiple subnets by using the Azure portal is now generally available. To learn more, review [Configure availability group through the Azure portal](availability-group-azure-portal-configure.md). |
| **Azure SQL bindings for Azure Functions GA** | May | Azure Functions supports input bindings, and output bindings for the Azure SQL and SQL Server products. This feature is now generally available. Review [Azure SQL bindings for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql) to learn more.  | 
| **Azure SQL triggers for Azure Functions preview**|  May |  Azure Functions supports function triggers for the Azure SQL and SQL Server products. This feature is currently in preview. Review [Azure SQL triggers for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql-trigger) to learn more. | 
| **Auto upgrade SQL IaaS Agent extension** | April | It's now possible to enable auto upgrade for your SQL IaaS Agent extension to ensure you're automatically receiving the latest updates to the extension every month. Review [SQL IaaS Agent Settings](manage-sql-vm-portal.md#sql-iaas-agent-extension-settings) to learn more. 
| **Microsoft Entra authentication GA** | April | Microsoft Entra authentication is now generally available. Review [Configure Microsoft Entra authentication](configure-azure-ad-authentication-for-sql-vm.md) to learn more. |  
| **Migrate AG to multi-subnet** | April | Learn how to migrate your single-subnet Always On availability group to multiple subnets to remove the reliance on an Azure Load Balancer or Distributed Network Name (DNN) to route traffic to your listener. See [Migrate availability group to a multi-subnet environment](availability-group-manually-migrate-multi-subnet.md) to learn more. | 
| **Removed extension management modes** | March | The architecture for the SQL IaaS Agent extension has been updated to remove management modes. All newly deployed SQL Server VMs are registered with the extension by using the same default configuration and least privileged security model. To learn more, review [Management modes](sql-server-iaas-agent-extension-automate-management.md#management-modes). |
| **Enable Microsoft Entra for SQL Server** |February | We've published a guide to help you enable Microsoft Entra authentication for your SQL Server VM. Review [Configure Microsoft Entra](configure-azure-ad-authentication-for-sql-vm.md) to learn more. | 
| **Extend your multi-subnet AG to multiple regions** | January | Extend an existing multi-subnet availability group, either on Azure virtual machines, or on-premises, to another region in Azure. To learn more, review [Multi-subnet availability group in multiple regions](availability-group-manually-configure-multi-subnet-multiple-regions.md). | 



## 2022

| Changes | Details |
| --- | --- |
| **Troubleshoot SQL IaaS Agent extension** | We've added an article to help you troubleshoot and address some known issues with the SQL Server IaaS agent extension. To learn more, read [Troubleshoot known issues](sql-agent-extension-troubleshoot-known-issues.md). |
| **Configure AG from Azure portal** | There is a new experience to deploy an Always On availability group to multiple subnets by using the Azure portal. The new availability group deployment method replaces the previous deployment through the SQL virtual machines resource. This feature is currently in preview. To learn more, review [Configure availability group through the Azure portal](availability-group-azure-portal-configure.md). | 
| **Microsoft Entra authentication** | It's now possible to configure Microsoft Entra authentication to your SQL Server 2022 on Azure VM by using the Azure portal. This feature is currently in preview. To get started, review [Microsoft Entra with SQL Server VMs](security-considerations-best-practices.md#azure-ad-authentication). | 
| **Least privilege permission model for SQL IaaS Agent extension** | There is a new permissions model available for the SQL Server IaaS Agent extension that grants the least privileged permission for each feature used by the extension. To learn more, review [SQL IaaS Agent extension permissions](sql-server-iaas-agent-extension-automate-management.md#permissions-models). | 
| **Confidential VMs** | SQL Server on Azure VMs has added support to deploy to [SQL Server on Azure confidential VMs](sql-vm-create-confidential-vm-how-to.md). To get started, review the [Quickstart: Deploy SQL Server to an Azure confidential VM](sql-vm-create-portal-quickstart.md?tabs=confidential-vm). 
| **Azure CLI for SQL best practices assessment**| It's now possible to configure the [SQL best practices assessment](sql-assessment-for-sql-vm.md) feature using the Azure CLI. |
| **Configure tempdb from Azure portal** | It's now possible to configure your `tempdb` settings, such as the number of files, initial size, and autogrowth ratio for an existing SQL Server instance by using the Azure portal. See [manage SQL Server VM from portal](manage-sql-vm-portal.md#storage-configuration) to learn more. |
| **SDK-style SQL projects**| Use [Microsoft.Build.Sql](https://www.nuget.org/packages/Microsoft.Build.Sql) for SDK-style SQL projects in the SQL Database Projects extension in Azure Data Studio or VS Code. This feature is currently in preview. To learn more, see [SDK-style SQL projects](/azure-data-studio/extensions/sql-database-project-extension-sdk-style-projects). |
| **Ebdsv5-series** | The new [Ebdsv5-series](/azure/virtual-machines/ebdsv5-ebsv5-series#ebdsv5-series) provides the highest I/O throughput-to-vCore ratio in Azure along with a memory-to-vCore ratio of 8. This series offers the best price-performance for SQL Server workloads on Azure VMs. Consider this series first for most SQL Server workloads. To learn more, see the updates in [VM sizes](performance-guidelines-best-practices-vm-size.md). |
| **Security best practices** | The [SQL Server VM security best practices](security-considerations-best-practices.md) have been rewritten and refreshed! |
| **Migrate with distributed AG** | It's now possible to migrate your database(s) from a [standalone instance](../../migration-guides/virtual-machines/sql-server-distributed-availability-group-migrate-standalone-instance.md) of SQL Server or an [entire availability group](../../migration-guides/virtual-machines/sql-server-distributed-availability-group-migrate-ag.md) over to SQL Server on Azure VMs using a distributed availability group! See the [prerequisites](../../migration-guides/virtual-machines/sql-server-distributed-availability-group-migrate-prerequisites.md) to get started. |

## 2021

| Changes | Details |
| --- | --- |
| **Deployment configuration improvements** | It's now possible to configure the following options when deploying your SQL Server VM from an Azure Marketplace image: System database location, number of `tempdb` data files, collation, max degree of parallelism, min and max server memory settings, and optimize for ad hoc workloads. Review [Deploy SQL Server VM](create-sql-vm-portal.md) to learn more. | 
| **Automated backup improvements** | The possible maximum automated backup retention period has changed from 30 days to 90, and you're now able to choose a specific container within the storage account. Review [automated backup](automated-backup.md) to learn more.  | 
| **Tempdb configuration** | You can now modify `tempdb` settings directly from the [SQL virtual machines](manage-sql-vm-portal.md) pane in the Azure portal, such as increasing the size, and adding data files. |
| **Eliminate need for HADR Azure Load Balancer or DNN** | Deploy your SQL Server VMs to multiple subnets to eliminate the dependency on the Azure Load Balancer or distributed network name (DNN) to route traffic to your high availability / disaster recovery (HADR) solution! See the [multi-subnet availability group](availability-group-manually-configure-prerequisites-tutorial-multi-subnet.md) tutorial, or [prepare SQL Server VM for FCI](failover-cluster-instance-prepare-vm.md#subnets) article to learn more. | 
| **SQL Assessment** | It's now possible to assess the health of your SQL Server VM in the Azure portal using [SQL Assessment](sql-assessment-for-sql-vm.md) to surface recommendations that improve performance, and identify missing best practices configurations. This feature is currently in preview. |
| **SQL IaaS Agent extension now supports Ubuntu** | Support has been added to [register](../linux/sql-iaas-agent-extension-register-vm-linux.md) your SQL Server VM running on Ubuntu Linux with the [SQL Server IaaS Extension](../linux/sql-server-iaas-agent-extension-linux.md) for limited functionality. | 
| **SQL IaaS Agent extension full mode no longer requires restart** | Restarting the SQL Server service is no longer necessary when registering your SQL Server VM with the [SQL IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md)! | 
| **Repair SQL Server IaaS extension in portal** | It's now possible to verify the status of your SQL Server IaaS Agent extension directly from the Azure portal, and [repair](sql-agent-extension-troubleshoot-known-issues.md#repair-extension) it, if necessary. | 
| **Security enhancements in the Azure portal** | Once you've enabled [Microsoft Defender for SQL](/azure/security-center/defender-for-sql-usage), you can view Security Center recommendations in the [SQL virtual machines resource in the Azure portal](manage-sql-vm-portal.md#security-center). | 
| **HADR content refresh** | We've refreshed and enhanced our high availability and disaster recovery (HADR) content! There's now an [Overview of the Windows Server Failover Cluster](hadr-windows-server-failover-cluster-overview.md), as well as a consolidated [how-to configure quorum](hadr-cluster-quorum-configure-how-to.md) for SQL Server VMs.  Additionally, we've enhanced the [cluster best practices](hadr-cluster-best-practices.md) with more comprehensive setting recommendations adopted to the cloud.| 
| **Migrate high availability to VM** | Azure Migrate brings support to lift and shift your entire high availability solution to SQL Server on Azure VMs! Bring your [availability group](../../migration-guides/virtual-machines/sql-server-availability-group-to-sql-on-azure-vm.md) or your [failover cluster instance](../../migration-guides/virtual-machines/sql-server-failover-cluster-instance-to-sql-on-azure-vm.md) to SQL Server VMs using Azure Migrate today! 
| **Performance best practices refresh** | We've rewritten, refreshed, and updated the performance best practices documentation, splitting one article into a series that contains: [a checklist](performance-guidelines-best-practices-checklist.md), [VM size guidance](performance-guidelines-best-practices-vm-size.md), [Storage guidance](performance-guidelines-best-practices-storage.md), and [collecting baseline instructions](performance-guidelines-best-practices-collect-baseline.md). |


## 2020

| Changes | Details |
| --- | --- |
| **Azure Government support** | It's now possible to register SQL Server virtual machines with the SQL IaaS Agent extension for virtual machines hosted in the [Azure Government](https://azure.microsoft.com/global-infrastructure/government/) cloud. | 
| **Azure SQL family** | SQL Server on Azure Virtual Machines is now a part of the [Azure SQL family of products](../../azure-sql-iaas-vs-paas-what-is-overview.md). Check out our [new look](../index.yml)! Nothing has changed in the product, but the documentation aims to make the Azure SQL product decision easier. | 
| **Distributed network name (DNN)** | SQL Server 2019 on Windows Server 2016+ is now previewing support for routing traffic to your failover cluster instance (FCI) by using a [distributed network name](./failover-cluster-instance-distributed-network-name-dnn-configure.md) rather than using Azure Load Balancer. This support simplifies and streamlines connecting to your high-availability (HA) solution in Azure. | 
| **FCI with Azure shared disks** | It's now possible to deploy your [failover cluster instance (FCI)](failover-cluster-instance-overview.md) by using [Azure shared disks](failover-cluster-instance-azure-shared-disks-manually-configure.md). |
| **Reorganized FCI docs** | The documentation around [failover cluster instances with SQL Server on Azure VMs](failover-cluster-instance-overview.md) has been rewritten and reorganized for clarity. We've separated some of the configuration content, like the [cluster configuration best practices](hadr-cluster-best-practices.md), how to prepare a [virtual machine for a SQL Server FCI](failover-cluster-instance-prepare-vm.md), and how to configure [Azure Load Balancer](./availability-group-vnn-azure-load-balancer-configure.md). | 
| **Migrate log to ultra disk** | Learn how you can [migrate your log file to an ultra disk](storage-migrate-to-ultradisk.md) to leverage high performance and low latency. | 
| **Create availability group using Azure PowerShell** | It's now possible to simplify the creation of an availability group by using [Azure PowerShell](availability-group-az-commandline-configure.md) as well as the Azure CLI. | 
| **Configure availability group in portal** | It's now possible to [configure your availability group via the Azure portal](availability-group-azure-portal-configure.md). This feature is currently in preview and being deployed so if your desired region is unavailable, check back soon. | 
| **Automatic extension registration** | You can now enable the [Automatic registration](sql-agent-extension-automatic-registration-all-vms.md) feature to automatically register all SQL Server VMs already deployed to your subscription with the [SQL IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md). This applies to all existing VMs, and will also automatically register all SQL Server VMs added in the future.   | 
| **DNN for availability group** | You can now configure a [distributed network name (DNN) listener)](availability-group-distributed-network-name-dnn-listener-configure.md) for SQL Server 2019 CU8 and later to replace the traditional [VNN listener](availability-group-overview.md#connectivity), negating the need for an Azure Load Balancer.   | 

## 2019

|Changes | Details |
 --- | --- |
| **Free DR replica in Azure** | You can host a [free passive instance](business-continuity-high-availability-disaster-recovery-hadr-overview.md#free-dr-replica-in-azure) for disaster recovery in Azure for your on-premises SQL Server instance if you have [Software Assurance](https://www.microsoft.com/licensing/licensing-programs/software-assurance-default?rtc=1&activetab=software-assurance-default-pivot:primaryr3). | 
| **Bulk SQL IaaS Agent extension registration** | You can now [bulk register](sql-agent-extension-manually-register-vms-bulk.md) SQL Server virtual machines with the [SQL IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md). | 
|**Performance-optimized storage configuration** | You can now [fully customize your storage configuration](storage-configuration.md#new-vms) when creating a new SQL Server VM. |
|**Premium file share for FCI** | You can now create a failover cluster instance by using a [Premium file share](failover-cluster-instance-premium-file-share-manually-configure.md) instead of the original method of [Storage Spaces Direct](failover-cluster-instance-storage-spaces-direct-manually-configure.md). 
| **Azure Dedicated Host** | You can run your SQL Server VM on [Azure Dedicated Host](dedicated-host.md). | 
| **SQL Server VM migration to a different region** | Use Azure Site Recovery to [migrate your SQL Server VM from one region to another](move-sql-vm-different-region.md). |
|  **New SQL IaaS installation modes** | It's now possible to install the SQL Server IaaS extension in [lightweight mode](sql-server-iaas-agent-extension-automate-management.md) to avoid restarting the SQL Server service.  |
| **SQL Server edition modification** | You can now change the [edition property](change-sql-server-edition.md) for your SQL Server VM. |
| **Changes to the SQL IaaS Agent extension** | You can [register your SQL Server VM with the SQL IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md) by using the new SQL IaaS modes. This capability includes [Windows Server 2008](sql-server-iaas-agent-extension-automate-management.md#management-modes) images.|
| **Bring-your-own-license images using Azure Hybrid Benefit** | Bring-your-own-license images deployed from Azure Marketplace can now switch their [license type to pay-as-you-go](licensing-model-azure-hybrid-benefit-ahb-change.md#remarks).| 
| **New SQL Server VM management in the Azure portal** | There's now a way to manage your SQL Server VM in the Azure portal. For more information, see [Manage SQL Server VMs in the Azure portal](manage-sql-vm-portal.md).  | 
| **Extended support for SQL Server 2008 and 2008 R2** | [Extend support](sql-server-2008-extend-end-of-support.md) for SQL Server 2008 and SQL Server 2008 R2 by migrating *as is* to an Azure VM. | 
| **Custom image supportability** | You can now install the [SQL Server IaaS extension](sql-server-iaas-agent-extension-automate-management.md#installation) to custom OS and SQL Server images, which offers the limited functionality of [flexible licensing](licensing-model-azure-hybrid-benefit-ahb-change.md). When you're registering your custom image with the SQL IaaS Agent extension, specify the license type as "AHUB." Otherwise, the registration will fail. | 
| **Named instance supportability** | You can now use the [SQL Server IaaS extension](sql-server-iaas-agent-extension-automate-management.md#installation) with a named instance, if the default instance has been uninstalled properly. | 
| **Portal enhancement** | The Azure portal experience for deploying a SQL Server VM has been revamped to improve usability. For more information, see the brief [quickstart](sql-vm-create-portal-quickstart.md) and more thorough [how-to guide](create-sql-vm-portal.md) to deploy a SQL Server VM.|
| **Portal improvement** | It's now possible to change the licensing model for a SQL Server VM from pay-as-you-go to bring-your-own-license by using the [Azure portal](licensing-model-azure-hybrid-benefit-ahb-change.md#change-license-model).|
| **Simplification of availability group deployment to a SQL Server VM through the Azure CLI** | It's now easier than ever to deploy an availability group to a SQL Server VM in Azure. You can use the [Azure CLI](/cli/azure/sql/vm?view=azure-cli-2018-03-01-hybrid&preserve-view=true) to create the Windows failover cluster, internal load balancer, and availability group listeners, all from the command line. For more information, see [Use the Azure CLI to configure an Always On availability group for SQL Server on an Azure VM](./availability-group-az-commandline-configure.md). | 
| &nbsp; | &nbsp; |

## 2018 

 Changes | Details |
| --- | --- |
|  **New resource provider for a SQL Server cluster** | A new resource provider (Microsoft.SqlVirtualMachine/SqlVirtualMachineGroups) defines the metadata of the Windows failover cluster. Joining a SQL Server VM to *SqlVirtualMachineGroups* bootstraps the Windows Server Failover Cluster (WSFC) service and joins the VM to the cluster.  |
| **Automated setup of an availability group deployment with Azure Quickstart Templates** |It's now possible to create the Windows failover cluster, join SQL Server VMs to it, create the listener, and configure the internal load balancer by using two Azure Quickstart Templates. For more information, see [Use Azure Quickstart Templates to configure an Always On availability group for SQL Server on an Azure VM](availability-group-quickstart-template-configure.md). | 
| **Automatic registration to the SQL IaaS Agent extension** | SQL Server VMs deployed after this month are automatically registered with the new SQL IaaS Agent extension. SQL Server VMs deployed before this month still need to be manually registered. For more information, see [Register a SQL Server virtual machine in Azure with the SQL IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md).|
|**New SQL IaaS Agent extension** |  A new resource provider (Microsoft.SqlVirtualMachine) provides better management of your SQL Server VMs. For more information on registering your VMs, see [Register a SQL Server virtual machine in Azure with the SQL IaaS Agent extension](sql-agent-extension-manually-register-single-vm.md). |
|**Switch licensing model** | You can now switch between the pay-per-usage and bring-your-own-license models for your SQL Server VM by using the Azure CLI or PowerShell. For more information, see [How to change the licensing model for a SQL Server virtual machine in Azure](licensing-model-azure-hybrid-benefit-ahb-change.md). | 
| &nbsp; | &nbsp; |


## Contribute to content

To contribute to the Azure SQL documentation, see the [Docs contributor guide](/contribute/).

## Additional resources

**Windows VMs**:

* [Overview of SQL Server on Windows VMs](sql-server-on-azure-vm-iaas-what-is-overview.md)
* [Provision SQL Server on Windows VMs](create-sql-vm-portal.md)
* [Migrate a database to SQL Server on an Azure VM](migrate-to-vm-from-sql-server.md)
* [High availability and disaster recovery for SQL Server on Azure Virtual Machines](business-continuity-high-availability-disaster-recovery-hadr-overview.md)
* [Performance best practices for SQL Server on Azure Virtual Machines](./performance-guidelines-best-practices-checklist.md)
* [Application patterns and development strategies for SQL Server on Azure Virtual Machines](application-patterns-development-strategies.md)

**Linux VMs**:

* [Overview of SQL Server on a Linux VM](../linux/sql-server-on-linux-vm-what-is-iaas-overview.md)
* [Provision SQL Server on a Linux virtual machine](../linux/sql-vm-create-portal-quickstart.md)
* [FAQ (Linux)](../linux/frequently-asked-questions-faq.yml)
* [SQL Server on Linux documentation](/sql/linux/sql-server-linux-overview)
