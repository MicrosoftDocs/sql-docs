---
title: Extended Security Updates
description: Learn how to manage licensing and billing of Extended Security Updates for SQL Server.
author: MikeRayMSFT
ms.author: sashan
ms.reviewer: randolphwest, maghan
ms.date: 09/25/2024
ms.topic: conceptual
ms.custom:
  - references_regions
---

# SQL Server Extended Security Updates enabled by Azure Arc

[!INCLUDE [sql-migration-end-of-support](../../includes/applies-to-version/sql-migration-end-of-support.md)]

After [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] reaches the end of its support lifecycle, you can sign up for an Extended Security Update (ESU) subscription for your servers and remain protected for up to three years. When you upgrade to a newer version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you can terminate your ESU subscription and stop paying for it. When you [migrate to Azure SQL](/azure/azure-sql/migration-guides/), the ESU charges automatically stop but you continue to have access to the security updates.

This article explains how to manage a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] subscription to Extended Security Updates enabled by Azure Arc. For more information about the program, see [What are Extended Security Updates for SQL Server?](../end-of-support/sql-server-extended-security-updates.md).

## Subscribe to Extended Security Updates in a production environment

You can use one of the following three options to subscribe to ESUs in a production environment. The links in the list take you to sections in this article that provide more details.

The diagrams in the list use normalized cores (NCs) to illustrate the cost implications of the licensing options. One core license for the Standard edition is equivalent to one NC. One core license for the Enterprise edition is equivalent to four NCs. For more information, see [How licenses apply to Azure resources](/azure/cost-management-billing/scope-level/overview-azure-hybrid-benefit-scope#how-licenses-apply-to-azure-resources).

- [License by virtual cores](#license-esu-vcores)

  Use an Enterprise or Standard ESU subscription for the vCPUs (v-cores) of the virtual machine (VM) that runs one or multiple instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Each virtual machine is billed individually for the v-cores allocated to it.

  The following diagram illustrates this licensing method and the cost implications.

  :::image type="content" source="media/extended-security-updates/virtual-core-licensing.svg" alt-text="Diagram that illustrates the virtual core licensing option.":::

- [License by physical cores (p-cores) without virtual machines](#license-pcores-without-vms)

  Use an Enterprise or Standard license for the p-cores of the host that runs one or multiple instances of SQL Server installed directly on the host without using VMs. Each instance has access to all p-cores supported by the installed edition limits, up to all p-cores of the host. Regardless of the instance limits, the host is billed for all the p-cores based on the highest [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] edition installed on it. For details, see [Compute capacity limits by edition of SQL Server](../compute-capacity-limits-by-edition-of-sql-server.md).

  The following diagram illustrates the cost implications of deploying two Standard instances on a physical host without using VMs.

  :::image type="content" source="media/extended-security-updates/physical-core-licensing-without-vms.svg" alt-text="Diagram that illustrates physical core licensing without using virtual machines.":::

- [License by physical cores with unlimited virtualization](#unlimited-virtualization)

  Use an Enterprise ESU subscription for the physical cores of the host that runs any number of virtual machines with any number of out-of-support [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances. A single p-core license is a separate Azure resource that represents all p-cores licensed for ESUs and is billed independently.

  The following diagram illustrates the cost implications of licensing a physical host and using unlimited virtualization.

  :::image type="content" source="media/extended-security-updates/physical-core-licensing-with-vms.svg" alt-text="Diagram that illustrates physical core licensing with unlimited virtualization.":::

To subscribe to ESUs, you must have active Software Assurance or enable a pay-as-you-go billing for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] software. The following table shows ESU availability for each license type that you use for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] software:

| SQL Server license type | ESU v-core subscription | ESU p-core subscription without VMs | ESU p-core subscription with unlimited virtualization |
| --- | --- | --- | --- |
| Subscribe to the service through Microsoft Azure by using a pay-as-you-go method | Yes | Yes | Yes |
| Bring your own license with Software Assurance or a SQL Server subscription <sup>1</sup> | Yes | Yes | Yes |
| Bring your own license without Software Assurance <sup>2</sup> | No | No | No |

<sup>1</sup> You already have a license with active Software Assurance or an active SQL Server subscription.

<sup>2</sup> You own a perpetual license or use a Server+CAL license.

Your choice of payment option might affect your outsourcing options. For more information, see the [service-specific terms](https://www.microsoft.com/licensing/terms/productoffering/MicrosoftAzure/eaeas#ServiceSpecificTerms) and the [Flexible Virtualization Benefit licensing guide](https://www.microsoft.com/licensing/docs/view/Virtualization).

For information about licensing your nonproduction out-of-support [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances for ESUs through Azure Arc, see [Manage SQL Server ESU subscriptions for nonproduction use](extended-security-updates.md#non-production-esu-licensing) later in this article.

## <a id="license-esu-vcores"></a> Subscribe to SQL Server ESUs by virtual cores

Subscribing to ESUs by v-cores allows you to limit the scope of the subscription to a specific virtual machine and one or more out-of-support [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances installed on the operating system environment (OSE) of that machine. It's optimized for the following scenarios:

- Your out-of-service [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] VMs are mixed with the VMs running other software on the same physical servers.
- You run your out-of-service [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] VMs in a hosted environment or in a non-Microsoft cloud where you don't control the physical infrastructure.

You manage a v-core ESU subscription for each VM by using the [SQL Server configuration](manage-configuration.md) areas of the Azure portal. The **Overview** pane for each Azure Arc-enabled [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] resource shows the ESU subscription status under **SQL Server configuration**.

The ESU subscription for the Standard edition is limited to a maximum of 24 v-cores, even if the OSE is configured with more v-cores. For more information about limits, see [Compute capacity limits by edition of SQL Server](../compute-capacity-limits-by-edition-of-sql-server.md).

## <a id="license-pcores-without-vms"></a> Subscribe to SQL Server ESUs by physical cores without using VMs

The option of subscribing to SQL Server ESUs by physical cores without using VMs is optimized for the following scenarios:

- You control your physical environment.
- Your out-of-support [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances are installed directly on a physical server to maximize the performance of your database application.
- You aren't running virtual machines on that physical server.

In this option, the ESU subscription requirements are similar to [subscribing to SQL Server ESUs by virtual cores](#license-esu-vcores). You manage the ESU subscription for each host by using the [SQL Server configuration](manage-configuration.md) areas of the Azure portal. The main difference is that the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] software usage is reported based on the physical cores available to the OSE of that server. For details, see [Metering software usage](extended-security-updates.md#esu-usage-metering).

The ESU subscription for the Standard edition is limited to a maximum of 24 p-cores, even if the OSE is installed on a larger machine. For more information about limits, see [Compute capacity limits by edition of SQL Server](../compute-capacity-limits-by-edition-of-sql-server.md).

> [!IMPORTANT]  
> If a physical machine without VMs is connected to Azure Arc in the scope that a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] ESU p-core license covers, the unlimited virtualization benefit doesn't apply to that machine. It's billed individually, based on the physical cores that the OSE can access.

## <a id="unlimited-virtualization"></a> Subscribe to SQL Server ESUs by physical cores with unlimited virtualization

The option of subscribing to SQL Server ESUs by physical cores with unlimited virtualization is most effective when:

- You control your physical environment and run the out-of-support [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances on different VMs for security isolation and better resource management.
- Your infrastructure and the selected payment method support the unlimited virtualization benefit for ESU.
- Subscribing to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] ESUs by v-cores is more expensive than subscribing by the p-cores of the host.

To use the unlimited virtualization benefit, you need to create a *SQLServerEsuLicense* resource that covers the specific *SQL Server - Azure Arc* instances that you intend to include. For details about managing *SQLServerEsuLicense* resources, see [Manage the unlimited virtualization benefit for a SQL Server ESU subscription](manage-configuration.md#manage-pcore-esu-license).

> [!CAUTION]  
> The unlimited virtualization benefit isn't available to VMs running on infrastructure from any of the [listed providers](https://aka.ms/listedproviders). These VMs can be licensed only by v-cores. If you create a *SqlServerEsuLicense* resource with the intent of licensing these VMs by using unlimited virtualization, you'll be charged for the consumption of v-cores based on the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] configuration of the host. Any existing p-core licenses don't apply to offset such charges.

For more information about licensing by physical cores with unlimited virtualization, see the section "Licensing for maximum virtualization" in the [SQL Server licensing guide (download link)](https://download.microsoft.com/download/e/2/9/e29a9331-965d-4faa-bd2e-7c1db7cd8348/SQL_Server_2019_Licensing_guide.pdf).

A single *SqlServerEsuLicense* resource can cover multiple virtual machines connected to Azure Arc. It includes several properties that define how the license is applied and billed.

To qualify, each *Machine - Azure Arc* resource must be configured to use a physical core ESU license. Otherwise, the *Machine - Azure Arc* resource is billed for ESUs individually.

### License details

The **License details** tab includes the standard Azure properties and the ESU license-specific settings:

- The `scopeType` property sets the Azure scope in which the license covers all qualified *Machine - Azure Arc* resources. The following Azure scopes are supported:

  - Azure tenant (`Tenant`)
  - Azure subscription (`Subscription`)
  - Resource group (`ResourceGroup`)

  The specific scope is derived from the location of the license resource. For example, if you select `Subscription`, the subscription ID that hosts the license resource is used as the scope.

- The `billingPlan` property is set to pay-as-you-go billing automatically, because an ESU subscription is always billed on an hourly meter.

- The `physicalCores` property of the license resource represents the sum of physical cores of the servers in the selected scope to which the license is applied. The minimum size of the license is 16 p-cores.

- The `TenantID` property is automatically set when the tenant scope is selected.

You can create the license resource in a resource group in any of the [supported regions](overview.md#supported-azure-regions). The location of the resource is set to the location of the selected resource group by default, but you can change it to a different region.

The location of the license resource doesn't affect the scope. It applies to all *Machine - Azure Arc* resources in the selected license scope, regardless of the regions where these resources are onboarded.

> [!IMPORTANT]  
> You can associate multiple license resources with the same scope or overlapping scopes. For example, you can add a new license when you deploy additional physical servers during temporary bursts of activity, or to reflect unexpected growth.
>  
> All the virtual machines running on these physical servers must be connected to Azure Arc in the scope of the license resource. And they must have the `UsePhysicalEsuCoreLicense` host configuration property set to `True`. For more information, see [Use a physical core ESU license](manage-configuration.md#use-physical-core-esu-license).

### License activation

You use the **License Activation** tab to control when the license takes effect or is deactivated. You can activate the license during creation, or you can create the license first and then activate it later. Delaying the activation allows you to coordinate it with other events in the licensing life cycle, such as the expiration of an existing Enterprise Agreement. The `activatedAt` and `deactivatedAt` time-stamp properties show when the license was last activated and deactivated. For more information, see [Update a SQL Server license resource](manage-configuration.md#change-license-resource).

After the license is activated:

- You can't change the license version.
- You can decrease the core count, but you can't increase it. To increase the core count, create another license resource.

After the license is terminated:

- You can't reactivate the license. You can delete the resource if you don't need it.
- The ESU subscriptions for the VMs in scope remain active and become billable at the VM level.

To stop all ESU charges, unsubscribe from ESUs on all virtual machines before you terminate the license. For details, review [Manage resources in the scope of an ESU p-core license](manage-configuration.md#manage-esu-license-resources).

## <a id="non-production-esu-licensing"></a> Manage SQL Server ESU subscriptions for nonproduction use

If you enabled ESU subscriptions in your production environment managed through Azure Arc, you can enable a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] ESU subscription in the nonproduction environment for free. You can take advantage of this benefit in the following two ways.

### Use SQL Server Developer edition

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Developer edition is free and can be used in any Azure subscription. If you enable the ESU subscription on the VM that's hosting a Developer edition, Azure Extension for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] detects it and reports the usage via a $0 *Dev edition* meter. The extension doesn't generate the ESU charges. At the same time, it installs the ESUs when they're released, as long as the ESU subscription is active. The Developer edition has the same feature set as the Enterprise edition.

> [!IMPORTANT]  
> If the Developer edition is colocated on the same host with an instance of the Standard or Enterprise edition, the latter takes billing precedence as a production edition. The active ESU subscription generates the ESU charges according to that edition.

### Use an Azure dev/test subscription

If you configure your nonproduction environment as a mirror of the production environment, and you want to use the same editions that you use in production, you must connect the hosting machines and the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances to an Azure dev/test subscription.

The production [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] meters are enabled to support the dev/test subscriptions and are automatically nullified when they're emitted from a dev/test subscription. The same conditions apply to the ESU meters. So it's safe to enable ESU subscription on these machines even if they run the Standard or Enterprise edition.

For information, see:

- [Create an Enterprise Agreement subscription](/azure/cost-management-billing/manage/create-enterprise-subscription#create-an-ea-subscription).
- The section "Licensing SQL Server for nonproduction use" in the [SQL Server licensing guide (download link)](https://download.microsoft.com/download/e/2/9/e29a9331-965d-4faa-bd2e-7c1db7cd8348/SQL_Server_2019_Licensing_guide.pdf).

## Manage SQL Server ESU subscriptions on high-availability and disaster recovery replicas

If your out-of-service [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance is a passive replica created as part of your high-availability or disaster recovery configuration, you're entitled to the failover benefits that are included if your license type is set to `Paid` or `PAYG`. For more information about the failover benefits, see the section "Licensing SQL Server for high availability and disaster recovery" in the [SQL Server licensing guide (download link)](https://download.microsoft.com/download/e/2/9/e29a9331-965d-4faa-bd2e-7c1db7cd8348/SQL_Server_2019_Licensing_guide.pdf).

To help you manage the failover benefits and remain compliant, Azure Extension for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] automatically detects the passive instances and reflects the use of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] software by emitting special $0 meters for disaster recovery, as long as you properly configured the `LicenseType` property. For more information, see [Metering software usage](manage-license-billing.md#usage-metering).

During the failovers, the extension is aware of the transition and automatically switches the ESU billing to the active replica without new billback charges.

## <a id="server-cal"></a> Manage SQL Server instances that use a Server+CAL license

You can connect any licensed [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance to Azure Arc, including instances that use the Server+CAL licensing model. However, the ESU subscription enabled by Azure Arc isn't available for the out-of-support Server+CAL licensing model. If you want to receive ESUs, you can set the license type to `PAYG` and then enable the ESU subscription.

## <a id="license-transition"></a> Manage the transition from a p-core ESU license to a v-core ESU license

Because the p-core ESU license is billed with an ESU meter for the Enterprise edition, it's cost-effective when the out-of-support [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances are colocated on a set of designated physical hosts. As you upgrade the individual instances or migrate them to Azure, you might lose the cost-effectiveness of the p-core ESU license. Using the v-core ESU licensing might then become more attractive. You can terminate the p-core ESU license and switch to billing the individual VMs for the ESU subscriptions.

To properly manage this transition, use the following sequence of best practices:

1. Ensure that the VMs with the out-of-support [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances are connected to Azure Arc and configured to [use the p-core ESU license](manage-configuration.md#use-physical-core-esu-license) after that license is activated.

1. Continuously evaluate the cost benefits of using the p-core ESU license.

1. Terminate the p-core ESU license if it's no longer financially beneficial, but keep the ESU subscription active on the individual VMs.

> [!IMPORTANT]  
> If the VMs in scope are configured to use a ESU subscription *while the p-core ESU license is active* (as described in step 1), after the p-core license termination, they automatically switch to billing for ESU based on the installed [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] edition and the v-core count of each VM. There are no additional billback charges.  
>  
> If the VM is configured to use the ESU subscription *after the p-core ESU license is terminated*, it's treated as a new subscription and the appropriate billback charges apply.

## <a id="vl-sku-transition"></a> Manage the transition from an ESU license purchased through Volume Licensing to an ESU subscription

The [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] ESU subscription requires you to purchase a Year 1 Volume Licensing ESU offer before you can activate it. That is, the transition from a Year 1 Volume Licensing ESU offer to an ESU subscription is the default pattern, and the billback charges reflect it. For details about the billback charges, see [Billing for SQL Server 2012 ESUs](#2012-esu-billing) later in this article.

If you purchase a Year 2 Volume Licensing ESU offer and then decide to switch to an ESU subscription, you must take additional steps to ensure that the billback is adjusted accordingly. Before you activate the ESU subscription on the machines that a Year 2 Volume Licensing ESU license covers, you must open a support ticket by using the subcategory `\<new subcategory>`.

## <a id="esu-usage-metering"></a> Understand ESU usage meters

The usage of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] ESU subscription is reported once an hour. The specific meter is automatically selected based on the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] edition and the number v-cores or p-cores visible to the OSE. The following rules apply:

- If you install one or several [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances on a virtual machine and don't specify the use of a p-core ESU license, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] ESU subscription usage is metered based on the total number of virtual cores available to the OSE. The minimum is four cores per OSE.

- If you install one or several [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances on a physical server without using virtual machines, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] ESU subscription usage is metered based on the total number physical cores available to the OSE. The minimum is four cores per OSE.

- [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] ESU subscription usage is reported per OSE whether one or multiple [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances are installed on the same OSE.

- If multiple out-of-support [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances of the same version are installed on the same OSE, the highest [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] edition defines the ESU subscription meter that's sent every hour.

- If multiple out-of-support instances of both [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)] and [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] are installed on the same OSE, with the same or different editions, one instance of the same version is billed separately because they have different prices and billback periods.

The following table shows the ESU subscription meters (also called *SKUs*) that are used for metering and billing for a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] ESU subscription on a single OSE:

| Projected edition <sup>1</sup> | SQL Server version | Failover replica | Use p-core license | Meter SKU |
| --- | --- | --- | --- | --- |
| Enterprise | 2012 | No | No | `Ent edition - ESU`<br />`Ent edition - ESU back billing` |
| Enterprise | 2012 | No | Yes | None |
| Enterprise | 2012 | Yes | Yes or no | None |
| Enterprise | 2014 | No | No | `Ent edition - ESU 2014`<br />`Ent edition - ESU 2014 back billing` |
| Enterprise | 2014 | No | Yes | None |
| Enterprise | 2014 | Yes | Yes or no | None |
| Standard | 2012 | No | No | `Std edition - ESU`<br />`Std edition - ESU back billing` |
| Standard | 2012 | No | Yes | None |
| Standard | 2012 | Yes | Yes or no | None |
| Standard | 2014 | No | No | `Std edition - ESU 2014`<br />`Std edition - ESU 2014 back billing` |
| Standard | 2014 | No | Yes | None |
| Standard | 2014 | Yes | Yes or no | None |
| Evaluation | Any | Yes or no | Yes or no | None |
| Developer | Any | Yes or no | Yes or no | None |
| Web | Any | Not applicable | Yes or no | None |
| Express | Any | Not applicable | Yes or no | None |

<sup>1</sup> For edition projection rules, see [Metering software usage](manage-license-billing.md#usage-metering).

The next table shows the meter SKUs that are used for metering and billing for active p-core ESU licenses:

| Azure resource | SQL Server version | Meter SKU |
| --- | --- | --- |
| P-core ESU license | 2012 | `Ent edition - ESU`<br />`Ent edition - ESU back billing` |
| P-core ESU license | 2014 | `Ent edition - ESU 2014`<br />`Ent edition - ESU 2014 back billing` |

## Understand ESU subscription billing

The ESU subscription extends support for critical updates for up to three years. If you start the subscription after the end-of-support date, you must purchase the Volume Licensing offer or ESU subscription to cover any previous years. With an ESU subscription, you have the additional benefit of canceling the subscription and all future charges without penalty at any time.

### <a id="2012-esu-billing"></a> Billing for SQL Server 2012 ESUs

Because the ESU subscription option was introduced in Year 2 of the [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] extended support period, you must have purchased the Year 1 Volume Licensing ESU offer before signing up for the ESU subscription in Year 2. You can sign up for the ESU subscription at any time within Year 2, and your bill reflects the cost of continuous ESU coverage.

After you sign up for the ESU subscription, your next monthly bill includes a one-time billback charge for each machine that hosted a [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] instance or instances with an active ESU subscription from July 12, 2023, to the date of activation. From this point, you're billed for each machine on an hourly basis.

Both billback and regular hourly charges use the hourly rate from this formula: *(core count) x (100% of Year 2 ESU license price) / 730*. So, the size of the billback charge depends on how much time passes from July 12, 2023, to the activation time.

The following billing rules apply:

- If you install a [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] instance or instances on a virtual machine, and you don't use the unlimited virtualization benefit, you're billed for the total number of virtual cores of the machine, with a minimum of four cores. If the virtual machine is eligible to receive failover rights, the virtual cores of that machine aren't billable.

- If you install a [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] instance or instances on a physical server without using virtual machines, you're billed for all physical cores of the machine, with a minimum of four cores. If the physical server is eligible to receive failover rights, the physical cores of that server aren't billable. For more information, see the [service-specific terms](https://www.microsoft.com/licensing/terms/productoffering/MicrosoftAzure/eaeas#ServiceSpecificTerms).

For more information about [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] ESU pricing, see [Plan your Windows Server and SQL Server end of support](https://www.microsoft.com/windows-server/extended-security-updates).

### <a id="2014-esu-billing"></a> Billing for SQL Server 2014 ESUs

The ESU subscription for [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] is available from Year 1 of the extended support period, which started on July 10, 2024. If you signed up before that date, you see only the hourly ESU charges starting at midnight on July 10, 2024. If you signed up after July 10, 2024, your next month's bill includes a billback charge from July 10, 2024, to the date of activation.

The following billing rules apply:

- If you install a [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] instance or instances on a virtual machine, and you don't use the unlimited virtualization benefit, you're billed for the total number of virtual cores of the machine, with a minimum of four cores. If the virtual machine is eligible to receive failover rights, the virtual cores of that machine aren't billable.

- If you install a [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] instance or instances on a physical server without using virtual machines, you're billed for all physical cores of the machine, with a minimum of four cores. If the physical server is eligible to receive failover rights, the physical cores of that server aren't billable. For more information, see the [service-specific terms](https://www.microsoft.com/licensing/terms/productoffering/MicrosoftAzure/eaeas#ServiceSpecificTerms).

- If you install both instances of [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] on the same physical or virtual machine, you're billed for the total number of physical or virtual cores of the machine, for both [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] ESU subscriptions, with a minimum of four cores. The billing for each version is based on the ESU price for that version. If the virtual machine is eligible to receive failover rights, the virtual cores of that machine aren't billable.

For more information about [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] ESU pricing, see [Azure Arc pricing](https://azure.microsoft.com/pricing/details/azure-arc/core-control-plane/).

### <a id="esu-billing-during-connectivity-loss"></a> Billing during connectivity loss and other disruptions

If your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance loses connectivity, the billing stops, and the subscription is suspended.

To make sure that intermittent disconnection doesn't negatively affect your ESU coverage, we automatically reactivate it without penalty if the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance reconnects within 30 days. In that case, you see an additional billback charge for the days since the last day that your server was connected.

If you manually terminate the ESU subscription and then reactivate it within 30 days, there's also no penalty. Your bill includes an additional charge for the time since you canceled the subscription. If the server reconnects after 30 days of disconnection, the subscription is terminated. To resume the ESU coverage, you need to activate a new ESU subscription and pay all the associated billback charges.

> [!IMPORTANT]  
> The billback charges are recorded within the first hour of the ESU subscription and look like single hourly charges for the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances that have the ESU subscriptions enabled. Because the amount reflects the accumulated costs since one of the following dates, it's much higher than the regular hourly ESU charges:
>  
> - July 11, 2023, for [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)]
> - July 10, 2024, for [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)]
>  
> This difference is expected, and it should be a one-time charge.
>  
> During the following months, you should see only the regular hourly charges. Additional billback charges could be added in cases of connectivity disruptions, but they're typically much smaller amounts.

## Related content

- [Product terms for SQL Server enabled by Azure Arc](https://www.microsoft.com/licensing/terms/productoffering/MicrosoftAzure/eaeas#ServiceSpecificTerms)
- [SQL Server licensing guide (download link)](https://download.microsoft.com/download/e/2/9/e29a9331-965d-4faa-bd2e-7c1db7cd8348/SQL_Server_2019_Licensing_guide.pdf)
- [SQL Server 2022 pricing and licensing](https://www.microsoft.com/sql-server/sql-server-2022-pricing)
- [Configure SQL Server enabled by Azure Arc](manage-configuration.md)
- [Frequently asked questions about billing](faq.yml#billing)
- [Extended Security Updates: Frequently asked questions](../end-of-support/extended-security-updates-frequently-asked-questions.md)
- [Prerequisites - SQL Server enabled by Azure Arc](prerequisites.md)
- [Automatically connect your SQL Server to Azure Arc](automatically-connect.md)
- [Manage the unlimited virtualization benefit for a SQL Server ESU subscription](manage-configuration.md#manage-pcore-esu-license)
