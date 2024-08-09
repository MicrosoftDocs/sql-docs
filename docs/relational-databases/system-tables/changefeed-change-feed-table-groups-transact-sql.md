---
title: "changefeed.change_feed_table_groups (Transact-SQL)"
description: "changefeed.change_feed_table_groups stores metadata that is used to configure change feed table groups for Azure Synapse Link for SQL."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: imotiwala
ms.date: 03/18/2024
ms.service: azure-synapse-analytics
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "changefeed.change_feed_table_groups"
  - "changefeed.change_feed_table_groups_TSQL"
helpviewer_keywords:
  - "changefeed.change_feed_table_groups"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16||=azuresqldb-current||=azure-sqldw-latest"
---
# changefeed.change_feed_table_groups (Transact-SQL)
[!INCLUDE [sqlserver2022-asdb-asa](../../includes/applies-to-version/sqlserver2022-asdb-asa.md)]

Contains metadata that is used to configure change feed table groups for Azure Synapse Link for SQL.

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
| `table_group_id` |**uniqueidentifier**| Unique identifier of the change feed table group.|
| `table_group_name` |**nvarchar(140)**| The name of the table group.|
| `destination_location` |**nvarchar(512)**| URL string of the landing zone folder. |
| `destination_credential` |**sysname**| The credential name to access the landing zone.|
| `workspace_id` |**nvarchar(247)**| The related Azure Synapse Analytics workspace Azure resource ID.|  
| `synapse_workgroup_name` |**nvarchar(50)**| The related Synapse workspace name.|  
| `enabled` |**bit**|Tracks if the table group is enabled for change feed. `1` - Yes, `0` - No. |

## Remarks

The `changefeed.change_feed_table_groups` system table isn't used in [Fabric mirrored databases](/fabric/database/mirrored-database/overview), instead use the [sys.sp_help_change_feed_table_groups](../system-stored-procedures/sp-help-change-feed-table-groups.md) system stored procedure.

## Related content

- [What is Azure Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)
- [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)
- [sys.sp_help_change_feed (Transact-SQL)](../system-stored-procedures/sp-help-change-feed.md)
- [changefeed.change_feed_tables (Transact-SQL)](changefeed-change-feed-tables-transact-sql.md)
- [changefeed.change_feed_settings (Transact-SQL)](changefeed-change-feed-settings.md)
