---
title: "sys.sp_help_change_feed_table (Transact-SQL)"
description: "The sys.sp_help_change_feed_table system stored procedure provides the provision or deprovision flow status of Azure Synapse Link for SQL or Fabric Mirrored Databases."
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
  - "sys.sp_help_change_feed_table_TSQL"
  - "sys.sp_help_change_feed_table"
  - "sp_help_change_feed_table_TSQL"
  - "sp_help_change_feed_table"
helpviewer_keywords:
  - "sp_help_change_feed_table"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16 || =azuresqldb-current || =azuresqldb-mi-current || =fabric || =fabric-sqldb || =azure-sqldw-latest"
---
# sys.sp_help_change_feed_table (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asdbmi-asa-fabricmirroredsqldb-fabricsqldb](../../includes/applies-to-version/sqlserver2022-asdb-asdbmi-asa-fabricmirroredsqldb-fabricsqldb.md)]

Provides the provision or deprovision status and information of table group and table metadata.

This system stored procedure is used for:

- [SQL database in Microsoft Fabric](/fabric/database/sql/overview)
- [Microsoft Fabric mirrored databases](/fabric/database/mirrored-database/overview)
- [Azure Synapse Link](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)
- [What is change event streaming (preview)?](../track-changes/change-event-streaming/overview.md) introduced in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and Azure SQL Database.

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
sys.sp_help_change_feed_table
    [ [ @table_group_id = ] 'table_group_id' ]
    [ , [ @table_id = ] 'table_id' ]
    [ , [ @source_schema = ] N'source_schema' ]
    [ , [ @source_name = ] N'source_name' ]
[ ; ]
```

## Arguments

#### [ @table_group_id = ] '*table_group_id*'

The unique identifier of the table group

#### [ @table_id = ] '*table_id*'

The source table identifier

#### [ @source_schema = ] N'*source_schema*'

The source table schema name

#### [ @source_name = ] N'*source_name*'

The source table name

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `table_group_id` | **uniqueidentifier** | The unique identifier of the table group. |
| `table_group_name` | **nvarchar(140)** | The name of the table group. |
| `schema_name` | **sysname** | The schema name of the original table |
| `table_name` | **sysname** | The name of the original table. |
| `table_id` | **uniqueidentifier** | The source table identifier. |
| `destination_location` | **nvarchar(512)** | URL string of the landing zone folder. |
| `workspace_id` | **nvarchar(247)** | The related Synapse workspace Azure resource ID. |
| `state` | **tinyint** | The current state of the table. Valid state values:<br /><br />`1` - Enabled.<br />`2` - Exporting.<br />`3` - Exported.<br />`4` - Active.<br />`5` - Disabled.<br />`6` - Pending Disablement.<br />`7` - Reseeding.<br />`8` - Reseed Notified. |
| `table_object_id` | **int** | The object ID of the change feed table. |

## Permissions

A user with `CONTROL` database permissions, **db_owner** database role membership, or **sysadmin** server role membership can execute this procedure.

## Related content

- [sys.sp_help_change_feed (Transact-SQL)](sp-help-change-feed.md)
- [sys.sp_help_change_feed_settings (Transact-SQL)](sp-help-change-feed-settings.md)
- [sys.sp_help_change_feed_table_groups (Transact-SQL)](sp-help-change-feed-table-groups.md)
- [sys.sp_change_feed_configure_parameters (Transact-SQL)](sp-change-feed-configure-parameters.md)
- [sys.dm_change_feed_log_scan_sessions (Transact-SQL)](../system-dynamic-management-objects/sys-dm-change-feed-log-scan-sessions.md)
- [sys.dm_change_feed_errors (Transact-SQL)](../system-dynamic-management-objects/sys-dm-change-feed-errors.md)
