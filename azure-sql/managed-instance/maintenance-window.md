---
title: Maintenance window
titleSuffix: Azure SQL Managed Instance
description: Understand how the Azure SQL Managed Instance maintenance window can be configured.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma, urosmil, scottkim
ms.date: 05/27/2024
ms.service: sql-managed-instance
ms.subservice: service-overview
ms.topic: conceptual
ms.custom:
  - references_regions
  - ignite-2023
  - azure-sql-split
monikerRange: "=azuresql||=azuresql-mi"
---

# Maintenance window in Azure SQL Managed Instance
[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/maintenance-window.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](maintenance-window.md?view=azuresql-mi&preserve-view=true)

The maintenance window feature allows you to configure maintenance schedule for [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md?view=azuresql-mi&preserve-view=true) resources making impactful maintenance events predictable and less disruptive for your workload.

> [!NOTE]
> The maintenance window feature only protects from planned impact from upgrades or scheduled maintenance. It does not protect from all failover causes; exceptions that might cause short connection interruptions outside of a maintenance window include hardware failures, and other reconfigurations.

Advance notifications enable customers to configure notifications to be sent up to 24 hours in advance of any planned event.

## Overview

Azure periodically performs [planned maintenance](../database/planned-maintenance.md) of SQL managed instance resources. During a maintenance event, SQL managed instances are fully available but can be subject to short reconfigurations within availability Service Level Agreements (SLA) for [SQL managed instance](https://azure.microsoft.com/support/legal/sla/azure-sql-sql-managed-instance).

Maintenance window is intended for production workloads that are not resilient to instance reconfigurations and cannot absorb short connection interruptions caused by planned maintenance events. By choosing a maintenance window you prefer, you can minimize the impact of [planned maintenance](../database/planned-maintenance.md) by scheduling it to occur outside of your peak business hours. Resilient workloads and nonproduction workloads can rely on Azure SQL's default maintenance policy.

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

> [!IMPORTANT]
> Azure SQL Managed Instance follows a safe deployment practice where Azure paired regions are guaranteed to be not deployed to at the same time. However, it is not possible to predict which region will be upgraded first, so the order of deployment is not guaranteed. Sometimes, your primary instance will be upgraded first, and sometimes it would be secondary.
>
> - In situations where your SQL managed instance has [failover groups](failover-group-sql-mi.md), and the groups are not aligned with the [Azure region pairing](/azure/reliability/cross-region-replication-azure#azure-cross-region-replication-pairings-for-all-geographies), **you should choose different maintenance window schedules for your primary and secondary SQL managed instance**. For example, you can select **Weekday** maintenance window for your geo-secondary and **Weekend** maintenance window for your geo-primary SQL managed instance.
>
> - In very rare circumstances where any postponement of action could cause serious impact, like applying critical security patch, configured maintenance window might be temporarily overriden.

## Advance notifications

Maintenance notifications can be configured to alert you of upcoming planned maintenance events for your Azure SQL Managed Instance. The alerts arrive 24 hours in advance, before maintenance window opens, and at the end of maintenance window. For more information, see [Advance Notifications](advance-notifications.md).

## Feature availability

### Supported subscription types

Configuring and using maintenance window is available for the following [offer types](https://azure.microsoft.com/support/legal/offer-details/): Pay-as-you-go, Cloud Solution Provider (CSP), Microsoft Enterprise Agreement, or Microsoft Customer Agreement. 

Offers restricted to dev/test usage only are not eligible (like pay-as-you-go Dev/Test or Enterprise Dev/Test as examples).

> [!Note]
> An Azure offer is the type of the Azure subscription you have. For example, a subscription with [pay-as-you-go rates](https://azure.microsoft.com/offers/ms-azr-0003p/), [Azure in Open](https://azure.microsoft.com/offers/ms-azr-0111p/), and [Visual Studio Enterprise](https://azure.microsoft.com/offers/ms-azr-0063p/) are all Azure offers. Each offer or plan has different terms and benefits. Your offer or plan is shown on the subscription's Overview. For more information on switching your subscription to a different offer, see [Change your Azure subscription to a different offer](/azure/cost-management-billing/manage/switch-azure-offer).

### Supported service level objectives

Choosing a maintenance window other than the default is available on all SLOs **except for** Azure SQL Managed Instance pools.

### Azure SQL Managed Instance region support for maintenance windows

Choosing a maintenance window for Azure SQL Managed Instance other than the default is available in all regions.

## Gateway maintenance

In Azure SQL Managed Instance, the gateway nodes are hosted [within the virtual cluster](virtual-cluster-architecture.md) and have the same maintenance window as the SQL managed instance.

> [!IMPORTANT]
> The **redirect connection policy** is recommended to minimize number of disruptions during the maintenance event, see [connection types](connection-types-overview.md).

## Considerations for Azure SQL Managed Instance

Azure SQL Managed Instance consists of service components hosted on a dedicated set of isolated virtual machines that run inside the subnet of a customer's virtual network. These virtual machines are organized in groups to form a [virtual cluster](virtual-cluster-architecture.md) that can host multiple managed instances. Since a maintenance window configured for instances in the same subnet can influence the number of virtual machine groups within the virtual cluster and virtual cluster management operations, there are a few things to consider before configuring the maintenance window. 

### Maintenance window configuration is a long running operation

All instances hosted in the same virtual machine group share the same maintenance window. By default, all managed instances are hosted in a group with a default maintenance window. If you specify another maintenance window, either while you're creating the instance, or after it's already created, the instance is placed into a separate machine group with a corresponding maintenance window. If no such group exists in the cluster, a new one is created to accommodate the new configuration of the instance. If you configure additional instances in the virtual cluster to use the same maintenance window, those instances are also added to the group, which means the group might need to be resized. Adding instances to a new machine group, and resizing existing machine groups, might increase the duration of the operation to configure a maintenance window.  

Expected duration to configure a maintenance window for a managed instance can be calculated using the [estimated duration of instance management operations](management-operations-overview.md#duration).

> [!Important]
> When you configure a maintenance window, the final step of the operation requires a reconfiguration of the instance that typically lasts up to 8 seconds, even if it interrupts long-running transactions. To minimize impact, configure a maintenance window outside of peak business hours. 

### IP address space requirements

Each new virtual machine group in a subnet requires additional IP addresses according to the [virtual cluster IP address allocation](vnet-subnet-determine-size.md#determine-subnet-size). Changing a maintenance window for an existing managed instance also requires [temporary additional IP capacity](vnet-subnet-determine-size.md#update-scenarios), similar to when scaling the number of vCores for the respective service tier. 

### IP address change

Configuring or changing a maintenance window changes the IP address of the instance to a different IP address within the IP address range of the subnet.

> [!Important]
> Make sure that network security group (NSG) and firewall rules don't block data traffic after an IP address change.

### Serialization of virtual cluster management operations

Operations that affect the virtual cluster, such as service upgrades or resizing the virtual cluster (like adding new or removing unused compute nodes), are serialized. As such, a new virtual cluster operation can't start until the previous operation completes. If the maintenance window closes before the ongoing maintenance operation completes, the ongoing maintenance operation is put on hold until the next maintenance window. Other management operations submitted during that time are also put on hold, and resume during or after the next maintenance window after the original ongoing maintenance operation completes. It's not common for a maintenance operation to take longer than a single maintenance window per virtual machine group within a cluster, but it can happen for very complex maintenance operations.

The serialization of virtual cluster management operations is a general behavior that applies to the default maintenance policy as well. When you configure a maintenance window schedule, the period between two adjacent windows can be a few days long. While rare, if the maintenance operation spans two windows, newly submitted operations can be on hold for several days, potentially blocking operations that require additional compute nodes, such as creating a new, or resizing an existing, instance.  

## Retrieve list of maintenance events

[Azure Resource Graph](/azure/governance/resource-graph/overview) is an Azure service designed to extend Azure Resource Management. The Azure Resource Graph Explorer provides efficient and performant resource exploration with the ability to query at scale across a given set of subscriptions so that you can effectively govern your environment.

You can use the Azure Resource Graph Explorer to query for maintenance events. For an introduction on how to run these queries, see [Quickstart: Run your first Resource Graph query using Azure Resource Graph Explorer](/azure/governance/resource-graph/first-query-portal).

To check for the maintenance events for all SQL managed instances in your subscription, use the following sample query in Azure Resource Graph Explorer:

```kusto
servicehealthresources
| where type =~ 'Microsoft.ResourceHealth/events'
| extend impact = properties.Impact
| extend impactedService = parse_json(impact[0]).ImpactedService
| where  impactedService =~ 'SQL Managed Instance'
| extend eventType = properties.EventType, status = properties.Status, description = properties.Title, trackingId = properties.TrackingId, summary = properties.Summary, priority = properties.Priority, impactStartTime = todatetime(tolong(properties.ImpactStartTime)), impactMitigationTime = todatetime(tolong(properties.ImpactMitigationTime))
| where eventType == 'PlannedMaintenance'
| order by impactStartTime desc
```

For the full reference of the sample queries and how to use them across tools like PowerShell or Azure CLI, visit [Azure Resource Graph sample queries for Azure Service Health](/azure/service-health/resource-graph-samples).

## Related content

- [Configure maintenance window](maintenance-window-configure.md)
- [Advance notifications for planned maintenance events (Preview)](advance-notifications.md)
- [Maintenance window FAQ](maintenance-window-faq.yml)
- [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md)
- [Plan for Azure maintenance events in Azure SQL Database and Azure SQL Managed Instance](../database/planned-maintenance.md)
