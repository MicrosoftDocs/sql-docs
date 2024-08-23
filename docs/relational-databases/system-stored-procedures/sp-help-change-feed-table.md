---
title: "sys.sp_help_change_feed_table (Transact-SQL)"
description: "The sys.sp_help_change_feed_table system stored procedure provides the provision or deprovision flow status of Azure Synapse Link for SQL or Fabric Mirrored Databases."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: imotiwala
ms.date: 03/18/2024
ms.service: fabric
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.sp_help_change_feed_table_TSQL"
  - "sys.sp_help_change_feed_table"
  - "sp_help_change_feed_table_TSQL"
  - "sp_help_change_feed_table"
helpviewer_keywords:
  - "sp_help_change_feed_table"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16 || =azuresqldb-current || =fabric || =azure-sqldw-latest"
---
# sys.sp_help_change_feed_table (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-asa-fabric](../../includes/applies-to-version/sqlserver2022-asdb-asa-fabric.md)]

Provides the provision or deprovision status and information of table group and table metadata.

This system stored procedure is used for:

- The Azure Synapse Link feature for SQL Server instances and Azure SQL Database. For more information, see [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md).
- The Fabric Mirrored Database feature for Azure SQL Database. For more information, see [What is Mirroring in Fabric?](/fabric/database/mirrored-database/overview).

## Syntax

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

```syntaxsql
EXECUTE sys.sp_help_change_feed_table;
```

## Arguments

#### @table_group_id

The unique identifier of the table group

#### @table_id

The source table identifier

#### @source_schema

The source table schema name

#### @source_name

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
| `state` | **nvarchar(50)** | The current state of the table. |
| `table_object_id` | **int** | The object ID of the change feed table. |

## Permissions

A user with [CONTROL database permissions](../security/permissions-database-engine.md), **db_owner** database role membership, or **sysadmin** server role membership can execute this procedure.

## Related content

- [sys.sp_help_change_feed (Transact-SQL)](sp-help-change-feed.md)
- [sys.sp_help_change_feed_settings (Transact-SQL)](sp-help-change-feed-settings.md)
- [sys.sp_help_change_feed_table_groups (Transact-SQL)](sp-help-change-feed-table-groups.md)
- [sys.sp_change_feed_configure_parameters (Transact-SQL)](sp-change-feed-configure-parameters.md)
- [sys.dm_change_feed_log_scan_sessions (Transact-SQL)](../system-dynamic-management-views/sys-dm-change-feed-log-scan-sessions.md)
- [sys.dm_change_feed_errors (Transact-SQL)](../system-dynamic-management-views/sys-dm-change-feed-errors.md)

**For Microsoft Fabric mirrored databases**:

- [What is Mirroring in Fabric?](/fabric/database/mirrored-database/overview)
- [Monitor Fabric mirrored database replication](/fabric/database/mirrored-database/monitor)
- [Explore data in your Mirrored database using Microsoft Fabric](/fabric/database/mirrored-database/explore)

**For Azure Synapse Link**:

- [What is Azure Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)
- [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)

- [Troubleshoot: Azure Synapse Link for SQL initial snapshot issues](/azure/synapse-analytics/synapse-link/troubleshoot/troubleshoot-sql-snapshot-issues)
