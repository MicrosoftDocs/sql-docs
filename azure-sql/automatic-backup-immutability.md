---
title: Automatic backup immutability for Azure SQL Database and Azure SQL Managed Instance
description: Learn about default PITR backup immutability and how to permanently lock immutability to the configured retention period in Azure SQL Database and Azure SQL Managed Instance.
author: dnethi
ms.author: dinethi
ms.reviewer: vanto
ms.date: 10/28/2026
ms.service: azure-sql
ms.subservice: backup-restore
ms.topic: concept-article
---

# Automatic backup immutability for Azure SQL Database and Azure SQL Managed Instance

Azure SQL Database and Azure SQL Managed Instance protect automatic point-in-time restore (PITR) backups with immutability. Immutable backups can't be modified or deleted during their immutability period, helping protect recovery points against accidental deletion, malicious activity, and compromised administrative credentials.

There are two levels of protection:

- **Default immutability [Generally Available]:** The service automatically protects up to the most recent seven days of PITR backups. No configuration is required, and this default protection is included at no extra cost.
- **Locked immutability aligned with PITR retention [Preview]:** You can optionally lock immutability to the database's configured PITR backup retention period. This extends immutability across the configured retention period, including retention periods longer than seven days.

> [!NOTE]  
> To review the legal terms that apply to Azure features that are in beta, preview, or otherwise not yet released into general availability, see [supplemental terms of use](https://azure.microsoft.com/support/legal/preview-supplemental-terms/). Azure SQL Database provides previews to give you a chance to evaluate and [share feedback with the product group](https://feedback.azure.com/d365community/forum/ef2b2b38-2f25-ec11-b6e6-000d3a4f0f84) on features before they become generally available (GA).

Both capabilities are integrated into the managed backup service. You don't need to create a storage account or manage Azure Storage immutability policies.

> [!IMPORTANT]
> Locking immutability is permanent. After you enable the lock, you can't disable it or reduce the configured PITR retention period. You can only increase the retention period, within the limits supported by your database.
>
> The lock doesn't prevent database deletion. If you delete the database, immutability continues to protect its retained backups for the applicable configured duration. Backup immutability isn't a resource deletion lock.

## Compare the protection options

PITR retention determines how long backups are kept for point-in-time restore. Immutability determines how long those backups are protected against modification or deletion. The optional lock aligns these two protections.

| Characteristic | Default immutability | Locked immutability aligned with PITR retention |
| --- | --- | --- |
| Enablement | Automatic; no configuration required | Optional; enable for the database |
| Immutability period | Up to the most recent seven days of PITR backups | The configured PITR backup retention period |
| Retention longer than seven days | Configuring a longer PITR retention period alone doesn't extend default immutability to the full retention period | Immutability covers the full configured PITR retention period |
| Changes to PITR retention | Existing retention configuration rules apply | Retention can only be increased, not reduced |
| Disable the optional lock | Not applicable | Not allowed after the lock is enabled |
| Database deletion | Doesn't prevent database deletion | Doesn't prevent database deletion; retained backups remain protected for their applicable immutability period |

This article describes immutability for PITR backups, which use the short-term retention policy. The PITR immutability lock doesn't configure long-term retention (LTR). For LTR backup immutability in Azure SQL Database, see [Backup immutability for long-term retention backups](database/backup-immutability.md).

## Default PITR backup immutability

For supported databases, Azure SQL automatically applies immutability protection to up to the most recent seven days of PITR backups. You don't need to enable a setting, configure a policy, or lock retention to receive this protection.

Default immutability doesn't change your configured PITR retention period or create additional restore history. For example, configuring 35 days of PITR retention doesn't, by itself, make all 35 days immutable. Without the optional lock, default immutability protects up to the most recent seven days.

Existing backup and restore workflows continue to work. To extend immutability to the full configured retention period, enable the optional lock.

## Lock immutability to PITR retention

Enable **Lock immutability to configured PITR backup retention period** when you need immutability to match your configured PITR retention. This option is especially useful when your retention period is longer than seven days.

For example, if you configure 35 days of PITR retention and enable the lock, immutability aligns with the 35-day retention period rather than only the default period of up to seven days.

After you enable the lock:

- The lock remains enabled permanently.
- You can't reduce PITR retention, including through the portal, Azure CLI, or PowerShell.
- You can increase PITR retention up to the maximum supported for your database. The lock then covers the longer retention period, and you can't reduce it later.
- You can still delete the database. Deleting it doesn't remove immutability protection from its retained backups.

For example, after locking a 14-day PITR retention period, you can increase it to 35 days. You can't reduce it to seven days, or return to 14 days after increasing it to 35 days.

> [!NOTE]
> Increasing PITR retention doesn't recreate backups that have already expired or immediately provide older restore points. The available restore history grows over time as the service retains backups for longer.

### Plan before enabling the lock

Before you enable locked immutability:

- Choose a PITR retention period that meets your recovery, governance, and data retention requirements.
- Confirm that you can retain backups for that period without needing to shorten it later.
- Review the applicable backup storage costs. Default immutability is included at no extra cost; backup retention and storage remain subject to the applicable service pricing.
- Confirm that your service tier and, for SQL Managed Instance, your update policy support backup immutability. See [Limitations](#limitations).
- Use an account with permission to update the database's short-term backup retention policy.

The following examples set PITR retention to **35 days** and permanently enable the lock. Replace the example resource names and choose a supported retention period before running a command. To lock the current retention period without increasing it, supply the database's existing retention value instead.

<!-- Publication check: The LockImmutability parameters below come from the supplied SQL Database and Managed Instance command slides. Confirm minimum Azure CLI and Az.Sql versions, the PowerShell Boolean parameter type, rollout status, and any lock-specific pricing before publication. Public command references reviewed on 2026-10-28 don't yet document these parameters. -->

## Enable locked immutability

Choose the Azure portal, Azure CLI, or Azure PowerShell. The setting applies to the selected database's PITR backup retention policy.

> [!WARNING]
> Saving the portal configuration or running an enablement command permanently locks immutability to the configured PITR retention. There's no unlock operation. Don't enable the setting as a temporary test on a database whose retention you might need to reduce.

### Azure portal

#### Azure SQL Database

1. In the [Azure portal](https://portal.azure.com), open the SQL logical server that contains the database.
1. Select **Backups**, and then select **Retention policies**.
1. Select the database whose PITR backup retention you want to lock.
1. Select **Configure policies**.
1. Under **Point-in-time restore**, set the number of days to retain PITR backups, for example, **35**.
1. Select **Lock immutability to configured PITR backup retention period**.
1. Review the warning that only extensions to PITR retention are allowed after locking.
1. Apply the changes and confirm the permanent lock if prompted.

#### Azure SQL Managed Instance

1. In the [Azure portal](https://portal.azure.com), open the managed instance that contains the database.
1. Select **Backups**, and then select **Retention policies**.
1. Select the database whose PITR backup retention you want to lock.
1. Select **Configure policies**.
1. Under **Point-in-time restore**, set the PITR backup retention period, for example, **35** days.
1. Select **Lock immutability to configured PITR backup retention period**.
1. Review the warning that the setting is permanent and retention can only be increased.
1. Apply the changes and confirm the permanent lock if prompted.

### Azure CLI

Use an Azure CLI version that supports `--lock-immutability`. Sign in and select the subscription that contains your database before running these commands. The examples use single-line commands so that shell-specific line-continuation characters aren't required.

#### Azure SQL Database

Set PITR retention to 35 days and enable the lock:

```azurecli
az sql db str-policy set --resource-group "myResourceGroup" --server "mySqlServer" --name "myDatabase" --retention-days 35 --lock-immutability true
```

#### Azure SQL Managed Instance

Set PITR retention to 35 days and enable the lock:

```azurecli
az sql midb short-term-retention-policy set --resource-group "myResourceGroup" --mi "myManagedInstance" --name "myDatabase" --retention-days 35 --lock-immutability true
```

The value `true` enables the permanent lock. Although the parameter accepts a Boolean value, specifying `false` isn't an unlock operation and can't reverse an enabled lock.

### Azure PowerShell

Use a version of the `Az.Sql` module that supports `-LockImmutability`. Connect to Azure and select the subscription that contains your database before running these commands.

#### Azure SQL Database

Set PITR retention to 35 days and enable the lock:

```powershell
Set-AzSqlDatabaseBackupShortTermRetentionPolicy `
    -ResourceGroupName "myResourceGroup" `
    -ServerName "mySqlServer" `
    -DatabaseName "myDatabase" `
    -RetentionDays 35 `
    -LockImmutability $true
```

#### Azure SQL Managed Instance

Set PITR retention to 35 days and enable the lock:

```powershell
Set-AzSqlInstanceDatabaseBackupShortTermRetentionPolicy `
    -ResourceGroupName "myResourceGroup" `
    -InstanceName "myManagedInstance" `
    -DatabaseName "myDatabase" `
    -RetentionDays 35 `
    -LockImmutability $true
```

The value `$true` enables the permanent lock. Passing `$false` can't reverse an enabled lock.

### Verify the configuration

After you apply the change, open **Backups** > **Retention policies** in the Azure portal, select the database, and review its configured PITR retention and immutability lock in **Configure policies**.

You can also retrieve the short-term retention policy to check the configured retention:

| Service | Azure CLI | Azure PowerShell |
| --- | --- | --- |
| Azure SQL Database | `az sql db str-policy show --resource-group "myResourceGroup" --server "mySqlServer" --name "myDatabase"` | `Get-AzSqlDatabaseBackupShortTermRetentionPolicy -ResourceGroupName "myResourceGroup" -ServerName "mySqlServer" -DatabaseName "myDatabase"` |
| Azure SQL Managed Instance | `az sql midb short-term-retention-policy show --resource-group "myResourceGroup" --mi "myManagedInstance" --name "myDatabase"` | `Get-AzSqlInstanceDatabaseBackupShortTermRetentionPolicy -ResourceGroupName "myResourceGroup" -InstanceName "myManagedInstance" -DatabaseName "myDatabase"` |

## Increase retention after locking

To extend retention, use the same portal workflow or the applicable retention-policy command with a greater supported retention value. Keep immutability locked. The lock remains permanent, and you can't later reduce retention to the previous value.

For example, if you locked retention at 14 days, you can increase it to 35 days by using the 35-day examples in this article. After you lock retention at 35 days, you can't reduce it to 14 days.

## Database deletion and backup protection

Locked immutability protects backups; it doesn't prevent deletion of the database or act as deletion protection for its parent logical server or managed instance.

If you delete a database with locked immutability enabled, its retained backups remain subject to immutability for the applicable configured duration. Database deletion doesn't unlock those backups or shorten their protected period. The lock doesn't retain backups indefinitely; they remain subject to expiration under the applicable retention policy.

For example, deleting a database with a locked 35-day PITR retention policy doesn't remove the protection from its retained backups. It also doesn't start a new 35-day retention period for every backup.

Backup protection and resource recovery are separate concerns. Follow the service-specific recovery requirements when you restore a deleted database. For Azure SQL logical servers, also consider [soft delete retention (preview)](database/deleted-logical-server-restore.md), which lets you restore a deleted server during its retention period.

For recovery procedures, see:

- [Restore a database from a backup in Azure SQL Database](database/recovery-using-backups.md)
- [Restore a database from a backup in Azure SQL Managed Instance](managed-instance/recovery-using-backups.md)

## Compliance and governance considerations

Backup immutability can support resilience and governance programs by protecting recovery points against modification or deletion. Locking immutability to PITR retention can help when your protection requirements extend beyond the default period of up to seven days.

Azure SQL backup immutability uses Azure Storage's immutable storage capabilities to enforce protection. Azure Storage immutable storage capabilities are validated for scenarios that include the following requirements:

- SEC Rule 17a-4(f)
- CFTC Rule 1.31(d)
- FINRA record-retention requirements

> [!IMPORTANT]
> Compliance requirements vary by regulation, jurisdiction, and implementation. Evaluate your specific legal and regulatory obligations with your compliance and legal teams. Locking PITR immutability doesn't, by itself, establish compliance with a particular regulation.

## Limitations

- Automatic backup immutability isn't currently available for the Hyperscale service tier of Azure SQL Database. As such, if a database is migrated from non-Hyperscale service tier to Hyperscale service tier, backups lose the immutability protection. 
- In Azure SQL Managed Instance, automatic backup immutability is available only to instances configured with the **Always-up-to-date** or **SQL Server 2025** [update policy](managed-instance/update-policy.md), not the **SQL Server 2022** update policy.
- The lock doesn't increase the maximum PITR retention supported by your service tier. Choose a supported retention value; 35 days isn't supported in every tier.
- An enabled lock can't be disabled, and the locked PITR retention period can't be reduced.
- The lock protects PITR backups. It doesn't configure LTR retention or prevent resource deletion.
- In Azure SQL Managed Instance environments with a high volume of database management (CRUD) operations (approximately 5,000 per month), automatic backup immutability might not be applied to backups for some databases.

## Related content

- [Automated backups in Azure SQL Database](database/automated-backups-overview.md)
- [Automated backups in Azure SQL Managed Instance](managed-instance/automated-backups-overview.md)
- [Change automated backup settings for Azure SQL Database](database/automated-backups-change-settings.md)
- [Change automated backup settings for Azure SQL Managed Instance](managed-instance/automated-backups-change-settings.md)
- [Long-term retention backups in Azure SQL Database and Azure SQL Managed Instance](database/long-term-retention-overview.md)
- [Immutable storage for Azure Blob Storage](/azure/storage/blobs/immutable-storage-overview)
