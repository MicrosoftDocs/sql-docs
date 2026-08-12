---
title: Create a Single Database
description: Create a single database in Azure SQL Database using the Azure portal, PowerShell, or the Azure CLI.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma, randolphwest
ms.date: 01/23/2026
ms.service: azure-sql-database
ms.subservice: deployment-configuration
ms.topic: quickstart
ms.custom:
  - devx-track-azurecli
  - devx-track-azurepowershell
  - mode-ui
  - references-regions
  - sfi-image-nochange
---
# Quickstart: Create a single database - Azure SQL Database

In this quickstart, you create a [single database](single-database-overview.md) in Azure SQL Database using either the Azure portal, a PowerShell script, or an Azure CLI script. You then query the database using **Query editor** in the Azure portal.

Watch this video in the [Azure SQL Database essentials series](/shows/azure-sql-database-essentials/) for an overview of the deployment process: 
> [!VIDEO https://learn-video.azurefd.net/vod/player?id=0c5d0700-b422-4a46-99bd-84c09ba65804]

## Prerequisites

- An active Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn).
- Much of this article can be accomplished with the Azure portal alone. Optionally, use the latest version of [Azure PowerShell](/powershell/azure/install-az-ps) or [Azure CLI](/cli/azure/install-azure-cli-windows).

[!INCLUDE [modifiable-config-note](../includes/database/modifiable-config-note.md)]

### Permissions

**To create databases via Transact-SQL**: `CREATE DATABASE` permissions are necessary. To create a database a login must be either the server admin login (created when the Azure SQL Database logical server was provisioned), the Microsoft Entra admin of the server, a member of the dbmanager database role in `master`. For more information, see [CREATE DATABASE](/sql/t-sql/statements/create-database-transact-sql?view=azuresqldb-current&preserve-view=true).

**To create databases via the Azure portal, PowerShell, Azure CLI, or REST API**: Azure RBAC permissions are needed, specifically the Contributor, SQL DB Contributor, or SQL Server Contributor Azure RBAC role. For more information, see [Azure RBAC built-in roles](/azure/role-based-access-control/built-in-roles).

## Create a single database

This quickstart creates a single database in the [serverless compute tier](serverless-tier-overview.md).

[!INCLUDE [azure-sql-database-free-offer-note](../includes/azure-sql-database-free-offer-note.md)]

[!INCLUDE [resource-naming-customer-data-note](~/../reusable-content/ce-skilling/azure/includes/databases/resource-naming-customer-data-note.md)]

# [Portal](#tab/azure-portal)

To create a single database in the Azure portal:

1. Go to [Azure SQL hub at aka.ms/azuresqlhub](https://aka.ms/azuresqlhub).
1. In the resource menu, expand **Azure SQL Database** and select **SQL databases**.
1. Select the **+ Create** dropdown button and select **SQL database**.

   :::image type="content" source="media/single-database-create-quickstart/create-sql-database.png" alt-text="Screenshot from the Azure portal showing the SQL databases page, the Create button, and the SQL database option." lightbox="media/single-database-create-quickstart/create-sql-database.png":::

1. On the **Basics** tab of the **Create SQL Database** form, under **Project details**, select the desired Azure **Subscription**.
1. For **Resource group**, select **Create new**, enter *myResourceGroup*, and select **OK**.
1. For **Database name**, enter *mySampleDatabase*.
1. For **Server**, select **Create new**, and fill out the **New server** form with the following values:

    - **Server name**: Enter *mysqlserver*, and add some characters for uniqueness. We can't provide an exact server name to use because server names must be globally unique for all servers in Azure, not just unique within a subscription. The Azure portal lets you know if the name you type is available or not.
    - **Location**: Select a location from the dropdown list.
    - **Authentication method**: Select **Use SQL authentication**.
    - **Server admin login**: Enter *azureuser*.
    - **Password**: Enter a password that meets requirements, and enter it again in the **Confirm password** field.

   [!INCLUDE [server-admin-login-security-note](../includes/server-admin-login-security-note.md)]

   Select **OK**.

1. Leave **Want to use SQL elastic pool** set to **No**.
1. For **Workload environment**, specify **Development** for this exercise.

The Azure portal provides a **Workload environment** option that helps to preset some configuration settings. These settings can be overridden. This option applies to the **Create SQL Database** portal page only. Otherwise, the **Workload environment** option has no impact on licensing or other database configuration settings.

   - Choosing the **development** workload environment sets a few options, including: 
   - **Backup storage redundancy** option is locally redundant storage. Locally redundant storage incurs less cost and is appropriate for pre-production environments that do not require the redundance of zone- or geo-replicated storage. 
   - **Compute + storage** is General Purpose, Serverless with a single vCore. By default, there is a [one-hour auto-pause delay](serverless-tier-overview.md?view=azuresql&preserve-view=true&tabs=general-purpose#performance-configuration).
   - Choosing the **Production** workload environment sets: 
   - **Backup storage redundancy** is geo-redundant storage, the default. 
   - **Compute + storage** is General Purpose, Provisioned with 2 vCores and 32 GB of storage. This can be further modified in the next step.

1. Under **Compute + storage**, select **Configure database**.
1. This quickstart uses a serverless database, so leave **Service tier** set to **General Purpose (Most budget-friendly, serverless compute)** and set **Compute tier** to **Serverless**. Select **Apply**.
1. Under **Backup storage redundancy**, choose a redundancy option for the storage account where your backups will be saved. To learn more, see [backup storage redundancy](automated-backups-overview.md#backup-storage-redundancy).
1. Select **Next: Networking**.

   :::image type="content" source="media/single-database-create-quickstart/new-sql-database-basics.png" alt-text="Screenshot of the Create SQL Database page, Basic tab from the Azure portal." lightbox="media/single-database-create-quickstart/new-sql-database-basics.png":::

1. On the **Networking** tab, for **Connectivity method**, select **Public endpoint**.
1. For **Firewall rules**, set **Add current client IP address** to **Yes**. Leave **Allow Azure services and resources to access this server** set to **No**.

   :::image type="content" source="media/single-database-create-quickstart/networking.png" alt-text="Screenshot of the Azure portal showing the networking tab for firewall rules." lightbox="media/single-database-create-quickstart/networking.png":::

1. Under **Connection policy**, choose the **Default** [connection policy](connectivity-architecture.md#connection-policy), and leave the **Minimum TLS version** at the default of TLS 1.2.
1. Select **Next: Security**.

   :::image type="content" source="media/single-database-create-quickstart/networking-connections.png" alt-text="Screenshot that shows the networking tab for policy and encryption." lightbox="media/single-database-create-quickstart/networking-connections.png":::

1. On the **Security** page, you can choose to start a free trial of [Microsoft Defender for SQL](azure-defender-for-sql.md), as well as configure [Ledger](/sql/relational-databases/security/ledger/ledger-overview), [Managed identities](/azure/active-directory/managed-identities-azure-resources/overview) and [Azure SQL transparent data encryption with customer-managed key](transparent-data-encryption-byok-overview.md) if you desire. Select **Next: Additional settings**.
1. On the **Additional settings** tab, in the **Data source** section, for **Use existing data**, select **Sample**. This creates an `AdventureWorksLT` sample database so there's some tables and data to query and experiment with, as opposed to an empty blank database. You can also configure [database collation](/sql/t-sql/statements/collations) and a [maintenance window](maintenance-window.md).

1. Select **Review + create**.

   :::image type="content" source="media/single-database-create-quickstart/additional-settings.png" alt-text="Screenshot of the Azure portal showing the Additional settings tab." lightbox="media/single-database-create-quickstart/additional-settings.png":::

1. On the **Review + create** page, after reviewing, select **Create**.

# [Azure CLI](#tab/azure-cli)

The Azure CLI code blocks in this section create a resource group, server, single database, and server-level IP firewall rule for access to the server. Make sure to record the generated resource group and server names, so you can manage these resources later.

First, install the latest [Azure CLI](/cli/azure/install-azure-cli-windows).

[!INCLUDE [quickstarts-free-trial-note](../includes/quickstarts-free-trial-note.md)]

[!INCLUDE [azure-cli-prepare-your-environment.md](~/../reusable-content/azure-cli/azure-cli-prepare-your-environment-h3.md)]

[!INCLUDE [cli-launch-cloud-shell-sign-in.md](../includes/cli-launch-cloud-shell-sign-in.md)]

### Set parameter values

The following values are used in subsequent commands to create the database and required resources. Server names need to be globally unique across all of Azure so the $RANDOM function is used to create the server name.

Change the location as appropriate for your environment. Replace `0.0.0.0` with the IP address range that matches your specific environment. Use the public IP address of the computer you're using to restrict access to the server to only your IP address.

:::code language="azurecli" source="~/../azure_cli_scripts/sql-database/create-and-configure-database/create-and-configure-database.sh" id="SetParameterValues":::

[!INCLUDE [server-admin-login-security-note](../includes/server-admin-login-security-note.md)]

### Create a resource group

Create a resource group with the [az group create](/cli/azure/group) command. An Azure resource group is a logical container into which Azure resources are deployed and managed. The following example creates a resource group named *myResourceGroup* in the *eastus* Azure region:

:::code language="azurecli" source="~/../azure_cli_scripts/sql-database/create-and-configure-database/create-and-configure-database.sh" id="CreateResourceGroup":::

### Create a server

Create a server with the [az sql server create](/cli/azure/sql/server) command.

:::code language="azurecli" source="~/../azure_cli_scripts/sql-database/create-and-configure-database/create-and-configure-database.sh" id="CreateServer":::

### Configure a server-based firewall rule

Create a firewall rule with the [az sql server firewall-rule create](/cli/azure/sql/server/firewall-rule) command.

:::code language="azurecli" source="~/../azure_cli_scripts/sql-database/create-and-configure-database/create-and-configure-database.sh" id="CreateFirewallRule":::

### Create a single database

Create a database with the [az sql db create](/cli/azure/sql/db) command in the [serverless compute tier](serverless-tier-overview.md).

```azurecli
echo "Creating $database in serverless tier"
az sql db create \
    --resource-group $resourceGroup \
    --server $server \
    --name $database \
    --sample-name AdventureWorksLT \
    --edition GeneralPurpose \
    --compute-model Serverless \
    --family Gen5 \
    --capacity 2
```

# [PowerShell](#tab/azure-powershell)

You can create a resource group, server, and single database using Azure PowerShell.

First, install the latest [Azure PowerShell](/powershell/azure/install-az-ps).

### Launch Azure Cloud Shell

The Azure Cloud Shell is a free interactive shell that you can use to run the steps in this article. It has common Azure tools preinstalled and configured to use with your account.

To open the Cloud Shell, select **Try it** from the upper right corner of a code block. You can also launch Cloud Shell in a separate browser tab by going to [https://shell.azure.com](https://portal.azure.com/#cloudshell).

When Cloud Shell opens, verify that **PowerShell** is selected for your environment. Subsequent sessions use Azure CLI in a PowerShell environment. Select **Copy** to copy the blocks of code, paste it into the Cloud Shell, and press **Enter** to run it.

### Set parameter values

The following values are used in subsequent commands to create the database and required resources. Server names need to be globally unique across all of Azure so the [Get-Random](/powershell/module/microsoft.powershell.utility/get-random) cmdlet is used to create the server name.

[!INCLUDE [server-admin-login-security-note](../includes/server-admin-login-security-note.md)]

In the following code snippet:

1. Replace `0.0.0.0` in the ip address range to match your specific environment.
1. Replace `<strong password here>` with a strong password for your `adminLogin`.

```azurepowershell-interactive
   # Set variables for your server and database
   $resourceGroupName = "myResourceGroup"
   $location = "eastus"
   $adminLogin = "azureuser"
   $password = "<strong password here>"
   $serverName = "mysqlserver-$(Get-Random)"
   $databaseName = "mySampleDatabase"

   # The ip address range that you want to allow to access your server
   $startIp = "0.0.0.0"
   $endIp = "0.0.0.0"

   # Show randomized variables
   Write-host "Resource group name is" $resourceGroupName
   Write-host "Server name is" $serverName
```

### Create resource group

Create an Azure resource group with [New-AzResourceGroup](/powershell/module/az.resources/new-azresourcegroup). A resource group is a logical container into which Azure resources are deployed and managed.

```azurepowershell-interactive
   Write-host "Creating resource group..."
   $resourceGroup = New-AzResourceGroup -Name $resourceGroupName -Location $location -Tag @{Owner="SQLDB-Samples"}
   $resourceGroup
```

### Create a server

Create a server with the [New-AzSqlServer](/powershell/module/az.sql/new-azsqlserver) cmdlet.

```azurepowershell-interactive
  Write-host "Creating primary server..."
   $server = New-AzSqlServer -ResourceGroupName $resourceGroupName `
      -ServerName $serverName `
      -Location $location `
      -SqlAdministratorCredentials $(New-Object -TypeName System.Management.Automation.PSCredential `
      -ArgumentList $adminLogin, $(ConvertTo-SecureString -String $password -AsPlainText -Force))
   $server
```

### Create a firewall rule

Create a server firewall rule with the [New-AzSqlServerFirewallRule](/powershell/module/az.sql/new-azsqlserverfirewallrule) cmdlet.

```azurepowershell-interactive
   Write-host "Configuring server firewall rule..."
   $serverFirewallRule = New-AzSqlServerFirewallRule -ResourceGroupName $resourceGroupName `
      -ServerName $serverName `
      -FirewallRuleName "AllowedIPs" -StartIpAddress $startIp -EndIpAddress $endIp
   $serverFirewallRule
```

### Create a single database with PowerShell

Create a single database with the [New-AzSqlDatabase](/powershell/module/az.sql/new-azsqldatabase) cmdlet.

```azurepowershell-interactive
   Write-host "Creating a gen5 2 vCore serverless database..."
   $database = New-AzSqlDatabase  -ResourceGroupName $resourceGroupName `
      -ServerName $serverName `
      -DatabaseName $databaseName `
      -Edition GeneralPurpose `
      -ComputeModel Serverless `
      -ComputeGeneration Gen5 `
      -VCore 2 `
      -MinimumCapacity 2 `
      -SampleName "AdventureWorksLT"
   $database
```

---

## Query the database

Once your database is created, you can use the **Query editor (preview)** in the Azure portal to connect to the database and query data. For more information, see [Azure portal query editor for Azure SQL Database](query-editor.md).

1. Go to [Azure SQL hub at aka.ms/azuresqlhub](https://aka.ms/azuresqlhub).
1. In the resource menu, expand **Azure SQL Database** and select **SQL databases**. Select your database from the list.
1. In the resource menu, select **Query editor (preview)**.
1. Enter your **SQL authentication** server admin login information or use **Microsoft Entra authentication**.

    [!INCLUDE [entra-id](../includes/entra-id.md)]

    :::image type="content" source="media/single-database-create-quickstart/query-editor-login.png" alt-text="Screenshot of the Query editor sign-in page in the Azure portal." lightbox="media/single-database-create-quickstart/query-editor-login.png":::

1. Enter the following query in the **Query editor** pane.

   ```sql
   SELECT TOP 20 pc.Name as CategoryName, p.name as ProductName
   FROM SalesLT.ProductCategory pc
   JOIN SalesLT.Product p
   ON pc.productcategoryid = p.productcategoryid;
   ```

1. Select **Run**, and then review the query results in the **Results** pane.

    :::image type="content" source="media/single-database-create-quickstart/query-editor-results.png" alt-text="Screenshot of Query editor results." lightbox="media/single-database-create-quickstart/query-editor-results.png":::

1. Close the **Query editor** page, and select **OK** when prompted to discard your unsaved edits.

## Clean up resources

Keep the resource group, server, and single database to go on to the next steps, and learn how to connect and query your database with different methods.

When you're finished using these resources, you can delete the resource group you created, which will also delete the server and single database within it.

# [Portal](#tab/azure-portal)

To delete **myResourceGroup** and all its resources using the Azure portal:

1. In the portal, search for and select **Resource groups**, and then select **myResourceGroup** from the list.
1. On the resource group page, select **Delete resource group**.
1. Under **Type the resource group name**, enter *myResourceGroup*, and then select **Delete**.

# [Azure CLI](#tab/azure-cli)

Use the following command to remove the resource group and all resources associated with it using the [az group delete](/cli/azure/vm/extension#az-vm-extension-set) command - unless you have an ongoing need for these resources. Some of these resources might take a while to create, as well as to delete.

```azurecli
az group delete --name $resourceGroup
```

# [PowerShell](#tab/azure-powershell)

To delete the resource group and all its resources, run the following PowerShell cmdlet, using the name of your resource group:

```azurepowershell-interactive
Remove-AzResourceGroup -Name $resourceGroupName
```

---

## Next step

Want to optimize and save on your cloud spending?

> [!div class="nextstepaction"]
> [Start analyzing costs with Cost Management](/azure/cost-management-billing/costs/quick-acm-cost-analysis?WT.mc_id=costmanagementcontent_docsacmhorizontal_-inproduct-learn)

## Related content

- [Connect and query your database](connect-query-content-reference-guide.md)
- [Quickstart: Use SSMS to connect to and query Azure SQL Database](connect-query-ssms.md)
- [Connect and with the MSSQL extension for Visual Studio Code](/sql/tools/visual-studio-code-extensions/mssql/connect-database-visual-studio-code)
- [Modifiable configuration reference for Azure SQL Database](modifiable-configuration-reference.md)
