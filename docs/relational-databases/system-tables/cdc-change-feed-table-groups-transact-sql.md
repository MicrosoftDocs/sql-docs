---
title: "cdc.change_feed_table_groups (Transact-SQL)"
description: "cdc.change_feed_table_groups stores metadata with their associated indexes for Azure Synapse Link for SQL."
author: IdrisMotiwalaMSFT
ms.author: imotiwala
ms.date: 11/04/2022
ms.reviewer: wiassaf
ms.prod: sql
ms.prod_service: "database-engine"
ms.technology: system-objects
ms.topic: "reference"
f1_keywords:
  - "cdc.change_feed_table_groups"
  - "cdc.change_feed_table_groups_TSQL"
helpviewer_keywords:
  - "cdc.change_feed_table_groups"
dev_langs:
  - "TSQL"
---
# cdc.change_feed_table_groups (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Stores metadata with their associated indexes for Azure Synapse Link for SQL.


|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**table_group_id**|**uniqueidentifier**| Unique Identifier of the change feed table group.|
|**table_group_name**|**nvarchar(140)**| The name of the table group.|
|**destination_location**|**nvarchar(512)**| URL string of the landing zone folder. |
|**destination_credential**|**sysname**| The credential name to access the landing zone.|
|**workspace_id**|**nvarchar(247)**| The related Synapse workspace Azure resource ID.|  
|**synapse_workgroup_name**|**nvarchar(50)**| The related Synapse workspace name.|  
|**enabled**|**bit**|Tracks if the table group is enabled for change feed. 1 - Yes, 0 - No. |  
|**destination_type**|**tinyint** |The type of destination assigned to this table group. Valid `destination_type` values: 0 - Synapse. 1 - Event Hub. 2 - Kafka.|  
|**max_message_size_bytes**|**int**| Maximum message size that the destination supports.|  
|**partition_scheme**|**tinyint**| Partitioning scheme to partition the change data while publishing table group.|  

## See also  

- [What is Azure Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview)

## Next steps

- [Manage Azure Synapse Link for SQL Server and Azure SQL Database](../../sql-server/synapse-link/synapse-link-sql-server-change-feed-manage.md)
- [sys.sp_help_change_feed (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-help-change-feed.md)