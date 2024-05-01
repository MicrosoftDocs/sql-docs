---
title: Manage automated backups 
description: Describes how to configure automated backups
author: AbdullahMSFT
ms.author: amamun 
ms.reviewer: mikeray, randolphwest
ms.date: 3/12/2024
ms.topic: conceptual
ms.custom: ignite-2023, devx-track-azurecli
---

# Manage automated backups - SQL Server enabled by Azure Arc (preview)

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

The Azure extension for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] can perform backups automatically for the system and user databases of the instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] enabled by Azure Arc.

This article explains how you can:

- Enable these built-in automated backups
- Configure backup schedule

[!INCLUDE [azure-arc-sql-preview](includes/azure-arc-sql-preview.md)]

Backup files are stored in the default backup location of the SQL instance.

You can enable automated backups through Azure portal or via `az` CLI.

To enable automated backups, set the retention days to a nonzero value.

### Supported license types

Automated backups are only available for licenses with Software Assurance, SQL subscription, or pay-as-you-go. For details, see [Feature availability depending on license type](overview.md#feature-differentiation).

## Backup frequency and retention days

You can configure two properties for automated backups:

- **retention days** - number of days to retain the backup files. Use a number between 1 and 35. If the backup retention day is set to 0, automated backup is disabled and no backups are taken, even though backup policy is retained.
- **backup schedule** - the schedule at which the full, differential, and transaction log backups should be performed. Depends on backup type:
  - Full backups: Daily or weekly
  - Differential backups: Every 12 hours or every 24 hours
  - Transaction log backups: Increments of 5 minutes.

You can also run backups on a **default** schedule:

- Retention period: 7 days
- Full backups: every 7 days
- Differential backups: every 24 hours
- Transaction log backups: every 5 minutes

## Backup schedule level

You can schedule backups at

- Instance level
- Database level (available from [extension version 1.1.2594.118](release-notes.md#march-12-2024) or later)

If both database and instance level backup schedule is set, database level schedule takes precedence over the instance level backup schedule. If you delete the database level backup schedule, the instance level backup schedule applies.

## Assign permissions

The backup service within the Azure extension for Arc-enabled SQL Server uses [NT AUTHORITY\SYSTEM] account to perform the backups. If you're [operating SQL Server enabled by Arc with least privilege](configure-least-privilege.md), A local Windows account - [NT Service\SQLServerExtension] - performs the backup.

> [!NOTE]
> [!INCLUDE [least-privilege-default](includes/least-privilege-default.md)]

If you use Azure extension for SQL Server [version 1.1.2504.99](release-notes.md#november-14-2023) or later, the necessary permissions are granted to [NT AUTHORITY\SYSTEM] automatically. You don't need to assign permissions manually.

**For earlier extensions only**, follow the below steps to assign permission to [NT AUTHORITY\SYSTEM] account.

   > [!NOTE]  
   > This requirement applies to the preview release.

1. Add `[NT AUTHORITY\SYSTEM]` account to Logins, and make it a member of the **dbcreator** server role at the server level. Run the following Transact-SQL to add this account:

   ```sql
   USE master;
   GO
   CREATE LOGIN [NT AUTHORITY\SYSTEM] FROM WINDOWS WITH DEFAULT_DATABASE = [master];
   GO
   ALTER SERVER ROLE [dbcreator] ADD MEMBER [NT AUTHORITY\SYSTEM];
   GO
   ```

1. Add `[NT AUTHORITY\SYSTEM]` account to Users, and make it a member of the **db_backupoperator** role in `master`, `model`, `msdb`, and each user database.

   For example:

   ```sql
   CREATE USER [NT AUTHORITY\SYSTEM] FOR LOGIN [NT AUTHORITY\SYSTEM];
   GO
   ALTER ROLE [db_backupoperator] ADD MEMBER [NT AUTHORITY\SYSTEM];
   GO
   ```

1. Run the preceding code for each user and system database (except `tempdb`).

## Configure automated backups

Automated backups are disabled by default.

After you assigned permissions, you can schedule automated backups. After the automated backups are configured, the Arc SQL extension initiates a backup to the default backup location.

The backups are native SQL Server backups, so all backup history is available in the backup related tables in the msdb database.

### Instance level

### [Azure portal](#tab/azure)

To enable automated backups in Azure portal:

1. Disable any existing external backup routines.
1. Browse to the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] you want to enable automated backups.
1. Select **Backups**.
1. Select **Configure policies**.
1. Under **Configure policies**:
   - Set a value for backup retention days - between 1 and 35.
   - Set a schedule for the full, differential, and transactional log backups.
1. Select **Apply** to enable this configuration.

Set retention period and frequency to meet business requirements. The retention policy should be greater than the full backup frequency. As a measure of safety, the automated backup process always keeps backups sets of at least one full backup frequency plus the retention days.

### [Azure CLI](#tab/az)

To enable automated backups using `az` CLI:

1. Disable any existing backup routines.
1. If necessary, add the arcdata extension:

   ```azurecli
   az extension add --name arcdata
   ```

1. Configure either the default schedule or a custom schedule:

    **Default schedule**

    ```azurecli
    az sql server-arc backups-policy set --name <arc-server-name> --resource-group <resourcegroup> --default-policy 
    ```

    > [!NOTE]
    > Examples in this article use `<arc-server-name>` to identify the server name, as follows:
    >
    > - Default instance, replace: `<arc-server-name>` with the server name.
    > - Named instance, replace: `<arc-server-name>` with `ServerName_InstanceName`.

    Example:

    ```azurecli
    az sql server-arc backups-policy set --name MyArcServer_SQLServerPROD --resource-group MyResourceGroup --default-policy
    ```

    **Custom schedule**

    ```azurecli
    az sql server-arc backups-policy set --name <arc-server-name> --resource-group <resourcegroup> --retention-days <number of days> --full-backup-days <num of days> --diff-backup-hours <12 or 24 hours> --tlog-backup-mins <number of minutes>
    ```

    Example:

    ```azurecli
    az sql server-arc backups-policy set --name MyArcServer_SQLServerPROD --resource-group MyResourceGroup --retention-days 24 --full-backup-days 7 --diff-backup-hours 24 --tlog-backup-mins 30
    ```

> [!NOTE]
> If the backup retention day is set to 0, automated backup is disabled and no backups are taken.

---

### Database level

### [Azure portal](#tab/azure)

To configure individual custom database level backup in the portal:

1. Select the instance
1. Select the database
1. Under **Data management** on the left
1. Select **Backup (preview) - Configure database backup policies (Preview)**
1. Select **Configure policies**.
1. Under **Configure policies**:
   - Set a value for backup retention days - between 1 and 35.
   - Set a schedule for the full, differential, and transactional log backups.
1. Select **Apply** to enable this configuration.

Set retention period and frequency to meet business requirements. The retention policy should be greater than the full backup frequency. As a measure of safety, the automated backup process always keeps backups sets of at least one full backup frequency plus the retention days.

### [Azure CLI](#tab/az)

To enable automated backups on a database level using az CLI:

1. Disable any existing backup routines.
2. If necessary, add the arcdata extension: 

   ```azurecli
   az extension add --name arcdata 
   ```

1. Configure either the default schedule or a custom schedule: 

    **Default schedule**

    ```azurecli
    az sql db-arc backups-policy set --name <sql-database-name> --server <arc-server-name> --resource-group <resourcegroup> --default-policy 
    ```

    Example:

     ```azurecli
     az sql db-arc backups-policy set --name MyDatabaseName--server MyArcServer_SQLServerPROD --resource-group MyResourceGroup --default-policy 
     ```

     **Custom schedule**

     ```azurecli
     az sql db-arc backups-policy set --name <sql-database-name> --server <arc-server-name> --resource-group <resourcegroup> --retention-days <number of days> --full-backup-days <num of days> --diff-backup-hours <12 or 24 hours> --tlog-backup-mins <number of minutes> 
     ```

     Example:

     ```azurecli
     az sql db-arc backups-policy set --name MyDatabaseName --server MyArcServer_SQLServerPROD --resource-group MyResourceGroup --retention-days 30 --full-backup-days 7 --diff-backup-hours 12 --tlog-backup-mins 10
     ```

---

## Disable automated backup  

If the backup retention day is set to 0, automated backup is disabled and no backups are taken, even though backup policy is retained. Setting the backup retention to a nonzero value enables the policy again.  

This setting applies to both database and instance level backup. If database level backup schedule is disabled, no backups are taken for the database even if instance level backup is scheduled.  

## Delete automated backup  

From the portal for individual database level backup scheduling page, select **Revert backup policy to instance level** to delete the database level backup policy.

To delete instance level backup schedule, you can do it through CLI. Once deleted, no backup is taken either in instance level or database level. You must configure a new backup schedule to take the backup again.  

### Delete Instance Level Policy

```azurecli
az sql server-arc backups-policy delete --name <arc-server-name> --resource-group <resourcegroup> 
```

Example:

```azurecli
az sql server-arc backups-policy delete --name MyArcServer_SQLServerPROD --resource-group MyResourceGroup  
```

### Delete Database Level Policy

```azurecli
az sql db-arc backups-policy delete --name <sql-database-name> --server <arc-server-name> --resource-group <resourcegroup> 
```

Example:

```azurecli
az sql db-arc backups-policy delete --name MyDatabaseName --server MyArcServer_SQLServerPROD --resource-group MyResourceGroup 
```

## View current backup policy

To view the current backup policy for a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], run the following command:

```azurecli
az sql server-arc backups-policy show --name <arc-server-name> --resource-group <resourcegroup>
```

Example:

```azurecli
az sql server-arc backups-policy show --name MyArcServer_SQLServerPROD --resource-group MyResourceGroup
```

Output:

```json
{
  "differentialBackupHours": 24,
  "fullBackupDays": 7,
  "instanceName": "MSSQLSERVER01",
  "retentionPeriodDays": 16,
  "transactionLogBackupMinutes": 5
}
```

## Backup system databases

When the built-in automated backups are enabled on an instance of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] enabled by Azure Arc, the system databases are also backed up into the default backup location. Only full backups are performed for the system databases.

## Considerations

- The backup files are stored at the default backup location as configured at the SQL Server instance level.
- To find the default backup location for a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance (on [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later), run:

   ```sql
   SELECT SERVERPROPERTY('InstanceDefaultBackupPath');
   ```

- For [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] versions below 2019, the default backup path is stored in a registry setting. Configure this setting with the extended stored procedure `xp_instance_regwrite` or from SQL Server Management Studio (SSMS). To use SSMS:

  1. Connect to the Arc-enabled SQL Server from SSMS.
  1. Go to **Server properties** > **Database Settings** > **Database default locations**.

- The backup policy configured at the instance level applies to all the databases on the instance.
- If both database and instance level backup schedules are set, database level takes precedence over the instance level backup schedule. Deleting the database level backup schedule reverts back to instance level backup schedule, if there's any.
- The value for `--name` should be the name of the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] enabled by Azure Arc, which is usually in the `[Servername_SQLservername]` format.
- The value for `--retention-days` can be from 0-35.
- A value of `0` for `--retention-days` indicates to not perform automated backups for the instance or the database.
- If there are multiple [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances on the same host where the Azure extension for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is installed, you need to configure automated backups separately for each instance.
- If you change the `--retention-days` after the `--backups-policy` is already configured, any change takes effect going forward and isn't retroactively applied.

## Limitations

- The user databases need to be in full recovery model for the backups to be performed. Databases that aren't in full recovery model aren't automatically backed up.
- Automated backups are currently not supported for Always On failover cluster instances (FCI).
- Automated backups aren't supported on any instance that hosts an availability group (AG) replica.
- Automated backups are only available for licenses with Software Assurance, SQL subscription, or pay-as-you-go. For details, see [Feature availability depending on license type](overview.md#feature-differentiation).

## Related tasks

- [Restore to a point-in-time](point-in-time-restore.md)
- [View SQL Server databases - Azure Arc](view-databases.md)
- [Recovery Models (SQL Server)](../../relational-databases/backup-restore/recovery-models-sql-server.md)
