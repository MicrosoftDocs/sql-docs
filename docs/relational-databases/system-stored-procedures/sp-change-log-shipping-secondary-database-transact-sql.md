---
title: "sp_change_log_shipping_secondary_database (Transact-SQL)"
description: sp_change_log_shipping_secondary_database changes secondary database settings.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_change_log_shipping_secondary_database"
  - "sp_change_log_shipping_secondary_database_TSQL"
helpviewer_keywords:
  - "sp_change_log_shipping_secondary_database"
dev_langs:
  - "TSQL"
---
# sp_change_log_shipping_secondary_database (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Changes secondary database settings.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_change_log_shipping_secondary_database
    [ @secondary_database = ] N'secondary_database'
    [ , [ @restore_delay = ] restore_delay ]
    [ , [ @restore_all = ] restore_all ]
    [ , [ @restore_mode = ] restore_mode ]
    [ , [ @disconnect_users = ] disconnect_users ]
    [ , [ @block_size = ] block_size ]
    [ , [ @buffer_count = ] buffer_count ]
    [ , [ @max_transfer_size = ] max_transfer_size ]
    [ , [ @restore_threshold = ] restore_threshold ]
    [ , [ @threshold_alert = ] threshold_alert ]
    [ , [ @threshold_alert_enabled = ] threshold_alert_enabled ]
    [ , [ @history_retention_period = ] history_retention_period ]
    [ , [ @ignoreremotemonitor = ] ignoreremotemonitor ]
[ ; ]
```

## Arguments

#### [ @secondary_database = ] N'*secondary_database*'

The database name on the secondary server. *@secondary_database* is **sysname**, with no default.

#### [ @restore_delay = ] *restore_delay*

The amount of time, in minutes, that the secondary server waits before restoring a given backup file. *@restore_delay* is **int**, with a default of `0`, and can't be `NULL`.

#### [ @restore_all = ] *restore_all*

If set to `1`, the secondary server restores all available transaction log backups when the restore job runs. Otherwise, it stops after one file is restored. *@restore_all* is **bit**, and can't be `NULL`.

#### [ @restore_mode = ] *restore_mode*

The restore mode for the secondary database. *@restore_mode* is **bit**, and can't be `NULL`.

- `0` = restore log with `NORECOVERY`.
- `1` = restore log with `STANDBY`.

#### [ @disconnect_users = ] *disconnect_users*

If set to `1`, users are disconnected from the secondary database when a restore operation is performed. *@disconnect_users* is **bit**, with a default of `0`, and can't be `NULL`.

#### [ @block_size = ] *block_size*

The size, in bytes, that is used as the block size for the backup device. *@block_size* is **int**, with a default of `-1`.

#### [ @buffer_count = ] *buffer_count*

The total number of buffers used by the backup or restore operation. *@buffer_count* is **int**, with a default of `-1`.

#### [ @max_transfer_size = ] *max_transfer_size*

The size, in bytes, of the maximum input or output request that is issued by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to the backup device. *@max_transfer_size* is **int**, with a default of `NULL`.

#### [ @restore_threshold = ] *restore_threshold*

The number of minutes allowed to elapse between restore operations before an alert is generated. *@restore_threshold* is **int**, and can't be `NULL`.

#### [ @threshold_alert = ] *threshold_alert*

The alert to be raised when the restore threshold is exceeded. *@threshold_alert* is **int**, with a default of `14421`.

#### [ @threshold_alert_enabled = ] *threshold_alert_enabled*

Specifies whether an alert is raised when *@restore_threshold* is exceeded.

- `1` = enabled
- `0` = disabled.

*@threshold_alert_enabled* is **bit**, and can't be `NULL`.

#### [ @history_retention_period = ] *history_retention_period*

The length of time in minutes in which the history is retained. *@history_retention_period* is **int**, with a default of `1440`.

#### [ @ignoreremotemonitor = ] *ignoreremotemonitor*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sp_change_log_shipping_secondary_database` must be run from the `master` database on the secondary server. This stored procedure performs the following steps:

1. Changes the settings in the `log_shipping_secondary_database` records as necessary.

1. Changes the local monitor record in `log_shipping_monitor_secondary` on the secondary server using supplied arguments, if necessary.

## Permissions

Only members of the **sysadmin** fixed server role can run this procedure.

## Examples

This example illustrates using `sp_change_log_shipping_secondary_database` to update secondary database parameters for the database `LogShipAdventureWorks`.

```sql
EXEC master.dbo.sp_change_log_shipping_secondary_database
    @secondary_database = 'LogShipAdventureWorks',
    @restore_delay = 0,
    @restore_all = 1,
    @restore_mode = 0,
    @disconnect_users = 0,
    @threshold_alert = 14420,
    @threshold_alert_enabled = 1,
    @history_retention_period = 14420;
```

## Related content

- [About log shipping (SQL Server)](../../database-engine/log-shipping/about-log-shipping-sql-server.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
