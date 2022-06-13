---
title: Automatic, geo-redundant backups
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: Azure SQL Database and Azure SQL Managed Instance automatically create a local database backup every few minutes and use Azure read-access geo-redundant storage for geo-redundancy.
services:
  - "sql-database"
ms.service: sql-db-mi
ms.subservice: backup-restore
ms.custom:
  - "references_regions"
  - "devx-track-azurepowershell"
  - "devx-track-azurecli"
ms.topic: conceptual
author: SudhirRaparla
ms.author: nvraparl
ms.reviewer: wiassaf, mathoma, danil
ms.date: 04/26/2022
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---
# Automated backups - Azure SQL Database & Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]


## Backups and SQL Managed Instance 

This section highlights specific differences when configuring backups for Azure SQL Managed Instance. 

**Backup storage redundancy**

Azure SQL Database supports **locally-redundant (LRS)**, **zone-redundant (ZRS)**, **geo-redundant (GRS)**, and **geo-zone-redundant (GZRS)** storage blobs for automated backups.  To learn more about storage redundancy, see [Data redundancy](/azure/storage/common/storage-redundancy). 

**Short-term retention**

If you delete a managed instance, all databases on that managed instance are also deleted and cannot be recovered. You cannot restore a deleted managed instance. But if you had configured long-term retention (LTR) for a managed instance, long-term retention backups are not deleted, and can be used to restore databases to a different managed instance in the same subscription, to a point in time when a long-term retention backup was taken.

**Long-term retention** 

It is possible to set the point-in-time restore (PITR) backup retention rate once a database has been deleted in the 0-35 day range. 

LTR backup storage redundancy in Azure SQL Managed Instance is inherited from the backup storage redundancy used by STR at the time the LTR policy is defined and cannot be changed subsequently, even if the STR backup storage redundancy is changed in the future. 

**Backup storage costs**

For managed instances, a backup storage amount equal to 100 percent of the maximum instance storage size, is provided at no extra charge. 

The total billable backup storage size is aggregated at the instance level and is calculated as follows:

`Total billable backup storage size = (total size of full backups + total size of differential backups + total size of log backups) – maximum instance data storage`

For pricing see the [Azure SQL Managed Instance pricing](https://azure.microsoft.com/pricing/details/azure-sql/sql-managed-instance/single/) page. 



## Encrypted backups

If your database is encrypted with TDE, backups are automatically encrypted at rest, including LTR backups. All new databases in Azure SQL are configured with TDE enabled by default. For more information on TDE, see  [Transparent Data Encryption with SQL Database & SQL Managed Instance](/sql/relational-databases/security/encryption/transparent-data-encryption-azure-sql).

## Backup integrity

On an ongoing basis, the Azure SQL engineering team automatically tests the restore of automated database backups. (This testing is not currently available in SQL Managed Instance. You should schedule DBCC CHECKDB on your databases in SQL Managed Instance, scheduled around on your workload.)

Upon point-in-time restore, databases also receive DBCC CHECKDB integrity checks.

Any issues found during the integrity check will result in an alert to the engineering team. For more information, see [Data Integrity in SQL Database](https://azure.microsoft.com/blog/data-integrity-in-azure-sql-database/).

All database backups are taken with the CHECKSUM option to provide additional backup integrity.

## Compliance

When you migrate your database from a DTU-based service tier to a vCore-based service tier, the PITR retention is preserved to ensure that your application's data recovery policy isn't compromised. If the default retention doesn't meet your compliance requirements, you can change the PITR retention period. For more information, see [Change the PITR backup retention period](#change-the-short-term-retention-policy).

[!INCLUDE [GDPR-related guidance](~/../azure/includes/gdpr-intro-sentence.md)]

## Change the short-term retention policy

You can change the default PITR backup retention period and the differential backup frequency by using the Azure portal, PowerShell, or the REST API. The following examples illustrate how to change the PITR retention to 28 days and the differential backups to 24 hour interval.

> [!WARNING]
> If you reduce the current retention period, you lose the ability to restore to points in time older than the new retention period. Backups that are no longer needed to provide PITR within the new retention period are deleted. If you increase the current retention period, you do not immediately gain the ability to restore to older points in time within the new retention period. You gain that ability over time, as the system starts to retain backups for longer.

> [!NOTE]
> These APIs will affect only the PITR retention period. If you configured LTR for your database, it won't be affected. For information about how to change LTR retention periods, see [Long-term retention](long-term-retention-overview.md).

### Change the short-term retention policy using the Azure portal

To change the PITR backup retention period or the differential backup frequency for active databases by using the Azure portal, go to the server or managed instance with the databases whose retention period you want to change. Select **Backups** in the left pane, then select the **Retention policies** tab. Select the database(s) for which you want to change the PITR backup retention. Then select **Configure retention** from the action bar.

#### [SQL Database](#tab/single-database)

![Change PITR retention, server level](./media/automated-backups-overview/configure-backup-retention-sqldb.png)

#### [SQL Managed Instance](#tab/managed-instance)

![Change PITR retention, managed instance](./media/automated-backups-overview/configure-backup-retention-sqlmi.png)

---

### Change the short-term retention policy using Azure CLI

Prepare your environment for the Azure CLI.

[!INCLUDE[azure-cli-prepare-your-environment-no-header](../includes/azure-cli-prepare-your-environment-no-header.md)]

#### [SQL Database](#tab/single-database)

Change the PITR backup retention and differential backup frequency for active Azure SQL Databases by using the following example.

```azurecli
# Set new PITR differential backup frequency on an active individual database
# Valid backup retention must be between 1 and 35 days
# Valid differential backup frequency must be ether 12 or 24
az sql db str-policy set \
    --resource-group myresourcegroup \
    --server myserver \
    --name mydb \
    --retention-days 28 \
    --diffbackup-hours 24
```

#### [SQL Managed Instance](#tab/managed-instance)

Use the following example to change the PITR backup retention of a **single active** database in a SQL Managed Instance.

```azurecli
# Set new PITR backup retention period on an active individual database
# Valid backup retention must be between 1 and 35 days
az sql midb short-term-retention-policy set \
    --resource-group myresourcegroup \
    --managed-instance myinstance \
    --name mymanageddb \
    --retention-days 1 \
```

Use the following example to change the PITR backup retention for **all active** databases in a SQL Managed Instance.

```azurecli
# Set new PITR backup retention period for ALL active databases
# Valid backup retention must be between 1 and 35 days
az sql midb short-term-retention-policy set \
    --resource-group myresourcegroup \
    --managed-instance myinstance \
    --retention-days 1 \
```

---

### Change the short-term retention policy using PowerShell

[!INCLUDE [updated-for-az](../includes/updated-for-az.md)]
> [!IMPORTANT]
> The PowerShell AzureRM module is still supported by SQL Database and SQL Managed Instance, but all future development is for the Az.Sql module. For more information, see [AzureRM.Sql](/powershell/module/AzureRM.Sql/). The arguments for the commands in the Az module are substantially identical to those in the AzureRm modules.

#### [SQL Database](#tab/single-database)

To change the PITR backup retention and differential backup frequency for active Azure SQL Databases, use the following PowerShell example.

```powershell
# SET new PITR backup retention period on an active individual database
# Valid backup retention must be between 1 and 35 days
Set-AzSqlDatabaseBackupShortTermRetentionPolicy -ResourceGroupName resourceGroup -ServerName testserver -DatabaseName testDatabase -RetentionDays 28
```

```powershell
# SET new PITR differential backup frequency on an active individual database
# Valid differential backup frequency must be ether 12 or 24. 
Set-AzSqlDatabaseBackupShortTermRetentionPolicy -ResourceGroupName resourceGroup -ServerName testserver -DatabaseName testDatabase -RetentionDays 28 -DiffBackupIntervalInHours 24
```

#### [SQL Managed Instance](#tab/managed-instance)

To change the PITR backup retention for an **single active** database in a SQL Managed Instance, use the following PowerShell example.

```powershell
# SET new PITR backup retention period on an active individual database
# Valid backup retention must be between 1 and 35 days
Set-AzSqlInstanceDatabaseBackupShortTermRetentionPolicy -ResourceGroupName resourceGroup -InstanceName testserver -DatabaseName testDatabase -RetentionDays 1
```

To change the PITR backup retention for **all active** databases in a SQL Managed Instance, use the following PowerShell example.

```powershell
# SET new PITR backup retention period for ALL active databases
# Valid backup retention must be between 1 and 35 days
Get-AzSqlInstanceDatabase -ResourceGroupName resourceGroup -InstanceName testserver | Set-AzSqlInstanceDatabaseBackupShortTermRetentionPolicy -RetentionDays 1
```

To change the PITR backup retention for an **single deleted** database in a SQL Managed Instance, use the following PowerShell example.
 
```powershell
# SET new PITR backup retention on an individual deleted database
# Valid backup retention must be between 0 (no retention) and 35 days. Valid retention rate can only be lower than the period of the retention period when database was active, or remaining backup days of a deleted database.
Get-AzSqlDeletedInstanceDatabaseBackup -ResourceGroupName resourceGroup -InstanceName testserver -DatabaseName testDatabase | Set-AzSqlInstanceDatabaseBackupShortTermRetentionPolicy -RetentionDays 0
```

To change the PITR backup retention for **all deleted** databases in a SQL Managed Instance, use the following PowerShell example.

```powershell
# SET new PITR backup retention for ALL deleted databases
# Valid backup retention must be between 0 (no retention) and 35 days. Valid retention rate can only be lower than the period of the retention period when database was active, or remaining backup days of a deleted database
Get-AzSqlDeletedInstanceDatabaseBackup -ResourceGroupName resourceGroup -InstanceName testserver | Set-AzSqlInstanceDatabaseBackupShortTermRetentionPolicy -RetentionDays 0
```

Zero (0) days retention would denote that backup is immediately deleted and no longer kept for a deleted database.
Once PITR backup retention has been reduced for a deleted database, it no longer can be increased.

---

### Change the short-term retention policy using the REST API

The below request updates the retention period to 28 days and also sets the differential backup frequency to 24 hours.


#### [SQL Database](#tab/single-database)

#### Sample Request

```http
PUT https://management.azure.com/subscriptions/00000000-1111-2222-3333-444444444444/resourceGroups/resourceGroup/providers/Microsoft.Sql/servers/testserver/databases/testDatabase/backupShortTermRetentionPolicies/default?api-version=2021-02-01-preview
```

#### Request Body

```json
{ 
    "properties":{
        "retentionDays":28,
        "diffBackupIntervalInHours":24
  }
}
```

#### Sample Response: 

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


For more information, see [Backup Retention REST API](/rest/api/sql/backupshorttermretentionpolicies).

#### [SQL Managed Instance](#tab/managed-instance)

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

For more information, see [Backup Retention REST API](/rest/api/sql/backupshorttermretentionpolicies).

---

## Hyperscale backups and storage redundancy

Hyperscale databases in Azure SQL Database use a [unique architecture](service-tier-hyperscale.md#distributed-functions-architecture) with highly scalable storage and compute performance tiers.

Hyperscale backups are snapshot based and are nearly instantaneous. Log generated is stored in long term Azure storage for the backup retention period. Hyperscale architecture does not use full database backups or log backups and hence the backup frequency, storage costs, scheduling, storage and redundancy and restore capabilities described in the previous sections of this article do not apply.

### Hyperscale backup and restore performance

Storage and compute separation enables Hyperscale to push down backup and restore operation to the storage layer to reduce the processing burden on the primary compute replica. As a result, database backups don't impact performance of the primary compute node. 

Backup and restore operations for Hyperscale databases are fast regardless of data size due to the use of storage snapshots. A database can be restored to any point in time within its backup retention period. Point in time recovery (PITR) is achieved by reverting to file snapshots, and as such is not a size of data operation. Restore of a Hyperscale database within the same Azure region is a constant-time operation, and even multiple-terabyte databases can be restored in minutes instead of hours or days. Creation of new databases by restoring an existing backup or copying the database also takes advantage of this feature: creating database copies for development or testing purposes, even of multi-terabyte databases, is doable in minutes within the same region when the same storage type is used.

### Hyperscale backup retention

Default short-term backup retention (STR) for Hyperscale databases is 7 days; long-term retention (LTR) policies aren't currently supported.

> [!NOTE]
> Short-term backup retention up to 35 days for Hyperscale databases is now in preview. 

### Hyperscale backup scheduling

There are no traditional full, differential, and transaction log backups for Hyperscale databases. Instead, regular storage snapshots of data files are taken. The generated transaction log is retained as-is for the configured retention period. At restore time, relevant transaction log records are applied to the restored storage snapshot, resulting in a transactionally-consistent database without any data loss as of the specified point in time within the retention period. 

### Hyperscale backup storage costs

Hyperscale backup storage cost depends on the choice of region and backup storage redundancy. It also depends on the workload type. Write-heavy workloads are more likely to change data pages frequently, which results in larger storage snapshots. Such workloads also generate more transaction log, contributing to the overall backup costs. Backup storage is charged per GB/month consumed, for pricing details see the [Azure SQL Database pricing](https://azure.microsoft.com/pricing/details/sql-database/single/) page. 

For Hyperscale, billable backup storage is calculated as follows: 

`Total billable backup storage size = (Data backup storage size + Log backup storage size)` 

Data storage size is not included in the billable backup as it is already billed as allocated database storage. 

Deleted Hyperscale databases incur backup costs to support recovery to a point in time before deletion. For a deleted Hyperscale database, billable backup storage is calculated as follows: 

`Total billable backup storage size for deleted Hyperscale database = (Data storage size + Data backup size + Log backup storage size) * (remaining backup retention period after deletion/configured backup retention period)` 

Data storage size is included in the formula because allocated database storage is not billed separately for a deleted database. For a deleted database, data is stored post deletion to enable recovery during the configured backup retention period. Billable backup storage for a deleted database reduces gradually over time after it is deleted. It becomes zero when backups are no longer retained, and recovery is no longer possible. However if it is a permanent deletion and backups are no longer needed, to optimize costs you can reduce retention before deleting the database.

### Hyperscale monitor backup consumption

In Hyperscale, data backup storage size (snapshot backup size), data storage size(database size) and log backup storage size(transactions log backup size) are reported via Azure Monitor metrics. 

To view backup and data storage metrics in the Azure portal, follow these steps: :

1.	Go to the Hyperscale database for which you'd like to monitor backup and data storage metrics.
2.	Select the Metrics page in the **Monitoring** section.

:::image type="content" source="./media/automated-backups-overview/hyperscale-backup-storage-metrics.png" alt-text="Screenshot of the Azure portal showing the Hyperscale Backup storage metrics":::

3.  From the Metric drop-down list, select the **Data backup Storage** and **Log Backup Storage** metrics with an appropriate aggregation rule. 


#### Reduce backup storage consumption

Backup storage consumption for a Hyperscale database depends on the retention period, choice of region, backup storage redundancy and workload type. Consider some of the following tuning techniques to reduce your backup storage consumption for a Hyperscale database:

- Reduce the [backup retention period](#change-the-short-term-retention-policy-using-the-azure-portal) to the minimum possible for your needs.
- Avoid doing large write-operations, such as index maintenance, more frequently than you need to. For index maintenance recommendations, see [Optimize index maintenance to improve query performance and reduce resource consumption](/sql/relational-databases/indexes/reorganize-and-rebuild-indexes).
- For large data-load operations, consider using data compression when appropriate.
- Use the `tempdb` database instead of permanent tables in your application logic to store temporary results and/or transient data.
- Use locally-redundant or zone-redundant backup storage when geo-restore capability is unnecessary (for example: dev/test environments). 

### Hyperscale storage redundancy applies to both data storage and backup storage

Hyperscale supports configurable storage redundancy. When creating a Hyperscale database, you can choose your preferred storage type: read-access geo-redundant storage (RA-GRS), zone-redundant storage (ZRS), or locally redundant storage (LRS) Azure standard storage. The selected storage redundancy option is used for the lifetime of the database for both data storage redundancy and backup storage redundancy.

### Consider storage redundancy carefully when you create a Hyperscale database

Backup storage redundancy for Hyperscale databases can only be set during database creation. This setting cannot be modified once the resource is provisioned. Geo-restore is only available when geo-redundant storage (RA-GRS) has been chosen for backup storage redundancy. The [database copy](database-copy.md) process can be used to update the storage redundancy settings for an existing Hyperscale database. Copying a database to a different storage type will be a size-of-data operation. Find example code in [configure backup storage redundancy](#configure-backup-storage-redundancy).

> [!IMPORTANT]
> Zone-redundant storage is currently only available in [certain regions](/azure/storage/common/storage-redundancy#zone-redundant-storage). 

### Restoring a Hyperscale database to a different region

If you need to restore a Hyperscale database in Azure SQL Database to a region other than the one it's currently hosted in, as part of a disaster recovery operation or drill, relocation, or any other reason, the primary method is to do a geo-restore of the database. This involves exactly the same steps as what you would use to restore any other database in SQL Database to a different region:

1. Create a [server](logical-servers.md) in the target region if you don't already have an appropriate server there.  This server should be owned by the same subscription as the original (source) server.
2. Follow the instructions in the [geo-restore](./recovery-using-backups.md#geo-restore) section of the page on restoring a database in Azure SQL Database from automatic backups.

> [!NOTE]
> Because the source and target are in separate regions, the database cannot share snapshot storage with the source database as in non-geo restores, which complete quickly regardless of database size. In the case of a geo-restore of a Hyperscale database, it will be a size-of-data operation, even if the target is in the paired region of the geo-replicated storage. Therefore, a geo-restore will take time proportional to the size of the database being restored. If the target is in the paired region, data transfer will be within a region, which will be significantly faster than a cross-region data transfer, but it will still be a size-of-data operation.

If you prefer, you can copy the database to a different region as well. Learn about [Database Copy for Hyperscale](database-copy.md#database-copy-for-azure-sql-hyperscale).

## Configure backup storage redundancy

Backup storage redundancy for databases in Azure SQL Database can be configured at the time of database creation or can be updated for an existing database; the changes made to an existing database apply to future backups only. The default value is geo-redundant storage. For differences in pricing between locally redundant, zone-redundant and geo-redundant backup storage visit [managed instance pricing page](https://azure.microsoft.com/pricing/details/azure-sql/sql-managed-instance/single/). Storage redundancy for Hyperscale databases is unique: learn more in [Hyperscale backups and storage redundancy](#hyperscale-backups-and-storage-redundancy).

For Azure SQL Managed Instance, backup storage redundancy is set at the instance level, and it is applied for all belonging managed databases. It can be configured at the time of an instance creation or updated for existing instances; the backup storage redundancy change would trigger then a new full backup per database and the change will apply for all future backups. The default storage redundancy type is geo-redundancy (RA-GRS).

### Configure backup storage redundancy by using the Azure portal

#### [SQL Database](#tab/single-database)

In Azure portal, you can configure the backup storage redundancy on the **Create SQL Database** pane. The option is available under the Backup Storage Redundancy section. 

![Open Create SQL Database pane](./media/automated-backups-overview/sql-database-backup-storage-redundancy.png)

#### [SQL Managed Instance](#tab/managed-instance)

In the Azure portal, during an instance creation, the default option for the backup storage redundancy is Geo-redundancy. The option to change it is located on the **Compute + storage** pane accessible from the **Configure Managed Instance** option on the **Basics** tab.

![Open Compute+Storage configuration-pane](./media/automated-backups-overview/open-configuration-blade-managed-instance.png)

Find the option to select backup storage redundancy on the **Compute + storage** pane.

![Configure backup storage redundancy](./media/automated-backups-overview/select-backup-storage-redundancy-managed-instance.png)

To change the Backup storage redundancy option for an existing instance, go to the **Compute + storage** pane, choose the new backup option and select **Apply**. For now, this change will be applied only for PITR backups, while LTR backups will retain the old storage redundancy type. The time it takes to perform the backup redundancy change depends on the size of the all the databases within a single managed instance. Changing the backup redundancy will take more time for instances that have large databases. It's possible to combine the backup storage redundancy change operation with the UpdateSLO operation. Use the **Notification** pane of the Azure portal to view the status of the change operation. 

:::image type="content" source="./media/automated-backups-overview/change-backup-storage-redundancy-managed-instance-notification.png" alt-text="Change backup storage redundancy notification":::

---

### Configure backup storage redundancy by using the Azure CLI

#### [SQL Database](#tab/single-database)

To configure backup storage redundancy when creating a new database, you can specify the `--backup-storage-redundancy` parameter with the `az sql db create` command. Possible values are `Geo`, `Zone`, and `Local`. By default, all databases in Azure SQL Database use geo-redundant storage for backups. Geo-restore is disabled if a database is created or updated with local or zone redundant backup storage.

This example creates a database in the [General Purpose](service-tier-general-purpose.md) service tier with local backup redundancy:

```azurecli
az sql db create \
    --resource-group myresourcegroup \
    --server myserver \
    --name mydb \
    --tier GeneralPurpose \
    --backup-storage-redundancy Local
```

Carefully consider the configuration option for `--backup-storage-redundancy` when creating a Hyperscale database. Storage redundancy can only be specified during the database creation process for Hyperscale databases. The selected storage redundancy option will be used for the lifetime of the database for both data storage redundancy and backup storage redundancy.  Learn more in [Hyperscale backups and storage redundancy](#hyperscale-backups-and-storage-redundancy). 

Existing Hyperscale databases can migrate to different storage redundancy using [database copy](database-copy.md) or point in time restore: sample code to copy a Hyperscale database follows in this section.

This example creates a database in the [Hyperscale](service-tier-general-purpose.md) service tier with Zone redundancy:

```azurecli
az sql db create \
    --resource-group myresourcegroup \
    --server myserver \
    --name mydb \
    --tier Hyperscale \
    --backup-storage-redundancy Zone
```

For more information, see [az sql db create](/cli/azure/sql/db#az-sql-db-create) and [az sql db update](/cli/azure/sql/db#az-sql-db-update).

Except for Hyperscale and Basic tier databases, you can update the backup storage redundancy setting for an existing database with the `--backup-storage-redundancy` parameter and the `az sql db update` command. It may take up to 48 hours for the changes to be applied on the database. Switching from geo-redundant backup storage to local or zone redundant storage disables geo-restore.

This example code changes the backup storage redundancy to `Local`.

```azurecli
az sql db update \
    --resource-group myresourcegroup \
    --server myserver \
    --name mydb \
    --backup-storage-redundancy Local
```

You cannot update the backup storage redundancy of a Hyperscale database directly. However, you can change it using [the database copy command](database-copy.md) with the `--backup-storage-redundancy` parameter. This example copies a Hyperscale database to a new database using Gen5 hardware and two vCores. The new database has the backup redundancy set to `Zone`.

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

For syntax details, see [az sql db copy](/cli/azure/sql/db#az-sql-db-copy). For an overview of database copy, visit [Copy a transactionally consistent copy of a database in Azure SQL Database](database-copy.md).

#### [SQL Managed Instance](#tab/managed-instance)

To change the backup storage redundancy after your SQL Managed Instance is created using the Azure CLI, specify the `-BackupStorageRedundancy` parameter when using the `az sql mi update` cmdlet.  View the [update mi backup storage redundancy](/cli/azure/sql/mi#az-sql-mi-update-examples) example. 

Possible values for `-BackupStorageRedundancy` are `Geo` for geo-redundant, `Zone` for zone-redundant, `Local` for locally-redundant, and `GeoZone` for geo-zone redundant backup storage. 

---

### Configure backup storage redundancy by using PowerShell

#### [SQL Database](#tab/single-database)

To configure backup storage redundancy when creating a new database, you can specify the `-BackupStorageRedundancy` parameter with the `New-AzSqlDatabase` cmdlet. Possible values are `Geo`, `Zone`, and `Local`. By default, all databases in Azure SQL Database use geo-redundant storage for backups. Geo-restore is disabled if a database is created with local or zone redundant backup storage.

This example creates a database in the [General Purpose](service-tier-general-purpose.md) service tier with local backup redundancy:

```powershell
# Create a new database with geo-redundant backup storage.  
New-AzSqlDatabase -ResourceGroupName "ResourceGroup01" -ServerName "Server01" -DatabaseName "Database03" -Edition "GeneralPurpose" -Vcore 2 -ComputeGeneration "Gen5" -BackupStorageRedundancy Local
```

Carefully consider the configuration option for `--backup-storage-redundancy` when creating a Hyperscale database. Storage redundancy can only be specified during the database creation process for Hyperscale databases. The selected storage redundancy option will be used for the lifetime of the database for both data storage redundancy and backup storage redundancy.  Learn more in [Hyperscale backups and storage redundancy](#hyperscale-backups-and-storage-redundancy).

Existing databases can migrate to different storage redundancy using [database copy](database-copy.md) or point in time restore: sample code to copy a Hyperscale database follows in this section.

This example creates a database in the [Hyperscale](service-tier-general-purpose.md) service tier with Zone redundancy:

```powershell
# Create a new database with geo-redundant backup storage.  
New-AzSqlDatabase -ResourceGroupName "ResourceGroup01" -ServerName "Server01" -DatabaseName "Database03" -Edition "Hyperscale" -Vcore 2 -ComputeGeneration "Gen5" -BackupStorageRedundancy Zone
```

For syntax details visit [New-AzSqlDatabase](/powershell/module/az.sql/new-azsqldatabase).

Except for Hyperscale and Basic tier databases, you can use the `-BackupStorageRedundancy` parameter with the `Set-AzSqlDatabase` cmdlet to update the backup storage redundancy setting for an existing database. Possible values are Geo, Zone, and Local. It may take up to 48 hours for the changes to be applied on the database. Switching from geo-redundant backup storage to local or zone redundant storage disables geo-restore.

This example code changes the backup storage redundancy to `Local`.

```powershell
# Change the backup storage redundancy for Database01 to zone-redundant. 
Set-AzSqlDatabase -ResourceGroupName "ResourceGroup01" -DatabaseName "Database01" -ServerName "Server01" -BackupStorageRedundancy Local
```

For details visit [Set-AzSqlDatabase](/powershell/module/az.sql/set-azsqldatabase)

Backup storage redundancy of an existing Hyperscale database cannot be updated. However, you can use the [database copy command](database-copy.md) to create a copy of the database and use the `-BackupStorageRedundancy` parameter to update the backup storage redundancy. This example copies a Hyperscale database to a new database using Gen5 hardware and two vCores. The new database has the backup redundancy set to `Zone`.

```powershell
# Change the backup storage redundancy for Database01 to zone-redundant. 
New-AzSqlDatabaseCopy -ResourceGroupName "ResourceGroup01" -ServerName "Server01" -DatabaseName "HSSourceDB" -CopyResourceGroupName "DestResourceGroup" -CopyServerName "DestServer" -CopyDatabaseName "HSDestDB" -Vcore 2 -ComputeGeneration "Gen5" -ComputeModel Provisioned -BackupStorageRedundancy Zone
```

For syntax details, visit [New-AzSqlDatabaseCopy](/powershell/module/az.sql/new-azsqldatabasecopy). 

For an overview of database copy, visit [Copy a transactionally consistent copy of a database in Azure SQL Database](database-copy.md).

> [!NOTE]
> To use -BackupStorageRedundancy parameter with database restore, database copy or create secondary operations, use Azure PowerShell version Az.Sql 2.11.0. 


#### [SQL Managed Instance](#tab/managed-instance)

To configure backup storage redundancy when you create your managed instance, specify the `-BackupStorageRedundancy` parameter when using the [New-AzSqlInstance](/powershell/module/az.sql/new-azsqlinstance) cmdlet. To change the backup storage redundancy for an existing managed instance, specify the `-BackupStorageRedundancy` parameter when using the `Set-AzSqlInstance` cmdlet. Review the [update an existing instance to be zone redundant](/powershell/module/az.sql/set-azsqlinstance#example-7-update-an-existing-instance-to-be-zone-redundant) example to learn more. 

Possible values for `-BackupStorageRedundancy` are `Geo` for geo-redundant, `Zone` for zone-redundant, `Local` for locally-redundant, and `GeoZone` for geo-zone redundant backup storage. 


---

## Use Azure Policy to enforce backup storage redundancy

If you have data residency requirements that require you to keep all your data in a single Azure region, you may want to enforce zone-redundant or locally redundant backups for your SQL Database or Managed Instance using Azure Policy. 
Azure Policy is a service that you can use to create, assign, and manage policies that apply rules to Azure resources. Azure Policy helps you to keep these resources compliant with your corporate standards and service level agreements. For more information, see [Overview of Azure Policy](/azure/governance/policy/overview). 

### Built-in backup storage redundancy policies 

Following new built-in policies are added, which can be assigned at the subscription or resource group level to block creation of new database(s) or instance(s) with geo-redundant backup storage. 

[SQL Database should avoid using GRS backup redundancy](https://portal.azure.com/#blade/Microsoft_Azure_Policy/PolicyDetailBlade/definitionId/%2Fproviders%2FMicrosoft.Authorization%2FpolicyDefinitions%2Fb219b9cf-f672-4f96-9ab0-f5a3ac5e1c13)


[SQL Managed Instances should avoid using GRS backup redundancy](https://portal.azure.com/#blade/Microsoft_Azure_Policy/PolicyDetailBlade/definitionId/%2Fproviders%2FMicrosoft.Authorization%2FpolicyDefinitions%2Fa9934fd7-29f2-4e6d-ab3d-607ea38e9079)

A full list of built-in policy definitions for SQL Database and Managed Instance can be found [here](./policy-reference.md).

To enforce data residency requirements at an organizational level, these policies can be assigned to a subscription. After these policies are assigned at a subscription level, users in the given subscription will not be able to create a database or a managed instance with geo-redundant backup storage via Azure portal or Azure PowerShell. 

> [!IMPORTANT]
> Azure policies are not enforced when creating a database via T-SQL. To enforce data residency when creating a database using T-SQL, [use 'LOCAL' or 'ZONE' as input to BACKUP_STORAGE_REDUNDANCY paramater in CREATE DATABASE statement](/sql/t-sql/statements/create-database-transact-sql#create-database-using-zone-redundancy-for-backups).

Learn how to assign policies using the [Azure portal](/azure/governance/policy/assign-policy-portal) or [Azure PowerShell](/azure/governance/policy/assign-policy-powershell)

## Next steps

- Database backups are an essential part of any business continuity and disaster recovery strategy because they protect your data from accidental corruption or deletion. To learn about the other SQL Database business continuity solutions, see [Business continuity overview](business-continuity-high-availability-disaster-recover-hadr-overview.md).
- For information about how to configure, manage, and restore from long-term retention of automated backups in Azure Blob storage by using the Azure portal, see [Manage long-term backup retention by using the Azure portal](long-term-backup-retention-configure.md).
- For information about how to configure, manage, and restore from long-term retention of automated backups in Azure Blob storage by using PowerShell, see [Manage long-term backup retention by using PowerShell](long-term-backup-retention-configure.md). 
- Get more information about how to [restore a database to a point in time by using the Azure portal](recovery-using-backups.md).
- Get more information about how to [restore a database to a point in time by using PowerShell](scripts/restore-database-powershell.md).
- To learn all about backup storage consumption on Azure SQL Managed Instance, see [Backup storage consumption on Managed Instance explained](https://aka.ms/mi-backup-explained).
- To learn how to fine-tune backup storage retention and costs for Azure SQL Managed Instance, see [Fine tuning backup storage costs on Managed Instance](https://aka.ms/mi-backup-tuning).
