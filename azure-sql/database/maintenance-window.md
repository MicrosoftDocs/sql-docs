---
title: Maintenance window
titleSuffix: Azure SQL Database 
description: Understand how the Azure SQL Database maintenance window can be configured.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: urosmil, scottkim, mathoma
ms.date: 03/27/2024
ms.service: azure-sql-database
ms.subservice: service-overview
ms.topic: conceptual
ms.custom:
  - references_regions
  - ignite-2023
  - azure-sql-split
monikerRange: "=azuresql||=azuresql-db"
---

# Maintenance window in Azure SQL Database
[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](maintenance-window.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/maintenance-window.md?view=azuresql-mi&preserve-view=true)

The maintenance window feature allows you to configure maintenance schedule for [Azure SQL Database](sql-database-paas-overview.md) and [Azure SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md) resources making impactful maintenance events predictable and less disruptive for your workload.

> [!NOTE]
> The maintenance window feature only protects from planned impact from upgrades or scheduled maintenance. It does not protect from all failover causes; exceptions that might cause short connection interruptions outside of a maintenance window include hardware failures, cluster load balancing, and database reconfigurations due to events like a change in database Service Level Objective.

[Advance notifications](advance-notifications.md) are available for databases configured to use a nondefault maintenance window. Advance notifications enable customers to configure notifications to be sent up to 24 hours in advance of any planned event.

## Overview

Azure periodically performs [planned maintenance](planned-maintenance.md) of SQL Database resources. During a maintenance event, databases are fully available but can be subject to short reconfigurations within availability Service Level Agreements (SLA) for [SQL Database](https://azure.microsoft.com/support/legal/sla/azure-sql-database).

Maintenance window is intended for production workloads that are not resilient to database reconfigurations and cannot absorb short connection interruptions caused by planned maintenance events. By choosing a maintenance window you prefer, you can minimize the impact of [planned maintenance](planned-maintenance.md) by scheduling it to occur outside of your peak business hours. Resilient workloads and nonproduction workloads can rely on Azure SQL's default maintenance policy.

The maintenance window is free of charge and can be configured on creation or for existing resources. It can be configured using the Azure portal, PowerShell, CLI, or Azure API.

> [!Important]
> Configuring maintenance window is a long running asynchronous operation, similar to changing the service tier of the Azure SQL resource. The resource is available during the operation, except a short reconfiguration that happens at the end of the operation and typically lasts up to 8 seconds even in case of interrupted long-running transactions. To minimize the impact of the reconfiguration you should perform the operation outside of the peak hours.

### Gain more predictability with maintenance window

By default, Azure SQL maintenance policy blocks most impactful updates during the period **8AM to 5PM local time every day** to avoid any disruptions during typical peak business hours. Local time is determined by the location of [Azure region](https://azure.microsoft.com/global-infrastructure/geographies/) that hosts the resource and might observe daylight saving time in accordance with local time zone definition.

During maintenance, databases remain available, but some updates may require a failover. The system default maintenance window (5pm to 8am) limits most activities to this time, but urgent updates may occur outside of it. To ensure all updates occur only during the maintenance window, select a non-default option.

You can adjust the window for maintenance updates to a time suitable to your Azure SQL resources by choosing from two non-default maintenance window slots:
 
- **Weekday** window: 10:00 PM to 6:00 AM local time, Monday - Thursday
- **Weekend** window: 10:00 PM to 6:00 AM local time, Friday - Sunday

Maintenance window days listed indicate the starting day of each eight-hour maintenance window. For example, "10:00 PM to 6:00 AM local time, Monday – Thursday" means that the maintenance windows start at 10:00 PM local time on each day (Monday through Thursday) and complete at 6:00 AM local time the following day (Tuesday through Friday).

Once the maintenance window selection is made and service configuration completed, planned maintenance occurs only during the window of your choice. While maintenance events typically complete within a single window, some of them might span two or more adjacent windows.

> [!NOTE]
> Azure SQL Database follows a safe deployment practice where Azure paired regions are guaranteed to be not deployed to at the same time. However, it is not possible to predict which region will be upgraded first, so the order of deployment is not guaranteed. Sometimes, your primary database will be upgraded first, and sometimes it would be secondary.
> 
> - In situations where your database is enabled for [geo-replication](active-geo-replication-overview.md) or [failover groups](failover-group-sql-db.md), and the geo-replication does not align with the [Azure region pairing](/azure/reliability/cross-region-replication-azure#azure-cross-region-replication-pairings-for-all-geographies), you should different maintenance window schedules for your primary and secondary database. For example, you can select **Weekday** maintenance window for your geo-secondary database and **Weekend** maintenance window for your geo-primary database.

> [!Important]
> In very rare circumstances where any postponement of action could cause serious impact, like applying critical security patch, configured maintenance window might be temporarily overriden.

## Advance notifications

Maintenance notifications can be configured to alert you of upcoming planned maintenance events for your Azure SQL Database. The alerts arrive 24 hours in advance, before maintenance window opens, and at the end of maintenance window. For more information, see [Advance Notifications](advance-notifications.md).

## Feature availability

### Supported subscription types

Configuring and using maintenance window is available for the following [offer types](https://azure.microsoft.com/support/legal/offer-details/): Pay-as-you-go, Cloud Solution Provider (CSP), Microsoft Enterprise Agreement, or Microsoft Customer Agreement.  

Offers restricted to dev/test usage only are not eligible (like pay-as-you-go Dev/Test or Enterprise Dev/Test as examples).

> [!Note]
> An Azure offer is the type of the Azure subscription you have. For example, a subscription with [pay-as-you-go rates](https://azure.microsoft.com/offers/ms-azr-0003p/), [Azure in Open](https://azure.microsoft.com/offers/ms-azr-0111p/), and [Visual Studio Enterprise](https://azure.microsoft.com/offers/ms-azr-0063p/) are all Azure offers. Each offer or plan has different terms and benefits. Your offer or plan is shown on the subscription's Overview. For more information on switching your subscription to a different offer, see [Change your Azure subscription to a different offer](/azure/cost-management-billing/manage/switch-azure-offer).

### Supported service level objectives

Choosing a maintenance window other than the default is available on all SLOs **except for** the following.

- SLOs not supported:
    - Azure SQL Database DTU Basic, S0 and S1 tiers
    - DC hardware
    - Fsv2 hardware

Maintenance window for Hyperscale elastic pools is in preview and is available in specific regions and configurations. For more information, see [Blog: Maintenance window support for Azure SQL Database Hyperscale elastic pools](https://aka.ms/hsep-fmw).

<!-- Check Known limitations in azure-sql/database/service-tier-hyperscale.md as well -->

### Azure SQL Database region support for maintenance windows

Choosing a maintenance window for Azure SQL Database other than the default is currently available in the following regions, organized by purchasing model.

The following table is for databases that are not [zone-redundant](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability). For databases in an [Azure Availability Zone](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability), see [the table for zone-redundant databases.](#ZR-maintenance-window-availability)

| Azure Region | Hyperscale premium-series and premium-series memory optimized | Hyperscale standard-series | All other Azure SQL Database purchasing models and tiers |
|:---|:---|:---|:---|
| Australia East | Yes | Yes | Yes |
| Australia Southeast | | Yes | Yes |
| Brazil South | | Yes | Yes |
| Brazil Southeast | | Yes | Yes |
| Canada Central  | Yes | Yes | Yes |
| Canada East  | | Yes | Yes |
| Central India | | Yes | Yes |
| Central US | Yes | Yes | Yes |
| China East 2 | | Yes | Yes |
| China North 2 | | Yes | Yes |
| East US 1 | Yes | Yes | Yes |
| East US 2  | Yes | Yes | Yes |
| East Asia  | | Yes | Yes |
| France Central  | | Yes | Yes |
| France South  | | Yes | Yes |
| Germany West Central | | Yes | Yes |
| Japan East | Yes | Yes | Yes |
| Japan West | | Yes | Yes |
| North Central US | | Yes | Yes |
| North Europe | Yes | Yes | Yes |
| South Africa North | | Yes | Yes |
| South Central US | Yes | Yes | Yes |
| South India | | Yes | Yes |
| Southeast Asia | | Yes | Yes |
| Switzerland North | | Yes | Yes |
| UAE North | | Yes | Yes |
| UK South | Yes | Yes | Yes |
| UK West | | Yes | Yes |
| US Gov Texas | | Yes | Yes |
| US Gov Virginia | | Yes | Yes |
| West Central US | | Yes | Yes |
| West Europe | Yes | Yes | Yes |
| West US | Yes | Yes | Yes |
| West US 2 | Yes | Yes | Yes |
| West US 3 | Yes | Yes | Yes |

<a id="ZR-maintenance-window-availability"></a>

The following table is for [zone-redundant](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability) databases.

| Azure Region  | Hyperscale premium-series and premium-series memory optimized | Hyperscale standard-series | All other Azure SQL Database purchasing models and tiers in an [Azure Availability Zone](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability) |
|:---|:---|:---|:---|
| Australia East | Yes | Yes | Yes |
| Canada Central  |  Yes | Yes | Yes |
| Central US |  Yes | Yes | Yes |
| East US 1  |  Yes | Yes | Yes |
| East US 2  | | | Yes |
| France Central | | Yes | Yes |
| Japan East | | | Yes |
| North Europe |  Yes | Yes | Yes |
| South Central US  | | | Yes |
| Southeast Asia | | |  Yes |
| UK South | | | Yes |
| West Europe |  Yes | Yes | Yes |
| West US 2 | | | Yes |
| West US 3 | Yes | Yes | Yes |

## Gateway maintenance

To get the maximum benefit from maintenance windows, make sure your client applications are using the redirect connection policy. Redirect is the recommended connection policy, where clients establish connections directly to the node hosting the database, leading to reduced latency and improved throughput.  

In Azure SQL Database, any connections using the proxy connection policy could be affected by both the chosen maintenance window and a gateway node maintenance window. However, client connections using the recommended redirect connection policy are unaffected by a gateway node maintenance reconfiguration.

For more on the client connection policy in Azure SQL Database, see [Azure SQL Database Connection policy](../database/connectivity-architecture.md#connection-policy).

## Retrieve list of maintenance events

[Azure Resource Graph](/azure/governance/resource-graph/overview) is an Azure service designed to extend Azure Resource Management. The Azure Resource Graph Explorer provides efficient and performant resource exploration with the ability to query at scale across a given set of subscriptions so that you can effectively govern your environment.

You can use the Azure Resource Graph Explorer to query for maintenance events. For an introduction on how to run these queries, see [Quickstart: Run your first Resource Graph query using Azure Resource Graph Explorer](/azure/governance/resource-graph/first-query-portal).

To check for the maintenance events for all SQL databases in your subscription, use the following sample query in Azure Resource Graph Explorer:

```kusto
servicehealthresources
| where type =~ 'Microsoft.ResourceHealth/events'
| extend impact = properties.Impact
| extend impactedService = parse_json(impact[0]).ImpactedService
| where  impactedService =~ 'SQL Database'
| extend eventType = properties.EventType, status = properties.Status, description = properties.Title, trackingId = properties.TrackingId, summary = properties.Summary, priority = properties.Priority, impactStartTime = todatetime(tolong(properties.ImpactStartTime)), impactMitigationTime = todatetime(tolong(properties.ImpactMitigationTime))
| where eventType == 'PlannedMaintenance'
| order by impactStartTime desc
```

For the full reference of the sample queries and how to use them across tools like PowerShell or Azure CLI, visit [Azure Resource Graph sample queries for Azure Service Health](/azure/service-health/resource-graph-samples).

## Related content

- [Configure maintenance window](maintenance-window-configure.md)
- [Advance notifications for planned maintenance events](advance-notifications.md)
- [Maintenance window FAQ](maintenance-window-faq.yml)
- [Azure SQL Database](sql-database-paas-overview.md)
- [Plan for Azure maintenance events in Azure SQL Database and Azure SQL Managed Instance](planned-maintenance.md)
