---
title: Configure maintenance window
titleSuffix: Azure SQL Managed Instance
description: Learn how to set the time when planned maintenance should be performed when you use Azure SQL Managed Instance.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: scottkim, mathoma, urosmil
ms.date: 05/27/2024
ms.service: sql-managed-instance
ms.subservice: deployment-configuration
ms.topic: how-to
ms.custom:
  - devx-track-azurecli
  - devx-track-azurepowershell
  - azure-sql-split
monikerRange: "=azuresql||=azuresql-mi"
---
# Configure maintenance window in Azure SQL Managed Instance

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/maintenance-window-configure.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/maintenance-window-configure.md?view=azuresql-mi&preserve-view=true)

You can configure the [maintenance window](maintenance-window.md) for an Azure SQL Managed Instance during resource creation, or anytime after a resource is created.

- The *System default* maintenance window is 5PM to 8AM daily (local time of the Azure region the resource is located) to avoid peak business hours interruptions. 
- If the *System default* maintenance window is not the best time, select one of the other available maintenance windows.

For availability, see [Maintenance window feature availability in Azure SQL Managed Instance](maintenance-window.md#feature-availability).

> [!Important]
> Configuring maintenance window is a long running asynchronous operation, similar to changing the service tier of the Azure SQL resource. The resource is available during the operation, except a short reconfiguration that happens at the end of the operation and typically lasts up to 8 seconds even in case of interrupted long-running transactions. To minimize the impact of the reconfiguration you should perform the operation outside of the peak hours.

## Configure maintenance window during creation

# [Portal](#tab/azure-portal)

To configure the maintenance window when you create a SQL managed instance, set the desired **Maintenance window** on the **Additional settings** page.

### Set the maintenance window while creating a SQL managed instance

For step-by-step information on creating a new managed instance, see [Create an Azure SQL Managed Instance](../managed-instance/instance-create-quickstart.md).

   :::image type="content" source="media/maintenance-window-configure/additional-settings-sql-managed-instance.png" alt-text="Screenshot from the Azure portal. In the Create Azure SQL managed instance, Additional settings tab, the Maintenance window drop down is open.":::

# [PowerShell](#tab/azure-powershell)

The following examples show how to configure the maintenance window using Azure PowerShell. You can [install Azure PowerShell](/powershell/azure/install-az-ps), or use the Azure Cloud Shell.

#### Launch Azure Cloud Shell

The Azure Cloud Shell is a free interactive shell that you can use to run the steps in this article. It has common Azure tools preinstalled and configured to use with your account.

To open the Cloud Shell, select **Try it** from the upper right corner of a code block. You can also launch Cloud Shell in a separate browser tab by going to [https://shell.azure.com](https://shell.azure.com).

When Cloud Shell opens, verify that **PowerShell** is selected for your environment. Subsequent sessions will use Azure CLI in a PowerShell environment. Select **Copy** to copy the blocks of code, paste it into the Cloud Shell, and press **Enter** to run it.

#### Discover available maintenance windows

When setting the maintenance window, each region has its own maintenance window options that correspond to the timezone for the region.

#### Discover SQL Managed Instance maintenance windows

The following example returns the available maintenance windows for the *eastus2* region using the [Get-AzMaintenancePublicConfiguration](/powershell/module/az.maintenance/get-azmaintenancepublicconfiguration) cmdlet. For managed instances, set `MaintenanceScope` to `SQLManagedInstance`.

   ```powershell-interactive
   $location = "eastus2"

   Write-Host "Available maintenance schedules in ${location}:"
   $configurations = Get-AzMaintenancePublicConfiguration
   $configurations | ?{ $_.Location -eq $location -and $_.MaintenanceScope -eq "SQLManagedInstance"}
   ```

#### Set the maintenance window while creating a SQL managed instance

The following example creates a new managed instance and sets the maintenance window using the [New-AzSqlInstance](/powershell/module/az.sql/new-azsqlinstance) cmdlet. The maintenance window is set on the instance, so all databases in the instance have the same maintenance window schedule. For `-MaintenanceConfigurationId`, the *MaintenanceConfigName* must be a valid value for your instance's region. To get valid values for your region, see [Discover available maintenance windows](#discover-available-maintenance-windows).

   ```powershell
   New-AzSqlInstance -Name "your_mi_name" `
     -ResourceGroupName "your_resource_group_name" `
     -Location "your_mi_location" `
     -SubnetId /subscriptions/{SubID}/resourceGroups/{ResourceGroup}/providers/Microsoft.Network/virtualNetworks/{VNETName}/subnets/{SubnetName} `
     -MaintenanceConfigurationId "/subscriptions/{SubID}/providers/Microsoft.Maintenance/publicMaintenanceConfigurations/SQL_{Region}_{MaintenanceConfigName}"
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

When setting the maintenance window, each region has its own maintenance window options that correspond to the timezone for the region.
 
#### Discover SQL Managed Instance maintenance windows

The following example returns the available maintenance windows for the *eastus2* region using the [az maintenance public-configuration list
](/cli/azure/maintenance/public-configuration#az-maintenance-public-configuration-list) command. For managed instances, set `maintenanceScope` to `SQLManagedInstance`.

   ```azurecli
   az maintenance public-configuration list --query "[?location=='eastus2'&&contains(maintenanceScope,'SQLManagedInstance')]"
   ```

### Set the maintenance window while creating a SQL managed instance

The following example creates a new managed instance and sets the maintenance window using [az sql mi create](/cli/azure/sql/mi#az-sql-mi-create). The maintenance window is set on the instance, so all databases in the instance have the same maintenance window schedule. *MaintenanceConfigName* must be a valid value for your instance's region. To get valid values for your region, see [Discover available maintenance windows](#discover-available-maintenance-windows).

   ```azurecli
   az sql mi create -g mygroup -n myinstance -l mylocation -i -u myusername -p mypassword --subnet /subscriptions/{SubID}/resourceGroups/{ResourceGroup}/providers/Microsoft.Network/virtualNetworks/{VNETName}/subnets/{SubnetName} -m /subscriptions/{SubID}/providers/Microsoft.Maintenance/publicMaintenanceConfigurations/SQL_{Region}_{MaintenanceConfigName}
   ```

-----

## Configure maintenance window for existing SQL managed instances

When applying a maintenance window selection to a SQL managed instance, a brief reconfiguration (several seconds) might be experienced in some cases as Azure applies the required changes.

# [Portal](#tab/azure-portal)

The following steps set the maintenance window on an existing SQL managed instance using the Azure portal:
 
#### Set the maintenance window for an existing managed instance

1. Navigate to the SQL managed instance you want to set the maintenance window for.
1. In the **Settings** menu select **Maintenance**, then select the desired maintenance window.

    :::image type="content" source="media/maintenance-window-configure/maintenance-sql-managed-instance.png" alt-text="Screenshot of the SQL managed instance Maintenance page." lightbox="media/maintenance-window-configure/maintenance-sql-managed-instance.png":::

# [PowerShell](#tab/azure-powershell)

#### Set the maintenance window on an existing managed instance

The following example sets the maintenance window on an existing managed instance using the [Set-AzSqlInstance](/powershell/module/az.sql/set-azsqlinstance) cmdlet. 
It's important to make sure that the `$maintenanceConfig` value must be a valid value for your instance's region.  To get valid values for a region, see [Discover available maintenance windows](#discover-available-maintenance-windows).

   ```powershell-interactive
   Set-AzSqlInstance -Name "your_mi_name" `
     -ResourceGroupName "your_resource_group_name" `
     -MaintenanceConfigurationId "/subscriptions/{SubID}/providers/Microsoft.Maintenance/publicMaintenanceConfigurations/SQL_{Region}_{MaintenanceConfigName}"
   ```

# [CLI](#tab/azure-cli)

The following examples show how to configure the maintenance window using Azure CLI. You can [install Azure CLI](/cli/azure/install-azure-cli), or use the Azure Cloud Shell.
 
### Set the maintenance window on an existing managed instance

The following example sets the maintenance window using [az sql mi update](/cli/azure/sql/mi#az-sql-mi-update). The maintenance window is set on the instance, so all databases in the instance have the same maintenance window schedule. For `-MaintenanceConfigurationId`, the *MaintenanceConfigName* must be a valid value for your instance's region. To get valid values for your region, see [Discover available maintenance windows](#discover-available-maintenance-windows).

   ```azurecli
   az sql mi update -g mygroup  -n myinstance -m /subscriptions/{SubID}/providers/Microsoft.Maintenance/publicMaintenanceConfigurations/SQL_{Region}_{MaintenanceConfigName}
   ```

-----

> [!NOTE]
> For displaying user friendly names in Azure Portal, Azure SQL Managed Instance relies on [maintenance configurations](../../virtual-machines/maintenance-configurations.md) as a resource. Maintenace definitions for Azure SQL Managed Instance are part of the public maintenance configurations. There might be the situation for newly added Azure regions in which SQL Managed Instance can be used in the region, while public maintenance configurations are still being created. In that case, Azure Portal will not display the user friendly names in the dropdown and instead users will see the system names:
> - MI_1 which is equivalent for Weekday window: 10:00 PM to 6:00 AM local time, Monday - Thursday
> - MI_2 which is equivalent for Weekend window: 10:00 PM to 6:00 AM local time, Friday - Sunday

## Related content

- To learn more about maintenance window, see [Maintenance window](maintenance-window.md).
- For more information, see [Maintenance window FAQ](maintenance-window-faq.yml).
- To learn about optimizing performance, see [Monitoring and performance tuning in Azure SQL Database and Azure SQL Managed Instance](../database/monitor-tune-overview.md).
