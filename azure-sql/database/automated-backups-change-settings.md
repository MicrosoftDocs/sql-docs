---
title: Change automated backup settings
titleSuffix: Azure SQL Database
description: Change point-in-time restore and backup redundancy options for automatic backups in Azure SQL Database by using the Azure portal, the Azure CLI, Azure PowerShell, and the REST API.
author: SudhirRaparla
ms.author: nvraparl
ms.reviewer: wiassaf, mathoma, danil
ms.date: 07/20/2022
ms.service: sql-database
ms.subservice: backup-restore
ms.topic: conceptual
ms.custom:
  - "references_regions"
  - "devx-track-azurepowershell"
  - "devx-track-azurecli"
  - "azure-sql-split"
monikerRange: "= azuresql || = azuresql-db"
---
# Change automated backup settings for Azure SQL Database
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

<!---
Some of the content in this article is duplicated in /azure-sql/managed-instance/automated-backups-change-settings.md. Any relevant changes made to this article should be made in the other article as well. 
--->


> [!div class="op_single_selector"]
> * [Azure SQL Database](automated-backups-change-settings.md)
> * [Azure SQL Managed Instance](../managed-instance/automated-backups-change-settings.md)


This article provides examples to modify [automated backup](automated-backups-overview.md) settings for Azure SQL Database, such as the short-term retention policy and the backup storage redundancy option that's used for backups. For Azure SQL Managed Instance, see [Change automated backup settings for Azure SQL Managed Instance](../managed-instance/automated-backups-change-settings.md).

[!INCLUDE [GDPR-related guidance](~/../azure/includes/gdpr-intro-sentence.md)]

## Change short-term retention policy

You can change the default point-in-time recovery (PITR) backup retention period and the differential backup frequency by using the Azure portal, the Azure CLI, PowerShell, or the REST API. The following examples illustrate how to change the PITR retention to 28 days and the differential backups to a 24-hour interval.

> [!WARNING]
> If you reduce the current retention period, you lose the ability to restore to points in time older than the new retention period. Backups that are no longer needed to provide PITR within the new retention period are deleted. 
>
> If you increase the current retention period, you don't immediately gain the ability to restore to older points in time within the new retention period. You gain that ability over time, as the system starts to retain backups for longer periods.

> [!NOTE]
> - These APIs will affect only the PITR retention period. If you configured long-term retention (LTR) for your database, it won't be affected. For information about how to change long-term retention periods, see [Long-term retention](long-term-retention-overview.md).
> - Hyperscale databases don't support configuring the differential backup frequency because differential backups don't apply to Hyperscale databases. 

### [Azure portal](#tab/azure-portal)

To change the PITR backup retention period or the differential backup frequency for active databases by using the Azure portal:

1. Go to the [logical server in Azure](logical-servers.md) with the databases whose retention period you want to change. 
1. Select **Backups** on the left pane, and then select the **Retention policies** tab. 
1. Select the databases for which you want to change the PITR backup retention. 
1. Select **Configure policies** from the action bar.

:::image type="content" source="./media/automated-backups-overview/configure-backup-retention-sqldb.png" alt-text="Screenshot of the Azure portal, where you can change the PITR retention settings at the server level. ":::

### [Azure CLI](#tab/azure-cli)

Prepare your environment for the Azure CLI:

[!INCLUDE[azure-cli-prepare-your-environment-no-header](../includes/azure-cli-prepare-your-environment-no-header.md)]

Change the PITR backup retention and differential backup frequency for active databases by using the following example:

```azurecli
# Set new PITR differential backup frequency on an active individual database
# Valid backup retention must be 1 to 35 days
# Valid differential backup frequency must be ether 12 or 24 hours
az sql db str-policy set \
    --resource-group myresourcegroup \
    --server myserver \
    --name mydb \
    --retention-days 28 \
    --diffbackup-hours 24
```

### [PowerShell](#tab/powershell)

To change the PITR backup retention and differential backup frequency for active databases, use the following PowerShell example:

```powershell
# Set a new PITR backup retention period on an active individual database
# Valid backup retention must be 1 to 35 days
Set-AzSqlDatabaseBackupShortTermRetentionPolicy -ResourceGroupName resourceGroup -ServerName testserver -DatabaseName testDatabase -RetentionDays 28
```

```powershell
# Set a new PITR differential backup frequency on an active individual database
# Valid differential backup frequency must be ether 12 or 24 hours 
Set-AzSqlDatabaseBackupShortTermRetentionPolicy -ResourceGroupName resourceGroup -ServerName testserver -DatabaseName testDatabase -RetentionDays 28 -DiffBackupIntervalInHours 24
```


### [REST API](#tab/rest-api)

#### Sample request

```http
PUT https://management.azure.com/subscriptions/00000000-1111-2222-3333-444444444444/resourceGroups/resourceGroup/providers/Microsoft.Sql/servers/testserver/databases/testDatabase/backupShortTermRetentionPolicies/default?api-version=2021-02-01-preview
```

#### Request body

```json
{ 
    "properties":{
        "retentionDays":28,
        "diffBackupIntervalInHours":24
  }
}
```

#### Sample response 

```json
{ 
  "id": "/subscriptions/00000000-1111-2222-3333-444444444444/providers/Microsoft.Sql/resourceGroups/resourceGroup/servers/testserver/databases/testDatabase/backupShortTermRetentionPolicies/default",
  "name": "default",
  "type": "Microsoft.Sql/resourceGroups/servers/databases/backupShortTermRetentionPolicies",
  "properties": {
    "retentionDays": 28,
    "diffBackupIntervalInHours":24
  }
}
```

For more information, see [Backup retention REST API](/rest/api/sql/backupshorttermretentionpolicies).

---

## Configure backup storage redundancy

You can configure backup storage redundancy for databases in Azure SQL Database when you create your database. You can also change the storage redundancy after the database is already created. 

Backup storage redundancy changes made to existing databases apply to future backups only. The default value is geo-redundant storage. For differences in pricing between locally redundant, zone-redundant, and geo-redundant backup storage, see the [SQL Database pricing page](https://azure.microsoft.com/pricing/details/sql-database/single/). 

Storage redundancy for Hyperscale databases is unique. To learn more, review [Hyperscale backup storage redundancy](hyperscale-automated-backups-overview.md#data-and-backup-storage-redundancy). 


### [Azure portal](#tab/azure-portal)

In the Azure portal, you can choose a backup storage redundancy option when you create your database. You can later update the backup storage redundancy from the **Compute & storage** page of your database settings. 

When you're creating your database, choose the backup storage redundancy option on the **Basics** tab. 

:::image type="content" source="./media/automated-backups-overview/sql-database-backup-storage-redundancy.png" alt-text="Screenshot of the Azure portal, where you can change backup storage redundancy from the Basics tab when you create your database.":::

For existing databases, go to your database in the [Azure portal](https://portal.azure.com). Select **Compute & storage** under **Settings**, and then choose your desired option for backup storage redundancy. 

:::image type="content" source="./media/automated-backups-overview/change-redundancy-for-existing-databases.png" alt-text="Screenshot of the Azure portal that shows where to change the backup storage redundancy for existing databases.":::


### [Azure CLI](#tab/azure-cli)

To configure backup storage redundancy when you're creating a new database, you can specify the `--backup-storage-redundancy` parameter with the `az sql db create` command. Possible values are `Geo`, `Zone`, and `Local`. 

By default, all databases in Azure SQL Database use geo-redundant storage for backups. Geo-restore is disabled if a database is created or updated with locally redundant or zone-redundant backup storage.

This example creates a database in the [General Purpose](service-tier-general-purpose.md) service tier with local backup redundancy:

```azurecli
az sql db create \
    --resource-group myresourcegroup \
    --server myserver \
    --name mydb \
    --tier GeneralPurpose \
    --backup-storage-redundancy Local
```

Except for Hyperscale and Basic databases, you can update the backup storage redundancy setting for an existing database by using the `--backup-storage-redundancy` parameter and the `az sql db update` command. It might take up to 48 hours for the changes to be applied on the database. Switching from geo-redundant backup storage to locally redundant or zone-redundant storage disables geo-restore.

This example code changes the backup storage redundancy to `Local`:

```azurecli
az sql db update \
    --resource-group myresourcegroup \
    --server myserver \
    --name mydb \
    --backup-storage-redundancy Local
```

#### Hyperscale

Carefully consider the configuration option for `--backup-storage-redundancy` when you're creating a Hyperscale database. The storage redundancy can be specified only during the database creation process for Hyperscale databases. You can't update it later. The selected storage redundancy option will be used for the lifetime of the database for both data storage redundancy and backup storage redundancy. Learn more in [Hyperscale backup storage redundancy](hyperscale-automated-backups-overview.md#data-and-backup-storage-redundancy). 

Existing [Hyperscale](service-tier-hyperscale.md) databases can migrate to different storage redundancy through [active geo-replication](active-geo-replication-overview.md), which causes minimal downtime. Alternatively, you can migrate to a different storage redundancy by using [database copy](database-copy.md) or point-in-time restore. This example creates a database in the Hyperscale service tier with zone redundancy:

```azurecli
az sql db create \
    --resource-group myresourcegroup \
    --server myserver \
    --name mydb \
    --tier Hyperscale \
    --backup-storage-redundancy Zone
```

For more information, see [az sql db create](/cli/azure/sql/db#az-sql-db-create) and [az sql db update](/cli/azure/sql/db#az-sql-db-update).

You can't update the backup storage redundancy of a Hyperscale database directly. However, you can change it by using [the database copy command](database-copy.md) with the `--backup-storage-redundancy` parameter. This example copies a Hyperscale database to a new database that uses Gen5 hardware and two vCores. The new database has the backup redundancy set to `Zone`.

```azurecli
az sql db copy \
    --resource-group myresourcegroup \ 
    --server myserver 
    --name myHSdb 
    --dest-resource-group mydestresourcegroup 
    --dest-server destdb 
    --dest-name myHSdb 
    --service-objective HS_Gen5_2 
    --read-replicas 0 
    --backup-storage-redundancy Zone
```

For syntax details, see [az sql db copy](/cli/azure/sql/db#az-sql-db-copy). For an overview of database copy, see [Copy a transactionally consistent copy of a database in Azure SQL Database](database-copy.md).

### [PowerShell](#tab/powershell)

To configure backup storage redundancy when you're creating a new database, you can specify the `-BackupStorageRedundancy` parameter with the `New-AzSqlDatabase` cmdlet. Possible values are `Geo`, `Zone`, and `Local`. By default, all databases in Azure SQL Database use geo-redundant storage for backups. Geo-restore is disabled if a database is created with locally redundant or zone-redundant backup storage.

This example creates a database in the [General Purpose](service-tier-general-purpose.md) service tier with local backup redundancy:

```powershell
# Create a new database with geo-redundant backup storage.  
New-AzSqlDatabase -ResourceGroupName "ResourceGroup01" -ServerName "Server01" -DatabaseName "Database03" -Edition "GeneralPurpose" -Vcore 2 -ComputeGeneration "Gen5" -BackupStorageRedundancy Local
```

Except for Hyperscale and Basic databases, you can use the `-BackupStorageRedundancy` parameter with the `Set-AzSqlDatabase` cmdlet to update the backup storage redundancy setting for an existing database. Possible values are `Geo`, `Zone`, and `Local`. It might take up to 48 hours for the changes to be applied on the database. Switching from geo-redundant backup storage to locally redundant or zone-redundant storage disables geo-restore.

This example code changes the backup storage redundancy to `Local`:

```powershell
# Change the backup storage redundancy for Database01 to zone-redundant. 
Set-AzSqlDatabase -ResourceGroupName "ResourceGroup01" -DatabaseName "Database01" -ServerName "Server01" -BackupStorageRedundancy Local
```

For details, see [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase).

#### Hyperscale

Carefully consider the configuration option for `--backup-storage-redundancy` when you're creating a Hyperscale database. You can specify storage redundancy only during the database creation process for Hyperscale databases. The selected storage redundancy option will be used for the lifetime of the database for both data storage redundancy and backup storage redundancy. Learn more in [Hyperscale backups and storage redundancy](hyperscale-automated-backups-overview.md#data-and-backup-storage-redundancy).

Existing databases can migrate to different storage redundancy through [database copy](database-copy.md) or point-in-time restore. This example creates a database in the [Hyperscale](service-tier-general-purpose.md) service tier with zone redundancy:

```powershell
# Create a new database with geo-redundant backup storage.  
New-AzSqlDatabase -ResourceGroupName "ResourceGroup01" -ServerName "Server01" -DatabaseName "Database03" -Edition "Hyperscale" -Vcore 2 -ComputeGeneration "Gen5" -BackupStorageRedundancy Zone
```

For syntax details, see [New-AzSqlDatabase](/powershell/module/az.sql/new-azsqldatabase).

Backup storage redundancy of an existing Hyperscale database can't be updated. However, you can use the [database copy command](database-copy.md) to create a copy of the database. Then you can use the `-BackupStorageRedundancy` parameter to update the backup storage redundancy. 

This example copies a Hyperscale database to a new database by using Gen5 hardware and two vCores. The new database has the backup redundancy set to `Zone`.

```powershell
# Change the backup storage redundancy for Database01 to zone-redundant. 
New-AzSqlDatabaseCopy -ResourceGroupName "ResourceGroup01" -ServerName "Server01" -DatabaseName "HSSourceDB" -CopyResourceGroupName "DestResourceGroup" -CopyServerName "DestServer" -CopyDatabaseName "HSDestDB" -Vcore 2 -ComputeGeneration "Gen5" -ComputeModel Provisioned -BackupStorageRedundancy Zone
```

For syntax details, see [New-AzSqlDatabaseCopy](/powershell/module/az.sql/new-azsqldatabasecopy). For an overview of database copy, see [Copy a transactionally consistent copy of a database in Azure SQL Database](database-copy.md).

> [!NOTE]
> To use the `-BackupStorageRedundancy` parameter with database restore, database copy, or create secondary operations, use Azure PowerShell version Az.Sql 2.11.0 or later. 

### [REST API](#tab/rest-api)

It's not currently possible to change backup storage redundancy by using the REST API. 

---

## Next steps

- Database backups are an essential part of any business continuity and disaster recovery strategy because they help protect your data from accidental corruption or deletion. To learn about the other business continuity solutions for SQL Database, see [Business continuity overview](business-continuity-high-availability-disaster-recover-hadr-overview.md).
- For information about how to configure, manage, and restore from long-term retention of automated backups in Azure Blob Storage by using the Azure portal, see [Manage long-term backup retention by using the Azure portal](long-term-backup-retention-configure.md).
- For information about how to configure, manage, and restore from long-term retention of automated backups in Azure Blob Storage by using PowerShell, see [Manage long-term backup retention by using PowerShell](long-term-backup-retention-configure.md). 
- Get more information about how to [restore a database to a point in time by using the Azure portal](recovery-using-backups.md).
- Get more information about how to [restore a database to a point in time by using PowerShell](scripts/restore-database-powershell.md).

