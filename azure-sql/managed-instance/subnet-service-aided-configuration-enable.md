---
title: Service-Aided Subnet Configuration
description: Learn how you can enable the service-aided subnet configuration for Azure SQL Managed Instance with subnet delegation.
author: zoran-rilak-msft
ms.author: zoranrilak
ms.reviewer: vanto
ms.date: 09/11/2025
ms.service: azure-sql-managed-instance
ms.subservice: deployment-configuration
ms.topic: how-to
ms.custom:
  - ignite-2023
---
# Enable service-aided subnet configuration for Azure SQL Managed Instance

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article provides an overview of service-aided subnet configuration and how it interacts with the subnets delegated to Azure SQL Managed Instance. Service-aided subnet configuration automates network configuration management for SQL managed instance subnets. This mechanism leaves the user fully in control of access to the data while the SQL managed instance assumes responsibility for the uninterrupted flow of management traffic.

## Overview

To improve service security, manageability, and availability, SQL Managed Instance automates the management of certain critical network pathways inside the user's subnet. The service configures the subnet, its associated network security group, and route table to contain a set of required entries.

The mechanism behind this behavior is called network intent policy. A network intent policy is automatically applied to the subnet when the subnet is first [delegated](/azure/virtual-network/subnet-delegation-overview) to Azure SQL Managed Instance's resource provider `Microsoft.Sql/managedInstances`. At that point, the automatic configuration takes effect. When you delete the last SQL managed instance from a subnet, the network intent policy is also removed from that subnet.

## The effect of network intent policy on the delegated subnet

A network intent policy extends the route table and network security group associated with the subnet by adding *mandatory* and *optional* rules and routes.

A network intent policy doesn't prevent you from updating most of the subnet's configuration. When you change the subnet's route table or update its network security group rules, the associated network intent policy checks if the effective routes and security rules comply with the requirements for Azure SQL Managed Instance. If they don't, the network intent policy raises an error, preventing the change in configuration.

This behavior stops when you remove the last SQL managed instance from the subnet and the network intent policy is detached. It can't be turned off while SQL managed instances are present in the subnet.

Consider the following points: 

- **We advise you maintain a separate route table and NSG for each delegated subnet.** Autoconfigured rules and routes reference the specific subnet ranges that might overlap with those in another subnet. When you reuse RTs and NSGs across multiple subnets delegated to Azure SQL Managed Instance, autoconfigured rules stack and might interfere with the rules governing unrelated traffic.
- **We advise against taking dependency on any of the service-managed rules and routes.** As a rule, always create explicit routes and NSG rules for your particular purposes. Both the mandatory and optional rules are subject to change.
- Similarly, **we advise against updating the service-managed rules.** Because the network intent policy only checks for *effective* rules and routes, it's possible to extend one of the autoconfigured rules. For example, to open more ports for inbound traffic or to extend routing to a broader prefix. However, service-configured rules and routes might change. It's best to create your own routes and security rules to achieve the desired outcome.

### Mandatory security rules and routes

To ensure uninterrupted management connectivity for SQL Managed Instance, some security rules and routes are mandatory and can't be removed or modified.

The names of mandatory rules and routes always begin with `Microsoft.Sql-managedInstances_UseOnly_mi-`. This prefix is reserved for Azure SQL Managed Instance's use. **Do not use this prefix when updating your route table and NSG.** Service updates might delete all rules and routes with that prefix, after which only the mandatory ones will be recreated.

The following table lists the mandatory rules and routes that are automatically deployed to and enforced on the user's subnet:

| Kind | Name | Description |
| ---- | ---- | ----------- |
| NSG inbound | Microsoft.Sql-managedInstances_UseOnly_mi-healthprobe-in | Allows inbound health probes from the associated load balancer to reach instance nodes. This mechanism allows the load balancer to keep track of active database replicas after a failover. |
| NSG inbound |Microsoft.Sql-managedInstances_UseOnly_mi-internal-in | Ensures internal node connectivity required for management operations. |
| NSG outbound | Microsoft.Sql-managedInstances_UseOnly_mi-internal-out | Ensures internal node connectivity required for management operations. |
| Route | Microsoft.Sql-managedInstances_UseOnly_mi-subnet-*\<range\>*-to-vnetlocal | Ensures there's always a route for the internal nodes to reach each other. |

> [!NOTE]  
> Some subnets might contain additional network security rules and routes that use the `Microsoft.Sql-managedInstances_UseOnly_mi-` prefix. These rules and routes are also mandatory when present, but might be removed in a future service update.

### Optional security rules and routes

Some rules and routes are optional and can be safely removed without impairing the internal management connectivity of SQLmanaged instances.

> [!IMPORTANT]  
> **Optional rules and routes will be retired in a future service update.** We advise you to update your deployment and network configuration procedures such that each deployment of Azure SQL Managed Instance in a new subnet is followed with an explicit removal and/or replacement of the optional rules and routes.

To help differentiate optional from mandatory rules and routes, the names of optional rules and routes always begin with `Microsoft.Sql-managedInstances_UseOnly_mi-optional-`.

The following table lists the optional rules and routes that can be modified or removed:

| Kind | Name | Description |
| ---- | ---- | ----------- |
| NSG outbound | Microsoft.Sql-managedInstances_UseOnly_mi-optional-azure-out | Optional security rule to preserve outbound HTTPS connectivity to Azure. |
| Route | Microsoft.Sql-managedInstances_UseOnly_mi-optional-AzureCloud._\<region\>_ | Optional route to AzureCloud services in the primary region. |
| Route | Microsoft.Sql-managedInstances_UseOnly_mi-optional-AzureCloud._\<geo-paired\>_ | Optional route to AzureCloud services in the secondary region. |

<a id="removing-the-network-intent-policy"></a>

## Remove the network intent policy

The effect of a network intent policy on the subnet stops when there are no more [Virtual clusters](virtual-cluster-architecture.md) inside and the delegation is removed. For the details of the lifecycle of the virtual cluster, see how to [delete a subnet after deleting SQL Managed Instance](virtual-cluster-architecture.md#delete-a-subnet-after-deleting-an-azure-sql-managed-instance).

## Related content

- [Connectivity architecture for Azure SQL Managed Instance](connectivity-architecture-overview.md)
- [Virtual cluster architecture - Azure SQL Managed Instance](virtual-cluster-architecture.md)
