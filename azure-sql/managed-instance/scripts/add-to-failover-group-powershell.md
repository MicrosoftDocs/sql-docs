---
title: "PowerShell: Add a managed instance to a failover group"
titleSuffix: Azure SQL Managed Instance
description: Azure PowerShell example script to create a managed instance, add it to a failover group, and test failover.
author: Stralle
ms.author: strrodic
ms.reviewer: mathoma
ms.date: 12/15/2023
ms.service: azure-sql-managed-instance
ms.subservice: high-availability
ms.topic: sample
ms.custom:
  - sqldbrb=1
  - devx-track-azurepowershell
ms.devlang: powershell
---
# Use PowerShell to add a managed instance to a failover group 

[!INCLUDE[appliesto-sqldb](../../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](../../database/scripts/add-database-to-failover-group-powershell.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](add-to-failover-group-powershell.md?view=azuresql-mi&preserve-view=true)


This PowerShell script example creates two managed instances, adds them to a failover group, and then tests failover from the primary managed instance to the secondary managed instance. 

[!INCLUDE [quickstarts-free-trial-note](../../includes/quickstarts-free-trial-note.md)]
[!INCLUDE [updated-for-az](../../includes/updated-for-az.md)]
[!INCLUDE [cloud-shell-try-it.md](../../includes/cloud-shell-try-it.md)]

If you choose to install and use PowerShell locally, this tutorial requires Azure PowerShell 1.4.0 or later. If you need to upgrade, see [Install Azure PowerShell module](/powershell/azure/install-az-ps). If you're running PowerShell locally, you also need to run `Connect-AzAccount` to create a connection with Azure.

## Set your variables 

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/managed-instance/failover-groups/add-managed-instance-to-failover-group-az-ps.ps1" id="SetVariables":::

## Set subscription and create resource group 

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/managed-instance/failover-groups/add-managed-instance-to-failover-group-az-ps.ps1" id="CreateResourceGroup":::

| Command | Notes |
|---|---|
| 1. [Connect-AzAccount](/powershell/module/az.accounts/connect-azaccount) | Connect to Azure. |
| 2. [Set-AzContext](/powershell/module/az.accounts/set-azcontext) | Set the subscription context. 
| 3. [New-AzResourceGroup](/powershell/module/az.resources/new-azresourcegroup) | Create an Azure resource group.  |


## Create both managed instances

First, create the primary managed instance: 

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/managed-instance/failover-groups/add-managed-instance-to-failover-group-az-ps.ps1" id="CreatePrimaryInstance":::

Then, create the secondary managed instance: 

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/managed-instance/failover-groups/add-managed-instance-to-failover-group-az-ps.ps1" id="CreateSecondaryInstance":::

| Command | Notes |
|---|---|
| 1. [New-AzVirtualNetwork](/powershell/module/az.network/new-azvirtualnetwork) | Create a virtual network.  |
| 2. [Add-AzVirtualNetworkSubnetConfig](/powershell/module/az.network/add-azvirtualnetworksubnetconfig) | Add a subnet configuration to a virtual network. | 
| 3. [Set-AzVirtualNetwork](/powershell/module/az.network/set-azvirtualnetwork) | Updates a virtual network. | 
| 4. [Get-AzVirtualNetwork](/powershell/module/az.network/get-azvirtualnetwork) | Get a virtual network in a resource group. | 
| 5. [Get-AzVirtualNetworkSubnetConfig](/powershell/module/az.network/get-azvirtualnetworksubnetconfig) | Get a subnet in a virtual network. | 
| 6. [New-AzNetworkSecurityGroup](/powershell/module/az.network/new-aznetworksecuritygroup) | Create a network security group. | 
| 7. [New-AzRouteTable](/powershell/module/az.network/new-azroutetable) | Create a route table. |
| 8. [Set-AzVirtualNetworkSubnetConfig](/powershell/module/az.network/set-azvirtualnetworksubnetconfig) | Update a subnet configuration for a virtual network.  |
| 9. [Set-AzVirtualNetwork](/powershell/module/az.network/set-azvirtualnetwork) | Update a virtual network.  |
| 10. [Get-AzNetworkSecurityGroup](/powershell/module/az.network/get-aznetworksecuritygroup) | Get a network security group. |
| 11. [Add-AzNetworkSecurityRuleConfig](/powershell/module/az.network/add-aznetworksecurityruleconfig)| Add a network security rule configuration to a network security group. |
| 12. [Set-AzNetworkSecurityGroup](/powershell/module/az.network/set-aznetworksecuritygroup) | Update a network security group.  | 
| 13. [Get-AzRouteTable](/powershell/module/az.network/get-azroutetable) | Gets route tables. | 
| 14. [Add-AzRouteConfig](/powershell/module/az.network/add-azrouteconfig) | Add a route to a route table. |
| 15. [Set-AzRouteTable](/powershell/module/az.network/set-azroutetable) | Update a route table.  |
| 16. [New-AzSqlInstance](/powershell/module/az.sql/new-azsqlinstance) | Create a managed instance. When creating the secondary instance, be sure to provide the `-DnsZonePartner` to link the secondary instance to your primary instance. |

## Configure virtual network peering 

Configure global virtual network peering between the virtual networks of the primary and secondary managed instances: 

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/managed-instance/failover-groups/add-managed-instance-to-failover-group-az-ps.ps1" id="VNetPeering":::

| Command | Notes |
|---|---|
| 1. [Get-AzVirtualNetwork](/powershell/module/az.network/get-azvirtualnetwork) | Gets a virtual network in a resource group. |
| 2. [Add-AzVirtualNetworkPeering](/powershell/module/az.network/add-azvirtualnetworkpeering) | Adds a peering to a virtual network. | 
| 3. [Get-AzVirtualNetworkPeering](/powershell/module/az.network/get-azvirtualnetworkpeering) | Gets a peering for a virtual network. |


## Create the failover group

Create the failover group: 

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/managed-instance/failover-groups/add-managed-instance-to-failover-group-az-ps.ps1" id="CreateFailoverGroup":::

| Command | Notes |
|---|---|
| [New-AzSqlDatabaseInstanceFailoverGroup](/powershell/module/az.sql/new-azsqldatabaseinstancefailovergroup)| Creates a new Azure SQL Managed Instance failover group.  |

## Test planned failover

Test planned failover by failing over to the secondary replica, and then failing back. 

| Command | Notes |
|---|---|
| 1. [Get-AzSqlDatabaseInstanceFailoverGroup](/powershell/module/az.sql/get-azsqldatabaseinstancefailovergroup) | Gets or lists SQL Managed Instance failover groups.| 
| 2. [Switch-AzSqlDatabaseInstanceFailoverGroup](/powershell/module/az.sql/switch-azsqldatabaseinstancefailovergroup) | Executes a failover of a SQL Managed Instance failover group. | 

### Verify the roles of each server

Use the [Get-AzSqlDatabaseInstanceFailoverGroup](/powershell/module/az.sql/get-azsqldatabaseinstancefailovergroup) command to confirm the roles of each server:

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/managed-instance/failover-groups/add-managed-instance-to-failover-group-az-ps.ps1" id="CheckRole":::

### Fail over to the secondary server

Use the  [Switch-AzSqlDatabaseInstanceFailoverGroup](/powershell/module/az.sql/switch-azsqldatabaseinstancefailovergroup) to fail over to the secondary server. 

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/managed-instance/failover-groups/add-managed-instance-to-failover-group-az-ps.ps1" id="Failover":::

### Revert failover group back to the primary server

Use the  [Switch-AzSqlDatabaseInstanceFailoverGroup](/powershell/module/az.sql/switch-azsqldatabaseinstancefailovergroup) command to fail back to the primary server.

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/managed-instance/failover-groups/add-managed-instance-to-failover-group-az-ps.ps1" id="FailBack":::

## Clean up deployment

Use the following command to remove  the resource group and all resources associated with it. You'll need to remove the resource group twice. Removing the resource group the first time will remove the managed instance and virtual clusters but will then fail with the error message `Remove-AzResourceGroup : Long running operation failed with status 'Conflict'`. Run the Remove-AzResourceGroup command a second time to remove any residual resources as well as the resource group.

```powershell
Remove-AzResourceGroup -ResourceGroupName $resourceGroupName
```

## Full script

The following snippet is the full script: 

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/managed-instance/failover-groups/add-managed-instance-to-failover-group-az-ps.ps1" id="FullScript":::

This script uses the following commands. Each command in the table links to command specific documentation.

| Command | Notes |
|---|---|
| [New-AzResourceGroup](/powershell/module/az.resources/new-azresourcegroup) | Creates an Azure resource group.  |
| [New-AzVirtualNetwork](/powershell/module/az.network/new-azvirtualnetwork) | Creates a virtual network.  |
| [Add-AzVirtualNetworkSubnetConfig](/powershell/module/az.network/add-azvirtualnetworksubnetconfig) | Adds a subnet configuration to a virtual network. | 
| [Get-AzVirtualNetwork](/powershell/module/az.network/get-azvirtualnetwork) | Gets a virtual network in a resource group. | 
| [Get-AzVirtualNetworkSubnetConfig](/powershell/module/az.network/get-azvirtualnetworksubnetconfig) | Gets a subnet in a virtual network. | 
| [New-AzNetworkSecurityGroup](/powershell/module/az.network/new-aznetworksecuritygroup) | Creates a network security group. | 
| [New-AzRouteTable](/powershell/module/az.network/new-azroutetable) | Creates a route table. |
| [Set-AzVirtualNetworkSubnetConfig](/powershell/module/az.network/set-azvirtualnetworksubnetconfig) | Updates a subnet configuration for a virtual network.  |
| [Set-AzVirtualNetwork](/powershell/module/az.network/set-azvirtualnetwork) | Updates a virtual network.  |
| [Get-AzNetworkSecurityGroup](/powershell/module/az.network/get-aznetworksecuritygroup) | Gets a network security group. |
| [Add-AzNetworkSecurityRuleConfig](/powershell/module/az.network/add-aznetworksecurityruleconfig)| Adds a network security rule configuration to a network security group. |
| [Set-AzNetworkSecurityGroup](/powershell/module/az.network/set-aznetworksecuritygroup) | Updates a network security group.  | 
| [Add-AzRouteConfig](/powershell/module/az.network/add-azrouteconfig) | Adds a route to a route table. |
| [Set-AzRouteTable](/powershell/module/az.network/set-azroutetable) | Updates a route table.  |
| [New-AzSqlInstance](/powershell/module/az.sql/new-azsqlinstance) | Creates a managed instance.  |
| [Get-AzSqlInstance](/powershell/module/az.sql/get-azsqlinstance)| Returns information about Azure SQL Managed Instance. |
| [New-AzPublicIpAddress](/powershell/module/az.network/new-azpublicipaddress) | Creates a public IP address.  | 
| [New-AzVirtualNetworkGatewayIpConfig](/powershell/module/az.network/new-azvirtualnetworkgatewayipconfig) | Creates an IP Configuration for a Virtual Network Gateway |
| [New-AzVirtualNetworkGateway](/powershell/module/az.network/new-azvirtualnetworkgateway) | Creates a Virtual Network Gateway |
| [New-AzVirtualNetworkGatewayConnection](/powershell/module/az.network/new-azvirtualnetworkgatewayconnection) | Creates a connection between the two virtual network gateways.   |
| [New-AzSqlDatabaseInstanceFailoverGroup](/powershell/module/az.sql/new-azsqldatabaseinstancefailovergroup)| Creates a new Azure SQL Managed Instance failover group.  |
| [Get-AzSqlDatabaseInstanceFailoverGroup](/powershell/module/az.sql/get-azsqldatabaseinstancefailovergroup) | Gets or lists SQL Managed Instance failover groups.| 
| [Switch-AzSqlDatabaseInstanceFailoverGroup](/powershell/module/az.sql/switch-azsqldatabaseinstancefailovergroup) | Executes a failover of a SQL Managed Instance failover group. | 
| [Remove-AzResourceGroup](/powershell/module/az.resources/remove-azresourcegroup) | Removes a resource group. | 

## Next steps

For more information on Azure PowerShell, see [Azure PowerShell documentation](/powershell/azure/).

Additional PowerShell script samples for SQL Managed Instance can be found in [Azure SQL Managed Instance PowerShell scripts](../../database/powershell-script-content-guide.md).
