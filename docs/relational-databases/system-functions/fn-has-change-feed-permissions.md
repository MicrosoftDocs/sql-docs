---
title: "sys.fn_has_change_feed_permissions (Transact-SQL)"
description: "The internal sys.fn_has_change_feed_permissions system function checks for permissions when enabling change feed publishing."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: imotiwala, ajayj
ms.date: 05/01/2025
ms.service: fabric
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.fn_has_change_feed_permissions_TSQL"
  - "sys.fn_has_change_feed_permissions"
  - "fn_has_change_feed_permissions_TSQL"
  - "fn_has_change_feed_permissions"
helpviewer_keywords:
  - "fn_has_change_feed_permissions"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16 || =azuresqldb-current || =azuresqldb-mi-current || =fabric || =fabric-sqldb || =azure-sqldw-latest"
---
# sys.fn_has_change_feed_permissions (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asdbmi-asa-fabricmirroredsqldb-fabricsqldb](../../includes/applies-to-version/sqlserver2022-asdb-asdbmi-asa-fabricmirroredsqldb-fabricsqldb.md)]

Internal procedure that checks for permissions when enabling change feed publishing.

> [!CAUTION]
> This system function is used internally and is not recommended for direct administrative use. Use Synapse Studio or the Fabric portal instead.

This system function is used for:

- [SQL database in Microsoft Fabric](/fabric/database/sql/overview)
- [Microsoft Fabric mirrored databases](/fabric/database/mirrored-database/overview)
- [Azure Synapse Link](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
sys.fn_has_change_feed_permissions (@destination_type int);
```

## Arguments

#### destination_type

Int. `0` = Azure Synapse Link. `2` = Fabric mirroring. Default is `2`.

## Result set

`0` (success) or `1` (failure).

## Permissions

Only **public** role membership is required to query this function.

## Related content

- [sys.dm_change_feed_log_scan_sessions (Transact-SQL)](../system-dynamic-management-objects/sys-dm-change-feed-log-scan-sessions.md)
- [sys.dm_change_feed_errors (Transact-SQL)](../system-dynamic-management-objects/sys-dm-change-feed-errors.md)
