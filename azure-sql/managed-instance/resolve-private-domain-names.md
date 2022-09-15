---
title: Resolve private domain names
titleSuffix: Azure SQL Managed Instance 
description: This article describes how to configure Azure SQL Managed Instance to resolve private domain names.
services: sql-database
ms.service: sql-managed-instance
ms.subservice: deployment-configuration
ms.custom: 
ms.devlang: 
ms.topic: conceptual
author: zoran-rilak-msft
ms.author: zoranrilak
ms.reviewer: mathoma, srbozovi
ms.date: 09/16/2022
---
# Resolve private domain names in Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

In this article, learn how Azure SQL Managed Instance resolves private domain names. 

## Overview

In certain situations, it's necessary for the SQL Server database engine to resolve domain names that don't exist in public DNS records. For example, the following scenarios are likely to involve private domain names:

* Sending emails by using [database mail](/../sql/relational-databases/database-mail/database-mail.md)
* Accessing remote data sources by using [linked servers](/../sql/relational-databases/linked-servers/linked-servers-database-engine.md)
* Replicating data to the cloud by using the [Managed Instance Link](managed-instance-link-feature-overview.md).

Azure SQL Managed Instance is deployed in an Azure [virtual network (VNet)](/azure/virtual-network/virtual-networks-overview.md) and uses Azure-provided name resolution by default to resolve Internet addresses. 

To change the default name resolution behavior and enable the resolution of private domain names, you can do one of the following: 

- Use Azure private DNS zones: [What is Azure Private DNS?](/azure/dns/private-dns-overview)
- Use a custom DNS server: [Name resolution for resources in Azure virtual networks](/azure/virtual-network/virtual-networks-name-resolution-for-vms-and-role-instances.md#name-resolution-that-uses-your-own-dns-server)

## Considerations

* Be careful not to override or disable the resolution of domain names that Azure SQL Managed Instance uses internally. Always configure your custom DNS server so that it can resolve public domain names.
* When you update the DNS servers for a virtual network, SQL Managed Instances in that network must also be notified of this change, as described in the [Update SQL Managed Instances](#update-sql-managed-instances) section in this article. 
* Always use a fully qualified domain name (FQDN) for the services that you want Azure SQL Managed Instance to resolve, such as your mail server or an on-premises SQL Server instance. Use FQDNs even if those services are within your private DNS zone. For example, use `smtp.contoso.com`. Creating a linked server or configuring replication that reference SQL Server VMs inside the same virtual network also requires a FQDN and a default DNS suffix; for example, `SQLVM.internal.cloudapp.net`.

## Update SQL Managed Instances

If the DNS server setting is changed in a virtual network which already hosts SQL Managed Instances, then their virtual clusters need to synchronize with the changes in the DNS configuration. You can do so by using Azure PowerShell or the Azure CLI. 

> [!NOTE]
> Updating a single virtual cluster affects all SQL Managed Instances hosted in it.

### [Azure PowerShell](#tab/azure-powershell)

Use Azure PowerShell to update the DNS server settings for an existing virtual cluster. 

First, get the virtual network where the DNS settings have changed, and then use the Azure PowerShell command [Invoke-AzResourceAction](/powershell/module/az.resources/invoke-azresourceaction) to synchronize DNS server configurations for all the virtual clusters in the subnet:

```powershell
$ResourceGroup = 'enter resource group of virtual network'
$VirtualNetworkName = 'enter virtual network name'
$virtualNetwork = Get-AzVirtualNetwork -ResourceGroup $ResourceGroup -Name $VirtualNetworkName

Get-AzSqlVirtualCluster `
    | where SubnetId -match $virtualNetwork.Id `
    | select Id `
    | Invoke-AzResourceAction -Action updateManagedInstanceDnsServers -Force
```

# [Azure CLI](#tab/azure-cli)

Use the Azure CLI to update the DNS server settings for an existing virtual cluster. 

First, get the virtual network where the DNS settings have changed, and then use the Azure CLI command [az resource invoke-action](/cli/azure/resource#az-resource-invoke-action) to synchronize DNS server configurations for all the virtual clusters in the subnet:


```azurecli
resourceGroup="auto-failover-group"
virtualNetworkName="vnet-fog-eastus"
virtualNetwork=$(az network vnet show -g $resourceGroup -n $virtualNetworkName --query "id" -otsv)

az sql virtual-cluster list --query "[? contains(subnetId,'$virtualNetwork')].id" -o tsv \
	| az resource invoke-action --action updateManagedInstanceDnsServers --ids @-

```

---

## Permissions 

A user that is synchronizing DNS server configurations across a virtual network:

- Should be a member of the **Subscription Contributor role**, or
- Have a custom role with the `Microsoft.Sql/virtualClusters/updateManagedInstanceDnsServers/action` permission.

## Next steps

- For an overview, see [What is Azure SQL Managed Instance?](sql-managed-instance-paas-overview.md)
- For information on how managed instance resolves and directs traffic, see [Connectivity architecture for Azure SQL Managed Instance](connectivity-architecture-overview.md).
- For more on DNS resolution in Azure virtual networks, see [Name resolution for resources in Azure virtual networks](/azure/virtual-network/virtual-networks-name-resolution-for-vms-and-role-instances#name-resolution-using-your-own-dns-server.md).
- For a tutorial showing you how to create a new managed instance, see [Create a managed instance](instance-create-quickstart.md).
- For information about configuring a virtual network for a managed instance, see [VNet configuration for managed instances](connectivity-architecture-overview.md).

