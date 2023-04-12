---
title: Point in time restore 
description: Describes how to configure automated backups and restore to point in time
ms.service: sql
author: dnethi
ms.author: dinethi
ms.reviewer: mikeray
ms.date: 04/10/2023
ms.topic: conceptual
---

# Point in time restore

The Azure extension for SQL Server can perform backups automatically and allows for point in time restore.  

> [!NOTE]
> As a preview feature, the technology presented in this article is subject to [Supplemental Terms of Use for Microsoft Azure Previews](https://azure.microsoft.com/support/legal/preview-supplemental-terms/).
>
> The latest updates are available in the [release notes](release-notes.md).

The backups are performed at the following schedule:

- Full: every 7 days
- Differential: every 24 hours
- Transaction log: every 15 minutes

> [!NOTE]
> Currently, you can't change the schedule.

## Configure automated backups

### Assign permissions

The current backup service within the Azure extension for Arc enabled Server uses `[NT AUTHORITY\SYSTEM]` account to perform the backups. As such, you need to grant the following permissions to this account.

   > [!NOTE]
   > This requirement applies to the preview release.

- Add `[NT AUTHORITY\SYSTEM]` user account to the `dbcreator` server role at the server level. Run the following Transact SQL to add this account:

   ```sql
   USE master
   GO
   ALTER SERVER ROLE [dbcreator] ADD MEMBER [NT AUTHORITY\SYSTEM]
   GO
   ```

  - For each database (system databases such as master, model and msdb, as well as each user database), the `[NT AUTHORITY\SYSTEM]` user account needs to be added into the Logins, and granted ` [db_backupoperator]` role.

   This can be done via the Transact SQL:

   ```sql
   USE [master]
   GO
   CREATE USER [NT AUTHORITY\SYSTEM] FOR LOGIN [NT AUTHORITY\SYSTEM]
   GO
   ALTER ROLE [db_backupoperator] ADD MEMBER [NT AUTHORITY\SYSTEM]
   GO
   ```

  - Run the preceding code for each user and system database (except `tempdb`).

### Configure backups using Azure (az) CLI

Automated backups are off by default when a SQL Server is Arc enabled. To turn on automated backups, run the following command:

```azurecli
--Install the arcdata extension if not already done
az extension add --name arcdata

az sql server-arc backups-policy set --name <arc-server-name> --resource-group <resourcegroup> --retention-days <retentiondays>
```

Exmaple:
```azurecli
az sql server-arc backups-policy set --name MyArcServer-SQLServerPROD --resource-group my-rg --retention-days 24
```

Things to note:
- The setting is configured at the instance level and applies to all the databases on the instance
- The value for `--name` should be the name of the Arc enabled SQL Server, which is usually in the [Servername_SQLservername] format
- The value for `--retention-days` can be from 0-35
- A value of 0 for `--retention-days` indicates no backups will be performed for the instance
- The backup files are written to the default backup location as configured at the instance level. 

The default backup location for SQL Server (SQL 2019 and above) can be verified via:

```sql
SELECT SERVRPROPERTY('InstanceDefaultBackupPath')
```

- If there are multiple SQL Servers on the same host where the Azure extension for SQL Server is installed, you need to turn on automated backups separately for each instance separately.

## View current backup policy

To view the current backup policy for a given Arc enabled SQL Server, run the following command:

```azurecli
az sql server-arc backups-policy show --name <arc-server-name> --resource-group <resourcegroup> 
```

Exmaple:

```azurecli
az sql server-arc backups-policy show --name MyArcServer-SQLServerPROD --resource-group my-rg
```

Output:

```azurecli
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
