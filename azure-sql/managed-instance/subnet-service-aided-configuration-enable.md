---
title: Enabling service-aided subnet configuration for Azure SQL Managed Instance
description: Enabling service-aided subnet configuration for Azure SQL Managed Instance
author: zoran-rilak-msft
ms.author: zoranrilak
ms.date: 03/25/2022
ms.reviewer: mathoma
ms.service: sql-managed-instance
ms.subservice: deployment-configuration
ms.topic: how-to
ms.custom: ignite-2023
---
# Enabling service-aided subnet configuration for Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

Service-aided subnet configuration provides automated network configuration management for subnets hosting managed instances. With service-aided subnet configuration user stays in full control of access to data (TDS traffic flows) while managed instance takes responsibility to ensure uninterrupted flow of management traffic in order to fulfill SLA.

Automatically configured network security groups and route table rules are visible to customer and annotated with prefix `Microsoft.Sql-managedInstances_UseOnly_.

Service-aided configuration is enabled automatically once you turn on [subnet-delegation](/azure/virtual-network/subnet-delegation-overview) for `Microsoft.Sql/managedInstances` resource provider.

> [!IMPORTANT] 
> Once subnet-delegation is turned on, you can't turn it off until the virtual cluster is removed from the subnet. For lifetime details of the virtual cluster, see how to [delete a subnet after deleting SQL Managed Instance](virtual-cluster-architecture.md#delete-a-subnet-after-deleting-an-azure-sql-managed-instance).


## Enabling subnet-delegation for new deployments

To deploy a managed instance to an empty subnet, you need to delegate it to the `Microsoft.Sql/managedInstances` resource provider as described in [Manage subnet delegation](/azure/virtual-network/manage-subnet-delegation). _The referenced article uses `Microsoft.DBforPostgreSQL/serversv2` resource provider as an example but you need to use the `Microsoft.Sql/managedInstances` resource provider instead._

## Enabling subnet-delegation for existing deployments

In order to enable subnet-delegation for your existing managed instance deployment, you need to find out virtual network subnet where it is placed. 

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
