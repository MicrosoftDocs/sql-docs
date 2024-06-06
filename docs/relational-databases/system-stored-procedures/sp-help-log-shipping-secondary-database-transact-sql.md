---
title: "sp_help_log_shipping_secondary_database (Transact-SQL)"
description: sp_help_log_shipping_secondary_database retrieves the settings for one or more secondary databases.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_log_shipping_secondary_database"
  - "sp_help_log_shipping_secondary_database_TSQL"
helpviewer_keywords:
  - "sp_help_log_shipping_secondary_database"
dev_langs:
  - "TSQL"
---
# sp_help_log_shipping_secondary_database (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This stored procedure retrieves the settings for one or more secondary databases.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_log_shipping_secondary_database
    [ [ @secondary_database = ] N'secondary_database' ]
    [ , [ @secondary_id = ] 'secondary_id' ]
[ ; ]
```

## Arguments

#### [ @secondary_database = ] N'*secondary_database*'

The name of the secondary database. *@secondary_database* is **sysname**, with a default of `NULL`.

#### [ @secondary_id = ] '*secondary_id*'

The ID for the secondary server in the log shipping configuration. *@secondary_id* is **uniqueidentifier**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Description |
| --- | --- |
| `secondary_id` | The ID for the secondary server in the log shipping configuration. |
| `primary_server` | The name of the primary instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] in the log shipping configuration. |
| `primary_database` | The name of the primary database in the log shipping configuration. |
| `backup_source_directory` | The directory where transaction log backup files from the primary server are stored. |
| `backup_destination_directory` | The directory on the secondary server where backup files are copied to. |
| `file_retention_period` | The length of time, in minutes, that a backup file is retained on the secondary server before being deleted. |
| `copy_job_id` | The ID associated with the copy job on the secondary server. |
| `restore_job_id` | The ID associated with the restore job on the secondary server. |
| `monitor_server` | The name of the instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] being used as a monitor server in the log shipping configuration. |
| `monitor_server_security_mode` | The security mode used to connect to the monitor server.<br /><br />`1` = Windows Authentication.<br />`0` = [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. |
| `secondary_database` | The name of the secondary database in the log shipping configuration. |
| `restore_delay` | The amount of time, in minutes, that the secondary server waits before restoring a given backup file. The default is 0 minutes. |
| `restore_all` | If set to 1, the secondary server restores all available transaction log backups when the restore job runs. Otherwise, it stops after one file is restored. |
| `restore_mode` | The restore mode for the secondary database.<br /><br />`0` = Restore log with `NORECOVERY`.<br />`1` = Restore log with `STANDBY`. |
| `disconnect_users` | If set to 1, users are disconnected from the secondary database when a restore operation is performed. Default = 0. |
| `block_size` | The size in bytes that is used as the block size for the backup device. |
| `buffer_count` | The total number of buffers used by the backup or restore operation. |
| `max_transfer_size` | The size, in bytes, of the maximum input or output request issued by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to the backup device. |
| `restore_threshold` | The number of minutes allowed to elapse between restore operations before an alert is generated. |
| `threshold_alert` | The alert to be raised when the restore threshold is exceeded. |
| `threshold_alert_enabled` | Determines whether restore threshold alerts are enabled.<br /><br />`1` = Enabled.<br />`0` = Disabled. |
| `last_copied_file` | The filename of the last backup file copied to the secondary server. |
| `last_copied_date` | The time and date of the last copy operation to the secondary server. |
| `last_copied_date_utc` | The time and date of the last copy operation to the secondary server, expressed in Coordinated Universal Time. |
| `last_restored_file` | The filename of the last backup file restored to the secondary database. |
| `last_restored_date` | The time and date of the last restore operation on the secondary database. |
| `last_restored_date_utc` | The time and date of the last restore operation on the secondary database, expressed in Coordinated Universal Time. |
| `history_retention_period` | The amount of time in minutes that log shipping history records are retained for a given secondary database, before being deleted. |
| `last_restored_latency` | The amount of time, in minutes, that elapsed between when the log backup was created on the primary and when it was restored on the secondary.<br /><br />The initial value is `NULL`. |

## Remarks

If you include the *@secondary_database* parameter, the result set contains information about that secondary database; if you include the *@secondary_id* parameter, the result set contains information about all secondary databases associated with that secondary ID.

`sp_help_log_shipping_secondary_database` must be run from the `master` database on the secondary server.

## Permissions

Only members of the **sysadmin** fixed server role can run this procedure.

## Related content

- [sp_help_log_shipping_secondary_primary (Transact-SQL)](sp-help-log-shipping-secondary-primary-transact-sql.md)
- [About log shipping (SQL Server)](../../database-engine/log-shipping/about-log-shipping-sql-server.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
