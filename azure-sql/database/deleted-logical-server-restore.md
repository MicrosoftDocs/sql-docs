---
title: "Restore a Deleted Logical Server (Preview)"
titleSuffix: Azure SQL Database
description: Learn about restoring a deleted logical server in Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dinethi, jaypatel, mathoma
ms.date: 05/27/2026
ms.service: azure-sql-database
ms.subservice: backup-restore
ms.topic: how-to
monikerRange: "=azuresql || =azuresql-db"
---
# Restore a deleted logical server in Azure SQL Database (preview)

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

This article explains how to configure soft delete retention for your [logical server in Azure](logical-servers.md), and how to restore a logical server that's been deleted within the retention period. 

After you enable soft delete retention, you can list and restore a deleted logical server to its original state during the retention window. You can configure a soft delete retention period for your logical server and restore a deleted server by using the Azure portal, PowerShell, or the Azure CLI.

> [!IMPORTANT]  
> The ability to configure a soft delete retention period and restore a deleted logical server is currently in [preview](doc-changes-updates-release-notes-whats-new.md#preview). Any logical server older than two years automatically has a soft delete retention period of seven days. Logical servers less than two years old have soft delete retention disabled by default.

## Overview

Soft delete retention helps protect logical servers in Azure from accidental deletion by retaining deleted server metadata for a configurable period. When you enable soft delete retention and a logical server is deleted, Azure doesn't immediately remove the server permanently. Instead, Azure keeps the server in a soft-deleted state for the configured retention period. While the server is in this state, you can discover and restore it. The retention period is configured in days at the logical server level and determines how long a deleted server remains recoverable.

Soft delete retention is useful in the following scenarios:

- **Accidental deletion** – Recover a logical server that was unintentionally deleted.
- **Operational safety** – Reduce risk during automation, scripted cleanup, or bulk operations.
- **Development and testing** – Safely delete and restore servers in test or non-production environments.
- **Governance and protection** – Add a recovery buffer to mitigate irreversible loss due to human error.

When the logical server is deleted, the user databases are deleted. When you restore the server, you can also [restore](recovery-using-backups.md) databases within retention.

## Prerequisites

To configure soft delete retention, you need the following prerequisites:

- An Azure subscription. If you don't have an Azure subscription, [create a free account](https://azure.microsoft.com/pricing/purchase-options/azure-account?cid=msft_learn)
- A [logical server in Azure](logical-servers.md).
- If using PowerShell or the Azure CLI, you need [Azure PowerShell Az module 15.4.0 or later](/powershell/azure/install-az-ps) or [Azure CLI version 2.84.0 or later](/cli/azure/install-azure-cli).

## Permissions

To configure soft delete retention, or restore a deleted server, the user needs to be a member of the [SQL Server contributor](/azure/role-based-access-control/built-in-roles/databases#sql-server-contributor) role.

## Configure soft delete retention

You can configure soft delete retention for a logical server when you create the server by using PowerShell or the Azure CLI. You can update the retention period for an existing server by using the Azure portal, PowerShell, or the Azure CLI. 

Set the retention period from 0 to 7 days. Setting the retention to 0 days disables soft delete retention.

### [Azure portal](#tab/azure-portal)

Currently, you can't configure the soft delete retention period when you create a new logical server in the Azure portal. You can only set the retention period for an existing server.

To view or configure soft delete retention for an existing server in the Azure portal, follow these steps:

1. Go to your [logical server](https://portal.azure.com/#servicemenu/SqlAzureExtension/AzureSqlHub/DatabaseServer) in the Azure portal.
1. Under **Data Management**, select **Delete protection (preview)** to open the **Delete protection (preview)** pane.
1. On the **Delete protection (preview)** pane:
   - The **Keep deleted servers (in days)** field shows the current retention period for the server. If the value is `Not enabled` or `0`, soft delete is OFF and the server can't be recovered if deleted. A value between `1 and 7` indicates the number of days a deleted server is retained and available for restore.
   - Modify the value in the **Keep deleted servers (in days)** field to set the desired retention period for the server. You can enter a value between 1 and 7 to specify the number of days to retain a deleted server.

   :::image type="content" source="media/deleted-logical-server-restore/soft-retention-portal.png" alt-text="Screenshot of the Delete protection pane for a SQL server in the Azure portal." lightbox="media/deleted-logical-server-restore/soft-retention-portal.png":::

1. Select **Apply** to save your changes.

### [PowerShell](#tab/powershell)

Use the `-SoftDeleteRetentionDays` parameter for the [New-AzSqlServer](/powershell/module/az.sql/new-azsqlserver) and [Set-AzSqlServer](/powershell/module/az.sql/set-azsqlserver) cmdlets to configure soft delete retention for a new or existing server.

The `-SoftDeleteRetentionDays` parameter is an integer between **0 and 7** where:
- A value of `0` turns soft delete OFF. When the logical server is deleted, it can't be recovered.
- A value between `1` and `7` sets the number of days the server enters into a soft-deleted state when deleted. The server can be restored during the retention period.

The following example creates a new logical server with a 7-day soft delete retention period:

```powershell
New-AzSqlServer `
   -ServerName <server-name> `
   -ResourceGroupName <resource-group-name> `
   -Location <azure-region> `
   -AdministratorLogin <admin-username> `
   -AdministratorLoginPassword <secure-password> `
   -SoftDeleteRetentionDays 7
```

The following example updates an existing logical server to have a 7-day soft delete retention period:

```powershell
Set-AzSqlServer `
   -ServerName <server-name> `
   -ResourceGroupName <resource-group-name> `
   -SoftDeleteRetentionDays 7
```

### [Azure CLI](#tab/azure-cli)

Use the `soft-delete-retention-days` parameter for the [az sql server create](/cli/azure/sql/server#az-sql-server-create) and [az sql server update](/cli/azure/sql/server#az-sql-server-update) commands to configure soft delete retention for a new or existing server.

The `soft-delete-retention-days` parameter is an integer between **0 and 7** where:
- A value of `0` turns soft delete OFF. When the logical server is deleted, it can't be recovered.
- A value between `1` and `7` sets the number of days the server enters into a soft-deleted state when deleted. The server can be restored during the retention period.

The following example uses [az sql server create](/cli/azure/sql/server#az-sql-server-create) to create a new logical server with a 7-day soft delete retention period:

```azurecli
az sql server create   --name <server-name>   --resource-group <resource-group-name>   --location <azure-region>   --administrator-login <admin-username>   --administrator-login-password <admin-password>   --soft-delete-retention-days 7
```

The following example uses [az sql server update](/cli/azure/sql/server#az-sql-server-update) to update an existing logical server to have a 7-day soft delete retention period:

```azurecli
az sql server update   --name <server-name>   --resource-group <resource-group-name>   --soft-delete-retention-days 7
```

---

## Delete a logical server

If you delete a logical server with soft delete retention enabled, the server enters a soft-deleted state for the configured retention period. You can delete a server by using the Azure portal, PowerShell, or the Azure CLI.

If soft delete  retention isn't configured for the server, deleting the server results in permanent deletion with no retention or recovery option.

### [Azure portal](#tab/azure-portal)

In the Azure portal, you can delete a [logical server](https://portal.azure.com/#servicemenu/SqlAzureExtension/AzureSqlHub/DatabaseServer) from the following locations:

- Use the **Delete** button on the command bar of your logical server's **Overview** pane.
- Select the checkbox next to the server you want to delete on the **SQL logical servers** pane of the Azure SQL Hub, and then use the **Delete** button on the command bar.

### [PowerShell](#tab/powershell)

Use [Remove-AzSqlServer](/powershell/module/az.sql/remove-azsqlserver) to delete a logical server:

```powershell
Remove-AzSqlServer `
   -ServerName <server-name> `
   -ResourceGroupName <resource-group-name>
```

### [Azure CLI](#tab/azure-cli)

Use [az sql server delete](/cli/azure/sql/server#az-sql-server-delete) to delete a logical server:

```azurecli
az sql server delete \
   --name <server-name> \
   --resource-group <resource-group-name>
```

---

## List soft-deleted servers

You can list soft-deleted logical servers to see which servers are available to restore within the retention period by using the Azure portal, PowerShell, or the Azure CLI.

### [Azure portal](#tab/azure-portal)

To view a list of soft-deleted servers follow the steps in [Restore a deleted server](#restore-a-soft-deleted-logical-server) to open the **Restore deleted server** pane. The dropdown list of deleted servers shows all soft-deleted servers available to restore for a subscription within their specified retention period.

### [PowerShell](#tab/powershell)

Use [Get-AzSqlDeletedServer](/powershell/module/az.sql/get-azsqldeletedserver) to list soft-deleted servers in a region:

```powershell
Get-AzSqlDeletedServer `
   -Location <azure-region> `
   -ServerName
```

### [Azure CLI](#tab/azure-cli)

Use [az sql server deleted-server list](/cli/azure/sql/server/deleted-server#az-sql-server-deleted-server-list) to list soft-deleted servers in a region:

```azurecli
az sql server deleted-server list \
   --location <azure-region>
```

To show details of a specific soft-deleted server, use [az sql server deleted-server show](/cli/azure/sql/server/deleted-server#az-sql-server-deleted-server-show):

```azurecli
az sql server deleted-server show \
   --name <server-name> \
   --location <azure-region>
```

---

## Restore a soft-deleted logical server

You can restore a soft-deleted logical server during the retention period by using the Azure portal, PowerShell, or the Azure CLI.

When the logical server is deleted, the user databases are deleted. When you restore the server, you can also [restore](recovery-using-backups.md) databases within retention.

### [Azure portal](#tab/azure-portal)

To restore a soft-deleted logical server in the Azure portal:

1. Go to the [Azure SQL hub](https://portal.azure.com/#servicemenu/SqlAzureExtension/AzureSqlHub) in the Azure portal.
1. Under **Azure SQL Database**, select **SQL logical servers** to open the **SQL logical servers** pane.
1. On the **SQL logical servers** pane, select **Restore** from the command bar to open the **Restore deleted server** pane:

   1. Select the **Subscription** from the dropdown list.
   1. Select the **Location** of the deleted server from the dropdown list.
   1. Select a **Deleted server** from the dropdown list. The dropdown list shows all soft-deleted servers available to restore within their specified retention period.

   :::image type="content" source="media/deleted-logical-server-restore/restore-deleted-server.png" alt-text="Screenshot of the restore deleted server pane in the Azure portal." lightbox="media/deleted-logical-server-restore/restore-deleted-server.png":::

1. Select **Restore** to restore the deleted server. The restored server has the same name and configuration as the deleted server. It's restored to its original state at the time of deletion. After the restore operation completes, the server is available in the list of active servers in the Azure portal.

### [PowerShell](#tab/powershell)

Use [Restore-AzSqlServer](/powershell/module/az.sql/restore-azsqlserver) to restore a soft-deleted logical server:

```powershell
Restore-AzSqlServer `
   -ServerName <server-name> `
   -ResourceGroupName <resource-group-name> `
   -Location <azure-region>
```

### [Azure CLI](#tab/azure-cli)

Use [az sql server restore](/cli/azure/sql/server#az-sql-server-restore) to restore a soft-deleted logical server:

```azurecli
az sql server restore \
   --name <server-name> \
   --resource-group <resource-group-name> \
   --location <azure-region>
```

---

## Permanently delete a logical server

If you want to permanently delete a logical server, make sure the soft delete retention period is set to 0 to disable retention before deleting the server. When the server is deleted, it's deleted permanently.

If your logical server has already been deleted with a specified retention but you want to permanently delete it before the retention period expires, follow these steps:
1. Restore the logical server.
1. Disable retention by setting the soft delete retention period to `0`. 
1. Delete the logical server again.

## Actions after restoring a deleted server

After the logical server has been restored, the databases can be restored manually using the **Backups** pane under **Data management** for your [logical server](https://portal.azure.com/#servicemenu/SqlAzureExtension/AzureSqlHub/DatabaseServer) in the Azure portal.

The following server-level configurations are dropped during soft-deletion and must be reconfigured after restoring the server:

- Failover groups
- Geo-replication links
- Server DNS aliases
- Private endpoint connections
- Firewall rules
- Elastic job agents
- Server identity in Microsoft Entra ID
- CMK/TDE encryption keys
- Data Sync groups (server communication links), if Data Sync was being used
- Synapse workspace link,  if there was a connected Synapse workspace

## Limitations

The following limitations apply when using soft delete retention for your logical server:
- If you use the built-in [Azure policy to enforce Microsoft Entra-only authentication](authentication-azure-ad-only-authentication-policy.md), you can't restore the deleted server. To restore the server, remove the policy and then restore the server.
- When the logical server is deleted, the managed identities are deleted. 
- When the logical server is restored, any encryption with Customer Managed Key (CMK) needs to be reconfigured.

## Related content

- [Restore a database from a backup in Azure SQL Database](recovery-using-backups.md)
- [Automated backups in Azure SQL Database](automated-backups-overview.md)
