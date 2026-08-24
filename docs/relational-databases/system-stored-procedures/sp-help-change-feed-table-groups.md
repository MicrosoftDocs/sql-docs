---
title: "sys.sp_help_change_feed_table_groups (Transact-SQL)"
description: "The sys.sp_help_change_feed_table_groups_groups system stored procedure returns the table group information for Fabric Mirrored Databases."
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
  - "sys.sp_help_change_feed_table_groups_TSQL"
  - "sys.sp_help_change_feed_table_groups"
  - "sp_help_change_feed_table_groups_TSQL"
  - "sp_help_change_feed_table_groups"
helpviewer_keywords:
  - "sp_help_change_feed_table_groups"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16 || =azuresqldb-current || =azuresqldb-mi-current || =fabric || =fabric-sqldb || =azure-sqldw-latest"
---
# sys.sp_help_change_feed_table_groups (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asdbmi-asa-fabricmirroredsqldb-fabricsqldb](../../includes/applies-to-version/sqlserver2022-asdb-asdbmi-asa-fabricmirroredsqldb-fabricsqldb.md)]

Returns metadata that is used to configure change feed table groups.

This system stored procedure is used for:

- [SQL database in Microsoft Fabric](/fabric/database/sql/overview)
- [Microsoft Fabric mirrored databases](/fabric/database/mirrored-database/overview)
- [Azure Synapse Link](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)
- [What is change event streaming (preview)?](../track-changes/change-event-streaming/overview.md) introduced in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and Azure SQL Database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sys.sp_help_change_feed_table_groups
[ ; ]
```

## Arguments

None.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `table_group_id` | **uniqueidentifier** | Unique identifier of the change feed table group. |
| `table_group_name` | **nvarchar(140)** | The name of the table group. |
| `destination_location` | **nvarchar(512)** | URL string of the landing zone folder. |
| `destination_credential` | **sysname** | The credential name to access the landing zone. |
| `workspace_id` | **nvarchar(247)** | The related workspace Azure resource ID. |
| `synapse_workgroup_name` | **nvarchar(50)** | The related workspace name. |
| `enabled` | **bit** | Tracks if the table group is enabled for change feed.<br /><br />`1` = Yes<br />`0` = No. |
| `destination_type` | **int** | `0` = Azure Synapse Link<br />`2` = Fabric mirroring. |

## Permissions

A user with `CONTROL` database permissions, **db_owner** database role membership, or **sysadmin** server role membership can execute this procedure.

## Related content

- [sys.sp_help_change_feed (Transact-SQL)](sp-help-change-feed.md)
- [sys.sp_help_change_feed_table (Transact-SQL)](sp-help-change-feed-table.md)
- [sys.sp_help_change_feed_settings (Transact-SQL)](sp-help-change-feed-settings.md)
- [sys.sp_change_feed_configure_parameters (Transact-SQL)](sp-change-feed-configure-parameters.md)
- [sys.dm_change_feed_log_scan_sessions (Transact-SQL)](../system-dynamic-management-objects/sys-dm-change-feed-log-scan-sessions.md)
- [sys.dm_change_feed_errors (Transact-SQL)](../system-dynamic-management-objects/sys-dm-change-feed-errors.md)
