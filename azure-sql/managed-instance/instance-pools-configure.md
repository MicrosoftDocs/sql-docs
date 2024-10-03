---
title: Create instance pool (preview)
titleSuffix: Azure SQL Managed Instance
description: Learn how to create instance pools (preview) for Azure SQL Managed Instance, a feature that lets you share resources for multiple instances, and provides a convenient and cost-efficient way to migrate smaller SQL Server databases to the cloud at scale. Create your instance pool by using the Azure portal, PowerShell, or the Azure CLI.
author: MariDjo
ms.author: dmarinkovic
ms.reviewer: mathoma, randolphwest
ms.date: 08/23/2024
ms.service: azure-sql-managed-instance
ms.subservice: deployment-configuration
ms.topic: how-to
ms.custom:
  - devx-track-azurepowershell
  - devx-track-azurecli
---
# Create an instance pool (preview) - Azure SQL Managed Instance

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article teaches how to create an [instance pool](instance-pools-overview.md) for [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md) by using the Azure portal, PowerShell, or the Azure CLI, as well as how to move instances in and out of the pool by using PowerShell, or the Azure CLI.

Instance pools make it possible to deploy multiple instances with shared resources to a single virtual machine, which provides a convenient and cost-effective infrastructure to migrate multiple SQL Server instances without having to consolidate smaller and less compute-intensive workloads onto a larger SQL Managed Instance.

> [!NOTE]  
> Instance pools for Azure SQL Managed Instance are currently in preview.

## Prerequisites

To create an instance pool, you should have:

- An existing [virtual network](vnet-existing-add-subnet.md) with an [appropriately sized](vnet-subnet-determine-size.md) subnet range.
- The latest [Az.SQL](/powershell/module/az.sql/) module for the current version of PowerShell or the latest version of the [Azure CLI](/cli/azure/install-azure-cli-windows).
- Reviewed [Instance and pool properties](instance-pools-overview.md#instance-and-pool-properties).

### Subnet size considerations

Carefully plan the size of your subnet when you use an instance pool. Refer to [Determine required subnet size & range](vnet-subnet-determine-size.md) for subnet sizing guidelines.

Use the following formula when calculating the number of IP addresses required by one instance pool that contains multiple General Purpose instances:

`2 * (5 + (3 * # of MIs)) + 5`

The `# of MIs` refers to the maximum potential number of instances you plan to provision. The maximum possible number of instances in a pool is 40.

## Create instance pool

You can create an instance pool by using the Azure portal, PowerShell or the Azure CLI. Consider the following:

- Only the General Purpose service tier on either standard-series (Gen5) or premium-series hardware is currently available.
- The pool name can contain only lowercase letters, numbers and hyphens, and can't start with a hyphen.
- The [Azure Hybrid Benefit](../azure-hybrid-benefit.md) is applied at the instance pool level. You can set the license type when you create the pool, and update the license type after the pool is created.

> [!IMPORTANT]  
> Deploying an instance pool is a long running operation that can take up to 4.5 hours.

### [Azure portal](#tab/portal)

To create an instance pool in the Azure portal, follow these steps:

1. Search for **instance pools** in the [Azure portal](https://portal.azure.com) and select the **Instance pools** service to open the **Instance pools** page:

   :::image type="content" source="media/instance-pools-configure/instance-pool-search-portal.png" alt-text="Screenshot searching for instance pools in the Azure portal." lightbox="media/instance-pools-configure/instance-pool-search-portal.png":::

1. On the **Instance pools** page, select **+ Create** to open the **Create Azure SQL Managed Instance Pool** page:

   :::image type="content" source="media/instance-pools-configure/instance-pool-portal-page.png" alt-text="Screenshot of the Instance pools page in the Azure portal, with +Create selected.":::

1. On the **Create Azure SQL Managed Instance Pool**:
    1. Provide project and instance details on the **Basics** tab.
    1. Use **Configure instance pool** under **Compute + storage** to open the **Compute + Storage** page and choose the service tier, compute hardware and SQL Server license that you want the pool to use. Use **Apply** to save your compute settings and go back to the **Create Azure SQL Managed Instance Pool** page.
    1. Select an existing virtual network, or configure a new virtual network on the **Networking** tab.
    1. (Optional) Configure a non-default maintenance window for the pool on the **Additional settings** tab.
    1. Review your configuration on the **Review + create** tab, and then select **Create** to create your instance pool.

   :::image type="content" source="media/instance-pools-configure/instance-pool-create-portal-page.png" alt-text="Screenshot of the Create Azure SQL Managed Instance Pool page in the Azure portal, with Configure instance pool selected.":::

1. You can monitor pool deployment from **Notifications**.

After your instance pool is created, you can [create a new instance](#create-new-instance-inside-pool) in the pool by using the Azure portal, or you can [move an existing instance into the pool](#move-existing-instance) by using PowerShell or the Azure CLI.

### [PowerShell](#tab/powershell)

To create your instance pool, use [New-AzSqlInstancePool](/powershell/module/az.sql/new-azsqlinstancepool).

Consider the following:

- For `LicenseType`, use *BasePrice* for the Azure Hybrid Benefit or *LicenseIncluded* if you don't have a SQL Server license that can be used for the Azure Hybrid Benefit discount.
- Use `Get-AzLocation | select displayname, location` to obtain a list of regions where instance pools are available.

Create a new instance pool with 8 vCores on standard-series (Gen5) hardware by running the following sample script:

```powershell
$virtualNetwork = Get-AzVirtualNetwork -Name "<vNetName>" -ResourceGroupName "<resourceGroupName>"
$miSubnet = Get-AzVirtualNetworkSubnetConfig -Name "<miSubnetName>" -VirtualNetwork 
$virtualNetwork$miSubnetConfigId = $miSubnet.Id

$parameters = @{
    ResourceGroupName = "<resource group name>"
    Name = "<instance pool name>"
    LicenseType = "LicenseIncluded"
    VCore = 8
    Edition = "GeneralPurpose"
    ComputeGeneration = "Gen5"
    Location = "<region>"
    $virtualNetwork = Get-AzVirtualNetwork -Name "<vNetName>" -ResourceGroupName "<resourceGroupName>"
    $miSubnet = Get-AzVirtualNetworkSubnetConfig -Name "<miSubnetName>" -VirtualNetwork $virtualNetwork
    $miSubnetConfigId = $miSubnet.Id
    SubnetId = $miSubnetConfigId
}

$instancePool = New-AzSqlInstancePool @parameters
```

### [Azure CLI](#tab/azure-cli)

To create your instance pool, use [az sql instance-pool create](/cli/azure/sql/instance-pool#az-sql-instance-pool-create).

Consider the following:
- For `--license-type`, use *BasePrice* for the Azure Hybrid Benefit or *LicenseIncluded* if you don't have a SQL Server license that can be used for the Azure Hybrid Benefit discount.

Create a new instance pool with 8 vCores on standard-series (Gen5) hardware by running the following sample script:

```azurecli
# Create the instance pool
az sql instance-pool create \
    --license-type LicenseIncluded \
    --location <region> \
    --name <pool name> \
    --capacity 8 \
    --tier GeneralPurpose \
    --family Gen5 \
    --resource-group <resource group name> \
    --subnet <subnet name> \
    --vnet-name <vnet name>
```

---

## Create new instance inside pool

After your pool is created, you can create a new instance within the pool by using the Azure portal, PowerShell, or the Azure CLI.

Consider the following:
- You must specify the license type for the new instance, and it must match the license type of the pool. 

### [Azure portal](#tab/portal)

To create a new instance inside a pool by using the Azure portal, follow these steps:

1. Go to the [Azure SQL](https://portal.azure.com/#view/HubsExtension/BrowseResource/resourceType/Microsoft.Sql%2Fazuresql) page in the Azure portal.
1. On the **Azure SQL** page, select **+ Create** to open the **Select SQL deployment option**.
1. On the **SQL managed instances** tile, choose **Single instance** as the resource type and then select **Create** to open the **Create Azure SQL Managed Instance** page.
1. On the **Basics** tab of the **Create Azure SQL Managed Instance** page:
    1. Select the resource group that contains your existing instance pool.
    1. Choose _Yes_ to **Belongs to an instance pool?** under **Managed Instance details** to create your new instance inside an instance pool.
    1. Select the pool from the **Instance pool** dropdown list.

   :::image type="content" source="media/instance-pools-configure/create-instance-inside-pool.png" alt-text="Screenshot of the Create Azure SQL Managed Instance page in the Azure portal with belongs to an instance pool selected.":::
1. Fill out the remaining details on the **Create Azure SQL Managed Instance** page to create your instance inside the pool. For details, review [Create Azure SQL Managed Instance](instance-create-quickstart.md).
1. Select **Review + create** to review settings for your new instance and then use **Create** to deploy your instance inside the selected pool.

### [PowerShell](#tab/powershell)

To identify pool parameters with PowerShell, use [Get-AzSqlInstancePool](/powershell/module/az.sql/get-azsqlinstancepool) then create your instance inside the specific pool with [New-AzSqlInstance](/powershell/module/az.sql/new-azsqlinstance).

Create a new instance in your pool by running the following sample script:

```powershell
$adminCredential = Get-Credential
$instancePool = Get-AzSqlInstancePool -ResourceGroupName <resource group name> -Name <instance pool name>

$instance01Params = @{
    Name = $instance01
    VCore = 2
    StorageSizeInGB = 32
    AdministratorCredential = $adminCredential
}

$instance01 = $instancePool | New-AzSqlInstance @instance01Params
```

### [Azure CLI](#tab/azure-cli)

To create a new instance in your pool with the Azure CLI, provide the pool name in the `--instance-pool-name` parameter when you use [az sql mi create](/cli/azure/sql/mi#az-sql-mi-create) to create your instance:

```azurecli
#obtain the subnetId of an instance pool
sqlmipoolSubnetId=$(az sql instance-pool show -g <resource group name> -n <instance pool name> --query subnetId --output tsv)

az sql mi create \
  --license-type LicenseIncluded \
  --name <Instance name> \
  --admin-user <username> \
  --admin-password <password> \
  --capacity 2 \
  --instance-pool-name <instance pool name> \
  --storage 32 \
  --resource-group <resource group name> \
  --subnet $sqlmipoolSubnetId
```

---

## Move existing instance

You can move an existing instance into and out of a pool by using PowerShell or the Azure CLI if:

- It's in the same resource group as the pool.
- It's on the same virtual network and subnet as the pool.
- It fits the instance pool resource limits.

When an existing instance is moved into a pool, settings at the pool level take precedence over instance-level settings. For example, the instance inherits the license type and maintenance window set at the pool level. When an instance is moved out of the pool, it retains the settings it inherited from the pool. The only exception is with the license type, which defaults back to 'LicenseIncluded' when an instance is removed from the instance pool - the [Azure Hybrid Benefit](../azure-hybrid-benefit.md) and [hybrid failover rights benefit](managed-instance-link-feature-overview.md#license-free-passive-dr-replica) must be configured manually after an instance is moved out of a pool.

Moving an existing instance inside a pool by using the Azure portal is not currently supported.

### [PowerShell](#tab/powershell-1)

To move an instance into a pool with PowerShell, provide the pool name when you use [Set-AzSqlInstance](/powershell/module/az.sql/set-azsqlinstance):

```powershell
$instance01 | Set-AzSqlInstance -InstancePoolName $instancePoolName
```

To move an instance out of a pool, provide a _blank_ pool name:

```powershell
$instance01 | Set-AzSqlInstance -InstancePoolName ''
```

### [Azure CLI](#tab/azure-cli-1)

To move an instance into a pool with the Azure CLI, provide the pool name in the `--instance-pool-name` parameter when you use [az sql mi update](/cli/azure/sql/mi#az-sql-mi-update) to update your instance:

```azurecli
az sql mi update \
  --name <instance name> \
  --instance-pool-name <instance pool name> \
  --resource-group <resource group name>
```

To move an instance out of a pool, provide a _blank_ name in the `--instance-pool-name` parameter when you use [az sql mi update](/cli/azure/sql/mi#az-sql-mi-update) to update your instance:

```azurecli
az sql mi update \
  --name <instance name> \
  --instance-pool-name \
  --resource-group <resource group name>
```

---

## Connect to instance in a pool

You can choose to connect to an instance in a pool with either a private endpoint, or a public endpoint. To use a private endpoint, you'll need to use the [Azure Private Link](private-endpoint-overview.md).

To connect to an instance in a pool with a public endpoint, you need to [enable the endpoint](public-endpoint-configure.md#enable-public-endpoint) and then [allow public endpoint traffic on the network security group](public-endpoint-configure.md#allow-public-endpoint-traffic-in-the-network-security-group).

## Create a database

Creating a database for an instance inside a pool is the same as creating a database for a single instance. You can create a new database by using the Azure portal, PowerShell or the Azure CLI.

### [Azure portal](#tab/portal)

To create a new database for an existing SQL managed instance by using the Azure portal, follow these steps:

1. Go to your [SQL managed instance](https://portal.azure.com/#view/HubsExtension/BrowseResource/resourceType/Microsoft.Sql%2FmanagedInstances) in the Azure portal.
1. On the **Overview** pane, select **+ New database** from the command bar to open the **Create Azure SQL Managed Database** page.
1. Provide details for the new database.
1. Select **Review + create** to review your new database configuration and then use **Create** to deploy your database.

### [PowerShell](#tab/powershell)

To create a new database for your instance, use [New-AzSqlInstanceDatabase](/powershell/module/az.sql/new-azsqlinstancedatabase):

```powershell
$databaseParams = @{
    Name = "<database name>"
    InstanceName = "<instance name>"
    ResourceGroupName = "<resource group>"
}

New-AzSqlInstanceDatabase @databaseParams
```

### [Azure CLI](#tab/azure-cli)

To create a new database for your instance, use [az sql midb create](/cli/azure/sql/midb#az-sql-midb-create):

```azurecli
az sql midb create
   --managed-instance <Instance name> \
   --name <Database name> \
   --resource-group <Resource group name>
```

---

## Get pool usage

### [PowerShell](#tab/powershell-1)

You can use PowerShell to determine how resources are being used inside a pool.

To get a list of instances inside a pool, use [Get-AzSqlInstance](/powershell/module/az.sql/get-azsqlinstance):

```powershell
$instancePool | Get-AzSqlInstance
```

To get pool resource usage, use [Get-AzSqlInstancePoolUsage](/powershell/module/az.sql/get-azsqlinstancepoolusage):

```powershell
$instancePool| Get-AzSqlInstancePoolUsage
```

You can add the -ExpandChildren parameter to get a detailed overview of the pool and instances inside it:

```powershell
$instancePool | Get-AzSqlInstancePoolUsage –ExpandChildren
```

To list the databases in an instance, use [Get-AzSqlInstanceDatabase](/powershell/module/az.sql/get-azsqlinstancedatabase):

```powershell
$databaseParams = @{
    InstanceName = $instance01Name
    ResourceGroupName = $resourceGroupName
}

$databases = Get-AzSqlInstanceDatabase @databaseParams
```

> [!NOTE]  
> To check limits on the instances deployed to a pool, and databases per instance pool, review [resource limits](instance-pools-overview.md#resource-limits).

### [Azure CLI](#tab/azure-cli-1)

To get information about the instances and resource usage in the pool, use [az sql instance-pool show](/cli/azure/sql/instance-pool#az-sql-instance-pool-show):

```azurecli
sqlmipoolId=$(az sql instance-pool show --name <pool name> \
--resource-group <resource group name> --query id | cut -d '"' -f 2) \
az sql mi list --resource-group <resource group name> \
--query "[?instancePoolId == '$sqlmipoolId'].{sqlmiName:name}" -o tsv
```

---

## Update an instance pool

You can update settings for an existing instance pool by using PowerShell or the Azure CLI.

### [PowerShell](#tab/powershell-1)

You can use PowerShell to make changes to the instance pool limits.

The following sample script changes the license type, vCore size, and hardware type:

Change license type:

```powershell
$instancePoolParams = @{
    LicenseType = "BasePrice"
    VCores = 16
    ComputeGeneration = "Gen8"
}
$instancePool | Set-AzSqlInstancePool @instancePoolParams
```

You can also determine the available maintenance window schedules:

```powershell
$parameters = @{
    Location = $location
    MaintenanceScope = "SQLManagedInstance"
}
 
$configurations = Get-AzMaintenancePublicConfiguration @parameters
$maintenanceWindowOptions = $configurations | Where-Object { $_.Location -eq $location -and $_.MaintenanceScope -eq "SQLManagedInstance" }
```

And then you can change the maintenance window by specifying a window option, such as:

```powershell
$instancePoolParams = @{
    MaintenanceConfigurationId = $maintenanceWindowOptions[1].Id
}

$instancePool | Set-AzSqlInstancePool @instancePoolParams
```

### [Azure CLI](#tab/azure-cli-1)

To update configuration settings for your pool, use [az sql instance-pool update](/cli/azure/sql/instance-pool#az-sql-instance-pool-update):

```azurecli
az sql instance-pool update --name <pool name> \
--resource-group <resource group> --capacity 16 \
--license-type LicenseIncluded --family Gen8IM
```

To update the maintenance window:

```azurecli
maintenanceWindowOptions=$(az maintenance public-configuration list \
--query "[?location==<eastus2>&&contains(maintenanceScope,'SQLManagedInstance')]")

az sql instance-pool update --name <pool name> \
--resource-group <resource group> --maint-config-id <maintenance configuration id>
```

---

## Update a pooled instance

If pool resource limits haven't been exceeded, you can modify resource configurations for an instance inside a pool using PowerShell or the Azure CLI,

### [PowerShell](#tab/powershell-1)

To modify resource parameters for an instance inside a pool, use [Set-AzSqlInstance](/powershell/module/az.sql/set-azsqlinstance).

The following sample updates the vCores to 8 and changes the storage size to 512 GB for Instance1:

```powershell
$instancePoolParams = @{
    VCore = 8
    StorageSizeInGB = 512
    InstancePoolName = $instancePoolName
}

$instance1name | Set-AzSqlInstance @instancePoolParams
```

### [Azure CLI](#tab/azure-cli-1)

To modify resource parameters for an instance inside a pool, use [az sql mi update](/cli/azure/sql/mi#az-sql-mi-update).

The following sample updates the vCores to 8 and changes the storage size to 512 GB for Instance1:

```azurecli
az sql mi update \
  --name Instance1 \
  --resource-group <resource group name> \
  --capacity 8 \
  --storage 512
```

---

## Delete an instance pool

You can delete an instance pool by using PowerShell or the Azure CLI, once all instances in the pool have either been deleted, or moved out of the pool.

### [PowerShell](#tab/powershell-1)

To delete an instance pool, use [Remove-AzSqlInstancePool](/powershell/module/az.sql/remove-azsqlinstancepool/).

The following sample script deletes an empty instance pool:

```powershell
$params = @{
    ResourceGroupName = "<resource group name>"
    Name = "<instance pool name>"
}

Remove-AzSqlInstancePool @params
```

### [Azure CLI](#tab/azure-cli-1)

To delete an instance pool, use [az sql instance-pool delete](/cli/azure/sql/instance-pool#az-sql-instance-pool-delete).

The following sample script deletes an empty instance pool:

```azurecli
az sql instance-pool delete
  --name <pool name>
  --resource group <resource group name>
```

---

## Instance pool operations

The following table shows available instance pool operations:

| Command | Azure portal | PowerShell | Azure CLI |
| :--- | :--- | :--- | :--- |
| Create an instance pool | Yes | Yes | Yes |
| Update pool properties | No | Yes | Yes |
| Check a pool use and properties | Yes | Yes | Yes |
| Delete an instance pool | Yes | Yes | Yes |
| Create new managed instance inside a pool | Yes | Yes | Yes |
| Move a managed instance into a pool | No | Yes | Yes |
| Delete a managed instance from a pool | Yes | Yes | Yes |
| Move a managed instance out of a pool | No | Yes | Yes |
| Create a database in instance within a pool | Yes | Yes | Yes |
| Delete a database from SQL Managed Instance | Yes | Yes | Yes |

# [PowerShell](#tab/powershell-1)

To use PowerShell, [install the latest version of PowerShell Core](/powershell/scripting/install/installing-powershell#powershell), and follow instructions to [Install the Azure PowerShell module](/powershell/azure/install-az-ps).

Available [PowerShell commands](/powershell/module/az.sql/):

| Cmdlet | Description |
| :--- | :--- |
| [New-AzSqlInstancePool](/powershell/module/az.sql/new-azsqlinstancepool/) | Creates an instance pool. |
| [Get-AzSqlInstancePool](/powershell/module/az.sql/get-azsqlinstancepool/) | Returns information about an instance pool. |
| [Set-AzSqlInstancePool](/powershell/module/az.sql/set-azsqlinstancepool/) | Sets properties for an instance pool. |
| [Remove-AzSqlInstancePool](/powershell/module/az.sql/remove-azsqlinstancepool/) | Removes an instance pool. |
| [Get-AzSqlInstancePoolUsage](/powershell/module/az.sql/get-azsqlinstancepoolusage/) | Returns information about instance pool usage. |

For operations related to instances both inside pools and single instances, use the standard [managed instance commands](api-references-create-manage-instance.md#powershell-create-and-configure-managed-instances), but the *instance pool name* property must be populated when using these commands for an instance in a pool.

# [Azure CLI](#tab/azure-cli-1)

Prepare your environment for the Azure CLI.

[!INCLUDE [azure-cli-prepare-your-environment-no-header](~/../azure-sql/reusable-content/azure-cli/azure-cli-prepare-your-environment-no-header.md)]

Available [Azure CLI](/cli/azure/sql) commands:

| Cmdlet | Description |
| :--- | :--- |
| [az sql instance-pool create](/cli/azure/sql/instance-pool#az-sql-instance-pool-create) | Creates an instance pool. |
| [az sql instance-pool show](/cli/azure/sql/instance-pool#az-sql-instance-pool-show) | Returns information about an instance pool. |
| [az sql instance-pool update](/cli/azure/sql/instance-pool#az-sql-instance-pool-update) | Sets or updates properties for an instance pool. |
| [az sql instance-pool delete](/cli/azure/sql/instance-pool#az-sql-instance-pool-delete) | Removes an instance pool. |

---

## Limitations

During public preview, instances in a pool have the following limitations:

- The pool name can contain only lowercase letters, numbers and hyphens, and can't start with a hyphen.
- All instances in the pool use the same licensing model. When you specify a license model for an instance that is different than the license model for the pool, the pool license model is used. When the instance is moved out of the pool, it automatically switches to a full paid license (`LicenseType` = 'LicenseIncluded'). Manually activate the [Azure Hybrid Benefit](../azure-hybrid-benefit.md) or the [hybrid failover rights benefit](managed-instance-link-feature-overview.md#license-free-passive-dr-replica) to change the licensing model.
- Pooled instances must belong to the same subnet and resource group. Moving an instance in and out of the pool is only possible within the subnet of the pool and same resource group.
- Only the General Purpose service tier is available on standard-series (Gen5) or premium-series hardware. The Next-gen General Purpose, Business Critical service tier, and premium-series memory optimized hardware isn't available.
- The maximum possible number of instances in the pool is 40.
- An instance pool can only be deleted after all instances in the pool are either deleted or moved out of the pool.
- You can't use the Azure portal to:
    - Configure the instance pool. Use PowerShell or the Azure CLI instead.
    - Move instances in and out of the pool. Use PowerShell or the Azure CLI instead.
- The following SQL Managed Instance features aren't supported when instances are in a pool:
    - [Failover groups](failover-group-sql-mi.md). [Failover rights](failover-group-standby-replica-how-to-configure.md) aren't available to instances in a pool.
    - [Start/Stop](instance-stop-start-how-to.md).
    - [Zone Redundancy](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability). 
    - [Reserved capacity](../database/reserved-capacity-overview.md) instance pricing isn't available.

## Support requests

[Create and manage support requests](/azure/azure-portal/supportability/how-to-create-azure-support-request) for instance pools in the Azure portal.

To create a new support request in the Azure portal, follow these steps:

1. Open the [New support request](https://portal.azure.com/#view/Microsoft_Azure_Support/NewSupportRequestV3Blade/assetId//callerWorkflowId/) page in the Azure portal.
1. On the **New support request**, provide the following information:
    1. For **Issue type**, select `Technical`.
    1. Choose the appropriate **Subscription** from the dropdown list.
    1. For the **Service type**, select `SQL Managed Instance`.
    1. For **Resource**, provide the name of your SQL Managed Instance if it exists, or select **General question** if you're not able to deploy your instance inside the pool.
    1. For **Summary**, type `instance pools`.
    1. For **Problem type**, choose `Create, Scale, Stop, Start, or Delete Resources`.
    1. For **Problem Subtype**, choose `Instance Pools`.

   :::image type="content" source="media/instance-pools-configure/support-request.png" alt-text="Screenshot of the Instance pools support request in the Azure portal." lightbox="media/instance-pools-configure/support-request.png":::

1. Select **Next** on the subsequent pages until you're able to **Create** your support request.

To create larger SQL Managed Instance deployments (with or without instance pools), you might need to obtain a larger regional quota. For more information, see [Request quota increases for Azure SQL Database](../database/quota-increase-request.md). The deployment logic for instance pools compares total vCore consumption *at the pool level* against your quota to determine whether you're allowed to create new resources without further increasing your quota.

## Related content

- [SQL common features](../database/features-comparison.md)
- [SQL Managed Instance virtual network configuration](connectivity-architecture-overview.md)
- [Create a managed instance quickstart](instance-create-quickstart.md)
- [SQL Managed Instance migration using Database Migration Service](/azure/dms/tutorial-sql-server-to-managed-instance)
- [Monitor Azure SQL Managed Instance using database watcher](../database-watcher-overview.md)
- [SQL Managed Instance pricing](https://azure.microsoft.com/pricing/details/sql-database/managed/)
