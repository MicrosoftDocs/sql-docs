---
title: "sys.sp_change_log_shipping_secondary_primary (Transact-SQL)"
description: Changes secondary database settings.
author: dimitri-furman
ms.author: dfurman
ms.reviewer: vanto
ms.date: 06/19/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sp_change_log_shipping_secondary_primary"
  - "sp_change_log_shipping_secondary_primary_TSQL"
helpviewer_keywords:
  - "sp_change_log_shipping_secondary_primary"
dev_langs:
  - TSQL
---
# sys.sp_change_log_shipping_secondary_primary (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Changes secondary database settings.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_change_log_shipping_secondary_primary
    [ @primary_server = ] N'primary_server'
    , [ @primary_database = ] N'primary_database'
    [ , [ @backup_source_directory = ] N'backup_source_directory' ]
    [ , [ @backup_destination_directory = ] N'backup_destination_directory' ]
    [ , [ @file_retention_period = ] file_retention_period ]
    [ , [ @monitor_server_security_mode = ] monitor_server_security_mode ]
    [ , [ @monitor_server_login = ] 'monitor_server_login' ]
    [ , [ @monitor_server_password = ] 'monitor_server_password' ]
    [ , [ @monitor_connection_options = ] '<key_value_pairs>;[...]' ]
[ ; ]
```

## Arguments

#### [ @primary_server = ] N'*primary_server*'

The name of the primary instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] in the log shipping configuration. *@primary_server* is **sysname** and can't be `NULL`.

#### [ @primary_database = ] N'*primary_database*'

The name of the database on the primary server. *@primary_database* is **sysname**, with no default.

#### [ @backup_source_directory = ] N'*backup_source_directory*'

The directory where transaction log backup files from the primary server are stored. *@backup_source_directory* is **nvarchar(500)** and can't be `NULL`.

#### [ @backup_destination_directory = ] N'*backup_destination_directory*'

The directory on the secondary server where backup files are copied to. *@backup_destination_directory* is **nvarchar(500)** and can't be `NULL`.

#### [ @file_retention_period = ] *file_retention_period*

The length of time in minutes in which the backup files are retained. *@file_retention_period* is **int**, with a default of `NULL`. A value of 14420 is used if none is specified.

#### [ @monitor_server_security_mode = ] *monitor_server_security_mode*

The security mode used to connect to the monitor server.

- `1`: Windows Authentication;
- `0`: [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.

*@monitor_server_security_mode* is **bit** and defaults to `NULL`.

#### [ @monitor_server_login = ] N'*monitor_server_login*'

The username of the account used to access the monitor server.

#### [ @monitor_server_password = ] N'*monitor_server_password*'

The password of the account used to access the monitor server.

#### [ @monitor_connection_options = ] '*<key_value_pairs>; [...]*'

**Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions

Specifies more connectivity options for the linked server connection when utilizing a remote monitor, in the form of key value pairs. *@monitor_connection_options* is **nvarchar(4000)** and has the default of `NULL`.

The following table lists the available connectivity options:

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

`sp_change_log_shipping_secondary_primary` must be run from the `master` database on the secondary server. This stored procedure does the following actions:

1. Changes settings in `log_shipping_secondary` as necessary.

1. If the monitor server is different from the secondary server, changes monitor record in `log_shipping_monitor_secondary` on the monitor server using supplied arguments, if necessary.

## Permissions

Only members of the **sysadmin** fixed server role can run this procedure.

## Related content

- [About log shipping (SQL Server)](../../database-engine/log-shipping/about-log-shipping-sql-server.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
