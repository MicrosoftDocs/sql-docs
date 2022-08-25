---
title: Resolve private domain names
titleSuffix: Azure SQL Managed Instance 
description: This topic describes how to get Azure SQL Managed Instance to resolve private domain names.
services: sql-database
ms.service: sql-managed-instance
ms.subservice: deployment-configuration
ms.custom: sqldbrb=1
ms.devlang: 
ms.topic: conceptual
author: zoran-rilak-msft
ms.author: zoranrilak
ms.reviewer: mathoma, srbozovi
ms.date: 08/25/2022
---
# Get Azure SQL Managed Instance to resolve private domain names
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

In certain situations it is necessary for a SQL Server to resolve domain names that do not exist in public DNS records. Some scenarios where this is called for include:
* Sending emails via [Database Mail](/../sql/relational-databases/database-mail/database-mail.md)
* Accessing remote data sources via [Linked Servers](/../sql/relational-databases/linked-servers/linked-servers-database-engine.md)
* Replicating data to the cloud via [Managed Instance Link feature](managed-instance-link-feature-overview.md).

Azure SQL Managed Instance is deployed in an Azure [virtual network (VNet)](/azure/virtual-network/virtual-networks-overview.md) and uses Azure-provided name resolution by default to resolve Internet addresses. To change this and enable the resolution of private domain names, you can:

1. Use Azure private DNS zones: [What is Azure Private DNS?](/azure/dns/private-dns-overview), or
2. Use a custom DNS server: [Name resolution for resources in Azure virtual networks](/azure/virtual-network/virtual-networks-name-resolution-for-vms-and-role-instances.md#name-resolution-that-uses-your-own-dns-server)

## Important notes

* Always use a fully qualified domain name (FQDN) for the services that you want Azure SQL Managed Instance to resolve, such as your mail server or an on-prem SQL Server instance. Use FQDNs even if those services are within your private DNS zone. For example, use `smtp.contoso.com`. Creating a linked server or replication that references SQL Server VMs inside the same virtual network also requires an FQDN and a default DNS suffix; for example, `SQLVM.internal.cloudapp.net`.
* When you configure a custom DNS server, you need to be careful not to override or disable the resolution of domain names that Azure SQL Managed Instance uses internally. To prevent this, always configure your custom DNS server so that it can resolve public domain names.
* Updating virtual network DNS servers won't affect SQL Managed Instance automatically, but must be triggered as a follow-up step. This is explained below.

## Update SQL Managed Instance with changes to the virtual network's DNS servers setting

If a virtual network's DNS servers setting has been changed after the cluster hosting SQL Managed Instance was created, then you will need to synchronize that virtual cluster's DNS servers configuration with the new configuration on the virtual network.

> [!NOTE]
> Updating a single cluster will affect all managed instances hosted in it.

### Azure RBAC permissions required

User synchronizing DNS server configuration will need to have one of the following Azure roles:

- Subscription contributor role, or
- Custom role with the following permission:
  - `Microsoft.Sql/virtualClusters/updateManagedInstanceDnsServers/action`

### Azure PowerShell

Get the virtual network where DNS servers setting has been updated, then use the PowerShell command [Invoke-AzResourceAction](/powershell/module/az.resources/invoke-azresourceaction) to synchronize DNS servers configuration for all the virtual clusters in the subnet:
```PowerShell
$ResourceGroup = 'enter resource group of virtual network'
$VirtualNetworkName = 'enter virtual network name'
$virtualNetwork = Get-AzVirtualNetwork -ResourceGroup $ResourceGroup -Name $VirtualNetworkName

Get-AzSqlVirtualCluster `
    | where SubnetId -match $virtualNetwork.Id `
    | select Id `
    | Invoke-AzResourceAction -Action updateManagedInstanceDnsServers -Force
```
### Azure CLI

Get the virtual network where DNS servers setting has been updated, then use the Azure CLI command [az resource invoke-action](/cli/azure/resource#az-resource-invoke-action) to synchronize DNS servers configuration for all the virtual clusters in the subnet:

```Azure CLI
resourceGroup="auto-failover-group"
virtualNetworkName="vnet-fog-eastus"
virtualNetwork=$(az network vnet show -g $resourceGroup -n $virtualNetworkName --query "id" -otsv)

az sql virtual-cluster list --query "[? contains(subnetId,'$virtualNetwork')].id" -o tsv \
	| az resource invoke-action --action updateManagedInstanceDnsServers --ids @-
```

## Next steps

- For an overview, see [What is Azure SQL Managed Instance?](sql-managed-instance-paas-overview.md)
- For information on how managed instance resolves and directs traffic, see [Connectivity architecture for Azure SQL Managed Instance](connectivity-architecture-overview.md).
- For more on DNS resolution in Azure virtual networks, see [Name resolution for resources in Azure virtual networks](/azure/virtual-network/virtual-networks-name-resolution-for-vms-and-role-instances#name-resolution-using-your-own-dns-server.md).
- For a tutorial showing you how to create a new managed instance, see [Create a managed instance](instance-create-quickstart.md).
- For information about configuring a virtual network for a managed instance, see [VNet configuration for managed instances](connectivity-architecture-overview.md).

