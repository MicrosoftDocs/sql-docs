---
title: "Extended Security Updates FAQ"
description: Frequently asked questions about using Azure Arc to get extended security updates for your end-of-support and end-of-life SQL Server products, such as SQL Server 2012 and SQL Server 2014.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/10/2024
ms.service: sql
ms.subservice: install
ms.topic: conceptual
ms.custom:
  - references_regions
monikerRange: ">=sql-server-2016"
---
# Extended Security Updates: Frequently asked questions

[!INCLUDE [SQL Server end of support](../../includes/applies-to-version/sql-migration-end-of-support.md)]

General frequently asked questions about Extended Security updates can be found at the [Extended security updates FAQ](https://www.microsoft.com/windows-server/extended-security-updates). [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]-specific frequently asked questions are listed in this article.

For information about using [Azure Arc](/azure/azure-arc/overview) to receive Extended Security Updates (ESUs) for versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] that are out of extended support, see [What are Extended Security Updates for SQL Server?](sql-server-extended-security-updates.md)

For information about ESU pricing, see [Plan your Windows Server and SQL Server end of support](https://www.microsoft.com/windows-server/extended-security-updates).

#### When is the End of Support for SQL Server 2012 and SQL Server 2014?

The End of Support date for [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] was July 12, 2022.

The End of Support date for [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] was July 9, 2024.

#### What does End of Support mean?

Microsoft Lifecycle Policy offers 10 years of support (five years for Mainstream Support and five years for Extended Support) for Business and Developer products (such as [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and Windows Server). After the end of the Extended Support period, there are no patches or security updates, which might cause security and compliance issues, and expose your applications and business to serious security risks.

#### What editions of SQL Server are eligible for Extended Security Updates?

Enterprise, Datacenter, Standard, Web, and Workgroup editions of [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] are eligible for ESUs for both x86 and x64 versions.

#### When is the Extended Security Updates offer available?

ESUs are now available for purchase and can be ordered from [!INCLUDE [msCoName](../../includes/msconame-md.md)] or a [!INCLUDE [msCoName](../../includes/msconame-md.md)] licensing partner. The delivery of ESUs will begin after the End of Support dates, if and when available. Customers interested in migrating to Azure can do so immediately.

#### What do Extended Security Updates include?

ESUs include provision of Security Updates and Bulletins rated **critical** by the [Microsoft Security Response Center (MSRC)](https://msrc.microsoft.com/update-guide), for a maximum of three years after the end of extended support:

- For [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)], ESUs are available until July 8, 2025.
- For [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)], ESUs are available until July 8, 2027.

ESUs are distributed if and when available. ESUs don't include technical support, but you can use other [!INCLUDE [msCoName](../../includes/msconame-md.md)] support plans to get assistance on your [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] questions on workloads covered by ESUs. ESUs don't include new features, functional improvements, nor customer-requested fixes. However, [!INCLUDE [msCoName](../../includes/msconame-md.md)] might include non-security fixes as deemed necessary.

#### Why do Extended Security Updates only offer "critical" updates?

For End of Support events in the past, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] provided only Critical Security Updates, which meets the compliance criteria of our enterprise customers. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't ship a general monthly security update. [!INCLUDE [msCoName](../../includes/msconame-md.md)] only provides on-demand [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] security updates (GDRs) for MSRC bulletins where [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is identified as an affected product.
If there are situations where new [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] important updates aren't provided, and it's deemed critical by the customer but not by MSRC, we work with the customer on a case-by-case basis to suggest appropriate mitigation.

#### What Licensing programs are eligible for Extended Security Updates?

Software Assurance customers can purchase ESUs on-premises under an Enterprise Agreement (EA), Enterprise Subscription Agreement (EAS), a Server & Cloud Enrollment (SCE), or an Enrollment for Education Solutions (EES). Software Assurance doesn't need to be on the same enrollment.

#### Do I need to be running the most current SQL Server Service Pack to benefit from Extended Security Updates?

Yes, you need to run [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] with the latest Service Pack to apply ESUs. [!INCLUDE [msCoName](../../includes/msconame-md.md)] only produces updates that can be applied on the latest Service Pack.

#### What are my options for SQL Server without Software Assurance?

For customers who don't have Software Assurance, the alternative option to get access to ESUs is to migrate to Azure. For variable workloads, we recommend that you migrate on Azure via Pay-As-You-Go, which allows for scaling up or down at any time. For predictable workloads, you should migrate to Azure via Server Subscription and Reserved Instances.

#### Does this offer apply to older versions of SQL Server?

No. For [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and earlier versions, we recommend upgrading to the latest supported versions, but you can upgrade to [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] or [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] to take advantage of this offer.

#### Can I deploy a brand new SQL Server 2012 or SQL Server 2014 instance on Azure and still get Extended Security Updates?

Yes, you can start a new [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] or [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] instance on an Azure [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Virtual Machine and have access to ESUs.

#### Can I get technical support on-premises for SQL Server after the End of Support date, without purchasing Extended Security Updates?

No. If you have [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] or [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)], and choose to remain on-premises during a migration without ESUs, you can't log a support ticket even if you have a support plan. If you migrate to Azure, however, you can get support using your Azure Support Plan.

#### If I want to bring my own SQL Server license (BYOL), am I required to have Software Assurance coverage?

Yes, you need to have Software Assurance to take advantage of the BYOL program for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Azure Virtual Machines as part of the License Mobility program. For customers without Software Assurance, we recommend that you move to Azure SQL Managed Instance for your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] environments.

You can also migrate to pay-as-you-go Azure Virtual Machines. Software Assurance customers who license SQL by core also have the option of migrating to Azure using the Azure Hybrid Benefit (AHB).

Azure SQL Managed Instance is a service in Azure providing nearly 100% compatibility with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on-premises. SQL Managed Instance provides built-in high availability and disaster recovery capabilities plus intelligent performance features and the ability to scale on the fly. SQL Managed Instance also provides a version-less experience that takes away the need for manual security patching and upgrades. For more information on the BYOL program, see [Azure SQL Managed Instance pricing](https://azure.microsoft.com/pricing/details/azure-sql-managed-instance/single/).

#### What options do I have to run SQL Server in Azure?

You can move legacy [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] environments to Azure SQL Managed Instance, a fully managed data platform service (PaaS) that offers a "version-free" option to eliminate concerns with End of Support dates, or to Azure Virtual Machines to have access to Security Updates. The migrated databases retain their compatibility with the legacy system. For more information, see [Compatibility certification](../../database-engine/install-windows/compatibility-certification.md).

ESUs are available for [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] in Azure Virtual Machines after the End of Support date of July 12, 2022, for the next three years.

ESUs are available for [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] in Azure Virtual Machines after the End of Support date of July 9, 2024, for the next three years.

If you're looking to upgrade from [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] or [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)], all subsequent versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] are supported. For [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] and [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], you're required to be on the latest supported Service Pack. Starting with [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)], you're advised to be on the latest Cumulative Update. Service Packs aren't available starting with [!INCLUDE [ssSQL17](../../includes/sssql17-md.md)], only Cumulative Updates and General Distribution Releases (GDRs).

Azure SQL Managed Instance is an instance-scoped deployment option in Azure SQL that provides the broadest [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] engine compatibility and native virtual network (VNET) support, so you can migrate [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] databases to SQL Managed Instance without changing apps. It combines the rich [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] surface area with the operational and financial benefits of an intelligent, fully managed service. You can use the new Azure Database Migration Service to move [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] or [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] to Azure SQL Managed Instance with few or no application code changes.

#### Can I use the Azure Hybrid Benefit for SQL Server 2012 or SQL Server 2014?

Yes, customers with active Software Assurance or equivalent Server Subscriptions can use the Azure Hybrid Benefit using existing on-premises license investments for discounted pricing on [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] running on Azure SQL and Azure VMs.

#### Can I get free Extended Security Updates on Azure Government regions?

Not at this stage. For more information, see the [Supported regions](sql-server-extended-security-updates.md#supported-regions) section.

Government customers that are unable to connect or register their [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances in one of the supported Azure regions, can [open a ticket with Microsoft Support](https://support.serviceshub.microsoft.com/supportforbusiness) for further instructions. For more information, review the [support options for businesses](https://support.microsoft.com/smallbusiness).

#### Can I get free Extended Security Updates on Azure Stack?

Yes, you can migrate [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to Azure Stack and receive ESUs for no extra cost after the End of Support dates.

#### Can I get Extended Security Updates for SQL Server with a third-party hosting provider?

You can't get ESUs if you move your [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] or [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] environment to a PaaS implementation on other cloud providers. If your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running on a virtual machine (IaaS) with License Mobility for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] via Software Assurance, you can subscribe to Extended Security Updates enabled by Azure Arc.

#### How do US Federal Government customers register and obtain SQL Server 2012/2014 ESUs if they are running in Azure Government/O365 GCCH/O365 DOD?

Azure Government regions aren't currently supported in the Azure portal. Until then, [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] and [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] customers in Government regions interested in Extended Security Updates (ESU) must create an Azure subscription in one of the supported regions and register their [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances there.

Registering provides access to offers via the Azure portal, including ESUs, for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances that can't be directly connected to Azure. You can register your instance in a [disconnected](sql-server-extended-security-updates.md#overview) state using the following metadata for each instance: `name,version,edition,cores,hostType`. For more information, see the [formatting requirements](sql-server-extended-security-updates.md#formatting-requirements-for-csv-file).

If there's a critical security patch for [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] or [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)], you need to download the patch from the Azure portal following these [step-by-step instructions](sql-server-extended-security-updates.md#download-esus), and then apply the patch to their [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances.

Government customers that are unable to connect or register their [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances in one of the supported Azure regions, can [open a ticket with Microsoft Support](https://support.serviceshub.microsoft.com/supportforbusiness) for further instructions. For more information, review the [support options for businesses](https://support.microsoft.com/smallbusiness).

#### What are the benefits of Extended Security Updates (ESUs) enabled by Azure Arc?

Extended Security Updates enabled by Azure Arc (ESU subscription) includes several additional benefits compared to the Volume Licensing Service Center (VLSC) purchased ESU products:

- An ESU subscription provides visibility of your entire [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] estate, automating repeatable updates and patches, and can enhance security and data governance with Azure services.
- It's offered as a monthly subscription that automatically cancels when you migrate or upgrade. You pay only for what you need.
- You don't need to resubscribe for Year 3.

#### How do I activate an ESU subscription on multiple SQL Server instances?

If you want to activate Extended Security Updates enabled by Azure Arc subscriptions on a large set of [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] or [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] instances, you can use an [open source PowerShell script](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-arc-enabled-sql-server/modify-license-type) that allows you to do it for different scopes, such as resource group, Azure subscription, multiple subscriptions or the entire account.

#### How do I move from my current ESU to the ESU subscription enabled by Azure Arc?

If you already purchased ESU for Year 1 from VLSC, but haven't yet paid for Year 2, you can activate the ESU subscription via Azure Arc. If you already purchased Year 2, you must wait until Year 3 before switching to Arc-enabled ESUs.

#### Do I need to have purchased the first year of ESU to enable the ESU subscription?

Yes, you must have purchased the Year 1 ESU from VLSC to qualify for the monthly subscription. If you're new to the ESU plans, you must pay for Year 1 ESU before activating the monthly ESU subscription.

#### Can I continue to use the Year 2 ESU purchased from VLSC for Arc-enabled SQL Server 2012/2014 instances?

Yes, you can use your VLSC purchased Year 2 ESU for both registered (disconnected) SQL Server instances enabled for Azure Arc. However, the patches aren't be automatically deployed. You must manually install them. Unlike the ESU subscription, VLSC customers commit for the whole year of the ESU term.

#### How can I cancel the ESU charges?

Your ESU charges stop immediately in the following cases:

- You manually unsubscribe from ESUs for any reason. You lose access to future patches.

- You migrated your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to Azure; no manual cancellation is needed. You continue to have access to future ESUs.

- You upgraded your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to a newer version; no manual cancellation is needed. You have access to all SQL Server updates.

#### What happens if I accidentally cancel my subscription and need to reinstate?

If you cancel your ESU subscription accidentally, you can reactivate it within 30 days. You're billed back to the time of the previous charge. After 30 days the subscription is terminated, and you'll lose access to the future patches. To gain access to the future patches, you need to re-enable ESUs, which is treated as a brand new subscription.

#### What happens if I lose Internet connectivity to Azure due to an outage?

If your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance loses connectivity, the billing stops, and the subscription is suspended. We automatically reactivate it if the instance reconnects within 30 days. Your bill includes a charge for the days since the last day your server was connected. If the server reconnects after 30 days of disconnection, the subscription is terminated.

> [!NOTE]  
> The ESU setting in the Azure portal isn't automatically changed. If ESU is enabled for the server, the reconnection after 30 days of disconnection is treated as a new ESU subscription and billed accordingly.

#### Can I reactivate an ESU subscription if I migrated to Azure, but then decide to move back to on-premises?

If you migrated an instance to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Azure VM, and then moved back and restarted the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on the same machine, this case is treated as a case of temporary disconnection. You must ensure that the original VM is reconnected to Azure Arc using the same Azure subscription, resource group and machine name. The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance must have the same instance name. Under those conditions, the ESU subscription is automatically reactivated if the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] reconnects within 30 days. After 30 days of disconnection, a new ESU subscription is required.

#### If I paid for the annual commitment using the registered (disconnected) option, can I change to the ESU subscription in the following year?

You need to onboard your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances to Azure Arc and select the ESU subscription. You're prompted to confirm your previous annual purchase during this stage.

#### Am I automatically subscribed for Year 3, or do I need to remember to resubscribe?

Once you subscribe to the monthly ESU plan, you don't need to resubscribe for Year 3. You continue to be billed until you upgrade or migrate to Azure.

#### If I registered disconnected instances before 11 July 2023, do I need to register again?

Yes. You can select your [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] or [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] instance in the Azure portal, and link to the Year 2 invoice. The ESU expiration date updates automatically.

#### What happens if I have an active ESU subscription, but unintentionally disconnect SQL Server from Azure Arc by uninstalling the extension?

You don't have access to the patches until you reinstall the Azure extension for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. The following rules apply:

- If the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance resource is still present as `Offline`, there's an extra charge for the period while the instance was disconnected, because the ESU plan remains active.

- If the Azure extension for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is reinstalled in the same subscription or resource group, there's an extra charge for the period while the instance was disconnected, because the ESU plan remains active.

- If the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance is reinstalled to a different subscription, resource group, or region, the instance is considered to be a new resource. You must enable the ESU subscription and pay for it.

#### What happens if I have taken a SQL Server VM with an active ESU subscription offline for some time, and then bring it back online?

You don't have access to the patches until the VM is online. When the VM is back online and [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance shows a status of `Connected`, the following rules apply:

- You're charged for the period of downtime (back to the previous ESU charge).
- The ESU plan is reactivated.

#### Will the billing start automatically when I sign up?

While you can sign up at any time, you're charged for ESUs from the beginning of Year 2. When you sign up, your bill includes a one time bill-back charge for each [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] or [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] instance with an active ESU subscription, from July 12, 2023, to the current date. Going forward, each server will be billed on an hourly basis. Both charges will use the hourly rate (core count) x (100% of Year 2 ESU license price) / 730. For more information about ESU pricing, see [Plan your Windows Server and SQL Server end of support](https://www.microsoft.com/windows-server/extended-security-updates).

#### Is Microsoft extending SQL Server 2008 / 2008 R2 and Windows Server 2008 Extended Security Updates?

Microsoft isn't extending [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE [winserver2008-md](../../includes/winserver2008-md.md)] Extended Security Updates. Support for [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and [!INCLUDE [sql2008r2-md](../../includes/sql2008r2-md.md)] in Azure ended on July 9, 2023, and these servers need to be upgraded to maintain support.

Customers using Windows Server on Azure, will have until January 14, 2024 to upgrade to a supported release.

## Related content

- [What are Extended Security Updates for SQL Server?](sql-server-extended-security-updates.md)
