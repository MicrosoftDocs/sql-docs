---
title: "sp_add_log_shipping_primary_database (Transact-SQL)"
description: "Sets up the primary database for a log shipping configuration, including the backup job, local monitor record, and remote monitor record."
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_add_log_shipping_primary_database"
  - "sp_add_log_shipping_primary_database_TSQL"
helpviewer_keywords:
  - "sp_add_log_shipping_primary_database"
dev_langs:
  - "TSQL"
---
# sp_add_log_shipping_primary_database (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Sets up the primary database for a log shipping configuration, including the backup job, local monitor record, and remote monitor record.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_add_log_shipping_primary_database
    [ @database = ] 'database'
    , [ @backup_directory = ] N'backup_directory'
    , [ @backup_share = ] N'backup_share'
    , [ @backup_job_name = ] 'backup_job_name'
    [ , [ @backup_retention_period = ] backup_retention_period ]
    [ , [ @monitor_server = ] 'monitor_server' ]
    [ , [ @monitor_server_security_mode = ] monitor_server_security_mode ]
    [ , [ @monitor_server_login = ] 'monitor_server_login' ]
    [ , [ @monitor_server_password = ] 'monitor_server_password' ]
    [ , [ @backup_threshold = ] backup_threshold ]
    [ , [ @threshold_alert = ] threshold_alert ]
    [ , [ @threshold_alert_enabled = ] threshold_alert_enabled ]
    [ , [ @history_retention_period = ] history_retention_period ]
    [ , [ @backup_job_id = ] backup_job_id OUTPUT ]
    [ , [ @primary_id = ] primary_id OUTPUT ]
    [ , [ @backup_compression = ] backup_compression_option ]
[ ; ]
```

## Arguments

#### [ @database = ] '*database*'

The name of the log shipping primary database. *@database* is **sysname**, with no default, and can't be `NULL`.

#### [ @backup_directory = ] N'*backup_directory*'

The path to the backup folder on the primary server. *@backup_directory* is **nvarchar(500)**, with no default, and can't be `NULL`.

#### [ @backup_share = ] N'*backup_share*'

The network path to the backup directory on the primary server. *@backup_share* is **nvarchar(500)**, with no default, and can't be `NULL`.

#### [ @backup_job_name = ] '*backup_job_name*'

The name of the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent job on the primary server that copies the backup into the backup folder. *@backup_job_name* is **sysname** and can't be `NULL`.

#### [ @backup_retention_period = ] *backup_retention_period*

The length of time, in minutes, to retain the log backup file in the backup directory on the primary server. *@backup_retention_period* is **int**, with no default, and can't be `NULL`.

#### [ @monitor_server = ] '*monitor_server*'

The name of the monitor server. *@monitor_server* is **sysname**, with no default, and can't be `NULL`.

#### [ @monitor_server_security_mode = ] *monitor_server_security_mode*

The security mode used to connect to the monitor server.

- `1`: Windows Authentication
- `0`: [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication

*@monitor_server_security_mode* is **bit**, with a default of `1`, and can't be `NULL`.

#### [ @monitor_server_login = ] '*monitor_server_login*'

The username of the account used to access the monitor server.

#### [ @monitor_server_password = ] '*monitor_server_password*'

The password of the account used to access the monitor server.

#### [ @backup_threshold = ] *backup_threshold*

The length of time, in minutes, after the last backup before a *@threshold_alert* error is raised. *@backup_threshold* is **int**, with a default of 60 minutes.

#### [ @threshold_alert = ] *threshold_alert*

The alert to be raised when the backup threshold is exceeded. *@threshold_alert* is **int**, with a default of 14,420.

#### [ @threshold_alert_enabled = ] *threshold_alert_enabled*

Specifies whether an alert is raised when *@backup_threshold* is exceeded. The value of zero (0), the default, means that the alert is disabled and won't be raised. *@threshold_alert_enabled* is **bit**.

#### [ @history_retention_period = ] *history_retention_period*

The length of time in minutes in which the history is retained. *@history_retention_period* is **int**, with a default of `NULL`. A value of 14420 is used if none is specified.

#### [ @backup_job_id = ] *backup_job_id* OUTPUT

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent job ID associated with the backup job on the primary server. *@backup_job_id* is an OUTPUT parameter of type **uniqueidentifier** and can't be `NULL`.

#### [ @primary_id = ] *primary_id* OUTPUT

The ID of the primary database for the log shipping configuration. *@primary_id* is an OUTPUT parameter of type **uniqueidentifier** and can't be `NULL`.

#### [ @backup_compression = ] *backup_compression_option*

Specifies whether a log shipping configuration uses [backup compression](../backup-restore/backup-compression-sql-server.md).

- `0`: Disabled. Never compress log backups.
- `1`: Enabled. Always compress log backups.
- `2` (default): Use the setting of the [View or configure the backup compression default (server configuration option)](../../database-engine/configure-windows/view-or-configure-the-backup-compression-default-server-configuration-option.md).

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sp_add_log_shipping_primary_database` must be run from the `master` database on the primary server. This stored procedure performs the following functions:

1. Generates a primary ID and adds an entry for the primary database in the table `log_shipping_primary_databases` using the supplied arguments.

1. Creates a backup job for the primary database that is disabled.

1. Sets the backup job ID in the `log_shipping_primary_databases` entry to the job ID of the backup job.

1. Adds a local monitor record in the table `log_shipping_monitor_primary` on the primary server using supplied arguments.

1. If the monitor server is different from the primary server, `sp_add_log_shipping_primary_database` adds a monitor record in `log_shipping_monitor_primary` on the monitor server using supplied arguments.

## Permissions

Only members of the **sysadmin** fixed server role can run this procedure.

## Examples

This example adds the database [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] as the primary database in a log shipping configuration.

```sql
DECLARE @LS_BackupJobId AS UNIQUEIDENTIFIER;
DECLARE @LS_PrimaryId AS UNIQUEIDENTIFIER;

EXEC master.dbo.sp_add_log_shipping_primary_database
    @database = N'AdventureWorks',
    @backup_directory = N'c:\lsbackup',
    @backup_share = N'\\tribeca\lsbackup',
    @backup_job_name = N'LSBackup_AdventureWorks',
    @backup_retention_period = 1440,
    @monitor_server = N'rockaway',
    @monitor_server_security_mode = 1,
    @backup_threshold = 60,
    @threshold_alert = 0,
    @threshold_alert_enabled = 0,
    @history_retention_period = 1440,
    @backup_job_id = @LS_BackupJobId OUTPUT,
    @primary_id = @LS_PrimaryId OUTPUT,
    @overwrite = 1,
    @backup_compression = 0;
GO
```

## Related content

- [About log shipping (SQL Server)](../../database-engine/log-shipping/about-log-shipping-sql-server.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
