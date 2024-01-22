---
title: "sp_help_log_shipping_monitor (Transact-SQL)"
description: Returns a result set containing status and other information for registered primary and secondary databases.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_log_shipping_monitor_TSQL"
  - "sp_help_log_shipping_monitor"
helpviewer_keywords:
  - "sp_help_log_shipping_monitor"
dev_langs:
  - "TSQL"
---
# sp_help_log_shipping_monitor (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns a result set containing status and other information for registered primary and secondary databases on a primary, secondary, or monitor server.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_log_shipping_monitor [ [ @verbose = ] verbose ]
[ ; ]
```

## Arguments

#### [ @verbose = ] *verbose*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `status` | **bit** | Collective status of agents for the log shipping database:<br /><br />`0` = healthy and no-agent failures.<br />`1` = otherwise. |
| `is_primary` | **bit** | Indicates whether this row is for a primary database:<br /><br />`1` = The row is for a primary database.<br />`0` = The row is for a secondary database. |
| `server` | **sysname** | The name of the primary or secondary server where this database resides. |
| `database_name` | **sysname** | The database name. |
| `time_since_last_backup` | **int** | The length of time, in minutes, since the last log backup.<br /><br />`NULL` = The information isn't available or isn't relevant. |
| `last_backup_file` | **nvarchar(500)** | The name of the last successful log backup file.<br /><br />`NULL` = The information isn't available or isn't relevant. |
| `backup_threshold` | **int** | The length of time, in minutes, after the last backup before a threshold_alert error is raised. `backup_threshold` is **int**, with a default of `60` minutes.<br /><br />`NULL` = The information isn't available or isn't relevant.<br /><br />This value can be changed using [sp_add_log_shipping_primary_database](sp-add-log-shipping-primary-database-transact-sql.md). |
| `is_backup_alert_enabled` | **bit** | Specifies whether an alert is raised when `backup_threshold` is exceeded. The value of one (`1`), the default, means that the alert is raised.<br /><br />`NULL` = The information isn't available or isn't relevant.<br /><br />This value can be changed using [sp_add_log_shipping_primary_database](sp-add-log-shipping-primary-database-transact-sql.md). |
| `time_since_last_copy` | **int** | The length of time, in minutes, since the last log backup was copied.<br /><br />`NULL` = The information isn't available or isn't relevant. |
| `last_copied_file` | **nvarchar(500)** | The name of the last successfully copied log backup file.<br /><br />`NULL` = The information isn't available or isn't relevant. |
| `time_since_last_restore` | **int** | The length of time, in minutes, since the last log backup was restored.<br /><br />`NULL` = The information isn't available or isn't relevant. |
| `last_restored_file` | **nvarchar(500).** | The name of the last successfully restored log backup file.<br /><br />`NULL` = The information isn't available or isn't relevant. |
| `last_restored_latency` | **int** | Duration of time, in minutes, from the creation of the last backup to restore of the backup.<br /><br />`NULL` = The information isn't available or isn't relevant. |
| `restore_threshold` | **int** | The number of minutes allowed to elapse between restore operations before an alert is generated. **restore_threshold** can't be `NULL`. |
| `is_restore_alert_enabled` | **bit** | Specifies whether an alert is raised when `restore_threshold` is exceeded. The value of one (`1`), the default, means that the alert is raised.<br /><br />`NULL` = The information isn't available or isn't relevant.<br /><br />To set restore threshold, use [sp_add_log_shipping_secondary_database](sp-add-log-shipping-secondary-database-transact-sql.md). |

## Remarks

`sp_help_log_shipping_monitor` must be run from the `master` database on the monitor server.

## Permissions

Requires membership in the **sysadmin** fixed server role, or execute permission directly on this stored procedure.

## Related content

- [About log shipping (SQL Server)](../../database-engine/log-shipping/about-log-shipping-sql-server.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
