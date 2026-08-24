---
title: "sys.sp_change_feed_enable_table (Transact-SQL)"
description: "The sys.sp_change_feed_enable_table system stored procedure enables the addition of a new table to an existing table group for Fabric Mirrored Databases or Azure Synapse Link."
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
  - "sys.sp_change_feed_enable_table_TSQL"
  - "sys.sp_change_feed_enable_table"
  - "sp_change_feed_enable_table_TSQL"
  - "sp_change_feed_enable_table"
helpviewer_keywords:
  - "sp_change_feed_enable_table"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16 || =azuresqldb-current || =azuresqldb-mi-current || =fabric || =fabric-sqldb || =azure-sqldw-latest"
---
# sys.sp_change_feed_enable_table (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asdbmi-asa-fabricmirroredsqldb-fabricsqldb](../../includes/applies-to-version/sqlserver2022-asdb-asdbmi-asa-fabricmirroredsqldb-fabricsqldb.md)]

Stored procedure to enable the creation of a new table to an existing table group.

[!INCLUDE [fabric-internal-use](includes/fabric-internal-use.md)]

This system stored procedure is used for:

- [SQL database in Microsoft Fabric](/fabric/database/sql/overview)
- [Microsoft Fabric mirrored databases](/fabric/database/mirrored-database/overview)
- [Azure Synapse Link](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
sys.sp_change_feed_enable_table
    [ @table_group_id = ] 'table_group_id'
    , [ @table_id = ] 'table_id'
    , [ @source_schema = ] N'source_schema'
    , [ @source_name = ] N'source_name'
[ ; ]
```

## Arguments

#### [ @table_group_id = ] '*table_group_id*'

The unique identifier of the table group.

#### [ @table_id = ] '*table_id*'

The unique identifier for the change feed table generated during setup workflow.

#### [ @source_schema = ] N'*source_schema*'

The source table schema name.

#### [ @source_name = ] N'*source_name*'

The source table name.

## Permissions

A user with `CONTROL` database permissions, **db_owner** database role membership, or **sysadmin** server role membership can execute this procedure.

## Related content

- [sys.sp_help_change_feed (Transact-SQL)](sp-help-change-feed.md)
- [sys.sp_help_change_feed_table (Transact-SQL)](sp-help-change-feed-table.md)
- [sys.sp_change_feed_configure_parameters (Transact-SQL)](sp-change-feed-configure-parameters.md)
- [sys.dm_change_feed_log_scan_sessions (Transact-SQL)](../system-dynamic-management-objects/sys-dm-change-feed-log-scan-sessions.md)
- [sys.dm_change_feed_errors (Transact-SQL)](../system-dynamic-management-objects/sys-dm-change-feed-errors.md)
