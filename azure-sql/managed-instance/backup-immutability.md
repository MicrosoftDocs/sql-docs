---
title: LTR backup immutability
titleSuffix: Azure SQL Managed Instance
description: Learn how to enable, lock, verify, and manage time-based immutability and legal hold, both in preview, for long-term retention backups in Azure SQL Managed Instance.
author: dnethi
ms.author: dinethi
ms.reviewer: vanto
ms.date: 11/06/2026
ms.service: azure-sql-managed-instance
ms.subservice: backup-restore
ms.topic: how-to
ai-usage: ai-assisted
---

# LTR backup immutability

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

Backup immutability helps protect long-term retention (LTR) backups for Azure SQL Managed Instance from accidental or malicious modification and deletion. When you lock time-based immutability, affected LTR backups can't be modified or deleted until their configured retention period expires.

Configure time-based immutability at the LTR policy level for future LTR backups. You can enable it in an unlocked state and lock individual backups later, or enable and lock the policy in one operation.

Legal hold immutability is a separate capability for existing LTR backups. Both time-based immutability and legal hold are in preview for SQL Managed Instance. For more information, see [Supplemental Terms of Use for Microsoft Azure Previews](https://azure.microsoft.com/support/legal/preview-supplemental-terms/).

> [!IMPORTANT]
> Locking time-based immutability is irreversible for the affected backups. A locked backup can't be modified, deleted, or have time-based immutability removed before the backup's retention period expires. This protection continues even if the source database or SQL managed instance is deleted.

> [!NOTE]
> Immutable LTR backups don't block deletion of the SQL managed instance. Deleting a SQL managed instance doesn't delete its immutable LTR backups. Backup storage charges continue for the configured retention duration.

## How time-based immutability works

Time-based immutability has two states:

- **Enabled, unlocked**: Time-based immutability is enabled for new LTR backups, but the setting is still reversible for each affected backup. You can later lock the backup or disable time-based immutability on that backup.
- **Enabled, locked**: Time-based immutability can't be removed from affected backups. The backups remain immutable until their retention period expires.

Policy changes apply to future LTR backups:

- Enabling time-based immutability affects LTR backups created after the policy change.
- Locking the policy causes future LTR backups to be created with time-based immutability enabled and locked.
- Disabling time-based immutability at the policy level affects only future LTR backups. It doesn't change existing backups.
- For an existing backup created while the policy was enabled but unlocked, use the backup-level **Lock** or **Disable** action.

The following table summarizes the supported backup-level actions.

| Time-based immutability state | Lock | Disable time-based immutability | Delete backup | Apply legal hold |
| --- | --- | --- | --- | --- |
| Disabled | Not applicable | Not applicable | Allowed, subject to standard LTR rules | Allowed |
| Enabled, unlocked | Allowed | Allowed | Disable time-based immutability first | Allowed |
| Enabled, locked | Not allowed | Not allowed | Not allowed until retention expires | Allowed |

> [!IMPORTANT]
> After you disable time-based immutability on an individual backup, you can't enable it again for that backup.

## Prerequisites

To complete the procedures in this article, you need:

- An active Azure subscription.
- An existing SQL managed instance and managed database.
- An LTR policy configured for the database.
- An existing LTR backup for backup-level lock, disable, verification, or legal hold operations.
- Azure role-based access control (Azure RBAC) permissions to manage the LTR policy and LTR backups. For the required roles, see [Permissions](long-term-backup-retention-configure.md#permissions).
- For command-line procedures, a version of Azure CLI or the Az.Sql PowerShell module that supports SQL Managed Instance LTR backup immutability.

For more information about configuring LTR, see [Manage Azure SQL Managed Instance long-term backup retention](long-term-backup-retention-configure.md).

## Configure time-based immutability in the Azure portal

Use an unlocked policy when you want future LTR backups to have time-based immutability enabled but don't want to make the configuration irreversible yet. Lock the policy to enforce immutability for the configured retention.

1. In the [Azure portal](https://portal.azure.com), go to your SQL managed instance.
1. Under **Data management**, select **Backups**.
1. Select the **Retention policies** tab.
1. Select the database.
1. Select **Configure policies**.
1. In the **Configure policies** pane, configure the long-term retention policy as needed.
1. Select **Enable time-based immutability policy**.
1. To lock the policy, also select **Lock time-based immutability policy**. To keep the policy unlocked, leave this option cleared.
1. Review the confirmation message.
1. Confirm the operation.

To disable time-based immutability for future LTR backups, follow the same steps, but clear **Enable time-based immutability policy** and **Lock time-based immutability policy**. This change affects only future LTR backups.

## Set or remove legal hold in the Azure portal

When you apply legal hold, you can't delete the selected backups until you remove the legal hold, even if their configured retention period expires.

To set or remove legal hold:

1. Go to your SQL managed instance.
1. Under **Data management**, select **Backups**.
1. Select the **Available backups** tab.
1. Locate the database.
1. Select **Manage**.
1. Select one or more backups.
1. Select **Set Legal Hold (Preview)** or **Remove Legal Hold (Preview)**.
1. Review the confirmation message.
1. Confirm the operation.

> [!IMPORTANT]
> If the configured LTR retention period has already expired and legal hold is the only protection keeping the backup available, removing legal hold makes the backup eligible for deletion.

## Use time-based immutability and legal hold together

Time-based immutability and legal hold operate independently.

An LTR backup can have both protections enabled at the same time.

For example:

- A backup can have locked time-based immutability and legal hold.
- The time-based immutability remains effective until the configured retention period expires.
- Legal hold can continue protecting the backup after the configured retention period expires.
- The backup remains immutable as long as either protection is in effect.

If both protections are enabled, the backup becomes eligible for deletion only after:

- The time-based immutability retention period expires.
- Legal hold is removed.

## Verify backup immutability

You can verify immutability at both the policy level and the individual backup level.

### Verify the policy in the Azure portal

1. Go to your SQL managed instance.
1. Select **Backups**.
1. Select **Retention policies**.
1. Select the database.
1. Select **Configure policies**.
1. Review the **LTR Immutability** configuration.

### Verify individual backups with Azure CLI

```azurecli
az sql midb ltr-backup list \
  --subscription "$subscription" \
  --resource-group "$resourceGroup" \
  --location "$location" \
  --managed-instance "$managedInstance" \
  --database "$database"
```

To inspect a specific backup:

```azurecli
az sql midb ltr-backup show \
  --subscription "$subscription" \
  --location "$location" \
  --managed-instance "$managedInstance" \
  --database "$database" \
  --name "$backupName"
```

### Verify the policy with PowerShell

```azurepowershell
Get-AzSqlInstanceDatabaseBackupLongTermRetentionPolicy `
    -ResourceGroupName $ResourceGroupName `
    -InstanceName $InstanceName `
    -DatabaseName $DatabaseName
```

### Verify individual backups with PowerShell

```azurepowershell
Get-AzSqlInstanceDatabaseLongTermRetentionBackup `
    -Location $Location `
    -ResourceGroupName $ResourceGroupName `
    -InstanceName $InstanceName `
    -DatabaseName $DatabaseName
```

## Limitations and considerations

Consider the following behavior before enabling LTR backup immutability:

- Time-based immutability in the LTR policy applies to LTR backups created after the policy setting takes effect.
- Time-based immutability doesn't apply retroactively to existing LTR backups.
- You can apply legal hold to existing LTR backups.
- You can't remove a locked time-based immutability setting before the backup retention period expires.
- Disabling time-based immutability at the policy level changes only future backup behavior. It doesn't unlock or disable existing backups.
- Removing unlocked time-based immutability from an individual backup is irreversible for that backup.
- You can't delete a backup while locked time-based immutability or legal hold is in effect.
- If both time-based immutability and legal hold are present, the backup remains immutable as long as either protection is in effect.
- Immutable LTR backups don't prevent deletion of the SQL managed instance.
- Deleting the source database or SQL managed instance doesn't delete immutable LTR backups.
- You continue to incur backup storage charges for immutable backups during the applicable retention period.
- There's no additional feature charge for enabling time-based immutability or legal hold.
- You can retain LTR backups for up to 10 years. Review the retention configuration carefully before locking time-based immutability because locked backups can't be deleted early to reduce backup storage consumption.
- LTR backup immutability doesn't change point-in-time restore (PITR) backup retention behavior.

## Related content

- [Backup immutability for long-term retention backups in Azure SQL Database](../database/backup-immutability.md)
- [Configure time-based backup immutability for Azure SQL Database](../database/backup-immutability-time-based.md)
- [Configure legal hold backup immutability for Azure SQL Database](../database/backup-immutability-legal-hold.md)
- [Long-term retention backups in Azure SQL Database and Azure SQL Managed Instance](../database/long-term-retention-overview.md)
- [Manage Azure SQL Managed Instance long-term backup retention](long-term-backup-retention-configure.md)
- [Azure CLI: `az sql midb ltr-policy`](/cli/azure/sql/midb/ltr-policy)
- [Azure CLI: `az sql midb ltr-backup`](/cli/azure/sql/midb/ltr-backup)
- [PowerShell: `Set-AzSqlInstanceDatabaseBackupLongTermRetentionPolicy`](/powershell/module/az.sql/set-azsqlinstancedatabasebackuplongtermretentionpolicy)
- [PowerShell: `Get-AzSqlInstanceDatabaseBackupLongTermRetentionPolicy`](/powershell/module/az.sql/get-azsqlinstancedatabasebackuplongtermretentionpolicy)
- [PowerShell: `Get-AzSqlInstanceDatabaseLongTermRetentionBackup`](/powershell/module/az.sql/get-azsqlinstancedatabaselongtermretentionbackup)
