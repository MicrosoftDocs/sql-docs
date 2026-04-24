---
title: Azure-based Pay-as-you-go subscription for SQL Server
description: A comprehensive guide to pay-as-you-go subscription for SQL Server, including core licensing fundamentals, billing models, metering, included features and benefits.
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest, maghan, mathoma
ms.date: 04/21/2026
ai-usage: ai-assisted
ms.topic: conceptual
ms.custom:
  - ignite-2025
---

# Pay-as-you-go subscription for SQL Server

Pay-as-you-go subscription for SQL Server allows you to use SQL Server software on physical or virtual machines running on Azure or connected to Azure via Azure Arc and pay for it using a modern cloud-based billing model. While subscribed, you pay for SQL Server software based on the time the software is online and the number of cores it can use. The subscription can be cancelled at any time and the billing will stop immediately.

This article is a self-contained guide covering everything you need to know about the subscription options, the billing rules that determine how charges are calculated, and the features that are included with the subscription.

## Terminology

- **Pay-as-you-go (PAYG)** is a type of contract with a service where you pay only for the resources you actually consume, rather than committing to a fixed upfront cost or long-term contract. Usage is metered hourly and billed periodically based on accumulated consumption. You can start or stop using the service at any time without penalties.
- A **core** is a physical processing unit in a CPU (physical core, or p-core) or a virtual CPU allocated to a virtual machine (virtual core, or v-core). If virtual cores are mapped to hardware threads, each hardware thread is counted as a core.
- **Available cores** are the cores that SQL Server can access. If the available cores are constrained by the OS or by the SQL Server edition, available cores are adjusted accordingly. The SQL Server software consumption is measured in **available cores** and billed hourly.
- **Normalized core (NC)** is a uniform unit of consumption that allows comparing the cost of SQL Server deployments across different editions. One core used by Standard edition equals to one NC. One core used by Enterprise edition equals to four NCs.
- **Operating system environment (OSE)** is the operating system instance that SQL Server runs in. On a physical server without virtualization, the OSE is the physical OS instance that has access to all the resources of the physical server. On a virtual machine, the OSE is the guest OS inside the VM that has access to the resources assigned to it by the virtualization software.
- **Recurring billing** is a fallback billing mechanism during extended periods of disconnectivity that uses the last known configuration of SQL Server (edition, number of cores, HADR setup, etc.) to determine the level of consumption instead of the hourly usage data collected by the extension.

## Editions

PAYG subscription is available for the following editions:

- **Enterprise edition**: Ideal for applications requiring mission-critical performance, security, and high availability. Enterprise edition also offers unlimited virtualization rights.
- **Standard edition**: Designed for small to medium-sized businesses. It provides many of the core database features of Enterprise edition, but with limitations on scalability and high availability. The Standard edition enforces a maximum compute capacity that varies by SQL Server version. For details, see [Compute capacity limits by edition of SQL Server](../compute-capacity-limits-by-edition-of-sql-server.md).
- **Web edition**:  A legacy edition designed for web hosting applications and available exclusively through services providers. The PAYG subscription allows the customers to streamline their transition to a cloud-based billing model.

For a full feature-by-feature comparison of the SQL Server editions, visit [SQL Server 2025 editions](https://aka.ms/sqlserver2025editions).

For more information about the compute capacity limits for each edition of SQL Server, see [Compute capacity limits by edition of SQL Server](../compute-capacity-limits-by-edition-of-sql-server.md).

> [!NOTE]
> Free editions (Developer, Express, and Evaluation) don't require a PAYG subscription. Developer and Express editions are metered at $0 and Evaluation edition is free for up to 180 days.

## PAYG subscription for individual OSE

Use this option when you want to pay for SQL Server software running in each OSE based on the available cores, the installed SQL Server editions, and online status.

This option is optimized for the following scenarios:

- Your SQL Server VMs are mixed with VMs running other software on the same physical servers.
- You deploy your VMs to a hosting partner or a non-Microsoft cloud where you don't control your physical infrastructure.
- You don't use virtualization to maximize SQL Server performance or because of the licensing cost of the 3rd party software.

### Billing rules

You enable the PAYG subscription for each machine using the [SQL Server configuration](manage-configuration.md) UX of the Azure portal. By doing so you automatically consent to recurring billing. The billing starts immediately after the consent is registered.

- The billing system uses **available cores** as a measure of SQL Server software consumption.
- The consumption is evaluated and billed every hour. The minimum hourly consumption is **four cores**.
- No fractional billing. Each hour the usage is billed for all **available cores** even if SQL Server was online for a fraction of an hour.
- No duplicate billing. For billing purposes, each available core is counted once per hour regardless of the number of online SQL Server instances that can run on that core during the hour.
- The billing rate is based on the highest production edition running in the OSE.

The following example illustrates the cost implications of enabling PAYG subscription for the four VMs running on the same physical server.

:::image type="content" source="media/manage-license-billing/virtual-core-licensing.svg" alt-text="Diagram that illustrates the virtual core licensing option.":::

The following example shows the cost implications of running two SQL Server instances on a physical server without virtualization.

:::image type="content" source="media/manage-license-billing/physical-core-licensing-without-vms.svg" alt-text="Diagram that illustrates physical core licensing without using virtual machines.":::

### SQL Configuration properties  

To activate a PAYG subscription, ensure that SQL Server Configuration has the following properties:

| Property | Value | Description |
| --- | --- | --- |
| `LicenseType` | `PAYG` | Enables PAYG subscription for the host. |
| `ConsentToRecurringPAYG.Consented` | `True` | You agree to recurring billing. Required to enable PAYG subscription. |
| `ConsentToRecurringPAYG.ConsentTimestamp` | UTC timestamp | The time when the consent was registered. Used by the billing system to determine when recurring billing goes into effect. After that time any disconnection longer than 30 days activates recurring billing. |

> [!IMPORTANT]
>
> PAYG subscriptions aren't allowed without the consent. Once registered, the consent property can't be changed without reinstalling the extension.

## PAYG subscription for physical servers with unlimited virtualization

Use this option when you control your physical environment, install SQL Server instances on different VMs, and licensing by v-cores would be more expensive than licensing the p-cores of the host.

This option requires Enterprise edition and enables running any number of instances in an unlimited number of VMs on a licensed physical host.

### Billing rules

- You create a `SQLServerLicenses` resource in Azure that represents the total size in cores for a single or multiple physical hosts. The **minimum core count** of the license is **16 cores**.
- All the SQL Server virtual machines covered by the license must be connected to Azure Arc, configured to use the p-core license and have PAYG subscription enabled.
- Once the license is activated, it's billed on an hourly basis based on the configured core count at an **Enterprise edition** rate.
- The billing amount doesn't change regardless of the covered SQL Servers' configurations, VM sizes, and online status.
- The covered VMs report usage with a $0 meter but can access all the features included with the PAYG subscription.
- If a physical OSE is subscribed to PAYG subscription and connected to Azure Arc in the scope covered by a p-core license, the unlimited virtualization benefit doesn't apply to that machine. Such PAYG subscription is billed separately.
- If you deactivate or delete the `SQLServerLicenses` resource, the billing system will automatically start charging each individual VM in its scope.

For details about creating `SQLServerLicenses` resources, see [Create a SQL Server license](manage-configuration.md#create-a-sql-server-license).

The following example shows the cost of running four SQL Server instances in virtual OSE using a p-core license.

:::image type="content" source="media/manage-license-billing/physical-core-licensing-with-vms.svg" alt-text="Diagram that illustrates physical core licensing with unlimited virtualization.":::

> [!CAUTION]
> Unlimited virtualization benefit isn't available to VMs running on infrastructure from any of the [listed providers](https://aka.ms/listedproviders). These VMs can only enable PAYG subscription for individual OSE. If you create a `SQLServerLicenses` resource to enable Unlimited Virtualization, it will not have an effect on the VMs hosted by the listed providers.

### Physical core license properties

The following properties define how the p-core license is applied and billed:

| Property | Description |
| --- | --- |
| `licenseCategory` | Set to `Core` to represent a SQL Server physical core license. |
| `scopeType` | The Azure scope in which the license covers all qualified *Machine - Azure Arc* resources. Supported scopes: Azure tenant, Azure subscription, resource group. |
| `Size` | The sum of physical cores of the licensed servers. Minimum 16 p-cores. |
| `Subscription` | The Azure subscription used for billing and invoicing. |
| `billingPlan` | Set to `PAYG` for pay-as-you-go. The license is billed on an hourly meter for Enterprise edition. |
| `activationState` | Controls when the license takes effect. You can activate during creation or delay activation. |
| `TenantID` | Automatically set when you select a tenant scope. |

> [!IMPORTANT]
> You can associate multiple license resources with the same scope or overlapping scopes. For example, you can add new licenses when you deploy additional physical servers during temporary bursts of activity. All VMs running on these physical servers must be connected to Azure Arc in the scope of the license resource.

### SQL Configuration properties  

To ensure correct application of a p-core license, make sure that each VM in the scope of the `SQLServerLicenses` resource has the following SQL Server Configuration properties:

| Property | Value | Description |
| --- | --- | --- |
| `UsePhysicalCoreLicense` | `True` | Indicates that the VM is covered by a p-core license. |
| `LicenseType` | `PAYG` | Enables PAYG subscription for the host. |
| `ConsentToRecurringPAYG.Consented` | `True` | You agree to recurring billing. Required to enable PAYG subscription. |
| `ConsentToRecurringPAYG.ConsentTimestamp` | UTC timestamp | The time when the consent was registered. Used by the billing system to determine when recurring billing goes into effect. After that time any disconnection longer than 30 days activates recurring billing. |

> [!IMPORTANT]
>
> PAYG subscriptions aren't allowed without the consent. Once registered, the consent property can't be changed without reinstalling the extension.


For more information, see [Use a physical core license](manage-configuration.md#use-physical-core-license).

## Connectivity requirements

The PAYG subscription requires the subscribed machines to maintain steady connectivity with Azure. The periodic uploads of the usage data to Azure provide detailed accounting of the **available cores** and the online status of the SQL Servers every hour and ensure accurate billing.

The billing pipeline has the built-in resilience to intermittent connectivity disruptions for up to **30 consecutive days** without affecting billing accuracy. If the machine stays disconnected for more than 30 days, the PAYG subscription falls back to recurring billing.

### Reconnection within 30 days

If the machine reconnects within 30 days of being disconnected, PAYG billing is based on the actual usage logs maintained locally by the extension.

### Disconnection beyond 30 days

If the machine remains disconnected for more than 30 days, the SQL Arc service switches to recurring billing based on the last known configuration. For example: edition, number of cores, HADR setup. Charges include:

- Backfill charges for the previous 30 days.
- Ongoing hourly charges until the machine reconnects.

These charges use separate recurring PAYG meters to track usage during the disconnected state.

### Billing behavior during disconnection

The following timeline illustrates the billing behavior for both SQL Server and Windows Server, comparing scenarios where the machine reconnects within 30 days versus after 30 days.

| Timeline | Event | Service's actions |
|----------|-------|-------------------|
| Day 1 | Sets up Arc + Arc SQL using PAYG on a Windows Server. | - Billing starts based on current configuration and actual usage.<br>- Usage is uploaded and processed every 12 hours. |
| Day 2 | Azure detects disconnection due to agent failure or blocked connectivity. | Connected machine state changes to **Disconnected**.<br>- Azure extension for SQL Server continues collecting and storing usage data locally.<br>- Warnings appear in the Arc machine Activity Log, SQL Server Configuration page, and SQL Server Overview blade.<br>- No hourly meters are emitted. |
| Day 3+ | Continued disconnection | Daily reminders are emitted, warning that recurring billing will begin after 30 days. |
| Day 30 | Still no usage records or heartbeat. | Connected machine agent's certificate expires.<br>- Connected machine state changes to Expired.<br>- SQL switches to recurring billing based on the last known configuration (edition, cores, HADR setup, etc.).<br>- Charges are backfilled for the past 30 days.<br>- Hourly billing resumes using recurring meters. |
| Day 31+ | Continued disconnection. | Hourly billing continues indefinitely using the last known configuration. |
| Day 40 | Connectivity is restored | Hourly billing switches to regular PAYG meters and continues. |

If connectivity is restored within 30 days, PAYG billing resumes based on actual usage data collected by the Azure extension for SQL Server and reported through standard PAYG meters. If connectivity is restored after 30 days, billing continues based on the last known configuration of the SQL instance using recurring PAYG meters until the connection is re-established.

## PAYG benefits

The following table summarizes the PAYG subscription benefits and shows how it compares to benefits included with License type `Paid`, i.e. SQL Servers covered by License with Software Assurance or SQL Server subscription.

| Benefit | Description | Included if LT=`Paid` |
| --- | --- | --- |
| New version rights | Access to new SQL Server versions as soon as they're released. | Yes |
| Free HADR replica | Automatic detection and $0 billing of passive SQL Server instances for HADR scenarios. See [High availability and disaster recovery](#high-availability-and-disaster-recovery). | Yes |
| Support a monitoring database on the HADR replica | Allows running an Express edition database on free HADR replica nodes | No |
| Unlimited virtualization (Enterprise edition) | Ability to create a single PAYG subscription for one or multiple physical servers that covers an unlimited number of VMs they host. | Yes |
| Constrained cores optimization | Available cores are automatically adjusted to the limits set by the SQL engine or the OSE. See [Compute capacity limits by edition of SQL Server](../compute-capacity-limits-by-edition-of-sql-server.md) and [VM vCore Customization Feature](/azure/virtual-machines/vm-customization). | No |
| Flexible hosting | Run SQL Server enabled by Azure Arc with PAYG on your own devices, or devices under the day-to-day management and control of third parties, including listed providers. <sup>1</sup> | Yes<sup>2</sup> |
| Option to switch between different subscription types | Easily switch between PAYG subscription for VMs and PAYG subscription for physical servers. | n/a |
| Support dev/test environment | Ability to run SQL Server production editions for free using Azure dev/test subscriptions | Yes |
| Free Power BI Report Server license | Available for both Standard and Enterprise editions | Yes |
| ESU subscription | Access to [Extended Security Updates](extended-security-updates.md). | Yes |
| Best practices assessment | Access to [SQL best practices assessment](assess.md). | Yes |
| Automated backups | Access to [automated backups to local storage (preview)](backup-local.md). | Yes |
| Point-in-time restore | Access to [point-in-time restore](point-in-time-restore.md). | Yes |
| Automatic updates | Access to [automatic updates](update.md). | Yes |
| Monitoring (preview) | Access to [SQL monitoring](sql-monitoring.md). | Yes |

<sup>1</sup> Unlimited virtualization benefit isn't available to VMs running on infrastructure from any of the [listed providers](https://aka.ms/listedproviders). These VMs can only enable PAYG subscription for individual OSE. If you create a `SQLServerLicenses` resource to enable Unlimited Virtualization, it will not have an effect on the VMs hosted by the listed providers.

<sup>2</sup> Through Bring Your Own License benefit

## PAYG on Linux

The following limitations apply to PAYG subscription on Linux:

- **Passive instance detection**: Automatic detection of passive replicas in availability groups or failover cluster instances isn't available. All instances are billed as active.
- **Core visibility**: Core count is based on the operating system environment. Database Engine-level core verification isn't available.
- **Connected user detection**: Verification of active user connections on readable secondary replicas isn't available.

These limitations don't affect license compliance or the ability to use PAYG subscription on Linux. However, you should account for the billing differences when planning PAYG deployments on Linux. For more information about feature availability by operating system, see [Feature availability by operating system](overview.md#feature-availability-by-operating-system).

## High availability and disaster recovery

With PAYG subscriptions you can benefit from free passive instances of SQL Server for high availability and disaster recovery (HADR) configurations. Azure Extension for SQL Server automatically detects passive instances for availability groups (AGs) or failover clustered instances (FCIs) and reports their usage via special $0 meters for disaster recovery.

A passive SQL Server replica is one that isn't serving SQL Server data to clients or running active production SQL Server workloads. You can use Express edition SQL Server on the passive node to store the SQL Server monitoring data.

### Passive instance eligibility for availability groups

To qualify as a passive instance for an AG:

1. All replicas present in the OSE must be a secondary replica of an Always On availability group or the forwarder of a distributed availability group.
1. No standalone database outside of an AG irrespective of database state except for a single Express edition instance.
1. No active connections to any database except `master`, `msdb`, `tempdb`, or `model` databases.
1. No instances of SQL Server associated services in the same OSE.

If there are multiple SQL Server instances on the OSE, all instances and replicas must meet the conditions above.

**Replica role requirements:**

| Role | Readable secondary? | Active user connections? | Passive DR eligible? |
| --- | --- | --- | --- |
| Secondary | No (non-readable) | N/A | Yes |
| Secondary | Yes (readable) | No connections | Yes |
| Secondary | Yes (readable) | Has connections | No |
| Primary (standalone AG) | N/A | N/A | No |
| Primary (in DAG, primary AG) | N/A | N/A | No |
| Forwarder (primary in secondary AG of DAG) | No (not in use) | N/A | Yes |
| Forwarder | Yes (readable) | Has connections | No |

### Passive instance eligibility for failover clustered instances

To qualify as a passive FCI node:

- No instances of the SQL Server service (whether as standalone or as an active FCI node) can be in a running state on the node, unless those instances qualify as free passive replicas of AGs.
- No instances of SQL Server associated services can be running in the same OSE.

### Billing after failover

During failovers, the extension detects the role transition and automatically switches billing to the active replica without duplicate charges.

### Limitations

The current passive instance detection logic has the following limitations:

- The checks are hourly. A failover within the hour might or might not bill both replicas.
- Passive instances for other disaster recovery technologies like log shipping or mirroring aren't automatically detected.
- The detection logic doesn't support free disaster recovery testing.
- The detection logic doesn't support monitoring connections like database consistency checks, backups, or monitoring resource usage data.
- On Linux, passive instance detection isn't available. All SQL Server instances on Linux are billed as active, regardless of their HADR role.

## Non-production use

If you're using PAYG subscription to manage your production environment, you can use SQL Server for non-production purposes for free in the following ways:

### Use SQL Server Developer edition

SQL Server Developer edition is free and includes the same feature set as Enterprise edition. It can be used in any Azure subscription. Azure Extension for SQL Server detects it and reports the usage via a $0 *Dev edition* meter, regardless of the host license type.

### Use an Azure dev/test subscription

If you configure the non-production environment as a mirror of the production environment, and you want to use the same editions that you use in production, connect the hosting machines and SQL Server instances to an Azure dev/test subscription. The PAYG meters in the dev/test subscriptions are nullified.

For information, see [Creating Enterprise and Organization Azure Dev/Test Subscriptions](/azure/devtest/offer/quickstart-create-enterprise-devtest-subscriptions).

## SQL Server associated services

[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] enabled by Azure Arc provides license management for the following associated services:

[!INCLUDE [sql-server-associated-services](includes/sql-server-associated-services.md)]

The SQL Server associated services are represented and managed for licensing purposes as SQL Server instances. Their usage is reported using the same metering rules described in [Metering and reporting](#metering-and-reporting).

> [!IMPORTANT]
> The SQL Server associated service installations require a separate license only when they're installed on the machine as a standalone instance (without SQL Server database engine). Otherwise, a separate license isn't required.
>
> When the SQL Server associated service is a standalone instance (without SQL Server database engine) and the machine uses a PAYG subscription, the corresponding PAYG meters are activated for the instance.
>
> If a p-core license is activated as a PAYG subscription and the machine is configured to use it, the SQL Server associated service isn't individually billed when it's a standalone instance (without SQL Server database engine). For details, see [Use a physical core license](manage-configuration.md#use-physical-core-license).

## Metering and reporting

The usage of SQL Server software is reported once an hour. The specific meter is automatically selected based on the SQL Server edition and the number of v-cores or p-cores visible to the OSE. The following rules apply:

- SQL Server software usage is reported per OSE whether one or multiple instances are installed on the same OSE.
- If two or more instances with the same edition are installed, the first instance in alphabetical order reports usage.
- If two or more instances are installed on the same OSE, the instance with the highest edition is billed.
- The combination of the `PAYG` license type value and the highest SQL Server edition installed on the OSE defines which meter is sent.

### Billing meters used with PAYG subscription for EOS

| Installed edition | Host license type | Failover replica | Use p-core license | Meter SKU |
| --- | --- | --- | --- | --- |
| Enterprise Core | `PAYG` | No | No | `Ent edition - PAYG` |
| Enterprise Core | `PAYG` | No | Yes | `Ent edition - Virtual license` |
| Enterprise Core | `PAYG` | Yes | Yes or no | `Ent edition - DR replica` |
| Enterprise | `PAYG` | No | No | `Ent edition - PAYG` |
| Enterprise | `PAYG` | No | Yes | `Ent edition - Virtual license` |
| Enterprise | `PAYG` | Yes | Yes or no | `Ent edition - DR replica` |
| Standard | `PAYG` | No | No | `Std edition - PAYG` |
| Standard | `PAYG` | No | Yes | `Std edition - Virtual license` |
| Standard | `PAYG` | Yes | Yes or no | `Std edition - DR replica` |

> [!NOTE]
> The `Ent edition - Virtual license` and `Std edition - Virtual license` meters reflect the software usage covered by the p-core license and the unlimited virtualization benefit. For the SQL Server instance to be covered, it must be installed on a virtual machine.

### Billing meters used with PAYG subscription with unlimited virtualization

| License category | Projected edition | Billing plan | Meter SKU |
| --- | --- | --- | --- |
| P-core license | Enterprise | `PAYG` | `Ent edition - Host - PAYG` |

For pricing, see [SQL Server pricing and licensing](https://www.microsoft.com/sql-server/sql-server-2022-pricing).


## Manage extension health

The extension health is critical for compliance because it collects the usage data and ensures correct billing. Intermittent disconnections up to 30 days are allowed as the extension maintains a usage log on the machine, but it's your responsibility to ensure the extensions stay healthy. The Azure portal includes a [Health Dashboard](https://ms.portal.azure.com/#view/Microsoft_Azure_ArcCenterUX/ArcCenterMenuBlade/~/sqlServerHealthDashboard) providing the high level view of the extensions' state. For details of troubleshooting of the unhealthy extensions, see [Troubleshoot Azure extension for SQL Server](troubleshoot-extension.md).

## Analyze costs

After you enable PAYG subscription, you can view current and forecasted charges through [Microsoft Cost Management](/azure/cost-management-billing/cost-management-billing-overview). Upcoming charges for Azure Arc-enabled SQL Server PAYG aren't shown on SQL Server or Arc resource pages. All cost analysis and forecasting happens at the subscription level or higher.

### View forecasted charges

To view forecasted PAYG charges:

1. In the Azure portal, open **Cost Management** > **Cost analysis**.
1. Select the appropriate scope (subscription, management group, or resource group).
1. Confirm the chart shows both actual and forecasted costs.
   - Solid bars or lines represent actual costs
   - Shaded extensions represent forecasted costs based on historical usage trends
1. Set the date range to the current month to see projected month-end costs.

:::image type="content" source="media/manage-pay-as-you-go-transition/cost-analysis-forecasted-charges.png" alt-text="Screenshot of Microsoft Cost Management showing actual and forecasted charges." lightbox="media/manage-pay-as-you-go-transition/cost-analysis-forecasted-charges.png":::

### Filter for Arc SQL Server usage

To isolate Azure Arc-enabled SQL Server PAYG charges, apply these filters:

| Filter | Value |
|--------|-------|
| **Service name** | Azure Arc-enabled SQL Server |
| **Charge type** | Usage |
| **Publisher type** | Microsoft |

> [!TIP]
> If **Azure Arc-enabled SQL Server** doesn't appear in the filter list, remove other filters first, then reapply **Service name**.

### Identify resource-level costs

To see which SQL Server instances are driving costs:

1. In Cost analysis, select **Group by** > **Resource**.
1. Optionally, select **Group by** > **Resource group** if you organize Arc machines by resource group.

This breakdown helps you identify:

- Core count changes
- Edition differences (Standard vs Enterprise)
- Instances that were unintentionally left running

### Identify charge categories

To see which SQL Server related charge categories are driving costs, in **Cost analysis**, select **Group by** > **Meter**. This breakdown helps you identify:

- Usage category breakdown (for example, ESU costs, PAYG costs)
- Edition differences (meter names indicate the editions)
- Unexpected charges

### Set up budget alerts

To proactively manage costs:

1. In Cost Management, select **Budgets** > **Add**.
1. Create a monthly budget for your expected Arc SQL spend.
1. Configure alerts at 50%, 75%, and 90% of budget.
1. Save the budget.

Budget alerts use forecasted costs, not just actual spend, to help you avoid unexpected charges. For more information, see [Create and manage budgets](/azure/cost-management-billing/costs/tutorial-acm-create-budgets).

## Monitor billing events

To monitor, review [Use activity logs with SQL Server enabled by Azure Arc](activity-logs.md).

## Related content

- [Manage licensing and billing of SQL Server enabled by Azure Arc](manage-license-billing.md)
- [Product terms for SQL Server enabled by Azure Arc](https://www.microsoft.com/licensing/terms/productoffering/MicrosoftAzure/eaeas#ServiceSpecificTerms)
- [SQL Server pricing and licensing](https://www.microsoft.com/sql-server/sql-server-2022-pricing)
- [Configure SQL Server enabled by Azure Arc](manage-configuration.md)
- [Frequently asked questions](faq.yml#recurring-pay-as-you-go-billing)
- [Manage recurring billing for pay-as-you-go](manage-pay-as-you-go-transition.md)
