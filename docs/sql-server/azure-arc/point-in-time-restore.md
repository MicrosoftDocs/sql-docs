---
title: Point in time Restore 
description: Describes how to configure automated backups and restore to point in time
services: azure-arc
ms.service: azure-arc
ms.subservice: azure-arc-data-sqlserver
author: dnethi
ms.author: dinethi
ms.reviewer: mikeray
ms.date: 04/10/2023
ms.topic: conceptual
---

# Point in time restore

The Azure extension for Arc enabled SQL Server ships with a built-in capability to perform backups automatically and allows for point in time restore.  

The backups are performed at the following schedule:

- Full backup every 7 days
- Differential backup every 24 hours
- Transactional log backup every 15 minutes

> [!NOTE]
> At this point, this schedule cannot be changed


[!INCLUDE [azure-arc-data-preview](../../../includes/azure-arc-data-preview.md)]

## Configure automated backups

### Assign permissions

The current backup service within the Azure extension for Arc enabled Server uses `[NT AUTHORITY\SYSTEM]` account to perform the backups. As such, the following permissions need to be granted to this account.

> [!NOTE]
> This requirement will be removed in a future release

- `[NT AUTHORITY\SYSTEM]` user account needs to be added to the `dbcreator` server role at the server level. This can be done via the following T-SQL:

```sql
USE master
GO
ALTER SERVER ROLE [dbcreator] ADD MEMBER [NT AUTHORITY\SYSTEM]
GO
```

- For each database (system databases such as master, model and msdb, as well as each user database), the `[NT AUTHORITY\SYSTEM]` user account needs to be added into the Logins, and granted ` [db_backupoperator]` role. 

This can be done via the T-SQL:

```sql
USE [master]
GO
CREATE USER [NT AUTHORITY\SYSTEM] FOR LOGIN [NT AUTHORITY\SYSTEM]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [NT AUTHORITY\SYSTEM]
GO
```

- Above T-SQL code needs to be run for each user and system database (except Tempdb)

### Configure backups using Azure (az) CLI

Automated backups is turned OFF by default when a SQL Server is Arc enabled. To turn on automated backups, run the following command:

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

- If there are multiple SQL Servers on the same host where the Azure extension for SQL Server is installed, automated backups needs to be turned ON for each instance separately. 

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

