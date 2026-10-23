---
title: Overview of SQL Server on Azure Windows Virtual Machines
description: Learn how to run full editions of SQL Server on Azure Virtual Machines in the cloud without having to manage any on-premises hardware.
author: bluefooted
ms.author: pamela
ms.reviewer: vanto
ms.date: 04/02/2026
ms.service: azure-vm-sql-server
ms.subservice: service-overview
ms.topic: overview
ms.custom:
  - ignite-2025
tags: azure-service-management
---
# What is SQL Server on Azure Windows Virtual Machines?

[!INCLUDE [appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

> [!div class="op_single_selector"]
> - [Windows](sql-server-on-azure-vm-iaas-what-is-overview.md)
> - [Linux](../linux/sql-server-on-linux-vm-what-is-iaas-overview.md)

This article provides an overview of SQL Server on Azure Virtual Machines (VMs) on the Windows platform.

If you're new to SQL Server on Azure VMs, check out the *SQL Server on Azure VM Overview* video from our in-depth [Azure SQL video series](/shows/Azure-SQL-for-Beginners?WT.mc_id=azuresql4beg_azuresql-ch9-niner):

> [!VIDEO https://learn.microsoft.com/shows/Azure-SQL-for-Beginners/SQL-Server-on-Azure-VM-Overview-4-of-61/player]

## Overview

[SQL Server on Azure Virtual Machines](https://azure.microsoft.com/services/virtual-machines/sql-server/) enables you to use full versions of SQL Server in the cloud without having to manage any on-premises hardware. SQL Server virtual machines (VMs) also simplify licensing costs when you pay as you go.

Azure virtual machines run in many different [geographic regions](https://azure.microsoft.com/regions/) around the world. They also offer various [machine sizes](/azure/virtual-machines/sizes). The virtual machine image gallery allows you to create a SQL Server VM with the right version, edition, and operating system. This makes virtual machines a good option for many different SQL Server workloads.

**Ready to get started?** Jump to our [quickstart guide](sql-vm-create-portal-quickstart.md) to create your first SQL Server VM, or explore the [feature benefits](#feature-benefits) you unlock by registering with the SQL IaaS Agent extension.

## Feature benefits

When you register your SQL Server on Azure VM with the [SQL IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md), you unlock a number of feature benefits. Registering with the extension is free.

The following table details the benefits unlocked by the extension:

[!INCLUDE [SQL VM feature benefits](../../includes/sql-vm-feature-benefits.md)]

## Getting started

To get started with SQL Server on Azure VMs, review the following resources:

- **Create your first SQL Server VM**: Get up and running quickly with our step-by-step quickstarts for the [Azure portal](sql-vm-create-portal-quickstart.md), [Azure PowerShell](sql-vm-create-powershell-quickstart.md), or [ARM template](create-sql-vm-resource-manager-template.md). For comprehensive setup guidance, see the complete [provisioning guide](create-sql-vm-portal.md).
- **Connect to SQL VM**: To connect to your SQL Server on Azure VMs, review the [ways to connect](ways-to-connect-to-sql.md).
- **Unlock feature benefits**: Register your SQL Server VM with the [SQL IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md) to unlock a number of feature benefits, such as automated backup and patching. 
- **Manage SQL VM from the portal**: Manage SQL Server settings directly from the Azure portal by using the [SQL virtual machines](manage-sql-vm-portal.md) resource.
- **Optimize performance**: Use the [SQL best practices assessment](sql-assessment-for-sql-vm.md) to validate that you're following [Best practices](performance-guidelines-best-practices-checklist.md) and use [I/O Performance Analysis](storage-performance-analysis.md) to investigate I/O issues. 
- **Migrate data**: Migrate your data to SQL Server on Azure VMs from [SQL Server](/data-migration/sql-server/virtual-machines/overview), [Oracle](../../migration-guides/virtual-machines/oracle-to-sql-on-azure-vm-guide.md), or [Db2](../../migration-guides/virtual-machines/db2-to-sql-on-azure-vm-guide.md).
- **Pricing**: For information about the pricing structure of your SQL Server on Azure VM, review the [Pricing guidance](pricing-guidance.md).
- **Frequently asked questions**: For commonly asked questions and scenarios, review the [FAQ](frequently-asked-questions-faq.yml).

## Videos

For videos about the latest features to optimize SQL Server VM performance and automate management, review the following Data Exposed videos:

- [Caching and Storage Capping](/shows/data-exposed/azure-sql-vm-caching-and-storage-capping-ep-1-data-exposed)
- [Automate Management with the SQL Server IaaS Agent extension](/shows/data-exposed/azure-sql-vm-automate-management-with-the-sql-server-iaas-agent-extension-ep-2)
- [Use Azure Monitor Metrics to Track VM Cache Health](/shows/data-exposed/azure-sql-vm-use-azure-monitor-metrics-to-track-vm-cache-health-ep-3)
- [Get the best price-performance for your SQL Server workloads on Azure VM](/shows/data-exposed/azure-sql-vm-get-the-best-price-performance-for-your-sql-server-workloads-on-azure-vm)
- [Using PerfInsights to Evaluate Resource Health and Troubleshoot](/shows/data-exposed/azure-sql-vm-using-perfinsights-to-evaluate-resource-health-and-troubleshoot-ep-5)
- [Best Price-Performance with Ebdsv5 Series](/shows/data-exposed/azure-sql-vm-best-price-performance-with-ebdsv5-series)
- [Optimally Configure SQL Server on Azure Virtual Machines with SQL Assessment](/shows/data-exposed/optimally-configure-sql-server-on-azure-virtual-machines-with-sql-assessment)
- [New and Improved SQL Server on Azure VM deployment and management experience](/shows/data-exposed/new-and-improved-sql-on-azure-vm-deployment-and-management-experience)

## High availability and disaster recovery

In addition to the built-in [high availability provided by Azure Virtual Machines](/azure/virtual-machines/availability), you can use the high availability and disaster recovery features provided by SQL Server.

For more information, see the overview of [Always On availability groups](availability-group-overview.md) and [Always On failover cluster instances](failover-cluster-instance-overview.md). For more information, see the [business continuity overview](business-continuity-high-availability-disaster-recovery-hadr-overview.md).

To get started, see the tutorials for [availability groups](availability-group-manually-configure-prerequisites-tutorial-multi-subnet.md) or [preparing your VM for a failover cluster instance](failover-cluster-instance-prepare-vm.md).

<a name="licensing"></a>

## SQL VM images and licensing

To get started, choose a SQL Server virtual machine image with your required version, edition, and operating system. The following sections provide direct links to the Azure portal for the SQL Server virtual machine gallery images. Change the licensing model of a pay-per-usage SQL Server VM to use your own license. For more information, see [How to change the licensing model for a SQL Server VM](licensing-model-azure-hybrid-benefit-ahb-change.md).

Azure only maintains one virtual machine image for each supported operating system, version, and edition combination. This maintenance means that over time, Microsoft refreshes images and removes older images. For more information, see the **Images** section of the [SQL Server VMs FAQ](frequently-asked-questions-faq.yml).

> [!TIP]  
> For more information about how to understand pricing for SQL Server images, see [Pricing guidance for SQL Server on Azure Virtual Machines](pricing-guidance.md).

The following table provides a matrix of pay-as-you-go SQL Server images.

| Version | Operating System |
| --- | --- |
| **SQL Server 2025**| [Windows Server 2025](https://portal.azure.com/#create/microsoftsqlserver.sql2025-ws2025entdev-gen2) |
| **SQL Server 2022** | [Windows Server 2022](https://portal.azure.com/#create/microsoftsqlserver.sql2022-ws2022enterprise-gen2) |
| **SQL Server 2019** | [Windows Server 2022](https://portal.azure.com/#create/microsoftsqlserver.sql2019-ws2022enterprise-gen2), [Windows Server 2019](https://portal.azure.com/#create/microsoftsqlserver.sql2019-ws2019enterprise-ARM) |
| **SQL Server 2017** | [Windows Server 2019](https://portal.azure.com/#create/microsoftsqlserver.sql2017-ws2019enterprise-ARM), [Windows Server 2016](https://portal.azure.com/#create/Microsoft.SQLServer2017EnterpriseWindowsServer2016-ARM) |
| **SQL Server 2016** | [Windows Server 2019](https://portal.azure.com/#create/microsoftsqlserver.sql2016sp3-ws2019enterprise), [Windows Server 2016](https://portal.azure.com/#create/Microsoft.SQLServer2016SP2EnterpriseWindowsServer2016-ARM) |

[!INCLUDE [appliesto-sqlvm](../../includes/virtual-machines-2014-end-of-support.md)]

To see the available SQL Server on Linux virtual machine images, see [Overview of SQL Server on Azure Virtual Machines (Linux)](../linux/sql-server-on-linux-vm-what-is-iaas-overview.md).

You can deploy an older image of SQL Server that isn't available in the Azure portal by using PowerShell. To view all available images by using PowerShell, use the following command:

```powershell
$Location = "<location>"
Get-AzVMImageOffer -Location $Location -Publisher 'MicrosoftSQLServer'
```

For more information about deploying SQL Server VMs by using PowerShell, see [How to provision SQL Server virtual machines with Azure PowerShell](create-sql-vm-powershell.md).

> [!IMPORTANT]  
> SQL Server images from Azure Marketplace deploy at RTM and aren't updated over time. For more information, see [Updating SQL Server on Azure VMs](servicing-updates-guidelines.md).

## Azure Hybrid Benefit

Azure Hybrid Benefit grants you the ability to allocate your SQL Server license to your SQL Server on Azure VM. With Azure Hybrid Benefit, you get a discount on the allocation of SQL Server licenses to the SQL Server instance but must still pay for the cost of the underlying cloud compute (that is, the base rate), storage, and backups. You must also pay for I/O associated with their use of the services (as applicable).

To estimate the cost of Pay-as-you-go licensing and see cost savings with the Azure Hybrid benefit use the [Pricing calculator](https://azure.microsoft.com/pricing/calculator/).

According to the Microsoft [Product Terms](https://www.microsoft.com/licensing/terms/productoffering/MicrosoftAzureServices/EAEAS): "Customers must indicate that they're using Azure SQL Database (SQL Managed Instance, Elastic Pool, and Single Database), Azure Data Factory, SQL Server Integration Services, or SQL Server Virtual Machines under Azure Hybrid Benefit for SQL Server when configuring workloads on Azure."

To indicate the use of Azure Hybrid Benefit for SQL Server on Azure VM and be compliant, you have two options:

- Provision a virtual machine by using a pay-as-you-go SQL Server image from Azure Marketplace and [activate the Azure Hybrid Benefit](licensing-model-azure-hybrid-benefit-ahb-change.md#change-license-model).
- Self-install SQL Server on Azure VM, manually [register with the SQL IaaS Agent Extension](sql-agent-extension-manually-register-single-vm.md), and activate Azure Hybrid Benefit.

The license type of SQL Server can be configured when the VM is provisioned or anytime afterward. Switching between license models incurs no downtime, doesn't restart the VM or the SQL Server service, doesn't add any additional costs, and is effective immediately. In fact, activating Azure Hybrid Benefit *reduces* cost.

## Customer experience improvement program (CEIP)

The Customer Experience Improvement Program (CEIP) is enabled by default. This program periodically sends reports to Microsoft to help improve SQL Server. You don't need to manage CEIP unless you want to disable it after provisioning. You can customize or disable the CEIP by connecting to the VM with [Bastion](/azure/bastion/bastion-overview). Then run the **SQL Server Error and Usage Reporting** utility. Follow the instructions to disable reporting. For more information about data collection, see the [SQL Server Privacy Statement](/sql/sql-server/sql-server-privacy).

## Related products and services

Since SQL Server on Azure VMs is integrated into the Azure platform, review resources from related products and services that interact with the SQL Server on Azure VM ecosystem:

- **AI capabilities and features**: 
    - [Intelligent applications and AI in SQL Server](/sql/sql-server/ai/artificial-intelligence-intelligent-applications?view=azuresqldb-mi-current&preserve-view=true)
    - [Multi-model capabilities in Azure SQL](../../multi-model-features.md)
    - [Connect to REST API endpoints for a SQL database](/azure/data-api-builder/concept/rest/overview)
    - [Connect to GraphQL endpoints for a SQL database](/azure/data-api-builder/concept/graphql/overview)
    - [SQL MCP Server](/azure/data-api-builder/mcp/overview)
- **Windows virtual machines**: [Azure Virtual Machines overview](/azure/virtual-machines/windows/overview)
- **Storage**: [Introduction to Azure Storage](/azure/storage/common/storage-introduction)
- **Networking**: [Azure Virtual Network overview](/azure/virtual-network/virtual-networks-overview), [IP addresses in Azure](/azure/virtual-network/ip-services/public-ip-addresses), [Create a Fully Qualified Domain Name in the Azure portal](/azure/virtual-machines/create-fqdn)
- **SQL**: [SQL Server documentation](/sql/index), [Azure SQL Database comparison](../../azure-sql-iaas-vs-paas-what-is-overview.md)

## Related content

- [Updating SQL Server on Azure VMs](servicing-updates-guidelines.md)
- [Quickstart: Create SQL Server VM in the Azure portal](sql-vm-create-portal-quickstart.md)
- [SQL Server on Azure Virtual Machines FAQ](frequently-asked-questions-faq.yml)
- [Windows N-tier application on Azure with SQL Server](/azure/architecture/reference-architectures/n-tier/n-tier-sql-server)
- [Run an N-tier application in multiple Azure regions for high availability](/azure/architecture/reference-architectures/n-tier/multi-region-sql-server)
