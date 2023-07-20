---
title: Configure automated backup
description: Describes how to configure automated backups
author: dnethi
ms.author: dinethi
ms.reviewer: mikeray, randolphwest
ms.date: 05/10/2023
ms.topic: conceptual
---
# Configure automatic backups

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

The Azure extension for SQL Server can perform backups automatically. This article explains how you can configure these automatic backups.

> [!NOTE]  
> As a preview feature, the technology presented in this article is subject to [Supplemental Terms of Use for Microsoft Azure Previews](https://azure.microsoft.com/support/legal/preview-supplemental-terms/).
>  
> The latest updates are available in the [release notes](release-notes.md).

> [!NOTE]  
> Automated backups is only supported for license types of PAID or PAYG.

The backups are performed at the following schedule:

- Full: every 7 days
- Differential: every 24 hours
- Transaction log: every 15 minutes

> [!NOTE]  
> Currently, the schedule can't be changed.

## Assign permissions

The current backup service within the Azure extension for Arc enabled Server uses `[NT AUTHORITY\SYSTEM]` account to perform the backups. As such, you need to grant the following permissions to this account.

   > [!NOTE]  
   > This requirement applies to the preview release.

1. Add `[NT AUTHORITY\SYSTEM]` user account to the **dbcreator** server role at the server level. Run the following Transact-SQL to add this account:

   ```sql
   USE master;
   GO
   ALTER SERVER ROLE [dbcreator] ADD MEMBER [NT AUTHORITY\SYSTEM];
   GO
   ```

1. Add `[NT AUTHORITY\SYSTEM]` user account to Logins, and make it a member of `[db_backupoperator]` role in `master`, `model`, `msdb`, and each user database.

   For example:

   ```sql
   CREATE USER [NT AUTHORITY\SYSTEM] FOR LOGIN [NT AUTHORITY\SYSTEM];
   GO
   ALTER ROLE [db_backupoperator] ADD MEMBER [NT AUTHORITY\SYSTEM];
   GO
   ```

1. Run the preceding code for each user and system database (except `tempdb`).

## Configure backups using Azure (az) CLI

Use Azure CLI to enable automated backups.

> [!IMPORTANT]  
> Automated backups are disabled by default when a SQL Server is Arc enabled. The default setting prevents automated backups from breaking the backup sequence of any existing backup processes.

To enable automated backups:

1. Disable any existing backup routines.
1. After you disable existing backup routines, run the following command:

   ```azurecli
   --Install the arcdata extension if not already done
   az extension add --name arcdata

   az sql server-arc backups-policy set --name <arc-server-name> --resource-group <resourcegroup> --retention-days <retentiondays>
   ```

   Example:

   ```azurecli
   az sql server-arc backups-policy set --name MyArcServer-SQLServerPROD --resource-group my-rg --retention-days 24
   ```

### Considerations

- The backup files are stored in the default backup location.
- The default backup location for SQL Server (SQL 2019 and above) can be verified via:

```sql
SELECT SERVERPROPERTY('InstanceDefaultBackupPath');
```

- For SQL Server versions below 2019, the default backup path is stored in a registry setting. Configure this setting with the extended stored procedure `xp_instance_regwrite` or from SQL Server Management Studio (SSMS). To use SSMS:

  1. Connect to the Arc enabled SQL Server from SSMS.
  1. Go to **Server properties** > **Database Settings** > **Database default locations**.

- The setting is configured at the instance level and applies to all the databases on the instance.
- The value for `--name` should be the name of the Arc enabled SQL Server, which is usually in the [Servername_SQLservername] format.
- The value for `--retention-days` can be from 0-35.
- A value of 0 for `--retention-days` indicates to not perform automated backups for the instance.
- The backup files are written to the default backup location as configured at the instance level.
- If there are multiple SQL Server instances on the same host where the Azure extension for SQL Server is installed, you need to turn on automated backups separately for each instance separately.
- The user databases need to be in full recovery model for the backups to be performed. Databases that aren't in full recovery model aren't automatically backed up.
- If you change the `--retention-days` after the `--backups-policy` is already configured, any change will take effect going forward and isn't retroactively applied.
- Automated backups are only supported on the primary replica of an Always On availability group.
- Automated backups are only available for licenses with Software Assurance, SQL subscription, or pay-as-you-go. For details, see [Feature availability depending on license type](overview.md#feature-availability-depending-on-license-type).

## View current backup policy

To view the current backup policy for a given Arc enabled SQL Server, run the following command:

```azurecli
az sql server-arc backups-policy show --name <arc-server-name> --resource-group <resourcegroup>
```

Example:

```azurecli
az sql server-arc backups-policy show --name MyArcServer-SQLServerPROD --resource-group my-rg
```

Output:

```json
{
  "DiffBackupHours": 24,
  "FullBackupDays": 7,
  "InstanceName": "SQLServerPROD",
  "RetentionPeriodInDays": 24,
  "TlogBackupMins": 5
}
```

## See also

- [View SQL Server databases - Azure Arc](view-databases.md)
- [Disconnect your SQL Server instances from Azure Arc](delete-from-azure-arc.md)
- [Recovery Models (SQL Server)](../../relational-databases/backup-restore/recovery-models-sql-server.md)
