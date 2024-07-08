---
title: "sp_cleanup_log_shipping_history (Transact-SQL)"
description: sp_cleanup_log_shipping_history cleans up history locally, and on the monitor server, based on retention period.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_cleanup_log_shipping_history_TSQL"
  - "sp_cleanup_log_shipping_history"
helpviewer_keywords:
  - "sp_cleanup_log_shipping_history"
dev_langs:
  - "TSQL"
---
# sp_cleanup_log_shipping_history (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This stored procedure cleans up history locally, and on the monitor server, based on retention period.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_cleanup_log_shipping_history
    [ @agent_id = ] 'agent_id'
    , [ @agent_type = ] agent_type
[ ; ]
```

## Arguments

#### [ @agent_id = ] '*agent_id*'

The primary ID for backup or the secondary ID for copy or restore. *@agent_id* is **uniqueidentifier**, with no default, and can't be `NULL`.

#### [ @agent_type = ] *agent_type*

The type of log shipping job. *@agent_type* is **tinyint**, with no default, and must be one of these values:

| Value | Description |
| --- | --- |
| 0 | Backup |
| 1 | Copy |
| 2 | Restore |

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

`sp_cleanup_log_shipping_history` must be run from the `master` database on any log shipping server. This stored procedure cleans up local and remote copies of `log_shipping_monitor_history_detail` and `log_shipping_monitor_error_detail` based on history retention period.

## Permissions

Only members of the **sysadmin** fixed server role can run this procedure.

## Related content

- [About log shipping (SQL Server)](../../database-engine/log-shipping/about-log-shipping-sql-server.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
