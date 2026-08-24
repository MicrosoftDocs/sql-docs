---
title: "sys.sp_change_feed_reseed_db_start_replication (Transact-SQL)"
description: "The sys.sp_change_feed_reseed_db_start_replication system internal stored procedure begins replication for a database in a reseed state."
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
  - "sys.sp_change_feed_reseed_db_start_replication_TSQL"
  - "sys.sp_change_feed_reseed_db_start_replication"
  - "sp_change_feed_reseed_db_start_replication_TSQL"
  - "sp_change_feed_reseed_db_start_replication"
helpviewer_keywords:
  - "sp_change_feed_reseed_db_start_replication"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver17 || =azuresqldb-current || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# sys.sp_change_feed_reseed_db_start_replication (Transact-SQL)

[!INCLUDE [sqlserver2025-asdb-asdbmi-fabricmirroredsqldb-fabricsqldb](../../includes/applies-to-version/sqlserver2025-asdb-asdbmi-fabricmirroredsqldb-fabricsqldb.md)]

Begins replication for a database in a reseed state.

[!INCLUDE [fabric-internal-use](includes/fabric-internal-use.md)]

This system stored procedure is used for [Microsoft Fabric mirrored databases](/fabric/database/mirrored-database/overview) and [SQL database in Microsoft Fabric](/fabric/database/sql/overview).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_change_feed_reseed_db_start_replication
[ ; ]
```

## Arguments

None.

## Returns

`0` (success) or non-zero (failure).

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
- [What is Mirroring in Fabric?](/fabric/database/mirrored-database/overview)
- [Monitor Fabric mirrored database replication](/fabric/database/mirrored-database/monitor)
- [Explore data in your mirrored database using Microsoft Fabric](/fabric/database/mirrored-database/explore)
