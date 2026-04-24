---
title: Move SQL Server license agreement to pay-as-you-go subscription
description: How to transition SQL Server from the legacy licensing model to Azure pay-as-you-go subscription. 
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest, maghan
ms.date: 04/01/2026
ai-usage: ai-assisted
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

## Linux-specific considerations

For Linux-specific limitations that apply to PAYG subscription, see [PAYG on Linux](pay-as-you-go-subscription.md#payg-on-linux).

### Connectivity and disconnection billing

For details about how billing works during disconnections, including reconnection within 30 days, disconnection beyond 30 days, intermittent use scenarios, and a timeline of billing events, see [Connectivity requirements](pay-as-you-go-subscription.md#connectivity-requirements).

## Recurring billing consent

For details about recurring billing consent, see [Recurring billing consent](pay-as-you-go-subscription.md#recurring-billing-consent).

## Enable recurring pay-as-you-go at scale using Azure Automation

Cloud solution providers who manage large customer accounts can enable recurring pay-as-you-go billing on multiple machines with a script. A flexible Modify License Type PowerShell script performs the necessary configuration changes, including the registration of consent. For example, the following operation will switch all connected machines in the same tenant to pay-as-you-go:

```powershell
.\modify-license-type.ps1 -LicenseType PAYG -ConsentToRecurringPAYG Yes -Force 
```

Review the complete script in GitHub at [sql-server-samples modify-license-type](https://github.com/microsoft/sql-server-samples/tree/master/samples/manage/azure-arc-enabled-sql-server/modify-license-type).

## Manage automatic deployment

SQL Server enabled by Azure Arc automatically installs Azure extension for SQL Server on any new connected machine and supports an option to [specify the license type as a subscription tag](manage-autodeploy.md#specify-license-type). Because these tags are generic and don't support the option to consent to recurring pay-as-you-go, these tags shouldn't be assigned to CSP-managed subscriptions.

## Manage extension health

For details about managing extension health for billing compliance, see [Manage extension health](pay-as-you-go-subscription.md#manage-extension-health).

## Analyze costs

For details about analyzing PAYG subscription costs, including viewing forecasted charges, filtering for Arc SQL Server usage, identifying resource-level costs, and setting up budget alerts, see [Analyze costs](pay-as-you-go-subscription.md#analyze-costs).

## Monitor billing events

For details about monitoring billing events, see [Monitor billing events](pay-as-you-go-subscription.md#monitor-billing-events).


## Related content

[Recurring billing for SQL Server enabled by Azure Arc FAQ](faq.yml#recurring-pay-as-you-go-billing)

[Manage licensing and billing of SQL Server enabled by Azure Arc](manage-license-billing.md)
