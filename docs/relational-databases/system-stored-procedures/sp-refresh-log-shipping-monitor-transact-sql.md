---
title: "sp_refresh_log_shipping_monitor (Transact-SQL)"
description: sp_refresh_log_shipping_monitor refreshes the remote monitor tables with the latest information from a given primary or secondary server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_refresh_log_shipping_monitor"
  - "sp_refresh_log_shipping_monitor_TSQL"
helpviewer_keywords:
  - "sp_refresh_log_shipping_monitor"
dev_langs:
  - "TSQL"
---
# sp_refresh_log_shipping_monitor (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This stored procedure refreshes the remote monitor tables with the latest information from a given primary or secondary server for the specified log shipping agent. The procedure is invoked on the primary or secondary server.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_refresh_log_shipping_monitor
    [ @agent_id = ] 'agent_id'
    , [ @agent_type = ] agent_type
    [ , [ @database = ] N'database' ]
    , [ @mode = ] mode
[ ; ]
```

## Arguments

#### [ @agent_id = ] '*agent_id*'

The primary ID for backup or the secondary ID for copy or restore. *@agent_id* is **uniqueidentifier**, with no default, and can't be `NULL`.

#### [ @agent_type = ] *agent_type*

The type of log shipping job. *@agent_type* is **tinyint**, and can't be `NULL`. *@agent_type* must be one of these values:

| Value | Description |
| --- | --- |
| `0` | Backup |
| `1` | Copy |
| `2` | Restore |

#### [ @database = ] N'*database*'

The primary or secondary database used by logging by backup or restore agents. *@database* is **sysname**, with a default of `NULL`.

#### [ @mode = ] *mode*

Specifies whether to refresh the monitor data or clean it. *@mode* is **tinyint**, and can be one of these values:

| Value | Description |
| --- | --- |
| `1` (default) | Refresh |
| `2` | Delete |

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sp_refresh_log_shipping_monitor` refreshes the `log_shipping_monitor_primary`, `log_shipping_monitor_secondary`, `log_shipping_monitor_history_detail`, and `log_shipping_monitor_error_detail` tables with any session information that isn't already transferred. `sp_refresh_log_shipping_monitor` allows you to synchronize the monitor server with primary or a secondary server when the monitor is out of sync for some time. Additionally, it allows you to clean up the monitor information on monitor server if necessary.

`sp_refresh_log_shipping_monitor` must be run from the `master` database on the primary or secondary server.

## Permissions

Only members of the **sysadmin** fixed server role can run this procedure.

## Related content

- [About log shipping (SQL Server)](../../database-engine/log-shipping/about-log-shipping-sql-server.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
