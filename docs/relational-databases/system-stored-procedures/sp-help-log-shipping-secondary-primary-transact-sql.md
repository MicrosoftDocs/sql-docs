---
title: "sp_help_log_shipping_secondary_primary (Transact-SQL)"
description: sp_help_log_shipping_secondary_primary retrieves the settings for a given primary database on the secondary server.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_log_shipping_secondary_primary"
  - "sp_help_log_shipping_secondary_primary_TSQL"
helpviewer_keywords:
  - "sp_help_log_shipping_secondary_primary"
dev_langs:
  - "TSQL"
---
# sp_help_log_shipping_secondary_primary (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This stored procedure retrieves the settings for a given primary database on the secondary server.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_log_shipping_secondary_primary
    [ @primary_server = ] N'primary_server'
    , [ @primary_database = ] N'primary_database'
[ ; ]
```

## Arguments

#### [ @primary_server = ] N'*primary_server*'

The name of the primary instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] in the log shipping configuration. *@primary_server* is **sysname**, with no default.

#### [ @primary_database = ] N'*primary_database*'

The name of the database on the primary server. *@primary_database* is **sysname**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Result set

The result set contains the following columns from [log_shipping_secondary](../system-tables/log-shipping-secondary-transact-sql.md).

| Column name | Data type | Description |
| --- | --- | --- |
| `secondary_id` | **uniqueidentifier** | The ID for the secondary server in the log shipping configuration. |
| `primary_server` | **sysname** | The name of the primary instance of the SQL Server Database Engine in the log shipping configuration. |
| `primary_database` | **sysname** | The name of the primary database in the log shipping configuration. |
| `backup_source_directory` | **nvarchar(500)** | The directory where transaction log backup files from the primary server are stored. |
| `backup_destination_directory` | **nvarchar(500)** | The directory on the secondary server where backup files are copied to. |
| `file_retention_period` | **int** | The length of time, in minutes, that a backup file is retained on the secondary server before being deleted. |
| `copy_job_id` | **uniqueidentifier** | The ID associated with the copy job on the secondary server. |
| `restore_job_id` | **uniqueidentifier** | The ID associated with the restore job on the secondary server. |
| `monitor_server` | **sysname** | The name of the instance of the [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] being used as a monitor server in the log shipping configuration. |
| `monitor_server_security_mode` | **bit** | The security mode used to connect to the monitor server.<br /><br />`1` = Windows Authentication.<br />`0` = [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. |

## Remarks

`sp_help_log_shipping_secondary_primary` must be run from the `master` database on the secondary server.

## Permissions

Only members of the **sysadmin** fixed server role can run this procedure.

## Related content

- [About log shipping (SQL Server)](../../database-engine/log-shipping/about-log-shipping-sql-server.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
