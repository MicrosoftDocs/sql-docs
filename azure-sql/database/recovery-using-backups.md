---
title: Restore a database from a backup
titleSuffix: Azure SQL Database
description: Learn about point-in-time restore, which enables you to roll back a database in Azure SQL Database up to 35 days.
services:
  - "sql-database"
ms.service: sql-database
ms.subservice: backup-restore
ms.topic: how-to
ms.custom:  "azure-sql-split"
author: SudhirRaparla
ms.author: nvraparl
ms.reviewer: wiassaf, mathoma, danil
ms.date: 06/25/2022
monikerRange: "= azuresql || = azuresql-db"
---
# Recover using automated database backups - Azure SQL Database
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

<!---
Some of the content in this article is duplicated in /azure-sql/managed-instance/recovery-using-backups.md. Any relevant changes made to this article should be made in the other article as well. 
--->


> [!div class="op_single_selector"]
> * [Azure SQL Database](recovery-using-backups.md)
> * [Azure SQL Managed Instance](../managed-instance/recovery-using-backups.md)

This article provides steps to recover any database from a backup in Azure SQL Database, including Hyperscale databases. For Azure SQL Managed Instance, see [recovery using backup](../managed-instance/recovery-using-backups.md). 


Automatic backups protect your databases from user and application errors, accidental database deletion, and prolonged outages. This built-in capability is available for all service tiers and compute sizes. The following options are available for database recovery by using [automated database backups](automated-backups-overview.md). You can:

- Create a new database on the same server, recovered to a specified point in time within the retention period.
- Create a database on the same server, recovered to the deletion time for a deleted database.
- Create a new database on any server in the same region, recovered to the time of a recent backup.
- Create a new database on any server in any other region, recovered to the point of the most recent replicated backups. 

If you configured [backup long-term retention](long-term-retention-overview.md), you can also create a new database from any long-term retention backup on any server.

> [!IMPORTANT]
> - You can't overwrite an existing database during restore.
> - Database restore operations do not restore the tags of the original database. 

When you're using the Standard or Premium service tier in the DTU purchasing model, your database restore might incur an extra storage cost. The extra cost is incurred when the maximum size of the restored database is greater than the amount of storage included with the target database's service tier and service objective. For pricing details of extra storage, see the [SQL Database pricing page](https://azure.microsoft.com/pricing/details/sql-database/). If the actual amount of used space is less than the amount of storage included, you can avoid this extra cost by setting the maximum database size to the included amount.



## Recovery time

The recovery time to restore a database by using automated database backups is affected by several factors:

- The size of the database.
- The compute size of the database.
- The number of transaction logs involved.
- The amount of activity that needs to be replayed to recover to the restore point.
- The network bandwidth if the restore is to a different region.
- The number of concurrent restore requests being processed in the target region.

For a large or very active database, the restore might take several hours. If there is a prolonged outage in a region, it's possible that a high number of geo-restore requests will be initiated for disaster recovery. When there are many requests, the recovery time for individual databases can increase. Most database restores finish in less than 12 hours.

For a single subscription, there are limitations on the number of concurrent restore requests. These limitations apply to any combination of point-in-time restores, geo-restores, and restores from long-term retention backup: 

| **Deployment option** | **Max # of concurrent requests being processed** | **Max # of concurrent requests being submitted** |
| :--- | --: | --: |
|**Single database (per subscription)**|30|100|
|**Elastic pool (per pool)**|4|2000|


There isn't a built-in method to restore the entire server. For an example of how to accomplish this task, see [Azure SQL Database: Full server recovery](https://gallery.technet.microsoft.com/Azure-SQL-Database-Full-82941666).

## Permissions

To recover by using automated backups, you must be a member of the Contributor role or the SQL Server Contributor role in the subscription or resource group containing the logical server, or you must be the subscription or resource group owner. For more information, see [Azure RBAC: Built-in roles](/azure/role-based-access-control/built-in-roles). You can recover by using the Azure portal, PowerShell, or the REST API. You can't use Transact-SQL.

## Point-in-time restore

You can restore any database to an earlier point in time within its retention period.  The restore request can specify any service tier or compute size for the restored database. When restoring a database into an elastic pool, ensure you have sufficient resources in the pool to accommodate the database.

When complete, the restore creates a new database on the same server as the original database. The restored database is charged at normal rates, based on its service tier and compute size. You don't incur charges until the database restore is complete.

You generally restore a database to an earlier point for recovery purposes. You can treat the restored database as a replacement for the original database or use it as a data source to update the original database.

> [!IMPORTANT]
> - You can only run restore on the same server, cross-server restoration is not supported by point-in-time restore.
> - You can't perform a point-in-time restore on a geo-secondary database. You can do so only on a primary database.
> - The `BackupFrequency` parameter isn't supported for Hyperscale databases. 

- **Database replacement**

  If you intend the restored database to be a replacement for the original database, you should specify the original database's compute size and service tier. You can then rename the original database and give the restored database the original name by using the [ALTER DATABASE](/sql/t-sql/statements/alter-database-azure-sql-database) command in T-SQL.

- **Data recovery**

  If you plan to retrieve data from the restored database to recover from a user or application error, you need to write and execute a data recovery script that extracts data from the restored database and applies to the original database. Although the restore operation may take a long time to complete, the restoring database is visible in the database list throughout the restore process. If you delete the database during the restore, the restore operation will be canceled and you will not be charged for the database that did not complete the restore.

### [Azure portal](#tab/azure-portal)

To recover a database to a point in time by using the Azure portal, open the database overview page and select **Restore** on the toolbar. Choose the backup source, and select the point-in-time backup point from which a new database will be created.

:::image type="content" source="./media/recovery-using-backups/pitr-backup-sql-database-annotated.png" alt-text="Screenshot of database restore options for SQL Database.":::

### [Azure CLI](#tab/azure-cli)

To restore a database by using the Azure CLI, see [az sql db restore](/cli/azure/sql/db#az-sql-db-restore).

### [PowerShell](#tab/powershell)

[!INCLUDE [updated-for-az](../includes/updated-for-az.md)]
> [!IMPORTANT]
> The PowerShell Azure Resource Manager module is still supported by SQL Database, but all future development is for the Az.Sql module. For these cmdlets, see [AzureRM.Sql](/powershell/module/AzureRM.Sql/). Arguments for the commands in the Az module and in Azure Resource Manager modules are to a great extent identical.

> [!NOTE]
> Restore points represent a period between the earliest restore point and the latest log backup point. Information on latest restore point is currently unavailable on Azure PowerShell.

To restore a standalone or pooled database, see [Restore-AzSqlDatabase](/powershell/module/az.sql/restore-azsqldatabase).

  | Cmdlet | Description |
  | --- | --- |
  | [Get-AzSqlDatabase](/powershell/module/az.sql/get-azsqldatabase) |Gets one or more databases. |
  | [Restore-AzSqlDatabase](/powershell/module/az.sql/restore-azsqldatabase) |Restores a database. |

  > [!TIP]
  > For a sample PowerShell script that shows how to perform a point-in-time restore of a database, see [Restore a database by using PowerShell](scripts/restore-database-powershell.md).

### [Rest API](#tab/rest-api)

To restore a database by using the REST API:

| API | Description |
| --- | --- |
| [REST (createMode=Recovery)](/rest/api/sql/databases) |Restores a database. |
| [Get Create or Update Database Status](/rest/api/sql/operations) |Returns the status during a restore operation. |

---

## Long-term backup restore

To perform a restore operation of a long-term backup, you can use the Azure CLI, Azure PowerShell, and the REST API. For more details, see [Restore a long-term backup](long-term-backup-retention-configure.md#view-backups-and-restore-from-a-backup). Long-term retention is not applicable to Hyperscale databases. 

### [Azure portal](#tab/azure-portal)

To recover a long-term backup by using the Azure portal, go to your logical server, select **Backups** under **Settings** and then choose **Manage** under **Available LTR backups** for the database you're trying to restore: 

:::image type="content" source="media/long-term-backup-retention-configure/ltr-available-backups-tab.png" alt-text="Screenshot of the Azure portal showing available long term retention backups.":::


### [Azure CLI](#tab/azure-cli)


To restore a database by using the Azure CLI, see [az sql db ltr-backup restore](/cli/azure/sql/ltr-backup#az-sql-db-ltr-backup-restore).

### [PowerShell](#tab/powershell)

To restore a standalone or pooled database, see [Restore-AzSqlDatabase](/powershell/module/az.sql/restore-azsqldatabase).

  | Cmdlet | Description |
  | --- | --- |
  | [Get-AzSqlDatabase](/powershell/module/az.sql/get-azsqldatabase) |Gets one or more databases. |
  | [Get-AzSqlDatabaseGeoBackup](/powershell/module/az.sql/get-azsqldatabasegeobackup) |Gets a geo-redundant backup of a database. |
  | [Restore-AzSqlDatabase -FromLongTermRetentionBackup](/powershell/module/az.sql/restore-azsqldatabase) |Restores a database from long-term backup. |


### [Rest API](#tab/rest-api)

To restore a database by using the REST API:

| API | Description |
| --- | --- |
| [REST (createMode=Recovery)](/rest/api/sql/databases) |Restores a database. |
| [Get Create or Update Database Status (createMode=RestoreLongTermRetentionBackup)](/rest/api/sql/operations) |Restores a database from long-term backup. |

---

## Deleted database restore

You can restore a deleted database to the deletion time, or an earlier point in time, on the same server.

> [!IMPORTANT]
> If you delete a server, all its databases are also deleted and can't be recovered. You can't restore a deleted server.

### [Azure portal](#tab/azure-portal)

You restore deleted databases from the Azure portal, using the **Deleted databases** page for the server that hosted the deleted database.

> [!TIP]
> It may take several minutes for recently deleted databases to appear on the **Deleted databases** page in Azure portal, or when displaying deleted databases programmatically. 

To recover a deleted database to the deletion time by using the Azure portal, open the server overview page, and select **Deleted databases**. Select a deleted database that you want to restore, and type the name for the new database that will be created with data restored from the backup.

  :::image type="content" source="./media/recovery-using-backups/restore-deleted-sql-database-annotated.png" alt-text="Screenshot of restore deleted database":::

### [Azure CLI](#tab/azure-cli)

To restore a database by using the Azure CLI, see [az sql db restore](/cli/azure/sql/db#az-sql-db-restore).

### [PowerShell](#tab/powershell)

For a sample PowerShell script showing how to restore a deleted database in Azure SQL Database, see [Restore a database using PowerShell](scripts/restore-database-powershell.md).


  | Cmdlet | Description |
  | --- | --- |
  | [Get-AzSqlDatabase](/powershell/module/az.sql/get-azsqldatabase) |Gets one or more databases. |
  | [Get-AzSqlDeletedDatabaseBackup](/powershell/module/az.sql/get-azsqldeleteddatabasebackup) | Gets a deleted database that you can restore. |
  | [Restore-AzSqlDatabase](/powershell/module/az.sql/restore-azsqldatabase) |Restores a database. |

### [Rest API](#tab/rest-api)

To restore a database by using the REST API:

| API | Description |
| --- | --- |
| [REST (createMode=Recovery)](/rest/api/sql/databases) | Restores a database. |
| [Get Create or Update Database Status](/rest/api/sql/operations) |Returns the status during a restore operation. |

---

## Geo-restore

> [!IMPORTANT]
> - Geo-restore is available only for databases configured with geo-redundant [backup storage](automated-backups-overview.md#backup-storage-redundancy). If you are not currently using geo-replicated backups for a database, you can change this by [configuring backup storage redundancy](automated-backups-change-settings.md#configure-backup-storage-redundancy).
> - Geo-restore can only be performed on databases residing in the same subscription.

Geo-restore uses geo-replicated backups as the source. You can restore a database on any [logical server](logical-servers.md) in any Azure region from the most recent geo-replicated backups. You can request a geo-restore even if the database or the entire region is inaccessible due to an outage.

Geo-restore is the default recovery option when your database is unavailable because of an incident in the hosting region. You can restore the database to a server in any other region. There is a delay between when a backup is taken and when it is geo-replicated to an Azure blob in a different region. As a result, the restored database can be up to one hour behind the original database. The following illustration shows a database restore from the last available backup in another region.

:::image type="content" source="./media/recovery-using-backups/geo-restore-2.png" alt-text="Graphic of geo-restore":::


### [Azure portal](#tab/azure-portal)

From the Azure portal, you create a new single database and select an available geo-restore backup. The newly created database contains the geo-restored backup data.

To geo-restore a single database from the Azure portal in the region and server of your choice, follow these steps:

1. From **Dashboard**, select **Add** > **Create SQL Database**. On the **Basics** tab, enter the required information.
2. Select **Additional settings**.
3. For **Use existing data**, select **Backup**.
4. For **Backup**, select a backup from the list of available geo-restore backups.

    :::image type="content" source="./media/recovery-using-backups/geo-restore-azure-sql-database-list-annotated.png" alt-text="Screenshot of Create SQL Database options":::

Complete the process of creating a new database from the backup. When you create a database in Azure SQL Database, it contains the restored geo-restore backup.

### [Azure CLI](#tab/azure-cli)

To restore a database by using the Azure CLI, see [az sql db restore](/cli/azure/sql/db#az-sql-db-restore).

### [PowerShell](#tab/powershell)

For a PowerShell script that shows how to perform geo-restore for a single database, see [Use PowerShell to restore a single database to an earlier point in time](scripts/restore-database-powershell.md).

  | Cmdlet | Description |
  | --- | --- |
  | [Get-AzSqlDatabase](/powershell/module/az.sql/get-azsqldatabase) |Gets one or more databases. |
  | [Get-AzSqlDatabaseGeoBackup](/powershell/module/az.sql/get-azsqldatabasegeobackup) |Gets a geo-redundant backup of a database. |
  | [Restore-AzSqlDatabase](/powershell/module/az.sql/restore-azsqldatabase) |Restores a database. |

### [Rest API](#tab/rest-api)

To restore a database by using the REST API:

| API | Description |
| --- | --- |
| [REST (createMode=Recovery)](/rest/api/sql/databases) |Restores a database. |
| [Get Create or Update Database Status](/rest/api/sql/operations) |Returns the status during a restore operation. |

---




### Geo-restore considerations

For detailed information about using geo-restore to recover from an outage, see [Recover from an outage](disaster-recovery-guidance.md#recover-using-geo-restore).

Geo-restore is the most basic disaster-recovery solution available in SQL Database. It relies on automatically created geo-replicated backups with a recovery point objective (RPO) up to 1 hour and an estimated recovery time of up to 12 hours. It doesn't guarantee that the target region will have the capacity to restore your databases after a regional outage, because a sharp increase of demand is likely. If your application uses relatively small databases and is not critical to the business, geo-restore is an appropriate disaster-recovery solution. 

For business-critical applications that require large databases and must ensure business continuity, use [Auto-failover groups](auto-failover-group-sql-db.md). It offers a much lower RPO and recovery time objective, and the capacity is always guaranteed. 

For more information about business continuity choices, see [Overview of business continuity](business-continuity-high-availability-disaster-recover-hadr-overview.md).



## Next steps

- [Business continuity overview](business-continuity-high-availability-disaster-recover-hadr-overview.md)
- [SQL Database automated backups](automated-backups-overview.md)
- [Long-term retention](long-term-retention-overview.md)
- To learn about faster recovery options, see [Active geo-replication](active-geo-replication-overview.md) or [Auto-failover groups](auto-failover-group-sql-db.md).
