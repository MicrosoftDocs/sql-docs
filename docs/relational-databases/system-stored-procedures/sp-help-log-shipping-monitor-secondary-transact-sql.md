---
title: "sp_help_log_shipping_monitor_secondary (Transact-SQL)"
description: Returns information regarding a secondary database from the monitor tables.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_log_shipping_monitor_secondary"
  - "sp_help_log_shipping_monitor_secondary_TSQL"
helpviewer_keywords:
  - "sp_help_log_shipping_monitor_secondary"
dev_langs:
  - "TSQL"
---
# sp_help_log_shipping_monitor_secondary (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns information regarding a secondary database from the monitor tables.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_log_shipping_monitor_secondary
    [ @secondary_server = ] N'secondary_server'
    , [ @secondary_database = ] N'secondary_database'
[ ; ]
```

## Arguments

#### [ @secondary_server = ] N'*secondary_server*'

The name of the secondary server. *@secondary_server* is **sysname**, with no default.

#### [ @secondary_database = ] N'*secondary_database*'

The name of the secondary database. *@secondary_database* is **sysname**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column | Description |
| --- | --- |
| `secondary_server` | The name of the secondary instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] in the log shipping configuration. |
| `secondary_database` | The name of the secondary database in the log shipping configuration. |
| `secondary_id` | The ID for the secondary server in the log shipping configuration. |
| `primary_server` | The name of the primary instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] in the log shipping configuration. |
| `primary_database` | The name of the primary database in the log shipping configuration. |
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

## Remarks

`sp_help_log_shipping_monitor_secondary` must be run from the `master` database on the monitor server.

## Permissions

Only members of the **sysadmin** fixed server role can run this procedure.

## Related content

- [About log shipping (SQL Server)](../../database-engine/log-shipping/about-log-shipping-sql-server.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
