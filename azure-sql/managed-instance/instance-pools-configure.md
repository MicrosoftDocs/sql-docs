---
title: Deploy SQL Managed Instance to an instance pool
titleSuffix: Azure SQL Managed Instance
description: This article describes how to create and manage Azure SQL Managed Instance pools (preview).
author: urosmil
ms.author: urmilano
ms.reviewer: mathoma
ms.date: 09/05/2019
ms.service: sql-managed-instance
ms.subservice: deployment-configuration
ms.topic: how-to
ms.custom:
  - devx-track-azurepowershell
  - devx-track-azurecli
---
# Deploy Azure SQL Managed Instance to an instance pool
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article provides details on how to create an [instance pool](instance-pools-overview.md) and deploy Azure SQL Managed Instance to it. 

## Instance pool operations

The following table shows the available operations related to instance pools and their availability in the Azure portal, PowerShell, and Azure CLI.

|Command|Azure portal|PowerShell|Azure CLI|
|:---|:---|:---|:---|
|Create an instance pool|Yes|Yes|Yes|
|Update an instance pool |No|Yes|Yes|
|Check an instance pool usage and properties|Yes|Yes|Yes|
|Delete an instance pool|Yes|Yes|Yes|
|Create a managed instance inside an instance pool|Yes|Yes|Yes|
|Move a managed instance into the pool|No|Yes|Yes|
|Delete a managed instance from the pool|Yes|Yes|Yes|
|Move a managed instance out of the pool|No|Yes|Yes|
|Create a database in instance within the pool|Yes|Yes|Yes|
|Delete a database from SQL Managed Instance|Yes|Yes|Yes|

# [PowerShell](#tab/powershell)

To use PowerShell, [install the latest version of PowerShell Core](/powershell/scripting/install/installing-powershell#powershell), and follow instructions to [Install the Azure PowerShell module](/powershell/azure/install-az-ps).

Available [PowerShell commands](/powershell/module/az.sql/):

|Cmdlet |Description |
|:---|:---|
|[New-AzSqlInstancePool](/powershell/module/az.sql/new-azsqlinstancepool/) | Creates a SQL Managed Instance pool. |
|[Get-AzSqlInstancePool](/powershell/module/az.sql/get-azsqlinstancepool/) | Returns information about an instance pool. |
|[Set-AzSqlInstancePool](/powershell/module/az.sql/set-azsqlinstancepool/) | Sets properties for an instance pool in SQL Managed Instance. |
|[Remove-AzSqlInstancePool](/powershell/module/az.sql/remove-azsqlinstancepool/) | Removes an instance pool in SQL Managed Instance. |
|[Get-AzSqlInstancePoolUsage](/powershell/module/az.sql/get-azsqlinstancepoolusage/) | Returns information about SQL Managed Instance pool usage. |

For operations related to instances both inside pools and single instances, use the standard [managed instance commands](api-references-create-manage-instance.md#powershell-create-and-configure-managed-instances), but the *instance pool name* property must be populated when using these commands for an instance in a pool.

# [Azure CLI](#tab/azure-cli)

Prepare your environment for the Azure CLI.

[!INCLUDE [azure-cli-prepare-your-environment-no-header](~/../azure-sql/reusable-content/azure-cli/azure-cli-prepare-your-environment-no-header.md)]

Available [Azure CLI](/cli/azure/sql) commands:

|Cmdlet |Description |
|:---|:---|
|[az sql instance-pool create](/cli/azure/sql/instance-pool#az-sql-instance-pool-create) | Creates a SQL Managed Instance pool. |
|[az sql instance-pool show](/cli/azure/sql/instance-pool#az-sql-instance-pool-show) | Returns information about an instance pool. |
|[az sql instance-pool update](/cli/azure/sql/instance-pool#az-sql-instance-pool-update) | Sets or updates properties for an instance pool in SQL Managed Instance. |
|[az sql instance-pool delete](/cli/azure/sql/instance-pool#az-sql-instance-pool-delete) | Removes an instance pool in SQL Managed Instance. |

---

## Deployment process

To deploy a managed instance into an instance pool, you must first deploy the instance pool, which is a one-time long-running operation. After that, you can deploy a managed instance into the pool, which is a relatively fast operation that typically takes up to five minutes. The instance pool parameter must be explicitly specified as part of this operation.

You can deploy the instance pool, and then deploy a managed instance into the pool, by using the Azure portal, PowerShell, Azure CLI, and Azure Resource Manager templates.


# [PowerShell](#tab/powershell)

### Set variables

```powershell
$NSnetworkModels = 'Microsoft.Azure.Commands.Network.Models'
$NScollections = 'System.Collections.Generic'

# The SubscriptionId in which to create these objects
$SubscriptionId = '<your subscription>'

# Set resource group name
$resourceGroupName = 'myResourceGroup-'+$(Get-Random)

# Set location for your instance pool
# To obtain region list: Get-AzLocation | select displayname, location
$location = '<your region>'

# Set networking values for your instance pool
$vNetName = 'myVnet-'+$(Get-Random)
$vNetAddressPrefix = '10.0.0.0/16'
$miSubnetName = 'myMISubnet-'+$(Get-Random)
$miSubnetAddressPrefix = "10.0.0.0/24"

# Set instance pool name (new instance pool)
$instancePoolName = 'sqlmipool-'+$(Get-Random)

# Set instance pool service tier, compute hardware type and number of vcores
$edition = 'GeneralPurpose'
$computeGeneration = 'Gen5'
$vCores = 8

# Set licence type. 'BasePrice' or 'LicenseIncluded' if you have don't have SQL Server license that can be used for AHB discount
$license = 'BasePrice'

# Tag used for testing
# $testingTag = @{'Owner'='sqlmipoolsTesting'}
```

### Create a virtual network with a subnet

To understand how to create and configure a virtual network and subnet in which an instance pool or multiple pools can be placed, see the following articles:

- [Determine VNet subnet size for Azure SQL Managed Instance](vnet-subnet-determine-size.md).
- Create new virtual network and subnet using the [Azure portal template](virtual-network-subnet-create-arm-template.md) or follow the instructions for [preparing an existing virtual network](vnet-existing-add-subnet.md).
- [Configure existing VNet and subnet for SQL MI](https://learn.microsoft.com/en-us/azure/azure-sql/managed-instance/vnet-existing-add-subnet?view=azuresql)

You can execute the following script to create a new VNet and configure its subnet for SQL MI:

```powershell
# Configure virtual network, subnets, network security group, and routing table
$virtualNetwork = New-AzVirtualNetwork `
                      -ResourceGroupName $resourceGroupName `
                      -Location $location `
                      -Name $vNetName `
                      -AddressPrefix $vNetAddressPrefix

Add-AzVirtualNetworkSubnetConfig `
    -Name $miSubnetName `
    -VirtualNetwork $virtualNetwork `
    -AddressPrefix $miSubnetAddressPrefix | Set-AzVirtualNetwork
                  
$scriptUrlBase = 'https://raw.githubusercontent.com/Microsoft/sql-server-samples/master/samples/manage/azure-sql-db-managed-instance/delegate-subnet'

$parameters = @{
    subscriptionId = $SubscriptionId
    resourceGroupName = $resourceGroupName
    virtualNetworkName = $vNetName
    subnetName = $miSubnetName
    }

Invoke-Command -ScriptBlock ([Scriptblock]::Create((iwr ($scriptUrlBase+'/delegateSubnet.ps1?t='+ [DateTime]::Now.Ticks)).Content)) -ArgumentList $parameters
```

### Create an instance pool

After completing the previous steps, you are ready to create an instance pool.

The following restrictions apply to instance pools:

- Only General Purpose and standard-series (Gen5) are available in public preview.
- The pool name can contain only lowercase letters, numbers and hyphens, and can't start with a hyphen.
- If you want to use Azure Hybrid Benefit, it is applied at the instance pool level. You can set the license type during pool creation or update it anytime after creation.

> [!IMPORTANT]
> Deploying an instance pool is a long running operation that takes approximately 4.5 hours.

Get network parameters:

```powershell
$virtualNetwork = Get-AzVirtualNetwork -Name $vNetName -ResourceGroupName $resourceGroupName
$miSubnet = Get-AzVirtualNetworkSubnetConfig -Name $miSubnetName -VirtualNetwork $virtualNetwork
$miSubnetConfigId = $miSubnet.Id
```

Create a new instance pool by using `New-AzSqlInstancePool` (***Note:*** *long running operation*):

```powershell
$instancePool = New-AzSqlInstancePool `
    -ResourceGroupName  $resourceGroupName `
    -Name $instancePoolName `
    -SubnetId $miSubnetConfigId `
    -LicenseType $license `
    -VCore $vCores `
    -Edition $edition `
    -ComputeGeneration $computeGeneration `
    -Location $location
    # -Tag $testingTag
    # -WhatIf
    # -AsJob
```

# [Azure CLI](#tab/azure-cli)

To get the virtual network parameters:

```azurecli
az network vnet show --resource-group MyResourceGroup --name miPoolVirtualNetwork
```

To get the virtual subnet parameters:

```azurecli
az network vnet subnet show --resource group MyResourceGroup --name miPoolSubnet --vnet-name miPoolVirtualNetwork
```

To create an instance pool:

```azurecli
az sql instance-pool create
    --license-type LicenseIncluded 
    --location westeurope
    --name mi-pool-name
    --capacity 8
    --tier GeneralPurpose
    --family Gen5 
    --resource-group myResourceGroup
    --subnet miPoolSubnet
    --vnet-name miPoolVirtualNetwork
```

---

> [!IMPORTANT]
> Because deploying an instance pool is a long running operation, you need to wait until it completes before running any of the following steps in this article.

## Create a managed instance

After the successful deployment of the instance pool, it's time to create a managed instance inside it.

### Set variables

Uncomment in case you need to redefine the variables:

```powershell
# $resourceGroupName = <your resource group>
# $instancePoolName = <your instance pool name>
# $location = <your location>
# $vNetName = <your vNet name>
# $miSubnetName = <your miSubnetName>
# $virtualNetwork = Get-AzVirtualNetwork -Name $vNetName -ResourceGroupName $resourceGroupName
# $miSubnet = Get-AzVirtualNetworkSubnetConfig -Name $miSubnetName -VirtualNetwork $virtualNetwork
# $miSubnetConfigId = $miSubnet.Id
# $testingTag = @{'Owner'='sqlmipoolsTesting'}
```

### Create SQL MI in instance pool

```powershell
$instancePool = Get-AzSqlInstancePool -ResourceGroupName $resourceGroupName -Name $instancePoolName
$instance01 = $instancePool | New-AzSqlInstance `
    -Name $instance01Name `
    -VCore 2 `
    -StorageSizeInGB 32 `
    -AdministratorCredential $adminCredential `
    # -Tag $testingTag `
    # -Verbose 
```

Deploying an instance inside a pool takes a couple of minutes.

## Create a database

To create and manage databases in a managed instance that's inside a pool, use the standard single instance commands.

To create a database inside a managed instance:

```powershell
$poolinstancedb = New-AzSqlInstanceDatabase -Name "mipooldb1" -InstanceName $instance01Name -ResourceGroupName $resourceGroupName
```

## Get pool usage

To get a list of instances inside a pool:

```powershell
$instancePool | Get-AzSqlInstance
```

To get pool resource usage:

```powershell
$instancePool | Get-AzSqlInstancePoolUsage
```

To get detailed usage overview of the pool and instances inside it:

```powershell
$instancePool | Get-AzSqlInstancePoolUsage –ExpandChildren
```

To list the databases in an instance:

```powershell
$databases = Get-AzSqlInstanceDatabase -InstanceName $instance01Name  -ResourceGroupName $resourceGroupName
```

> [!NOTE]
> For checking limits on number of databases per instance pool and managed instance deployed inside the pool visit [Instance pool resource limits](instance-pools-overview.md#resource-limitations) section.

## Update an instance pool

### Variables
Uncomment only if you  don't already have the variables defined or want to redefine them:

```powershell
# $resourceGroupName = <your resource group>
# $instancePoolName = <your instance pool name>
# $location = <yourlocation>
```

```powershell
$instancePool = Get-AzSqlInstancePool -ResourceGroupName $resourceGroupName -Name $instancePoolName
```

### Update instance pool parameters

Change license type:

```powershell
$instancePool | Set-AzSqlInstancePool -LicenseType 'BasePrice' # 'LicenseIncluded' | 'BasePrice'
```

Change vCore size:

```powershell
$instancePool | Set-AzSqlInstancePool -VCores 16
```

Change Hardware type:

```powershell
$instancePool | Set-AzSqlInstancePool -ComputeGeneration 'Gen8' # 'Gen5' | 'Gen8'
```

Discover available maintenace windows:

```powershell
# 'Available maintenance schedules in $location'
$configurations = Get-AzMaintenancePublicConfiguration
$configurations | ?{ $_.Location -eq $location -and $_.MaintenanceScope -eq "SQLManagedInstance"} 
$maintenanceWindowOptions = $configurations | ?{ $_.Location -eq $location -and $_.MaintenanceScope -eq "SQLManagedInstance"}
```

Change the maintenance window and choose (for example) the second ([1]) option:

```powershell
$instancePool | Set-AzSqlInstancePool -MaintenanceConfigurationId $maintenanceWindowOptions[1].Id
```

## Update a pooled instance

After populating a managed instance with databases, you may hit instance limits regarding storage or performance. In that case, if pool usage has not been exceeded, you can scale your instance.
Scaling a managed instance inside a pool is an operation that takes a couple of minutes. The prerequisite for scaling is available vCores and storage on the instance pool level.

To update the number of vCores and storage size:

```powershell
$instance01 | Set-AzSqlInstance -VCore 8 -StorageSizeInGB 512 -InstancePoolName $instancePoolName
```

## Connect to SQL MI

To connect to a managed instance in a pool, the following two steps are required:

1. [Enable the public endpoint for the instance](#enable-the-public-endpoint).
2. [Add an inbound rule to the network security group (NSG)](#add-an-inbound-rule-to-the-network-security-group).

After both steps are complete, you can connect to the instance by using a public endpoint address, port, and credentials provided during instance creation.

### Enable the public endpoint

Enabling the public endpoint for an instance can be done through the Azure portal or by using the following PowerShell command:

```powershell
$instance01 | Set-AzSqlInstance -InstancePoolName $instancePoolName -PublicDataEndpointEnabled $true
```

This parameter can be set during instance creation as well.

### Add an inbound rule to the network security group

This step can be done through the Azure portal or using PowerShell commands, and can be done anytime after the subnet is prepared for the managed instance.

For details, see [Allow public endpoint traffic on the network security group](public-endpoint-configure.md#allow-public-endpoint-traffic-in-the-network-security-group).

## Move an existing single instance to and from an instance pool

### Move within a same subnet

Move a single instance out of an instance pool:

```powershell
$instance01 | Set-AzSqlInstance -InstancePoolName ''
```

Move a single instance into an instance pool:

```powershell
$instance01 | Set-AzSqlInstance -InstancePoolName $instancePoolName
```

## Related content

- For a features and comparison list, see [SQL common features](../database/features-comparison.md).
- For more information about VNet configuration, see [SQL Managed Instance VNet configuration](connectivity-architecture-overview.md).
- For a quickstart that creates a managed instance and restores a database from a backup file, see [Create a managed instance](instance-create-quickstart.md).
- For a tutorial about using Azure Database Migration Service for migration, see [SQL Managed Instance migration using Database Migration Service](/azure/dms/tutorial-sql-server-to-managed-instance).
- For advanced monitoring of SQL Managed Instance database performance with built-in troubleshooting intelligence, see [Monitor Azure SQL Managed Instance using Azure SQL Analytics](/azure/azure-monitor/insights/azure-sql).
- For pricing information, see [SQL Managed Instance pricing](https://azure.microsoft.com/pricing/details/sql-database/managed/).
