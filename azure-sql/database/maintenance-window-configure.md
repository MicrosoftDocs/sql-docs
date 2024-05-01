---
title: Configure maintenance window
titleSuffix: Azure SQL Database 
description: Learn how to set the time when planned maintenance should be performed on your databases when you use Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: scottkim, mathoma
ms.date: 01/11/2024
ms.service: sql-database
ms.subservice: deployment-configuration
ms.topic: how-to
ms.custom:
  - devx-track-azurecli
  - devx-track-azurepowershell
  - azure-sql-split
monikerRange: "=azuresql||=azuresql-db"
---
# Configure maintenance window in Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/maintenance-window-configure.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/maintenance-window-configure.md?view=azuresql-mi&preserve-view=true)

You can configure the [maintenance window](maintenance-window.md) for a database or elastic pool in Azure SQL Database, or anytime after a resource is created.

- The *System default* maintenance window is 5PM to 8AM daily (local time of the Azure region the resource is located) to avoid peak business hours interruptions. 
- If the *System default* maintenance window is not the best time, select one of the other available maintenance windows.

The ability to change to a different maintenance window is not available for every service level or in every region. For details on feature availability, see [Maintenance window availability](maintenance-window.md#feature-availability). 

> [!Important]
> Configuring maintenance window is a long running asynchronous operation, similar to changing the service tier of the Azure SQL resource. The resource is available during the operation, except a short reconfiguration that happens at the end of the operation and typically lasts up to 8 seconds even in case of interrupted long-running transactions. To minimize the impact of the reconfiguration you should perform the operation outside of the peak hours.

## Configure maintenance window during database creation

# [Portal](#tab/azure-portal)

To configure the maintenance window when you create a database or elastic pool, set the desired **Maintenance window** on the **Additional settings** page.

### Set the maintenance window while creating a single database or elastic pool

For step-by-step information on creating a new database or pool, see [Create an Azure SQL Database single database](single-database-create-quickstart.md).

   :::image type="content" source="media/maintenance-window-configure/additional-settings.png" alt-text="Screenshot from the Azure portal showing the Create SQL Database wizard. The additional settings tab is open, and the Maintenance window drop down is boxed in red.":::

# [PowerShell](#tab/azure-powershell)

The following examples show how to configure the maintenance window using Azure PowerShell. You can [install Azure PowerShell](/powershell/azure/install-az-ps), or use the Azure Cloud Shell.

#### Launch Azure Cloud Shell

The Azure Cloud Shell is a free interactive shell that you can use to run the steps in this article. It has common Azure tools preinstalled and configured to use with your account.

To open the Cloud Shell, select **Try it** from the upper right corner of a code block. You can also launch Cloud Shell in a separate browser tab by going to [https://shell.azure.com](https://shell.azure.com).

When Cloud Shell opens, verify that **PowerShell** is selected for your environment. Subsequent sessions will use Azure CLI in a PowerShell environment. Select **Copy** to copy the blocks of code, paste it into the Cloud Shell, and press **Enter** to run it.

#### Discover available maintenance windows

When setting the maintenance window, each region has its own maintenance window options that correspond to the timezone for the region the database or pool is located.

#### Discover SQL Database and elastic pool maintenance windows

The following example returns the available maintenance windows for the *eastus2* region using the [Get-AzMaintenancePublicConfiguration](/powershell/module/az.maintenance/get-azmaintenancepublicconfiguration) cmdlet. For databases and elastic pools, set `MaintenanceScope` to `SQLDB`.

   ```powershell-interactive
   $location = "eastus2"

   Write-Host "Available maintenance schedules in ${location}:"
   $configurations = Get-AzMaintenancePublicConfiguration
   $configurations | ?{ $_.Location -eq $location -and $_.MaintenanceScope -eq "SQLDB"}
   ```

#### Set the maintenance window while creating a single database

The following example creates a new database and sets the maintenance window using the [New-AzSqlDatabase](/powershell/module/az.sql/new-azsqldatabase) cmdlet. The `-MaintenanceConfigurationId` must be set to a valid value for your database's region. To get valid values for your region, see [Discover available maintenance windows](#discover-available-maintenance-windows).

   ```powershell-interactive
    # Set variables for your database
    $resourceGroupName = "your_resource_group_name"
    $serverName = "your_server_name"
    $databaseName = "your_db_name"
    
    # Set selected maintenance window
    $maintenanceConfig = "SQL_EastUS2_DB_1"

    Write-host "Creating a standard-series (Gen5) 2 vCore database with maintenance window ${maintenanceConfig} ..."
    $database = New-AzSqlDatabase `
      -ResourceGroupName $resourceGroupName `
      -ServerName $serverName `
      -DatabaseName $databaseName `
      -Edition GeneralPurpose `
      -ComputeGeneration Gen5 `
      -VCore 2 `
      -MaintenanceConfigurationId $maintenanceConfig
    $database
   ```

### Set the maintenance window while creating an elastic pool

The following example creates a new elastic pool and sets the maintenance window using the [New-AzSqlElasticPool](/powershell/module/az.sql/new-azsqlelasticpool) cmdlet. The maintenance window is set on the elastic pool, so all databases in the pool have the pool's maintenance window schedule. The `-MaintenanceConfigurationId` must be set to a valid value for your pool's region. To get valid values for your region, see [Discover available maintenance windows](#discover-available-maintenance-windows).

   ```powershell-interactive
    # Set variables for your pool
    $resourceGroupName = "your_resource_group_name"
    $serverName = "your_server_name"
    $poolName = "your_pool_name"
    
    # Set selected maintenance window
    $maintenanceConfig = "SQL_EastUS2_DB_2"

    Write-host "Creating a Standard 50 pool with maintenance window ${maintenanceConfig} ..."
    $pool = New-AzSqlElasticPool `
      -ResourceGroupName $resourceGroupName `
      -ServerName $serverName `
      -ElasticPoolName $poolName `
      -Edition "Standard" `
      -Dtu 50 `
      -DatabaseDtuMin 10 `
      -DatabaseDtuMax 20 `
      -MaintenanceConfigurationId $maintenanceConfig
    $pool
   ```

# [CLI](#tab/azure-cli)

The following examples show how to configure the maintenance window using Azure CLI. You can [install Azure CLI](/cli/azure/install-azure-cli), or use the Azure Cloud Shell.

### Launch Azure Cloud Shell

The Azure Cloud Shell is a free interactive shell that you can use to run the steps in this article. It has common Azure tools preinstalled and configured to use with your account.

To open the Cloud Shell, select **Try it** from the upper right corner of a code block. You can also launch Cloud Shell in a separate browser tab by going to [https://shell.azure.com](https://shell.azure.com).

When Cloud Shell opens, verify that **Bash** is selected for your environment. Subsequent sessions will use Azure CLI in a Bash environment. Select **Copy** to copy the blocks of code, paste it into the Cloud Shell, and press **Enter** to run it.

### Sign in to Azure

Cloud Shell is automatically authenticated under the initial account signed-in with. Use the following script to sign in using a different subscription, replacing `<Subscription ID>` with your Azure Subscription ID.  [!INCLUDE [quickstarts-free-trial-note](../includes/quickstarts-free-trial-note.md)]

```azurecli-interactive
subscription="<subscriptionId>" # add subscription here

az account set -s $subscription # ...or use 'az login'
```

For more information, see [set active subscription](/cli/azure/account#az-account-set) or [sign in interactively](/cli/azure/reference-index#az-login)

### Discover available maintenance windows

When setting the maintenance window, each region has its own maintenance window options that correspond to the timezone for the region the database or pool is located.

#### Discover SQL Database and elastic pool maintenance windows

The following example returns the available maintenance windows for the *eastus2* region using the [az maintenance public-configuration list
](/cli/azure/maintenance/public-configuration#az-maintenance-public-configuration-list) command. For databases and elastic pools, set `maintenanceScope` to `SQLDB`.

   ```azurecli
   location="eastus2"

   az maintenance public-configuration list --query "[?location=='$location'&&contains(maintenanceScope,'SQLDB')]"
   ```
 
#### Set the maintenance window while creating a single database

The following example creates a new database and sets the maintenance window using the [az sql db create](/cli/azure/sql/db#az-sql-db-create) command. The `--maint-config-id` (or `-m`) must be set to a valid value for your database's region. To get valid values for your region, see [Discover available maintenance windows](#discover-available-maintenance-windows).

   ```azurecli
    # Set variables for your database
    resourceGroupName="your_resource_group_name"
    serverName="your_server_name"
    databaseName="your_db_name"

    # Set selected maintenance window
    maintenanceConfig="SQL_EastUS2_DB_1"

    # Create database
    az sql db create \
      --resource-group $resourceGroupName \
      --server $serverName \
      --name $databaseName \
      --edition GeneralPurpose \
      --family Gen5 \
      --capacity 2 \
      --maint-config-id $maintenanceConfig
   ```

### Set the maintenance window while creating an elastic pool

The following example creates a new elastic pool and sets the maintenance window using the [az sql elastic-pool create](/cli/azure/sql/elastic-pool#az-sql-elastic-pool-create) cmdlet. The maintenance window is set on the elastic pool, so all databases in the pool have the pool's maintenance window schedule. The `--maint-config-id` (or `-m`) must be set to a valid value for your pool's region. To get valid values for your region, see [Discover available maintenance windows](#discover-available-maintenance-windows).

   ```azurecli
    # Set variables for your pool
    resourceGroupName="your_resource_group_name"
    serverName="your_server_name"
    poolName="your_pool_name"

    # Set selected maintenance window
    maintenanceConfig="SQL_EastUS2_DB_2"

    # Create elastic pool
    az sql elastic-pool create \
      --resource-group $resourceGroupName \
      --server $serverName \
      --name $poolName \
      --edition GeneralPurpose \
      --family Gen5 \
      --capacity 2 \
      --maint-config-id $maintenanceConfig
   ```
 
-----

## Configure maintenance window for existing databases

When applying a maintenance window selection to a database, a brief reconfiguration (several seconds) might be experienced in some cases as Azure applies the required changes.

# [Portal](#tab/azure-portal)

The following steps set the maintenance window on an existing database or elastic pool using the Azure portal:

#### Set the maintenance window for an existing database or elastic pool

1. Navigate to the SQL database or elastic pool you want to set the maintenance window for.
1. In the **Settings** menu select **Maintenance**, then select the desired maintenance window.

   :::image type="content" source="media/maintenance-window-configure/maintenance.png" alt-text="Screenshot from the Azure portal of the SQL database Maintenance page." lightbox="media/maintenance-window-configure/maintenance.png":::
 
# [PowerShell](#tab/azure-powershell)

#### Set the maintenance window for an existing database

The following example sets the maintenance window on an existing database using the [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase) cmdlet. 
The `-MaintenanceConfigurationId` must be set to a valid value for your database's region. To get valid values for your region, see [Discover available maintenance windows](#discover-available-maintenance-windows).

   ```powershell-interactive
    # Select different maintenance window
    $maintenanceConfig = "SQL_EastUS2_DB_2"

    Write-host "Changing database maintenance window to ${maintenanceConfig} ..."
    $database = Set-AzSqlDatabase `
      -ResourceGroupName $resourceGroupName `
      -ServerName $serverName `
      -DatabaseName $databaseName `
      -MaintenanceConfigurationId $maintenanceConfig
    $database
   ```

#### Set the maintenance window on an existing elastic pool

The following example sets the maintenance window on an existing elastic pool using the [Set-AzSqlElasticPool](/powershell/module/az.sql/set-azsqlelasticpool) cmdlet. 
It's important to make sure that the `$maintenanceConfig` value is a valid value for your pool's region.  To get valid values for a region, see [Discover available maintenance windows](#discover-available-maintenance-windows).

   ```powershell-interactive
    # Select different maintenance window
    $maintenanceConfig = "SQL_EastUS2_DB_1"
    
    Write-host "Changing pool maintenance window to ${maintenanceConfig} ..."
    $pool = Set-AzSqlElasticPool `
      -ResourceGroupName $resourceGroupName `
      -ServerName $serverName `
      -ElasticPoolName $poolName `
      -MaintenanceConfigurationId $maintenanceConfig
    $pool
   ```

# [CLI](#tab/azure-cli)

The following examples show how to configure the maintenance window using Azure CLI. You can [install Azure CLI](/cli/azure/install-azure-cli), or use the Azure Cloud Shell.

#### Set the maintenance window for an existing database

The following example sets the maintenance window on an existing database using the [az sql db update](/cli/azure/sql/db#az-sql-db-update) command. The `--maint-config-id` (or `-m`) must be set to a valid value for your database's region. To get valid values for your region, see [Discover available maintenance windows](#discover-available-maintenance-windows).

   ```azurecli
    # Select different maintenance window
    maintenanceConfig="SQL_EastUS2_DB_2"

    # Update database
    az sql db update \
      --resource-group $resourceGroupName \
      --server $serverName \
      --name $databaseName \
      --maint-config-id $maintenanceConfig
   ```

#### Set the maintenance window on an existing elastic pool

The following example sets the maintenance window on an existing elastic pool using the [az sql elastic-pool update](/cli/azure/sql/elastic-pool#az-sql-elastic-pool-update) command. 
It's important to make sure that the `maintenanceConfig` value is a valid value for your pool's region.  To get valid values for a region, see [Discover available maintenance windows](#discover-available-maintenance-windows).

   ```azurecli
    # Select different maintenance window
    maintenanceConfig="SQL_EastUS2_DB_1"

    # Update pool
    az sql elastic-pool update \
      --resource-group $resourceGroupName \
      --server $serverName \
      --name $poolName \
      --maint-config-id $maintenanceConfig
   ```
 
-----

## Cleanup resources

If you create Azure SQL resources as part of this tutorial, be sure to delete unneeded resources after you're finished with them to avoid unnecessary charges.

# [Portal](#tab/azure-portal)

1. Navigate to the SQL database or elastic pool you no longer need.
1. On the **Overview** menu, select the option to delete the resource.

# [PowerShell](#tab/azure-powershell)

   ```powershell-interactive
   # Delete database
   Remove-AzSqlDatabase `
      -ResourceGroupName $resourceGroupName `
      -ServerName $serverName `
      -DatabaseName $databaseName

   # Delete elastic pool
   Remove-AzSqlElasticPool `
      -ResourceGroupName $resourceGroupName `
      -ServerName $serverName `
      -ElasticPoolName $poolName
   ```

# [CLI](#tab/azure-cli)

   ```azurecli
   az sql db delete \
      --resource-group $resourceGroupName \
      --server $serverName \
      --name $databaseName

   az sql elastic-pool delete \
      --resource-group $resourceGroupName \
      --server $serverName \
      --name $poolName
   ```

-----

## Related content

- To learn more about maintenance window, see [Maintenance window](maintenance-window.md).
- For more information, see [Maintenance window FAQ](maintenance-window-faq.yml).
- To learn about optimizing performance, see [Monitoring and performance tuning in Azure SQL Database and Azure SQL Managed Instance](monitor-tune-overview.md).
