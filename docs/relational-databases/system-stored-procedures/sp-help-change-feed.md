---
title: "sys.sp_help_change_feed (Transact-SQL)"
description: "The sys.sp_help_change_feed system stored procedure monitors the current configuration of Azure Synapse Link or Fabric Mirrored Database."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: wiassaf, randolphwest
ms.date: 03/12/2024
ms.service: fabric
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.sp_help_change_feed_TSQL"
  - "sys.sp_help_change_feed"
  - "sp_help_change_feed_TSQL"
  - "sp_help_change_feed"
helpviewer_keywords:
  - "sp_help_change_feed"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16 || =azuresqldb-current || =fabric || =azure-sqldw-latest"
---
# sys.sp_help_change_feed (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asa-fabric](../../includes/applies-to-version/sqlserver2022-asdb-asa-fabric.md)]

Monitors the current configuration of the change feed.

This system stored procedure is used for:

- The Azure Synapse Link feature for SQL Server instances and Azure SQL Database. For more information, see [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md).
- The Fabric Mirrored Database feature for Azure SQL Database. For more information, see [What is Mirroring in Fabric?](/fabric/database/mirrored-database/overview)

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
| `state` | **tinyint** | The state of the change feed table. |
| `version` | **binary(10)** | The version of the change feed table. |
| `snapshot_phase` | **tinyint** | Phase of the current snapshot. |
| `snapshot_current_phase_time` | **datetime** | Time when the current snapshot phase started. |
| `snapshot_retry_count` | **int** | Number of times snapshot has attempted to retry. |
| `snapshot_start_time` | **datetime** | Start time of snapshot phase |
| `snapshot_end_time` | **datetime** | End time of snapshot phase |
| `snapshot_row_count` | **int** | The number of rows of data being exported during the snapshot operation of the change feed table |
| `destination_type` | **int** | `0` = Azure Synapse Link. `2` = Fabric mirroring. |

## Permissions

Currently, only a member of the **sysadmin** server role or **db_owner** role, or a user with **CONTROL** database permissions can execute this procedure.

## Examples

To check the status of tables and metadata:

```sql
EXEC sp_help_change_feed;
```

## Related content

- [sys.sp_help_change_feed_table (Transact-SQL)](sp-help-change-feed-table.md)
- [sys.sp_help_change_feed_table_groups (Transact-SQL)](sp-help-change-feed-table-groups.md)
- [sys.sp_help_change_feed_settings (Transact-SQL)](sp-help-change-feed-settings.md)
- [sys.sp_change_feed_configure_parameters (Transact-SQL)](sp-change-feed-configure-parameters.md)
- [sys.dm_change_feed_log_scan_sessions (Transact-SQL)](../system-dynamic-management-views/sys-dm-change-feed-log-scan-sessions.md)
- [sys.dm_change_feed_errors (Transact-SQL)](../system-dynamic-management-views/sys-dm-change-feed-errors.md)

**For Microsoft Fabric mirrored databases**:

- [What is Mirroring in Fabric?](/fabric/database/mirrored-database/overview)
- [Monitor Fabric mirrored database replication](/fabric/database/mirrored-database/monitor)
- [Explore data in your Mirrored database using Microsoft Fabric](/fabric/database/mirrored-database/explore)

**For Azure Synapse Link**:

- [What is Azure Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)
- [Azure Synapse Link for SQL change feed](../../sql-server/synapse-link/synapse-link-sql-server-change-feed.md)
- [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)
- [Troubleshoot: Azure Synapse Link for SQL initial snapshot issues](/azure/synapse-analytics/synapse-link/troubleshoot/troubleshoot-sql-snapshot-issues)
