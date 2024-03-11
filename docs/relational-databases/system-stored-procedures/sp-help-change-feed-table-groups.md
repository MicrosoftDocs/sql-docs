---
title: "sys.sp_help_change_feed_table_groups (Transact-SQL)"
description: "The sys.sp_help_change_feed_table_groups_groups system stored procedure returns the table group information for Fabric Mirrored Databases."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: imotiwala
ms.date: 03/11/2024
ms.service: fabric
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.sp_help_change_feed_table_groups_TSQL"
  - "sys.sp_help_change_feed_table_groups"
  - "sp_help_change_feed_table_groups_TSQL"
  - "sp_help_change_feed_table_groups"
helpviewer_keywords:
  - "sp_help_change_feed_table_groups"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver16||=azuresqldb-current||=fabric"
---
# sys.sp_help_change_feed_table_groups (Transact-SQL)

[!INCLUDE [sqlserver2022-asdb-fabric](../../includes/applies-to-version/sqlserver2022-asdb-fabric.md)]

Returns metadata that is used to configure change feed table groups Azure Synapse Link for SQL.

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
| `table_group_id` |**uniqueidentifier**| Unique identifier of the change feed table group.|
| `table_group_name` |**nvarchar(140)**| The name of the table group.|
| `destination_location` |**nvarchar(512)**| URL string of the landing zone folder. |
| `destination_credential` |**sysname**| The credential name to access the landing zone.|
| `workspace_id` |**nvarchar(247)**| The related workspace Azure resource ID.|  
| `synapse_workgroup_name` |**nvarchar(50)**| The related workspace name.|  
| `enabled` |**bit**|Tracks if the table group is enabled for change feed. `1` - Yes, `0` - No. |

## Permissions

A user with [CONTROL database permissions](../security/permissions-database-engine.md), **db_owner** database role membership, or **sysadmin** server role membership can execute this procedure.

## Related content

- [sys.dm_change_feed_errors (Transact-SQL)](../system-dynamic-management-views/sys-dm-change-feed-errors.md)
- [sys.sp_help_change_feed (Transact-SQL)](sp-help-change-feed.md)
- [sys.sp_help_change_feed_table (Transact-SQL)](sp-help-change-feed-table.md)
- [sys.dm_change_feed_log_scan_sessions (Transact-SQL)](../system-dynamic-management-views/sys-dm-change-feed-log-scan-sessions.md)
- [Microsoft Fabric mirrored databases (Preview)](/fabric/database/mirrored-database/overview)
- [Microsoft Fabric mirrored databases monitoring](/fabric/database/mirrored-database/monitor)
- [Explore data in your Mirrored database using Microsoft Fabric](/fabric/database/mirrored-database/explore)
