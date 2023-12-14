---
title: Manage automated backups from Azure portal
description: Describes how to configure automated backups
author: dnethi
ms.author: dinethi
ms.reviewer: mikeray, randolphwest
ms.date: 11/14/2023
ms.topic: conceptual
ms.custom: ignite-2023, devx-track-azurecli
---

# Manage automated backups - SQL Server enabled by Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

The Azure extension for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] can perform backups automatically for the system and user databases that are part of the Azure Arc-enabled [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance.

This article explains how you can:

- Enable these built-in automated backups
- Configure backup schedule

[!INCLUDE [azure-arc-sql-preview](includes/azure-arc-sql-preview.md)]

Automated backups are disabled by default.

You can enable automated backups through Azure portal or via `az` CLI by setting the retention days property to a value between 1 and 35 days.

### Supported license types

Automated backups are only supported for license types of PAID or PAYG.

## Backup frequency and retention days

Two properties can be configured when you enable automated backups:

- **retention days** - number of days to retain the backup files. Use a number between 1 and 35.
- **backup schedule** - the schedule at which the full, differential and transaction log backups should be performed. Full backups can be configured to run on a daily or weekly basis. Differential backups can be configured to run either every 12 hours or every 24 hours. Transaction log backups can be configured to run in increments of 5 minutes.

Backups can also be configured to run on a **default** schedule which is as follows:

- Full backups: every 7 days
- Differential backups: every 24 hours
- Transaction log backups: every 15 minutes

## Assign permissions

The backup service within the Azure extension for Arc-enabled [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] uses `[NT AUTHORITY\SYSTEM]` account to perform the backups. As such, you need to grant the following permissions to this account.

   > [!NOTE]  
   > This requirement applies to the preview release.

1. Add `[NT AUTHORITY\SYSTEM]` user account to the **dbcreator** server role at the server level. Run the following Transact-SQL to add this account:

   ```sql
   USE master;
   GO
   ALTER SERVER ROLE [dbcreator] ADD MEMBER [NT AUTHORITY\SYSTEM];
   GO
   ```

1. Add `[NT AUTHORITY\SYSTEM]` user account to logins, and make it a member of `[db_backupoperator]` role in `master`, `model`, `msdb`, and each user database.

   For example:

   ```sql
   CREATE USER [NT AUTHORITY\SYSTEM] FOR LOGIN [NT AUTHORITY\SYSTEM];
   GO
   ALTER ROLE [db_backupoperator] ADD MEMBER [NT AUTHORITY\SYSTEM];
   GO
   ```

1. Run the preceding code for each user and system database (except `tempdb`).

## Enable automated backups

After you have assigned permissions, you can enable automated backups.

### [Azure portal](#tab/azure)

To enable automated backups in Azure portal:

1. Disable any existing backup routines.
1. Browse to the Arc-enabled [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] for which you want to enable automated backups.
1. Select **Backups**.
1. Select **Configure policies**.
1. From the configure policies options:
   - Set a value for backup retention days - between 1 and 35.
   - Set a schedule for the full, differential, and transactional log backups.
1. Select **Apply** to enable this configuration.

Once the automated backups are configured, the Arc SQL extension will initiate a backup of all the databases to the default backup location configured.

Once the backups are enabled, a full backup for each user database is initiated right away. The backups are native [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] backups which means all backup history is available in the backup related tables in the `msdb` database. 

### [Azure CLI](#tab/az)

To enable automated backups using `az` CLI:

1. Disable any existing backup routines.
1. After you disable existing backup routines, run the following command:

   a. Add the extension:

   ```azurecli
   --Install the arcdata extension if not already done
   az extension add --name arcdata
   ```

   b. Configure either the default schedule or a custom schedule:

    **Default schedule**

    ```azurecli
    az sql server-arc backups-policy set --name <arc-server-name> --resource-group <resourcegroup> --retention-days <retentiondays>
    ```

    Example:

    ```azurecli
    az sql server-arc backups-policy set --name MyArcServer-SQLServerPROD --resource-group my-rg --retention-days 24
    ```

    **Custom schedule**

    ```azurecli
    az sql server-arc backups-policy set --name <arc-server-name> --resource-group <resourcegroup> --retention-days <retentiondays> --full-backup-days <num of days> --diff-backup-hours <12 or 24 hours> --tlog-backup-mins <num in minutes>
    ```

    Example:

    ```azurecli
    az sql server-arc backups-policy set --name MyArcServer-SQLServerPROD --resource-group my-rg --retention-days 24 --full-backup-days 7 --diff-backup-hours 24 --tlog-backup-mins 30
    ```

---

## View current backup policy

To view the current backup policy for a given Arc-enabled [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], run the following command:

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

## Backup system databases

When the built-in automated backups are enabled on an Arc-enabled [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], the system databases are also backed up into the default backup location. Only full backups are performed for the system databases.

## Considerations

- The backup files are stored in the default backup location.
- To find the default backup location for a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance (on [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later) run:

   ```sql
   SELECT SERVERPROPERTY('InstanceDefaultBackupPath');
   ```

- For [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] versions below 2019, the default backup path is stored in a registry setting. Configure this setting with the extended stored procedure `xp_instance_regwrite` or from SQL Server Management Studio (SSMS). To use SSMS:

  1. Connect to the Arc-enabled SQL Server from SSMS.
  1. Go to **Server properties** > **Database Settings** > **Database default locations**.

- The backups policy is configured at the instance level and applies to all the databases on the instance.
- The value for `--name` should be the name of the Arc-enabled [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], which is usually in the [Servername_SQLservername] format.
- The value for `--retention-days` can be from 0-35.
- A value of `0` for `--retention-days` indicates to not perform automated backups for the instance.
- The backup files are written to the default backup location as configured at the instance level.
- If there are multiple [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances on the same host where the Azure extension for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is installed, you need to turn on automated backups separately for each instance separately.
- If you change the `--retention-days` after the `--backups-policy` is already configured, any change will take effect going forward and isn't retroactively applied.

## Limitations

- Automated backups are currently not supported for Always On failover cluster instances
- The user databases need to be in full recovery model for the backups to be performed. Databases that aren't in full recovery model are not automatically backed up.
- Automated backups are only supported on the primary replica of an Always On availability group.
- Automated backups are only available for licenses with Software Assurance, SQL subscription, or pay-as-you-go. For details, see [Feature availability depending on license type](overview.md#feature-availability-depending-on-license-type).

## Related tasks

- [Restore to a point-in-time](point-in-time-restore.md)
- [View SQL Server databases - Azure Arc](view-databases.md)
- [Recovery Models (SQL Server)](../../relational-databases/backup-restore/recovery-models-sql-server.md)
