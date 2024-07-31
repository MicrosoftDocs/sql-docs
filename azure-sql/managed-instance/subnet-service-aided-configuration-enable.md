---
title: Enable service-aided subnet configuration
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

This article provides an overview of the service-aided subnet configuration and how to enable it with subnet delegation for Azure SQL Managed Instance.

A service-aided subnet configuration automates network configuration management for subnets that host managed instances, leaving the user fully in control of access to the data (TDS traffic flows) while the managed instance is responsible for ensuring uninterrupted flow of management traffic in order to fulfill service-level agreements.

## Overview

To improve service security, manageability, and availability, SQL Managed Instance applies a network intent policy to elements of the Azure virtual network infrastructure. The policy configures the subnet, as well as the associated network security group and route table, to ensure meeting minimum requirements for SQL Managed Instance. This platform mechanism transparently communicates networking requirements to users when they attempt a configuration that fails to meet minimal requirements. The policy prevents network misconfiguration, and helps to maintain normal SQL Managed Instance operations and service-level agreements (SLAs). When you delete the last managed instance from a subnet, the network intent policy is also removed from that subnet.

Service-aided subnet configuration builds on top of the virtual network [subnet delegation](/azure/virtual-network/subnet-delegation-overview) feature to provide automatic network configuration management. The service-aided subnet configuration is automatically enabled when you turn on subnet delegation for the `Microsoft.Sql/managedInstances` resource provider.

You can use service endpoints to configure virtual network firewall rules for storage accounts that contain backups and audit logs. However, even with service endpoints enabled, customers are encouraged to use [Azure Private Link](/azure/private-link/private-link-overview) to access their storage accounts as the Private Link provides more isolation than service endpoints.

> [!IMPORTANT]
> - Once subnet delegation is turned on, you can't turn it off until the [virtual cluster](virtual-cluster-architecture.md) is removed from the subnet. For lifetime details of the virtual cluster, see how to [delete a subnet after deleting SQL Managed Instance](virtual-cluster-architecture.md#delete-a-subnet-after-deleting-an-azure-sql-managed-instance).
> - Due to control plane configuration specifics, service endpoints aren't available in national clouds.

## Enable subnet delegation for new deployments

To deploy a managed instance to an empty subnet, you need to delegate it to the `Microsoft.Sql/managedInstances` resource provider as described in [Manage subnet delegation](/azure/virtual-network/manage-subnet-delegation). _The referenced article uses `Microsoft.DBforPostgreSQL/serversv2` resource provider as an example but you need to use the `Microsoft.Sql/managedInstances` resource provider instead._

## Enable subnet delegation for existing deployments

In order to enable subnet-delegation for your existing managed instance deployment, you need to find out where the virtual network subnet where it's placed. 

To find the subnet, check the value under **Virtual network/subnet** on the **Overview** page of your SQL Managed Instance resource in the [Azure portal](https://portal.azure.com).

Alternatively, you could run the following PowerShell commands to find the virtual network subnet for your instance. Replace the following values in the sample:

- **subscription-id** with your subscription ID
- **rg-name** with the resource group for your managed instance
- **mi-name** with the name of your managed instance

```powershell
Install-Module -Name Az

Import-Module Az.Accounts
Import-Module Az.Sql

Connect-AzAccount

# Use your subscription ID in place of subscription-id below

Select-AzSubscription -SubscriptionId {subscription-id}

# Replace rg-name with the resource group for your managed instance, and replace mi-name with the name of your managed instance

$mi = Get-AzSqlInstance -ResourceGroupName {rg-name} -Name {mi-name}

$mi.SubnetId
```

Once you determine the managed instance subnet, you need to delegate it to the `Microsoft.Sql/managedInstances` resource provider as described in [Manage subnet delegation](/azure/virtual-network/manage-subnet-delegation). _While the referenced article uses the `Microsoft.DBforPostgreSQL/serversv2` resource provider as an example, you need to use the `Microsoft.Sql/managedInstances` resource provider instead._


> [!IMPORTANT]
> Enabling service-aided configuration doesn't cause failover or interruption in connectivity for managed instances that are already in the subnet.

## Mandatory security rules and routes

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


## Optional security rules and routes

Some rules and routes are optional and can be safely modified or removed without impairing the internal management connectivity of managed instances. These optional rules are used to preserve outbound connectivity of managed instances that are deployed with the assumption that the full complement of the mandatory rules and routes will still be in place.

> [!IMPORTANT]
> **Optional rules and routes will be deprecated in the future.** We strongly advise you to update your deployment and network configuration procedures such that each deployment of Azure SQL Managed Instance in a new subnet is followed with an explicit removal and/or replacement of the optional rules and routes, such that only the minimal required traffic is allowed to flow.

To help differentiate optional from mandatory rules and routes, the names of optional rules and routes always begin with `Microsoft.Sql-managedInstances_UseOnly_mi-optional-`.

The following table lists the optional rules and routes that can be modified or removed:

| Kind | Name | Description |
| ---- | ---- | ----------- |
| NSG outbound | Microsoft.Sql-managedInstances_UseOnly_mi-optional-azure-out | Optional security rule to preserve outbound HTTPS connectivity to Azure. |
| Route | Microsoft.Sql-managedInstances_UseOnly_mi-optional-AzureCloud._\<region\>_ | Optional route to AzureCloud services in the primary region. |
| Route | Microsoft.Sql-managedInstances_UseOnly_mi-optional-AzureCloud._\<geo-paired\>_ | Optional route to AzureCloud services in the secondary region. |

## Related content

- [Connectivity architecture](connectivity-architecture-overview.md)
- [Virtual cluster architecture](virtual-cluster-architecture.md)
