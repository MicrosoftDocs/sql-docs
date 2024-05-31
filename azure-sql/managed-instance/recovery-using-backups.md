---
title: Restore a database from a backup
titleSuffix: Azure SQL Managed Instance
description: Learn about point-in-time restore, which enables you to roll back a database in Azure SQL Managed Instance up to 35 days.
author: Stralle
ms.author: strrodic
ms.reviewer: wiassaf, mathoma, danil
ms.date: 12/27/2023
ms.service: sql-managed-instance
ms.subservice: backup-restore
ms.topic: how-to
ms.custom:
  - azure-sql-split
  - build-2024
monikerRange: "=azuresql||=azuresql-mi"
---
# Restore a database from a backup in Azure SQL Managed Instance
[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]


<!---
Some of the content in this article is duplicated in /azure-sql/database/recovery-using-backups.md. Any relevant changes made to this article should be made in the other article as well. 
--->


> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/recovery-using-backups.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](recovery-using-backups.md?view=azuresql-mi&preserve-view=true)

This article provides steps to recover a database from a backup in Azure SQL Managed Instance. For Azure SQL Database, see [Restore a database from a backup in Azure SQL Database](../database/recovery-using-backups.md).

## Overview

[Automated database backups](automated-backups-overview.md) help protect your databases from user and application errors, accidental database deletion, and prolonged outages. This built-in capability is available for all service tiers and compute sizes. The following options are available for database recovery through automated backups:

- Create a new database on the same managed instance, recovered to a specified point in time within the retention period.
- Create a new database on the same managed instance or a different managed instance, recovered to a specified point in time within the retention period.
- Create a database on the same managed instance or a different managed instance, recovered to the deletion time for a deleted database.
- Create a new database on any managed instance in same subscription or different subscription in same tenant and in the same region, recovered to the point of the most recent backups.

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
> To achieve a predictable time of database restores, consider configuring [maintenance windows](maintenance-window.md) that allow scheduling of system updates at a specific day and time. Also consider running database restores outside the scheduled maintenance window.

## Permissions

To recover by using automated backups, you must be either:

- A member of the SQL Server Contributor role or SQL Managed Instance Contributor role (depending on the recovery destination) in the subscription
- The subscription owner 

For more information, see [Azure RBAC: Built-in roles](/azure/role-based-access-control/built-in-roles). 

You can recover by using the Azure portal, PowerShell, or the REST API. You can't use Transact-SQL.

## Point-in-time restore

You can restore a database to an earlier point in time. The request can specify any service tier or compute size for the restored database. Ensure that you have sufficient resources on the instance to which you're restoring the database. 

When the restore is complete, it creates a new database on the target instance, whether it's the same instance, or a different instance.  The restored database is charged at normal rates, based on its service tier and compute size. You don't incur charges until the database restore is complete.

You generally restore a database to an earlier point for recovery purposes. You can treat the restored database as a replacement for the original database or use it as a data source to update the original database.

> [!IMPORTANT]
> You can't perform a point-in-time restore on a geo-secondary database. You can do so only on a primary database.

- **Database replacement**

  If you want the restored database to be a replacement for the original database, you should specify the original database's compute size and service tier. You can then rename the original database and give the restored database the original name by using the [ALTER DATABASE](/sql/t-sql/statements/alter-database-azure-sql-database) command in T-SQL.

- **Data recovery**

  If you plan to retrieve data from the restored database to recover from a user or application error, you need to write and run a data recovery script that extracts data from the restored database and applies to the original database. Although the restore operation might take a long time to complete, the restoring database is visible in the database list throughout the restore process. 
  
  If you delete the database during the restore, the restore operation will be canceled. You won't be charged for the database that didn't complete the restore.
  
### [Azure portal](#tab/azure-portal)

To recover a database in SQL Managed Instance to a point in time by using the Azure portal, you can go to the database in the portal and choose **Restore**. Alternatively, you can open the target SQL Managed Instance overview page, and select **+ New database** on the toolbar to open the **Create Azure SQL Managed Database** page. 

:::image type="content" source="media/point-in-time-restore/choose-database-to-restore.png" alt-text="Screenshot that shows the SQL Managed Instance overview pane in the Azure portal, with adding a new database selected." lightbox="media/point-in-time-restore/choose-database-to-restore.png":::

Provide target managed instance details on the **Basics** tab, and choose a type of backup from the **Data source** tab. 

:::image type="content" source="media/point-in-time-restore/database-data-source.png" alt-text="Screenshot of the Azure portal that shows the data source tab of the Create Azure SQL Managed Database page, with point-in-time restore selected." lightbox="media/point-in-time-restore/database-data-source.png" :::

For greater details, review the [Point in time restore](point-in-time-restore.md#restore-an-existing-database) article. 

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
> - Restore points represent a period between the earliest restore point and the latest log backup point. Information on the latest restore point is currently unavailable on Azure PowerShell.

To restore a database in SQL Managed Instance, see [Restore-AzSqlInstanceDatabase](/powershell/module/az.sql/restore-azsqlinstancedatabase).

---

## Deleted database restore

You can restore a deleted database to the deletion time, or an earlier point in time, to the same instance, or a different instance than the source instance. The target instance can be in the same subscription or in a different subscription than the source instance. You restore a deleted database by creating a new database from the backup.

> [!IMPORTANT]
> You can't restore a deleted managed instance. If you delete a managed instance, all its databases are also deleted and can't be restored to the deletion time, or an earlier point in time. If you configured [long-term retention (LTR)](../database/long-term-retention-overview.md), you can still restore a database from deleted instance to another instance and to point in time when LTR backup was taken.

### [Azure portal](#tab/azure-portal)

To recover a database by using the Azure portal, open the managed instance's overview page and select **Backups**. Choose to show **Deleted** backups, and then select **Restore** next to the deleted backup you want to recover to open the **Create Azure SQL Managed Database** page. Provide target managed instance details on the **Basics** tab, and source managed instance details on the **Data source** tab. Configure retention settings on the **Additional settings** tab. 

:::image type="content" source="media/point-in-time-restore/restore-deleted-sql-managed-instance-annotated.png" alt-text="Screenshot of the Azure portal, Backups page of the SQL Managed Instance, showing deleted databases and selecting the Restore action." lightbox="media/point-in-time-restore/restore-deleted-sql-managed-instance-annotated.png":::

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

Geo-restore is the default recovery option when your database is unavailable because of an incident in the hosting region. You can restore the database to an instance in any other region. You can restore a database on any managed instance in any Azure region from the most recent geo-replicated backups. Geo-restore uses a geo-replicated backup as its source. You can request a geo-restore even if an outage has made the database or datacenter inaccessible.

There's a delay between when a backup is taken and when it's geo-replicated to an Azure blob in a different region. As a result, the restored database can be up to one hour behind the original database. The following illustration shows a database restore from the last available backup in another region.

:::image type="content" source="../database/media/recovery-using-backups/geo-restore-2.png" alt-text="Illustration of restoring a database across regions for the purpose of geo-restore." lightbox="../database/media/recovery-using-backups/geo-restore-2.png":::

### [Azure portal](#tab/azure-portal)

From the Azure portal, you can restore a geo-replicated backup to an existing instance, or you create a new managed instance and select an available geo-restore backup. The newly created database contains the geo-restored backup data.

To restore to an existing instance, follow the steps in [Point-in-time restore](#point-in-time-restore), and be sure to choose the appropriate source and target instances to restore your database to your intended instance. 

To geo-restore to a new instance by using the Azure portal, follow these steps: 

1. Go to your new Azure SQL managed instance. 
1. Select **New database**.
1. Enter a database name. 
1. Under **Data source**, choose the appropriate type of backup, and then provide details for the data source. 
1. Select a backup from the list of available geo-restore backups. 

After you complete the process of creating an instance database, it will contain the restored geo-restore backup.

### [Azure CLI](#tab/azure-cli)

Geo-restore with the Azure CLI is currently unavailable. 

### [PowerShell](#tab/powershell)

For a PowerShell script that shows how to perform geo-restore for a database in SQL Managed Instance, see [Use PowerShell to restore a database to another geo-region](scripts/restore-geo-backup.md).

---

### Geo-restore considerations

Geo-restore is the most basic disaster-recovery solution available in Azure SQL Managed Instance. It relies on automatically created geo-replicated backups in a secondary (paired) region. Here're some considerations for geo-restore:

- Recovery point objective (RPO) is up to 1 hour.
- Restore processes (recovery time objective - RTO) usually take less than 12 hours but might vary based on database size and activity so restoration could extend beyond this time frame.
- Secondary (paired) region is Azure storage settings for the primary region. You can't change the secondary region.
- Newly created/restored databases might not immediately appear as restorable in other regions due to a lag in populating new data. If customers do not see the backups of new databases, they should anticipate a waiting period of up to 24 hours.

It's essential to acknowledge that geo-restore serves as an appropriate disaster-recovery solution for applications with relatively small databases that aren't critical to the business. For business-critical applications that require large databases and must ensure business continuity, use [failover groups](failover-group-sql-mi.md). That feature offers a much lower RPO and RTO, and the capacity is always guaranteed.

For more information about business continuity choices, see [Overview of business continuity](../database/business-continuity-high-availability-disaster-recover-hadr-overview.md).

## Limitations

Consider the following limitations when working with backups and Azure SQL Managed Instance: 

- Geo-restore of a database can only be performed to an instance in the same subscription as the source SQL managed instance. 
- Native backups taken on Azure SQL Managed Instance databases can only be [restored to SQL Server 2022](restore-database-to-sql-server.md) (either on-premises, or on a virtual machine) if the source SQL Managed Instance has enrolled in the [November 2022 feature wave](november-2022-feature-wave-enroll.md).
- Azure SQL Managed Instance databases are encrypted with TDE by default. When the source database uses a customer-managed key (CMK) as the TDE protector, to restore your database to an instance other than the source SQL Managed Instance, the target instance must have access to the same key used to encrypt the source database in Azure Key Vault, or you must disable TDE encryption on the source database before taking the backup.
- You can only track the progress of the restore process by using the [sys.dm_exec_requests](/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-requests-transact-sql) and [sys.dm_operation_status](/sql/relational-databases/system-dynamic-management-views/sys-dm-operation-status-azure-sql-database) dynamic management views.
- When [service endpoint policies](service-endpoint-policies-configure.md) are present on a subnet delegated to Azure SQL Managed Instance, point-in-time restore (PITR) to managed instances in that subnet cannot be performed from instances in different regions.
- Recovery point objective (RPO) is up to 1 hour.
- Recovery time objective (RTO) is approximately 12 hours, but might vary based on database size and activity could go beyond this timeframe.
- Secondary (paired) region can't be changed.
- Newly created/restored databases might not immediately appear as restorable in other regions due to a lag in populating new data. It can take up to 24 hours for backups of new database to become visible.
- The maximum number of databases you can restore in parallel is 200 per single subscription. In some cases, it's possible to increase this limit by opening a support ticket. 
- Database backups taken from instances configured with the SQL Server 2022 [update policy](update-policy.md) can be restored to instances configured with either the SQL Server 2022 or Always-up-to-date update policy. Database backups taken from instances configured with the Always-up-to-date update policy can only be restored to instances also configured with the Always-up-to-date update policy. 

## Related content

- [SQL Managed Instance automated backups](automated-backups-overview.md)
- [Long-term retention](../database/long-term-retention-overview.md)
- To learn about faster recovery options, see [Failover groups](failover-group-sql-mi.md).
