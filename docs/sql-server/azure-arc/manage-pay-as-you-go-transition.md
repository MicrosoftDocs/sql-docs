---
title: Move SQL Server license agreement to pay-as-you-go subscription
description: How to transition SQL Server from the legacy licensing model to Azure pay-as-you-go subscription. 
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest, maghan
ms.date: 07/08/2025
ms.topic: how-to
---

# Move SQL Server license agreement to pay-as-you-go subscription

This article explains how to transition the SQL Server instances to pay-as-you-go subscriptions. This option is available for instances of SQL Server that currently:

- Use a SQL Server license with Software Assurance (SA)
- Use a SQL Server subscription license
- Licensed through a Services Provider License Agreement (SPLA)

## Transition from License with Software Assurance  or SQL Server subscription

If your SQL Server instances are covered by a license with Software assurance or a subscription license, typically you want to transition to a pay-as-you-go Azure subscription immediately after the expiration time. At that point you want to make sure that:

- All Arc SQL deployments are switched to pay-as-you-go billing.
- All Azure SQL deployments (PaaS and IaaS) are switched to pay-as-you-go billing.
- The transition tasks are executed immediately after the license agreement expiration for continuous compliance and accurate billing.

To manage the transition follow these steps.

### Prior to license agreement expiration

- Make sure you have an active Azure account with at least one subscription.
- Make sure that all on-premises SQL Server instances covered by the license with Software assurance or by SQL subscription are connected to Azure Arc.
- If you license virtual cores or physical cores without using VMs, make sure that the Azure extensions for SQL Server are configured with `licenseType` set to `Paid`. See [License SQL Server instances by virtual cores](manage-license-billing.md#license-vcores) and [License SQL Server instances by physical cores without VMs](manage-license-billing.md#license-pcores-without-vms) for details.
- If you use the unlimited virtualization licensing method, make sure the p-core license or licenses are created with `billingPlan` set to `Paid`, activated and all SQL Servers instances covered by the licenses are properly configured with `licenseType` set to `Paid`. See [License SQL Server instances by physical cores with unlimited virtualization](manage-license-billing.md#unlimited-virtualization) for details.

> [!IMPORTANT]
>
> When using the placement policy on Azure VMware Service to enable unlimited virtualization, ensure that the p-core license with `billingPlan` set to `Paid` is created in coordination with the Microsoft account team.

### On the license agreement expiration date

Change the license type value on all resources that are no longer covered by SQL Server license with Software assurance or SQL Server subscription. 

- To switch several Azure SQL resources to pay-as-you-go subscription, use the [Modify Azure SQL license type](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-hybrid-benefit/modify-license-type) PowerShell script. 

- To switch several Azure Arc-enabled SQL Server instances to pay-as-you-go subscription, use the [Modify Arc SQL  license type](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-arc-enabled-sql-server/modify-license-type) PowerShell script. 

These scripts are provided "as is" under the [MIT license](https://github.com/microsoft/sql-server-samples/blob/master/license.txt).

> [!IMPORTANT]
> If you are leveraging the unlimited virtualization benefit of SQL Server Software assurance or SQL Server subscription, make sure to switch the SQL Server license billing plan from `Paid` to `PAYG` before switching the individual VMs in scope of the license to `PAYG`. This way you can ensure that the VMs are not individually billed. For details, see [Change SQL Server license resource](manage-configuration.md#change-license-resource).

## Transition from License provided by SPLA vendor

Transitioning of the SQL Servers licensed through a Services Provider License Agreement (SPLA) to Azure pay-as-you-go billing requires that:

- The end customer SQL Servers are onboarded to Azure Arc in CSP-managed Azure subscriptions.
- Pay-as-you-go billing is selected with the consent to recurring billing on each connected machine.

When moving customers to Azure pay-as-you-go billing, it's essential to ensure that the Azure Connected Machine agent and the SQL Server extension are healthy and can connect to Azure. If the extension is broken, blocked by firewalls, or misconfigured (for example, proxy issues), it may stop reporting SQL Server usage correctly. This can result in underreported usage, billing errors, and noncompliance.

These issues also limit the functionality of SQL Servers connected via Azure Arc. Affected features include:

- Monitoring
- Inventory
- Entra ID authentication (which depends on a healthy agent and active connection)

Azure Arc-connected servers must check in with Azure at least once every 30 days. Starting June 2026, this check-in will be enforced. This ensures accurate usage reporting and prevents unauthorized SQL Server use.

## Customer consent

You or your cloud solutions provider must explicitly provide consent before you enabled pay-as-you-go billing. For details, review [Recurring billing consent](#recurring-billing-consent).

### Reconnection within 30 days

If the machine reconnects within 30 days of being disconnected, pay-as-you-go billing is based on the actual usage logs maintained locally by the extension.

### Disconnection beyond 30 days

If the machine remains disconnected for more than 30 days, the SQL Arc service switches to recurring billing based on the last known configuration. For example: edition, number of cores, high availability setup. Charges include:

- Backfill charges for the previous 30 days.
- Ongoing hourly charges until the machine reconnects.

These charges use separate recurring pay-as-you-go (`PAYG`) meters to track usage during the disconnected state.

### Intermittent use of SQL Server

If you have an application that's infrequently used and can be offline longer than 30 days, it will trigger recurring billing because the SQL Arc service cannot tell if the disconnection is intentional or not. To prevent billing, disconnect the SQL Server instance from Azure Arc. When the VM is up and running, you will need to onboard it to Arc again using any of the supported methods. For details, review [Disconnect SQL Server instances from Azure Arc](delete-from-azure-arc.md).

### Solution overview

Because the current Windows Server implementation enforces a fixed 30-day disconnection limit, the system automatically handles the re-onboarding of underlying servers without resetting SQL Server billing. This feature ensures that services like Extended Security Updates (ESU) aren't reset and that no new back-billing is triggered unnecessarily.

The following timeline illustrates the billing behavior for both SQL Server and Windows Server, comparing scenarios where the machine reconnects within 30 days versus after 30 days.

| Timeline | Event | Service's actions |
|----------|-------|-------------------|
| Day 1 | Sets up Arc + Arc SQL using pay-as-you-go on a Windows Server. | - Billing starts based on current configuration and actual usage.<br>- Usage is uploaded and processed every 12 hours. |
| Day 2 | Azure detects disconnection due to agent failure or blocked connectivity. | Connected machine state changes to **Disconnected**.<br>- Azure extension for SQL Server continues collecting and storing usage data locally.<br>- Warnings appear in the Arc machine Activity Log, SQL Server Configuration page, and SQL Server Overview blade.<br>- No hourly meters are emitted. |
| Day 3+ | Continued disconnection | Daily reminders are emitted, warning that recurring billing will begin after 30 days. |
| Day 30 | Still no usage records or heartbeat. | Connected machine agent's certificate expires.<br>- Connected machine state changes to Expired.<br>- SQL switches to recurring billing based on the last known configuration (edition, cores, HA setup, etc.).<br>- Charges are backfilled for the past 30 days.<br>- Hourly billing resumes using recurring meters. |
| Day 31+ | Continued disconnection. | Hourly billing continues indefinitely using the last known configuration. |
| Day 40 | Connectivity is restored | Hourly billing switches to regular pay-as-you-go meters and continues. |

If connectivity is restored within 30 days, pay-as-you-go billing resumes based on actual usage data collected by the Azure extension for SQL Server and reported through standard pay-as-you-go meters. If connectivity is restored after 30 days, billing continues based on the last known configuration of the SQL instance using recurring pay-as-you-go meters until the connection is re-established.

## Recurring billing consent

An explicit consent is required to select the pay-as-you-go billing for SQL Server in the CSP-managed Azure subscriptions.

Consent is recorded by adding a `ConsentToRecurringPAYG` property to the Azure extension for SQL Server resource. It consists of the two values:

- `Consented`: You agree to recurring billing.
- `ConsentTimestamp`: The UTC timestamp marking when the consent was granted. This timestamp is used by the Hybrid Data Service to determine when recurring billing goes into effect. After that time any disconnection longer than 30 days activates the recurring pay-as-you-go billing.  

> [!IMPORTANT]
>
> New pay-as-you-go subscriptions aren't allowed without the consent.
>
> Once registered, the consent property can't be changed without reinstalling the extension.

## Enable recurring pay-as-you-go at scale using Azure Automation

Cloud solution providers who manage large customer accounts can enable recurring pay-as-you-go billing on multiple machines with a script. A flexible Modify License Type PowerShell script performs the necessary configuration changes, including the registration of consent. For example, the following operation will switch all connected machines in the same tenant to pay-as-you-go:

```powershell
.\modify-license-type.ps1 -LicenseType PAYG -ConsentToRecurringPAYG Yes -Force 
```

Review the complete script in GitHub at [sql-server-samples modify-license-type](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-arc-enabled-sql-server/modify-license-type).

## Manage automatic deployment

SQL Server enabled by Azure Arc automatically installs Azure extension for SQL Server on any new connected machine and supports an option to [specify the license type as a subscription tag](manage-autodeploy.md#specify-license-type). Because these tags are generic and don't support the option to consent to recurring pay-as-you-go, these tags shouldn't be assigned to CSP-managed subscriptions.

## Manage extension health

With a pay-as-you-go subscription, the health of the extensions becomes a critical factor of your compliance as it collects the usage data and ensures the correct billing. The intermittent disconnections up to 30 days are allowed as the extension maintains a usage log on the machine, but it is your responsibility to ensure the extensions stay healthy. The Azure portal includes a [Health Dashboard](https://ms.portal.azure.com/#view/Microsoft_Azure_ArcCenterUX/ArcCenterMenuBlade/~/sqlServerHealthDashboard) providing the high level view of the extensions' state. For details of troubleshooting of the unhealthy extensions, see [Troubleshoot Azure extension for SQL Server](troubleshoot-extension.md).

## Analyze costs

After you transition to pay-as-you-go billing, you can view current and forecasted charges through [Microsoft Cost Management](/azure/cost-management-billing/cost-management-billing-overview). Upcoming charges for Azure Arc-enabled SQL Server pay-as-you-go aren't shown on SQL Server or Arc resource pages. All cost analysis and forecasting happens at the subscription level or higher.

### View forecasted charges

To view forecasted pay-as-you-go charges:

1. In the Azure portal, open **Cost Management** > **Cost analysis**.
1. Select the appropriate scope (subscription, management group, or resource group).
1. Confirm the chart shows both actual and forecasted costs.
   - Solid bars or lines represent actual costs
   - Shaded extensions represent forecasted costs based on historical usage trends
1. Set the date range to the current month to see projected month-end costs.

:::image type="content" source="media/manage-pay-as-you-go-transition/cost-analysis-forecasted-charges.png" alt-text="Screenshot of Microsoft Cost Management showing actual and forecasted charges." lightbox="media/manage-pay-as-you-go-transition/cost-analysis-forecasted-charges.png":::

### Filter for Arc SQL Server usage

To isolate Azure Arc-enabled SQL Server pay-as-you-go charges, apply these filters:

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

- Usage category breakdown (for example, ESU costs, pay-as-you-go costs)
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

[Recurring billing for SQL Server enabled by Azure Arc FAQ](faq.yml#recurring-pay-as-you-go-billing)

[Manage licensing and billing of SQL Server enabled by Azure Arc](manage-license-billing.md)
