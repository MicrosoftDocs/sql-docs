---
title: Extended Security Updates
description: Learn how to manage licensing and billing of SQL Server Extended Security Updates enabled by Azure Arc. 
author: MikeRayMSFT
ms.author: sashan
ms.reviewer: randolphwest
ms.date: 06/05/2024
ms.topic: conceptual
ms.custom: references_regions
---

# SQL Server Extended Security Updates enabled by Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

Once [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] reaches the end of its support lifecycle, you can sign up for an Extended Security Update (ESU) subscription for your servers and remain protected for up to three years. When you upgrade to a newer version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you can terminate your ESU subscription and stop paying for it. When you [migrate to Azure SQL](/azure/azure-sql/migration-guides/), the ESU charges automatically stop but you continue to have access to the ESUs. This article explains how to manage SQL Server subscription to Extended Security Updates (ESU) enabled by Azure Arc. For details, see [What are Extended Security Updates for SQL Server?](../end-of-support/sql-server-extended-security-updates.md)

## Subscribing to Extended Security Updates in production environment

You can use one of the three options to subscribe to ESUs in production environment.

- [License by virtual cores](#license-esu-vcores)

   Use Enterprise or Standard ESU subscription for the vCPUs (v-cores) of the virtual machine that runs one or multiple instances of SQL Server. Each virtual machine is billed individually for the v-core allocated to it. The following diagram illustrates this licensing method and shows its cost implications.

   :::image type="content" source="media/billing/virtual-core-licensing.svg" alt-text="Screenshot illustrating the virtual core licensing option.":::

- [License by physical cores without virtual machines option](#license-pcores-without-vms)

   Use Enterprise or Standard Edition license for the physical cores (p-cores) of the host that runs one or multiple instances of SQL installed directly on the host without using VMs. Each instance has access to all p-cores supported by the installed edition up to all p-cores of the host. Regardless of the instance limits though, the host is billed for all the p-cores based on the highest SQL Server edition installed on it. For details, see [Compute capacity limits by edition of SQL Server](../compute-capacity-limits-by-edition-of-sql-server.md). The following diagram illustrates the cost implications of deploying two standard edition instances on a physical host without using VMs.

   :::image type="content" source="media/billing/physical-core-licensing-without-vms.svg" alt-text="Screenshot illustrating the physical core licensing without using virtual machines.":::

- [License by physical cores with unlimited virtualization](#unlimited-virtualization)

   Use Enterprise Edition ESU subscription for the physical cores (p-cores) of the host that runs any number of virtual machines with any number out-of-support SQL Server instances. A single p-core license is a separate Azure resource representing all p-cores licensed for ESU and is billed independently. The following diagram illustrates the cost implications of licensing a physical host and using unlimited virtualization.

   :::image type="content" source="media/billing/physical-core-licensing-with-vms.svg" alt-text="Screenshot illustrating the physical core licensing using unlimited virtualization.":::

> [!NOTE]
>
> *Normalized cores* (NC) are used to illustrate the cost implications of different licensing options. One Standard Edition core license is an equivalent of one NC. One Enterprise Edition core license is an equivalent of four NCs. For more information, see  [How licenses apply to Azure resources](/azure/cost-management-billing/scope-level/overview-azure-hybrid-benefit-scope#how-licenses-apply-to-azure-resources).

To subscribe to ESUs, you must have active Software Assurance (SA) or enable a pay-as-you-go billing for SQL Server software. The following table shows ESU availability depending on the license type you are using for SQL Server software.

| SQL Server license type | ESU v-core subscription | ESU p-core subscription without VMs | ESU p-core subscription with unlimited virtualization |
|---|---|---|---|
| Subscribe to the service through Microsoft Azure using a pay-as-you-go method | Yes | Yes | Yes |
| Bring your own license with SA or SQL subscription<sup>1</sup> | Yes | Yes | Yes |
| Bring your own license without SA<sup>2</sup> | No | No | No |

<sup>1</sup> You already have a license with active Software Assurance or an active SQL subscription.

<sup>2</sup> You own a perpetual license or use a Server+CAL license.

> [!NOTE]
>
> Your choice of payment options above may impact your outsourcing options. For more information, please see [Product Terms](https://www.microsoft.com/licensing/terms/productoffering/MicrosoftAzure/eaeas#ServiceSpecificTerms) and [Flexible Virtualization Benefit Licensing Guide](https://wwlpdocumentsearch.blob.core.windows.net/prodv2/Licensing_guide_PLT_Flexible_Virtualization_Benefit_Nov2022.pdf).

For information about licensing your non-production out-of-support SQL Server instances for ESU through Azure Arc, see [Managing SQL Server ESUs for non-production use](extended-security-updates.md#non-production-esu-licensing).

## <a id="license-esu-vcores"></a> Subscribe to SQL Server ESUs by virtual cores

Subscribing to ESUs by v-cores allows you to limit the scope of the subscription to a specific virtual machine (VM) and one or more out-of-support SQL Server instances installed on the operating system environment (OSE) of that machine. It's optimized for the following scenarios:

- Your out-of-service SQL Server VMs are mixed with the VMs running other software on the same physical servers.
- You run your out-of-service SQL Server VMs in a hosted environment or in a third party cloud where you don't control the physical infrastructure.

V-core ESU subscription is managed for each VM using [SQL Server configuration](manage-configuration.md) panel. For your convenience, **Overview** of each Arc-enabled SQL Server resource shows the ESU subscription status under **SQL Server Configuration**.

Standard Edition ESU subscription is limited to a maximum of 24 v-cores even if the OSE is configured with more v-cores. Details in [Compute capacity limits by edition of SQL Server](../compute-capacity-limits-by-edition-of-sql-server.md).


## <a id="license-pcores-without-vms"></a> Subscribe to SQL Server ESUs by physical cores without using VMs

This option is optimized for the following scenario:

- You control your physical environment
- Your out-of-support SQL Server instances are installed directly on a physical server to maximize the performance of your databases application.
- You are not running virtual machines on that physical server.

In this scenario, the ESU subscription requirements are similar to [Subscribing to SQL Server USEs by virtual cores](#license-esu-vcores), which is managed for each host using [SQL Server configuration](manage-configuration.md) panel. The main difference is that the SQL Server software usage is reported based on the physical cores available to the OSE of that server. For details, see [Metering software usage](extended-security-updates.md#esu-usage-metering).

Standard Edition ESU subscription is limited to a maximum of 24 p-cores even if the OSE is installed on a larger machine. Details in [Compute capacity limits by edition of SQL Server](../compute-capacity-limits-by-edition-of-sql-server.md).

> [!IMPORTANT]
> If a physical machine without VMs is connected to Azure Arc in the scope that is covered by a SQL Server ESU physical core license, the unlimited virtualization benefit won't apply to that machine. It's billed individually based on the physical cores the OSE has access to.

## <a id="unlimited-virtualization"></a> Subscribe to SQL Server ESUs by physical cores with unlimited virtualization

This licensing option is most effective when:

- You control your physical environment and run the out-of-support SQL Server instances on different VMs for security isolation and better resource management.
- Your infrastructure and the selected payment method support the unlimited virtualization benefit (UV) for ESU.
- Subscribing to SQL Server ESUs by v-cores is more expensive than subscribing by the p-cores of the host.

To leverage the UV benefit, you need to create a *SQLServerEsuLicense* resource covering the specific *SQL Server - Azure Arc* instances that you intend to include. For  details of managing *SQLServerEsuLicense* resources, see [Manage unlimited virtualization benefit for SQL Server ESU subscription](manage-configuration.md#manage-pcore-esu-license).

> [!CAUTION]
> The UV benefit is not available to VMs running on any listed providers' infrastructure. They can only be licensed by v-cores. If you create a *SqlServerEsuLicense* resource with the intent of licensing these VMs using UV, you will be charged for the consumption of v-cores based on the SQL Server configuration of the host, and any existing p-core licenses will not apply to offset such charge. See [Listed providers](https://aka.ms/listedproviders) for details.

A single *SqlServerEsuLicense* resource can cover multiple virtual machines connected to Azure Arc. It includes several properties that define how the license is applied and billed. 

To qualify, each *Machine - Azure Arc* resource must be configured to use a physical core ESU license. Otherwise, the *Machine - Azure Arc* resource will be billed for ESUs individually. See [Subscribe to SQL Server ESUs by virtual cores](#license-esu-vcores).

The **License details** tab includes the standard Azure properties and the ESU license-specific settings.  

The **Scope** property sets the Azure scope in which all qualified *Machine - Azure Arc* resource are covered by the license. The following Azure scopes are supported:

- Azure tenant (`Tenant`)
- Azure subscription (`Subscription`)
- Resource group (`ResourceGroup`)

> [!NOTE]
> The specific scope will be derived from the location of the license resource. E.g., if `Subscription` is selected, the subscription ID hosting the license resource will be used as the scope. 

The **Billing plan** property is set to PAYG automatically as ESU subscription is always billed on an hourly meter. 

The **Physical cores** property of the license resource represents the sum of physical cores of the servers in the selected scope to which the license will be applied. The minimum size of the license is 16 p-cores.

The license resource can be created in a resource group in any of the supported regions. For the list of supported regions, see [Supported Azure regions](overview.md#supported-azure-regions). By default, the location of the resource is set to the location of the selected resource group. But you can change it to a different region. It doesn't impact the scope. It will apply to all *Machine - Azure Arc* resources in the selected license scope regardless of the regions where these resources are on-boarded.

> [!IMPORTANT]
> - Multiple license resources can be associated with the same scope or overlapping scopes. For example, a new license can be added when additional physical servers are deployed during temporary bursts of activity, or to reflect unexpected growth. 
> - All the virtual machines running on these physical servers must be connected to Azure Arc in the scope of the license resource, and have the `UsePhysicalEsuCoreLicense` Host configuration property set to `True`. For more details, see [Use physical core ESU license](manage-configuration.md#use-physical-core-esu-license).

The **License Activation** tab controls when the license takes effect or deactivated. The license can be activated during creation, or created first and then activated at a later time. The delayed activation allows you to coordinate it with other events in the licensing lifecycle, such as the expiration of an existing Enterprise Agreement. The `activatedAt` and `deactivatedAt` timestamp properties show when the license was last activated and deactivated. For more details, see [Change SQL Server license properties](manage-configuration.md#change-license-resource).


> [!IMPORTANT]
> - After the license is activated the core size and version of the license cannot be changed.  If you want to increase the core count, you will need to create another license resource.
> - After the license is terminated it cannot be re-activated. You can delete the resource if not needed.  
> - After the license is terminated, the ESU subscriptions for the VMs in scope will remain active and will become billable at the VM level. To stop all ESU charges, make sure to unsubscribe them before terminating the license. For details, see [Manage resources in scope](manage-configuration.md#manage-esu-license-resources).

The **Tenant ID** property is automatically set when the tenant scope is selected. 

For more information about licensing by physical cores with unlimited virtualization, see section *Licensing for maximum virtualization* in [SQL Server licensing guide](https://download.microsoft.com/download/e/2/9/e29a9331-965d-4faa-bd2e-7c1db7cd8348/SQL_Server_2019_Licensing_guide.pdf).

## <a id="non-production-esu-licensing"></a> Managing SQL Server USE subscriptions for non-production use

If you enabled ESU subscription in your production environment managed through Azure Arc, you can enable SQL Server ESU subscription in the non-production environment for free. There are two ways you can take advantage of this benefit.

### Using SQL Server Developer Edition

SQL Server Developer Edition itself is free and can be used in any Azure subscription. If you enable the ESU subscription on the VM hosting a Developer Edition, The Azure extension for SQL Server detects it and reports the usage via a $0 *Dev edition* meter. It does not generate the ESU charges. At the same time, it installs the ESUs when released as long as the ESU subscription is active. The Developer Edition has the same feature set as Enterprise Edition.

> [!IMPORTANT]
> If the Developer Edition is co-located on the same host with a Standard or Enterprise edition instance, the latter will take billing precedence as a production edition and the active ESU subscription with generate the ESU charges according to that edition.

### Using Azure dev/test subscription

If you configure your non-production environment as a mirror of the production environment, and want to use the same editions as in production, you must onboard the hosting machines and the SQL Server instances to an Azure dev/test subscription. The production SQL Server meters are enabled to support the dev/test subscriptions and are automatically nullified when emitted from a dev/test subscription. The same applies to the ESU meters. So it's safe to enable ESU subscription on these machines even if they run the Standard or Enterprise Editions. For information on how to create a dev/test subscription on Azure, see [Create an EA subscription](/azure/cost-management-billing/manage/create-enterprise-subscription#create-an-ea-subscription).

For more information, see section *Licensing SQL Server for non-production use* in [SQL Server licensing guide](https://download.microsoft.com/download/e/2/9/e29a9331-965d-4faa-bd2e-7c1db7cd8348/SQL_Server_2019_Licensing_guide.pdf).

## Managing SQL Server ESU subscriptions on high availability and disaster recovery replicas.

If your out-of-service SQL Server instance is a passive replica created as part of your high availability or disaster recovery configuration, you are entitled to the failover benefits that are included if your **License type** is set to `Paid` or `PAYG`. For more information about the failover benefits, see section *Licensing SQL Server for high availability and disaster recovery* in [SQL Server licensing guide](https://download.microsoft.com/download/e/2/9/e29a9331-965d-4faa-bd2e-7c1db7cd8348/SQL_Server_2019_Licensing_guide.pdf).

To help you manage the failover benefits and remain compliant, Azure extension for SQL Server automatically detects the passive instances and reflects the use of the SQL Server software by emitting special $0 disaster recovery (DR) meters, as long as you properly configured the **license type** setting. For more details, see [Metering software usage](manage-license-billing.md#usage-metering).

> [!NOTE]
> During the failovers, the extension is aware of the transition and automatically switches the ESU billing to the active replica without new bill-back charges. 

## <a id="server-cal"></a> Managing SQL Server instances with a Server+CAL license

You can connect any licensed SQL Server instance to Azure Arc, including the ones that are licensed with the Server+CAL licensing model. However the ESU subscription enabled by Azure Arc is not available for out-of-support the Server+CAL licensing model. If you wish to receive ESUs, you can set the license type to `PAYG` and then enable the ESU subscription.

## <a id="license-transition"></a> Managing transition from p-core ESU license to v-core ESU license

Because the p-core ESU license is billed with an Enterprise edition ESU meter, it is cost-effective when the out-of-support SQL Server instances are co-located on a set of designated physical hosts. As you upgrade the individual instances or migrate them to Azure, you may lose the cost-effectiveness of the p-core ESU license and using the v-core ESU licensing may become more attractive. You have an option to terminate the p-core ESU license and switch to billing the individual VMs for the ESUs subscriptions. To properly manage this transition, use the following best practices:

1. Ensure that the VMs with the out-of-support SQL Server instances are connected to Azure Arc and configured to use the p-core ESU license after the p-core ESU license is activated. See [Use physical core ESU license](manage-configuration.md#use-physical-core-esu-license).

1. Continuously evaluate the cost benefits of using the p-core ESU license.  
1. Terminate the p-core ESU license if it is no longer financially beneficial but keep the ESU subscription active on the individual VMs.

> [!IMPORTANT]
> 1. If the VMs in scope have been configured to use a ESU subscription *while the p-core ESU license was active* (as described in step 1), after the p-core license termination they will automatically switch to billing for ESU based on the installed SQL Server edition and the v-core count of each VM. There will no additional bill-back charges.  
> 1. If the VM is configured to use the ESU subscription *after the p-core ESU license was terminated*, it will be treated as a new subscription and the appropriate bill-back chargess will apply.   

## <a id="esu-usage-metering"></a> Metering ESU usage

The usage of the SQL Server ESU subscription is reported once an hour. The specific meter is automatically selected based on the SQL Server edition and the number v-cores or p-cores visible to the Operating System Environment (OSE). The following rules apply:

- If you install one or several SQL Server instances on a virtual machine and don't specify that a physical core ESU license should be used, SQL Server ESU subscription usage is metered based on the total number of virtual cores available to OSE, with a minimum of four cores per OSE.

- If you install one or several SQL Server instances on a physical server without using virtual machines,  SQL Server ESU subscription usage is metered based on the total number physical cores available to the OSE, with a minimum of four cores per OSE.

- SQL Server ESU subscription usage is reported per OSE whether one or multiple SQL Server instances are installed on the same OSE. 

- If multiple out-of-support SQL Server instances of the same version are installed on the same OSE, the highest SQL Server edition defines the ESU subscription meter that will be sent every hour.

- If multiple out-of-support instances of both version 2012 and 2014 are installed on the same OSE, with the same or different editions, one instance of the same version is billed separately as they have different prices and bill-back periods.

The next table shows the ESU subscription meters that are used for metering and billing for SQL Server ESU subscription on a single OSE:

| Projected edition<sup>1</sup> | SQL Server Version  | Failover replica | Use p-core license | Meter SKU |
|--|--|--|--|--|
| Enterprise | 2012 | No | No | `Ent edition - ESU` <br/> `Ent edition - ESU back billing` |
| Enterprise | 2012 | No | Yes | None |
| Enterprise | 2012 | Yes | Yes or No | None |
| Enterprise | 2014 | No | No |`Ent edition - ESU 2014` <br/>`Ent edition - ESU 2014 back billing` |
| Enterprise | 2014 | No | Yes | None |
| Enterprise | 2014 | Yes | Yes or No | None |
| Standard | 2012 | No | No | `Std edition - ESU` <br/> `Std edition - ESU back billing` |
| Standard | 2012 | No | Yes | None |
| Standard | 2012 | Yes | Yes or No | None |
| Standard | 2014 | No | No | `Std edition - ESU 2014` <br/> `Std edition - ESU 2014 back billing` |
| Standard | 2014 | No | Yes | None |
| Standard | 2014 | Yes | Yes or No | None |
| Evaluation | Any | Yes or No | Yes or No | None |
| Developer | Any | Yes or No | Yes or No | None |
| Web | Any | n/a | Yes or No | None |
| Express | Any | n/a | Yes or No | None |

<sup>1</sup> For edition projection rules, see [Metering software usage](manage-license-billing.md#usage-metering). 

The next table shows the meter SKUs that are used for metering and billing for active Physical core ESU licenses:

| Azure resource | SQL Server Version | Meter SKU |
|--|--|--|
| P-core ESU license | 2012 | `Ent edition - ESU` <br/> `Ent edition - ESU back billing`|
| P-core ESU license | 2014 | `Ent edition - ESU 2014` <br/> `Ent edition - ESU 2014 back billing`|

## Understand ESU subscription billing

The ESU subscription extends support for critical updates for up to three more years. If you start the subscription after the end of support date, you must purchase the volume licensing offer or ESU subscription to cover any previous years. With ESU subscriptions, you have the additional benefit of canceling the subscription and all future charges without penalty at any time.

### Billing for SQL Server 2012 ESUs

Because the ESU subscription option was introduced in Year 2 of the [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] extended support period, you must have purchased the Year 1 Volume Licensing ESU offer, before signing up for the ESU subscription in Year 2. You can sign up for the ESU subscription at any time within year 2, and your bill reflects the cost of continuous ESU coverage. After you sign up for the ESU subscription, your next monthly bill includes a one-time billback charge for each machine hosting a [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] instance or instances with an active ESU subscription, from July 12, 2023, to the date of activation.

From this point, you're billed for each machine on an hourly basis. Both billback and regular hourly charges use the hourly rate *(core count) x (100% of year 2 ESU license price) / 730*. So, the size of the billback charge depends on how much time has passed since July 12, 2023, until the activation time. The following billing rules apply:

- If you install a [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] instance or instances on a virtual machine, and don't use the unlimited virtualization benefit, you're billed for the total number of virtual cores of the machine, with a minimum of four cores. If the virtual machine is eligible to receive failover rights, the virtual cores of that machine aren't billable.

- If you install a [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] instance or instances on a physical server without using virtual machines, you're billed for all physical cores of the machine, with a minimum of four cores. If the physical server is eligible to receive failover rights (subject to the SQL Server - Failover rights clause), the physical cores of that server aren't billable. For more information, see the [product terms](https://www.microsoft.com/licensing/terms/productoffering/MicrosoftAzure/eaeas#ServiceSpecificTerms).

For more information about [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] ESU pricing, see [Plan your Windows Server 2012/2012 R2 and SQL Server 2012 end-of-support](https://www.microsoft.com/windows-server/extended-security-updates).

### Billing for SQL Server 2014 ESUs

The ESU subscription for [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] is available from year 1 of the extended support period, which starts on July 10, 2024. You can sign up for it at any time before or after that date. If you sign up before that date, you only see the hourly ESU charges starting at midnight on July 10, 2024. If you sign up after July 10, 2024, your next month's bill includes a billback charge from July 10, 2024, to the date of activation. The following billing rules apply:

- If you install a [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] instance or instances on a virtual machine, and don't use the unlimited virtualization benefit, you're billed for the total number of virtual cores of the machine, with a minimum of four cores. If the virtual machine is eligible to receive failover rights, the virtual cores of that machine aren't billable.

- If you install a [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] instance or instances on a physical server without using virtual machines, you're billed for all physical cores of the machine, with a minimum of four cores. If the physical server is eligible to receive failover rights (subject to the SQL Server - Failover rights clause), the physical cores of that server aren't billable. For more information, see the [product terms](https://www.microsoft.com/licensing/terms/productoffering/MicrosoftAzure/eaeas#ServiceSpecificTerms).

- If you install both instances of [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] on the same physical or virtual machine, you're billed for the total number of physical or virtual cores of the machine, for both [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] ESU, with a minimum of four cores. The billing for each version is based on the ESU price for that version. If the virtual machine is eligible to receive failover rights, the virtual cores of that machine aren't billable.

For more information about [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] ESU pricing, see [Plan your Windows Server and SQL Server end of support](https://www.microsoft.com/windows-server/extended-security-updates).

### Billing during the connectivity loss and other disruptions

If your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance loses connectivity, the billing stops, and the subscription is suspended. To make sure that intermittent disconnection doesn't negatively affect your ESU coverage, we automatically reactivate it if the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance reconnects within 30 days, without penalty. In that case, you see an additional billback charge for the days since the last day your server was connected. If you manually terminate the ESU subscription, and then reactivate it within 30 days, there's also no penalty. Your bill includes an additional charge for the time since you canceled the subscription. If the server reconnects after 30 days of disconnection, the subscription is terminated. To resume the ESU coverage, you need to activate a new ESU subscription and pay all the associated billback charges.

> [!IMPORTANT]  
> The billback charges are recorded within the first hour of the ESU subscription and look like single hourly charges for the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances that have the ESU subscriptions enabled. Because the amount reflects the accumulated costs since July 11, 2023 for [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)], or July 10, 2024 for [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)], it's much higher than the regular hourly ESU charges. This is expected, and it should be a one-time charge. During the following months you should only see the regular hourly charges. Additional billback charges could be added in cases of connectivity disruptions, but they are typically much smaller amounts.

## Next steps

- [Review the Extended Security Updates: Frequently asked questions](../end-of-support/extended-security-updates-frequently-asked-questions.md).
- [Learn about the prerequisites to connect your SQL Server to Azure Arc](prerequisites.md)
- [Automatically connect your SQL Server to Azure Arc](automatically-connect.md)

## Related content

- [Product terms for SQL Server enabled by Azure Arc](https://www.microsoft.com/licensing/terms/productoffering/MicrosoftAzure/eaeas#ServiceSpecificTerms).
- [SQL Server licensing guide](https://download.microsoft.com/download/e/2/9/e29a9331-965d-4faa-bd2e-7c1db7cd8348/SQL_Server_2019_Licensing_guide.pdf).
- [Review SQL Server 2022 Pricing](https://www.microsoft.com/sql-server/sql-server-2022-pricing)
- [Manage SQL configuration](manage-configuration.md)
- [Frequently asked questions](faq.yml#billing)

