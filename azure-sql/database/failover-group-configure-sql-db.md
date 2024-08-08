---
title: Configure a failover group
titleSuffix: Azure SQL Database
description: Learn how to configure a failover group for a single or pooled database in Azure SQL Database by using the Azure portal, PowerShell, and the Azure CLI.
author: rajeshsetlem
ms.author: rsetlem
ms.reviewer: wiassaf, mathoma, randolphwest
ms.date: 05/21/2024
ms.service: azure-sql-database
ms.subservice: high-availability
ms.topic: how-to
ms.custom:
  - azure-sql-split
  - devx-track-azurecli
zone_pivot_groups: azure-sql-deployment-option-single-elastic
---
# Configure a failover group for Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](failover-group-configure-sql-db.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/failover-group-configure-sql-mi.md?view=azuresql-mi&preserve-view=true)

This article teaches you how to configure a [failover group](failover-group-sql-db.md) for single and pooled databases in Azure SQL Database by using the Azure portal, Azure PowerShell and the Azure CLI.

::: zone pivot="azure-sql-single-db"

For end-to-end scripts, review how to add a single database to a failover group with [Azure PowerShell](scripts/add-database-to-failover-group-powershell.md) or [the Azure CLI](scripts/add-database-to-failover-group-cli.md).

## Prerequisites

Consider the following prerequisites to create your failover group for a single database:

- Your primary database should already be created. [Create single database](single-database-create-quickstart.md) to get started.
- If your secondary server already exists in a different region to the primary server, the server login and firewall settings must match that of your primary server.

## Create failover group

You can create your failover group and add a single database to it by using the Azure portal, PowerShell, and the Azure CLI.

> [!IMPORTANT]  
> If you need to delete a secondary database after it's been added to a failover group, remove it from the failover group before deleting the database. Deleting a secondary database before it is removed from the failover group can cause unpredictable behavior.

# [Portal](#tab/azure-portal)

To create your failover group and add your single database to it by using the Azure portal, follow these steps:

1. If you know the [logical server](logical-servers.md) that hosts your database, go directly to it in the [Azure portal](https://portal.azure.com). If you need to find the server, follow these steps:
    1. Select **Azure SQL** in the service menu. If **Azure SQL** isn't in the list, select **All services**, then type `Azure SQL` in the search box. (Optional) Select the star next to **Azure SQL** to favorite it and add it as an item in the service menu.
    1. On the **Azure SQL** page, find the database you want to add to a failover group and select it to open the **SQL database** pane.
    1. On the **Overview** pane of **SQL database**, select the name of the server under **Server name** to open the **SQL server** pane.

    :::image type="content" source="media/failover-group-configure-sql-db/open-sql-db-server.png" alt-text="Screenshot to open the server for a single database in the Azure portal.":::

1. On the **SQL server** resource menu, select **Failover groups** under **Data management**. Select **+ Add group** to open the **Failover group** page where you can create a new failover group.

   :::image type="content" source="media/failover-group-configure-sql-db/sqldb-add-new-failover-group.png" alt-text="Screenshot highlighting the Add new failover group option on the failover groups page in the Azure portal.":::

1. On the **Failover Group** page:
    1. Provide a **Failover group name**.
    1. Choose an existing secondary server, or create a new server by selecting **Create new** under **Server**. The secondary server in the failover group must be in a different region than the primary server.
    1. Select **Configure database** to open the **Databases for failover group** page.

   :::image type="content" source="media/failover-group-configure-sql-db/add-sqldb-to-failover-group.png" alt-text="Screenshot of the failover group pane in the Azure portal.":::

1. On the **Databases for failover group** page:
    1. Choose the databases you want to add to the failover group (#1 in screenshot).
    1. (Optional) Choose **Yes** if you intend to designate these databases as [standby replicas](standby-replica-how-to-configure.md) to use for _only_ disaster recovery (#2 in screenshot). Check the box to confirm that you'll use the replica for standby.
    1. Use **Select** to save your database selection and go back to the **Failover group** page (not visible in screenshot).

   :::image type="content" source="media/failover-group-configure-sql-db/select-databases-for-failover-group.png" alt-text="Screenshot of the databases for failover group pane in the Azure portal.":::

1. Use **Create** on the **Failover group** page to create your failover group.

# [PowerShell](#tab/azure-powershell)

Create your failover group with the [New-AzSqlDatabaseFailoverGroup](/powershell/module/az.sql/new-azsqldatabasefailovergroup) PowerShell command:

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/database/failover-groups/add-single-db-to-failover-group-az-ps.ps1" id="CreateFailoverGroup":::

Use the [Get-AzSqlDatabase](/powershell/module/az.sql/get-azsqldatabase) and [Add-AzSqlDatabaseToFailoverGroup](/powershell/module/az.sql/add-azsqldatabasetofailovergroup) commands to add the database to the failover group:

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/database/failover-groups/add-single-db-to-failover-group-az-ps.ps1" id="AddDBtoFailoverGroup":::

> [!NOTE]  
> It's possible to deploy your failover group across subscriptions by using the `-PartnerSubscriptionId` parameter in Azure Powershell starting with [Az.SQL 3.11.0](https://www.powershellgallery.com/packages/Az.Sql/3.11.0). To learn more, review the following [Example](/powershell/module/az.sql/new-azsqldatabasefailovergroup#example-3).

# [Azure CLI](#tab/azure-cli)

Create your failover group and add databases to it with the [az sql failover-group create](/cli/azure/sql/failover-group#az-sql-failover-group-create) Azure CLI command:

:::code language="azurecli" source="~/../azure_cli_scripts/sql-database/failover-groups/add-single-db-to-failover-group-az-cli.sh" id="CreateFailoverGroup":::

---

## Test planned failover

Test failover of your failover group with no data loss using the Azure portal or PowerShell.

# [Portal](#tab/azure-portal)

To test failover of your failover group by using the Azure portal, follow these steps:

1. If you know the [logical server](logical-servers.md) that hosts your database, go directly to it in the [Azure portal](https://portal.azure.com). If you need to find the server, follow these steps:
    1. Select **Azure SQL** in the service menu. If **Azure SQL** isn't in the list, select **All services**, then type `Azure SQL` in the search box. (Optional) Select the star next to **Azure SQL** to favorite it and add it as an item in the service menu.
    1. On the **Azure SQL** page, find the database to want to test failover for and select it to open the **SQL database** pane.
    1. On the **Overview** pane of **SQL database**, select the name of the server under **Server name** to open the **SQL server** pane.

    :::image type="content" source="media/failover-group-configure-sql-db/open-sql-db-server.png" alt-text="Screenshot to open the server for a single database in the Azure portal.":::

1. On the **SQL server** resource menu, select **Failover groups** under **Data management**, and then choose an existing failover group to open the **Failover group** page.

   :::image type="content" source="media/failover-group-configure-sql-db/select-failover-group.png" alt-text="Screenshot shows Failover groups where you can select a failover group for your SQL Server.":::

1. On the **Failover group** page:
    1. Review which server is primary and which server is secondary.
    1. Select **Failover** from the command bar to fail over your failover group containing your database.
    1. Select **Yes** on the warning that notifies you that TDS sessions will be disconnected.

   :::image type="content" source="media/failover-group-configure-sql-db/failover-sql-db.png" alt-text="Screenshot of the Failover group page in the Azure portal with failover selected.":::

1. Review which server is now primary and which server is secondary. Once failover succeeds, the two servers swap roles, so that the former primary becomes the secondary.
1. (Optional) Select **Failover** again to fail the servers back to their original roles.

# [PowerShell](#tab/azure-powershell)

Test failover of your failover group by using PowerShell.

### Verify the roles of each server

Use the [Get-AzSqlDatabaseFailoverGroup](/powershell/module/az.sql/get-azsqldatabasefailovergroup) command to confirm the roles of each server.

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/database/failover-groups/add-single-db-to-failover-group-az-ps.ps1" id="CheckRole":::

### Fail over to the secondary server

Use the [Switch-AzSqlDatabaseFailoverGroup](/powershell/module/az.sql/switch-azsqldatabasefailovergroup) to fail over to the secondary server.

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/database/failover-groups/add-single-db-to-failover-group-az-ps.ps1" id="Failover":::

### Revert failover group back to the primary server

Use the [Switch-AzSqlDatabaseFailoverGroup](/powershell/module/az.sql/switch-azsqldatabasefailovergroup) command to fail back to the primary server.

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/database/failover-groups/add-single-db-to-failover-group-az-ps.ps1" id="FailBack":::

# [Azure CLI](#tab/azure-cli)

Test failover by using the Azure CLI.

### Verify the roles of each server

Use the [az sql failover-group show](/cli/azure/sql/failover-group#az-sql-failover-group-show) command to confirm the roles of each server.

:::code language="azurecli" source="~/../azure_cli_scripts/sql-database/failover-groups/add-single-db-to-failover-group-az-cli.sh" id="VerifyRole":::

### Fail over to the secondary server

Use the [az sql failover-group set-primary](/cli/azure/sql/failover-group#az-sql-failover-group-set-primary) to fail over to the secondary server. Use the [az sql failover-group show](/cli/azure/sql/failover-group#az-sql-failover-group-show) command to verify a successful failover.

:::code language="azurecli" source="~/../azure_cli_scripts/sql-database/failover-groups/add-single-db-to-failover-group-az-cli.sh" id="FailingOver":::

### Revert failover group back to the primary server

Use the [az sql failover-group set-primary](/cli/azure/sql/failover-group#az-sql-failover-group-set-primary) command to fail back to the primary server.

:::code language="azurecli" source="~/../azure_cli_scripts/sql-database/failover-groups/add-single-db-to-failover-group-az-cli.sh" id="FailingBack":::

---

::: zone-end

::: zone pivot="azure-sql-elastic-pool"

For end-to-end scripts, review how to add an elastic pool to a failover group with [Azure PowerShell](scripts/add-elastic-pool-to-failover-group-powershell.md) or [the Azure CLI](scripts/add-elastic-pool-to-failover-group-cli.md).

## Prerequisites

Consider the following prerequisites for creating your failover group for a pooled database:

- Your primary elastic pool should already exist. [Create elastic pool](elastic-pool-overview.md#create-a-new-sql-database-elastic-pool-by-using-the-azure-portal) to get started.
- If your secondary server already exists, the server login and firewall settings must match that of your primary server.

## Create failover group

Create the failover group for your elastic pool by using the Azure portal, PowerShell, or the Azure CLI.

> [!IMPORTANT]  
> If you need to delete a secondary database after its been added to a failover group, remove it from the failover group before deleting the database. Deleting a secondary database before it is removed from the failover group can cause unpredictable behavior.

# [Portal](#tab/azure-portal)

To create your failover group and add your elastic pool to it by using the Azure portal, follow these steps:

1. Go to the [Create SQL Elastic pool](https://portal.azure.com/#create/Microsoft.SQLElasticDatabasePool) page in the Azure portal. Create an elastic pool that:
    - Has the same name as the elastic pool on the primary server.
    - Uses a secondary server you intend to use for the failover group. The secondary server must be in a region different to the primary server, and the server login and firewall settings must match that of your primary server. Create a new server if the secondary server doesn't already exist.

1. If you know the [logical server](logical-servers.md) that hosts your primary elastic pool, go directly to it in the [Azure portal](https://portal.azure.com). If you need to find the server, follow these steps:
    1. Select **Azure SQL** in the service menu. If **Azure SQL** isn't in the list, select **All services**, then type `Azure SQL` in the search box. (Optional) Select the star next to **Azure SQL** to favorite it and add it as an item in the service menu.
    1. On the **Azure SQL** page, find the elastic pool you want to add to the failover group and select it to open the **SQL elastic pool** pane.
    1. On the **Overview** pane of **SQL elastic pool**, select the name of the server under **Server name** to open the **SQL server** pane.

   :::image type="content" source="media/failover-group-configure-sql-db/server-for-elastic-pool.png" alt-text="Screenshot selecting the server for the elastic pool in the Azure portal.":::

1. On the **SQL server** resource menu, select **Failover groups** under **Data management**. Select **+ Add group** to open the **Failover group** page where you can create a new failover group.

   :::image type="content" source="media/failover-group-configure-sql-db/sqldb-pool-add-new-failover-group.png" alt-text="Screenshot of the failover groups page in the Azure portal.":::

1. On the **Failover Group** page:
    1. Provide a **Failover group name**.
    1. Choose an existing secondary server. The secondary server in the failover group must be in a different region than the primary server, and contain an elastic pool with the same name as the primary server.
    1. Select **Configure database** to open the **Databases for failover group** page.

   :::image type="content" source="media/failover-group-configure-sql-db/add-elastic-pool-to-failover-group.png" alt-text="Screenshot to add elastic pool to failover group in the Azure portal.":::

1. On the **Databases for failover group** page, choose the pooled databases you want to add to the failover group. Use **Select** to save your database selection and go back to the **Failover group** page.

   :::image type="content" source="media/failover-group-configure-sql-db/select-pool-for-failover-group.png" alt-text="Screenshot of the databases for failover group pane in the Azure portal.":::

1. Select **Create** on the **Failover group** page to create your failover group. Adding the elastic pool to the failover group automatically starts the geo-replication process.

# [PowerShell](#tab/azure-powershell)

Create your failover group and add your elastic pool to it by using PowerShell.

Use the [New-AzSqlDatabaseFailoverGroup](/powershell/module/az.sql/new-azsqldatabasefailovergroup) to create the failover group:

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/database/failover-groups/add-elastic-pool-to-failover-group-az-ps.ps1" id="CreateFailoverGroup":::

Use the [Get-AzSqlDatabaseFailoverGroup](/powershell/module/az.sql/get-azsqldatabasefailovergroup) and [Get-AzSqlElasticPoolDatabase](/powershell/module/az.sql/get-azsqlelasticpooldatabase) commands to add the database to the failover group:

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/database/failover-groups/add-elastic-pool-to-failover-group-az-ps.ps1" id="AddPooltoFailoverGroup":::

# [Azure CLI](#tab/azure-cli)

In this step, create your failover group and add your database to it by using the Azure CLI.

Use the [az sql failover-group create](/cli/azure/sql/failover-group#az-sql-failover-group-create) command to create a failover group.

:::code language="azurecli" source="~/../azure_cli_scripts/sql-database/failover-groups/add-elastic-pool-to-failover-group-az-cli.sh" id="CreateFailoverGroup":::

Use the [az sql failover-group update](/cli/azure/sql/failover-group#az-sql-failover-group-update) command to add a database to the failover group.

:::code language="azurecli" source="~/../azure_cli_scripts/sql-database/failover-groups/add-elastic-pool-to-failover-group-az-cli.sh" id="AddDatabaseToFailoverGroup":::

---

## Test planned failover

Test failover of your elastic pool with no data loss by using the Azure portal, PowerShell, or the Azure CLI.

# [Portal](#tab/azure-portal)

Fail your failover group over to the secondary server, and then fail back using the Azure portal.

1. If you know the [logical server](logical-servers.md) that hosts your primary elastic pool, go directly to it in the [Azure portal](https://portal.azure.com). If you need to find the server, follow these steps:
    1. Select **Azure SQL** in the service menu. If **Azure SQL** isn't in the list, select **All services**, then type `Azure SQL` in the search box. (Optional) Select the star next to **Azure SQL** to favorite it and add it as an item in the service menu.
    1. On the **Azure SQL** page, find the elastic pool you want to add to the failover group and select it to open the **SQL elastic pool** pane.
    1. On the **Overview** pane of **SQL elastic pool**, select the name of the server under **Server name** to open the **SQL server** pane.

   :::image type="content" source="media/failover-group-configure-sql-db/server-for-elastic-pool.png" alt-text="Screenshot selecting the server for the elastic pool in the Azure portal.":::

1. On the **SQL server** resource menu, select **Failover groups** under **Data management**, and then choose an existing failover group to open the **Failover group** page.

   :::image type="content" source="media/failover-group-configure-sql-db/select-failover-group.png" alt-text="Screenshot shows Failover groups where you can select a failover group for your SQL Server.":::

1. On the **Failover group** page:
    1. Review which server is primary and which server is secondary.
    1. Select **Failover** from the command bar to fail over your failover group containing your database.
    1. Select **Yes** on the warning that notifies you that TDS sessions will be disconnected.

   :::image type="content" source="media/failover-group-configure-sql-db/failover-sql-db.png" alt-text="Screenshot selecting fail over on the Failover groups page in the Azure portal.":::

1. Review which server is now primary and which server is secondary. Once failover succeeds, the two servers swap roles, so that the former primary becomes the secondary.
1. (Optional) Select **Failover** again to fail the servers back to their original roles.

# [PowerShell](#tab/azure-powershell)

Test failover of your failover group using PowerShell by using the following PowerShell commands:

### Verify the roles of each server

Use the [Get-AzSqlDatabaseFailoverGroup](/powershell/module/az.sql/get-azsqldatabasefailovergroup) command to confirm the roles of each server.

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/database/failover-groups/add-elastic-pool-to-failover-group-az-ps.ps1" id="CheckRole":::

### Fail over to the secondary server

Use the [Switch-AzSqlDatabaseFailoverGroup](/powershell/module/az.sql/switch-azsqldatabasefailovergroup) to fail over to the secondary server.

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/database/failover-groups/add-elastic-pool-to-failover-group-az-ps.ps1" id="Failover":::

### Revert failover group back to the primary server

Use the [Switch-AzSqlDatabaseFailoverGroup](/powershell/module/az.sql/switch-azsqldatabasefailovergroup) command to fail back to the primary server.

:::code language="powershell" source="~/../azure_powershell_scripts/azure-sql/database/failover-groups/add-elastic-pool-to-failover-group-az-ps.ps1" id="FailBack":::

# [Azure CLI](#tab/azure-cli)

Test failover using the Azure CLI.

### Verify the roles of each server

Use the [az sql failover-group show](/cli/azure/sql/failover-group#az-sql-failover-group-show) command to confirm the roles of each server in the failover group.

:::code language="azurecli" source="~/../azure_cli_scripts/sql-database/failover-groups/add-elastic-pool-to-failover-group-az-cli.sh" id="VerifyRoles":::

### Fail over to the secondary server

Use the [az sql failover-group set-primary](/cli/azure/sql/failover-group#az-sql-failover-group-set-primary) command to fail over to the secondary server. Use the [az sql failover-group show](/cli/azure/sql/failover-group#az-sql-failover-group-show) command to verify a successful failover.

:::code language="azurecli" source="~/../azure_cli_scripts/sql-database/failover-groups/add-elastic-pool-to-failover-group-az-cli.sh" id="FailingOver":::

### Revert failover group back to the primary server

Use the [az sql failover-group set-primary](/cli/azure/sql/failover-group#az-sql-failover-group-set-primary) command to fail back to the primary server.

:::code language="azurecli" source="~/../azure_cli_scripts/sql-database/failover-groups/add-elastic-pool-to-failover-group-az-cli.sh" id="FailingBack":::

---

::: zone-end

## Modify existing failover group

You can add or remove databases from an existing failover group, or edit failover group configuration settings by using the Azure portal, PowerShell, and the Azure CLI.

### [Portal](#tab/azure-portal)

To make changes to an existing failover group by using the Azure portal, follow these steps:

1. If you know the [logical server](logical-servers.md) that hosts your database, or elastic pool, go directly to it in the [Azure portal](https://portal.azure.com). If you need to find the server, follow these steps:
    1. Select **Azure SQL** in the service menu. If **Azure SQL** isn't in the list, select **All services**, then type `Azure SQL` in the search box. (Optional) Select the star next to **Azure SQL** to favorite it and add it as an item in the service menu.
    1. On the **Azure SQL** page, find the database or elastic pool you want to modify and select it to open the **SQL database** or **SQL elastic pool** pane.
    1. On the **Overview** pane for **SQL database** or **SQL elastic pool**, select the name of the server under **Server name** to open the **SQL server** pane.
1. On the **SQL server** resource menu, select **Failover groups** under **Data management**, and then choose an existing failover group to open the **Failover group** page.

   :::image type="content" source="media/failover-group-configure-sql-db/select-failover-group.png" alt-text="Screenshot shows Failover groups where you can select a failover group for your SQL server.":::

1. On the **Failover group** page, use the command bar:
    1. To add a database, select **Add databases** to open the **Add databases to failover group** pane and then expand **#Databases** to display the list of databases on the primary server. Check the box next to the database(s) you want to add to the failover group, and then use **Select** to save your changes and add your database(s).
    1. To remove a database, select **Remove databases** to open the **Remove databases from failover group** pane and then expand **#Databases** to list the databases in the failover group. Check the box next to the database(s) you want to remove from the failover group, and then use **Select** to save your changes and remove your database(s).
    1. To edit the failover policy, or configure a grace period, select **Edit configuration** to open the **Edit configurations Failover groups** pane and modify your settings. Use **Select** to save your changes.

   :::image type="content" source="media/failover-group-configure-sql-db/modify-existing-failover-group.png" alt-text="Screenshot of the failover group page in the Azure portal with the command bar highlighted.":::

### [PowerShell](#tab/azure-powershell)

To modify an existing failover group by using PowerShell:

- Use the [Add-AzSqlDatabaseToFailoverGroup](/powershell/module/az.sql/add-azsqldatabasetofailovergroup) command to add databases to a failover group.
- Use the [Remove-AzSqlDatabaseFromFailoverGroup](/powershell/module/az.sql/remove-azsqldatabasefromfailovergroup) command to remove databases from a failover group.
- Use the [Set-AzSqlDatabaseFailoverGroup](/powershell/module/az.sql/set-azsqldatabasefailovergroup) command to modify failover group configuration settings.

### [Azure CLI](#tab/azure-cli)

To modify an existing failover group by using the Azure CLI, use the [az sql failover-group update](/cli/azure/sql/failover-group#az-sql-failover-group-update) command to add or remove databases, or modify configuration settings.

---

## Use Private Link

Using a private link allows you to associate a logical server to a specific private IP address within the virtual network and subnet.

To use a private link with your failover group, do the following:

1. Ensure your primary and secondary servers are in a [paired region](/azure/availability-zones/cross-region-replication-azure).
1. Create the virtual network and subnet in each region to host private endpoints for primary and secondary servers such that they have nonoverlapping IP address spaces. For example, the primary virtual network address range of 10.0.0.0/16 and the secondary virtual network address range of 10.0.0.1/16 overlaps. For more information about virtual network address ranges, see the blog [designing Azure virtual networks](https://devblogs.microsoft.com/premier-developer/understanding-cidr-notation-when-designing-azure-virtual-networks-and-subnets/).
1. Create a [private endpoint and Azure Private DNS zone for the primary server](/azure/private-link/create-private-endpoint-portal#create-a-private-endpoint).
1. Create a private endpoint for the secondary server as well, but this time choose to reuse the same Private DNS zone that was created for the primary server.
1. Once the private link is established, you can create the failover group following the steps outlined previously in this article.

## Locate listener endpoint

Once your failover group is configured, update the connection string for your application to the listener endpoint. This keeps your application connected to the failover group listener, rather than the primary database, or elastic pool. That way, you don't have to manually update the connection string every time your database entity fails over, and traffic is routed to whichever entity is currently primary.

The listener endpoint is in the form of `fog-name.database.windows.net`, and is visible in the Azure portal when viewing the failover group:

:::image type="content" source="media/failover-group-configure-sql-db/find-failover-group-connection-string.png" alt-text="Screenshot showing the failover group connection string on the Failover groups page in the Azure portal." lightbox="media/failover-group-configure-sql-db/find-failover-group-connection-string.png":::

## <a name="upgrading-or-downgrading-primary-database"></a> Scaling databases in a failover group

You can scale the primary database up or down to a different compute size (within the same service tier) without disconnecting any geo-secondaries. When scaling up, we recommend that you scale up the geo-secondary first, and then scale up the primary. When scaling down, reverse the order: scale down the primary first, and then scale down the secondary. When you scale a database to a different service tier, this recommendation is enforced.

This sequence is recommended specifically to avoid the problem where the geo-secondary at a lower SKU gets overloaded and must be reseeded during an upgrade or downgrade process. You could also avoid the problem by making the primary read-only, at the expense of affecting all read-write workloads against the primary.

> [!NOTE]  
> If you created a geo-secondary as part of the failover group configuration, it's not recommended to scale down the geo-secondary. This is to ensure your data tier has sufficient capacity to process your regular workload after a geo-failover.
You might not be able to scale a geo-secondary after an unplanned failover when the former geo-primary is unavailable due to outage. This is a known limitation.

## <a name="preventing-the-loss-of-critical-data"></a> Prevent loss of critical data

<!--
There is some overlap in the following content, be sure to update all that's necessary:  
/azure-sql/database/failover-group-configure-sql-db.md
/azure-sql/managed-instance/failover-configure-group-sql-mi.md
-->

Due to the high latency of wide area networks, geo-replication uses an asynchronous replication mechanism. Asynchronous replication makes the possibility of data loss unavoidable if the primary fails. To protect critical transactions from data loss, an application developer can call the [sp_wait_for_database_copy_sync](/sql/relational-databases/system-stored-procedures/sp-wait-for-database-copy-sync-transact-sql) stored procedure immediately after committing the transaction. Calling `sp_wait_for_database_copy_sync` blocks the calling thread until the last committed transaction has been transmitted and hardened in the transaction log of the secondary database. However, it doesn't wait for the transmitted transactions to be replayed (redone) on the secondary. `sp_wait_for_database_copy_sync` is scoped to a specific geo-replication link. Any user with the connection rights to the primary database can call this procedure.

> [!NOTE]  
> `sp_wait_for_database_copy_sync` prevents data loss after geo-failover for specific transactions, but does not guarantee full synchronization for read access. The delay caused by a `sp_wait_for_database_copy_sync` procedure call can be significant and depends on the size of the not yet transmitted transaction log on the primary at the time of the call.

## <a name="changing-secondary-region-of-the-failover-group"></a> Change the secondary region

To illustrate the change sequence, we'll assume that server A is the primary server, server B is the existing secondary server, and server C is the new secondary in the third region. To make the transition, follow these steps:

1. Create additional secondaries of each database on server A to server C using [active geo-replication](active-geo-replication-overview.md). Each database on server A will have two secondaries, one on server B and one on server C. This guarantees that the primary databases remain protected during the transition.
1. Delete the failover group. At this point sign in attempts using failover group endpoints start to fail.
1. Re-create the failover group with the same name between servers A and C.
1. Add all primary databases on server A to the new failover group. At this point sign in attempts stop failing.
1. Delete server B. All databases on B will be deleted automatically.

## <a name="changing-primary-region-of-the-failover-group"></a> Change the primary region

To illustrate the change sequence, we'll assume server A is the primary server, server B is the existing secondary server, and server C is the new primary in the third region. To make the transition, follow these steps:

1. Perform a planned geo-failover to switch the primary server to B. Server A becomes the new secondary server. The failover might result in several minutes of downtime. The actual time depends on the size of failover group.
1. Create additional secondaries of each database on server B to server C using [active geo-replication](active-geo-replication-overview.md). Each database on server B will have two secondaries, one on server A and one on server C. This guarantees that the primary databases remain protected during the transition.
1. Delete the failover group. At this point sign in attempts using failover group endpoints start to fail.
1. Re-create the failover group with the same name between servers B and C.
1. Add all primary databases on B to the new failover group. At this point login attempts stop failing.
1. Perform a planned geo-failover of the failover group to switch B and C. Now server C becomes the primary and B the secondary. All secondary databases on server A will be automatically linked to the primaries on C. As in step 1, the failover might result in several minutes of downtime.
1. Delete server A. All databases on A will be deleted automatically.

> [!IMPORTANT]  
> When the failover group is deleted, the DNS records for the listener endpoints are also deleted. At that point, there is a non-zero probability of somebody else creating a failover group or a server DNS alias with the same name. Because failover group names and DNS aliases must be globally unique, this will prevent you from using the same name again. To minimize this risk, don't use generic failover group names.

## Failover groups and network security

For some applications, the security rules require that the network access to the data tier is restricted to a specific component or components such as a VM, web service, etc. This requirement presents some challenges for business continuity design and the use of failover groups. Consider the following options when implementing such restricted access.

### <a name="using-failover-groups-and-virtual-network-rules"></a> Use failover groups and virtual network service endpoints

If you're using [Virtual Network service endpoints and rules](vnet-service-endpoint-rule-overview.md) to restrict access to your database, be aware that each virtual network service endpoint applies to only one Azure region. The endpoint doesn't enable other regions to accept communication from the subnet. Therefore, only the client applications deployed in the same region can connect to the primary database. Since a geo-failover results in the SQL Database client sessions being rerouted to a server in a different (secondary) region, these sessions might fail if originated from a client outside of that region. For that reason, the Microsoft managed failover policy can't be enabled if the participating servers are included in the Virtual Network rules. To support manual failover policy, follow these steps:

1. Provision redundant copies of the frontend components of your application (web service, virtual machines etc.) in the secondary region.
1. Configure [virtual network rules](vnet-service-endpoint-rule-overview.md) individually for the primary and secondary server.
1. Enable [frontend failover using a Traffic manager configuration](designing-cloud-solutions-for-disaster-recovery.md#scenario-1-using-two-azure-regions-for-business-continuity-with-minimal-downtime).
1. Initiate a manual geo-failover when the outage is detected. This option is optimized for applications that require consistent latency between the frontend and the data tier and supports recovery when either frontend, data tier, or both are affected by the outage.

> [!NOTE]  
> If you're using the **read-only listener** to load-balance a read-only workload, make sure this workload is executed in a VM or other resource in the secondary region so it can connect to the secondary database.

### Use failover groups and firewall rules

If your business continuity plan requires failover using failover groups, you can restrict access to your SQL Database by using public IP firewall rules. This configuration ensures that a geo-failover won't block connections from frontend components and assumes that the application can tolerate the longer latency between the frontend and the data tier.

To support failover group failover, follow these steps:

1. [Create a public IP](/azure/virtual-network/ip-services/virtual-network-public-ip-address#create-a-public-ip-address).
1. [Create a public load balancer](/azure/load-balancer/quickstart-load-balancer-standard-public-portal) and assign the public IP to it.
1. [Create a virtual network and the virtual machines](/azure/load-balancer/quickstart-load-balancer-standard-public-portal) for your front-end components.
1. [Create network security group](/azure/virtual-network/network-security-groups-overview) and configure inbound connections.
1. Ensure that the outbound connections are open to Azure SQL Database in a region by using an `Sql.<Region>` [service tag](/azure/virtual-network/network-security-groups-overview#service-tags).
1. Create a [SQL Database firewall rule](firewall-configure.md) to allow inbound traffic from the public IP address you create in step 1.

For more information on how to configure outbound access and what IP to use in the firewall rules, see [Load balancer outbound connections](/azure/load-balancer/load-balancer-outbound-connections).

> [!IMPORTANT]  
> To guarantee business continuity during regional outages you must ensure geographic redundancy for both frontend components and databases.

## Permissions

Permissions for a failover group are managed via [Azure role-based access control (Azure RBAC)](/azure/role-based-access-control/overview).

Azure RBAC write access is necessary to create and manage failover groups. The [SQL Server Contributor role](/azure/role-based-access-control/built-in-roles#sql-server-contributor) has all the necessary permissions to manage failover groups.

The following table lists specific permission scopes for Azure SQL Database:

| **Action** | **Permission** | **Scope** |
| :--- | :--- | :--- |
| **Create failover group** | Azure RBAC write access | Primary server<br />Secondary server<br />All databases in failover group |
| **Update failover group** | Azure RBAC write access | Failover group<br />All databases on the current primary server |
| **Fail over failover group** | Azure RBAC write access | Failover group on new server |

## Limitations

Be aware of the following limitations:

- Failover groups can't be created between two servers in the same Azure region.
- Failover groups support geo-replication of all databases in the group to only one secondary logical server in a different region.
- Failover groups can't be renamed. You'll need to delete the group and re-create it with a different name.
- Database rename isn't supported for databases in a failover group. You'll need to temporarily delete the failover group to be able to rename a database, or remove the database, from the failover group.
- Removing a failover group for a single or pooled database doesn't stop replication, and it doesn't delete the replicated database. You'll need to manually stop geo-replication and delete the database from the secondary server if you wanted to add a single or pooled database back to a failover group after it's been removed. Failing to do either might result in an error similar to `The operation cannot be performed due to multiple errors` when attempting to add the database to the failover group.
- Failover group name is subject to [naming restrictions](/azure/azure-resource-manager/management/resource-name-rules).
- When creating a new failover group, or when adding databases to an existing failover group, you can _only_ designate the databases as [standby replicas](standby-replica-how-to-configure.md) _when using the Azure portal_ - Azure PowerShell and the Azure CLI are not currently available.

## <a name="programmatically-managing-failover-groups"></a> Programmatically manage failover groups

Failover groups can also be managed programmatically by using Azure PowerShell, Azure CLI, and REST API. The following tables describe the set of commands available. Failover groups include a set of Azure Resource Manager APIs for management, including the [Azure SQL Database REST API](/rest/api/sql/) and [Azure PowerShell cmdlets](/powershell/azure/). These APIs require the use of resource groups and support Azure role-based access control (Azure RBAC). For more information on how to implement access roles, see [Azure role-based access control (Azure RBAC)](/azure/role-based-access-control/overview).

# [PowerShell](#tab/azure-powershell-manage)

| Cmdlet | Description |
| --- | --- |
| [New-AzSqlDatabaseFailoverGroup](/powershell/module/az.sql/new-azsqldatabasefailovergroup) | This command creates a failover group and registers it on both primary and secondary servers |
| [Add-AzSqlDatabaseToFailoverGroup](/powershell/module/az.sql/add-azsqldatabasetofailovergroup) | Adds one or more databases to a failover group |
| [Remove-AzSqlDatabaseFromFailoverGroup](/powershell/module/az.sql/remove-azsqldatabasefromfailovergroup) | Removes one or more databases from a failover group |
| [Remove-AzSqlDatabaseFailoverGroup](/powershell/module/az.sql/remove-azsqldatabasefailovergroup) | Removes a failover group from the server |
| [Get-AzSqlDatabaseFailoverGroup](/powershell/module/az.sql/get-azsqldatabasefailovergroup) | Retrieves a failover group's configuration |
| [Set-AzSqlDatabaseFailoverGroup](/powershell/module/az.sql/set-azsqldatabasefailovergroup) | Modifies configuration of a failover group |
| [Switch-AzSqlDatabaseFailoverGroup](/powershell/module/az.sql/switch-azsqldatabasefailovergroup) | Triggers failover of a failover group to the secondary server |
> [!NOTE]  
> It's possible to deploy your failover group across subscriptions by using the `-PartnerSubscriptionId` parameter in Azure Powershell starting with [Az.SQL 3.11.0](https://www.powershellgallery.com/packages/Az.Sql/3.11.0). To learn more, review the following [Example](/powershell/module/az.sql/new-azsqldatabasefailovergroup#example-3).

# [Azure CLI](#tab/azure-cli-manage)

| Command | Description |
| --- | --- |
| [az sql failover-group create](/cli/azure/sql/failover-group#az-sql-failover-group-create) | Creates a failover group and registers it on both primary and secondary servers |
| [az sql failover-group update](/cli/azure/sql/failover-group#az-sql-failover-group-update) | Adds or removes databases, or modifies the configuration settings for the failover group |
| [az sql failover-group delete](/cli/azure/sql/failover-group#az-sql-failover-group-delete) | Removes a failover group from the server |
| [az sql failover-group show](/cli/azure/sql/failover-group#az-sql-failover-group-show) | Retrieves a failover group configuration |
| [az sql failover-group set-primary](/cli/azure/sql/failover-group#az-sql-failover-group-set-primary) | Triggers failover of a failover group to the secondary server |

# [REST API](#tab/rest-api-manage)

| API | Description |
| --- | --- |
| [Create or Update Failover Group](/rest/api/sql/failover-groups/create-or-update) | Creates or updates a failover group, and adds or removes databases |
| [Delete Failover Group](/rest/api/sql/failover-groups/delete) | Removes a failover group from the server |
| [Failover (Planned)](/rest/api/sql/failover-groups/failover) | Triggers failover from the current primary server to the secondary server with full data synchronization. |
| [Force Failover Allow Data Loss](/rest/api/sql/failover-groups/force-failover-allow-data-loss) | Triggers failover from the current primary server to the secondary server without synchronizing data. This operation might result in data loss. |
| [Get Failover Group](/rest/api/sql/failover-groups/get) | Retrieves a failover group's configuration. |
| [List Failover Groups By Server](/rest/api/sql/failover-groups/list-by-server) | Lists the failover groups on a server. |
| [Update Failover Group](/rest/api/sql/failover-groups/update) | Updates a failover group's configuration. |

---

## Related content

- [Failover groups overview](failover-group-sql-db.md)
- [Active geo-replication](active-geo-replication-overview.md)
