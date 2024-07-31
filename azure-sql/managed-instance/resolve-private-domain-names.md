---
title: Resolve private domain names
titleSuffix: Azure SQL Managed Instance
description: This article describes how to configure Azure SQL Managed Instance to resolve private domain names.
author: zoran-rilak-msft
ms.author: zoranrilak
ms.reviewer: mathoma, srbozovi
ms.date: 09/16/2022
ms.service: azure-sql-managed-instance
ms.subservice: deployment-configuration
ms.custom: ignite-2023
ms.topic: conceptual
---
# Resolve private domain names in Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

In this article, learn how Azure SQL Managed Instance resolves private domain names. 

## Overview

In certain situations, it's necessary for the SQL Server database engine to resolve domain names that don't exist in public DNS records. For example, the following scenarios are likely to involve private domain names:

* Sending emails by using [database mail](/sql/relational-databases/database-mail/database-mail)
* Accessing remote data sources by using [linked servers](/sql/relational-databases/linked-servers/linked-servers-database-engine)
* Replicating data to the cloud by using the [Managed Instance Link](managed-instance-link-feature-overview.md).

Azure SQL Managed Instance is deployed in an Azure [virtual network](/azure/virtual-network/virtual-networks-overview) and uses Azure-provided name resolution by default to resolve Internet addresses.

To change the default name resolution behavior and enable the resolution of private domain names, you can:

- Use Azure private DNS zones: [What is Azure Private DNS?](/azure/dns/private-dns-overview)
- Use a custom DNS server: [Name resolution for resources in Azure virtual networks](/azure/virtual-network/virtual-networks-name-resolution-for-vms-and-role-instances#name-resolution-that-uses-your-own-dns-server)

> [!IMPORTANT]
> When you change a virtual network's DNS server from Azure to custom or vice versa, SQL Managed Instances in that virtual network must also be notified of the change. This is described in the section [Update SQL Managed Instances](#update-sql-managed-instances).
>
> You don't need to do this when you only attach or update Azure private DNS zones to the managed instances' virtual network. Those changes automatically propagate to the managed instances.

## Considerations

* Be careful not to override or disable the resolution of domain names that Azure SQL Managed Instance uses internally, as listed in [Networking constraints](connectivity-architecture-overview.md#networking-constraints). Always configure your custom DNS server so that it can resolve public domain names.
* Always use a fully qualified domain name (FQDN) for the services that you want Azure SQL Managed Instance to resolve, such as your mail server or an on-premises SQL Server instance. Use FQDNs even if those services are within your private DNS zone. For example, use `smtp.contoso.com`. Creating a linked server or configuring replication that reference SQL Server VMs inside the same virtual network also require an FQDN and a default DNS suffix; for example, `SQLVM.internal.cloudapp.net`.

## Update SQL Managed Instances

If the DNS server setting is changed in a virtual network with existing SQL managed instances, then the virtual cluster that hosts those instances, and the underlying machine groups, need to synchronize with the changes in the DNS configuration. Updating a virtual cluster affects all SQL Managed Instances hosted in it.

When you update a virtual cluster's DNS server settings, the custom DNS server IP addresses set on the virtual network become the preferred DNS servers for the instances in that cluster. The instances still retain Azure's DNS resolver address as a backup, but now resolve addresses using the custom DNS servers first.


### [Azure portal](#tab/azure-portal)

Use the Azure portal to update the DNS server settings for an existing virtual cluster.

1. Open the [Azure portal](https://portal.azure.com/).
2. Open the resource group with a managed instance in the subnet you're configuring, and select the **SQL managed instance** that you want to update the DNS server settings for.
3. In **Overview**, select the **Virtual cluster** the instance belongs to.
4. Select **Synchronize DNS server settings** to update the cluster.

:::image type="content" source="./media/resolve-private-domain-names/synchronize-dns-server-settings.png" alt-text="Screenshot showing the Synchronize DNS server settings action on the virtual cluster's overview page.":::

### [Azure PowerShell](#tab/azure-powershell)

Use Azure PowerShell to update the DNS server settings for an existing virtual cluster. 

First, get the virtual network where the DNS settings changed, and then use the Azure PowerShell command [Invoke-AzResourceAction](/powershell/module/az.resources/invoke-azresourceaction) to synchronize DNS server configurations for the virtual cluster:

```powershell
$ResourceGroup = 'enter resource group of virtual network'
$VirtualNetworkName = 'enter virtual network name'
$virtualNetwork = Get-AzVirtualNetwork -ResourceGroup $ResourceGroup -Name $VirtualNetworkName

Get-AzSqlVirtualCluster `
    | where SubnetId -match $virtualNetwork.Id `
    | select Id `
    | Invoke-AzResourceAction -Action updateManagedInstanceDnsServers -Force
```

### [Azure CLI](#tab/azure-cli)

Use the Azure CLI to update the DNS server settings for an existing virtual cluster. 

First, get the virtual network where the DNS settings changed, and then use the Azure CLI command [az resource invoke-action](/cli/azure/resource#az-resource-invoke-action) to synchronize DNS server configurations for the virtual cluster:


```azurecli
resourceGroup="failover-group"
virtualNetworkName="vnet-fog-eastus"
virtualNetwork=$(az network vnet show -g $resourceGroup -n $virtualNetworkName --query "id" -otsv)

az sql virtual-cluster list --query "[? contains(subnetId,'$virtualNetwork')].id" -o tsv \
	| az resource invoke-action --action updateManagedInstanceDnsServers --ids @-

```

---

## Verify the configuration

After you update the DNS server settings on the virtual cluster, you can verify that it's now in effect for the managed instances in that cluster. One way to do so is to create and run a [SQL Server Agent](/sql/ssms/agent/sql-server-agent) job that outputs a list of DNS servers currently configured on the network interface.

To view the list of DNS servers configured on the managed instance's network interface:

1. Start [SQL Server Management Studio](/sql/ssms/download-sql-server-management-studio-ssms).
2. Connect to a managed instance in the cluster you updated the DNS settings for.
3. In **Object Explorer**, expand **SQL Server Agent**.
4. Right-click **Jobs** and select **New Job...**.
5. In **General**, write the name of the job; for example "Verify DNS settings".
6. In **Steps**, select **New...**.
7. Write the name of the step; for example, "run".
8. Set **Type** to PowerShell.
9. In **Command**, paste the following command: `Get-DnsClientServerAddress`
10. Select **OK**, then **OK** to close both dialog windows.
11. In **Object Explorer**, right-click the job you created and select **Start Job at Step...**. The job will run.
12. When the job finishes, right-click it again and select **View History**.
13. In **Log file summary**, expand the job and select its first and only step underneath it.
14. In **Selected row details:**, review the output of the command and confirm that it contains your DNS server IP address(es).

## Permissions

A user that is synchronizing DNS server configurations across a virtual network:

- Should be a member of the **Subscription Contributor role**, or
- Have a custom role with the `Microsoft.Sql/virtualClusters/updateManagedInstanceDnsServers/action` permission.

## Next steps

- For an overview, see [What is Azure SQL Managed Instance?](sql-managed-instance-paas-overview.md)
- For information on how managed instance resolves and directs traffic, see [Connectivity architecture for Azure SQL Managed Instance](connectivity-architecture-overview.md).
- To learn more, see [Architecture of SQL Managed Instance virtual cluster](virtual-cluster-architecture.md)
- For more on DNS resolution in Azure virtual networks, see [Name resolution for resources in Azure virtual networks](/azure/virtual-network/virtual-networks-name-resolution-for-vms-and-role-instances#name-resolution-using-your-own-dns-server.md).
- For a tutorial showing you how to create a new managed instance, see [Create a managed instance](instance-create-quickstart.md).
- For information about configuring a virtual network for a managed instance, see [Connectivity architecture](connectivity-architecture-overview.md).
