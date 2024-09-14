---
title: Automatic, geo-redundant backups
titleSuffix: Azure SQL Managed Instance
description: Learn how Azure SQL Managed Instance automatically backs up all databases and provides point-in-time restore capability.
author: Stralle
ms.author: strrodic
ms.reviewer: mathoma, wiassaf, danil
ms.date: 07/12/2023
ms.service: azure-sql-managed-instance
ms.subservice: backup-restore
ms.topic: conceptual
ms.custom: references_regions, azure-sql-split, build-2024
monikerRange: "= azuresql || = azuresql-mi"
---
# Automated backups in Azure SQL Managed Instance

[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

<!---
Some of the content in this article is duplicated in /azure-sql/database/automated-backups-overview.md. Any relevant changes made to this article should be made in the other article as well. 
--->

> [!div class="op_single_selector"]
>
> * [Azure SQL Database](../database/automated-backups-overview.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](automated-backups-overview.md?view=azuresql-mi&preserve-view=true)

This article describes the automated backup feature for Azure SQL Managed Instance.

To change backup settings, see [Change settings](automated-backups-change-settings.md). To restore a backup, see [Recover using automated database backups](recovery-using-backups.md).

## What are automated database backups?

Database backups are an essential part of any business continuity and disaster recovery strategy because they help protect your data from corruption or deletion. With Azure SQL Managed Instance, SQL Server database engine backups are automatically managed by Microsoft, and stored on Microsoft-managed Azure storage accounts.

Use these backups to restore your database to a specific point in time within the configured retention period, up to 35 days. However, if your data protection rules require that your backups are available for an extended time (up to 10 years), you can configure [long-term retention (LTR)](../database/long-term-retention-overview.md) policies per each database. 

### Backup frequency

Azure SQL Managed Instance creates:

- [Full backups](/sql/relational-databases/backup-restore/full-database-backups-sql-server) every week.
- [Differential backups](/sql/relational-databases/backup-restore/differential-backups-sql-server) every 12 hours.
- [Transaction log backups](/sql/relational-databases/backup-restore/transaction-log-backups-sql-server) every ~10 minutes.

The frequency of transaction log backups depends on the compute size and the amount of database activity. Transaction logs are taken approximately every 10 minutes, but can vary. When you restore a database, the service determines which full, differential, and transaction log backups need to be restored, in their respective order.

> [!CAUTION]
> Automatic full backups are initiated once a week based on a schedule determined by Microsoft. [User-initiated backups](/sql/relational-databases/backup-restore/copy-only-backups-sql-server) have priority over automatic full backups, so a long-running copy-only backup can affect the timing of the next automatic full backup.

### Backup storage redundancy

By default, Azure SQL Managed Instance stores data in geo-redundant [storage blobs](/azure/storage/common/storage-redundancy) that are replicated to a [paired region](/azure/availability-zones/cross-region-replication-azure). Geo-redundancy helps protect against outages that affect backup storage in the primary region. It also allows you to restore your instance to a different region in the event of a disaster.

The storage redundancy mechanism stores multiple copies of your data so that it's protected from planned and unplanned events. Those events might include transient hardware failures, network or power outages, or massive natural disasters.

To ensure that your backups stay within the same region where your database is deployed, you can change backup storage redundancy from the default geo-redundant storage to other types of storage that keep your data within the region. To learn more about storage redundancy, see [Data redundancy](/azure/storage/common/storage-redundancy).

You can configure backup storage redundancy when you create your instance, and you can update it at a later time at the instance level. The changes that you make to an existing instance apply to future backups only. After you update the backup storage redundancy of an existing instance, it might take up to 24 hours for the changes to be applied. Changes made to backup storage redundancy apply to short-term backups only. Long-term retention policies inherit the redundancy option of short-term backups when the policy is created. The redundancy option persists for long-term backups even if the redundancy option for short-term backups subsequently changes.

> [!NOTE]
> Changing backup redundancy is an [update management operation](management-operations-overview.md#management-operations-long-running-segments) that initiates a failover.

You can choose one of the following storage redundancies for backups:

- **Locally redundant storage (LRS)**: Copies your backups synchronously three times within a single physical location in the primary region. LRS is the least expensive replication option, but we don't recommend it for applications that require high availability or durability.

   :::image type="content" source="../database/media/automated-backups-overview/multi-paired-lrs.svg" alt-text="Diagram showing the locally redundant storage (LRS) option.":::

- **Zone-redundant storage (ZRS)**: Copies your backups synchronously across three Azure availability zones in the primary region. It's currently available in [certain regions](/azure/storage/common/storage-redundancy#zone-redundant-storage).

   :::image type="content" source="../database/media/automated-backups-overview/multi-paired-zrs.svg" alt-text="Diagram showing the zone-redundant storage (ZRS) option.":::

- **Geo-redundant storage (GRS)**: Copies your backups synchronously three times within a single physical location in the primary region by using LRS. Then it copies your data asynchronously three times to a single physical location in the [paired](/azure/availability-zones/cross-region-replication-azure#azure-cross-region-replication-pairings-for-all-geographies) secondary region.

  The result is:
  
  - Three synchronous copies in the primary region within a single availability zone.
  - Three synchronous copies in the paired region within a single availability zone that were copied over from the primary region to the secondary region asynchronously.

   :::image type="content" source="../database/media/automated-backups-overview/multi-paired-grs.svg" alt-text="Diagram showing the geo-redundant storage (GRS) option.":::

- **Geo-zone-redundant storage (GZRS)**: Combines the high availability provided by redundancy across availability zones with protection from regional outages provided by geo-replication. Data in a GZRS account is copied across three Azure availability zones in the primary region. The data is also replicated to a secondary geographic region for protection from regional disasters. In that region, you also have three synchronous copies in a single availability zone that were copied over from the primary region to the secondary region asynchronously.

   :::image type="content" source="media/automated-backups-overview/multi-paired-gzrs.svg" alt-text="Diagram showing the geo-zone-redundant storage (GZRS) option.":::

> [!WARNING]
>
> - [Geo-restore](recovery-using-backups.md#geo-restore) is disabled as soon as a database is updated to use locally redundant or zone-redundant storage.
> - The storage redundancy diagrams all show regions with multiple availability zones (multi-az). However, there are [some regions](/azure/storage/common/redundancy-regions-zrs) which provide only a single availability zone and do not support ZRS or GZRS.

## Backup usage

You can use these backups to:

- [Restore an existing database to a point in time](recovery-using-backups.md#point-in-time-restore) in the past within the retention period by using the Azure portal, Azure PowerShell, the Azure CLI, or the REST API. This operation creates a new database on either the same instance as the original database or a different instance in the same subscription and region. It uses a different name to avoid overwriting the original database. You can also use the Azure portal to restore your point-in-time database backup to an instance in a different subscription from your source instance.

  After the restore finishes, you can delete the original database. Alternatively, you can both [rename](/sql/relational-databases/databases/rename-a-database) the original database and rename the restored database to the original database name.
- [Restore a deleted database to a point in time](recovery-using-backups.md#deleted-database-restore) within the retention period, including the time of deletion. You can restore the deleted database to the same managed instance where the backup was taken, or another instance in the same, or different subscription to the source instance. Before you delete a database, the service takes a final transaction log backup to prevent any data loss.
- [Restore a database to another geographic region](recovery-using-backups.md#geo-restore). Geo-restore allows you to recover from a geographic disaster when you can't access your database or backups in the primary region. It creates a new database on any existing managed instance in any Azure region.
   > [!IMPORTANT]
   > Geo-restore is available only for databases that are configured with geo-redundant backup storage. If you're not currently using geo-replicated backups for a database, you can change this by [configuring backup storage redundancy](automated-backups-change-settings.md#configure-backup-storage-redundancy).
- [Restore a database from a long-term backup](../database/long-term-retention-overview.md) of a database, if the database has a configured LTR policy. LTR allows you to [restore an older version of the database](long-term-backup-retention-configure.md) by using the Azure portal, the Azure CLI, or Azure PowerShell to satisfy a compliance request or to run an old version of the application. For more information, review the [Long-term retention overview](../database/long-term-retention-overview.md) page.

## Restore capabilities and features

This table summarizes the capabilities and features of [point-in-time restore](recovery-using-backups.md#point-in-time-restore) (PITR), [geo-restore](recovery-using-backups.md#geo-restore), and [long-term retention](../database/long-term-retention-overview.md).

| Backup properties | PITR | Geo-restore | LTR |
|---|---|---|---|
| **Types of SQL backup** | Full, differential, and transaction log backups. | Replicated copies of PITR backups. | Full backups only. |
| **Recovery point objective (RPO)** | Approximately 10 minutes, based on compute size and amount of database activity. | Up to 1 hour, based on geo-replication. <sup>1</sup>  | One week (or user's policy). |
| **Recovery time objective (RTO)** | Restore usually takes less than 12 hours but could take longer, depending on size and activity. See [Recovery](recovery-using-backups.md#recovery-time). | Restore usually takes less than 12 hours but could take longer, depending on size and activity. See [Recovery](recovery-using-backups.md#recovery-time). | Restore usually takes less than 12 hours but could take longer, depending on size and activity. See [Recovery](recovery-using-backups.md#recovery-time). |
| **Retention** | 1 to 35 days. | Enabled by default, same as source. <sup>2</sup> | Not enabled by default. Retention is up to 10 years. |
| **Azure storage**  | Geo-redundant by default. You can optionally configure zone-redundant or locally redundant storage. | Available when PITR backup storage redundancy is set to geo-redundant. Not available when PITR backup storage is zone-redundant or locally redundant. | Geo-redundant by default. You can configure zone-redundant or locally redundant storage. |
| **Configure backups as [immutable](/azure/storage/blobs/immutable-storage-overview)** | Not supported | Not supported | Not supported | 
| **Update policy**<sup>3</sup> | Must match, or upgrade | Must match, or upgrade | Must match, or upgrade | 
| **Restoring a new database in the same region** | Supported | Supported | Supported |
| **Restoring a new database in another region** | Not supported | Supported in any Azure region | Supported in any Azure region |
| **Restoring a new database in another subscription** | Supported  | Not supported <sup>4</sup> | Not supported <sup>4</sup> |
| **Restoring via Azure portal**|Yes|Yes|Yes|
| **Restoring via PowerShell** |Yes|Yes|Yes|
| **Restoring via Azure CLI** |Yes|Yes|Yes|

<sup>1</sup> For business-critical applications that require large databases and must ensure business continuity, see [failover groups](failover-group-sql-mi.md).   
<sup>2</sup> All PITR backups are stored on geo-redundant storage by default, meaning geo-restore is enabled by default.   
<sup>3</sup> Database backups taken from instances configured with the SQL Server 2022 [update policy](update-policy.md) can be restored to instances configured with either the SQL Server 2022 or Always-up-to-date update policy. Database backups taken from instances configured with the Always-up-to-date update policy can only be restored to instances also configured with the Always-up-to-date update policy.   
<sup>4</sup> The workaround is to restore to a new server and use Resource Move to move the server to another subscription.   


## Restore a database from backup

To perform a restore, see [Restore a database from backups](recovery-using-backups.md). You can try backup configuration and restore operations by using the following examples.

| Operation | Azure portal | Azure CLI | Azure PowerShell |
|---|---|---|---|
| **Change backup retention** | [SQL Database](../database/automated-backups-change-settings.md?tabs=azure-portal#change-short-term-retention-policy) / [SQL Managed Instance](automated-backups-change-settings.md?tabs=azure-portal#change-short-term-retention-policy) | [SQL Database](../database/automated-backups-change-settings.md?tabs=azure-cli#change-short-term-retention-policy) / [SQL Managed Instance](automated-backups-change-settings.md?tabs=azure-cli#change-short-term-retention-policy) | [SQL Database](../database/automated-backups-change-settings.md?tabs=powershell#change-short-term-retention-policy) / [SQL Managed Instance](automated-backups-change-settings.md?tabs=powershell#change-short-term-retention-policy) |
| **Change long-term backup retention** | [SQL Database](../database/long-term-backup-retention-configure.md#create-long-term-retention-policies) / [SQL Managed Instance](long-term-backup-retention-configure.md) | [SQL Database](../database/long-term-backup-retention-configure.md) / [SQL Managed Instance](long-term-backup-retention-configure.md) | [SQL Database](../database/long-term-backup-retention-configure.md) / [SQL Managed Instance](long-term-backup-retention-configure.md)  |
| **Restore a database from a point in time** | [SQL Database](../database/recovery-using-backups.md#point-in-time-restore) / [SQL Managed Instance](point-in-time-restore.md) | [SQL Database](/cli/azure/sql/db#az-sql-db-restore) / [SQL Managed Instance](/cli/azure/sql/midb#az-sql-midb-restore) | [SQL Database](/powershell/module/az.sql/restore-azsqldatabase) / [SQL Managed Instance](/powershell/module/az.sql/restore-azsqlinstancedatabase) |
| **Restore a deleted database** | [SQL Database](../database/recovery-using-backups.md) / [SQL Managed Instance](point-in-time-restore.md#restore-a-deleted-database) | [SQL Database](../database/long-term-backup-retention-configure.md#restore-from-ltr-backups) / [SQL Managed Instance](long-term-backup-retention-configure.md#restore-from-ltr-backups) | [SQL Database](/powershell/module/az.sql/get-azsqldeleteddatabasebackup) / [SQL Managed Instance](/powershell/module/az.sql/get-azsqldeletedinstancedatabasebackup)|
| **Restore a database from Azure Blob Storage** |  |  | [SQL Managed Instance](restore-sample-database-quickstart.md) |


## Automatic backups schedule

Azure SQL Managed Instance automatically manages backups by creating full, differential, and transaction log backups. This process is governed by an internal schedule.

### Initial backup
- **New databases**: Immediately after a database is created, restored, or undergoes backup redundancy changes, the first full backup is initiated. This backup typically completes within 30 minutes, though it may take longer for larger databases.

- **Restored databases**: The duration of the initial backup for restored databases varies and depends on the database size. Restored databases or database copies, which are often larger, may require more time for the initial backup.

> [!IMPORTANT]
> The first full backup for a *new* database takes priority over other database backups, so it's the first backup taken during the first full backup window. If the full backup window is already active and other databases are being backed up, the first full backup for the new database is taken immediately after the full back up of another database completes.

### Scheduled full backups
- **Weekly Schedule**: The system sets a weekly full backup window for the entire instance.
- **Full Backup Window**: This is a designated period when full backups are performed. While the system aims to complete full backups within this window, if necessary, the backup may continue beyond the scheduled time until it completes.
- **Adaptive Scheduling**: The backup algorithm evaluates the impact of the backup window on the workload approximately once a week, using CPU usage and I/O throughput as indicators. Depending on the previous week's workload, the full backup window may be adjusted.
- **User Configuration**: It's crucial to note that users **cannot** modify or disable the backup schedule.

> [!IMPORTANT]
> For a new, restored, or copied database, the point-in-time restore (PITR) capability becomes available after the initial transaction log backup completes following the initial full backup.

## Backup storage consumption

With SQL Server backup and restore technology, restoring a database to a point in time requires an uninterrupted backup chain. That chain consists of one full backup, optionally one differential backup, and one or more transaction log backups.

Azure SQL Managed Instance backup schedules include one full backup every week. To provide PITR within the entire retention period, the system must store additional full, differential, and transaction log backups for up to a week longer than the configured retention period.

In other words, for any point in time during the retention period, there must be a full backup that's older than the oldest time of the retention period. There must also be an uninterrupted chain of differential and transaction log backups from that full backup until the next full backup.

Backups that are no longer needed to provide PITR functionality are automatically deleted. Because differential backups and log backups require an earlier full backup to be restorable, all three backup types are purged together in weekly sets.

For all databases, including [TDE-encrypted](../database/transparent-data-encryption-tde-overview.md) databases, all full and differential backups are compressed, to reduce backup storage compression and costs. Average backup compression ratio is 3 to 4 times. However, it can be significantly lower or higher depending on the nature of the data and whether data compression is used in the database.

> [!IMPORTANT]
> For TDE-encrypted databases, log backups files are not compressed for performance reasons. Log backups for non-TDE-encrypted databases are compressed.

Azure SQL Managed Instance computes your total used backup storage as a cumulative value. Every hour, this value is reported to the Azure billing pipeline. The pipeline is responsible for aggregating this hourly usage to calculate your consumption at the end of each month. After the database is deleted, consumption decreases as backups age out and are deleted. After all backups are deleted and PITR is no longer possible, billing stops.

> [!IMPORTANT]
> Backups for a deleted database are kept for point-in-time restore (PITR), which can increase storage costs as backups are kept even though the database is deleted. To reduce costs, you can set the backup retention period to 0 days, but only for deleted databases. For regular databases, the minimum retention period is 1 day.

### Fine-tune backup storage consumption

Backup storage consumption up to the maximum data size for a database isn't charged. Excess backup storage consumption depends on the workload and maximum size of the individual databases. Consider some of the following tuning techniques to reduce your backup storage consumption:

- Reduce the database [backup retention period](automated-backups-change-settings.md#change-short-term-retention-policy) to the minimum for your needs.
- Avoid doing large write operations, like index rebuilds, more frequently than you need to.
- For large data load operations, consider using [clustered columnstore indexes](/sql/relational-databases/indexes/columnstore-indexes-overview) and following related [best practices](/sql/relational-databases/indexes/columnstore-indexes-data-loading-guidance). Also consider reducing the number of nonclustered indexes.
- In the General Purpose service tier, the provisioned data storage is less expensive than the price of the backup storage. If you have continually high excess backup storage costs, you might consider increasing data storage to save on the backup storage.
- Use `tempdb` instead of permanent tables in your application logic for storing temporary results or transient data.
- Use locally redundant backup storage whenever possible (for example, dev/test environments).

## Backup retention

Azure SQL Managed Instance provides both short-term and long-term retention of backups. Short-term retention allows PITR within the retention period for the database. Long-term retention provides backups for various compliance requirements.  

### Short-term retention

For all new, restored, and copied databases, Azure SQL Managed Instance retains sufficient backups to allow PITR within the last 7 days by default. [This configuration can be changed](automated-backups-change-settings.md#change-short-term-retention-policy) in the range of 1 to 35 days. The service takes regular full, differential, and log backups to ensure that databases are restorable to any point in time within the retention period that's defined for the database or managed instance.

You can specify your backup storage redundancy option for STR when you create your instance, and then change it later. If you change your backup redundancy option after your instance is created, new backups will use the new redundancy option. Backup copies made with the previous STR redundancy option aren't moved or copied. They're left in the original storage account until the retention period expires. As described in [Backup storage consumption](#backup-storage-consumption), backups stored to enable PITR might be older than the retention period to ensure precise data restore.

If you delete a database, the system keeps backups in the same way that it would for an online database with its specific retention period. However, for a deleted database, the retention period is updated from 1-35 days to 0-35 days, making it possible to delete backups manually. If you need to keep backups for longer than the maximum short-term retention period of 35 days, you can enable [long-term retention](../database/long-term-retention-overview.md).

> [!IMPORTANT]
> If you delete a managed instance, all databases on that managed instance are also deleted and can't be recovered. You can't restore a deleted managed instance. But if you've configured long-term retention for a managed instance, LTR backups are not deleted. You can then use those backups to restore databases to a different managed instance in the same subscription, to a point in time when an LTR backup was taken. To learn more, review [Restore long-term backup](long-term-backup-retention-configure.md#view-backups-and-restore-from-a-backup).

### Long-term retention (LTR)

With SQL Managed Instance, you can configure storing full LTR backups for up to 10 years in Azure Blob Storage. After the LTR policy is configured, full backups are automatically copied to a different storage container.

To meet various compliance requirements, you can select different retention periods for weekly, monthly, and/or yearly full backups. The frequency depends on the policy. For example, setting `W=0, M=1, Y=0` would create a monthly LTR copy. For more information about LTR, see [Long-term retention](../database/long-term-retention-overview.md).

LTR backup storage redundancy in Azure SQL Managed Instance is inherited from the backup storage redundancy used by STR at the time that the LTR policy is defined. You can't change it, even if the STR backup storage redundancy changes in the future.

Storage consumption depends on the selected frequency and retention periods of LTR backups. You can use the [LTR pricing calculator](https://azure.microsoft.com/pricing/calculator/?service=sql-managed-instance) to estimate the cost of LTR storage.

## Backup storage costs

Azure SQL Managed Instance computes your total billable backup storage as a cumulative value across all backup files. Every hour, this value is reported to the Azure billing pipeline. The pipeline aggregates this hourly usage to get your backup storage consumption at the end of each month.

If a database is deleted, backup storage consumption will gradually decrease as older backups age out and are deleted. Because differential backups and log backups require an earlier full backup to be restorable, all three backup types are purged together in weekly sets. After all backups are deleted, billing stops.

The price for backup storage varies. It depends on your chosen backup storage redundancy option and your region. Backup storage is charged based on gigabytes consumed per month, at the same rate for all backups.

Backup storage redundancy affects backup costs in the following way:

- `Locally redundant price = published price`
- `Zone-redundant price = published price x 1.25`
- `Geo-redundant price = published price x 2`
- `Geo-zone-redundant price = published price x 3.4`

For pricing, review the [Azure SQL Managed Instance pricing](https://azure.microsoft.com/pricing/details/azure-sql/sql-managed-instance/single/) page.

> [!NOTE]
> An Azure invoice shows only the excess backup storage consumption, not the entire backup storage consumption. For example, in a hypothetical scenario, if you have provisioned 4 TB of data storage, you'll get 4 TB of free backup storage space. If you use a total of 5.8 TB of backup storage space, the Azure invoice shows only 1.8 TB, because you're charged only for excess backup storage that you've used.

For managed instances, the total size of billable backup storage is aggregated at the instance level and is calculated as follows:

`Total billable backup storage size = (total size of full backups + total size of differential backups + total size of log backups) – maximum instance data storage`

Total billable backup storage, if any, is charged in gigabytes per month for each region, according to the rate of the backup storage redundancy that you've used. Backup storage consumption depends on the workload and size of individual databases and managed instances. Heavily modified databases have larger differential and log backups, because the size of these backups is proportional to the amount of changed data. Therefore, such databases will have higher backup charges.

As a simplified example, assume that a database has accumulated 744 GB of backup storage and that this amount stays constant throughout an entire month because the database is completely idle. To convert this cumulative storage consumption to hourly usage, divide it by 744.0 (31 days per month times 24 hours per day). SQL Managed Instance reports to the Azure billing pipeline that the database consumed 1 GB of PITR backup each hour, at a constant rate. Azure billing aggregates this consumption and show a usage of 744 GB for the entire month. The cost is based on the rate for gigabytes per month in your region.

Here's another example. Suppose the same idle database has its retention increased from 7 days to 14 days in the middle of the month. This increase results in the total backup storage doubling to 1,488 GB. SQL Managed Instance would report 1 GB of usage for hours 1 through 372 (the first half of the month). It would report the usage as 2 GB for hours 373 through 744 (the second half of the month). This usage would be aggregated to a final bill of 1,116 GB per month. Retention costs don't increase immediately. They increase gradually every day, because the backups grow until they reach the maximum retention period of 14 days.

Actual backup billing scenarios are more complex. Because the rate of changes in the database depends on the workload and is variable over time, the size of each differential and log backup will also vary. The hourly consumption of backup storage fluctuates accordingly.

Each differential backup also contains all changes made in the database since the last full backup. So, the total size of all differential backups gradually increases over the course of a week. Then it drops sharply after an older set of full, differential, and log backups ages out.

For example, assume that a heavy write activity, such as index rebuild,  runs just after a full backup is completed. The modifications that the index rebuild makes will then be included:

- In the transaction log backups taken over the duration of the rebuild.
- In the next differential backup.
- In every differential backup taken until the next full backup occurs.

To reduce the size of all differential backups, excessively large differential backups that exceed 750 GB and become equal to 75% of the database size are promoted to full backups, if the last full backup was taken more than 24 hours ago. 

### Monitor costs

To understand backup storage costs, go to **Cost Management + Billing** in the Azure portal. Select **Cost Management**, and then select **Cost analysis**. Select the desired subscription for **Scope**, and then filter for the time period and service that you're interested in as follows:

1. Add a filter for **Service name**.
2. In the dropdown list, select **sql managed instance** for a managed instance.
3. Add another filter for **Meter subcategory**.
4. To monitor PITR backup costs, in the dropdown list, select **managed instance pitr backup storage**. Meters show up only if backup storage consumption exists.

   To monitor LTR backup costs, in the dropdown list, select **sql managed instance - ltr backup storage**. Meters show up only if backup storage consumption exists.

The **Storage** and **compute** subcategories might also interest you, but they're not associated with backup storage costs.

:::image type="content" source="../database/media/automated-backups-overview/check-backup-storage-cost-sql-mi.png" alt-text="Screenshot that shows an analysis of backup storage costs.":::

>[!IMPORTANT]
> Meters are visible only for counters that are currently in use. If a counter is not available, it's likely that the category is not currently being used. For example, managed instance counters won't be present for customers who do not have a managed instance deployed. Likewise, storage counters won't be visible for resources that are not consuming storage. If there is no PITR or LTR backup storage consumption, these meters won't be visible.

## Encrypted backups

If your database is encrypted with TDE, backups are automatically encrypted at rest, including LTR backups. All new databases in Azure SQL are configured with TDE enabled by default. For more information on TDE, see [Transparent data encryption with SQL Managed Instance](/sql/relational-databases/security/encryption/transparent-data-encryption-azure-sql).

Microsoft is fully responsible for keeping and rotating keys for databases with service-managed keys (SMK). Backups, either PITR or LTR, taken from instances that have TDE with SMK enabled can be restored by Microsoft.

Automatic backups stored in Azure-managed storage accounts are automatically encrypted by Azure storage. Learn more about [Azure Storage encryption for data at rest](/azure/storage/common/storage-service-encryption).

## Backup integrity

All database backups are taken with the CHECKSUM option to provide additional backup integrity. Automatic testing of automated database backups by the Azure SQL engineering team isn't currently available for Azure SQL Managed Instance. Schedule test backup restoration and DBCC CHECKDB on your databases in SQL Managed Instance around your workload.

Although the system doesn't verify the integrity of the backups, there's still built-in protection of your backups that alerts Microsoft if there's an issue with the backup service. Additionally, Microsoft supports you if an issue occurs with a backup, such as if a full backup isn't taken, the backup service is stuck, a log backup is out of SLA, or if the backup hardware or software is corrupted.

## Use Azure Policy to enforce backup storage redundancy

If you have data residency requirements that require you to keep all your data in a single Azure region, you might want to enforce zone-redundant or locally redundant backups for your SQL managed instance by using Azure Policy.

Azure Policy is a service that you can use to create, assign, and manage policies that apply rules to Azure resources. Azure Policy helps you to keep these resources compliant with your corporate standards and service-level agreements. For more information, see [Overview of Azure Policy](/azure/governance/policy/overview).

### Built-in backup storage redundancy policies

To enforce data residency requirements at an organizational level, you can assign policies to a subscription by using the [Azure portal](/azure/governance/policy/assign-policy-portal) or [Azure PowerShell](/azure/governance/policy/assign-policy-powershell). For example, if you assign the following built-in policy at the subscription or resource group level, users in the subscription won't be able to create a managed instance with geo-redundant backup storage via the Azure portal or Azure PowerShell: [SQL Managed Instances should avoid using GRS backup redundancy](https://portal.azure.com/#blade/Microsoft_Azure_Policy/PolicyDetailBlade/definitionId/%2Fproviders%2FMicrosoft.Authorization%2FpolicyDefinitions%2Fa9934fd7-29f2-4e6d-ab3d-607ea38e9079).

For a full list of built-in policy definitions for SQL Managed Instance, review the [policy reference](../database/policy-reference.md).

> [!IMPORTANT]
> Azure policies are not enforced when you're creating a database via T-SQL. To enforce data residency when you're creating a database by using T-SQL, [use LOCAL or ZONE as input to the BACKUP_STORAGE_REDUNDANCY parameter in the CREATE DATABASE statement](/sql/t-sql/statements/create-database-transact-sql#create-database-using-zone-redundancy-for-backups).

## Compliance

If the default retention doesn't meet your compliance requirements, you can change the PITR retention period. For more information, see [Change the PITR backup retention period](automated-backups-change-settings.md#change-short-term-retention-policy).

> [!NOTE]
> The [Change automated backup settings](automated-backups-change-settings.md) article provides steps about how to delete personal data from the device or service and can be used to support your obligations under the GDPR. For general information about GDPR, see the [GDPR section of the Microsoft Trust Center](https://www.microsoft.com/trust-center/privacy/gdpr-overview) and the [GDPR section of the Service Trust portal](https://servicetrust.microsoft.com/ViewPage/GDPRGetStarted).

## Next steps

- [Business continuity overview](business-continuity-high-availability-disaster-recover-hadr-overview.md)
- [Manage long-term backup retention by using the Azure portal](long-term-backup-retention-configure.md)
- [Recover using automated database backups](recovery-using-backups.md)
- [Backup storage consumption on SQL Managed Instance explained](https://aka.ms/mi-backup-explained)
- [Fine tuning backup storage costs on SQL Managed Instance](https://aka.ms/mi-backup-tuning)
