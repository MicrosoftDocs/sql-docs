---
title: "sp_add_log_shipping_primary_database (Transact-SQL)"
description: Sets up the primary database for a log shipping configuration, including the backup job, local monitor record, and remote monitor record.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 02/23/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "sp_add_log_shipping_primary_database"
  - "sp_add_log_shipping_primary_database_TSQL"
helpviewer_keywords:
  - "sp_add_log_shipping_primary_database"
dev_langs:
  - TSQL
---
# sp_add_log_shipping_primary_database (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Sets up the primary database for a log shipping configuration, including the backup job, local monitor record, and remote monitor record.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_add_log_shipping_primary_database
    [ @database = ] N'database'
    , [ @backup_directory = ] N'backup_directory'
    , [ @backup_share = ] N'backup_share'
    [ , [ @backup_job_name = ] N'backup_job_name' ]
    [ , [ @backup_retention_period = ] backup_retention_period ]
    [ , [ @monitor_server = ] N'monitor_server' ]
    [ , [ @monitor_server_security_mode = ] monitor_server_security_mode ]
    [ , [ @monitor_server_login = ] N'monitor_server_login' ]
    [ , [ @monitor_server_password = ] N'monitor_server_password' ]
    [ , [ @backup_threshold = ] backup_threshold ]
    [ , [ @threshold_alert = ] threshold_alert ]
    [ , [ @threshold_alert_enabled = ] threshold_alert_enabled ]
    [ , [ @history_retention_period = ] history_retention_period ]
    [ , [ @backup_job_id = ] 'backup_job_id' OUTPUT ]
    [ , [ @primary_id = ] 'primary_id' OUTPUT ]
    [ , [ @overwrite = ] overwrite ]
    [ , [ @ignoreremotemonitor = ] ignoreremotemonitor ]
    [ , [ @backup_compression = ] backup_compression ]
    [ , [ @primary_server_with_port_override = ] N'primary_server_with_port_override' ]
    [ , [ @primary_connection_options = ] N'primary_connection_options' ]
    [ , [ @monitor_connection_options = ] N'monitor_connection_options' ]
[ ; ]
```

## Arguments

#### [ @database = ] N'*database*'

The name of the log shipping primary database. *@database* is **sysname**, with no default, and can't be `NULL`.

#### [ @backup_directory = ] N'*backup_directory*'

The path to the backup folder on the primary server. *@backup_directory* is **nvarchar(500)**, with no default, and can't be `NULL`.

#### [ @backup_share = ] N'*backup_share*'

The network path to the backup directory on the primary server. *@backup_share* is **nvarchar(500)**, with no default, and can't be `NULL`.

#### [ @backup_job_name = ] N'*backup_job_name*'

The name of the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent job on the primary server that copies the backup into the backup folder. *@backup_job_name* is **sysname**, and can't be `NULL`.

#### [ @backup_retention_period = ] *backup_retention_period*

The length of time, in minutes, to retain the log backup file in the backup directory on the primary server. *@backup_retention_period* is **int**, with a default of `1440`, and can't be `NULL`.

<a id="monitor-server"></a>

#### [ @monitor_server = ] N'*monitor_server*'

The name of the monitor server. *@monitor_server* is **sysname**, and can't be `NULL`.

#### [ @monitor_server_security_mode = ] *monitor_server_security_mode*

The security mode used to connect to the monitor server. *@monitor_server_security_mode* is **bit**, and can be one of the following values:

- `1` (default): Windows Authentication
- `0`: [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication

#### [ @monitor_server_login = ] N'*monitor_server_login*'

The username of the account used to access the monitor server. *@monitor_server_login* is **sysname**, with a default of `NULL`.

#### [ @monitor_server_password = ] N'*monitor_server_password*'

The password of the account used to access the monitor server. *@monitor_server_password* is **sysname**, with a default of `NULL`.

#### [ @backup_threshold = ] *backup_threshold*

The length of time, in minutes, after the last backup before a *@threshold_alert* error is raised. *@backup_threshold* is **int**, with a default of `45` minutes.

#### [ @threshold_alert = ] *threshold_alert*

The alert to be raised when the backup threshold is exceeded. *@threshold_alert* is **int**, with a default of `14420`.

#### [ @threshold_alert_enabled = ] *threshold_alert_enabled*

Specifies whether an alert is raised when *@backup_threshold* is exceeded. *@threshold_alert_enabled* is **bit**, with a default of `0`. When set to `0`, the alert is disabled and isn't raised.

#### [ @history_retention_period = ] *history_retention_period*

The length of time in minutes in which the history is retained. *@history_retention_period* is **int**, with a default of `1440`.

#### [ @backup_job_id = ] '*backup_job_id*' OUTPUT

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent job ID associated with the backup job on the primary server. *@backup_job_id* is an OUTPUT parameter of type **uniqueidentifier** and can't be `NULL`.

#### [ @primary_id = ] '*primary_id*' OUTPUT

The ID of the primary database for the log shipping configuration. *@primary_id* is an OUTPUT parameter of type **uniqueidentifier** and can't be `NULL`.

#### [ @overwrite = ] *overwrite*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

#### [ @ignoreremotemonitor = ] *ignoreremotemonitor*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

#### [ @backup_compression = ] *backup_compression*

Specifies whether a log shipping configuration uses [backup compression](../backup-restore/backup-compression-sql-server.md). *@backup_compression* is **tinyint**, and can be one of the following values:

- `0`: Disabled. Never compress log backups.
- `1`: Enabled. Always compress log backups.
- `2` (default): Use the [backup compression default](../../database-engine/configure-windows/view-or-configure-the-backup-compression-default-server-configuration-option.md) server configuration option.

<a id="primary-server-with-port-override"></a>

#### [ @primary_server_with_port_override = ] N'*primary_server_with_port_override*'

Valid only for scenarios where log shipping is used with contained availability groups on the primary. *@primary_server_with_port_override* is **sysname**, with a default of `NULL`.

If you use a contained availability group on the primary side of log shipping, you should pass the connection string to the contained availability group listener with a port instead.

For more information, see [What is a contained availability group?](../../database-engine/availability-groups/windows/contained-availability-groups-overview.md#log-shipping)

#### [ @primary_connection_options = ] N'*primary_connection_options*'

**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions.

Specifies additional connectivity options when connecting to the primary, in the form of key value pairs. *@primary_connection_options* is **nvarchar(4000)**, and can be one of the following values:

| Key | Value |
| --- | --- |
| `Encrypt` | `strict`, `mandatory`, `optional`, `true`, `false` |
| `TrustServerCertificate` | `true`, `false`, `yes`, `no` |
| `ServerCertificate` | Path on the filesystem to the server certificate. This has a maximum length of 260 characters. |
| `HostNameInCertificate` | Hostname override for the certificate. This has a maximum length of 255 characters. |

#### [ @monitor_connection_options = ] N'*monitor_connection_options*'

**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions.

Specifies additional connectivity options for the linked server connection when utilizing a remote monitor, in the form of key value pairs. *@monitor_connection_options* is **nvarchar(4000)**, and can be one of the following values:

| Key | Value |
| --- | --- |
| `Encrypt` | `strict`, `mandatory`, `optional`, `true`, `false` |
| `TrustServerCertificate` | `true`, `false`, `yes`, `no` |
| `ServerCertificate` | Path on the filesystem to the server certificate. This has a maximum length of 260 characters. |
| `HostNameInCertificate` | Hostname override for the certificate. This has a maximum length of 255 characters. |

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

### A. Add a primary database in a log shipping configuration

This example adds the database [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] as the primary database in a log shipping configuration.

```sql
DECLARE @LS_BackupJobId AS UNIQUEIDENTIFIER;
DECLARE @LS_PrimaryId AS UNIQUEIDENTIFIER;

EXECUTE master.dbo.sp_add_log_shipping_primary_database
    @database = N'AdventureWorks',
    @backup_directory = N'c:\lsbackup',
    @backup_share = N'\\backupshare\lsbackup',
    @backup_job_name = N'LSBackup_AdventureWorks',
    @backup_retention_period = 1440,
    @monitor_server = N'monitor-server',
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

### B. Add primary database with strict encryption

This example adds the database [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] as the primary database in a log shipping configuration and instructs log shipping to use the strict encryption options for both the connection to the primary instance from the log shipping executable and from the primary instance to the remote monitor instance `monitor-server`.

```sql
DECLARE @LS_BackupJobId AS UNIQUEIDENTIFIER;
DECLARE @LS_PrimaryId AS UNIQUEIDENTIFIER;

EXECUTE master.dbo.sp_add_log_shipping_primary_database
    @database = N'AdventureWorks',
    @backup_directory = N'c:\lsbackup',
    @backup_share = N'\\backupshare\lsbackup',
    @backup_job_name = N'LSBackup_AdventureWorks',
    @backup_retention_period = 1440,
    @monitor_server = N'monitor-server',
    @monitor_server_security_mode = 1,
    @backup_threshold = 60,
    @threshold_alert = 0,
    @threshold_alert_enabled = 0,
    @history_retention_period = 1440,
    @backup_job_id = @LS_BackupJobId OUTPUT,
    @primary_id = @LS_PrimaryId OUTPUT,
    @overwrite = 1,
    @backup_compression = 0,
    @primary_connection_options = N'Encrypt=Strict;',
    @monitor_connection_options = N'Encrypt=Strict;';
GO
```

### C. Use a remote monitor with connectivity options

Log shipping monitoring can break if the monitor is a remote [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] instance, when other SQL Server instances in the log shipping topology use a previous version.

Once you drop the existing configuration, use the following example script to recreate the log shipping configuration with the correct *@monitor_connection_options*, for both the primary and the secondary replicas.

```sql
DECLARE @LS_BackupJobId AS UNIQUEIDENTIFIER;
DECLARE @LS_PrimaryId AS UNIQUEIDENTIFIER;
EXECUTE master.dbo.sp_add_log_shipping_primary_database
    @database = N'LogShippedDB',
    @backup_directory = N'\\backupshare\lsbackup',
    @backup_share = N'\\backupshare\lsbackup',
    @backup_job_name = N'LSBackup_AdventureWorks',
    @backup_retention_period = 4320,
    @backup_compression = 2,
    @monitor_server = N'LS25Monitor',
    @monitor_server_security_mode = 1,
    @backup_threshold = 60,
    @threshold_alert_enabled = 1,
    @history_retention_period = 5760,
    @backup_job_id = @LS_BackupJobId OUTPUT,
    @primary_id = @LS_PrimaryId OUTPUT,
    @overwrite = 1,
    @monitor_connection_options = N'Encrypt=Mandatory;TrustServerCertificate=Yes;';
```

For more information, see [Encryption and certificate validation behavior](../../connect/oledb/features/encryption-and-certificate-validation.md#encryption-and-certificate-validation-behavior).

## Related content

- [About log shipping (SQL Server)](../../database-engine/log-shipping/about-log-shipping-sql-server.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
