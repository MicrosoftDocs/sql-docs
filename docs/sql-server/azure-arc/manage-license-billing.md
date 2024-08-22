---
title: Manage licensing and billing
description: This article explains how to manage SQL Server licensing options. It also demonstrates how SQL Server enabled by Azure Arc can be billed from Microsoft Azure. 
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 03/31/2024
ms.topic: conceptual
---

# Manage licensing and billing of SQL Server enabled by Azure Arc

This article explains how to manage licensing and billing of SQL Server enabled by Azure Arc. SQL Server enabled by Azure Arc directly supports only the core-based licensing methods. For information about how you can manage SQL Server instances with a Server+CAL license, see the section [Manage SQL Server instances with a Server+CAL license](manage-license-billing.md#server-cal) in this article. The full range of the licensing options is described in the [SQL Server licensing guide (download link)](https://go.microsoft.com/fwlink/p/?linkid=2215573).

## Licensing and billing in the production environment

You can use one of the following three licensing options. The links in the list take you to sections in this article that provide more details.

The diagrams in the list use normalized cores (NCs) to illustrate the cost implications of the licensing options. One core license for the Standard edition is equivalent to one NC. One core license for the Enterprise edition is equivalent to four NCs. For more information, see [How licenses apply to Azure resources](/azure/cost-management-billing/scope-level/overview-azure-hybrid-benefit-scope#how-licenses-apply-to-azure-resources).

- [License by virtual cores](#license-vcores)

  Use an Enterprise or Standard license for the vCPUs (v-cores) of the virtual machine (VM) that runs one or multiple instances of SQL Server. Each virtual machine is billed individually for the v-cores allocated to it.

  The following diagram illustrates this licensing method and the cost implications.

  :::image type="content" source="media/billing/virtual-core-licensing.svg" alt-text="Diagram that illustrates the virtual core licensing option.":::

- [License by physical cores (p-cores) without virtual machines](#license-pcores-without-vms)

  Use an Enterprise or Standard license for the p-cores of the host that runs one or multiple instances of SQL Server installed directly on the host without using VMs. Each instance has access to all p-cores supported by the installed edition limits, up to all p-cores of the host. Regardless of the instance limits, the host is billed for all the p-cores based on the highest SQL Server edition installed on it. For details, review [Compute capacity limits by edition of SQL Server](../compute-capacity-limits-by-edition-of-sql-server.md).

  The following diagram illustrates the cost implications of deploying two Standard instances on a physical host without using VMs.

  :::image type="content" source="media/billing/physical-core-licensing-without-vms.svg" alt-text="Diagram that illustrates physical core licensing without using virtual machines.":::

- [License by physical cores with unlimited virtualization](#unlimited-virtualization)

  Use an Enterprise license for the physical cores of the host that runs any number of virtual machines with any number of SQL Server instances. A single p-core license is a separate Azure resource that represents all licensed p-cores and is billed independently.

  The following diagram illustrates the cost implications of licensing a physical host and using unlimited virtualization.

  :::image type="content" source="media/billing/physical-core-licensing-with-vms.svg" alt-text="Diagram that illustrates physical core licensing with unlimited virtualization.":::

For each of these options, you have to decide how you want to pay for the license. The following table shows your payment options:

| Payment option | V-core licensing | P-core licensing without VMs | P-core licensing with unlimited virtualization |
|---|---|---|---|
| Subscribe to the service through Microsoft Azure by using a pay-as-you-go method | Yes | Yes | Yes |
| Bring your own license with Software Assurance or a SQL Server subscription<sup>1</sup> | Yes | Yes | Yes |
| Bring your own license without Software Assurance<sup>2</sup> | Yes | Yes | No |

<sup>1</sup> You already have a license with active Software Assurance or an active SQL Server subscription.

<sup>2</sup> You own a perpetual license or use a Server+CAL license.

Your choice of payment option might affect your outsourcing options. For more information, see the [service-specific terms](https://www.microsoft.com/licensing/terms/productoffering/MicrosoftAzure/eaeas#ServiceSpecificTerms) and the [Flexible Virtualization Benefit licensing guide (download link)](https://wwlpdocumentsearch.blob.core.windows.net/prodv2/Licensing_guide_PLT_Flexible_Virtualization_Benefit_Nov2022.pdf).

For information about licensing your non-production or test SQL Server instances through Azure Arc, see the [Manage SQL Server licensed for non-production use](manage-license-billing.md#non-production-licensing) section later in this article.

## <a id="license-vcores"></a> License SQL Server instances by virtual cores

Licensing SQL Server by v-cores allows you to limit the scope of the license to a specific virtual machine and one or more SQL Server instances installed on the operating system environment (OSE) of that machine. It's optimized for the following scenarios:

- Your SQL Server VMs are mixed with the VMs running other software on the same physical servers.
- You deploy your VMs to a hosting partner or a non-Microsoft cloud where you don't control your physical infrastructure.

You can choose v-core licensing to license both SQL Server software and SQL Server Extended Security Updates. You manage a v-core license independently for each VM by using the [SQL Server configuration](manage-configuration.md) areas of the Azure portal. The **Overview** pane for each Azure Arc-enabled SQL Server resource shows the configured license under **Host License Type**.

The Standard edition is limited to a maximum of 24 v-cores, even if the OSE is configured with more v-cores. For more information about limits, see [Compute capacity limits by edition of SQL Server](../compute-capacity-limits-by-edition-of-sql-server.md).

For more information about licensing SQL Server by virtual cores, see the section "Licensing individual virtual machines" in the [SQL Server licensing guide (download link)](https://go.microsoft.com/fwlink/p/?linkid=2215573).

### License types

The following license types are supported when you're licensing v-cores:

| License type | Description | Value |
|---|---|---|
| Pay-as-you-go | Subscribe to the Standard or Enterprise edition of the service and be billed on an hourly meter. See [SQL Server pricing and licensing](https://www.microsoft.com/sql-server/sql-server-2022-pricing). | `PAYG` |
| License with Software Assurance | Bring your own Standard or Enterprise license with Software Assurance or a SQL Server subscription. Your software usage is reported through a free hourly meter according to the metering rules. See [Metering software usage](#usage-metering) later in this article. | `Paid`|
| License only | You use a perpetual or Server+CAL license for the Standard or Enterprise edition, or you use the Developer, Evaluation, or Express edition. Your software usage is reported according to the metering rules. See [Metering software usage](#usage-metering) later in this article. | `LicenseOnly` |

#### Important considerations

- The pay-as-you-go subscription requires the hosting machine to be continuously connected to Azure.

  Intermittent connectivity disruptions for up to 30 days are tolerated with built-in resilience. After 30 days without a connection, the pay-as-you-go subscription expires. After your subscription expires, you aren't authorized to use the software.

- The pay-as-you-go hourly charges are issued only when SQL Server is running on the machine at any point within a particular hour, and if the machine is online.

- By selecting a license with Software Assurance, you attest that you have Enterprise or Standard licenses with active Software Assurance or an active SQL Server subscription license.

#### Available features

In addition to billing differences, the license type determines what features are available to your SQL Server instance.

[!INCLUDE [license-types](includes/license-types.md)]

> [!NOTE]
>
> - The license type is a required parameter when you install Azure Extension for SQL Server. Each supported onboarding method includes the license type options.
> - [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] allows you to select the license type during setup. See [Install SQL Server from the Installation Wizard](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md#azure-extension-for-sql-server-2022).

## <a id="license-pcores-without-vms"></a> License SQL Server instances by physical cores without using VMs

The option of licensing SQL Server by physical cores without using VMs is optimized for the following scenarios:

- You control your physical environment and install the SQL Server instances on a physical server to maximize the performance of your database application.
- Your SQL Server instance uses a license without Software Assurance.

In this option, the licensing requirements are identical to [licensing SQL Server by virtual cores](#license-vcores), but the SQL Server software usage is reported based on the physical cores available to the OSE of that server. For details, see [Metering software usage](#usage-metering) later in this article.

The Standard edition is limited to a maximum of 24 p-cores, even if the OSE is installed on a larger machine. For more information about limits, see [Compute capacity limits by edition of SQL Server](../compute-capacity-limits-by-edition-of-sql-server.md).

> [!IMPORTANT]
> If a physical machine without VMs is connected to Azure Arc in the scope that a SQL Server physical core license covers, the unlimited virtualization benefit doesn't apply to that machine. It's licensed and billed separately.

For more information about licensing SQL Server on a physical OSE, see the section "Core-based licensing" in the [SQL Server licensing guide (download link)](https://go.microsoft.com/fwlink/p/?linkid=2215573).

## <a id="unlimited-virtualization"></a> License SQL Server instances by physical cores with unlimited virtualization

The option of licensing SQL Server by physical cores with unlimited virtualization is most effective when:

- You control your physical environment and install the SQL Server instances on different VMs for security isolation and better resource management.
- Your infrastructure and the selected payment method support the unlimited virtualization benefit.
- Licensing your SQL Server instances by v-cores is more expensive than licensing the p-cores of the host.

To use the unlimited virtualization benefit, you need to create a *SQLServerLicense* resource that covers the specific *SQL Server - Azure Arc* instances that you intend to include. For details about creating *SQLServerLicense* resources, see [Create a SQL Server license](manage-configuration.md#create-license-resource).

> [!CAUTION]
> The unlimited virtualization benefit isn't available to VMs running on infrastructure from any of the [listed providers](https://aka.ms/listedproviders). These VMs can be licensed only by v-cores. If you create a *SQLServerLicense* resource with the intent of licensing these VMs by using unlimited virtualization, you'll be charged for the consumption of v-cores based on the SQL Server configuration of the host. Any existing p-core licenses don't apply to offset such charges.

For more information about licensing by physical cores with unlimited virtualization, see the section "Licensing for maximum virtualization" in the [SQL Server licensing guide (download link)](https://go.microsoft.com/fwlink/p/?linkid=2215573).

A single *SqlServerLicense* resource can cover multiple virtual machines connected to Azure Arc. It includes the following properties that define how the license is applied and billed.

### License category

The `licenseCategory` property is set to `Core` to represent a SQL Server physical core license.

### Scope

The `scopeType` property sets the Azure scope in which the license covers all qualified *Machine - Azure Arc* resources. The following Azure scopes are supported:

- Azure tenant
- Azure subscription
- Resource group

To qualify, each *Machine - Azure Arc* resource must be configured to use a physical core license. Otherwise, the *Machine - Azure Arc* resource must be licensed for SQL Server individually.

### Size

The `Size` property of the license resource represents the sum of physical cores of the servers to which the license will be applied. The minimum size of the license is 16 p-cores.

### Subscription

The `Subscription` property defines which Azure subscription will be used for billing and invoicing when the license is active.

You can create the license resource in a resource group in any of the [supported regions](overview.md#supported-azure-regions). The location of the resource is set to the location of the selected resource group.

The location of the license resource doesn't affect the scope. It applies to all *Machine - Azure Arc* resources in the scope of the license, regardless of the regions where these resources are onboarded.

> [!IMPORTANT]
> You can associate multiple license resources with the same scope or overlapping scopes. For example, you can add a new license when you deploy additional physical servers during temporary bursts of activity, or to reflect unexpected growth. All the virtual machines running on these physical servers must be connected to Azure Arc in the scope of the license resource.

### Billing plan

The `billingPlan` property provides a choice between paying for the license on an hourly meter or by bringing your own license.

| Billing&nbsp;plan&nbsp;&nbsp;| Description | Value |
|---|---|---|
| Pay-as-you-go | By selecting this option, you subscribe to unlimited virtualization service that's billed on an hourly meter for the Enterprise edition. See [SQL Server prices and licensing](https://www.microsoft.com/sql-server/sql-server-2022-pricing). | `PAYG` |
| Bring your own license | By selecting this option, you attest that you have an active Enterprise license with Software Assurance or a SQL Server subscription for the same or a greater number of cores. You also attest that you want to use that license to cover the usage of the SQL Server software on each VM in its scope by using the unlimited virtualization benefit. | `Paid` |

To ensure the correct application of the p-core license, make sure that each VM in the scope that you want to be licensed:

- Has the `UsePhysicalCoreLicense` property set to `True`.
- Has the `LicenseType` property set to match the selected `billingPlan` property of the p-core license.

For more information, see [Use a physical core license](manage-configuration.md#use-physical-core-license).

### Activation state

The `activationState` property controls when the license takes effect. You can activate the license during creation, or you can create the license first and then activate it later. Delaying the activation helps you coordinate it with other events in the licensing life cycle, such as the expiration of an existing Enterprise Agreement. The `activatedAt` and `deactivatedAt` time-stamp properties show when the license was last activated and deactivated. For more information, see [Update a SQL Server license resource](manage-configuration.md#change-license-resource).

### Tenant ID

The `TenantID` property is automatically set when you select a tenant scope.

## <a id="non-production-licensing"></a> Manage SQL Server licensed for non-production use

If you're using one of the supported licensing options to manage your production environment through Azure Arc, you can use SQL Server for non-production purposes for free. You can take advantage of this benefit in the following two ways when you're using SQL Server enabled by Azure Arc.

### Use SQL Server Developer edition

The SQL Server Developer edition is free and can be used in any Azure subscription. Azure Extension for SQL Server detects it and reports the usage via a $0 *Dev edition* meter, even if the license type of the host is set to `Paid` or `PAYG`. The Developer edition has the same feature set as the Enterprise edition. For more information, see [Metering software usage](manage-license-billing.md#usage-metering) later in this article.

### Use an Azure dev/test subscription

If you configure the non-production environment as a mirror of the production environment, and you want to use the same editions that you use in production, you must connect the hosting machines and SQL Server instances to an Azure dev/test subscription. The SQL Server meters in a dev/test subscription are nullified.

For information, see:

- [Creating Enterprise and Organization Azure Dev/Test Subscriptions](/azure/devtest/offer/quickstart-create-enterprise-devtest-subscriptions).
- The section "Licensing SQL Server for non-production use" in the [SQL Server licensing guide (download link)](https://go.microsoft.com/fwlink/p/?linkid=2215573).

## Manage SQL Server licensed for high availability and disaster recovery

If your SQL Server instance is a passive replica created as part of your high-availability or disaster recovery configuration, you're entitled to the failover benefits that are included if your license type is set to `Paid` or `PAYG`. For more information about the failover benefits, see the section "Licensing SQL Server for high availability and disaster recovery" in the [SQL Server licensing guide (download link)](https://go.microsoft.com/fwlink/p/?linkid=2215573).

To help you manage the failover benefits and remain compliant, Azure Extension for SQL Server automatically detects the passive instances and reflects the use of the SQL Server software by emitting special $0 meters for disaster recovery, as long as you properly configured the `LicenseType` property. For more information, see [Metering software usage](manage-license-billing.md#usage-metering) later in this article.

## <a id="server-cal"></a> Manage SQL Server instances that use a Server+CAL license

You can connect any licensed SQL Server instance to Azure Arc, including instances that use the Server+CAL licensing model. If your instance uses this license, you must set the license type to `LicenseOnly`, even if you have active Software Assurance for it.

If you converted your Enterprise Server+CAL license to a core-based license, you should set the license type to `Paid` or `PAYG`. The best practice is to also upgrade the SQL Server edition from Enterprise to Enterprise Core, because the latter provides the complete set of SQL Server capabilities. But even if you didn't upgrade the instances, Azure Extension for SQL Server monitors software usage as Enterprise Core.

## <a id="usage-metering"></a> Metering software usage

The usage of the SQL Server software is reported once an hour. The specific meter is automatically selected based on the SQL Server edition and the number v-cores or p-cores visible to the OSE. The following rules apply:

- If you install one or several SQL Server instances on a virtual machine and don't specify the use of a physical core license, SQL Server software usage is metered based on the total number of virtual cores available to the OSE. The minimum is four cores per OSE.

- If you install one or several SQL Server instances on a physical server without using virtual machines, SQL Server software usage is metered based on the total number physical cores available to the OSE. The minimum is four cores per OSE.

- SQL Server software usage is reported per OSE whether one or multiple SQL Server instances are installed on the same OSE.

- If two or more instances of the same edition are installed, the first instance in alphabetical order reports usage.

- The combination of the selected `LicenseType` value and the highest SQL Server edition installed on the OSE defines which meter is sent every hour.

For more information, see [SQL Server Licensing Resources and Documents](https://www.microsoft.com/licensing/docs/view/SQL-Server).

The following table shows the meter product tiers (also called *SKUs*) that are used for metering and billing for SQL Server software installed on a single OSE:

| Installed edition | Projected edition | License type | Failover replica | Use p-core license | Meter SKU |
|--|--|--|--|--|--|
| Enterprise Core | Enterprise | `PAYG` | No | No | `Ent edition - PAYG` |
| Enterprise Core | Enterprise | `PAYG` | No | Yes | `Ent edition - Virtual license`<sup>2</sup> |
| Enterprise Core | Enterprise | `Paid` | No | No |`Ent edition - AHB` |
| Enterprise Core | Enterprise | `Paid` | No | Yes |`Ent edition - Virtual license`<sup>2</sup> |
| Enterprise Core | Enterprise | `LicenseOnly` | Yes or no | Not applicable |`Ent edition - License only` |
| Enterprise Core | Enterprise | `PAYG` or `Paid` | Yes | Yes or no | `Ent edition - DR replica` |
| Enterprise <sup>1</sup> | Enterprise | `PAYG` | No | No | `Ent edition - PAYG` |
| Enterprise <sup>1</sup> | Enterprise | `PAYG` | No | Yes | `Ent edition - Virtual license`<sup>2</sup> |
| Enterprise <sup>1</sup>| Enterprise | `Paid` | No | No | `Ent edition - AHB` |
| Enterprise <sup>1</sup>| Enterprise | `Paid` | No | Yes | `Ent edition - Virtual license`<sup>2</sup> |
| Enterprise <sup>1</sup>| Enterprise | `LicenseOnly` | Yes or no | Not applicable | `Ent edition - License only` |
| Enterprise <sup>1</sup>| Enterprise | `PAYG` or `Paid` | Yes | Yes or no | `Ent edition - DR replica` |
| Standard | Standard | `PAYG` | No | No | `Std edition - PAYG` |
| Standard | Standard | `PAYG` | No | Yes | `Std edition - Virtual license`<sup>2</sup> |
| Standard | Standard | `Paid` | No | No | `Std edition - AHB` |
| Standard | Standard | `Paid` | No | Yes | `Std edition - Virtual license`<sup>2</sup> |
| Standard | Standard | `LicenseOnly` | No | Not applicable | `Std edition - Virtual license`<sup>2</sup> |
| Standard | Standard | `PAYG` or `Paid` | Yes | Yes or no | `Std edition - DR replica` |
| Evaluation | Evaluation | Any | Yes or no | Not applicable | `Eval edition` |
| Developer | Developer | Any | Yes or no | Not applicable | `Dev edition` |
| Web | Web | Any | Not applicable | Not applicable | `Web edition` |
| Express | Express | Any | Not applicable | Not applicable | `Express edition` |

<sup>1</sup> Installation of the Enterprise edition indicates use of the Server+CAL licensing model.

<sup>2</sup> This meter reflects the software usage covered by the p-core license and the unlimited virtualization benefit. For the SQL Server instance to be covered, it must be installed on a virtual machine.

The following table shows the meter SKUs that are used for metering and billing for SQL Server software covered by a physical core license with unlimited virtualization:

| License category | Projected edition | Billing plan | Meter SKU |
|--|--|--|--|
| P-core license | Enterprise | `PAYG` | `Ent edition - Host - PAYG` |
| P-core license | Enterprise | `Paid` | `Ent edition - AHB` |

## Related content

- [Product terms for SQL Server enabled by Azure Arc](https://www.microsoft.com/licensing/terms/productoffering/MicrosoftAzure/eaeas#ServiceSpecificTerms)
- [SQL Server Licensing Resources and Documents](https://www.microsoft.com/licensing/docs/view/SQL-Server)
- [SQL Server 2022 pricing and licensing](https://www.microsoft.com/sql-server/sql-server-2022-pricing)
- [Configure SQL Server enabled by Azure Arc](manage-configuration.md)
- [Frequently asked questions](faq.yml#billing)
