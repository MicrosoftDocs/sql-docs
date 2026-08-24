---
title: "sys.sp_change_feed_vupgrade (Transact-SQL)"
description: "The internal sys.sp_change_feed_vupgrade system stored procedure upgrades the change feed schema if required."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: imotiwala, ajayj, randolphwest
ms.date: 12/17/2025
ms.service: fabric
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.sp_change_feed_vupgrade_TSQL"
  - "sys.sp_change_feed_vupgrade"
  - "sp_change_feed_vupgrade_TSQL"
  - "sp_change_feed_vupgrade"
helpviewer_keywords:
  - "sp_change_feed_vupgrade"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16 || =azuresqldb-current || =azuresqldb-mi-current || =fabric || =fabric-sqldb || =azure-sqldw-latest"
---
# sys.sp_change_feed_vupgrade (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asdbmi-asa-fabricmirroredsqldb-fabricsqldb](../../includes/applies-to-version/sqlserver2022-asdb-asdbmi-asa-fabricmirroredsqldb-fabricsqldb.md)]

Upgrades the change feed schema if required.

[!INCLUDE [fabric-internal-use](includes/fabric-internal-use.md)]

This system stored procedure is used for:

- [SQL database in Microsoft Fabric](/fabric/database/sql/overview)
- [Microsoft Fabric mirrored databases](/fabric/database/mirrored-database/overview)
- [Azure Synapse Link](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
sys.sp_change_feed_vupgrade;
```

## Returns

`0` (success) or `1` (failure).

## Permissions

A user with `CONTROL` database permissions, **db_owner** database role membership, or **sysadmin** server role membership can execute this procedure.

## Related content

- [sys.sp_help_change_feed (Transact-SQL)](sp-help-change-feed.md)
- [sys.sp_help_change_feed_table (Transact-SQL)](sp-help-change-feed-table.md)
- [sys.sp_help_change_feed_table_groups (Transact-SQL)](sp-help-change-feed-table-groups.md)
- [sys.sp_help_change_feed_settings (Transact-SQL)](sp-help-change-feed-settings.md)
- [sys.sp_change_feed_configure_parameters (Transact-SQL)](sp-change-feed-configure-parameters.md)
- [sys.dm_change_feed_log_scan_sessions (Transact-SQL)](../system-dynamic-management-objects/sys-dm-change-feed-log-scan-sessions.md)
- [sys.dm_change_feed_errors (Transact-SQL)](../system-dynamic-management-objects/sys-dm-change-feed-errors.md)
