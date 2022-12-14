---
title: Change automated backup settings
titleSuffix: Azure SQL Managed Instance
description: Change point-in-time restore and backup redundancy options for automatic backups in Azure SQL Managed Instance by using the Azure portal, the Azure CLI, Azure PowerShell, and the REST API.
author: MilanMSFT
ms.author: mlazic
ms.reviewer: wiassaf, mathoma, danil
ms.date: 07/20/2022
ms.service: sql-managed-instance
ms.subservice: backup-restore
ms.topic: conceptual
ms.custom:
  - "references_regions"
  - "devx-track-azurepowershell"
  - "devx-track-azurecli"
  - "azure-sql-split"
monikerRange: "= azuresql || = azuresql-mi"
---
# Change automated backup settings for Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

<!---
Some of the content in this article is duplicated in /azure-sql/database/automated-backups-change-settings.md. Any relevant changes made to this article should be made in the other article as well. 
--->

> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/automated-backups-change-settings.md)
> * [Azure SQL Managed Instance](automated-backups-change-settings.md)

This article provides examples to modify [automated backup](automated-backups-overview.md) settings for Azure SQL Managed Instance, such as the short-term retention policy and the backup storage redundancy option that's used for backups. For Azure SQL Database, see [Change automated backup settings for Azure SQL Database](../database/automated-backups-change-settings.md).

[!INCLUDE [GDPR-related guidance](~/../azure/includes/gdpr-intro-sentence.md)]

## Change short-term retention policy

You can change the default point-in-time recovery (PITR) backup retention period and the differential backup frequency by using the Azure portal, PowerShell, or the REST API. The following examples illustrate how to change the PITR retention to 28 days and the differential backups to a 24-hour interval.

> [!WARNING]
> If you reduce the current retention period, you lose the ability to restore to points in time older than the new retention period. Backups that are no longer needed to provide PITR within the new retention period are deleted. 
>
> If you increase the current retention period, you don't immediately gain the ability to restore to older points in time within the new retention period. You gain that ability over time, as the system starts to retain backups for longer periods.

> [!NOTE]
> These APIs will affect only the PITR retention period. If you configured long-term retention (LTR) for your database, it won't be affected. For information about how to change long-term retention periods, see [Long-term retention](../database/long-term-retention-overview.md).

### [Azure portal](#tab/azure-portal)

To change the PITR backup retention period or the differential backup frequency for active databases by using the Azure portal:

1. Go to the managed instance with the databases whose retention period you want to change. 
1. Select **Backups** on the left pane, and then select the **Retention policies** tab. 
1. Select the databases for which you want to change the PITR backup retention. 
1. Select **Configure policies** from the action bar.

:::image type="content" source="../database/media/automated-backups-overview/configure-backup-retention-sqlmi.png" alt-text="Screenshot of the Azure portal backup settings to change PITR retention for the managed instance.":::

### [Azure CLI](#tab/azure-cli)

Prepare your environment for the Azure CLI:

[!INCLUDE[azure-cli-prepare-your-environment-no-header](../includes/azure-cli-prepare-your-environment-no-header.md)]

Use the following example to change the PITR backup retention of a *single active* database in a managed instance:

```azurecli
# Set a new PITR backup retention period on an active individual database
# Valid backup retention must be 1 to 35 days
az sql midb short-term-retention-policy set \
    --resource-group myresourcegroup \
    --managed-instance myinstance \
    --name mymanageddb \
    --retention-days 1 \
```

Use the following example to change the PITR backup retention for *all active* databases in a managed instance:

```azurecli
# Set a new PITR backup retention period for all active databases
# Valid backup retention must be 1 to 35 days
az sql midb short-term-retention-policy set \
    --resource-group myresourcegroup \
    --managed-instance myinstance \
    --retention-days 1 \
```


### [PowerShell](#tab/powershell)

To change the PITR backup retention for a *single active* database in a managed instance, use the following PowerShell example:

```powershell
# Set a new PITR backup retention period on an active individual database
# Valid backup retention must be 1 to 35 days
Set-AzSqlInstanceDatabaseBackupShortTermRetentionPolicy -ResourceGroupName resourceGroup -InstanceName testserver -DatabaseName testDatabase -RetentionDays 1
```

To change the PITR backup retention for *all active* databases in a managed instance, use the following PowerShell example:

```powershell
# Set a new PITR backup retention period for all active databases
# Valid backup retention must be 1 to 35 days
Get-AzSqlInstanceDatabase -ResourceGroupName resourceGroup -InstanceName testserver | Set-AzSqlInstanceDatabaseBackupShortTermRetentionPolicy -RetentionDays 1
```

To change the PITR backup retention for a *single deleted* database in a managed instance, use the following PowerShell example:
 
```powershell
# Set a new PITR backup retention on an individual deleted database
# Valid backup retention must be 0 (no retention) to 35 days. Valid retention rate can only be lower than the retention period when database was active, or the remaining backup days of a deleted database.
Get-AzSqlDeletedInstanceDatabaseBackup -ResourceGroupName resourceGroup -InstanceName testserver -DatabaseName testDatabase | Set-AzSqlInstanceDatabaseBackupShortTermRetentionPolicy -RetentionDays 0
```

To change the PITR backup retention for *all deleted* databases in a managed instance, use the following PowerShell example:

```powershell
# Set a new PITR backup retention for all deleted databases
# Valid backup retention must be 0 (no retention) to 35 days. Valid retention rate can only be lower than the retention period when database was active, or the remaining backup days of a deleted database
Get-AzSqlDeletedInstanceDatabaseBackup -ResourceGroupName resourceGroup -InstanceName testserver | Set-AzSqlInstanceDatabaseBackupShortTermRetentionPolicy -RetentionDays 0
```

Zero days of retention would denote that a backup is immediately deleted and no longer kept for a deleted database. After you reduce PITR backup retention for a deleted database, you can no longer increase it.

### [Rest API](#tab/rest-api)

#### Sample request

```http
PUT https://management.azure.com/subscriptions/00000000-1111-2222-3333-444444444444/resourceGroups/resourceGroup/providers/Microsoft.Sql/servers/testserver/databases/testDatabase/backupShortTermRetentionPolicies/default?api-version=2017-10-01-preview
```

#### Request body

```json
{
  "properties":{
    "retentionDays":28
  }
}
```

#### Sample response

Status code: 200

```json
{
  "id": "/subscriptions/00000000-1111-2222-3333-444444444444/providers/Microsoft.Sql/resourceGroups/resourceGroup/servers/testserver/databases/testDatabase/backupShortTermRetentionPolicies/default",
  "name": "default",
  "type": "Microsoft.Sql/resourceGroups/servers/databases/backupShortTermRetentionPolicies",
  "properties": {
    "retentionDays": 28
  }
}
```

For more information, see [Backup retention REST API](/rest/api/sql/backupshorttermretentionpolicies).

---

## Configure backup storage redundancy 

Configure backup storage redundancy for SQL Managed Instance by using the Azure portal, the Azure CLI, and Azure PowerShell. 

### [Azure portal](#tab/azure-portal)

In the Azure portal, during instance creation, the default option for the backup storage redundancy is geo-redundancy. To change it:

1. Go to the **Basics** tab and select **Configure Managed Instance**.

   :::image type="content" source="../database/media/automated-backups-overview/open-configuration-blade-managed-instance.png" alt-text="Screenshot of the pane for configuring backup storage redundancy in the Azure portal for a managed instance.":::

1. On the **Compute + storage** pane, select the option for the type of backup storage redundancy that you want.

   :::image type="content" source="../database/media/automated-backups-overview/select-backup-storage-redundancy-managed-instance.png" alt-text="Screenshot of selecting backup storage redundancy in the Azure portal for a managed instance.":::

1. Select **Apply**. For now, this change will be applied only for PITR backups. Long-term retention backups will retain the old storage redundancy type. 

The time it takes to perform the backup redundancy change depends on the size of the all the databases within a single managed instance. Changing the backup redundancy will take more time for instances that have large databases. It's possible to combine the backup storage redundancy change with the operation to update the service-level objective (SLO). 

Use the **Notification** pane of the Azure portal to view the status of the change operation. 

:::image type="content" source="../database/media/automated-backups-overview/change-backup-storage-redundancy-managed-instance-notification.png" alt-text="Screenshot of the status of ongoing operations in the Azure portal.":::

### [Azure CLI](#tab/azure-cli)

To change the backup storage redundancy after you create a managed instance by using the Azure CLI, specify the `-BackupStorageRedundancy` parameter with the `az sql mi update` cmdlet. View the [update mi backup storage redundancy](/cli/azure/sql/mi#az-sql-mi-update-examples) example. 

Possible values for `-BackupStorageRedundancy` are `Geo` for geo-redundant, `Zone` for zone-redundant, `Local` for locally redundant, and `GeoZone` for geo-zone redundant backup storage. 

### [PowerShell](#tab/powershell)

To configure backup storage redundancy when you create a managed instance, specify the `-BackupStorageRedundancy` parameter with the [New-AzSqlInstance](/powershell/module/az.sql/new-azsqlinstance) cmdlet. To change the backup storage redundancy for an existing managed instance, specify the `-BackupStorageRedundancy` parameter with the `Set-AzSqlInstance` cmdlet. To learn more, review the [Update an existing instance to be zone-redundant](/powershell/module/az.sql/set-azsqlinstance#example-7-update-an-existing-instance-to-be-zone-redundant) example. 

Possible values for `-BackupStorageRedundancy` are `Geo` for geo-redundant, `Zone` for zone-redundant, `Local` for locally redundant, and `GeoZone` for geo-zone redundant backup storage. 

### [Rest API](#tab/rest-api)

It's not currently possible to change the backup storage redundancy option by using the REST API. 

---

## Next steps

- Database backups are an essential part of any business continuity and disaster recovery strategy because they help protect your data from accidental corruption or deletion. To learn about the other business continuity solutions for SQL Managed Instance, see [Business continuity overview](../database/business-continuity-high-availability-disaster-recover-hadr-overview.md).
- For information about how to configure, manage, and restore from long-term retention of automated backups in Azure Blob Storage by using the Azure portal, see [Manage long-term backup retention by using the Azure portal](long-term-backup-retention-configure.md).
- For information about how to configure, manage, and restore from long-term retention of automated backups in Azure Blob Storage by using PowerShell, see [Manage long-term backup retention by using PowerShell](long-term-backup-retention-configure.md). 
- Get more information about how to [restore a database to a point in time by using the Azure portal](recovery-using-backups.md).
- To learn all about backup storage consumption on Azure SQL Managed Instance, see [Backup storage consumption on Managed Instance explained](https://aka.ms/mi-backup-explained).
- To learn how to fine-tune backup storage retention and costs for Azure SQL Managed Instance, see [Fine tuning backup storage costs on SQL Managed Instance](https://aka.ms/mi-backup-tuning).
