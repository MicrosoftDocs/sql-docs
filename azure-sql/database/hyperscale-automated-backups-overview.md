---
title: Automated backups for Hyperscale databases
titleSuffix: Azure SQL Database
description: Learn about automated backups for Hyperscale databases in Azure SQL Database.
author: SudhirRaparla
ms.author: nvraparl
ms.reviewer: wiassaf, mathoma, danil
ms.date: 09/08/2022
ms.service: sql-db-mi
ms.subservice: backup-restore
ms.topic: conceptual
ms.custom:
  - "references_regions"
  - "devx-track-azurepowershell"
  - "devx-track-azurecli"
  - "azure-sql-split"
monikerRange: "= azuresql || = azuresql-db"
---
# Automated backups for Hyperscale databases
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

This article explains the [automated backup](automated-backups-overview.md) feature with Hyperscale databases in Azure SQL Database. 

Hyperscale databases use a [unique architecture](service-tier-hyperscale.md#distributed-functions-architecture) with highly scalable storage and compute performance tiers. Hyperscale backups are snapshot based and are nearly instantaneous. Log backups are stored in long-term Azure storage for the backup retention period. 

A Hyperscale architecture doesn't require full, differential, or log backups. As such, backup frequency, storage costs, scheduling, storage redundancy, and restore capabilities differ from other databases in Azure SQL Database. 

## Backup and restore performance

Storage and compute separation enables Hyperscale to push down backup and restore operations to the storage layer to eliminate resource consumption on compute replicas. Database backups don't affect the performance of either primary or secondary compute replicas. 

Backup and restore operations for Hyperscale databases are fast regardless of data size, because they use storage snapshots. Backup is virtually instantaneous. 

You can restore a database to any point in time within its backup retention period by:

1. Reverting to applicable file snapshots.
1. Applying transaction logs to make the restored database transactionally consistent. 

As such, restore is not a size-of-data operation. Restore of a Hyperscale database within the same Azure region finishes in minutes instead of hours or days, even for multi-terabyte databases. 

Creation of new databases by restoring an existing backup or copying the database also takes advantage of compute and storage separation in Hyperscale. Creating copies for development or testing purposes, even of multi-terabyte databases, is doable in minutes within the same region when you use the same storage type.

## Backup retention

Default short-term retention of backups for Hyperscale databases is 7 days.

> [!NOTE]
> - Short-term retention of backups in the range of 1 to 35 days for Hyperscale databases is now in preview.
> - Long-term backup retention (LTR) capability for Hyperscale databases is now in preview.

## Backup scheduling

There are no traditional full, differential, and transaction log backups for Hyperscale databases. Instead, regular storage snapshots of data files are taken. 

The generated transaction logs are retained as is for the configured retention period. At restore time, relevant transaction log records are applied to the restored storage snapshot. The result is a transactionally consistent database without any data loss as of the specified point in time within the retention period. 

## Monitor backup storage consumption

In Hyperscale, Azure Monitor metrics report the following consumption information:

- Data backup storage size (snapshot backup size)
- Data storage size (allocated database size)
- Log backup storage size (transaction log backup size) 

To view backup and data storage metrics in the Azure portal, follow these steps:

1. Go to the Hyperscale database for which you want to monitor backup and data storage metrics.
2. In the **Monitoring** section, select the **Metrics** page.
3. From the **Metric** dropdown list, select the **Data backup storage**, **Data storage size**, and **Log backup storage** metrics with an appropriate aggregation rule. 

:::image type="content" source="./media/automated-backups-overview/hyperscale-backup-storage-metrics.png" alt-text="Screenshot of the Azure portal that shows selections for viewing Hyperscale backup storage consumption.":::

### Reduce backup storage consumption

Backup storage consumption for a Hyperscale database depends on the retention period, choice of region, backup storage redundancy, and workload type. Consider some of the following tuning techniques to reduce your backup storage consumption for a Hyperscale database:

- Reduce the [backup retention period](automated-backups-change-settings.md#change-short-term-retention-policy) to the minimum for your needs.
- Avoid doing large write operations, such as index maintenance, more frequently than you need to. For index maintenance recommendations, see [Optimize index maintenance to improve query performance and reduce resource consumption](/sql/relational-databases/indexes/reorganize-and-rebuild-indexes).
- For large data-load operations, consider using data compression when appropriate.
- Use the `tempdb` database instead of permanent tables in your application logic to store temporary results and/or transient data.
- Use locally redundant or zone-redundant backup storage when geo-restore capability is unnecessary (for example, dev/test environments). 

## Backup storage costs

Hyperscale backup storage cost depends on the choice of region and backup storage redundancy. It also depends on the workload type. 

Write-heavy workloads are more likely to change data pages frequently, which results in larger storage snapshots. Such workloads also generate more transaction logs, contributing to the overall backup costs. Backup storage is charged based on gigabytes consumed per month. For pricing details, see the [Azure SQL Database pricing](https://azure.microsoft.com/pricing/details/sql-database/single/) page. 

For Hyperscale, billable backup storage is calculated as follows: 

`Total billable backup storage size = (data backup storage size + log backup storage size)` 

Data storage size is not included in the billable backup because it's already billed as allocated database storage. 

Deleted Hyperscale databases incur backup costs to support recovery to a point in time before deletion. For a deleted Hyperscale database, billable backup storage is calculated as follows: 

`Total billable backup storage size for deleted Hyperscale database = (data storage size + data backup size + log backup storage size) * (remaining backup retention period after deletion / configured backup retention period)` 

Data storage size is included in the formula because allocated database storage is not billed separately for a deleted database. For a deleted database, data is stored after deletion to enable recovery during the configured backup retention period. 

Billable backup storage for a deleted database reduces gradually over time after it's deleted. It becomes zero when backups are no longer retained, and then recovery is no longer possible. If it's a permanent deletion and you no longer need backups, you can optimize costs by reducing retention before deleting the database.

### Monitor backup costs

To understand backup storage costs:

1. In the Azure portal, go to **Cost Management + Billing**.
1. Select **Cost Management** > **Cost analysis**. 
1. For **Scope**, select the desired subscription.
1. Filter for the time period and service you're interested in by following these steps: 

   1. Add a filter for **Service name**.
   1. Choose **sql-database** from the dropdown list.
   1. Add another filter for **Meter**. 
   1. To monitor backup costs for point-in-time recovery, select **Data Stored - Backup - RA** from the dropdown list.

The following screenshot shows an example cost analysis.    

:::image type="content" source="./media/hyperscale-automated-backups-overview/monitor-hyperscale-backup-costs.png" alt-text="Screenshot of the Azure portal that shows Hyperscale Backup storage costs.":::

## Data and backup storage redundancy

Hyperscale supports configurable storage redundancy. When you're creating a Hyperscale database, you can choose your preferred storage type:  read-access geo-zone-redundant storage (RA-GZRS), read-access geo-redundant storage (RA-GRS), zone-redundant storage (ZRS), or locally redundant storage (LRS). 

- **Geo-zone-redundant storage**: Copies your backups synchronously across three Azure availability zones in the primary region. similar to zone-redundant storage(ZRS).  In addition,copies your data asynchronously to a single physical location in the [paired](/azure/availability-zones/cross-region-replication-azure#azure-cross-region-replication-pairings-for-all-geographies) secondary region. It's currently available in only [certain regions](/azure/storage/common/storage-redundancy#geo-zone-redundant-storage).

To learn how the backups are replicated for other storage types, see [backup storage redundancy](automated-backups-overview.md#backup-storage-redundancy)

Since Hyperscale uses storage snapshots for backups, data and backups share the same storage account. As a result the selected backup storage redundancy is applicable for both data and backups. 

Consider backup storage redundancy carefully when you create a Hyperscale database, because you can set it only during database creation. You can't modify this setting after the resource is provisioned. 

Use [active geo-replication](active-geo-replication-overview.md) to update backup storage redundancy settings for an existing Hyperscale database with minimum downtime. Alternatively, you can use [database copy](database-copy.md).

> [!WARNING]
> - [Geo-restore](recovery-using-backups.md#geo-restore) is disabled as soon as a database is updated to use locally redundant or zone-redundant storage. 
> - Zone-redundant storage is currently available in only [certain regions](/azure/storage/common/storage-redundancy#zone-redundant-storage). 
> - Geo-zone-redundant storage is currently available in only [certain regions](/azure/storage/common/storage-redundancy#geo-zone-redundant-storage). 

## Restore a Hyperscale database to a different region

You might need to restore your Hyperscale database to a region that's different from the current region. Common reasons include a disaster recovery operation or drill, or a relocation. The primary method is to do a geo-restore of the database. You use the same steps that you would use to restore any other database in Azure SQL Database to a different region:

1. Create a [server](logical-servers.md) in the target region if you don't already have an appropriate server there. This server should be owned by the same subscription as the original (source) server.
2. Follow the instructions in the [geo-restore](./recovery-using-backups.md#geo-restore) section of the page on restoring a database in Azure SQL Database from automatic backups.

> [!NOTE]
> Because the source and target are in separate regions, the database can't share snapshot storage with the source database as it does in non-geo restores. Non-geo restores finish quickly regardless of database size. 
>
> A geo-restore of a Hyperscale database is a size-of-data operation, even if the target is in the paired region of the geo-replicated storage. Therefore, a geo-restore will take a significantly longer time compared to a point-in-time restore in the same region.
>
> If the target is in the paired region, data transfer will be within a region. That transfer will be significantly faster than a cross-region data transfer. But it will still be a size-of-data operation.

If you prefer, you can copy the database to a different region. Use this method if geo-restore is not available because it's not supported with the selected storage redundancy type. For details, see [Database copy for Hyperscale](database-copy.md#database-copy-for-azure-sql-hyperscale).

## Next steps

- Database backups are an essential part of any business continuity and disaster recovery strategy because they help protect your data from accidental corruption or deletion. To learn about the other SQL Database business continuity solutions, see [Business continuity overview](business-continuity-high-availability-disaster-recover-hadr-overview.md).
- For information about how to configure, manage, and restore from long-term retention of automated backups in Azure Blob Storage by using the Azure portal, see [Manage long-term backup retention by using the Azure portal](long-term-backup-retention-configure.md).
- For information about how to configure, manage, and restore from long-term retention of automated backups in Azure Blob Storage by using PowerShell, see [Manage long-term backup retention by using PowerShell](long-term-backup-retention-configure.md). 
- Get more information about how to [restore a database to a point in time by using the Azure portal](recovery-using-backups.md).
- Get more information about how to [restore a database to a point in time by using PowerShell](scripts/restore-database-powershell.md).

