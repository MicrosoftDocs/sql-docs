---
description: "The sp_help_change_feed system stored procedure monitors the current Azure Synapse Link configuration."
title: "sp_help_change_feed (Transact-SQL)"
ms.date: 11/09/2022
ms.service: synapse-analytics
ms.reviewer: wiassaf
ms.topic: "reference"
f1_keywords: 
  - "sp_help_change_feed_TSQL"
  - "sp_help_change_feed"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_help_change_feed"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-ver16 || =azuresqldb-current"
---
# sp_help_change_feed (Transact-SQL)
[!INCLUDE [sqlserver2022-asdb](../../includes/applies-to-version/sqlserver2022-asdb.md)]

Monitors the current configuration of the [Azure Synapse Link change feed](../../sql-server/synapse-link/synapse-link-sql-server-change-feed.md). For more information, see [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md).

## Syntax  

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
```syntaxsql  
EXECUTE sys.sp_help_change_feed;
```

## Result set

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
| **table_group_id** | **uniqueidentifier** | The unique identifier of the table group.| 
| **table_group_name** | **nvarchar(140)** | The name of the table group.| 
| **destination_location** | **nvarchar(512)** | URL string of the landing zone folder.| 
| **destination_credential** | **sysname** | The credential name to access the landing zone. |
| **workspace_id** | **nvarchar(247)** | The related Synapse workspace Azure resource ID. |
| **synapse_workgroup_name** | **nvarchar(50)** | The related Synapse workspace name. |
| **schema_name** | **sysname** | The database schema name of the change feed table. | 
| **table_name** | **sysname** | The name of the change feed table. | 
| **table_id** | **uniqueidentifier** | The unique identifier for the change feed table. Generated during change feed setup workflow. | 
| **table_object_id** | **int** | The object ID of the change feed table. | 
| **state** | **tinyint** | The state of the change feed table. | 
| **version** | **binary(10)** | The version of the change feed table. | 
| **snapshot_phase** | **tinyint** | Phase of the current snapshot. | 
| **snapshot_current_phase_time** | **datetime** | Time when the current snapshot phase started. | 
| **snapshot_retry_count** | **int** | Number of times snapshot has attempted to retry.| 
| **snapshot_start_time** | **datetime** | Start time of snapshot phase | 
| **snapshot_end_time** | **datetime** | End time of snapshot phase|
| **snapshot_row_count** | **int** | The number of rows of data being exported during the snapshot operation of the change feed table |
 
## Permissions  

 Currently, only a member of the sysadmin server role or db_owner role, or a user with CONTROL database permissions can execute this procedure.

## See also  

- [What is Azure Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)

## Next steps

- [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)