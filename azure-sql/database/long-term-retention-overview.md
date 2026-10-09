---
title: "Long-Term Retention Backups"
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: Learn how Azure SQL Database & Azure SQL Managed Instance support storing full database backups for up to 10 years via the long-term retention policy.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: dinethi, mathoma, randolphwest, strrodic
ms.date: 10/09/2026
ai-usage: ai-assisted
ms.service: azure-sql
ms.subservice: backup-restore
ms.topic: concept-article
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi"
---
# Long-term retention backups - Azure SQL Database and Azure SQL Managed Instance

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

This article provides a conceptual overview of long-term retention (LTR) backups for Azure SQL Database and Azure SQL Managed Instance. Long-term retention can be configured for up to 10 years on backups for Azure SQL Database (including in the Hyperscale service tier) and Azure SQL Managed Instance.

To get started using the long-term retention backup feature, see: 
- [Manage Azure SQL Database long-term backup retention](long-term-backup-retention-configure.md) 
- [Manage Azure SQL Managed Instance long-term backup retention](../managed-instance/long-term-backup-retention-configure.md)

> [!IMPORTANT]
> Some older APIs used for long-term retention (LTR) backup operations are deprecated and no longer supported. Avoid using legacy PowerShell cmdlets such as `Copy-AzSqlDatabaseLongTermRetentionBackup`. Use the supported restore methods described in this article instead.

## How long-term retention works

Many applications have regulatory, compliance, or other business reasons that require you to retain database backups beyond the 1-35 days provided by the short-term retention period of automatic backups. Long-term backup retention (LTR) relies on the full database backups that are automatically created by the Azure SQL service. For more information, see [Automated backups in Azure SQL Database](automated-backups-overview.md?view=azuresql-db&preserve-view=true) or [Automated backups in Azure SQL Managed Instance](../managed-instance/automated-backups-overview.md?view=azuresql-mi&preserve-view=true).

By using the LTR feature, you can store specified full SQL Database and SQL Managed Instance backups in redundant Azure Blob storage with a configurable retention policy of up to 10 years. LTR backups can then be restored as a new database. If an LTR policy is configured, automated backups are copied to different blobs for long-term storage which you can then use to restore your database to a specific point in time. The copy process is a background job that has no performance impact on the database workload. The LTR policy for each database can also specify how frequently the LTR backups are created. 

> [!NOTE]  
> - In Azure SQL Database, you can [configure long-term retention backups as immutable](backup-immutability.md).
> - In Azure SQL Managed Instance, it's not currently possible to configure backups as [immutable](/azure/storage/blobs/immutable-storage-overview). LTR backups are nonmodifiable, but you can delete them through Azure portal, Azure CLI, PowerShell, or REST API. As a workaround in Azure SQL Managed Instance, you can take [copy-only database backups](/sql/relational-databases/backup-restore/copy-only-backups-sql-server?view=azuresqldb-mi-current&preserve-view=true) and retain them in your own Azure Storage account as an immutable file.

To enable LTR, you can define a policy using a combination of four parameters: weekly backup retention (W), monthly backup retention (M), yearly backup retention (Y), and week of the year (WeekOfYear). If you specify W, one backup every week is copied to long-term storage. If you specify M, the first backup of each month is copied to the long-term storage. If you specify Y, one backup during the week specified by WeekOfYear is copied to the long-term storage. If the specified WeekOfYear is in the past when the policy is configured, the first LTR backup is created the following year. Each backup is kept in long-term storage according to the policy parameters that are configured when the LTR backup is created.

Changes to the LTR policy apply only to future backups. For example, if you modify the weekly backup retention (W), monthly backup retention (M), or yearly backup retention (Y), the new retention setting only applies to new backups. The retention of existing backups isn't modified. The LTR policy can be configured for each database in Azure SQL Database and Azure SQL Managed Instance. If you intend to delete old LTR backups before their retention period expires, you can [manually delete the backups](./long-term-backup-retention-configure.md#delete-ltr-backups).

> [!NOTE]
> In both Azure SQL Database and Azure SQL Managed Instance, when you enable an LTR policy for the first time for a database, the most recent full backup from point-in-time-restore (PITR) is copied to long term storage.


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

`W=12 weeks` (84 days), `M=12 months` (365 days), `Y=10 years` (3,650 days), `WeekOfYear=20` (the week after May 13)

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

If you modify this policy and set `W=0` (no weekly backups), the weekly backups are retained until they expire, and then the service only retains the monthly and yearly backups. No future weekly backups are stored under the LTR policy. The storage amount needed to keep these backups reduces accordingly.

> [!IMPORTANT]  
> The timing of individual LTR backups is controlled by Microsoft. You can't manually create an LTR backup or control the timing of the backup creation. After you configure an LTR policy, it might take up to seven days before the first LTR backup shows up on the list of available backups.  
>  
> If you delete a logical server or a SQL managed instance, all databases on that server or managed instance are also deleted. You can't restore a deleted logical server or SQL managed instance. However, if you had configured LTR for a database, LTR backups aren't deleted and can be used to restore databases to a different server or managed instance in the same subscription, to a point in time when an LTR backup was taken.
>  
> Similarly, if you delete a database, LTR backups aren't deleted and are retained for the configured retention period. These backups can be restored to the same server or a different server in the same subscription.

## Geo-replication and long-term backup retention

If you're using active geo-replication or failover groups as your business continuity solution, prepare for eventual failovers and configure the same LTR policy on the secondary database or instance as you have on the primary. Your LTR storage cost doesn't increase, as backups aren't generated from secondaries. Backups are only created after the secondary becomes primary to ensure uninterrupted generation of LTR backups when a failover is triggered and the primary moves to the secondary region.

When the original primary database recovers from an outage that caused the failover, it becomes the new secondary. Therefore, the backup creation won't resume on the new secondary, and the existing LTR policy doesn't take effect until it becomes the primary again.

## Network security perimeter and LTR restore (preview)

> [!NOTE]  
> A [network security perimeter](network-security-perimeter.md) (preview) applies to Azure SQL Database only. Azure SQL Managed Instance isn't an onboarded resource, so this section doesn't apply to it. For the current list, see [Onboarded private link resources](/azure/private-link/network-security-perimeter-concepts#onboarded-private-link-resources).

LTR restore is a platform-managed operation that restores a database from a long-term backup to a target logical server. When either logical server belongs to a perimeter, the restore is subject to the perimeter's access rules.

The service checks outbound access on the source server, when that server still exists, and inbound access on the target server. If the source logical server was deleted, only the target-side inbound check can run.

### Supported configurations

LTR restore works in both transition and enforced mode. In transition mode, restores succeed without access rules. Configure the rules before you switch to enforced mode.

In enforced mode, these configurations are supported:

| Source server | Target server | Access rules required |
| --- | --- | --- |
| In a perimeter | In the same perimeter | None. Access between resources in the same perimeter is always allowed. |
| In a perimeter | In a different perimeter | Outbound on the source, inbound on the target. |
| In a perimeter | Not in a perimeter | Outbound on the source. |
| Not in a perimeter | In a perimeter | Inbound on the target. |
| Deleted | In a perimeter | Inbound on the target. Source-side validation isn't possible. |
| Not in a perimeter | Not in a perimeter | None. |

LTR restore doesn't require a managed identity.

### Configure access rules for an LTR restore

1. [Associate the logical servers that take part in the restore with the perimeter](network-security-perimeter.md#get-started).
1. If the source server is in a perimeter and the target server isn't in the same perimeter, add an outbound rule on the source profile with **Destination type** set to **FQDN** and the target server's fully qualified domain name.
1. If the target server is in a perimeter and the source server isn't in the same perimeter, add an inbound rule on the target profile that allows one of these sources:
   - The subscription ID of the source server.
   - The `SqlManagement` [service tag](/azure/private-link/configure-network-security-perimeter-service-tag).
   - The IP address ranges listed for `SqlManagement` in [Azure IP Ranges and Service Tags - Public Cloud](https://www.microsoft.com/download/details.aspx?id=56519). The restore request comes from the Azure SQL management infrastructure, not from the source server. Microsoft doesn't publish these ranges by region, so allow all of them.
1. With the servers still in transition mode, run a restore and check the [network security perimeter diagnostic logs](/azure/private-link/network-security-perimeter-diagnostic-logs) for unexpected denials.
1. Switch to enforced mode only after the restore succeeds.

### Limitations and behavior differences

- You can't delete a logical server that has LTR backups while its perimeter association is in enforced mode. Immutable LTR backups also block deletion, whether or not the server is in a perimeter. For more information, see [Immutable long-term backups and deleting logical servers](backup-immutability.md#immutable-long-term-backups-and-deleting-logical-servers).
- If you delete a source server while its association is in transition mode, the service can no longer read that server's perimeter metadata, so it can only validate the target side of a restore. Account for this gap if you rely on source-side validation as a control.
- In transition mode, a restore succeeds even when the required rules are missing. In enforced mode, the same restore is blocked.

### Errors when a perimeter blocks a restore

The customer-facing error text for LTR restore isn't final yet, so this article doesn't list error numbers. A blocked restore fails because a required rule is missing.

- If the source server still exists, check the outbound rule on the source and the inbound rule on the target.
- If the source server was deleted, check the inbound rule on the target.

## Configure long-term backup retention

> [!IMPORTANT]
> Some older APIs used for long-term retention (LTR) backup operations are deprecated and no longer supported. Avoid using legacy PowerShell cmdlets such as `Copy-AzSqlDatabaseLongTermRetentionBackup`. Use the supported restore methods described in this article instead.

You can configure long-term backup retention using the Azure portal and PowerShell for Azure SQL Database and Azure SQL Managed Instance. To restore a database from the LTR storage, you can select a specific backup based on its timestamp. The database can be restored to any existing server or managed instance **under the same subscription** as the original database. For a complete list of restore capabilities, limitations and features, see [Restore capabilities and features in Azure SQL Managed Instance](../managed-instance/automated-backups-overview.md#restore-capabilities-and-features).

- [Manage Azure SQL Database long-term backup retention](long-term-backup-retention-configure.md?view=azuresql-db&preserve-view=true).
- [Manage Azure SQL Managed Instance long-term backup retention](../managed-instance/long-term-backup-retention-configure.md?view=azuresql-mi&preserve-view=true).

When a restore request is initiated in the final seven days of the LTR retention period, the LTR backup is only deleted after the restore operation is completed, even if the retention period has expired. 

[!INCLUDE [hyperscale-cross-tier-restore-note](../includes/hyperscale-cross-tier-restore-note.md)]


In Azure SQL Managed Instance, you can use SQL Agent jobs to schedule [copy-only database backups](/sql/relational-databases/backup-restore/copy-only-backups-sql-server?view=azuresqldb-mi-current&preserve-view=true) and move them to your own storage account as an alternative to: 
- Keep backups for longer than 10 years. 
- Keep daily copies of your databases for longer than 35 days.
- Store database backups on immutable storage.

> [!TIP]  
> If you're using LTR backups to meet compliance or other mission-critical requirements, consider conducting periodic recovery drills to verify that LTR backups can be restored, and that the restore results in the expected database state.

## Next step

> [!div class="nextstepaction"]
> [Manage Azure SQL Database long-term backup retention](long-term-backup-retention-configure.md?view=azuresql-db&preserve-view=true)

> [!div class="nextstepaction"]
> [Manage Azure SQL Managed Instance long-term backup retention](../managed-instance/long-term-backup-retention-configure.md?view=azuresql-mi&preserve-view=true)

## Related content

Because database backups protect data from accidental corruption or deletion, they're an essential part of any business continuity and disaster recovery strategy.

- [Business continuity overview for Azure SQL Database](business-continuity-high-availability-disaster-recover-hadr-overview.md?view=azuresql-db&preserve-view=true)
- [Business continuity overview for Azure SQL Managed Instance](../managed-instance/business-continuity-high-availability-disaster-recover-hadr-overview.md?view=azuresql-mi&preserve-view=true)
- [Automated backups in Azure SQL Database](automated-backups-overview.md?view=azuresql-db&preserve-view=true)
- [Automated backups in Azure SQL Managed Instance](../managed-instance/automated-backups-overview.md?view=azuresql-mi&preserve-view=true)
- [Modifiable configuration reference for Azure SQL Database](modifiable-configuration-reference.md)
- [Network security perimeter for Azure SQL Database (preview)](network-security-perimeter.md)
