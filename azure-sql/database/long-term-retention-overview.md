---
title: "Long-term backup retention"
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: Learn how Azure SQL Database & Azure SQL Managed Instance support storing full database backups for up to 10 years via the long-term retention policy.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dinethi, mathoma, randolphwest
ms.date: 06/18/2024
ms.service: azure-sql
ms.subservice: backup-restore
ms.topic: conceptual
monikerRange: "=azuresql||=azuresql-db||=azuresql-mi"
---
# Long-term retention - Azure SQL Database and Azure SQL Managed Instance
[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

This article provides a conceptual overview of long-term retention of backups for Azure SQL Database and Azure SQL Managed Instance. Long-term retention can be configured for up to 10 years on backups for Azure SQL Database (including in the Hyperscale service tier), and Azure SQL Managed Instance.

To get started, see configure long-term backup retention for [Azure SQL Database](long-term-backup-retention-configure.md) and [Azure SQL Managed Instance](../managed-instance/long-term-backup-retention-configure.md).

## How long-term retention works

Many applications have regulatory, compliance, or other business reasons that require you to retain database backups beyond the 1-35 days provided by the short-term retention period of automatic backups. Long-term backup retention (LTR) relies on the full database backups that are automatically created by the Azure SQL service. For more information, see automated backups in [Azure SQL Database](automated-backups-overview.md?view=azuresql-db&preserve-view=true) or [Azure SQL Managed Instance](../managed-instance/automated-backups-overview.md?view=azuresql-mi&preserve-view=true).

By using the LTR feature, you can store specified full SQL Database and SQL Managed Instance backups in redundant Azure Blob storage with a configurable retention policy of up to 10 years. LTR backups can then be restored as a new database. If an LTR policy is configured, automated backups are copied to different blobs for long-term storage which you can then use to restore your database to a specific point in time. The copy is a background job that has no performance impact on the database workload. The LTR policy for each database in SQL Database can also specify how frequently the LTR backups are created. 

> [!NOTE]  
> - It's not currently possible to configure backups of Azure SQL Database and Azure SQL Managed Instance as [immutable](/azure/storage/blobs/immutable-storage-overview). LTR backups are non-modifiable, but you can be delete them through Azure portal, Azure CLI, PowerShell or REST API. For more information, see [Configure LTR backups](../managed-instance/long-term-backup-retention-configure.md).
> - In Azure SQL Managed Instance, use SQL Agent jobs to schedule [copy-only database backups](/sql/relational-databases/backup-restore/copy-only-backups-sql-server?view=azuresqldb-mi-current&preserve-view=true) and keep them on your own storage account. This could be an alternative to LTR functionality that can keep your backups up to 10 years.

To enable LTR, you can define a policy using a combination of four parameters: weekly backup retention (W), monthly backup retention (M), yearly backup retention (Y), and week of the year (WeekOfYear). If you specify W, one backup every week is copied to long-term storage. If you specify M, the first backup of each month is copied to the long-term storage. If you specify Y, one backup during the week specified by WeekOfYear is copied to the long-term storage. If the specified WeekOfYear is in the past when the policy is configured, the first LTR backup is created the following year. Each backup is kept in long-term storage according to the policy parameters that are configured when the LTR backup is created.

Any change to the LTR policy applies _only to future backups_. For example, if weekly backup retention (W), monthly backup retention (M), or yearly backup retention (Y) is modified, the new retention setting will only apply to new backups. The retention of existing backups will not be modified. If your intention is to delete old LTR backups before their retention period expires, you will need to [manually delete the backups](./long-term-backup-retention-configure.md#delete-ltr-backups).

Examples of the LTR policy:

- `W=0, M=0, Y=5, WeekOfYear=3`

  The third full backup of each year is kept for five years.

- `W=0, M=3, Y=0`

  The first full backup of each month is kept for three months.

- `W=12, M=0, Y=0`

  Each weekly full backup is kept for 12 weeks.

- `W=6, M=12, Y=10, WeekOfYear=20`

  Each weekly full backup is kept for six weeks. Except the first full backup of each month, which is kept for 12 months. Except the full backup taken on the 20th week of the year, which is kept for 10 years.

The following table illustrates the cadence and expiration of the long-term backups for the following policy:

`W=12 weeks` (84 days), `M=12 months` (365 days), `Y=10 years` (3650 days), `WeekOfYear=20` (the week after May 13)

The following dates are in ISO 8601 (`YYYY-MM-DD`).

|**PITR backup to LTR**|**Expiration W**|**Expiration M**|**Expiration Y**|
|:--|:--|:--|:--|
|**2018-03-07**| | 2019-03-02 | |
|2018-03-14 | 2018-06-06 | | |
|2018-03-21 | 2018-06-13 | | |
|2018-03-28 | 2018-06-20 | | |
|2018-04-04 | | 2019-03-30 | |
|2018-04-11 | 2018-07-04 | | |
|2018-04-18 | 2018-07-11 | | |
|2018-04-25 | 2018-07-18 | | |
|2018-05-02 | | 2019-04-27 | |
|2018-05-09 | 2018-08-01 | | |
|2018-05-16 | | | 2028-05-13 |
|2018-05-23 | 2018-08-15 | | |
|2018-05-30 | 2018-08-22 | | |
|2018-06-06 | | 2019-06-01 | |
|2018-06-13 | 2018-09-05 | | |
|2018-06-20 | 2018-09-12 | | |
|2018-06-27 | 2018-09-19 | | |
|2018-07-04 | | 2019-06-29 | |
|2018-07-11 | 2018-10-03 | | |
|2018-07-18 | 2018-10-10 | | |
|2018-07-25 | 2018-10-17 | | |
|2018-08-01 | | 2019-07-27 | |
|2018-08-08 | 2018-10-31 | | |
|2018-08-15 | 2018-11-07 | | |
|2018-08-22 | 2018-11-14 | | |
|2018-08-29 | 2018-11-21 | | |

If you modify the above policy and set `W=0` (no weekly backups), the service only retains the monthly and yearly backups. No weekly backups are stored under the LTR policy. The storage amount needed to keep these backups reduces accordingly.

> [!IMPORTANT]  
> The timing of individual LTR backups is controlled by Azure SQL Database. You cannot manually create an LTR backup or control the timing of the backup creation. After configuring an LTR policy, it might take up to 7 days before the first LTR backup will show up on the list of available backups.  
>  
> If you delete a logical server or a managed instance, all databases on that server or managed instance are also deleted and can't be recovered. You can't restore a deleted server or managed instance. However, if you had configured LTR for a database or managed instance, LTR backups are not deleted, and they can be used to restore databases on a different server or managed instance in the same subscription, to a point in time when an LTR backup was taken.
>  
> Similarly, if you delete a database, LTR backups are not deleted and are retained for the configured retention period. These backups can be restored to the same server or a different server in the same subscription.

## Geo-replication and long-term backup retention

If you're using active geo-replication or failover groups as your business continuity solution, you should prepare for eventual failovers and configure the same LTR policy on the secondary database or instance. Your LTR storage cost doesn't increase, as backups aren't generated from the secondaries. The backups are only created when the secondary becomes primary. It ensures noninterrupted generation of the LTR backups when the failover is triggered and the primary moves to the secondary region.

> [!NOTE]  
> When the original primary database recovers from an outage that caused the failover, it becomes a new secondary. Therefore, the backup creation will not resume, and the existing LTR policy doesn't take effect until it becomes the primary again.

## Configure long-term backup retention

You can configure long-term backup retention using the Azure portal and PowerShell for Azure SQL Database and Azure SQL Managed Instance. To restore a database from the LTR storage, you can select a specific backup based on its timestamp. The database can be restored to any existing server or managed instance under the same subscription as the original database.

To learn how to configure long-term retention or restore a database from backup for SQL Database using the Azure portal or PowerShell, see [Manage Azure SQL Database long-term backup retention](long-term-backup-retention-configure.md?view=azuresql-db&preserve-view=true).

To learn how to configure long-term retention or restore a database from backup for SQL Managed Instance using the Azure portal or PowerShell, see [Manage Azure SQL Managed Instance long-term backup retention](../managed-instance/long-term-backup-retention-configure.md?view=azuresql-mi&preserve-view=true).

When a restore request is initiated in the final 7 days of the LTR retention period, Azure will automatically extend the expiration date of all backups +7 days, to prevent an LTR backup from expiring during the restore.

> [!NOTE]  
> If you are using LTR backups to meet compliance or other mission-critical requirements, consider conducting periodic recovery drills to verify that LTR backups can be restored, and that the restore results in the expected database state.

## Related content

Because database backups protect data from accidental corruption or deletion, they're an essential part of any business continuity and disaster recovery strategy.

- [Business continuity overview for Azure SQL Database](business-continuity-high-availability-disaster-recover-hadr-overview.md?view=azuresql-db&preserve-view=true)
- [Business continuity overview for Azure SQL Managed Instance](../managed-instance/business-continuity-high-availability-disaster-recover-hadr-overview.md?view=azuresql-mi&preserve-view=true)
- [Automated backups in Azure SQL Database](automated-backups-overview.md?view=azuresql-db&preserve-view=true)
- [Automated backups in Azure SQL Managed Instance](../managed-instance/automated-backups-overview.md?view=azuresql-mi&preserve-view=true)

For a tutorial on configuring and managing LTR backups, visit:

- [Manage Azure SQL Database long-term backup retention](long-term-backup-retention-configure.md?view=azuresql-db&preserve-view=true)
- [Manage Azure SQL Managed Instance long-term backup retention](../managed-instance/long-term-backup-retention-configure.md?view=azuresql-mi&preserve-view=true)
