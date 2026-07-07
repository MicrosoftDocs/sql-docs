---
title: Extended Security Updates
description: Learn how to manage licensing and billing of Extended Security Updates for SQL Server.
author: MashaMSFT
ms.author: sashan
ms.reviewer: randolphwest, maghan
ms.date: 06/22/2026
ai-usage: ai-assisted
ms.topic: how-to
ms.custom:
  - references_regions
  - ignite-2025
---

# SQL Server Extended Security Updates enabled by Azure Arc

[!INCLUDE [sql-migration-end-of-support](../../includes/applies-to-version/sql-migration-end-of-support.md)]

This article explains how to manage a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] subscription to Extended Security Updates enabled by Azure Arc. For more information about the program, see [What are Extended Security Updates for SQL Server?](../end-of-support/sql-server-extended-security-updates.md)

> [!IMPORTANT]
> ESU options and related Azure Arc capabilities can vary by cloud and region. For Azure Government-specific availability and limitations, review [SQL Server enabled by Azure Arc in US Virginia Government](us-government-region.md).

After [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] reaches the end of its support lifecycle, you can sign up for an Extended Security Update (ESU) subscription for your servers and remain protected for up to three years. When you upgrade to a newer version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], you can terminate your ESU subscription and stop paying for it. When you [migrate to Azure SQL](/azure/azure-sql/migration-guides/), the ESU charges automatically stop but you continue to have access to the security updates.

[!INCLUDE [2016-esu](../../includes/2016-esu.md)]

## Enable ESUs for SQL Server instances

[!INCLUDE [esu-enable-sql-server-instances](../../includes/esu-enable-sql-server-instances.md)]

## Licenses for ESUs

You can use one of the following three options to subscribe to ESUs in a production environment. The links in the list take you to sections in this article that provide more details.

The diagrams in the list use normalized cores (NCs) to illustrate the cost implications of the licensing options. One core license for the Standard edition is equivalent to one NC. One core license for the Enterprise edition is equivalent to four NCs. For more information, see [How licenses apply to Azure resources](/azure/cost-management-billing/scope-level/overview-azure-hybrid-benefit-scope#how-licenses-apply-to-azure-resources).

- [License by virtual cores](#license-esu-vcores)

  Use an Enterprise or Standard ESU subscription for the vCPUs (virtual cores) of the virtual machine (VM) that runs one or multiple instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Each virtual machine is billed individually for the virtual cores allocated to it.

  The following diagram illustrates this licensing method and the cost implications.

  :::image type="content" source="media/extended-security-updates/virtual-core-licensing.svg" alt-text="Diagram that illustrates the virtual core licensing option.":::

- [License by physical cores without virtual machines](#license-pcores-without-vms)

  Use an Enterprise or Standard license for the physical cores of the host that runs one or multiple instances of SQL Server installed directly on the host without using VMs. Each instance has access to all physical cores supported by the installed edition limits, up to all physical cores of the host. Regardless of the instance limits, the host is billed for all the physical cores based on the highest [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] edition installed on it. For details, see [Compute capacity limits by edition of SQL Server](../compute-capacity-limits-by-edition-of-sql-server.md).

  The following diagram illustrates the cost implications of deploying two Standard instances on a physical host without using VMs.

  :::image type="content" source="media/extended-security-updates/physical-core-licensing-without-vms.svg" alt-text="Diagram that illustrates physical core licensing without using virtual machines.":::

- [License by physical cores with unlimited virtualization](#unlimited-virtualization)

  Use an Enterprise ESU subscription for the physical cores of the host that runs any number of virtual machines with any number of out-of-support [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances. A single physical core license is a separate Azure resource that represents all physical cores licensed for ESUs and is billed independently.

  The following diagram illustrates the cost implications of licensing a physical host and using unlimited virtualization.

  :::image type="content" source="media/extended-security-updates/physical-core-licensing-with-vms.svg" alt-text="Diagram that illustrates physical core licensing with unlimited virtualization.":::

To subscribe to ESUs, you must have active Software Assurance or enable a pay-as-you-go billing for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] software. The following table shows ESU availability for each license type that you use for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] software:

| SQL Server license type | ESU virtual core subscription | ESU physical core subscription without VMs | ESU physical core subscription with unlimited virtualization |
| --- | --- | --- | --- |
| Subscribe to the service through Microsoft Azure by using a pay-as-you-go method | Yes | Yes | Yes |
| Bring your own license with Software Assurance or a SQL Server subscription <sup>1</sup> | Yes | Yes | Yes |
| Bring your own license without Software Assurance <sup>2</sup> | No | No | No |

<sup>1</sup> You already have a license with active Software Assurance or an active SQL Server subscription.

<sup>2</sup> You own a perpetual license or use a Server+CAL license.

Your choice of payment option might affect your outsourcing options. For more information, see the [service-specific terms](https://www.microsoft.com/licensing/terms/productoffering/MicrosoftAzure/eaeas#ServiceSpecificTerms) and the [Flexible Virtualization Benefit licensing guide](https://www.microsoft.com/licensing/docs/view/Virtualization).

For information about licensing your nonproduction out-of-support [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances for ESUs through Azure Arc, see [Manage SQL Server ESU subscriptions for nonproduction use](extended-security-updates.md#non-production-esu-licensing) later in this article.

<a id="license-esu-vcores"></a>

## Subscribe to SQL Server ESUs by virtual cores

When you subscribe to ESUs by virtual cores, you can limit the scope of the subscription to a specific virtual machine and one or more out-of-support [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances installed on the operating system environment (OSE) of that machine. This subscription model is optimized for the following scenarios:

- Your out-of-service [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] VMs are mixed with the VMs running other software on the same physical servers.
- You run your out-of-service [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] VMs in a hosted environment or in a non-Microsoft cloud where you don't control the physical infrastructure.

Use the [SQL Server configuration](manage-configuration.md) areas of the Azure portal to manage a virtual core ESU subscription for each VM. The **Overview** pane for each Azure Arc-enabled [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] resource shows the ESU subscription status under **SQL Server configuration**.

The ESU subscription for the Standard edition is limited to a maximum of 24 virtual cores, even if the OSE is configured with more virtual cores. For more information about limits, see [Compute capacity limits by edition of SQL Server](../compute-capacity-limits-by-edition-of-sql-server.md).

<a id="license-pcores-without-vms"></a>

## Subscribe to SQL Server ESUs by physical cores without using VMs

The option of subscribing to SQL Server ESUs by physical cores without using VMs is optimized for the following scenarios:

- You control your physical environment.
- Your out-of-support [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances are installed directly on a physical server to maximize the performance of your database application.
- You aren't running virtual machines on that physical server.

In this option, the ESU subscription requirements are similar to [subscribing to SQL Server ESUs by virtual cores](#license-esu-vcores). You manage the ESU subscription for each host by using the [SQL Server configuration](manage-configuration.md) areas of the Azure portal. The main difference is that the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] software usage is reported based on the physical cores available to the OSE of that server. For details, see [Metering software usage](extended-security-updates.md#esu-usage-metering).

The ESU subscription for the Standard edition is limited to a maximum of 24 physical cores, even if the OSE is installed on a larger machine. For more information about limits, see [Compute capacity limits by edition of SQL Server](../compute-capacity-limits-by-edition-of-sql-server.md).

> [!IMPORTANT]  
> If you connect a physical machine without VMs to Azure Arc in the scope that a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] ESU physical core license covers, the unlimited virtualization benefit doesn't apply to that machine. It's billed individually, based on the physical cores that the OSE can access.

<a id="unlimited-virtualization"></a>

## Subscribe to SQL Server ESUs by physical cores with unlimited virtualization

The option of subscribing to SQL Server ESUs by physical cores with unlimited virtualization is most effective when:

- You control your physical environment and run the out-of-support [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances on different VMs for security isolation and better resource management.
- Your infrastructure and the selected payment method support the unlimited virtualization benefit for ESU.
- Subscribing to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] ESUs by virtual cores is more expensive than subscribing by the physical cores of the host.

To use the unlimited virtualization benefit, you need to create a SQL Server ESU License (*SqlServerEsuLicenses*) resource that represents one or more physical hosts. The covered SQL Server instances must be connected to Azure Arc and configured to use the physical core ESU license. For details about managing *SqlServerEsuLicenses* resources, see [Manage the unlimited virtualization benefit for a SQL Server ESU subscription](manage-configuration.md#manage-pcore-esu-license).

<a id="esu-license-resource"></a>

To create the SQL Server ESU License resource in the Azure portal, go to the [ESU - SQL Server](https://portal.azure.com/#servicemenu/Microsoft_Azure_ArcCenterUX/AzureArcCenterHub/sqlServerEsuLicenses) page and use **Create** to create your resource:

:::image type="content" source="media/extended-security-updates/esu-license-resource.png" alt-text="Screenshot of the ESU license resource page in the Azure portal.":::

> [!CAUTION]  
> The unlimited virtualization benefit isn't available to VMs running on infrastructure from any of the [listed providers](https://aka.ms/listedproviders). You can license these VMs only by virtual cores. If you create a *SqlServerEsuLicenses* resource with the intent of licensing these VMs by using unlimited virtualization, you pay for the consumption of virtual cores based on the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] configuration of the host. Any existing physical core licenses don't apply to offset such charges.

For more information about licensing by physical cores with unlimited virtualization, see the section "Licensing for maximum virtualization" in the [SQL Server licensing guide (download link)](https://download.microsoft.com/download/e/2/9/e29a9331-965d-4faa-bd2e-7c1db7cd8348/SQL_Server_2019_Licensing_guide.pdf).

A single *SqlServerEsuLicenses* resource can cover multiple virtual machines connected to Azure Arc. It includes several properties that define how the license is applied and billed.

### License details

The **License details** tab includes the standard Azure properties and the ESU license-specific settings:

- The `scopeType` property sets the Azure scope in which the license covers all qualified *Machine - Azure Arc* resources. The following Azure scopes are supported:

  - Azure tenant (`Tenant`)
  - Azure subscription (`Subscription`)
  - Resource group (`ResourceGroup`)

  The specific scope is derived from the location of the license resource. For example, if you select `Subscription`, the subscription ID that hosts the license resource is used as the scope.

- The `billingPlan` property is set to pay-as-you-go billing automatically, because an ESU subscription is always billed on an hourly meter.

- The `physicalCores` property of the license resource represents the sum of physical cores of the servers in the selected scope to which the license is applied. The minimum size of the license is 16 physical cores.

- The `TenantID` property is automatically set when the tenant scope is selected.

You can create the license resource in a resource group in any of the [supported regions](overview.md#supported-azure-regions). The location of the resource is set to the location of the selected resource group by default, but you can change it to a different region.

The location of the license resource doesn't affect the scope. It applies to all *Machine - Azure Arc* resources in the selected license scope, regardless of the regions where these resources are onboarded. You can associate multiple license resources with the same scope or overlapping scopes. For example, you can add a new license when you deploy additional physical servers for the increased demand.

> [!IMPORTANT]  
> When using the unlimited virtualization benefit, make sure that
>
> 1. All the virtual machines on the licensed physical servers are connected to Azure Arc.
> 1. They are in the scope specified in the license. For example, they are in the same subscription or resource group.
> 1. They have the `UsePhysicalEsuCoreLicense` host configuration property set to `True`. For more information, review [Use a physical core ESU license](manage-configuration.md#use-physical-core-esu-license).

### License activation

You use the **License Activation** tab to control when the license takes effect or is deactivated. You can activate the license during creation, or you can create the license first and then activate it later. Delaying the activation allows you to coordinate it with other events in the licensing life cycle, such as the expiration of an existing Enterprise Agreement. The `activatedAt` and `deactivatedAt` time-stamp properties show when the license was last activated and deactivated. For more information, see [Update a SQL Server license resource](manage-configuration.md#change-license-resource).

After the license is activated:

- You can't change the license version.
- You can decrease the core count, but you can't increase it. To increase the core count, create another license resource.
- You can't change the license scope.

After the license is terminated:

- You can't reactivate the license. You can delete the resource if you don't need it.
- The ESU subscriptions for the VMs in scope remain active and become billable at the VM level.

To stop all ESU charges, unsubscribe from ESUs on all virtual machines before you terminate the license. For details, review [Manage resources in the scope of an ESU physical core license](manage-configuration.md#manage-esu-license-resources).

<a id="non-production-esu-licensing"></a>

## Manage SQL Server ESU subscriptions for nonproduction use

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

<a id="manage-hadr"></a>

## Manage SQL Server ESU subscriptions on high-availability and disaster recovery replicas

[!INCLUDE [manage-passive-instance](includes/manage-passive-instance.md)]

The ESU subscription enabled on a connected server with passive SQL Server instances doesn't incur the ESU charges. This way you can guarantee that future ESUs will be applied to that server. To qualify, all SQL Server instances on this server must meet the passivity criteria defined in [Manage passive license for high availability and disaster recovery](manage-license-billing.md#free-dr).

### ESU billing after failover

During the failovers, the extension is aware of the transition and automatically switches the ESU billing to the active replica without new bill-back charges.

<a id="server-cal"></a>

## Manage SQL Server instances that use a Server+CAL license

You can connect any licensed [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance to Azure Arc, including instances that use the Server+CAL licensing model. However, the ESU subscription enabled by Azure Arc isn't available for the out-of-support Server+CAL licensing model. If you want to receive ESUs, you can set the license type to `PAYG` and then enable the ESU subscription.

<a id="esu-subscription-ssxs"></a>

## Manage SQL Server ESU subscriptions for the associated services

SQL Server ESU subscriptions support the following associated services:

[!INCLUDE [sql-server-associated-services](includes/sql-server-associated-services.md)]

For details, see [Feature availability by service type](overview.md#feature-availability-by-service-type).

The SQL Server associated services are billed for ESU using the regular ESU meters. For details, see [Understand ESU usage meters](extended-security-updates.md#esu-usage-metering).

> [!IMPORTANT]  
>
> - The SQL Server associated service installations are billed for the ESU subscription only when they are installed on the machine as a standalone instance (without SQL Server engine). Otherwise, the SQL Server engine instance is billed.
>
> - If you activate a physical core ESU license for the corresponding scope and configure the machine to use it, the SQL Server associated service isn't individually billed for ESU even if it's a standalone instance (without SQL Server engine). For details, see [Manage resources in the scope of an ESU physical core license](manage-configuration.md#manage-esu-license-resources).
>

<a id="license-transition"></a>

## Manage the transition from a physical core ESU license to a virtual core ESU license

Because the physical core ESU license uses an ESU meter for the Enterprise edition, it offers cost savings when you colocate the out-of-support [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances on a set of designated physical hosts. As you upgrade the individual instances or migrate them to Azure, you might lose the cost-effectiveness of the physical core ESU license. Using the virtual core ESU licensing might then become more attractive. You can terminate the physical core ESU license and switch to billing the individual VMs for the ESU subscriptions.

To properly manage this transition, use the following sequence of best practices:

1. Ensure that the VMs with the out-of-support [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances are connected to Azure Arc and configured to [use the physical core ESU license](manage-configuration.md#use-physical-core-esu-license) after that license activates.

1. Continuously evaluate the cost benefits of using the physical core ESU license.

1. Terminate the physical core ESU license if it's no longer financially beneficial, but keep the ESU subscription active on the individual VMs.

> [!IMPORTANT]  
> If you configure the VMs in scope to use an ESU subscription *while the physical core ESU license is active* (as described in step 1), after the physical core license termination, they automatically switch to billing for ESU based on the installed [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] edition and the virtual core count of each VM. There are no additional bill-back charges.  
>
> If you configure the VM to use the ESU subscription *after the physical core ESU license is terminated*, it's treated as a new subscription and the appropriate bill-back charges apply.

<a id="vl-sku-transition"></a>

## Transition from a Volume Licensing ESU license to an ESU subscription

When you enable an ESU subscription, Azure billing begins at the start of the current ESU year. To transition without additional action, you must already have ESU coverage for all prior ESU years through Volume Licensing. When this condition is met, the transition occurs automatically.

If you do not have ESU coverage for previous years through Volume Licensing, you must obtain it to remain compliant. If instead you want to pay for prior ESU years through Azure billing, additional configuration is required.

For guidance on enabling Azure-based billing for prior ESU years, contact your Microsoft account team or open a support request using the following path:

**Azure → SQL Server enabled by Azure Arc → Licensing, Billing, or Disconnected Registration Issues → SQL Server (ESU versions only)**

<a id="esu-usage-metering"></a>

## Understand ESU usage meters

The usage of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] ESU subscription is reported once an hour. The specific meter is automatically selected based on edition and the version eligible for ESU. The ESU usage is reported for all virtual cores or physical cores visible to the OSE. The following rules apply:

- If you install one or more instances of SQL Server or SQL Server associated services that are eligible for ESU on a virtual machine and don't specify the use of a physical core ESU license, the ESU subscription usage is metered based on the total number of virtual cores available to the OSE. The minimum is four virtual cores per OSE.

- If several instances of SQL Server or SQL Server associated services eligible for ESU are installed on a physical server without using virtual machines, the ESU subscription usage is metered based on the total number physical cores available to the OSE. The minimum is four cores per OSE.

- If multiple instances of SQL Server or SQL Server associated services are installed with the same version that is eligible for ESU, only one ESU subscription usage is reported per OSE. The reported usage is associated with the instance that has the highest edition.

- If two or more instances of SQL Server or SQL Server associated services are installed with different versions that are eligible for ESU, each eligible version will report ESU usage separately based on the instance of that version with the highest edition. This reflects the differences in ESU prices and bill-back periods for different versions.

The following tables show the ESU subscription meters (also called *SKUs*) that are used for metering and billing for a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] ESU subscription on a single OSE:

### [SQL Server 2016](#tab/sql2016)

| Projected edition <sup>1</sup> | Failover replica | Use physical core license | Meter SKU |
| --- | --- | --- | --- |
| Enterprise | No | No | `Ent edition - ESU 2016`<br />`Ent edition - ESU 2016 back billing` |
| Enterprise | No | Yes | None |
| Enterprise | Yes | Yes or no | None |
| Standard | No | No | `Std edition - ESU 2016`<br />`Std edition - ESU 2016 back billing` |
| Standard | No | Yes | None |
| Standard | Yes | Yes or no | None |
| Evaluation | Yes or no | Yes or no | None |
| Developer | Yes or no | Yes or no | None |
| Web <sup>2</sup> | Not applicable | Yes or no | None |
| Express | Not applicable | Yes or no | None |

### [SQL Server 2014](#tab/sql2014)

| Projected edition <sup>1</sup> | Failover replica | Use physical core license | Meter SKU |
| --- | --- | --- | --- |
| Enterprise | No | No | `Ent edition - ESU 2014`<br />`Ent edition - ESU 2014 back billing` |
| Enterprise | No | Yes | None |
| Enterprise | Yes | Yes or no | None |
| Standard | No | No | `Std edition - ESU 2014`<br />`Std edition - ESU 2014 back billing` |
| Standard | No | Yes | None |
| Standard | Yes | Yes or no | None |
| Evaluation | Yes or no | Yes or no | None |
| Developer | Yes or no | Yes or no | None |
| Web <sup>2</sup> | Not applicable | Yes or no | None |
| Express | Not applicable | Yes or no | None |

### [All other versions](#tab/other)

| Projected edition <sup>1</sup> | Failover replica | Use physical core license | Meter SKU |
| --- | --- | --- | --- |
| Evaluation | Yes or no | Yes or no | None |
| Developer | Yes or no | Yes or no | None |
| Web <sup>2</sup> | Not applicable | Yes or no | None |
| Express | Not applicable | Yes or no | None |

---

<sup>1</sup> For edition projection rules, see [Metering software usage](manage-license-billing.md#usage-metering).

<sup>2</sup> Web edition isn't available in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions.

The next table shows the meter SKUs that are used for metering and billing for active physical core ESU licenses:

| Azure resource | SQL Server version | Meter SKU |
| --- | --- | --- |
| Physical core ESU license | 2016 | `Ent edition - ESU 2016`<br />`Ent edition - ESU 2016 back billing` |
| Physical core ESU license | 2014 | `Ent edition - ESU 2014`<br />`Ent edition - ESU 2014 back billing` |

<a id="2012-esu-billing"></a>

<a id="2014-esu-billing"></a>

## Understand ESU subscription billing

The ESU subscription extends support for critical updates for up to three years. If you start the subscription after the end-of-support date, you must purchase the Volume Licensing offer or ESU subscription to cover any previous years. With an ESU subscription, you have the additional benefit of canceling the subscription and all future charges without penalty at any time.

The ESU subscription is available from Year 1 of the extended support period. If you sign up before ESU updates are available, you see only the hourly ESU charges starting at midnight the day extended support starts. If you sign up after ESU support starts, your next month's bill includes a bill-back charge from the beginning of the current ESU year, based on the timestamp when ESU was enabled, or when physical core ESU license got activated.

The following billing rules apply:

- If you install a SQL Server instance or instances on a virtual machine, and you don't use the unlimited virtualization benefit, you're billed for the total number of virtual cores of the machine, with a minimum of four cores. If the virtual machine is eligible for the free HA passive replica benefit, the virtual cores of that machine aren't billable.

- If you install a SQL Server instance or instances on a physical server without using virtual machines, you're billed for all physical cores of the machine, with a minimum of four cores. If the physical server is eligible for the free HA passive replica benefit, the physical cores of that server aren't billable. For more information, see the [service-specific terms](https://www.microsoft.com/licensing/terms/productoffering/MicrosoftAzure/eaeas#ServiceSpecificTerms).

- If you install two or more SQL Server instances on the same physical or virtual machine, you're billed for the total number of physical or virtual cores of the machine, for each version's ESU subscription. The minimum is four cores. The billing for each version is based on the ESU price for that version. If the virtual machine is eligible for the free HA passive replica benefit, the virtual cores of that machine aren't billable.

### Billing for SQL Server 2016 ESUs

The ESU subscription for [!INCLUDE [ssSQL16](../../includes/sssql16-md.md)] is available from Year 1 of the extended support period, which starts on July 14, 2026. If you sign up before that date, you see only the hourly ESU charges starting at midnight on July 14, 2026. If you sign up after July 14, 2026, your next month's bill includes a bill-back charge from the beginning of the current ESU year, based on the timestamp when ESU was enabled, or when physical core ESU license got activated.

### Billing for SQL Server 2014 ESUs

The ESU subscription for [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] is available from Year 1 of the extended support period, which started on July 10, 2024. If you sign up before that date, you see only the hourly ESU charges starting at midnight on July 10, 2024. If you sign up after July 10, 2024, your next month's bill includes a bill-back charge from the beginning of the current ESU year, based on the timestamp when ESU was enabled, or when physical core ESU license got activated.

For more information about [!INCLUDE [ssSQL14](../../includes/sssql14-md.md)] ESU pricing, see [Azure Arc pricing](https://azure.microsoft.com/pricing/details/azure-arc/core-control-plane/).

<a id="esu-billing-during-connectivity-loss"></a>

### Billing during connectivity loss and other disruptions

If your Azure Extension for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] loses connectivity, the billing stops, and the subscription is suspended.

If you manually terminate the ESU subscription and then reactivate it, the ESU subscription resumes as long as your machine's connection to Azure Arc is in a healthy state. Your bill includes an additional charge for the time since you canceled the subscription.

If in the meantime the server permanently lost connectivity due to certificate expiration, the subscription is terminated. After the machine is reonboarded to Azure Arc, you must activate a new ESU subscription to resume coverage, and pay all the associated bill-back charges. For information about the server certificate lifecycle, review [Agent Status](/azure/azure-arc/servers/overview#agent-status).

If the Arc enabled machine goes offline and reconnects to Azure in a different subscription, in a different resource group, or with a different name, it will be treated as the same machine as long as the [Virtual Machine ID property](/azure/azure-arc/servers/agent-overview#instance-metadata) remains unchanged and the machine resource is in the same Azure location as the original machine resource.

ESU subscriptions are pinned to a specific Azure location. If the Arc enabled machine with an active ESU subscription is moved to a different Azure location, the subscription is terminated. To resume ESU coverage, you must activate a new ESU subscription and pay all the associated bill-back charges.

#### Scenarios that may result in VMID changes

Certain operational scenarios can result in a Virtual Machine ID change, which causes the system to treat the machine as a new resource and trigger bill-back charges. These scenarios include:

- **Renaming an on-premises Windows machine**: When you [rename an Azure Arc-enabled server resource](/azure/azure-arc/servers/manage-agent?tabs=windows#rename-an-azure-arc-enabled-server-resource), the VMID may change.

- **Moving a private link scope**: When moving a private link scope in Azure Arc, the agent may disconnect and require reconnection. If the agent is disconnected and re-onboarded, a new VMID is generated.

- **Certificate expiration after extended disconnection**: If the Azure Connected Machine agent remains offline for more than 45 days, the server certificate expires. To recover, you must fully re-onboard the machine to Azure. If the new resource URI matches the original, the ESU subscription isn't terminated. The bill-back charge is based on the time elapsed since the machine last connected to Azure Arc and reported usage. If the new resource URI differs from the original, a new ESU subscription is created, and a full bill-back charge is generated. For more information about the server certificate lifecycle, review [Agent Status](/azure/azure-arc/servers/overview#agent-status).

To avoid unexpected charges, monitor your Azure Arc-enabled machines for connectivity issues and address them promptly before certificates expire.

> [!IMPORTANT]
> The bill-back charge for the disconnected time is recorded within the first hour after the connectivity is restored, and is associated with the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance that is eligible for ESU coverage. The amount of the charge reflects the time since the previous heartbeat was registered.

## Related content

- [Product terms for SQL Server enabled by Azure Arc](https://www.microsoft.com/licensing/terms/productoffering/MicrosoftAzure/eaeas#ServiceSpecificTerms)
- [SQL Server licensing guide (download link)](https://download.microsoft.com/download/e/2/9/e29a9331-965d-4faa-bd2e-7c1db7cd8348/SQL_Server_2019_Licensing_guide.pdf)
- [SQL Server 2022 pricing and licensing](https://www.microsoft.com/sql-server/sql-server-2022-pricing)
- [Configure SQL Server enabled by Azure Arc](manage-configuration.md)
- [Frequently asked questions about billing](faq.yml#recurring-pay-as-you-go-billing)
- [Extended Security Updates: Frequently asked questions](../end-of-support/extended-security-updates-frequently-asked-questions.md)
- [Prerequisites - SQL Server enabled by Azure Arc](prerequisites.md)
- [Manage the unlimited virtualization benefit for a SQL Server ESU subscription](manage-configuration.md#manage-pcore-esu-license)
- [Microsoft.AzureArcData tag support](/azure/azure-resource-manager/management/tag-support#microsoftazurearcdata)
