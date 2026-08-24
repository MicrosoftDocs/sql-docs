---
title: "sys.sp_has_change_feed_permissions (Transact-SQL)"
description: "The internal sys.sp_has_change_feed_permissions system stored procedure checks for permissions when enabling change feed publishing."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: imotiwala, ajayj, randolphwest
ms.date: 06/19/2026
ms.service: fabric
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.sp_has_change_feed_permissions_TSQL"
  - "sys.sp_has_change_feed_permissions"
  - "sp_has_change_feed_permissions_TSQL"
  - "sp_has_change_feed_permissions"
helpviewer_keywords:
  - "sp_has_change_feed_permissions"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16 || =azuresqldb-current || =azuresqldb-mi-current || =fabric || =fabric-sqldb || =azure-sqldw-latest"
---
# sys.sp_has_change_feed_permissions (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asdbmi-asa-fabricmirroredsqldb-fabricsqldb](../../includes/applies-to-version/sqlserver2022-asdb-asdbmi-asa-fabricmirroredsqldb-fabricsqldb.md)]

Internal procedure that checks for permissions when enabling change feed publishing.

[!INCLUDE [fabric-internal-use](includes/fabric-internal-use.md)]

This system stored procedure is used for:

- [SQL database in Microsoft Fabric](/fabric/database/sql/overview)
- [Microsoft Fabric mirrored databases](/fabric/database/mirrored-database/overview)
- [Azure Synapse Link](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_has_change_feed_permissions [ [ @destination_type = ] destination_type ]
[ ; ]
```

## Arguments

#### [ @destination_type = ] *destination_type*

*@destination_type* is **int**, and can't be `NULL`.

*@destination_type* can be one of the following values:

| Value | Description |
| --- | --- |
| `0` | Azure Synapse Link |
| `2` (default) | Fabric mirroring |

## Result set

| Result | Description |
| --- | --- |
| `0` | Success |
| `1` | Failure |
| `22740` | The *destination_type* is invalid |
| `22702` | Insufficient permissions |

## Permissions

Only **public** role membership is required to execute this procedure.

## Related content

- [sys.sp_help_change_feed (Transact-SQL)](sp-help-change-feed.md)
- [sys.sp_help_change_feed_table (Transact-SQL)](sp-help-change-feed-table.md)
- [sys.sp_change_feed_configure_parameters (Transact-SQL)](sp-change-feed-configure-parameters.md)
- [sys.dm_change_feed_log_scan_sessions (Transact-SQL)](../system-dynamic-management-objects/sys-dm-change-feed-log-scan-sessions.md)
- [sys.dm_change_feed_errors (Transact-SQL)](../system-dynamic-management-objects/sys-dm-change-feed-errors.md)
