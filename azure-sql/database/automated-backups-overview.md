---
title: Automatic, Geo-Redundant Backups
titleSuffix: Azure SQL Database
description: Learn how Azure SQL Database automatically backs up all databases and provides a point-in-time restore capability.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma, danil, dinethi
ms.date: 07/13/2026
ms.service: azure-sql-database
ms.subservice: backup-restore
ms.topic: concept-article
ms.custom:
  - azure-sql-split
  - ignite-2025
monikerRange: "=azuresql || =azuresql-db"
---
# Automated backups in Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](automated-backups-overview.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/automated-backups-overview.md?view=azuresql-mi&preserve-view=true)

<!---
Some of the content in this article is duplicated in /azure-sql/managed-instance/automated-backups-overview.md. Any relevant changes made to this article should be made in the other article as well. 
--->

This article describes the automated backup feature for Azure SQL Database.  

To change backup settings, see [Change settings](automated-backups-change-settings.md). To restore a backup, see [Recover using automated database backups](recovery-using-backups.md). 

## What is a database backup?

Database backups are an essential part of any business continuity and disaster recovery strategy, because they help protect your data from corruption or deletion. These backups enable database restore to a point in time within the configured retention period. If your data protection rules require that your backups are available for an extended time (up to 10 years), you can configure [long-term retention (LTR)](long-term-retention-overview.md) for both single and pooled databases.

For service tiers other than Hyperscale, Azure SQL Database uses SQL Server engine technology to back up and restore data. Hyperscale databases use backup and restore based on [storage snapshots](hyperscale-architecture.md#azure-storage). With traditional SQL Server backup technology, larger databases have long backup and restore times. By using snapshots, Hyperscale provides instant backup and fast restore capabilities regardless of database size. To learn more, see [Hyperscale backups](hyperscale-automated-backups-overview.md). 


## Backup frequency

Azure SQL Database creates:

- [Full backups](/sql/relational-databases/backup-restore/full-database-backups-sql-server) every week.
- [Differential backups](/sql/relational-databases/backup-restore/differential-backups-sql-server) every 12 or 24 hours.
- [Transaction log backups](/sql/relational-databases/backup-restore/transaction-log-backups-sql-server) approximately every 10 minutes. 

The exact frequency of transaction log backups depends on the compute size and the amount of database activity. When you restore a database, Azure SQL Database determines which full, differential, and transaction log backups to restore.

The Hyperscale architecture doesn't require full, differential, or log backups. To learn more, see [Hyperscale backups](hyperscale-automated-backups-overview.md). 

## Backup storage redundancy

The storage redundancy mechanism stores multiple copies of your data so it's protected from planned and unplanned events. These events might include transient hardware failure, network or power outages, or massive natural disasters. 

By default, new databases in Azure SQL Database store backups in geo-redundant [storage blobs](/azure/storage/common/storage-redundancy) that are replicated to a [paired region](/azure/reliability/cross-region-replication-azure). Geo-redundancy helps protect against outages that affect backup storage in the primary region. It also allows you to restore your databases in a different region in the event of a regional outage. 

The Azure portal provides a **Workload environment** option that helps to preset some configuration settings. You can override these settings. This option applies to the **Create SQL Database** portal page only.

- Choosing the **development** workload environment sets the **Backup storage redundancy** option to use locally redundant storage. Locally redundant storage incurs less cost and is appropriate for preproduction environments that don't require the redundance of zone- or geo-replicated storage. 
- Choosing the **Production** workload environment sets the **Backup storage redundancy** to geo-redundant storage, the default. 
- The **Workload environment** option also changes the initial setting for compute, though you can override this setting. Otherwise, the **Workload environment** option has no impact on licensing or other database configuration settings. 

To ensure that your backups stay within the same region where your database is deployed, change backup storage redundancy from the default geo-redundant storage to other types of storage that keep your data within the region. The configured backup storage redundancy is applied to both short-term retention (STR) backups and LTR backups. To learn more about storage redundancy, see [Data redundancy](/azure/storage/common/storage-redundancy). 

You can configure backup storage redundancy when you create your database, and you can update it later. Changes that you make to an existing database apply to future backups only. After you update the backup storage redundancy of an existing database, the changes might take up to 48 hours to be applied. 

You can choose one of the following storage redundancies for backups:

- **Locally redundant storage (LRS)**:  Copies your backups synchronously three times within a single physical location in the primary region. LRS is the least expensive storage option, but it's not recommended for applications that require resiliency to regional outages or a guarantee of high data durability.

   :::image type="content" source="media/automated-backups-overview/multi-paired-lrs.svg" alt-text="Diagram showing the locally redundant storage (LRS) option.":::

- **Zone-redundant storage (ZRS)**: Copies your backups synchronously across three Azure availability zones in the primary region. It's currently available in only [certain regions](/azure/storage/common/storage-redundancy#zone-redundant-storage). 

   :::image type="content" source="media/automated-backups-overview/multi-paired-zrs.svg" alt-text="Diagram showing the zone-redundant storage (ZRS) option.":::

- **Geo-redundant storage (GRS)**: Copies your backups synchronously three times within a single physical location in the primary region by using LRS. Then it copies your data asynchronously three times to a single physical location in the [paired secondary region](/azure/reliability/cross-region-replication-azure#azure-cross-region-replication-pairings-for-all-geographies). 

  The result is:
  
  - Three synchronous copies in the primary region.
  - Three synchronous copies in the paired region that were copied over from the primary region to the secondary region asynchronously. 

   :::image type="content" source="media/automated-backups-overview/multi-paired-grs.svg" alt-text="Diagram showing the geo-redundant storage (GRS) option.":::

- **Geo-Zone redundant storage (GZRS)**: Geo-zone-redundant storage (GZRS) combines the high availability provided by redundancy across availability zones (ZRS) with protection from regional outages provided by geo-replication (GRS). In GZRS, Azure copies your backups synchronously across three Azure availability zones in the primary region, and asynchronously three times to a single physical location in the [paired secondary region](/azure/reliability/cross-region-replication-azure#azure-cross-region-replication-pairings-for-all-geographies).

   Microsoft recommends using GZRS for applications requiring maximum consistency, durability, and availability, excellent performance, and resilience for disaster recovery.

   The result is:
  
  - Three synchronous copies across Availability Zones, in the primary region.
  - Three synchronous copies in the paired region, asynchronously copied over from the primary region to the secondary region. 

    The following diagram shows how your data is replicated with GZRS or RA-GZRS:

   :::image type="content" source="media/automated-backups-overview/multi-paired-gzrs.png" alt-text="Diagram showing the geo-zone redundant storage (GZRS) option.":::

> [!WARNING]
> - [Geo-restore](recovery-using-backups.md#geo-restore) is disabled as soon as a database is updated to use locally redundant or zone-redundant storage. 
> - The storage redundancy diagrams all show regions with multiple availability zones (multi-az). However, some regions provide only a single availability zone and don't support ZRS. 
> - You can set backup storage redundancy for Hyperscale databases only during creation. You can't modify this setting after the resource is provisioned. To update backup storage redundancy settings for an existing Hyperscale database with minimum downtime, use [active geo-replication](active-geo-replication-overview.md). Alternatively, you can use [database copy](database-copy.md). Learn more in [Hyperscale backups and storage redundancy](hyperscale-automated-backups-overview.md#data-and-backup-storage-redundancy).

## Backup usage

Use automatically created backups in the following scenarios:

- [Restore an existing database to a point in time](recovery-using-backups.md#point-in-time-restore) within the retention period by using the Azure portal, Azure PowerShell, the Azure CLI, or the REST API. This operation creates a new database on the same server as the original database, but it uses a different name to avoid overwriting the original database. 

  After restore finishes, you can optionally delete the original database and rename the restored database to the original database name. Alternatively, instead of deleting the original database, you can [rename](/sql/relational-databases/databases/rename-a-database) it, and then rename the restored database to the original database name. 
- [Restore a deleted database to a point in time](recovery-using-backups.md#restore-deleted-database) within the retention period, including the time of deletion. You can only restore the deleted database on the same server where you created the original database. Before you delete a database, Azure SQL Database takes a final transaction log backup to prevent any data loss.
- [Restore a database to another geographic region](recovery-using-backups.md#geo-restore). Geo-restore helps you recover from a regional outage when you can't access your database or backups in the primary region. It creates a new database on any existing server in any Azure region.
   > [!IMPORTANT]
   > Geo-restore is available only for databases that are configured with geo-redundant backup storage. If you're not currently using geo-replicated backups for a database, you can change this setting by [configuring backup storage redundancy](automated-backups-change-settings.md#configure-backup-storage-redundancy).
- [Restore a database from a specific long-term backup](long-term-retention-overview.md) of a single or pooled database, if the database is configured with an LTR policy. LTR allows you to [restore an older version of the database](long-term-backup-retention-configure.md) by using the Azure portal, the Azure CLI, or Azure PowerShell to satisfy a compliance request or to run an older version of the application. For more information, see [Long-term retention](long-term-retention-overview.md).

> [!WARNING]
> When restoring a database and the source backup storage redundancy is configured as Geo-Zone Redundant Storage (GZRS), the new database inherits the source backup storage configuration if you don't explicitly specify the backup storage redundancy configuration. This inheritance applies to any restore operation, such as point-in-time restore, database copy, geo-restore, and restore from a long-term backup.
> During this operation, if the target Azure region doesn't support the specific backup storage redundancy, the restore operation fails with an appropriate error message. You can mitigate this error by explicitly specifying the available storage options for the region.

## Automatic backups on secondary replicas

The [Business Critical](service-tiers-sql-database-vcore.md#business-critical) service tier takes automatic backups from a secondary replica. Since data is replicated between SQL Server processes on each node, the backup service takes the backup from the non-readable secondary replicas. This design ensures the primary replica remains dedicated to your main workload, and the readable secondary replica is dedicated to read-only workloads. Automatic backups in the Business Critical service tier are taken from a secondary replica most of the time. If an automatic backup fails on a secondary replica, the backup service takes the backup from the primary replica. 

Automatic backups on secondary replicas: 

- Are enabled by default. 
- Are included at no extra cost beyond the price of the service tier.
- Bring improved performance and predictability to the Business Critical service tier.

> [!NOTE]
> Create a Microsoft support ticket to disable the feature for your instance. 

## <a id="restore-capabilities"></a> Restore capabilities and features

This table summarizes the capabilities and features of [point-in-time restore (PITR)](recovery-using-backups.md#point-in-time-restore), [geo-restore](recovery-using-backups.md#geo-restore), and [long-term retention](long-term-retention-overview.md).

For information on recovery times, see [RTO and RPO](business-continuity-high-availability-disaster-recover-hadr-overview.md?view=azuresql-db&preserve-view=true#rto-and-rpo).

| Backup property | PITR | Geo-restore | LTR |
|---|---|---|---|
| **Types of SQL backup** | Full, differential, log. | Most recent geo-replicated copies of PITR backups. | Only the full backups. |
| **Retention** | 7 days by default, configurable between 1 and 35 days (except Basic databases, which are configurable between 1 and 7 days). | Enabled by default, same as source.<sup>2</sup>| Not enabled by default. Retention is up to 10 years. |
| **Azure Storage**  | Geo-redundant by default. You can optionally configure zone-redundant or locally redundant storage. | Available when PITR backup storage redundancy is set to geo-redundant or geo-zone redundant (GZRS). Not available when PITR backup storage is zone-redundant or locally redundant. | Geo-redundant by default. You can configure zone-redundant or locally redundant storage. |
| **Configure backups as [immutable](/azure/storage/blobs/immutable-storage-overview)** | Not supported | Not supported | [Supported](backup-immutability.md) | 
| **Restoring a new database in the same region** | Supported | Supported | Supported |
| **Restoring a new database in another region** | Not supported | Supported in any Azure region | Supported in any Azure region |
| **Restoring a new database in another subscription** |  Not supported  |  Not supported<sup>3</sup> | Not supported<sup>3</sup> |
| **Restoring via Azure portal**|Yes|Yes|Yes|
| **Restoring via PowerShell** |Yes|Yes|Yes|
| **Restoring via Azure CLI** |Yes|Yes|Yes|


<sup>1</sup> For business-critical applications that require large databases and must ensure business continuity, use [failover groups](failover-group-sql-db.md).   
<sup>2</sup> All PITR backups are stored on geo-redundant storage by default, so geo-restore is enabled by default.   
<sup>3</sup> The workaround is to restore to a new server and use Resource Move to move the server to another subscription, or use a [cross-subscription database copy](database-copy.md#copy-to-a-different-subscription).   

## Restore a database from backup

For more information about restoring a database, see [Restore a database from backups](recovery-using-backups.md). To explore backup configuration and restore operations, use the following examples.

| Operation | Azure portal | Azure CLI | Azure PowerShell |
|---|---|---|---|
| **Change backup retention** | [SQL Database](automated-backups-change-settings.md?tabs=azure-portal#change-short-term-retention-policy) <br/> [SQL Managed Instance](../managed-instance/automated-backups-change-settings.md?tabs=azure-portal#change-short-term-retention-policy) | [SQL Database](automated-backups-change-settings.md?tabs=azure-cli#change-short-term-retention-policy) <br/> [SQL Managed Instance](../managed-instance/automated-backups-change-settings.md?tabs=azure-cli#change-short-term-retention-policy) | [SQL Database](automated-backups-change-settings.md?tabs=powershell#change-short-term-retention-policy) <br/>[SQL Managed Instance](../managed-instance/automated-backups-change-settings.md?tabs=powershell#change-short-term-retention-policy)|
| **Change long-term backup retention** | [SQL Database](long-term-backup-retention-configure.md#create-long-term-retention-policies)<br/> [SQL Managed Instance](../managed-instance/long-term-backup-retention-configure.md) | [SQL Database](long-term-backup-retention-configure.md) <br/> [SQL Managed Instance](../managed-instance/long-term-backup-retention-configure.md) | [SQL Database](long-term-backup-retention-configure.md)<br/>[SQL Managed Instance](../managed-instance/long-term-backup-retention-configure.md)  |
| **Restore a database from a point in time** | [SQL Database](recovery-using-backups.md#point-in-time-restore)<br>[SQL Managed Instance](../managed-instance/point-in-time-restore.md) | [SQL Database](/cli/azure/sql/db#az-sql-db-restore) <br/> [SQL Managed Instance](/cli/azure/sql/midb#az-sql-midb-restore) | [SQL Database](/powershell/module/az.sql/restore-azsqldatabase) <br/> [SQL Managed Instance](/powershell/module/az.sql/restore-azsqlinstancedatabase) |
| **Restore a deleted database** | [SQL Database](recovery-using-backups.md)<br>[SQL Managed Instance](../managed-instance/point-in-time-restore.md#restore-a-deleted-database) | [SQL Database](long-term-backup-retention-configure.md#restore-from-ltr-backups) <br/> [SQL Managed Instance](../managed-instance/long-term-backup-retention-configure.md#restore-from-ltr-backups) | [SQL Database](/powershell/module/az.sql/get-azsqldeleteddatabasebackup) <br/> [SQL Managed Instance](/powershell/module/az.sql/get-azsqldeletedinstancedatabasebackup)|


[!INCLUDE [hyperscale-cross-tier-restore-note](../includes/hyperscale-cross-tier-restore-note.md)]

## Export a database

You can't download or directly access automatic backups taken by the Azure service. Azure only uses these backups for restore operations.

To export an Azure SQL Database, consider other alternatives. 

- When you need to export a database for archiving or for moving to another platform, [export the database schema and data](database-export.md) to a [BACPAC](/sql/relational-databases/data-tier-applications/data-tier-applications#bacpac) file. A BACPAC file is a ZIP file with an extension of BACPAC containing the metadata and data from the database. You can store a BACPAC file in Azure Blob storage or in local storage in an on-premises location. Later, you can import it back into [Azure SQL Database](sql-database-paas-overview.md), [Azure SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md), or a [SQL Server instance](/sql/database-engine/sql-server-database-engine-overview).
- You can also [Import or export an Azure SQL Database using private link](database-import-export-private-link.md) or [Import or export an Azure SQL Database without allowing Azure services to access the server](database-import-export-azure-services-off.md).

## Backup scheduling

The first full backup is scheduled right after you create or restore a new database. This backup usually finishes within 30 minutes, but it can take longer when the database is large. For example, the initial backup can take longer on a restored database or a database copy.   

After the first full backup, Azure automatically schedules and manages all further backups. The SQL Database service determines the exact timing of all database backups as it balances the overall system workload. You can't change the schedule of backup jobs or disable them.  

> [!IMPORTANT]
> - For a new, restored, or copied database, the point-in-time restore capability becomes available when the initial transaction log backup that follows the initial full backup is created.
> - Hyperscale databases are protected immediately after creation, unlike other databases where the initial backup takes time. The protection is immediate even if the Hyperscale database was created with a large amount of data through copy or restore. For more information, see [Hyperscale automated backups](hyperscale-automated-backups-overview.md). 

## Backup storage consumption

With SQL Server backup and restore technology, restoring a database to a point in time requires an uninterrupted backup chain. That chain consists of one full backup, optionally one differential backup, and one or more transaction log backups. 

Azure SQL Database schedules one full backup every week. To provide PITR within the entire retention period, Azure must store additional full, differential, and transaction log backups for up to a week longer than the configured retention period. 

In other words, for any point in time during the retention period, there must be a full backup that's older than the oldest time of the retention period. There must also be an uninterrupted chain of differential and transaction log backups from that full backup until the next full backup. 

Hyperscale databases use a different backup scheduling mechanism. For more information, see [Hyperscale backup scheduling](hyperscale-automated-backups-overview.md#backup-scheduling). 

Azure automatically deletes backups that are no longer needed to provide PITR functionality. Because differential backups and log backups require an earlier full backup to be restorable, Azure purges all three backup types together in weekly sets.

For all databases, including [TDE-encrypted](../database/transparent-data-encryption-tde-overview.md) databases, Azure compresses all full and differential backups to reduce backup storage compression and costs. The average backup compression ratio is three to four times. However, it can be lower or higher depending on the nature of the data and whether data compression is used in the database.

> [!IMPORTANT]
> For TDE-encrypted databases, Azure doesn't compress log backup files for performance reasons. For non-TDE-encrypted databases, log backups are compressed.

Azure SQL Database computes your total used backup storage as a cumulative value. Every hour, Azure reports this value to the billing pipeline. The pipeline is responsible for aggregating this hourly usage to calculate your consumption at the end of each month. After you delete a database, consumption decreases as backups age out and are deleted. After all backups are deleted and PITR is no longer possible, billing stops.
   
> [!IMPORTANT]
> Azure retains backups of a database to provide PITR even if you delete the database. Although deleting and recreating a database might save storage and compute costs, it might increase backup storage costs. The reason is that Azure retains backups for each deleted database, every time you delete it.  

### Monitor consumption

For vCore databases in Azure SQL Database, the database monitoring pane reports the storage that each type of backup (full, differential, and log) consumes as a separate metric. The following screenshot shows how to monitor the backup storage consumption for a single database. 

:::image type="content" source="media/automated-backups-overview/backup-metrics.png" alt-text="Screenshot that shows selections for monitoring database backup consumption in the Azure portal." lightbox="media/automated-backups-overview/backup-metrics.png":::

For instructions on how to monitor consumption in Hyperscale, see [Monitor Hyperscale backup consumption](hyperscale-automated-backups-overview.md#monitor-backup-storage-consumption).

### Fine-tune backup storage consumption

You aren't charged for backup storage consumption up to the maximum data size for a database. To reduce your backup storage consumption, consider some of the following tuning techniques:

- Reduce the [backup retention period](automated-backups-change-settings.md#change-short-term-retention-policy) to the minimum for your needs.
- Avoid doing large write operations, like index rebuilds, more often than you need to.
- For large data load operations, consider using [clustered columnstore indexes](/sql/relational-databases/indexes/columnstore-indexes-overview) and following related [best practices](/sql/relational-databases/indexes/columnstore-indexes-data-loading-guidance). Also consider reducing the number of nonclustered indexes.
- In the General Purpose service tier, the provisioned data storage is less expensive than the price of the backup storage. If you have continually high excess backup storage costs, consider increasing data storage to save on the backup storage.
- Use `tempdb` instead of permanent tables in your application logic for storing temporary results or transient data.
- Use locally redundant backup storage whenever possible (for example, dev/test environments).

## Backup retention

Azure SQL Database provides both short-term and long-term retention of backups. Short-term retention allows PITR within the retention period for the database. Long-term retention provides backups for various compliance requirements.  

### Short-term retention

For all new, restored, and copied databases, Azure SQL Database retains sufficient backups to allow PITR within the last seven days by default. Azure SQL Database takes regular full, differential, and log backups to ensure that databases are restorable to any point in time within the retention period of the database.  

You can configure differential backups to occur either once in 12 hours or once in 24 hours. A 24-hour differential backup frequency might increase the time required to restore the database, compared to the 12-hour frequency. In the vCore model, the default frequency for differential backups is once in 12 hours. In the DTU model, the default frequency is once in 24 hours.  

You can specify your backup storage redundancy option for STR when you create your database, and then change it later. If you change your backup redundancy option on an existing database, new backups use the new redundancy option. Azure doesn't move or copy backup copies made with the previous short term redundancy option. Azure leaves them in the original storage account until the retention period expires, which can be 1 to 35 days.

You can [change the backup retention period](automated-backups-change-settings.md#change-short-term-retention-policy) for each active database in the range of 1 to 35 days, except for Basic databases, which are configurable from 1 to 7 days. As described in [Backup storage consumption](#backup-storage-consumption), backups stored to enable PITR might be older than the retention period. If you need to keep backups for longer than the maximum short-term retention period of 35 days, you can enable [long-term retention](long-term-retention-overview.md).

If you delete a database, Azure keeps backups in the same way for an online database with its specific retention period. You can't change the backup retention period for a deleted database.

> [!IMPORTANT]
> If you delete a logical Azure SQL server, you also delete all databases on that logical server. You can't recover deleted databases. You can't restore a deleted logical server. But if you configured long-term retention for a database, LTR backups aren't deleted. You can then use those backups to restore databases on a different logical server in the same subscription, to a point in time when an LTR backup was taken. For more information, see [Restore long-term backup](long-term-backup-retention-configure.md#view-backups-and-restore-from-a-backup).

### Long-term retention

For SQL Database, you can configure full long-term retention (LTR) backups for up to 10 years in Azure Blob Storage. After you configure the LTR policy, Azure automatically copies full backups to a different storage container weekly.  

To meet various compliance requirements, select different retention periods for weekly, monthly, and yearly full backups. The frequency depends on the policy. For example, setting `W=0, M=1` creates an LTR copy monthly. For more information about LTR, see [Long-term retention](long-term-retention-overview.md). 

Updating the backup storage redundancy for an existing database applies the change only to subsequent backups taken in the future and not to existing backups. All existing LTR backups for the database continue to reside in the existing storage blob. New backups are replicated based on the configured backup storage redundancy. 

Storage consumption depends on the selected frequency and retention periods of LTR backups. Use the [LTR pricing calculator](https://azure.microsoft.com/pricing/calculator/?service=sql-database) to estimate the cost of LTR storage.

When restoring a Hyperscale database from an LTR backup, the read scale property is disabled. To enable, read scale on the restored database, update the database after it has been created. You need to specify the target service level objective when restoring from an LTR backup. 

You can enable long-term retention for Hyperscale databases created or migrated from other service tiers. If you attempt to enable LTR for a Hyperscale database where it isn't yet supported, you receive the following error: "An error has occurred while enabling Long-term backup retention for this database. Please reach out to Microsoft support to enable long-term backup retention." In this case, contact Microsoft support and create a support ticket to resolve.

## Backup storage costs

The price for backup storage varies and depends on your [purchasing model (DTU or vCore)](purchasing-models.md), chosen backup storage redundancy option, and region. You pay for backup storage based on gigabytes consumed per month, at the same rate for all backups. 

For pricing, see the [Azure SQL Database pricing](https://azure.microsoft.com/pricing/details/sql-database/single/) page. 

> [!NOTE]
> An Azure invoice shows only the excess backup storage consumption, not the entire backup storage consumption. For example, in a hypothetical scenario, if you provision 4 TB of data storage, you get 4 TB of free backup storage space. If you use a total of 5.8 TB of backup storage space, the Azure invoice shows only 1.8 TB, because you pay only for excess backup storage that you use.

### DTU model

In the DTU model, for databases and elastic pools there's no extra charge for PITR backup storage for default retention of seven days and beyond. The price of PITR backup storage is part of the database or pool price.

In the DTU model, you pay for the [LTR backup](long-term-retention-overview.md) storage for databases and elastic pools based on the actual storage consumed by LTR backups. 

### vCore model

Azure SQL Database calculates your total billable backup storage as a cumulative value across all backup files. Every hour, Azure sends this value to the billing pipeline. The pipeline aggregates this hourly usage to determine your backup storage consumption at the end of each month.  

If you delete a database, backup storage consumption gradually decreases as older backups age out and are deleted. Because differential backups and log backups require an earlier full backup to be restorable, Azure purges all three backup types together in weekly sets. After all backups are deleted, billing stops. 

Hyperscale databases use a different method for calculating backup storage costs. For more information, see [Hyperscale backup storage costs](hyperscale-automated-backups-overview.md#backup-storage-costs). 

For single databases, you get a backup storage amount equal to the maximum data storage size for the database at no extra charge. The following equation calculates the total billable backup storage usage:

`Total billable backup storage size = (size of full backups + size of differential backups + size of log backups) – maximum data storage`

For elastic pools, you get a backup storage amount equal to the maximum data storage for the pool storage size at no extra charge. For pooled databases, the total size of billable backup storage is aggregated at the pool level and is calculated as follows:

`Total billable backup storage size = (total size of all full backups + total size of all differential backups + total size of all log backups) - maximum pool data storage`

You pay for total billable backup storage, if any, in gigabytes per month according to the backup storage redundancy. This backup storage consumption depends on the workload and size of individual databases, elastic pools, and managed instances. Heavily modified databases have larger differential and log backups, because the size of these backups is proportional to the amount of changed data. Therefore, such databases have higher backup charges.

As a simplified example, assume that a database accumulates 744 GB of backup storage and that this amount stays constant throughout an entire month because the database is completely idle. To convert this cumulative storage consumption to hourly usage, divide it by 744.0 (31 days per month times 24 hours per day). SQL Database reports to the Azure billing pipeline that the database consumed 1 GB of PITR backup each hour, at a constant rate. Azure billing aggregates this consumption and shows a usage of 744 GB for the entire month. The cost is based on the rate for gigabytes per month in your region.

Here's another example. Suppose the same idle database has its retention increased from 7 days to 14 days in the middle of the month. This increase results in the total backup storage doubling to 1,488 GB. SQL Database reports 1 GB of usage for hours 1 through 372 (the first half of the month). It reports the usage as 2 GB for hours 373 through 744 (the second half of the month). This usage aggregates to a final bill of 1,116 GB per month.

Actual backup billing scenarios are more complex. Because the rate of changes in the database depends on the workload and is variable over time, the size of each differential and log backup also varies. The hourly consumption of backup storage fluctuates accordingly.

Each differential backup also contains all changes made in the database since the last full backup. So, the total size of all differential backups gradually increases over the course of a week. Then it drops sharply after an older set of full, differential, and log backups ages out. 

For example, assume that a heavy write activity, such as an index rebuild, runs just after a full backup is completed. The modifications that the index rebuild makes are included:

- In the transaction log backups taken over the duration of the rebuild.
- In the next differential backup.
- In every differential backup taken until the next full backup occurs. 

For the last scenario in larger databases, an optimization in Azure creates a full backup instead of a differential backup if a differential backup would be excessively large otherwise. This optimization reduces the size of all differential backups until the following full backup.

You can monitor total backup storage consumption for each backup type (full, differential, transaction log) over time, as described in [Monitor consumption](#monitor-consumption).

### Monitor costs

To understand backup storage costs, go to **Cost Management + Billing** in the Azure portal. Select **Cost Management**, and then select **Cost analysis**. Select the desired subscription for **Scope**, and then filter for the time period and service that you're interested in as follows:

1. Add a filter for **Service name**.
1. In the dropdown list, select **sql database** for a single database or an elastic database pool.
1. Add another filter for **Meter subcategory**.
1. To monitor PITR backup costs, in the dropdown list, select **single/elastic pool pitr backup storage** for a single database or an elastic database pool. Meters show up only if backup storage consumption exists.
   
   To monitor LTR backup costs, in the dropdown list, select **ltr backup storage** for a single database or an elastic database pool. Meters show up only if backup storage consumption exists.

The **Storage** and **compute** subcategories might also interest you, but they're not associated with backup storage costs.

:::image type="content" source="media/automated-backups-overview/check-backup-storage-cost-sql-mi.png" alt-text="Screenshot that shows an analysis of backup storage costs." lightbox="media/automated-backups-overview/check-backup-storage-cost-sql-mi.png":::

> [!IMPORTANT]
> Meters are visible only for counters that are currently in use. If a counter isn't available, it's likely that the category isn't currently being used. For example, storage counters aren't visible for resources that aren't consuming storage. If there's no PITR or LTR backup storage consumption, these meters aren't visible.

For more information, see [Azure SQL Database cost management](cost-management.md).

## Encrypted backups

If you encrypt your database by using TDE, backups are automatically encrypted at rest, including LTR backups. All new databases in Azure SQL are configured with TDE enabled by default. For more information on TDE, see [Transparent data encryption with SQL Database](/sql/relational-databases/security/encryption/transparent-data-encryption-azure-sql).

## Backup integrity

Azure SQL Database automatically handles certain types of data corruption by using built-in techniques when needed, without any data loss. In Azure SQL Database, the SQL Database Engine performs page verification during service-managed backups and during each restore operation. Any issues found during an integrity check result in an alert to the engineering team.

As an extra layer of protection, you can test backup restoration and run integrity checks. For more information, see [Data integrity in Azure SQL Database](data-integrity.md).

All database backups use the `CHECKSUM` option to provide extra backup integrity.

## Backup protection 

Microsoft-owned Azure subscriptions manage Azure SQL Database backups by using secure, internal Azure Storage accounts. You can't access these backups externally, so they provide strong data isolation and protection. Within Microsoft, only backend services can access, create, copy, or restore these backups. Microsoft engineers, including developers, don't have standing access. To minimize exposure and maximize security, Microsoft can only obtain Just-In-Time (JIT) access under strict audit controls when absolutely necessary to troubleshoot specific customer issues.

Backups are automatically deleted after the retention period expires.

## Compliance through backup retention 

If the default retention doesn't meet your compliance requirements, change the PITR retention period. For more information, see [Change the PITR backup retention period](automated-backups-change-settings.md#change-short-term-retention-policy).

When you migrate your database from a DTU-based service tier to a vCore-based service tier, the migration preserves the PITR retention to ensure that your application's data recovery policy isn't compromised. 

> [!NOTE]
> For steps to delete personal data in Azure SQL Database backups to support your obligations under the GDPR, see [Change automated backup settings](automated-backups-change-settings.md). For general information about GDPR, see the [GDPR section of the Microsoft Trust Center](https://www.microsoft.com/trust-center/privacy/gdpr-overview) and the [GDPR section of the Service Trust portal](https://servicetrust.microsoft.com/ViewPage/GDPRGetStarted).

## Use Azure Policy to enforce backup storage redundancy

If you have data residency requirements that require you to keep all your data in a single Azure region, you can enforce zone-redundant or locally redundant backups for your SQL database by using Azure Policy. 

Azure Policy is a service that you can use to create, assign, and manage policies that apply rules to Azure resources. Azure Policy helps you keep these resources compliant with your corporate standards and service-level agreements. For more information, see [Overview of Azure Policy](/azure/governance/policy/overview). 

### Built-in backup storage redundancy policies

To enforce data residency requirements at an organizational level, assign policies to a subscription by using the [Azure portal](/azure/governance/policy/assign-policy-portal) or [Azure PowerShell](/azure/governance/policy/assign-policy-powershell). 

For example, if you enable the policy "Azure SQL DB should avoid using GRS backup", users can't create databases with the default storage as globally redundant storage. The policy prevents users from using GRS and returns the error message "Configuring backup storage account type to 'Standard_RAGRS' failed during Database create or update."

For a full list of built-in policy definitions for SQL Database, see the [policy reference](./policy-reference.md).

> [!IMPORTANT]
> Azure policies aren't enforced when you create a database via T-SQL. To specify data residency when you create a database by using T-SQL, [use LOCAL or ZONE as input to the BACKUP_STORAGE_REDUNDANCY parameter in the CREATE DATABASE statement](/sql/t-sql/statements/create-database-transact-sql#create-database-using-zone-redundancy-for-backups).

## Related content

- To learn about other SQL Database business continuity solutions, see [Business continuity overview](business-continuity-high-availability-disaster-recover-hadr-overview.md).
- To change backup settings, see [Change settings](automated-backups-change-settings.md). 
- To restore a backup, see [Recover by using backups](recovery-using-backups.md) or [Restore a database to a point in time by using PowerShell](scripts/restore-database-powershell.md).
- For information about how to configure, manage, and restore from long-term retention of automated backups in Azure Blob Storage, see [Manage long-term backup retention](long-term-backup-retention-configure.md).
- For Azure SQL Managed Instance, see [Automated backups for SQL Managed Instance](../managed-instance/automated-backups-overview.md).
