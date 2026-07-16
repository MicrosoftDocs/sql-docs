---
title: "Extended Security Updates FAQ"
description: Frequently asked questions about using Azure Arc to get extended security updates for your end-of-support and end-of-life SQL Server products, such as SQL Server 2014.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/22/2026
ms.service: sql
ms.subservice: install
ms.topic: faq
ms.custom:
  - references_regions
monikerRange: ">=sql-server-2016"
---
# Extended Security Updates: Frequently asked questions

[!INCLUDE [SQL Server end of support](../../includes/applies-to-version/sql-migration-end-of-support.md)]

For general frequently asked questions about Extended Security Updates (ESUs), see the [Extended security updates FAQ](https://www.microsoft.com/windows-server/extended-security-updates). This article lists [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]-specific frequently asked questions.

For more information, see:

- [What are Extended Security Updates for SQL Server?](sql-server-extended-security-updates.md)
- [Extended security updates enabled by Azure Arc](../azure-arc/extended-security-updates.md)
- [ESU Pricing plans for Windows Server and SQL Server end of support](https://www.microsoft.com/windows-server/extended-security-updates)

## When is the end of support for SQL Server 2014 and SQL Server 2016?

The end of support date for [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] is July 14, 2026. ESUs are available until July 17, 2029.

The end of support date for [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] was July 9, 2024. ESUs are available until July 8, 2027.

## What does end of support mean?

Microsoft Lifecycle Policy offers 10 years of support (five years for Mainstream Support and five years for Extended Support) for Business and Developer products, such as [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and Windows Server. After the end of the Extended Support period, Microsoft doesn't provide patches or security updates. This lack of support might cause security and compliance problems and exposes your applications and business to serious security risks.

## What editions of SQL Server are eligible for ESUs?

Enterprise and Standard editions of [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] and [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] are eligible for ESUs for both x86 and x64 versions.

## Can customers subscribe to ESUs for SQL Server Express, Web, or Developer edition?

No. Customers can't subscribe to ESUs for SQL Server Express, Web, or Developer edition. However, customers who have ESUs for SQL Server production workloads can apply updates to their servers running SQL Server Developer edition [solely for development and test purposes](../azure-arc/extended-security-updates.md#manage-sql-server-esu-subscriptions-for-nonproduction-use).

## What happens when the edition changes?

When you downgrade an instance edition from Enterprise Edition to Standard Edition on the same machine, the billing meter automatically switches from Enterprise to Standard both for IP and ESU. For ESU, there's no additional bill-back. However, there's no refund of the original EE bill-back.

When you upgrade an instance edition from Standard Edition to Enterprise Edition on the same machine, the billing meter automatically switches from Standard to Enterprise both for IP and ESU. For ESU, there's no additional charge for the upgrade. However, you are billed for the difference between the original SE bill-back and the new EE bill-back going forward.

## What do ESUs include?

ESUs include Security Updates and Bulletins rated **Critical** by the [Microsoft Security Response Center (MSRC)](https://msrc.microsoft.com/update-guide), for a maximum of three years after the end of extended support. Microsoft distributes ESUs if and when available. Technical support is limited to issues directly related to the released updates.

## Why do ESUs only offer "critical" updates?

For End of Support events in the past, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] provided only Critical Security Updates, which meets the compliance criteria of our enterprise customers. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't ship a general monthly security update. [!INCLUDE [msCoName](../../includes/msconame-md.md)] only provides on-demand [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] security updates (GDRs) for MSRC bulletins where [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is identified as an affected product.

If there are situations where new [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] important updates aren't provided, and it's deemed critical by the customer but not by MSRC, Microsoft works with the customer on a case-by-case basis to suggest appropriate mitigation.

## What Licensing programs are eligible for ESUs?

Software Assurance customers can subscribe to ESUs on-premises under an Enterprise Agreement (EA), Enterprise Subscription Agreement (EAS), a Server & Cloud Enrollment (SCE), or an Enrollment for Education Solutions (EES). Software Assurance doesn't need to be on the same enrollment.

## How are ESUs licensed?

For SQL Server, licensing is based on the number of virtual cores (v-cores). If you have multiple virtual machines (VMs), you need to pay for all the v-cores used across those VMs. There is a 4-core minimum per VM. Licensing is based on physical cores (p-cores) of the host that runs one or multiple instances of SQL Server installed directly on the host without using VMs.

For unlimited virtualization, you can subscribe to ESUs by p-cores if SQL Server ESUs by v-cores is more expensive than subscribing by the p-cores of the host.

## Do I need to be running the most current SQL Server Service Pack to benefit from ESUs?

Yes, you need to run [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] with the latest Service Pack to apply ESUs. [!INCLUDE [msCoName](../../includes/msconame-md.md)] only produces updates that can be applied on the latest Service Pack.

## What are my options for SQL Server without Software Assurance?

If you don't have Software Assurance, the alternative option to get access to ESUs is to migrate to Azure or connect to Azure Arc. Since you don't have Software Assurance, make sure to use a "License included" license type (pay-as-you-go).

## Does this offer apply to older versions of SQL Server?

No. For [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and earlier versions, upgrade to the latest supported versions.

## Can I deploy a brand new SQL Server 2016 instance on Azure and still get ESUs?

Yes, you can also deploy a [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] instance on an Azure VM and subscribe to ESUs.

## Can I deploy a brand new SQL Server 2014 instance on Azure and still get ESUs?

Yes, you can deploy a new [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] instance on a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Azure Virtual Machine and have access to ESUs for free until ESU availability ends for [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)].

## Can I get technical support on-premises for SQL Server after the End of Support date, without subscribing to ESUs?

No. If you have [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] or [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], you can't log a support ticket even if you have a support plan.

If you subscribe to ESUs, you can log a support ticket for issues related to the extended security updates for your SQL Server instance, or issues introduced as a result of installing the ESUs.

## If I want to bring my own SQL Server license (BYOL), am I required to have Software Assurance coverage?

Yes, you need to have Software Assurance to take advantage of the BYOL program for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Azure Virtual Machines as part of the License Mobility program. For customers without Software Assurance, we recommend that you move to Azure SQL Managed Instance for your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] environments.

You can also migrate to pay-as-you-go Azure Virtual Machines. Software Assurance customers who license SQL by core also have the option of migrating to Azure using the Azure Hybrid Benefit (AHB).

Azure SQL Managed Instance is a service in Azure providing nearly 100% compatibility with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on-premises. SQL Managed Instance provides built-in high availability and disaster recovery capabilities plus intelligent performance features and the ability to scale on the fly. SQL Managed Instance also provides a version-less experience that takes away the need for manual security patching and upgrades. For more information on the Azure Hybrid Benefit program, see [Azure SQL Managed Instance pricing](https://azure.microsoft.com/pricing/details/azure-sql-managed-instance/single/).

## What options do I have to run SQL Server in Azure?

[!INCLUDE [2016-esu](../../includes/2016-esu.md)]

To learn about options for running SQL Server instances on Azure, see [SQL Server end of support](sql-server-end-of-support-overview.md).

## Can I use Azure Hybrid Benefit for SQL Server 2014 or SQL Server 2016?

Yes, customers with active Software Assurance or equivalent Server Subscriptions can use Azure Hybrid Benefit to allocate on-premises license investments for discounted pricing for Azure SQL Database, Azure SQL Managed Instance, and SQL Server on Azure VMs.

## Can I get free ESUs on Azure Stack?

Not for SQL Server 2016 ESUs but yes for SQL Server 2014 ESUs. If you have [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] running on Azure Stack, you can get ESUs for free. If you have [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] running on Azure Stack, you can purchase ESUs.

## Can I get ESUs for SQL Server with a third-party hosting provider?

You can get ESUs for third-party hosted SQL Server by subscribing to ESUs after connecting your instance to Azure Arc. If you can't connect your instance to Azure Arc, you're not eligible for this offer.

You can't get ESUs if you move your [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] environment to a PaaS implementation on other cloud providers. If your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running on a virtual machine (IaaS) with License Mobility for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] via Software Assurance, you can subscribe to ESUs enabled by Azure Arc.

## Are all SQL Server enabled by Azure Arc features available in Azure Government?

No. Feature availability can differ in Azure Government compared to the commercial Azure cloud.

For current supported features and limitations, review [SQL Server enabled by Azure Arc in US Government](../azure-arc/us-government-region.md) and [Release notes - SQL Server enabled by Azure Arc](../azure-arc/release-notes.md).

## How do US Federal Government customers register and obtain ESUs if they are running in Azure Government/O365 GCCH/O365 DOD?

For [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], Azure Arc isn't currently supported in Azure Government regions. So if [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] is in an Azure Government region, contact your account team to purchase ESUs through volume licensing, and then follow the steps to [register your disconnected instance](extended-security-updates-disconnected-instances.md). 

For both [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] and [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] ESUs, Federal Government customers can get ESUs by migrating their workloads to SQL Server on Azure VMs in supported Azure Government regions. ESUs are free for [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] and purchasable for [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)].

## What are the benefits of subscribing to ESUs?

For the benefits of the ESU subscription, see [SQL Server ESUs](sql-server-extended-security-updates.md).

## How do I subscribe to ESUs on multiple SQL Server instances?

If you want to subscribe to ESUs for multiple [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances, use an [open source PowerShell script](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-arc-enabled-sql-server/modify-license-type) that you can use for different scopes, such as resource group, Azure subscription, multiple subscriptions, or the entire account. This option is currently only available to SQL Server instances enabled by Azure Arc.

Alternatively, you can assign an [Azure policy](../azure-arc/manage-configuration.md#subscribe-to-extended-security-updates-at-scale-by-using-azure-policy) to your Azure scope that enables ESU on all SQL Server resources in that scope.

## How can I cancel the ESU charges?

Your ESU charges stop immediately in the following cases:

- You manually unsubscribe from ESUs for any reason. You lose access to future patches.
- You migrated your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to Azure; no manual cancellation is needed. You continue to have access to future ESUs.
- You upgraded your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to a newer version; no manual cancellation is needed. You have access to all SQL Server updates.

## What happens if I accidentally cancel my subscription and need to reinstate it?

If you accidentally cancel your ESU subscription, you can reactivate it within 30 days as long as your server remains connected. You're billed back to the hour of when the ESU subscription was last active. After 30 days, the ESU subscription is terminated, and you lose access to future patches. To gain access to future patches, you need to re-enable ESUs, which is treated as a brand new ESU subscription.

## What happens if I lose internet connectivity to Azure due to an outage?

If your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance loses connectivity, the billing stops, and the subscription is suspended. We automatically reactivate it if the instance reconnects within 30 days. Your bill includes a charge for the days since the last day your server was connected. If the server reconnects after 30 days of disconnection, the subscription is terminated.

> [!NOTE]
> The ESU setting in the Azure portal isn't automatically changed. If ESU is enabled for the server, the reconnection after 30 days of disconnection is treated as a new ESU subscription and billed accordingly.

## What happens if my machine's Virtual Machine ID (VMID) changes?

If your machine's VMID changes (due to VM rebuild, migration, hardware changes, or cloning), Azure Arc treats it as an entirely new machine, even if it's running on the same physical or virtual infrastructure. This change can result in double billing because:

- The original machine resource continues to be billed for ESU if you don't manually deactivate it.
- The machine with the new VMID is treated as a new ESU subscription and triggers backbill charges.

**To avoid double billing:**

1. Before performing any operation that might change the VMID, unsubscribe from ESU on the original machine.
1. Disconnect the machine from Azure Arc.
1. After the VMID change, onboard the machine as a new resource to Azure Arc.
1. Subscribe to ESU on the new machine resource.

> [!NOTE]
> For SCVMM machines, see [Remove SCVMM management from Azure Arc](/azure/azure-arc/system-center-virtual-machine-manager/remove-scvmm-from-azure-arc?tabs=for-windows-virtual-machines).
>
> For VMware machines, see [Remove vCenter from Azure Arc](/azure/azure-arc/vmware-vsphere/remove-vcenter-from-arc-vmware).

**If you already experienced double billing due to a VMID change**, contact Microsoft Support immediately to resolve the billing discrepancy.

## Can I reactivate an ESU subscription if I migrated to Azure, but then decide to move back to on-premises?

If you migrated an instance to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on Azure VM, and then moved back and restarted the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on the same machine, Azure Arc treats this case as a temporary disconnection. You must ensure that the original server reconnects to Azure Arc by using the same Azure subscription, resource group, and machine name. The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance must have the same instance name. Under those conditions, the ESU subscription is automatically reactivated if the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] reconnects within 30 days. After 30 days of disconnection, a new ESU subscription is required.

## What happens if I have an active ESU subscription, but unintentionally disconnect SQL Server from Azure Arc by uninstalling the extension?

You don't have access to the patches until you reinstall the Azure extension for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. The following rules apply:

- If the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance resource is still present as `Offline`, you incur an extra charge for the period while the instance was disconnected, because the ESU subscription remains active.
- If you reinstall the Azure extension for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] in the same subscription or resource group, you pay an extra charge for the period while the instance was disconnected, because the ESU subscription remains active.
- If you reinstall the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance to a different subscription, resource group, or region, the instance is considered to be a new resource. You must enable ESUs and pay for it.

## What happens if I take a SQL Server VM with an active ESU subscription offline for some time, and then bring it back online?

You don't have access to the patches until the VM is online. When the VM is back online and the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance shows a status of `Connected`, the following rules apply:

- You're charged for the period of downtime (back to the previous ESU charge).
- The ESU subscription is reactivated.

## Does billing start automatically when I subscribe to ESUs?

It depends on your ESU subscription: 
- The [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] ESU subscription is currently available, so billing starts immediately when you subscribe. You receive a one-time bill-back charge to the beginning of the current ESU term. 
- Billing for [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] starts at midnight UTC on July 15, 2026. If you subscribe before the end of the support period, billing starts on the first day of the ESU term. If you subscribe after the end of the support period, billing starts on the day you subscribe and includes a one-time bill-back charge to the first day of the ESU term.

For more information about ESU pricing, see [Plan your Windows Server and SQL Server end of support](https://www.microsoft.com/windows-server/extended-security-updates).

## Is Microsoft extending SQL Server 2008 / 2008 R2 and Windows Server 2008 ESUs?

Microsoft hasn't extended [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE [winserver2008-md](../../includes/winserver2008-md.md)] ESUs. Support for [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] and [!INCLUDE [sql2008r2-md](../../includes/sql2008r2-md.md)] in Azure ended on July 9, 2023, and these servers need to be upgraded to maintain support.

## Can I subscribe to asynchronous notifications about the published ESUs?

The [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] ESUs are included in the [Security Update Guide](https://msrc.microsoft.com/update-guide/) published by the Microsoft Security Response Center (MSRC). Customers can subscribe to the RSS feed, use automation services like IFTTT to create automated actions, or subscribe to direct email notifications from that page.

## Does SQL Server ESU license resource for unlimited virtualization support Standard edition?

No. The [SQL Server ESU license resource](../azure-arc/extended-security-updates.md#esu-license-resource) for SQL Server enabled by Azure Arc is an implementation of the unlimited virtualization benefit for ESU. Once activated, the license is billed using an Enterprise edition ESU meter based on the number of physical cores of the servers that host the virtual machine with ESU-eligible SQL Server instances.

## Does SQL Server ESU license resource for unlimited virtualization support virtual cores?

No. The [SQL Server ESU license resource](../azure-arc/extended-security-updates.md#esu-license-resource) for SQL Server enabled by Azure Arc is an implementation of the unlimited virtualization benefit for ESU. It applies to physical cores of the servers that host the virtual machine with ESU-eligible SQL Server instances with a minimum of 16 cores. The SQL Server instances in the virtual machines hosted by the servers get access to the ESUs without an additional charge.

## Can I use 3rd-party application control solutions to protect my SQL Server environment, instead of installing SQL Server security updates after the end of support date? 

Third party application control solutions aren't a replacement for product security fixes.

## Does Software Assurance benefits include ESUs?

No. Software Assurance (SA) is a prerequisite for ESUs but the ESU subscription must be activated separately. Review [What Licensing programs are eligible for ESUs?](#what-licensing-programs-are-eligible-for-esus)

SQL Server ESU License uses pay-as-you-go (PAYG) billing plan.

## Related content

- [What are Extended Security Updates for SQL Server?](sql-server-extended-security-updates.md)
- [Extended Security Updates enabled by Azure Arc](../azure-arc/extended-security-updates.md)
