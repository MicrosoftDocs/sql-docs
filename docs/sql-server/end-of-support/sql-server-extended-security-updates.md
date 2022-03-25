---
title: "What are Extended Security Updates?"
description: Learn how to use Azure Arc to get extended security updates for your end-of-support and end-of-life SQL Server products, such as SQL Server 2008, SQL Server 2008 R2, and SQL Server 2012.
ms.custom: ""
ms.date: 03/15/2022
ms.prod: sql
ms.technology: install
ms.topic: conceptual
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: pmasl
monikerRange: ">=sql-server-2016"
---
# What are Extended Security Updates for SQL Server?

[!INCLUDE [SQL Server end of support](../../includes/applies-to-version/sql-migration-end-of-support.md)]

This article provides information for using [Azure Arc](/azure/azure-arc/overview) to receive Extended Security Updates (ESUs) for versions of SQL Server that are out of extended support.

> [!TIP]  
> Customers on [!INCLUDE[SQL Server 2008](../../includes/ssKatmai-md.md)] and [!INCLUDE[SQL Server 2008 R2](../../includes/ssKilimanjaro-md.md)] can migrate to Azure SQL Server VMs if they wish to continue receiving Extended Security Updates, until [July 12, 2023](https://www.microsoft.com/windows-server/extended-security-updates).

For more information about other options, see [End of support options](sql-server-end-of-support-overview.md).

## Overview

Once [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has reached the end of its support lifecycle, you can sign up for an Extended Security Update (ESU) subscription for your servers and remain protected for up to three years, until you're ready to upgrade to a newer version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [migrate to Azure SQL](/azure/azure-sql/migration-guides/).

You can receive Extended Security Updates in several ways:

- **Azure Arc**. Purchased for your on-premises or hosted environment. You will download updates when they're available. There are two ways to use Azure Arc:

  - **Connected**. Install the Azure Connected Machine agent with direct connectivity to Azure. You will benefit from the features that [Azure Arc-enabled servers](/azure/azure-arc/servers/overview) provide.

  - **Registered**. Manually add your instance using a process similar to the deprecated SQL Server registry. The instance will be added in a *disconnected* state.

- **Azure services**. Free and enabled by default when migrating on-premises servers to one of the following Azure services:

  - [SQL Server on Azure Virtual Machines](/azure/azure-sql/virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview)

  - [Azure Stack HCI](/azure-stack/hci)

  - [Azure Stack Hub](/azure-stack#azure-stack-hub)

  - [Azure VMware Solution &#40;AVS&#41;](/azure/azure-vmware/)

  - [Nutanix Cloud Clusters on Azure](https://nutanix.com/azure/)

[!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends applying ESU patches as soon as they're available to keep your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance protected. For detailed information about ESUs, see the [ESU FAQ page](https://www.microsoft.com/cloud-platform/extended-security-updates).

## Extended support dates

For the versions in the table below, consider using [Extended Security Updates](/azure/azure-sql/virtual-machines/windows/sql-server-extend-end-of-support) described in this article, or other migration options. For more information, see [End of support options](sql-server-end-of-support-overview.md).

|SQL Server Version|Extended Support End Date|
|---|---|
|SQL Server 2012|July 12, 2022|
|SQL Server 2008 R2|July 10, 2019|
|SQL Server 2008|July 10, 2019|

## What are Extended Security Updates

Extended Security Updates (ESUs) include security updates for customers who have purchased an Extended Support Update subscription, and are available for [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)].

> [!TIP]  
> Customers on [!INCLUDE[SQL Server 2008](../../includes/ssKatmai-md.md)] and [!INCLUDE[SQL Server 2008 R2](../../includes/ssKilimanjaro-md.md)] can migrate to Azure SQL Server VMs if they wish to continue receiving Extended Security Updates, until [July 12, 2023](https://www.microsoft.com/windows-server/extended-security-updates).

ESUs are made available **if needed**, once a security vulnerability is discovered and is rated as **Critical** by the [Microsoft Security Response Center (MSRC)](https://portal.msrc.microsoft.com). Therefore, there is no regular release cadence for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ESUs.

ESUs don't include:

- New features
- Functional improvements
- Customer-requested fixes

### Support

ESUs don't include technical support, but you can use an active support contract such as [Software Assurance](https://www.microsoft.com/licensing/licensing-programs/software-assurance-default?activetab=software-assurance-default-pivot%3aprimaryr3) or Premier/Unified Support to get technical support on workloads covered by ESUs if you choose to stay on-premises. Alternatively, if you're hosting on Azure, you can use an Azure Support plan to get technical support.

[!INCLUDE[msCoName](../../includes/msconame-md.md)] cannot provide technical support for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances (both on-premises, and in hosting environments) that are not covered with an ESU subscription.

## ESU Availability and Deployment

ESUs are available to customers running their workload in Azure, on-premises, or hosted environments.

### Azure Virtual Machines

If you migrate your workloads to Azure Virtual Machines (IaaS), you'll have access to ESUs for [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] for up to three years after the End of Support, at **no additional charges** above the cost of running the virtual machine. Customers don't need Software Assurance to receive ESUs in Azure.

Azure VMs running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will receive ESUs automatically through existing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] update channels, when the virtual machine is configured to use [automated patching](/azure/virtual-machines/windows/sql/virtual-machines-windows-sql-automated-patching).

Azure VMs that have ***not* been configured for [automated patching](/azure/virtual-machines/windows/sql/virtual-machines-windows-sql-automated-patching)** will need to manually download and deploy ESU patches as described in the [on-premises or hosted environments](#on-premises-or-hosted-environments) section.

### On-premises or hosted environments

If you have Software Assurance, you can purchase an Extended Security Update (ESU) subscription for up to three years after the End of Support date, under an Enterprise Agreement (EA), Enterprise Subscription Agreement (EAS), a Server & Cloud Enrollment (SCE), or an Enrollment for Education Solutions (EES). You can purchase ESUs only for the servers you need to cover. ESUs can be purchased directly from [!INCLUDE[msCoName](../../includes/msconame-md.md)] or a [!INCLUDE[msCoName](../../includes/msconame-md.md)] licensing partner.

Customers covered by ESU agreements must follow these steps to download and deploy an ESU patch. The process is the same for Azure Stack and Azure Virtual Machines that are not configured to receive automatic updates:

- [Register eligible instances](#register-instances-for-esus).
- Once registered, whenever ESU patches are released, a download link will be available in the Azure portal to download the package.
- The downloaded package can be deployed to your on-premises or hosted environments manually, or through whatever update orchestration solution is used in your organization, such as Microsoft Endpoint Configuration Manager (formerly System Center Configuration Manager).

For more information, see the [Extended Security Updates frequently asked questions](https://www.microsoft.com/cloud-platform/extended-security-updates).

## Set up an Azure Arc resource

### [RW] -> [Still to come]

It isn't necessary to register [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances for ESUs when running an Azure Virtual Machine that is configured for [automatic updates](/azure/virtual-machines/windows/sql/virtual-machines-windows-sql-automated-patching).

## <a id="register-instances-for-esus"></a> Register SQL Server instances for ESUs

In this example, you will add your SQL Server instances to Azure Arc following these steps below.

1. Sign into the [Azure portal](https://portal.azure.com).

1. Select the option to **Create a resource**.

1. Navigate to **Azure Arc** and select **SQL Servers** from the Infrastructure section.

1. To add a machine, you can either select **Add** from the menu at the top of the screen, or choose the **Add SQL Server - Azure Arc** button lower down.

    :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-empty-list.png" alt-text="Screenshot showing an empty list of SQL Servers list on the Azure Arc portal":::

1. Select **Register servers** to add a disconnected SQL Server instance.

    :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-add-connected-or-registered.png" alt-text="Screenshot showing the two options for adding connected or registered servers":::

### Single SQL Server instance

1. On the next screen, you can choose to add a single or multiple SQL Server instances. Select the option for **Single SQL instance** to continue.

    :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-add-sql-registration-options.png" alt-text="Screenshot showing the Add SQL Registrations options":::

### [RW] -> [Confirm what goes in here]

1. Provide the required information as is detailed in this table, and then select **Register**:

   |Value|Description|Additional information|
   |---|---|---|
   | **Instance** | Enter the output of command `SELECT @@SERVERNAME`, such `MyServer\Instance01`. ||
   | **SQL Version** | Select your version from the drop-down. ||
   | **Edition** | Select the applicable edition from the drop-down: Datacenter, Developer (free to deploy if purchased ESUs), Enterprise, Standard, Web, Workgroup. |
   | **Cores** | Enter the number of cores for this instance ||
   | **Host Type** | Select the applicable host type from the drop-down: Virtual machine (on-premises), Physical Server (on-premises), Azure Virtual Machine, Amazon EC2, Google Compute Engine, Other. ||
   | **SubscriptionID** | Enter the SubscriptionID where the VM is created.  |Only necessary for Azure virtual machines|
   | **Resource Group** | Enter the resource group where the VM is created.  |Only necessary for Azure virtual machines|
   | **Azure VM name** | Enter the VM resource name.  |Only necessary for Azure virtual machines|
   | **Azure VM operating system** | Select the applicable Windows Server operating system version from the drop-down. |Only necessary for Azure virtual machines|

The newly registered [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance is now visible in the **Register SQL Server instances** section of the **Overview** pane:

:::image type="content" source="media/sql-server-extended-security-updates/registered-sql-instance.png" alt-text="Registered SQL Server instances":::

Once a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance has been registered, the **Security Updates** section becomes available. Any available ESUs will be posted there.

### Multiple SQL Server instances in bulk

Multiple [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances can be registered in bulk by uploading a .CSV file. Once your [.CSV file has been formatted correctly](#formatting-requirements-for-csv-file), you can follow these steps to bulk register your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances with Azure Arc:

1. On the next screen, you can choose to add a single or multiple SQL Server instances. Select the option for **Multiple SQL instances** to continue.

    :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-multiple-sql-instances.png" alt-text="Screenshot showing the Multiple SQL instances option":::

1. Select **Browse** to upload the CSV file containing multiple disconnected SQL Server instances.

    :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-browse-csv-file.png" alt-text="Screenshot showing how to browse to the CSV file":::

1. Before you can continue, you must confirm that you have the rights to receive Extended Security Updates, using the checkbox provided.

    :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-rights.png" alt-text="Screenshot showing the checkbox to indicate the rights to continue":::

1. The `Year1OrderID` tag is optional, but should reflect the ESU invoice number for ease of reference. The `Year1EntitlementConfirmed` tag is automatically filled in.

    :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-tags.png" alt-text="Screenshot showing tags":::

1. Before you can add your SQL Server instances, you must agree to the terms and conditions.

    :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-terms-and-conditions.png" alt-text="Screenshot showing terms and conditions":::

1. Finally, you will see your registered SQL Server instances in the portal. Because they were added manually, they will always show in a disconnected state.

    :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-connected-servers.png" alt-text="Screenshot showing two registered SQL Server instances on the Azure Arc portal":::

### Add section for linking ESU Invoice here


### Formatting requirements for CSV file

- Values are comma-separated
- Values aren't single or double-quoted
- Column names are case-insensitive but must be named as follows:

   |Column name|Required for Azure SQL VM|
   |---|---|
   |name|No|
   |version|No|
   |edition|No|
   |cores|No|
   |hostType|No|
   |subscriptionID|Yes|
   |resourceGroup|Yes|
   |azureVmName|Yes|
   |AzureVmOS|Yes|

#### CSV Example 1 - On-premises

For on-premises [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances, the CSV file should look like this:

```csv
name,version,edition,cores,hostType
Server1\SQL2012,2012,Enterprise,12,Physical Server
Server1\SQL2012,2012,Enterprise,12,Physical Server
Server2\SQL2012,2012,Enterprise,24,Physical Server
Server3\SQL2012,2012,Enterprise,12,Virtual Machine
Server4\SQL2012,2012,Developer,8,Physical Server  
```

Refer to [MyPhysicalServers.csv](https://github.com/microsoft/sql-server-samples/blob/master/samples/manage/sql-server-extended-security-updates/scripts/MyPhysicalServers.csv) for a CSV file example.

#### CSV Example 2 - Azure SQL VM

For Azure Virtual Machine [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances, the CSV file should look like this:

```csv
name,version,edition,cores,hostType,subscriptionId,resourceGroup,azureVmName,azureVmOS    
ProdServerUS1\SQL01,2012,Enterprise,12,Azure Virtual Machine,61868ab8-16d4-44ec-a9ff-f35d05922847,RG,VM1,2012    
ProdServerUS1\SQL02,2012,Enterprise,24,Azure Virtual Machine,61868ab8-16d4-44ec-a9ff-f35d05922847,RG,VM1,2012    
ServerUS2\SQL01,2012,Enterprise,12,Azure Virtual Machine,61868ab8-16d4-44ec-a9ff-f35d05922847,RG,VM2,2012    
ServerUS2\SQL02,2012,Enterprise,8,Azure Virtual Machine,61868ab8-16d4-44ec-a9ff-f35d05922847,RG,VM2,2012    
SalesServer\SQLProdSales,2012,Developer,8,Azure Virtual Machine,61868ab8-16d4-44ec-a9ff-f35d05922847,RG,VM3,2012  
```

Refer to [MyAzureVMs.csv](https://github.com/microsoft/sql-server-samples/blob/master/samples/manage/sql-server-extended-security-updates/scripts/MyAzureVMs.csv) for an Azure VM targeted CSV file example.

For [!INCLUDE[tsql](../../includes/tsql-md.md)] and PowerShell example scripts that can generate the required [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance registration information into a .CSV file, see [ESU registration script examples](https://github.com/microsoft/sql-server-samples/blob/master/samples/manage/sql-server-extended-security-updates/scripts.md).

## Download ESUs

Once your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances have been registered with Azure Arc, you can download the Extended Security Update packages using the link found in the Azure portal, if and when they're made available.

To download ESUs, follow these steps:

1. Sign into the [Azure portal](https://portal.azure.com).

1. Go to your **Azure Arc** resource, and navigate to **SQL Servers** in the Infrastructure section. Select a server from the list.

    :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-list-of-servers.png" alt-text="Screenshot showing a list of servers, with one server highlighted":::

1. Download security updates from here, if and when they are made available.

    :::image type="content" source="media/sql-server-extended-security-updates/extended-security-updates-available-updates.png" alt-text="Screenshot showing available security updates":::

1. Download security updates from here, if and when they are made available.

## Supported regions and data residency

You can register your instance with Azure Arc in a subset of Azure regions. The following table shows the supported regions and the data residency type in each.

| **Region** | **Data residency** |
|:--|:--|
|Australia East|Geo|
|Australia Southeast|Geo|
|Canada Central|Geo|
|France Central|Geo|
|Japan East|Geo|
|Japan West|Geo|
|Korea Central|Geo|
|Korea South|Geo|
|North Central US|Geo|
|North Europe|Geo|
|South Central US|Geo|
|Southeast Asia|Single region|
|South India|Geo|
|South Africa North|Geo|
|UK South|Geo|
|UK West|Geo|
|West US|Geo|
|East US|Geo|
|Central US|Geo|
|East Asia|Geo|
|West Europe|Geo|
|West Central US|Geo|
|West US 2|Geo|
|East US 2|Geo|

In the regions with geographic residency, the Azure Arc service maintains data backups in a geo-redundant storage account (GRS). In the regions with the single region residency, the Azure Arc service maintains data backups in a zone-redundant storage account (ZRS). For more information, see the [Trust Center](https://azuredatacentermap.azurewebsites.net/).

## Configure regional redundancy

Customers that require regional redundancy for their registered servers can create registration data in two distinct regions. Customers can then download security updates from either region based on service availability.

For regional redundancy, the Azure Arc service has to be created in two different regions, and your SQL Server inventory has to be split between these two services. This way, half of your SQL Servers are registered in one region, and then the other half of your SQL Servers are registered in the other region.

To configure regional redundancy, follow these steps:

1. Split your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] inventory into two files, such as `upload1.csv` and `upload2.csv`.

    **`upload1.csv`**

    |Name|Version|Edition|Cores|HostType|
    |---|---|---|---|---|
    |Server2|2012|Standard|55|Physical Server|
    |t5\Server3|2012|Standard|55|Physical Server|
    |Server1\SQL2012|2012|Enterprise|12|Physical Server|

    **`upload2.csv`**

    |Name|Version|Edition|Cores|HostType|
    |---|---|---|---|---|
    |Server2\SQL2012|2012|Enterprise|24|Physical Server|
    |Server3\SQL2012|2012|Enterprise|12|Physical Server|
    |Server4\SQL2012|2012|Developer|8|Physical Server|

1. Create the first **Azure Arc** service in one region, and then bulk register one of the csv files to it. For example, create the first **Azure Arc** service in the **West US** region, and bulk register your SQL Servers using the `upload1.csv` file.
1. Create the second **Azure Arc** service in the second region, and then bulk register the other csv file to it. For example, create the second **Azure Arc** service in the **East US** region, and bulk register your SQL Servers using the `upload2.csv` file.

Once your data has been registered with the two different **Azure Arc** resources, you will be able to download security updates from either region, based on service availability.

## Frequently Asked Questions

General frequently asked questions about Extended Security updates can be found at the [Extended security updates FAQ](https://www.microsoft.com/cloud-platform/extended-security-updates). [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-specific frequently asked questions are listed below.

**When was the End of Support for SQL Server 2012?**

The End of Support date for [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] was July 12, 2022.

**What does End of Support mean?**

Microsoft Lifecycle Policy offers 10 years of support (5 years for Mainstream Support and 5 years for Extended Support) for Business and Developer products (such as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and Windows Server). As per the policy, after the end of the Extended Support period there will be no patches or security updates, which may cause security and compliance issues, and expose customers' applications and business to serious security risks.

**What editions of SQL Server are eligible for Extended Security Updates?**

Enterprise, Datacenter, Standard, Web, and Workgroup editions of [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] are eligible for ESUs for both x86 and x64 versions.

> [!TIP]  
> Customers on [!INCLUDE[SQL Server 2008](../../includes/ssKatmai-md.md)] and [!INCLUDE[SQL Server 2008 R2](../../includes/ssKilimanjaro-md.md)] can migrate to Azure SQL Server VMs if they wish to continue receiving Extended Security Updates, until [July 12, 2023](https://www.microsoft.com/windows-server/extended-security-updates).

**When will the Extended Security Updates offer be available?**

ESUs are now available for purchase and can be ordered from [!INCLUDE[msCoName](../../includes/msconame-md.md)] or a [!INCLUDE[msCoName](../../includes/msconame-md.md)] licensing partner. The delivery of ESUs will begin after the End of Support dates, if and when available. Customers interested in migrating to Azure can do so immediately.

## [review]

**What do Extended Security Updates include?**

ESUs include provision of Security Updates and Bulletins rated **critical** by the [Microsoft Security Response Center (MSRC)](https://portal.msrc.microsoft.com/), for a maximum of three years after the end of extended support:

- For [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], ESUs will be available until July 8, 2025.

- For [!INCLUDE[ssKatmai](../../includes/ssKatmai-md.md)] and [!INCLUDE[ssKilimanjaro](../../includes/ssKilimanjaro-md.md)], ESUs will be available until July 12, 2023 for customers who have migrated their workloads to Azure VMs.

ESU will be distributed if and when available. ESUs do not include technical support, but you may use other [!INCLUDE[msCoName](../../includes/msconame-md.md)] support plans to get assistance on your [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] questions on workloads covered by ESUs. ESUs don't include new features, functional improvements, nor customer-requested fixes. However, [!INCLUDE[msCoName](../../includes/msconame-md.md)] may include non-security fixes as deemed necessary.

**Why do Extended Security Updates only offer "critical" updates?**

For End of Support events in the past, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provided only Critical Security Updates, which meets the compliance criteria of our enterprise customers. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] doesn't ship a general monthly security update. [!INCLUDE[msCoName](../../includes/msconame-md.md)] only provides on-demand [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security updates (GDRs) for MSRC bulletins where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is identified as an affected product.
If there are situations where new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] important updates won't be provided and it's deemed critical by the customer but not by MSRC, we'll work with the customer on a case-by-case basis to suggest appropriate mitigation.

**What Licensing programs are eligible for Extended Security Updates?**

Software Assurance customers can purchase ESUs on-premises under an Enterprise Agreement (EA), Enterprise Subscription Agreement (EAS), a Server & Cloud Enrollment (SCE), or an Enrollment for Education Solutions (EES). Software Assurance doesn't need to be on the same enrollment.

**Do SQL Server customers need to be running the most current Service Pack to benefit from Extended Security Updates?**

Yes, customers need to run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with the latest Service Pack to apply ESUs. [!INCLUDE[msCoName](../../includes/msconame-md.md)] will only produce updates that can be applied on the latest Service Pack.

**What are the options for SQL Server customers without Software Assurance?**

For customers who don't have Software Assurance, the alternative option to get access to ESUs is to migrate to Azure. For variable workloads, we recommend that customers migrate on Azure via Pay-As-You-Go, which allows for scaling up or down at any time. For predictable workloads, customers should migrate to Azure via Server Subscription and Reserved Instances.

**Does this offer apply to older versions of SQL Server?**

No. For versions before [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] we we recommend upgrading to the latest supported versions, but customers can upgrade to [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] to take advantage of this offer.

> [!TIP]  
> Customers on [!INCLUDE[SQL Server 2008](../../includes/ssKatmai-md.md)] and [!INCLUDE[SQL Server 2008 R2](../../includes/ssKilimanjaro-md.md)] can migrate to Azure SQL Server VMs if they wish to continue receiving Extended Security Updates, until [July 12, 2023](https://www.microsoft.com/windows-server/extended-security-updates).

**Can I deploy a brand new SQL Server 2012 instance on Azure and still get Extended Security Updates?**

Yes, customers can start a new [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] instance on an Azure SQL Server Virtual Machine and have access to ESUs.

**Can I get technical support on-premises for SQL Server after the End of Support date, without purchasing Extended Security Updates?**

No. If a customer has [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and chooses to remain on-premises during a migration without ESUs, they can't log a support ticket even if they have a support plan. If they migrate to Azure, however, they can get support using their Azure Support Plan.

**If a customer wants to bring their own SQL Server license (BYOL), are they required to have Software Assurance coverage?**

Yes, customers need to have Software Assurance to take advantage of the BYOL program for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on Azure Virtual Machines as part of the License Mobility program. For customers without Software Assurance, we recommend customers move to Azure SQL Managed Instance for their [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] environments.

Customers can also migrate to pay-as-you-go Azure Virtual Machines. Software Assurance customers who license SQL by core also have the option of migrating to Azure using the Azure Hybrid Benefit (AHB).

Azure SQL Managed Instance is a service in Azure providing nearly 100% compatibility with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on-premises. Managed Instance provides built-in high availability and disaster recovery capabilities plus intelligent performance features and the ability to scale on the fly. Managed Instance also provides a version-less experience that takes away the need for manual security patching and upgrades. See the Azure pricing guidance page for more information on the BYOL program.

**What options do customers have to run SQL Server in Azure?**

Customers can move legacy [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] environments to Azure SQL Managed Instance, a fully managed data platform service (PaaS) that offers a "version-free" option to eliminate concerns with End of Support dates, or to Azure Virtual Machines to have access to Security Updates. The migrated databases will retain their compatibility with the legacy system. For more information, see [Compatibility Certification](../../database-engine/install-windows/compatibility-certification.md).

ESUs will be available for [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] in Azure Virtual Machines after the End of Support date of July 12, 2022, for the next three years.

> [!TIP]  
> Customers on [!INCLUDE[SQL Server 2008](../../includes/ssKatmai-md.md)] and [!INCLUDE[SQL Server 2008 R2](../../includes/ssKilimanjaro-md.md)] can migrate to Azure SQL Server VMs if they wish to continue receiving Extended Security Updates, until [July 12, 2023](https://www.microsoft.com/windows-server/extended-security-updates).

For customers looking to upgrade from [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], all subsequent versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will be supported. For [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], customers are required to be on the latest supported Service Pack. Starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)], customers are advised to be on the latest Cumulative Update. Service Packs won't be available starting with [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)], only Cumulative Updates and General Distribution Releases (GDRs).

Azure SQL Managed Instance is an instance-scoped deployment option in Azure SQL that provides the broadest [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] engine compatibility and native virtual network (VNET) support, so you can migrate [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases to Managed Instance without changing apps. It combines the rich [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] surface area with the operational and financial benefits of an intelligent, fully managed service. You can use the new Azure Database Migration Service to move [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] to Azure SQL Managed Instance with few or no application code changes.

**Can customers use the Azure Hybrid Benefit for SQL Server 2012?**

Yes, customers with active Software Assurance or equivalent Server Subscriptions can use the Azure Hybrid Benefit using existing on-premises license investments for discounted pricing on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] running on Azure SQL and Azure VMs.

**Can customers get free Extended Security Updates on Azure Government regions?**

Yes, ESUs will be available on Azure Virtual Machines on Azure Government regions.

**Can customers get free Extended Security Updates on Azure Stack?**

Yes, customers can migrate [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to Azure Stack and receive ESUs for no additional cost after the End of Support dates.

**For customers with a SQL Server cluster using shared storage, what is the guidance to migrating to Azure?**

Azure doesn't currently support shared storage clustering. For advice on how to configure a highly available [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance on Azure, refer to the [SQL Server High Availability guide](/azure/virtual-machines/windows/sql/virtual-machines-windows-sql-high-availability-dr).

**Can customers use Extended Security Updates for SQL Server with a third-party hosting provider?**

Customers can't use ESUs if they move their [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] environment to a PaaS implementation on other cloud providers. If customers are looking to move to virtual machines (IaaS), they can use License Mobility for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] via Software Assurance to make the move, and purchase ESUs from [!INCLUDE[msCoName](../../includes/msconame-md.md)] to manually apply patches to the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] instances running in a VM (IaaS) on an authorized SPLA hosting provider's server. However, free updates in Azure are the more attractive offer.

**What are the best practices for enhancing performance of SQL Server in Azure virtual machines?**

For advice on how to optimize performance for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on Azure virtual machines, see the [SQL Server optimization guide](/azure/virtual-machines/windows/sql/virtual-machines-windows-sql-performance).

## See also

- [SQL Server 2012 lifecycle page](/lifecycle/products/microsoft-sql-server-2012)
- [SQL Server 2008 R2 lifecycle page](/lifecycle/products/microsoft-sql-server-2008-r2)
- [SQL Server end of support page](sql-server-end-of-support-overview.md?WT.mc_id=akamseos)
- [Extended Security Updates frequently asked questions (FAQ)](https://aka.ms/sqleosfaq)
- [Microsoft Security Response Center (MSRC)](https://portal.msrc.microsoft.com/security-guidance/summary)
- [Manage Windows updates by using Azure Automation](/azure/automation/update-management/overview)
- [SQL Server VM automated patching](/azure/virtual-machines/windows/sql/virtual-machines-windows-sql-automated-patching)
- [Microsoft Data Migration Guide](https://datamigration.microsoft.com/)
- [Azure migrate: lift-and-shift options to move your current SQL Server into an Azure VM](https://azure.microsoft.com/services/azure-migrate/)
- [Cloud adoption framework for SQL migration](/azure/cloud-adoption-framework/migrate/expanded-scope/sql-migration)
- [ESU-related scripts on GitHub](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/sql-server-extended-security-updates/scripts)
