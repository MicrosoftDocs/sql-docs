---
title: "sys.sp_change_feed_create_table_group (Transact-SQL)"
description: "The sys.sp_change_feed_create_table_group system stored procedure enables the creation of new change feed table group within the current database."
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
  - "sys.sp_change_feed_create_table_group_TSQL"
  - "sys.sp_change_feed_create_table_group"
  - "sp_change_feed_create_table_group_TSQL"
  - "sp_change_feed_create_table_group"
helpviewer_keywords:
  - "sp_change_feed_create_table_group"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16 || =azuresqldb-current || =azuresqldb-mi-current || =fabric || =fabric-sqldb || =azure-sqldw-latest"
---
# sys.sp_change_feed_create_table_group (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asdbmi-asa-fabricmirroredsqldb-fabricsqldb](../../includes/applies-to-version/sqlserver2022-asdb-asdbmi-asa-fabricmirroredsqldb-fabricsqldb.md)]

Creates a source to maintain metadata specific to each table group. A table group represents the container for all the individual tables that will be replicated.

[!INCLUDE [fabric-internal-use](includes/fabric-internal-use.md)]

This system stored procedure is used for:

- [SQL database in Microsoft Fabric](/fabric/database/sql/overview)
- [Microsoft Fabric mirrored databases](/fabric/database/mirrored-database/overview)
- [Azure Synapse Link](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
sys.sp_change_feed_create_table_group
    [ @table_group_id = ] 'table_group_id'
    , [ @table_group_name = ] N'table_group_name'
    [ , [ @workspace_id = ] N'workspace_id' ]
    [ , [ @destination_location = ] N'destination_location' ]
    [ , [ @destination_credential = ] N'destination_credential' ]
[ ; ]
```

## Arguments

#### [ @table_group_id = ] '*table_group_id*'

The unique identifier of the table group.

#### [ @table_group_name = ] N'*table_group_name*'

The name of the table group.

#### [ @workspace_id = ] N'*workspace_id*'

Azure resourceID for the workspace requesting creation of the new table group.

#### [ @destination_location = ] N'*destination_location*'

URL string of the landing zone folder.

#### [ @destination_credential = ] N'*destination_credential*'

The credential name to access the landing zone.

## Permissions

A user with `CONTROL` database permissions, **db_owner** database role membership, or **sysadmin** server role membership can execute this procedure.

## Related content

- [sys.sp_help_change_feed (Transact-SQL)](sp-help-change-feed.md)
- [sys.sp_help_change_feed_table (Transact-SQL)](sp-help-change-feed-table.md)
- [sys.sp_change_feed_configure_parameters (Transact-SQL)](sp-change-feed-configure-parameters.md)
- [sys.dm_change_feed_log_scan_sessions (Transact-SQL)](../system-dynamic-management-objects/sys-dm-change-feed-log-scan-sessions.md)
- [sys.dm_change_feed_errors (Transact-SQL)](../system-dynamic-management-objects/sys-dm-change-feed-errors.md)
