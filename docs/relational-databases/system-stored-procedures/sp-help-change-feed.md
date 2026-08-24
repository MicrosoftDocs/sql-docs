---
title: "sys.sp_help_change_feed (Transact-SQL)"
description: "The sys.sp_help_change_feed system stored procedure monitors the current configuration of Azure Synapse Link or Fabric Mirrored Database."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: imotiwala, randolphwest, anagha-todalbagi, ajayj
ms.date: 06/23/2025
ms.service: fabric
ms.subservice: system-objects
ms.topic: "reference"
ms.custom:
  - ignite-2025
f1_keywords:
  - "sys.sp_help_change_feed_TSQL"
  - "sys.sp_help_change_feed"
  - "sp_help_change_feed_TSQL"
  - "sp_help_change_feed"
helpviewer_keywords:
  - "sp_help_change_feed"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16 || =azuresqldb-current || =azuresqldb-mi-current || =fabric || =fabric-sqldb || =azure-sqldw-latest"
---
# sys.sp_help_change_feed (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asdbmi-asa-fabricmirroredsqldb-fabricsqldb](../../includes/applies-to-version/sqlserver2022-asdb-asdbmi-asa-fabricmirroredsqldb-fabricsqldb.md)]

Monitors the current configuration of the change feed.

This system stored procedure is used for:

- [SQL database in Microsoft Fabric](/fabric/database/sql/overview)
- [Microsoft Fabric mirrored databases](/fabric/database/mirrored-database/overview)
- [Azure Synapse Link](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)
- [Change event streaming (preview)](../track-changes/change-event-streaming/overview.md) introduced in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and Azure SQL Database. 

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
EXECUTE sys.sp_help_change_feed;
```

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `table_group_id` | **uniqueidentifier** | The unique identifier of the table group. |
| `table_group_name` | **nvarchar(140)** | The name of the table group. |
| `destination_location` | **nvarchar(512)** | URL string of the landing zone folder. |
| `destination_credential` | **sysname** | The credential name to access the landing zone. |
| `workspace_id` | **nvarchar(247)** | The related Synapse workspace Azure resource ID. |
| `synapse_workgroup_name` | **nvarchar(50)** | The related Synapse workspace name. |
| `schema_name` | **sysname** | The database schema name of the change feed table. |
| `table_name` | **sysname** | The name of the change feed table. |
| `table_id` | **uniqueidentifier** | The unique identifier for the change feed table. Generated during change feed setup workflow. |
| `table_object_id` | **int** | The object ID of the change feed table. |
| `state` | **tinyint** | The state of the change feed table. Valid state values:<br /><br />`1` - Enabled.<br />`2` - Exporting.<br />`3` - Exported.<br />`4` - Active.<br />`5` - Disabled.<br />`6` - Pending Disablement.<br />`7` - Reseeding.<br />`8` - Reseed Notified. |
| `version` | **binary(10)** | The version of the change feed table. |
| `snapshot_phase` | **tinyint** | Phase of the current snapshot which progresses from one to six.<br /><br />`1` - ABORT_PRIOR_SNAPSHOT_IF_ANY<br />`2` - SET_TABLEVERSIONLSN<br />`3` - EXPORT_SCHEMA_FILE<br />`4` - EMIT_SNAPSHOT_BEGINENTRY<br />`5` - EXPORT_DATA_FILE<br />`6` - EMIT_SNAPSHOT_ENDENTRY |
| `snapshot_current_phase_time` | **datetime** | Time when the current snapshot phase started. |
| `snapshot_retry_count` | **int** | Number of times snapshot has attempted to retry. |
| `snapshot_start_time` | **datetime** | Start time of snapshot phase |
| `snapshot_end_time` | **datetime** | End time of snapshot phase |
| `snapshot_row_count` | **int** | The number of rows of data being exported during the snapshot operation of the change feed table |
| `destination_type` | **int** | `0` = Azure Synapse Link.<br />`2` = Fabric mirroring. |

## Permissions

Currently, only a member of the **sysadmin** server role or **db_owner** role, or a user with **CONTROL** database permissions can execute this procedure.

## Examples

To check the status of tables and metadata:

```sql
EXECUTE sp_help_change_feed;
```

## Related content

- [sys.sp_help_change_feed_table (Transact-SQL)](sp-help-change-feed-table.md)
- [sys.sp_help_change_feed_table_groups (Transact-SQL)](sp-help-change-feed-table-groups.md)
- [sys.sp_help_change_feed_settings (Transact-SQL)](sp-help-change-feed-settings.md)
- [sys.sp_change_feed_configure_parameters (Transact-SQL)](sp-change-feed-configure-parameters.md)
- [sys.dm_change_feed_log_scan_sessions (Transact-SQL)](../system-dynamic-management-objects/sys-dm-change-feed-log-scan-sessions.md)
- [sys.dm_change_feed_errors (Transact-SQL)](../system-dynamic-management-objects/sys-dm-change-feed-errors.md)
