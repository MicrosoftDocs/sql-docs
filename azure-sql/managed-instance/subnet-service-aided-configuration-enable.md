---
title: Service-aided subnet configuration
description: Learn how you can enable the service-aided subnet configuration for Azure SQL Managed Instance with subnet delegation.
author: zoran-rilak-msft
ms.author: zoranrilak
ms.reviewer: mathoma
ms.date: 07/10/2024
ms.service: azure-sql-managed-instance
ms.subservice: deployment-configuration
ms.topic: how-to
ms.custom:
  - ignite-2023
---
# Enable service-aided subnet configuration for Azure SQL Managed Instance
[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article provides an overview of service-aided subnet configuration and how it interacts with the subnets delegated to Azure SQL Managed Instance. Service-aided subnet configuration automates network configuration management for subnets that host managed instances, leaving the user fully in control of access to the data (TDS traffic flows) while the managed instance is responsible for ensuring uninterrupted flow of management traffic.

## Overview

To improve service security, manageability, and availability, SQL Managed Instance automates the management of certain critical network pathways inside the user's subnet. This is achieved by configuring the subnet, its associated network security group, and route table to contain a set of required entries.

The mechanism that accomplishes this is called network intent policy. A network intent policy is automatically applied to the subnet when it is first [delegated](/azure/virtual-network/subnet-delegation-overview) to Azure SQL Managed Instance's resource provider `Microsoft.Sql/managedInstances`. At that point, the automatic configuration takes effect. When you delete the last managed instance from a subnet, the network intent policy is also removed from that subnet.

## The effect of network intent policy on the delegated subnet

When applied to a subnet, the network intent policy will extend the route table and network security group associated with the subnet by adding mandatory and optional rules and routes. 

While applied to a subnet, a network intent policy will not prevent you from updating most of the subnet's configuration. Whenever you change the subnet's route table or update its network security group rules, the  network intent policy will check whether the effective routes and security rules comply with the requirements for Azure SQL Managed Instance. If they don't, the network intent policy will cause an error and prevent you from updating the configuration.

This behavior stops when you remove the last managed instance from the subnet and the network intent policy is detached. It cannot be turned off while managed instances are present in the subnet.

>[!NOTE]
>- **We advise you maintain a separate route table and NSG for each delegated subnet.** Auto-configured rules and routes reference the specific subnet ranges that may exist in another subnet. When you reuse RTs and NSGs across multiple subnets delegated to Azure SQL Managed Instance, auto-configured rules will stack and may interfere with the rules governing unrelated traffic.
>- **We advise against taking dependency on any of the service-managed rules and routes.** As a rule, always create explicit routes and NSG rules for your particular purposes. Both the mandatory and optional rules are subject to change.
>- Similarly, **we advise against updating the service-managed rules.** Because the network intent policy only checks for *effective* rules and routes, it is possible to extend one of the auto-configured rules, for example to open additional ports for inbound or to extend routing to a broader prefix. However, service-configured rules and routes may change. It is best to create your own routes and security rules to achieve the desired outcome.

### Mandatory security rules and routes

To ensure uninterrupted management connectivity for SQL Managed Instance, some security rules and routes are mandatory and can't be removed or modified.

Mandatory rules and routes always begin with `Microsoft.Sql-managedInstances_UseOnly_mi-`.

The following table lists the mandatory rules and routes that are enforced and automatically deployed to the user's subnet:

| Kind | Name | Description |
| ---- | ---- | ----------- |
| NSG inbound | Microsoft.Sql-managedInstances_UseOnly_mi-healthprobe-in | Allows inbound health probes from the associated load balancer to reach instance nodes. This mechanism allows the load balancer to keep track of active database replicas after a failover. |
| NSG inbound |Microsoft.Sql-managedInstances_UseOnly_mi-internal-in | Ensures internal node connectivity required for management operations. |
| NSG outbound | Microsoft.Sql-managedInstances_UseOnly_mi-internal-out | Ensures internal node connectivity required for management operations. |
| Route | Microsoft.Sql-managedInstances_UseOnly_mi-subnet-_\<range\>_-to-vnetlocal | Ensures there's always a route for the internal nodes to reach each other. |

> [!NOTE]
> Some subnets contain additional mandatory network security rules and routes that aren't listed in either of the above two sections. Such rules are considered obsolete and will be removed from their subnets.

### Optional security rules and routes

Some rules and routes are optional and can be safely removed without impairing the internal management connectivity of managed instances. These optional rules are used to preserve outbound connectivity of managed instances that are deployed with the assumption that the full complement of the mandatory rules and routes will still be in place.

> [!IMPORTANT]
> **Optional rules and routes will be deprecated in the future.** We strongly advise you to update your deployment and network configuration procedures such that each deployment of Azure SQL Managed Instance in a new subnet is followed with an explicit removal and/or replacement of the optional rules and routes, such that only the minimal required traffic is allowed to flow.

To help differentiate optional from mandatory rules and routes, the names of optional rules and routes always begin with `Microsoft.Sql-managedInstances_UseOnly_mi-optional-`.

The following table lists the optional rules and routes that can be modified or removed:

| Kind | Name | Description |
| ---- | ---- | ----------- |
| NSG outbound | Microsoft.Sql-managedInstances_UseOnly_mi-optional-azure-out | Optional security rule to preserve outbound HTTPS connectivity to Azure. |
| Route | Microsoft.Sql-managedInstances_UseOnly_mi-optional-AzureCloud._\<region\>_ | Optional route to AzureCloud services in the primary region. |
| Route | Microsoft.Sql-managedInstances_UseOnly_mi-optional-AzureCloud._\<geo-paired\>_ | Optional route to AzureCloud services in the secondary region. |

## Removing the network intent policy

The effect of network intent policy on the subnet stops when there are no more [virtual clusters](virtual-cluster-architecture.md) inside and the delegation is removed. For the details of the lifecycle  of the virtual cluster, see how to [delete a subnet after deleting SQL Managed Instance](virtual-cluster-architecture.md#delete-a-subnet-after-deleting-an-azure-sql-managed-instance).

## Related content

- [Connectivity architecture](connectivity-architecture-overview.md)
- [Virtual cluster architecture](virtual-cluster-architecture.md)
