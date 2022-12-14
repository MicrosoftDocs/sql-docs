---
title: "What are Extended Security Updates?"
description: Learn how to use Azure Arc to get extended security updates for your end-of-support and end-of-life SQL Server products, such as SQL Server 2008, SQL Server 2008 R2, and SQL Server 2012.
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/01/2022
ms.service: sql
ms.subservice: install
ms.topic: conceptual
ms.custom: references_regions
monikerRange: ">=sql-server-2016"
---
# What are Extended Security Updates for SQL Server?

[!INCLUDE [SQL Server end of support](../../includes/applies-to-version/sql-migration-end-of-support.md)]

This article provides information for using [Azure Arc](/azure/azure-arc/overview) to receive Extended Security Updates (ESUs) for versions of SQL Server that are out of extended support.

[!INCLUDE [esu-table](includes/esu-table.md)]

> [!WARNING]  
> Effective July 12, 2022, the SQL Registry portal has been retired. Please use the new Azure portal as described below to connect and/or register your SQL Server instances that qualify for Extended Security Updates (ESUs).

> [!TIP]  
> Customers on [!INCLUDE[SQL Server 2008](../../includes/ssKatmai-md.md)] and [!INCLUDE[SQL Server 2008 R2](../../includes/ssKilimanjaro-md.md)] can migrate to Azure services if they wish to continue receiving Extended Security Updates, until [July 12, 2023](https://www.microsoft.com/windows-server/extended-security-updates). See the [Overview](#overview) for more information.

For more information about other options, see [End of support options](sql-server-end-of-support-overview.md).

## Overview

Once [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has reached the end of its support lifecycle, you can sign up for an Extended Security Update (ESU) subscription for your servers and remain protected for up to three years, until you're ready to upgrade to a newer version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [migrate to Azure SQL](/azure/azure-sql/migration-guides/).

You can receive Extended Security Updates in several ways:

- **Azure Arc**. Purchased for your on-premises or hosted environment. You'll download updates when they're available. There are two ways to use Azure Arc:

  - **Connected**. Install the Azure Connected Machine agent along with the Azure extension for SQL Server, with direct connectivity to Azure. You'll benefit from the features that [Azure Arc-enabled SQL Server](../azure-arc/overview.md) provides.

  - **Registered**. Manually add your instance using a process similar to the deprecated SQL Server registry. The instance will be added in a *disconnected* state. See [below](#prerequisites) for required prerequisites.

- **Azure services**. Free and enabled by default when migrating on-premises servers to one of the following Azure services:

  - [SQL Server on Azure Virtual Machines](/azure/azure-sql/virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview)

  - [Azure Stack HCI](/azure-stack/hci)

  - [Azure Stack Hub](/azure-stack#azure-stack-hub)

  - [Azure VMware Solution &#40;AVS&#41;](/azure/azure-vmware/)

  - [Nutanix Cloud Clusters on Azure](https://nutanix.com/azure/)

[!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends applying ESU patches as soon as they're available to keep your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance protected. For detailed information about ESUs, see the [ESU FAQ page](https://www.microsoft.com/cloud-platform/extended-security-updates).

## Extended support dates

For the versions in the table below, consider using [Extended Security Updates](/azure/azure-sql/virtual-machines/windows/sql-server-extend-end-of-support) described in this article, or other migration options. For more information, see [End of support options](sql-server-end-of-support-overview.md).

| SQL Server version | Extended Support end date |
| --- | --- |
| SQL Server 2012 | July 12, 2022 |
| SQL Server 2008 R2 | July 10, 2019 |
| SQL Server 2008 | July 10, 2019 |

## What are Extended Security Updates

Extended Security Updates (ESUs) include security updates for customers who have purchased an Extended Support Update subscription, and are available for [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].

> [!TIP]  
> Customers on [!INCLUDE[SQL Server 2008](../../includes/ssKatmai-md.md)] and [!INCLUDE[SQL Server 2008 R2](../../includes/ssKilimanjaro-md.md)] can migrate to Azure services if they wish to continue receiving Extended Security Updates, until [July 12, 2023](https://www.microsoft.com/windows-server/extended-security-updates). See the [Overview](#overview) for more information.

ESUs are made available **if needed**, once a security vulnerability is discovered and is rated as **Critical** by the [Microsoft Security Response Center (MSRC)](https://portal.msrc.microsoft.com). Therefore, there's no regular release cadence for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ESUs.

ESUs don't include:

- New features
- Functional improvements
- Customer-requested fixes

### Support

ESUs don't include technical support, but you can use an active support contract such as [Software Assurance](https://www.microsoft.com/licensing/licensing-programs/software-assurance-default?activetab=software-assurance-default-pivot%3aprimaryr3) or Premier/Unified Support to get technical support on workloads covered by ESUs if you choose to stay on-premises. Alternatively, if you're hosting on Azure, you can use an Azure Support plan to get technical support.

[!INCLUDE[msCoName](../../includes/msconame-md.md)] can't provide technical support for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances (both on-premises, and in hosting environments) that aren't covered with an ESU subscription.

## ESU availability and deployment

ESUs are available to customers running their workload in Azure, on-premises, or hosted environments.

### Azure workloads

If you migrate your workloads to an Azure service (see the [Overview](#overview) for more information), you'll have access to ESUs for [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] for up to three years after the End of Support, at **no additional charges** above the cost of running the Azure service. You don't need Software Assurance to receive ESUs in Azure.

Azure services running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will receive ESUs automatically through existing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] update channels or Windows Update. You don't need to install the SQL Server IaaS Agent extension to download ESU patches on an Azure SQL Virtual Machine.

### On-premises or hosted environments

If you have Software Assurance, you can purchase an Extended Security Update (ESU) subscription for up to three years after the End of Support date, under an Enterprise Agreement (EA), Enterprise Subscription Agreement (EAS), a Server & Cloud Enrollment (SCE), or an Enrollment for Education Solutions (EES). You can purchase ESUs only for the servers you need to cover. ESUs can be purchased directly from [!INCLUDE[msCoName](../../includes/msconame-md.md)] or a [!INCLUDE[msCoName](../../includes/msconame-md.md)] licensing partner.

Customers covered by ESU agreements must follow these steps to download and deploy an ESU patch. The process is the same for Azure Stack and Azure Virtual Machines that aren't configured to receive automatic updates:

- [Register eligible instances](#register-instances-for-esus).
- Once registered, whenever ESU patches are released, a download link will be available in the Azure portal to download the package.
- The downloaded package can be deployed to your on-premises or hosted environments manually, or through the update orchestration solution you use in your organization, such as Microsoft Endpoint Configuration Manager.

For more information, see the [Extended Security Updates frequently asked questions](https://www.microsoft.com/cloud-platform/extended-security-updates).

## <a id="register-instances-for-esus"></a> Register disconnected SQL Server instances for ESUs

This example shows you how to manually add your SQL Server instances in a disconnected state to Azure Arc. If you would prefer to add your server as an Azure Arc-enabled server running the Connected Machine agent, see [Connect hybrid machines with Azure Arc-enabled servers](/azure/azure-arc/servers/learn/quick-enable-hybrid-vm) instead.

### Prerequisites

1. If you don't already have an Azure subscription, you can create an account using one of the following methods:

   - [Create a Microsoft Customer Agreement subscription](/azure/cost-management-billing/manage/create-subscription)
   - [Create an Enterprise Agreement subscription](/azure/cost-management-billing/manage/create-enterprise-subscription)
   - [Create an Azure account with pay-as-you-go pricing](https://azure.microsoft.com/pricing/purchase-options/pay-as-you-go/)
   - [Create a free Azure account](https://azure.microsoft.com/free/)

1. The user creating disconnected Arc-enabled SQL Server resources must have the following permissions:

   - `Microsoft.AzureArcData/sqlServerInstances/read`
   - `Microsoft.AzureArcData/sqlServerInstances/write`

   Users can be assigned to the `Azure Connected SQL Server Onboarding` role to get those specific permissions, or they can be assigned to built-in roles such as Contributor or Owner that have these permissions. See [Assign Azure roles using the Azure portal](/azure/role-based-access-control/role-assignments-portal) for more information.

1. Register the `Microsoft.AzureArcData` resource provider in your Azure subscription:

   - Sign in to the Azure portal.

   - Navigate to your subscription, and select **Resource providers**.

   - If the `Microsoft.AzureArcData` resource provider isn't listed, you can add it to your subscription using the **Register** option.

1. If you are using Azure policies that only allow the creation of specific resource types, you will need to allow the `Microsoft.AzureArcData/sqlServerInstances` resource type. If it isn't allowed, the `SQLServerInstances_Update` operation will fail with a **'deny' Policy action** log entry in the activity log of the subscription.

You can either register a [single SQL Server instance](#single-sql-server-instance), or upload a CSV file to register [multiple SQL Server instances in bulk](#multiple-sql-server-instances-in-bulk).

### Single SQL Server instance

1. Sign into the [Azure portal](https://portal.azure.com).

1. Navigate to **Azure Arc** and select **Infrastructure** > **SQL Servers**.

1. To register a disconnected machine, select **Add** from the menu at the top of the screen.

    :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-empty-list.png" alt-text="Screenshot of an empty list of SQL Servers list on the Azure Arc portal.":::

1. Select **Register Servers** to add a disconnected SQL Server instance.

    :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-add-connected-or-registered.png" alt-text="Screenshot of the two options for adding connected or registered servers.":::

1. On the next screen, you can choose to add a single or multiple SQL Server instances. The option for **Single SQL Instance** is selected by default.

    :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-add-sql-registration-options.png" alt-text="Screenshot of the Add SQL Registrations options.":::

1. Choose the **Subscription** and **Resource group** for your registered SQL Server instance.

1. Provide the required information as is detailed in this table, and then select **Next**:

   |Value|Description|Additional information|
   |---|---|---|
   |**Instance Name**|Enter the output of command `SELECT @@SERVERNAME`, such as `MyServer\Instance01`.|If you have a named instance, you must replace the backslash (`\`) with a hyphen (`-`). For example, `MyServer\Instance01` will become `MyServer-Instance01`.|
   |**SQL Server Version**|Select your version from the drop-down.||
   |**Edition**| Select the applicable edition from the drop-down: Datacenter, Developer (free to deploy if purchased ESUs), Enterprise, Standard, Web, Workgroup.||
   |**Cores**|Enter the number of cores for this instance||
   |**Host Type**|Select the applicable host type from the drop-down: Virtual machine (on-premises), Physical Server (on-premises), Azure Virtual Machine, Amazon EC2, Google Compute Engine, Other.||

1. You must confirm that you have the rights to receive Extended Security Updates, using the checkbox provided.

Now you can continue to the [Confirmation](#confirmation) section.

### Multiple SQL Server instances in bulk

Multiple [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances can be registered in bulk by uploading a .CSV file. Once your [.CSV file has been formatted correctly](#formatting-requirements-for-csv-file), you can follow these steps to bulk register your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances with Azure Arc:

1. Sign into the [Azure portal](https://portal.azure.com).

1. Navigate to **Azure Arc** and select **Infrastructure** > **SQL Servers**.

1. To register a disconnected machine, select **Add** from the menu at the top of the screen.

    :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-empty-list.png" alt-text="Screenshot of an empty list of SQL Servers list on the Azure Arc portal.":::

1. Select **Register Servers** to add a disconnected SQL Server instance.

    :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-add-connected-or-registered.png" alt-text="Screenshot of the two options for adding connected or registered servers.":::

1. On this screen, you can choose to add a single or multiple SQL Server instances. Select the option for **Multiple SQL Instances**.

    :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-multiple-sql-instances.png" alt-text="Screenshot of the Multiple SQL Instances option.":::

1. Select the Browse icon to upload the CSV file containing multiple disconnected SQL Server instances.

1. You must confirm that you have the rights to receive Extended Security Updates, using the checkbox provided.

Now you can continue to the [Confirmation](#confirmation) section.

### Confirmation

1. We recommend using the `Year1OrderID` tag to link your SQL Server instances to your ESU invoice number for easy reference. The `Year1EntitlementConfirmed` tag is automatically filled in.

    > [!NOTE]
    > If you use Azure services such as Azure Dedicated Host, Azure VMware Solution, Azure Nutanix Solution, and Azure Stack (Hub, Edge, and HCI), you can set the ESU invoice number to `InvoiceNotNeeded`.

    :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-tags.png" alt-text="Screenshot of confirmation tags.":::

1. Before you can add your SQL Server instances, you must agree to the terms of use and privacy policy.

    :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-terms.png" alt-text="Screenshot of the terms of use.":::

1. Once you've added your SQL Server instances, you'll see them in the portal after a few minutes. Because they were added manually, they'll always show in a disconnected state, with the description **Registered**.

    :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-connected-servers.png" alt-text="Screenshot of two registered SQL Server instances on the Azure Arc portal.":::

### Link ESU invoice to registered servers

Customers can use the **Purchase Order Number** under Invoice Summary in their Microsoft invoice (as shown in the screenshot below) to link the ESU purchase with the registration of SQL Server instances.

:::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-invoice-sample.png" alt-text="Sample invoice with Purchase Order Number highlighted.":::

Follow these steps to link an ESU invoice to your Azure Arc SQL Server instances to get access to extended updates. This example includes both **Connected** and **Registered** servers.

1. Sign into the [Azure portal](https://portal.azure.com).

1. Navigate to **Azure Arc** and select **Infrastructure** > **SQL Servers**.

1. Use the checkboxes next to each server to select which machines you would like to link, and then select **Link ESU invoice**.

    :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-invoice-select.png" alt-text="Screenshot of all SQL Server instances on the Azure Arc section.":::

1. Fill in the ESU invoice number in the **Invoice ID** section, and then select **Link invoice**.

    :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-invoice-save.png" alt-text="Screenshot of the invoice ID on the Link ESU invoice page.":::

1. The servers you linked to the ESU invoice now show a valid ESU expiration date.

    :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-invoice-linked.png" alt-text="Screenshot of SQL Server instances with a valid ESU expiration value.":::

### Formatting requirements for CSV file

- Values are comma-separated
- Values aren't single or double-quoted
- Values can include letters, numbers, hyphens (`-`), and underscores (`_`). No other special characters can be used. If you have a named instance, you must replace the backslash (`\`) with a hyphen (`-`). For example, `MyServer\Instance01` will become `MyServer-Instance01`.
- Column names are case-sensitive and must be named as follows:

  - name
  - version
  - edition
  - cores
  - hostType

#### Example CSV file

The CSV file should look like this:

```csv
name,version,edition,cores,hostType
Server1-SQL2012,SQL Server 2012,Enterprise,12,Other Physical Server
Server2-SQL2012,SQL Server 2012,Enterprise,24,Other Physical Server
Server3-SQL2012,SQL Server 2012,Enterprise,12,Azure Virtual Machine
Server4-SQL2012,SQL Server 2012,Standard,8,Azure VMWare Solutions
```

## Download ESUs

Once your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances have been registered with Azure Arc, you can download the Extended Security Update packages using the link found in the Azure portal, if and when they're made available.

To download ESUs, follow these steps:

1. Sign into the [Azure portal](https://portal.azure.com).

1. Navigate to **Azure Arc** and select **Infrastructure** > **SQL Servers**.

1. Select a server from the list.

    :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-list-of-servers.png" alt-text="Screenshot of a list of servers, with one server highlighted.":::

1. Download security updates from here, if and when they're made available.

    :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-available-updates.png" alt-text="Screenshot of available security updates.":::

## Supported regions

The following list shows the supported regions for this service:

- Australia East
- Canada Central
- Central US
- East Asia
- East US
- East US 2
- France Central
- Japan East
- Korea Central
- North Central US
- North Europe
- South Central US
- Southeast Asia
- UK South
- West Europe
- West US
- West US 2
- West US 3

Government regions are not supported. For more information, see [Can customers get free Extended Security Updates on Azure Government regions?](#can-customers-get-free-extended-security-updates-on-azure-government-regions)

## Frequently asked questions

General frequently asked questions about Extended Security updates can be found at the [Extended security updates FAQ](https://www.microsoft.com/cloud-platform/extended-security-updates). [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-specific frequently asked questions are listed below.

#### When is the End of Support for SQL Server 2012?

The End of Support date for [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] was July 12, 2022.

#### What does End of Support mean?

Microsoft Lifecycle Policy offers 10 years of support (five years for Mainstream Support and five years for Extended Support) for Business and Developer products (such as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows Server). After the end of the Extended Support period, there will be no patches or security updates, which may cause security and compliance issues, and expose your applications and business to serious security risks.

#### What editions of SQL Server are eligible for Extended Security Updates?

Enterprise, Datacenter, Standard, Web, and Workgroup editions of [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] are eligible for ESUs for both x86 and x64 versions.

> [!TIP]  
> Customers on [!INCLUDE[SQL Server 2008](../../includes/ssKatmai-md.md)] and [!INCLUDE[SQL Server 2008 R2](../../includes/ssKilimanjaro-md.md)] can migrate to Azure services if they wish to continue receiving Extended Security Updates, until [July 12, 2023](https://www.microsoft.com/windows-server/extended-security-updates). See the [Overview](#overview) for more information.

#### When will the Extended Security Updates offer be available?

ESUs are now available for purchase and can be ordered from [!INCLUDE[msCoName](../../includes/msconame-md.md)] or a [!INCLUDE[msCoName](../../includes/msconame-md.md)] licensing partner. The delivery of ESUs will begin after the End of Support dates, if and when available. Customers interested in migrating to Azure can do so immediately.

#### What do Extended Security Updates include?

ESUs include provision of Security Updates and Bulletins rated **critical** by the [Microsoft Security Response Center (MSRC)](https://portal.msrc.microsoft.com/), for a maximum of three years after the end of extended support:

- For [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], ESUs will be available until July 8, 2025.

- For [!INCLUDE[ssKatmai](../../includes/ssKatmai-md.md)] and [!INCLUDE[ssKilimanjaro](../../includes/ssKilimanjaro-md.md)], ESUs will be available until July 12, 2023 for customers who have migrated their workloads to Azure services. See the [Overview](#overview) for more information.

ESU will be distributed if and when available. ESUs don't include technical support, but you may use other [!INCLUDE[msCoName](../../includes/msconame-md.md)] support plans to get assistance on your [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] questions on workloads covered by ESUs. ESUs don't include new features, functional improvements, nor customer-requested fixes. However, [!INCLUDE[msCoName](../../includes/msconame-md.md)] may include non-security fixes as deemed necessary.

#### Why do Extended Security Updates only offer "critical" updates?

For End of Support events in the past, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provided only Critical Security Updates, which meets the compliance criteria of our enterprise customers. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] doesn't ship a general monthly security update. [!INCLUDE[msCoName](../../includes/msconame-md.md)] only provides on-demand [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security updates (GDRs) for MSRC bulletins where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is identified as an affected product.
If there are situations where new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] important updates won't be provided and it's deemed critical by the customer but not by MSRC, we'll work with the customer on a case-by-case basis to suggest appropriate mitigation.

#### What Licensing programs are eligible for Extended Security Updates?

Software Assurance customers can purchase ESUs on-premises under an Enterprise Agreement (EA), Enterprise Subscription Agreement (EAS), a Server & Cloud Enrollment (SCE), or an Enrollment for Education Solutions (EES). Software Assurance doesn't need to be on the same enrollment.

#### Do SQL Server customers need to be running the most current Service Pack to benefit from Extended Security Updates?

Yes, customers need to run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with the latest Service Pack to apply ESUs. [!INCLUDE[msCoName](../../includes/msconame-md.md)] will only produce updates that can be applied on the latest Service Pack.

#### What are the options for SQL Server customers without Software Assurance?

For customers who don't have Software Assurance, the alternative option to get access to ESUs is to migrate to Azure. For variable workloads, we recommend that customers migrate on Azure via Pay-As-You-Go, which allows for scaling up or down at any time. For predictable workloads, customers should migrate to Azure via Server Subscription and Reserved Instances.

#### Does this offer apply to older versions of SQL Server?

No. For versions before [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] we recommend upgrading to the latest supported versions, but customers can upgrade to [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] to take advantage of this offer.

> [!TIP]  
> Customers on [!INCLUDE[SQL Server 2008](../../includes/ssKatmai-md.md)] and [!INCLUDE[SQL Server 2008 R2](../../includes/ssKilimanjaro-md.md)] can migrate to Azure services if they wish to continue receiving Extended Security Updates, until [July 12, 2023](https://www.microsoft.com/windows-server/extended-security-updates). See the [Overview](#overview) for more information.

#### Can I deploy a brand new SQL Server 2012 instance on Azure and still get Extended Security Updates?

Yes, customers can start a new [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] instance on an Azure SQL Server Virtual Machine and have access to ESUs.

#### Can I get technical support on-premises for SQL Server after the End of Support date, without purchasing Extended Security Updates?

No. If a customer has [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and chooses to remain on-premises during a migration without ESUs, they can't log a support ticket even if they have a support plan. If they migrate to Azure, however, they can get support using their Azure Support Plan.

#### If a customer wants to bring their own SQL Server license (BYOL), are they required to have Software Assurance coverage?

Yes, customers need to have Software Assurance to take advantage of the BYOL program for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on Azure Virtual Machines as part of the License Mobility program. For customers without Software Assurance, we recommend customers move to Azure SQL Managed Instance for their [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] environments.

Customers can also migrate to pay-as-you-go Azure Virtual Machines. Software Assurance customers who license SQL by core also have the option of migrating to Azure using the Azure Hybrid Benefit (AHB).

Azure SQL Managed Instance is a service in Azure providing nearly 100% compatibility with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on-premises. SQL Managed Instance provides built-in high availability and disaster recovery capabilities plus intelligent performance features and the ability to scale on the fly. SQL Managed Instance also provides a version-less experience that takes away the need for manual security patching and upgrades. For more information on the BYOL program, see [Azure SQL Managed Instance pricing](https://azure.microsoft.com/pricing/details/azure-sql-managed-instance/single/).

#### What options do customers have to run SQL Server in Azure?

Customers can move legacy [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] environments to Azure SQL Managed Instance, a fully managed data platform service (PaaS) that offers a "version-free" option to eliminate concerns with End of Support dates, or to Azure Virtual Machines to have access to Security Updates. The migrated databases will retain their compatibility with the legacy system. For more information, see [Compatibility Certification](../../database-engine/install-windows/compatibility-certification.md).

ESUs are available for [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] in Azure Virtual Machines after the End of Support date of July 12, 2022, for the next three years.

> [!TIP]  
> Customers on [!INCLUDE[SQL Server 2008](../../includes/ssKatmai-md.md)] and [!INCLUDE[SQL Server 2008 R2](../../includes/ssKilimanjaro-md.md)] can migrate to Azure services if they wish to continue receiving Extended Security Updates, until [July 12, 2023](https://www.microsoft.com/windows-server/extended-security-updates). See the [Overview](#overview) for more information.

For customers looking to upgrade from [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], all subsequent versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will be supported. For [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], customers are required to be on the latest supported Service Pack. Starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)], customers are advised to be on the latest Cumulative Update. Service Packs won't be available starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)], only Cumulative Updates and General Distribution Releases (GDRs).

Azure SQL Managed Instance is an instance-scoped deployment option in Azure SQL that provides the broadest [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] engine compatibility and native virtual network (VNET) support, so you can migrate [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases to SQL Managed Instance without changing apps. It combines the rich [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] surface area with the operational and financial benefits of an intelligent, fully managed service. You can use the new Azure Database Migration Service to move [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] to Azure SQL Managed Instance with few or no application code changes.

#### Can customers use the Azure Hybrid Benefit for SQL Server 2012?

Yes, customers with active Software Assurance or equivalent Server Subscriptions can use the Azure Hybrid Benefit using existing on-premises license investments for discounted pricing on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] running on Azure SQL and Azure VMs.

#### Can customers get free Extended Security Updates on Azure Government regions?

Not at this stage. Refer to the [Supported regions](#supported-regions) for more information.

Government customers that are unable to connect or register their [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances in one of the supported Azure regions, can [open a ticket with Microsoft Support](https://support.serviceshub.microsoft.com/supportforbusiness) for further instructions. Review the [support options for businesses](https://support.microsoft.com/topic/support-for-business-1f4c4d09-9047-28ac-bb3b-618757e3bffd) for more information.

#### Can customers get free Extended Security Updates on Azure Stack?

Yes, customers can migrate [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to Azure Stack and receive ESUs for no extra cost after the End of Support dates.

#### For customers with a SQL Server cluster using shared storage, what is the guidance to migrating to Azure?

Azure doesn't currently support shared storage clustering. For advice on how to configure a highly available [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance on Azure, refer to the [SQL Server High Availability guide](/azure/azure-sql/virtual-machines/windows/business-continuity-high-availability-disaster-recovery-hadr-overview).

#### Can customers use Extended Security Updates for SQL Server with a third-party hosting provider?

Customers can't use ESUs if they move their [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] environment to a PaaS implementation on other cloud providers. If you want to move to virtual machines (IaaS), you can use License Mobility for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] via Software Assurance to make the move, and purchase ESUs from [!INCLUDE[msCoName](../../includes/msconame-md.md)] to manually apply patches to the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] instances running in a VM (IaaS) on an authorized SPLA hosting provider's server. However, free updates in Azure are the more attractive offer.

#### What are the best practices for enhancing performance of SQL Server in Azure virtual machines?

For advice on how to optimize performance for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on Azure virtual machines, see the [SQL Server optimization guide](/azure/azure-sql/virtual-machines/windows/performance-guidelines-best-practices).

#### How do US Federal Government customers register and obtain SQL Server 2012 ESUs if they are running in Azure Government/O365 GCCH/O365 DOD?

Azure Government regions aren't currently supported in the Azure portal. Until then, [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] customers in Government regions interested in Extended Security Updates (ESU) will have to create an Azure subscription in one of the supported regions and register their [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances there.

Registering provides access to offers via the Azure portal, including ESUs, for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances that can't be directly connected to Azure. You can register your instance in a [disconnected](#overview) state using the following metadata for each instance: `name,version,edition,cores,hostType`. See the [formatting requirements](#formatting-requirements-for-csv-file) for more information.

If there is a critical security patch for [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)], customers will need to download the patch from the Azure portal following these [step-by-step instructions](#download-esus), and then apply the patch to their [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances.

Government customers that are unable to connect or register their [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances in one of the supported Azure regions, can [open a ticket with Microsoft Support](https://support.serviceshub.microsoft.com/supportforbusiness) for further instructions. Review the [support options for businesses](https://support.microsoft.com/topic/support-for-business-1f4c4d09-9047-28ac-bb3b-618757e3bffd) for more information.

## See also

- [SQL Server 2012 lifecycle page](/lifecycle/products/microsoft-sql-server-2012)
- [SQL Server 2008 R2 lifecycle page](/lifecycle/products/microsoft-sql-server-2008-r2)
- [SQL Server end of support page](sql-server-end-of-support-overview.md?WT.mc_id=akamseos)
- [Extended Security Updates frequently asked questions (FAQ)](https://aka.ms/sqleosfaq)
- [Microsoft Security Response Center (MSRC)](https://portal.msrc.microsoft.com/security-guidance/summary)
- [Manage Windows updates by using Azure Automation](/azure/automation/update-management/overview)
- [SQL Server VM automated patching](/azure/azure-sql/virtual-machines/windows/automated-patching)
- [Microsoft Data Migration Guide](https://datamigration.microsoft.com/)
- [Azure migrate: lift-and-shift options to move your current SQL Server into an Azure VM](https://azure.microsoft.com/services/azure-migrate/)
- [Cloud adoption framework for SQL migration](/azure/cloud-adoption-framework/migrate/expanded-scope/sql-migration)
- [ESU-related scripts on GitHub](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/sql-server-extended-security-updates/scripts)
