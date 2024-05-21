---
title: "sp_help_log_shipping_monitor_primary (Transact-SQL)"
description: Returns information regarding a primary database from the monitor tables.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_log_shipping_monitor_primary"
  - "sp_help_log_shipping_monitor_primary_TSQL"
helpviewer_keywords:
  - "sp_help_log_shipping_monitor_primary"
dev_langs:
  - "TSQL"
---
# sp_help_log_shipping_monitor_primary (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns information regarding a primary database from the monitor tables.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_log_shipping_monitor_primary
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

| Column name | Description |
| --- | --- |
| `primary_id` | The ID of the primary database for the log shipping configuration. |
| `primary_server` | The name of the primary instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] in the log shipping configuration. |
| `primary_database` | The name of the primary database in the log shipping configuration. |
| `backup_threshold` | The number of minutes allowed to elapse between backup operations before an alert is generated. |
| `threshold_alert` | The alert to be raised when the backup threshold is exceeded. |
| `threshold_alert_enabled` | Determines if backup threshold alerts are enabled. `1` = enabled; `0` = disabled. |
| `last_backup_file` | The absolute path of the most recent transaction log backup. |
| `last_backup_date` | The time and date of the last transaction log backup operation on the primary database. |
| `last_backup_date_utc` | The time and date of the last transaction log backup operation on the primary database, expressed in Coordinated Universal Time. |
| `history_retention_period` | The amount of time in minutes that log shipping history records are retained for a given primary database, before being deleted. |

## Remarks

`sp_help_log_shipping_monitor_primary` must be run from the `master` database on the monitor server.

## Permissions

Only members of the **sysadmin** fixed server role can run this procedure.

## Related content

- [About log shipping (SQL Server)](../../database-engine/log-shipping/about-log-shipping-sql-server.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
