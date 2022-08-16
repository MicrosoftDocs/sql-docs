---
title: Restore a database from a backup
titleSuffix: Azure SQL Managed Instance
description: Learn about point-in-time restore, which enables you to roll back a database in Azure SQL Managed Instance up to 35 days.
services:
  - "sql-database"
ms.service: sql-managed-instance
ms.subservice: backup-restore
ms.topic: how-to
ms.custom: "azure-sql-split"
author: MilanMSFT
ms.author: mlazic
ms.reviewer: wiassaf, mathoma, danil
ms.date: 07/20/2022
monikerRange: "= azuresql ||  = azuresql-mi"
---
# Restore a database from a backup in Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]


<!---
Some of the content in this article is duplicated in /azure-sql/database/recovery-using-backups.md. Any relevant changes made to this article should be made in the other article as well. 
--->


> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/recovery-using-backups.md)
> * [Azure SQL Managed Instance](recovery-using-backups.md)

This article provides steps to recover a database from a backup in Azure SQL Managed Instance. For Azure SQL Database, see [Restore a database from a backup in Azure SQL Database](../database/recovery-using-backups.md).

[Automated database backups](automated-backups-overview.md) help protect your databases from user and application errors, accidental database deletion, and prolonged outages. This built-in capability is available for all service tiers and compute sizes. The following options are available for database recovery through automated backups:

- Create a new database on the same managed instance, recovered to a specified point in time within the retention period.
- Create a database on the same managed instance, recovered to the deletion time for a deleted database.
- Create a new database on any managed instance in the same region, recovered to the point of the most recent backups.

Cross-region and cross-subscription point-in-time restore for SQL Managed Instance isn't currently supported. 

If you configured [long-term retention (LTR)](../database/long-term-retention-overview.md), you can also create a new database from any long-term retention backup on any instance.

> [!IMPORTANT]
> You can't overwrite an existing database during restore.

## Recovery time

Several factors affect the recovery time to restore a database through automated database backups:

- The size of the database
- The compute size of the database
- The number of transaction logs involved
- The amount of activity that needs to be replayed to recover to the restore point
- The network bandwidth if the restore is to a different region
- The number of concurrent restore requests that are processed in the target region

For a large or very active database, the restore might take several hours. A prolonged outage in a region might cause a high number of geo-restore requests for disaster recovery. When there are many requests, the recovery time for individual databases can increase. Most database restores finish in less than 12 hours.

> [!TIP]
> For Azure SQL Managed Instance, system updates take precedence over database restores in progress. If there's a system update for SQL Managed Instance, all pending restores are suspended and then resumed after the update has been applied. This system behavior might prolong the time of restores and might be especially impactful to long-running restores. 
>
> To achieve a predictable time of database restores, consider configuring [maintenance windows](../database/maintenance-window.md) that allow scheduling of system updates at a specific day and time. Also consider running database restores outside the scheduled maintenance window.

## Permissions 

To recover by using automated backups, you must be either:

- A member of the SQL Server Contributor role or SQL Managed Instance Contributor role (depending on the recovery destination) in the subscription
- The subscription owner 

For more information, see [Azure RBAC: Built-in roles](/azure/role-based-access-control/built-in-roles). 

You can recover by using the Azure portal, PowerShell, or the REST API. You can't use Transact-SQL.

## Point-in-time restore

You can restore a database to an earlier point in time. The request can specify any service tier or compute size for the restored database. Ensure that you have sufficient resources on the instance to which you're restoring the database. 

When the restore is complete, it creates a new database on the same instance as the original database. The restored database is charged at normal rates, based on its service tier and compute size. You don't incur charges until the database restore is complete.

You generally restore a database to an earlier point for recovery purposes. You can treat the restored database as a replacement for the original database or use it as a data source to update the original database.

> [!IMPORTANT]
> You can't perform a point-in-time restore on a geo-secondary database. You can do so only on a primary database.

- **Database replacement**

  If you want the restored database to be a replacement for the original database, you should specify the original database's compute size and service tier. You can then rename the original database and give the restored database the original name by using the [ALTER DATABASE](/sql/t-sql/statements/alter-database-azure-sql-database) command in T-SQL.

- **Data recovery**

  If you plan to retrieve data from the restored database to recover from a user or application error, you need to write and run a data recovery script that extracts data from the restored database and applies to the original database. Although the restore operation might take a long time to complete, the restoring database is visible in the database list throughout the restore process. 
  
  If you delete the database during the restore, the restore operation will be canceled. You won't be charged for the database that did not complete the restore.
  
### [Azure portal](#tab/azure-portal)

To recover a database in SQL Managed Instance to a point in time by using the Azure portal, open the database overview page, and select **Restore** on the toolbar. Choose the point-in-time backup point from which a new database will be created.

:::image type="content" source="../database/media/recovery-using-backups/pitr-backup-managed-instance-annotated.png" alt-text="Screenshot of database restore options for SQL Managed Instance.":::

### [Azure CLI](#tab/azure-cli)

To restore a database in SQL Managed Instance by using the Azure CLI, see [az sql midb restore](/cli/azure/sql/midb#az-sql-midb-restore).

### [PowerShell](#tab/powershell)

[!INCLUDE [updated-for-az](../includes/updated-for-az.md)]

To restore a database in SQL Managed Instance by using PowerShell, use the following cmdlets:

| Cmdlet | Description |
| --- | --- |
| [Get-AzSqlInstance](/powershell/module/az.sql/get-azsqlinstance) |Gets one or more managed instances. |
| [Get-AzSqlInstanceDatabase](/powershell/module/az.sql/get-azsqlinstancedatabase) | Gets an instance database. |
| [Restore-AzSqlInstanceDatabase](/powershell/module/az.sql/restore-azsqlinstancedatabase) |Restores an instance database. |

> [!IMPORTANT]
> - SQL Managed Instance still supports the PowerShell Azure Resource Manager module, but all future development is for the Az.Sql module. For these cmdlets, see [AzureRM.Sql](/powershell/module/AzureRM.Sql/). Arguments for the commands in the Az module and in Azure Resource Manager modules are largely identical.
> 
> - Restore points represent a period between the earliest restore point and the latest log backup point. Information on the latest restore point is currently unavailable on Azure PowerShell.

To restore a database in SQL Managed Instance, see [Restore-AzSqlInstanceDatabase](/powershell/module/az.sql/restore-azsqlinstancedatabase).

---

## Deleted database restore

You can restore a deleted database to the deletion time, or an earlier point in time, on the same managed instance. You restore a deleted database by creating a new database from the backup.

> [!IMPORTANT]
> If you delete a managed instance, all its databases are also deleted and can't be recovered. You can't restore a deleted managed instance.

### [Azure portal](#tab/azure-portal)

To recover a database by using the Azure portal, open the managed instance's overview page and select **Deleted databases**. Select a deleted database that you want to restore. Then enter the name for the new database that will be created with data restored from the backup.

:::image type="content" source="../database/media/recovery-using-backups/restore-deleted-sql-managed-instance-annotated.png" alt-text="Screenshot of selections for restoring a deleted database in Azure SQL Managed Instance.":::

> [!TIP]
> It might take several minutes for recently deleted databases to appear on the **Deleted databases** page in the Azure portal, or when you want to display deleted databases by using the command line. 

### [Azure CLI](#tab/azure-cli)

To restore a database in SQL Managed Instance by using the Azure CLI, see [az sql midb restore](/cli/azure/sql/midb#az-sql-midb-restore).

### [PowerShell](#tab/powershell)

For a sample PowerShell script that shows how to restore a deleted instance database, see [Restore a deleted instance database by using PowerShell](point-in-time-restore.md#restore-a-deleted-database).

---

## Geo-restore

> [!IMPORTANT]
> - Geo-restore is available only for managed instances configured with geo-redundant [backup storage](automated-backups-overview.md#backup-storage-redundancy). If you're not currently using geo-replicated backups for a database, you can change this by [configuring backup storage redundancy](automated-backups-change-settings.md#configure-backup-storage-redundancy).
> - You can perform geo-restore on managed instances that reside in the same subscription only.

You can restore a database on any managed instance in any Azure region from the most recent geo-replicated backups. Geo-restore uses a geo-replicated backup as its source. You can request a geo-restore even if an outage has made the database or datacenter inaccessible.

Geo-restore is the default recovery option when your database is unavailable because of an incident in the hosting region. You can restore the database to a server in any other region. 

There's a delay between when a backup is taken and when it's geo-replicated to an Azure blob in a different region. As a result, the restored database can be up to one hour behind the original database. The following illustration shows a database restore from the last available backup in another region.

:::image type="content" source="../database/media/recovery-using-backups/geo-restore-2.png" alt-text="Illustration of restoring a database across regions for the purpose of geo-restore.":::

### [Azure portal](#tab/azure-portal)

From the Azure portal, you create a new managed instance and select an available geo-restore backup. The newly created database contains the geo-restored backup data.

To geo-restore a database from the Azure portal to an existing managed instance in a region of your choice, select the managed instance. Then follow these steps:

1. Select **New database**.
2. Enter a database name.
3. Under **Use existing data**, select **Backup**.
4. Select a backup from the list of available geo-restore backups.

:::image type="content" source="../database/media/recovery-using-backups/geo-restore-sql-managed-instance-list-annotated.png" alt-text="Screenshot of the Azure portal that shows options to create a database.":::

After you complete the process of creating an instance database, it will contain the restored geo-restore backup.

### [Azure CLI](#tab/azure-cli)

To restore a database in SQL Managed Instance by using the Azure CLI, see [az sql midb restore](/cli/azure/sql/midb#az-sql-midb-restore).

### [PowerShell](#tab/powershell)

For a PowerShell script that shows how to perform geo-restore for a database in SQL Managed Instance, see [Use PowerShell to restore a database to another geo-region](scripts/restore-geo-backup.md).

---

### Geo-restore considerations

For detailed information about using geo-restore to recover from an outage, see [Recover from an outage](../database/disaster-recovery-guidance.md#recover-using-geo-restore).

Geo-restore is the most basic disaster-recovery solution available in SQL Managed Instance. It relies on automatically created geo-replicated backups with a recovery point objective (RPO) of up to 1 hour and an estimated recovery time objective (RTO) of up to 12 hours. It doesn't guarantee that the target region will have the capacity to restore your databases after a regional outage, because a sharp increase of demand is likely. If your application uses relatively small databases and is not critical to the business, geo-restore is an appropriate disaster-recovery solution. 

For business-critical applications that require large databases and must ensure business continuity, use [auto-failover groups](auto-failover-group-sql-mi.md). That feature offers a much lower RPO and RTO, and the capacity is always guaranteed. 

For more information about business continuity choices, see [Overview of business continuity](../database/business-continuity-high-availability-disaster-recover-hadr-overview.md).

## Next steps

- [SQL Managed Instance automated backups](automated-backups-overview.md)
- [Long-term retention](../database/long-term-retention-overview.md)
- To learn about faster recovery options, see [Auto-failover groups](auto-failover-group-sql-mi.md).
