---
title: "sp_help_log_shipping_primary_database (Transact-SQL)"
description: sp_help_log_shipping_primary_database retrieves primary database settings.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_log_shipping_primary_database_TSQL"
  - "sp_help_log_shipping_primary_database"
helpviewer_keywords:
  - "sp_help_log_shipping_primary_database"
dev_langs:
  - "TSQL"
---
# sp_help_log_shipping_primary_database (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Retrieves primary database settings.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_log_shipping_primary_database
    [ [ @database = ] N'database' ]
    [ , [ @primary_id = ] 'primary_id' ]
[ ; ]
```

## Arguments

#### [ @database = ] N'*database*'

The name of the log shipping primary database. *@database* is **sysname**, with a default of `NULL`.

#### [ @primary_id = ] '*primary_id*'

The ID of the primary database for the log shipping configuration. *@primary_id* is **uniqueidentifier**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Description |
| --- | --- |
| `primary_id` | The ID of the primary database for the log shipping configuration. |
| `primary_database` | The name of the primary database in the log shipping configuration. |
| `backup_directory` | The directory where transaction log backup files from the primary server are stored. |
| `backup_share` | The network or UNC path to the backup directory. |
| `backup_retention_period` | The length of time, in minutes, that a log backup file is retained in the backup directory before being deleted. |
| `backup_compression` | Indicates whether the log shipping configuration uses [backup compression](../backup-restore/backup-compression-sql-server.md).<br /><br />`0` = Disabled. Never compress log backups.<br />`1` = Enabled. Always compress log backups.<br />`2` = Use the setting of the [backup compression default](../../database-engine/configure-windows/view-or-configure-the-backup-compression-default-server-configuration-option.md). This is the default value.<br /><br />Backup compression is supported only in [!INCLUDE [ssEnterpriseEd10](../../includes/ssenterpriseed10-md.md)] (or a later version). In other editions, the value is always `2`. |
| `backup_job_id` | The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent job ID associated with the backup job on the primary server. |
| `monitor_server` | The name of the instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] being used as a monitor server in the log shipping configuration. |
| `monitor_server_security_mode` | The security mode used to connect to the monitor server.<br /><br />`1` = Windows Authentication.<br />`0` = [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. |
| `backup_threshold` | The number of minutes allowed to elapse between backup operations before an alert is generated. |
| `threshold_alert` | The alert to be raised when the backup threshold is exceeded. |
| `threshold_alert_enabled` | Determines if backup threshold alerts are enabled.<br /><br />`1` = Enabled.<br />`0` = Disabled. |
| `last_backup_file` | The absolute path of the most recent transaction log backup. |
| `last_backup_date` | The time and date of the last log backup operation. |
| `last_backup_date_utc` | The time and date of the last transaction log backup operation on the primary database, expressed in Coordinated Universal Time. |
| `history_retention_period` | The amount of time, in minutes, that log shipping history records are retained for a given primary database before being deleted. |

## Remarks

`sp_help_log_shipping_primary_database` must be run from the `master` database on the primary server.

## Permissions

Only members of the **sysadmin** fixed server role can run this procedure.

## Examples

This example illustrates using `sp_help_log_shipping_primary_database` to retrieve primary database settings for the database [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)].

```sql
EXEC master.dbo.sp_help_log_shipping_primary_database
    @database = N'AdventureWorks2022';
GO
```

## Related content

- [About log shipping (SQL Server)](../../database-engine/log-shipping/about-log-shipping-sql-server.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
