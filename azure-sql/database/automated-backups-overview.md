---
title: Automatic, geo-redundant backups
titleSuffix: Azure SQL Database
description: Learn how Azure SQL Database automatically backs up all databases and provides a point-in-time restore capability.
author: SudhirRaparla
ms.author: nvraparl
ms.reviewer: mathoma, wiassaf, danil
ms.date: 09/14/2022
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
# Automated backups in Azure SQL Database

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](automated-backups-overview.md)
> * [Azure SQL Managed Instance](../managed-instance/automated-backups-overview.md)

<!---
Some of the content in this article is duplicated in /azure-sql/managed-instance/automated-backups-overview.md. Any relevant changes made to this article should be made in the other article as well. 
--->

This article describes the automated backup feature for Azure SQL Database.  

To change backup settings, see [Change settings](automated-backups-change-settings.md). To restore a backup, see [Recover using automated database backups](recovery-using-backups.md). 

## What is a database backup?

Database backups are an essential part of any business continuity and disaster recovery strategy, because they help protect your data from corruption or deletion. These backups enable database restore to a point in time within the configured retention period. If your data protection rules require that your backups are available for an extended time (up to 10 years), you can configure [long-term retention (LTR)](long-term-retention-overview.md) for both single and pooled databases.

For service tiers other than Hyperscale, Azure SQL Database uses SQL Server engine technology to back up and restore data. Hyperscale databases use backup and restore based on [storage snapshots](hyperscale-architecture.md#azure-storage). With traditional SQL Server backup technology, larger databases have long backup/restore times. With the use of snapshots, Hyperscale provides instant backup and fast restore capabilities irrespective of database size. To learn more, see [Hyperscale backups](hyperscale-automated-backups-overview.md). 


## Backup frequency

Azure SQL Database creates:

- [Full backups](/sql/relational-databases/backup-restore/full-database-backups-sql-server) every week.
- [Differential backups](/sql/relational-databases/backup-restore/differential-backups-sql-server) every 12 or 24 hours.
- [Transaction log backups](/sql/relational-databases/backup-restore/transaction-log-backups-sql-server) approximately every 10 minutes. 

The exact frequency of transaction log backups is based on the compute size and the amount of database activity. When you restore a database, the service determines which full, differential, and transaction log backups need to be restored.

The Hyperscale architecture does not require full, differential, or log backups. To learn more, see [Hyperscale backups](hyperscale-automated-backups-overview.md). 


## Backup storage redundancy

By default, Azure SQL Database stores backups in geo-redundant [storage blobs](/azure/storage/common/storage-redundancy) that are replicated to a [paired region](/azure/availability-zones/cross-region-replication-azure). Geo-redundancy helps protect against outages that affect backup storage in the primary region. It also allows you to restore your databases in a different region in the event of a regional outage. 

The storage redundancy mechanism stores multiple copies of your data so that it's protected from planned and unplanned events. These events might include transient hardware failure, network or power outages, or massive natural disasters. 

To ensure that your backups stay within the same region where your database is deployed, you can change backup storage redundancy from the default geo-redundant storage to other types of storage that keep your data within the region. The configured backup storage redundancy is applied to both short-term retention (STR) backups and LTR backups. To learn more about storage redundancy, see [Data redundancy](/azure/storage/common/storage-redundancy). 

You can configure backup storage redundancy when you create your database, and you can update it at a later time. The changes that you make to an existing database apply to future backups only. After you update the backup storage redundancy of an existing database, the changes might take up to 48 hours to be applied. 

You can choose one of the following storage redundancies for backups:

- **Locally redundant storage (LRS)**:  Copies your backups synchronously three times within a single physical location in the primary region. LRS is the least expensive storage option, but we don't recommend it for applications that require resiliency to regional outages or a guarantee of high data durability.

   :::image type="content" source="../database/media/automated-backups-overview/multi-paired-lrs.svg" alt-text="Diagram showing the locally-redundant storage (LRS) option.":::

- **Zone-redundant storage (ZRS)**: Copies your backups synchronously across three Azure availability zones in the primary region. It's currently available in only [certain regions](/azure/storage/common/storage-redundancy#zone-redundant-storage). 

   :::image type="content" source="../database/media/automated-backups-overview/multi-paired-zrs.svg" alt-text="Diagram showing the zone-redundant storage (ZRS) option.":::

- **Geo-redundant storage (GRS)**: Copies your backups synchronously three times within a single physical location in the primary region by using LRS. Then it copies your data asynchronously three times to a single physical location in the [paired](/azure/availability-zones/cross-region-replication-azure#azure-cross-region-replication-pairings-for-all-geographies) secondary region. 

  The result is:
  
  - Three synchronous copies in the primary region.
  - Three synchronous copies in the paired region that were copied over from the primary region to the secondary region asynchronously. 

   :::image type="content" source="../database/media/automated-backups-overview/multi-paired-grs.svg" alt-text="Diagram showing the geo-redundant storage (GRS) option.":::

> [!WARNING]
> - [Geo-restore](recovery-using-backups.md#geo-restore) is disabled as soon as a database is updated to use locally redundant or zone-redundant storage. 
> - The storage redundancy diagrams all show regions with multiple availability zones (multi-az). However, there are some regions which provide only a single availability zone and do not support ZRS. 
> - Backup storage redundancy for Hyperscale databases can be set only during creation. You can't modify this setting after the resource is provisioned. To update backup storage redundancy settings for an existing Hyperscale database with minimum downtime, use [active geo-replication](active-geo-replication-overview.md). Alternatively, you can use [database copy](database-copy.md). Learn more in [Hyperscale backups and storage redundancy](hyperscale-automated-backups-overview.md#data-and-backup-storage-redundancy).

## Backup usage

You can use automatically created backups in the following scenarios:

- [Restore an existing database to a point in time](recovery-using-backups.md#point-in-time-restore) within the retention period by using the Azure portal, Azure PowerShell, the Azure CLI, or the REST API. This operation creates a new database on the same server as the original database, but it uses a different name to avoid overwriting the original database. 

  After restore finishes, you can optionally delete the original database and rename the restored database to the original database name. Alternatively, instead of deleting the original database, you can [rename](/sql/relational-databases/databases/rename-a-database) it, and then rename the restored database to the original database name. 
- [Restore a deleted database to a point in time](recovery-using-backups.md#deleted-database-restore) within the retention period, including the time of deletion. The deleted database can be restored only on the same server where you created the original database. Before you delete a database, the service takes a final transaction log backup to prevent any data loss.
- [Restore a database to another geographic region](recovery-using-backups.md#geo-restore). Geo-restore allows you to recover from a regional outage when you can't access your database or backups in the primary region. It creates a new database on any existing server in any Azure region.
   > [!IMPORTANT]
   > Geo-restore is available only for databases that are configured with geo-redundant backup storage. If you're not currently using geo-replicated backups for a database, you can change this by [configuring backup storage redundancy](automated-backups-change-settings.md#configure-backup-storage-redundancy).
- [Restore a database from a specific long-term backup](long-term-retention-overview.md) of a single or pooled database, if the database has been configured with an LTR policy. LTR allows you to [restore an older version of the database](long-term-backup-retention-configure.md) by using the Azure portal, the Azure CLI, or Azure PowerShell to satisfy a compliance request or to run an older version of the application. For more information, see [Long-term retention](long-term-retention-overview.md).


## <a id="restore-capabilities"></a>Restore capabilities and features

This table summarizes the capabilities and features of [point-in-time restore (PITR)](recovery-using-backups.md#point-in-time-restore), [geo-restore](recovery-using-backups.md#geo-restore), and [long-term retention](long-term-retention-overview.md).

| Backup property | PITR | Geo-restore | LTR |
|---|---|---|---|
| **Types of SQL backup** | Full, differential, log. | Replicated copies of PITR backups. | Only the full backups. |
| **Recovery point objective (RPO)** | 10 minutes, based on compute size and amount of database activity. | Up to 1 hour, based on geo-replication.\*  | One week (or user's policy).|
| **Recovery time objective (RTO)** | Restore usually takes less than 12 hours but could take longer, depending on size and activity. See [Recovery](recovery-using-backups.md#recovery-time). | Restore usually takes less than 12 hours but could take longer, depending on size and activity. See [Recovery](recovery-using-backups.md#recovery-time). | Restore usually takes less than 12 hours but could take longer, depending on size and activity. See [Recovery](recovery-using-backups.md#recovery-time). |
| **Retention** | 7 days by default, configurable up to 35 days. | Enabled by default, same as source.\*\* | Not enabled by default. Retention is up to 10 years. |
| **Azure Storage**  | Geo-redundant by default. You can optionally configure zone-redundant or locally redundant storage. | Available when PITR backup storage redundancy is set to geo-redundant. Not available when PITR backup storage is zone-redundant or locally redundant. | Geo-redundant by default. You can configure zone-redundant or locally redundant storage. |
| **Restoring a new database in the same region** | Supported | Supported | Supported |
| **Restoring a new database in another region** | Not supported | Supported in any Azure region | Supported in any Azure region |
| **Restoring a new database in another subscription** |  Not supported  |  Not supported\*\*\* | Not supported\*\*\*  |
| **Restoring via Azure portal**|Yes|Yes|Yes|
| **Restoring via PowerShell** |Yes|Yes|Yes|
| **Restoring via Azure CLI** |Yes|Yes|Yes|


\* For business-critical applications that require large databases and must ensure business continuity, use [auto-failover groups](auto-failover-group-sql-db.md). 

\*\* All PITR backups are stored on geo-redundant storage by default, so geo-restore is enabled by default. 

\*\*\* The workaround is to restore to a new server and use Resource Move to move the server to another subscription, or use a [cross-subscription database copy](database-copy.md#copy-to-a-different-subscription).

## Restore a database from backup

To perform a restore, see [Restore a database from backups](recovery-using-backups.md). You can explore backup configuration and restore operations by using the following examples.

| Operation | Azure portal | Azure CLI | Azure PowerShell |
|---|---|---|---|
| **Change backup retention** | [SQL Database](automated-backups-change-settings.md?tabs=azure-portal#change-short-term-retention-policy) <br/> [SQL Managed Instance](../managed-instance/automated-backups-change-settings.md?tabs=azure-portal#change-short-term-retention-policy) | [SQL Database](automated-backups-change-settings.md?tabs=azure-cli#change-short-term-retention-policy) <br/> [SQL Managed Instance](../managed-instance/automated-backups-change-settings.md?tabs=azure-cli#change-short-term-retention-policy) | [SQL Database](automated-backups-change-settings.md?tabs=powershell#change-short-term-retention-policy) <br/>[SQL Managed Instance](../managed-instance/automated-backups-change-settings.md?tabs=powershell#change-short-term-retention-policy)|
| **Change long-term backup retention** | [SQL Database](long-term-backup-retention-configure.md#create-long-term-retention-policies)<br/> [SQL Managed Instance](../managed-instance/long-term-backup-retention-configure.md) | [SQL Database](long-term-backup-retention-configure.md) <br/> [SQL Managed Instance](../managed-instance/long-term-backup-retention-configure.md) | [SQL Database](long-term-backup-retention-configure.md)<br/>[SQL Managed Instance](../managed-instance/long-term-backup-retention-configure.md)  |
| **Restore a database from a point in time** | [SQL Database](recovery-using-backups.md#point-in-time-restore)<br>[SQL Managed Instance](../managed-instance/point-in-time-restore.md) | [SQL Database](/cli/azure/sql/db#az-sql-db-restore) <br/> [SQL Managed Instance](/cli/azure/sql/midb#az-sql-midb-restore) | [SQL Database](/powershell/module/az.sql/restore-azsqldatabase) <br/> [SQL Managed Instance](/powershell/module/az.sql/restore-azsqlinstancedatabase) |
| **Restore a deleted database** | [SQL Database](recovery-using-backups.md)<br>[SQL Managed Instance](../managed-instance/point-in-time-restore.md#restore-a-deleted-database) | [SQL Database](long-term-backup-retention-configure.md#restore-from-ltr-backups) <br/> [SQL Managed Instance](../managed-instance/long-term-backup-retention-configure.md#restore-from-ltr-backups) | [SQL Database](/powershell/module/az.sql/get-azsqldeleteddatabasebackup) <br/> [SQL Managed Instance](/powershell/module/az.sql/get-azsqldeletedinstancedatabasebackup)|

## Backup scheduling

The first full backup is scheduled immediately after a new database is created or restored. This backup usually finishes within 30 minutes, but it can take longer when the database is large. For example, the initial backup can take longer on a restored database or a database copy, which would typically be larger than a new database. 

After the first full backup, all further backups are scheduled and managed automatically. The exact timing of all database backups is determined by the SQL Database service as it balances the overall system workload. You can't change the schedule of backup jobs or disable them. 

> [!IMPORTANT]
> - For a new, restored, or copied database, the point-in-time restore capability becomes available when the initial transaction log backup that follows the initial full backup is created.
> - Hyperscale databases are protected immediately after creation, unlike other databases where the initial backup takes time. The protection is immediate even if the Hyperscale database was created with a large amount of data via copy or restore. To learn more, review [Hyperscale automated backups](hyperscale-automated-backups-overview.md). 

## Backup storage consumption

With SQL Server backup and restore technology, restoring a database to a point in time requires an uninterrupted backup chain. That chain consists of one full backup, optionally one differential backup, and one or more transaction log backups. 

Azure SQL Database schedules one full backup every week. To provide PITR within the entire retention period, the system must store additional full, differential, and transaction log backups for up to a week longer than the configured retention period. 

In other words, for any point in time during the retention period, there must be a full backup that's older than the oldest time of the retention period. There must also be an uninterrupted chain of differential and transaction log backups from that full backup until the next full backup. 

Hyperscale databases use a different backup scheduling mechanism. For more information, see [Hyperscale backup scheduling](hyperscale-automated-backups-overview.md#backup-scheduling). 

Backups that are no longer needed to provide PITR functionality are automatically deleted. Because differential backups and log backups require an earlier full backup to be restorable, all three backup types are purged together in weekly sets.

For all databases, including [TDE-encrypted](transparent-data-encryption-tde-overview.md) databases, backups are compressed to reduce backup storage consumption and costs. Average backup compression ratio is 3 to 4 times. However, it can be significantly lower or higher depending on the nature of the data and whether data compression is used in the database.

Azure SQL Database computes your total used backup storage as a cumulative value. Every hour, this value is reported to the Azure billing pipeline. The pipeline is responsible for aggregating this hourly usage to calculate your consumption at the end of each month. After the database is deleted, consumption decreases as backups age out and are deleted. After all backups are deleted and PITR is no longer possible, billing stops.
   
> [!IMPORTANT]
> Backups of a database are retained to provide PITR even if the database has been deleted. Although deleting and re-creating a database might save storage and compute costs, it might increase backup storage costs. The reason is that the service retains backups for each deleted database, every time it's deleted. 

### Monitor consumption

For vCore databases in Azure SQL Database, the storage that each type of backup (full, differential, and log) consumes is reported on the database monitoring pane as a separate metric. The following screenshot shows how to monitor the backup storage consumption for a single database. 

:::image type="content" source="./media/automated-backups-overview/backup-metrics.png" alt-text="Screenshot that shows selections for monitoring database backup consumption in the Azure portal.":::

For instructions on how to monitor consumption in Hyperscale, see [Monitor Hyperscale backup consumption](hyperscale-automated-backups-overview.md#monitor-backup-storage-consumption).

### Fine-tune backup storage consumption

Backup storage consumption up to the maximum data size for a database is not charged. Excess backup storage consumption will depend on the workload and maximum size of the individual databases. Consider some of the following tuning techniques to reduce your backup storage consumption:

- Reduce the [backup retention period](automated-backups-change-settings.md#change-short-term-retention-policy) to the minimum for your needs.
- Avoid doing large write operations, like index rebuilds, more often than you need to.
- For large data load operations, consider using [clustered columnstore indexes](/sql/relational-databases/indexes/columnstore-indexes-overview) and following related [best practices](/sql/relational-databases/indexes/columnstore-indexes-data-loading-guidance). Also consider reducing the number of non-clustered indexes.
- In the General Purpose service tier, the provisioned data storage is less expensive than the price of the backup storage. If you have continually high excess backup storage costs, you might consider increasing data storage to save on the backup storage.
- Use TempDB instead of permanent tables in your application logic for storing temporary results or transient data.
- Use locally redundant backup storage whenever possible (for example, dev/test environments).

## Backup retention

Azure SQL Database provides both short-term and long-term retention of backups. Short-term retention allows PITR within the retention period for the database. Long-term retention provides backups for various compliance requirements.  

### Short-term retention

For all new, restored, and copied databases, Azure SQL Database retains sufficient backups to allow PITR within the last 7 days by default. The service takes regular full, differential, and log backups to ensure that databases are restorable to any point in time within the retention period that's defined for the database. 

Short-term back up retention of 1 to 35 days for Hyperscale databases is now in preview. To learn more, review [Managing backup retention in Hyperscale](hyperscale-automated-backups-overview.md#backup-retention). 

Differential backups can be configured to occur either once in 12 hours or once in 24 hours. A 24-hour differential backup frequency might increase the time required to restore the database, compared to the 12-hour frequency. In the vCore model, the default frequency for differential backups is once in 12 hours. In the DTU model, the default frequency is once in 24 hours.  

You can specify your backup storage redundancy option for STR when you create your database, and then change it at a later time. If you change your backup redundancy option after your database is created, new backups will use the new redundancy option. Backup copies made with the previous STR redundancy option are not moved or copied. They're left in the original storage account until the retention period expires, which can be 1 to 35 days.

Except for Basic databases, you can [change the backup retention period](automated-backups-change-settings.md#change-short-term-retention-policy) for each active database in the range of 1 to 35 days. As described in [Backup storage consumption](#backup-storage-consumption), backups stored to enable PITR might be older than the retention period. If you need to keep backups for longer than the maximum short-term retention period of 35 days, you can enable [long-term retention](long-term-retention-overview.md).

If you delete a database, the system keeps backups in the same way that it would for an online database with its specific retention period. You can't change the backup retention period for a deleted database.

> [!IMPORTANT]
> If you delete a server, all databases on that server are also deleted and can't be recovered. You can't restore a deleted server. But if you've configured long-term retention for a database, LTR backups are not deleted. You can then use those backups to restore databases on a different server in the same subscription, to a point in time when an LTR backup was taken. To learn more, review [Restore long-term backup](long-term-backup-retention-configure.md#view-backups-and-restore-from-a-backup).

### Long-term retention

For SQL Database, you can configure full LTR backups for up to 10 years in Azure Blob Storage. After the LTR policy is configured, full backups are automatically copied to a different storage container weekly. 

To meet various compliance requirements, you can select different retention periods for weekly, monthly, and/or yearly full backups. The frequency depends on the policy. For example, setting `W=0, M=1` would create an LTR copy monthly. For more information about LTR, see [Long-term retention](long-term-retention-overview.md). 

Updating the backup storage redundancy for an existing database applies the change only to subsequent backups taken in the future and not for existing backups. All existing LTR backups for the database will continue to reside in the existing storage blob. New backups will be replicated based on the configured backup storage redundancy. 

Storage consumption depends on the selected frequency and retention periods of LTR backups. You can use the [LTR pricing calculator](https://azure.microsoft.com/pricing/calculator/?service=sql-database) to estimate the cost of LTR storage.

## Backup storage costs

The price for backup storage varies and depends on your [purchasing model (DTU or vCore)](purchasing-models.md), chosen backup storage redundancy option, and region. Backup storage is charged based on gigabytes consumed per month, at the same rate for all backups. 

Backup storage redundancy affects backup costs in the following way:

- `Locally redundant price = published price`
- `Zone-redundant price = published price x 1.25`
- `Geo-redundant price = published price x 2`

For pricing, see the [Azure SQL Database pricing](https://azure.microsoft.com/pricing/details/sql-database/single/) page. 

> [!NOTE]
> An Azure invoice shows only the excess backup storage consumption, not the entire backup storage consumption. For example, in a hypothetical scenario, if you have provisioned 4 TB of data storage, you'll get 4 TB of free backup storage space. If you use a total of 5.8 TB of backup storage space, the Azure invoice will show only 1.8 TB, because you're charged only for excess backup storage that you've used.

### DTU model

In the DTU model, there's no additional charge for backup storage for databases and elastic pools. The price of backup storage is a part of the database or pool price.

### vCore model

Azure SQL Database computes your total billable backup storage as a cumulative value across all backup files. Every hour, this value is reported to the Azure billing pipeline. The pipeline aggregates this hourly usage to get your backup storage consumption at the end of each month. 

If a database is deleted, backup storage consumption will gradually decrease as older backups age out and are deleted. Because differential backups and log backups require an earlier full backup to be restorable, all three backup types are purged together in weekly sets. After all backups are deleted, billing stops. 

Backup storage cost is calculated differently for Hyperscale databases. For more information, see [Hyperscale backup storage costs](hyperscale-automated-backups-overview.md#backup-storage-costs). 

For single databases, a backup storage amount equal to 100 percent of the maximum data storage size for the database is provided at no extra charge.  The following equation is used to calculate the total billable backup storage usage:

`Total billable backup storage size = (size of full backups + size of differential backups + size of log backups) – maximum data storage`

For elastic pools, a backup storage amount equal to 100 percent of the maximum data storage for the pool storage size is provided at no extra charge. For pooled databases, the total size of billable backup storage is aggregated at the pool level and is calculated as follows:

`Total billable backup storage size = (total size of all full backups + total size of all differential backups + total size of all log backups) - maximum pool data storage`

Total billable backup storage, if any, is charged in gigabytes per month according to the rate of the backup storage redundancy that you've used. This backup storage consumption depends on the workload and size of individual databases, elastic pools, and managed instances. Heavily modified databases have larger differential and log backups, because the size of these backups is proportional to the amount of changed data. Therefore, such databases will have higher backup charges.

As a simplified example, assume that a database has accumulated 744 GB of backup storage and that this amount stays constant throughout an entire month because the database is completely idle. To convert this cumulative storage consumption to hourly usage, divide it by 744.0 (31 days per month times 24 hours per day). SQL Database will report to the Azure billing pipeline that the database consumed 1 GB of PITR backup each hour, at a constant rate. Azure billing will aggregate this consumption and show a usage of 744 GB for the entire month. The cost will be based on the rate for gigabytes per month in your region.

Here's another example. Suppose the same idle database has its retention increased from 7 days to 14 days in the middle of the month. This increase results in the total backup storage doubling to 1,488 GB. SQL Database would report 1 GB of usage for hours 1 through 372 (the first half of the month). It would report the usage as 2 GB for hours 373 through 744 (the second half of the month). This usage would be aggregated to a final bill of 1,116 GB per month.

Actual backup billing scenarios are more complex. Because the rate of changes in the database depends on the workload and is variable over time, the size of each differential and log backup will also vary. The hourly consumption of backup storage will fluctuate accordingly.

Each differential backup also contains all changes made in the database since the last full backup. So, the total size of all differential backups gradually increases over the course of a week. Then it drops sharply after an older set of full, differential, and log backups ages out. 

For example, assume that a heavy write activity, such as an index rebuild, runs just after a full backup is completed. The modifications that the index rebuild makes will then be included:

- In the transaction log backups taken over the duration of the rebuild.
- In the next differential backup.
- In every differential backup taken until the next full backup occurs. 

For the last scenario in larger databases, an optimization in the service creates a full backup instead of a differential backup if a differential backup would be excessively large otherwise. This reduces the size of all differential backups until the following full backup.

You can monitor total backup storage consumption for each backup type (full, differential, transaction log) over time, as described in [Monitor consumption](#monitor-consumption).

### Monitor costs

To understand backup storage costs, go to **Cost Management + Billing** in the Azure portal. Select **Cost Management**, and then select **Cost analysis**. Select the desired subscription for **Scope**, and then filter for the time period and service that you're interested in as follows:

1. Add a filter for **Service name**.
2. In the dropdown list, select **sql database** for a single database or an elastic database pool.
3. Add another filter for **Meter subcategory**.
4. To monitor PITR backup costs, in the dropdown list, select **single/elastic pool pitr backup storage** for a single database or an elastic database pool. Meters will show up only if backup storage consumption exists.
   
   To monitor LTR backup costs, in the dropdown list, select **ltr backup storage** for a single database or an elastic database pool. Meters will show up only if backup storage consumption exists.

The **Storage** and **compute** subcategories might also interest you, but they're not associated with backup storage costs.

:::image type="content" source="./media/automated-backups-overview/check-backup-storage-cost-sql-mi.png" alt-text="Screenshot that shows an analysis of backup storage costs.":::

> [!IMPORTANT]
> Meters are visible only for counters that are currently in use. If a counter is not available, it's likely that the category is not currently being used. For example, storage counters won't be visible for resources that are not consuming storage. If there is no PITR or LTR backup storage consumption, these meters won't be visible.

For more information, see [Azure SQL Database cost management](cost-management.md).

## Encrypted backups

If your database is encrypted with TDE, backups are automatically encrypted at rest, including LTR backups. All new databases in Azure SQL are configured with TDE enabled by default. For more information on TDE, see [Transparent data encryption with SQL Database](/sql/relational-databases/security/encryption/transparent-data-encryption-azure-sql).

## Backup integrity

On an ongoing basis, the Azure SQL engineering team automatically tests the restore of automated database backups. Upon point-in-time restore, databases also receive DBCC CHECKDB integrity checks.

Any issues found during an integrity check will result in an alert to the engineering team. For more information, see [Data integrity in SQL Database](https://azure.microsoft.com/blog/data-integrity-in-azure-sql-database/).

All database backups are taken with the CHECKSUM option to provide additional backup integrity.

## Compliance

When you migrate your database from a DTU-based service tier to a vCore-based service tier, the PITR retention is preserved to ensure that your application's data recovery policy isn't compromised. If the default retention doesn't meet your compliance requirements, you can change the PITR retention period. For more information, see [Change the PITR backup retention period](automated-backups-change-settings.md#change-short-term-retention-policy).

> [!NOTE]
> The [Change automated backup settings](automated-backups-change-settings.md) article provides steps about how to delete personal data from the device or service and can be used to support your obligations under the GDPR. For general information about GDPR, see the [GDPR section of the Microsoft Trust Center](https://www.microsoft.com/trust-center/privacy/gdpr-overview) and the [GDPR section of the Service Trust portal](https://servicetrust.microsoft.com/ViewPage/GDPRGetStarted).

## Use Azure Policy to enforce backup storage redundancy

If you have data residency requirements that require you to keep all your data in a single Azure region, you might want to enforce zone-redundant or locally redundant backups for your SQL database by using Azure Policy. 

Azure Policy is a service that you can use to create, assign, and manage policies that apply rules to Azure resources. Azure Policy helps you to keep these resources compliant with your corporate standards and service-level agreements. For more information, see [Overview of Azure Policy](/azure/governance/policy/overview). 

### Built-in backup storage redundancy policies 

To enforce data residency requirements at an organizational level, you can assign policies to a subscription by using the [Azure portal](/azure/governance/policy/assign-policy-portal) or [Azure PowerShell](/azure/governance/policy/assign-policy-powershell). For example, if you assign the following built-in policy, users in the subscription won't be able to create a database with geo-redundant backup storage via the Azure portal or Azure PowerShell: [SQL Database should avoid using GRS backup redundancy](https://portal.azure.com/#blade/Microsoft_Azure_Policy/PolicyDetailBlade/definitionId/%2Fproviders%2FMicrosoft.Authorization%2FpolicyDefinitions%2Fb219b9cf-f672-4f96-9ab0-f5a3ac5e1c13).

For a full list of built-in policy definitions for SQL Database, review the [policy reference](./policy-reference.md). 

> [!IMPORTANT]
> Azure policies are not enforced when you're creating a database via T-SQL. To specify data residency when you're creating a database by using T-SQL, [use LOCAL or ZONE as input to the BACKUP_STORAGE_REDUNDANCY parameter in the CREATE DATABASE statement](/sql/t-sql/statements/create-database-transact-sql#create-database-using-zone-redundancy-for-backups).

## Next steps

- To learn about other SQL Database business continuity solutions, see [Business continuity overview](business-continuity-high-availability-disaster-recover-hadr-overview.md).
- To change backup settings, see [Change settings](automated-backups-change-settings.md). 
- To restore a backup, see [Recover by using backups](recovery-using-backups.md) or [Restore a database to a point in time by using PowerShell](scripts/restore-database-powershell.md).
- For information about how to configure, manage, and restore from long-term retention of automated backups in Azure Blob Storage, see [Manage long-term backup retention](long-term-backup-retention-configure.md).
- For Azure SQL Managed Instance, see [Automated backups for SQL Managed Instance](../managed-instance/automated-backups-overview.md).
