---
title: "sys.sp_change_log_shipping_primary_database (Transact-SQL)"
description: "Changes the primary database settings."
author: dimitri-furman
ms.author: dfurman
ms.reviewer: vanto
ms.date: 06/19/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_change_log_shipping_primary_database"
  - "sp_change_log_shipping_primary_database_TSQL"
helpviewer_keywords:
  - "sp_change_log_shipping_primary_database"
dev_langs:
  - "TSQL"
---
# sys.sp_change_log_shipping_primary_database (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Changes the primary database settings.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_change_log_shipping_primary_database
    [ @database = ] N'database'
    [ , [ @backup_directory = ] N'backup_directory' ]
    [ , [ @backup_share = ] N'backup_share' ]
    [ , [ @backup_retention_period = ] backup_retention_period ]
    [ , [ @monitor_server_security_mode = ] monitor_server_security_mode ]
    [ , [ @monitor_server_login = ] N'monitor_server_login' ]
    [ , [ @monitor_server_password = ] N'monitor_server_password' ]
    [ , [ @backup_threshold = ] backup_threshold ]
    [ , [ @threshold_alert = ] threshold_alert ]
    [ , [ @threshold_alert_enabled = ] threshold_alert_enabled ]
    [ , [ @history_retention_period = ] history_retention_period ]
    [ , [ @ignoreremotemonitor = ] ignoreremotemonitor ]
    [ , [ @backup_compression = ] backup_compression ]
    [ , [ @monitor_connection_options = ] '<key_value_pairs>;[...]' ]
[ ; ]
```

## Arguments

#### [ @database = ] N'*database*'

The name of the database on the primary server. *@primary_database* is **sysname**, with no default.

#### [ @backup_directory = ] N'*backup_directory*'

The path to the backup folder on the primary server. *@backup_directory* is **nvarchar(500)**, with no default, and can't be `NULL`.

#### [ @backup_share = ] N'*backup_share*'

The network path to the backup directory on the primary server. *@backup_share* is **nvarchar(500)**, with no default, and can't be `NULL`.

#### [ @backup_retention_period = ] *backup_retention_period*

The length of time, in minutes, to retain the log backup file in the backup directory on the primary server. *@backup_retention_period* is **int**, with no default, and can't be `NULL`.

#### [ @monitor_server_security_mode = ] *monitor_server_security_mode*

The security mode used to connect to the monitor server.

- `1` = Windows Authentication
- `0` = SQL Server Authentication

*@monitor_server_security_mode* is **bit** and defaults to `NULL`.

#### [ @monitor_server_login = ] N'*monitor_server_login*'

The username of the account used to access the monitor server.

#### [ @monitor_server_password = ] N'*monitor_server_password*'

The password of the account used to access the monitor server.

#### [ @backup_threshold = ] *backup_threshold*

The length of time, in minutes, after the last backup before a *@threshold_alert* error is raised. *@backup_threshold* is **int**, with a default of 60 minutes.

#### [ @threshold_alert = ] *threshold_alert*

The alert to be raised when the backup threshold is exceeded. *@threshold_alert* is **int** and can't be `NULL`.

#### [ @threshold_alert_enabled = ] *threshold_alert_enabled*

Specifies whether an alert is raised when *@backup_threshold* is exceeded.

- `1`: enabled
- `0`: disabled

*@threshold_alert_enabled* is **bit** and can't be `NULL`.

#### [ @history_retention_period = ] *history_retention_period*

The length of time in minutes in which the history is retained. *@history_retention_period* is **int**. A value of 14420 is used if none is specified.

#### [ @ignoreremotemonitor = ] *ignoreremotemonitor*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

#### [ @backup_compression = ] *backup_compression*

Specifies whether a log shipping configuration uses [backup compression](../backup-restore/backup-compression-sql-server.md). This parameter is supported in Enterprise edition for [!INCLUDE [sql2008-md](../../includes/sql2008-md.md)] (and later versions), and all editions on [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] with Service Pack 1 (and later versions).

- `0`: Disabled. Never compress log backups

- `1`: Enabled. Always compress log backups

- `2` (default): Use the [backup compression default](../../database-engine/configure-windows/view-or-configure-the-backup-compression-default-server-configuration-option.md) server configuration option.

#### [ @monitor_connection_options = ] *'<key_value_pairs>;[...]'*

**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions

Specifies additional connectivity options for the linked server connection when utilizing a remote monitor, in the form of key value pairs. **@monitor_connection_options** is **nvarchar(4000)** and has the default of `NULL`. 

The following table lists the available connectivity options:

| `Key` | `Value` |
|-----------|-----------------|
| `Encrypt` | `strict`, `mandatory`, `optional`, `true`, `false` |
| `TrustServerCertificate` | `true`, `false`, `yes`, `no` |
| `ServerCertificate` | Path on the filesystem to the server certificate. This has a maximum length of 260 characters. |
| `HostNameInCertificate` | Hostname override for the certificate. This has a maximum length of 255 characters. |

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sp_change_log_shipping_primary_database` must be run from the `master` database on the primary server. This stored procedure does the following:

1. Changes the settings in the `log_shipping_primary_database` record, if necessary.

1. Changes the local record in `log_shipping_monitor_primary` on the primary server using supplied arguments, if necessary.

1. If the monitor server is different from the primary server, changes record in `log_shipping_monitor_primary` on the monitor server using supplied arguments, if necessary.

## Permissions

Only members of the **sysadmin** fixed server role can run this procedure.

## Examples

This example illustrates the use of `sp_change_log_shipping_primary_database` to update the settings associated with the primary database [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)].

```sql
EXECUTE master.dbo.sp_change_log_shipping_primary_database
    @database = N'AdventureWorks',
    @backup_directory = N'c:\LogShipping',
    @backup_share = N'\\tribeca\LogShipping',
    @backup_retention_period = 1440,
    @backup_threshold = 60,
    @threshold_alert = 0,
    @threshold_alert_enabled = 1,
    @history_retention_period = 1440,
    @monitor_server_security_mode = 1,
    @backup_compression = 1;
```

## Related content

- [About log shipping (SQL Server)](../../database-engine/log-shipping/about-log-shipping-sql-server.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [log_shipping_primary_databases (Transact-SQL)](../system-tables/log-shipping-primary-databases-transact-sql.md)
