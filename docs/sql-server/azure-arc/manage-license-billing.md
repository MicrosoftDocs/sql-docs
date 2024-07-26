---
title: Manage license and billing
description: Explains how to manage SQL Server licensing options. Also demonstrates how SQL Server enabled by Azure Arc can be billed from Microsoft Azure. 
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest
ms.date: 03/31/2024
ms.topic: conceptual
---

# Manage licensing and billing of SQL Server enabled by Azure Arc

This article explains how to manage licensing and billing of SQL Server enabled by Azure Arc. Only the core-based licensing methods are directly supported by SQL Server enabled by Azure Arc. For information about how you can manage SQL Server instances with a Server+CAL license, see [Manage SQL Server instances with a Server+CAL license](manage-license-billing.md#server-cal).  The full range of the licensing options is described in the [SQL Server Licensing Guide](https://go.microsoft.com/fwlink/p/?linkid=2215573).

## Licensing and billing in production environment

You can use one of the three licensing options.

- [License by virtual cores](#license-vcores)

   Use Enterprise or Standard license for the vCPUs (v-cores) of the virtual machine that runs one or multiple instances of SQL Server. Each virtual machine is billed individually for the v-core allocated to it. The following diagram illustrates this licensing method and shows its cost implications.

   :::image type="content" source="media/billing/virtual-core-licensing.svg" alt-text="Screenshot illustrating the virtual core licensing option.":::

- [License by physical cores without virtual machines option](#license-pcores-without-vms)

   Use Enterprise or Standard Edition license for the physical cores (p-cores) of the host that runs one or multiple instances of SQL installed directly on the host without using VMs. Each instance has access to all p-cores supported by the installed edition limits up to all p-cores of the host. Regardless of the instance limits though, the host is billed for all the p-cores based on the highest SQL Server edition installed on it. For details, review [Compute capacity limits by edition](../compute-capacity-limits-by-edition-of-sql-server.md).

   The following diagram illustrates the cost implications of deploying two standard edition instances on a physical host without using VMs.

   :::image type="content" source="media/billing/physical-core-licensing-without-vms.svg" alt-text="Screenshot illustrating the physical core licensing without using virtual machines.":::

- [License by physical cores with unlimited virtualization](#unlimited-virtualization)

   Use Enterprise Edition license for the physical cores (p-cores) of the host that runs any number of virtual machines with any number of instances of SQL Server. A single p-core license is a separate Azure resource representing all licensed p-cores and is billed independently. The following diagram illustrates the cost implications of licensing a physical host and using unlimited virtualization.

   :::image type="content" source="media/billing/physical-core-licensing-with-vms.svg" alt-text="Screenshot illustrating the physical core licensing using unlimited virtualization.":::

> [!NOTE]
>
> *Normalized cores* (NC) are used to illustrate the cost implications of different licensing options. One Standard Edition core license is an equivalent of one NC. One Enterprise Edition core license is an equivalent of four NCs. For more information, see  [How licenses apply to Azure resources](/azure/cost-management-billing/scope-level/overview-azure-hybrid-benefit-scope#how-licenses-apply-to-azure-resources).

For each of these options, you have to decide how you want to pay for the license. The following table shows your payment options.

| Payment option | V-core licensing | P-core licensing without VMs | P-core licensing with unlimited virtualization |
|---|---|---|---|
| Subscribe to the service through Microsoft Azure using a pay-as-you-go method | Yes | Yes | Yes |
| Bring your own license with SA or SQL subscription<sup>1</sup> | Yes | Yes | Yes |
| Bring your own license without SA<sup>2</sup> | Yes | Yes | No |

<sup>1</sup> You already have a license with active Software Assurance or an active SQL subscription.

<sup>2</sup> You own a perpetual license or use a Server+CAL license.

> [!NOTE]
>
> Your choice of payment options above may impact your outsourcing options. For more information, please see [Product Terms](https://www.microsoft.com/licensing/terms/productoffering/MicrosoftAzure/eaeas#ServiceSpecificTerms) and [Flexible Virtualization Benefit Licensing Guide](https://wwlpdocumentsearch.blob.core.windows.net/prodv2/Licensing_guide_PLT_Flexible_Virtualization_Benefit_Nov2022.pdf).

For information about licensing your non-production or test SQL Server instances through Azure Arc, see [Manage SQL Server licensed for non-production use](manage-license-billing.md#non-production-licensing).



## <a id="license-vcores"></a> License SQL Server instances by virtual cores

Licensing SQL Server by v-cores allows you to limit the scope of the license to a specific virtual machine (VM) and one or more SQL Server instances installed on the operating system environment (OSE) of that machine. It's optimized for the following scenarios:

- Your SQL Server VMs are mixed with the VMs running other software on the same physical servers.
- You deploy your VMs to a hosting partner or a third party cloud where you don't control your physical infrastructure.

You can choose v-core licensing to license both SQL Server software and SQL Server Extended Security Updates. V-core license is managed independently for each VM using [SQL Server configuration](manage-configuration.md) panel. For your convenience, **Overview** of each Arc-enabled SQL Server resource shows the configured license under **Host License Type**.

Standard Edition is limited to a maximum of 24 v-cores even if the OSE is configured with more v-cores. Details in [Compute capacity limits by edition of SQL Server](../compute-capacity-limits-by-edition-of-sql-server.md).

For more information about licensing SQL Server by virtual cores, see section *Licensing individual virtual machines* in the [SQL Server Licensing Guide](https://go.microsoft.com/fwlink/p/?linkid=2215573).

### License types

The following license types are supported when licensing v-cores:

| License type | Description | Value |  
|---|---|---|
| Pay-as-you-go | Subscribe to the Standard or Enterprise Edition service and be billed on an hourly meter. See [SQL Server prices and licensing](https://www.microsoft.com/sql-server/sql-server-2022-pricing). | `PAYG` |
| License with Software Assurance | Bring your own Standard or Enterprise Edition license with Software Assurance or SQL Subscription. Your software usage is reported using a free hourly meter according to the metering rules. See  [Metering software usage](#usage-metering). | `Paid`|
| License only | You use a perpetual or Server+CAL license for Standard or Enterprise Edition,  or you use Developer, Evaluation, or Express Edition. Your software usage is reported according to the metering rules. See [Metering software usage](#usage-metering). | `LicenseOnly` |

> [!IMPORTANT]
> 
> - The pay-as-you-go subscription requires the hosting machine to be continuously connected to Azure. 
>
>   Intermittent connectivity disruptions for up to 30 days are tolerated with built-in resilience. After 30 days without a connection, the pay-as-you-go subscription will expire. Once your subscription expires, you aren't authorized to use the software.
>
> - The pay-as-you-go hourly charges are issued only when SQL Server is running on the machine at any point within a given hour and if the machine is online.   
>
> - By selecting License with Software Assurance, you attest that you have Enterprise Edition or Standard Edition licenses with active Software Assurance or a SQL subscription license.

In addition to billing differences, license type determines what features are available to your SQL Server instance.

[!INCLUDE [license-types](includes/license-types.md)]

> [!NOTE]
>
> - License type is a required parameter when you install Azure Extension for SQL Server and each supported onboarding method includes the license type options.
> - [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] allows you to select the license type during setup. See [Install Azure Extension for SQL Server from the Installation Wizard](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md#azure-extension-for-sql-server-2022).

## <a id="license-pcores-without-vms"></a> License SQL Server instances by physical cores without using VMs

This licensing option is optimized for the following scenarios:

- You control your physical environment and install the SQL Server instances on a physical server to maximize the performance of your databases application.
- Your SQL Server instance uses a license without Software Assurance.

In this scenario, the licensing requirements are identical to [Licensing SQL Server by virtual cores](#license-vcores) but the SQL Server software usage is reported based on the physical cores available to the OSE of that server. For details, see [Metering software usage](#usage-metering).

Standard Edition is limited to a maximum of 24 p-cores even if the OSE is installed on a larger machine. Details in [Compute capacity limits by edition of SQL Server](../compute-capacity-limits-by-edition-of-sql-server.md).

> [!IMPORTANT]
> If a physical machine without VMs is connected to Azure Arc in the scope that is covered by a SQL Server physical core license, the unlimited virtualization benefit will not apply to that machine. It will be licensed and billed separately.

For more information about licensing SQL Server on physical OSE, see section *Core-based licensing* in the [SQL Server Licensing Guide](https://go.microsoft.com/fwlink/p/?linkid=2215573).


## <a id="unlimited-virtualization"></a> License SQL Server instances by physical cores with unlimited virtualization

This licensing option is most effective when:

- You control your physical environment and install the SQL Server instances on different VMs for security isolation and better resource management.
- Your infrastructure and the selected payment method support the unlimited virtualization benefit (UV).
- Licensing your SQL Server instances by v-cores is more expensive than licensing the p-cores of the host.

To leverage the UV benefit, you need to create a *SQLServerLicense* resource covering the specific *SQL Server - Azure Arc* instances that you intend to include. For  details of creating *SQLServerLicense* resources, see [Create SQL Server license](manage-configuration.md#create-license-resource).

> [!CAUTION]
> The UV benefit is not available to VMs running on any listed providers' infrastructure. They can only be licensed by v-cores. If you create a *SQLServerLicense* resource with the intent of licensing these VMs using UV, you will be charged for the consumption of v-cores based on the SQL Server configuration of the host, and any existing p-core licenses will not apply to offset such charge. See [Listed providers](https://aka.ms/listedproviders) for details.

A single *SqlServerLicense* resource can cover multiple virtual machines connected to Azure Arc. It includes several properties that define how the license is applied and billed. 

The **License category** is set to `Core` to represent SQL Server physical core license.

The **Scope** property sets the Azure scope in which all qualified *Machine - Azure Arc* resource are covered by the license. The following Azure scopes are supported:

- Azure tenant
- Azure subscription
- Resource group

To qualify, each *Machine - Azure Arc* resource must be configured to use a physical core license. Otherwise, the *Machine - Azure Arc* resource must be licensed for SQL Server individually. See [Licensing SQL Server by virtual cores](#license-vcores).

The **Size** property of the license resource represents the sum of physical cores of the servers to which the license will be applied. The minimum size of the license is 16 p-cores. 

The **Subscription** property defines which Azure subscription will be used for billing and invoicing when the license is active.

The license resource can be created in a resource group in any of the supported regions. For the list of supported regions, see [Supported Azure regions](overview.md#supported-azure-regions). The location of the resource is set to the location of the selected resource group. The location of the license resource doesn't impact the scope. It will apply to ALL *Machine - Azure Arc* resources in the scope of the license regardless of the regions where these resources are onboarded.

> [!IMPORTANT]
> Multiple license resources can be associated with the same scope or overlapping scopes. For example, a new license can be added when additional physical servers are deployed during temporary bursts of activity, or to reflect unexpected growth. All the virtual machines running on these physical servers must be connected to Azure Arc in the scope of the license resource.

The **Billing plan** property provides a choice between paying for the license on an hourly meter or by bringing your own license.

| Billing&nbsp;plan&nbsp;&nbsp;| Description | Value |  
|---|---|---|
| Pay-as-you-go | By selecting this option you subscribe to unlimited virtualization service that is billed on an Enterprise Edition hourly meter. See [SQL Server prices and licensing](https://www.microsoft.com/sql-server/sql-server-2022-pricing). | `PAYG` |
| Bring-your-own-license | By selecting this option you attest that you have an active Enterprise Edition license with Software assurance or a SQL subscription for the same or greater number of cores, and wish to use that license to cover the usage of the SQL Server software on each VM in its scope leveraging the unlimited virtualization benefit. | `Paid` |

> [!IMPORTANT]
> To ensure the correct application of the p-core license, make sure that each  VM in the scope you wish to be licensed:
> 1.  has the **Physical core license** property set to True. 
> 1.  has the **License type** property set to match the selected **Billing plan** of the p-core license. 
>
> For more details, see [Apply physical core license](manage-configuration.md#use-physical-core-license).

The **Activation state** property controls when the license takes effect. The license can be activated during creation, or created first and then activated at a later time. The delayed activation allows you to coordinate it with other events in the licensing lifecycle, such as the expiration of an existing Enterprise Agreement. The **Last activated** and **Last deactivated** timestamp properties show when the license was last activated and deactivated. For more details, see [Change SQL Server license properties](manage-configuration.md#change-license-resource).

The **Tenant ID** property is automatically set when the tenant scope is selected. 

For more information about licensing by physical cores with unlimited virtualization, see section *Licensing for maximum virtualization* in the [SQL Server Licensing Guide](https://go.microsoft.com/fwlink/p/?linkid=2215573).

## <a id="non-production-licensing"></a> Manage SQL Server licensed for non-production use

If you have your production environment managed through Azure Arc using one of the supported licensing options, you can use SQL Server for non-production purposes for free. There are two ways you can take advantage of this benefit when using SQL Server enabled by Azure Arc.

### Use SQL Server Developer Edition

SQL Server Developer Edition is free and can be used in any Azure subscription. The Azure extension for SQL Server will detect it and report the usage via a $0 *Dev edition* meter even if the *License type* of the host is set to `Paid` or `PAYG` . The Developer Edition has the same feature set as Enterprise Edition. For more details, see [Metering software usage](manage-license-billing.md#usage-metering).

### Use Azure dev/test subscription

If you configure the non-production environment as a mirror of the production environment, and want to use the same editions as in production, you must connect the hosting machines and SQL Server instances to an Azure dev/test subscription. The SQL Server meters in a dev/test subscription will be nullified. For information on how to create a dev/test subscription on Azure, see [Creating Enterprise and Organization Azure Dev/Test Subscriptions](/azure/devtest/offer/quickstart-create-enterprise-devtest-subscriptions).

For more information, see section *Licensing SQL Server for non-production use* in the [SQL Server Licensing Guide](https://go.microsoft.com/fwlink/p/?linkid=2215573).

## Manage SQL Server licensed for high availability and disaster recovery

If your SQL Server instance is a passive replica created as part of your high availability or disaster recovery configuration, you are entitled to the failover benefits that are included if your *license type* is set to `Paid` or `PAYG`. For more information about the failover benefits, see section *Licensing SQL Server for high availability and disaster recovery* in the [SQL Server Licensing Guide](https://go.microsoft.com/fwlink/p/?linkid=2215573).

To help you manage the failover benefits and remain compliant, Azure extension for SQL Server automatically detects the passive instances and reflects the use of the SQL Server software by emitting special $0 disaster recovery (DR) meters, as long as you properly configured the *license type* property. For more details, see [Metering software usage](manage-license-billing.md#usage-metering).


## <a id="server-cal"></a> Manage SQL Server instances with a Server+CAL license

You can connect any licensed SQL Server instance to Azure Arc, including the ones that are licensed with the Server+CAL licensing model. If your instance uses this license you must set the license type to `LicenseOnly` even if you have active Software assurance for it.  

> [!NOTE]
> If you have converted your Enterprise Server+CAL license to a core-based license, you should set the license type to `Paid` or `PAYG`. The best practice is to also to upgrade the SQL Server edition from `Enterprise` to `EnterpriseCore` as the latter provides the complete set of SQL Server capabilities. But even if you did not upgrade the instances, Azure extension for SQL Server will monitor software usage as `EnterpriseCore`. For more details, see [Metering software usage](manage-license-billing.md#usage-metering).

## <a id="usage-metering"></a> Metering software usage

The usage of the SQL Server software is reported once an hour. The specific meter is automatically selected based on the SQL Server edition and the number v-cores or p-cores visible to the Operating System Environment (OSE). The following rules apply:

- If you install one or several SQL Server instances on a virtual machine and don't specify that a physical core license should be used, SQL Server software usage is metered based on the total number of virtual cores available to OSE, with a minimum of four cores per OSE.

- If you install one or several SQL Server instances on a physical server without using virtual machines,  SQL Server software usage is metered based on the total number physical cores available to the OSE, with a minimum of four cores per OSE.

- SQL Server software usage is reported per OSE whether one or multiple SQL Server instances are installed on the same OSE.

- If two or more instances of the same edition are installed, the first instance in alphabetical order will report usage.

- The combination of the selected **License Type** and the highest SQL Server edition installed on the OSE defines which meter will be sent every hour.

For more information, see [SQL Server Licensing Resources and Documents](https://www.microsoft.com/licensing/docs/view/SQL-Server).

The next table shows the meter SKUs that are used for metering and billing for SQL Server software installed on a single OSE:

| Installed edition | Projected edition | License type  | Failover replica | Use p-core license | Meter SKU |
|--|--|--|--|--|--|
| Enterprise Core | Enterprise | `PAYG` | No | No | `Ent edition - PAYG` |
| Enterprise Core | Enterprise | `PAYG` | No | Yes | `Ent edition - Virtual license`<sup>2</sup> |
| Enterprise Core | Enterprise | `Paid` | No | No |`Ent edition - AHB` |
| Enterprise Core | Enterprise | `Paid` | No | Yes |`Ent edition - Virtual license`<sup>2</sup> |
| Enterprise Core | Enterprise | `LicenseOnly` | Yes or No | n/a |`Ent edition - License only` |
| Enterprise Core | Enterprise | `PAYG` or `Paid` | Yes | Yes or No | `Ent edition - DR replica` |
| Enterprise <sup>1</sup> | Enterprise | `PAYG` | No | No | `Ent edition - PAYG` |
| Enterprise <sup>1</sup> | Enterprise | `PAYG` | No | Yes | `Ent edition - Virtual license`<sup>2</sup> |
| Enterprise <sup>1</sup>| Enterprise | `Paid` | No | No | `Ent edition - AHB` |
| Enterprise <sup>1</sup>| Enterprise | `Paid` | No | Yes | `Ent edition - Virtual license`<sup>2</sup> |
| Enterprise <sup>1</sup>| Enterprise | `LicenseOnly` | Yes or No | n/a | `Ent edition - License only` |
| Enterprise <sup>1</sup>| Enterprise | `PAYG` or `Paid` | Yes | Yes or No | `Ent edition - DR replica` |
| Standard | Standard | `PAYG` | No | No | `Std edition - PAYG` |
| Standard | Standard | `PAYG` | No | Yes | `Std edition - Virtual license`<sup>2</sup> |
|  Standard | Standard | `Paid` | No | No | `Std edition - AHB` |
|  Standard | Standard | `Paid` | No | Yes | `Std edition - Virtual license`<sup>2</sup> |
|  Standard | Standard | `LicenseOnly` | No | n/a | `Std edition - Virtual license`<sup>2</sup> |
| Standard | Standard | `PAYG` or `Paid` | Yes | Yes or No | `Std edition - DR replica` |
| Evaluation | Evaluation | Any | Yes or No | n/a | `Eval edition` |
| Developer | Developer | Any | Yes or No | n/a | `Dev edition` |
| Web | Web | Any | n/a | n/a | `Web edition` |
| Express | Express | Any | n/a | n/a | `Express edition` |

<sup>1</sup> When Enterprise Edition is installed, it indicates that the Server/CAL licensing model is used. See [Manage SQL Server instances with a Server+CAL license](manage-license-billing.md#server-cal) for more information. 

<sup>2</sup> This meter reflects the software usage covered by the p-core license and the unlimited virtualization benefit. The SQL Server instance must be installed on a virtual machine to be covered.

The next table shows the meter SKUs that are used for metering and billing for SQL Server software covered by a physical core license with unlimited virtualization:

| License category | Projected edition | Billing plan | Meter SKU |
|--|--|--|--|
| P-core license | Enterprise | `PAYG` | `Ent edition - Host - PAYG` |
| P-core license | Enterprise | `Paid` | `Ent edition - AHB` |

## Related content

- [Product terms for SQL Server enabled by Azure Arc](https://www.microsoft.com/licensing/terms/productoffering/MicrosoftAzure/eaeas#ServiceSpecificTerms)
- [SQL Server Licensing Resources and Documents](https://www.microsoft.com/licensing/docs/view/SQL-Server)
- [Review SQL Server 2022 Pricing](https://www.microsoft.com/sql-server/sql-server-2022-pricing)
- [Manage SQL configuration](manage-configuration.md)
- [Frequently asked questions](faq.yml#billing)
